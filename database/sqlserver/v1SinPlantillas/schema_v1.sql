USE [SysExpert]; -- O la base de datos donde se implementará el sistema
GO

-- =============================================
-- Tabla: Catálogo de Plantillas de Reportes
-- =============================================
CREATE TABLE [dbo].[Cat016PlantillasReportes] (
    i_Cve_Plantilla INT IDENTITY(1,1) NOT NULL,
    t_NombrePlantilla VARCHAR(200) NOT NULL,
    t_Descripcion VARCHAR(500) NULL,
    t_ConsultaSQL TEXT NOT NULL,
    t_FormatoSalida VARCHAR(10) NOT NULL DEFAULT 'XLSX', -- XLSX, CSV, PDF
    t_ColumnasJSON NVARCHAR(MAX) NULL, -- Mapeo de columnas SQL a Excel
    t_ParametrosJSON NVARCHAR(MAX) NULL, -- Definición de parámetros de la consulta
    i_Estatus INT NOT NULL DEFAULT 1, -- 1=Activa, 0=Inactiva, 2=En revisión
    fh_FechaRegistro DATETIME NOT NULL DEFAULT GETDATE(),
    t_UsuarioRegistro VARCHAR(50) NOT NULL,
    fh_FechaModificacion DATETIME NULL,
    t_UsuarioModificacion VARCHAR(50) NULL,
    CONSTRAINT PK_Cat016PlantillasReportes PRIMARY KEY (i_Cve_Plantilla)
);
GO

-- Índice para búsqueda por nombre
CREATE INDEX IX_Cat016PlantillasReportes_Nombre 
ON [dbo].[Cat016PlantillasReportes] (t_NombrePlantilla);
GO

-- Índice para estatus activo
CREATE INDEX IX_Cat016PlantillasReportes_Estatus 
ON [dbo].[Cat016PlantillasReportes] (i_Estatus);
GO

-- =============================================
-- Tabla: Encabezado de Validaciones de Reportes
-- =============================================
CREATE TABLE [dbo].[Enc016ValidacionesReportes] (
    i_Cve_Validacion INT IDENTITY(1,1) NOT NULL,
    i_Cve_Plantilla INT NOT NULL,
    t_TipoValidacion VARCHAR(50) NOT NULL, -- MIN_REGISTROS, CAMPOS_OBLIGATORIOS, TOTALES, ETC.
    t_Descripcion VARCHAR(300) NULL,
    t_ConfiguracionJSON NVARCHAR(MAX) NOT NULL, -- Parámetros específicos de la validación
    i_Orden INT NOT NULL DEFAULT 0,
    i_Estatus INT NOT NULL DEFAULT 1, -- 1=Activa, 0=Inactiva
    fh_FechaRegistro DATETIME NOT NULL DEFAULT GETDATE(),
    t_UsuarioRegistro VARCHAR(50) NOT NULL,
    fh_FechaModificacion DATETIME NULL,
    t_UsuarioModificacion VARCHAR(50) NULL,
    CONSTRAINT PK_Enc016ValidacionesReportes PRIMARY KEY (i_Cve_Validacion),
    CONSTRAINT FK_Enc016ValidacionesReportes_Plantilla 
        FOREIGN KEY (i_Cve_Plantilla) 
        REFERENCES [dbo].[Cat016PlantillasReportes](i_Cve_Plantilla)
        ON DELETE CASCADE
);
GO

-- Índice para búsqueda por plantilla
CREATE INDEX IX_Enc016ValidacionesReportes_Plantilla 
ON [dbo].[Enc016ValidacionesReportes] (i_Cve_Plantilla);
GO

-- Índice para tipo de validación
CREATE INDEX IX_Enc016ValidacionesReportes_Tipo 
ON [dbo].[Enc016ValidacionesReportes] (t_TipoValidacion);
GO

-- =============================================
-- Tabla: Configuración del Sistema de Automatización
-- =============================================
CREATE TABLE [dbo].[Cng016ConfigAutomatizacion] (
    i_Cve_Config INT IDENTITY(1,1) NOT NULL,
    t_Clave VARCHAR(50) NOT NULL,
    t_Valor VARCHAR(500) NOT NULL,
    t_Descripcion VARCHAR(200) NULL,
    t_Grupo VARCHAR(50) NULL, -- GENERAL, CORREO, RUTAS, CONEXIONES
    i_Estatus INT NOT NULL DEFAULT 1, -- 1=Activa, 0=Inactiva
    fh_Actualizacion DATETIME NOT NULL DEFAULT GETDATE(),
    t_UsuarioActualizacion VARCHAR(50) NOT NULL,
    CONSTRAINT PK_Cng016ConfigAutomatizacion PRIMARY KEY (i_Cve_Config),
    CONSTRAINT UQ_Cng016ConfigAutomatizacion_Clave UNIQUE (t_Clave)
);
GO

-- Índice para búsqueda por grupo
CREATE INDEX IX_Cng016ConfigAutomatizacion_Grupo 
ON [dbo].[Cng016ConfigAutomatizacion] (t_Grupo);
GO

-- =============================================
-- Tabla: Encabezado de Programación de Reportes
-- =============================================
CREATE TABLE [dbo].[Enc016ProgramacionReportes] (
    i_Cve_Programacion INT IDENTITY(1,1) NOT NULL,
    i_Cve_Plantilla INT NOT NULL,
    t_NombreProgramacion VARCHAR(150) NOT NULL,
    t_Frecuencia CHAR(1) NOT NULL, -- D=Diaria, S=Semanal, M=Mensual, U=Única vez
    t_DiasSemana VARCHAR(20) NULL, -- Para frecuencia semanal: 1,3,5 (Lunes, Miércoles, Viernes)
    t_DiaMes INT NULL, -- Para frecuencia mensual: día del mes
    t_HoraEjecucion TIME NOT NULL,
    t_ParametrosFijosJSON NVARCHAR(MAX) NULL, -- Valores fijos para los parámetros de la plantilla
    i_Estatus INT NOT NULL DEFAULT 1, -- 1=Activa, 0=Inactiva, 2=Pausada
    fh_ProximaEjecucion DATETIME NULL,
    fh_UltimaEjecucion DATETIME NULL,
    fh_FechaRegistro DATETIME NOT NULL DEFAULT GETDATE(),
    t_UsuarioRegistro VARCHAR(50) NOT NULL,
    fh_FechaModificacion DATETIME NULL,
    t_UsuarioModificacion VARCHAR(50) NULL,
    CONSTRAINT PK_Enc016ProgramacionReportes PRIMARY KEY (i_Cve_Programacion),
    CONSTRAINT FK_Enc016ProgramacionReportes_Plantilla 
        FOREIGN KEY (i_Cve_Plantilla) 
        REFERENCES [dbo].[Cat016PlantillasReportes](i_Cve_Plantilla)
        ON DELETE CASCADE
);
GO

-- Índice para búsqueda por plantilla
CREATE INDEX IX_Enc016ProgramacionReportes_Plantilla 
ON [dbo].[Enc016ProgramacionReportes] (i_Cve_Plantilla);
GO

-- Índice para próximas ejecuciones (optimización del scheduler)
CREATE INDEX IX_Enc016ProgramacionReportes_ProximaEjecucion 
ON [dbo].[Enc016ProgramacionReportes] (fh_ProximaEjecucion) 
WHERE i_Estatus = 1;
GO

-- =============================================
-- Tabla: Detalle de Destinatarios de Reportes
-- =============================================
CREATE TABLE [dbo].[Det016DestinatariosReportes] (
    i_Cve_Destinatario INT IDENTITY(1,1) NOT NULL,
    i_Cve_Programacion INT NOT NULL,
    t_NombreDestinatario VARCHAR(150) NULL,
    t_Correo VARCHAR(200) NOT NULL,
    t_TipoDestino CHAR(1) NOT NULL DEFAULT 'T', -- T=To, C=CC, B=BCC
    t_TipoNotificacion CHAR(1) NOT NULL DEFAULT 'A', -- A=Ambos, E=Éxito, F=Fallo
    i_Orden INT NOT NULL DEFAULT 0,
    i_Estatus INT NOT NULL DEFAULT 1, -- 1=Activo, 0=Inactivo
    fh_FechaRegistro DATETIME NOT NULL DEFAULT GETDATE(),
    t_UsuarioRegistro VARCHAR(50) NOT NULL,
    CONSTRAINT PK_Det016DestinatariosReportes PRIMARY KEY (i_Cve_Destinatario),
    CONSTRAINT FK_Det016DestinatariosReportes_Programacion 
        FOREIGN KEY (i_Cve_Programacion) 
        REFERENCES [dbo].[Enc016ProgramacionReportes](i_Cve_Programacion)
        ON DELETE CASCADE
);
GO

-- Índice para búsqueda por programación
CREATE INDEX IX_Det016DestinatariosReportes_Programacion 
ON [dbo].[Det016DestinatariosReportes] (i_Cve_Programacion);
GO

-- =============================================
-- Tabla: Bitácora de Ejecuciones de Reportes
-- =============================================
CREATE TABLE [dbo].[Bit016EjecucionesReportes] (
    i_Cve_Ejecucion INT IDENTITY(1,1) NOT NULL,
    i_Cve_Programacion INT NOT NULL,
    fh_InicioEjecucion DATETIME NOT NULL DEFAULT GETDATE(),
    fh_FinEjecucion DATETIME NULL,
    t_Estatus VARCHAR(20) NOT NULL, -- EXITO, ERROR, ADVERTENCIA, VALIDACION_FALLIDA
    i_RegistrosProcesados INT NULL,
    t_RutaDocumento VARCHAR(500) NULL, -- Ruta del archivo generado
    t_IDDocumentoPaperless VARCHAR(50) NULL, -- ID en el sistema Paperless
    t_ErrorDetalle TEXT NULL,
    t_ParametrosUsadosJSON NVARCHAR(MAX) NULL, -- Parámetros usados en esta ejecución
    fh_FechaRegistro DATETIME NOT NULL DEFAULT GETDATE(),
    CONSTRAINT PK_Bit016EjecucionesReportes PRIMARY KEY (i_Cve_Ejecucion),
    CONSTRAINT FK_Bit016EjecucionesReportes_Programacion 
        FOREIGN KEY (i_Cve_Programacion) 
        REFERENCES [dbo].[Enc016ProgramacionReportes](i_Cve_Programacion)
        ON DELETE CASCADE
);
GO

-- Índice para búsqueda por programación
CREATE INDEX IX_Bit016EjecucionesReportes_Programacion 
ON [dbo].[Bit016EjecucionesReportes] (i_Cve_Programacion);
GO

-- Índice para búsqueda por fecha (reportes históricos)
CREATE INDEX IX_Bit016EjecucionesReportes_Fecha 
ON [dbo].[Bit016EjecucionesReportes] (fh_InicioEjecucion);
GO

-- Índice para búsqueda por estatus
CREATE INDEX IX_Bit016EjecucionesReportes_Estatus 
ON [dbo].[Bit016EjecucionesReportes] (t_Estatus);
GO

-- =============================================
-- Datos iniciales de configuración
-- =============================================
INSERT INTO [dbo].[Cng016ConfigAutomatizacion] 
    (t_Clave, t_Valor, t_Descripcion, t_Grupo, t_UsuarioActualizacion)
VALUES
    -- CONFIGURACIÓN DE CORREO
    ('SMTP_SERVER', 'smtp.corporativo.com', 'Servidor SMTP para envío de correos', 'CORREO', 'SISTEMA'),
    ('SMTP_PORT', '587', 'Puerto SMTP', 'CORREO', 'SISTEMA'),
    ('SMTP_USER', 'reportes@krom.com', 'Usuario SMTP', 'CORREO', 'SISTEMA'),
    ('SMTP_SSL', '1', 'Usar SSL (1=Sí, 0=No)', 'CORREO', 'SISTEMA'),
    ('CORREO_REMITENTE', 'reportes@krom.com', 'Correo remitente por defecto', 'CORREO', 'SISTEMA'),
    
    -- RUTAS DEL SISTEMA
    ('RUTA_REPORTES', '\\servidor\reportes\', 'Ruta base para guardar reportes', 'RUTAS', 'SISTEMA'),
    ('RUTA_TEMPORAL', 'C:\Temp\Reportes\', 'Ruta para archivos temporales', 'RUTAS', 'SISTEMA'),
    ('RETENCION_DIAS', '90', 'Días de retención de reportes generados', 'RUTAS', 'SISTEMA'),
    
    -- CONFIGURACIÓN GENERAL
    ('TIEMPO_MAX_EJECUCION', '300', 'Tiempo máximo de ejecución en segundos', 'GENERAL', 'SISTEMA'),
    ('HORA_INICIO_SCHEDULER', '06:00', 'Hora de inicio del scheduler', 'GENERAL', 'SISTEMA'),
    ('HORA_FIN_SCHEDULER', '22:00', 'Hora de fin del scheduler', 'GENERAL', 'SISTEMA'),
    ('LOG_LEVEL', 'INFO', 'Nivel de log (DEBUG, INFO, WARN, ERROR)', 'GENERAL', 'SISTEMA'),
    
    -- INTEGRACIÓN CON PAPERLESS
    ('PAPERLESS_URL', 'http://paperless/api', 'URL de API de Paperless', 'INTEGRACION', 'SISTEMA'),
    ('PAPERLESS_API_KEY', '', 'API Key para Paperless', 'INTEGRACION', 'SISTEMA');
GO

-- =============================================
-- Comentarios descriptivos de las tablas
-- =============================================
EXEC sp_addextendedproperty 
    @name = N'MS_Description', 
    @value = N'Catálogo de plantillas de reportes con consultas SQL y configuración de formato',
    @level0type = N'SCHEMA', @level0name = N'dbo', 
    @level1type = N'TABLE', @level1name = N'Cat016PlantillasReportes';
GO

EXEC sp_addextendedproperty 
    @name = N'MS_Description', 
    @value = N'Encabezado de validaciones aplicables a cada plantilla de reporte',
    @level0type = N'SCHEMA', @level0name = N'dbo', 
    @level1type = N'TABLE', @level1name = N'Enc016ValidacionesReportes';
GO

EXEC sp_addextendedproperty 
    @name = N'MS_Description', 
    @value = N'Configuración global del sistema de automatización de reportes',
    @level0type = N'SCHEMA', @level0name = N'dbo', 
    @level1type = N'TABLE', @level1name = N'Cng016ConfigAutomatizacion';
GO

EXEC sp_addextendedproperty 
    @name = N'MS_Description', 
    @value = N'Encabezado de programaciones de ejecución de reportes',
    @level0type = N'SCHEMA', @level0name = N'dbo', 
    @level1type = N'TABLE', @level1name = N'Enc016ProgramacionReportes';
GO

EXEC sp_addextendedproperty 
    @name = N'MS_Description', 
    @value = N'Detalle de destinatarios por programación de reporte',
    @level0type = N'SCHEMA', @level0name = N'dbo', 
    @level1type = N'TABLE', @level1name = N'Det016DestinatariosReportes';
GO

EXEC sp_addextendedproperty 
    @name = N'MS_Description', 
    @value = N'Bitácora de ejecuciones realizadas por el sistema',
    @level0type = N'SCHEMA', @level0name = N'dbo', 
    @level1type = N'TABLE', @level1name = N'Bit016EjecucionesReportes';
GO

PRINT 'Base de datos del Generador de Reportes Automatizados creada exitosamente.';
PRINT 'Tablas creadas: 6';
PRINT 'Índices creados: 12';
PRINT 'Configuraciones iniciales insertadas: 15';
GO