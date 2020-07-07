
Partial Class indicadores_POA_FrmListaCategoriaActividadProyecto
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("id_per") = "" Or Request.QueryString("id") = "" Then
            Response.Redirect("../../../sinacceso.html")
        End If

        Try
            If Not IsPostBack Then
                Call CargarCategorias()
                Call CargarActividades()
                 btnBuscar_Click(sender, e)
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Sub CargarCategorias()
        Try
            Dim obj As New clsPlanOperativoAnual
            Dim dt As New Data.DataTable
            dt = obj.POA_CategoriaProgProyActividad(0, 0, 0, 0, "CPP")

            Me.ddlCategoriasProgramaProyecto.DataSource = dt
            Me.ddlCategoriasProgramaProyecto.DataTextField = "descripcion"
            Me.ddlCategoriasProgramaProyecto.DataValueField = "codigo"
            Me.ddlCategoriasProgramaProyecto.DataBind()
            dt.Dispose()
            obj = Nothing
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Sub CargarActividades()
        Try
            Dim obj As New clsPlanOperativoAnual
            Dim dt As New Data.DataTable
            dt = obj.POA_CategoriaProgProyActividad(0, 0, 0, 0, "CCA")

            Me.ddlActividades.DataSource = dt
            Me.ddlActividades.DataTextField = "descripcion"
            Me.ddlActividades.DataValueField = "codigo"
            Me.ddlActividades.DataBind()
            dt.Dispose()
            obj = Nothing
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub btnNuevo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNuevo.Click
        Try
            Dim dt As New Data.DataTable
            Dim obj As New clsPlanOperativoAnual
            Dim codigo_cpa As Integer = 0
            Dim codigo_cap As Integer = ddlCategoriasProgramaProyecto.SelectedValue
            Dim codigo_cat As Integer = ddlActividades.SelectedValue
            Dim usuario_reg As Integer = Request.QueryString("id")

            If codigo_cap = 0 Then
                Response.Write("<script>alert('Debe Seleccionar una Categoría')</script>")
                Return
            End If

            If codigo_cat = 0 Then
                Response.Write("<script>alert('Debe Seleccionar una Actividad')</script>")
                Return
            End If

            obj.POA_CategoriaProgProyActividad(codigo_cpa, codigo_cap, codigo_cat, usuario_reg, "INS")

            btnBuscar_Click(sender, e)
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub btnBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBuscar.Click
        Try
            Dim obj As New clsPlanOperativoAnual
            Dim dt As New Data.DataTable

            Dim codigo_cpa As Integer = 0
            Dim codigo_cap As String = IIf(ddlCategoriasProgramaProyecto.SelectedValue = 0, "%", ddlCategoriasProgramaProyecto.SelectedValue.ToString)
            Dim codigo_cat As String = IIf(ddlActividades.SelectedValue = 0, "%", ddlActividades.SelectedValue.ToString)
            Dim usuario_reg As Integer = Request.QueryString("id")
            dt = obj.POA_CategoriaProgProyActividad(codigo_cpa, codigo_cap, codigo_cat, usuario_reg, "ALL")
            Me.lblMensajeFormulario.Text = "Se encontraron " & dt.Rows.Count.ToString & " registro(s)."
            If dt.Rows.Count = 0 Then
                '    'Me.lblMensajeFormulario.Text = "No se encontraron registros"
                Me.dgv_Categoria.DataSource = Nothing
            Else
                Me.dgv_Categoria.DataSource = dt
                '    'Me.dgvAsignar.Columns.Item(4).Visible = False 'codigo_pla
            End If
            Me.dgv_Categoria.DataBind()
            dt = Nothing
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub dgv_Categoria_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles dgv_Categoria.RowDeleting
        Try
            Dim obj As New clsPlanOperativoAnual
            
            Dim codigo_cap As String = "0"
            Dim codigo_cat As String = "0"
            Dim usuario_reg As Integer = Request.QueryString("id")
            obj.POA_CategoriaProgProyActividad(dgv_Categoria.DataKeys(e.RowIndex).Value.ToString(), codigo_cap, codigo_cat, usuario_reg, "DEL")

            btnBuscar_Click(sender, e)
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

End Class
