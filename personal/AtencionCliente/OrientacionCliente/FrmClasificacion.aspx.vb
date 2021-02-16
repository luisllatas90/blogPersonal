
Partial Class OrientacionCliente_FrmClasificacion
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "Loading", "fnLoading(true)", True)

        If (Session("id_per") Is Nothing) Then
            Response.Redirect("../../../sinacceso.html")
        End If
        If IsPostBack = False Then
            Listar()
        End If
    End Sub

    Private Sub Listar()
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Dim dt As New Data.DataTable
        dt = obj.TraerDataTable("[OAC_ListarTipoOrientacion]")

        If dt.Rows.Count > 0 Then
            Me.gvLista.DataSource = dt
            Me.gvLista.DataBind()
        Else
            Me.gvLista.DataSource = Nothing
            Me.gvLista.DataBind()
        End If

        obj.CerrarConexion()
    End Sub

    Protected Sub lbNuevo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbNuevo.Click
        If (Session("id_per") Is Nothing) Then
            Response.Redirect("../../../sinacceso.html")
        End If
        Me.Lista.Visible = False
        Me.DivMantenimiento.Visible = True
        Me.txtDescripcion.Text = ""
        Me.txtDetalle.Text = ""
        Me.hdc.Value = 0
        Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "Loading", "fnLoading(false)", True)

    End Sub

    Protected Sub lbCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbCancelar.Click
        Me.Lista.Visible = True
        Me.DivMantenimiento.Visible = False
        Me.txtDescripcion.Text = ""
        Me.txtDetalle.Text = ""
        Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "Loading", "fnLoading(false)", True)

    End Sub


    Protected Sub gvLista_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvLista.RowCommand
        If (Session("id_per") Is Nothing) Then
            Response.Redirect("../../../sinacceso.html")
        End If
        Try
            If (e.CommandName = "Editar") Then
                Me.Lista.Visible = False
                Me.DivMantenimiento.Visible = True
                Me.hdc.Value = Me.gvLista.DataKeys(e.CommandArgument).Values("codigo_tio")
                Me.txtDescripcion.Text = Me.gvLista.DataKeys(e.CommandArgument).Values("nombre_tio")
                Me.txtDetalle.Text = Me.gvLista.DataKeys(e.CommandArgument).Values("descripcion_tio")
            End If
            If (e.CommandName = "Eliminar") Then
                Dim codigo As Integer = Me.gvLista.DataKeys(e.CommandArgument).Values("codigo_tio")
                EliminarTipo(codigo, Session("id_per"))
            End If
            Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "Loading", "fnLoading(false)", True)

        Catch ex As Exception

            Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "fnMensaje('error','" + ex.Message.ToString + "')", True)

        End Try
    End Sub

    Private Sub ActualizarTipo(ByVal codigo As Integer, ByVal descripcion As String, ByVal detalle As String, ByVal usuario As Integer)
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Dim dt As New Data.DataTable
        dt = obj.TraerDataTable("[OAC_ActualizarTipoOrientacion]", codigo, descripcion, detalle, usuario)
        obj.CerrarConexion()
        If dt.Rows(0).Item("Respuesta") = 1 Then
            Me.DivMantenimiento.Visible = False
            Me.Lista.Visible = True
            Listar()
            Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "alert1", "fnMensaje('success','" + dt.Rows(0).Item("Mensaje") + "')", True)
            Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "Loading1", "fnLoading(false)", True)
        Else
            Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "alert2", "fnMensaje('error','" + dt.Rows(0).Item("Mensaje") + "')", True)
        End If
    End Sub

    Protected Sub lbGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbGuardar.Click
        If (Session("id_per") Is Nothing) Then
            Response.Redirect("../../../sinacceso.html")
        End If
        If Me.txtDescripcion.Text.Trim = "" Then
            Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "alert2", "fnMensaje('error','Ingrese descripcion de la clasificación')", True)
        ElseIf Me.txtDetalle.Text.Trim = "" Then
            Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "alert5", "fnMensaje('error','Ingrese detalle de la clasificación')", True)
        Else

            ActualizarTipo(Me.hdc.Value, Me.txtDescripcion.Text, Me.txtDetalle.Text, Session("id_per"))
        End If
    End Sub


    Private Sub EliminarTipo(ByVal codigo As Integer, ByVal usuario As Integer)
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Dim dt As New Data.DataTable
        dt = obj.TraerDataTable("[OAC_EliminarTipoOrientacion]", codigo, usuario)
        obj.CerrarConexion()
        If dt.Rows(0).Item("Respuesta") = 1 Then
            Listar()
            Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "alert3", "fnMensaje('success','" + dt.Rows(0).Item("Mensaje") + "')", True)
            Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "Loading2", "fnLoading(false)", True)
        Else
            Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "alert4", "fnMensaje('error','" + dt.Rows(0).Item("Mensaje") + "')", True)
        End If
    End Sub


End Class

