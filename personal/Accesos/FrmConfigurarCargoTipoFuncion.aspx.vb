Imports System.Data
Partial Class FrmConfigurarCargoTipoFuncion
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                ListarCargos()
                ListarTipoFuncion()
                ListarAplicaciones()
            End If
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "Busqueda", "initCombo('ddlCargo');", True)
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "Busqueda", "initCombo('ddlTipoFuncion');", True)
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "Busqueda", "initCombo('ddlAplicacion');", True)
        Catch ex As Exception
            Response.Write(ex.Message.ToString)
        End Try
    End Sub

    Private Sub ListarCargos()
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Dim tb As New Data.DataTable
        tb = obj.TraerDataTable("ListarCargos", "TO")
        Me.ddlCargo.Items.Add(New ListItem("<<Seleccione>>", ""))
        If tb.Rows.Count > 0 Then
            For i As Integer = 0 To tb.Rows.Count - 1
                Me.ddlCargo.Items.Add(New ListItem(tb.Rows(i).Item("descripcion_cgo"), tb.Rows(i).Item("codigo_cgo")))
            Next
        End If
        obj.CerrarConexion()
    End Sub

    Private Sub ListarTipoFuncion()
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Dim tb As New Data.DataTable
        tb = obj.TraerDataTable("ListarTipoFuncion", "TO")
        Me.ddlTipoFuncion.Items.Add(New ListItem("<<Seleccione>>", ""))
        If tb.Rows.Count > 0 Then
            For i As Integer = 0 To tb.Rows.Count - 1
                Me.ddlTipoFuncion.Items.Add(New ListItem(tb.Rows(i).Item("descripcion_Tfu"), tb.Rows(i).Item("codigo_Tfu")))
            Next
        End If
        obj.CerrarConexion()
    End Sub

    Private Sub ListarAplicaciones()
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Dim tb As New Data.DataTable
        tb = obj.TraerDataTable("ConsultarAplicacionUsuario", "1", 0, 0, 0)
        Me.ddlAplicacion.Items.Add(New ListItem("<<Seleccione>>", ""))
        If tb.Rows.Count > 0 Then
            For i As Integer = 0 To tb.Rows.Count - 1
                Me.ddlAplicacion.Items.Add(New ListItem(tb.Rows(i).Item("descripcion_apl"), tb.Rows(i).Item("codigo_apl")))
            Next
        End If
        obj.CerrarConexion()
    End Sub

    Protected Sub ddlCargo_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlCargo.SelectedIndexChanged
        Try
            If Me.ddlCargo.SelectedValue <> "" Then
                ListarPersonal(Me.ddlCargo.SelectedValue)
                ListarConfiguraciónCargoAplicacionTipoFuncion(Me.ddlCargo.SelectedValue)
            Else
                Me.gvPersonal.DataSource = ""
                Me.gvPersonal.DataBind()
                Me.gvConfiguracion.DataSource = ""
                Me.gvConfiguracion.DataBind()
            End If
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "Busqueda", "initCombo('ddlCargo');", True)
        Catch ex As Exception
            Response.Write(ex.Message.ToString)
        End Try
    End Sub

    Private Sub ListarPersonal(ByVal codigo_cgo As Integer)
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Dim tb As New Data.DataTable
        tb = obj.TraerDataTable("ListarPersonalxCargo", codigo_cgo)

        Me.gvPersonal.DataSource = tb
        Me.gvPersonal.DataBind()

        obj.CerrarConexion()
    End Sub

    Private Sub ListarConfiguraciónCargoAplicacionTipoFuncion(ByVal codigo_cgo As Integer)
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Dim tb As New Data.DataTable
        tb = obj.TraerDataTable("ListarConfiguracionCargoAplicacionTipoFuncion", codigo_cgo)

        Me.gvConfiguracion.DataSource = tb
        Me.gvConfiguracion.DataBind()

        obj.CerrarConexion()
    End Sub

    Protected Sub btnAgregar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAgregar.Click
        Try
            If validar() = True Then
                Dim dt As New Data.DataTable
                dt = Guardar(Me.ddlCargo.SelectedValue, Me.ddlAplicacion.SelectedValue, Me.ddlTipoFuncion.SelectedValue, Session("id_per"))
                'Response.Write("alert('" + dt.Rows(0).Item("msje").ToString + "')")
                If dt.Rows(0).Item("rpta").ToString = "1" Then
                    ListarConfiguraciónCargoAplicacionTipoFuncion(Me.ddlCargo.SelectedValue)
                End If
            End If
        Catch ex As Exception
            Response.Write(ex.Message.ToString)
        End Try
    End Sub

    Private Function validar() As Boolean
        If Me.ddlCargo.SelectedValue = "" Then
            Return False
        End If
        If Me.ddlAplicacion.SelectedValue = "" Then
            Return False
        End If
        If Me.ddlTipoFuncion.SelectedValue = "" Then
            Return False
        End If
        Return True
    End Function

    Private Function Guardar(ByVal codigo_cgo As Integer, ByVal codigo_apl As Integer, ByVal codigo_tfu As Integer, ByVal codigo_per As Integer) As Data.DataTable
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Dim tb As New Data.DataTable
        tb = obj.TraerDataTable("RegistrarConfiguracionCargoAplicacionTipoFuncion", codigo_cgo, codigo_apl, codigo_tfu, codigo_per)
        Return tb
        obj.CerrarConexion()
    End Function

    Protected Sub gvConfiguracion_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvConfiguracion.RowCommand
        If e.CommandName = "Eliminar" Then
            Dim dt As New Data.DataTable
            EliminarRegistro(Me.gvConfiguracion.DataKeys(e.CommandArgument).Values("codigo_caf"), Session("id_per"))
            ListarConfiguraciónCargoAplicacionTipoFuncion(Me.ddlCargo.SelectedValue)
        End If
    End Sub


    Private Sub EliminarRegistro(ByVal codigo As Integer, ByVal codigo_per As Integer)
        Try
            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            Dim tb As New Data.DataTable
            tb = obj.TraerDataTable("EliminarConfiguracionCargoAplicacionTipoFuncion", codigo, codigo_per)
            If tb.Rows.Count > 0 Then
                ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "confirmaciónVinculo", "alert('" + tb.Rows(0).Item("msje").ToString + "')", True)
            End If
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "confirmaciónVinculo", "alert('No se pudo eliminar registro')", True)
        End Try

    End Sub
End Class

