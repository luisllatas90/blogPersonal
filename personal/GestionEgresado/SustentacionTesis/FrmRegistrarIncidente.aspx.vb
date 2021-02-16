Imports System.Data
Imports System.IO
Imports System.Collections.Generic

Partial Class FrmRegistrarIncidente
    Inherits System.Web.UI.Page
    Dim ruta As String = ConfigurationManager.AppSettings("SharedFiles")
    Private Sub ConsultarTesis(ByVal codigo_per As Integer, ByVal codigo_ctf As Integer, ByVal estado As String)
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Dim dt As New Data.DataTable
        dt = obj.TraerDataTable("SUST_ListarSustentacionesIncidentes", codigo_per, codigo_ctf, estado)
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
        Try
            If IsPostBack = False Then
                ConsultarTesis(Session("id_per"), Request("ctf"), Me.ddlEstado.SelectedValue)
            End If
        Catch ex As Exception
            Response.Write(ex.Message.ToString)
        End Try
    End Sub

    Protected Sub gvTesis_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvTesis.RowCommand
        If (Session("id_per") Is Nothing) Then
            Response.Redirect("../../../sinacceso.html")
        End If
        Try
            If (e.CommandName = "RegistrarIncidente") Then
                Me.Lista.Visible = False
                Me.DivIncidente.Visible = True
                Me.hdPst.Value = Me.gvTesis.DataKeys(e.CommandArgument).Values("codigo_pst")
                Me.hdtes.Value = Me.gvTesis.DataKeys(e.CommandArgument).Values("codigo_Tes")
                Me.hddta.Value = Me.gvTesis.DataKeys(e.CommandArgument).Values("codigo_dta")
                Me.ddlAsistente.Items.Clear()
                Me.ddlAsistente.Items.Add(New ListItem("[-- Seleccione --]", ""))
                ConsultarDatosTesis(Me.hdtes.Value)
                ConsultarJuradoTesis(Me.hdtes.Value)
                Me.Lista.Visible = False
                Me.DivIncidente.Visible = True
                ListarIncidentes(Me.hdPst.Value)
                Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "Loading", "fnLoading(false)", True)
            End If

        Catch ex As Exception
            Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "fnMensaje('error','" + ex.Message.ToString + "')", True)
        End Try
        Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "LoadingEstado", "LoadingEstado();", True)

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
                Me.ddlAsistente.Items.Add(New ListItem(dt.Rows(i).Item("alumno").ToString, dt.Rows(i).Item("codigo_alu").ToString))
            Next
            Me.Alumnos.InnerHtml = str
            Me.txtTitulo.Text = dt.Rows(0).Item("Titulo_Tes").ToString
            Me.txtCarrera.Text = dt.Rows(0).Item("nombre_cpf").ToString
            Me.ddlAsistente.DataBind()
        End If
        obj.CerrarConexion()
    End Sub

    Private Sub ConsultarJuradoTesis(ByVal codigo_tes As Integer)
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Dim dt As New Data.DataTable
        dt = obj.TraerDataTable("SUST_DatosJuradoSustentacion", codigo_tes)
        obj.CerrarConexion()
        If dt.Rows.Count > 0 Then
            For i As Integer = 0 To dt.Rows.Count - 1
                Me.ddlAsistente.Items.Add(New ListItem(dt.Rows(i).Item("descripcion_Tpi").ToString + " - " + dt.Rows(i).Item("jurado").ToString, dt.Rows(i).Item("codigo_jur").ToString))
            Next
        End If
        Me.ddlAsistente.DataBind()
    End Sub

    Protected Sub btnCerrar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCerrar.Click
        If (Session("id_per") Is Nothing) Then
            Response.Redirect("../../../sinacceso.html")
        End If
        Me.Lista.Visible = True
        Me.DivIncidente.Visible = False
        ConsultarTesis(Session("id_per"), Request("ctf"), Me.ddlEstado.SelectedValue)
        Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "Loading", "fnLoading(false)", True)
        Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "LoadingEstado", "LoadingEstado();", True)

    End Sub

    Protected Sub btnGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardar.Click
        If (Session("id_per") Is Nothing) Then
            Response.Redirect("../../../sinacceso.html")
        End If
        Try
            If ValidarRegistroIncidente() = True Then
                Dim codigo_alu As Integer = 0
                Dim codigo_jur As Integer = 0
                'CUANDO ES JURADO
                If Me.ddlAsistente.SelectedItem.Text.Contains(" - ") Then
                    codigo_jur = Me.ddlAsistente.SelectedValue
                    'CUANDO EL INCIDENTE ES POR UN ALUMNO
                Else
                    codigo_alu = Me.ddlAsistente.SelectedValue
                End If
                RegistrarIncidente(Me.hdPst.Value, codigo_alu, codigo_jur, Me.txtdetalle.Text, Session("id_per"), Me.hddta.Value)
            End If
        Catch ex As Exception
            Response.Write(ex.Message.ToString)
        End Try
        Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "Loading", "fnLoading(false)", True)
        Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "LoadingEstado", "LoadingEstado();", True)

    End Sub

    Private Function ValidarRegistroIncidente() As Boolean
        If Me.ddlAsistente.SelectedValue = "" Then
            Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "valida", "fnMensaje('error','Seleccione asistente faltante')", True)
            Return False
        End If
        If Me.txtdetalle.Text.Trim = "" Then
            Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "valida", "fnMensaje('error','Ingrese detalle de incidente')", True)
            Return False
        End If
        Return True
    End Function

    Protected Sub ddlEstado_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlEstado.SelectedIndexChanged
        If (Session("id_per") Is Nothing) Then
            Response.Redirect("../../../sinacceso.html")
        End If
        Try
            ConsultarTesis(Session("id_per"), Request("ctf"), Me.ddlEstado.SelectedValue)
        Catch ex As Exception
            Response.Write(ex.Message.ToString)
        End Try
        Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "Loading", "fnLoading(false)", True)
        Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "LoadingEstado", "LoadingEstado();", True)

    End Sub

    Private Sub RegistrarIncidente(ByVal codigo_pst As Integer, ByVal codigo_alu As Integer, ByVal codigo_jur As Integer, ByVal detalle As String, ByVal usuario As Integer, ByVal codigo_dta As Integer)
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Dim dt As New Data.DataTable
        dt = obj.TraerDataTable("SUST_RegistrarIncidenteSustentacion", codigo_pst, codigo_alu, codigo_jur, detalle, usuario)
        obj.CerrarConexion()
        If dt.Rows.Count > 0 Then
            If dt.Rows(0).Item("Respuesta") = 1 Then
                Me.ddlAsistente.SelectedValue = ""
                Me.txtdetalle.Text = ""
                If dt.Rows(0).Item("RevierteEtapa").ToString = "1" Then
                    'ACTUALIZAR ETAPA DE TRAMITE
                    Dim dttramite As New Data.DataTable
                    dttramite = RevertirInstanciaTramite(codigo_dta, 1)
                    If dttramite.Rows.Count > 0 Then ' SI HAY RESPUESTA DEL CAMBIO DE ETAPA
                        If dttramite.Rows(0).Item("revision") = True Then ' SI SE HIZO LA REVISIÓN
                            Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "mensaje4", "fnMensaje('success','Se revirtió Etapa de trámite correctamente')", True)
                        Else ' NO SE ACTUALIZO LA ETAPA DEL TRAMITE
                            Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "mensaje4", "fnMensaje('success','No se pudo realizar la actualización de la etapa del trámite')", True)
                        End If
                    Else
                        Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "mensaje5", "fnMensaje('success','No se pudo realizar la actualización de la etapa del trámite')", True)
                    End If
                End If
                ListarIncidentes(Me.hdPst.Value)
                Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "mensaje2", "fnMensaje('success','" + dt.Rows(0).Item("Mensaje") + "')", True)
            Else
                Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "mensaje2", "fnMensaje('error','" + dt.Rows(0).Item("Mensaje") + "')", True)
            End If
        End If
    End Sub

    Private Sub ListarIncidentes(ByVal codigo_pst As Integer)
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Dim dt As New Data.DataTable
        dt = obj.TraerDataTable("SUST_ListarincidentesSustentacion", codigo_pst)
        obj.CerrarConexion()
        If dt.Rows.Count > 0 Then
            Me.gvIncidentes.DataSource = dt
        Else
            Me.gvIncidentes.DataSource = Nothing
        End If
        Me.gvIncidentes.DataBind()

    End Sub

    Private Function RevertirInstanciaTramite(ByVal codigo_dta As Integer, ByVal nroinstancias As Integer) As Data.DataTable
        Dim dt As New Data.DataTable
        dt.Columns.Add("revision")
        dt.Columns.Add("registros")
        dt.Columns.Add("email")
        Try
            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            Dim dtdatos As New Data.DataTable
            dtdatos = obj.TraerDataTable("SUST_DatosTramitesAutores", codigo_dta)
            obj.CerrarConexion()
            For i As Integer = 0 To dtdatos.Rows.Count - 1

                Dim cmp As New clsComponenteTramiteVirtualCVE
                Dim objcmp As New List(Of Dictionary(Of String, Object))()
                cmp._codigo_dta = codigo_dta
                cmp._codigo_dta = dtdatos.Rows(i).Item("codigo_dta")
                cmp._codigo_tfu = 251 'INSTANCIA EN LA QUE SE ENCUENTRA PENDIENTE
                cmp._numeroinstanciareversa_dft = nroinstancias ' Número de instancias que va a retornar
                cmp.tipoOperacion = 1
                objcmp = cmp.mt_EvaluarTramite()

                For Each fila As Dictionary(Of String, Object) In objcmp
                    dt.Rows.Add(fila.Item("evaluacion"), fila.Item("registos evaluados").ToString, fila.Item("email"))
                    'dt.Rows.Add(True, "ok", True)
                Next
            Next
            Return dt
        Catch ex As Exception
            dt.Rows.Add(False, "", False)
            Return dt
        End Try
    End Function

End Class

