
Partial Class administrativo_Tesoreria_Rendiciones_AppRendiciones_ListarRendiciones
    Inherits System.Web.UI.Page


    Private Sub Listar()
        Dim codigo_per As String
        codigo_per = 30182 ' Session("id_per")
        Session("codigo_per") = Request.QueryString("id")
        '  Response.Write("Hola:"+codigo_per)
        Dim strTbody As New StringBuilder
        Dim obj As New ClsConectarDatos
        Dim tb As New Data.Datatable
        Dim cn As New clsaccesodatos
        Dim i As Integer = 0
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        ' dtscliente = cn.consultar("spDocumentosEgresoRendir", "3", Mid(Me.cboestado.Text, 1, 1), "", "", "", "")
        tb = obj.TraerDataTable("dbo.USP_DOCUMENTOS_X_RENDIR", "1", codigo_per, "P", "", "")
        obj.CerrarConexion()
        If Not tb Is Nothing AndAlso tb.Rows.Count > 0 Then
            For i = 0 To tb.Rows.Count - 1
                strTbody.Append("<tr role='row' id='" & tb.Rows(i).Item("Id").ToString & "' style=''>")
                strTbody.Append("<td>" & tb.Rows(i).Item("Fecha").ToString & "</td>")
                strTbody.Append("<td>" & tb.Rows(i).Item("TipoDocumento").ToString & "</td>")
                strTbody.Append("<td>" & tb.Rows(i).Item("Numero").ToString & "</td>")
                strTbody.Append("<td>" & tb.Rows(i).Item("Moneda").ToString & "</td>")
                strTbody.Append("<td>" & tb.Rows(i).Item("Importe").ToString & "</td>")
                strTbody.Append("<td>" & tb.Rows(i).Item("Estado").ToString & "</td>")
                strTbody.Append("<td>" & tb.Rows(i).Item("Usuario").ToString & "</td>")
                strTbody.Append("<td>" & Trunc(tb.Rows(i).Item("Obsservacion").ToString, 60) & "</td>")
                strTbody.Append("<td>" & "" & "</td>")
                strTbody.Append("<td>" & "" & "</td>")
                strTbody.Append("</tr>")
            Next
        Else
            strTbody.Append("")
        End If
        Me.tbRendicion.InnerHtml = strTbody.ToString
    End Sub
    Function Trunc(ByVal str As String, ByVal num As Integer) As String

        If num > str.Length Then
            Return str
        Else
            str = str.Substring(0, num)

            'str = str.substring(0, num);
            Return str & "..."
        End If

    End Function
   
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Listar()
    End Sub
End Class
