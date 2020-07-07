
Partial Class GestionDocumentaria_frmAtencionDocumentaria
    Inherits System.Web.UI.Page
#Region "variables"
    Dim md_documentacion As d_Documentacion
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
        'Dim usuario As String = Session("perlogin")
        'Response.Write(usuario)
        codigo_tfu = Request.QueryString("ctf")
        'Response.Write(codigo_tfu)
        'tipoestudio = Request.QueryString("mod")
        tipoEstudio = "2"
        codigo_usu = Request.QueryString("id")

        If IsPostBack = False Then
            Call mt_ShowMessage("¡ Se ha descargado el documento correctamente !", MessageType.success)
            Call mt_CargarComboDocumento()
            Call mt_CargarSolicitudes("TOD")
        End If
    End Sub
    Protected Sub ddlCodigo_doc_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlCodigo_doc.SelectedIndexChanged
        If ddlCodigo_doc.SelectedValue = -1 Then
            Call mt_CargarSolicitudes("TOD")
        Else
            'Me.ddlCodigo_are.Enabled = True
            'Call mt_CargarComboArea()
            'Me.gvListaSolicitudes.DataSource = Nothing
            'Me.gvListaSolicitudes.DataBind()
            Call mt_CargarSolicitudesByDocumento()
        End If
        
    End Sub
    Protected Sub ddlCodigo_are_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlCodigo_are.SelectedIndexChanged
        Try
            Dim dt As New Data.DataTable

            If ddlCodigo_are.SelectedValue <> "" And ddlCodigo_doc.SelectedValue <> "" Then
                dt = mt_ListaCDA()
                If dt.Rows.Count > 0 Then
                    With dt.Rows(0)
                        'Me.txtCodigo_cda.Text = .Item("codigo_cda")
                    End With
                Else
                    'Me.txtCodigo_cda.Text = "0"
                End If
            End If

            Call mt_CargarSolicitudesByDocumento()

        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub
    Protected Sub gvListaSolicitudes_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvListaSolicitudes.RowCommand
        Try

            Dim dtDet As New Data.DataTable
            Dim dtAlumno As New Data.DataTable
            Dim dtCorrelativo As New Data.DataTable
            Dim usuario As String = Session("perlogin")
            Dim codigo_alu, codigo_cac, codigo_cda, correlativo_dot, codigo_dot, codigo_sol, codigo_doc, indFirma As Integer
            Dim codigoUniver_Alu, referencia01, serieCorrelativo, respuesta As String
            Dim index As Integer = 0 : index = CInt(e.CommandArgument)

            codigo_alu = Me.gvListaSolicitudes.DataKeys(index).Values("codigo_alu")
            codigo_cac = Me.gvListaSolicitudes.DataKeys(index).Values("codigo_cac")
            codigoUniver_Alu = Me.gvListaSolicitudes.DataKeys(index).Values("codigoUniver_Alu").ToString
            referencia01 = Me.gvListaSolicitudes.DataKeys(index).Values("referencia01").ToString
            codigo_sol = Me.gvListaSolicitudes.DataKeys(index).Values("codigo_sol")
            codigo_dot = Me.gvListaSolicitudes.DataKeys(index).Values("codigo_dot")
            codigo_doc = Me.gvListaSolicitudes.DataKeys(index).Values("codigo_doc")

            md_documentacion = New d_Documentacion
            md_documentacion = New d_Documentacion

            Select Case e.CommandName
                Case "ver"



                    dtDet = consultarDatosReporte(codigo_doc, codigo_alu, codigo_cac)
                    If dtDet.Rows.Count > 0 Then
                        dtAlumno = consultarAlumno(codigoUniver_Alu)
                    End If

                    md_clsGeneraDocumento.fuente = Server.MapPath(".") & "/font/segoeui.ttf"

                    ''si tipo de impresión Fich de MAtrícula
                    If codigo_doc = "8" Then

                        md_clsGeneraDocumento.EmiteFichaMatricula("", memory, dtDet, dtAlumno, True, sourceIcon, sourceSello, referencia01)
                        Dim bytes() As Byte = memory.ToArray
                        memory.Close()

                        Response.Clear()
                        Response.ContentType = "application/pdf"
                        Response.AddHeader("content-length", bytes.Length.ToString())
                        Response.BinaryWrite(bytes)
                    Else


                        md_clsGeneraDocumento.EmiteFichaNotas("", memory, dtDet, dtAlumno, True, sourceIcon, sourceSello, referencia01)
                        Dim bytes() As Byte = memory.ToArray
                        memory.Close()

                        Response.Clear()
                        Response.ContentType = "application/pdf"
                        Response.AddHeader("content-length", bytes.Length.ToString())
                        Response.BinaryWrite(bytes)

                    End If

                Case "generar"


                    dtCorrelativo = md_documentacion.GeneraCorrelativoDocumentacion(1, 1, 181, codigo_doc, Year(Now), codigo_usu) '--tipodocumento, area,tipofuncion,documento, anio, usuario

                    If dtCorrelativo.Rows.Count > 0 Then

                        Console.Write("aqui")

                        dtDet = consultarDatosReporte(codigo_doc, codigo_alu, codigo_cac)
                        If dtDet.Rows.Count > 0 Then
                            'Me.txtCodigo_cda.Text = dtCorrelativo.Rows(0).Item("correlativo_dot") & "-" & dtDet.Rows.Count
                            dtAlumno = consultarAlumno(codigoUniver_Alu)
                        End If

                        With dtCorrelativo.Rows(0)
                            codigo_cda = .Item("codigo_cda")
                            correlativo_dot = .Item("correlativo_dot")
                            'anio_dot = .Item("anio_dot")
                            serieCorrelativo = .Item("configuracion")
                            codigo_dot = .Item("codigo_dot")
                            indFirma = .Item("indFirma")
                        End With

                        md_clsGeneraDocumento.fuente = Server.MapPath(".") & "/font/segoeui.ttf"

                        If codigo_doc = "1" Then
                            md_clsGeneraDocumento.EmiteFichaMatricula(serieCorrelativo, memory, dtDet, dtAlumno, False, sourceIcon, sourceSello, referencia01)
                        Else
                            md_clsGeneraDocumento.EmiteFichaNotas(serieCorrelativo, memory, dtDet, dtAlumno, False, sourceIcon, sourceSello, referencia01)
                        End If


                        Dim bytes() As Byte = memory.ToArray
                        memory.Close()

                        ''subir archivo
                        respuesta = md_clsGeneraDocumento.fc_SubirArchivo(30, codigo_dot, 0, memory, "FichaMatricula.pdf", Session("perlogin").ToString)


                        ' ''*******************documentacion archivo
                        me_documentacionArchivo = New e_DocumentacionArchivo
                        md_documentacionArchivo = New d_DocumentacionArchivo

                        Dim dtSf As New Data.DataTable
                        Dim codigo_shf As Integer
                        ''traigo el sharedFile
                        dtSf = md_documentacionArchivo.ListarSharedFile(codigo_dot, "30") '---30 codigo de la tabla
                        If dtSf.Rows.Count > 0 Then
                            With dtSf.Rows(0)
                                codigo_shf = .Item("idArchivosCompartidos")
                            End With
                        Else
                            codigo_shf = 0
                        End If

                        With me_documentacionArchivo
                            .codigo_doa = 0
                            .codigo_dot = codigo_dot
                            .codigo_shf = codigo_shf
                            .estado_doa = "1"
                            .usuarioReg = codigo_usu
                            .codigo_sol = codigo_sol
                        End With

                        'Me.txtCodigo_cda.Text = codigo_sol
                        'inserto la documentacionArchivo
                        md_documentacionArchivo.RegistrarActualizarDocumentacionArchivo(me_documentacionArchivo)

                        ''''''' ************************ Firmas
                        'si requiere firma
                        If indFirma = 1 Then
                            md_documentacion.CrearFirmasParaDocumento(codigo_dot, codigo_shf, codigo_cda, codigo_usu)
                        End If
                        '''''''''' fin firmas
                        If Me.ddlCodigo_doc.SelectedValue = -1 Then
                            Call mt_CargarSolicitudes("TOD")
                        Else
                            Call mt_CargarSolicitudes("SXD")
                        End If



                        Call mt_ShowMessage("Archivo generado con éxito", MessageType.success)

                    Else
                        Call mt_ShowMessage("Este documento no se encuentra configurado", MessageType.error)
                        Exit Sub
                    End If

                Case "descargar"

                    'mt_ShowMessage("¡ Se ha descargado el silabo correctamente !", MessageType.success)

                    bytes = md_clsGeneraDocumento.DescargarArchivo(codigo_dot, 30, "3N23G777FS", usuario)
                    'mt_DescargarArchivo(codigo_dot, 30, "3N23G777FS")

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

            End Select

        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub
    Protected Sub gvListaSolicitudes_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvListaSolicitudes.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim fila As Data.DataRowView
            fila = e.Row.DataItem

            If e.Row.Cells(4).Text = "PROCESADA" Then
                'CType(e.Row.Cells(15).FindControl("lnkbtnresend"), LinkButton)).Enabled = False
                Dim lnk As LinkButton = (CType(e.Row.Cells(5).FindControl("btnVer"), LinkButton))
                'lnk.Attributes.Remove("OnClientClick")
                lnk.Visible = False
                Dim lnk1 As LinkButton = (CType(e.Row.Cells(5).FindControl("btnGenerar"), LinkButton))
                'lnk.Attributes.Remove("OnClientClick")
                lnk1.Visible = False
            Else
                Dim lnk As LinkButton = (CType(e.Row.Cells(5).FindControl("btnDescargar"), LinkButton))
                'lnk.Attributes.Remove("OnClientClick")
                lnk.Visible = False
            End If
        End If
    End Sub
    Protected Sub lbVerProcesadas_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbVerProcesadas.Click
        Call mt_CargarSolicitudes("PRO")
    End Sub
    Protected Sub lbVerPendientes_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbVerPendientes.Click
        Call mt_CargarSolicitudes("PEN")
    End Sub

#End Region
#Region "Metodos Funciones"

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

            dt = md_documento.ListarDocumento("DCN", 0, 181) ''181 para que liste lo de direccion academica, lista doumento configurado
            Call md_funciones.CargarCombo(Me.ddlCodigo_doc, dt, "codigo_doc", "descripcion_doc", True, "[-- TODOS --]", "-1")

        Catch ex As Exception
            Throw ex
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub
    Private Sub mt_CargarComboArea()
        Try
            md_funciones = New d_Funciones
            md_area = New d_DocArea
            Dim dt As New Data.DataTable

            If Me.ddlCodigo_doc.SelectedValue <> "" Then
                dt = md_area.ListarArea("AXD", 0, Me.ddlCodigo_doc.SelectedValue) ''lista el area x documento configurado
                Call md_funciones.CargarCombo(Me.ddlCodigo_are, dt, "codigo_are", "descripcion_are", True, "[-- SELECCIONE --]", "")
            Else
                Me.ddlCodigo_are.DataSource = Nothing
                Me.ddlCodigo_are.Items.Clear()
            End If

        Catch ex As Exception
            Throw ex
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub
    Private Function mt_ListaCDA() As Data.DataTable
        Dim dt As New Data.DataTable
        Try
            md_cofiguraDocArea = New d_ConfigurarDocumentoArea
            dt = md_cofiguraDocArea.ListarConfigurarDocumentoArea("CDA", 0, "", Me.ddlCodigo_are.SelectedValue, Me.ddlCodigo_doc.SelectedValue, codigo_tfu) ' busco el codigo_cda para ver si el documento está condigurado y hacer la solicitud --- codigo tfu en duro   

        Catch ex As Exception
            dt = Nothing
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
        Return dt
    End Function
    Private Sub mt_CargarSolicitudesByDocumento()
        Try
            md_documentacion = New d_Documentacion
            Dim dt As New Data.DataTable
            dt = md_documentacion.ListarSolicitaDocumentacion("SXD", 0, Me.ddlCodigo_doc.SelectedValue, "")

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
    Private Function consultarDatosReporte(ByVal tipoPrint As String, ByVal codigo_alu As Integer, ByVal codigo_cac As Integer) As Data.DataTable
        Dim dt As New Data.DataTable

        Try
            Dim objcnx As New ClsConectarDatos
            objcnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            objcnx.AbrirConexion()

            If tipoPrint = "8" Then ' si es ficha de matrícula
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
    Private Sub mt_CargarSolicitudes(ByVal operacion As String)
        Try
            md_documentacion = New d_Documentacion

            Dim dt As New Data.DataTable
            dt = md_documentacion.ListarSolicitaDocumentacion(operacion, 0, Me.ddlCodigo_doc.SelectedValue, "")

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


#End Region

    
    
   
    

    
    
   
    
    
End Class
