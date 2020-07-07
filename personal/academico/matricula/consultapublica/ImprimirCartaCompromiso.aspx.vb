Imports System
Imports iTextSharp.text
Imports iTextSharp.text.html.simpleparser
Imports iTextSharp.text.pdf
Imports System.Text
Imports System.IO

Partial Class academico_matricula_consultapublica_ImprimirCartaCompromiso
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            ' Response.Write("ALUMNO ID=" & Request.QueryString("alumno").ToString())
            'If Request.QueryString("ciclo") IsNot Nothing Then
            'Response.Write("CICLO=" & Request.QueryString("ciclo").ToString())
            'End If

            Dim codigo_alu As Integer = CInt(Request.QueryString("alumno").ToString())
            Dim codigo_cac As Integer = CInt(Request.QueryString("ciclo").ToString())
            Try
                Dim dt As New Data.DataTable
                Dim obj As New ClsConectarDatos
                obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
                obj.AbrirConexion()
                dt = obj.TraerDataTable("ConsultarCicloAcademico", "CO", codigo_cac)
                obj.CerrarConexion()
                obj = Nothing
                If dt.Rows(0).Item("tipo_Cac").ToString = "N" Then
                    GenerarCarta(codigo_alu, codigo_cac)
                Else
                    GenerarCartaVerano(codigo_alu, codigo_cac)
                End If


            Catch ex As Exception

            End Try





        End If

    End Sub

    Public Sub GenerarCarta(ByVal codigo_alu As Integer, ByVal codigo_cac As Integer)
        Try
            Dim nombre As String = ""
            Dim dni As String = ""
            Dim direccion As String = ""
            Dim celular As String = ""
            Dim escuela As String = ""
            Dim codigouniver As String = ""


            Dim str As New StringBuilder


            Dim dt As New Data.DataTable
            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()

            dt = obj.TraerDataTable("ALU_DatosAlumno", codigo_alu)

            str.Append("<table width='90%' style='text-align:justify'>")
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then


                nombre = dt.Rows(0).Item("nombres_Alu").ToString & " " & dt.Rows(0).Item("apellidoPat_Alu").ToString & " " & dt.Rows(0).Item("apellidoMat_Alu").ToString

                dni = dt.Rows(0).Item("nroDocIdent_Alu").ToString
                direccion = dt.Rows(0).Item("direccion_Dal").ToString
                celular = dt.Rows(0).Item("telefonoMovil_Dal").ToString
                escuela = dt.Rows(0).Item("nombre_Cpf").ToString
                codigouniver = dt.Rows(0).Item("codigoUniver_Alu").ToString



                str.Append("<tr>")
                str.Append("<td style='text-align:center'>")
                'str.Append("<img height='100' width='90' src='" & rootPath & "\assets\images\logousat.PNG'>")

                str.Append("</td>")
                str.Append("<tr>")

                str.Append("<tr>")
                str.Append("<td style='text-align:center'>")
                str.Append("<b>CARTA COMPROMISO</b>")
                str.Append("<td>")
                str.Append("</td>")
                str.Append("<tr>")


                str.Append("<tr>")
                str.Append("<td style='text-align:justify'>")
                str.Append("<p style='text-align:justify'>Yo, <b>" & nombre & "</b>, identificado con DNI N° " & dni & ", domiciliado en <b>" & direccion & "</b>, teléfono celular <b>" & celular & "</b>, estudiante de la carrera profesional de <b>" & escuela & "</b> de la Universidad Católica Santo Toribio de Mogrovejo, con código universitario N° " & codigouniver & "; <b>ME COMPROMETO A:</b></p>")
                str.Append("</td>")
                str.Append("<tr>")

                str.Append("<tr>")
                str.Append("<td style='text-align:justify'>")
                str.Append("<p>Cursar y aprobar el(los) curso(s):</p>")
                str.Append("</td>")
                str.Append("<tr>")

                str.Append("<tr>")
                str.Append("<td style='text-align:center'>")

                ' 
                str.Append("<table width='100%' style='border: 1px solid black;border-collapse: collapse;'>")
                str.Append("<thead>")
                str.Append("<tr style='background-color:#d3d3d3'>")
                str.Append("<th width='50%' style='text-align:left; width:50%; border: 1px solid black;border-collapse: collapse;'>")
                str.Append("Curso")
                str.Append("</th>")
                str.Append("<th width='20%' style='text-align:left; width:20%; border: 1px solid black;border-collapse: collapse;'>")
                str.Append("Ciclo")
                str.Append("</th>")
                str.Append("<th width='30%' style='text-align:left; width:30%; border: 1px solid black;border-collapse: collapse;'>")
                str.Append("Veces desaprobadas")
                str.Append("</th>")
                str.Append("</tr>")
                str.Append("</thead>")
                str.Append("<tbody>")
                str.Append(ListarCursos(codigo_alu, codigo_cac))
                str.Append("</tbody>")
                str.Append("</table>")

                str.Append("</td>")
                str.Append("</tr>")


                str.Append("<tr>")
                str.Append("<td style='text-align:justify'>")
                str.Append("<p style='text-align:justify'><br><br><br><br>Programado(s) en el semestre 2018-II; caso contrario, acepto la medida de separación definitiva y automática de la universidad establecida en el artículo 44° del Reglamento de Estudios de Pregrado y el artículo 102 de la Ley Universitaria 30220.</p>")
                str.Append("</td>")
                str.Append("<tr>")

                str.Append("<tr>")
                str.Append("<td style='text-align:justify'>")
                str.Append("<p>Suscribo el presente documento en señal de conformidad, a los " & CDate(Date.Today).ToString("dd").ToString & ", días del mes de " & CDate(Date.Today).ToString("MMMM").ToString & " del año " & Date.Today.Year.ToString & "</p>")
                str.Append("</td>")
                str.Append("<tr>")


                str.Append("<tr>")
                str.Append("<td style='text-align:justify'><br><br><br><br><br>")

                str.Append("</td>")
                str.Append("<tr>")

                str.Append("<tr>")
                str.Append("<td style='text-align:justify'>")

                str.Append("<table width='100%'>")
                str.Append("<tr>")
                str.Append("<td  style='text-align:justify;width:80%;'>")
                str.Append("<center>")
                str.Append("<p>___________________________</p>")
                str.Append("</center>")
                str.Append("<center>")
                str.Append("FIRMA<br>DNI:")
                str.Append("<center>")
                str.Append("</td>")
                str.Append("<td style='text-align:right;width:20%'>")

                str.Append("_________________<br>")
                str.Append("HUELLA DIGITAL<br><br>")
                str.Append("<center>")
                str.Append("</td>")
                str.Append("<tr>")
                str.Append("</table>")
                str.Append("</td>")
                str.Append("<tr>")


                str.Append("<tr>")
                str.Append("<td style='text-align:justify;'>")
                str.Append("<p style='text-align:justify'><b>Una vez iniciada las clases debe presentar la presente carta impresa y firmada a Dirección de Escuela. La fecha límite de presentacion es hasta el 24 de agosto 2018</b></p>")
                str.Append("</td>")
                str.Append("<tr>")

            End If
            str.Append("</table>")


            Me.divCab.InnerHtml = str.ToString
            obj.CerrarConexion()
            obj = Nothing

            descargar(codigouniver)
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub
    Public Sub GenerarCartaVerano(ByVal codigo_alu As Integer, ByVal codigo_cac As Integer)
        Try
            Dim nombre As String = ""
            Dim dni As String = ""
            Dim direccion As String = ""
            Dim celular As String = ""
            Dim escuela As String = ""
            Dim codigouniver As String = ""


            Dim str As New StringBuilder


            Dim dt As New Data.DataTable
            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()

            dt = obj.TraerDataTable("ALU_DatosAlumno", codigo_alu)

            str.Append("<table width='90%' style='text-align:justify'>")
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then


                nombre = dt.Rows(0).Item("nombres_Alu").ToString & " " & dt.Rows(0).Item("apellidoPat_Alu").ToString & " " & dt.Rows(0).Item("apellidoMat_Alu").ToString

                dni = dt.Rows(0).Item("nroDocIdent_Alu").ToString
                direccion = dt.Rows(0).Item("direccion_Dal").ToString
                celular = dt.Rows(0).Item("telefonoMovil_Dal").ToString
                escuela = dt.Rows(0).Item("nombre_Cpf").ToString
                codigouniver = dt.Rows(0).Item("codigoUniver_Alu").ToString



                str.Append("<tr>")
                str.Append("<td style='text-align:center'>")
                'str.Append("<img height='100' width='90' src='" & rootPath & "\assets\images\logousat.PNG'>")

                str.Append("</td>")
                str.Append("<tr>")

                str.Append("<tr>")
                str.Append("<td style='text-align:center'>")
                str.Append("<b>CARTA COMPROMISO</b>")
                str.Append("<td>")
                str.Append("</td>")
                str.Append("<tr>")


                str.Append("<tr>")
                str.Append("<td style='text-align:justify'>")
                str.Append("<p style='text-align:justify'>Yo, <b>" & nombre & "</b>, identificado con DNI N° " & dni & ", domiciliado en <b>" & direccion & "</b>, teléfono celular <b>" & celular & "</b>, estudiante de la carrera profesional de <b>" & escuela & "</b> de la Universidad Católica Santo Toribio de Mogrovejo, con código universitario N° " & codigouniver & "; <b>ME COMPROMETO A:</b></p>")
                str.Append("</td>")
                str.Append("<tr>")

                str.Append("<tr>")
                str.Append("<td style='text-align:justify'>")
                str.Append("<p>Cursar y aprobar el(los) curso(s):</p>")
                str.Append("</td>")
                str.Append("<tr>")

                str.Append("<tr>")
                str.Append("<td style='text-align:center'>")

                ' 
                str.Append("<table width='100%' style='border: 1px solid black;border-collapse: collapse;'>")
                str.Append("<thead>")
                str.Append("<tr style='background-color:#d3d3d3'>")
                str.Append("<th width='60%' style='text-align:left; width:60%; border: 1px solid black;border-collapse: collapse;'>")
                str.Append("Curso")
                str.Append("</th>")
                str.Append("<th width='20%' style='text-align:left; width:20%; border: 1px solid black;border-collapse: collapse;'>")
                str.Append("Ciclo")
                str.Append("</th>")
                str.Append("<th width='20%' style='text-align:left; width:20%; border: 1px solid black;border-collapse: collapse;'>")
                str.Append("Veces desaprobadas")
                str.Append("</th>")
                str.Append("</tr>")
                str.Append("</thead>")
                str.Append("<tbody>")
                str.Append(ListarCursos(codigo_alu, codigo_cac))
                str.Append("</tbody>")
                str.Append("</table>")

                str.Append("</td>")
                str.Append("</tr>")


                str.Append("<tr>")
                str.Append("<td style='text-align:justify'>")
                str.Append("<p style='text-align:justify'><br><br><br><br>Programado(s) para enero y febrero de 2018; caso contrario, acepto la medida de separación definitiva y automática de la universidad establecida en el artículo 47° del Reglamento de Estudios de Pregrado y el artículo 102 de la Ley Universitaria 30220.</p>")
                str.Append("</td>")
                str.Append("<tr>")

                str.Append("<tr>")
                str.Append("<td style='text-align:justify'>")
                str.Append("<p>Suscribo el presente documento en señal de conformidad, a los " & CDate(Date.Today).ToString("dd").ToString & ", días del mes de " & CDate(Date.Today).ToString("MMMM").ToString & " del año " & Date.Today.Year.ToString & "</p>")
                str.Append("</td>")
                str.Append("<tr>")


                str.Append("<tr>")
                str.Append("<td style='text-align:justify'><br><br><br><br><br>")

                str.Append("</td>")
                str.Append("<tr>")

                str.Append("<tr>")
                str.Append("<td style='text-align:justify'>")

                str.Append("<table width='100%'>")
                str.Append("<tr>")
                str.Append("<td  style='text-align:justify;width:80%;'>")
                str.Append("<center>")
                str.Append("<p>___________________________</p>")
                str.Append("</center>")
                str.Append("<center>")
                str.Append("FIRMA<br>DNI:")
                str.Append("<center>")
                str.Append("</td>")
                str.Append("<td style='text-align:right;width:20%'>")

                str.Append("_________________<br>")
                str.Append("HUELLA DIGITAL<br><br>")
                str.Append("<center>")
                str.Append("</td>")
                str.Append("<tr>")
                str.Append("</table>")
                str.Append("</td>")
                str.Append("<tr>")


                str.Append("<tr>")
                str.Append("<td style='text-align:justify;'>")
                str.Append("<p style='text-align:justify'><b>Una vez iniciada las clases debe presentar la presente carta impresa y firmada a Dirección de Escuela. La fecha límite de presentacion es hasta el 15 de enero 2018</b></p>")
                str.Append("</td>")
                str.Append("<tr>")

            End If
            str.Append("</table>")


            Me.divCab.InnerHtml = str.ToString
            obj.CerrarConexion()
            obj = Nothing

            descargar(codigouniver)
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Function ListarCursos(ByVal codigo_alu As Integer, ByVal codigo_cac As Integer) As String
        Try

            Dim strTbody As New StringBuilder
            Dim i As Integer = 0

            Dim dt As New Data.DataTable
            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()

            dt = obj.TraerDataTable("consultarmatriculav2", codigo_cac, codigo_alu)



            If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then

                For i = 0 To dt.Rows.Count - 1
                    If i < (dt.Rows.Count - 1) Then
                        If dt.Rows(i).Item("codigo_cup") <> dt.Rows(i + 1).Item("codigo_cup") Then
                            If dt.Rows(i).Item("vecesCurso_Dma") >= 3 Then


                                strTbody.Append("<tr>")
                                strTbody.Append("<td width='50%' style='text-align:left; width:50%; border: 1px solid black;border-collapse: collapse;'>")
                                strTbody.Append(dt.Rows(i).Item("nombre_Cur").ToString)
                                strTbody.Append("</td>")
                                strTbody.Append("<td  width='20%'  style='text-align:left;width:20%;  border: 1px solid black;border-collapse: collapse;'>")
                                strTbody.Append(dt.Rows(i).Item("ciclo_Cur").ToString)
                                strTbody.Append("</td>")
                                strTbody.Append("<td  width='30%'  style='text-align:left;width:30%;  border: 1px solid black;border-collapse: collapse;'>")
                                strTbody.Append(dt.Rows(i).Item("vecesCurso_Dma").ToString)
                                strTbody.Append("</td>")
                                strTbody.Append("</tr>")

                            End If
                        End If

                    End If
                Next

                If dt.Rows(dt.Rows.Count - 1).Item("vecesCurso_Dma") >= 3 Then
                    strTbody.Append("<tr>")
                    strTbody.Append("<td width='50%'  style='text-align:left;width:50%; border: 1px solid black;border-collapse: collapse;'>")
                    strTbody.Append(dt.Rows(dt.Rows.Count - 1).Item("nombre_Cur").ToString)
                    strTbody.Append("</td>")
                    strTbody.Append("<td width='20%'style='text-align:left;width:20%; border: 1px solid black;border-collapse: collapse;'>")
                    strTbody.Append(dt.Rows(dt.Rows.Count - 1).Item("ciclo_Cur").ToString)
                    strTbody.Append("</td>")
                    strTbody.Append("<td width='30%' style='text-align:left;width:30%; border: 1px solid black;border-collapse: collapse;'>")
                    strTbody.Append(dt.Rows(dt.Rows.Count - 1).Item("vecesCurso_Dma").ToString)
                    strTbody.Append("</td>")
                    strTbody.Append("</tr>")
                End If

            End If
            obj.CerrarConexion()
            obj = Nothing
            Return strTbody.ToString
        Catch ex As Exception
            Return ex.Message
        End Try

    End Function

    Sub descargar(ByVal codigouniver As String)
        Try
            Dim documento As New Document(PageSize.A4, 85, 85, 67, 50)
            Dim ms As New MemoryStream
            Dim writer As PdfWriter
            writer = PdfWriter.GetInstance(documento, ms)
            Dim textohtml As String = divCab.InnerHtml.ToString()

            Response.Write(textohtml)
            divCab.InnerHtml = ""
            'textohtml = "X"
            Dim se As New StringReader(textohtml)
            Dim obj As New HTMLWorker(documento)

            Dim rootPath As String = Server.MapPath("~")
            Dim customfont As BaseFont

            Dim style As New StyleSheet
            Dim anio As String = Date.Now.Year.ToString()
            documento.Open()
            obj.Parse(se)


            documento.Close()
            Response.Clear()
            Response.AddHeader("content-disposition", "attachment; filename=MATRICULACARTACOMPROMISO-" & anio & "-" & codigouniver.ToString & ".pdf")
            Response.ContentType = "application/pdf"
            Response.Buffer = True
            Response.OutputStream.Write(ms.GetBuffer(), 0, ms.GetBuffer().Length)
            Response.OutputStream.Flush()
            Response.End()
            Response.Close()

            'documento.Close()

            'Response.Clear()
            'Response.ContentType = "application/pdf"
            'Response.AddHeader("Content-Disposition", "attachment; filename=Employee.pdf")
            'Response.ContentType = "application/pdf"
            'Response.Buffer = True
            'Response.Cache.SetCacheability(HttpCacheability.NoCache)
            '' Response.BinaryWrite(bytes)
            'Response.[End]()
            'Response.Close()

        Catch ex As Exception
            Response.Write(ex.Message & " - " & ex.StackTrace)
        End Try

    End Sub

End Class
