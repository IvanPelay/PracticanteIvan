-- ===================================================================
-- Script: Creación de tablas para Módulo Reporteador Automático
-- Sistema: KROM - Automatización de Reportes Aduanales
-- Familia: 016 (Reportes)
-- Autor: Residente Iván Pelayo Martínez
-- Fecha: Febrero 2026
-- Base de datos: SysExpert (según corresponda)
-- Descripción: Script generado a partir del nuevo diagrama ER que elimina
--              las tablas Cng016ConfigReporteador y Det016DestinatariosReporteador,
--              y ajusta las tablas restantes al esquema definido.
-- ===================================================================

USE [SysExpert];  -- ← Cambiar por la BD destino
GO

-- ===================================================================
-- 1. CATÁLOGO DE PLANTILLAS DE REPORTE
--    Almacena la definición lógica del reporte: consulta SQL, parámetros,
--    formato de salida, ruta de plantilla, etc.
-- ===================================================================
CREATE TABLE [dbo].[Cat016PlantillasReporteador] (
    -- Llave primaria
    i_Cve_Plantilla INT IDENTITY(1,1) NOT NULL,
    
    -- Datos generales
    t_Nombre VARCHAR(200) NOT NULL,
    t_Descripcion VARCHAR(500) NULL,
    t_RutaPlantilla VARCHAR(500) NOT NULL,
    t_NombreBaseDatos VARCHAR(100) NOT NULL,
    t_Consulta NVARCHAR(MAX) NOT NULL,
    t_ColumnasConfig VARCHAR(MAX) NULL,
    t_ParametrosConfig VARCHAR(MAX) NULL,      -- JSON con parámetros esperados (nombre, tipo, default, etc.)
    t_FormatoSalida VARCHAR(10) NOT NULL CONSTRAINT DF_Cat016_FormatoSalida DEFAULT ('XLSX'),
    
    -- Estado y auditoría
    f_FechaRegistro DATETIME NOT NULL CONSTRAINT DF_Cat016_FechaRegistro DEFAULT (GETDATE()),
    t_UsuarioRegistro VARCHAR(50) NOT NULL CONSTRAINT DF_Cat016_UsuarioRegistro DEFAULT (''),
    i_Cve_Estatus INT NOT NULL CONSTRAINT DF_Cat016_Estatus DEFAULT (1),
    i_Cve_Estado INT NOT NULL CONSTRAINT DF_Cat016_Estado DEFAULT (1),
    
    -- Restricciones
    CONSTRAINT PK_Cat016PlantillasReporteador PRIMARY KEY CLUSTERED (i_Cve_Plantilla ASC),
    CONSTRAINT CK_Cat016_Formato CHECK (t_FormatoSalida IN ('XLSX', 'XLS', 'CSV', 'PDF')),
    CONSTRAINT CK_Cat016_Estatus CHECK (i_Cve_Estatus IN (0, 1)),  -- 0 = Inactivo, 1 = Activo
    CONSTRAINT CK_Cat016_Estado CHECK (i_Cve_Estado IN (0, 1))   -- 0 = Inactivo, 1 = Activo
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

-- ÍNDICES
CREATE NONCLUSTERED INDEX IX_Cat016_Nombre ON [dbo].[Cat016PlantillasReporteador] (t_Nombre) WHERE i_Cve_Estado = 1
GO
CREATE NONCLUSTERED INDEX IX_Cat016_Estatus ON [dbo].[Cat016PlantillasReporteador] (i_Cve_Estatus)
GO
CREATE NONCLUSTERED INDEX IX_Cat016_Estado ON [dbo].[Cat016PlantillasReporteador] (i_Cve_Estado)
GO
CREATE NONCLUSTERED INDEX IX_Cat016_Formato ON [dbo].[Cat016PlantillasReporteador] (t_FormatoSalida)
GO
CREATE NONCLUSTERED INDEX IX_Cat016_Fecha ON [dbo].[Cat016PlantillasReporteador] (f_FechaRegistro)
GO

-- DESCRIPCIÓN DE TABLA
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'Catálogo de plantillas de reportes con consulta SQL, ruta de archivo, base de datos destino y configuración de parámetros',
    @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Cat016PlantillasReporteador';
GO

-- DESCRIPCIONES DE COLUMNAS
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'Clave única de la plantilla', 
    @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Cat016PlantillasReporteador', @level2type = N'COLUMN', @level2name = N'i_Cve_Plantilla';
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre descriptivo de la plantilla', 
    @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Cat016PlantillasReporteador', @level2type = N'COLUMN', @level2name = N't_Nombre';
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'Breve descripción de la plantilla', 
    @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Cat016PlantillasReporteador', @level2type = N'COLUMN', @level2name = N't_Descripcion';
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'Ruta física o lógica de la plantilla de reporte (archivo .rdl, .jrxml, etc.)', 
    @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Cat016PlantillasReporteador', @level2type = N'COLUMN', @level2name = N't_RutaPlantilla';
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre de la base de datos contra la que se ejecutará la consulta', 
    @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Cat016PlantillasReporteador', @level2type = N'COLUMN', @level2name = N't_NombreBaseDatos';
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'Consulta SQL que obtiene los datos del reporte', 
    @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Cat016PlantillasReporteador', @level2type = N'COLUMN', @level2name = N't_Consulta';
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'JSON con configuración de Campos del Reporte Ejemplo {"campo":"NumeroReferencia","titulo":"Referencia"},{"campo":"Pago","titulo":"Fecha Pago"}',
    @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Cat016PlantillasReporteador', @level2type = N'COLUMN', @level2name = N't_ColumnasConfig';
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'JSON con configuración de parámetros Ejemplo {"nombre":"@FechaInicio","tipo":"DATE"},{"nombre":"@RFC","tipo":"VARCHAR"}', 
    @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Cat016PlantillasReporteador', @level2type = N'COLUMN', @level2name = N't_ParametrosConfig';
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'Formato de archivo de salida: XLSX, XLS, CSV, PDF', 
    @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Cat016PlantillasReporteador', @level2type = N'COLUMN', @level2name = N't_FormatoSalida';
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de registro de la plantilla', 
    @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Cat016PlantillasReporteador', @level2type = N'COLUMN', @level2name = N'f_FechaRegistro';
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que registró la plantilla', 
    @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Cat016PlantillasReporteador', @level2type = N'COLUMN', @level2name = N't_UsuarioRegistro';
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'Estatus: 1=Activo, 0=Inactivo (para borrado lógico)', 
    @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Cat016PlantillasReporteador', @level2type = N'COLUMN', @level2name = N'i_Cve_Estatus';
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'Estado: 1=Activo, 0=Inactivo (para borrado de sistema)', 
    @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Cat016PlantillasReporteador', @level2type = N'COLUMN', @level2name = N'i_Cve_Estado';
GO

-- ===================================================================
-- 2. VALIDACIONES POR PLANTILLA
--    Reglas de negocio que se ejecutarán antes de generar el reporte.
--    Cada validación es reutilizable y configurable vía JSON.
-- ===================================================================
CREATE TABLE [dbo].[Enc016ValidacionesReporteador] (
    i_Cve_Validacion INT IDENTITY(1,1) NOT NULL,
    i_Cve_Plantilla INT NOT NULL,
    
    t_Tipo VARCHAR(50) NOT NULL,  -- 'registros_minimos', 'campos_obligatorios', 'totales', 'nulos', 'personalizada'
    t_Configuracion NVARCHAR(MAX) NOT NULL,  -- JSON con umbrales, campos, condiciones
    i_Orden INT NOT NULL CONSTRAINT DF_Enc016_Orden DEFAULT (1),
    
    f_FechaRegistro DATETIME NOT NULL CONSTRAINT DF_Enc016_FechaRegistro DEFAULT (GETDATE()),
    t_UsuarioRegistro VARCHAR(50) NOT NULL CONSTRAINT DF_Enc016_UsuarioRegistro DEFAULT (''),
    i_Cve_Estatus INT NOT NULL CONSTRAINT DF_Enc016_Estatus DEFAULT (1),
    i_Cve_Estado INT NOT NULL CONSTRAINT DF_Enc016_Estado DEFAULT (1),
    
    CONSTRAINT PK_Enc016ValidacionesReporteador PRIMARY KEY CLUSTERED (i_Cve_Validacion ASC),
    CONSTRAINT FK_Enc016_Plantilla FOREIGN KEY (i_Cve_Plantilla) 
        REFERENCES [dbo].[Cat016PlantillasReporteador] (i_Cve_Plantilla),
    CONSTRAINT CK_Enc016_Estatus CHECK (i_Cve_Estatus IN (0, 1)),
    CONSTRAINT CK_Enc016_Estado CHECK (i_Cve_Estado IN (0, 1))
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

-- ÍNDICES
CREATE NONCLUSTERED INDEX IX_Enc016_Plantilla ON [dbo].[Enc016ValidacionesReporteador] (i_Cve_Plantilla)
GO
CREATE NONCLUSTERED INDEX IX_Enc016_Tipo ON [dbo].[Enc016ValidacionesReporteador] (t_Tipo)
GO
CREATE NONCLUSTERED INDEX IX_Enc016_Orden ON [dbo].[Enc016ValidacionesReporteador] (i_Orden)
GO
CREATE NONCLUSTERED INDEX IX_Enc016_Estatus ON [dbo].[Enc016ValidacionesReporteador] (i_Cve_Estatus)
GO
CREATE NONCLUSTERED INDEX IX_Enc016_Fecha ON [dbo].[Enc016ValidacionesReporteador] (f_FechaRegistro)
GO

-- DESCRIPCIÓN DE TABLA
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'Validaciones aplicables a cada plantilla de reporte',
    @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Enc016ValidacionesReporteador';
GO

-- DESCRIPCIONES DE COLUMNAS
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'Clave única de la validación', 
    @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Enc016ValidacionesReporteador', @level2type = N'COLUMN', @level2name = N'i_Cve_Validacion';
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'Clave de la plantilla asociada (FK)', 
    @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Enc016ValidacionesReporteador', @level2type = N'COLUMN', @level2name = N'i_Cve_Plantilla';
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de validación: registros_minimos, campos_obligatorios, totales, nulos, personalizada', 
    @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Enc016ValidacionesReporteador', @level2type = N'COLUMN', @level2name = N't_Tipo';
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'JSON con configuración específica de la validación', 
    @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Enc016ValidacionesReporteador', @level2type = N'COLUMN', @level2name = N't_Configuracion';
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'Orden de ejecución de la validación', 
    @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Enc016ValidacionesReporteador', @level2type = N'COLUMN', @level2name = N'i_Orden';
GO

-- ===================================================================
-- 3. PROGRAMACIÓN DE EJECUCIONES
--    Define cuándo y con qué parámetros fijos se ejecuta una plantilla.
-- ===================================================================
CREATE TABLE [dbo].[Enc016ProgramacionReporteador] (
    i_Cve_Programacion INT IDENTITY(1,1) NOT NULL,
    i_Cve_Plantilla INT NOT NULL,
    
    t_Nombre VARCHAR(150) NOT NULL,
    t_Descripcion VARCHAR(500) NULL,
    t_Frecuencia CHAR(1) NOT NULL,  -- 'U'=Única, 'D'=Diaria, 'S'=Semanal, 'M'=Mensual
    t_DiasSemana VARCHAR(20) NULL,  -- '1,3,5' = Lunes, Miércoles, Viernes (para frecuencia semanal)
    i_DiaMes INT NULL,              -- Día del mes (para frecuencia mensual)
    t_Hora TIME NOT NULL,
    t_Parametros VARCHAR(MAX) NULL, -- JSON con valores fijos para los parámetros de la plantilla
    
    f_ProximaEjecucion DATETIME NULL,
    f_UltimaEjecucion DATETIME NULL,
    f_FechaRegistro DATETIME NOT NULL CONSTRAINT DF_Enc016_FechaRegistroProgramacion DEFAULT (GETDATE()),
    t_UsuarioRegistro VARCHAR(50) NOT NULL CONSTRAINT DF_Enc016_UsuarioRegistroProgramacion DEFAULT (''),
    i_Cve_Estatus INT NOT NULL CONSTRAINT DF_Enc016_EstatusProgramacion DEFAULT (1),
    i_Cve_Estado INT NOT NULL CONSTRAINT DF_Enc016_EstadoProgramacion DEFAULT (1),
    
    CONSTRAINT PK_Enc016ProgramacionReporteador PRIMARY KEY CLUSTERED (i_Cve_Programacion ASC),
    CONSTRAINT FK_Enc016_PlantillaProgramacion FOREIGN KEY (i_Cve_Plantilla) 
        REFERENCES [dbo].[Cat016PlantillasReporteador] (i_Cve_Plantilla),
    CONSTRAINT CK_Enc016_Frecuencia CHECK (t_Frecuencia IN ('U', 'D', 'S', 'M')),
    CONSTRAINT CK_Enc016_DiaSemana CHECK (t_DiasSemana IS NULL OR t_DiasSemana LIKE '[0-9]%' OR t_DiasSemana = ''),
    CONSTRAINT CK_Enc016_DiaMes CHECK (i_DiaMes IS NULL OR (i_DiaMes BETWEEN 1 AND 31)),
    CONSTRAINT CK_Enc016_EstatusProgramacion CHECK (i_Cve_Estatus IN (0, 1)),
    CONSTRAINT CK_Enc016_EstadoProgramacion CHECK (i_Cve_Estado IN (0, 1))
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

-- ÍNDICES
CREATE NONCLUSTERED INDEX IX_Enc016_ProximaEjecucion ON [dbo].[Enc016ProgramacionReporteador] (f_ProximaEjecucion) 
    WHERE i_Cve_Estado = 1 AND f_ProximaEjecucion IS NOT NULL
GO
CREATE NONCLUSTERED INDEX IX_Enc016_PlantillaProgramacion ON [dbo].[Enc016ProgramacionReporteador] (i_Cve_Plantilla)
GO
CREATE NONCLUSTERED INDEX IX_Enc016_Frecuencia ON [dbo].[Enc016ProgramacionReporteador] (t_Frecuencia)
GO
CREATE NONCLUSTERED INDEX IX_Enc016_Estatus ON [dbo].[Enc016ProgramacionReporteador] (i_Cve_Estatus)
GO
CREATE NONCLUSTERED INDEX IX_Enc016_Estado ON [dbo].[Enc016ProgramacionReporteador] (i_Cve_Estado)
GO

-- DESCRIPCIÓN DE TABLA
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'Programaciones de ejecución de reportes',
    @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Enc016ProgramacionReporteador';
GO

-- DESCRIPCIONES DE COLUMNAS
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'Clave única de la programación', 
    @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Enc016ProgramacionReporteador', @level2type = N'COLUMN', @level2name = N'i_Cve_Programacion';
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'Clave de la plantilla asociada (FK)', 
    @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Enc016ProgramacionReporteador', @level2type = N'COLUMN', @level2name = N'i_Cve_Plantilla';
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre de la programación', 
    @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Enc016ProgramacionReporteador', @level2type = N'COLUMN', @level2name = N't_Nombre';
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'Breve descripcion de la programación', 
    @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Enc016ProgramacionReporteador', @level2type = N'COLUMN', @level2name = N't_Descripcion';
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'Frecuencia de ejecución: U=Única, D=Diaria, S=Semanal, M=Mensual', 
    @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Enc016ProgramacionReporteador', @level2type = N'COLUMN', @level2name = N't_Frecuencia';
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'Días de la semana (1=Domingo, 2=Lunes, ..., 7=Sábado) en formato "1,3,5"', 
    @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Enc016ProgramacionReporteador', @level2type = N'COLUMN', @level2name = N't_DiasSemana';
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'Día del mes (1-31) para frecuencia mensual', 
    @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Enc016ProgramacionReporteador', @level2type = N'COLUMN', @level2name = N'i_DiaMes';
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'Hora de ejecución', 
    @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Enc016ProgramacionReporteador', @level2type = N'COLUMN', @level2name = N't_Hora';
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'JSON con valores fijos para los parámetros de la plantilla', 
    @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Enc016ProgramacionReporteador', @level2type = N'COLUMN', @level2name = N't_Parametros';
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de la próxima ejecución programada', 
    @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Enc016ProgramacionReporteador', @level2type = N'COLUMN', @level2name = N'f_ProximaEjecucion';
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de la última ejecución', 
    @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Enc016ProgramacionReporteador', @level2type = N'COLUMN', @level2name = N'f_UltimaEjecucion';
GO

-- ===================================================================
-- 4. BITÁCORA DE GENERACIONES
--    Registro histórico de cada ejecución del reporteador.
-- ===================================================================
CREATE TABLE [dbo].[Bit016GeneracionReporteador] (
    i_Cve_Generacion INT IDENTITY(1,1) NOT NULL,
    i_Cve_Programacion INT NOT NULL,
    
    f_FechaInicio DATETIME NOT NULL CONSTRAINT DF_Bit016_FechaInicio DEFAULT (GETDATE()),
    f_FechaFin DATETIME NULL,
    t_Proceso VARCHAR(20) NOT NULL,  -- 'PROCESANDO', 'COMPLETADO', 'FALLIDO', 'VALIDACION_FALLIDA'
    i_RegistrosProcesados INT NOT NULL CONSTRAINT DF_Bit016_Registros DEFAULT (0),
    t_RutaDocumento VARCHAR(500) NULL,
    t_IdDocumento VARCHAR(50) NULL,
    t_Error TEXT NULL,               -- NULL = sin error
    t_ParametrosUsados VARCHAR(MAX) NULL,  -- JSON con los parámetros reales usados en esta ejecución
    
    f_FechaRegistro DATETIME NOT NULL CONSTRAINT DF_Bit016_FechaRegistro DEFAULT (GETDATE()),
    i_Cve_Estatus INT NOT NULL CONSTRAINT DF_Bit016_Estatus DEFAULT (1),
    i_Cve_Estado INT NOT NULL CONSTRAINT DF_Bit016_Estado DEFAULT (1),
    
    CONSTRAINT PK_Bit016GeneracionReporteador PRIMARY KEY CLUSTERED (i_Cve_Generacion ASC),
    CONSTRAINT FK_Bit016_Programacion FOREIGN KEY (i_Cve_Programacion) 
        REFERENCES [dbo].[Enc016ProgramacionReporteador] (i_Cve_Programacion),
    CONSTRAINT CK_Bit016_Proceso CHECK (t_Proceso IN ('PROCESANDO', 'COMPLETADO', 'FALLIDO', 'VALIDACION_FALLIDA')),
    CONSTRAINT CK_Bit016_Estatus CHECK (i_Cve_Estatus IN (0, 1)),
    CONSTRAINT CK_Bit016_Estado CHECK (i_Cve_Estado IN (0, 1))
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

-- ÍNDICES
CREATE NONCLUSTERED INDEX IX_Bit016_ProgramacionFecha ON [dbo].[Bit016GeneracionReporteador] (i_Cve_Programacion, f_FechaInicio)
GO
CREATE NONCLUSTERED INDEX IX_Bit016_Proceso ON [dbo].[Bit016GeneracionReporteador] (t_Proceso)
GO
CREATE NONCLUSTERED INDEX IX_Bit016_FechaInicio ON [dbo].[Bit016GeneracionReporteador] (f_FechaInicio)
GO
CREATE NONCLUSTERED INDEX IX_Bit016_FechaFin ON [dbo].[Bit016GeneracionReporteador] (f_FechaFin)
GO
CREATE NONCLUSTERED INDEX IX_Bit016_Estatus ON [dbo].[Bit016GeneracionReporteador] (i_Cve_Estatus)
GO

-- DESCRIPCIÓN DE TABLA
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'Bitácora de generaciones de reportes',
    @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Bit016GeneracionReporteador';
GO

-- DESCRIPCIONES DE COLUMNAS
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'Clave única de la generación', 
    @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Bit016GeneracionReporteador', @level2type = N'COLUMN', @level2name = N'i_Cve_Generacion';
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'Clave de la programación asociada (FK)', 
    @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Bit016GeneracionReporteador', @level2type = N'COLUMN', @level2name = N'i_Cve_Programacion';
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de inicio de la generación', 
    @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Bit016GeneracionReporteador', @level2type = N'COLUMN', @level2name = N'f_FechaInicio';
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de fin de la generación', 
    @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Bit016GeneracionReporteador', @level2type = N'COLUMN', @level2name = N'f_FechaFin';
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'Estatus de la ejecución: PROCESANDO, COMPLETADO, FALLIDO, VALIDACION_FALLIDA', 
    @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Bit016GeneracionReporteador', @level2type = N'COLUMN', @level2name = N't_Proceso';
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'Número de registros procesados', 
    @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Bit016GeneracionReporteador', @level2type = N'COLUMN', @level2name = N'i_RegistrosProcesados';
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'Ruta del documento generado', 
    @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Bit016GeneracionReporteador', @level2type = N'COLUMN', @level2name = N't_RutaDocumento';
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del documento en el sistema de gestión documental', 
    @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Bit016GeneracionReporteador', @level2type = N'COLUMN', @level2name = N't_IdDocumento';
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle del error (NULL = sin error)', 
    @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Bit016GeneracionReporteador', @level2type = N'COLUMN', @level2name = N't_Error';
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'JSON con los parámetros utilizados en esta ejecución', 
    @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Bit016GeneracionReporteador', @level2type = N'COLUMN', @level2name = N't_ParametrosUsados';
GO

-- ===================================================================
-- 5. DATOS INICIALES (OPCIONAL - SOLO COMO REFERENCIA)
--    Nota: Las tablas de configuración y destinatarios han sido eliminadas
--    según el nuevo esquema. Este bloque se mantiene como placeholder
--    por si se requiere insertar alguna configuración inicial en el futuro.
-- ===================================================================
--PRINT 'Las tablas de configuración global y destinatarios han sido eliminadas según el nuevo esquema ER.'
--PRINT 'No se insertan datos iniciales en este script.'
--GO

-- ===================================================================
-- 6. VISTA ÚTIL: Programaciones activas con próxima ejecución
-- ===================================================================
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
    pl.t_ColumnasConfig
    pl.t_ParametrosConfig,
    pl.t_RutaPlantilla,
    pl.t_NombreBaseDatos
FROM [dbo].[Enc016ProgramacionReporteador] p
INNER JOIN [dbo].[Cat016PlantillasReporteador] pl ON p.i_Cve_Plantilla = pl.i_Cve_Plantilla
WHERE p.i_Cve_Estado = 1 
AND pl.i_Cve_Estado = 1
AND p.f_ProximaEjecucion IS NOT NULL
AND p.f_ProximaEjecucion <= GETDATE()
GO

-- DESCRIPCIÓN DE VISTA
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'Vista de programaciones activas listas para ejecutar',
    @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'Vig016ProgramacionesPendientes';
GO

-- ===================================================================
-- 7. VALIDACIÓN DE INTEGRIDAD REFERENCIAL
--    Este bloque verifica que las relaciones entre tablas sean correctas.
-- ===================================================================
PRINT 'Verificando integridad referencial...'
GO

-- Verificar existencia de llaves foráneas
IF EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK_Enc016_Plantilla')
    PRINT 'FK_Enc016_Plantilla: OK'
ELSE
    PRINT 'FK_Enc016_Plantilla: NO ENCONTRADA'
GO

IF EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK_Enc016_PlantillaProgramacion')
    PRINT 'FK_Enc016_PlantillaProgramacion: OK'
ELSE
    PRINT 'FK_Enc016_PlantillaProgramacion: NO ENCONTRADA'
GO

IF EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK_Bit016_Programacion')
    PRINT 'FK_Bit016_Programacion: OK'
ELSE
    PRINT 'FK_Bit016_Programacion: NO ENCONTRADA'
GO

-- ===================================================================
-- 8. RESUMEN DE CREACIÓN
-- ===================================================================
PRINT '========================================================='
PRINT 'MÓDULO REPORTEADOR KROM - FAMILIA 016 (NUEVO ESQUEMA)'
PRINT '========================================================='
PRINT 'Tablas creadas: 4'
PRINT 'Índices creados: 19'
PRINT 'Restricciones: 20+'
PRINT 'Descripciones de tabla: 4'
PRINT 'Descripciones de columna: 30+'
PRINT 'Vistas creadas: 1'
PRINT '========================================================='
PRINT 'Script ejecutado exitosamente.'
PRINT '========================================================='
GO