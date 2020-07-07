﻿
Imports System.Collections.Generic
Imports System.Net
Imports System.IO
Imports System.Xml
Imports System.Security.Cryptography
Partial Class frmInscripcionAlmuerzo
    Inherits System.Web.UI.Page
    Dim UrlPeticion As String = ConfigurationManager.AppSettings("RutaCampusLocal") & "Congreso/CongresoUsat.asmx"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If IsPostBack = False Then
                Me.hdtoken.Value = "QMIPVU9A" 'AQ3GA56B -> PARA PRUEBAS: CEREMONIA DE GRADUACIÓN BACHILLERES 2018
                Me.hdevento.Value = "0" 'No se utiliza en el procedimiento, pero igual se envía

                InfoCentroCostos()
                ListaUniversidades()
                'ListaTipoParticipante()

                Dim tipoParticipante As String

                If Not Request.QueryString("p") Is Nothing Then


                    Select Case Desencriptar(Request.QueryString("p"))

                        Case "E"
                            tipoParticipante = "1"
                        Case "G"
                            tipoParticipante = "3"
                        Case ""
                            tipoParticipante = ""

                    End Select
                Else
                    tipoParticipante = "6"
                End If

                'Response.Write(Encriptar("G"))
                ListaTipoParticipante2(tipoParticipante)


            End If
        Catch ex As Exception
            Response.Write(ex.Message())
        End Try

    End Sub

    ' Deshabilitar Catpcha para Pruebas en Dev o Agregar Serverdev a Dominios en Google Catpcha
    Public Function ValidarCaptcha(ByVal captcha As String) As String
        Dim resultado As Boolean = False
        Dim RespuestaControl As String = captcha
        Dim ClaveServidor As String = "6LemTGAUAAAAABF8vxah0eThYUMpuSxTbRN-O7HO"
        Dim apiUrl As String = "https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}"
        Dim requestUri As String = String.Format(apiUrl, ClaveServidor, RespuestaControl)
        Dim request As HttpWebRequest = CType(WebRequest.Create(requestUri), HttpWebRequest)

        Dim respuesta As String
        Dim objRpta As New ClsRespuestaCaptcha
        Dim rpta As String = ""

        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()


        Using response As WebResponse = request.GetResponse()
            Using stream As StreamReader = New StreamReader(response.GetResponseStream())
                respuesta = stream.ReadToEnd()
                'rpta = respuesta("success")

                'Dim isSuccess As Boolean = jobject.Value(Of Boolean)("success")
                'resultado = If((isSuccess), True, False)
            End Using
        End Using

        objRpta = serializer.Deserialize(Of ClsRespuestaCaptcha)(respuesta)

        Return objRpta.success
    End Function

    Protected Sub btnInscribir_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnInscribir.Click

        Try

            'Dim captchaResponse As String = Request("g-recaptcha-response")
            'If (ValidarCaptcha(captchaResponse) = True) Then
            Dim list As New Dictionary(Of String, String)

            list.Add("token_cco", Me.hdtoken.Value)
            list.Add("codigo_eve", Me.hdevento.Value)
            list.Add("tipo_doc", Me.ddltipodoc.SelectedValue)
            list.Add("nro_doc", Me.txtnrodoc.Text)
            list.Add("apepat", Me.txtapepat.Text)
            list.Add("apemat", Me.txtapemat.Text)
            list.Add("nombre", Me.txtnombres.Text)
            list.Add("fecha_nac", Me.txtfechaNacimiento.Value)
            list.Add("sexo", Me.ddlsexo.SelectedValue)
            list.Add("email", Me.txtemail.Text)
            list.Add("universidad", IIf(Me.ddlUniversidad.Value = -1, Me.txtUniversidad.Text, Me.ddlUniversidad.Value))
            list.Add("tipo_participante", Me.ddltipoparticipante.Value)
            list.Add("telefono", Me.txttelefono.Text)
            list.Add("requiere_factura", IIf(Me.chkReqFactura.Checked, 1, 0))
            Dim obj As New ClsCongreso
            Dim envelope As String = obj.SoapEnvelope(list)
            'Response.Write(envelope)
            Dim result As String = obj.PeticionRequestSoap(UrlPeticion, envelope, "http://tempuri.org/Inscripcion", "serverdev\esaavedra") ' Session("perlogin").ToString
            result = obj.ResultFile(result)
            If result = -1 Then
                Me.mensaje.Attributes.Add("class", "alert alert-danger")
                Me.mensaje.InnerHtml() = "<p>No se pudo Inscribir.</p>"
            ElseIf result = -2 Then
                limpiar()
                Me.mensaje.Attributes.Remove("class")
                Me.mensaje.InnerHtml = ""
                Me.mensaje.Attributes.Add("class", "alert alert-danger")
                Me.mensaje.InnerHtml() = "<p>No se pudo inscribir, usted ya se encuentra inscrito.</p>"
            ElseIf result = -3 Then
                Me.mensaje.Attributes.Remove("class")
                Me.mensaje.InnerHtml = ""
                Me.mensaje.Attributes.Add("class", "alert alert-danger")
                Me.mensaje.InnerHtml() = "<p>Tipo de Participante seleccionado no corresponde.</p>"
            Else
                Dim universidad As String
                universidad = Me.ddlUniversidad.Items(Me.ddlUniversidad.SelectedIndex).Text

                enviarEmailInscripcion(Me.Descripcion.InnerText, Me.txtapepat.Text, Me.txtapemat.Text, Me.txtnombres.Text, Me.txtnrodoc.Text, universidad, Me.txtemail.Text, Me.txtfechaNacimiento.Value, Me.hdmonto.Value, Me.hdfechavenc.Value)

                Me.mensaje.Attributes.Remove("class")
                Me.mensaje.InnerHtml = ""
                Me.mensaje.Attributes.Add("class", "alert alert-success")
                If Me.chkReqFactura.Checked Then
                    Me.mensaje.InnerHtml() = "<p>Inscripción exitosa<br>Puedes pagar la inscripción de S/ " + Me.hdmonto.Value + " siguiendo los pasos especificados en el correo que se te acaba de enviar a: " + Me.txtemail.Text + "</p>"
                Else
                    Me.mensaje.InnerHtml() = "<p>Inscripción exitosa<br>Puedes pagar la inscripción de S/ " + Me.hdmonto.Value + " en los canales de atención de los bancos BCP o BBVA indicando tu número de documento de identidad registrado: " + Me.txtnrodoc.Text + "</p>"
                End If

                limpiar()

                Page.RegisterStartupScript("redireccionar1", "<script>setTimeout(function(){location.href ='http://www.usat.edu.pe';}, 3000);</script>")
            End If

            'Else
            '    Me.mensaje.Attributes.Add("class", "alert alert-danger")
            '    Me.mensaje.InnerHtml() = "<p>Error en Captcha.</p>"
            'End If


        Catch ex As Exception
            Me.mensaje.Attributes.Add("class", "alert alert-danger")
            Me.mensaje.InnerHtml() = "<p>No se pudo Inscribir.</p>" + ex.Message.ToString

        End Try
        Page.RegisterStartupScript("bloquea0", "<script type='text/javascript'>MascaraEspera('0');</script>")
    End Sub


    Sub limpiar()
        Me.ddltipodoc.SelectedValue = 0
        Me.txtnrodoc.Text = ""
        Me.txtapepat.Text = ""
        Me.txtapemat.Text = ""
        Me.txtnombres.Text = ""
        Me.txtfechaNacimiento.Value = ""
        Me.ddlsexo.SelectedValue = 0
        Me.txtemail.Text = ""
        Me.ddlUniversidad.Value = 0
        Me.ddlUniversidad.Value = -1
        Me.ddltipoparticipante.Value = -1
        Me.txttelefono.Text = ""
        Me.txtUniversidad.Text = ""
        Me.chkReqFactura.Checked = False
    End Sub

    Protected Sub ListaUniversidades()
        Try
            Dim obj As New ClsCongreso
            Dim envelope As String = obj.SoapEnvelopeVacio()
            Dim result As String

            result = obj.PeticionRequestSoap(UrlPeticion, envelope, "http://tempuri.org/Universidades", "serverdev\esaavedra") ' Session("perlogin").ToString
            Dim dt As New Data.DataTable
            dt = obj.ResultFileTabla(result, "")
            Me.ddlUniversidad.Items.Add("-- Seleccione --")
            Me.ddlUniversidad.Items(0).Value = -1
            For i As Integer = 1 To dt.Rows.Count
                Me.ddlUniversidad.Items.Add(dt.Rows(i - 1).Item("des").ToString)
                Me.ddlUniversidad.Items(i).Value = dt.Rows(i - 1).Item("cod").ToString
            Next


            'Response.Write(result)
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub ListaTipoParticipante()
        Try
            Dim list As New Dictionary(Of String, String)
            list.Add("token_cco", Me.hdtoken.Value.ToString)
            Dim obj As New ClsCongreso
            Dim envelope As String = obj.SoapEnvelopeTokenP(list)
            Dim result As String

            result = obj.PeticionRequestSoap(UrlPeticion, envelope, "http://tempuri.org/TipoParticipantes", "serverdev\esaavedra") ' Session("perlogin").ToString
            Dim dt As New Data.DataTable
            dt = obj.ResultFileTabla(result, "")
            Me.ddltipoparticipante.Items.Add("-- Seleccione --")
            Me.ddltipoparticipante.Items(0).Value = -1
            For i As Integer = 1 To dt.Rows.Count
                Me.ddltipoparticipante.Items.Add(dt.Rows(i - 1).Item("des").ToString)
                Me.ddltipoparticipante.Items(i).Value = dt.Rows(i - 1).Item("cod").ToString
            Next

            'Response.Write(result)
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub
    Protected Sub ListaTipoParticipante2(ByVal tp As String)
        Try
            Dim list As New Dictionary(Of String, String)
            list.Add("token_cco", Me.hdtoken.Value.ToString)
            Dim obj As New ClsCongreso
            Dim envelope As String = obj.SoapEnvelopeTokenP(list)
            Dim result As String

            result = obj.PeticionRequestSoap(UrlPeticion, envelope, "http://tempuri.org/TipoParticipantes", "serverdev\esaavedra") ' Session("perlogin").ToString
            Dim dt As New Data.DataTable
            dt = obj.ResultFileTabla(result, "")

            Me.ddltipoparticipante.Items.Add("-- Seleccione --")
            Me.ddltipoparticipante.Items(0).Value = 0

            Dim j As Integer = 1
            For i As Integer = 1 To dt.Rows.Count
                ' Response.Write(dt.Rows(i - 1).Item("cod").ToString & "--" & tp & "<br>")
                If dt.Rows(i - 1).Item("cod").ToString = tp Then
                    Me.ddltipoparticipante.Items.Add(dt.Rows(i - 1).Item("des").ToString)
                    Me.ddltipoparticipante.Items(j).Value = dt.Rows(i - 1).Item("cod").ToString
                    j = j + 1
                End If

            Next

            'Response.Write(result)
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub InfoCentroCostos()
        Dim m As String = ""
        Try
            Dim list As New Dictionary(Of String, String)
            list.Add("token_cco", Me.hdtoken.Value.ToString)
            Dim obj As New ClsCongreso
            Dim envelope As String = obj.SoapEnvelopeToken(list)
            'm= m & "01 "
            Dim result As String = obj.PeticionRequestSoap(UrlPeticion, envelope, "http://tempuri.org/VerCentroCosto", "serverdev\esaavedra") ' Session("perlogin").ToString
            'm=m & "02 "
            Dim dt As New Data.DataTable
            dt = obj.ResultFileTabla(result, "")
            'm=m & "03 " 
            'Response.Write("<script>alert('" + obj.ResultFileTabla(result, "") + "')</script>")
            'result = obj.ResultFileTabla(result, "")
            If dt.Rows.Count = 1 Then
                'm=m & "04 "
                Me.Descripcion.InnerText = dt.Rows(0).Item("des").ToString
                Me.Descripcion.Attributes.Add("style", "font-size:14px;font-weight:bold; color:Red")
            Else
                'm=m & "05 "
                'Response.Write("<script>alert('Evento No Existe'); location.href ='http://www.usat.edu.pe'; </script>")
            End If

            'Response.Write(result)
        Catch ex As Exception
            'Response.Write("<script>alert('Error al Cargar Datos'); location.href ='http://www.usat.edu.pe'; </script>")
        End Try
    End Sub

    Private Sub enviarEmailInscripcion(ByVal descripcion_cco As String, ByVal apepat As String, ByVal apemat As String, ByVal nombres As String, ByVal nrodoc As String, ByVal universidad As String, ByVal email As String, ByVal fechanacimiento As String, ByVal monto As String, ByVal fechavenc As String)

        Try
            apepat = apepat.ToUpper
            apemat = apemat.ToUpper
            nombres = nombres.ToUpper

            Dim obj As New ClsConectarDatos
            Dim objemail As New ClsMail
            Dim mensaje, receptor, AsuntoCorreo, eveCambiarCorreo As String

            eveCambiarCorreo = ""
            AsuntoCorreo = "[USAT]Inscripción a " + descripcion_cco

            mensaje = ""
            mensaje = mensaje + "<html><head><meta http-equiv='Content-Type' content='text/html; charset=iso-8859-1' />"
            mensaje = mensaje + "<title>Confirmac&íacute;on de Inscripción a " + descripcion_cco + "</title>"
            mensaje = mensaje + "<style type='text/css'>.usat { font-family:Calibri;color:#F1132A;font-size:25px;font-weight: bold;} "
            mensaje = mensaje + ".bolsa{color:#F1132A;font-family:Calibri;font-size: 13px;font-weight: 500;}</style></head>"
            mensaje = mensaje + "<body>"
            mensaje = mensaje + "<div style='text-align:center;width:100%'>"
            mensaje = mensaje + "<table border='0' width='100%' cellpadding='0' cellspacing='0'><tr><td>"
            mensaje = mensaje + "<div style='width:70%;margin:0 auto;text-align:center;'><img src='https://intranet.usat.edu.pe/campusestudiante/assets/images/logousat.png' width='100' height='100' ></div>"
            mensaje = mensaje + "<div style='width:70%;margin:0 auto;text-align:center;'><div class='usat'>" + descripcion_cco + "</div></div></td></tr></table>"
            mensaje = mensaje + "<table border='0' width='100%' cellpadding='0' cellspacing='0'><tr>"
            mensaje = mensaje + "<td style = 'background:none;border-bottom:1px solid #F1132A;height:1px;width:50%;margin:0px 0px 0px 0px' > &nbsp;</td></tr></table><br />"
            mensaje = mensaje + "<table border='0' width='100%' cellpadding='0' cellspacing='0'><tr><td>"
            mensaje = mensaje + "<div style='text-align:center;font-family:Calibri'><b> Estimado " + nombres + " </b></div>"
            mensaje = mensaje + "<div style='margin-top:10px;text-align:center;color:#675E5C;font-family:Calibri '><b>Se ha realizado el registro de inscripción a " + descripcion_cco + " con los siguientes datos:</b></div>"

            mensaje = mensaje + "<div style='margin-top:5px;text-align:left;color:#675E5C;font-family:Calibri'></div></td></tr></table>"
            mensaje = mensaje + "<table border='0' width='100%' cellpadding='0' cellspacing='0'><tr><td style='width:20%;'></td><td>"
            mensaje = mensaje + "<div style='width:100%;margin-top:10px;text-align:left;color:#675E5C;font-family:Calibri'>&nbsp;&nbsp;&nbsp;<b>- Nombres y Apellidos: </b>" + nombres + " " + apepat + " " + apemat + " </div>"
            mensaje = mensaje + "<div style='width:100%;margin-top:10px;text-align:left;color:#675E5C;font-family:Calibri'>&nbsp;&nbsp;&nbsp;<b>- Documento de Identidad: </b>" + nrodoc + " </div>"
            'mensaje = mensaje + "<div style='width:100%;margin-top:10px;text-align:left;color:#675E5C;font-family:Calibri'>&nbsp;&nbsp;&nbsp;<b>- Universidad: </b>" + universidad + " </div>"
            'mensaje = mensaje + "<div style='width:100%;margin-top:10px;text-align:left;color:#675E5C;font-family:Calibri'>&nbsp;&nbsp;&nbsp;- Teléfono: " + telefono + " </div>"
            mensaje = mensaje + "<div style='width:100%;margin-top:10px;text-align:left;color:#675E5C;font-family:Calibri'>&nbsp;&nbsp;&nbsp;<b>- E-mail: </b>" + email + " </div>"
            mensaje = mensaje + "<div style='width:100%;margin-top:10px;text-align:left;color:#675E5C;font-family:Calibri'>&nbsp;&nbsp;&nbsp;<b>- Fecha de Nacimiento: </b>" + fechanacimiento + " </div>"
            mensaje = mensaje + "</td></tr></table>"
            If Me.chkReqFactura.Checked Then
                mensaje = mensaje + "<table border='0' width='100%' cellpadding='0' cellspacing='0'><tr><td><div style='margin-top:10px;text-align:center;color:#675E5C;font-family:Calibri '><b>Puedes cancelar la Inscripción en el Banco BCP, depositando en la Cta. Cte. Soles Nº 305-1137448080 (CCI Nº 002-30500113744808011); el importe de S/" + monto + " <br>Enviar la constancia del depósito al correo tesoreria@usat.edu.pe; indicando los datos para la factura:</b></div></br>"
                mensaje = mensaje + "<div style='width:100%;margin-top:10px;text-align:left;color:#675E5C;font-family:Calibri'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b>- Razón social</b></div>"
                mensaje = mensaje + "<div style='width:100%;margin-top:10px;text-align:left;color:#675E5C;font-family:Calibri'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b>- Ruc</b></div>"
                mensaje = mensaje + "<div style='width:100%;margin-top:10px;text-align:left;color:#675E5C;font-family:Calibri'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b>- Dirección</b></div>"
                mensaje = mensaje + "<div style='width:100%;margin-top:10px;text-align:left;color:#675E5C;font-family:Calibri'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b>- Nombre del participante</b></div>"
                mensaje = mensaje + "<div style='width:100%;margin-top:10px;text-align:left;color:#675E5C;font-family:Calibri'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b>- Correo electrónico (al cual será remitido la factura electrónica)</b></div>"
            Else
                mensaje = mensaje + "<table border='0' width='100%' cellpadding='0' cellspacing='0'><tr><td><div style='margin-top:10px;text-align:center;color:#675E5C;font-family:Calibri '><b>Puedes pagar la Inscripción de S/" + monto + " en los canales de atención de los bancos BCP o BBVA indicando tu número de documento de identidad registrado: " + nrodoc + "</b></div></br>"
            End If
			if fechavenc is not null or fechavenc <>"" then
				mensaje = mensaje + "</td></tr></table>"
				mensaje = mensaje + "<div style='margin-top:10px;text-align:left;color:#675E5C;font-family:Calibri '>Tarifa válida hasta el " + fechavenc + " </div>"
				mensaje = mensaje + "</div></body></html>"
			end if
			
            'receptor = "yperez@usat.edu.pe"
            receptor = email
            objemail.EnviarMail("campusvirtual@usat.edu.pe", "Inscripcion", receptor, AsuntoCorreo, mensaje, True)

        Catch ex As Exception
            Response.Write(ex.Message().ToString)
        End Try

    End Sub

    'Protected Sub ddltipoparticipante_ServerChange(ByVal sender As Object, ByVal e As EventArgs) Handles ddltipoparticipante.ServerChange

    '    Try
    '        Dim item As String = Me.ddltipoparticipante.Value.ToString 'ddltipoparticipante.SelectedIndex.ToString 
    '        Dim token_cco As String = Me.hdtoken.Value.ToString



    '            Dim list As New Dictionary(Of String, String)
    '            list.Add("token_cco", token_cco)
    '            list.Add("codigo_tpar", item)
    '            Dim obj As New ClsCongreso
    '            Dim envelope As String = obj.SoapEnvelopeTokenC(list)
    '            Dim result As String

    '            result = obj.PeticionRequestSoap(UrlPeticion, envelope, "http://tempuri.org/ParametrosTipoParticipante", "serverdev\esaavedra") ' Session("perlogin").ToString
    '            Dim dt As New Data.DataTable
    '            dt = obj.ResultFileTabla(result, "")

    '            Dim usat As Boolean = dt.Rows(0).Item("val1")
    '            Dim monto As String = dt.Rows(0).Item("val2").ToString
    '            Dim etiqueta As String = dt.Rows(0).Item("val3").ToString
    '            Dim univ_nac As Boolean = dt.Rows(0).Item("val4")
    '            Dim fecha_venc As String = dt.Rows(0).Item("val5").ToString

    '            'Response.Write("<li>usat:" & dt.Rows(0).Item("val1") & "</li><br/><li>ambito:" & ambito & "</li><br/><li>etiqueta:" & dt.Rows(0).Item("val3") & "</li>")

    '            'If ambito.Trim = "NACIONAL" Then
    '            If univ_nac Then
    '                divSelec.Visible = True
    '                divText.Visible = False
    '            Else
    '                divSelec.Visible = False
    '                divText.Visible = True
    '            End If
    '            Me.lblUniversidad.Text = etiqueta.Trim + ":"
    '            If usat Then
    '                Me.ddlUniversidad.Value = "260000069"
    '                ddlUniversidad.Disabled = True
    '            Else
    '                Me.ddlUniversidad.Value = "-1"
    '                ddlUniversidad.Disabled = False
    '            End If
    '            Me.hdmonto.Value = monto
    '            Me.hdfechavenc.Value = fecha_venc


    '    Catch ex As Exception
    '        Response.Write(ex.Message())
    '    End Try
    'End Sub
    Protected Sub ddltipoparticipante_ServerChange(ByVal sender As Object, ByVal e As EventArgs) Handles ddltipoparticipante.ServerChange

        Try
            Dim item As String = Me.ddltipoparticipante.Value.ToString 'ddltipoparticipante.SelectedIndex.ToString 
            Dim token_cco As String = Me.hdtoken.Value.ToString


            If item > 0 Then
                Dim list As New Dictionary(Of String, String)
                list.Add("token_cco", token_cco)
                list.Add("codigo_tpar", item)
                Dim obj As New ClsCongreso
                Dim envelope As String = obj.SoapEnvelopeTokenC(list)
                Dim result As String

                result = obj.PeticionRequestSoap(UrlPeticion, envelope, "http://tempuri.org/ParametrosTipoParticipante", "serverdev\esaavedra") ' Session("perlogin").ToString
                Dim dt As New Data.DataTable
                dt = obj.ResultFileTabla(result, "")

                Dim usat As Boolean = dt.Rows(0).Item("val1")
                Dim monto As String = dt.Rows(0).Item("val2").ToString
                Dim etiqueta As String = dt.Rows(0).Item("val3").ToString
                Dim univ_nac As Boolean = dt.Rows(0).Item("val4")
                Dim fecha_venc As String = dt.Rows(0).Item("val5").ToString

                'Response.Write("<li>usat:" & dt.Rows(0).Item("val1") & "</li><br/><li>ambito:" & ambito & "</li><br/><li>etiqueta:" & dt.Rows(0).Item("val3") & "</li>")

                'If ambito.Trim = "NACIONAL" Then
                If univ_nac Then
                    divSelec.Visible = True
                    divText.Visible = False
                Else
                    divSelec.Visible = False
                    divText.Visible = True
                End If
                Me.lblUniversidad.Text = etiqueta.Trim + ":"
                If usat Then
                    Me.ddlUniversidad.Value = "260000069"
                    ddlUniversidad.Disabled = True
                Else
                    Me.ddlUniversidad.Value = "-1"
                    ddlUniversidad.Disabled = False
                End If
                Me.hdmonto.Value = monto
                Me.hdfechavenc.Value = fecha_venc

            Else
                Dim tipoParticipante As String

                If Not Request.QueryString("p") Is Nothing Then


                    Select Case Desencriptar(Request.QueryString("p"))

                        Case "E"
                            tipoParticipante = "1"
                        Case "G"
                            tipoParticipante = "3"
                        Case ""
                            tipoParticipante = ""

                    End Select
                Else
                    tipoParticipante = "6"
                End If

                Me.ddltipoparticipante.Value = tipoParticipante
            End If

        Catch ex As Exception
            Response.Write(ex.Message())
        End Try
    End Sub

    Public Function Encriptar(ByVal Input As String) As String

        Dim IV() As Byte = ASCIIEncoding.ASCII.GetBytes("qualityi") 'La clave debe ser de 8 caracteres
        Dim EncryptionKey() As Byte = Convert.FromBase64String("rpaSPvIvVLlrcmtzPU9/c67Gkj7yL1S5") 'No se puede alterar la cantidad de caracteres pero si la clave
        Dim buffer() As Byte = Encoding.UTF8.GetBytes(Input)
        Dim des As TripleDESCryptoServiceProvider = New TripleDESCryptoServiceProvider
        des.Key = EncryptionKey
        des.IV = IV

        Return Convert.ToBase64String(des.CreateEncryptor().TransformFinalBlock(buffer, 0, buffer.Length()))

    End Function

    Public Function Desencriptar(ByVal Input As String) As String
        Dim IV() As Byte = ASCIIEncoding.ASCII.GetBytes("qualityi") 'La clave debe ser de 8 caracteres
        Dim EncryptionKey() As Byte = Convert.FromBase64String("rpaSPvIvVLlrcmtzPU9/c67Gkj7yL1S5") 'No se puede alterar la cantidad de caracteres pero si la clave
        Dim buffer() As Byte = Convert.FromBase64String(Input)
        Dim des As TripleDESCryptoServiceProvider = New TripleDESCryptoServiceProvider
        des.Key = EncryptionKey
        des.IV = IV
        Return Encoding.UTF8.GetString(des.CreateDecryptor().TransformFinalBlock(buffer, 0, buffer.Length()))

    End Function
End Class
