﻿Imports iTextSharp.text.html

Partial Class administrativo_gestion_educativa_frmGrupoAsignacionAlumno
    Inherits System.Web.UI.Page

#Region "Variables"

    Private cnx As ClsConectarDatos
    Private cod_user As Integer, cod_ctf As Integer, cod_test As Integer
    Private mo_RepoAdmision As New ClsAdmision
    Private oeGrupo As e_GrupoAdmisionVirtual, odGrupo As d_GrupoAdmisionVirtual
    Private oeGrupoAlu As e_GrupoAdmisionVirtual_Alumno, odGrupoAlu As d_GrupoAdmisionVirtual_Alumno
    Private _fuente As String = Server.MapPath(".") & "/segoeui.ttf"

    Public Enum MessageType
        Success
        [Error]
        Info
        Warning
    End Enum

#End Region

#Region "Eventos"

    'Sub New()
    '    If cnx Is Nothing Then
    '        cnx = New ClsConectarDatos
    '        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
    '    End If
    'End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If (Session("id_per") Is Nothing) Then
                Response.Redirect("../../../sinacceso.html")
            End If
            cod_user = Session("id_per")
            cod_ctf = Request.QueryString("ctf")
            cod_test = Request.QueryString("mod")
            'If Not IsPostBack Then
            '    'mt_CargarCentroCosto()
            '    mt_CargarGrupoVirtual()
            '    Me.btnAgregar.visible = False
            '    Me.chkDisponible.visible = False
            '    Me.btnGenerarDoc.visible = False
            '    Me.btnExportar.visible = False
            '    Me.divBusqueda.visible = False
            'End If

            Me.gvGrupoAlumno.DataSource = CType(Session("adm_dtAlumnos"), Data.DataTable)
            Me.gvGrupoAlumno.DataBind()

            mt_ExportToExcel()

        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", ""), MessageType.Error)
        End Try
    End Sub

    'Protected Sub cboCentroCosto_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboCentroCosto.SelectedIndexChanged
    '    Try

    '        mt_CargarGrupoVirtual(IIf(Me.cboCentroCosto.SelectedValue = -1, 0, Me.cboCentroCosto.SelectedValue))
    '        cboGrupo_SelectedIndexChanged(Nothing, Nothing)

    '    Catch ex As Exception
    '        mt_ShowMessage(ex.Message.Replace("'", ""), MessageType.Error)
    '    End Try
    'End Sub

    'Protected Sub cboGrupo_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboGrupo.SelectedIndexChanged
    '    Try
    '        Me.cboestado.selectedvalue = iif(Me.cbogrupo.selectedvalue = -1, -1, 0)
    '        Me.btnAgregar.visible = IIf(Me.cboGrupo.SelectedValue = -1, False, True)
    '        mt_CargarCarreraProfesional(IIf(Me.cboGrupo.SelectedValue = -1, 0, Me.cboGrupo.SelectedValue))
    '        Me.divBusqueda.visible = IIf(Me.cboEstado.SelectedValue = 0, True, False)
    '        If Me.divbusqueda.visible Then
    '            mt_CargarDatos(IIf(Me.cboGrupo.SelectedValue = -1, 0, Me.cboGrupo.SelectedValue), Me.cboEstado.selectedvalue, Me.cboEscuela.selectedvalue)
    '        Else
    '            mt_CargarDatos(IIf(Me.cboGrupo.SelectedValue = -1, 0, Me.cboGrupo.SelectedValue), Me.cboEstado.selectedvalue, -1)
    '        End If
    '        Me.chkDisponible.visible = IIf(Me.cboGrupo.SelectedValue = -1, False, True)
    '        Me.btnGenerarDoc.visible = IIf(Me.cboestado.SelectedValue = 1, True, False)
    '        Me.btnexportar.visible = IIf(Me.cboestado.SelectedValue = 1, True, False)
    '        Me.chkDisponible.Checked = False
    '    Catch ex As Exception
    '        mt_ShowMessage(ex.Message.Replace("'", ""), MessageType.Error)
    '    End Try
    'End Sub

    'Protected Sub cboEstado_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboEstado.SelectedIndexChanged
    '    Try
    '        'mt_CargarDatos(IIf(Me.cboGrupo.SelectedValue = -1, 0, Me.cboGrupo.SelectedValue), Me.cboEstado.selectedvalue)
    '        Me.btnAgregar.visible = IIf(Me.cboEstado.SelectedValue = 1, False, True)
    '        mt_CargarCarreraProfesional(IIf(Me.cboGrupo.SelectedValue = -1, 0, Me.cboGrupo.SelectedValue))
    '        Me.divBusqueda.visible = IIf(Me.cboEstado.SelectedValue = 0, True, False)
    '        If Me.divbusqueda.visible Then
    '            mt_CargarDatos(IIf(Me.cboGrupo.SelectedValue = -1, 0, Me.cboGrupo.SelectedValue), Me.cboEstado.selectedvalue, Me.cboEscuela.selectedvalue)
    '        Else
    '            mt_CargarDatos(IIf(Me.cboGrupo.SelectedValue = -1, 0, Me.cboGrupo.SelectedValue), Me.cboEstado.selectedvalue, -1)
    '        End If
    '        Me.chkDisponible.Checked = False
    '        Me.chkDisponible.visible = IIf(Me.cboEstado.SelectedValue = 1, False, True)
    '        Me.btnGenerarDoc.visible = IIf(Me.cboestado.SelectedValue = 1, True, False)
    '        Me.btnexportar.visible = IIf(Me.cboestado.SelectedValue = 1, True, False)
    '    Catch ex As Exception
    '        mt_ShowMessage(ex.Message.Replace("'", ""), MessageType.Error)
    '    End Try
    'End Sub

    'Protected Sub gvGrupoAlumno_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvGrupoAlumno.RowCommand
    '    Dim index As Integer, _codigo_gva As Integer, _codigo_alu As Integer
    '    Dim dt As New System.Data.DataTable
    '    Try
    '        index = CInt(e.CommandArgument)
    '        If index >= 0 Then
    '            _codigo_gva = Me.gvgrupoalumno.datakeys(index).values("codigo_gva")
    '            _codigo_alu = Me.gvgrupoalumno.datakeys(index).values("codigo_alu")
    '            oeGrupoAlu = New e_GrupoAdmisionVirtual_Alumno : odGrupoAlu = New d_GrupoAdmisionVirtual_Alumno
    '            If e.CommandName = "Agregar" Then
    '                oeGrupoAlu.codigo_gru = Me.cboGrupo.selectedvalue : oeGrupoAlu.codigo_alu = _codigo_alu : oeGrupoAlu.codigo_per = cod_user
    '                dt = odGrupoAlu.fc_AgregarGrupoAdmisionVirtual_Alumno(oeGrupoAlu)
    '                If dt.Rows.Count > 0 Then
    '                    mt_ShowMessage("¡ Se agregó correctamente el alumno !", MessageType.Success)
    '                    cboEstado_SelectedIndexChanged(Nothing, Nothing)
    '                Else
    '                    mt_ShowMessage("¡ Ocurrio un error en el registro !", MessageType.Error)
    '                End If
    '            ElseIf e.CommandName = "Quitar" Then
    '                oeGrupoAlu.codigo_gva = _codigo_gva : oeGrupoAlu.codigo_per = cod_user
    '                dt = odGrupoAlu.fc_QuitarGrupoAdmisionVirtual_Alumno(oeGrupoAlu)
    '                If dt.Rows.Count > 0 Then
    '                    mt_ShowMessage("¡ Se quitó correctamente el alumno !", MessageType.Success)
    '                    cboEstado_SelectedIndexChanged(Nothing, Nothing)
    '                Else
    '                    mt_ShowMessage("¡ Ocurrio un error en el registro !", MessageType.Error)
    '                End If
    '            End If
    '        End If
    '    Catch ex As Exception
    '        mt_ShowMessage(ex.Message.Replace("'", ""), MessageType.Error)
    '    End Try
    'End Sub

    'Protected Sub btnAgregar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAgregar.Click
    '    Dim dt As New System.Data.DataTable
    '    Dim _codigo_alu As String = ""
    '    Dim _cant As Integer = 0, _disponible As Integer = 0
    '    Try
    '        For i As Integer = 0 To Me.gvGrupoAlumno.Rows.Count - 1
    '            Dim chk As CheckBox = Me.gvGrupoAlumno.Rows(i).FindControl("chkSelect")
    '            If chk.Checked Then
    '                If _codigo_alu.Length > 0 Then _codigo_alu &= ","
    '                _codigo_alu &= Me.gvGrupoAlumno.datakeys(i).values("codigo_alu")
    '                _cant += 1
    '            End If
    '        Next
    '        If _codigo_alu = "" Then
    '            mt_ShowMessage("¡ Seleccione al menos un postulante !", MessageType.Warning)
    '            Exit Sub
    '        End If
    '        _disponible = Session("adm_disponible")
    '        If _disponible = 0 Then
    '            mt_ShowMessage("¡ El Grupo esta completo !", MessageType.Error)
    '        End If
    '        If _disponible < _cant Then
    '            mt_ShowMessage("¡ Como máximo puede seleccionar " & _disponible & " postulantes !", MessageType.Warning)
    '            Exit Sub
    '        End If
    '        oeGrupoAlu = New e_GrupoAdmisionVirtual_Alumno : odGrupoAlu = New d_GrupoAdmisionVirtual_Alumno
    '        oeGrupoAlu.codigo_gru = Me.cboGrupo.selectedvalue : oeGrupoAlu.codigo_alu = _codigo_alu : oeGrupoAlu.codigo_per = cod_user
    '        dt = odGrupoAlu.fc_AgregarGrupoAdmisionVirtual_Alumno(oeGrupoAlu)
    '        If dt.Rows.Count > 0 Then
    '            mt_ShowMessage("¡ Se agregaron correctamente " & _cant & " postulantes !", MessageType.Success)
    '            cboEstado_SelectedIndexChanged(Nothing, Nothing)
    '        Else
    '            mt_ShowMessage("¡ Ocurrio un error en el registro !", MessageType.Error)
    '        End If
    '    Catch ex As Exception
    '        mt_ShowMessage(ex.Message.Replace("'", ""), MessageType.Error)
    '    End Try
    'End Sub

    'Protected Sub chkDisponible_ChekedChanged(ByVal sender As Object, ByVal e As System.EventArgs)
    '    Dim _cant As Integer = 0, _disponible As Integer = 0
    '    Try
    '        If chkDisponible.checked Then
    '            _disponible = Session("adm_disponible")
    '            If _disponible > 0 Then
    '                For i As Integer = 0 To Me.gvGrupoAlumno.Rows.Count - 1
    '                    If _disponible > _cant Then
    '                        Dim chk As CheckBox = Me.gvGrupoAlumno.Rows(i).FindControl("chkSelect")
    '                        If chk.visible Then
    '                            chk.Checked = True
    '                            _cant += 1
    '                        End If
    '                    Else
    '                        Exit Sub
    '                    End If
    '                Next
    '            Else
    '                mt_ShowMessage("¡ El Grupo esta completo !", MessageType.Error)
    '            End If
    '        Else
    '            For i As Integer = 0 To Me.gvGrupoAlumno.Rows.Count - 1
    '                Dim chk As CheckBox = Me.gvGrupoAlumno.Rows(i).FindControl("chkSelect")
    '                If chk.visible Then
    '                    chk.Checked = False
    '                End If
    '            Next
    '        End If
    '    Catch ex As Exception
    '        mt_ShowMessage(ex.Message.Replace("'", ""), MessageType.Error)
    '    End Try
    'End Sub

    'Protected Sub btnGenerarDoc_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGenerarDoc.Click
    '    Dim memory As New System.IO.MemoryStream
    '    Dim dtGru, dtPos As New System.Data.DataTable
    '    Dim _grupo, _ambiente, _nro_pos As String
    '    Try

    '        oeGrupo = New e_GrupoAdmisionVirtual : odGrupo = New d_GrupoAdmisionVirtual
    '        oeGrupo.tipoOperacion = "GN" : oeGrupo.codigo_gru = Me.cboGrupo.SelectedValue
    '        dtGru = odGrupo.fc_ListarGrupoAdmisionVirtual(oeGrupo)

    '        If dtGru.Rows.Count > 0 Then
    '            _grupo = dtGru.Rows(0).Item(1)
    '            _ambiente = dtGru.Rows(0).Item(5) & " - " & dtGru.Rows(0).Item(2) & iif(dtGru.Rows(0).Item(4) = 0, "", " (VIRTUAL)")
    '            _nro_pos = dtGru.Rows(0).Item(6)
    '        End If

    '        oeGrupoAlu = New e_GrupoAdmisionVirtual_Alumno : odGrupoAlu = New d_GrupoAdmisionVirtual_Alumno
    '        oeGrupoAlu.codigo_gru = Me.cboGrupo.SelectedValue : oeGrupoAlu.codigo_alu = -1 : oeGrupoAlu.tipoOperacion = "LT"
    '        dtPos = odGrupoAlu.fc_ListarGrupoAdmisionVirtual_Alumno(oeGrupoAlu)

    '        Dim pdfDoc As New iTextSharp.text.Document(iTextSharp.text.PageSize.A4)
    '        Dim pdfWrite As iTextSharp.text.pdf.PdfWriter = iTextSharp.text.pdf.PdfWriter.GetInstance(pdfDoc, memory)

    '        pdfDoc.Open()

    '        Dim pdfTable As iTextSharp.text.pdf.PdfPTable = New iTextSharp.text.pdf.PdfPTable(1)
    '        pdfTable.WidthPercentage = 98.0F
    '        pdfTable.DefaultCell.Border = 0

    '        Dim table As iTextSharp.text.pdf.PdfPTable = New iTextSharp.text.pdf.PdfPTable(10)
    '        table.WidthPercentage = 100.0F
    '        table.SetWidths(New Single() {5.0F, 15.0F, 10.0F, 10.0F, 10.0F, 10.0F, 10.0F, 10.0F, 10.0F, 10.0F})
    '        table.DefaultCell.Border = 0

    '        table.AddCell(fc_CeldaTexto(" ", 10.0F, 1, 0, 2, 1, 1))
    '        table.AddCell(fc_CeldaTexto("LISTADO DE POSTULANTES PARA EVALUACIÓN", 12.0F, 1, 2, 6, 1, 1))
    '        table.AddCell(fc_CeldaTexto(" ", 10.0F, 1, 0, 2, 1, 1))

    '        table.AddCell(fc_CeldaTexto(" " & Environment.NewLine, 10.0F, 1, 0, 10, 1, 1))

    '        table.AddCell(fc_CeldaTexto("GRUPO EVALUACIÓN:", 9.0F, 1, 0, 2, 1, 0))
    '        table.AddCell(fc_CeldaTexto(_grupo, 9.0F, 0, 2, 4, 1, 0))
    '        table.AddCell(fc_CeldaTexto(" ", 9.0F, 1, 0, 4, 1, 0))

    '        table.AddCell(fc_CeldaTexto("AMBIENTE:", 9.0F, 1, 0, 2, 1, 0))
    '        table.AddCell(fc_CeldaTexto(_ambiente, 9.0F, 0, 2, 4, 1, 0))
    '        table.AddCell(fc_CeldaTexto("N° POSTULANTES:", 9.0F, 1, 0, 2, 1, 0))
    '        table.AddCell(fc_CeldaTexto(_nro_pos, 9.0F, 0, 2, 2, 1, 1))

    '        table.AddCell(fc_CeldaTexto(" " & Environment.NewLine, 10.0F, 1, 0, 10, 1, 1))

    '        table.AddCell(fc_CeldaTexto("N°", 9.0F, 1, 15, 1, 1, 0))
    '        table.AddCell(fc_CeldaTexto("D.N.I.", 9.0F, 1, 15, 1, 1, 1))
    '        table.AddCell(fc_CeldaTexto("COD. UNIV.", 9.0F, 1, 15, 2, 1, 1))
    '        table.AddCell(fc_CeldaTexto("POSTULANTE", 9.0F, 1, 15, 5, 1, 1))
    '        table.AddCell(fc_CeldaTexto("ESC. PROF.", 9.0F, 1, 15, 1, 1, 1))

    '        If dtPos.Rows.Count > 0 Then
    '            For i As Integer = 0 To dtPos.Rows.Count - 1
    '                table.AddCell(fc_CeldaTexto(i + 1, 8.0F, 0, 15, 1, 1, 1))
    '                table.AddCell(fc_CeldaTexto(dtPos.Rows(i).Item(9), 8.0F, 0, 15, 1, 1, 1))
    '                table.AddCell(fc_CeldaTexto(dtPos.Rows(i).Item(5), 8.0F, 0, 15, 2, 1, 1))
    '                table.AddCell(fc_CeldaTexto(dtPos.Rows(i).Item(4), 8.0F, 0, 15, 5, 1, 0))
    '                table.AddCell(fc_CeldaTexto(dtPos.Rows(i).Item(8), 8.0F, 0, 15, 1, 1, 1))
    '            Next
    '        End If

    '        pdfTable.AddCell(table)

    '        pdfDoc.Add(pdfTable)

    '        pdfDoc.Close()

    '        Dim bytes() As Byte = memory.ToArray
    '        memory.Close()

    '        Response.Clear()
    '        Response.Buffer = False
    '        Response.Charset = ""
    '        Response.Cache.SetCacheability(HttpCacheability.NoCache)
    '        Response.ContentType = "application/pdf"
    '        Response.AddHeader("content-disposition", "attachment;filename=grupoevaluacion")
    '        Response.AppendHeader("Content-Length", bytes.Length.ToString())
    '        Response.BinaryWrite(bytes)
    '        Response.End()

    '    Catch ex As Exception
    '        mt_ShowMessage(ex.Message.Replace("'", ""), MessageType.Error)
    '    End Try
    'End Sub

    'Protected Sub cboEscuela_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboEscuela.SelectedIndexChanged
    '    Try
    '        If Me.divbusqueda.visible Then
    '            mt_CargarDatos(IIf(Me.cboGrupo.SelectedValue = -1, 0, Me.cboGrupo.SelectedValue), Me.cboEstado.selectedvalue, Me.cboEscuela.selectedvalue)
    '        Else
    '            mt_CargarDatos(IIf(Me.cboGrupo.SelectedValue = -1, 0, Me.cboGrupo.SelectedValue), Me.cboEstado.selectedvalue, -1)
    '        End If
    '    Catch ex As Exception
    '        mt_ShowMessage(ex.Message.Replace("'", ""), MessageType.Error)
    '    End Try
    'End Sub

    'Protected Sub btnExportar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExportar.Click

    'End Sub

#End Region

#Region "Metodos"

    Protected Sub mt_ShowMessage(ByVal Message As String, ByVal type As MessageType, Optional ByVal modal As Boolean = False)
        'If modal Then
        '    Me.divAlertModal.Visible = True
        '    Me.lblMensaje.InnerText = Message
        '    Me.validar.Value = "0"
        '    updMensaje.Update()
        'Else
        Page.RegisterStartupScript("Mensaje", "<script>ShowMessage('" & Message & "','" & type.ToString & "');</script>")
        'End If
    End Sub

    'Private Sub mt_CargarCentroCosto()
    '    ClsFunciones.LlenarListas(Me.cboCentroCosto, mo_RepoAdmision.ListarCentroCosto(cod_ctf, cod_user, cod_test), "codigo_Cco", "Nombre", "-- Seleccione --")
    'End Sub

    'Private Sub mt_CargarGrupoVirtual()
    '    oeGrupo = New e_GrupoAdmisionVirtual : odGrupo = New d_GrupoAdmisionVirtual
    '    oeGrupo.tipoOperacion = "LT"
    '    ClsFunciones.LlenarListas(Me.cboGrupo, odGrupo.fc_ListarGrupoAdmisionVirtual(oeGrupo), "codigo_gru", "descripcion_gru", "-- Seleccione --")
    'End Sub

    'Private Sub mt_CargarDatos(ByVal _codigo_gru As Integer, ByVal _estado As Integer, ByVal _codigo_cpf As Integer)
    '    Try
    '        Dim dt, dt2 As New System.Data.DataTable
    '        oeGrupoAlu = New e_GrupoAdmisionVirtual_Alumno : odGrupoAlu = New d_GrupoAdmisionVirtual_Alumno
    '        oeGrupoAlu.codigo_gru = _codigo_gru : oeGrupoAlu.codigo_alu = -1
    '        dt = odGrupoAlu.fc_ListarGrupoAdmisionVirtual_Alumno(oeGrupoAlu)
    '        If dt.Rows.Count > 0 Then
    '            If _estado <> -1 Then
    '                Dim dv As Data.DataView
    '                If _codigo_cpf = -1 Then
    '                    dv = New Data.DataView(dt, iif(_estado = 0, "codigo_gva = -1", "codigo_gva <> -1"), "", Data.DataViewRowState.CurrentRows)
    '                Else
    '                    dv = New Data.DataView(dt, _
    '                                           iif(_estado = 0, "codigo_gva = -1 AND codigo_Cpf = " & _codigo_cpf, "codigo_gva <> -1 AND codigo_Cpf = " & _codigo_cpf), _
    '                                           "", _
    '                                           Data.DataViewRowState.CurrentRows)
    '                End If
    '                dt = dv.ToTable
    '            End If
    '            Me.gvGrupoAlumno.datasource = dt
    '        Else
    '            Me.gvGrupoAlumno.datasource = Nothing
    '        End If
    '        Me.gvGrupoAlumno.databind()

    '        Dim _disponible As Integer = 0
    '        oeGrupo = New e_GrupoAdmisionVirtual : odGrupo = New d_GrupoAdmisionVirtual
    '        oeGrupo.tipoOperacion = "GN" : oeGrupo.codigo_gru = _codigo_gru
    '        dt2 = odGrupo.fc_ListarGrupoAdmisionVirtual(oeGrupo)
    '        If dt2.Rows.Count > 0 Then
    '            Me.txtAmbiente.text = dt2.Rows(0).Item(5) & " - " & dt2.Rows(0).Item(2) & iif(dt2.Rows(0).Item(4) = 0, "", " (VIRTUAL)")
    '            Me.txtcapacidad.text = dt2.Rows(0).Item(3)
    '            _disponible = dt2.Rows(0).Item(3) - dt2.Rows(0).Item(6)
    '            Me.txtdisponible.text = _disponible
    '        Else
    '            Me.txtAmbiente.text = ""
    '            Me.txtcapacidad.text = ""
    '            Me.txtdisponible.text = ""
    '            _disponible = 0
    '        End If
    '        Session("adm_disponible") = _disponible
    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Sub

    'Private Sub mt_CargarCarreraProfesional(ByVal _codigo_gru As Integer)
    '    oeGrupoAlu = New e_GrupoAdmisionVirtual_Alumno : odGrupoAlu = New d_GrupoAdmisionVirtual_Alumno
    '    oeGrupoAlu.codigo_gru = _codigo_gru : oeGrupoAlu.codigo_alu = -1 : oeGrupoAlu.tipoOperacion = "CB"
    '    ClsFunciones.LlenarListas(Me.cboEscuela, odGrupoAlu.fc_ListarGrupoAdmisionVirtual_Alumno(oeGrupoAlu), "codigo_Cpf", "nombre_Cpf", "-- Todas las Carreras --")
    'End Sub

    Private Sub mt_ExportToExcel()
        Dim attachment As String = "attachment; filename=grupoevaluacion.xls"
        Response.ClearContent()
        Response.AddHeader("content-disposition", attachment)
        Response.ContentType = "application/ms-excel"
        Dim sw As io.StringWriter = New io.StringWriter()
        Dim htw As HtmlTextWriter = New HtmlTextWriter(sw)
        Me.gvGrupoAlumno.RenderControl(htw)
        Response.Write(sw.ToString())
        Response.[End]()
    End Sub

#End Region

#Region "Funciones"

    ''' <summary>
    ''' Función para crear una celda tipo texto con más atributos
    ''' </summary>
    ''' <param name="_text">Contenido de la Celda</param>
    ''' <param name="_size">Tamaño de letra del contenido de la celda</param>
    ''' <param name="_style">Estilo del Contenido de la Celda. 0: NORMAL, 1: BOLD, 2: ITALIC, 3: BOLDITALIC, 4: UNDERLINE</param>
    ''' <param name="_border">Borde de la Celda. 0: NO_BORDER, 1: TOP_BORDER , 2: BOTTON_BORDER, 4: LEFT_BORDER, 8: RIGHT_BORDER, 15: FULL_BORDER </param>
    ''' <param name="_colspan">Cantidad de Columnas</param>
    ''' <param name="_rowspan">Cantidad de Filas</param>
    ''' <param name="_haligment">Alineacion horizontal del contenido de la celda. 0: LEFT, 1: CENTER, 2: RIGHT, 4: TOP</param>
    ''' <param name="_valigment">Alineacion Vertical. 5: MIDDLE</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    'Private Function fc_CeldaTexto(ByVal _text As String, ByVal _size As Single, ByVal _style As Integer, ByVal _border As Integer, _
    '                             ByVal _colspan As Integer, ByVal _rowspan As Integer, ByVal _haligment As Integer, _
    '                             Optional ByVal _valigment As Integer = -1, Optional ByVal _padding As Integer = 6, _
    '                             Optional ByVal _backgroundcolor As String = "") As iTextSharp.text.pdf.PdfPCell
    '    Dim celdaITC As iTextSharp.text.pdf.PdfPCell
    '    Dim fontITC As iTextSharp.text.Font
    '    'fontITC = New iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, _size, _style)
    '    Dim segoe As iTextSharp.text.pdf.BaseFont = iTextSharp.text.pdf.BaseFont.CreateFont(_fuente, iTextSharp.text.pdf.BaseFont.IDENTITY_H, iTextSharp.text.pdf.BaseFont.EMBEDDED)
    '    fontITC = New iTextSharp.text.Font(segoe, _size, _style)
    '    celdaITC = New iTextSharp.text.pdf.PdfPCell(New iTextSharp.text.Phrase(_text, fontITC))
    '    celdaITC.Border = _border
    '    celdaITC.Colspan = _colspan
    '    celdaITC.Rowspan = _rowspan
    '    celdaITC.HorizontalAlignment = _haligment
    '    celdaITC.VerticalAlignment = _valigment
    '    celdaITC.Padding = _padding
    '    If _backgroundcolor <> "" Then celdaITC.BackgroundColor = New iTextSharp.text.BaseColor(System.Drawing.Color.FromName(_backgroundcolor))
    '    'celdaITC.SetLeading(0.0F, 1.15F)
    '    Return celdaITC
    'End Function


#End Region

End Class
