Imports System.Net
Imports System.IO
Imports System.Data
Imports System.Collections.Generic

Partial Class Biblioteca_frmListaRecursoVirtual
    Inherits System.Web.UI.Page

#Region "Declaracion de Variables"
    'ENTIDADES
    Dim me_RecursoVirtual As e_RecursoVirtual
    Dim me_RecursoVirtualDetalle As e_RecursoVirtualDetalle
    Dim me_RecursoVirtualEnlace As e_RecursoVirtualEnlace
    Dim me_Categoria As e_Categoria
    Dim me_Personal As e_Personal
    Dim me_ArchivoCompartido As e_ArchivoCompartido

    'DATOS
    Dim md_RecursoVirtual As New d_RecursoVirtual
    Dim md_RecursoVirtualDetalle As New d_RecursoVirtualDetalle
    Dim md_RecursoVirtualEnlace As New d_RecursoVirtualEnlace
    Dim md_Categoria As New d_Categoria
    Dim md_Personal As New d_Personal
    Dim md_ArchivoCompartido As New d_ArchivoCompartido
    Dim md_Funciones As New d_Funciones

    'VARIABLES
    Dim cod_user As Integer = 0
    Dim tipo_repositorio As String = String.Empty
    Dim tipo_acceso As String = String.Empty

    Public Enum MessageType
        success
        [error]
        info
        warning
    End Enum

#End Region

#Region "Eventos"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If (Session("id_per") Is Nothing OrElse Session("perlogin") Is Nothing) Then
                Response.Redirect("../../sinacceso.html")
            End If

            cod_user = Session("id_per")            
            tipo_acceso = "P"

            If Not String.IsNullOrEmpty(Request.QueryString("tiporepo")) Then
                tipo_repositorio = Request.QueryString("tiporepo").ToString
            End If

            If Request("__EVENTTARGET") = "btnContar" Then
                'Call mt_ShowMessage(Request("__EVENTARGUMENT"), MessageType.error)
                mt_ContarAcceso(Request("__EVENTARGUMENT"))
            End If

            If IsPostBack = False Then
                Call mt_CargarDatos()
            End If
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

#End Region

#Region "Metodos"

    Private Sub mt_ShowMessage(ByVal Message As String, ByVal type As MessageType)
        Try
            Me.udpScripts.Update()
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "showMessage", "showMessage('" & Message & "','" & type.ToString & "');", True)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub mt_CargarDatos()
        Try
            Dim dtCategoria As New DataTable : me_Categoria = New e_Categoria

            With me_Categoria
                .operacion = "GEN"
                .grupo_cat = "TIPO_REPOSITORIO"
                .abreviatura_cat = tipo_repositorio
            End With

            dtCategoria = md_Categoria.ListarCategoria(me_Categoria)

            If dtCategoria.Rows.Count = 0 Then Exit Sub

            Dim recursos_virtuales As String = "<div class='card-header'>ACCESO A " & dtCategoria.Rows(0).Item("nombre_cat") & "</div>"

            Dim dt, dtRecurso, dtDetalle, dtEnlace As New DataTable

            'Verificar que tenga disciplinas asociadas
            me_RecursoVirtual = New e_RecursoVirtual

            With me_RecursoVirtual
                .operacion = "DIS"
                .tipoRepo_rvi = tipo_repositorio
                .acceso_rvi = tipo_acceso
                .estado_rvi = "A"
            End With

            dt = md_RecursoVirtual.ListarRecursoVirtual(me_RecursoVirtual)

            If dt.Rows.Count > 0 Then
                recursos_virtuales &= "<div class='card-body'>"

                'Listar disciplinas asociadas
                For Each row As DataRow In dt.Rows
                    recursos_virtuales &= "<div class='card'>"
                    recursos_virtuales &= "<div class='card-header'>" & row("nombre_cat") & "</div>"

                    me_RecursoVirtual = New e_RecursoVirtual

                    With me_RecursoVirtual
                        .operacion = "GEN"
                        .tipoRepo_rvi = tipo_repositorio
                        .disciplinaRepo_rvi = row("disciplinaRepo_rvi")
                        .acceso_rvi = tipo_acceso
                        .estado_rvi = "A"
                    End With

                    dtRecurso = md_RecursoVirtual.ListarRecursoVirtual(me_RecursoVirtual)

                    Dim hrRecurso As Boolean = False

                    For Each rowRecurso As DataRow In dtRecurso.Rows
                        If hrRecurso Then
                            recursos_virtuales &= "<hr/>"
                        Else
                            hrRecurso = True
                        End If

                        recursos_virtuales &= "<div class='row no-gutters'>"

                        recursos_virtuales &= "<div class='col-md-4 card-image'>"

                        'Obtener la imagen
                        Dim ls_imagen As String = String.Empty

                        'Obtener el logo del Shared Files
                        If Not (String.IsNullOrEmpty(rowRecurso("IdArchivosCompartidos")) OrElse rowRecurso("IdArchivosCompartidos") = 0) Then
                            'Obtener los datos del archivo compartido
                            me_ArchivoCompartido = md_ArchivoCompartido.GetArchivoCompartido(rowRecurso("IdArchivosCompartidos"))
                            Dim ls_extensiones As String = ".png .jpg .jpeg"

                            'Comprobar que el archivo compartido tenga la extencion de imagen
                            If ls_extensiones.Contains(me_ArchivoCompartido.extencion.ToLower) Then
                                me_ArchivoCompartido.usuario_act = Session("perlogin")
                                me_ArchivoCompartido.ruta_archivo = ConfigurationManager.AppSettings("SharedFiles")

                                Dim archivo As Byte() = md_ArchivoCompartido.ObtenerArchivoCompartido(me_ArchivoCompartido)

                                Dim ms As New IO.MemoryStream(CType(archivo, Byte()))

                                me_ArchivoCompartido.content_type = md_Funciones.ObtenerContentType(me_ArchivoCompartido.extencion)

                                ls_imagen = "data:" + me_ArchivoCompartido.content_type + ";base64," + Convert.ToBase64String(ms.ToArray(), 0, ms.ToArray().Length)
                            End If
                        End If

                        If rowRecurso("contarVisita_rvi") = "S" Then
                            If rowRecurso("codigo_biv") = 33 Then 'vLex
                                Dim correo_electronico As String = String.Empty
                                Dim dtCorreo As New DataTable

                                me_Personal = New e_Personal

                                With me_Personal
                                    .codigo_per = cod_user
                                End With

                                dtCorreo = md_Personal.CorreoPersonal(me_Personal)
                                If dtCorreo.Rows.Count > 0 Then correo_electronico = dtCorreo.Rows(0).Item("email")

                                recursos_virtuales &= "<a onclick='javascript:ContarAccesos(" & rowRecurso("codigo_biv") & ")' href='" & rowRecurso("enlace_rvi") & "?email=" & correo_electronico & "' target='_blank'>"
                                recursos_virtuales &= "<img src='" & ls_imagen & "' width='280' height='95'>"
                                recursos_virtuales &= "</a><br/>"
                            ElseIf rowRecurso("codigo_biv") = 37 Then 'Uptodate
                                recursos_virtuales &= "<a onclick='javascript:ContarAccesos(" & rowRecurso("codigo_biv") & ")' href='" & rowRecurso("enlace_rvi") & "?unid=" & cod_user & "&srcsys=HMGR292737" & "' target='_blank'>"
                                recursos_virtuales &= "<img src='" & ls_imagen & "' width='280' height='95'>"
                                recursos_virtuales &= "</a><br/>"
                            Else
                                recursos_virtuales &= "<a onclick='javascript:ContarAccesos(" & rowRecurso("codigo_biv") & ")' href='" & rowRecurso("enlace_rvi") & "' target='_blank'>"
                                recursos_virtuales &= "<img src='" & ls_imagen & "' width='280' height='95'>"
                                recursos_virtuales &= "</a><br/>"
                            End If
                        Else
                            recursos_virtuales &= "<img src='" & ls_imagen & "' width='280' height='95'>"
                        End If

                        recursos_virtuales &= "</div>"

                        recursos_virtuales &= "<div class='col-md-8'>"
                        recursos_virtuales &= "<div class='card-body'>"

                        me_RecursoVirtualDetalle = New e_RecursoVirtualDetalle

                        With me_RecursoVirtualDetalle
                            .operacion = "GEN"
                            .codigo_rvi = rowRecurso("codigo_rvi")
                            .acceso_rvd = tipo_acceso
                        End With

                        dtDetalle = md_RecursoVirtualDetalle.ListarRecursoVirtualDetalle(me_RecursoVirtualDetalle)

                        For Each rowDetalle As DataRow In dtDetalle.Rows
                            recursos_virtuales &= "<h5 class='card-title'>" & rowDetalle("titulo_rvd") & "</h5>"
                            recursos_virtuales &= "<p class='card-text' style='text-align: justify;'>"
                            recursos_virtuales &= "<small class='text-muted'>" & rowDetalle("cuerpo_rvd") & "</small>"
                            recursos_virtuales &= "</p>"

                            me_RecursoVirtualEnlace = New e_RecursoVirtualEnlace

                            With me_RecursoVirtualEnlace
                                .operacion = "GEN"
                                .codigo_rvd = rowDetalle("codigo_rvd")
                                .acceso_rve = tipo_acceso
                            End With

                            dtEnlace = md_RecursoVirtualEnlace.ListarRecursoVirtualEnlace(me_RecursoVirtualEnlace)

                            If dtEnlace.Rows.Count > 0 Then
                                recursos_virtuales &= "<p class='card-text'>"
                                recursos_virtuales &= "<small class='text-muted'>"

                                For Each rowEnlace As DataRow In dtEnlace.Rows
                                    If rowEnlace("contarVisita_rve") = "S" Then
                                        If rowEnlace("codigo_biv") = 33 Then 'vLex
                                            Dim correo_electronico As String = String.Empty
                                            Dim dtCorreo As New DataTable

                                            me_Personal = New e_Personal

                                            With me_Personal
                                                .codigo_per = cod_user
                                            End With

                                            dtCorreo = md_Personal.CorreoPersonal(me_Personal)
                                            If dtCorreo.Rows.Count > 0 Then correo_electronico = dtCorreo.Rows(0).Item("email")

                                            recursos_virtuales &= "<a onclick='javascript:ContarAccesos(" & rowEnlace("codigo_biv") & ")' href='" & rowEnlace("enlace") & "?email=" & correo_electronico & "' target='_blank'>" & rowEnlace("descripcion_rve") & "</a><br/>"

                                        ElseIf rowEnlace("codigo_biv") = 37 Then 'Uptodate
                                            recursos_virtuales &= "<a onclick='javascript:ContarAccesos(" & rowEnlace("codigo_biv") & ")' href='" & rowEnlace("enlace") & "?unid=" & cod_user & "&srcsys=HMGR292737" & "' target='_blank'>" & rowEnlace("descripcion_rve") & "</a><br/>"
                                        Else
                                            recursos_virtuales &= "<a onclick='javascript:ContarAccesos(" & rowEnlace("codigo_biv") & ")' href='" & rowEnlace("enlace") & "' target='_blank'>" & rowEnlace("descripcion_rve") & "</a><br/>"
                                        End If
                                    Else
                                        If Not (String.IsNullOrEmpty(rowEnlace("IdArchivosCompartidos")) OrElse rowEnlace("IdArchivosCompartidos") = 0) Then
                                            recursos_virtuales &= "<a href='../frmDescargarArchivoCompartido.aspx?Id=" & rowEnlace("IdArchivosCompartidos") & "' target='_blank'>" & rowEnlace("descripcion_rve") & "</a><br/>"
                                        Else
                                            recursos_virtuales &= "<a href='" & rowEnlace("enlace") & "' target='_blank'>" & rowEnlace("descripcion_rve") & "</a><br/>"
                                        End If
                                    End If
                                Next

                                recursos_virtuales &= "</small>"
                                recursos_virtuales &= "</p>"
                            End If
                        Next

                        recursos_virtuales &= "</div>"
                        recursos_virtuales &= "</div>"

                        recursos_virtuales &= "</div>"
                    Next

                    recursos_virtuales &= "</div>"
                    recursos_virtuales &= "<br/>"
                Next

                recursos_virtuales &= "</div>"
            End If

            Me.divRecursosVirtuales.InnerHtml = recursos_virtuales
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Public Sub mt_ContarAcceso(ByVal codigo_biv As Integer)
        Try
            me_RecursoVirtualEnlace = New e_RecursoVirtualEnlace

            With me_RecursoVirtualEnlace
                .tipo_vis = "P"
                .codigo_vis = cod_user
                .codigo_biv = codigo_biv
            End With

            md_RecursoVirtualEnlace.RegistrarVisita(me_RecursoVirtualEnlace)
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

#End Region

End Class
