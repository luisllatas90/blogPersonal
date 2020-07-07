'For converting HTML TO PDF- START

Imports iTextSharp.text
Imports iTextSharp.text.html
Imports iTextSharp.text.pdf
Imports iTextSharp.text.xml
Imports iTextSharp.text.html.simpleparser
Imports System.IO
Imports System.util
Imports System.Text.RegularExpressions
Imports System.Globalization

Partial Class administrativo_doccartacategorizacion
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If IsPostBack = False Then
                Generarpdf()
            End If

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try

    End Sub

    Private Sub Generarpdf()

        Dim fechaactual As Date = Now
        Dim alumnosArray() As String
        Dim codigo_alu As Integer
        Dim apellidos_alu As String
        Dim nombres_alu As String
        Dim codigouniver_alu As String
        Dim nombre_cpf As String
        Dim credito_alu As Decimal
        Dim password_alu As String
        Dim textohtml As String        
        Dim objcx As New ClsConectarDatos
        Dim tbl As Data.DataTable
        Dim mes As String
        Dim dia As String
        Dim año As String
        Dim cicloingreso As String
        Dim codigo_cpf As Integer
        Dim codigo_test As Integer

        Dim documento As New Document(PageSize.A4, 85, 85, 67, 50)
        Dim ms As New MemoryStream
        Dim writer As PdfWriter
        Dim parrafo As New Paragraph ' Declaracion de un parrafo
        Dim parrafo2 As New Paragraph ' Declaracion de un parrafo
        'Dim cb As iTextSharp.text.pdf.PdfContentByte

        objcx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        objcx.AbrirConexion()
        alumnosArray = Split(Request.QueryString("alumnosArray"), ",")
        'fechaactual = fechaactual.ToString("dd 'de' MMMM 'de' yyyy") + "."
        mes = fechaactual.ToString("MMMM")
        dia = fechaactual.ToString("dd")
        año = fechaactual.ToString("yyyy")

        writer = PdfWriter.GetInstance(documento, ms)
        codigo_test = Request.QueryString("test")
        Response.Write("<script>alert('" & codigo_test & "')</script>")
        'Obtener data
        '------------------------------------------------------------------------------
        For i As Integer = 0 To UBound(alumnosArray)
            codigo_alu = Mid(alumnosArray(i), 3, Len(alumnosArray(i) - 2))
            tbl = objcx.TraerDataTable("EPRE_ListarPostulantes", "%", 0, 0, "%", "%", "%", "%", codigo_test, codigo_alu, "%", "%", 0, "")

            'Obj.CerrarConexion()
            'Obj = Nothing

            If tbl.Rows.Count > 0 Then
                apellidos_alu = tbl.Rows(0).Item("apellidos")
                nombres_alu = tbl.Rows(0).Item("nombres")
                codigouniver_alu = tbl.Rows(0).Item("CodUniversitario")
                nombre_cpf = tbl.Rows(0).Item("carrera")
                'credito_alu = Replace(tbl.Rows(0).Item("Categorizacion"), ",", ".")                
                credito_alu = Format((tbl.Rows(0).Item("precioCreditoAct_Alu")) * 5, "##,##0.00")
                password_alu = tbl.Rows(0).Item("password_Alu")
                cicloingreso = tbl.Rows(0).Item("CicloIngreso")
                codigo_cpf = tbl.Rows(0).Item("tempcodigo_cpf")
                codigo_test = tbl.Rows(0).Item("codigo_test")

                textohtml = ""
                textohtml = textohtml & "<br /><br />"
                'textohtml = textohtml & "<p style='text-align:right'>Chiclayo, " & fechaactual & "</p>"
                textohtml = textohtml & "<p style='text-align:right'>Chiclayo, " & dia & " de " & mes & " de " & año & "</p>"
                textohtml = textohtml & "<br /><br /><br /><br />"
                textohtml = textohtml & "<p>Sr(a).</p>"
                If (codigo_test = 2) Then
                    textohtml = textohtml & "<p><b>" & apellidos_alu & "</b></p>"
                    textohtml = textohtml & "<p>Ciudad.-</p>"
                    textohtml = textohtml & "<br /><br /><br />"
                    textohtml = textohtml & "<p style='text-align:justify'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;En nombre  de la Universidad Católica Santo Toribio de Mogrovejo, les expreso mi cordial saludo y felicitación por el ingreso de su hijo (a) <b>" & nombres_alu & "</b> a la Escuela Profesional de <b>" & nombre_cpf & "</b>.<p/><br />"
                Else
                    textohtml = textohtml & "<p><b>" & apellidos_alu & " " & nombres_alu & "</b></p>"
                    textohtml = textohtml & "<p>Ciudad.-</p>"
                    textohtml = textohtml & "<br /><br /><br />"
                    textohtml = textohtml & "<p style='text-align:justify'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;En nombre  de la Universidad Católica Santo Toribio de Mogrovejo, les expreso mi cordial saludo y felicitación por su ingreso a la Escuela Profesional de <b>" & nombre_cpf & "</b>.<p/><br />"
                End If                

                If tbl.Rows(0).Item("codigo_min") = 33 Then 'BECA 18
                    textohtml = textohtml & "<p style='text-align:justify'>"
                    textohtml = textohtml & "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Su hijo (a) podrá verificar sus asignaturas matriculadas a través de nuestra página web: <span style='text-decoration: underline'>www.usat.edu.pe/campusvirtual</span>, ingresando su código universitario <b>" & codigouniver_alu & "</b>, cuya clave es <b>" & password_alu & "</b>. Asimismo, adjunto encontrará su carné y el Reglamento de  Pensiones " & cicloingreso & " para su  atenta lectura. "
                    textohtml = textohtml & "</p><br />"
                    textohtml = textohtml & "<p style='text-align:justify'>"
                    textohtml = textohtml & "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Reciban nuestro agradecimiento por la confianza depositada en nuestra Universidad."
                    textohtml = textohtml & "</p><br />"
                    textohtml = textohtml & "<p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Sin otro particular, quedo de ustedes.</p><br />"
                    textohtml = textohtml & "<p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Atentamente,</p><br />"

                ElseIf (codigo_cpf <> 24 And codigo_cpf <> 31) Then
                    textohtml = textohtml & "<p style='text-align:justify'>"
                    If (codigo_test = 2) Then
                        textohtml = textohtml & "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;En la evaluación de acuerdo al colegio de procedencia la Comisión de Pensiones ha determinado asignarle un <b> costo de crédito por ciclo académico de S/. " & credito_alu & "  soles. </b> La pensión académica está en función del costo por crédito asignado y de la carga académica; la misma que podrá ser cancelada en 5 cuotas los días 30 de cada mes. Esta categorización será supervisada periódicamente y podrá suspenderse o extinguirse de conformidad con el ítem IV (k) del  Reglamento de  Pensiones " & cicloingreso & "."
                        textohtml = textohtml & "</p><br />"
                        textohtml = textohtml & "<p style='text-align:justify'>"
                        textohtml = textohtml & "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Su hijo (a) podrá verificar sus asignaturas matriculadas a través de nuestra página web: <span style='text-decoration: underline'>www.usat.edu.pe/campusvirtual</span>, ingresando su código universitario <b>" & codigouniver_alu & "</b>, cuya clave es <b>" & password_alu & "</b>. Asimismo, adjunto encontrará su carné y el Reglamento de  Pensiones " & cicloingreso & " para su  atenta lectura. "
                        textohtml = textohtml & "</p><br />"
                        textohtml = textohtml & "<p style='text-align:justify'>"
                        textohtml = textohtml & "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Reciban nuestro agradecimiento por la confianza depositada en nuestra Universidad."
                        textohtml = textohtml & "</p><br />"
                        textohtml = textohtml & "<p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Sin otro particular, quedo de ustedes.</p><br />"
                        textohtml = textohtml & "<p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Atentamente,</p><br />"
                    Else    'GO
                        textohtml = textohtml & "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;En la evaluación de acuerdo a los <b>Programa GO</b> la Comisión de Pensiones ha determinado asignarle un <b> costo de crédito por ciclo académico de S/. " & credito_alu & "  soles. </b> La pensión académica está en función del costo por crédito asignado y de la carga académica; la misma que podrá ser cancelada en 5 cuotas los días 30 de cada mes. "
                        textohtml = textohtml & "</p><br />"
                        textohtml = textohtml & "<p style='text-align:justify'>"
                        textohtml = textohtml & "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Usted podrá verificar sus asignaturas matriculadas a través de nuestra página web: <span style='text-decoration: underline'>www.usat.edu.pe/campusvirtual</span>, ingresando su código universitario <b>" & codigouniver_alu & "</b>, cuya clave es <b>" & password_alu & "</b>. Asimismo, adjunto encontrará su carné y el Reglamento de  Pensiones " & cicloingreso & " para su  atenta lectura. "
                        textohtml = textohtml & "</p><br />"
                        textohtml = textohtml & "<p style='text-align:justify'>"
                        textohtml = textohtml & "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Reciban nuestro agradecimiento por la confianza depositada en nuestra Universidad."
                        textohtml = textohtml & "</p><br />"
                        textohtml = textohtml & "<p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Sin otro particular, quedo de usted.</p><br />"
                        textohtml = textohtml & "<p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Atentamente,</p><br />"
                    End If
                    
                    
                Else
                    textohtml = textohtml & "<p style='text-align:justify'>"
                    'textohtml = textohtml & "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Su hijo (a) podrá realizar su matrícula a través de nuestra página web: <span style='text-decoration: underline'>www.usat.edu.pe/campusvirtual</span>, ingresando su código universitario <b>" & codigouniver_alu & "</b>, cuya clave es <b>" & password_alu & "</b>. Asimismo, adjunto encontrará el Reglamento de  Pensiones " & cicloingreso & " para su  atenta lectura. "
                    textohtml = textohtml & "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Su hijo (a) podrá verificar sus asignaturas matriculadas a través de nuestra página web: <span style='text-decoration: underline'>www.usat.edu.pe/campusvirtual</span>, ingresando su código universitario <b>" & codigouniver_alu & "</b>, cuya clave es <b>" & password_alu & "</b>. Asimismo, adjunto encontrará su carné y el Reglamento de  Pensiones " & cicloingreso & " para su  atenta lectura. "
                    textohtml = textohtml & "</p><br />"
                    textohtml = textohtml & "<p style='text-align:justify'>"
                    textohtml = textohtml & "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Reciban nuestro agradecimiento por la confianza depositada en nuestra Universidad."
                    textohtml = textohtml & "</p><br />"
                    textohtml = textohtml & "<p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Sin otro particular, quedo de ustedes.</p><br />"
                    textohtml = textohtml & "<p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Atentamente,</p>"
                End If

               

                '------------------------------------------------------------------------------        
                'PDF
                '------------------------------------------------------------------------------                   
                'Paso a html
                Dim se As New StringReader(textohtml)
                Dim obj As New HTMLWorker(documento)

                'Registrar Fuente
                Dim rootPath As String = Server.MapPath("~")
                Dim customfont As BaseFont
                customfont = BaseFont.CreateFont(rootPath & "\administrativo\Belwel.ttf", BaseFont.CP1252, BaseFont.EMBEDDED)
                iTextSharp.text.FontFactory.Register(rootPath & "\administrativo\Belwel.ttf", "Belwe")
                'Dim font As New Font(customfont, 12)
                Dim fontbold As New Font(customfont, 12.5, Font.BOLD)

                'Seteo estilo para html
                Dim style As New StyleSheet
                style.LoadTagStyle(HtmlTags.P, HtmlTags.SIZE, "12.5pt")
                style.LoadTagStyle(HtmlTags.P, HtmlTags.FACE, "Belwe") 'interlineado
                style.LoadTagStyle(HtmlTags.P, HtmlTags.LEADING, "15")
                obj.SetStyleSheet(style)

                'Defino parrafos para la firma
                parrafo.Alignment = Element.ALIGN_CENTER
                parrafo2.Alignment = Element.ALIGN_CENTER
                parrafo.Font = fontbold
                parrafo2.Font = fontbold

                'parrafo.Font = FontFactory.GetFont("Belwe", 13, iTextSharp.text.Font.BOLD)                
                'parrafo.IndentationLeft
                parrafo.Add("Luis Enrique Bermudez Malca") 'Texto que se insertara
                parrafo2.Add("Administrador General") 'Texto que se insertara

                'Apertura del documento
                documento.Open()
                'cb = writer.DirectContent

                'Seteamos el tipo de letra y el tamaño.
                'cb.SetFontAndSize(customfont, 12)

                'cb.ShowTextAligned(iTextSharp.text.pdf.PdfContentByte.ALIGN_LEFT, "Ejemplo basico con iTextSharp", 200, iTextSharp.text.PageSize.A4.Height - 50, 0)

                Dim oImagen As iTextSharp.text.Image

                'documento.Add(New Paragraph(s, font))

                oImagen = iTextSharp.text.Image.GetInstance(rootPath & "\administrativo\firmaBermudez.jpg")
                oImagen.ScaleAbsoluteWidth(150)
                oImagen.ScaleAbsoluteHeight(75)
                oImagen.Alignment = Element.ALIGN_CENTER
                obj.Parse(se)
                documento.Add(oImagen)
                documento.Add(parrafo)
                documento.Add(parrafo2)
                parrafo.Clear()
                parrafo2.Clear()

                'Abrimos una nueva página
                documento.NewPage()

                'Iniciamos el flujo de bytes.
                'cb.BeginText()
                '------------------------------------------------------------------------------
            Else

            End If

        Next

        '------------------------------------------------------------------------------
        'Mostrar pdf
        '------------------------------------------------------------------------------
        'Fin del flujo de bytes.
        'cb.EndText()

        'Forzamos vaciamiento del buffer.
        'writer.Flush()

        'Cerramos el documento.
        documento.Close()

        Response.Clear()
        Response.AddHeader("content-disposition", "attachment; filename=carta.pdf")

        Response.ContentType = "application/pdf"
        Response.Buffer = True
        Response.OutputStream.Write(ms.GetBuffer(), 0, ms.GetBuffer().Length)
        Response.OutputStream.Flush()
        Response.End()
        '------------------------------------------------------------------------------
        'ConvertHtmlStringToPDF(textohtml)
    End Sub

    'Public Sub ConvertHtmlStringToPDF(ByVal htmlDisplayText As String)

    '    'Dim styles As New StyleSheet
    '    ''styles.LoadTagStyle(HtmlTags.H1, HtmlTags.FONTSIZE, "16")
    '    ''styles.LoadTagStyle(HtmlTags.P, HtmlTags.FONTSIZE, "10")
    '    ''styles.LoadTagStyle(HtmlTags.P, HtmlTags.COLOR, "#ff0000")
    '    'styles.LoadTagStyle(HtmlTags.UL, HtmlTags.INDENT, "15")
    '    'styles.LoadTagStyle(HtmlTags.UL, HtmlTags.ALIGN, "Justify")
    '    ''styles.LoadTagStyle(HtmlTags.LI, HtmlTags.LEADING, "16")

    '    'Dim htmlDisplayText As String
    '    Dim documento As New Document
    '    Dim ms As New MemoryStream
    '    Dim writer As PdfWriter

    '    writer = PdfWriter.GetInstance(documento, ms)


    '    Dim se As New StringReader(htmlDisplayText)
    '    Dim obj As New HTMLWorker(documento)
    '    documento.Open()

    '    Dim oImagen As iTextSharp.text.Image
    '    Dim coordenadaX As Single = 200
    '    Dim coordenadaY As Single = 240
    '    Dim rootPath As String = Server.MapPath("~")

    '    oImagen = iTextSharp.text.Image.GetInstance(rootPath & "\administrativo\firma.jpg")

    '    oImagen.SetAbsolutePosition(coordenadaX, coordenadaY)
    '    'oImagen.ScaleAbsoluteWidth(30)
    '    'oImagen.ScaleAbsoluteHeight(10)
    '    documento.Add(oImagen)

    '    obj.Parse(se)        

    '    documento.Close()

    '    Response.Clear()
    '    Response.AddHeader("content-disposition", "attachment; filename=report.pdf")

    '    Response.ContentType = "application/pdf"
    '    Response.Buffer = True
    '    Response.OutputStream.Write(ms.GetBuffer(), 0, ms.GetBuffer().Length)
    '    Response.OutputStream.Flush()
    '    Response.End()
    'End Sub


End Class
