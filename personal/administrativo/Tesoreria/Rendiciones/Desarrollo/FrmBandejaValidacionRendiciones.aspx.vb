Imports System.Net
Imports System.Text
Imports System.IO

Partial Class administrativo_Tesoreria_Rendiciones_nuevavista_frmBandejaRendiciones
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load





    End Sub

    ' Public Sub Try01(ByVal URL)
    '     Try
    '         Dim myReq As HttpWebRequest
    '         Dim myResp As HttpWebResponse
    '         Dim myReader As StreamReader
    '         myReq = HttpWebRequest.Create(URL)
    '         myReq.Method = "POST"
    '         myReq.ContentType = "application/json"
    '         myReq.Accept = "application/json"
            ' myReq.Headers.Add("Authorization", "Bearer LKJLMLKJLHLMKLJLM839800K=")
    '         Dim myData As String = "{""riskLevelStatus"":""0001"",""userId"":""10000004030"",""applicationName"":""MyTestRESTAPI""}"
    '         myReq.GetRequestStream.Write(System.Text.Encoding.UTF8.GetBytes(myData), 0, System.Text.Encoding.UTF8.GetBytes(myData).Count)
    '         myResp = myReq.GetResponse
    '         myReader = New System.IO.StreamReader(myResp.GetResponseStream)
    '         TextBox1.Text = myReader.ReadToEnd
    '     Catch ex As Exception
    '         TextBox1.Text = TextBox1.Text & "Error: " & ex.Message
    '     End Try
    ' End Sub



End Class


