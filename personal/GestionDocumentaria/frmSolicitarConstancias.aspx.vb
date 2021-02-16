Imports System.Collections.Generic
Imports System.Net
Imports System.IO
Imports System.Web.Script.Serialization


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
        'Response.Write(HttpContext.Current.Server.MapPath("~/"))
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


            Dim codigo_sol As Integer = 0  '--- codigo de solicitud que devuelve
            Dim abreviatura_tid As String = "RESO" '---fijo  --- 
            Dim abreviatura_doc As String = "SUST" '---fijo
            Dim abreviatura_are As String = "USAT" '---fijo  
            Dim nombreArchivo As String = "ResolucionSustentacion"
            Dim codigo_fac As Integer = "19"      '--- 21 para ubicar el secretario de facultad de posgrado 
            Dim codigo_dta As Integer = 0   '--codigo de trámite / para el campo referencia01 de la tabla doc_solicitaDocumentacion
            Dim codigDatos As Integer = 8012  '-- en este caso codigo_tes

            ''''**** 1. SOLICITA DOCUMENTO*************************************************************************************
            codigo_sol = clsDocumentacion.GeneraSolicitudDocumentoXAbreviaturas(codigo_dta, abreviatura_doc, abreviatura_tid, _
                                                                                abreviatura_are, codigDatos, nombreArchivo, codigo_usu, codigo_fac)
            Call mt_ShowMessage(codigo_sol, MessageType.success)
            ''''****************************************************************************************************************

            'Dim lo_Validacion As Dictionary(Of String, String) = ValidarFormulario()

            'If lo_Validacion.Item("rpta") = 0 Then
            '    Call mt_ShowMessage(lo_Validacion.Item("msg"), MessageType.warning)
            '    Exit Sub
            'End If

            'dtDet = consultarDatosReporte(Me.ddlCodigo_doc.SelectedValue, Me.TxtCodAlu.Text, Me.ddlCodigo_cac.SelectedValue)
            'If dtDet.Rows.Count = 0 Then
            '    Call mt_ShowMessage("¡ No hay información para esta solicitud !", MessageType.warning)
            '    Exit Sub
            'End If

            ' '' obtengo el codigo_cda
            'codigo_cda = mt_devuelveCodigo_cda()

            ''If Me.txtCodigo_cda.Text <> "" Then
            'If codigo_cda <> 0 Then
            '    ''        
            '    md_documentacion.RegistraActualizarSolicitud(0, codigo_cda, codigo_usu, "6", Me.TxtCodAlu.Text, Me.ddlCodigo_cac.SelectedValue, Me.ddlCodigo_cac.SelectedItem.Text, Me.txtCodigoAlu.Text)


            '    Call mt_CargarSolicitudesByDocumento()
            '    Call limpiar()
            '    Call mt_ShowMessage("Se registró la solicitud con éxito", MessageType.success)

            'End If


        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Protected Sub lbInforme_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbInforme.Click
        Try

            '*************************** PREVIO *************************************************************
            'Dim arreglo As New Dictionary(Of String, String)
            'Dim nombreArchivo As String = "InformeDeAsesor"
            'Dim codigoDatos As String = "8603" ''--- codigo_tes o codigo tesis

            'arreglo.Add("nombreArchivo", nombreArchivo)
            'arreglo.Add("sesionUsuario", Session("perlogin").ToString) ' login de usuario ejemplo: usat\olluen
            'arreglo.Add("codigo_tes", codigoDatos)
            ''--- se adiciona  para el tipo de estudio post grado
            'arreglo.Add("tipoEstudio", "2") ''--- codigo_test de la tabla tipo estudio no el que viene de la url si no el del alumno 

            ''-------------------- se envian los parametros para generar el documeno---------------------------------------------------------
            ''clsDocumentacion.PrevioDocumentopdf("", arreglo)
            ''-----------------------------------------------------------------------------------------------------------------------------

            'clsDocumentacion.PrevioDocumentopdf("", arreglo, memory)
            'Dim bytes() As Byte = memory.ToArray
            'memory.Close()

            'Response.Clear()
            'Response.ContentType = "application/pdf"
            'Response.AddHeader("content-length", bytes.Length.ToString())
            'Response.BinaryWrite(bytes)

            ''***-------------------------------------------------------------------------------------------------------------------------------

            'Dim codigo_dot As Integer
            'Dim codigo_cda As Integer = 4  ''-- Configuracion del documento


            ''''**** 1. GENERA CORRELATIVO DEL DOCUMNETO CONFIGURADO *******************************************************
            'serieCorrelativoDoc = clsDocumentacion.ObtenerSerieCorrelativoDocPorAbreviatura(codigo_cda, Year(Now), codigo_usu)
            ''''******* GENERA DOCUMENTO PDF *****************************************************************************

            'If serieCorrelativoDoc <> "" Then
            '    '--------necesarios
            '    Dim arreglo As New Dictionary(Of String, String)
            '    arreglo.Add("nombreArchivo", "InformeDeAsesor")
            '    arreglo.Add("sesionUsuario", Session("perlogin").ToString)
            '    '-----------------                
            '    arreglo.Add("codigo_tes", "8603")

            '    '********2. GENERA DOCUMENTO PDF **************************************************************
            '    codigo_dot = clsDocumentacion.generarDocumentoPdf(serieCorrelativoDoc, arreglo, 0, memory)
            '    'codigo_dot = clsDocumentacion.generarDocumentoPdf(serieCorrelativoDoc, arreglo)
            '    '**********************************************************************************************

            '    Dim bytes() As Byte = memory.ToArray
            '    memory.Close()

            '    Response.Clear()
            '    Response.ContentType = "application/pdf"
            '    Response.AddHeader("content-length", bytes.Length.ToString())
            '    Response.BinaryWrite(bytes)



            'Else
            'Call mt_ShowMessage("no hay nada", MessageType.error)
            'End If







            '''''************************** X ABREVIATURAS *****************************************************************************************************************


            ' '' ------------------------------ nuevo por abreviaturas
            Dim serieCorrelativoDoc As String
            Dim codigo_dot As Integer
            Dim abreviatura_tid As String = "INFO" '---fijo 
            Dim abreviatura_doc As String = "IASE" '---fijo
            Dim abreviatura_are As String = "USAT" '---fijo / para postgrado
            '---Dim abreviatura_are As String = "USAT" '---fijo / para pregrado

            ''-------------------------------genera el correlativo
            serieCorrelativoDoc = clsDocumentacion.ObtenerSerieCorrelativoDocPorAbreviatura(abreviatura_tid, abreviatura_doc, abreviatura_are, Year(Now), codigo_usu)

            ''-------------------------------genera el documento

            Dim arreglo As New Dictionary(Of String, String)
            Dim nombreArchivo As String = "InformeDeAsesor"
            Dim codigoDatos As String = "8603" ''--- codigo_tes o codigo tesis

            arreglo.Add("nombreArchivo", nombreArchivo)
            arreglo.Add("sesionUsuario", Session("perlogin").ToString) ' login de usuario ejemplo: usat\olluen
            arreglo.Add("codigo_tes", codigoDatos)
            '--- se adiciona  para el tipo de estudio post grado
            arreglo.Add("tipoEstudio", "2") ''--- codigo_test de la tabla tipo estudio no el que viene de la url si no el del alumno 

            '-------------------- se envian los parametros para generar el documeno---------------------------------------------------------
            codigo_dot = clsDocumentacion.generarDocumentoPdf(serieCorrelativoDoc, arreglo)
            '-----------------------------------------------------------------------------------------------------------------------------



            '''''*******************************************************************************************************************************************
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try





    End Sub
    Protected Sub lbResolPrueba_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbResolPrueba.Click

        '''''' Resolución de sustentación --------------------------------------------------------------------------------------------------
        'Dim codigo_sol As Integer = 0
        'Dim codigo_cda As Integer = 3 ''-- Configuracion del documento
        'Dim nombreArchivo As String = "ResolucionSustentacion"
        'Dim codigo_tes As Integer = 8603
        'Dim codigo_fac As Integer = 20
        '''''**** 1. SOLICITA DOCUMENTO*************************************************************************************
        'codigo_sol = clsDocumentacion.GeneraSolicitudDocumento(codigo_cda, codigo_tes, nombreArchivo, codigo_usu, codigo_fac)
        'Call mt_ShowMessage(codigo_sol, MessageType.success)


        ''''****************************************************************************************************************
        '''' fin Resolución de sustentación --------------------------------------------------------------------------------------------------
        '' ----------------- Resolucion de sustentación 
        Try

            Dim codigo_sol As Integer = 0  '--- codigo de solicitud que devuelve
            Dim abreviatura_tid As String = "RESO" '---fijo  --- 
            Dim abreviatura_doc As String = "SUST" '---fijo
            Dim abreviatura_are As String = "PGRA" '---fijo  --- PGRA para postgrado 
            Dim nombreArchivo As String = "ResolucionSustentacion"
            Dim codigo_fac As Integer = "21"      '--- 21 para ubicar el secretario de facultad de posgrado 
            Dim codigo_dta As Integer = 65583   '--codigo de trámite / para el campo referencia01 de la tabla doc_solicitaDocumentacion
            Dim codigDatos As Integer = 8603  '-- en este caso codigo_tes

            ''''**** 1. SOLICITA DOCUMENTO*************************************************************************************
            codigo_sol = clsDocumentacion.GeneraSolicitudDocumentoXAbreviaturas(codigo_dta, abreviatura_doc, abreviatura_tid, _
                                                                                abreviatura_are, codigDatos, nombreArchivo, codigo_usu, codigo_fac)
            Call mt_ShowMessage(codigo_sol, MessageType.success)
            ''''****************************************************************************************************************
            '''' fin Resolución de otorgamiento de grado --------------------------------------------------------------------------------------------------


        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try


        '*******************aca es para ver un previo de la resolucion de sustentacion ********************************************************************************
        'Try
        '    Dim arreglo As New Dictionary(Of String, String)
        '    Dim nombreArchivo As String = "ResolucionSustentacion"
        '    Dim codigoDatos As String = "8012" ''6162 '' 7572



        '    arreglo.Add("nombreArchivo", nombreArchivo)
        '    arreglo.Add("sesionUsuario", Session("perlogin").ToString)
        '    arreglo.Add("codigo_datos", codigoDatos) '---- este es obligatorio tmb  
        '    '----------------------------------------------------------------------------- de aqui para adelante se pueden ir agregando mas campos segun lo requiera el documento               
        '    arreglo.Add("codigo_fac", "19")
        '    arreglo.Add("codigo_sol", "")

        '    clsDocumentacion.PrevioDocumentopdf("", arreglo, memory)
        '    Dim bytes() As Byte = memory.ToArray
        '    memory.Close()

        '    Response.Clear()
        '    Response.ContentType = "application/pdf"
        '    Response.AddHeader("content-length", bytes.Length.ToString())
        '    Response.BinaryWrite(bytes)
        'Catch ex As Exception
        '    Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        'End Try
       

        '***************************************************



        '*******************aca es para ver un previo de la resolucion de sustentacion otorgaGrado********************************************************************************

        'Dim arreglo As New Dictionary(Of String, String)
        'Dim nombreArchivo As String = "ResolucionOtorgaGrado"
        'Dim codigoDatos As String = "50693" ''6162 '' 7572



        'arreglo.Add("nombreArchivo", nombreArchivo)
        'arreglo.Add("sesionUsuario", Session("perlogin").ToString)
        'arreglo.Add("codigo_datos", codigoDatos) '---- este es obligatorio tmb  
        ''----------------------------------------------------------------------------- de aqui para adelante se pueden ir agregando mas campos segun lo requiera el documento               
        'arreglo.Add("codigo_fac", "20")
        'arreglo.Add("codigo_sol", "")




        'clsDocumentacion.PrevioDocumentopdf("", arreglo, memory)
        'Dim bytes() As Byte = memory.ToArray
        'memory.Close()

        'Response.Clear()
        'Response.ContentType = "application/pdf"
        'Response.AddHeader("content-length", bytes.Length.ToString())
        'Response.BinaryWrite(bytes)

        '***********************************************************************************************************************************************************

        '*******************aca es para ver un previo de la resolucion de sustentacion otorgaTirulo********************************************************************************

        'Dim arreglo As New Dictionary(Of String, String)
        'Dim nombreArchivo As String = "ResolucionOtorgaTitulo"
        'Dim codigoDatos As String = "50763" ''6162 '' 7572



        'arreglo.Add("nombreArchivo", nombreArchivo)
        'arreglo.Add("sesionUsuario", Session("perlogin").ToString)
        'arreglo.Add("codigo_datos", codigoDatos) '---- este es obligatorio tmb  
        ''----------------------------------------------------------------------------- de aqui para adelante se pueden ir agregando mas campos segun lo requiera el documento               
        'arreglo.Add("codigo_fac", "20")
        'arreglo.Add("codigo_sol", "")




        'clsDocumentacion.PrevioDocumentopdf("", arreglo, memory)
        'Dim bytes() As Byte = memory.ToArray
        'memory.Close()

        'Response.Clear()
        'Response.ContentType = "application/pdf"
        'Response.AddHeader("content-length", bytes.Length.ToString())
        'Response.BinaryWrite(bytes)

        '******************************************************************************


        ' *********************************************************** este es para enviar correo

        'Try

        '    Dim codigoEnvio As Integer
        '    Dim ultimaFila As Integer
        '    Dim alumnos As String = ""
        '    Dim emailAlumno As String = ""
        '    '---------------------
        '    Dim md_documentacion As New d_Documentacion

        '    Dim dtCodigoDta As New Data.DataTable
        '    dtCodigoDta = md_documentacion.ListarCodigoDta("1641")
        '    'cambio 01
        '    Dim codigo_tes As Integer
        '    Dim codigo_pst As Integer
        '    If dtCodigoDta.Rows.Count > 0 Then

        '        codigo_tes = CInt(dtCodigoDta.Rows(0).Item("codigo_tes"))
        '        codigo_pst = CInt(dtCodigoDta.Rows(0).Item("codigo_pst"))

        '    End If

        '    Dim rptaEmail As Boolean = False
        '    'llamo esta clase por que ahi se genera la resolucion de sustentacion y traigo los datos para el envio de correos 
        '    Dim objGeneraDocumento As New clsGeneraDocumento
        '    Dim dtEmail As New Data.DataTable
        '    dtEmail = objGeneraDocumento.fc_DatosResolSustentacion(codigo_tes)

        '    If dtEmail.Rows.Count > 0 Then

        '        codigoEnvio = ClsComunicacionInstitucional.ObtenerCodigoEnvio(codigo_usu, codigo_tfu, 77)

        '        For i As Integer = 0 To dtEmail.Rows.Count - 1

        '            ultimaFila = dtEmail.Rows.Count - 1

        '            '---------------------- coma
        '            If i = ultimaFila Then
        '                alumnos = alumnos + dtEmail.Rows(i).Item("alumno")
        '                emailAlumno = emailAlumno + dtEmail.Rows(i).Item("eMail_Alu")
        '            Else
        '                alumnos = alumnos + dtEmail.Rows(i).Item("alumno") & "<br>"
        '                If ConfigurationManager.AppSettings("CorreoUsatActivo") = "1" Then
        '                    emailAlumno = emailAlumno + dtEmail.Rows(i).Item("eMail_Alu") & ";"
        '                Else
        '                    emailAlumno = "hcano@usat.edu.pe"
        '                End If
        '            End If

        '        Next
        '        '*************************************
        '        If dtEmail.Rows(0).Item("codigo_test") = "5" Then
        '            ClsComunicacionInstitucional.EnviarNotificacionEmail(codigoEnvio, "SUST", "CJYE", "1", codigo_usu, "codigo_alu", dtEmail.Rows(0).Item("codigo_Alu"), "77", emailAlumno, "", "", "", _
        '                                             "Posgrado", dtEmail.Rows(0).Item("titulo_tes"), alumnos, dtEmail.Rows(0).Item("fechaprogramacion"), dtEmail.Rows(0).Item("horaprogramacion"), dtEmail.Rows(0).Item("ambiente"), dtEmail.Rows(0).Item("presidente"), dtEmail.Rows(0).Item("secretario"), dtEmail.Rows(0).Item("vocal"))

        '            If ConfigurationManager.AppSettings("CorreoUsatActivo") = "1" Then

        '                ClsComunicacionInstitucional.EnviarNotificacionEmail(codigoEnvio, "SUST", "CJYE", "1", codigo_usu, "codigo_alu", dtEmail.Rows(0).Item("codigo_Alu"), "77", "todousat@usat.edu.pe", "", "", "", _
        '                       "Posgrado", dtEmail.Rows(0).Item("titulo_tes"), alumnos, dtEmail.Rows(0).Item("fechaprogramacion"), dtEmail.Rows(0).Item("horaprogramacion"), dtEmail.Rows(0).Item("ambiente"), dtEmail.Rows(0).Item("presidente"), dtEmail.Rows(0).Item("secretario"), dtEmail.Rows(0).Item("vocal"))

        '            End If
        '        Else
        '            ClsComunicacionInstitucional.EnviarNotificacionEmail(codigoEnvio, "SUST", "CJYE", "1", codigo_usu, "codigo_alu", dtEmail.Rows(0).Item("codigo_Alu"), "77", emailAlumno, "", "", "", _
        '                                             dtEmail.Rows(0).Item("nombreOficial_cpf"), dtEmail.Rows(0).Item("titulo_tes"), alumnos, dtEmail.Rows(0).Item("fechaprogramacion"), dtEmail.Rows(0).Item("horaprogramacion"), dtEmail.Rows(0).Item("ambiente"), dtEmail.Rows(0).Item("presidente"), dtEmail.Rows(0).Item("secretario"), dtEmail.Rows(0).Item("vocal"))

        '            If ConfigurationManager.AppSettings("CorreoUsatActivo") = "1" Then

        '                ClsComunicacionInstitucional.EnviarNotificacionEmail(codigoEnvio, "SUST", "CJYE", "1", codigo_usu, "codigo_alu", dtEmail.Rows(0).Item("codigo_Alu"), "77", "todousat@usat.edu.pe", "", "", "", _
        '                       dtEmail.Rows(0).Item("nombreOficial_cpf"), dtEmail.Rows(0).Item("titulo_tes"), alumnos, dtEmail.Rows(0).Item("fechaprogramacion"), dtEmail.Rows(0).Item("horaprogramacion"), dtEmail.Rows(0).Item("ambiente"), dtEmail.Rows(0).Item("presidente"), dtEmail.Rows(0).Item("secretario"), dtEmail.Rows(0).Item("vocal"))

        '            End If
        '        End If


        '        md_documentacion.ActualizaEnviaCorreoProgramacion(codigo_pst)


        '    End If

        'Catch ex As Exception
        '    Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        'End Try

        '**************************************************************************************************************************************************************************************




        '' ----------------- Resolucion de otorgamiento de grado
        'Try
        '    Dim codigo_sol As Integer = 0  '--- codigo de solicitud que devuelve
        '    Dim abreviatura_doc As String = "GRAD" '---fijo
        '    Dim abreviatura_tid As String = "RESO" '---fijo
        '    Dim abreviatura_are As String = "USAT" '---fijo
        '    Dim nombreArchivo As String = "ResolucionOtorgaGrado"
        '    Dim codigo_egr As Integer = 44223   '--codigo alumno
        '    Dim codigo_fac As Integer = 19      '--codigo facultad    
        '    Dim codigo_dta As Integer = 65583   '--codigo de trámite

        '    ''''**** 1. SOLICITA DOCUMENTO*************************************************************************************
        '    codigo_sol = clsDocumentacion.GeneraSolicitudDocumentoXAbreviaturas(codigo_dta, abreviatura_doc, abreviatura_tid, abreviatura_are, codigo_egr, nombreArchivo, codigo_usu, codigo_fac)
        '    Call mt_ShowMessage(codigo_sol, MessageType.success)
        '    ''''****************************************************************************************************************
        '''' fin Resolución de otorgamiento de grado --------------------------------------------------------------------------------------------------
        '' ----------------- Resolucion de otorgamiento de grado
        ''Try
        ''    Dim codigo_sol As Integer = 0  '--- codigo de solicitud que devuelve
        ''    Dim abreviatura_doc As String = "TIT" '---fijo
        ''    Dim abreviatura_tid As String = "RESO" '---fijo
        ''    Dim abreviatura_are As String = "USAT" '---fijo
        ''    Dim nombreArchivo As String = "ResolucionOtorgaTitulo"
        ''    Dim codigo_egr As Integer = 44223   '--codigo alumno
        ''    Dim codigo_fac As Integer = 19      '--codigo facultad    
        ''    Dim codigo_dta As Integer = 65583   '--codigo de trámite

        ''    ''''**** 1. SOLICITA DOCUMENTO*************************************************************************************
        ''    codigo_sol = clsDocumentacion.GeneraSolicitudDocumentoXAbreviaturas(codigo_dta, abreviatura_doc, abreviatura_tid, abreviatura_are, codigo_egr, nombreArchivo, codigo_usu, codigo_fac)
        ''    Call mt_ShowMessage(codigo_sol, MessageType.success)
        ''    ''''****************************************************************************************************************
        ''    '''' fin Resolución de otorgamiento de grado --------------------------------------------------------------------------------------------------

        ''Catch ex As Exception
        ''    Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        ''End Try


    End Sub
    Protected Sub lbActaPrueba_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbActaPrueba.Click
        Try
            'Dim idArchivoCompartido As Integer = "13222456"
            'Dim txtOriginal As String = "Su trámite ha sido realizado, puede descargar su documento <span onClick=""downloadDoc(" & idArchivoCompartido & ");"" style=""cursor: pointer; color: blue;"">aquí</span>"

            'Me.txtCodigoAlu.Text = txtOriginal
            'Call mt_ShowMessage(txtOriginal, MessageType.success)


            Dim arreglo As New Dictionary(Of String, String)
            Dim nombreArchivo As String = "ActaDeSustentacion"
            Dim codigoDatos As String = "5524" '-- 6162, 7572

            arreglo.Add("nombreArchivo", nombreArchivo)
            arreglo.Add("sesionUsuario", Session("perlogin").ToString)
            '-----------------                
            arreglo.Add("codigo_tes", codigoDatos)
            '--- se adiciona  para el tipo de estudio post grado
            arreglo.Add("tipoEstudio", "2") ''--- codigo_test de la tabla tipo estudio no el que viene de la url si no el del alumno 


            clsDocumentacion.PrevioDocumentopdf("", arreglo, memory)
            Dim bytes() As Byte = memory.ToArray
            memory.Close()

            Response.Clear()
            Response.ContentType = "application/pdf"
            Response.AddHeader("content-length", bytes.Length.ToString())
            Response.BinaryWrite(bytes)






            '''''****************************************************************************************************************

            ''Dim arreglo As New Dictionary(Of String, String)
            'Dim codigo_dot As Integer
            'Dim codigo_cda As Integer = 5  ''-- Configuracion del documento
            'Dim serieCorrelativoDoc As String

            '''''**** 1. GENERA CORRELATIVO DEL DOCUMNETO CONFIGURADO *******************************************************
            'serieCorrelativoDoc = clsDocumentacion.ObtenerSerieCorrelativoDocPorCda(codigo_cda, Year(Now), codigo_usu)
            '''''******* GENERA DOCUMENTO PDF *****************************************************************************

            'If serieCorrelativoDoc <> "" Then
            '    '--------necesarios
            '    Dim arreglo As New Dictionary(Of String, String)
            '    arreglo.Add("nombreArchivo", "ActaDeSustentacion")
            '    arreglo.Add("sesionUsuario", Session("perlogin").ToString)
            '    '-----------------                
            '    arreglo.Add("codigo_tes", "6980")
            '    '--- se adiciona  para el tipo de estudio post grado
            '    arreglo.Add("tipoEstudio", "5") ''--- codigo_test de la tabla tipo estudio no el que viene de la url si no el del alumno 

            '    '********2. GENERA DOCUMENTO PDF **************************************************************
            '   codigo_dot = clsDocumentacion.generarDocumentoPdf(serieCorrelativoDoc, arreglo)

            '    '**********************************************************************************************


            'Else
            '    Call mt_ShowMessage("correlativo no generado", MessageType.error)
            'End If


            '''''**************************POR ABREVIATURA**************************************************************************************



            '' '' ------------------------------ nuevo por abreviaturas
            'Dim serieCorrelativoDoc As String
            'Dim codigo_dot As Integer
            'Dim abreviatura_tid As String = "ACT" '---fijo 
            'Dim abreviatura_doc As String = "ACST" '---fijo
            'Dim abreviatura_are As String = "PGRA" '---fijo / para postgrado
            ''---Dim abreviatura_are As String = "USAT" '---fijo / para pregrado

            ' ''-------------------------------genera el correlativo
            'serieCorrelativoDoc = clsDocumentacion.ObtenerSerieCorrelativoDocPorAbreviatura(abreviatura_tid, abreviatura_doc, abreviatura_are, Year(Now), codigo_usu)

            ' ''-------------------------------genera el documento
            '''''******* GENERA DOCUMENTO PDF *****************************************************************************

            'If serieCorrelativoDoc <> "" Then
            '    '--------necesarios
            '    Dim arreglo As New Dictionary(Of String, String)
            '    arreglo.Add("nombreArchivo", "ActaDeSustentacion")
            '    arreglo.Add("sesionUsuario", Session("perlogin").ToString)
            '    '-----------------                
            '    arreglo.Add("codigo_tes", "6980") '' codigo_tes
            '    arreglo.Add("tipoEstudio", "5") ''--- codigo_test de la tabla tipo estudio no el que viene de la url si no el del alumno 

            '    '********2. GENERA DOCUMENTO PDF **************************************************************
            '    codigo_dot = clsDocumentacion.generarDocumentoPdf(serieCorrelativoDoc, arreglo)
            '    'codigo_dot = clsDocumentacion.generarDocumentoPdf(serieCorrelativoDoc, arreglo)
            '    '**********************************************************************************************


            'Else
            '    Call mt_ShowMessage("correlativo no generado", MessageType.error)
            'End If

        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try


    End Sub



    Protected Sub lbAutorRepos_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbAutorRepos.Click
        Try

            Dim arreglo As New Dictionary(Of String, String)
            Dim nombreArchivo As String = "AutorPublTesis"
            Dim codigoDatos As String = "8603"

            arreglo.Add("nombreArchivo", nombreArchivo)
            arreglo.Add("sesionUsuario", Session("perlogin").ToString)
            '-----------------                
            arreglo.Add("codigo_tes", codigoDatos)
            '--- se adiciona  para el tipo de estudio post grado
            arreglo.Add("tipoEstudio", "5") ''--- codigo_test de la tabla tipo estudio no el que viene de la url si no el del alumno 

            clsDocumentacion.PrevioDocumentopdf("", arreglo, memory)
            Dim bytes() As Byte = memory.ToArray
            memory.Close()

            Response.Clear()
            Response.ContentType = "application/pdf"
            Response.AddHeader("content-length", bytes.Length.ToString())
            Response.BinaryWrite(bytes)

            '''''****************************************************************************************************************


            'Dim codigo_dot As Integer
            'Dim codigo_cda As Integer = 6  ''-- Configuracion del documento
            'Dim serieCorrelativoDoc As String

            '''''**** 1. GENERA CORRELATIVO DL DOCUMNETO CONFIGURADO *******************************************************
            'serieCorrelativoDoc = clsDocumentacion.ObtenerSerieCorrelativoDocPorCda(codigo_cda, Year(Now), codigo_usu)
            '''''******* GENERA DOCUMENTO PDF *****************************************************************************

            'If serieCorrelativoDoc <> "" Then
            '    '--------necesarios
            '    Dim arreglo As New Dictionary(Of String, String)
            '    arreglo.Add("nombreArchivo", "AutorPublTesis")
            '    arreglo.Add("sesionUsuario", Session("perlogin").ToString)
            '    '-----------------                
            '    arreglo.Add("codigo_tes", "7435")
            '    '--- se adiciona  para el tipo de estudio post grado
            '    arreglo.Add("tipoEstudio", "5") ''--- codigo_test de la tabla tipo estudio no el que viene de la url si no el del alumno 

            '    '********2. GENERA DOCUMENTO PDF **************************************************************
            '    codigo_dot = clsDocumentacion.generarDocumentoPdf(serieCorrelativoDoc, arreglo)

            '    '**********************************************************************************************


            '    Call mt_ShowMessage(codigo_dot, MessageType.success)


            'Else
            '    Call mt_ShowMessage("no se generó correlativo", MessageType.error)
            'End If


            '''''****************************************************************************************************************







        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub
    Protected Sub lbActaAprob_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbActaAprob.Click
        Try
            'Dim arreglo As New Dictionary(Of String, String)
            'Dim nombreArchivo As String = "ActaTrabajoBachiller"
            'Dim codigoDatos As String = "1"

            'arreglo.Add("nombreArchivo", nombreArchivo)
            'arreglo.Add("sesionUsuario", Session("perlogin").ToString)
            ''-----------------                
            'arreglo.Add("codigo_tba", codigoDatos)


            'clsDocumentacion.PrevioDocumentopdf("ACT-AATI-0000-2020-USAT", arreglo, memory)
            'Dim bytes() As Byte = memory.ToArray
            'memory.Close()

            'Response.Clear()
            'Response.ContentType = "application/pdf"
            'Response.AddHeader("content-length", bytes.Length.ToString())
            'Response.BinaryWrite(bytes)

            ''''****************************************************************************************************************


            Dim codigo_dot As Integer
            Dim codigo_cda As Integer = 7  ''-- Configuracion del documento
            Dim serieCorrelativoDoc As String

            ''''**** 1. GENERA CORRELATIVO DEL DOCUMNETO CONFIGURADO *******************************************************
            serieCorrelativoDoc = clsDocumentacion.ObtenerSerieCorrelativoDocPorCda(codigo_cda, Year(Now), codigo_usu)
            serieCorrelativoDoc = clsDocumentacion.ObtenerSerieCorrelativoDocPorAbreviatura("RESO", "SUST", "USAT", Year(Now), codigo_usu)
            ''''******* GENERA DOCUMENTO PDF *****************************************************************************

            If serieCorrelativoDoc <> "" Then
                '--------necesarios
                Dim arreglo As New Dictionary(Of String, String)
                arreglo.Add("nombreArchivo", "ActaTrabajoBachiller")
                arreglo.Add("sesionUsuario", Session("perlogin").ToString)
                '-----------------                
                arreglo.Add("codigo_tba", "1")

                '********2. GENERA DOCUMENTO PDF **************************************************************
                codigo_dot = clsDocumentacion.generarDocumentoPdf(serieCorrelativoDoc, arreglo)
                'codigo_dot = clsDocumentacion.generarDocumentoPdf(serieCorrelativoDoc, arreglo)
                '**********************************************************************************************

                'Dim bytes() As Byte = memory.ToArray
                'memory.Close()

                'Response.Clear()
                'Response.ContentType = "application/pdf"
                'Response.AddHeader("content-length", bytes.Length.ToString())
                'Response.BinaryWrite(bytes)
                Call mt_ShowMessage(codigo_dot, MessageType.success)


            Else
                Call mt_ShowMessage("no se generó correlativo", MessageType.error)
            End If


            '''''****************************************************************************************************************







        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub


    Protected Sub lbNotas_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbNotas.Click
        Try

            ''''********************************* Previo **********************************************************************

            'Dim arreglo As New Dictionary(Of String, String)
            'Dim nombreArchivo As String = "ActaEvaluacion"
            'Dim codigoDatos As String = "596812"

            'arreglo.Add("nombreArchivo", nombreArchivo)
            'arreglo.Add("sesionUsuario", Session("perlogin").ToString)
            ''-----------------                
            'arreglo.Add("codigo_cup", codigoDatos)


            'clsDocumentacion.PrevioDocumentopdf("T", arreglo, memory)
            'Dim bytes() As Byte = memory.ToArray
            'memory.Close()

            'Response.Clear()
            'Response.ContentType = "application/pdf"
            'Response.AddHeader("content-length", bytes.Length.ToString())
            'Response.BinaryWrite(bytes)

            ''''******************************************Genera **********************************************************************


            Dim codigo_dot As Integer      '' es lo que se va obtener a invocar el metododo generarDocumentoPdf y va servir para descargar doucmento luego
            'Dim codigo_cda As Integer = 8  ''-- Configuracion del documento obligatorio
            Dim serieCorrelativoDoc As String ''--- Este es el correlativo o la numeración del documento a utilizar

            ''''**** 1. GENERA CORRELATIVO DEL DOCUMNETO CONFIGURADO *******************************************************
            'serieCorrelativoDoc = clsDocumentacion.ObtenerSerieCorrelativoDocPorCda(codigo_cda, Year(Now), codigo_usu) '' Se obtiene el correlativo
            serieCorrelativoDoc = clsDocumentacion.ObtenerSerieCorrelativoDocPorAbreviatura("ACT", "AEVN", "USAT", Year(Now), codigo_usu)

            ''''******* GENERA DOCUMENTO PDF *****************************************************************************
            If serieCorrelativoDoc <> "" Then
                '--------necesarios
                Dim arreglo As New Dictionary(Of String, String)

                arreglo.Add("nombreArchivo", "ActaEvaluacion") '' nombre del documento tal como está
                arreglo.Add("sesionUsuario", Session("perlogin").ToString) '---- ejemplo: USAT\LLLONTOP
                '-----------------                
                arreglo.Add("codigo_cup", "596812")  ''Aqui se debe enviar el codigo del curso programado codigo_cup en ese caso

                '********2. GENERA DOCUMENTO PDF **************************************************************
                codigo_dot = clsDocumentacion.generarDocumentoPdf(serieCorrelativoDoc, arreglo, 0, memory) '' aqui se obtiene el codigo_dot para usarlo despues en la descarga del documento
                '**********************************************************************************************

                'Dim bytes() As Byte = memory.ToArray
                'memory.Close()

                'Response.Clear()
                'Response.ContentType = "application/pdf"
                'Response.AddHeader("content-length", bytes.Length.ToString())
                'Response.BinaryWrite(bytes)


                Call mt_ShowMessage(codigo_dot, MessageType.success)


            Else
                Call mt_ShowMessage("no se generó correlativo", MessageType.error)
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
                    'mt_DescargarArchivo(codigo_dot, 30, "3N23G777FS")

                    Response.Clear()
                    Response.Buffer = False
                    Response.Charset = ""
                    Response.Cache.SetCacheability(HttpCacheability.NoCache)
                    Response.ContentType = "application/pdf"
                    Response.AddHeader("content-disposition", "attachment;filename=" & "ResolucionSustentacion.pdf".ToString.Replace(",", ""))
                    Response.AppendHeader("Content-Length", bytes.Length.ToString())
                    Response.BinaryWrite(bytes)
                    Response.End()

                    'mt_ShowMessage("¡ Se ha descargado el silabo correctamente !", MessageType.success)
            End Select

        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Protected Sub lbEmail_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbEmail.Click
        Try
            Dim objGeneraDocumento As New clsGeneraDocumento
            Dim dtEmail As New Data.DataTable
            Dim codigoEnvio As Integer
            Dim ultimaFila As Integer
            Dim alumnos As String = ""
            Dim emailAlumno As String = ""

            dtEmail = objGeneraDocumento.fc_DatosResolSustentacion("6162")

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
                        emailAlumno = emailAlumno + dtEmail.Rows(i).Item("eMail_Alu") & ";"
                    End If

                Next

                '*************************************
                ClsComunicacionInstitucional.EnviarNotificacionEmail(codigoEnvio, "SUST", "CJYE", "1", codigo_usu, "codigo_alu", dtEmail.Rows(0).Item("codigo_Alu"), "77", "olluen@usat.edu.pe", "", "", "C:/Documentos/Documentacion/2020\10\6\0\89191.pdf", _
                                                                     dtEmail.Rows(0).Item("nombreOficial_cpf"), dtEmail.Rows(0).Item("titulo_tes"), alumnos, dtEmail.Rows(0).Item("fechaprogramacion"), dtEmail.Rows(0).Item("horaprogramacion"), dtEmail.Rows(0).Item("ambiente"), dtEmail.Rows(0).Item("presidente"), dtEmail.Rows(0).Item("secretario"), dtEmail.Rows(0).Item("vocal"))


                'ClsComunicacionInstitucional.EnviarNotificacionEmail(codigoEnvio, "SUST", "CJYE", "1", codigo_usu, "codigo_alu", dtEmail.Rows(i).Item("codigo_Alu"), "77", dtEmail.Rows(i).Item("eMail_Alu"), "", "", "", _
                '                                                     dtEmail.Rows(i).Item("nombreOficial_cpf"), dtEmail.Rows(i).Item("titulo_tes"), dtEmail.Rows(i).Item("alumno"), dtEmail.Rows(i).Item("fechaprogramacion"), dtEmail.Rows(i).Item("horaprogramacion"), dtEmail.Rows(i).Item("ambiente"), dtEmail.Rows(i).Item("presidente"), dtEmail.Rows(i).Item("secretario"), dtEmail.Rows(i).Item("vocal"))

                'ClsComunicacionInstitucional.EnviarNotificacionEmail(codigoEnvio, "SUST", "CJYE", "1", codigo_usu, "codigo_alu", dtEmail.Rows(i).Item("codigo_Alu"), "77", "todousat@usat.edu.pe", "", "", "", _
                '                                                dtEmail.Rows(i).Item("nombreOficial_cpf"), dtEmail.Rows(i).Item("titulo_tes"), dtEmail.Rows(i).Item("alumno"), dtEmail.Rows(i).Item("fechaprogramacion"), dtEmail.Rows(i).Item("horaprogramacion"), dtEmail.Rows(i).Item("ambiente"), dtEmail.Rows(i).Item("presidente"), dtEmail.Rows(i).Item("secretario"), dtEmail.Rows(i).Item("vocal"))

                'md_documentacion.ActualizaEnviaCorreoProgramacion(codigo_pst)

            End If

            'ClsComunicacionInstitucional.EnviarNotificacion("EMAIL", "SUST", "STEG", "1", cod_user, "codigo_pso", 33233, codigo_apl, "jbanda@usat.edu.pe", "", "correoCampusVirtual", _
            '                              nombre_escuela, nombre_bachiller, nombre_tesis, dia_sustentacion, hora_sustentacion, aula_sustentacion)
            'fin cambio 01 

            Call mt_ShowMessage("Se firmó el archivo con éxito", MessageType.success)
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
        

    End Sub

    Protected Sub lbCarta_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbCarta.Click
        Try
            Dim arreglo As New Dictionary(Of String, String)
            Dim nombreArchivo As String = "CartaCompromisoAsesoramientoTesis"
            Dim codigoDatos As String = "8812" '-- 3277
            Dim codigoCac As String = "74"

            arreglo.Add("nombreArchivo", nombreArchivo)
            arreglo.Add("sesionUsuario", Session("perlogin").ToString)
            '-----------------                
            arreglo.Add("codigo_tes", codigoDatos)
            '--- se adiciona  para el tipo de estudio post grado
            arreglo.Add("codigo_cac", codigoCac) ''--- codigo_test de la tabla tipo estudio no el que viene de la url si no el del alumno 


            clsDocumentacion.PrevioDocumentopdf("", arreglo, memory)
            Dim bytes() As Byte = memory.ToArray
            memory.Close()

            Response.Clear()
            Response.ContentType = "application/pdf"
            Response.AddHeader("content-length", bytes.Length.ToString())
            Response.BinaryWrite(bytes)

        Catch ex As Exception

            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try



        '' '' ------------------------------ nuevo por abreviaturas
        'Dim serieCorrelativoDoc As String
        'Dim codigo_dot As Integer
        'Dim abreviatura_tid As String = "CART" '---fijo 
        'Dim abreviatura_doc As String = "CCAT" '---fijo
        'Dim abreviatura_are As String = "USAT" '---fijo 


        ' ''-------------------------------genera el correlativo
        'serieCorrelativoDoc = clsDocumentacion.ObtenerSerieCorrelativoDocPorAbreviatura(abreviatura_tid, abreviatura_doc, abreviatura_are, Year(Now), codigo_usu)

        ' ''-------------------------------genera el documento
        '''''******* GENERA DOCUMENTO PDF *****************************************************************************

        'If serieCorrelativoDoc <> "" Then
        '    '--------necesarios
        '    Dim arreglo As New Dictionary(Of String, String)
        '    arreglo.Add("nombreArchivo", "CartaCompromisoAsesoramientoTesis")
        '    arreglo.Add("sesionUsuario", Session("perlogin").ToString)
        '    '-----------------                
        '    arreglo.Add("codigo_tes", "8812") '' codigo_tes
        '    arreglo.Add("codigo_cac", "74") ''--- ciclo académico

        '    '********2. GENERA DOCUMENTO PDF **************************************************************
        '    codigo_dot = clsDocumentacion.generarDocumentoPdf(serieCorrelativoDoc, arreglo)
        '    'codigo_dot = clsDocumentacion.generarDocumentoPdf(serieCorrelativoDoc, arreglo)
        '    '**********************************************************************************************

        '    Call mt_ShowMessage(codigo_dot, MessageType.success)

        'Else
        '    Call mt_ShowMessage("correlativo no generado", MessageType.error)
        'End If

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

                dt = md_solicitaDocumento.ListarSolicitaDocumentacion("SXD", 0, Me.ddlCodigo_doc.SelectedValue, "", codigo_usu)
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





