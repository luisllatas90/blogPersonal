<%@ WebHandler Language="VB" Class="JsonResponse1" %>

Imports System
Imports System.Web
Imports System.Collections.Generic

Public Class JsonResponse1 : Implements IHttpHandler, IRequiresSessionState   
    Public Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
        context.Response.ContentType = "application/json"        
        
        Dim codigo_pro As Integer = 0        
        If (context.Session("cod_pro") IsNot Nothing) Then
            codigo_pro = Integer.Parse(context.Session("cod_pro"))
        End If
        
        Dim clsEvento As New clsPlanCalendario
        Dim cevent As EventosCalendario
        Dim aux As New List(Of EventosCalendario1)
        
        'Dim color As String = ""
        For Each cevent In clsEvento.getEvents(codigo_pro)
            'color = retornaColor()
            
            Dim ev As New EventosCalendario1
            'ev.id = cevent.id
            ev.title = cevent.title
            ev.description = cevent.description
            ev.start = cevent.start.Year
            ev.end = cevent.end.Year
            ev.isDuration = False
            'ev.start = ConvertToTimestamp(cevent.start)
            'ev.end = ConvertToTimestamp(cevent.end)
            'ev.background = cevent.background
            'ev.color = cevent.color
            aux.Add(ev)
        Next
        
        Dim oSerializer As New System.Web.Script.Serialization.JavaScriptSerializer
        Dim sJSON As String = ""        
        sJSON = oSerializer.Serialize(aux)               
        context.Response.Write(sJSON)
    End Sub
    
    Public ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property

    Private Function retornaColor() As String
        Dim rnd As New Random
        Dim ubicacion As Integer
        Dim strNumeros As String = "0123456789"
        Dim Token As String = ""
        While Token.Length < 6
            ubicacion = rnd.Next(0, strNumeros.Length)
            If (ubicacion = 10) Then
                Token = Token & strNumeros.Substring(ubicacion - 1, 1)
            End If
            If (ubicacion < 10) Then
                Token = Token & strNumeros.Substring(ubicacion, 1)
            End If
        End While

        Return "#" & Token
    End Function
    
    Private Function ConvertToTimestamp(ByVal value As Date) As Long
        Dim tspan As TimeSpan
        tspan = value.ToUniversalTime().Subtract(New DateTime(1970, 1, 1, 0, 0, 0))

        Return CLng(Math.Truncate(tspan.TotalSeconds))
        
        
    End Function
End Class