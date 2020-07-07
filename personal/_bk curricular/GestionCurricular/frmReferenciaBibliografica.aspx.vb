﻿
Partial Class GestionCurricular_frmReferenciaBibliografica
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
                mt_CargarTipos()
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
                    Me.btnAgregar.Visible = True
                    Me.btnSeguir.Visible = True
                    Me.lblCurso.InnerText = "Registrar Referencias Bibliográficas de Asignatura: " & Session("gc_nombre_cur")
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
            If Me.gvReferencia.Rows.Count > 0 Then Me.gvReferencia.DataSource = Nothing : Me.gvReferencia.DataBind()
            If cboDisApr.Items.Count > 0 Then Me.btnAgregar.Visible = IIf(Me.cboDisApr.SelectedValue <> -1, True, False)
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub cboCarrProf_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboCarrProf.SelectedIndexChanged
        Try
            mt_CargarPlanEstudio(IIf(Me.cboSemestre.SelectedValue = -1, 0, Me.cboSemestre.SelectedValue), IIf(Me.cboCarrProf.SelectedValue = -1, 0, Me.cboCarrProf.SelectedValue))
            If Me.gvReferencia.Rows.Count > 0 Then Me.gvReferencia.DataSource = Nothing : Me.gvReferencia.DataBind()
            If cboDisApr.Items.Count > 0 Then Me.btnAgregar.Visible = IIf(Me.cboDisApr.SelectedValue <> -1, True, False)
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub cboPlanEstudio_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboPlanEstudio.SelectedIndexChanged
        Try
            mt_CargarDiseñoAsignatura(IIf(Me.cboSemestre.SelectedValue = -1, 0, Me.cboSemestre.SelectedValue), IIf(Me.cboCarrProf.SelectedValue = -1, 0, Me.cboCarrProf.SelectedValue), IIf(Me.cboPlanEstudio.SelectedValue = -1, 0, Me.cboPlanEstudio.SelectedValue))
            If Me.gvReferencia.Rows.Count > 0 Then Me.gvReferencia.DataSource = Nothing : Me.gvReferencia.DataBind()
            If cboDisApr.Items.Count > 0 Then Me.btnAgregar.Visible = IIf(Me.cboDisApr.SelectedValue <> -1, True, False)
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

    Protected Sub btnEditar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Page.RegisterStartupScript("Pop", "<script>openModal('" & "Editar" & "');</script>")
    End Sub

    Protected Sub btnAgregar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAgregar.Click
        Session("gc_codigo_ref") = ""
        Page.RegisterStartupScript("Pop", "<script>openModal('" & "Agregar" & "');</script>")
    End Sub

    Protected Sub gvReferencia_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvReferencia.RowCommand
        Dim obj As New ClsConectarDatos
        Dim codigo_ref As Integer
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
        Try
            Dim index As Integer
            index = CInt(e.CommandArgument)
            If e.CommandName = "Editar" Then
                Session("gc_codigo_ref") = Me.gvReferencia.DataKeys(index).Values("codigo_ref")
                Me.txtNombre.Text = Me.gvReferencia.DataKeys(index).Values("descripcion_ref")
                Me.cboTipo.SelectedValue = Me.gvReferencia.DataKeys(index).Values("codigo_tip")
                Me.chkTipo.Checked = IIf(Me.gvReferencia.DataKeys(index).Values("tipo_ref") = 1, True, False)
                Me.txtObservacion.Text = Me.gvReferencia.DataKeys(index).Values("observacion_ref")
            End If
            If e.CommandName = "Eliminar" Then
                codigo_ref = Me.gvReferencia.DataKeys(index).Values("codigo_ref")
                obj.AbrirConexion()
                obj.Ejecutar("ReferenciaBibliografica_eliminar", codigo_ref, cod_user)
                obj.CerrarConexion()
                mt_ShowMessage("¡ Se eliminó la estrategia didáctica correctamente !", MessageType.Success)
                mt_CargarDatos(Me.cboDisApr.SelectedValue)
            End If
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub btnGrabar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGrabar.Click
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
        Try
            If Not String.IsNullOrEmpty(Session("gc_codigo_ref")) Then
                Dim cod As Integer
                cod = CInt(Session("gc_codigo_ref"))
                obj.AbrirConexion()
                obj.Ejecutar("ReferenciaBibliografica_actualizar", cod, Me.cboDisApr.SelectedValue, Me.cboTipo.SelectedValue, Me.txtNombre.Text, IIf(Me.chkTipo.Checked, 1, 0), Me.txtObservacion.Text, cod_user)
                obj.CerrarConexion()
                mt_ShowMessage("¡ Los datos han sido actualizados correctamente !", MessageType.Success)
            Else
                obj.AbrirConexion()
                obj.Ejecutar("ReferenciaBibliografica_insertar", Me.cboDisApr.SelectedValue, Me.cboTipo.SelectedValue, Me.txtNombre.Text, IIf(Me.chkTipo.Checked, 1, 0), Me.txtObservacion.Text, cod_user)
                obj.CerrarConexion()
                mt_ShowMessage("¡ Los datos han sido registrados correctamente !", MessageType.Success)
            End If
            mt_CargarDatos(Me.cboDisApr.SelectedValue)
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub btnBack_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBack.Click
        Try
            Response.Redirect("~/gestioncurricular/frmEstrategiasDidactica.aspx")
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub btnSeguir_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSeguir.Click
        Dim i, cont_usat, cont_comp, codigo_tip As Integer
        Dim nombre_ref As String
        Try
            If Me.gvReferencia.Rows.Count = 0 Then Throw New Exception("¡ Ingrese al menos una Referencia Bibliográfica para esta asignatura !")
            cont_usat = 0 : cont_comp = 0
            For i = 0 To Me.gvReferencia.Rows.Count - 1
                codigo_tip = CInt(Me.gvReferencia.DataKeys(i).Values("codigo_tip"))
                nombre_ref = Me.gvReferencia.DataKeys(i).Values("nombre")
                If nombre_ref = "USAT" Then cont_usat += 1
                If nombre_ref = "Complementarias" Then cont_comp += 1
            Next
            If cont_usat = 0 Then Throw New Exception("¡ Ingrese al menos una Referencia Bibliográfica USAT !")
            If cont_comp = 0 Then Throw New Exception("¡ Ingrese al menos una Referencia Bibliográfica Complementarias !")
            Response.Redirect("~/gestioncurricular/FrmContenidosAsignatura.aspx")
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

    Private Sub mt_CargarTipos()
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
        Try
            obj.AbrirConexion()
            dt = obj.TraerDataTable("TipoReferencia_Listar", -1, "")
            obj.CerrarConexion()
            mt_CargarCombo(Me.cboTipo, dt, "codigo_tip", "nombre")
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
            dt = obj.TraerDataTable("ReferenciaBibliografica_Listar", -1, codigo_dis, -1, "")
            obj.CerrarConexion()
            Me.gvReferencia.DataSource = dt
            Me.gvReferencia.DataBind()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

End Class
