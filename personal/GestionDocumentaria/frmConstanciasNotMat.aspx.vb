Imports System.Collections.Generic

Partial Class GestionDocumentaria_frmConstanciasNotMat
    Inherits System.Web.UI.Page

#Region "variables"
    Dim codigo_tfu As Integer
    Dim codigo_usu As Integer
    Dim tipoEstudio As String

    Dim md_funciones As d_Funciones
    Dim md_documento As d_Documento
    Dim md_horario As d_Horario
    Dim me_cicloAcademico As e_CicloAcademico
    Dim me_solictaDocumento As e_SolicitaDocumento
    Dim md_solicitaDocumento As New d_SolicitaDocumentacion
    Dim md_documentacion As New d_Documentacion


    Dim sourceSello As String = Server.MapPath(".") & "/img/selloDiracadMtesen.png"
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

        codigo_tfu = Request.QueryString("ctf")
        tipoEstudio = "2"
        codigo_usu = Request.QueryString("id")


        If IsPostBack = False Then
            Call mt_CargarComboDocumento()
            Call mt_CargarComboCicloAcademico()
        Else
            Call mt_CargarSolicitudesByDocumento()
        End If
    End Sub
    Protected Sub lbGeneraDocumento_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbGeneraDocumento.Click
        Try

            Dim dtDet As New Data.DataTable
            Dim dtSolicitud As New Data.DataTable
            Dim codigo_sol As Integer = 0
            Dim codigo_dot As Integer = 0
            Dim serieCorrelativo As String = ""
            Dim lo_Validacion As Dictionary(Of String, String) = ValidarFormulario()

            me_solictaDocumento = New e_SolicitaDocumento
            md_solicitaDocumento = New d_SolicitaDocumentacion

            If lo_Validacion.Item("rpta") = 0 Then
                Call mt_ShowMessage(lo_Validacion.Item("msg"), MessageType.warning)
                Exit Sub
            End If


            dtDet = consultarDatosReporte(Me.ddlCodigo_doc.SelectedValue, Me.TxtCodAlu.Text, Me.ddlCodigo_cac.SelectedValue)
            If dtDet.Rows.Count = 0 Then
                Call mt_ShowMessage("¡ No hay información para esta documento !", MessageType.warning)
                Exit Sub
            End If

            '**----------------------------------------------------------------------------------------

            If Me.ddlCodigo_doc.SelectedValue = 1 Then 'FichaMatricula

                With me_solictaDocumento
                    .codigo_cda = 1
                    .codigoUniver_Alu = Trim(Me.txtCodigoAlu.Text)
                    .codigo_alu = Me.TxtCodAlu.Text
                    .codigo_sol = 0
                    .codigo_cac = ddlCodigo_cac.SelectedValue
                    .codigoDatos = 0
                    .estado_sol = "7"
                    .usuarioReg = codigo_usu
                    .referencia01 = ddlCodigo_cac.SelectedItem.Text
                    .nombreArchivo = "FichaMatricula"
                End With
                dtSolicitud = md_solicitaDocumento.RegistraActualizaSolicitaDocumentacion(me_solictaDocumento)
                If dtSolicitud.Rows.Count > 0 Then
                    codigo_sol = dtSolicitud.Rows(0).Item("codigo_sol")
                End If
                If codigo_sol > 0 Then
                    serieCorrelativo = clsDocumentacion.ObtenerSerieCorrelativoDocPorCda(1, Year(Now), codigo_usu)
                Else
                    Call mt_ShowMessage("¡ no se ha generado la solicitud !", MessageType.warning)
                End If

                '--------necesarios
                Dim arreglo As New Dictionary(Of String, String)
                arreglo.Add("nombreArchivo", "FichaMatricula")
                arreglo.Add("sesionUsuario", Session("perlogin").ToString)
                '-----------------                
                arreglo.Add("codigo_sol", codigo_sol)
                arreglo.Add("sourceSello", sourceSello) '---- el sello o la firma 

                If serieCorrelativo <> "" Then
                    clsDocumentacion.generarDocumentoPdf(serieCorrelativo, arreglo, arreglo.Item("codigo_sol"))
                Else
                    Call mt_ShowMessage("¡ no se ha generado el correlativo!", MessageType.warning)
                End If

              

            ElseIf Me.ddlCodigo_doc.SelectedValue = 2 Then 'Ficha Notas

                With me_solictaDocumento
                    .codigo_cda = 2  '' ---- Ficha de notas
                    .codigoUniver_Alu = Trim(Me.txtCodigoAlu.Text)
                    .codigo_alu = Me.TxtCodAlu.Text
                    .codigo_sol = 0
                    .codigo_cac = ddlCodigo_cac.SelectedValue
                    .codigoDatos = 0
                    .estado_sol = "7"
                    .usuarioReg = codigo_usu
                    .referencia01 = ddlCodigo_cac.SelectedItem.Text
                    .nombreArchivo = "FichaNotas"
                End With

                dtSolicitud = md_solicitaDocumento.RegistraActualizaSolicitaDocumentacion(me_solictaDocumento)
                If dtSolicitud.Rows.Count > 0 Then
                    codigo_sol = dtSolicitud.Rows(0).Item("codigo_sol")
                End If
                If codigo_sol > 0 Then
                    serieCorrelativo = clsDocumentacion.ObtenerSerieCorrelativoDocPorCda(1, Year(Now), codigo_usu)
                Else
                    Call mt_ShowMessage("¡ no se ha generado el documento !", MessageType.warning)
                End If

                '--------necesarios
                Dim arreglo As New Dictionary(Of String, String)
                arreglo.Add("nombreArchivo", "FichaNotas")
                arreglo.Add("sesionUsuario", Session("perlogin").ToString)
                '-----------------                
                arreglo.Add("codigo_sol", codigo_sol)
                arreglo.Add("sourceSello", sourceSello) '---- el sello o la firma 

                If serieCorrelativo <> "" Then
                    clsDocumentacion.generarDocumentoPdf(serieCorrelativo, arreglo, arreglo.Item("codigo_sol"))
                Else
                    Call mt_ShowMessage("¡ no se ha generado el correlativo!", MessageType.warning)
                End If

            End If

            Call limpiar()
            Call mt_CargarSolicitudesByDocumento()
            Call mt_ShowMessage("¡ Se ha generado el documento!", MessageType.success)

        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try

    End Sub
    Protected Sub lbBuscaAlu_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbBuscaAlu.Click
        Try
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
    Protected Sub ddlCodigo_doc_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlCodigo_doc.SelectedIndexChanged
        Try
            Call mt_CargarSolicitudesByDocumento()
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try

    End Sub
    Protected Sub lbVerPdf_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbVerPdf.Click
        Try
            Dim dtDet As New Data.DataTable
            Dim lo_Validacion As Dictionary(Of String, String) = ValidarFormulario()

            If lo_Validacion.Item("rpta") = 0 Then
                Call mt_ShowMessage(lo_Validacion.Item("msg"), MessageType.warning)
                Exit Sub
            End If

            dtDet = consultarDatosReporte(Me.ddlCodigo_doc.SelectedValue, Me.TxtCodAlu.Text, Me.ddlCodigo_cac.SelectedValue)
            If dtDet.Rows.Count = 0 Then
                Call mt_ShowMessage("¡ No hay información para esta documento !", MessageType.warning)
                Exit Sub
            End If

            If ddlCodigo_doc.SelectedValue = 1 Then   ''---****** Ficha Matricula
                Dim arreglo As New Dictionary(Of String, String)
                '--------------------- necesarios
                arreglo.Add("nombreArchivo", "FichaMatricula")
                arreglo.Add("sesionUsuario", Session("perlogin").ToString)
                '--------------------- opcionales
                arreglo.Add("codigoUniv_alu", Trim(Me.txtCodigoAlu.Text))
                arreglo.Add("codigo_Alu", Me.TxtCodAlu.Text)
                arreglo.Add("codigo_sol", "0")
                arreglo.Add("codigo_cac", Me.ddlCodigo_cac.SelectedValue)
                arreglo.Add("sourceSello", sourceSello)

                clsDocumentacion.PrevioDocumentopdf("", arreglo, memory)

            ElseIf ddlCodigo_doc.SelectedValue = 2 Then

                Dim arreglo As New Dictionary(Of String, String)
                '--------------------- necesarios
                arreglo.Add("nombreArchivo", "FichaNotas")
                arreglo.Add("sesionUsuario", Session("perlogin").ToString)
                '--------------------- opcionales
                arreglo.Add("codigoUniv_alu", Trim(Me.txtCodigoAlu.Text))
                arreglo.Add("codigo_Alu", Me.TxtCodAlu.Text)
                arreglo.Add("codigo_sol", "0")
                arreglo.Add("codigo_cac", Me.ddlCodigo_cac.SelectedValue)
                arreglo.Add("sourceSello", sourceSello)

                clsDocumentacion.PrevioDocumentopdf("", arreglo, memory)
            End If
          
           

            Dim bytes() As Byte = memory.ToArray
            memory.Close()

            Response.Clear()
            Response.ContentType = "application/pdf"
            Response.AddHeader("content-length", bytes.Length.ToString())
            Response.BinaryWrite(bytes)


        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
        

    End Sub
    Protected Sub gvListaSolicitudes_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvListaSolicitudes.RowCommand
        Try
            Dim md_clsGeneraDocumento As New clsGeneraDocumento
            Dim codigo_dot As Integer
            Dim user_login As String = Session("perlogin")

            Dim index As Integer = 0 : index = CInt(e.CommandArgument)
            codigo_dot = Me.gvListaSolicitudes.DataKeys(index).Values("codigo_dot")
            Select Case e.CommandName

                Case "descargar"
                    bytes = md_clsGeneraDocumento.DescargarArchivo(codigo_dot, 30, "3N23G777FS", user_login)
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

            End Select
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
       
    End Sub
    Protected Sub gvListaSolicitudes_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvListaSolicitudes.RowDataBound

        If e.Row.RowType = DataControlRowType.Header Then
            e.Row.TableSection = TableRowSection.TableHeader
        End If

    End Sub

    
#End Region
#Region "Metodos y funciones"
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

        ElseIf Me.txtDescripcionAlu.Text = "" Then
            lo_Validacion.Item("rpta") = 0
            lo_Validacion.Item("msg") = "¡ Ingrese el alumno !"

        ElseIf Me.txtCodigoAlu.Text = "" Then
            lo_Validacion.Item("rpta") = 0
            lo_Validacion.Item("msg") = "¡ Ingrese el alumno !"
        End If

        Return lo_Validacion
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

                dt = md_solicitaDocumento.ListarSolicitaDocumentacion("SXD", 0, Me.ddlCodigo_doc.SelectedValue, "", 0)
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

            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "formatoGrilla", "formatoGrilla();", True)

        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub
    Private Sub limpiar()
        Me.ddlCodigo_cac.SelectedValue = ""
        Me.txtCodigoAlu.Text = ""
        Me.txtDescripcionAlu.Text = ""
        Me.TxtCodAlu.Text = ""
    End Sub

    

#End Region








End Class
