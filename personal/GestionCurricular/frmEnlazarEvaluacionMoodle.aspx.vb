﻿
Partial Class GestionCurricular_frmEnlazarEvaluacionMoodle
    Inherits System.Web.UI.Page

#Region "Declaracion de Variables"

    Private cod_user As Integer
    Private _nro_ra As Integer = 0
    Private _nro_ind As Integer = 0

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

            If Not IsPostBack Then
                Call mt_ObtenerInfoPersonal()
                Call mt_CargarDatos(Session("gc_codigo_cup"))
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
        Dim _codigo_ra, _codigo_ra_ant As Integer
        Dim _codigo_ind, _codigo_ind_ant As Integer
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                If e.Row.RowIndex > 0 Then
                    _codigo_ra = CInt(Me.gvEvaluacion.DataKeys(e.Row.RowIndex).Values("codigo_res"))
                    _codigo_ra_ant = CInt(Me.gvEvaluacion.DataKeys(e.Row.RowIndex - 1).Values("codigo_res"))
                    If _codigo_ra <> _codigo_ra_ant Then
                        _nro_ra += 1
                    End If
                    _codigo_ind = CInt(Me.gvEvaluacion.DataKeys(e.Row.RowIndex).Values("codigo_ind"))
                    _codigo_ind_ant = CInt(Me.gvEvaluacion.DataKeys(e.Row.RowIndex - 1).Values("codigo_ind"))
                    If _codigo_ind <> _codigo_ind_ant Then
                        _nro_ind += 1
                    End If
                Else
                    _nro_ra = 1
                    _nro_ind = 1
                End If
                e.Row.Cells(1).Text = "RA " & _nro_ra & " : " & e.Row.Cells(1).Text
                e.Row.Cells(2).Text = "IND " & _nro_ind & " : " & e.Row.Cells(2).Text
            End If
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub cboMoodle_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        'Dim obj As New ClsConectarDatos
        'Dim _codigo_eva, _codigo_emd As Integer
        'obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        'Try
        '    Dim cbo As DropDownList = sender
        '    Dim row As GridViewRow = CType(cbo.Parent.Parent, GridViewRow)
        '    _codigo_eva = CInt(Me.gvEvaluacion.DataKeys(row.RowIndex).Values("codigo_eva"))
        '    _codigo_emd = CInt(Me.gvEvaluacion.DataKeys(row.RowIndex).Values("codigo_emd"))
        '    obj.AbrirConexion()
        '    If _codigo_emd <> -1 Then
        '        If cbo.SelectedValue > -1 Then
        '            obj.Ejecutar("DEA_EnlaceMoodle_actualizar", _codigo_emd, cbo.SelectedValue, cod_user)
        '        Else
        '            obj.Ejecutar("DEA_EnlaceMoodle_eliminar", _codigo_emd, cod_user)
        '        End If
        '    Else
        '        If cbo.SelectedValue > -1 Then
        '            obj.Ejecutar("DEA_EnlaceMoodle_insertar", _codigo_eva, cbo.SelectedValue, cod_user)
        '        End If
        '    End If
        '    obj.CerrarConexion()
        '    mt_ShowMessage("¡ Se registraron los datos correctamente !", MessageType.Success)
        '    mt_CargarDatos(Session("gc_codigo_cup"))
        'Catch ex As Exception
        '    mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        'End Try
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

    Protected Sub gvEvaluacion_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvEvaluacion.RowCommand
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
        Try
            Dim index As Integer
            index = CInt(e.CommandArgument)
            If e.CommandName = "Enlazar" Then
                'mt_ShowMessage(Session("gc_codigo_cup"), MessageType.Warning)
                obj.AbrirConexion()
                dt = obj.TraerDataTable("DEA_EvaluacionesCurso_listar", "EM", Session("gc_codigo_cup"))
                obj.CerrarConexion()

                mt_CargarCombo(Me.cboMoodle, dt, "codigo_mod", "tarea_mod")
                Session("codigo_ins") = Me.gvEvaluacion.DataKeys(index).Values("codigo_eva")
                Session("codigo_emd") = CInt(Me.gvEvaluacion.DataKeys(index).Values("codigo_emd"))
                dt.Dispose()
            End If

            If e.CommandName = "Desenlazar" Then
                'mt_ShowMessage(Session("gc_codigo_cup"), MessageType.Warning)
                Dim codigo_emd As String
                codigo_emd = gvEvaluacion.DataKeys(index).Values("codigo_emd").ToString
                codigo_emd = IIf(String.IsNullOrEmpty(codigo_emd), "-1", codigo_emd)

                obj.AbrirConexion()
                dt = obj.TraerDataTable("DEA_EnlaceMoodle_eliminar", codigo_emd, cod_user)
                obj.CerrarConexion()

                If dt.Rows.Count > 0 Then
                    mt_ShowMessage(dt.Rows(0).Item(1).ToString, IIf(dt.Rows(0).Item(0).ToString.Equals("1"), MessageType.Success, MessageType.Warning))
                End If
                dt.Dispose()

                mt_CargarDatos(Session("gc_codigo_cup"))
            End If
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub btnEnlazar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Page.RegisterStartupScript("Pop", "<script>openModal();</script>")
    End Sub

    Protected Sub btnGrabar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGrabar.Click
        Dim obj As New ClsConectarDatos
        Dim _codigo_ins, _codigo_emd, _codigo_cup As Integer
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        Try
            _codigo_ins = CInt(Session("codigo_ins"))
            _codigo_emd = CInt(Session("codigo_emd"))
            _codigo_cup = CInt(Session("gc_codigo_cup"))
            obj.AbrirConexion()
            If _codigo_emd <> -1 Then
                If cboMoodle.SelectedValue > -1 Then
                    obj.Ejecutar("DEA_EnlaceMoodle_actualizar", _codigo_emd, cboMoodle.SelectedValue, cod_user)
                Else
                    obj.Ejecutar("DEA_EnlaceMoodle_eliminar", _codigo_emd, cod_user)
                End If
            Else
                If cboMoodle.SelectedValue > -1 Then
                    obj.Ejecutar("DEA_EnlaceMoodle_insertar", _codigo_ins, cboMoodle.SelectedValue, _codigo_cup, Me.cboMoodle.SelectedItem.Text, cod_user)
                End If
            End If
            obj.CerrarConexion()
            mt_ShowMessage("¡ Se registraron los datos correctamente !", MessageType.Success)
            mt_CargarDatos(Session("gc_codigo_cup"))
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
            dt = obj.TraerDataTable("DEA_EvaluacionesCurso_listar", "CE", codigo_cup)
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

    Private Sub mt_ObtenerInfoPersonal()
        Dim dt As New Data.DataTable
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString

        Try
            obj.AbrirConexion()
            dt = obj.TraerDataTable("DEA_CortesCurso_listar", "ID", Session("gc_codigo_cac"), -1, Session("gc_codigo_cup"))
            obj.CerrarConexion()

            If dt.Rows.Count > 0 Then
                Me.lblCarrera.InnerText = "Carrera: " & dt.Rows(0).Item("nombre_Cpf").ToString()
                Me.lblCurso.InnerText = "Asignatura: " & Session("gc_nombre_cur")
                Me.lblDocente.InnerText = dt.Rows(0).Item("docente").ToString()
            End If
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        Finally
            dt.Dispose()
        End Try
    End Sub

#End Region

End Class
