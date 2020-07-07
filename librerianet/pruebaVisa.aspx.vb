Imports System.IO
Imports System.Net


Partial Class pruebaVisa
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try
            Dim request As HttpWebRequest
            request = WebRequest.Create("https://devapice.vnforapps.com/api.ecommerce/api/v1/ecommerce/token/merchantId=502400510")

            Dim resp As HttpWebResponse
            resp = CType(request.GetResponse, HttpWebResponse)

            Dim stream As Stream
            stream = CType(resp.GetResponseStream(), Stream)

            Dim reader As New StreamReader(stream)
            Dim obj As Object = reader.ReadToEnd
            Dim objDato As New Object

            'objDato = JsonConvert.DeserializeObject<String>(json);

            Console.WriteLine("La temperatura en Madrid es: " + obj)

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try

    End Sub

End Class
