Imports System.Data
Imports System.Data.SqlClient
Imports Krom.Reporteador2.Modelos
Imports Krom.Reporteador2.Modelos.PlantillaModelo.FiltroPlantilla


Namespace Krom.Reporteador2.Datos

    Public Class PlantillaAccesoDatos
        Private ReadOnly _cadenaConexion As String

        Public Sub New(cadenaConexion As String)
            _cadenaConexion = cadenaConexion
        End Sub

        'Obtener plantillas activas usando la VT
        Public Function ObtenerPlantillasActivas(filtro As FiltroPlantilla) As DataTable
            Dim dt As New DataTable()

            Using conexion As New SqlConnection(_cadenaConexion)
                'usamos la vista de Trabajo

                Dim query As String = "SELECT * FROM Vt016Plantillas WHERE 1=1"

                ' Aplicar filtros según la VE (TipoFiltro)
                If Not String.IsNullOrEmpty(filtro.Nombre) Then
                    query += " AND t_Nombre LIKE @Nombre"
                End If

                If filtro.FechaInicio.HasValue Then
                    query += " AND CAST(f_FechaRegistro AS DATE) >= @FechaInicio"
                End If

                If filtro.FechaFin.HasValue Then
                    query += " AND CAST(f_FechaRegistro AS DATE) <= @FechaFin"
                End If

                If Not String.IsNullOrEmpty(filtro.Estatus) Then
                    query += " AND t_Estatus = @Estatus"
                End If

                query += " ORDER BY i_Cve_Plantilla DESC"

                Using cmd As New SqlCommand(query, conexion)
                    ' Parámetros
                    If Not String.IsNullOrEmpty(filtro.Nombre) Then
                        cmd.Parameters.AddWithValue("@Nombre", "%" & filtro.Nombre & "%")
                    End If

                    If filtro.FechaInicio.HasValue Then
                        cmd.Parameters.AddWithValue("@FechaInicio", filtro.FechaInicio.Value.Date)
                    End If

                    If filtro.FechaFin.HasValue Then
                        cmd.Parameters.AddWithValue("@FechaFin", filtro.FechaFin.Value.Date)
                    End If

                    If Not String.IsNullOrEmpty(filtro.Estatus) Then
                        cmd.Parameters.AddWithValue("@Estatus", filtro.Estatus)
                    End If

                    conexion.Open()
                    Using da As New SqlDataAdapter(cmd)
                        da.Fill(dt)
                    End Using
                End Using
            End Using

            Return dt
        End Function

        'Obtener plantilla por ID
        Public Function ObtenerPlantillaPorId(id As Integer) As PlantillaModelo
            Dim plantilla As PlantillaModelo = Nothing
            Using conexion As New SqlConnection(_cadenaConexion)
                Dim query As String = "SELECT * FROM Cat016PlantillasReporteador WHERE i_Cve_Plantilla = @Id"
                Using cmd As New SqlCommand(query, conexion)
                    cmd.Parameters.AddWithValue("@Id", id)
                    conexion.Open()
                    Using reader As SqlDataReader = cmd.ExecuteReader()
                        If reader.Read() Then
                            plantilla = New PlantillaModelo() With {
                                .i_Cve_Plantilla = Convert.ToInt32(reader("i_Cve_Plantilla")),
                                .t_Nombre = reader("t_Nombre").ToString(),
                                .t_Descripcion = reader("t_Descripcion").ToString(),
                                .t_RutaPlantilla = reader("t_RutaPlantilla").ToString(),
                                .t_NombreBaseDatos = reader("t_NombreBaseDatos").ToString(),
                                .t_Consulta = reader("t_Consulta").ToString(),
                                .t_ColumnasConfig = reader("t_ColumnasConfig").ToString(),
                                .t_ParametrosConfig = reader("t_ParametrosConfig").ToString(),
                                .t_FormatoSalida = reader("t_FormatoSalida").ToString(),
                                .f_FechaRegistro = Convert.ToDateTime(reader("f_FechaRegistro")),
                                .t_UsuarioRegistro = reader("t_UsuarioRegistro").ToString(),
                                .t_Estatus = reader("t_Estatus").ToString(),
                                .t_Estado = reader("t_Estado").ToString()
                            }
                            plantilla.ConvertirParametros()
                        End If
                    End Using
                End Using
            End Using
            Return plantilla
        End Function

        'Guardar plantilla (inserción o actualización)
        Public Function GuardarPlantilla(plantilla As PlantillaModelo, usuario As String) As Integer
            Using conexion As New SqlConnection(_cadenaConexion)
                Dim query As String
                Dim NuevoId As Integer = 0

                If plantilla.EsNuevo OrElse plantilla.i_Cve_Plantilla = 0 Then
                    query = "INSERT INTO Cat016PlantillasReporteador (t_Nombre, t_Descripcion, t_RutaPlantilla, t_NombreBaseDatos, t_Consulta, t_ColumnasConfig, t_ParametrosConfig, t_FormatoSalida, f_FechaRegistro, t_UsuarioRegistro, i_Cve_Estatus, i_Cve_Estado) " &
                            "VALUES (@Nombre, @Descripcion, @RutaPlantilla, @NombreBaseDatos, @Consulta, @ColumnasConfig, @ParametrosConfig, @FormatoSalida, @FechaRegistro, @UsuarioRegistro, @Status, @Estado)"
                Else
                    query = "UPDATE Cat016PlantillasReporteador SET t_Nombre = @Nombre, t_Descripcion = @Descripcion, t_RutaPlantilla = @RutaPlantilla, t_NombreBaseDatos = @NombreBaseDatos, " &
                            "t_Consulta = @Consulta, t_ColumnasConfig = @ColumnasConfig, t_ParametrosConfig = @ParametrosConfig, t_FormatoSalida = @FormatoSalida, f_FechaRegistro = @FechaRegistro, " &
                            "t_UsuarioRegistro = @UsuarioRegistro, t_Estatus = @Estatus, t_Estado = @Estado WHERE i_Cve_Plantilla = @Id"
                End If
                Using cmd As New SqlCommand(query, conexion)
                    cmd.Parameters.AddWithValue("@Nombre", plantilla.t_Nombre)
                    cmd.Parameters.AddWithValue("@Descripcion", plantilla.t_Descripcion)
                    cmd.Parameters.AddWithValue("@RutaPlantilla", plantilla.t_RutaPlantilla)
                    cmd.Parameters.AddWithValue("@NombreBaseDatos", plantilla.t_NombreBaseDatos)
                    cmd.Parameters.AddWithValue("@Consulta", plantilla.t_Consulta)
                    cmd.Parameters.AddWithValue("@ColumnasConfig", plantilla.t_ColumnasConfig)
                    cmd.Parameters.AddWithValue("@ParametrosConfig", plantilla.GenerarParametrosJson())
                    cmd.Parameters.AddWithValue("@FormatoSalida", plantilla.t_FormatoSalida)
                    cmd.Parameters.AddWithValue("@FechaRegistro", DateTime.Now)
                    cmd.Parameters.AddWithValue("@UsuarioRegistro", plantilla.t_UsuarioRegistro)
                    cmd.Parameters.AddWithValue("@Status", plantilla.t_Status)
                    cmd.Parameters.AddWithValue("@Estado", plantilla.t_Estado)
                    If Not plantilla.EsNuevo Then
                        cmd.Parameters.AddWithValue("@Id", plantilla.i_Cve_Plantilla)
                        cmd.Parameters.AddWithValue("@Estatus", plantilla.t_Estatus)
                        cmd.Parameters.AddWithValue("@Estado", plantilla.t_Estado)
                    End If
                    conexion.Open()
                    cmd.ExecuteNonQuery()

                End Using
            End Using
            Return True
        End Function

        ' Eliminar plantilla (cambiar estatus a inactivo)
        Public Sub EliminarPlantilla(id As Integer, usuario As String)
            Using conexion As New SqlConnection(_cadenaConexion)
                Dim query As String = "UPDATE Cat016PlantillasReporteador SET t_Estatus = 'Inactivo', f_FechaRegistro = @FechaRegistro, t_UsuarioRegistro = @UsuarioRegistro WHERE i_Cve_Plantilla = @Id"
                Using cmd As New SqlCommand(query, conexion)
                    cmd.Parameters.AddWithValue("@Id", id)
                    cmd.Parameters.AddWithValue("@FechaRegistro", DateTime.Now)
                    cmd.Parameters.AddWithValue("@UsuarioRegistro", usuario)
                    conexion.Open()
                    cmd.ExecuteNonQuery()
                End Using
            End Using
        End Sub
    End Class

End Namespace