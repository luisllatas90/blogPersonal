Imports System.Windows.Forms
Partial Class administrativo_personal_frmRegistrarEntregaEntrada
    Inherits System.Web.UI.Page
    Dim codigo_per As Integer

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        codigo_per = Request.QueryString("id")
    End Sub

    Protected Sub CmdBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdBuscar.Click

        Buscar()
    End Sub

    Private Sub Buscar()
        Dim obj As New Inscripcion
        Dim dts As New Data.DataTable
        dts = obj.ConsultarInscritos(55, Me.txtCadena.text)
        dgvEntradas.DataSource = dts
        dgvEntradas.DataBind()
    End Sub

    Protected Sub dgvEntradas_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles dgvEntradas.RowDataBound

        If e.Row.Cells(10).Text <> "Entradas Entregadas" Then
            If e.Row.Cells(10).Text <> "0" Then
                e.Row.Cells(11).Text = ""
            End If
        End If

    End Sub

    Protected Sub dgvEntradas_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgvEntradas.SelectedIndexChanged
        'Response.Write(Me.dgvEntradas.Rows(Me.dgvEntradas.SelectedIndex).Cells("codigo_deu").Text)
        Dim obj As New Inscripcion
        obj.RegistrarEntrega(codigo_per, Me.dgvEntradas.SelectedRow.Cells(12).Text)
        Buscar()
        'Response.Write("<script>alert('Se registró la entrega de la entrada');</script>")
    End Sub
End Class
