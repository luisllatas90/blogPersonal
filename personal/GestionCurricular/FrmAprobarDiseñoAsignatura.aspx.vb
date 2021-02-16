Imports System.Collections.Generic
Imports System.IO

Partial Class GestionCurricular_FrmAprobarDiseñoAsignatura
    Inherits System.Web.UI.Page

#Region "Declaracion de Variables"

    Private cod_user As Integer
    Private cod_ctf As Integer
    Private ruta As String = ConfigurationManager.AppSettings("SharedFiles") '"http://localhost/campusvirtual/ArchivosCompartidos/SharedFiles.asmx"
    Private oeDiseño As e_DiseñoAsignatura, odDiseño As d_DiseñoAsignatura

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
                Response.Redirect("../../sinacceso.html")
            End If

            cod_user = Session("id_per")
            cod_ctf = Request.QueryString("ctf")

            If Not IsPostBack Then
                Session("gc_AsignaturaAP") = Nothing
                Call mt_CargarSemestre()
                Call mt_CargarPlanCurricular()

                Me.txtBuscar.Attributes.Add("onKeyPress", "txtBuscar_onKeyPress('" & Me.btnBuscar.ClientID & "', event)")
                Me.txtBuscar.Visible = False
                Me.btnBuscar.Visible = False
            End If
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub ddlSemestre_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlSemestre.SelectedIndexChanged
        Try
            lblMensaje.Text = ""
            Call mt_CargarDatos()
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub ddlPlanCurricular_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPlanCurricular.SelectedIndexChanged
        Try
            Call mt_CargarDatos()
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub ddlEstado_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlEstado.SelectedIndexChanged
        Try
            Call mt_CargarDatos()
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub btnBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBuscar.Click
        Try
            If Session("gc_AsignaturaAP") Is Nothing Then
                Call mt_CargarDatos()
            End If

            Dim dt As New Data.DataTable
            Dim dv As New Data.DataView
            Dim strBuscar As String = ""

            If Me.txtBuscar.Text.Trim <> "" Then
                strBuscar = Me.txtBuscar.Text.Trim.ToUpper.Replace("Á", "A").Replace("É", "E").Replace("Í", "I").Replace("Ó", "O").Replace("Ú", "U")
                dv = New Data.DataView(CType(Session("gc_AsignaturaAP"), Data.DataTable), "nombre_Cur_Aux like '%" & strBuscar & "%'", "", Data.DataViewRowState.CurrentRows)
                dt = dv.ToTable
            Else
                dt = CType(Session("gc_AsignaturaAP"), Data.DataTable)
            End If

            Me.gvResultados.DataSource = dt
            Me.gvResultados.DataBind()

            Me.txtBuscar.Focus()
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub btnListar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnListar.Click
        Dim flag As Boolean = True

        If ddlPlanCurricular.SelectedValue = -1 Then
            Page.RegisterStartupScript("alerta", "<script>alert('Seleccione la Carrera Profesional');</script>")
            Me.ddlPlanCurricular.Focus()
            flag = False
        End If

        If ddlSemestre.SelectedValue = -1 Then
            Page.RegisterStartupScript("alerta", "<script>alert('Seleccione el Semestre');</script>")
            Me.ddlSemestre.Focus()
            flag = False
        End If

        If flag Then
            Call mt_CargarDatos()
        Else
            Me.gvResultados.DataSource = Nothing
            Me.gvResultados.DataBind()
        End If
    End Sub

    Protected Sub gvResultados_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gvResultados.PageIndexChanging
        If Session("gc_AsignaturaAP") IsNot Nothing Then
            Me.gvResultados.DataSource = CType(Session("gc_AsignaturaAP"), Data.DataTable)
            Me.gvResultados.DataBind()
        End If
        
        Me.gvResultados.PageIndex = e.NewPageIndex
        Me.gvResultados.DataBind()
    End Sub

    Protected Sub gvResultados_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvResultados.RowCommand
        Try
            Dim index As Integer = CInt(e.CommandArgument)
            Dim obj As New ClsConectarDatos
            Dim objRes As Object()
            Dim dtUni, dtRes, dtCon, dtEst, dtRef As New Data.DataTable
            Dim codigo_Pes, codigo_Cur, codigo_dis, codigo_cac As Integer
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString

            If index >= 0 Then
                Select e.CommandName
                    Case "Aprobar"
                        If Not String.IsNullOrEmpty(Me.gvResultados.DataKeys(index).Values("fecha_apr").ToString) Then
                            Call mt_ShowMessage("El diseño fue aprobado anteriormente", MessageType.Info)
                            Return
                        End If

                        If fc_tieneCronograma() Then
                            codigo_dis = Me.gvResultados.DataKeys(index).Values("codigo_dis")
                            codigo_Cur = Me.gvResultados.DataKeys(index).Values("codigo_Cur")
                            codigo_Pes = Me.gvResultados.DataKeys(index).Values("codigo_Pes")
                            Session("gc_codigo_dis") = codigo_dis

                            obj.AbrirConexion()
                            dtUni = obj.TraerDataTable("COM_ListarResultadoAprendizaje", -1, codigo_Pes, codigo_Cur, Me.ddlSemestre.SelectedValue, -1)
                            dtRes = obj.TraerDataTable("ResultadoAprendizaje_Listar", "", -1, codigo_dis, "")
                            dtCon = obj.TraerDataTable("COM_ListarContenidoAsignatura", codigo_dis, -1, 0)
                            dtEst = obj.TraerDataTable("EstrategiaDidactica_Listar", -1, codigo_dis, "")
                            dtRef = obj.TraerDataTable("ReferenciaBibliografica_Listar", -1, codigo_dis, -1, "")
                            obj.CerrarConexion()

                            ' Mostrar Unidades de Asignatura
                            Me.gvUnidad.DataSource = dtUni
                            Me.gvUnidad.DataBind()
                            If Me.gvUnidad.Rows.Count > 0 Then mt_AgruparFilas(Me.gvUnidad.Rows, 0, 2)
                            ' Mostrar Criterios de Evaluación 
                            Me.gvCriterios.DataSource = dtRes
                            Me.gvCriterios.DataBind()
                            If Me.gvCriterios.Rows.Count > 0 Then mt_AgruparFilas(Me.gvCriterios.Rows, 0, 5)
                            ' Mostrar Contenido de Asignatura
                            Me.gvContenido.DataSource = dtCon
                            Me.gvContenido.DataBind()
                            ' Mostrar Estrategias Didácticas
                            Me.gvEstrategia.DataSource = dtEst
                            Me.gvEstrategia.DataBind()
                            ' Mostrar Referencias Bibliográficas
                            Me.gvReferencia.DataSource = dtRef
                            Me.gvReferencia.DataBind()
                            Page.RegisterStartupScript("Pop", "<script>openModal('aprobar');</script>")
                        End If

                    Case "Observar"
                        codigo_dis = Me.gvResultados.DataKeys(index).Values("codigo_dis")
                        Session("gc_codigo_dis") = codigo_dis
                        Page.RegisterStartupScript("Pop", "<script>openModal('observar');</script>")
                    Case "Ver"
                        Dim clsPdf As New clsGenerarPDF
                        Dim memory As New System.IO.MemoryStream
                        Dim memory_pdf As New System.IO.MemoryStream
                        Dim codigo_cup, idTransa, var As Long

                        Dim _modular As Boolean

                        codigo_Cur = Me.gvResultados.DataKeys(index).Values("codigo_Cur")
                        codigo_Pes = Me.gvResultados.DataKeys(index).Values("codigo_Pes")
                        codigo_cup = fc_ObtieneCodCup(Me.ddlSemestre.SelectedValue, codigo_Pes, codigo_Cur)
                        idTransa = CLng(Me.gvResultados.DataKeys(index).Values("codigo_dis"))
                        var = CLng(Me.gvResultados.DataKeys(index).Values("IdArchivo_Anexo"))
                        _modular = CBool(Me.gvResultados.DataKeys(index).Values("modular_pcu"))

                        clsPdf.fuente = Server.MapPath(".") & "/segoeui.ttf"
                        'clsPdf.anexo = Server.MapPath(".") & "/ManualTutoria.pdf"
                        If _modular Then
                            clsPdf.mt_GenerarSilabo2(codigo_cup, Server.MapPath(".") & "/logo_usat.png", memory, True)
                        Else
                            clsPdf.mt_GenerarSilabo(codigo_cup, Server.MapPath(".") & "/logo_usat.png", memory, True)
                        End If

                        Dim bytes() As Byte = memory.ToArray
                        memory.Close()

                        If var <> -1 Then
                            Dim bytes_anexo() As Byte = fc_ObtenerArchivo(idTransa, 24, "SU3WMBVV4W", var)
                            mt_CopiarPdf(bytes, bytes_anexo, memory_pdf)
                            Dim bytes_pdf() As Byte = memory_pdf.ToArray
                            memory_pdf.Close()
                            Response.Clear()
                            Response.ContentType = "application/pdf"
                            Response.AddHeader("content-length", bytes_pdf.Length.ToString())
                            Response.BinaryWrite(bytes_pdf)
                        Else
                            Response.Clear()
                            Response.ContentType = "application/pdf"
                            Response.AddHeader("content-length", bytes.Length.ToString())
                            Response.BinaryWrite(bytes)
                        End If
                    Case "Desaprobar"
                        If fc_tieneCronograma() Then
                            Dim dt As New System.Data.DataTable
                            codigo_dis = Me.gvResultados.DataKeys(index).Values("codigo_dis")
                            codigo_Cur = Me.gvResultados.DataKeys(index).Values("codigo_Cur")
                            codigo_Pes = Me.gvResultados.DataKeys(index).Values("codigo_Pes")
                            codigo_cac = Me.ddlSemestre.SelectedValue
                            oeDiseño = New e_DiseñoAsignatura : odDiseño = New d_DiseñoAsignatura
                            With oeDiseño
                                .codigo_dis = codigo_dis : .codigo_cac = codigo_cac : .codigo_pes = codigo_Pes : .codigo_cur = codigo_Cur : .codigo_per = cod_user
                            End With
                            dt = odDiseño.fc_DesaprobarDiseñoAsignatura(oeDiseño)
                            If dt.Rows.Count > 0 Then
                                If dt.Rows(0).Item(0) = 1 Then
                                    mt_ShowMessage("¡ El diseño ha sido desaprobado correctamente !", MessageType.Success)
                                    mt_CargarDatos()
                                Else
                                    mt_ShowMessage("¡ No se puede desaprobar el diseño por que tiene publicado silabos !", MessageType.Warning)
                                End If
                            End If
                        End If
                End Select
            End If
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub btnAprobar_Click(ByVal sender As Object, ByVal e As System.EventArgs)

    End Sub

    Protected Sub gvCriterios_OnRowCreated(ByVal sender As Object, ByVal e As GridViewRowEventArgs)
        Try
            If e.Row.RowType = DataControlRowType.Header Then
                Dim objGridView As GridView = CType(sender, GridView)
                Dim objgridviewrow As GridViewRow = New GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert)
                Dim objtablecell As TableCell = New TableCell()
                mt_AgregarCabecera(objgridviewrow, objtablecell, 2, "Resultado de Aprendizaje", "#D9534F")
                mt_AgregarCabecera(objgridviewrow, objtablecell, 2, "Indicadores de Aprendizaje", "#D9534F")
                mt_AgregarCabecera(objgridviewrow, objtablecell, 1, "Evidencia de Evaluación", "#D9534F")
                mt_AgregarCabecera(objgridviewrow, objtablecell, 2, "Instrumento de Evaluación", "#D9534F")
                objGridView.Controls(0).Controls.AddAt(0, objgridviewrow)
            End If
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub gvCriterios_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvCriterios.RowDataBound
        Try
            Dim tipo_prom, tipo_prom2 As Integer
            Dim peso_res, peso_ind, peso_ins As Double
            Dim codigo_ind, codigo_ins As Integer
            tipo_prom = Me.gvCriterios.DataKeys(e.Row.RowIndex).Values("tipo_prom")
            tipo_prom2 = Me.gvCriterios.DataKeys(e.Row.RowIndex).Values("tipo_prom2")
            peso_res = Me.gvCriterios.DataKeys(e.Row.RowIndex).Values("peso_res")
            peso_ind = Me.gvCriterios.DataKeys(e.Row.RowIndex).Values("peso_ind")
            peso_ins = Me.gvCriterios.DataKeys(e.Row.RowIndex).Values("peso_ins")
            codigo_ind = Me.gvCriterios.DataKeys(e.Row.RowIndex).Values("codigo_ind")
            codigo_ins = Me.gvCriterios.DataKeys(e.Row.RowIndex).Values("codigo_ins")
            If e.Row.RowType = DataControlRowType.DataRow Then
                If tipo_prom = 1 Then
                    e.Row.Cells(3).Text = IIf(codigo_ind = -1, "", "Promedio Simple")
                Else
                    e.Row.Cells(3).Text = CStr(peso_ind * 100) & " %"
                End If
                If tipo_prom2 = 1 Then
                    e.Row.Cells(6).Text = IIf(codigo_ins = -1, "", "Promedio Simple")
                Else
                    e.Row.Cells(6).Text = CStr(peso_ins * 100) & " %"
                End If
                e.Row.Cells(1).Text = CStr(peso_res * 100) & " %"
            End If
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub btnGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardar.Click
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable

        Try
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString

            obj.AbrirConexion()
            dt = obj.TraerDataTable("COM_ActualizarDiseñoAsignatura", Session("gc_codigo_dis"), cod_user)
            obj.CerrarConexion()

            If dt.Rows.Count > 0 Then
                Dim msj As String = dt.Rows(0).Item(0).ToString()
                If msj.Substring(0, 5).Equals("Error") Then
                    Call mt_ShowMessage(msj, MessageType.Error)
                Else
                    Call mt_ShowMessage(msj, MessageType.Success)
                    Call mt_CargarDatos()
                End If
            Else
                Call mt_ShowMessage("Ocurrió un error al procesar la aprobación del diseño", MessageType.Error)
            End If
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub btnGrabar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGrabar.Click
        Dim obj As New ClsConectarDatos
        Dim codigo_dis As Integer
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
        Try
            codigo_dis = Session("gc_codigo_dis")
            'Throw New Exception(codigo_dis)
            obj.AbrirConexion()
            obj.Ejecutar("DiseñoAsignatura_observar", codigo_dis, "O", Me.txtObservacion.Text, cod_user)
            obj.CerrarConexion()
            mt_ShowMessage("¡ Se ha registrado la observación al sílabo !", MessageType.Success)
            ddlPlanCurricular_SelectedIndexChanged(Nothing, Nothing)
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

#End Region

#Region "Metodos"

    Protected Sub mt_ShowMessage(ByVal Message As String, ByVal type As MessageType)
        Page.RegisterStartupScript("Mensaje", "<script>ShowMessage('" & Message & "','" & type.ToString & "');</script>")
    End Sub

    Private Sub mt_CargarSemestre()
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString

        Try
            obj.AbrirConexion()
            dt = obj.TraerDataTable("ConsultarCicloAcademico", "DAN", "")
            obj.CerrarConexion()

            Call mt_CargarCombo(Me.ddlSemestre, dt, "codigo_Cac", "descripcion_Cac")
            dt.Dispose()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub mt_CargarPlanCurricular()
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
        'cod_user = 648
        Try
            obj.AbrirConexion()
            If cod_ctf = 1 Or cod_ctf = 232 Then
                dt = obj.TraerDataTable("ConsultarCarreraProfesionalV2", "CF", "2", cod_user)
            Else
                dt = obj.TraerDataTable("ConsultarCarreraProfesionalV2", "UX", "2", cod_user)
            End If
            'dt = obj.TraerDataTable("COM_ListarPlanCurricular2", DBNull.Value, -1, cod_user)
            obj.CerrarConexion()
            'Call mt_CargarCombo(Me.ddlPlanCurricular, dt, "codigo_cpf", "nombre_pcur")nombre_Cpf
            mt_CargarCombo(Me.ddlPlanCurricular, dt, "codigo_cpf", "nombre_Cpf")
            dt.Dispose()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub mt_CargarCombo(ByVal cbo As DropDownList, ByVal dt As Data.DataTable, ByVal datavalue As String, ByVal datatext As String)
        cbo.DataSource = dt
        cbo.DataTextField = datatext
        cbo.DataValueField = datavalue
        cbo.DataBind()
    End Sub

    Private Sub mt_CargarDatos()
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        Dim plan, semestre As String
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString

        Try
            plan = IIf(String.IsNullOrEmpty(Me.ddlPlanCurricular.SelectedValue), "0", Me.ddlPlanCurricular.SelectedValue)
            semestre = IIf(String.IsNullOrEmpty(Me.ddlSemestre.SelectedValue), "0", Me.ddlSemestre.SelectedValue)

            obj.AbrirConexion()
            dt = obj.TraerDataTable("COM_ListarAsignaturaAprobar", plan, semestre, Me.ddlEstado.SelectedValue, cod_ctf)
            obj.CerrarConexion()

            Session("gc_AsignaturaAP") = dt
            Me.gvResultados.DataSource = dt 'CType(Session("gc_AsignaturaAP"), Data.DataTable)
            Me.gvResultados.DataBind()
            dt.Dispose()

            Me.txtBuscar.Visible = IIf(Me.gvResultados.Rows.Count > 0, True, False)
            Me.btnBuscar.Visible = IIf(Me.gvResultados.Rows.Count > 0, True, False)
            If Me.gvResultados.Rows.Count > 0 Then fc_tieneCronograma(True)
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Private Sub mt_AgruparFilas(ByVal gridViewRows As GridViewRowCollection, ByVal startIndex As Integer, ByVal totalColumns As Integer)
        If totalColumns = 0 Then Return
        Dim i As Integer, count As Integer = 1
        Dim lst As ArrayList = New ArrayList()
        lst.Add(gridViewRows(0))
        Dim ctrl As TableCell
        ctrl = gridViewRows(0).Cells(startIndex)
        For i = 1 To gridViewRows.Count - 1
            Dim nextTbCell As TableCell = gridViewRows(i).Cells(startIndex)
            If ctrl.Text = nextTbCell.Text Then
                count += 1
                nextTbCell.Visible = False
                lst.Add(gridViewRows(i))
            Else
                If count > 1 Then
                    ctrl.RowSpan = count
                    ctrl.VerticalAlign = VerticalAlign.Middle
                    mt_AgruparFilas(New GridViewRowCollection(lst), startIndex + 1, totalColumns - 1)
                End If
                count = 1
                lst.Clear()
                ctrl = gridViewRows(i).Cells(startIndex)
                lst.Add(gridViewRows(i))
            End If
        Next
        If count > 1 Then
            ctrl.RowSpan = count
            ctrl.VerticalAlign = VerticalAlign.Middle
            mt_AgruparFilas(New GridViewRowCollection(lst), startIndex + 1, totalColumns - 1)
        End If
        count = 1
        lst.Clear()
    End Sub

    Protected Sub mt_AgregarCabecera(ByVal objgridviewrow As GridViewRow, ByVal objtablecell As TableCell, ByVal colspan As Integer, ByVal celltext As String, ByVal backcolor As String)
        objtablecell = New TableCell()
        objtablecell.Text = celltext
        objtablecell.ColumnSpan = colspan
        'objtablecell.Style.Add("background-color", backcolor)
        'objtablecell.Style.Add("BackColor", backcolor)
        'objtablecell.Style.Add("Font-Bold", "true")
        objtablecell.HorizontalAlign = HorizontalAlign.Center
        objgridviewrow.Cells.Add(objtablecell)
    End Sub

    Private Sub mt_CopiarPdf(ByVal bytes_pdf() As Byte, ByVal bytes_anexo() As Byte, ByVal memory As System.IO.Stream)
        Dim pdfFile_anexo As iTextSharp.text.pdf.PdfReader
        Dim pdfFile_silabo As iTextSharp.text.pdf.PdfReader
        Dim doc As iTextSharp.text.Document
        Dim pCopy As iTextSharp.text.pdf.PdfWriter
        'Dim msOutput As MemoryStream = New MemoryStream()
        pdfFile_anexo = New iTextSharp.text.pdf.PdfReader(bytes_anexo)
        pdfFile_silabo = New iTextSharp.text.pdf.PdfReader(bytes_pdf)
        doc = New iTextSharp.text.Document()
        pCopy = New iTextSharp.text.pdf.PdfSmartCopy(doc, memory)
        doc.Open()

        'For k As Integer = 0 To files.Count - 1
        'pdfFile = New iTextSharp.text.pdf.PdfReader(Server.MapPath(".") & "/ManualTutoria.pdf")

        For j As Integer = 1 To pdfFile_silabo.NumberOfPages
            CType(pCopy, iTextSharp.text.pdf.PdfSmartCopy).AddPage(pCopy.GetImportedPage(pdfFile_silabo, j))
        Next

        pCopy.FreeReader(pdfFile_silabo)
        pdfFile_silabo.Close()

        For i As Integer = 1 To pdfFile_anexo.NumberOfPages
            CType(pCopy, iTextSharp.text.pdf.PdfSmartCopy).AddPage(pCopy.GetImportedPage(pdfFile_anexo, i))
        Next

        pCopy.FreeReader(pdfFile_anexo)
        'Next

        pdfFile_anexo.Close()

        pCopy.Close()
        doc.Close()
    End Sub

#End Region

#Region "Funciones"

    Private Function fc_ObtenerArchivo(ByVal idTransa As Long, ByVal idTabla As Integer, ByVal token As String, ByVal IdArchivo As Long) As Byte()
        Try
            Dim wsCloud As New ClsArchivosCompartidosV2
            Dim list As New Generic.Dictionary(Of String, String)
            Dim obj As New ClsConectarDatos
            Dim tb As New Data.DataTable
            Dim usuario As String = Session("perlogin")
            Dim bytes() As Byte = Nothing

            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            tb = obj.TraerDataTable("ArchivosCompartidos_Listar2", 1, idTabla, idTransa, token)
            obj.CerrarConexion()

            'Comentado por Luis Q.T. | 17DIC2019: Se copió condición de frmConfigurarAsignatura
            'If tb.Rows.Count = 0 Then Throw New Exception("¡ No se encontró el archivo !")

            If tb.Rows.Count = 0 Then
                obj.AbrirConexion()
                tb = obj.TraerDataTable("ArchivosCompartidos_Listar2", 3, idTabla, IdArchivo, token)
                If tb.Rows.Count = 0 Then Throw New Exception("¡ Archivo no encontrado !")

                obj.CerrarConexion()
            End If

            list.Add("IdArchivo", tb.Rows(0).Item("IdArchivo").ToString)
            list.Add("Usuario", usuario)
            list.Add("Token", token)

            Dim envelope As String = wsCloud.SoapEnvelopeDescarga(list)
            Dim result As String = wsCloud.PeticionRequestSoap(ruta, envelope, "http://usat.edu.pe/DownloadFile", usuario)
            Dim imagen As String = fc_ResultFile(result)

            If tb.Rows.Count > 0 Then
                bytes = Convert.FromBase64String(imagen)
            End If
            'Response.Write(envelope)

            Return bytes

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

    Private Function fc_ObtieneCodCup(ByVal _codcac As Integer, ByVal _codpes As Integer, ByVal _codcur As Integer) As Long
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
        Try
            Dim _codcup As Long = -1
            obj.AbrirConexion()
            dt = obj.TraerDataTable("DiseñoAsignatura_Listar", "XP", -1, _codpes, _codcur, _codcac, -1)
            If dt.Rows.Count > 0 Then
                _codcup = CLng(dt.Rows(0).Item("codigo_cup"))
            End If
            obj.CerrarConexion()
            Return _codcup
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Function fc_tieneCronograma(Optional ByVal mostrarEnEtiqueta As Boolean = False) As Boolean
        Dim obj As New ClsConectarDatos
        Dim objRes As Object()
        Dim codigo_cac As Integer
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString

        '[Inicio] Condición por Luis Q.T| 17DIC2019
        codigo_cac = IIf(String.IsNullOrEmpty(Me.ddlSemestre.SelectedValue), "0", Me.ddlSemestre.SelectedValue)

        obj.AbrirConexion()
        objRes = obj.Ejecutar("DEA_validarAccionEnCronograma", "AD", codigo_cac, "2")
        obj.CerrarConexion()

        If objRes Is Nothing Then
            If Not mostrarEnEtiqueta Then Call mt_ShowMessage("El plazo para aprobar el diseño de asignatura ha caducado. Consulte con Dirección Académica", MessageType.Info)
            lblMensaje.Text = "El plazo para aprobar el diseño de asignatura ha caducado. Consulte con Dirección Académica"
            Return False
        Else
            If objRes(0).ToString.Equals("0") Then
                If Not mostrarEnEtiqueta Then Call mt_ShowMessage("El plazo para publicar el diseño de asignatura ha caducado. Consulte con Dirección Académica", MessageType.Info)
                lblMensaje.Text = "El plazo para publicar el diseño de asignatura ha caducado. Consulte con Dirección Académica"
                Return False
            End If
        End If
        '[Fin] Condición por Luis Q.T| 17DIC2019

        lblMensaje.Text = ""
        Return True
    End Function

#End Region

End Class
