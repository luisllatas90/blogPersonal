
Partial Class DirectorInvestigacion_frmagregarunidadinvestigacion
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            Dim ObjInv As New Investigacion
            Dim Objcombos As New ClsFunciones
            ClsFunciones.LlenarListas(Me.LstUnidades, ObjInv.ConsultarUnidadesInvestigacion("12", ""), "codigo_cco", "descripcion_cco")
            Objcombos = Nothing
            ObjInv = Nothing
        End If
    End Sub

    Protected Sub CmdBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdBuscar.Click
        Dim ObjInv As New Investigacion
        Dim Objcombos As New ClsFunciones
        ClsFunciones.LlenarListas(Me.LstUnidades, ObjInv.ConsultarUnidadesInvestigacion("13", Me.TextBox1.Text.Trim), "codigo_cco", "descripcion_cco")
        Objcombos = Nothing
        ObjInv = Nothing
    End Sub

    Protected Sub CmdAgregar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdAgregar.Click
        Dim objinv As New Investigacion
        If objinv.ModificarUnidadInvestigacion(2, Me.LstUnidades.SelectedValue) = 1 Then
            Response.Write("<script>window.opener.location.reload(); window.close();</script>")
        Else
            Me.LblMensaje.ForeColor = Drawing.Color.Red
            Me.LblMensaje.Text = "Ocurrio un error al procesar los datos, intemtelo nuevamente."
        End If
        objinv = Nothing
    End Sub
End Class
