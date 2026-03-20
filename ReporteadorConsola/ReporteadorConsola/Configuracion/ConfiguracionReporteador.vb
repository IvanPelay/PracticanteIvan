Imports System.Configuration

Public Class ConfiguracionReporteador
    Private Shared _instancia As ConfiguracionReporteador
    Private Shared _lock As New Object()

    Private _conexion As String
    Private _intervaloEjecucion As Integer
    Private _directorioSalida As String
    Private _directorioPlantillas As String
    Private _directorioLogs As String
    Private _logNivel As String
    Private _usuarioSistema As String
    Private _commandTimeout As Integer
    Private _maxReintentos As Integer
    Private _intervaloReintentos As Integer
    Private _mantenerLogsDias As Integer

    Private Sub New()
        CargarConfiguracion()
    End Sub

    Public Shared ReadOnly Property Instance As ConfiguracionReporteador
        Get
            If _instancia Is Nothing Then
                SyncLock _lock
                    If _instancia Is Nothing Then
                        _instancia = New ConfiguracionReporteador()
                    End If
                End SyncLock
            End If
            Return _instancia
        End Get
    End Property

    Private Sub CargarConfiguracion()
        ' Connection Strings
        _conexion = ConfigurationManager.ConnectionStrings("ReporteadorDB")?.ConnectionString
        If String.IsNullOrEmpty(_conexion) Then
            Throw New Exception("No se encontró la cadena de conexión 'ReporteadorDB' en app.config")
        End If

        ' AppSettings con valores por defecto
        _intervaloEjecucion = GetAppSettingInteger("IntervaloEjecucion", 60000)
        _directorioSalida = GetAppSettingString("DirectorioSalida", "C:\ReporteadorGenerados")
        _directorioPlantillas = GetAppSettingString("DirectorioPlantillas", "C:\ReporteadorPlantillas")
        _directorioLogs = GetAppSettingString("DirectorioLogs", "C:\ReporteadorLogs")
        _logNivel = GetAppSettingString("LogNivel", "INFO")
        _usuarioSistema = GetAppSettingString("UsuarioSistema", "MOTOR_REPORTEADOR")
        _commandTimeout = GetAppSettingInteger("CommandTimeout", 300)
        _maxReintentos = GetAppSettingInteger("MaxReintentos", 3)
        _intervaloReintentos = GetAppSettingInteger("IntervaloReintentos", 5000)
        _mantenerLogsDias = GetAppSettingInteger("MantenerLogsDias", 30)

        ' Crear directorios si no existen
        CrearDirectorios()
    End Sub

    Private Function GetAppSettingString(clave As String, defaultValue As String) As String
        Dim valor = ConfigurationManager.AppSettings(clave)
        Return If(String.IsNullOrEmpty(valor), defaultValue, valor)
    End Function

    Private Function GetAppSettingInteger(clave As String, defaultValue As Integer) As Integer
        Dim valor = ConfigurationManager.AppSettings(clave)
        If Integer.TryParse(valor, defaultValue) Then
            Return defaultValue
        End If
        Return defaultValue
    End Function

    Private Sub CrearDirectorios()
        Try
            If Not IO.Directory.Exists(_directorioSalida) Then
                IO.Directory.CreateDirectory(_directorioSalida)
                Console.WriteLine($"Directorio de salida creado: {_directorioSalida}")
            End If

            If Not IO.Directory.Exists(_directorioLogs) Then
                IO.Directory.CreateDirectory(_directorioLogs)
                Console.WriteLine($"Directorio de logs creado: {_directorioLogs}")
            End If

            If Not IO.Directory.Exists(_directorioPlantillas) Then
                IO.Directory.CreateDirectory(_directorioPlantillas)
                Console.WriteLine($"Directorio de plantillas creado: {_directorioPlantillas}")
            End If
        Catch ex As Exception
            Console.WriteLine($"[ERROR] No se pudieron crear los directorios: {ex.Message}")
        End Try
    End Sub

    ' Propiedades públicas
    Public ReadOnly Property Conexion As String
        Get
            Return _conexion
        End Get
    End Property

    Public ReadOnly Property IntervaloEjecucion As Integer
        Get
            Return _intervaloEjecucion
        End Get
    End Property

    Public ReadOnly Property DirectorioSalida As String
        Get
            Return _directorioSalida
        End Get
    End Property

    Public ReadOnly Property DirectorioPlantillas As String
        Get
            Return _directorioPlantillas
        End Get
    End Property

    Public ReadOnly Property DirectorioLogs As String
        Get
            Return _directorioLogs
        End Get
    End Property

    Public ReadOnly Property LogNivel As String
        Get
            Return _logNivel
        End Get
    End Property

    Public ReadOnly Property UsuarioSistema As String
        Get
            Return _usuarioSistema
        End Get
    End Property

    Public ReadOnly Property CommandTimeout As Integer
        Get
            Return _commandTimeout
        End Get
    End Property

    Public ReadOnly Property MaxReintentos As Integer
        Get
            Return _maxReintentos
        End Get
    End Property

    Public ReadOnly Property IntervaloReintentos As Integer
        Get
            Return _intervaloReintentos
        End Get
    End Property

    Public ReadOnly Property MantenerLogsDias As Integer
        Get
            Return _mantenerLogsDias
        End Get
    End Property

    ' Método de ayuda para validar configuración
    Public Function ValidarConfiguracion() As List(Of String)
        Dim errores As New List(Of String)()

        If String.IsNullOrEmpty(_conexion) Then
            errores.Add("La cadena de conexión no está configurada")
        End If

        If Not IO.Directory.Exists(_directorioPlantillas) Then
            errores.Add($"El directorio de plantillas no existe: {_directorioPlantillas}")
        End If

        If Not IO.Directory.Exists(_directorioSalida) Then
            errores.Add($"No se puede acceder al directorio de salida: {_directorioSalida}")
        End If

        Return errores
    End Function
End Class

