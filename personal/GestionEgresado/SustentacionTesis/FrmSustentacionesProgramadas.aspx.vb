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
                Me.lblMensajeObservación.InnerText = ""
                Me.lblMensajeObservación.Attributes.Remove("class")
                Me.lblmensaje.InnerHtml = ""
                Me.lblmensaje.Attributes.Remove("class")
                Me.hdPst.Value = Me.gvTesis.DataKeys(e.CommandArgument).Values("codigo_pst")
                Me.hdjur.Value = Me.gvTesis.DataKeys(e.CommandArgument).Values("codigo_jur")
                Me.hdtes.Value = Me.gvTesis.DataKeys(e.CommandArgument).Values("codigo_Tes")
                Me.hddta.Value = Me.gvTesis.DataKeys(e.CommandArgument).Values("codigo_dta")
                Me.hdtest.Value = Me.gvTesis.DataKeys(e.CommandArgument).Values("codigo_test")
                Me.hdfac.Value = Me.gvTesis.DataKeys(e.CommandArgument).Values("codigo_fac")

                ConsultarDatosProgramacion(Me.hdPst.Value, Me.hdjur.Value)
                Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "Loading", "fnLoading(false)", True)

            End If
            If (e.CommandName = "Observaciones") Then
                Me.Lista.Visible = False
                Me.DivAsesorias.Visible = True
                Me.hdPst.Value = Me.gvTesis.DataKeys(e.CommandArgument).Values("codigo_pst")
                Me.hdjur.Value = Me.gvTesis.DataKeys(e.CommandArgument).Values("codigo_jur")
                Me.hdtes.Value = Me.gvTesis.DataKeys(e.CommandArgument).Values("codigo_Tes")
                Me.hddta.Value = Me.gvTesis.DataKeys(e.CommandArgument).Values("codigo_dta")
                Me.txtTituloObservacion.Text = Me.gvTesis.DataKeys(e.CommandArgument).Values("titulo_tes")
                If Me.gvTesis.DataKeys(e.CommandArgument).Values("archivofinal") <> "" Then
                    Me.btnArchivoTesis.OnClientClick = "fnDescargar('../../DescargarArchivo.aspx?Id=" + Me.gvTesis.DataKeys(e.CommandArgument).Values("archivofinal") + "&idt=23');return false;"
                Else
                    Me.btnArchivoTesis.OnClientClick = "alert('No cuenta con un archivo de tesis');return false;"
                End If
                CargarLineaTiempo(Me.hdtes.Value, Me.hdjur.Value)
                Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "Loading", "fnLoading(false)", True)
                If Me.gvTesis.DataKeys(e.CommandArgument).Values("conformidad") = "0" Then
                    Me.btnGuardarObservacion.Visible = True
                    Me.btnConformidad.CssClass = "btn btn-sm btn-success btn-radius"
                    Me.btnConformidad.Text = "<span class='fa fa-check' ></span> Dar Conformidad"
                    Me.btnConformidad.OnClientClick = "return confirm('¿Está seguro que desea dar conformidad a tesis?')"
                    Me.txtObservacion.Enabled = True
                    If Me.gvTesis.DataKeys(e.CommandArgument).Values("condicion") = "DESAPROBADO" Then
                        Me.btnConformidad.Visible = False
                        Me.btnGuardarObservacion.Visible = False
                    Else
                        Me.btnGuardarObservacion.Visible = True
                    End If
                Else
                    Me.btnGuardarObservacion.Visible = False
                    Me.btnConformidad.CssClass = "btn btn-sm btn-primary btn-radius"
                    Me.btnConformidad.Text = "<span class='fa fa-thumbs-o-up' ></span> Conforme"
                    Me.btnConformidad.OnClientClick = "return false;"
                    Me.txtObservacion.Enabled = False
                    Me.btnConformidad.Visible = True

                End If

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
                btn2.OnClientClick = "fnDescargar('../../DescargarArchivo.aspx?Id=" + Me.gvTesis.DataKeys(e.Row.RowIndex).Values("resolucion") + "&idt=31');return false;"
                btn2.CssClass = "btn btn-sm btn-info btn-radius"
                btn2.ToolTip = "Descargar Resolución"
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

            Dim btnacta As LinkButton = DirectCast(e.Row.Cells(4).FindControl("btnActa"), LinkButton)
            If Me.gvTesis.DataKeys(e.Row.RowIndex).Values("archivoacta") <> "" Then
                btnacta.OnClientClick = "fnDescargar('../../DescargarArchivo.aspx?Id=" + Me.gvTesis.DataKeys(e.Row.RowIndex).Values("archivoacta") + "&idt=30');return false;"
                btnacta.Visible = True
            Else
                btnacta.Visible = False
            End If

            Dim btnrubrica As LinkButton = DirectCast(e.Row.Cells(4).FindControl("btnRubrica"), LinkButton)
            If Me.gvTesis.DataKeys(e.Row.RowIndex).Values("archivorubrica") <> "" Then
                btnrubrica.OnClientClick = "fnDescargar('../../DescargarArchivo.aspx?Id=" + Me.gvTesis.DataKeys(e.Row.RowIndex).Values("archivorubrica") + "&idt=23');return false;"
                btnrubrica.Visible = True
            Else
                btnrubrica.Visible = False
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
            Me.Alumnos.InnerHtml = str
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
                'Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "alertx", "fnMensaje('error','" + Me.archivo.HasFile.ToString + "')", True)
                If Me.archivo.HasFile = True Then
                    If Not (SubirArchivo(23, dt.Rows(0).Item("cod"), Me.archivo.PostedFile, 14)) Then
                        'Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "alertx", "fnMensaje('error','No se pudo subir archivo')", True)
                        Me.lblmensaje.InnerHtml = "No se pudo subir archivo de rúbrica<br>"
                        Me.lblMensaje.Attributes.Add("class", "alert alert-danger")
                    End If
                End If
                Me.lblmensaje.Attributes.Add("class", "alert alert-success")
                Me.lblmensaje.InnerHtml = Me.lblmensaje.InnerHtml + dt.Rows(0).Item("Mensaje")
                'Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "fnMensaje('success','" + dt.Rows(0).Item("Mensaje") + "')", True)
                If dt.Rows(0).Item("notascompletas") = 1 Then
                    Dim codigo_Dot As Integer = 0
                    codigo_Dot = GenerarActaSustentacion(Me.hdtes.Value, Session("id_per"), Session("perlogin").ToString, Me.hdtest.Value, Me.hdfac.Value)
                    If codigo_Dot > 0 Then
                        Dim dta As New Data.DataTable
                        dta = ActualizarCodigoActaSustentacion(Me.hdPst.Value, codigo_Dot)
                        If dta.Rows(0).Item("Respuesta") = "1" Then
                            'ACTUALIZAR ETAPA DE TRÁMITE
                            Dim dttramite As New Data.DataTable
                            If dt.Rows(0).Item("condicion") = "APROBADO" And dt.Rows(0).Item("actualizaetapa").ToString = "1" Then
                                dttramite = ActualizarEtapaTramite(Me.hddta.Value, 1, "A")
                            End If
                            If dt.Rows(0).Item("condicion") = "DESAPROBADO" Then
                                dttramite = ActualizarEtapaTramite(Me.hddta.Value, 1, "R")
                            End If
                            If dttramite.Rows.Count > 0 Then
                                If dttramite.Rows(0).Item("revision") = True Then ' SI SE HIZO LA REVISIÓN
                                    If dttramite.Rows(0).Item("email") = True Then 'SI SE ENVIO EL CORREO(INSER´CIÓN CORREOMASIVO)
                                        'Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "alert2", "fnMensaje('success','Se actualizó Etapa de trámite correctamente')", True)
                                        Me.lblmensaje.Attributes.Add("class", "alert alert-success")
                                        Me.lblmensaje.InnerHtml = Me.lblmensaje.InnerHtml + "<br>Se actualizó Etapa de trámite correctamente"
                                    Else  ' NO SE INSERTO ENVIO CORREO MASIVO
                                        'Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "alert2", "fnMensaje('success','Se actualizó Etapa de trámite correctamente, pero no se pudo realizar el envío de correo correctamente')", True)
                                        Me.lblmensaje.Attributes.Add("class", "alert alert-success")
                                        Me.lblmensaje.InnerHtml = Me.lblmensaje.InnerHtml + "<br>Se actualizó Etapa de trámite correctamente, pero no se pudo realizar el envío de correo correctamente"
                                    End If
                                Else ' NO SE ACTUALIZO LA ETAPA DEL TRAMITE
                                    Me.lblmensaje.Attributes.Add("class", "alert alert-danger")
                                    Me.lblmensaje.InnerHtml = Me.lblmensaje.InnerHtml + "<br>No se pudo actualizar la etapa del trámite"

                                    'Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "alert2", "fnMensaje('error','No se pudo actualizar la etapa del trámite')", True)
                                End If
                            End If
                            Me.lblmensaje.Attributes.Add("class", "alert alert-success")
                            Me.lblmensaje.InnerHtml = Me.lblmensaje.InnerHtml + "<br>Se generó acta de sustentación correctamente"

                            'Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "alert3", "fnMensaje('success','Se generó acta de sustentación correctamente')", True)
                        Else
                            Me.lblmensaje.Attributes.Add("class", "alert alert-danger")
                            Me.lblmensaje.InnerHtml = Me.lblmensaje.InnerHtml + "<br>" + dta.Rows(0).Item("Mensaje")
                            'Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "alert3", "fnMensaje('error','" + dta.Rows(0).Item("Mensaje") + "')", True)
                        End If
                    Else
                        'Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "alert2", "fnMensaje('error','No se pudo generar acta de sustentación')", True)
                        Me.lblmensaje.Attributes.Add("class", "alert alert-danger")
                        Me.lblmensaje.InnerHtml = Me.lblmensaje.InnerHtml + "<br>No se pudo generar acta de sustentación"
                    End If
                End If
                Me.Lista.Visible = True
                Me.divCalificar.Visible = False
                ConsultarTesis(usuario, Request("ctf"), Me.ddlEstado.SelectedValue)
            Else
                Me.lblMensajeObservación.Attributes.Add("class", "alert alert-danger")
                Me.lblMensajeObservación.InnerHtml = Me.lblmensaje.InnerHtml + "<br>" + dt.Rows(0).Item("Mensaje")
                'Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "fnMensaje('error','" + dt.Rows(0).Item("Mensaje") + "')", True)

            End If
        End If
        obj.CerrarConexion()
    End Sub


    Protected Sub btnCerrar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCerrar.Click
        Me.Lista.Visible = True
        Me.divCalificar.Visible = False
        Me.lblmensaje.InnerHtml = ""
        Me.lblmensaje.Attributes.Remove("class")
        Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "Loading45", "fnLoading(false)", True)
    End Sub

    Protected Sub btnGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardar.Click
        If (Session("id_per") Is Nothing) Then
            Response.Redirect("../../../sinacceso.html")
        End If
        Try
            Me.lblmensaje.InnerHtml = ""
            Me.lblmensaje.Attributes.Remove("class")
            If ValidarRegistroNota() = True Then
                Dim observado As Integer = 0
                If Me.chkobservaciones.Checked = True Then
                    observado = 1
                Else
                    observado = 0
                End If
                RegistrarNotaSustentacion(Me.hdPst.Value, Me.hdtes.Value, Me.hdjur.Value, Me.ddlNota.SelectedValue, observado, Me.txtObservacionSustentacion.Text, Session("id_per"))

                'Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "Loading", "fnLoading(false)", True)

            End If
        Catch ex As Exception
            Response.Write(ex.Message.ToString)
        End Try
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

    Private Function ValidarRegistroNota() As Boolean
        Me.lblmensaje.InnerHtml = ""
        Me.lblmensaje.Attributes.Remove("class")
        If Me.ddlNota.SelectedValue = "" Then
            'Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "fnMensaje('error','Seleccione una nota')", True)
            Me.lblMensajeObservación.InnerText = "Seleccione una nota"
            Me.lblMensajeObservación.Attributes.Add("class", "alert alert-danger")
            Return False
        End If
        If Me.archivo.HasFile = True Then
            If Me.archivo.PostedFile.ContentLength > 20971520 Then
                Me.lblMensajeObservación.InnerText = "Solo puede adjuntar archivos de un tamaño máximo de 20MB"
                Me.lblMensajeObservación.Attributes.Add("class", "alert alert-danger")
                Return False
            End If

            Dim Extensiones As String() = {".doc", ".docx", ".pdf", ".zip", ".rar", ".xls", ".xlsx"}
            Dim validarArchivo As Integer = -1
            validarArchivo = Array.IndexOf(Extensiones, System.IO.Path.GetExtension(archivo.FileName))
            If validarArchivo = -1 Then
                Me.lblMensajeObservación.InnerText = "Solo puede adjuntar archivos en formato .doc,.docx,.pdf,.zip,.rar,.xls,.xlsx"
                Me.lblMensajeObservación.Attributes.Add("class", "alert alert-danger")
                Return False
            End If
        End If
        If Me.chkobservaciones.Checked = True Then
            If Me.txtObservacionSustentacion.Text = "" Then
                Me.lblMensajeObservación.InnerText = "Ingrese observación"
                Me.lblMensajeObservación.Attributes.Add("class", "alert alert-danger")
                'Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "fnMensaje('error','Ingrese Observación')", True)
                Return False
            End If
        End If
        Return True
    End Function
    Private Sub Limpiar()
        Me.hdPst.Value = 0
        Me.hdjur.Value = 0
        Me.hdtes.Value = 0
        Me.hdfac.Value = 0
        Me.hddta.Value = 0
        Me.hdtest.Value = 0
        Me.ddlNota.SelectedValue = ""
        Me.chkobservaciones.Checked = False
        Me.txtObservacionSustentacion.Text = ""
        Me.txtObservacionSustentacion.Enabled = False
    End Sub

    Protected Sub btnAtras_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAtras.Click
        Me.DivAsesorias.Visible = False
        Me.Lista.Visible = True
        Me.lblmensaje.InnerHtml = ""
        Me.lblmensaje.Attributes.Remove("class")
        Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "Loading", "fnLoading(false)", True)

    End Sub

    Protected Sub btnGuardarObservacion_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardarObservacion.Click
        If (Session("id_per") Is Nothing) Then
            Response.Redirect("../../sinacceso.html")
        End If
        Try
            Me.lblmensaje.InnerHtml = ""
            Me.lblmensaje.Attributes.Remove("class")
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
            Me.txtObservacion.Text = ""
            Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "fnMensaje('success','" + dt.Rows(0).Item("Mensaje") + "')", True)
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

    Protected Sub chkobservaciones_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkobservaciones.CheckedChanged
        If Me.chkobservaciones.Checked = True Then
            Me.txtObservacionSustentacion.Enabled = True
        Else
            Me.txtObservacionSustentacion.Enabled = False
            Me.txtObservacionSustentacion.Text = ""
        End If
    End Sub

    Protected Sub btnConformidad_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnConformidad.Click
        If (Session("id_per") Is Nothing) Then
            Response.Redirect("../../sinacceso.html")
        End If
        Try
            Me.lblmensaje.InnerHtml = ""
            Me.lblmensaje.Attributes.Remove("class")
            DarConformidadTesis(Me.hdPst.Value, Me.hdtes.Value, Me.hdjur.Value, Session("id_per"))
            Me.Lista.Visible = True
            Me.DivAsesorias.Visible = False
        Catch ex As Exception
            Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "fnMensaje('error','" + ex.Message.ToString + "')", True)
        End Try
    End Sub

    Private Sub DarConformidadTesis(ByVal codigo_pst As Integer, ByVal codigo_Tes As Integer, ByVal codigo_jur As Integer, ByVal codigo_per As Integer)
        Dim dt As New Data.DataTable
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        dt = obj.TraerDataTable("SUST_ActualizarConformidadSustentacion", codigo_pst, codigo_Tes, codigo_jur, codigo_per)
        obj.CerrarConexion()
        If dt.Rows(0).Item("Respuesta") = "1" Then
            Me.txtObservacion.Text = ""
            'ACTUALIZAR ETAPA DE TRÁMITE
            Dim dttramite As New Data.DataTable
            If dt.Rows(0).Item("condicion") = "APROBADO" And dt.Rows(0).Item("actualizaetapa").ToString = "1" Then
                dttramite = ActualizarEtapaTramite(Me.hddta.Value, 1, "A")
            End If
            If dttramite.Rows.Count > 0 Then
                If dttramite.Rows(0).Item("revision") = True Then ' SI SE HIZO LA REVISIÓN
                    If dttramite.Rows(0).Item("email") = True Then 'SI SE ENVIO EL CORREO(INSER´CIÓN CORREOMASIVO)
                        Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "alert3", "fnMensaje('success','Se actualizó Etapa de trámite correctamente')", True)
                    Else  ' NO SE INSERTO ENVIO CORREO MASIVO
                        Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "alert3", "fnMensaje('success','Se actualizó Etapa de trámite correctamente, pero no se pudo realizar el envío de correo correctamente')", True)

                    End If
                Else ' NO SE ACTUALIZO LA ETAPA DEL TRAMITE
                    Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "alert2", "fnMensaje('error','No se pudo actualizar la etapa del trámite')", True)
                End If
            End If
            Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "fnMensaje('success','" + dt.Rows(0).Item("Mensaje") + "')", True)
            ConsultarTesis(Session("id_per"), Request("ctf"), Me.ddlEstado.SelectedValue)

        Else
            Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "fnMensaje('error','" + dt.Rows(0).Item("Mensaje") + "')", True)
            'Me.lblMensajeObservación.InnerText = dt.Rows(0).Item("Mensaje").ToString
            'Me.lblMensajeObservación.Attributes.Add("class", "alert alert-danger")
        End If
    End Sub


    Private Function GenerarActaSustentacion(ByVal codigo_Tes As Integer, ByVal codigo_per As Integer, ByVal usuario_per As String, ByVal codigo_fac As Integer, ByVal codigo_test As Integer) As Integer
        'Dim codigo_dot As Integer
        Dim codigo_cda As Integer = 5 ''-- Configuracion del documento
        Dim codigo_dot As Integer = 0 '- Codigo del doumento generado en la tabla DOC_documentoArchivo
        Dim serieCorrelativoDoc As String '- Serie o numeracion del comprobante

        Dim abreviatura_tid As String = "ACT" '---fijo 
        Dim abreviatura_doc As String = "ACST" '---fijo
        Dim abreviatura_are As String = ""
        If codigo_fac = 21 Then
            abreviatura_are = "PGRA" '---fijo / para postgrado

        Else
            abreviatura_are = "USAT" '---fijo / para pregrado
        End If
        '---

        ''-------------------------------genera el correlativo
        Try
            'Dim obj As New clsDocumentacion
            ''''**** 1. GENERA CORRELATIVO DEL DOCUMENTO CONFIGURADO *******************************************************
            'serieCorrelativoDoc = clsDocumentacion.ObtenerSerieCorrelativoDocPorCda(codigo_cda, Year(Now), codigo_per)
            serieCorrelativoDoc = clsDocumentacion.ObtenerSerieCorrelativoDocPorAbreviatura(abreviatura_tid, abreviatura_doc, abreviatura_are, Year(Now), codigo_per)

            ''''******* GENERA DOCUMENTO PDF *****************************************************************************
            If serieCorrelativoDoc <> "" Then
                '--------necesarios
                Dim arreglo As New Dictionary(Of String, String)
                arreglo.Add("nombreArchivo", "ActaDeSustentacion")
                arreglo.Add("sesionUsuario", usuario_per)
                '-----------------                
                arreglo.Add("codigo_tes", codigo_Tes)
                arreglo.Add("tipoEstudio", codigo_test)

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

    Private Function ActualizarCodigoActaSustentacion(ByVal codigo_pst As Integer, ByVal codigo_dot As Integer) As Data.DataTable
        Dim dt As New Data.DataTable
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        dt = obj.TraerDataTable("SUST_ActualizarCodigoDocumentoActa", codigo_pst, codigo_dot)
        obj.CerrarConexion()
        Return dt
    End Function

    Private Function ActualizarEtapaTramite(ByVal codigo_dta As Integer, ByVal tipooperacion As String, ByVal estadoaprobacion As String) As Data.DataTable
        Dim dt As New Data.DataTable
        dt.Columns.Add("revision")
        dt.Columns.Add("registros")
        dt.Columns.Add("email")
        Try
            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            Dim dtdatos As New Data.DataTable
            dtdatos = obj.TraerDataTable("SUST_DatosTramitesAutores", codigo_dta)
            obj.CerrarConexion()
            For i As Integer = 0 To dtdatos.Rows.Count - 1
                Dim cmp As New clsComponenteTramiteVirtualCVE
                Dim objcmp As New List(Of Dictionary(Of String, Object))()
                'cmp._codigo_dta = codigo_dta
                cmp._codigo_dta = dtdatos.Rows(i).Item("codigo_dta")
                'cmp.tipoOperacion = "1"
                cmp.tipoOperacion = tipooperacion
                cmp._codigo_per = Session("id_per")
                cmp._codigo_tfu = 251 ' tipo funcion JURADO
                If estadoaprobacion = "A" Then
                    cmp._estadoAprobacion = "A" ' DA CONFORMIDAD OSEA APRUEBA
                    cmp._observacionEvaluacion = "Se Aprobó instancia de trámite"
                Else
                    cmp._estadoAprobacion = "R"
                    cmp._observacionEvaluacion = "Se Rechaza trámite debido a que Bachiller desaprobó en el acto de sustentación"
                End If
                objcmp = cmp.mt_EvaluarTramite()

                For Each fila As Dictionary(Of String, Object) In objcmp
                    dt.Rows.Add(fila.Item("evaluacion"), fila.Item("registos evaluados").ToString, fila.Item("email"))
                Next
            Next
            Return dt
        Catch ex As Exception

            dt.Rows.Add(False, "", False)

            Return dt
        End Try
    End Function


    Protected Sub ddlEstado_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlEstado.SelectedIndexChanged
        If (Session("id_per") Is Nothing) Then
            Response.Redirect("../../../sinacceso.html")
        End If
        Try
            Me.lblmensaje.InnerHtml = ""
            Me.lblmensaje.Attributes.Remove("class")
            'ConsultarTesis(Session("id_per"), Request("ctf"), Me.ddlEstado.SelectedValue, Me.txtFecha.Text)
            ConsultarTesis(Session("id_per"), Request("ctf"), Me.ddlEstado.SelectedValue)
            Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "Loading", "fnLoading(false)", True)
            'Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "Change2", "Calendario();", True)
        Catch ex As Exception
            Response.Write(ex.Message.ToString)
        End Try
    End Sub
End Class

