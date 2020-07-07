Imports System.Collections.Generic
Imports System.IO

Partial Class GestionCurricular_FrmPerfilesPlan
    Inherits System.Web.UI.Page

#Region "Declaracion de Variables"

    Dim cod_user As Integer '= 1971
    Dim cod_ctf As Integer

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
                Response.Redirect("../../sinacceso.html")
            End If

            cod_user = Session("id_per")
            cod_ctf = Request.QueryString("ctf")

            If IsPostBack = False Then
                Call mt_CargarCarreraProfesional()
                Call mt_CargarCategoria()
            End If

        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub btnAgregarIng_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAgregarIng.Click
        Try
            Me.hdCodigoIng.Value = ""

            If Me.ddlPlanCurricular.SelectedIndex < 0 Then
                Call mt_ShowMessage("Seleccione primero el Plan Curricular", MessageType.Info)

                Me.ddlPlanCurricular.Focus()
            Else
                Call mt_LimpiarFormulario()
                Me.ddlCategoriaIng_SelectedIndexChanged(sender, e)

                Page.RegisterStartupScript("Pop", "<script>openModal('nuevo', '', 'ingreso', '');</script>")
            End If
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub btnAceptarIng_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAceptarIng.Click
        Dim obj As New ClsConectarDatos
        Dim codigo_pIng As Integer = 0
        Dim dt As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString

        Try
            If Not (IsDBNull(Session("perlogin"))) And Session("perlogin").ToString <> "" Then
                obj.IniciarTransaccion()

                If Me.ddlCategoriaIng.SelectedValue.Equals("1") Then
                    If ddlCompetenciaIng.Visible Then
                        Me.txtDescripcionIng.Text = Me.ddlCompetenciaIng.SelectedItem.ToString()
                    Else
                        Me.txtDescripcionIng.Text = Me.txtCompetenciaIng.Text
                    End If
                End If

                Dim codigo_com As String = ""
                If Not Me.ddlCategoriaIng.SelectedValue.Equals("1") Or Me.chkCompetenciaIng.Checked Then
                    Dim descripcion As String
                    descripcion = IIf(Me.chkCompetenciaIng.Checked, Me.txtCompetenciaIng.Text, Me.txtDescripcionIng.Text)

                    dt = obj.TraerDataTable("COM_RegistrarCompetenciaAprendizaje", descripcion, 1, Me.ddlCategoriaIng.SelectedValue, cod_user)

                    If dt.Rows.Count > 0 Then
                        codigo_com = dt.Rows(0).Item(0).ToString()
                    Else
                        Throw New Exception("No se pudo Registrar el Tipo de Competencia")
                        Return
                    End If
                Else
                    codigo_com = Me.ddlCompetenciaIng.SelectedValue
                End If

                If String.IsNullOrEmpty(Me.hdCodigoIng.Value) Then
                    dt = obj.TraerDataTable("COM_RegistrarPerfilIngreso", Me.txtDescripcionIng.Text, codigo_com, Me.ddlPlanCurricular.SelectedValue, cod_user)

                    If dt.Rows.Count > 0 Then
                        Dim msj As String = dt.Rows(0).Item(1).ToString

                        If CInt(dt.Rows(0).Item(0).ToString) <> 0 Then
                            Call mt_ShowMessage("Perfil de Ingreso registrado con éxito", MessageType.Success)
                        Else
                            Throw New Exception(msj)
                            Return
                        End If
                    Else
                        Throw New Exception("No se pudo Registrar el Tipo de Competencia")
                        Return
                    End If
                Else
                    codigo_pIng = CInt(Me.hdCodigoIng.Value)

                    obj.Ejecutar("COM_ActualizarPerfilIngreso", codigo_pIng, Me.txtDescripcionIng.Text, codigo_com, Me.ddlPlanCurricular.SelectedValue, cod_user)

                    Call mt_ShowMessage("Perfil de Ingreso actualizado con éxito", MessageType.Success)
                End If

                obj.TerminarTransaccion()

                chkCompetenciaIng.Checked = False
                Call chkCompetenciaIng_CheckedChanged(sender, e)
                Call mt_CargarDatosIng(Me.ddlPlanCurricular.SelectedValue)
            Else
                Throw New Exception("Inicie Sesión")
            End If
        Catch ex As Exception
            obj.AbortarTransaccion()
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub btnAgregarEgr_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAgregarEgr.Click
        Try
            Me.hdCodigoEgr.Value = ""

            If Me.ddlPlanCurricular.SelectedIndex < 0 Then
                Call mt_ShowMessage("Seleccione primero el Plan Curricular", MessageType.Info)

                Me.ddlPlanCurricular.Focus()
            Else
                Call mt_LimpiarFormulario()
                Me.ddlCategoriaEgr_SelectedIndexChanged(sender, e)

                Page.RegisterStartupScript("Pop", "<script>openModal('nuevo', '', 'egreso', '');</script>")
            End If
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub btnAceptarEgr_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAceptarEgr.Click
        Dim obj As New ClsConectarDatos
        Dim codigo_pEgr As Integer = 0
        Dim dt As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString

        Try
            If Not (IsDBNull(Session("perlogin"))) And Session("perlogin").ToString <> "" Then
                obj.IniciarTransaccion()

                If Me.ddlCategoriaEgr.SelectedValue.Equals("1") Then
                    If ddlCompetenciaEgr.Visible Then
                        Me.txtDescripcionEgr.Text = Me.ddlCompetenciaEgr.SelectedItem.ToString()
                    Else
                        Me.txtDescripcionEgr.Text = Me.txtCompetenciaEgr.Text
                    End If
                End If

                Dim codigo_com As String = ""
                If Not Me.ddlCategoriaEgr.SelectedValue.Equals("1") Or Me.chkCompetenciaEgr.Checked Then
                    Dim descripcion As String
                    descripcion = IIf(Me.chkCompetenciaEgr.Checked, Me.txtCompetenciaEgr.Text, Me.txtDescripcionEgr.Text)

                    dt = obj.TraerDataTable("COM_RegistrarCompetenciaAprendizaje", descripcion, 2, Me.ddlCategoriaEgr.SelectedValue, cod_user)

                    If dt.Rows.Count > 0 Then
                        codigo_com = dt.Rows(0).Item(0).ToString()
                    Else
                        Throw New Exception("No se pudo Registrar el Tipo de Competencia")
                        Return
                    End If
                Else
                    codigo_com = Me.ddlCompetenciaEgr.SelectedValue
                End If

                If String.IsNullOrEmpty(Me.hdCodigoEgr.Value) Then
                    dt = obj.TraerDataTable("COM_RegistrarPerfilEgreso", Me.txtDescripcionEgr.Text, codigo_com, Me.ddlPlanCurricular.SelectedValue, cod_user)

                    If dt.Rows.Count > 0 Then
                        Dim msj As String = dt.Rows(0).Item(1).ToString

                        If CInt(dt.Rows(0).Item(0).ToString) <> 0 Then
                            Call mt_ShowMessage("Perfil de Ingreso registrado con éxito", MessageType.Success)
                        Else
                            Throw New Exception(msj)
                            Return
                        End If
                    Else
                        Throw New Exception("No se pudo Registrar el Tipo de Competencia")
                        Return
                    End If
                Else
                    codigo_pEgr = CInt(Me.hdCodigoEgr.Value)

                    obj.Ejecutar("COM_ActualizarPerfilEgreso", codigo_pEgr, Me.txtDescripcionEgr.Text, codigo_com, Me.ddlPlanCurricular.SelectedValue, cod_user)

                    Call mt_ShowMessage("Perfil de Ingreso actualizado con éxito", MessageType.Success)
                End If

                obj.TerminarTransaccion()

                chkCompetenciaEgr.Checked = False
                Call chkCompetenciaEgr_CheckedChanged(sender, e)
                Call mt_CargarDatosEgr(Me.ddlPlanCurricular.SelectedValue)
            Else
                Throw New Exception("Inicie Sesión")
            End If
        Catch ex As Exception
            obj.AbortarTransaccion()
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub ddlCarreraProf_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlCarreraProf.SelectedIndexChanged
        Try
            If Me.ddlCarreraProf.SelectedIndex = 0 Or Me.ddlCarreraProf.SelectedValue = -2 Then
                mt_ShowMessage("¡ Seleccione una Carrera Profesional !", MessageType.Warning)
                Call mt_CargarPlanCurricular(0)
                Call mt_CargarDatosIng("")
                Call mt_CargarDatosEgr("")
                Exit Sub
            End If
            Call mt_CargarPlanCurricular(Me.ddlCarreraProf.SelectedValue)
            Call mt_CargarDatosIng(Me.ddlPlanCurricular.SelectedValue)
            Call mt_CargarDatosEgr(Me.ddlPlanCurricular.SelectedValue)
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub ddlPlanCurricular_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPlanCurricular.SelectedIndexChanged
        Try
            Call mt_CargarDatosIng(Me.ddlPlanCurricular.SelectedValue)
            Call mt_CargarDatosEgr(Me.ddlPlanCurricular.SelectedValue)
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub ddlCategoriaIng_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlCategoriaIng.SelectedIndexChanged
        Call mt_CargarCompetenciaIng()

        If ddlCategoriaIng.SelectedValue.Equals("1") Then
            lblCompetenciaIng.Visible = True
            ddlCompetenciaIng.Visible = True
            ddlCompetenciaIng.Enabled = True
            chkCompetenciaIng.Visible = True
            txtDescripcionIng.Visible = False
            lblDescripcionIng.Visible = False
        Else
            lblCompetenciaIng.Visible = False
            chkCompetenciaIng.Visible = False
            chkCompetenciaIng.Checked = False
            txtCompetenciaIng.Visible = False
            ddlCompetenciaIng.Visible = False
            ddlCompetenciaIng.Enabled = False
            txtDescripcionIng.Visible = True
            lblDescripcionIng.Visible = True
            ddlCompetenciaIng.Focus()
        End If

        Me.panModalIng.Update()
    End Sub

    Protected Sub ddlCategoriaEgr_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlCategoriaEgr.SelectedIndexChanged
        Call mt_CargarCompetenciaEgr()

        If ddlCategoriaEgr.SelectedValue.Equals("1") Then
            lblCompetenciaEgr.Visible = True
            ddlCompetenciaEgr.Visible = True
            ddlCompetenciaEgr.Enabled = True
            chkCompetenciaEgr.Visible = True
            txtDescripcionEgr.Visible = False
            lblDescripcionEgr.Visible = False
        Else
            lblCompetenciaEgr.Visible = False
            chkCompetenciaEgr.Visible = False
            chkCompetenciaEgr.Checked = False
            txtCompetenciaEgr.Visible = False
            ddlCompetenciaEgr.Visible = False
            ddlCompetenciaEgr.Enabled = False
            txtDescripcionEgr.Visible = True
            lblDescripcionEgr.Visible = True
            ddlCompetenciaEgr.Focus()
        End If

        Me.panModalEgr.Update()
    End Sub

    Protected Sub chkCompetenciaIng_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkCompetenciaIng.CheckedChanged
        Me.lblCompetenciaIng.InnerText = "Competencia:"
        Me.ddlCompetenciaIng.Visible = True
        Me.ddlCompetenciaIng.Enabled = True
        Me.txtCompetenciaIng.Visible = False

        If Me.chkCompetenciaIng.Checked Then
            Me.lblCompetenciaIng.InnerText = "Nueva Competencia:"
            Me.ddlCompetenciaIng.Visible = False
            Me.ddlCompetenciaIng.Enabled = False
            Me.txtCompetenciaIng.Visible = True

            Me.ddlCompetenciaIng.SelectedIndex = -1
            Me.txtCompetenciaIng.Focus()
        End If

        Me.panModalIng.Update()
        ScriptManager.GetCurrent(Me.Page).SetFocus(Me.txtCompetenciaIng)
    End Sub

    Protected Sub chkCompetenciaEgr_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkCompetenciaEgr.CheckedChanged
        Me.lblCompetenciaEgr.InnerText = "Competencia:"
        Me.ddlCompetenciaEgr.Visible = True
        Me.ddlCompetenciaEgr.Enabled = True
        Me.txtCompetenciaEgr.Visible = False

        If Me.chkCompetenciaEgr.Checked Then
            Me.lblCompetenciaEgr.InnerText = "Nueva Competencia:"
            Me.ddlCompetenciaEgr.Visible = False
            Me.ddlCompetenciaEgr.Enabled = False
            Me.txtCompetenciaEgr.Visible = True

            Me.ddlCompetenciaEgr.SelectedIndex = -1
            Me.txtCompetenciaEgr.Focus()
        End If

        Me.panModalEgr.Update()
        ScriptManager.GetCurrent(Me.Page).SetFocus(Me.txtCompetenciaEgr)
    End Sub

    Protected Sub gvIngreso_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvIngreso.RowCommand
        Try
            If IsDBNull(Session("perlogin")) Or Session("perlogin").ToString = "" Then
                Throw New Exception("Inicie Sesión")
                Return
            End If

            If Me.gvIngreso.Rows.Count = 0 Then
                Call mt_ShowMessage("No existe Perfil para editar", MessageType.Info)
                Session("codigo_ing") = ""
                Me.hdCodigoIng.Value = ""

                Me.ddlPlanCurricular.Focus()
            Else
                Dim obj As New ClsConectarDatos
                Dim com, cat As String
                Dim index As Integer = CInt(e.CommandArgument)

                obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
                Session("codigo_ing") = Me.gvIngreso.DataKeys(index).Values("codigo_pIng")
                Me.hdCodigoIng.Value = Me.gvIngreso.DataKeys(index).Values("codigo_pIng")
                
                Select Case e.CommandName
                    Case "Editar"
                        com = Me.gvIngreso.DataKeys(index).Values("codigo_com")
                        cat = Me.gvIngreso.DataKeys(index).Values("codigo_cat")

                        Me.txtDescripcionIng.Text = Me.gvIngreso.DataKeys(index).Values("descripcion_pIng")
                        Me.ddlCategoriaIng.SelectedValue = Me.gvIngreso.DataKeys(index).Values("codigo_cat")
                        Me.ddlCategoriaIng_SelectedIndexChanged(sender, Nothing)
                        'Me.ddlCompetenciaIng.Text = Me.txtDescripcionIng.Text

                        Page.RegisterStartupScript("Pop", "<script>openModal('editar', '" & com & "', 'ingreso', '" & cat & "');</script>")
                    Case "Eliminar"
                        Dim dt As New Data.DataTable("data")
                        obj.AbrirConexion()
                        dt = obj.TraerDataTable("COM_CambiarEstadoPerfilIngreso", Session("codigo_ing"), 0)
                        obj.CerrarConexion()

                        If dt.Rows.Count > 0 Then
                            If dt.Rows(0).Item(0).ToString().Equals("1") Then
                                Call mt_ShowMessage(dt.Rows(0).Item(1).ToString(), MessageType.Success)
                                Call mt_CargarDatosIng(Me.ddlPlanCurricular.SelectedValue)
                            Else
                                Call mt_ShowMessage(dt.Rows(0).Item(1).ToString(), MessageType.Warning)
                            End If
                        End If

                        dt.Dispose()
                End Select
            End If
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub gvIngreso_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles gvIngreso.RowEditing

    End Sub

    Protected Sub gvIngreso_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles gvIngreso.RowDeleting

    End Sub

    Protected Sub gvEgreso_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvEgreso.RowCommand
        Try
            If IsDBNull(Session("perlogin")) Or Session("perlogin").ToString = "" Then
                Throw New Exception("Inicie Sesión")
                Return
            End If

            If Me.gvEgreso.Rows.Count = 0 Then
                Call mt_ShowMessage("No existe Perfil para editar", MessageType.Info)
                Session("codigo_egr") = ""
                Me.hdCodigoEgr.Value = ""

                Me.ddlPlanCurricular.Focus()
            Else
                Dim obj As New ClsConectarDatos
                Dim com, cat As String
                Dim index As Integer = CInt(e.CommandArgument)

                obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
                Session("codigo_egr") = Me.gvEgreso.DataKeys(index).Values("codigo_pEgr")
                Me.hdCodigoEgr.Value = Me.gvEgreso.DataKeys(index).Values("codigo_pEgr")

                Select Case e.CommandName
                    Case "Editar"
                        com = Me.gvEgreso.DataKeys(index).Values("codigo_com")
                        cat = Me.gvEgreso.DataKeys(index).Values("codigo_cat")

                        Me.txtDescripcionEgr.Text = Me.gvEgreso.DataKeys(index).Values("descripcion_pEgr")
                        Me.ddlCategoriaEgr.SelectedValue = Me.gvEgreso.DataKeys(index).Values("codigo_cat")
                        Me.ddlCategoriaEgr_SelectedIndexChanged(sender, Nothing)
                        'Me.ddlCompetenciaEgr.Text = Me.txtDescripcionEgr.Text

                        Page.RegisterStartupScript("Pop", "<script>openModal('editar', '" & com & "', 'egreso', '" & cat & "');</script>")
                    Case "Eliminar"
                        Dim dt As New Data.DataTable("data")
                        obj.AbrirConexion()
                        dt = obj.TraerDataTable("COM_CambiarEstadoPerfilEgreso", Session("codigo_egr"), 0)
                        obj.CerrarConexion()

                        If dt.Rows.Count > 0 Then
                            If dt.Rows(0).Item(0).ToString().Equals("1") Then
                                Call mt_ShowMessage(dt.Rows(0).Item(1).ToString(), MessageType.Success)
                                Call mt_CargarDatosEgr(Me.ddlPlanCurricular.SelectedValue)
                            Else
                                Call mt_ShowMessage(dt.Rows(0).Item(1).ToString(), MessageType.Warning)
                            End If
                        End If

                        dt.Dispose()
                End Select
            End If
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub gvEgreso_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles gvEgreso.RowEditing

    End Sub

    Protected Sub gvEgreso_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles gvEgreso.RowDeleting

    End Sub

#End Region

#Region "Metodos"

    Protected Sub mt_ShowMessage(ByVal Message As String, ByVal type As MessageType)
        Page.RegisterStartupScript("Mensaje", "<script>ShowMessage('" & Message & "','" & type.ToString & "');</script>")
    End Sub

    Private Sub mt_CargarCarreraProfesional()
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString

        Try
            obj.AbrirConexion()
            If cod_ctf = 1 Or cod_ctf = 232 Then
                dt = obj.TraerDataTable("ConsultarCarreraProfesionalV2", "CF", "2", cod_user)
            Else
                dt = obj.TraerDataTable("ConsultarCarreraProfesionalV2", "UX", "2", cod_user)
            End If
            obj.CerrarConexion()

            Call mt_CargarCombo(Me.ddlCarreraProf, dt, "codigo_Cpf", "nombre_Cpf")
            dt.dispose()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub mt_CargarPlanCurricular(ByVal codigo_cpf As String)
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString

        Try
            obj.AbrirConexion()
            dt = obj.TraerDataTable("COM_ListarPlanCurricular", codigo_cpf, -1)
            obj.CerrarConexion()

            Call mt_CargarCombo(Me.ddlPlanCurricular, dt, "codigo_pcur", "nombre_pcur")
            dt.dispose()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub mt_CargarCompetenciaIng()
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString

        Try
            obj.AbrirConexion()
            dt = obj.TraerDataTable("COM_ListarCompetenciaPorTipo", "1", Me.ddlCategoriaIng.SelectedValue)
            obj.CerrarConexion()

            Call mt_CargarCombo(Me.ddlCompetenciaIng, dt, "codigo_com", "nombre")
            dt.dispose()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub mt_CargarCompetenciaEgr()
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString

        Try
            obj.AbrirConexion()
            dt = obj.TraerDataTable("COM_ListarCompetenciaPorTipo", "2", Me.ddlCategoriaEgr.SelectedValue)
            obj.CerrarConexion()

            Call mt_CargarCombo(Me.ddlCompetenciaEgr, dt, "codigo_com", "nombre")
            dt.dispose()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub mt_CargarCategoria()
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString

        Try
            obj.AbrirConexion()
            dt = obj.TraerDataTable("COM_ListarCategoriaCompetencia")
            obj.CerrarConexion()

            Call mt_CargarCombo(Me.ddlCategoriaIng, dt, "codigo_cat", "nombre")
            Call mt_CargarCombo(Me.ddlCategoriaEgr, dt, "codigo_cat", "nombre")
            dt.dispose()
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

    Private Sub mt_CargarDatosIng(ByVal cod_pcur As String)
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString

        Try
            Dim codigo_pcur As String
            'codigo_pcur = IIf(String.IsNullOrEmpty(Me.ddlPlanCurricular.SelectedValue), "-1", Me.ddlPlanCurricular.SelectedValue)
            codigo_pcur = IIf(String.IsNullOrEmpty(cod_pcur), "-1", cod_pcur)

            obj.AbrirConexion()
            dt = obj.TraerDataTable("COM_ListarPerfilIngreso", codigo_pcur)
            obj.CerrarConexion()
            Me.gvIngreso.DataSource = dt
            Me.gvIngreso.DataBind()

            If ddlPlanCurricular.SelectedValue >= 0 Then
                Me.tabs.Visible = True
            Else
                Me.tabs.Visible = False
            End If

            Call mt_CargarCompetenciaIng()
            dt.Dispose()
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Private Sub mt_CargarDatosEgr(ByVal cod_pcur As String)
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString

        Try
            Dim codigo_pcur As String
            'codigo_pcur = IIf(String.IsNullOrEmpty(Me.ddlPlanCurricular.SelectedValue), "-1", Me.ddlPlanCurricular.SelectedValue)
            codigo_pcur = IIf(String.IsNullOrEmpty(cod_pcur), "-1", cod_pcur)

            obj.AbrirConexion()
            dt = obj.TraerDataTable("COM_ListarPerfilEgreso", codigo_pcur)
            obj.CerrarConexion()
            Me.gvEgreso.DataSource = dt
            Me.gvEgreso.DataBind()

            If ddlPlanCurricular.SelectedValue >= 0 Then
                Me.tabs.Visible = True
            Else
                Me.tabs.Visible = False
            End If

            Call mt_CargarCompetenciaEgr()
            dt.Dispose()
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Private Sub mt_LimpiarFormulario()
        Me.hdCodigoIng.Value = ""
        Me.txtCompetenciaIng.Text = ""
        Me.txtDescripcionIng.Text = ""
        'Me.ddlCategoriaIng.SelectedValue = 0
        'Me.ddlCompetenciaIng.SelectedValue = 0

        Me.hdCodigoEgr.Value = ""
        Me.txtCompetenciaEgr.Text = ""
        Me.txtDescripcionEgr.Text = ""
        'Me.ddlCategoriaEgr.SelectedValue = 0
        'Me.ddlCompetenciaEgr.SelectedValue = 0
    End Sub

#End Region

End Class
