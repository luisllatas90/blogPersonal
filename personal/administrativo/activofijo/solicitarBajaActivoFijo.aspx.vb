﻿

Partial Class administrativo_activofijo_solicitarBajaActivoFijo
    Inherits System.Web.UI.Page

#Region "Declaracion de Varibles"

    Dim cod_ctf As Integer

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
                    Dim user As Integer
                    user = CInt(Request.QueryString("id"))
                    obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
                    obj.AbrirConexion()
                    dt = obj.TraerDataTable("SEG_ListaPersonal", user)
                    If dt.Rows.Count > 0 Then
                        Me.hdUserNom.Value = dt.Rows(0).Item("trabajador").ToString()
                        Me.hdUserCod.Value = user
                        Me.hdUserCgo.Value = dt.Rows(0).Item("descripcion_Cgo").ToString
                        Me.hdUserCco.Value = dt.Rows(0).Item("codigo_Cco").ToString
                        Me.hdUserEml.Value = dt.Rows(0).Item("usuario_per").ToString + "@usat.edu.pe;"
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

    Protected Sub btnGenerar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGenerar.Click
        Dim x As Integer
        Dim dt As New Data.DataTable
        Try
            dt.Columns.Add("codigo_af", System.Type.GetType("System.String"))
            dt.Columns.Add("etiqueta", System.Type.GetType("System.String"))
            dt.Columns.Add("descripcion", System.Type.GetType("System.String"))
            'Dim msj As String
            If Me.gvActivoFijo.Rows.Count = 0 Then
                Throw New Exception("¡No hay registros para realizar esta operacion!")
            End If
            For x = 0 To Me.gvActivoFijo.Rows.Count - 1
                Dim chk As CheckBox = Me.gvActivoFijo.Rows(x).FindControl("chkSelec")
                If chk.Checked Then
                    Dim newRow As Data.DataRow
                    newRow = dt.NewRow
                    newRow("codigo_af") = Me.gvActivoFijo.DataKeys(x).Values("codigo_af")
                    newRow("etiqueta") = Me.gvActivoFijo.DataKeys(x).Values("etiqueta_af")
                    newRow("descripcion") = Me.gvActivoFijo.DataKeys(x).Values("descripcionArt") & _
                                            " " & Me.gvActivoFijo.DataKeys(x).Values("Marca") & _
                                            " " & Me.gvActivoFijo.DataKeys(x).Values("Modelo") & _
                                            " " & Me.gvActivoFijo.DataKeys(x).Values("Serie")
                    'msj += Me.gvActivoFijo.DataKeys(x).Values("codigo_af") & ","
                    dt.Rows.Add(newRow)
                End If
            Next
            Me.gvSolicitud.DataSource = dt
            Me.gvSolicitud.DataBind()
            If dt.Rows.Count = 0 Then
                Throw New Exception("¡ Se seleccione al menos un Activo Fijo para realizar esta operación !")
            End If
            'mt_ShowMessage(msj, MessageType.Success)
            Me.txtFecha.Text = Now.Date.ToShortDateString
            Page.RegisterStartupScript("Pop", "<script>openModal();</script>")
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub btnGrabar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGrabar.Click
        Dim obj As New ClsConectarDatos
        Dim y, _codigo_af, _nroSolicitud, user, codigo_sba As Integer
        Dim _recomendacion, _observacion, _sigla As String
        Dim dt As New Data.DataTable
        Dim txt As TextBox
        Dim rdl As RadioButtonList
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        user = CInt(Request.QueryString("Id"))
        Try
            obj.IniciarTransaccion()
            dt = obj.TraerDataTable("AF_traerNroSolicitudBaja", CDate(txtFecha.Text).Year)
            If dt.Rows.Count > 0 Then
                _nroSolicitud = dt.Rows(0).Item(0).ToString
            End If
            dt = obj.TraerDataTable("AF_insertarSolictudBaja", CDate(txtFecha.Text), CDate(txtFecha.Text).Year, _nroSolicitud, CInt(Me.hdCodPerDe.Value), CInt(Me.hdCodPerPara.Value), Me.txtAsunto.Text, user)
            If dt.Rows.Count > 0 Then
                codigo_sba = dt.Rows(0).Item(0).ToString
            End If
            For y = 0 To Me.gvSolicitud.Rows.Count - 1
                txt = Me.gvSolicitud.Rows(y).FindControl("txtObservacion")
                rdl = Me.gvSolicitud.Rows(y).FindControl("rbRecomendacion")
                _codigo_af = Me.gvSolicitud.DataKeys(y).Values("codigo_af")
                _recomendacion = rdl.SelectedValue
                _observacion = txt.Text
                obj.Ejecutar("AF_generarSolicitudBaja", codigo_sba, _codigo_af, "", _recomendacion, _observacion, user)
            Next
            obj.TerminarTransaccion()

            Select Case CInt(Me.hdCcoDe.Value)
                Case 634 : _sigla = "TEIN"
                Case 2528 : _sigla = "OPER"
                Case Else : _sigla = ""
            End Select

            Dim pdfDoc As New iTextSharp.text.Document()
            Dim memory As New System.IO.MemoryStream
            Dim pdfWrite As iTextSharp.text.pdf.PdfWriter = iTextSharp.text.pdf.PdfWriter.GetInstance(pdfDoc, memory)

            pdfDoc.Open()

            Dim pdfTabInfo As iTextSharp.text.pdf.PdfPTable = New iTextSharp.text.pdf.PdfPTable(4)
            pdfTabInfo.WidthPercentage = 100.0F
            pdfTabInfo.SetWidths(New Single() {15.0F, 20.0F, 55.0F, 10.0F})

            Dim srcIcon2 As String = Server.MapPath(".") & "/logo_usat.png"
            Dim usatIcon2 As iTextSharp.text.Image = iTextSharp.text.Image.GetInstance(srcIcon2)
            usatIcon2.ScalePercent(40.0F)
            usatIcon2.Alignment = iTextSharp.text.Element.ALIGN_LEFT

            Dim cellIcon2 As iTextSharp.text.pdf.PdfPCell = New iTextSharp.text.pdf.PdfPCell(usatIcon2)
            cellIcon2.HorizontalAlignment = 0
            cellIcon2.VerticalAlignment = 2
            cellIcon2.Colspan = 2
            cellIcon2.Rowspan = 3
            cellIcon2.Border = 0
            pdfTabInfo.AddCell(cellIcon2)

            ' None 01
            pdfTabInfo.AddCell(fc_CeldaTexto(" " & Environment.NewLine & " ", 8.0F, 1, 0, 2, 3, 0))

            ' None 02
            pdfTabInfo.AddCell(fc_CeldaTexto(" " & Environment.NewLine & " ", 8.0F, 1, 0, 1, 2, 0))
            ' Titulo
            pdfTabInfo.AddCell(fc_CeldaTexto(" " & Environment.NewLine & " INFORME TÉCNICO N° " & String.Format("{0:0000}", _nroSolicitud) & _
                                             "-" & CDate(Me.txtFecha.Text).Year & "-USAT/" & _sigla, 12.0F, 1, 0, 2, 2, 1))
            ' None 03
            pdfTabInfo.AddCell(fc_CeldaTexto(" " & Environment.NewLine & " ", 8.0F, 1, 0, 1, 2, 0))

            ' Salto de Celda
            pdfTabInfo.AddCell(fc_CeldaTexto(" " & Environment.NewLine & " " & Environment.NewLine & " ", 12.0F, 1, 0, 4, 2, 0))

            ' Para
            pdfTabInfo.AddCell(fc_CeldaTexto(" PARA:", 11.0F, 1, 0, 1, 2, 0))
            ' Dato Para
            pdfTabInfo.AddCell(fc_CeldaTexto(Me.txtPara.Text & Environment.NewLine & Me.hdCargoPara.Value & Environment.NewLine & " ", 11.0F, 0, 0, 3, 2, 0))

            ' De
            pdfTabInfo.AddCell(fc_CeldaTexto(" DE:", 11.0F, 1, 0, 1, 2, 0))
            ' Dato De
            pdfTabInfo.AddCell(fc_CeldaTexto(Me.txtDe.Text & Environment.NewLine & Me.hdCargoDe.Value & Environment.NewLine & " ", 11.0F, 0, 0, 3, 2, 0))

            ' Asunto
            pdfTabInfo.AddCell(fc_CeldaTexto(" ASUNTO:", 11.0F, 1, 0, 1, 2, 0))
            ' Dato Asunto
            pdfTabInfo.AddCell(fc_CeldaTexto(Me.txtAsunto.Text & Environment.NewLine & " ", 11.0F, 0, 0, 3, 2, 0))

            ' Fecha
            pdfTabInfo.AddCell(fc_CeldaTexto(" FECHA:", 11.0F, 1, 0, 1, 2, 0))
            ' Dato Fecha
            pdfTabInfo.AddCell(fc_CeldaTexto(Me.txtFecha.Text & Environment.NewLine & " ", 11.0F, 0, 0, 3, 2, 0))

            ' Salto de Celda
            pdfTabInfo.AddCell(fc_CeldaTexto(" " & Environment.NewLine & " " & Environment.NewLine & " " & Environment.NewLine & " ", 11.0F, 1, 0, 4, 2, 0))

            ' Contexto
            pdfTabInfo.AddCell(fc_CeldaTexto("El presente informe tiene por finalidad dar a conocer el diagnóstico de los " & _
            "activos fijos AEE inoperativos revisados, el/los cual(es) se da(n) a conocer en " & _
            "la Ficha Técnica de Bienes de Baja de Activo Fijo, para que determine la " & _
            "disposición final de baja de/del activo(s) fijo. " & Environment.NewLine & " " & Environment.NewLine & _
            "Es todo en cuanto tengo que informar.", 11.0F, 0, 0, 4, 2, 0))

            ' Salto de Celda
            pdfTabInfo.AddCell(fc_CeldaTexto(" " & Environment.NewLine & " " & Environment.NewLine & " " & Environment.NewLine & " ", 11.0F, 1, 0, 4, 2, 0))

            ' None 06
            Dim cellTable As iTextSharp.text.pdf.PdfPTable = New iTextSharp.text.pdf.PdfPTable(1)
            cellTable.AddCell(fc_CeldaTexto(" " & Environment.NewLine & " " & Environment.NewLine & " " & Environment.NewLine & " ", 11.0F, 1, 0, 1, 1, 0))
            cellTable.AddCell(fc_CeldaTexto(" " & Environment.NewLine & " " & Environment.NewLine & " " & Environment.NewLine & " ", 11.0F, 1, 0, 1, 1, 0))
            cellTable.AddCell(fc_CeldaTexto(" " & Environment.NewLine & " " & Environment.NewLine & " " & Environment.NewLine & " ", 11.0F, 1, 0, 1, 1, 0))
            Dim cellNone As iTextSharp.text.pdf.PdfPCell
            cellNone = New iTextSharp.text.pdf.PdfPCell(cellTable)
            cellNone.Border = 0
            pdfTabInfo.AddCell(cellNone)

            ' Sello
            pdfTabInfo.AddCell(fc_CeldaTexto(" Sello ", 11.0F, 1, 15, 1, 3, 0))

            Dim cellData As iTextSharp.text.pdf.PdfPTable = New iTextSharp.text.pdf.PdfPTable(1)
            ' Firma
            cellData.AddCell(fc_CeldaTexto(" Firma " & Environment.NewLine & " " & Environment.NewLine & " " & Environment.NewLine & " " & _
                                           Environment.NewLine & " " & Environment.NewLine & " " & Environment.NewLine & " ", 11.0F, 1, 15, 1, 2, 0))
            ' Nombre
            cellData.AddCell(fc_CeldaTexto(" Nombre ", 11.0F, 1, 15, 1, 1, 0))
            Dim cellFirma As iTextSharp.text.pdf.PdfPCell
            cellFirma = New iTextSharp.text.pdf.PdfPCell(cellData)
            cellFirma.Padding = 0
            pdfTabInfo.AddCell(cellFirma)

            ' None 07
            pdfTabInfo.AddCell(fc_CeldaTexto(" " & Environment.NewLine & " ", 11.0F, 1, 0, 1, 3, 0))

            pdfDoc.Add(pdfTabInfo)

            pdfDoc.NewPage()

            Dim pdfTable As iTextSharp.text.pdf.PdfPTable = New iTextSharp.text.pdf.PdfPTable(6)
            pdfTable.WidthPercentage = 100.0F
            pdfTable.SetWidths(New Single() {5.0F, 10.0F, 30.0F, 10.0F, 20.0F, 25.0F})

            Dim srcIcon As String = Server.MapPath(".") & "/logo_usat.png"
            Dim usatIcon As iTextSharp.text.Image = iTextSharp.text.Image.GetInstance(srcIcon)
            usatIcon.ScalePercent(40.0F)
            usatIcon.Alignment = iTextSharp.text.Element.ALIGN_LEFT

            Dim cellIcon As iTextSharp.text.pdf.PdfPCell = New iTextSharp.text.pdf.PdfPCell(usatIcon)
            cellIcon.HorizontalAlignment = 1
            cellIcon.VerticalAlignment = 2
            cellIcon.Colspan = 2
            cellIcon.Rowspan = 2
            cellIcon.Border = 0
            pdfTable.AddCell(cellIcon)

            ' None 01
            pdfTable.AddCell(fc_CeldaTexto(" " & Environment.NewLine & " ", 10.0F, 1, 0, 2, 2, 0))
            ' Titulo
            pdfTable.AddCell(fc_CeldaTexto(" " & Environment.NewLine & " FICHA TÉCNICA DE BIENES DE BAJA DE ACTIVO FIJO ", 10.0F, 1, 15, 2, 2, 1))

            ' Salto de Celda
            pdfTable.AddCell(fc_CeldaTexto(" " & Environment.NewLine & " " & Environment.NewLine & " ", 10.0F, 1, 0, 6, 1, 0))

            ' Item
            pdfTable.AddCell(fc_CeldaTexto2(" N°", 9.0F, 1, 1))
            ' Codigo Inventario
            pdfTable.AddCell(fc_CeldaTexto2(" Código Inventario", 9.0F, 1, 1))
            ' Descripcion
            pdfTable.AddCell(fc_CeldaTexto2(" Descripción", 9.0F, 1, 1))
            ' Cantidad
            pdfTable.AddCell(fc_CeldaTexto2(" Cantidad", 9.0F, 1, 1))
            ' Recomendacion
            pdfTable.AddCell(fc_CeldaTexto2(" Recomendación", 9.0F, 1, 1))
            ' Observacion
            pdfTable.AddCell(fc_CeldaTexto2(" Observación", 9.0F, 1, 1))

            For i As Integer = 0 To 9
                If i > (Me.gvSolicitud.Rows.Count - 1) Then
                    ' Item
                    pdfTable.AddCell(fc_CeldaTexto2(" " & Environment.NewLine & " " & Environment.NewLine & " " & Environment.NewLine, 8.0F, 0, 0))
                    ' Codigo Inventario
                    pdfTable.AddCell(fc_CeldaTexto2(" ", 8.0F, 0, 0))
                    ' Descripcion
                    pdfTable.AddCell(fc_CeldaTexto2(" ", 8.0F, 0, 0))
                    ' Cantidad
                    pdfTable.AddCell(fc_CeldaTexto2(" ", 8.0F, 0, 0))
                    ' Recomendacion
                    pdfTable.AddCell(fc_CeldaTexto2(" " & Environment.NewLine & "   [   ]   L   [   ]   D   [   ]   E", 8.0F, 0, 1))
                    ' Observacion
                    pdfTable.AddCell(fc_CeldaTexto2(" ", 8.0F, 0, 0))
                Else
                    ' Item
                    pdfTable.AddCell(fc_CeldaTexto2(Environment.NewLine & (i + 1) & Environment.NewLine & " " & Environment.NewLine, 8.0F, 0, 1))
                    ' Codigo Inventario
                    pdfTable.AddCell(fc_CeldaTexto2(" " & Me.gvSolicitud.DataKeys(i).Values("etiqueta").ToString, 8.0F, 0, 1))
                    ' Descripcion
                    pdfTable.AddCell(fc_CeldaTexto2(" " & Me.gvSolicitud.DataKeys(i).Values("descripcion").ToString, 8.0F, 0, 0))
                    ' Cantidad
                    pdfTable.AddCell(fc_CeldaTexto2("   1   ", 8.0F, 0, 1))
                    ' Recomendacion
                    rdl = Me.gvSolicitud.Rows(i).FindControl("rbRecomendacion")
                    Select Case rdl.SelectedValue
                        Case "L"
                            pdfTable.AddCell(fc_CeldaTexto2("   [ X ]   L   [   ]   D   [   ]   E", 8.0F, 0, 1))
                        Case "D"
                            pdfTable.AddCell(fc_CeldaTexto2("   [   ]   L   [ X ]   D   [   ]   E", 8.0F, 0, 1))
                        Case "E"
                            pdfTable.AddCell(fc_CeldaTexto2("   [   ]   L   [   ]   D   [ X ]   E", 8.0F, 0, 1))
                    End Select
                    ' Observacion
                    txt = Me.gvSolicitud.Rows(i).FindControl("txtObservacion")
                    pdfTable.AddCell(fc_CeldaTexto2(" " & txt.Text, 8.0F, 0, 0))
                End If
            Next

            ' None 02
            pdfTable.AddCell(fc_CeldaTexto(" " & Environment.NewLine & " " & Environment.NewLine & " " & Environment.NewLine & " ", 8.0F, 1, 0, 6, 1, 0))

            ' Leyenda
            pdfTable.AddCell(fc_CeldaTexto(" " & Environment.NewLine & " Leyenda: L = Licitación D = Donación E = Eliminación", 7.0F, 1, 0, 3, 1, 0))
            ' None 03
            pdfTable.AddCell(fc_CeldaTexto(" " & Environment.NewLine & " ", 8.0F, 1, 0, 2, 1, 0))
            ' Comision
            pdfTable.AddCell(fc_CeldaTexto(" " & Environment.NewLine & " COMISIÓN DE ACTIVO FIJO", 9.0F, 1, 15, 1, 1, 1))

            ' None 04
            pdfTable.AddCell(fc_CeldaTexto(" " & Environment.NewLine & " ", 8.0F, 1, 0, 5, 3, 0))
            ' Comision - Dato
            pdfTable.AddCell(fc_CeldaTexto(" " & Environment.NewLine & " " & Environment.NewLine & " " & Environment.NewLine & " ", 8.0F, 1, 15, 1, 3, 0))

            ' None 05
            pdfTable.AddCell(fc_CeldaTexto(" " & Environment.NewLine & " ", 8.0F, 1, 0, 4, 1, 0))
            ' Fecha
            pdfTable.AddCell(fc_CeldaTexto(" " & Environment.NewLine & " Fecha ", 9.0F, 1, 0, 1, 1, 2))
            ' Dato Fecha
            pdfTable.AddCell(fc_CeldaTexto2(" " & Environment.NewLine & " ", 8.0F, 1, 0))

            pdfDoc.Add(pdfTable)

            pdfDoc.Close()

            Dim bytes() As Byte = memory.ToArray
            memory.Close()

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
            cuerpo = cuerpo & "<tr><td colspan=2></br></br>la solicitud se ha generado.</td></tr>"
            cuerpo = cuerpo & "<tr><td colspan=2></br></br>Saludos Cordiales</td></tr>"
            cuerpo = cuerpo & "</table>"
            cuerpo = cuerpo & "</body>"
            cuerpo = cuerpo & "</html>"
            'receptor = "enevado@usat.edu.pe"
            receptor = Me.hdEmailPara.Value
            AsuntoCorreo = "[Solicitud Baja Activo Fijo]"

            If CInt(Me.hdCcoDe.Value) = 634 Then
                'cco = "cmonja@usat.edu.pe;csenmache@usat.edu.pe"
                cco = Me.hdUserEml.Value + "jalvarado@usat.edu.pe"
            Else
                'cco = "cmonja@usat.edu.pe;csenmache@usat.edu.pe"
                cco = Me.hdUserEml.Value + "cmonja@usat.edu.pe"
            End If

            objEmail.EnviarPDFMail("campusvirtual@usat.edu.pe", "Solicitud de Baja", receptor, AsuntoCorreo, cuerpo, True, "Solicitud-" & _
                                   String.Format("{0:0000}", _nroSolicitud) & "-" & CDate(Me.txtFecha.Text).Year & "-USAT-" & _sigla, New System.IO.MemoryStream(bytes), cco)

            mt_ShowMessage("La solicitud se ha generado correctamente ", MessageType.Success)
            mt_CargarDatos()
        Catch ex As Exception
            obj.AbortarTransaccion()
            mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
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
            dt = obj.TraerDataTable("AF_listarActivoFijo4", cod_ctf, Me.txtEtiqueta.Text, 0)
            obj.CerrarConexion()
            Me.gvActivoFijo.DataSource = dt
            Me.gvActivoFijo.DataBind()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub mt_Load(ByVal _flat As Boolean)
        If _flat Then
            Page.RegisterStartupScript("Load", "<script>document.body.style.cursor = 'wait';</script>")
        Else
            Page.RegisterStartupScript("Load", "<script>document.body.style.cursor = 'default';</script>")
        End If
    End Sub

#End Region

#Region "Funciones"

    Private Function fc_CeldaTexto(ByVal _text As String, ByVal _size As Single, ByVal _style As Integer, ByVal _border As Integer, _
                                 ByVal _colspan As Integer, ByVal _rowspan As Integer, ByVal _haligment As Integer) As iTextSharp.text.pdf.PdfPCell
        Dim celdaITC As iTextSharp.text.pdf.PdfPCell
        Dim fontITC As iTextSharp.text.Font
        fontITC = New iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, _size, _style)
        celdaITC = New iTextSharp.text.pdf.PdfPCell(New iTextSharp.text.Phrase(_text, fontITC))
        celdaITC.Border = _border
        celdaITC.Colspan = _colspan
        celdaITC.Rowspan = _rowspan
        celdaITC.HorizontalAlignment = _haligment
        Return celdaITC
    End Function

    Private Function fc_CeldaTexto2(ByVal _text As String, ByVal _size As Single, ByVal _style As Integer, ByVal _haligment As Integer) As iTextSharp.text.pdf.PdfPCell
        Dim celdaITC2 As iTextSharp.text.pdf.PdfPCell
        Dim fontITC2 As iTextSharp.text.Font
        fontITC2 = New iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, _size, _style)
        celdaITC2 = New iTextSharp.text.pdf.PdfPCell(New iTextSharp.text.Phrase(_text, fontITC2))
        celdaITC2.HorizontalAlignment = _haligment
        Return celdaITC2
    End Function

#End Region

End Class
