Imports System.IO
Imports System.Web.HttpRequest
Imports System.Collections.Generic
Imports System.Web
Imports System.Net
Partial Class administrativo_Tesoreria_Rendiciones_AppRendiciones_SubirArchivos
    Inherits System.Web.UI.Page
    Sub SubirArchivo()
        Dim post As HttpPostedFile = HttpContext.Current.Request.Files("UploadedImage")
        Dim codigo_Dren As String = Request.Form("CodigoDren")
        Dim list As New List(Of Dictionary(Of String, Object))()
        'Dim JSONresult As String = ""
        If Not post Is Nothing Then
            Dim extension As String = System.IO.Path.GetExtension(post.FileName)
            Dim obj As New ClsConectarDatos
            Dim tb As New Data.Datatable
            Dim cn As New clsaccesodatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()

            tb = obj.TraerDataTable("dbo.USP_RENDAGREGARARCHIVO", codigo_Dren, "", extension, 0)
            obj.CerrarConexion()
            If tb.Rows.Count > 0 Then
                Dim FilePath As String = Server.MapPath("../../Archivosderendicion")
                Dim CodigoArchivo As String
                CodigoArchivo = tb.Rows(0)("Code")
                Dim ruta As String = ""
                ruta = FilePath & "\A" & CodigoArchivo & extension
                post.SaveAs(ruta)
                Dim Data As New Dictionary(Of String, Object)()
                Data.Add("Status", "OK")
                Data.Add("Message", "Archivo Subido")
                Data.Add("Code", ruta)
                List.Add(Data)
            Else
                Dim Data As New Dictionary(Of String, Object)()
                Data.Add("Status", "ERROR")
                Data.Add("Message", "No se Registrado ningun archivo")
                Data.Add("Code", Request.Form("CodigoDren"))
                List.Add(Data)
            End If

        Else
            Dim Data As New Dictionary(Of String, Object)()
            Data.Add("Status", "ERROR")
            Data.Add("Message", "No existe ningun archivo")
            Data.Add("Code", Request.Form("CodigoDren"))
            List.Add(Data)
        End If
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        JSONresult = serializer.Serialize(list)
        Response.Write(JSONresult)

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim FPost As String = Request.Form("Funcion")
        Select Case FPost           
            Case "UpFile"
                SubirArchivo()
        End Select
    End Sub
End Class
