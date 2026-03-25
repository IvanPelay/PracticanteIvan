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

    'Expresiones dinamicas validas para tipo Date
    'El usuario puede escribirlas como ValorDefault en parametros de fecha
    Private Shared ReadOnly _expresionesValidas As String() = {
    "INICIO_PERIODO", "FIN_PERIODO",
    "INICIO_SEMANA", "FIN_SEMANA",
    "INICIO_MES", "FIN_MES",
    "INICIO_QUINCENA", "FIN_QUINCENA",
    "HOY"
    }

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

    'informacion de la consulta seleccionada
    Private _cveConsultaSeleccionada As Integer = 0
    Private _vistaConsultaSeleccionada As String = ""
    Private _spConsultseleccionada As String = ""

    Private Class ConsultaInfo
        Public Property Id As Integer
        Public Property Nombre As String
        Public Property Vista As String
        Public Property SP As String
    End Class

    Private _listaConsultas As New List(Of ConsultaInfo)

#End Region

#Region "Constructores"

    Public Sub New(ByVal ioperacionescatalogo_ As IOperacionesCatalogo,
                ByVal tipooperacion_ As IOperacionesCatalogo.TiposOperacionSQL)

            'Llamada necesaria para el diseñador.
            InitializeComponent()

            ' Agregue cualquier inicialización después de la llamada a InitializeComponent().
            _ioperacionescatalogo = New OperacionesCatalogo

            _ioperacionescatalogo = ioperacionescatalogo_

            _modalidadoperativa = tipooperacion_

            ' Agregue cualquier inicialización después de la llamada a InitializeComponent().
            _sistema = New Organismo

        'Inicializamos la UI
        InicializaCombos()
        CargarConsultasDisponibles()

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

    'cargar las consultas disponibles
    Private Sub CargarConsultasDisponibles()
        Try
            _listaConsultas.Clear()

            Dim op As New OperacionesCatalogo()
            op.IdentificadorCatalogo = "i_Cve_Consulta"
            op.OperadorCatalogoConsulta = "Vt016ConsultasDisponibles"

            If op.CantidadVisibleRegistros > 0 Then
                For i As Integer = 0 To op.CantidadVisibleRegistros - 1
                    op.IndicePaginacion = i
                    _listaConsultas.Add(New ConsultaInfo With {
                        .Id = CInt(op.CampoPorNombre("i_Cve_Consulta")),
                        .Nombre = op.CampoPorNombre("t_Nombre").ToString(),
                        .Vista = op.CampoPorNombre("t_NombreVista").ToString(),
                        .SP = If(op.CampoPorNombre("t_NombreSP") Is Nothing OrElse
                                     op.CampoPorNombre("t_NombreSP").ToString() = "",
                                     "", op.CampoPorNombre("t_NombreSP").ToString())
                    })
                Next

                cbConsulta.DisplayMember = "Nombre"
                cbConsulta.ValueMember = "Id"
                cbConsulta.DataSource = _listaConsultas.ToList()

                If cbConsulta.Items.Count > 0 Then
                    cbConsulta.SelectedIndex = 0
                End If
            Else
                cbConsulta.DataSource = Nothing
                cbConsulta.Items.Clear()
                cbConsulta.Items.Add("Sin consultas registradas")
                cbConsulta.SelectedIndex = 0
                LimpiarInfoConsulta()
            End If
        Catch ex As Exception
            MessageBox.Show($"Error al cargar consultas disponibles: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub CbConsulta_SelectedIndexChanged(sender As Object, e As EventArgs) _
        Handles cbConsulta.SelectedIndexChanged

        If cbConsulta.SelectedItem IsNot Nothing AndAlso TypeOf cbConsulta.SelectedItem Is ConsultaInfo Then

            Dim info = CType(cbConsulta.SelectedItem, ConsultaInfo)
            _cveConsultaSeleccionada = info.Id
            _vistaConsultaSeleccionada = info.Vista
            _spConsultseleccionada = info.SP

            'Mostrar info de solo lectura en las etiquetas
            lblConsultaVistaVal.Text = info.Vista
            lblConsultaSPVal.Text = If(String.IsNullOrEmpty(info.SP), "(ninguno - se usará la vista directo)", info.SP)

        Else
            LimpiarInfoConsulta()
        End If
    End Sub

    Private Sub LimpiarInfoConsulta()
        _cveConsultaSeleccionada = 0
        _vistaConsultaSeleccionada = ""
        _spConsultseleccionada = ""
        lblConsultaVistaVal.Text = ""
        lblConsultaSPVal.Text = ""
    End Sub

    'Selecciona en el combo la consulta que corresponde al id guardado en la BD
    Private Sub SeleccionarConsultaPorId(id As Integer)
        For i As Integer = 0 To cbConsulta.Items.Count - 1
            If TypeOf cbConsulta.Items(1) Is ConsultaInfo Then
                Dim info = CType(cbConsulta.Items(1), ConsultaInfo)
                If info.Id = id Then
                    cbConsulta.SelectedIndex = 1
                    Return
                End If
            End If
        Next
    End Sub

    Private Sub InicializaCombos()
        'Inicializamos combo de Formatos para Columnas
        cbFormato.SelectedIndex = 0
        'Inicializamos combo de Tipos para Parámetros
        cbTipo.SelectedIndex = 0
    End Sub

    Public Overrides Sub PreparaModificacion()

            'Este método se llama solo para una modificación
            'Cargamos los datos del _ioperacionescatalogo en los controles

            tbNombrePlantilla.Text = _ioperacionescatalogo.CampoPorNombre("t_Nombre")
            RtbDescripcion.Text = _ioperacionescatalogo.CampoPorNombre("t_Descripcion")
            tbRutaPlantillaxlsx.Text = _ioperacionescatalogo.CampoPorNombre("t_RutaPlantilla")
        tbCliente.Text = _ioperacionescatalogo.CampoPorNombre("t_NombreCliente")
        chkEstatus.Checked = (_ioperacionescatalogo.CampoPorNombre("i_Cve_Estatus") = 1)

        'seleccionar la consulta guardada
        Dim cveConsulta As Integer = 0
        If Integer.TryParse(_ioperacionescatalogo.CampoPorNombre("i_Cve_Consulta"), cveConsulta) Then
            SeleccionarConsultaPorId(cveConsulta)
        End If

        'Validar que el Formato de Salida sea XLSX
        Dim formatoSalidaBD As String = _ioperacionescatalogo.CampoPorNombre("t_FormatoSalida")

            If formatoSalidaBD <> FORMATO_SALIDA Then
                MessageBox.Show($"El formato de salida se ajustará a {FORMATO_SALIDA} (estándar del sistema)",
                                "Información", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

        'Cargar los JSON
        Dim columnasJson = _ioperacionescatalogo.CampoPorNombre("t_ColumnasConfig")

        If Not String.IsNullOrEmpty(columnasJson) Then
                Try
                    _listaColumnas = JsonConvert.DeserializeObject(Of List(Of ColumnaConfig))(columnasJson)

                    If _listaColumnas Is Nothing Then
                        _listaColumnas = New List(Of ColumnaConfig)
                    End If

                Catch ex As Exception
                    MessageBox.Show($"Error al cargar configuración de columnas: {ex.Message}",
                                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    _listaColumnas = New List(Of ColumnaConfig)
                End Try
            Else
                _listaColumnas = New List(Of ColumnaConfig)
            End If


        'cargar Json de los parametros
        Dim parametrosJson As String = _ioperacionescatalogo.CampoPorNombre("t_ParametrosConfig")
            If Not String.IsNullOrEmpty(parametrosJson) Then
                Try
                _listaParametros = JsonConvert.DeserializeObject(Of List(Of ParametroConfig))(parametrosJson)
                If _listaParametros IsNot Nothing Then _listaParametros = New List(Of ParametroConfig)
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
                ColumnasCargadas.Items.Add($"{col.Orden}: {col.Titulo} ({col.Campo}) -  Formato: {ObtenerNombreFormatoColumna(col.Formato)}")
            Next

            ParametrosCargados.Items.Clear()
            For Each param In _listaParametros.OrderBy(Function(p) p.Orden)
                Dim req As String = If(param.Requerido, "SÍ", "NO")
                ParametrosCargados.Items.Add($"{param.Orden} {req}: {param.Etiqueta} ({param.Nombre}: {param.Tipo}) = {param.ValorDefault}")
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
            _ioperacionescatalogo.CampoPorNombre("t_NombreCliente") = tbCliente.Text
        _ioperacionescatalogo.CampoPorNombre("t_Cve_Consulta") = _cveConsultaSeleccionada.ToString()
        _ioperacionescatalogo.CampoPorNombre("i_Cve_Estatus") = If(chkEstatus.Checked, "1", "0") 'Activo/Inactivo
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

            'Validar orden único
            If _listaColumnas.Any(Function(c) c.Orden = orden) Then
                MessageBox.Show($"Ya existe una columna con el orden {orden}. Por favor, elija un orden diferente.",
                                "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            'validar campo único
            If _listaColumnas.Any(Function(c) c.Campo.Equals(tbCampo.Text, StringComparison.OrdinalIgnoreCase)) Then
                MessageBox.Show($"Ya existe una columna con el campo '{tbCampo.Text}'. Por favor, elija un campo diferente.",
                                "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            'Agregar nueva columna a la lista
            Dim nuevaColumna As New ColumnaConfig With {
                .Campo = tbCampo.Text,
                .Titulo = tbEtiqueta.Text,
                .Formato = cbFormato.SelectedIndex,
                .Orden = orden
            }

            _listaColumnas.Add(nuevaColumna)
            'Refrescar UI y limpiar campos
            RefrescarListasUI()
            TLimpiarCamposColumna()
        End Sub

        Private Sub BModificarColumna_Click(sender As Object, e As EventArgs) Handles bModificarColumna.Click
            'Validar que se haya seleccionado una columna
            If ColumnasCargadas.SelectedIndex < 0 Then
                MessageBox.Show("Debe seleccionar una columna para modificar.",
                            "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            'Obtener columna seleccionada DIRECTAMENTE
            Dim indice As Integer = ColumnasCargadas.SelectedIndex
            Dim listaOrdenada = _listaColumnas.OrderBy(Function(c) c.Orden).ToList()

            'Validar que el índice sea válido
            If indice < 0 OrElse indice >= listaOrdenada.Count Then
                MessageBox.Show("Error al obtener la columna seleccionada.",
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            End If

            Dim columnaSeleccionada = listaOrdenada(indice)

            'Validar campos de columna
            If String.IsNullOrWhiteSpace(tbCampo.Text) OrElse String.IsNullOrWhiteSpace(tbEtiqueta.Text) Then
                MessageBox.Show("Debe ingresar el campo y Etiqueta de la columna.",
                                "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            'Validar que el Orden sea Númerico y positivo
            Dim orden As Integer
            If Not Integer.TryParse(tbOrden.Text, orden) OrElse orden <= 0 OrElse orden > 100 Then
                MessageBox.Show("El orden de la columna debe ser un número entero positivo entre 1 y 100.",
                                "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            'Validar orden único (permitiendo el mismo orden si no se cambia)
            Dim columnaMismoOrden = _listaColumnas.FirstOrDefault(Function(c) c.Orden = orden AndAlso c IsNot columnaSeleccionada)

            If columnaMismoOrden IsNot Nothing Then
                MessageBox.Show($"Ya existe una columna con el orden {orden}. Por favor, elija un orden diferente.",
                                "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            'Validar campo único (permitiendo el mismo campo si no se cambia)
            Dim columnaMismoCampo = _listaColumnas.FirstOrDefault(Function(c) c.Campo.Equals(tbCampo.Text, StringComparison.OrdinalIgnoreCase) AndAlso c IsNot columnaSeleccionada)
            If columnaMismoCampo IsNot Nothing Then
                MessageBox.Show($"Ya existe una columna con el campo '{tbCampo.Text}'. Por favor, elija un campo diferente.",
                                "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            'Modificar la columna seleccionada en la lista
            columnaSeleccionada.Titulo = tbEtiqueta.Text.Trim()
            columnaSeleccionada.Campo = tbCampo.Text.Trim()
            columnaSeleccionada.Formato = cbFormato.SelectedIndex
            columnaSeleccionada.Orden = orden
            'Refrescar UI y limpiar campos
            RefrescarListasUI()
            TLimpiarCamposColumna()
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

        Private Sub TLimpiarCamposColumna()
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


        If _listaParametros.Any(Function(p) p.Nombre.ToUpper = tbNombre.Text.ToUpper()) Then
                MessageBox.Show($"Ya existe un parámetro con ese nombre.",
                                "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

        If Not ValidarValorDefault(TbValorDefault.Text, cbTipo.SelectedItem.ToString()) Then
            MessageBox.Show("El valor default no es válido para el tipo seleccionado.",
                                "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        'validar orden único
        If _listaParametros.Any(Function(p) p.Orden = orden) Then
                MessageBox.Show($"Ya existe un parámetro con el orden {orden}. Por favor, elija un orden diferente.",
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
                .Orden = orden
            }
            _listaParametros.Add(nuevoParametro)
            'Refrescar UI y limpiar campos
            RefrescarListasUI()
            TLimpiarCamposParametro()
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

        If Not ValidarValorDefault(TbValorDefault.Text, cbTipo.SelectedItem.ToString()) Then
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
            TLimpiarCamposParametro()
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

        Private Sub TLimpiarCamposParametro()
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

            If _cveConsultaSeleccionada = 0 Then
                MessageBox.Show("Debe seleccionar una consulta/reporte de la lista.",
                            "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                e.Cancel = True : Return
            End If

            If String.IsNullOrEmpty(tbCliente.Text) Then
                    Dim resultado = MessageBox.Show("El nombre del cliente está vacío. ¿Desea continuar sin asignar un cliente?",
                                                "Advertencia", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                    If resultado = DialogResult.No Then
                        e.Cancel = True
                        Return
                    End If
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
            If Not ValidarIntegridadJSON() Then
                e.Cancel = True
                Return
            End If

        End If
        End Sub

    Private Function ValidarIntegridadJSON() As Boolean
        'Validar que no haya nobres duplicados en las columnas
        Dim camposDuplicados = _listaColumnas.GroupBy(Function(c) c.Campo.ToUpper()).
                Where(Function(g) g.Count() > 1).Select(Function(g) g.Key).ToList()

        If camposDuplicados.Any() Then
            MessageBox.Show($"Los siguiuentes campos están duplicados: {String.Join(", ", camposDuplicados)}",
                                "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return False
        End If

        'Validar que no haya nombres de parámetros duplicados
        Dim parametrosDuplicados = _listaParametros.GroupBy(Function(p) p.Nombre.ToUpper()).
                Where(Function(g) g.Count() > 1).Select(Function(g) g.Key).ToList()

        If parametrosDuplicados.Any() Then
            MessageBox.Show($"Los siguientes nombres de parámetros están duplicados: {String.Join(", ", parametrosDuplicados)}",
                                "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return False
        End If

        Return True
    End Function

    Private Function ObtenerNombreFormatoColumna(formatoIndex As Integer) As String
            Select Case formatoIndex
                Case 0 : Return "Moneda"
                Case 1 : Return "Número"
                Case 2 : Return "Número Entero"
                Case 3 : Return "Porcentaje"
                Case 4 : Return "Fecha"
                Case 5 : Return "Texto"
                Case 6 : Return "Sin formato"
                Case Else : Return formatoIndex.ToString()
            End Select
        End Function

    'Validar valor default de los parámetros dependiendo del tipo seleccionado
    Private Function ValidarValorDefault(valor As String, tipo As String) As Boolean
        If String.IsNullOrWhiteSpace(valor) Then Return True

        Try
            Select Case tipo
                Case "String"
                    Return True

                Case "Int"
                    Dim r As Integer
                    If Not Integer.TryParse(valor, r) Then
                        MessageBox.Show($"'{valor}' no es un entero válido.",
                                        "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        Return False
                    End If
                    Return True

                Case "Decimal"
                    Dim r As Decimal
                    If Not Decimal.TryParse(valor, r) Then
                        MessageBox.Show($"'{valor}' no es un decimal válido.",
                                        "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        Return False
                    End If
                    Return True

                Case "Date"
                    ' Acepta expresiones dinámicas conocidas
                    Dim vUpper = valor.ToUpperInvariant()
                    If _expresionesValidas.Contains(vUpper) Then Return True
                    ' Acepta HOY-N y HOY+N
                    If vUpper.StartsWith("HOY-") OrElse vUpper.StartsWith("HOY+") Then
                        Dim n As Integer
                        If Integer.TryParse(vUpper.Substring(4), n) Then Return True
                        MessageBox.Show($"'{valor}' no es válido. Use HOY-N o HOY+N (ej: HOY-7).",
                                        "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        Return False
                    End If
                    ' Acepta fecha fija parseble
                    Dim fecha As DateTime
                    If Not DateTime.TryParse(valor, fecha) Then
                        MessageBox.Show(
                            $"'{valor}' no es una fecha válida." & Environment.NewLine &
                            "Use una fecha fija (2026-01-01) o una expresión dinámica:" &
                            Environment.NewLine &
                            String.Join(", ", _expresionesValidas) & ", HOY-N, HOY+N",
                            "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        Return False
                    End If
                    Return True

                Case Else
                    Return True
            End Select
        Catch ex As Exception
            MessageBox.Show($"Error validando valor default: {ex.Message}",
                            "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return False
        End Try
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