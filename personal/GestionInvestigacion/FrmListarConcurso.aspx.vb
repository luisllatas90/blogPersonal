Imports System.IO
Imports System.Collections.Generic

Partial Class GestionInvestigacion_FrmListarConcurso
    Inherits System.Web.UI.Page

    Dim ruta As String = "http://serverdev/campusvirtual/ArchivosCompartidos/SharedFiles.asmx"
    'Dim ruta As String = "http://serverQA/campusvirtual/ArchivosCompartidos/SharedFiles.asmx"
    'Dim ruta As String = "http://localhost/campusvirtual/ArchivosCompartidos/SharedFiles.asmx"

    Private Sub ListarConcursos()
        If (Session("id_per") Is Nothing) Then
            Response.Redirect("../../sinacceso.html")
        End If
        Dim obj As New ClsGestionInvestigacion
        Dim dt As New Data.DataTable
        dt = obj.ListarConcurso("L", "%", Me.txtbusqueda.Text, Me.ddlEstado.SelectedValue, "%", Session("id_per"), Request("ctf"))

        Me.gvConcursos.DataSource = dt
        Me.gvConcursos.DataBind()

    End Sub



    Protected Sub form1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles form1.Load
        Try
            ListarConcursos()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub btnConsultar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnConsultar.Click
        Mensaje("", "")
        If Me.ddlEstado.SelectedValue <> "" Then
            ListarConcursos()
        Else
            Me.gvConcursos.DataSource = ""
            Me.gvConcursos.DataBind()
        End If
    End Sub

    Public Sub sub_Editar(ByVal sender As Object, ByVal e As System.EventArgs)
        Me.txtbusqueda.Text = Me.gvConcursos.SelectedDataKey("codigo_con").ToString
    End Sub


    Protected Sub gvConcursos_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvConcursos.RowCommand
        If e.CommandName = "EliminarConcurso" Then
            Me.cod_con.Value = Me.gvConcursos.DataKeys(e.CommandArgument).Values("codigo_con")
            'Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "MostrarModal('1')", True)
            'Response.Write("<script>MostrarModal('1')</script>")
            'Mostrar Modal de Confirmación
            Me.mdConfirmacion.Attributes.Remove("class")
            Me.mdConfirmacion.Attributes.Add("class", "modal fade in")
            Me.mdConfirmacion.Attributes.CssStyle.Remove("display")
            Me.mdConfirmacion.Attributes.CssStyle.Add("display", "block")

        End If
        If e.CommandName = "EditarConcurso" Then
            LimpiarControles()
            Me.cod_con.Value = Me.gvConcursos.DataKeys(e.CommandArgument).Values("codigo_con")
            'Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "MostrarModal('1')", True)
            'Response.Write("<script>MostrarModal('1')</script>")
            'Mostrar Modal de Confirmación
            Me.mdRegistro.Attributes.Remove("class")
            Me.mdRegistro.Attributes.Add("class", "modal fade in")
            Me.mdRegistro.Attributes.CssStyle.Remove("display")
            Me.mdRegistro.Attributes.CssStyle.Add("display", "block")
            CargarDatosConcurso(Me.cod_con.Value)
        End If
    End Sub

    Private Sub CargarDatosConcurso(ByVal codigo_con As Integer)
        If (Session("id_per") Is Nothing) Then
            Response.Redirect("../../sinacceso.html")
        End If
        Try

            Dim dt As New Data.DataTable
            Dim obj As New ClsGestionInvestigacion
            dt = obj.ListarConcurso("E", codigo_con, "", "0", 0, Session("id_per"), Request("ctf"))
            Me.cod_con.Value = dt.Rows(0).Item("codigo_con").ToString
            Me.txtTitulo.Text = dt.Rows(0).Item("titulo_con").ToString
            Me.txtDescripcion.Text = dt.Rows(0).Item("descripcion_con").ToString
            Me.ddlAmbito.SelectedValue = dt.Rows(0).Item("ambito_con").ToString
            Me.txtfecini.Value = dt.Rows(0).Item("fechaini_con").ToString
            Me.txtfecfin.Value = dt.Rows(0).Item("fechafin_con").ToString
            Me.txtfecfineva.Value = dt.Rows(0).Item("fechafinevaluacion_con").ToString
            Me.txtfecres.Value = dt.Rows(0).Item("fecharesultados_con").ToString
            Me.ddlTipo.SelectedValue = dt.Rows(0).Item("tipo_con").ToString
            Me.ddlDirigidoA.SelectedValue = dt.Rows(0).Item("diridigoa_con").ToString
            Me.chkInnovacion.Checked = dt.Rows(0).Item("innovacion_con").ToString
            Me.archivo_base.InnerHtml = "<a onclick=fnDownload('" + dt.Rows(0).Item("bases_con").ToString + "')>Descargar Bases</a>"

        Catch ex As Exception
            Mensaje(ex.Message, "alert alert-danger")
        End Try
    End Sub


    Private Sub Mensaje(ByVal texto As String, ByVal tipo As String)
        Me.lblMensaje.InnerText = ""
        Me.lblMensaje.Attributes.Remove("class")

        Me.lblMensaje.InnerText = texto
        Me.lblMensaje.Attributes.Add("class", tipo)
    End Sub

    Protected Sub btnEliminar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEliminar.Click
        If (Session("id_per") Is Nothing) Then
            Response.Redirect("../../sinacceso.html")
        End If

        Dim obj As New ClsGestionInvestigacion
        Dim dt As New Data.DataTable
        dt = obj.EliminarConcurso(Me.cod_con.Value, Session("id_per"))
        If dt.Rows(0).Item("Respuesta") = 1 Then
            Mensaje(dt.Rows(0).Item("Mensaje").ToString, "alert alert-success")
            ListarConcursos()
        Else
            Mensaje(dt.Rows(0).Item("Mensaje").ToString, "alert alert-danger")
        End If
        'Ocultar Modal de Confirmación
        Me.mdConfirmacion.Attributes.Remove("class")
        Me.mdConfirmacion.Attributes.Add("class", "modal fade")
        Me.mdConfirmacion.Attributes.CssStyle.Remove("display")
        Me.mdConfirmacion.Attributes.CssStyle.Add("display", "none")
        Me.cod_con.Value = 0
    End Sub


    Protected Sub btnCancelarEliminar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelarEliminar.Click

        'Ocultar Modal de Confirmación
        Me.mdConfirmacion.Attributes.Remove("class")
        Me.mdConfirmacion.Attributes.Add("class", "modal fade")
        Me.mdConfirmacion.Attributes.CssStyle.Remove("display")
        Me.mdConfirmacion.Attributes.CssStyle.Add("display", "none")
        Me.cod_con.Value = 0
    End Sub


    Protected Sub btnCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        'Ocultar Modal de Confirmación
        Me.mdRegistro.Attributes.Remove("class")
        Me.mdRegistro.Attributes.Add("class", "modal fade")
        Me.mdRegistro.Attributes.CssStyle.Remove("display")
        Me.mdRegistro.Attributes.CssStyle.Add("display", "none")
        LimpiarControles()
    End Sub

    Protected Sub btnGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardar.Click
        If (Session("id_per") Is Nothing) Then
            Response.Redirect("../../sinacceso.html")
        End If
        Try

            If validar() = True Then
                Dim obj As New ClsGestionInvestigacion

                Dim dt As New Data.DataTable
                Dim innovacion As Integer = 0
                If Me.chkInnovacion.Checked = True Then
                    innovacion = 1
                Else
                    innovacion = 0
                End If

                dt = obj.ActualizarConcurso(Me.cod_con.Value, Me.txtTitulo.Text, Me.txtDescripcion.Text, Me.ddlAmbito.SelectedValue, Me.txtfecini.Value, _
                                          Me.txtfecfin.Value, Me.txtfecfineva.Value, Me.txtfecres.Value, Me.ddlTipo.SelectedValue, Me.ddlDirigidoA.SelectedValue, _
                                           innovacion, Session("id_per"), Request("ctf"))

                Mensaje(dt.Rows(0).Item("Mensaje"), "alert alert-success")

                If dt.Rows(0).Item("Respuesta") = 1 Then
                    Dim archivo As HttpPostedFile
                    If HttpContext.Current.Request.Files("archivo").ContentLength > 0 Then
                        archivo = HttpContext.Current.Request.Files("archivo")
                        'Me.TextBox1.Text = Me.archivo.FileName
                        SubirArchivo(9, dt.Rows(0).Item("cod"), archivo)
                    End If
                End If

                If dt.Rows(0).Item("Respuesta") = 1 Then
                    'Ocultar Modal de Confirmación
                    Me.mdRegistro.Attributes.Remove("class")
                    Me.mdRegistro.Attributes.Add("class", "modal fade")
                    Me.mdRegistro.Attributes.CssStyle.Remove("display")
                    Me.mdRegistro.Attributes.CssStyle.Add("display", "none")
                    ListarConcursos()
                    LimpiarControles()
                Else
                    Me.lblMensajeRegistro.InnerHtml = dt.Rows(0).Item("Mensaje")
                    'Me.lblMensajeRegistro.InnerHtml = "No se pudo guardar el concurso"
                    Me.lblMensajeRegistro.Attributes.Add("class", "alert alert-danger")
                End If

            End If
        Catch ex As Exception
            Me.lblMensajeRegistro.InnerHtml = ex.Message
            'Me.lblMensajeRegistro.InnerHtml = "No se pudo guardar el concurso"
            Me.lblMensajeRegistro.Attributes.Add("class", "alert alert-danger")
        End Try
    End Sub

    Protected Sub btnAgregar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAgregar.Click
        LimpiarControles()
        Me.mdRegistro.Attributes.Remove("class")
        Me.mdRegistro.Attributes.Add("class", "modal fade in")
        Me.mdRegistro.Attributes.CssStyle.Remove("display")
        Me.mdRegistro.Attributes.CssStyle.Add("display", "block")
    End Sub

    Private Sub LimpiarControles()
        Me.cod_con.Value = 0
        Me.txtTitulo.Text = ""
        Me.txtDescripcion.Text = ""
        Me.ddlAmbito.SelectedValue = ""
        Me.txtfecini.Value = ""
        Me.txtfecfin.Value = ""
        Me.txtfecfineva.Value = ""
        Me.txtfecres.Value = ""
        Me.ddlTipo.SelectedValue = ""
        Me.ddlDirigidoA.SelectedValue = ""
        Me.chkInnovacion.Checked = False
        'Me.archivo.FileContent.Dispose()
        Me.archivo_base.InnerHtml = ""
        Me.lblMensajeRegistro.InnerHtml = ""
        Me.lblMensajeRegistro.Attributes.Remove("class")
    End Sub

    Function validar() As Boolean
        Me.lblMensajeRegistro.InnerText = ""
        Me.lblMensajeRegistro.Attributes.Remove("class")

        If Me.txtTitulo.Text = "" Then
            Me.lblMensajeRegistro.InnerText = "Debe ingresar un Título para el concurso"
            Me.lblMensajeRegistro.Attributes.Add("class", "alert alert-danger")
            ScriptManager.GetCurrent(Me.Page).SetFocus(Me.txtTitulo)
            Return False
        End If
        If Me.txtDescripcion.Text = "" Then
            Me.lblMensajeRegistro.InnerText = "Debe ingresar una descripción para el concurso"
            Me.lblMensajeRegistro.Attributes.Add("class", "alert alert-danger")
            ScriptManager.GetCurrent(Me.Page).SetFocus(Me.txtDescripcion)
            Return False
        End If
        If Me.ddlAmbito.SelectedValue = "" Then
            Me.lblMensajeRegistro.InnerText = "Debe seleccionar un ámbito para el concurso"
            Me.lblMensajeRegistro.Attributes.Add("class", "alert alert-danger")
            Return False
        End If
        If Me.txtfecini.Value = "" Then
            Me.lblMensajeRegistro.InnerText = "Debe seleccionar una Fecha de inicio de postulación para el concurso"
            Me.lblMensajeRegistro.Attributes.Add("class", "alert alert-danger")
            ScriptManager.GetCurrent(Me.Page).SetFocus(Me.txtfecini)
            Return False
        End If
        If Me.txtfecfin.Value = "" Then
            Me.lblMensajeRegistro.InnerText = "Debe seleccionar una Fecha de finalización de postulación para el concurso"
            Me.lblMensajeRegistro.Attributes.Add("class", "alert alert-danger")
            ScriptManager.GetCurrent(Me.Page).SetFocus(Me.txtfecfin)
            Return False
        End If
        If CDate(Me.txtfecini.Value) > CDate(Me.txtfecfin.Value) Then
            Me.lblMensajeRegistro.InnerText = "Fecha de Inicio de postulación no puede ser menor a la fecha de fin de postulación para el concurso"
            Me.lblMensajeRegistro.Attributes.Add("class", "alert alert-danger")
            ScriptManager.GetCurrent(Me.Page).SetFocus(Me.txtfecfin)
            Return False
        End If

        If Me.txtfecfineva.Value = "" Then
            Me.lblMensajeRegistro.InnerText = "Debe seleccionar una Fecha de finalización de evaluación de postulaciones"
            Me.lblMensajeRegistro.Attributes.Add("class", "alert alert-danger")
            ScriptManager.GetCurrent(Me.Page).SetFocus(Me.txtfecfineva)
            Return False
        End If
        If CDate(Me.txtfecfin.Value) > CDate(Me.txtfecfineva.Value) Then
            Me.lblMensajeRegistro.InnerText = "Fecha fin de evaluación no puede ser menor a la fecha de fin de postulación para el concurso"
            Me.lblMensajeRegistro.Attributes.Add("class", "alert alert-danger")
            ScriptManager.GetCurrent(Me.Page).SetFocus(Me.txtfecfineva)
            Return False
        End If

        If Me.txtfecres.Value = "" Then
            Me.lblMensajeRegistro.InnerText = "Debe seleccionar una Fecha de publicación de resultados para el concurso"
            Me.lblMensajeRegistro.Attributes.Add("class", "alert alert-danger")
            ScriptManager.GetCurrent(Me.Page).SetFocus(Me.txtfecres)
            Return False
        End If

        If CDate(Me.txtfecfineva.Value) > CDate(Me.txtfecres.Value) Then
            Me.lblMensajeRegistro.InnerText = "Fecha de publicación de resultados no puede ser menor a la fecha de fin de postulación para el concurso"
            Me.lblMensajeRegistro.Attributes.Add("class", "alert alert-danger")
            ScriptManager.GetCurrent(Me.Page).SetFocus(Me.txtfecres)
            Return False
        End If

        If Me.ddlTipo.SelectedValue = "" Then
            Me.lblMensajeRegistro.InnerText = "Debe seleccionar el tipo de concurso"
            Me.lblMensajeRegistro.Attributes.Add("class", "alert alert-danger")
            Return False
        End If
        If Me.ddlDirigidoA.SelectedValue = "" Then
            Me.lblMensajeRegistro.InnerText = "Debe seleccionar hacia quíén va diridigo el concurso"
            Me.lblMensajeRegistro.Attributes.Add("class", "alert alert-danger")
            Return False
        End If
        If Me.cod_con.Value = 0 Then
            If Me.archivo.HasFile = False Then
                Me.lblMensajeRegistro.InnerText = "Debe adjuntar las bases del concurso"
                Me.lblMensajeRegistro.Attributes.Add("class", "alert alert-danger")
                Return False
            End If
        End If

        If Me.archivo.HasFile = True Then
            If Path.GetExtension(Me.archivo.PostedFile.FileName) <> ".pdf" Then
                Me.lblMensajeRegistro.InnerText = "Solo se permiten archivos en formato PDF"
                Me.lblMensajeRegistro.Attributes.Add("class", "alert alert-danger")
                Return False
            End If
        End If

        If Me.archivo.HasFile = True Then
            If Me.archivo.PostedFile.ContentLength > 2097152 Then
                Me.lblMensajeRegistro.InnerText = "Solo se permiten adjuntar archivos de 2MB"
                Me.lblMensajeRegistro.Attributes.Add("class", "alert alert-danger")
                Return False
            End If
        End If
        Return True
    End Function


    Sub SubirArchivo(ByVal idtabla As Integer, ByVal codigo As String, ByVal ArchivoSubir As HttpPostedFile)
        Try
            Dim dt As New Data.DataTable
            Dim obj As New ClsGestionInvestigacion

            Dim post As HttpPostedFile = ArchivoSubir
            Dim codigo_con As String = codigo
            Dim nrooperacion As String = 3
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
                dt = obj.ActualizarIDArchivoCompartido(idtabla, codigo, nrooperacion)
            Else
                Me.lblMensaje.InnerHtml = Me.lblMensaje.InnerHtml + "<div style='color:red'>Archivo no se pudo adjuntar</div>"
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
            Me.lblMensaje.InnerHtml = Me.lblMensaje.InnerHtml + "<div class='row' style='color:red'>" + ex.Message + "</div>"

        End Try
    End Sub

End Class
