USE [Solium]
GO

/****** Object:  View [dbo].[Ve016IUAnexo24Grammer]    Script Date: 20/02/2026 08:23:22 a. m. ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

 --Create view [dbo].[Ve016IUAnexo24Grammer] as
 
 ( SELECT 'NumeroPedimento' as Nombre,1 AS Llave, 30 AS Longitud,  1 AS TipoDato,  1 AS Visible,  'Numero Pedimento' AS NombreColumna,  0 AS PuedeInsertar,  0 AS PuedeModificar,  '' AS ValorDefault,  '1' AS TipoFiltro 
 UNION ALL 
 SELECT 'ClaveAduana' as Nombre,0 AS Llave, 3 AS Longitud,  1 AS TipoDato,  1 AS Visible,  'ClaveAduana' AS NombreColumna,  0 AS PuedeInsertar,  0 AS PuedeModificar,  '' AS ValorDefault,  '2' AS TipoFiltro 
 UNION ALL 
 SELECT 'Pago' as Nombre,0 AS Llave, 11 AS Longitud,  4 AS TipoDato,  1 AS Visible,  'Pago' AS NombreColumna,  0 AS PuedeInsertar,  0 AS PuedeModificar,  '' AS ValorDefault,  '2' AS TipoFiltro 
 UNION ALL 
 SELECT 'TipoDeCambio' as Nombre,0 AS Llave, 11 AS Longitud,  0 AS TipoDato,  1 AS Visible,  'TipoDeCambio' AS NombreColumna,  0 AS PuedeInsertar,  0 AS PuedeModificar,  '' AS ValorDefault,  '2' AS TipoFiltro 
 UNION ALL 
 SELECT 'IVA' as Nombre,0 AS Llave, 11 AS Longitud,  0 AS TipoDato,  1 AS Visible,  'IVA' AS NombreColumna,  0 AS PuedeInsertar,  0 AS PuedeModificar,  '' AS ValorDefault,  '2' AS TipoFiltro 
 UNION ALL 
 SELECT 'Clave' as Nombre,0 AS Llave, 3 AS Longitud,  1 AS TipoDato,  1 AS Visible,  'Clave' AS NombreColumna,  0 AS PuedeInsertar,  0 AS PuedeModificar,  '' AS ValorDefault,  '2' AS TipoFiltro 
 UNION ALL 
 SELECT 'FletesUSD' as Nombre,0 AS Llave, 11 AS Longitud,  0 AS TipoDato,  1 AS Visible,  'FletesUSD' AS NombreColumna,  0 AS PuedeInsertar,  0 AS PuedeModificar,  '' AS ValorDefault,  '2' AS TipoFiltro 
 UNION ALL 
 SELECT 'SegurosUSD' as Nombre,0 AS Llave, 11 AS Longitud,  0 AS TipoDato,  1 AS Visible,  'SegurosUSD' AS NombreColumna,  0 AS PuedeInsertar,  0 AS PuedeModificar,  '' AS ValorDefault,  '2' AS TipoFiltro 
 UNION ALL 
 SELECT 'EmbalajeUSD' as Nombre,0 AS Llave, 11 AS Longitud,  0 AS TipoDato,  1 AS Visible,  'EmbalajeUSD' AS NombreColumna,  0 AS PuedeInsertar,  0 AS PuedeModificar,  '' AS ValorDefault,  '2' AS TipoFiltro 
 UNION ALL 
 SELECT 'OtrosIncrementablesUSD' as Nombre,0 AS Llave, 11 AS Longitud,  0 AS TipoDato,  1 AS Visible,  'OtrosIncrementablesUSD' AS NombreColumna,  0 AS PuedeInsertar,  0 AS PuedeModificar,  '' AS ValorDefault,  '2' AS TipoFiltro 
 UNION ALL 
 SELECT 'DTA' as Nombre,0 AS Llave, 11 AS Longitud,  3 AS TipoDato,  1 AS Visible,  'DTA' AS NombreColumna,  0 AS PuedeInsertar,  0 AS PuedeModificar,  '' AS ValorDefault,  '2' AS TipoFiltro 
 UNION ALL 
 SELECT 'ValorComercialPedimento' as Nombre,0 AS Llave, 11 AS Longitud,  0 AS TipoDato,  1 AS Visible,  'ValorComercialPedimento' AS NombreColumna,  0 AS PuedeInsertar,  0 AS PuedeModificar,  '' AS ValorDefault,  '2' AS TipoFiltro 
 UNION ALL 
 SELECT 'ValorAduanaPedimento' as Nombre,0 AS Llave, 11 AS Longitud,  0 AS TipoDato,  1 AS Visible,  'ValorAduanaPedimento' AS NombreColumna,  0 AS PuedeInsertar,  0 AS PuedeModificar,  '' AS ValorDefault,  '2' AS TipoFiltro 
 UNION ALL 
 SELECT 'NuevasObservaciones' as Nombre,0 AS Llave, 2147483647 AS Longitud,  1 AS TipoDato,  1 AS Visible,  'NuevasObservaciones' AS NombreColumna,  0 AS PuedeInsertar,  0 AS PuedeModificar,  '' AS ValorDefault,  '2' AS TipoFiltro 
 UNION ALL 
 SELECT 'DescargoFechaFactura' as Nombre,0 AS Llave, 1 AS Longitud,  1 AS TipoDato,  1 AS Visible,  'DescargoFechaFactura' AS NombreColumna,  0 AS PuedeInsertar,  0 AS PuedeModificar,  '' AS ValorDefault,  '2' AS TipoFiltro 
 UNION ALL 
 SELECT 'RecibidaDe' as Nombre,0 AS Llave, 1 AS Longitud,  1 AS TipoDato,  1 AS Visible,  'RecibidaDe' AS NombreColumna,  0 AS PuedeInsertar,  0 AS PuedeModificar,  '' AS ValorDefault,  '2' AS TipoFiltro 
 UNION ALL 
 SELECT 'NotaPedimento' as Nombre,0 AS Llave, 1 AS Longitud,  1 AS TipoDato,  1 AS Visible,  'NotaPedimento' AS NombreColumna,  0 AS PuedeInsertar,  0 AS PuedeModificar,  '' AS ValorDefault,  '2' AS TipoFiltro 
 UNION ALL 
 SELECT 'PRV' as Nombre,0 AS Llave, 11 AS Longitud,  3 AS TipoDato,  1 AS Visible,  'PRV' AS NombreColumna,  0 AS PuedeInsertar,  0 AS PuedeModificar,  '' AS ValorDefault,  '2' AS TipoFiltro 
 UNION ALL 
 SELECT 'NumeroFactura' as Nombre,0 AS Llave, 50 AS Longitud,  1 AS TipoDato,  1 AS Visible,  'NumeroFactura' AS NombreColumna,  0 AS PuedeInsertar,  0 AS PuedeModificar,  '' AS ValorDefault,  '2' AS TipoFiltro 
 UNION ALL 
 SELECT 'CodigoProveedor' as Nombre,0 AS Llave, 11 AS Longitud,  0 AS TipoDato,  1 AS Visible,  'CodigoProveedor' AS NombreColumna,  0 AS PuedeInsertar,  0 AS PuedeModificar,  '' AS ValorDefault,  '2' AS TipoFiltro 
 UNION ALL 
 SELECT 'FechaFactura' as Nombre,0 AS Llave, 11 AS Longitud,  4 AS TipoDato,  1 AS Visible,  'FechaFactura' AS NombreColumna,  0 AS PuedeInsertar,  0 AS PuedeModificar,  '' AS ValorDefault,  '2' AS TipoFiltro 
 UNION ALL 
 SELECT 'FactorMoneda' as Nombre,0 AS Llave, 11 AS Longitud,  3 AS TipoDato,  1 AS Visible,  'FactorMoneda' AS NombreColumna,  0 AS PuedeInsertar,  0 AS PuedeModificar,  '' AS ValorDefault,  '2' AS TipoFiltro 
 UNION ALL 
 SELECT 'NumeroParteComercioExterior' as Nombre,0 AS Llave, 14 AS Longitud,  1 AS TipoDato,  1 AS Visible,  'NumeroParteComercioExterior' AS NombreColumna,  0 AS PuedeInsertar,  0 AS PuedeModificar,  '' AS ValorDefault,  '2' AS TipoFiltro 
 UNION ALL 
 SELECT 'Fraccion' as Nombre,0 AS Llave, 8000 AS Longitud,  1 AS TipoDato,  1 AS Visible,  'Fraccion' AS NombreColumna,  0 AS PuedeInsertar,  0 AS PuedeModificar,  '' AS ValorDefault,  '2' AS TipoFiltro 
 UNION ALL 
 SELECT 'TasaIGI' as Nombre,0 AS Llave, 11 AS Longitud,  0 AS TipoDato,  1 AS Visible,  'TasaIGI' AS NombreColumna,  0 AS PuedeInsertar,  0 AS PuedeModificar,  '' AS ValorDefault,  '2' AS TipoFiltro 
 UNION ALL 
 SELECT 'TipoTasa' as Nombre,0 AS Llave, -1 AS Longitud,  1 AS TipoDato,  1 AS Visible,  'TipoTasa' AS NombreColumna,  0 AS PuedeInsertar,  0 AS PuedeModificar,  '' AS ValorDefault,  '2' AS TipoFiltro 
 UNION ALL 
 SELECT 'UnidadMC' as Nombre,0 AS Llave, 30 AS Longitud,  1 AS TipoDato,  1 AS Visible,  'UnidadMC' AS NombreColumna,  0 AS PuedeInsertar,  0 AS PuedeModificar,  '' AS ValorDefault,  '2' AS TipoFiltro 
 UNION ALL 
 SELECT 'PrecioUnitario' as Nombre,0 AS Llave, 11 AS Longitud,  0 AS TipoDato,  1 AS Visible,  'PrecioUnitario' AS NombreColumna,  0 AS PuedeInsertar,  0 AS PuedeModificar,  '' AS ValorDefault,  '2' AS TipoFiltro 
 UNION ALL 
 SELECT 'CantidadComercial' as Nombre,0 AS Llave, 11 AS Longitud,  3 AS TipoDato,  1 AS Visible,  'CantidadComercial' AS NombreColumna,  0 AS PuedeInsertar,  0 AS PuedeModificar,  '' AS ValorDefault,  '2' AS TipoFiltro 
 UNION ALL 
 SELECT 'PaisOrigen' as Nombre,0 AS Llave, 3 AS Longitud,  1 AS TipoDato,  1 AS Visible,  'PaisOrigen' AS NombreColumna,  0 AS PuedeInsertar,  0 AS PuedeModificar,  '' AS ValorDefault,  '2' AS TipoFiltro 
 UNION ALL 
 SELECT 'PaisVendedor' as Nombre,0 AS Llave, 3 AS Longitud,  1 AS TipoDato,  1 AS Visible,  'PaisVendedor' AS NombreColumna,  0 AS PuedeInsertar,  0 AS PuedeModificar,  '' AS ValorDefault,  '2' AS TipoFiltro 
 UNION ALL 
 SELECT 'Nota1Pedimento' as Nombre,0 AS Llave, 1 AS Longitud,  1 AS TipoDato,  1 AS Visible,  'Nota1Pedimento' AS NombreColumna,  0 AS PuedeInsertar,  0 AS PuedeModificar,  '' AS ValorDefault,  '2' AS TipoFiltro 
 UNION ALL 
 SELECT 'FP_IGI' as Nombre,0 AS Llave, 11 AS Longitud,  0 AS TipoDato,  1 AS Visible,  'FP_IGI' AS NombreColumna,  0 AS PuedeInsertar,  0 AS PuedeModificar,  '' AS ValorDefault,  '2' AS TipoFiltro 
 UNION ALL 
 SELECT 'Incoterm' as Nombre,0 AS Llave, 3 AS Longitud,  1 AS TipoDato,  1 AS Visible,  'Incoterm' AS NombreColumna,  0 AS PuedeInsertar,  0 AS PuedeModificar,  '' AS ValorDefault,  '2' AS TipoFiltro 
 UNION ALL 
 SELECT 'Tratado' as Nombre,0 AS Llave, 122 AS Longitud,  1 AS TipoDato,  1 AS Visible,  'Tratado' AS NombreColumna,  0 AS PuedeInsertar,  0 AS PuedeModificar,  '' AS ValorDefault,  '2' AS TipoFiltro 
 UNION ALL 
 SELECT 'Id_EB' as Nombre,0 AS Llave, 2 AS Longitud,  1 AS TipoDato,  1 AS Visible,  'Id_EB' AS NombreColumna,  0 AS PuedeInsertar,  0 AS PuedeModificar,  '' AS ValorDefault,  '2' AS TipoFiltro 
 UNION ALL 
 SELECT 'EnConsignacion' as Nombre,0 AS Llave, 1 AS Longitud,  1 AS TipoDato,  1 AS Visible,  'EnConsignacion' AS NombreColumna,  0 AS PuedeInsertar,  0 AS PuedeModificar,  '' AS ValorDefault,  '2' AS TipoFiltro 
 UNION ALL 
 SELECT 'NotaIntena2' as Nombre,0 AS Llave, 1 AS Longitud,  1 AS TipoDato,  1 AS Visible,  'NotaIntena2' AS NombreColumna,  0 AS PuedeInsertar,  0 AS PuedeModificar,  '' AS ValorDefault,  '2' AS TipoFiltro 
 UNION ALL 
 SELECT 'COVE' as Nombre,0 AS Llave, 15 AS Longitud,  1 AS TipoDato,  1 AS Visible,  'COVE' AS NombreColumna,  0 AS PuedeInsertar,  0 AS PuedeModificar,  '' AS ValorDefault,  '2' AS TipoFiltro 
 UNION ALL 
 SELECT 'Multas' as Nombre,0 AS Llave, 11 AS Longitud,  3 AS TipoDato,  1 AS Visible,  'Multas' AS NombreColumna,  0 AS PuedeInsertar,  0 AS PuedeModificar,  '' AS ValorDefault,  '2' AS TipoFiltro 
 UNION ALL 
 SELECT 'Recargos' as Nombre,0 AS Llave, 11 AS Longitud,  3 AS TipoDato,  1 AS Visible,  'Recargos' AS NombreColumna,  0 AS PuedeInsertar,  0 AS PuedeModificar,  '' AS ValorDefault,  '2' AS TipoFiltro 
 UNION ALL 
 SELECT 'NotaInterna2' as Nombre,0 AS Llave, 1 AS Longitud,  1 AS TipoDato,  1 AS Visible,  'NotaInterna2' AS NombreColumna,  0 AS PuedeInsertar,  0 AS PuedeModificar,  '' AS ValorDefault,  '2' AS TipoFiltro 
 UNION ALL 
 SELECT 'CNT' as Nombre,0 AS Llave, 11 AS Longitud,  3 AS TipoDato,  1 AS Visible,  'CNT' AS NombreColumna,  0 AS PuedeInsertar,  0 AS PuedeModificar,  '' AS ValorDefault,  '2' AS TipoFiltro 
 UNION ALL 
 SELECT 'ORDEN' as Nombre,0 AS Llave, 11 AS Longitud,  0 AS TipoDato,  1 AS Visible,  'ORDEN' AS NombreColumna,  0 AS PuedeInsertar,  0 AS PuedeModificar,  '' AS ValorDefault,  '2' AS TipoFiltro 
 UNION ALL 
 SELECT 'FactorDeConversion' as Nombre,0 AS Llave, 11 AS Longitud,  0 AS TipoDato,  1 AS Visible,  'FactorDeConversion' AS NombreColumna,  0 AS PuedeInsertar,  0 AS PuedeModificar,  '' AS ValorDefault,  '2' AS TipoFiltro 
 UNION ALL 
 SELECT 'UnidadTarifa' as Nombre,0 AS Llave, 30 AS Longitud,  1 AS TipoDato,  1 AS Visible,  'UnidadTarifa' AS NombreColumna,  0 AS PuedeInsertar,  0 AS PuedeModificar,  '' AS ValorDefault,  '2' AS TipoFiltro 
 UNION ALL 
 SELECT 'CantidadTarifa' as Nombre,0 AS Llave, 11 AS Longitud,  3 AS TipoDato,  1 AS Visible,  'CantidadTarifa' AS NombreColumna,  0 AS PuedeInsertar,  0 AS PuedeModificar,  '' AS ValorDefault,  '2' AS TipoFiltro 
 UNION ALL 
 SELECT 'FormaPagoIVAPartida' as Nombre,0 AS Llave, 11 AS Longitud,  0 AS TipoDato,  1 AS Visible,  'FormaPagoIVAPartida' AS NombreColumna,  0 AS PuedeInsertar,  0 AS PuedeModificar,  '' AS ValorDefault,  '2' AS TipoFiltro 
 UNION ALL 
 SELECT 'FechaEntrada' as Nombre,0 AS Llave, 11 AS Longitud,  4 AS TipoDato,  1 AS Visible,  'FechaEntrada' AS NombreColumna,  0 AS PuedeInsertar,  0 AS PuedeModificar,  '' AS ValorDefault,  '2' AS TipoFiltro 
 UNION ALL 
 SELECT 'Regimen' as Nombre,0 AS Llave, 3 AS Longitud,  1 AS TipoDato,  1 AS Visible,  'Regimen' AS NombreColumna,  0 AS PuedeInsertar,  0 AS PuedeModificar,  '' AS ValorDefault,  '2' AS TipoFiltro 
 UNION ALL 
 SELECT 'TipoOperacion' as Nombre,0 AS Llave, 1 AS Longitud,  1 AS TipoDato,  1 AS Visible,  'TipoOperacion' AS NombreColumna,  0 AS PuedeInsertar,  0 AS PuedeModificar,  '' AS ValorDefault,  '2' AS TipoFiltro 
 UNION ALL 
 SELECT 'IdentificadorPC' as Nombre,0 AS Llave, 2 AS Longitud,  1 AS TipoDato,  1 AS Visible,  'IdentificadorPC' AS NombreColumna,  0 AS PuedeInsertar,  0 AS PuedeModificar,  '' AS ValorDefault,  '2' AS TipoFiltro 
 UNION ALL 
 SELECT 'IdentificadorIM' as Nombre,0 AS Llave, 2 AS Longitud,  1 AS TipoDato,  1 AS Visible,  'IdentificadorIM' AS NombreColumna,  0 AS PuedeInsertar,  0 AS PuedeModificar,  '' AS ValorDefault,  '2' AS TipoFiltro 
 UNION ALL 
 SELECT 'IEPS' as Nombre,0 AS Llave, 11 AS Longitud,  0 AS TipoDato,  1 AS Visible,  'IEPS' AS NombreColumna,  0 AS PuedeInsertar,  0 AS PuedeModificar,  '' AS ValorDefault,  '2' AS TipoFiltro 
 UNION ALL 
 SELECT 'FormaPagosIEPS' as Nombre,0 AS Llave, 11 AS Longitud,  0 AS TipoDato,  1 AS Visible,  'FormaPagosIEPS' AS NombreColumna,  0 AS PuedeInsertar,  0 AS PuedeModificar,  '' AS ValorDefault,  '2' AS TipoFiltro 
 UNION ALL 
 SELECT 'IVAPRV' as Nombre,0 AS Llave, 11 AS Longitud,  0 AS TipoDato,  1 AS Visible,  'IVAPRV' AS NombreColumna,  0 AS PuedeInsertar,  0 AS PuedeModificar,  '' AS ValorDefault,  '2' AS TipoFiltro 
 UNION ALL 
 SELECT 'Moneda' as Nombre,0 AS Llave, 3 AS Longitud,  1 AS TipoDato,  1 AS Visible,  'Moneda' AS NombreColumna,  0 AS PuedeInsertar,  0 AS PuedeModificar,  '' AS ValorDefault,  '2' AS TipoFiltro 
 UNION ALL 
 SELECT 'ValorMonedaFacturacion' as Nombre,0 AS Llave, 11 AS Longitud,  3 AS TipoDato,  1 AS Visible,  'ValorMonedaFacturacion' AS NombreColumna,  0 AS PuedeInsertar,  0 AS PuedeModificar,  '' AS ValorDefault,  '2' AS TipoFiltro 
 UNION ALL 
 SELECT 'ValorDolaresPedimento' as Nombre,0 AS Llave, 11 AS Longitud,  0 AS TipoDato,  1 AS Visible,  'ValorDolaresPedimento' AS NombreColumna,  0 AS PuedeInsertar,  0 AS PuedeModificar,  '' AS ValorDefault,  '2' AS TipoFiltro 
 UNION ALL 
 SELECT 'IGIMNPedimento' as Nombre,0 AS Llave, 11 AS Longitud,  0 AS TipoDato,  1 AS Visible,  'IGIMNPedimento' AS NombreColumna,  0 AS PuedeInsertar,  0 AS PuedeModificar,  '' AS ValorDefault,  '2' AS TipoFiltro 
 UNION ALL 
 SELECT 'IdentificadorMS' as Nombre,0 AS Llave, 2 AS Longitud,  1 AS TipoDato,  1 AS Visible,  'IdentificadorMS' AS NombreColumna,  0 AS PuedeInsertar,  0 AS PuedeModificar,  '' AS ValorDefault,  '2' AS TipoFiltro 
 UNION ALL 
 SELECT 'TransporteDecrementables' as Nombre,0 AS Llave, 1 AS Longitud,  1 AS TipoDato,  1 AS Visible,  'TransporteDecrementables' AS NombreColumna,  0 AS PuedeInsertar,  0 AS PuedeModificar,  '' AS ValorDefault,  '2' AS TipoFiltro 
 UNION ALL 
 SELECT 'SeguroDecrementables' as Nombre,0 AS Llave, 1 AS Longitud,  1 AS TipoDato,  1 AS Visible,  'SeguroDecrementables' AS NombreColumna,  0 AS PuedeInsertar,  0 AS PuedeModificar,  '' AS ValorDefault,  '2' AS TipoFiltro 
 UNION ALL 
 SELECT 'Carga' as Nombre,0 AS Llave, 1 AS Longitud,  1 AS TipoDato,  1 AS Visible,  'Carga' AS NombreColumna,  0 AS PuedeInsertar,  0 AS PuedeModificar,  '' AS ValorDefault,  '2' AS TipoFiltro 
 UNION ALL 
 SELECT 'Descarga' as Nombre,0 AS Llave, 1 AS Longitud,  1 AS TipoDato,  1 AS Visible,  'Descarga' AS NombreColumna,  0 AS PuedeInsertar,  0 AS PuedeModificar,  '' AS ValorDefault,  '2' AS TipoFiltro 
 UNION ALL 
 SELECT 'OtrosDecrementables' as Nombre,0 AS Llave, 1 AS Longitud,  1 AS TipoDato,  1 AS Visible,  'OtrosDecrementables' AS NombreColumna,  0 AS PuedeInsertar,  0 AS PuedeModificar,  '' AS ValorDefault,  '2' AS TipoFiltro 
 UNION ALL 
 SELECT 'Referencia' as Nombre,0 AS Llave, 30 AS Longitud,  1 AS TipoDato,  1 AS Visible,  'Referencia' AS NombreColumna,  0 AS PuedeInsertar,  0 AS PuedeModificar,  '' AS ValorDefault,  '2' AS TipoFiltro 
 UNION ALL 
 SELECT 'Proveedor' as Nombre,0 AS Llave, 120 AS Longitud,  1 AS TipoDato,  1 AS Visible,  'Proveedor' AS NombreColumna,  0 AS PuedeInsertar,  0 AS PuedeModificar,  '' AS ValorDefault,  '2' AS TipoFiltro 
 UNION ALL 
 SELECT 'PedimentoOriginalF4' as Nombre,0 AS Llave, 18 AS Longitud,  1 AS TipoDato,  1 AS Visible,  'PedimentoOriginalF4' AS NombreColumna,  0 AS PuedeInsertar,  0 AS PuedeModificar,  '' AS ValorDefault,  '2' AS TipoFiltro 
 UNION ALL 
 SELECT 'i_Cve_Estado' as Nombre,0 AS Llave, 11 AS Longitud,  0 AS TipoDato,  1 AS Visible,  'i_Cve_Estado' AS NombreColumna,  0 AS PuedeInsertar,  0 AS PuedeModificar,  '' AS ValorDefault,  '2' AS TipoFiltro );
GO


