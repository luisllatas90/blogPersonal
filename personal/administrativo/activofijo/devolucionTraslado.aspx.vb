
Partial Class administrativo_activofijo_devolucionTraslado
    Inherits System.Web.UI.Page

#Region "Declaracion de Varibles"

    Dim cod_ctf As Integer
    Dim cod_user As Integer

    Public Enum MessageType
        Success
        [Error]
        Info
        Warning
    End Enum

#End Region

#Region "Eventos"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If (Session("id_per") Is Nothing) Then
                Response.Redirect("../../../sinacceso.html")
            Else
                If (Request.QueryString("id") <> "") Then
                    Dim dt As New Data.DataTable
                    Dim obj As New ClsConectarDatos
                    cod_user = Request.QueryString("id")
                    Me.hdCodUser.Value = cod_user
                    obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
                    obj.AbrirConexion()
                    dt = obj.TraerDataTable("SEG_ListaPersonal", cod_user)
                    If dt.Rows.Count > 0 Then
                        Me.hdNomUser.Value = dt.Rows(0).Item("trabajador")
                        Me.hdCcoUser.Value = dt.Rows(0).Item("codigo_Cco")
                        Me.hdEmaUser.Value = dt.Rows(0).Item("usuario_per").ToString + "@usat.edu.pe"
                    End If
                    obj.CerrarConexion()
                    obj = Nothing
                End If
            End If
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub btnBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBuscar.Click
        Try
            mt_CargarDatos()
            'mt_ShowMessage("La búsqueda se realizo correctamente", MessageType.Success)
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub btnDevolver_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Page.RegisterStartupScript("Pop", "<script>openModal();</script>")
    End Sub

    Protected Sub gvTraslado_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvTraslado.RowCommand
        Try
            Dim index As Integer
            index = CInt(e.CommandArgument)
            If e.CommandName = "Devolver" Then
                Me.hdCodTraslado.Value = Me.gvTraslado.DataKeys(index).Values("codigo_tld")
                Me.txtFecha.Text = Now.Date.ToShortDateString
                Me.hdCodPer.Value = Me.gvTraslado.DataKeys(index).Values("codigo_per")
                Me.txtAsignado.Text = Me.gvTraslado.DataKeys(index).Values("responsable")
                Me.hdCcoPer.Value = Me.gvTraslado.DataKeys(index).Values("codigo_Cco")
                Me.txtCentroCosto.Text = Me.gvTraslado.DataKeys(index).Values("descripcion_Cco")
            End If
            If e.CommandName = "Ver" Then
                Dim codigo_tld As Integer
                codigo_tld = CInt(Me.gvTraslado.DataKeys(index).Values("codigo_tld"))
                Dim obj As New ClsConectarDatos
                Dim dtDet As New Data.DataTable
                obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
                obj.AbrirConexion()
                dtDet = obj.TraerDataTable("AF_DetalleTrasladoAF_V2", codigo_tld)
                obj.CerrarConexion()
                Me.gvDetalle.DataSource = dtDet
                Me.gvDetalle.DataBind()
            End If
        Catch ex As Exception
            Response.Write(ex.Message & " " & ex.StackTrace)
        End Try
    End Sub

    Protected Sub btnGrabar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGrabar.Click
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        Dim tbDet As New Data.DataTable
        Dim nro_corr As String = ""
        Dim nro_traslado As Integer
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cod_user = Request.QueryString("id")
        Try
            obj.AbrirConexion()
            dt = obj.TraerDataTable("AF_devolverTraslado", Me.hdCodTraslado.Value, CDate(Me.txtFecha.Text), Me.hdCodUser.Value, Me.hdCcoUser.Value, Me.hdCodPer.Value, Me.hdCcoPer.Value, Me.hdCodUba.Value, Me.txtObservacion.Text, cod_user)
            If dt.Rows.Count > 0 Then
                nro_traslado = dt.Rows(0).Item(0)
                nro_corr = dt.Rows(0).Item(1)
                tbDet = obj.TraerDataTable("AF_DetalleTrasladoAF_V2", nro_traslado)
            End If
            obj.CerrarConexion()

            Dim pdfDoc As New iTextSharp.text.Document()
            Dim memory As New System.IO.MemoryStream
            Dim pdfWrite As iTextSharp.text.pdf.PdfWriter = iTextSharp.text.pdf.PdfWriter.GetInstance(pdfDoc, memory)

            pdfDoc.Open()

            Dim pdfTable As iTextSharp.text.pdf.PdfPTable = New iTextSharp.text.pdf.PdfPTable(7)
            pdfTable.WidthPercentage = 100.0F

            ' Titulo
            Dim titleTab As iTextSharp.text.pdf.PdfPCell = New iTextSharp.text.pdf.PdfPCell(New iTextSharp.text.Phrase("FICHA DE RETIRO DEL ACTIVO FIJO", New iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 9.0F, iTextSharp.text.Font.BOLD)))
            titleTab.Colspan = 7
            titleTab.HorizontalAlignment = 1
            pdfTable.AddCell(titleTab)

            ' Cabecera
            Dim srcIcon As String = Server.MapPath(".") & "/logo_usat.png"
            Dim usatIcon As iTextSharp.text.Image = iTextSharp.text.Image.GetInstance(srcIcon)
            usatIcon.ScalePercent(40.0F)
            usatIcon.Alignment = iTextSharp.text.Element.ALIGN_LEFT

            Dim cellIcon As iTextSharp.text.pdf.PdfPCell = New iTextSharp.text.pdf.PdfPCell(usatIcon)
            cellIcon.HorizontalAlignment = 1
            cellIcon.VerticalAlignment = 2
            cellIcon.Rowspan = 2
            pdfTable.AddCell(cellIcon)

            Dim cellCompany As iTextSharp.text.pdf.PdfPCell = New iTextSharp.text.pdf.PdfPCell(New iTextSharp.text.Phrase("UNIVERSIDAD CATOLICA SANTO TORIBIO DE MOGROVEJO" & Environment.NewLine & _
                                                      "RUC: 20395492129" & Environment.NewLine & _
                                                      "Av. San Josemaría Escrivá de Balaguer Nº 855 Chiclayo - Perú", New iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8.0F)))
            cellCompany.Colspan = 5
            cellCompany.Rowspan = 2
            pdfTable.AddCell(cellCompany)

            Dim cellNro As iTextSharp.text.pdf.PdfPCell
            cellNro = New iTextSharp.text.pdf.PdfPCell(New iTextSharp.text.Phrase("FICHA N° " & Environment.NewLine & _
                                                                                  nro_corr, New iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7.0F)))
            pdfTable.AddCell(cellNro)

            Dim cellDate As iTextSharp.text.pdf.PdfPCell
            cellDate = New iTextSharp.text.pdf.PdfPCell(New iTextSharp.text.Phrase("FECHA: " & Environment.NewLine _
                                                                                   & Now.Date, New iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7.0F)))
            pdfTable.AddCell(cellDate)

            ' Informacion
            Dim cellInfo As iTextSharp.text.pdf.PdfPCell = New iTextSharp.text.pdf.PdfPCell(New iTextSharp.text.Phrase("INFORMACIÓN GENERAL", New iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8.0F, iTextSharp.text.Font.BOLD)))
            cellInfo.Colspan = 7
            cellInfo.HorizontalAlignment = 1
            pdfTable.AddCell(cellInfo)

            ' Solicitante
            Dim cellI1 As iTextSharp.text.pdf.PdfPCell = New iTextSharp.text.pdf.PdfPCell(New iTextSharp.text.Phrase("SOLICITANTE:", New iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7.0F, iTextSharp.text.Font.BOLD)))
            cellI1.Colspan = 1
            pdfTable.AddCell(cellI1)

            Dim cellI1Data As iTextSharp.text.pdf.PdfPCell = New iTextSharp.text.pdf.PdfPCell(New iTextSharp.text.Phrase(Me.txtAsignado.Text, New iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7.0F)))
            cellI1Data.Colspan = 5
            pdfTable.AddCell(cellI1Data)

            ' Otros
            Dim cellOtros As iTextSharp.text.pdf.PdfPCell
            cellOtros = New iTextSharp.text.pdf.PdfPCell(New iTextSharp.text.Phrase("OBSERVACION:" & Environment.NewLine & _
                                                    Me.txtObservacion.Text, New iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7.0F)))
            cellOtros.Colspan = 1
            cellOtros.Rowspan = 10
            pdfTable.AddCell(cellOtros)

            ' Area
            Dim cellI2 As iTextSharp.text.pdf.PdfPCell = New iTextSharp.text.pdf.PdfPCell(New iTextSharp.text.Phrase("AREA SOLICITANTE:", New iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7.0F, iTextSharp.text.Font.BOLD)))
            cellI2.Colspan = 1
            pdfTable.AddCell(cellI2)

            Dim cellI2Data As iTextSharp.text.pdf.PdfPCell = New iTextSharp.text.pdf.PdfPCell(New iTextSharp.text.Phrase(Me.txtCentroCosto.Text, New iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7.0F)))
            cellI2Data.Colspan = 5
            pdfTable.AddCell(cellI2Data)

            ' Ubicacion
            Dim cellI3 As iTextSharp.text.pdf.PdfPCell = New iTextSharp.text.pdf.PdfPCell(New iTextSharp.text.Phrase("UBICACION FISICA:", New iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7.0F, iTextSharp.text.Font.BOLD)))
            cellI3.Colspan = 1
            pdfTable.AddCell(cellI3)

            Dim cellI3Data As iTextSharp.text.pdf.PdfPCell = New iTextSharp.text.pdf.PdfPCell(New iTextSharp.text.Phrase(Me.txtUbicacion.Text, New iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7.0F)))
            cellI3Data.Colspan = 5
            pdfTable.AddCell(cellI3Data)

            ' Tipo Espacio
            Dim cellI4 As iTextSharp.text.pdf.PdfPCell = New iTextSharp.text.pdf.PdfPCell(New iTextSharp.text.Phrase("TIPO DE ESPACIO:", New iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7.0F, iTextSharp.text.Font.BOLD)))
            cellI4.Colspan = 1
            pdfTable.AddCell(cellI4)

            Dim cellI4Data As iTextSharp.text.pdf.PdfPCell = New iTextSharp.text.pdf.PdfPCell(New iTextSharp.text.Phrase("", New iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7.0F)))
            cellI4Data.Colspan = 5
            pdfTable.AddCell(cellI4Data)

            ' Fecha Retiro
            Dim cellI5 As iTextSharp.text.pdf.PdfPCell = New iTextSharp.text.pdf.PdfPCell(New iTextSharp.text.Phrase("FECHA DE RETIRO:", New iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7.0F, iTextSharp.text.Font.BOLD)))
            cellI5.Colspan = 1
            pdfTable.AddCell(cellI5)

            Dim cellI5Data As iTextSharp.text.pdf.PdfPCell = New iTextSharp.text.pdf.PdfPCell(New iTextSharp.text.Phrase(CDate(Me.txtFecha.Text), New iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7.0F)))
            cellI5Data.Colspan = 5
            pdfTable.AddCell(cellI5Data)

            ' Hora Retiro
            Dim cellI6 As iTextSharp.text.pdf.PdfPCell = New iTextSharp.text.pdf.PdfPCell(New iTextSharp.text.Phrase("HORA DE RETIRO:", New iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7.0F, iTextSharp.text.Font.BOLD)))
            cellI6.Colspan = 1
            pdfTable.AddCell(cellI6)

            Dim cellI6Data As iTextSharp.text.pdf.PdfPCell = New iTextSharp.text.pdf.PdfPCell(New iTextSharp.text.Phrase("", New iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7.0F)))
            cellI6Data.Colspan = 5
            pdfTable.AddCell(cellI6Data)

            ' Fecha Devolucion
            Dim cellI7 As iTextSharp.text.pdf.PdfPCell = New iTextSharp.text.pdf.PdfPCell(New iTextSharp.text.Phrase("FECHA DEVOLUCION(*):", New iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7.0F, iTextSharp.text.Font.BOLD)))
            cellI7.Colspan = 1
            pdfTable.AddCell(cellI7)

            Dim cellI7Data As iTextSharp.text.pdf.PdfPCell = New iTextSharp.text.pdf.PdfPCell(New iTextSharp.text.Phrase("", New iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7.0F)))
            cellI7Data.Colspan = 5
            pdfTable.AddCell(cellI7Data)

            ' Retiro Interno
            Dim cellI8 As iTextSharp.text.pdf.PdfPCell = New iTextSharp.text.pdf.PdfPCell(New iTextSharp.text.Phrase("RETIRO INTERNO:", New iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7.0F, iTextSharp.text.Font.BOLD)))
            cellI8.Colspan = 1
            pdfTable.AddCell(cellI8)

            Dim tipoT As String = "I"
            Dim motivoT As String = "2"
            Dim cellI8Data As iTextSharp.text.pdf.PdfPCell
            cellI8Data = New iTextSharp.text.pdf.PdfPCell(New iTextSharp.text.Phrase(" MANTENIMIENTO ( " & IIf(tipoT = "I" And motivoT = "2", "X", "") & _
                                                     " )        PRESTAMO ( " & IIf(tipoT = "I" And motivoT = "3", "X", "") & _
                                                     " )        REASIGNACION ( " & IIf(tipoT = "I" And motivoT = "1", "X", "") & _
                                                     " ) ", New iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7.0F)))
            cellI8Data.Colspan = 5
            pdfTable.AddCell(cellI8Data)

            ' Retiro Externo
            Dim cellI9 As iTextSharp.text.pdf.PdfPCell = New iTextSharp.text.pdf.PdfPCell(New iTextSharp.text.Phrase("RETIRO EXTERNO:", New iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7.0F, iTextSharp.text.Font.BOLD)))
            cellI9.Colspan = 1
            pdfTable.AddCell(cellI9)

            Dim cellI9Data As iTextSharp.text.pdf.PdfPCell
            cellI9Data = New iTextSharp.text.pdf.PdfPCell(New iTextSharp.text.Phrase(" MANTENIMIENTO ( " & IIf(tipoT = "E" And motivoT = "2", "X", "") & _
                                                     " )        PRESTAMO ( " & IIf(tipoT = "E" And motivoT = "3", "X", "") & _
                                                     " )        REASIGNACION ( " & IIf(tipoT = "E" And motivoT = "1", "X", "") & _
                                                     " ) ", New iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7.0F)))
            cellI9Data.Colspan = 5
            pdfTable.AddCell(cellI9Data)

            ' Documento Relacionado
            Dim cellI10 As iTextSharp.text.pdf.PdfPCell
            cellI10 = New iTextSharp.text.pdf.PdfPCell(New iTextSharp.text.Phrase("DOCUMENTO RELACIONADO(**):", New iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7.0F, iTextSharp.text.Font.BOLD)))
            cellI10.Colspan = 2
            pdfTable.AddCell(cellI10)

            Dim nro_corr_ant As String = ""

            If tbDet.Rows.Count > 0 Then
                nro_corr_ant = tbDet.Rows(0).Item(12)
            End If

            Dim cellI10Data As iTextSharp.text.pdf.PdfPCell
            cellI10Data = New iTextSharp.text.pdf.PdfPCell(New iTextSharp.text.Phrase(nro_corr_ant, New iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7.0F)))
            cellI10Data.Colspan = 4
            pdfTable.AddCell(cellI10Data)

            ' Notas (*)
            Dim cellNotas As iTextSharp.text.pdf.PdfPCell
            cellNotas = New iTextSharp.text.pdf.PdfPCell(New iTextSharp.text.Phrase(" " & Environment.NewLine & _
                                                    " (*)  SI ES POR ELIMINACIÓN NO CONSIDERAR" & Environment.NewLine & _
                                                    " (**) EN CASO" & Environment.NewLine & _
                                                    " ", New iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7.0F, iTextSharp.text.Font.BOLD)))
            cellNotas.Border = 0
            cellNotas.Colspan = 7
            pdfTable.AddCell(cellNotas)

            ' Descripcion del Bien
            Dim cellDescripcion As iTextSharp.text.pdf.PdfPCell = New iTextSharp.text.pdf.PdfPCell(New iTextSharp.text.Phrase("DESCRIPCIÓN DEL BIEN", New iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8.0F, iTextSharp.text.Font.BOLD)))
            cellDescripcion.Colspan = 7
            cellDescripcion.HorizontalAlignment = 1
            pdfTable.AddCell(cellDescripcion)

            ' Codigo del Inventario
            Dim cellDC1 As iTextSharp.text.pdf.PdfPCell = New iTextSharp.text.pdf.PdfPCell(New iTextSharp.text.Phrase("CODIGO DEL INVENTARIO", New iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7.0F, iTextSharp.text.Font.BOLD)))
            cellDC1.HorizontalAlignment = 1
            pdfTable.AddCell(cellDC1)

            ' Descripcion
            Dim cellDC2 As iTextSharp.text.pdf.PdfPCell = New iTextSharp.text.pdf.PdfPCell(New iTextSharp.text.Phrase("DESCRIPCIÓN", New iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7.0F, iTextSharp.text.Font.BOLD)))
            cellDC2.HorizontalAlignment = 1
            pdfTable.AddCell(cellDC2)

            ' Marca
            Dim cellDC3 As iTextSharp.text.pdf.PdfPCell = New iTextSharp.text.pdf.PdfPCell(New iTextSharp.text.Phrase("MARCA", New iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7.0F, iTextSharp.text.Font.BOLD)))
            cellDC3.HorizontalAlignment = 1
            pdfTable.AddCell(cellDC3)

            ' Modelo
            Dim cellDC4 As iTextSharp.text.pdf.PdfPCell = New iTextSharp.text.pdf.PdfPCell(New iTextSharp.text.Phrase("MODELO", New iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7.0F, iTextSharp.text.Font.BOLD)))
            cellDC4.HorizontalAlignment = 1
            pdfTable.AddCell(cellDC4)

            ' Serie
            Dim cellDC5 As iTextSharp.text.pdf.PdfPCell = New iTextSharp.text.pdf.PdfPCell(New iTextSharp.text.Phrase("SERIE", New iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7.0F, iTextSharp.text.Font.BOLD)))
            cellDC5.HorizontalAlignment = 1
            pdfTable.AddCell(cellDC5)

            ' Unidad Medida
            Dim cellDC6 As iTextSharp.text.pdf.PdfPCell = New iTextSharp.text.pdf.PdfPCell(New iTextSharp.text.Phrase("UN. MEDIDA", New iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7.0F, iTextSharp.text.Font.BOLD)))
            cellDC6.HorizontalAlignment = 1
            pdfTable.AddCell(cellDC6)

            ' Cantidad
            Dim cellDC7 As iTextSharp.text.pdf.PdfPCell = New iTextSharp.text.pdf.PdfPCell(New iTextSharp.text.Phrase("CANTIDAD", New iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7.0F, iTextSharp.text.Font.BOLD)))
            cellDC7.HorizontalAlignment = 1
            pdfTable.AddCell(cellDC7)



            ' Detalle Descripcion
            For x As Integer = 0 To 19

                If x > (tbDet.Rows.Count - 1) Then
                    ' Codigo de Inventario
                    pdfTable.AddCell(New iTextSharp.text.Phrase(" ", New iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7.0F)))
                    ' Descripcion
                    pdfTable.AddCell(New iTextSharp.text.Phrase(" ", New iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7.0F)))
                    ' Marca
                    pdfTable.AddCell(New iTextSharp.text.Phrase(" ", New iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7.0F)))
                    ' Modelo
                    pdfTable.AddCell(New iTextSharp.text.Phrase(" ", New iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7.0F)))
                    ' Serie
                    pdfTable.AddCell(New iTextSharp.text.Phrase(" ", New iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7.0F)))
                    ' Unidad Medida
                    pdfTable.AddCell(New iTextSharp.text.Phrase(" ", New iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7.0F)))
                    ' Cantidad
                    pdfTable.AddCell(New iTextSharp.text.Phrase(" ", New iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7.0F)))
                Else
                    ' Codigo de Inventario
                    pdfTable.AddCell(New iTextSharp.text.Phrase(tbDet.Rows(x).Item("etiqueta_af"), New iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7.0F)))
                    ' Descripcion
                    pdfTable.AddCell(New iTextSharp.text.Phrase(tbDet.Rows(x).Item("descripcionArt"), New iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7.0F)))
                    ' Marca
                    pdfTable.AddCell(New iTextSharp.text.Phrase(tbDet.Rows(x).Item("Marca"), New iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7.0F)))
                    ' Modelo
                    pdfTable.AddCell(New iTextSharp.text.Phrase(tbDet.Rows(x).Item("Modelo"), New iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7.0F)))
                    ' Serie
                    pdfTable.AddCell(New iTextSharp.text.Phrase(tbDet.Rows(x).Item("Serie"), New iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7.0F)))
                    ' Unidad Medida
                    pdfTable.AddCell(New iTextSharp.text.Phrase("UND.", New iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7.0F)))
                    ' Cantidad
                    pdfTable.AddCell(New iTextSharp.text.Phrase("1", New iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7.0F)))
                End If

            Next

            ' Salto de Celda
            Dim cellBr As iTextSharp.text.pdf.PdfPCell = New iTextSharp.text.pdf.PdfPCell(New iTextSharp.text.Phrase(" " & Environment.NewLine & " ", New iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7.0F, iTextSharp.text.Font.BOLD)))
            cellBr.Border = 0
            cellBr.Colspan = 7
            pdfTable.AddCell(cellBr)

            ' Intenerario
            Dim cellIntenerario As iTextSharp.text.pdf.PdfPCell
            cellIntenerario = New iTextSharp.text.pdf.PdfPCell(New iTextSharp.text.Phrase(" " & Environment.NewLine & _
                                                          " ITINERARIO (LLENAR SOLO SI SE LLEVARAN BIEN ES POR COMISION)" & Environment.NewLine & _
                                                          " COMISION A:" & Environment.NewLine & _
                                                          " ", New iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7.0F, iTextSharp.text.Font.BOLD)))
            cellIntenerario.Colspan = 7
            pdfTable.AddCell(cellIntenerario)

            ' CeldaNone
            Dim cellNone1 As iTextSharp.text.pdf.PdfPCell = New iTextSharp.text.pdf.PdfPCell(New iTextSharp.text.Phrase(""))
            cellNone1.Border = 0
            pdfTable.AddCell(cellNone1)

            ' Firma Solicitante
            Dim cellFirma1 As iTextSharp.text.pdf.PdfPCell
            cellFirma1 = New iTextSharp.text.pdf.PdfPCell(New iTextSharp.text.Phrase(" " & Environment.NewLine & _
                                                     " " & Environment.NewLine & _
                                                     " " & Environment.NewLine & _
                                                     " " & Environment.NewLine & _
                                                     "                  ___________________" & Environment.NewLine & _
                                                     " " & Environment.NewLine & _
                                                     "                      SOLICITANTE", New iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7.0F, iTextSharp.text.Font.BOLD)))
            cellFirma1.Border = 0
            cellFirma1.Colspan = 2
            pdfTable.AddCell(cellFirma1)

            ' CeldaNone
            Dim cellNone2 As iTextSharp.text.pdf.PdfPCell = New iTextSharp.text.pdf.PdfPCell(New iTextSharp.text.Phrase(""))
            cellNone2.Border = 0
            pdfTable.AddCell(cellNone2)

            ' Firma Responsable
            Dim cellFirma2 As iTextSharp.text.pdf.PdfPCell
            cellFirma2 = New iTextSharp.text.pdf.PdfPCell(New iTextSharp.text.Phrase(" " & Environment.NewLine & _
                                                     " " & Environment.NewLine & _
                                                     " " & Environment.NewLine & _
                                                     " " & Environment.NewLine & _
                                                     "                    ________________________" & Environment.NewLine & _
                                                     " " & Environment.NewLine & _
                                                     "                      RESPONSABLE DEL BIEN", New iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7.0F, iTextSharp.text.Font.BOLD)))
            cellFirma2.Border = 0
            cellFirma2.Colspan = 2
            pdfTable.AddCell(cellFirma2)

            ' CeldaNone
            Dim cellNone3 As iTextSharp.text.pdf.PdfPCell = New iTextSharp.text.pdf.PdfPCell(New iTextSharp.text.Phrase(""))
            cellNone3.Border = 0
            pdfTable.AddCell(cellNone3)

            ' Firma Director de Area
            Dim cellFirma3 As iTextSharp.text.pdf.PdfPCell = New iTextSharp.text.pdf.PdfPCell(New iTextSharp.text.Phrase(" " & Environment.NewLine & _
                                                     " " & Environment.NewLine & _
                                                     " " & Environment.NewLine & _
                                                     " " & Environment.NewLine & _
                                                     "                  _______________________" & Environment.NewLine & _
                                                     " " & Environment.NewLine & _
                                                     "                      DIRECTOR DE AREA", New iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7.0F, iTextSharp.text.Font.BOLD)))
            cellFirma3.Border = 0
            cellFirma3.Colspan = 2
            pdfTable.AddCell(cellFirma3)

            ' Firma Director de Area
            Dim cellFirma4 As iTextSharp.text.pdf.PdfPCell
            cellFirma4 = New iTextSharp.text.pdf.PdfPCell(New iTextSharp.text.Phrase(" " & Environment.NewLine & _
                                                     " " & Environment.NewLine & _
                                                     " " & Environment.NewLine & _
                                                     " " & Environment.NewLine & _
                                                     "                      _____________________________" & Environment.NewLine & _
                                                     " " & Environment.NewLine & _
                                                     "                          RESPONSABLE DEL BIEN", New iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7.0F, iTextSharp.text.Font.BOLD)))
            cellFirma4.Border = 0
            cellFirma4.Colspan = 3
            pdfTable.AddCell(cellFirma4)

            ' Firma Comision de Activo Fijo
            Dim cellFirma5 As iTextSharp.text.pdf.PdfPCell
            cellFirma5 = New iTextSharp.text.pdf.PdfPCell(New iTextSharp.text.Phrase(" " & Environment.NewLine & _
                                                     " " & Environment.NewLine & _
                                                     " " & Environment.NewLine & _
                                                     " " & Environment.NewLine & _
                                                     "                  __________________________" & Environment.NewLine & _
                                                     " " & Environment.NewLine & _
                                                     "                   COMISION DE ACTIVO FIJO", New iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7.0F, iTextSharp.text.Font.BOLD)))
            cellFirma5.Border = 0
            cellFirma5.Colspan = 2
            pdfTable.AddCell(cellFirma5)

            ' Salto de Celda
            Dim cellBr2 As iTextSharp.text.pdf.PdfPCell = New iTextSharp.text.pdf.PdfPCell(New iTextSharp.text.Phrase(" " & Environment.NewLine & " ", New iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7.0F, iTextSharp.text.Font.BOLD)))
            cellBr2.Border = 0
            cellBr2.Colspan = 7
            pdfTable.AddCell(cellBr2)

            ' Pie de Pagina
            Dim cellFoot As iTextSharp.text.pdf.PdfPCell
            cellFoot = New iTextSharp.text.pdf.PdfPCell(New iTextSharp.text.Phrase(" " & Environment.NewLine & _
                                                    " " & Environment.NewLine & _
                                                    " " & Environment.NewLine & _
                                                    " " & Environment.NewLine & _
                                                    "USUARIO: " & Me.hdNomUser.Value & Environment.NewLine & _
                                                    "FECHA: " & Now.Date & " HORA: " & Now.ToShortTimeString, New iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7.0F, iTextSharp.text.Font.BOLD)))
            cellFoot.Border = 0
            cellFoot.Colspan = 7
            pdfTable.AddCell(cellFoot)

            pdfDoc.Add(pdfTable)

            pdfDoc.Close()

            'Data.Add("PDF", "OK")

            Dim bytes() As Byte = memory.ToArray
            memory.Close()

            ' 2.- Creacion de Email para envio de PDF ----------------------------------------------------------------------

            Dim objEmail As New ClsMail
            Dim cuerpo, receptor, AsuntoCorreo, cco As String
            cuerpo = "<html>"
            cuerpo = cuerpo & "<head>"
            cuerpo = cuerpo & "<title></title>"
            cuerpo = cuerpo & "<style>"
            cuerpo = cuerpo & ".FontType{font-family: calibri; font-size:12px; }"
            cuerpo = cuerpo & "</style>"
            cuerpo = cuerpo & "</head>"
            cuerpo = cuerpo & "<body>"
            cuerpo = cuerpo & "<table border=0 cellpadding=0 class=""FontType"">"
            cuerpo = cuerpo & "<tr><td colspan=2><b>Estimado(a):</b></td></tr>"
            cuerpo = cuerpo & "<tr><td colspan=2></br></br>El documento de traslado ha sido generado.</td></tr>"
            cuerpo = cuerpo & "<tr><td colspan=2></br></br>Saludos Cordiales</td></tr>"
            cuerpo = cuerpo & "</table>"
            cuerpo = cuerpo & "</body>"
            cuerpo = cuerpo & "</html>"
            'receptor = "enevado@usat.edu.pe"
            receptor = Me.hdEmaUser.Value
            AsuntoCorreo = "[Documento de Traslado]"

            'cco = "cmonja@usat.edu.pe;csenmache@usat.edu.pe"
            cco = Me.hdEmaPer.Value + "cmonja@usat.edu.pe"

            objEmail.EnviarPDFMail("campusvirtual@usat.edu.pe", "Documento de Traslado", receptor, AsuntoCorreo, cuerpo, True, "Traslado" & "-" & nro_corr, New System.IO.MemoryStream(bytes), cco)

            mt_ShowMessage("La devolución se ha generado correctamente :" & nro_corr, MessageType.Success)
            mt_CargarDatos()

        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub btnVer_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Page.RegisterStartupScript("Pop", "<script>openModal2();</script>")
    End Sub

#End Region

#Region "Metodos"

    Protected Sub mt_ShowMessage(ByVal Message As String, ByVal Type As MessageType)
        Page.RegisterStartupScript("Mensaje", "<script>ShowMessage('" & Message & "','" & Type.ToString & "');</script>")
    End Sub

    Private Sub mt_CargarDatos()
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        Try
            cod_ctf = CInt(Request.QueryString("ctf"))
            obj.AbrirConexion()
            dt = obj.TraerDataTable("AF_listarTraslado3", Me.txtNroTraslado.Text, cod_ctf)
            obj.CerrarConexion()
            Me.gvTraslado.DataSource = dt
            Me.gvTraslado.DataBind()

            cod_ctf = Request.QueryString("ctf")

            If cod_ctf = 207 Or cod_ctf = 208 Then
                For x As Integer = 0 To gvTraslado.Rows.Count - 1
                    Dim btn As Button = Me.gvTraslado.Rows(x).FindControl("btnDevolver")
                    btn.Enabled = False
                Next
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

End Class
