Imports System.IO
Imports System.Web.HttpRequest
Imports System.Collections.Generic
Imports System.Web
Imports System.Net
Imports System.Xml
Imports System.Xml.Serialization
Imports System.Text
Imports System.Web.Script.Serialization

Partial Class administrativo_Tesoreria_Rendiciones_nuevavista_LogicaWS
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim funcion As String = Request.Form("Funcion")
        Dim data As String = Request.Form("dataobj")

        Select Case funcion
            Case "PostDataFun"
                 PostDataFun(data)
            Case "hola"
                Response.Write("hola")
        End Select
        
    End Sub


    Sub PostDataFun(ByRef data As String)
        
        Dim l_LNEjecutarServicio As New LNEjecutarServicio
        Dim MyObject As Object
        Dim l_Rpt_WS As String
        Dim url As String = "http://teclovendo.com/wsccadmin/13_ws_wa_ValidarCredenciales/public/Autenticacion/Get_Id_Conexion"

        l_Rpt_WS = l_LNEjecutarServicio.EjecutarPOST(data,url)

        Dim l_rpt As new ENResultado
        Dim serializer As JavaScriptSerializer = New JavaScriptSerializer()

        l_Rpt_WS = " {  ""Resultado"" : 456, ""User"" : ""Juanito"", ""info"" : ""no acceder al sistema jamas"" "
        l_Rpt_WS = l_Rpt_WS & " , ""listaDatos"" : [ { ""Id"" : 1 , ""Descripcion"" : ""hola mundo"" },{ ""Id"" : 2 , ""Descripcion"" : ""hola mundo2"" } ]  "
        l_Rpt_WS = l_Rpt_WS & "}"
        

        Dim model As Prueba = serializer.Deserialize(Of Prueba)(l_Rpt_WS)


        '  Response.Write(l_Rpt_WS)
         Response.Write(serializer.Serialize(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString))
    End Sub

End Class


Public Class LNEjecutarServicio

    Public Function EjecutarPOST(ByRef data As String, ByRef url As String) As String
        Dim request As HttpWebRequest = DirectCast(WebRequest.Create(url), HttpWebRequest)
        request.Method = "POST"
        request.ContentType = "application/json"
        Dim postData As String = data
        request.ContentLength = postData.Length

        Dim writer As New StreamWriter(request.GetRequestStream(), System.Text.Encoding.ASCII)
        writer.Write(postData)
        writer.Close()

        Dim stream As Stream = request.GetResponse().GetResponseStream()
        Dim reader As New StreamReader(stream)
        Dim response As String = String.Empty
        response = reader.ReadToEnd()

        Return response
    End Function

End Class

Public Class ENResultado

    public Resultado As new ENResponse
    public lError As new ENError

    Public Sub New()
        Inicializar()
    End Sub

    Private Sub Inicializar()
        Resultado = new ENResponse
        lError = new ENError
    End Sub

End Class

Public Class ENResponse

    public Id_Conexion As String

    Public Sub New()
        Inicializar()
    End Sub

    Private Sub Inicializar()
        Id_Conexion = 0
    End Sub

End Class

public Class ENError

    public status As Integer
    public lerror As Integer
    public flagerror As Boolean
    public originerror As Integer
    public messages As String
    public Resultado As New ENResponse

    Public Sub New()
        Inicializar()
    End Sub

    Private Sub Inicializar()
        status = 0
        lerror = 0
        flagerror = false
        originerror = 0
        messages = ""
        Resultado = New ENResponse
    End Sub

End Class

public Class Prueba
    Public Resultado As Integer
    Public User As String
    Public info As String
    Public extra As String
    Public listaDatos As List(Of Detalle) = New List(Of Detalle)
End Class

public Class Detalle
    Public Id As Integer
    Public Descripcion As String
End Class