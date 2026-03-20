use solium
declare @claveexpediente int
declare @obligatorios int
declare @diasarestar int=3

set @claveexpediente = 30
/*
43 -- Ferrero
30 -- Continental Automotive
28 -- Hisense
8 -- Continental Treat
9 -- Continental sepa dios
22 -- Becton Dickenson
27 -- Molex
31 -- CNH Comercial
32 -- CNH Industrial
33 -- CBI
35 -- CBI
36 -- CBI
37 -- CBI
38 -- CBI
39 -- CBI
44 -- Colgate
45 --Mission Hills
48 -- DIAGEO Operativa
49 -- DIAGEO Operativa
50 -- DIAGEO Operativa
51 -- DIAGEO Operativa
*/

if @claveexpediente in(2,35,37,39) begin set @diasarestar=0 end


set @obligatorios= 1 --(select COUNT(*) from Det044GeneracionExpedientes where i_Cve_GeneracionExpediente=@claveexpediente and i_Obligatorio=1 and i_Cve_Estado=1)

--select @obligatorios   echo F|xcopy /y "M:\MDO\Clientes\Multiuso\ALC\2022\FME920608SM3\FME920608SM3_001-110-000036914_FAC.pdf" "\\10.66.3.253\RepositorioExpedientes\FME920608SM3\2023\01\23-510-3921-3000046\FME920608SM3_001-110-000036914_FAC.pdf"


select 
case
when i_Cve_GeneracionExpediente =  4 then
'echo F|xcopy /y "' +  t_rutadocumento  +'"'+' "' +
concat('\\10.66.3.138\Bostik\Pendientes\',REPLACE((select * from Fn044ConfiguracionNombreCarpeta(i_Cve_MaestroOperaciones,i_Cve_GeneracionExpediente,i_ConfiguracionSalida,i_tipodocumentacion,i_Cve_Documento,t_NumeroReferencia,i_Cve_TipoArchivo)),'BME631003N72\',''),
concat(right(i_anio,2),'-',i_aduanaes,'-',i_patente,'-',i_numeropedimento,(select * from Fn044ConfiguracionNombreExpediente(i_Cve_MaestroOperaciones,i_Cve_GeneracionExpediente,i_ConfiguracionSalida,i_tipodocumentacion,i_Cve_Documento,t_NumeroReferencia,'',i_Cve_TipoArchivo)),'"') )
else
'echo F|xcopy /y "' +  t_rutadocumento  +'"'+' "' +
concat(t_rutadestino,REPLACE((select * from Fn044ConfiguracionNombreCarpeta(i_Cve_MaestroOperaciones,i_Cve_GeneracionExpediente,i_ConfiguracionSalida,i_tipodocumentacion,i_Cve_Documento,t_NumeroReferencia,i_Cve_TipoArchivo)),'BDM571004IZ6\',''),
(select * from Fn044ConfiguracionNombreExpediente(i_Cve_MaestroOperaciones,i_Cve_GeneracionExpediente,i_ConfiguracionSalida,i_tipodocumentacion,i_Cve_Documento,t_NumeroReferencia,Consecutivo,i_Cve_TipoArchivo)),'"') 
end as t_ruta--,Consecutivo,f_FechaEvento,i_Cve_MaestroOperaciones
--i_Cve_MaestroOperaciones,i_TipoDocumentacion,i_ConfiguracionSalida,
--i_Cve_Documento,i_Cve_TipoDocumento,i_Cve_TipoArchivo,i_ConfiguracionAgrupacion,t_RutaDocumento,t_NombreDocumentoOriginal,consecutivo,t_numeroreferencia
from(
select bita.t_RutaDestino,
bita.i_Cve_MaestroOperaciones,bita.t_NumeroReferencia,bita.i_TipoDocumentacion,bita.i_ConfiguracionSalida,bita.i_Cve_GeneracionExpediente,bita.f_FechaEvento,
bitadet.i_Cve_Documento,bitadet.i_Cve_TipoDocumento,bitadet.i_Cve_TipoArchivo,bitadet.i_ConfiguracionAgrupacion,bitadet.t_RutaDocumento,bitadet.t_NombreDocumentoOriginal,
case 
when  bita.i_Cve_GeneracionExpediente=30 then
case
when i_Cve_TipoDocumento =109 then ROW_NUMBER() OVER(PARTITION BY t_numeroreferencia,i_Cve_TipoDocumento ORDER BY i_Cve_TipoDocumento ASC) 
when i_Cve_TipoDocumento =119 then ROW_NUMBER() OVER(PARTITION BY t_numeroreferencia,i_Cve_TipoDocumento,i_cve_doctovucem ORDER BY i_Cve_TipoDocumento ASC) 
else
ROW_NUMBER() OVER(PARTITION BY t_numeroreferencia,i_Cve_TipoDocumento ORDER BY i_Cve_TipoDocumento ASC)
end
when  bita.i_Cve_GeneracionExpediente=4 then ''
ELse
ROW_NUMBER() OVER(PARTITION BY t_numeroreferencia,i_Cve_TipoDocumento ORDER BY i_Cve_TipoDocumento ASC)
END as Consecutivo,
bita.i_anio,bita.i_aduanaes,bita.i_patente,bita.i_numeropedimento
from Bit044GeneracionExpedientes bita  with(nolock) 
inner join Bit044DetGeneracionExpedientes bitadet with(nolock) on bita.i_Cve_BitacoraExpedientes=bitadet.i_Cve_BitacoraExpedientes and bita.i_Cve_MaestroOperaciones=bitadet.i_Cve_MaestroOperaciones
where bita.i_Cve_GeneracionExpediente=@claveexpediente
and bita.i_Cve_Estatus in(0,3) --and dateadd(day,@diasarestar,cast(f_FechaEvento as date))<=cast(GETDATE() as date)
--usar para bostik
--and CONVERT(DATE,bita.f_fechaevento) between '2025-12-01' and '2025-12-31'
--AND CONVERT(DATE,bita.f_fechaevento) >= '2026-01-01' --and '2025-12-31'
--and i_Cve_TipoDocumento in (73,74)
--------------------
and bita.i_Cve_MaestroOperaciones in(--992498) --and i_Cve_TipoDocumento in(110)
select distinct i_Cve_MaestroOperaciones from Bit044DetGeneracionExpedientes  where i_Aux_GeneracionExpediente=@claveexpediente and i_procesado=0 and i_cve_estatus in(0,3,1)group by i_Cve_MaestroOperaciones having count(distinct i_Cve_TipoDocumento) >=@obligatorios)
--1293964
--,1322573
--,1328959
--,1335589
--,1336005
--)
)x
--select i_Cve_MaestroOperaciones/*,COUNT(distinct i_Cve_TipoDocumento) */from Bit044DetGeneracionExpedientes  where i_Aux_GeneracionExpediente=8 and i_procesado=0 and i_cve_estatus in(0,3)group by i_Cve_MaestroOperaciones having count(distinct i_Cve_TipoDocumento) >=3


--
--habilitar para bostik
--update Bit044GeneracionExpedientes set i_Cve_Estatus=1, f_FechaCreacionExpediente=GETDATE() where i_Cve_MaestroOperaciones in(
--select i_Cve_MaestroOperaciones
--from Bit044DetGeneracionExpedientes  
--where i_Aux_GeneracionExpediente=@claveexpediente 
--and i_procesado=0 and i_cve_estatus in(0,3)
--group by i_Cve_MaestroOperaciones having count(distinct i_Cve_TipoDocumento) >=@obligatorios
--) 
--and f_fechaevento between '01-01-2024' and '02-01-2024' and i_Cve_GeneracionExpediente=@claveexpediente

--update Bit044DetGeneracionExpedientes set i_Cve_Estatus=1, i_Procesado=1 where i_Cve_MaestroOperaciones in(
--select i_Cve_MaestroOperaciones from Bit044GeneracionExpedientes  where i_cve_GeneracionExpediente=@claveexpediente and i_cve_estatus in(1) and f_fechaevento between '01-01-2024' and '02-01-2024'
--) and i_Aux_GeneracionExpediente=@claveexpediente
--and i_Cve_Estatus in(0,3)

----------------------------------------------------------------------SIEMPRE TENER COMENTADO LO SIGUIENTE ANTES DE EJECUTAR

----habilitar cuando se quiera actualizar a procesado en el encabezado y detalle de las bitacoras de expedientes demas clientes
--update Bit044GeneracionExpedientes set i_Cve_Estatus=1, f_FechaCreacionExpediente=GETDATE() where i_Cve_MaestroOperaciones in(
--select bitdet.i_Cve_MaestroOperaciones
--from Bit044DetGeneracionExpedientes  as bitdet with(nolock) 
--inner join Bit044GeneracionExpedientes as bitenc with(nolock) on bitenc.i_Cve_BitacoraExpedientes=bitdet.i_Cve_BitacoraExpedientes
--where bitdet.i_Aux_GeneracionExpediente=@claveexpediente 
--and bitdet.i_procesado=0 and bitdet.i_cve_estatus in(0,3) and bitdet.i_aux_GeneracionExpediente=@claveexpediente and bitenc.f_fechaevento between '01-10-2025' and '31-10-2025'--and dateadd(day,@diasarestar,cast(bitenc.f_FechaEvento as date))<=cast(GETDATE() as date)
--group by bitdet.i_Cve_MaestroOperaciones having count(distinct bitdet.i_Cve_TipoDocumento) >=@obligatorios)
--and i_Cve_GeneracionExpediente=@claveexpediente


--update Bit044DetGeneracionExpedientes set i_Cve_Estatus=1, i_Procesado=1 where i_Cve_MaestroOperaciones in(
--select bitdet.i_Cve_MaestroOperaciones
--from Bit044DetGeneracionExpedientes  as bitdet with(nolock) 
--inner join Bit044GeneracionExpedientes as bitenc with(nolock) on bitenc.i_Cve_BitacoraExpedientes=bitdet.i_Cve_BitacoraExpedientes
--where bitdet.i_Aux_GeneracionExpediente=@claveexpediente 
--and bitdet.i_procesado=0 and bitdet.i_cve_estatus in(0,3) and bitdet.i_aux_GeneracionExpediente=@claveexpediente and bitenc.f_fechaevento between '01-10-2025' and '31-10-2025'-- and dateadd(day,@diasarestar,cast(bitenc.f_FechaEvento as date))<=cast(GETDATE() as date)
--group by bitdet.i_Cve_MaestroOperaciones having count(distinct bitdet.i_Cve_TipoDocumento) >=@obligatorios)
--and i_Aux_GeneracionExpediente=@claveexpediente