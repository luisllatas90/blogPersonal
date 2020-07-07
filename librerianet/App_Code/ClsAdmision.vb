Imports Microsoft.VisualBasic

Public Class ClsAdmision
    Private mo_Cnx As New ClsConectarDatos

#Region "FUNCIONES"
    Public Function ConsultarDatosAlumno(ByVal tipo As String, ByVal param As String) As Data.DataTable
        Dim lo_Dts As New Data.DataTable
        mo_Cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        mo_Cnx.AbrirConexion()
        lo_Dts = mo_Cnx.TraerDataTable("ConsultarAlumno", tipo, param)
        mo_Cnx.CerrarConexion()
        Return lo_Dts
    End Function

    Public Function ConsultarDatosUsuario(ByVal tipo As String, ByVal codigoPer As String) As Data.DataTable
        Dim lo_Dts As New Data.DataTable
        mo_Cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        mo_Cnx.AbrirConexion()
        lo_Dts = mo_Cnx.TraerDataTable("spPla_ConsultarDatosPersonal", tipo, codigoPer)
        mo_Cnx.CerrarConexion()
        Return lo_Dts
    End Function

    Public Function ObtenerDatosInscripcion(ByVal ls_CodigoAlu As String, ByVal ls_TipoConsulta As String) As Data.DataTable
        Dim lo_Dts As New Data.DataTable
        mo_Cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        mo_Cnx.AbrirConexion()
        Dim ls_Tipo As String = "1" 'Según procedimiento: Agregar E-Pre+Encuentra alumno registrado
        lo_Dts = mo_Cnx.TraerDataTable("ADM_ConsultarAlumnoInscripcion", ls_CodigoAlu, ls_TipoConsulta)
        mo_Cnx.CerrarConexion()
        Return lo_Dts
    End Function
#End Region

#Region "VARIABLES_GLOBALES"
    '5531: andy.diaz | 473: Sra. Julia
    Public Shared CodigoJefaPensiones As String = IIf(ConfigurationManager.AppSettings("CorreoUsatActivo") = "0", 5531, 473)
    Public Shared CorreoServiciosTI As String = "servicios.ti@usat.edu.pe"
#End Region
End Class
