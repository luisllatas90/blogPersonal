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

Partial Class administrativo_frmExportaFichaPostulacion
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
        Dim textohtml As String
        Dim objcx As New ClsConectarDatos
        Dim tbl As Data.DataTable
        Dim mes As String
        Dim dia As String
        Dim año As String

        Dim documento As New Document(PageSize.A4, 75, 85, 57, 30)
        Dim ms As New MemoryStream
        Dim writer As PdfWriter
        Dim parrafo1 As New Paragraph ' Declaracion de un parrafo
        Dim parrafo2 As New Paragraph ' Declaracion de un parrafo
        Dim parrafo3 As New Paragraph ' Declaracion de un parrafo
        Dim parrafo35 As New Paragraph ' Declaracion de un parrafo
        Dim parrafo36 As New Paragraph ' Declaracion de un parrafo
        Dim parrafo4 As New Paragraph ' Declaracion de un parrafo
        Dim parrafo5 As New Paragraph ' Declaracion de un parrafo
        Dim parrafo6 As New Paragraph ' Declaracion de un parrafo

        'Dim cb As iTextSharp.text.pdf.PdfContentByte

        objcx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        objcx.AbrirConexion()

      
        writer = PdfWriter.GetInstance(documento, ms)

        'Obtener data
        '------------------------------------------------------------------------------
        tbl = objcx.TraerDataTable("PERSON_ConsultarAlumnoPersona", 3, Request.QueryString("pso"), Request.QueryString("cli"), "")

        'Obj.CerrarConexion()
        'Obj = Nothing

        If tbl.Rows.Count > 0 Then

            Dim cicloingreso As String = tbl.Rows(0).Item("cicloIng_Alu").ToString
            Dim codigouni As String = tbl.Rows(0).Item("codigoUniver_Alu").ToString
            Dim modalidad As String = tbl.Rows(0).Item("nombre_Min").ToString
            Dim alumno As String = tbl.Rows(0).Item("alumno").ToString
            Dim carreraprof As String = tbl.Rows(0).Item("carrera").ToString
            Dim preciocredito As String = tbl.Rows(0).Item("preciocredito").ToString

            Dim datefechanacalu As Date = Convert.ToDateTime(tbl.Rows(0).Item("fechaNacimiento_Alu").ToString)
            Dim fechanacalu As String = datefechanacalu.ToString("dd/MM/yyyy")
            Dim tipodocalu As String = tbl.Rows(0).Item("tipoDocIdent_Alu").ToString
            Dim nrodocalu As String = tbl.Rows(0).Item("nroDocIdent_Alu").ToString
            Dim paisnacalu As String = tbl.Rows(0).Item("nombrePaisNac").ToString
            If String.IsNullOrEmpty(paisnacalu) Then
                paisnacalu = tbl.Rows(0).Item("nombrePais").ToString
            End If
            Dim sexoalu As String = tbl.Rows(0).Item("sexo_Alu").ToString
            Dim distritonacalu As String = tbl.Rows(0).Item("nombreDistritoNac").ToString
            Dim ciudadalu As String = tbl.Rows(0).Item("ciudad_Dal").ToString
            Dim provincianacalu As String = tbl.Rows(0).Item("nombreProvinciaNac").ToString
            Dim dptonacalu As String = tbl.Rows(0).Item("nombreDptoNac").ToString
            Dim direccionalu As String = tbl.Rows(0).Item("direccion_Dal").ToString
            Dim distritodomalu As String = tbl.Rows(0).Item("nombreDistritoDomicilio").ToString
            Dim provinciadomalu As String = tbl.Rows(0).Item("nombreProvinciaDomicilio").ToString
            Dim dptodomalu As String = tbl.Rows(0).Item("nombreDptoDomicilio").ToString
            Dim tlfdomalu As String = tbl.Rows(0).Item("telefonoCasa_Dal").ToString
            Dim celularalu As String = tbl.Rows(0).Item("telefono_Dal").ToString
            Dim operadoralu As String = IIf(tbl.Rows(0).Item("OperadorMovil_Dal").ToString = "--Seleccione--", "", tbl.Rows(0).Item("OperadorMovil_Dal").ToString)
            Dim emailalu As String = tbl.Rows(0).Item("eMail_Alu").ToString
            Dim discapacidadaud As String = IIf(tbl.Rows(0).Item("discapacidadAud_Dal").ToString = True, "X", " ")
            Dim discapacidadfis As String = IIf(tbl.Rows(0).Item("discapacidadFis_Dal").ToString = True, "X", " ")
            Dim discapacidadvis As String = IIf(tbl.Rows(0).Item("discapacidadVis_Dal").ToString = True, "X", " ")
            Dim paiscolegio As String = tbl.Rows(0).Item("nombrepaiscolegio").ToString
            Dim colegio As String = tbl.Rows(0).Item("Nombre_ied").ToString
            Dim promocioncolegio As String = tbl.Rows(0).Item("añoEgresoSec_Dal").ToString
            Dim tipocolegio As String = tbl.Rows(0).Item("tipocolegio_dal").ToString
            Dim distritocolegio As String = tbl.Rows(0).Item("nombrediscolegio").ToString
            Dim provinciacolegio As String = tbl.Rows(0).Item("nombredepcolegio").ToString
            Dim depcolegio As String = tbl.Rows(0).Item("nombredepcolegio").ToString
            Dim direccioncolegio As String = tbl.Rows(0).Item("Direccion_ied").ToString
            Dim nombresfam As String = tbl.Rows(0).Item("PersonaFam_Dal").ToString
            Dim direccionfam As String = tbl.Rows(0).Item("direccionFam_Dal").ToString & " " & tbl.Rows(0).Item("urbanizacionFam_Dal").ToString
            Dim distritofam As String = tbl.Rows(0).Item("nombreDistritoFam").ToString
            Dim provinciafam As String = tbl.Rows(0).Item("nombreProvinciaFam").ToString
            Dim dptofam As String = tbl.Rows(0).Item("nombreDptoFam").ToString
            Dim tlfcasafam As String = tbl.Rows(0).Item("telefonoFam_Dal").ToString
            Dim tlfoffam As String = tbl.Rows(0).Item("telefonoTrabajoFam_Dal").ToString
            Dim celfam As String = tbl.Rows(0).Item("telefonoMovilFam_Dal").ToString
            Dim operadorfam As String = IIf(tbl.Rows(0).Item("OperadorMovilFam_Dal").ToString = "--Seleccione--", "", tbl.Rows(0).Item("OperadorMovilFam_Dal").ToString)
            Dim emailfam As String = tbl.Rows(0).Item("emailPadre_Dal").ToString
            Dim nombresapo As String = tbl.Rows(0).Item("PersonaApod_Dal").ToString
            Dim direccionapo As String = tbl.Rows(0).Item("direccionApod_Dal") & " " & tbl.Rows(0).Item("urbanizacionApod_Dal").ToString
            Dim distritoapo As String = tbl.Rows(0).Item("nombreDistritoApod").ToString
            Dim provinciaapo As String = tbl.Rows(0).Item("nombreProvinciaApod").ToString
            Dim dptoapo As String = tbl.Rows(0).Item("nombreDptoApod").ToString
            Dim tlfcasaapo As String = tbl.Rows(0).Item("telefonoCasaApod_Dal").ToString
            Dim tlfofapo As String = tbl.Rows(0).Item("telefonoTrabajoApod_Dal").ToString
            Dim celapo As String = tbl.Rows(0).Item("telefonoMovilApod_Dal").ToString
            Dim operadorapo As String = IIf(tbl.Rows(0).Item("OperadorMovilApod_Dal").ToString = "--Seleccione--", "", tbl.Rows(0).Item("OperadorMovilApod_Dal").ToString)
            Dim emailapo As String = tbl.Rows(0).Item("emailApoderado_Dal").ToString
            Dim observacion As String = tbl.Rows(0).Item("observacion_Dal").ToString
            Dim usuario As String = tbl.Rows(0).Item("UsuarioRegistro").ToString

            Dim observacionDeuda As String = tbl.Rows(0).Item("observacion_Deu").ToString.Trim
            Dim limiteObservacionDeuda As Integer = 180 'Limito la cantidad de caracteres para evitar que no se generen en 2 hojas
            Dim sufijoObservacionDeuda As String = ""
            Dim codigoTest As String = tbl.Rows(0).Item("codigo_test").ToString.Trim

            If observacionDeuda.Length > limiteObservacionDeuda Then
                sufijoObservacionDeuda = "..."
            End If
            observacionDeuda = Left(observacionDeuda, limiteObservacionDeuda) & sufijoObservacionDeuda

            fechaactual = tbl.Rows(0).Item("fechains")
            mes = fechaactual.ToString("MMMM")
            dia = fechaactual.ToString("dd")
            año = fechaactual.ToString("yyyy")



            '==========================Tabla1==========================================

            Dim table1 As New PdfPTable(2)
            'actual width of table in points
            table1.TotalWidth = 300.0F

            'fix the absolute width of the table
            table1.LockedWidth = True
            'relative col widths in proportions
            Dim width(1) As Integer
            width(0) = 1.0F
            width(1) = 2.0F
            table1.SetWidths(width)
            table1.HorizontalAlignment = 2 'alienacion derecha

            'leave a gap before and after the table
            table1.SpacingBefore = 20.0F
            'table.SpacingAfter = 30f;

            Dim celda70 As New PdfPCell(New Phrase("Código", FontFactory.GetFont("Arial", 10, Font.BOLD)))
            table1.AddCell(celda70)
            Dim celda71 As New PdfPCell(New Phrase(codigouni, FontFactory.GetFont("Arial", 9)))
            table1.AddCell(celda71)

            Dim celda72 As New PdfPCell(New Phrase("Modalidad", FontFactory.GetFont("Arial", 10, Font.BOLD)))
            table1.AddCell(celda72)
            Dim celda73 As New PdfPCell(New Phrase(modalidad, FontFactory.GetFont("Arial", 9)))
            table1.AddCell(celda73)

            Dim celda74 As New PdfPCell(New Phrase("Carrera Profesional", FontFactory.GetFont("Arial", 10, Font.BOLD)))
            table1.AddCell(celda74)
            Dim celda75 As New PdfPCell(New Phrase(carreraprof, FontFactory.GetFont("Arial", 9)))
            table1.AddCell(celda75)

            Dim celda76 As New PdfPCell(New Phrase("Costo Crédito", FontFactory.GetFont("Arial", 10, Font.BOLD)))
            table1.AddCell(celda76)
            Dim celda77 As New PdfPCell(New Phrase(preciocredito, FontFactory.GetFont("Arial", 9)))
            table1.AddCell(celda77)

            '==========================Tabla2==========================================
            Dim table As New PdfPTable(6)
            'actual width of table in points
            table.TotalWidth = 427.0F

            'fix the absolute width of the table
            table.LockedWidth = True
            'relative col widths in proportions
            Dim widths(5) As Integer
            widths(0) = 3.0F
            widths(1) = 2.0F
            widths(2) = 2.0F
            widths(3) = 1.0F
            widths(4) = 1.0F
            widths(5) = 1.5F
            table.SetWidths(widths)

            table.HorizontalAlignment = 0
            'leave a gap before and after the table
            table.SpacingBefore = 20.0F
            'table.SpacingAfter = 30f;

            Dim cell As New PdfPCell(New Phrase("DATOS DEL POSTULANTE", FontFactory.GetFont("Arial", 10, Font.BOLD)))
            cell.Colspan = 6
            table.AddCell(cell)
            Dim celda As New PdfPCell(New Phrase("Apellidos y Nombres", FontFactory.GetFont("Arial", 9)))
            table.AddCell(celda)
            Dim cell2 As New PdfPCell(New Phrase(alumno, FontFactory.GetFont("Arial", 9)))
            cell2.Colspan = 5
            table.AddCell(cell2)
            Dim celda2 As New PdfPCell(New Phrase("Fecha de Nacimiento", FontFactory.GetFont("Arial", 9)))
            table.AddCell(celda2)
            Dim celda3 As New PdfPCell(New Phrase(fechanacalu, FontFactory.GetFont("Arial", 9)))
            table.AddCell(celda3)
            Dim celda4 As New PdfPCell(New Phrase("Doc. de identidad", FontFactory.GetFont("Arial", 9)))
            table.AddCell(celda4)
            Dim celda5 As New PdfPCell(New Phrase(tipodocalu, FontFactory.GetFont("Arial", 9)))
            table.AddCell(celda5)
            Dim celda6 As New PdfPCell(New Phrase("Nº", FontFactory.GetFont("Arial", 9)))
            table.AddCell(celda6)
            Dim celda7 As New PdfPCell(New Phrase(nrodocalu, FontFactory.GetFont("Arial", 9)))
            table.AddCell(celda7)
            Dim celda8 As New PdfPCell(New Phrase("Nacionalidad", FontFactory.GetFont("Arial", 9)))
            table.AddCell(celda8)
            Dim cell3 As New PdfPCell(New Phrase(paisnacalu, FontFactory.GetFont("Arial", 9)))
            cell3.Colspan = 3
            table.AddCell(cell3)
            Dim celda9 As New PdfPCell(New Phrase("Sexo:", FontFactory.GetFont("Arial", 9)))
            table.AddCell(celda9)
            Dim celda10 As New PdfPCell(New Phrase("M (" & IIf(sexoalu = "M", "X", " ") & ") o F (" & IIf(sexoalu = "F", "X", " ") & ")", FontFactory.GetFont("Arial", 9)))
            table.AddCell(celda10)
            Dim celda11 As New PdfPCell(New Phrase("Lugar de nacimiento", FontFactory.GetFont("Arial", 9)))
            table.AddCell(celda11)

            Dim celda12 As New PdfPCell(New Phrase("Distr.: " & distritonacalu, FontFactory.GetFont("Arial", 9)))
            table.AddCell(celda12)
            Dim cell4 As New PdfPCell(New Phrase("Prov.: " & provincianacalu, FontFactory.GetFont("Arial", 9)))
            cell4.Colspan = 2
            table.AddCell(cell4)
            Dim cell5 As New PdfPCell(New Phrase("Dpto.: " & dptonacalu, FontFactory.GetFont("Arial", 9)))
            cell5.Colspan = 2
            table.AddCell(cell5)

            Dim cell6 As New PdfPCell(New Phrase("Domicilio", FontFactory.GetFont("Arial", 9)))
            cell6.Rowspan = 2
            table.AddCell(cell6)
            Dim cell7 As New PdfPCell(New Phrase("Dirección: " & direccionalu, FontFactory.GetFont("Arial", 9)))
            cell7.Colspan = 5
            table.AddCell(cell7)

            If tipodocalu = "DNI" Then
                Dim celda13 As New PdfPCell(New Phrase("Distr.: " & distritodomalu, FontFactory.GetFont("Arial", 9)))
                table.AddCell(celda13)
                Dim cell8 As New PdfPCell(New Phrase("Prov.: " & provinciadomalu, FontFactory.GetFont("Arial", 9)))
                cell8.Colspan = 2
                table.AddCell(cell8)
                Dim cell9 As New PdfPCell(New Phrase("Dpto.: " & dptodomalu, FontFactory.GetFont("Arial", 9)))
                cell9.Colspan = 2
                table.AddCell(cell9)
            Else
                Dim celda13 As New PdfPCell(New Phrase("Ciudad: " & ciudadalu, FontFactory.GetFont("Arial", 9)))
                celda13.Colspan = 5
                table.AddCell(celda13)
            End If

            Dim celda14 As New PdfPCell(New Phrase("Teléfono", FontFactory.GetFont("Arial", 9)))
            table.AddCell(celda14)
            Dim celda15 As New PdfPCell(New Phrase("Casa: " & tlfdomalu, FontFactory.GetFont("Arial", 9)))
            table.AddCell(celda15)
            Dim cell10 As New PdfPCell(New Phrase("Celular: " & celularalu, FontFactory.GetFont("Arial", 9)))
            cell10.Colspan = 2
            table.AddCell(cell10)
            Dim cell11 As New PdfPCell(New Phrase("Operador: " & operadoralu, FontFactory.GetFont("Arial", 9)))
            cell11.Colspan = 2
            table.AddCell(cell11)
            Dim celda16 As New PdfPCell(New Phrase("Correo electrónico", FontFactory.GetFont("Arial", 9)))
            table.AddCell(celda16)
            Dim cell12 As New PdfPCell(New Phrase(emailalu, FontFactory.GetFont("Arial", 9)))
            cell12.Colspan = 5
            table.AddCell(cell12)
            Dim celda17 As New PdfPCell(New Phrase("Tipo de discapacidad", FontFactory.GetFont("Arial", 9)))
            table.AddCell(celda17)
            Dim celda18 As New PdfPCell(New Phrase("Auditiva: (" & discapacidadaud & ")", FontFactory.GetFont("Arial", 9)))
            table.AddCell(celda18)
            Dim cell13 As New PdfPCell(New Phrase("Física: (" & discapacidadfis & ")", FontFactory.GetFont("Arial", 9)))
            cell13.Colspan = 2
            table.AddCell(cell13)
            Dim cell14 As New PdfPCell(New Phrase("Visual: (" & discapacidadvis & ")", FontFactory.GetFont("Arial", 9)))
            cell14.Colspan = 2
            table.AddCell(cell14)
            'Estudios secundarios
            Dim celda19 As New PdfPCell(New Phrase("DE LOS ESTUDIOS SECUNDARIOS REALIZADOS", FontFactory.GetFont("Arial", 10, Font.BOLD)))
            celda19.Colspan = 6
            table.AddCell(celda19)
            Dim celda20 As New PdfPCell(New Phrase("En Perú(" & IIf(tbl.Rows(0).Item("nombrepaiscolegio").ToString = "Perú", "X", " ") & ")", FontFactory.GetFont("Arial", 9)))
            table.AddCell(celda20)
            Dim celda21 As New PdfPCell(New Phrase("En el Extranj.:(" & IIf(tbl.Rows(0).Item("nombrepaiscolegio").ToString = "Perú" Or tbl.Rows(0).Item("nombrepaiscolegio").ToString = "", " ", "X") & ")", FontFactory.GetFont("Arial", 9)))
            table.AddCell(celda21)
            Dim celda22 As New PdfPCell(New Phrase("País", FontFactory.GetFont("Arial", 9)))
            celda22.Colspan = 2
            celda22.HorizontalAlignment = Element.ALIGN_RIGHT
            table.AddCell(celda22)
            Dim celda23 As New PdfPCell(New Phrase(IIf(tbl.Rows(0).Item("nombrepaiscolegio").ToString = "Perú" Or tbl.Rows(0).Item("nombrepaiscolegio").ToString = "", " ", tbl.Rows(0).Item("nombrepaiscolegio").ToString), FontFactory.GetFont("Arial", 9)))
            celda23.Colspan = 2
            table.AddCell(celda23)
            Dim celda24 As New PdfPCell(New Phrase("Colegio", FontFactory.GetFont("Arial", 9)))
            table.AddCell(celda24)
            Dim celda25 As New PdfPCell(New Phrase(colegio, FontFactory.GetFont("Arial", 9)))
            celda25.Colspan = 2
            table.AddCell(celda25)
            Dim celda26 As New PdfPCell(New Phrase("Promoción", FontFactory.GetFont("Arial", 9)))
            celda26.Colspan = 2
            table.AddCell(celda26)
            Dim celda27 As New PdfPCell(New Phrase("Nacional(" & IIf(tipocolegio = "Nacional", "X", " ") & ") Particular(" & IIf(tipocolegio = "Particular", "X", " ") & ")", FontFactory.GetFont("Arial", 9)))
            table.AddCell(celda27)
            Dim celda28 As New PdfPCell(New Phrase("Lugar", FontFactory.GetFont("Arial", 9)))
            celda28.Rowspan = 2
            table.AddCell(celda28)
            Dim celda29 As New PdfPCell(New Phrase(direccioncolegio, FontFactory.GetFont("Arial", 9)))
            celda29.Colspan = 5
            table.AddCell(celda29)
            Dim celda30 As New PdfPCell(New Phrase("Distr.: " & distritocolegio, FontFactory.GetFont("Arial", 9)))
            table.AddCell(celda30)
            Dim celda31 As New PdfPCell(New Phrase("Prov.: " & provinciacolegio, FontFactory.GetFont("Arial", 9)))
            celda31.Colspan = 2
            table.AddCell(celda31)
            Dim celda32 As New PdfPCell(New Phrase("Dpto.: " & depcolegio, FontFactory.GetFont("Arial", 9)))
            celda32.Colspan = 2
            table.AddCell(celda32)
            'Datos del padre o madre
            Dim celda33 As New PdfPCell(New Phrase("DATOS DEL PADRE ( ) O MADRE ( )", FontFactory.GetFont("Arial", 10, Font.BOLD)))
            celda33.Colspan = 6
            table.AddCell(celda33)
            Dim celda34 As New PdfPCell(New Phrase("Apellidos y Nombres", FontFactory.GetFont("Arial", 9)))
            table.AddCell(celda34)
            Dim celda35 As New PdfPCell(New Phrase(nombresfam, FontFactory.GetFont("Arial", 9)))
            celda35.Colspan = 5
            table.AddCell(celda35)
            Dim celda36 As New PdfPCell(New Phrase("Domicilio", FontFactory.GetFont("Arial", 9)))
            celda36.Rowspan = 2
            table.AddCell(celda36)
            Dim celda37 As New PdfPCell(New Phrase(direccionfam, FontFactory.GetFont("Arial", 9)))
            celda37.Colspan = 5
            table.AddCell(celda37)
            Dim celda38 As New PdfPCell(New Phrase("Distr.: " & distritofam, FontFactory.GetFont("Arial", 9)))
            table.AddCell(celda38)
            Dim celda39 As New PdfPCell(New Phrase("Prov.: " & provinciafam, FontFactory.GetFont("Arial", 9)))
            celda39.Colspan = 2
            table.AddCell(celda39)
            Dim celda40 As New PdfPCell(New Phrase("Dpto.: " & dptofam, FontFactory.GetFont("Arial", 9)))
            celda40.Colspan = 2
            table.AddCell(celda40)
            Dim celda41 As New PdfPCell(New Phrase("Teléfono", FontFactory.GetFont("Arial", 9)))
            table.AddCell(celda41)
            Dim celda42 As New PdfPCell(New Phrase("Casa: " & tlfcasafam, FontFactory.GetFont("Arial", 9)))
            table.AddCell(celda42)
            Dim celda43 As New PdfPCell(New Phrase("Oficina: " & tlfoffam, FontFactory.GetFont("Arial", 9)))
            table.AddCell(celda43)
            Dim celda44 As New PdfPCell(New Phrase("Celular: " & celfam, FontFactory.GetFont("Arial", 9)))
            celda44.Colspan = 2
            table.AddCell(celda44)
            Dim celda45 As New PdfPCell(New Phrase("Operador: " & operadorfam, FontFactory.GetFont("Arial", 9)))
            table.AddCell(celda45)
            Dim celda46 As New PdfPCell(New Phrase("Correo electrónico", FontFactory.GetFont("Arial", 9)))
            table.AddCell(celda46)
            Dim celda47 As New PdfPCell(New Phrase(emailfam, FontFactory.GetFont("Arial", 9)))
            celda47.Colspan = 5
            table.AddCell(celda47)
            'Datos de apoderado
            Dim celda48 As New PdfPCell(New Phrase("DATOS DEL RESPONSABLE DE PAGO", FontFactory.GetFont("Arial", 10, Font.BOLD)))
            celda48.Colspan = 6
            table.AddCell(celda48)
            Dim celda349 As New PdfPCell(New Phrase("Apellidos y Nombres", FontFactory.GetFont("Arial", 9)))
            table.AddCell(celda349)
            Dim celda50 As New PdfPCell(New Phrase(nombresapo, FontFactory.GetFont("Arial", 9)))
            celda50.Colspan = 5
            table.AddCell(celda50)
            Dim celda51 As New PdfPCell(New Phrase("Domicilio", FontFactory.GetFont("Arial", 9)))
            celda51.Rowspan = 2
            table.AddCell(celda51)
            Dim celda52 As New PdfPCell(New Phrase(direccionapo, FontFactory.GetFont("Arial", 9)))
            celda52.Colspan = 5
            table.AddCell(celda52)
            Dim celda53 As New PdfPCell(New Phrase("Distr.: " & distritoapo, FontFactory.GetFont("Arial", 9)))
            table.AddCell(celda53)
            Dim celda54 As New PdfPCell(New Phrase("Prov.: " & provinciaapo, FontFactory.GetFont("Arial", 9)))
            celda54.Colspan = 2
            table.AddCell(celda54)
            Dim celda55 As New PdfPCell(New Phrase("Dpto.: " & dptoapo, FontFactory.GetFont("Arial", 9)))
            celda55.Colspan = 2
            table.AddCell(celda55)
            Dim celda56 As New PdfPCell(New Phrase("Teléfono", FontFactory.GetFont("Arial", 9)))
            table.AddCell(celda56)
            Dim celda57 As New PdfPCell(New Phrase("Casa: " & tlfcasaapo, FontFactory.GetFont("Arial", 9)))
            table.AddCell(celda57)
            Dim celda58 As New PdfPCell(New Phrase("Oficina: " & tlfofapo, FontFactory.GetFont("Arial", 9)))
            table.AddCell(celda58)
            Dim celda59 As New PdfPCell(New Phrase("Celular: " & celapo, FontFactory.GetFont("Arial", 9)))
            celda59.Colspan = 2
            table.AddCell(celda59)
            Dim celda60 As New PdfPCell(New Phrase("Operador: " & operadorapo, FontFactory.GetFont("Arial", 9)))
            table.AddCell(celda60)

            Dim celda61 As New PdfPCell(New Phrase("Correo electrónico", FontFactory.GetFont("Arial", 9)))
            table.AddCell(celda61)
            Dim celda62 As New PdfPCell(New Phrase(emailapo, FontFactory.GetFont("Arial", 9)))
            celda62.Colspan = 5
            table.AddCell(celda62)

            'Solo se muestra la fila "Observación (Cargo)" para el tipo de estudios INGLÉS
            If codigoTest = "4" Then
                Dim celda63 As New PdfPCell(New Phrase("Observación (Cargo)", FontFactory.GetFont("Arial", 9)))
                table.AddCell(celda63)

                Dim celda64 As New PdfPCell(New Phrase(observacionDeuda, FontFactory.GetFont("Arial", 9)))
                celda64.Colspan = 5
                table.AddCell(celda64)

                documento.SetMargins(75, 85, 50, 50)
                table.SpacingBefore = 10.0F
                table1.SpacingBefore = 10.0F
            End If
            

            '==============================   PDF     =================================
            'Paso a html
            'Dim se As New StringReader(textohtml)
            Dim obj As New HTMLWorker(documento)

            'Roothpath
            Dim rootPath As String = Server.MapPath("~")

            'Seteo estilo para html
            Dim style As New StyleSheet
            style.LoadTagStyle(HtmlTags.P, HtmlTags.SIZE, "9pt")
            'style.LoadTagStyle(HtmlTags.P, HtmlTags.FACE, "Belwe") 'interlineado
            style.LoadTagStyle(HtmlTags.P, HtmlTags.LEADING, "10")
            obj.SetStyleSheet(style)

            'Defino parrafos para la firma
            parrafo1.Alignment = Element.ALIGN_RIGHT
            parrafo2.Alignment = Element.ALIGN_RIGHT
            parrafo3.Alignment = Element.ALIGN_CENTER
            parrafo35.Alignment = Element.ALIGN_JUSTIFIED
            parrafo36.Alignment = Element.ALIGN_JUSTIFIED
            parrafo4.Alignment = Element.ALIGN_RIGHT
            parrafo5.Alignment = Element.ALIGN_CENTER
            parrafo6.Alignment = Element.ALIGN_CENTER            
            parrafo1.Font = FontFactory.GetFont("Arial", 10)
            parrafo2.Font = FontFactory.GetFont("Arial", 10)
            parrafo3.Font = FontFactory.GetFont("Arial", 10, Font.BOLD)
            parrafo35.Font = FontFactory.GetFont("Arial", 8)
            parrafo36.Font = FontFactory.GetFont("Arial", 8)
            parrafo4.Font = FontFactory.GetFont("Arial", 9)
            parrafo5.Font = FontFactory.GetFont("Arial", 9)
            parrafo6.Font = FontFactory.GetFont("Arial", 9)


            'parrafo.Font = FontFactory.GetFont("Belwe", 13, iTextSharp.text.Font.BOLD)                
            'parrafo.IndentationLeft
            parrafo1.Add("PROCESO DE ADMISIÓN " & cicloingreso) 'Texto que se insertara
            'parrafo2.Add(cicloingreso & "           ") 'Texto que se insertara
            parrafo3.Add("FICHA DE DATOS") 'Texto que se insertara
            parrafo35.Add("Los estudiantes que no concretaron su proceso de matrícula deben solicitar la devolución de sus documentos en un plazo máximo de 30 días, de lo contrario USAT no se hace responsable por la entrega de los mismos. El programa se abre con un número mínimo de 15 participantes.") 'Texto que se insertara
            parrafo36.Add("El costo de crédito indicado, se calcula en base al colegio de procedencia") 'Texto que se insertara
            parrafo4.Add("Chiclayo, " & dia & " de " & mes & " de " & año) 'Texto que se insertara
            parrafo5.Add("______________________________") 'Texto que se insertara
            parrafo6.Add("Firma del Postulante") 'Texto que se insertara

            'Apertura del documento
            documento.Open()
            'cb = writer.DirectContent

            'Seteamos el tipo de letra y el tamaño.
            'cb.SetFontAndSize(customfont, 12)

            Dim oImagen As iTextSharp.text.Image

            oImagen = iTextSharp.text.Image.GetInstance(rootPath & "\Images\LogoOficial.png")
            oImagen.ScaleAbsoluteWidth(130)
            oImagen.ScaleAbsoluteHeight(75)
            oImagen.Alignment = Element.ALIGN_LEFT            
            documento.Add(oImagen)
            documento.Add(parrafo1)
            documento.Add(parrafo2)
            documento.Add(parrafo3)
            documento.Add(table1)
            documento.Add(table)
            documento.Add(parrafo35)
            documento.Add(parrafo36)

            documento.Add(parrafo4)
            documento.Add(New Paragraph(" ")) 'Salto de linea
            documento.Add(New Paragraph(" ")) 'Salto de linea

            documento.Add(parrafo5)
            documento.Add(parrafo6)
            'obj.Parse(se)

            'Abrimos una nueva página
            documento.NewPage()

            'Iniciamos el flujo de bytes.
            'cb.BeginText()
            '------------------------------------------------------------------------------
        Else

        End If


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
        Response.AddHeader("content-disposition", "attachment; filename=ficha.pdf")

        Response.ContentType = "application/pdf"
        Response.Buffer = True
        Response.OutputStream.Write(ms.GetBuffer(), 0, ms.GetBuffer().Length)
        Response.OutputStream.Flush()
        Response.End()
        '------------------------------------------------------------------------------
        'ConvertHtmlStringToPDF(textohtml)
    End Sub


End Class
