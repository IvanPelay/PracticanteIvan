Imports System
Imports System.IO
Imports System.Threading
Imports ReporteadorConsola.Servicios
Imports ReporteadorConsola.Modelos
Imports Newtonsoft.Json
Imports System.Configuration

Module Module1

    'Configuracion a la BD
    Private ReadOnly _intervaloEjecucion As Integer = 60000
    Private ReadOnly _cadenaConexion As String = ConfigurationManager.ConnectionStrings("Solium").ConnectionString

    Private _ejecutando As Boolean = True
    Private _servicioDB As ServicioBaseDeDatos
    Private _servicioExcel As ServicioGeneracionExcel
    Private _config As ConfiguracionReporteador

    Sub Main()
        Console.WriteLine("========================================")
        Console.WriteLine("REPORTEADOR AUTOMÁTICO - MODULO EJECUCIÓN")
        Console.WriteLine($"Inicio: {DateTime.Now:yyyy-MM-dd HH:mm:ss}")
        Console.WriteLine("========================================")

        Try
            'Cargamos la configuracion
            _config = ConfiguracionReporteador.Instance
            Console.WriteLine($"Configuración cargada desde: {AppDomain.CurrentDomain.SetupInformation.ConfigurationFile}")

            Dim erroresConfiguracion = _config.ValidarConfiguracion()
            If erroresConfiguracion.Count > 0 Then
                Console.WriteLine("Errores en la configuración:")
                For Each miss In erroresConfiguracion
                    Console.WriteLine($" - {miss}")
                Next
                Console.WriteLine("Presione una tecla para salir...")
                Console.ReadKey()
                Return
            End If

            'Inicializar los servicios
            _servicioDB = New ServicioBaseDeDatos(_cadenaConexion, _config)
            _servicioExcel = New ServicioGeneracionExcel()

            'configurar manejador
            AddHandler Console.CancelKeyPress, AddressOf ManejadorSalida

            'mostrar configuracion
            MostrarConfiguracion()

            'bucle principal del motor
            While _ejecutando
                EjecutarCicloProcesamiento()
                Thread.Sleep(_intervaloEjecucion)
            End While

        Catch ex As Exception
            Console.WriteLine($"[ERROR FATAL] {ex.Message}")
            Console.WriteLine(ex.StackTrace)
            LogErrorGlobal("Main", ex)
        End Try

        Console.WriteLine("Reporteador Detenido")
    End Sub

    'mostramos la configuracion actual
    Private Sub MostrarConfiguracion()
        Console.WriteLine("------------------------------------------")
        Console.WriteLine("Configuración actual:")
        Console.WriteLine($"  - BD: {_config.Conexion}")
        Console.WriteLine($"  - Directorio salida: {_config.DirectorioSalida}")
        Console.WriteLine($"  - Directorio plantillas: {_config.DirectorioPlantillas}")
        Console.WriteLine($"  - Directorio logs: {_config.DirectorioLogs}")
        Console.WriteLine($"  - Intervalo: {_config.IntervaloEjecucion / 1000} seg")
        Console.WriteLine($"  - Timeout consultas: {_config.CommandTimeout} seg")
        Console.WriteLine($"  - Usuario sistema: {_config.UsuarioSistema}")
        Console.WriteLine("------------------------------------------")
    End Sub

    'Ejecutamos un ciclo completo de procesamiento
    Private Sub EjecutarCicloProcesamiento()
        Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] Iniciando ciclo de procesamiento")

        Try
            'Obtenemos los reportes pendientes
            Dim reportesPendientes = _servicioDB.ObtenerReportesPendientes()

            If reportesPendientes Is Nothing OrElse reportesPendientes.Count = 0 Then
                Console.WriteLine("No hay reportes pendientes por procesar")
                Return
            End If

            Console.WriteLine($"Se encontraron {reportesPendientes.Count} reportes pendientes")

            For Each reporte In reportesPendientes
                ProcesarReporte(reporte)
            Next

        Catch ex As Exception

            Console.WriteLine($"[ERROR] EjecutarCicloProcesamiento: {ex.Message}")
            LogErrorGlobal("EjecutarCicloProsesamiento", ex)

        End Try
    End Sub

    'Procesar un reporte individual
    Private Sub ProcesarReporte(reporte As ReportePendiente)
        Console.WriteLine($"Procesando: {reporte.NombreReporte} (Gen: {reporte.i_Cve_Generacion})")
        Console.WriteLine($"  Modo: {If(reporte.TieneSP, "SP → " & reporte.t_NombreSP, "Vista → " & reporte.t_NombreVista)}")

        Dim rutaDocumento As String = Nothing
        Dim registrosProcesados = 0
        Dim exito As Boolean = False
        Dim errorMsg As String = Nothing

        Try
            'Validamos que tenemos lo minimo necesario
            ValidarReporte(reporte)

            'ejecutar consulta
            Console.WriteLine(" Ejecutando Consulta...")
            Dim datos = _servicioDB.EjecutarConsultaReporte(reporte)
            registrosProcesados = If(datos Is Nothing, 0, datos.Rows.Count)
            Console.WriteLine($" Registros obtenidos: {registrosProcesados}")

            'generar archivo
            Select Case reporte.t_FormatoSalida.ToUpper()
                Case "XLSX", "XLS", ""
                    rutaDocumento = _servicioExcel.GenerarExcelDesdePlantilla(
                        reporte.t_RutaPlantilla,
                        datos,
                        reporte.t_ColumnasConfig,
                        reporte.NombreReporte)
                Case Else
                    Throw New Exception($"Formato no soportado: {reporte.t_FormatoSalida}")
            End Select

            Console.WriteLine($" Archivo generado: {rutaDocumento}")
            exito = True

        Catch ex As Exception

            errorMsg = ex.Message
            Console.WriteLine($" ERROR {errorMsg}")
            LogErrorGlobal($"Procesar Reporte_{reporte.i_Cve_Generacion}", ex)
        End Try


        'Actualizar la bitacora
        Try
            _servicioDB.ActualizarEstadoGeneracion(
                reporte.i_Cve_Generacion,
                exito,
                rutaDocumento,
                registrosProcesados,
                errorMsg
            )
            Console.WriteLine($" Estado Actualizado: {If(exito, "COMPLETADO", "FALLIDO")}")
        Catch ex As Exception
            Console.WriteLine($" Error Actualizando a la Bitácora: {ex.Message} ")
        End Try

        Console.WriteLine($"Procesamiento Finalizado: {If(exito, "EXITO", "FALLIDO")}")
        Console.WriteLine()

    End Sub

    'Validar que el reporte contenga los valores minimos necesarios
    Private Sub ValidarReporte(reporte As ReportePendiente)
        If String.IsNullOrEmpty(reporte.t_NombreVista) Then
            Throw New Exception("La Vista es obligatoria y esta vacia")
        End If

        If String.IsNullOrEmpty(reporte.t_RutaPlantilla) AndAlso reporte.t_FormatoSalida.ToUpper() = "XLSX" Then
            Throw New Exception("La ruta de plantilla es Requerida para el Repórte")
        End If

        If Not File.Exists(reporte.t_RutaPlantilla) AndAlso reporte.t_FormatoSalida.ToUpper() = "XLSX" Then
            Throw New Exception($"No se encuentra la plantilla: {reporte.t_RutaPlantilla}")
        End If
    End Sub

    Private Sub ManejadorSalida(evento As Object, e As ConsoleCancelEventArgs)
        Console.WriteLine()
        Console.WriteLine("Deteniendo el reporteador ...")
        _ejecutando = False
        e.Cancel = True
    End Sub

    ' Log de errores global
    Private Sub LogErrorGlobal(origen As String, ex As Exception)
        Try
            Dim rutaLogs = "C:\ReporteadorLogs"
            If Not Directory.Exists(rutaLogs) Then Directory.CreateDirectory(rutaLogs)

            Dim archivoLog = Path.Combine(rutaLogs, $"errores_fatal_{DateTime.Now:yyyyMMdd}.log")
            Dim mensaje = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] [{origen}] {ex.Message}" &
                             Environment.NewLine & ex.StackTrace & Environment.NewLine &
                             "--------------------------------------------------" & Environment.NewLine
            File.AppendAllText(archivoLog, mensaje)
        Catch
        End Try
    End Sub

End Module