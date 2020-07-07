
Partial Class PlanProyecto_frmListaPlan
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (IsPostBack = False) Then
            If ((Request.QueryString("id") IsNot Nothing) And (Request.QueryString("ctf") IsNot Nothing)) Then
                Me.lblMensaje.Text = ""
                CargaProyectos(Request.QueryString("id"))

                If (Request.QueryString("ctf") = "1") Then
                    Me.btnNuevo.Visible = True
                Else
                    Me.btnNuevo.Visible = False
                End If

            Else
                Me.lblMensaje.Text = "Error al cargar formulario"
            End If
        End If
    End Sub

    Private Sub CargaProyectos(ByVal codigo_per As Integer)        
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        Dim dtProyecto As New Data.DataTable
        obj.AbrirConexion()
        dtProyecto = obj.TraerDataTable("PLAN_BuscaProyecto", 0, "", codigo_per)
        obj.CerrarConexion()
        If (dtProyecto.Rows.Count > 0) Then
            ClsFunciones.LlenarListas(Me.dpProyectos, dtProyecto, "codigo_pro", "descripcion_pro", "--Seleccione--")
            Me.lblMensaje.Text = ""
        Else
            Me.lblMensaje.Text = "No se encontraron proyectos relacionados con su usuario"
        End If
        dtProyecto.Dispose()
        obj = Nothing
    End Sub

    Private Sub EnviarAPagina(ByVal pagina As String)
        Me.fradetalle.Attributes("src") = pagina
    End Sub

    Protected Sub dpProyectos_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dpProyectos.SelectedIndexChanged
        If (Me.dpProyectos.SelectedItem.Text <> "--Seleccione--") Then
            lnkResumen_Click(sender, e)
            Me.lblMensaje.Text = ""
        Else
            Me.lblMensaje.Text = "Debe seleccionar un registro"
        End If
    End Sub

    Protected Sub lnkResumen_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkResumen.Click
        EnviarAPagina("frmResumen.aspx?pro=" & Me.dpProyectos.SelectedValue)
    End Sub

    Protected Sub lnkConfiguracion_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkConfiguracion.Click

    End Sub

    Protected Sub lnkGrupo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkGrupo.Click
        EnviarAPagina("frmRegistroGrupo.aspx?pro=" & Me.dpProyectos.SelectedValue)
    End Sub

    Protected Sub btnNuevo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNuevo.Click
        If (Me.dpProyectos.SelectedItem.Text <> "--Seleccione--") Then
            EnviarAPagina("frmRegistroGrupo.aspx?pl=" & Me.dpProyectos.SelectedValue)
        Else
            Me.lblMensaje.Text = "Debe seleccionar un Plan"
        End If

    End Sub
End Class
