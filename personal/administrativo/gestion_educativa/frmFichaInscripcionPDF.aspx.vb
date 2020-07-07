Imports iTextSharp.text
Imports System.IO
Imports iTextSharp.text.pdf

Partial Class administrativo_pec_test_frmFichaInscripcionPDF
    Inherits System.Web.UI.Page

#Region "Variables"
    Private mo_RepoAdmision As New ClsAdmision
    Private Shared mo_Font As Font = FontFactory.GetFont("Arial", 9)
    Private Shared mo_FontB As Font = FontFactory.GetFont("Arial", 9, Font.BOLD)
    Private Shared mo_FontSmall As Font = FontFactory.GetFont("Arial", 8)
    Private Shared mo_FontSmallB As Font = FontFactory.GetFont("Arial", 8, Font.BOLD)
    Private Shared mo_FontMini As Font = FontFactory.GetFont("Arial", 5)
    Private Sharedmo_FontMiniB As Font = FontFactory.GetFont("Arial", 6, Font.BOLD)
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
            doc.SetMargins(20.0F, 20.0F, 20.0F, 90.0F)
            writer = PdfWriter.GetInstance(doc, ms)
            writer.PageEvent = New PDFFooter()

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
            oImagen.ScaleAbsoluteWidth(90)

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

            Dim tblInfoEducativa As PdfPTable = TablaInfoEducativa(lo_DataRow)
            Dim cellInfoEducativa As New PdfPCell
            cellInfoEducativa.Colspan = 12
            cellInfoEducativa.AddElement(tblInfoEducativa)
            tblLayout.AddCell(cellInfoEducativa)
            tblLayout.AddCell(cellEmpty) : tblLayout.AddCell(cellEmpty)

            'If Not String.IsNullOrEmpty(lo_DataRow.Item("numeroDocIdentPadre_fam")) Then
            Dim tblInfoPadre As PdfPTable = TablaInfoPadre(lo_DataRow)
            Dim cellInfoPadre As New PdfPCell
            cellInfoPadre.Colspan = 12
            cellInfoPadre.AddElement(tblInfoPadre)
            tblLayout.AddCell(cellInfoPadre)
            tblLayout.AddCell(cellEmpty) : tblLayout.AddCell(cellEmpty)
            'End If

            'If Not String.IsNullOrEmpty(lo_DataRow.Item("numeroDocIdentMadre_fam")) Then
            Dim tblInfoMadre As PdfPTable = TablaInfoMadre(lo_DataRow)
            Dim cellInfoMadre As New PdfPCell
            cellInfoMadre.Colspan = 12
            cellInfoMadre.AddElement(tblInfoMadre)
            tblLayout.AddCell(cellInfoMadre)
            tblLayout.AddCell(cellEmpty) : tblLayout.AddCell(cellEmpty)
            'End If

            'If Not String.IsNullOrEmpty(lo_DataRow.Item("numeroDocIdentApod_fam")) Then
            Dim tblInfoApod As PdfPTable = TablaInfoApoderado(lo_DataRow)
            Dim cellInfoApod As New PdfPCell
            cellInfoApod.Colspan = 12
            cellInfoApod.AddElement(tblInfoApod)
            tblLayout.AddCell(cellInfoApod)

            tblLayout.AddCell(cellEmpty)
            tblLayout.AddCell(cellEmpty)

            Dim cellContinuara As New PdfPCell(New Phrase("CONTINUAR INDICACIONES EN PÁGINA 2", mo_FontSmallB))
            With cellContinuara
                .Colspan = 12
                .Border = Rectangle.NO_BORDER
            End With
            tblLayout.AddCell(cellContinuara)
            'End If

            doc.Add(tblLayout)

            'Nueva página - 07/05/2020
            doc.NewPage()

            Dim tblLayout2 As New PdfPTable(12)
            With tblLayout2
                .WidthPercentage = 100
            End With

            Dim tblManifiesto As PdfPTable = TablaManifiesto()
            Dim cellManifiesto As New PdfPCell
            cellManifiesto.Colspan = 12
            cellManifiesto.AddElement(tblManifiesto)
            tblLayout2.AddCell(cellManifiesto)
            tblLayout2.AddCell(cellEmpty)

            doc.Add(tblLayout2)

            doc.Close()

            Response.Clear()
            Response.AddHeader("content-disposition", "attachment; filename=FICHA DE INSCRIPCION - " & lo_DataRow.Item("nroDocIdent_Alu") & ".pdf")
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

        Dim cellLblCostCred As New PdfPCell(New Phrase("Costo de Crédito", mo_FontSmallB))
        cellLblCostCred.Colspan = 5
        cellLblCostCred.HorizontalAlignment = PdfPCell.ALIGN_CENTER
        table.AddCell(cellLblCostCred)

        Dim cellValCodEst As New PdfPCell(New Phrase(lo_DataRow.Item("codigoUniver_Alu"), mo_FontSmall))
        cellValCodEst.Colspan = 7
        cellValCodEst.HorizontalAlignment = PdfPCell.ALIGN_CENTER
        table.AddCell(cellValCodEst)

        Dim cellValCostCred As New PdfPCell(New Phrase(Decimal.Parse(lo_DataRow.Item("precioCreditoAct_Alu")).ToString("N2"), mo_FontSmall))
        cellValCostCred.Colspan = 5
        cellValCostCred.HorizontalAlignment = PdfPCell.ALIGN_CENTER
        table.AddCell(cellValCostCred)

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

        Dim cellTitulo As PdfPCell = GenerarSubTitulo("CARRERA PROFESIONAL")
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
            .Colspan = 17
            .Border = Rectangle.NO_BORDER
            .PaddingTop = 4
            .PaddingBottom = 4
        End With
        table.AddCell(cellValEmail)
        '-------------------------------------------------------------------------

        Dim cellLblDiscapacidad As New PdfPCell(New Phrase("Discapacidad:", mo_FontSmallB))
        With cellLblDiscapacidad
            .Colspan = 3
            .Border = Rectangle.NO_BORDER
            .PaddingTop = 4
            .PaddingBottom = 4
        End With
        table.AddCell(cellLblDiscapacidad)

        Dim la_Discapacidades As String() = lo_DataRow.Item("discapacidades_Dal").ToString.Split(",")
        Dim ln_Index As Integer = 0
        Dim ls_Discapacidades As String = ""
        For Each _letra As String In la_Discapacidades
            If ls_Discapacidades <> "" AndAlso ln_Index < (la_Discapacidades.Length) Then
                ls_Discapacidades &= ", "
            End If
            Select Case _letra
                Case "F"
                    ls_Discapacidades &= "Física"
                Case "S"
                    ls_Discapacidades &= "Sensorial"
                Case "A"
                    ls_Discapacidades &= "Auditiva"
                Case "V"
                    ls_Discapacidades &= "Visual"
                Case "I"
                    ls_Discapacidades &= "Intelectual"
            End Select
            ln_Index += 1
        Next
        Dim cellValDiscapacidades As New PdfPCell(New Phrase(ls_Discapacidades, mo_FontSmall))
        With cellValDiscapacidades
            .Colspan = 17
            .Border = Rectangle.NO_BORDER
            .PaddingTop = 4
            .PaddingBottom = 4
        End With
        table.AddCell(cellValDiscapacidades)

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

        Dim ls_FechaNacimientoPadre_fam As String = ""
        If Not String.IsNullOrEmpty(lo_DataRow.Item("numeroDocIdentPadre_fam")) AndAlso IsDate(lo_DataRow.Item("fechaNacimientoPadre_fam")) Then
            ls_FechaNacimientoPadre_fam = Date.Parse(lo_DataRow.Item("fechaNacimientoPadre_fam")).ToString("dd/MM/yyyy")
        End If
        Dim cellValFecNacimiento As New PdfPCell(New Phrase(ls_FechaNacimientoPadre_fam, mo_FontSmall))
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

        Dim ls_FechaNacimientoMadre_fam As String = ""
        If Not String.IsNullOrEmpty(lo_DataRow.Item("numeroDocIdentMadre_fam")) AndAlso IsDate(lo_DataRow.Item("fechaNacimientoMadre_fam")) Then
            ls_FechaNacimientoMadre_fam = Date.Parse(lo_DataRow.Item("fechaNacimientoMadre_fam")).ToString("dd/MM/yyyy")
        End If
        Dim cellValFecNacimiento As New PdfPCell(New Phrase(ls_FechaNacimientoMadre_fam, mo_FontSmall))
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

        Dim ls_FechaNacimientoApod_fam As String = ""
        If Not String.IsNullOrEmpty(lo_DataRow.Item("numeroDocIdentApod_fam")) AndAlso IsDate(lo_DataRow.Item("fechaNacimientoApod_fam")) Then
            ls_FechaNacimientoApod_fam = Date.Parse(lo_DataRow.Item("fechaNacimientoApod_fam")).ToString("dd/MM/yyyy")
        End If
        Dim cellValFecNacimiento As New PdfPCell(New Phrase(ls_FechaNacimientoApod_fam, mo_FontSmall))
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

    Private Function TablaManifiesto() As PdfPTable
        Dim cellEmptySpace As New PdfPCell
        With cellEmptySpace
            .Colspan = 100
            .Border = Rectangle.NO_BORDER
        End With

        Dim table As New PdfPTable(100)
        With table
            .WidthPercentage = 100
        End With

        Dim cellTitulo As New PdfPCell(New Phrase("MANIFIESTO BAJO JURAMENTO QUE LOS DATOS PERSONALES ESTIPULADOS EN LA PÁGINA 1 SON FIDEDIGNOS.", mo_FontB))
        With cellTitulo
            .Colspan = 100
            .Border = Rectangle.NO_BORDER
            .PaddingLeft = 10
            .PaddingTop = 10
            .PaddingRight = 10
        End With
        table.AddCell(cellTitulo)
        table.AddCell(cellEmptySpace)

        Dim subtitulo As New Phrase("AUTORIZACIÓN PARA EL TRATAMIENTO DE DATOS PERSONALES", mo_FontB)
        Dim cellSubTitulo As New PdfPCell()
        With cellSubTitulo
            .Colspan = 100
            .Border = Rectangle.NO_BORDER
            .PaddingLeft = 10
            .PaddingRight = 10
            .AddElement(subtitulo)
        End With
        table.AddCell(cellSubTitulo)
        table.AddCell(cellEmptySpace)

        Dim parrafo As New Paragraph("De conformidad con la Ley N° 29733, Ley de Protección de Datos Personales, autorizo de forma libre, previa, expresa e inequívoca a la USAT a utilizar los datos personales proporcionados en la presente ficha de inscripción, para las siguientes finalidades: gestión administrativa para comunicar sobre el proceso de admisión y comercial a fin de ofrecer servicios académicos que realice, para el envío de información, publicidad, encuestas y estadísticas de servicios educativos.", mo_Font)
        parrafo.Alignment = Element.ALIGN_JUSTIFIED
        Dim cellParrafo As New PdfPCell(parrafo)
        With cellParrafo
            .Colspan = 100
            .Border = Rectangle.NO_BORDER
            .PaddingLeft = 10
            .PaddingRight = 10
        End With
        table.AddCell(cellParrafo)
        table.AddCell(cellEmptySpace)

        parrafo = New Paragraph("Dichos datos serán almacenados en la Base de Datos correspondiente de la USAT.", mo_Font)
        parrafo.Alignment = Element.ALIGN_JUSTIFIED
        cellParrafo = New PdfPCell(parrafo)
        With cellParrafo
            .Colspan = 100
            .Border = Rectangle.NO_BORDER
            .PaddingLeft = 10
            .PaddingRight = 10
        End With
        table.AddCell(cellParrafo)
        table.AddCell(cellEmptySpace)

        parrafo = New Paragraph("Los datos personales solicitados son necesarios para el cumplimiento de los fines detallados anteriormente y sin la autorización para su tratamiento, la USAT no podría dar la adecuada atención a los servicios o trámites ofrecidos.", mo_Font)
        parrafo.Alignment = Element.ALIGN_JUSTIFIED
        cellParrafo = New PdfPCell(parrafo)
        With cellParrafo
            .Colspan = 100
            .Border = Rectangle.NO_BORDER
            .PaddingLeft = 10
            .PaddingRight = 10
        End With
        table.AddCell(cellParrafo)
        table.AddCell(cellEmptySpace)

        parrafo = New Paragraph("Asimismo, declaro que tengo pleno conocimiento que puedo acceder, rectificar, oponerme y cancelar mis datos personales y ejercer, así como revocar mi consentimiento. Para ello, podré enviar un correo a: informes.admision@usat.edu.pe.", mo_Font)
        parrafo.Alignment = Element.ALIGN_JUSTIFIED
        cellParrafo = New PdfPCell(parrafo)
        With cellParrafo
            .Colspan = 100
            .Border = Rectangle.NO_BORDER
            .PaddingLeft = 10
            .PaddingRight = 10
        End With
        table.AddCell(cellParrafo)
        table.AddCell(cellEmptySpace)

        parrafo = New Paragraph("Declaro que mis datos personales podrán ser tratados por la USAT.", mo_Font)
        parrafo.Alignment = Element.ALIGN_JUSTIFIED
        cellParrafo = New PdfPCell(parrafo)
        With cellParrafo
            .Colspan = 100
            .Border = Rectangle.NO_BORDER
            .PaddingLeft = 10
            .PaddingRight = 10
        End With
        table.AddCell(cellParrafo)
        table.AddCell(cellEmptySpace)

        parrafo = New Paragraph("Tengo conocimiento que la USAT podrá conservar mis datos personales hasta que los mismos le sean útiles para las finalidades antes indicadas, salvo que revoque mi consentimiento de forma previa.", mo_Font)
        parrafo.Alignment = Element.ALIGN_JUSTIFIED
        cellParrafo = New PdfPCell(parrafo)
        With cellParrafo
            .Colspan = 100
            .Border = Rectangle.NO_BORDER
            .PaddingLeft = 10
            .PaddingRight = 10
        End With
        table.AddCell(cellParrafo)
        table.AddCell(cellEmptySpace)

        'TERMINOS Y CONDICIONES
        Dim cellTerminos As New PdfPCell()
        With cellTerminos
            .Colspan = 100
            .Rowspan = 2
            .PaddingLeft = 10
            .PaddingRight = 10
            .PaddingBottom = 10
            .PaddingTop = 10
        End With

        Dim lblTenerEnCuenta As New Phrase("IMPORTANTE", mo_FontB)
        cellTerminos.AddElement(lblTenerEnCuenta)
        cellTerminos.Border = Rectangle.NO_BORDER

        Dim yLeading As Integer = 10
        Dim ySpacing As Integer = 5

        Dim uoList As New List(List.UNORDERED)
        Dim liCustom As New ListItem()
        liCustom.SetLeading(yLeading, 0)
        Dim pCustom As New Paragraph("El ingresante deberá pagar su matrícula en el plazo establecido por la dirección de Admisión y Marketing, ", mo_Font)
        pCustom.SetLeading(yLeading, 0)
        pCustom.Add(New Phrase("caso contrario pierde su vacante y se dará pase a los accesitarios.", mo_FontB))
        liCustom.Add(pCustom)

        Dim p1 As New Paragraph("El postulante que se inscribe a un proceso de admisión y paga su derecho para rendir el examen, en caso posteriormente desista de su postulación, NO TENDRÁ DERECHO A DEVOLUCIÓN del monto pagado.", mo_Font)
        p1.SetLeading(yLeading, 0)
        p1.SpacingBefore = ySpacing
        p1.SpacingAfter = ySpacing

        Dim p2 As New Paragraph("El postulante que llegue tarde o no asista a rendir el examen de admisión presencial o virtual, NO TENDRÁ DERECHO A DEVOLUCIÓN del monto pagado y tampoco está permitido reprogramar el examen.", mo_Font)
        p2.SetLeading(yLeading, 0)
        p2.SpacingAfter = ySpacing

        Dim p3 As New Paragraph("El costo de crédito está de acuerdo al costo actual de la pensión del colegio de procedencia, el mismo que está sujeto a variación. Se le hace de conocimiento que para las carreras de Medicina y Odontología no aplica categorización.", mo_Font)
        p3.SetLeading(yLeading, 0)
        p3.SpacingAfter = ySpacing

        Dim p4 As New Paragraph("El postulante podrá cambiar de carrera a la que postula hasta tres (03) días antes del cierre de inscripción al examen de admisión.", mo_Font)
        p4.SetLeading(yLeading, 0)
        p4.SpacingAfter = ySpacing

        Dim p5 As New Paragraph("La carrera profesional se abre con número mínimo de estudiantes matriculados estipulados por la USAT.", mo_Font)
        p5.SetLeading(yLeading, 0)
        p5.SpacingAfter = ySpacing

        Dim p6 As New Paragraph("Los que NO logren INGRESAR A LA USAT, y presentaron certificado de estudios en físico podrán solicitar su devolución en un plazo máximo de seis (06) meses, caso contrario, la USAT no se responsabiliza por la entrega del mismo.", mo_Font)
        p6.SetLeading(yLeading, 0)
        p6.SpacingAfter = ySpacing

        Dim p7 As New Paragraph("El ingresante que paga su derecho de matrícula para asegurar su vacante y luego desiste de continuar sus estudios en el ciclo al que ingresó, NO TENDRÁ DERECHO A DEVOLUCIÓN del monto pagado y tampoco a RESERVAR SU VACANTE.", mo_Font)
        p7.SetLeading(yLeading, 0)
        p7.SpacingAfter = ySpacing

        Dim p9 As New Paragraph("El ingresante que pagó matrícula y luego solicita su cambio de carrera, la Dirección de Admisión y Marketing evaluará si procede o no, de acuerdo a la disponibilidad de vacantes y en base al perfil de ingreso. En caso de NO haber vacantes para el cambio de carrera y de haber otro evento de admisión, se le invitará a postular a la nueva carrera, pagando los derechos respectivos. Si logra ingresar nuevamente, tendrá que renunciar a la primera carrera a la que ingresó, pagar el total de la matrícula a la nueva carrera, y luego se le gestionará la devolución del pago que realizó por derecho de matrícula a la primera carrera.", mo_Font)
        p9.SetLeading(yLeading, 0)
        p9.SpacingBefore = ySpacing
        p9.SpacingAfter = ySpacing

        Dim p10 As New Paragraph("CONSENTIMIENTO INFORMADO: Los padres o apoderados del estudiante matriculado en caso de ser menores de edad, AUTORIZAN A LA USAT, la aplicación del Examen Psicológico y Examen Médico, como requisitos del proceso de admisión.", mo_FontB)
        p10.SetLeading(yLeading, 0)
        p10.SpacingAfter = ySpacing

        With uoList
            .SetListSymbol("•   ")
            .Add(New ListItem(p1))
            .Add(New ListItem(p2))
            .Add(New ListItem(p3))
            .Add(New ListItem(p4))
            .Add(New ListItem(p5))
            .Add(New ListItem(p6))
            .Add(New ListItem(p7))
            .Add(New ListItem(liCustom))
            .Add(New ListItem(p9))
            .Add(New ListItem(p10))
        End With
        cellTerminos.AddElement(uoList)
        table.AddCell(cellTerminos)

        table.AddCell(cellEmptySpace)
        table.AddCell(cellEmptySpace)
        table.AddCell(cellEmptySpace)
        table.AddCell(cellEmptySpace)
        table.AddCell(cellEmptySpace)
        table.AddCell(cellEmptySpace)
        table.AddCell(cellEmptySpace)
        table.AddCell(cellEmptySpace)
        table.AddCell(cellEmptySpace)
        table.AddCell(cellEmptySpace)
        table.AddCell(cellEmptySpace)
        table.AddCell(cellEmptySpace)
        table.AddCell(cellEmptySpace)
        table.AddCell(cellEmptySpace)
        table.AddCell(cellEmptySpace)
        table.AddCell(cellEmptySpace)
        table.AddCell(cellEmptySpace)
        table.AddCell(cellEmptySpace)
        table.AddCell(cellEmptySpace)
        table.AddCell(cellEmptySpace)
        table.AddCell(cellEmptySpace)


        'AUTORIZACIÓN
        'cellEmptySpace = New PdfPCell
        cellEmptySpace.Colspan = 30
        table.AddCell(cellEmptySpace)

        Dim tblFirma As PdfPTable = TablaFirma()
        Dim cellAutorizacion As New PdfPCell()
        With cellAutorizacion
            .Colspan = 40
            .Border = Rectangle.NO_BORDER
            .AddElement(tblFirma)
        End With
        table.AddCell(cellAutorizacion)

        cellEmptySpace.Colspan = 30
        table.AddCell(cellEmptySpace)

        cellEmptySpace.Colspan = 100
        table.AddCell(cellEmptySpace)
        table.AddCell(cellEmptySpace)
        table.AddCell(cellEmptySpace)
        table.AddCell(cellEmptySpace)

        Return table
    End Function

    Public Shared Function TablaFirma() As PdfPTable
        Dim tblFirma As New PdfPTable(100)
        tblFirma.WidthPercentage = 100

        Dim cellEmptySpace As New PdfPCell
        With cellEmptySpace
            .Colspan = 100
            .Border = Rectangle.NO_BORDER
        End With

        Dim dottedColor As BaseColor = New BaseColor(Drawing.ColorTranslator.FromHtml("#6c757d"))
        Dim lo_FontSubTitulo As Font = FontFactory.GetFont("Arial", 7.5, Font.BOLD, BaseColor.BLACK)
        Dim cellFirma As New PdfPCell(New Phrase("Autorización-Firma", lo_FontSubTitulo))
        With cellFirma
            .Colspan = 100
            .HorizontalAlignment = PdfPCell.ALIGN_CENTER
            .VerticalAlignment = PdfPCell.ALIGN_MIDDLE
            .Border = Rectangle.TOP_BORDER
            .BorderColor = New BaseColor(Drawing.ColorTranslator.FromHtml("#E33439"))
        End With
        tblFirma.AddCell(cellFirma)

        Dim cellLblDNI As New PdfPCell(New Phrase("D.N.I.:", mo_FontSmallB))
        With cellLblDNI
            .Colspan = 40
            .PaddingTop = 4
            .Border = Rectangle.NO_BORDER
        End With
        tblFirma.AddCell(cellLblDNI)

        Dim cellValDNI As New PdfPCell()
        With cellValDNI
            .Colspan = 60
            .Border = Rectangle.BOTTOM_BORDER
            .BorderColor = dottedColor
        End With
        tblFirma.AddCell(cellValDNI)

        tblFirma.AddCell(cellEmptySpace)

        Dim cellLblNombre As New PdfPCell(New Phrase("Nombre y Apellidos:", mo_FontSmallB))
        With cellLblNombre
            .Colspan = 40
            .PaddingTop = 4
            .Border = Rectangle.NO_BORDER
        End With
        tblFirma.AddCell(cellLblNombre)

        Dim cellValNombre As New PdfPCell()
        With cellValNombre
            .Colspan = 60
            .Border = Rectangle.BOTTOM_BORDER
            .BorderColor = dottedColor
        End With
        tblFirma.AddCell(cellValNombre)

        tblFirma.AddCell(cellEmptySpace)

        Dim cellValNombre2 As New PdfPCell()
        With cellValNombre2
            .Colspan = 100
            .PaddingTop = 10
            .Border = Rectangle.BOTTOM_BORDER
            .BorderColor = dottedColor
        End With
        tblFirma.AddCell(cellValNombre2)
        Return tblFirma
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

        Dim cellEmpty As New PdfPCell
        With cellEmpty
            .Border = Rectangle.NO_BORDER
        End With

        'PÁGINA
        Dim numPag As Integer = writer.PageNumber
        Dim cellPagina As New PdfPCell(New Phrase("Página " & numPag, mo_FontSmallB))

        If numPag = 1 Then
            'AUTORIZACIÓN
            cellEmpty.Colspan = 30
            tblFooter.AddCell(cellEmpty)

            Dim cellAutorizacion As New PdfPCell()
            With cellAutorizacion
                .Colspan = 38
                .Padding = 0
                .Border = Rectangle.NO_BORDER
                .AddElement(administrativo_pec_test_frmFichaInscripcionPdf.TablaFirma())
            End With
            tblFooter.AddCell(cellAutorizacion)
            cellEmpty.Colspan = 32
            tblFooter.AddCell(cellEmpty)

            With cellPagina
                .Colspan = 100
                .Border = Rectangle.NO_BORDER
                .PaddingLeft = 10
                .PaddingRight = 10
                .PaddingTop = 7
                .HorizontalAlignment = PdfPCell.ALIGN_RIGHT
            End With
        Else
            With cellPagina
                .Colspan = 100
                .Border = Rectangle.NO_BORDER
                .PaddingLeft = 10
                .PaddingRight = 10
                .PaddingTop = 68
                .HorizontalAlignment = PdfPCell.ALIGN_RIGHT
            End With
        End If

        tblFooter.AddCell(cellPagina)

        tblFooter.WriteSelectedRows(0, -1, 15, document.Bottom, writer.DirectContent)
    End Sub
End Class
