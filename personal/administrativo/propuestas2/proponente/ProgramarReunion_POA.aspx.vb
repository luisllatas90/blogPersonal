﻿Partial Class administrativo_propuestas2_proponente_ProgramarReunion_POA
    Inherits System.Web.UI.Page

    Protected Sub btnCalendario_ini_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCalendario_ini.Click
        If Me.divCalendario_ini.Visible = False Then
            Me.divCalendario_ini.Visible = True

            'Dim fec_ini() As String = Me.txt_fecha.Text.Split("/")
            'Me.Calendario_ini.VisibleDate = New DateTime(fec_ini(2), fec_ini(1), fec_ini(0))
        Else
            Me.divCalendario_ini.Visible = False
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            Me.divCalendario_ini.Visible = False
            HD_Usuario.Value = Request.QueryString("idUsu")
            HD_id_rec.Value = Request.QueryString("id_rec")
            HD_Tipo.Value = Request.QueryString("tipo")
            'Response.Write("<script>alert('" & Request.QueryString("tipo") & "')</script>")

            txt_fecha.Text = DateTime.Now.ToString("dd/MM/yyyy")
            Dim ObjCnx As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
            Dim rsFac As New Data.DataTable
            rsFac = ObjCnx.TraerDataTable("PRP_ConsutarReunionesConsejo_POA", "FA", HD_Usuario.Value, "", "")
            If rsFac.Rows.Count > 0 Then
                HD_Facultad.Value = rsFac.Rows(0).Item("codigo_fac")
            Else
                HD_Facultad.Value = HD_Usuario.Value
            End If

            ''Llenar Combo Propuestas 
            Dim dttpropuesta As New Data.DataTable
            dttpropuesta = ObjCnx.TraerDataTable("PRP_ListarPropuestaSecretarios", HD_Facultad.Value)
            ClsFunciones.LlenarListas(Me.ddl_Propuesta, dttpropuesta, "codigo_prp", "nombre_prp")

            Call wf_limpiarGridView()

            If HD_Tipo.Value = "M" Then
                'Consultar a la Base de Datos y traer los registros
                Dim dtt As New Data.DataTable
                dtt = ObjCnx.TraerDataTable("PRP_BuscarProgramacionReuniones", HD_id_rec.Value, "")

                If dtt.Rows.Count > 0 Then
                    txt_fecha.Text = dtt.Rows(0).Item("fecha_Rec").ToString ''("dd/MM/yyyy")
                    txt_Agenda.Text = dtt.Rows(0).Item("agenda_Rec").ToString
                    txt_lugar.Text = dtt.Rows(0).Item("lugar_Rec").ToString

                    For i As Integer = 0 To dtt.Rows.Count - 1
                        Dim objPropuesta As New ClsPropuesta
                        objPropuesta.AgregarItemDetalle(dtt.Rows(i).Item("codigo_prp").ToString, dtt.Rows(i).Item("nombre_Prp").ToString)
                    Next
                    Call wf_CargarDetalle()
                End If
            End If
        End If
    End Sub

    Protected Sub Calendario_ini_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Calendario_ini.SelectionChanged
       Me.txt_fecha.Text = Me.Calendario_ini.SelectedDate.ToString("dd/MM/yyyy")
        Me.divCalendario_ini.Visible = False
    End Sub

    Protected Sub btn_Agregar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Agregar.Click
        Dim objPropuesta As New ClsPropuesta

        If ddl_Propuesta.SelectedIndex <> 0 Then
            objPropuesta.AgregarItemDetalle(ddl_Propuesta.SelectedValue, ddl_Propuesta.SelectedItem.ToString)
        End If
        Call wf_CargarDetalle()
    End Sub

    Public Sub wf_CargarDetalle()
        Dim objPropuesta As New ClsPropuesta
        dgvPropuesta.DataSource = objPropuesta.ConsultarDetalle()
        dgvPropuesta.DataBind()

        objPropuesta = Nothing
    End Sub

    Sub wf_limpiarGridView()
        Dim objPropuesta As New ClsPropuesta
        objPropuesta.wf_limpiarGridView()
    End Sub

    Protected Sub dgvPropuesta_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles dgvPropuesta.RowDeleting
        Dim objPropuesta As New ClsPropuesta
        objPropuesta.wf_EliminarItem(e.RowIndex)

        Call wf_CargarDetalle()
    End Sub

    Private Function validaForm() As Boolean
        If (Me.txt_Agenda.Text.Trim = "") Then
            Response.Write("<script>alert('Debe ingresar la Agenda a Tratar')</script>")
            Me.txt_Agenda.Focus()
            Return False
        End If


        If (Me.txt_lugar.Text.Trim = "") Then
            Response.Write("<script>alert('Debe describir el lugar')</script>")
            Me.txt_lugar.Focus()
            Return False
        End If

        If dgvPropuesta.Rows.Count <= 0 Then
            If ddl_Propuesta.SelectedItem.ToString.Trim = "<<SELECCIONE>>" Then
                Response.Write("<script>alert('Debe Seleccionar una Propuesta')</script>")
                Return False
            End If
        End If

        Return True
    End Function

    Protected Sub btnRegistrar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRegistrar.Click
        Try
            If (validaForm() = True) Then
                Dim codigo_prp As Integer = 0
                Dim dtt As New Data.DataTable
                Dim ObjCnx As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)

                If HD_Tipo.Value = "N" Then
                    dtt = ObjCnx.TraerDataTable("PRP_InsertaReunionConsejo", 0, txt_Agenda.Text, txt_fecha.Text, txt_lugar.Text, "O", "P", HD_Usuario.Value)
                    ''Insertar Detalle de Propuestas
                    For i As Integer = 0 To dgvPropuesta.Rows.Count - 1
                        codigo_prp = dgvPropuesta.DataKeys.Item(i).Values(0).ToString
                        ObjCnx.Ejecutar("PRP_ReunionConsejoPropuesta ", 0, dtt.Rows(0).Item("Codigo").ToString, codigo_prp, "A", i)
                    Next

                    Call wf_EnviarMails(dtt.Rows(0).Item("Codigo").ToString)
                Else
                    dtt = ObjCnx.TraerDataTable("PRP_InsertaReunionConsejo", HD_id_rec.Value, txt_Agenda.Text, txt_fecha.Text, txt_lugar.Text, "O", "P", HD_Usuario.Value)
                    ObjCnx.Ejecutar("PRP_EliminaReunionConsejoPropuesta", HD_id_rec.Value)

                    For i As Integer = 0 To dgvPropuesta.Rows.Count - 1
                        codigo_prp = dgvPropuesta.DataKeys.Item(i).Values(0).ToString
                        ObjCnx.Ejecutar("PRP_ReunionConsejoPropuesta ", 0, dtt.Rows(0).Item("Codigo").ToString, codigo_prp, "A", i)
                    Next

                    Call wf_EnviarMails(HD_id_rec.Value)
                End If

                Response.Redirect("ListaProgramarReunion_POA.aspx?id=" & HD_Usuario.Value)

            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

  
    Protected Sub btnCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        Response.Redirect("ListaProgramarReunion_POA.aspx?id=" & HD_Usuario.Value)
    End Sub

    Sub wf_EnviarMails(ByVal id_rec As Integer)
        Dim ObjCnx As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)

        Dim para As String = ""
        Dim Mensaje As String = ""
        Dim strRuta As String = ""
        Dim copia As String = "moises.vilchez@usat.edu.pe"

        Dim nombres As String = ""
        Dim agenda As String = ""
        Dim fecha As String = ""
        Dim lugar As String = ""
        Dim propuesta As String = ""

        Try
            Dim dtt As New Data.DataTable
            dtt = ObjCnx.TraerDataTable("PRP_EnviarMails", id_rec)
            agenda = dtt.Rows(0).Item("agenda_rec").ToString
            fecha = dtt.Rows(0).Item("fecha_rec").ToString
            lugar = dtt.Rows(0).Item("lugar_rec").ToString
            'PRP_EnviarMails 67
            For i As Integer = 0 To dtt.Rows.Count - 1
                para = dtt.Rows(i).Item("correo").ToString
                nombres = dtt.Rows(i).Item("nombres").ToString
                propuesta = dtt.Rows(i).Item("propuesta").ToString

                Mensaje = "<H1 style='font-family: Calibri; font-size: 16px; font-weight: bold;'>PROGRAMACIÓN DE REUNIONES DE PROPUESTAS: </H1>"
                Mensaje &= "<H4 style='font-family: Calibri; font-size: 12px; font-weight: bold;'>SEÑOR: " & nombres & " </H4>"
                Mensaje &= "<H4 style='font-family: Calibri; font-size: 12px; font-weight: bold;'>FECHA: " & fecha & " </H4>"
                Mensaje &= "<H4 style='font-family: Calibri; font-size: 12px; font-weight: bold;'>AGENDA: " & agenda & " </H4>"
                Mensaje &= "<H4 style='font-family: Calibri; font-size: 12px; font-weight: bold;'>LUGAR: " & lugar & " </H4>"
                Mensaje &= "<table border='1' cellspacing='0' cellpadding='4' style='font-family: Calibri; font-size: 13px; font-weight: bold;'>"
                Mensaje &= "<tr style='color: white; background-color: rgb(56, 113, 176);' ><th>LISTADO DE PROPUESTAS</th></tr>"
                Mensaje &= "<tr><td> * " + Replace(propuesta, "~", "</td></tr><tr><td> * ")
                Mensaje &= "</td></tr>"
                Mensaje &= "</table><br>' "
                'Mensaje &= "<h5 style='font-family: Calibri; font-size: 12px; font-weight: bold;'>(*)Debe acceder al Campus, módulo de Propuestas para realizar la revisión.</h5>"

                EnviarMensaje(para, "Reunion de propuestas", Mensaje, copia, strRuta, "", "")
            Next

        Catch ex As Exception
            '        Me.lblMensajeFormulario.Text = ex.Message & " " & ex.StackTrace.ToString
            '        'Me.lblMensajeFormulario.Text = "Correos Enviados: " & correosEnviados.ToString & "  de  " & nro & " destinatario(s) seleccionado(s). Usuarios sin correo registrado: " & sincorreo
        End Try
    End Sub

    Function EnviarMensaje(ByVal para As String, ByVal asunto As String, ByVal mensaje As String, ByVal copia As String, ByVal rutaarchivo As String, ByVal nombrearchivo As String, ByVal replyto As String) As Boolean
        Try
            Dim cls As New ClsEnvioMailAlumni
            If cls.EnviarMailAd("alumni@usat.edu.pe", "SISTEMA DE PROPUESTAS", para, asunto, mensaje, True, copia, replyto, rutaarchivo, nombrearchivo) Then
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

    'Sub EnviarMensajeNotificacion(ByVal cuentas As String, ByVal mensaje As String, ByVal asunto As String, ByVal adjunto As String, ByVal dt As Integer, ByVal de As Integer, ByVal dne As Integer, ByVal dsc As Integer, ByVal nombre_per As String)
    '    Dim obj As New ClsConectarDatos
    '    Dim dtEscuela As Data.DataTable
    '    obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
    '    obj.AbrirConexion()
    '    dtEscuela = obj.TraerDataTable("ALUMNI_ListarEscuelaCoordinador", Request.QueryString("ID"))
    '    obj.CerrarConexion()

    '    Dim ls_escuelaAll As String = ""
    '    For i As Integer = 0 To dtEscuela.Rows.Count - 1
    '        If ls_escuelaAll = "" Then
    '            ls_escuelaAll = dtEscuela.Rows(i).Item("escuela")
    '        Else
    '            ls_escuelaAll = ls_escuelaAll + ", " + dtEscuela.Rows(i).Item("escuela")
    '        End If
    '    Next

    '    Dim xmensaje As String = ""
    '    xmensaje &= "<font face='Trebuchet MS'>"
    '    xmensaje &= "<b>Notificación de Envío de Correo</b><hr /><br />"
    '    xmensaje &= "<b>Fecha: </b>" & Now.Date & "<br />"
    '    xmensaje &= "<b>Asunto: </b>" & asunto & "<br />"
    '    xmensaje &= "<b>Mensaje: </b>" & mensaje & "<br />"
    '    xmensaje &= "<b>Adjunto: </b>" & adjunto & "<br /><br />"
    '    xmensaje &= "<b>Total Destinatarios: </b>" & dt.ToString & "<br />"
    '    xmensaje &= "<b>Destinatarios sin correo registrado: </b>" & dsc.ToString & "<br />"
    '    xmensaje &= "<b>Mensajes Enviados: </b>" & de.ToString & "<br />"
    '    xmensaje &= "<b>Mensajes Fallidos: </b>" & dne.ToString & "<br />"
    '    xmensaje &= "<b>Mail enviado por: </b>" & nombre_per & "<br />"
    '    xmensaje &= "<b>Escuela: </b>" & ls_escuelaAll & "<br />"

    '    If cuentas <> "" Then
    '        xmensaje &= "<br /><b>Detalle de correos fallidos: </b>"
    '        xmensaje &= "<br /><br /><table border=""1"" style=""border:1px solid black;""><tr><th>Nombres Apellidos</th><th>Correo Personal</th><th>Correo Profesional</th></tr>"
    '        xmensaje &= cuentas
    '        xmensaje &= "</table>"
    '    End If
    '    xmensaje &= "<br /><hr /><br />"
    '    xmensaje &= "<b>CampusVirtual USAT</b>"
    '    xmensaje &= "</font>"
    '    Dim cls As New ClsEnvioMailAlumni
    '    cls.EnviarMailAd("alumni@usat.edu.pe", "alumniUSAT", "jfarias@usat.edu.pe", "Módulo de Alumni USAT - Notificación de Envío de Correo", xmensaje, True, "")

    '    'lbl_msgbox.Text = xmensaje

    '    xmensaje = ""
    '    cls = Nothing
    'End Sub
    'Function firmaMensaje(ByVal nombre As String) As String

    '    Dim obj As New ClsConectarDatos
    '    Dim dtEscuela As Data.DataTable
    '    obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
    '    obj.AbrirConexion()
    '    dtEscuela = obj.TraerDataTable("ALUMNI_ListarEscuelaCoordinador", Request.QueryString("ID"))
    '    obj.CerrarConexion()

    '    Dim ls_escuelaAll As String = ""
    '    For i As Integer = 0 To dtEscuela.Rows.Count - 1
    '        If ls_escuelaAll = "" Then
    '            ls_escuelaAll = dtEscuela.Rows(i).Item("escuela")
    '        Else
    '            ls_escuelaAll = ls_escuelaAll + ", " + dtEscuela.Rows(i).Item("escuela")
    '        End If
    '    Next

    '    Dim dtCelular As Data.DataTable
    '    obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
    '    obj.AbrirConexion()
    '    dtCelular = obj.TraerDataTable("ALUMNI_ListarPersonalCelular", Request.QueryString("ID"))
    '    obj.CerrarConexion()
    '    Dim ls_Celular As String = ""

    '    If dtCelular.Rows.Count = 0 Then
    '        ls_Celular = ""
    '    End If

    '    ls_Celular = dtCelular.Rows(0).Item("celular_Per")

    '    Dim ls_direccion As String = "Av. San Josemaría Escrivá N°855. Chiclayo - Perú"
    '    Dim ls_telefono As String = "T: (074) 606200. Anexo: 1239 - C: " & ls_Celular
    '    Dim ls_pagWeb As String = "www.usat.edu.pe / http://www.facebook.com/usat.peru"


    '    Dim firma As String
    '    firma = "<br /><br />---------------------------------------<br />"
    '    firma &= nombre & "<br />"
    '    'firma &= "Dirección de Alumni - USAT " & "<br />"

    '    Select Case CInt(Request.QueryString("ctf"))
    '        Case 1
    '            firma &= "Administrador del Sistena  - " & ls_escuelaAll & "<br />"
    '        Case 90
    '            firma &= "Dirección de Alumni - " & ls_escuelaAll & "<br />"

    '        Case 145
    '            firma &= "Coordinación de Alumni - " & ls_escuelaAll & "<br />"
    '    End Select

    '    firma &= ls_direccion & "<br />"
    '    firma &= ls_telefono & "<br />"
    '    firma &= ls_pagWeb & "<br />"

    '    firma &= "<div>Síguenos en :</div>"
    '    'lbl_msgbox.Text = firma
    '    firma &= LogoFb()
    '    Return firma
    'End Function

    'Function LogoFb() As String
    '    Dim Logo As String
    '    Logo = "<a href='https://www.facebook.com/usatalumni' style='" & style() & " ' ><b>f</b></a>"
    '    'Logo = "<a href='https://www.facebook.com/usatalumni'><img src='https://intranet.usat.edu.pe/autoevaluacion/dyandroid/fb.png'></a>"
    '    Return Logo
    'End Function

    'Function style() As String
    '    Dim stylestr As String
    '    stylestr = " background-color: #5B74A8;  border-color: #29447E #29447E #1A356E;    border-image: none;    border-style: solid;    border-width: 1px;    box-shadow: 0 1px 0 rgba(0, 0, 0, 0.1), 0 1px 0 #8A9CC2 inset;    color: #FFFFFF;    cursor: pointer;    display: inline-block;    font: bold 20px verdana,arial,sans-serif;    margin: 0;    overflow: visible;    padding: 0.1em 0.5em 0.1em;    position: relative;    text-align: center;    text-decoration: none;white-space: nowrap;z-index: 1;"
    '    'stylestr = "width: 32px; height: 32px;	background-repeat: no-repeat;	background-position: center center;	text-indent: -900em;	text-decoration: none;	line-height: 100%;	white-space: nowrap;	display: inline-block;	position: relative;	vertical-align: middle;	margin: 0 2px 5px 0;	/* default button color */	background-color: #ececec;	border: solid 1px #b8b8b9;	/* default box shadow */	-webkit-box-shadow: inset 0 1px 0 rgba(255,255,255,.3), 0 1px 0 rgba(0,0,0,.1);	-moz-box-shadow: inset 0 1px 0 rgba(255,255,255,.3), 0 1px 0 rgba(0,0,0,.1);	box-shadow: inset 0 1px 0 rgba(255,255,255,.3), 0 1px 0 rgba(0,0,0,.1);	/* default border radius */	-webkit-border-radius: 5px;	-moz-border-radius: 5px;	border-radius: 5px;background-color: #4d7de1;	border-color: #294c89;	color: #fff; background-image: url('https://intranet.usat.edu.pe/autoevaluacion/dyandroid/white_facebook.png');"
    '    Return stylestr
    'End Function
End Class
