
Partial Class perfil
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            Me.CmdGuardar.Attributes.Add("OnMouseOver", "tabsobre(this,1)")
            Me.CmdGuardar.Attributes.Add("OnMouseOut", "tabsobre(this,2)")
            Dim obj As New Personal
            Me.TxtPerfil.Text = obj.ObtienePerfilPersonal(Request.QueryString("id"))
            obj = Nothing
        End If
    End Sub

    'Se ejecuta cuando acepta el modal
    Protected Sub btnGuardarInforme_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardarInforme.Click
        Try
            Dim objPersonal As New Personal
            Dim i As Integer = objPersonal.ActualizarEstadoDeclaracionJurada(Request.QueryString("id"))
            Grabar()
            Response.Redirect("educacionuniversitaria.aspx?menu=" & Request.QueryString("menu") & "&id=" & Request.QueryString("id") & "&ctf=" & Request.QueryString("ctf"))
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub btnCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        Try
            ClientScript.RegisterStartupScript(Me.GetType, "FaltanDatos", "alert('Los datos consignados no fueron registrados.');", True)
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub CmdGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdGuardar.Click

        Try
            '-----------------------------------------------------------------------------------------------
            ' Verifica que el trabajador haya aceptado la declaracion jurada para el registro de sus datos
            '-----------------------------------------------------------------------------------------------
            Dim objPersonal As New Personal
            Dim dts As New Data.DataTable
            objPersonal.codigo = Request.QueryString("id")
            dts = objPersonal.VerificaDeclaracionJuradaPersonal(Request.QueryString("id"))
            If dts.Rows.Count > 0 Then
                If dts.Rows(0).Item("rpt") = 0 Then
                    Me.lblDeclarante.Text = dts.Rows(0).Item("Declarante").ToString
                    mpeInforme.Show()
                Else
                    Grabar()
                    Response.Redirect("educacionuniversitaria.aspx?menu=" & Request.QueryString("menu") & "&id=" & Request.QueryString("id") & "&ctf=" & Request.QueryString("ctf"))
                End If
            End If
            '-----------------------------------------------------------------------------------------------
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub
    Private Sub Grabar()
        Dim obj As New Personal
        Dim Script As String

        If obj.GrabarPerfilPersonal(Request.QueryString("id"), Me.TxtPerfil.Text.Trim) <> -1 Then
            Script = "<script>alert('Se grabaron los datos satisfactoriamente.')</script>"
            Page.RegisterClientScriptBlock("Exito", Script)
        Else
            Script = "<script>alert('Ocurrió un errir al grabar los datos.')</script>"
            Page.RegisterClientScriptBlock("Error", Script)
        End If
        obj = Nothing
    End Sub
    Protected Sub CmdVolver_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdVolver.Click
        Grabar()
        Response.Redirect("personales.aspx?menu=" & Request.QueryString("menu") & "&id=" & Request.QueryString("id") & "&ctf=" & Request.QueryString("ctf"))
    End Sub
End Class
