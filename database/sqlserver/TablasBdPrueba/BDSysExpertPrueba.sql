USE [SysExpert];
GO

-- =============================================
-- Tablas necesarias para el reporte de operaciones
-- =============================================

-- Tabla: VT016Operaciones_
CREATE TABLE [dbo].[VT016Operaciones_] (
    idReferencia INT IDENTITY(1,1) PRIMARY KEY,
    NumeroReferencia VARCHAR(50),
    Anio INT,
    ClaveAduana VARCHAR(3),
    Patente VARCHAR(4),
    NumeroPedimento VARCHAR(7),
    Pago DATE,
    FechaEntrada DATE,
    TipoDeCambio DECIMAL(10,4),
    Regimen VARCHAR(3),
    Clave VARCHAR(2),
    ValorSegurosMN DECIMAL(18,2),
    SegurosUSD DECIMAL(18,2),
    FletesUSD DECIMAL(18,2),
    EmbalajeUSD DECIMAL(18,2),
    OtrosIncrementablesUSD DECIMAL(18,2),
    Firmae VARCHAR(100),
    firmabancaria VARCHAR(100),
    TipoOperacion INT,
    idCliente INT
);
GO

-- Tabla: Clientes
CREATE TABLE [dbo].[Clientes] (
    idCliente INT IDENTITY(1,1) PRIMARY KEY,
    RFC VARCHAR(20),
    RazonSocial VARCHAR(200)
);
GO

-- Tabla: Tab016Buques
CREATE TABLE [dbo].[Tab016Buques] (
    idBuque INT IDENTITY(1,1) PRIMARY KEY,
    idReferencia INT,
    Buque VARCHAR(100)
);
GO

-- Tabla: AsignacionFacturas
CREATE TABLE [dbo].[AsignacionFacturas] (
    idAsignacion INT IDENTITY(1,1) PRIMARY KEY,
    idReferencia INT,
    idRemesa INT,
    idFactura INT
);
GO

-- Tabla: Ped_Remesas
CREATE TABLE [dbo].[Ped_Remesas] (
    idRemesa INT IDENTITY(1,1) PRIMARY KEY,
    idReferencia INT,
    Observaciones VARCHAR(500)
);
GO

-- Tabla: Ped_Facturas
CREATE TABLE [dbo].[Ped_Facturas] (
    idFactura INT IDENTITY(1,1) PRIMARY KEY,
    NumeroFactura VARCHAR(50),
    FechaFactura DATE,
    idIncoterm INT,
    idProveedor INT,
    idEquivalencia INT,
    idGuia INT,
    idPaisMoneda INT
);
GO

-- Tabla: COVEFactura
CREATE TABLE [dbo].[COVEFactura] (
    idCOVEFactura INT IDENTITY(1,1) PRIMARY KEY,
    idFactura INT,
    idCOVEGeneral INT
);
GO

-- Tabla: COVEGeneral
CREATE TABLE [dbo].[COVEGeneral] (
    idCOVEGeneral INT IDENTITY(1,1) PRIMARY KEY,
    eDocument VARCHAR(50)
);
GO

-- Tabla: Incoterms
CREATE TABLE [dbo].[Incoterms] (
    idIncoterm INT IDENTITY(1,1) PRIMARY KEY,
    Incoterm VARCHAR(10)
);
GO

-- Tabla: Proveedores
CREATE TABLE [dbo].[Proveedores] (
    idProveedor INT IDENTITY(1,1) PRIMARY KEY,
    RazonSocial VARCHAR(200)
);
GO

-- Tabla: Monedas_Equivalencias
CREATE TABLE [dbo].[Monedas_Equivalencias] (
    idEquivalencia INT IDENTITY(1,1) PRIMARY KEY,
    Equivalencia DECIMAL(10,4)
);
GO

-- Tabla: Ped_Guias
CREATE TABLE [dbo].[Ped_Guias] (
    idGuia INT IDENTITY(1,1) PRIMARY KEY,
    GuiaMaster VARCHAR(210),
    GuiaHouse VARCHAR(250)
);
GO

-- Tabla: Ped_Partidas
CREATE TABLE [dbo].[Ped_Partidas] (
    idPedPartida INT IDENTITY(1,1) PRIMARY KEY,
    idReferencia INT,
    idFactura INT,
    idOrdenFraccion INT,
    Commodity VARCHAR(20),
    idParte INT
);
GO

-- Tabla: PARTES
CREATE TABLE [dbo].[PARTES] (
    idParte INT IDENTITY(1,1) PRIMARY KEY,
    Clave VARCHAR(50),
    idTipo INT
);
GO

-- Tabla: Partes_Clasificacion
CREATE TABLE [dbo].[Partes_Clasificacion] (
    idTipo INT IDENTITY(1,1) PRIMARY KEY,
    Descripcion VARCHAR(100)
);
GO

-- Tabla: Ped_Fraccion
CREATE TABLE [dbo].[Ped_Fraccion] (
    idPedFraccion INT IDENTITY(1,1) PRIMARY KEY,
    ORDEN VARCHAR(20),
    idFraccion INT,
    ValorAduana DECIMAL(18,2),
    ImporteME DECIMAL(18,2),
    idPaisOrigen INT,
    idPaisVendedor INT
);
GO

-- Tabla: TIGIE
CREATE TABLE [dbo].[TIGIE] (
    idFraccion INT IDENTITY(1,1) PRIMARY KEY,
    Fraccion VARCHAR(10),
    idUnidad INT
);
GO

-- Tabla: UnidadesMedida
CREATE TABLE [dbo].[UnidadesMedida] (
    idUnidad INT IDENTITY(1,1) PRIMARY KEY,
    ClaveUnidad VARCHAR(10)
);
GO

-- Tabla: Paises
CREATE TABLE [dbo].[Paises] (
    idPais INT IDENTITY(1,1) PRIMARY KEY,
    ClaveM3 VARCHAR(3),
    Nombre VARCHAR(100)
);
GO

-- Tabla: Vt016Contribuciones
CREATE TABLE [dbo].[Vt016Contribuciones] (
    idContribucion INT IDENTITY(1,1) PRIMARY KEY,
    idReferencia INT,
    PRV DECIMAL(18,2),
    CNT DECIMAL(18,2),
    ivaprv DECIMAL(18,2),
    DTA DECIMAL(18,2),
    IGI DECIMAL(18,2),
    IVA DECIMAL(18,2)
);
GO

-- Tabla: Vt016ContribucionesPedimento
CREATE TABLE [dbo].[Vt016ContribucionesPedimento] (
    idContribucionPedimento INT IDENTITY(1,1) PRIMARY KEY,
    idReferencia INT
);
GO

-- Tabla: Vt016Contribuciones_Recti
CREATE TABLE [dbo].[Vt016Contribuciones_Recti] (
    idContribucionRecti INT IDENTITY(1,1) PRIMARY KEY,
    idReferencia INT,
    ivaprv DECIMAL(18,2),
    IGI DECIMAL(18,2),
    IVA DECIMAL(18,2)
);
GO

-- Tabla: Ped_IdentificadoresFraccion
CREATE TABLE [dbo].[Ped_IdentificadoresFraccion] (
    idIdentificadorFraccion INT IDENTITY(1,1) PRIMARY KEY,
    idPedFraccion INT,
    idIdentificador INT,
    Complemento VARCHAR(100)
);
GO

-- Tabla: Identificadores
CREATE TABLE [dbo].[Identificadores] (
    idIdentificador INT IDENTITY(1,1) PRIMARY KEY,
    Identificador VARCHAR(10)
);
GO

-- Tabla: Ped_ContribucionesFraccion
CREATE TABLE [dbo].[Ped_ContribucionesFraccion] (
    idContribucionFraccion INT IDENTITY(1,1) PRIMARY KEY,
    idFraccion INT,
    idContribucion INT,
    TasaContribucion DECIMAL(10,4)
);
GO

-- Tabla: Vt015ValoresTotalesPartidasPedimento
CREATE TABLE [dbo].[Vt015ValoresTotalesPartidasPedimento] (
    idValorTotal INT IDENTITY(1,1) PRIMARY KEY,
    idReferencia INT,
    Descripcion VARCHAR(200)
);
GO

-- =============================================
-- Datos de prueba para el reporte
-- =============================================

-- Insertar datos en Clientes
INSERT INTO [dbo].[Clientes] (RFC, RazonSocial) VALUES
('WME990813BAA', 'WALMART DE MEXICO S DE RL DE CV');

-- Insertar datos en VT016Operaciones_
INSERT INTO [dbo].[VT016Operaciones_] (
    NumeroReferencia, Anio, ClaveAduana, Patente, NumeroPedimento, Pago, FechaEntrada,
    TipoDeCambio, Regimen, Clave, ValorSegurosMN, SegurosUSD, FletesUSD, EmbalajeUSD,
    OtrosIncrementablesUSD, Firmae, firmabancaria, TipoOperacion, idCliente
) VALUES
('REF001', 2026, '240', '3584', '1234567', '2026-01-15', '2026-01-10',
 17.50, '10', 'A1', 1000.00, 50.00, 200.00, 30.00, 20.00, 'FIRMA001', 'FIRMABANCO001', 2, 1),
('REF002', 2026, '240', '3584', '7654321', '2026-01-20', '2026-01-18',
 17.60, '10', 'R1', 1500.00, 75.00, 250.00, 40.00, 30.00, 'FIRMA002', 'FIRMABANCO002', 1, 1);

-- Insertar datos en Proveedores
INSERT INTO [dbo].[Proveedores] (RazonSocial) VALUES
('PROVEEDOR INTERNACIONAL SA DE CV'),
('SUPPLIER CORP USA');

-- Insertar datos en Ped_Facturas
INSERT INTO [dbo].[Ped_Facturas] (NumeroFactura, FechaFactura, idIncoterm, idProveedor, idEquivalencia, idGuia, idPaisMoneda) VALUES
('INV001', '2026-01-05', 1, 1, 1, 1, 1),
('INV002', '2026-01-12', 2, 2, 2, 2, 2);

-- Insertar datos en Incoterms
INSERT INTO [dbo].[Incoterms] (Incoterm) VALUES
('FOB'),
('CIF');

-- Insertar datos en Monedas_Equivalencias
INSERT INTO [dbo].[Monedas_Equivalencias] (Equivalencia) VALUES
(1.00),  -- USD
(0.85);  -- EUR

-- Insertar datos en Ped_Guias
INSERT INTO [dbo].[Ped_Guias] (GuiaMaster, GuiaHouse) VALUES
('MASTER001', 'HOUSE001'),
('MASTER002', 'HOUSE002');

-- Insertar datos en Paises
INSERT INTO [dbo].[Paises] (ClaveM3, Nombre) VALUES
('840', 'ESTADOS UNIDOS'),
('484', 'MEXICO'),
('276', 'ALEMANIA'),
('156', 'CHINA');

-- Insertar datos en Tab016Buques
INSERT INTO [dbo].[Tab016Buques] (idReferencia, Buque) VALUES
(1, 'BUQUE ATLANTICO'),
(2, 'BUQUE PACIFICO');

-- Insertar datos en AsignacionFacturas
INSERT INTO [dbo].[AsignacionFacturas] (idReferencia, idRemesa, idFactura) VALUES
(1, 1, 1),
(2, 2, 2);

-- Insertar datos en Ped_Remesas
INSERT INTO [dbo].[Ped_Remesas] (idReferencia, Observaciones) VALUES
(1, 'REmesa de prueba 1'),
(2, 'REmesa de prueba 2');

-- Insertar datos en PARTES
INSERT INTO [dbo].[PARTES] (Clave, idTipo) VALUES
('PROD001', 1),
('PROD002', 2),
('PROD003', 1);

-- Insertar datos en Partes_Clasificacion
INSERT INTO [dbo].[Partes_Clasificacion] (Descripcion) VALUES
('Electrónicos'),
('Textiles'),
('Alimentos');

-- Insertar datos en TIGIE
INSERT INTO [dbo].[TIGIE] (Fraccion, idUnidad) VALUES
('8471.30.01', 1),
('6204.62.00', 2),
('0808.10.01', 3);

-- Insertar datos en UnidadesMedida
INSERT INTO [dbo].[UnidadesMedida] (ClaveUnidad) VALUES
('PZA'),
('KG'),
('L'),
('M2');

-- Insertar datos en Vt016Contribuciones
INSERT INTO [dbo].[Vt016Contribuciones] (idReferencia, PRV, CNT, ivaprv, DTA, IGI, IVA) VALUES
(1, 100.00, 50.00, 200.00, 30.00, 150.00, 300.00),
(2, 120.00, 60.00, 250.00, 40.00, 180.00, 350.00);

-- Insertar datos en Ped_Fraccion
INSERT INTO [dbo].[Ped_Fraccion] (ORDEN, idFraccion, ValorAduana, ImporteME, idPaisOrigen, idPaisVendedor) VALUES
('001', 1, 5000.00, 4800.00, 1, 1),
('002', 2, 7500.00, 7200.00, 3, 3),
('003', 3, 3000.00, 2900.00, 4, 4);

-- Insertar datos en Ped_Partidas
INSERT INTO [dbo].[Ped_Partidas] (idReferencia, idFactura, idOrdenFraccion, Commodity, idParte) VALUES
(1, 1, 1, '001', 1),
(2, 2, 2, '002', 2);

-- =============================================
-- Comentarios descriptivos de las tablas
-- =============================================
EXEC sp_addextendedproperty 
    @name = N'MS_Description', 
    @value = N'Vista de operaciones principales del sistema',
    @level0type = N'SCHEMA', @level0name = N'dbo', 
    @level1type = N'TABLE', @level1name = N'VT016Operaciones_';
GO

EXEC sp_addextendedproperty 
    @name = N'MS_Description', 
    @value = N'Catálogo de clientes',
    @level0type = N'SCHEMA', @level0name = N'dbo', 
    @level1type = N'TABLE', @level1name = N'Clientes';
GO

EXEC sp_addextendedproperty 
    @name = N'MS_Description', 
    @value = N'Tabla de buques para operaciones',
    @level0type = N'SCHEMA', @level0name = N'dbo', 
    @level1type = N'TABLE', @level1name = N'Tab016Buques';
GO

EXEC sp_addextendedproperty 
    @name = N'MS_Description', 
    @value = N'Tabla de prueba creada para el sistema de reportes automatizados',
    @level0type = N'SCHEMA', @level0name = N'dbo', 
    @level1type = N'TABLE', @level1name = N'Ped_Facturas';
GO

PRINT '=============================================';
PRINT 'TABLAS DE PRUEBA CREADAS EXITOSAMENTE';
PRINT '=============================================';
PRINT 'Tablas creadas: 24';
PRINT 'Datos de prueba insertados';
PRINT '';
PRINT 'Para probar el reporte, ejecuta:';
PRINT '---------------------------------------------';
PRINT 'USE [SysExpert];';
PRINT 'DECLARE @FechaInicio   VARCHAR(20) = ''2026-01-01''';
PRINT 'DECLARE @FechaFin      VARCHAR(20) = ''2026-01-31''';
PRINT 'DECLARE @TipoOperacion VARCHAR(20) = 2';
PRINT 'DECLARE @RFC           VARCHAR(20) = ''WME990813BAA''';
PRINT '---------------------------------------------';
PRINT '-- Luego ejecuta la consulta del reporte --';
PRINT '=============================================';
GO