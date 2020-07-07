﻿Imports System.Data
Imports System.IO
Imports System.Collections.Generic

Partial Class FrmConfirmarJurados
    Inherits System.Web.UI.Page
    Dim ruta As String = ConfigurationManager.AppSettings("SharedFiles")

    Dim dtpersonal As New Data.DataTable
    Private Sub ListarDocentes(ByVal tipo As String, ByVal codigo_dac As String, ByVal codigo_cac As String)
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Dim dt As New Data.DataTable
        dt = obj.TraerDataTable("SUST_DocentesAdscriptos", tipo, codigo_dac, codigo_cac)
        If dt.Rows.Count > 0 Then
            dtpersonal = dt
        End If
        obj.CerrarConexion()
    End Sub

    Private Sub ListarTramites(ByVal codigo_per As Integer, ByVal codigo_ctf As Integer, ByVal estado As String)
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Dim dt As New Data.DataTable
        dt = obj.TraerDataTable("SUST_ListarTramitesSustentacion", codigo_per, codigo_ctf, estado)

        If dt.Rows.Count > 0 Then
            Me.gvTesis.DataSource = dt
            Me.gvTesis.DataBind()
        Else
            Me.gvTesis.DataSource = Nothing
            Me.gvTesis.DataBind()
        End If
        obj.CerrarConexion()
    End Sub


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (Session("id_per") Is Nothing) Then
            Response.Redirect("../../../sinacceso.html")
        End If

        If IsPostBack = False Then
            Response.Write(HttpContext.Current.Server.MapPath("~/") & "GestionDocumentaria/font/segoeui.ttf")
            ListarDocentes("EJT", "", "")
            ListarTramites(Session("id_per"), Request("ctf"), Me.ddlEstado.SelectedValue)
            'ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "DdlPresi", "initCombo('ddlPresidente');", True)

        End If
    End Sub

    Protected Sub gvTesis_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvTesis.RowCommand
        If (Session("id_per") Is Nothing) Then
            Response.Redirect("../../../sinacceso.html")
        End If
        Try
            If (e.CommandName = "Actualizar") Then
                Dim codigo_tes As Integer = Me.gvTesis.DataKeys(e.CommandArgument).Values("codigo_Tes")
                Dim ddlPresi As DropDownList = Me.gvTesis.Rows(e.CommandArgument).FindControl("ddlPresidente")
                Dim ddlSecre As DropDownList = Me.gvTesis.Rows(e.CommandArgument).FindControl("ddlSecretario")
                Dim ddlVocal As DropDownList = Me.gvTesis.Rows(e.CommandArgument).FindControl("ddlVocal")
                If ddlPresi.SelectedValue <> 0 And ddlSecre.SelectedValue <> 0 And ddlVocal.SelectedValue <> 0 Then
                    Dim dt As New Data.DataTable
                    dt = ActualizarJurado(codigo_tes, ddlPresi.SelectedValue, ddlSecre.SelectedValue, ddlVocal.SelectedValue, Session("id_per"))
                    If dt.Rows(0).Item("Respuesta").ToString = "1" Then
                        ListarDocentes("EJT", "", "")
                        ListarTramites(Session("id_per"), Request("ctf"), Me.ddlEstado.SelectedValue)
                        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "valida", "fnMensaje('success','" + dt.Rows(0).Item("Mensaje").ToString + "');", True)
                    Else
                        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "valida", "fnMensaje('error','" + dt.Rows(0).Item("Mensaje").ToString + "');", True)
                    End If
                Else
                    ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "valida", "fnMensaje('error','Debe seleccionar los 3 jurados');", True)
                End If
                Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "Loading", "fnLoading(false)", True)

            End If

            If (e.CommandName = "Conformidad") Then
                Dim codigo_tes As Integer = Me.gvTesis.DataKeys(e.CommandArgument).Values("codigo_Tes")
                Dim ddlPresi As DropDownList = Me.gvTesis.Rows(e.CommandArgument).FindControl("ddlPresidente")
                Dim ddlSecre As DropDownList = Me.gvTesis.Rows(e.CommandArgument).FindControl("ddlSecretario")
                Dim ddlVocal As DropDownList = Me.gvTesis.Rows(e.CommandArgument).FindControl("ddlVocal")
                If ddlPresi.SelectedValue <> 0 And ddlSecre.SelectedValue <> 0 And ddlVocal.SelectedValue <> 0 Then
                    Dim mensaje As String = ""
                    Dim dt As New Data.DataTable
                    'Actualizar Jurado
                    dt = ActualizarJurado(codigo_tes, ddlPresi.SelectedValue, ddlSecre.SelectedValue, ddlVocal.SelectedValue, Session("id_per"))
                    If dt.Rows(0).Item("Respuesta").ToString = "1" Then
                        'mensaje += dt.Rows(0).Item("Mensaje").ToString
                        'Dar conformidad a Jurado
                        dt = ConformidadJurado(codigo_tes, Session("id_per"))
                        If dt.Rows(0).Item("Respuesta").ToString = "1" Then ' CONFORMIDAD OK
                            mensaje += "<br>" + dt.Rows(0).Item("Mensaje").ToString
                            'ACTUALIZAR ETAPA DE TRAMITE
                            Dim codigo_dta As Integer = Me.gvTesis.DataKeys(e.CommandArgument).Values("codigo_dta")
                            Dim dttramite As New Data.DataTable
                            dttramite = ActualizarEtapaTramite(codigo_dta, "1", "A")
                            If dttramite.Rows.Count > 0 Then ' SI HAY RESPUESTA DEL CAMBIO DE ETAPA
                                If dttramite.Rows(0).Item("revision") = True Then ' SI SE HIZO LA REVISIÓN
                                    If dttramite.Rows(0).Item("email") = True Then 'SI SE ENVIO EL CORREO(INSER´CIÓN CORREOMASIVO)
                                        mensaje += "<br>Se actualizó Etapa de trámite correctamente"
                                    Else  ' NO SE INSERTO ENVIO CORREO MASIVO
                                        mensaje += "<br>Se actualizó Etapa de trámite correctamente, pero no se pudo realizar el envío de correo correctamente"
                                    End If
                                Else ' NO SE ACTUALIZO LA ETAPA DEL TRAMITE
                                    mensaje = mensaje + "<br>No se pudo realizar la actualización de la etapa del trámite"
                                End If
                            Else
                                mensaje += "<br>No se pudo realizar la actualización de la etapa del trámite"
                            End If

                            ListarDocentes("EJT", "", "")
                            ListarTramites(Session("id_per"), Request("ctf"), Me.ddlEstado.SelectedValue)
                            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "valida", "fnMensaje('success','" + mensaje + "');", True)
                            EnviarNotificacionJurado(Session("id_per"), Request("ctf"), 47, codigo_tes)

                        Else 'SI NO SE DIÓ CONFORMIDAD CORRECTAMENTE
                            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "valida", "fnMensaje('error','" + dt.Rows(0).Item("Mensaje").ToString + "');", True)
                        End If
                    Else ' SI NO SE ACTUALIZARON LOS JURADOS
                        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "valida", "fnMensaje('error','" + dt.Rows(0).Item("Mensaje").ToString + "');", True)
                    End If
                Else ' SI NO TIENE LOS 3 JURADOS SELECCIONADOS
                    ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "valida", "fnMensaje('error','Debe seleccionar los 3 jurados');", True)
                End If
                Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "Loading", "fnLoading(false)", True)

            End If

        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "valida", "fnMensaje('error','" + ex.Message.ToString + "');", True)

        End Try

    End Sub

    Private Function ActualizarEtapaTramite(ByVal codigo_dta As Integer, ByVal tipooperacion As String, ByVal estadoaprobacion As String) As Data.DataTable
        Try
            Dim cmp As New clsComponenteTramiteVirtualCVE
            Dim objcmp As New List(Of Dictionary(Of String, Object))()
            cmp._codigo_dta = codigo_dta
            'cmp.tipoOperacion = "1"
            cmp.tipoOperacion = tipooperacion
            cmp._codigo_per = Session("id_per")
            cmp._codigo_tfu = 9 ' tipo funcion director de escuela
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

    Private Sub EnviarNotificacionJurado(ByVal codigo_per_emisor As Integer, ByVal codigo_tfu_emisor As Integer, ByVal codigo_apl As Integer, ByVal codigo_Tes As Integer)
        Try
            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            Dim dt As New Data.DataTable
            dt = obj.TraerDataTable("SUST_DatosNotificacionJurados", codigo_Tes)
            obj.CerrarConexion()

            If dt.Rows.Count > 0 Then

                Dim codigo_envio As Integer = ClsComunicacionInstitucional.ObtenerCodigoEnvio(codigo_per_emisor, codigo_tfu_emisor, codigo_apl)
                Dim correo_destino As String = ""
                Dim mensaje As String = ""
                Dim bandera As Integer = 0

                For i As Integer = 0 To dt.Rows.Count - 1
                    If ConfigurationManager.AppSettings("CorreoUsatActivo") = 1 Then
                        correo_destino = dt.Rows(i).Item("correo")
                    Else
                        correo_destino = "hcano@usat.edu.pe"
                    End If
                    If ClsComunicacionInstitucional.EnviarNotificacionEmail(codigo_envio, "SUST", "ASJU", "1", codigo_per_emisor, "codigo_per", dt.Rows(i).Item("codigo_per"), codigo_apl, correo_destino, "", "ASIGNACIÓN DE JURADO DE SUSTENTACIÓN DE TESIS", "", dt.Rows(i).Item("escuela"), dt.Rows(i).Item("nombrejurado"), dt.Rows(i).Item("descripcion_Tpi"), dt.Rows(i).Item("Titulo_Tes"), dt.Rows(i).Item("autores")) Then
                        mensaje += "<br>Notificación enviada al jurado: " + dt.Rows(i).Item("nombrejurado")

                    Else
                        mensaje += "<br>No se pudo enviar notificación al jurado: " + dt.Rows(i).Item("nombrejurado")
                        bandera = bandera + 1
                    End If
                Next
                If bandera = 0 Then
                    ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "valida1", "fnMensaje('success','" + mensaje + "');", True)
                Else
                    ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "valida2", "fnMensaje('error','" + mensaje + "');", True)
                End If
            End If
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "valida3", "fnMensaje('error','Operación no se ejecuto correctamente');", True)
        End Try

    End Sub

    Private Function ActualizarJurado(ByVal codigo_Tes As Integer, ByVal codigoper_p As Integer, ByVal codigoper_s As Integer, ByVal codigoper_v As Integer, ByVal usuario As Integer) As Data.DataTable
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Dim dt As New Data.DataTable
        dt = obj.TraerDataTable("SUST_ActualizarJuradosTesis", codigo_Tes, codigoper_p, codigoper_s, codigoper_v, usuario)
        obj.CerrarConexion()
        Return dt
    End Function

    Private Function ConformidadJurado(ByVal codigo_Tes As Integer, ByVal usuario As Integer) As Data.DataTable
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Dim dt As New Data.DataTable
        dt = obj.TraerDataTable("SUST_ConfirmarJurados", codigo_Tes, usuario)
        obj.CerrarConexion()
        Return dt
    End Function

    Protected Sub gvTesis_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvTesis.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim ddlPresi As DropDownList = e.Row.FindControl("ddlPresidente")
            ddlPresi.DataSource = dtpersonal
            ddlPresi.DataValueField = "codigo"
            ddlPresi.DataTextField = "descripcion"
            ddlPresi.DataBind()
            ddlPresi.SelectedValue = Me.gvTesis.DataKeys(e.Row.RowIndex).Values("jpresidente")

            Dim ddlSecre As DropDownList = e.Row.FindControl("ddlSecretario")
            ddlSecre.DataSource = dtpersonal
            ddlSecre.DataValueField = "codigo"
            ddlSecre.DataTextField = "descripcion"
            ddlSecre.DataBind()
            ddlSecre.SelectedValue = Me.gvTesis.DataKeys(e.Row.RowIndex).Values("jsecretario")

            Dim ddlVocal As DropDownList = e.Row.FindControl("ddlVocal")
            ddlVocal.DataSource = dtpersonal
            ddlVocal.DataValueField = "codigo"
            ddlVocal.DataTextField = "descripcion"
            ddlVocal.DataBind()
            ddlVocal.SelectedValue = Me.gvTesis.DataKeys(e.Row.RowIndex).Values("jvocal")
            If Me.ddlEstado.SelectedValue = "A" Then
                Me.gvTesis.Columns(4).Visible = False
            Else
                Me.gvTesis.Columns(4).Visible = True
            End If
        End If
       
    End Sub



    Protected Sub ddlEstado_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlEstado.SelectedIndexChanged
        If (Session("id_per") Is Nothing) Then
            Response.Redirect("../../../sinacceso.html")
        End If
        ListarDocentes("EJT", "", "")
        ListarTramites(Session("id_per"), Request("ctf"), Me.ddlEstado.SelectedValue)
        Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "Loading", "fnLoading(false)", True)

    End Sub
End Class

