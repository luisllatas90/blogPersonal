Imports System.Data
Imports System.IO
Imports System.Collections.Generic

Partial Class FrmRevisionTesisAsesor
    Inherits System.Web.UI.Page
    Dim ruta As String = ConfigurationManager.AppSettings("SharedFiles")
   
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (Session("id_per") Is Nothing) Then
            Response.Redirect("../../../sinacceso.html")
        End If
        If IsPostBack = False Then
            ConsultarTesis(Session("id_per"), Request("ctf"), Me.ddlEstado.SelectedValue)
        End If
    End Sub

    Private Sub ConsultarTesis(ByVal codigo_per As Integer, ByVal codigo_ctf As Integer, ByVal estado As String)
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Dim dt As New Data.DataTable
        dt = obj.TraerDataTable("SUST_ListarTesisxAsesor", codigo_per, codigo_ctf, estado)
        obj.CerrarConexion()
        Me.gvAlumnos.DataSource = dt
        Me.gvAlumnos.DataBind()
    End Sub

    Private Function ActualizarConformidad(ByVal codigo_Rtes As Integer, ByVal conformidad As Integer) As Data.DataTable
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Dim dt As New Data.DataTable
        dt = obj.TraerDataTable("SUST_ActualizarConformidadTesis", codigo_Rtes, conformidad)
        obj.CerrarConexion()
        Return dt
    End Function

    Private Function ActualizarBloqueoTesis(ByVal codigo_bat As Integer, ByVal codigo_Tes As Integer, ByVal codigo_Rtes As Integer, ByVal codigo_per As Integer, ByVal bloquear As Integer) As Data.DataTable
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Dim dt As New Data.DataTable
        dt = obj.TraerDataTable("SUST_ActualizarBloqueoAsesorTesis", codigo_bat, codigo_Tes, codigo_Rtes, codigo_per, bloquear)
        obj.CerrarConexion()
        Return dt
    End Function

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

    Dim LastCategory As String = String.Empty
    Dim CurrentRow As Integer = -1

    Protected Sub gvAlumnos_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvAlumnos.RowCommand
        If (Session("id_per") Is Nothing) Then
            Response.Redirect("../../../sinacceso.html")
        End If
        Try
            If (e.CommandName = "Conformidad") Then
                Dim codigo_Rtes As Integer = Me.gvAlumnos.DataKeys(e.CommandArgument).Values("codigo_RTes")
                Dim codigo_tes As Integer = Me.gvAlumnos.DataKeys(e.CommandArgument).Values("codigo_Tes")
                'Dar Conformidad
                Dim dt As New Data.DataTable
                dt = ActualizarConformidad(codigo_Rtes, 1)
                If dt.Rows.Count() > 0 Then
                    If dt.Rows(0).Item("Respuesta") = 1 Then ' dio conformidad
                        'Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "fnMensaje('success','" + dt.Rows(0).Item("Mensaje") + "')", True)
                        Dim codigo_dot As Integer = 0
                        codigo_dot = GenerarInformeAsesor(codigo_tes, Session("id_per"), Session("perlogin").ToString)
                        If codigo_dot > 0 Then
                            ACtualizarCodigoDocumento(codigo_Rtes, codigo_dot)
                            Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "fnMensaje('success','Informe de asesor generado correctamente')", True)
                        Else
                            Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "fnMensaje('error','No se pudo generar informe de asesor')", True)

                        End If
                        Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "fnMensaje('success','" + dt.Rows(0).Item("Mensaje") + "')", True)

                        ConsultarTesis(Session("id_per"), Request("ctf"), Me.ddlEstado.SelectedValue)
                    Else
                        Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "fnMensaje('error','" + dt.Rows(0).Item("Mensaje") + "')", True)
                    End If
                End If
            End If
            If (e.CommandName = "Bloquear") Then
                Dim codigo_tes As Integer = Me.gvAlumnos.DataKeys(e.CommandArgument).Values("codigo_Tes")
                Dim codigo_Rtes As Integer = Me.gvAlumnos.DataKeys(e.CommandArgument).Values("codigo_RTes")
                Dim bloqueo As Integer = 1
                Dim codigo_bat As Integer = 0 ' nuevo registro
                'quitar bloqueo
                If Me.gvAlumnos.DataKeys(e.CommandArgument).Values("BloqueadoxAsesor") > 0 Then ' si esta bloqueado, desbloquear
                    bloqueo = 0
                    codigo_bat = Me.gvAlumnos.DataKeys(e.CommandArgument).Values("BloqueadoxAsesor")
                End If
                Dim dt As New Data.DataTable
                dt = ActualizarBloqueoTesis(codigo_bat, codigo_tes, codigo_Rtes, Session("id_per"), bloqueo)
                If dt.Rows.Count() > 0 Then
                    If dt.Rows(0).Item("Respuesta") = 1 Then
                        Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "fnMensaje('success','" + dt.Rows(0).Item("Mensaje") + "')", True)
                        ConsultarTesis(Session("id_per"), Request("ctf"), Me.ddlEstado.SelectedValue)
                    Else
                        Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "fnMensaje('error','" + dt.Rows(0).Item("Mensaje") + "')", True)
                    End If
                End If
            End If
            If (e.CommandName = "Asesorias") Then
                Me.Lista.Visible = False
                Me.DivAsesorias.Visible = True
                Me.hdrtes.Value = Me.gvAlumnos.DataKeys(e.CommandArgument).Values("codigo_RTes")
                Me.hdtes.Value = Me.gvAlumnos.DataKeys(e.CommandArgument).Values("codigo_Tes")
                ConsultarDatosTesis(Me.gvAlumnos.DataKeys(e.CommandArgument).Values("codigo_Tes"))
                CargarLineaTiempo(Me.gvAlumnos.DataKeys(e.CommandArgument).Values("codigo_Tes"))
                limpiarobservacion()
                Me.lblMensajeObservación.InnerText = ""
                Me.lblMensajeObservación.Attributes.Remove("class")
                If Me.gvAlumnos.DataKeys(e.CommandArgument).Values("fechaconformidad") = "" Then
                    Me.txtObservacion.Enabled = True
                    Me.archivo.Enabled = True
                    Me.btnGuardarObservacion.Visible = True
                Else
                    Me.txtObservacion.Enabled = False
                    Me.archivo.Enabled = False
                    Me.btnGuardarObservacion.Visible = False
                End If
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub


    Protected Sub gvAlumnos_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvAlumnos.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then

            Dim row As Data.DataRowView = CType(e.Row.DataItem, Data.DataRowView)
            If LastCategory = row("codigo_tes").ToString Then

                'If (gvAlumnos.Rows(CurrentRow).Cells(0).RowSpan = 0) Then
                '    gvAlumnos.Rows(CurrentRow).Cells(0).RowSpan = 2
                'Else
                '    gvAlumnos.Rows(CurrentRow).Cells(0).RowSpan += 1
                'End If
                'e.Row.Cells(0).Visible = False

                If (gvAlumnos.Rows(CurrentRow).Cells(0).RowSpan = 0) Then
                    gvAlumnos.Rows(CurrentRow).Cells(0).RowSpan = 2
                Else
                    gvAlumnos.Rows(CurrentRow).Cells(0).RowSpan += 1
                End If
                e.Row.Cells(0).Visible = False

                If (gvAlumnos.Rows(CurrentRow).Cells(2).RowSpan = 0) Then
                    gvAlumnos.Rows(CurrentRow).Cells(2).RowSpan = 2
                Else
                    gvAlumnos.Rows(CurrentRow).Cells(2).RowSpan += 1
                End If
                e.Row.Cells(2).Visible = False

                If (gvAlumnos.Rows(CurrentRow).Cells(3).RowSpan = 0) Then
                    gvAlumnos.Rows(CurrentRow).Cells(3).RowSpan = 2
                Else
                    gvAlumnos.Rows(CurrentRow).Cells(3).RowSpan += 1
                End If
                e.Row.Cells(3).Visible = False

                If (gvAlumnos.Rows(CurrentRow).Cells(4).RowSpan = 0) Then
                    gvAlumnos.Rows(CurrentRow).Cells(4).RowSpan = 2
                Else
                    gvAlumnos.Rows(CurrentRow).Cells(4).RowSpan += 1
                End If
                e.Row.Cells(4).Visible = False

                If (gvAlumnos.Rows(CurrentRow).Cells(5).RowSpan = 0) Then
                    gvAlumnos.Rows(CurrentRow).Cells(5).RowSpan = 2
                Else
                    gvAlumnos.Rows(CurrentRow).Cells(5).RowSpan += 1
                End If
                e.Row.Cells(5).Visible = False
            Else
                e.Row.VerticalAlign = VerticalAlign.Middle
                LastCategory = row("codigo_tes").ToString()
                CurrentRow = e.Row.RowIndex
            End If
            'Boton descargar
            If Me.gvAlumnos.DataKeys(e.Row.RowIndex).Values("archivofinal") <> "" Then
                Dim btn As LinkButton = DirectCast(e.Row.Cells(2).FindControl("btnDescargar"), LinkButton)
                btn.Text = "<span class='fa fa-download'></span>"
                btn.OnClientClick = "fnDescargar('../../DescargarArchivo.aspx?Id=" + Me.gvAlumnos.DataKeys(e.Row.RowIndex).Values("archivofinal") + "&idt=23');return false;"
                btn.CssClass = "btn btn-sm btn-info btn-radius"
            End If
            'Boton Bloquear
            If Me.gvAlumnos.DataKeys(e.Row.RowIndex).Values("BloqueadoxAsesor") <> "0" Then
                Dim btn As LinkButton = DirectCast(e.Row.Cells(3).FindControl("btnBloquear"), LinkButton)
                btn.Text = "<span class='fa fa-unlock'></span>"
                btn.CssClass = "btn btn-sm btn-primary btn-radius"
                btn.ToolTip = "Desbloquear"
                btn.OnClientClick = "return confirm('¿Está seguro que desea desbloquear la tesis?')"
            End If
            If Me.gvAlumnos.DataKeys(e.Row.RowIndex).Values("fechaconformidad").ToString <> "" Then
                Dim btn As LinkButton = DirectCast(e.Row.Cells(5).FindControl("btnConforme"), LinkButton)
                Me.gvAlumnos.Columns(3).Visible = False
                If Me.gvAlumnos.DataKeys(e.Row.RowIndex).Values("informeasesor").ToString = "0" Then
                    btn.Visible = False
                Else
                    btn.Text = "<span class='fa fa-download'></span>"
                    btn.CssClass = "btn btn-sm btn-danger btn-radius"
                    btn.ToolTip = "Descargar Informe de asesor"
                    btn.OnClientClick = "fnDescargar('../../DescargarArchivo.aspx?Id=" + Me.gvAlumnos.DataKeys(e.Row.RowIndex).Values("informeasesor") + "&idt=30');return false;"
                    btn.Visible = True
                End If
            Else
                Me.gvAlumnos.Columns(3).Visible = True
            End If
        End If
    End Sub

    Protected Sub ddlEstado_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlEstado.SelectedIndexChanged
        If (Session("id_per") Is Nothing) Then
            Response.Redirect("../../../sinacceso.html")
        End If
        ConsultarTesis(Session("id_per"), Request("ctf"), Me.ddlEstado.SelectedValue)
    End Sub


    Protected Sub btnAtras_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAtras.Click
        Me.DivAsesorias.Visible = False
        Me.Lista.Visible = True
    End Sub

    Protected Sub btnGuardarObservacion_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardarObservacion.Click
        If (Session("id_per") Is Nothing) Then
            Response.Redirect("../../sinacceso.html")
        End If
        Try
            If validarRevision() = True Then
                GuardarRevision(Me.hdtes.Value, Me.hdrtes.Value, Me.txtObservacion.Text)
            End If
        Catch ex As Exception
            Me.lblMensajeObservación.InnerText = ex.Message.ToString
            Me.lblMensajeObservación.Attributes.Add("class", "alert alert-danger")
        End Try
    End Sub

    Public Function validarRevision() As Boolean
        Me.lblMensajeObservación.InnerText = ""
        Me.lblMensajeObservación.Attributes.Remove("class")
        If Me.txtObservacion.Text = "" Then
            Me.lblMensajeObservación.InnerText = "Ingrese observación"
            Me.lblMensajeObservación.Attributes.Add("class", "alert alert-danger")
            Return False
        End If
        If Me.archivo.HasFile = True Then
            If Me.archivo.PostedFile.ContentLength > 20971520 Then
                Me.lblMensajeObservación.InnerText = "Solo puede adjuntar archivos de un tamaño máximo de 20MB"
                Me.lblMensajeObservación.Attributes.Add("class", "alert alert-danger")
                Return False
            End If

            Dim Extensiones As String() = {".doc", ".docx", ".pdf", ".jpg", ".jpeg", ".png", ".zip", ".rar", ".xls", ".xlsx"}
            Dim validarArchivo As Integer = -1
            validarArchivo = Array.IndexOf(Extensiones, System.IO.Path.GetExtension(archivo.FileName))
            If validarArchivo = -1 Then
                Me.lblMensajeObservación.InnerText = "Solo puede adjuntar archivos en formato .doc,.docx,.jpg,.png,.pdf,.zip,.rar,.xls,.xlsx"
                Me.lblMensajeObservación.Attributes.Add("class", "alert alert-danger")
                Return False
            End If
        End If
        Return True
    End Function

    Private Sub GuardarRevision(ByVal codigo_Tes As Integer, ByVal codigo_Rtes As Integer, ByVal descripcion As String)

        Dim dt As New Data.DataTable
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        dt = obj.TraerDataTable("SUST_GuardarObservacionAsesorTesis", codigo_Tes, codigo_Rtes, descripcion)
        obj.CerrarConexion()
        If dt.Rows(0).Item("Respuesta") = "1" Then
            Me.lblMensajeObservación.InnerText = dt.Rows(0).Item("Mensaje").ToString
            Me.lblMensajeObservación.Attributes.Add("class", "alert alert-success")
            If Me.archivo.HasFile = True Then
                If Not (SubirArchivo(23, dt.Rows(0).Item("cod"), Me.archivo.PostedFile, 9)) Then
                    Me.lblMensajeObservación.InnerText = "No se pudo subir archivo"
                    Me.lblMensajeObservación.Attributes.Add("class", "alert alert-danger")

                End If
            End If
            'End If
            limpiarobservacion()
            CargarLineaTiempo(Me.hdtes.Value)

        Else
            Me.lblMensajeObservación.InnerText = dt.Rows(0).Item("Mensaje").ToString
            Me.lblMensajeObservación.Attributes.Add("class", "alert alert-danger")
        End If
    End Sub

    Private Sub limpiarobservacion()
        Me.txtObservacion.Text = ""
    End Sub

    Function SubirArchivo(ByVal idtabla As Integer, ByVal codigo As String, ByVal ArchivoSubir As HttpPostedFile, ByVal nro_operacion As Integer) As Boolean
        Try
            Dim dt As New Data.DataTable

            Dim post As HttpPostedFile = ArchivoSubir
            Dim codigo_obs As String = codigo
            Dim nrooperacion As String = nro_operacion 'NUMERO DE OPERACIÓN
            Dim Fecha As String = Date.Now.ToString("dd/MM/yyyy")
            Dim Usuario As String = Session("perlogin").ToString
            Dim Input(post.ContentLength) As Byte
            ' Dim b As New BinaryReader(post.InputStream)
            '  Dim by() As Byte = b.ReadByte(post.ContentLength)

            Dim b As New BinaryReader(post.InputStream)
            Dim binData As Byte() = b.ReadBytes(post.InputStream.Length)
            Dim base64 = System.Convert.ToBase64String(binData)

            Dim wsCloud As New ClsArchivosCompartidos
            Dim list As New Dictionary(Of String, String)
            '  Dim list As New List(Of Dictionary(Of String, String))()
            list.Add("Fecha", Fecha)
            list.Add("Extencion", System.IO.Path.GetExtension(post.FileName))
            list.Add("Nombre", Regex.Replace(System.IO.Path.GetFileName(post.FileName), "[^0-9A-Za-z._ ]", ""))
            list.Add("TransaccionId", codigo_obs)
            list.Add("TablaId", idtabla)
            list.Add("NroOperacion", nrooperacion)
            list.Add("Archivo", System.Convert.ToBase64String(binData, 0, binData.Length))
            list.Add("Usuario", Usuario)
            list.Add("Equipo", "")
            list.Add("Ip", "")
            list.Add("param8", Usuario)

            Dim envelope As String = wsCloud.SoapEnvelope(list)
            Dim result As String = wsCloud.PeticionRequestSoap(ruta, envelope, "http://usat.edu.pe/UploadFile", Session("perlogin").ToString)
            'Me.TextBox1.Text = envelope
            If result.Contains("Registro procesado correctamente") Then
                ActualizarArchivoCompartido(idtabla, codigo_obs, nrooperacion)
                Return True
            Else
                Return False
                '    Me.lblmensaje.InnerHtml = Me.lblmensaje.InnerHtml + "<div style='color:red'>Archivo no se pudo adjuntar</div>"
            End If

            'Me.lblMensaje.InnerText = envelope
            'Response.Write(result)
        Catch ex As Exception
            'Dim Data1 As New Dictionary(Of String, Object)()
            'Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
            'Dim JSONresult As String = ""
            'Dim list As New List(Of Dictionary(Of String, Object))()
            'Data1.Add("rpta", "1 - SUBIR ARCHIVO")
            'Data1.Add("msje", ex.Message)
            'list.Add(Data1)
            'JSONresult = serializer.Serialize(list)
            'Me.txtbusqueda.Text = JSONresult
            'Me.TextBox1.Text = JSONresult
            'ClientScript.RegisterClientScriptBlock(Me.GetType(), "alerta", "<script>alert('" + JSONresult + "')</script>")
            'Response.Write(JSONresult)
            'Me.lblmensaje.InnerHtml = Me.lblmensaje.InnerHtml + "<div class='row' style='color:red'>" + ex.Message + "</div>"
            Me.lblMensajeObservación.InnerText = ex.Message.ToString
            Return False
        End Try
    End Function
    Private Sub ActualizarArchivoCompartido(ByVal idtabla As Integer, ByVal codigo As Integer, ByVal nro_operacion As Integer)
        Dim dt As New Data.DataTable
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        dt = obj.TraerDataTable("SUST_ActualizarIDArchivoCompartido", idtabla, codigo, nro_operacion)
        obj.CerrarConexion()

    End Sub

    Private Sub CargarLineaTiempo(ByVal codigo_tes As Integer)
        Dim dt As New Data.DataTable
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        dt = obj.TraerDataTable("SUST_ListarObservacionesTesisAsesor", codigo_tes)
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


    Protected Sub btnGuardarRespuesta_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardarRespuesta.Click
        If (Session("id_per") Is Nothing) Then
            Response.Redirect("../../sinacceso.html")
        End If
        Dim dt As New Data.DataTable
        dt = GuardarRespuesta(Me.hdc.Value, Me.txtRespuesta.Text, Me.hdrtes.Value)
        If dt.Rows.Count > 0 Then
            If dt.Rows(0).Item("Respuesta") = "1" Then
                Me.lblMensajeObservación.InnerText = dt.Rows(0).Item("Mensaje").ToString
                Me.lblMensajeObservación.Attributes.Add("class", "alert alert-success")
                If Me.ArchivoRespuesta.HasFile = True Then
                    If Not (SubirArchivo(23, dt.Rows(0).Item("cod"), Me.ArchivoRespuesta.PostedFile, 10)) Then
                        Me.lblMensajeObservación.InnerText = "No se pudo subir archivo"
                        Me.lblMensajeObservación.Attributes.Add("class", "alert alert-danger")
                    End If
                End If
                'End If
                Me.txtRespuesta.Text = ""
                CargarLineaTiempo(Me.hdtes.Value)
            Else
                Me.lblMensajeObservación.InnerText = dt.Rows(0).Item("Mensaje").ToString
                Me.lblMensajeObservación.Attributes.Add("class", "alert alert-danger")
            End If
        Else
            Me.lblMensajeObservación.InnerText = "No se pudo registrar respuesta"
            Me.lblMensajeObservación.Attributes.Add("class", "alert alert-danger")
        End If
    End Sub
    Private Function GuardarRespuesta(ByVal codigo_ost As Integer, ByVal descripcion As String, ByVal codigo_Rtes As Integer) As Data.DataTable
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Dim dt As New Data.DataTable
        dt = obj.TraerDataTable("SUST_GuardarRespuestaConsultaAlumno", codigo_ost, descripcion, codigo_Rtes)
        obj.CerrarConexion()
        Return dt
    End Function

    Private Function ACtualizarCodigoDocumento(ByVal codigo_rtes As Integer, ByVal codigo_dot As Integer) As Data.DataTable
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Dim dt As New Data.DataTable
        dt = obj.TraerDataTable("SUST_ActualizarCodigoDocumentoAsesor", codigo_rtes, codigo_dot)
        obj.CerrarConexion()
        Return dt
    End Function

    Private Function GenerarInformeAsesor(ByVal codigo_Tes As Integer, ByVal codigo_per As Integer, ByVal usuario_per As String) As Integer
        'Dim codigo_dot As Integer
        Dim codigo_cda As Integer = 4 ''-- Configuracion del documento
        Dim codigo_dot As Integer = 0 '- Codigo del doumento generado en la tabla DOC_documentoArchivo
        Dim serieCorrelativoDoc As String '- Serie o numeracion del comprobante
        Try
            'Dim obj As New clsDocumentacion
            ''''**** 1. GENERA CORRELATIVO DEL DOCUMENTO CONFIGURADO *******************************************************
            serieCorrelativoDoc = clsDocumentacion.ObtenerSerieCorrelativoDocPorCda(codigo_cda, Year(Now), codigo_per)

            ''''******* GENERA DOCUMENTO PDF *****************************************************************************
            If serieCorrelativoDoc <> "" Then
                '--------necesarios
                Dim arreglo As New Dictionary(Of String, String)
                arreglo.Add("nombreArchivo", "InformeDeAsesor")
                arreglo.Add("sesionUsuario", usuario_per)
                '-----------------                
                arreglo.Add("codigo_tes", codigo_Tes)

                '******** 2. GENERA DOCUMENTO PDF **************************************************************
                codigo_dot = clsDocumentacion.generarDocumentoPdf(serieCorrelativoDoc, arreglo)
                'codigo_dot = clsDocumentacion.generarDocumentoPdf(serieCorrelativoDoc, memory, arreglo)
                '**********************************************************************************************
            Else
                codigo_dot = 0
            End If

        Catch ex As Exception
            codigo_dot = 0
        End Try
        Return codigo_dot
    End Function


End Class

