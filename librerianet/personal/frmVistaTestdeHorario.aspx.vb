Partial Class personal_frmVistaTestdeHorario
    Inherits System.Web.UI.Page

    Dim codigo_per As Integer
    Dim codigo_pel As Integer
    Dim codigo_cac As Integer


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try
            codigo_per = Request.QueryString("id")
            codigo_cac = Request.QueryString("codigo_cac")
            'Response.Write("codigo_per: " & codigo_per)
            'Response.Write("<br/>")
            'Response.Write("codigo_cac: " & codigo_cac)

            'función que devuelve el codigo_pel vigente
            Dim obj As New clsPersonal
            'codigo_pel = obj.ConsultarPeridoLaborable                                                              'comentado el 13.09.2013
            Me.codigo_pel = obj.ConsultarPeridoLaborable_SegunCicloAcademico(Request.QueryString("codigo_cac"))     'agregado el 13.09.2013 xDguevara
            Me.ddlSemana.Enabled = obj.EsCCSalud(codigo_per)

            If Not IsPostBack Then
                CargaLeyenda()
                consultarVistaHorario()
                consultarListaHorario()
                RangoFechasSemanas()
                cargarControles()
            End If
            'lblFechas.Text = obj.ConsultarRangoFechasSemana(Me.ddlSemana.SelectedValue)        
            ConsultarListaCambiosHorarios()
            verifica_registro_horario_personal()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try

    End Sub

    Private Sub verifica_registro_horario_personal()
        Try
            Dim obj As New clsPersonal
            Dim dts As New Data.DataTable
            dts = obj.verifica_registro_horario_personal(Request.QueryString("id"), Request.QueryString("codigo_cac"))
            If dts.Rows.Count > 0 Then
                pnlMensaje.Visible = False
                pnlHorario.Visible = True
            Else
                pnlMensaje.Visible = True
                pnlHorario.Visible = False
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub cargarControles()
        Dim obj As New clsPersonal
        Dim dts As New Data.DataTable
        Dim dtsSemanas As New Data.DataTable

        'Carga el dropdowlist de las semanas
        If ddlSemana.Enabled = True Then
            dtsSemanas = obj.ConsultarTotalSemanas(codigo_pel)
            'Incluye semestre
            If dtsSemanas.Rows.Count > 0 Then
                ddlSemana.DataSource = dtsSemanas
                ddlSemana.DataTextField = "Semana"
                ddlSemana.DataValueField = "numeroSemana_sec"
                ddlSemana.DataBind()
                'Else
                '    ddlSemana.Items(0).Text = "Semestre"
            End If
        Else
            ddlSemana.Items(0).Text = "Semestral"
        End If
    End Sub
    
    Private Sub consultarListaHorario()
        Dim obj As New clsPersonal
        Dim dts As New Data.DataTable

        dts = obj.ConsultarListaHorario_v2(codigo_per, codigo_pel, ddlSemana.SelectedValue)
        gvEditHorario.DataSource = dts
        gvEditHorario.DataBind()
    End Sub

    

    Private Sub consultarVistaHorario()
        Dim obj As New clsPersonal
        Dim dts As New Data.DataTable

        '## Linea comentada el 28.02.2012. Esta linea estaba cargando la vista del horario y no mostraba las horas lectivas
        'dts = obj.ConsultarVistaHorario_v3(codigo_per, codigo_pel, Me.ddlSemana.SelectedValue)

        '## La linea anterios la cambie por esta, asi como las demas páginas de horarios el 28.02.2012. xDguevara
        dts = obj.ConsultarVistaHorario_v2(codigo_per, codigo_pel, Me.ddlSemana.SelectedValue)

        gvVistaHorario.DataSource = dts
        gvVistaHorario.DataBind()

        lblHorasSemanales.Text = obj.ConsultarTotalHorasSemana_v2(codigo_per, codigo_pel, Me.ddlSemana.SelectedValue)
        lblHorasMensuales.Text = obj.TotalHorasMes_v2(codigo_per, codigo_pel)
    End Sub

    Protected Sub gvVistaHorario_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvVistaHorario.RowDataBound
        e.Row.Cells(2).Width = 40
        e.Row.Cells(3).Width = 40
        e.Row.Cells(4).Width = 40
        e.Row.Cells(5).Width = 40
        e.Row.Cells(6).Width = 40
        e.Row.Cells(7).Width = 40
        Dim obj As New clsPersonal
        Dim dts As New Data.DataTable
        For i As Integer = 2 To 7            
            Select Case Left(e.Row.Cells(i).Text, 2).Trim
                Case "A"
                    dts = obj.ConsultaTipoActividadPorAbreviatura("A")
                    Dim color As String = dts.Rows(0).Item("color_td")
                    e.Row.Cells(i).BackColor = System.Drawing.ColorTranslator.FromHtml(color)
                Case "D"
                    dts = obj.ConsultaTipoActividadPorAbreviatura("D")
                    Dim color As String = dts.Rows(0).Item("color_td")
                    e.Row.Cells(i).BackColor = System.Drawing.ColorTranslator.FromHtml(color)

                Case "G"
                    dts = obj.ConsultaTipoActividadPorAbreviatura("G")
                    Dim color As String = dts.Rows(0).Item("color_td")
                    e.Row.Cells(i).BackColor = System.Drawing.ColorTranslator.FromHtml(color)

                Case "T"
                    dts = obj.ConsultaTipoActividadPorAbreviatura("T")
                    Dim color As String = dts.Rows(0).Item("color_td")
                    e.Row.Cells(i).BackColor = System.Drawing.ColorTranslator.FromHtml(color)
                Case "I"
                    dts = obj.ConsultaTipoActividadPorAbreviatura("I")
                    Dim color As String = dts.Rows(0).Item("color_td")
                    e.Row.Cells(i).BackColor = System.Drawing.ColorTranslator.FromHtml(color)
                Case "U"
                    dts = obj.ConsultaTipoActividadPorAbreviatura("U")
                    Dim color As String = dts.Rows(0).Item("color_td")
                    e.Row.Cells(i).BackColor = System.Drawing.ColorTranslator.FromHtml(color)
                Case "P"
                    dts = obj.ConsultaTipoActividadPorAbreviatura("P")
                    Dim color As String = dts.Rows(0).Item("color_td")
                    e.Row.Cells(i).BackColor = System.Drawing.ColorTranslator.FromHtml(color)
                Case "H"
                    dts = obj.ConsultaTipoActividadPorAbreviatura("H")
                    Dim color As String = dts.Rows(0).Item("color_td")
                    e.Row.Cells(i).BackColor = System.Drawing.ColorTranslator.FromHtml(color)

                Case "F"
                    dts = obj.ConsultaTipoActividadPorAbreviatura("F")
                    Dim color As String = dts.Rows(0).Item("color_td")
                    e.Row.Cells(i).BackColor = System.Drawing.ColorTranslator.FromHtml(color)

                Case "E"
                    dts = obj.ConsultaTipoActividadPorAbreviatura("E")
                    Dim color As String = dts.Rows(0).Item("color_td")
                    e.Row.Cells(i).BackColor = System.Drawing.ColorTranslator.FromHtml(color)

                Case "C"
                    dts = obj.ConsultaTipoActividadPorAbreviatura("C")
                    Dim color As String = dts.Rows(0).Item("color_td")
                    e.Row.Cells(i).BackColor = System.Drawing.ColorTranslator.FromHtml(color)
                Case "GR"
                    dts = obj.ConsultaTipoActividadPorAbreviatura("GR")
                    Dim color As String = dts.Rows(0).Item("color_td")
                    e.Row.Cells(i).BackColor = System.Drawing.ColorTranslator.FromHtml(color)

                Case "GA"
                    dts = obj.ConsultaTipoActividadPorAbreviatura("GA")
                    Dim color As String = dts.Rows(0).Item("color_td")
                    e.Row.Cells(i).BackColor = System.Drawing.ColorTranslator.FromHtml(color)
                Case "GP"
                    dts = obj.ConsultaTipoActividadPorAbreviatura("GP")
                    Dim color As String = dts.Rows(0).Item("color_td")
                    e.Row.Cells(i).BackColor = System.Drawing.ColorTranslator.FromHtml(color)

                Case "R"
                    dts = obj.ConsultaTipoActividadPorAbreviatura("R")
                    Dim color As String = dts.Rows(0).Item("color_td")
                    e.Row.Cells(i).BackColor = System.Drawing.ColorTranslator.FromHtml(color)

                    'Agregado 22/08/2011
                Case "CP"
                    dts = obj.ConsultaTipoActividadPorAbreviatura("CP")
                    Dim color As String = dts.Rows(0).Item("color_td")
                    e.Row.Cells(i).BackColor = System.Drawing.ColorTranslator.FromHtml(color)
                    'Agregado 24/11/2011
                Case "CA"
                    dts = obj.ConsultaTipoActividadPorAbreviatura("CA")
                    Dim color As String = dts.Rows(0).Item("color_td")
                    e.Row.Cells(i).BackColor = System.Drawing.ColorTranslator.FromHtml(color)
            End Select
        Next

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

        dts = obj.ConsultaTipoActividadPorAbreviatura("C")
        lblC.Text = dts.Rows(0).Item("descripcion_td").ToString
        lblC.BackColor = System.Drawing.ColorTranslator.FromHtml(dts.Rows(0).Item("color_td").ToString)

        dts = obj.ConsultaTipoActividadPorAbreviatura("GR")
        lblGR.Text = dts.Rows(0).Item("descripcion_td").ToString
        lblGR.BackColor = System.Drawing.ColorTranslator.FromHtml(dts.Rows(0).Item("color_td").ToString)

        dts = obj.ConsultaTipoActividadPorAbreviatura("CP")
        lblCP.Text = dts.Rows(0).Item("descripcion_td").ToString
        lblCP.BackColor = System.Drawing.ColorTranslator.FromHtml(dts.Rows(0).Item("color_td").ToString)

        dts = obj.ConsultaTipoActividadPorAbreviatura("CA")
        lblCA.Text = dts.Rows(0).Item("descripcion_td").ToString
        lblCA.BackColor = System.Drawing.ColorTranslator.FromHtml(dts.Rows(0).Item("color_td").ToString)


        '==================================================================================================================
        '------------------------------------------------------Anulados ---------------------------------------------------
        '==================================================================================================================

        dts = obj.ConsultaTipoActividadPorAbreviatura("G")
        lblG.Text = dts.Rows(0).Item("descripcion_td").ToString
        lblG.BackColor = System.Drawing.ColorTranslator.FromHtml(dts.Rows(0).Item("color_td").ToString)

        'dts = obj.ConsultaTipoActividadPorAbreviatura("F")
        'lblF.Text = dts.Rows(0).Item("descripcion_td").ToString
        'lblF.BackColor = System.Drawing.ColorTranslator.FromHtml(dts.Rows(0).Item("color_td").ToString)

        'dts = obj.ConsultaTipoActividadPorAbreviatura("E")
        'lblE.Text = dts.Rows(0).Item("descripcion_td").ToString
        'lblE.BackColor = System.Drawing.ColorTranslator.FromHtml(dts.Rows(0).Item("color_td").ToString)

        dts = obj.ConsultaTipoActividadPorAbreviatura("GA")
        lblGA.Text = dts.Rows(0).Item("descripcion_td").ToString
        lblGA.BackColor = System.Drawing.ColorTranslator.FromHtml(dts.Rows(0).Item("color_td").ToString)

        dts = obj.ConsultaTipoActividadPorAbreviatura("GP")
        lblGP.Text = dts.Rows(0).Item("descripcion_td").ToString
        lblGP.BackColor = System.Drawing.ColorTranslator.FromHtml(dts.Rows(0).Item("color_td").ToString)

        '==================================================================================================================

    End Sub

    Private Sub ConsultarListaCambiosHorarios()
        Dim obj As New clsPersonal
        Dim dts As New Data.DataTable
        dts = obj.ConsultarListaCambiosHorarios(codigo_per, codigo_pel)
        gvListaCambios.DataSource = dts
        gvListaCambios.DataBind()
    End Sub

    Protected Sub ddlSemana_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlSemana.SelectedIndexChanged
        RangoFechasSemanas()
        consultarListaHorario()
        consultarVistaHorario()
    End Sub

    Private Sub RangoFechasSemanas()
        Dim dts As New Data.DataTable
        Dim obj As New clsPersonal
        
        Dim dtsMesVigente As New Data.DataTable
        dtsMesVigente = obj.MesVigente(codigo_pel, 0, "C")

        dts = obj.RangoFechasSemanasBitacora(ddlSemana.SelectedValue, dtsMesVigente.Rows(0).Item("mes_sec").ToString, codigo_pel)
        Dim FechaIni As String = dts.Rows(0).Item("desde_sec").ToString
        Dim FechaFin As String = dts.Rows(0).Item("hasta_sec").ToString

        lblFechas.Visible = True
        lblFechas.Text = FechaIni & " hasta " & FechaFin
    End Sub

    Protected Sub gvListaCambios_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvListaCambios.RowDataBound        
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim fila As Data.DataRowView
                fila = e.Row.DataItem
                Dim mes_vigente As String = e.Row.Cells(1).Text
                Dim fecha As String = e.Row.Cells(2).Text
                Dim hora As String = e.Row.Cells(3).Text
                Dim registradopor As String = e.Row.Cells(4).Text
                e.Row.Cells(5).Text = "<a href='frmVerHorarios.aspx?codigo_pel=" & codigo_pel & "&codigo_per=" & codigo_per & "&registradopor=" & registradopor & "&fecha=" & fecha & "&hora=" & hora & "&mes_vigente=" & mes_vigente & "&KeepThis=true&TB_iframe=true&height=600&width=750&modal=true' title='Ver horarios' class='thickbox'><img src='../../images/resultados.gif' border=0 /><a/>"
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try

    End Sub

 
End Class




