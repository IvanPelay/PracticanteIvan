  
  
  
  
--CREATE  VIEW [dbo].[Vt016LayoutGrammer] as  
  
SELECT --c.RazonSocial,o.idReferencia, o.NumeroReferencia,   
  concat(substring(cast(year(o.Pago) as CHAR(4)),3,2),'-',o.ClaveAduana, '-',o.Patente,'-', o.NumeroPedimento ) as NumeroPedimento,  
  o.ClaveAduana,   
  o.Pago,   
  o.TipoDeCambio,   
  case when ROW_NUMBER() OVER(PARTITION BY o.idReferencia,fr.orden ORDER BY fr.orden, par.idPartida desc)=1   
            then   
            isnull((SELECT cont.ImporteContribucion   
                    FROM SysExpert.dbo.Ped_ContribucionesFraccion AS cont WITH (NOLOCK)   
                    INNER JOIN SysExpert.dbo.Contribuciones AS conc WITH (NOLOCK) ON conc.idContribucion = cont.idContribucion   
                    WHERE conc.ClaveContribucion = 3 --and cont.idFormaPago=1   
                    AND cont.idFraccion = fr.idPedFraccion)  
       ,0)   
      else null   
  end as IVA,  
  o.Clave,  
  o.FletesUSD,   
  o.SegurosUSD,   
  o.EmbalajeUSD,  
  o.OtrosIncrementablesUSD,   
  case when o.Clave='R1'   
             then imr.DTA   
       else cont.DTA   
  end as DTA,  
  v.ValorComercial as ValorComercialPedimento,  
  v.ValorAduana as ValorAduanaPedimento,  
  o.NuevasObservaciones,  
  '' DescargoFechaFactura,  
  ''RecibidaDe,  
  ''NotaPedimento,  
  case when o.Clave='R1' then imr.PRV else cont.PRV end PRV,  
  f.NumeroFactura,   
  p.ClaveProveedor as CodigoProveedor,  
  cast(f.FechaFactura as DATE)FechaFactura,   
  e.Equivalencia FactorMoneda,  
  --pa.Clave as NumeroParteTrafico,  
  CASE WHEN CHARINDEX('-', pa.Clave) > 0   
              THEN LEFT(STUFF(pa.Clave, CHARINDEX('-', pa.Clave), LEN(pa.Clave), ''),14)  
              ELSE LEFT(pa.Clave,14)   
  END AS NumeroParteComercioExterior,  
  concat(replace(tg.Fraccion,'.',''),nico.nico) as Fraccion,  
  CASE WHEN ROW_NUMBER() OVER(PARTITION BY o.idReferencia,fr.orden ORDER BY fr.orden, par.idPartida desc)=1   
             THEN   
      isnull((SELECT SUM(cont.ImporteContribucion)  
         FROM SysExpert.dbo.Ped_ContribucionesFraccion AS cont WITH (NOLOCK)   
         INNER JOIN SysExpert.dbo.Contribuciones AS conc WITH (NOLOCK) ON conc.idContribucion = cont.idContribucion   
         INNER JOIN SysExpert.dbo.FormasDePago as fpiva on fpiva.idFormapago=cont.idFormaPago  
         WHERE conc.ClaveContribucion = 6 AND cont.idFraccion = fr.idPedFraccion)  
        ,0)   
     ELSE NULL   
  END  AS TasaIGI,   
  isnull(stuff((select distinct ' | ' + i.Identificador   
                      from SysExpert.dbo.Ped_IdentificadoresFraccion as co WITH (NOLOCK)  
                      inner join SysExpert.dbo.Identificadores as i on i.idIdentificador=co.idIdentificador  
                     where co.idPedFraccion=fr.idPedFraccion  
      FOR XML PATH('')), 1,2 ,'')  
         ,'') AS TipoTasa,  
  umc.Abreviacion AS UnidadMC,  
  par.PrecioUnitario,   
  par.CantidadComercial,  
  po.ClaveM3 AS PaisOrigen,  
  pv.ClaveM3 AS PaisVendedor,  
  '' Nota1Pedimento  
  ,fp.ClaveFormaPago AS FP_IGI  
  ,i.Incoterm  
  ,concat(co.Complemento,' ',co.Complemento2,' ',co.Complemento3) AS Tratado  
  ,CASE WHEN id.idIdentificador IS NULL   
             THEN 'No'   
       ELSE 'Si'   
  END AS Id_EB  
  ,'' EnConsignacion  
  ,'' NotaIntena2  
  , cg.eDocument COVE  
  ,CASE WHEN o.Clave='R1'   
              THEN imr.MULT   
     ELSE cont.MULT   
  END AS Multas  
  ,CASE WHEN o.Clave='R1'   
              THEN imr.REC   
     ELSE cont.REC   
  END AS Recargos  
  ,'' NotaInterna2  
  ,CASE WHEN o.Clave='R1'   
               THEN imr.CNT   
      ELSE cont.CNT   
  END AS CNT  
  ,fr.ORDEN  
  ,CASE WHEN umc.ClaveUnidad in(2)   
              THEN   
      .001  
               ELSE   
                 CAST(CASE WHEN umc.ClaveUnidad in (6,8,1,3,4,5)   
                            THEN   
                                            1  
                                       ELSE   
                                    CASE WHEN umc.ClaveUnidad in(14,11)   
                       THEN   
                                                   1000  
                                   ELSE   
                                                   CASE WHEN umc.claveUnidad in (18)   
                             THEN   
                                                                   100   
                                                              ELSE   
                                                                 0   
                                                    END  
                                    END   
                          END AS INT)  
   END AS FactorDeConversion  
  ,umt.Abreviacion as UnidadTarifa  
  ,par.CantidadTarifa  
  ,ISNULL((SELECT distinct fpiva.ClaveFormaPago  
      FROM SysExpert.dbo.Ped_ContribucionesFraccion AS cont WITH (NOLOCK)   
      INNER JOIN SysExpert.dbo.Contribuciones AS conc WITH (NOLOCK) ON conc.idContribucion = cont.idContribucion   
      inner join SysExpert.dbo.FormasDePago as fpiva on fpiva.idFormapago=cont.idFormaPago  
      WHERE conc.ClaveContribucion = 3 AND cont.idFraccion = fr.idPedFraccion)  
     ,0) AS FormaPagoIVAPartida  
  ,o.FechaEntrada  
  ,o.Regimen  
  ,'' TipoOperacion  
  ,CASE WHEN idp.idIdentificador IS null   
              THEN 'No'   
     ELSE 'Si'   
  END AS IdentificadorPC  
  ,CASE WHEN idim.idIdentificador IS null   
             THEN 'No'   
       ELSE 'Si'   
  END AS IdentificadorIM  
  ,CASE WHEN ROW_NUMBER() OVER(PARTITION BY o.idReferencia,fr.orden ORDER BY fr.orden, par.idPartida desc)=1   
              THEN   
      isnull((select SUM(cp.ImporteContribucion)   
         from SysExpert.dbo.Ped_ContribucionesFraccion as cp  WITH (NOLOCK)   
         INNER JOIN SysExpert.dbo.Contribuciones AS conc WITH (NOLOCK) ON conc.idContribucion = cp.idContribucion   
         where cp.idFraccion =fr.idPedFraccion  
            and cp.idContribucion=5)  
        ,0)  
     ELSE NULL   
  END AS IEPS  
  ,CASE WHEN ROW_NUMBER() OVER(PARTITION BY o.idReferencia,fr.orden ORDER BY fr.orden, par.idPartida desc)=1   
              THEN   
      isnull((SELECT DISTINCT fpiep.ClaveFormaPago  
         FROM SysExpert.dbo.Ped_ContribucionesFraccion AS cont WITH (NOLOCK)   
         INNER JOIN SysExpert.dbo.Contribuciones AS conc WITH (NOLOCK) ON conc.idContribucion = cont.idContribucion   
         inner join SysExpert.dbo.FormasDePago as fpiep on fpiep.idFormapago=cont.idFormaPago  
         WHERE conc.ClaveContribucion = 5 AND cont.idFraccion = fr.idPedFraccion),0)   
     ELSE NULL   
  END AS FormaPagosIEPS,  
  isnull((SELECT cont.ImporteContribucion  
         FROM SysExpert.dbo.Ped_ContribucionesPedimento AS cont WITH (NOLOCK)   
         INNER JOIN SysExpert.dbo.Contribuciones AS conc WITH (NOLOCK) ON conc.idContribucion = cont.idContribucion   
         WHERE conc.ClaveContribucion = 23 --and cont.idFormaPago=1   
         AND cont.idReferencia = o.idReferencia)  
   ,0) as IVAPRV,  
  pm.ClaveMoneda AS Moneda,  
  eq.Equivalencia as ValorMonedaFacturacion,  
  v.ValorDolares as ValorDolaresPedimento,  
  ISNULL((select sum(conf.ImporteContribucion)   
      from SysExpert.dbo.Ped_ContribucionesFraccion as conf   
                  inner join SysExpert.dbo.Ped_Fraccion as Frac WITH (NOLOCK) on Frac.idReferencia=o.idReferencia   
      where conf.idFraccion = frac.idPedFraccion and conf.idContribucion in (42,6))  
    ,0) AS IGIMNPedimento,  
   CASE WHEN MS.idIdentificador IS NULL   
             THEN 'No'   
       ELSE 'Si'   
  END AS IdentificadorMS,  
  '' as TransporteDecrementables ,  
  '' as SeguroDecrementables,   
  '' as Carga,  
  '' as Descarga,   
  '' as OtrosDecrementables,  
  o.NumeroReferencia as Referencia,  
  p.RazonSocial AS Proveedor,  
  descargos.PedimentoOriginal AS PedimentoOriginalF4,  
  1 as i_Cve_Estado  
from SysExpert.dbo.vt016Operaciones_ as o   
inner join SysExpert.dbo.Clientes as c WITH (NOLOCK) on c.idCliente=o.idcliente  
left join SysExpert.dbo.Vt015ValoresTotalesPartidasPedimento as v on v.idreferencia=o.idReferencia  
inner join SysExpert.dbo.Ped_Fraccion as fr WITH (NOLOCK) on fr.idReferencia=o.idReferencia   
LEFT JOIN SYSEXPERT.DBO.Tigie_Nico AS Nico ON Nico.idTigie_Nico = Fr.idTigie_Nico  
left join SysExpert.dbo.Ped_Partidas as par WITH (NOLOCK) on par.idOrdenFraccion=fr.idPedFraccion  
left join SysExpert.dbo.AsignacionFacturas as a  WITH (NOLOCK) on a.idReferencia = fr.idReferencia and a.idFactura=par.idFactura  
left join SysExpert.dbo.Ped_Facturas as f  WITH (NOLOCK) on f.idFactura = a.idFactura  
left join SysExpert.dbo.COVEGeneral AS cg WITH (NOLOCK)ON cg.idFactura = f.idFactura  
left join SysExpert.dbo.Incoterms as i  WITH (NOLOCK) on i.idIncoterm=f.idIncoterm  
left join SysExpert.dbo.Proveedores as p  WITH (NOLOCK) on p.idProveedor = f.idProveedor  
left join SysExpert.dbo.Partes as pa  WITH (NOLOCK) on pa.idParte = par.idParte  
left join SysExpert.dbo.Monedas_Equivalencias as e  WITH (NOLOCK) on e.idEquivalencia=f.idEquivalencia  
left join SysExpert.dbo.TIGIE as tg  WITH (NOLOCK) on tg.idFraccion=fr.idFraccion  
left join SysExpert.dbo.UnidadesMedida as umc  WITH (NOLOCK) on umc.idUnidad=fr.idUMC  
left join SysExpert.dbo.UnidadesMedida as umt  WITH (NOLOCK) on umt.idUnidad=tg.idUnidad  
left join SysExpert.dbo.Paises as po WITH (NOLOCK)on po.idPais=fr.idPaisOrigen  
left join SysExpert.dbo.Paises as pv WITH (NOLOCK) on pv.idPais=fr.idPaisVendedor  
left join SysExpert.dbo.Paises as pm on pm.idPais=f.idPaisMoneda  
left join SysExpert.dbo.Monedas_Equivalencias AS eq WITH (NOLOCK) on eq.idEquivalencia = f.idEquivalencia  
left join SysExpert.dbo.Ped_IdentificadoresFraccion as id WITH (NOLOCK)on id.idIdentificador=83 and id.idPedFraccion=fr.idPedFraccion  
left join SysExpert.dbo.Ped_IdentificadoresFraccion as co WITH (NOLOCK) on co.idPedFraccion=fr.idPedFraccion and co.idIdentificador=41  
left join SysExpert.dbo.Ped_IdentificadoresPedimento as idp WITH (NOLOCK) on idp.idReferencia=o.idReferencia and idp.idIdentificador=2   
left join SysExpert.dbo.Ped_IdentificadoresPedimento as MS WITH (NOLOCK) on MS.idReferencia=o.idReferencia and MS.idIdentificador=157   
left join SysExpert.dbo.Ped_IdentificadoresPedimento as idim WITH (NOLOCK) on idim.idReferencia=o.idReferencia and idim.idIdentificador=7   
left join SysExpert.dbo.Ped_ContribucionesFraccion as  pc WITH (NOLOCK) on pc.idFraccion =fr.idPedFraccion and pc.idContribucion=6  and pc.idTipoTasa <>2  
left join SysExpert.dbo.FormasDePago as fp WITH (NOLOCK) on fp.idFormapago=pc.idFormaPago  
left join SysExpert.dbo.Vt016Contribuciones as cont on cont.idReferencia=o.idReferencia  
left join SysExpert.dbo.Vt016Contribuciones_Recti as imr on imr.idReferencia=o.idReferencia  and o.Clave='R1'  
left join (  
select  g.idreferencia  
,max(concat(substring(cast(year(d.FechaOriginal) as varchar(4)),3,2),' ',  
substring(a.ClaveAduana,1,2),' ',d.PatenteOriginal,' ',d.PedimentoOriginal))PedimentoOriginal  
from   
sysexpert.dbo.ped_grales as  g WITH (nolock)  
inner join sysexpert.dbo.Clientes as c1 WITH (nolock) on c1.idcliente=g.idcliente  
inner join sysexpert.dbo.Ped_Descargas  as d WITH (nolock)on d.idreferencia=g.idreferencia  
inner join sysexpert.dbo.Aduanas as a WITH (nolock)on a.idaduana=d.idAduanaOriginal  
inner join sysexpert.dbo.CvesDocs AS cd WITH (nolock) ON cd.idClave = idCveDocOriginal  
where g.Acuse<>'' and cd.Clave in ('IN','V1')  
and c1.RFC ='GAP9801082RA'  
group by g.idReferencia  
)Descargos on Descargos.idReferencia=o.idreferencia and o.clave='F4'  
where c.RFC ='GAP9801082RA'-- and o.numeroReferencia='RKU20-11582'  
 and o.firmae<>''   
 and o.FirmaBancaria<>''--2295  
 and o.TipoOperacion=1  
 and o.Pago between '2024-05-01' and '2024-05-31'  
 --And o.NumeroReferencia IN('ALC22-1336')  
--,'RKU22-09891'  
--,'RKU22-07632'  
--,'RKU22-08752'  
--,'RKU22-09061'  
--,'ALC22-1409'  
--,'RKU22-07616'  
--,'RKU22-06686'  
--,'RKU22-09451'  
--,'RKU22-09289'  
--,'RKU22-09204'  
--,'RKU22-09021'  
--,'RKU22-09677'  
--,'RKU22-09039'  
--,'RKU22-09138'  
--)  
--and o.ClaveAduana=430 and o.Patente=3921   
--and o.NumeroPedimento='0010043'  
 --and fr.ORDEN in (152,165)  
   
-- and concat(substring(cast(year(o.Pago) as CHAR(4)),3,2),' ',o.ClaveAduana, ' ',o.Patente,' ', o.NumeroPedimento ) IN (  
--'20 160 3921 0000083',  
--'20 160 3921 0000125',  
--'20 160 3921 0000134',  
--'20 160 3921 0000500',  
--'20 430 3921 0000708',  
--'20 430 3921 0000709',  
--'20 430 3921 0000723',  
--'20 430 3921 0000726',  
--'20 430 3921 0000731',  
--'20 430 3921 0000772',  
--'20 430 3921 0000775',  
--'20 430 3921 0000874')  
  
  
  
/*select o.NumeroReferencia ,o.pago,o.TipoDeCambio,o.Clave ClavePedimento,  
  concat(substring(cast(year(o.Pago) as CHAR(4)),3,2),' ',o.ClaveAduana, ' ',o.Patente,' ', o.NumeroPedimento ) as NumeroPedimento,  
  v.idReferencia,  
  FirmaBancaria,   
  (select count(*) from ped_contenedores as con where con.idreferencia=o.idreferencia and con.idcontenedor is not null)Contenedores,  
  v.ValorDolares,  
  v.ValorAduana,   
  v.ValorComercial,   
    
  round((select sum(par.ImporteUSD*o2.TipoDeCambio )  
    from ped_grales as o2  
    inner join Ped_Fraccion as fr WITH (NOLOCK) on fr.idReferencia=o2.idReferencia  
    inner join Ped_Partidas as par WITH (NOLOCK) on par.idOrdenFraccion=fr.idPedFraccion  
    inner join AsignacionFacturas as a  WITH (NOLOCK) on a.idReferencia = fr.idReferencia and a.idFactura=par.idFactura  
    inner join Ped_Facturas as f  WITH (NOLOCK) on f.idFactura = a.idFactura  
    left join Monedas_Equivalencias as e  WITH (NOLOCK) on e.idEquivalencia=f.idEquivalencia  
    where fr.idReferencia=o.idReferencia),2) ValorComercialMercancias,  
  
  ROUND((select sum(par.ValorMciaUSD*o2.TipoDeCambio )  
    from ped_grales as o2  
    inner join Ped_Fraccion as fr WITH (NOLOCK) on fr.idReferencia=o2.idReferencia  
    inner join Ped_Partidas as par WITH (NOLOCK) on par.idOrdenFraccion=fr.idPedFraccion  
    inner join AsignacionFacturas as a  WITH (NOLOCK) on a.idReferencia = fr.idReferencia and a.idFactura=par.idFactura  
    inner join Ped_Facturas as f  WITH (NOLOCK) on f.idFactura = a.idFactura  
    left join Monedas_Equivalencias as e  WITH (NOLOCK) on e.idEquivalencia=f.idEquivalencia  
    where fr.idReferencia=o.idReferencia),2) ValorComercialMercanciasValorUSD,  
  
  --round((select sum(isnull(par.ImporteME*fr.ValorAduana/isnull(fr.ImporteME,1),0))  
  round((select sum(isnull((((par.ValorMciaME*e.Equivalencia)*o2.TipoDeCambio)*o2.FactorIncrementables),0))  
  --round((select sum(isnull(par.ValorMciaME*fr.ValorAduana/isnull(fr.ImporteME,1),0))  
    from ped_grales as o2  
    inner join Ped_Fraccion as fr WITH (NOLOCK) on fr.idReferencia=o2.idReferencia  
    inner join Ped_Partidas as par WITH (NOLOCK) on par.idOrdenFraccion=fr.idPedFraccion  
    inner join AsignacionFacturas as a  WITH (NOLOCK) on a.idReferencia = fr.idReferencia and a.idFactura=par.idFactura  
    inner join Ped_Facturas as f  WITH (NOLOCK) on f.idFactura = a.idFactura  
    left join Monedas_Equivalencias as e  WITH (NOLOCK) on e.idEquivalencia=f.idEquivalencia  
    where fr.idReferencia=o.idReferencia),2)ValorAduanaMercanciasConInc    
  
from vt016Operaciones_ as o   
  inner join Clientes as c WITH (NOLOCK) on c.idCliente=o.idcliente  
  left join Vt015ValoresTotalesPartidasPedimento as v on v.idreferencia=o.idReferencia  
  where c.RFC='KME850218KA9'  
  and o.numeroReferencia='RKU23-08898'  
  and o.firmae<>'' --and o.FirmaBancaria<>''  
  and o.TipoOperacion=1  
  and o.Pago between '2023-10-01' and '2023-10-31'*/  
   --and concat(substring(cast(year(o.Pago) as CHAR(4)),3,2),' ',o.ClaveAduana, ' ',o.Patente,' ', o.NumeroPedimento ) IN ('20 160 3921 0000124')  
--'20 160 3921 0000083',  
--'20 160 3921 0000125',  
--'20 160 3921 0000134',  
--'20 160 3921 0000500',  
--'20 430 3921 0000708',  
--'20 430 3921 0000709',  
--'20 430 3921 0000723',  
--'20 430 3921 0000726',  
--'20 430 3921 0000731',  
--'20 430 3921 0000772',  
--'20 430 3921 0000775',  
--'20 430 3921 0000874')  
  --and o.NumeroPedimento = '0010043'  