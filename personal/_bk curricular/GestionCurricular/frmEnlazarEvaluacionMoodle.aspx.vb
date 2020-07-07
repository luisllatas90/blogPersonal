﻿
Partial Class GestionCurricular_frmEnlazarEvaluacionMoodle
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
            cod_user = Session("id_per")
            'cod_user = IIf(cod_user = 684, -1, cod_user) '2238
            If Not IsPostBack Then
                Me.lblCurso.InnerText = "Asignatura: " & Session("gc_nombre_cur")
                mt_CargarDatos(Session("gc_codigo_cup"))
                'Else
                '    mt_RefreshGrid()
            End If
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub btnBack_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBack.Click
        Try
            Response.Redirect("~/gestioncurricular/frmResumenCalificador.aspx")
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub gvEvaluacion_OnRowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs)
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        Dim _codigo_mod As Integer
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim ddl As DropDownList = CType(e.Row.FindControl("cboMoodle"), DropDownList)
                _codigo_mod = CInt(Me.gvEvaluacion.DataKeys(e.Row.RowIndex).Values("codigo_mod"))
                obj.AbrirConexion()
                dt = obj.TraerDataTable("DEA_EvaluacionesCurso_listar", "EM", Session("gc_codigo_cup"))
                obj.CerrarConexion()
                mt_CargarCombo(ddl, dt, "codigo_mod", "tarea_mod")
                ddl.SelectedValue = _codigo_mod
                ddl.Enabled = IIf(_codigo_mod <> -1, True, False)
            End If
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub cboMoodle_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim obj As New ClsConectarDatos
        Dim _codigo_eva, _codigo_emd As Integer
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        Try
            Dim cbo As DropDownList = sender
            Dim row As GridViewRow = CType(cbo.Parent.Parent, GridViewRow)
            _codigo_eva = CInt(Me.gvEvaluacion.DataKeys(row.RowIndex).Values("codigo_eva"))
            _codigo_emd = CInt(Me.gvEvaluacion.DataKeys(row.RowIndex).Values("codigo_emd"))
            obj.AbrirConexion()
            If _codigo_emd <> -1 Then
                If cbo.SelectedValue > -1 Then
                    obj.Ejecutar("DEA_EnlaceMoodle_actualizar", _codigo_emd, cbo.SelectedValue, cod_user)
                Else
                    obj.Ejecutar("DEA_EnlaceMoodle_eliminar", _codigo_emd, cod_user)
                End If
            Else
                If cbo.SelectedValue > -1 Then
                    obj.Ejecutar("DEA_EnlaceMoodle_insertar", _codigo_eva, cbo.SelectedValue, cod_user)
                End If
            End If
            obj.CerrarConexion()
            mt_ShowMessage("¡ Se registraron los datos correctamente !", MessageType.Success)
            mt_CargarDatos(Session("gc_codigo_cup"))
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub rdModoCalifica_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim rb As RadioButtonList = sender
            Dim row As GridViewRow = CType(rb.Parent.Parent, GridViewRow)
            Dim cbo As DropDownList = CType(Me.gvEvaluacion.Rows(row.RowIndex).FindControl("cboMoodle"), DropDownList)
            cbo.Enabled = IIf(rb.SelectedValue = 1, True, False)
            'mt_ShowMessage(rb.SelectedValue, MessageType.Success)
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

#End Region

#Region "Metodos"

    Protected Sub mt_ShowMessage(ByVal Message As String, ByVal type As MessageType)
        Page.RegisterStartupScript("Mensaje", "<script>ShowMessage('" & Message & "','" & type.ToString & "');</script>")
    End Sub

    Private Sub mt_CargarDatos(ByVal codigo_cup As Integer)
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
        Try
            obj.AbrirConexion()
            dt = obj.TraerDataTable("DEA_EvaluacionesCurso_listar", "", codigo_cup)
            obj.CerrarConexion()
            Me.gvEvaluacion.DataSource = dt
            Me.gvEvaluacion.DataBind()
            If Me.gvEvaluacion.Rows.Count > 0 Then mt_AgruparFilas(Me.gvEvaluacion.Rows, 0, 5)
            'Dim _codigo_emd As Integer
            'For x As Integer = 0 To Me.gvEvaluacion.Rows.Count - 1
            '    Dim ddl As DropDownList = CType(Me.gvEvaluacion.Rows(x).FindControl("cboMoodle"), DropDownList)
            '    _codigo_emd = CInt(Me.gvEvaluacion.DataKeys(x).Values("codigo_emd"))
            '    If _codigo_emd > -1 Then ddl.SelectedValue = _codigo_emd
            'Next
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
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

    Private Sub mt_CargarCombo(ByVal cbo As DropDownList, ByVal dt As Data.DataTable, ByVal datavalue As String, ByVal datatext As String)
        cbo.DataSource = dt
        cbo.DataTextField = datatext
        cbo.DataValueField = datavalue
        cbo.DataBind()
    End Sub

#End Region

End Class
