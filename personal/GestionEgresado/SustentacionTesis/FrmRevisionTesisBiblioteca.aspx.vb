Imports System.Data
Imports System.IO
Imports System.Collections.Generic

Partial Class FrmRevisionTesisBiblioteca
    Inherits System.Web.UI.Page
    Dim ruta As String = ConfigurationManager.AppSettings("SharedFiles")

    Private Sub ConsultarTesis(ByVal estado As String)
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Dim dt As New Data.DataTable
        dt = obj.TraerDataTable("SUST_ListarTesisRevisionBiblioteca", estado)
        If dt.Rows.Count > 0 Then
            Me.gvTesis.DataSource = dt
            Me.gvTesis.DataBind()
            Me.btnExportarExcel.Visible = True
        Else
            Me.gvTesis.DataSource = Nothing
            Me.gvTesis.DataBind()
            Me.btnExportarExcel.Visible = False

        End If
        obj.CerrarConexion()
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (Session("id_per") Is Nothing) Then
            Response.Redirect("../../../sinacceso.html")
        End If
        Try
            If IsPostBack = False Then
                ConsultarTesis(Me.ddlEstado.SelectedValue)
            End If
        Catch ex As Exception
            Response.Write(ex.Message.ToString)
        End Try

    End Sub

    Protected Sub gvTesis_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvTesis.RowCommand
        If (Session("id_per") Is Nothing) Then
            Response.Redirect("../../../sinacceso.html")
        End If
        Try
            If (e.CommandName = "Asesorias") Then
                Me.Lista.Visible = False
                Me.DivAsesorias.Visible = True
                Me.hdjur.Value = Me.gvTesis.DataKeys(e.CommandArgument).Values("codigo_jur")
                Me.hdtes.Value = Me.gvTesis.DataKeys(e.CommandArgument).Values("codigo_Tes")
                LimpiarDatos()
                Me.txtObservacion.Text = ""
                ConsultarDatosTesis(Me.gvTesis.DataKeys(e.CommandArgument).Values("codigo_Tes"))
                CargarLineaTiempo(Me.hdtes.Value, Me.hdjur.Value)
                If Me.gvTesis.DataKeys(e.CommandArgument).Values("codigo_ctb") = "0" Then
                    Me.txtObservacion.Enabled = True
                    Me.btnGuardarObservacion.Visible = True
                Else
                    Me.txtObservacion.Enabled = False
                    Me.btnGuardarObservacion.Visible = False
                End If
            End If
            If (e.CommandName = "Conformidad") Then
                Dim codigo_jur As Integer = Me.gvTesis.DataKeys(e.CommandArgument).Values("codigo_jur")
                Dim codigo_pst As Integer = Me.gvTesis.DataKeys(e.CommandArgument).Values("codigo_pst")
                'Dar Conformidad
                Dim dt As New Data.DataTable
                dt = ActualizarConformidad(codigo_pst, codigo_jur, 1, Session("id_per"))
                If dt.Rows.Count() > 0 Then
                    If dt.Rows(0).Item("Respuesta") = 1 Then ' dio conformidad
                        Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "alert1", "fnMensaje('success','" + dt.Rows(0).Item("Mensaje") + "')", True)
                        ConsultarTesis(Me.ddlEstado.SelectedValue)
                    Else
                        Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "alert2", "fnMensaje('error','" + dt.Rows(0).Item("Mensaje") + "')", True)
                    End If
                Else
                    Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "alert3", "fnMensaje('error','No se pudo actualizar conformidad')", True)
                End If
                Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "LoadingEstado", "LoadingEstado();", True)

            End If
            If (e.CommandName = "Anular") Then
                If Me.gvTesis.DataKeys(e.CommandArgument).Values("estado_trl") = "P" Then
                    Dim dtAnular As Data.DataTable
                    dtAnular = AnularConformidad(Me.gvTesis.DataKeys(e.CommandArgument).Values("codigo_ctb"), Me.gvTesis.DataKeys(e.CommandArgument).Values("codigo_Tes"))
                    If dtAnular.Rows.Count > 0 Then
                        If dtAnular.Rows(0).Item("Respuesta").ToString = 1 Then
                            Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "alert65", "fnMensaje('success','Se anuló conformidad')", True)
                        Else
                            Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "alert53", "fnMensaje('error','No se pudo anular conformidad de tesis')", True)
                        End If
                    Else
                        Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "alert53", "fnMensaje('error','No se pudo anular conformidad de tesis')", True)

                    End If
                    ConsultarTesis(Me.ddlEstado.SelectedValue)
                Else
                    Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "alert543", "fnMensaje('error','Para realizar la anulación de la conformidad, primero debe anular la autorización de publicación generada')", True)
                End If
            End If

            Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "Loadingx", "fnLoading(false)", True)
        Catch ex As Exception
            Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "alert4", "fnMensaje('error','" + ex.Message.ToString + "')", True)
        End Try

    End Sub


    Private Sub ConsultarDatosTesis(ByVal codigo_Tes As Integer)
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Dim dt As New Data.DataTable
        dt = obj.TraerDataTable("SUST_ConsultarDatosTesis", codigo_Tes)
        obj.CerrarConexion()
        Me.txtAutor.Text = Me.txtAutor.Text & dt.Rows(0).Item("alumno").ToString
        If dt.Rows.Count > 1 Then
            For i As Integer = 1 To dt.Rows.Count - 1
                Me.txtAutor.Text = Me.txtAutor.Text & vbCrLf & dt.Rows(i).Item("alumno").ToString
            Next
        End If
        Me.txtFacultad.Text = dt.Rows(0).Item("facultad").ToString
        Me.txtcarrera.Text = dt.Rows(0).Item("nombre_cpf").ToString
        Me.txtlinea.Text = dt.Rows(0).Item("linea").ToString
        Me.txtarea.Text = dt.Rows(0).Item("area").ToString
        Me.txtsubarea.Text = dt.Rows(0).Item("subarea").ToString
        Me.txtdisciplina.Text = dt.Rows(0).Item("disciplina").ToString
        Me.txtPresupuesto.Text = dt.Rows(0).Item("presupuesto").ToString
        Me.txtFinanciamiento.Text = dt.Rows(0).Item("Financiamiento").ToString
        Me.txttitulo.Text = dt.Rows(0).Item("Titulo_Tes").ToString
        Me.txtObjetivoG.Text = dt.Rows(0).Item("objetivogeneral").ToString
        Me.txtObjetivoE.Text = dt.Rows(0).Item("objetivoespecifico").ToString
    End Sub


    Private Sub LimpiarDatos()
        Me.txttitulo.Text = ""
        Me.txtAutor.Text = ""
        Me.txtarea.Text = ""
        Me.txtcarrera.Text = ""
        Me.txtFacultad.Text = ""
        Me.txtFinanciamiento.Text = ""
        Me.txtdisciplina.Text = ""
        Me.txtlinea.Text = ""
        Me.txtObjetivoE.Text = ""
        Me.txtObjetivoG.Text = ""
        Me.txtPresupuesto.Text = ""
        Me.txtsubarea.Text = ""
        Me.txtarea.Text = ""
    End Sub


    Dim LastCategory As String = String.Empty
    Dim CurrentRow As Integer = -1

    Protected Sub gvTesis_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvTesis.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim row As Data.DataRowView = CType(e.Row.DataItem, Data.DataRowView)
            If LastCategory = row("codigo_tes").ToString Then


                If (gvTesis.Rows(CurrentRow).Cells(0).RowSpan = 0) Then
                    gvTesis.Rows(CurrentRow).Cells(0).RowSpan = 2
                Else
                    gvTesis.Rows(CurrentRow).Cells(0).RowSpan += 1
                End If
                e.Row.Cells(0).Visible = False

                If (gvTesis.Rows(CurrentRow).Cells(1).RowSpan = 0) Then
                    gvTesis.Rows(CurrentRow).Cells(1).RowSpan = 2
                Else
                    gvTesis.Rows(CurrentRow).Cells(1).RowSpan += 1
                End If
                e.Row.Cells(1).Visible = False
            Else
                e.Row.VerticalAlign = VerticalAlign.Middle
                LastCategory = row("codigo_tes").ToString()
                CurrentRow = e.Row.RowIndex
            End If
            'Boton descargar
            If Me.gvTesis.DataKeys(e.Row.RowIndex).Values("informe") <> "" Then
                Dim btn As LinkButton = DirectCast(e.Row.Cells(2).FindControl("btnDescargar"), LinkButton)
                btn.Text = "<span class='fa fa-download'></span>"
                btn.OnClientClick = "fnDescargar('../../DescargarArchivo.aspx?Id=" + Me.gvTesis.DataKeys(e.Row.RowIndex).Values("informe") + "&idt=23');return false;"
                btn.CssClass = "btn btn-sm btn-info btn-radius"
            End If
            If Me.gvTesis.DataKeys(e.Row.RowIndex).Values("codigo_ctb").ToString <> "0" Then
                'Dim btn As LinkButton = DirectCast(e.Row.Cells(5).FindControl("btnConforme"), LinkButton)
                'Me.gvTesis.Columns(3).Visible = False
                Me.gvTesis.Columns(8).Visible = False
                If Request("ctf") = 1 Or Request("ctf") = 76 Then 'ADMINISTRADOR O DIRECTOR DE BIBLIOTECA
                    Me.gvTesis.Columns(9).Visible = True
                Else
                    Me.gvTesis.Columns(9).Visible = False
                End If
            Else
                Me.gvTesis.Columns(8).Visible = True
                Me.gvTesis.Columns(9).Visible = False
            End If
        End If

    End Sub

    Private Function ActualizarConformidad(ByVal codigo_pst As Integer, ByVal codigo_jur As Integer, ByVal conformidad As Integer, ByVal codigo_per As Integer) As Data.DataTable
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Dim dt As New Data.DataTable
        dt = obj.TraerDataTable("SUST_RegistrarConformidadBiblioteca", codigo_pst, codigo_jur, conformidad, codigo_per)
        obj.CerrarConexion()
        Return dt
    End Function

    Protected Sub btnAtras_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAtras.Click
        If (Session("id_per") Is Nothing) Then
            Response.Redirect("../../sinacceso.html")
        End If
        ConsultarTesis(Me.ddlEstado.SelectedValue)
        Me.DivAsesorias.Visible = False
        Me.Lista.Visible = True
        Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "Loading", "fnLoading(false)", True)
        Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "LoadingEstado", "LoadingEstado();", True)

    End Sub

    Protected Sub btnGuardarObservacion_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardarObservacion.Click
        If (Session("id_per") Is Nothing) Then
            Response.Redirect("../../sinacceso.html")
        End If
        Try
            If validarRevision() = True Then
                GuardarRevision(Me.hdtes.Value, Me.hdjur.Value, Me.txtObservacion.Text, Session("id_per"))
            End If
            Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "Loading", "fnLoading(false)", True)
        Catch ex As Exception
            Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "Valida1", "fnMensaje('error','" + ex.Message.ToString + "')", True)
        End Try
    End Sub

    Public Function validarRevision() As Boolean
        If Me.txtObservacion.Text = "" Then
            Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "Valida1", "fnMensaje('error','Ingrese observación')", True)
            Return False
        End If
        Return True
    End Function

    Private Sub GuardarRevision(ByVal codigo_Tes As Integer, ByVal codigo_jur As Integer, ByVal descripcion As String, ByVal codigo_per As Integer)
        Dim dt As New Data.DataTable
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        dt = obj.TraerDataTable("SUST_GuardarObservacionSustentacion", codigo_Tes, codigo_jur, descripcion, codigo_per)
        obj.CerrarConexion()
        If dt.Rows(0).Item("Respuesta") = "1" Then
            Me.txtObservacion.Text = ""
            CargarLineaTiempo(Me.hdtes.Value, Me.hdjur.Value)
            EnviarNotificacionObservacion(Session("id_per"), Request("ctf"), 47, Me.hdtes.Value, Me.hdjur.Value)
            Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "Valida1", "fnMensaje('success','" + dt.Rows(0).Item("Mensaje").ToString + "')", True)
        Else
            Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "Valida1", "fnMensaje('error','" + dt.Rows(0).Item("Mensaje").ToString + "')", True)
        End If
    End Sub


    Private Sub CargarLineaTiempo(ByVal codigo_tes As Integer, ByVal codigo_jur As Integer)
        Dim dt As New Data.DataTable
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        dt = obj.TraerDataTable("SUST_ListarObservacionesBiblioteca", codigo_tes, codigo_jur)
        obj.CerrarConexion()

        Me.LineaDeTiempo.InnerHtml = ""
        Dim validarPendientes As Integer = 0
        If dt.Rows.Count > 0 Then
            Dim verificar As Integer = 0
            Dim str As String = ""
            Dim color As String = "Green"
            For i As Integer = 0 To dt.Rows.Count - 1
                verificar = 0
                If i = 0 Then
                    verificar = 1
                ElseIf dt.Rows(i).Item("MesAnio") <> dt.Rows(i - 1).Item("MesAnio") Then
                    verificar = 1
                End If
                If verificar = 1 Then
                    ' CABECERA DE MES
                    str += "<div class='row'>"
                    str += "<div class='col-xs-6 col-sm-4 col-md-2 col-md-offset-3'>"
                    str += "<div class='timeline-year'>"
                    str += dt.Rows(i).Item("MesAnio")
                    str += " </div>"
                    str += "</div>"
                    str += "</div>"
                    'LINEA DE TIEMPO
                    str += "<div class='time-bar'>"
                    str += "</div>"
                End If

                ' FECHA DE CONSULTA/RESPUESTA
                str += "<div class='col-xs-4 col-sm-2 col-md-2 col-md-offset-1'>"
                str += "<div class='timeline-date'>"
                str += dt.Rows(i).Item("fecha") + "<br />" + dt.Rows(i).Item("hora")
                str += "</div>"
                str += "</div>"
                ' CONTENIDO
                color = dt.Rows(i).Item("color")
                str += "<div class='col-xs-2 col-sm-1 col-md-1'>"
                str += "<div class='timeline-icon flat" + color + "'>"
                str += "<i class='" + dt.Rows(i).Item("icono") + "'></i>"
                str += "</div>"
                str += "</div>"
                str += "<div class='col-xs-12 col-sm-9 col-md-8'>"
                str += "<div class='timeline-hover'>"
                str += "<div class='timeline-heading flat" + color + "'>"
                str += "<div class='timeline-arrow arrow-" + color.ToLower + "'>"
                str += "</div>"
                str += dt.Rows(i).Item("personal") ' TITULO
                'If dt.Rows(i).Item("estado") <> "" Then
                'str += "<p class='text-right' style='float:right' >" + dt.Rows(i).Item("estado") + "</p>" ' TITULO
                'End If
                str += "</div>"
                str += "<div class='timeline-content'>"
                str += dt.Rows(i).Item("observacion")
                str += "<hr><div class='row'><div class='col-sm-4  col-md-4'>"
                If dt.Rows(i).Item("archivo") <> "" Then
                    str += "<a  onclick='fnDescargar(""../../DescargarArchivo.aspx?Id="
                    str += dt.Rows(i).Item("archivo")
                    str += "&idt=23"");return false;'>Archivo adjunto <i class='ion-android-attach'></i></a>"
                End If
                str += "</div>"
                str += "<div class='col-sm-4  col-md-4'>"
                str += "</div>"
                str += "<div class='col-sm-4  col-md-4 text-right'>"
                If dt.Rows(i).Item("estado").ToString = "PENDIENTE" And dt.Rows(i).Item("responder").ToString = "1" Then
                    str += "<button type='button' id='btnResponder" + i.ToString + "' class='btn btn-sm btn-primary' data-toggle='modal' data-target='#mdResponder' attr='" + dt.Rows(i).Item("codigo_ost").ToString + "' >Responder <i class='ion-chatbubble-working'></i></button>"
                End If
                str += "</div>" ' cierra div col-sm-4

                str += "</div>" ' cierra div row

                str += "</div>" ' cierra div timeline-content
                str += "</div>"
                str += "</div>"
            Next
            Me.LineaDeTiempo.InnerHtml = str

        End If
    End Sub

    Protected Sub ddlEstado_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlEstado.SelectedIndexChanged
        If (Session("id_per") Is Nothing) Then
            Response.Redirect("../../../sinacceso.html")
        End If
        ConsultarTesis(Me.ddlEstado.SelectedValue)
        Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "Loading", "fnLoading(false)", True)
        Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "LoadingEstado", "LoadingEstado();", True)
    End Sub


    Protected Sub btnExportExcel_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        'Response.ClearContent()
        'Response.AddHeader("content-disposition", "attachment; filename=CustomerDetails.xls")
        'Response.ContentType = "application/excel"
        'Dim SW As StringWriter = New StringWriter()
        'Dim HTW As HtmlTextWriter = New HtmlTextWriter(SW)
        'Me.gvTesis.RenderControl(HTW)
        'Response.Write(SW.ToString())
        'Response.End()
        'Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "LoadsingEstado", "alert('asdsa');", True)
        GenerarExcel()
    End Sub

    Private Sub GenerarExcel()
        Dim sb As StringBuilder = New StringBuilder()
        Dim sw As StringWriter = New StringWriter(sb)
        Dim htw As HtmlTextWriter = New HtmlTextWriter(sw)
        Dim page As Page = New Page()
        Dim form As HtmlForm = New HtmlForm()
        ' Deshabilitar la validación de eventos, sólo asp.net 2
        gvTesis.EnableViewState = False
        page.EnableEventValidation = False
        'Realiza las inicializaciones de la instancia de la clase Page que requieran los diseñadores RAD.
        page.DesignerInitialize()
        page.Controls.Add(form)
        form.Controls.Add(gvTesis)
        page.RenderControl(htw)
        Response.Clear()
        Response.Buffer = True
        Response.ContentType = "application/vnd.ms-excel"
        Response.AddHeader("Content-Disposition", "attachment;filename=revision_postsustentacion_" + DateTime.Now.ToString("yyyy_MM_dd_H_mm_ss") + ".xls")
        Response.Charset = "UTF-8"
        Response.ContentEncoding = Encoding.[Default]
        Response.Write(sb.ToString())
        Response.End()
    End Sub


    Private Sub EnviarNotificacionObservacion(ByVal codigo_per_emisor As Integer, ByVal codigo_tfu_emisor As Integer, ByVal codigo_apl As Integer, ByVal codigo_Tes As Integer, ByVal codigo_jur As Integer)
        Try
            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            Dim dt As New Data.DataTable
            dt = obj.TraerDataTable("SUST_DatosNotificacionObservacionJurado", codigo_Tes, codigo_jur, codigo_per_emisor)
            obj.CerrarConexion()

            If dt.Rows.Count > 0 Then

                Dim codigo_envio As Integer = ClsComunicacionInstitucional.ObtenerCodigoEnvio(codigo_per_emisor, codigo_tfu_emisor, codigo_apl)
                Dim correo_destino As String = ""
                Dim mensaje As String = ""
                Dim bandera As Integer = 0

                For i As Integer = 0 To dt.Rows.Count - 1
                    If ConfigurationManager.AppSettings("CorreoUsatActivo") = 1 Then
                        correo_destino = dt.Rows(i).Item("correo")
                    Else
                        correo_destino = "hcano@usat.edu.pe,ravalos@usat.edu.pe,csenmache@usat.edu.pe,yperez@usat.edu.pe"
                    End If
                    If ClsComunicacionInstitucional.EnviarNotificacionEmail(codigo_envio, "SUST", "MBBP", "1", codigo_per_emisor, "codigo_alu", dt.Rows(i).Item("codigo_alu"), codigo_apl, correo_destino, "", "OBSERVACIÓN DE REVISIÓN FINAL DE BIBLIOTECA", "", dt.Rows(i).Item("estudiante"), dt.Rows(i).Item("tipojurado"), dt.Rows(i).Item("jurado"), dt.Rows(i).Item("Titulo_Tes")) Then
                        mensaje += ", Notificación enviada al estudiante: " + dt.Rows(i).Item("estudiante")

                    Else
                        mensaje += ", No se pudo enviar notificación al estudiante: " + dt.Rows(i).Item("estudiante")
                        bandera = bandera + 1
                    End If
                Next
            End If
        Catch ex As Exception
            'ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "valida3", "fnMensaje('error','Operación no se ejecuto correctamente');", True)
        End Try

    End Sub

    Private Function AnularConformidad(ByVal codigo_ctb As Integer, ByVal codigo_Tes As Integer) As Data.DataTable
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Dim dt As New Data.DataTable
        dt = obj.TraerDataTable("SUST_AnularConformidadPostSustentacion", codigo_ctb, codigo_Tes, Session("id_per"))
        obj.CerrarConexion()
        Return dt

    End Function



End Class

