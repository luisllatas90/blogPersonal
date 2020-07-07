Imports System.Collections.Generic

Partial Class GestionDocumentaria_SolicitarConstancias
    Inherits System.Web.UI.Page

#Region "Variables"
    Dim md_documentacion As d_Documentacion
    Dim md_solicitaDocumento As d_SolicitaDocumentacion
    Dim md_documento As d_Documento
    Dim md_funciones As d_Funciones
    Dim md_horario As d_Horario
    Dim md_cofiguraDocArea As d_ConfigurarDocumentoArea
    Dim md_area As d_DocArea
    Dim me_cicloAcademico As e_CicloAcademico

    Dim dtDet As New Data.DataTable
    Dim dtAlumno As New Data.DataTable
    'Dim objClsGeneraDocumento As New clsGeneraDocumento

    Dim codigo_tfu As Integer
    Dim codigo_usu As Integer
    Dim tipoEstudio As String

    Dim codigo_tid As Integer
    Dim codigo_doc As Integer
    Dim codigo_are As Integer
    'Dim codigo_tfu As Integer
    Dim codigo_cda As Integer

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

        'Call mt_ShowMessage(Server.MapPath(".") & "/font/segoeui.ttf", MessageType.success)

        'Response.Write(Server.MapPath(".") & "/font/segoeui.ttf")
        'Response.Write(Request.Url)
        Response.Write(HttpContext.Current.Server.MapPath("~/"))
        'Response.Write(HttpContext.Current.Server.MapPath("../../") & "GestionDocumentaria/font/segoeui.ttf")
        'objGeneraDocumento.fuente = "E:\ProyectoCampusVirtual\campusvirtual\personal\GestionDocumentaria/font/segoeui.ttf"

        'Dim usuario As String = Session("perlogin")
        'Response.Write(usuario)
        codigo_tfu = Request.QueryString("ctf")
        'Response.Write(codigo_tfu)
        'tipoestudio = Request.QueryString("mod")
        tipoEstudio = "2"
        codigo_usu = Request.QueryString("id")

        If IsPostBack = False Then
            Call mt_CargarComboDocumento()
            Call mt_CargarComboCicloAcademico()
        End If
    End Sub
    Protected Sub ddlCodigo_doc_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlCodigo_doc.SelectedIndexChanged
        Call mt_CargarComboArea()
        Me.txtCodigo_cda.Text = ""
        Me.gvListaSolicitudes.DataSource = Nothing
        Me.gvListaSolicitudes.DataBind()
        Call mt_CargarSolicitudesByDocumento()
    End Sub
    Protected Sub lbBuscaAlu_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbBuscaAlu.Click
        Try
            ifrmReporte.Attributes("src") = "about:blank"
            'validamos los 10 caracteres
            If Len(Me.txtCodigoAlu.Text.Trim()) <> 10 Then
                Call mt_ShowMessage("El código del alumno debe contener 10 caracteres..!", MessageType.warning)
                Me.txtDescripcionAlu.Text = String.Empty
                Me.txtCodigoAlu.Focus()

                Exit Sub
            Else

                Dim dt As Data.DataTable
                dt = consultarAlumno(Me.txtCodigoAlu.Text.Trim)
                If dt.Rows.Count > 0 Then
                    With dt.Rows(0)
                        Me.txtDescripcionAlu.Text = .Item("alumno")
                        Me.TxtCodAlu.Text = .Item("codigo_Alu")
                    End With
                Else
                    Call mt_ShowMessage("No se encontró el alumno..¡", MessageType.warning)
                    Me.txtDescripcionAlu.Text = String.Empty
                End If

            End If


        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub
    Protected Sub ddlCodigo_are_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlCodigo_are.SelectedIndexChanged
        'Try
        '    Dim dt As New Data.DataTable

        '    If ddlCodigo_are.SelectedValue <> "" And ddlCodigo_doc.SelectedValue <> "" Then
        '        dt = mt_ListaCDA()
        '        If dt.Rows.Count > 0 Then
        '            With dt.Rows(0)
        '                Me.txtCodigo_cda.Text = .Item("codigo_cda")
        '            End With
        '        Else
        '            Me.txtCodigo_cda.Text = "0"
        '        End If
        '    End If



        'Catch ex As Exception
        '    Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        'End Try
    End Sub
    Protected Sub lbGeneraSolicitud_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbGeneraSolicitud.Click
        Try

            Dim lo_Validacion As Dictionary(Of String, String) = ValidarFormulario()

            If lo_Validacion.Item("rpta") = 0 Then
                Call mt_ShowMessage(lo_Validacion.Item("msg"), MessageType.warning)
                Exit Sub
            End If

            dtDet = consultarDatosReporte(Me.ddlCodigo_doc.SelectedValue, Me.TxtCodAlu.Text, Me.ddlCodigo_cac.SelectedValue)
            If dtDet.Rows.Count = 0 Then
                Call mt_ShowMessage("¡ No hay información para esta solicitud !", MessageType.warning)
                Exit Sub
            End If

            '' obtengo el codigo_cda
            codigo_cda = mt_devuelveCodigo_cda()

            'If Me.txtCodigo_cda.Text <> "" Then
            If codigo_cda <> 0 Then
                ''        
                md_documentacion.RegistraActualizarSolicitud(0, codigo_cda, codigo_usu, "6", Me.TxtCodAlu.Text, Me.ddlCodigo_cac.SelectedValue, Me.ddlCodigo_cac.SelectedItem.Text, Me.txtCodigoAlu.Text)


                Call mt_CargarSolicitudesByDocumento()
                Call limpiar()
                Call mt_ShowMessage("Se registró la solicitud con éxito", MessageType.success)

            End If


        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Protected Sub lbInforme_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbInforme.Click
        Try

            'Dim arreglo As New Dictionary(Of String, String)
            'Dim nombreArchivo As String = "InformeDeAsesor"
            'Dim codigoDatos As String = "7618"

            'arreglo.Add("nombreArchivo", nombreArchivo)
            'arreglo.Add("sesionUsuario", Session("perlogin").ToString)
            ''-----------------                
            'arreglo.Add("codigo_tes", codigoDatos)


            'clsDocumentacion.PrevioDocumentopdf("", arreglo, memory)
            'Dim bytes() As Byte = memory.ToArray
            'memory.Close()

            'Response.Clear()
            'Response.ContentType = "application/pdf"
            'Response.AddHeader("content-length", bytes.Length.ToString())
            'Response.BinaryWrite(bytes)




            Dim codigo_dot As Integer
            Dim codigo_cda As Integer = 4  ''-- Configuracion del documento
            Dim serieCorrelativoDoc As String

            ''''**** 1. GENERA CORRELATIVO DEL DOCUMNETO CONFIGURADO *******************************************************
            serieCorrelativoDoc = clsDocumentacion.ObtenerSerieCorrelativoDocPorCda(codigo_cda, Year(Now), codigo_usu)
            ''''******* GENERA DOCUMENTO PDF *****************************************************************************

            If serieCorrelativoDoc <> "" Then
                '--------necesarios
                Dim arreglo As New Dictionary(Of String, String)
                arreglo.Add("nombreArchivo", "InformeDeAsesor")
                arreglo.Add("sesionUsuario", Session("perlogin").ToString)
                '-----------------                
                arreglo.Add("codigo_tes", "7618")

                '********2. GENERA DOCUMENTO PDF **************************************************************
                codigo_dot = clsDocumentacion.generarDocumentoPdf(serieCorrelativoDoc, arreglo, 0, memory)
                'codigo_dot = clsDocumentacion.generarDocumentoPdf(serieCorrelativoDoc, arreglo)
                '**********************************************************************************************

                Dim bytes() As Byte = memory.ToArray
                memory.Close()

                Response.Clear()
                Response.ContentType = "application/pdf"
                Response.AddHeader("content-length", bytes.Length.ToString())
                Response.BinaryWrite(bytes)



            Else
                Call mt_ShowMessage("no hay nada", MessageType.error)
            End If


            '''''****************************************************************************************************************


        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try





    End Sub
    Protected Sub lbResolPrueba_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbResolPrueba.Click
        Try
            Dim codigo_sol As Integer = 0
            Dim codigo_cda As Integer = 3 ''-- Configuracion del documento
            Dim nombreArchivo As String = "ResolucionSustentacion"
            Dim codigo_tes As Integer = 6980

            ''''**** 1. SOLICITA DOCUMENTO*************************************************************************************
            codigo_sol = clsDocumentacion.GeneraSolicitudDocumento(codigo_cda, codigo_tes, nombreArchivo, codigo_usu, 6980)
            Call mt_ShowMessage(codigo_sol, MessageType.success)
            ''''****************************************************************************************************************


        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try


    End Sub
    Protected Sub lbActaPrueba_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbActaPrueba.Click
        Try

            ''Dim arreglo As New Dictionary(Of String, String)
            ''Dim nombreArchivo As String = "ActaDeSustentacion"
            ''Dim codigoDatos As String = "6980"

            ''arreglo.Add("nombreArchivo", nombreArchivo)
            ''arreglo.Add("sesionUsuario", Session("perlogin").ToString)
            ''-----------------                
            ''arreglo.Add("codigo_tes", codigoDatos)


            ''clsDocumentacion.PrevioDocumentopdf("", arreglo, memory)
            ''Dim bytes() As Byte = memory.ToArray
            ''memory.Close()

            ''Response.Clear()
            ''Response.ContentType = "application/pdf"
            ''Response.AddHeader("content-length", bytes.Length.ToString())
            ''Response.BinaryWrite(bytes)




            '''''****************************************************************************************************************


            Dim codigo_dot As Integer
            Dim codigo_cda As Integer = 5  ''-- Configuracion del documento
            Dim serieCorrelativoDoc As String

            ''''**** 1. GENERA CORRELATIVO DEL DOCUMNETO CONFIGURADO *******************************************************
            serieCorrelativoDoc = clsDocumentacion.ObtenerSerieCorrelativoDocPorCda(codigo_cda, Year(Now), codigo_usu)
            ''''******* GENERA DOCUMENTO PDF *****************************************************************************

            If serieCorrelativoDoc <> "" Then
                '--------necesarios
                Dim arreglo As New Dictionary(Of String, String)
                arreglo.Add("nombreArchivo", "ActaDeSustentacion")
                arreglo.Add("sesionUsuario", Session("perlogin").ToString)
                '-----------------                
                arreglo.Add("codigo_tes", "6980")

                '********2. GENERA DOCUMENTO PDF **************************************************************
                codigo_dot = clsDocumentacion.generarDocumentoPdf(serieCorrelativoDoc, arreglo, 0, memory)
                'codigo_dot = clsDocumentacion.generarDocumentoPdf(serieCorrelativoDoc, arreglo)
                '**********************************************************************************************

                Dim bytes() As Byte = memory.ToArray
                memory.Close()

                Response.Clear()
                Response.ContentType = "application/pdf"
                Response.AddHeader("content-length", bytes.Length.ToString())
                Response.BinaryWrite(bytes)



            Else
                Call mt_ShowMessage("no hay nada", MessageType.error)
            End If


            '''''****************************************************************************************************************
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try


    End Sub

    Protected Sub gvListaSolicitudes_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvListaSolicitudes.RowCommand
        Try
            Dim codigo_dot, codigo_alu, codigo_cac As Integer
            Dim descripcion_cac, codigoUniver_Alu As String

            Dim usuario As String = Session("perlogin")

            Dim md_clsGeneraDocumento As New clsGeneraDocumento

            Dim index As Integer = 0 : index = CInt(e.CommandArgument)
            codigo_dot = Me.gvListaSolicitudes.DataKeys(index).Values("codigo_dot")
            codigo_alu = Me.gvListaSolicitudes.DataKeys(index).Values("codigo_alu")
            codigo_cac = Me.gvListaSolicitudes.DataKeys(index).Values("codigo_cac")
            descripcion_cac = Me.gvListaSolicitudes.DataKeys(index).Values("descripcion_cac")
            codigoUniver_Alu = Me.gvListaSolicitudes.DataKeys(index).Values("codigoUniver_Alu")

            Select Case e.CommandName
                Case "ver"


                    dtDet = consultarDatosReporte(Me.ddlCodigo_doc.SelectedValue, codigo_alu, codigo_cac)

                    If dtDet.Rows.Count > 0 Then

                        dtAlumno = consultarAlumno(codigoUniver_Alu)
                    End If
                    md_clsGeneraDocumento.fuente = Server.MapPath(".") & "/font/segoeui.ttf"


                    ''si tipo de impresión Fich de MAtrícula
                    If ddlCodigo_doc.SelectedValue = "1" Then

                        'md_clsGeneraDocumento.EmiteFichaMatricula("", memory, dtDet, dtAlumno, True, sourceIcon, sourceSello, descripcion_cac)
                        Dim bytes() As Byte = memory.ToArray
                        memory.Close()

                        Response.Clear()
                        Response.ContentType = "application/pdf"
                        Response.AddHeader("content-length", bytes.Length.ToString())
                        Response.BinaryWrite(bytes)

                    ElseIf ddlCodigo_doc.SelectedValue = "2" Then

                        'md_clsGeneraDocumento.EmiteFichaNotas("", memory, dtDet, dtAlumno, True, sourceIcon, sourceSello, descripcion_cac)
                        Dim bytes() As Byte = memory.ToArray
                        memory.Close()

                        Response.Clear()
                        Response.ContentType = "application/pdf"
                        Response.AddHeader("content-length", bytes.Length.ToString())
                        Response.BinaryWrite(bytes)
                    End If

                Case "descargar"
                    bytes = md_clsGeneraDocumento.DescargarArchivo(codigo_dot, 30, "3N23G777FS", usuario)
                    'mt_DescargarArchivo(codigo_dot, 30, "3N23G777FS")ñ

                    Response.Clear()
                    Response.Buffer = False
                    Response.Charset = ""
                    Response.Cache.SetCacheability(HttpCacheability.NoCache)
                    Response.ContentType = "application/pdf"
                    Response.AddHeader("content-disposition", "attachment;filename=" & "FichaMatricula.pdf".ToString.Replace(",", ""))
                    Response.AppendHeader("Content-Length", bytes.Length.ToString())
                    Response.BinaryWrite(bytes)
                    Response.End()

                    'mt_ShowMessage("¡ Se ha descargado el silabo correctamente !", MessageType.success)
            End Select

        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub



#End Region

#Region "Procedimientos Funciones"

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
            Call md_funciones.CargarCombo(Me.ddlCodigo_doc, dt, "codigo_doc", "descripcion_doc", True, "[-- SELECCIONE --]", "")

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

            Call md_funciones.CargarCombo(Me.ddlCodigo_cac, dt, "codigo_cac", "descripcion_cac", True, "[-- SELECCIONE --]", "")

        Catch ex As Exception
            Throw ex
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub
    Private Function consultarAlumno(ByVal codigoUniver_Alu As String) As Data.DataTable
        Dim dt As New Data.DataTable
        Try
            Dim objcnx As New ClsConectarDatos
            objcnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            objcnx.AbrirConexion()

            dt = objcnx.TraerDataTable("ConsultarAlumno", "CU", codigoUniver_Alu)
            objcnx.CerrarConexion()
            If dt.Rows.Count = 0 Then
                Call mt_ShowMessage("No se encontró el alumno..¡", MessageType.warning)
                Me.txtDescripcionAlu.Text = String.Empty
            End If

        Catch ex As Exception
            dt = Nothing
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
        Return dt
    End Function
    Private Function mt_ListaCDA() As Data.DataTable
        Dim dt As New Data.DataTable
        Try
            md_cofiguraDocArea = New d_ConfigurarDocumentoArea
            dt = md_cofiguraDocArea.ListarConfigurarDocumentoArea("CDA", 0, "", Me.ddlCodigo_are.SelectedValue, Me.ddlCodigo_doc.SelectedValue, codigo_tfu, 0) ' busco el codigo_cda para ver si el documento está condigurado y hacer la solicitud --- codigo tfu en duro   

        Catch ex As Exception
            dt = Nothing
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
        Return dt
    End Function
    Private Function consultarDatosReporte(ByVal tipoPrint As String, ByVal codigo_alu As Integer, ByVal codigo_cac As Integer) As Data.DataTable
        Dim dt As New Data.DataTable

        Try
            Dim objcnx As New ClsConectarDatos
            objcnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            objcnx.AbrirConexion()

            If tipoPrint = "1" Then ' si es ficha de matrícula
                dt = objcnx.TraerDataTable("ConsultarNotas", "BA", codigo_alu, codigo_cac, "0")
            Else : tipoPrint = "2" 'ficha de notas
                dt = objcnx.TraerDataTable("ACAD_FichaNotas", codigo_alu, codigo_cac)
            End If

            objcnx.CerrarConexion()


        Catch ex As Exception
            dt = Nothing
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try

        Return dt

    End Function
    Private Sub mt_CargarSolicitudesByDocumento()
        md_documentacion = New d_Documentacion
        Dim dt As New Data.DataTable
        Try
            If Me.ddlCodigo_doc.SelectedValue <> "" Then

                dt = md_solicitaDocumento.ListarSolicitaDocumentacion("SXD", 0, Me.ddlCodigo_doc.SelectedValue, "")
                If dt.Rows.Count > 0 Then
                    Me.gvListaSolicitudes.DataSource = dt
                    Me.gvListaSolicitudes.DataBind()
                    'Else
                    '    Call mt_ShowMessage("No se encontraron solic emitidos", MessageType.warning)
                Else
                    Me.gvListaSolicitudes.DataSource = Nothing
                    Me.gvListaSolicitudes.DataBind()
                End If
            Else
                Me.gvListaSolicitudes.DataSource = Nothing
                Me.gvListaSolicitudes.DataBind()
            End If
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub
    Private Function ValidarFormulario() As Dictionary(Of String, String)

        Dim lo_Validacion As New Dictionary(Of String, String)
        lo_Validacion.Add("rpta", 1)
        lo_Validacion.Add("msg", "")
        lo_Validacion.Add("control", "")

        If Me.ddlCodigo_doc.SelectedValue = "" Then
            lo_Validacion.Item("rpta") = 0
            lo_Validacion.Item("msg") = "¡ Seleccione un docuumento !"

        ElseIf Me.ddlCodigo_cac.SelectedValue = "" Then
            lo_Validacion.Item("rpta") = 0
            lo_Validacion.Item("msg") = "¡ Seleccione un ciclo !"

        ElseIf Me.ddlCodigo_are.SelectedValue = "" Then
            lo_Validacion.Item("rpta") = 0
            lo_Validacion.Item("msg") = "¡ Seleccione un area !"

        ElseIf Me.txtDescripcionAlu.Text = "" Then
            lo_Validacion.Item("rpta") = 0
            lo_Validacion.Item("msg") = "¡ Ingrese el alumno !"

        ElseIf Me.txtCodigoAlu.Text = "" Then
            lo_Validacion.Item("rpta") = 0
            lo_Validacion.Item("msg") = "¡ Ingrese el alumno !"
        End If

        Return lo_Validacion
    End Function
    Private Sub limpiar()
        Me.ddlCodigo_cac.SelectedValue = ""
        Me.txtCodigoAlu.Text = ""
        Me.txtDescripcionAlu.Text = ""
        Me.TxtCodAlu.Text = ""
    End Sub
    Private Function mt_devuelveCodigo_cda() As Integer

        If Me.ddlCodigo_doc.SelectedValue = "1" Then ''--ficha de matrícula
            codigo_tid = 1
            codigo_doc = Me.ddlCodigo_doc.SelectedValue
            codigo_are = 1
            codigo_tfu = 181
        ElseIf Me.ddlCodigo_doc.SelectedValue = "2" Then
            codigo_tid = 1
            codigo_doc = Me.ddlCodigo_doc.SelectedValue
            codigo_are = 1
            codigo_tfu = 181
        End If

        'instancio mi clase
        md_documentacion = New d_Documentacion
        codigo_cda = md_documentacion.BuscaDocumentoConfigurado(codigo_tid, codigo_doc, codigo_are, codigo_tfu)

    End Function

#End Region









   
   
End Class
