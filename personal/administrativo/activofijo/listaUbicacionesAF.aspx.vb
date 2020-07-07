Imports System.Data

Partial Class administrativo_activofijo_listaUbicacionesAF
    Inherits System.Web.UI.Page

#Region "Declaracion de Variables"

    Public Enum MessageType
        Success
        [Error]
        Info
        Warning
    End Enum

#End Region

#Region "Eventos"

    Protected Sub btnBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBuscar.Click
        mt_CargarDatos()
    End Sub

    Protected Sub btnEditar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Page.RegisterStartupScript("Pop", "<script>openModal('" & "Editar" & "');</script>")
    End Sub

    Protected Sub btnAgregar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAgregar.Click
        Page.RegisterStartupScript("Pop", "<script>openModal('" & "Agregar" & "');</script>")
    End Sub

    Protected Sub btnEliminar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        ''Response.Write("<script>alert('" + Me.hdIdUbicacion.Value + "')</script>")
        'Dim obj As New ClsConectarDatos
        'obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        'Try
        '    obj.AbrirConexion()
        '    'If MsgBox("¿Desea Eliminar este Registro?", MsgBoxStyle.YesNo, "Mensaje de Sistema") = MsgBoxResult.Yes Then
        '    obj.Ejecutar("AF_eliminarUbicacionAF", Me.hdIdUbicacion.Value, Request.QueryString("Id"))
        '    'End If
        '    obj.CerrarConexion()
        '    mt_CargarDatos()
        'Catch ex As Exception
        '    'Response.Write(ex.Message & " " & ex.StackTrace)
        '    mt_ShowMessage("Error_CargaDatos(): " & ex.Message.Replace("'", ""), MessageType.Error)
        'Page.RegisterStartupScript("Confirm", "<script>showConfirm();</script>")
        'End Try
        'Page.RegisterStartupScript("Confirm", "<script>showConfirm();</script>")
        'Response.Write("<script>alert('" + Me.hdConfirmacion.Value + "')</script>")
    End Sub

    Protected Sub gvUbicacion_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvUbicacion.RowCommand
        Try
            Dim index As Integer
            index = CInt(e.CommandArgument)
            Me.hdIdUbicacion.Value = String.Empty
            Me.hdCodigoUbe.Value = String.Empty
            Me.hdDescripcion.Value = String.Empty
            Me.hdConfirmacion.Value = String.Empty
            If (e.CommandName = "Editar") Then
                Me.hdIdUbicacion.Value = gvUbicacion.DataKeys(index).Values("codigo_uba")
                Me.hdCodigoUbe.Value = gvUbicacion.DataKeys(index).Values("codigo_ube")
                Me.hdDescripcion.Value = gvUbicacion.DataKeys(index).Values("des_uba")
            End If
            If (e.CommandName = "Eliminar") Then
                Dim obj As New ClsConectarDatos
                obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
                Me.hdIdUbicacion.Value = gvUbicacion.DataKeys(index).Values("codigo_uba")
                obj.AbrirConexion()
                'If MsgBox("¿Desea Eliminar este Registro?", MsgBoxStyle.YesNo, "Mensaje de Sistema") = MsgBoxResult.Yes Then
                'Page.RegisterStartupScript("Confirm", "<script>showConfirm();</script>")
                'Response.Write("<script>alert('" + Me.hdConfirmacion.Value + "')</script>")
                obj.Ejecutar("AF_eliminarUbicacionAF", Me.hdIdUbicacion.Value, Request.QueryString("Id"))
                'End If
                obj.CerrarConexion()
                mt_CargarDatos()
            End If
            'Response.Write("<script>alert('" + Me.hdIdUbicacion.Value + "')</script>")
        Catch ex As Exception
            'Response.Write(ex.Message & " " & ex.StackTrace)
            mt_ShowMessage("Error_CargaDatos(): " & ex.Message.Replace("'", ""), MessageType.Error)
        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If IsPostBack = False Then
                mt_CargarCombo()
            End If
        Catch ex As Exception
            Response.Write(ex.Message & " " & ex.StackTrace)
        End Try
    End Sub

    Protected Sub btnGrabar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGrabar.Click
        'Response.Write("<script>alert('" + Me.cboUbicacion.SelectedValue + "')</script>")
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        Try
            'If Me.cboUbicacion.SelectedIndex < 0 Then
            '    Response.Write("<script>alert('Selecione una Ubicacion')</script>")
            '    Exit Sub
            'End If
            obj.AbrirConexion()
            If Me.hdAccion.Value = "Agregar" Then
                obj.Ejecutar("AF_registrarUbicacionAF", Me.cboUbicacion.SelectedValue, Me.txtDescripcion.Text, Request.QueryString("Id"))
            Else
                obj.Ejecutar("AF_actualizarUbicacionAF", Me.hdIdUbicacion.Value, Me.cboUbicacion.SelectedValue, Me.txtDescripcion.Text, Request.QueryString("Id"))
            End If
            obj.CerrarConexion()
            Me.hdIdUbicacion.Value = String.Empty
            mt_CargarDatos()
        Catch ex As Exception
            Response.Write(ex.Message & " " & ex.StackTrace)
        End Try
    End Sub

    Protected Sub cboUbicacion_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboUbicacion.SelectedIndexChanged
        Me.hdCodigoUbe.Value = Me.cboUbicacion.SelectedValue
    End Sub

#End Region

#Region "Metodos"

    Private Sub mt_CargarDatos()
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        Try
            obj.AbrirConexion()
            dt = obj.TraerDataTable("AF_listadoUbicacionAF_V2", Me.cboUbicacionBus.SelectedValue, Me.txtDescripcionBus.Text)
            obj.CerrarConexion()
            Me.gvUbicacion.DataSource = dt
            Me.gvUbicacion.DataBind()
        Catch ex As Exception
            mt_ShowMessage("Error_CargaDatos(): " & ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub mt_ShowMessage(ByVal Message As String, ByVal type As MessageType)
        Page.RegisterStartupScript("Mensaje", "<script>ShowMessage('" & Message & "','" & type.ToString & "');</script>")
    End Sub

    Private Sub mt_CargarCombo()
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        Dim dt2 As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        Try
            obj.AbrirConexion()
            dt = obj.TraerDataTable("ACAD_ListaUbicacionEdificio_v3")
            dt2 = obj.TraerDataTable("ACAD_ListaUbicacionEdificio_v4")
            obj.CerrarConexion()
            ClsFunciones.LlenarListas(Me.cboUbicacion, dt, "codigo_ube", "descripcion_ube")
            ClsFunciones.LlenarListas(Me.cboUbicacionBus, dt2, "codigo_ube", "descripcion_ube")
        Catch ex As Exception
            mt_ShowMessage("Error_CargaDatos(): " & ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

#End Region
 
End Class
