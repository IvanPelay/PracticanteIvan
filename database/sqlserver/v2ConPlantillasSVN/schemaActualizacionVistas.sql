-- ===================================================================
-- Script: Esquema BD Módulo Reporteador Automático v3
-- Sistema: KROM - Automatización de Reportes Aduanales
-- Familia: 016 (Reportes)
-- Autor: Residente Iván Pelayo Martínez
-- Fecha: Marzo 2026
-- Base de datos: Solium (BD por defecto del CMF)
-- 
-- Descripción: Versión simplificada según recomendación de asesora:
--   - Solium es la BD por defecto, no se necesita guardar servidor
--     ni cadena de conexión.
--   - Las vistas usan USE [BD] internamente y referencian otras BDs
--     directamente en sus JOINs, CTEs o subconsultas.
--   - Ya existe una clase en el framework para conectarse a SysExpert.
--   - Cat016ConsultasReporteador guarda solo: nombre visible,
--     descripción, nombre de la vista (obligatoria) y nombre del SP
--     (opcional). El registro lo hace directamente el DBA en la tabla.
--
-- Cambios respecto a v2:
--   - Se eliminan: t_NombreServidor, t_NombreBaseDatos,
--     t_CadenaConexion, t_NotasTecnicas de Cat016Consultas
--   - t_NombreVista pasa a ser NOT NULL (obligatoria)
--   - t_NombreSP queda NULL (opcional)
-- ===================================================================
 
USE [Solium];
GO
 
-- ===================================================================
-- PASO 1: NUEVA TABLA Cat016ConsultasReporteador
--   Catálogo de consultas disponibles para reportes.
--   Solo DBA/Desarrolladores registran aquí directamente en BD.
--   El usuario del sistema solo ve el t_Nombre en un ComboBox.
--
--   La vista (t_NombreVista) es OBLIGATORIA: contiene los JOINs y
--   referencias a otras BDs (SysExpert, Aduana, etc.) de forma
--   transparente para el motor.
--
--   El SP (t_NombreSP) es OPCIONAL: si existe, el motor lo ejecuta
--   pasándole los parámetros. Si es NULL, el motor consulta
--   directamente la vista con filtros básicos.
-- ===================================================================
CREATE TABLE [dbo].[Cat016ConsultasReporteador] (
 
    i_Cve_Consulta    INT          IDENTITY(1,1) NOT NULL,
 
    -- Lo que ve el usuario en el ComboBox del form de plantillas
    t_Nombre          VARCHAR(200) NOT NULL,
    t_Descripcion     VARCHAR(500) NULL,
 
    -- Vista base OBLIGATORIA
    -- Contiene todos los JOINs. Puede referenciar otras BDs
    -- directamente: SysExpert.dbo.Tabla, Aduana.dbo.Tabla, etc.
    -- Ejemplo: 'VT016OperacionesAnexo24'
    t_NombreVista     VARCHAR(200) NOT NULL,
 
    -- Stored Procedure OPCIONAL
    -- Si existe: el motor lo ejecuta pasando los parámetros del JSON.
    -- Si es NULL: el motor ejecuta SELECT * FROM [vista] con filtros.
    -- Ejemplo: 'spVT016OperacionesAnexo24'
    t_NombreSP        VARCHAR(200) NULL,
 
    -- Auditoría
    f_FechaRegistro   DATETIME     NOT NULL CONSTRAINT DF_Cat016Con_Fecha    DEFAULT (GETDATE()),
    t_UsuarioRegistro VARCHAR(50)  NOT NULL CONSTRAINT DF_Cat016Con_Usuario  DEFAULT (''),
    i_Cve_Estatus     INT          NOT NULL CONSTRAINT DF_Cat016Con_Estatus  DEFAULT (1),
    i_Cve_Estado      INT          NOT NULL CONSTRAINT DF_Cat016Con_Estado   DEFAULT (1),
 
    CONSTRAINT PK_Cat016ConsultasReporteador
        PRIMARY KEY CLUSTERED (i_Cve_Consulta ASC),
    CONSTRAINT UQ_Cat016Con_Nombre
        UNIQUE (t_Nombre),
    CONSTRAINT UQ_Cat016Con_Vista
        UNIQUE (t_NombreVista),
    CONSTRAINT CK_Cat016Con_Estatus
        CHECK (i_Cve_Estatus IN (0, 1)),
    CONSTRAINT CK_Cat016Con_Estado
        CHECK (i_Cve_Estado  IN (0, 1))
 
) ON [PRIMARY]
GO
 
-- Índices
CREATE NONCLUSTERED INDEX IX_Cat016Con_Nombre
    ON [dbo].[Cat016ConsultasReporteador] (t_Nombre)
    WHERE i_Cve_Estado = 1
GO
CREATE NONCLUSTERED INDEX IX_Cat016Con_Estatus
    ON [dbo].[Cat016ConsultasReporteador] (i_Cve_Estatus)
GO
CREATE NONCLUSTERED INDEX IX_Cat016Con_Estado
    ON [dbo].[Cat016ConsultasReporteador] (i_Cve_Estado)
GO
 
-- Descripción de tabla
EXEC sp_addextendedproperty
    @name  = N'MS_Description',
    @value = N'Catálogo de consultas para reportes. Solo DBA/Dev registran aquí. ' +
             N'La vista (t_NombreVista) es obligatoria y maneja los JOINs a cualquier BD. ' +
             N'El SP (t_NombreSP) es opcional: si existe el motor lo ejecuta, si no consulta la vista directo.',
    @level0type = N'SCHEMA', @level0name = N'dbo',
    @level1type = N'TABLE',  @level1name = N'Cat016ConsultasReporteador';
GO
 
-- Descripciones de columnas
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Clave única de la consulta',
    @level0type = N'SCHEMA', @level0name = N'dbo',
    @level1type = N'TABLE',  @level1name = N'Cat016ConsultasReporteador',
    @level2type = N'COLUMN', @level2name = N'i_Cve_Consulta';
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Nombre descriptivo que se muestra al usuario en el ComboBox del form de plantillas',
    @level0type = N'SCHEMA', @level0name = N'dbo',
    @level1type = N'TABLE',  @level1name = N'Cat016ConsultasReporteador',
    @level2type = N'COLUMN', @level2name = N't_Nombre';
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Descripción de qué datos devuelve esta consulta',
    @level0type = N'SCHEMA', @level0name = N'dbo',
    @level1type = N'TABLE',  @level1name = N'Cat016ConsultasReporteador',
    @level2type = N'COLUMN', @level2name = N't_Descripcion';
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Nombre de la vista base (OBLIGATORIA). Ejemplo: VT016OperacionesBase. ' +
             N'La vista maneja JOINs a otras BDs internamente (SysExpert, Aduana, etc.)',
    @level0type = N'SCHEMA', @level0name = N'dbo',
    @level1type = N'TABLE',  @level1name = N'Cat016ConsultasReporteador',
    @level2type = N'COLUMN', @level2name = N't_NombreVista';
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Nombre del SP (OPCIONAL). Si existe: el motor lo ejecuta con los parámetros del JSON. ' +
             N'Si es NULL: el motor consulta directamente la vista. Ejemplo: spVT016OperacionesDetalle',
    @level0type = N'SCHEMA', @level0name = N'dbo',
    @level1type = N'TABLE',  @level1name = N'Cat016ConsultasReporteador',
    @level2type = N'COLUMN', @level2name = N't_NombreSP';
GO
 
-- ===================================================================
-- PASO 2: MODIFICAR Cat016PlantillasReporteador
--   - Se elimina t_Consulta (queries en bruto, ya no se usan)
--   - Se agrega i_Cve_Consulta como FK a Cat016ConsultasReporteador
-- ===================================================================
 
-- 2a. Eliminar t_Consulta
ALTER TABLE [dbo].[Cat016PlantillasReporteador]
    DROP COLUMN t_Consulta;
GO
 
-- 2b. Agregar FK
ALTER TABLE [dbo].[Cat016PlantillasReporteador]
    ADD i_Cve_Consulta INT NOT NULL CONSTRAINT DF_Cat016Pla_Consulta DEFAULT (0);
GO
 
ALTER TABLE [dbo].[Cat016PlantillasReporteador]
    ADD CONSTRAINT FK_Cat016Pla_Consulta
        FOREIGN KEY (i_Cve_Consulta)
        REFERENCES [dbo].[Cat016ConsultasReporteador] (i_Cve_Consulta);
GO
 
-- 2c. Índice para el FK
CREATE NONCLUSTERED INDEX IX_Cat016Pla_Consulta
    ON [dbo].[Cat016PlantillasReporteador] (i_Cve_Consulta);
GO
 
-- 2d. Descripción
EXEC sp_addextendedproperty
    @name  = N'MS_Description',
    @value = N'FK a Cat016ConsultasReporteador. Indica qué vista/SP usar para este reporte.',
    @level0type = N'SCHEMA', @level0name = N'dbo',
    @level1type = N'TABLE',  @level1name = N'Cat016PlantillasReporteador',
    @level2type = N'COLUMN', @level2name = N'i_Cve_Consulta';
GO
 
-- ===================================================================
-- PASO 3: RECREAR VISTAS AFECTADAS
-- ===================================================================
 
-- 3a. Vt016Plantillas
IF OBJECT_ID('dbo.Vt016Plantillas', 'V') IS NOT NULL
    DROP VIEW dbo.Vt016Plantillas;
GO
 
CREATE VIEW [dbo].[Vt016Plantillas] AS
SELECT
    p.i_Cve_Plantilla,
    p.i_Cve_Consulta,
    c.t_Nombre          AS t_NombreConsulta,
    c.t_NombreVista,
    c.t_NombreSP,       -- NULL si la consulta no tiene SP
    p.t_Nombre,
    p.t_Descripcion,
    p.t_RutaPlantilla,
    p.t_NombreCliente,
    p.t_ColumnasConfig,
    p.t_ParametrosConfig,
    p.t_FormatoSalida,
    p.f_FechaRegistro,
    p.t_UsuarioRegistro,
    CASE WHEN p.i_Cve_Estatus = 1 THEN 'Activo' ELSE 'Inactivo' END AS t_Estatus,
    CASE WHEN p.i_Cve_Estado  = 1 THEN 'Activo' ELSE 'Inactivo' END AS t_Estado
FROM [dbo].[Cat016PlantillasReporteador]  p
INNER JOIN [dbo].[Cat016ConsultasReporteador] c
    ON c.i_Cve_Consulta = p.i_Cve_Consulta
WHERE p.i_Cve_Estado = 1
GO
 
-- 3b. Ve016IUPlantillas
--     t_Consulta (RichTextBox) se reemplaza por i_Cve_Consulta (ComboBox)
--     Se agregan t_NombreConsulta, t_NombreVista y t_NombreSP como solo lectura
IF OBJECT_ID('dbo.Ve016IUPlantillas', 'V') IS NOT NULL
    DROP VIEW dbo.Ve016IUPlantillas;
GO
 
CREATE VIEW [dbo].[Ve016IUPlantillas] AS
SELECT * FROM (
    SELECT 'i_Cve_Plantilla'  AS Nombre, 1 AS Llave, 11  AS Longitud, 0 AS TipoDato, 1 AS Visible, 'Clave Plantilla' AS NombreColumna, 0 AS PuedeInsertar, 0 AS PuedeModificar, '' AS ValorDefault, '1' AS TipoFiltro
    UNION ALL
    SELECT 'i_Cve_Consulta'  AS Nombre,  0 AS Llave, 11 AS Longitud, 0 AS TipoDato, 1 AS Visible, 'Consulta/Reporte' AS NombreColumna, 1 AS PuedeInsertar, 1 AS PuedeModificar, '' AS ValorDefault, '2' AS TipoFiltro
    UNION ALL
    SELECT 't_NombreConsulta' AS Nombre, 0 AS Llave, 200 AS Longitud, 1 AS TipoDato, 1 AS Visible, 'Nombre Consulta' AS NombreColumna, 0 AS PuedeInsertar, 0 AS PuedeModificar, '' AS ValorDefault, '2' AS TipoFiltro
    UNION ALL
    SELECT 't_NombreVista'   AS Nombre,  0 AS Llave, 200 AS Longitud, 1 AS TipoDato, 1 AS Visible, 'Vista Base' AS NombreColumna, 0 AS PuedeInsertar, 0 AS PuedeModificar, '' AS ValorDefault, '2' AS TipoFiltro
    UNION ALL
    SELECT 't_NombreSP'      AS Nombre,  0 AS Llave, 200 AS Longitud, 1 AS TipoDato, 1 AS Visible, 'Stored Procedure' AS NombreColumna, 0 AS PuedeInsertar, 0 AS PuedeModificar, '' AS ValorDefault, '2' AS TipoFiltro
    UNION ALL
    SELECT 't_Nombre'        AS Nombre,  0 AS Llave, 200 AS Longitud, 1 AS TipoDato, 1 AS Visible, 'Nombre Plantilla' AS NombreColumna, 1 AS PuedeInsertar, 1 AS PuedeModificar, '' AS ValorDefault, '2' AS TipoFiltro
    UNION ALL
    SELECT 't_Descripcion'   AS Nombre,  0 AS Llave, 1000 AS Longitud, 1 AS TipoDato, 1 AS Visible, 'Descripción Plantilla' AS NombreColumna, 1 AS PuedeInsertar, 1 AS PuedeModificar, '' AS ValorDefault, '0' AS TipoFiltro
    UNION ALL
    SELECT 't_RutaPlantilla' AS Nombre,  0 AS Llave, 500 AS Longitud, 1 AS TipoDato, 1 AS Visible, 'Ruta Plantilla' AS NombreColumna, 1 AS PuedeInsertar, 1 AS PuedeModificar, '' AS ValorDefault, '2' AS TipoFiltro
    UNION ALL
    SELECT 't_NombreCliente' AS Nombre,  0 AS Llave, 100 AS Longitud, 1 AS TipoDato, 1 AS Visible, 'Cliente/RFC' AS NombreColumna, 1 AS PuedeInsertar, 1 AS PuedeModificar, '' AS ValorDefault, '2' AS TipoFiltro
    UNION ALL
    SELECT 't_ColumnasConfig' AS Nombre, 0 AS Llave, 2147483647 AS Longitud, 1 AS TipoDato, 1 AS Visible, 'Config. Columnas (JSON)' AS NombreColumna, 1 AS PuedeInsertar, 1 AS PuedeModificar, '' AS ValorDefault, '0' AS TipoFiltro
    UNION ALL
    SELECT 't_ParametrosConfig' AS Nombre, 0 AS Llave, 2147483647 AS Longitud, 1 AS TipoDato, 1 AS Visible, 'Config. Parámetros (JSON)' AS NombreColumna, 1 AS PuedeInsertar, 1 AS PuedeModificar, '' AS ValorDefault, '0' AS TipoFiltro
    UNION ALL
    SELECT 't_FormatoSalida' AS Nombre,  0 AS Llave, 10 AS Longitud, 1 AS TipoDato, 1 AS Visible, 'Formato Salida' AS NombreColumna, 1 AS PuedeInsertar, 1 AS PuedeModificar, 'XLSX' AS ValorDefault, '2' AS TipoFiltro
    UNION ALL
    SELECT 'f_FechaRegistro' AS Nombre,  0 AS Llave, 23 AS Longitud, 4 AS TipoDato, 1 AS Visible, 'Fecha Registro' AS NombreColumna, 0 AS PuedeInsertar, 0 AS PuedeModificar, '' AS ValorDefault, '2' AS TipoFiltro
    UNION ALL
    SELECT 't_UsuarioRegistro' AS Nombre, 0 AS Llave, 50 AS Longitud, 1 AS TipoDato, 1 AS Visible, 'Usuario Registro' AS NombreColumna, 0 AS PuedeInsertar, 0 AS PuedeModificar, '' AS ValorDefault, '2' AS TipoFiltro
    UNION ALL
    SELECT 't_Estatus'      AS Nombre,   0 AS Llave, 10 AS Longitud, 1 AS TipoDato, 1 AS Visible, 'Estatus' AS NombreColumna, 1 AS PuedeInsertar, 1 AS PuedeModificar, '' AS ValorDefault, '2' AS TipoFiltro
    UNION ALL
    SELECT 't_Estado'       AS Nombre,   0 AS Llave, 10 AS Longitud, 1 AS TipoDato, 1 AS Visible, 'Estado' AS NombreColumna, 1 AS PuedeInsertar, 1 AS PuedeModificar, '' AS ValorDefault, '2' AS TipoFiltro
) AS VE
GO
 
-- 3c. Vig016ProgramacionesPendientes
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
    pl.i_Cve_Plantilla,
    pl.t_Nombre          AS t_NombrePlantilla,
    pl.t_ColumnasConfig,
    pl.t_ParametrosConfig,
    pl.t_RutaPlantilla,
    -- Datos de la consulta (reemplazan a t_Consulta en bruto)
    con.i_Cve_Consulta,
    con.t_NombreVista,
    con.t_NombreSP       -- NULL si no tiene SP; el motor lo detecta
FROM [dbo].[Enc016ProgramacionReporteador]  prog
INNER JOIN [dbo].[Cat016PlantillasReporteador]   pl  ON pl.i_Cve_Plantilla  = prog.i_Cve_Plantilla
INNER JOIN [dbo].[Cat016ConsultasReporteador]    con ON con.i_Cve_Consulta  = pl.i_Cve_Consulta
WHERE prog.i_Cve_Estado = 1
  AND pl.i_Cve_Estado   = 1
  AND con.i_Cve_Estado  = 1
  AND prog.f_ProximaEjecucion IS NOT NULL
  AND prog.f_ProximaEjecucion <= GETDATE()
GO
 
-- ===================================================================
-- PASO 4: NUEVA VISTA Vt016ConsultasDisponibles
--   Para el ComboBox del form de plantillas.
--   No expone datos internos innecesarios para la UI.
-- ===================================================================
CREATE VIEW [dbo].[Vt016ConsultasDisponibles] AS
SELECT
    i_Cve_Consulta,
    t_Nombre,
    t_Descripcion,
    t_NombreVista,
    -- Mostramos si tiene SP para que el usuario sepa qué esperar
    CASE WHEN t_NombreSP IS NULL THEN 'No' ELSE 'Sí' END AS t_TieneSP,
    t_NombreSP
FROM [dbo].[Cat016ConsultasReporteador]
WHERE i_Cve_Estado  = 1
  AND i_Cve_Estatus = 1
GO
 
-- ===================================================================
-- PASO 5: ACTUALIZAR SP016ProcesarColaReportes
--   Devuelve t_NombreVista y t_NombreSP en lugar de t_Consulta.
--   El motor en C# decide:
--     - Si t_NombreSP no es NULL → ejecuta el SP con parámetros
--     - Si t_NombreSP es NULL    → ejecuta SELECT * FROM [vista]
-- ===================================================================
IF OBJECT_ID('dbo.SP016ProcesarColaReportes', 'P') IS NOT NULL
    DROP PROCEDURE dbo.SP016ProcesarColaReportes;
GO
 
CREATE PROCEDURE [dbo].[SP016ProcesarColaReportes]
    @UsuarioSistema VARCHAR(50) = 'MOTOR_REPORTEADOR'
AS
/*
===================================================================
Autor: Residencia Profesional
Fecha: Marzo 2026
Descripción: Identifica reportes programados pendientes, aparta
la ejecución en bitácora y calcula la siguiente fecha.
 
v3 - Cambios:
- Se elimina t_Consulta (query en bruto)
- Se devuelven t_NombreVista y t_NombreSP
- El motor decide si ejecutar SP o vista directamente según
  si t_NombreSP es NULL o no
===================================================================
*/
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
 
    CREATE TABLE #Pendientes (
        i_Cve_Programacion INT,
        i_Cve_Plantilla    INT,
        f_ProximaEjecucion DATETIME
    );
 
    BEGIN TRY
        BEGIN TRANSACTION;
 
            -- 1. Identificar y bloquear registros pendientes
            INSERT INTO #Pendientes (i_Cve_Programacion, i_Cve_Plantilla, f_ProximaEjecucion)
            SELECT i_Cve_Programacion, i_Cve_Plantilla, f_ProximaEjecucion
            FROM [dbo].[Vig016ProgramacionesPendientes] WITH (UPDLOCK, READPAST);
 
            -- 2. Registrar inicio en bitácora
            INSERT INTO [dbo].[Bit016GeneracionReporteador]
                (i_Cve_Programacion, f_FechaInicio, t_Proceso,
                 t_ParametrosUsados, t_UsuarioRegistro, i_Cve_Estatus, i_Cve_Estado)
            SELECT
                p.i_Cve_Programacion,
                GETDATE(),
                'PROCESANDO',
                prog.t_Parametros,
                @UsuarioSistema,
                1,
                1
            FROM #Pendientes p
            INNER JOIN [dbo].[Enc016ProgramacionReporteador] prog
                ON p.i_Cve_Programacion = prog.i_Cve_Programacion;
 
            -- 3. Calcular siguiente ejecución según frecuencia
            UPDATE prog
            SET
                prog.f_UltimaEjecucion  = tmp.f_ProximaEjecucion,
                prog.f_ProximaEjecucion = CASE
                    WHEN prog.t_Frecuencia = 'D' THEN DATEADD(DAY,  1,  tmp.f_ProximaEjecucion)
                    WHEN prog.t_Frecuencia = 'S' THEN DATEADD(DAY,  7,  tmp.f_ProximaEjecucion)
                    WHEN prog.t_Frecuencia = 'Q' THEN DATEADD(DAY,  15, tmp.f_ProximaEjecucion)
                    WHEN prog.t_Frecuencia = 'M' THEN
                        CASE
                            WHEN prog.i_DiaMes IS NOT NULL THEN
                                DATEADD(DAY,
                                    prog.i_DiaMes - DAY(DATEADD(MONTH, 1, tmp.f_ProximaEjecucion)),
                                    DATEADD(MONTH, 1, tmp.f_ProximaEjecucion))
                            ELSE DATEADD(MONTH, 1, tmp.f_ProximaEjecucion)
                        END
                    WHEN prog.t_Frecuencia = 'U' THEN NULL
                    ELSE prog.f_ProximaEjecucion
                END,
                prog.i_Cve_Estado      = CASE
                    WHEN prog.t_Frecuencia = 'U' THEN 0
                    ELSE prog.i_Cve_Estado
                END,
                prog.t_UsuarioRegistro = @UsuarioSistema
            FROM [dbo].[Enc016ProgramacionReporteador] prog
            INNER JOIN #Pendientes tmp
                ON prog.i_Cve_Programacion = tmp.i_Cve_Programacion;
 
        COMMIT TRANSACTION;
 
        -- 4. Devolver datos al motor
        --    t_NombreSP puede ser NULL → el motor ejecuta vista directamente
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
            -- Origen de datos simplificado
            con.t_NombreVista,
            con.t_NombreSP      -- NULL = ejecutar vista directo; valor = ejecutar SP
        FROM #Pendientes tmp
        INNER JOIN [dbo].[Cat016PlantillasReporteador]   pl   ON pl.i_Cve_Plantilla    = tmp.i_Cve_Plantilla
        INNER JOIN [dbo].[Cat016ConsultasReporteador]    con  ON con.i_Cve_Consulta    = pl.i_Cve_Consulta
        INNER JOIN [dbo].[Enc016ProgramacionReporteador] prog ON prog.i_Cve_Programacion = tmp.i_Cve_Programacion
        INNER JOIN [dbo].[Bit016GeneracionReporteador]   gen  ON gen.i_Cve_Programacion  = tmp.i_Cve_Programacion
        WHERE gen.t_Proceso   = 'PROCESANDO'
          AND gen.f_FechaFin IS NULL
          AND gen.i_Cve_Generacion = (
              SELECT MAX(g2.i_Cve_Generacion)
              FROM [dbo].[Bit016GeneracionReporteador] g2
              WHERE g2.i_Cve_Programacion = gen.i_Cve_Programacion
          );
 
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        DECLARE @ErrorMessage  NVARCHAR(4000) = ERROR_MESSAGE();
        DECLARE @ErrorSeverity INT            = ERROR_SEVERITY();
        DECLARE @ErrorState    INT            = ERROR_STATE();
        RAISERROR(@ErrorMessage, @ErrorSeverity, @ErrorState);
    END CATCH
 
    DROP TABLE #Pendientes;
END
GO
 
GRANT EXECUTE ON [dbo].[SP016ProcesarColaReportes] TO [public];
GO
 
EXEC sp_addextendedproperty
    @name  = N'MS_Description',
    @value = N'v3 - Procesa cola de reportes pendientes. ' +
             N'Devuelve t_NombreVista y t_NombreSP. ' +
             N'Si t_NombreSP es NULL el motor ejecuta la vista directo; si tiene valor ejecuta el SP.',
    @level0type = N'SCHEMA', @level0name = N'dbo',
    @level1type = N'PROCEDURE', @level1name = N'SP016ProcesarColaReportes';
GO
 
-- ===================================================================
-- PASO 6: DATOS DE EJEMPLO
--   Muestra cómo registra el DBA una consulta de cada tipo.
-- ===================================================================
INSERT INTO [dbo].[Cat016ConsultasReporteador]
    (t_Nombre, t_Descripcion, t_NombreVista, t_NombreSP, t_UsuarioRegistro)
VALUES
(
    'Operaciones por RFC y Fecha',
    'Pedimentos de importación/exportación filtrados por RFC y rango de fechas. ' +
    'La vista hace JOIN a SysExpert internamente.',
    'VT016OperacionesBase',
    'spVT016OperacionesDetalle',  -- Tiene SP: aplica filtros y cálculos
    'SISTEMA'
),
(
    'Catálogo de Clientes Activos',
    'Lista de clientes activos. Consulta simple, no requiere SP.',
    'VT016ClientesActivos',
    NULL,   -- Sin SP: el motor consulta la vista directamente
    'SISTEMA'
);
GO
 
-- ===================================================================
-- PASO 7: VERIFICACIÓN DE INTEGRIDAD
-- ===================================================================
PRINT 'Verificando llaves foráneas...'
GO
IF EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = 'FK_Cat016Pla_Consulta')
    PRINT 'FK_Cat016Pla_Consulta: OK'
ELSE
    PRINT 'FK_Cat016Pla_Consulta: NO ENCONTRADA'
GO
IF EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = 'FK_Enc016_Plantilla')
    PRINT 'FK_Enc016_Plantilla: OK'
ELSE
    PRINT 'FK_Enc016_Plantilla: NO ENCONTRADA'
GO
IF EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = 'FK_Enc016_PlantillaProgramacion')
    PRINT 'FK_Enc016_PlantillaProgramacion: OK'
ELSE
    PRINT 'FK_Enc016_PlantillaProgramacion: NO ENCONTRADA'
GO
IF EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = 'FK_Bit016_Programacion')
    PRINT 'FK_Bit016_Programacion: OK'
ELSE
    PRINT 'FK_Bit016_Programacion: NO ENCONTRADA'
GO
 
-- ===================================================================
-- RESUMEN
-- ===================================================================
PRINT '========================================================='
PRINT 'MÓDULO REPORTEADOR KROM - FAMILIA 016 (ESQUEMA v3)'
PRINT '========================================================='
PRINT ''
PRINT 'Tablas nuevas:'
PRINT '  Cat016ConsultasReporteador'
PRINT '    - t_NombreVista  (OBLIGATORIA)'
PRINT '    - t_NombreSP     (OPCIONAL - NULL = motor usa vista directo)'
PRINT ''
PRINT 'Tablas modificadas:'
PRINT '  Cat016PlantillasReporteador'
PRINT '    - ELIMINADO: t_Consulta'
PRINT '    - AGREGADO:  i_Cve_Consulta (FK)'
PRINT ''
PRINT 'Vistas recreadas:'
PRINT '  Vt016Plantillas'
PRINT '  Ve016IUPlantillas      (ComboBox en lugar de RichTextBox)'
PRINT '  Vig016ProgramacionesPendientes'
PRINT ''
PRINT 'Vistas nuevas:'
PRINT '  Vt016ConsultasDisponibles  (para ComboBox en frm Plantillas)'
PRINT ''
PRINT 'SPs actualizados:'
PRINT '  SP016ProcesarColaReportes  (devuelve t_NombreVista + t_NombreSP)'
PRINT ''
PRINT 'PENDIENTE (cambios en código VB.NET):'
PRINT '  - ReportePendiente.vb: quitar t_Consulta, agregar t_NombreVista + t_NombreSP'
PRINT '  - ServicioBaseDeDatos.vb: lógica dual (SP vs vista directa)'
PRINT '  - frm000PlantillasConfColParam.vb: ComboBox en lugar de RichTextBox SQL'
PRINT '========================================================='
GO