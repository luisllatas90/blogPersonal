﻿Imports System.Collections.Generic
Imports System.IO

Partial Class GestionCurricular_FrmResultadoAprendizaje
    Inherits System.Web.UI.Page

#Region "Declaracion de Variables"

    Dim cod_user As Integer '= 5751
    Dim codigo_dis As Integer = 0

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
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub Page_LoadComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.LoadComplete
        Try
            cod_user = Session("id_per")

            If Not IsPostBack Then
                Dim dt As Data.DataTable = New Data.DataTable()
                Dim dtElim As Data.DataTable = New Data.DataTable()

                dt.Columns.Add("codigo_res")
                dt.Columns.Add("descripcion_res")
                dt.Columns.Add("peso_res")
                dt.Columns.Add("tipo_prom")
                ViewState("dt") = dt

                dtElim.Columns.Add("codigo_res")
                ViewState("dtElim") = dtElim

                Call BindGrid()

                Call mt_CargarSemestre()
                Call mt_CargarCarreraProf()
                Call mt_CargarPlanEstudio(0)
                Session("codigo_dis") = Nothing

                If Not String.IsNullOrEmpty(Session("gc_codigo_cur")) Then
                    If Me.ddlSemestre.Items.Count > 0 Then Me.ddlSemestre.SelectedValue = Session("gc_codigo_cac")
                    If Me.ddlCarreraProf.Items.Count > 0 Then Me.ddlCarreraProf.SelectedValue = Session("gc_codigo_cpf") : mt_CargarPlanEstudio(Session("gc_codigo_cpf"))
                    If Me.ddlPlanEstudio.Items.Count > 0 Then Me.ddlPlanEstudio.SelectedValue = Session("gc_codigo_pes")
                    Call mt_CargarDatos(Session("gc_codigo_cac"), Session("gc_codigo_pes"), Session("gc_codigo_cur"))
                    Me.ddlSemestre.Enabled = False
                    Me.ddlCarreraProf.Enabled = False
                    Me.ddlPlanEstudio.Enabled = False
                    Me.lblAsignatura.Visible = False
                    Me.ddlAsignatura.Visible = False
                    Me.btnBack.Visible = True
                    Me.btnSeguir.Visible = True
                    Me.lblCurso.InnerText = "Registrar Diseño de Asignatura: " & Session("gc_nombre_cur")
                Else
                    Call mt_CargarAsignatura()
                    Call mt_CargarDatos(Me.ddlSemestre.SelectedValue, Me.ddlPlanEstudio.SelectedValue, Me.ddlAsignatura.SelectedValue)
                    Me.ddlSemestre.Enabled = True
                    Me.ddlCarreraProf.Enabled = True
                    Me.ddlPlanEstudio.Enabled = True
                    Me.lblAsignatura.Visible = True
                    Me.ddlAsignatura.Visible = True
                    Me.btnBack.Visible = False
                    Me.btnSeguir.Visible = False
                End If
            End If
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub BindGrid()
        Dim dt As Data.DataTable = TryCast(ViewState("dt"), Data.DataTable)

        If dt.Rows.Count > 0 Then
            gvDetalle.DataSource = dt
            gvDetalle.DataBind()
        Else
            dt.Rows.Add(dt.NewRow())
            gvDetalle.DataSource = dt
            gvDetalle.DataBind()

            gvDetalle.Rows(0).Cells.Clear()
        End If

        Me.udpDetalle.Update()
    End Sub

    Protected Sub gvDetalle_RowEditing(ByVal sender As Object, ByVal e As GridViewEditEventArgs)
        gvDetalle.EditIndex = e.NewEditIndex
        BindGrid()

        'gvDetalle.FooterRow.FindControl("txtNewResultado").Focus()
        'gvDetalle.Rows(e.NewEditIndex).FindControl("txtNewResultado").Focus()
        'gvDetalle.Rows(e.NewEditIndex).Cells(1).Controls(0).Focus()
    End Sub

    Protected Sub gvDetalle_RowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs) Handles gvDetalle.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            If e.Row.RowState = DataControlRowState.Edit Or e.Row.RowState = DataControlRowState.Insert Then
                GridViewSetFocus(e.Row, "txtResultado")
            End If
        End If

        If e.Row.RowType = DataControlRowType.Footer Then
            If e.Row.RowState = DataControlRowState.Selected Then
                GridViewSetFocus(e.Row, "txtNewResultado")
            End If
        End If
    End Sub

    Private Sub GridViewSetFocus(ByVal row As GridViewRow, ByVal ControlName As String)
        Dim found As Boolean = False
        Dim control_name_lower As String = ControlName.ToLower()

        For Each cell As TableCell In row.Cells
            For Each control As Control In cell.Controls
                If control.ID IsNot Nothing Then
                    If control.ID.ToLower() = control_name_lower Then
                        found = True
                        control.Focus()
                        Exit For
                    End If
                End If
            Next

            If found Then Exit For
        Next
    End Sub

    Protected Sub OnUpdate(ByVal sender As Object, ByVal e As EventArgs)
        Dim dt As Data.DataTable = TryCast(ViewState("dt"), Data.DataTable)
        Dim row As GridViewRow = TryCast((TryCast(sender, LinkButton)).NamingContainer, GridViewRow)
        Dim txtResultado As TextBox = CType(gvDetalle.Rows(row.RowIndex).FindControl("txtResultado"), TextBox)

        dt.Rows(row.RowIndex)("descripcion_res") = txtResultado.Text
        ViewState("dt") = dt
        gvDetalle.EditIndex = -1
        BindGrid()
    End Sub

    Protected Sub OnCancel(ByVal sender As Object, ByVal e As EventArgs)
        gvDetalle.EditIndex = -1
        BindGrid()
    End Sub

    Protected Sub OnDelete(ByVal sender As Object, ByVal e As EventArgs)
        'Try
        Dim valor As String = "1"
        Dim dt As Data.DataTable = TryCast(ViewState("dt"), Data.DataTable)
        Dim row As GridViewRow = TryCast((TryCast(sender, LinkButton)).NamingContainer, GridViewRow)
        Dim codigo_res As String = gvDetalle.DataKeys(row.RowIndex).Item("codigo_res").ToString '(CType(row.Cells(0).Controls(0), TextBox)).Text

        If Not String.IsNullOrEmpty(codigo_res) And Not codigo_res.Equals("0") Then
            Dim rpta As String = ""
            Dim dtRpta As New Data.DataTable
            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString

            obj.AbrirConexion()
            dtRpta = obj.TraerDataTable("COM_VerificarIndicadorResultados", codigo_res)
            obj.CerrarConexion()

            valor = dtRpta.Rows(0).Item(0).ToString
            rpta = dtRpta.Rows(0).Item(1).ToString

            dtRpta.Dispose()

            If valor.Equals("1") Then
                Dim dtElim As Data.DataTable = TryCast(ViewState("dtElim"), Data.DataTable)
                dtElim.Rows.Add(codigo_res)
                ViewState("dtElim") = dtElim
            Else
                Throw New Exception(rpta)
                Return
            End If
        End If

        If valor.Equals("1") Then
            dt.Rows.RemoveAt(row.RowIndex)
            ViewState("dt") = dt
            gvDetalle.EditIndex = -1
            BindGrid()
        End If
        'Catch ex As Exception
        '    Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        'End Try
    End Sub

    Protected Sub OnNew(ByVal sender As Object, ByVal e As EventArgs)
        Dim dt As Data.DataTable = TryCast(ViewState("dt"), Data.DataTable)
        Dim txtResultado As TextBox = CType(gvDetalle.FooterRow.FindControl("txtNewResultado"), TextBox)

        If String.IsNullOrEmpty(txtResultado.Text) Then
            Throw New Exception("Ingrese un valor al resultado de la unidad")
            Return
        Else
            If String.IsNullOrEmpty(dt.Rows(0).Item(1).ToString) Then
                dt.Rows.RemoveAt(0)
            End If

            dt.Rows.Add(0, txtResultado.Text, 0, 0)
            ViewState("dt") = dt
            gvDetalle.EditIndex = -1
            BindGrid()
        End If
    End Sub

    Protected Sub OnEditUnidad(ByVal sender As Object, ByVal e As EventArgs)
        Try
            Dim row As GridViewRow = TryCast((TryCast(sender, LinkButton)).NamingContainer, GridViewRow)
            Dim codigo_dis As String = gvResultado.DataKeys(row.RowIndex).Item("codigo_dis").ToString
            Dim codigo_uni As String = gvResultado.DataKeys(row.RowIndex).Item("codigo_uni").ToString
            Dim numero_uni As String = gvResultado.DataKeys(row.RowIndex).Item("numero_uni").ToString
            Dim descripcion As String = gvResultado.DataKeys(row.RowIndex).Item("descripcion_uni").ToString

            Me.hdCodigoUnidad.Value = ""
            Me.txtUnidad.Text = ""

            If Me.gvResultado.Rows.Count = 0 Then
                Page.RegisterStartupScript("alerta", "<script>alert('No existe Diseño de Asignatura para editar');</script>")
            Else
                Session("codigo_dis") = codigo_dis
                Me.hdCodigoUnidad.Value = codigo_uni
                Me.txtUnidad.Text = descripcion

                Call mt_CargarDatosDetalle()
                Call mt_CargarNumeroUnidad()
                Me.ddlUnidad.SelectedItem.Text = numero_uni
                Page.RegisterStartupScript("Pop", "<script>openModal('editar');</script>")
            End If
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub OnDeleteUnidad(ByVal sender As Object, ByVal e As EventArgs)
        Try
            Dim row As GridViewRow = TryCast((TryCast(sender, LinkButton)).NamingContainer, GridViewRow)
            Dim codigo_uni As String = gvResultado.DataKeys(row.RowIndex).Item("codigo_uni").ToString

            Dim obj As New ClsConectarDatos
            Dim dt As New Data.DataTable
            Dim valor As Integer = 1
            Dim rpta As String = ""

            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()

            dt = obj.TraerDataTable("COM_EliminarUnidad", codigo_uni, cod_user)
            If dt.Rows.Count > 0 Then
                valor = CInt(dt.Rows(0).Item(0).ToString())
                rpta = dt.Rows(0).Item(1).ToString()
            End If
            dt.Dispose()
            obj.CerrarConexion()

            If valor = 0 Then
                Call mt_ShowMessage(rpta, MessageType.Warning)
            Else
                Call mt_ShowMessage(rpta, MessageType.Success)
            End If

            If Not String.IsNullOrEmpty(Session("gc_codigo_cur")) Then
                Call mt_CargarDatos(Session("gc_codigo_cac"), Session("gc_codigo_pes"), Session("gc_codigo_cur"))
            Else
                Call mt_CargarDatos(Me.ddlSemestre.SelectedValue, Me.ddlPlanEstudio.SelectedValue, Me.ddlAsignatura.SelectedValue)
            End If
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub ddlCarreraProf_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlCarreraProf.SelectedIndexChanged
        Dim _codigo_cur As Integer
        Try
            Call mt_CargarPlanEstudio(Me.ddlCarreraProf.SelectedValue)
            _codigo_cur = IIf(String.IsNullOrEmpty(Session("gc_codigo_cur")), Me.ddlAsignatura.SelectedValue, Session("gc_codigo_cur"))
            Call mt_CargarDatos(Me.ddlSemestre.SelectedValue, Me.ddlPlanEstudio.SelectedValue, _codigo_cur)
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub ddlPlanEstudio_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPlanEstudio.SelectedIndexChanged
        Dim _codigo_cur As Integer
        Try
            Call mt_CargarAsignatura()
            _codigo_cur = IIf(String.IsNullOrEmpty(Session("gc_codigo_cur")), Me.ddlAsignatura.SelectedValue, Session("gc_codigo_cur"))
            Call mt_CargarDatos(Me.ddlSemestre.SelectedValue, Me.ddlPlanEstudio.SelectedValue, _codigo_cur)
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub ddlSemestre_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlSemestre.SelectedIndexChanged
        Dim _codigo_cur As Integer
        Try
            _codigo_cur = IIf(String.IsNullOrEmpty(Session("gc_codigo_cur")), Me.ddlAsignatura.SelectedValue, Session("gc_codigo_cur"))
            Call mt_CargarDatos(Me.ddlSemestre.SelectedValue, Me.ddlPlanEstudio.SelectedValue, _codigo_cur)
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub ddlAsignatura_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlAsignatura.SelectedIndexChanged
        Dim _codigo_cur As Integer
        Try
            _codigo_cur = IIf(String.IsNullOrEmpty(Session("gc_codigo_cur")), Me.ddlAsignatura.SelectedValue, Session("gc_codigo_cur"))
            Call mt_CargarDatos(Me.ddlSemestre.SelectedValue, Me.ddlPlanEstudio.SelectedValue, _codigo_cur)
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub btnAgregar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAgregar.Click
        Try
            Session("codigo_dis") = Nothing

            Me.hdCodigoUnidad.Value = ""
            Me.txtUnidad.Text = ""

            Call mt_CargarNumeroUnidad()
            Page.RegisterStartupScript("Pop", "<script>openModal('nuevo');</script>")
            Call mt_CargarDatosDetalle()
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub btnAceptar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAceptar.Click
        Dim obj As New ClsConectarDatos
        Dim codigo_uni As Integer = 0
        Dim _codigo_cur, _tipo_prom As Integer
        Dim _peso_res As Decimal
        Dim dt As New Data.DataTable
        Dim flag As Boolean = False
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString

        Try
            If Not (IsDBNull(Session("perlogin"))) And Session("perlogin").ToString <> "" Then
                obj.IniciarTransaccion()
                _codigo_cur = IIf(String.IsNullOrEmpty(Session("gc_codigo_cur")), Me.ddlAsignatura.SelectedValue, Session("gc_codigo_cur"))

                If String.IsNullOrEmpty(Me.hdCodigoUnidad.Value) Then
                    dt = obj.TraerDataTable("COM_RegistrarDiseñoAsignatura", Me.ddlPlanEstudio.SelectedValue, _codigo_cur, Me.ddlSemestre.SelectedValue, cod_user)

                    If dt.Rows.Count > 0 Then
                        codigo_dis = CInt(dt.Rows(0).Item(0).ToString())

                        dt = obj.TraerDataTable("COM_RegistrarUnidad", codigo_dis, ddlUnidad.SelectedValue, ddlUnidad.SelectedItem.ToString, Me.txtUnidad.Text, cod_user)

                        If dt.Rows.Count > 0 Then
                            codigo_uni = CInt(dt.Rows(0).Item(0).ToString())
                            flag = True
                        End If
                    End If
                Else
                    codigo_uni = CInt(Me.hdCodigoUnidad.Value)
                    codigo_dis = Session("codigo_dis")

                    obj.Ejecutar("COM_ActualizarUnidad", codigo_uni, codigo_dis, ddlUnidad.SelectedValue, ddlUnidad.SelectedItem.ToString, Me.txtUnidad.Text, cod_user)
                    flag = True
                End If

                If flag Then
                    dt = New Data.DataTable

                    Dim dtDet As Data.DataTable = TryCast(ViewState("dt"), Data.DataTable)
                    Dim dtElim As Data.DataTable = TryCast(ViewState("dtElim"), Data.DataTable)

                    '--> Confirmar eliminación de resultados suprimidos de la lista actual
                    For i As Integer = 0 To dtElim.Rows.Count - 1
                        Dim codigo_res As Object
                        codigo_res = dtElim.Rows(i).Item(0).ToString
                        dt = obj.TraerDataTable("COM_EliminarResultados", codigo_res)
                    Next

                    If dtDet.Rows.Count > 0 Then
                        Dim valor As String = "1"
                        Dim rpta As String = ""

                        If dt.Rows.Count > 0 Then
                            valor = dt.Rows(0).Item(0).ToString
                            rpta = dt.Rows(0).Item(1).ToString

                            dt.Dispose()
                        End If

                        If valor.Equals("1") Then
                            '--> Confirmar registro o actualización de resultados
                            For i As Integer = 0 To Me.gvDetalle.Rows.Count - 1
                                Dim codigo_res, descripcion_res As Object
                                codigo_res = dtDet.Rows(i).Item(0).ToString
                                codigo_res = IIf(String.IsNullOrEmpty(codigo_res), DBNull.Value, codigo_res)
                                descripcion_res = dtDet.Rows(i).Item(1).ToString

                                _peso_res = CDbl(dtDet.Rows(i).Item(2).ToString)
                                _tipo_prom = CInt(dtDet.Rows(i).Item(3).ToString)

                                obj.Ejecutar("COM_RegistrarResultados", codigo_res, codigo_uni, descripcion_res, _peso_res, _tipo_prom, cod_user)
                            Next

                            If String.IsNullOrEmpty(Me.hdCodigoUnidad.Value) Then
                                Call mt_ShowMessage("Diseño de Asignatura registrado con éxito", MessageType.Success)
                            Else
                                Call mt_ShowMessage("Diseño de Asignatura actualizado con éxito", MessageType.Success)
                            End If
                        Else
                            Call mt_ShowMessage(rpta, MessageType.Info)
                        End If
                    End If

                    Session("codigo_dis") = Nothing
                    obj.TerminarTransaccion()
                    Call mt_CargarDatos(Me.ddlSemestre.SelectedValue, Me.ddlPlanEstudio.SelectedValue, _codigo_cur)
                Else
                    obj.TerminarTransaccion()
                    Call mt_ShowMessage("Ocurrió un error al procesar el registro", MessageType.Error)
                End If
            Else
                Throw New Exception("Inicie Sesión")
            End If
        Catch ex As Exception
            obj.AbortarTransaccion()
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub btnBack_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBack.Click
        Try
            Response.Redirect("~/GestionCurricular/frmConfigurarAsignatura.aspx")
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub btnSeguir_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSeguir.Click
        Try
            If Me.gvResultado.Rows.Count = 0 Then Throw New Exception("¡ No se ha registrado ninguna Unidad para esta Asignatura !")
            If Session("gc_codigo_dis") = -1 Then Session("gc_codigo_dis") = CInt(Me.gvResultado.DataKeys(0).Values("codigo_dis"))
            Response.Redirect("~/GestionCurricular/frmCriteriosEvaluacion.aspx")
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

#End Region

#Region "Metodos"

    Protected Sub mt_ShowMessage(ByVal Message As String, ByVal type As MessageType)
        Page.RegisterStartupScript("Mensaje", "<script>ShowMessage('" & Message & "','" & type.ToString & "');</script>")
    End Sub

    Private Sub mt_CargarCarreraProf()
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString

        Try
            obj.AbrirConexion()
            If cod_user = 684 Then
                dt = obj.TraerDataTable("ConsultarCarreraProfesionalV2", "CF", "2", cod_user)
            Else
                'dt = obj.TraerDataTable("ConsultarCarreraProfesionalV2", "UC", "2", cod_user)
                dt = obj.TraerDataTable("DiseñoAsignatura_Listar", "CD", -1, -1, -1, Session("gc_codigo_cac"), cod_user)
            End If
            obj.CerrarConexion()

            Call mt_CargarCombo(Me.ddlCarreraProf, dt, "codigo_Cpf", "nombre_Cpf")
            dt.Dispose()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub mt_CargarPlanEstudio(ByVal codigo_cpf As String)
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString

        Try
            obj.AbrirConexion()
            dt = obj.TraerDataTable("ConsultarPlanEstudio", "PE", 2, codigo_cpf)
            obj.CerrarConexion()

            Call mt_CargarCombo(Me.ddlPlanEstudio, dt, "codigo_Pes", "descripcion_Pes")
            dt.Dispose()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub mt_CargarSemestre()
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString

        Try
            obj.AbrirConexion()
            dt = obj.TraerDataTable("ConsultarCicloAcademico", "DA", "")
            obj.CerrarConexion()

            Call mt_CargarCombo(Me.ddlSemestre, dt, "codigo_Cac", "descripcion_Cac")
            dt.Dispose()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub mt_CargarAsignatura()
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString

        Try
            obj.AbrirConexion()
            dt = obj.TraerDataTable("COM_ListarAsignatura", cod_user, "TO", "GR")
            obj.CerrarConexion()

            Call mt_CargarCombo(Me.ddlAsignatura, dt, "codigo_cur", "nombre_Cur")
            dt.Dispose()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub mt_CargarNumeroUnidad()
        Try
            Dim obj As New ClsConectarDatos
            Dim dt As New Data.DataTable
            Dim codigo_uni As String = IIf(String.IsNullOrEmpty(hdCodigoUnidad.Value), "-1", hdCodigoUnidad.Value)
            Dim _codigo_cur As Integer = IIf(String.IsNullOrEmpty(Session("gc_codigo_cur")), Me.ddlAsignatura.SelectedValue, Session("gc_codigo_cur"))

            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString

            obj.AbrirConexion()
            dt = obj.TraerDataTable("COM_ObtenerNumeroUnidad", Me.ddlPlanEstudio.SelectedValue, _codigo_cur, Me.ddlSemestre.SelectedValue, codigo_uni)
            obj.CerrarConexion()

            Call mt_CargarCombo(Me.ddlUnidad, dt, "orden", "codigo")
            dt.Dispose()

            If ddlUnidad.Items.Count > 0 Then ddlUnidad.SelectedIndex = 0
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

    Private Sub mt_CargarDatos(ByVal codigo_cac As Integer, ByVal codigo_pes As Integer, ByVal codigo_cur As Integer)
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString

        Try
            obj.AbrirConexion()
            dt = obj.TraerDataTable("COM_ListarResultadoAprendizaje", -1, codigo_pes, codigo_cur, codigo_cac, -1)
            obj.CerrarConexion()

            Me.gvResultado.DataSource = dt
            Me.gvResultado.DataBind()

            If Me.gvResultado.Rows.Count > 0 Then
                Call mt_AgruparFilas(Me.gvResultado.Rows, 0, 3)
                Call mt_AgruparBotones()
            End If
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Private Sub mt_CargarDatosDetalle()
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        Dim _codigo_cur As Integer
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString

        Try
            obj.AbrirConexion()
            Dim cod As String = IIf(String.IsNullOrEmpty(Session("codigo_dis")), "0", Session("codigo_dis"))
            Dim uni As String = IIf(String.IsNullOrEmpty(Me.hdCodigoUnidad.Value), "-1", Me.hdCodigoUnidad.Value)
            _codigo_cur = IIf(String.IsNullOrEmpty(Session("gc_codigo_cur")), Me.ddlAsignatura.SelectedValue, Session("gc_codigo_cur"))
            dt = obj.TraerDataTable("COM_ListarResultadoAprendizaje", cod, Me.ddlPlanEstudio.SelectedValue, _codigo_cur, Me.ddlSemestre.SelectedValue, uni)
            obj.CerrarConexion()

            Dim columns As String() = {"codigo_res", "descripcion_res", "peso_res", "tipo_prom"}
            Dim dtDet As Data.DataTable = New Data.DataView(dt).ToTable(False, columns)

            ViewState("dt") = dtDet
            BindGrid()
            dt.Dispose()
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Private Sub mt_AgruparFilas(ByVal gridViewRows As GridViewRowCollection, ByVal startIndex As Integer, ByVal totalColumns As Integer)
        If totalColumns = 0 Then Return
        Dim i As Integer, count As Integer = 1
        Dim lst As ArrayList = New ArrayList()
        lst.Add(gridViewRows(0))
        Dim ctrl As TableCell
        ctrl = gridViewRows(0).Cells(startIndex)
        For i = 1 To gridViewRows.Count - 1
            Dim nextTbCell As TableCell = gridViewRows(i).Cells(startIndex)
            If ctrl.Text = nextTbCell.Text Then
                count += 1
                nextTbCell.Visible = False
                lst.Add(gridViewRows(i))
            Else
                If count > 1 Then
                    ctrl.RowSpan = count
                    Call mt_AgruparFilas(New GridViewRowCollection(lst), startIndex + 1, totalColumns - 1)
                End If
                count = 1
                lst.Clear()
                ctrl = gridViewRows(i).Cells(startIndex)
                lst.Add(gridViewRows(i))
            End If
        Next
        If count > 1 Then
            ctrl.RowSpan = count
            Call mt_AgruparFilas(New GridViewRowCollection(lst), startIndex + 1, totalColumns - 1)
        End If
        count = 1
        lst.Clear()
    End Sub

    Private Sub mt_AgruparBotones()
        Dim filas As Integer = 0
        Dim i As Integer = 0
        Dim cod_uni As String
        cod_uni = gvResultado.DataKeys(0).Item("codigo_uni").ToString

        Dim ctrlE As TableCell
        Dim ctrlD As TableCell

        For i = 0 To gvResultado.Rows.Count - 1
            Dim cod_ref As String = gvResultado.DataKeys(i).Item("codigo_uni").ToString

            If cod_uni <> cod_ref Then
                ctrlE = gvResultado.Rows(i - filas).Cells(4)
                ctrlE.RowSpan = filas

                ctrlD = gvResultado.Rows(i - filas).Cells(5)
                ctrlD.RowSpan = filas

                filas = 1
                cod_uni = cod_ref
            Else
                filas += 1
            End If

            If i = gvResultado.Rows.Count - 1 Then
                ctrlE = gvResultado.Rows(gvResultado.Rows.Count - filas).Cells(4)
                ctrlE.RowSpan = filas

                ctrlD = gvResultado.Rows(gvResultado.Rows.Count - filas).Cells(5)
                ctrlD.RowSpan = filas
            End If
        Next

        cod_uni = -1
        For j As Integer = 0 To gvResultado.Rows.Count - 1
            Dim cod_ref As String = gvResultado.DataKeys(j).Item("codigo_uni").ToString

            If cod_uni <> cod_ref Then
                cod_uni = cod_ref
            Else
                gvResultado.Rows(j).Cells(4).Visible = False
                gvResultado.Rows(j).Cells(5).Visible = False
            End If
        Next
    End Sub

#End Region

End Class
