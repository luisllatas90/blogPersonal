
Partial Class Egresado_frmListaEgresados
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (IsPostBack = False) Then            
            OcultaAlEnviar(False)
            OcultarDatosActualizar(False)
            CargarListas()
        End If


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
        Me.gvExporta.Visible = Not sw
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
        dt = obj.TraerDataTable("ALUMNI_ListarFacultad")
        Me.ddlFacultad.DataSource = dt
        Me.ddlFacultad.DataTextField = "nombre"
        Me.ddlFacultad.DataValueField = "codigo"
        Me.ddlFacultad.DataBind()
        Me.ddlFacultad.SelectedValue = 0
        dt.Dispose()
        '#Escuela
        dt = New Data.DataTable
        dt = obj.TraerDataTable("ALUMNI_ListarCarreraProfesional")
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
        dtTabla = obj.TraerDataTable("ALUMNI_ListarAlumni", _
                                     IIf(Me.ddlNivel.SelectedIndex = 0, "%", Me.ddlNivel.SelectedItem.Text), _
                                     IIf(Me.ddlModalidad.SelectedIndex = 0, "%", Me.ddlModalidad.SelectedItem.Text), _
                                     IIf(Me.ddlFacultad.SelectedIndex = 5, "%", Me.ddlFacultad.SelectedItem.Text), _
                                     IIf(Me.ddlEscuela.SelectedIndex = 0, "%", Me.ddlEscuela.SelectedItem.Text), _
                                     IIf(Me.ddlEgreso.SelectedIndex = 0, "", Me.ddlEgreso.SelectedItem.Text), _
                                     IIf(Me.ddlBachiller.SelectedIndex = 0, "%", Me.ddlBachiller.Text), _
                                     IIf(Me.ddlTitulo.SelectedIndex = 0, "%", Me.ddlTitulo.Text), _
                                     IIf(Me.txtApellidoNombre.Text.Trim = "", "%", Me.txtApellidoNombre.Text.Trim), _
                                     IIf(Me.ddlSexo.SelectedIndex = 0, "%", Me.ddlSexo.SelectedItem.Text))
        'IIf(Me.ddlCorreo.SelectedIndex = 0, "%", Me.ddlCorreo.SelectedItem.Text))
        obj.CerrarConexion()
        obj = Nothing
        Me.lblMensajeFormulario.Text = "Se encontraron " & dtTabla.Rows.Count & " registro(s)."
        Return dtTabla
    End Function
    

    'Protected Sub gvwEgresados_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvwEgresados.DataBound        
    '    Me.gvwEgresados.Columns(3).Visible = False
    '    Me.gvwEgresados.Columns(4).Visible = False

    '    If (Me.dpEstado.SelectedValue = "0") Then
    '        Me.gvwEgresados.Columns(14).Visible = False
    '    Else
    '        Me.gvwEgresados.Columns(14).Visible = True
    '    End If
    '    Me.gvwEgresados.Columns(2).Visible = False
    'End Sub

    Protected Sub gvwEgresados_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gvwEgresados.PageIndexChanging
        gvwEgresados.PageIndex = e.NewPageIndex()
        cargarGrid()
    End Sub

    'Protected Sub gvwEgresados_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvwEgresados.RowDataBound
    '    Me.gvwEgresados.Columns(2).Visible = True
    '    Me.gvwEgresados.Columns(3).Visible = True
    '    Me.gvwEgresados.Columns(4).Visible = True
    '    Me.gvwEgresados.Columns(14).Visible = True
    '    'Agregamos la foto y luego ocultamos la celda que contiene el nombre foto
    '    If (e.Row.RowIndex <> -1) Then
    '        If (e.Row.Cells(4).Text.Length > 6) Then
    '            e.Row.Cells(1).Text = "<center><img src='../../librerianet/Egresado/fotos/" & e.Row.Cells(4).Text & "' Height='35px' Width='35px' /></center>"
    '        Else
    '            e.Row.Cells(1).Text = "<center><img src='../../librerianet/Egresado/archivos/no_disponible.jpg' Height='35px' Width='35px' /></center>"
    '        End If
    '    End If

    '    'Agregamos el Link del CV y luego ocultamos la celda que contiene el nombre del curriculum
    '    If (e.Row.RowIndex <> -1) Then
    '        If (e.Row.Cells(3).Text.Length > 6) Then
    '            e.Row.Cells(12).Text = "<center><a href=../../librerianet/Egresado/curriculum/" & e.Row.Cells(3).Text & " target='_blank'><img src='../../images/doc.gif' style='border: 0px'/></a></center>"
    '        Else
    '            e.Row.Cells(12).Text = "<center><img src='../../images/bloquear.gif' style='border: 0px'/></center>"
    '        End If

    '    End If

    '    'Agregamos el Link para Ver Ficha y luego ocultamos la celda que contiene el codigo de persona
    '    Dim Enc As New EncriptaCodigos.clsEncripta
    '    If (e.Row.RowIndex <> -1) Then
    '        e.Row.Cells(11).Text = "<center><a href='frmFichaEgresado.aspx?pso=" & Enc.Codifica("069" & e.Row.Cells(2).Text) & "' target='_blank'><img src='../../images/previo.gif' style='border: 0px'/></a></center>"

    '        'Agregamos el Link para enviar mensaje
    '        If (Me.dpEstado.SelectedValue = "0") Then
    '            e.Row.Cells(13).Text = "<center><a href=frmEnvioMensaje.aspx?pso=" & Enc.Codifica("069" & e.Row.Cells(2).Text) & "&id=" & Request.QueryString("id") & "&envio=False&KeepThis=true&TB_iframe=true&height=220&width=570&modal=true' title='Envio de Mensaje' class='thickbox''><img src='../../images/Mensaje.gif' style='border: 0px'/></a></center>"
    '        Else
    '            e.Row.Cells(13).Text = "<center><a href=frmEnvioMensaje.aspx?pso=" & Enc.Codifica("069" & e.Row.Cells(2).Text) & "&id=" & Request.QueryString("id") & "&envio=True&KeepThis=true&TB_iframe=true&height=220&width=570&modal=true' title='Envio de Mensaje' class='thickbox''><img src='../../images/Mensaje.gif' style='border: 0px'/></a></center>"
    '        End If
    '    End If

    '    'If ((Me.gvwEgresados.Rows.Count < gvwEgresados.PageSize) And (gvwEgresados.Rows.Count + gvwEgresados.PageSize * gvwEgresados.PageIndex < ((gvwEgresados.DataSource)).Rows.Count)) Then
    '    '    e.Row.Cells(13).Visible = False
    '    'End If


    'End Sub

    Protected Sub btnExportar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExportar.Click
        Dim dt As New Data.DataTable
        dt = CargarGrid()
        If (dt.Rows.Count > 0) Then
            dt.Columns.Remove("codigo_Pso")
            dt.Columns.Remove("cv_Ega")
            dt.Columns.Remove("foto_Ega")            
            Me.gvExporta.DataSource = dt
            Me.gvExporta.DataBind()
            Axls()
        Else
            Response.Write("<script> alert('La tabla no tiene datos')</script>")
        End If
    End Sub

    Private Sub Axls()
        Dim sb As StringBuilder = New StringBuilder()
        Dim SW As System.IO.StringWriter = New System.IO.StringWriter(sb)
        Dim htw As HtmlTextWriter = New HtmlTextWriter(SW)
        Dim Page As Page = New Page()
        Dim form As HtmlForm = New HtmlForm()
        Me.gvExporta.EnableViewState = False
        Page.EnableEventValidation = False
        Page.DesignerInitialize()
        Page.Controls.Add(form)
        form.Controls.Add(Me.gvExporta)
        Page.RenderControl(htw)
        Response.Clear()
        Response.Buffer = True
        Response.ContentType = "application/vnd.ms-excel"
        Response.AddHeader("Content-Disposition", "attachment;filename=Reporte_Egresados" & ".xls")
        Response.Charset = "UTF-8"
        Response.ContentEncoding = Encoding.Default
        Response.Write(sb.ToString())
        Response.End()
        Me.gvExporta.DataSource = Nothing
        Me.gvExporta.DataBind()
    End Sub

    Protected Sub gvwEgresados_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles gvwEgresados.RowEditing
        'Dim obj As New ClsConectarDatos
        'Dim strClave As String
        'strClave = GeneraClave()
        'obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        'obj.AbrirConexion()
        'obj.Ejecutar("ALUMNI_ActivaEgresado", Me.gvwEgresados.DataKeys.Item(e.NewEditIndex).Values(0), strClave)
        'obj.CerrarConexion()
        'obj = Nothing

        'Dim cls As New clsEnvioMensajeAlumni
        'Dim strMensaje As String
        'Dim dtDirector As New Data.DataTable
        'dtDirector = cls.RetornaDirectorAlumni()

        ''Enviar el Usuario(DNI) y su clave
        'strMensaje = "Estimado(a) " & Me.gvwEgresados.Rows(e.NewEditIndex).Cells(6).Text & ": <br />"
        'strMensaje = strMensaje & "Ud. acaba de ser activado en la lista de Egresados de la universidad. <br />"
        'strMensaje = strMensaje & "Usuario: " & Me.gvwEgresados.Rows(e.NewEditIndex).Cells(5).Text & "<br />"
        'strMensaje = strMensaje & "Clave: " & strClave & "<br /><br />"
        ''cls.EnviarUno("Confirmación de Egresado ALUMNI", strMensaje, dtDirector.Rows(0).Item("codigo_Per"), Me.gvwEgresados.DataKeys.Item(e.NewEditIndex).Values(0), True)
        'btnBusca_Click(sender, e)
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
            Me.lblMensajeFormulario.Text = "Debe seleccionar un egresado"
        End If
    End Sub

    Private Function validaCheckActivo() As Boolean
        Dim Fila As GridViewRow
        Dim sw As Byte = 0
        Me.lblDestinatario.Text = ""
        For i As Integer = 0 To Me.gvwEgresados.Rows.Count - 1
            'Capturamos las filas que estan activas
            Fila = Me.gvwEgresados.Rows(i)
            Dim valor As Boolean = CType(Fila.FindControl("chkElegir"), CheckBox).Checked
            If (valor = True) Then
                sw = 1
                Me.lblDestinatario.Text = Me.lblDestinatario.Text & "</br> " & Me.gvwEgresados.Rows(i).Cells(14).Text & ", " & Me.gvwEgresados.Rows(i).Cells(13).Text
            End If
        Next

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
        Me.txtMensaje.Visible = sw
        Me.btnGuardar.Visible = sw
        Me.btnSalir.Visible = sw
        Me.lblEnvioMultiple.Visible = sw
        Me.lblDestinatario.Visible = sw
        Me.lblEnviado.Visible = sw
        Me.btnMensaje.Visible = Not sw
        Me.gvExporta.Visible = Not sw
        Me.gvwEgresados.Visible = Not sw
        Me.btnNuevoEgresado.Visible = Not sw
        Me.btnExportar.Visible = Not sw        
    End Sub

    Protected Sub btnSalir_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSalir.Click
        Me.txtAsunto.Text = ""
        Me.txtMensaje.Text = ""
        OcultaAlEnviar(False)
    End Sub

    Protected Sub btnGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardar.Click
        Try
            If (ValidaMensaje() = True) Then
                Dim Fila As GridViewRow
                Dim cls As New ClsEnvioMailAlumni
                Dim Mensaje As String = ""
                Dim para As String = ""
                Dim obj As New ClsConectarDatos
                obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
                obj.AbrirConexion()

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
                        If Me.gvwEgresados.DataKeys(i).Values("CORREO").ToString = "SI" Then
                            Mensaje = IIf(Me.gvwEgresados.DataKeys(i).Values("SEXO").ToString = "F", "Estimada ", "Estimado ")
                            Mensaje &= Me.gvwEgresados.DataKeys(i).Values("NOMBRES").ToString & ":<br/><br/>"
                            Mensaje &= Me.txtMensaje.Text.Trim
                            Mensaje &= "</br>----------------------------</br>"
                            Mensaje &= "Dirección de Alumni USAT"
                            para = Me.gvwEgresados.DataKeys(i).Values("EMAIL").ToString
                            'para = "yperez@usat.edu.pe"
                            Me.lblMensajeFormulario.Text = cls.EnviarMailAd("alumni@usat.edu.pe", "alumniUSAT", para, Me.txtAsunto.Text.Trim, Mensaje, True, "", "alumni@usat.edu.pe", strRuta, Me.FileUpload1.FileName)
                            '#Guardar en Bitácora 
                            obj.Ejecutar("Insertar_Alumni_BitacoraEnviaMail", Date.Now, CInt(Me.gvwEgresados.DataKeys(i).Values("codigo_pso").ToString), para, Me.txtAsunto.Text.Trim, Me.txtMensaje.Text.Trim, Me.FileUpload1.FileName.ToString)
                        Else
                            Me.lblMensajeFormulario.Text = "Correo no registrado"
                        End If
                    End If
                Next
                cls = Nothing
                obj.CerrarConexion()
                obj = Nothing
            End If
            Me.txtAsunto.Text = ""
            Me.txtMensaje.Text = ""
            OcultaAlEnviar(False)
        Catch ex As Exception
            Me.lblMensajeFormulario.Text = ex.Message & " " & ex.StackTrace.ToString
        End Try
    End Sub

    Private Function ValidaMensaje() As Boolean
        If (Me.txtAsunto.Text = "") Then
            Me.lblMensajeFormulario.Text = "Debe ingresar el asunto del mensaje"
            Return False
        End If

        If (Me.txtMensaje.Text = "") Then
            Me.lblMensajeFormulario.Text = "Debe ingresar el mensaje a enviar"
            Return False
        End If

        Return True
    End Function

    Protected Sub btnBusca_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBusca.Click
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
        End If
        Me.gvwEgresados.DataBind()
        dt.Dispose()
    End Sub


    Protected Sub gvwEgresados_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvwEgresados.RowDataBound
        If (e.Row.RowIndex <> -1) Then
            If (e.Row.Cells(3).Text.Length > 6) Then
                e.Row.Cells(3).Text = "<center><img src='../../librerianet/Egresado/fotos/" & e.Row.Cells(3).Text & "' Height='28px' Width='25px' /></center>"
            Else
                e.Row.Cells(3).Text = "<center><img src='../../librerianet/Egresado/archivos/no_disponible.jpg' Height='28px' Width='25px' /></center>"
            End If
            'Dim Enc As New EncriptaCodigos.clsEncripta
            If (e.Row.RowIndex <> -1) Then
                'e.Row.Cells(15).Text = "<center><a href='frmFichaEgresado.aspx?pso=" & Enc.Codifica("069" & e.Row.Cells(2).Text) & "' target='_blank'><img src='../../images/previo.gif' style='border: 0px'/></a></center>"
                e.Row.Cells(15).Text = "<center><a href='frmFichaEgresado.aspx?pso=" & e.Row.Cells(2).Text & "' target='_blank'><img src='../../images/previo.gif' style='border: 0px'/></a></center>"
            End If

            e.Row.Cells(7).Text = e.Row.Cells(7).Text.ToUpper
            e.Row.Cells(5).Text = e.Row.Cells(5).Text.Substring(0, 3)

        End If
    End Sub

    Protected Sub btnActualizar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnActualizar.Click
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
    End Sub

    Protected Sub btnRegresar2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRegresar2.Click        
        OcultarDatosActualizar(False)
    End Sub

    Protected Sub btnActualizarDatos_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnActualizarDatos.Click

        If psotoupdate.Value > 0 Then
            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            obj.Ejecutar("Alumni_ActualizaCorreoTelefono", "C", psotoupdate.Value, Me.txtCorreo1.Text.Trim, Me.txtCorreo2.Text)
            obj.Ejecutar("Alumni_ActualizaCorreoTelefono", "T", psotoupdate.Value, Me.txtFijo.Text.Trim, Me.txtCelular1.Text.Trim, Me.txtCelular2.Text.Trim)
            obj.CerrarConexion()
            obj = Nothing
            Me.txtEgresado.Text = ""
            Me.txtCorreo1.Text = ""
            Me.txtCorreo2.Text = ""
            Me.txtFijo.Text = ""
            Me.txtCelular1.Text = ""
            Me.txtCelular2.Text = ""
            btnBusca_Click(sender, e)
            OcultarDatosActualizar(False)
            Me.lblMensajeFormulario.Text = "Datos Actualizados"
        End If
    End Sub

    Protected Sub ddlNivel_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlNivel.SelectedIndexChanged
        If Me.ddlNivel.SelectedIndex > -1 Then
            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            If Me.ddlNivel.SelectedItem.Text <> "TODOS" Then
                Me.ddlEscuela.DataSource = obj.TraerDataTable("Alumni_ListarEscuelasxNivel", Me.ddlNivel.SelectedItem.Text)
            Else
                Me.ddlEscuela.DataSource = obj.TraerDataTable("ALUMNI_ListarCarreraProfesional")
            End If
            Me.ddlEscuela.DataTextField = "nombre"
            Me.ddlEscuela.DataValueField = "codigo"

            Me.ddlEscuela.DataBind()
            obj.CerrarConexion()
            obj = Nothing            
        End If        
    End Sub
End Class