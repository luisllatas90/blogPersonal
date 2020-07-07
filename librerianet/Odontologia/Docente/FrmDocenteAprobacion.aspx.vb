
Partial Class Odontologia_Docente_FrmDocenteAprobacion
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try            
            If (Session("codper_odo") IsNot Nothing) Then
                If (Session("codper_odo").ToString.Trim <> "") Then
                    CargaPedidos()
                    Me.lblTrabajador.Text = "Personal: " & Session("nomper_odo")
                Else
                    Response.Redirect("frmAcceso.aspx")
                End If
            Else
                Response.Redirect("frmAcceso.aspx")
            End If            
        Catch ex As Exception
            Response.Redirect("frmAcceso.aspx")
        End Try
    End Sub

    Private Sub CargaPedidos()
        Try
            Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("cnxBDUSAT").ConnectionString)
            Dim dt As New Data.DataTable
            dt = obj.TraerDataTable("ODO_BuscaPedidosxAprobar", Session("codper_odo"))
            Me.gvPedidos.DataSource = dt
            Me.gvPedidos.DataBind()
            If (dt.Rows.Count = 0) Then
                Me.lblMensaje.Text = "No hay más pedidos por mostrar."
            End If
        Catch ex As Exception
            Response.Write("Error al cargar datos: " & ex.Message)
        End Try
    End Sub

    Protected Sub gvPedidos_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles gvPedidos.RowEditing
        Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("cnxBDUSAT").ConnectionString)
        Try
            Dim data As String = gvPedidos.DataKeys(e.NewEditIndex).Item("CantMaxima")
            'Dim alumno As String = gvPedidos.DataKeys(e.NewEditIndex).Item("codigo_alu")            
            Dim dt As New Data.DataTable
            If (data.Trim <> "") Then                
                If (Double.Parse(data) = 0) Then
                    obj.IniciarTransaccion()                    
                    dt = obj.TraerDataTable("ODO_VerificaDeudaPendiente", Me.gvPedidos.Rows(e.NewEditIndex).Cells(0).Text)
                    If (dt.Rows.Count = 0) Then     'Igual a 0 - No tiene deudas   |   Mayor 0 - Tiene deudas 
                        obj.Ejecutar("ODO_AprobacionPedido", Me.gvPedidos.Rows(e.NewEditIndex).Cells(0).Text)
                        obj.Ejecutar("ODO_GeneraDeudaTratamiento", Me.gvPedidos.Rows(e.NewEditIndex).Cells(0).Text, Request.ServerVariables("REMOTE_ADDR"))
                        Me.lblMensaje.Text = ""
                    Else
                        Me.lblMensaje.Text = "Excede el crédito permitido."
                    End If
                    obj.TerminarTransaccion()
                End If
            End If
            CargaPedidos()
        Catch ex As Exception
            obj.AbortarTransaccion()
            Response.Write("Error al generar deuda: " & ex.Message)
        End Try
    End Sub

    Protected Sub gvPedidos_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles gvPedidos.RowDeleting
        Try
            Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("cnxBDUSAT").ConnectionString)
            obj.Ejecutar("ODO_RechazarPedido", Me.gvPedidos.Rows(e.RowIndex).Cells(0).Text)
            CargaPedidos()
        Catch ex As Exception
            Response.Write("Error al rechazar pedido: " & ex.Message)
        End Try
    End Sub

    Protected Sub btnSalir_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSalir.Click
        Session.Clear()
        Session.RemoveAll()
        Response.Redirect("frmAcceso.aspx")
    End Sub

    Protected Sub btnRefrescar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRefrescar.Click
        CargaPedidos()
    End Sub

    Protected Sub gvPedidos_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvPedidos.RowDataBound
        'CantMaxima
        If (e.Row.RowIndex > -1) Then
            If (e.Row.Cells(9).Text > 0) Then                
                e.Row.Cells(7).Text = "<a href='frmDetallePedido.aspx?codigo_pod=" & e.Row.Cells(0).Text.ToString & "&KeepThis=true&TB_iframe=true&top=0&height=600&width=750&modal=true' title='Ver horarios' class='thickbox'>Revisar<a/>"

                'e.Row.Cells(6).Text = "<a href='#'>Revisar<a/>"
            End If
        End If
        
        e.Row.Cells(9).Visible = False
    End Sub

End Class
