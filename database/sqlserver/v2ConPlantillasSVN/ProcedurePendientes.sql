USE [Solium];
GO

IF OBJECT_ID('dbo.SP016ProcesarColaReportes', 'P') IS NOT NULL
    DROP PROCEDURE dbo.SP016ProcesarColaReportes;
GO

CREATE PROCEDURE [dbo].[SP016ProcesarColaReportes]
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
                (i_Cve_Programacion, f_FechaInicio, t_Proceso, t_ParametrosUsados, t_UsuarioRegistro, i_Cve_Estatus, i_Cve_Estado)
            SELECT 
                p.i_Cve_Programacion, 
                GETDATE(), 
                'PROCESANDO',
                prog.t_Parametros,
                @UsuarioSistema,
                1, -- i_Cve_Estatus Activo
                1  -- i_Cve_Estado Activo
            FROM #Pendientes p
            INNER JOIN [dbo].[Enc016ProgramacionReporteador] prog ON p.i_Cve_Programacion = prog.i_Cve_Programacion;

            -- 3. Calcular la siguiente ejecución según la frecuencia
            UPDATE prog
            SET 
                prog.f_UltimaEjecucion = tmp.f_ProximaEjecucion,
                prog.f_ProximaEjecucion = CASE 
                    -- Diaria: más 1 día
                    WHEN prog.t_Frecuencia = 'D' THEN DATEADD(DAY, 1, tmp.f_ProximaEjecucion)
                    -- Semanal: agregamos 7 dias
                    WHEN prog.t_Frecuencia = 'S' THEN DATEADD(DAY, 7, tmp.f_ProximaEjecucion)
                    --Mensual sumamos un mes y ajustamos el dia correcto
                    WHEN prog.t_Frecuencia = 'M' THEN 
                        CASE
                            --si tiene dia especifico mantenerlo
                            WHEN prog.i_DiaMes IS NOT NULL THEN
                                DATEADD(DAY, prog.i_DiaMes - DAY(DATEADD(MONTH, 1, tmp.f_ProximaEjecucion)), DATEADD(MONTH, 1, tmp.f_ProximaEjecucion))
                            --SI NO mismo día del mes siguiente
                            ELSE DATEADD(MONTH, 1, tmp.f_ProximaEjecucion)
                        END

                    --Quincenal: agregamos 15 Días
                    WHEN prog.t_Frecuencia = 'Q' THEN DATEADD(DAY, 15, tmp.f_ProximaEjecucion)
                    --unica vez null ya que no hay proxima ejecucion
                    WHEN prog.t_Frecuencia = 'U' THEN NULL 
                    -- otros casos solo mantener la programacion
                    ELSE prog.f_ProximaEjecucion 
                END,
                --para la frecuencia unica, desactivasmos la programacion cambiando el estado
                prog.i_Cve_Estado = CASE WHEN prog.t_Frecuencia = 'U' THEN 0 ELSE prog.i_Cve_Estado END,
                prog.t_UsuarioRegistro = @UsuarioSistema
            FROM [dbo].[Enc016ProgramacionReporteador] prog
            INNER JOIN #Pendientes tmp ON prog.i_Cve_Programacion = tmp.i_Cve_Programacion;

        COMMIT TRANSACTION;

        -- 4. Retornar el set de datos para la aplicación de C#
        -- Aquí enviamos todo lo necesario para que el código ejecute el reporte
        SELECT 
            tmp.i_Cve_Programacion,
            gen.i_Cve_Generacion,
            pl.t_Nombre AS NombreReporte,
            pl.t_RutaPlantilla,
            pl.t_Consulta,
            pl.t_FormatoSalida,
            pl.t_ParametrosConfig,  -- Antes era t_Columnas, ahora incluye configuración completa
            pl.t_ColumnasConfig,
            prog.t_Parametros,       -- Parámetros fijos de la programación
            prog.t_DiasSemana,
            prog.t_DiaMes
        FROM #Pendientes tmp
        INNER JOIN [dbo].[Cat016PlantillasReporteador] pl ON tmp.i_Cve_Plantilla = pl.i_Cve_Plantilla
        INNER JOIN [dbo].[Enc016ProgramacionReporteador] prog ON tmp.i_Cve_Programacion = prog.i_Cve_Programacion
        INNER JOIN [dbo].[Bit016GeneracionReporteador] gen ON gen.i_Cve_Programacion = tmp.i_Cve_Programacion
        WHERE gen.t_Proceso = 'PROCESANDO' 
        AND gen.f_FechaFin IS NULL
        AND gen.i_Cve_Generacion = (
            SELECT MAX(g2.i_Cve_Generacion)
            FROM Bit016GeneracionReporteador g2
            WHERE g2.i_Cve_Programacion = gen.i_Cve_Programacion
        );

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
GRANT EXECUTE ON [dbo].[SP016ProcesarColaReportes] TO [public]; -- Ajustar según sea necesario
GO

-- ===================================================================
-- Descripción del procedimiento
-- ===================================================================
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'Procesa la cola de reportes pendientes: identifica programaciones listas, las aparta en bitácora y calcula próxima ejecución',
    @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP016ProcesarColaReportes';
GO