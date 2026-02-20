USE [SysExpert];
GO

IF OBJECT_ID('dbo.Pr016ProcesarColaReportes', 'P') IS NOT NULL
    DROP PROCEDURE dbo.Pr016ProcesarColaReportes;
GO

CREATE PROCEDURE [dbo].[Pr016ProcesarColaReportes]
    @UsuarioSistema VARCHAR(50) = 'MOTOR_REPORTEADOR'
AS
/*
===================================================================
Autor: Residencia Profesional
Fecha: Febrero 2026
Descripción: Identifica reportes programados pendientes, aparta 
la ejecución en bitácora y calcula la siguiente fecha.

Adaptación al nuevo esquema (Feb 2026):
- Se eliminó referencia a tabla Det016DestinatariosReporteador
- Se ajustó Cat016PlantillasReporteador: t_Columnas → t_ParametrosConfig
- Se agregó t_RutaPlantilla y t_NombreBaseDatos al SELECT final
===================================================================
*/
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    -- Tabla temporal para trabajar con los registros identificados
    CREATE TABLE #Pendientes (
        i_Cve_Programacion INT,
        i_Cve_Plantilla INT,
        f_ProximaEjecucion DATETIME
    );

    BEGIN TRY
        BEGIN TRANSACTION;

            -- 1. Identificar y bloquear registros para que ninguna otra instancia los tome
            INSERT INTO #Pendientes (i_Cve_Programacion, i_Cve_Plantilla, f_ProximaEjecucion)
            SELECT i_Cve_Programacion, i_Cve_Plantilla, f_ProximaEjecucion
            FROM [dbo].[Vig016ProgramacionesPendientes] WITH (UPDLOCK, READPAST);

            -- 2. Registrar el inicio del trabajo en la Bitácora
            -- Insertamos y obtenemos el ID de generación para que la App sepa qué registro actualizar al final
            INSERT INTO [dbo].[Bit016GeneracionReporteador] 
                (i_Cve_Programacion, f_FechaInicio, t_Estatus, t_ParametrosUsados, t_UsuarioRegistro)
            SELECT 
                p.i_Cve_Programacion, 
                GETDATE(), 
                'PROCESANDO',
                prog.t_Parametros,
                @UsuarioSistema
            FROM #Pendientes p
            INNER JOIN [dbo].[Enc016ProgramacionReporteador] prog ON p.i_Cve_Programacion = prog.i_Cve_Programacion;

            -- 3. Calcular la siguiente ejecución según la frecuencia
            UPDATE p
            SET 
                p.f_UltimaEjecucion = tmp.f_ProximaEjecucion,
                p.f_ProximaEjecucion = CASE 
                    WHEN p.t_Frecuencia = 'D' THEN DATEADD(DAY, 1, tmp.f_ProximaEjecucion)
                    WHEN p.t_Frecuencia = 'S' THEN DATEADD(WEEK, 1, tmp.f_ProximaEjecucion)
                    WHEN p.t_Frecuencia = 'M' THEN DATEADD(MONTH, 1, tmp.f_ProximaEjecucion)
                    WHEN p.t_Frecuencia = 'U' THEN NULL -- Única vez
                    ELSE p.f_ProximaEjecucion 
                END,
                p.i_Cve_Estado = CASE WHEN p.t_Frecuencia = 'U' THEN 0 ELSE p.i_Cve_Estado END,
                p.t_UsuarioRegistro = @UsuarioSistema
            FROM [dbo].[Enc016ProgramacionReporteador] p
            INNER JOIN #Pendientes tmp ON p.i_Cve_Programacion = tmp.i_Cve_Programacion;

        COMMIT TRANSACTION;

        -- 4. Retornar el set de datos para la aplicación de C#
        -- Aquí enviamos todo lo necesario para que el código ejecute el reporte
        SELECT 
            tmp.i_Cve_Programacion,
            gen.i_Cve_Generacion,
            pl.t_Nombre AS NombreReporte,
            pl.t_RutaPlantilla,
            pl.t_NombreBaseDatos,
            pl.t_Consulta,
            pl.t_FormatoSalida,
            pl.t_ParametrosConfig,  -- Antes era t_Columnas, ahora incluye configuración completa
            prog.t_Parametros       -- Parámetros fijos de la programación
        FROM #Pendientes tmp
        INNER JOIN [dbo].[Cat016PlantillasReporteador] pl ON tmp.i_Cve_Plantilla = pl.i_Cve_Plantilla
        INNER JOIN [dbo].[Enc016ProgramacionReporteador] prog ON tmp.i_Cve_Programacion = prog.i_Cve_Programacion
        INNER JOIN [dbo].[Bit016GeneracionReporteador] gen ON gen.i_Cve_Programacion = tmp.i_Cve_Programacion
        WHERE gen.t_Estatus = 'PROCESANDO' 
        AND gen.f_FechaFin IS NULL;

    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;

        DECLARE @ErrorMessage NVARCHAR(4000) = ERROR_MESSAGE();
        DECLARE @ErrorSeverity INT = ERROR_SEVERITY();
        DECLARE @ErrorState INT = ERROR_STATE();
        
        -- Registrar error en bitácora (opcional, se puede implementar tabla de errores)
        RAISERROR(@ErrorMessage, @ErrorSeverity, @ErrorState);
    END CATCH

    DROP TABLE #Pendientes;
END
GO

-- ===================================================================
-- Permisos de ejecución
-- ===================================================================
GRANT EXECUTE ON [dbo].[Pr016ProcesarColaReportes] TO [usuario_aplicacion] -- Ajustar según sea necesario
GO

-- ===================================================================
-- Descripción del procedimiento
-- ===================================================================
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'Procesa la cola de reportes pendientes: identifica programaciones listas, las aparta en bitácora y calcula próxima ejecución',
    @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'Pr016ProcesarColaReportes';
GO