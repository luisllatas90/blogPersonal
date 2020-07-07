Imports System.Collections.Generic
Imports System.Net
Imports System.IO
Imports System.Xml
Imports System.Security.Cryptography
Partial Class Alumni_frmInscripcionAlumni
    Inherits System.Web.UI.Page
    '/*Service*/
    Dim UrlPeticion As String = ConfigurationManager.AppSettings("RutaCampusLocal") & "Congreso/CongresoUsat.asmx"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If IsPostBack = False Then
                Me.hdtoken.Value = "X3VKQUUM" 'AQ3GA56B''X3VKQUUM' -> PARA PRUEBAS: CEREMONIA DE GRADUACIÓN BACHILLERES 2018
                'Me.hdtoken.Value = "74DUAUZD" 'AQ3GA56B -> PARA PRUEBAS: ALUMNI 
                Me.hdevento.Value = "0" 'No se utiliza en el procedimiento, pero igual se envía



                InfoCentroCostos()
                ListaUniversidades()

                Dim tipoParticipante As String
                tipoParticipante = ""
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
                ListaTipoParticipante2(tipoParticipante)

                habilitaLabora(False)
            End If
        Catch ex As Exception
            Response.Write(ex.Message())
        End Try
    End Sub
    Private Sub habilitaLabora(ByVal estado As Boolean)
        Me.txtEmpAlum.Enabled = estado
        Me.txtCargAlum.Enabled = estado
        Me.txtDirAlum.Enabled = estado
        Me.txtTelEmp.Enabled = estado
        Me.txtEmailEmp.Enabled = estado
        Me.rbModLabora.Enabled = estado
        If estado = False Then
            Me.txtEmpAlum.Text = ""
            Me.txtCargAlum.Text = ""
            Me.txtDirAlum.Text = ""
            Me.txtTelEmp.Text = ""
            Me.txtEmailEmp.Text = ""
        End If
    End Sub
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

                ''************ olluen 28/11/2019 lo comente para que no deshabilitara el combo de la universidad
                'If univ_nac Then
                '    divSelec.Visible = True
                '    divText.Visible = False
                'Else
                '    divSelec.Visible = False
                '    divText.Visible = True
                'End If

                'Me.lblUniversidad.Text = etiqueta.Trim + ":"
                'If usat Then
                '    Me.ddlUniversidad.Value = "260000069"
                '    ddlUniversidad.Disabled = True
                'Else
                '    Me.ddlUniversidad.Value = "-1"
                '    ddlUniversidad.Disabled = False
                'End If
                ''************** Fin olluen 28/11/2019 

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
    Public Function Desencriptar(ByVal Input As String) As String
        Dim IV() As Byte = ASCIIEncoding.ASCII.GetBytes("qualityi") 'La clave debe ser de 8 caracteres
        Dim EncryptionKey() As Byte = Convert.FromBase64String("rpaSPvIvVLlrcmtzPU9/c67Gkj7yL1S5") 'No se puede alterar la cantidad de caracteres pero si la clave
        Dim buffer() As Byte = Convert.FromBase64String(Input)
        Dim des As TripleDESCryptoServiceProvider = New TripleDESCryptoServiceProvider
        des.Key = EncryptionKey
        des.IV = IV
        Return Encoding.UTF8.GetString(des.CreateDecryptor().TransformFinalBlock(buffer, 0, buffer.Length()))

    End Function
    Protected Sub InfoCentroCostos()
        Dim m As String = ""
        Try
            Dim list As New Dictionary(Of String, String)
            list.Add("token_cco", Me.hdtoken.Value.ToString)
            Dim obj As New ClsCongreso
            Dim envelope As String = obj.SoapEnvelopeToken(list)

            'm= m & "01 "
            Dim result As String = obj.PeticionRequestSoap(UrlPeticion, envelope, "http://tempuri.org/VerCentroCosto", "serverdev\esaavedra") ' Session("perlogin").ToString

            'Response.Write(result)

            'm=m & "02 "
            Dim dt As New Data.DataTable
            dt = obj.ResultFileTabla(result, "")
            'm=m & "03 " 
            'Response.Write("<script>alert('" + obj.ResultFileTabla(result, "") + "')</script>")
            'result = obj.ResultFileTabla(result, "")
            If dt.Rows.Count = 1 Then
                'm=m & "04 "                
                Me.divEvento.InnerText = dt.Rows(0).Item("des").ToString
                Me.divEvento.Attributes.Add("style", "color:White")
            Else
                Response.Write("Evento No Existe")
                'm=m & "05 "
                'Response.Write("<script>alert('Evento No Existe'); location.href ='http://www.usat.edu.pe'; </script>")
            End If

            'Response.Write(result)
        Catch ex As Exception
            Response.Write(ex)
            'Response.Write("<script>alert('Error al Cargar Datos'); location.href ='http://www.usat.edu.pe'; </script>")
        End Try
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
    'Protected Sub lnkComprobarDNI_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkComprobarDNI.Click
    '    InfoAlumno()
    '    'Call InfoCentroCostos1()
    'End Sub
    'Protected Sub InfoAlumno()
    '    Dim m As String = ""
    '    Try
    '        Dim list As New Dictionary(Of String, String)
    '        list.Add("token_cco", Me.hdtoken.Value.ToString)
    '        Dim obj As New ClsCongreso
    '        Dim envelope As String = obj.SoapEnvelopeTokenDatosAlumno(list)
    '        'm= m & "01 "
    '        Dim result As String = obj.PeticionRequestSoap(UrlPeticion, envelope, "http://tempuri.org/VerDatosParticipante", "serverdev\esaavedra") ' Session("perlogin").ToString
    '        'm=m & "02 "
    '        Dim dt As New Data.DataTable
    '        dt = obj.ResultFileTabla(result, "")
    '        'm=m & "03 " 
    '        'Response.Write("<script>alert('" + obj.ResultFileTabla(result, "") + "')</script>")
    '        'result = obj.ResultFileTabla(result, "")
    '        If dt.Rows.Count = 1 Then
    '            'm=m & "04 "
    '            'Me.Descripcion.InnerText = dt.Rows(0).Item("des").ToString
    '            'Me.Descripcion.Attributes.Add("style", "font-size:14px;font-weight:bold; color:Red")
    '            Me.txtapepat.Text = dt.Rows(0).Item("des").ToString
    '        Else
    '            'm=m & "05 "
    '            'Response.Write("<script>alert('Evento No Existe'); location.href ='http://www.usat.edu.pe'; </script>")
    '        End If

    '        'Response.Write(result)
    '    Catch ex As Exception
    '        'Response.Write("<script>alert('Error al Cargar Datos'); location.href ='http://www.usat.edu.pe'; </script>")
    '    End Try
    'End Sub
    Protected Sub InfoCentroCostos1()
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
                'Me.Descripcion.InnerText = dt.Rows(0).Item("des").ToString
                'Me.Descripcion.Attributes.Add("style", "font-size:14px;font-weight:bold; color:Red")
                Me.txtapepat.Text = dt.Rows(0).Item("des").ToString
            Else
                'm=m & "05 "
                'Response.Write("<script>alert('Evento No Existe'); location.href ='http://www.usat.edu.pe'; </script>")
            End If

            'Response.Write(result)
        Catch ex As Exception
            'Response.Write("<script>alert('Error al Cargar Datos'); location.href ='http://www.usat.edu.pe'; </script>")
        End Try
    End Sub
    Protected Sub btnInscribir_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnInscribir.Click
        Try
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
            list.Add("requiere_factura", "0")
            list.Add("indica_labora", IIf(Me.chkLabora.Checked, 1, 0))

            list.Add("empresa", Me.txtEmpAlum.Text)
            list.Add("cargo", Me.txtCargAlum.Text)
            list.Add("dirEmp", Me.txtDirAlum.Text)
            list.Add("telefEmp", Me.txtTelEmp.Text)
            list.Add("emailEmp", Me.txtEmailEmp.Text)
            list.Add("indLabMod", Me.rbModLabora.SelectedValue)

            Dim obj As New ClsCongreso
            Dim envelope As String = obj.SoapEnvelopeAlumni(list)
            'Response.Write(envelope)
            Dim result As String = obj.PeticionRequestSoap(UrlPeticion, envelope, "http://tempuri.org/InscripcionAlumni", "serverdev\esaavedra") ' Session("perlogin").ToString
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

                'enviarEmailInscripcion(Me.Descripcion.InnerText, Me.txtapepat.Text, Me.txtapemat.Text, Me.txtnombres.Text, Me.txtnrodoc.Text, universidad, Me.txtemail.Text, Me.txtfechaNacimiento.Value, Me.hdmonto.Value, Me.hdfechavenc.Value)

                Me.mensaje.Attributes.Remove("class")
                Me.mensaje.InnerHtml = ""
                Me.mensaje.Attributes.Add("class", "alert alert-success")
                'If Me.chkReqFactura.Checked Then
                '    Me.mensaje.InnerHtml() = "<p>Inscripción exitosa<br>Puedes pagar la inscripción de S/ " + Me.hdmonto.Value + " siguiendo los pasos especificados en el correo que se te acaba de enviar a: " + Me.txtemail.Text + "</p>"
                'Else
                'Me.mensaje.InnerHtml() = "<p>INSCRIPCION EXITOSA<br>Puedes pagar la inscripción de S/ " + Me.hdmonto.Value + " en los canales de atención de los bancos BCP o BBVA indicando tu número de documento de identidad registrado: " + Me.txtnrodoc.Text + "</p>"
                Me.mensaje.InnerHtml() = "<p>INSCRIPCION EXITOSA</p>"
                'End If

                limpiar()

                Page.RegisterStartupScript("redireccionar1", "<script>setTimeout(function(){location.href ='http://www.usat.edu.pe';}, 3000);</script>")
            End If

        Catch ex As Exception

        End Try
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
        Me.chkLabora.Checked = False
    End Sub
    Protected Sub chkLabora_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkLabora.CheckedChanged
        If Me.chkLabora.Checked = True Then
            Call habilitaLabora(True)
        Else
            Call habilitaLabora(False)
        End If
    End Sub
End Class
