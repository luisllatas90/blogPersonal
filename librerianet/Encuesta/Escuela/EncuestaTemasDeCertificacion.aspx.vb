
Partial Class Encuesta_Escuela_TemasDeCertificacion
    Inherits System.Web.UI.Page

    
    Protected Sub cboCisco_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboCisco.SelectedIndexChanged
        CargarValoresFaltantes()
    End Sub

    Sub CargarValoresFaltantes()
        Dim a(6) As Integer
        Dim i As Integer = 0
        Dim j As Integer = 0
        Dim sw As Integer
        If cboCisco.SelectedValue <> 0 Then
            a(i) = cboCisco.SelectedValue
            i = i + 1
        End If
        If cboItil.SelectedValue <> 0 Then
            a(i) = cboItil.SelectedValue
            i = i + 1
        End If
        If cboJava.SelectedValue <> 0 Then
            a(i) = cboJava.SelectedValue
            i = i + 1
        End If
        If cboLpi.SelectedValue <> 0 Then
            a(i) = cboLpi.SelectedValue
            i = i + 1
        End If
        If cboMicrosoft.SelectedValue <> 0 Then
            a(i) = cboMicrosoft.SelectedValue
            i = i + 1
        End If
        If cboOracle.SelectedValue <> 0 Then
            a(i) = cboOracle.SelectedValue
            i = i + 1
        End If
        If cboSiemon.SelectedValue <> 0 Then
            a(i) = cboSiemon.SelectedValue
            i = i + 1
        End If
        LimpiarCombos()
        For i = 0 To 6
            sw = 0
            For j = 0 To 6
                If i + 1 = a(j) Then
                    sw = 1
                End If
            Next
            If sw = 0 Then
                cboCisco.Items.Add(i + 1)
                cboItil.Items.Add(i + 1)
                cboJava.Items.Add(i + 1)
                cboLpi.Items.Add(i + 1)
                cboMicrosoft.Items.Add(i + 1)
                cboOracle.Items.Add(i + 1)
                cboSiemon.Items.Add(i + 1)
            End If
        Next
    End Sub

    Sub LimpiarCombos()
        InicializaCombo(cboCisco)
        InicializaCombo(cboItil)
        InicializaCombo(cboJava)
        InicializaCombo(cboLpi)
        InicializaCombo(cboMicrosoft)
        InicializaCombo(cboOracle)
        InicializaCombo(cboSiemon)
    End Sub

    Sub InicializaCombo(ByRef combo As DropDownList)
        Dim valor As Int16 = 0
        If combo.SelectedValue <> 0 Then
            valor = combo.SelectedValue
        End If
        combo.Items.Clear()
        combo.Items.Add("-")
        combo.Items(0).Value = 0
        If valor <> 0 Then
            combo.Items.Add(valor)
            combo.Items(1).Value = valor
            combo.SelectedValue = valor
        End If
    End Sub

    Protected Sub cboOracle_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboOracle.SelectedIndexChanged
        CargarValoresFaltantes()
    End Sub

    Protected Sub cboJava_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboJava.SelectedIndexChanged
        CargarValoresFaltantes()
    End Sub

    Protected Sub cboSiemon_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboSiemon.SelectedIndexChanged
        CargarValoresFaltantes()
    End Sub

    Protected Sub cboLpi_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboLpi.SelectedIndexChanged
        CargarValoresFaltantes()
    End Sub

    Protected Sub cboItil_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboItil.SelectedIndexChanged
        CargarValoresFaltantes()
    End Sub

    Protected Sub cboMicrosoft_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboMicrosoft.SelectedIndexChanged
        CargarValoresFaltantes()
    End Sub

    Protected Sub cmdGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdGuardar.Click
        Dim objCnx As New ClsConectarDatos
        Try
         If cboCisco.SelectedValue <> 0 and cboItil.SelectedValue <> 0 and   cboJava.SelectedValue <> 0 and cboLpi.SelectedValue <> 0 and cboMicrosoft.SelectedValue <> 0 and cboOracle.SelectedValue <> 0 and cboSiemon.SelectedValue <> 0 Then
            objCnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            objCnx.AbrirConexion()
            objCnx.Ejecutar("ENC_AgregarTemaCertificacion", cboCisco.SelectedValue, cboOracle.SelectedValue, cboJava.SelectedValue, cboSiemon.SelectedValue, _
                            cboLpi.SelectedValue, cboItil.SelectedValue, cboMicrosoft.SelectedValue, Request.QueryString("codigo_alu"))
            ClientScript.RegisterStartupScript(Me.GetType, "correcto", "alert('Gracias por llenar la encuesta');window.close()", True)
            objCnx.CerrarConexion()
	else 
		ClientScript.RegisterStartupScript(Me.GetType, "marcar", "alert('Debe indicar una prioridad a cada tema');", True)
         end if
        Catch ex As Exception
            ClientScript.RegisterStartupScript(Me.GetType, "error", "alert('Ocurrió un error al procesar los datos')", True)
        End Try
    End Sub

End Class
