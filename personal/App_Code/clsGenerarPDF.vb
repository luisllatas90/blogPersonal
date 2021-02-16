Imports Microsoft.VisualBasic
Imports iTextSharp.text.html

Public Class clsGenerarPDF

#Region "Declaracion de Variables"

    Private _fuente As String
    Private _anexo As String

    Public WriteOnly Property fuente() As String
        Set(ByVal value As String)
            _fuente = value
        End Set
    End Property

    Public WriteOnly Property anexo() As String
        Set(ByVal value As String)
            _anexo = value
        End Set
    End Property

#End Region

#Region "Metodos"

    ''' <summary>
    ''' Metodo para generar silabo en pdf
    ''' </summary>
    ''' <param name="codigo_cup"></param>
    ''' <param name="sourceIcon"></param>
    ''' <param name="memory"></param>
    ''' <param name="vista"></param>
    ''' <remarks></remarks>
    Public Sub mt_GenerarSilabo(ByVal codigo_cup As Integer, ByVal sourceIcon As String, ByVal memory As System.IO.Stream, Optional ByVal vista As Boolean = False)
        Dim obj As New ClsConectarDatos
        Dim x, y, z, i, j As Integer
        Dim dtDis, dtCup, dtHor, dtFer As New Data.DataTable
        Dim codigo_dis, codigo_pes, codigo_cur, codigo_cac As Integer
        Dim transversal As Boolean
        Dim errorGrupoSesion As String = "no"
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
        Try

            obj.AbrirConexion()
            dtCup = obj.TraerDataTable("CursoProgramado_Listar2", codigo_cup)
            If dtCup.Rows.Count > 0 Then
                codigo_cac = CInt(dtCup.Rows(0).Item("codigo_Cac"))
                codigo_pes = CInt(dtCup.Rows(0).Item("codigo_Pes"))
                codigo_cur = CInt(dtCup.Rows(0).Item("codigo_Cur"))
                transversal = IIf(dtCup.Rows(0).Item("transversal_pcu") = 0, False, True)
            End If
            If transversal Then
                dtDis = obj.TraerDataTable("DiseñoAsignatura_Listar", "", -1, -2, codigo_cur, codigo_cac, codigo_cup)
            Else
                dtDis = obj.TraerDataTable("DiseñoAsignatura_Listar", "", -1, codigo_pes, codigo_cur, codigo_cac, -1)
            End If

            dtHor = obj.TraerDataTable("COM_ListarHorarioDocente", codigo_cup, codigo_cac, "01/01/1900")
            obj.CerrarConexion()

            If dtDis.Rows.Count > 0 Then
                codigo_dis = CInt(dtDis.Rows(0).Item("codigo_dis"))
            Else
                Throw New Exception("¡ No existe Diseño de Asignatura o Coordinador para este Curso !")
            End If

            If dtHor.Rows.Count > 0 Then
                dtFer = New Data.DataView(dtHor, "es_feriado = 1", "", Data.DataViewRowState.CurrentRows).ToTable
            End If

            Dim pdfDoc As New iTextSharp.text.Document(iTextSharp.text.PageSize.A4)
            Dim pdfWrite As iTextSharp.text.pdf.PdfWriter = iTextSharp.text.pdf.PdfWriter.GetInstance(pdfDoc, memory)

            pdfDoc.Open()
            'pdfDoc.AddDocListener(pdfWrite)

            If vista Then mt_AddWaterMark(pdfWrite, "BORRADOR")

            Dim pdfTable As iTextSharp.text.pdf.PdfPTable = New iTextSharp.text.pdf.PdfPTable(1)
            pdfTable.WidthPercentage = 98.0F
            pdfTable.DefaultCell.Border = 0

            ' 0: Cabecera de Silabo ----------------------------------------------------------------------------------

            Dim cellTable0 As iTextSharp.text.pdf.PdfPTable = New iTextSharp.text.pdf.PdfPTable(2)
            cellTable0.WidthPercentage = 100.0F
            cellTable0.SetWidths(New Single() {60.0F, 40.0F})
            cellTable0.DefaultCell.Border = 0

            Dim srcIcon As String = sourceIcon
            Dim usatIcon As iTextSharp.text.Image = iTextSharp.text.Image.GetInstance(srcIcon)
            usatIcon.ScalePercent(60.0F)
            usatIcon.Alignment = iTextSharp.text.Element.ALIGN_LEFT

            Dim cellIcon As iTextSharp.text.pdf.PdfPCell = New iTextSharp.text.pdf.PdfPCell(usatIcon)
            cellIcon.HorizontalAlignment = 0
            cellIcon.VerticalAlignment = 2
            cellIcon.Border = 0
            'cellIcon.Rowspan = 2
            'pdfTable.AddCell(cellIcon)

            cellTable0.AddCell(cellIcon)
            cellTable0.AddCell(fc_CeldaTexto("SISTEMA DE GESTIÓN DE LA CALIDAD" & Environment.NewLine & " " & Environment.NewLine & _
                               "CÓDIGO: USAT-PM0401-D-01" & Environment.NewLine & " " & Environment.NewLine & _
                               "VERSIÓN: 04", 8.0F, 1, 0, 1, 1, 2))

            pdfTable.AddCell(cellTable0)

            pdfTable.AddCell(fc_CeldaTexto("" & Environment.NewLine, 10.0F, 1, 0, 1, 1, 1))

            '20191223-ENEVADO -----------------------------------------------------------------------------------------------------------------\
            If dtDis.Rows(0).Item("nombre_Fac").ToString.ToUpper = "--- NO DEFINIDA ---" Then
                pdfTable.AddCell(fc_CeldaTexto(" " & Environment.NewLine, 10.0F, 1, 0, 1, 1, 1))
            Else
                pdfTable.AddCell(fc_CeldaTexto("FACULTAD DE " & dtDis.Rows(0).Item("nombre_Fac").ToString.ToUpper, 10.0F, 1, 0, 1, 1, 1))
            End If
            '-----------------------------------------------------------------------------------------------------------------------------------/

            pdfTable.AddCell(fc_CeldaTexto("PROGRAMA DE ESTUDIOS DE " & dtDis.Rows(0).Item("nombreOficial_cpf").ToString.ToUpper, 10.0F, 1, 0, 1, 1, 1))
            pdfTable.AddCell(fc_CeldaTexto("SÍLABO DE " & dtDis.Rows(0).Item("nombre_Cur").ToString.ToUpper, 10.0F, 1, 0, 1, 1, 1))

            pdfTable.AddCell(fc_CeldaTexto("" & Environment.NewLine, 10.0F, 1, 0, 1, 1, 1))

            ' 1: Datos Informativos ----------------------------------------------------------------------------------

            Dim cellTable1 As iTextSharp.text.pdf.PdfPTable = New iTextSharp.text.pdf.PdfPTable(2)
            cellTable1.WidthPercentage = 100.0F
            cellTable1.SetWidths(New Single() {6.0F, 94.0F})
            cellTable1.DefaultCell.Border = 0

            cellTable1.AddCell(fc_CeldaTexto("I. ", 10.0F, 1, 0, 1, 1, 0, -1, 10))
            cellTable1.AddCell(fc_CeldaTexto("DATOS INFORMATIVOS", 10.0F, 1, 0, 1, 1, 0, -1, 10))

            cellTable1.AddCell(fc_CeldaTexto("" & Environment.NewLine, 10.0F, 1, 0, 1, 1, 1))

            Dim cellTable11 As iTextSharp.text.pdf.PdfPTable = New iTextSharp.text.pdf.PdfPTable(5)
            cellTable11.WidthPercentage = 100.0F
            cellTable11.SetWidths(New Single() {40.0F, 8.5F, 24.0F, 7.5F, 20.0F})

            cellTable11.AddCell(fc_CeldaTexto("1.1 Asignatura:", 10.0F, 1, 15, 1, 1, 0, 5, 8, "WhiteSmoke"))
            cellTable11.AddCell(fc_CeldaTexto(dtCup.Rows(0).Item("nombre_Cur").ToString, 10.0F, 0, 15, 4, 1, 0, -1, 8))
            cellTable11.AddCell(fc_CeldaTexto("1.2 Código:", 10.0F, 1, 15, 1, 1, 0, 5, 8, "WhiteSmoke"))
            cellTable11.AddCell(fc_CeldaTexto(dtCup.Rows(0).Item("identificador_Cur").ToString.ToUpper, 10.0F, 0, 15, 4, 1, 0, -1, 8))
            cellTable11.AddCell(fc_CeldaTexto("1.3 Ciclo del plan de estudios:", 10.0F, 1, 15, 1, 1, 0, 5, 8, "WhiteSmoke"))
            cellTable11.AddCell(fc_CeldaTexto(dtCup.Rows(0).Item("ciclo_Cur").ToString.ToUpper, 10.0F, 0, 15, 4, 1, 0, -1, 8))
            cellTable11.AddCell(fc_CeldaTexto("1.4 Créditos:", 10.0F, 1, 15, 1, 1, 0, 5, 8, "WhiteSmoke"))
            cellTable11.AddCell(fc_CeldaTexto(dtCup.Rows(0).Item("creditos_Cur"), 10.0F, 0, 15, 4, 1, 0, -1, 8))
            cellTable11.AddCell(fc_CeldaTexto("1.5 Tipo de asignatura:", 10.0F, 1, 15, 1, 1, 0, 5, 8, "WhiteSmoke"))
            cellTable11.AddCell(fc_CeldaTexto(IIf(dtCup.Rows(0).Item("electivo_Cur") = 0, "( X )", "(   )"), 10.0F, 0, 15, 1, 1, 1, -1, 8))
            cellTable11.AddCell(fc_CeldaTexto("Obligatorio", 10.0F, 0, 15, 1, 1, 0, -1, 8))
            cellTable11.AddCell(fc_CeldaTexto(IIf(dtCup.Rows(0).Item("electivo_Cur") = 0, "(   )", "( X )"), 10.0F, 0, 15, 1, 1, 1, -1, 8))
            cellTable11.AddCell(fc_CeldaTexto("Electivo", 10.0F, 0, 15, 1, 1, 0, -1, 8))
            cellTable11.AddCell(fc_CeldaTexto("1.6 Prerrequisito:", 10.0F, 1, 15, 1, 1, 0, 5, 8, "WhiteSmoke"))
            cellTable11.AddCell(fc_CeldaTexto(dtCup.Rows(0).Item("prerequisito").ToString.ToUpper, 10.0F, 0, 15, 4, 1, 0, -1, 8))
            cellTable11.AddCell(fc_CeldaTexto("1.7 Número de horas semanales:", 10.0F, 1, 15, 1, 3, 0, 5, 8, "WhiteSmoke"))
            cellTable11.AddCell(fc_CeldaTexto("N° de horas teóricas:", 10.0F, 0, 15, 3, 1, 1, -1, 8))
            cellTable11.AddCell(fc_CeldaTexto(dtCup.Rows(0).Item("horasTeo_Cur"), 10.0F, 0, 15, 1, 1, 1, -1, 8))
            cellTable11.AddCell(fc_CeldaTexto("N° de horas prácticas:", 10.0F, 0, 15, 3, 1, 1, -1, 8))
            cellTable11.AddCell(fc_CeldaTexto(dtCup.Rows(0).Item("horasPra_Cur"), 10.0F, 0, 15, 1, 1, 1, -1, 8))
            cellTable11.AddCell(fc_CeldaTexto("N° de horas totales:", 10.0F, 0, 15, 3, 1, 1, -1, 8))
            cellTable11.AddCell(fc_CeldaTexto(dtCup.Rows(0).Item("totalHoras_Cur"), 10.0F, 0, 15, 1, 1, 1, -1, 8))
            cellTable11.AddCell(fc_CeldaTexto("1.8 Duración:", 10.0F, 1, 15, 1, 1, 0, 5, 8, "WhiteSmoke"))
            Dim fecInicio As Date = CDate(dtCup.Rows(0).Item("fechainicio_Cup").ToString)
            Dim fecFin As Date = CDate(dtCup.Rows(0).Item("fechafin_Cup").ToString)
            cellTable11.AddCell(fc_CeldaTexto("Del (" & String.Format("{0:00}", fecInicio.Day) & "/" & String.Format("{0:00}", fecInicio.Month) & ") al (" & fecFin.Date & ")", 10.0F, 0, 15, 4, 1, 0, -1, 8))
            cellTable11.AddCell(fc_CeldaTexto("1.9 Semestre académico:", 10.0F, 1, 15, 1, 1, 0, 5, 8, "WhiteSmoke"))
            cellTable11.AddCell(fc_CeldaTexto(dtCup.Rows(0).Item("descripcion_Cac"), 10.0F, 0, 15, 4, 1, 0, -1, 8))
            cellTable11.AddCell(fc_CeldaTexto("1.10 Grupo Horario:", 10.0F, 1, 15, 1, 1, 0, 5, 8, "WhiteSmoke"))
            cellTable11.AddCell(fc_CeldaTexto(dtCup.Rows(0).Item("grupoHor_Cup"), 10.0F, 0, 15, 4, 1, 0, -1, 8))

            cellTable11.AddCell(fc_CeldaTexto("1.11 Docente coordinador:", 10.0F, 1, 15, 1, 1, 0, 5, 10, "WhiteSmoke"))

            Dim dtCoo As Data.DataTable
            obj.AbrirConexion()
            dtCoo = obj.TraerDataTable("DocenteCursoProgramado_Listar", "CO", codigo_cup)
            obj.CerrarConexion()

            If dtCoo.Rows.Count > 0 Then
                Dim _textDocenteCoordinador As New iTextSharp.text.Phrase
                _textDocenteCoordinador.Add(fc_textFrase(dtCoo.Rows(0).Item("docente").ToString & Environment.NewLine, 5, 10.0F, 0, iTextSharp.text.BaseColor.BLACK))
                _textDocenteCoordinador.Add(fc_textFrase(dtCoo.Rows(0).Item("email_Per").ToString & Environment.NewLine, 5, 10.0F, 4, iTextSharp.text.BaseColor.BLUE))
                cellTable11.AddCell(fc_CeldaTexto(_textDocenteCoordinador, 15, 4, 1, 0, 5, 8))
            End If


            Dim dtDoc As Data.DataTable
            obj.AbrirConexion()
            dtDoc = obj.TraerDataTable("DocenteCursoProgramado_Listar", "", codigo_cup)
            obj.CerrarConexion()

            'If dtDoc.Rows.Count > 0 Then
            '    cellTable11.AddCell(fc_CeldaTexto("1.12 Docente(s):", 10.0F, 1, 15, 1, 1, 0, 5, 8, "WhiteSmoke"))
            '    Dim _fontDoc As Single = 10.0F
            '    If dtDoc.Rows.Count > 5 Then _fontDoc = 8.0F
            '    Dim _textDocente As New iTextSharp.text.Phrase
            '    For x = 0 To dtDoc.Rows.Count - 1
            '        _textDocente.Add(fc_textFrase(dtDoc.Rows(x).Item("docente").ToString & Environment.NewLine, 5, _fontDoc, 0, iTextSharp.text.BaseColor.BLACK))
            '        _textDocente.Add(fc_textFrase(dtDoc.Rows(x).Item("email_Per").ToString & Environment.NewLine, 5, _fontDoc, 4, iTextSharp.text.BaseColor.BLUE))
            '    Next
            '    cellTable11.AddCell(fc_CeldaTexto(_textDocente, 15, 4, 1, 0, 5, 8))
            'Else
            '    cellTable11.AddCell(fc_CeldaTexto("1.12 Docente(s):", 10.0F, 1, 15, 1, 1, 0, 5, 8, "WhiteSmoke"))
            '    cellTable11.AddCell(fc_CeldaTexto(" No Definido ", 10.0F, 0, 15, 4, 1, 0, 5, 8))
            'End If

            ' 20200327-ENevado ===================================================================================================
            If dtDoc.Rows.Count = 0 Then
                cellTable11.AddCell(fc_CeldaTexto("1.12 Docente(s):", 10.0F, 1, 15, 1, 1, 0, 5, 8, "WhiteSmoke"))
                cellTable11.AddCell(fc_CeldaTexto(" No Definido ", 10.0F, 0, 15, 4, 1, 0, 5, 8))
                ' 13NOV2020 - JQuepuy =====================================================================================================
                cellTable11.AddCell(fc_CeldaTexto("1.13 Modalidad:", 10.0F, 1, 15, 1, 1, 0, 5, 8, "WhiteSmoke"))
                cellTable11.AddCell(fc_CeldaTexto("Educación Remota de Emergencia (ERE)" & Environment.NewLine & "(RCD 039-2020-SUNEDU / RVM 085-2020-MINEDU)", 10.0F, 0, 15, 4, 1, 0, 5, 8))
                ' =========================================================================================================================
                cellTable1.AddCell(cellTable11)
                pdfTable.AddCell(cellTable1)
                
            ElseIf dtDoc.Rows.Count < 4 Then
                cellTable11.AddCell(fc_CeldaTexto("1.12 Docente(s):", 10.0F, 1, 15, 1, 1, 0, 5, 8, "WhiteSmoke"))
                Dim _textDocente As New iTextSharp.text.Phrase
                For x = 0 To dtDoc.Rows.Count - 1
                    _textDocente.Add(fc_textFrase(dtDoc.Rows(x).Item("docente").ToString & Environment.NewLine, 5, 10.0F, 0, iTextSharp.text.BaseColor.BLACK))
                    _textDocente.Add(fc_textFrase(dtDoc.Rows(x).Item("email_Per").ToString & Environment.NewLine, 5, 10.0F, 4, iTextSharp.text.BaseColor.BLUE))
                Next
                cellTable11.AddCell(fc_CeldaTexto(_textDocente, 15, 4, 1, 0, 5, 8))
                ' 13NOV2020 - JQuepuy =====================================================================================================
                cellTable11.AddCell(fc_CeldaTexto("1.13 Modalidad:", 10.0F, 1, 15, 1, 1, 0, 5, 8, "WhiteSmoke"))
                cellTable11.AddCell(fc_CeldaTexto("Educación Remota de Emergencia (ERE)" & Environment.NewLine & "(RCD 039-2020-SUNEDU / RVM 085-2020-MINEDU)", 10.0F, 0, 15, 4, 1, 0, 5, 8))
                ' =========================================================================================================================
                cellTable1.AddCell(cellTable11)
                pdfTable.AddCell(cellTable1)

            Else
                cellTable1.AddCell(cellTable11)
                pdfTable.AddCell(cellTable1)

                Dim cellTablex As iTextSharp.text.pdf.PdfPTable = New iTextSharp.text.pdf.PdfPTable(2)
                cellTablex.WidthPercentage = 100.0F
                cellTablex.SetWidths(New Single() {6.0F, 94.0F})
                cellTablex.DefaultCell.Border = 0
                cellTablex.AddCell(fc_CeldaTexto("" & Environment.NewLine, 10.0F, 1, 0, 1, 1, 1))

                Dim cellTablexx As iTextSharp.text.pdf.PdfPTable = New iTextSharp.text.pdf.PdfPTable(5)
                cellTablexx.WidthPercentage = 100.0F
                cellTablexx.SetWidths(New Single() {40.0F, 8.5F, 24.0F, 7.5F, 20.0F})

                cellTablexx.AddCell(fc_CeldaTexto("1.12 Docente(s):", 10.0F, 1, 15, 1, 1, 0, 5, 8, "WhiteSmoke"))
                Dim _textDocente As New iTextSharp.text.Phrase
                For x = 0 To dtDoc.Rows.Count - 1
                    _textDocente.Add(fc_textFrase(dtDoc.Rows(x).Item("docente").ToString & Environment.NewLine, 5, 10.0F, 0, iTextSharp.text.BaseColor.BLACK))
                    '_textDocente.Add(fc_textFrase(" " & Environment.NewLine, 5, 5.0F, 0, iTextSharp.text.BaseColor.BLACK))
                    _textDocente.Add(fc_textFrase(dtDoc.Rows(x).Item("email_Per").ToString & Environment.NewLine, 5, 10.0F, 4, iTextSharp.text.BaseColor.BLUE))
                    _textDocente.Add(fc_textFrase(" " & Environment.NewLine, 5, 5.0F, 0, iTextSharp.text.BaseColor.BLACK))
                Next
                cellTablexx.AddCell(fc_CeldaTexto(_textDocente, 15, 4, 1, 0, 5, 8))
                ' 13NOV2020 - JQuepuy =====================================================================================================
                cellTablexx.AddCell(fc_CeldaTexto("1.13 Modalidad:", 10.0F, 1, 15, 1, 1, 0, 5, 8, "WhiteSmoke"))
                cellTablexx.AddCell(fc_CeldaTexto("Educación Remota de Emergencia (ERE)" & Environment.NewLine & "(RCD 039-2020-SUNEDU / RVM 085-2020-MINEDU)", 10.0F, 0, 15, 4, 1, 0, 5, 8))
                ' =========================================================================================================================
                cellTablex.AddCell(cellTablexx)
                pdfTable.AddCell(cellTablex)

               
            End If
            ' =========================================================================================================================

          
            'cellTable1.AddCell(cellTable11)

            'pdfTable.AddCell(cellTable1)

            pdfDoc.Add(pdfTable)

            pdfDoc.NewPage()

            If vista Then mt_AddWaterMark(pdfWrite, "BORRADOR")

            Dim pdfTable2 As iTextSharp.text.pdf.PdfPTable = New iTextSharp.text.pdf.PdfPTable(2)
            pdfTable2.WidthPercentage = 98.0F
            pdfTable2.SetWidths(New Single() {6.0F, 94.0F})
            pdfTable2.DefaultCell.Border = 0

            ' 2: Sumilla ------------------------------------------------------------------------------------

            Dim dtSum As Data.DataTable
            obj.AbrirConexion()
            If transversal Then
                dtSum = obj.TraerDataTable("PlanCursoSumilla_Listar", "PC", -1, -2, codigo_cur)
            Else
                dtSum = obj.TraerDataTable("PlanCursoSumilla_Listar", "PC", -1, codigo_pes, codigo_cur)
            End If
            obj.CerrarConexion()

            'Throw New Exception("codigo_pes: " & codigo_pes & " codigo_cur: " & codigo_cur)

            If dtSum.Rows.Count = 0 Then
                Throw New Exception("¡ No se ha registrado Sumilla para la Asignatura !")
            Else
                If dtSum.Rows(0).Item("descripcion_sum").ToString.Trim = "" Then
                    Throw New Exception("¡ No se ha registrado Sumilla para la Asignatura !")
                End If
            End If

            pdfTable2.AddCell(fc_CeldaTexto("II. ", 10.0F, 1, 0, 1, 1, 0, -1, 10))
            pdfTable2.AddCell(fc_CeldaTexto("SUMILLA", 10.0F, 1, 0, 1, 1, 0, -1, 10))

            pdfTable2.AddCell(fc_CeldaTexto("" & Environment.NewLine, 10.0F, 1, 0, 1, 1, 0))
            pdfTable2.AddCell(fc_CeldaTexto(dtSum.Rows(0).Item("descripcion_sum").ToString, 10.0F, 0, 15, 1, 1, 0))

            pdfTable2.AddCell(fc_CeldaTexto("" & Environment.NewLine, 10.0F, 1, 0, 2, 1, 0))

            ' 3: Competencias -------------------------------------------------------------------------------------------------------

            Dim dtCom As Data.DataTable
            obj.AbrirConexion()
            dtCom = obj.TraerDataTable("PerfilEgresoCurso_Listar", -1, -1, codigo_pes, codigo_cur, -1)
            obj.CerrarConexion()

            If dtCom.Rows.Count = 0 Then Throw New Exception("¡ No se ha alineado una competencia para la Asignatura !")

            Dim strCom As String = ""
            For _com As Integer = 0 To dtCom.Rows.Count - 1
                strCom = strCom & " - " & dtCom.Rows(_com).Item("nombre_com").ToString & Environment.NewLine '& " " & Environment.NewLine
            Next

            pdfTable2.AddCell(fc_CeldaTexto("III. ", 10.0F, 1, 0, 1, 1, 0, -1, 10))
            pdfTable2.AddCell(fc_CeldaTexto("COMPETENCIA(S)", 10.0F, 1, 0, 1, 1, 0, -1, 10))
            pdfTable2.AddCell(fc_CeldaTexto("" & Environment.NewLine, 10.0F, 1, 0, 1, 4, 1))
            pdfTable2.AddCell(fc_CeldaTexto("3.1 Competencia(s) de perfil de egreso", 10.0F, 1, 15, 1, 1, 0, -1, 6, "WhiteSmoke"))
            pdfTable2.AddCell(fc_CeldaTexto("La asignatura " & dtDis.Rows(0).Item("nombre_Cur").ToString & _
                                            ", que corresponde al área de estudios " & dtCom.Rows(0).Item("nombre_cat").ToString & _
                                            ", contribuye al logro del perfil de egreso, específicamente a la(s) competencia(s): " & Environment.NewLine & _
                                            strCom, 10.0F, 0, 15, 1, 1, 0))
            pdfTable2.AddCell(fc_CeldaTexto("3.2 Logro(s) de la asignatura", 10.0F, 1, 15, 1, 1, 0, -1, 6, "WhiteSmoke"))
            pdfTable2.AddCell(fc_CeldaTexto(dtSum.Rows(0).Item("competencia_sum").ToString, 10.0F, 0, 15, 1, 1, 0))

            pdfTable2.AddCell(fc_CeldaTexto("" & Environment.NewLine, 10.0F, 1, 0, 2, 1, 0))

            ' 4: Unidades didactidas -------------------------------------------------------------------------------------------------

            Dim dtUni, dtRes, dtInd, dtEvi, dtIns, dtCon As Data.DataTable
            obj.AbrirConexion()
            dtUni = obj.TraerDataTable("UnidadAsignatura_Listar", "", -1, codigo_dis, "")
            dtRes = obj.TraerDataTable("ResultadoAprendizaje_Listar", "RA", -1, codigo_dis, "")
            dtInd = obj.TraerDataTable("IndicadorAprendizaje_listar", "IA", -1, codigo_dis, "")
            dtEvi = obj.TraerDataTable("IndicadorEvidencia_Listar", "IE", -1, codigo_dis, -1)
            dtIns = obj.TraerDataTable("EvidenciaInstrumento_Listar", "IE", -1, codigo_dis, -1)
            dtCon = obj.TraerDataTable("ContenidoAsignatura_Listar", "", -1, codigo_dis)
            obj.CerrarConexion()

            pdfTable2.AddCell(fc_CeldaTexto("IV. ", 10.0F, 1, 0, 1, 1, 0))
            pdfTable2.AddCell(fc_CeldaTexto("UNIDADES DIDÁCTICAS", 10.0F, 1, 0, 1, 1, 0))


            Dim rowsInd As Integer = 0
            'Dim strInd, strEvi, strIns, strCon As String
            Dim strCon As String = "", strInd As String = ""
            Dim _nroIns As Integer = 0, _nroRes As Integer = 0, _iRes As Integer = 0, _iInd As Integer = 0, _xInd As Integer = 0
            Dim lb_Contenido As Boolean
            For x = 0 To (dtUni.Rows.Count - 1)

                'If x > 0 Then Exit For

                pdfTable2.AddCell(fc_CeldaTexto("" & Environment.NewLine, 10.0F, 1, 0, 1, 1, 1))

                Dim cellTable2 As iTextSharp.text.pdf.PdfPTable = New iTextSharp.text.pdf.PdfPTable(6)
                cellTable2.WidthPercentage = 100.0F
                'cellTable2.SetWidths(New Single() {20.0F, 20.0F, 20.0F, 15.0F, 25.0F})
                cellTable2.SetWidths(New Single() {20.0F, 7.0F, 20.0F, 7.0F, 15.0F, 31.0F})
                cellTable2.DefaultCell.Border = 0
                cellTable2.SpacingBefore = 0.0F
                cellTable2.SpacingAfter = 0.0F

                'If x = 0 Then
                cellTable2.AddCell(fc_CeldaTexto("Unidad didáctica N° " & String.Format("{0:00}", (x + 1)) & ": " & dtUni.Rows(x).Item("descripcion_uni").ToString, 9.0F, 1, 15, 6, 1, 1, 5, 6, "WhiteSmoke"))

                Dim dtResx, dtIndx, dtEvix, dtInsx, dtConx, dtCant As Data.DataTable
                dtResx = New Data.DataView(dtRes, "codigo_uni = " & dtUni.Rows(x).Item("codigo_uni"), "", Data.DataViewRowState.CurrentRows).ToTable
                dtConx = New Data.DataView(dtCon, "codigo_uni = " & dtUni.Rows(x).Item("codigo_uni"), "", Data.DataViewRowState.CurrentRows).ToTable

                dtCant = New Data.DataView(dtIns, "codigo_uni = " & dtUni.Rows(x).Item("codigo_uni"), "", Data.DataViewRowState.CurrentRows).ToTable
                _nroIns = IIf(dtCant.Rows.Count = 0, 1, dtCant.Rows.Count)
                _nroRes = dtResx.Rows.Count
                lb_Contenido = True

                For xRes As Integer = 0 To dtResx.Rows.Count - 1
                    _iRes += 1
                    dtIndx = New Data.DataView(dtInd, "codigo_res = " & dtResx.Rows(xRes).Item("codigo_res"), "", Data.DataViewRowState.CurrentRows).ToTable
                    Dim _prom_ind As Integer
                    _prom_ind = dtResx.Rows(xRes).Item("promedio_indicador")
                    strInd = ""
                    For _ind As Integer = 0 To dtIndx.Rows.Count - 1
                        _iInd += 1
                        If _prom_ind = 2 Then
                            strInd += " IND" & _iInd & "(" & Math.Round(dtIndx.Rows(_ind).Item("peso_ind"), 2) & ") +"
                        Else
                            strInd += " IND" & _iInd & " +"
                        End If
                    Next
                    strInd = strInd.TrimEnd("+")
                    If _prom_ind <> 2 Then
                        If dtIndx.Rows.Count > 1 Then
                            strInd = " (" + strInd + ") / " & dtIndx.Rows.Count
                        End If
                    End If
                    Dim _textRes As iTextSharp.text.Phrase = New iTextSharp.text.Phrase
                    _textRes.Add(fc_textFrase("Resultado de aprendizaje N° " & String.Format("{0:00}", _iRes) & " (RA" & _iRes & "): " & Environment.NewLine, 5, 8.0F, 1, iTextSharp.text.BaseColor.BLACK))
                    _textRes.Add(fc_textFrase(dtResx.Rows(xRes).Item("descripcion_res").ToString & Environment.NewLine, 5, 8.0F, 0, iTextSharp.text.BaseColor.BLACK))
                    _textRes.Add(fc_textFrase("RA" & _iRes & " =" & strInd, 5, 8.0F, 1, iTextSharp.text.BaseColor.BLACK))
                    cellTable2.AddCell(fc_CeldaTexto(_textRes, 15, 5, 1, 1, -1, 6, "WhiteSmoke"))
                    'cellTable2.AddCell(fc_CeldaTexto("Resultado de aprendizaje N° " & String.Format("{0:00}", dtResx.Rows(xRes).Item("orden_res")) & " (RA" & dtResx.Rows(xRes).Item("orden_res") & "): " & _
                    '                                 dtResx.Rows(xRes).Item("descripcion_res").ToString, 9.0F, 1, 15, 5, 1, 1, 5, 6, "WhiteSmoke"))

                    If lb_Contenido Then cellTable2.AddCell(fc_CeldaTexto("Contenidos", 8.0F, 1, 15, 1, 1, 1, 5, 6, "WhiteSmoke"))

                    cellTable2.AddCell(fc_CeldaTexto("Indicadores", 8.0F, 1, 15, 2, 1, 1, 5, 6, "WhiteSmoke"))
                    cellTable2.AddCell(fc_CeldaTexto("Evaluación", 8.0F, 1, 15, 3, 1, 1, 5, 6, "WhiteSmoke"))

                    If lb_Contenido Then
                        strCon = ""
                        For z = 0 To (dtConx.Rows.Count - 1)
                            strCon = strCon & (x + 1) & "." & (z + 1) & " " & dtConx.Rows(z).Item("descripcion").ToString & Environment.NewLine
                        Next
                        If _nroRes = 1 Then
                            cellTable2.AddCell(fc_CeldaTexto(strCon, 7.0F, 0, 15, 1, 2 + _nroIns, 0, 5, 6))
                        Else
                            cellTable2.AddCell(fc_CeldaTexto(strCon, 7.0F, 0, 15, 1, 2 + (3 * (_nroRes - 1)) + _nroIns, 0, 5, 6))
                        End If

                        lb_Contenido = False
                    End If

                    cellTable2.AddCell(fc_CeldaTexto("Descripción", 8.0F, 1, 15, 1, 1, 1, 5, 6, "WhiteSmoke"))
                    cellTable2.AddCell(fc_CeldaTexto("Peso", 8.0F, 1, 15, 1, 1, 1, 5, 6, "WhiteSmoke"))
                    cellTable2.AddCell(fc_CeldaTexto("Evidencia", 8.0F, 1, 15, 1, 1, 1, 5, 6, "WhiteSmoke"))
                    cellTable2.AddCell(fc_CeldaTexto("Peso", 8.0F, 1, 15, 1, 1, 1, 5, 6, "WhiteSmoke"))
                    cellTable2.AddCell(fc_CeldaTexto("Instrumentos", 8.0F, 1, 15, 1, 1, 1, 5, 6, "WhiteSmoke"))
                    'cellTable2.AddCell(fc_CeldaTexto("Descripción", 9.0F, 1, 15, 1, 1, 1, 5, 6, "WhiteSmoke"))

                    For xInd As Integer = 0 To dtIndx.Rows.Count - 1
                        _xInd += 1
                        Dim _can_ins As Integer = 0
                        Dim _prom_ins As Integer

                        _prom_ins = dtIndx.Rows(xInd).Item("promedio_instrumento")
                        dtEvix = New Data.DataView(dtEvi, "codigo_ind = " & dtIndx.Rows(xInd).Item("codigo_ind"), "", Data.DataViewRowState.CurrentRows).ToTable

                        If dtIndx.Rows(xInd).Item("nro_ins") = 0 Then
                            _can_ins = dtEvix.Rows.Count
                        Else
                            _can_ins = dtIndx.Rows(xInd).Item("nro_ins")
                        End If

                        cellTable2.AddCell(fc_CeldaTexto("IND" & _xInd & ": " & dtIndx.Rows(xInd).Item("descripcion_ind"), 8.0F, 0, 15, 1, _can_ins, 1, 5, 6))
                        If _prom_ind = 2 Then
                            cellTable2.AddCell(fc_CeldaTexto(Math.Round(dtIndx.Rows(xInd).Item("peso_ind") * 100, 2), 7.0F, 0, 15, 1, _can_ins, 1, 5, 6))
                        Else
                            cellTable2.AddCell(fc_CeldaTexto("Prom. Simple", 7.0F, 0, 15, 1, _can_ins, 1, 5, 6))
                        End If


                        For xEvi As Integer = 0 To dtEvix.Rows.Count - 1
                            dtInsx = New Data.DataView(dtIns, "codigo_evi = " & dtEvix.Rows(xEvi).Item("codigo_evi"), "", Data.DataViewRowState.CurrentRows).ToTable
                            cellTable2.AddCell(fc_CeldaTexto(dtEvix.Rows(xEvi).Item("descripcion_evi"), 8.0F, 0, 15, 1, dtInsx.Rows.Count, 1, 5, 6))
                            'nroIns += dtIndx.Rows.Count
                            For xIns As Integer = 0 To dtInsx.Rows.Count - 1
                                If _prom_ins = 2 Then
                                    cellTable2.AddCell(fc_CeldaTexto(Math.Round(dtInsx.Rows(xIns).Item("peso_ins") * 100, 2), 7.0F, 0, 15, 1, 1, 1, 5, 6))
                                Else
                                    cellTable2.AddCell(fc_CeldaTexto("Prom. Simple", 7.0F, 0, 15, 1, 1, 1, 5, 6))
                                End If
                                'cellTable2.AddCell(fc_CeldaTexto(0, 8.0F, 0, 15, 1, 1, 1, 5, 6))
                                cellTable2.AddCell(fc_CeldaTexto(dtInsx.Rows(xIns).Item("descripcion_ins"), 8.0F, 0, 15, 1, 1, 1, 5, 6))
                                'cellTable2.AddCell(fc_CeldaTexto("", 9.0F, 0, 15, 1, 1, 1, 5, 6))
                            Next

                        Next

                    Next

                Next

                pdfTable2.AddCell(cellTable2)

            Next


            'pdfTable2.AddCell(fc_CeldaTexto("" & Environment.NewLine, 10.0F, 1, 0, 2, 1, 0))

            ' 5: Estrategias Didacticas -------------------------------------------------------------------------------------

            Dim dtEst As Data.DataTable
            obj.AbrirConexion()
            dtEst = obj.TraerDataTable("EstrategiaDidactica_Listar", -1, codigo_dis, "")
            obj.CerrarConexion()

            pdfTable2.AddCell(fc_CeldaTexto("V. ", 10.0F, 1, 0, 1, 1, 0))
            pdfTable2.AddCell(fc_CeldaTexto("ESTRATEGIAS DIDÁCTICAS", 10.0F, 1, 0, 1, 1, 0))
            pdfTable2.AddCell(fc_CeldaTexto("" & Environment.NewLine, 10.0F, 1, 0, 1, 1, 0))

            Dim strEst As String
            strEst = "Para el desarrollo de la asignatura se emplearán las siguientes estrategias didácticas:" & Environment.NewLine & Environment.NewLine
            For x = 0 To (dtEst.Rows.Count - 1)
                strEst = strEst & "• " & dtEst.Rows(x).Item("descripcion_dis").ToString & Environment.NewLine
                'list.Add(New iTextSharp.text.ListItem(dtEst.Rows(x).Item("descripcion_dis").ToString))
            Next

            pdfTable2.AddCell(fc_CeldaTexto(strEst, 10.0F, 0, 15, 1, 1, 0))
            'pdfTable2.AddCell(fc_CeldaTexto3(list, 10.0F, 0, 15, 1, 1, 0))

            pdfTable2.AddCell(fc_CeldaTexto("" & Environment.NewLine, 10.0F, 1, 0, 2, 1, 0))

            ' 6: Evaluacion ---------------------------------------------------------------------------------------------------

            Dim _total_eva As Integer = 0

            pdfTable2.AddCell(fc_CeldaTexto("VI. ", 10.0F, 1, 0, 1, 1, 0))
            pdfTable2.AddCell(fc_CeldaTexto("EVALUACIÓN", 10.0F, 1, 0, 1, 1, 0))
            pdfTable2.AddCell(fc_CeldaTexto("" & Environment.NewLine, 10.0F, 1, 0, 1, 1, 1))

            Dim cellTable3 As iTextSharp.text.pdf.PdfPTable = New iTextSharp.text.pdf.PdfPTable(5)
            cellTable3.WidthPercentage = 100.0F
            cellTable3.SetWidths(New Single() {40.0F, 20.0F, 10.0F, 10.0F, 20.0F})
            cellTable3.DefaultCell.Border = 0

            cellTable3.AddCell(fc_CeldaTexto("6.1 Criterios de evaluación", 10.0F, 1, 15, 5, 1, 0, -1, 6, "WhiteSmoke"))

            Dim dtNor As Data.DataTable
            obj.AbrirConexion()
            dtNor = obj.TraerDataTable("DEA_CicloAcademicoConf_Normas_listar", "", codigo_cac)
            obj.CerrarConexion()

            Dim _textCri As iTextSharp.text.Phrase
            _textCri = New iTextSharp.text.Phrase
            _textCri.Add(fc_textFrase("La calificación para todas las asignaturas, se realizará en la escala vigesimal, es decir, de cero (00) a veinte (20). " & _
                                      "La nota aprobatoria mínima es catorce (14). " & Environment.NewLine & " " & Environment.NewLine, 5, 10.0F, 1, iTextSharp.text.BaseColor.BLACK))
            _textCri.Add(fc_textFrase("La evaluación será formativa y sumativa, se aplicará evaluaciones de entrada y de salida, considerando las evidencias " & _
                                             "(por ejemplo informes, exposiciones sobre textos académicos) e instrumentos que se emplearán para la evaluación " & _
                                             "de cada una de ellas. Por ejemplo: listas de cotejo, escalas estimativas, rúbricas, pruebas de ensayo etc.", 5, 10.0F, 0, iTextSharp.text.BaseColor.BLACK))
            cellTable3.AddCell(fc_CeldaTexto(_textCri, 15, 5, 1, 0, -1, 6, ""))
            Dim _textEva As iTextSharp.text.Phrase
            _textEva = New iTextSharp.text.Phrase
            _textEva.Add(fc_textFrase("Normatividad: " & Environment.NewLine & " " & Environment.NewLine, 5, 10.0F, 1, iTextSharp.text.BaseColor.BLACK))

            For _nor As Integer = 0 To dtNor.Rows.Count - 1
                _textEva.Add(fc_textFrase(" - " & dtNor.Rows(_nor).Item("descripcion_nor").ToString & Environment.NewLine & " " & Environment.NewLine, 5, 10.0F, 0, iTextSharp.text.BaseColor.BLACK))
            Next

            'cellTable3.AddCell(fc_CeldaTexto(_textEva, 15, 5, 1, 0, -1, 6, ""))

            'cellTable3.AddCell(fc_CeldaTexto("6.2 Sistema de calificación", 10.0F, 1, 15, 5, 1, 0, -1, 6, "WhiteSmoke"))
            'cellTable3.AddCell(fc_CeldaTexto("Fórmula para la obtención de la nota de resultado de aprendizaje (RA)", 10.0F, 1, 15, 5, 1, 1, -1, 6, "WhiteSmoke"))
            '_textEva = New iTextSharp.text.Phrase
            '_textEva.Add(fc_textFrase("RA ", 5, 10.0F, 1, iTextSharp.text.BaseColor.BLACK))
            '_textEva.Add(fc_textFrase("= promedio (Calificaciones obtenidas en sus indicadores)", 5, 10.0F, 0, iTextSharp.text.BaseColor.BLACK))
            'cellTable3.AddCell(fc_CeldaTexto(_textEva, 15, 5, 1, 1))

            'cellTable3.AddCell(fc_CeldaTexto("Evaluación", 10.0F, 1, 15, 1, 1, 1, 5, 6, "WhiteSmoke"))
            'cellTable3.AddCell(fc_CeldaTexto("Unidad(es) en la(s) que se trabaja", 10.0F, 1, 15, 1, 1, 1, 5, 6, "WhiteSmoke"))
            'cellTable3.AddCell(fc_CeldaTexto("Peso", 10.0F, 1, 15, 2, 1, 1, 5, 6, "WhiteSmoke"))
            'cellTable3.AddCell(fc_CeldaTexto("N° de evaluaciones", 10.0F, 1, 15, 1, 1, 1, 5, 6, "WhiteSmoke"))

            ''iTextSharp.text.List 

            'Dim strRes As String = " ="
            'Dim prom_inst As Integer = 0, cant_xfila As Integer = 0
            'For x = 0 To (dtRes.Rows.Count - 1)
            '    cant_xfila = 1

            '    cellTable3.AddCell(fc_CeldaTexto("Resultado de aprendizaje N° " & String.Format("{0:00}", (x + 1)) & " (RA" & (x + 1) & ")", 10.0F, 0, 15, 1, cant_xfila, 0, 5))
            '    cellTable3.AddCell(fc_CeldaTexto(dtRes.Rows(x).Item("unidad_res").ToString, 10.0F, 0, 15, 1, cant_xfila, 1, 5))

            '    cellTable3.AddCell(fc_CeldaTexto(dtRes.Rows(x).Item("peso_res"), 10.0F, 0, 15, 2, 1, 1))
            '    'cellTable3.AddCell(fc_CeldaTexto(String.Format("{0:00}", dtRes.Rows(x).Item("cant_eva")), 10.0F, 0, 15, 1, 1, 1))
            '    cellTable3.AddCell(fc_CeldaTexto(String.Format("{0:00}", dtRes.Rows(x).Item("cant_ins")), 10.0F, 0, 15, 1, 1, 1))
            '    'End If
            '    strRes = strRes & " RA" & (x + 1) & "(" & dtRes.Rows(x).Item("peso_res") & ") +"
            '    '_total_eva += CInt(dtRes.Rows(x).Item("cant_eva"))
            '    _total_eva += CInt(dtRes.Rows(x).Item("cant_ins"))
            'Next
            'strRes = strRes.TrimEnd("+")

            'cellTable3.AddCell(fc_CeldaTexto("Total de evaluaciones programadas", 10.0F, 1, 15, 4, 1, 1, -1, 6, "WhiteSmoke"))
            'cellTable3.AddCell(fc_CeldaTexto(String.Format("{0:00}", _total_eva), 10.0F, 1, 15, 1, 1, 1))
            'cellTable3.AddCell(fc_CeldaTexto("Fórmula para la obtención de la nota final de la asignatura (NF)", 10.0F, 1, 15, 5, 1, 1, -1, 6, "WhiteSmoke"))
            '_textEva = New iTextSharp.text.Phrase
            '_textEva.Add(fc_textFrase("NF", 5, 10.0F, 1, iTextSharp.text.BaseColor.BLACK))
            '_textEva.Add(fc_textFrase(strRes, 5, 10.0F, 0, iTextSharp.text.BaseColor.BLACK))
            'cellTable3.AddCell(fc_CeldaTexto(_textEva, 15, 5, 1, 1))

            'pdfTable2.AddCell(cellTable3)

            cellTable3.AddCell(fc_CeldaTexto(_textEva, 15, 5, 1, 0, -1, 6, ""))

            pdfTable2.AddCell(cellTable3)

            ' 20200327-ENevado ===================================================================================================

            pdfTable2.AddCell(fc_CeldaTexto("" & Environment.NewLine, 10.0F, 1, 0, 1, 1, 1))

            Dim cellTablen As iTextSharp.text.pdf.PdfPTable = New iTextSharp.text.pdf.PdfPTable(5)
            cellTablen.WidthPercentage = 100.0F
            cellTablen.SetWidths(New Single() {40.0F, 20.0F, 10.0F, 10.0F, 20.0F})
            cellTablen.DefaultCell.Border = 0

            cellTablen.AddCell(fc_CeldaTexto("6.2 Sistema de calificación", 10.0F, 1, 15, 5, 1, 0, -1, 6, "WhiteSmoke"))
            cellTablen.AddCell(fc_CeldaTexto("Fórmula para la obtención de la nota de resultado de aprendizaje (RA)", 10.0F, 1, 15, 5, 1, 1, -1, 6, "WhiteSmoke"))
            _textEva = New iTextSharp.text.Phrase
            _textEva.Add(fc_textFrase("RA ", 5, 10.0F, 1, iTextSharp.text.BaseColor.BLACK))
            _textEva.Add(fc_textFrase("= promedio (Calificaciones obtenidas en sus indicadores)", 5, 10.0F, 0, iTextSharp.text.BaseColor.BLACK))
            cellTablen.AddCell(fc_CeldaTexto(_textEva, 15, 5, 1, 1))

            cellTablen.AddCell(fc_CeldaTexto("Evaluación", 10.0F, 1, 15, 1, 1, 1, 5, 6, "WhiteSmoke"))
            cellTablen.AddCell(fc_CeldaTexto("Unidad(es) en la(s) que se trabaja", 10.0F, 1, 15, 1, 1, 1, 5, 6, "WhiteSmoke"))
            cellTablen.AddCell(fc_CeldaTexto("Peso", 10.0F, 1, 15, 2, 1, 1, 5, 6, "WhiteSmoke"))
            cellTablen.AddCell(fc_CeldaTexto("N° de evaluaciones", 10.0F, 1, 15, 1, 1, 1, 5, 6, "WhiteSmoke"))

            Dim strRes As String = " ="
            Dim prom_inst As Integer = 0, cant_xfila As Integer = 0
            For x = 0 To (dtRes.Rows.Count - 1)
                cant_xfila = 1

                cellTablen.AddCell(fc_CeldaTexto("Resultado de aprendizaje N° " & String.Format("{0:00}", (x + 1)) & " (RA" & (x + 1) & ")", 10.0F, 0, 15, 1, cant_xfila, 0, 5))
                cellTablen.AddCell(fc_CeldaTexto(dtRes.Rows(x).Item("unidad_res").ToString, 10.0F, 0, 15, 1, cant_xfila, 1, 5))

                cellTablen.AddCell(fc_CeldaTexto(dtRes.Rows(x).Item("peso_res"), 10.0F, 0, 15, 2, 1, 1))
                cellTablen.AddCell(fc_CeldaTexto(String.Format("{0:00}", dtRes.Rows(x).Item("cant_ins")), 10.0F, 0, 15, 1, 1, 1))
                strRes = strRes & " RA" & (x + 1) & "(" & dtRes.Rows(x).Item("peso_res") & ") +"
                _total_eva += CInt(dtRes.Rows(x).Item("cant_ins"))
            Next
            strRes = strRes.TrimEnd("+")

            cellTablen.AddCell(fc_CeldaTexto("Total de evaluaciones programadas", 10.0F, 1, 15, 4, 1, 1, -1, 6, "WhiteSmoke"))
            cellTablen.AddCell(fc_CeldaTexto(String.Format("{0:00}", _total_eva), 10.0F, 1, 15, 1, 1, 1))
            cellTablen.AddCell(fc_CeldaTexto("Fórmula para la obtención de la nota final de la asignatura (NF)", 10.0F, 1, 15, 5, 1, 1, -1, 6, "WhiteSmoke"))
            _textEva = New iTextSharp.text.Phrase
            _textEva.Add(fc_textFrase("NF", 5, 10.0F, 1, iTextSharp.text.BaseColor.BLACK))
            _textEva.Add(fc_textFrase(strRes, 5, 10.0F, 0, iTextSharp.text.BaseColor.BLACK))
            cellTablen.AddCell(fc_CeldaTexto(_textEva, 15, 5, 1, 1))

            pdfTable2.AddCell(cellTablen)

            ' =========================================================================================================================

            pdfTable2.AddCell(fc_CeldaTexto("" & Environment.NewLine, 10.0F, 1, 0, 2, 1, 0))

            ' 7: Referencias --------------------------------------------------------------------------------------------------------------

            Dim dtRef As Data.DataTable
            obj.AbrirConexion()
            dtRef = obj.TraerDataTable("ReferenciaBibliografica_Listar", -1, codigo_dis, -1, "")
            obj.CerrarConexion()

            pdfTable2.AddCell(fc_CeldaTexto("VII. ", 10.0F, 1, 0, 1, 1, 0))
            pdfTable2.AddCell(fc_CeldaTexto("REFERENCIAS", 10.0F, 1, 0, 1, 1, 0))

            Dim dtRef1, dtRef2, dtRef3, dtRef4 As Data.DataTable
            Dim strRef1, strRef2, strRef3 As String

            dtRef1 = New Data.DataView(dtRef, "nombre = " & "'USAT'", "", Data.DataViewRowState.CurrentRows).ToTable
            dtRef2 = New Data.DataView(dtRef, "nombre = " & "'Complementarias' AND tipo_ref = 0", "", Data.DataViewRowState.CurrentRows).ToTable
            dtRef4 = New Data.DataView(dtRef, "nombre = " & "'Complementarias' AND tipo_ref = 1", "", Data.DataViewRowState.CurrentRows).ToTable
            dtRef3 = New Data.DataView(dtRef, "nombre = " & "'Investigaciones de docentes'", "", Data.DataViewRowState.CurrentRows).ToTable

            strRef1 = "" : strRef2 = "" : strRef3 = ""

            pdfTable2.AddCell(fc_CeldaTexto("" & Environment.NewLine, 10.0F, 1, 0, 1, IIf(dtRef4.Rows.Count > 0, 8, 6), 1))

            For x = 0 To (dtRef1.Rows.Count - 1)
                strRef1 = strRef1 & "• " & dtRef1.Rows(x).Item("descripcion_ref").ToString & Environment.NewLine
            Next
            For x = 0 To (dtRef2.Rows.Count - 1)
                strRef2 = strRef2 & "• " & dtRef2.Rows(x).Item("descripcion_ref").ToString & Environment.NewLine
            Next
            For x = 0 To (dtRef3.Rows.Count - 1)
                strRef3 = strRef3 & "• " & dtRef3.Rows(x).Item("descripcion_ref").ToString & Environment.NewLine
            Next
            Dim _textRefCompUrl As New iTextSharp.text.Phrase
            For x = 0 To (dtRef4.Rows.Count - 1)
                'strRef4 = strRef4 & "• " & dtRef4.Rows(x).Item("descripcion_ref").ToString & Environment.NewLine
                _textRefCompUrl.Add(fc_textFrase("• " & dtRef4.Rows(x).Item("descripcion_ref").ToString & " Recuperado de: " & Environment.NewLine, 5, 10.0F, 0, iTextSharp.text.BaseColor.BLACK))
                _textRefCompUrl.Add(fc_textFrase(dtRef4.Rows(x).Item("observacion_ref").ToString & Environment.NewLine, 5, 10.0F, 4, iTextSharp.text.BaseColor.BLUE))
            Next

            pdfTable2.AddCell(fc_CeldaTexto("7.1 Referencias USAT", 10.0F, 1, 15, 1, 1, 0, -1, 6, "WhiteSmoke"))
            pdfTable2.AddCell(fc_CeldaTexto(strRef1, 10.0F, 0, 15, 1, 1, 0))

            pdfTable2.AddCell(fc_CeldaTexto("7.2 Referencias complementarias", 10.0F, 1, 15, 1, 1, 0, -1, 6, "WhiteSmoke"))
            pdfTable2.AddCell(fc_CeldaTexto(strRef2, 10.0F, 0, 15, 1, 1, 0))

            If dtRef4.Rows.Count > 0 Then
                'pdfTable2.AddCell(fc_CeldaTexto("Enlaces de Internet", 10.0F, 1, 15, 1, 1, 0, -1, 6)) 'Comentado por Luis Q.T. | 27AGO2020 | GLPI 38371
                pdfTable2.AddCell(fc_CeldaTexto(_textRefCompUrl, 15, 1, 1, 0, 5, 8))
            End If

            pdfTable2.AddCell(fc_CeldaTexto("7.3 Investigaciones de docentes", 10.0F, 1, 15, 1, 1, 0, -1, 6, "WhiteSmoke"))
            pdfTable2.AddCell(fc_CeldaTexto(strRef3, 10.0F, 0, 15, 1, 1, 0))

            pdfTable2.AddCell(fc_CeldaTexto("" & Environment.NewLine, 10.0F, 1, 0, 2, 1, 0))

            ' 8: Programacion de Actividades -------------------------------------------------------------------------------

            Dim dtGru, dtSes, dtAct, dtEva As Data.DataTable
            Dim corCont As Integer
            obj.AbrirConexion()
            dtGru = obj.TraerDataTable("GrupoContenidoAsignatura_Listar", "GC", -1, codigo_dis)
            dtAct = obj.TraerDataTable("ActividadAsignatura_Listar", "AA", -1, codigo_dis)
            dtSes = obj.TraerDataTable("SesionAsignatura_Listar", "SC", codigo_cup, codigo_dis, "")
            dtEva = obj.TraerDataTable("EvaluacionCurso_Listar", "EC", -1, codigo_cup, -1, -1)
            obj.CerrarConexion()

            pdfTable2.AddCell(fc_CeldaTexto("VIII. ", 10.0F, 1, 0, 1, 1, 0))
            pdfTable2.AddCell(fc_CeldaTexto("PROGRAMACIÓN DE ACTIVIDADES", 10.0F, 1, 0, 1, 1, 0))

            Dim _ngrupo As Boolean

            For x = 0 To (dtUni.Rows.Count - 1)

                _ngrupo = False

                pdfTable2.AddCell(fc_CeldaTexto("" & Environment.NewLine, 10.0F, 1, 0, 1, 1, 1))

                Dim cellTable4 As iTextSharp.text.pdf.PdfPTable = New iTextSharp.text.pdf.PdfPTable(4)
                cellTable4.WidthPercentage = 100.0F
                cellTable4.SetWidths(New Single() {20.0F, 35.0F, 30.0F, 15.0F})
                cellTable4.DefaultCell.Border = 0

                cellTable4.AddCell(fc_CeldaTexto("Unidad didáctica N° " & String.Format("{0:00}", (x + 1)) & ": " & dtUni.Rows(x).Item("descripcion_uni").ToString, 9.0F, 1, 15, 4, 1, 1, -1, 6, "WhiteSmoke"))
                cellTable4.AddCell(fc_CeldaTexto("Sesión" & Environment.NewLine & "(N° / dd-mm)", 9.0F, 1, 15, 1, 1, 1, 5, 6, "WhiteSmoke"))
                cellTable4.AddCell(fc_CeldaTexto("Contenidos", 9.0F, 1, 15, 1, 1, 1, 5, 6, "WhiteSmoke"))
                cellTable4.AddCell(fc_CeldaTexto("Actividades", 9.0F, 1, 15, 1, 1, 1, 5, 6, "WhiteSmoke"))
                cellTable4.AddCell(fc_CeldaTexto("Evaluaciones", 9.0F, 1, 15, 1, 1, 1, 5, 6, "WhiteSmoke"))

                Dim dtGrux, dtCony, dtActx, dtSesx, dtEvax As Data.DataTable
                Dim strCony, strActx, strFecx, strMes, strEvax As String
                Dim strFec As String()

                strCony = "" : strActx = "" : strFecx = "" : strMes = "" : strEvax = ""
                dtGrux = New Data.DataView(dtGru, "codigo_uni = " & dtUni.Rows(x).Item("codigo_uni"), "", Data.DataViewRowState.CurrentRows).ToTable

                corCont = 0

                For y = 0 To (dtGrux.Rows.Count - 1)

                    dtCony = New Data.DataView(dtCon, "codigo_gru = " & dtGrux.Rows(y).Item("codigo_gru"), "", Data.DataViewRowState.CurrentRows).ToTable
                    dtActx = New Data.DataView(dtAct, "codigo_gru = " & dtGrux.Rows(y).Item("codigo_gru"), "", Data.DataViewRowState.CurrentRows).ToTable
                    dtSesx = New Data.DataView(dtSes, "codigo_gru = " & dtGrux.Rows(y).Item("codigo_gru"), "", Data.DataViewRowState.CurrentRows).ToTable
                    dtEvax = New Data.DataView(dtEva, "codigo_gru = " & dtGrux.Rows(y).Item("codigo_gru"), "", Data.DataViewRowState.CurrentRows).ToTable
                    strCony = "" : strActx = "" : strFecx = "" : strMes = "" : strEvax = ""
                    strFec = Nothing
                    If dtCony.Rows.Count = 0 Then
                        'Throw New Exception("Unidad didáctica N° " & String.Format("{0:00}", (x + 1)))
                    Else
                        For z = 0 To (dtCony.Rows.Count - 1)
                            corCont = corCont + 1
                            strCony = strCony & (x + 1) & "." & corCont & " " & dtCony.Rows(z).Item("descripcion").ToString.Replace("'", "").Replace(vbCr, " ").Replace(vbLf, "") & Environment.NewLine
                        Next
                    End If
                    For z = 0 To (dtActx.Rows.Count - 1)
                        strActx = strActx & "• " & dtActx.Rows(z).Item("descripcion").ToString.Replace("'", "").Replace(vbCr, " ").Replace(vbLf, "") & Environment.NewLine
                    Next
                    For z = 0 To (dtEvax.Rows.Count - 1)
                        'strEvax = strEvax & "• " & dtEvax.Rows(z).Item("descripcion_ins").ToString & Environment.NewLine
                        strEvax = strEvax & "• " & dtEvax.Rows(z).Item("descripcion_evi").ToString.Replace("'", "").Replace(vbCr, " ").Replace(vbLf, "") & " (" & dtEvax.Rows(z).Item("descripcion_ins").ToString.Replace("'", "").Replace(vbCr, " ").Replace(vbLf, "") & ")" & Environment.NewLine
                    Next
                    If dtSesx.Rows.Count = 1 Then

                        If _ngrupo Then

                            pdfTable2.AddCell(fc_CeldaTexto("" & Environment.NewLine, 10.0F, 1, 0, 1, 1, 1))

                            cellTable4 = New iTextSharp.text.pdf.PdfPTable(4)
                            cellTable4.WidthPercentage = 100.0F
                            cellTable4.SetWidths(New Single() {20.0F, 35.0F, 30.0F, 15.0F})
                            cellTable4.DefaultCell.Border = 0

                        End If

                        If dtSesx.Rows(0).Item("fecha_ses").ToString = "-" Then
                            strFecx = "-"
                        Else
                            strFec = dtSesx.Rows(0).Item("fecha_ses").ToString.Split(",")
                            For j = 0 To strFec.Length - 1
                                strMes = CDate(strFec(j)).Month
                                strFecx = strFecx & CDate(strFec(j)).Day & " -"
                            Next
                            strFecx = strFecx.TrimEnd("-") & " de " & MonthName(strMes)
                            If dtFer.Rows.Count > 0 Then
                                For df As Integer = 0 To dtFer.Rows.Count - 1
                                    If dtFer.Rows(df).Item("es_feriado") Then
                                        If CDate(strFec(0)) > CDate(dtFer.Rows(df).Item("ord_fechas")) Then
                                            cellTable4.AddCell(fc_CeldaTexto(CDate(dtFer.Rows(df).Item("ord_fechas")).Day & " de " & MonthName(CDate(dtFer.Rows(df).Item("ord_fechas")).Month), 9.0F, 0, 15, 1, 1, 0))
                                            cellTable4.AddCell(fc_CeldaTexto(dtFer.Rows(df).Item("evento").ToString.ToUpper, 9.0F, 0, 15, 3, 1, 1))
                                            dtFer.Rows(df).Item("es_feriado") = False
                                            'Exit For
                                        End If
                                    End If
                                Next
                            End If
                        End If
                        'cellTable4.AddCell(fc_CeldaTexto(dtSesx.Rows(0).Item("orden_ses").ToString & " / " & strFecx, 9.0F, 0, 15, 1, 1, 0))
                        cellTable4.AddCell(fc_CeldaTexto(dtSesx.Rows(0).Item("nombre_ses").ToString.Replace("Sesión ", "") & " / " & strFecx, 8.0F, 0, 15, 1, 1, 0))
                        cellTable4.AddCell(fc_CeldaTexto(strCony, 7.0F, 0, 15, 1, 1, 0))
                        cellTable4.AddCell(fc_CeldaTexto(strActx, 8.0F, 0, 15, 1, 1, 0))
                        cellTable4.AddCell(fc_CeldaTexto(strEvax, 8.0F, 0, 15, 1, 1, 0))

                        If _ngrupo Then
                            pdfTable2.AddCell(cellTable4)
                        End If

                    Else
                        ' 20200327-ENevado ===================================================================================================
                        Dim _xrow As Integer = 25
                        If dtSesx.Rows.Count > _xrow Then

                            If Not _ngrupo Then
                                pdfTable2.AddCell(cellTable4)
                            End If

                            _ngrupo = True

                            Dim _round, _mod As Integer
                            _round = Math.Floor(dtSesx.Rows.Count / _xrow)
                            _mod = dtSesx.Rows.Count Mod _xrow

                            Dim lb_session As Boolean
                            For _row As Integer = 0 To (_round - 1)
                                If _row = 0 Then

                                    pdfTable2.AddCell(fc_CeldaTexto("" & Environment.NewLine, 10.0F, 1, 0, 1, 1, 1))

                                    cellTable4 = New iTextSharp.text.pdf.PdfPTable(4)
                                    cellTable4.WidthPercentage = 100.0F
                                    cellTable4.SetWidths(New Single() {20.0F, 35.0F, 30.0F, 15.0F})
                                    cellTable4.DefaultCell.Border = 0

                                    For i = (_row * _xrow) To (((_row + 1) * _xrow) - 1)
                                        lb_session = True
                                        strFecx = ""
                                        If i = (_row * _xrow) Then
                                            If dtSesx.Rows(i).Item("fecha_ses").ToString = "-" Then
                                                strFecx = "-"
                                            Else
                                                strFec = dtSesx.Rows(i).Item("fecha_ses").ToString.Split(",")
                                                For j = 0 To strFec.Length - 1
                                                    strMes = CDate(strFec(j)).Month
                                                    strFecx = strFecx & CDate(strFec(j)).Day & " -"
                                                Next
                                                strFecx = strFecx.TrimEnd("-") & " de " & MonthName(strMes)
                                                If dtFer.Rows.Count > 0 Then
                                                    For df As Integer = 0 To dtFer.Rows.Count - 1
                                                        If dtFer.Rows(df).Item("es_feriado") Then
                                                            If CDate(strFec(0)) > CDate(dtFer.Rows(df).Item("ord_fechas")) Then
                                                                cellTable4.AddCell(fc_CeldaTexto(CDate(dtFer.Rows(df).Item("ord_fechas")).Day & " de " & MonthName(CDate(dtFer.Rows(df).Item("ord_fechas")).Month), 9.0F, 0, 15, 1, 1, 0))
                                                                cellTable4.AddCell(fc_CeldaTexto(dtFer.Rows(df).Item("evento").ToString.ToUpper, 9.0F, 0, 15, 3, 1, 1))
                                                                dtFer.Rows(df).Item("es_feriado") = False
                                                                'Exit For
                                                            End If
                                                        End If
                                                    Next
                                                End If
                                            End If
                                            cellTable4.AddCell(fc_CeldaTexto(dtSesx.Rows(i).Item("nombre_ses").ToString.Replace("Sesión ", "") & " / " & strFecx, 8.0F, 0, 15, 1, 1, 0))
                                            cellTable4.AddCell(fc_CeldaTexto(strCony, 7.0F, 0, 15, 1, _xrow, 0))
                                            cellTable4.AddCell(fc_CeldaTexto(strActx, 8.0F, 0, 15, 1, _xrow, 0))
                                            cellTable4.AddCell(fc_CeldaTexto(strEvax, 8.0F, 0, 15, 1, _xrow, 0))
                                        Else
                                            If dtSesx.Rows(i).Item("fecha_ses").ToString = "-" Then
                                                strFecx = "-"
                                            Else
                                                strFec = dtSesx.Rows(i).Item("fecha_ses").ToString.Split(",")
                                                For j = 0 To strFec.Length - 1
                                                    strMes = CDate(strFec(j)).Month
                                                    strFecx = strFecx & CDate(strFec(j)).Day & " -"
                                                Next
                                                strFecx = strFecx.TrimEnd("-") & " de " & MonthName(strMes)
                                                If dtFer.Rows.Count > 0 Then
                                                    For df As Integer = 0 To dtFer.Rows.Count - 1
                                                        If dtFer.Rows(df).Item("es_feriado") Then
                                                            If CDate(strFec(0)) > CDate(dtFer.Rows(df).Item("ord_fechas")) Then
                                                                If i = (dtSesx.Rows.Count - 1) Then
                                                                    If lb_session Then
                                                                        cellTable4.AddCell(fc_CeldaTexto(dtSesx.Rows(i).Item("nombre_ses").ToString.Replace("Sesión ", "") & " / " & strFecx, 8.0F, 0, 15, 1, 1, 0))
                                                                        lb_session = False
                                                                    End If
                                                                    cellTable4.AddCell(fc_CeldaTexto(CDate(dtFer.Rows(df).Item("ord_fechas")).Day & " de " & MonthName(CDate(dtFer.Rows(df).Item("ord_fechas")).Month), 9.0F, 0, 15, 1, 1, 0))
                                                                    cellTable4.AddCell(fc_CeldaTexto(dtFer.Rows(df).Item("evento").ToString.ToUpper, 9.0F, 0, 15, 3, 1, 1))
                                                                    dtFer.Rows(df).Item("es_feriado") = False
                                                                End If
                                                                'Exit For
                                                            End If
                                                        End If
                                                    Next
                                                End If
                                            End If
                                            If lb_session Then cellTable4.AddCell(fc_CeldaTexto(dtSesx.Rows(i).Item("nombre_ses").ToString.Replace("Sesión ", "") & " / " & strFecx, 8.0F, 0, 15, 1, 1, 0))
                                        End If
                                    Next
                                    pdfTable2.AddCell(cellTable4)
                                Else
                                    pdfTable2.AddCell(fc_CeldaTexto("" & Environment.NewLine, 10.0F, 1, 0, 1, 1, 1))

                                    cellTable4 = New iTextSharp.text.pdf.PdfPTable(4)
                                    cellTable4.WidthPercentage = 100.0F
                                    cellTable4.SetWidths(New Single() {20.0F, 35.0F, 30.0F, 15.0F})
                                    cellTable4.DefaultCell.Border = 0

                                    For i = (_row * _xrow) To (((_row + 1) * _xrow) - 1)
                                        lb_session = True
                                        strFecx = ""
                                        If i = (_row * _xrow) Then
                                            If dtSesx.Rows(i).Item("fecha_ses").ToString = "-" Then
                                                strFecx = "-"
                                            Else
                                                strFec = dtSesx.Rows(i).Item("fecha_ses").ToString.Split(",")
                                                For j = 0 To strFec.Length - 1
                                                    strMes = CDate(strFec(j)).Month
                                                    strFecx = strFecx & CDate(strFec(j)).Day & " -"
                                                Next
                                                strFecx = strFecx.TrimEnd("-") & " de " & MonthName(strMes)
                                                If dtFer.Rows.Count > 0 Then
                                                    For df As Integer = 0 To dtFer.Rows.Count - 1
                                                        If dtFer.Rows(df).Item("es_feriado") Then
                                                            If CDate(strFec(0)) > CDate(dtFer.Rows(df).Item("ord_fechas")) Then
                                                                cellTable4.AddCell(fc_CeldaTexto(CDate(dtFer.Rows(df).Item("ord_fechas")).Day & " de " & MonthName(CDate(dtFer.Rows(df).Item("ord_fechas")).Month), 9.0F, 0, 15, 1, 1, 0))
                                                                cellTable4.AddCell(fc_CeldaTexto(dtFer.Rows(df).Item("evento").ToString.ToUpper, 9.0F, 0, 15, 3, 1, 1))
                                                                dtFer.Rows(df).Item("es_feriado") = False
                                                                'Exit For
                                                            End If
                                                        End If
                                                    Next
                                                End If
                                            End If
                                            cellTable4.AddCell(fc_CeldaTexto(dtSesx.Rows(i).Item("nombre_ses").ToString.Replace("Sesión ", "") & " / " & strFecx, 8.0F, 0, 15, 1, 1, 0))
                                            cellTable4.AddCell(fc_CeldaTexto(strCony, 7.0F, 0, 15, 1, _xrow, 0))
                                            cellTable4.AddCell(fc_CeldaTexto(strActx, 8.0F, 0, 15, 1, _xrow, 0))
                                            cellTable4.AddCell(fc_CeldaTexto(strEvax, 8.0F, 0, 15, 1, _xrow, 0))
                                        Else
                                            If dtSesx.Rows(i).Item("fecha_ses").ToString = "-" Then
                                                strFecx = "-"
                                            Else
                                                strFec = dtSesx.Rows(i).Item("fecha_ses").ToString.Split(",")
                                                For j = 0 To strFec.Length - 1
                                                    strMes = CDate(strFec(j)).Month
                                                    strFecx = strFecx & CDate(strFec(j)).Day & " -"
                                                Next
                                                strFecx = strFecx.TrimEnd("-") & " de " & MonthName(strMes)
                                                If dtFer.Rows.Count > 0 Then
                                                    For df As Integer = 0 To dtFer.Rows.Count - 1
                                                        If dtFer.Rows(df).Item("es_feriado") Then
                                                            If CDate(strFec(0)) > CDate(dtFer.Rows(df).Item("ord_fechas")) Then
                                                                If i = (dtSesx.Rows.Count - 1) Then
                                                                    If lb_session Then
                                                                        cellTable4.AddCell(fc_CeldaTexto(dtSesx.Rows(i).Item("nombre_ses").ToString.Replace("Sesión ", "") & " / " & strFecx, 8.0F, 0, 15, 1, 1, 0))
                                                                        lb_session = False
                                                                    End If
                                                                    cellTable4.AddCell(fc_CeldaTexto(CDate(dtFer.Rows(df).Item("ord_fechas")).Day & " de " & MonthName(CDate(dtFer.Rows(df).Item("ord_fechas")).Month), 9.0F, 0, 15, 1, 1, 0))
                                                                    cellTable4.AddCell(fc_CeldaTexto(dtFer.Rows(df).Item("evento").ToString.ToUpper, 9.0F, 0, 15, 3, 1, 1))
                                                                    dtFer.Rows(df).Item("es_feriado") = False
                                                                End If
                                                                'Exit For
                                                            End If
                                                        End If
                                                    Next
                                                End If
                                            End If
                                            If lb_session Then cellTable4.AddCell(fc_CeldaTexto(dtSesx.Rows(i).Item("nombre_ses").ToString.Replace("Sesión ", "") & " / " & strFecx, 8.0F, 0, 15, 1, 1, 0))
                                        End If
                                    Next

                                    pdfTable2.AddCell(cellTable4)

                                End If
                            Next
                            If _mod > 0 Then

                                pdfTable2.AddCell(fc_CeldaTexto("" & Environment.NewLine, 10.0F, 1, 0, 1, 1, 1))

                                cellTable4 = New iTextSharp.text.pdf.PdfPTable(4)
                                cellTable4.WidthPercentage = 100.0F
                                cellTable4.SetWidths(New Single() {20.0F, 35.0F, 30.0F, 15.0F})
                                cellTable4.DefaultCell.Border = 0

                                For _sobra As Integer = 0 To (_mod - 1)
                                    lb_session = True
                                    strFecx = ""
                                    If _sobra = 0 Then
                                        If dtSesx.Rows(_sobra + (_round * _xrow)).Item("fecha_ses").ToString = "-" Then
                                            strFecx = "-"
                                        Else
                                            strFec = dtSesx.Rows(_sobra + (_round * _xrow)).Item("fecha_ses").ToString.Split(",")
                                            For j = 0 To strFec.Length - 1
                                                strMes = CDate(strFec(j)).Month
                                                strFecx = strFecx & CDate(strFec(j)).Day & " -"
                                            Next
                                            strFecx = strFecx.TrimEnd("-") & " de " & MonthName(strMes)
                                            If dtFer.Rows.Count > 0 Then
                                                For df As Integer = 0 To dtFer.Rows.Count - 1
                                                    If dtFer.Rows(df).Item("es_feriado") Then
                                                        If CDate(strFec(0)) > CDate(dtFer.Rows(df).Item("ord_fechas")) Then
                                                            cellTable4.AddCell(fc_CeldaTexto(CDate(dtFer.Rows(df).Item("ord_fechas")).Day & " de " & MonthName(CDate(dtFer.Rows(df).Item("ord_fechas")).Month), 9.0F, 0, 15, 1, 1, 0))
                                                            cellTable4.AddCell(fc_CeldaTexto(dtFer.Rows(df).Item("evento").ToString.ToUpper, 9.0F, 0, 15, 3, 1, 1))
                                                            dtFer.Rows(df).Item("es_feriado") = False
                                                            'Exit For
                                                        End If
                                                    End If
                                                Next
                                            End If
                                        End If
                                        cellTable4.AddCell(fc_CeldaTexto(dtSesx.Rows(_sobra + (_round * _xrow)).Item("nombre_ses").ToString.Replace("Sesión ", "") & " / " & strFecx, 8.0F, 0, 15, 1, 1, 0))
                                        cellTable4.AddCell(fc_CeldaTexto(strCony, 7.0F, 0, 15, 1, _mod, 0))
                                        cellTable4.AddCell(fc_CeldaTexto(strActx, 8.0F, 0, 15, 1, _mod, 0))
                                        cellTable4.AddCell(fc_CeldaTexto(strEvax, 8.0F, 0, 15, 1, _mod, 0))
                                    Else
                                        If dtSesx.Rows(_sobra + (_round * 25)).Item("fecha_ses").ToString = "-" Then
                                            strFecx = "-"
                                        Else
                                            strFec = dtSesx.Rows(_sobra + (_round * 25)).Item("fecha_ses").ToString.Split(",")
                                            For j = 0 To strFec.Length - 1
                                                strMes = CDate(strFec(j)).Month
                                                strFecx = strFecx & CDate(strFec(j)).Day & " -"
                                            Next
                                            strFecx = strFecx.TrimEnd("-") & " de " & MonthName(strMes)
                                            If dtFer.Rows.Count > 0 Then
                                                For df As Integer = 0 To dtFer.Rows.Count - 1
                                                    If dtFer.Rows(df).Item("es_feriado") Then
                                                        If CDate(strFec(0)) > CDate(dtFer.Rows(df).Item("ord_fechas")) Then
                                                            If i = (dtSesx.Rows.Count - 1) Then
                                                                If lb_session Then
                                                                    cellTable4.AddCell(fc_CeldaTexto(dtSesx.Rows(i).Item("nombre_ses").ToString.Replace("Sesión ", "") & " / " & strFecx, 8.0F, 0, 15, 1, 1, 0))
                                                                    lb_session = False
                                                                End If
                                                                cellTable4.AddCell(fc_CeldaTexto(CDate(dtFer.Rows(df).Item("ord_fechas")).Day & " de " & MonthName(CDate(dtFer.Rows(df).Item("ord_fechas")).Month), 9.0F, 0, 15, 1, 1, 0))
                                                                cellTable4.AddCell(fc_CeldaTexto(dtFer.Rows(df).Item("evento").ToString.ToUpper, 9.0F, 0, 15, 3, 1, 1))
                                                                dtFer.Rows(df).Item("es_feriado") = False
                                                            End If
                                                            'Exit For
                                                        End If
                                                    End If
                                                Next
                                            End If
                                        End If
                                        If lb_session Then cellTable4.AddCell(fc_CeldaTexto(dtSesx.Rows(_sobra + (_round * _xrow)).Item("nombre_ses").ToString.Replace("Sesión ", "") & " / " & strFecx, 8.0F, 0, 15, 1, 1, 0))
                                    End If
                                Next
                                pdfTable2.AddCell(cellTable4)
                            End If
                            ' =========================================================================================================================
                        Else

                            Dim lb_session As Boolean
                            For i = 0 To (dtSesx.Rows.Count - 1)
                                lb_session = True
                                strFecx = ""

                                If i = 0 Then
                                    If dtSesx.Rows(i).Item("fecha_ses").ToString = "-" Then
                                        strFecx = "-"
                                    Else
                                        strFec = dtSesx.Rows(i).Item("fecha_ses").ToString.Split(",")
                                        For j = 0 To strFec.Length - 1
                                            strMes = CDate(strFec(j)).Month
                                            strFecx = strFecx & CDate(strFec(j)).Day & " -"
                                        Next
                                        strFecx = strFecx.TrimEnd("-") & " de " & MonthName(strMes)
                                        If dtFer.Rows.Count > 0 Then
                                            For df As Integer = 0 To dtFer.Rows.Count - 1
                                                If dtFer.Rows(df).Item("es_feriado") Then
                                                    If CDate(strFec(0)) > CDate(dtFer.Rows(df).Item("ord_fechas")) Then
                                                        cellTable4.AddCell(fc_CeldaTexto(CDate(dtFer.Rows(df).Item("ord_fechas")).Day & " de " & MonthName(CDate(dtFer.Rows(df).Item("ord_fechas")).Month), 9.0F, 0, 15, 1, 1, 0))
                                                        cellTable4.AddCell(fc_CeldaTexto(dtFer.Rows(df).Item("evento").ToString.ToUpper, 9.0F, 0, 15, 3, 1, 1))
                                                        dtFer.Rows(df).Item("es_feriado") = False
                                                        'Exit For
                                                    End If
                                                End If
                                            Next
                                        End If
                                    End If
                                    'cellTable4.AddCell(fc_CeldaTexto(dtSesx.Rows(i).Item("orden_ses").ToString & " / " & strFecx, 9.0F, 0, 15, 1, 1, 0))
                                    cellTable4.AddCell(fc_CeldaTexto(dtSesx.Rows(i).Item("nombre_ses").ToString.Replace("Sesión ", "") & " / " & strFecx, 8.0F, 0, 15, 1, 1, 0))
                                    cellTable4.AddCell(fc_CeldaTexto(strCony, 7.0F, 0, 15, 1, dtSesx.Rows.Count, 0))
                                    cellTable4.AddCell(fc_CeldaTexto(strActx, 8.0F, 0, 15, 1, dtSesx.Rows.Count, 0))
                                    cellTable4.AddCell(fc_CeldaTexto(strEvax, 8.0F, 0, 15, 1, dtSesx.Rows.Count, 0))
                                Else
                                    If dtSesx.Rows(i).Item("fecha_ses").ToString = "-" Then
                                        strFecx = "-"
                                    Else
                                        strFec = dtSesx.Rows(i).Item("fecha_ses").ToString.Split(",")
                                        For j = 0 To strFec.Length - 1
                                            strMes = CDate(strFec(j)).Month
                                            strFecx = strFecx & CDate(strFec(j)).Day & " -"
                                        Next
                                        strFecx = strFecx.TrimEnd("-") & " de " & MonthName(strMes)
                                        If dtFer.Rows.Count > 0 Then
                                            For df As Integer = 0 To dtFer.Rows.Count - 1
                                                If dtFer.Rows(df).Item("es_feriado") Then
                                                    If CDate(strFec(0)) > CDate(dtFer.Rows(df).Item("ord_fechas")) Then
                                                        If i = (dtSesx.Rows.Count - 1) Then
                                                            If lb_session Then
                                                                cellTable4.AddCell(fc_CeldaTexto(dtSesx.Rows(i).Item("nombre_ses").ToString.Replace("Sesión ", "") & " / " & strFecx, 8.0F, 0, 15, 1, 1, 0))
                                                                lb_session = False
                                                            End If
                                                            cellTable4.AddCell(fc_CeldaTexto(CDate(dtFer.Rows(df).Item("ord_fechas")).Day & " de " & MonthName(CDate(dtFer.Rows(df).Item("ord_fechas")).Month), 9.0F, 0, 15, 1, 1, 0))
                                                            cellTable4.AddCell(fc_CeldaTexto(dtFer.Rows(df).Item("evento").ToString.ToUpper, 9.0F, 0, 15, 3, 1, 1))
                                                            dtFer.Rows(df).Item("es_feriado") = False
                                                        End If
                                                        'Exit For
                                                    End If
                                                End If
                                            Next
                                        End If
                                    End If
                                    'cellTable4.AddCell(fc_CeldaTexto(dtSesx.Rows(i).Item("orden_ses").ToString & " / " & strFecx, 9.0F, 0, 15, 1, 1, 0))
                                    If lb_session Then cellTable4.AddCell(fc_CeldaTexto(dtSesx.Rows(i).Item("nombre_ses").ToString.Replace("Sesión ", "") & " / " & strFecx, 8.0F, 0, 15, 1, 1, 0))
                                End If
                            Next
                        End If

                    End If
                Next

                If Not _ngrupo Then
                    pdfTable2.AddCell(cellTable4)
                End If


            Next


            errorGrupoSesion = "si" 'Por Luis Q.T. | 28AGO2020 | Controlar error 

            pdfDoc.Add(pdfTable2)

            pdfDoc.Close()

        Catch ex As Exception

            If errorGrupoSesion.Equals("si") Then
                Throw New Exception("Se encontraron sesiones que comparten actividades y contenidos pero no están agrupadas. Por favor, proceda a hacerlo en el diseño de asignatura antes de publicar el silabo.")
            Else
                Throw ex
            End If

            'Return ex.Message.ToString
        End Try
    End Sub

    Public Sub mt_GenerarSilabo2(ByVal codigo_cup As Integer, ByVal sourceIcon As String, ByVal memory As System.IO.Stream, Optional ByVal vista As Boolean = False)
        Dim obj As New ClsConectarDatos
        Dim x, y, z, i, j As Integer
        Dim dtDis, dtCup, dtHor, dtFer As New Data.DataTable
        Dim codigo_dis, codigo_pes, codigo_cur, codigo_cac As Integer
        Dim transversal As Boolean
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
        Try

            obj.AbrirConexion()
            dtCup = obj.TraerDataTable("CursoProgramado_Listar2", codigo_cup)
            If dtCup.Rows.Count > 0 Then
                codigo_cac = CInt(dtCup.Rows(0).Item("codigo_Cac"))
                codigo_pes = CInt(dtCup.Rows(0).Item("codigo_Pes"))
                codigo_cur = CInt(dtCup.Rows(0).Item("codigo_Cur"))
                transversal = IIf(dtCup.Rows(0).Item("transversal_pcu") = 0, False, True)
            End If
            If transversal Then
                dtDis = obj.TraerDataTable("DiseñoAsignatura_Listar", "", -1, -2, codigo_cur, codigo_cac, codigo_cup)
            Else
                dtDis = obj.TraerDataTable("DiseñoAsignatura_Listar", "", -1, codigo_pes, codigo_cur, codigo_cac, -1)
            End If

            dtHor = obj.TraerDataTable("COM_ListarHorarioDocente", codigo_cup, codigo_cac, "01/01/1900")
            obj.CerrarConexion()

            If dtDis.Rows.Count > 0 Then
                codigo_dis = CInt(dtDis.Rows(0).Item("codigo_dis"))
            Else
                Throw New Exception("¡ No existe Diseño de Asignatura o Coordinador para este Curso !" & codigo_pes)
            End If

            If dtHor.Rows.Count > 0 Then
                dtFer = New Data.DataView(dtHor, "es_feriado = 1", "", Data.DataViewRowState.CurrentRows).ToTable
            End If

            Dim pdfDoc As New iTextSharp.text.Document(iTextSharp.text.PageSize.A4)
            Dim pdfWrite As iTextSharp.text.pdf.PdfWriter = iTextSharp.text.pdf.PdfWriter.GetInstance(pdfDoc, memory)

            pdfDoc.Open()
            'pdfDoc.AddDocListener(pdfWrite)

            If vista Then mt_AddWaterMark(pdfWrite, "BORRADOR")

            Dim pdfTable As iTextSharp.text.pdf.PdfPTable = New iTextSharp.text.pdf.PdfPTable(1)
            pdfTable.WidthPercentage = 98.0F
            pdfTable.DefaultCell.Border = 0

            ' 0: Cabecera de Silabo ----------------------------------------------------------------------------------

            Dim cellTable0 As iTextSharp.text.pdf.PdfPTable = New iTextSharp.text.pdf.PdfPTable(2)
            cellTable0.WidthPercentage = 100.0F
            cellTable0.SetWidths(New Single() {60.0F, 40.0F})
            cellTable0.DefaultCell.Border = 0

            Dim srcIcon As String = sourceIcon
            Dim usatIcon As iTextSharp.text.Image = iTextSharp.text.Image.GetInstance(srcIcon)
            usatIcon.ScalePercent(60.0F)
            usatIcon.Alignment = iTextSharp.text.Element.ALIGN_LEFT

            Dim cellIcon As iTextSharp.text.pdf.PdfPCell = New iTextSharp.text.pdf.PdfPCell(usatIcon)
            cellIcon.HorizontalAlignment = 0
            cellIcon.VerticalAlignment = 2
            cellIcon.Border = 0

            cellTable0.AddCell(cellIcon)
            cellTable0.AddCell(fc_CeldaTexto("SISTEMA DE GESTIÓN DE LA CALIDAD" & Environment.NewLine & " " & Environment.NewLine & _
                               "CÓDIGO: USAT-PM0401-D-01" & Environment.NewLine & " " & Environment.NewLine & _
                               "VERSIÓN: 04", 8.0F, 1, 0, 1, 1, 2))

            pdfTable.AddCell(cellTable0)

            pdfTable.AddCell(fc_CeldaTexto("" & Environment.NewLine, 10.0F, 1, 0, 1, 1, 1))

            pdfTable.AddCell(fc_CeldaTexto("FACULTAD DE " & dtDis.Rows(0).Item("nombre_Fac").ToString.ToUpper, 10.0F, 1, 0, 1, 1, 1))
            pdfTable.AddCell(fc_CeldaTexto("PROGRAMA DE ESTUDIOS DE " & dtDis.Rows(0).Item("nombreOficial_cpf").ToString.ToUpper, 10.0F, 1, 0, 1, 1, 1))
            pdfTable.AddCell(fc_CeldaTexto("SÍLABO DE " & dtDis.Rows(0).Item("nombre_Cur").ToString.ToUpper, 10.0F, 1, 0, 1, 1, 1))

            pdfTable.AddCell(fc_CeldaTexto("" & Environment.NewLine, 10.0F, 1, 0, 1, 1, 1))

            ' 1: Datos Informativos ----------------------------------------------------------------------------------

            Dim cellTable1 As iTextSharp.text.pdf.PdfPTable = New iTextSharp.text.pdf.PdfPTable(2)
            cellTable1.WidthPercentage = 100.0F
            cellTable1.SetWidths(New Single() {6.0F, 94.0F})
            cellTable1.DefaultCell.Border = 0

            cellTable1.AddCell(fc_CeldaTexto("I. ", 10.0F, 1, 0, 1, 1, 0, -1, 10))
            cellTable1.AddCell(fc_CeldaTexto("DATOS INFORMATIVOS", 10.0F, 1, 0, 1, 1, 0, -1, 10))

            cellTable1.AddCell(fc_CeldaTexto("" & Environment.NewLine, 10.0F, 1, 0, 1, 1, 1))

            Dim cellTable11 As iTextSharp.text.pdf.PdfPTable = New iTextSharp.text.pdf.PdfPTable(5)
            cellTable11.WidthPercentage = 100.0F
            cellTable11.SetWidths(New Single() {40.0F, 8.5F, 24.0F, 7.5F, 20.0F})

            cellTable11.AddCell(fc_CeldaTexto("1.1 Asignatura:", 10.0F, 1, 15, 1, 1, 0, 5, 8, "WhiteSmoke"))
            cellTable11.AddCell(fc_CeldaTexto(dtCup.Rows(0).Item("nombre_Cur").ToString, 10.0F, 0, 15, 4, 1, 0, -1, 8))
            cellTable11.AddCell(fc_CeldaTexto("1.2 Código:", 10.0F, 1, 15, 1, 1, 0, 5, 8, "WhiteSmoke"))
            cellTable11.AddCell(fc_CeldaTexto(dtCup.Rows(0).Item("identificador_Cur").ToString.ToUpper, 10.0F, 0, 15, 4, 1, 0, -1, 8))
            cellTable11.AddCell(fc_CeldaTexto("1.3 Ciclo del plan de estudios:", 10.0F, 1, 15, 1, 1, 0, 5, 8, "WhiteSmoke"))
            cellTable11.AddCell(fc_CeldaTexto(dtCup.Rows(0).Item("ciclo_Cur").ToString.ToUpper, 10.0F, 0, 15, 4, 1, 0, -1, 8))
            cellTable11.AddCell(fc_CeldaTexto("1.4 Créditos:", 10.0F, 1, 15, 1, 1, 0, 5, 8, "WhiteSmoke"))
            cellTable11.AddCell(fc_CeldaTexto(dtCup.Rows(0).Item("creditos_Cur"), 10.0F, 0, 15, 4, 1, 0, -1, 8))
            cellTable11.AddCell(fc_CeldaTexto("1.5 Tipo de asignatura:", 10.0F, 1, 15, 1, 1, 0, 5, 8, "WhiteSmoke"))
            cellTable11.AddCell(fc_CeldaTexto(IIf(dtCup.Rows(0).Item("electivo_Cur") = 0, "( X )", "(   )"), 10.0F, 0, 15, 1, 1, 1, -1, 8))
            cellTable11.AddCell(fc_CeldaTexto("Obligatorio", 10.0F, 0, 15, 1, 1, 0, -1, 8))
            cellTable11.AddCell(fc_CeldaTexto(IIf(dtCup.Rows(0).Item("electivo_Cur") = 0, "(   )", "( X )"), 10.0F, 0, 15, 1, 1, 1, -1, 8))
            cellTable11.AddCell(fc_CeldaTexto("Electivo", 10.0F, 0, 15, 1, 1, 0, -1, 8))
            cellTable11.AddCell(fc_CeldaTexto("1.6 Prerrequisito:", 10.0F, 1, 15, 1, 1, 0, 5, 8, "WhiteSmoke"))
            cellTable11.AddCell(fc_CeldaTexto(dtCup.Rows(0).Item("prerequisito").ToString.ToUpper, 10.0F, 0, 15, 4, 1, 0, -1, 8))
            cellTable11.AddCell(fc_CeldaTexto("1.7 Número de horas semanales:", 10.0F, 1, 15, 1, 3, 0, 5, 8, "WhiteSmoke"))
            cellTable11.AddCell(fc_CeldaTexto("N° de horas teóricas:", 10.0F, 0, 15, 3, 1, 1, -1, 8))
            cellTable11.AddCell(fc_CeldaTexto(dtCup.Rows(0).Item("horasTeo_Cur"), 10.0F, 0, 15, 1, 1, 1, -1, 8))
            cellTable11.AddCell(fc_CeldaTexto("N° de horas prácticas:", 10.0F, 0, 15, 3, 1, 1, -1, 8))
            cellTable11.AddCell(fc_CeldaTexto(dtCup.Rows(0).Item("horasPra_Cur"), 10.0F, 0, 15, 1, 1, 1, -1, 8))
            cellTable11.AddCell(fc_CeldaTexto("N° de horas totales:", 10.0F, 0, 15, 3, 1, 1, -1, 8))
            cellTable11.AddCell(fc_CeldaTexto(dtCup.Rows(0).Item("totalHoras_Cur"), 10.0F, 0, 15, 1, 1, 1, -1, 8))
            cellTable11.AddCell(fc_CeldaTexto("1.8 Duración:", 10.0F, 1, 15, 1, 1, 0, 5, 8, "WhiteSmoke"))
            Dim fecInicio As Date = CDate(dtCup.Rows(0).Item("fechainicio_Cup").ToString)
            Dim fecFin As Date = CDate(dtCup.Rows(0).Item("fechafin_Cup").ToString)
            cellTable11.AddCell(fc_CeldaTexto("Del (" & String.Format("{0:00}", fecInicio.Day) & "/" & String.Format("{0:00}", fecInicio.Month) & ") al (" & fecFin.Date & ")", 10.0F, 0, 15, 4, 1, 0, -1, 8))
            cellTable11.AddCell(fc_CeldaTexto("1.9 Semestre académico:", 10.0F, 1, 15, 1, 1, 0, 5, 8, "WhiteSmoke"))
            cellTable11.AddCell(fc_CeldaTexto(dtCup.Rows(0).Item("descripcion_Cac"), 10.0F, 0, 15, 4, 1, 0, -1, 8))
            cellTable11.AddCell(fc_CeldaTexto("1.10 Grupo Horario:", 10.0F, 1, 15, 1, 1, 0, 5, 8, "WhiteSmoke"))
            cellTable11.AddCell(fc_CeldaTexto(dtCup.Rows(0).Item("grupoHor_Cup"), 10.0F, 0, 15, 4, 1, 0, -1, 8))

            cellTable11.AddCell(fc_CeldaTexto("1.11 Docente coordinador:", 10.0F, 1, 15, 1, 1, 0, 5, 10, "WhiteSmoke"))

            Dim dtCoo As Data.DataTable
            obj.AbrirConexion()
            dtCoo = obj.TraerDataTable("DocenteCursoProgramado_Listar", "CO", codigo_cup)
            obj.CerrarConexion()

            If dtCoo.Rows.Count > 0 Then
                Dim _textDocenteCoordinador As New iTextSharp.text.Phrase
                _textDocenteCoordinador.Add(fc_textFrase(dtCoo.Rows(0).Item("docente").ToString & Environment.NewLine, 5, 10.0F, 0, iTextSharp.text.BaseColor.BLACK))
                _textDocenteCoordinador.Add(fc_textFrase(dtCoo.Rows(0).Item("email_Per").ToString & Environment.NewLine, 5, 10.0F, 4, iTextSharp.text.BaseColor.BLUE))
                cellTable11.AddCell(fc_CeldaTexto(_textDocenteCoordinador, 15, 4, 1, 0, 5, 8))
            End If


            Dim dtDoc As Data.DataTable
            obj.AbrirConexion()
            dtDoc = obj.TraerDataTable("DocenteCursoProgramado_Listar", "", codigo_cup)
            obj.CerrarConexion()

            'If dtDoc.Rows.Count > 0 Then
            '    cellTable11.AddCell(fc_CeldaTexto("1.12 Docente(s):", 10.0F, 1, 15, 1, 1, 0, 5, 8, "WhiteSmoke"))
            '    Dim _textDocente As New iTextSharp.text.Phrase
            '    For x = 0 To dtDoc.Rows.Count - 1
            '        _textDocente.Add(fc_textFrase(dtDoc.Rows(x).Item("docente").ToString & Environment.NewLine, 5, 10.0F, 0, iTextSharp.text.BaseColor.BLACK))
            '        _textDocente.Add(fc_textFrase(dtDoc.Rows(x).Item("email_Per").ToString & Environment.NewLine, 5, 10.0F, 4, iTextSharp.text.BaseColor.BLUE))
            '    Next
            '    cellTable11.AddCell(fc_CeldaTexto(_textDocente, 15, 4, 1, 0, 5, 8))
            'Else
            '    cellTable11.AddCell(fc_CeldaTexto("1.12 Docente(s):", 10.0F, 1, 15, 1, 1, 0, 5, 8, "WhiteSmoke"))
            '    cellTable11.AddCell(fc_CeldaTexto(" No Definido ", 10.0F, 0, 15, 4, 1, 0, 5, 8))
            'End If

            ' 20200327-ENevado ===================================================================================================
            If dtDoc.Rows.Count = 0 Then
                cellTable11.AddCell(fc_CeldaTexto("1.12 Docente(s):", 10.0F, 1, 15, 1, 1, 0, 5, 8, "WhiteSmoke"))
                cellTable11.AddCell(fc_CeldaTexto(" No Definido ", 10.0F, 0, 15, 4, 1, 0, 5, 8))
                ' 13NOV2020 - JQuepuy =====================================================================================================
                cellTable11.AddCell(fc_CeldaTexto("1.13 Modalidad:", 10.0F, 1, 15, 1, 1, 0, 5, 8, "WhiteSmoke"))
                cellTable11.AddCell(fc_CeldaTexto("Educación Remota de Emergencia (ERE)" & Environment.NewLine & "(RCD 039-2020-SUNEDU / RVM 085-2020-MINEDU)", 10.0F, 0, 15, 4, 1, 0, 5, 8))
                ' =========================================================================================================================
                cellTable1.AddCell(cellTable11)
                pdfTable.AddCell(cellTable1)

            ElseIf dtDoc.Rows.Count < 4 Then
                cellTable11.AddCell(fc_CeldaTexto("1.12 Docente(s):", 10.0F, 1, 15, 1, 1, 0, 5, 8, "WhiteSmoke"))
                Dim _textDocente As New iTextSharp.text.Phrase
                For x = 0 To dtDoc.Rows.Count - 1
                    _textDocente.Add(fc_textFrase(dtDoc.Rows(x).Item("docente").ToString & Environment.NewLine, 5, 10.0F, 0, iTextSharp.text.BaseColor.BLACK))
                    _textDocente.Add(fc_textFrase(dtDoc.Rows(x).Item("email_Per").ToString & Environment.NewLine, 5, 10.0F, 4, iTextSharp.text.BaseColor.BLUE))
                Next
                cellTable11.AddCell(fc_CeldaTexto(_textDocente, 15, 4, 1, 0, 5, 8))
                ' 13NOV2020 - JQuepuy =====================================================================================================
                cellTable11.AddCell(fc_CeldaTexto("1.13 Modalidad:", 10.0F, 1, 15, 1, 1, 0, 5, 8, "WhiteSmoke"))
                cellTable11.AddCell(fc_CeldaTexto("Educación Remota de Emergencia (ERE)" & Environment.NewLine & "(RCD 039-2020-SUNEDU / RVM 085-2020-MINEDU)", 10.0F, 0, 15, 4, 1, 0, 5, 8))
                ' =========================================================================================================================
                cellTable1.AddCell(cellTable11)
                pdfTable.AddCell(cellTable1)

            Else
                cellTable1.AddCell(cellTable11)
                pdfTable.AddCell(cellTable1)

                Dim cellTablex As iTextSharp.text.pdf.PdfPTable = New iTextSharp.text.pdf.PdfPTable(2)
                cellTablex.WidthPercentage = 100.0F
                cellTablex.SetWidths(New Single() {6.0F, 94.0F})
                cellTablex.DefaultCell.Border = 0
                cellTablex.AddCell(fc_CeldaTexto("" & Environment.NewLine, 10.0F, 1, 0, 1, 1, 1))

                Dim cellTablexx As iTextSharp.text.pdf.PdfPTable = New iTextSharp.text.pdf.PdfPTable(5)
                cellTablexx.WidthPercentage = 100.0F
                cellTablexx.SetWidths(New Single() {40.0F, 8.5F, 24.0F, 7.5F, 20.0F})

                cellTablexx.AddCell(fc_CeldaTexto("1.12 Docente(s):", 10.0F, 1, 15, 1, 1, 0, 5, 8, "WhiteSmoke"))
                Dim _textDocente As New iTextSharp.text.Phrase
                For x = 0 To dtDoc.Rows.Count - 1
                    _textDocente.Add(fc_textFrase(dtDoc.Rows(x).Item("docente").ToString & Environment.NewLine, 5, 10.0F, 0, iTextSharp.text.BaseColor.BLACK))
                    '_textDocente.Add(fc_textFrase(" " & Environment.NewLine, 5, 5.0F, 0, iTextSharp.text.BaseColor.BLACK))
                    _textDocente.Add(fc_textFrase(dtDoc.Rows(x).Item("email_Per").ToString & Environment.NewLine, 5, 10.0F, 4, iTextSharp.text.BaseColor.BLUE))
                    _textDocente.Add(fc_textFrase(" " & Environment.NewLine, 5, 5.0F, 0, iTextSharp.text.BaseColor.BLACK))
                Next
                cellTablexx.AddCell(fc_CeldaTexto(_textDocente, 15, 4, 1, 0, 5, 8))
                ' 13NOV2020 - JQuepuy =====================================================================================================
                cellTablexx.AddCell(fc_CeldaTexto("1.13 Modalidad:", 10.0F, 1, 15, 1, 1, 0, 5, 8, "WhiteSmoke"))
                cellTablexx.AddCell(fc_CeldaTexto("Educación Remota de Emergencia (ERE)" & Environment.NewLine & "(RCD 039-2020-SUNEDU / RVM 085-2020-MINEDU)", 10.0F, 0, 15, 4, 1, 0, 5, 8))
                ' =========================================================================================================================
                cellTablex.AddCell(cellTablexx)
                pdfTable.AddCell(cellTablex)

            End If
            ' =========================================================================================================================

            'cellTable1.AddCell(cellTable11)
            'pdfTable.AddCell(cellTable1)

            pdfDoc.Add(pdfTable)

            pdfDoc.NewPage()

            If vista Then mt_AddWaterMark(pdfWrite, "BORRADOR")

            Dim pdfTable2 As iTextSharp.text.pdf.PdfPTable = New iTextSharp.text.pdf.PdfPTable(2)
            pdfTable2.WidthPercentage = 98.0F
            pdfTable2.SetWidths(New Single() {6.0F, 94.0F})
            pdfTable2.DefaultCell.Border = 0

            ' 2: Sumilla ------------------------------------------------------------------------------------

            Dim dtSum As Data.DataTable
            obj.AbrirConexion()
            If transversal Then
                dtSum = obj.TraerDataTable("PlanCursoSumilla_Listar", "PC", -1, -2, codigo_cur)
            Else
                dtSum = obj.TraerDataTable("PlanCursoSumilla_Listar", "PC", -1, codigo_pes, codigo_cur)
            End If
            obj.CerrarConexion()

            If dtSum.Rows.Count = 0 Then
                Throw New Exception("¡ No se ha registrado Sumilla para la Asignatura !")
            Else
                If dtSum.Rows(0).Item("descripcion_sum").ToString.Trim = "" Then
                    Throw New Exception("¡ No se ha registrado Sumilla para la Asignatura !")
                End If
            End If

            pdfTable2.AddCell(fc_CeldaTexto("II. ", 10.0F, 1, 0, 1, 1, 0, -1, 10))
            pdfTable2.AddCell(fc_CeldaTexto("SUMILLA", 10.0F, 1, 0, 1, 1, 0, -1, 10))

            pdfTable2.AddCell(fc_CeldaTexto("" & Environment.NewLine, 10.0F, 1, 0, 1, 1, 0))
            pdfTable2.AddCell(fc_CeldaTexto(dtSum.Rows(0).Item("descripcion_sum").ToString, 10.0F, 0, 15, 1, 1, 0))

            pdfTable2.AddCell(fc_CeldaTexto("" & Environment.NewLine, 10.0F, 1, 0, 2, 1, 0))

            ' 3: Competencias -------------------------------------------------------------------------------------------------------

            Dim dtCom As Data.DataTable
            obj.AbrirConexion()
            dtCom = obj.TraerDataTable("PerfilEgresoCurso_Listar", -1, -1, codigo_pes, codigo_cur, -1)
            obj.CerrarConexion()

            If dtCom.Rows.Count = 0 Then Throw New Exception("¡ No se ha alineado una competencia para la Asignatura !")

            Dim strCom As String = ""
            For _com As Integer = 0 To dtCom.Rows.Count - 1
                strCom = strCom & " - " & dtCom.Rows(_com).Item("nombre_com").ToString & Environment.NewLine '& " " & Environment.NewLine
            Next

            pdfTable2.AddCell(fc_CeldaTexto("III. ", 10.0F, 1, 0, 1, 1, 0, -1, 10))
            pdfTable2.AddCell(fc_CeldaTexto("COMPETENCIA(S)", 10.0F, 1, 0, 1, 1, 0, -1, 10))
            pdfTable2.AddCell(fc_CeldaTexto("" & Environment.NewLine, 10.0F, 1, 0, 1, 4, 1))
            pdfTable2.AddCell(fc_CeldaTexto("3.1 Competencia(s) de perfil de egreso", 10.0F, 1, 15, 1, 1, 0, -1, 6, "WhiteSmoke"))
            pdfTable2.AddCell(fc_CeldaTexto("La asignatura " & dtDis.Rows(0).Item("nombre_Cur").ToString & _
                                            ", que corresponde al área de estudios " & dtCom.Rows(0).Item("nombre_cat").ToString & _
                                            ", contribuye al logro del perfil de egreso, específicamente a la(s) competencia(s): " & Environment.NewLine & _
                                            strCom, 10.0F, 0, 15, 1, 1, 0))
            pdfTable2.AddCell(fc_CeldaTexto("3.2 Logro(s) de la asignatura", 10.0F, 1, 15, 1, 1, 0, -1, 6, "WhiteSmoke"))
            pdfTable2.AddCell(fc_CeldaTexto(dtSum.Rows(0).Item("competencia_sum").ToString, 10.0F, 0, 15, 1, 1, 0))

            pdfTable2.AddCell(fc_CeldaTexto("" & Environment.NewLine, 10.0F, 1, 0, 2, 1, 0))

            ' 4: Unidades didactidas -------------------------------------------------------------------------------------------------

            Dim dtUni, dtRes, dtInd, dtEvi, dtIns, dtCon As Data.DataTable
            obj.AbrirConexion()
            dtUni = obj.TraerDataTable("UnidadAsignatura_Listar", "", -1, codigo_dis, "")
            dtRes = obj.TraerDataTable("ResultadoAprendizaje_Listar", "RA", -1, codigo_dis, "")
            dtInd = obj.TraerDataTable("IndicadorAprendizaje_listar", "IA", -1, codigo_dis, "")
            dtEvi = obj.TraerDataTable("IndicadorEvidencia_Listar", "IE", -1, codigo_dis, -1)
            dtIns = obj.TraerDataTable("EvidenciaInstrumento_Listar", "IE", -1, codigo_dis, -1)
            dtCon = obj.TraerDataTable("ContenidoAsignatura_Listar", "", -1, codigo_dis)
            obj.CerrarConexion()

            pdfTable2.AddCell(fc_CeldaTexto("IV. ", 10.0F, 1, 0, 1, 1, 0))
            pdfTable2.AddCell(fc_CeldaTexto("UNIDADES DIDÁCTICAS", 10.0F, 1, 0, 1, 1, 0))


            Dim rowsInd As Integer = 0
            Dim strCon As String = "", strInd As String = ""
            Dim _nroIns As Integer = 0, _nroRes As Integer = 0, _iRes As Integer = 0, _iInd As Integer = 0, _xInd As Integer = 0
            Dim lb_Contenido As Boolean
            For x = 0 To (dtUni.Rows.Count - 1)

                pdfTable2.AddCell(fc_CeldaTexto("" & Environment.NewLine, 10.0F, 1, 0, 1, 1, 1))

                Dim cellTable2 As iTextSharp.text.pdf.PdfPTable = New iTextSharp.text.pdf.PdfPTable(6)
                cellTable2.WidthPercentage = 100.0F
                cellTable2.SetWidths(New Single() {20.0F, 7.0F, 20.0F, 7.0F, 15.0F, 31.0F})
                cellTable2.DefaultCell.Border = 0
                cellTable2.SpacingBefore = 0.0F
                cellTable2.SpacingAfter = 0.0F

                cellTable2.AddCell(fc_CeldaTexto("Unidad didáctica N° " & String.Format("{0:00}", (x + 1)) & ": " & dtUni.Rows(x).Item("descripcion_uni").ToString, 9.0F, 1, 15, 6, 1, 1, 5, 6, "WhiteSmoke"))

                Dim dtResx, dtIndx, dtEvix, dtInsx, dtConx, dtCant As Data.DataTable
                dtResx = New Data.DataView(dtRes, "codigo_uni = " & dtUni.Rows(x).Item("codigo_uni"), "", Data.DataViewRowState.CurrentRows).ToTable
                dtConx = New Data.DataView(dtCon, "codigo_uni = " & dtUni.Rows(x).Item("codigo_uni"), "", Data.DataViewRowState.CurrentRows).ToTable

                dtCant = New Data.DataView(dtIns, "codigo_uni = " & dtUni.Rows(x).Item("codigo_uni"), "", Data.DataViewRowState.CurrentRows).ToTable
                _nroIns = IIf(dtCant.Rows.Count = 0, 1, dtCant.Rows.Count)
                _nroRes = dtResx.Rows.Count
                lb_Contenido = True

                For xRes As Integer = 0 To dtResx.Rows.Count - 1
                    _iRes += 1
                    dtIndx = New Data.DataView(dtInd, "codigo_res = " & dtResx.Rows(xRes).Item("codigo_res"), "", Data.DataViewRowState.CurrentRows).ToTable
                    Dim _prom_ind As Integer
                    _prom_ind = dtResx.Rows(xRes).Item("promedio_indicador")
                    strInd = ""
                    For _ind As Integer = 0 To dtIndx.Rows.Count - 1
                        _iInd += 1
                        If _prom_ind = 2 Then
                            strInd += " IND" & _iInd & "(" & Math.Round(dtIndx.Rows(_ind).Item("peso_ind"), 2) & ") +"
                        Else
                            strInd += " IND" & _iInd & " +"
                        End If
                    Next
                    strInd = strInd.TrimEnd("+")
                    If _prom_ind <> 2 Then
                        If dtIndx.Rows.Count > 1 Then
                            strInd = " (" + strInd + ") / " & dtIndx.Rows.Count
                        End If
                    End If
                    Dim _textRes As iTextSharp.text.Phrase = New iTextSharp.text.Phrase
                    _textRes.Add(fc_textFrase("Resultado de aprendizaje N° " & String.Format("{0:00}", _iRes) & " (RA" & _iRes & "): " & Environment.NewLine, 5, 8.0F, 1, iTextSharp.text.BaseColor.BLACK))
                    _textRes.Add(fc_textFrase(dtResx.Rows(xRes).Item("descripcion_res").ToString & Environment.NewLine, 5, 8.0F, 0, iTextSharp.text.BaseColor.BLACK))
                    _textRes.Add(fc_textFrase("RA" & _iRes & " =" & strInd, 5, 8.0F, 1, iTextSharp.text.BaseColor.BLACK))
                    cellTable2.AddCell(fc_CeldaTexto(_textRes, 15, 5, 1, 1, -1, 6, "WhiteSmoke"))

                    If lb_Contenido Then cellTable2.AddCell(fc_CeldaTexto("Contenidos", 8.0F, 1, 15, 1, 1, 1, 5, 6, "WhiteSmoke"))

                    cellTable2.AddCell(fc_CeldaTexto("Indicadores", 8.0F, 1, 15, 2, 1, 1, 5, 6, "WhiteSmoke"))
                    cellTable2.AddCell(fc_CeldaTexto("Evaluación", 8.0F, 1, 15, 3, 1, 1, 5, 6, "WhiteSmoke"))

                    If lb_Contenido Then
                        strCon = ""
                        For z = 0 To (dtConx.Rows.Count - 1)
                            strCon = strCon & (x + 1) & "." & (z + 1) & " " & dtConx.Rows(z).Item("descripcion").ToString & Environment.NewLine
                        Next
                        If _nroRes = 1 Then
                            cellTable2.AddCell(fc_CeldaTexto(strCon, 7.0F, 0, 15, 1, 2 + _nroIns, 0, 5, 6))
                        Else
                            cellTable2.AddCell(fc_CeldaTexto(strCon, 7.0F, 0, 15, 1, 2 + (3 * (_nroRes - 1)) + _nroIns, 0, 5, 6))
                        End If

                        lb_Contenido = False
                    End If

                    cellTable2.AddCell(fc_CeldaTexto("Descripción", 8.0F, 1, 15, 1, 1, 1, 5, 6, "WhiteSmoke"))
                    cellTable2.AddCell(fc_CeldaTexto("Peso", 8.0F, 1, 15, 1, 1, 1, 5, 6, "WhiteSmoke"))
                    cellTable2.AddCell(fc_CeldaTexto("Evidencia", 8.0F, 1, 15, 1, 1, 1, 5, 6, "WhiteSmoke"))
                    cellTable2.AddCell(fc_CeldaTexto("Peso", 8.0F, 1, 15, 1, 1, 1, 5, 6, "WhiteSmoke"))
                    cellTable2.AddCell(fc_CeldaTexto("Instrumentos", 8.0F, 1, 15, 1, 1, 1, 5, 6, "WhiteSmoke"))

                    For xInd As Integer = 0 To dtIndx.Rows.Count - 1
                        _xInd += 1
                        Dim _can_ins As Integer = 0
                        Dim _prom_ins As Integer

                        _prom_ins = dtIndx.Rows(xInd).Item("promedio_instrumento")
                        dtEvix = New Data.DataView(dtEvi, "codigo_ind = " & dtIndx.Rows(xInd).Item("codigo_ind"), "", Data.DataViewRowState.CurrentRows).ToTable

                        If dtIndx.Rows(xInd).Item("nro_ins") = 0 Then
                            _can_ins = dtEvix.Rows.Count
                        Else
                            _can_ins = dtIndx.Rows(xInd).Item("nro_ins")
                        End If

                        cellTable2.AddCell(fc_CeldaTexto("IND" & _xInd & ": " & dtIndx.Rows(xInd).Item("descripcion_ind"), 8.0F, 0, 15, 1, _can_ins, 1, 5, 6))
                        If _prom_ind = 2 Then
                            cellTable2.AddCell(fc_CeldaTexto(Math.Round(dtIndx.Rows(xInd).Item("peso_ind") * 100, 2), 7.0F, 0, 15, 1, _can_ins, 1, 5, 6))
                        Else
                            cellTable2.AddCell(fc_CeldaTexto("Prom. Simple", 7.0F, 0, 15, 1, _can_ins, 1, 5, 6))
                        End If


                        For xEvi As Integer = 0 To dtEvix.Rows.Count - 1
                            dtInsx = New Data.DataView(dtIns, "codigo_evi = " & dtEvix.Rows(xEvi).Item("codigo_evi"), "", Data.DataViewRowState.CurrentRows).ToTable
                            cellTable2.AddCell(fc_CeldaTexto(dtEvix.Rows(xEvi).Item("descripcion_evi"), 8.0F, 0, 15, 1, dtInsx.Rows.Count, 1, 5, 6))
                            For xIns As Integer = 0 To dtInsx.Rows.Count - 1
                                If _prom_ins = 2 Then
                                    cellTable2.AddCell(fc_CeldaTexto(Math.Round(dtInsx.Rows(xIns).Item("peso_ins") * 100, 2), 7.0F, 0, 15, 1, 1, 1, 5, 6))
                                Else
                                    cellTable2.AddCell(fc_CeldaTexto("Prom. Simple", 7.0F, 0, 15, 1, 1, 1, 5, 6))
                                End If
                                cellTable2.AddCell(fc_CeldaTexto(dtInsx.Rows(xIns).Item("descripcion_ins"), 8.0F, 0, 15, 1, 1, 1, 5, 6))
                            Next

                        Next

                    Next

                Next

                pdfTable2.AddCell(cellTable2)

            Next


            pdfTable2.AddCell(fc_CeldaTexto("" & Environment.NewLine, 10.0F, 1, 0, 2, 1, 0))

            ' 5: Estrategias Didacticas -------------------------------------------------------------------------------------

            Dim dtEst As Data.DataTable
            obj.AbrirConexion()
            dtEst = obj.TraerDataTable("EstrategiaDidactica_Listar", -1, codigo_dis, "")
            obj.CerrarConexion()

            pdfTable2.AddCell(fc_CeldaTexto("V. ", 10.0F, 1, 0, 1, 1, 0))
            pdfTable2.AddCell(fc_CeldaTexto("ESTRATEGIAS DIDÁCTICAS", 10.0F, 1, 0, 1, 1, 0))
            pdfTable2.AddCell(fc_CeldaTexto("" & Environment.NewLine, 10.0F, 1, 0, 1, 1, 0))

            Dim strEst As String
            strEst = "Para el desarrollo de la asignatura se emplearán las siguientes estrategias didácticas:" & Environment.NewLine & Environment.NewLine
            For x = 0 To (dtEst.Rows.Count - 1)
                strEst = strEst & "• " & dtEst.Rows(x).Item("descripcion_dis").ToString & Environment.NewLine
            Next

            pdfTable2.AddCell(fc_CeldaTexto(strEst, 10.0F, 0, 15, 1, 1, 0))

            pdfTable2.AddCell(fc_CeldaTexto("" & Environment.NewLine, 10.0F, 1, 0, 2, 1, 0))

            ' 6: Evaluacion ---------------------------------------------------------------------------------------------------

            Dim _total_eva As Integer = 0

            pdfTable2.AddCell(fc_CeldaTexto("VI. ", 10.0F, 1, 0, 1, 1, 0))
            pdfTable2.AddCell(fc_CeldaTexto("EVALUACIÓN", 10.0F, 1, 0, 1, 1, 0))
            pdfTable2.AddCell(fc_CeldaTexto("" & Environment.NewLine, 10.0F, 1, 0, 1, 1, 1))

            Dim cellTable3 As iTextSharp.text.pdf.PdfPTable = New iTextSharp.text.pdf.PdfPTable(5)
            cellTable3.WidthPercentage = 100.0F
            cellTable3.SetWidths(New Single() {40.0F, 20.0F, 10.0F, 10.0F, 20.0F})
            cellTable3.DefaultCell.Border = 0

            cellTable3.AddCell(fc_CeldaTexto("6.1 Criterios de evaluación", 10.0F, 1, 15, 5, 1, 0, -1, 6, "WhiteSmoke"))

            Dim dtNor As Data.DataTable
            obj.AbrirConexion()
            dtNor = obj.TraerDataTable("DEA_CicloAcademicoConf_Normas_listar", "", codigo_cac)
            obj.CerrarConexion()

            Dim _textCri As iTextSharp.text.Phrase
            _textCri = New iTextSharp.text.Phrase
            _textCri.Add(fc_textFrase("La calificación para todas las asignaturas, se realizará en la escala vigesimal, es decir, de cero (00) a veinte (20). " & _
                                      "La nota aprobatoria mínima es catorce (14). " & Environment.NewLine & " " & Environment.NewLine, 5, 10.0F, 1, iTextSharp.text.BaseColor.BLACK))
            _textCri.Add(fc_textFrase("La evaluación será formativa y sumativa, se aplicará evaluaciones de entrada y de salida, considerando las evidencias " & _
                                             "(por ejemplo informes, exposiciones sobre textos académicos) e instrumentos que se emplearán para la evaluación " & _
                                             "de cada una de ellas. Por ejemplo: listas de cotejo, escalas estimativas, rúbricas, pruebas de ensayo etc.", 5, 10.0F, 0, iTextSharp.text.BaseColor.BLACK))
            cellTable3.AddCell(fc_CeldaTexto(_textCri, 15, 5, 1, 0, -1, 6, ""))
            Dim _textEva As iTextSharp.text.Phrase
            _textEva = New iTextSharp.text.Phrase
            _textEva.Add(fc_textFrase("Normatividad: " & Environment.NewLine & " " & Environment.NewLine, 5, 10.0F, 1, iTextSharp.text.BaseColor.BLACK))

            For _nor As Integer = 0 To dtNor.Rows.Count - 1
                _textEva.Add(fc_textFrase(" - " & dtNor.Rows(_nor).Item("descripcion_nor").ToString & Environment.NewLine & " " & Environment.NewLine, 5, 10.0F, 0, iTextSharp.text.BaseColor.BLACK))
            Next

            cellTable3.AddCell(fc_CeldaTexto(_textEva, 15, 5, 1, 0, -1, 6, ""))

            pdfTable2.AddCell(cellTable3)

            ' 20200327-ENevado ===================================================================================================

            pdfTable2.AddCell(fc_CeldaTexto("" & Environment.NewLine, 10.0F, 1, 0, 1, 1, 1))

            Dim cellTablen As iTextSharp.text.pdf.PdfPTable = New iTextSharp.text.pdf.PdfPTable(5)
            cellTablen.WidthPercentage = 100.0F
            cellTablen.SetWidths(New Single() {40.0F, 20.0F, 10.0F, 10.0F, 20.0F})
            cellTablen.DefaultCell.Border = 0

            cellTablen.AddCell(fc_CeldaTexto("6.2 Sistema de calificación", 10.0F, 1, 15, 5, 1, 0, -1, 6, "WhiteSmoke"))
            cellTablen.AddCell(fc_CeldaTexto("Fórmula para la obtención de la nota de resultado de aprendizaje (RA)", 10.0F, 1, 15, 5, 1, 1, -1, 6, "WhiteSmoke"))
            _textEva = New iTextSharp.text.Phrase
            _textEva.Add(fc_textFrase("RA ", 5, 10.0F, 1, iTextSharp.text.BaseColor.BLACK))
            _textEva.Add(fc_textFrase("= promedio (Calificaciones obtenidas en sus indicadores)", 5, 10.0F, 0, iTextSharp.text.BaseColor.BLACK))
            cellTablen.AddCell(fc_CeldaTexto(_textEva, 15, 5, 1, 1))

            cellTablen.AddCell(fc_CeldaTexto("Evaluación", 10.0F, 1, 15, 1, 1, 1, 5, 6, "WhiteSmoke"))
            cellTablen.AddCell(fc_CeldaTexto("Unidad(es) en la(s) que se trabaja", 10.0F, 1, 15, 1, 1, 1, 5, 6, "WhiteSmoke"))
            cellTablen.AddCell(fc_CeldaTexto("Peso", 10.0F, 1, 15, 2, 1, 1, 5, 6, "WhiteSmoke"))
            cellTablen.AddCell(fc_CeldaTexto("N° de evaluaciones", 10.0F, 1, 15, 1, 1, 1, 5, 6, "WhiteSmoke"))

            Dim strRes As String = " ="
            Dim prom_inst As Integer = 0, cant_xfila As Integer = 0
            For x = 0 To (dtRes.Rows.Count - 1)
                cant_xfila = 1

                cellTablen.AddCell(fc_CeldaTexto("Resultado de aprendizaje N° " & String.Format("{0:00}", (x + 1)) & " (RA" & (x + 1) & ")", 10.0F, 0, 15, 1, cant_xfila, 0, 5))
                cellTablen.AddCell(fc_CeldaTexto(dtRes.Rows(x).Item("unidad_res").ToString, 10.0F, 0, 15, 1, cant_xfila, 1, 5))

                cellTablen.AddCell(fc_CeldaTexto(dtRes.Rows(x).Item("peso_res"), 10.0F, 0, 15, 2, 1, 1))
                cellTablen.AddCell(fc_CeldaTexto(String.Format("{0:00}", dtRes.Rows(x).Item("cant_ins")), 10.0F, 0, 15, 1, 1, 1))
                strRes = strRes & " RA" & (x + 1) & "(" & dtRes.Rows(x).Item("peso_res") & ") +"
                _total_eva += CInt(dtRes.Rows(x).Item("cant_ins"))
            Next
            strRes = strRes.TrimEnd("+")

            cellTablen.AddCell(fc_CeldaTexto("Total de evaluaciones programadas", 10.0F, 1, 15, 4, 1, 1, -1, 6, "WhiteSmoke"))
            cellTablen.AddCell(fc_CeldaTexto(String.Format("{0:00}", _total_eva), 10.0F, 1, 15, 1, 1, 1))
            cellTablen.AddCell(fc_CeldaTexto("Fórmula para la obtención de la nota final de la asignatura (NF)", 10.0F, 1, 15, 5, 1, 1, -1, 6, "WhiteSmoke"))
            _textEva = New iTextSharp.text.Phrase
            _textEva.Add(fc_textFrase("NF", 5, 10.0F, 1, iTextSharp.text.BaseColor.BLACK))
            _textEva.Add(fc_textFrase(strRes, 5, 10.0F, 0, iTextSharp.text.BaseColor.BLACK))
            cellTablen.AddCell(fc_CeldaTexto(_textEva, 15, 5, 1, 1))

            pdfTable2.AddCell(cellTablen)

            ' =========================================================================================================================

            pdfTable2.AddCell(fc_CeldaTexto("" & Environment.NewLine, 10.0F, 1, 0, 2, 1, 0))

            ' 7: Referencias --------------------------------------------------------------------------------------------------------------

            Dim dtRef As Data.DataTable
            obj.AbrirConexion()
            dtRef = obj.TraerDataTable("ReferenciaBibliografica_Listar", -1, codigo_dis, -1, "")
            obj.CerrarConexion()

            pdfTable2.AddCell(fc_CeldaTexto("VII. ", 10.0F, 1, 0, 1, 1, 0))
            pdfTable2.AddCell(fc_CeldaTexto("REFERENCIAS", 10.0F, 1, 0, 1, 1, 0))

            Dim dtRef1, dtRef2, dtRef3, dtRef4 As Data.DataTable
            Dim strRef1, strRef2, strRef3 As String

            dtRef1 = New Data.DataView(dtRef, "nombre = " & "'USAT'", "", Data.DataViewRowState.CurrentRows).ToTable
            dtRef2 = New Data.DataView(dtRef, "nombre = " & "'Complementarias' AND tipo_ref = 0", "", Data.DataViewRowState.CurrentRows).ToTable
            dtRef4 = New Data.DataView(dtRef, "nombre = " & "'Complementarias' AND tipo_ref = 1", "", Data.DataViewRowState.CurrentRows).ToTable
            dtRef3 = New Data.DataView(dtRef, "nombre = " & "'Investigaciones de docentes'", "", Data.DataViewRowState.CurrentRows).ToTable

            strRef1 = "" : strRef2 = "" : strRef3 = ""

            pdfTable2.AddCell(fc_CeldaTexto("" & Environment.NewLine, 10.0F, 1, 0, 1, IIf(dtRef4.Rows.Count > 0, 8, 6), 1))

            For x = 0 To (dtRef1.Rows.Count - 1)
                strRef1 = strRef1 & "• " & dtRef1.Rows(x).Item("descripcion_ref").ToString & Environment.NewLine
            Next
            For x = 0 To (dtRef2.Rows.Count - 1)
                strRef2 = strRef2 & "• " & dtRef2.Rows(x).Item("descripcion_ref").ToString & Environment.NewLine
            Next
            For x = 0 To (dtRef3.Rows.Count - 1)
                strRef3 = strRef3 & "• " & dtRef3.Rows(x).Item("descripcion_ref").ToString & Environment.NewLine
            Next
            Dim _textRefCompUrl As New iTextSharp.text.Phrase
            For x = 0 To (dtRef4.Rows.Count - 1)
                _textRefCompUrl.Add(fc_textFrase("• " & dtRef4.Rows(x).Item("descripcion_ref").ToString & " Recuperado de: " & Environment.NewLine, 5, 10.0F, 0, iTextSharp.text.BaseColor.BLACK))
                _textRefCompUrl.Add(fc_textFrase(dtRef4.Rows(x).Item("observacion_ref").ToString & Environment.NewLine, 5, 10.0F, 4, iTextSharp.text.BaseColor.BLUE))
            Next

            pdfTable2.AddCell(fc_CeldaTexto("7.1 Referencias USAT", 10.0F, 1, 15, 1, 1, 0, -1, 6, "WhiteSmoke"))
            pdfTable2.AddCell(fc_CeldaTexto(strRef1, 10.0F, 0, 15, 1, 1, 0))

            pdfTable2.AddCell(fc_CeldaTexto("7.2 Referencias complementarias", 10.0F, 1, 15, 1, 1, 0, -1, 6, "WhiteSmoke"))
            pdfTable2.AddCell(fc_CeldaTexto(strRef2, 10.0F, 0, 15, 1, 1, 0))

            If dtRef4.Rows.Count > 0 Then
                'pdfTable2.AddCell(fc_CeldaTexto("Enlaces de Internet", 10.0F, 1, 15, 1, 1, 0, -1, 6)) 'Comentado por Luis Q.T. | 27AGO2020 | GLPI 38371
                pdfTable2.AddCell(fc_CeldaTexto(_textRefCompUrl, 15, 1, 1, 0, 5, 8))
            End If

            pdfTable2.AddCell(fc_CeldaTexto("7.3 Investigaciones de docentes", 10.0F, 1, 15, 1, 1, 0, -1, 6, "WhiteSmoke"))
            pdfTable2.AddCell(fc_CeldaTexto(strRef3, 10.0F, 0, 15, 1, 1, 0))

            pdfTable2.AddCell(fc_CeldaTexto("" & Environment.NewLine, 10.0F, 1, 0, 2, 1, 0))

            ' 8: Programacion de Actividades -------------------------------------------------------------------------------

            Dim dtGru, dtSes, dtAct, dtEva As Data.DataTable
            Dim corCont As Integer
            obj.AbrirConexion()
            dtGru = obj.TraerDataTable("GrupoContenidoAsignatura_Listar", "GC", -1, codigo_dis)
            dtAct = obj.TraerDataTable("ActividadAsignatura_Listar", "AA", -1, codigo_dis)
            dtSes = obj.TraerDataTable("SesionAsignatura_Listar", "SB", codigo_cup, codigo_dis, "")
            dtEva = obj.TraerDataTable("EvaluacionCurso_Listar", "EC", -1, codigo_cup, -1, -1)
            obj.CerrarConexion()

            pdfTable2.AddCell(fc_CeldaTexto("VIII. ", 10.0F, 1, 0, 1, 1, 0))
            pdfTable2.AddCell(fc_CeldaTexto("PROGRAMACIÓN DE ACTIVIDADES", 10.0F, 1, 0, 1, 1, 0))

            Dim _ngrupo As Boolean

            For x = 0 To (dtUni.Rows.Count - 1)

                _ngrupo = False

                pdfTable2.AddCell(fc_CeldaTexto("" & Environment.NewLine, 10.0F, 1, 0, 1, 1, 1))

                Dim cellTable4 As iTextSharp.text.pdf.PdfPTable = New iTextSharp.text.pdf.PdfPTable(4)
                cellTable4.WidthPercentage = 100.0F
                cellTable4.SetWidths(New Single() {20.0F, 35.0F, 30.0F, 15.0F})
                cellTable4.DefaultCell.Border = 0

                cellTable4.AddCell(fc_CeldaTexto("Unidad didáctica N° " & String.Format("{0:00}", (x + 1)) & ": " & dtUni.Rows(x).Item("descripcion_uni").ToString, 9.0F, 1, 15, 4, 1, 1, -1, 6, "WhiteSmoke"))
                cellTable4.AddCell(fc_CeldaTexto("Sesión" & Environment.NewLine & "(N° / dd-mm)", 9.0F, 1, 15, 1, 1, 1, 5, 6, "WhiteSmoke"))
                cellTable4.AddCell(fc_CeldaTexto("Contenidos", 9.0F, 1, 15, 1, 1, 1, 5, 6, "WhiteSmoke"))
                cellTable4.AddCell(fc_CeldaTexto("Actividades", 9.0F, 1, 15, 1, 1, 1, 5, 6, "WhiteSmoke"))
                cellTable4.AddCell(fc_CeldaTexto("Evaluaciones", 9.0F, 1, 15, 1, 1, 1, 5, 6, "WhiteSmoke"))

                Dim dtGrux, dtCony, dtActx, dtSesx, dtEvax As Data.DataTable
                Dim strCony, strActx, strFecx, strMes, strEvax As String
                Dim strFec As String()

                strCony = "" : strActx = "" : strFecx = "" : strMes = "" : strEvax = ""
                dtGrux = New Data.DataView(dtGru, "codigo_uni = " & dtUni.Rows(x).Item("codigo_uni"), "", Data.DataViewRowState.CurrentRows).ToTable

                corCont = 0

                For y = 0 To (dtGrux.Rows.Count - 1)

                    dtCony = New Data.DataView(dtCon, "codigo_gru = " & dtGrux.Rows(y).Item("codigo_gru"), "", Data.DataViewRowState.CurrentRows).ToTable
                    dtActx = New Data.DataView(dtAct, "codigo_gru = " & dtGrux.Rows(y).Item("codigo_gru"), "", Data.DataViewRowState.CurrentRows).ToTable
                    dtSesx = New Data.DataView(dtSes, "codigo_gru = " & dtGrux.Rows(y).Item("codigo_gru"), "", Data.DataViewRowState.CurrentRows).ToTable
                    dtEvax = New Data.DataView(dtEva, "codigo_gru = " & dtGrux.Rows(y).Item("codigo_gru"), "", Data.DataViewRowState.CurrentRows).ToTable
                    strCony = "" : strActx = "" : strFecx = "" : strMes = "" : strEvax = ""
                    strFec = Nothing
                    If dtCony.Rows.Count = 0 Then
                        'Throw New Exception("Unidad didáctica N° " & String.Format("{0:00}", (x + 1)))
                    Else
                        For z = 0 To (dtCony.Rows.Count - 1)
                            corCont = corCont + 1
                            strCony = strCony & (x + 1) & "." & corCont & " " & dtCony.Rows(z).Item("descripcion").ToString.Replace("'", "").Replace(vbCr, " ").Replace(vbLf, "") & Environment.NewLine
                        Next
                    End If
                    For z = 0 To (dtActx.Rows.Count - 1)
                        strActx = strActx & "• " & dtActx.Rows(z).Item("descripcion").ToString.Replace("'", "").Replace(vbCr, " ").Replace(vbLf, "") & Environment.NewLine
                    Next
                    For z = 0 To (dtEvax.Rows.Count - 1)
                        strEvax = strEvax & "• " & dtEvax.Rows(z).Item("descripcion_evi").ToString.Replace("'", "").Replace(vbCr, " ").Replace(vbLf, "") & " (" & dtEvax.Rows(z).Item("descripcion_ins").ToString.Replace("'", "").Replace(vbCr, " ").Replace(vbLf, "") & ")" & Environment.NewLine
                    Next
                    If dtSesx.Rows.Count = 1 Then

                        If _ngrupo Then

                            pdfTable2.AddCell(fc_CeldaTexto("" & Environment.NewLine, 10.0F, 1, 0, 1, 1, 1))

                            cellTable4 = New iTextSharp.text.pdf.PdfPTable(4)
                            cellTable4.WidthPercentage = 100.0F
                            cellTable4.SetWidths(New Single() {20.0F, 35.0F, 30.0F, 15.0F})
                            cellTable4.DefaultCell.Border = 0

                        End If

                        If dtSesx.Rows(0).Item("fecha_ses").ToString = "-" Then
                            strFecx = "-"
                        Else
                            strFec = dtSesx.Rows(0).Item("fecha_ses").ToString.Split(",")
                            For j = 0 To strFec.Length - 1
                                strFecx = strFecx & strFec(j) & Environment.NewLine
                            Next
                        End If
                        cellTable4.AddCell(fc_CeldaTexto(dtSesx.Rows(0).Item("nombre_ses").ToString & Environment.NewLine & strFecx, 8.0F, 0, 15, 1, 1, 1, 5))
                        cellTable4.AddCell(fc_CeldaTexto(strCony, 7.0F, 0, 15, 1, 1, 0))
                        cellTable4.AddCell(fc_CeldaTexto(strActx, 8.0F, 0, 15, 1, 1, 0))
                        cellTable4.AddCell(fc_CeldaTexto(strEvax, 8.0F, 0, 15, 1, 1, 0))

                        If _ngrupo Then
                            pdfTable2.AddCell(cellTable4)
                        End If

                    Else
                        ' 20200327-ENevado ===================================================================================================
                        If dtSesx.Rows.Count > 20 Then

                            If Not _ngrupo Then
                                pdfTable2.AddCell(cellTable4)
                            End If

                            _ngrupo = True

                            Dim _round, _mod As Integer
                            _round = Math.Floor(dtSesx.Rows.Count / 20)
                            _mod = dtSesx.Rows.Count Mod 20

                            Dim lb_session As Boolean
                            For _row As Integer = 0 To (_round - 1)
                                If _row = 0 Then

                                    pdfTable2.AddCell(fc_CeldaTexto("" & Environment.NewLine, 10.0F, 1, 0, 1, 1, 1))

                                    cellTable4 = New iTextSharp.text.pdf.PdfPTable(4)
                                    cellTable4.WidthPercentage = 100.0F
                                    cellTable4.SetWidths(New Single() {20.0F, 35.0F, 30.0F, 15.0F})
                                    cellTable4.DefaultCell.Border = 0

                                    For i = (_row * 20) To (((_row + 1) * 20) - 1)
                                        lb_session = True
                                        strFecx = ""
                                        If i = (_row * 20) Then
                                            If dtSesx.Rows(i).Item("fecha_ses").ToString = "-" Then
                                                strFecx = "-"
                                            Else
                                                strFec = dtSesx.Rows(i).Item("fecha_ses").ToString.Split(",")
                                                For j = 0 To strFec.Length - 1
                                                    strFecx = strFecx & strFec(j) & Environment.NewLine
                                                Next
                                            End If
                                            cellTable4.AddCell(fc_CeldaTexto(dtSesx.Rows(i).Item("nombre_ses").ToString & Environment.NewLine & strFecx, 8.0F, 0, 15, 1, 1, 1, 5))
                                            cellTable4.AddCell(fc_CeldaTexto(strCony, 7.0F, 0, 15, 1, 20, 0))
                                            cellTable4.AddCell(fc_CeldaTexto(strActx, 8.0F, 0, 15, 1, 20, 0))
                                            cellTable4.AddCell(fc_CeldaTexto(strEvax, 8.0F, 0, 15, 1, 20, 0))
                                        Else
                                            If dtSesx.Rows(i).Item("fecha_ses").ToString = "-" Then
                                                strFecx = "-"
                                            Else
                                                strFec = dtSesx.Rows(i).Item("fecha_ses").ToString.Split(",")
                                                For j = 0 To strFec.Length - 1
                                                    strFecx = strFecx & strFec(j) & Environment.NewLine
                                                Next
                                            End If
                                            If lb_session Then cellTable4.AddCell(fc_CeldaTexto(dtSesx.Rows(i).Item("nombre_ses").ToString & Environment.NewLine & strFecx, 8.0F, 0, 15, 1, 1, 1, 5))
                                        End If
                                    Next
                                    pdfTable2.AddCell(cellTable4)

                                Else

                                    pdfTable2.AddCell(fc_CeldaTexto("" & Environment.NewLine, 10.0F, 1, 0, 1, 1, 1))

                                    cellTable4 = New iTextSharp.text.pdf.PdfPTable(4)
                                    cellTable4.WidthPercentage = 100.0F
                                    cellTable4.SetWidths(New Single() {20.0F, 35.0F, 30.0F, 15.0F})
                                    cellTable4.DefaultCell.Border = 0

                                    For i = (_row * 20) To (((_row + 1) * 20) - 1)
                                        lb_session = True
                                        strFecx = ""
                                        If i = (_row * 20) Then
                                            If dtSesx.Rows(i).Item("fecha_ses").ToString = "-" Then
                                                strFecx = "-"
                                            Else
                                                strFec = dtSesx.Rows(i).Item("fecha_ses").ToString.Split(",")
                                                For j = 0 To strFec.Length - 1
                                                    strFecx = strFecx & strFec(j) & Environment.NewLine
                                                Next
                                            End If
                                            cellTable4.AddCell(fc_CeldaTexto(dtSesx.Rows(i).Item("nombre_ses").ToString & Environment.NewLine & strFecx, 8.0F, 0, 15, 1, 1, 1, 5))
                                            cellTable4.AddCell(fc_CeldaTexto(strCony, 7.0F, 0, 15, 1, 20, 0))
                                            cellTable4.AddCell(fc_CeldaTexto(strActx, 8.0F, 0, 15, 1, 20, 0))
                                            cellTable4.AddCell(fc_CeldaTexto(strEvax, 8.0F, 0, 15, 1, 20, 0))
                                        Else
                                            If dtSesx.Rows(i).Item("fecha_ses").ToString = "-" Then
                                                strFecx = "-"
                                            Else
                                                strFec = dtSesx.Rows(i).Item("fecha_ses").ToString.Split(",")
                                                For j = 0 To strFec.Length - 1
                                                    strFecx = strFecx & strFec(j) & Environment.NewLine
                                                Next
                                            End If
                                            If lb_session Then cellTable4.AddCell(fc_CeldaTexto(dtSesx.Rows(i).Item("nombre_ses").ToString & Environment.NewLine & strFecx, 8.0F, 0, 15, 1, 1, 1, 5))
                                        End If
                                    Next

                                    pdfTable2.AddCell(cellTable4)

                                End If
                            Next
                            If _mod > 0 Then
                                pdfTable2.AddCell(fc_CeldaTexto("" & Environment.NewLine, 10.0F, 1, 0, 1, 1, 1))

                                cellTable4 = New iTextSharp.text.pdf.PdfPTable(4)
                                cellTable4.WidthPercentage = 100.0F
                                cellTable4.SetWidths(New Single() {20.0F, 35.0F, 30.0F, 15.0F})
                                cellTable4.DefaultCell.Border = 0

                                For _sobra As Integer = 0 To (_mod - 1)
                                    lb_session = True
                                    strFecx = ""
                                    If _sobra = 0 Then
                                        If dtSesx.Rows(_sobra + (_round * 20)).Item("fecha_ses").ToString = "-" Then
                                            strFecx = "-"
                                        Else
                                            strFec = dtSesx.Rows(_sobra + (_round * 20)).Item("fecha_ses").ToString.Split(",")
                                            For j = 0 To strFec.Length - 1
                                                strFecx = strFecx & strFec(j) & Environment.NewLine
                                            Next
                                        End If
                                        cellTable4.AddCell(fc_CeldaTexto(dtSesx.Rows(_sobra + (_round * 20)).Item("nombre_ses").ToString & Environment.NewLine & strFecx, 8.0F, 0, 15, 1, 1, 1, 5))
                                        cellTable4.AddCell(fc_CeldaTexto(strCony, 7.0F, 0, 15, 1, _mod, 0))
                                        cellTable4.AddCell(fc_CeldaTexto(strActx, 8.0F, 0, 15, 1, _mod, 0))
                                        cellTable4.AddCell(fc_CeldaTexto(strEvax, 8.0F, 0, 15, 1, _mod, 0))
                                    Else
                                        If dtSesx.Rows(_sobra + (_round * 20)).Item("fecha_ses").ToString = "-" Then
                                            strFecx = "-"
                                        Else
                                            strFec = dtSesx.Rows(_sobra + (_round * 20)).Item("fecha_ses").ToString.Split(",")
                                            For j = 0 To strFec.Length - 1
                                                strFecx = strFecx & strFec(j) & Environment.NewLine
                                            Next
                                        End If
                                        If lb_session Then cellTable4.AddCell(fc_CeldaTexto(dtSesx.Rows(_sobra + (_round * 20)).Item("nombre_ses").ToString & Environment.NewLine & strFecx, 8.0F, 0, 15, 1, 1, 1, 5))
                                    End If
                                Next
                                pdfTable2.AddCell(cellTable4)
                            End If
                            ' =========================================================================================================================
                        Else
                            Dim lb_session As Boolean
                            For i = 0 To (dtSesx.Rows.Count - 1)
                                lb_session = True
                                strFecx = ""
                                If i = 0 Then
                                    If dtSesx.Rows(i).Item("fecha_ses").ToString = "-" Then
                                        strFecx = "-"
                                    Else
                                        strFec = dtSesx.Rows(i).Item("fecha_ses").ToString.Split(",")
                                        For j = 0 To strFec.Length - 1
                                            strFecx = strFecx & strFec(j) & Environment.NewLine
                                        Next
                                    End If
                                    cellTable4.AddCell(fc_CeldaTexto(dtSesx.Rows(i).Item("nombre_ses").ToString & Environment.NewLine & strFecx, 8.0F, 0, 15, 1, 1, 1, 5))
                                    cellTable4.AddCell(fc_CeldaTexto(strCony, 7.0F, 0, 15, 1, dtSesx.Rows.Count, 0))
                                    cellTable4.AddCell(fc_CeldaTexto(strActx, 8.0F, 0, 15, 1, dtSesx.Rows.Count, 0))
                                    cellTable4.AddCell(fc_CeldaTexto(strEvax, 8.0F, 0, 15, 1, dtSesx.Rows.Count, 0))
                                Else
                                    If dtSesx.Rows(i).Item("fecha_ses").ToString = "-" Then
                                        strFecx = "-"
                                    Else
                                        strFec = dtSesx.Rows(i).Item("fecha_ses").ToString.Split(",")
                                        For j = 0 To strFec.Length - 1
                                            strFecx = strFecx & strFec(j) & Environment.NewLine
                                        Next
                                    End If
                                    If lb_session Then cellTable4.AddCell(fc_CeldaTexto(dtSesx.Rows(i).Item("nombre_ses").ToString & Environment.NewLine & strFecx, 8.0F, 0, 15, 1, 1, 1, 5))
                                End If
                            Next
                        End If
                    End If
                Next

                If Not _ngrupo Then
                    pdfTable2.AddCell(cellTable4)
                End If

            Next

            pdfDoc.Add(pdfTable2)

            pdfDoc.Close()

            ' Return "OK"
        Catch ex As Exception
            Throw ex
            'Return ex.Message.ToString
        End Try
    End Sub

    Private Sub mt_AddWaterMark(ByVal pdfwrite As iTextSharp.text.pdf.PdfWriter, ByVal texto As String)
        Dim bfTimes As iTextSharp.text.pdf.BaseFont = iTextSharp.text.pdf.BaseFont.CreateFont(iTextSharp.text.pdf.BaseFont.TIMES_ITALIC, iTextSharp.text.pdf.BaseFont.CP1252, False)
        Dim times As iTextSharp.text.Font = New iTextSharp.text.Font(bfTimes, 145.5F, iTextSharp.text.Font.ITALIC, iTextSharp.text.BaseColor.LIGHT_GRAY)
        iTextSharp.text.pdf.ColumnText.ShowTextAligned(pdfwrite.DirectContent, 1, New iTextSharp.text.Phrase(texto, times), 295.5F, 450.0F, 55)
    End Sub

#End Region

#Region "Funciones"

    ''' <summary>
    ''' Función para crear una celda tipo texto con más atributos
    ''' </summary>
    ''' <param name="_text">Contenido de la Celda</param>
    ''' <param name="_size">Tamaño de letra del contenido de la celda</param>
    ''' <param name="_style">Estilo del Contenido de la Celda. 0: NORMAL, 1: BOLD, 2: ITALIC, 3: BOLDITALIC, 4: UNDERLINE</param>
    ''' <param name="_border">Borde de la Celda. 0: NO_BORDER, 1: TOP_BORDER , 2: BOTTON_BORDER, 4: LEFT_BORDER, 8: RIGHT_BORDER, 15: FULL_BORDER </param>
    ''' <param name="_colspan">Cantidad de Columnas</param>
    ''' <param name="_rowspan">Cantidad de Filas</param>
    ''' <param name="_haligment">Alineacion horizontal del contenido de la celda. 0: LEFT, 1: CENTER, 2: RIGHT, 4: TOP</param>
    ''' <param name="_valigment">Alineacion Vertical. 5: MIDDLE</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function fc_CeldaTexto(ByVal _text As String, ByVal _size As Single, ByVal _style As Integer, ByVal _border As Integer, _
                                 ByVal _colspan As Integer, ByVal _rowspan As Integer, ByVal _haligment As Integer, _
                                 Optional ByVal _valigment As Integer = -1, Optional ByVal _padding As Integer = 6, _
                                 Optional ByVal _backgroundcolor As String = "") As iTextSharp.text.pdf.PdfPCell
        Dim celdaITC As iTextSharp.text.pdf.PdfPCell
        Dim fontITC As iTextSharp.text.Font
        'fontITC = New iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, _size, _style)
        Dim segoe As iTextSharp.text.pdf.BaseFont = iTextSharp.text.pdf.BaseFont.CreateFont(_fuente, iTextSharp.text.pdf.BaseFont.IDENTITY_H, iTextSharp.text.pdf.BaseFont.EMBEDDED)
        fontITC = New iTextSharp.text.Font(segoe, _size, _style)
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

    Private Function fc_CeldaTexto3(ByVal _list As iTextSharp.text.List, ByVal _size As Single, ByVal _style As Integer, ByVal _border As Integer, _
                                 ByVal _colspan As Integer, ByVal _rowspan As Integer, ByVal _haligment As Integer, _
                                 Optional ByVal _valigment As Integer = -1, Optional ByVal _padding As Integer = 6, _
                                 Optional ByVal _backgroundcolor As String = "") As iTextSharp.text.pdf.PdfPCell
        Dim celdaITC As iTextSharp.text.pdf.PdfPCell
        Dim fontITC As iTextSharp.text.Font
        Dim texto As New iTextSharp.text.Phrase
        Dim segoe As iTextSharp.text.pdf.BaseFont = iTextSharp.text.pdf.BaseFont.CreateFont(_fuente, iTextSharp.text.pdf.BaseFont.IDENTITY_H, iTextSharp.text.pdf.BaseFont.EMBEDDED)
        fontITC = New iTextSharp.text.Font(segoe, _size, _style)
        texto.Add(_list)
        celdaITC = New iTextSharp.text.pdf.PdfPCell(New iTextSharp.text.Phrase("", fontITC))
        'celdaITC = New iTextSharp.text.pdf.PdfPCell()
        celdaITC.Border = _border
        celdaITC.Colspan = _colspan
        celdaITC.Rowspan = _rowspan
        celdaITC.HorizontalAlignment = _haligment
        celdaITC.VerticalAlignment = _valigment
        celdaITC.Padding = _padding
        celdaITC.AddElement(texto)
        If _backgroundcolor <> "" Then celdaITC.BackgroundColor = New iTextSharp.text.BaseColor(System.Drawing.Color.FromName(_backgroundcolor))
        'celdaITC.SetLeading(0.0F, 1.2F)
        Return celdaITC
    End Function

    '''' <summary>
    '''' Función para crear una celta tipo texto con más atributos
    '''' </summary>
    '''' <param name="_text">Contenido de la Celda</param>
    '''' <param name="_size">Tamano de letra del contenido de la celda</param>
    '''' <param name="_style">Estilo del Contenido de la Celda. 0: NORMAL, 1: BOLD, 2: ITALIC, 3: BOLDITALIC, 4: UNDERLINE</param>
    '''' <param name="_border">Borde de la Celda. 0: NO_BORDER, 1: TOP_BORDER , 2: BOTTON_BORDER, 4: LEFT_BORDER, 8: RIGHT_BORDER, 15: FULL_BORDER </param>
    '''' <param name="_colspan">Cantidad de Columnas</param>
    '''' <param name="_rowspan">Cantidad de Filas</param>
    '''' <param name="_haligment">Alineacion horizontal del contenido de la celda. 0: LEFT, 1: CENTER, 2: RIGHT, 4: TOP, 5: MIDDLE</param>
    '''' <param name="_fontcolor"></param>
    '''' <returns></returns>
    '''' <remarks></remarks>
    'Private Function fc_CeldaTexto(ByVal _text As String, ByVal _size As Single, ByVal _style As Integer, ByVal _border As Integer, _
    '                             ByVal _colspan As Integer, ByVal _rowspan As Integer, ByVal _haligment As Integer, _
    '                             ByVal _fontcolor As iTextSharp.text.BaseColor) As iTextSharp.text.pdf.PdfPCell
    '    Dim celdaITC As iTextSharp.text.pdf.PdfPCell
    '    Dim fontITC As iTextSharp.text.Font
    '    fontITC = New iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, _size, _style, _fontcolor)
    '    celdaITC = New iTextSharp.text.pdf.PdfPCell(New iTextSharp.text.Phrase(_text, fontITC))
    '    celdaITC.Border = _border
    '    celdaITC.Colspan = _colspan
    '    celdaITC.Rowspan = _rowspan
    '    celdaITC.HorizontalAlignment = _haligment
    '    celdaITC.Padding = 6
    '    Return celdaITC
    'End Function

    ''' <summary>
    ''' Funcion para crear un celda tipo frase
    ''' </summary>
    ''' <param name="_phrase">Frase de la celda</param>
    ''' <param name="_border">Borde de la Celda. 0: NO_BORDER, 1: TOP_BORDER , 2: BOTTON_BORDER, 4: LEFT_BORDER, 8: RIGHT_BORDER, 15: FULL_BORDER</param>
    ''' <param name="_colspan">Cantidad de Columnas</param>
    ''' <param name="_rowspan">Cantidad de Filas</param>
    ''' <param name="_haligment">Alineacion horizontal del contenido de la celda. 0: LEFT, 1: CENTER, 2: RIGHT, 4: TOP, 5: MIDDLE</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function fc_CeldaTexto(ByVal _phrase As iTextSharp.text.Phrase, ByVal _border As Integer, ByVal _colspan As Integer, ByVal _rowspan As Integer, _
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
        _celda.SetLeading(0.0F, 1.15F)
        If _backgroundcolor <> "" Then _celda.BackgroundColor = New iTextSharp.text.BaseColor(System.Drawing.Color.FromName(_backgroundcolor))
        Return _celda
    End Function

    ''' <summary>
    ''' Función para crear una celta tipo texto
    ''' </summary>
    ''' <param name="_text">Contenido de la Celda</param>
    ''' <param name="_size">Tamano de letra del contenido de la celda</param>
    ''' <param name="_style">Estilo del Contenido de la Celda. 0: NORMAL, 1: BOLD, 2: ITALIC, 3: BOLDITALIC</param>
    ''' <param name="_haligment">Alineacion horizontal del contenido de la celda. 0: LEFT, 1: CENTER, 2: RIGHT, 4: TOP, 5: MIDDLE</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function fc_CeldaTexto2(ByVal _text As String, ByVal _size As Single, ByVal _style As Integer, ByVal _haligment As Integer) As iTextSharp.text.pdf.PdfPCell
        Dim celdaITC2 As iTextSharp.text.pdf.PdfPCell
        Dim fontITC2 As iTextSharp.text.Font
        fontITC2 = New iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, _size, _style)
        celdaITC2 = New iTextSharp.text.pdf.PdfPCell(New iTextSharp.text.Phrase(_text, fontITC2))
        celdaITC2.HorizontalAlignment = _haligment
        Return celdaITC2
    End Function

    ''' <summary>
    ''' Función que devuelve un texto fon formato
    ''' </summary>
    ''' <param name="_text">Contenido del Texto. </param>
    ''' <param name="_fontfamily">Tipo de Letra. -1: UNDEFINED, 0: COURIER, 1: HELVETICA, 2: TIMES_ROMAN, 3: SYMBOL, 4: ZAPFDINGBATS</param>
    ''' <param name="_size">Tamaño de letra del contenido de la celda</param>
    ''' <param name="_style">Estilo del Contenido de la Celda. 0: NORMAL, 1: BOLD, 2: ITALIC, 3: BOLDITALIC, 4: UNDERLINE</param>
    ''' <param name="_fontcolor"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
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

#End Region

End Class
