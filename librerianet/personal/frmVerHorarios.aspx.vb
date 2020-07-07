
Partial Class personal_frmVerHorarios
    Inherits System.Web.UI.Page
    Dim codigo_per As Integer
    Dim codigo_pel As Integer
    Dim fecha As String
    Dim hora As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                lblfechaenvio.Text = Request.QueryString("fecha") & " " & Request.QueryString("hora")
                lblregistradopor.Text = Request.QueryString("registradopor")
                CargarVigencia()
                CargaLeyenda()
                ConsultarObservacion()

                If ddlSemana.Items.Count > 0 Then
                    consultarVistaHorario(ddlSemana.SelectedValue)
                    consultarListaHorario(ddlSemana.SelectedValue)
                    RangoFechasSemanas()
                End If
            End If

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub consultarListaHorario(ByVal semana As Integer)
        Try
            Dim obj As New clsPersonal
            Dim dts As New Data.DataTable
            codigo_pel = Request.QueryString("codigo_pel")
            codigo_per = Request.QueryString("codigo_per")
            fecha = Request.QueryString("fecha")
            hora = Request.QueryString("hora")
            dts = obj.ConsultarListaHorarioPorFecha(codigo_per, codigo_pel, fecha, hora, semana)
            gvEditHorario.DataSource = dts
            gvEditHorario.DataBind()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try

    End Sub

    Private Sub consultarVistaHorario(ByVal semana As Integer)
        Try
            Dim obj As New clsPersonal
            Dim dts As New Data.DataTable
            codigo_per = Request.QueryString("codigo_per")
            codigo_pel = Request.QueryString("codigo_pel")
            fecha = Request.QueryString("fecha")
            hora = Request.QueryString("hora")
            If (obj.PeridoLaboralVigente(codigo_pel).Rows.Count > 0) Then
                dts = obj.ConsultarVistaHorarioPorFecha(codigo_per, codigo_pel, fecha, hora, semana)
            Else
                dts = obj.ConsultarVistaHorarioPorFecha_v2(codigo_per, codigo_pel, fecha, hora, semana)
            End If
            'dts = obj.ConsultarVistaHorarioPorFecha(codigo_per, codigo_pel, fecha, hora, semana)
            gvVistaHorario.DataSource = dts
            gvVistaHorario.DataBind()

            lblHorasSemanales.Text = obj.ConsultarTotalHorasSemanaBitacora(codigo_per, codigo_pel, fecha, hora, semana)
            lblHorasMensuales.Text = obj.ConsultarTotalHorasMesBitacora(codigo_per, codigo_pel, fecha, hora, semana)
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub gvVistaHorario_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvVistaHorario.RowDataBound
        Try
            e.Row.Cells(2).Width = 40
            e.Row.Cells(3).Width = 40
            e.Row.Cells(4).Width = 40
            e.Row.Cells(5).Width = 40
            e.Row.Cells(6).Width = 40
            e.Row.Cells(7).Width = 40
            Dim obj As New clsPersonal
            Dim dts As New Data.DataTable
            For i As Integer = 2 To 7
                Select Case e.Row.Cells(i).Text
                    Case "A "
                        dts = obj.ConsultaTipoActividadPorAbreviatura("A")
                        Dim color As String = dts.Rows(0).Item("color_td")
                        e.Row.Cells(i).BackColor = System.Drawing.ColorTranslator.FromHtml(color)
                    Case "D "
                        dts = obj.ConsultaTipoActividadPorAbreviatura("D")
                        Dim color As String = dts.Rows(0).Item("color_td")
                        e.Row.Cells(i).BackColor = System.Drawing.ColorTranslator.FromHtml(color)

                    Case "G "
                        dts = obj.ConsultaTipoActividadPorAbreviatura("G")
                        Dim color As String = dts.Rows(0).Item("color_td")
                        e.Row.Cells(i).BackColor = System.Drawing.ColorTranslator.FromHtml(color)

                    Case "T "
                        dts = obj.ConsultaTipoActividadPorAbreviatura("T")
                        Dim color As String = dts.Rows(0).Item("color_td")
                        e.Row.Cells(i).BackColor = System.Drawing.ColorTranslator.FromHtml(color)
                    Case "I "
                        dts = obj.ConsultaTipoActividadPorAbreviatura("I")
                        Dim color As String = dts.Rows(0).Item("color_td")
                        e.Row.Cells(i).BackColor = System.Drawing.ColorTranslator.FromHtml(color)
                    Case "U "
                        dts = obj.ConsultaTipoActividadPorAbreviatura("U")
                        Dim color As String = dts.Rows(0).Item("color_td")
                        e.Row.Cells(i).BackColor = System.Drawing.ColorTranslator.FromHtml(color)
                    Case "P "
                        dts = obj.ConsultaTipoActividadPorAbreviatura("P")
                        Dim color As String = dts.Rows(0).Item("color_td")
                        e.Row.Cells(i).BackColor = System.Drawing.ColorTranslator.FromHtml(color)
                    Case "H "
                        dts = obj.ConsultaTipoActividadPorAbreviatura("H")
                        Dim color As String = dts.Rows(0).Item("color_td")
                        e.Row.Cells(i).BackColor = System.Drawing.ColorTranslator.FromHtml(color)

                    Case "F "
                        dts = obj.ConsultaTipoActividadPorAbreviatura("F")
                        Dim color As String = dts.Rows(0).Item("color_td")
                        e.Row.Cells(i).BackColor = System.Drawing.ColorTranslator.FromHtml(color)

                    Case "E "
                        dts = obj.ConsultaTipoActividadPorAbreviatura("E")
                        Dim color As String = dts.Rows(0).Item("color_td")
                        e.Row.Cells(i).BackColor = System.Drawing.ColorTranslator.FromHtml(color)


                    Case "C "
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

                    Case "R "
                        dts = obj.ConsultaTipoActividadPorAbreviatura("R")
                        Dim color As String = dts.Rows(0).Item("color_td")
                        e.Row.Cells(i).BackColor = System.Drawing.ColorTranslator.FromHtml(color)

                        'Agregado 22/08/2011
                    Case "CP"
                        dts = obj.ConsultaTipoActividadPorAbreviatura("CP")
                        Dim color As String = dts.Rows(0).Item("color_td")
                        e.Row.Cells(i).BackColor = System.Drawing.ColorTranslator.FromHtml(color)

                        '#############################################################################
                        'Modificacion Agregado 13/12/2011 Xdguevara
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
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try      
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

        'dts = obj.ConsultaTipoActividadPorAbreviatura("G")
        'lblG.Text = dts.Rows(0).Item("descripcion_td").ToString
        'lblG.BackColor = System.Drawing.ColorTranslator.FromHtml(dts.Rows(0).Item("color_td").ToString)

        'dts = obj.ConsultaTipoActividadPorAbreviatura("F")
        'lblF.Text = dts.Rows(0).Item("descripcion_td").ToString
        'lblF.BackColor = System.Drawing.ColorTranslator.FromHtml(dts.Rows(0).Item("color_td").ToString)

        'dts = obj.ConsultaTipoActividadPorAbreviatura("E")
        'lblE.Text = dts.Rows(0).Item("descripcion_td").ToString
        'lblE.BackColor = System.Drawing.ColorTranslator.FromHtml(dts.Rows(0).Item("color_td").ToString)

        'dts = obj.ConsultaTipoActividadPorAbreviatura("GA")
        'lblGA.Text = dts.Rows(0).Item("descripcion_td").ToString
        'lblGA.BackColor = System.Drawing.ColorTranslator.FromHtml(dts.Rows(0).Item("color_td").ToString)

        'dts = obj.ConsultaTipoActividadPorAbreviatura("GP")
        'lblGP.Text = dts.Rows(0).Item("descripcion_td").ToString
        'lblGP.BackColor = System.Drawing.ColorTranslator.FromHtml(dts.Rows(0).Item("color_td").ToString)

        '==================================================================================================================

    End Sub

    Private Sub RangoFechasSemanas()
        Try
            Dim dts As New Data.DataTable
            Dim obj As New clsPersonal
            Dim mes_vigente As String
            codigo_pel = Request.QueryString("codigo_pel")
            codigo_per = Request.QueryString("codigo_per")
            fecha = Request.QueryString("fecha")
            hora = Request.QueryString("hora")
            mes_vigente = Request.QueryString("mes_vigente")

            dts = obj.RangoFechasSemanasBitacora(ddlSemana.SelectedValue, mes_vigente, codigo_pel)
            Dim FechaIni As String = dts.Rows(0).Item("desde_sec").ToString
            Dim FechaFin As String = dts.Rows(0).Item("hasta_sec").ToString
            lblFechas.Text = FechaIni & " hasta " & FechaFin
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub ConsultarObservacion()
        Try
            Dim obj As New clsPersonal
            codigo_pel = Request.QueryString("codigo_pel")
            codigo_per = Request.QueryString("codigo_per")
            fecha = Request.QueryString("fecha")
            hora = Request.QueryString("hora")

            If obj.ConsultarBitacoraObservacion(codigo_per, codigo_pel, fecha, hora).ToString <> "" Then
                lblObservacionHorario.Text = obj.ConsultarBitacoraObservacion(codigo_per, codigo_pel, fecha, hora)
            Else
                lblObservacionHorario.Text = ""
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub


    Private Sub CargarVigencia()
        Try
            Dim dts As New Data.DataTable
            Dim obj As New clsPersonal
            Dim mes_vigente As String
            codigo_pel = Request.QueryString("codigo_pel")
            codigo_per = Request.QueryString("codigo_per")
            fecha = Request.QueryString("fecha")
            mes_vigente = Request.QueryString("mes_vigente")                         
            dts = obj.ConsultarSemanasBitacora(codigo_per, codigo_pel, mes_vigente)
            If Not IsDBNull(dts.Rows(0).Item("semana_hop")) Then
                ddlSemana.DataSource = dts
                ddlSemana.DataTextField = "semana"
                ddlSemana.DataValueField = "semana_hop"
                ddlSemana.DataBind()
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
        
    End Sub


    Protected Sub ddlSemana_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlSemana.SelectedIndexChanged
        Try
            If ddlSemana.SelectedValue <> -1 Then
                consultarVistaHorario(ddlSemana.SelectedValue)
                consultarListaHorario(ddlSemana.SelectedValue)
                RangoFechasSemanas()
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
        

    End Sub

End Class
