Imports System.IO
Imports System.Collections.Generic

'fimardoumentacion.aspx.vb
Partial Class GestionDocumentaria_frmFirmarDocumentacion
    Inherits System.Web.UI.Page

#Region "variables"


    Dim codigo_tfu, tipoestudio, codigo_usu As Integer
    Dim md_funciones As d_Funciones
    Dim md_documento As d_Documento
    Dim md_confFirma As d_configuraFirma
    Dim md_documentacionArchivo As d_DocumentacionArchivo
    Dim md_documentacion As d_Documentacion
    Dim codigo_dot, codigo_dofm As Integer
    Dim descripcion_est As String
    Dim codigoTabla As Integer
    Dim usuario As String
    Dim respuesta As String

    Dim md_clsGeneraDocumento As New clsGeneraDocumento
    Dim memory As New System.IO.MemoryStream
    Dim bytes As Byte()

    Public Enum MessageType
        success
        [error]
        info
        warning
    End Enum

#End Region

#Region "Eventos"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (Session("id_per") Is Nothing) Then
            Response.Redirect("../../sinacceso.html")
        End If
        Try
            codigo_tfu = Request.QueryString("ctf")
            'Response.Write(codigo_tfu)
            'tipoestudio = Request.QueryString("mod")
            tipoestudio = "2"
            codigo_usu = Request.QueryString("id")
            usuario = Session("perlogin")

            If IsPostBack = False Then
                Call mt_CargarComboDocumento()
                'Call mt_ShowMessage(codigo_tfu, MessageType.success)
                Call mt_CargarDocumentosByFirmar(Me.ddlCodigo_doc.SelectedValue)
                'Call mt_ShowMessage("firmar documentos", MessageType.success)
            Else
                Call mt_CargarDocumentosByFirmar(Me.ddlCodigo_doc.SelectedValue)
            End If
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try

    End Sub

    Protected Sub ddlCodigo_doc_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlCodigo_doc.SelectedIndexChanged
        Call mt_CargarDocumentosByFirmar(Me.ddlCodigo_doc.SelectedValue)
    End Sub

    Protected Sub gvListaDocByFirmar_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvListaDocByFirmar.RowDataBound

        If e.Row.RowType = DataControlRowType.Header Then
            e.Row.TableSection = TableRowSection.TableHeader
        End If

        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim fila As Data.DataRowView
            fila = e.Row.DataItem

            Dim lnk As LinkButton = (CType(e.Row.Cells(4).FindControl("btnFirmar"), LinkButton))
            Dim lnkd As LinkButton = (CType(e.Row.Cells(4).FindControl("btnDescargar"), LinkButton))
            Dim fuUpl As FileUpload = (CType(e.Row.Cells(4).FindControl("fuArchivoPdf"), FileUpload))

            If e.Row.Cells(3).Text = "FIRMADO" Then

                lnk.Visible = False
                lnkd.Visible = False
                fuUpl.Visible = False
            Else
                'Dim lnk As LinkButton = (CType(e.Row.Cells(5).FindControl("fuArchivoPdf"), LinkButton))
                ''lnk.Attributes.Remove("OnClientClick")
                'lnk.Visible = False
            End If
        End If
    End Sub

    Protected Sub gvListaDocByFirmar_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvListaDocByFirmar.RowCommand
        Try
            Dim token As String
            Dim codigo_cda As Integer
            Dim codigo_doa As Integer
            Dim codigo_dotx As Integer  '--- el codigo_dot original despues cambia segun vayan subiendo el archivo
            Dim codigo_sol As Integer
            Dim estado_doa, estado_sol As Boolean

            Dim index As Integer = 0 : index = CInt(e.CommandArgument)

            descripcion_est = Me.gvListaDocByFirmar.DataKeys(index).Values("descripcion_est")
            codigo_cda = Me.gvListaDocByFirmar.DataKeys(index).Values("codigo_cda")
            codigo_dotx = Me.gvListaDocByFirmar.DataKeys(index).Values("codigo_dot")
            codigo_doa = Me.gvListaDocByFirmar.DataKeys(index).Values("codigo_doa")
            codigo_sol = Me.gvListaDocByFirmar.DataKeys(index).Values("codigo_sol")

            If descripcion_est = "FIRMADO" Then
                codigoTabla = 32
                codigo_dot = Me.gvListaDocByFirmar.DataKeys(index).Values("codigo_dofm")
                token = "3N23G666FS"
            Else
                codigoTabla = 30
                codigo_dot = Me.gvListaDocByFirmar.DataKeys(index).Values("codigo_dot")
                token = "3N23G777FS"
            End If


            codigo_dofm = Me.gvListaDocByFirmar.DataKeys(index).Values("codigo_dofm")


            Dim codigo_shf As Integer
            

            Select Case e.CommandName
                Case "ver"
                    bytes = md_clsGeneraDocumento.DescargarArchivo(codigo_dot, codigoTabla, token, usuario)
                    Response.Clear()
                    Response.ContentType = "application/pdf"
                    Response.AddHeader("content-length", bytes.Length.ToString())
                    Response.BinaryWrite(bytes)

                Case "descargar"
                    bytes = md_clsGeneraDocumento.DescargarArchivo(codigo_dot, codigoTabla, token, usuario)
                    Response.Clear()
                    Response.Buffer = False
                    Response.Charset = ""
                    Response.Cache.SetCacheability(HttpCacheability.NoCache)
                    Response.ContentType = "application/pdf"
                    Response.AddHeader("content-disposition", "attachment;filename=" & "FichaMatricula.pdf".ToString.Replace(",", ""))
                    Response.AppendHeader("Content-Length", bytes.Length.ToString())
                    Response.BinaryWrite(bytes)
                    Response.End()
                    Call mt_ShowMessage("¡ Se ha descargado el documento correctamente !", MessageType.success)

                Case "firmar"
                    Dim rowIndex As Integer = Integer.Parse(e.CommandArgument.ToString().Trim())
                    Dim fileUpload As FileUpload = CType(gvListaDocByFirmar.Rows(rowIndex).FindControl("fuArchivoPdf"), FileUpload)

                    If fileUpload.HasFile Then

                        Dim nombreArchivo As String = Path.GetFileNameWithoutExtension(fileUpload.FileName)
                        Dim extension As String = System.IO.Path.GetExtension(fileUpload.PostedFile.FileName)
                        Dim data As Byte() = LoadUploadedFile(fileUpload.PostedFile)
                        Dim memory As New MemoryStream(data)

                        'envio archivo
                        respuesta = md_clsGeneraDocumento.fc_SubirArchivo(32, codigo_dofm, 0, memory, nombreArchivo & extension, Session("perlogin").ToString)

                        'traer el codigo del archivo compartido
                        md_documentacionArchivo = New d_DocumentacionArchivo
                        Dim dtSf As New Data.DataTable
                        dtSf = md_documentacionArchivo.ListarSharedFile(codigo_dofm, "32") '
                        If dtSf.Rows.Count > 0 Then
                            With dtSf.Rows(0)
                                codigo_shf = .Item("idArchivosCompartidos")
                            End With
                        Else
                            Call mt_ShowMessage("No se ha ubicado el archivo compartivo, verifique", MessageType.error)
                        End If
                        '***************  Actualizo la firma ************************************
                        md_documentacion = New d_Documentacion
                        md_documentacion.CrearActualizarFirmasDocumento(codigo_dofm, 0, codigo_shf, 0, codigo_usu, "3")

                        ''ActualizoEtapaTramite y actualizo estado de solicitud y documento y archivo
                        If codigo_cda = "3" Then ''''''

                            Dim codigo_dta As Integer

                            md_documentacion = New d_Documentacion
                            Dim dtCodigoDta As New Data.DataTable
                            dtCodigoDta = md_documentacion.ListarCodigoDta(codigo_dotx)
                            'cambio 01
                            Dim codigo_tes As Integer
                            Dim codigo_pst As Integer
                            If dtCodigoDta.Rows.Count > 0 Then
                                codigo_dta = CInt(dtCodigoDta.Rows(0).Item("codigo_dta"))
                                'cambio 01
                                codigo_tes = CInt(dtCodigoDta.Rows(0).Item("codigo_tes"))
                                codigo_pst = CInt(dtCodigoDta.Rows(0).Item("codigo_pst"))
                                If codigo_dta <> 0 Then
                                    Dim dtTramite As New Data.DataTable
                                    dtTramite = mt_ActualizarEtapaTramite(codigo_dta, "1", "A", codigo_tfu)
                                    If dtTramite.Rows.Count > 0 Then ' SI HAY RESPUESTA DEL CAMBIO DE ETAPA
                                        If dtTramite.Rows(0).Item("revision") = True Then ' SI SE HIZO LA REVISIÓN
                                            If dtTramite.Rows(0).Item("email") = False Then ' NO SE INSERTO ENVIO CORREO MASIVO                                                
                                                Call mt_ShowMessage("No se pudo realizar el envío de correo correctamente", MessageType.error)
                                            End If
                                        Else ' NO SE ACTUALIZO LA ETAPA DEL TRAMITE
                                            Call mt_ShowMessage("No se pudo realizar la actualización de la etapa del trámite", MessageType.error)
                                        End If
                                    Else
                                        Call mt_ShowMessage("No se pudo realizar la actualización de la etapa del trámite", MessageType.error)
                                    End If
                                Else
                                    Call mt_ShowMessage("Código del trámite invalido", MessageType.error)
                                End If
                            End If

                            ''***** si no hay firma pendiente actualizamos el documento en archivo
                            Dim md_configuraFirma As New d_configuraFirma
                            Dim dtIdArchivoCompartido As Data.DataTable
                            Dim idArchivoCompartido As Integer = 0

                            dtIdArchivoCompartido = md_configuraFirma.ActualizaIdArchivoCompartido(codigo_dotx)

                            If dtIdArchivoCompartido.Rows.Count > 0 Then
                                idArchivoCompartido = CInt(dtIdArchivoCompartido.Rows(0).Item("idArchivoCompartido"))
                            End If
                            ''**** si no hay firma pendientes y ha actualizado el archivo compatido
                            If idArchivoCompartido > 0 Then
                                '**** Actualizamos documentoArchivo *******
                                estado_doa = clsDocumentacion.actualizaEstadoDoc("DOA", codigo_doa, "3", codigo_usu)
                                If estado_doa = False Then
                                    Call mt_ShowMessage("No se actualizó el estado del documento en el archivo", MessageType.error)
                                End If
                                '**** Actualizamos solicitud documento *******
                                estado_sol = clsDocumentacion.actualizaEstadoDoc("SOL", codigo_sol, "7", codigo_usu)
                                If estado_sol = False Then
                                    Call mt_ShowMessage("No se actualizó el estado la solicitud", MessageType.error)
                                End If
                            End If
                            '**** Enviar correos  cambio 01
                            'Dim clsComunicacionEmail As New ClsComunicacionInstitucional

                            Dim codigoEnvio As Integer
                            Dim rptaEmail As Boolean = False
                            'llamo esta clase por que ahi se genera la resolucion de sustentacion y traigo los datos para el envio de correos 
                            Dim objGeneraDocumento As New clsGeneraDocumento
                            Dim dtEmail As New Data.DataTable
                            dtEmail = objGeneraDocumento.fc_DatosResolSustentacion(codigo_tes)

                            If dtEmail.Rows.Count > 0 Then
                                codigoEnvio = ClsComunicacionInstitucional.ObtenerCodigoEnvio(codigo_usu, codigo_tfu, 77)
                                For i As Integer = 0 To dtEmail.Rows.Count - 1
                                    '*************************************
                                    ClsComunicacionInstitucional.EnviarNotificacionEmail(codigoEnvio, "SUST", "CJYE", "1", codigo_usu, "codigo_alu", dtEmail.Rows(i).Item("codigo_Alu"), "77", "olluen@usat.edu.pe", "", "", "", _
                                                                                         dtEmail.Rows(i).Item("nombreOficial_cpf"), dtEmail.Rows(i).Item("titulo_tes"), dtEmail.Rows(i).Item("alumno"), dtEmail.Rows(i).Item("fechaprogramacion"), dtEmail.Rows(i).Item("horaprogramacion"), dtEmail.Rows(i).Item("ambiente"), dtEmail.Rows(i).Item("presidente"), dtEmail.Rows(i).Item("secretario"), dtEmail.Rows(i).Item("vocal"))

                                Next
                                'actualizo tabla programacionsustentaciontesis
                                md_documentacion.ActualizaEnviaCorreoProgramacion(codigo_pst)

                            End If
                            
                            'ClsComunicacionInstitucional.EnviarNotificacion("EMAIL", "SUST", "STEG", "1", cod_user, "codigo_pso", 33233, codigo_apl, "jbanda@usat.edu.pe", "", "correoCampusVirtual", _
                            '                              nombre_escuela, nombre_bachiller, nombre_tesis, dia_sustentacion, hora_sustentacion, aula_sustentacion)
                            'fin cambio 01 

                            Call mt_ShowMessage("Se firmó el archivo con éxito", MessageType.success)

                        End If

                        Call mt_CargarDocumentosByFirmar(Me.ddlCodigo_doc.SelectedValue)
                    Else
                        Call mt_ShowMessage("Seleccione un archivo", MessageType.error)
                    End If
            End Select
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

#End Region

#Region "Procedimientos y funciones"

    Protected Sub mt_ShowMessage(ByVal Message As String, ByVal type As MessageType)
        Try
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "showMessage", "showMessage('" & Message & "','" & type.ToString & "');", True)
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub
    Private Sub mt_CargarComboDocumento()
        Try
            md_funciones = New d_Funciones
            md_documento = New d_Documento
            Dim dt As New Data.DataTable

            dt = md_documento.ListarDocumento("DCN", 0, codigo_tfu) ''181 para que liste lo de direccion academica, lista doumento configurado
            Call md_funciones.CargarCombo(Me.ddlCodigo_doc, dt, "codigo_doc", "descripcion_doc", True, "[-- TODOS --]", "-1")

        Catch ex As Exception
            Throw ex
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub
    Private Sub mt_CargarDocumentosByFirmar(ByVal codigo_doc As Integer)
        Try
            md_confFirma = New d_configuraFirma
            Dim dt As New Data.DataTable
            dt = md_confFirma.ListarConfiguraFirma("TOD", 0, 0, codigo_tfu, codigo_usu, codigo_doc, "")

            If dt.Rows.Count > 0 Then
                Me.gvListaDocByFirmar.DataSource = dt
                Me.gvListaDocByFirmar.DataBind()
                'Else
                '    Call mt_ShowMessage("No se encontraron solic emitidos", MessageType.warning)
            Else
                Me.gvListaDocByFirmar.DataSource = Nothing
                Me.gvListaDocByFirmar.DataBind()
            End If

            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "formatoGrilla", "formatoGrilla();", True)

        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub
    Public Function LoadUploadedFile(ByVal uploadedFile As HttpPostedFile) As Byte()
        Dim buf As Byte() = New Byte(uploadedFile.InputStream.Length - 1) {}
        uploadedFile.InputStream.Read(buf, 0, CInt(uploadedFile.InputStream.Length))
        Return buf
    End Function

    Private Function mt_ActualizarEtapaTramite(ByVal codigo_dta As Integer, ByVal tipooperacion As String, ByVal estadoaprobacion As String, ByVal codigo_tfu As Integer) As Data.DataTable
        Try
            Dim cmp As New clsComponenteTramiteVirtualCVE
            Dim objcmp As New List(Of Dictionary(Of String, Object))()
            cmp._codigo_dta = codigo_dta
            'cmp.tipoOperacion = "1"
            cmp.tipoOperacion = tipooperacion
            cmp._codigo_per = Session("id_per")
            cmp._codigo_tfu = codigo_tfu ' tipo funcion director de escuela
            If estadoaprobacion = "A" Then
                cmp._estadoAprobacion = "A" ' DA CONFORMIDAD OSEA APRUEBA
                cmp._observacionEvaluacion = "Aprobar componente"
            Else
                cmp._estadoAprobacion = "R"
                cmp._observacionEvaluacion = "Rechazar componente"
            End If
            objcmp = cmp.mt_EvaluarTramite()
            Dim dt As New Data.DataTable
            dt.Columns.Add("revision")
            dt.Columns.Add("registros")
            dt.Columns.Add("email")
            For Each fila As Dictionary(Of String, Object) In objcmp
                dt.Rows.Add(fila.Item("evaluacion"), fila.Item("registos evaluados").ToString, fila.Item("email"))
            Next
            Return dt
        Catch ex As Exception
            Dim dt As New Data.DataTable
            dt.Columns.Add("revision")
            dt.Columns.Add("registros")
            dt.Columns.Add("email")

            dt.Rows.Add(False, "", False)

            Return dt
        End Try
    End Function


#End Region






End Class
