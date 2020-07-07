Partial Class DirectorDepartamento_frmlinea
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim Valores(2) As String
        Valores = Split(Request.QueryString("codigo"), "|")
        Me.HddCodigo_cco.Value = Valores(0) ' --> El número de centro de costos.
        Me.HDDCodigo_are.Value = Valores(1) ' --> El número de codigo de area
        Me.HddNivel_are.Value = Valores(2)  ' --> El numero de nivel de area

        If IsPostBack = False Then
            If Request.QueryString("modo") = "n" Then
                Me.LblTitulo.Text = "Registrar Linea de Investigación"
            Else
                Dim objinv As New Investigacion
                Dim tabla As New Data.DataTable
                tabla = objinv.ConsultarUnidadesInvestigacion("5", Valores(1))
                Me.Txtlinea.Text = tabla.Rows(0).Item("nombre_are")
                Me.TxtProposito.Text = tabla.Rows(0).Item("proposito_Are").ToString
                tabla.Dispose()
                tabla = Nothing
                objinv = Nothing

                Me.LblTitulo.Text = "Modificar Línea de Investigación"
            End If
        End If
    End Sub

    Protected Sub CmdGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdGuardar.Click
        Dim ObjInv As New Investigacion
        If Request.QueryString("modo") = "n" Then
            If ObjInv.AgregarLineas(Me.Txtlinea.Text.Trim, Me.TxtProposito.Text.Trim, Me.HddCodigo_cco.Value, Me.HDDCodigo_are.Value, Me.HddNivel_are.Value) = -1 Then
                Me.LblError.ForeColor = Drawing.Color.Red
                Me.LblError.Text = "Ocurrió un error al procesar los datos."
            Else
                Response.Write("<script>window.opener.location.reload(); window.close();</script>")
            End If
        Else
            If ObjInv.ModificarLineas(Me.Txtlinea.Text.Trim, Me.TxtProposito.Text.Trim, CInt(Me.HDDCodigo_are.Value)) = -1 Then
                Me.LblError.ForeColor = Drawing.Color.Red
                Me.LblError.Text = "Ocurrió un error al procesar los datos."
            Else
                Response.Write("<script>window.opener.location.reload(); window.close();</script>")
            End If
        End If
        ObjInv = Nothing
    End Sub
End Class
