
Partial Class Investigador_frminvestigacionotros
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            'Me.TxtFecFin.Attributes.Add("OnKeyDown", "return false")
            Me.TxtFecInicio.Attributes.Add("OnKeyDown", "return false")

            Dim objdatos As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
            'ClsFunciones.LlenarListas(Me.DDLTipo, objdatos.TraerDataTable("ConsultarInvestigaciones", 5, ""), 0, 1, "----- Seleccione Tipo de Investigación -----")
            objdatos = Nothing

        End If
        
    End Sub

    Protected Sub CmdGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdGuardar.Click
        Dim strruta As String
        strruta = Server.MapPath("../../../../filesInvestigacion/")
        Dim objNuevoInv As New Investigacion
        Dim codigo_inv As Integer

        If RbUSAT.Checked = True Then
            Me.TxtInstitucion.Text = "UNIVERSIDAD CATOLICA SANTO TORIBIO DE MOGROVEJO"
        End If

        codigo_inv = objNuevoInv.NuevaInvestigacionOtros("1", Me.TxtTitulo.Text, CDate(Me.TxtFecInicio.Text), 1, -1, 1, CDate(Me.TxtFecInicio.Text), Me.FileInforme, Me.FileResumen, strruta, _
        Me.TxtInstitucion.Text, Request.QueryString("id"), Me.TxtDuracion.Text, Me.DDLDuracion.SelectedValue, Me.DDLAmbito.SelectedValue, Me.DDLPoblacion.SelectedValue, Me.TxtDetalle.Text.Trim, Me.TxtCita.Text.Trim)

        If codigo_inv <> -1 Then
            
            Response.Redirect("agrega_responsables.aspx?modo=ant&id=" & Request.QueryString("id") & "&codigo_Inv=" & codigo_inv)
        Else
            Me.lblMensjae.ForeColor = Drawing.Color.Red
            Me.lblMensjae.Text = "Ocurrio un error al insertar la investigación"
        End If
        objNuevoInv = Nothing
    End Sub

   
End Class
