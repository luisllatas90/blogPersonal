﻿
Partial Class GestionInvestigacion_frmRevisarBonos
    Inherits System.Web.UI.Page
    Dim cod_user_i As Integer
    Dim cod_user_s As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (Session("id_per") Is Nothing) Then
            Response.Redirect("../../sinacceso.html")
        End If

        Dim obj As New ClsGestionInvestigacion
        Try
            'Ver el tema de autentificación
            'If Not Me.Page.User.Identity.IsAuthenticated Then
            '    Response.Redirect("~/Default.aspx")
            '    Exit Sub
            'Else
            If (Request.QueryString("id") <> "") Then
                cod_user_s = obj.EncrytedString64(Request.QueryString("id"))
                cod_user_i = Request.QueryString("id")
                Me.hdUser.Value = cod_user_s
                Me.hdCtf.Value = Request.QueryString("ctf")
                'Información

            End If
            'End If

        Catch ex As Exception
            Response.Write(ex.Message.ToString)
        End Try

    End Sub

End Class
