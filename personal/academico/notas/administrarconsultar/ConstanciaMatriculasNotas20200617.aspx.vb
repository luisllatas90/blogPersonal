﻿Imports iTextSharp.text.html
Imports System.Collections.Generic

Partial Class academico_notas_administrarconsultar_ConstanciaMatriculasNotas
    Inherits System.Web.UI.Page

#Region "Variables"

    Private ruta As String = ConfigurationManager.AppSettings("SharedFiles") '"http://localhost/campusvirtual/ArchivosCompartidos/SharedFiles.asmx"
    Dim tipoestudio, codigo_tfu, curso, codigo_cac, tipo, codigo_usu, tipoPrint, codigo_alu, rutaReporte As String
    Dim md_configuraDoc As d_ConfigurarDocumentoArea
    Dim me_cofiguracionDoc As e_ConfigurarDocumentoArea
    Dim me_documentacion As e_Documentacion
    Dim md_documentacion As d_Documentacion
    Dim me_documentacionArchivo As e_DocumentacionArchivo
    Dim md_documentacionArchivo As d_DocumentacionArchivo

    Dim md_Funciones As d_Funciones
    Dim md_Horario As d_Horario
    Dim me_CicloAcademico As e_CicloAcademico

    Dim memory As New System.IO.MemoryStream
    Dim bytes As Byte()

    Dim sourceIcon As String = Server.MapPath(".") & "/logo_usat.png"
    Dim sourceSello As String = Server.MapPath(".") & "/selloDiracadMtesen.png"
    Private _fuente As String = Server.MapPath(".") & "/segoeui.ttf"

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
            Response.Redirect("../../../../sinacceso.html")
        End If

        'Response.Write(Server.MapPath("."))
        'Dim usuario As String = Session("perlogin")
        'Response.Write(usuario)
        codigo_tfu = Request.QueryString("ctf")
        'tipoestudio = Request.QueryString("mod")
        tipoestudio = "2"
        codigo_usu = Request.QueryString("id")
        tipoPrint = Request.QueryString("tipoPrint")

        rutaReporte = ConfigurationManager.AppSettings("RutaReporte")

        If IsPostBack = False Then

            Call mt_CargarComboCicloAcademico()
            Call mt_cargarComboTipoReporte()
            Call mt_CargarDocumentacion()
            'Call mt_cargarCarrreraProfesional()
        End If

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
                dt = consultarAlumno()
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

    Protected Sub ddlSemAca_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlSemAca.SelectedIndexChanged
        ifrmReporte.Attributes("src") = "about:blank"
    End Sub

    Protected Sub lbImprimir_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbImprimir.Click
        Try

            'mt_DescargarArchivo(3, 30, "3N23G777FS")
            'mt_ShowMessage("¡ Se ha descargado el silabo correctamente !", MessageType.success)

            Dim correlativo_doc As String = "_"

            ''si tipo de impresión Fich de MAtrícula
            If ddlTipoReporte.SelectedValue = "1" Then
                ifrmReporte.Attributes("src") = rutaReporte & "PRIVADOS/ACADEMICO/ACAD_FichaMatricula&id=684&ctf=1&codigo_Alu=" & Me.TxtCodAlu.Text & "&codigoUniver_Alu=" & Me.txtCodigoAlu.Text & "&codigo_cac=" & Me.ddlSemAca.SelectedValue & "&correlativo_doc=" & correlativo_doc

                ''si tipo de impresión Fich de Notas
            ElseIf ddlTipoReporte.SelectedValue = "2" Then
                ifrmReporte.Attributes("src") = rutaReporte & "PRIVADOS/ACADEMICO/ACAD_FichaNotas&id=684&ctf=1&codigo_alu=" & Me.TxtCodAlu.Text & "&codigoUniver_Alu=" & Me.txtCodigoAlu.Text & "&codigo_cac=" & Me.ddlSemAca.SelectedValue & "&correlativo_doc=" & correlativo_doc

            End If

        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try

    End Sub

    Protected Sub lbGenerar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbGenerar.Click
        Try
            Dim correlativo_doc As Integer = 0
            Dim anio_doc As String
            Dim codigo_cda As Integer = 0
            Dim correlativo As String = ""
            Dim codigo_doc As Integer = 0
            Dim dt As New Data.DataTable
            ''validaciones
            If Me.ddlTipoReporte.SelectedValue = "0" Then
                Call mt_ShowMessage("Debe seleccionar el tipo de reporte..!", MessageType.warning)
                Exit Sub
            End If
            If Me.ddlSemAca.SelectedValue = "" Then
                Call mt_ShowMessage("Debe seleccionar el ciclo académico..!", MessageType.warning)
                Exit Sub
            End If
            If Me.txtDescripcionAlu.Text = "" Then
                Call mt_ShowMessage("Debe seleccionar el alumno...!", MessageType.warning)
                Me.txtCodigoAlu.Focus()
                Exit Sub
            End If
            ''fin de validaciones

            ''si tipo de impresión Fich de MAtrícula
            If ddlTipoReporte.SelectedValue = "1" Then

                md_configuraDoc = New d_ConfigurarDocumentoArea
                dt = md_configuraDoc.ListarConfigurarDocumentoArea("COR", 0, "ACAD_FichaMatricula", 0, 0, 0, 0)

                If dt.Rows.Count > 0 Then

                    With dt.Rows(0)
                        codigo_cda = .Item("codigo_cda")
                        correlativo_doc = .Item("correlativo_doc")
                        anio_doc = .Item("anio_doc")
                        correlativo = .Item("correlativo")
                    End With

                    me_documentacion = New e_Documentacion
                    md_documentacion = New d_Documentacion

                    With me_documentacion
                        .anio_dot = Year(Now)
                        .codigo_cda = codigo_cda
                        .codigo_dot = codigo_doc
                        .correlativo_dot = correlativo_doc
                        .estado_dot = 1
                        .glosa_dot = "Glosa ACAD_FichaMatricula"
                        .usuarioReg_AUD = codigo_usu
                    End With
                    'codigo_doc = md_documentacion.RegistrarActualizarDocumentacion(me_documentacion)

                    ifrmReporte.Attributes("src") = rutaReporte & "PRIVADOS/ACADEMICO/ACAD_FichaMatricula&id=684&ctf=1&codigo_Alu=" & Me.TxtCodAlu.Text & "&codigoUniver_Alu=" & Me.txtCodigoAlu.Text & "&codigo_cac=" & Me.ddlSemAca.SelectedValue & "&correlativo_doc= " & correlativo
                Else
                    Call mt_ShowMessage("Este es documento no se encuentra configurado", MessageType.error)
                    Exit Sub
                End If

                ''si tipo de impresión Fich de Notas
            ElseIf ddlTipoReporte.SelectedValue = "2" Then

                md_configuraDoc = New d_ConfigurarDocumentoArea
                dt = md_configuraDoc.ListarConfigurarDocumentoArea("COR", 0, "ACAD_FichaNotas", 0, 0, 0, 0)
                If dt.Rows.Count > 0 Then

                    With dt.Rows(0)
                        codigo_cda = .Item("codigo_cda")
                        correlativo_doc = .Item("correlativo_doc")
                        anio_doc = .Item("anio_doc")
                        correlativo = .Item("correlativo")
                    End With

                    me_documentacion = New e_Documentacion
                    md_documentacion = New d_Documentacion

                    With me_documentacion
                        .anio_dot = Year(Now)
                        .codigo_cda = codigo_cda
                        .codigo_dot = codigo_doc
                        .correlativo_dot = correlativo_doc
                        .estado_dot = 1
                        .glosa_dot = "Glosa ACAD_FichaMatricula"
                        .usuarioReg_AUD = codigo_usu
                    End With
                    'codigo_doc = md_documentacion.RegistrarActualizarDocumentacion(me_documentacion)
                    ifrmReporte.Attributes("src") = rutaReporte & "PRIVADOS/ACADEMICO/ACAD_FichaNotas&id=684&ctf=1&codigo_alu=" & Me.TxtCodAlu.Text & "&codigoUniver_Alu=" & Me.txtCodigoAlu.Text & "&codigo_cac=" & Me.ddlSemAca.SelectedValue & "&correlativo_doc= " & correlativo
                Else
                    ifrmReporte.Attributes("src") = "about:blank"
                    Call mt_ShowMessage("Este es documento no se encuentra configurado", MessageType.error)
                    Exit Sub
                End If


            End If
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Protected Sub lbGeneraPdf_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbGeneraPdf.Click

        Dim dt As New Data.DataTable
        Dim dtAlumno As New Data.DataTable
        Dim serieCorrelativo As String
        Dim respuesta As String
        Dim dtInserta As New Data.DataTable
        Dim codigo_dot, codigo_cda, correlativo_dot As Integer
        md_configuraDoc = New d_ConfigurarDocumentoArea

        ''-----******** validaciones***---------------
        If Me.ddlSemAca.SelectedValue = "" Then
            Call mt_ShowMessage("Debe seleccionar el ciclo académico..!", MessageType.warning)
            Exit Sub
        End If
        If Me.txtDescripcionAlu.Text = "" Then
            Call mt_ShowMessage("Debe seleccionar el alumno...!", MessageType.warning)
            Me.txtCodigoAlu.Focus()
            Exit Sub
        End If
        ''----- ------------------------------------- 
        'verifico si hay información para el reporte
        Dim dtDet As New Data.DataTable
        dtDet = consultarDatosReporte(Me.ddlTipoReporte.SelectedValue)

        If dtDet.Rows.Count = 0 Then
            Call mt_ShowMessage("No hay información para generar el documento solicitado...!", MessageType.warning)
            Exit Sub
        Else
            dtAlumno = consultarAlumno()
        End If

        Try
            'instancio mi clase
            md_documentacion = New d_Documentacion

            Dim dtCorrelativo As New Data.DataTable
            ''si tipo de impresión Fich de MAtrícula
            If ddlTipoReporte.SelectedValue = "1" Then

                '********************************** traigo correlativo 

                dtCorrelativo = md_documentacion.GeneraCorrelativoDocumentacion(1, 1, 181, 8, Year(Now), codigo_usu) '--tipodocumento, area,tipofuncion,documento, anio

                If dtCorrelativo.Rows.Count > 0 Then

                    With dtCorrelativo.Rows(0)
                        codigo_cda = .Item("codigo_cda")
                        correlativo_dot = .Item("correlativo_dot")
                        'anio_dot = .Item("anio_dot")
                        serieCorrelativo = .Item("configuracion")
                        codigo_dot = .Item("codigo_dot")
                    End With
                    'utilizo la clase generaDocumento
                    Dim objClsGeneraDocumento As New clsGeneraDocumento

                    objClsGeneraDocumento.fuente = Server.MapPath(".") & "/segoeui.ttf"
                    objClsGeneraDocumento.EmiteFichaMatricula(serieCorrelativo, memory, dtDet, dtAlumno, False, sourceIcon, sourceSello, Me.ddlSemAca.SelectedItem.Text)

                    'imprimo o creo el documento en pdf y lo presento
                    'imprimirDocumento(1, serieCorrelativo, memory)

                    Dim bytes() As Byte = memory.ToArray
                    memory.Close()

                    'subir archivo
                    respuesta = objClsGeneraDocumento.fc_SubirArchivo(30, codigo_dot, 0, memory, "FichaMatricula.pdf", Session("perlogin").ToString)

                    ''*******************documentacion archivo
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
                    End With
                    'inserto la documentacionArchivo
                    md_documentacionArchivo.RegistrarActualizarDocumentacionArchivo(me_documentacionArchivo)

                    Call mt_CargarDocumentacion()

                    Call mt_ShowMessage("Archivo generado con éxito", MessageType.success)

                Else
                    Call mt_ShowMessage("Este documento no se encuentra configurado", MessageType.error)
                    Exit Sub
                End If

            Else

                dtCorrelativo = md_documentacion.GeneraCorrelativoDocumentacion(1, 1, 181, 10, Year(Now), codigo_usu) '--tipodocumento, area,tipofuncion,documento, anio

                If dtCorrelativo.Rows.Count > 0 Then

                    With dtCorrelativo.Rows(0)
                        codigo_cda = .Item("codigo_cda")
                        correlativo_dot = .Item("correlativo_dot")
                        'anio_dot = .Item("anio_dot")
                        serieCorrelativo = .Item("configuracion")
                        codigo_dot = .Item("codigo_dot")
                    End With
                    'utilizo la clase generaDocumento
                    Dim objClsGeneraDocumento As New clsGeneraDocumento

                    objClsGeneraDocumento.fuente = Server.MapPath(".") & "/segoeui.ttf"
                    objClsGeneraDocumento.EmiteFichaNotas(serieCorrelativo, memory, dtDet, dtAlumno, False, sourceIcon, sourceSello, Me.ddlSemAca.SelectedItem.Text)

                    'imprimo o creo el documento en pdf y lo presento
                    'imprimirDocumento(1, serieCorrelativo, memory)

                    Dim bytes() As Byte = memory.ToArray
                    memory.Close()

                    'subir archivo
                    respuesta = objClsGeneraDocumento.fc_SubirArchivo(30, codigo_dot, 0, memory, "FichaMatricula.pdf", Session("perlogin").ToString)

                    ''*******************documentacion archivo
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
                    End With
                    'inserto la documentacionArchivo
                    md_documentacionArchivo.RegistrarActualizarDocumentacionArchivo(me_documentacionArchivo)

                    Call mt_CargarDocumentacion()

                    Call mt_ShowMessage("Archivo generado con éxito", MessageType.success)

                Else
                    Call mt_ShowMessage("Este documento no se encuentra configurado", MessageType.error)
                    Exit Sub
                End If

            End If
        Catch ex As Exception
            Call mt_ShowMessage("boton " & ex.Message.Replace("'", " "), MessageType.error)
        End Try


    End Sub

    Protected Sub lbVerPdf_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbVerPdf.Click
        Dim dtDet As New Data.DataTable
        Dim dtAlumno As New Data.DataTable
        Dim objClsGeneraDocumento As New clsGeneraDocumento

        ''-----******** validaciones***---------------
        If Me.ddlTipoReporte.SelectedValue = "0" Then
            Call mt_ShowMessage("Debe seleccionar el tipo de reporte..!", MessageType.warning)
            Exit Sub
        End If
        If Me.ddlSemAca.SelectedValue = "" Then
            Call mt_ShowMessage("Debe seleccionar el ciclo académico..!", MessageType.warning)
            Exit Sub
        End If
        If Me.txtDescripcionAlu.Text = "" Then
            Call mt_ShowMessage("Debe seleccionar el alumno...!", MessageType.warning)
            Me.txtCodigoAlu.Focus()
            Exit Sub
        End If
        ''----- ------------------------------------- 

        Try
            'verifico si hay información para el reporte
            dtDet = consultarDatosReporte(Me.ddlTipoReporte.SelectedValue)
            If dtDet.Rows.Count = 0 Then
                Call mt_ShowMessage("No hay información para generar el documento solicitado...!", MessageType.warning)
                Exit Sub
            Else
                dtAlumno = consultarAlumno()
            End If

            objClsGeneraDocumento.fuente = Server.MapPath(".") & "/segoeui.ttf"


            ''si tipo de impresión Fich de MAtrícula
            If ddlTipoReporte.SelectedValue = "1" Then

                objClsGeneraDocumento.EmiteFichaMatricula("", memory, dtDet, dtAlumno, True, sourceIcon, sourceSello, Me.ddlSemAca.SelectedItem.Text)
                Dim bytes() As Byte = memory.ToArray
                memory.Close()

                Response.Clear()
                Response.ContentType = "application/pdf"
                Response.AddHeader("content-length", bytes.Length.ToString())
                Response.BinaryWrite(bytes)

            Else

                objClsGeneraDocumento.EmiteFichaNotas("", memory, dtDet, dtAlumno, True, sourceIcon, sourceSello, Me.ddlSemAca.SelectedItem.Text)
                Dim bytes() As Byte = memory.ToArray
                memory.Close()

                Response.Clear()
                Response.ContentType = "application/pdf"
                Response.AddHeader("content-length", bytes.Length.ToString())
                Response.BinaryWrite(bytes)

            End If
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try




    End Sub

    Protected Sub gvListaDocumentacion_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvListaDocumentacion.RowCommand
        Dim codigo_dot As Integer = 0
        Dim usuario As String = Session("perlogin")

        Dim md_clsGeneraDocumento As New clsGeneraDocumento

        Try
            Dim index As Integer = 0 : index = CInt(e.CommandArgument)
            codigo_dot = Me.gvListaDocumentacion.DataKeys(index).Values("codigo_dot")

            Select Case e.CommandName

                Case "descargar"

                    mt_ShowMessage("¡ Se ha descargado el silabo correctamente !", MessageType.success)

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

                    mt_ShowMessage("¡ Se ha descargado el silabo correctamente !", MessageType.success)
            End Select

        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Protected Sub lbPrevisualizar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbPrevisualizar.Click
        Try
            Dim objGeneraDoc As New clsGeneraDocumento

            objGeneraDoc.fuente = Server.MapPath(".") & "/segoeui.ttf"

            objGeneraDoc.EmiteActaSustentación(memory, sourceIcon, "ACTA-SUST-0000-2020-USAT")
            Dim bytes() As Byte = memory.ToArray
            memory.Close()



            'abrir una nueva pestaa en el navegador
            Response.Clear()
            Response.ContentType = "application/pdf"
            Response.AddHeader("content-length", bytes.Length.ToString())
            Response.BinaryWrite(bytes)

        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

#End Region

#Region "Métodos y Funciones"


    Protected Sub mt_ShowMessage(ByVal Message As String, ByVal type As MessageType)
        Try
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "showMessage", "showMessage('" & Message & "','" & type.ToString & "');", True)
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Private Sub mt_CargarComboCicloAcademico()
        Try
            md_Funciones = New d_Funciones
            md_Horario = New d_Horario
            me_CicloAcademico = New e_CicloAcademico

            Dim dt As New Data.DataTable

            With me_CicloAcademico
                .tipooperacion = "TO"
                .tipocac = "0"
            End With

            dt = md_Horario.ObtenerCicloAcademicoHorario(me_CicloAcademico)

            Call md_Funciones.CargarCombo(Me.ddlSemAca, dt, "codigo_cac", "descripcion_cac", True, "[-- SELECCIONE --]", "")

        Catch ex As Exception
            Throw ex
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Private Sub mt_cargarComboTipoReporte()
        '***** Tio de Reporte
        Me.ddlTipoReporte.Items.Clear()
        'Me.ddlTipoReporte.Items.Add("[--SELECCIONE--]")
        'Me.ddlTipoReporte.Items.Add("CONSTANCIA DE MATRICULA")
        'Me.ddlTipoReporte.Items.Add("CONSTANCIA DE NOTAS")

        Me.ddlTipoReporte.Items.Add(New System.Web.UI.WebControls.ListItem("[--SELECCIONE--]", "0"))
        Me.ddlTipoReporte.Items.Add(New System.Web.UI.WebControls.ListItem("FICHA DE MATRICULA", "1"))
        Me.ddlTipoReporte.Items.Add(New System.Web.UI.WebControls.ListItem("FICHA DE NOTAS", "2"))

        Me.ddlTipoReporte.SelectedIndex = tipoPrint


    End Sub

    Private Function consultarDatosReporte(ByVal tipoPrint As String) As Data.DataTable
        Dim dt As New Data.DataTable

        Try
            Dim objcnx As New ClsConectarDatos
            objcnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            objcnx.AbrirConexion()

            If tipoPrint = "1" Then ' si es ficha de matrícula
                dt = objcnx.TraerDataTable("ConsultarNotas", "BA", Me.TxtCodAlu.Text, Me.ddlSemAca.SelectedValue, "0")
            Else 'ficha de notas
                dt = objcnx.TraerDataTable("ACAD_FichaNotas", CInt(Me.TxtCodAlu.Text), CInt(Me.ddlSemAca.SelectedValue))
            End If

            objcnx.CerrarConexion()


        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
        
        Return dt

    End Function

    Private Function consultarAlumno() As Data.DataTable
        Dim objcnx As New ClsConectarDatos
        objcnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        objcnx.AbrirConexion()
        Dim dt As New Data.DataTable
        dt = objcnx.TraerDataTable("ConsultarAlumno", "CU", Me.txtCodigoAlu.Text.Trim())
        objcnx.CerrarConexion()
        If dt.Rows.Count = 0 Then
            Call mt_ShowMessage("No se encontró el alumno..¡", MessageType.warning)
            Me.txtDescripcionAlu.Text = String.Empty
        End If
        Return dt
    End Function

    Private Function insertaDocumentacion(ByVal codigo_cda As Integer, ByVal codigo_dot As Integer, ByVal correlativo_dot As Integer, ByVal glosa_dot As String) As Data.DataTable
        Dim dt As New Data.DataTable
        Try
            me_documentacion = New e_Documentacion
            md_documentacion = New d_Documentacion

            With me_documentacion
                .anio_dot = Year(Now)
                .codigo_cda = codigo_cda
                .codigo_dot = codigo_dot
                .correlativo_dot = correlativo_dot
                .estado_dot = 1
                .glosa_dot = glosa_dot
                .usuarioReg_AUD = codigo_usu
            End With
            dt = md_documentacion.RegistrarActualizarDocumentacion(me_documentacion)

            Return dt
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Function

    Private Sub mt_CargarDocumentacion()
        md_documentacion = New d_Documentacion

        Dim dt As New Data.DataTable

        Try
            ' si es ficha de notas

            If tipoPrint = 1 Then
                dt = md_documentacion.ListarDocumentacion("FIC", 0)
            Else
                dt = md_documentacion.ListarDocumentacion("FIN", 0)
            End If

            If dt.Rows.Count > 0 Then
                Me.gvListaDocumentacion.DataSource = dt
                Me.gvListaDocumentacion.DataBind()
                'Para ocultar las columnas y no pierda el valor
                'Call mt_ocultarColCursosAmbientes()
            Else
                Call mt_ShowMessage("No se encontraron documentos emitidos", MessageType.warning)
            End If

        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    

#End Region


    
    
End Class
