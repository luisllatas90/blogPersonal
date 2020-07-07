Imports iTextSharp.text
Imports System.IO
Imports iTextSharp.text.pdf

Partial Class academico_asistencia_frmRegistro
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Me.txtFechaAsistencia.Value = Date.Now.ToString.Substring(0, 10)
            CargaCombos()
        Else
            RefrescarGrilla()
        End If
    End Sub

    Private Sub ConsultarAcceso(ByVal codigo_tipo As Integer)
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Dim tb As New Data.DataTable
        tb = obj.TraerDataTable("Asistencia_ListarTipoAsistenciaAcceso", codigo_tipo, Request.QueryString("ctf"))

        Me.btnRegistrar.Visible = False
        Me.btnBorrar.Visible = False

        If tb.Rows.Count Then
            btnRegistrar.Visible = tb.Rows(0).Item("registrar")
            btnBorrar.Visible = tb.Rows(0).Item("administrar")
        End If
        obj.CerrarConexion()
        obj = Nothing
    End Sub

    Private Sub CargaCombos()
        Dim obj As New ClsConectarDatos
        Dim objfun As New ClsFunciones
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        objfun.CargarListas(Me.ddlTipoAsistencia, obj.TraerDataTable("Asistencia_ListarTipoAsistencia", Request.QueryString("ctf"), True), "codigo_tipo", "descripcion")
        objfun.CargarListas(Me.ddlSemestre, obj.TraerDataTable("Asistencia_ListarSemestres"), "codigo_cac", "descripcion_Cac")
        objfun.CargarListas(Me.ddlCarrera, obj.TraerDataTable("Asistencia_ListarCarreras"), "codigo_cpf", "nombre_cpf")
        objfun.CargarListas(Me.ddlModalidad, obj.TraerDataTable("Asistencia_ModalidadEstudio"), "codigo_min", "nombre_min")
        obj.CerrarConexion()
        obj = Nothing
        Me.btnBuscar.Visible = ddlTipoAsistencia.Items.Count
    End Sub



    Protected Sub btnBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBuscar.Click
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()

        ViewState("codigo_tipo") = Me.ddlTipoAsistencia.SelectedValue

        Dim tb As New Data.DataTable
        tb = obj.TraerDataTable("Asistencia_ListarAlumnos", Me.ddlEstado.SelectedValue, Me.ddlTipoAsistencia.SelectedValue, Me.ddlSemestre.SelectedValue, Me.ddlCarrera.SelectedValue, Me.ddlModalidad.SelectedValue, IIf(Me.txtTextoBuscar.Text.Trim = "", "%", txtTextoBuscar.Text.Trim))

        If tb.Rows.Count Then
            Me.gvData.DataSource = tb
        Else
            Me.gvData.DataSource = Nothing
        End If
        Me.gvData.DataBind()
        ConsultarAcceso(Me.ddlTipoAsistencia.SelectedValue)
        obj.CerrarConexion()
        obj = Nothing
    End Sub

    Protected Sub gvData_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvData.DataBound
        If gvData.Rows.Count > 0 Then
            gvData.UseAccessibleHeader = True
            gvData.HeaderRow.TableSection = TableRowSection.TableHeader
        End If
    End Sub

    Protected Sub gvData_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvData.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim fila As Data.DataRowView
            fila = e.Row.DataItem
            e.Row.Cells(1).Text = e.Row.RowIndex + 1

            CType(e.Row.FindControl("chkElegir"), CheckBox).Attributes.Add("OnClick", "PintarFilaMarcada(this.parentNode.parentNode,this.checked)")
            If Me.btnBorrar.Visible = False Then
                If e.Row.Cells(9).Text.Trim = "Ninguno" Then
                Else
                    e.Row.Cells(0).Text = ""
                End If
            End If

            Dim cellsRow As TableCellCollection = e.Row.Cells
            Dim index As Integer = e.Row.RowIndex + 1
            Dim columnas As Integer = gvData.Columns.Count
            Dim codigoAlu As String = gvData.DataKeys(e.Row.RowIndex).Values.Item("codigo_alu")

            Dim esTipoCertificado As Boolean = (ViewState("codigo_tipo") = 11) '11-> CERTIFICADO / TÍTULO
            Dim entregado As Boolean = IsDate(e.Row.Cells(9).Text.Trim)

            If esTipoCertificado AndAlso entregado Then
                'Generar Constancia
                Dim btnGenConst As New HtmlButton()
                With btnGenConst
                    .ID = "btnInfoPostulante" & index
                    .Attributes.Add("data-alu", codigoAlu)
                    .Attributes.Add("class", "btn btn-secondary btn-sm")
                    .Attributes.Add("type", "button")
                    .Attributes.Add("data-toggle", "tooltip")
                    .Attributes.Add("data-placement", "left")
                    .Attributes.Add("title", "Generar constancia")
                    .InnerHtml = "<i class='far fa-file-pdf'></i>"
                    AddHandler .ServerClick, AddressOf btnGenerarConstancia_Click
                End With
                cellsRow(columnas - 1).Controls.Add(btnGenConst)
            End If
        End If
    End Sub

    Protected Sub btnRegistrar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRegistrar.Click
        If Me.txtFechaAsistencia.Value.ToString.Length = 10 Then

            Dim Fila As GridViewRow
            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            Dim codigo_usu As Integer = Request.QueryString("id")

            Me.btnRegistrar.Enabled = False
            Try
                obj.AbrirConexion()
                For I As Int16 = 0 To Me.gvData.Rows.Count - 1
                    Fila = Me.gvData.Rows(I)
                    If Fila.RowType = DataControlRowType.DataRow Then
                        If Fila.FindControl("chkElegir") IsNot Nothing Then
                            If CType(Fila.FindControl("chkElegir"), CheckBox).Checked = True Then
                                obj.TraerDataTable("Asistencia_Registrar", Me.gvData.DataKeys.Item(Fila.RowIndex).Values("codigo_alu"), Me.ddlTipoAsistencia.SelectedValue, codigo_usu, Me.txtFechaAsistencia.Value, Me.ddlSemestre.SelectedValue)
                            End If
                        End If
                    End If
                Next
                obj.CerrarConexion()
                Me.btnRegistrar.Enabled = True

            Catch ex As Exception
                obj = Nothing
                Me.btnRegistrar.Enabled = True
            End Try
            btnBuscar_Click(sender, e)
        Else
            Response.Write("Debe ingresar la fecha de asistencia")
            Me.txtFechaAsistencia.Focus()
        End If

    End Sub

    Protected Sub btnBorrar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBorrar.Click
        Dim Fila As GridViewRow
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        Dim codigo_usu As Integer = Request.QueryString("id")

        Me.btnRegistrar.Enabled = False
        Try

            obj.AbrirConexion()
            For I As Int16 = 0 To Me.gvData.Rows.Count - 1
                Fila = Me.gvData.Rows(I)
                If Fila.RowType = DataControlRowType.DataRow Then

                    If CType(Fila.FindControl("chkElegir"), CheckBox).Checked = True Then
                        obj.TraerDataTable("Asistencia_Borrar", Me.gvData.DataKeys.Item(Fila.RowIndex).Values("codigo_alu"), Me.ddlTipoAsistencia.SelectedValue, codigo_usu, Me.ddlSemestre.SelectedValue)
                    End If
                End If
            Next
            obj.CerrarConexion()
            Me.btnRegistrar.Enabled = True

        Catch ex As Exception
            obj = Nothing
            Me.btnRegistrar.Enabled = True
        End Try
        btnBuscar_Click(sender, e)
    End Sub


    Protected Sub ddlTipoAsistencia_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlTipoAsistencia.SelectedIndexChanged
        Me.btnBorrar.Visible = False
        Me.btnRegistrar.Visible = False
        LimpiarGrilla()
    End Sub

    Private Sub btnGenerarConstancia_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim obj As New ClsConectarDatos

        Try
            Dim button As HtmlButton = DirectCast(sender, HtmlButton)
            Dim codigoAlu As String = button.Attributes("data-alu")

            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()

            Dim dtDatos As Data.DataTable = obj.TraerDataTable("ALU_DatosAlumno", codigoAlu)
            If dtDatos.Rows.Count > 0 Then
                If Request.Cookies("fileDownload") Is Nothing Then
                    Dim aCookie As New HttpCookie("fileDownload")
                    aCookie.Value = "true"
                    aCookie.Path = "/"
                    Response.Cookies.Add(aCookie)
                End If

                Dim apellidoPaterno As String = dtDatos.Rows(0).Item("apellidoPat_Alu")
                Dim apellidoMaterno As String = dtDatos.Rows(0).Item("apellidoMat_Alu")
                Dim nombres As String = dtDatos.Rows(0).Item("nombres_Alu")
                Dim nombreCompleto As String = apellidoPaterno & " " & apellidoMaterno & ", " & nombres
                Dim codigoUniv As String = dtDatos.Rows(0).Item("codigoUniver_Alu")

                Dim fontBold As Font = FontFactory.GetFont("Calibri", 12, Font.BOLD)
                Dim fontSimple As Font = FontFactory.GetFont("Calibri", 12, Font.NORMAL)
                Dim fontBoldMini As Font = FontFactory.GetFont("Calibri", 10, Font.BOLD)

                'GENERACIÓN DE PDF
                Dim ms As New MemoryStream
                Dim writer As PdfWriter

                Dim doc As New Document(PageSize.A4, 250, 85, 67, 50)
                doc.SetMargins(65.0F, 45.0F, 40.0F, 80.0F)
                writer = PdfWriter.GetInstance(doc, ms)

                doc.Open()

                Dim tblLayout As New PdfPTable(12)
                With tblLayout
                    .WidthPercentage = 100
                End With

                'Logo
                Dim rootPath As String = Server.MapPath("~")
                Dim oImagen As iTextSharp.text.Image = iTextSharp.text.Image.GetInstance(rootPath & "\Images\LogoOficial.png")
                oImagen.ScaleAbsoluteWidth(100)

                Dim cellLogo As New PdfPCell
                cellLogo.Border = Rectangle.NO_BORDER
                cellLogo.Colspan = 12
                cellLogo.AddElement(oImagen)
                tblLayout.AddCell(cellLogo)
                doc.Add(tblLayout)

                Dim prgTitulo As New Paragraph()
                prgTitulo.Add(New Chunk("CONSTANCIA DE ENTREGA DE CERTIFICADO DE ESTUDIOS", fontBold))
                With prgTitulo
                    .Alignment = Element.ALIGN_CENTER
                End With
                doc.Add(prgTitulo)
                doc.Add(New Paragraph(" "))

                Dim chkTipo As New Chunk("CERTIFICADO DE ESTUDIOS.", fontBold)

                Dim prgCuerpo As New Paragraph("Por el presente documento se hace constar que el estudiante: " & nombreCompleto & " con código: " & codigoUniv & " ha entregado en la oficina de admisión el ")
                With prgCuerpo
                    .Font = fontSimple
                    .Alignment = Element.ALIGN_JUSTIFIED
                End With
                prgCuerpo.Add(chkTipo)
                doc.Add(prgCuerpo)
                doc.Add(New Paragraph(" "))
                doc.Add(New Paragraph(" "))

                Dim fechaActual As Date = Date.Now
                Dim prgFecha As New Paragraph("Chiclayo, " & fechaActual.ToString("dd \de MMMM \del yyyy"))
                prgFecha.Alignment = Element.ALIGN_RIGHT
                doc.Add(prgFecha)
                doc.Add(New Paragraph(" "))
                doc.Add(New Paragraph(" "))
                doc.Add(New Paragraph(" "))
                doc.Add(New Paragraph(" "))
                doc.Add(New Paragraph(" "))
                doc.Add(New Paragraph(" "))

                Dim prgFirma As New Paragraph()
                With prgFirma
                    .Add(New Chunk("Oficina de Admisión", fontBold))
                    .Alignment = Element.ALIGN_CENTER
                End With

                Dim tblFirma As New PdfPTable(12)
                tblFirma.WidthPercentage = 100

                Dim cellEmpty As New PdfPCell
                With cellEmpty
                    .Colspan = 3
                    .Border = Rectangle.NO_BORDER
                End With

                Dim cellFirma As New PdfPCell
                With cellFirma
                    .Colspan = 6
                    .AddElement(New Paragraph(" "))
                    .AddElement(prgFirma)
                    .Border = Rectangle.TOP_BORDER
                End With
                tblFirma.AddCell(cellEmpty)
                tblFirma.AddCell(cellFirma)
                tblFirma.AddCell(cellEmpty)
                doc.Add(tblFirma)

                doc.Close()

                Response.Clear()
                Response.AddHeader("content-disposition", "attachment; filename=CONSTANCIA - " & codigoUniv & ".pdf")
                Response.ContentType = "application/pdf"
                Response.Buffer = True
                Response.OutputStream.Write(ms.GetBuffer(), 0, ms.GetBuffer().Length)
                Response.OutputStream.Flush()
                Response.End()
            End If

            obj.CerrarConexion()
        Catch ex As Exception
            obj = Nothing
            Throw (ex)
        End Try
    End Sub

    Private Sub LimpiarGrilla()
        gvData.DataSource = New Data.DataTable
        gvData.DataBind()
    End Sub


    Private Sub RefrescarGrilla()
        For Each _Row As GridViewRow In gvData.Rows
            gvData_RowDataBound(gvData, New GridViewRowEventArgs(_Row))
        Next
    End Sub
End Class
