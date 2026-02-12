INSERT INTO dbo.StagingImportacion (
    NumeroReferencia,
    Pedimento,
    Pago,
    FechaEntrada,
    TipoDeCambio,
    Regimen,
    ClavePedimento,
    Patente,
    Aduana,
    Seccion,
    ValorSegurosMN,
    SegurosUSD,
    FletesUSD,
    EmbalajeUSD,
    OtrosIncrementablesUSD,
    PRV,
    CNT,
    IVAPRV,
    DTA,
    GuiaBL,
    Buque,
    Partida,
    NumeroFactura,
    Cove,
    FechaFactura,
    TC,
    ProveedorComprador,
    Incoterm,
    MonedaFactura,
    FactorMoneda,
    CodigoProducto,
    DescripcionMercancia,
    CantidadComercial,
    ClaveUnidad,
    CantidadTarifa,
    UMT,
    ValorMciaME,
    ValorMciaUSD,
    Categoria,
    Fraccion,
    PaisOrigen,
    TLC,
    PROSEC,
    Advalorem,
    IGI,
    TasaIVA,
    IVA,
    ValorAduana,
    PaisVendedor,
    TipoOperacion,
	HashRegistro
)
SELECT 
    [Numero Referencia],
    Pedimento,
    Pago,
    TRY_CONVERT(DATETIME, [Fecha de Entrada]),
    TRY_CAST(REPLACE([Tipo De Cambio], ',', '.') AS DECIMAL(10,4)),
    Regimen,
    [Clave Pedimento],
    Patente,
    Aduana,
    Seccion,
    TRY_CAST(REPLACE(ValorSegurosMN, ',', '.') AS DECIMAL(15,2)),
    TRY_CAST(REPLACE(SegurosUSD, ',', '.') AS DECIMAL(15,2)),
    TRY_CAST(REPLACE(FletesUSD, ',', '.') AS DECIMAL(15,2)),
    TRY_CAST(REPLACE(EmbalajeUSD, ',', '.') AS DECIMAL(15,2)),
    TRY_CAST(REPLACE([Otros Incrementables USD], ',', '.') AS DECIMAL(15,2)),
    TRY_CAST(REPLACE(PRV, ',', '.') AS DECIMAL(15,2)),
    TRY_CAST(REPLACE(CNT, ',', '.') AS DECIMAL(15,2)),
    TRY_CAST(REPLACE([IVA/PRV], ',', '.') AS DECIMAL(15,2)),
    TRY_CAST(REPLACE(DTA, ',', '.') AS DECIMAL(15,2)),
    [GuiaBL],
    Buque,
    Partida,
    [NumeroFactura],
    Cove,
    TRY_CONVERT(DATETIME, [FechaFactura]),
    TRY_CAST(REPLACE(TC, ',', '.') AS DECIMAL(10,4)),
    [Proveedor/Comprador],
    Incoterm,
    [MonedaFactura],
    TRY_CAST(REPLACE([FactorMoneda], ',', '.') AS DECIMAL(10,4)),
    [CodigoProducto],
    [DescripcionMercancia],
    TRY_CAST(REPLACE([CantidadComercial], ',', '.') AS DECIMAL(15,4)),
    [ClaveUnidad],
    TRY_CAST(REPLACE([CantidadTarifa], ',', '.') AS DECIMAL(15,4)),
    UMT,
    TRY_CAST(REPLACE([ValorMciaME], ',', '.') AS DECIMAL(15,2)),
    TRY_CAST(REPLACE([ValorMciaUSD], ',', '.') AS DECIMAL(15,2)),
    Categoria,
    Fraccion,
    [PaisOrigen],
    TLC,
    PROSEC,
    TRY_CAST(REPLACE(Advalorem, ',', '.') AS DECIMAL(10,4)),
    TRY_CAST(REPLACE(IGI, ',', '.') AS DECIMAL(15,2)),
    TRY_CAST(REPLACE([TasaIVA], ',', '.') AS DECIMAL(10,4)),
    TRY_CAST(REPLACE(IVA, ',', '.') AS DECIMAL(15,2)),
    TRY_CAST(REPLACE([ValorAduana], ',', '.') AS DECIMAL(15,2)),
    [PaisVendedor],
    [TipoOperación],
	HASHBYTES(
    'SHA2_256',
    CONCAT(
        ISNULL([Numero Referencia],''),'|',
        ISNULL(Partida,''),'|',
        ISNULL([CodigoProducto],''),'|',
        ISNULL(CantidadComercial,''),'|',
        ISNULL(ValorMciaME,''),'|',
        ISNULL(ValorAduana,'')
    )
)



FROM OPENROWSET(
    'Microsoft.ACE.OLEDB.16.0',
    'Excel 12.0;HDR=YES;IMEX=1;Database=C:\Temp\archivo.xlsx',
    'SELECT * FROM [Hoja1$]');

GO

DROP INDEX UX_Staging_Hash ON dbo.StagingImportacion;

ALTER TABLE dbo.StagingImportacion
DROP COLUMN HashRegistro;
GO

ALTER TABLE dbo.StagingImportacion
ADD HashRegistro VARBINARY(32) NOT NULL;

CREATE UNIQUE INDEX UX_Staging_Hash
ON dbo.StagingImportacion(HashRegistro);

SELECT * FROM OPENROWSET(
    'Microsoft.ACE.OLEDB.16.0',
    'Excel 12.0;HDR=YES;IMEX=1;Database=C:\Temp\archivo.xlsx',
    'SELECT * FROM [Hoja1$]'
);


SELECT *
FROM OPENROWSET(
    'Microsoft.ACE.OLEDB.16.0',
    'Excel 12.0;HDR=YES;IMEX=1;Database=C:\Temp\archivo.xlsx',
    'SELECT * FROM [Hoja1$]'
)
WHERE [Numero Referencia] = 'SAP26-0752'
  AND Partida = 1
  AND [CodigoProducto] = '10014995 - REGLA OCTAVA';

  INSERT INTO Incoterms (Incoterm)
SELECT DISTINCT s.Incoterm
FROM StagingImportacion s
LEFT JOIN Incoterms i 
    ON i.Incoterm = s.Incoterm
WHERE i.idIncoterm IS NULL;



