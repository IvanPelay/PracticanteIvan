-- =============================================
-- SCRIPT DE CREACIÓN DE BASE DE DATOS
-- GENERADOR DE REPORTES AUTOMATIZADOS
-- FAMILIA 016 - REPORTES
-- VERSIÓN: 1.0
-- AUTOR: Sistema Automatizado
-- FECHA: 2024
-- =============================================

USE [SysExpert]; -- O la BD donde se implementará el sistema
GO

PRINT 'Creando tablas del Generador de Reportes Automatizados...';
GO

-- =============================================
-- 1. CATÁLOGO DE PLANTILLAS DE REPORTES
-- =============================================
PRINT 'Creando Cat016PlantillasReportes...';
GO

CREATE TABLE [dbo].[Cat016PlantillasReportes] (
    -- IDENTIFICADOR
    i_Cve_Plantilla INT IDENTITY(1,1) NOT NULL,
    
    -- INFORMACIÓN BÁSICA
    t_NombrePlantilla VARCHAR(200) NOT NULL,
    t_Descripcion VARCHAR(500) NULL,
    
    -- REFERENCIAS A SVN
    t_RutaPlantillaSVN VARCHAR(500) NOT NULL,
    t_RutaConfigSVN VARCHAR(500) NULL,
    
    -- CONFIGURACIÓN DE CONEXIÓN
    t_NombreBaseDatos VARCHAR(100) NOT NULL DEFAULT 'SysExpert',
    t_ConsultaSQL TEXT NULL,
    t_ParametrosConfigJSON NVARCHAR(MAX) NULL,
    
    -- CONFIGURACIÓN DE SALIDA
    t_FormatoSalida VARCHAR(10) NOT NULL DEFAULT 'XLSX',
    
    -- ESTADO Y AUDITORÍA
    i_Estatus INT NOT NULL DEFAULT 1,
    fh_FechaRegistro DATETIME NOT NULL DEFAULT GETDATE(),
    t_UsuarioRegistro VARCHAR(50) NOT NULL,
    fh_FechaModificacion DATETIME NULL,
    t_UsuarioModificacion VARCHAR(50) NULL,
    
    -- RESTRICCIONES
    CONSTRAINT PK_Cat016PlantillasReportes PRIMARY KEY (i_Cve_Plantilla),
    CONSTRAINT CK_Cat016PlantillasReportes_Estatus 
        CHECK (i_Estatus IN (0, 1, 2)), -- 0=Inactiva, 1=Activa, 2=En revisión
    CONSTRAINT CK_Cat016PlantillasReportes_Formato 
        CHECK (t_FormatoSalida IN ('XLSX', 'CSV', 'PDF'))
);
GO

-- =============================================
-- 2. ENCABEZADO DE VALIDACIONES DE REPORTES
-- =============================================
PRINT 'Creando Enc016ValidacionesReportes...';
GO

CREATE TABLE [dbo].[Enc016ValidacionesReportes] (
    -- IDENTIFICADOR
    i_Cve_Validacion INT IDENTITY(1,1) NOT NULL,
    
    -- RELACIÓN CON PLANTILLA
    i_Cve_Plantilla INT NOT NULL,
    
    -- CONFIGURACIÓN DE VALIDACIÓN
    t_TipoValidacion VARCHAR(50) NOT NULL,
    t_Descripcion VARCHAR(300) NULL,
    t_ConfiguracionJSON NVARCHAR(MAX) NOT NULL,
    
    -- ORDEN Y ESTADO
    i_Orden INT NOT NULL DEFAULT 0,
    i_Estatus INT NOT NULL DEFAULT 1,
    
    -- AUDITORÍA
    fh_FechaRegistro DATETIME NOT NULL DEFAULT GETDATE(),
    t_UsuarioRegistro VARCHAR(50) NOT NULL,
    fh_FechaModificacion DATETIME NULL,
    t_UsuarioModificacion VARCHAR(50) NULL,
    
    -- RESTRICCIONES
    CONSTRAINT PK_Enc016ValidacionesReportes PRIMARY KEY (i_Cve_Validacion),
    CONSTRAINT FK_Enc016ValidacionesReportes_Plantilla 
        FOREIGN KEY (i_Cve_Plantilla) 
        REFERENCES [dbo].[Cat016PlantillasReportes](i_Cve_Plantilla)
        ON DELETE CASCADE,
    CONSTRAINT CK_Enc016ValidacionesReportes_Estatus 
        CHECK (i_Estatus IN (0, 1)) -- 0=Inactiva, 1=Activa
);
GO

-- =============================================
-- 3. CONFIGURACIÓN DEL SISTEMA DE AUTOMATIZACIÓN
-- =============================================
PRINT 'Creando Cng016ConfigAutomatizacion...';
GO

CREATE TABLE [dbo].[Cng016ConfigAutomatizacion] (
    -- IDENTIFICADOR
    i_Cve_Config INT IDENTITY(1,1) NOT NULL,
    
    -- CONFIGURACIÓN
    t_Clave VARCHAR(50) NOT NULL,
    t_Valor VARCHAR(500) NOT NULL,
    t_Descripcion VARCHAR(200) NULL,
    t_Grupo VARCHAR(50) NULL,
    
    -- ESTADO Y AUDITORÍA
    i_Estatus INT NOT NULL DEFAULT 1,
    fh_Actualizacion DATETIME NOT NULL DEFAULT GETDATE(),
    t_UsuarioActualizacion VARCHAR(50) NOT NULL,
    
    -- RESTRICCIONES
    CONSTRAINT PK_Cng016ConfigAutomatizacion PRIMARY KEY (i_Cve_Config),
    CONSTRAINT UQ_Cng016ConfigAutomatizacion_Clave UNIQUE (t_Clave),
    CONSTRAINT CK_Cng016ConfigAutomatizacion_Estatus 
        CHECK (i_Estatus IN (0, 1)) -- 0=Inactiva, 1=Activa
);
GO

-- =============================================
-- 4. ENCABEZADO DE PROGRAMACIÓN DE REPORTES
-- =============================================
PRINT 'Creando Enc016ProgramacionReportes...';
GO

CREATE TABLE [dbo].[Enc016ProgramacionReportes] (
    -- IDENTIFICADOR
    i_Cve_Programacion INT IDENTITY(1,1) NOT NULL,
    
    -- RELACIÓN CON PLANTILLA
    i_Cve_Plantilla INT NOT NULL,
    
    -- INFORMACIÓN BÁSICA
    t_NombreProgramacion VARCHAR(150) NOT NULL,
    
    -- CONFIGURACIÓN DE FRECUENCIA
    t_Frecuencia CHAR(1) NOT NULL,
    t_DiasSemana VARCHAR(20) NULL,
    t_DiaMes INT NULL,
    t_HoraEjecucion TIME NOT NULL,
    
    -- PARÁMETROS ESPECÍFICOS
    t_ParametrosFijosJSON NVARCHAR(MAX) NULL,
    
    -- ESTADO Y SEGUIMIENTO
    i_Estatus INT NOT NULL DEFAULT 1,
    fh_ProximaEjecucion DATETIME NULL,
    fh_UltimaEjecucion DATETIME NULL,
    
    -- AUDITORÍA
    fh_FechaRegistro DATETIME NOT NULL DEFAULT GETDATE(),
    t_UsuarioRegistro VARCHAR(50) NOT NULL,
    fh_FechaModificacion DATETIME NULL,
    t_UsuarioModificacion VARCHAR(50) NULL,
    
    -- RESTRICCIONES
    CONSTRAINT PK_Enc016ProgramacionReportes PRIMARY KEY (i_Cve_Programacion),
    CONSTRAINT FK_Enc016ProgramacionReportes_Plantilla 
        FOREIGN KEY (i_Cve_Plantilla) 
        REFERENCES [dbo].[Cat016PlantillasReportes](i_Cve_Plantilla)
        ON DELETE CASCADE,
    CONSTRAINT CK_Enc016ProgramacionReportes_Estatus 
        CHECK (i_Estatus IN (0, 1, 2)), -- 0=Inactiva, 1=Activa, 2=Pausada
    CONSTRAINT CK_Enc016ProgramacionReportes_Frecuencia 
        CHECK (t_Frecuencia IN ('D', 'S', 'M', 'U')), -- D=Diaria, S=Semanal, M=Mensual, U=Única
    CONSTRAINT CK_Enc016ProgramacionReportes_DiaMes 
        CHECK (t_DiaMes IS NULL OR (t_DiaMes BETWEEN 1 AND 31) OR t_DiaMes = 99) -- 99=último día
);
GO

-- =============================================
-- 5. DETALLE DE DESTINATARIOS DE REPORTES
-- =============================================
PRINT 'Creando Det016DestinatariosReportes...';
GO

CREATE TABLE [dbo].[Det016DestinatariosReportes] (
    -- IDENTIFICADOR
    i_Cve_Destinatario INT IDENTITY(1,1) NOT NULL,
    
    -- RELACIÓN CON PROGRAMACIÓN
    i_Cve_Programacion INT NOT NULL,
    
    -- INFORMACIÓN DEL DESTINATARIO
    t_NombreDestinatario VARCHAR(150) NULL,
    t_Correo VARCHAR(200) NOT NULL,
    
    -- CONFIGURACIÓN DE NOTIFICACIÓN
    t_TipoDestino CHAR(1) NOT NULL DEFAULT 'T',
    t_TipoNotificacion CHAR(1) NOT NULL DEFAULT 'A',
    
    -- ORDEN Y ESTADO
    i_Orden INT NOT NULL DEFAULT 0,
    i_Estatus INT NOT NULL DEFAULT 1,
    
    -- AUDITORÍA
    fh_FechaRegistro DATETIME NOT NULL DEFAULT GETDATE(),
    t_UsuarioRegistro VARCHAR(50) NOT NULL,
    
    -- RESTRICCIONES
    CONSTRAINT PK_Det016DestinatariosReportes PRIMARY KEY (i_Cve_Destinatario),
    CONSTRAINT FK_Det016DestinatariosReportes_Programacion 
        FOREIGN KEY (i_Cve_Programacion) 
        REFERENCES [dbo].[Enc016ProgramacionReportes](i_Cve_Programacion)
        ON DELETE CASCADE,
    CONSTRAINT CK_Det016DestinatariosReportes_Estatus 
        CHECK (i_Estatus IN (0, 1)), -- 0=Inactivo, 1=Activo
    CONSTRAINT CK_Det016DestinatariosReportes_TipoDestino 
        CHECK (t_TipoDestino IN ('T', 'C', 'B')), -- T=To, C=CC, B=BCC
    CONSTRAINT CK_Det016DestinatariosReportes_TipoNotificacion 
        CHECK (t_TipoNotificacion IN ('A', 'E', 'F')) -- A=Ambos, E=Éxito, F=Fallo
);
GO

-- =============================================
-- 6. BITÁCORA DE EJECUCIONES DE REPORTES
-- =============================================
PRINT 'Creando Bit016EjecucionesReportes...';
GO

CREATE TABLE [dbo].[Bit016EjecucionesReportes] (
    -- IDENTIFICADOR
    i_Cve_Ejecucion INT IDENTITY(1,1) NOT NULL,
    
    -- RELACIÓN CON PROGRAMACIÓN
    i_Cve_Programacion INT NOT NULL,
    
    -- TIEMPOS DE EJECUCIÓN
    fh_InicioEjecucion DATETIME NOT NULL DEFAULT GETDATE(),
    fh_FinEjecucion DATETIME NULL,
    
    -- RESULTADO DE LA EJECUCIÓN
    t_Estatus VARCHAR(20) NOT NULL,
    i_RegistrosProcesados INT NULL,
    
    -- ARCHIVO GENERADO
    t_RutaDocumento VARCHAR(500) NULL,
    t_IDDocumentoPaperless VARCHAR(50) NULL,
    
    -- INFORMACIÓN DE ERROR (SI APLICA)
    t_ErrorDetalle TEXT NULL,
    
    -- PARÁMETROS USADOS
    t_ParametrosUsadosJSON NVARCHAR(MAX) NULL,
    
    -- AUDITORÍA
    fh_FechaRegistro DATETIME NOT NULL DEFAULT GETDATE(),
    
    -- RESTRICCIONES
    CONSTRAINT PK_Bit016EjecucionesReportes PRIMARY KEY (i_Cve_Ejecucion),
    CONSTRAINT FK_Bit016EjecucionesReportes_Programacion 
        FOREIGN KEY (i_Cve_Programacion) 
        REFERENCES [dbo].[Enc016ProgramacionReportes](i_Cve_Programacion)
        ON DELETE CASCADE,
    CONSTRAINT CK_Bit016EjecucionesReportes_Estatus 
        CHECK (t_Estatus IN ('EXITO', 'ERROR', 'ADVERTENCIA', 'VALIDACION_FALLIDA', 'EN_PROCESO'))
);
GO

PRINT 'Tablas creadas exitosamente.';
GO

-- =============================================
-- CREACIÓN DE ÍNDICES PARA OPTIMIZACIÓN
-- =============================================
PRINT 'Creando índices de optimización...';
GO

-- ÍNDICES PARA Cat016PlantillasReportes
CREATE INDEX IX_Cat016PlantillasReportes_Nombre 
ON [dbo].[Cat016PlantillasReportes] (t_NombrePlantilla);

CREATE INDEX IX_Cat016PlantillasReportes_Estatus 
ON [dbo].[Cat016PlantillasReportes] (i_Estatus);

-- ÍNDICES PARA Enc016ValidacionesReportes
CREATE INDEX IX_Enc016ValidacionesReportes_Plantilla 
ON [dbo].[Enc016ValidacionesReportes] (i_Cve_Plantilla);

CREATE INDEX IX_Enc016ValidacionesReportes_Tipo 
ON [dbo].[Enc016ValidacionesReportes] (t_TipoValidacion);

CREATE INDEX IX_Enc016ValidacionesReportes_Orden 
ON [dbo].[Enc016ValidacionesReportes] (i_Orden);

-- ÍNDICES PARA Cng016ConfigAutomatizacion
CREATE INDEX IX_Cng016ConfigAutomatizacion_Grupo 
ON [dbo].[Cng016ConfigAutomatizacion] (t_Grupo);

CREATE INDEX IX_Cng016ConfigAutomatizacion_Clave 
ON [dbo].[Cng016ConfigAutomatizacion] (t_Clave);

-- ÍNDICES PARA Enc016ProgramacionReportes
CREATE INDEX IX_Enc016ProgramacionReportes_Plantilla 
ON [dbo].[Enc016ProgramacionReportes] (i_Cve_Plantilla);

CREATE INDEX IX_Enc016ProgramacionReportes_Estatus 
ON [dbo].[Enc016ProgramacionReportes] (i_Estatus);

CREATE INDEX IX_Enc016ProgramacionReportes_ProximaEjecucion 
ON [dbo].[Enc016ProgramacionReportes] (fh_ProximaEjecucion) 
WHERE i_Estatus = 1;

CREATE INDEX IX_Enc016ProgramacionReportes_Frecuencia 
ON [dbo].[Enc016ProgramacionReportes] (t_Frecuencia);

-- ÍNDICES PARA Det016DestinatariosReportes
CREATE INDEX IX_Det016DestinatariosReportes_Programacion 
ON [dbo].[Det016DestinatariosReportes] (i_Cve_Programacion);

CREATE INDEX IX_Det016DestinatariosReportes_Correo 
ON [dbo].[Det016DestinatariosReportes] (t_Correo);

CREATE INDEX IX_Det016DestinatariosReportes_Estatus 
ON [dbo].[Det016DestinatariosReportes] (i_Estatus);

-- ÍNDICES PARA Bit016EjecucionesReportes
CREATE INDEX IX_Bit016EjecucionesReportes_Programacion 
ON [dbo].[Bit016EjecucionesReportes] (i_Cve_Programacion);

CREATE INDEX IX_Bit016EjecucionesReportes_Fecha 
ON [dbo].[Bit016EjecucionesReportes] (fh_InicioEjecucion);

CREATE INDEX IX_Bit016EjecucionesReportes_Estatus 
ON [dbo].[Bit016EjecucionesReportes] (t_Estatus);

CREATE INDEX IX_Bit016EjecucionesReportes_Paperless 
ON [dbo].[Bit016EjecucionesReportes] (t_IDDocumentoPaperless) 
WHERE t_IDDocumentoPaperless IS NOT NULL;

PRINT 'Índices creados exitosamente.';
GO

-- =============================================
-- DATOS INICIALES DE CONFIGURACIÓN
-- =============================================
PRINT 'Insertando configuración inicial...';
GO

-- CONFIGURACIÓN DE CORREO
INSERT INTO [dbo].[Cng016ConfigAutomatizacion] 
    (t_Clave, t_Valor, t_Descripcion, t_Grupo, t_UsuarioActualizacion)
VALUES
    ('SMTP_SERVER', 'smtp.office365.com', 'Servidor SMTP para envío de correos', 'CORREO', 'SISTEMA'),
    ('SMTP_PORT', '587', 'Puerto SMTP', 'CORREO', 'SISTEMA'),
    ('SMTP_USER', 'notificaciones@krom.com.mx', 'Usuario SMTP', 'CORREO', 'SISTEMA'),
    ('SMTP_PASSWORD', '', 'Contraseña SMTP (encriptada)', 'CORREO', 'SISTEMA'),
    ('SMTP_SSL', '1', 'Usar SSL (1=Sí, 0=No)', 'CORREO', 'SISTEMA'),
    ('SMTP_TIMEOUT', '30000', 'Timeout en milisegundos', 'CORREO', 'SISTEMA'),
    ('CORREO_REMITENTE', 'notificaciones@krom.com.mx', 'Correo remitente por defecto', 'CORREO', 'SISTEMA'),
    ('CORREO_NOMBRE_REMITENTE', 'Sistema de Reportes Automatizados', 'Nombre del remitente', 'CORREO', 'SISTEMA');

-- RUTAS DEL SISTEMA
INSERT INTO [dbo].[Cng016ConfigAutomatizacion] 
    (t_Clave, t_Valor, t_Descripcion, t_Grupo, t_UsuarioActualizacion)
VALUES
    ('RUTA_BASE_REPORTES', '\\KROMSERVER\ReportesAutomatizados\', 'Ruta base para guardar reportes generados', 'RUTAS', 'SISTEMA'),
    ('RUTA_TEMPORAL', 'C:\Temp\ReportesAutomatizados\', 'Ruta para archivos temporales', 'RUTAS', 'SISTEMA'),
    ('RETENCION_DIAS', '365', 'Días de retención de reportes generados', 'RUTAS', 'SISTEMA'),
    ('MAX_SIZE_MB', '50', 'Tamaño máximo por archivo en MB', 'RUTAS', 'SISTEMA');

-- CONFIGURACIÓN GENERAL
INSERT INTO [dbo].[Cng016ConfigAutomatizacion] 
    (t_Clave, t_Valor, t_Descripcion, t_Grupo, t_UsuarioActualizacion)
VALUES
    ('TIEMPO_MAX_EJECUCION', '600', 'Tiempo máximo de ejecución en segundos', 'GENERAL', 'SISTEMA'),
    ('HORA_INICIO_SCHEDULER', '06:00', 'Hora de inicio del scheduler diario', 'GENERAL', 'SISTEMA'),
    ('HORA_FIN_SCHEDULER', '22:00', 'Hora de fin del scheduler diario', 'GENERAL', 'SISTEMA'),
    ('INTERVALO_SCHEDULER', '60', 'Intervalo de verificación en segundos', 'GENERAL', 'SISTEMA'),
    ('LOG_LEVEL', 'INFO', 'Nivel de log (DEBUG, INFO, WARN, ERROR)', 'GENERAL', 'SISTEMA'),
    ('MAX_REGISTROS_EXCEL', '1000000', 'Máximo de registros por archivo Excel', 'GENERAL', 'SISTEMA'),
    ('ENVIAR_CORREO_EXITOSO', '1', 'Enviar correo cuando el reporte es exitoso (1=Sí, 0=No)', 'GENERAL', 'SISTEMA');

-- INTEGRACIÓN CON SVN
INSERT INTO [dbo].[Cng016ConfigAutomatizacion] 
    (t_Clave, t_Valor, t_Descripcion, t_Grupo, t_UsuarioActualizacion)
VALUES
    ('SVN_URL', 'https://svn.krom.com.mx/svn/Desarrollo/', 'URL base del repositorio SVN', 'INTEGRACION_SVN', 'SISTEMA'),
    ('SVN_USER', 'svn_reportes', 'Usuario SVN', 'INTEGRACION_SVN', 'SISTEMA'),
    ('SVN_PASSWORD', '', 'Contraseña SVN (encriptada)', 'INTEGRACION_SVN', 'SISTEMA'),
    ('SVN_RUTA_PLANTILLAS', '/Reportes/Plantillas/', 'Ruta base de plantillas Excel en SVN', 'INTEGRACION_SVN', 'SISTEMA'),
    ('SVN_RUTA_CONFIG', '/Reportes/Config/', 'Ruta base de configuración JSON en SVN', 'INTEGRACION_SVN', 'SISTEMA');

-- INTEGRACIÓN CON PAPERLESS
INSERT INTO [dbo].[Cng016ConfigAutomatizacion] 
    (t_Clave, t_Valor, t_Descripcion, t_Grupo, t_UsuarioActualizacion)
VALUES
    ('PAPERLESS_URL', 'http://paperless.krom.com.mx/api', 'URL de API de Paperless', 'INTEGRACION_PAPERLESS', 'SISTEMA'),
    ('PAPERLESS_API_KEY', '', 'API Key para Paperless', 'INTEGRACION_PAPERLESS', 'SISTEMA'),
    ('PAPERLESS_CARPETA_ID', '16', 'ID de carpeta donde subir reportes', 'INTEGRACION_PAPERLESS', 'SISTEMA'),
    ('SUBIR_A_PAPERLESS', '1', 'Subir reportes a Paperless automáticamente (1=Sí, 0=No)', 'INTEGRACION_PAPERLESS', 'SISTEMA');

-- CONFIGURACIÓN DE CONEXIONES A BD
INSERT INTO [dbo].[Cng016ConfigAutomatizacion] 
    (t_Clave, t_Valor, t_Descripcion, t_Grupo, t_UsuarioActualizacion)
VALUES
    ('BD_SYSEXPERT_CONNECTION', 'Server=KROMDB;Database=SysExpert;Integrated Security=True;', 'Cadena de conexión a SysExpert', 'CONEXIONES_BD', 'SISTEMA'),
    ('BD_KRONBASE_CONNECTION', 'Server=KROMDB;Database=KronBase;Integrated Security=True;', 'Cadena de conexión a KronBase', 'CONEXIONES_BD', 'SISTEMA'),
    ('BD_TIMEOUT', '300', 'Timeout de consultas en segundos', 'CONEXIONES_BD', 'SISTEMA'),
    ('BD_POOL_SIZE', '100', 'Tamaño máximo del pool de conexiones', 'CONEXIONES_BD', 'SISTEMA');

PRINT 'Configuración inicial insertada.';
GO

-- =============================================
-- COMENTARIOS DESCRIPTIVOS DE LAS TABLAS
-- =============================================
PRINT 'Agregando descripciones a las tablas...';
GO

-- Cat016PlantillasReportes
EXEC sp_addextendedproperty 
    @name = N'MS_Description', 
    @value = N'Catálogo maestro de plantillas de reportes con referencias a SVN y configuración de consultas SQL',
    @level0type = N'SCHEMA', @level0name = N'dbo', 
    @level1type = N'TABLE', @level1name = N'Cat016PlantillasReportes';

-- Enc016ValidacionesReportes
EXEC sp_addextendedproperty 
    @name = N'MS_Description', 
    @value = N'Encabezado de validaciones aplicables a cada plantilla de reporte para control de calidad',
    @level0type = N'SCHEMA', @level0name = N'dbo', 
    @level1type = N'TABLE', @level1name = N'Enc016ValidacionesReportes';

-- Cng016ConfigAutomatizacion
EXEC sp_addextendedproperty 
    @name = N'MS_Description', 
    @value = N'Configuración global del sistema de automatización de reportes (correo, rutas, integraciones)',
    @level0type = N'SCHEMA', @level0name = N'dbo', 
    @level1type = N'TABLE', @level1name = N'Cng016ConfigAutomatizacion';

-- Enc016ProgramacionReportes
EXEC sp_addextendedproperty 
    @name = N'MS_Description', 
    @value = N'Encabezado de programaciones de ejecución de reportes con frecuencias y parámetros fijos',
    @level0type = N'SCHEMA', @level0name = N'dbo', 
    @level1type = N'TABLE', @level1name = N'Enc016ProgramacionReportes';

-- Det016DestinatariosReportes
EXEC sp_addextendedproperty 
    @name = N'MS_Description', 
    @value = N'Detalle de destinatarios por programación de reporte con configuración de notificaciones',
    @level0type = N'SCHEMA', @level0name = N'dbo', 
    @level1type = N'TABLE', @level1name = N'Det016DestinatariosReportes';

-- Bit016EjecucionesReportes
EXEC sp_addextendedproperty 
    @name = N'MS_Description', 
    @value = N'Bitácora histórica de todas las ejecuciones realizadas por el sistema de reportes',
    @level0type = N'SCHEMA', @level0name = N'dbo', 
    @level1type = N'TABLE', @level1name = N'Bit016EjecucionesReportes';

PRINT 'Descripciones agregadas.';
GO

-- =============================================
-- INSERCIÓN DE DATOS DE EJEMPLO
-- =============================================
PRINT 'Insertando datos de ejemplo...';
GO

-- EJEMPLO DE PLANTILLA (Anexo 24)
INSERT INTO [dbo].[Cat016PlantillasReportes] 
    (t_NombrePlantilla, t_Descripcion, t_RutaPlantillaSVN, t_RutaConfigSVN, 
    t_NombreBaseDatos, t_ConsultaSQL, t_FormatoSalida, t_UsuarioRegistro)
VALUES
    (
        'Anexo 24 - Importaciones WEG México',
        'Reporte mensual de importaciones para cumplimiento del Anexo 24',
        '/Reportes/Plantillas/Anexo24_WEG_Mexico.xlsx',
        '/Reportes/Config/Anexo24_WEG_Mexico.json',
        'SysExpert',
        'USE SysExpert; DECLARE @FechaInicio VARCHAR(20); DECLARE @FechaFin VARCHAR(20); DECLARE @TipoOperacion VARCHAR(20); DECLARE @RFC VARCHAR(20); SET @FechaInicio = ''@FechaInicio''; SET @FechaFin = ''@FechaFin''; SET @TipoOperacion = @TipoOperacion; SET @RFC = ''@RFC''; SELECT o.NumeroReferencia, SUBSTRING(CAST(o.Anio AS CHAR(4)), 3, 2) + '' '' + SUBSTRING(o.ClaveAduana, 1, 2) + '' '' + o.Patente + '' '' + o.NumeroPedimento AS Pedimento, ISNULL(REPLACE(o.Pago, ''1900-01-01'', ''''), '''') AS Pago, ...',
        'XLSX',
        'admin'
    ),
    (
        'Reporte de Exportaciones Semanal',
        'Reporte semanal de exportaciones para análisis comercial',
        '/Reportes/Plantillas/Exportaciones_Semanal.xlsx',
        '/Reportes/Config/Exportaciones_Semanal.json',
        'SysExpert',
        'SELECT * FROM VT016Operaciones_ WHERE TipoOperacion = 2',
        'XLSX',
        'admin'
    );

-- EJEMPLO DE VALIDACIONES PARA ANEXO 24
INSERT INTO [dbo].[Enc016ValidacionesReportes] 
    (i_Cve_Plantilla, t_TipoValidacion, t_Descripcion, t_ConfiguracionJSON, i_Orden, t_UsuarioRegistro)
VALUES
    (
        1, -- Anexo 24
        'MIN_REGISTROS',
        'Validar que haya al menos 1 registro',
        '{"minimo": 1, "mensajeError": "No se encontraron registros para el período especificado"}',
        1,
        'admin'
    ),
    (
        1, -- Anexo 24
        'CAMPOS_OBLIGATORIOS',
        'Validar campos obligatorios no nulos',
        '{"campos": ["NumeroReferencia", "Pedimento", "Pago"], "mensajeError": "Faltan campos obligatorios en los resultados"}',
        2,
        'admin'
    ),
    (
        1, -- Anexo 24
        'RANGO_FECHAS',
        'Validar que las fechas estén en rango válido',
        '{"campoFecha": "Pago", "fechaMinima": "2020-01-01", "fechaMaxima": "2030-12-31", "mensajeError": "Fecha fuera de rango válido"}',
        3,
        'admin'
    );

-- EJEMPLO DE PROGRAMACIÓN PARA ANEXO 24
INSERT INTO [dbo].[Enc016ProgramacionReportes] 
    (i_Cve_Plantilla, t_NombreProgramacion, t_Frecuencia, t_HoraEjecucion, 
    t_ParametrosFijosJSON, i_Estatus, t_UsuarioRegistro)
VALUES
    (
        1, -- Anexo 24
        'Reporte Mensual WEG México - Anexo 24',
        'M', -- Mensual
        '08:00:00',
        '{"@TipoOperacion": 1, "@RFC": "WME990813BAA"}',
        1, -- Activa
        'admin'
    ),
    (
        1, -- Anexo 24
        'Reporte Semanal WEG México - Monitoreo',
        'S', -- Semanal
        '09:00:00',
        '{"@TipoOperacion": 1, "@RFC": "WME990813BAA", "@DiasAtras": 7}',
        1, -- Activa
        'admin'
    );

-- EJEMPLO DE DESTINATARIOS PARA LA PROGRAMACIÓN 1
INSERT INTO [dbo].[Det016DestinatariosReportes] 
    (i_Cve_Programacion, t_NombreDestinatario, t_Correo, 
    t_TipoDestino, t_TipoNotificacion, i_Orden, t_UsuarioRegistro)
VALUES
    (
        1, -- Programación mensual
        'Contabilidad WEG México',
        'contabilidad.mexico@weg.com',
        'T', -- To
        'A', -- Ambos (éxito y error)
        1,
        'admin'
    ),
    (
        1, -- Programación mensual
        'Gerente de Logística',
        'gerente.logistica@weg.com',
        'C', -- CC
        'E', -- Solo éxito
        2,
        'admin'
    ),
    (
        1, -- Programación mensual
        'Soporte KROM',
        'soporte.reportes@krom.com.mx',
        'B', -- BCC
        'F', -- Solo fallo
        3,
        'admin'
    );

PRINT 'Datos de ejemplo insertados.';
GO

-- =============================================
-- RESUMEN FINAL
-- =============================================
PRINT '=============================================';
PRINT 'BASE DE DATOS CREADA EXITOSAMENTE';
PRINT '=============================================';
PRINT '';
PRINT 'TABLAS CREADAS: 6';
PRINT '   • Cat016PlantillasReportes';
PRINT '   • Enc016ValidacionesReportes';  
PRINT '   • Cng016ConfigAutomatizacion';
PRINT '   • Enc016ProgramacionReportes';
PRINT '   • Det016DestinatariosReportes';
PRINT '   • Bit016EjecucionesReportes';
PRINT '';
PRINT 'ÍNDICES CREADOS: 18';
PRINT '   • Optimizados para búsquedas frecuentes';
PRINT '   • Índices condicionales para scheduler';
PRINT '   • Índices para integraciones (Paperless)';
PRINT '';
PRINT 'CONFIGURACIÓN INICIAL:';
PRINT '   • Parámetros de correo: 8';
PRINT '   • Rutas del sistema: 4';
PRINT '   • Configuración general: 7';
PRINT '   • Integración SVN: 5';
PRINT '   • Integración Paperless: 4';
PRINT '   • Conexiones BD: 4';
PRINT '';
PRINT 'DATOS DE EJEMPLO:';
PRINT '   • Plantillas: 2';
PRINT '   • Validaciones: 3';
PRINT '   • Programaciones: 2';
PRINT '   • Destinatarios: 3';
PRINT '';
PRINT '=============================================';
PRINT 'EL SISTEMA ESTÁ LISTO PARA CONFIGURAR';
PRINT '=============================================';
GO