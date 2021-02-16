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
    Dim codigo_per As Integer
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
            tipoestudio = "2"
            codigo_usu = Request.QueryString("id")
            usuario = Session("perlogin")
            codigo_per = Session("id_per")

            If IsPostBack = False Then
                Call mt_CargarComboDocumento()
                Call mt_CargarComboEstado()
                Call mt_CargarDocumentosByFirmar(Me.ddlCodigo_doc.SelectedValue)
            Else
                'Call mt_CargarDocumentosByFirmar(Me.ddlCodigo_doc.SelectedValue)
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
                lnkd.Visible = True
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
            Dim referencia01 As String
            Dim ordenFirma As Integer
            Dim estado_doa, estado_sol As Boolean
            Dim nombreFile As String
            'Dim nomAluFile As String
            Dim msgAprueba As String

            Dim finEtapaTramiteFir As String
            Dim enviaEmailFir As String
            Dim adjuntaFileFir As String
           
            Dim index As Integer = 0 : index = CInt(e.CommandArgument)

            descripcion_est = Me.gvListaDocByFirmar.DataKeys(index).Values("descripcion_est")
            codigo_cda = Me.gvListaDocByFirmar.DataKeys(index).Values("codigo_cda")
            codigo_dotx = Me.gvListaDocByFirmar.DataKeys(index).Values("codigo_dot")
            codigo_doa = Me.gvListaDocByFirmar.DataKeys(index).Values("codigo_doa")
            codigo_sol = Me.gvListaDocByFirmar.DataKeys(index).Values("codigo_sol")
            referencia01 = Me.gvListaDocByFirmar.DataKeys(index).Values("referencia01")
            ordenFirma = Me.gvListaDocByFirmar.DataKeys(index).Values("orden_fma")

            finEtapaTramiteFir = Me.gvListaDocByFirmar.DataKeys(index).Values("finEtapaTramiteFir")
            enviaEmailFir = Me.gvListaDocByFirmar.DataKeys(index).Values("enviaEmailFir")
            adjuntaFileFir = Me.gvListaDocByFirmar.DataKeys(index).Values("adjuntaFileFir")

            nombreFile = Me.gvListaDocByFirmar.DataKeys(index).Values("serieCorrelativo_dot")

            If descripcion_est = "FIRMADO" Then
                codigoTabla = 31  ''tomar en cuenta en produccion y QA cambiar id a 31
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

                    If ordenFirma = 1 Then

                        bytes = md_clsGeneraDocumento.DescargarArchivo(codigo_dot, codigoTabla, token, usuario)

                    Else


                        Dim md_configFirmas As New d_configuraFirma
                        Dim dt_docFirma As New Data.DataTable
                        Dim codigo_dofm_sf As Integer

                        dt_docFirma = md_configFirmas.ListaEstadoDocumentoFirma("DOT", codigo_dotx)
                        codigo_dofm_sf = dt_docFirma.Rows(0).Item("codigo_dofm")

                        'Call mt_ShowMessage(codigo_dofm_sf, MessageType.success)

                        bytes = md_clsGeneraDocumento.DescargarArchivo(codigo_dofm_sf, "31", "3N23G666FS", usuario)

                    End If

                    'bytes = md_clsGeneraDocumento.DescargarArchivo(codigo_dot, codigoTabla, token, usuario)
                    Response.Clear()
                    Response.Buffer = False
                    Response.Charset = ""
                    Response.Cache.SetCacheability(HttpCacheability.NoCache)
                    Response.ContentType = "application/pdf"
                    Response.AddHeader("content-disposition", "attachment;filename=" & nombreFile & ".pdf".ToString.Replace(",", ""))
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
                        Dim codigo_dta As Integer
                        Dim dtCodigoDta As New Data.DataTable

                        '*************************************** Obliga a que el usaurio suba el documento con el mismo nombre ******************
                        If nombreArchivo <> nombreFile Then
                            Call mt_ShowMessage("El documento que intenta subir no coincide", MessageType.error)
                            Exit Sub
                        End If
                        '*************************************************************************************************************************
                        'envio archivo
                        'cambiar el id a 31 asi está en producción
                        respuesta = md_clsGeneraDocumento.fc_SubirArchivo(31, codigo_dofm, 0, memory, nombreArchivo & extension, Session("perlogin").ToString)

                        'traer el codigo del archivo compartido
                        md_documentacionArchivo = New d_DocumentacionArchivo
                        Dim dtSf As New Data.DataTable
                        'cambiar el id a 31 asi está en producción
                        dtSf = md_documentacionArchivo.ListarSharedFile(codigo_dofm, "31") '
                        If dtSf.Rows.Count > 0 Then
                            With dtSf.Rows(0)
                                codigo_shf = .Item("idArchivosCompartidos")
                            End With
                        Else
                            Call mt_ShowMessage("No se ha ubicado el archivo compartiDo, verifique", MessageType.error)
                        End If
                        '***************  Actualizo la firma ************************************
                        md_documentacion = New d_Documentacion
                        md_documentacion.CrearActualizarFirmasDocumento(codigo_dofm, 0, codigo_shf, 0, codigo_usu, "3")


                        ''ActualizoEtapaTramite y actualizo estado de solicitud y documento y archivo

                        If codigo_cda = "3" Then '''''' Resolución de sustentación

                            md_documentacion = New d_Documentacion
                            dtCodigoDta = md_documentacion.ListarCodigoDta(codigo_dotx)
                            'cambio 01
                            Dim codigo_tes As Integer
                            Dim codigo_pst As Integer
                            If dtCodigoDta.Rows.Count > 0 Then
                                ''--cambio cuando hay varios tramites se agrega el for
                                For i As Integer = 0 To dtCodigoDta.Rows.Count - 1
                                    codigo_dta = CInt(dtCodigoDta.Rows(i).Item("codigo_dta"))
                                    'cambio 01
                                    codigo_tes = CInt(dtCodigoDta.Rows(i).Item("codigo_tes"))
                                    codigo_pst = CInt(dtCodigoDta.Rows(i).Item("codigo_pst"))
                                    If codigo_dta <> 0 Then
                                        Dim dtTramite As New Data.DataTable
                                        dtTramite = mt_ActualizarEtapaTramite(codigo_dta, "1", "A", codigo_tfu)
                                        If dtTramite.Rows.Count > 0 Then ' SI HAY RESPUESTA DEL CAMBIO DE ETAPA
                                            If dtTramite.Rows(0).Item("revision") = False Then ' SI SE HIZO LA REVISIÓN
                                                '    If dtTramite.Rows(0).Item("email") = False Then ' NO SE INSERTO ENVIO CORREO MASIVO                                                
                                                '        Call mt_ShowMessage("No se pudo realizar el envío de correo correctamente", MessageType.error)
                                                '    End If
                                                'Else ' NO SE ACTUALIZO LA ETAPA DEL TRAMITE
                                                Call mt_ShowMessage("No se pudo realizar la actualización de la etapa del trámite", MessageType.error)
                                            End If
                                        Else
                                            Call mt_ShowMessage("No se pudo realizar la actualización de la etapa del trámite", MessageType.error)
                                        End If
                                    Else
                                        Call mt_ShowMessage("Código del trámite invalido", MessageType.error)
                                    End If
                                Next

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
                            Dim ultimaFila As Integer
                            Dim alumnos As String = ""
                            Dim emailAlumno As String = ""

                            Dim rptaEmail As Boolean = False
                            'llamo esta clase por que ahi se genera la resolucion de sustentacion y traigo los datos para el envio de correos 
                            Dim objGeneraDocumento As New clsGeneraDocumento
                            Dim dtEmail As New Data.DataTable
                            dtEmail = objGeneraDocumento.fc_DatosResolSustentacion(codigo_tes)

                            If dtEmail.Rows.Count > 0 Then

                                codigoEnvio = ClsComunicacionInstitucional.ObtenerCodigoEnvio(codigo_usu, codigo_tfu, 77)

                                'If ConfigurationManager.AppSettings("CorreoUsatActivo") = "1" Then
                                '    For i As Integer = 0 To dtEmail.Rows.Count - 1
                                '        ultimaFila = dtEmail.Rows.Count - 1
                                '        '---------------------- coma
                                '        If i = ultimaFila Then
                                '            alumnos = alumnos + dtEmail.Rows(i).Item("alumno")
                                '            emailAlumno = emailAlumno + dtEmail.Rows(i).Item("eMail_Alu")
                                '        Else
                                '            alumnos = alumnos + dtEmail.Rows(i).Item("alumno") & "<br>"
                                '            emailAlumno = emailAlumno + dtEmail.Rows(i).Item("eMail_Alu") & ";"
                                '        End If

                                '        '-------------------------------------
                                '    Next
                                'Else
                                '    emailAlumno = "fatima.vasquez@usat.edu.pe"
                                'End If

                               

                                For i As Integer = 0 To dtEmail.Rows.Count - 1

                                    ultimaFila = dtEmail.Rows.Count - 1

                                    '---------------------- coma
                                    If i = ultimaFila Then
                                        alumnos = alumnos + dtEmail.Rows(i).Item("alumno")
                                        If ConfigurationManager.AppSettings("CorreoUsatActivo") = "1" Then
                                            emailAlumno = emailAlumno + dtEmail.Rows(i).Item("eMail_Alu")
                                        Else
                                            emailAlumno = "fatima.vasquez@usat.edu.pe"
                                        End If
                                    Else
                                        alumnos = alumnos + dtEmail.Rows(i).Item("alumno") & "<br>"
                                        If ConfigurationManager.AppSettings("CorreoUsatActivo") = "1" Then
                                            emailAlumno = emailAlumno + dtEmail.Rows(i).Item("eMail_Alu") & ";"
                                        Else
                                            emailAlumno = "fatima.vasquez@usat.edu.pe"
                                        End If
                                    End If

                                        '-------------------------------------
                                Next

                                ClsComunicacionInstitucional.EnviarNotificacionEmail(codigoEnvio, "SUST", "CJYE", "1", codigo_usu, "codigo_alu", dtEmail.Rows(0).Item("codigo_Alu"), "77", emailAlumno, "", "", "", _
                                                                    dtEmail.Rows(0).Item("nombreOficial_cpf"), dtEmail.Rows(0).Item("titulo_tes"), alumnos, dtEmail.Rows(0).Item("fechaprogramacion"), dtEmail.Rows(0).Item("horaprogramacion"), dtEmail.Rows(0).Item("ambiente"), dtEmail.Rows(0).Item("presidente"), dtEmail.Rows(0).Item("secretario"), dtEmail.Rows(0).Item("vocal"))

                                '*************************************
                                
                                If ConfigurationManager.AppSettings("CorreoUsatActivo") = "1" Then

                                    ClsComunicacionInstitucional.EnviarNotificacionEmail(codigoEnvio, "SUST", "CJYE", "1", codigo_usu, "codigo_alu", dtEmail.Rows(0).Item("codigo_Alu"), "77", "todousat@usat.edu.pe", "", "", "", _
                                           dtEmail.Rows(0).Item("nombreOficial_cpf"), dtEmail.Rows(0).Item("titulo_tes"), alumnos, dtEmail.Rows(0).Item("fechaprogramacion"), dtEmail.Rows(0).Item("horaprogramacion"), dtEmail.Rows(0).Item("ambiente"), dtEmail.Rows(0).Item("presidente"), dtEmail.Rows(0).Item("secretario"), dtEmail.Rows(0).Item("vocal"))

                                Else
                                    ClsComunicacionInstitucional.EnviarNotificacionEmail(codigoEnvio, "SUST", "CJYE", "1", codigo_usu, "codigo_alu", dtEmail.Rows(0).Item("codigo_Alu"), "77", "fatima.vasquez@usat.edu.pe", "", "", "", _
                                           dtEmail.Rows(0).Item("nombreOficial_cpf"), dtEmail.Rows(0).Item("titulo_tes"), alumnos, dtEmail.Rows(0).Item("fechaprogramacion"), dtEmail.Rows(0).Item("horaprogramacion"), dtEmail.Rows(0).Item("ambiente"), dtEmail.Rows(0).Item("presidente"), dtEmail.Rows(0).Item("secretario"), dtEmail.Rows(0).Item("vocal"))


                                End If

                                md_documentacion.ActualizaEnviaCorreoProgramacion(codigo_pst)

                                Call mt_ShowMessage("Se adjuntó el archivo firmado con éxito", MessageType.success)
                            End If
                            ''******--------------------------- cambio a partir de grados y títulos

                        Else '' todo documento (Td)
                            Dim dtsf1 As New Data.DataTable
                            Dim idArchivoCompartido As String = ""
                            md_documentacionArchivo = New d_DocumentacionArchivo
                            Dim ruta_shf As String

                            ''''' cambio 23/10/2020 ---------------------------------------------------------------------------
                            If finEtapaTramiteFir = "1" Then
                                If adjuntaFileFir = "1" Then
                                    'traigo la ruta y el id del archivo compartido
                                    '*************************************************************************************************************************
                                    dtsf1 = md_documentacionArchivo.ListarSharedFile(codigo_dofm, "31") '
                                    If dtsf1.Rows.Count > 0 Then
                                        With dtSf.Rows(0)
                                            'ruta_shf = .Item("RutaArchivo")
                                            idArchivoCompartido = .Item("IdArchivosCompartidos")
                                        End With
                                        msgAprueba = clsDocumentacion.ApruebaEtapaTramite(referencia01, codigo_tfu, codigo_per, idArchivoCompartido, "A", "") '''' En este caso referencia 01 as codigo_dta
                                        If msgAprueba <> "" Then
                                            Call mt_ShowMessage(msgAprueba, MessageType.error)
                                        End If
                                    Else
                                        Call mt_ShowMessage("No se han ubicado los datos del archivo compartido, verifique", MessageType.error)
                                    End If
                                Else
                                    msgAprueba = mt_ApruebaEtapaTramite(codigo_dotx)
                                    If msgAprueba <> "" Then
                                        Call mt_ShowMessage(msgAprueba, MessageType.error) '----- falta cambiar
                                    Else
                                        'si requiere enviar correo
                                        mt_enviarCorreo(codigo_dotx)
                                    End If
                                End If
                            End If

                            'envia correo
                            If enviaEmailFir = "1" Then ' ---- si envia email
                                If adjuntaFileFir = "1" Then '-- si  adjuntaarchivo
                                    'traigo la ruta y el id del archivo compartido
                                    '*************************************************************************************************************************
                                    dtSf = md_documentacionArchivo.ListarSharedFile(codigo_dofm, "31") '
                                    If dtSf.Rows.Count > 0 Then
                                        With dtSf.Rows(0)
                                            ruta_shf = .Item("RutaArchivo")
                                            'idArchivoCompartido = .Item("IdArchivosCompartidos")
                                        End With
                                        'envia correo
                                        'Call mt_enviarCorreoAdjunto(codigo_sol, ruta_shf)}
                                        clsDocumentacion.EnviarCorreoAdjunto(referencia01, ruta_shf, codigo_usu, codigo_tfu)
                                    Else
                                        Call mt_ShowMessage("No se han ubicado los datos del archivo compartido, verifique", MessageType.error)
                                    End If
                                Else
                                    mt_enviarCorreo(codigo_dotx)
                                End If
                            End If

                            '----fin cambio 23/10/2020 --------------------------------------------------------------------------------------------------

                            ''***** si no hay firma pendiente actualizamos el documento en archivo
                            Dim md_configuraFirmaTd As New d_configuraFirma
                            Dim dtIdArchivoCompartidoTd As Data.DataTable
                            Dim idArchivoCompartidoTd As Integer = 0
                            '***************************************************************************************************************

                            dtIdArchivoCompartidoTd = md_configuraFirmaTd.ActualizaIdArchivoCompartido(codigo_dotx)

                            If dtIdArchivoCompartidoTd.Rows.Count > 0 Then
                                idArchivoCompartidoTd = CInt(dtIdArchivoCompartidoTd.Rows(0).Item("idArchivoCompartido"))
                            End If
                            ''**** si no hay firma pendientes y ha actualizado el archivo compatido

                            Dim md_docFirma As New d_configuraFirma
                            Dim dtDocFirma As New Data.DataTable
                            dtDocFirma = md_docFirma.ListaEstadoDocumentoFirma("CDOT", codigo_dotx)

                            'If idArchivoCompartidoTd > 0 Then
                            If dtDocFirma.Rows(0).Item("cuenta") = 0 Then

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
                                '**** Actualizamos documentacion *******
                                estado_sol = clsDocumentacion.actualizaEstadoDoc("DOC", codigo_dotx, "3", codigo_usu)
                                If estado_sol = False Then
                                    Call mt_ShowMessage("No se actualizó el estado la documentación", MessageType.error)
                                End If

                            End If

                            Call mt_ShowMessage("Se adjuntó el archivo firmado con éxito", MessageType.success)

                        End If

                        'Call mt_ShowMessage("Se firmó el archivo con éxito", MessageType.success)

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

            dt = md_documento.ListarDocumento("DXF", 0, codigo_per) ''181 para que liste lo de direccion academica, lista doumento configurado
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
            dt = md_confFirma.ListarConfiguraFirma("TOD", 0, 0, codigo_tfu, codigo_usu, codigo_doc, Me.ddlCodigo_est.SelectedValue)

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

    Private Sub mt_CargarComboEstado()
        Try
            md_funciones = New d_Funciones
            md_documentacion = New d_Documentacion
            Dim dt As New Data.DataTable

            dt = md_documentacion.ListarEstados("FMA", 0, "")
            Call md_funciones.CargarCombo(Me.ddlCodigo_est, dt, "codigo_est", "descripcion_est", False, "", "")

        Catch ex As Exception
            Throw ex
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Private Function mt_TraeAbreviaturasDocumento(ByVal codigo_cda As String) As String
        Dim md_configDocArea As New d_ConfigurarDocumentoArea
        Dim dtAbrv As New Data.DataTable
        Dim abreviatura_doc As String
        Dim abreviatura_tid As String
        Dim abreviatura_are As String
        Dim abreviaturasDoc As String = ""

        dtAbrv = md_configDocArea.ListarConfigurarDocumentoArea("COD", codigo_cda, "", 0, 0, 0, 0)
        If dtAbrv.Rows.Count > 0 Then
            With dtAbrv.Rows(0)
                abreviatura_doc = .Item("abreviatura_doc")
                abreviatura_tid = .Item("abreviatura_tid")
                abreviatura_are = .Item("abreviatura_are")
            End With
            abreviaturasDoc = abreviatura_tid & "-" & abreviatura_doc & "-" & abreviatura_are
        End If
        Return abreviaturasDoc
    End Function

    Private Function mt_ApruebaEtapaTramite(ByVal codigo_dotx As String) As String
        Dim mensaje As String = ""
        Dim codigo_dta As Integer
        Dim dtCodigoDta As New Data.DataTable
        md_documentacion = New d_Documentacion
        dtCodigoDta = md_documentacion.ListarCodigoDta(codigo_dotx)
        'cambio 01
        Dim codigo_tes As Integer
        Dim codigo_pst As Integer
        If dtCodigoDta.Rows.Count > 0 Then
            For i As Integer = 0 To dtCodigoDta.Rows.Count - 1
                codigo_dta = CInt(dtCodigoDta.Rows(i).Item("codigo_dta"))
                'cambio 01
                codigo_tes = CInt(dtCodigoDta.Rows(i).Item("codigo_tes"))
                codigo_pst = CInt(dtCodigoDta.Rows(i).Item("codigo_pst"))
                If codigo_dta <> 0 Then
                    Dim dtTramite As New Data.DataTable
                    dtTramite = mt_ActualizarEtapaTramite(codigo_dta, "1", "A", codigo_tfu)
                    If dtTramite.Rows.Count > 0 Then ' SI HAY RESPUESTA DEL CAMBIO DE ETAPA
                        If dtTramite.Rows(0).Item("revision") = True Then ' SI SE HIZO LA REVISIÓN
                            If dtTramite.Rows(0).Item("email") = False Then ' NO SE INSERTO ENVIO CORREO MASIVO                                                
                                'Call mt_ShowMessage("No se pudo realizar el envío de correo correctamente", MessageType.error)
                                mensaje = "No se pudo realizar el envío de correo correctamente"
                            End If
                        Else ' NO SE ACTUALIZO LA ETAPA DEL TRAMITE
                            ''Call mt_ShowMessage("No se pudo realizar la actualización de la etapa del trámite", MessageType.error)
                            mensaje = "No se pudo realizar la actualización de la etapa del trámite"
                        End If
                    Else
                        'Call mt_ShowMessage("No se pudo realizar la actualización de la etapa del trámite", MessageType.error)
                        mensaje = "No se pudo realizar la actualización de la etapa del trámite"
                    End If
                Else
                    'call mt_ShowMessage("Código del trámite invalido", MessageType.error)
                    mensaje = "Código del trámite invalido"
                End If
            Next
            


        End If
        Return mensaje
    End Function

    Private Function mt_enviarCorreo(ByVal codigo_dotx As String) As String
        Dim mensaje As String = ""
        Try

            Dim codigoEnvio As Integer
            Dim ultimaFila As Integer
            Dim alumnos As String = ""
            Dim emailAlumno As String = ""
            Dim md_documentacion As New d_Documentacion

            Dim dtCodigoDta As New Data.DataTable
            dtCodigoDta = md_documentacion.ListarCodigoDta(codigo_dotx)
            'cambio 01
            Dim codigo_tes As Integer
            Dim codigo_pst As Integer
            If dtCodigoDta.Rows.Count > 0 Then

                codigo_tes = CInt(dtCodigoDta.Rows(0).Item("codigo_tes"))
                codigo_pst = CInt(dtCodigoDta.Rows(0).Item("codigo_pst"))

            End If

            Dim rptaEmail As Boolean = False
            'llamo esta clase por que ahi se genera la resolucion de sustentacion y traigo los datos para el envio de correos 
            Dim objGeneraDocumento As New clsGeneraDocumento
            Dim dtEmail As New Data.DataTable
            dtEmail = objGeneraDocumento.fc_DatosResolSustentacion(codigo_tes)

            If dtEmail.Rows.Count > 0 Then

                codigoEnvio = ClsComunicacionInstitucional.ObtenerCodigoEnvio(codigo_usu, codigo_tfu, 77)

                For i As Integer = 0 To dtEmail.Rows.Count - 1

                    ultimaFila = dtEmail.Rows.Count - 1

                    '---------------------- coma
                    If i = ultimaFila Then
                        alumnos = alumnos + dtEmail.Rows(i).Item("alumno")
                        emailAlumno = emailAlumno + dtEmail.Rows(i).Item("eMail_Alu")
                    Else
                        alumnos = alumnos + dtEmail.Rows(i).Item("alumno") & "<br>"
                        If ConfigurationManager.AppSettings("CorreoUsatActivo") = "1" Then
                            emailAlumno = emailAlumno + dtEmail.Rows(i).Item("eMail_Alu") & ";"
                        Else
                            emailAlumno = "fatima.vasquez@usat.edu.pe"
                        End If
                    End If

                Next
                '*************************************
                If dtEmail.Rows(0).Item("codigo_test") = "5" Then
                    ClsComunicacionInstitucional.EnviarNotificacionEmail(codigoEnvio, "SUST", "CJYE", "1", codigo_usu, "codigo_alu", dtEmail.Rows(0).Item("codigo_Alu"), "77", emailAlumno, "", "", "", _
                                                     "Posgrado", dtEmail.Rows(0).Item("titulo_tes"), alumnos, dtEmail.Rows(0).Item("fechaprogramacion"), dtEmail.Rows(0).Item("horaprogramacion"), dtEmail.Rows(0).Item("ambiente"), dtEmail.Rows(0).Item("presidente"), dtEmail.Rows(0).Item("secretario"), dtEmail.Rows(0).Item("vocal"))

                    If ConfigurationManager.AppSettings("CorreoUsatActivo") = "1" Then

                        ClsComunicacionInstitucional.EnviarNotificacionEmail(codigoEnvio, "SUST", "CJYE", "1", codigo_usu, "codigo_alu", dtEmail.Rows(0).Item("codigo_Alu"), "77", "todousat@usat.edu.pe", "", "", "", _
                               "Posgrado", dtEmail.Rows(0).Item("titulo_tes"), alumnos, dtEmail.Rows(0).Item("fechaprogramacion"), dtEmail.Rows(0).Item("horaprogramacion"), dtEmail.Rows(0).Item("ambiente"), dtEmail.Rows(0).Item("presidente"), dtEmail.Rows(0).Item("secretario"), dtEmail.Rows(0).Item("vocal"))
                    Else
                        ClsComunicacionInstitucional.EnviarNotificacionEmail(codigoEnvio, "SUST", "CJYE", "1", codigo_usu, "codigo_alu", dtEmail.Rows(0).Item("codigo_Alu"), "77", "fatima.vasquez@usat.edu.pe", "", "", "", _
                              "Posgrado", dtEmail.Rows(0).Item("titulo_tes"), alumnos, dtEmail.Rows(0).Item("fechaprogramacion"), dtEmail.Rows(0).Item("horaprogramacion"), dtEmail.Rows(0).Item("ambiente"), dtEmail.Rows(0).Item("presidente"), dtEmail.Rows(0).Item("secretario"), dtEmail.Rows(0).Item("vocal"))
                    End If
                Else
                    ClsComunicacionInstitucional.EnviarNotificacionEmail(codigoEnvio, "SUST", "CJYE", "1", codigo_usu, "codigo_alu", dtEmail.Rows(0).Item("codigo_Alu"), "77", emailAlumno, "", "", "", _
                                                     dtEmail.Rows(0).Item("nombreOficial_cpf"), dtEmail.Rows(0).Item("titulo_tes"), alumnos, dtEmail.Rows(0).Item("fechaprogramacion"), dtEmail.Rows(0).Item("horaprogramacion"), dtEmail.Rows(0).Item("ambiente"), dtEmail.Rows(0).Item("presidente"), dtEmail.Rows(0).Item("secretario"), dtEmail.Rows(0).Item("vocal"))

                    If ConfigurationManager.AppSettings("CorreoUsatActivo") = "1" Then

                        ClsComunicacionInstitucional.EnviarNotificacionEmail(codigoEnvio, "SUST", "CJYE", "1", codigo_usu, "codigo_alu", dtEmail.Rows(0).Item("codigo_Alu"), "77", "todousat@usat.edu.pe", "", "", "", _
                               dtEmail.Rows(0).Item("nombreOficial_cpf"), dtEmail.Rows(0).Item("titulo_tes"), alumnos, dtEmail.Rows(0).Item("fechaprogramacion"), dtEmail.Rows(0).Item("horaprogramacion"), dtEmail.Rows(0).Item("ambiente"), dtEmail.Rows(0).Item("presidente"), dtEmail.Rows(0).Item("secretario"), dtEmail.Rows(0).Item("vocal"))

                    Else
                        ClsComunicacionInstitucional.EnviarNotificacionEmail(codigoEnvio, "SUST", "CJYE", "1", codigo_usu, "codigo_alu", dtEmail.Rows(0).Item("codigo_Alu"), "77", "fatima.vasquez@usat.edu.pe", "", "", "", _
                               dtEmail.Rows(0).Item("nombreOficial_cpf"), dtEmail.Rows(0).Item("titulo_tes"), alumnos, dtEmail.Rows(0).Item("fechaprogramacion"), dtEmail.Rows(0).Item("horaprogramacion"), dtEmail.Rows(0).Item("ambiente"), dtEmail.Rows(0).Item("presidente"), dtEmail.Rows(0).Item("secretario"), dtEmail.Rows(0).Item("vocal"))

                    End If
                End If


                md_documentacion.ActualizaEnviaCorreoProgramacion(codigo_pst)


            End If

        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
        Return mensaje
    End Function

    Private Function mt_ApruebaEtapaTramiteByAdjunto(ByVal codigo_dta As String, ByVal idArchivoCompartido As Integer) As String
        Dim mensaje As String = ""
        If codigo_dta <> 0 Or codigo_dta <> "" Then
            Dim dtTramite As New Data.DataTable
            dtTramite = mt_ActualizarEtapaTramiteByAdjunto(codigo_dta, "1", "A", codigo_tfu, idArchivoCompartido)  'le he puesto 7 en duro hasta que cambien la configuracion del trámite
            If dtTramite.Rows.Count > 0 Then ' SI HAY RESPUESTA DEL CAMBIO DE ETAPA
                If dtTramite.Rows(0).Item("revision") = False Then ' NoSI SE HIZO LA REVISIÓN
                    ' NO SE ACTUALIZO LA ETAPA DEL TRAMITE
                    mensaje = "No se pudo realizar la actualización de la etapa del trámite"
                End If
            Else
                mensaje = "No se pudo realizar la actualización de la etapa del trámite"
            End If
        Else
            'call mt_ShowMessage("Código del trámite invalido", MessageType.error)
            mensaje = "Código del trámite invalido"
        End If




        Return mensaje
    End Function
    Private Function mt_ActualizarEtapaTramiteByAdjunto(ByVal codigo_dta As Integer, ByVal tipooperacion As String, ByVal estadoaprobacion As String, ByVal codigo_tfu As Integer, ByVal idArchivoCompartido As Integer) As Data.DataTable
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
                'cmp._observacionEvaluacion = "Su trámite ha sido aceptado, puede descargar su documento <span onClick=""downloadDoc(" & idArchivoCompartido & ");"" style=""cursor: pointer; color: blue;"">aquí</span>"
                cmp._observacionEvaluacion = "Su trámite ha sido aceptado"
                cmp._idArchivoCompartido = idArchivoCompartido
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
    Private Function mt_enviarCorreoAdjunto(ByVal codigo_sol As String, ByVal ruta_shf As String) As String
        Dim mensaje As String = ""
        Try

            Dim codigoEnvio As Integer
            Dim emailAlumno As String = ""
            Dim md_documentacion As New d_Documentacion
            Dim md_solicitaDocumento As New d_SolicitaDocumentacion
            Dim codigo_univ_alu As String = ""

            Dim rptaEmail As Boolean = False
            'llamo esta clase por que ahi se genera la resolucion de sustentacion y traigo los datos para el envio de correos 
            Dim objGeneraDocumento As New clsGeneraDocumento
            Dim dtSol As New Data.DataTable
            Dim dtEmail As New Data.DataTable

            dtSol = md_solicitaDocumento.ListarSolicitaDocumentacion("", codigo_sol, 0, "", 0)
            If dtSol.Rows.Count > 0 Then
                With dtSol.Rows(0)
                    codigo_univ_alu = .Item("codigoUniver_Alu")
                End With
            End If

            dtEmail = objGeneraDocumento.fc_ConsultarAlumnoFichaByCU(codigo_univ_alu)

            If dtEmail.Rows.Count > 0 Then

                codigoEnvio = ClsComunicacionInstitucional.ObtenerCodigoEnvio(codigo_usu, codigo_tfu, 77)

                If ConfigurationManager.AppSettings("CorreoUsatActivo") = "1" Then
                    emailAlumno = emailAlumno + dtEmail.Rows(0).Item("eMail_Alu")
                Else
                    emailAlumno = "olluen@usat.edu.pe"
                End If

                ClsComunicacionInstitucional.EnviarNotificacionEmail(codigoEnvio, "TRVE", "ATRL", "1", codigo_usu, "codigo_alu", dtEmail.Rows(0).Item("codigo_Alu"), "77", emailAlumno, "", "", ruta_shf, _
                      dtEmail.Rows(0).Item("nombre_cpf"), dtEmail.Rows(0).Item("alumno"), "1", "Ficha de Matrícula")


            End If

        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
        Return mensaje
    End Function

#End Region






End Class
