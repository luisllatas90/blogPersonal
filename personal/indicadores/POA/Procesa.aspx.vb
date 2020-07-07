﻿
Partial Class indicadores_POA_Procesar
    Inherits System.Web.UI.Page

    Public Function ToJSON(ByVal dato As String) As String
        Dim jsonSerializer As New System.Web.Script.Serialization.JavaScriptSerializer()
        Return jsonSerializer.Serialize(dato)
    End Function

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (Session("id_per") Is Nothing) Then
            Response.Redirect("../../../sinacceso.html")
        End If
        Dim obj As New clsPlanOperativoAnual
        Dim dt As New Data.DataTable
        If Request.QueryString("Accion") = "CambiarInstancia" Then
            Dim id, ctf, codigo_acp As Integer
            codigo_acp = Request("acp")
            id = Request.QueryString("id")
            ctf = Request.QueryString("ctf")
            If obj.InstanciaEstadoActividad(codigo_acp, 9, Request.QueryString("id")) = "1" Then
                Response.Redirect("FrmListaEvaluarPresupuesto.aspx?id=" & id & "&ctf=" & ctf & "&back=Estado&ok=1")
            Else
                Response.Redirect("FrmListaEvaluarPresupuesto.aspx?id=" & id & "&ctf=" & ctf & "&back=Estado&ok=0")
            End If
        End If

    End Sub

End Class
