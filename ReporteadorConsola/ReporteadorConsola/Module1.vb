Imports System
Imports System.IO
Imports System.Threading
Imports ReporteadorConsola.Servicios
Imports ReporteadorConsola.Modelos
Imports Newtonsoft.Json
Imports System.Configuration

Imports Gsol
Imports Gsol.BaseDatos.Operaciones
Imports System.IdentityModel.Metadata
Imports Wma.Exceptions

Module Module1

    'Configuracion a la BD
    Private ReadOnly _intervaloEjecucion As Integer = 60000
    Private _ejecutando As Boolean = True

    'Private ReadOnly _cadenaConexion As String = ConfigurationManager.ConnectionStrings("Solium").ConnectionString

    ' Objetos core del framework de la empresa
    Private _sistema As Organismo
    Private _sesion As ISesion
    Private _espacioTrabajo As IEspacioTrabajo
    Private _ioperacionescatalogo As OperacionesCatalogo
    Private _estatusSesion As Boolean

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
            'Console.WriteLine($"Configuración cargada desde: {AppDomain.CurrentDomain.SetupInformation.ConfigurationFile}")

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

            'Iniciar sesion en el framework de Krom
            Console.WriteLine("Conectando con el framework central ...")
            If Not IniciarSesionKrom() Then
                Console.WriteLine("[ERROR FATAL] No se pudo iniciar sesion")
                Console.ReadKey()
                Return
            End If
            Console.WriteLine("Sesion de iniciada correctamente")

            'Inicializar los servicios
            _servicioDB = New ServicioBaseDeDatos(_ioperacionescatalogo, _sistema, _config)
            _servicioExcel = New ServicioGeneracionExcel()

            'configurar manejador
            AddHandler Console.CancelKeyPress, AddressOf ManejadorSalida

            'mostrar configuracion
            MostrarConfiguracion()

            'bucle principal del motor
            While _ejecutando
                EjecutarCicloProcesamiento()

                Dim tiempoEspera As Integer = 0
                'dormimos el intervalo hasta la salida
                While tiempoEspera < _intervaloEjecucion AndAlso _ejecutando
                    Thread.Sleep(1000)
                    tiempoEspera += 1000
                End While
            End While

        Catch ex As Exception

            Console.ForegroundColor = ConsoleColor.Red
            Console.WriteLine(vbCrLf & "================ ERROR DETALLADO ================")
            Console.WriteLine(ex.ToString())
            Console.WriteLine("=================================================" & vbCrLf)
            Console.ResetColor()

            Console.WriteLine($"[ERROR FATAL EXCEPCION NO CONTROLADA] {ex.Message}")
            Console.WriteLine(ex.StackTrace)
            LogErrorGlobal("Main", ex)
        End Try

        Console.WriteLine("Reporteador Detenido")
    End Sub

    'Metodo para iniciar el framework
    Private Function IniciarSesionKrom() As Boolean
        Try
            _sistema = New Organismo
            _sesion = New Sesion()

            'Preguntar si estas claves se quedan asi o por que valores debo cambiarlas
            _sesion.IdentificadorUsuario = "desarrollo@kromaduanal.com"
            _sesion.ContraseniaUsuario = "DesarrolloKROM19"
            _sesion.GrupoEmpresarial = 1
            _sesion.DivisionEmpresarial = 1
            _sesion.Aplicacion = 4
            _sesion.Idioma = ISesion.Idiomas.Espaniol
            _estatusSesion = _sesion.StatusArgumentos

            If _estatusSesion Then
                _espacioTrabajo = _sesion.EspacioTrabajo
                _ioperacionescatalogo = New OperacionesCatalogo()
                _ioperacionescatalogo.EspacioTrabajo = _espacioTrabajo
                _ioperacionescatalogo.ModalidadConsulta = IOperacionesCatalogo.ModalidadesConsulta.ConexionLibre
                Return True
            End If

            Return False

        Catch ex As Exception
            LogErrorGlobal("IniciarSesionKrom", ex)
            Return False
        End Try
    End Function

    'mostramos la configuracion actual
    Private Sub MostrarConfiguracion()
        Console.WriteLine("------------------------------------------")
        Console.WriteLine("Configuración actual:")
        Console.WriteLine($"  - Cuenta Motor: {_sesion.IdentificadorUsuario}")
        Console.WriteLine($"  - Directorio salida: {_config.DirectorioSalida}")
        Console.WriteLine($"  - Directorio plantillas: {_config.DirectorioPlantillas}")
        Console.WriteLine($"  - Intervalo: {_config.IntervaloEjecucion / 1000} seg")
        Console.WriteLine("------------------------------------------")
    End Sub

    'Ejecutamos un ciclo completo de procesamiento
    Private Sub EjecutarCicloProcesamiento()
        Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] Iniciando ciclo de procesamiento")

        Dim estatusReporte As TagWatcher = _servicioDB.ObtenerReportesPendientes()

        If estatusReporte.Status = TagWatcher.TypeStatus.Ok Then
            Dim reportesPendientes = CType(estatusReporte.ObjectReturned, List(Of ReportePendiente))

            If reportesPendientes Is Nothing OrElse reportesPendientes.Count = 0 Then
                Console.WriteLine("No hay reportes pendientes por procesar")
                Return
            End If

            For Each reporte In reportesPendientes
                ProcesarReporte(reporte)
            Next
        Else
            Console.WriteLine($"[ADVERTENCIA] Error obteniendo reportes: {estatusReporte.ErrorDescription}")

        End If

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
            Dim estatusConsulta As TagWatcher = _servicioDB.EjecutarConsultaReporte(reporte)

            If estatusConsulta.Status = TagWatcher.TypeStatus.Ok Then
                Dim datos = CType(estatusConsulta.ObjectReturned, DataTable)
                registrosProcesados = If(datos Is Nothing, 0, datos.Rows.Count)
                Console.WriteLine($" Registros obtenidos: {registrosProcesados}")

                'generar archivo
                Select Case reporte.t_FormatoSalida.ToUpper()
                    Case "XLSX", "XLS", ""
                        Dim estatusExcel As TagWatcher = _servicioExcel.GenerarExcelDesdePlantilla(
                            reporte.t_RutaPlantilla,
                            datos,
                            reporte.NombreReporte,
                            _config.DirectorioSalida,
                            reporte.i_FilaInicio,
                            reporte.i_ColumnaInicio)

                        If estatusExcel.Status = TagWatcher.TypeStatus.Ok Then
                            rutaDocumento = estatusExcel.ObjectReturned.ToString()
                            Console.WriteLine($" Archivo generado: {rutaDocumento}")
                            exito = True
                        Else
                            errorMsg = estatusExcel.ErrorDescription
                            Console.WriteLine($" ERROR EXCEL: {errorMsg}")
                            exito = False
                        End If
                End Select
            End If

            'ejecutar consulta
            'Console.WriteLine(" Ejecutando Consulta...")---------------------------
            'Dim datos = _servicioDB.EjecutarConsultaReporte(reporte)---------------

        Catch ex As Exception

            errorMsg = ex.Message
            Console.WriteLine($" ERROR {errorMsg}")
            LogErrorGlobal($"Procesar Reporte_{reporte.i_Cve_Generacion}", ex)
        End Try


        'Actualizar la bitacora

        Dim estatusBitacora As TagWatcher = _servicioDB.ActualizarEstadoGeneracion(reporte.i_Cve_Generacion, exito, rutaDocumento, registrosProcesados, errorMsg)

        If estatusBitacora.Status <> TagWatcher.TypeStatus.Ok Then
            Console.WriteLine($" Error Actualizando a la Bitacora: {estatusBitacora.ErrorDescription}")
        Else
            Console.WriteLine($" Estado Actualizado: {If(exito, "COMPLETADO", "FALLIDO")}")
        End If

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
            Dim rutaLogs = _config.DirectorioLogs
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