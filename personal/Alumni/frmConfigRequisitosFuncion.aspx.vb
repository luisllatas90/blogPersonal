Imports System.Data

Partial Class frmConfigRequisitosFuncion
    Inherits System.Web.UI.Page

#Region "Variables de clase"
    Dim codigo_tfu, codigo_usu, codigo_test As Integer
    Dim md_Funciones As d_Funciones
    Dim md_CarreraProfesional As d_CarreraProfesional
    Dim md_categoria As d_Categoria
    Dim md_PlanEstudio As d_PlanEstudio
    Dim md_RequisitoEgreso As d_RequisitoEgreso
    Dim me_CarreraProfesional As e_CarreraProfesional
    Dim me_planEstudio As e_PlanEstudio
    Dim me_Categoria As e_Categoria
    Dim me_RequisitoEgreso As e_RequisitoEgreso



    Public Enum MessageType
        success
        [error]
        info
        warning
    End Enum

#End Region

#Region "Eventos"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If (Session("id_per") Is Nothing) Then
                Response.Redirect("../../sinacceso.html")
            End If

            codigo_tfu = Request.QueryString("ctf") '1
            codigo_usu = Request.QueryString("id") 'Session("id_per") -- 684'
            codigo_test = "2" 'Request.QueryString("mod")

            'codigo_tfu = "1"
            'codigo_usu = "684"
            'codigo_test = "2" 'Request.QueryString("mod")

            If IsPostBack = False Then
                Call mt_CargarComboCarrera()
            End If

        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try

    End Sub
    Protected Sub ddlCarrrera_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlCarrrera.SelectedIndexChanged
        Try
            'Call mt_CargarComboPlan()
            mt_CargarPlanCurricular()
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub
    Protected Sub lbBusca_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbBusca.Click
        If Me.ddlCarrrera.SelectedValue = "" Or Me.ddlPlanEst.SelectedValue = "" Then
            Call mt_ShowMessage("Completar los datos", MessageType.success)
        Else
            mt_CargarGrillaRequisitos(Me.ddlPlanEst.SelectedValue)
        End If

    End Sub
    Protected Sub gvRequisitoEgreso_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvRequisitoEgreso.RowCommand
        Dim index, codigo_req, codigo_tip, cantidad As Integer
        Dim requisito, codigoCat As String
        Try
            index = CInt(e.CommandArgument)
            codigo_req = Me.gvRequisitoEgreso.DataKeys(index).Values("codigo_req")
            codigo_tip = Me.gvRequisitoEgreso.DataKeys(index).Values("codigo_tip")
            cantidad = Me.gvRequisitoEgreso.DataKeys(index).Values("cantidad")
            requisito = Me.gvRequisitoEgreso.DataKeys(index).Values("nombre")
            codigoCat = IIf(IsDBNull(Me.gvRequisitoEgreso.DataKeys(index).Values("codigo_cat")), "", Me.gvRequisitoEgreso.DataKeys(index).Values("codigo_cat"))
            mt_CargarComboTipoRequisito()
            Select Case e.CommandName
                Case "Editar"
                    Me.hf_Codigo_req.Value = codigo_req
                    If Not String.IsNullOrEmpty(codigoCat) Then
                        md_RequisitoEgreso = New d_RequisitoEgreso : me_RequisitoEgreso = New e_RequisitoEgreso
                        Dim dt As New Data.DataTable
                        With me_RequisitoEgreso
                            .codigo_req = codigo_req
                        End With
                        dt = md_RequisitoEgreso.ListarRequisitos(me_RequisitoEgreso)
                        If dt.Rows.Count = 0 Then mt_ShowMessage("El registro seleccionado no ha sido encontrado.", MessageType.warning)

                        'Call mt_LimpiarControles()

                        With dt.Rows(0)
                            Me.txtRequisito.Text = .Item("nombre").ToString
                            Me.ddlTipoRequisito.SelectedValue = .Item("codigo_cat")
                            Me.chkPlanEstudio.Checked = .Item("indica_pe")
                        End With
                    Else
                        Me.txtRequisito.Text = requisito
                        'mt_CargarComboTipoRequisito()
                    End If
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "openModal", "openModal();", True)
                    udpModal.Update()
            End Select
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub
    Protected Sub btnGuardar_Click(sender As Object, e As System.EventArgs) Handles btnGuardar.Click
        Try

            If Not mt_ActualizaRequisitoEgrso() Then Exit Sub
            Call mt_CargarGrillaRequisitos(Me.ddlPlanEst.SelectedValue)
            Me.udpListado.Update()
            'If Me.ddlTipoRequisito.SelectedValue = "" Then
            '    ' udpScripts.Update()
            '    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "validaModal", "validaModal('DEBE SELECCIONAR UN TIPO DE REQUISITO');", True)
            '    Me.ddlTipoRequisito.Focus()
            '    udpModal.Update()
            'Else
            '    md_RequisitoEgreso = New d_RequisitoEgreso : me_RequisitoEgreso = New e_RequisitoEgreso
            '    Dim dt As New Data.DataTable
            '    With me_RequisitoEgreso
            '        .codigo_req = Me.hf_Codigo_req.Value
            '        .codigo_cat = Me.ddlTipoRequisito.SelectedValue
            '        .indica_pe = Me.chkPlanEstudio.Checked
            '    End With
            '    dt = md_RequisitoEgreso.UpdateRequisitoEgreso(me_RequisitoEgreso)

            '    Call mt_ShowMessage("¡Se actualizó el requisito con éxito!", MessageType.success)
            'End If

        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub
    Protected Sub btnRetornar_Click(sender As Object, e As System.EventArgs) Handles btnRetornar.Click
        Try
            mt_LimpiarControlesModal()
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "closeModal", "closeModal();", True)
            udpListado.Update()
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub
#End Region

#Region "Metodos"
    Private Sub mt_CargarComboCarrera()
        Try
            md_Funciones = New d_Funciones : md_CarreraProfesional = New d_CarreraProfesional : me_CarreraProfesional = New e_CarreraProfesional
            Dim dtCarreraProfesional As New Data.DataTable
            me_CarreraProfesional.codigo_per = codigo_usu
            me_CarreraProfesional.codigo_tfu = codigo_tfu
            me_CarreraProfesional.codigo_test = codigo_test
            dtCarreraProfesional = md_CarreraProfesional.ListarCarreraProfesionalByAcceso(me_CarreraProfesional)
            Call md_Funciones.CargarCombo(Me.ddlCarrrera, dtCarreraProfesional, "codigo_cpf", "nombre_cpf", True, "[-- SELECCIONE --]", "")
            dtCarreraProfesional.Dispose()

        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub
    Protected Sub mt_ShowMessage(ByVal Message As String, ByVal type As MessageType)
        Try
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "showMessage", "showMessage('" & Message & "','" & type.ToString & "');", True)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub mt_CargarComboPlan()
        Try
            md_Funciones = New d_Funciones : md_PlanEstudio = New d_PlanEstudio : me_planEstudio = New e_PlanEstudio
            Dim dtPlanEstudio As New Data.DataTable
            me_planEstudio.codigo_cpf = Me.ddlCarrrera.SelectedValue
            'Me.txtPrueba.Text = me_planEstudio.codigo_cpf
            dtPlanEstudio = md_PlanEstudio.ListarPlanEstudioByCarrera(me_planEstudio)
            Call md_Funciones.CargarCombo(Me.ddlPlanEst, dtPlanEstudio, "codigo_Pes", "descripcion_Pes", True, "[-- SELECCIONE --]", "")
            Me.udpListado.Update()
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub
    Private Sub mt_CargarGrillaRequisitos(ByVal codigo_pec As Integer)
        'Me.txtPlan.Text = codigo_pec
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
        Try
            obj.AbrirConexion()
            dt = obj.TraerDataTable("ALUMNI_RequisitoEgresoListar", "RE", -1, -1, codigo_pec)
            obj.CerrarConexion()
            Me.gvRequisitoEgreso.DataSource = dt
            Me.gvRequisitoEgreso.DataBind()

        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
        Me.udpListado.Update()
    End Sub
    Private Sub mt_CargarPlanCurricular()
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
        Try
            obj.AbrirConexion()
            dt = obj.TraerDataTable("COM_ListarPlanCurricular", Me.ddlCarrrera.SelectedValue, -1)
            obj.CerrarConexion()
            mt_CargarCombo(Me.ddlPlanEst, dt, "codigo_pcur", "nombre_pcur")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub mt_CargarCombo(ByVal cbo As DropDownList, ByVal dt As Data.DataTable, ByVal datavalue As String, ByVal datatext As String)
        cbo.DataSource = dt
        cbo.DataTextField = datatext
        cbo.DataValueField = datavalue
        cbo.DataBind()
    End Sub
    Private Sub mt_CargarComboTipoRequisito()
        Try
            md_Funciones = New d_Funciones : md_categoria = New d_Categoria : me_Categoria = New e_Categoria
            Dim dt As New Data.DataTable
            me_Categoria.operacion = "GEN"
            me_Categoria.grupo_cat = "TIPO_REQUISITO"
            dt = md_categoria.ListarCategoria(me_Categoria)
            Call md_Funciones.CargarCombo(Me.ddlTipoRequisito, dt, "codigo_cat", "nombre_cat", True, "[-- SELECCIONE --]", "")
            dt.Dispose()

        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub
    Private Function mt_ActualizaRequisitoEgrso() As Boolean
        Try
            'udpModal.Update()
            If Not fu_ValidarRegistrarContacto() Then Return False

            md_RequisitoEgreso = New d_RequisitoEgreso : me_RequisitoEgreso = New e_RequisitoEgreso
            Dim dt As New Data.DataTable
            With me_RequisitoEgreso
                .codigo_req = Me.hf_Codigo_req.Value
                .codigo_cat = Me.ddlTipoRequisito.SelectedValue
                .indica_pe = Me.chkPlanEstudio.Checked
            End With
            dt = md_RequisitoEgreso.UpdateRequisitoEgreso(me_RequisitoEgreso)

            Call mt_ShowMessage("¡Se actualizó el requisito con éxito!", MessageType.success)
           
            Return True
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
            Return False
        End Try

    End Function
    Private Function fu_ValidarRegistrarContacto() As Boolean
        Try
            If String.IsNullOrEmpty(Me.ddlTipoRequisito.SelectedValue) Then mt_ShowMessage("Debe seleccionar un tipo de requisito.", MessageType.warning) : Return False
            Return True
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
            Return False
        End Try

       

    End Function
    Private Sub mt_LimpiarControlesModal()
        Me.ddlTipoRequisito.SelectedValue = String.Empty
        Me.chkPlanEstudio.Checked = False
    End Sub
#End Region

#Region "Funciones"

#End Region





    
End Class
