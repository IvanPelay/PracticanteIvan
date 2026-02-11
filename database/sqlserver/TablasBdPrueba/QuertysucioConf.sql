-- Habilitar opciones avanzadas
EXEC sp_configure 'show advanced options', 1;
RECONFIGURE;
EXEC sp_configure 'Ad Hoc Distributed Queries', 1;
RECONFIGURE;
GO

-- Configurar proveedor ACE 16.0 (64-bit)
EXEC master.dbo.sp_MSset_oledb_prop N'Microsoft.ACE.OLEDB.16.0', 
                                     N'AllowInProcess', 1;
EXEC master.dbo.sp_MSset_oledb_prop N'Microsoft.ACE.OLEDB.16.0', 
                                     N'DynamicParameters', 1;
EXEC master.dbo.sp_MSset_oledb_prop N'Microsoft.ACE.OLEDB.16.0', 
                                     N'AllowInProcess', 1;
EXEC master.dbo.sp_MSset_oledb_prop N'Microsoft.ACE.OLEDB.16.0', 
                                     N'DynamicParameters', 1;
GO

-- Ver proveedores OLE DB disponibles
SELECT *
FROM sys.servers
WHERE provider IN ('Microsoft.ACE.OLEDB.16.0', 'Microsoft.ACE.OLEDB.12.0');

-- Ver propiedades
EXEC master.dbo.sp_MSset_oledb_prop;

-- 1. Probar con archivo pequeño primero
CREATE TABLE #TestResult (Resultado VARCHAR(100));

-- Prueba 1: Sin rango específico
SELECT TOP 1 *
FROM OPENROWSET(
    'Microsoft.ACE.OLEDB.16.0',
    'Excel 12.0;Database=C:\Temp\archivo.xlsx',
    'SELECT * FROM [Hoja1$]');


SELECT * FROM #TestResult;
DROP TABLE #TestResult;

--configuraciones para trabajar con excel
EXEC sp_configure 'show advanced options', 1;
RECONFIGURE;
EXEC sp_configure 'Ad Hoc Distributed Queries', 1;
RECONFIGURE;

SELECT servicename, service_account
FROM sys.dm_server_services;


-- Prueba primero con una consulta simple
SELECT @@SERVERNAME as NombreServidor, 
       DB_NAME() as BaseDatosActual,
       'SQL Server funcionando' as Estado;

-- Ejecuta esto rápidamente antes de que se detenga
SELECT @@VERSION, 
       SERVERPROPERTY('Edition') as Edition,
       SERVERPROPERTY('ProductVersion') as Version,
       SERVERPROPERTY('ProductLevel') as SPLevel,
       SERVERPROPERTY('EngineEdition') as EngineEdition;


SELECT 
    blocking_session_id, 
    wait_type, 
    wait_time, 
    last_wait_type
FROM sys.dm_exec_requests 
WHERE blocking_session_id > 0;
GO




-- Ejecuta esto primero
SELECT @@VERSION;
SELECT SERVERPROPERTY('BuildClrVersion') as CLR_Version,
       SERVERPROPERTY('Edition') as Edition,
       '¿64-bit?' = CASE WHEN CHARINDEX('64', @@VERSION) > 0 
                         THEN 'Sí' ELSE 'No' END;


-- Ver todas las columnas disponibles
SELECT TOP 5 *
FROM OPENROWSET(
    'Microsoft.ACE.OLEDB.16.0',
    'Excel 12.0;HDR=YES;IMEX=1;Database=C:\Temp\archivo.xlsx',
    'SELECT * FROM [Hoja1$]');

-- Obtener solo los metadatos (nombres de columnas)
SELECT TOP 0 *
FROM OPENROWSET(
    'Microsoft.ACE.OLEDB.16.0',
    'Excel 12.0;HDR=YES;IMEX=1;Database=C:\Temp\archivo.xlsx',
    'SELECT * FROM [Hoja1$]');

	-- Tabla temporal para importación completa
CREATE TABLE #StagingImportacion (
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
    TipoOperacion VARCHAR(50)
);

-- Importar datos del Excel
INSERT INTO #StagingImportacion
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
    [TipoOperación]
FROM OPENROWSET(
    'Microsoft.ACE.OLEDB.16.0',
    'Excel 12.0;HDR=YES;IMEX=1;Database=C:\Temp\archivo.xlsx',
    'SELECT * FROM [Hoja1$]');

-- Verificar importación
SELECT COUNT(*) as TotalRegistros FROM #StagingImportacion;
SELECT TOP 10 * FROM #StagingImportacion;