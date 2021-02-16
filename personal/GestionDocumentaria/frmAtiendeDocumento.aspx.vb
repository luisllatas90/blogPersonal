Imports System.Collections.Generic

Partial Class GestionDocumentaria_frmAtiendeDocumento
    Inherits System.Web.UI.Page

#Region "variables"
    Dim md_documentacion As d_Documentacion
    Dim md_solicitaDocumento As d_SolicitaDocumentacion
    Dim md_documento As d_Documento
    Dim md_funciones As d_Funciones
    Dim md_horario As d_Horario
    Dim md_cofiguraDocArea As d_ConfigurarDocumentoArea
    Dim md_area As d_DocArea
    Dim me_cicloAcademico As e_CicloAcademico
    Dim me_documentacionArchivo As e_DocumentacionArchivo
    Dim md_clsGeneraDocumento As New clsGeneraDocumento
    Dim md_documentacionArchivo As New d_DocumentacionArchivo

    'generales
    Dim codigo_sol_gen As String
    Dim estado_sol_gen As String
    Dim referencia01_gen As String
    Dim AbreviaturaDoc_gen As String
    Dim observaTramite_gen As String
    'fin generales

    Dim codigo_tfu As Integer
    Dim codigo_usu As Integer
    Dim tipoEstudio As String
    Dim codigo_per As Integer

    Dim memory As New System.IO.MemoryStream
    Dim bytes As Byte()

    'Private _fuente As String = Server.MapPath(".") & "/img/segoeui.ttf"
    Dim sourceIcon As String = Server.MapPath(".") & "/img/logo_usat.png"
    Dim sourceSello As String = Server.MapPath(".") & "/img/selloWilliOliva.jpg"

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
        'Response.Write(sourceSello)
        Dim usuario As String = Session("perlogin")
        'Response.Write(usuario)
        codigo_tfu = Request.QueryString("ctf")
        'Response.Write(codigo_tfu)
        'tipoestudio = Request.QueryString("mod")
        tipoEstudio = "2"
        codigo_usu = Request.QueryString("id")
        codigo_per = Session("id_per")

        If IsPostBack = False Then
            'Call mt_ShowMessage("¡ Se ha descargado el documento correctamente !", MessageType.success)
            Call mt_CargarComboDocumento()
            Call mt_CargarComboEstado()
            Call mt_CargarDocumentos("TOD")
       
        End If

    End Sub
    Protected Sub gvListaSolicitudes_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvListaSolicitudes.RowCommand
        Try

            Dim memory As New System.IO.MemoryStream
            Dim codigo_cda As Integer
            Dim codigoDatos As Integer
            Dim nombreArchivo As String
            Dim codigo_dot As Integer
            Dim codigo_sol As Integer
            Dim estado_sol As String
            Dim serieCorrelativoDoc As String
            Dim referencia01 As String
            Dim codigo_fac As String
            Dim codigo_cac As String

            Dim finalizaEtapTramGen As String
            Dim enviaEmailGen As String
            Dim adjuntaFileGen As String
            Dim completarDatosGen As String
            Dim ObservaDocGen As String
            Dim indFirma As String
            Dim observaTramite As String

            ''''' Aquí traigo todos los datos necesarios de la solicitud 
            Dim index As Integer = 0 : index = CInt(e.CommandArgument)
            codigo_cda = Me.gvListaSolicitudes.DataKeys(index).Values("codigo_cda")
            codigoDatos = Me.gvListaSolicitudes.DataKeys(index).Values("codigoDatos")
            nombreArchivo = Me.gvListaSolicitudes.DataKeys(index).Values("nombreArchivo").ToString

            codigo_sol = Me.gvListaSolicitudes.DataKeys(index).Values("codigo_sol")
            codigo_sol_gen = Me.gvListaSolicitudes.DataKeys(index).Values("codigo_sol")

            codigo_dot = Me.gvListaSolicitudes.DataKeys(index).Values("codigo_dot")

            estado_sol = Me.gvListaSolicitudes.DataKeys(index).Values("estado_sol")
            estado_sol_gen = Me.gvListaSolicitudes.DataKeys(index).Values("estado_sol")

            referencia01 = Me.gvListaSolicitudes.DataKeys(index).Values("referencia01")
            referencia01_gen = Me.gvListaSolicitudes.DataKeys(index).Values("referencia01")

            codigo_fac = Me.gvListaSolicitudes.DataKeys(index).Values("codigo_fac")
            codigo_cac = Me.gvListaSolicitudes.DataKeys(index).Values("codigo_cac")

            ''---24/10/2020
            finalizaEtapTramGen = Me.gvListaSolicitudes.DataKeys(index).Values("finEtapaTramiteGen")
            enviaEmailGen = Me.gvListaSolicitudes.DataKeys(index).Values("enviaEmailGen")
            adjuntaFileGen = Me.gvListaSolicitudes.DataKeys(index).Values("adjuntaFileGen")
            completarDatosGen = Me.gvListaSolicitudes.DataKeys(index).Values("completarDatosGen")
            ObservaDocGen = Me.gvListaSolicitudes.DataKeys(index).Values("observaDocGen")
            indFirma = Me.gvListaSolicitudes.DataKeys(index).Values("indFirma").ToString

            observaTramite = Me.gvListaSolicitudes.DataKeys(index).Values("observaTramite")
            observaTramite_gen = Me.gvListaSolicitudes.DataKeys(index).Values("observaTramite")
            ''---Fin 24/10/2020 


            '************ para traer la abreviatura del documento *****************************************1
            Dim AbreviaturaDoc As String
            AbreviaturaDoc = mt_TraeAbreviaturasDocumento(codigo_cda)
            AbreviaturaDoc_gen = AbreviaturaDoc
            '**********************************************************************************************1


            Select Case e.CommandName

                Case "ver"
                    Try
                        Dim arreglo As New Dictionary(Of String, String)
                        '---- Sustentación de Resolución / esto se hizo primero

                        arreglo.Add("nombreArchivo", nombreArchivo)
                        arreglo.Add("sesionUsuario", Session("perlogin").ToString)
                        '-----------------                
                        'arreglo.Add("codigo_tes", codigoDatos) se cambia para uniformizar el dat en este caso es codigo_tes
                        arreglo.Add("codigo_datos", codigoDatos)
                        arreglo.Add("codigo_sol", codigo_sol)
                        arreglo.Add("codigo_cac", codigo_cac)
                        arreglo.Add("sourceSello", sourceSello)

                        clsDocumentacion.PrevioDocumentopdf("", arreglo, memory)

                        Dim bytes() As Byte = memory.ToArray
                        memory.Close()

                        Response.Clear()
                        Response.Buffer = False
                        Response.ContentType = "application/pdf"
                        Response.AddHeader("content-disposition", "inline")
                        Response.AddHeader("content-length", bytes.Length.ToString())
                        Response.BinaryWrite(bytes)
                        Response.End()
                        'Response.Flush()
                        'Response.Redirect("")



                        '        HttpContext.Current.Response.ClearHeaders();
                        'HttpContext.Current.Response.Clear();
                        'HttpContext.Current.Response.Buffer = false;
                        'HttpContext.Current.Response.ContentType = "application/pdf";
                        'HttpContext.Current.Response.AddHeader("content-disposition", "inline");
                        'HttpContext.Current.Response.AddHeader("Content-Length", ArchivoPDF.Length.ToString());
                        'HttpContext.Current.Response.Charset = "UTF-8";
                        'HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.Private);
                        'HttpContext.Current.Response.BinaryWrite(ArchivoPDF);
                        'HttpContext.Current.Response.Flush();
                        'HttpContext.Current.Response.Clear();
                        'HttpContext.Current.Response.End();






                       
                    Catch ex As Exception
                        If ex.Message.Replace("'", " ") = "No hay ninguna fila en la posición 0." Then
                            If completarDatosGen = "1" Then
                                Call mt_ShowMessage("¡.. Debe completar datos", MessageType.error)
                                'Me.udpModalListado.Update()
                            End If
                        Else
                            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
                        End If
                    End Try
                    

                Case "generar"
                    Try
                        Me.gvListaSolicitudes.Enabled = False

                        Dim memory1 As New System.IO.MemoryStream
                        Dim arreglov As New Dictionary(Of String, String)
                        arreglov.Add("nombreArchivo", nombreArchivo)
                        arreglov.Add("sesionUsuario", Session("perlogin").ToString)
                        arreglov.Add("codigo_datos", codigoDatos)
                        '-----------------                

                        arreglov.Add("codigo_fac", codigo_fac)
                        arreglov.Add("codigo_sol", codigo_sol)


                        ''''**** 1. GENERA CORRELATIVO DEL DOCUMNETO CONFIGURADO *******************************************************
                        If verificaPrevioDocumento(arreglov, memory1) = "Si" Then
                            serieCorrelativoDoc = clsDocumentacion.ObtenerSerieCorrelativoDocPorCda(codigo_cda, Year(Now), codigo_usu)
                        Else
                            serieCorrelativoDoc = ""
                        End If


                        'serieCorrelativoDoc = clsDocumentacion.ObtenerSerieCorrelativoDocPorAbreviatura("RESO", "SUST", "USAT", Year(Now), codigo_usu)

                        ''''******* GENERA DOCUMENTO PDF *****************************************************************************

                        '******* Desde el primer documento ******************'
                        If codigo_cda = 3 Then 'Resolucion de sustentacion


                            If serieCorrelativoDoc <> "" Then
                                '--------necesarios
                                Dim arreglo As New Dictionary(Of String, String)
                                arreglo.Add("nombreArchivo", nombreArchivo)
                                arreglo.Add("sesionUsuario", Session("perlogin").ToString)
                                arreglo.Add("codigo_datos", codigoDatos)
                                '-----------------                

                                arreglo.Add("codigo_fac", codigo_fac)
                                arreglo.Add("codigo_sol", codigo_sol)


                                '********2. GENERA DOCUMENTO PDF **************************************************************
                                codigo_dot = clsDocumentacion.generarDocumentoPdf(serieCorrelativoDoc, arreglo, codigo_sol, memory)

                                'codigo_dot = clsDocumentacion.generarDocumentoPdf(serieCorrelativoDoc, arreglo)
                                '**********************************************************************************************

                                'Dim bytes() As Byte = memory.ToArray
                                'memory.Close()

                                'Response.Clear()
                                'Response.ContentType = "application/pdf"
                                'Response.AddHeader("content-length", bytes.Length.ToString())
                                'Response.BinaryWrite(bytes)

                                '***************** 3.  Actualiza solicitaDocumentacion********************************************************
                                If codigo_dot <> 0 Then
                                    Dim estado As Boolean = False
                                    estado = clsDocumentacion.actualizaEstadoDoc("SOL", codigo_sol, "2", codigo_usu)
                                    'Call mt_ShowMessage(estado, MessageType.error)
                                    If estado = False Then
                                        Call mt_ShowMessage("No Se ha actualizado el documento", MessageType.error)
                                    End If
                                    Call mt_CargarDocumentos("TOD")
                                    Call mt_ShowMessage("Se generó el Documento con éxito", MessageType.success)
                                Else
                                    Call mt_ShowMessage("No se generó el documento", MessageType.error)
                                End If
                                ''''****************************************************************************************************************
                            Else
                                Call mt_ShowMessage("No se genero el correlativo", MessageType.error)
                            End If

                        Else   '''''' para todos los documentos
                            If serieCorrelativoDoc <> "" Then
                                If AbreviaturaDoc <> "" Then
                                    '----cambio
                                    md_documentacionArchivo = New d_DocumentacionArchivo
                                    Dim dtSf As New Data.DataTable
                                    Dim ruta_shf As String = ""
                                    Dim idArchivoCompartido As String = ""

                                    Dim arreglo As New Dictionary(Of String, String)
                                    arreglo.Add("nombreArchivo", nombreArchivo)
                                    arreglo.Add("sesionUsuario", Session("perlogin").ToString)
                                    arreglo.Add("codigo_datos", codigoDatos)
                                    '-----------------      
                                    arreglo.Add("codigo_fac", codigo_fac)
                                    arreglo.Add("codigo_sol", codigo_sol)
                                    arreglo.Add("sourceSello", sourceSello) '---- el sello o la firma 


                                    '********2. GENERA DOCUMENTO PDF **************************************************************
                                    codigo_dot = clsDocumentacion.generarDocumentoPdf(serieCorrelativoDoc, arreglo, codigo_sol, memory)
                                    'envia correo
                                    If enviaEmailGen = "1" Then ' ---- si envia email
                                        If adjuntaFileGen = "1" Then '-- si  adjuntaarchivo
                                            'traigo la ruta y el id del archivo compartido
                                            '*************************************************************************************************************************
                                            dtSf = md_documentacionArchivo.ListarSharedFile(codigo_dot, "30") '
                                            If dtSf.Rows.Count > 0 Then
                                                With dtSf.Rows(0)
                                                    ruta_shf = .Item("RutaArchivo")
                                                    'idArchivoCompartido = .Item("IdArchivosCompartidos")
                                                End With
                                                'envia correo
                                                'Call mt_enviarCorreoAdjunto(codigo_sol, ruta_shf)}
                                                clsDocumentacion.EnviarCorreoAdjunto(codigo_sol, ruta_shf, codigo_usu, codigo_tfu)
                                            Else
                                                Call mt_ShowMessage("No se han ubicado los datos del archivo compartido, verifique", MessageType.error)
                                            End If

                                        End If
                                    End If
                                    'finaliza esta tramite
                                    If finalizaEtapTramGen = "1" Then
                                        Dim rpt_tramite As String
                                        If adjuntaFileGen = "1" Then
                                            'traigo la ruta y el id del archivo compartido
                                            '*************************************************************************************************************************
                                            dtSf = md_documentacionArchivo.ListarSharedFile(codigo_dot, "30") '
                                            If dtSf.Rows.Count > 0 Then
                                                With dtSf.Rows(0)
                                                    'ruta_shf = .Item("RutaArchivo")
                                                    idArchivoCompartido = .Item("IdArchivosCompartidos")
                                                End With
                                                rpt_tramite = clsDocumentacion.ApruebaEtapaTramite(referencia01, codigo_tfu, codigo_per, idArchivoCompartido, "A", "") '''' En este caso referencia 01 as codigo_dta
                                                If rpt_tramite <> "" Then
                                                    Call mt_ShowMessage(rpt_tramite, MessageType.error)
                                                End If
                                            Else
                                                Call mt_ShowMessage("No se han ubicado los datos del archivo compartido, verifique", MessageType.error)
                                            End If
                                        Else
                                            idArchivoCompartido = ""
                                            rpt_tramite = clsDocumentacion.ApruebaEtapaTramite(referencia01, codigo_tfu, codigo_per, idArchivoCompartido, "A", "") '''' En este caso referencia 01 as codigo_dta
                                            If rpt_tramite <> "" Then
                                                Call mt_ShowMessage(rpt_tramite, MessageType.error)
                                            End If
                                        End If
                                    End If
                                    '---- Fin cambio                                   

                                    '***************** 3.  Actualiza solicitaDocumentacion********************************************************
                                    If codigo_dot <> 0 Then
                                        Dim estado As Boolean = False
                                        If indFirma = "True" Then
                                            estado = clsDocumentacion.actualizaEstadoDoc("SOL", codigo_sol, "2", codigo_usu)
                                        Else
                                            estado = clsDocumentacion.actualizaEstadoDoc("SOL", codigo_sol, "7", codigo_usu)
                                        End If

                                        'If AbreviaturaDoc = "FICH-FMAT-ACAD" Or AbreviaturaDoc = "CERT-CAFC-USAT" Then 'Ficha de matrícula
                                        '    'If AbreviaturaDoc = "FICH-FMAT-ACAD" Or AbreviaturaDoc = "FICH-FNOT-ACAD" Or AbreviaturaDoc = "CERT-CAFC-USAT" Then 'Ficha de matrícula
                                        '    estado = clsDocumentacion.actualizaEstadoDoc("SOL", codigo_sol, "7", codigo_usu)
                                        '    'Call mt_ShowMessage(estado, MessageType.error)
                                        'Else
                                        '    estado = clsDocumentacion.actualizaEstadoDoc("SOL", codigo_sol, "2", codigo_usu)
                                        '    'Call mt_ShowMessage(estado, MessageType.error)
                                        'End If

                                        If estado = False Then
                                            Call mt_ShowMessage("No Se ha actualizado el documento", MessageType.error)
                                        End If
                                        Call mt_CargarDocumentos("TOD")
                                        Call mt_ShowMessage("!.. Se generó el Documento con éxito", MessageType.success)
                                    Else
                                        Call mt_ShowMessage("No se generó el documento", MessageType.error)
                                    End If
                                    ''''****************************************************************************************************************
                                Else
                                    Call mt_ShowMessage("!..No se ubicó la abreviatura del documento", MessageType.error)
                                End If
                            Else
                                Call mt_ShowMessage("No se generó el correlativo, faltan datos al generar documento", MessageType.error)
                            End If
                        End If
                    Catch ex As Exception
                        Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
                    End Try


                Case "descargar"


                    If estado_sol = "7" Then  'si ya esta generado es por que tmb esta con firmas
                        Dim md_configuraFirma As New d_configuraFirma
                        Dim dtConfigFirma As New Data.DataTable
                        dtConfigFirma = md_configuraFirma.ListaEstadoDocumentoFirma("DOT", codigo_dot)
                        Dim codigo_dofm As Integer = dtConfigFirma.Rows(0).Item("codigo_dofm")
                        If codigo_dofm <> 0 Then
                            bytes = md_clsGeneraDocumento.DescargarArchivo(codigo_dofm, 31, "3N23G666FS", Session("perlogin"))
                        End If
                    Else
                        bytes = md_clsGeneraDocumento.DescargarArchivo(codigo_dot, 30, "3N23G777FS", Session("perlogin"))
                        'Call mt_ShowMessage(estado_sol, MessageType.success)
                    End If

                    

                    Response.Clear()
                    Response.Buffer = False
                    Response.Charset = ""
                    Response.Cache.SetCacheability(HttpCacheability.NoCache)
                    Response.ContentType = "application/pdf"
                    Response.AddHeader("content-disposition", "attachment;filename=" & nombreArchivo & ".pdf".ToString.Replace(",", ""))
                    Response.AppendHeader("Content-Length", bytes.Length.ToString())
                    Response.BinaryWrite(bytes)
                    Response.End()

                    Call mt_ShowMessage("¡..Se ha descargado el documento correctamente", MessageType.success)

                Case "observar"
                    Me.txtCodigoSol_mod.Text = codigo_sol
                    Me.txtCodigoEstado_obs.Text = estado_sol

                    Dim dtDatosTramite As New Data.DataTable
                    dtDatosTramite = mt_traeDatosTramite(referencia01) '-- referencia 01 = codigo_dta

                    If dtDatosTramite.Rows.Count > 0 Then
                        Call mt_llenaModalDatosObs(dtDatosTramite, AbreviaturaDoc, observaTramite)
                    Else
                        Call mt_ShowMessage("¡..No se ubicaron datos del trámite", MessageType.error)
                    End If

                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "openModalObserva", "openModalObserva();", True)

                Case "datos"
                    'ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "darclick", "darclick();", True)

                    Me.txtCodigoSol_mod_dat.Text = codigo_sol
                    Me.txtCodigoCda_mod_dat.Text = codigo_cda
                    '----- trae las abreviaturas de los documentos por el codigo_cda
                    If AbreviaturaDoc = "FICH-FMAT-ACAD" Or AbreviaturaDoc = "FICH-FNOT-ACAD" Then
                        'resolución de sustentación de posgrado /tipo/documento/area -- Ficha de matrícula
                        ''--traer datos del trámite
                        Dim dtDatosTramite As New Data.DataTable
                        ''--lleno modal
                        dtDatosTramite = mt_traeDatosTramite(referencia01) '-- referencia 01 = codigo_dta
                        If dtDatosTramite.Rows.Count > 0 Then
                            Call mt_CargarComboCicloAcademico()
                            Call mt_llenaModalDatos(dtDatosTramite, AbreviaturaDoc)
                            Call mt_habilitaDivOpciones()

                        End If

                    End If

                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "openModalDatos", "openModalDatos();", True)
                    'Me.udpModalDatos.Update()
                    'Me.udpModalListado.Update()
                Case "observado"

                    Me.txtObservacion_obs.Text = referencia01
                    Me.txtCodigoSol_mod.Text = codigo_sol
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "openModalObserva", "openModalObserva();", True)

            End Select

        Catch ex As Exception
            'Call mt_ShowMessage("!.. No existe información para emitir este documento", MessageType.error)
            If ex.Message.Replace("'", " ") = "No hay ninguna fila en la posición 0." Then
                Call mt_ShowMessage("!.. No hay información necesesaria para el documento", MessageType.error)
            ElseIf ex.Message.Replace("'", " ") = "La conversión del tipo DBNull en el tipo Integer no es válida." Then
                Call mt_ShowMessage("!.. Debe completar la información", MessageType.error)
            Else
                Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
            End If

        End Try
    End Sub
    Protected Sub ddlCodigo_doc_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlCodigo_doc.SelectedIndexChanged
        If ddlCodigo_doc.SelectedValue = -1 Then
            Call mt_CargarDocumentos("TOD")
            'Call mt_CargarDocumentosByDocumento()
        Else
            Call mt_CargarDocumentos("SXD")
        End If

    End Sub
    Protected Sub ddlCodigo_est_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlCodigo_est.SelectedIndexChanged
        If ddlCodigo_doc.SelectedValue = -1 Then
            Call mt_CargarDocumentos("TOD")
            'Call mt_CargarDocumentosByDocumento()
        Else
            Call mt_CargarDocumentos("SXD")
        End If
    End Sub
    Protected Sub gvListaSolicitudes_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvListaSolicitudes.RowDataBound
        'para traer datos de la solicitu
        Dim md_solicitud As New d_SolicitaDocumentacion
        Dim dtSol As New Data.DataTable




        'Dim txtNombreArchivo As String
        If e.Row.RowType = DataControlRowType.Header Then
            e.Row.TableSection = TableRowSection.TableHeader
        End If


        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim fila As Data.DataRowView
            fila = e.Row.DataItem
            Dim lnkBtnVer As LinkButton = (CType(e.Row.Cells(5).FindControl("btnVer"), LinkButton))
            Dim lnkBtnGenerar As LinkButton = (CType(e.Row.Cells(5).FindControl("btnGenerar"), LinkButton))
            Dim lnkBtnObservar As LinkButton = (CType(e.Row.Cells(5).FindControl("btnObservar"), LinkButton))
            Dim lnkBtnDescargar As LinkButton = (CType(e.Row.Cells(5).FindControl("btnDescargar"), LinkButton))
            Dim lnkBtnObservado As LinkButton = (CType(e.Row.Cells(5).FindControl("btnObservado"), LinkButton))
            Dim lnkBtnDatos As LinkButton = (CType(e.Row.Cells(5).FindControl("btnDatos"), LinkButton))

            Dim celdaNombreArchivo As String = gvListaSolicitudes.DataKeys(e.Row.RowIndex).Values("nombreArchivo").ToString
            Dim celdaCodigo_sol As String = gvListaSolicitudes.DataKeys(e.Row.RowIndex).Values("codigo_sol").ToString
            Dim celdaCodigo_dta As String = gvListaSolicitudes.DataKeys(e.Row.RowIndex).Values("referencia01").ToString
            Dim celdacompletarDatos As String = gvListaSolicitudes.DataKeys(e.Row.RowIndex).Values("completarDatosGen").ToString
            Dim celdaobservaDocGen As String = gvListaSolicitudes.DataKeys(e.Row.RowIndex).Values("observaDocGen").ToString
            '= IIf(fila.Row("nombreArchivo") = "FichaMatricula", fila.Row("alumno"), "Activo")

            If fila.Row("nombreArchivo") = "FichaMatricula" Then
                e.Row.Cells(3).Text = fila.Row("alumno")
            ElseIf fila.Row("nombreArchivo") = "FichaNotas" Then
                e.Row.Cells(3).Text = fila.Row("alumno")
            ElseIf fila.Row("nombreArchivo") = "CertActFormComp" Then
                e.Row.Cells(3).Text = fila.Row("alumno")
            Else
                e.Row.Cells(3).Text = fila.Row("usuario")
            End If

            If e.Row.Cells(4).Text = "PENDIENTE" Then
                lnkBtnVer.Visible = True
                lnkBtnGenerar.Visible = True
                lnkBtnDescargar.Visible = False
                lnkBtnObservado.Visible = False

                If celdacompletarDatos = 1 Then
                    lnkBtnDatos.Visible = True
                Else
                    lnkBtnDatos.Visible = False
                End If
                If celdaobservaDocGen = "1" Then
                    lnkBtnObservar.Visible = True
                Else
                    lnkBtnObservar.Visible = False
                End If
                If celdaNombreArchivo = "FichaMatricula" Or celdaNombreArchivo = "FichaNotas" Then
                    'lnkBtnDatos.Visible = True
                    'lnkBtnObservar.Visible = True
                    dtSol = md_solicitud.ListarSolicitaDocumentacion("", celdaCodigo_sol, 0, "", 0)
                    If dtSol.Rows.Count > 0 Then
                        With dtSol.Rows(0)
                            If .Item("codigo_cac") Is DBNull.Value Then
                                lnkBtnGenerar.Visible = False
                            Else
                                lnkBtnGenerar.Visible = True
                            End If
                        End With
                    End If
                    'e.Row.Cells("3").Text = "TRÁMITES"
                    ' e.Row.Cells(5).Text = IIf(fila.Row("estadoactual_alu") = 0, "Inactivo", "Activo")
                ElseIf celdaNombreArchivo = "CertActFormComp" Then
                    'lnkBtnDatos.Visible = False
                    lnkBtnGenerar.Enabled = True
                    'lnkBtnObservar.Visible = False
                Else
                    lnkBtnDatos.Visible = False
                End If

            ElseIf e.Row.Cells(4).Text = "OBSERVADO" Then

                lnkBtnVer.Visible = False
                lnkBtnGenerar.Visible = False
                'lnkBtnObservar.Visible = False
                lnkBtnDescargar.Visible = False
                lnkBtnObservado.Visible = False
                lnkBtnDatos.Visible = False
              
            Else
                lnkBtnVer.Visible = False
                lnkBtnGenerar.Visible = False
                lnkBtnObservar.Visible = False
                lnkBtnDescargar.Visible = True
                'lnkBtnObservado.Visible = False
                lnkBtnDatos.Visible = False
                lnkBtnObservado.Visible = False
            End If
        End If

    End Sub
    Protected Sub lbGuardarObservacion_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbGuardarObservacion.Click
        Try
            If Me.txtObservacion_obs.Text = "" Then
                Call mt_ShowMessage("¡..Ingrese la observación..!", MessageType.error)
                Call mt_postBackModalDat("observa")
                Exit Sub
            End If

            If Me.txtCodigoSol_mod.Text <> "" Then
                'cambia de estado a la solicitud a observada
                Call mt_observaSolicitud(Me.txtCodigoSol_mod.Text, "9") '--observa solicitud

                'ingresa la observacion en gestión documentaria
                Dim md_observaDocumeto As New d_ObservaDocumentacion
                Dim me_observaDocumento As New e_ObservaDocumentacion
                Dim rptaObservaDoc As Integer

                With me_observaDocumento
                    .codigo_dob = "0"
                    .codigo_sol = Me.txtCodigoSol_mod.Text
                    .observacion = Me.txtObservacion_obs.Text
                    .codigo_dot = Nothing
                    .usuarioReg = codigo_usu
                    .estado_dob = "1"
                    .respuesta = Nothing

                End With
                rptaObservaDoc = md_observaDocumeto.RegistrarActualizarObservaDoc(me_observaDocumento)
                If rptaObservaDoc = 0 Then
                    Call mt_ShowMessage("¡..No se pudo guardar la observación..!", MessageType.error)
                    Exit Sub
                End If

                'observa el trámite
                If Me.txtObservaTramite_obs.Text = "1" Then
                    Dim rpta_tramite As String
                    rpta_tramite = clsDocumentacion.ApruebaEtapaTramite(Me.txtCodigoDta_obs.Text, codigo_tfu, codigo_per, "", "O", Me.txtObservacion_obs.Text)
                    If rpta_tramite <> "" Then
                        Call mt_ShowMessage(rpta_tramite, MessageType.error)
                        Exit Sub
                    End If

                End If
                '---- enviar correo
                Dim dtEmail As New Data.DataTable
                Dim codigoEnvio As Integer
                Dim emailAlumno As String = ""
                md_documentacion = New d_Documentacion

                dtEmail = md_documentacion.ListarDatosTramiteAlumno("EMAIL", Me.txtCodigoDta_obs.Text)

                If dtEmail.Rows.Count > 0 Then

                    codigoEnvio = ClsComunicacionInstitucional.ObtenerCodigoEnvio(codigo_usu, codigo_tfu, 77)

                    If ConfigurationManager.AppSettings("CorreoUsatActivo") = "1" Then
                        emailAlumno = dtEmail.Rows(0).Item("eMail_Alu")
                    Else
                        emailAlumno = "olluen@usat.edu.pe"
                    End If

                    ClsComunicacionInstitucional.EnviarNotificacionEmail(codigoEnvio, "TRVE", "OTVL", "1", codigo_usu, "codigo_alu", dtEmail.Rows(0).Item("codigo_Alu"), "77", emailAlumno, "", "", "", _
                          dtEmail.Rows(0).Item("glosaCorrelativo_trl"), dtEmail.Rows(0).Item("alumno"), dtEmail.Rows(0).Item("descripcion_ctr"))


                End If
                ' fin de envio de correo

                'se actualizó la solicitud
                Call mt_ShowMessage("¡..Se guardó la observación con éxito..!", MessageType.success)
                Call mt_CargarDocumentos("TOD")

            End If

        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)

        End Try
    End Sub
    Protected Sub lbDatosGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbDatosGuardar.Click
        Try
            If Me.txtCodigoSol_mod_dat.Text <> "" Then
                If Me.txtCodigoCda_mod_dat.Text <> "" Then
                    Call mt_TraeAbreviaturasDocumento(Me.txtCodigoCda_mod_dat.Text)
                    If mt_TraeAbreviaturasDocumento(Me.txtCodigoCda_mod_dat.Text) = "FICH-FMAT-ACAD" Or mt_TraeAbreviaturasDocumento(Me.txtCodigoCda_mod_dat.Text) = "FICH-FNOT-ACAD" Then
                        Dim md_solicitaDocumentacion As New d_SolicitaDocumentacion
                        Dim me_solicitaDocumento As New e_SolicitaDocumento
                        Dim dtSolDoc As New Data.DataTable
                        With me_solicitaDocumento
                            .codigo_sol = Me.txtCodigoSol_mod_dat.Text
                            .codigo_cda = Me.txtCodigoCda_mod_dat.Text
                            .codigo_cac = Me.ddlModDatosCodigoCac.SelectedValue
                            .estado_sol = "8"
                            .usuarioReg = codigo_usu
                            .codigoDatos = 0
                        End With
                        If Me.ddlModDatosCodigoCac.SelectedValue <> "" Then
                            ''verificar si hay datos 
                            Dim dtSol As New Data.DataTable
                            Dim dtCons As New Data.DataTable
                            dtSol = md_solicitaDocumentacion.ListarSolicitaDocumentacion("", Me.txtCodigoSol_mod_dat.Text, 0, "", 0)
                            If dtSol.Rows.Count > 0 Then
                                Dim objGeneraDocumento As New clsGeneraDocumento
                                If mt_TraeAbreviaturasDocumento(Me.txtCodigoCda_mod_dat.Text) = "FICH-FNOT-ACAD" Then
                                    dtCons = objGeneraDocumento.fc_DatosFichaMatriculaNotas("FichaNotas", dtSol.Rows(0).Item("codigo_alu"), Me.ddlModDatosCodigoCac.SelectedValue)
                                Else
                                    dtCons = objGeneraDocumento.fc_DatosFichaMatriculaNotas("FichaMatricula", dtSol.Rows(0).Item("codigo_alu"), Me.ddlModDatosCodigoCac.SelectedValue)
                                End If
                            End If
                            ''fin de verifiacr si hay datos
                            If dtCons.Rows.Count = 0 Then
                                Call mt_ShowMessage("!..No encuentran datos para el ciclo académico seleccionado", MessageType.error)
                                ' para no perder los datos del modal -pnpm
                                Call mt_postBackModalDat("datos")
                                '------ fin pnpm
                            Else
                                dtSolDoc = md_solicitaDocumentacion.RegistraActualizaSolicitaDocumentacion(me_solicitaDocumento)

                                Call mt_ShowMessage("!..Se actualizó la solicitud", MessageType.success)
                                Call mt_CargarDocumentos("TOD")
                                'ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "cerrarModalDatos", "cerrarModalDatos();", True)

                            End If
                        Else
                            Call mt_ShowMessage("!..Seleccione el Ciclo Académico", MessageType.error)
                            ' para no perder los datos del modal -pnpm
                            Call mt_postBackModalDat("datos")
                            '------ fin pnpm
                        End If
                    End If
                Else
                    Call mt_ShowMessage("!..configuración de documento no encontrado", MessageType.error)

                End If
            Else
                Call mt_ShowMessage("!..Solicitud no encontrada", MessageType.error)
            End If

        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
       
    End Sub


#End Region

#Region "Procedimeintos y funciones"

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

            dt = md_documento.ListarDocumento("DXP", 0, codigo_per) ''para que liste por personal
            Call md_funciones.CargarCombo(Me.ddlCodigo_doc, dt, "codigo_doc", "descripcion_doc", True, "[-- TODOS --]", "-1")

        Catch ex As Exception
            Throw ex
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Private Sub mt_verificarRespuetaTramite()
        md_solicitaDocumento = New d_SolicitaDocumentacion()
        Dim dt As New Data.DataTable
        dt = md_solicitaDocumento.ListarSolicitaDocumentacion("TOD", 0, Me.ddlCodigo_doc.SelectedValue, "9", codigo_usu)
        If dt.Rows.Count > 0 Then

            For i As Integer = 0 To dt.Rows.Count - 1
                If dt.Rows(i).Item("estado_sol") = "9" Then
                    'busco la respuesta en trami y actualizo campos necescarios
                    Dim md_ObservaDocumento As New d_ObservaDocumentacion
                    Dim rptaTrans As Integer
                    rptaTrans = md_ObservaDocumento.ActualizaRespuestaObservacionTramite(dt.Rows(i).Item("codigo_sol"), dt.Rows(i).Item("referencia01"), "0", codigo_tfu)

                End If
            Next

        End If



    End Sub
    Private Sub mt_CargarDocumentos(ByVal operacion As String)
        Try
            Me.gvListaSolicitudes.Enabled = True

            'aqui para verificar si hay respuesta en el módulo de trámites para los documentos que están observados----------------------------------------

            Call mt_verificarRespuetaTramite()
            '------------------------------------------------------------------------------------------------------------------------------------------------

            md_solicitaDocumento = New d_SolicitaDocumentacion
            Dim dt As New Data.DataTable
            dt = md_solicitaDocumento.ListarSolicitaDocumentacion(operacion, 0, Me.ddlCodigo_doc.SelectedValue, Me.ddlCodigo_est.SelectedValue, codigo_usu)

            If dt.Rows.Count > 0 Then
                Me.gvListaSolicitudes.DataSource = Nothing
                Me.gvListaSolicitudes.DataBind()
                Me.gvListaSolicitudes.DataSource = dt
                Me.gvListaSolicitudes.DataBind()
                'Else
                '    Call mt_ShowMessage("No se encontraron solic emitidos", MessageType.warning)
            Else
                Me.gvListaSolicitudes.DataSource = Nothing
                Me.gvListaSolicitudes.DataBind()
            End If

            'ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "formatoGrilla", "formatoGrilla();", True)
            'Me.udpListado.Update()

        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try

    End Sub
    Private Function consultarDatosReporte(ByVal tipoPrint As String, ByVal codigo_alu As Integer, ByVal codigo_cac As Integer) As Data.DataTable
        Dim dt As New Data.DataTable

        Try
            Dim objcnx As New ClsConectarDatos
            objcnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            objcnx.AbrirConexion()

            If tipoPrint = "1" Then ' si es ficha de matrícula
                dt = objcnx.TraerDataTable("ConsultarNotas", "BA", codigo_alu, codigo_cac, "0")
            Else 'ficha de notas
                dt = objcnx.TraerDataTable("ACAD_FichaNotas", codigo_alu, codigo_cac)
            End If

            objcnx.CerrarConexion()


        Catch ex As Exception
            dt = Nothing
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try

        Return dt

    End Function
    Private Function consultarAlumno(ByVal codigoUniver_Alu As String) As Data.DataTable
        Dim dt As New Data.DataTable
        Try

            Dim objcnx As New ClsConectarDatos
            objcnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            objcnx.AbrirConexion()

            dt = objcnx.TraerDataTable("ConsultarAlumno", "CU", codigoUniver_Alu.Trim)

            objcnx.CerrarConexion()
            If dt.Rows.Count = 0 Then
                Call mt_ShowMessage("No se encontró el alumno..¡", MessageType.warning)
                'Me.txtDescripcionAlu.Text = String.Empty
            End If

        Catch ex As Exception
            dt = Nothing
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try

        Return dt
    End Function
    Private Sub mt_CargarDocumentosByDocumento()
        Try
            md_documentacion = New d_Documentacion
            Dim dt As New Data.DataTable
            'dt = md_documentacion.ListarSolicitaDocumentacion("SXD", 0, Me.ddlCodigo_doc.SelectedValue, "")
            dt = md_documentacion.ListarDocumentacion("ATD", Me.ddlCodigo_doc.SelectedValue, "")
            If dt.Rows.Count > 0 Then
                Me.gvListaSolicitudes.DataSource = dt
                Me.gvListaSolicitudes.DataBind()
                'Else
                '    Call mt_ShowMessage("No se encontraron solic emitidos", MessageType.warning)
            Else
                Me.gvListaSolicitudes.DataSource = Nothing
                Me.gvListaSolicitudes.DataBind()
            End If

        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Private Sub mt_observaSolicitud(ByVal codigo_sol As Integer, ByVal estado As String)
        Try
            ''Call mt_ShowMessage(codigo_sol & " - " & txtObservacion, MessageType.error)
            Dim me_solicitaDocumento As New e_SolicitaDocumento

            Dim dt As New Data.DataTable
            With me_solicitaDocumento
                .codigo_sol = codigo_sol
                .codigoDatos = 0
                .usuarioReg = codigo_usu
                .codigo_cda = 0
                .estado_sol = estado
                '.referencia01 = txtObservacion
            End With
            Dim md_solicitaDocumento As New d_SolicitaDocumentacion
            dt = md_solicitaDocumento.RegistraActualizaSolicitaDocumentacion(me_solicitaDocumento)
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try

    End Sub
    Private Sub mt_CargarComboEstado()
        Try
            md_funciones = New d_Funciones
            md_documentacion = New d_Documentacion
            Dim dt As New Data.DataTable

            dt = md_documentacion.ListarEstados("SOL", 0, "")
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
    Private Function mt_traeDatosTramite(ByVal codigo_dta As String) As Data.DataTable
        Dim dt As New Data.DataTable
        Dim md_DetTramite As New d_DetalleTramiteAlumno

        Try
            dt = md_DetTramite.ConsultaDatosTramite(codigo_dta)

        Catch ex As Exception
            dt = Nothing
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
        Return dt


    End Function
    Private Sub mt_llenaModalDatos(ByVal dtDatosTramite As Data.DataTable, ByVal abreviaturaDoc As String)
        Try
            With dtDatosTramite.Rows(0)
                Me.txtModDatTramite.Text = .Item("glosaCorrelativo_trl")
                Me.txtModDatConcepto.Text = .Item("descripcion_ctr")
                Me.txtModDatDescripcion.Text = .Item("observacion_trl")
                Me.txtModDatAlumnos.Text = .Item("Alumno")
                Me.txtModDatCodUniv.Text = .Item("codigoUniver_Alu")
                Me.txtModDatFecha.Text = .Item("fechaReg_trl")
                If abreviaturaDoc = "FICH-FMAT-ACAD" Or abreviaturaDoc = "FICH-FNOT-ACAD" Then
                    Me.ddlModDatosCodigoCac.SelectedValue = .Item("codigo_cac")
                    'Call mt_ShowMessage(.Item("codigo_cac"), MessageType.error)
                End If

            End With

            'llena datos de la tabla doc_observacionesDocumento
            Dim md_observacionDocumento As New d_ObservaDocumentacion
            Dim dtObservacion As New Data.DataTable
            Dim me_observaDocumento As New e_ObservaDocumentacion

            With me_observaDocumento
                .operacion = "XCS"
                .codigo_dot = Nothing
                .codigo_sol = Me.txtCodigoSol_mod_dat.Text
                .estado_dob = Nothing
                .usuarioReg = Nothing
                .observacion = Nothing
            End With

            dtObservacion = md_observacionDocumento.ListarDocumentosObsrvados(me_observaDocumento)

            If dtObservacion.Rows.Count > 0 Then
                Me.divDescripcionDat.Visible = False
                If Me.txtCodigoEstado_obs.Text = "8" Then
                    Me.divObservacion_mod.Visible = True
                End If
                Me.gvListaObservacionesDat.DataSource = dtObservacion
                Me.gvListaObservacionesDat.DataBind()
            End If

        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try

    End Sub
    Private Sub mt_habilitaDivOpciones()
        Try
            Me.divDdlCodigoCac.Visible = True
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub
    Private Sub mt_CargarComboCicloAcademico()
        Try
            md_funciones = New d_Funciones
            md_horario = New d_Horario
            me_cicloAcademico = New e_CicloAcademico

            Dim dt As New Data.DataTable

            With me_cicloAcademico
                .tipooperacion = "TO"
                .tipocac = "0"
            End With

            dt = md_horario.ObtenerCicloAcademicoHorario(me_cicloAcademico)

            Call md_funciones.CargarCombo(Me.ddlModDatosCodigoCac, dt, "codigo_cac", "descripcion_cac", True, "[-- SELECCIONE --]", "")

        Catch ex As Exception
            Throw ex
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub
    Private Function mt_ApruebaEtapaTramite(ByVal codigo_dta As String) As String
        ''Private Function mt_ApruebaEtapaTramite(ByVal codigo_dta As String, ByVal idArchivoCompartido As Integer) As String
        Dim mensaje As String = ""
        If codigo_dta <> 0 Or codigo_dta <> "" Then
            Dim dtTramite As New Data.DataTable
            dtTramite = mt_ActualizarEtapaTramite(codigo_dta, "1", "A", codigo_tfu)
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
    Private Function mt_ActualizarEtapaTramite(ByVal codigo_dta As Integer, ByVal tipooperacion As String, ByVal estadoaprobacion As String, ByVal codigo_tfu As Integer) As Data.DataTable
        'Private Function mt_ActualizarEtapaTramite(ByVal codigo_dta As Integer, ByVal tipooperacion As String, ByVal estadoaprobacion As String, ByVal codigo_tfu As Integer, ByVal idArchivoCompartido As Integer) As Data.DataTable
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
                'cmp._idArchivoCompartido = idArchivoCompartido
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

    Private Sub mt_llenaModalDatosObs(ByVal dtDatosTramite As Data.DataTable, ByVal abreviaturaDoc As String, ByVal observaDocumento As String)
        Try
            With dtDatosTramite.Rows(0)
                Me.txtTramite_obs.Text = .Item("glosaCorrelativo_trl")
                Me.txtConcepto_obs.Text = .Item("descripcion_ctr")
                'Me.txtDescripcion_obs.Text = .Item("observacion_trl")
                Me.txtAlumno_obs.Text = .Item("Alumno")
                Me.txtFecha_obs.Text = .Item("fechaReg_trl")
                Me.txtCodigoDta_obs.Text = .Item("codigo_dta")
                Me.txtObservaTramite_obs.Text = observaDocumento

                If Me.txtCodigoEstado_obs.Text = "9" Then
                    Me.divObservacion_mod.Visible = False
                    Me.lbGuardarObservacion.Visible = False
                ElseIf Me.txtCodigoEstado_obs.Text = "8" Then
                    Me.divObservacion_mod.Visible = True
                    Me.lbGuardarObservacion.Visible = True
                End If
            End With
           
            'llena datos de la tabla doc_observacionesDocumento
            Dim md_observacionDocumento As New d_ObservaDocumentacion
            Dim dtObservacion As New Data.DataTable
            Dim me_observaDocumento As New e_ObservaDocumentacion

            With me_observaDocumento
                .operacion = "XCS"
                .codigo_dot = Nothing
                .codigo_sol = Me.txtCodigoSol_mod.Text
                .estado_dob = Nothing
                .usuarioReg = Nothing
                .observacion = Nothing
            End With

            dtObservacion = md_observacionDocumento.ListarDocumentosObsrvados(me_observaDocumento)
            If dtObservacion.Rows.Count > 0 Then
                Me.gvListaObservaciones.DataSource = dtObservacion
                Me.gvListaObservaciones.DataBind()
            End If

        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try

    End Sub

    Private Sub mt_postBackModalDat(ByVal modal As String)

        Me.txtCodigoSol_mod.Text = codigo_sol_gen
        Me.txtCodigoEstado_obs.Text = estado_sol_gen

        Dim dtDatosTramite As New Data.DataTable
        dtDatosTramite = mt_traeDatosTramite(referencia01_gen) '-- referencia 01 = codigo_dta


        If dtDatosTramite.Rows.Count > 0 Then
            Call mt_llenaModalDatosObs(dtDatosTramite, AbreviaturaDoc_gen, observaTramite_gen)
        Else
            Call mt_ShowMessage("¡..No se ubicaron datos del trámite", MessageType.error)
        End If

        If modal = "datos" Then

            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "openModalDatos", "openModalDatos();", True)

        ElseIf modal = "observa" Then
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "openModalObserva", "openModalObserva();", True)
        End If
        





    End Sub

    Private Function verificaPrevioDocumento(ByVal arreglo As Dictionary(Of String, String), ByVal memory1 As System.IO.MemoryStream) As String
        Try
            clsDocumentacion.PrevioDocumentopdf("", arreglo, memory1)
            Dim bytes() As Byte = memory1.ToArray
            memory1.Close()
            Return "Si"
        Catch ex As Exception
            Return "No"
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try

    End Function
#End Region




    
   
   
   
End Class






