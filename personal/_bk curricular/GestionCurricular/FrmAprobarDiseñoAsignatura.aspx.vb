﻿
Imports System.Collections.Generic
Imports System.IO

Partial Class GestionCurricular_FrmAprobarDiseñoAsignatura
    Inherits System.Web.UI.Page

#Region "Declaracion de Variables"

    Dim cod_user As Integer '= 5074
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
                Response.Redirect("../../../sinacceso.html")
            End If

            cod_user = Session("id_per")
            cod_ctf = Request.QueryString("ctf")

            If Not IsPostBack Then
                Call mt_CargarSemestre()
                Call mt_CargarPlanCurricular()
            End If
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub ddlSemestre_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlSemestre.SelectedIndexChanged
        Try
            Call mt_CargarDatos()
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub ddlPlanCurricular_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPlanCurricular.SelectedIndexChanged
        Try
            Call mt_CargarDatos()
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub ddlEstado_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlEstado.SelectedIndexChanged
        Try
            Call mt_CargarDatos()
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub btnListar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnListar.Click
        Dim flag As Boolean = True

        If ddlPlanCurricular.SelectedValue = -1 Then
            Page.RegisterStartupScript("alerta", "<script>alert('Seleccione un Plan Curricular');</script>")
            Me.ddlPlanCurricular.Focus()
            flag = False
        End If

        If ddlSemestre.SelectedValue = -1 Then
            Page.RegisterStartupScript("alerta", "<script>alert('Seleccione el Semestre');</script>")
            Me.ddlSemestre.Focus()
            flag = False
        End If

        If flag Then
            Call mt_CargarDatos()
        Else
            Me.gvResultados.DataSource = Nothing
            Me.gvResultados.DataBind()
        End If
    End Sub

    Protected Sub gvResultados_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvResultados.RowCommand
        Try
            Dim index As Integer = CInt(e.CommandArgument)
            Dim obj As New ClsConectarDatos
            Dim dtUni, dtRes, dtCon, dtEst, dtRef As New Data.DataTable
            Dim codigo_Pes, codigo_Cur, codigo_dis As Integer
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            If index >= 0 And e.CommandName = "Aprobar" Then
                If Not String.IsNullOrEmpty(Me.gvResultados.DataKeys(index).Values("fecha_apr").ToString) Then
                    Call mt_ShowMessage("El diseño fue aprobado anteriormente", MessageType.Info)
                    Return
                End If
                codigo_dis = Me.gvResultados.DataKeys(index).Values("codigo_dis")
                codigo_Cur = Me.gvResultados.DataKeys(index).Values("codigo_Cur")
                codigo_Pes = Me.gvResultados.DataKeys(index).Values("codigo_Pes")
                Session("gc_codigo_dis") = codigo_dis
                obj.AbrirConexion()
                dtUni = obj.TraerDataTable("COM_ListarResultadoAprendizaje", -1, codigo_Pes, codigo_Cur, Me.ddlSemestre.SelectedValue, -1)
                dtRes = obj.TraerDataTable("ResultadoAprendizaje_Listar", "", -1, codigo_dis, "")
                dtCon = obj.TraerDataTable("COM_ListarContenidoAsignatura", codigo_dis, -1, 0)
                dtEst = obj.TraerDataTable("EstrategiaDidactica_Listar", -1, codigo_dis, "")
                dtRef = obj.TraerDataTable("ReferenciaBibliografica_Listar", -1, codigo_dis, -1, "")
                obj.CerrarConexion()
                ' Mostrar Unidades de Asignatura
                Me.gvUnidad.DataSource = dtUni
                Me.gvUnidad.DataBind()
                If Me.gvUnidad.Rows.Count > 0 Then mt_AgruparFilas(Me.gvUnidad.Rows, 0, 2)
                ' Mostrar Criterios de Evaluación 
                Me.gvCriterios.DataSource = dtRes
                Me.gvCriterios.DataBind()
                If Me.gvCriterios.Rows.Count > 0 Then mt_AgruparFilas(Me.gvCriterios.Rows, 0, 5)
                ' Mostrar Contenido de Asignatura
                Me.gvContenido.DataSource = dtCon
                Me.gvContenido.DataBind()
                ' Mostrar Estrategias Didácticas
                Me.gvEstrategia.DataSource = dtEst
                Me.gvEstrategia.DataBind()
                ' Mostrar Referencias Bibliográficas
                Me.gvReferencia.DataSource = dtRef
                Me.gvReferencia.DataBind()
                Page.RegisterStartupScript("Pop", "<script>openModal('aprobar');</script>")
            End If
            If index >= 0 And e.CommandName = "Observar" Then
                codigo_dis = Me.gvResultados.DataKeys(index).Values("codigo_dis")
                Session("gc_codigo_dis") = codigo_dis
                Page.RegisterStartupScript("Pop", "<script>openModal('observar');</script>")
            End If
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub btnAprobar_Click(ByVal sender As Object, ByVal e As System.EventArgs)

    End Sub

    Protected Sub gvCriterios_OnRowCreated(ByVal sender As Object, ByVal e As GridViewRowEventArgs)
        Try
            If e.Row.RowType = DataControlRowType.Header Then
                Dim objGridView As GridView = CType(sender, GridView)
                Dim objgridviewrow As GridViewRow = New GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert)
                Dim objtablecell As TableCell = New TableCell()
                mt_AgregarCabecera(objgridviewrow, objtablecell, 2, "Resultado de Aprendizaje", "#D9534F")
                mt_AgregarCabecera(objgridviewrow, objtablecell, 2, "Indicadores de Aprendizaje", "#D9534F")
                mt_AgregarCabecera(objgridviewrow, objtablecell, 1, "Evidencia de Evaluación", "#D9534F")
                mt_AgregarCabecera(objgridviewrow, objtablecell, 2, "Instrumento de Evaluación", "#D9534F")
                objGridView.Controls(0).Controls.AddAt(0, objgridviewrow)
            End If
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub gvCriterios_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvCriterios.RowDataBound
        Try
            Dim tipo_prom, tipo_prom2 As Integer
            Dim peso_res, peso_ind, peso_ins As Double
            Dim codigo_ind, codigo_ins As Integer
            tipo_prom = Me.gvCriterios.DataKeys(e.Row.RowIndex).Values("tipo_prom")
            tipo_prom2 = Me.gvCriterios.DataKeys(e.Row.RowIndex).Values("tipo_prom2")
            peso_res = Me.gvCriterios.DataKeys(e.Row.RowIndex).Values("peso_res")
            peso_ind = Me.gvCriterios.DataKeys(e.Row.RowIndex).Values("peso_ind")
            peso_ins = Me.gvCriterios.DataKeys(e.Row.RowIndex).Values("peso_ins")
            codigo_ind = Me.gvCriterios.DataKeys(e.Row.RowIndex).Values("codigo_ind")
            codigo_ins = Me.gvCriterios.DataKeys(e.Row.RowIndex).Values("codigo_ins")
            If e.Row.RowType = DataControlRowType.DataRow Then
                If tipo_prom = 1 Then
                    e.Row.Cells(3).Text = IIf(codigo_ind = -1, "", "Promedio Simple")
                Else
                    e.Row.Cells(3).Text = CStr(peso_ind * 100) & " %"
                End If
                If tipo_prom2 = 1 Then
                    e.Row.Cells(6).Text = IIf(codigo_ins = -1, "", "Promedio Simple")
                Else
                    e.Row.Cells(6).Text = CStr(peso_ins * 100) & " %"
                End If
                e.Row.Cells(1).Text = CStr(peso_res * 100) & " %"
            End If
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub btnGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardar.Click
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        Try

            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString

            obj.AbrirConexion()
            dt = obj.TraerDataTable("COM_ActualizarDiseñoAsignatura", Session("gc_codigo_dis"), cod_user)
            obj.CerrarConexion()

            If dt.Rows.Count > 0 Then
                Dim msj As String = dt.Rows(0).Item(0).ToString()
                If msj.Substring(0, 5).Equals("Error") Then
                    Call mt_ShowMessage(msj, MessageType.Error)
                Else
                    Call mt_ShowMessage(msj, MessageType.Success)
                    Call mt_CargarDatos()
                End If
            Else
                Call mt_ShowMessage("Ocurrió un error al procesar la aprobación del diseño", MessageType.Error)
            End If
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub btnGrabar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGrabar.Click
        Dim obj As New ClsConectarDatos
        Dim codigo_dis As Integer
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
        Try
            codigo_dis = Session("gc_codigo_dis")
            'Throw New Exception(codigo_dis)
            obj.AbrirConexion()
            obj.Ejecutar("DiseñoAsignatura_observar", codigo_dis, "O", Me.txtObservacion.Text, cod_user)
            obj.CerrarConexion()
            mt_ShowMessage("¡ Se ha registrado la observación al sílabo !", MessageType.Success)
            ddlPlanCurricular_SelectedIndexChanged(Nothing, Nothing)
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

#End Region

#Region "Metodos"

    Protected Sub mt_ShowMessage(ByVal Message As String, ByVal type As MessageType)
        Page.RegisterStartupScript("Mensaje", "<script>ShowMessage('" & Message & "','" & type.ToString & "');</script>")
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

    Private Sub mt_CargarPlanCurricular()
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
        'cod_user = 648
        Try
            obj.AbrirConexion()
            If cod_ctf = 1 Or cod_ctf = 232 Then
                dt = obj.TraerDataTable("ConsultarCarreraProfesionalV2", "CF", "2", cod_user)
            Else
                dt = obj.TraerDataTable("ConsultarCarreraProfesionalV2", "UX", "2", cod_user)
            End If
            'dt = obj.TraerDataTable("COM_ListarPlanCurricular2", DBNull.Value, -1, cod_user)
            obj.CerrarConexion()
            'Call mt_CargarCombo(Me.ddlPlanCurricular, dt, "codigo_cpf", "nombre_pcur")nombre_Cpf
            mt_CargarCombo(Me.ddlPlanCurricular, dt, "codigo_cpf", "nombre_Cpf")
            dt.Dispose()
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

    Private Sub mt_CargarDatos()
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        Dim plan, semestre As String
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString

        Try
            plan = IIf(String.IsNullOrEmpty(Me.ddlPlanCurricular.SelectedValue), "0", Me.ddlPlanCurricular.SelectedValue)
            semestre = IIf(String.IsNullOrEmpty(Me.ddlSemestre.SelectedValue), "0", Me.ddlSemestre.SelectedValue)

            obj.AbrirConexion()
            dt = obj.TraerDataTable("COM_ListarAsignaturaAprobar", plan, semestre, Me.ddlEstado.SelectedValue)
            obj.CerrarConexion()
            Me.gvResultados.DataSource = dt
            Me.gvResultados.DataBind()
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
                    ctrl.VerticalAlign = VerticalAlign.Middle
                    mt_AgruparFilas(New GridViewRowCollection(lst), startIndex + 1, totalColumns - 1)
                End If
                count = 1
                lst.Clear()
                ctrl = gridViewRows(i).Cells(startIndex)
                lst.Add(gridViewRows(i))
            End If
        Next
        If count > 1 Then
            ctrl.RowSpan = count
            ctrl.VerticalAlign = VerticalAlign.Middle
            mt_AgruparFilas(New GridViewRowCollection(lst), startIndex + 1, totalColumns - 1)
        End If
        count = 1
        lst.Clear()
    End Sub

    Protected Sub mt_AgregarCabecera(ByVal objgridviewrow As GridViewRow, ByVal objtablecell As TableCell, ByVal colspan As Integer, ByVal celltext As String, ByVal backcolor As String)
        objtablecell = New TableCell()
        objtablecell.Text = celltext
        objtablecell.ColumnSpan = colspan
        'objtablecell.Style.Add("background-color", backcolor)
        'objtablecell.Style.Add("BackColor", backcolor)
        'objtablecell.Style.Add("Font-Bold", "true")
        objtablecell.HorizontalAlign = HorizontalAlign.Center
        objgridviewrow.Cells.Add(objtablecell)
    End Sub

#End Region

End Class
