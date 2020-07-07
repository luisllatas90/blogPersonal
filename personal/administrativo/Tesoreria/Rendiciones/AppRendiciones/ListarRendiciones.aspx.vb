
Partial Class administrativo_Tesoreria_Rendiciones_AppRendiciones_ListarRendiciones
    Inherits System.Web.UI.Page

    Private Sub Listar()
        Dim codigo_per As String
        codigo_per = Session("id_per")
        Session("codigo_per") = Request.QueryString("id")
        '  Response.Write("Hola:"+codigo_per)
        Dim strTbody As New StringBuilder
        Dim obj As New ClsConectarDatos
        Dim tb As New Data.Datatable
        Dim cn As New clsaccesodatos
        Dim i As Integer = 0
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        ' Response.Write(Request("EstRend"))
        Dim Estado As String = Request.Form("EstRend")
        If Estado Is Nothing Then
            Estado = "P"
        End If
        If codigo_per Is Nothing Then
            codigo_per = ""
        End If

        ' dtscliente = cn.consultar("spDocumentosEgresoRendir", "3", Mid(Me.cboestado.Text, 1, 1), "", "", "", "")
        tb = obj.TraerDataTable("dbo.USP_DOCUMENTOS_X_RENDIR", "1", codigo_per, Estado, "", "")
        obj.CerrarConexion()
        If Not tb Is Nothing AndAlso tb.Rows.Count > 0 Then
            For i = 0 To tb.Rows.Count - 1
                lblColaborador.InnerHtml = tb.Rows(i).Item("Nombres").ToString
                Me.lblCodigo.InnerHtml = codigo_per
                lblDni.InnerHtml = tb.Rows(i).Item("NumDoc").ToString
                strTbody.Append("<tr role='row' id='" & tb.Rows(i).Item("Id").ToString & "' style=''>")
                strTbody.Append("<td>" & tb.Rows(i).Item("Fecha").ToString & "</td>")
                strTbody.Append("<td>" & tb.Rows(i).Item("TipoDocumento").ToString & "</td>")
                strTbody.Append("<td>" & tb.Rows(i).Item("Numero").ToString & "</td>")
                strTbody.Append("<td>" & tb.Rows(i).Item("Moneda").ToString & "</td>")
                strTbody.Append("<td class='text-right'>" & tb.Rows(i).Item("Importe").ToString & "</td>")
                strTbody.Append("<td>" & tb.Rows(i).Item("Estado").ToString & "</td>")
                strTbody.Append("<td>" & tb.Rows(i).Item("Usuario").ToString & "</td>")
                strTbody.Append("<td>" & Trunc(tb.Rows(i).Item("Obsservacion").ToString, 60) & "</td>")
                strTbody.Append("<td>" & "<a href='#' id='" & tb.Rows(i).Item("Obsservacion").ToString & "'   onclick='Detalle($(this))'><span>Detalle</span></a>" & "</td>")
                strTbody.Append("</tr>")
            Next
        Else
            strTbody.Append("")
        End If

        If tb.Rows.Count = 0 Then
            Dim tbl As New Data.Datatable
            obj.AbrirConexion()
            tbl = obj.TraerDataTable("dbo.USP_DOCUMENTOS_X_RENDIR", "2", codigo_per, Estado, "", "")
            obj.CerrarConexion()

            If tbl.Rows.Count > 0 Then
                lblColaborador.InnerHtml = tbl.Rows(0).Item("Nombres").ToString
                Me.lblCodigo.InnerHtml = codigo_per
                lblDni.InnerHtml = tbl.Rows(0).Item("NumDoc").ToString
            End If
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
        ' If Not Page.IsPostBack Then
        Listar()
        ' End If
    End Sub
End Class
