Imports Newtonsoft.Json
Imports Gsol
Imports Gsol.BaseDatos.Operaciones

Public Class frm000Plantillas
    Inherits FormularioBase64

#Region "Atributos"
    'Listas para guardar en memoria las configuraciones de los catálogos, mientras el usuario las edita.
    Private _listaColumnas As New List(Of ColumnaConfig)
    Private _listaParametros As New List(Of ParametroConfig)
    Private _rutaBaseSeleccionada As String = ""
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

        Select Case tipooperacion_
            Case IOperacionesCatalogo.TiposOperacionSQL.Insercion
                LblAccion.Text = "Nuevo registro"
                'Inicializar listas vacias para una nueva plantilla
                _listaColumnas = New List(Of ColumnaConfig)
                _listaParametros = New List(Of ParametroConfig)


            Case IOperacionesCatalogo.TiposOperacionSQL.Modificar

                LblAccion.Text = "Edición"

                PreparaModificacion()

            Case Else

        End Select
        'Inicializar otros componentes
        InicializaCombos()

        'Actualizar los labels de conteo
        ActualizarConteos()

    End Sub

#End Region

#Region "Métodos"

    Private Sub InicializaCombos()
        'Cargamos Bases de Datos disponibles
        cbBaseDeDatos.Items.Clear()
        cbBaseDeDatos.Items.Add("SysExpert")
        cbBaseDeDatos.Items.Add("Solium")
    End Sub

    Private Sub bloquearControles()
        'Ejemplo de bloqueo de controles
        'TbClaveMaterial.Enabled = False
    End Sub

    Public Overrides Sub PreparaModificacion()

        ':::::::::::::Ejemplo de asignación directa::::::::::::::

        'TbClaveMaterial.Text = _ioperacionescatalogo.CampoPorNombre("i_Cve_Material")

        'TbDescripcion.Text = _ioperacionescatalogo.CampoPorNombre("t_Descripcion")

        'TbCantidad.Text = _ioperacionescatalogo.CampoPorNombre("i_CantidadRequerida")

    End Sub

    Public Overrides Sub RealizarInsercion()

        ':::::::::::::Ejemplo de asignación directa::::::::::::::

        '_ioperacionescatalogo.CampoPorNombre("i_Cve_Requisicion") = _claverequisicion

        '_ioperacionescatalogo.CampoPorNombre("i_Cve_Material") = TbClaveMaterial.Text

        '_ioperacionescatalogo.CampoPorNombre("t_Descripcion") = TbDescripcion.Text

        '_ioperacionescatalogo.CampoPorNombre("i_CantidadRequerida") = TbCantidad.Text

        '_ioperacionescatalogo.CampoPorNombre("i_Cve_Estado") = "1"

    End Sub

    Public Overrides Sub RealizarModificacion()

        ':::::::::::::Ejemplo de asignación directa::::::::::::::

        '_ioperacionescatalogo.CampoPorNombre("i_Cve_Material") = TbClaveMaterial.Text

        '_ioperacionescatalogo.CampoPorNombre("t_Descripcion") = TbDescripcion.Text

        '_ioperacionescatalogo.CampoPorNombre("i_CantidadRequerida") = TbCantidad.Text

        '_ioperacionescatalogo.CampoPorNombre("i_Cve_Estado") = "1"

    End Sub

#End Region


End Class
