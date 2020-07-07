
Partial Class Odontologia_frmDetallePedido
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (IsPostBack = False) Then
            If Request.QueryString("codigo_pod") IsNot Nothing Then
                CargaDetalle(Request.QueryString("codigo_pod"))
            End If
        End If
    End Sub

    Private Sub CargaDetalle(ByVal Codigo As Integer)
        Try
            Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("cnxBDUSAT").ConnectionString)
            Dim dt As New Data.DataTable
            dt = obj.TraerDataTable("ODO_ListaDetallePedidoCantMax", Codigo)
            Me.gvDetalle.DataSource = dt
            Me.gvDetalle.DataBind()
        Catch ex As Exception
            Response.Write("Error: " & ex.Message)
        End Try
    End Sub

    Protected Sub btnAprobar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAprobar.Click
        Try
            Dim Fila As GridViewRow
            If (ValidaFormulario() = True) Then
                Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("cnxBDUSAT").ConnectionString)
                For i As Integer = 0 To Me.gvDetalle.Rows.Count - 1
                    Fila = Me.gvDetalle.Rows(i)
                    Dim valor As String = CType(Fila.FindControl("txtPermitido"), TextBox).Text
                    obj.Ejecutar("ODO_ActualizaCantidadMaxima", Me.gvDetalle.Rows(i).Cells(0).Text, valor)
                Next

                Dim dt As New Data.DataTable
                dt = obj.TraerDataTable("ODO_VerificaDeudaPendiente", Request.QueryString("codigo_pod"))
                If (dt.Rows.Count = 0) Then     'Igual a 0 - No tiene deudas   |   Mayor 0 - Tiene deudas 
                    obj.IniciarTransaccion()
                    obj.Ejecutar("ODO_AprobacionPedido", Request.QueryString("codigo_pod"))
                    obj.Ejecutar("ODO_GeneraDeudaTratamiento", Request.QueryString("codigo_pod"), Request.ServerVariables("REMOTE_ADDR"))
                    obj.TerminarTransaccion()
                Else
                    Response.Write("<script>alert('Excede el crédito permitido.');</script>")
                End If                
            End If
            Response.Write("<script>self.parent.location.reload();</script>")
            Response.Write("<script>self.parent.tb_remove();</script>")
        Catch ex As Exception
            Response.Write("Error: " & ex.Message)
        End Try
        'Exit Sub
    End Sub

    Private Function ValidaFormulario() As Boolean
        Try
            Dim Fila As GridViewRow
            For i As Integer = 0 To Me.gvDetalle.Rows.Count - 1
                Fila = Me.gvDetalle.Rows(i)
                Dim valor As String = CType(Fila.FindControl("txtPermitido"), TextBox).Text
                Dim Limite As Double = Double.Parse(valor)                
                If (Limite < Double.Parse(Me.gvDetalle.Rows(i).Cells(2).Text) Or _
                    Limite > Double.Parse(Me.gvDetalle.Rows(i).Cells(3).Text)) Then
                    Response.Write("<script>alert('El valor ingresado no es el correcto para " & Me.gvDetalle.Rows(i).Cells(1).Text & " ')</script>")
                    Return False
                End If
            Next

            Return True
        Catch ex As Exception
            Response.Write("<script>alert('Error al verificar los datos')</script>")
            Return False
        End Try
    End Function
End Class
