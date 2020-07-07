﻿Imports System.Collections.Generic

Partial Class GradosYTitulos_FrmNotificarEntregaDiplomas
    Inherits System.Web.UI.Page
    Dim contador As Integer = 0

    Private Sub ListarSesionesConsejo()
        Dim dt As New Data.DataTable
        Dim obj As New ClsGradosyTitulos
        dt = obj.ListaSesionConsejoU("L", "%")

        For i As Integer = 0 To dt.Rows.Count - 1
            Me.cboSesion.Items.Add(New ListItem(dt.Rows(i).Item("descripcion_scu").ToString, dt.Rows(i).Item("codigo_scu")))
        Next
        Me.cboSesion.DataBind()
    End Sub

    Private Sub ListarTipoDenominacion()
        Dim dt As New Data.DataTable
        Dim obj As New ClsGradosyTitulos
        dt = obj.ConsultarTipoDenominacion("GYT", "")

        For i As Integer = 0 To dt.Rows.Count - 1
            Me.cboTipoDenominacion.Items.Add(New ListItem(dt.Rows(i).Item("nombre").ToString, dt.Rows(i).Item("codigo")))
        Next
        Me.cboTipoDenominacion.DataBind()
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (Session("id_per") Is Nothing) Then
            Response.Redirect("../../sinacceso.html")
        End If
        If IsPostBack = False Then
            ListarSesionesConsejo()
            ListarTipoDenominacion()
        End If
    End Sub

    Function Validar() As Boolean
        If Me.cboSesion.SelectedValue = "" And Me.cboTipoDenominacion.SelectedValue = "" Then
            If Me.txtBusqueda.Text.Length < 3 Then
                Me.divMensaje.InnerHtml = "Debe Ingresar al menos 3 Caracteres para Buscar Sin Filtros."
                Me.divMensaje.Attributes.Add("Class", "alert alert-danger")
                ScriptManager.GetCurrent(Me.Page).SetFocus(Me.txtBusqueda)
                Me.gvAlumnos.DataSource = Nothing
                Me.gvAlumnos.DataBind()
                Return False
            End If
        End If
        Return True
    End Function


    Protected Sub btnBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBuscar.Click
        If (Session("id_per") Is Nothing) Then
            Response.Redirect("../../sinacceso.html")
        End If
        contador = 0
        Me.lblcontador.Text = ""
        If Validar() = True Then
            Me.divMensaje.InnerHtml = ""
            Me.divMensaje.Attributes.Remove("Class")

            Dim obj As New ClsGradosyTitulos
            Dim dt As New Data.DataTable
            dt = obj.ConsultarEntregaDiplomas(Me.cboSesion.SelectedValue, Me.cboTipoDenominacion.SelectedValue, Me.txtBusqueda.Text)
            Me.gvAlumnos.DataSource = dt
            Me.gvAlumnos.DataBind()

            Me.lblcontador.Text = " Filas seleccionadas de " + Me.gvAlumnos.Rows.Count.ToString + ""

            For Each row As GridViewRow In Me.gvAlumnos.Rows
                Dim chckrw As CheckBox = CType(row.FindControl("chkAutorizar"), CheckBox)
                If chckrw.Checked Then
                    contador += 1
                    row.ControlStyle.BackColor = Drawing.Color.AntiqueWhite
                    row.ControlStyle.Font.Bold = True
                Else
                    row.ControlStyle.BackColor = Drawing.Color.White
                    row.ControlStyle.Font.Bold = False
                End If
            Next
            Me.lblContadorSeleccionado.Text = contador.ToString
        End If

    End Sub

    Protected Sub chckchanged(ByVal sender As Object, ByVal e As EventArgs)
        Dim chckheader As CheckBox = CType(gvAlumnos.HeaderRow.FindControl("chkall"), CheckBox)
        contador = 0
        For Each row As GridViewRow In gvAlumnos.Rows
            Dim chckrw As CheckBox = CType(row.FindControl("chkAutorizar"), CheckBox)
            If chckheader.Checked = True Then
                chckrw.Checked = True
            Else
                chckrw.Checked = False
            End If
            If chckrw.Checked = True Then
                contador = contador + 1
                row.ControlStyle.BackColor = Drawing.Color.AntiqueWhite
                row.ControlStyle.Font.Bold = True
            Else
                row.ControlStyle.BackColor = Drawing.Color.White
                row.ControlStyle.Font.Bold = False
            End If
        Next
        Me.lblContadorSeleccionado.Text = contador.ToString
    End Sub

    Public Sub lnkContar_Click(ByVal sender As Object, ByVal e As EventArgs)

        For Each row As GridViewRow In Me.gvAlumnos.Rows
            Dim chckrw As CheckBox = CType(row.FindControl("chkAutorizar"), CheckBox)
            If chckrw.Checked Then
                contador += 1
                row.ControlStyle.BackColor = Drawing.Color.AntiqueWhite
                row.ControlStyle.Font.Bold = True
            Else
                row.ControlStyle.BackColor = Drawing.Color.White
                row.ControlStyle.Font.Bold = False
            End If

        Next
        Me.lblContadorSeleccionado.Text = contador.ToString
    End Sub


    Public Sub btnEnviarCorreo_Click(ByVal sender As Object, ByVal e As EventArgs)
        If (Session("id_per") Is Nothing) Then
            Response.Redirect("../../sinacceso.html")
        End If
        EnviarEmail()
    End Sub

    Sub EnviarEmail()
        Me.divMensaje.InnerHtml = ""
        Me.divMensaje.Attributes.Remove("Class")

        Dim JSONresult As String = ""

        Dim obj As New ClsConectarDatos
        Dim tb As New Data.DataTable

        Dim objemail As New ClsMail
        'obj.IniciarTransaccion()
        Try
            Dim receptor, AsuntoCorreo As String
            Dim mensaje As String = ""
            Dim descripcion As String = ""

            receptor = ""

            Dim dt As New Data.DataTable
            dt.Columns.Add("denominacion")
            dt.Columns.Add("correos")
            dt.Columns.Add("codigos")

            For Each row As GridViewRow In Me.gvAlumnos.Rows
                Dim chckrw As CheckBox = CType(row.FindControl("chkAutorizar"), CheckBox)
                If chckrw.Checked Then
                    If dt.Rows.Count = 0 Then
                        dt.Rows.Add(Me.gvAlumnos.DataKeys(row.RowIndex).Values("descripcion_tdg").ToString, "", "")
                    Else
                        Dim contador As Integer = 0
                        For i As Integer = 0 To dt.Rows.Count - 1
                            If dt.Rows(i).Item("denominacion").ToString = Me.gvAlumnos.DataKeys(row.RowIndex).Values("descripcion_tdg").ToString Then
                                contador = contador + 1
                            End If
                        Next
                        If contador = 0 Then
                            dt.Rows.Add(Me.gvAlumnos.DataKeys(row.RowIndex).Values("descripcion_tdg").ToString, "", "")
                        End If
                    End If
                    'receptor += Me.gvAlumnos.DataKeys(row.RowIndex).Values("email_alu").ToString + ","
                End If
            Next

            For Each row As GridViewRow In Me.gvAlumnos.Rows
                Dim chckrw As CheckBox = CType(row.FindControl("chkAutorizar"), CheckBox)
                If chckrw.Checked Then
                    For j As Integer = 0 To dt.Rows.Count - 1
                        If Me.gvAlumnos.DataKeys(row.RowIndex).Values("descripcion_tdg").ToString = dt.Rows(j).Item("denominacion") Then
                            If dt.Rows(j).Item("correos") = "" Then
                                dt.Rows(j).Item("correos") = Me.gvAlumnos.DataKeys(row.RowIndex).Values("email_alu").ToString
                            Else
                                dt.Rows(j).Item("correos") += "," + Me.gvAlumnos.DataKeys(row.RowIndex).Values("email_alu").ToString
                            End If
                            If dt.Rows(j).Item("codigos") = "" Then
                                dt.Rows(j).Item("codigos") = Me.gvAlumnos.DataKeys(row.RowIndex).Values("codigo_egr").ToString
                            Else
                                dt.Rows(j).Item("codigos") += "," + Me.gvAlumnos.DataKeys(row.RowIndex).Values("codigo_egr").ToString
                            End If
                        End If
                    Next
                End If
            Next

            Dim dtr As New Data.DataTable
            Dim objGyt As New ClsGradosyTitulos

            Dim lblresultado As Boolean
            Dim codigo_dta As Integer = 0
            Dim codigo_dft As Integer = 0
            For Each row As GridViewRow In Me.gvAlumnos.Rows
                Dim chckrw As CheckBox = CType(row.FindControl("chkAutorizar"), CheckBox)
                If chckrw.Checked Then
                    If Me.gvAlumnos.DataKeys(row.RowIndex).Values("enviado").ToString = "NO" Then
                        codigo_dta = Me.gvAlumnos.DataKeys(row.RowIndex).Values("codigo_dta")
                        codigo_dft = Me.gvAlumnos.DataKeys(row.RowIndex).Values("codigo_dft")
                        lblresultado = objGyt.ActualizarEstadosTramite("B", codigo_dta, 0, codigo_dft, 1, "F", Session("id_per"))
                    End If
                End If
            Next


            If dt.Rows.Count > 0 Then

                For k As Integer = 0 To dt.Rows.Count - 1
                    'Response.Write(dt.Rows(k).Item("denominacion") + " - " + dt.Rows(k).Item("correos") + "<br/>")

                    Dim list As New List(Of Dictionary(Of String, Object))()

                    AsuntoCorreo = "Inscripción de diploma de " + dt.Rows(k).Item("denominacion").ToString

                    descripcion = ""

                    descripcion += "Estimado(a) Egresado(a): <br><br> Se les invita a recoger su " + dt.Rows(k).Item("denominacion").ToString + " en la Oficina de Grados y Títulos (2do. Piso del edificio de Gobierno), "
                    descripcion += "<b>portando su DNI original </b>(Documento Nacional de Identidad) <b>requisito indispensable</b>. El horario de oficina es de lunes a viernes:<br><br>"
                    'descripcion += "Esta información podrá encontrarla ingresando a: <a href='https://www.sunedu.gob.pe/registro-nacional-de-grados-y-titulos/'>https://www.sunedu.gob.pe/registro-nacional-de-grados-y-titulos/</a>  en el icono <b>Verifica si estás inscrito en el Registro Nacional de Grados Títulos</b><br><br>"
                    'descripcion += "Ante cualquier inquietud estamos para atenderlos.<br><br>"

                    'descripcion += "Atentamente"


                    receptor = dt.Rows(k).Item("correos")
                    receptor = "hcano@usat.edu.pe"

                    mensaje = ""

                    mensaje = mensaje + "<html><head><meta http-equiv='Content-Type' content='text/html; charset=iso-8859-1' />"
                    mensaje = mensaje + "<title>Evaluación de Proyecto</title>"
                    mensaje = mensaje + "<style type='text/css'>.usat { font-family:Humnst777 Lt BT;font-size:14px;} "
                    mensaje = mensaje + ".bolsa{color:#F1132A;font-family:Calibri;font-size: 13px;font-weight: 500;}</style></head>"
                    mensaje = mensaje + "<body>"
                    mensaje = mensaje + "<div style='text-align:left;width:100%'>"
                    mensaje = mensaje + "<table border='0' width='90%' cellpadding='0' cellspacing='0'><tr><td>"
                    mensaje = mensaje + "<div style='width:70%;margin:0 auto;text-align:left;'><div class='usat'>" + descripcion + "</div></div>"
                    mensaje = mensaje + "</td></tr></tbody></table>"
                    mensaje = mensaje + "<table border='0' width='50%' cellpadding='0' cellspacing='0'><tr><td>"
                    mensaje = mensaje + "<div style='width:70%;margin:0 auto;text-align:center;'>"
                    mensaje = mensaje + "<div class='usat'>Mañana                 8:30 am a 1:00 pm <br>"
                    mensaje = mensaje + "<br>Tarde                  3:00 pm a 4:00 pm</div></div>"
                    mensaje = mensaje + "</td></tr><tr><td><b><div style='width:70%;margin:0 auto;text-align:left;'><div class='usat'></br>*De Enero a Marzo solo turno mañana</b></div></div></td></tr></tbody></table></br>"
                    mensaje = mensaje + "<table border='0' width='970%' cellpadding='0' cellspacing='0'><tr><td>"
                    mensaje = mensaje + "<div style='width:100%;margin-top:10px;text-align:center;color:white'>"
                    mensaje = mensaje + "</div></td></tr></table>"
                    mensaje = mensaje + "<table border='0' width='100%' cellpadding='0' cellspacing='0'><tr><td colspan='2'>"
                    mensaje = mensaje + "<div style='width:70%;margin:0 auto;text-align:left;'>"
                    mensaje = mensaje + "<div class='usat'><b>A TENER EN CUENTA: </b><br><br>"
                    mensaje = mensaje + "•	Indicar en portería que se acercarán a esta oficina a recabar su diploma, por tanto necesitarán su DNI para presentarlo en la Oficina de Grados y Títulos.</br></br>"
                    mensaje = mensaje + "•	La entrega del diploma es personal.</br></td></tr><tr><td width='3%'>&nbsp;</td><td width='97%'><div style='width:70%;margin:0 auto;text-align:left;'>"
                    mensaje = mensaje + "<div style='padding-left:5px;font-family:Humnst777 Lt BT;font-size:14px;'><b>*SOLO para las personas que NO LES SEA POSIBLE recoger su diploma personalmente, <u>descargar el formato de CARTA PODER en el campus</u></b> (Operaciones en línea>>Trámites Virtuales>>Requisitos para trámites) para ser llenada y firmada por su persona, también debe firmarse por el notario del lugar donde actualmente residen (Si reside en el exterior, el documento adjunto lo debe hacer visar por el Consulado del país donde Ud. actualmente reside). La persona que Ud. designe para recoger su diploma deberá presentarse en esta oficina portando <b>la carta original y DNI para la identificación respectiva.</b></div></div></br></td></tr>"
                    mensaje = mensaje + "<tr><td colspan='2'>"
                    mensaje = mensaje + "<div style='width:70%;margin:0 auto;text-align:left;'>"
                    mensaje = mensaje + "<div class='usat'>"
                    mensaje = mensaje + "•	Para visualizar la inscripción de Diploma en la página de SUNEDU, el tiempo establecido es de 55 días hábiles a partir del día siguiente de haberse efectuado la Sesión del Consejo Universitario. (Incluye los días que SUNEDU toma para subir la información).</br></br>"
                    mensaje = mensaje + "Gracias</br>"
                    mensaje = mensaje + "</div></div>"
                    mensaje = mensaje + "</td></tr></tbody></table>"

                    mensaje = mensaje + "<table border='0' width='970%' cellpadding='0' cellspacing='0'><tr><td>"
                    mensaje = mensaje + "<div style='width:100%;margin-top:10px;text-align:center;color:white'>"
                    mensaje = mensaje + "</div></td></tr></table>"
                    mensaje = mensaje + "<table border='0' width='90%' cellpadding='0' cellspacing='0'>"
                    mensaje = mensaje + "<tr><td style='background:none;border-bottom:1px solid #F1132A;height:1px;width:50%;margin:0px 0px 0px 0px' > &nbsp;</td></tr></table><br />"
                    mensaje = mensaje + "<table border='0' width='90%' cellpadding='0' cellspacing='0'><tr><td>"
                    mensaje = mensaje + "<div style='width:70%;margin:0 auto;text-align:center;'><img src='//intranet.usat.edu.pe/campusestudiante/assets/images/logousat.png' width='100' height='100' ></div>"
                    mensaje = mensaje + "<div style='width:70%;margin:0 auto;text-align:center;'></td></tr></table>"
                    mensaje = mensaje + "<table border='0' width='90%' cellpadding='0' cellspacing='0'><tr><td>"
                    mensaje = mensaje + "<div style='margin:0 auto;text-align:center;color:gray;font-family:Calibri '><b>Oficina de Grados y Titulos</b></div><br /></td></tr></table>"
                    mensaje = mensaje + "<table border='0' width='90%' cellpadding='0' cellspacing='0'><tr><td>"
                    mensaje = mensaje + "<div style='text-align:center;font-size:11px;color:gray;font-family:Calibri '><div>Av. San Josemaría Escrivá de Balaguer Nº 855 Chiclayo - Perú | Teléfono: 606200 - anexo: 1013"
                    mensaje = mensaje + "<a href='mailto:pdiaz@usat.edu.pe' style='color:gray;text-decoration:none;' target='_blank'><br/><b>pdiaz@usat.edu.pe</b></a></div> "
                    mensaje = mensaje + "<div style='font-family:Calibri'>© Copyright " + Year(Now()).ToString + ": USAT - Todos los derechos reservados</div>"
                    mensaje = mensaje + "</td></tr></table></div></body></html>"

                    objemail.EnviarMailCCO("campusvirtual@usat.edu.pe", "Inscripción de diploma de Grado Académico de Bachiller", "hcano@usat.edu.pe", AsuntoCorreo, mensaje, True, receptor, "pdiaz@usat.edu.pe")

                    dtr = objGyt.ActualizarEstadoEnvioEntregaDiploma(dt.Rows(k).Item("codigos").ToString)
                Next

                Dim dtc As New Data.DataTable
                dtc = objGyt.ConsultarEntregaDiplomas(Me.cboSesion.SelectedValue, Me.cboTipoDenominacion.SelectedValue, Me.txtBusqueda.Text)
                Me.gvAlumnos.DataSource = dtc
                Me.gvAlumnos.DataBind()

                Me.divMensaje.InnerHtml = "Envío de correo(s) realizado correctamente."
                Me.divMensaje.Attributes.Add("Class", "alert alert-success")
            Else
                Me.divMensaje.InnerHtml = "Debe Seleccionar al menos una Fila."
                Me.divMensaje.Attributes.Add("Class", "alert alert-danger")
            End If
            'obj.TerminarTransaccion()
        Catch ex As Exception
            'obj.AbortarTransaccion()
            Me.divMensaje.InnerHtml = "No se pudo realizar la operación." + ex.Message.ToString
            Me.divMensaje.Attributes.Add("Class", "alert alert-danger")
        End Try
    End Sub

    Protected Sub gvAlumnos_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvAlumnos.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim email As String = DataBinder.Eval(e.Row.DataItem, "email_alu").ToString()
            'e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFC0CB")
            If email = "" Or validarEmail(email) = False Then
                e.Row.Cells(0).ForeColor = Drawing.Color.Red
                e.Row.Cells(1).ForeColor = Drawing.Color.Red
                e.Row.Cells(2).ForeColor = Drawing.Color.Red
                e.Row.Cells(3).ForeColor = Drawing.Color.Red
                e.Row.Cells(4).ForeColor = Drawing.Color.Red
                e.Row.Cells(5).ForeColor = Drawing.Color.Red
                e.Row.Font.Bold = True
            End If
        End If
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

End Class
