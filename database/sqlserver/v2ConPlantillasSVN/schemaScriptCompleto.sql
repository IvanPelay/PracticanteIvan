-- ===================================================================
-- Script: Creación COMPLETA de BD para Módulo Reporteador Automático
-- Sistema: KROM - Automatización de Reportes Aduanales
-- Familia: 016 (Reportes)
-- Autor: Residente Iván Pelayo Martínez
-- Fecha: Marzo 2026
-- Base de datos: Solium (BD por defecto del CMF)
--
-- VERSIÓN CONSOLIDADA — incluye todos los cambios aplicados:
--   v1: Esquema inicial (Cat016Plantillas, Enc016Validaciones,
--       Enc016Programacion, Bit016Generacion)
--   v2: Se elimina t_Consulta de Cat016Plantillas
--   v3: Nueva tabla Cat016ConsultasReporteador (vista + SP)
--       Se simplifica según recomendación de asesora:
--       Solium es BD por defecto, vistas manejan JOINs internamente
--   v4: Se agrega f_VigenciaInicio / f_VigenciaFin a Enc016Programacion
--       para controlar el período de vida de programaciones recurrentes
--
-- ORDEN DE EJECUCIÓN:
--   1. Cat016ConsultasReporteador  (nuevo catálogo de consultas)
--   2. Cat016PlantillasReporteador (plantillas — sin t_Consulta)
--   3. Enc016ValidacionesReporteador
--   4. Enc016ProgramacionReporteador (con vigencia)
--   5. Bit016GeneracionReporteador
--   6. Vistas operativas (Vig016ProgramacionesPendientes)
--   7. Vistas de trabajo (Vt016...)
--   8. Vistas de entorno (Ve016IU...)
--   9. SP016ProcesarColaReportes
--  10. Índices, constraints, descripciones
--  11. Permisos
-- ===================================================================
 
USE [Solium];
GO
 
-- ===================================================================
-- BLOQUE 0: LIMPIEZA PREVIA (útil para reinstalar en pruebas)
-- Descomentar solo si se necesita recrear desde cero
-- ===================================================================
/*
IF OBJECT_ID('dbo.SP016ProcesarColaReportes','P')       IS NOT NULL DROP PROCEDURE dbo.SP016ProcesarColaReportes
IF OBJECT_ID('dbo.Vig016ProgramacionesPendientes','V')  IS NOT NULL DROP VIEW dbo.Vig016ProgramacionesPendientes
IF OBJECT_ID('dbo.Vt016ConsultasDisponibles','V')       IS NOT NULL DROP VIEW dbo.Vt016ConsultasDisponibles
IF OBJECT_ID('dbo.Vt016Plantillas','V')                 IS NOT NULL DROP VIEW dbo.Vt016Plantillas
IF OBJECT_ID('dbo.Vt016Programacion','V')               IS NOT NULL DROP VIEW dbo.Vt016Programacion
IF OBJECT_ID('dbo.Ve016IUConsultas','V')                IS NOT NULL DROP VIEW dbo.Ve016IUConsultas
IF OBJECT_ID('dbo.Ve016IUPlantillas','V')               IS NOT NULL DROP VIEW dbo.Ve016IUPlantillas
IF OBJECT_ID('dbo.Ve016IUProgramacion','V')             IS NOT NULL DROP VIEW dbo.Ve016IUProgramacion
IF OBJECT_ID('dbo.Bit016GeneracionReporteador','U')     IS NOT NULL DROP TABLE dbo.Bit016GeneracionReporteador
IF OBJECT_ID('dbo.Enc016ProgramacionReporteador','U')   IS NOT NULL DROP TABLE dbo.Enc016ProgramacionReporteador
IF OBJECT_ID('dbo.Enc016ValidacionesReporteador','U')   IS NOT NULL DROP TABLE dbo.Enc016ValidacionesReporteador
IF OBJECT_ID('dbo.Cat016PlantillasReporteador','U')     IS NOT NULL DROP TABLE dbo.Cat016PlantillasReporteador
IF OBJECT_ID('dbo.Cat016ConsultasReporteador','U')      IS NOT NULL DROP TABLE dbo.Cat016ConsultasReporteador
GO
*/
 
-- ===================================================================
-- TABLA 1: Cat016ConsultasReporteador
--   Catálogo de consultas disponibles para reportes.
--   SOLO DBA/Desarrolladores registran aquí directamente en BD.
--   El usuario del sistema solo ve el t_Nombre en un ComboBox.
--
--   t_NombreVista (OBLIGATORIA): vista que hace los JOINs.
--     Puede referenciar otras BDs internamente:
--     SysExpert.dbo.Tabla, Aduana.dbo.Tabla, etc.
--   t_NombreSP (OPCIONAL): si existe el motor lo ejecuta con
--     parámetros; si es NULL el motor consulta la vista directo.
-- ===================================================================
CREATE TABLE [dbo].[Cat016ConsultasReporteador] (
 
    i_Cve_Consulta    INT          IDENTITY(1,1) NOT NULL,
    t_Nombre          VARCHAR(200) NOT NULL,
    t_Descripcion     VARCHAR(500) NULL,
    t_NombreVista     VARCHAR(200) NOT NULL,
    t_NombreSP        VARCHAR(200) NULL,
 
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
    CONSTRAINT CK_Cat016Con_Estatus CHECK (i_Cve_Estatus IN (0,1)),
    CONSTRAINT CK_Cat016Con_Estado  CHECK (i_Cve_Estado  IN (0,1))
 
) ON [PRIMARY]
GO
 
CREATE NONCLUSTERED INDEX IX_Cat016Con_Nombre
    ON [dbo].[Cat016ConsultasReporteador] (t_Nombre) WHERE i_Cve_Estado = 1
GO
CREATE NONCLUSTERED INDEX IX_Cat016Con_Estatus
    ON [dbo].[Cat016ConsultasReporteador] (i_Cve_Estatus)
GO
CREATE NONCLUSTERED INDEX IX_Cat016Con_Estado
    ON [dbo].[Cat016ConsultasReporteador] (i_Cve_Estado)
GO
 
EXEC sp_addextendedproperty @name=N'MS_Description',
    @value=N'Catálogo de consultas para reportes. Solo DBA/Dev registran aquí. ' +
           N't_NombreVista es obligatoria. t_NombreSP es opcional: si existe el motor ' +
           N'lo ejecuta; si es NULL consulta la vista directo.',
    @level0type=N'SCHEMA',@level0name=N'dbo',
    @level1type=N'TABLE', @level1name=N'Cat016ConsultasReporteador'
GO
EXEC sp_addextendedproperty @name=N'MS_Description', @value=N'Clave única de la consulta',
    @level0type=N'SCHEMA',@level0name=N'dbo',@level1type=N'TABLE',@level1name=N'Cat016ConsultasReporteador',@level2type=N'COLUMN',@level2name=N'i_Cve_Consulta'
EXEC sp_addextendedproperty @name=N'MS_Description', @value=N'Nombre visible para el usuario en el ComboBox del form de plantillas',
    @level0type=N'SCHEMA',@level0name=N'dbo',@level1type=N'TABLE',@level1name=N'Cat016ConsultasReporteador',@level2type=N'COLUMN',@level2name=N't_Nombre'
EXEC sp_addextendedproperty @name=N'MS_Description', @value=N'Vista base OBLIGATORIA. Maneja JOINs a otras BDs internamente. Ej: VT016OperacionesBase',
    @level0type=N'SCHEMA',@level0name=N'dbo',@level1type=N'TABLE',@level1name=N'Cat016ConsultasReporteador',@level2type=N'COLUMN',@level2name=N't_NombreVista'
EXEC sp_addextendedproperty @name=N'MS_Description', @value=N'SP OPCIONAL. Si existe el motor lo ejecuta con parámetros. Si es NULL usa la vista directo. Ej: spVT016OperacionesDetalle',
    @level0type=N'SCHEMA',@level0name=N'dbo',@level1type=N'TABLE',@level1name=N'Cat016ConsultasReporteador',@level2type=N'COLUMN',@level2name=N't_NombreSP'
GO
 
-- ===================================================================
-- TABLA 2: Cat016PlantillasReporteador
--   Definición del reporte: qué consulta usar, qué plantilla XLSX,
--   configuración de columnas y parámetros.
--   NO almacena SQL en bruto (eliminado desde v2).
-- ===================================================================
CREATE TABLE [dbo].[Cat016PlantillasReporteador] (
 
    i_Cve_Plantilla   INT          IDENTITY(1,1) NOT NULL,
    i_Cve_Consulta    INT          NOT NULL,            -- FK → Cat016ConsultasReporteador
 
    t_Nombre          VARCHAR(200) NOT NULL,
    t_Descripcion     VARCHAR(500) NULL,
    t_RutaPlantilla   VARCHAR(500) NOT NULL,
    t_NombreCliente   VARCHAR(100) NOT NULL,
    t_ColumnasConfig  VARCHAR(MAX) NULL,                -- JSON con config de columnas
    t_ParametrosConfig VARCHAR(MAX) NULL,               -- JSON con definición de parámetros
    t_FormatoSalida   VARCHAR(10)  NOT NULL CONSTRAINT DF_Cat016Pla_Formato  DEFAULT ('XLSX'),
 
    f_FechaRegistro   DATETIME     NOT NULL CONSTRAINT DF_Cat016Pla_Fecha    DEFAULT (GETDATE()),
    t_UsuarioRegistro VARCHAR(50)  NOT NULL CONSTRAINT DF_Cat016Pla_Usuario  DEFAULT (''),
    i_Cve_Estatus     INT          NOT NULL CONSTRAINT DF_Cat016Pla_Estatus  DEFAULT (1),
    i_Cve_Estado      INT          NOT NULL CONSTRAINT DF_Cat016Pla_Estado   DEFAULT (1),
 
    CONSTRAINT PK_Cat016PlantillasReporteador
        PRIMARY KEY CLUSTERED (i_Cve_Plantilla ASC),
    CONSTRAINT FK_Cat016Pla_Consulta
        FOREIGN KEY (i_Cve_Consulta)
        REFERENCES [dbo].[Cat016ConsultasReporteador] (i_Cve_Consulta),
    CONSTRAINT CK_Cat016Pla_Formato
        CHECK (t_FormatoSalida IN ('XLSX','XLS','CSV','PDF')),
    CONSTRAINT CK_Cat016Pla_Estatus CHECK (i_Cve_Estatus IN (0,1)),
    CONSTRAINT CK_Cat016Pla_Estado  CHECK (i_Cve_Estado  IN (0,1))
 
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
 
CREATE NONCLUSTERED INDEX IX_Cat016Pla_Nombre
    ON [dbo].[Cat016PlantillasReporteador] (t_Nombre) WHERE i_Cve_Estado = 1
GO
CREATE NONCLUSTERED INDEX IX_Cat016Pla_Consulta
    ON [dbo].[Cat016PlantillasReporteador] (i_Cve_Consulta)
GO
CREATE NONCLUSTERED INDEX IX_Cat016Pla_Estatus
    ON [dbo].[Cat016PlantillasReporteador] (i_Cve_Estatus)
GO
CREATE NONCLUSTERED INDEX IX_Cat016Pla_Estado
    ON [dbo].[Cat016PlantillasReporteador] (i_Cve_Estado)
GO
CREATE NONCLUSTERED INDEX IX_Cat016Pla_NombreCliente
    ON [dbo].[Cat016PlantillasReporteador] (t_NombreCliente)
GO
CREATE NONCLUSTERED INDEX IX_Cat016Pla_Formato
    ON [dbo].[Cat016PlantillasReporteador] (t_FormatoSalida)
GO
 
EXEC sp_addextendedproperty @name=N'MS_Description',
    @value=N'Catálogo de plantillas de reportes. Referencia a Cat016ConsultasReporteador ' +
           N'en lugar de almacenar SQL en bruto. Incluye configuración de columnas y parámetros en JSON.',
    @level0type=N'SCHEMA',@level0name=N'dbo',
    @level1type=N'TABLE', @level1name=N'Cat016PlantillasReporteador'
GO
EXEC sp_addextendedproperty @name=N'MS_Description', @value=N'FK a Cat016ConsultasReporteador. Indica qué vista/SP usar.',
    @level0type=N'SCHEMA',@level0name=N'dbo',@level1type=N'TABLE',@level1name=N'Cat016PlantillasReporteador',@level2type=N'COLUMN',@level2name=N'i_Cve_Consulta'
EXEC sp_addextendedproperty @name=N'MS_Description', @value=N'JSON con config de columnas. Ej: [{"campo":"NumReferencia","titulo":"Referencia","formato":0,"orden":1}]',
    @level0type=N'SCHEMA',@level0name=N'dbo',@level1type=N'TABLE',@level1name=N'Cat016PlantillasReporteador',@level2type=N'COLUMN',@level2name=N't_ColumnasConfig'
EXEC sp_addextendedproperty @name=N'MS_Description', @value=N'JSON con definición de parámetros. Ej: [{"nombre":"@FechaInicio","tipo":"Date","etiqueta":"Fecha inicio","requerido":true,"valorDefault":"INICIO_PERIODO","orden":1}]',
    @level0type=N'SCHEMA',@level0name=N'dbo',@level1type=N'TABLE',@level1name=N'Cat016PlantillasReporteador',@level2type=N'COLUMN',@level2name=N't_ParametrosConfig'
GO
 
-- ===================================================================
-- TABLA 3: Enc016ValidacionesReporteador
--   Reglas de negocio por plantilla (para uso futuro).
-- ===================================================================
CREATE TABLE [dbo].[Enc016ValidacionesReporteador] (
 
    i_Cve_Validacion  INT          IDENTITY(1,1) NOT NULL,
    i_Cve_Plantilla   INT          NOT NULL,
 
    t_Tipo            VARCHAR(50)  NOT NULL,
    t_Configuracion   NVARCHAR(MAX) NOT NULL,
    i_Orden           INT          NOT NULL CONSTRAINT DF_Enc016Val_Orden    DEFAULT (1),
 
    f_FechaRegistro   DATETIME     NOT NULL CONSTRAINT DF_Enc016Val_Fecha    DEFAULT (GETDATE()),
    t_UsuarioRegistro VARCHAR(50)  NOT NULL CONSTRAINT DF_Enc016Val_Usuario  DEFAULT (''),
    i_Cve_Estatus     INT          NOT NULL CONSTRAINT DF_Enc016Val_Estatus  DEFAULT (1),
    i_Cve_Estado      INT          NOT NULL CONSTRAINT DF_Enc016Val_Estado   DEFAULT (1),
 
    CONSTRAINT PK_Enc016ValidacionesReporteador
        PRIMARY KEY CLUSTERED (i_Cve_Validacion ASC),
    CONSTRAINT FK_Enc016Val_Plantilla
        FOREIGN KEY (i_Cve_Plantilla)
        REFERENCES [dbo].[Cat016PlantillasReporteador] (i_Cve_Plantilla),
    CONSTRAINT CK_Enc016Val_Estatus CHECK (i_Cve_Estatus IN (0,1)),
    CONSTRAINT CK_Enc016Val_Estado  CHECK (i_Cve_Estado  IN (0,1))
 
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
 
CREATE NONCLUSTERED INDEX IX_Enc016Val_Plantilla ON [dbo].[Enc016ValidacionesReporteador] (i_Cve_Plantilla)
CREATE NONCLUSTERED INDEX IX_Enc016Val_Tipo      ON [dbo].[Enc016ValidacionesReporteador] (t_Tipo)
CREATE NONCLUSTERED INDEX IX_Enc016Val_Orden     ON [dbo].[Enc016ValidacionesReporteador] (i_Orden)
GO
 
EXEC sp_addextendedproperty @name=N'MS_Description',
    @value=N'Validaciones aplicables a cada plantilla de reporte (uso futuro).',
    @level0type=N'SCHEMA',@level0name=N'dbo',
    @level1type=N'TABLE', @level1name=N'Enc016ValidacionesReporteador'
GO
 
-- ===================================================================
-- TABLA 4: Enc016ProgramacionReporteador
--   Define cuándo ejecutar una plantilla y con qué parámetros.
--   Incluye f_VigenciaInicio / f_VigenciaFin para controlar
--   el período de vida de programaciones recurrentes.
--
--   VIGENCIA:
--   f_VigenciaInicio: obligatoria, default = GETDATE()
--   f_VigenciaFin:    obligatoria para D/S/M/Q, opcional para U
--                     NULL = sin límite (solo frecuencia U)
--   Al alcanzar f_VigenciaFin el SP pone f_ProximaEjecucion = NULL.
--   La programación queda activa pero sin próxima ejecución.
--
--   PARÁMETROS (t_Parametros JSON):
--   Valores reales para los parámetros de la consulta.
--   Para parámetros de fecha se pueden usar expresiones dinámicas:
--     INICIO_PERIODO / FIN_PERIODO  → rango caído según frecuencia
--     INICIO_SEMANA  / FIN_SEMANA   → lunes-domingo semana anterior
--     INICIO_MES     / FIN_MES      → mes anterior completo
--     INICIO_QUINCENA/ FIN_QUINCENA → quincena anterior
--     HOY, HOY-N, HOY+N             → fecha relativa
--     2026-01-01                    → fecha fija (frecuencia U)
-- ===================================================================
CREATE TABLE [dbo].[Enc016ProgramacionReporteador] (
 
    i_Cve_Programacion INT          IDENTITY(1,1) NOT NULL,
    i_Cve_Plantilla    INT          NOT NULL,
 
    t_Nombre           VARCHAR(150) NOT NULL,
    t_Descripcion      VARCHAR(500) NULL,
    t_Frecuencia       CHAR(1)      NOT NULL,  -- U=Única D=Diaria S=Semanal M=Mensual Q=Quincenal
    t_DiasSemana       VARCHAR(20)  NULL,       -- '1,3,5' para semanal
    i_DiaMes           INT          NULL,       -- 1-31 para mensual
 
    t_Hora             TIME         NOT NULL,
    t_Parametros       VARCHAR(MAX) NULL,       -- JSON con valores (puede incluir expresiones dinámicas)
 
    -- Vigencia de la programación
    f_VigenciaInicio   DATETIME     NOT NULL CONSTRAINT DF_Enc016Pro_VigIni  DEFAULT (GETDATE()),
    f_VigenciaFin      DATETIME     NULL,       -- NULL solo permitido en frecuencia U
 
    -- Control de ejecución
    f_ProximaEjecucion DATETIME     NULL,
    f_UltimaEjecucion  DATETIME     NULL,
 
    f_FechaRegistro    DATETIME     NOT NULL CONSTRAINT DF_Enc016Pro_Fecha   DEFAULT (GETDATE()),
    t_UsuarioRegistro  VARCHAR(50)  NOT NULL CONSTRAINT DF_Enc016Pro_Usuario DEFAULT (''),
    i_Cve_Estatus      INT          NOT NULL CONSTRAINT DF_Enc016Pro_Estatus DEFAULT (1),
    i_Cve_Estado       INT          NOT NULL CONSTRAINT DF_Enc016Pro_Estado  DEFAULT (1),
 
    CONSTRAINT PK_Enc016ProgramacionReporteador
        PRIMARY KEY CLUSTERED (i_Cve_Programacion ASC),
    CONSTRAINT FK_Enc016Pro_Plantilla
        FOREIGN KEY (i_Cve_Plantilla)
        REFERENCES [dbo].[Cat016PlantillasReporteador] (i_Cve_Plantilla),
    CONSTRAINT CK_Enc016Pro_Frecuencia
        CHECK (t_Frecuencia IN ('U','D','S','M','Q')),
    CONSTRAINT CK_Enc016Pro_DiasSemana
        CHECK (t_DiasSemana IS NULL OR t_DiasSemana LIKE '[0-9]%' OR t_DiasSemana = ''),
    CONSTRAINT CK_Enc016Pro_DiaMes
        CHECK (i_DiaMes IS NULL OR (i_DiaMes BETWEEN 1 AND 31)),
    CONSTRAINT CK_Enc016Pro_Estatus CHECK (i_Cve_Estatus IN (0,1)),
    CONSTRAINT CK_Enc016Pro_Estado  CHECK (i_Cve_Estado  IN (0,1))
 
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
 
CREATE NONCLUSTERED INDEX IX_Enc016Pro_ProximaEjecucion
    ON [dbo].[Enc016ProgramacionReporteador] (f_ProximaEjecucion)
    WHERE i_Cve_Estado = 1 AND f_ProximaEjecucion IS NOT NULL
GO
CREATE NONCLUSTERED INDEX IX_Enc016Pro_Plantilla  ON [dbo].[Enc016ProgramacionReporteador] (i_Cve_Plantilla)
CREATE NONCLUSTERED INDEX IX_Enc016Pro_Frecuencia ON [dbo].[Enc016ProgramacionReporteador] (t_Frecuencia)
CREATE NONCLUSTERED INDEX IX_Enc016Pro_Estatus    ON [dbo].[Enc016ProgramacionReporteador] (i_Cve_Estatus)
CREATE NONCLUSTERED INDEX IX_Enc016Pro_Estado     ON [dbo].[Enc016ProgramacionReporteador] (i_Cve_Estado)
CREATE NONCLUSTERED INDEX IX_Enc016Pro_VigenciaFin
    ON [dbo].[Enc016ProgramacionReporteador] (f_VigenciaFin)
    WHERE f_VigenciaFin IS NOT NULL
GO
 
EXEC sp_addextendedproperty @name=N'MS_Description',
    @value=N'Programaciones de ejecución de reportes. Incluye vigencia para controlar ' +
           N'el período de vida de recurrentes. t_Parametros acepta expresiones dinámicas de fecha.',
    @level0type=N'SCHEMA',@level0name=N'dbo',
    @level1type=N'TABLE', @level1name=N'Enc016ProgramacionReporteador'
GO
EXEC sp_addextendedproperty @name=N'MS_Description',
    @value=N'Fecha desde la que la programación está activa. No ejecuta antes de esta fecha.',
    @level0type=N'SCHEMA',@level0name=N'dbo',@level1type=N'TABLE',@level1name=N'Enc016ProgramacionReporteador',@level2type=N'COLUMN',@level2name=N'f_VigenciaInicio'
EXEC sp_addextendedproperty @name=N'MS_Description',
    @value=N'Fecha hasta la que la programación es válida. Obligatoria para D/S/M/Q. ' +
           N'NULL solo permitido en U. Al superarse el SP pone f_ProximaEjecucion = NULL.',
    @level0type=N'SCHEMA',@level0name=N'dbo',@level1type=N'TABLE',@level1name=N'Enc016ProgramacionReporteador',@level2type=N'COLUMN',@level2name=N'f_VigenciaFin'
EXEC sp_addextendedproperty @name=N'MS_Description',
    @value=N'JSON con valores de parámetros para la consulta. Acepta expresiones dinámicas de fecha: ' +
           N'INICIO_PERIODO, FIN_PERIODO, INICIO_SEMANA, FIN_SEMANA, INICIO_MES, FIN_MES, ' +
           N'INICIO_QUINCENA, FIN_QUINCENA, HOY, HOY-N, HOY+N, o fechas fijas (2026-01-01).',
    @level0type=N'SCHEMA',@level0name=N'dbo',@level1type=N'TABLE',@level1name=N'Enc016ProgramacionReporteador',@level2type=N'COLUMN',@level2name=N't_Parametros'
GO
 
-- ===================================================================
-- TABLA 5: Bit016GeneracionReporteador
--   Historial de cada ejecución del motor.
-- ===================================================================
CREATE TABLE [dbo].[Bit016GeneracionReporteador] (
 
    i_Cve_Generacion       INT          IDENTITY(1,1) NOT NULL,
    i_Cve_Programacion     INT          NOT NULL,
 
    f_FechaInicio          DATETIME     NOT NULL CONSTRAINT DF_Bit016_FechaInicio DEFAULT (GETDATE()),
    f_FechaFin             DATETIME     NULL,
    t_Proceso              VARCHAR(20)  NOT NULL,  -- PROCESANDO COMPLETADO FALLIDO VALIDACION_FALLIDA
    i_RegistrosProcesados  INT          NOT NULL CONSTRAINT DF_Bit016_Registros   DEFAULT (0),
    t_RutaDocumento        VARCHAR(500) NULL,
    t_IdDocumento          VARCHAR(50)  NULL,
    t_Error                TEXT         NULL,
    t_ParametrosUsados     VARCHAR(MAX) NULL,       -- JSON con los parámetros reales usados (ya resueltos)
 
    f_FechaRegistro        DATETIME     NOT NULL CONSTRAINT DF_Bit016_Fecha       DEFAULT (GETDATE()),
    t_UsuarioRegistro      VARCHAR(50)  NOT NULL CONSTRAINT DF_Bit016_Usuario     DEFAULT (''),
    i_Cve_Estatus          INT          NOT NULL CONSTRAINT DF_Bit016_Estatus     DEFAULT (1),
    i_Cve_Estado           INT          NOT NULL CONSTRAINT DF_Bit016_Estado      DEFAULT (1),
 
    CONSTRAINT PK_Bit016GeneracionReporteador
        PRIMARY KEY CLUSTERED (i_Cve_Generacion ASC),
    CONSTRAINT FK_Bit016_Programacion
        FOREIGN KEY (i_Cve_Programacion)
        REFERENCES [dbo].[Enc016ProgramacionReporteador] (i_Cve_Programacion),
    CONSTRAINT CK_Bit016_Proceso
        CHECK (t_Proceso IN ('PROCESANDO','COMPLETADO','FALLIDO','VALIDACION_FALLIDA')),
    CONSTRAINT CK_Bit016_Estatus CHECK (i_Cve_Estatus IN (0,1)),
    CONSTRAINT CK_Bit016_Estado  CHECK (i_Cve_Estado  IN (0,1))
 
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
 
CREATE NONCLUSTERED INDEX IX_Bit016_ProgramacionFecha ON [dbo].[Bit016GeneracionReporteador] (i_Cve_Programacion, f_FechaInicio)
CREATE NONCLUSTERED INDEX IX_Bit016_Proceso           ON [dbo].[Bit016GeneracionReporteador] (t_Proceso)
CREATE NONCLUSTERED INDEX IX_Bit016_FechaInicio       ON [dbo].[Bit016GeneracionReporteador] (f_FechaInicio)
CREATE NONCLUSTERED INDEX IX_Bit016_FechaFin          ON [dbo].[Bit016GeneracionReporteador] (f_FechaFin)
CREATE NONCLUSTERED INDEX IX_Bit016_Estatus           ON [dbo].[Bit016GeneracionReporteador] (i_Cve_Estatus)
GO
 
EXEC sp_addextendedproperty @name=N'MS_Description',
    @value=N'Bitácora de generaciones de reportes. t_ParametrosUsados guarda los valores ' +
           N'reales usados en cada ejecución (expresiones ya resueltas a fechas concretas).',
    @level0type=N'SCHEMA',@level0name=N'dbo',
    @level1type=N'TABLE', @level1name=N'Bit016GeneracionReporteador'
GO
 
-- ===================================================================
-- VISTA 1: Vig016ProgramacionesPendientes
--   Programaciones activas listas para ejecutar.
--   Filtra por vigencia activa: HOY >= f_VigenciaInicio
--                               HOY <= f_VigenciaFin (o f_VigenciaFin IS NULL)
-- ===================================================================
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
    con.t_NombreSP     -- NULL = motor usa vista directo
FROM [dbo].[Enc016ProgramacionReporteador]  prog
INNER JOIN [dbo].[Cat016PlantillasReporteador]  pl  ON pl.i_Cve_Plantilla = prog.i_Cve_Plantilla
INNER JOIN [dbo].[Cat016ConsultasReporteador]   con ON con.i_Cve_Consulta  = pl.i_Cve_Consulta
WHERE prog.i_Cve_Estado = 1
  AND pl.i_Cve_Estado   = 1
  AND con.i_Cve_Estado  = 1
  AND prog.f_ProximaEjecucion IS NOT NULL
  AND prog.f_ProximaEjecucion <= GETDATE()
  AND GETDATE() >= prog.f_VigenciaInicio
  AND (prog.f_VigenciaFin IS NULL OR GETDATE() <= prog.f_VigenciaFin)
GO
 
EXEC sp_addextendedproperty @name=N'MS_Description',
    @value=N'Programaciones activas listas para ejecutar. Filtra por vigencia activa.',
    @level0type=N'SCHEMA',@level0name=N'dbo',
    @level1type=N'VIEW',  @level1name=N'Vig016ProgramacionesPendientes'
GO
 
-- ===================================================================
-- VISTA 2: Vt016ConsultasDisponibles
--   Para el ComboBox del form de plantillas.
--   No expone datos internos innecesarios.
-- ===================================================================
CREATE VIEW [dbo].[Vt016ConsultasDisponibles] AS
SELECT
    i_Cve_Consulta,
    t_Nombre,
    t_Descripcion,
    t_NombreVista,
    CASE WHEN t_NombreSP IS NULL THEN 'No' ELSE 'Sí' END AS t_TieneSP,
    t_NombreSP
FROM [dbo].[Cat016ConsultasReporteador]
WHERE i_Cve_Estado  = 1
  AND i_Cve_Estatus = 1
GO
 
-- ===================================================================
-- VISTA 3: Vt016Plantillas
--   Vista de trabajo de plantillas con datos de consulta incluidos.
-- ===================================================================
CREATE VIEW [dbo].[Vt016Plantillas] AS
SELECT
    p.i_Cve_Plantilla,
    p.i_Cve_Consulta,
    c.t_Nombre        AS t_NombreConsulta,
    c.t_NombreVista,
    c.t_NombreSP,
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
FROM [dbo].[Cat016PlantillasReporteador]     p
INNER JOIN [dbo].[Cat016ConsultasReporteador] c ON c.i_Cve_Consulta = p.i_Cve_Consulta
WHERE p.i_Cve_Estado = 1
GO
 
-- ===================================================================
-- VISTA 4: Vt016Programacion
--   Vista de trabajo de programaciones con descripción de frecuencia.
-- ===================================================================
CREATE VIEW [dbo].[Vt016Programacion] AS
SELECT
    p.i_Cve_Programacion,
    p.i_Cve_Plantilla,
    pl.t_Nombre AS t_NombrePlantilla,
    p.t_Nombre  AS t_NombreProgramacion,
    CASE p.t_Frecuencia
        WHEN 'U' THEN 'Única'
        WHEN 'D' THEN 'Diaria'
        WHEN 'S' THEN 'Semanal'
        WHEN 'M' THEN 'Mensual'
        WHEN 'Q' THEN 'Quincenal'
    END AS t_FrecuenciaDesc,
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
 
-- ===================================================================
-- VISTA 5: Ve016IUConsultas (solo DBA)
-- ===================================================================
CREATE VIEW [dbo].[Ve016IUConsultas] AS
SELECT * FROM (
    SELECT 'i_Cve_Consulta' AS Nombre, 1 AS Llave, 11  AS Longitud, 0 AS TipoDato, 1 AS Visible, 'Clave Consulta'           AS NombreColumna, 0 AS PuedeInsertar, 0 AS PuedeModificar, ''  AS ValorDefault, '1' AS TipoFiltro
    UNION ALL SELECT 't_Nombre',         0, 200, 1, 1, 'Nombre Consulta',        1, 1, '',  '2'
    UNION ALL SELECT 't_Descripcion',    0, 500, 1, 1, 'Descripción',            1, 1, '',  '0'
    UNION ALL SELECT 't_NombreVista',    0, 200, 1, 1, 'Vista Base',             1, 1, '',  '2'
    UNION ALL SELECT 't_NombreSP',       0, 200, 1, 1, 'Stored Procedure',       1, 1, '',  '2'
    UNION ALL SELECT 'f_FechaRegistro',  0, 23,  4, 1, 'Fecha Registro',         0, 0, '',  '2'
    UNION ALL SELECT 't_UsuarioRegistro',0, 50,  1, 1, 'Usuario Registro',       0, 0, '',  '2'
    UNION ALL SELECT 't_Estatus',        0, 10,  1, 1, 'Estatus',                1, 1, '',  '2'
    UNION ALL SELECT 't_Estado',         0, 10,  1, 1, 'Estado',                 1, 1, '',  '2'
) AS VE
GO
 
-- ===================================================================
-- VISTA 6: Ve016IUPlantillas
-- ===================================================================
CREATE VIEW [dbo].[Ve016IUPlantillas] AS
SELECT * FROM (
    SELECT 'i_Cve_Plantilla'   AS Nombre, 1 AS Llave, 11          AS Longitud, 0 AS TipoDato, 1 AS Visible, 'Clave Plantilla'          AS NombreColumna, 0 AS PuedeInsertar, 0 AS PuedeModificar, ''     AS ValorDefault, '1' AS TipoFiltro
    UNION ALL SELECT 'i_Cve_Consulta',     0, 11,         0, 1, 'Consulta/Reporte',                           1,                 1,                 '',              '2'
    UNION ALL SELECT 't_NombreConsulta',   0, 200,        1, 1, 'Nombre Consulta',                            0,                 0,                 '',              '2'
    UNION ALL SELECT 't_NombreVista',      0, 200,        1, 1, 'Vista Base',                                 0,                 0,                 '',              '2'
    UNION ALL SELECT 't_NombreSP',         0, 200,        1, 1, 'Stored Procedure',                           0,                 0,                 '',              '2'
    UNION ALL SELECT 't_Nombre',           0, 200,        1, 1, 'Nombre Plantilla',                           1,                 1,                 '',              '2'
    UNION ALL SELECT 't_Descripcion',      0, 1000,       1, 1, 'Descripción Plantilla',                      1,                 1,                 '',              '0'
    UNION ALL SELECT 't_RutaPlantilla',    0, 500,        1, 1, 'Ruta Plantilla',                             1,                 1,                 '',              '2'
    UNION ALL SELECT 't_NombreCliente',    0, 100,        1, 1, 'Cliente/RFC',                                1,                 1,                 '',              '2'
    UNION ALL SELECT 't_ColumnasConfig',   0, 2147483647, 1, 1, 'Config. Columnas (JSON)',                    1,                 1,                 '',              '0'
    UNION ALL SELECT 't_ParametrosConfig', 0, 2147483647, 1, 1, 'Config. Parámetros (JSON)',                  1,                 1,                 '',              '0'
    UNION ALL SELECT 't_FormatoSalida',    0, 10,         1, 1, 'Formato Salida',                             1,                 1,                 'XLSX',          '2'
    UNION ALL SELECT 'f_FechaRegistro',    0, 23,         4, 1, 'Fecha Registro',                             0,                 0,                 '',              '2'
    UNION ALL SELECT 't_UsuarioRegistro',  0, 50,         1, 1, 'Usuario Registro',                           0,                 0,                 '',              '2'
    UNION ALL SELECT 't_Estatus',          0, 10,         1, 1, 'Estatus',                                    1,                 1,                 '',              '2'
    UNION ALL SELECT 't_Estado',           0, 10,         1, 1, 'Estado',                                     1,                 1,                 '',              '2'
) AS VE
GO
 
-- ===================================================================
-- VISTA 7: Ve016IUProgramacion
-- ===================================================================
CREATE VIEW [dbo].[Ve016IUProgramacion] AS
SELECT * FROM (
    SELECT 'i_Cve_Programacion'  AS Nombre, 1 AS Llave, 11 AS Longitud, 0 AS TipoDato, 1 AS Visible, 'Clave Programación'    AS NombreColumna, 0 AS PuedeInsertar, 0 AS PuedeModificar, ''   AS ValorDefault, '1' AS TipoFiltro
    UNION ALL SELECT 'i_Cve_Plantilla',       0, 11,  0, 1, 'Clave Plantilla',        1, 1, '',   '2'
    UNION ALL SELECT 't_NombrePlantilla',      0, 200, 1, 1, 'Plantilla',              0, 0, '',   '2'
    UNION ALL SELECT 't_NombreProgramacion',   0, 150, 1, 1, 'Nombre Programación',    1, 1, '',   '2'
    UNION ALL SELECT 't_FrecuenciaDesc',       0, 20,  1, 1, 'Frecuencia',             1, 1, '',   '2'
    UNION ALL SELECT 't_DiasSemana',           0, 20,  1, 1, 'Días Semana',            1, 1, '',   '2'
    UNION ALL SELECT 'i_DiaMes',               0, 2,   0, 1, 'Día Mes',                1, 1, '',   '2'
    UNION ALL SELECT 't_Hora',                 0, 8,   4, 1, 'Hora',                   1, 1, '',   '2'
    UNION ALL SELECT 't_Parametros',           0, 2147483647, 1, 1, 'Parámetros (JSON)', 1, 1, '', '2'
    UNION ALL SELECT 'f_VigenciaInicio',       0, 23,  4, 1, 'Vigencia Inicio',        1, 1, '',   '2'
    UNION ALL SELECT 'f_VigenciaFin',          0, 23,  4, 1, 'Vigencia Fin',           1, 1, '',   '2'
    UNION ALL SELECT 'f_ProximaEjecucion',     0, 23,  4, 1, 'Próxima Ejecución',      0, 0, '',   '2'
    UNION ALL SELECT 'f_UltimaEjecucion',      0, 23,  4, 1, 'Última Ejecución',       0, 0, '',   '2'
    UNION ALL SELECT 'f_FechaRegistro',        0, 23,  4, 1, 'Fecha Registro',         0, 0, '',   '2'
    UNION ALL SELECT 't_UsuarioRegistro',      0, 50,  1, 1, 'Usuario Registro',       0, 0, '',   '2'
    UNION ALL SELECT 't_Estatus',              0, 10,  1, 1, 'Estatus',                0, 0, '',   '2'
    UNION ALL SELECT 't_Estado',               0, 10,  1, 1, 'Estado',                 0, 0, '',   '2'
) AS VE
GO
 
-- ===================================================================
-- SP: SP016ProcesarColaReportes
--   Identifica programaciones pendientes dentro de su vigencia,
--   las aparta en bitácora y calcula la siguiente fecha.
--   Si la próxima ejecución supera f_VigenciaFin → pone NULL.
-- ===================================================================
CREATE PROCEDURE [dbo].[SP016ProcesarColaReportes]
    @UsuarioSistema VARCHAR(50) = 'MOTOR_REPORTEADOR'
AS
/*
v4 - Consolida todos los cambios:
  - Lee desde Vig016ProgramacionesPendientes (que ya filtra por vigencia)
  - Calcula próxima ejecución; si supera f_VigenciaFin pone NULL
  - Devuelve t_NombreVista y t_NombreSP (sin SQL en bruto)
  - Devuelve t_Frecuencia para que el motor calcule INICIO/FIN_PERIODO
  - Devuelve f_VigenciaInicio y f_VigenciaFin para trazabilidad
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
 
            -- Calcular próxima ejecución respetando f_VigenciaFin
            UPDATE prog
            SET
                prog.f_UltimaEjecucion  = tmp.f_ProximaEjecucion,
                prog.f_ProximaEjecucion =
                    CASE
                        -- Si la siguiente fecha calculada supera f_VigenciaFin → NULL
                        WHEN tmp.f_VigenciaFin IS NOT NULL AND
                             CASE prog.t_Frecuencia
                                 WHEN 'D' THEN DATEADD(DAY,    1,  tmp.f_ProximaEjecucion)
                                 WHEN 'S' THEN DATEADD(DAY,    7,  tmp.f_ProximaEjecucion)
                                 WHEN 'Q' THEN DATEADD(DAY,   15,  tmp.f_ProximaEjecucion)
                                 WHEN 'M' THEN DATEADD(MONTH,  1,  tmp.f_ProximaEjecucion)
                                 ELSE NULL
                             END > tmp.f_VigenciaFin
                        THEN NULL
                        -- Cálculo normal por frecuencia
                        WHEN prog.t_Frecuencia = 'D' THEN DATEADD(DAY,   1, tmp.f_ProximaEjecucion)
                        WHEN prog.t_Frecuencia = 'S' THEN DATEADD(DAY,   7, tmp.f_ProximaEjecucion)
                        WHEN prog.t_Frecuencia = 'Q' THEN DATEADD(DAY,  15, tmp.f_ProximaEjecucion)
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
 
        -- Devolver datos al motor de consola
        SELECT
            tmp.i_Cve_Programacion,
            gen.i_Cve_Generacion,
            pl.t_Nombre           AS NombreReporte,
            pl.t_RutaPlantilla,
            pl.t_FormatoSalida,
            pl.t_ParametrosConfig,
            pl.t_ColumnasConfig,
            prog.t_Parametros,
            prog.t_DiasSemana,
            prog.i_DiaMes,
            prog.t_Frecuencia,    -- necesario para resolver INICIO/FIN_PERIODO
            prog.f_VigenciaInicio,
            prog.f_VigenciaFin,
            con.t_NombreVista,
            con.t_NombreSP        -- NULL = motor ejecuta vista directo
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
 
EXEC sp_addextendedproperty @name=N'MS_Description',
    @value=N'v4 - Procesa cola de reportes pendientes dentro de vigencia. ' +
           N'Devuelve t_NombreVista, t_NombreSP y t_Frecuencia para el motor. ' +
           N'Si la próxima ejecución supera f_VigenciaFin pone f_ProximaEjecucion = NULL.',
    @level0type=N'SCHEMA',@level0name=N'dbo',
    @level1type=N'PROCEDURE',@level1name=N'SP016ProcesarColaReportes'
GO
 
-- ===================================================================
-- VERIFICACIÓN DE INTEGRIDAD REFERENCIAL
-- ===================================================================
PRINT 'Verificando llaves foráneas...'
GO
IF EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name='FK_Cat016Pla_Consulta')          PRINT 'FK_Cat016Pla_Consulta: OK'          ELSE PRINT 'FK_Cat016Pla_Consulta: NO ENCONTRADA'
IF EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name='FK_Enc016Val_Plantilla')         PRINT 'FK_Enc016Val_Plantilla: OK'         ELSE PRINT 'FK_Enc016Val_Plantilla: NO ENCONTRADA'
IF EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name='FK_Enc016Pro_Plantilla')         PRINT 'FK_Enc016Pro_Plantilla: OK'         ELSE PRINT 'FK_Enc016Pro_Plantilla: NO ENCONTRADA'
IF EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name='FK_Bit016_Programacion')         PRINT 'FK_Bit016_Programacion: OK'         ELSE PRINT 'FK_Bit016_Programacion: NO ENCONTRADA'
GO
 
-- ===================================================================
-- RESUMEN
-- ===================================================================
PRINT '======================================================='
PRINT 'MÓDULO REPORTEADOR KROM - FAMILIA 016 (SCRIPT COMPLETO)'
PRINT '======================================================='
PRINT ''
PRINT 'Tablas creadas (5):'
PRINT '  Cat016ConsultasReporteador'
PRINT '  Cat016PlantillasReporteador'
PRINT '  Enc016ValidacionesReporteador'
PRINT '  Enc016ProgramacionReporteador'
PRINT '  Bit016GeneracionReporteador'
PRINT ''
PRINT 'Vistas creadas (7):'
PRINT '  Vig016ProgramacionesPendientes'
PRINT '  Vt016ConsultasDisponibles'
PRINT '  Vt016Plantillas'
PRINT '  Vt016Programacion'
PRINT '  Ve016IUConsultas'
PRINT '  Ve016IUPlantillas'
PRINT '  Ve016IUProgramacion'
PRINT ''
PRINT 'Stored Procedures (1):'
PRINT '  SP016ProcesarColaReportes (v4)'
PRINT ''
PRINT 'PENDIENTE EN SYSEXPERT (por DBA):'
PRINT '  - Crear VT016OperacionesBase (vista base con JOINs)'
PRINT '  - Crear spVT016OperacionesDetalle (SP con filtros)'
PRINT '  - Registrar en Cat016ConsultasReporteador'
PRINT '  - Configurar linked servers si se requieren (Aduana, SLAM)'
PRINT '======================================================='
GO