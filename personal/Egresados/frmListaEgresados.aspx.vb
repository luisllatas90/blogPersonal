﻿Partial Class Egresado_frmListaEgresados
    Inherits System.Web.UI.Page
    Public nro As Integer = 0

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If (Session("id_per") Is Nothing) Then
                Response.Redirect("../../sinacceso.html")
            End If

            If (IsPostBack = False) Then
                OcultaAlEnviar(False)
                OcultarDatosActualizar(False)
                CargarListas()
            End If

        Catch ex As Exception
            Response.Write(ex.Message.ToString)
        End Try
    End Sub
    Sub OcultarDatosActualizar(ByVal sw As Boolean)
        Me.lblTit.Visible = sw
        Me.tblDatos.Visible = sw
        Me.lblEgresado.Visible = sw
        Me.txtEgresado.Visible = sw
        Me.lblCorreo1.Visible = sw
        Me.lblCorreo2.Visible = sw
        Me.txtCorreo1.Visible = sw
        Me.txtCorreo2.Visible = sw
        Me.txtFijo.Visible = sw
        Me.lblFijo.Visible = sw
        Me.txtCelular1.Visible = sw
        Me.lblCelular1.Visible = sw
        Me.txtCelular2.Visible = sw
        Me.lblCelular2.Visible = sw
        Me.btnActualizarDatos.Visible = sw
        Me.btnRegresar2.Visible = sw
        Me.btnMensaje.Visible = Not sw

        Me.gvwEgresados.Visible = Not sw
        Me.btnNuevoEgresado.Visible = Not sw
        Me.btnExportar.Visible = Not sw
    End Sub
    Sub CargarListas()
        Dim obj As New ClsConectarDatos
        Dim dt As Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()

        '#Nivel
        Me.ddlNivel.Items.Add("TODOS")
        Me.ddlNivel.Items.Add("PRE GRADO")
        Me.ddlNivel.Items.Add("POST GRADO")
        Me.ddlNivel.Items.Add("POST TITULO")

        '#Modalidad
        dt = New Data.DataTable
        dt = obj.TraerDataTable("ALUMNI_ListarModalidad")
        Me.ddlModalidad.DataSource = dt
        Me.ddlModalidad.DataTextField = "nombre"
        Me.ddlModalidad.DataValueField = "codigo"
        Me.ddlModalidad.DataBind()
        dt.Dispose()


        '#Facultad
        dt = New Data.DataTable
        'dt = obj.TraerDataTable("ALUMNI_ListarFacultad")
        If Request.QueryString("ctf") = 145 Then
            dt = obj.TraerDataTable("ALUMNI_ListarFacultadCoordinador", Request.QueryString("ID"))
        Else
            dt = obj.TraerDataTable("ALUMNI_ListarFacultad")
        End If
        Me.ddlFacultad.DataSource = dt
        Me.ddlFacultad.DataTextField = "nombre"
        Me.ddlFacultad.DataValueField = "codigo"
        Me.ddlFacultad.DataBind()
        Me.ddlFacultad.SelectedValue = 0
        dt.Dispose()

        '#Escuela
        dt = New Data.DataTable
        If Request.QueryString("ctf") = 145 Then
            If Me.ddlFacultad.SelectedItem.Text = "TODOS" Then
                dt = obj.TraerDataTable("ALUMNI_ListarCarreraProfesionalPersonal", 2, Request.QueryString("ID"), "%")
            Else
                dt = obj.TraerDataTable("ALUMNI_ListarCarreraProfesionalPersonal", 2, Request.QueryString("ID"), ddlFacultad.SelectedValue)
            End If
        Else
            '' Modificado
            dt = obj.TraerDataTable("ALUMNI_ListarCarreraProfesional", "2", "2")
        End If

            Me.ddlEscuela.DataSource = dt
            Me.ddlEscuela.DataTextField = "nombre"
            Me.ddlEscuela.DataValueField = "codigo"
            Me.ddlEscuela.DataBind()
            dt.Dispose()
            obj.CerrarConexion()
            obj = Nothing


            '#Años
            Me.ddlEgreso.Items.Add("TODOS")
            Me.ddlBachiller.Items.Add("TODOS")
            Me.ddlTitulo.Items.Add("TODOS")
            For i As Integer = 2004 To Date.Today.Year
                Me.ddlEgreso.Items.Add(i)
                Me.ddlBachiller.Items.Add(i)
                Me.ddlTitulo.Items.Add(i)
            Next
            Me.ddlEgreso.SelectedIndex = 0
            Me.ddlBachiller.SelectedIndex = 0
            Me.ddlTitulo.SelectedIndex = 0

    End Sub
    Function CargarGrid() As Data.DataTable
        Dim obj As New ClsConectarDatos
        Dim dtTabla As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()

        'Codigo_per
        'Tipo de Función si es Adminidtrador o Coordinador, todos los permisos, sino 

        Dim ls_codigo_per As String = "%"
        If Request.QueryString("ctf") = 145 Then
             ls_codigo_per = Request.QueryString("ID")
        End If

        'Dim ls_cadena As String = "(" & IIf(Me.ddlNivel.SelectedIndex = 0, "%", Me.ddlNivel.SelectedItem.Text) & ", " & IIf(Me.ddlModalidad.SelectedIndex = 0, "%", Me.ddlModalidad.SelectedItem.Text) & ", " & IIf(Me.ddlFacultad.SelectedItem.Text = "TODOS", "%", Me.ddlFacultad.SelectedItem.Text) & ", " & IIf(Me.ddlEscuela.SelectedIndex = 0, "%", Me.ddlEscuela.SelectedItem.Text) & ", " & IIf(Me.ddlEgreso.SelectedIndex = 0, "", Me.ddlEgreso.SelectedItem.Text) & ", " & IIf(Me.ddlBachiller.SelectedIndex = 0, "%", Me.ddlBachiller.Text) & ", " & IIf(Me.ddlTitulo.SelectedIndex = 0, "%", Me.ddlTitulo.Text) & ", " & IIf(Me.txtApellidoNombre.Text.Trim = "", "%", Me.txtApellidoNombre.Text.Trim) & ", " & IIf(Me.ddlSexo.SelectedIndex = 0, "%", Me.ddlSexo.SelectedItem.Text) & ", " & ls_codigo_per & ")"
        'Response.Write(ls_cadena)

        dtTabla = obj.TraerDataTable("ALUMNI_ListarAlumni", _
                                     IIf(Me.ddlNivel.SelectedIndex = 0, "%", Me.ddlNivel.SelectedItem.Text), _
                                     IIf(Me.ddlModalidad.SelectedIndex = 0, "%", Me.ddlModalidad.SelectedItem.Text), _
                                     IIf(Me.ddlFacultad.SelectedItem.Text = "TODOS", "%", Me.ddlFacultad.SelectedItem.Text), _
                                     IIf(Me.ddlEscuela.SelectedIndex = 0, "%", Me.ddlEscuela.SelectedItem.Text), _
                                     IIf(Me.ddlEgreso.SelectedIndex = 0, "", Me.ddlEgreso.SelectedItem.Text), _
                                     IIf(Me.ddlBachiller.SelectedIndex = 0, "%", Me.ddlBachiller.Text), _
                                     IIf(Me.ddlTitulo.SelectedIndex = 0, "%", Me.ddlTitulo.Text), _
                                     IIf(Me.txtApellidoNombre.Text.Trim = "", "%", Me.txtApellidoNombre.Text.Trim), _
                                     IIf(Me.ddlSexo.SelectedIndex = 0, "%", Me.ddlSexo.SelectedItem.Text), _
                                     ls_codigo_per)
        obj.CerrarConexion()
        obj = Nothing
        Me.lblMensajeFormulario.Text = "Se encontraron " & dtTabla.Rows.Count & " registro(s)."
        Return dtTabla
    End Function
    Protected Sub gvwEgresados_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gvwEgresados.PageIndexChanging
        gvwEgresados.PageIndex = e.NewPageIndex()
        CargarGrid()
    End Sub
    Protected Sub btnExportar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExportar.Click
        'Dim dt As New Data.DataTable
        'dt = CargarGrid()
        'If (dt.Rows.Count > 0) Then
        '    dt.Columns.Remove("codigo_Pso")
        '    dt.Columns.Remove("cv_Ega")
        '    dt.Columns.Remove("foto_Ega")
        '    Me.gvExporta.DataSource = dt
        '    Me.gvExporta.DataBind()
        '    Axls()
        'Else
        '    Response.Write("<script> alert('La tabla no tiene datos')</script>")
        'End If

        'Dim obj As New ClsConectarDatos
        'Dim dtt As New Data.DataTable
        'obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        'obj.AbrirConexion()

        'Dim ls_codigo_per As String = "%"
        'If Request.QueryString("ctf") = 145 Then
        '    ls_codigo_per = Request.QueryString("ID")
        'End If

        'dtt = obj.TraerDataTable("ALUMNI_ListarAlumniExportacion", _
        '                             IIf(Me.ddlNivel.SelectedIndex = 0, "%", Me.ddlNivel.SelectedItem.Text), _
        '                             IIf(Me.ddlModalidad.SelectedIndex = 0, "%", Me.ddlModalidad.SelectedItem.Text), _
        '                             IIf(Me.ddlFacultad.SelectedItem.Text = "TODOS", "%", Me.ddlFacultad.SelectedItem.Text), _
        '                             IIf(Me.ddlEscuela.SelectedIndex = 0, "%", Me.ddlEscuela.SelectedItem.Text), _
        '                             IIf(Me.ddlEgreso.SelectedIndex = 0, "", Me.ddlEgreso.SelectedItem.Text), _
        '                             IIf(Me.ddlBachiller.SelectedIndex = 0, "%", Me.ddlBachiller.Text), _
        '                             IIf(Me.ddlTitulo.SelectedIndex = 0, "%", Me.ddlTitulo.Text), _
        '                             IIf(Me.txtApellidoNombre.Text.Trim = "", "%", Me.txtApellidoNombre.Text.Trim), _
        '                             IIf(Me.ddlSexo.SelectedIndex = 0, "%", Me.ddlSexo.SelectedItem.Text), _
        '                             ls_codigo_per)

        ' ''Response.Write(dtt.Rows.Count)


        'If (dtt.Rows.Count > 0) Then

        '    ''gvExporta.Visible = False
        gvwEgresados0.Visible = True

        '    Me.dgv_exportar.DataSource = dtt
        '    Me.dgv_exportar.DataBind()
        Axls()
        'Else
        'Response.Write("<script> alert('La tabla no tiene datos')</script>")
        'End If

        'obj.CerrarConexion()
        'obj = Nothing
        gvwEgresados0.Visible = False
    End Sub
    Private Sub Axls()
        Dim sb As StringBuilder = New StringBuilder()
        Dim SW As System.IO.StringWriter = New System.IO.StringWriter(sb)
        Dim htw As HtmlTextWriter = New HtmlTextWriter(SW)
        Dim Page As Page = New Page()
        Dim form As HtmlForm = New HtmlForm()
        'Me.gvExporta.EnableViewState = False
        Me.gvwEgresados0.EnableViewState = False
        Page.EnableEventValidation = False
        Page.DesignerInitialize()
        Page.Controls.Add(form)
        'form.Controls.Add(Me.gvExporta)
        form.Controls.Add(Me.gvwEgresados0)
        Page.RenderControl(htw)
        Response.Clear()
        Response.Buffer = True
        Response.ContentType = "application/vnd.ms-excel"
        Response.AddHeader("Content-Disposition", "attachment;filename=Reporte_Egresados" & ".xls")
        Response.Charset = "UTF-8"
        Response.ContentEncoding = Encoding.Default
        Response.Write(sb.ToString())
        Response.End()
     
    End Sub
    Private Function GeneraClave() As String
        Dim rnd As New Random
        Dim ubicacion As Integer
        Dim strNumeros As String = "0123456789"
        Dim strLetraMin As String = "abcdefghijklmnopqrstuvwxyz"
        Dim strLetraMay As String = "ABCDEFGHIJKLMNOPQRSTUVWXYZ"
        Dim Token As String = ""
        Dim strCadena As String = ""
        strCadena = strLetraMin & strNumeros & strLetraMay
        While Token.Length < 6
            ubicacion = rnd.Next(0, strCadena.Length)
            If (ubicacion = 62) Then
                Token = Token & strCadena.Substring(ubicacion - 1, 1)
            End If
            If (ubicacion < 62) Then
                Token = Token & strCadena.Substring(ubicacion, 1)
            End If
        End While

        Return Token
    End Function
    Protected Sub btnNuevoEgresado_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNuevoEgresado.Click
        Response.Redirect("frmpersonaAdmin.aspx?sou=A")
    End Sub
    Protected Sub btnMensaje_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnMensaje.Click
        If (validaCheckActivo() = True) Then
            Me.lblMensajeFormulario.Text = ""
            OcultaAlEnviar(True)
        Else
            Me.lblMensajeFormulario.Text = "Debe seleccionar al menos un egresado"
        End If
    End Sub
    Private Function validaCheckActivo() As Boolean
        Dim Fila As GridViewRow
        Dim sw As Byte = 0
        Dim d As Integer = 0
        Dim destinatarios As String = ""
        'Me.lblDestinatario.Text = ""
        For i As Integer = 0 To Me.gvwEgresados.Rows.Count - 1
            'Capturamos las filas que estan activas
            Fila = Me.gvwEgresados.Rows(i)
            Dim valor As Boolean = CType(Fila.FindControl("chkElegir"), CheckBox).Checked
            If (valor = True) Then
                d = d + 1
                sw = 1
                destinatarios &= Me.gvwEgresados.Rows(i).Cells(14).Text & ", " & Me.gvwEgresados.Rows(i).Cells(13).Text & " | "
            End If
        Next
        lblDestinatario1.InnerHtml = d.ToString & " Destinatario(s)."

        lblDestinatario1.Attributes.Add("title", Server.HtmlDecode(destinatarios))
        If (sw = 1) Then
            Return True
        End If

        Return False
    End Function

    Private Sub OcultaAlEnviar(ByVal sw As Boolean)
        Me.tbMensaje.Visible = sw
        Me.lblAsunto.Visible = sw
        Me.lblMensaje.Visible = sw
        Me.txtAsunto.Visible = sw
        'Me.txtMensaje.Visible = sw
        Me.btnGuardar.Visible = sw
        Me.btnSalir.Visible = sw
        Me.lblEnvioMultiple.Visible = sw
        'Me.lblDestinatario.Visible = sw
        Me.lblEnviado.Visible = sw
        Me.btnMensaje.Visible = Not sw

        Me.gvwEgresados.Visible = Not sw
        Me.btnNuevoEgresado.Visible = Not sw
        Me.btnExportar.Visible = Not sw
    End Sub

    Protected Sub btnSalir_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSalir.Click
        Me.txtAsunto.Text = ""
        Me.txtMensaje.Value = ""
        OcultaAlEnviar(False)
    End Sub
    Protected Sub btnGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardar.Click
        Dim correosEnviados As Integer = 0
        Dim correosNoEnviados As Integer = 0
        Dim sincorreo As Integer = 0
        Dim StrCuentas As String = ""
        Dim para As String = ""
        Dim Mensaje As String = ""
        Dim nombreper As String = ""

        Try
            If (ValidaMensaje() = True) Then
                Dim Fila As GridViewRow
                Dim copia As String = ""
                Dim obj As New ClsConectarDatos
                obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString

                Dim tbDatosFirma As Data.DataTable
                'Dim nombreper As String = ""
                Dim replyto As String = "alumni@usat.edu.pe"
                obj.AbrirConexion()
                tbDatosFirma = obj.TraerDataTable("ALUMNI_FIRMA", Request.QueryString("id"))
                If tbDatosFirma.Rows.Count Then
                    nombreper = tbDatosFirma.Rows(0).Item("nombreper").ToString
                End If
                If Request.QueryString("ctf") = 145 Then 'COORDINADOR DE ALUMNI
                    replyto = tbDatosFirma.Rows(0).Item("usuario_per").ToString + "@usat.edu.pe"
                End If

                obj.CerrarConexion()

                '#Adjunto
                Dim strRuta As String
                Dim nombrearchivo As String
                strRuta = ""
                If (Me.FileUpload1.HasFile = True) Then
                    strRuta = Server.MapPath("..\egresados\adjuntos")
                    nombrearchivo = Date.Today.Year.ToString & Date.Today.Month.ToString & Date.Today.Day.ToString & Date.Today.Hour.ToString & Date.Today.Minute.ToString & DateTime.Now.Millisecond.ToString()
                    Me.FileUpload1.PostedFile.SaveAs(strRuta & "\" & nombrearchivo & System.IO.Path.GetExtension(Me.FileUpload1.FileName).ToString)
                    strRuta = strRuta & "\" & nombrearchivo & System.IO.Path.GetExtension(Me.FileUpload1.FileName).ToString
                End If

                For i As Integer = 0 To Me.gvwEgresados.Rows.Count - 1
                    Fila = Me.gvwEgresados.Rows(i)
                    Dim valor As Boolean = CType(Fila.FindControl("chkElegir"), CheckBox).Checked
                    If (valor = True) Then
                        nro += 1
                        'If Me.gvwEgresados.DataKeys(i).Values("CORREO").ToString = "SI" Then
                        Mensaje = "<font face='Trebuchet MS'>"
                        Mensaje &= IIf(Me.gvwEgresados.DataKeys(i).Values("SEXO").ToString = "F", "Estimada ", "Estimado ")
                        Mensaje &= Me.gvwEgresados.DataKeys(i).Values("NOMBRES").ToString & ":<br /><br />"
                        Mensaje &= Me.txtMensaje.Value
                        Mensaje &= firmaMensaje(nombreper)
                        Mensaje &= "</font>"


                        If validarEmail(Me.gvwEgresados.DataKeys(i).Values("EMAIL_PER").ToString) = True Then


                            para = Me.gvwEgresados.DataKeys(i).Values("EMAIL_PER").ToString.Trim

                            If para = "" Then
                                para = Me.gvwEgresados.DataKeys(i).Values("EMAIL").ToString.Trim
                            End If
                            'copia = Me.gvwEgresados.DataKeys(i).Values("EMAIL").ToString.Trim
                            'para = "yperez@usat.edu.pe"
                            'para = "moises.vilchez@usat.edu.pe"

                            'Inicio HCano 23/07/2019

                            If ConfigurationManager.AppSettings("CorreoUsatActivo") = 0 Then
                                para = "hcano@usat.edu.pe"
                            End If


                            'Fin HCano 23/07/2019

                            'If para <> "" Then

                            'Inicio HCano 30/07/2019
                            'If EnviarMensaje(para, Me.txtAsunto.Text.Trim, Mensaje, copia, strRuta, Me.FileUpload1.FileName, replyto) Then
                            '    obj.AbrirConexion()
                            '    obj.Ejecutar("Insertar_Alumni_BitacoraEnviaMail", Date.Now, CInt(Me.gvwEgresados.DataKeys(i).Values("codigo_pso").ToString), para, Me.txtAsunto.Text.Trim, Me.txtMensaje.Value, Me.FileUpload1.FileName.ToString)
                            '    correosEnviados += 1
                            '    obj.CerrarConexion()
                            'Else
                            '    correosNoEnviados += 1
                            '    StrCuentas &= "<tr><td>" & Me.gvwEgresados.DataKeys(i).Values("NOMBRES").ToString.Trim & " " & Me.gvwEgresados.DataKeys(i).Values("APELLIDOS").ToString.Trim & "</td><td>" & Me.gvwEgresados.DataKeys(i).Values("EMAIL_PER").ToString.Trim & "</td><td>" & Me.gvwEgresados.DataKeys(i).Values("EMAIL").ToString.Trim & "</td></tr>"
                            'End If

                            Dim dt As New Data.DataTable
                            Dim tieneadjunto As Boolean = False

                            obj.AbrirConexion()
                            If Me.FileUpload1.FileName.ToString <> "" Then
                                tieneadjunto = True
                            End If
                            dt = obj.TraerDataTable("InsertaEnvioCorreosMasivo", Me.gvwEgresados.DataKeys(i).Values("codigo_pso"), Me.txtAsunto.Text.Trim, para, Mensaje, 47, replyto, strRuta)

                            obj.CerrarConexion()

                            If dt.Rows(0).Item("Respuesta") = "1" Then
                                obj.AbrirConexion()
                                obj.Ejecutar("Insertar_Alumni_BitacoraEnviaMail", Date.Now, CInt(Me.gvwEgresados.DataKeys(i).Values("codigo_pso").ToString), para, Me.txtAsunto.Text.Trim, Me.txtMensaje.Value, Me.FileUpload1.FileName.ToString)
                                correosEnviados += 1
                                obj.CerrarConexion()
                            Else
                                correosNoEnviados += 1
                                StrCuentas &= "<tr><td>" & Me.gvwEgresados.DataKeys(i).Values("NOMBRES").ToString.Trim & " " & Me.gvwEgresados.DataKeys(i).Values("APELLIDOS").ToString.Trim & "</td><td>" & Me.gvwEgresados.DataKeys(i).Values("EMAIL_PER").ToString.Trim & "</td><td>" & Me.gvwEgresados.DataKeys(i).Values("EMAIL").ToString.Trim & "</td></tr>"
                            End If

                            'Fin HCano 30/07/2019
                            'Else
                            'sincorreo += 1
                            'End If
                            'End If
                        Else
                            correosNoEnviados += 1
                            StrCuentas &= "<tr><td>" & Me.gvwEgresados.DataKeys(i).Values("NOMBRES").ToString.Trim & " " & Me.gvwEgresados.DataKeys(i).Values("APELLIDOS").ToString.Trim & "</td><td>" & Me.gvwEgresados.DataKeys(i).Values("EMAIL_PER").ToString.Trim & "</td><td>" & Me.gvwEgresados.DataKeys(i).Values("EMAIL").ToString.Trim & "</td></tr>"
                        End If
                    End If
                Next
                Me.lblMensajeFormulario.Text = "Correos Enviados: " & correosEnviados.ToString & "  de  " & nro & " destinatario(s) seleccionado(s).Usuarios sin correo registrado: " & sincorreo & ". Usuarios con correos inválidos: " & correosNoEnviados
                obj = Nothing
            End If

            EnviarMensajeNotificacion(StrCuentas, txtMensaje.Value, txtAsunto.Text, Me.FileUpload1.FileName, nro, correosEnviados, correosNoEnviados, sincorreo, nombreper)
            Me.txtAsunto.Text = ""
            Me.txtMensaje.Value = ""

            OcultaAlEnviar(False)
        Catch ex As Exception
            Me.lblMensajeFormulario.Text = ex.Message & " " & ex.StackTrace.ToString
            'Me.lblMensajeFormulario.Text = "Correos Enviados: " & correosEnviados.ToString & "  de  " & nro & " destinatario(s) seleccionado(s). Usuarios sin correo registrado: " & sincorreo
        End Try
    End Sub


    Function validarEmail(ByVal email As String) As Boolean
        Try
            Dim estructura As String = "^[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?$"
            Dim match As Match = Regex.Match(email.Trim(), estructura, RegexOptions.IgnoreCase)

            If match.Success Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Return False
        End Try

    End Function

    Function EnviarMensaje(ByVal para As String, ByVal asunto As String, ByVal mensaje As String, ByVal copia As String, ByVal rutaarchivo As String, ByVal nombrearchivo As String, ByVal replyto As String) As Boolean
        Try
            Dim cls As New ClsEnvioMailAlumni
            'If cls.EnviarMailAd("alumni@usat.edu.pe", "AlumniUSAT", para, asunto, mensaje, True, copia, replyto, rutaarchivo, nombrearchivo) Then ' HCano 23/07/2019
            If cls.EnviarMailAd("campusvirtual@usat.edu.pe", "Campus Virtual", para, asunto, mensaje, True, copia, replyto, rutaarchivo, nombrearchivo) Then ' HCano 23/07/2019
                cls = Nothing
                Return True
            Else
                cls = Nothing
                Return False
            End If
        Catch ex As Exception
            Return False
        End Try
    End Function
    Sub EnviarMensajeNotificacion(ByVal cuentas As String, ByVal mensaje As String, ByVal asunto As String, ByVal adjunto As String, ByVal dt As Integer, ByVal de As Integer, ByVal dne As Integer, ByVal dsc As Integer, ByVal nombre_per As String)
        Try

        Dim obj As New ClsConectarDatos
            Dim dtEscuela As Data.DataTable
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            dtEscuela = obj.TraerDataTable("ALUMNI_ListarEscuelaCoordinador", Request.QueryString("ID"))
            obj.CerrarConexion()

            Dim ls_escuelaAll As String = ""
            For i As Integer = 0 To dtEscuela.Rows.Count - 1
                If ls_escuelaAll = "" Then
                    ls_escuelaAll = dtEscuela.Rows(i).Item("escuela")
                Else
                    ls_escuelaAll = ls_escuelaAll + ", " + dtEscuela.Rows(i).Item("escuela")
                End If
            Next

            Dim xmensaje As String = ""
            xmensaje &= "<font face='Trebuchet MS'>"
            xmensaje &= "<b>Notificación de Envío de Correo</b><hr /><br />"
            xmensaje &= "<b>Fecha: </b>" & Now.Date & "<br />"
            xmensaje &= "<b>Asunto: </b>" & asunto & "<br />"
            xmensaje &= "<b>Mensaje: </b>" & mensaje & "<br />"
            xmensaje &= "<b>Adjunto: </b>" & adjunto & "<br /><br />"
            xmensaje &= "<b>Total Destinatarios: </b>" & dt.ToString & "<br />"
            xmensaje &= "<b>Destinatarios sin correo registrado: </b>" & dsc.ToString & "<br />"
            xmensaje &= "<b>Mensajes Enviados: </b>" & de.ToString & "<br />"
            xmensaje &= "<b>Mensajes Fallidos: </b>" & dne.ToString & "<br />"
            xmensaje &= "<b>Mail enviado por: </b>" & nombre_per & "<br />"
            xmensaje &= "<b>Escuela: </b>" & ls_escuelaAll & "<br />"

            If cuentas <> "" Then
                xmensaje &= "<br /><b>Detalle de correos fallidos: </b>"
                xmensaje &= "<br /><br /><table border=""1"" style=""border:1px solid black;""><tr><th>Nombres Apellidos</th><th>Correo Personal</th><th>Correo Profesional</th></tr>"
                xmensaje &= cuentas
                xmensaje &= "</table>"
            End If
            xmensaje &= "<br /><hr /><br />"
            xmensaje &= "<b>CampusVirtual USAT</b>"
            xmensaje &= "</font>"
            Dim cls As New ClsEnvioMailAlumni

            'Inicio HCano 23/07/2019
            Dim para As String = "esther.vasquez@usat.edu.pe"
            If ConfigurationManager.AppSettings("CorreoUsatActivo") = 0 Then
                para = "hcano@usat.edu.pe"
            End If

            'Inicio HCano 30/07/2019
            'cls.EnviarMailAd("alumni@usat.edu.pe", "Alumni USAT", "ghernandez@usat.edu.pe", "Módulo de Alumni USAT - Notificación de Envío de Correo", xmensaje, True, "") 
            'cls.EnviarMailAd("campusvirtual@usat.edu.pe", "Campus Virtual", para, "Módulo de Alumni USAT - Notificación de Envío de Correo", xmensaje, True, "")
            'Fin HCano 23/07/2019

            'lbl_msgbox.Text = xmensaje

            Dim dtRespuesta As New Data.DataTable
            Dim tieneadjunto As Boolean = False
            obj.AbrirConexion()
            dtRespuesta = obj.TraerDataTable("InsertaEnvioCorreosMasivo", 26982, "Módulo de Alumni USAT - Notificación de Envío de Correo", para, xmensaje, 47, "", "")
            obj.CerrarConexion()
            'Fin HCano 30/07/2019

            xmensaje = ""
            cls = Nothing

        Catch ex As Exception
            Response.Write(ex.Message.ToString)
        End Try

    End Sub
    Function firmaMensaje(ByVal nombre As String) As String

        Dim obj As New ClsConectarDatos
        Dim dtEscuela As Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        dtEscuela = obj.TraerDataTable("ALUMNI_ListarEscuelaCoordinador", Request.QueryString("ID"))
        obj.CerrarConexion()

        Dim ls_escuelaAll As String = ""
        For i As Integer = 0 To dtEscuela.Rows.Count - 1
            If ls_escuelaAll = "" Then
                ls_escuelaAll = dtEscuela.Rows(i).Item("escuela").ToString
            Else
                ls_escuelaAll = ls_escuelaAll + ", " + dtEscuela.Rows(i).Item("escuela").ToString
            End If
        Next

        Dim dtCelular As Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        dtCelular = obj.TraerDataTable("ALUMNI_ListarPersonalCelular", Request.QueryString("ID"))
        obj.CerrarConexion()
        Dim ls_Celular As String = ""

        If dtCelular.Rows.Count = 0 Then
            ls_Celular = ""
        Else
            ls_Celular = dtCelular.Rows(0).Item("celular_Per").ToString
        End If


        Dim ls_direccion As String = "Av. San Josemaría Escrivá N°855. Chiclayo - Perú"
        Dim ls_telefono As String = "T: (074) 606200. Anexo: 1239 - C: " & ls_Celular
        Dim ls_pagWeb As String = "www.usat.edu.pe / http://www.facebook.com/usat.peru"


        Dim firma As String
        firma = "<br /><br />---------------------------------------<br />"
        firma &= nombre & "<br />"
        'firma &= "Dirección de Alumni - USAT " & "<br />"

        Select Case CInt(Request.QueryString("ctf"))
            Case 1
                firma &= "Administrador del Sistema  - " & ls_escuelaAll & "<br />"
            Case 90
                firma &= "Dirección de Alumni - " & ls_escuelaAll & "<br />"
            Case 145
                firma &= "Coordinación de Alumni - " & ls_escuelaAll & "<br />"
        End Select

        firma &= ls_direccion & "<br />"
        firma &= ls_telefono & "<br />"
        firma &= ls_pagWeb & "<br />"

        firma &= "<div>Síguenos en :</div>"
        'lbl_msgbox.Text = firma
        firma &= LogoFb()
        Return firma
    End Function

    Function LogoFb() As String
        Dim Logo As String
        Logo = "<a href='https://www.facebook.com/usatalumni' style='" & style() & " ' ><b>f</b></a>"
        'Logo = "<a href='https://www.facebook.com/usatalumni'><img src='https://intranet.usat.edu.pe/autoevaluacion/dyandroid/fb.png'></a>"
        Return Logo
    End Function

    Function style() As String
        Dim stylestr As String
        stylestr = " background-color: #5B74A8;  border-color: #29447E #29447E #1A356E;    border-image: none;    border-style: solid;    border-width: 1px;    box-shadow: 0 1px 0 rgba(0, 0, 0, 0.1), 0 1px 0 #8A9CC2 inset;    color: #FFFFFF;    cursor: pointer;    display: inline-block;    font: bold 20px verdana,arial,sans-serif;    margin: 0;    overflow: visible;    padding: 0.1em 0.5em 0.1em;    position: relative;    text-align: center;    text-decoration: none;white-space: nowrap;z-index: 1;"
        'stylestr = "width: 32px; height: 32px;	background-repeat: no-repeat;	background-position: center center;	text-indent: -900em;	text-decoration: none;	line-height: 100%;	white-space: nowrap;	display: inline-block;	position: relative;	vertical-align: middle;	margin: 0 2px 5px 0;	/* default button color */	background-color: #ececec;	border: solid 1px #b8b8b9;	/* default box shadow */	-webkit-box-shadow: inset 0 1px 0 rgba(255,255,255,.3), 0 1px 0 rgba(0,0,0,.1);	-moz-box-shadow: inset 0 1px 0 rgba(255,255,255,.3), 0 1px 0 rgba(0,0,0,.1);	box-shadow: inset 0 1px 0 rgba(255,255,255,.3), 0 1px 0 rgba(0,0,0,.1);	/* default border radius */	-webkit-border-radius: 5px;	-moz-border-radius: 5px;	border-radius: 5px;background-color: #4d7de1;	border-color: #294c89;	color: #fff; background-image: url('https://intranet.usat.edu.pe/autoevaluacion/dyandroid/white_facebook.png');"
        Return stylestr
    End Function


    Private Function ValidaMensaje() As Boolean
        If (Me.txtAsunto.Text = "") Then
            Me.lblMensajeFormulario.Text = "Debe ingresar el asunto del mensaje"
            Return False
        End If

        If (Me.txtMensaje.Value = "") Then
            Me.lblMensajeFormulario.Text = "Debe ingresar el mensaje a enviar"
            Return False
        End If

        Return True
    End Function

    Protected Sub btnBusca_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBusca.Click
        Try

        Me.lblMensajeFormulario.Text = ""

            Dim dt As New Data.DataTable
            dt = CargarGrid()
            If dt.Rows.Count = 0 Then
                Me.lblMensajeFormulario.Text = "No se encontraron registros"
                Me.gvwEgresados.DataSource = Nothing
            Else
                Me.gvwEgresados.DataSource = dt
                Me.gvwEgresados.Columns.Item(1).Visible = False 'codigo_pso
                Me.gvwEgresados.Columns.Item(2).Visible = False 'cv_ega

                gvwEgresados0.DataSource = dt

            End If
            Me.gvwEgresados.DataBind()
            Me.gvwEgresados0.DataBind()


            dt.Dispose()
        Catch ex As Exception
            Response.Write(ex.Message.ToString)
        End Try

    End Sub

    Function encode(ByVal str As String) As String
        Return (Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(str)))
    End Function


    Protected Sub gvwEgresados_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvwEgresados.RowDataBound
        Dim rutaCV As String = ""

        e.Row.Cells(22).Visible = True
        'rutaCV = "../../librerianet/egresado/alumniusat.aspx?xcod=" & encode(e.Row.Cells(22).Text)
        rutaCV = "//intranet.usat.edu.pe/campusestudiante/alumniegresadousat.aspx?xcod=" & encode(e.Row.Cells(22).Text)

        e.Row.Cells(22).Visible = False

        If (e.Row.RowIndex <> -1) Then
            If (e.Row.Cells(3).Text.Length > 6) Then
                'e.Row.Cells(3).Text = "<center><a style=""text-decoration:none;"" target='_blank' href='" & rutaCV & "'><img style=""border:0px;"" src='../../librerianet/Egresado/fotos/" & e.Row.Cells(3).Text & "' Height='28px' Width='25px' /></a></center>"
                e.Row.Cells(3).Text = "<center><a style=""text-decoration:none;"" target='_blank' href='" & rutaCV & "'><img style=""border:0px;"" src='//intranet.usat.edu.pe/campusestudiante/alumni/fotos/" & e.Row.Cells(3).Text & "' Height='28px' Width='25px' /></a></center>"

            Else
                If e.Row.Cells(8).Text = "F" Then
                    'e.Row.Cells(3).Text = "<center><a style=""text-decoration:none;"" target='_blank' href='" & rutaCV & "'><img style=""border:0px;""  src='../../librerianet/Egresado/archivos/female.png' Height='28px' Width='25px' /></a></center>"
                    e.Row.Cells(3).Text = "<center><a style=""text-decoration:none;"" target='_blank' href='" & rutaCV & "'><img style=""border:0px;"" src='//intranet.usat.edu.pe/campusestudiante/alumni/fotos/female.png' Height='28px' Width='25px' /></a></center>"
                Else
                    'e.Row.Cells(3).Text = "<center><a style=""text-decoration:none;"" target='_blank' href='" & rutaCV & "'><img style=""border:0px;""  src='../../librerianet/Egresado/archivos/male.png' Height='28px' Width='25px' /></a></center>"
                    e.Row.Cells(3).Text = "<center><a style=""text-decoration:none;"" target='_blank' href='" & rutaCV & "'><img style=""border:0px;"" src='//intranet.usat.edu.pe/campusestudiante/alumni/fotos/male.png' Height='28px' Width='25px' /></a></center>"
                End If
            End If
            'Dim Enc As New EncriptaCodigos.clsEncripta
            If (e.Row.RowIndex <> -1) Then
                'e.Row.Cells(15).Text = "<center><a href='frmFichaEgresado.aspx?pso=" & Enc.Codifica("069" & e.Row.Cells(2).Text) & "' target='_blank'><img src='../../images/previo.gif' style='border: 0px'/></a></center>"
                'e.Row.Cells(16).Text = "<center><a href='" & rutaCV & "' target='_blank'><img src='../../images/previo.gif' style='border: 0px'/></a></center>"
            End If

            e.Row.Cells(7).Text = e.Row.Cells(7).Text.ToUpper
            e.Row.Cells(5).Text = e.Row.Cells(5).Text.Substring(0, 3)

        End If
    End Sub

    Protected Sub btnActualizar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnActualizar.Click
        Try


            Dim Fila As GridViewRow
            Dim dt As Data.DataTable
            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            If (validaCheckActivo() = True) Then
                Me.lblMensajeFormulario.Text = ""
                For i As Integer = 0 To Me.gvwEgresados.Rows.Count - 1
                    Fila = Me.gvwEgresados.Rows(i)
                    Dim valor As Boolean = CType(Fila.FindControl("chkElegir"), CheckBox).Checked

                    If (valor = True) Then
                        Me.txtEgresado.Text = Me.gvwEgresados.DataKeys(i).Values("NOMBRES")
                        Me.txtEgresado.Text &= ", " & Me.gvwEgresados.DataKeys(i).Values("APELLIDOS")
                        Me.psotoupdate.Value = Me.gvwEgresados.DataKeys(i).Values("codigo_pso")
                        dt = New Data.DataTable
                        dt = obj.TraerDataTable("Alumni_MostrarCorreoTelefono", CInt(psotoupdate.Value))
                        If dt.Rows.Count Then
                            Me.txtCorreo1.Text = dt.Rows(0).Item("correo1").ToString
                            Me.txtCorreo2.Text = dt.Rows(0).Item("correo2").ToString
                            Me.txtFijo.Text = dt.Rows(0).Item("fijo").ToString
                            Me.txtCelular1.Text = dt.Rows(0).Item("celular1").ToString
                            Me.txtCelular2.Text = dt.Rows(0).Item("celular2").ToString

                            Me.txtCentroLaboral.Text = dt.Rows(0).Item("CentroLaboral").ToString
                            Me.txtCargo.Text = dt.Rows(0).Item("Cargo").ToString
                        End If
                        Exit For
                    End If
                Next
                OcultarDatosActualizar(True)
            Else
                Me.lblMensajeFormulario.Text = "Debe seleccionar un egresado."
            End If
            obj.CerrarConexion()
            obj = Nothing
        Catch ex As Exception
            Response.Write(ex.Message.ToString)
        End Try
    End Sub

    Protected Sub btnRegresar2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRegresar2.Click
        OcultarDatosActualizar(False)
    End Sub

    Protected Sub btnActualizarDatos_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnActualizarDatos.Click
        Try
            If psotoupdate.Value > 0 Then
                Dim obj As New ClsConectarDatos
                obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
                obj.AbrirConexion()
                obj.Ejecutar("Alumni_ActualizaCorreoTelefono", "C", psotoupdate.Value, Me.txtCorreo1.Text.Trim, Me.txtCorreo2.Text)
                obj.Ejecutar("Alumni_ActualizaCorreoTelefono", "T", psotoupdate.Value, Me.txtFijo.Text.Trim, Me.txtCelular1.Text.Trim, Me.txtCelular2.Text.Trim, String.Empty, String.Empty, Me.txtCentroLaboral.Text.Trim, Me.txtCargo.Text.Trim)
                obj.CerrarConexion()
                obj = Nothing
                Me.txtEgresado.Text = ""
                Me.txtCorreo1.Text = ""
                Me.txtCorreo2.Text = ""
                Me.txtFijo.Text = ""
                Me.txtCelular1.Text = ""
                Me.txtCelular2.Text = ""
                Me.txtCentroLaboral.Text = ""
                Me.txtCargo.Text = ""
                btnBusca_Click(sender, e)
                OcultarDatosActualizar(False)
                Me.lblMensajeFormulario.Text = "Datos Actualizados"
            End If
        Catch ex As Exception
            Response.Write(ex.Message.ToString)
        End Try
    End Sub

    Protected Sub ddlNivel_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlNivel.SelectedIndexChanged
        Try
            If Me.ddlNivel.SelectedIndex > -1 Then
                Dim obj As New ClsConectarDatos
                obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
                obj.AbrirConexion()
                If Me.ddlNivel.SelectedItem.Text <> "TODOS" Then
                    Me.ddlEscuela.DataSource = obj.TraerDataTable("Alumni_ListarEscuelasxNivel", Me.ddlNivel.SelectedItem.Text)
                Else
                    Me.ddlEscuela.DataSource = obj.TraerDataTable("ALUMNI_ListarCarreraProfesional", "2", "2")
                End If
                Me.ddlEscuela.DataTextField = "nombre"
                Me.ddlEscuela.DataValueField = "codigo"

                Me.ddlEscuela.DataBind()
                obj.CerrarConexion()
                obj = Nothing
            End If
        Catch ex As Exception
            Response.Write(ex.Message.ToString)
        End Try
    End Sub

    Protected Sub ddlFacultad_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlFacultad.SelectedIndexChanged
        Try

            If Me.ddlFacultad.SelectedIndex > -1 Then
                Dim obj As New ClsConectarDatos
                obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
                obj.AbrirConexion()

                If Request.QueryString("ctf") = 145 Then
                    If Me.ddlFacultad.SelectedItem.Text = "TODOS" Then
                        Me.ddlEscuela.DataSource = obj.TraerDataTable("ALUMNI_ListarCarreraProfesionalPersonal", 2, Request.QueryString("ID"), "%")
                    Else
                        Me.ddlEscuela.DataSource = obj.TraerDataTable("ALUMNI_ListarCarreraProfesionalPersonal", 2, Request.QueryString("ID"), ddlFacultad.SelectedValue)
                    End If
                Else
                    Me.ddlEscuela.DataSource = obj.TraerDataTable("Alumni_ListarEscuelasxFacultad", CInt(Me.ddlFacultad.SelectedValue))
                End If

                Me.ddlEscuela.DataTextField = "nombre"
                Me.ddlEscuela.DataValueField = "codigo"

                Me.ddlEscuela.DataBind()
                obj.CerrarConexion()
                obj = Nothing
            End If
        Catch ex As Exception
            Response.Write(ex.Message.ToString)
        End Try
    End Sub


End Class
