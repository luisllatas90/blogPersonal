
Partial Class PlanProyecto_frmListaGrupo
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (IsPostBack = False) Then
            Me.btnRegresar.Attributes.Add("onclick", "self.parent.tb_remove();")
            If (Session("cod_pro") > 0) Then
                CargaGrupos(Session("cod_pro"))
                Me.lblMensaje.Text = ""
            Else
                Me.lblMensaje.Text = "No se encontró relación con ningun plan"
            End If
        End If        
    End Sub

    Private Sub CargaGrupos(ByVal codigo_pro As Integer)
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        Try
            Dim dtGrupo As New Data.DataTable
            obj.AbrirConexion()
            dtGrupo = obj.TraerDataTable("PLAN_BuscaGrupoProyecto", 0, codigo_pro, "")
            obj.CerrarConexion()
            Me.gvGrupo.DataSource = dtGrupo
            Me.gvGrupo.DataBind()

            If (dtGrupo.Rows.Count > 0) Then
                Me.lblProyecto.Text = "Proyecto: " & dtGrupo.Rows(0).Item("titulo_pro")
            End If

            dtGrupo.Dispose()
            obj = Nothing
        Catch ex As Exception
            obj.CerrarConexion()
            Me.lblProyecto.Text = "Error al realizar la busqueda"
        End Try
    End Sub

    Protected Sub btnConfigurar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnConfigurar.Click
        If (Session("cod_pro") > 0) Then
            Response.Redirect("frmRegistroGrupo.aspx?pl=" & Session("cod_pro"))
        Else
            Me.lblMensaje.Text = "No se encontró relación con ningun plan"
        End If
    End Sub

    Protected Sub btnRegresar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRegresar.Click
        Response.Write("<script>window.close()</script>")
    End Sub

    Protected Sub gvGrupo_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles gvGrupo.RowDeleting        
        EliminaProyecto(Me.gvGrupo.DataKeys.Item(e.RowIndex).Values(0))
        CargaGrupos(Session("cod_pro"))
    End Sub

    Private Sub EliminaProyecto(ByVal codigo_gpr As Integer)
        Dim obj As New ClsConectarDatos
        Dim dtRespuesta As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        Try
            obj.AbrirConexion()
            dtRespuesta = obj.TraerDataTable("PLAN_EliminaGrupoProyecto", codigo_gpr, 0)
            obj.CerrarConexion()
            obj = Nothing

            Me.lblMensaje.Text = dtRespuesta.Rows(0).Item(0).ToString
        Catch ex As Exception
            Me.lblMensaje.Text = "Error al eliminar el Grupo del Proyecto"
        End Try
    End Sub
End Class
