
Partial Class personal_academico_estudiante_adminfoto_fotoMdl
    Inherits System.Web.UI.Page
    

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load


        Dim codUniver As String
        Dim codigoFoto As String
        Dim tbl As New System.Data.DataTable

        'codigoFoto = objEnc.CodificaWeb("069" & codUniver)
        'Response.Write(codigoFoto)

        Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("cnxBDUSAT").ConnectionString)
        tbl = obj.TraerDataTable("Moodle_FotoAlumno")
        Dim ruta As String

        Dim obEnc As Object
        obEnc = Server.CreateObject("EncriptaCodigos.clsEncripta")

        Response.Write("<table>")
        For i As Integer = 0 To tbl.Rows.Count - 1
            Response.Write("<tr>")
            Response.Write("<td>CodigoPso: " & tbl.Rows(i).Item("codigo_pso").ToString & "</td>")
            ruta = obEnc.CodificaWeb("069" & tbl.Rows(i).Item("codigouniver_alu").ToString)
            '---------------------------------------------------------------------------------------------------------------
            'Fecha: 29.10.2012
            'Usuario: dguevara
            'Modificacion: Se modifico el http://www.usat.edu.pe por http://intranet.usat.edu.pe
            '---------------------------------------------------------------------------------------------------------------
            ruta = "//intranet.usat.edu.pe/imgestudiantes/" & ruta
            Response.Write("<td>" & ruta & "</td>")
            Response.Write("<td>" & tbl.Rows(i).Item("codigouniver_alu").ToString & "</td>")
            'Dim wc As New System.Net.WebClient
            'wc.DownloadFile(ruta, "myfilename")
        Next
        Response.Write("</table>")


    End Sub
End Class
