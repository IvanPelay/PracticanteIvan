INSERT INTO [dbo].[Cat016PlantillasReporteador] 
(t_Nombre, t_Descripcion, t_Consulta, t_FormatoSalida, t_Columnas, t_Parametros, t_UsuarioRegistro)
VALUES 
(
'Reporte de Operaciones Aduanales (KROM)', 
'Detalle de pedimentos, facturas y contribuciones filtrado por RFC y fechas.', 
'SELECT o.NumeroReferencia, o.Patente, o.Pago, f.NumeroFactura, p.DescripcionMercancia, co.IGI, co.IVA 
FROM VT016Operaciones_ AS o 
LEFT JOIN Clientes AS c ON c.idCliente = o.idCliente
LEFT JOIN Ped_Facturas AS f ON f.idReferencia = o.idReferencia
LEFT JOIN Ped_Partidas AS p ON p.idFactura = f.idFactura
LEFT JOIN Vt016Contribuciones AS CO ON CO.idReferencia = o.idReferencia
WHERE o.Pago BETWEEN @FechaInicio AND @FechaFin 
AND c.RFC = @RFC 
AND o.TipoOperacion = CASE WHEN @TipoOperacion = 0 THEN o.TipoOperacion ELSE @TipoOperacion END', 
'XLSX', 
'[{"campo":"NumeroReferencia","titulo":"Referencia"},{"campo":"Pago","titulo":"Fecha Pago"},{"campo":"IGI","titulo":"Impuesto IGI"}]', 
'[{"nombre":"@FechaInicio","tipo":"DATE"},{"nombre":"@FechaFin","tipo":"DATE"},{"nombre":"@RFC","tipo":"VARCHAR"},{"nombre":"@TipoOperacion","tipo":"INT","default":"0"}]', 
'IPELAYO'
);


INSERT INTO [dbo].[Enc016ProgramacionReporteador] 
(i_Cve_Plantilla, t_Nombre, t_Descripcion, t_Frecuencia, t_DiasSemana, t_Hora, t_Parametros, f_ProximaEjecucion, t_UsuarioRegistro)
VALUES 
(
1, -- ID de la plantilla anterior
'Cierre Semanal - Walmart México', 
'Reporte automático de operaciones para el cliente Walmart', 
'S', -- Semanal
'2', -- Lunes
'07:00:00', 
'{"@RFC": "WME990813BAA", "@TipoOperacion": 2, "@FechaInicio": "2026-02-01", "@FechaFin": "2026-02-07"}', 
'2026-02-23 07:00:00', 
'IPELAYO'
);

INSERT INTO [dbo].[Bit016GeneracionReporteador] 
(i_Cve_Programacion, f_FechaInicio, f_FechaFin, t_Estatus, i_RegistrosProcesados, t_RutaDocumento, t_ParametrosUsados)
VALUES 
(
1, 
GETDATE(), 
DATEADD(SECOND, 45, GETDATE()), -- Duró 45 segundos
'COMPLETADO', 
250, 
'C:\Reportes\2026\02\Walmart_Semana5.xlsx', 
'{"@RFC": "WME990813BAA", "EjecutadoPor": "Scheduler"}'
);