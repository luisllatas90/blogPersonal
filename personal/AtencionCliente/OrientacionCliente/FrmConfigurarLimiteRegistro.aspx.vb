
Partial Class OrientacionCliente_FrmConfigurarLimiteRegistro
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
        dt = obj.TraerDataTable("[OAC_ListarConfiguracionLimites]")

        If dt.Rows.Count > 0 Then
            Me.gvLista.DataSource = dt
            Me.gvLista.DataBind()
        Else
            Me.gvLista.DataSource = Nothing
            Me.gvLista.DataBind()
        End If

        obj.CerrarConexion()
    End Sub

    'Protected Sub lbNuevo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbNuevo.Click
    '    Me.Lista.Visible = False
    '    Me.DivMantenimiento.Visible = True
    '    Me.txtDescripcion.Text = ""
    '    Me.hdc.Value = 0
    '    Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "Loading", "fnLoading(false)", True)

    'End Sub

    Protected Sub lbCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbCancelar.Click
        Me.Lista.Visible = True
        Me.DivMantenimiento.Visible = False
        Me.txtDescripcion.Text = ""
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
                Me.hdc.Value = Me.gvLista.DataKeys(e.CommandArgument).Values("codigo_cla")
                Me.txtDescripcion.Text = Me.gvLista.DataKeys(e.CommandArgument).Values("nombre_cla")
                Me.txtCantidad.Text = Me.gvLista.DataKeys(e.CommandArgument).Values("limite_cla")
            End If
            'If (e.CommandName = "Eliminar") Then
            '    Dim codigo As Integer = Me.gvLista.DataKeys(e.CommandArgument).Values("codigo_ooc")
            '    EliminarTipo(codigo, Session("id_per"))
            'End If
            Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "Loading", "fnLoading(false)", True)

        Catch ex As Exception

            Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "fnMensaje('error','" + ex.Message.ToString + "')", True)

        End Try
    End Sub

    Private Sub ActualizarTipo(ByVal codigo As Integer, ByVal descripcion As String, ByVal limite As Integer, ByVal usuario As Integer)
        Try
            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            Dim dt As New Data.DataTable
            dt = obj.TraerDataTable("[OAC_ActualizarConfiguracionLimites]", codigo, descripcion, limite, usuario)
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
        Catch ex As Exception
            Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "alert44", "fnMensaje('error','" + ex.Message.ToString + "')", True)

        End Try
    End Sub

    Protected Sub lbGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbGuardar.Click
        If (Session("id_per") Is Nothing) Then
            Response.Redirect("../../../sinacceso.html")
        End If
        If Me.txtDescripcion.Text.Trim = "" Then
            Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "alert2", "fnMensaje('error','Ingrese nombre del canal de recepción');fnLoading(false);", True)
        ElseIf Me.txtCantidad.Text.Trim = "" Then
            Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "alert24", "fnMensaje('error','Ingrese cantidad de días');fnLoading(false);", True)
        Else
            If ValidaSoloNumeros(Me.txtCantidad.Text.Trim) = True Then
                ActualizarTipo(Me.hdc.Value, Me.txtDescripcion.Text, Me.txtCantidad.Text, Session("id_per"))
            Else
                Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "alert33", "fnMensaje('error','solo puede ingresar valores numericos como cantidad');fnLoading(false);", True)
            End If
        End If
    End Sub

    Function ValidaSoloNumeros(ByVal cadena As String) As Boolean
        Try
            Dim estructura As String = "^[0-9]*$"
            Dim match As Match = Regex.Match(cadena.Trim(), estructura, RegexOptions.IgnoreCase)
            If match.Success Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Return False
        End Try
    End Function

    'Private Sub EliminarTipo(ByVal codigo As Integer, ByVal usuario As Integer)
    '    Dim obj As New ClsConectarDatos
    '    obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
    '    obj.AbrirConexion()
    '    Dim dt As New Data.DataTable
    '    dt = obj.TraerDataTable("[OAC_EliminarTipoOrientacion]", codigo, usuario)
    '    obj.CerrarConexion()
    '    If dt.Rows(0).Item("Respuesta") = 1 Then
    '        Listar()
    '        Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "alert3", "fnMensaje('success','" + dt.Rows(0).Item("Mensaje") + "')", True)
    '        Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "Loading2", "fnLoading(false)", True)
    '    Else
    '        Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "alert4", "fnMensaje('error','" + dt.Rows(0).Item("Mensaje") + "')", True)
    '    End If
    'End Sub


End Class

