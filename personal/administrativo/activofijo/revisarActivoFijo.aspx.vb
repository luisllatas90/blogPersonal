
Partial Class administrativo_activofijo_revisarActivoFijo
    Inherits System.Web.UI.Page

#Region "Declaraciones de Variables"

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

    Protected Sub cboEstado_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboEstado.SelectedIndexChanged
        Me.txtNroPedido.Text = String.Empty
        If Me.cboEstado.SelectedValue = "0" Then
            Me.txtNroPedido.Enabled = True
        Else
            Me.txtNroPedido.Enabled = False
        End If
    End Sub

    Protected Sub btnAutorizar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Page.RegisterStartupScript("Pop", "<script>openModal();</script>")
    End Sub

    Protected Sub btnConfirmar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Page.RegisterStartupScript("Pop", "<script>openModal();</script>")
    End Sub

    Protected Sub gvTraslado_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvTraslado.RowCommand
        Try
            Dim index As Integer
            index = CInt(e.CommandArgument)
            Me.hdTraslado.Value = gvTraslado.DataKeys(index).Values("codigo_tld")
            Me.hdEstado.Value = gvTraslado.DataKeys(index).Values("cod_estado")
            Me.hdIdRevision.Value = gvTraslado.DataKeys(index).Values("codigo_rta")
            If IsDBNull(gvTraslado.DataKeys(index).Values("observacion_tld")) Then
                Me.hdObservacion.Value = String.Empty
            Else
                Me.hdObservacion.Value = gvTraslado.DataKeys(index).Values("observacion_tld")
            End If
        Catch ex As Exception
            Response.Write(ex.Message & " " & ex.StackTrace)
        End Try
    End Sub

    Protected Sub gvTraslado_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvTraslado.RowDataBound
        If (e.Row.RowType.ToString = "DataRow") Then
            e.Row.FindControl("btnAutorizar").Visible = False
            e.Row.FindControl("btnConfirmar").Visible = False
            Select Case e.Row.Cells(8).Text
                Case "Pendiente" : e.Row.FindControl("btnAutorizar").Visible = True
                Case "Autorizado" : e.Row.FindControl("btnConfirmar").Visible = True
            End Select
        End If
    End Sub

    Protected Sub btnGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardar.Click
        Dim obj As New ClsConectarDatos
        Dim codestado As Integer = 0
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        codestado = CInt(Me.hdEstado.Value) + 1
        Try
            obj.AbrirConexion()
            obj.Ejecutar("AF_revisarTrasladorAF", Me.hdTraslado.Value, codestado, Me.txtObservacion.Text, Me.hdIdRevision.Value, Request.QueryString("Id"))
            obj.CerrarConexion()
            mt_CargarDatos()
            Me.txtObservacion.Text = String.Empty
        Catch ex As Exception
            mt_ShowMessage("Error_CargaDatos(): " & ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

#End Region

#Region "Metodos"

    Private Sub mt_CargarDatos()
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        Dim nroped As Integer = 0
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        Try
            If Me.cboEstado.SelectedValue = "0" Then
                If Me.txtNroPedido.Text.Trim <> "" Then
                    If IsNumeric(Me.txtNroPedido.Text) Then
                        nroped = CInt(Me.txtNroPedido.Text)
                    Else
                        Throw New Exception("¡Ingrese un numero de Pedido correcto!")
                    End If
                End If
            End If
            obj.AbrirConexion()
            dt = obj.TraerDataTable("AF_ListarTrasladoAF2", nroped, Me.cboEstado.SelectedValue, Request.QueryString("Id"))
            obj.CerrarConexion()
            Me.gvTraslado.DataSource = dt
            Me.gvTraslado.DataBind()
        Catch ex As Exception
            mt_ShowMessage("Error_CargaDatos(): " & ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub mt_ShowMessage(ByVal Message As String, ByVal type As MessageType)
        Page.RegisterStartupScript("Mensaje", "<script>ShowMessage('" & Message & "','" & type.ToString & "');</script>")
    End Sub

#End Region

End Class
