Imports iTextSharp.text
Imports System.IO
Imports iTextSharp.text.pdf

Partial Class administrativo_gestion_educativa_frmFichaInscripcionPosgradoPDF
    Inherits System.Web.UI.Page

#Region "Variables"
    Private mo_RepoAdmision As New ClsAdmision

    Private Shared mo_FontTitulo As Font = FontFactory.GetFont("Arial", 13)
    Private Shared mo_Font As Font = FontFactory.GetFont("Arial", 9)
    Private Shared mo_FontB As Font = FontFactory.GetFont("Arial", 9, Font.BOLD)
    Private Shared mo_FontSmall As Font = FontFactory.GetFont("Arial", 8)
    Private Shared mo_FontSmallB As Font = FontFactory.GetFont("Arial", 8, Font.BOLD)
    Private Shared mo_FontMini As Font = FontFactory.GetFont("Arial", 6)
    Private Shared mo_FontMiniB As Font = FontFactory.GetFont("Arial", 6, Font.BOLD)

    Private Shared ms_ColorGranate As String = "#9E0817"
    Private Shared ms_ColorGris As String = "#D8D8D8"
#End Region

#Region "Eventos"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                If Request.Cookies("fileDownload") Is Nothing Then
                    Dim aCookie As New HttpCookie("fileDownload")
                    aCookie.Value = "true"
                    aCookie.Path = "/"
                    Response.Cookies.Add(aCookie)
                End If

                Dim ls_CodigoAlu As String = ""
                If HttpContext.Current.Request.HttpMethod = "POST" Then
                    ls_CodigoAlu = Request.Form("alu")
                Else
                    ls_CodigoAlu = Request.QueryString("alu")
                End If
                GenerarFichaInscripcion(ls_CodigoAlu)
            End If

        Catch ex As Exception
            errorMenssage.InnerHtml = ex.Message
        End Try
    End Sub
#End Region

#Region "Métodos"
    Private Sub GenerarFichaInscripcion(ByVal ls_codigoAlu As String)
        Try
            Dim dtbDatos As Data.DataTable = mo_RepoAdmision.ObtenerDatosInscripcion(ls_codigoAlu, "F")
            If dtbDatos.Rows.Count = 0 Then
                With respuestaPostback
                    .Attributes.Item("data-rpta") = "-1"
                    .Attributes.Item("data-msg") = "No se han encontrado datos para mostrar en la ficha"
                End With
                Exit Sub
            End If
            Dim lo_DataRow As Data.DataRow = dtbDatos.Rows(0)

            Dim ms As New MemoryStream
            Dim writer As PdfWriter

            Dim doc As New Document(PageSize.A4, 75, 85, 67, 50)
            doc.SetMargins(20.0F, 20.0F, 20.0F, 90.0F)
            writer = PdfWriter.GetInstance(doc, ms)
            writer.PageEvent = New PDFFooter()

            doc.Open()

            Dim tblLayout As New PdfPTable(100)
            With tblLayout
                .WidthPercentage = 100
            End With

            Dim cellEmpty As New PdfPCell()
            cellEmpty.Colspan = 100
            cellEmpty.Border = Rectangle.NO_BORDER

            

            Dim cellCabecera As PdfPCell = GetCabecera()
            cellCabecera.Border = Rectangle.NO_BORDER
            cellCabecera.Colspan = 100
            tblLayout.AddCell(cellCabecera)

            Dim cellPrograma As PdfPCell = GetPrograma()
            With cellPrograma
                .Border = Rectangle.NO_BORDER
                .Colspan = 100
            End With
            tblLayout.AddCell(cellPrograma)

            Dim cellInfoPersonal As PdfPCell = GetInfoPersonal()
            With cellInfoPersonal
                .Border = Rectangle.NO_BORDER
                .Colspan = 100
            End With
            tblLayout.AddCell(cellInfoPersonal)

            '-----------

            doc.Add(tblLayout)

            doc.Close()

            Response.Clear()
            Response.AddHeader("content-disposition", "attachment; filename=FICHA DE INSCRIPCION - " & lo_DataRow.Item("nroDocIdent_Alu") & ".pdf")
            Response.ContentType = "application/pdf"
            Response.Buffer = True
            Response.OutputStream.Write(ms.GetBuffer(), 0, ms.GetBuffer().Length)
            Response.OutputStream.Flush()
            Response.End()
        Catch ex As Exception
            errorMenssage.InnerHtml = ex.Message
        End Try
    End Sub

    Private Function GetCabecera() As PdfPCell
        Dim cell As New PdfPCell

        Dim table As New PdfPTable(100)
        With table
            .WidthPercentage = 100
            .DefaultCell.Border = Rectangle.NO_BORDER
            '.HorizontalAlignment = 2 'alienacion derecha
        End With

        Dim cellEmpty As New PdfPCell
        cellEmpty.Border = Rectangle.NO_BORDER

        'Logo
        Dim rootPath As String = Server.MapPath("~")
        Dim oImagen As iTextSharp.text.Image = iTextSharp.text.Image.GetInstance(rootPath & "\Images\LogoOficial.png")
        oImagen.ScaleAbsoluteWidth(120)

        Dim cellLogo As New PdfPCell
        With cellLogo
            .Border = Rectangle.NO_BORDER
            .Colspan = 30
            .Rowspan = 4
            .AddElement(oImagen)
        End With
        table.AddCell(cellLogo)

        Dim cellTitulo As New PdfPCell
        With cellTitulo
            .Border = Rectangle.NO_BORDER
            .Colspan = 17
            .Rowspan = 4
            .AddElement(New Phrase("ESCUELA DE POSGRADO USAT", mo_FontTitulo))
        End With
        table.AddCell(cellTitulo)

        cellEmpty.Colspan = 17
        cellEmpty.Rowspan = 4
        table.AddCell(cellEmpty)

        cellEmpty.Colspan = 36
        cellEmpty.PaddingTop = 25
        cellEmpty.Rowspan = 1
        table.AddCell(cellEmpty)
        table.AddCell(cellEmpty)

        Dim cellTitulo2 As PdfPCell = GenerarTitulo("FICHA DE INSCRIPCIÓN", 12)
        With cellTitulo2
            .HorizontalAlignment = PdfPCell.ALIGN_CENTER
            .Colspan = 36
            .Rowspan = 1
            .PaddingTop = 2
            .PaddingBottom = 6
        End With
        table.AddCell(cellTitulo2)

        cellEmpty.PaddingTop = 10
        table.AddCell(cellEmpty)

        cellEmpty.Colspan = 100
        cellEmpty.PaddingTop = 0
        table.AddCell(cellEmpty)

        cellEmpty.Colspan = 64
        table.AddCell(cellEmpty)

        Dim cellLabelFecha As New PdfPCell(New Phrase("FECHA", mo_Font))
        With cellLabelFecha
            .Colspan = 18
            .Border = Rectangle.NO_BORDER
            .HorizontalAlignment = PdfPCell.ALIGN_CENTER
        End With
        table.AddCell(cellLabelFecha)

        Dim cellFecha As New PdfPCell(New Phrase(Date.Now.ToString("dd/MM/yyyy"), mo_FontB))
        With cellFecha
            .Colspan = 18
            .Border = Rectangle.NO_BORDER
            .HorizontalAlignment = PdfPCell.ALIGN_CENTER
            .PaddingBottom = 4
            .BackgroundColor = New BaseColor(Drawing.ColorTranslator.FromHtml(ms_ColorGris))
        End With
        table.AddCell(cellFecha)

        cell.AddElement(table)
        Return cell
    End Function

    Private Function GetPrograma() As PdfPCell
        Dim cell As New PdfPCell

        Dim cellEmpty As New PdfPCell
        cellEmpty.Border = Rectangle.NO_BORDER

        Dim table As New PdfPTable(100)
        With table
            .WidthPercentage = 100
            .DefaultCell.Border = Rectangle.NO_BORDER
        End With

        Dim cellTitulo As PdfPCell = GenerarTitulo("PROGRAMA DE POSGRADO", 12)
        With cellTitulo
            .Colspan = 40
            .PaddingTop = 3
            .PaddingBottom = 6
        End With
        table.AddCell(cellTitulo)

        cellEmpty.Colspan = 60
        table.AddCell(cellEmpty)

        With cellEmpty
            .Colspan = 100
            .PaddingTop = 5
            .PaddingBottom = 5
        End With
        table.AddCell(cellEmpty)

        Dim cellPrograma As New PdfPCell(New Phrase("SISTEMAS", mo_Font))
        With cellPrograma
            .Colspan = 100
            .Border = Rectangle.NO_BORDER
            .PaddingBottom = 4
            .BackgroundColor = New BaseColor(Drawing.ColorTranslator.FromHtml(ms_ColorGris))
        End With
        table.AddCell(cellPrograma)

        cellEmpty.Colspan = 100
        table.AddCell(cellEmpty)

        cell.AddElement(table)
        Return cell
    End Function

    Private Function GetInfoPersonal() As PdfPCell
        Dim cell As New PdfPCell

        Dim cellEmptyPadded As New PdfPCell
        cellEmptyPadded.Border = Rectangle.NO_BORDER

        Dim cellEmpty As New PdfPCell
        cellEmpty.Border = Rectangle.NO_BORDER

        Dim table As New PdfPTable(100)
        With table
            .WidthPercentage = 100
            .DefaultCell.Border = Rectangle.NO_BORDER
        End With

        Dim cellTitulo As PdfPCell = GenerarTitulo("INFORMACIÓN PERSONAL", 12)
        With cellTitulo
            .Colspan = 40
            .PaddingTop = 3
            .PaddingBottom = 6
        End With
        table.AddCell(cellTitulo)

        cellEmptyPadded.Colspan = 60
        table.AddCell(cellEmptyPadded)

        With cellEmptyPadded
            .Colspan = 100
            .PaddingTop = 5
            .PaddingBottom = 5
        End With
        table.AddCell(cellEmptyPadded)

        '----

        Dim colspanLeft As Integer = 17

        With cellEmpty
            .Colspan = colspanLeft
        End With
        table.AddCell(cellEmpty)

        Dim cellLeyenda As New PdfPCell(New Phrase("Los nombres y apellidos deben ser escritos igual al DNI", mo_FontMini))
        With cellLeyenda
            .Colspan = 83
            .Border = Rectangle.NO_BORDER
        End With
        table.AddCell(cellLeyenda)

        Dim cellLabelApePat As New PdfPCell(New Phrase("Apellido Paterno", mo_Font))
        With cellLabelApePat
            .Colspan = colspanLeft
            .Border = Rectangle.NO_BORDER
        End With
        table.AddCell(cellLabelApePat)

        Dim cellApePat As New PdfPCell(New Phrase("DÍAZ", mo_Font))
        With cellApePat
            .Colspan = 33
            .Border = Rectangle.NO_BORDER
            .PaddingBottom = 5
            .PaddingLeft = 5
            .BackgroundColor = New BaseColor(Drawing.ColorTranslator.FromHtml(ms_ColorGris))
        End With
        table.AddCell(cellApePat)

        Dim cellLabelApeMat As New PdfPCell(New Phrase("Apellido Materno", mo_Font))
        With cellLabelApeMat
            .Colspan = colspanLeft
            .Border = Rectangle.NO_BORDER
            .HorizontalAlignment = PdfPCell.ALIGN_CENTER
        End With
        table.AddCell(cellLabelApeMat)

        Dim cellApeMat As New PdfPCell(New Phrase("VALDIVIEZO", mo_Font))
        With cellApeMat
            .Colspan = 33
            .Border = Rectangle.NO_BORDER
            .PaddingBottom = 5
            .PaddingLeft = 5
            .BackgroundColor = New BaseColor(Drawing.ColorTranslator.FromHtml(ms_ColorGris))
        End With
        table.AddCell(cellApeMat)

        cellEmpty.Colspan = 100
        cellEmpty.PaddingBottom = 5
        table.AddCell(cellEmpty)

        Dim cellLabelNombres As New PdfPCell(New Phrase("Nombres", mo_Font))
        With cellLabelNombres
            .Colspan = colspanLeft
            .Border = Rectangle.NO_BORDER
        End With
        table.AddCell(cellLabelNombres)

        Dim cellNombres As New PdfPCell(New Phrase("ANDY JAIR", mo_Font))
        With cellNombres
            .Colspan = 83
            .Border = Rectangle.NO_BORDER
            .PaddingBottom = 5
            .PaddingLeft = 5
            .BackgroundColor = New BaseColor(Drawing.ColorTranslator.FromHtml(ms_ColorGris))
        End With
        table.AddCell(cellNombres)

        '----

        cellEmpty.Colspan = 100
        cellEmpty.PaddingBottom = 5
        table.AddCell(cellEmpty)

        Dim cellLabelDNI As New PdfPCell(New Phrase("DNI", mo_Font))
        With cellLabelDNI
            .Colspan = colspanLeft
            .Border = Rectangle.NO_BORDER
        End With
        table.AddCell(cellLabelDNI)

        Dim cellDNI As New PdfPCell(New Phrase("11223344", mo_Font))
        With cellDNI
            .Colspan = 12
            .Border = Rectangle.NO_BORDER
        End With
        table.AddCell(cellDNI)

        Dim cellTipoDocIdent As New PdfPCell(New Phrase("DNI", mo_Font))
        With cellTipoDocIdent
            .Colspan = 11
            .Border = Rectangle.NO_BORDER
            .PaddingBottom = 5
            .PaddingLeft = 5
            .BackgroundColor = New BaseColor(Drawing.ColorTranslator.FromHtml(ms_ColorGris))
        End With
        table.AddCell(cellTipoDocIdent)

        Dim cellLabelFechaNacimiento As New PdfPCell(New Phrase("Fecha de Nacimiento", mo_Font))
        With cellLabelFechaNacimiento
            .Colspan = 22
            .Border = Rectangle.NO_BORDER
            .HorizontalAlignment = PdfPCell.ALIGN_CENTER
        End With
        table.AddCell(cellLabelFechaNacimiento)

        Dim cellFechaNacimiento As New PdfPCell(New Phrase("01/01/1995", mo_Font))
        With cellFechaNacimiento
            .Colspan = 20
            .Border = Rectangle.NO_BORDER
            .PaddingBottom = 5
            .PaddingLeft = 5
            .BackgroundColor = New BaseColor(Drawing.ColorTranslator.FromHtml(ms_ColorGris))
        End With
        table.AddCell(cellFechaNacimiento)

        Dim cellLabelEdad As New PdfPCell(New Phrase("Edad", mo_Font))
        With cellLabelEdad
            .Colspan = 9
            .Border = Rectangle.NO_BORDER
            .HorizontalAlignment = PdfPCell.ALIGN_CENTER
        End With
        table.AddCell(cellLabelEdad)

        Dim cellEdad As New PdfPCell(New Phrase("25", mo_Font))
        With cellEdad
            .Colspan = 9
            .Border = Rectangle.NO_BORDER
            .PaddingBottom = 5
            .PaddingLeft = 5
            .BackgroundColor = New BaseColor(Drawing.ColorTranslator.FromHtml(ms_ColorGris))
        End With
        table.AddCell(cellEdad)

        '----

        cellEmpty.Colspan = 100
        cellEmpty.PaddingBottom = 5
        table.AddCell(cellEmpty)

        Dim cellLabelSexo As New PdfPCell(New Phrase("Sexo", mo_Font))
        With cellLabelSexo
            .Colspan = colspanLeft
            .Border = Rectangle.NO_BORDER
        End With
        table.AddCell(cellLabelSexo)

        Dim cellSexo As New PdfPCell(New Phrase("MASCULINO", mo_Font))
        With cellSexo
            .Colspan = 33
            .Border = Rectangle.NO_BORDER
            .PaddingBottom = 5
            .PaddingLeft = 5
            .BackgroundColor = New BaseColor(Drawing.ColorTranslator.FromHtml(ms_ColorGris))
        End With
        table.AddCell(cellSexo)

        Dim cellLabelEstadoCivil As New PdfPCell(New Phrase("Estado Civil", mo_Font))
        With cellLabelEstadoCivil
            .Colspan = 17
            .Border = Rectangle.NO_BORDER
            .HorizontalAlignment = PdfPCell.ALIGN_CENTER
        End With
        table.AddCell(cellLabelEstadoCivil)

        Dim cellEstadoCivil As New PdfPCell(New Phrase("SOLTERO", mo_Font))
        With cellEstadoCivil
            .Colspan = 33
            .Border = Rectangle.NO_BORDER
            .PaddingBottom = 5
            .PaddingLeft = 5
            .BackgroundColor = New BaseColor(Drawing.ColorTranslator.FromHtml(ms_ColorGris))
        End With
        table.AddCell(cellEstadoCivil)

        '----

        cellEmpty.Colspan = 100
        cellEmpty.PaddingBottom = 5
        table.AddCell(cellEmpty)

        Dim cellLabelEmail As New PdfPCell(New Phrase("Email", mo_Font))
        With cellLabelEmail
            .Colspan = colspanLeft
            .Border = Rectangle.NO_BORDER
        End With
        table.AddCell(cellLabelEmail)

        Dim cellEmail As New PdfPCell(New Phrase("CORREO@GMAIL.COM", mo_Font))
        With cellEmail
            .Colspan = 83
            .Border = Rectangle.NO_BORDER
            .PaddingBottom = 5
            .PaddingLeft = 5
            .BackgroundColor = New BaseColor(Drawing.ColorTranslator.FromHtml(ms_ColorGris))
        End With
        table.AddCell(cellEmail)

        '----

        cellEmpty.Colspan = 100
        cellEmpty.PaddingBottom = 5
        table.AddCell(cellEmpty)

        Dim cellLabelDireccion As New PdfPCell(New Phrase("Dirección Domicilio", mo_Font))
        With cellLabelDireccion
            .Colspan = colspanLeft
            .Border = Rectangle.NO_BORDER
        End With
        table.AddCell(cellLabelDireccion)

        Dim cellDireccion As New PdfPCell(New Phrase("URB SANTA VICTORIA MZ-B LT-6", mo_Font))
        With cellDireccion
            .Colspan = 83
            .Border = Rectangle.NO_BORDER
            .PaddingBottom = 5
            .PaddingLeft = 5
            .BackgroundColor = New BaseColor(Drawing.ColorTranslator.FromHtml(ms_ColorGris))
        End With
        table.AddCell(cellDireccion)

        '----

        cellEmpty.Colspan = 100
        cellEmpty.PaddingBottom = 5
        table.AddCell(cellEmpty)

        Dim cellLabelDepartamento As New PdfPCell(New Phrase("Departamento", mo_Font))
        With cellLabelDepartamento
            .Colspan = colspanLeft
            .Border = Rectangle.NO_BORDER
        End With
        table.AddCell(cellLabelDepartamento)

        Dim cellDepartamento As New PdfPCell(New Phrase("LAMBAYEQUE", mo_Font))
        With cellDepartamento
            .Colspan = 19
            .Border = Rectangle.NO_BORDER
            .PaddingBottom = 5
            .PaddingLeft = 5
            .BackgroundColor = New BaseColor(Drawing.ColorTranslator.FromHtml(ms_ColorGris))
        End With
        table.AddCell(cellDepartamento)

        Dim cellLabelProvincia As New PdfPCell(New Phrase("Provincia", mo_Font))
        With cellLabelProvincia
            .Colspan = 15
            .Border = Rectangle.NO_BORDER
            .HorizontalAlignment = PdfPCell.ALIGN_CENTER
        End With
        table.AddCell(cellLabelProvincia)

        Dim cellProvincia As New PdfPCell(New Phrase("CHICLAYO", mo_Font))
        With cellProvincia
            .Colspan = 17
            .Border = Rectangle.NO_BORDER
            .PaddingBottom = 5
            .PaddingLeft = 5
            .BackgroundColor = New BaseColor(Drawing.ColorTranslator.FromHtml(ms_ColorGris))
        End With
        table.AddCell(cellProvincia)

        Dim cellLabelDistrito As New PdfPCell(New Phrase("Distrito", mo_Font))
        With cellLabelDistrito
            .Colspan = 15
            .Border = Rectangle.NO_BORDER
            .HorizontalAlignment = PdfPCell.ALIGN_CENTER
        End With
        table.AddCell(cellLabelDistrito)

        Dim cellDistrito As New PdfPCell(New Phrase("CHICLAYO", mo_Font))
        With cellDistrito
            .Colspan = 17
            .Border = Rectangle.NO_BORDER
            .PaddingBottom = 5
            .PaddingLeft = 5
            .BackgroundColor = New BaseColor(Drawing.ColorTranslator.FromHtml(ms_ColorGris))
        End With
        table.AddCell(cellDistrito)

        '----

        cellEmpty.Colspan = 100
        cellEmpty.PaddingBottom = 5
        table.AddCell(cellEmpty)

        Dim cellLabelTelDomicilio As New PdfPCell(New Phrase("Teléfono Domicilio", mo_Font))
        With cellLabelTelDomicilio
            .Colspan = colspanLeft
            .Border = Rectangle.NO_BORDER
        End With
        table.AddCell(cellLabelTelDomicilio)

        Dim cellTelDomicilio As New PdfPCell(New Phrase("074-995566", mo_Font))
        With cellTelDomicilio
            .Colspan = 19
            .Border = Rectangle.NO_BORDER
            .PaddingBottom = 5
            .PaddingLeft = 5
            .BackgroundColor = New BaseColor(Drawing.ColorTranslator.FromHtml(ms_ColorGris))
        End With
        table.AddCell(cellTelDomicilio)

        Dim cellLabelCelular As New PdfPCell(New Phrase("Celular", mo_Font))
        With cellLabelCelular
            .Colspan = 15
            .Border = Rectangle.NO_BORDER
            .HorizontalAlignment = PdfPCell.ALIGN_CENTER
        End With
        table.AddCell(cellLabelCelular)

        Dim cellCelular As New PdfPCell(New Phrase("987654321", mo_Font))
        With cellCelular
            .Colspan = 17
            .Border = Rectangle.NO_BORDER
            .PaddingBottom = 5
            .PaddingLeft = 5
            .BackgroundColor = New BaseColor(Drawing.ColorTranslator.FromHtml(ms_ColorGris))
        End With
        table.AddCell(cellCelular)

        Dim cellLabelTelFijo As New PdfPCell(New Phrase("Teléfono Fijo", mo_Font))
        With cellLabelTelFijo
            .Colspan = 15
            .Border = Rectangle.NO_BORDER
            .HorizontalAlignment = PdfPCell.ALIGN_CENTER
        End With
        table.AddCell(cellLabelTelFijo)

        Dim cellTelFijo As New PdfPCell(New Phrase("074-995566", mo_Font))
        With cellTelFijo
            .Colspan = 17
            .Border = Rectangle.NO_BORDER
            .PaddingBottom = 5
            .PaddingLeft = 5
            .BackgroundColor = New BaseColor(Drawing.ColorTranslator.FromHtml(ms_ColorGris))
        End With
        table.AddCell(cellTelFijo)

        '----
        cell.AddElement(table)
        Return cell
    End Function

    Private Function GenerarTitulo(ByVal texto As String, ByVal nroLetra As Integer) As PdfPCell
        Dim lo_FontTitulo As Font = FontFactory.GetFont("Arial", nroLetra, Font.BOLD, BaseColor.WHITE)
        Dim cellTitulo As New PdfPCell(New Phrase(texto, lo_FontTitulo))
        With cellTitulo
            .Border = Rectangle.NO_BORDER
            .VerticalAlignment = PdfPCell.ALIGN_MIDDLE
            .BackgroundColor = New BaseColor(Drawing.ColorTranslator.FromHtml(ms_ColorGranate))
        End With
        Return cellTitulo
    End Function
#End Region

End Class

Partial Class PDFFooter
    Inherits PdfPageEventHelper

    Private mo_FontMini As Font = FontFactory.GetFont("Arial", 5)
    Private mo_FontMiniB As Font = FontFactory.GetFont("Arial", 6, Font.BOLD)
    Private mo_FontSmall As Font = FontFactory.GetFont("Arial", 7)
    Private mo_FontSmallB As Font = FontFactory.GetFont("Arial", 7, Font.BOLD)

    Public Overrides Sub OnEndPage(ByVal writer As iTextSharp.text.pdf.PdfWriter, ByVal document As iTextSharp.text.Document)
        MyBase.OnEndPage(writer, document)

        Dim tblFooter As New PdfPTable(100)
        With tblFooter
            .TotalWidth = 572.0F
        End With

        tblFooter.WriteSelectedRows(0, -1, 15, document.Bottom, writer.DirectContent)
    End Sub
End Class
