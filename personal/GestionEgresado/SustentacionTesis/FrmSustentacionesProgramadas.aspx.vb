Imports System.Data
Imports System.IO
Imports System.Collections.Generic

Partial Class FrmSustentacionesProgramadas
    Inherits System.Web.UI.Page
    Dim ruta As String = ConfigurationManager.AppSettings("SharedFiles")
    Private Sub ConsultarTesis(ByVal codigo_per As Integer, ByVal codigo_ctf As Integer, ByVal estado As String)
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Dim dt As New Data.DataTable
        dt = obj.TraerDataTable("[SUST_ListarsustentacionesProgramadas]", codigo_per, codigo_ctf, estado)
        If dt.Rows.Count > 0 Then
            Me.gvTesis.DataSource = dt
            Me.gvTesis.DataBind()
        Else
            Me.gvTesis.DataSource = Nothing
            Me.gvTesis.DataBind()
        End If
        obj.CerrarConexion()
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (Session("id_per") Is Nothing) Then
            Response.Redirect("../../../sinacceso.html")
        End If
        Try
            If IsPostBack = False Then
                ConsultarTesis(Session("id_per"), Request("ctf"), Me.ddlEstado.SelectedValue)
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
            If (e.CommandName = "Calificar") Then
                Me.Lista.Visible = False
                Me.divCalificar.Visible = True
                Limpiar()
                Me.hdPst.Value = Me.gvTesis.DataKeys(e.CommandArgument).Values("codigo_pst")
                Me.hdjur.Value = Me.gvTesis.DataKeys(e.CommandArgument).Values("codigo_jur")
                Me.hdtes.Value = Me.gvTesis.DataKeys(e.CommandArgument).Values("codigo_Tes")
                ConsultarDatosProgramacion(Me.hdPst.Value, Me.hdjur.Value)
                Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "Loading", "fnLoading(false)", True)

            End If
            If (e.CommandName = "Observaciones") Then
                Me.Lista.Visible = False
                Me.DivAsesorias.Visible = True
                Me.hdPst.Value = Me.gvTesis.DataKeys(e.CommandArgument).Values("codigo_pst")
                Me.hdjur.Value = Me.gvTesis.DataKeys(e.CommandArgument).Values("codigo_jur")
                Me.hdtes.Value = Me.gvTesis.DataKeys(e.CommandArgument).Values("codigo_Tes")
                Me.txtTituloObservacion.Text = Me.gvTesis.DataKeys(e.CommandArgument).Values("titulo_tes")
                If Me.gvTesis.DataKeys(e.CommandArgument).Values("archivofinal") <> "" Then
                    Me.btnArchivoTesis.OnClientClick = "fnDescargar('../../DescargarArchivo.aspx?Id=" + Me.gvTesis.DataKeys(e.CommandArgument).Values("archivofinal") + "&idt=23');return false;"
                Else
                    Me.btnArchivoTesis.OnClientClick = "alert('No cuenta con un archivo de tesis');return false;"
                End If
                CargarLineaTiempo(Me.hdtes.Value, Me.hdjur.Value)
                Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "Loading", "fnLoading(false)", True)

            End If
        Catch ex As Exception
            Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "fnMensaje('error','" + ex.Message.ToString + "')", True)
        End Try
    End Sub

    Protected Sub gvTesis_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvTesis.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim btn2 As LinkButton = DirectCast(e.Row.Cells(4).FindControl("btnResolucion"), LinkButton)
            If Me.gvTesis.DataKeys(e.Row.RowIndex).Values("resolucion") <> "" Then
                btn2.Text = "<span class='fa fa-download'></span>"
                btn2.OnClientClick = "fnDescargar('../../DescargarArchivo.aspx?Id=" + Me.gvTesis.DataKeys(e.Row.RowIndex).Values("resolucion") + "&idt=32');return false;"
                btn2.CssClass = "btn btn-sm btn-info btn-radius"
            Else
                btn2.Visible = False
            End If
            Dim btn As LinkButton = DirectCast(e.Row.Cells(4).FindControl("btnCalificar"), LinkButton)
            If Me.gvTesis.DataKeys(e.Row.RowIndex).Values("codigo_cst") <> "0" Then
                btn.Visible = False
            End If
            Dim btnObservaciones As LinkButton = DirectCast(e.Row.Cells(4).FindControl("btnObservaciones"), LinkButton)
            If Me.gvTesis.DataKeys(e.Row.RowIndex).Values("observado") = "1" Then
                btnObservaciones.Visible = True
            Else
                btnObservaciones.Visible = False
            End If
        End If

    End Sub

    Private Sub ConsultarDatosProgramacion(ByVal codigo_pst As Integer, ByVal codigo_jur As Integer)
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Dim dt As New Data.DataTable
        dt = obj.TraerDataTable("SUST_DatosCalificacionSustentacion", codigo_pst, codigo_jur)
        If dt.Rows.Count > 0 Then
            Dim str As String = ""
            For i As Integer = 0 To dt.Rows.Count - 1
                str += "<div class='form-group'>"
                str += "<label class='col-sm-3 col-md-3 control-label'>Código universitario</label>"
                str += "<div class='col-sm-3 col-md-2'>"
                str += "<input type='text' class='form-control' value='" + dt.Rows(i).Item("codigoUniver_Alu").ToString + "' readonly='readonly' >"
                str += "</div>"
                str += "<label class='col-sm-1 col-md-1 control-label'>Bachiller</label>"
                str += "<div class='col-sm-5 col-md-6'>"
                str += "<input type='text' class='form-control' value='" + dt.Rows(i).Item("alumno").ToString + "' readonly='readonly' >"
                str += "</div>"
                str += "</div>"
            Next
            Me.alumnos.InnerHtml = str
            Me.txtCarrera.Text = dt.Rows(0).Item("nombreOficial_cpf").ToString
            Me.txtTitulo.Text = dt.Rows(0).Item("Titulo_Tes").ToString
            Me.txtfechainforme.Text = dt.Rows(0).Item("fechainforme").ToString
            Me.txtFechaResolucion.Text = dt.Rows(0).Item("fecharesolucion").ToString
            Me.txtNroResolucion.Text = dt.Rows(0).Item("resolucion").ToString
            Me.txtFechaSustentacion.Text = dt.Rows(0).Item("fechasustentacion").ToString
            Me.txttimpoambiente.Text = dt.Rows(0).Item("tipoambiente_pst").ToString
            Me.txtAmbiente.Text = dt.Rows(0).Item("ambiente").ToString
            Me.txtDetalle.Text = dt.Rows(0).Item("detalle_pst").ToString
            Me.txtTipoJurado.Text = dt.Rows(0).Item("tipojurado").ToString
            Me.txtJurado.Text = dt.Rows(0).Item("jurado").ToString
            If dt.Rows(0).Item("archivofinal") <> "" Then
                Me.btnArchivo.OnClientClick = "fnDescargar('../../DescargarArchivo.aspx?Id=" + dt.Rows(0).Item("archivofinal") + "&idt=23');return false;"
            Else
                Me.btnArchivo.OnClientClick = "alert('No cuenta con un archivo de tesis');return false;"
            End If
        End If
        obj.CerrarConexion()
    End Sub

    Private Sub RegistrarNotaSustentacion(ByVal codigo_pst As Integer, ByVal codigo_Tes As Integer, ByVal codigo_jur As Integer, ByVal nota As Integer, ByVal observado As Integer, ByVal observacion As String, ByVal usuario As Integer)
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Dim dt As New Data.DataTable
        dt = obj.TraerDataTable("SUST_RegistrarNotaSustentacion", codigo_pst, codigo_Tes, codigo_jur, nota, observado, observacion, usuario)
        If dt.Rows.Count > 0 Then
            If dt.Rows(0).Item("Respuesta") = 1 Then
                Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "fnMensaje('success','" + dt.Rows(0).Item("Mensaje") + "')", True)
                Me.Lista.Visible = True
                Me.divCalificar.Visible = False
                ConsultarTesis(usuario, Request("ctf"), Me.ddlEstado.SelectedValue)
            Else
                Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "fnMensaje('error','" + dt.Rows(0).Item("Mensaje") + "')", True)

            End If
        End If
        obj.CerrarConexion()
    End Sub

    Protected Sub ddlEstado_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlEstado.SelectedIndexChanged
        If (Session("id_per") Is Nothing) Then
            Response.Redirect("../../../sinacceso.html")
        End If
        Try
            ConsultarTesis(Session("id_per"), Request("ctf"), Me.ddlEstado.SelectedValue)
            Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "Loading", "fnLoading(false)", True)
            Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "Change", "$('#ddlEstado').change(function() { fnLoading(true); });", True)
        Catch ex As Exception
            Response.Write(ex.Message.ToString)
        End Try
    End Sub

    Protected Sub btnCerrar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCerrar.Click
        Me.Lista.Visible = True
        Me.divCalificar.Visible = False
        Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "Loading", "fnLoading(false)", True)

    End Sub

    Protected Sub btnGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardar.Click
        If (Session("id_per") Is Nothing) Then
            Response.Redirect("../../../sinacceso.html")
        End If
        Try
            If ValidarRegistroNota() = True Then
                Dim observado As Integer = 0
                If Me.chkobservaciones.Checked = True Then
                    observado = 1
                Else
                    observado = 0
                End If
                RegistrarNotaSustentacion(Me.hdPst.Value, Me.hdtes.Value, Me.hdjur.Value, Me.ddlNota.SelectedValue, observado, Me.txtObservacionSustentacion.Text, Session("id_per"))
                Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "Loading", "fnLoading(false)", True)

            End If
        Catch ex As Exception
            Response.Write(ex.Message.ToString)
        End Try
    End Sub
    Private Function ValidarRegistroNota() As Boolean
        If Me.ddlNota.SelectedValue = "" Then
            Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "fnMensaje('error','Seleccione una nota')", True)
            Return False
        End If
        If Me.chkobservaciones.Checked = True Then
            If Me.txtObservacionSustentacion.Text = "" Then
                Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "fnMensaje('error','Ingrese Observación')", True)
                Return False
            End If
        End If
        Return True
    End Function
    Private Sub Limpiar()
        Me.hdPst.Value = 0
        Me.hdjur.Value = 0
        Me.hdtes.Value = 0
        Me.ddlNota.SelectedValue = ""
        Me.chkobservaciones.Checked = False
        Me.txtObservacionSustentacion.Text = ""
    End Sub

    Protected Sub btnAtras_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAtras.Click
        Me.DivAsesorias.Visible = False
        Me.Lista.Visible = True
        Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "Loading", "fnLoading(false)", True)

    End Sub

    Protected Sub btnGuardarObservacion_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardarObservacion.Click
        If (Session("id_per") Is Nothing) Then
            Response.Redirect("../../sinacceso.html")
        End If
        Try
            If validarRevision() = True Then
                GuardarRevision(Me.hdtes.Value, Me.hdjur.Value, Me.txtObservacion.Text, Session("id_per"))
            End If
        Catch ex As Exception
            Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "fnMensaje('error','" + ex.Message.ToString + "')", True)
        End Try
    End Sub

    Public Function validarRevision() As Boolean
        If Me.txtObservacion.Text = "" Then
            Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "fnMensaje('error','Ingrese observación')", True)
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
            Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "fnMensaje('success','" + dt.Rows(0).Item("Mensaje") + "')", True)

            'Me.lblMensajeObservación.InnerText = dt.Rows(0).Item("Mensaje").ToString
            'Me.lblMensajeObservación.Attributes.Add("class", "alert alert-success")
            'If Me.archivo.HasFile = True Then
            '    If Not (SubirArchivo(23, dt.Rows(0).Item("cod"), Me.archivo.PostedFile, 13)) Then
            '        Me.lblMensajeObservación.InnerText = "No se pudo subir archivo"
            '        Me.lblMensajeObservación.Attributes.Add("class", "alert alert-danger")
            '    End If
            'End If
            'End If
            'limpiarobservacion()
            CargarLineaTiempo(Me.hdtes.Value, Me.hdjur.Value)
        Else
            Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "fnMensaje('error','" + dt.Rows(0).Item("Mensaje") + "')", True)

            'Me.lblMensajeObservación.InnerText = dt.Rows(0).Item("Mensaje").ToString
            'Me.lblMensajeObservación.Attributes.Add("class", "alert alert-danger")
        End If
    End Sub

    Private Sub CargarLineaTiempo(ByVal codigo_tes As Integer, ByVal codigo_jur As Integer)
        Dim dt As New Data.DataTable
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        dt = obj.TraerDataTable("SUST_ListarObservacionesSustentacion", codigo_tes, codigo_jur)
        obj.CerrarConexion()

        Me.LineaDeTiempo.InnerHtml = ""
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

End Class

