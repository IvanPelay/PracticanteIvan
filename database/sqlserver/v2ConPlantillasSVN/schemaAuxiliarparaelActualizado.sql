-- ===================================================================
-- Script: Agregar vigencia a Enc016ProgramacionReporteador
-- Sistema: KROM - Reporteador Automático
-- Fecha: Marzo 2026
-- Descripción:
--   Se agregan f_VigenciaInicio y f_VigenciaFin para controlar
--   el período de vida de una programación recurrente.
--
--   Reglas:
--   - f_VigenciaInicio: obligatoria siempre, default = GETDATE()
--   - f_VigenciaFin:    obligatoria para D/S/M/Q, opcional para U
--                       NULL = sin límite (solo permitido en U)
--   - Al alcanzar f_VigenciaFin el SP pone f_ProximaEjecucion = NULL
--     y la programación queda activa pero sin próxima ejecución.
-- ===================================================================
USE [Solium];
GO

-- 1. Agregar columnas de vigencia
ALTER TABLE [dbo].[Enc016ProgramacionReporteador]
    ADD f_VigenciaInicio DATETIME NOT NULL
        CONSTRAINT DF_Enc016_VigenciaInicio DEFAULT (GETDATE());
GO

ALTER TABLE [dbo].[Enc016ProgramacionReporteador]
    ADD f_VigenciaFin DATETIME NULL;
GO

-- 2. Descripciones
EXEC sp_addextendedproperty
    @name  = N'MS_Description',
    @value = N'Fecha desde la cual la programación está activa. ' +
             N'No se ejecutará antes de esta fecha aunque f_ProximaEjecucion lo indique.',
    @level0type = N'SCHEMA', @level0name = N'dbo',
    @level1type = N'TABLE',  @level1name = N'Enc016ProgramacionReporteador',
    @level2type = N'COLUMN', @level2name = N'f_VigenciaInicio';
GO

EXEC sp_addextendedproperty
    @name  = N'MS_Description',
    @value = N'Fecha hasta la cual la programación es válida. ' +
             N'Obligatoria para frecuencias D/S/M/Q. NULL solo permitido en U (única). ' +
             N'Al superarse, el SP pone f_ProximaEjecucion = NULL.',
    @level0type = N'SCHEMA', @level0name = N'dbo',
    @level1type = N'TABLE',  @level1name = N'Enc016ProgramacionReporteador',
    @level2type = N'COLUMN', @level2name = N'f_VigenciaFin';
GO

-- 3. Índice para filtrar por vigencia en el SP
CREATE NONCLUSTERED INDEX IX_Enc016_VigenciaFin
    ON [dbo].[Enc016ProgramacionReporteador] (f_VigenciaFin)
    WHERE f_VigenciaFin IS NOT NULL;
GO

-- 4. Recrear Vig016ProgramacionesPendientes con filtro de vigencia
IF OBJECT_ID('dbo.Vig016ProgramacionesPendientes', 'V') IS NOT NULL
    DROP VIEW dbo.Vig016ProgramacionesPendientes;
GO

CREATE VIEW [dbo].[Vig016ProgramacionesPendientes] AS
SELECT
    prog.i_Cve_Programacion,
    prog.t_Nombre,
    prog.t_Frecuencia,
    prog.t_Hora,
    prog.f_ProximaEjecucion,
    prog.f_VigenciaInicio,
    prog.f_VigenciaFin,
    pl.i_Cve_Plantilla,
    pl.t_Nombre       AS t_NombrePlantilla,
    pl.t_ColumnasConfig,
    pl.t_ParametrosConfig,
    pl.t_RutaPlantilla,
    con.i_Cve_Consulta,
    con.t_NombreVista,
    con.t_NombreSP
FROM [dbo].[Enc016ProgramacionReporteador]  prog
INNER JOIN [dbo].[Cat016PlantillasReporteador]  pl  ON pl.i_Cve_Plantilla = prog.i_Cve_Plantilla
INNER JOIN [dbo].[Cat016ConsultasReporteador]   con ON con.i_Cve_Consulta  = pl.i_Cve_Consulta
WHERE prog.i_Cve_Estado = 1
  AND pl.i_Cve_Estado   = 1
  AND con.i_Cve_Estado  = 1
  AND prog.f_ProximaEjecucion IS NOT NULL
  AND prog.f_ProximaEjecucion <= GETDATE()
  -- Vigencia activa
  AND GETDATE() >= prog.f_VigenciaInicio
  AND (prog.f_VigenciaFin IS NULL OR GETDATE() <= prog.f_VigenciaFin)
GO

-- 5. Actualizar SP016ProcesarColaReportes:
--    - Devuelve f_VigenciaFin para que el motor lo registre
--    - Al calcular próxima ejecución verifica si supera f_VigenciaFin;
--      si la supera pone f_ProximaEjecucion = NULL (queda sin ejecutar más)
IF OBJECT_ID('dbo.SP016ProcesarColaReportes', 'P') IS NOT NULL
    DROP PROCEDURE dbo.SP016ProcesarColaReportes;
GO

CREATE PROCEDURE [dbo].[SP016ProcesarColaReportes]
    @UsuarioSistema VARCHAR(50) = 'MOTOR_REPORTEADOR'
AS
/*
===================================================================
v4 - Agrega soporte de vigencia:
  - f_VigenciaInicio / f_VigenciaFin filtran en la vista
  - Al calcular próxima ejecución: si supera f_VigenciaFin
    se pone f_ProximaEjecucion = NULL en lugar de la fecha calculada
===================================================================
*/
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    CREATE TABLE #Pendientes (
        i_Cve_Programacion INT,
        i_Cve_Plantilla    INT,
        f_ProximaEjecucion DATETIME,
        f_VigenciaFin      DATETIME NULL
    );

    BEGIN TRY
        BEGIN TRANSACTION;

            INSERT INTO #Pendientes
                (i_Cve_Programacion, i_Cve_Plantilla, f_ProximaEjecucion, f_VigenciaFin)
            SELECT i_Cve_Programacion, i_Cve_Plantilla, f_ProximaEjecucion, f_VigenciaFin
            FROM [dbo].[Vig016ProgramacionesPendientes] WITH (UPDLOCK, READPAST);

            INSERT INTO [dbo].[Bit016GeneracionReporteador]
                (i_Cve_Programacion, f_FechaInicio, t_Proceso,
                 t_ParametrosUsados, t_UsuarioRegistro, i_Cve_Estatus, i_Cve_Estado)
            SELECT
                p.i_Cve_Programacion, GETDATE(), 'PROCESANDO',
                prog.t_Parametros, @UsuarioSistema, 1, 1
            FROM #Pendientes p
            INNER JOIN [dbo].[Enc016ProgramacionReporteador] prog
                ON p.i_Cve_Programacion = prog.i_Cve_Programacion;

            -- Calcular próxima ejecución respetando vigencia
            UPDATE prog
            SET
                prog.f_UltimaEjecucion  = tmp.f_ProximaEjecucion,
                prog.f_ProximaEjecucion = CASE
                    -- Si tiene fecha fin de vigencia y la próxima ejecución la supera → NULL
                    WHEN tmp.f_VigenciaFin IS NOT NULL AND
                         CASE prog.t_Frecuencia
                             WHEN 'D' THEN DATEADD(DAY,  1,  tmp.f_ProximaEjecucion)
                             WHEN 'S' THEN DATEADD(DAY,  7,  tmp.f_ProximaEjecucion)
                             WHEN 'Q' THEN DATEADD(DAY, 15,  tmp.f_ProximaEjecucion)
                             WHEN 'M' THEN DATEADD(MONTH, 1, tmp.f_ProximaEjecucion)
                             ELSE NULL
                         END > tmp.f_VigenciaFin
                    THEN NULL
                    -- Cálculo normal
                    WHEN prog.t_Frecuencia = 'D' THEN DATEADD(DAY,  1,  tmp.f_ProximaEjecucion)
                    WHEN prog.t_Frecuencia = 'S' THEN DATEADD(DAY,  7,  tmp.f_ProximaEjecucion)
                    WHEN prog.t_Frecuencia = 'Q' THEN DATEADD(DAY, 15,  tmp.f_ProximaEjecucion)
                    WHEN prog.t_Frecuencia = 'M' THEN
                        CASE
                            WHEN prog.i_DiaMes IS NOT NULL THEN
                                DATEADD(DAY,
                                    prog.i_DiaMes - DAY(DATEADD(MONTH,1,tmp.f_ProximaEjecucion)),
                                    DATEADD(MONTH, 1, tmp.f_ProximaEjecucion))
                            ELSE DATEADD(MONTH, 1, tmp.f_ProximaEjecucion)
                        END
                    WHEN prog.t_Frecuencia = 'U' THEN NULL
                    ELSE prog.f_ProximaEjecucion
                END,
                prog.t_UsuarioRegistro = @UsuarioSistema
            FROM [dbo].[Enc016ProgramacionReporteador] prog
            INNER JOIN #Pendientes tmp
                ON prog.i_Cve_Programacion = tmp.i_Cve_Programacion;

        COMMIT TRANSACTION;

        SELECT
            tmp.i_Cve_Programacion,
            gen.i_Cve_Generacion,
            pl.t_Nombre         AS NombreReporte,
            pl.t_RutaPlantilla,
            pl.t_FormatoSalida,
            pl.t_ParametrosConfig,
            pl.t_ColumnasConfig,
            prog.t_Parametros,
            prog.t_DiasSemana,
            prog.i_DiaMes,
            prog.t_Frecuencia,
            prog.f_VigenciaInicio,
            prog.f_VigenciaFin,
            con.t_NombreVista,
            con.t_NombreSP
        FROM #Pendientes tmp
        INNER JOIN [dbo].[Cat016PlantillasReporteador]   pl   ON pl.i_Cve_Plantilla     = tmp.i_Cve_Plantilla
        INNER JOIN [dbo].[Cat016ConsultasReporteador]    con  ON con.i_Cve_Consulta     = pl.i_Cve_Consulta
        INNER JOIN [dbo].[Enc016ProgramacionReporteador] prog ON prog.i_Cve_Programacion = tmp.i_Cve_Programacion
        INNER JOIN [dbo].[Bit016GeneracionReporteador]   gen  ON gen.i_Cve_Programacion  = tmp.i_Cve_Programacion
        WHERE gen.t_Proceso   = 'PROCESANDO'
          AND gen.f_FechaFin IS NULL
          AND gen.i_Cve_Generacion = (
              SELECT MAX(g2.i_Cve_Generacion)
              FROM [dbo].[Bit016GeneracionReporteador] g2
              WHERE g2.i_Cve_Programacion = gen.i_Cve_Programacion);

    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        DECLARE @Msg NVARCHAR(4000) = ERROR_MESSAGE();
        DECLARE @Sev INT = ERROR_SEVERITY();
        DECLARE @Sta INT = ERROR_STATE();
        RAISERROR(@Msg, @Sev, @Sta);
    END CATCH

    DROP TABLE #Pendientes;
END
GO

GRANT EXECUTE ON [dbo].[SP016ProcesarColaReportes] TO [public];
GO

-- 6. Actualizar VE y VT de Programación para incluir los nuevos campos
IF OBJECT_ID('dbo.Vt016Programacion', 'V') IS NOT NULL
    DROP VIEW dbo.Vt016Programacion;
GO

CREATE VIEW [dbo].[Vt016Programacion] AS
SELECT
    p.i_Cve_Programacion,
    p.i_Cve_Plantilla,
    pl.t_Nombre     AS t_NombrePlantilla,
    p.t_Nombre      AS t_NombreProgramacion,
    CASE p.t_Frecuencia
        WHEN 'U' THEN 'Única'
        WHEN 'D' THEN 'Diaria'
        WHEN 'S' THEN 'Semanal'
        WHEN 'M' THEN 'Mensual'
        WHEN 'Q' THEN 'Quincenal'
    END             AS t_FrecuenciaDesc,
    p.t_DiasSemana,
    p.i_DiaMes,
    p.t_Hora,
    p.t_Parametros,
    p.f_VigenciaInicio,
    p.f_VigenciaFin,
    p.f_ProximaEjecucion,
    p.f_UltimaEjecucion,
    p.f_FechaRegistro,
    p.t_UsuarioRegistro,
    CASE WHEN p.i_Cve_Estatus = 1 THEN 'Activo' ELSE 'Inactivo' END AS t_Estatus,
    CASE WHEN p.i_Cve_Estado  = 1 THEN 'Activo' ELSE 'Inactivo' END AS t_Estado
FROM [dbo].[Enc016ProgramacionReporteador] p
INNER JOIN [dbo].[Cat016PlantillasReporteador] pl ON p.i_Cve_Plantilla = pl.i_Cve_Plantilla
WHERE p.i_Cve_Estado = 1
GO

IF OBJECT_ID('dbo.Ve016IUProgramacion', 'V') IS NOT NULL
    DROP VIEW dbo.Ve016IUProgramacion;
GO

CREATE VIEW [dbo].[Ve016IUProgramacion] AS
SELECT * FROM (
    SELECT 'i_Cve_Programacion' AS Nombre, 1 AS Llave, 11 AS Longitud, 0 AS TipoDato, 1 AS Visible, 'Clave Programación'   AS NombreColumna, 0 AS PuedeInsertar, 0 AS PuedeModificar, ''   AS ValorDefault, '1' AS TipoFiltro
    UNION ALL SELECT 'i_Cve_Plantilla' AS Nombre, 0 AS Llave, 11 AS Longitud, 0 AS TipoDato, 1 AS Visible, 'Clave Plantilla' AS NombreColumna, 1 AS PuedeInsertar, 1 AS PuedeModificar, '' AS ValorDefault, '2' AS TipoFiltro
    UNION ALL SELECT 't_NombrePlantilla' AS Nombre, 0 AS Llave, 200 AS Longitud, 1 AS TipoDato, 1 AS Visible, 'Plantilla' AS NombreColumna, 0 AS PuedeInsertar, 0 AS PuedeModificar, '' AS ValorDefault, '2' AS TipoFiltro
    UNION ALL SELECT 't_NombreProgramacion' AS Nombre, 0 AS Llave, 150 AS Longitud, 1 AS TipoDato, 1 AS Visible, 'Nombre Programación' AS NombreColumna, 1 AS PuedeInsertar, 1 AS PuedeModificar, '' AS ValorDefault, '2' AS TipoFiltro
    UNION ALL SELECT 't_FrecuenciaDesc' AS Nombre, 0 AS Llave, 20 AS Longitud, 1 AS TipoDato, 1 AS Visible, 'Frecuencia' AS NombreColumna, 1 AS PuedeInsertar, 1 AS PuedeModificar, '' AS ValorDefault, '2' AS TipoFiltro
    UNION ALL SELECT 't_DiasSemana' AS Nombre, 0 AS Llave, 20 AS Longitud, 1 AS TipoDato, 1 AS Visible, 'Días Semana' AS NombreColumna, 1 AS PuedeInsertar, 1 AS PuedeModificar, '' AS ValorDefault, '2' AS TipoFiltro
    UNION ALL SELECT 'i_DiaMes' AS Nombre, 0 AS Llave, 2 AS Longitud, 0 AS TipoDato, 1 AS Visible, 'Día Mes' AS NombreColumna, 1 AS PuedeInsertar, 1 AS PuedeModificar, '' AS ValorDefault, '2' AS TipoFiltro
    UNION ALL SELECT 't_Hora' AS Nombre, 0 AS Llave, 8 AS Longitud, 4 AS TipoDato, 1 AS Visible, 'Hora' AS NombreColumna, 1 AS PuedeInsertar, 1 AS PuedeModificar, '' AS ValorDefault, '2' AS TipoFiltro
    UNION ALL SELECT 't_Parametros' AS Nombre, 0 AS Llave, 2147483647 AS Longitud, 1 AS TipoDato, 1 AS Visible, 'Parámetros (JSON)' AS NombreColumna, 1 AS PuedeInsertar, 1 AS PuedeModificar, '' AS ValorDefault, '2' AS TipoFiltro
    UNION ALL SELECT 'f_VigenciaInicio' AS Nombre, 0 AS Llave, 23 AS Longitud, 4 AS TipoDato, 1 AS Visible, 'Vigencia Inicio' AS NombreColumna, 1 AS PuedeInsertar, 1 AS PuedeModificar, '' AS ValorDefault, '2' AS TipoFiltro
    UNION ALL SELECT 'f_VigenciaFin' AS Nombre, 0 AS Llave, 23 AS Longitud, 4 AS TipoDato, 1 AS Visible, 'Vigencia Fin' AS NombreColumna, 1 AS PuedeInsertar, 1 AS PuedeModificar, '' AS ValorDefault, '2' AS TipoFiltro
    UNION ALL SELECT 'f_ProximaEjecucion' AS Nombre, 0 AS Llave, 23 AS Longitud, 4 AS TipoDato, 1 AS Visible, 'Próxima Ejecución' AS NombreColumna, 0 AS PuedeInsertar, 0 AS PuedeModificar, '' AS ValorDefault, '2' AS TipoFiltro
    UNION ALL SELECT 'f_UltimaEjecucion' AS Nombre, 0 AS Llave, 23 AS Longitud, 4 AS TipoDato, 1 AS Visible, 'Última Ejecución' AS NombreColumna, 0 AS PuedeInsertar, 0 AS PuedeModificar, '' AS ValorDefault, '2' AS TipoFiltro
    UNION ALL SELECT 'f_FechaRegistro' AS Nombre, 0 AS Llave, 23 AS Longitud, 4 AS TipoDato, 1 AS Visible, 'Fecha Registro' AS NombreColumna, 0 AS PuedeInsertar, 0 AS PuedeModificar, '' AS ValorDefault, '2' AS TipoFiltro
    UNION ALL SELECT 't_UsuarioRegistro' AS Nombre, 0 AS Llave, 50 AS Longitud, 1 AS TipoDato, 1 AS Visible, 'Usuario Registro' AS NombreColumna, 0 AS PuedeInsertar, 0 AS PuedeModificar, '' AS ValorDefault, '2' AS TipoFiltro
    UNION ALL SELECT 't_Estatus' AS Nombre, 0 AS Llave, 10 AS Longitud, 1 AS TipoDato, 1 AS Visible, 'Estatus' AS NombreColumna, 0 AS PuedeInsertar, 0 AS PuedeModificar, '' AS ValorDefault, '2' AS TipoFiltro
    UNION ALL SELECT 't_Estado' AS Nombre, 0 AS Llave, 10 AS Longitud, 1 AS TipoDato, 1 AS Visible, 'Estado' AS NombreColumna, 0 AS PuedeInsertar, 0 AS PuedeModificar, '' AS ValorDefault, '2' AS TipoFiltro
) AS VE
GO

PRINT '========================================================='
PRINT 'Vigencia agregada a Enc016ProgramacionReporteador'
PRINT 'f_VigenciaInicio: obligatoria, default GETDATE()'
PRINT 'f_VigenciaFin:    opcional (NULL = sin límite / frecuencia U)'
PRINT 'SP016ProcesarColaReportes: v4 con control de vigencia'
PRINT 'Vig016ProgramacionesPendientes: filtra por vigencia activa'
PRINT 'Vt016Programacion: incluye f_VigenciaInicio y f_VigenciaFin'
PRINT 'Ve016IUProgramacion: incluye campos de vigencia'
PRINT '========================================================='
GO