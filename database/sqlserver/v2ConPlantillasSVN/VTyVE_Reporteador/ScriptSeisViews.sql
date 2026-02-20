USE [SysExpert];
GO

-- ===================================================================
-- MÓDULO 1: ADMINISTRACIÓN DE PLANTILLAS
-- ===================================================================
PRINT 'Creando vistas para Módulo de Plantillas...'
GO

-- Vista de Trabajo: Plantillas
IF EXISTS (SELECT * FROM sys.views WHERE name = 'Vt016Plantillas')
    DROP VIEW [dbo].[Vt016Plantillas]
GO

CREATE VIEW [dbo].[Vt016Plantillas] AS
SELECT 
    i_Cve_Plantilla,
    t_Nombre,
    t_RutaPlantilla,
    t_NombreBaseDatos,
    t_Consulta,
    t_ParametrosConfig,
    t_FormatoSalida,
    f_FechaRegistro,
    t_UsuarioRegistro,
    CASE WHEN i_Cve_Status = 1 THEN 'Activo' ELSE 'Inactivo' END AS t_Status,
    CASE WHEN i_Cve_Estado = 1 THEN 'Activo' ELSE 'Inactivo' END AS t_Estado
FROM [dbo].[Cat016PlantillasReporteador]
WHERE i_Cve_Estado = 1
GO

EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'Vista de Trabajo: Catálogo de plantillas de reportes',
    @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'Vt016Plantillas';
GO

-- Vista de Entorno: Plantillas
IF EXISTS (SELECT * FROM sys.views WHERE name = 'Ve016Plantillas')
    DROP VIEW [dbo].[Ve016Plantillas]
GO

CREATE VIEW [dbo].[Ve016Plantillas] AS
SELECT * FROM (
    SELECT 'i_Cve_Plantilla' as Nombre, 1 AS Llave, 11 AS Longitud, 0 AS TipoDato, 1 AS Visible, 'Clave Plantilla' AS NombreColumna, 0 AS PuedeInsertar, 0 AS PuedeModificar, '' AS ValorDefault, '2' AS TipoFiltro
    UNION ALL SELECT 't_Nombre', 0, 200, 1, 1, 'Nombre Plantilla', 1, 1, '', '1'
    UNION ALL SELECT 't_RutaPlantilla', 0, 500, 1, 1, 'Ruta Plantilla', 1, 1, '', '1'
    UNION ALL SELECT 't_NombreBaseDatos', 0, 100, 1, 1, 'Base de Datos', 1, 1, '', '1'
    UNION ALL SELECT 't_Consulta', 0, 2147483647, 1, 1, 'Consulta SQL', 1, 1, '', '1'
    UNION ALL SELECT 't_ParametrosConfig', 0, 2147483647, 1, 1, 'Config. Parámetros (JSON)', 1, 1, '', '1'
    UNION ALL SELECT 't_FormatoSalida', 0, 10, 1, 1, 'Formato Salida', 1, 1, 'XLSX', '1'
    UNION ALL SELECT 'f_FechaRegistro', 0, 23, 4, 1, 'Fecha Registro', 0, 0, '', '4'
    UNION ALL SELECT 't_UsuarioRegistro', 0, 50, 1, 1, 'Usuario Registro', 0, 0, '', '1'
    UNION ALL SELECT 't_Status', 0, 10, 1, 1, 'Status', 0, 0, '', '1'
    UNION ALL SELECT 't_Estado', 0, 10, 1, 1, 'Estado', 0, 0, '', '1'
) AS VE
GO

EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'Vista de Entorno: Configuración UI para catálogo de plantillas',
    @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'Ve016Plantillas';
GO

-- ===================================================================
-- MÓDULO 2: PROGRAMACIÓN DE REPORTES
-- ===================================================================
PRINT 'Creando vistas para Módulo de Programación...'
GO

-- Vista de Trabajo: Programaciones
IF EXISTS (SELECT * FROM sys.views WHERE name = 'Vt016Programacion')
    DROP VIEW [dbo].[Vt016Programacion]
GO

CREATE VIEW [dbo].[Vt016Programacion] AS
SELECT 
    p.i_Cve_Programacion,
    p.i_Cve_Plantilla,
    pl.t_Nombre AS t_NombrePlantilla,
    p.t_Nombre AS t_NombreProgramacion,
    CASE p.t_Frecuencia 
        WHEN 'U' THEN 'Única'
        WHEN 'D' THEN 'Diaria'
        WHEN 'S' THEN 'Semanal'
        WHEN 'M' THEN 'Mensual'
    END AS t_FrecuenciaDesc,
    p.t_DiasSemana,
    p.i_DiaMes,
    p.t_Hora,
    p.t_Parametros,
    p.f_ProximaEjecucion,
    p.f_UltimaEjecucion,
    p.f_FechaRegistro,
    p.t_UsuarioRegistro,
    CASE WHEN p.i_Cve_Status = 1 THEN 'Activo' ELSE 'Inactivo' END AS t_Status,
    CASE WHEN p.i_Cve_Estado = 1 THEN 'Activo' ELSE 'Inactivo' END AS t_Estado
FROM [dbo].[Enc016ProgramacionReporteador] p
INNER JOIN [dbo].[Cat016PlantillasReporteador] pl ON p.i_Cve_Plantilla = pl.i_Cve_Plantilla
WHERE p.i_Cve_Estado = 1
GO

EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'Vista de Trabajo: Programaciones de reportes',
    @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'Vt016Programacion';
GO

-- Vista de Entorno: Programaciones
IF EXISTS (SELECT * FROM sys.views WHERE name = 'Ve016Programacion')
    DROP VIEW [dbo].[Ve016Programacion]
GO

CREATE VIEW [dbo].[Ve016Programacion] AS
SELECT * FROM (
    SELECT 'i_Cve_Programacion' as Nombre, 1 AS Llave, 11 AS Longitud, 0 AS TipoDato, 1 AS Visible, 'Clave Programación' AS NombreColumna, 0 AS PuedeInsertar, 0 AS PuedeModificar, '' AS ValorDefault, '2' AS TipoFiltro
    UNION ALL SELECT 'i_Cve_Plantilla', 0, 11, 0, 1, 'Clave Plantilla', 1, 1, '', '2'
    UNION ALL SELECT 't_NombrePlantilla', 0, 200, 1, 1, 'Plantilla', 0, 0, '', '1'
    UNION ALL SELECT 't_NombreProgramacion', 0, 150, 1, 1, 'Nombre Programación', 1, 1, '', '1'
    UNION ALL SELECT 't_FrecuenciaDesc', 0, 20, 1, 1, 'Frecuencia', 1, 1, '', '1'
    UNION ALL SELECT 't_DiasSemana', 0, 20, 1, 1, 'Días Semana', 1, 1, '', '1'
    UNION ALL SELECT 'i_DiaMes', 0, 2, 0, 1, 'Día Mes', 1, 1, '', '2'
    UNION ALL SELECT 't_Hora', 0, 8, 4, 1, 'Hora', 1, 1, '', '4'
    UNION ALL SELECT 't_Parametros', 0, 2147483647, 1, 1, 'Parámetros (JSON)', 1, 1, '', '1'
    UNION ALL SELECT 'f_ProximaEjecucion', 0, 23, 4, 1, 'Próxima Ejecución', 0, 0, '', '4'
    UNION ALL SELECT 'f_UltimaEjecucion', 0, 23, 4, 1, 'Última Ejecución', 0, 0, '', '4'
    UNION ALL SELECT 'f_FechaRegistro', 0, 23, 4, 1, 'Fecha Registro', 0, 0, '', '4'
    UNION ALL SELECT 't_UsuarioRegistro', 0, 50, 1, 1, 'Usuario Registro', 0, 0, '', '1'
    UNION ALL SELECT 't_Status', 0, 10, 1, 1, 'Status', 0, 0, '', '1'
    UNION ALL SELECT 't_Estado', 0, 10, 1, 1, 'Estado', 0, 0, '', '1'
) AS VE
GO

EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'Vista de Entorno: Configuración UI para programaciones',
    @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'Ve016Programacion';
GO

-- ===================================================================
-- MÓDULO 3: BITÁCORA DE EJECUCIONES (Opcional)
-- ===================================================================
PRINT 'Creando vistas para Módulo de Bitácora...'
GO

-- Vista de Trabajo: Bitácora
IF EXISTS (SELECT * FROM sys.views WHERE name = 'Vt016Bitacora')
    DROP VIEW [dbo].[Vt016Bitacora]
GO

CREATE VIEW [dbo].[Vt016Bitacora] AS
SELECT 
    b.i_Cve_Generacion,
    b.i_Cve_Programacion,
    p.t_Nombre AS t_NombreProgramacion,
    pl.t_Nombre AS t_NombrePlantilla,
    b.f_FechaInicio,
    b.f_FechaFin,
    b.t_Estatus,
    b.i_RegistrosProcesados,
    b.t_RutaDocumento,
    b.t_IdDocumento,
    b.t_Error,
    b.t_ParametrosUsados,
    DATEDIFF(SECOND, b.f_FechaInicio, ISNULL(b.f_FechaFin, GETDATE())) AS i_DuracionSegundos
FROM [dbo].[Bit016GeneracionReporteador] b
INNER JOIN [dbo].[Enc016ProgramacionReporteador] p ON b.i_Cve_Programacion = p.i_Cve_Programacion
INNER JOIN [dbo].[Cat016PlantillasReporteador] pl ON p.i_Cve_Plantilla = pl.i_Cve_Plantilla
WHERE b.i_Cve_Estado = 1
GO

EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'Vista de Trabajo: Bitácora de ejecuciones de reportes',
    @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'Vt016Bitacora';
GO

-- Vista de Entorno: Bitácora
IF EXISTS (SELECT * FROM sys.views WHERE name = 'Ve016Bitacora')
    DROP VIEW [dbo].[Ve016Bitacora]
GO

CREATE VIEW [dbo].[Ve016Bitacora] AS
SELECT * FROM (
    SELECT 'i_Cve_Generacion' as Nombre, 1 AS Llave, 11 AS Longitud, 0 AS TipoDato, 1 AS Visible, 'Clave Generación' AS NombreColumna, 0 AS PuedeInsertar, 0 AS PuedeModificar, '' AS ValorDefault, '2' AS TipoFiltro
    UNION ALL SELECT 'i_Cve_Programacion', 0, 11, 0, 1, 'Clave Programación', 0, 0, '', '2'
    UNION ALL SELECT 't_NombreProgramacion', 0, 150, 1, 1, 'Programación', 0, 0, '', '1'
    UNION ALL SELECT 't_NombrePlantilla', 0, 200, 1, 1, 'Plantilla', 0, 0, '', '1'
    UNION ALL SELECT 'f_FechaInicio', 0, 23, 4, 1, 'Inicio', 0, 0, '', '4'
    UNION ALL SELECT 'f_FechaFin', 0, 23, 4, 1, 'Fin', 0, 0, '', '4'
    UNION ALL SELECT 't_Estatus', 0, 20, 1, 1, 'Estatus', 0, 0, '', '1'
    UNION ALL SELECT 'i_RegistrosProcesados', 0, 11, 0, 1, 'Registros', 0, 0, '', '2'
    UNION ALL SELECT 'i_DuracionSegundos', 0, 11, 0, 1, 'Duración (s)', 0, 0, '', '2'
    UNION ALL SELECT 't_RutaDocumento', 0, 500, 1, 1, 'Ruta Documento', 0, 0, '', '1'
    UNION ALL SELECT 't_IdDocumento', 0, 50, 1, 1, 'ID Documento', 0, 0, '', '1'
    UNION ALL SELECT 't_Error', 0, 2147483647, 1, 1, 'Error', 0, 0, '', '1'
    UNION ALL SELECT 't_ParametrosUsados', 0, 2147483647, 1, 1, 'Parámetros Usados', 0, 0, '', '1'
) AS VE
GO

EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'Vista de Entorno: Configuración UI para bitácora de ejecuciones',
    @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'Ve016Bitacora';
GO

-- ===================================================================
-- VISTA ADICIONAL: Reportes pendientes (para el motor)
-- ===================================================================
PRINT 'Creando vista de programaciones pendientes...'
GO

IF EXISTS (SELECT * FROM sys.views WHERE name = 'Vig016ProgramacionesPendientes')
    DROP VIEW [dbo].[Vig016ProgramacionesPendientes]
GO

CREATE VIEW [dbo].[Vig016ProgramacionesPendientes] AS
SELECT 
    p.i_Cve_Programacion,
    p.t_Nombre,
    p.t_Frecuencia,
    p.t_Hora,
    p.f_ProximaEjecucion,
    pl.i_Cve_Plantilla,
    pl.t_Nombre AS t_NombrePlantilla,
    pl.t_Consulta,
    pl.t_ParametrosConfig,
    pl.t_RutaPlantilla,
    pl.t_NombreBaseDatos,
    pl.t_FormatoSalida
FROM [dbo].[Enc016ProgramacionReporteador] p
INNER JOIN [dbo].[Cat016PlantillasReporteador] pl ON p.i_Cve_Plantilla = pl.i_Cve_Plantilla
WHERE p.i_Cve_Estado = 1 
AND pl.i_Cve_Estado = 1
AND p.f_ProximaEjecucion IS NOT NULL
AND p.f_ProximaEjecucion <= GETDATE()
GO

EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'Vista de programaciones activas listas para ejecutar (para el motor)',
    @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'Vig016ProgramacionesPendientes';
GO

-- ===================================================================
-- RESUMEN
-- ===================================================================
PRINT '========================================================='
PRINT 'MÓDULO REPORTEADOR KROM - VISTAS VE/VT'
PRINT '========================================================='
PRINT 'Vistas de Trabajo (VT): 3'
PRINT '- Vt016Plantillas'
PRINT '- Vt016Programacion'  
PRINT '- Vt016Bitacora'
PRINT '========================================================='
PRINT 'Vistas de Entorno (VE): 3'
PRINT '- Ve016Plantillas'
PRINT '- Ve016Programacion'
PRINT '- Ve016Bitacora'
PRINT '========================================================='
PRINT 'Vistas adicionales: 1'
PRINT '- Vig016ProgramacionesPendientes'
PRINT '========================================================='
PRINT 'Script ejecutado exitosamente.'
PRINT '========================================================='
GO