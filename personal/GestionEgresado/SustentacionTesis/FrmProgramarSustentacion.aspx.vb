Imports System.Data
Imports System.IO
Imports System.Collections.Generic

Partial Class FrmProgramarSustentacion
    Inherits System.Web.UI.Page
    Dim ruta As String = ConfigurationManager.AppSettings("SharedFiles")

    Private Sub ConsultarTesis(ByVal codigo_per As Integer, ByVal codigo_ctf As Integer, ByVal estado As String)
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Dim dt As New Data.DataTable
        dt = obj.TraerDataTable("SUST_ListarProgramacionSustentacion", codigo_per, codigo_ctf, estado)
        If dt.Rows.Count > 0 Then
            Me.gvTesis.DataSource = dt
            Me.gvTesis.DataBind()
        Else
            Me.gvTesis.DataSource = Nothing
            Me.gvTesis.DataBind()
        End If
        obj.CerrarConexion()
    End Sub

    Private Sub ConsultarJuradoTesis(ByVal codigo_tes As Integer)
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Dim dt As New Data.DataTable
        dt = obj.TraerDataTable("SUST_DatosJuradoSustentacion", codigo_tes)
        If dt.Rows.Count > 0 Then
            Me.gvJurado.DataSource = dt
            Me.gvJurado.DataBind()
        Else
            Me.gvJurado.DataSource = Nothing
            Me.gvJurado.DataBind()
        End If
        obj.CerrarConexion()
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (Session("id_per") Is Nothing) Then
            Response.Redirect("../../../sinacceso.html")
        End If
        Try
            If IsPostBack = False Then
                ConsultarTesis(Session("id_per"), Request("ctf"), Me.ddlEstado.SelectedValue)
            End If
        Catch ex As Exception
            Response.Write(ex.Message.ToString)
        End Try
    End Sub


    Protected Sub ddlEstado_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlEstado.SelectedIndexChanged
        If (Session("id_per") Is Nothing) Then
            Response.Redirect("../../../sinacceso.html")
        End If
        Try
            ConsultarTesis(Session("id_per"), Request("ctf"), Me.ddlEstado.SelectedValue)
            Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "LoadingD", "fnLoading(false)", True)
            Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "LoadingEstado", "LoadingEstado();", True)

        Catch ex As Exception
            Response.Write(ex.Message.ToString)
        End Try
    End Sub


    Private Sub ConsultarDatosTesis(ByVal codigo_tes As Integer)
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Dim dt As New Data.DataTable
        dt = obj.TraerDataTable("SUST_ConsultarDatosTesis", codigo_tes)
        If dt.Rows.Count > 0 Then
            Dim str As String = ""
            For i As Integer = 0 To dt.Rows.Count - 1
                str += "<div class='form-group'>"
                str += "<label class='col-sm-3 col-md-3 control-label'>Código universitario</label>"
                str += "<div class='col-sm-3 col-md-2'>"
                str += "<input type='text' class='form-control' value='" + dt.Rows(i).Item("codigoUniver_Alu").ToString + "' readonly='readonly' >"
                str += "</div>"
                str += "<label class='col-sm-1 col-md-1 control-label'>Bachiller</label>"
                str += "<div class='col-sm-5 col-md-6'>"
                str += "<input type='text' class='form-control' value='" + dt.Rows(i).Item("alumno").ToString + "' readonly='readonly' >"
                str += "</div>"
                str += "</div>"
                str += "<div class='form-group'>"
                str += "<label class='col-sm-3 col-md-3 control-label'>Teléfono</label>"
                str += "<div class='col-sm-5 col-md-5'>"
                str += "<input type='text' class='form-control' value='" + dt.Rows(i).Item("telefonomovil").ToString + " / " + dt.Rows(i).Item("telefono").ToString + " / " + dt.Rows(i).Item("telefonocasa").ToString + " ' readonly='readonly' >"
                str += "</div>"
                str += "</div>"
                str += "<div class='form-group'>"
                str += "<label class='col-xs-3 col-sm-3 col-md-3 control-label'>Correo electrónico</label>"
                str += "<div class='col-sm-5 col-md-5'>"
                str += "<input type='text' class='form-control' value='" + dt.Rows(i).Item("correoalumno").ToString + " ' readonly='readonly' >"
                str += "</div>"
                str += "</div>"
            Next
            Me.alumnos.InnerHtml = str
            Me.txtTitulo.Text = dt.Rows(0).Item("Titulo_Tes").ToString
            Me.txtCarrera.Text = dt.Rows(0).Item("nombre_cpf").ToString
            Me.txtfechainforme.Text = dt.Rows(0).Item("fechaconformidadasesor").ToString
            Me.txtCarrera.Text = dt.Rows(0).Item("nombre_cpf").ToString
        End If
        obj.CerrarConexion()
    End Sub

    Protected Sub gvTesis_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvTesis.RowCommand
        If (Session("id_per") Is Nothing) Then
            Response.Redirect("../../../sinacceso.html")
        End If
        Try
            If (e.CommandName = "Programar") Then

                Me.hdTipoProg.Value = Me.gvTesis.DataKeys(e.CommandArgument).Values("tipoprogramacion")

                Me.Lista.Visible = False
                Me.DivProgramacion.Visible = True
                Me.hddta.Value = Me.gvTesis.DataKeys(e.CommandArgument).Values("codigo_dta")
                Me.hdfac.Value = Me.gvTesis.DataKeys(e.CommandArgument).Values("codigo_fac")
                Me.hdtes.Value = Me.gvTesis.DataKeys(e.CommandArgument).Values("codigo_Tes")
                Dim codigo_pst As Integer = 0
                codigo_pst = Me.gvTesis.DataKeys(e.CommandArgument).Values("codigo_pst")
                Me.hdpst.Value = codigo_pst
                ConsultarDatosTesis(Me.hdtes.Value)
                ConsultarJuradoTesis(Me.hdtes.Value)

                Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "Calendario()", True)
                If codigo_pst = 0 Then ' Nuevo
                    Limpiar()
                Else ' Edición
                    Limpiar()
                    Me.txtFecha.Enabled = False
                    Me.txtFecha.Text = Me.gvTesis.DataKeys(e.CommandArgument).Values("fecha")

                    Dim tipoambiente As String = Me.gvTesis.DataKeys(e.CommandArgument).Values("tipoambiente")
                    Me.ddlTipoAmbiente.SelectedValue = tipoambiente
                    If tipoambiente = "FÍSICO" Then
                        Me.DivFisico.Visible = True
                        Me.divDatosAmbiente.Visible = True
                        Me.DivVirtual.Visible = False
                        ListarAmbientes(Me.gvTesis.DataKeys(e.CommandArgument).Values("fecha"))
                    Else
                        Me.DivFisico.Visible = False
                        Me.DivVirtual.Visible = True
                        Me.divDatosAmbiente.Visible = True
                    End If
                    Me.ddlAmbiente.SelectedValue = Me.gvTesis.DataKeys(e.CommandArgument).Values("codigo_amb")
                    Me.txtAmbienteVirtual.Text = Me.gvTesis.DataKeys(e.CommandArgument).Values("link")
                    Me.txtDetalle.Text = Me.gvTesis.DataKeys(e.CommandArgument).Values("detalle")
                    If Me.gvTesis.DataKeys(e.CommandArgument).Values("archivoresolucion") = 0 Then
                        Me.btnGuardar.Visible = True
                        Me.ddlTipoAmbiente.Enabled = True
                        Me.ddlAmbiente.Enabled = True
                        Me.txtAmbienteVirtual.Enabled = True
                        Me.txtDetalle.Enabled = True

                    Else
                        Me.ddlTipoAmbiente.Enabled = False
                        Me.ddlAmbiente.Enabled = False
                        Me.txtAmbienteVirtual.Enabled = False
                        Me.txtDetalle.Enabled = False
                        Me.btnGuardar.Visible = False
                    End If
                End If
                Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "LoadingD", "fnLoading(false)", True)

            End If
        Catch ex As Exception
            Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "Errorx", "fnMensaje('error','" + ex.Message.ToString + "')", True)

        End Try

    End Sub

    Protected Sub btnCerrar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCerrar.Click
        Me.Lista.Visible = True
        Me.DivProgramacion.Visible = False
        Limpiar()
        Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "LoadingD", "fnLoading(false)", True)
        Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "LoadingEstado", "LoadingEstado();", True)

    End Sub

    Protected Sub ddlTipoAmbiente_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlTipoAmbiente.SelectedIndexChanged
        Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "calendario", "Calendario()", True)
        If Me.txtFecha.Text <> "" Then
            If Me.ddlTipoAmbiente.SelectedValue = "FÍSICO" Then
                Me.DivFisico.Visible = True
                Me.divDatosAmbiente.Visible = True
                Me.DivVirtual.Visible = False
                ListarAmbientes(Me.txtFecha.Text)
                Me.txtFecha.Enabled = False

            End If
            If Me.ddlTipoAmbiente.SelectedValue = "VIRTUAL" Then
                Me.ddlAmbiente.SelectedValue = 0
                Me.DivFisico.Visible = False
                Me.DivVirtual.Visible = True
                Me.divDatosAmbiente.Visible = True

                Me.txtFecha.Enabled = False

            End If
            If Me.ddlTipoAmbiente.SelectedValue = "" Then
                Me.ddlAmbiente.SelectedValue = 0
                Me.txtDetalle.Text = ""
                Me.txtAmbienteVirtual.Text = ""
                Me.DivFisico.Visible = False
                Me.DivVirtual.Visible = False
                Me.divDatosAmbiente.Visible = False
                Me.txtFecha.Enabled = True

            End If
        Else
            Me.ddlTipoAmbiente.SelectedValue = ""
            Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "fnMensaje('error','Seleccionar Fecha de programación de sustentación')", True)
        End If

    End Sub


    Private Function ListarAmbientes(ByVal fecha As String) As Data.DataTable
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Dim dt As New Data.DataTable
        dt = obj.TraerDataTable("SUST_VerificarDisponibilidadAmbiente", fecha)
        Me.ddlAmbiente.Items.Clear()
        Me.ddlAmbiente.Items.Add(New ListItem("[ -- Seleccione --]", "0"))
        If dt.Rows.Count > 0 Then
            For i As Integer = 0 To dt.Rows.Count - 1
                Me.ddlAmbiente.Items.Add(New ListItem(dt.Rows(i).Item("descripcionReal_Amb"), dt.Rows(i).Item("codigo_Amb")))
            Next
        End If
        obj.CerrarConexion()
        Return dt
    End Function

    Private Function RegistrarProgramacion(ByVal codigo_pst As Integer, ByVal codigo_dta As Integer, ByVal codigo_Tes As Integer, ByVal tipoprogramacion As String, ByVal fechaprogramacion As String, ByVal tipoambiente As String, ByVal codigo_ambiente As Integer, ByVal link_ambiente As String, ByVal detalle As String, ByVal codigo_per As Integer) As Data.DataTable
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Dim dt As New Data.DataTable
        dt = obj.TraerDataTable("SUST_RegistrarProgramacionSustentacion", codigo_pst, codigo_dta, codigo_Tes, tipoprogramacion, fechaprogramacion, tipoambiente, codigo_ambiente, link_ambiente, detalle, codigo_per)
        obj.CerrarConexion()
        Return dt
    End Function

    Protected Sub btnGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardar.Click
        If (Session("id_per") Is Nothing) Then
            Response.Redirect("../../../sinacceso.html")
        End If
        Try
            Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "calendario", "Calendario()", True)

            If ValidarProgramacion() = True Then
                Dim dt As New Data.DataTable
                dt = RegistrarProgramacion(Me.hdpst.Value, Me.hddta.Value, Me.hdtes.Value, Me.hdTipoProg.Value, Me.txtFecha.Text, Me.ddlTipoAmbiente.SelectedValue, Me.ddlAmbiente.SelectedValue, Me.txtAmbienteVirtual.Text, Me.txtDetalle.Text, Session("id_per"))
                If dt.Rows.Count > 0 Then
                    If dt.Rows(0).Item("Respuesta") = 1 Then
                        If Me.hdpst.Value = 0 Then ' si es nueva programación
                            RegistrarSolicitud(dt.Rows(0).Item("cod"), Me.hdtes.Value, Session("id_per"), Me.hdfac.Value)
                        End If
                        Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "fnMensaje('success','" + dt.Rows(0).Item("Mensaje").ToString + "')", True)
                        ConsultarTesis(Session("id_per"), Request("ctf"), Me.ddlEstado.SelectedValue)
                        Me.Lista.Visible = True
                        Me.DivProgramacion.Visible = False
                        Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "Loadingx", "fnLoading(false);", True)
                        Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "LoadingEstado", "LoadingEstado();", True)

                    Else
                        Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "Loadingx", "fnLoading(false);", True)
                        Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "LoadingEstado", "LoadingEstado();", True)
                        Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "fnMensaje('error','" + dt.Rows(0).Item("Mensaje").ToString + "')", True)
                    End If
                Else
                    Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "fnMensaje('error','No se pudo guardar programación')", True)
                End If
            End If
        Catch ex As Exception
            Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "fnMensaje('error','No se pudo guardar programación:" + ex.Message.ToString + "')", True)
        End Try
    End Sub

    Private Function ValidarProgramacion() As Boolean
        If Me.txtFecha.Text = "" Then
            Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "fnMensaje('error','Seleccione una fecha')", True)
            Me.txtFecha.Focus()
            Return False
        End If
        If Me.ddlTipoAmbiente.SelectedValue = "" Then
            Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "alert1", "fnMensaje('error','Seleccione el tipo de ambiente')", True)
            Me.txtFecha.Focus()
            Return False
        End If
        If Me.ddlTipoAmbiente.SelectedValue = "FÍSICO" And Me.ddlAmbiente.SelectedValue = "0" Then
            Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "alert2", "fnMensaje('error','Seleccione el ambiente')", True)
            Me.ddlAmbiente.Focus()
            Return False

        End If
        If Me.ddlTipoAmbiente.SelectedValue = "VIRTUAL" And Me.txtAmbienteVirtual.Text = "" Then
            Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "alert3", "fnMensaje('error','Ingrese link de ambiente virtual')", True)
            Me.txtAmbienteVirtual.Focus()
            Return False

        End If
        Return True
    End Function

    Private Sub Limpiar()
        Me.txtFecha.Enabled = True
        Me.txtFecha.Text = ""
        Me.ddlTipoAmbiente.SelectedValue = ""
        Me.ddlAmbiente.SelectedValue = "0"
        Me.txtAmbienteVirtual.Text = ""
        Me.txtDetalle.Text = ""
        Me.divDatosAmbiente.Visible = False
        Me.DivFisico.Visible = False
        Me.DivVirtual.Visible = False
        Me.btnGuardar.Visible = True
    End Sub
    Private Sub RegistrarSolicitud(ByVal codigo_prog As Integer, ByVal codigo_Tes As Integer, ByVal codigo_per As Integer, ByVal codigo_Fac As Integer)
        Try
            'Dim codigo_sol As Integer = 0
            'codigo_sol = clsDocumentacion.GeneraSolicitudDocumento(3, codigo_Tes, "ResolucionSustentacion", codigo_per, codigo_Fac)

            Dim codigo_sol As Integer = 0  '--- codigo de solicitud que devuelve
            Dim abreviatura_tid As String = "RESO" '---fijo  --- 
            Dim abreviatura_doc As String = "SUST" '---fijo
            Dim abreviatura_are As String = ""
            If codigo_Fac = 21 Then
                abreviatura_are = "PGRA" '---fijo  --- PGRA para postgrado 
            Else
                abreviatura_are = "USAT" '---fijo  --- PGRA para pregrado,go, prof 
            End If
            Dim nombreArchivo As String = "ResolucionSustentacion"
            'Dim codigo_egr As String = ""   '-- codigo alumno en caso requiera el documento
            'Dim codigo_fac As Integer = "21"      '--- 21 para ubicar el secretario de facultad de posgrado 
            'Dim codigo_dta As Integer = 65583   '--codigo de trámite / para el campo referencia01 de la tabla doc_solicitaDocumentacion
            'Dim codigDatos As Integer = 8603  '-- en este caso codigo_tes
            ''''**** 1. SOLICITA DOCUMENTO*************************************************************************************
            codigo_sol = clsDocumentacion.GeneraSolicitudDocumentoXAbreviaturas(Me.hddta.Value, abreviatura_doc, abreviatura_tid, abreviatura_are, codigo_Tes, nombreArchivo, codigo_per, codigo_Fac)
            'Call mt_ShowMessage(codigo_sol, MessageType.success)

            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            Dim dt As New Data.DataTable
            dt = obj.TraerDataTable("SUST_ActualizarCodigoSolicitud", codigo_prog, codigo_sol)
            obj.CerrarConexion()
        Catch ex As Exception
            Response.Write(ex.Message.ToString)
        End Try
    End Sub

    'Protected Sub txtFecha_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtFecha.TextChanged
    '    Me.ddlTipoAmbiente.SelectedValue = ""
    '    ddlTipoAmbiente_SelectedIndexChanged(sender, e)
    'End Sub
End Class


