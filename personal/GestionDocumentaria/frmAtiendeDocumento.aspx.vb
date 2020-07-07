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

    Dim codigo_tfu As Integer
    Dim codigo_usu As Integer
    Dim tipoEstudio As String

    Dim memory As New System.IO.MemoryStream
    Dim bytes As Byte()

    'Private _fuente As String = Server.MapPath(".") & "/img/segoeui.ttf"
    Dim sourceIcon As String = Server.MapPath(".") & "/img/logo_usat.png"
    Dim sourceSello As String = Server.MapPath(".") & "/img/selloDiracadMtesen.png"
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

        If IsPostBack = False Then
            'Call mt_ShowMessage("¡ Se ha descargado el documento correctamente !", MessageType.success)
            Call mt_CargarComboDocumento()
            Call mt_CargarDocumentos("TOD")
        Else
            Call mt_CargarDocumentos("TOD")
        End If
    End Sub
    Protected Sub gvListaSolicitudes_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvListaSolicitudes.RowCommand
        Try
            'Dim dtDet As New Data.DataTable
            'Dim dtAlumno As New Data.DataTable
            'Dim dtCorrelativo As New Data.DataTable
            'Dim usuario As String = Session("perlogin")
            'Dim codigo_alu, codigo_cac, codigo_cda, correlativo_dot, codigo_dot, codigo_sol, codigo_doc As Integer
            'Dim indFirma As Boolean
            'Dim codigoUniver_Alu, referencia01, serieCorrelativo, respuesta As String

            Dim memory As New System.IO.MemoryStream

            'codigo_alu = Me.gvListaSolicitudes.DataKeys(index).Values("codigo_alu")
            'codigo_cac = Me.gvListaSolicitudes.DataKeys(index).Values("codigo_cac")
            'codigoUniver_Alu = Me.gvListaSolicitudes.DataKeys(index).Values("codigoUniver_Alu").ToString
            'referencia01 = Me.gvListaSolicitudes.DataKeys(index).Values("referencia01").ToString
            'codigo_sol = Me.gvListaSolicitudes.DataKeys(index).Values("codigo_sol")
            'codigo_dot = Me.gvListaSolicitudes.DataKeys(index).Values("codigo_dot")
            'codigo_doc = Me.gvListaSolicitudes.DataKeys(index).Values("codigo_doc")

            'md_documentacion = New d_Documentacion
            'md_documentacion = New d_Documentacion

            Dim codigo_cda As Integer
            Dim codigoDatos As Integer
            Dim nombreArchivo As String
            Dim codigo_dot As Integer
            Dim codigo_sol As Integer
            Dim estado_sol As String
            Dim serieCorrelativoDoc As String
            Dim referencia01 As String
            Dim codigo_fac As String

            Dim index As Integer = 0 : index = CInt(e.CommandArgument)
            codigo_cda = Me.gvListaSolicitudes.DataKeys(index).Values("codigo_cda")
            codigoDatos = Me.gvListaSolicitudes.DataKeys(index).Values("codigoDatos")
            nombreArchivo = Me.gvListaSolicitudes.DataKeys(index).Values("nombreArchivo").ToString
            codigo_sol = Me.gvListaSolicitudes.DataKeys(index).Values("codigo_sol")
            codigo_dot = Me.gvListaSolicitudes.DataKeys(index).Values("codigo_dot")
            estado_sol = Me.gvListaSolicitudes.DataKeys(index).Values("estado_sol")
            referencia01 = Me.gvListaSolicitudes.DataKeys(index).Values("referencia01")
            codigo_fac = Me.gvListaSolicitudes.DataKeys(index).Values("codigo_fac")


            Select Case e.CommandName
                Case "ver"
                    Dim arreglo As New Dictionary(Of String, String)
                    '---- Sustentación de Resolución
                    If codigo_cda = 3 Then

                        arreglo.Add("nombreArchivo", nombreArchivo)
                        arreglo.Add("sesionUsuario", Session("perlogin").ToString)
                        '-----------------                
                        arreglo.Add("codigo_tes", codigoDatos)


                        clsDocumentacion.PrevioDocumentopdf("", arreglo, memory)
                        Dim bytes() As Byte = memory.ToArray
                        memory.Close()

                        Response.Clear()
                        Response.ContentType = "application/pdf"
                        Response.AddHeader("content-length", bytes.Length.ToString())
                        Response.BinaryWrite(bytes)

                    End If

                Case "generar"
                    Try

                        If codigo_cda = 3 Then 'Resolucion de sustentacion

                            ''''**** 1. GENERA CORRELATIVO DEL DOCUMNETO CONFIGURADO *******************************************************
                            serieCorrelativoDoc = clsDocumentacion.ObtenerSerieCorrelativoDocPorCda(codigo_cda, Year(Now), codigo_usu)
                            ''''******* GENERA DOCUMENTO PDF *****************************************************************************

                            If serieCorrelativoDoc <> "" Then
                                '--------necesarios
                                Dim arreglo As New Dictionary(Of String, String)
                                arreglo.Add("nombreArchivo", nombreArchivo)
                                arreglo.Add("sesionUsuario", Session("perlogin").ToString)
                                '-----------------                
                                arreglo.Add("codigo_tes", codigoDatos)
                                arreglo.Add("codigo_fac", codigo_fac)

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
                        End If
                    Catch ex As Exception
                        Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
                    End Try

                    '    Dim nombreArchivo As String
                    '    dtCorrelativo = md_documentacion.GeneraCorrelativoDocumentacion(1, 1, 181, codigo_doc, Year(Now), codigo_usu) '--tipodocumento, area,tipofuncion,documento, anio, usuario

                    '    If dtCorrelativo.Rows.Count > 0 Then


                    '        dtDet = consultarDatosReporte(codigo_doc, codigo_alu, codigo_cac)
                    '        If dtDet.Rows.Count > 0 Then
                    '            'Me.txtCodigo_cda.Text = dtCorrelativo.Rows(0).Item("correlativo_dot") & "-" & dtDet.Rows.Count
                    '            dtAlumno = consultarAlumno(codigoUniver_Alu)

                    '        End If

                    '        With dtCorrelativo.Rows(0)
                    '            codigo_cda = .Item("codigo_cda")
                    '            correlativo_dot = .Item("correlativo_dot")
                    '            'anio_dot = .Item("anio_dot")
                    '            serieCorrelativo = .Item("configuracion")
                    '            codigo_dot = .Item("codigo_dot")
                    '            indFirma = .Item("indFirma")
                    '        End With

                    '        md_clsGeneraDocumento.fuente = Server.MapPath(".") & "/font/segoeui.ttf"

                    '        If codigo_doc = "1" Then
                    '            md_clsGeneraDocumento.EmiteFichaMatricula(serieCorrelativo, memory, dtDet, dtAlumno, False, sourceIcon, sourceSello, referencia01)
                    '            nombreArchivo = "FichaMatricula.pdf"
                    '        Else
                    '            md_clsGeneraDocumento.EmiteFichaNotas(serieCorrelativo, memory, dtDet, dtAlumno, False, sourceIcon, sourceSello, referencia01)
                    '            nombreArchivo = "FichaNotas.pdf"
                    '        End If


                    '        Dim bytes() As Byte = memory.ToArray
                    '        memory.Close()

                    '        ''subir archivo
                    '        respuesta = md_clsGeneraDocumento.fc_SubirArchivo(30, codigo_dot, 0, memory, nombreArchivo, Session("perlogin").ToString)


                    '        ' ''*******************documentacion archivo
                    '        me_documentacionArchivo = New e_DocumentacionArchivo
                    '        md_documentacionArchivo = New d_DocumentacionArchivo

                    '        Dim dtSf As New Data.DataTable
                    '        Dim codigo_shf As Integer
                    '        ''traigo el sharedFile
                    '        dtSf = md_documentacionArchivo.ListarSharedFile(codigo_dot, "30") '---30 codigo de la tabla
                    '        If dtSf.Rows.Count > 0 Then
                    '            With dtSf.Rows(0)
                    '                codigo_shf = .Item("idArchivosCompartidos")
                    '            End With
                    '        Else
                    '            codigo_shf = 0
                    '        End If

                    '        With me_documentacionArchivo
                    '            .codigo_doa = 0
                    '            .codigo_dot = codigo_dot
                    '            .codigo_shf = codigo_shf
                    '            .estado_doa = "1"
                    '            .usuarioReg = codigo_usu
                    '            .codigo_sol = codigo_sol
                    '        End With

                    '        'Me.txtCodigo_cda.Text = codigo_sol
                    '        'inserto la documentacionArchivo
                    '        md_documentacionArchivo.RegistrarActualizarDocumentacionArchivo(me_documentacionArchivo)

                    '        ''''''' ************************ Firmas
                    '        'si requiere firma
                    '        If indFirma Then
                    '            md_documentacion.CrearActualizarFirmasDocumento(0, codigo_dot, codigo_shf, codigo_cda, codigo_usu, "2")
                    '        End If
                    '        '''''''''' fin firmas
                    '        If Me.ddlCodigo_doc.SelectedValue = -1 Then
                    '            Call mt_CargarDocumentosByDocumento()
                    '        Else
                    '            Call mt_CargarDocumentosByDocumento()
                    '        End If

                    '        Call mt_ShowMessage("Archivo generado con éxito", MessageType.success)

                    '    Else
                    '        Call mt_ShowMessage("Este documento no se encuentra configurado", MessageType.error)
                    '        Exit Sub
                    '    End If

                Case "descargar"
                    'Call mt_ShowMessage("¡ Aqui !", MessageType.success)
                    'mt_ShowMessage("¡ Se ha descargado el silabo correctamente !", MessageType.success)
                    'mt_ShowMessage(codigo_dot, MessageType.success)
                    If codigo_cda = 3 Then '' resolucion de sustentcion
                        If estado_sol <> "7" Then
                            bytes = md_clsGeneraDocumento.DescargarArchivo(codigo_dot, 30, "3N23G777FS", Session("perlogin"))
                        Else
                            bytes = md_clsGeneraDocumento.DescargarArchivo(codigo_dot, 32, "3N23G666FS", Session("perlogin"))
                        End If
                    End If
                    'bytes = md_clsGeneraDocumento.DescargarArchivo(codigo_dot, 30, "3N23G777FS", Session("perlogin"))
                    'mt_DescargarArchivo(codigo_dot, 30, "3N23G777FS")

                    Response.Clear()
                    Response.Buffer = False
                    Response.Charset = ""
                    Response.Cache.SetCacheability(HttpCacheability.NoCache)
                    Response.ContentType = "application/pdf"
                    Response.AddHeader("content-disposition", "attachment;filename=" & "Documentacion.pdf".ToString.Replace(",", ""))
                    Response.AppendHeader("Content-Length", bytes.Length.ToString())
                    Response.BinaryWrite(bytes)
                    Response.End()

                    Call mt_ShowMessage("¡ Se ha descargado el documento correctamente !", MessageType.success)

                Case "observar"
                    Me.txtCodigoSol_mod.Text = codigo_sol
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "openModalObserva", "openModalObserva();", True)

                Case "observado"
                    Me.txtObservacion_mod.Text = referencia01
                    Me.txtCodigoSol_mod.Text = codigo_sol
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "openModalObserva", "openModalObserva();", True)

            End Select

        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
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
    Protected Sub gvListaSolicitudes_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvListaSolicitudes.RowDataBound

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

            If e.Row.Cells(4).Text = "PENDIENTE" Then
                lnkBtnVer.Visible = True
                lnkBtnGenerar.Visible = True
                lnkBtnObservar.Visible = True
                lnkBtnDescargar.Visible = False
                lnkBtnObservado.Visible = False
            ElseIf e.Row.Cells(4).Text = "OBSERVADO" Then
                lnkBtnVer.Visible = True
                lnkBtnGenerar.Visible = False
                lnkBtnObservar.Visible = False
                lnkBtnDescargar.Visible = False
                lnkBtnObservado.Visible = True
            Else
                lnkBtnVer.Visible = False
                lnkBtnGenerar.Visible = False
                lnkBtnObservar.Visible = False
                lnkBtnDescargar.Visible = True
                lnkBtnObservado.Visible = False
            End If
        End If

    End Sub
    Protected Sub lbVerProcesadas_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbVerProcesadas.Click
        'Call mt_CargarDocumentosByDocumento()
        Call mt_CargarDocumentos("PRO")
    End Sub
    Protected Sub lbVerPendientes_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbVerPendientes.Click
        Call mt_CargarDocumentos("PEN")
    End Sub
    Protected Sub lbGuardarObservacion_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbGuardarObservacion.Click
        Try
            If Me.txtObservacion_mod.Text = "" Then
                Call mt_ShowMessage("¡..Ingrese la observación..!", MessageType.error)
                Exit Sub
            End If

            If Me.txtCodigoSol_mod.Text <> "" Then
                Call mt_observaSolicitud(Me.txtCodigoSol_mod.Text, Me.txtObservacion_mod.Text)
                Call mt_CargarDocumentos("TOD")
                Call mt_ShowMessage("¡..Se guardó la observación..!", MessageType.success)
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

            dt = md_documento.ListarDocumento("DCN", 0, codigo_tfu) ''181 para que liste lo de direccion academica, lista doumento configurado
            Call md_funciones.CargarCombo(Me.ddlCodigo_doc, dt, "codigo_doc", "descripcion_doc", True, "[-- TODOS --]", "-1")

        Catch ex As Exception
            Throw ex
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub
    Private Sub mt_CargarDocumentos(ByVal operacion As String)
        Try
            md_solicitaDocumento = New d_SolicitaDocumentacion

            Dim dt As New Data.DataTable

            dt = md_solicitaDocumento.ListarSolicitaDocumentacion(operacion, 0, Me.ddlCodigo_doc.SelectedValue, "")

            If dt.Rows.Count > 0 Then
                Me.gvListaSolicitudes.DataSource = dt
                Me.gvListaSolicitudes.DataBind()
                'Else
                '    Call mt_ShowMessage("No se encontraron solic emitidos", MessageType.warning)
            Else
                Me.gvListaSolicitudes.DataSource = Nothing
                Me.gvListaSolicitudes.DataBind()
            End If

            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "formatoGrilla", "formatoGrilla();", True)

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
    Private Sub mt_observaSolicitud(ByVal codigo_sol As Integer, ByVal txtObservacion As String)
        Try
            'Call mt_ShowMessage(codigo_sol & " - " & txtObservacion, MessageType.error)
            Dim me_solicitaDocumento As New e_SolicitaDocumento

            Dim dt As New Data.DataTable
            With me_solicitaDocumento
                .codigo_sol = codigo_sol
                .codigoDatos = 0
                .usuarioReg = codigo_usu
                .codigo_cda = 0
                .estado_sol = "9" '--observada
                .referencia01 = txtObservacion
            End With
            Dim md_solicitaDocumento As New d_SolicitaDocumentacion
            dt = md_solicitaDocumento.RegistraActualizaSolicitaDocumentacion(me_solicitaDocumento)

        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try

    End Sub

#End Region


    
    'Protected Sub lbGenerarInforme_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbGenerarInforme.Click

    '    Dim codigo_dot As Integer
    '    Dim codigo_cda As Integer = 4  ''-- Configuracion del documento
    '    Dim serieCorrelativoDoc As String

    '    ''''**** 1. GENERA CORRELATIVO DEL DOCUMNETO CONFIGURADO *******************************************************
    '    serieCorrelativoDoc = clsDocumentacion.ObtenerSerieCorrelativoDocPorCda(codigo_cda, Year(Now), codigo_usu)
    '    ''''******* GENERA DOCUMENTO PDF *****************************************************************************

    '    If serieCorrelativoDoc <> "" Then
    '        '--------necesarios
    '        Dim arreglo As New Dictionary(Of String, String)
    '        arreglo.Add("nombreArchivo", "InformeDeAsesor")
    '        arreglo.Add("sesionUsuario", Session("perlogin").ToString)
    '        '-----------------                
    '        arreglo.Add("codigo_tes", "6980")

    '        '********2. GENERA DOCUMENTO PDF **************************************************************
    '        codigo_dot = clsDocumentacion.generarDocumentoPdf(serieCorrelativoDoc, arreglo, 0, memory)
    '        'codigo_dot = clsDocumentacion.generarDocumentoPdf(serieCorrelativoDoc, arreglo)
    '        '**********************************************************************************************

    '        Dim bytes() As Byte = memory.ToArray
    '        memory.Close()

    '        Response.Clear()
    '        Response.ContentType = "application/pdf"
    '        Response.AddHeader("content-length", bytes.Length.ToString())
    '        Response.BinaryWrite(bytes)
    '    Else
    '        Call mt_ShowMessage("no hay nada", MessageType.error)
    '    End If
    'End Sub
End Class






