
Partial Class Encuesta_PyD_EncuestaBoletin
    Inherits System.Web.UI.Page

    Protected Sub cmdGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdGuardar.Click
        Try
            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            If Me.rblInteresado.SelectedValue = "Si" Then
                obj.AbrirConexion()
                obj.Ejecutar("ENC_AgregarEncuestaBoletin", Me.rblInteresado.SelectedValue, IIf(Me.rblPaginas.Enabled = True, Me.rblPaginas.SelectedItem.Text, 0), Me.rblTiempo.SelectedValue, IIf(Me.rblTemas.SelectedValue = 5, Me.txtOtro.Text, Me.rblTemas.SelectedItem.Text), Me.rblMedio.SelectedItem.Text, Request.QueryString("id"))
                obj.CerrarConexion()
            Else
                obj.AbrirConexion()
                obj.Ejecutar("ENC_AgregarEncuestaBoletin", Me.rblInteresado.SelectedValue, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, Request.QueryString("id"))
                obj.CerrarConexion()
            End If
            ClientScript.RegisterStartupScript(Me.GetType, "Guardar", "alert('Gracias por contestar la encuesta');", True)
            ClientScript.RegisterStartupScript(Me.GetType, "Cerrar", "window.close();", True)
        Catch ex As Exception
            ClientScript.RegisterStartupScript(Me.GetType, "Error", "alert('Ocurrió un error al procesar los datos')", True)
        End Try
    End Sub

    Protected Sub rblInteresado_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rblInteresado.SelectedIndexChanged
        If Me.rblinteresado.selectedvalue = "Si" Then
            Controles(True)
        Else
            Controles(False)
        End If
    End Sub
    Private Sub Controles(ByVal estado As Boolean)
        Me.rfvPreg2.Enabled = estado
        Me.rfvPreg3.Enabled = estado
        Me.rfvPreg5.Enabled = estado
        Me.rfvPreg4.Enabled = estado

        Me.rblMedio.Enabled = estado
        Me.rblTemas.Enabled = estado
        Me.rblTiempo.Enabled = estado
        If Me.rblMedio.SelectedValue = "1" Then Me.rblPaginas.Enabled = estado
        If Me.rblTemas.SelectedValue = "5" Then Me.txtOtro.Enabled = estado
    End Sub

    Protected Sub rblMedio_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rblMedio.SelectedIndexChanged
        If Me.rblMedio.SelectedValue = 1 Then
            Me.rblPaginas.Enabled = True
            Me.rfvPreg4.Enabled = True
        Else
            Me.rblPaginas.Enabled = False
            Me.rfvPreg4.Enabled = False
        End If
    End Sub

    Protected Sub rblTemas_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rblTemas.SelectedIndexChanged
        If Me.rblTemas.SelectedValue = 5 Then
            Me.txtOtro.Enabled = True
            Me.rfvtxtOtro.Enabled = True
        Else
            Me.txtOtro.Enabled = False
            Me.rfvtxtOtro.Enabled = False
            Me.txtOtro.Text = ""
        End If
    End Sub
End Class
