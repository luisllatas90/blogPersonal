Imports System.IO
Imports System.Web.HttpRequest
Imports System.Collections.Generic
Imports System.Data

Partial Class demo_Sesion
    Inherits System.Web.UI.Page

    Private oeUsuAplLog As e_UsuarioAplicacionLogeo, odUsuAplLog As d_UsuarioAplicacionLogeo

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim Data As New Dictionary(Of String, Object)()
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()
        Try
            Session("gcodigo_ctfu") = Request("ctfu")
            Session("gcodigo_apl") = Request("capl")
            Session("gdescri_apl") = Request("dapl")
            Session("gdescri_tfu") = Request("dtfu")
            Session("gestilo_apl") = Request("eapl")

            Dim _varsplit() As String = GetClientHostIP().Split("-")
            oeUsuAplLog = New e_UsuarioAplicacionLogeo : odUsuAplLog = New d_UsuarioAplicacionLogeo
            With oeUsuAplLog
                .codigo_per = Session("id_per") : .codigo_apl = Session("gcodigo_apl") : .codigo_tfu = Session("gcodigo_ctfu")
                .hostname_uac = _varsplit(0) : .ip_uac = _varsplit(1)
            End With
            odUsuAplLog.fc_RegistrarUsuarioAplicacionLogeo(oeUsuAplLog)

            Data.Add("rpta", "ok")
            Data.Add("msje", "ctfu : " & Request("ctfu") & " capl: " & Request("capl") & " dapl: " & Request("dapl") & " dtfu: " & Request("dtfu") & " eapl: " & Request("eapl"))
            list.Add(Data)
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)
            'Response.Write("ctfu : " & Request("ctfu") & " capl: " & Request("capl") & " dapl: " & Request("dapl") & " dtfu: " & Request("dtfu") & " eapl: " & Request("eapl"))
        Catch ex As Exception
            Data.Add("rpta", "error")
            Data.Add("msje", ex.Message)
            list.Add(Data)
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)
        End Try
    End Sub

    Public Function GetClientHostIP() As String
        Dim clientIP As String = String.Empty
        Dim hostName As String = String.Empty

        Try
            clientIP = Request.UserHostAddress
            hostName = System.Net.Dns.GetHostEntry(Request.UserHostAddress).HostName
        Catch ex As Exception
            clientIP = "0.0.0.0"
            hostName = "PCError"
        End Try

        Return hostName & " - " & clientIP
    End Function

End Class
