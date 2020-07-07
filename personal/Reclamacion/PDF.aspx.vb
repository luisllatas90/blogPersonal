Imports System.Collections.Generic
Imports System.Net
Imports System.IO
Imports System.Xml
'Imports iTextSharp.text
'Imports iTextSharp.text.html.simpleparser
'Imports iTextSharp.text.pdf

Partial Class PDF
    Inherits System.Web.UI.Page
    Private C As ClsConectarDatos

    Sub New()
        If C Is Nothing Then
            C = New ClsConectarDatos
            C.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim cod, nom, dir, mai As String
            cod = Request.QueryString("cod").Trim()
            nom = Request.QueryString("nom").Trim()
            dir = Request.QueryString("dir").Trim()
            mai = Request.QueryString("mai").Trim()

            If Request.QueryString("acc").Trim().Equals("ATENDIDO") Then
                Dim sol, ate, fec, env As String
                sol = Request.QueryString("sol").Trim()
                ate = Request.QueryString("ate").Trim()
                fec = Request.QueryString("fec").Trim()
                env = Request.QueryString("env").Trim()

                Call GenerarPDFAtendido(cod, nom, dir, mai, sol, ate, fec, env)
            Else
                Dim dni, tel, ped, tip, tbc As String
                dni = Request.QueryString("dni").Trim()
                tel = Request.QueryString("tel").Trim()
                ped = Request.QueryString("ped").Trim()
                tip = Request.QueryString("tip").Trim()
                tbc = Request.QueryString("tbc").Trim()

                Call GenerarPDF(cod, tbc, tip, nom, dni, mai, tel, dir, ped)
            End If
        Catch ex As Exception
            Response.Write(ex.Message & " - " & ex.StackTrace)
        End Try
    End Sub

    Private Function GenerarPDF(ByVal codigo As String, ByVal bien As String, ByVal tipo As String, ByVal nombre As String, ByVal ndoc As String, ByVal correo As String, ByVal telf As String, ByVal domicilio As String, ByVal pedido As String) As Boolean
        tipo = IIf(tipo.Equals("Q"), "QUEJA", "RECLAMO")
        bien = IIf(bien.Equals("P"), "PRODUCTO", "SERVICIO")
        pedido = pedido.ToLower()
        pedido = pedido.Replace("\r\n", "<br/>")
        pedido = pedido.Replace("\n", "<br/>")
        pedido = pedido.Replace("""", "")

        Dim imageURL As String = Server.MapPath(".") & "/images/logousat.png"
        Dim str As New StringBuilder
        str.Append("<html>")
        str.Append("    <head>")
        str.Append("    <meta http-equiv='Content-Type' content='text/html; charset=UTF-8'/>")
        str.Append("    </head>")
        str.Append("    <body style='font-family:Calibri; font-size:10px;'>")
        str.Append("        <table border='0' cellpadding='0' cellspacing='0'>")
        str.Append("            <tr><td align='right'><div style='font-family:Calibri;'><i>Hoja de reclamo N° " & codigo & "</i></div></td></tr>")
        str.Append("            <tr><td align='center'><img src='" & imageURL & "' align='center' /></td></tr>")
        str.Append("            <tr><td align='center'><div style='font-family:Calibri; color:#F1132A; font-size:14px; font-weight: bold;'>" & tipo & " " & bien & "</div></td></tr>")
        str.Append("        </table>")
        str.Append("        <table border='0' cellpadding='0' cellspacing='0'>")
        str.Append("            <tr><td style='color:#F1132A;'>____________________________________________________________________________</td></tr>")
        str.Append("        </table>")
        str.Append("        <table border='0' cellpadding='0' cellspacing='0'><tr><td>")
        str.Append("            <div style='text-align:left;font-family:Calibri'><b>Estimado(a) " & nombre.Split(" ")(0).ToUpper() & "</b></div>")
        str.Append("            <div style='margin-top:10px;text-align:left;color:#675E5C;font-family:Calibri'><b>Se ha realizado el registro de " & tipo & " con los siguientes datos:</b></div>")
        str.Append("            <div style='margin-top:5px;text-align:left;color:#675E5C;font-family:Calibri'></div></td></tr>")
        str.Append("        </table>")
        str.Append("        <table border='0' cellpadding='0' cellspacing='0'><tr><td>")
        str.Append("            <div style='width:100%;margin-top:10px;text-align:left;color:#675E5C;font-family:Calibri'>&nbsp;&nbsp;&nbsp;<b>- Nombres y Apellidos: </b>" & nombre.ToUpper() & "</div>")
        str.Append("            <div style='width:100%;margin-top:10px;text-align:left;color:#675E5C;font-family:Calibri'>&nbsp;&nbsp;&nbsp;<b>- Documento: </b>" & ndoc & "</div>")
        str.Append("            <div style='width:100%;margin-top:10px;text-align:left;color:#675E5C;font-family:Calibri'>&nbsp;&nbsp;&nbsp;<b>- E-mail: </b>" & correo & "</div>")
        str.Append("            <div style='width:100%;margin-top:10px;text-align:left;color:#675E5C;font-family:Calibri'>&nbsp;&nbsp;&nbsp;<b>- Teléfono: </b>" & telf & "</div>")
        str.Append("            <div style='width:100%;margin-top:10px;text-align:left;color:#675E5C;font-family:Calibri'>&nbsp;&nbsp;&nbsp;<b>- Dirección: </b>" & domicilio.ToUpper() & "</div></td></tr>")
        str.Append("        </table><br/><br/>")
        str.Append("        <table border='0' cellpadding='0' cellspacing='0'><tr><td>")
        str.Append("            <div style='text-align:left;font-family:Calibri'><b>Contenido del reclamo o queja:</b></div>")
        str.Append("            <div style='margin-top:10px;text-align:left;color:#675E5C;font-family:Calibri'><i>&nbsp;&nbsp;" & pedido.ToUpper() & "</i></div></td></tr>")
        str.Append("        </table><br/>")
        str.Append("        <table border='0' cellpadding='0' cellspacing='0'>")
        str.Append("            <tr><td style='color:#F1132A;'>____________________________________________________________________________</td></tr>")
        str.Append("        </table><br/>")
        str.Append("        <table border='0' cellpadding='0' cellspacing='0'><tr><td>")
        str.Append("            <div style='margin-top:10px;text-align:left;color:#675E5C;font-family:Calibri'>")
        str.Append("                Su reclamo o queja será atendido a la brevedad.<br/><br/>Atentamente,<br/><br/><b>UNIVERSIDAD CATÓLICA SANTO TORIBIO DE MOGROVEJO</b>")
        str.Append("            </div></td></tr>")
        str.Append("        </table>")
        str.Append("    </body>")
        str.Append("</html>")

        Try
            Dim documento As New iTextSharp.text.Document(iTextSharp.text.PageSize.A4, 85, 85, 67, 50)
            Dim ms As New MemoryStream
            Dim writer As iTextSharp.text.pdf.PdfWriter
            Dim se As New StringReader(str.ToString)
            Dim obj As New iTextSharp.text.html.simpleparser.HTMLWorker(documento)
            writer = iTextSharp.text.pdf.PdfWriter.GetInstance(documento, ms)

            documento.Open()
            obj.Parse(se)
            writer.CloseStream = False
            documento.Close()
            ms.Position = 0 '--> Para mantener en memoria

            'ms.Close()
            se.Close()
            obj.Close()

            '--> Para descargar el PDF
            Response.Clear()
            Response.AddHeader("content-disposition", "attachment; filename=" & codigo & ".pdf")
            Response.ContentType = "application/pdf"
            Response.Buffer = True
            Response.OutputStream.Write(ms.GetBuffer(), 0, ms.GetBuffer().Length)
            Response.OutputStream.Flush()
            Response.End()
            Response.Close()
        Catch ex As Exception
            Response.Write(ex.Message & " - " & ex.StackTrace)
            Return False
        End Try

        Return True
    End Function

    Private Function GenerarPDFAtendido(ByVal codigo As String, ByVal nombre As String, ByVal domicilio As String, ByVal correo As String, ByVal solucion As String, ByVal atendido As String, ByVal fecha As String, ByVal enviar As String) As Boolean
        solucion = solucion.Replace("\r\n", "<br/>")
        solucion = solucion.Replace("""", "")

        Dim imageURL As String = Server.MapPath(".") & "/images/logousat.png"
        Dim str As New StringBuilder
        str.Append("<html>")
        str.Append("    <head>")
        str.Append("    <meta http-equiv='Content-Type' content='text/html; charset=UTF-8'/>")
        str.Append("    </head>")
        str.Append("    <body style='font-family:Calibri; font-size:10px;'>")
        str.Append("        <table border='0' cellpadding='0' cellspacing='0' style='border-collapse: collapse'>")
        str.Append("            <tr><td align='center'><div style='border-bottom-style: solid; border-bottom: thick dotted #ff0000;'><img src='" & imageURL & "' align='center' width='80px' height='80px' /></div></td></tr>")
        str.Append("            <tr><td style='color:#F1132A;'>____________________________________________________________________________</td></tr>")
        str.Append("            <tr><td align='left'>Chiclayo, " & atendido & "</td></tr>")
        str.Append("            <tr><td align='justify'><br/></td></tr>")
        str.Append("            <tr><td align='left'>Sr(a).:</td></tr>")
        str.Append("            <tr><td align='left'>" & nombre & "</td></tr>")
        str.Append("            <tr><td align='left'>" & domicilio & "</td></tr>")
        str.Append("            <tr><td align='justify'><br/></td></tr>")
        str.Append("            <tr><td align='left'><b>Presente.-</b></td></tr>")
        str.Append("            <tr><td align='left'>De nuestra consideración:</td></tr>")
        str.Append("            <tr><td align='justify'><br/></td></tr>")
        str.Append("            <tr><td align='left'><b>Referencia: " & codigo & "</b></td></tr>")
        str.Append("        </table><br/>")
        str.Append("        <table border='0' cellpadding='0' cellspacing='0'>")
        str.Append("            <tr><td align='justify'>" & solucion & "</td></tr>")
        str.Append("        </table><br/>")
        str.Append("        <table border='0' cellpadding='0' cellspacing='0'>")
        str.Append("            <tr><td align='left'>Sin otro particular, quedamos de usted.</td></tr>")
        str.Append("            <tr><td align='justify'><br/></td></tr>")
        str.Append("            <tr><td align='left'>Atentamente,</td></tr>")
        str.Append("            <tr><td align='justify'><br/></td></tr>")
        str.Append("            <tr><td align='left'><b>UNIVERSIDAD CATÓLICA SANTO TORIBIO DE MOGROVEJO</b></td></tr>")
        str.Append("            <tr><td align='left'>Av. San Josemaria Escriva Nro. 855, Chiclayo - Lambayeque</td></tr>")
        str.Append("        </table>")
        str.Append("    </body>")
        str.Append("</html>")

        Try
            Dim documento As New iTextSharp.text.Document(iTextSharp.text.PageSize.A4, 85, 85, 67, 50)
            Dim ms As New MemoryStream
            Dim writer As iTextSharp.text.pdf.PdfWriter
            Dim se As New StringReader(str.ToString)
            Dim obj As New iTextSharp.text.html.simpleparser.HTMLWorker(documento)
            writer = iTextSharp.text.pdf.PdfWriter.GetInstance(documento, ms)

            documento.Open()
            obj.Parse(se)
            writer.CloseStream = False
            documento.Close()

            If enviar.Equals("SI") Then
                ms.Position = 0 '--> Para mantener en memoria

                Dim cM As New ClsMail
                cM.EnviarPDFMail("campusvirtual@usat.edu.pe", "Campus Virtual-USAT", correo, "Respuesta al libro de reclamo " & codigo, "Se adjunta la constancia de respuesta del libro de reclamo registrado el " & fecha & ".<br /><br />Por favor, revise su archivo adjunto.", True, (codigo.Trim() & "-Respuesta"), ms, listaCorreos())
            End If

            'ms.Close()
            se.Close()
            obj.Close()

            'If Not enviar.Equals("SI") Then
            '--> Para descargar el PDF
            Response.Clear()
            Response.AddHeader("content-disposition", "attachment; filename=" & codigo & "-Respuesta.pdf")
            Response.ContentType = "application/pdf"
            Response.Buffer = True
            Response.OutputStream.Write(ms.GetBuffer(), 0, ms.GetBuffer().Length)
            Response.OutputStream.Flush()
            Response.End()
            Response.Close()
            'End If

        Catch ex As Exception
            Response.Write("<script> alert('PDF: " & ex.Message & " - " & ex.StackTrace & "'); </script>")
            Return False
        End Try

        Return True
    End Function

    Private Function listaCorreos() As String
        Dim correos As String

        Try
            Dim dt As New Data.DataTable("correos")
            C.AbrirConexion()
            dt = C.TraerDataTable("LST_CorreosAsesoriaJuridica")

            correos = dt.Rows(0)(0).ToString()
            dt.dispose()
        Catch ex As Exception
            correos = ""
        End Try

        Return correos
    End Function
End Class
