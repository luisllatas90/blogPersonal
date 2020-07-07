
Partial Class firmojuramento
    Inherits System.Web.UI.Page

    Protected Sub dgwResultados_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles dgwResultados.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim fila As Data.DataRowView
            fila = e.Row.DataItem

            '           e.Row.Attributes.Add("OnMouseOver", "Resaltar(1,this,'S')")
            '          e.Row.Attributes.Add("OnMouseOut", "Resaltar(0,this,'S')")
            CType(e.Row.FindControl("chkElegir"), CheckBox).Attributes.Add("OnClick", "HabilitarEnvio(this)")
       End If
    End Sub

    Protected Sub cmdGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdGuardar.Click
        Dim I As Integer
        Dim Fila As GridViewRow
        Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("cnxBDUSAT").ConnectionString)
        Dim email As String

        'Try
        For I = 0 To Me.dgwResultados.Rows.Count - 1
            Fila = Me.dgwResultados.Rows(I)
            If Fila.RowType = DataControlRowType.DataRow Then
                If CType(Fila.FindControl("chkElegir"), CheckBox).Checked = True Then
                    '==================================
                    ' Guardar los datos
                    '==================================
                    'aca mi procedimientos
                    obj.Ejecutar("HOJ_ActualizarFirmaJuramento", Me.dgwResultados.DataKeys.Item(Fila.RowIndex).Values("codigo_per"))


                End If
            End If
        Next
        Me.dgwResultados.DataBind()
        obj = Nothing
        Page.RegisterStartupScript("CambioEstado", "<script>alert('Los cambios se guardaron correctamente.')</script>")
        'Catch ex As Exception
        '    Me.cmdGuardar.Visible = False
        '    Me.lblmensaje.Text = "Ocurrió un Error al Registrar el estado. Intente mas tarde." & Chr(13) & ex.Message
        '    obj = Nothing
        'End Try
    End Sub
End Class
