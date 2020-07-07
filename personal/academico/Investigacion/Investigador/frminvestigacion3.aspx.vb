
Partial Class Investigador_frminvestigacion3
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            Me.Form.Attributes.Add("OnSubmit", "return OcultarTabla()")
            Me.DDLFinanciamiento.Attributes.Add("OnChange", "MuestraCaja();")
            Dim datos As Data.DataTable
            Dim codigo_inv As String
            codigo_inv = Request.QueryString("codigo_Inv")
            If Request.QueryString("codigo_Inv") <> "" Then
                Dim ObjInv As New Investigacion
                datos = ObjInv.ConsultarInvestigaciones("13", codigo_inv.ToString)
                Me.LblTitulo.Text = datos.Rows(0).Item("Titulo_Inv")
                'Me.LblFecIni.Text = CDate(datos.Rows(0).Item("fechaInicio_Inv")).ToShortDateString
                'Me.LblFecFin.Text = CDate(datos.Rows(0).Item("fechaFin_Inv")).ToShortDateString
                Me.TxtCosto.Text = datos.Rows(0).Item("costo_Inv").ToString
                Me.DDLFinanciamiento.SelectedValue = datos.Rows(0).Item("tipoFinanciamiento_Inv").ToString
                datos.Dispose()
                ObjInv = Nothing
            End If
        End If
    End Sub

    Protected Sub CmdGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdGuardar.Click
        Dim ObjInv As New Investigacion
        Dim strRuta As String
        strRuta = Server.MapPath("../../../../filesInvestigacion/")
        If ObjInv.ModificarInvestigacion(CInt(Request.QueryString("codigo_inv")), "", 2, 0, Me.FileArchivo, strRuta, _
                                         Me.DDLFinanciamiento.SelectedValue, CDbl(Me.TxtCosto.Text), Request.QueryString("id"), _
                                         0, 0, "", "", "", "", Me.TxtDesFin.Text.Trim) <> -1 Then
            Response.Write("<script>window.opener.location.reload();window.close();</script>")
        Else
            Me.LblMensaje.ForeColor = Drawing.Color.Red
            Me.LblMensaje.Text = "Ocurrió un error al registrar los datos"
        End If

    End Sub
End Class
