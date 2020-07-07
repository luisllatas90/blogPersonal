
Partial Class DirectorDepartamento_frmtematica
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            If Request.QueryString("modo") = "n" Then
                Me.LblTitulo.Text = "Registrar Temática de Investigación"
            Else
                Dim objInv As New Investigacion
                Dim tabla As New Data.DataTable
                tabla = objInv.ConsultarUnidadesInvestigacion("7", Request.QueryString("codigo"))
                Me.TxtTematica.Text = tabla.Rows(0).Item("nombre_tem")
                Me.TxtProposito.Text = tabla.Rows(0).Item("proposito_tem")
                tabla.Dispose()
                tabla = Nothing
                objInv = Nothing

                Me.LblTitulo.Text = "Modificar Temática de Investigación"
            End If
        End If
    End Sub

    Protected Sub CmdGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdGuardar.Click
        Dim ObjInv As New Investigacion
        If Request.QueryString("modo") = "n" Then
            If ObjInv.AgregarTematicas(Me.TxtTematica.Text.Trim, Me.TxtProposito.Text.Trim, Request.QueryString("codigo")) = -1 Then
                Me.LblError.ForeColor = Drawing.Color.Red
                Me.LblError.Text = "Ocurrió un error al procesar los datos."
            Else
                Response.Write("<script>window.opener.location.reload(); window.close();</script>")
            End If
        Else
            If ObjInv.ModificarTematicas(Me.TxtTematica.Text.Trim, Me.TxtProposito.Text.Trim, Request.QueryString("codigo")) = -1 Then
                Me.LblError.ForeColor = Drawing.Color.Red
                Me.LblError.Text = "Ocurrió un error al procesar los datos."
            Else
                Response.Write("<script>window.opener.location.reload(); window.close();</script>")
            End If
        End If
        ObjInv = Nothing
    End Sub
End Class
