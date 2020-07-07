Imports System.IO
Imports System.Collections.Generic

Partial Class GestionInvestigacion_FrmListaAsesorias
    Inherits System.Web.UI.Page

    Dim ruta As String = ConfigurationManager.AppSettings("SharedFiles")
    Dim ArchivoParaSubir As HttpPostedFile

    Protected Sub form1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles form1.Load
        If (Session("id_per") Is Nothing) Then
            Response.Redirect("../../sinacceso.html")
        End If
        If IsPostBack = False Then
            ListarCicloAcademico()
            'ListarDocentes()
            'If Request("ctf") <> 1 Then
            '    Dim obj As New ClsGestionInvestigacion
            '    Me.ddlDocente.SelectedValue = obj.EncrytedString64(Session("id_per"))
            'End If
        End If
        'mt_RefreshGrid()
    End Sub


    Private Sub ListarCicloAcademico()
        Dim obj As New ClsGestionInvestigacion
        Dim dt As New Data.DataTable
        dt = obj.ConsultarCicloAcademico("CVP", "")
        For i As Integer = 0 To dt.Rows.Count - 1
            Me.ddlSemestre.Items.Add(New ListItem(dt.Rows(i).Item("descripcion_cac"), dt.Rows(i).Item("codigo_cac")))
        Next
    End Sub


    Private Sub ListarAsesorias(ByVal codigo_cac As Integer, ByVal abrev_etapa As String, ByVal codigo_per As Integer)
        Dim obj As New ClsGestionInvestigacion
        Dim dt As New Data.DataTable
        dt = obj.ListarAsesorias(codigo_cac, abrev_etapa, codigo_per, Session("id_per"), Request("ctf"))
        Me.gvAsesorias.DataSource = dt
        Me.gvAsesorias.DataBind()
    End Sub

    Private Sub ListarDocentes()
        Dim obj As New ClsGestionInvestigacion
        Dim dt As New Data.DataTable
        Me.ddlDocente.Items.Clear()
        Me.ddlDocente.Items.Add(New ListItem("-- Seleccione --", ""))
        dt = obj.ListarDocenteAsesorTesis(Me.ddlSemestre.SelectedValue, Me.ddlEtapa.SelectedValue, Session("id_per"), Request("ctf"))
        For i As Integer = 0 To dt.Rows.Count - 1
            Me.ddlDocente.Items.Add(New ListItem(dt.Rows(i).Item("descripcion"), dt.Rows(i).Item("codigo")))
        Next
    End Sub

    Protected Sub ddlDocente_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlDocente.SelectedIndexChanged
        If (Session("id_per") Is Nothing) Then
            Response.Redirect("../../sinacceso.html")
        End If
        Me.gvAsesorias.DataSource = ""
        Me.gvAsesorias.DataBind()
        If Me.ddlDocente.SelectedValue <> "" And ddlSemestre.SelectedValue <> "" And ddlEtapa.SelectedValue <> "" Then
            ListarAsesorias(Me.ddlSemestre.SelectedValue, Me.ddlEtapa.SelectedValue, Me.ddlDocente.SelectedValue)
        End If
        Me.DivAsesorias.Visible = False
        Me.Asesorados.Visible = True
    End Sub

    Protected Sub ddlSemestre_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlSemestre.SelectedIndexChanged
        If (Session("id_per") Is Nothing) Then
            Response.Redirect("../../sinacceso.html")
        End If
        If Me.ddlSemestre.SelectedValue <> "" And Me.ddlEtapa.SelectedValue <> "" Then
            ListarDocentes()
        End If
        Me.gvAsesorias.DataSource = ""
        Me.gvAsesorias.DataBind()
    End Sub

    Protected Sub ddlEtapa_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlEtapa.SelectedIndexChanged
        If (Session("id_per") Is Nothing) Then
            Response.Redirect("../../sinacceso.html")
        End If
        If Me.ddlSemestre.SelectedValue <> "" And Me.ddlEtapa.SelectedValue <> "" Then
            ListarDocentes()
        End If
        Me.gvAsesorias.DataSource = ""
        Me.gvAsesorias.DataBind()
    End Sub

    Protected Sub gvAsesorias_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvAsesorias.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            'Dim porcentaje As String = Me.gvAsesorias.DataKeys(e.Row.RowIndex).Values("porcentaje").ToString
            'Dim nota As String = Me.gvAsesorias.DataKeys(e.Row.RowIndex).Values("nota").ToString
            Dim permisocalificar As String = Me.gvAsesorias.DataKeys(e.Row.RowIndex).Values("PermiteCalificar").ToString
            'If porcentaje <> "" And nota <> "" Then
            If permisocalificar = "0" Then
                e.Row.Cells(5).Enabled = False
                Dim btn As LinkButton = e.Row.Cells(6).FindControl("Guardar")
                btn.Visible = False

                'e.Row.Cells(6).Text = "Evaluado"
                'e.Row.Cells(6).ForeColor = Drawing.Color.Green
                'e.Row.Cells(6).Font.Bold = True
            End If
        End If
    End Sub

    Protected Sub gvAsesorias_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvAsesorias.RowCommand
        If (Session("id_per") Is Nothing) Then
            Response.Redirect("../../sinacceso.html")
        End If
        If e.CommandName = "Actualizar" Then
            Dim porcentaje_asesor As String
            Dim nota_asesor As String

            Dim seleccion As GridViewRow = gvAsesorias.Rows(e.CommandArgument)

            'Dim fechaini As DateTime = Me.gvAsesorias.DataKeys(seleccion.RowIndex).Values("fechaIni_Cro").ToString
            'Dim fechafin As DateTime = Me.gvAsesorias.DataKeys(seleccion.RowIndex).Values("fechaFin_Cro").ToString

            'Dim fechaactual As DateTime = Now
            'If DateTime.Compare(fechaactual, fechaini) >= 0 And DateTime.Compare(fechaactual, fechafin) <= 0 Then
            'Me.lblmensaje.InnerText = DateTime.Compare(fechaactual, fechaini).ToString + " - " + DateTime.Compare(fechaactual, fechafin)

            'End If

            Dim porcentaje As TextBox = seleccion.FindControl("txtPorcentaje")
            porcentaje_asesor = porcentaje.Text

            Dim nota As TextBox = seleccion.FindControl("txtNota")
            nota_asesor = nota.Text

            If ValidaSoloNumeros0a100(porcentaje.Text) = True And ValidaSoloNumeros0a20(nota.Text) = True Then
                Dim obj As New ClsGestionInvestigacion
                Dim dt As New Data.DataTable


                Me.lblmensaje.InnerText = ""
                Me.lblmensaje.Attributes.Remove("class")

                If nota_asesor <> "" And porcentaje_asesor <> "" Then

                    dt = obj.CalificacionAsesor(Me.gvAsesorias.DataKeys(seleccion.RowIndex).Values("codigo_tes"), Me.gvAsesorias.DataKeys(seleccion.RowIndex).Values("codigo_cac"), Me.gvAsesorias.DataKeys(seleccion.RowIndex).Values("codigo_RTes"), Me.gvAsesorias.DataKeys(seleccion.RowIndex).Values("codigo_Eti"), porcentaje_asesor, nota_asesor, Session("id_per"))

                    If dt.Rows(0).Item("Respuesta") = 1 Then
                        Me.lblmensaje.InnerText = dt.Rows(0).Item("Mensaje").ToString
                        Me.lblmensaje.Attributes.Add("class", "alert alert-success")

                    Else
                        Me.lblmensaje.InnerText = dt.Rows(0).Item("Mensaje").ToString
                        Me.lblmensaje.Attributes.Add("class", "alert alert-danger")
                    End If
                Else
                    Me.lblmensaje.InnerText = "Debe colocar porcentaje y nota de Tesis"
                    Me.lblmensaje.Attributes.Add("class", "alert alert-danger")
                End If

            Else
                Me.lblmensaje.InnerText = "solo se permiten numeros enteros de 0 a 100 para el porcentaje y de 0 a 20 para nota"
                Me.lblmensaje.Attributes.Add("class", "alert alert-danger")
            End If
            ListarAsesorias(Me.ddlSemestre.SelectedValue, Me.ddlEtapa.SelectedValue, Me.ddlDocente.SelectedValue)
            'mt_RefreshGrid()
        End If

        If e.CommandName = "Asesorias" Then

            Me.DivAsesorias.Visible = True
            Me.Asesorados.Visible = False
            Dim seleccion As GridViewRow = gvAsesorias.Rows(e.CommandArgument)
            Me.hdtes.Value = Me.gvAsesorias.DataKeys(seleccion.RowIndex).Values("codigo_tes")
            Me.hdetapa.Value = Me.gvAsesorias.DataKeys(seleccion.RowIndex).Values("codigo_Eti")
            Me.hdrtes.Value = Me.gvAsesorias.DataKeys(seleccion.RowIndex).Values("codigo_RTes")
            CargarTesis(Me.gvAsesorias.DataKeys(seleccion.RowIndex).Values("codigo_tes"))
            Me.lblMensajeObservación.InnerText = ""
            Me.lblMensajeObservación.Attributes.Remove("class")
            Me.txtObservacion.Text = ""
            CargarLineaTiempo(Me.gvAsesorias.DataKeys(seleccion.RowIndex).Values("codigo_tes"))
        End If
        mt_RefreshGrid()
    End Sub

    Function ValidaSoloNumeros0a100(ByVal cadena As String) As Boolean
        Try
            Dim estructura As String = "^([0-9]|[1-9][0-9]|100)$"
            Dim match As Match = Regex.Match(cadena.Trim(), estructura, RegexOptions.IgnoreCase)
            If match.Success Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Return False
        End Try
    End Function
    Function ValidaSoloNumeros0a20(ByVal cadena As String) As Boolean
        Try
            Dim estructura As String = "^([0-9]|[1][0-9]|20)$"
            Dim match As Match = Regex.Match(cadena.Trim(), estructura, RegexOptions.IgnoreCase)
            If match.Success Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Return False
        End Try
    End Function

    Protected Sub btnRegresar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRegresar.Click
        Me.DivAsesorias.Visible = False
        Me.Asesorados.Visible = True
        mt_RefreshGrid()
    End Sub

    Private Sub mt_RefreshGrid()
        Try
            For Each _Row As GridViewRow In Me.gvAsesorias.Rows
                gvAsesorias_RowDataBound(Me.gvAsesorias, New GridViewRowEventArgs(_Row))
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CargarTesis(ByVal codigo_tes As Integer)
        Dim obj As New ClsGestionInvestigacion
        Dim dt As New Data.DataTable
        dt = obj.ListarTesisxCodigo(codigo_tes)
        Me.txtAutores.Text = dt.Rows(0).Item("alumno").ToString
        Me.txtFacultad.Text = dt.Rows(0).Item("nombre_fac").ToString
        Me.txtCarrera.Text = dt.Rows(0).Item("nombre_cpf").ToString
        Me.txtLinea.Text = dt.Rows(0).Item("linea").ToString
        Me.txtArea.Text = dt.Rows(0).Item("descripcion_ocde").ToString
        Me.txtSubArea.Text = dt.Rows(0).Item("descripcion_sa_ocde").ToString
        Me.txtDisciplina.Text = dt.Rows(0).Item("descripcion_dis_ocde").ToString
        Me.txtPresupuesto.Text = dt.Rows(0).Item("presupuesto_tes").ToString
        Me.txtFinanciamiento.Text = dt.Rows(0).Item("Financiamiento").ToString
        Me.txtTitulo.Text = dt.Rows(0).Item("titulo_tes").ToString
        Me.txtObjetivoG.Text = dt.Rows(0).Item("objetivoG").ToString
        Me.txtObjetivoE.Text = dt.Rows(0).Item("objetivoE").ToString
        Me.divProyecto.InnerHtml = ""
        Me.divPreinforme.InnerHtml = ""
        Me.divInforme.InnerHtml = ""
        If dt.Rows(0).Item("proyecto").ToString <> "" Then
            Dim btn As New LinkButton
            btn.Text = "Descargar Proyecto <i class='ion-android-attach'></i>"
            btn.CssClass = "btn btn-sm btn-primary"
            btn.OnClientClick = "fnDescargar('" + dt.Rows(0).Item("proyecto") + "');return false;"
            Me.divProyecto.Controls.Add(btn)
        Else
            Me.divProyecto.InnerHtml = "Proyecto sin Adjunto"
        End If
        If dt.Rows(0).Item("preinforme").ToString <> "" Then
            Dim btn As New LinkButton
            btn.Text = "Descargar PreInforme <i class='ion-android-attach'></i>"
            btn.CssClass = "btn btn-sm btn-orange"
            btn.OnClientClick = "fnDescargar('" + dt.Rows(0).Item("preinforme") + "');return false;"
            Me.divPreinforme.Controls.Add(btn)
        Else
            Me.divPreinforme.InnerHtml = "Preinforme sin Adjunto"
        End If
        If dt.Rows(0).Item("informe").ToString <> "" Then
            Dim btn As New LinkButton
            btn.Text = "Descargar Informe <i class='ion-android-attach'></i>"
            btn.CssClass = "btn btn-sm btn-success"
            btn.OnClientClick = "fnDescargar('" + dt.Rows(0).Item("informe") + "');return false;"
            Me.divInforme.Controls.Add(btn)
        Else
            Me.divInforme.InnerHtml = "Informe sin Adjunto"
        End If
        If dt.Rows(0).Item("linkinforme").ToString <> "" Then
            Dim btn As New LinkButton
            btn.Text = "Link de  Informe"
            btn.CssClass = "btn btn-sm btn-warning btn-radius"
            btn.OnClientClick = "window.open('" + dt.Rows(0).Item("linkinforme") + "');return false;"
            Me.divEnlaceInforme.Controls.Add(btn)
        Else
            Me.divEnlaceInforme.InnerHtml = ""
        End If
    End Sub

    Protected Sub btnGuardarObservacion_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardarObservacion.Click
        If (Session("id_per") Is Nothing) Then
            Response.Redirect("../../sinacceso.html")
        End If
        Try
            If validar() = True Then
                Dim obj As New ClsGestionInvestigacion
                Dim dt As New Data.DataTable
                dt = obj.GuardarAsesoriaTesisVirtual(Me.hdtes.Value, Me.hdrtes.Value, Me.hdetapa.Value, Me.txtObservacion.Text, 0, Session("id_per"))
                If dt.Rows(0).Item("Respuesta") = "1" Then
                    Me.lblMensajeObservación.InnerText = dt.Rows(0).Item("Mensaje").ToString
                    Me.lblMensajeObservación.Attributes.Add("class", "alert alert-success")
                    If HttpContext.Current.Request.Files("archivo").ContentLength > 0 Then
                        ArchivoParaSubir = HttpContext.Current.Request.Files("archivo")
                        If Not (SubirArchivo(23, dt.Rows(0).Item("cod"), Me.archivo.PostedFile, 11)) Then
                            Me.lblMensajeObservación.InnerText = "No se pudo subir archivo"
                            Me.lblMensajeObservación.Attributes.Add("class", "alert alert-danger")
                        End If
                    End If
                    limpiarobservacion()
                    CargarLineaTiempo(Me.hdtes.Value)
                Else
                    Me.lblMensajeObservación.InnerText = dt.Rows(0).Item("Mensaje").ToString
                    Me.lblMensajeObservación.Attributes.Add("class", "alert alert-danger")
                End If
            End If
        Catch ex As Exception
            Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "fnMensaje('error','" + ex.Message.ToString + "')", True)
        End Try
    End Sub


    Function validar() As Boolean
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
                Me.lblMensajeObservación.InnerText = "Solo puede adjuntar archivos de proyecto en formato .doc,.docx,.jpg,.png,.pdf,.zip,.rar,.xls,.xlsx"
                Me.lblMensajeObservación.Attributes.Add("class", "alert alert-danger")
                Return False
            End If

        End If
        Return True
    End Function

    Private Sub limpiarobservacion()
        Me.txtObservacion.Text = ""
    End Sub

    Function SubirArchivo(ByVal idtabla As Integer, ByVal codigo As String, ByVal ArchivoSubir As HttpPostedFile, ByVal nro_operacion As Integer) As Boolean
        Try
            Dim dt As New Data.DataTable
            Dim obj As New ClsGestionInvestigacion

            Dim post As HttpPostedFile = ArchivoSubir
            Dim codigo_con As String = codigo
            Dim nrooperacion As String = nro_operacion 'NUMERO DE OPERACIÓN 11 PARA ARCHIVOS DE ASESORIAS
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
            list.Add("Nombre", System.IO.Path.GetFileName(post.FileName))
            list.Add("TransaccionId", codigo_con)
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
                dt = obj.ActualizarIDArchivoCompartidoAsesoria(idtabla, codigo, nrooperacion)
                Return True
            Else
                Return False
                Me.lblmensaje.InnerHtml = Me.lblmensaje.InnerHtml + "<div style='color:red'>Archivo no se pudo adjuntar</div>"
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
            Return False
        End Try
    End Function


    Private Sub CargarLineaTiempo(ByVal codigo_tes As Integer)
        Dim obj As New ClsGestionInvestigacion
        Dim dt As New Data.DataTable
        dt = obj.ListarAsesoriaTesisVirtual(codigo_tes)
        Me.LineaDeTiempo.InnerHtml = ""
        If dt.Rows.Count > 0 Then
            Dim verificar As Integer = 0
            Dim str As String = ""
            Dim color As String = "Red"
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
                    str += "<a  onclick='fnDescargar("""
                    str += dt.Rows(i).Item("archivo")
                    str += """);return false;'>Archivo adjunto <i class='ion-android-attach'></i></a>"
                End If
                str += "</div>"
                str += "<div class='col-sm-4  col-md-4'>"
                str += "</div>"
                str += "<div class='col-sm-4  col-md-4 text-right'>"
                If dt.Rows(i).Item("estado").ToString = "PENDIENTE" And dt.Rows(i).Item("responder").ToString = "1" Then
                    str += "<button id='btnResponder" + i.ToString + "' runat='server' class='btn btn-sm btn-primary' data-toggle='modal' data-target='#mdResponder' attr='" + dt.Rows(i).Item("codigo_atv").ToString + "' >Responder <i class='ion-chatbubble-working'></i></button>"
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



    Function validarRespuesta() As Boolean
        Me.lblMensajeRespuesta.InnerText = ""
        Me.lblMensajeRespuesta.Attributes.Remove("class")
        If Me.txtRespuesta.Text = "" Then
            Me.lblMensajeRespuesta.InnerText = "Ingrese Respuesta"
            Me.lblMensajeRespuesta.Attributes.Add("class", "alert alert-danger")
            Return False
        End If
        If Me.archivoRespuesta.HasFile = True Then
            If Me.archivoRespuesta.PostedFile.ContentLength > 20971520 Then
                Me.lblMensajeRespuesta.InnerText = "Solo puede adjuntar archivos de un tamaño máximo de 20MB"
                Me.lblMensajeRespuesta.Attributes.Add("class", "alert alert-danger")
                Return False
            End If
        End If
        Return True
    End Function

    Protected Sub btnGuardarRespuesta_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardarRespuesta.Click
        If (Session("id_per") Is Nothing) Then
            Response.Redirect("../../sinacceso.html")
        End If
        Try
            If validarRespuesta() = True Then
                Dim obj As New ClsGestionInvestigacion
                Dim dt As New Data.DataTable
                dt = obj.GuardarRespuestaAsesoria(Me.hdc.Value, Me.txtRespuesta.Text, Me.hdrtes.Value)
                If dt.Rows(0).Item("Respuesta") = "1" Then
                    Me.lblMensajeObservación.InnerText = dt.Rows(0).Item("Mensaje").ToString
                    Me.lblMensajeObservación.Attributes.Add("class", "alert alert-success")
                    If HttpContext.Current.Request.Files("ArchivoRespuesta").ContentLength > 0 Then
                        If Not (SubirArchivo(23, dt.Rows(0).Item("cod"), Me.ArchivoRespuesta.PostedFile, 12)) Then
                            Me.lblMensajeObservación.InnerText = "No se pudo subir archivo"
                            Me.lblMensajeObservación.Attributes.Add("class", "alert alert-danger")
                        End If
                    End If
                    Me.txtRespuesta.Text = ""
                    CargarLineaTiempo(Me.hdtes.Value)
                Else
                    Me.lblMensajeRespuesta.InnerText = dt.Rows(0).Item("Mensaje").ToString
                    Me.lblMensajeRespuesta.Attributes.Add("class", "alert alert-danger")
                End If
            End If
        Catch ex As Exception
            Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "fnMensaje('error','" + ex.Message.ToString + "')", True)
        End Try
    End Sub
End Class
