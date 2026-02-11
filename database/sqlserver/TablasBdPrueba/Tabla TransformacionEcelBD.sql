USE SysExpert;
GO

CREATE TABLE dbo.StagingImportacion (
    IdStaging INT IDENTITY(1,1) PRIMARY KEY, -- 🔥 clave técnica
    NumeroReferencia VARCHAR(100),
    Pedimento VARCHAR(100),
    Pago VARCHAR(50),
    FechaEntrada DATETIME,
    TipoDeCambio DECIMAL(10,4),
    Regimen VARCHAR(50),
    ClavePedimento VARCHAR(50),
    Patente VARCHAR(50),
    Aduana VARCHAR(100),
    Seccion VARCHAR(50),
    ValorSegurosMN DECIMAL(15,2),
    SegurosUSD DECIMAL(15,2),
    FletesUSD DECIMAL(15,2),
    EmbalajeUSD DECIMAL(15,2),
    OtrosIncrementablesUSD DECIMAL(15,2),
    PRV DECIMAL(15,2),
    CNT DECIMAL(15,2),
    IVAPRV DECIMAL(15,2),
    DTA DECIMAL(15,2),
    GuiaBL VARCHAR(100),
    Buque VARCHAR(100),
    Partida VARCHAR(100),
    NumeroFactura VARCHAR(100),
    Cove VARCHAR(100),
    FechaFactura DATETIME,
    TC DECIMAL(10,4),
    ProveedorComprador VARCHAR(500),
    Incoterm VARCHAR(20),
    MonedaFactura VARCHAR(10),
    FactorMoneda DECIMAL(10,4),
    CodigoProducto VARCHAR(100),
    DescripcionMercancia VARCHAR(MAX),
    CantidadComercial DECIMAL(15,4),
    ClaveUnidad VARCHAR(20),
    CantidadTarifa DECIMAL(15,4),
    UMT VARCHAR(20),
    ValorMciaME DECIMAL(15,2),
    ValorMciaUSD DECIMAL(15,2),
    Categoria VARCHAR(50),
    Fraccion VARCHAR(20),
    PaisOrigen VARCHAR(50),
    TLC VARCHAR(50),
    PROSEC VARCHAR(50),
    Advalorem DECIMAL(10,4),
    IGI DECIMAL(15,2),
    TasaIVA DECIMAL(10,4),
    IVA DECIMAL(15,2),
    ValorAduana DECIMAL(15,2),
    PaisVendedor VARCHAR(50),
    TipoOperacion VARCHAR(50),
    FechaCarga DATETIME DEFAULT GETDATE()
);

ALTER TABLE dbo.StagingImportacion
ADD HashRegistro AS 
    HASHBYTES(
        'SHA2_256',
        CONCAT(
            ISNULL(Pedimento,''),'|',
            ISNULL(Partida,''),'|',
            ISNULL(NumeroFactura,''),'|',
            ISNULL(CodigoProducto,'')
        )
    );


CREATE UNIQUE INDEX UX_StagingImportacion_Hash
ON dbo.StagingImportacion(HashRegistro);
