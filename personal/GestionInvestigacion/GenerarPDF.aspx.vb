﻿Imports System
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

Partial Class tesis_GenerarPDF
    Inherits System.Web.UI.Page


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (Session("id_per") Is Nothing) Then
            Response.Redirect("../../sinacceso.html")
        End If
        Try
            Select Case Request("tipo")
                Case "RESO"
                    GenerarResolucionSustentacion()
                Case "ACTA"
                    GenerarActaSustentacion()
            End Select

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub


    Private Sub GenerarResolucionSustentacion()
        Try
            Dim str As New StringBuilder
            Dim rootPath As String = Server.MapPath("~") ' Ruta para imagen de logo
            Dim carrera As String = ""
            Dim titulo As String = ""
            Dim alumno As String = ""
            Dim abreviatura As String = "S"
            Dim dt As New Data.DataTable

            Dim objc As New ClsGestionInvestigacion

            dt = objc.DatosResolucionJurado(Request.QueryString("t"), Request.QueryString("a"), abreviatura)

            str.Append("<div style='text-align:left'>")
            str.Append("<img height='80' width='100' src='" + rootPath + "/images/LogoOficial.png' style='text-align:left'>")
            str.Append("</div>")
            str.Append("<table width='100%' style='text-align:center; font-size:11px; font-family:Belwe Lt BT' padding='1'>")
            str.Append("<tr>")
            str.Append("<td style='text-align:center; font-size:14px;'>")
            str.Append("<b>CONSEJO DE FACULTAD</b>")
            str.Append("</td>")
            str.Append("</tr>")
            str.Append("<tr>")
            str.Append("<td style='text-align:center; font-size:12px;'>")
            str.Append("<b>RESOLUCIÓN N° " + dt.Rows(0).Item("Resolucion").ToString + "</b>")
            str.Append("</td>")
            str.Append("</tr>")
            str.Append("<tr>")
            str.Append("<td style='text-align:center; font-size:12px;'>")
            str.Append("<b>DESIGNACIÓN DE JURADO DE TESIS</b>")
            str.Append("</td>")
            str.Append("</tr>")
            str.Append("<tr>")
            str.Append("<td style='text-align:center; font-size:11px;'>")
            str.Append("<b>Chiclayo, " + dt.Rows(0).Item("FechaResolucion").ToString + "</b>")
            str.Append("</td>")
            str.Append("</tr>")
            str.Append("<tr>")
            str.Append("<td style='text-align:left; font-size:11px;'>")
            str.Append("<br><b>VISTO</b>")
            str.Append("</td>")
            str.Append("</tr>")
            str.Append("<tr>")
            str.Append("<td style='text-align:justify; font-size:11px;'>")
            str.Append("el expediente N° " + Request.QueryString("x") + " presentado por ")

            'Para un solo autor
            If dt.Rows.Count = 1 Then
                str.Append(dt.Rows(0).Item("Alumno").ToString + " bachiller en " + dt.Rows(0).Item("nombreOficial_cpf").ToString())
            Else ' Varios autores

                For i As Integer = 0 To dt.Rows.Count - 1
                    If i = 0 Then
                        str.Append(dt.Rows(i).Item("Alumno").ToString)
                    ElseIf i > 0 And i < dt.Rows.Count - 1 Then
                        str.Append(", " + dt.Rows(i).Item("Alumno").ToString)
                    Else
                        str.Append(" y " + dt.Rows(i).Item("Alumno").ToString)
                    End If
                Next
                str.Append(" bachilleres en " + dt.Rows(0).Item("nombreOficial_cpf").ToString)
            End If
            str.Append(", mediante el cual solicita la sustentación de su tesis con fines de titulación; y, ")
            str.Append("</td>")
            str.Append("</tr>")
            str.Append("<tr>")
            str.Append("<td style='text-align:left; font-size:11px;'>")
            str.Append("<br><b>CONSIDERANDO</b>")
            str.Append("</td>")
            str.Append("</tr>")
            str.Append("<tr>")
            str.Append("<td style='text-align:justify; font-size:11px;'>")
            str.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Que, mediante Informe de fecha " + dt.Rows(0).Item("fechaInformeAsesor").ToString + ", el Asesor de Tesis")
            If dt.Rows(0).Item("docente3").ToString <> "" Then
                str.Append(dt.Rows(0).Item("grado3").ToString + " " + dt.Rows(0).Item("docente3").ToString)
            End If
            str.Append(", aprobó el informe de la tesis denominada " + dt.Rows(0).Item("Titulo_Tes").ToString + ";")
            str.Append("</td>")
            str.Append("</tr>")
            str.Append("<tr>")
            str.Append("<td style='text-align:justify; font-size:11px;'>")
            str.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Que, ")
            If dt.Rows.Count = 1 Then
                str.Append("el mencionado bachiller cumple")
            Else
                str.Append("los mencionados bachilleres cumplen")
            End If
            str.Append(" con los requisitos establecidos por el Reglamento de Tesis de la Universidad Católica Santo Toribio de Mogrovejo para la designación del jurado de tesis;")
            str.Append("</td>")
            str.Append("</tr>")
            str.Append("<tr>")
            str.Append("<td style='text-align:justify; font-size:11px;'>")
            str.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Que, por las razones expuestas, en concordancia con lo establecido por la Ley Universitaria Nª 30220, el Estatuto de la Universidad Católica Santo Toribio de Mogrovejo, y en el ejercicio de las atribuciones transferidas por el Reglamento de Tesis de la misma Universidad;")
            str.Append("</td>")
            str.Append("</tr>")
            str.Append("<tr>")
            str.Append("<td style='text-align:left; font-size:11px;'>")
            str.Append("<br><b>SE RESUELVE:</b><br>")
            str.Append("</td>")
            str.Append("</tr>")
            str.Append("</table>")

            str.Append("<table width='100%' style='text-align:justify; font-size:11px; font-family:Belwe Lt BT'>")
            str.Append("<tr><td width='17%'><b>Articulo 1°.-</b></td>")
            str.Append("<td colspan='2' width='83%'>")
            str.Append("Designar como miembros del Jurado de Tesis a los siguientes docentes:")
            str.Append("</td>")
            str.Append("</tr>")
            str.Append("<tr><td width='17%'></td>")
            str.Append("<td width='15%'>")
            str.Append("Presidente")
            str.Append("</td>")
            str.Append("<td width='68%'>")
            str.Append(": " + dt.Rows(0).Item("grado1").ToString + " " + dt.Rows(0).Item("docente1").ToString)
            str.Append("</td>")
            str.Append("</tr>")
            str.Append("<tr><td width='17%'></td>")
            str.Append("<td width='15%'>")
            str.Append("Secretario")
            str.Append("</td>")
            str.Append("<td width='68%'>")
            str.Append(": " + dt.Rows(0).Item("grado2").ToString + " " + dt.Rows(0).Item("docente2").ToString)
            str.Append("</td>")
            str.Append("</tr>")
            If dt.Rows(0).Item("docente3").ToString <> "" Then
                str.Append("<tr><td width='17%'></td>")
                str.Append("<td width='15%'>")
                str.Append("Vocal")
                str.Append("</td>")
                str.Append("<td width='68%'>")
                str.Append(": " + dt.Rows(0).Item("grado3").ToString + " " + dt.Rows(0).Item("docente3").ToString)
                str.Append("</td>")
                str.Append("</tr>")
            End If
            str.Append("</table>")

            str.Append("<table width='100%' style='text-align:justify; font-size:11px; font-family:Belwe Lt BT'>")
            str.Append("<tr><td width='17%' valign='top'><b>Articulo 2°.-</b></td>")
            str.Append("<td width='83%' style='text-align:justify;' >")
            str.Append("Entregar los ejemplares de la tesis, con las correspondiente ficha de observación, a los miembros del Jurado de Tesis para que procedan a emitir su opinión previa a la sustentación, de conformidad con lo establecido en el Reglamento de Tesis de la Universidad.")
            str.Append("</td>")
            str.Append("</tr>")
            str.Append("<tr><td width='17%' valign='top'><b>Articulo 3°.-</b></td>")
            str.Append("<td width='83%' style='text-align:justify;' >")
            str.Append("Comunicar a los miembros que al término de la revisión de la Tesis, alcancen la ficha de observación al coordinador de investigación de la respectiva Escuela Profesional.")
            str.Append("</td>")
            str.Append("</tr>")
            str.Append("<tr><td width='17%' valign='top'><b>Articulo 4°.-</b></td>")
            str.Append("<td width='83%' style='text-align:justify;' >")
            str.Append("Comunicar que una vez aprobada la tesis por los miembros del Jurado, el coordinador de investigación de la Escuela Profesional respectiva, programe la sustentación de la tesis, fijando fecha y hora para tal efecto.")
            str.Append("</td>")
            str.Append("</tr>")
            str.Append("<tr><td width='17%' valign='top'><b>Articulo 5°.-</b></td>")
            str.Append("<td width='83%' style='text-align:justify;' >")
            str.Append("Son funciones del Presidente del jurado: convocar a través del secretario a reuniones del jurado, indicar la mecánica a seguir en cada una de las evaluaciones y emitir al coordinador de investigación de la escuela los informes respectivos.")
            str.Append("</td>")
            str.Append("</tr>")
            str.Append("</table>")

            str.Append("<table width='100%' style='text-align:justify; font-size:11px; font-family:Belwe Lt BT'>")
            str.Append("<tr><td width='17%'></td>")
            str.Append("<td width='83%'>")
            str.Append("<b>Regístrese, comuníquese y archívese.</b>")
            str.Append("</td>")
            str.Append("</tr>")
            str.Append("</table>")

            str.Append("<br><br><br><br><br><br><br>")


            str.Append("<table width='100%' cellspacing='0' cellpadding='0' style='font-size:11px;'>")
            str.Append("<tr><td width='40%'></td><td width='60%' style='text-align:center;' ><b>" + dt.Rows(0).Item("Decano").ToString + "</b></td>")
            str.Append("</tr>")
            str.Append("<tr><td width='40%'></td><td width='60%' style='text-align:center;'><b>Decano</b></td>")
            str.Append("</tr>")
            str.Append("<tr><td width='40%'></td><td width='60%' style='text-align:center;'><b>" + dt.Rows(0).Item("Facultad").ToString + "</b></td>")
            str.Append("</tr>")
            str.Append("</table>")

            str.Append("<br><br><br><br><br><br><br>")


            str.Append("<table width='100%' cellspacing='0' cellpadding='0' style='font-size:11px;'>")
            str.Append("<tr><td width='60%' style='text-align:center;'><b>" + dt.Rows(0).Item("Secretario").ToString + "</b></td><td width='60%'></td>")
            str.Append("</tr>")
            str.Append("<tr><td width='60%' style='text-align:center;'><b>Secretario de Facultad</b></td><td width='40%'></td>")
            str.Append("</tr>")
            str.Append("<tr><td width='60%' style='text-align:center;'><b>" + dt.Rows(0).Item("Facultad").ToString + "</b></td><td width='40%'></td>")
            str.Append("</tr>")
            str.Append("</table>")




            Me.divCab.InnerHtml = str.ToString


            Dim documento As New Document(PageSize.A4, 60, 60, 40, 50)
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
            Response.AddHeader("content-disposition", "attachment; filename=Résolucion.pdf")
            Response.ContentType = "application/pdf"
            Response.Buffer = True
            Response.OutputStream.Write(ms.GetBuffer(), 0, ms.GetBuffer().Length)
            Response.OutputStream.Flush()
            Response.End()
            Response.Close()

        Catch ex As Exception
            Response.Write(ex.Message.ToString)
        End Try


    End Sub



    Private Sub GenerarActaSustentacion()
        Try
            Dim str As New StringBuilder
            Dim rootPath As String = Server.MapPath("~") ' Ruta para imagen de logo
            Dim carrera As String = ""
            Dim titulo As String = ""
            Dim alumno As String = ""
            Dim abreviatura As String = "S"
            Dim dt As New Data.DataTable

            Dim objc As New ClsGestionInvestigacion
            dt = objc.DatosActaSustentacion(Request.QueryString("t"), Request.QueryString("a"), abreviatura)

            str.Append("<div style='text-align:left'>")
            str.Append("<img height='90' width='110' src='" + rootPath + "/images/LogoOficial.png' style='text-align:left'>")
            str.Append("</div>")
            str.Append("<table width='100%' style='text-align:center; font-size:11px; font-family:Belwe Lt BT' padding='1'>")
            str.Append("<tr>")
            str.Append("<td style='text-align:center; font-size:14px;'>")
            str.Append("<b>ACTA DE SUSTENTACIÓN DE TESIS</b>")
            str.Append("<br>")
            str.Append("</td>")
            str.Append("</tr>")
            str.Append("<tr>")
            str.Append("<td style='text-align:justify; font-size:12px;'>")
            str.Append("Los Miembros del Jurado, designados por el Consejo de la Facultad de " + dt.Rows(0).Item("facultad").ToString + " mediante Resolución N° " + dt.Rows(0).Item("Resolucion").ToString + ", del " + dt.Rows(0).Item("FechaResolucion").ToString + ", ")
            str.Append("Presidente: " + dt.Rows(0).Item("grado1").ToString + " " + dt.Rows(0).Item("docente1").ToString + ", ")
            str.Append("Secretario: " + dt.Rows(0).Item("grado2").ToString + " " + dt.Rows(0).Item("docente2").ToString)
            If dt.Rows(0).Item("docente3").ToString <> "" Then
                str.Append(" y Vocal: " + dt.Rows(0).Item("grado3").ToString + " " + dt.Rows(0).Item("docente3").ToString)
            End If
            str.Append(", se reunieron el día " + dt.Rows(0).Item("FechaSustentacion").ToString + ", a las " + dt.Rows(0).Item("HoraSustentacion").ToString + " horas, en el aula " + dt.Rows(0).Item("ambiente").ToString + " de la Universidad Católica Santo Toribio de Mogrovejo, para recibir la sustentación de la tesis denominada: ")
            str.Append(dt.Rows(0).Item("Titulo_Tes").ToString)
            str.Append("<br><br>")
            str.Append("</td>")
            str.Append("</tr>")
            str.Append("<tr>")
            str.Append("<td style='text-align:justify; font-size:12px;'>")
            str.Append("Luego de la sustentación de la tesis y absueltas las preguntas del Jurado, así como del público asistente, el Jurado acordó dar el calificativo de ______________________ declarando a ")
            'Para un solo autor
            If dt.Rows.Count = 1 Then
                str.Append(dt.Rows(0).Item("pronombre").ToString + " bachiller en " + dt.Rows(0).Item("nombreOficial_cpf").ToString + " <b>" + dt.Rows(0).Item("Alumno").ToString + "</b>, " + dt.Rows(0).Item("apto").ToString + " para obtener el titulo correspondiente.")
            Else ' Varios autores
                str.Append("los bachilleres en " + dt.Rows(0).Item("nombreOficial_cpf").ToString + " <b>")
                For i As Integer = 0 To dt.Rows.Count - 1
                    If i = 0 Then
                        str.Append(dt.Rows(i).Item("Alumno").ToString)
                    ElseIf i > 0 And i < dt.Rows.Count - 1 Then
                        str.Append(", " + dt.Rows(i).Item("Alumno").ToString)
                    Else
                        str.Append(" y " + dt.Rows(i).Item("Alumno").ToString)
                    End If
                Next
                str.Append("</b>, aptos para obtener el titulo correspondiente.")
            End If
            str.Append("</td>")
            str.Append("</tr>")
            str.Append("<tr>")
            str.Append("<td style='text-align:justify; font-size:12px;'>")
            str.Append("Siendo las ___________________, se dio por concluido el acto.")
            str.Append("</td>")
            str.Append("</tr>")
            str.Append("</table>")

            str.Append("<br><br>")

            str.Append("<table width='100%' cellspacing='0' cellpadding='0' style='font-size:11px; '>")
            str.Append("<tr>")
            str.Append("<td style='text-align:right;'>Chiclayo, " + dt.Rows(0).Item("FechaSustentacion").ToString + "</td>")
            str.Append("</tr>")
            str.Append("</table>")


            str.Append("<br><br><br><br><br>")

            str.Append("<table width='100%' cellspacing='0' cellpadding='0' style='font-size:10px; font-weight:bold;'>")
            str.Append("<tr><td width='45%' style='text-align:center;'>" + dt.Rows(0).Item("grado1").ToString + " " + dt.Rows(0).Item("docente1").ToString + "</td><td width='10%' ></td><td width='45%' style='text-align:center;'>" + dt.Rows(0).Item("grado2").ToString + " " + dt.Rows(0).Item("docente2").ToString + "</td>")
            str.Append("</tr>")
            str.Append("<tr><td width='45%' style='text-align:center;'>" + dt.Rows(0).Item("tipo1").ToString + "</td><td width='10%' ></td><td width='45%' style='text-align:center;'>" + dt.Rows(0).Item("tipo2").ToString + "</td>")
            str.Append("</tr>")
            str.Append("</table>")

            str.Append("<br><br><br><br><br>")



            str.Append("<table width='100%' cellspacing='0' cellpadding='0' style='font-size:10px;font-weight:bold;'>")
            str.Append("<tr><td width='20%'></td><td width='60%' style='text-align:center;'>" + dt.Rows(0).Item("grado3").ToString + " " + dt.Rows(0).Item("docente3").ToString + "</td><td width='20%'></td>")
            str.Append("</tr>")
            str.Append("<tr><td width='20%'></td><td width='60%' style='text-align:center;'>" + dt.Rows(0).Item("tipo3").ToString + "</td><td width='20%'></td>")
            str.Append("</tr>")
            str.Append("</table>")




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
            Response.AddHeader("content-disposition", "attachment; filename=ActaSustentacion.pdf")
            Response.ContentType = "application/pdf"
            Response.Buffer = True
            Response.OutputStream.Write(ms.GetBuffer(), 0, ms.GetBuffer().Length)
            Response.OutputStream.Flush()
            Response.End()
            Response.Close()

        Catch ex As Exception
            Response.Write(ex.Message.ToString)
        End Try


    End Sub

End Class
