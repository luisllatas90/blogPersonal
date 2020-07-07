
Partial Class GestionCurricular_frmEstrategiasDidactica
    Inherits System.Web.UI.Page

#Region "Declaracion de Variables"

    Dim cod_user As Integer

    Public Enum MessageType
        Success
        [Error]
        Info
        Warning
    End Enum

#End Region

#Region "Eventos"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If (Session("id_per") Is Nothing) Then
                Response.Redirect("../../../sinacceso.html")
            End If
            'cod_user = Request.QueryString("id")
            cod_user = Session("id_per")
            If IsPostBack = False Then
                mt_CargarSemestre()
                Me.btnAgregar.Visible = False

                If Not String.IsNullOrEmpty(Session("gc_codigo_dis")) Then
                    If Me.cboSemestre.Items.Count > 0 Then Me.cboSemestre.SelectedValue = Session("gc_codigo_cac") : mt_CargarCarreraProfesional(Session("gc_codigo_cac"))
                    If Me.cboCarrProf.Items.Count > 0 Then Me.cboCarrProf.SelectedValue = Session("gc_codigo_cpf") : mt_CargarPlanEstudio(Session("gc_codigo_cac"), Session("gc_codigo_cpf"))
                    If Me.cboPlanEstudio.Items.Count > 0 Then Me.cboPlanEstudio.SelectedValue = Session("gc_codigo_pes") : mt_CargarDiseñoAsignatura(Session("gc_codigo_cac"), Session("gc_codigo_cpf"), Session("gc_codigo_pes"))
                    If Me.cboDisApr.Items.Count > 0 Then Me.cboDisApr.SelectedValue = Session("gc_codigo_dis") : mt_CargarDatos(Session("gc_codigo_dis"))
                    Me.cboSemestre.Enabled = False
                    Me.cboCarrProf.Enabled = False
                    Me.cboPlanEstudio.Enabled = False
                    Me.cboDisApr.Enabled = False
                    Me.btnBack.Visible = True
                    Me.btnSeguir.Visible = True
                    Me.btnAgregar.Visible = True
                    Me.lblCurso.InnerText = "Registrar Estrategias Didácticas de Asignatura: " & Session("gc_nombre_cur")
                Else
                    Me.cboSemestre.Enabled = True
                    Me.cboCarrProf.Enabled = True
                    Me.cboPlanEstudio.Enabled = True
                    Me.cboDisApr.Enabled = True
                    Me.btnBack.Visible = False
                    Me.btnSeguir.Visible = False
                End If

            End If
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub cboSemestre_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboSemestre.SelectedIndexChanged
        Try
            mt_CargarCarreraProfesional(IIf(Me.cboSemestre.SelectedValue = -1, 0, Me.cboSemestre.SelectedValue))
            If Me.cboPlanEstudio.Items.Count > 0 Then Me.cboPlanEstudio.SelectedIndex = 0
            If Me.cboDisApr.Items.Count > 0 Then Me.cboDisApr.SelectedIndex = 0
            If Me.gvEstrategia.Rows.Count > 0 Then Me.gvEstrategia.DataSource = Nothing : Me.gvEstrategia.DataBind()
            If cboDisApr.Items.Count > 0 Then Me.btnAgregar.Visible = IIf(Me.cboDisApr.SelectedValue <> -1, True, False)
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub cboCarrProf_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboCarrProf.SelectedIndexChanged
        Try
            mt_CargarPlanEstudio(IIf(Me.cboSemestre.SelectedValue = -1, 0, Me.cboSemestre.SelectedValue), IIf(Me.cboCarrProf.SelectedValue = -1, 0, Me.cboCarrProf.SelectedValue))
            If Me.gvEstrategia.Rows.Count > 0 Then Me.gvEstrategia.DataSource = Nothing : Me.gvEstrategia.DataBind()
            If cboDisApr.Items.Count > 0 Then Me.btnAgregar.Visible = IIf(Me.cboDisApr.SelectedValue <> -1, True, False)
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub cboPlanEstudio_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboPlanEstudio.SelectedIndexChanged
        Try
            mt_CargarDiseñoAsignatura(IIf(Me.cboSemestre.SelectedValue = -1, 0, Me.cboSemestre.SelectedValue), IIf(Me.cboCarrProf.SelectedValue = -1, 0, Me.cboCarrProf.SelectedValue), IIf(Me.cboPlanEstudio.SelectedValue = -1, 0, Me.cboPlanEstudio.SelectedValue))
            If Me.gvEstrategia.Rows.Count > 0 Then Me.gvEstrategia.DataSource = Nothing : Me.gvEstrategia.DataBind()
            If cboDisApr.Items.Count > 0 Then Me.btnAgregar.Visible = IIf(Me.cboDisApr.SelectedValue <> -1, True, False)
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub btnAgregar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAgregar.Click
        Session("gc_codigo_est") = ""
        Page.RegisterStartupScript("Pop", "<script>openModal('" & "Agregar" & "');</script>")
    End Sub

    Protected Sub btnEditar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Page.RegisterStartupScript("Pop", "<script>openModal('" & "Editar" & "');</script>")
    End Sub

    Protected Sub btnGrabar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGrabar.Click
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
        Try
            If Not String.IsNullOrEmpty(Session("gc_codigo_est")) Then
                Dim cod As Integer
                cod = CInt(Session("gc_codigo_est"))
                obj.AbrirConexion()
                obj.Ejecutar("EstrategiaDidactica_actualizar", cod, Me.cboDisApr.SelectedValue, Me.txtNombre.Text, cod_user)
                obj.CerrarConexion()
                mt_ShowMessage("¡ Los datos han sido actualizados correctamente !", MessageType.Success)
            Else
                obj.AbrirConexion()
                obj.Ejecutar("EstrategiaDidactica_insertar", Me.cboDisApr.SelectedValue, Me.txtNombre.Text, cod_user)
                obj.CerrarConexion()
                mt_ShowMessage("¡ Los datos han sido registrados correctamente !", MessageType.Success)
            End If
            mt_CargarDatos(Me.cboDisApr.SelectedValue)
        Catch ex As Exception
            mt_ShowMessage("Error_CargaDatos(): " & ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub gvEstrategia_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvEstrategia.RowCommand
        Dim obj As New ClsConectarDatos
        Dim codigo_est As Integer
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
        Try
            Dim index As Integer
            index = CInt(e.CommandArgument)
            If e.CommandName = "Editar" Then
                Session("gc_codigo_est") = Me.gvEstrategia.DataKeys(index).Values("codigo_est")
                Me.txtNombre.Text = Me.gvEstrategia.DataKeys(index).Values("descripcion_dis")
            End If
            If e.CommandName = "Eliminar" Then
                codigo_est = Me.gvEstrategia.DataKeys(index).Values("codigo_est")
                obj.AbrirConexion()
                obj.Ejecutar("EstrategiaDidactica_eliminar", codigo_est, cod_user)
                obj.CerrarConexion()
                mt_ShowMessage("¡ Se eliminó la estrategia didáctica correctamente !", MessageType.Success)
                mt_CargarDatos(Me.cboDisApr.SelectedValue)
            End If
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub cboDisApr_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboDisApr.SelectedIndexChanged
        Try
            mt_CargarDatos(IIf(Me.cboDisApr.SelectedValue = -1, 0, Me.cboDisApr.SelectedValue))
            If cboDisApr.Items.Count > 0 Then Me.btnAgregar.Visible = IIf(Me.cboDisApr.SelectedValue <> -1, True, False)
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub btnBack_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBack.Click
        Try
            Response.Redirect("~/gestioncurricular/frmCriteriosEvaluacion.aspx")
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub btnSeguir_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSeguir.Click
        Try
            If Me.gvEstrategia.Rows.Count = 0 Then Throw New Exception("¡ Ingrese al menos una Estrategia Didáctica para esta asignatura !")
            Response.Redirect("~/gestioncurricular/frmReferenciaBibliografica.aspx")
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

#End Region

#Region "Metodos"

    Protected Sub mt_ShowMessage(ByVal Message As String, ByVal type As MessageType)
        Page.RegisterStartupScript("Mensaje", "<script>ShowMessage('" & Message & "','" & type.ToString & "');</script>")
    End Sub

    Private Sub mt_CargarCombo(ByVal cbo As DropDownList, ByVal dt As Data.DataTable, ByVal datavalue As String, ByVal datatext As String)
        cbo.DataSource = dt
        cbo.DataTextField = datatext
        cbo.DataValueField = datavalue
        cbo.DataBind()
    End Sub

    Private Sub mt_CargarSemestre()
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
        Try
            obj.AbrirConexion()
            dt = obj.TraerDataTable("ConsultarCicloAcademico", "DA", "")
            obj.CerrarConexion()
            mt_CargarCombo(Me.cboSemestre, dt, "codigo_Cac", "descripcion_Cac")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub mt_CargarDiseñoAsignatura(ByVal codigo_cac As Integer, ByVal codigo_cpf As Integer, ByVal codigo_pes As Integer)
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
        Try
            obj.AbrirConexion()
            dt = obj.TraerDataTable("DiseñoAsignatura_Listar", "DA", -1, codigo_pes, -1, codigo_cac, codigo_cpf)
            obj.CerrarConexion()
            mt_CargarCombo(Me.cboDisApr, dt, "codigo_dis", "nombre_Cur")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub mt_CargarCarreraProfesional(ByVal codigo_cac As Integer)
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
        Try
            obj.AbrirConexion()
            dt = obj.TraerDataTable("DiseñoAsignatura_Listar", "CP", -1, -1, -1, codigo_cac, -1)
            obj.CerrarConexion()
            mt_CargarCombo(Me.cboCarrProf, dt, "codigo_Cpf", "nombre_Cpf")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub mt_CargarPlanEstudio(ByVal codigo_cac As Integer, ByVal codigo_cpf As Integer)
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
        Try
            obj.AbrirConexion()
            dt = obj.TraerDataTable("DiseñoAsignatura_Listar", "PE", -1, -1, -1, codigo_cac, codigo_cpf)
            obj.CerrarConexion()
            mt_CargarCombo(Me.cboPlanEstudio, dt, "codigo_Pes", "descripcion_Pes")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub mt_CargarDatos(ByVal codigo_dis As Integer)
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
        Try
            obj.AbrirConexion()
            dt = obj.TraerDataTable("EstrategiaDidactica_Listar", -1, codigo_dis, "")
            obj.CerrarConexion()
            Me.gvEstrategia.DataSource = dt
            Me.gvEstrategia.DataBind()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

End Class
