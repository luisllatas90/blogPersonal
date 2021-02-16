Imports Microsoft.VisualBasic
Imports System.Collections.Generic
Imports iTextSharp.text.html
Imports System.IO



Public Class clsGeneraDocumento
#Region "variables"
    Private cnx As ClsConectarDatos
    Private dt As Data.DataTable

    Dim md_configuraDoc As d_ConfigurarDocumentoArea
    Private ruta As String = ConfigurationManager.AppSettings("SharedFiles") '"http://localhost/campusvirtual/ArchivosCompartidos/SharedFiles.asmx"
    Private _fuente As String
    Dim server As HttpServerUtility = HttpContext.Current.Server


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
            Case "AutorPublTesis"
                Call EmiteAutorPublicTesis(memory, sourceIcon, serieCorrelativoDoc, arreglo)
            Case "ActaTrabajoBachiller"
                Call EmiteActaAprobacionTrabajoInvestigacion(memory, sourceIcon, serieCorrelativoDoc, arreglo)
            Case "ActaEvaluacion"
                Call EmiteActaEvaluacionNotas(memory, sourceIcon, serieCorrelativoDoc, arreglo)
            Case "ResolucionOtorgaGrado"
                Call EmiteResolucionOtorgaGrado(memory, sourceIcon, serieCorrelativoDoc, arreglo)
            Case "ResolucionOtorgaTitulo"
                Call EmiteResolucionOtorgaTitulo(memory, sourceIcon, serieCorrelativoDoc, arreglo)
            Case "ResolConsUnivOtorgaGrado"
                Call EmiteResolConsUnivOtorgaGrado(memory, sourceIcon, serieCorrelativoDoc, arreglo)
            Case "ResolConsUnivOtorgaTitulo"
                Call EmiteResolConsUnivOtorgaTitulo(memory, sourceIcon, serieCorrelativoDoc, arreglo)
            Case "CertActFormComp"
                Call EmiteCertificadoActividadesFormacionComplementarias(memory, sourceIcon, serieCorrelativoDoc, arreglo)
            Case "CartaCompromisoAsesoramientoTesis"
                Call EmiteCartaCompromisoAsesoramientoTesis(memory, sourceIcon, serieCorrelativoDoc, arreglo)
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
            Dim txtDirector As String = "" '---director / directora
            Dim txtMension As String = ""
            Dim txtEscuela As String = ""
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
               
                'trae el director de escuela postGrado / mencion / escuela
                If arreglo.Item("tipoEstudio") = "5" Then '--- si el tipo de estudio es 5 post grado
                    dtDirector = fc_ListarDirectorAcademico("672", "CO")  '' 672 es el centro de costos de: DIRECCIÓN DE ESCUELA DE POSGRADO                    
                Else
                    'trae el director academico -- Director de escuela
                    dtDirector = fc_ListarDirectorAcademico(dt.Rows(0).Item("codigo_cpf"), "E") '-- E: director de esccuela                    
                End If
                '---- director / directora
                If dtDirector.Rows.Count > 0 Then
                    If dtDirector.Rows(0).Item("sexo_per") = "F" Then
                        txtDirector = "Directora"
                    Else
                        txtDirector = "Director"
                    End If
                End If
                'trae mencion / escuela
                If arreglo.Item("tipoEstudio") = "5" Then '--- si el tipo de estudio es 5 post grado
                    txtMension = txtDirector & " de Escuela de Posgrado"
                    txtEscuela = "Escuela de Posgrado"
                Else
                    txtMension = txtDirector & " de Escuela del Programa de " & dtDirector.Rows(0).Item("nombre_Cpf")
                    txtEscuela = dtDatos.Rows(0).Item("nombre_Cpf")
                End If
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
            cellDeParaAsunto.AddCell(fc_CeldaTexto(dtDirector.Rows(0).Item("apellidoPat_per") & " " & dtDirector.Rows(0).Item("apellidoMat_per") & " " & dtDirector.Rows(0).Item("nombres_per") & Environment.NewLine & txtMension, 9.0F, 0, 0, 1, 1, 0))

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

            _parrafoOne.Add(fc_textFrase("Tengo a bien dirigirme a Ud. con la finalidad de comunicarle que " & txtEl & " " & txtBachiller & " de " & txtEscuela & ", " & alumnos & ", " & _
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
            dt = fc_DatosResolSustentacion(arreglo.Item("codigo_datos"))
            '************* Articulos / prefijos / sufijos
            Dim txtBachiller As String = ""
            Dim txtQuien As String = ""
            Dim sexoDirector As String = ""
            Dim txtAl As String = ""
            Dim txtDecano As String = ""
            Dim txtApto As String = ""
            Dim txtSexo As String = ""
            Dim txtPrograma As String = "" '-- carrera profesional / programa de maestría
            Dim txtObtencion As String = "" '--- del titulo / del grado de maestro
            Dim txtEscuela As String = "" '--- escuela del director de escuela
            Dim txtPrefEscuela As String = "" '-- de la Escuela de / program de maestria
            Dim txtDirector As String = "" '--- director / directora
            Dim txtVocal2 As String = "" '--- 2(segundo vocal en caso de doctorado)
            Dim txtExpedientes As String = "" ' el expediente  / los Expedientes 
            Dim txtTramites As String = ""
            Dim txtPerteneciente As String = "" ' perteneciente
            Dim txtAmbiente As String = ""
            '********************************************
            Dim dtDatos As New Data.DataTable
            dtDatos = fc_DatosResolSustentacion(arreglo.Item("codigo_datos"))

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
                        txtTramites = txtTramites + dtDatos.Rows(i).Item("glosaCorrelativo_trl")
                    Else
                        alumnosDNI = alumnosDNI + dt.Rows(i).Item("alumno") & " con DNI " & dt.Rows(i).Item("nroDocIdent_Alu") & ", "
                        alumnos = alumnos + dt.Rows(i).Item("alumno") & ", "
                        txtTramites = txtTramites + dtDatos.Rows(i).Item("glosaCorrelativo_trl") & ", "
                    End If
                Next

                If ultimaFila > 0 Then
                    If dtDatos.Rows(0).Item("codigo_test") = "5" And dtDatos.Rows(0).Item("codigo_stest") = "2" Then
                        txtBachiller = "maestros"
                    Else
                        txtBachiller = "bachilleres"
                    End If

                    txtQuien = "quienes solicitan"
                    If txtSexo = "F" Then
                        txtAl = "a las"
                        txtApto = "aptas"
                    Else
                        txtAl = "a los"
                        txtApto = "aptos"
                    End If
                    txtExpedientes = "los Expedientes Nros. "
                    txtPerteneciente = "pertenecientes a"
                Else
                    If dtDatos.Rows(0).Item("codigo_test") = "5" And dtDatos.Rows(0).Item("codigo_stest") = "2" Then
                        If txtSexo = "F" Then
                            txtBachiller = "maestra"
                        Else
                            txtBachiller = "maestro"
                        End If

                    Else
                        txtBachiller = "bachiller"
                    End If
                    'txtBachiller = "bachiller"
                    txtQuien = "quien solicita"
                    If txtSexo = "F" Then
                        txtAl = "a la"
                        txtApto = "apta"
                    Else
                        txtAl = "al"
                        txtApto = "apto"
                    End If
                    txtExpedientes = "el Expediente Nº"
                    txtPerteneciente = "perteneciente a"
                End If
                If dtDatos.Rows(0).Item("codigo_test") = "5" Then '---- si es tipo de estudio posgrado

                    dtDirector = fc_ListarDirectorAcademico(672, "CO") '--CO director de esccuela / posgrado / 672 es el centro de costos de posgrado

                Else
                    dtDirector = fc_ListarDirectorAcademico(dt.Rows(0).Item("codigo_cpf"), "E") '-- E: director de esccuela / pregrado
                End If
                'trae el director academico -- Director de escuela
                Try
                    If dtDirector.Rows(0).Item("sexo_per") = "M" Then
                        sexoDirector = "el Director"
                        txtDirector = "Director"
                    Else
                        sexoDirector = "la Directora"
                        txtDirector = "Directora"
                    End If
                Catch ex As Exception
                    Throw New Exception("No se encuentra configurado el Director de Escuela", ex)
                End Try
                

                'dtDecano = fc_ListarDirectorAcademico(dt.Rows(0).Item("codigo_Fac"), "F") '-- F: Decano

                If dtDatos.Rows(0).Item("codigo_amb") = "0" Then
                    txtAmbiente = "VIRTUAL"
                Else
                    txtAmbiente = dtDatos.Rows(0).Item("ambiente")
                End If

            End If
           



            ''-----------------------------------------acodicionando frases al tipo de estudio ------------------------------------------------------------------------
            If dtDatos.Rows(0).Item("codigo_test") = "5" Then '---- si es tipo de estudio posgrado

                txtPrograma = dtDatos.Rows(0).Item("NombreOficial_cpf")
                txtObtencion = "del grado de maestro"
                txtEscuela = "Posgrado"
                txtPrefEscuela = "del Programa de"
                ''para el segundo vocal en caso de doctorado
                If dtDatos.Rows(0).Item("codigo_stest") = "2" Then
                    txtVocal2 = "● Vocal: " & dtDatos.Rows(0).Item("vocal2")
                End If
            Else

                dtDecano = fc_ListarDirectorAcademico(dtDatos.Rows(0).Item("codigo_Fac"), "F")
                Try
                    If dtDecano.Rows(0).Item("sexo_per") = "M" Then
                        txtDecano = "Decano"
                    Else
                        txtDecano = "Decana"
                    End If
                Catch ex As Exception
                    Throw New Exception("No se encuentra configurado el Decano de Facultad", ex)
                End Try
                
                txtPrograma = dtDatos.Rows(0).Item("NombreOficial_cpf")
                txtObtencion = "de titulación"
                txtEscuela = dtDirector.Rows(0).Item("nombre_cpf")
                txtPrefEscuela = "de la Escuela de"
            End If


            ''********************Principal*************************************************************************
            Dim pdfDoc As New iTextSharp.text.Document(iTextSharp.text.PageSize.A4)
            Dim pdfWrite As iTextSharp.text.pdf.PdfWriter = iTextSharp.text.pdf.PdfWriter.GetInstance(pdfDoc, memory)
            pdfDoc.Open()

            Dim pdfTable As iTextSharp.text.pdf.PdfPTable = New iTextSharp.text.pdf.PdfPTable(1)
            pdfTable.WidthPercentage = 85.0F
            pdfTable.DefaultCell.Border = 0

            ''********************Principal*************************************************************************
            If serieCorrelativo = "" Then mt_AddWaterMark(pdfWrite, "BORRADOR")
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

            If dtDatos.Rows(0).Item("codigo_test") <> "5" Then 'cuando sea diferente de posgrado
                cellTituloCorrelativo.AddCell(fc_CeldaTexto("CONSEJO DE FACULTAD" & Environment.NewLine, 10.0F, 1, 0, 1, 1, 1))
            End If
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

            _parrafoOne.Add(fc_textFrase("Visto " & txtExpedientes & " " & txtTramites & " " & txtPerteneciente & " " & alumnos & ", " & txtBachiller & " de " & txtPrograma & " " & txtQuien & " sustentación de tesis con fines " & txtObtencion & " y; " & Environment.NewLine & Environment.NewLine & _
                                     "CONSIDERANDO: " & Environment.NewLine & Environment.NewLine & _
                                     "Que mediante el Informe del " & dtDatos.Rows(0).Item("fechainformeasesor") & ", el Asesor de tesis, " & dtDatos.Rows(0).Item("asesor") & ", da conformidad a la tesis denominada: " & dtDatos.Rows(0).Item("titulo_tes") & ". " & Environment.NewLine & _
                                     "Que " & sexoDirector & " de la Escuela de " & txtEscuela & ", " & dtDirector.Rows(0).Item("grado") & " " & dtDirector.Rows(0).Item("nombres_per") & " " & dtDirector.Rows(0).Item("apellidoPat_per") & " " & dtDirector.Rows(0).Item("apellidoMat_per") & ", ha declarado " & txtAl & " " & txtBachiller & " como " & txtApto & " y ha cumplido con los requisitos establecidos por el Reglamento de Grados y Títulos de la Universidad Católica Santo Toribio de Mogrovejo; " & Environment.NewLine & _
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
            _parrafoTwo.Add(fc_textFrase("Declarar expedito " & txtAl & " " & txtBachiller & " " & alumnos & " " & txtPrefEscuela & " " & txtPrograma & ", para la sustentación de la tesis denominada: " & dtDatos.Rows(0).Item("titulo_tes") & ".", 5, 10.0F, 0, iTextSharp.text.BaseColor.BLACK))
            cellContenidoT1.AddCell(fc_CeldaTextoPhrase(_parrafoTwo, 0, 1, 1, 3, 0, 5, ""))
            pdfTable.AddCell(cellContenidoT1)

            cellContenidoT2.AddCell(fc_CeldaTexto("Articulo 2°-", 9.0F, 0, 0, 1, 1, 1))
            _parrafoTree.Add(fc_textFrase("Designar como miembros del jurado de tesis a los siguientes docentes: " & Environment.NewLine & _
                                          "● Presidente: " & dtDatos.Rows(0).Item("presidente") & "." & Environment.NewLine & _
                                          "● Secretario: " & dtDatos.Rows(0).Item("secretario") & "." & Environment.NewLine & _
                                          "● Vocal: " & dtDatos.Rows(0).Item("vocal") & "." & Environment.NewLine & _
                                          "" & txtVocal2 & ".", 5, 10.0F, 0, iTextSharp.text.BaseColor.BLACK))
            cellContenidoT2.AddCell(fc_CeldaTextoPhrase(_parrafoTree, 0, 1, 1, 3, 0, 5, ""))
            pdfTable.AddCell(cellContenidoT2)

            cellContenidoT3.AddCell(fc_CeldaTexto("Articulo 3°-", 9.0F, 0, 0, 1, 1, 1))
            _parrafoFor.Add(fc_textFrase("Fijar como fecha de sustentación el día " & dtDatos.Rows(0).Item("fechaprogramacion") & " a las " & dtDatos.Rows(0).Item("horaprogramacion") & " en el ambiente " & txtAmbiente & " de la Universidad Católica Santo Toribio de Mogrovejo. " & Environment.NewLine, 5, 10.0F, 0, iTextSharp.text.BaseColor.BLACK))
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

            If dtDatos.Rows(0).Item("codigo_test") <> "5" Then ' diferente a pos grado
                cellFirmas.AddCell(fc_CeldaTexto(dtDecano.Rows(0).Item("grado") & " " & dtDecano.Rows(0).Item("nombres_per") & " " & dtDecano.Rows(0).Item("apellidoPat_per") & " " & dtDecano.Rows(0).Item("apellidoMat_per") & Environment.NewLine & txtDecano & Environment.NewLine & " Facultad de " & dtDecano.Rows(0).Item("nombre_Fac") & " ", 9.0F, 0, 0, 3, 1, 1))
                'cellFirmas.AddCell(fc_CeldaTexto("Decano", 8.0F, 0, 0, 3, 1, 1))
                'cellFirmas.AddCell(fc_CeldaTexto("Facultad de [Nombre de facultad]", 8.0F, 0, 0, 3, 1, 1))

            Else ' posgrado
                cellFirmas.AddCell(fc_CeldaTexto(dtDirector.Rows(0).Item("grado") & " " & dtDirector.Rows(0).Item("nombres_per") & " " & dtDirector.Rows(0).Item("apellidoPat_per") & " " & dtDirector.Rows(0).Item("apellidoMat_per") & Environment.NewLine & txtDirector & " de Escuela de Posgrado" & Environment.NewLine & " Escuela de Posgrado ", 9.0F, 0, 0, 3, 1, 1))
            End If
            pdfTable.AddCell(cellFirmas)

            pdfDoc.Add(pdfTable)
            pdfDoc.Close()

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function EmiteResolucionOtorgaGrado(ByVal memory As System.IO.MemoryStream, ByVal sourceIcon As String, ByVal serieCorrelativo As String, ByVal arreglo As Dictionary(Of String, String)) As Integer
        Try
            Dim alumnosDNI As String = ""
            Dim alumnos As String = ""
            Dim dtDirector As New Data.DataTable
            Dim dtDecano As New Data.DataTable
            'traigo los datos de los alumnos
            Dim ultimaFila As Integer
            dt = New Data.DataTable
            dt = fc_DatosResolFacOtorgaGrado(arreglo.Item("codigo_datos"))
            '************* Articulos / prefijos / sufijos
            Dim txtBachiller As String = ""
            Dim txtQuien As String = ""
            Dim sexoDirector As String = ""
            Dim txtAl As String = ""
            Dim txtDecano As String = ""
            Dim txtApto As String = ""
            Dim txtSexo As String = ""
            Dim txtEgresado As String = ""
            Dim txtEncargado As String = ""
            '********************************************
            Dim dtDatos As New Data.DataTable
            dtDatos = fc_DatosResolFacOtorgaGrado(arreglo.Item("codigo_datos"))

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
                        txtEgresado = "egresada"
                    Else
                        txtAl = "a los"
                        txtApto = "aptos"
                        txtEgresado = "egresado"
                    End If
                Else
                    txtBachiller = "bachiller"
                    txtQuien = "quien solicita"
                    If txtSexo = "F" Then
                        txtAl = "a la"
                        txtEgresado = "egresada"
                        txtApto = "apta"
                    Else
                        txtAl = "al"
                        txtEgresado = "egresado"
                        txtApto = "apto"
                    End If

                End If
                'trae el director academico -- Director de escuela
                dtDirector = fc_ListarDirectorAcademico(dt.Rows(0).Item("codigo_cpf"), "E") '-- E: director de esccuela
                Try
                    If dtDirector.Rows(0).Item("sexo_per") = "M" Then
                        sexoDirector = "el Director encargado"
                    Else
                        sexoDirector = "la Directora encargada"
                    End If
                Catch ex As Exception
                    Throw New Exception("No se encuentra configurado el Director de Escuela", ex)
                End Try
                
                'dtDecano = fc_ListarDirectorAcademico(dt.Rows(0).Item("codigo_Fac"), "F") '-- F: Decano
            End If
            dtDecano = fc_ListarDirectorAcademico(dtDatos.Rows(0).Item("codigo_Fac"), "F")

            Try
                If dtDecano.Rows(0).Item("sexo_per") = "M" Then
                    txtDecano = "Decano"
                Else
                    txtDecano = "Decana"
                End If
                If dtDecano.Rows(0).Item("codigo_per") = "6251" Then '--- es para agregar (e) Decano alvarado choy
                    txtEncargado = " (e)"
                Else
                    txtEncargado = ""
                End If
            Catch ex As Exception
                Throw New Exception("No se encuentra configurado el Decado de Facultad", ex)
            End Try
           
            ''********************Principal*************************************************************************
            Dim pdfDoc As New iTextSharp.text.Document(iTextSharp.text.PageSize.A4)
            Dim pdfWrite As iTextSharp.text.pdf.PdfWriter = iTextSharp.text.pdf.PdfWriter.GetInstance(pdfDoc, memory)
            pdfDoc.Open()

            If serieCorrelativo = "" Then
                mt_AddWaterMark(pdfWrite, "BORRADOR")
                serieCorrelativo = "RESO-GRAD-USAT-XXXX-2020"
            End If


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

            _parrafoOne.Add(fc_textFrase("Visto el Expediente Nº " & dtDatos.Rows(0).Item("glosaCorrelativo_trl") & "" & " perteneciente a " & alumnosDNI & ", " & txtEgresado & " de la Escuela de " & dtDatos.Rows(0).Item("NombreOficial_cpf") & ", " & txtQuien & " el otorgamiento del Grado Académico de " & dtDatos.Rows(0).Item("descripcion_dgt") & "; y," & Environment.NewLine & Environment.NewLine & _
                                     "CONSIDERANDO: " & Environment.NewLine & Environment.NewLine & _
                                     "Que mediante la aprobación para egreso realizada por " & sexoDirector & " de la Escuela de " & dtDirector.Rows(0).Item("nombre_cpf") & ", " & _
                                     "" & dtDirector.Rows(0).Item("grado") & " " & dtDirector.Rows(0).Item("nombres_per") & " " & dtDirector.Rows(0).Item("apellidoPat_per") & " " & dtDirector.Rows(0).Item("apellidoMat_per") & " realizado en la fecha " & dtDatos.Rows(0).Item("fechaEgresado") & " pone en conocimiento que el estudiante se encuentra apto para recibir " & _
                                     "el Grado Académico de Bachiller, por haber cumplido satisfactoriamente los requisitos del Plan curricular de la Escuela de " & dtDatos.Rows(0).Item("NombreOficial_cpf") & "; " & Environment.NewLine & Environment.NewLine & _
                                     "Que habiendo verificado los requisitos exigidos por el Reglamento General de Grados y Títulos de la Universidad Católica Santo Toribio de Mogrovejo, el mismo que fue aprobado mediante Decreto Nº 061- 2017- ASOC. de fecha 27 de noviembre de 2017, se aprueba el otorgamiento del Grado Académico de " & dtDatos.Rows(0).Item("descripcion_dgt") & ", al estudiante solicitante, debiendo emitirse una resolución específica que será elevada al Consejo Universitario para que le confiera el Grado Académico de Bachiller, conforme lo estipula el artículo 59.9 de la Ley 30220;" & Environment.NewLine, 5, 10.0F, 0, iTextSharp.text.BaseColor.BLACK))
            cellContenido.AddCell(fc_CeldaTextoPhrase(_parrafoOne, 0, 1, 1, 3, 0, 5, ""))
            pdfTable.AddCell(cellContenido)

            Dim cellContenidoDos As iTextSharp.text.pdf.PdfPTable = New iTextSharp.text.pdf.PdfPTable(1)
            cellContenidoDos.WidthPercentage = 100.0F
            cellContenidoDos.SetWidths(New Single() {100.0F})
            cellContenidoDos.DefaultCell.Border = 0

            Dim _parrafoDos As iTextSharp.text.Phrase
            _parrafoDos = New iTextSharp.text.Phrase

            _parrafoDos.Add(fc_textFrase("En uso de las atribuciones conferidas por la Ley Universitaria Nº 30220, el Estatuto de la Asociación Civil USAT y el Reglamento de Grados y Títulos de la Universidad Católica Santo Toribio de Mogrovejo;" & Environment.NewLine & Environment.NewLine & _
                                     "SE RESUELVE:. " & Environment.NewLine & " " & Environment.NewLine, 5, 10.0F, 0, iTextSharp.text.BaseColor.BLACK))
            cellContenidoDos.AddCell(fc_CeldaTextoPhrase(_parrafoDos, 0, 1, 1, 3, 0, 5, ""))
            pdfTable.AddCell(cellContenidoDos)

            '----- Contenido con tabla ----------------------------------------------------------------------------------------------
            Dim cellContenidoT1 As iTextSharp.text.pdf.PdfPTable = New iTextSharp.text.pdf.PdfPTable(2)
            cellContenidoT1.WidthPercentage = 100.0F
            cellContenidoT1.SetWidths(New Single() {20.0F, 80.0F})
            cellContenidoT1.DefaultCell.Border = 0

            Dim cellContenidoT2 As iTextSharp.text.pdf.PdfPTable = New iTextSharp.text.pdf.PdfPTable(2)
            cellContenidoT2.WidthPercentage = 100.0F
            cellContenidoT2.SetWidths(New Single() {20.0F, 80.0F})
            cellContenidoT2.DefaultCell.Border = 0

            'Dim cellContenidoT3 As iTextSharp.text.pdf.PdfPTable = New iTextSharp.text.pdf.PdfPTable(2)
            'cellContenidoT3.WidthPercentage = 100.0F
            'cellContenidoT3.SetWidths(New Single() {20.0F, 80.0F})
            'cellContenidoT3.DefaultCell.Border = 0

            Dim _parrafoTwo As iTextSharp.text.Phrase
            _parrafoTwo = New iTextSharp.text.Phrase

            Dim _parrafoTree As iTextSharp.text.Phrase
            _parrafoTree = New iTextSharp.text.Phrase

            'Dim _parrafoFor As iTextSharp.text.Phrase
            '_parrafoFor = New iTextSharp.text.Phrase


            cellContenidoT1.AddCell(fc_CeldaTexto("Articulo 1°-", 9.0F, 0, 0, 1, 1, 1))
            _parrafoTwo.Add(fc_textFrase("APROBAR EL OTORGAMIENTO DEL GRADO ACADÉMICO DE BACHILLER EN " & dtDatos.Rows(0).Item("NombreOficial_cpf") & " " & txtAl & " " & txtEgresado & " " & alumnos & " por haber cumplido con los requisitos exigidos por la Universidad Católica Santo Toribio de Mogrovejo. ", 5, 10.0F, 0, iTextSharp.text.BaseColor.BLACK))
            cellContenidoT1.AddCell(fc_CeldaTextoPhrase(_parrafoTwo, 0, 1, 1, 3, 0, 5, ""))
            pdfTable.AddCell(cellContenidoT1)

            cellContenidoT2.AddCell(fc_CeldaTexto("Articulo 2°-", 9.0F, 0, 0, 1, 1, 1))
            _parrafoTree.Add(fc_textFrase("ELEVAR la presente resolución al Consejo Universitario, a través de la Oficina de Grados y Títulos, para que se le confiera el grado académico correspondiente. ", 5, 10.0F, 0, iTextSharp.text.BaseColor.BLACK))
            cellContenidoT2.AddCell(fc_CeldaTextoPhrase(_parrafoTree, 0, 1, 1, 3, 0, 5, ""))
            pdfTable.AddCell(cellContenidoT2)

            'cellContenidoT3.AddCell(fc_CeldaTexto("Articulo 3°-", 9.0F, 0, 0, 1, 1, 1))
            '_parrafoFor.Add(fc_textFrase("Fijar como fecha de sustentación el día " & dtDatos.Rows(0).Item("fechaprogramacion") & " a las " & dtDatos.Rows(0).Item("horaprogramacion") & " en el ambiente " & dtDatos.Rows(0).Item("ambiente") & " de la Universidad Católica Santo Toribio de Mogrovejo. ", 5, 10.0F, 0, iTextSharp.text.BaseColor.BLACK))
            'cellContenidoT3.AddCell(fc_CeldaTextoPhrase(_parrafoFor, 0, 1, 1, 3, 0, 5, ""))
            'pdfTable.AddCell(cellContenidoT3)


            '------- firmas
            Dim cellFirmas As iTextSharp.text.pdf.PdfPTable = New iTextSharp.text.pdf.PdfPTable(3)
            cellFirmas.WidthPercentage = 100.0F
            cellFirmas.SetWidths(New Single() {34.0F, 33.0F, 33.0F})
            cellFirmas.DefaultCell.Border = 0

            'linea en blanco
            cellFirmas.AddCell(fc_CeldaTexto("Regístrese, comuníquese y archívese. ", 9.0F, 0, 0, 3, 1, 2))
            cellFirmas.AddCell(fc_CeldaTexto("", 9.0F, 0, 0, 3, 1, 1))
            cellFirmas.AddCell(fc_CeldaTexto("", 9.0F, 0, 0, 3, 1, 1))
            cellFirmas.AddCell(fc_CeldaTexto("", 9.0F, 0, 0, 3, 1, 1))


            cellFirmas.AddCell(fc_CeldaTexto(dtDecano.Rows(0).Item("grado") & " " & dtDecano.Rows(0).Item("nombres_per") & " " & dtDecano.Rows(0).Item("apellidoPat_per") & " " & dtDecano.Rows(0).Item("apellidoMat_per") & Environment.NewLine & txtDecano & txtEncargado & Environment.NewLine & " Facultad de " & dtDecano.Rows(0).Item("nombre_Fac") & " ", 9.0F, 0, 0, 3, 1, 1))
            'cellFirmas.AddCell(fc_CeldaTexto("Decano", 8.0F, 0, 0, 3, 1, 1))
            'cellFirmas.AddCell(fc_CeldaTexto("Facultad de [Nombre de facultad]", 8.0F, 0, 0, 3, 1, 1))
            pdfTable.AddCell(cellFirmas)

            pdfDoc.Add(pdfTable)
            pdfDoc.Close()

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function EmiteResolucionOtorgaTitulo(ByVal memory As System.IO.MemoryStream, ByVal sourceIcon As String, ByVal serieCorrelativo As String, ByVal arreglo As Dictionary(Of String, String)) As Integer
        Try
            Dim alumnosDNI As String = ""
            Dim alumnos As String = ""
            Dim dtDirector As New Data.DataTable
            Dim dtDecano As New Data.DataTable
            'traigo los datos de los alumnos
            Dim ultimaFila As Integer
            dt = New Data.DataTable
            dt = fc_DatosResolFacOtorgaTitulo(arreglo.Item("codigo_datos"))
            '************* Articulos / prefijos / sufijos
            Dim txtBachiller As String = ""
            Dim txtQuien As String = ""
            Dim sexoDirector As String = ""
            Dim txtAl As String = ""
            Dim txtDecano As String = ""
            Dim txtApto As String = ""
            Dim txtSexo As String = ""
            Dim txtEgresado As String = ""
            Dim txtEncargado As String = ""
            Dim txtLaEl As String = ""
            '********************************************
            Dim dtDatos As New Data.DataTable
            dtDatos = fc_DatosResolFacOtorgaTitulo(arreglo.Item("codigo_datos"))



            If dt.Rows.Count > 0 Then

                ''--------------------------- valida

                With dt.Rows(0)
                    If .Item("fechaRegReso") Is DBNull.Value Or .Item("serieCorrelativo_dot") Is DBNull.Value Then
                        Throw New Exception("Faltan datos del documento Resolución de Sustentación")
                    ElseIf .Item("fechaActa") Is DBNull.Value Or .Item("acta") Is DBNull.Value Then
                        Throw New Exception("Faltan datos del Acta de Sustentación")
                    ElseIf .Item("escala_pst") Is DBNull.Value Or .Item("promedio_pst") Is DBNull.Value Then
                        Throw New Exception("Faltan datos de calificación del Acta Sustentación")
                    ElseIf .Item("presidente") Is DBNull.Value Or .Item("secretario") Is DBNull.Value Or .Item("vocal") Is DBNull.Value Then
                        Throw New Exception("Faltan datos del jurado de tesis")
                        'ElseIf .Item("acta") Is DBNull.Value Then
                        '    Throw New Exception("Falta el numero del documento del Acta de Sustentación")
                    End If
                End With

                ''--------------------------

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
                        txtEgresado = "egresadas"
                    Else
                        txtAl = "a los"
                        txtApto = "aptos"
                        txtEgresado = "egresados"
                    End If
                Else
                    txtBachiller = "bachiller"
                    txtQuien = "quien solicita"
                    If txtSexo = "F" Then
                        txtAl = "a la"
                        txtApto = "apta"
                        txtEgresado = "egresada"
                        txtLaEl = "la"
                    Else
                        txtAl = "al"
                        txtApto = "apto"
                        txtEgresado = "egresado"
                        txtLaEl = "el"
                    End If

                End If
                'trae el director academico -- Director de escuela
                'dtDirector = fc_ListarDirectorAcademico(dt.Rows(0).Item("codigo_cpf"), "E") '-- E: director de esccuela
                'If dtDirector.Rows(0).Item("sexo_per") = "M" Then
                '    sexoDirector = "el Director encargado"
                'Else
                '    sexoDirector = "la Directora encargada"
                'End If

                'dtDecano = fc_ListarDirectorAcademico(dt.Rows(0).Item("codigo_Fac"), "F") '-- F: Decano
            End If

            dtDecano = fc_ListarDirectorAcademico(dtDatos.Rows(0).Item("codigo_Fac"), "F")
            Try
                If dtDecano.Rows(0).Item("sexo_per") = "M" Then
                    txtDecano = "Decano"
                Else
                    txtDecano = "Decana"
                End If
                If dtDecano.Rows(0).Item("codigo_per") = "6251" Then '--- es para agregar (e) Decano alvarado choy
                    txtEncargado = " (e)"
                Else
                    txtEncargado = ""
                End If

            Catch ex As Exception
                Throw New Exception("No se encuentra configurado el Decano de Facultad", ex)
            End Try

            ''********************Principal*************************************************************************
            Dim pdfDoc As New iTextSharp.text.Document(iTextSharp.text.PageSize.A4)
            Dim pdfWrite As iTextSharp.text.pdf.PdfWriter = iTextSharp.text.pdf.PdfWriter.GetInstance(pdfDoc, memory)
            pdfDoc.Open()

            If serieCorrelativo = "" Then
                mt_AddWaterMark(pdfWrite, "BORRADOR")
                serieCorrelativo = "RESO-TITU-USAT-XXXX-2020"
            End If


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

            'cellContenido.AddCell(fc_CeldaTexto("CONSEJO DE FACULTAD" & Environment.NewLine, 10.0F, 1, 0, 1, 1, 1))
            'cellContenido.AddCell(fc_CeldaTexto(serieCorrelativo & Environment.NewLine, 10.0F, 1, 0, 1, 1, 1))
            'cellContenido.AddCell(fc_CeldaTexto("Chiclayo, " & dtDatos.Rows(0).Item("FechaEmisionResol") & Environment.NewLine & " " & Environment.NewLine, 10.0F, 1, 0, 1, 1, 1))

            'pdfTable.AddCell(cellContenido)

            Dim _parrafoOne As iTextSharp.text.Phrase
            _parrafoOne = New iTextSharp.text.Phrase

            _parrafoOne.Add(fc_textFrase("Visto el Expediente Nº " & dtDatos.Rows(0).Item("glosaCorrelativo_trl") & " " & " perteneciente a " & alumnosDNI & ", " & txtBachiller & " de la Escuela de " & dtDatos.Rows(0).Item("NombreOficial_cpf") & ", " & txtQuien & " el otorgamiento del Título Profesional de " & dtDatos.Rows(0).Item("descripcion_dgt") & "; y," & Environment.NewLine & Environment.NewLine & _
                                     "CONSIDERANDO: " & Environment.NewLine & Environment.NewLine & _
                                     "Que mediante Resolución N° " & dtDatos.Rows(0).Item("serieCorrelativo_dot") & " de fecha " & dtDatos.Rows(0).Item("fechaRegReso") & " se declara expedito " & txtAl & " Bachiller " & dtDatos.Rows(0).Item("alumno") & _
                                     " para la sustentación de la Tesis denominada " & dtDatos.Rows(0).Item("Titulo_Tes") & ", designando como miembros del Jurado a los siguientes docentes: Presidente: " & dtDatos.Rows(0).Item("presidente") & ", Secretario: " & dtDatos.Rows(0).Item("secretario") & " y Vocal: " & dtDatos.Rows(0).Item("vocal") & "." & Environment.NewLine & Environment.NewLine & _
                                     "Que mediante Acta de Sustentación Nº " & dtDatos.Rows(0).Item("acta") & " de fecha " & dtDatos.Rows(0).Item("fechaActa") & ", el jurado calificador acuerda declarar " & txtAl & " bachiller, " & txtApto & " para obtener el Título Profesional de " & dtDatos.Rows(0).Item("descripcion_dgt") & " otorgando a la sustentación el calificativo de " & dtDatos.Rows(0).Item("escala_pst") & "(" & dtDatos.Rows(0).Item("promedio_pst") & ")" & " ;" & Environment.NewLine & Environment.NewLine & _
                                     "Por lo que, " & txtLaEl & " Bachiller " & dtDatos.Rows(0).Item("alumno") & " se encuentra " & txtApto & " para recibir el Título Profesional de " & dtDatos.Rows(0).Item("descripcion_dgt") & ", cumpliendo así con los requisitos establecidos por el Reglamento de Grados y Títulos de la Universidad Católica Santo Toribio de Mogrovejo aprobado mediante Decreto Nº 061- 2017- ASOC. de fecha 27 de noviembre de 2017, debiendo emitirse una resolución específica que será elevada al Consejo Universitario para que le confiera el título profesional, conforme lo estipula el artículo 59.9 de la Ley 30220;" & Environment.NewLine & Environment.NewLine, 5, 10.0F, 0, iTextSharp.text.BaseColor.BLACK))

            cellContenido.AddCell(fc_CeldaTextoPhrase(_parrafoOne, 0, 1, 1, 3, 0, 5, ""))
            pdfTable.AddCell(cellContenido)

            Dim cellContenidoDos As iTextSharp.text.pdf.PdfPTable = New iTextSharp.text.pdf.PdfPTable(1)
            cellContenidoDos.WidthPercentage = 100.0F
            cellContenidoDos.SetWidths(New Single() {100.0F})
            cellContenidoDos.DefaultCell.Border = 0

            Dim _parrafoDos As iTextSharp.text.Phrase
            _parrafoDos = New iTextSharp.text.Phrase

            _parrafoDos.Add(fc_textFrase("En uso de las atribuciones conferidas por la Ley Universitaria Nº 30220, el Estatuto de la Asociación Civil USAT y el Reglamento de Grados y Títulos de la Universidad Católica Santo Toribio de Mogrovejo;" & Environment.NewLine & Environment.NewLine & _
                                         "SE RESUELVE:. " & Environment.NewLine & " " & Environment.NewLine, 5, 10.0F, 0, iTextSharp.text.BaseColor.BLACK))
            cellContenidoDos.AddCell(fc_CeldaTextoPhrase(_parrafoDos, 0, 1, 1, 3, 0, 5, ""))
            pdfTable.AddCell(cellContenidoDos)

            '----- Contenido con tabla ----------------------------------------------------------------------------------------------
            Dim cellContenidoT1 As iTextSharp.text.pdf.PdfPTable = New iTextSharp.text.pdf.PdfPTable(2)
            cellContenidoT1.WidthPercentage = 100.0F
            cellContenidoT1.SetWidths(New Single() {20.0F, 80.0F})
            cellContenidoT1.DefaultCell.Border = 0

            Dim cellContenidoT2 As iTextSharp.text.pdf.PdfPTable = New iTextSharp.text.pdf.PdfPTable(2)
            cellContenidoT2.WidthPercentage = 100.0F
            cellContenidoT2.SetWidths(New Single() {20.0F, 80.0F})
            cellContenidoT2.DefaultCell.Border = 0

            'Dim cellContenidoT3 As iTextSharp.text.pdf.PdfPTable = New iTextSharp.text.pdf.PdfPTable(2)
            'cellContenidoT3.WidthPercentage = 100.0F
            'cellContenidoT3.SetWidths(New Single() {20.0F, 80.0F})
            'cellContenidoT3.DefaultCell.Border = 0

            Dim _parrafoTwo As iTextSharp.text.Phrase
            _parrafoTwo = New iTextSharp.text.Phrase

            Dim _parrafoTree As iTextSharp.text.Phrase
            _parrafoTree = New iTextSharp.text.Phrase

            'Dim _parrafoFor As iTextSharp.text.Phrase
            '_parrafoFor = New iTextSharp.text.Phrase


            cellContenidoT1.AddCell(fc_CeldaTexto("Articulo 1°-", 9.0F, 0, 0, 1, 1, 1))
            _parrafoTwo.Add(fc_textFrase("APROBAR EL OTORGAMIENTO DEL TITULO PROFESIONAL DE " & dtDatos.Rows(0).Item("descripcion_dgt") & " " & txtAl & " BACHILLER " & dtDatos.Rows(0).Item("alumno") & " por haber cumplido con los requisitos exigidos por la Universidad Católica Santo Toribio de Mogrovejo.", 5, 10.0F, 0, iTextSharp.text.BaseColor.BLACK))
            cellContenidoT1.AddCell(fc_CeldaTextoPhrase(_parrafoTwo, 0, 1, 1, 3, 0, 5, ""))
            pdfTable.AddCell(cellContenidoT1)

            cellContenidoT2.AddCell(fc_CeldaTexto("Articulo 2°-", 9.0F, 0, 0, 1, 1, 1))
            _parrafoTree.Add(fc_textFrase("ELEVAR la presente resolución al Consejo Universitario, a través de la Oficina de Grados y Títulos, para que se le confiera el grado académico correspondiente. ", 5, 10.0F, 0, iTextSharp.text.BaseColor.BLACK))
            cellContenidoT2.AddCell(fc_CeldaTextoPhrase(_parrafoTree, 0, 1, 1, 3, 0, 5, ""))
            pdfTable.AddCell(cellContenidoT2)

            'cellContenidoT3.AddCell(fc_CeldaTexto("Articulo 3°-", 9.0F, 0, 0, 1, 1, 1))
            '_parrafoFor.Add(fc_textFrase("Fijar como fecha de sustentación el día " & dtDatos.Rows(0).Item("fechaprogramacion") & " a las " & dtDatos.Rows(0).Item("horaprogramacion") & " en el ambiente " & dtDatos.Rows(0).Item("ambiente") & " de la Universidad Católica Santo Toribio de Mogrovejo. ", 5, 10.0F, 0, iTextSharp.text.BaseColor.BLACK))
            'cellContenidoT3.AddCell(fc_CeldaTextoPhrase(_parrafoFor, 0, 1, 1, 3, 0, 5, ""))
            'pdfTable.AddCell(cellContenidoT3)


            '------- firmas
            Dim cellFirmas As iTextSharp.text.pdf.PdfPTable = New iTextSharp.text.pdf.PdfPTable(3)
            cellFirmas.WidthPercentage = 100.0F
            cellFirmas.SetWidths(New Single() {34.0F, 33.0F, 33.0F})
            cellFirmas.DefaultCell.Border = 0

            'linea en blanco
            cellFirmas.AddCell(fc_CeldaTexto("Regístrese, comuníquese y archívese. ", 9.0F, 0, 0, 3, 1, 2))
            cellFirmas.AddCell(fc_CeldaTexto("", 9.0F, 0, 0, 3, 1, 1))
            cellFirmas.AddCell(fc_CeldaTexto("", 9.0F, 0, 0, 3, 1, 1))
            cellFirmas.AddCell(fc_CeldaTexto("", 9.0F, 0, 0, 3, 1, 1))
            cellFirmas.AddCell(fc_CeldaTexto("", 9.0F, 0, 0, 3, 1, 1))
            cellFirmas.AddCell(fc_CeldaTexto("", 9.0F, 0, 0, 3, 1, 1))


            cellFirmas.AddCell(fc_CeldaTexto(dtDecano.Rows(0).Item("grado") & " " & dtDecano.Rows(0).Item("nombres_per") & " " & dtDecano.Rows(0).Item("apellidoPat_per") & " " & dtDecano.Rows(0).Item("apellidoMat_per") & Environment.NewLine & txtDecano & txtEncargado & Environment.NewLine & " Facultad de " & dtDecano.Rows(0).Item("nombre_Fac") & " ", 9.0F, 0, 0, 3, 1, 1))
            'cellFirmas.AddCell(fc_CeldaTexto("Decano", 8.0F, 0, 0, 3, 1, 1))
            'cellFirmas.AddCell(fc_CeldaTexto("Facultad de [Nombre de facultad]", 8.0F, 0, 0, 3, 1, 1))
            pdfTable.AddCell(cellFirmas)

            pdfDoc.Add(pdfTable)
            pdfDoc.Close()

        Catch ex As Exception
            Throw ex
            'Throw New Exception("Faltan datos para generar el documento", ex)
        End Try
    End Function

    Public Function EmiteFichaMatricula(ByVal memory As System.IO.MemoryStream, ByVal sourceIcon As String, ByVal serieCorrelativo As String, ByVal arreglo As Dictionary(Of String, String)) As Integer
        Dim md_solicitud As New d_SolicitaDocumentacion
        Dim dtDet As New Data.DataTable
        Dim dtSolicitud As New Data.DataTable
        Dim dtAlumno As New Data.DataTable
        Dim codigo_alu As String = ""
        Dim codigo_cac As String = ""
        Dim descripcion_cac As String = ""
        Dim codigo_univ_alu As String = ""
        'Dim hash As String

        'Dim destinoQR As String = server.MapPath("~/") & "GestionDocumentaria/img/"

        If arreglo.Item("codigo_sol") <> "0" Then
            dtSolicitud = md_solicitud.ListarSolicitaDocumentacion("", arreglo.Item("codigo_sol"), 0, "", 0)
        End If

        If dtSolicitud.Rows.Count > 0 Then
            With dtSolicitud.Rows(0)

                If .Item("codigo_cac") Is DBNull.Value Or .Item("codigoUniver_Alu") Is DBNull.Value Then
                    codigo_cac = ""
                    codigo_univ_alu = ""
                Else
                    codigo_cac = .Item("codigo_cac")
                    codigo_univ_alu = .Item("codigoUniver_Alu")
                End If
                codigo_alu = .Item("codigo_alu")

                descripcion_cac = .Item("referencia01")

            End With
        End If
        If codigo_cac <> "" And codigo_univ_alu <> "" Then
            If arreglo.Item("codigo_sol") <> "0" Then
                dtDet = fc_DatosFichaMatriculaNotas("FichaMatricula", codigo_alu, codigo_cac)
                dtAlumno = fc_ConsultarAlumnoFichaByCU(codigo_univ_alu)
            Else
                dtDet = fc_DatosFichaMatriculaNotas("FichaMatricula", arreglo.Item("codigo_Alu"), arreglo.Item("codigo_cac"))
                dtAlumno = fc_ConsultarAlumnoFichaByCU(arreglo.Item("codigoUniv_alu"))
            End If
        End If
       

        'If serieCorrelativo = "" Then
        '    serieCorrelativo = "FICH-FMAT-ACAD-000-0000"
        'End If

        ''''QR
        'hash = serieCorrelativo & "|" & arreglo.Item("codigoUniv_alu")
        'Dim bm As Drawing.Bitmap = fc_ObtenerQR(hash)
        'bm.Save(destinoQR & serieCorrelativo & ".png")
        

        Try

            Dim pdfDoc As New iTextSharp.text.Document(iTextSharp.text.PageSize.A4)
            Dim pdfWrite As iTextSharp.text.pdf.PdfWriter = iTextSharp.text.pdf.PdfWriter.GetInstance(pdfDoc, memory)
            pdfDoc.Open()

            'If arreglo.Item("codigo_sol") = "0" Then mt_AddWaterMark(pdfWrite, "BORRADOR")
            If serieCorrelativo = "" Then
                mt_AddWaterMark(pdfWrite, "BORRADOR")
            End If


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

            ''---- QR
            'Dim srcQR As String = destinoQR & serieCorrelativo & ".png"

            'Dim usatQR As iTextSharp.text.Image = iTextSharp.text.Image.GetInstance(srcQR)
            'usatQR.ScalePercent(80.0F)
            'usatQR.Alignment = iTextSharp.text.Element.ALIGN_RIGHT

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

            'celda del Sello
            tablePie.AddCell(fc_CeldaTexto(" ", 8.0F, 0, 0, 1, 1, 1))
            Dim cellSello As iTextSharp.text.pdf.PdfPCell = New iTextSharp.text.pdf.PdfPCell(usatSello)
            cellSello.HorizontalAlignment = 1
            cellSello.VerticalAlignment = 0
            cellSello.Border = 0
            'agrego sello la tabla pie
            tablePie.AddCell(cellSello)


            tablePie.AddCell(fc_CeldaTexto(" ", 8.0F, 0, 0, 2, 1, 1))
            tablePie.AddCell(fc_CeldaTexto(" ", 8.0F, 0, 0, 1, 1, 1))
            tablePie.AddCell(fc_CeldaTexto(Environment.NewLine & "Ing. Willy Augusto Oliva Tong" & Environment.NewLine & " " & Environment.NewLine & _
"Dirección Académica", 8.0F, 0, 1, 1, 1, 1))

            ' linea vacía
            tablePie.AddCell(fc_CeldaTexto(" ", 8.0F, 0, 0, 2, 1, 1))

            ' ''*** QR
            'tablePie.AddCell(fc_CeldaTexto(" ", 8.0F, 0, 0, 1, 1, 1))
            'Dim cellQR As iTextSharp.text.pdf.PdfPCell = New iTextSharp.text.pdf.PdfPCell(usatQR)
            'cellQR.HorizontalAlignment = 1
            'cellQR.VerticalAlignment = 0
            'cellQR.Border = 0

            'tablePie.AddCell(cellQR)

            pdfTable.AddCell(tablePie)

            '''''' borrar un archivo
            'File.Delete(destinoQR & serieCorrelativo & ".png")

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
        Dim codigo_alu As String = ""
        Dim codigo_cac As String = ""
        Dim descripcion_cac As String = ""
        Dim codigo_univ_alu As String = ""

        If arreglo.Item("codigo_sol") <> "0" Then
            dtSolicitud = md_solicitud.ListarSolicitaDocumentacion("", arreglo.Item("codigo_sol"), 0, "", 0)
        End If

        If dtSolicitud.Rows.Count > 0 Then
            With dtSolicitud.Rows(0)
                If .Item("codigo_cac") Is DBNull.Value Or .Item("codigoUniver_Alu") Is DBNull.Value Then
                    codigo_cac = ""
                    codigo_univ_alu = ""
                Else
                    codigo_cac = .Item("codigo_cac").ToString()
                    codigo_univ_alu = .Item("codigoUniver_Alu")
                End If

                codigo_alu = .Item("codigo_alu").ToString()
                descripcion_cac = .Item("referencia01")
            End With
        End If

        If codigo_cac <> "" Then
            If arreglo.Item("codigo_sol") <> "0" Then
                dtDet = fc_DatosFichaMatriculaNotas("FichaNotas", codigo_alu, codigo_cac)
                dtAlumno = fc_ConsultarAlumnoFichaByCU(codigo_univ_alu)
            Else
                dtDet = fc_DatosFichaMatriculaNotas("FichaNotas", arreglo.Item("codigo_Alu"), arreglo.Item("codigo_cac"))
                dtAlumno = fc_ConsultarAlumnoFichaByCU(arreglo.Item("codigoUniv_alu"))
            End If
        End If
      


        ''''''''''' Fin Cambiado

        Try

            Dim pdfDoc As New iTextSharp.text.Document(iTextSharp.text.PageSize.A4)
            Dim pdfWrite As iTextSharp.text.pdf.PdfWriter = iTextSharp.text.pdf.PdfWriter.GetInstance(pdfDoc, memory)
            pdfDoc.Open()

            'If arreglo.Item("codigo_sol") = "0" Then mt_AddWaterMark(pdfWrite, "BORRADOR")
            If serieCorrelativo = "" Then mt_AddWaterMark(pdfWrite, "BORRADOR")


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
            table.AddCell(fc_CeldaTexto("", 8.0F, 0, 0, 4, 1, 2))
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
            tablePie.AddCell(fc_CeldaTexto(Environment.NewLine & "Ing. Willy Augusto Oliva Tong" & Environment.NewLine & " " & Environment.NewLine & _
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
            Dim txtTitulo As String = ""
            Dim txtEscuela As String = ""

            '********************************************

            'traigo los datos de
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
                    If dtDatos.Rows(0).Item("codigo_test") = "5" And dtDatos.Rows(0).Item("codigo_stest") = "2" Then
                        txtBachiller = "Maestros"
                    Else
                        txtBachiller = "Bachilleres"
                    End If
                    If txtSexo = "F" Then
                        txtEl = "a las bachilleres"
                        txtDel = "de las"
                    Else
                        txtEl = "a los bachilleres"
                        txtDel = "de los"
                    End If
                    txtHa = "han"
                Else
                    If txtSexo = "F" Then
                        txtDel = "de la"
                        txtEl = "a la bachiller"
                    Else
                        txtDel = "del"
                        txtEl = "al bachiller"
                    End If
                    If dtDatos.Rows(0).Item("codigo_test") = "5" And dtDatos.Rows(0).Item("codigo_stest") = "2" Then
                        If txtSexo = "F" Then
                            txtBachiller = "Maestra"
                        Else
                            txtBachiller = "Maestro"
                        End If

                    Else
                        txtBachiller = "Bachiller"
                    End If

                    txtHa = "ha"
                End If
                'trae el director academi

                
            End If
            '---------------------- se agrego para posgrado y maestría
            If arreglo.Item("tipoEstudio") = "5" Then '---- Posgrado

                txtTitulo = "ACTA DE SUSTENTACIÓN DE TESIS DE MAESTRÍA"
                txtEscuela = "Escuela de Posgrado del programa de " & dtDatos.Rows(0).Item("nombreOficial_cpf")

                If dtDatos.Rows(0).Item("codigo_stest") = "2" Then
                    txtTitulo = "ACTA DE SUSTENTACIÓN DE TESIS DE DOCTORADO"
                End If

            Else
                txtTitulo = "ACTA DE SUSTENTACIÓN DE TESIS"
                txtEscuela = "Escuela Profesional de " & dtDatos.Rows(0).Item("nombreOficial_cpf")
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

            cellTituloCorrelativo.AddCell(fc_CeldaTexto(txtTitulo & Environment.NewLine & " " & Environment.NewLine & _
                               serieCorrelativo, 10.0F, 1, 0, 1, 1, 1))

            pdfTable.AddCell(cellTituloCorrelativo)

            '----- Contenido
            Dim cellContenido As iTextSharp.text.pdf.PdfPTable = New iTextSharp.text.pdf.PdfPTable(1)
            cellContenido.WidthPercentage = 100.0F
            cellContenido.SetWidths(New Single() {100.0F})
            cellContenido.DefaultCell.Border = 0

            Dim _parrafoOne As iTextSharp.text.Phrase
            _parrafoOne = New iTextSharp.text.Phrase

            _parrafoOne.Add(fc_textFrase("En la ciudad de Chiclayo, a las" & dtDatos.Rows(0).Item("horaprogramacion") & " del día " & dtDatos.Rows(0).Item("fechaprogramacion") & ", los miembros del jurado designados por la " & txtEscuela & "" & _
                                      ", Presidente: " & dtDatos.Rows(0).Item("presidente") & ", Secretario: " & dtDatos.Rows(0).Item("secretario") & ", Vocal: " & dt.Rows(0).Item("vocal") & ", se reunieron en el ambiente " & dtDatos.Rows(0).Item("ambiente") & ", " & _
                                      "para recibir la sustentación de la Tesis titulada: " & dtDatos.Rows(0).Item("titulo_tes") & ", " & txtDel & " " & txtBachiller & " " & _
                                      "" & alumnos & "." & Environment.NewLine & " " & Environment.NewLine & "" & _
                                      "Siendo las" & dtDatos.Rows(0).Item("horaAprobacion") & ", habiéndose concluido la exposición y absueltas las preguntas del jurado, se acordó otorgar " & txtEl & " la calificación de " & dtDatos.Rows(0).Item("nota") & ", recibiendo la categoría de " & dtDatos.Rows(0).Item("descripcion_ecs") & ". ", 5, 10.0F, 0, iTextSharp.text.BaseColor.BLACK))


            cellContenido.AddCell(fc_CeldaTextoPhrase(_parrafoOne, 0, 1, 1, 3, 0, 5, ""))
            '----- fecha
            'linea en blanco
            cellContenido.AddCell(fc_CeldaTexto("", 8.0F, 0, 0, 1, 1, 1))
            cellContenido.AddCell(fc_CeldaTexto("", 8.0F, 0, 0, 1, 1, 1))
            cellContenido.AddCell(fc_CeldaTexto("", 8.0F, 0, 0, 1, 1, 1))

            cellContenido.AddCell(fc_CeldaTexto("Chiclayo, " & dtDatos.Rows(0).Item("fechaprogramacion"), 10.0F, 0, 0, 1, 1, 2))
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
            If dtDatos.Rows(0).Item("codigo_test") = "5" And dtDatos.Rows(0).Item("codigo_stest") = "2" Then
                cellFirmas.AddCell(fc_CeldaTexto(dtDatos.Rows(0).Item("vocal") & Environment.NewLine & " Vocal del Jurado ", 8.0F, 0, 0, 1, 1, 1))
                cellFirmas.AddCell(fc_CeldaTexto("", 8.0F, 0, 0, 1, 1, 1))
                cellFirmas.AddCell(fc_CeldaTexto(dtDatos.Rows(0).Item("vocal2") & Environment.NewLine & " Vocal/Asesor del Jurado ", 8.0F, 0, 0, 1, 1, 1))
            Else
                cellFirmas.AddCell(fc_CeldaTexto(dtDatos.Rows(0).Item("vocal") & Environment.NewLine & " Vocal del Jurado ", 8.0F, 0, 0, 3, 1, 1))
            End If

            pdfTable.AddCell(cellFirmas)

            pdfDoc.Add(pdfTable)
            pdfDoc.Close()

        Catch ex As Exception

            cnx.AbortarTransaccion()
            Throw ex

        End Try
    End Function

    Public Function EmiteActaAprobacionTrabajoInvestigacion(ByVal memory As System.IO.MemoryStream, ByVal sourceIcon As String, ByVal serieCorrelativo As String, ByVal arreglo As Dictionary(Of String, String)) As Integer
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

            'traigo los datos de
            dt = New Data.DataTable
            dt = fc_DatosActaAprobTrabInvest(arreglo.Item("codigo_tba"))


            Dim dtDatos As New Data.DataTable
            dtDatos = fc_DatosActaAprobTrabInvest(arreglo.Item("codigo_tba"))

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
                        txtEl = "a las estudiantes"
                        txtDel = "de las estudiantes"
                    Else
                        txtEl = "a los estudiantes"
                        txtDel = "de los estudiantes"
                    End If
                    txtHa = "han"
                Else
                    If txtSexo = "F" Then
                        txtDel = "de la estudiante"
                        txtEl = "a la estudiante"
                    Else
                        txtDel = "del estudiante"
                        txtEl = "al estudiante"
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

            cellCabecera.AddCell(fc_CeldaTexto("" & Environment.NewLine & " " & "" & Environment.NewLine & "" & Environment.NewLine & Environment.NewLine, 10.0, 1, 0, 1, 1, 2, -1, 6, ""))
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

            cellTituloCorrelativo.AddCell(fc_CeldaTexto("ACTA DE APROBACIÓN DEL TRABAJO DE INVESTIGACIÓN" & Environment.NewLine & " " & Environment.NewLine & _
                               serieCorrelativo, 10.0F, 1, 0, 1, 1, 1))

            pdfTable.AddCell(cellTituloCorrelativo)

            '----- Contenido
            Dim cellContenido As iTextSharp.text.pdf.PdfPTable = New iTextSharp.text.pdf.PdfPTable(1)
            cellContenido.WidthPercentage = 100.0F
            cellContenido.SetWidths(New Single() {100.0F})
            cellContenido.DefaultCell.Border = 0

            Dim _parrafoOne As iTextSharp.text.Phrase
            _parrafoOne = New iTextSharp.text.Phrase

            _parrafoOne.Add(fc_textFrase("En la ciudad de Chiclayo, del día " & dtDatos.Rows(0).Item("fecha") & ", el director de la Escuela " & _
                                      "Profesional de " & dtDatos.Rows(0).Item("nombreOficial_cpf") & ", recibe el Trabajo de Investigación titulado: " & dtDatos.Rows(0).Item("titulo_tba") & ", " & txtDel & " " & _
                                      "" & alumnos & "" & Environment.NewLine & " " & Environment.NewLine & "" & _
                                      "Habiéndose revisado el cumplimiento de los requisitos de presentación del trabajo de investigación para optar el grado de Bachiller, se otorga " & txtEl & " la categoría de " & dtDatos.Rows(0).Item("condicion") & ". ", 5, 10.0F, 0, iTextSharp.text.BaseColor.BLACK))


            cellContenido.AddCell(fc_CeldaTextoPhrase(_parrafoOne, 0, 1, 1, 3, 0, 5, ""))
            '----- fecha
            'linea en blanco
            cellContenido.AddCell(fc_CeldaTexto("", 8.0F, 0, 0, 1, 1, 1))
            cellContenido.AddCell(fc_CeldaTexto("", 8.0F, 0, 0, 1, 1, 1))
            cellContenido.AddCell(fc_CeldaTexto("", 8.0F, 0, 0, 1, 1, 1))

            cellContenido.AddCell(fc_CeldaTexto("Chiclayo, " & Day(Now) & " de " & MonthName(Month(Now)) & " del " & Year(Now), 10.0F, 0, 0, 1, 1, 2))
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
            'cellFirmas.AddCell(fc_CeldaTexto(dtDatos.Rows(0).Item("presidente") & Environment.NewLine & "Presidente del Jurado", 8.0F, 0, 0, 1, 1, 1))
            'cellFirmas.AddCell(fc_CeldaTexto("", 8.0F, 0, 0, 1, 1, 1))
            'cellFirmas.AddCell(fc_CeldaTexto(dtDatos.Rows(0).Item("secretario") & Environment.NewLine & "Secretario del Jurado", 8.0F, 0, 0, 1, 1, 1))
            'cellFirmas.AddCell(fc_CeldaTexto("", 8.0F, 0, 0, 3, 1, 1))
            'cellFirmas.AddCell(fc_CeldaTexto("", 8.0F, 0, 0, 3, 1, 1))
            cellFirmas.AddCell(fc_CeldaTexto(dtDatos.Rows(0).Item("director") & Environment.NewLine & " Director de Escuela", 8.0F, 0, 0, 3, 1, 1))
            pdfTable.AddCell(cellFirmas)

            pdfDoc.Add(pdfTable)
            pdfDoc.Close()

        Catch ex As Exception

            cnx.AbortarTransaccion()
            Throw ex

        End Try
    End Function

    Public Function EmiteAutorPublicTesis(ByVal memory As System.IO.MemoryStream, ByVal sourceIcon As String, ByVal serieCorrelativo As String, ByVal arreglo As Dictionary(Of String, String)) As Integer

        Dim tipoAcceso As String = ""
        Dim Confidencial As String = ""
        Dim Publico As String = ""
        Dim Embargado As String = ""

        Dim mesesConfidencial As String = ""
        Dim mesesPublico As String = ""
        Dim mesesEmbargado As String = ""

        dt = New Data.DataTable
        dt = fc_DatosAutorPublicTesis(arreglo.Item("codigo_tes"))



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

        cellCabecera.AddCell(fc_CeldaTexto("SISTEMA DE LA GESTIÓN DE LA CALIDAD" & Environment.NewLine & " " & "CÓDIGO USAT-PM0702-D-03" & Environment.NewLine & "VERSIÓN:01" & Environment.NewLine & Environment.NewLine, 10.0, 0, 0, 1, 1, 2, -1, 6, ""))
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

        cellTituloCorrelativo.AddCell(fc_CeldaTexto("AUTORIZACIÓN PARA PUBLICACIÓN DE LA VERSIÓN ELECTRÓNICA DE OBRAS INTELECTUALES EN EL REPOSITORIO DE LA USAT" & Environment.NewLine & " " & Environment.NewLine & _
                           serieCorrelativo, 10.0F, 1, 0, 1, 1, 1))

        pdfTable.AddCell(cellTituloCorrelativo)

        '----- Contenido
        Dim cellContenido As iTextSharp.text.pdf.PdfPTable = New iTextSharp.text.pdf.PdfPTable(1)
        cellContenido.WidthPercentage = 100.0F
        cellContenido.SetWidths(New Single() {100.0F})
        cellContenido.DefaultCell.Border = 0

        Dim _parrafoOne As iTextSharp.text.Phrase
        _parrafoOne = New iTextSharp.text.Phrase

        _parrafoOne.Add(fc_textFrase("El objetivo del Repositorio de la Universidad Católica Santo Toribio de Mogrovejo (USAT) es preservar y difundir en modo de acceso" & _
                                     " abierto la producción intelectual de la actividad investigadora de los estudiantes de  dicha casa de estudio. " & Environment.NewLine & _
                                     "Para que el Repositorio de la USAT pueda almacenar y distribuir la obra, es necesario que Ud. " & _
                                     " lea y acepte las condiciones establecidas en esta autorización. ", 5, 10.0F, 0, iTextSharp.text.BaseColor.BLACK))

        cellContenido.AddCell(fc_CeldaTextoPhrase(_parrafoOne, 0, 1, 1, 3, 0, 5, ""))
        pdfTable.AddCell(cellContenido)

        Dim cellItems As iTextSharp.text.pdf.PdfPTable = New iTextSharp.text.pdf.PdfPTable(2)
        cellItems.WidthPercentage = 100.0F
        cellItems.SetWidths(New Single() {5.0F, 95.0F})
        cellItems.DefaultCell.Border = 0

        cellItems.AddCell(fc_CeldaTexto("1.", 8.0F, 0, 0, 1, 1, 0))
        cellItems.AddCell(fc_CeldaTexto("DATOS PERSONALES", 8.0F, 0, 0, 1, 1, 0))

        If dt.Rows.Count > 0 Then
            Dim filas As Integer = dt.Rows.Count

            For i As Integer = 0 To dt.Rows.Count - 1

                cellItems.AddCell(fc_CeldaTexto("", 8.0F, 0, 0, 1, 1, 0))
                cellItems.AddCell(fc_CeldaTexto("Nombres y apellidos: " & dt.Rows(i).Item("autor"), 8.0F, 0, 15, 1, 1, 0))

                cellItems.AddCell(fc_CeldaTexto("", 8.0F, 0, 0, 1, 1, 0))
                cellItems.AddCell(fc_CeldaTexto("D.N.I.:  " & dt.Rows(i).Item("nroDocIdent_Alu") & "             Correo electrónico:   " & dt.Rows(i).Item("eMail_Alu"), 8.0F, 0, 15, 1, 1, 0))

                cellItems.AddCell(fc_CeldaTexto("", 8.0F, 0, 0, 1, 1, 0))
                cellItems.AddCell(fc_CeldaTexto("Teléfonos:  " & dt.Rows(i).Item("telefonoCelular_Pso"), 8.0F, 0, 15, 1, 1, 0))

                If filas > 1 Then
                    cellItems.AddCell(fc_CeldaTexto("", 8.0F, 0, 0, 2, 1, 0))
                End If


            Next

            '-------- para la publicacion
            tipoAcceso = dt.Rows(0).Item("tipoAutorizacion")
            If tipoAcceso = "Confidencial" Then
                Confidencial = "X"
                mesesConfidencial = dt.Rows(0).Item("mesesrestriccion")
            ElseIf tipoAcceso = "Público" Then
                Publico = "X"
                mesesPublico = dt.Rows(0).Item("mesesrestriccion")
            ElseIf tipoAcceso = "Embargado" Then
                Embargado = "X"
                mesesEmbargado = dt.Rows(0).Item("mesesrestriccion") & " meses"
            End If
            '------------------------------

        End If

        pdfTable.AddCell(cellItems)

        Dim cellItemsTwo As iTextSharp.text.pdf.PdfPTable = New iTextSharp.text.pdf.PdfPTable(2)
        cellItemsTwo.WidthPercentage = 100.0F
        cellItemsTwo.SetWidths(New Single() {5.0F, 95.0F})
        cellItemsTwo.DefaultCell.Border = 0

        cellItemsTwo.AddCell(fc_CeldaTexto("2.", 8.0F, 0, 0, 1, 1, 0))
        cellItemsTwo.AddCell(fc_CeldaTexto("DATOS ACADÉMICOS", 8.0F, 0, 0, 1, 1, 0))


        If arreglo.Item("tipoEstudio") = "5" Then '-- posgrado

            cellItemsTwo.AddCell(fc_CeldaTexto("", 8.0F, 0, 0, 1, 1, 0))
            cellItemsTwo.AddCell(fc_CeldaTexto("Escuela Profesional: Escuela de Posgrado", 8.0F, 0, 15, 1, 1, 0))

            cellItemsTwo.AddCell(fc_CeldaTexto("", 8.0F, 0, 0, 1, 1, 0))
            cellItemsTwo.AddCell(fc_CeldaTexto(dt.Rows(0).Item("nombreOficial_cpf"), 8.0F, 0, 15, 1, 1, 0))
        Else
            cellItemsTwo.AddCell(fc_CeldaTexto("", 8.0F, 0, 0, 1, 1, 0))
            cellItemsTwo.AddCell(fc_CeldaTexto("Facultad: " & dt.Rows(0).Item("nombre_Fac"), 8.0F, 0, 15, 1, 1, 0))

            cellItemsTwo.AddCell(fc_CeldaTexto("", 8.0F, 0, 0, 1, 1, 0))
            cellItemsTwo.AddCell(fc_CeldaTexto("Carrera Profesional: " & dt.Rows(0).Item("nombreOficial_cpf"), 8.0F, 0, 15, 1, 1, 0))


            cellItemsTwo.AddCell(fc_CeldaTexto("", 8.0F, 0, 0, 1, 1, 0))
            cellItemsTwo.AddCell(fc_CeldaTexto("Grado de Bachiller: " & dt.Rows(0).Item("bachiller") & Environment.NewLine & Environment.NewLine & "Título Profesional: " & dt.Rows(0).Item("titulo"), 8.0F, 0, 15, 1, 1, 0))


        End If

        pdfTable.AddCell(cellItemsTwo)

        Dim cellItemsTree As iTextSharp.text.pdf.PdfPTable = New iTextSharp.text.pdf.PdfPTable(2)
        cellItemsTree.WidthPercentage = 100.0F
        cellItemsTree.SetWidths(New Single() {5.0F, 95.0F})
        cellItemsTree.DefaultCell.Border = 0


        cellItemsTree.AddCell(fc_CeldaTexto("3.", 8.0F, 0, 0, 1, 1, 0))
        cellItemsTree.AddCell(fc_CeldaTexto("DATOS DE LA OBRA INTELECTUAL", 8.0F, 0, 0, 1, 1, 0))

        cellItemsTree.AddCell(fc_CeldaTexto("", 8.0F, 0, 0, 1, 1, 0))
        cellItemsTree.AddCell(fc_CeldaTexto("Título:  " & dt.Rows(0).Item("Titulo_Tes"), 8.0F, 0, 15, 1, 1, 0))

        cellItemsTree.AddCell(fc_CeldaTexto("", 8.0F, 0, 0, 1, 1, 0))
        cellItemsTree.AddCell(fc_CeldaTexto("Asesor: " & dt.Rows(0).Item("asesor") & Environment.NewLine & Environment.NewLine & "ORCID del asesor: " & dt.Rows(0).Item("orcid") & Environment.NewLine & Environment.NewLine & "Email: " & dt.Rows(0).Item("emailAsesor") & Environment.NewLine & Environment.NewLine & "Teléfono: " & dt.Rows(0).Item("celularAsesor"), 8.0F, 0, 15, 1, 1, 0))

        cellItemsTree.AddCell(fc_CeldaTexto("", 8.0F, 0, 0, 1, 1, 0))
        cellItemsTree.AddCell(fc_CeldaTexto("Año de sustentación: " & dt.Rows(0).Item("anio"), 8.0F, 0, 15, 1, 1, 0))

        If arreglo.Item("tipoEstudio") = "5" Then '-- posgrado

            cellItemsTree.AddCell(fc_CeldaTexto("", 8.0F, 0, 0, 1, 1, 0))
            cellItemsTree.AddCell(fc_CeldaTexto(" X Tesis (Doctorado, maestría)" & Environment.NewLine & Environment.NewLine & "    Trabajo de investigación (Solo maestría)", 8.0F, 0, 15, 1, 1, 0))
            
        Else

            cellItemsTree.AddCell(fc_CeldaTexto("", 8.0F, 0, 0, 1, 1, 0))
            cellItemsTree.AddCell(fc_CeldaTexto(" X Tesis" & Environment.NewLine & Environment.NewLine & "   Trabajo de suficiencia profesional:" & Environment.NewLine & Environment.NewLine & "   Trabajo de investigación", 8.0F, 0, 15, 1, 1, 0))

        End If

        cellItemsTree.AddCell(fc_CeldaTexto("", 8.0F, 0, 0, 1, 1, 0))
        cellItemsTree.AddCell(fc_CeldaTexto("Campo del conocimiento OCDE: " & dt.Rows(0).Item("lineaOcde"), 8.0F, 0, 15, 1, 1, 0))

        cellItemsTree.AddCell(fc_CeldaTexto("", 8.0F, 0, 0, 1, 1, 0))
        cellItemsTree.AddCell(fc_CeldaTexto("Línea de investigación USAT: " & dt.Rows(0).Item("lineausat"), 8.0F, 0, 15, 1, 1, 0))

        pdfTable.AddCell(cellItemsTree)

        Dim cellItemsFor As iTextSharp.text.pdf.PdfPTable = New iTextSharp.text.pdf.PdfPTable(2)
        cellItemsFor.WidthPercentage = 100.0F
        cellItemsFor.SetWidths(New Single() {5.0F, 95.0F})
        cellItemsFor.DefaultCell.Border = 0

        cellItemsFor.AddCell(fc_CeldaTexto("4.", 8.0F, 0, 0, 1, 1, 0))
        cellItemsFor.AddCell(fc_CeldaTexto("AUTORIZACIÓN DE PUBLICACIÓN", 8.0F, 0, 0, 1, 1, 0))

        cellItemsFor.AddCell(fc_CeldaTexto("", 8.0F, 0, 0, 1, 1, 0))

        Dim _parrafoTwo As iTextSharp.text.Phrase
        _parrafoTwo = New iTextSharp.text.Phrase

        _parrafoTwo.Add(fc_textFrase("El autor otorga a la Universidad Católica Santo Toribio de Mogrovejo una licencia " & _
                                     "no exclusiva, firmada en este formato de autorización, para que la universidad " & _
                                     "pueda distribuir, almacenar y preservar la obra poniéndola en acceso libre en " & _
                                     "el repositorio. ", 5, 10.0F, 0, iTextSharp.text.BaseColor.BLACK))


        cellItemsFor.AddCell(fc_CeldaTextoPhrase(_parrafoTwo, 0, 1, 1, 3, 0, 5, ""))
        pdfTable.AddCell(cellItemsFor)

        Dim cellItemsFive As iTextSharp.text.pdf.PdfPTable = New iTextSharp.text.pdf.PdfPTable(6)
        cellItemsFive.WidthPercentage = 100.0F
        cellItemsFive.SetWidths(New Single() {5.0F, 10.0F, 13.0F, 44.0F, 12.0F, 16.0F})
        cellItemsFive.DefaultCell.Border = 0

        cellItemsFive.AddCell(fc_CeldaTexto("", 8.0F, 0, 0, 1, 1, 0))
        cellItemsFive.AddCell(fc_CeldaTexto("MARCAR", 8.0F, 0, 15, 1, 1, 0))
        cellItemsFive.AddCell(fc_CeldaTexto("TIPO DE ACCESO", 8.0F, 0, 15, 1, 1, 0))
        cellItemsFive.AddCell(fc_CeldaTexto("CARACTERÍSTICA", 8.0F, 0, 15, 1, 1, 1))
        cellItemsFive.AddCell(fc_CeldaTexto("PERÍODO DE RESTRIC-CIÓN", 8.0F, 0, 15, 1, 1, 0))
        cellItemsFive.AddCell(fc_CeldaTexto("JUSTIFICACIÓN (Indicar documentos adjuntos)", 8.0F, 0, 15, 1, 1, 0))
        '--------------------------------------------------------------------------------------------------------------
        cellItemsFive.AddCell(fc_CeldaTexto("", 8.0F, 0, 0, 1, 1, 0))
        cellItemsFive.AddCell(fc_CeldaTexto(Publico, 8.0F, 0, 15, 1, 1, 1))
        cellItemsFive.AddCell(fc_CeldaTexto("Público", 8.0F, 0, 15, 1, 1, 0))
        Dim _parrafoTree As iTextSharp.text.Phrase
        _parrafoTree = New iTextSharp.text.Phrase

        _parrafoTree.Add(fc_textFrase("Autorizo a la Universidad Católica Santo Toribio de Mogrovejo publicar, por plazo indefinido, la versión electrónica del texto completo de mi obra y sus anexos en el repositorio de la universidad. " & Environment.NewLine & _
                                     "Acepto que se conserve más de una copia para garantizar la seguridad y preservación del archivo.", 5, 8.0F, 0, iTextSharp.text.BaseColor.BLACK))

        cellItemsFive.AddCell(fc_CeldaTextoPhrase(_parrafoTree, 15, 1, 1, 3, 0, 5, ""))
        cellItemsFive.AddCell(fc_CeldaTexto(mesesPublico, 8.0F, 0, 15, 1, 1, 0))
        If mesesPublico = "" Then
            cellItemsFive.AddCell(fc_CeldaTexto("", 8.0F, 0, 15, 1, 1, 0))
        Else
            cellItemsFive.AddCell(fc_CeldaTexto("Ninguna", 8.0F, 0, 15, 1, 1, 0))
        End If

        '-----------------------------------------------------------------------------------------------------------

        cellItemsFive.AddCell(fc_CeldaTexto("", 8.0F, 0, 0, 1, 1, 0))
        cellItemsFive.AddCell(fc_CeldaTexto(Embargado, 8.0F, 0, 15, 1, 1, 1))
        cellItemsFive.AddCell(fc_CeldaTexto("Embargado", 8.0F, 0, 15, 1, 1, 0))

        Dim _parrafoFor As iTextSharp.text.Phrase
        _parrafoFor = New iTextSharp.text.Phrase

        _parrafoFor.Add(fc_textFrase("En concordancia con el numeral 6.7 de la directiva Nº 004-2016-CONCYTEC DEGC  autorizo que se publique el texto completo después del tiempo de restricción establecido." & Environment.NewLine & _
                                     "En este periodo de postergación de obra solo se mostrará la información básica de la tesis más no el texto completo.", 5, 8.0F, 0, iTextSharp.text.BaseColor.BLACK))

        cellItemsFive.AddCell(fc_CeldaTextoPhrase(_parrafoFor, 15, 1, 1, 3, 0, 5, ""))
        cellItemsFive.AddCell(fc_CeldaTexto(mesesEmbargado, 8.0F, 0, 15, 1, 1, 0))
        If mesesEmbargado = "" Then
            cellItemsFive.AddCell(fc_CeldaTexto("", 8.0F, 0, 15, 1, 1, 0))
        Else
            cellItemsFive.AddCell(fc_CeldaTexto("Sustento: Declaración jurada simple", 8.0F, 0, 15, 1, 1, 0))
        End If

        '-----------------------------------------------------------------------------------------------------------

        cellItemsFive.AddCell(fc_CeldaTexto("", 8.0F, 0, 0, 1, 1, 0))
        cellItemsFive.AddCell(fc_CeldaTexto(Confidencial, 8.0F, 0, 15, 1, 1, 1, 1))
        cellItemsFive.AddCell(fc_CeldaTexto("Confidencial", 8.0F, 0, 15, 1, 1, 0))

        Dim _parrafoFive As iTextSharp.text.Phrase
        _parrafoFive = New iTextSharp.text.Phrase

        _parrafoFive.Add(fc_textFrase("En concordancia con el numeral 5.2 solo se visualizará el registro de la tesis de la directiva 004-2016- CONCYTEC DEGC." & Environment.NewLine & _
                                     "Autorizo que se permita visualizar solo la carátula y el resumen.", 5, 8.0F, 0, iTextSharp.text.BaseColor.BLACK))

        cellItemsFive.AddCell(fc_CeldaTextoPhrase(_parrafoFive, 15, 1, 1, 3, 0, 5, ""))
        cellItemsFive.AddCell(fc_CeldaTexto(mesesConfidencial, 8.0F, 0, 15, 1, 1, 1, 3))
        If mesesConfidencial = "" Then
            cellItemsFive.AddCell(fc_CeldaTexto("", 8.0F, 0, 15, 1, 1, 0))
        Else
            cellItemsFive.AddCell(fc_CeldaTexto("Sustento:Carta notarial del organismo público", 8.0F, 0, 15, 1, 1, 1, 3))
        End If

        '-----------------------------------------------------------------------------------------------------------
        pdfTable.AddCell(cellItemsFive)

        Dim cellItemsSix As iTextSharp.text.pdf.PdfPTable = New iTextSharp.text.pdf.PdfPTable(2)
        cellItemsSix.WidthPercentage = 100.0F
        cellItemsSix.SetWidths(New Single() {5.0F, 95.0F})
        cellItemsSix.DefaultCell.Border = 0
        Dim _parrafoSix As iTextSharp.text.Phrase
        _parrafoSix = New iTextSharp.text.Phrase

        _parrafoSix.Add(fc_textFrase("Confirmo que los datos presentados en este formato son verídicos, además que, en el trabajo de investigación," & _
                                   " no se ha incurrido en ningún tipo de plagio ni cometido violación contra los derechos de autor de terceras personas.", 5, 10.0F, 0, iTextSharp.text.BaseColor.BLACK))

        cellItemsSix.AddCell(fc_CeldaTextoPhrase(_parrafoSix, 0, 2, 1, 3, 0, 5, ""))
        pdfTable.AddCell(cellItemsSix)

        Dim cellFecha As iTextSharp.text.pdf.PdfPTable = New iTextSharp.text.pdf.PdfPTable(1)
        cellFecha.WidthPercentage = 100.0F
        cellFecha.SetWidths(New Single() {100.0F})
        cellFecha.DefaultCell.Border = 0
        cellFecha.AddCell(fc_CeldaTexto("", 10.0F, 0, 0, 1, 1, 2))

        cellFecha.AddCell(fc_CeldaTexto("Chiclayo, " & Day(Now) & " de " & MonthName(Month(Now)) & " de " & Year(Now), 10.0F, 0, 0, 1, 1, 2))
        pdfTable.AddCell(cellFecha)

        pdfDoc.Add(pdfTable)
        pdfDoc.Close()

    End Function

    Public Function EmiteActaEvaluacionNotas(ByVal memory As System.IO.MemoryStream, ByVal sourceIcon As String, ByVal serieCorrelativo As String, ByVal arreglo As Dictionary(Of String, String)) As Integer
        Try

            Dim dt As New Data.DataTable
            dt = fc_DatosActaDeEvaluacion(arreglo.Item("codigo_cup"))

            Dim pdfDoc As New iTextSharp.text.Document(iTextSharp.text.PageSize.A4)
            Dim pdfWrite As iTextSharp.text.pdf.PdfWriter = iTextSharp.text.pdf.PdfWriter.GetInstance(pdfDoc, memory)
            pdfDoc.Open()

            Dim pdfTable As iTextSharp.text.pdf.PdfPTable = New iTextSharp.text.pdf.PdfPTable(1)
            pdfTable.WidthPercentage = 85.0F
            pdfTable.DefaultCell.Border = 0

            '------ logotipo
            Dim cellLogotipo As iTextSharp.text.pdf.PdfPTable = New iTextSharp.text.pdf.PdfPTable(1)
            cellLogotipo.WidthPercentage = 100.0F
            cellLogotipo.SetWidths(New Single() {100.0F})
            cellLogotipo.DefaultCell.Border = 0

            '---- icono usat
            Dim srcIcon As String = sourceIcon
            Dim usatIcon As iTextSharp.text.Image = iTextSharp.text.Image.GetInstance(srcIcon)
            usatIcon.ScalePercent(40.0F)
            usatIcon.Alignment = iTextSharp.text.Element.ALIGN_CENTER

            Dim cellIcon As iTextSharp.text.pdf.PdfPCell = New iTextSharp.text.pdf.PdfPCell(usatIcon)
            cellIcon.HorizontalAlignment = 1
            cellIcon.VerticalAlignment = 0
            cellIcon.Border = 0

            '----- Tabla

            Dim cellTable As iTextSharp.text.pdf.PdfPTable = New iTextSharp.text.pdf.PdfPTable(6)
            cellTable.WidthPercentage = 100.0F
            cellTable.SetWidths(New Single() {7.0F, 11.0F, 14.0F, 43.0F, 11.0F, 14.0F})
            cellTable.DefaultCell.Border = 0

            cellTable.AddCell(cellIcon)

            cellTable.AddCell(fc_CeldaTexto("ACTA DE EVALUACIÓN" & Environment.NewLine & dt.Rows(0).Item("descripcion_cac"), 11.0F, 1, 0, 5, 1, 1))

            ''linea vacia
            cellTable.AddCell(fc_CeldaTexto("", 11.0F, 1, 0, 6, 1, 1))
            ''fin linea

            cellTable.AddCell(fc_CeldaTexto("Docente", 9.0F, 1, 0, 2, 1, 0))
            cellTable.AddCell(fc_CeldaTexto(": " & dt.Rows(0).Item("docente"), 9.0F, 0, 0, 4, 1, 0))

            cellTable.AddCell(fc_CeldaTexto("Asignatura", 9.0F, 1, 0, 2, 1, 0))
            cellTable.AddCell(fc_CeldaTexto(": " & dt.Rows(0).Item("nombre_cur"), 9.0F, 0, 0, 4, 1, 0))

            cellTable.AddCell(fc_CeldaTexto("Código", 9.0F, 1, 0, 2, 1, 0))
            cellTable.AddCell(fc_CeldaTexto(": " & dt.Rows(0).Item("identificador_cur") & "    Grupo: " & dt.Rows(0).Item("grupoHor_cup") & "    Ciclo: " & fc_EnteroRomano(CInt(dt.Rows(0).Item("ciclo_cur"))), 9.0F, 1, 0, 4, 1, 0))
            'linea vacía----
            cellTable.AddCell(fc_CeldaTexto("", 8.0F, 1, 0, 6, 1, 1))
            '------------------
            cellTable.AddCell(fc_CeldaTexto("", 9.0F, 1, 0, 2, 1, 0))
            cellTable.AddCell(fc_CeldaTexto("Fecha de impresión: " & DateTime.Now.ToString("dd/MM/yyyy") & " " & DateTime.Now.ToString("hh:mm:ss tt"), 9.0F, 1, 0, 4, 1, 2))
           
            cellTable.AddCell(fc_CeldaTexto("N°", 8.0F, 1, 15, 1, 1, 1))
            cellTable.AddCell(fc_CeldaTexto("ESCUELA PROFESIONAL", 8.0F, 1, 15, 1, 1, 1))
            cellTable.AddCell(fc_CeldaTexto("CÓDIGO", 8.0F, 1, 15, 1, 1, 1))
            cellTable.AddCell(fc_CeldaTexto("ESTUDIANTE", 8.0F, 1, 15, 1, 1, 1))
            cellTable.AddCell(fc_CeldaTexto("NOTA FINAL", 8.0F, 1, 15, 1, 1, 1))
            cellTable.AddCell(fc_CeldaTexto("CONDICIÓN", 8.0F, 1, 15, 1, 1, 1))

            Dim fAprobado As Integer = 0
            Dim fDesaprobado As Integer = 0
            Dim fInhabilitado As Integer = 0

            If dt.Rows.Count > 0 Then

                For i As Integer = 0 To dt.Rows.Count - 1
                    'Dim condicion As String
                    cellTable.AddCell(fc_CeldaTexto(i + 1, 8.0F, 0, 15, 1, 1, 1))
                    cellTable.AddCell(fc_CeldaTexto(dt.Rows(i).Item("nombre_cpf"), 8.0F, 0, 15, 1, 1, 1))
                    cellTable.AddCell(fc_CeldaTexto(dt.Rows(i).Item("codigoUniver_alu"), 8.0F, 0, 15, 1, 1, 0))
                    cellTable.AddCell(fc_CeldaTexto(dt.Rows(i).Item("alumno"), 8.0F, 0, 15, 1, 1, 0))
                    cellTable.AddCell(fc_CeldaTexto(dt.Rows(i).Item("notafinal_dma"), 8.0F, 0, 15, 1, 1, 1))
                    If dt.Rows(i).Item("condicion_dma").ToString = "A" Then
                        cellTable.AddCell(fc_CeldaTexto("Aprobado", 8.0F, 0, 15, 1, 1, 0, -1, 6, "", "Azul"))
                        fAprobado = fAprobado + 1
                    ElseIf dt.Rows(i).Item("condicion_dma").ToString = "D" Then
                        cellTable.AddCell(fc_CeldaTexto("Desaprobado", 8.0F, 0, 15, 1, 1, 0, -1, 6, "", "Rojo"))
                        fDesaprobado = fDesaprobado + 1
                    Else
                        cellTable.AddCell(fc_CeldaTexto("Inhabilitado", 8.0F, 0, 15, 1, 1, 0, -1, 6, "", "Verde"))
                        fInhabilitado = fInhabilitado + 1
                    End If
                Next
            End If
            ''linea vacia
            cellTable.AddCell(fc_CeldaTexto("", 11.0F, 1, 0, 6, 1, 1))
            ''fin linea

            cellTable.AddCell(fc_CeldaTexto("RESUMEN:", 9.0F, 1, 0, 6, 1, 0))


            cellTable.AddCell(fc_CeldaTexto("Aprobados: " & CStr(fAprobado).ToString, 9.0F, 0, 0, 2, 1, 0, -1, 6, "", "Azul"))
            cellTable.AddCell(fc_CeldaTexto("Desaprobados: " & CStr(fDesaprobado).ToString, 9.0F, 0, 0, 2, 0, 1, -1, 6, "", "Rojo"))
            'cellTable.AddCell(fc_CeldaTexto("Inhabilitados: " & CStr(fInhabilitado).ToString, 9.0F, 0, 0, 2, 0, 1, -1, 6, "", "Verde"))
            cellTable.AddCell(fc_CeldaTexto("", 9.0F, 0, 0, 2, 0, 1))
            'lineas en blanco 
            cellTable.AddCell(fc_CeldaTexto("", 11.0F, 1, 0, 6, 1, 1))
            cellTable.AddCell(fc_CeldaTexto("", 11.0F, 1, 0, 6, 1, 1))
            cellTable.AddCell(fc_CeldaTexto("", 11.0F, 1, 0, 6, 1, 1))
            cellTable.AddCell(fc_CeldaTexto("", 11.0F, 1, 0, 6, 1, 1))
            pdfTable.AddCell(cellTable)


            '------- firmas
            Dim cellFirmas As iTextSharp.text.pdf.PdfPTable = New iTextSharp.text.pdf.PdfPTable(3)
            cellFirmas.WidthPercentage = 100.0F
            cellFirmas.SetWidths(New Single() {32.0F, 36.0F, 32.0F})
            cellFirmas.DefaultCell.Border = 0

            cellFirmas.AddCell(fc_CeldaTexto("", 8.0F, 0, 0, 1, 1, 1))
            cellFirmas.AddCell(fc_CeldaTexto("", 8.0F, 0, 2, 1, 1, 1))
            cellFirmas.AddCell(fc_CeldaTexto("", 8.0F, 0, 0, 1, 1, 1))
            cellFirmas.AddCell(fc_CeldaTexto(dt.Rows(0).Item("grado") & " " & dt.Rows(0).Item("docente") & Environment.NewLine & "Docente", 8.0F, 0, 0, 3, 1, 1))
            'cellFirmas.AddCell(fc_CeldaTexto("Docente", 8.0F, 0, 0, 3, 1, 1))
            pdfTable.AddCell(cellFirmas)



            pdfDoc.Add(pdfTable)
            pdfDoc.Close()

        Catch ex As Exception

            cnx.AbortarTransaccion()
            Throw ex

        End Try
    End Function

    Public Function EmiteCertificadoActividadesFormacionComplementarias(ByVal memory As System.IO.MemoryStream, ByVal sourceIcon As String, ByVal serieCorrelativo As String, ByVal arreglo As Dictionary(Of String, String)) As Integer
        Try
            ''''
            Dim txtAlumo As String = ""

            ''''

            Dim dt As New Data.DataTable
            dt = fc_DatosDocumentosByAbrevDoc(arreglo.Item("codigo_datos"), "CAFC")

            Dim pdfDoc As New iTextSharp.text.Document(iTextSharp.text.PageSize.A4)
            Dim pdfWrite As iTextSharp.text.pdf.PdfWriter = iTextSharp.text.pdf.PdfWriter.GetInstance(pdfDoc, memory)
            pdfDoc.Open()

            Dim pdfTable As iTextSharp.text.pdf.PdfPTable = New iTextSharp.text.pdf.PdfPTable(1)
            pdfTable.WidthPercentage = 85.0F
            pdfTable.DefaultCell.Border = 0

            '------ logotipo
            Dim cellLogotipo As iTextSharp.text.pdf.PdfPTable = New iTextSharp.text.pdf.PdfPTable(1)
            cellLogotipo.WidthPercentage = 100.0F
            cellLogotipo.SetWidths(New Single() {100.0F})
            cellLogotipo.DefaultCell.Border = 0

            '---- icono usat
            Dim srcIcon As String = sourceIcon
            Dim usatIcon As iTextSharp.text.Image = iTextSharp.text.Image.GetInstance(srcIcon)
            usatIcon.ScalePercent(60.0F)
            usatIcon.Alignment = iTextSharp.text.Element.ALIGN_CENTER

            Dim cellIcon As iTextSharp.text.pdf.PdfPCell = New iTextSharp.text.pdf.PdfPCell(usatIcon)
            cellIcon.HorizontalAlignment = 1
            cellIcon.VerticalAlignment = 0
            cellIcon.Border = 0

            cellLogotipo.AddCell(cellIcon)
            pdfTable.AddCell(cellLogotipo)

            '----- Tabla

            Dim cellTable As iTextSharp.text.pdf.PdfPTable = New iTextSharp.text.pdf.PdfPTable(4)
            cellTable.WidthPercentage = 100.0F
            cellTable.SetWidths(New Single() {25.0F, 25.0F, 25.0F, 25.0F})
            cellTable.DefaultCell.Border = 0

            'cellTable.AddCell(cellIcon)
            cellTable.AddCell(fc_CeldaTexto("", 11.0F, 1, 0, 4, 1, 1))

            cellTable.AddCell(fc_CeldaTexto("El Director Académico de la Universidad Católica Santo Toribio de Mogrovejo" & Environment.NewLine & Environment.NewLine, 11.0F, 2, 0, 4, 1, 1))

            cellTable.AddCell(fc_CeldaTexto("CERTIFICA", 13.0F, 1, 0, 4, 1, 1))

            ''linea vacia
            cellTable.AddCell(fc_CeldaTexto("", 11.0F, 1, 0, 4, 1, 1))
            ''fin linea
            If dt.Rows.Count > 0 Then
                If dt.Rows(0).Item("sexo_Alu") = "F" Then
                    txtAlumo = "la alumna"
                Else
                    txtAlumo = "el alumno"
                End If
            End If
            


            Dim _parrafoOne As iTextSharp.text.Phrase
            _parrafoOne = New iTextSharp.text.Phrase

            _parrafoOne.Add(fc_textFrase("Que " & txtAlumo & " " & dt.Rows(0).Item("alumno") & ", con código de matrícula Nº " & dt.Rows(0).Item("codigoUniver_Alu") & ", " & _
                                         "estudiante de la Escuela de " & dt.Rows(0).Item("nombreOficial_cpf") & "," & _
                                         " Facultad de " & dt.Rows(0).Item("nombre_Fac") & " de nuestra Universidad, ha acreditado las Actividades" & _
                                         " de Formación Complementaria, según la siguiente descripción: ", 5, 10.0F, 0, iTextSharp.text.BaseColor.BLACK))

            cellTable.AddCell(fc_CeldaTextoPhrase(_parrafoOne, 0, 4, 1, 3, 0, 5, ""))

            cellTable.AddCell(fc_CeldaTexto("", 8.0F, 1, 0, 4, 1, 1))
            cellTable.AddCell(fc_CeldaTexto("", 8.0F, 1, 0, 4, 1, 1))

            cellTable.AddCell(fc_CeldaTexto("SEMESTRE ACADÉMICO", 8.0F, 1, 15, 1, 1, 1))
            cellTable.AddCell(fc_CeldaTexto("ACTIVIDAD", 8.0F, 1, 15, 2, 1, 1))
            cellTable.AddCell(fc_CeldaTexto("CREDITOS", 8.0F, 1, 15, 1, 1, 1))
           

            'Dim fAprobado As Integer = 0
            'Dim fDesaprobado As Integer = 0
            'Dim fInhabilitado As Integer = 0

            If dt.Rows.Count > 0 Then

                For i As Integer = 0 To dt.Rows.Count - 1

                    cellTable.AddCell(fc_CeldaTexto(dt.Rows(i).Item("descripcion_Cac"), 8.0F, 0, 15, 1, 1, 1))
                    cellTable.AddCell(fc_CeldaTexto(dt.Rows(i).Item("nombre_Cur"), 8.0F, 0, 15, 2, 1, 0))
                    cellTable.AddCell(fc_CeldaTexto(dt.Rows(i).Item("creditoCur_Dma"), 8.0F, 0, 15, 1, 1, 1))
                   
                Next

            End If
            'linea en blanco

            cellTable.AddCell(fc_CeldaTexto("", 11.0F, 1, 0, 4, 1, 1))

            cellTable.AddCell(fc_CeldaTexto("", 11.0F, 1, 0, 2, 1, 1))
            cellTable.AddCell(fc_CeldaTexto("Chiclayo, " & dt.Rows(0).Item("FechaEmision") & Environment.NewLine & " " & Environment.NewLine, 10.0F, 0, 0, 2, 1, 2))


            pdfTable.AddCell(cellTable)


            ''------- firmas
            Dim cellFirmas As iTextSharp.text.pdf.PdfPTable = New iTextSharp.text.pdf.PdfPTable(2)
            cellFirmas.WidthPercentage = 100.0F
            cellFirmas.SetWidths(New Single() {50.0F, 50.0F})
            cellFirmas.DefaultCell.Border = 0

            cellFirmas.AddCell(fc_CeldaTexto("", 8.0F, 0, 0, 1, 1, 1))
            cellFirmas.AddCell(fc_CeldaTexto("", 8.0F, 0, 0, 1, 1, 1))
            cellFirmas.AddCell(fc_CeldaTexto("", 8.0F, 0, 0, 1, 1, 1))
            cellFirmas.AddCell(fc_CeldaTexto("", 8.0F, 0, 0, 1, 1, 1))
            cellFirmas.AddCell(fc_CeldaTexto("", 8.0F, 0, 0, 1, 1, 1))
            cellFirmas.AddCell(fc_CeldaTexto("Ing. Willy Augusto Oliva Tong" & Environment.NewLine & "Director Académico", 10.0F, 0, 0, 1, 1, 1))
            'cellFirmas.AddCell(fc_CeldaTexto("", 8.0F, 0, 0, 1, 1, 1))
            'cellFirmas.AddCell(fc_CeldaTexto("Director Académico", 10.0F, 0, 0, 1, 1, 1))

            'cellFirmas.AddCell(fc_CeldaTexto("Docente", 8.0F, 0, 0, 3, 1, 1))
            pdfTable.AddCell(cellFirmas)



            pdfDoc.Add(pdfTable)
            pdfDoc.Close()

        Catch ex As Exception

            cnx.AbortarTransaccion()
            Throw ex

        End Try
    End Function

    Public Function EmiteResolConsUnivOtorgaGrado(ByVal memory As System.IO.MemoryStream, ByVal sourceIcon As String, ByVal serieCorrelativo As String, ByVal arreglo As Dictionary(Of String, String)) As Integer
        Try
            ''----------------------------------------- Datatables ------------------------------------------------------
            Dim dt As New Data.DataTable
            dt = fc_DatosResolConsUnivOtorgaGrado(arreglo.Item("codigo_sol"))

            Dim dtDecano As New Data.DataTable
            dtDecano = fc_ListarDirectorAcademico(dt.Rows(0).Item("codigo_Fac"), "F")

            Dim dtRector As New Data.DataTable
            dtRector = fc_ListarDirectorAcademico("17", "CU")


            Dim dtSecretario As New Data.DataTable
            dtSecretario = fc_ListarDirectorAcademico("47", "CU")


            ''----------------------------------------- Datatables ------------------------------------------------------
           
            '' ---------------------Abreviaturas y pronombres generos articulos -----------------------------------------------
            Dim txtDecano As String
            'Dim txtArtLa As String
            Dim txtResolucion As String
            Dim txtBachilleres As String
            Dim txtAlosBachilleres As String

            If dt.Rows.Count > 0 Then
                txtResolucion = "las resoluciones"
                txtBachilleres = "de los egresados aptos"
                txtAlosBachilleres = "a los egresados"
            Else
                txtResolucion = "la resolución"
                txtBachilleres = "del egresado apto"
                txtAlosBachilleres = "al egresado"
            End If



            If dtDecano.Rows(0).Item("sexo_per") = "M" Then
                txtDecano = "el Decano"
            Else
                txtDecano = "la Decana"
            End If

            '----------------------------------------------------------------------------------------------

            ''------ Documento principal  Todo ------------------------------------------------------------------------
            Dim pdfDoc As New iTextSharp.text.Document(iTextSharp.text.PageSize.A4)
            Dim pdfWrite As iTextSharp.text.pdf.PdfWriter = iTextSharp.text.pdf.PdfWriter.GetInstance(pdfDoc, memory)
            pdfDoc.Open()

            If serieCorrelativo = "" Then mt_AddWaterMark(pdfWrite, "BORRADOR")

            Dim pdfTable As iTextSharp.text.pdf.PdfPTable = New iTextSharp.text.pdf.PdfPTable(1)
            pdfTable.WidthPercentage = 85.0F
            pdfTable.DefaultCell.Border = 0

            ''------ Documento principal  Todo ------------------------------------------------------------------------

            '------ logotipo
            Dim cellLogotipo As iTextSharp.text.pdf.PdfPTable = New iTextSharp.text.pdf.PdfPTable(1)
            cellLogotipo.WidthPercentage = 100.0F
            cellLogotipo.SetWidths(New Single() {100.0F})
            cellLogotipo.DefaultCell.Border = 0

            '---- icono usat
            Dim srcIcon As String = sourceIcon
            Dim usatIcon As iTextSharp.text.Image = iTextSharp.text.Image.GetInstance(srcIcon)
            usatIcon.ScalePercent(50.0F)
            usatIcon.Alignment = iTextSharp.text.Element.ALIGN_CENTER

            Dim cellIcon As iTextSharp.text.pdf.PdfPCell = New iTextSharp.text.pdf.PdfPCell(usatIcon)
            cellIcon.HorizontalAlignment = 1
            cellIcon.VerticalAlignment = 0
            cellIcon.Border = 0

            '----- Tabla

            Dim cellTable As iTextSharp.text.pdf.PdfPTable = New iTextSharp.text.pdf.PdfPTable(4)
            cellTable.WidthPercentage = 100.0F
            cellTable.SetWidths(New Single() {10.0F, 20.0F, 35.0F, 35.0F})
            cellTable.DefaultCell.Border = 0

            cellTable.AddCell(cellIcon)

            ''linea vacia
            cellTable.AddCell(fc_CeldaTexto("", 11.0F, 1, 0, 4, 1, 1))
            cellTable.AddCell(fc_CeldaTexto("", 11.0F, 1, 0, 4, 1, 1))
            ''fin linea

            cellTable.AddCell(fc_CeldaTexto("CONSEJO UNIVERSITARIO" & Environment.NewLine, 11.0F, 1, 0, 4, 1, 1))
            cellTable.AddCell(fc_CeldaTexto(serieCorrelativo & Environment.NewLine, 11.0F, 1, 0, 4, 1, 1))
            cellTable.AddCell(fc_CeldaTexto("Chiclayo, " & dt.Rows(0).Item("FechaEmisionResol") & Environment.NewLine & " " & Environment.NewLine, 11.0F, 1, 0, 4, 1, 1))
            ''linea vacia
            cellTable.AddCell(fc_CeldaTexto("", 11.0F, 1, 0, 4, 1, 1))
            ''fin linea
            'cellTable.AddCell(fc_CeldaTexto("Visto y Considerando:", 11.0F, 1, 0, 4, 1, 1))

            Dim _parrafoOne As iTextSharp.text.Phrase
            _parrafoOne = New iTextSharp.text.Phrase

            _parrafoOne.Add(fc_textFrase("Visto y Considerando:" & Environment.NewLine & Environment.NewLine & "Que " & txtDecano & " de la Facultad de " & dt.Rows(0).Item("nombre_Fac") & " ha elevado al Consejo Universitario," & _
                                            "a través de la Oficina de Grados y Títulos, " & txtResolucion & " " & txtBachilleres & " para  " & _
                                            "obtener el Grado Académico de " & dt.Rows(0).Item("descripcion_dgt") & ", que se detalla en la parte resolutiva, " & _
                                            "a fin de que se le confiera el grado académico respectivo; " & Environment.NewLine & Environment.NewLine & _
                                            "Que el Consejo Universitario, ha evaluado " & txtResolucion & " y ha dado su conformidad, quedando registrada en el " & _
                                            "Acta Nº " & dt.Rows(0).Item("descripcion_scu") & " del " & dt.Rows(0).Item("fecha_scu") & ", de la Sesión " & dt.Rows(0).Item("tipo_sesion") & " de esta fecha;" & Environment.NewLine & Environment.NewLine & _
                                            "En uso de las atribuciones conferidas mediante Ley Universitaria Nº 30220 y el Estatuto de la Asociación Civil USAT;" & Environment.NewLine & Environment.NewLine & _
                                            "SE RESUELVE: " & Environment.NewLine & Environment.NewLine, 5, 10.0F, 0, iTextSharp.text.BaseColor.BLACK))

            cellTable.AddCell(fc_CeldaTextoPhrase(_parrafoOne, 0, 4, 1, 3, 0, 5, ""))

            cellTable.AddCell(fc_CeldaTexto("Artículo Único.-", 10.0F, 0, 0, 2, 1, 0))
            cellTable.AddCell(fc_CeldaTexto("Conferir el Título Profesional de " & dt.Rows(0).Item("descripcion_dgt") & " " & txtAlosBachilleres & ":", 10.0F, 0, 0, 2, 1, 3))

            'cabecera del detalle
            cellTable.AddCell(fc_CeldaTexto("N°", 10.0F, 1, 15, 1, 1, 1))
            cellTable.AddCell(fc_CeldaTexto("DNI", 10.0F, 1, 15, 1, 1, 1))
            cellTable.AddCell(fc_CeldaTexto("EGRESADO", 10.0F, 1, 15, 2, 1, 1))

            'detalle
            Dim fila As Integer
            If dt.Rows.Count > 0 Then
                For i As Integer = 0 To dt.Rows.Count - 1
                    fila = i + 1
                    With dt.Rows(i)
                        cellTable.AddCell(fc_CeldaTexto(fila, 10.0F, 0, 15, 1, 1, 1))
                        cellTable.AddCell(fc_CeldaTexto(.Item("nroDocIdent_Alu"), 10.0F, 0, 15, 1, 1, 0))
                        cellTable.AddCell(fc_CeldaTexto(.Item("egresado"), 10.0F, 0, 15, 2, 1, 0))
                    End With
                    
                Next
            End If

            ''linea vacia
            cellTable.AddCell(fc_CeldaTexto("", 11.0F, 1, 0, 4, 1, 1))

            cellTable.AddCell(fc_CeldaTexto("Regístrese, comuníquese y archívese.", 10.0F, 0, 0, 4, 1, 2))

            pdfTable.AddCell(cellTable)


            Dim cellFirmas As iTextSharp.text.pdf.PdfPTable = New iTextSharp.text.pdf.PdfPTable(4)
            cellFirmas.WidthPercentage = 100.0F
            cellFirmas.SetWidths(New Single() {25.0F, 25.0F, 25.0F, 25.0F})
            cellFirmas.DefaultCell.Border = 0


            ''linea vacia
            cellFirmas.AddCell(fc_CeldaTexto("", 11.0F, 1, 0, 4, 1, 1))
            cellFirmas.AddCell(fc_CeldaTexto("", 11.0F, 1, 0, 4, 1, 1))
            cellFirmas.AddCell(fc_CeldaTexto("", 11.0F, 1, 0, 4, 1, 1))
            cellFirmas.AddCell(fc_CeldaTexto("", 11.0F, 1, 0, 4, 1, 1))


            'firmas
            cellFirmas.AddCell(fc_CeldaTexto(dtSecretario.Rows(0).Item("grado") & " " & dtSecretario.Rows(0).Item("personal"), 10.0F, 1, 0, 2, 1, 1))
            cellFirmas.AddCell(fc_CeldaTexto(dtRector.Rows(0).Item("grado") & " " & dtRector.Rows(0).Item("personal"), 10.0F, 1, 0, 2, 1, 1))



            'firmas
            cellFirmas.AddCell(fc_CeldaTexto("SECRETARIO GENERAL", 10.0F, 1, 0, 2, 1, 1))
            cellFirmas.AddCell(fc_CeldaTexto("RECTOR", 10.0F, 1, 0, 2, 1, 1))

            pdfTable.AddCell(cellFirmas)


            pdfDoc.Add(pdfTable)
            pdfDoc.Close()


        Catch ex As Exception
            cnx.AbortarTransaccion()
            Throw ex

        End Try
    End Function

    Public Function EmiteResolConsUnivOtorgaTitulo(ByVal memory As System.IO.MemoryStream, ByVal sourceIcon As String, ByVal serieCorrelativo As String, ByVal arreglo As Dictionary(Of String, String)) As Integer
        Try
            ''----------------------------------------- Datatables ------------------------------------------------------
            Dim dt As New Data.DataTable
            dt = fc_DatosResolConsUnivOtorgaGrado(arreglo.Item("codigo_sol"))

            Dim dtDecano As New Data.DataTable
            dtDecano = fc_ListarDirectorAcademico(dt.Rows(0).Item("codigo_Fac"), "F")

            Dim dtRector As New Data.DataTable
            dtRector = fc_ListarDirectorAcademico("17", "CU")


            Dim dtSecretario As New Data.DataTable
            dtSecretario = fc_ListarDirectorAcademico("47", "CU")

            ''--------------------------------------------- Datatables ------------------------------------------------------

            '' ---------------------Abreviaturas y pronombres generos articulos -----------------------------------------------
            Dim txtDecano As String
            'Dim txtArtLa As String
            Dim txtResolucion As String
            Dim txtEgresados As String
            Dim txtAlosEgresados As String

            If dt.Rows.Count > 0 Then
                txtResolucion = "las resoluciones"
                txtEgresados = "de los bachilleres aptos"
                txtAlosEgresados = "a los bachilleres"
            Else
                txtResolucion = "la resolución"
                txtEgresados = "del bachiller apto"
                txtAlosEgresados = "al egresado"
            End If



            If dtDecano.Rows(0).Item("sexo_per") = "M" Then
                txtDecano = "el Decano"
            Else
                txtDecano = "la Decana"
            End If

            '----------------------------------------------------------------------------------------------

            ''------ Documento principal  Todo ------------------------------------------------------------------------
            Dim pdfDoc As New iTextSharp.text.Document(iTextSharp.text.PageSize.A4)
            Dim pdfWrite As iTextSharp.text.pdf.PdfWriter = iTextSharp.text.pdf.PdfWriter.GetInstance(pdfDoc, memory)
            pdfDoc.Open()

            If serieCorrelativo = "" Then mt_AddWaterMark(pdfWrite, "BORRADOR")

            Dim pdfTable As iTextSharp.text.pdf.PdfPTable = New iTextSharp.text.pdf.PdfPTable(1)
            pdfTable.WidthPercentage = 85.0F
            pdfTable.DefaultCell.Border = 0

            ''------ Documento principal  Todo ------------------------------------------------------------------------

            '------ logotipo
            Dim cellLogotipo As iTextSharp.text.pdf.PdfPTable = New iTextSharp.text.pdf.PdfPTable(1)
            cellLogotipo.WidthPercentage = 100.0F
            cellLogotipo.SetWidths(New Single() {100.0F})
            cellLogotipo.DefaultCell.Border = 0

            '---- icono usat
            Dim srcIcon As String = sourceIcon
            Dim usatIcon As iTextSharp.text.Image = iTextSharp.text.Image.GetInstance(srcIcon)
            usatIcon.ScalePercent(50.0F)
            usatIcon.Alignment = iTextSharp.text.Element.ALIGN_CENTER

            Dim cellIcon As iTextSharp.text.pdf.PdfPCell = New iTextSharp.text.pdf.PdfPCell(usatIcon)
            cellIcon.HorizontalAlignment = 1
            cellIcon.VerticalAlignment = 0
            cellIcon.Border = 0

            '----- Tabla

            Dim cellTable As iTextSharp.text.pdf.PdfPTable = New iTextSharp.text.pdf.PdfPTable(4)
            cellTable.WidthPercentage = 100.0F
            cellTable.SetWidths(New Single() {10.0F, 20.0F, 35.0F, 35.0F})
            cellTable.DefaultCell.Border = 0

            cellTable.AddCell(cellIcon)

            ''linea vacia
            cellTable.AddCell(fc_CeldaTexto("", 11.0F, 1, 0, 4, 1, 1))
            cellTable.AddCell(fc_CeldaTexto("", 11.0F, 1, 0, 4, 1, 1))
            ''fin linea

            cellTable.AddCell(fc_CeldaTexto("CONSEJO UNIVERSITARIO", 11.0F, 1, 0, 4, 1, 1))
            cellTable.AddCell(fc_CeldaTexto(serieCorrelativo & Environment.NewLine, 11.0F, 1, 0, 4, 1, 1))
            cellTable.AddCell(fc_CeldaTexto("Chiclayo, " & dt.Rows(0).Item("FechaEmisionResol") & Environment.NewLine & " " & Environment.NewLine, 11.0F, 1, 0, 4, 1, 1))

            ''linea vacia
            cellTable.AddCell(fc_CeldaTexto("", 11.0F, 1, 0, 4, 1, 1))
            ''fin linea
            'cellTable.AddCell(fc_CeldaTexto("Visto y Considerando:", 11.0F, 1, 0, 4, 1, 1))

            Dim _parrafoOne As iTextSharp.text.Phrase
            _parrafoOne = New iTextSharp.text.Phrase
            ''''''''''''''''''''''''''''''''''''''''''''''''''''' TITULO
            _parrafoOne.Add(fc_textFrase("Visto y Considerando:" & Environment.NewLine & Environment.NewLine & "Que " & txtDecano & " de la Facultad de " & dt.Rows(0).Item("nombre_Fac") & " ha elevado al Consejo Universitario," & _
                                            "a través de la Oficina de Grados y Títulos, " & txtResolucion & " " & txtEgresados & " para  " & _
                                            "obtener el Título Profesional de " & dt.Rows(0).Item("descripcion_dgt") & ", que se detalla en la parte resolutiva, " & _
                                            "a fin de que se le confiera el grado académico respectivo; " & Environment.NewLine & Environment.NewLine & _
                                            "Que el Consejo Universitario, ha evaluado " & txtResolucion & " y ha dado su conformidad, quedando registrada en el " & _
                                            "Acta Nº " & dt.Rows(0).Item("descripcion_scu") & " del " & dt.Rows(0).Item("fecha_scu") & ", de la Sesión " & dt.Rows(0).Item("tipo_sesion") & " de esta fecha;" & Environment.NewLine & Environment.NewLine & _
                                            "En uso de las atribuciones conferidas mediante Ley Universitaria Nº 30220 y el Estatuto de la Asociación Civil USAT;" & Environment.NewLine & Environment.NewLine & _
                                            "SE RESUELVE: " & Environment.NewLine & Environment.NewLine, 5, 10.0F, 0, iTextSharp.text.BaseColor.BLACK))

            cellTable.AddCell(fc_CeldaTextoPhrase(_parrafoOne, 0, 4, 1, 3, 0, 5, ""))

            cellTable.AddCell(fc_CeldaTexto("Artículo Único.-", 10.0F, 0, 0, 2, 1, 0))
            cellTable.AddCell(fc_CeldaTexto("Conferir el Título Profesional de " & dt.Rows(0).Item("descripcion_dgt") & " " & txtAlosEgresados & ":", 10.0F, 0, 0, 2, 1, 3))

            'cabecera del detalle
            cellTable.AddCell(fc_CeldaTexto("N°", 10.0F, 1, 15, 1, 1, 1))
            cellTable.AddCell(fc_CeldaTexto("DNI", 10.0F, 1, 15, 1, 1, 1))
            cellTable.AddCell(fc_CeldaTexto("EGRESADO", 10.0F, 1, 15, 2, 1, 1))

            'detalle
            Dim fila As Integer
            If dt.Rows.Count > 0 Then
                For i As Integer = 0 To dt.Rows.Count - 1
                    fila = i + 1
                    With dt.Rows(i)
                        cellTable.AddCell(fc_CeldaTexto(fila, 10.0F, 0, 15, 1, 1, 1))
                        cellTable.AddCell(fc_CeldaTexto(.Item("nroDocIdent_Alu"), 10.0F, 0, 15, 1, 1, 0))
                        cellTable.AddCell(fc_CeldaTexto(.Item("egresado"), 10.0F, 0, 15, 2, 1, 0))
                    End With

                Next
            End If

            ''linea vacia
            cellTable.AddCell(fc_CeldaTexto("", 11.0F, 1, 0, 4, 1, 1))

            cellTable.AddCell(fc_CeldaTexto("Regístrese, comuníquese y archívese.", 10.0F, 0, 0, 4, 1, 2))


            pdfTable.AddCell(cellTable)

            '' firmas
            Dim cellFirmas As iTextSharp.text.pdf.PdfPTable = New iTextSharp.text.pdf.PdfPTable(4)
            cellFirmas.WidthPercentage = 100.0F
            cellFirmas.SetWidths(New Single() {25.0F, 25.0F, 25.0F, 25.0F})
            cellFirmas.DefaultCell.Border = 0


            ''linea vacia
            cellFirmas.AddCell(fc_CeldaTexto("", 11.0F, 1, 0, 4, 1, 1))
            cellFirmas.AddCell(fc_CeldaTexto("", 11.0F, 1, 0, 4, 1, 1))
            cellFirmas.AddCell(fc_CeldaTexto("", 11.0F, 1, 0, 4, 1, 1))
            cellFirmas.AddCell(fc_CeldaTexto("", 11.0F, 1, 0, 4, 1, 1))


            'firmas
            cellFirmas.AddCell(fc_CeldaTexto(dtSecretario.Rows(0).Item("grado") & " " & dtSecretario.Rows(0).Item("personal"), 10.0F, 1, 0, 2, 1, 1))
            cellFirmas.AddCell(fc_CeldaTexto(dtRector.Rows(0).Item("grado") & " " & dtRector.Rows(0).Item("personal"), 10.0F, 1, 0, 2, 1, 1))


            'firmas
            cellFirmas.AddCell(fc_CeldaTexto("SECRETARIO GENERAL", 10.0F, 1, 0, 2, 1, 1))
            cellFirmas.AddCell(fc_CeldaTexto("RECTOR", 10.0F, 1, 0, 2, 1, 1))

            pdfTable.AddCell(cellFirmas)



            pdfDoc.Add(pdfTable)
            pdfDoc.Close()


        Catch ex As Exception
            cnx.AbortarTransaccion()
            Throw ex

        End Try
    End Function

    Public Function EmiteCartaCompromisoAsesoramientoTesis(ByVal memory As System.IO.MemoryStream, ByVal sourceIcon As String, ByVal serieCorrelativo As String, ByVal arreglo As Dictionary(Of String, String)) As Integer
        Try

            Dim alumnos As String = ""
            Dim ultimaFila As Integer
            ''Dim dtDirector As New Data.DataTable
            'Dim scP As String = "ASCAC ---ASCSC --"

            ''************* Articulos / prefijos / sufijos
            Dim txtLa As String = ""  '--- las, los, la, el 
            'Dim txtBachiller As String = ""
            'Dim txtEl As String = ""
            'Dim txtHa As String = ""
            Dim txtSexo As String = ""
            Dim txtAlumno As String = "" '''' alumono, alumna, alumnos, alumnas
            'Dim txtTitulo As String = ""
            'Dim txtEscuela As String = ""
            ''********************************************

            ''traigo los datos de la carta
            Dim dtDatos As New Data.DataTable
            dtDatos = New Data.DataTable
            dtDatos = fc_DatosCartaCompromisoTesis(arreglo.Item("codigo_tes"), arreglo.Item("codigo_cac"))


            If dtDatos.Rows.Count > 0 Then
                ultimaFila = dtDatos.Rows.Count - 1
                For i As Integer = 0 To dtDatos.Rows.Count - 1
                    '        '-------sexo
                    If i = 0 Then
                        txtSexo = dt.Rows(i).Item("sexo_alu").ToString
                    Else
                        If txtSexo <> dt.Rows(i).Item("sexo_alu").ToString Then
                            txtSexo = "U"
                        End If
                    End If
                    '        '------ coma
                    If i = ultimaFila Then
                        alumnos = alumnos + dt.Rows(i).Item("estudiante")
                    Else
                        alumnos = alumnos + dt.Rows(i).Item("estudiante") & ", "
                    End If
                Next
                '    '''''''''
                If ultimaFila > 0 Then

                    If txtSexo = "F" Then
                        txtLa = "las estudiantes"
                        txtAlumno = "alumnas"
                    Else
                        txtLa = "los estudiantes"
                        txtAlumno = "alumnos"
                    End If

                Else
                    If txtSexo = "F" Then
                        txtLa = "la estudiante"
                        txtAlumno = "alumna"
                    Else
                        txtLa = "el estudiante"
                        txtAlumno = "alumno"
                    End If
                    
                End If
                

            End If
                '---------------------- se agrego para posgrado y maestría
                'If arreglo.Item("tipoEstudio") = "5" Then '---- Posgrado

                '    txtTitulo = "ACTA DE SUSTENTACIÓN DE TESIS DE MAESTRÍA"
                '    txtEscuela = "Escuela de Posgrado del programa de " & dtDatos.Rows(0).Item("nombreOficial_cpf")

                '    If dtDatos.Rows(0).Item("codigo_stest") = "2" Then
                '        txtTitulo = "ACTA DE SUSTENTACIÓN DE TESIS DE DOCTORADO"
                '    End If

                'Else
                '    txtTitulo = "ACTA DE SUSTENTACIÓN DE TESIS"
                '    txtEscuela = "Escuela Profesional de " & dtDatos.Rows(0).Item("nombreOficial_cpf")
                'End If



                Dim pdfDoc As New iTextSharp.text.Document(iTextSharp.text.PageSize.A4)
                Dim pdfWrite As iTextSharp.text.pdf.PdfWriter = iTextSharp.text.pdf.PdfWriter.GetInstance(pdfDoc, memory)
                pdfDoc.Open()

                Dim pdfTable As iTextSharp.text.pdf.PdfPTable = New iTextSharp.text.pdf.PdfPTable(1)
                pdfTable.WidthPercentage = 85.0F
                pdfTable.DefaultCell.Border = 0

                ''---- Cabecera
                'Dim cellCabecera As iTextSharp.text.pdf.PdfPTable = New iTextSharp.text.pdf.PdfPTable(1)
                'cellCabecera.WidthPercentage = 100.0F
                'cellCabecera.SetWidths(New Single() {100.0F})
                'cellCabecera.DefaultCell.Border = 0

                'cellCabecera.AddCell(fc_CeldaTexto("SISTEMA DE LA GESTIÓN DE LA CALIDAD" & Environment.NewLine & " " & "CÓDIGO USAT-PM0702-D-02" & Environment.NewLine & "VERSIÓN:01" & Environment.NewLine & Environment.NewLine, 10.0, 1, 0, 1, 1, 2, -1, 6, ""))
                'pdfTable.AddCell(cellCabecera)

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

                ''------ titulo de documento y correlativo
                Dim cellTituloCorrelativo As iTextSharp.text.pdf.PdfPTable = New iTextSharp.text.pdf.PdfPTable(1)
                cellTituloCorrelativo.WidthPercentage = 100.0F
                cellTituloCorrelativo.SetWidths(New Single() {100.0F})
                cellTituloCorrelativo.DefaultCell.Border = 0


                cellTituloCorrelativo.AddCell(fc_CeldaTexto("" & Environment.NewLine, 10.0F, 1, 0, 1, 1, 1))
                cellTituloCorrelativo.AddCell(fc_CeldaTexto("" & Environment.NewLine, 10.0F, 1, 0, 1, 1, 1))


            cellTituloCorrelativo.AddCell(fc_CeldaTexto("CARTA DE COMPROMISO PARA ASESORAMIENTO DE TESIS" & Environment.NewLine & Environment.NewLine & serieCorrelativo, 10.0F, 1, 0, 1, 1, 1))

            cellTituloCorrelativo.AddCell(fc_CeldaTexto("", 10.0F, 1, 0, 1, 1, 1))
            cellTituloCorrelativo.AddCell(fc_CeldaTexto("", 10.0F, 1, 0, 1, 1, 1))

                pdfTable.AddCell(cellTituloCorrelativo)


                ''----- Contenido
                Dim cellContenido As iTextSharp.text.pdf.PdfPTable = New iTextSharp.text.pdf.PdfPTable(1)
                cellContenido.WidthPercentage = 100.0F
                cellContenido.SetWidths(New Single() {100.0F})
                cellContenido.DefaultCell.Border = 0

                Dim _parrafoOne As iTextSharp.text.Phrase
                _parrafoOne = New iTextSharp.text.Phrase

            _parrafoOne.Add(fc_textFrase("El que suscribe, " & dtDatos.Rows(0).Item("grado") & " " & dtDatos.Rows(0).Item("asesor") & " " & _
                                      "docente adscrito al " & dtDatos.Rows(0).Item("nombre_dac") & ", " & _
                                      "con conocimiento de lo establecido en el Reglamento de elaboración de trabajos de investigación para optar el grado académico de Bachiller y Título profesional, me comprometo y dejo constancia por " & _
                                      "la presente que asesoraré el proyecto de tesis denominado " & dtDatos.Rows(0).Item("titulo_tes") & ", presentado por " & txtLa & " " & alumnos & ", " & txtAlumno & " de la carrera de " & dtDatos.Rows(0).Item("nombreoficial_cpf") & " durante el semestre " & dtDatos.Rows(0).Item("descripcion_cac") & "." & Environment.NewLine & " " & Environment.NewLine & "" & _
                                      "", 5, 10.0F, 0, iTextSharp.text.BaseColor.BLACK))
                cellContenido.AddCell(fc_CeldaTextoPhrase(_parrafoOne, 0, 1, 1, 3, 0, 5, ""))
                '----- fecha
                'linea en blanco
                cellContenido.AddCell(fc_CeldaTexto("", 8.0F, 0, 0, 1, 1, 1))
                cellContenido.AddCell(fc_CeldaTexto("", 8.0F, 0, 0, 1, 1, 1))
                cellContenido.AddCell(fc_CeldaTexto("", 8.0F, 0, 0, 1, 1, 1))

            cellContenido.AddCell(fc_CeldaTexto("Chiclayo, " & Day(Now) & " de " & MonthName(Month(Now)) & " de " & Year(Now), 10.0F, 0, 0, 1, 1, 2))
                pdfTable.AddCell(cellContenido)

                ''------- firmas

                Dim cellFirmas As iTextSharp.text.pdf.PdfPTable = New iTextSharp.text.pdf.PdfPTable(3)
                cellFirmas.WidthPercentage = 100.0F
            cellFirmas.SetWidths(New Single() {20.0F, 60.0F, 20.0F})
                cellFirmas.DefaultCell.Border = 0

                ''linea en blanco
                cellFirmas.AddCell(fc_CeldaTexto("", 8.0F, 0, 0, 3, 1, 1))
            cellFirmas.AddCell(fc_CeldaTexto("", 8.0F, 0, 0, 3, 1, 1))
            cellFirmas.AddCell(fc_CeldaTexto("", 8.0F, 0, 0, 3, 1, 1))
            cellFirmas.AddCell(fc_CeldaTexto("", 8.0F, 0, 0, 3, 1, 1))
                cellFirmas.AddCell(fc_CeldaTexto("", 8.0F, 0, 0, 3, 1, 1))
            'cellFirmas.AddCell(fc_CeldaTexto("", Environment.NewLine & "Firma Nombre Asesor", 8.0F, 0, 3, 1, 1, 1))

            cellFirmas.AddCell(fc_CeldaTexto("", 8.0F, 0, 0, 1, 1, 1))
            cellFirmas.AddCell(fc_CeldaTexto("", 8.0F, 0, 0, 1, 1, 1))
            cellFirmas.AddCell(fc_CeldaTexto("", 8.0F, 0, 0, 1, 1, 1))


            cellFirmas.AddCell(fc_CeldaTexto("", 8.0F, 0, 0, 1, 1, 1))
            cellFirmas.AddCell(fc_CeldaTexto(dtDatos.Rows(0).Item("grado") & " " & dtDatos.Rows(0).Item("asesor") & Environment.NewLine, 8.0F, 0, 0, 1, 1, 1))
            cellFirmas.AddCell(fc_CeldaTexto("", 8.0F, 0, 0, 1, 1, 1))

            cellFirmas.AddCell(fc_CeldaTexto("", 8.0F, 0, 0, 1, 1, 1))
            'cellFirmas.AddCell(fc_CeldaTexto(Day(Now) & "/" & Month(Now) & "/" & Year(Now) & " " & Hour(Now) & ":" & Minute(Now), 8.0F, 0, 0, 1, 1, 1))
            cellFirmas.AddCell(fc_CeldaTexto(Day(Now) & "/" & Month(Now) & "/" & Year(Now) & " " & Format(Now, "hh:mm tt"), 8.0F, 0, 0, 1, 1, 1))
            cellFirmas.AddCell(fc_CeldaTexto("", 8.0F, 0, 0, 1, 1, 1))

            

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
                                Optional ByVal _backgroundcolor As String = "", Optional ByVal _fontColor As String = "") As iTextSharp.text.pdf.PdfPCell
        Dim celdaITC As iTextSharp.text.pdf.PdfPCell
        Dim fontITC As iTextSharp.text.Font

        'fontITC = New iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, _size, _style)
        Dim segoe As iTextSharp.text.pdf.BaseFont = iTextSharp.text.pdf.BaseFont.CreateFont(_fuente, iTextSharp.text.pdf.BaseFont.IDENTITY_H, iTextSharp.text.pdf.BaseFont.EMBEDDED)
        If _fontColor = "Rojo" Then
            fontITC = New iTextSharp.text.Font(segoe, _size, _style, iTextSharp.text.BaseColor.RED)
        ElseIf _fontColor = "Azul" Then
            fontITC = New iTextSharp.text.Font(segoe, _size, _style, iTextSharp.text.BaseColor.BLUE)
        ElseIf _fontColor = "Verde" Then
            fontITC = New iTextSharp.text.Font(segoe, _size, _style, iTextSharp.text.BaseColor.GREEN)
        Else
            fontITC = New iTextSharp.text.Font(segoe, _size, _style)
        End If

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

    Public Function fc_DatosResolFacOtorgaGrado(ByVal codigo_datos As Integer) As Data.DataTable
        Try
            cnx = New ClsConectarDatos
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.IniciarTransaccion()

            'Ejecutar Procedimiento
            dt = cnx.TraerDataTable("DOC_DatosResolFacOtorgaGrado", codigo_datos)

            cnx.TerminarTransaccion()

            Return dt
        Catch ex As Exception
            cnx.AbortarTransaccion()
            Throw ex
        End Try
    End Function

    Public Function fc_DatosResolFacOtorgaTitulo(ByVal codigo_datos As Integer) As Data.DataTable
        Try
            cnx = New ClsConectarDatos
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.IniciarTransaccion()

            'Ejecutar Procedimiento
            dt = cnx.TraerDataTable("DOC_DatosResolFacOtorgaTitulo", codigo_datos)

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

    Public Function fc_DatosActaAprobTrabInvest(ByVal codigo_tba As Integer) As Data.DataTable
        Try
            cnx = New ClsConectarDatos
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.IniciarTransaccion()

            'Ejecutar Procedimiento
            dt = cnx.TraerDataTable("DOC_DatosActaAprobTrabInvest", codigo_tba)

            cnx.TerminarTransaccion()

            Return dt
        Catch ex As Exception
            cnx.AbortarTransaccion()
            Throw ex
        End Try
    End Function

    Private Function fc_DatosAutorPublicTesis(ByVal codigo_tes As Integer) As Data.DataTable
        Try
            cnx = New ClsConectarDatos
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.IniciarTransaccion()

            'Ejecutar Procedimiento
            dt = cnx.TraerDataTable("DOC_DatosAutorPublicTesis", codigo_tes)

            cnx.TerminarTransaccion()

            Return dt
        Catch ex As Exception
            cnx.AbortarTransaccion()
            Throw ex
        End Try
    End Function

    Public Function fc_DatosFichaMatriculaNotas(ByVal tipoPrint As String, ByVal codigo_alu As Integer, ByVal codigo_cac As Integer) As Data.DataTable
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

    Public Function fc_ConsultarAlumnoFichaByCU(ByVal codigoUniver_Alu As String) As Data.DataTable
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

    Private Function fc_DatosActaDeEvaluacion(ByVal codigo_cup As Integer) As Data.DataTable
        Try
            cnx = New ClsConectarDatos
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.IniciarTransaccion()

            'Ejecutar Procedimiento
            dt = cnx.TraerDataTable("DOC_DatosActaEvaluacion", codigo_cup)

            cnx.TerminarTransaccion()

            Return dt
        Catch ex As Exception
            cnx.AbortarTransaccion()
            Throw ex
        End Try
    End Function

    Private Function fc_DatosResolConsUnivOtorgaGrado(ByVal codigo_sol As Integer) As Data.DataTable
        Try
            cnx = New ClsConectarDatos
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.IniciarTransaccion()

            'Ejecutar Procedimiento
            dt = cnx.TraerDataTable("DOC_DatosResolConsUnivOtorgaGrado", codigo_sol)

            cnx.TerminarTransaccion()

            Return dt
        Catch ex As Exception
            cnx.AbortarTransaccion()
            Throw ex
        End Try

    End Function


    Private Function fc_EnteroRomano(ByVal Numero As Integer) As String
        Dim x, Mil, Centena, Decena, Unidad As Integer
        'Math.Truncate Elimina Los Desimales Sin Redondear Por Ejemplo Si
        'Fuese El Resultado 2.69 Es Normal K Siendo Un Entero Lo Redondee A 3
        'Pero Con Truncate Nos Dara Un 2!!
        'TechPeru!!
        Mil = Math.Truncate(Numero / 1000)
        x = Numero Mod 1000
        Centena = Math.Truncate(x / 100)
        x = Numero Mod 100
        Decena = Math.Truncate(x / 10)
        Unidad = Numero Mod 10
        Dim Resultado As String = ""
        Select Case Mil
            Case 1
                Resultado = Resultado + "M"
            Case 2
                Resultado = Resultado + "MM"
            Case 3
                Resultado = Resultado + "MMM"
        End Select
        Select Case Centena
            Case 1
                Resultado = Resultado + "C"
            Case 2
                Resultado = Resultado + "CC"
            Case 3
                Resultado = Resultado + "CCC"
            Case 4
                Resultado = Resultado + "CD"
            Case 5
                Resultado = Resultado + "D"
            Case 6
                Resultado = Resultado + "DC"
            Case 7
                Resultado = Resultado + "DCC"
            Case 8
                Resultado = Resultado + "DCCC"
            Case 9
                Resultado = Resultado + "CM"
        End Select
        Select Case Decena
            Case 1
                Resultado = Resultado + "X"
            Case 2
                Resultado = Resultado + "XX"
            Case 3
                Resultado = Resultado + "XXX"
            Case 4
                Resultado = Resultado + "XL"
            Case 5

                Resultado = Resultado + "L"
            Case 6
                Resultado = Resultado + "LX"
            Case 7
                Resultado = Resultado + "LXX"
            Case 8
                Resultado = Resultado + "LXXX"
            Case 9
                Resultado = Resultado + "XC"
        End Select
        Select Case Unidad
            Case 1
                Resultado = Resultado + "I"
            Case 2
                Resultado = Resultado + "II"
            Case 3
                Resultado = Resultado + "III"
            Case 4
                Resultado = Resultado + "IV"
            Case 5
                Resultado = Resultado + "V"
            Case 6
                Resultado = Resultado + "VI"
            Case 7
                Resultado = Resultado + "VII"
            Case 8
                Resultado = Resultado + "VIII"
            Case 9
                Resultado = Resultado + "IX"
        End Select
        Return Resultado
    End Function


    Private Function fc_DatosDocumentosByAbrevDoc(ByVal codigoDatos As Integer, ByVal abreviarura As String) As Data.DataTable
        Try
            cnx = New ClsConectarDatos
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.IniciarTransaccion()

            'Ejecutar Procedimiento
            dt = cnx.TraerDataTable("DOC_DatosDocumentosByAbrevDoc", codigoDatos, abreviarura)

            cnx.TerminarTransaccion()

            Return dt
        Catch ex As Exception
            cnx.AbortarTransaccion()
            Throw ex
        End Try
    End Function

    Public Function fc_DatosCartaCompromisoTesis(ByVal codigo_tes As Integer, ByVal codigo_cac As Integer) As Data.DataTable
        Try
            cnx = New ClsConectarDatos
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.IniciarTransaccion()

            'Ejecutar Procedimiento
            dt = cnx.TraerDataTable("DOC_DatosCartaCompromisoTesis", codigo_tes, codigo_cac)

            cnx.TerminarTransaccion()

            Return dt
        Catch ex As Exception
            cnx.AbortarTransaccion()
            Throw ex
        End Try
    End Function




End Class
