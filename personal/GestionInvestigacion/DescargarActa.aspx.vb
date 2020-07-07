Imports System
Imports System.Configuration
Imports System.Data
Imports System.Linq
Imports System.Web
Imports System.Web.Security
Imports System.Web.UI
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.WebControls
Imports System.Web.UI.WebControls.WebParts
Imports System.Xml.Linq
Imports iTextSharp.text
Imports iTextSharp.text.html.simpleparser
Imports iTextSharp.text.pdf
Imports System.Text
Imports System.IO

Partial Class tesis_DescargarActa
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (Session("id_per") Is Nothing) Then
            Response.Redirect("../../sinacceso.html")
        End If
        Try

            'GenerarPdf()
            GenerarWord()

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub GenerarPdf()
        Dim str As New StringBuilder
        Dim rootPath As String = Server.MapPath("~") ' Ruta para imagen de logo
        Dim carrera As String = ""
        Dim titulo As String = ""
        'Dim alumno As String = ""
        Dim abreviatura As String = ""
        Dim dt As New Data.DataTable

        Dim objc As New ClsGestionInvestigacion
        abreviatura = Request.QueryString("e")
        dt = objc.ListarJuradosTesisxEtapa(Request.QueryString("t"), Request.QueryString("a"), abreviatura)

        If dt.Rows.Count > 0 Then
            carrera = dt.Rows(0).Item("nombre_Cpf").ToString
            'alumno = dt.Rows(0).Item("Alumno").ToString
            titulo = dt.Rows(0).Item("titulo_tes").ToString

            str.Append("<table width='100%' style='text-align:center; font-size:11px;'>")
            str.Append("<tr>")
            str.Append("<td style='text-align:center'>")
            str.Append("<div style='text-align:center'>")
            str.Append("<img height='90' width='110' src='" + rootPath + "/images/LogoOficial.png' style='text-align:center'>")
            str.Append("</div>")
            str.Append("</td>")
            str.Append("</tr>")

            str.Append("<tr>")
            str.Append("<td style='text-align:center'>")
            If abreviatura = "P" Then
                str.Append("<b>ACTA DE SUSTENTACIÓN DE PROYECTO DE TESIS</b><br>")
            Else
                str.Append("<b>ACTA DE SUSTENTACIÓN DEL INFORME FINAL DE TESIS</b><br>")
            End If
            str.Append("</td>")
            str.Append("</tr>")

            str.Append("<tr>")
            str.Append("<td style='text-align:center'>")
            str.Append("</td>")
            str.Append("</tr>")
            str.Append("<tr>")
            str.Append("<td style='text-align:justify'>")
            str.Append("En la ciudad de Chiclayo, a las _______________ del día _______ de _______ del _______, los miembros del jurado designados por la escuela profesional de ")
            str.Append(carrera + ", ")

            If abreviatura = "P" Then
                str.Append(dt.Rows(0).Item("jur1").ToString.ToLower)
            Else
                str.Append(dt.Rows(0).Item("tipo1").ToString.ToLower)
            End If

            str.Append(":" + dt.Rows(0).Item("grado1").ToString + " " + dt.Rows(0).Item("docente1").ToString + ", ")

            If abreviatura = "P" Then
                str.Append(dt.Rows(0).Item("jur2").ToString.ToLower)
            Else
                str.Append(dt.Rows(0).Item("tipo2").ToString.ToLower)
            End If

            str.Append(":" + dt.Rows(0).Item("grado2").ToString + " " + dt.Rows(0).Item("docente2").ToString + ", ")

            If abreviatura = "P" Then
                str.Append(dt.Rows(0).Item("jur3").ToString.ToLower)
            Else
                str.Append(dt.Rows(0).Item("tipo3").ToString.ToLower)
            End If

            If dt.Rows(0).Item("docente3").ToString <> "" Then ' si cuenta con los 3 jurados
                str.Append(":" + dt.Rows(0).Item("grado3").ToString + " " + dt.Rows(0).Item("docente3").ToString + ", ")
            End If

            str.Append("se reunieron en el aula N° ___________, para recibir la sustentación del")

            If abreviatura = "P" Then
                str.Append(" proyecto ")
            Else
                str.Append(" informe final ")
            End If
            str.Append("de Tesis titulado: ")
            str.Append("<b>" + titulo + "</b>")

            If dt.Rows.Count > 1 Then
                str.Append(", de los estudiantes <b>")
                For i As Integer = 0 To dt.Rows.Count - 1
                    str.Append(dt.Rows(i).Item("Alumno").ToString())
                    If i < dt.Rows.Count - 1 Then
                        str.Append(", ")
                    End If
                Next
                str.Append("</b>.")
            Else
                str.Append(", del estudiante <b>" + dt.Rows(0).Item("Alumno").ToString + "</b>.")
            End If


            str.Append("</td>")
            str.Append("</tr>")


            str.Append("<tr>")
            str.Append("<td style='text-align:center'><br>")
            str.Append("</td>")
            str.Append("</tr>")
            str.Append("<tr>")

            str.Append("<tr>")
            str.Append("<td style='text-align:justify'>")
            str.Append("Siendo las __________________, habiéndose concluido la exposición y absueltas las preguntas del jurado, se acordó otorgar al estudiante la calificación de ______________, recibiendo la categoria de ______________________.<br><br>")
            str.Append("</td>")
            str.Append("</tr>")

            str.Append("<tr style='height:30px;font-size:10px;'>")
            str.Append("<td style='text-align:right'>")
            str.Append("Chiclayo, _______ de _______________ del ________.")
            str.Append("</td>")
            str.Append("</tr>")
            str.Append("</table>")

            str.Append("<br>")
            str.Append("<br>")
            str.Append("<br>")
            str.Append("<br>")
            str.Append("<br>")
            str.Append("<br>")
            str.Append("<br>")

            str.Append("<table width='100%' style='text-align:center; font-size:8px;' cellspacing='0' cellpadding='0'>")
            str.Append("<tr>")
            str.Append("<td width='40%' border='1'  cellspacing='0' cellpadding='0'>")
            str.Append("</td>")
            str.Append("<td width='20%' >")
            str.Append("</td>")
            str.Append("<td width='40%' border='1' cellspacing='0' cellpadding='0'>")
            str.Append("</td>")
            str.Append("</tr>")
            str.Append("<tr>")
            str.Append("<td width='40%'><p style='text-align:center'><b>")
            str.Append(dt.Rows(0).Item("grado1").ToString + " " + dt.Rows(0).Item("docente1").ToString + "<br>")
            If abreviatura = "P" Then
                str.Append(dt.Rows(0).Item("jur1").ToString)
            Else
                str.Append(dt.Rows(0).Item("tipo1").ToString)
            End If
            str.Append("</b></p></td>")
            str.Append("<td width='20%' >")
            str.Append("</td>")
            str.Append("<td width='40%'><p style='text-align:center'><b>")
            str.Append(dt.Rows(0).Item("grado2").ToString + " " + dt.Rows(0).Item("docente2").ToString + "<br>")
            If abreviatura = "P" Then
                str.Append(dt.Rows(0).Item("jur2").ToString)
            Else
                str.Append(dt.Rows(0).Item("tipo2").ToString)
            End If
            str.Append("</b></p></td>")
            str.Append("</tr>")
            str.Append("</table>")

            If dt.Rows(0).Item("docente3").ToString <> "" Then ' si cuenta con los 3 jurados
                str.Append("<br>")
                str.Append("<br>")
                str.Append("<br>")
                str.Append("<br>")
                str.Append("<br>")

                str.Append("<table width='100%' style='text-align:center; font-size:8px;' cellspacing='0' cellpadding='0'>")
                str.Append("<tr>")
                str.Append("<td width='25%'>")
                str.Append("</td>")
                str.Append("<td width='50%' border='1' cellspacing='0' cellpadding='0'>")
                str.Append("</td>")
                str.Append("<td width='25%' >")
                str.Append("</td>")
                str.Append("</tr>")
                str.Append("<tr>")
                str.Append("<tr>")
                str.Append("<td width='25%'>")
                str.Append("</td>")
                str.Append("<td width='50%'><p style='text-align:center'><b>")
                str.Append(dt.Rows(0).Item("grado3").ToString + " " + dt.Rows(0).Item("docente3").ToString + "<br>")

                If abreviatura = "P" Then
                    str.Append(dt.Rows(0).Item("jur3").ToString)
                Else
                    str.Append(dt.Rows(0).Item("tipo3").ToString)
                End If

                str.Append("</b></p></td>")
                str.Append("<td width='25%'>")
                str.Append("</td>")
                str.Append("</tr>")
                str.Append("</table>")

            End If

            Me.divCab.InnerHtml = str.ToString


            Dim documento As New Document(PageSize.A4, 60, 60, 50, 50)
            Dim ms As New MemoryStream
            Dim writer As PdfWriter
            writer = PdfWriter.GetInstance(documento, ms)
            Dim textohtml As String = divCab.InnerHtml.ToString()

            'Response.Write(textohtml)
            'divCab.InnerHtml = ""
            ''textohtml = "X"
            Dim se As New StringReader(textohtml)
            Dim obj As New HTMLWorker(documento)


            documento.Open()
            obj.Parse(se)

            documento.Close()
            Response.Clear()
            Response.AddHeader("content-disposition", "attachment; filename=ACTA_DE_SUSTENTACION.pdf")
            Response.ContentType = "application/pdf"
            Response.Buffer = True
            Response.OutputStream.Write(ms.GetBuffer(), 0, ms.GetBuffer().Length)
            Response.OutputStream.Flush()
            Response.End()
            Response.Close()
        Else
            Response.Write("<h3>No se pudo Generar Acta</h3>")
        End If
    End Sub



    Private Sub GenerarWord()
        Dim str As New StringBuilder
        Dim rootPath As String = Server.MapPath("~") ' Ruta para imagen de logo
        Dim carrera As String = ""
        Dim titulo As String = ""
        'Dim alumno As String = ""
        Dim abreviatura As String = ""
        Dim dt As New Data.DataTable

        Dim objc As New ClsGestionInvestigacion
        abreviatura = Request.QueryString("e")
        dt = objc.ListarJuradosTesisxEtapa(Request.QueryString("t"), Request.QueryString("a"), abreviatura)

        If dt.Rows.Count > 0 Then
            carrera = dt.Rows(0).Item("nombre_Cpf").ToString
            'alumno = dt.Rows(0).Item("Alumno").ToString
            titulo = dt.Rows(0).Item("titulo_tes").ToString

            str.Append("<table width='100%' style='text-align:center; font-size:11pt; line-height:150%;'>")
            str.Append("<tr>")
            str.Append("<td style='text-align:center'>")
            str.Append("<div style='text-align:center'>")
            str.Append("<img height='90' width='110' src='http://" + HttpContext.Current.Request.Url.Host + "/campusvirtual/images/LogoOficial.png' style='text-align:center'>")
            'Dim posicion As Integer = Request.Url.AbsoluteUri.IndexOf("GestionInvestigacion/DescargarActa.aspx")
            'str.Append("<img height='90' width='110' src='" + Request.Url.AbsoluteUri.Substring(0, posicion) + "images/LogoOficial.png'  style='text-align:center'>")
            str.Append("</div>")
            str.Append("</td>")
            str.Append("</tr>")

            str.Append("<tr>")
            str.Append("<td style='text-align:center'>")
            If abreviatura = "P" Then
                str.Append("<b>ACTA DE SUSTENTACIÓN DE PROYECTO DE TESIS</b><br>")
            Else
                str.Append("<b>ACTA DE SUSTENTACIÓN DEL INFORME FINAL DE TESIS</b><br>")
            End If
            str.Append("</td>")
            str.Append("</tr>")

            str.Append("<tr>")
            str.Append("<td style='text-align:center'>")
            str.Append("</td>")
            str.Append("</tr>")
            str.Append("<tr>")
            str.Append("<td style='text-align:justify'>")
            str.Append("En la ciudad de Chiclayo, a las _______________ del día _______ de _______ del _______, los miembros del jurado designados por la escuela profesional de ")
            str.Append(carrera + ", ")

            If abreviatura = "P" Then
                str.Append(dt.Rows(0).Item("jur1").ToString.ToLower)
            Else
                str.Append(dt.Rows(0).Item("tipo1").ToString.ToLower)
            End If

            str.Append(":" + dt.Rows(0).Item("grado1").ToString + " " + dt.Rows(0).Item("docente1").ToString + ", ")

            If abreviatura = "P" Then
                str.Append(dt.Rows(0).Item("jur2").ToString.ToLower)
            Else
                str.Append(dt.Rows(0).Item("tipo2").ToString.ToLower)
            End If

            str.Append(":" + dt.Rows(0).Item("grado2").ToString + " " + dt.Rows(0).Item("docente2").ToString + ", ")

            If abreviatura = "P" Then
                str.Append(dt.Rows(0).Item("jur3").ToString.ToLower)
            Else
                str.Append(dt.Rows(0).Item("tipo3").ToString.ToLower)
            End If

            If dt.Rows(0).Item("docente3").ToString <> "" Then ' si cuenta con los 3 jurados
                str.Append(":" + dt.Rows(0).Item("grado3").ToString + " " + dt.Rows(0).Item("docente3").ToString + ", ")
            End If

            str.Append("se reunieron en el aula N° ___________, para recibir la sustentación del")

            If abreviatura = "P" Then
                str.Append(" proyecto ")
            Else
                str.Append(" informe final ")
            End If
            str.Append("de Tesis titulado: ")
            str.Append("<b>" + titulo + "</b>")

            If dt.Rows.Count > 1 Then
                str.Append(", de los estudiantes <b>")
                For i As Integer = 0 To dt.Rows.Count - 1
                    str.Append(dt.Rows(i).Item("Alumno").ToString())
                    If i < dt.Rows.Count - 1 Then
                        str.Append(", ")
                    End If
                Next
                str.Append("</b>.")
            Else
                str.Append(", del estudiante <b>" + dt.Rows(0).Item("Alumno").ToString + "</b>.")
            End If


            str.Append("</td>")
            str.Append("</tr>")


            str.Append("<tr>")
            str.Append("<td style='text-align:center;'><br>")
            str.Append("</td>")
            str.Append("</tr>")


            str.Append("<tr>")
            str.Append("<td style='text-align:justify'>")
            str.Append("Siendo las __________________, habiéndose concluido la exposición y absueltas las preguntas del jurado, se acordó otorgar al estudiante la calificación de ______________, recibiendo la categoria de ______________________.<br><br>")
            str.Append("</td>")
            str.Append("</tr>")

            str.Append("<tr style='height:30px;font-size:10pt;'>")
            str.Append("<td style='text-align:right'>")
            str.Append("Chiclayo, _______ de _______________ del ________.")
            str.Append("</td>")
            str.Append("</tr>")
            str.Append("</table>")

            str.Append("<br>")
            str.Append("<br>")
            str.Append("<br>")
            str.Append("<br>")
            str.Append("<br>")

            str.Append("<table width='100%' style='text-align:center; font-size:8pt;line-height:130%;' cellspacing='0' cellpadding='0'>")
            str.Append("<tr>")
            str.Append("<td width='40%' border='1'  cellspacing='0' cellpadding='0'>")
            str.Append("</td>")
            str.Append("<td width='20%' >")
            str.Append("</td>")
            str.Append("<td width='40%' border='1' cellspacing='0' cellpadding='0'>")
            str.Append("</td>")
            str.Append("</tr>")
            str.Append("<tr>")
            str.Append("<td width='40%'><p style='text-align:center'><b>")
            str.Append(dt.Rows(0).Item("grado1").ToString + " " + dt.Rows(0).Item("docente1").ToString + "<br>")
            If abreviatura = "P" Then
                str.Append(dt.Rows(0).Item("jur1").ToString)
            Else
                str.Append(dt.Rows(0).Item("tipo1").ToString)
            End If
            str.Append("</b></p></td>")
            str.Append("<td width='20%' >")
            str.Append("</td>")
            str.Append("<td width='40%'><p style='text-align:center'><b>")
            str.Append(dt.Rows(0).Item("grado2").ToString + " " + dt.Rows(0).Item("docente2").ToString + "<br>")
            If abreviatura = "P" Then
                str.Append(dt.Rows(0).Item("jur2").ToString)
            Else
                str.Append(dt.Rows(0).Item("tipo2").ToString)
            End If
            str.Append("</b></p></td>")
            str.Append("</tr>")
            str.Append("</table>")

            If dt.Rows(0).Item("docente3").ToString <> "" Then ' si cuenta con los 3 jurados
                str.Append("<br>")
                str.Append("<br>")
                str.Append("<br>")
                str.Append("<br>")
                str.Append("<br>")

                str.Append("<table width='100%' style='text-align:center; font-size:8pt;line-height:130%;' cellspacing='0' cellpadding='0'>")
                str.Append("<tr>")
                str.Append("<td width='25%'>")
                str.Append("</td>")
                str.Append("<td width='50%' border='1' cellspacing='0' cellpadding='0'>")
                str.Append("</td>")
                str.Append("<td width='25%' >")
                str.Append("</td>")
                str.Append("</tr>")
                str.Append("<tr>")
                str.Append("<tr>")
                str.Append("<td width='25%'>")
                str.Append("</td>")
                str.Append("<td width='50%'><p style='text-align:center'><b>")
                str.Append(dt.Rows(0).Item("grado3").ToString + " " + dt.Rows(0).Item("docente3").ToString + "<br>")

                If abreviatura = "P" Then
                    str.Append(dt.Rows(0).Item("jur3").ToString)
                Else
                    str.Append(dt.Rows(0).Item("tipo3").ToString)
                End If

                str.Append("</b></p></td>")
                str.Append("<td width='25%'>")
                str.Append("</td>")
                str.Append("</tr>")
                str.Append("</table>")

            End If

            Me.divCab.InnerHtml = str.ToString
            Dim textohtml As String = "<html><head><meta http-equiv='Content-Type' content='text/html; charset=utf-8'></head><body>"
            textohtml += "<style type='text/css'>table tbody tr td{ padding: 4px;line-height: 200%; }</style>"
            textohtml += Me.divCab.InnerHtml.ToString
            textohtml += "</body></html>"

            HttpContext.Current.Response.Clear()

            HttpContext.Current.Response.Charset = "utf-8"
            HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.UTF8
            HttpContext.Current.Response.ContentType = "application/msword"

            Dim strFileName As String = "ACTA DE SUSTENTACIÓN" & ".doc"

            HttpContext.Current.Response.AddHeader("Content-Disposition", "inline;filename=" & strFileName)

            HttpContext.Current.Response.Write(textohtml)
            HttpContext.Current.Response.Flush()
            HttpContext.Current.Response.End()
        Else
            Response.Write("<h3>No se pudo Generar Acta</h3>")
        End If
    End Sub



End Class

