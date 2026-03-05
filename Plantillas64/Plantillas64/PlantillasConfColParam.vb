Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Imports Gsol
Imports Gsol.BaseDatos.Operaciones
Imports System.Windows.Forms
Imports System.IO

Public Class frm000PlantillasConfColParam
    Inherits FormularioBase64

#Region "Atributos"

    'Estructuras para manejar los JSON
    Private _listaColumnas As New List(Of ColumnaConfig)
    Private _listaParametros As New List(Of ParametroConfig)

    'Constante para el formato de salida Fijo
    Private Const FORMATO_SALIDA As String = "XLSX"


    'Clases para la deserialización de JSON
    Private Class ColumnaConfig
        Public Property Campo As String
        Public Property Titulo As String
        Public Property Formato As Integer 'Formato de la Columna
        Public Property Orden As Integer
    End Class

    Private Class ParametroConfig
        Public Property Nombre As String
        Public Property Tipo As String
        Public Property Etiqueta As String
        Public Property Requerido As Boolean
        Public Property ValorDefault As String
        Public Property Orden As Integer
    End Class
#End Region

#Region "Constructores"

    Public Sub New(ByVal ioperacionescatalogo_ As IOperacionesCatalogo,
            ByVal tipooperacion_ As IOperacionesCatalogo.TiposOperacionSQL)

        'Llamada necesaria para el diseñador.
        InitializeComponent()

        '················ Consumo del formulario mediante IEnlace ...........................

        ' Agregue cualquier inicialización después de la llamada a InitializeComponent().

        'ioperacionescatalogo_ = _sistema.EnsamblaModulo("Responsables").Clone

        'Dim listaLibrerias_ As New List(Of String)

        'ConstructorContexto(ClasesFormulario.ClaseA1,
        '                     tipooperacion_,
        '                     ioperacionescatalogo_,
        '                     "0.0.0.0",
        '                     "Responsables",
        '                     "Vt026Responsables",
        '                     "Responsables")


        '················ Consumo del formulario manual ...........................

        ' Agregue cualquier inicialización después de la llamada a InitializeComponent().
        _ioperacionescatalogo = New OperacionesCatalogo

        _ioperacionescatalogo = ioperacionescatalogo_

        _modalidadoperativa = tipooperacion_

        ' Agregue cualquier inicialización después de la llamada a InitializeComponent().
        _sistema = New Organismo

        'Inicializamos la UI
        InicializaCombos()

        Select Case tipooperacion_
            Case IOperacionesCatalogo.TiposOperacionSQL.Insercion
                LblAccion.Text = "Nuevo registro"
                'Inicializamos las listas vacias para nueva plantilla
                _listaColumnas = New List(Of ColumnaConfig)
                _listaParametros = New List(Of ParametroConfig)


            Case IOperacionesCatalogo.TiposOperacionSQL.Modificar

                LblAccion.Text = "Edición"
                'Carga los datos que hay en la UI
                PreparaModificacion()

            Case Else

        End Select
        'Refrescar las listas en la UI
        RefrescarListasUI()

    End Sub

#End Region

#Region "Métodos"

    Private Sub InicializaCombos()
        'Cargamos Bases de Datos disponibles
        cbBaseDeDatos.Items.Clear()
        cbBaseDeDatos.Items.Add("SysExpert")
        cbBaseDeDatos.Items.Add("Solium")
        cbBaseDeDatos.SelectedIndex = 0

        'Inicializamos combo de Formatos para Columnas
        cbFormato.SelectedIndex = 0
        'Inicializamos combo de Tipos para Parámetros
        cbTipo.SelectedIndex = 0
    End Sub

    Private Sub bloquearControles()
        'Ejemplo de bloqueo de controles
        'TbClaveMaterial.Enabled = False
    End Sub

    Public Overrides Sub PreparaModificacion()

        'Este método se llama solo para una modificación
        'Cargamos los datos del _ioperacionescatalogo en los controles

        tbNombrePlantilla.Text = _ioperacionescatalogo.CampoPorNombre("t_Nombre")
        RtbDescripcion.Text = _ioperacionescatalogo.CampoPorNombre("t_Descripcion")
        tbRutaPlantillaxlsx.Text = _ioperacionescatalogo.CampoPorNombre("t_RutaPlantilla")
        cbBaseDeDatos.Text = _ioperacionescatalogo.CampoPorNombre("t_NombreBaseDeDatos")
        rtbConsultaSQL.Text = _ioperacionescatalogo.CampoPorNombre("t_Consulta")

        'Validar que el Formato de Salida sea XLSX
        Dim formatoSalidaBD As String = _ioperacionescatalogo.CampoPorNombre("t_FormatoSalida")

        If formatoSalidaBD <> FORMATO_SALIDA Then
            MessageBox.Show($"El formato de salida se ajustará a {FORMATO_SALIDA} (estándar del sistema)",
                            "Información", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If

        'Cargar los JSON
        Dim columnasJson As String = _ioperacionescatalogo.CampoPorNombre("t_ColumnasConfig")

        If Not String.IsNullOrEmpty(columnasJson) Then
            Try
                _listaColumnas = JsonConvert.DeserializeObject(Of List(Of ColumnaConfig))(columnasJson)
            Catch ex As Exception
                MessageBox.Show($"Error al cargar configuración de columnas: {ex.Message}",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                _listaColumnas = New List(Of ColumnaConfig)
            End Try
        Else
            _listaColumnas = New List(Of ColumnaConfig)
        End If

        Dim parametrosJson As String = _ioperacionescatalogo.CampoPorNombre("t_ParametrosConfig")
        If Not String.IsNullOrEmpty(parametrosJson) Then
            Try
                _listaParametros = JsonConvert.DeserializeObject(Of List(Of ParametroConfig))(parametrosJson)
            Catch ex As Exception
                MessageBox.Show($"Error al cargar configuración de parámetros: {ex.Message}",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                _listaParametros = New List(Of ParametroConfig)
            End Try
        Else
            _listaParametros = New List(Of ParametroConfig)
        End If
    End Sub

    Private Sub RefrescarListasUI()
        'Actualiza los listBox con el contenido de las listas
        ColumnasCargadas.Items.Clear()
        For Each col In _listaColumnas.OrderBy(Function(c) c.Orden)
            ColumnasCargadas.Items.Add($"{col.Orden}: {col.Titulo} ({col.Campo}) -  Formato: {col.Formato}")
        Next

        ParametrosCargados.Items.Clear()
        For Each param In _listaParametros.OrderBy(Function(p) p.Orden)
            Dim req As String = If(param.Requerido, "SÍ", "NO")
            ParametrosCargados.Items.Add($"{param.Orden}{req}: {param.Etiqueta} 
                ({param.Nombre}: {param.Tipo}) = {param.ValorDefault}")
        Next
    End Sub
    Public Overrides Sub RealizarInsercion()

        'Este método se llama cuando el usuario hace clic en Guardar para una nueva plantilla
        'Debemos llenar el _ioperacionescatalogo con los datos de los controles para que se inserten en la BD
        LlenarCatalogoConUI()
    End Sub

    Private Sub LlenarCatalogoConUI()
        'Asignación directa de los campos del catálogo con los controles de la UI
        _ioperacionescatalogo.CampoPorNombre("t_Nombre") = tbNombrePlantilla.Text
        _ioperacionescatalogo.CampoPorNombre("t_Descripcion") = RtbDescripcion.Text
        _ioperacionescatalogo.CampoPorNombre("t_RutaPlantilla") = tbRutaPlantillaxlsx.Text
        _ioperacionescatalogo.CampoPorNombre("t_NombreBaseDeDatos") = cbBaseDeDatos.Text
        _ioperacionescatalogo.CampoPorNombre("t_Consulta") = rtbConsultaSQL.Text
        'El formato de salida se fija a XLSX
        _ioperacionescatalogo.CampoPorNombre("t_FormatoSalida") = FORMATO_SALIDA
        'Serializar las listas a JSON para almacenarlas en el catálogo
        If _listaColumnas IsNot Nothing AndAlso _listaColumnas.Count > 0 Then
            _ioperacionescatalogo.CampoPorNombre("t_ColumnasConfig") = JsonConvert.SerializeObject(_listaColumnas)
        Else
            _ioperacionescatalogo.CampoPorNombre("t_ColumnasConfig") = String.Empty
        End If

        If _listaParametros IsNot Nothing AndAlso _listaParametros.Count > 0 Then
            _ioperacionescatalogo.CampoPorNombre("t_ParametrosConfig") = JsonConvert.SerializeObject(_listaParametros)
        Else
            _ioperacionescatalogo.CampoPorNombre("t_ParametrosConfig") = String.Empty
        End If

        If _modalidadoperativa = IOperacionesCatalogo.TiposOperacionSQL.Insercion Then
            'Asignar valores adicionales para nueva plantilla si es necesario
            '_ioperacionescatalogo.CampoPorNombre("t_UsuarioRegistro") = _sistema.UsuarioActual.CveUsuario
            _ioperacionescatalogo.CampoPorNombre("f_FechaRegistro") = DateTime.Now
            _ioperacionescatalogo.CampoPorNombre("i_Cve_Estatus") = "1" 'Activo
            _ioperacionescatalogo.CampoPorNombre("i_Cve_Estado") = "1" 'Activo
        Else
            'f_Registro y usuario de registro no se modifican en edición
        End If
    End Sub

    Private Sub BSeleccionarPlantilla_Click(sender As Object, e As EventArgs) Handles bSeleccionarPlantilla.Click
        'Permitir al usuario seleccionar un archivo XLSX como plantilla
        Using ofd As New OpenFileDialog()
            ofd.Filter = "Archivos Excel (*.xlsx)|*.xlsx"
            ofd.Title = "Seleccionar plantilla de Excel"
            If ofd.ShowDialog() = DialogResult.OK Then
                tbRutaPlantillaxlsx.Text = ofd.FileName
            End If
        End Using
    End Sub

    Public Overrides Sub RealizarModificacion()
        LlenarCatalogoConUI()

    End Sub


    '------------------------------ Gestión de Columnas ----------------------------------
    Private Sub BAgregarColumna_Click(sender As Object, e As EventArgs) Handles bAgregarColumna.Click
        'Validar campos de columna
        If String.IsNullOrEmpty(tbCampo.Text) OrElse String.IsNullOrEmpty(tbEtiqueta.Text) Then
            MessageBox.Show("Debe ingresar el campo y Etiqueta de la columna.",
                            "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        'Validar que el Orden sea Númerico y positivo
        Dim orden As Integer
        If Not Integer.TryParse(tbOrden.Text, orden) OrElse orden <= 0 Then
            MessageBox.Show("El orden de la columna debe ser un número entero positivo.",
                            "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        'Agregar nueva columna a la lista
        Dim nuevaColumna As New ColumnaConfig With {
            .Campo = tbCampo.Text,
            .Titulo = tbEtiqueta.Text,
            .Formato = cbFormato.SelectedIndex,
            .Orden = _listaColumnas.Count + 1
        }

        _listaColumnas.Add(nuevaColumna)
        'Refrescar UI y limpiar campos
        RefrescarListasUI()
        tLimpiarCamposColumna()
    End Sub

    Private Sub BModificarColumna_Click(sender As Object, e As EventArgs) Handles bModificarColumna.Click
        'Validar que se haya seleccionado una columna
        If ColumnasCargadas.SelectedIndex < 0 Then
            MessageBox.Show("Debe seleccionar una columna para modificar.",
                            "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        'Validar campos de columna
        If String.IsNullOrEmpty(tbCampo.Text) OrElse String.IsNullOrEmpty(tbEtiqueta.Text) Then
            MessageBox.Show("Debe ingresar el campo y Etiqueta de la columna.",
                            "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        'Validar que el Orden sea Númerico y positivo
        Dim orden As Integer
        If Not Integer.TryParse(tbOrden.Text, orden) OrElse orden <= 0 Then
            MessageBox.Show("El orden de la columna debe ser un número entero positivo.",
                            "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        'Modificar la columna seleccionada en la lista
        Dim indice As Integer = ColumnasCargadas.SelectedIndex
        Dim columnaSeleccionada As ColumnaConfig = _listaColumnas.OrderBy(Function(c) c.Orden).ElementAt(indice)
        columnaSeleccionada.Campo = tbCampo.Text
        columnaSeleccionada.Titulo = tbEtiqueta.Text
        columnaSeleccionada.Formato = cbFormato.Text
        columnaSeleccionada.Orden = orden
        'Refrescar UI y limpiar campos
        RefrescarListasUI()
        tLimpiarCamposColumna()
    End Sub

    Private Sub ColumnasCargadas_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ColumnasCargadas.SelectedIndexChanged
        'Cuando se selecciona una columna en el listBox, cargar sus datos en los campos para edición
        If ColumnasCargadas.SelectedIndex >= 0 Then
            Dim indice As Integer = ColumnasCargadas.SelectedIndex
            Dim columnaSeleccionada As ColumnaConfig = _listaColumnas.OrderBy(Function(c) c.Orden).ElementAt(indice)
            tbCampo.Text = columnaSeleccionada.Campo
            tbEtiqueta.Text = columnaSeleccionada.Titulo
            cbFormato.SelectedIndex = columnaSeleccionada.Formato
            tbOrden.Text = columnaSeleccionada.Orden.ToString()
        End If
    End Sub

    Private Sub tLimpiarCamposColumna()
        tbCampo.Clear()
        tbEtiqueta.Clear()
        cbFormato.SelectedIndex = 0
        tbOrden.Clear()
        ColumnasCargadas.ClearSelected()
    End Sub

    '------------------------------- Gestión de Parámetros ----------------------------------
    Private Sub BAgregarParametro_Click(sender As Object, e As EventArgs) Handles bAgregarParametro.Click
        'Validar campos de parámetro
        If String.IsNullOrEmpty(tbNombre.Text) OrElse String.IsNullOrEmpty(tbEtiquetaParametro.Text) OrElse cbTipo.SelectedItem Is Nothing Then
            MessageBox.Show("Debe ingresar el nombre y etiqueta del parámetro, Ademas seleccione el Tipo de parámetro.",
                            "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If
        'Validar que el Orden sea Númerico y positivo
        Dim orden As Integer
        If Not Integer.TryParse(tbOrdenParametro.Text, orden) OrElse orden <= 0 Then
            MessageBox.Show("El orden del parámetro debe ser un número entero positivo.",
                            "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        'Agregar nuevo parámetro a la lista
        Dim nuevoParametro As New ParametroConfig With {
            .Nombre = tbNombre.Text,
            .Tipo = cbTipo.SelectedItem.ToString(),
            .Etiqueta = tbEtiquetaParametro.Text,
            .Requerido = checkbRequerido.Checked,
            .ValorDefault = TbValorDefault.Text,
            .Orden = _listaParametros.Count + 1
        }
        _listaParametros.Add(nuevoParametro)
        'Refrescar UI y limpiar campos
        RefrescarListasUI()
        tLimpiarCamposParametro()
    End Sub

    Private Sub BModificarParametro_Click(sender As Object, e As EventArgs) Handles bModificarParametro.Click
        'Validar que se haya seleccionado un parámetro
        If ParametrosCargados.SelectedIndex < 0 Then
            MessageBox.Show("Debe seleccionar un parámetro para modificar.",
                            "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        'Validar campos de parámetro
        If String.IsNullOrEmpty(tbNombre.Text) OrElse String.IsNullOrEmpty(tbEtiquetaParametro.Text) OrElse cbTipo.SelectedItem Is Nothing Then
            MessageBox.Show("Debe ingresar el nombre y etiqueta del parámetro, Ademas seleccione el Tipo de parámetro.",
                            "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        'Validar que el Orden sea Númerico y positivo
        Dim orden As Integer
        If Not Integer.TryParse(tbOrdenParametro.Text, orden) OrElse orden <= 0 Then
            MessageBox.Show("El orden del parámetro debe ser un número entero positivo.",
                            "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        'Modificar el parámetro seleccionado en la lista
        Dim indice As Integer = ParametrosCargados.SelectedIndex
        Dim parametroSeleccionado As ParametroConfig = _listaParametros.OrderBy(Function(p) p.Orden).ElementAt(indice)
        parametroSeleccionado.Nombre = tbNombre.Text
        parametroSeleccionado.Tipo = cbTipo.SelectedItem.ToString()
        parametroSeleccionado.Etiqueta = tbEtiquetaParametro.Text
        parametroSeleccionado.Requerido = checkbRequerido.Checked
        parametroSeleccionado.ValorDefault = TbValorDefault.Text
        parametroSeleccionado.Orden = orden
        'Refrescar UI y limpiar campos
        RefrescarListasUI()
        tLimpiarCamposParametro()
    End Sub

    Private Sub ParametrosCargados_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ParametrosCargados.SelectedIndexChanged
        'Cuando se selecciona un parámetro en el listBox, cargar sus datos en los campos para edición
        If ParametrosCargados.SelectedIndex >= 0 Then
            Dim indice As Integer = ParametrosCargados.SelectedIndex
            Dim parametroSeleccionado As ParametroConfig = _listaParametros.OrderBy(Function(p) p.Orden).ElementAt(indice)
            tbNombre.Text = parametroSeleccionado.Nombre
            cbTipo.SelectedItem = parametroSeleccionado.Tipo
            tbEtiquetaParametro.Text = parametroSeleccionado.Etiqueta
            checkbRequerido.Checked = parametroSeleccionado.Requerido
            TbValorDefault.Text = parametroSeleccionado.ValorDefault
            tbOrdenParametro.Text = parametroSeleccionado.Orden.ToString()
        End If
    End Sub

    Private Sub tLimpiarCamposParametro()
        tbNombre.Clear()
        cbTipo.SelectedIndex = 0
        tbEtiquetaParametro.Clear()
        checkbRequerido.Checked = False
        TbValorDefault.Clear()
        tbOrdenParametro.Clear()
        ParametrosCargados.ClearSelected()
    End Sub

    '-------------------------Manejo de validaciones comunes----------------------------------
    Private Sub Frm000PlantillasConfColParam_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        'Validar datos si se esta cerrando y se quieren guardar los cambios
        If Me.DialogResult = DialogResult.OK Then

            '1 Validar campos obligatorios basicos
            If String.IsNullOrEmpty(tbNombrePlantilla.Text) Then
                MessageBox.Show("El nombre de la plantilla es obligatorio.",
                                "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                e.Cancel = True
                Return
            End If

            If String.IsNullOrEmpty(rtbConsultaSQL.Text) Then
                MessageBox.Show("La consulta SQL es obligatoria.",
                                "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                e.Cancel = True
                Return
            End If

            '2 Validar Ruta de plantilla
            If Not ValidarRutaPlantilla() Then
                e.Cancel = True
                Return
            End If

            '3 Validar que al menos haya una columna configurada
            If _listaColumnas Is Nothing OrElse _listaColumnas.Count = 0 Then
                Dim resultado = MessageBox.Show("No hay columnas configuradas. ¿Desea continuar sin configurar columnas?",
                                                "Advertencia", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                If resultado = DialogResult.No Then
                    e.Cancel = True
                    Return
                End If
            End If

            '4 Validar integridad de los JSON (duplicados)
            ValidarIntegridadJSON()

            '5 Validar consulta SQL contra los parámetros configurados
            If Not ValidarConsultaSQL() Then
                e.Cancel = True
                Return
            End If

        End If
    End Sub

    Private Sub ValidarIntegridadJSON()
        'Validar que no haya nobres duplicados en las columnas
        Dim camposDuplicados = _listaColumnas.GroupBy(Function(c) c.Campo.ToUpper()).
            Where(Function(g) g.Count() > 1).Select(Function(g) g.Key).ToList()

        If camposDuplicados.Any() Then
            MessageBox.Show($"Los siguiuentes campos están duplicados: {String.Join(", ", camposDuplicados)}",
                            "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If

        'Validar que no haya nombres de parámetros duplicados
        Dim parametrosDuplicados = _listaParametros.GroupBy(Function(p) p.Nombre.ToUpper()).
            Where(Function(g) g.Count() > 1).Select(Function(g) g.Key).ToList()

        If parametrosDuplicados.Any() Then
            MessageBox.Show($"Los siguientes nombres de parámetros están duplicados: {String.Join(", ", parametrosDuplicados)}",
                            "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
    End Sub

    Private Function ValidarConsultaSQL() As Boolean
        Dim consulta As String = rtbConsultaSQL.Text.Trim()
        'Validar que la consulta no esté vacía
        If String.IsNullOrEmpty(consulta) Then
            MessageBox.Show("La consulta SQL no puede estar vacía.",
                            "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return False
        End If

        'Validar que la consulta contenga al menos un SELECT
        If Not consulta.ToUpper().Contains("SELECT") Then
            MessageBox.Show("La consulta SQL debe contener una cláusula SELECT.",
                            "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return False
        End If

        'Validar que los parametros en la consulta coincidan con los parámetros definidos en la configuración
        Dim parametrosEnConsulta As New List(Of String)
        Dim coincidencia = System.Text.RegularExpressions.Regex.Matches(consulta, "@(\w+)")

        For Each match As System.Text.RegularExpressions.Match In coincidencia
            parametrosEnConsulta.Add(match.Groups(1).Value.ToUpper())
        Next

        'Comparar con los parámetros definidos
        Dim parametrosDefinidos = _listaParametros.Select(Function(p) "@" & p.Nombre).ToList()
        Dim parametrosFaltantes = parametrosEnConsulta.Except(parametrosDefinidos).ToList()
        Dim parametrosExtra = parametrosDefinidos.Except(parametrosEnConsulta).ToList()

        If parametrosFaltantes.Any() Then
            Dim resultado = MessageBox.Show($"La consulta tiene parámetros no configurados: 
                {String.Join(", ", parametrosFaltantes)}.¿Desea continuar?",
                                            "Advertencia", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

            If resultado = DialogResult.No Then
                Return False
            End If
        End If

        If parametrosExtra.Any() Then
            Dim resultado = MessageBox.Show($"Hay parámetros configurados que no se usan en la consulta: 
                {String.Join(", ", parametrosExtra)}.¿Desea continuar?",
                                            "Advertencia", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If resultado = DialogResult.No Then
                Return False
            End If
        End If

        Return True
    End Function

    'validacion de la ruta de la plantilla
    Private Function ValidarRutaPlantilla() As Boolean
        Dim ruta As String = tbRutaPlantillaxlsx.Text.Trim()
        If String.IsNullOrEmpty(ruta) Then
            MessageBox.Show("La ruta de la plantilla no puede estar vacía.",
                            "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return False
        End If

        'Validar Extencion del archivo
        If Path.GetExtension(ruta).ToUpper() <> ".XLSX" Then
            MessageBox.Show("El archivo seleccionado debe ser un archivo Excel (.xlsx).",
                            "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return False
        End If

        'Validar que el archivo exista
        If Not System.IO.File.Exists(ruta) Then
            Dim resultado = MessageBox.Show("El archivo seleccionado no existe. ¿Desea continuar con esta ruta?",
                                            "Advertencia", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            Return resultado = DialogResult.Yes
        End If

        Return True
    End Function

#End Region


End Class
