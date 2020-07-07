Imports iTextSharp.text
Imports System.IO
Imports iTextSharp.text.pdf

Partial Class administrativo_pec_test_frmFichaInscripcionPecPDF
    Inherits System.Web.UI.Page

#Region "Variables"
    Private mo_RepoAdmision As New ClsAdmision
    Private mo_FontSmall As Font = FontFactory.GetFont("Arial", 8)
    Private mo_FontSmallB As Font = FontFactory.GetFont("Arial", 8, Font.BOLD)
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

                Dim ls_CodigoAlu As String = Request.Form("alu")
                GenerarFichaInscripcion(ls_CodigoAlu)
            End If

        Catch ex As Exception
            errorMenssage.InnerHtml = ex.Message
        End Try
    End Sub
#End Region

#Region "Metodos"
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
            doc.SetMargins(30.0F, 30.0F, 30.0F, 30.0F)
            writer = PdfWriter.GetInstance(doc, ms)

            doc.Open()

            Dim tblLayout As New PdfPTable(12)
            With tblLayout
                .WidthPercentage = 100
            End With

            Dim cellEmpty As New PdfPCell()
            cellEmpty.Colspan = 12
            cellEmpty.Border = Rectangle.NO_BORDER

            'Logo
            Dim rootPath As String = Server.MapPath("~")
            Dim oImagen As iTextSharp.text.Image = iTextSharp.text.Image.GetInstance(rootPath & "\Images\LogoOficial.png")
            oImagen.ScaleAbsoluteWidth(110)

            Dim cellLogo As New PdfPCell
            cellLogo.Border = Rectangle.NO_BORDER
            cellLogo.Colspan = 6
            cellLogo.AddElement(oImagen)
            tblLayout.AddCell(cellLogo)

            Dim tblCabecera As PdfPTable = TablaCabecera(lo_DataRow)
            Dim cellCabecera As New PdfPCell
            cellCabecera.Border = Rectangle.NO_BORDER
            cellCabecera.Colspan = 6
            cellCabecera.AddElement(tblCabecera)
            tblLayout.AddCell(cellCabecera)

            Dim tblModalidad As PdfPTable = TablaModalidad(lo_DataRow)
            Dim cellModalidad As New PdfPCell
            cellModalidad.Border = Rectangle.NO_BORDER
            cellModalidad.Colspan = 6
            cellModalidad.AddElement(tblModalidad)
            tblLayout.AddCell(cellModalidad)

            Dim tblCarrera As PdfPTable = TablaCarrera(lo_DataRow)
            Dim cellCarrera As New PdfPCell
            cellCarrera.Border = Rectangle.NO_BORDER
            cellCarrera.Colspan = 6
            cellCarrera.AddElement(tblCarrera)
            tblLayout.AddCell(cellCarrera)
            tblLayout.AddCell(cellEmpty) : tblLayout.AddCell(cellEmpty)

            Dim tblInfoPersonal As PdfPTable = TablaInfoPersonal(lo_DataRow)
            Dim cellInfoPersonal As New PdfPCell
            cellInfoPersonal.Colspan = 12
            cellInfoPersonal.AddElement(tblInfoPersonal)
            tblLayout.AddCell(cellInfoPersonal)
            tblLayout.AddCell(cellEmpty) : tblLayout.AddCell(cellEmpty)

            Dim tblInfoLaboral As PdfPTable = TablaInfoLaboral(lo_DataRow)
            Dim cellInfoLaboral As New PdfPCell
            cellInfoLaboral.Colspan = 12
            cellInfoLaboral.AddElement(tblInfoLaboral)
            tblLayout.AddCell(cellInfoLaboral)
            tblLayout.AddCell(cellEmpty) : tblLayout.AddCell(cellEmpty)

            'Dim tblInfoEducativa As PdfPTable = TablaInfoEducativa(lo_DataRow)
            'Dim cellInfoEducativa As New PdfPCell
            'cellInfoEducativa.Colspan = 12
            'cellInfoEducativa.AddElement(tblInfoEducativa)
            'tblLayout.AddCell(cellInfoEducativa)
            'tblLayout.AddCell(cellEmpty) : tblLayout.AddCell(cellEmpty)

            'If Not String.IsNullOrEmpty(lo_DataRow.Item("numeroDocIdentPadre_fam")) Then
            '    Dim tblInfoPadre As PdfPTable = TablaInfoPadre(lo_DataRow)
            '    Dim cellInfoPadre As New PdfPCell
            '    cellInfoPadre.Colspan = 12
            '    cellInfoPadre.AddElement(tblInfoPadre)
            '    tblLayout.AddCell(cellInfoPadre)
            '    tblLayout.AddCell(cellEmpty) : tblLayout.AddCell(cellEmpty)
            'End If

            'If Not String.IsNullOrEmpty(lo_DataRow.Item("numeroDocIdentMadre_fam")) Then
            '    Dim tblInfoMadre As PdfPTable = TablaInfoMadre(lo_DataRow)
            '    Dim cellInfoMadre As New PdfPCell
            '    cellInfoMadre.Colspan = 12
            '    cellInfoMadre.AddElement(tblInfoMadre)
            '    tblLayout.AddCell(cellInfoMadre)
            '    tblLayout.AddCell(cellEmpty) : tblLayout.AddCell(cellEmpty)
            'End If

            'If Not String.IsNullOrEmpty(lo_DataRow.Item("numeroDocIdentApod_fam")) Then
            '    Dim tblInfoApod As PdfPTable = TablaInfoApoderado(lo_DataRow)
            '    Dim cellInfoApod As New PdfPCell
            '    cellInfoApod.Colspan = 12
            '    cellInfoApod.AddElement(tblInfoApod)
            '    tblLayout.AddCell(cellInfoApod)
            'End If

            doc.Add(tblLayout)

            doc.Close()

            Response.Clear()
            Response.AddHeader("content-disposition", "attachment; filename=ficha.pdf")
            Response.ContentType = "application/pdf"
            Response.Buffer = True
            Response.OutputStream.Write(ms.GetBuffer(), 0, ms.GetBuffer().Length)
            Response.OutputStream.Flush()
            Response.End()

        Catch ex As Exception
            With respuestaPostback
                .Attributes.Item("data-rpta") = "-1"
                .Attributes.Item("data-msg") = ex.Message
            End With
        End Try
    End Sub

    Private Function TablaCabecera(ByVal lo_DataRow As Data.DataRow) As PdfPTable
        Dim table As New PdfPTable(12)
        With table
            .TotalWidth = 250.0F
            .LockedWidth = True
            .DefaultCell.Border = Rectangle.NO_BORDER
            .HorizontalAlignment = 2 'alienacion derecha
        End With

        'Fecha
        Dim cellLblFecha As New PdfPCell(New Phrase("FECHA:", mo_FontSmallB))
        cellLblFecha.Colspan = 9
        cellLblFecha.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
        cellLblFecha.Border = Rectangle.NO_BORDER
        table.AddCell(cellLblFecha)

        Dim cellValFecha As New PdfPCell(New Phrase(DateTime.Now.ToString("dd/MM/yyyy"), mo_FontSmall))
        cellValFecha.Colspan = 3
        cellValFecha.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
        cellValFecha.Border = Rectangle.NO_BORDER
        table.AddCell(cellValFecha)

        'Proceso de admisión
        Dim cellLilProcAdmi As New PdfPCell(New Phrase("PROCESO DE ADMISIÓN:", mo_FontSmallB))
        cellLilProcAdmi.Colspan = 9
        cellLilProcAdmi.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
        cellLilProcAdmi.Border = Rectangle.NO_BORDER
        table.AddCell(cellLilProcAdmi)

        Dim cellValProcAdmi As New PdfPCell(New Phrase(lo_DataRow.Item("descripcion_Cac"), mo_FontSmall))
        cellValProcAdmi.Colspan = 3
        cellValProcAdmi.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
        cellValProcAdmi.Border = Rectangle.NO_BORDER
        table.AddCell(cellValProcAdmi)

        'Título
        Dim cellTitulo As PdfPCell = GenerarTitulo("FICHA DE INSCRIPCIÓN")
        cellTitulo.Colspan = 12
        table.AddCell(cellTitulo)

        'Código y costo de crédito
        Dim cellLblCodEst As New PdfPCell(New Phrase("Código de Estudiante", mo_FontSmallB))
        cellLblCodEst.Colspan = 7
        cellLblCodEst.HorizontalAlignment = PdfPCell.ALIGN_CENTER
        table.AddCell(cellLblCodEst)

        'Dim cellLblCostCred As New PdfPCell(New Phrase("Costo de Crédito", mo_FontSmallB))
        'cellLblCostCred.Colspan = 5
        'cellLblCostCred.HorizontalAlignment = PdfPCell.ALIGN_CENTER
        'table.AddCell(cellLblCostCred)

        Dim cellValCodEst As New PdfPCell(New Phrase(lo_DataRow.Item("codigoUniver_Alu"), mo_FontSmall))
        cellValCodEst.Colspan = 7
        cellValCodEst.HorizontalAlignment = PdfPCell.ALIGN_CENTER
        table.AddCell(cellValCodEst)

        'Dim cellValCostCred As New PdfPCell(New Phrase(Decimal.Parse(lo_DataRow.Item("precioCreditoAct_Alu")).ToString("N2"), mo_FontSmall))
        'cellValCostCred.Colspan = 5
        'cellValCostCred.HorizontalAlignment = PdfPCell.ALIGN_CENTER
        'table.AddCell(cellValCostCred)

        Return table
    End Function

    Private Function TablaModalidad(ByVal lo_DataRow As Data.DataRow) As PdfPTable
        Dim table As New PdfPTable(1)
        With table
            .WidthPercentage = 100
        End With

        Dim cellTitulo As PdfPCell = GenerarSubTitulo("MODALIDAD")
        table.AddCell(cellTitulo)

        Dim cellValModalidad As PdfPCell = New PdfPCell(New Phrase(lo_DataRow.Item("nombre_Min"), mo_FontSmall))
        cellValModalidad.HorizontalAlignment = PdfPCell.ALIGN_CENTER
        table.AddCell(cellValModalidad)

        Return table
    End Function

    Private Function TablaCarrera(ByVal lo_DataRow As Data.DataRow) As PdfPTable
        Dim table As New PdfPTable(1)
        With table
            .WidthPercentage = 100
        End With

        Dim cellTitulo As PdfPCell = GenerarSubTitulo("PROGRAMA")
        table.AddCell(cellTitulo)

        Dim cellValCarrera As PdfPCell = New PdfPCell(New Phrase(lo_DataRow.Item("nombre_Cpf"), mo_FontSmall))
        cellValCarrera.HorizontalAlignment = PdfPCell.ALIGN_CENTER
        table.AddCell(cellValCarrera)

        Return table
    End Function

    Private Function TablaInfoPersonal(ByVal lo_DataRow As Data.DataRow) As PdfPTable
        Dim cellEmptySpace As New PdfPCell
        Dim table As New PdfPTable(20)
        With table
            .WidthPercentage = 100
        End With

        Dim cellTitulo As PdfPCell = GenerarTitulo("INFORMACIÓN PERSONAL")
        cellTitulo.Colspan = 8
        cellTitulo.Border = Rectangle.NO_BORDER
        table.AddCell(cellTitulo)

        cellEmptySpace.Colspan = 12
        cellEmptySpace.Border = Rectangle.NO_BORDER
        table.AddCell(cellEmptySpace)

        '-------------------------------------------------------------------------

        'Apellido Paterno
        Dim cellLblApePat As New PdfPCell(New Phrase("Ape. Paterno:", mo_FontSmallB))
        With cellLblApePat
            .Colspan = 3
            .Border = Rectangle.NO_BORDER
            .PaddingTop = 4
            .PaddingBottom = 4
        End With
        table.AddCell(cellLblApePat)

        Dim cellValApePat As New PdfPCell(New Phrase(lo_DataRow.Item("apellidoPat_Alu"), mo_FontSmall))
        With cellValApePat
            .Colspan = 7
            .Border = Rectangle.NO_BORDER
            .PaddingTop = 4
            .PaddingBottom = 4
        End With
        table.AddCell(cellValApePat)

        'Apellido Materno
        Dim cellLblApeMat As New PdfPCell(New Phrase("Ape. Materno:", mo_FontSmallB))
        With cellLblApeMat
            .Colspan = 3
            .Border = Rectangle.NO_BORDER
            .PaddingTop = 4
            .PaddingBottom = 4
        End With
        table.AddCell(cellLblApeMat)

        Dim cellValApeMat As New PdfPCell(New Phrase(lo_DataRow.Item("apellidoMat_Alu"), mo_FontSmall))
        With cellValApeMat
            .Colspan = 7
            .Border = Rectangle.NO_BORDER
            .PaddingTop = 4
            .PaddingBottom = 4
        End With
        table.AddCell(cellValApeMat)

        '-------------------------------------------------------------------------

        'Nombres
        Dim cellLblNombres As New PdfPCell(New Phrase("Nombres:", mo_FontSmallB))
        With cellLblNombres
            .Colspan = 3
            .Border = Rectangle.NO_BORDER
            .PaddingTop = 4
            .PaddingBottom = 4
        End With
        table.AddCell(cellLblNombres)

        Dim cellValNombres As New PdfPCell(New Phrase(lo_DataRow.Item("nombres_Alu"), mo_FontSmall))
        With cellValNombres
            .Colspan = 10
            .Border = Rectangle.NO_BORDER
            .PaddingTop = 4
            .PaddingBottom = 4
        End With
        table.AddCell(cellValNombres)

        'Fecha de Nacimiento
        Dim cellLblFecNacimiento As New PdfPCell(New Phrase("Fech. Nacimiento:", mo_FontSmallB))
        With cellLblFecNacimiento
            .Colspan = 3
            .Border = Rectangle.NO_BORDER
            .PaddingTop = 4
            .PaddingBottom = 4
        End With
        table.AddCell(cellLblFecNacimiento)

        Dim cellValFecNacimiento As New PdfPCell(New Phrase(Date.Parse(lo_DataRow.Item("fechaNacimiento_Alu")).ToString("dd/MM/yyyy"), mo_FontSmall))
        With cellValFecNacimiento
            .Colspan = 2
            .Border = Rectangle.NO_BORDER
            .PaddingTop = 4
            .PaddingBottom = 4
        End With
        table.AddCell(cellValFecNacimiento)

        'Sexo
        Dim cellLblSexo As New PdfPCell(New Phrase("Sexo:", mo_FontSmallB))
        With cellLblSexo
            .Colspan = 1
            .Border = Rectangle.NO_BORDER
            .PaddingTop = 4
            .PaddingBottom = 4
        End With
        table.AddCell(cellLblSexo)

        Dim cellValSexo As New PdfPCell(New Phrase(lo_DataRow.Item("sexo_Alu"), mo_FontSmall))
        With cellValSexo
            .Colspan = 1
            .Border = Rectangle.NO_BORDER
            .HorizontalAlignment = PdfPCell.ALIGN_CENTER
            .PaddingTop = 4
            .PaddingBottom = 4
        End With
        table.AddCell(cellValSexo)

        '-------------------------------------------------------------------------

        'Tipo de Documento 
        Dim cellLblTipoDoc As New PdfPCell(New Phrase("Tipo Documento:", mo_FontSmallB))
        With cellLblTipoDoc
            .Colspan = 3
            .Border = Rectangle.NO_BORDER
            .PaddingTop = 4
            .PaddingBottom = 4
        End With
        table.AddCell(cellLblTipoDoc)

        Dim cellValTipoDocumento As New PdfPCell(New Phrase(lo_DataRow.Item("tipoDocIdent_Alu"), mo_FontSmall))
        With cellValTipoDocumento
            .Colspan = 3
            .Border = Rectangle.NO_BORDER
            .PaddingTop = 4
            .PaddingBottom = 4
        End With
        table.AddCell(cellValTipoDocumento)

        'Nro de Documento 
        Dim cellLblNroDocumento As New PdfPCell(New Phrase("N°:", mo_FontSmallB))
        With cellLblNroDocumento
            .Colspan = 1
            .Border = Rectangle.NO_BORDER
            .PaddingTop = 4
            .PaddingBottom = 4
        End With
        table.AddCell(cellLblNroDocumento)

        Dim cellValNroDocumento As New PdfPCell(New Phrase(lo_DataRow.Item("nroDocIdent_Alu"), mo_FontSmall))
        With cellValNroDocumento
            .Colspan = 3
            .Border = Rectangle.NO_BORDER
            .PaddingTop = 4
            .PaddingBottom = 4
        End With
        table.AddCell(cellValNroDocumento)

        'Nacionalidad
        Dim cellLblNacionalidad As New PdfPCell(New Phrase("Nacionalidad:", mo_FontSmallB))
        With cellLblNacionalidad
            .Colspan = 3
            .Border = Rectangle.NO_BORDER
            .PaddingTop = 4
            .PaddingBottom = 4
        End With
        table.AddCell(cellLblNacionalidad)

        Dim cellValNacionalidad As New PdfPCell(New Phrase(lo_DataRow.Item("nombre_PaiNac"), mo_FontSmall))
        With cellValNacionalidad
            .Colspan = 7
            .Border = Rectangle.NO_BORDER
            .PaddingTop = 4
            .PaddingBottom = 4
        End With
        table.AddCell(cellValNacionalidad)

        '-------------------------------------------------------------------------

        'Lugar de Nacimiento
        Dim cellLblLugNacimiento As New PdfPCell(New Phrase("Lugar de Nacimiento:", mo_FontSmallB))
        With cellLblLugNacimiento
            .Colspan = 4
            .Border = Rectangle.NO_BORDER
            .PaddingTop = 4
            .PaddingBottom = 4
        End With
        table.AddCell(cellLblLugNacimiento)

        Dim cellLblLugNacDepartamento As New PdfPCell(New Phrase("Dpto.", mo_FontSmallB))
        With cellLblLugNacDepartamento
            .Colspan = 1
            .Border = Rectangle.NO_BORDER
            .PaddingTop = 4
            .PaddingBottom = 4
        End With
        table.AddCell(cellLblLugNacDepartamento)

        Dim cellValLugNacDepartamento As New PdfPCell(New Phrase(lo_DataRow.Item("nombre_DepNac"), mo_FontSmall))
        With cellValLugNacDepartamento
            .Colspan = 4
            .Border = Rectangle.NO_BORDER
            .PaddingTop = 4
            .PaddingBottom = 4
        End With
        table.AddCell(cellValLugNacDepartamento)

        Dim cellLblLugNacProvincia As New PdfPCell(New Phrase("Prov.", mo_FontSmallB))
        With cellLblLugNacProvincia
            .Colspan = 1
            .Border = Rectangle.NO_BORDER
            .PaddingTop = 4
            .PaddingBottom = 4
        End With
        table.AddCell(cellLblLugNacProvincia)

        Dim cellValLugNacProvincia As New PdfPCell(New Phrase(lo_DataRow.Item("nombre_ProNac"), mo_FontSmall))
        With cellValLugNacProvincia
            .Colspan = 4
            .Border = Rectangle.NO_BORDER
            .PaddingTop = 4
            .PaddingBottom = 4
        End With
        table.AddCell(cellValLugNacProvincia)

        Dim cellLblLugNacDistrito As New PdfPCell(New Phrase("Distr.", mo_FontSmallB))
        With cellLblLugNacDistrito
            .Colspan = 1
            .Border = Rectangle.NO_BORDER
            .PaddingTop = 4
            .PaddingBottom = 4
        End With
        table.AddCell(cellLblLugNacDistrito)

        Dim cellValLugNacDistrito As New PdfPCell(New Phrase(lo_DataRow.Item("nombre_DisNac"), mo_FontSmall))
        With cellValLugNacDistrito
            .Colspan = 7
            .Border = Rectangle.NO_BORDER
            .PaddingTop = 4
            .PaddingBottom = 4
        End With
        table.AddCell(cellValLugNacDistrito)

        '-------------------------------------------------------------------------

        'Dirección Actual
        Dim cellLblDirActual As New PdfPCell(New Phrase("Dirección Actual:", mo_FontSmallB))
        With cellLblDirActual
            .Colspan = 3
            .Border = Rectangle.NO_BORDER
            .PaddingTop = 4
            .PaddingBottom = 4
        End With
        table.AddCell(cellLblDirActual)

        Dim cellValDirActual As New PdfPCell(New Phrase(lo_DataRow.Item("direccion_Dal"), mo_FontSmall))
        With cellValDirActual
            .Colspan = 5
            .Border = Rectangle.NO_BORDER
            .PaddingTop = 4
            .PaddingBottom = 4
        End With

        table.AddCell(cellValDirActual)

        Dim cellLblDirActDepartamento As New PdfPCell(New Phrase("Dpto.", mo_FontSmallB))
        With cellLblDirActDepartamento
            .Colspan = 1
            .Border = Rectangle.NO_BORDER
            .PaddingTop = 4
            .PaddingBottom = 4
        End With
        table.AddCell(cellLblDirActDepartamento)

        Dim cellValDirActDepartamento As New PdfPCell(New Phrase(lo_DataRow.Item("nombre_Dep"), mo_FontSmall))
        With cellValDirActDepartamento
            .Colspan = 3
            .Border = Rectangle.NO_BORDER
            .PaddingTop = 4
            .PaddingBottom = 4
        End With
        table.AddCell(cellValDirActDepartamento)

        Dim cellLblDirActProvincia As New PdfPCell(New Phrase("Prov.", mo_FontSmallB))
        With cellLblDirActProvincia
            .Colspan = 1
            .Border = Rectangle.NO_BORDER
            .PaddingTop = 4
            .PaddingBottom = 4
        End With
        table.AddCell(cellLblDirActProvincia)

        Dim cellValDirActProvincia As New PdfPCell(New Phrase(lo_DataRow.Item("nombre_Pro"), mo_FontSmall))
        With cellValDirActProvincia
            .Colspan = 3
            .Border = Rectangle.NO_BORDER
            .PaddingTop = 4
            .PaddingBottom = 4
        End With
        table.AddCell(cellValDirActProvincia)

        Dim cellLblDirActDistrito As New PdfPCell(New Phrase("Distr.", mo_FontSmallB))
        With cellLblDirActDistrito
            .Colspan = 1
            .Border = Rectangle.NO_BORDER
            .PaddingTop = 4
            .PaddingBottom = 4
        End With
        table.AddCell(cellLblDirActDistrito)

        Dim cellValDirActDistrito As New PdfPCell(New Phrase(lo_DataRow.Item("nombre_Dis"), mo_FontSmall))
        With cellValDirActDistrito
            .Colspan = 3
            .Border = Rectangle.NO_BORDER
            .PaddingTop = 4
            .PaddingBottom = 4
        End With
        table.AddCell(cellValDirActDistrito)

        '-------------------------------------------------------------------------

        Dim cellLblTelefono As New PdfPCell(New Phrase("Teléfono:", mo_FontSmallB))
        With cellLblTelefono
            .Colspan = 3
            .Border = Rectangle.NO_BORDER
            .PaddingTop = 4
            .PaddingBottom = 4
        End With
        table.AddCell(cellLblTelefono)

        Dim cellLblTelCasa As New PdfPCell(New Phrase("Casa:", mo_FontSmallB))
        With cellLblTelCasa
            .Colspan = 2
            .Border = Rectangle.NO_BORDER
            .PaddingTop = 4
            .PaddingBottom = 4
        End With
        table.AddCell(cellLblTelCasa)

        Dim cellValTelCasa As New PdfPCell(New Phrase(lo_DataRow.Item("telefonoCasa_Dal"), mo_FontSmall))
        With cellValTelCasa
            .Colspan = 2
            .Border = Rectangle.NO_BORDER
            .PaddingTop = 4
            .PaddingBottom = 4
        End With
        table.AddCell(cellValTelCasa)

        Dim cellLblCelular As New PdfPCell(New Phrase("Celular:", mo_FontSmallB))
        With cellLblCelular
            .Colspan = 2
            .Border = Rectangle.NO_BORDER
            .PaddingTop = 4
            .PaddingBottom = 4
        End With
        table.AddCell(cellLblCelular)

        Dim cellValCelular As New PdfPCell(New Phrase(lo_DataRow.Item("telefonoMovil_Dal"), mo_FontSmall))
        With cellValCelular
            .Colspan = 3
            .Border = Rectangle.NO_BORDER
            .PaddingTop = 4
            .PaddingBottom = 4
        End With
        table.AddCell(cellValCelular)

        Dim cellLblOperador As New PdfPCell(New Phrase("Operador:", mo_FontSmallB))
        With cellLblOperador
            .Colspan = 2
            .Border = Rectangle.NO_BORDER
            .PaddingTop = 4
            .PaddingBottom = 4
        End With
        table.AddCell(cellLblOperador)

        Dim cellValOperador As New PdfPCell(New Phrase(lo_DataRow.Item("OperadorMovil_Dal"), mo_FontSmall))
        With cellValOperador
            .Colspan = 6
            .Border = Rectangle.NO_BORDER
            .PaddingTop = 4
            .PaddingBottom = 4
        End With
        table.AddCell(cellValOperador)
        '-------------------------------------------------------------------------

        Dim cellLblEmail As New PdfPCell(New Phrase("Email:", mo_FontSmallB))
        With cellLblEmail
            .Colspan = 3
            .Border = Rectangle.NO_BORDER
            .PaddingTop = 4
            .PaddingBottom = 4
        End With
        table.AddCell(cellLblEmail)

        Dim cellValEmail As New PdfPCell(New Phrase(lo_DataRow.Item("eMail_Alu"), mo_FontSmall))
        With cellValEmail
            .Colspan = 8
            .Border = Rectangle.NO_BORDER
            .PaddingTop = 4
            .PaddingBottom = 4
        End With
        table.AddCell(cellValEmail)

        Dim cellLblEmail1 As New PdfPCell(New Phrase("Email Alternativo:", mo_FontSmallB))
        With cellLblEmail1
            .Colspan = 3
            .Border = Rectangle.NO_BORDER
            .PaddingTop = 4
            .PaddingBottom = 4
        End With
        table.AddCell(cellLblEmail1)

        Dim cellValEmail1 As New PdfPCell(New Phrase(lo_DataRow.Item("eMail_Alu"), mo_FontSmall))
        With cellValEmail1
            .Colspan = 8
            .Border = Rectangle.NO_BORDER
            .PaddingTop = 4
            .PaddingBottom = 4
        End With
        table.AddCell(cellValEmail1)
        '-------------------------------------------------------------------------
        Dim cellLblEstadoCivil As New PdfPCell(New Phrase("Estado Civil:", mo_FontSmallB))
        With cellLblEstadoCivil
            .Colspan = 3
            .Border = Rectangle.NO_BORDER
            .PaddingTop = 4
            .PaddingBottom = 4
        End With
        table.AddCell(cellLblEstadoCivil)

        Dim cellValEstadoCivil As New PdfPCell(New Phrase(lo_DataRow.Item("estadoCivil_Dal"), mo_FontSmall))
        With cellValEstadoCivil
            .Colspan = 8
            .Border = Rectangle.NO_BORDER
            .PaddingTop = 4
            .PaddingBottom = 4
        End With
        table.AddCell(cellValEstadoCivil)

        Dim cellLblRuc As New PdfPCell(New Phrase("RUC:", mo_FontSmallB))
        With cellLblRuc
            .Colspan = 3
            .Border = Rectangle.NO_BORDER
            .PaddingTop = 4
            .PaddingBottom = 4
        End With
        table.AddCell(cellLblRuc)

        Dim cellValRuc As New PdfPCell(New Phrase(lo_DataRow.Item("nroRuc_Pso"), mo_FontSmall))
        With cellValRuc
            .Colspan = 8
            .Border = Rectangle.NO_BORDER
            .PaddingTop = 4
            .PaddingBottom = 4
        End With
        table.AddCell(cellValRuc)
        '-------------------------------------------------------------------------

        'Dim cellLblDiscapacidad As New PdfPCell(New Phrase("Discapacidad:", mo_FontSmallB))
        'With cellLblDiscapacidad
        '    .Colspan = 3
        '    .Border = Rectangle.NO_BORDER
        '    .PaddingTop = 4
        '    .PaddingBottom = 4
        'End With
        'table.AddCell(cellLblDiscapacidad)

        'Dim la_Discapacidades As String() = lo_DataRow.Item("discapacidades_Dal").ToString.Split(",")
        'Dim ln_Index As Integer = 0
        'Dim ls_Discapacidades As String = ""
        'For Each _letra As String In la_Discapacidades
        '    If ls_Discapacidades <> "" AndAlso ln_Index < (la_Discapacidades.Length) Then
        '        ls_Discapacidades &= ", "
        '    End If
        '    Select Case _letra
        '        Case "F"
        '            ls_Discapacidades &= "Física"
        '        Case "S"
        '            ls_Discapacidades &= "Sensorial"
        '        Case "A"
        '            ls_Discapacidades &= "Auditiva"
        '        Case "V"
        '            ls_Discapacidades &= "Visual"
        '        Case "I"
        '            ls_Discapacidades &= "Intelectual"
        '    End Select
        '    ln_Index += 1
        'Next
        'Dim cellValDiscapacidades As New PdfPCell(New Phrase(ls_Discapacidades, mo_FontSmall))
        'With cellValDiscapacidades
        '    .Colspan = 17
        '    .Border = Rectangle.NO_BORDER
        '    .PaddingTop = 4
        '    .PaddingBottom = 4
        'End With
        'table.AddCell(cellValDiscapacidades)

        Return table
    End Function

    Private Function TablaInfoLaboral(ByVal lo_DataRow As Data.DataRow) As PdfPTable
        Dim cellEmptySpace As New PdfPCell
        Dim table As New PdfPTable(20)
        With table
            .WidthPercentage = 100
        End With

        Dim cellTitulo As PdfPCell = GenerarTitulo("INFORMACIÓN LABORAL")
        cellTitulo.Colspan = 8
        cellTitulo.Border = Rectangle.NO_BORDER
        table.AddCell(cellTitulo)

        cellEmptySpace.Colspan = 12
        cellEmptySpace.Border = Rectangle.NO_BORDER
        table.AddCell(cellEmptySpace)

        '-------------------------------------------------------------------------

        Dim cellLblCentroLabores As New PdfPCell(New Phrase("Centro de Labores:", mo_FontSmallB))
        With cellLblCentroLabores
            .Colspan = 3
            .Border = Rectangle.NO_BORDER
            .PaddingTop = 4
            .PaddingBottom = 4
        End With
        table.AddCell(cellLblCentroLabores)

        Dim cellValCentroLabores As New PdfPCell(New Phrase(lo_DataRow.Item("centroTrabajo_Dal"), mo_FontSmall))
        With cellValCentroLabores
            .Colspan = 17
            .Border = Rectangle.NO_BORDER
            .PaddingTop = 4
            .PaddingBottom = 4
        End With
        table.AddCell(cellValCentroLabores)

        Dim cellLblCargoActual As New PdfPCell(New Phrase("Cargo Actual:", mo_FontSmallB))
        With cellLblCargoActual
            .Colspan = 3
            .Border = Rectangle.NO_BORDER
            .PaddingTop = 4
            .PaddingBottom = 4
        End With
        table.AddCell(cellLblCargoActual)

        Dim cellValCargoActual As New PdfPCell(New Phrase(lo_DataRow.Item("cargoActual_Dal"), mo_FontSmall))
        With cellValCargoActual
            .Colspan = 17
            .Border = Rectangle.NO_BORDER
            .PaddingTop = 4
            .PaddingBottom = 4
        End With
        table.AddCell(cellValCargoActual)

        Return table
    End Function

    Private Function TablaInfoEducativa(ByVal lo_DataRow As Data.DataRow) As PdfPTable
        Dim cellEmptySpace As New PdfPCell
        Dim table As New PdfPTable(20)
        With table
            .WidthPercentage = 100
        End With

        Dim cellTitulo As PdfPCell = GenerarTitulo("INFORMACIÓN EDUCATIVA")
        cellTitulo.Colspan = 8
        cellTitulo.Border = Rectangle.NO_BORDER
        table.AddCell(cellTitulo)

        cellEmptySpace.Colspan = 12
        cellEmptySpace.Border = Rectangle.NO_BORDER
        table.AddCell(cellEmptySpace)

        '-------------------------------------------------------------------------

        Dim cellLblProcedencia As New PdfPCell(New Phrase("Procedencia:", mo_FontSmallB))
        With cellLblProcedencia
            .Colspan = 3
            .Border = Rectangle.NO_BORDER
            .PaddingTop = 4
            .PaddingBottom = 4
        End With
        table.AddCell(cellLblProcedencia)

        Dim cellValProcedencia As New PdfPCell(New Phrase(lo_DataRow.Item("nombre_PaiInstEdu"), mo_FontSmall))
        With cellValProcedencia
            .Colspan = 3
            .Border = Rectangle.NO_BORDER
            .PaddingTop = 4
            .PaddingBottom = 4
        End With
        table.AddCell(cellValProcedencia)

        Dim cellLblInstTipo As New PdfPCell(New Phrase("I.E:", mo_FontSmallB))
        With cellLblInstTipo
            .Colspan = 1
            .Border = Rectangle.NO_BORDER
            .PaddingTop = 4
            .PaddingBottom = 4
        End With
        table.AddCell(cellLblInstTipo)

        Dim cellValInstTipo As New PdfPCell(New Phrase(lo_DataRow.Item("Gestion_ied"), mo_FontSmall))
        With cellValInstTipo
            .Colspan = 2
            .Border = Rectangle.NO_BORDER
            .PaddingTop = 4
            .PaddingBottom = 4
        End With
        table.AddCell(cellValInstTipo)

        Dim cellLblPromocion As New PdfPCell(New Phrase("Promoción:", mo_FontSmallB))
        With cellLblPromocion
            .Colspan = 2
            .Border = Rectangle.NO_BORDER
            .PaddingTop = 4
            .PaddingBottom = 4
        End With
        table.AddCell(cellLblPromocion)

        Dim cellValPromocion As New PdfPCell(New Phrase(lo_DataRow.Item("añoEgresoSec_Dal"), mo_FontSmall))
        With cellValPromocion
            .Colspan = 2
            .Border = Rectangle.NO_BORDER
            .PaddingTop = 4
            .PaddingBottom = 4
        End With
        table.AddCell(cellValPromocion)

        Dim cellLblSeccion As New PdfPCell(New Phrase("Sección:", mo_FontSmallB))
        With cellLblSeccion
            .Colspan = 2
            .Border = Rectangle.NO_BORDER
            .PaddingTop = 4
            .PaddingBottom = 4
        End With
        table.AddCell(cellLblSeccion)

        Dim cellValSeccion As New PdfPCell(New Phrase(lo_DataRow.Item("seccionEgreso_Dal"), mo_FontSmall))
        With cellValSeccion
            .Colspan = 1
            .Border = Rectangle.NO_BORDER
            .PaddingTop = 4
            .PaddingBottom = 4
        End With
        table.AddCell(cellValSeccion)

        Dim cellLblMerito As New PdfPCell(New Phrase("Mérito:", mo_FontSmallB))
        With cellLblMerito
            .Colspan = 2
            .Border = Rectangle.NO_BORDER
            .PaddingTop = 4
            .PaddingBottom = 4
        End With
        table.AddCell(cellLblMerito)

        Dim ls_Merito As String = ""
        Select Case lo_DataRow.Item("ordenMerito_Dal")
            Case 0
                ls_Merito = "Ninguno"
            Case 1
                ls_Merito = "1°"
            Case 2
                ls_Merito = "2°"
        End Select
        Dim cellValMerito As New PdfPCell(New Phrase(ls_Merito, mo_FontSmall))
        With cellValMerito
            .Colspan = 2
            .Border = Rectangle.NO_BORDER
            .PaddingTop = 4
            .PaddingBottom = 4
        End With
        table.AddCell(cellValMerito)

        '-------------------------------------------------------------------------

        Dim cellLblLugInstitucion As New PdfPCell(New Phrase("Lugar de Institución:", mo_FontSmallB))
        With cellLblLugInstitucion
            .Colspan = 4
            .Border = Rectangle.NO_BORDER
            .PaddingTop = 4
            .PaddingBottom = 4
        End With
        table.AddCell(cellLblLugInstitucion)

        Dim cellLblLugInstDepartamento As New PdfPCell(New Phrase("Dpto.", mo_FontSmallB))
        With cellLblLugInstDepartamento
            .Colspan = 1
            .Border = Rectangle.NO_BORDER
            .PaddingTop = 4
            .PaddingBottom = 4
        End With
        table.AddCell(cellLblLugInstDepartamento)

        Dim cellValLugInstDepartamento As New PdfPCell(New Phrase(lo_DataRow.Item("nombre_DepInstEdu"), mo_FontSmall))
        With cellValLugInstDepartamento
            .Colspan = 3
            .Border = Rectangle.NO_BORDER
            .PaddingTop = 4
            .PaddingBottom = 4
        End With
        table.AddCell(cellValLugInstDepartamento)

        Dim cellLblLugInstProvincia As New PdfPCell(New Phrase("Dpto.", mo_FontSmallB))
        With cellLblLugInstProvincia
            .Colspan = 1
            .Border = Rectangle.NO_BORDER
            .PaddingTop = 4
            .PaddingBottom = 4
        End With
        table.AddCell(cellLblLugInstProvincia)

        Dim cellValLugInstProvincia As New PdfPCell(New Phrase(lo_DataRow.Item("nombre_ProInstEdu"), mo_FontSmall))
        With cellValLugInstProvincia
            .Colspan = 3
            .Border = Rectangle.NO_BORDER
            .PaddingTop = 4
            .PaddingBottom = 4
        End With
        table.AddCell(cellValLugInstProvincia)

        Dim cellLblLugInstDistrito As New PdfPCell(New Phrase("Dpto.", mo_FontSmallB))
        With cellLblLugInstDistrito
            .Colspan = 1
            .Border = Rectangle.NO_BORDER
            .PaddingTop = 4
            .PaddingBottom = 4
        End With
        table.AddCell(cellLblLugInstDistrito)

        Dim cellValLugInstDistrito As New PdfPCell(New Phrase(lo_DataRow.Item("nombre_DepInstEdu"), mo_FontSmall))
        With cellValLugInstDistrito
            .Colspan = 7
            .Border = Rectangle.NO_BORDER
            .PaddingTop = 4
            .PaddingBottom = 4
        End With
        table.AddCell(cellValLugInstDistrito)

        '-------------------------------------------------------------------------

        Dim cellLblInstNombre As New PdfPCell(New Phrase("Nombre de Institución Educativa:", mo_FontSmallB))
        With cellLblInstNombre
            .Colspan = 5
            .Border = Rectangle.NO_BORDER
            .PaddingTop = 4
            .PaddingBottom = 4
        End With
        table.AddCell(cellLblInstNombre)

        Dim cellValInstNombre As New PdfPCell(New Phrase(lo_DataRow.Item("Nombre_ied"), mo_FontSmall))
        With cellValInstNombre
            .Colspan = 15
            .Border = Rectangle.NO_BORDER
            .PaddingTop = 4
            .PaddingBottom = 4
        End With
        table.AddCell(cellValInstNombre)

        Return table
    End Function

    Private Function TablaInfoPadre(ByVal lo_DataRow As Data.DataRow) As PdfPTable
        Dim cellEmptySpace As New PdfPCell
        Dim table As New PdfPTable(20)
        With table
            .WidthPercentage = 100
        End With

        Dim cellTitulo As PdfPCell = GenerarTitulo("INFORMACIÓN PADRE")
        With cellTitulo
            .Colspan = 8
            .Border = Rectangle.NO_BORDER
        End With
        table.AddCell(cellTitulo)

        cellEmptySpace.Colspan = 12
        cellEmptySpace.Border = Rectangle.NO_BORDER
        table.AddCell(cellEmptySpace)

        '-------------------------------------------------------------------------

        Dim cellLblApePaterno As New PdfPCell(New Phrase("Ape. Paterno:", mo_FontSmallB))
        With cellLblApePaterno
            .Colspan = 3
            .Border = Rectangle.NO_BORDER
            .PaddingTop = 4
            .PaddingBottom = 4
        End With
        table.AddCell(cellLblApePaterno)

        Dim cellValApePaterno As New PdfPCell(New Phrase(lo_DataRow.Item("apellidoPaternoPadre_fam"), mo_FontSmall))
        With cellValApePaterno
            .Colspan = 7
            .Border = Rectangle.NO_BORDER
            .PaddingTop = 4
            .PaddingBottom = 4
        End With
        table.AddCell(cellValApePaterno)

        Dim cellLblApeMaterno As New PdfPCell(New Phrase("Ape. Materno:", mo_FontSmallB))
        With cellLblApeMaterno
            .Colspan = 3
            .Border = Rectangle.NO_BORDER
            .PaddingTop = 4
            .PaddingBottom = 4
        End With
        table.AddCell(cellLblApeMaterno)

        Dim cellValApeMaterno As New PdfPCell(New Phrase(lo_DataRow.Item("apellidoMaternoPadre_fam"), mo_FontSmall))
        With cellValApeMaterno
            .Colspan = 7
            .Border = Rectangle.NO_BORDER
            .PaddingTop = 4
            .PaddingBottom = 4
        End With
        table.AddCell(cellValApeMaterno)

        '-------------------------------------------------------------------------

        Dim cellLblNombres As New PdfPCell(New Phrase("Nombres:", mo_FontSmallB))
        With cellLblNombres
            .Colspan = 3
            .Border = Rectangle.NO_BORDER
            .PaddingTop = 4
            .PaddingBottom = 4
        End With
        table.AddCell(cellLblNombres)

        Dim cellValNombres As New PdfPCell(New Phrase(lo_DataRow.Item("nombresPadre_fam"), mo_FontSmall))
        With cellValNombres
            .Colspan = 9
            .Border = Rectangle.NO_BORDER
            .PaddingTop = 4
            .PaddingBottom = 4
        End With
        table.AddCell(cellValNombres)

        Dim cellLblFecNacimiento As New PdfPCell(New Phrase("Fech. Nacimiento:", mo_FontSmallB))
        With cellLblFecNacimiento
            .Colspan = 3
            .Border = Rectangle.NO_BORDER
            .PaddingTop = 4
            .PaddingBottom = 4
        End With
        table.AddCell(cellLblFecNacimiento)

        Dim cellValFecNacimiento As New PdfPCell(New Phrase(Date.Parse(lo_DataRow.Item("fechaNacimientoPadre_fam")).ToString("dd/MM/yyyy"), mo_FontSmall))
        With cellValFecNacimiento
            .Colspan = 5
            .Border = Rectangle.NO_BORDER
            .PaddingTop = 4
            .PaddingBottom = 4
        End With
        table.AddCell(cellValFecNacimiento)

        '-------------------------------------------------------------------------

        Dim cellLblDirActual As New PdfPCell(New Phrase("Dirección Actual:", mo_FontSmallB))
        With cellLblDirActual
            .Colspan = 3
            .Border = Rectangle.NO_BORDER
            .PaddingTop = 4
            .PaddingBottom = 4
        End With
        table.AddCell(cellLblDirActual)

        Dim cellValDirActual As New PdfPCell(New Phrase(lo_DataRow.Item("direccionPadre_fam"), mo_FontSmall))
        With cellValDirActual
            .Colspan = 5
            .Border = Rectangle.NO_BORDER
            .PaddingTop = 4
            .PaddingBottom = 4
        End With
        table.AddCell(cellValDirActual)

        Dim cellLblDirActDepartamento As New PdfPCell(New Phrase("Dpto.", mo_FontSmallB))
        With cellLblDirActDepartamento
            .Colspan = 1
            .Border = Rectangle.NO_BORDER
            .PaddingTop = 4
            .PaddingBottom = 4
        End With
        table.AddCell(cellLblDirActDepartamento)

        Dim cellValDirActDepartamento As New PdfPCell(New Phrase(lo_DataRow.Item("nombre_DepPadre"), mo_FontSmall))
        With cellValDirActDepartamento
            .Colspan = 3
            .Border = Rectangle.NO_BORDER
            .PaddingTop = 4
            .PaddingBottom = 4
        End With
        table.AddCell(cellValDirActDepartamento)

        Dim cellLblDirActProvincia As New PdfPCell(New Phrase("Prov.", mo_FontSmallB))
        With cellLblDirActProvincia
            .Colspan = 1
            .Border = Rectangle.NO_BORDER
            .PaddingTop = 4
            .PaddingBottom = 4
        End With
        table.AddCell(cellLblDirActProvincia)

        Dim cellValDirActProvincia As New PdfPCell(New Phrase(lo_DataRow.Item("nombre_ProPadre"), mo_FontSmall))
        With cellValDirActProvincia
            .Colspan = 3
            .Border = Rectangle.NO_BORDER
            .PaddingTop = 4
            .PaddingBottom = 4
        End With
        table.AddCell(cellValDirActProvincia)

        Dim cellLblDirActDistrito As New PdfPCell(New Phrase("Distr.", mo_FontSmallB))
        With cellLblDirActDistrito
            .Colspan = 1
            .Border = Rectangle.NO_BORDER
            .PaddingTop = 4
            .PaddingBottom = 4
        End With
        table.AddCell(cellLblDirActDistrito)

        Dim cellValDirActDistrito As New PdfPCell(New Phrase(lo_DataRow.Item("nombre_DisPadre"), mo_FontSmall))
        With cellValDirActDistrito
            .Colspan = 3
            .Border = Rectangle.NO_BORDER
            .PaddingTop = 4
            .PaddingBottom = 4
        End With
        table.AddCell(cellValDirActDistrito)

        '-------------------------------------------------------------------------

        Dim cellLblTelefono As New PdfPCell(New Phrase("Teléfono:", mo_FontSmallB))
        With cellLblTelefono
            .Colspan = 3
            .Border = Rectangle.NO_BORDER
            .PaddingTop = 4
            .PaddingBottom = 4
        End With
        table.AddCell(cellLblTelefono)

        Dim cellLblTelCasa As New PdfPCell(New Phrase("Casa:", mo_FontSmallB))
        With cellLblTelCasa
            .Colspan = 2
            .Border = Rectangle.NO_BORDER
            .PaddingTop = 4
            .PaddingBottom = 4
        End With
        table.AddCell(cellLblTelCasa)

        Dim cellValTelCasa As New PdfPCell(New Phrase(lo_DataRow.Item("telefonoFijoPadre_fam"), mo_FontSmall))
        With cellValTelCasa
            .Colspan = 2
            .Border = Rectangle.NO_BORDER
            .PaddingTop = 4
            .PaddingBottom = 4
        End With
        table.AddCell(cellValTelCasa)

        Dim cellLblCelular As New PdfPCell(New Phrase("Celular:", mo_FontSmallB))
        With cellLblCelular
            .Colspan = 2
            .Border = Rectangle.NO_BORDER
            .PaddingTop = 4
            .PaddingBottom = 4
        End With
        table.AddCell(cellLblCelular)

        Dim cellValCelular As New PdfPCell(New Phrase(lo_DataRow.Item("telefonoCelularPadre_fam"), mo_FontSmall))
        With cellValCelular
            .Colspan = 3
            .Border = Rectangle.NO_BORDER
            .PaddingTop = 4
            .PaddingBottom = 4
        End With
        table.AddCell(cellValCelular)

        Dim cellLblOperador As New PdfPCell(New Phrase("Operador:", mo_FontSmallB))
        With cellLblOperador
            .Colspan = 2
            .Border = Rectangle.NO_BORDER
            .PaddingTop = 4
            .PaddingBottom = 4
        End With
        table.AddCell(cellLblOperador)

        Dim cellValOperador As New PdfPCell(New Phrase(lo_DataRow.Item("operadorCelularPadre_fam"), mo_FontSmall))
        With cellValOperador
            .Colspan = 6
            .Border = Rectangle.NO_BORDER
            .PaddingTop = 4
            .PaddingBottom = 4
        End With
        table.AddCell(cellValOperador)

        '-------------------------------------------------------------------------

        Dim cellLblEmail As New PdfPCell(New Phrase("Email:", mo_FontSmallB))
        With cellLblEmail
            .Colspan = 3
            .Border = Rectangle.NO_BORDER
            .PaddingTop = 4
            .PaddingBottom = 4
        End With
        table.AddCell(cellLblEmail)

        Dim cellValEmail As New PdfPCell(New Phrase(lo_DataRow.Item("emailPadre_fam"), mo_FontSmall))
        With cellValEmail
            .Colspan = 7
            .Border = Rectangle.NO_BORDER
            .PaddingTop = 4
            .PaddingBottom = 4
        End With
        table.AddCell(cellValEmail)

        Dim cellLblRespPago As New PdfPCell(New Phrase("Responsable del Pago:", mo_FontSmallB))
        With cellLblRespPago
            .Colspan = 4
            .Border = Rectangle.NO_BORDER
            .PaddingTop = 4
            .PaddingBottom = 4
        End With
        table.AddCell(cellLblRespPago)

        Dim cellValRespPago As New PdfPCell(New Phrase(lo_DataRow.Item("indRespPagoPadre_fam"), mo_FontSmall))
        With cellValRespPago
            .Colspan = 6
            .Border = Rectangle.NO_BORDER
            .PaddingTop = 4
            .PaddingBottom = 4
        End With
        table.AddCell(cellValRespPago)

        Return table
    End Function

    Private Function TablaInfoMadre(ByVal lo_DataRow As Data.DataRow) As PdfPTable
        Dim cellEmptySpace As New PdfPCell
        Dim table As New PdfPTable(20)
        With table
            .WidthPercentage = 100
        End With

        Dim cellTitulo As PdfPCell = GenerarTitulo("INFORMACIÓN MADRE")
        With cellTitulo
            .Colspan = 8
            .Border = Rectangle.NO_BORDER
        End With
        table.AddCell(cellTitulo)

        cellEmptySpace.Colspan = 12
        cellEmptySpace.Border = Rectangle.NO_BORDER
        table.AddCell(cellEmptySpace)

        '-------------------------------------------------------------------------

        Dim cellLblApePaterno As New PdfPCell(New Phrase("Ape. Paterno:", mo_FontSmallB))
        With cellLblApePaterno
            .Colspan = 3
            .Border = Rectangle.NO_BORDER
            .PaddingTop = 4
            .PaddingBottom = 4
        End With
        table.AddCell(cellLblApePaterno)

        Dim cellValApePaterno As New PdfPCell(New Phrase(lo_DataRow.Item("apellidoPaternoMadre_fam"), mo_FontSmall))
        With cellValApePaterno
            .Colspan = 7
            .Border = Rectangle.NO_BORDER
            .PaddingTop = 4
            .PaddingBottom = 4
        End With
        table.AddCell(cellValApePaterno)

        Dim cellLblApeMaterno As New PdfPCell(New Phrase("Ape. Materno:", mo_FontSmallB))
        With cellLblApeMaterno
            .Colspan = 3
            .Border = Rectangle.NO_BORDER
            .PaddingTop = 4
            .PaddingBottom = 4
        End With
        table.AddCell(cellLblApeMaterno)

        Dim cellValApeMaterno As New PdfPCell(New Phrase(lo_DataRow.Item("apellidoMaternoMadre_fam"), mo_FontSmall))
        With cellValApeMaterno
            .Colspan = 7
            .Border = Rectangle.NO_BORDER
            .PaddingTop = 4
            .PaddingBottom = 4
        End With
        table.AddCell(cellValApeMaterno)

        '-------------------------------------------------------------------------

        Dim cellLblNombres As New PdfPCell(New Phrase("Nombres:", mo_FontSmallB))
        With cellLblNombres
            .Colspan = 3
            .Border = Rectangle.NO_BORDER
            .PaddingTop = 4
            .PaddingBottom = 4
        End With
        table.AddCell(cellLblNombres)

        Dim cellValNombres As New PdfPCell(New Phrase(lo_DataRow.Item("nombresMadre_fam"), mo_FontSmall))
        With cellValNombres
            .Colspan = 9
            .Border = Rectangle.NO_BORDER
            .PaddingTop = 4
            .PaddingBottom = 4
        End With
        table.AddCell(cellValNombres)

        Dim cellLblFecNacimiento As New PdfPCell(New Phrase("Fech. Nacimiento:", mo_FontSmallB))
        With cellLblFecNacimiento
            .Colspan = 3
            .Border = Rectangle.NO_BORDER
            .PaddingTop = 4
            .PaddingBottom = 4
        End With
        table.AddCell(cellLblFecNacimiento)

        Dim cellValFecNacimiento As New PdfPCell(New Phrase(Date.Parse(lo_DataRow.Item("fechaNacimientoMadre_fam")).ToString("dd/MM/yyyy"), mo_FontSmall))
        With cellValFecNacimiento
            .Colspan = 5
            .Border = Rectangle.NO_BORDER
            .PaddingTop = 4
            .PaddingBottom = 4
        End With
        table.AddCell(cellValFecNacimiento)

        '-------------------------------------------------------------------------

        Dim cellLblDirActual As New PdfPCell(New Phrase("Dirección Actual:", mo_FontSmallB))
        With cellLblDirActual
            .Colspan = 3
            .Border = Rectangle.NO_BORDER
            .PaddingTop = 4
            .PaddingBottom = 4
        End With
        table.AddCell(cellLblDirActual)

        Dim cellValDirActual As New PdfPCell(New Phrase(lo_DataRow.Item("direccionMadre_fam"), mo_FontSmall))
        With cellValDirActual
            .Colspan = 5
            .Border = Rectangle.NO_BORDER
            .PaddingTop = 4
            .PaddingBottom = 4
        End With
        table.AddCell(cellValDirActual)

        Dim cellLblDirActDepartamento As New PdfPCell(New Phrase("Dpto.", mo_FontSmallB))
        With cellLblDirActDepartamento
            .Colspan = 1
            .Border = Rectangle.NO_BORDER
            .PaddingTop = 4
            .PaddingBottom = 4
        End With
        table.AddCell(cellLblDirActDepartamento)

        Dim cellValDirActDepartamento As New PdfPCell(New Phrase(lo_DataRow.Item("nombre_DepMadre"), mo_FontSmall))
        With cellValDirActDepartamento
            .Colspan = 3
            .Border = Rectangle.NO_BORDER
            .PaddingTop = 4
            .PaddingBottom = 4
        End With
        table.AddCell(cellValDirActDepartamento)

        Dim cellLblDirActProvincia As New PdfPCell(New Phrase("Prov.", mo_FontSmallB))
        With cellLblDirActProvincia
            .Colspan = 1
            .Border = Rectangle.NO_BORDER
            .PaddingTop = 4
            .PaddingBottom = 4
        End With
        table.AddCell(cellLblDirActProvincia)

        Dim cellValDirActProvincia As New PdfPCell(New Phrase(lo_DataRow.Item("nombre_ProMadre"), mo_FontSmall))
        With cellValDirActProvincia
            .Colspan = 3
            .Border = Rectangle.NO_BORDER
            .PaddingTop = 4
            .PaddingBottom = 4
        End With
        table.AddCell(cellValDirActProvincia)

        Dim cellLblDirActDistrito As New PdfPCell(New Phrase("Distr.", mo_FontSmallB))
        With cellLblDirActDistrito
            .Colspan = 1
            .Border = Rectangle.NO_BORDER
            .PaddingTop = 4
            .PaddingBottom = 4
        End With
        table.AddCell(cellLblDirActDistrito)

        Dim cellValDirActDistrito As New PdfPCell(New Phrase(lo_DataRow.Item("nombre_DisMadre"), mo_FontSmall))
        With cellValDirActDistrito
            .Colspan = 3
            .Border = Rectangle.NO_BORDER
            .PaddingTop = 4
            .PaddingBottom = 4
        End With
        table.AddCell(cellValDirActDistrito)

        '-------------------------------------------------------------------------

        Dim cellLblTelefono As New PdfPCell(New Phrase("Teléfono:", mo_FontSmallB))
        With cellLblTelefono
            .Colspan = 3
            .Border = Rectangle.NO_BORDER
            .PaddingTop = 4
            .PaddingBottom = 4
        End With
        table.AddCell(cellLblTelefono)

        Dim cellLblTelCasa As New PdfPCell(New Phrase("Casa:", mo_FontSmallB))
        With cellLblTelCasa
            .Colspan = 2
            .Border = Rectangle.NO_BORDER
            .PaddingTop = 4
            .PaddingBottom = 4
        End With
        table.AddCell(cellLblTelCasa)

        Dim cellValTelCasa As New PdfPCell(New Phrase(lo_DataRow.Item("telefonoFijoMadre_fam"), mo_FontSmall))
        With cellValTelCasa
            .Colspan = 2
            .Border = Rectangle.NO_BORDER
            .PaddingTop = 4
            .PaddingBottom = 4
        End With
        table.AddCell(cellValTelCasa)

        Dim cellLblCelular As New PdfPCell(New Phrase("Celular:", mo_FontSmallB))
        With cellLblCelular
            .Colspan = 2
            .Border = Rectangle.NO_BORDER
            .PaddingTop = 4
            .PaddingBottom = 4
        End With
        table.AddCell(cellLblCelular)

        Dim cellValCelular As New PdfPCell(New Phrase(lo_DataRow.Item("telefonoCelularMadre_fam"), mo_FontSmall))
        With cellValCelular
            .Colspan = 3
            .Border = Rectangle.NO_BORDER
            .PaddingTop = 4
            .PaddingBottom = 4
        End With
        table.AddCell(cellValCelular)

        Dim cellLblOperador As New PdfPCell(New Phrase("Operador:", mo_FontSmallB))
        With cellLblOperador
            .Colspan = 2
            .Border = Rectangle.NO_BORDER
            .PaddingTop = 4
            .PaddingBottom = 4
        End With
        table.AddCell(cellLblOperador)

        Dim cellValOperador As New PdfPCell(New Phrase(lo_DataRow.Item("operadorCelularMadre_fam"), mo_FontSmall))
        With cellValOperador
            .Colspan = 6
            .Border = Rectangle.NO_BORDER
            .PaddingTop = 4
            .PaddingBottom = 4
        End With
        table.AddCell(cellValOperador)

        '-------------------------------------------------------------------------

        Dim cellLblEmail As New PdfPCell(New Phrase("Email:", mo_FontSmallB))
        With cellLblEmail
            .Colspan = 3
            .Border = Rectangle.NO_BORDER
            .PaddingTop = 4
            .PaddingBottom = 4
        End With
        table.AddCell(cellLblEmail)

        Dim cellValEmail As New PdfPCell(New Phrase(lo_DataRow.Item("emailMadre_fam"), mo_FontSmall))
        With cellValEmail
            .Colspan = 7
            .Border = Rectangle.NO_BORDER
            .PaddingTop = 4
            .PaddingBottom = 4
        End With
        table.AddCell(cellValEmail)

        Dim cellLblRespPago As New PdfPCell(New Phrase("Responsable del Pago:", mo_FontSmallB))
        With cellLblRespPago
            .Colspan = 4
            .Border = Rectangle.NO_BORDER
            .PaddingTop = 4
            .PaddingBottom = 4
        End With
        table.AddCell(cellLblRespPago)

        Dim cellValRespPago As New PdfPCell(New Phrase(lo_DataRow.Item("indRespPagoMadre_fam"), mo_FontSmall))
        With cellValRespPago
            .Colspan = 6
            .Border = Rectangle.NO_BORDER
            .PaddingTop = 4
            .PaddingBottom = 4
        End With
        table.AddCell(cellValRespPago)

        Return table
    End Function

    Private Function TablaInfoApoderado(ByVal lo_DataRow As Data.DataRow) As PdfPTable
        Dim cellEmptySpace As New PdfPCell
        Dim table As New PdfPTable(20)
        With table
            .WidthPercentage = 100
        End With

        Dim cellTitulo As PdfPCell = GenerarTitulo("INFORMACIÓN APODERADO")
        With cellTitulo
            .Colspan = 8
            .Border = Rectangle.NO_BORDER
        End With
        table.AddCell(cellTitulo)

        cellEmptySpace.Colspan = 12
        cellEmptySpace.Border = Rectangle.NO_BORDER
        table.AddCell(cellEmptySpace)

        '-------------------------------------------------------------------------

        Dim cellLblApePaterno As New PdfPCell(New Phrase("Ape. Paterno:", mo_FontSmallB))
        With cellLblApePaterno
            .Colspan = 3
            .Border = Rectangle.NO_BORDER
            .PaddingTop = 4
            .PaddingBottom = 4
        End With
        table.AddCell(cellLblApePaterno)

        Dim cellValApePaterno As New PdfPCell(New Phrase(lo_DataRow.Item("apellidoPaternoApod_fam"), mo_FontSmall))
        With cellValApePaterno
            .Colspan = 7
            .Border = Rectangle.NO_BORDER
            .PaddingTop = 4
            .PaddingBottom = 4
        End With
        table.AddCell(cellValApePaterno)

        Dim cellLblApeMaterno As New PdfPCell(New Phrase("Ape. Materno:", mo_FontSmallB))
        With cellLblApeMaterno
            .Colspan = 3
            .Border = Rectangle.NO_BORDER
            .PaddingTop = 4
            .PaddingBottom = 4
        End With
        table.AddCell(cellLblApeMaterno)

        Dim cellValApeMaterno As New PdfPCell(New Phrase(lo_DataRow.Item("apellidoMaternoApod_fam"), mo_FontSmall))
        With cellValApeMaterno
            .Colspan = 7
            .Border = Rectangle.NO_BORDER
            .PaddingTop = 4
            .PaddingBottom = 4
        End With
        table.AddCell(cellValApeMaterno)

        '-------------------------------------------------------------------------

        Dim cellLblNombres As New PdfPCell(New Phrase("Nombres:", mo_FontSmallB))
        With cellLblNombres
            .Colspan = 3
            .Border = Rectangle.NO_BORDER
            .PaddingTop = 4
            .PaddingBottom = 4
        End With
        table.AddCell(cellLblNombres)

        Dim cellValNombres As New PdfPCell(New Phrase(lo_DataRow.Item("nombresApod_fam"), mo_FontSmall))
        With cellValNombres
            .Colspan = 9
            .Border = Rectangle.NO_BORDER
            .PaddingTop = 4
            .PaddingBottom = 4
        End With
        table.AddCell(cellValNombres)

        Dim cellLblFecNacimiento As New PdfPCell(New Phrase("Fech. Nacimiento:", mo_FontSmallB))
        With cellLblFecNacimiento
            .Colspan = 3
            .Border = Rectangle.NO_BORDER
            .PaddingTop = 4
            .PaddingBottom = 4
        End With
        table.AddCell(cellLblFecNacimiento)

        Dim cellValFecNacimiento As New PdfPCell(New Phrase(Date.Parse(lo_DataRow.Item("fechaNacimientoApod_fam")).ToString("dd/MM/yyyy"), mo_FontSmall))
        With cellValFecNacimiento
            .Colspan = 5
            .Border = Rectangle.NO_BORDER
            .PaddingTop = 4
            .PaddingBottom = 4
        End With
        table.AddCell(cellValFecNacimiento)

        '-------------------------------------------------------------------------

        Dim cellLblDirActual As New PdfPCell(New Phrase("Dirección Actual:", mo_FontSmallB))
        With cellLblDirActual
            .Colspan = 3
            .Border = Rectangle.NO_BORDER
            .PaddingTop = 4
            .PaddingBottom = 4
        End With
        table.AddCell(cellLblDirActual)

        Dim cellValDirActual As New PdfPCell(New Phrase(lo_DataRow.Item("direccionApod_fam"), mo_FontSmall))
        With cellValDirActual
            .Colspan = 5
            .Border = Rectangle.NO_BORDER
            .PaddingTop = 4
            .PaddingBottom = 4
        End With
        table.AddCell(cellValDirActual)

        Dim cellLblDirActDepartamento As New PdfPCell(New Phrase("Dpto.", mo_FontSmallB))
        With cellLblDirActDepartamento
            .Colspan = 1
            .Border = Rectangle.NO_BORDER
            .PaddingTop = 4
            .PaddingBottom = 4
        End With
        table.AddCell(cellLblDirActDepartamento)

        Dim cellValDirActDepartamento As New PdfPCell(New Phrase(lo_DataRow.Item("nombre_DepApod"), mo_FontSmall))
        With cellValDirActDepartamento
            .Colspan = 3
            .Border = Rectangle.NO_BORDER
            .PaddingTop = 4
            .PaddingBottom = 4
        End With
        table.AddCell(cellValDirActDepartamento)

        Dim cellLblDirActProvincia As New PdfPCell(New Phrase("Prov.", mo_FontSmallB))
        With cellLblDirActProvincia
            .Colspan = 1
            .Border = Rectangle.NO_BORDER
            .PaddingTop = 4
            .PaddingBottom = 4
        End With
        table.AddCell(cellLblDirActProvincia)

        Dim cellValDirActProvincia As New PdfPCell(New Phrase(lo_DataRow.Item("nombre_ProApod"), mo_FontSmall))
        With cellValDirActProvincia
            .Colspan = 3
            .Border = Rectangle.NO_BORDER
            .PaddingTop = 4
            .PaddingBottom = 4
        End With
        table.AddCell(cellValDirActProvincia)

        Dim cellLblDirActDistrito As New PdfPCell(New Phrase("Distr.", mo_FontSmallB))
        With cellLblDirActDistrito
            .Colspan = 1
            .Border = Rectangle.NO_BORDER
            .PaddingTop = 4
            .PaddingBottom = 4
        End With
        table.AddCell(cellLblDirActDistrito)

        Dim cellValDirActDistrito As New PdfPCell(New Phrase(lo_DataRow.Item("nombre_DisApod"), mo_FontSmall))
        With cellValDirActDistrito
            .Colspan = 3
            .Border = Rectangle.NO_BORDER
            .PaddingTop = 4
            .PaddingBottom = 4
        End With
        table.AddCell(cellValDirActDistrito)

        '-------------------------------------------------------------------------

        Dim cellLblTelefono As New PdfPCell(New Phrase("Teléfono:", mo_FontSmallB))
        With cellLblTelefono
            .Colspan = 3
            .Border = Rectangle.NO_BORDER
            .PaddingTop = 4
            .PaddingBottom = 4
        End With
        table.AddCell(cellLblTelefono)

        Dim cellLblTelCasa As New PdfPCell(New Phrase("Casa:", mo_FontSmallB))
        With cellLblTelCasa
            .Colspan = 2
            .Border = Rectangle.NO_BORDER
            .PaddingTop = 4
            .PaddingBottom = 4
        End With
        table.AddCell(cellLblTelCasa)

        Dim cellValTelCasa As New PdfPCell(New Phrase(lo_DataRow.Item("telefonoFijoApod_fam"), mo_FontSmall))
        With cellValTelCasa
            .Colspan = 2
            .Border = Rectangle.NO_BORDER
            .PaddingTop = 4
            .PaddingBottom = 4
        End With
        table.AddCell(cellValTelCasa)

        Dim cellLblCelular As New PdfPCell(New Phrase("Celular:", mo_FontSmallB))
        With cellLblCelular
            .Colspan = 2
            .Border = Rectangle.NO_BORDER
            .PaddingTop = 4
            .PaddingBottom = 4
        End With
        table.AddCell(cellLblCelular)

        Dim cellValCelular As New PdfPCell(New Phrase(lo_DataRow.Item("telefonoCelularApod_fam"), mo_FontSmall))
        With cellValCelular
            .Colspan = 3
            .Border = Rectangle.NO_BORDER
            .PaddingTop = 4
            .PaddingBottom = 4
        End With
        table.AddCell(cellValCelular)

        Dim cellLblOperador As New PdfPCell(New Phrase("Operador:", mo_FontSmallB))
        With cellLblOperador
            .Colspan = 2
            .Border = Rectangle.NO_BORDER
            .PaddingTop = 4
            .PaddingBottom = 4
        End With
        table.AddCell(cellLblOperador)

        Dim cellValOperador As New PdfPCell(New Phrase(lo_DataRow.Item("operadorCelularApod_fam"), mo_FontSmall))
        With cellValOperador
            .Colspan = 6
            .Border = Rectangle.NO_BORDER
            .PaddingTop = 4
            .PaddingBottom = 4
        End With
        table.AddCell(cellValOperador)

        '-------------------------------------------------------------------------

        Dim cellLblEmail As New PdfPCell(New Phrase("Email:", mo_FontSmallB))
        With cellLblEmail
            .Colspan = 3
            .Border = Rectangle.NO_BORDER
            .PaddingTop = 4
            .PaddingBottom = 4
        End With
        table.AddCell(cellLblEmail)

        Dim cellValEmail As New PdfPCell(New Phrase(lo_DataRow.Item("emailApod_fam"), mo_FontSmall))
        With cellValEmail
            .Colspan = 7
            .Border = Rectangle.NO_BORDER
            .PaddingTop = 4
            .PaddingBottom = 4
        End With
        table.AddCell(cellValEmail)

        Dim cellLblRespPago As New PdfPCell(New Phrase("Responsable del Pago:", mo_FontSmallB))
        With cellLblRespPago
            .Colspan = 4
            .Border = Rectangle.NO_BORDER
            .PaddingTop = 4
            .PaddingBottom = 4
        End With
        table.AddCell(cellLblRespPago)

        Dim cellValRespPago As New PdfPCell(New Phrase(lo_DataRow.Item("indRespPagoApod_fam"), mo_FontSmall))
        With cellValRespPago
            .Colspan = 6
            .Border = Rectangle.NO_BORDER
            .PaddingTop = 4
            .PaddingBottom = 4
        End With
        table.AddCell(cellValRespPago)

        Return table
    End Function

    Private Function GenerarTitulo(ByVal texto As String) As PdfPCell
        Dim lo_FontTitulo As Font = FontFactory.GetFont("Arial", 9, Font.BOLD, BaseColor.WHITE)
        Dim cellTitulo As New PdfPCell(New Phrase(texto, lo_FontTitulo))
        With cellTitulo
            .HorizontalAlignment = PdfPCell.ALIGN_CENTER
            .VerticalAlignment = PdfPCell.ALIGN_MIDDLE
            .BackgroundColor = New BaseColor(Drawing.ColorTranslator.FromHtml("#E33439"))
        End With
        Return cellTitulo
    End Function

    Private Function GenerarSubTitulo(ByVal texto As String) As PdfPCell
        Dim lo_FontSubTitulo As Font = FontFactory.GetFont("Arial", 8, Font.BOLD, BaseColor.BLACK)
        Dim cellSubTitulo As New PdfPCell(New Phrase(texto, lo_FontSubTitulo))
        With cellSubTitulo
            .HorizontalAlignment = PdfPCell.ALIGN_CENTER
            .VerticalAlignment = PdfPCell.ALIGN_MIDDLE
            .BackgroundColor = New BaseColor(Drawing.ColorTranslator.FromHtml("#d6d6d6"))
        End With
        Return cellSubTitulo
    End Function
#End Region

End Class
