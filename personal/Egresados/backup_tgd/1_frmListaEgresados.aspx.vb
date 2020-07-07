
Partial Class Egresado_frmListaEgresados
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (IsPostBack = False) Then            
            CargaGridEscuela()
            Me.dpAnio.Items.Add("TODOS")
            For i As Integer = 2004 To Date.Today.Year
                Me.dpAnio.Items.Add(i)
            Next

            'Me.dpAnio.SelectedValue = Date.Today.Year
            Me.dpAnio.SelectedIndex = 0
            OcultaAlEnviar(False)
        End If
    End Sub

    Private Sub CargaGridFacultad()        
        Dim obj As New ClsConectarDatos
        Dim dtFacultad As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        dtFacultad = obj.TraerDataTable("ALUMNI_ListaFacultad", 0)
        obj.CerrarConexion()

        For i As Integer = 0 To dtFacultad.Rows.Count - 1
            Dim listaf As New ListItem
            listaf.Value = dtFacultad.Rows(i).Item("codigo_fac")
            listaf.Text = dtFacultad.Rows(i).Item("nombre_fac")

            Me.dpFacultad.Items.Add(listaf)
        Next

        dtFacultad.Dispose()
        obj = Nothing
    End Sub

	Private Sub CargaGridEscuela()        
        Dim obj As New ClsConectarDatos
        Dim dtCarrera As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        dtCarrera = obj.TraerDataTable("ALUMNI_ListaCarreraProfesional", 0)
        obj.CerrarConexion()

        For i As Integer = 0 To dtCarrera.Rows.Count - 1
            Dim lista As New ListItem
            lista.Value = dtCarrera.Rows(i).Item("codigo_Cpf")
            lista.Text = dtCarrera.Rows(i).Item("nombre_Cpf")

            Me.dpEscuela.Items.Add(lista)
        Next

        dtCarrera.Dispose()
        obj = Nothing
    End Sub

    Protected Sub btnBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBuscar.Click
        Me.lblMensajeFormulario.Text = ""
        cargaGrid()
    End Sub

    Private Sub cargaGrid()
        Try
            Dim anio As Integer = 0
            If (Me.dpAnio.SelectedItem.Text = "TODOS") Then
                anio = 0
            Else
                anio = Integer.Parse(Me.dpAnio.SelectedItem.Text)
            End If
            If (Me.dpEstado.SelectedValue = "1") Then               'CONFIRMADOS
                Dim obj As New ClsConectarDatos
                Dim dtTabla As New Data.DataTable
                obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
                obj.AbrirConexion()
                dtTabla = obj.TraerDataTable("ALUMNI_ListaEgresados", Me.dpTipo.SelectedValue, _
                                             Me.dpEscuela.SelectedValue, anio, Me.txtNombre.Text)
                obj.CerrarConexion()
                Me.gvwEgresados.DataSource = dtTabla
                Me.gvwEgresados.DataBind()
                dtTabla.Dispose()
                obj = Nothing
            ElseIf (Me.dpEstado.SelectedValue = "0") Then           'NO CONFIRMADOS
                Dim obj As New ClsConectarDatos
                Dim dtTabla As New Data.DataTable
                obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
                obj.AbrirConexion()
                dtTabla = obj.TraerDataTable("ALUMNI_ListaPreEgresados", Me.dpTipo.SelectedValue, _
                                             Me.dpEscuela.SelectedValue, anio, _
                                             Me.txtNombre.Text)
                obj.CerrarConexion()
                Me.gvwEgresados.DataSource = dtTabla
                Me.gvwEgresados.DataBind()
                dtTabla.Dispose()
                obj = Nothing
            ElseIf (Me.dpEstado.SelectedValue = "2") Then                   'OBSERVADOS
                Dim obj As New ClsConectarDatos
                Dim dtTabla As New Data.DataTable
                obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
                obj.AbrirConexion()
                dtTabla = obj.TraerDataTable("ALUMNI_ListaegresadosObservados", Me.dpTipo.SelectedValue, _
                                             Me.dpEscuela.SelectedValue, anio, _
                                             Me.txtNombre.Text)
                obj.CerrarConexion()
                Me.gvwEgresados.DataSource = dtTabla
                Me.gvwEgresados.DataBind()
                dtTabla.Dispose()
                obj = Nothing
            End If

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub gvwEgresados_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvwEgresados.DataBound        
        Me.gvwEgresados.Columns(3).Visible = False
        Me.gvwEgresados.Columns(4).Visible = False

        If (Me.dpEstado.SelectedValue = "0") Then
            Me.gvwEgresados.Columns(14).Visible = False
        Else
            Me.gvwEgresados.Columns(14).Visible = True
        End If
        Me.gvwEgresados.Columns(2).Visible = False
    End Sub

    Protected Sub gvwEgresados_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gvwEgresados.PageIndexChanging
        gvwEgresados.PageIndex = e.NewPageIndex()
        cargaGrid()
    End Sub

    Protected Sub gvwEgresados_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvwEgresados.RowDataBound
        Me.gvwEgresados.Columns(2).Visible = True
        Me.gvwEgresados.Columns(3).Visible = True
        Me.gvwEgresados.Columns(4).Visible = True
        Me.gvwEgresados.Columns(14).Visible = True
        'Agregamos la foto y luego ocultamos la celda que contiene el nombre foto
        If (e.Row.RowIndex <> -1) Then
            If (e.Row.Cells(4).Text.Length > 6) Then
                e.Row.Cells(1).Text = "<center><img src='../../librerianet/Egresado/fotos/" & e.Row.Cells(4).Text & "' Height='35px' Width='35px' /></center>"
            Else
                e.Row.Cells(1).Text = "<center><img src='../../librerianet/Egresado/archivos/no_disponible.jpg' Height='35px' Width='35px' /></center>"
            End If
        End If

        'Agregamos el Link del CV y luego ocultamos la celda que contiene el nombre del curriculum
        If (e.Row.RowIndex <> -1) Then
            If (e.Row.Cells(3).Text.Length > 6) Then
                e.Row.Cells(12).Text = "<center><a href=../../librerianet/Egresado/curriculum/" & e.Row.Cells(3).Text & " target='_blank'><img src='../../images/doc.gif' style='border: 0px'/></a></center>"
            Else
                e.Row.Cells(12).Text = "<center><img src='../../images/bloquear.gif' style='border: 0px'/></center>"
            End If

        End If

        'Agregamos el Link para Ver Ficha y luego ocultamos la celda que contiene el codigo de persona
        Dim Enc As New EncriptaCodigos.clsEncripta
        If (e.Row.RowIndex <> -1) Then
            e.Row.Cells(11).Text = "<center><a href='frmFichaEgresado.aspx?pso=" & Enc.Codifica("069" & e.Row.Cells(2).Text) & "' target='_blank'><img src='../../images/previo.gif' style='border: 0px'/></a></center>"

            'Agregamos el Link para enviar mensaje
            If (Me.dpEstado.SelectedValue = "0") Then
                e.Row.Cells(13).Text = "<center><a href=frmEnvioMensaje.aspx?pso=" & Enc.Codifica("069" & e.Row.Cells(2).Text) & "&id=" & Request.QueryString("id") & "&envio=False&KeepThis=true&TB_iframe=true&height=220&width=570&modal=true' title='Envio de Mensaje' class='thickbox''><img src='../../images/Mensaje.gif' style='border: 0px'/></a></center>"
            Else
                e.Row.Cells(13).Text = "<center><a href=frmEnvioMensaje.aspx?pso=" & Enc.Codifica("069" & e.Row.Cells(2).Text) & "&id=" & Request.QueryString("id") & "&envio=True&KeepThis=true&TB_iframe=true&height=220&width=570&modal=true' title='Envio de Mensaje' class='thickbox''><img src='../../images/Mensaje.gif' style='border: 0px'/></a></center>"
            End If
        End If

        'If ((Me.gvwEgresados.Rows.Count < gvwEgresados.PageSize) And (gvwEgresados.Rows.Count + gvwEgresados.PageSize * gvwEgresados.PageIndex < ((gvwEgresados.DataSource)).Rows.Count)) Then
        '    e.Row.Cells(13).Visible = False
        'End If


    End Sub

    Protected Sub btnExportar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExportar.Click
        If (Me.gvwEgresados.Rows.Count > 0) Then
            Dim anio As Integer = 0
            If (Me.dpAnio.SelectedItem.Text = "TODOS") Then
                anio = 0
            Else
                anio = Integer.Parse(Me.dpAnio.SelectedItem.Text)
            End If

            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString

            If (Me.dpEstado.SelectedValue = "0") Then           'CONFIRMADOS
                Dim dtTabla As New Data.DataTable
                obj.AbrirConexion()
                dtTabla = obj.TraerDataTable("ALUMNI_ListaEgresadosExportar", Me.dpTipo.SelectedValue, _
                                             Me.dpEscuela.SelectedValue, anio, _
                                             Me.txtNombre.Text)
                obj.CerrarConexion()
                Me.gvExporta.DataSource = dtTabla
                Me.gvExporta.DataBind()
                dtTabla.Dispose()
            ElseIf (Me.dpEstado.SelectedValue = "1") Then       'NO CONFIRMADOS
                Dim dtTabla As New Data.DataTable
                obj.AbrirConexion()
                dtTabla = obj.TraerDataTable("ALUMNI_ListaPreEgresadosExportar", Me.dpTipo.SelectedValue, _
                                             Me.dpEscuela.SelectedValue, anio, _
                                             Me.txtNombre.Text)
                obj.CerrarConexion()
                Me.gvExporta.DataSource = dtTabla
                Me.gvExporta.DataBind()
                dtTabla.Dispose()
            ElseIf (Me.dpEstado.SelectedValue = "2") Then       'OBSERVADOS
                Dim dtTabla As New Data.DataTable
                obj.AbrirConexion()
                dtTabla = obj.TraerDataTable("ALUMNI_ListaegresadosObservadosExporta", Me.dpTipo.SelectedValue, _
                                             Me.dpEscuela.SelectedValue, anio, _
                                             Me.txtNombre.Text)
                obj.CerrarConexion()
                Me.gvwEgresados.DataSource = dtTabla
                Me.gvwEgresados.DataBind()
                dtTabla.Dispose()
                obj = Nothing
            End If

            obj = Nothing
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
        Dim obj As New ClsConectarDatos
        Dim strClave As String
        strClave = GeneraClave()
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        obj.Ejecutar("ALUMNI_ActivaEgresado", Me.gvwEgresados.DataKeys.Item(e.NewEditIndex).Values(0), strClave)
        obj.CerrarConexion()
        obj = Nothing

        Dim cls As New clsEnvioMensajeAlumni
        Dim strMensaje As String
        Dim dtDirector As New Data.DataTable
        dtDirector = cls.RetornaDirectorAlumni()        

        'Enviar el Usuario(DNI) y su clave
        strMensaje = "Estimado(a) " & Me.gvwEgresados.Rows(e.NewEditIndex).Cells(6).Text & ": <br />"
        strMensaje = strMensaje & "Ud. acaba de ser activado en la lista de Egresados de la universidad. <br />"
        strMensaje = strMensaje & "Usuario: " & Me.gvwEgresados.Rows(e.NewEditIndex).Cells(5).Text & "<br />"
        strMensaje = strMensaje & "Clave: " & strClave & "<br /><br />"

        'cls.EnviarUno("Confirmación de Egresado ALUMNI", strMensaje, dtDirector.Rows(0).Item("codigo_Per"), Me.gvwEgresados.DataKeys.Item(e.NewEditIndex).Values(0), True)
        cargaGrid()
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
            Me.lblMensajeFormulario.Text = "Debe seleccionar un egresado como mínimo"
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
                Me.lblDestinatario.Text = Me.lblDestinatario.Text & "|" & Me.gvwEgresados.Rows(i).Cells(7).Text
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
        Me.btnBuscar.Enabled = Not sw
    End Sub

    Protected Sub btnSalir_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSalir.Click
        Me.txtAsunto.Text = ""
        Me.txtMensaje.Text = ""
        OcultaAlEnviar(False)
    End Sub

    Protected Sub btnGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardar.Click
        If (ValidaMensaje() = True) Then
            Dim Fila As GridViewRow
            Dim cont As Integer = 0
            For i As Integer = 0 To Me.gvwEgresados.Rows.Count - 1
                'Capturamos las filas que estan activas
                Fila = Me.gvwEgresados.Rows(i)
                Dim valor As Boolean = CType(Fila.FindControl("chkElegir"), CheckBox).Checked
                If (valor = True) Then
                    'Capturamos los registros activos
                    Dim cls As New clsEnvioMensajeAlumni
                    cls.EnviarUno(Me.txtAsunto.Text, Me.txtMensaje.Text, cls.RetornaDirectorAlumni.Rows(0).Item(0), Me.gvwEgresados.DataKeys(i).Values(0))
                End If
            Next
        End If
        Me.lblMensajeFormulario.Text = "Mensaje Enviado"
        Me.txtAsunto.Text = ""
        Me.txtMensaje.Text = ""
        OcultaAlEnviar(False)
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
End Class