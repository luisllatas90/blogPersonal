
Partial Class PlanProyecto_frmRegistroGrupo
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (IsPostBack = False) Then
            Me.btnCancelar.Attributes.Add("onclick", "self.parent.tb_remove();")
            MuestraFiltroBusqueda(False)
            CargaTipoProyecto()

            If (Request.QueryString("pl") IsNot Nothing) Then
                'Buscamos el plan                
                Dim obj As New ClsConectarDatos
                Dim dtPlan As New Data.DataTable
                obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
                obj.AbrirConexion()
                dtPlan = obj.TraerDataTable("PLAN_BuscaProyecto", Request.QueryString("pl"), "", 0)
                obj.CerrarConexion()

                If (dtPlan.Rows.Count > 0) Then
                    Me.HdCodigo_pro.Value = dtPlan.Rows(0).Item("codigo_pro").ToString
                    Me.lblPlan.Text = dtPlan.Rows(0).Item("titulo_pro").ToString

                    If (Request.QueryString("gpr") IsNot Nothing) Then
                        Me.HdAccion.Value = "M"
                        AsignaDatos(Request.QueryString("gpr"))
                    Else
                        Me.HdAccion.Value = "N"
                    End If
                Else
                    Me.lblMensaje.Text = "No se encontro relación con ningun plan"
                End If                
                dtPlan.Dispose()
                obj = Nothing
            Else
                Me.lblMensaje.Text = "No se encontro ningun plan"
            End If
        End If
        If Me.gvDetalle.Rows.Count > 0 Then
            Me.lblIntegrantes.Visible = True
        Else
            Me.lblIntegrantes.Visible = False
        End If
    End Sub

    Private Sub CargaTipoProyecto()
        Dim obj As New ClsConectarDatos
        Dim dtTipo As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        dtTipo = obj.TraerDataTable("PLAN_BuscaTipoProyecto", 0)
        obj.CerrarConexion()

        Me.cboTipo.DataTextField = "descripcion"
        Me.cboTipo.DataValueField = "codigo_tpr"
        Me.cboTipo.DataSource = dtTipo
        Me.cboTipo.DataBind()

        Dim item As New ListItem
        item.Value = 0
        item.Text = "---- NINGUNO ----"
        Me.cboTipo.Items.Add(item)
        Me.cboTipo.SelectedValue = 0

        obj = Nothing
        dtTipo.Dispose()
    End Sub

    Private Sub AsignaDatos(ByVal codigo As String)
        Dim obj As New ClsConectarDatos
        Dim dtPlan As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        dtPlan = obj.TraerDataTable("PLAN_BuscaProyecto", Request.QueryString("pl"), "", 0)
        obj.CerrarConexion()
        If (dtPlan.Rows.Count > 0) Then
            Me.HdCodigo_gpr.Value = dtPlan.Rows(0).Item("codigo_gpr")
            Me.HdCodigo_pro.Value = dtPlan.Rows(0).Item("codigo_pro")
            Me.lblPlan.Text = dtPlan.Rows(0).Item("titulo_pro")
            Me.txtNombre.Text = dtPlan.Rows(0).Item("nombre_gpr")
            Me.txtDescripcion.Text = dtPlan.Rows(0).Item("descripcion_gpr")        

            cargaDetalle(codigo)
        End If
        dtPlan.Dispose()
        obj = Nothing
    End Sub

    Private Sub MuestraFiltroBusqueda(ByVal sw As Boolean)
        Me.txtFiltro.Visible = sw
        Me.lblFiltro.Visible = sw
        Me.btnFiltro.Visible = sw
        Me.gvPersonal.Visible = sw
        Me.btnBuscar.Visible = Not sw
    End Sub

    Private Sub CargaGrid(ByVal strBusqueda As String)
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        Dim dtPersonal As New Data.DataTable
        obj.AbrirConexion()
        dtPersonal = obj.TraerDataTable("PLAN_BuscaPersonal", Me.txtFiltro.Text.Trim, Me.txtFiltro.Text.Replace(" ", "%"))
        obj.CerrarConexion()
        Me.gvPersonal.DataSource = dtPersonal
        Me.gvPersonal.DataBind()
        dtPersonal.Dispose()
        obj = Nothing
    End Sub

    Private Sub cargaDetalle(ByVal codigo_gpr As Integer)
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        Dim dtDetalle As New Data.DataTable
        obj.AbrirConexion()
        dtDetalle = obj.TraerDataTable("PLAN_BuscaGrupoDetalle", 0, 0, codigo_gpr)
        obj.CerrarConexion()
        Me.gvDetalle.DataSource = dtDetalle
        Me.gvDetalle.DataBind()
        dtDetalle.Dispose()
        obj = Nothing
    End Sub

    Protected Sub btnBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBuscar.Click
        Me.txtFiltro.Text = Me.txtPersonal.Text
        MuestraFiltroBusqueda(True)
    End Sub

    Protected Sub btnFiltro_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFiltro.Click
        If (Me.txtFiltro.Text.Trim <> "") Then
            CargaGrid(Me.txtFiltro.Text)
        Else
            Me.lblMensaje.Text = "Debe ingresar un fitro de busqueda"
            Me.txtFiltro.Focus()
        End If
    End Sub

    Protected Sub gvPersonal_SelectedIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSelectEventArgs) Handles gvPersonal.SelectedIndexChanging
        Me.HdCodigo_per.Value = gvPersonal.DataKeys(e.NewSelectedIndex).Values(0)
        Me.txtPersonal.Text = gvPersonal.Rows(e.NewSelectedIndex).Cells(1).Text
        MuestraFiltroBusqueda(False)
        Me.gvPersonal.DataSource = Nothing
        Me.gvPersonal.DataBind()
        MuestraFiltroBusqueda(True)
    End Sub

    Protected Sub gvPersonal_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gvPersonal.PageIndexChanging
        gvPersonal.PageIndex = e.NewPageIndex()
        CargaGrid(Me.txtFiltro.Text)
    End Sub

    Protected Sub btnAddDetalle_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddDetalle.Click
        Try
            If (ValidaFormulario("btnDetalle") = True) Then
                Dim obj As New ClsConectarDatos
                obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString

                If (Me.HdCodigo_gpr.Value = "") Then
                    Dim dtCodigo As New Data.DataTable
                    'Inserta Cabecera
                    obj.AbrirConexion()

                    If Me.cboTipo.SelectedValue <> 0 Then
                        dtCodigo = obj.TraerDataTable("PLAN_RegistraGrupoProyecto", 0, DBNull.Value, _
                            Me.txtNombre.Text, Me.txtDescripcion.Text, "01/01/" & Date.Today.Year, _
                            "30/12/" & Date.Today.Year, "A", "1", Session("id_per"), Me.cboTipo.SelectedValue)
                    Else
                        dtCodigo = obj.TraerDataTable("PLAN_RegistraGrupoProyecto", 0, Me.HdCodigo_pro.Value, _
                            Me.txtNombre.Text, Me.txtDescripcion.Text, "01/01/" & Date.Today.Year, _
                            "30/12/" & Date.Today.Year, "A", "1", Session("id_per"), DBNull.Value)
                    End If


                    obj.CerrarConexion()

                    Me.HdCodigo_gpr.Value = dtCodigo.Rows(0).Item(0).ToString
                End If

                'Inserta Detalle
                obj.AbrirConexion()
                obj.Ejecutar("PLAN_RegistrarGrupoDetalle", Me.HdCodigo_per.Value, Me.HdCodigo_gpr.Value)
                obj.CerrarConexion()

                obj = Nothing
                cargaDetalle(Me.HdCodigo_gpr.Value)

                Me.txtPersonal.Text = ""
                Me.HdCodigo_per.Value = ""
                MuestraFiltroBusqueda(False)
            End If
        Catch ex As Exception
            Me.lblMensaje.Text = "Error al registrar a los integrantes"
        End Try
    End Sub

    Protected Sub btnAgregar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAgregar.Click        
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        Try
            If (ValidaFormulario("btnAceptar") = True) Then
                obj.AbrirConexion()
                If (Me.HdCodigo_gpr.Value = "") Then
                    Me.HdCodigo_gpr.Value = "0"
                End If

                If Me.cboTipo.SelectedValue <> 0 Then
                    obj.Ejecutar("PLAN_RegistraGrupoProyecto", Me.HdCodigo_gpr.Value, DBNull.Value, _
                                Me.txtNombre.Text, Me.txtDescripcion.Text, "01/01/" & Date.Today.Year, _
                                "30/12/" & Date.Today.Year, "A", "1", Session("id_per"), Me.cboTipo.SelectedValue)
                Else
                    obj.Ejecutar("PLAN_RegistraGrupoProyecto", Me.HdCodigo_gpr.Value, Me.HdCodigo_pro.Value, _
                                Me.txtNombre.Text, Me.txtDescripcion.Text, "01/01/" & Date.Today.Year, _
                                "30/12/" & Date.Today.Year, "A", "1", Session("id_per"), DBNull.Value)
                End If

                obj.CerrarConexion()
                Me.lblMensaje.Text = "Grupo Guardado."
                LimpiaControles()
            End If
            obj = Nothing
        Catch ex As Exception
            Me.lblMensaje.Text = "Error al registrar el grupo."
        End Try
    End Sub

    Private Function ValidaFormulario(ByVal EnviadoDe As String) As Boolean
        If (Me.txtNombre.Text.Trim = "") Then
            Me.lblMensaje.Text = "Debe ingresar el nombre del grupo"
            Me.txtNombre.Focus()
            Return False
        End If

        If (Me.txtDescripcion.Text.Trim = "") Then
            Me.lblMensaje.Text = "Debe ingresar una descripcion del grupo"
            Me.txtDescripcion.Focus()
            Return False
        End If

        If (Me.HdCodigo_pro.Value = "") Then
            Me.lblMensaje.Text = "No se ha relacionado con ningún plan"
            Return False
        End If

        If (EnviadoDe <> "btnDetalle") Then
            If (Me.gvDetalle.Rows.Count = 0) Then
                Me.lblMensaje.Text = "Debe ingresar algún integrante al grupo"
                Me.txtPersonal.Text = ""
                Me.txtPersonal.Focus()
                Return False
            End If
        End If

        Me.lblMensaje.Text = ""
        Return True
    End Function

    Protected Sub btnCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        Try            
            If (Me.HdAccion.Value = "N") Then
                'Elimina desde cabecera
                If (Me.HdCodigo_gpr.Value <> "") Then
                    Dim obj As New ClsConectarDatos
                    obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
                    obj.AbrirConexion()
                    obj.Ejecutar("PLAN_EliminaGrupoProyecto", Me.HdCodigo_gpr.Value, 0)
                    obj.CerrarConexion()
                    obj = Nothing
                End If            
            End If

            LimpiaControles()
            Me.lblMensaje.Text = ""
            Response.Redirect("frmListaGrupo.aspx")
        Catch ex As Exception
            Me.lblMensaje.Text = "Error al limpiar formulario"
        End Try
    End Sub

    Private Sub LimpiaControles()
        Me.txtDescripcion.Text = ""
        Me.txtNombre.Text = ""        
        Me.HdCodigo_gpr.Value = ""
        Me.txtPersonal.Text = ""
        Me.HdCodigo_per.Value = ""
        Me.cboTipo.SelectedValue = 0
        Me.gvDetalle.DataSource = Nothing
        Me.gvDetalle.DataBind()
        MuestraFiltroBusqueda(False)
    End Sub

    Protected Sub gvDetalle_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles gvDetalle.RowDeleting        
        Try
            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            obj.Ejecutar("PLAN_EliminaGrupoDetalle", Me.gvDetalle.DataKeys.Item(e.RowIndex).Values(0))
            obj.CerrarConexion()
            obj = Nothing
            cargaDetalle(Me.HdCodigo_gpr.Value)
        Catch ex As Exception
            Me.lblMensaje.Text = "Error al eliminar al personal"
        End Try                
    End Sub
End Class
