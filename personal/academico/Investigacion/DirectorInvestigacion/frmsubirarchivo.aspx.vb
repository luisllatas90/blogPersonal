
Partial Class Investigador_frmsubirarchivo
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            Me.Form.Attributes.Add("OnSubmit", "return OcultarTabla();")
            Dim datos As Data.DataTable
            Dim codigo_inv As String
            codigo_inv = Request.QueryString("codigo_Inv")
            If codigo_inv <> "" Then
                Dim ObjInv As New Investigacion
                datos = ObjInv.ConsultarInvestigaciones("13", codigo_inv.ToString)

                Me.LblTitulo.Text = datos.Rows(0).Item("Titulo_Inv")
                Me.LblFecIni.Text = CDate(datos.Rows(0).Item("fechaInicio_Inv")).ToShortDateString
                Me.LblFecFin.Text = CDate(datos.Rows(0).Item("fechaFin_Inv")).ToShortDateString
                datos.Dispose()
                ObjInv = Nothing
            End If
        End If
    End Sub

    Protected Sub CmdGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdGuardar.Click
        Dim ObjInv As New Investigacion
        Dim strRuta As String

        strRuta = Server.MapPath("../../../../filesInvestigacion/")

        If ObjInv.ActualizarDecreto(CInt(Request.QueryString("codigo_inv")), Me.TxtDecreto.Text, Me.FileArchivo, strRuta) <> -1 Then
            Response.Write("<script>window.opener.location.reload();window.close();</script>")
        Else
            Me.LblMensaje.ForeColor = Drawing.Color.Red
            Me.LblMensaje.Text = "Ocurrió un error al registrar los datos"
        End If

    End Sub
End Class
