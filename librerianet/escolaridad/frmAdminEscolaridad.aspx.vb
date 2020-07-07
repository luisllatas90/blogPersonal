Imports System.IO   'Ultil para exporta a excel: dguevara 12.11.2013

Partial Class escolaridad_frmAdminEscolaridad
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                CargaAniosEscolaridad()
                CargarTipoTrabajadorEscolaridad()
                CargarDedicacionEscolaridad()
                CargarTrabajadores()
                BuscarSolicitudesTrabajador()
                lblInstrucciones.Text = "<b><font color='#982828'> Instrucciones:</b><br> &nbsp;&nbsp;-Marcar con check los registros a que desea aprobar. <br> &nbsp;&nbsp;-Pulse en el botón <b><font color='#20A7F5'>APROBAR</font></b>. <br> &nbsp;&nbsp;-Al terninar el proceso puede ver la columna Estado: <b><font color='#20A7F5'>APROBADO</font></b>."
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub CargarTrabajadores()
        Try
            Dim obj As New clsEscolaridad
            Dim dts As New Data.DataTable

            'Response.Write("ded" & Me.ddldedicacion.SelectedValue)
            'Response.Write("<br>")
            'Response.Write("ded" & Me.ddltipotrabajador.SelectedValue)

            dts = obj.CargarCombosEscolaridad("P", Me.ddldedicacion.SelectedValue, Me.ddltipotrabajador.SelectedValue, 0, 0, 0)

            'Response.Write("<br>")
            'Response.Write(dts.Rows.Count)
            If dts.Rows.Count > 0 Then
                ddltrabajador.DataSource = dts
                ddltrabajador.DataTextField = "trabajador"
                ddltrabajador.DataValueField = "codigo_Per"
                ddltrabajador.DataBind()
            Else
                ddltrabajador.DataSource = Nothing
                ddltrabajador.DataBind()
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub CargarDedicacionEscolaridad()
        Try
            Dim obj As New clsEscolaridad
            Dim dts As New Data.DataTable
            dts = obj.CargarCombosEscolaridad("D", 0, 0, 0, 0, 0)
            If dts.Rows.Count > 0 Then
                ddldedicacion.DataSource = dts
                ddldedicacion.DataTextField = "Descripcion_Ded"
                ddldedicacion.DataValueField = "codigo_Ded"
                ddldedicacion.DataBind()
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub CargarTipoTrabajadorEscolaridad()
        Try
            Dim obj As New clsEscolaridad
            Dim dts As New Data.DataTable
            dts = obj.CargarCombosEscolaridad("T", 0, 0, 0, 0, 0)
            If dts.Rows.Count > 0 Then
                ddltipotrabajador.DataSource = dts
                ddltipotrabajador.DataTextField = "descripcion_Tpe"
                ddltipotrabajador.DataValueField = "codigo_Tpe"
                ddltipotrabajador.DataBind()
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub CargaAniosEscolaridad()
        Try
            Dim obj As New clsEscolaridad
            Dim dts As New Data.DataTable
            dts = obj.CargarCombosEscolaridad("A", 0, 0, 0, 0, 0)
            If dts.Rows.Count > 0 Then
                ddlanio.DataSource = dts
                ddlanio.DataTextField = "anio_soe"
                ddlanio.DataValueField = "anio_soe"
                ddlanio.DataBind()
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub ddltipotrabajador_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddltipotrabajador.SelectedIndexChanged
        Try
            CargarTrabajadores()
            BuscarSolicitudesTrabajador()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub ddldedicacion_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddldedicacion.SelectedIndexChanged
        Try
            CargarTrabajadores()
            BuscarSolicitudesTrabajador()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub btnBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBuscar.Click
        Try
            BuscarSolicitudesTrabajador()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub BuscarSolicitudesTrabajador()
        Try
            Dim obj As New clsEscolaridad
            Dim dts As New Data.DataTable
            Dim desde As Date
            Dim hasta As Date


            If (Me.txtdesde.Text.Trim.Length = 0 And Me.txthasta.Text.Trim.Length > 0) Or _
                (Me.txtdesde.Text.Trim.Length > 0 And Me.txthasta.Text.Trim.Length = 0) Then
                Me.ClientScript.RegisterStartupScript(Me.GetType, "Faltan Datos", "alert('Ud. debe ingresar la fecha de inicio y fin, si desea hacer una búsqueda por fechas.')", True)
                Exit Sub
            End If


            '*** Validación ***'
            If Me.txtdesde.Text.Trim.Length = 0 And Me.txthasta.Text.Trim.Length = 0 Then
                desde = "01/01/1920"
                hasta = "01/01/2050"
            Else

                If IsDate(Me.txtdesde.Text) = True And IsDate(Me.txthasta.Text) = True Then
                    If CDate(Me.txtdesde.Text.Trim) > CDate(txthasta.Text.Trim) Then
                        Me.ClientScript.RegisterStartupScript(Me.GetType, "Faltan Datos", "alert('La fecha de inicio no puede ser mayor que la fecha fin, favor de verificar.')", True)
                        Exit Sub
                    Else
                        desde = CDate(Me.txtdesde.Text.Trim)
                        hasta = CDate(Me.txthasta.Text.Trim)
                    End If
                End If
            End If

            '*** recuperamos los datos ***'
            dts = obj.ListaRegistrosEscolaridad(Me.ddltrabajador.SelectedValue, CDate(desde), CDate(hasta), _
                                                Me.ddlestado.SelectedValue, Me.ddldedicacion.SelectedValue, Me.ddltipotrabajador.SelectedValue, Me.ddlanio.SelectedValue)
            lblCantidad.Text = "<font color='blue'> Se encontraron (<b>" & dts.Rows.Count.ToString & "</b>) solicitudes registradas.</font>"
            If dts.Rows.Count > 0 Then
                gvLista.DataSource = dts
                gvLista.DataBind()
            Else
                gvLista.DataSource = Nothing
                gvLista.DataBind()
            End If

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub btnAprobar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAprobar.Click
        Try
            If VerificarRegistros() = False Then
                Me.ClientScript.RegisterStartupScript(Me.GetType, "Faltan Datos", "alert('Ud. debe seleccionar por lo menos un registro para poder continuar la evaluación de escolaridad.');", True)
                Exit Sub
            End If

            If CalificarSolicitudes() = True Then
                Me.ClientScript.RegisterStartupScript(Me.GetType, "Faltan Datos", "alert('´Las solicitudes de escolaridad fueron aprobadas correctamente');", True)
            End If
            BuscarSolicitudesTrabajador()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Function CalificarSolicitudes() As Boolean
        Dim fila As GridViewRow
        Dim sw As Boolean = False
        Dim obj As New clsEscolaridad

        Try
            For i As Integer = 0 To gvLista.Rows.Count - 1
                fila = gvLista.Rows(i)
                Dim valor As Boolean = CType(fila.FindControl("chkElegir"), CheckBox).Checked
                If valor = True Then
                    'Response.Write(Me.gvLista.Rows(i).Cells(0).Text)
                    obj.CalificarSolicitud(Me.gvLista.Rows(i).Cells(0).Text)
                    sw = True
                End If
            Next
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
        If sw = True Then
            Return True
        End If
        Return False
    End Function


    Private Function VerificarRegistros() As Boolean
        Dim fila As GridViewRow
        Dim sw As Boolean = False

        Try
            For i As Integer = 0 To gvLista.Rows.Count - 1
                fila = gvLista.Rows(i)
                Dim valor As Boolean = CType(fila.FindControl("chkElegir"), CheckBox).Checked
                If valor = True Then
                    sw = True
                End If
            Next
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
        If sw = True Then
            Return True
        End If
        Return False
    End Function

    'Protected Sub gvLista_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gvLista.PageIndexChanging
    '    BuscarSolicitudesTrabajador()
    'End Sub

    Protected Sub gvLista_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvLista.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                'e.Row.Attributes.Add("OnMouseOver", "Resaltar(1,this,'S')")
                'e.Row.Attributes.Add("OnMouseOut", "Resaltar(0,this,'S')")
                'e.Row.Attributes.Add("OnClick", "javascript:__doPostBack('gvLista','Select$" & e.Row.RowIndex & "');")
                'e.Row.Style.Add("cursor", "hand")

                If e.Row.Cells(7).Text.ToString.Trim = "APROBADO" Then
                    Dim Cb As CheckBox
                    Cb = e.Row.FindControl("chkElegir")
                    Cb.Visible = True
                    Cb.Enabled = False
                    e.Row.Cells(7).ForeColor = Drawing.Color.Blue
                Else
                    e.Row.Cells(7).ForeColor = Drawing.Color.Red
                    Dim Cb As CheckBox
                    Cb = e.Row.FindControl("chkElegir")
                    Cb.Visible = True
                    Cb.Enabled = True
                End If
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

  
    Protected Sub ddlestado_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlestado.SelectedIndexChanged
        Try
            BuscarSolicitudesTrabajador()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub ddlanio_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlanio.SelectedIndexChanged
        Try
            BuscarSolicitudesTrabajador()
        Catch ex As Exception
            Me.Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub ddltrabajador_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddltrabajador.SelectedIndexChanged
        Try
            BuscarSolicitudesTrabajador()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub btnExportar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExportar.Click
        Try

            Dim obj As New clsEscolaridad
            Dim dts As New Data.DataTable
            Dim desde As Date
            Dim hasta As Date


            If (Me.txtdesde.Text.Trim.Length = 0 And Me.txthasta.Text.Trim.Length > 0) Or _
                (Me.txtdesde.Text.Trim.Length > 0 And Me.txthasta.Text.Trim.Length = 0) Then
                Me.ClientScript.RegisterStartupScript(Me.GetType, "Faltan Datos", "alert('Ud. debe ingresar la fecha de inicio y fin, si desea hacer una búsqueda por fechas.')", True)
                Exit Sub
            End If


            '*** Validación ***'
            If Me.txtdesde.Text.Trim.Length = 0 And Me.txthasta.Text.Trim.Length = 0 Then
                desde = "01/01/1920"
                hasta = "01/01/2050"
            Else

                If IsDate(Me.txtdesde.Text) = True And IsDate(Me.txthasta.Text) = True Then
                    If CDate(Me.txtdesde.Text.Trim) > CDate(txthasta.Text.Trim) Then
                        Me.ClientScript.RegisterStartupScript(Me.GetType, "Faltan Datos", "alert('La fecha de inicio no puede ser mayor que la fecha fin, favor de verificar.')", True)
                        Exit Sub
                    Else
                        desde = CDate(Me.txtdesde.Text.Trim)
                        hasta = CDate(Me.txthasta.Text.Trim)
                    End If
                End If
            End If

            '*** recuperamos los datos ***'
            dts = obj.ListaRegistrosEscolaridad_exportar(Me.ddltrabajador.SelectedValue, CDate(desde), CDate(hasta), _
                                                Me.ddlestado.SelectedValue, Me.ddldedicacion.SelectedValue, Me.ddltipotrabajador.SelectedValue, Me.ddlanio.SelectedValue)



            'Response.Write(dts.Rows.Count)

            If dts.Rows.Count > 0 Then

                Dim gvExportar As New GridView
                gvExportar.AllowPaging = False
                gvExportar.DataSource = dts
                gvExportar.DataBind()

                Response.Clear()
                Response.Buffer = True
                Response.AddHeader("content-disposition", "attachment;filename=ListaSolicitudesVacantes.xls")
                Response.Charset = ""
                Response.ContentType = "application/vnd.ms-excel"

                Dim sw As New StringWriter()
                Dim hw As New HtmlTextWriter(sw)

                '**# Formateamos la Cabecera **# ::: dguevara :::
                '---------------------------------------------------------------------
                gvExportar.HeaderRow.Cells(0).Style.Add("height", "35px")
                For i As Integer = 0 To dts.Columns.Count - 1
                    gvExportar.HeaderRow.Cells(i).Style.Add("background-color", "#7F0076")
                    gvExportar.HeaderRow.Cells(i).Style.Add("width", "auto")
                    gvExportar.HeaderRow.ForeColor = Drawing.Color.White

                Next
                '** Otra forma de hacerlo **' :::: dguevara
                'gvExportar.HeaderRow.BackColor = Drawing.Color.White
                'For Each cell As TableCell In gvExportar.HeaderRow.Cells
                '    cell.BackColor = gvExportar.HeaderStyle.BackColor
                'Next

                '---------------------------------------------------------------------
                '** Colores para las filas alternas **'
                For Each row As GridViewRow In gvExportar.Rows
                    'row.BackColor = Drawing.Color.Red
                    For Each cell As TableCell In row.Cells
                        '*Alternamos el colore de las filas.
                        If row.RowIndex Mod 2 = 0 Then
                            cell.BackColor = Drawing.Color.White
                        Else
                            cell.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFDD8")
                        End If
                        cell.CssClass = "textmode"
                    Next
                Next
                '---------------------------------------------------------------------

                gvExportar.RenderControl(hw)
                Dim style As String = "<style> .textmode{mso-number-format:\@;}</style>"
                Response.Write(style)
                Response.Output.Write(sw.ToString())
                Response.Flush()
                Response.End()

            Else
                Me.ClientScript.RegisterStartupScript(Me.GetType, "Faltan Datos", "alert('No se encontraron datos para Exportar.');", True)
                Exit Sub
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub
End Class
