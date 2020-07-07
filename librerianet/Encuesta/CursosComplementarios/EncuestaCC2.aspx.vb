﻿'----------------------------------------------------------------------
'Fecha: 30.10.2012
'Usuario: gcastillo
'Motivo: Cambio de URL del servidor de la WebUSAT
'----------------------------------------------------------------------
Partial Class Encuesta_CursosComplementarios_EncuestaCC2
    Inherits System.Web.UI.Page

    Protected Sub rblrpta2_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rblrpta2.SelectedIndexChanged
        If rblrpta2.SelectedValue = 7 Then
            txtOtroHorario.Enabled = True
        Else
            txtOtroHorario.Enabled = False
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
		ClientScript.RegisterStartupScript(Me.GetType, "direccionar", "location.href('../../../');", True)
    End Sub

    Protected Sub cmdGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdGuardar.Click
        Dim obj As New ClsConectarDatos
        Try
		ClientScript.RegisterStartupScript(Me.GetType, "direccionar", "location.href('../../../');", True)
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            If rblRpta1.SelectedValue = "S" Then
                If rblrpta2.SelectedValue = 7 And txtOtroHorario.Text.Length < 3 Then
                    lblValidarOtro.Visible = True
                    ClientScript.RegisterStartupScript(Me.GetType, "validar", "alert('Debe especificar el horario que desea si eligió la opción Otro en la pregunta 2')", True)
                Else
                    lblValidarOtro.Visible = False
                    obj.IniciarTransaccion()
                    If rblrpta2.SelectedValue = 7 Then
                        obj.Ejecutar("ECC_AgregarRespuestaEncuesta2", rblRpta1.SelectedValue, rblrpta2.SelectedItem.Text + " " + txtOtroHorario.Text, Request.QueryString("id"))
                    Else
                        obj.Ejecutar("ECC_AgregarRespuestaEncuesta2", rblRpta1.SelectedValue, rblrpta2.SelectedItem.Text, Request.QueryString("id"))
                    End If
                    obj.TerminarTransaccion()
                End If
            Else
                obj.IniciarTransaccion()
                obj.Ejecutar("ECC_AgregarRespuestaEncuesta2", rblRpta1.SelectedValue, "", Request.QueryString("id"))
                obj.TerminarTransaccion()
            End If
            ClientScript.RegisterStartupScript(Me.GetType, "correcto", "alert('Gracias por llenar la encuesta, ahora puede volver a ingresar a su campus');", True)
            ClientScript.RegisterStartupScript(Me.GetType, "direccionar", "location.href('../../../');", True)
        Catch ex As Exception
            obj.AbortarTransaccion()
            ClientScript.RegisterStartupScript(Me.GetType, "error", "alert('Ocurrió un error al procesar los datos')", True)
        End Try
    End Sub

    Protected Sub rblRpta1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rblRpta1.SelectedIndexChanged
        If rblRpta1.SelectedValue = "S" Then
            Me.valHorario.Enabled = True
        Else
            Me.valHorario.Enabled = False
        End If
    End Sub
End Class
