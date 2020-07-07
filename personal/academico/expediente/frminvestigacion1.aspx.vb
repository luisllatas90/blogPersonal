
Partial Class Investigador_frminvestigacion1
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            Dim ObjCombo As New combos
            ObjCombo.LLenaTematica(Me.DDLTematica, Request.QueryString("id"))
            ObjCombo.LlenaTipoInvestigacion(Me.DDLTipo)
            Me.TxtFecFin.Attributes.Add("OnKeyDown", "return false")
            Me.TxtFecInicio.Attributes.Add("OnKeyDown", "return false")
        End If
    End Sub

    Protected Sub CmdGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdGuardar.Click
        Dim strruta As String
        strruta = Server.MapPath("../../../filesInvestigacion/")
        Dim objNuevoInv As New Personal
        Dim codigo_inv As Integer

        If RbUSAT.Checked = True Then
            Me.TxtInstitucion.Text = "UNIVERSIDAD CATOLICA SANTO TORIBIO DE MOGROVEJO"
        End If

        codigo_inv = objNuevoInv.NuevaInvestigacion("1", Me.TxtTitulo.Text, CDate(Me.TxtFecInicio.Text), _
        CDate(Me.TxtFecFin.Text), Me.DDLTipo.SelectedValue, 1, Me.DDLTematica.SelectedValue, _
        1, Me.TxtBeneficiarios.Text, Now(), Me.FileInforme, Me.FileResumen, strruta, Me.TxtInstitucion.Text, Request.QueryString("id"))

        If codigo_inv <> -1 Then
            Response.Redirect("agrega_responsables.aspx?codigo_Inv=" & codigo_inv)
        Else
            Me.lblMensjae.ForeColor = Drawing.Color.Red
            Me.lblMensjae.Text = "OCurrio un error al insertar la investigación" & codigo_inv
        End If
        objNuevoInv = Nothing
    End Sub

End Class
