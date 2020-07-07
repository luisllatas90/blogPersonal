Imports Microsoft.VisualBasic
Imports System.Data

Public Class ClsDefensoria
    Private cnx As New ClsConectarDatos

    Public Function ListaDefensoria(ByVal tipo As String, ByVal codigo_def As String, ByVal tipo_def As String, ByVal codigo_per As Integer, ByVal tipo_per As Integer) As Data.DataTable
        Dim dt As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dt = cnx.TraerDataTable("ListadoDefensoria", tipo, codigo_def, tipo_def, codigo_per, tipo_per)
        cnx.CerrarConexion()
        Return dt
    End Function

    Public Function InsertarDefensoria(ByVal codigo_def As Integer, ByVal telefono_def As String, ByVal mail_def As String, ByVal detalle_def As String, _
                                       ByVal tipo_def As String, ByVal usuario_reg As Integer, ByVal codigo_alu As Integer, ByVal codigo_per As Integer) As Data.DataTable

        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("AcualizarDefensoria", codigo_def, telefono_def, mail_def, detalle_def, tipo_def, usuario_reg, codigo_alu, codigo_per)
        cnx.CerrarConexion()
        Return dts
    End Function

    ''22-01-2020
    Public Function InsertarRespuestaDefensoria(ByVal codigo_rde As Integer, ByVal codigo_def As Integer, ByVal respuesta_rde As String, ByVal usuario_reg As Integer, ByVal codigo_per As Integer) As Data.DataTable

        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("DEF_ActualizaRespuestaDefensoria", codigo_rde, codigo_def, respuesta_rde, usuario_reg, codigo_per)
        cnx.CerrarConexion()
        Return dts

    End Function

    Public Function EliminarDefensoria(ByVal codigo_def As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("EliminarDefensoria", codigo_def)
        cnx.CerrarConexion()
        Return dts
    End Function


    Public Function ListaNombreUsuario(ByVal tipo As String, ByVal codigo_per As Integer) As Data.DataTable
        Dim dt As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dt = cnx.TraerDataTable("ListaNombreUsuario", tipo, codigo_per)
        cnx.CerrarConexion()
        Return dt
    End Function


    Public Function ConsultaRegistroDefensoria(ByVal tipo As String, ByVal fecini As String, ByVal fecfin As String) As Data.DataTable
        Dim dt As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dt = cnx.TraerDataTable("ConsultaRegistroDefensoria", tipo, fecini, fecfin)
        cnx.CerrarConexion()
        Return dt
    End Function

    Public Function ConsultarRespuesta(ByVal codigo_def As Integer) As Data.DataTable
        Dim dt As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dt = cnx.TraerDataTable("DEF_ConsultaRespuestaDefensoria", codigo_def)
        cnx.CerrarConexion()
        Return dt
    End Function

    Public Function EncrytedString64(ByVal _stringToEncrypt As String) As String
        Dim result As String = ""
        Dim encryted As Byte()
        encryted = System.Text.Encoding.Unicode.GetBytes(_stringToEncrypt)
        result = Convert.ToBase64String(encryted)
        Return result
    End Function

    Public Function DecrytedString64(ByVal _stringToDecrypt As String) As String
        Dim result As String = ""
        Dim decryted As Byte()
        decryted = Convert.FromBase64String(_stringToDecrypt)
        result = System.Text.Encoding.Unicode.GetString(decryted, 0, decryted.Length)
        Return result
    End Function

End Class
