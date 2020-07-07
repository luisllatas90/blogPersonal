﻿'----------------------------------------------------------------------
'Fecha: 30.10.2012
'Usuario: gcastillo
'Motivo: Cambio de URL del servidor de la WebUSAT
'----------------------------------------------------------------------
Partial Class librerianet_personal_frmVistadeHorario
    Inherits System.Web.UI.Page

    Dim codigo_per As Integer
    Dim codigo_pel As Integer

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        codigo_per = Request.QueryString("id")

        'codigo_per = 684 

        'función que devuelva el codigo_pel vigente
        Dim obj As New clsPersonal
        codigo_pel = obj.ConsultarPeridoLaborable
        'Response.Write(codigo_pel)
        If Not IsPostBack Then
            cargarControles()
            CargaLeyenda()

        End If
        lblFechas.Text = obj.ConsultarRangoFechasSemana(Me.cboSemana.SelectedValue)
        consultarVistaHorario()
        consultarListaHorario()
        consultarDatosGenerales()
        ConsultarListaCambiosHorarios()


        Dim ObjCnx As New ClsConectarDatos
        Dim DatosHorasTesis As data.datatable
        ObjCnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        ObjCnx.AbrirConexion()
        DatosHorasTesis = ObjCnx.TraerDataTable("PER_ConsultarPersonalHorasTesis", "TE", codigo_pel, codigo_per)
        ObjCnx.CerrarConexion()

        If DatosHorasTesis.Rows.Count > 0 Then
            Dim id_per As Integer
            id_per = Request.QueryString("id")
            CmdReporteHorasTesis.Enabled = True
            'CmdReporteHorasTesis.Attributes.add("OnClick", "javascript:AbrirPopUp('http://www.usat.edu.pe/rptusat/?/privados/personal/PER_DocentesAvancesTesis&id=" & id_per.ToString & "&codigo_pel=" & codigo_pel.ToString & "&codigo_per=" & codigo_per.ToString & "',500,800,1,1,1,0);return false;")
            CmdReporteHorasTesis.Attributes.add("OnClick", "javascript:AbrirPopUp('//intranet.usat.edu.pe/rptusat/?/privados/personal/PER_DocentesAvancesTesis&id=" & id_per.ToString & "&codigo_pel=" & codigo_pel.ToString & "&codigo_per=" & codigo_per.ToString & "',500,800,1,1,1,0);return false;")
        Else
            CmdReporteHorasTesis.Enabled = False
        End If




    End Sub


    Private Sub ConsultarListaCambiosHorarios()
        Dim obj As New clsPersonal
        Dim dts As New Data.DataTable
        dts = obj.ConsultarListaCambiosHorarios(codigo_per, codigo_pel)
        gvListaCambios.DataSource = dts
        gvListaCambios.DataBind()
    End Sub
    Private Sub cargarControles()
        'Dim obj As New clsPersonal
        'Dim dts As New Data.DataTable
        'Dim dtsEscuela As New Data.DataTable

        'Dim dtsFacultad As New Data.DataTable

        'dts = obj.ConsultarHorasControl()
        'ddlHoraInicio.DataSource = dts
        'ddlHoraInicio.DataTextField = "hora"
        'ddlHoraInicio.DataValueField = "hora"
        'ddlHoraInicio.DataBind()
        'ddlHoraFin.DataSource = dts
        'ddlHoraFin.DataTextField = "hora"
        'ddlHoraFin.DataValueField = "hora"
        'ddlHoraFin.DataBind()
        ''cargo la lista de escuelas
        'dtsEscuela = obj.ConsultarCarreraProfesional()
        'ddlEscuela.DataSource = dtsEscuela
        'ddlEscuela.DataTextField = "nombre_cpf"
        'ddlEscuela.DataValueField = "codigo_cpf"
        'ddlEscuela.DataBind()
        ''cargo la lista de facultades
        'dtsFacultad = obj.ConsultarFacultad()
        'ddlFacultad.DataSource = dtsFacultad
        'ddlFacultad.DataTextField = "nombre_fac"
        'ddlFacultad.DataValueField = "codigo_fac"
        'ddlFacultad.DataBind()

    End Sub
    Private Sub consultarDatosGenerales()
        Dim obj As New clsPersonal
        Dim dts As New Data.DataTable


    End Sub
    Private Sub consultarVistaHorario()
        Dim obj As New clsPersonal
        Dim dts As New Data.DataTable
        dts = obj.ConsultarVistaHorario(codigo_per, codigo_pel, Me.cboSemana.SelectedValue)
        gvVistaHorario.DataSource = dts
        gvVistaHorario.DataBind()

        lblHorasSemanales.Text = obj.ConsultarTotalHorasSemana(codigo_per, codigo_pel, Me.cboSemana.SelectedValue)
    End Sub
    Private Sub consultarListaHorario()
        Dim obj As New clsPersonal
        Dim dts As New Data.DataTable
        dts = obj.ConsultarListaHorario(codigo_per, codigo_pel, Me.cboSemana.SelectedValue)
        gvEditHorario.DataSource = dts
        gvEditHorario.DataBind()
    End Sub



    Protected Sub gvEditHorario_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvEditHorario.RowDataBound
        e.Row.Cells(1).Visible = False
    End Sub



    'Protected Sub gvEditHorario_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles gvEditHorario.RowDeleting
    '    Dim obj As New clsPersonal
    '    obj.EliminarHorarioPersonal(gvEditHorario.Rows(e.RowIndex).Cells(1).Text)
    '    consultarVistaHorario()
    '    consultarListaHorario()
    'End Sub

    Protected Sub gvVistaHorario_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvVistaHorario.RowDataBound
        e.Row.Cells(2).Width = 40
        e.Row.Cells(3).Width = 40
        e.Row.Cells(4).Width = 40
        e.Row.Cells(5).Width = 40
        e.Row.Cells(6).Width = 40
        e.Row.Cells(7).Width = 40
        Select Case e.Row.Cells(2).Text
            Case "I"
                e.Row.Cells(2).BackColor = Drawing.Color.Yellow
            Case "D"
                e.Row.Cells(2).BackColor = Drawing.Color.Green
            Case "E"
                e.Row.Cells(2).BackColor = Drawing.Color.Violet
            Case "A"
                e.Row.Cells(2).BackColor = Drawing.Color.Orange
            Case "P"
                e.Row.Cells(2).BackColor = Drawing.Color.Blue
            Case "T"
                e.Row.Cells(2).BackColor = Drawing.Color.Gray
            Case "F"
                e.Row.Cells(2).BackColor = Drawing.Color.Brown
            Case "G"
                e.Row.Cells(2).BackColor = Drawing.Color.DarkTurquoise
            Case "H"
                e.Row.Cells(2).BackColor = Drawing.Color.Lime
        End Select

        Select Case e.Row.Cells(3).Text
            Case "I"
                e.Row.Cells(3).BackColor = Drawing.Color.Yellow
            Case "D"
                e.Row.Cells(3).BackColor = Drawing.Color.Green
            Case "E"
                e.Row.Cells(3).BackColor = Drawing.Color.Violet
            Case "A"
                e.Row.Cells(3).BackColor = Drawing.Color.Orange
            Case "P"
                e.Row.Cells(3).BackColor = Drawing.Color.Blue
            Case "T"
                e.Row.Cells(3).BackColor = Drawing.Color.Gray
            Case "F"
                e.Row.Cells(3).BackColor = Drawing.Color.Brown
            Case "G"
                e.Row.Cells(3).BackColor = Drawing.Color.DarkTurquoise
            Case "H"
                e.Row.Cells(3).BackColor = Drawing.Color.Lime
        End Select
        Select Case e.Row.Cells(4).Text
            Case "I"
                e.Row.Cells(4).BackColor = Drawing.Color.Yellow
            Case "D"
                e.Row.Cells(4).BackColor = Drawing.Color.Green
            Case "E"
                e.Row.Cells(4).BackColor = Drawing.Color.Violet
            Case "A"
                e.Row.Cells(4).BackColor = Drawing.Color.Orange
            Case "P"
                e.Row.Cells(4).BackColor = Drawing.Color.Blue
            Case "T"
                e.Row.Cells(4).BackColor = Drawing.Color.Gray
            Case "F"
                e.Row.Cells(4).BackColor = Drawing.Color.Brown
            Case "G"
                e.Row.Cells(4).BackColor = Drawing.Color.DarkTurquoise
            Case "H"
                e.Row.Cells(4).BackColor = Drawing.Color.Lime
        End Select
        Select Case e.Row.Cells(5).Text
            Case "I"
                e.Row.Cells(5).BackColor = Drawing.Color.Yellow
            Case "D"
                e.Row.Cells(5).BackColor = Drawing.Color.Green
            Case "E"
                e.Row.Cells(5).BackColor = Drawing.Color.Violet
            Case "A"
                e.Row.Cells(5).BackColor = Drawing.Color.Orange
            Case "P"
                e.Row.Cells(5).BackColor = Drawing.Color.Blue
            Case "T"
                e.Row.Cells(5).BackColor = Drawing.Color.Gray
            Case "F"
                e.Row.Cells(5).BackColor = Drawing.Color.Brown
            Case "G"
                e.Row.Cells(5).BackColor = Drawing.Color.DarkTurquoise
            Case "H"
                e.Row.Cells(5).BackColor = Drawing.Color.Lime
        End Select
        Select Case e.Row.Cells(6).Text
            Case "I"
                e.Row.Cells(6).BackColor = Drawing.Color.Yellow
            Case "D"
                e.Row.Cells(6).BackColor = Drawing.Color.Green
            Case "E"
                e.Row.Cells(6).BackColor = Drawing.Color.Violet
            Case "A"
                e.Row.Cells(6).BackColor = Drawing.Color.Orange
            Case "P"
                e.Row.Cells(6).BackColor = Drawing.Color.Blue
            Case "T"
                e.Row.Cells(6).BackColor = Drawing.Color.Gray
            Case "F"
                e.Row.Cells(6).BackColor = Drawing.Color.Brown
            Case "G"
                e.Row.Cells(6).BackColor = Drawing.Color.DarkTurquoise
            Case "H"
                e.Row.Cells(6).BackColor = Drawing.Color.Lime
        End Select
        Select Case e.Row.Cells(7).Text
            Case "I"
                e.Row.Cells(7).BackColor = Drawing.Color.Yellow
            Case "D"
                e.Row.Cells(7).BackColor = Drawing.Color.Green
            Case "E"
                e.Row.Cells(7).BackColor = Drawing.Color.Violet
            Case "A"
                e.Row.Cells(7).BackColor = Drawing.Color.Orange
            Case "P"
                e.Row.Cells(7).BackColor = Drawing.Color.Blue
            Case "T"
                e.Row.Cells(7).BackColor = Drawing.Color.Gray
            Case "F"
                e.Row.Cells(7).BackColor = Drawing.Color.Brown
            Case "G"
                e.Row.Cells(7).BackColor = Drawing.Color.DarkTurquoise
            Case "H"
                e.Row.Cells(7).BackColor = Drawing.Color.Lime
        End Select
        If e.Row.Cells(0).Text = "08:00" Or e.Row.Cells(0).Text = "16:30" Then
            e.Row.Cells(0).ForeColor = Drawing.Color.Blue
            e.Row.Cells(1).ForeColor = Drawing.Color.Blue
        End If
    End Sub

    Private Sub CargaLeyenda()
        Dim obj As New clsPersonal
        Dim dts As New Data.DataTable

        dts = obj.ConsultaTipoActividadPorAbreviatura("A")
        lblA.Text = dts.Rows(0).Item("descripcion_td").ToString
        lblA.BackColor = System.Drawing.ColorTranslator.FromHtml(dts.Rows(0).Item("color_td").ToString)

        dts = obj.ConsultaTipoActividadPorAbreviatura("D")
        lblD.Text = dts.Rows(0).Item("descripcion_td").ToString
        lblD.BackColor = System.Drawing.ColorTranslator.FromHtml(dts.Rows(0).Item("color_td").ToString)

        'dts = obj.ConsultaTipoActividadPorAbreviatura("G")
        'lblG.Text = dts.Rows(0).Item("descripcion_td").ToString
        'lblG.BackColor = System.Drawing.ColorTranslator.FromHtml(dts.Rows(0).Item("color_td").ToString)

        dts = obj.ConsultaTipoActividadPorAbreviatura("T")
        lblT.Text = dts.Rows(0).Item("descripcion_td").ToString
        lblT.BackColor = System.Drawing.ColorTranslator.FromHtml(dts.Rows(0).Item("color_td").ToString)

        dts = obj.ConsultaTipoActividadPorAbreviatura("I")
        lblI.Text = dts.Rows(0).Item("descripcion_td").ToString
        lblI.BackColor = System.Drawing.ColorTranslator.FromHtml(dts.Rows(0).Item("color_td").ToString)

        dts = obj.ConsultaTipoActividadPorAbreviatura("U")
        lblU.Text = dts.Rows(0).Item("descripcion_td").ToString
        lblU.BackColor = System.Drawing.ColorTranslator.FromHtml(dts.Rows(0).Item("color_td").ToString)

        dts = obj.ConsultaTipoActividadPorAbreviatura("P")
        lblP.Text = dts.Rows(0).Item("descripcion_td").ToString
        lblP.BackColor = System.Drawing.ColorTranslator.FromHtml(dts.Rows(0).Item("color_td").ToString)

        dts = obj.ConsultaTipoActividadPorAbreviatura("H")
        lblH.Text = dts.Rows(0).Item("descripcion_td").ToString
        lblH.BackColor = System.Drawing.ColorTranslator.FromHtml(dts.Rows(0).Item("color_td").ToString)

        'dts = obj.ConsultaTipoActividadPorAbreviatura("F")
        'lblF.Text = dts.Rows(0).Item("descripcion_td").ToString
        'lblF.BackColor = System.Drawing.ColorTranslator.FromHtml(dts.Rows(0).Item("color_td").ToString)

        'dts = obj.ConsultaTipoActividadPorAbreviatura("E")
        'lblE.Text = dts.Rows(0).Item("descripcion_td").ToString
        'lblE.BackColor = System.Drawing.ColorTranslator.FromHtml(dts.Rows(0).Item("color_td").ToString)

        dts = obj.ConsultaTipoActividadPorAbreviatura("C")
        lblC.Text = dts.Rows(0).Item("descripcion_td").ToString
        lblC.BackColor = System.Drawing.ColorTranslator.FromHtml(dts.Rows(0).Item("color_td").ToString)

        dts = obj.ConsultaTipoActividadPorAbreviatura("GR")
        lblGR.Text = dts.Rows(0).Item("descripcion_td").ToString
        lblGR.BackColor = System.Drawing.ColorTranslator.FromHtml(dts.Rows(0).Item("color_td").ToString)

        'dts = obj.ConsultaTipoActividadPorAbreviatura("G")
        'Me.lblGA.Text = dts.Rows(0).Item("descripcion_td").ToString
        'lblGA.BackColor = System.Drawing.ColorTranslator.FromHtml(dts.Rows(0).Item("color_td").ToString)

        'dts = obj.ConsultaTipoActividadPorAbreviatura("GP")
        'lblGP.Text = dts.Rows(0).Item("descripcion_td").ToString
        'lblGP.BackColor = System.Drawing.ColorTranslator.FromHtml(dts.Rows(0).Item("color_td").ToString)

        dts = obj.ConsultaTipoActividadPorAbreviatura("CP")
        lblCP.Text = dts.Rows(0).Item("descripcion_td").ToString
        lblCP.BackColor = System.Drawing.ColorTranslator.FromHtml(dts.Rows(0).Item("color_td").ToString)
    End Sub

    Protected Sub cboSemana_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboSemana.SelectedIndexChanged

    End Sub
End Class
