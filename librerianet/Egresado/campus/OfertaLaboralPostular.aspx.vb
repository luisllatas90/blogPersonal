
Partial Class Egresado_campus_OfertaLaboralPostular
    Inherits System.Web.UI.Page

    Protected Sub btnEnviar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEnviar.Click
        Response.Redirect("OfertasLaborales.aspx")
    End Sub

    Function encode(ByVal str As String) As String
        Return (Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(str)))
    End Function

    Function decode(ByVal str As String) As String
        Return System.Text.ASCIIEncoding.ASCII.GetString(Convert.FromBase64String(str))
    End Function

    Protected Sub btnEnviar0_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEnviar0.Click
        Try
            If validar() Then
                Dim cls As New ClsEnvioMailAlumni
                Dim mensaje As String = ""
                mensaje = "<span style=""font-size:12px; font-family:Verdana"">"
                mensaje &= "<br /><b>" & Me.lblNombreOferta.Text & "</b>"
                mensaje &= "<br /><br />" & Me.txtMensaje.Text.Trim
                mensaje &= "<br /><br /><br /><a style=""color:#e33439; text-align:center;font-size:11px;cursor:hand;cursor:pointer;background:#F3F3F3;padding:3px; border: 1px solid #C2C2C2;"" href=""" & Me.lblCvOnLine.NavigateUrl & """<br />Clic aquí para ver el CV de " & Me.Label1.Text & "</a>"
                mensaje &= "<br />"
                mensaje &= "</span>"
                mensaje &= "<br />_________________________________<br />"
                mensaje &= "<span style=""font-size:11px; font-family:Verdana"">Bolsa de Trabajo alumniUSAT<br />"
                mensaje &= "Contacto: alumni@usat.edu.pe<br /></span>"
                mensaje &= "<span style=""font-size:9px; font-family:Verdana""><br />*La información contenida en el CV es ingresada por el egresado conforme al formato diseñado por la Dirección ALUMNI USAT.<br /> *Envío automático de postulación a la oferta laboral publicada en la BOLSA DE TRABAJO de alumniUSAT.</span>"
                Dim para As String = ""
                para = Session("xc").ToString
                'para = "yperez@usat.edu.pe"
                'Dim rpta As String = ""
                'rpta = cls.EnviarMailAd("alumni@usat.edu.pe", "alumniUSAT", para, "Postulo a: " & Me.lblNombreOferta.Text & " - " & Me.Label1.Text, mensaje, True, "alumni@usat.edu.pe", Me.lblCorreo.Text, "", "")
                If cls.EnviarMailAd("alumni@usat.edu.pe", "alumniUSAT", para, "Postulo a: " & Me.lblNombreOferta.Text & " - " & Me.Label1.Text, mensaje, True, "alumni@usat.edu.pe", Me.lblCorreo.Text, "", "") Then
                    Session("xc") = ""
                    Dim obj As New ClsConectarDatos
                    obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
                    obj.AbrirConexion()
                    obj.Ejecutar("Alumni_RegistrarEgresadoEnviaCV", CInt(Session("codigo_alu")), CInt(decode(Request.QueryString("xof"))))
                    obj.CerrarConexion()
                    obj = Nothing
                    ClientScript.RegisterStartupScript(Me.GetType, "Alerta", "alert('Se ha enviado tu CV satisfactoriamente'); location.href='OfertasLaborales.aspx';", True)
                Else
                    'Me.lblAviso.Text = "rpta: " & rpta '"Error al enviar CV"
                    Me.lblAviso.Text = "Error al enviar CV"
                End If
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
        
    End Sub
    Function validar() As Boolean
        If Me.txtMensaje.Text.Trim = "" Then
            Me.lblAviso.Text = "Debe Ingresar el mensaje"
            Me.txtMensaje.Focus()
            Return False
        End If
        Return True
    End Function

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Session("pagina") = "../../../librerianet/egresado/"
            If Request.QueryString("xof") IsNot Nothing Then
                Dim obj As New ClsConectarDatos
                obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
                obj.AbrirConexion()
                Dim dt As Data.DataTable
                dt = obj.TraerDataTable("ALUMNI_CargarDatosPostular", CInt(Session("codigo_alu")), decode(Request.QueryString("xof")))
                If dt.Rows.Count Then
                    Me.lblDe.Text = dt.Rows(0).Item("Egresado") & "<br/>" & dt.Rows(0).Item("emailPrincipal_Pso") & "<br/>Bolsa de Trabajo alumniUSAT"
                    'Me.lblPara.Text = dt.Rows(0).Item("contacto_ofe") & "<br/>" & dt.Rows(0).Item("correocontacto_ofe") & "<br/>" & dt.Rows(0).Item("nombrepro")
                    Me.lblPara.Text = dt.Rows(0).Item("nombrepro")
                    Me.lblCorreo.Text = dt.Rows(0).Item("emailPrincipal_Pso")
                    Me.lblCorreoPara.Text = dt.Rows(0).Item("correocontacto_ofe")
                    Me.Label1.Text = dt.Rows(0).Item("Egresado")
                    Me.lblNombreOferta.Text = dt.Rows(0).Item("titulo_ofe")
                    Me.lblNombreOferta2.Text = "Postulo a: " & Me.lblNombreOferta.Text & " - " & Me.Label1.Text
                    Me.lblCvOnLine.NavigateUrl = Session("pagina") & "alumniusat.aspx?xcod=" & encode(Session("codigo_alu"))
                    Session("xc") = dt.Rows(0).Item("correopara")
                    dt = Nothing
                End If
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try

    End Sub
End Class
