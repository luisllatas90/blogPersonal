﻿
Partial Class academico_horarios_administrar_frmasignarambientesv2
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (Session("id_per") Is Nothing) Then
            Response.Redirect("../../../../sinacceso.html")
        End If

        If IsPostBack = False Then
            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            ClsFunciones.LlenarListas(Me.ddlCiclo, obj.TraerDataTable("AsignarAmbiente_ListarCiclos"), "codigo_cac", "descripcion_cac")
            'ClsFunciones.LlenarListas(Me.ddlTipoEstudio, obj.TraerDataTable("AsignarAmbiente_ListarTipoEstudio"), "codigo_test", "descripcion_test")
            ClsFunciones.LlenarListas(Me.ddlTipoAmbiente, obj.TraerDataTable("AsignarAmbiente_ListarAmbientes"), "codigo_tam", "descripcion_Tam", "--Seleccione--")
            'Me.ddlTipoEstudio.SelectedValue = 2 'pregrado
            Me.ddlTipoAmbiente.SelectedValue = 3 'aula
            Me.ddlEstadoAmbiente.Items.Add("Todos")
            Me.ddlEstadoAmbiente.Items.Add("Asignados")
            Me.ddlEstadoAmbiente.Items.Add("No Asignados")
            Me.ddlEstadoAmbiente.SelectedIndex = 0
            CargarFechas()
            CargarHorasMinutos()
            cargarEscuelas()
            obj.CerrarConexion()
            obj = Nothing
            'ddlTipoEstudio.Enabled = False
            Me.ddlDias.Enabled = False
        End If
    End Sub
    Sub CargarFechas()
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Dim tb As New Data.DataTable
        tb = obj.TraerDataTable("AsignarAmbiente_ListarCiclos", Me.ddlCiclo.SelectedValue, Me.ddlTipoEstudio.SelectedValue)
        Me.txtDesde.Value = tb.Rows(0)(2).ToString
        Me.txtHasta.Value = tb.Rows(0)(3).ToString
        obj.CerrarConexion()
        obj = Nothing
        tb.Dispose()
    End Sub
    Sub cargarEscuelas()
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Me.gridEscuelas.DataSource = obj.TraerDataTable("AsignarAmbiente_ListarEscuelas", Me.ddlTipoEstudio.SelectedValue)
        gridEscuelas.DataBind()
        obj.CerrarConexion()
        obj = Nothing
    End Sub
    Sub cargarAmbientesxEscuela(ByVal codigo_cpf As Integer)
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Dim tipA As String = "%"
        If Me.ddlEstadoAmbiente.SelectedIndex = 0 Then
            tipA = "T"
        Else
            tipA = IIf(Me.ddlEstadoAmbiente.SelectedIndex = 1, "A", "N")
        End If
        Me.gridAmbientes.DataSource = obj.TraerDataTable("AsignarAmbiente_ListarAmbientexEscuela", Me.ddlCiclo.SelectedValue, codigo_cpf, Me.ddlTipoAmbiente.SelectedValue, tipA)
        'Response.Write(Me.ddlCiclo.SelectedValue & "," & codigo_cpf & "," & Me.ddlTipoAmbiente.SelectedValue & "," & tipA)
        'Me.gridAmbientes.Columns.Item(3).Visible = False
        gridAmbientes.DataBind()
        If gridAmbientes.Rows.Count Then
            Me.divgridamb.Attributes.Add("class", "celda1")
        End If
        obj.CerrarConexion()
        obj = Nothing
        Me.lblMensaje.Text = ""
    End Sub
    Sub CargarHorasMinutos()
        Dim item As String
        For i As Integer = 1 To 24
            If i < 10 Then
                item = "0" & i.ToString
            Else
                item = i.ToString
            End If
            Me.ddlInicioHora.Items.Add(New ListItem(item.ToString(), item.ToString()))
            Me.ddlFinHora.Items.Add(New ListItem(item.ToString(), item.ToString()))
        Next i
        Me.ddlInicioHora.SelectedIndex = 6

        For i As Integer = 0 To 59
            If i < 10 Then
                item = "0" & i.ToString
            Else
                item = i.ToString
            End If
            Me.ddlInicioMinuto.Items.Add(New ListItem(item.ToString(), item.ToString()))
            Me.ddlFinMinuto.Items.Add(New ListItem(item.ToString(), item.ToString()))
        Next i
        Me.ddlFinHora.SelectedIndex = 21
    End Sub
    Protected Sub ddlCiclo_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlCiclo.SelectedIndexChanged
        CargarFechas()
    End Sub
    Protected Sub ddlTipoEstudio_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlTipoEstudio.SelectedIndexChanged
        cargarEscuelas()
        Session("scodigo_cpf") = 0
        ' If Me.ddlTipoEstudio.SelectedValue = 3 Then
        'Me.ddlDias.Enabled = True
        CargarFechas()
        'Else
        'Me.ddlDias.Enabled = False
        'End If
    End Sub

    Protected Sub gridEscuelas_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles gridEscuelas.SelectedIndexChanged
        Dim row As GridViewRow = gridEscuelas.SelectedRow
        Dim id As Integer = Convert.ToInt32(gridEscuelas.DataKeys(row.RowIndex).Value)
        Session("scodigo_cpf") = id
        Me.lblEscuela.Text = "ESCUELA PROFESIONAL DE " & Me.gridEscuelas.DataKeys(row.RowIndex).Values("nombre_cpf").ToString.ToUpper
        cargarAmbientesxEscuela(Session("scodigo_cpf"))
    End Sub

    Protected Sub ddlTipoAmbiente_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlTipoAmbiente.SelectedIndexChanged
        cargarAmbientesxEscuela(Session("scodigo_cpf"))
    End Sub

    Protected Sub ddlEstadoAmbiente_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlEstadoAmbiente.SelectedIndexChanged
        cargarAmbientesxEscuela(Session("scodigo_cpf"))
    End Sub

    Protected Sub gridAmbientes_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridAmbientes.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim check As CheckBox
            check = e.Row.FindControl("chkElegir")
            If gridAmbientes.DataKeys(e.Row.RowIndex).Values("codigo_aam").ToString > 0 Then
                check.Checked = True
                e.Row.Cells(2).ControlStyle.ForeColor = Drawing.Color.Blue
            Else
                e.Row.Cells(2).ControlStyle.ForeColor = Drawing.Color.Green
            End If

            Dim labelOtrasEsc As LinkButton
            labelOtrasEsc = e.Row.FindControl("lblOtrasEscuelas")
            Dim obj As New ClsConectarDatos
            Dim tb As New Data.DataTable
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            tb = obj.TraerDataTable("AsingarAmbiente_ConsultarAsignacion", gridAmbientes.DataKeys(e.Row.RowIndex).Values("codigo_amb").ToString, Me.ddlCiclo.SelectedValue)
            labelOtrasEsc.Text = "(" & tb.Rows.Count.ToString & ")"
            'labelOtrasEsc.PostBackUrl = "frmasignarambientev2Detalle.aspx?codigo_amb=" & gridAmbientes.DataKeys(e.Row.RowIndex).Values("codigo_amb").ToString
            Session("scodigo_cac") = Me.ddlCiclo.SelectedValue
            labelOtrasEsc.OnClientClick = "ejecutar(" & gridAmbientes.DataKeys(e.Row.RowIndex).Values("codigo_amb").ToString & ");"
            obj.CerrarConexion()
            obj = Nothing
            tb.Dispose()
        End If
    End Sub

    Protected Sub BtnGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnGuardar.Click
        If CInt(Session("scodigo_cpf")) > 0 Then
            Dim Fila As GridViewRow
            Dim codigo_amb As Integer = 0
            Dim codigo_aam As Integer = 0
            Dim usuario As Integer
            Dim fechaInicio As String
            Dim fechaFin As String
           
            Dim tb As New Data.DataTable
            Dim nroA As Integer = 0
            Dim nroD As Integer = 0
            Dim msgN As String = ""
            Dim valor As Boolean

            usuario = CInt(Session("id_per").ToString)
            fechaInicio = Me.txtDesde.Value & " " & Me.ddlInicioHora.SelectedValue & ":" & Me.ddlInicioMinuto.SelectedValue & ":00"
            fechaFin = Me.txtHasta.Value & " " & Me.ddlFinHora.SelectedValue & ":" & Me.ddlInicioMinuto.SelectedValue & ":00"

            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            For i As Integer = 0 To Me.gridAmbientes.Rows.Count - 1
                Fila = Me.gridAmbientes.Rows(i)
                codigo_amb = gridAmbientes.DataKeys(i).Values("codigo_amb").ToString
                codigo_aam = gridAmbientes.DataKeys(i).Values("codigo_aam").ToString
                valor = CType(Fila.FindControl("chkElegir"), CheckBox).Checked
                If (valor = True And codigo_aam = 0) Then

                    'Solo para Profesionalización/Maestría, puede elegir días dentro de un periodo.
                    If (Me.ddlTipoEstudio.SelectedValue = 3 Or Me.ddlTipoEstudio.SelectedValue = 5) And Me.ddlDias.SelectedValue <> "TO" Then
                        Dim fechaInicioRef As Date = Date.Parse(fechaInicio)
                        Dim fechaFinRef As Date = Date.Parse(fechaFin)
                        Do
                            If WeekdayName(Weekday(fechaInicioRef)) = Me.ddlDias.SelectedItem.Text.ToLower Then
                                obj.AbrirConexion()
                                Dim fechaInicioG As New Date(fechaInicioRef.Year, fechaInicioRef.Month, fechaInicioRef.Day, Me.ddlInicioHora.SelectedValue, Me.ddlInicioMinuto.SelectedValue, 0)
                                Dim fechaFinG As New Date(fechaInicioRef.Year, fechaInicioRef.Month, fechaInicioRef.Day, Me.ddlFinHora.SelectedValue, Me.ddlFinMinuto.SelectedValue, 0)                                
                                tb = obj.TraerDataTable("AsignarAmbiente_Registrar", codigo_amb, CInt(Session("Scodigo_cpf")), Me.ddlCiclo.SelectedValue, usuario, fechaInicioG, fechaFinG)
                                obj.CerrarConexion()
                                nroA = nroA + 1
                            End If
                            fechaInicioRef = fechaInicioRef.AddDays(1)
                        Loop While fechaInicioRef <= fechaFinRef
                    Else
                        obj.AbrirConexion()
                        tb = obj.TraerDataTable("AsignarAmbiente_Registrar", codigo_amb, CInt(Session("Scodigo_cpf")), Me.ddlCiclo.SelectedValue, usuario, fechaInicio, fechaFin)
                        obj.CerrarConexion()
                        nroA = nroA + 1
                    End If
                ElseIf (valor = False And codigo_aam > 0) Then
                    obj.AbrirConexion()
                    tb = obj.TraerDataTable("AsignarAmbiente_Eliminar", codigo_aam)
                    'elimina todos los detalles de la asignacion y la cabecera.
                    obj.CerrarConexion()
                    If tb.Rows.Count Then
                        msgN = tb.Rows(0).Item(0).ToString
                    Else
                        nroD = nroD + 1
                    End If
                End If
                'Response.Write(codigo_amb.ToString & "," & (Session("scodigo_cpf")) & "," & Me.ddlCiclo.SelectedValue & "," & usuario.ToString & "," & fechaInicio & "," & fechaFin)                    
            Next
            obj = Nothing
            cargarAmbientesxEscuela(CInt(Session("Scodigo_cpf")))
            Me.lblMensaje.Text = "Se asignaron (" & nroA & ") y se eliminaron (" & nroD & ") ambiente(s). " & msgN           
        End If
    End Sub
    Public Shared Function GetHTMLFromAddress(ByVal Address As String) As String
        Dim ASCII As New System.Text.ASCIIEncoding
        Dim netWeb As New System.Net.WebClient
        Dim lsWeb As String
        Dim laWeb As Byte()

        Try
            laWeb = netWeb.DownloadData(Address)
            lsWeb = ASCII.GetString(laWeb)
        Catch ex As Exception
            Throw New Exception(ex.Message.ToString + ex.ToString)
        End Try
        Return lsWeb
    End Function
    Public Shared Function GetApplicationPath() As String
        Dim strPath As String = ""
        If (Not HttpContext.Current.Request.Url Is Nothing) Then
            strPath = HttpContext.Current.Request.Url.AbsoluteUri.Substring(0, (HttpContext.Current.Request.Url.AbsoluteUri.ToLower.IndexOf(HttpContext.Current.Request.ApplicationPath.ToLower, CType((HttpContext.Current.Request.Url.AbsoluteUri.ToLower.IndexOf(HttpContext.Current.Request.Url.Authority.ToLower) + HttpContext.Current.Request.Url.Authority.Length), Integer)) + HttpContext.Current.Request.ApplicationPath.Length))
        End If
        strPath = strPath & "/"
        Return strPath
    End Function
    Protected Sub BtnEnviarMail_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnEnviarMail.Click


        Dim bodycorreo As String

        bodycorreo = "<html>"
        bodycorreo = bodycorreo & "<head><meta charset=""UTF-8""></head><body style=""text-align:justify; font-family:Tahoma;""> <div style=""color:#284775; Background-color:white; border-color:#284775; border:1px solid; padding:10px;"">"
        bodycorreo = bodycorreo & "<p><b>Estimado(a) Director(a): $director$ </b></p>"
        bodycorreo = bodycorreo & "<p>Reciba un cordial saludo, por encargo de la Coordinadora Acad&eacute;mica, MSc. Martha Tesén Arroyo, se hace de su conocimiento que para la Escuela Profesional de <b> $nombre_Cpf$ </b> se han asignado los siguientes ambientes para el registro de la programaci&oacute;n <b> $descripcion_cac$ </b></p>"
        bodycorreo = bodycorreo & "<p><b>Semestre $descripcion_cac$ </b></p>"
        bodycorreo = bodycorreo & "<p> $ambientes$  </p>"
        bodycorreo = bodycorreo & "<p>En los ambiente que digan <b>EXTERNO</b>, s&oacute;lo se deben programar asignaturas que <b>NO</b> se dictar&aacute;n en aulas. Ejm. Pr&aacute;cticas, internado, asesor&iacute;as, tesis, etc.</p>"
        bodycorreo = bodycorreo & "<p>Para revisar como van registrando los horarios en los ambientes podrán ingresar a la consulta con la siguiente ruta: </p>"
        bodycorreo = bodycorreo & "<p><b>Gesti&oacute;n de Escuelas Pre-Grado&gt;&gt;Consultas y Reportes&gt;&gt;Horarios&gt;&gt;Por ambientes asignados por Escuela.</b></p>"
        bodycorreo = bodycorreo & "<p>Agradeceremos optimizar al m&aacute;ximo los ambientes programando desde las <b>07:00 hasta las 21:00 horas</b>, tratando de no perjudicar a los estudiantes con los horarios. </p>"
        bodycorreo = bodycorreo & "<p>Con respecto a <b>Laboratorios de C&oacute;mputo</b>, deben ser coordinados con el Lic. Jos&eacute; Alvarado (jalvarado@usat.edu.pe), teniendo en cuenta que los horarios deben estar programados de 07:00 – 19:00 horas.</p>"
        bodycorreo = bodycorreo & "<p>Saludos cordiales, Campus Virtual</p>"
        bodycorreo = bodycorreo & "</div></body></html>"


        Dim Template As New StringBuilder
        Template.Append(bodycorreo)
        Template.Replace("???", "")
        Dim objDatos As New ClsConectarDatos
        Dim tabla As New Data.DataTable
        Dim correo As String = ""
        Dim descripcion_cac As String = ""
        objDatos.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        objDatos.AbrirConexion()
        tabla = objDatos.TraerDataTable("AsignarAmbiente_ConsultarDatosCorreo", CInt(Session("Scodigo_cpf")), ddlCiclo.SelectedValue, "D")
        If tabla.Rows.Count Then
            Template.Replace("$director$", tabla.Rows(0).Item("Personal").ToString)
            Template.Replace("$nombre_Cpf$", tabla.Rows(0).Item("nombre_Cpf").ToString)
            Template.Replace("$descripcion_cac$", tabla.Rows(0).Item("descripcion_cac").ToString)
            correo = tabla.Rows(0).Item("usuario_per").ToString & "@usat.edu.pe"
            descripcion_cac = tabla.Rows(0).Item("descripcion_cac").ToString        
        End If

        tabla.Dispose()
        tabla = objDatos.TraerDataTable("AsignarAmbiente_ConsultarDatosCorreo", CInt(Session("Scodigo_cpf")), ddlCiclo.SelectedValue, "A")
        objDatos.CerrarConexion()
        objDatos = Nothing
        Dim ambientes As String = ""
        If tabla.Rows.Count Then
            For i As Integer = 0 To tabla.Rows.Count - 1
                ambientes = ambientes & "<b>" & (i + 1).ToString & ".</b>" & " " & tabla.Rows(i).Item("ambiente").ToString & " " & tabla.Rows(i).Item("ubicacion").ToString & " </br>"
            Next
        End If
        Template.Replace("$ambientes$", ambientes)

        'correo = "yperez@usat.edu.pe"
        EnviarMailAd("campusvirtual@usat.edu.pe", "CAMPUS VIRTUAL", correo, "Programación horarios semestre " & descripcion_cac, Template.ToString, True, "mtesen@usat.edu.pe,eeda@usat.edu.pe,mfhuidobro@usat.edu.pe", "mfhuidobro@usat.edu.pe")
        'EnviarMailAd("campusvirtual@usat.edu.pe", "CAMPUS VIRTUAL", correo, "Programación horarios semestre " & descripcion_cac, Template.ToString, True, "yperez@usat.edu.pe", "mfhuidobro@usat.edu.pe")


    End Sub
    Public Function EnviarMailAd(ByVal De As String, ByVal nombreenvia As String, ByVal Para As String, ByVal Asunto As String, ByVal Mensaje As String, ByVal HTML As Boolean, Optional ByVal concopia As String = "", Optional ByVal replyTo As String = "", Optional ByVal rutaAdjunto As String = "", Optional ByVal nombreAdjunto As String = "") As Boolean

        Dim correo As New System.Net.Mail.MailMessage()
        correo.From = New System.Net.Mail.MailAddress(De, nombreenvia)


        correo.To.Add(Para)
        If replyTo.Trim <> "" Then
            correo.ReplyTo = New System.Net.Mail.MailAddress(replyTo)
        End If

        If concopia.Trim <> "" And concopia.Trim <> ";" Then
            Dim Destinos() As String
            Destinos = Split(concopia, ";")

            For i As Integer = 0 To Destinos.Length - 1
                correo.CC.Add(Trim(Destinos(i)))
            Next
        End If

        If rutaAdjunto <> "" Then
            Dim att As New System.Net.Mail.Attachment(rutaAdjunto)
            att.Name = nombreAdjunto
            correo.Attachments.Add(att)
        End If

        correo.Subject = Asunto
        correo.Body = Mensaje '& "headers:" & correo.Headers.Item(0).ToString
        correo.IsBodyHtml = HTML
        correo.Priority = System.Net.Mail.MailPriority.High

        Dim smtp As New System.Net.Mail.SmtpClient
        smtp.Port = 25
        smtp.Host = "172.16.1.5"
        smtp.DeliveryMethod = Net.Mail.SmtpDeliveryMethod.Network
        smtp.EnableSsl = False

        ' If Len(_mUser) <> 0 And Len(_mPass) <> 0 Then
        ' smtp.UseDefaultCredentials = True
        'Else
        smtp.Credentials = New System.Net.NetworkCredential("USAT\campusvirtual", "NLH951")
        'End If

        Try
            smtp.Send(correo)
            Return True
        Catch ex As Exception
            'Return "ERROr Y: " & ex.Message
            Return False
        End Try
    End Function


End Class


