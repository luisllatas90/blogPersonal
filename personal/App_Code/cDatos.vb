Imports Microsoft.VisualBasic
Imports System.Data

Public Class cDatos
    Dim clsConexion As New ClsConectarDatos

    Public Function ConsultaAmbiente() As DataTable
        Dim dt As New DataTable
        Try
           
            clsConexion.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
									   
  			clsConexion.AbrirConexion()
            dt = clsConexion.TraerDataTable("BIB_consultarAmbienteCVE")
            clsConexion.CerrarConexion()
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function ConsultaAmbienteActivos() As DataTable
        Dim dt As New DataTable
        Try
             
            clsConexion.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            clsConexion.AbrirConexion()
            dt = clsConexion.TraerDataTable("BIB_consultarAmbienteActivosCVE")
            clsConexion.CerrarConexion()
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function




    Public Function ConsultaReserva(ByVal oConsultaReserva As Object) As DataTable
        Dim dt As New DataTable
        Try
          
            clsConexion.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
			clsConexion.AbrirConexion()
            dt = clsConexion.TraerDataTable("BIB_consultarReservaCVE", oConsultaReserva)
            clsConexion.CerrarConexion()
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function ConsultaEquipos(ByVal oConsultaReserva As Object) As DataTable
        Dim dt As New DataTable
        Try
             
            clsConexion.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
			clsConexion.AbrirConexion()
            dt = clsConexion.TraerDataTable("BIB_consultarEquiposCVE", oConsultaReserva)
            clsConexion.CerrarConexion()
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function RegistrarReserva(ByVal idAlumno As String, ByVal idColaborador As String, ByVal fechaReserva As String, ByVal horainicio As String, ByVal horafin As String, ByVal estado As Integer, ByVal equipo_id As Integer) As Object
        Dim dt As Object
        Try
            clsConexion.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            clsConexion.AbrirConexion()
            dt = clsConexion.Ejecutar("BIB_registroReservaCVE", idAlumno, idColaborador, fechaReserva, horainicio, horafin, estado, equipo_id, 0)
            clsConexion.CerrarConexion()

            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function ConsultaReservaByColaborador(ByVal colaborador As String, ByVal fecha As String) As DataTable
        Dim dt As New DataTable
        Try
            
            clsConexion.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
  			clsConexion.AbrirConexion()
            dt = clsConexion.TraerDataTable("BIB_consultarReservaByColaboradorCVC", colaborador, fecha)
            clsConexion.CerrarConexion()
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function RegistrarAmbiente(ByVal nombreAmbiente As String) As Integer
        Dim dt As Integer
        Try
             
            clsConexion.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
  			clsConexion.AbrirConexion()
            dt = clsConexion.Ejecutar("BIB_registroAmbiente", nombreAmbiente)
            clsConexion.CerrarConexion()
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function RegistrarEquipo(ByVal nombreEquipo As String, ByVal ambienteId As Integer) As Integer
        Dim dt As Integer
        Try
             
            clsConexion.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
			clsConexion.AbrirConexion()
            dt = clsConexion.Ejecutar("BIB_registroEquipo", nombreEquipo, ambienteId)
            clsConexion.CerrarConexion()
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function EditarAmbiente(ByVal nombreEquipo As String, ByVal estado As Integer, ByVal ambiente_id As Integer) As Integer
        Dim dt As Integer
        Try
             
            clsConexion.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
  			clsConexion.AbrirConexion()
            dt = clsConexion.Ejecutar("BIB_updateAmbiente", nombreEquipo, estado, ambiente_id)
            clsConexion.CerrarConexion()
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function ConsultaEquipo() As DataTable
        Dim dt As New DataTable
        Try
            
            clsConexion.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
  			clsConexion.AbrirConexion()
            dt = clsConexion.TraerDataTable("BIB_consultarEquipoAll")
            clsConexion.CerrarConexion()
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function EditarEquipo(ByVal nombreEquipo As String, ByVal estado As Integer, ByVal ambiente_id As Integer, ByVal equipo_id As Integer) As Integer
        Dim dt As Integer
        Try
             
            clsConexion.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
			clsConexion.AbrirConexion()
            dt = clsConexion.Ejecutar("BIB_updateEquipo", nombreEquipo, estado, ambiente_id, equipo_id)
            clsConexion.CerrarConexion()
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function AnularReserva(ByVal idReserva As Integer, ByVal estado As Integer) As Integer
        Dim dt As Integer
        Try
          
            clsConexion.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            clsConexion.AbrirConexion()
            dt = clsConexion.Ejecutar("BIB_anularReservaCVE", idReserva, estado)
            clsConexion.CerrarConexion()
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
	
	
	''agregado el 03/08/2018
	Public Function ListEstudiantes() As DataTable
        Dim dt As New DataTable
        Try
            
            clsConexion.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            clsConexion.AbrirConexion()
            dt = clsConexion.TraerDataTable("BIB_consultarEstudiantes")
            clsConexion.CerrarConexion()
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function ListColaboradores() As DataTable
        Dim dt As New DataTable
        Try
             
            clsConexion.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            clsConexion.AbrirConexion()
            dt = clsConexion.TraerDataTable("BIB_consultarColaboradores")
            clsConexion.CerrarConexion()
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function RegisterUsuarioEstudianteSuspension(ByVal idAlumno As Integer) As Integer
        Dim dt As Integer
        Try
             
            clsConexion.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            clsConexion.AbrirConexion()
            dt = clsConexion.Ejecutar("BIB_registerUsuarioSuspensionEstudiante", idAlumno)
            clsConexion.CerrarConexion()
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function

	
	
	'02  03/08/2018
	   Public Function RegisterUsuarioColaboradorSuspension(ByVal idColaborador As Integer) As Integer
        Dim dt As Integer
        Try
             
            clsConexion.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            clsConexion.AbrirConexion()
            dt = clsConexion.Ejecutar("BIB_registerUsuarioSuspensionColaborador", idColaborador)
            clsConexion.CerrarConexion()
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function UpdateUsuarioSuspension(ByVal idUsuario As Integer, ByVal flag_equipo As Integer) As Integer
        Dim dt As Integer
        Try
             
            clsConexion.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            clsConexion.AbrirConexion()
            dt = clsConexion.Ejecutar("BIB_updateUsuarioSuspension", idUsuario, flag_equipo)
            clsConexion.CerrarConexion()
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function ObtenerMaximoUsuario() As DataTable
        Dim dt As New DataTable
        Try
             
            clsConexion.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            clsConexion.AbrirConexion()
            dt = clsConexion.TraerDataTable("BIB_obtenerMaxBIBusuario")
            clsConexion.CerrarConexion()
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function

	
	'03/08/2018
	 Public Function RegisterSuspensionByUsuario(ByVal idUsuario As Integer, ByVal fecha_inicio As String, ByVal fecha_fin As String) As Integer
        Dim dt As Integer
        Try
            
            clsConexion.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            clsConexion.AbrirConexion()
            dt = clsConexion.Ejecutar("BIB_registerSuspensionByUsuario", idUsuario, fecha_inicio, fecha_fin)
            clsConexion.CerrarConexion()
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function ConsultarSuspension(ByVal idColaborador As Integer, ByVal fecha As String) As DataTable
        Dim dt As New DataTable
        Try
            
            clsConexion.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            clsConexion.AbrirConexion()
            dt = clsConexion.TraerDataTable("BIB_consultarSuspensionCVC", idColaborador, fecha)
            clsConexion.CerrarConexion()
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function ConsultarReservaByColaboradorFecha(ByVal idColaborador As Integer, ByVal fecha As String) As DataTable
        Dim dt As New DataTable
        Try
          
            clsConexion.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            clsConexion.AbrirConexion()
            dt = clsConexion.TraerDataTable("BIB_consultarReservaColaboradorFecha", idColaborador, fecha)
            clsConexion.CerrarConexion()
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
End Class
