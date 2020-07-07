'----------------------------------------------------------------------
'Fecha: 30.10.2012
'Usuario: gcastillo
'Motivo: Cambio de URL del servidor de la WebUSAT
'----------------------------------------------------------------------
Partial Class Encuesta_CursosComplementarios_PlanPiloto
    Inherits System.Web.UI.Page

    Protected Sub cmdGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdGuardar.Click
        Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
        Dim horario, dia As String
        Try

            If cboEnSemana.SelectedValue <> "" Then
                horario = cboEnSemana.SelectedItem.Text
                dia = "L-V"
            ElseIf cboSabados.SelectedValue <> "" Then
                horario = cboSabados.SelectedItem.Text
                dia = "S"
            End If

            obj.Ejecutar("ECC_AgregarEncuestaCursosComplementarios", Request.QueryString("enc"), IIf(chkIdiomas.Items.Item(0).Selected, 1, 0), _
                               IIf(chkIdiomas.Items.Item(1).Selected, 1, 0), IIf(chkIdiomas.Items.Item(2).Selected, 1, 0), IIf(chkIdiomas.Items.Item(3).Selected, 1, 0), _
                               IIf(chkIdiomas.Items.Item(4).Selected, 1, 0), horario, dia)
            ClientScript.RegisterStartupScript(Me.GetType, "correcto", "alert('Gracias por llenar la encuesta, puede volver a ingresar a su campus');", True)
            'ClientScript.RegisterStartupScript(Me.GetType, "direccionar", "location.href('http://www.usat.edu.pe/campusvirtual/');", True)
            ClientScript.RegisterStartupScript(Me.GetType, "direccionar", "location.href('https://intranet.usat.edu.pe/campusvirtual/');", True)
        Catch ex As Exception
            Response.Write(ex.Message)
            'ClientScript.RegisterStartupScript(Me.GetType, "error", "alert('Ocurrió un error')", True)
        End Try
    End Sub

    Protected Sub cboEnSemana_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboEnSemana.SelectedIndexChanged
        For i As Int16 = 0 To cboSabados.Items.Count - 1
            Me.cboSabados.Items(i).Selected = False
        Next
    End Sub

    Protected Sub cboSabados_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboSabados.SelectedIndexChanged
        For i As Int16 = 0 To cboEnSemana.Items.Count - 1
            Me.cboEnSemana.Items(i).Selected = False
        Next
    End Sub

End Class
