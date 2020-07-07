Imports Microsoft.VisualBasic
Imports System.Collections.Generic
Imports iTextSharp.text.html

Public Class clsGeneraDocumento
#Region "variables"
    Private cnx As ClsConectarDatos
    Private dt As Data.DataTable

    Dim md_configuraDoc As d_ConfigurarDocumentoArea
    Private ruta As String = ConfigurationManager.AppSettings("SharedFiles") '"http://localhost/campusvirtual/ArchivosCompartidos/SharedFiles.asmx"
    Private _fuente As String


    Public WriteOnly Property fuente() As String
        Set(ByVal value As String)
            _fuente = value
        End Set
    End Property

#End Region

    Public Function GenerarDocumento(ByVal archivo As String, ByVal memory As System.IO.MemoryStream, ByVal serieCorrelativoDoc As String, ByVal sourceIcon As String, ByVal arreglo As Dictionary(Of String, String)) As Integer
        Select Case archivo
            Case "InformeDeAsesor"
                Call EmiteInformeDeAsesor(memory, sourceIcon, serieCorrelativoDoc, arreglo)
            Case "ResolucionSustentacion"
                Call EmiteResolucionSustentacion(memory, sourceIcon, serieCorrelativoDoc, arreglo)
            Case "ActaDeSustentacion"
                Call EmiteActaSustentación(memory, sourceIcon, serieCorrelativoDoc, arreglo)
            Case "FichaMatricula"
                Call EmiteFichaMatricula(memory, sourceIcon, serieCorrelativoDoc, arreglo)
            Case "FichaNotas"
                Call EmiteFichaNotas(memory, sourceIcon, serieCorrelativoDoc, arreglo)
        End Select

    End Function

    Public Function EmiteInformeDeAsesor(ByVal memory As System.IO.MemoryStream, ByVal sourceIcon As String, ByVal serieCorrelativo As String, ByVal arreglo As Dictionary(Of String, String)) As Integer
        Try
            Dim alumnos As String = ""
            Dim dtDirector As New Data.DataTable
            Dim ultimaFila As Integer

            '************* Articulos / prefijos / sufijos
            Dim txtDel As String = ""
            Dim txtBachiller As String = ""
            Dim txtEl As String = ""
            Dim txtHa As String = ""
            Dim txtSexo As String = ""
            '********************************************

            'traigo los datos de los alumnos
            dt = New Data.DataTable
            dt = fc_DatosInformeAsesor(arreglo.Item("codigo_tes"))

            Dim dtDatos As New Data.DataTable
            dtDatos = fc_DatosInformeAsesor(arreglo.Item("codigo_tes"))

            If dt.Rows.Count > 0 Then
                ultimaFila = dt.Rows.Count - 1
                For i As Integer = 0 To dt.Rows.Count - 1
                    '-------sexo
                    If i = 0 Then
                        txtSexo = dt.Rows(i).Item("sexo_alu").ToString
                    Else
                        If txtSexo <> dt.Rows(i).Item("sexo_alu").ToString Then
                            txtSexo = "U"
                        End If
                    End If

                    '------ coma
                    If i = ultimaFila Then
                        alumnos = alumnos + dt.Rows(i).Item("autor")
                    Else
                        alumnos = alumnos + dt.Rows(i).Item("autor") & ", "
                    End If
                Next
                '''''''''
                If ultimaFila > 0 Then

                    txtBachiller = "Bachilleres"
                    If txtSexo = "F" Then
                        txtEl = "las"
                        txtDel = "de las"
                    Else
                        txtEl = "los"
                        txtDel = "de los"
                    End If
                    txtHa = "han"
                Else
                    If txtSexo = "F" Then
                        txtDel = "de la"
                        txtEl = "la"
                    Else
                        txtDel = "del"
                        txtEl = "el"
                    End If
                    txtBachiller = "Bachiller"
                    txtHa = "ha"
                End If
                'trae el director academi

                'trae el director academico -- Director de escuela
                dtDirector = fc_ListarDirectorAcademico(dt.Rows(0).Item("codigo_cpf"), "E") '-- E: director de esccuela                 
            End If

            Dim pdfDoc As New iTextSharp.text.Document(iTextSharp.text.PageSize.A4)
            Dim pdfWrite As iTextSharp.text.pdf.PdfWriter = iTextSharp.text.pdf.PdfWriter.GetInstance(pdfDoc, memory)
            pdfDoc.Open()

            Dim pdfTable As iTextSharp.text.pdf.PdfPTable = New iTextSharp.text.pdf.PdfPTable(1)
            pdfTable.WidthPercentage = 85.0F
            pdfTable.DefaultCell.Border = 0

            '---- Cabecera
            Dim cellCabecera As iTextSharp.text.pdf.PdfPTable = New iTextSharp.text.pdf.PdfPTable(1)
            cellCabecera.WidthPercentage = 100.0F
            cellCabecera.SetWidths(New Single() {100.0F})
            cellCabecera.DefaultCell.Border = 0

            cellCabecera.AddCell(fc_CeldaTexto("SISTEMA DE LA GESTIÓN DE LA CALIDAD " & Environment.NewLine & " " & "CÓDIGO USAT-PM0702-D-01" & Environment.NewLine & "VERSIÓN:01" & Environment.NewLine & Environment.NewLine, 10.0, 1, 0, 1, 1, 2, -1, 6, ""))
            pdfTable.AddCell(cellCabecera)

            '------ logotipo
            Dim cellLogotipo As iTextSharp.text.pdf.PdfPTable = New iTextSharp.text.pdf.PdfPTable(1)
            cellLogotipo.WidthPercentage = 100.0F
            cellLogotipo.SetWidths(New Single() {100.0F})
            cellLogotipo.DefaultCell.Border = 0

            '---- icono usat
            Dim srcIcon As String = sourceIcon
            Dim usatIcon As iTextSharp.text.Image = iTextSharp.text.Image.GetInstance(srcIcon)
            usatIcon.ScalePercent(60.0F)
            usatIcon.Alignment = iTextSharp.text.Element.ALIGN_LEFT

            Dim cellIcon As iTextSharp.text.pdf.PdfPCell = New iTextSharp.text.pdf.PdfPCell(usatIcon)
            cellIcon.HorizontalAlignment = 0
            cellIcon.VerticalAlignment = 0
            cellIcon.Border = 0
            'cellIcon.Rowspan = 2
            'pdfTable.AddCell(cellIcon)

            cellLogotipo.AddCell(cellIcon)
            pdfTable.AddCell(cellLogotipo)

            '------ titulo de documento y correlativo
            Dim cellTituloCorrelativo As iTextSharp.text.pdf.PdfPTable = New iTextSharp.text.pdf.PdfPTable(1)
            cellTituloCorrelativo.WidthPercentage = 100.0F
            cellTituloCorrelativo.SetWidths(New Single() {100.0F})
            cellTituloCorrelativo.DefaultCell.Border = 0

            cellTituloCorrelativo.AddCell(fc_CeldaTexto("INFORME N° " & serieCorrelativo & Environment.NewLine & " " & Environment.NewLine, 10.0F, 1, 0, 1, 1, 1))

            pdfTable.AddCell(cellTituloCorrelativo)
            '--------- deParaAsunto ---------------------------------------------------------------------

            Dim cellDeParaAsunto As iTextSharp.text.pdf.PdfPTable = New iTextSharp.text.pdf.PdfPTable(2)
            cellDeParaAsunto.WidthPercentage = 100.0F
            cellDeParaAsunto.SetWidths(New Single() {20.0F, 80.0F})
            cellDeParaAsunto.DefaultCell.Border = 0

            cellDeParaAsunto.AddCell(fc_CeldaTexto("A", 9.0F, 1, 0, 1, 1, 0))
            'cellDeParaAsunto.AddCell(fc_CeldaTexto(arreglo.Item("directorEscuela"), 8.0F, 0, 0, 1, 1, 0))
            cellDeParaAsunto.AddCell(fc_CeldaTexto(dtDirector.Rows(0).Item("apellidoPat_per") & " " & dtDirector.Rows(0).Item("apellidoMat_per") & " " & dtDirector.Rows(0).Item("nombres_per") & Environment.NewLine & "Director de Escuela del Programa de " & dtDatos.Rows(0).Item("nombre_Cpf"), 9.0F, 0, 0, 1, 1, 0))

            cellDeParaAsunto.AddCell(fc_CeldaTexto("DE", 9.0F, 1, 0, 1, 1, 0))
            'cellDeParaAsunto.AddCell(fc_CeldaTexto(arreglo.Item("grado") & "  " & arreglo.Item("asesor"), 8.0F, 0, 0, 1, 1, 0))
            cellDeParaAsunto.AddCell(fc_CeldaTexto(dtDatos.Rows(0).Item("grado") & " " & dtDatos.Rows(0).Item("asesor"), 9.0F, 0, 0, 1, 1, 0))

            cellDeParaAsunto.AddCell(fc_CeldaTexto("ASUNTO", 9.0F, 1, 0, 1, 1, 0))
            cellDeParaAsunto.AddCell(fc_CeldaTexto("Aprobación de Informe de Tesis " & txtDel & " " & txtBachiller & ": " & alumnos, 9.0F, 0, 0, 1, 1, 0))

            cellDeParaAsunto.AddCell(fc_CeldaTexto("FECHA", 9.0F, 1, 0, 1, 1, 0))
            cellDeParaAsunto.AddCell(fc_CeldaTexto("Chiclayo, " & dtDatos.Rows(0).Item("fechaConformidad"), 9.0F, 0, 0, 1, 1, 0))

            cellDeParaAsunto.AddCell(fc_CeldaTexto("", 9.0F, 0, 1, 2, 1, 0))

            pdfTable.AddCell(cellDeParaAsunto)

            '----- Contenido ----------------------------------------------------------------------------------------------
            Dim cellContenido As iTextSharp.text.pdf.PdfPTable = New iTextSharp.text.pdf.PdfPTable(1)
            cellContenido.WidthPercentage = 100.0F
            cellContenido.SetWidths(New Single() {100.0F})
            cellContenido.DefaultCell.Border = 0

            Dim _parrafoOne As iTextSharp.text.Phrase
            _parrafoOne = New iTextSharp.text.Phrase

            _parrafoOne.Add(fc_textFrase("Tengo a bien dirigirme a Ud. con la finalidad de comunicarle que " & txtEl & " " & txtBachiller & " de " & dtDatos.Rows(0).Item("nombre_Cpf") & ", " & alumnos & ", " & _
                                      "" & txtHa & " cumplido con las observaciones sugeridas y concluido satisfactoriamente el proceso de formulación y desarrollo de su tesis titulada " & dtDatos.Rows(0).Item("Titulo_tes") & ". " & Environment.NewLine & _
                                      "En tal sentido, informo que he revisado el trabajo de investigación titulado " & dtDatos.Rows(0).Item("Titulo_tes") & " en su totalidad, tanto en forma como en fondo, y manifiesto que todos los " & _
                                      "objetivos han sido cumplidos por lo cual se encuentra apto para el proceso de la sustentación." & Environment.NewLine & _
                                      "Por lo cual doy conformidad. " & Environment.NewLine & " " & Environment.NewLine & "Atte.", 5, 10.0F, 0, iTextSharp.text.BaseColor.BLACK))

            cellContenido.AddCell(fc_CeldaTextoPhrase(_parrafoOne, 0, 1, 1, 3, 0, 5, ""))
            '----- fecha
            'linea en blanco
            cellContenido.AddCell(fc_CeldaTexto("", 8.0F, 0, 0, 1, 1, 1))
            cellContenido.AddCell(fc_CeldaTexto("", 8.0F, 0, 0, 1, 1, 1))
            cellContenido.AddCell(fc_CeldaTexto("", 8.0F, 0, 0, 1, 1, 1))

            'cellContenido.AddCell(fc_CeldaTexto("Chiclayo, " & Day(Now) & " de " & Month(Now) & " del " & Year(Now), 10.0F, 0, 0, 1, 1, 2))
            pdfTable.AddCell(cellContenido)

            '------- firmas
            Dim cellFirmas As iTextSharp.text.pdf.PdfPTable = New iTextSharp.text.pdf.PdfPTable(3)
            cellFirmas.WidthPercentage = 100.0F
            cellFirmas.SetWidths(New Single() {34.0F, 33.0F, 33.0F})
            cellFirmas.DefaultCell.Border = 0

            'linea en blanco
            cellContenido.AddCell(fc_CeldaTexto("", 8.0F, 0, 0, 3, 1, 1))
            cellContenido.AddCell(fc_CeldaTexto("", 8.0F, 0, 0, 3, 1, 1))

            cellContenido.AddCell(fc_CeldaTexto(dtDatos.Rows(0).Item("grado") & "  " & dtDatos.Rows(0).Item("asesor"), 8.0F, 0, 0, 3, 1, 1))
            cellContenido.AddCell(fc_CeldaTexto("Asesor", 8.0F, 0, 0, 1, 1, 1))

            pdfDoc.Add(pdfTable)
            pdfDoc.Close()

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function EmiteResolucionSustentacion(ByVal memory As System.IO.MemoryStream, ByVal sourceIcon As String, ByVal serieCorrelativo As String, ByVal arreglo As Dictionary(Of String, String)) As Integer
        Try
            Dim alumnosDNI As String = ""
            Dim alumnos As String = ""
            Dim dtDirector As New Data.DataTable
            Dim dtDecano As New Data.DataTable
            'traigo los datos de los alumnos
            Dim ultimaFila As Integer
            dt = New Data.DataTable
            dt = fc_DatosResolSustentacion(arreglo.Item("codigo_tes"))
            '************* Articulos / prefijos / sufijos
            Dim txtBachiller As String = ""
            Dim txtQuien As String = ""
            Dim sexoDirector As String = ""
            Dim txtAl As String = ""
            Dim txtDecano As String = ""
            Dim txtApto As String = ""
            Dim txtSexo As String = ""
            '********************************************
            Dim dtDatos As New Data.DataTable
            dtDatos = fc_DatosResolSustentacion(arreglo.Item("codigo_tes"))

            If dt.Rows.Count > 0 Then
                ultimaFila = dt.Rows.Count - 1
                For i As Integer = 0 To dt.Rows.Count - 1
                    '-----------------------sexo
                    If i = 0 Then
                        txtSexo = dt.Rows(i).Item("sexo_alu").ToString
                    Else
                        If txtSexo <> dt.Rows(i).Item("sexo_alu").ToString Then
                            txtSexo = "U"
                        End If
                    End If
                    '---------------------- coma
                    If i = ultimaFila Then
                        alumnosDNI = alumnosDNI + dt.Rows(i).Item("alumno") & " con DNI " & dt.Rows(i).Item("nroDocIdent_Alu")
                        alumnos = alumnos + dt.Rows(i).Item("alumno")
                    Else
                        alumnosDNI = alumnosDNI + dt.Rows(i).Item("alumno") & " con DNI " & dt.Rows(i).Item("nroDocIdent_Alu") & ", "
                        alumnos = alumnos + dt.Rows(i).Item("alumno") & ", "
                    End If
                Next

                If ultimaFila > 0 Then
                    txtBachiller = "bachilleres"
                    txtQuien = "quienes solicitan"
                    If txtSexo = "F" Then
                        txtAl = "a las"
                        txtApto = "aptas"
                    Else
                        txtAl = "a los"
                        txtApto = "aptos"
                    End If
                Else
                    txtBachiller = "bachiller"
                    txtQuien = "quien solicita"
                    If txtSexo = "F" Then
                        txtAl = "a la"
                        txtApto = "apta"
                    Else
                        txtAl = "al"
                        txtApto = "apto"
                    End If
                    
                End If
                'trae el director academico -- Director de escuela
                dtDirector = fc_ListarDirectorAcademico(dt.Rows(0).Item("codigo_cpf"), "E") '-- E: director de esccuela
                If dtDirector.Rows(0).Item("sexo_per") = "M" Then
                    sexoDirector = "el Director"
                Else
                    sexoDirector = "la Directora"
                End If

                'dtDecano = fc_ListarDirectorAcademico(dt.Rows(0).Item("codigo_Fac"), "F") '-- F: Decano
            End If
            dtDecano = fc_ListarDirectorAcademico(dtDatos.Rows(0).Item("codigo_Fac"), "F")
            If dtDecano.Rows(0).Item("sexo_per") = "M" Then
                txtDecano = "Decano"
            Else
                txtDecano = "Decana"
            End If
            ''********************Principal*************************************************************************
            Dim pdfDoc As New iTextSharp.text.Document(iTextSharp.text.PageSize.A4)
            Dim pdfWrite As iTextSharp.text.pdf.PdfWriter = iTextSharp.text.pdf.PdfWriter.GetInstance(pdfDoc, memory)
            pdfDoc.Open()

            Dim pdfTable As iTextSharp.text.pdf.PdfPTable = New iTextSharp.text.pdf.PdfPTable(1)
            pdfTable.WidthPercentage = 85.0F
            pdfTable.DefaultCell.Border = 0

            ''********************Principal*************************************************************************

            ''---- Cabecera
            'Dim cellCabecera As iTextSharp.text.pdf.PdfPTable = New iTextSharp.text.pdf.PdfPTable(1)
            'cellCabecera.WidthPercentage = 100.0F
            'cellCabecera.SetWidths(New Single() {100.0F})
            'cellCabecera.DefaultCell.Border = 0

            'cellCabecera.AddCell(fc_CeldaTexto("SISTEMA DE LA GESTIÓN DE LA CALIDAD " & Environment.NewLine & " " & "CÓDIGO USAT-PM0702-D-01" & Environment.NewLine & "VERSIÓN:01" & Environment.NewLine & Environment.NewLine, 10.0, 1, 0, 1, 1, 2, -1, 6, ""))
            'pdfTable.AddCell(cellCabecera)

            '------ logotipo
            Dim cellLogotipo As iTextSharp.text.pdf.PdfPTable = New iTextSharp.text.pdf.PdfPTable(1)
            cellLogotipo.WidthPercentage = 100.0F
            cellLogotipo.SetWidths(New Single() {100.0F})
            cellLogotipo.DefaultCell.Border = 0

            '---- icono usat
            Dim srcIcon As String = sourceIcon
            Dim usatIcon As iTextSharp.text.Image = iTextSharp.text.Image.GetInstance(srcIcon)
            usatIcon.ScalePercent(50.0F)
            usatIcon.Alignment = iTextSharp.text.Element.ALIGN_LEFT

            Dim cellIcon As iTextSharp.text.pdf.PdfPCell = New iTextSharp.text.pdf.PdfPCell(usatIcon)
            cellIcon.HorizontalAlignment = 0
            cellIcon.VerticalAlignment = 0
            cellIcon.Border = 0
            'cellIcon.Rowspan = 2
            'pdfTable.AddCell(cellIcon)

            cellLogotipo.AddCell(cellIcon)
            pdfTable.AddCell(cellLogotipo)

            '------ titulo de documento y correlativo
            Dim cellTituloCorrelativo As iTextSharp.text.pdf.PdfPTable = New iTextSharp.text.pdf.PdfPTable(1)
            cellTituloCorrelativo.WidthPercentage = 100.0F
            cellTituloCorrelativo.SetWidths(New Single() {100.0F})
            cellTituloCorrelativo.DefaultCell.Border = 0

            cellTituloCorrelativo.AddCell(fc_CeldaTexto("CONSEJO DE FACULTAD" & Environment.NewLine, 10.0F, 1, 0, 1, 1, 1))
            cellTituloCorrelativo.AddCell(fc_CeldaTexto(serieCorrelativo & Environment.NewLine, 10.0F, 1, 0, 1, 1, 1))
            cellTituloCorrelativo.AddCell(fc_CeldaTexto("Chiclayo, " & dtDatos.Rows(0).Item("FechaEmisionResol") & Environment.NewLine & " " & Environment.NewLine, 10.0F, 1, 0, 1, 1, 1))

            pdfTable.AddCell(cellTituloCorrelativo)


            '----- Contenido ----------------------------------------------------------------------------------------------
            Dim cellContenido As iTextSharp.text.pdf.PdfPTable = New iTextSharp.text.pdf.PdfPTable(1)
            cellContenido.WidthPercentage = 100.0F
            cellContenido.SetWidths(New Single() {100.0F})
            cellContenido.DefaultCell.Border = 0

            Dim _parrafoOne As iTextSharp.text.Phrase
            _parrafoOne = New iTextSharp.text.Phrase

            _parrafoOne.Add(fc_textFrase("Visto el Expediente Nº " & dtDatos.Rows(0).Item("glosaCorrelativo_trl") & " perteneciente a " & alumnos & ", " & txtBachiller & " de " & dtDatos.Rows(0).Item("NombreOficial_cpf") & " " & txtQuien & " sustentación de tesis con fines de titulación y; " & Environment.NewLine & Environment.NewLine & _
                                     "CONSIDERANDO: " & Environment.NewLine & Environment.NewLine & _
                                     "Que mediante el Informe del " & dtDatos.Rows(0).Item("fechainformeasesor") & ", el Asesor de tesis, " & dtDatos.Rows(0).Item("asesor") & ", da conformidad a la tesis denominada: " & dtDatos.Rows(0).Item("titulo_tes") & ". " & Environment.NewLine & _
                                     "Que " & sexoDirector & " de la Escuela de " & dtDirector.Rows(0).Item("nombre_cpf") & ", " & dtDirector.Rows(0).Item("grado") & " " & dtDirector.Rows(0).Item("nombres_per") & " " & dtDirector.Rows(0).Item("apellidoPat_per") & " " & dtDirector.Rows(0).Item("apellidoMat_per") & ", ha declarado " & txtAl & " " & txtBachiller & " como " & txtApto & " y ha cumplido con los requisitos establecidos por el Reglamento de Grados y Títulos de la Universidad Católica Santo Toribio de Mogrovejo; " & Environment.NewLine & _
                                     "En uso de las atribuciones conferidas por la Ley Universitaria Nº 30220, el Estatuto de la Asociación Civil USAT y el Reglamento de Grados y Títulos de la Universidad Católica Santo Toribio de Mogrovejo;" & Environment.NewLine & Environment.NewLine & _
                                     "SE RESUELVE:. " & Environment.NewLine & " " & Environment.NewLine, 5, 10.0F, 0, iTextSharp.text.BaseColor.BLACK))

            cellContenido.AddCell(fc_CeldaTextoPhrase(_parrafoOne, 0, 1, 1, 3, 0, 5, ""))

            pdfTable.AddCell(cellContenido)

            '----- Contenido con tabla ----------------------------------------------------------------------------------------------
            Dim cellContenidoT1 As iTextSharp.text.pdf.PdfPTable = New iTextSharp.text.pdf.PdfPTable(2)
            cellContenidoT1.WidthPercentage = 100.0F
            cellContenidoT1.SetWidths(New Single() {20.0F, 80.0F})
            cellContenidoT1.DefaultCell.Border = 0

            Dim cellContenidoT2 As iTextSharp.text.pdf.PdfPTable = New iTextSharp.text.pdf.PdfPTable(2)
            cellContenidoT2.WidthPercentage = 100.0F
            cellContenidoT2.SetWidths(New Single() {20.0F, 80.0F})
            cellContenidoT2.DefaultCell.Border = 0

            Dim cellContenidoT3 As iTextSharp.text.pdf.PdfPTable = New iTextSharp.text.pdf.PdfPTable(2)
            cellContenidoT3.WidthPercentage = 100.0F
            cellContenidoT3.SetWidths(New Single() {20.0F, 80.0F})
            cellContenidoT3.DefaultCell.Border = 0

            Dim _parrafoTwo As iTextSharp.text.Phrase
            _parrafoTwo = New iTextSharp.text.Phrase

            Dim _parrafoTree As iTextSharp.text.Phrase
            _parrafoTree = New iTextSharp.text.Phrase

            Dim _parrafoFor As iTextSharp.text.Phrase
            _parrafoFor = New iTextSharp.text.Phrase


            cellContenidoT1.AddCell(fc_CeldaTexto("Articulo 1°-", 9.0F, 0, 0, 1, 1, 1))
            _parrafoTwo.Add(fc_textFrase("Declarar expedito " & txtAl & " " & txtBachiller & " " & alumnos & " de la Escuela de " & dtDatos.Rows(0).Item("nombreOficial_cpf") & ", para la sustentación de la tesis denominada: " & dtDatos.Rows(0).Item("titulo_tes") & ".", 5, 10.0F, 0, iTextSharp.text.BaseColor.BLACK))
            cellContenidoT1.AddCell(fc_CeldaTextoPhrase(_parrafoTwo, 0, 1, 1, 3, 0, 5, ""))
            pdfTable.AddCell(cellContenidoT1)

            cellContenidoT2.AddCell(fc_CeldaTexto("Articulo 2°-", 9.0F, 0, 0, 1, 1, 1))
            _parrafoTree.Add(fc_textFrase("Designar como miembros del jurado de tesis a los siguientes docentes: " & Environment.NewLine & _
                                          "● Presidente: " & dtDatos.Rows(0).Item("presidente") & "." & Environment.NewLine & _
                                          "● Secretario: " & dtDatos.Rows(0).Item("secretario") & "." & Environment.NewLine & _
                                          "● Vocal: " & dtDatos.Rows(0).Item("vocal") & ".", 5, 10.0F, 0, iTextSharp.text.BaseColor.BLACK))
            cellContenidoT2.AddCell(fc_CeldaTextoPhrase(_parrafoTree, 0, 1, 1, 3, 0, 5, ""))
            pdfTable.AddCell(cellContenidoT2)

            cellContenidoT3.AddCell(fc_CeldaTexto("Articulo 3°-", 9.0F, 0, 0, 1, 1, 1))
            _parrafoFor.Add(fc_textFrase("Fijar como fecha de sustentación el día " & dtDatos.Rows(0).Item("fechaprogramacion") & " a las " & dtDatos.Rows(0).Item("horaprogramacion") & " en el ambiente " & dtDatos.Rows(0).Item("ambiente") & " de la Universidad Católica Santo Toribio de Mogrovejo. ", 5, 10.0F, 0, iTextSharp.text.BaseColor.BLACK))
            cellContenidoT3.AddCell(fc_CeldaTextoPhrase(_parrafoFor, 0, 1, 1, 3, 0, 5, ""))
            pdfTable.AddCell(cellContenidoT3)


            '------- firmas
            Dim cellFirmas As iTextSharp.text.pdf.PdfPTable = New iTextSharp.text.pdf.PdfPTable(3)
            cellFirmas.WidthPercentage = 100.0F
            cellFirmas.SetWidths(New Single() {34.0F, 33.0F, 33.0F})
            cellFirmas.DefaultCell.Border = 0

            'linea en blanco
            cellFirmas.AddCell(fc_CeldaTexto("", 9.0F, 0, 0, 3, 1, 1))
            cellFirmas.AddCell(fc_CeldaTexto("", 9.0F, 0, 0, 3, 1, 1))
            cellFirmas.AddCell(fc_CeldaTexto("", 9.0F, 0, 0, 3, 1, 1))
            cellFirmas.AddCell(fc_CeldaTexto("", 9.0F, 0, 0, 3, 1, 1))


            cellFirmas.AddCell(fc_CeldaTexto(dtDecano.Rows(0).Item("grado") & " " & dtDecano.Rows(0).Item("nombres_per") & " " & dtDecano.Rows(0).Item("apellidoPat_per") & " " & dtDecano.Rows(0).Item("apellidoMat_per") & Environment.NewLine & txtDecano & Environment.NewLine & " Facultad de " & dtDecano.Rows(0).Item("nombre_Fac") & " ", 9.0F, 0, 0, 3, 1, 1))
            'cellFirmas.AddCell(fc_CeldaTexto("Decano", 8.0F, 0, 0, 3, 1, 1))
            'cellFirmas.AddCell(fc_CeldaTexto("Facultad de [Nombre de facultad]", 8.0F, 0, 0, 3, 1, 1))
            pdfTable.AddCell(cellFirmas)

            pdfDoc.Add(pdfTable)
            pdfDoc.Close()

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function EmiteFichaMatricula(ByVal memory As System.IO.MemoryStream, ByVal sourceIcon As String, ByVal serieCorrelativo As String, ByVal arreglo As Dictionary(Of String, String)) As Integer
        Dim md_solicitud As New d_SolicitaDocumentacion
        Dim dtDet As New Data.DataTable
        Dim dtSolicitud As New Data.DataTable
        Dim dtAlumno As New Data.DataTable
        Dim codigo_alu As Integer
        Dim codigo_cac As Integer
        Dim descripcion_cac As String = ""
        Dim codigo_univ_alu As String = ""

        If arreglo.Item("codigo_sol") <> "0" Then
            dtSolicitud = md_solicitud.ListarSolicitaDocumentacion("", arreglo.Item("codigo_sol"), 0, "")
        End If

        If dtSolicitud.Rows.Count > 0 Then
            With dtSolicitud.Rows(0)
                codigo_alu = .Item("codigo_alu")
                codigo_cac = .Item("codigo_cac")
                descripcion_cac = .Item("referencia01")
                codigo_univ_alu = .Item("codigoUniver_Alu")
            End With
        End If
        If arreglo.Item("codigo_sol") <> "0" Then
            dtDet = fc_DatosFichaMatriculaNotas("FichaMatricula", codigo_alu, codigo_cac)
            dtAlumno = fc_ConsultarAlumnoFichaByCU(codigo_univ_alu)
        Else
            dtDet = fc_DatosFichaMatriculaNotas("FichaMatricula", arreglo.Item("codigo_Alu"), arreglo.Item("codigo_cac"))
            dtAlumno = fc_ConsultarAlumnoFichaByCU(arreglo.Item("codigoUniv_alu"))
        End If
        
        'md_configuraDoc = New d_ConfigurarDocumentoArea

        Try

            Dim pdfDoc As New iTextSharp.text.Document(iTextSharp.text.PageSize.A4)
            Dim pdfWrite As iTextSharp.text.pdf.PdfWriter = iTextSharp.text.pdf.PdfWriter.GetInstance(pdfDoc, memory)
            pdfDoc.Open()

            If arreglo.Item("codigo_sol") = "0" Then mt_AddWaterMark(pdfWrite, "BORRADOR")


            Dim pdfTable As iTextSharp.text.pdf.PdfPTable = New iTextSharp.text.pdf.PdfPTable(1)
            pdfTable.WidthPercentage = 98.0F
            pdfTable.DefaultCell.Border = 0

            '---- tabla cabecera
            Dim cellTable0 As iTextSharp.text.pdf.PdfPTable = New iTextSharp.text.pdf.PdfPTable(3)
            cellTable0.WidthPercentage = 100.0F
            cellTable0.SetWidths(New Single() {18.0F, 42.0F, 40.0F})
            cellTable0.DefaultCell.Border = 0

            '---- icono usat
            Dim srcIcon As String = sourceIcon
            Dim usatIcon As iTextSharp.text.Image = iTextSharp.text.Image.GetInstance(srcIcon)
            usatIcon.ScalePercent(60.0F)
            usatIcon.Alignment = iTextSharp.text.Element.ALIGN_LEFT
            '---- sello
            Dim srcSello As String = arreglo.Item("sourceSello")
            Dim usatSello As iTextSharp.text.Image = iTextSharp.text.Image.GetInstance(srcSello)
            usatSello.ScalePercent(30.0F)
            usatSello.Alignment = iTextSharp.text.Element.ALIGN_RIGHT

            'Cabecera
            'celda del icono
            Dim cellIcon As iTextSharp.text.pdf.PdfPCell = New iTextSharp.text.pdf.PdfPCell(usatIcon)
            cellIcon.HorizontalAlignment = 0
            cellIcon.VerticalAlignment = 2
            cellIcon.Border = 0
            'cellIcon.Rowspan = 2
            'pdfTable.AddCell(cellIcon)

            cellTable0.AddCell(cellIcon)

            cellTable0.AddCell(fc_CeldaTexto("Universidad Católica Santo Toribio de Mogrovejo" & Environment.NewLine & " " & Environment.NewLine & _
                               "Dirección Académica", 9.0F, 1, 0, 1, 1, 0))

            '''''' titulo
            cellTable0.AddCell(fc_CeldaTexto("FICHA DE MATRÍCULA" & Environment.NewLine & " " & Environment.NewLine & _
                               serieCorrelativo, 12.0F, 1, 0, 1, 1, 1))

            pdfTable.AddCell(cellTable0)

            ''''''
            'detalle
            Dim table As iTextSharp.text.pdf.PdfPTable = New iTextSharp.text.pdf.PdfPTable(10)
            table.WidthPercentage = 100.0F
            table.SetWidths(New Single() {14.0F, 8.0F, 8.0F, 10.0F, 10.0F, 10.0F, 10.0F, 10.0F, 8.0F, 12.0F})
            table.DefaultCell.Border = 0

            table.AddCell(fc_CeldaTexto("Nombre del estudiante:", 8.0F, 1, 0, 2, 1, 0))
            table.AddCell(fc_CeldaTexto(dtAlumno.Rows(0).Item(5), 9.0F, 0, 0, 5, 1, 0))
            table.AddCell(fc_CeldaTexto("Código:", 8.0F, 1, 0, 1, 1, 0))
            table.AddCell(fc_CeldaTexto(dtAlumno.Rows(0).Item(1), 9.0F, 0, 0, 2, 1, 0))

            table.AddCell(fc_CeldaTexto("Carrera Profesional:", 8.0F, 1, 0, 2, 1, 0))
            table.AddCell(fc_CeldaTexto(dtAlumno.Rows(0).Item(36), 9.0F, 0, 0, 5, 1, 0))
            table.AddCell(fc_CeldaTexto("Ciclo Académico:", 8.0F, 1, 0, 2, 1, 0))
            table.AddCell(fc_CeldaTexto(dtDet.Rows(0).Item(0), 9.0F, 0, 0, 2, 1, 0))
            'cabececer de la tabla
            table.AddCell(fc_CeldaTexto("CÓDIGO", 8.0F, 1, 15, 1, 1, 0))
            table.AddCell(fc_CeldaTexto("CICLO", 8.0F, 1, 15, 1, 1, 1))
            table.AddCell(fc_CeldaTexto("GRUPO", 8.0F, 1, 15, 1, 1, 1))
            table.AddCell(fc_CeldaTexto("ASIGNATURA", 8.0F, 1, 15, 6, 1, 1))
            table.AddCell(fc_CeldaTexto("CRÉDITOS", 8.0F, 1, 15, 1, 1, 1))
            'filas o detalle  de la tabla

            Dim sumaCredito As Integer = 0
            If dtDet.Rows.Count > 0 Then
                For i As Integer = 0 To dtDet.Rows.Count - 1
                    table.AddCell(fc_CeldaTexto(dtDet.Rows(i).Item(2), 8.0F, 0, 15, 1, 1, 1))
                    table.AddCell(fc_CeldaTexto(dtDet.Rows(i).Item(4), 8.0F, 0, 15, 1, 1, 1))
                    table.AddCell(fc_CeldaTexto(dtDet.Rows(i).Item(23), 8.0F, 0, 15, 1, 1, 1))
                    table.AddCell(fc_CeldaTexto(dtDet.Rows(i).Item(3), 8.0F, 0, 15, 6, 1, 0))
                    table.AddCell(fc_CeldaTexto(dtDet.Rows(i).Item(5), 8.0F, 0, 15, 1, 1, 1))
                    sumaCredito = CInt(dtDet.Rows(i).Item(5)) + sumaCredito
                Next
            End If
            'total
            table.AddCell(fc_CeldaTexto("Total Créditos", 8.0F, 1, 15, 9, 1, 2))
            table.AddCell(fc_CeldaTexto(CStr(sumaCredito), 8.0F, 0, 15, 1, 1, 1))
            'linea en blanco
            table.AddCell(fc_CeldaTexto("", 8.0F, 0, 0, 10, 1, 1))
            'fecha
            table.AddCell(fc_CeldaTexto("Fecha de Emisión:" & CDate(Now.Date), 8.0F, 0, 0, 10, 1, 2))

            'linea en blanco
            table.AddCell(fc_CeldaTexto("", 8.0F, 0, 0, 10, 1, 1))
            'agregamos al pdfTable
            pdfTable.AddCell(table)

            'Tabla pie del documento
            Dim tablePie As iTextSharp.text.pdf.PdfPTable = New iTextSharp.text.pdf.PdfPTable(2)
            tablePie.WidthPercentage = 100.0F
            tablePie.SetWidths(New Single() {50.0F, 50.0F})
            tablePie.DefaultCell.Border = 0

            tablePie.AddCell(fc_CeldaTexto(" ", 8.0F, 0, 0, 1, 1, 1))


            'celda del Sello
            Dim cellSello As iTextSharp.text.pdf.PdfPCell = New iTextSharp.text.pdf.PdfPCell(usatSello)
            cellSello.HorizontalAlignment = 1
            cellSello.VerticalAlignment = 0
            cellSello.Border = 0
            'agrego sello la tabla pie
            tablePie.AddCell(cellSello)

            tablePie.AddCell(fc_CeldaTexto(" ", 8.0F, 0, 0, 2, 1, 1))

            tablePie.AddCell(fc_CeldaTexto(" ", 8.0F, 0, 0, 1, 1, 1))
            tablePie.AddCell(fc_CeldaTexto(Environment.NewLine & "MSc. Martha Elina Tesén Arroyo" & Environment.NewLine & " " & Environment.NewLine & _
"Dirección Académica", 8.0F, 0, 1, 1, 1, 1))

            pdfTable.AddCell(tablePie)

            '''' agreo a la tabla general

            pdfDoc.Add(pdfTable)
            pdfDoc.Close()


        Catch ex As Exception
            Throw ex
        End Try

        Return 0
    End Function

    Public Function EmiteFichaNotas(ByVal memory As System.IO.MemoryStream, ByVal sourceIcon As String, ByVal serieCorrelativo As String, ByVal arreglo As Dictionary(Of String, String)) As Integer

        '''''''''''Cambiado

        Dim md_solicitud As New d_SolicitaDocumentacion
        Dim dtDet As New Data.DataTable
        Dim dtSolicitud As New Data.DataTable
        Dim dtAlumno As New Data.DataTable
        Dim codigo_alu As Integer
        Dim codigo_cac As Integer
        Dim descripcion_cac As String = ""
        Dim codigo_univ_alu As String = ""

        If arreglo.Item("codigo_sol") <> "0" Then
            dtSolicitud = md_solicitud.ListarSolicitaDocumentacion("", arreglo.Item("codigo_sol"), 0, "")
        End If

        If dtSolicitud.Rows.Count > 0 Then
            With dtSolicitud.Rows(0)
                codigo_alu = .Item("codigo_alu")
                codigo_cac = .Item("codigo_cac")
                descripcion_cac = .Item("referencia01")
                codigo_univ_alu = .Item("codigoUniver_Alu")
            End With
        End If
        If arreglo.Item("codigo_sol") <> "0" Then
            dtDet = fc_DatosFichaMatriculaNotas("FichaNotas", codigo_alu, codigo_cac)
            dtAlumno = fc_ConsultarAlumnoFichaByCU(codigo_univ_alu)
        Else
            dtDet = fc_DatosFichaMatriculaNotas("FichaNotas", arreglo.Item("codigo_Alu"), arreglo.Item("codigo_cac"))
            dtAlumno = fc_ConsultarAlumnoFichaByCU(arreglo.Item("codigoUniv_alu"))
        End If


        ''''''''''' Fin Cambiado

        Try

            Dim pdfDoc As New iTextSharp.text.Document(iTextSharp.text.PageSize.A4)
            Dim pdfWrite As iTextSharp.text.pdf.PdfWriter = iTextSharp.text.pdf.PdfWriter.GetInstance(pdfDoc, memory)
            pdfDoc.Open()

            If arreglo.Item("codigo_sol") = "0" Then mt_AddWaterMark(pdfWrite, "BORRADOR")

            Dim pdfTable As iTextSharp.text.pdf.PdfPTable = New iTextSharp.text.pdf.PdfPTable(1)
            pdfTable.WidthPercentage = 98.0F
            pdfTable.DefaultCell.Border = 0

            '---- tabla cabecera
            Dim cellTable0 As iTextSharp.text.pdf.PdfPTable = New iTextSharp.text.pdf.PdfPTable(3)
            cellTable0.WidthPercentage = 100.0F
            cellTable0.SetWidths(New Single() {18.0F, 42.0F, 40.0F})
            cellTable0.DefaultCell.Border = 0

            '---- icono usat
            Dim srcIcon As String = sourceIcon
            Dim usatIcon As iTextSharp.text.Image = iTextSharp.text.Image.GetInstance(srcIcon)
            usatIcon.ScalePercent(60.0F)
            usatIcon.Alignment = iTextSharp.text.Element.ALIGN_LEFT
            '---- sello
            Dim srcSello As String = arreglo.Item("sourceSello")
            Dim usatSello As iTextSharp.text.Image = iTextSharp.text.Image.GetInstance(srcSello)
            usatSello.ScalePercent(30.0F)
            usatSello.Alignment = iTextSharp.text.Element.ALIGN_RIGHT

            'Cabecera
            'celda del icono
            Dim cellIcon As iTextSharp.text.pdf.PdfPCell = New iTextSharp.text.pdf.PdfPCell(usatIcon)
            cellIcon.HorizontalAlignment = 0
            cellIcon.VerticalAlignment = 2
            cellIcon.Border = 0
            'cellIcon.Rowspan = 2
            'pdfTable.AddCell(cellIcon)

            cellTable0.AddCell(cellIcon)

            cellTable0.AddCell(fc_CeldaTexto("Universidad Católica Santo Toribio de Mogrovejo" & Environment.NewLine & " " & Environment.NewLine & _
                               "Dirección Académica", 9.0F, 1, 0, 1, 1, 0))

            '''''' titulo
            cellTable0.AddCell(fc_CeldaTexto("FICHA DE NOTAS" & Environment.NewLine & " " & Environment.NewLine & _
                               serieCorrelativo, 12.0F, 1, 0, 1, 1, 1))


            pdfTable.AddCell(cellTable0)

            ''''''
            'detalle
            Dim table As iTextSharp.text.pdf.PdfPTable = New iTextSharp.text.pdf.PdfPTable(10)
            table.WidthPercentage = 100.0F
            table.SetWidths(New Single() {14.0F, 8.0F, 8.0F, 10.0F, 10.0F, 10.0F, 10.0F, 10.0F, 8.0F, 12.0F})
            table.DefaultCell.Border = 0

            table.AddCell(fc_CeldaTexto("Nombre del estudiante:", 8.0F, 1, 0, 2, 1, 0))
            table.AddCell(fc_CeldaTexto(dtAlumno.Rows(0).Item(5), 9.0F, 0, 0, 5, 1, 0))
            table.AddCell(fc_CeldaTexto("Código:", 8.0F, 1, 0, 1, 1, 0))
            table.AddCell(fc_CeldaTexto(dtAlumno.Rows(0).Item(1), 9.0F, 0, 0, 2, 1, 0))

            table.AddCell(fc_CeldaTexto("Carrera Profesional:", 8.0F, 1, 0, 2, 1, 0))
            table.AddCell(fc_CeldaTexto(dtAlumno.Rows(0).Item(36), 9.0F, 0, 0, 5, 1, 0))
            table.AddCell(fc_CeldaTexto("Ciclo Académico:", 8.0F, 1, 0, 2, 1, 0))
            table.AddCell(fc_CeldaTexto(dtDet.Rows(0).Item(0), 9.0F, 0, 0, 2, 1, 0))
            'cabececer de la tabla
            table.AddCell(fc_CeldaTexto("CÓDIGO", 8.0F, 1, 15, 1, 1, 0))
            table.AddCell(fc_CeldaTexto("CRED.", 8.0F, 1, 15, 1, 1, 1))
            table.AddCell(fc_CeldaTexto("CICLO", 8.0F, 1, 15, 1, 1, 1))
            table.AddCell(fc_CeldaTexto("ASIGNATURA", 8.0F, 1, 15, 6, 1, 1))
            table.AddCell(fc_CeldaTexto("PROMEDIO", 8.0F, 1, 15, 1, 1, 1))

            Dim sumaCreditoAprob As Integer = 0
            Dim promedio As Double = 0
            Dim promedioPon As Double = 0
            Dim ponderado As Integer = 0
            If dtDet.Rows.Count > 0 Then
                For i As Integer = 0 To dtDet.Rows.Count - 1
                    table.AddCell(fc_CeldaTexto(dtDet.Rows(i).Item(2), 8.0F, 0, 15, 1, 1, 1))
                    table.AddCell(fc_CeldaTexto(dtDet.Rows(i).Item(5), 8.0F, 0, 15, 1, 1, 1))
                    table.AddCell(fc_CeldaTexto(dtDet.Rows(i).Item(4), 8.0F, 0, 15, 1, 1, 1))
                    table.AddCell(fc_CeldaTexto(dtDet.Rows(i).Item(3), 8.0F, 0, 15, 6, 1, 0))
                    table.AddCell(fc_CeldaTexto(dtDet.Rows(i).Item(6), 8.0F, 0, 15, 1, 1, 1))
                    If CDbl(dtDet.Rows(i).Item(6)) >= CDbl(dtDet.Rows(i).Item(8)) Then
                        sumaCreditoAprob = sumaCreditoAprob + CInt(dtDet.Rows(i).Item(5))
                    End If
                    ponderado = ponderado + dtDet.Rows(i).Item(5)
                    promedio = promedio + (CDbl(dtDet.Rows(i).Item(6)) * CDbl(dtDet.Rows(i).Item(5)))
                Next
                promedioPon = promedio / ponderado
            End If
            'linea en blanco
            table.AddCell(fc_CeldaTexto("", 8.0F, 0, 0, 10, 1, 1))
            'resumen
            table.AddCell(fc_CeldaTexto("Promedio Ponderado Semestral", 8.0F, 1, 15, 4, 1, 0))
            table.AddCell(fc_CeldaTexto(Math.Round(promedioPon, 2), 8.0F, 1, 15, 2, 1, 2))
            'table.AddCell(fc_CeldaTexto("", 8.0F, 0, 0, 4, 1, 2))
            table.AddCell(fc_CeldaTexto("Fecha de Emisión:" & CDate(Now.Date), 8.0F, 0, 0, 4, 1, 2))
            table.AddCell(fc_CeldaTexto("Nro. Total de Créditos Aprobados", 8.0F, 1, 15, 4, 1, 0))
            table.AddCell(fc_CeldaTexto(sumaCreditoAprob, 8.0F, 1, 15, 2, 1, 2))
            table.AddCell(fc_CeldaTexto("", 8.0F, 0, 0, 4, 1, 2))

            'linea en blanco
            table.AddCell(fc_CeldaTexto("", 8.0F, 0, 0, 10, 1, 1))
            'fecha
            table.AddCell(fc_CeldaTexto("Fecha de Emisión:" & CDate(Now.Date), 8.0F, 0, 0, 10, 1, 2))

            'linea en blanco
            table.AddCell(fc_CeldaTexto("", 8.0F, 0, 0, 10, 1, 1))
            'agregamos al pdfTable
            pdfTable.AddCell(table)

            'Tabla pie del documento
            Dim tablePie As iTextSharp.text.pdf.PdfPTable = New iTextSharp.text.pdf.PdfPTable(2)
            tablePie.WidthPercentage = 100.0F
            tablePie.SetWidths(New Single() {50.0F, 50.0F})
            tablePie.DefaultCell.Border = 0

            tablePie.AddCell(fc_CeldaTexto(" ", 8.0F, 0, 0, 1, 1, 1))


            'celda del Sello
            Dim cellSello As iTextSharp.text.pdf.PdfPCell = New iTextSharp.text.pdf.PdfPCell(usatSello)
            cellSello.HorizontalAlignment = 1
            cellSello.VerticalAlignment = 0
            cellSello.Border = 0
            'agrego sello la tabla pie
            tablePie.AddCell(cellSello)

            tablePie.AddCell(fc_CeldaTexto(" ", 8.0F, 0, 0, 2, 1, 1))

            tablePie.AddCell(fc_CeldaTexto(" ", 8.0F, 0, 0, 1, 1, 1))
            tablePie.AddCell(fc_CeldaTexto(Environment.NewLine & "MSc. Martha Elina Tesén Arroyo" & Environment.NewLine & " " & Environment.NewLine & _
"Dirección Académica", 8.0F, 0, 1, 1, 1, 1))

            pdfTable.AddCell(tablePie)

            '''' agreo a la tabla general

            pdfDoc.Add(pdfTable)
            pdfDoc.Close()


        Catch ex As Exception
            Throw ex
        End Try

        Return 0
    End Function

    Public Function EmiteActaSustentación(ByVal memory As System.IO.MemoryStream, ByVal sourceIcon As String, ByVal serieCorrelativo As String, ByVal arreglo As Dictionary(Of String, String)) As Integer
        Try

            Dim alumnos As String = ""
            'Dim dtDirector As New Data.DataTable
            Dim ultimaFila As Integer

            '************* Articulos / prefijos / sufijos
            Dim txtDel As String = ""
            Dim txtBachiller As String = ""
            Dim txtEl As String = ""
            Dim txtHa As String = ""
            Dim txtSexo As String = ""
            '********************************************

            'traigo los datos de los alumnos
            dt = New Data.DataTable
            dt = fc_DatosActaSustentacion(arreglo.Item("codigo_tes"))


            Dim dtDatos As New Data.DataTable
            dtDatos = fc_DatosActaSustentacion(arreglo.Item("codigo_tes"))

            If dt.Rows.Count > 0 Then
                ultimaFila = dt.Rows.Count - 1
                For i As Integer = 0 To dt.Rows.Count - 1
                    '-------sexo
                    If i = 0 Then
                        txtSexo = dt.Rows(i).Item("sexo_alu").ToString
                    Else
                        If txtSexo <> dt.Rows(i).Item("sexo_alu").ToString Then
                            txtSexo = "U"
                        End If
                    End If
                    '------ coma
                    If i = ultimaFila Then
                        alumnos = alumnos + dt.Rows(i).Item("alumno")
                    Else
                        alumnos = alumnos + dt.Rows(i).Item("alumno") & ", "
                    End If
                Next
                '''''''''
                If ultimaFila > 0 Then

                    txtBachiller = "Bachilleres"
                    If txtSexo = "F" Then
                        txtEl = "las"
                        txtDel = "de las"
                    Else
                        txtEl = "los"
                        txtDel = "de los"
                    End If
                    txtHa = "han"
                Else
                    If txtSexo = "F" Then
                        txtDel = "de la"
                        txtEl = "la"
                    Else
                        txtDel = "del"
                        txtEl = "el"
                    End If
                    txtBachiller = "Bachiller"
                    txtHa = "ha"
                End If
                'trae el director academi

                
            End If




            Dim pdfDoc As New iTextSharp.text.Document(iTextSharp.text.PageSize.A4)
            Dim pdfWrite As iTextSharp.text.pdf.PdfWriter = iTextSharp.text.pdf.PdfWriter.GetInstance(pdfDoc, memory)
            pdfDoc.Open()

            Dim pdfTable As iTextSharp.text.pdf.PdfPTable = New iTextSharp.text.pdf.PdfPTable(1)
            pdfTable.WidthPercentage = 85.0F
            pdfTable.DefaultCell.Border = 0

            '---- Cabecera
            Dim cellCabecera As iTextSharp.text.pdf.PdfPTable = New iTextSharp.text.pdf.PdfPTable(1)
            cellCabecera.WidthPercentage = 100.0F
            cellCabecera.SetWidths(New Single() {100.0F})
            cellCabecera.DefaultCell.Border = 0

            cellCabecera.AddCell(fc_CeldaTexto("SISTEMA DE LA GESTIÓN DE LA CALIDAD" & Environment.NewLine & " " & "CÓDIGO USAT-PM0702-D-02" & Environment.NewLine & "VERSIÓN:01" & Environment.NewLine & Environment.NewLine, 10.0, 1, 0, 1, 1, 2, -1, 6, ""))
            pdfTable.AddCell(cellCabecera)

            '------ logotipo
            Dim cellLogotipo As iTextSharp.text.pdf.PdfPTable = New iTextSharp.text.pdf.PdfPTable(1)
            cellLogotipo.WidthPercentage = 100.0F
            cellLogotipo.SetWidths(New Single() {100.0F})
            cellLogotipo.DefaultCell.Border = 0

            '---- icono usat
            Dim srcIcon As String = sourceIcon
            Dim usatIcon As iTextSharp.text.Image = iTextSharp.text.Image.GetInstance(srcIcon)
            usatIcon.ScalePercent(70.0F)
            usatIcon.Alignment = iTextSharp.text.Element.ALIGN_CENTER

            Dim cellIcon As iTextSharp.text.pdf.PdfPCell = New iTextSharp.text.pdf.PdfPCell(usatIcon)
            cellIcon.HorizontalAlignment = 1
            cellIcon.VerticalAlignment = 0
            cellIcon.Border = 0
            'cellIcon.Rowspan = 2
            'pdfTable.AddCell(cellIcon)

            cellLogotipo.AddCell(cellIcon)
            pdfTable.AddCell(cellLogotipo)

            '------ titulo de documento y correlativo
            Dim cellTituloCorrelativo As iTextSharp.text.pdf.PdfPTable = New iTextSharp.text.pdf.PdfPTable(1)
            cellTituloCorrelativo.WidthPercentage = 100.0F
            cellTituloCorrelativo.SetWidths(New Single() {100.0F})
            cellTituloCorrelativo.DefaultCell.Border = 0

            cellTituloCorrelativo.AddCell(fc_CeldaTexto("ACTA DE SUSTENTACION DE TESIS" & Environment.NewLine & " " & Environment.NewLine & _
                               serieCorrelativo, 10.0F, 1, 0, 1, 1, 1))

            pdfTable.AddCell(cellTituloCorrelativo)

            '----- Contenido
            Dim cellContenido As iTextSharp.text.pdf.PdfPTable = New iTextSharp.text.pdf.PdfPTable(1)
            cellContenido.WidthPercentage = 100.0F
            cellContenido.SetWidths(New Single() {100.0F})
            cellContenido.DefaultCell.Border = 0

            Dim _parrafoOne As iTextSharp.text.Phrase
            _parrafoOne = New iTextSharp.text.Phrase

            _parrafoOne.Add(fc_textFrase("En la ciudad de Chiclayo, a las [horas en número 00:00 a.m. / p.m. ] del día [fecha en números] de [mes] de [año en números], los miembros del jurado designados por la Escuela " & _
                                      "Profesional de " & dtDatos.Rows(0).Item("nombreOficial_cpf") & ", Presidente: " & dtDatos.Rows(0).Item("presidente") & ", Secretario: " & dtDatos.Rows(0).Item("secretario") & ", Vocal: " & dt.Rows(0).Item("vocal") & ", se reunieron en el ambiente " & dtDatos.Rows(0).Item("ambiente") & ", " & _
                                      "para recibir la sustentación de la Tesis titulado: " & dtDatos.Rows(0).Item("titulo_tes") & ", " & txtDel & " " & txtBachiller & " " & _
                                      "" & alumnos & "" & Environment.NewLine & " " & Environment.NewLine & " " & _
                                      "Siendo las [horas en número 00:00 a.m. / p.m.], habiéndose concluido la exposición y absueltas las preguntas del jurado, se acordó otorgar al/a la estudiante la calificación de [nota en números], recibiendo la categoría de [DISTINGUIDO/SOBRESALIENTE/NOTABLE/APROBADO/ DESAPROBADO EN MAYÚSCULAS]. ", 5, 10.0F, 0, iTextSharp.text.BaseColor.BLACK))


            cellContenido.AddCell(fc_CeldaTextoPhrase(_parrafoOne, 0, 1, 1, 3, 0, 5, ""))
            '----- fecha
            'linea en blanco
            cellContenido.AddCell(fc_CeldaTexto("", 8.0F, 0, 0, 1, 1, 1))
            cellContenido.AddCell(fc_CeldaTexto("", 8.0F, 0, 0, 1, 1, 1))
            cellContenido.AddCell(fc_CeldaTexto("", 8.0F, 0, 0, 1, 1, 1))

            cellContenido.AddCell(fc_CeldaTexto("Chiclayo, " & Day(Now) & " de " & Month(Now) & " del " & Year(Now), 10.0F, 0, 0, 1, 1, 2))
            pdfTable.AddCell(cellContenido)

            '------- firmas

            Dim cellFirmas As iTextSharp.text.pdf.PdfPTable = New iTextSharp.text.pdf.PdfPTable(3)
            cellFirmas.WidthPercentage = 100.0F
            cellFirmas.SetWidths(New Single() {45.0F, 10.0F, 45.0F})
            cellFirmas.DefaultCell.Border = 0

            ''linea en blanco
            cellFirmas.AddCell(fc_CeldaTexto("", 8.0F, 0, 0, 3, 1, 1))
            cellFirmas.AddCell(fc_CeldaTexto("", 8.0F, 0, 0, 3, 1, 1))
            cellFirmas.AddCell(fc_CeldaTexto("", 8.0F, 0, 0, 3, 1, 1))
            cellFirmas.AddCell(fc_CeldaTexto(dtDatos.Rows(0).Item("presidente") & Environment.NewLine & "Presidente del Jurado", 8.0F, 0, 0, 1, 1, 1))
            cellFirmas.AddCell(fc_CeldaTexto("", 8.0F, 0, 0, 1, 1, 1))
            cellFirmas.AddCell(fc_CeldaTexto(dtDatos.Rows(0).Item("secretario") & Environment.NewLine & "Secretario del Jurado", 8.0F, 0, 0, 1, 1, 1))
            cellFirmas.AddCell(fc_CeldaTexto("", 8.0F, 0, 0, 3, 1, 1))
            cellFirmas.AddCell(fc_CeldaTexto("", 8.0F, 0, 0, 3, 1, 1))
            cellFirmas.AddCell(fc_CeldaTexto(dtDatos.Rows(0).Item("vocal") & Environment.NewLine & " Vocal del Jurado ", 8.0F, 0, 0, 3, 1, 1))
            pdfTable.AddCell(cellFirmas)

            pdfDoc.Add(pdfTable)
            pdfDoc.Close()

        Catch ex As Exception

            cnx.AbortarTransaccion()
            Throw ex

        End Try
    End Function

    
    Private Function fc_CeldaTexto(ByVal _text As String, ByVal _size As Single, ByVal _style As Integer, ByVal _border As Integer, _
                                ByVal _colspan As Integer, ByVal _rowspan As Integer, ByVal _haligment As Integer, _
                                Optional ByVal _valigment As Integer = -1, Optional ByVal _padding As Integer = 6, _
                                Optional ByVal _backgroundcolor As String = "") As iTextSharp.text.pdf.PdfPCell
        Dim celdaITC As iTextSharp.text.pdf.PdfPCell
        Dim fontITC As iTextSharp.text.Font

        'fontITC = New iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, _size, _style)
        Dim segoe As iTextSharp.text.pdf.BaseFont = iTextSharp.text.pdf.BaseFont.CreateFont(_fuente, iTextSharp.text.pdf.BaseFont.IDENTITY_H, iTextSharp.text.pdf.BaseFont.EMBEDDED)
        'If _fontColor <> "" Then
        'fontITC = New iTextSharp.text.Font(segoe, _size, _style, iTextSharp.text.BaseColor.GRAY)
        'Else
        fontITC = New iTextSharp.text.Font(segoe, _size, _style)
        'End If


        celdaITC = New iTextSharp.text.pdf.PdfPCell(New iTextSharp.text.Phrase(_text, fontITC))
        celdaITC.Border = _border
        celdaITC.Colspan = _colspan
        celdaITC.Rowspan = _rowspan
        celdaITC.HorizontalAlignment = _haligment
        celdaITC.VerticalAlignment = _valigment
        celdaITC.Padding = _padding
        If _backgroundcolor <> "" Then celdaITC.BackgroundColor = New iTextSharp.text.BaseColor(System.Drawing.Color.FromName(_backgroundcolor))
        'celdaITC.SetLeading(0.0F, 1.15F)
        Return celdaITC
    End Function

    Private Sub mt_AddWaterMark(ByVal pdfwrite As iTextSharp.text.pdf.PdfWriter, ByVal texto As String)
        Dim bfTimes As iTextSharp.text.pdf.BaseFont = iTextSharp.text.pdf.BaseFont.CreateFont(iTextSharp.text.pdf.BaseFont.TIMES_ITALIC, iTextSharp.text.pdf.BaseFont.CP1252, False)
        Dim times As iTextSharp.text.Font = New iTextSharp.text.Font(bfTimes, 145.5F, iTextSharp.text.Font.ITALIC, iTextSharp.text.BaseColor.LIGHT_GRAY)
        iTextSharp.text.pdf.ColumnText.ShowTextAligned(pdfwrite.DirectContent, 1, New iTextSharp.text.Phrase(texto, times), 295.5F, 450.0F, 55)
    End Sub

    Public Function DescargarArchivo(ByVal IdArchivo As Long, ByVal idTabla As Integer, ByVal token As String, ByVal usuario As String) As Byte()
        Try
            Dim wsCloud As New ClsArchivosCompartidosV2
            Dim list As New Dictionary(Of String, String)
            Dim obj As New ClsConectarDatos
            Dim tb As New Data.DataTable
            Dim bytes As Byte() = Nothing

            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            tb = obj.TraerDataTable("ArchivosCompartidos_Listar2", 1, idTabla, IdArchivo, token)
            obj.CerrarConexion()
            If tb.Rows.Count = 0 Then Throw New Exception("¡ Archivo no encontrado !")

            list.Add("IdArchivo", tb.Rows(0).Item("IdArchivo").ToString)
            list.Add("Usuario", usuario)
            list.Add("Token", token)

            Dim envelope As String = wsCloud.SoapEnvelopeDescarga(list)
            Dim result As String = wsCloud.PeticionRequestSoap(ruta, envelope, "http://usat.edu.pe/DownloadFile", usuario)
            Dim imagen As String = fc_ResultFile(result)

            If tb.Rows.Count > 0 Then
                Dim extencion As String
                extencion = tb.Rows(0).Item("Extencion")
                Select Case tb.Rows(0).Item("Extencion")
                    Case ".txt"
                        extencion = "text/plain"
                    Case ".doc"
                        extencion = "application/ms-word"
                    Case ".xls"
                        extencion = "application/vnd.ms-excel"
                    Case ".gif"
                        extencion = "image/gif"
                    Case ".jpg"
                    Case ".jpeg"
                    Case "jpeg"
                        extencion = "image/jpeg"
                    Case "png"
                        extencion = "image/png"
                    Case ".bmp"
                        extencion = "image/bmp"
                    Case ".wav"
                        extencion = "audio/wav"
                    Case ".ppt"
                        extencion = "application/mspowerpoint"
                    Case ".dwg"
                        extencion = "image/vnd.dwg"
                    Case ".pdf"
                        extencion = "application/pdf"
                    Case Else
                        extencion = "application/octet-stream"
                End Select

                bytes = Convert.FromBase64String(imagen)
                'Response.Clear()
                'Response.Buffer = False
                'Response.Charset = ""
                'Response.Cache.SetCacheability(HttpCacheability.NoCache)
                'Response.ContentType = extencion
                'Response.AddHeader("content-disposition", "attachment;filename=" & tb.Rows(0).Item("NombreArchivo").ToString.Replace(",", ""))
                'Response.AppendHeader("Content-Length", bytes.Length.ToString())
                'Response.BinaryWrite(bytes)
                'Response.End()

            End If

            Return bytes
            'Response.Write(envelope)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Function fc_ResultFile(ByVal cadXml As String) As String
        Try
            Dim xError As String()
            Dim nsMgr As System.Xml.XmlNamespaceManager
            Dim xml As System.Xml.XmlDocument = New System.Xml.XmlDocument()
            xml.LoadXml(cadXml)
            nsMgr = New System.Xml.XmlNamespaceManager(xml.NameTable)
            nsMgr.AddNamespace("soap", "http://schemas.xmlsoap.org/soap/envelope/")
            Dim res As System.Xml.XmlNode = xml.DocumentElement.SelectSingleNode("/soap:Envelope/soap:Body", nsMgr)
            xError = res.InnerText.Split(":")

            If xError.Length = 2 Then
                Throw New Exception(res.InnerText)
            End If

            Return res.InnerText
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Function fc_SubirArchivo(ByVal idTabla As String, ByVal codigo As String, ByVal nro_operacion As String, ByVal file As System.IO.MemoryStream, ByVal name As String, ByVal usuario As String) As String

        Dim list As New Dictionary(Of String, String)
        Try
            Dim _TablaId As String = idTabla
            Dim _TransaccionId As String = codigo
            Dim _Fecha As String = Date.Now.ToString("dd/MM/yyyy")
            Dim _Usuario As String = usuario

            Dim binData As Byte() = file.ToArray
            Dim base64 As Object = System.Convert.ToBase64String(binData)
            Dim _Nombre As String = name.Replace("&", "_").Replace("'", "_").Replace("*", "_")

            Dim wsCloud As New ClsArchivosCompartidos

            list.Add("Fecha", _Fecha)
            list.Add("Extencion", ".pdf")
            list.Add("Nombre", _Nombre)
            list.Add("TransaccionId", _TransaccionId)
            list.Add("TablaId", _TablaId)
            list.Add("NroOperacion", nro_operacion)
            list.Add("Archivo", System.Convert.ToBase64String(binData, 0, binData.Length))
            list.Add("Usuario", _Usuario)
            list.Add("Equipo", "")
            list.Add("Ip", "")
            list.Add("param8", _Usuario)

            Dim envelope As String = wsCloud.SoapEnvelope(list)
            Dim result As String = wsCloud.PeticionRequestSoap(ruta, envelope, "http://usat.edu.pe/UploadFile", usuario)

            Return result
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Function fc_textFrase(ByVal _text As String, ByVal _fontfamily As Integer, ByVal _size As Single, ByVal _style As Integer, ByVal _fontcolor As iTextSharp.text.BaseColor) As iTextSharp.text.Chunk
        Dim _chunk As iTextSharp.text.Chunk
        Dim _font As iTextSharp.text.Font
        If _fontfamily = 5 Then
            Dim segoe As iTextSharp.text.pdf.BaseFont = iTextSharp.text.pdf.BaseFont.CreateFont(_fuente, iTextSharp.text.pdf.BaseFont.IDENTITY_H, iTextSharp.text.pdf.BaseFont.EMBEDDED)
            _font = New iTextSharp.text.Font(segoe, _size, _style, _fontcolor)
        Else
            _font = New iTextSharp.text.Font(_fontfamily, _size, _style, _fontcolor)
            '_font.SetStyle(4)
        End If
        _chunk = New iTextSharp.text.Chunk(_text, _font)
        '_chunk.SetUnderline(
        Return _chunk
    End Function

    Private Function fc_CeldaTextoPhrase(ByVal _phrase As iTextSharp.text.Phrase, ByVal _border As Integer, ByVal _colspan As Integer, ByVal _rowspan As Integer, _
                                   ByVal _haligment As Integer, Optional ByVal _valigment As Integer = -1, Optional ByVal _padding As Integer = 6, _
                                 Optional ByVal _backgroundcolor As String = "") As iTextSharp.text.pdf.PdfPCell
        Dim _celda As iTextSharp.text.pdf.PdfPCell
        _celda = New iTextSharp.text.pdf.PdfPCell(_phrase)
        _celda.Border = _border
        _celda.Colspan = _colspan
        _celda.Rowspan = _rowspan
        _celda.HorizontalAlignment = _haligment
        _celda.VerticalAlignment = _valigment
        _celda.Padding = _padding
        _celda.SetLeading(0.0F, 2.0F)
        If _backgroundcolor <> "" Then _celda.BackgroundColor = New iTextSharp.text.BaseColor(System.Drawing.Color.FromName(_backgroundcolor))
        Return _celda
    End Function

    Public Function fc_DatosInformeAsesor(ByVal codigo_tes As Integer) As Data.DataTable
        Try
            cnx = New ClsConectarDatos
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.IniciarTransaccion()

            'Ejecutar Procedimiento
            dt = cnx.TraerDataTable("DOC_DatosInformeAsesor", codigo_tes)

            cnx.TerminarTransaccion()

            Return dt
        Catch ex As Exception
            cnx.AbortarTransaccion()
            Throw ex
        End Try
    End Function

    Public Function fc_ListarDirectorAcademico(ByVal codigo As Integer, ByVal tipo As String) As Data.DataTable
        Try
            cnx = New ClsConectarDatos
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.IniciarTransaccion()
            'ejecutar procedimiento
            dt = cnx.TraerDataTable("ACAD_ListarDirectorAcademico", codigo, tipo)
            Return dt
        Catch ex As Exception
            cnx.AbortarTransaccion()
            Throw ex
        End Try
    End Function

    Public Function fc_DatosResolSustentacion(ByVal codigo_tes As Integer) As Data.DataTable
        Try
            cnx = New ClsConectarDatos
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.IniciarTransaccion()

            'Ejecutar Procedimiento
            dt = cnx.TraerDataTable("DOC_DatosResolucionSustentacion", codigo_tes)

            cnx.TerminarTransaccion()

            Return dt
        Catch ex As Exception
            cnx.AbortarTransaccion()
            Throw ex
        End Try
    End Function
    Public Function fc_DatosActaSustentacion(ByVal codigo_tes As Integer) As Data.DataTable
        Try
            cnx = New ClsConectarDatos
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.IniciarTransaccion()

            'Ejecutar Procedimiento
            dt = cnx.TraerDataTable("DOC_DatosActaSustentacion", codigo_tes)

            cnx.TerminarTransaccion()

            Return dt
        Catch ex As Exception
            cnx.AbortarTransaccion()
            Throw ex
        End Try
    End Function
    Private Function fc_DatosFichaMatriculaNotas(ByVal tipoPrint As String, ByVal codigo_alu As Integer, ByVal codigo_cac As Integer) As Data.DataTable
        Dim dt As New Data.DataTable

        Try
            Dim objcnx As New ClsConectarDatos
            objcnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            objcnx.AbrirConexion()

            If tipoPrint = "FichaMatricula" Then ' si es ficha de matrícula
                dt = objcnx.TraerDataTable("ConsultarNotas", "BA", codigo_alu, codigo_cac, "0")
            Else 'ficha de notas
                dt = objcnx.TraerDataTable("ACAD_FichaNotas", codigo_alu, codigo_cac)
            End If

            objcnx.CerrarConexion()


        Catch ex As Exception
            dt = Nothing
            Throw ex
        End Try

        Return dt

    End Function
    Private Function fc_ConsultarAlumnoFichaByCU(ByVal codigoUniver_Alu As String) As Data.DataTable
        Dim dt As New Data.DataTable
        Try
            Dim objcnx As New ClsConectarDatos
            objcnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            objcnx.AbrirConexion()

            dt = objcnx.TraerDataTable("ConsultarAlumno", "CU", codigoUniver_Alu)
            objcnx.CerrarConexion()

        Catch ex As Exception
            dt = Nothing
            Throw ex
        End Try
        Return dt
    End Function

End Class
