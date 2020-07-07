Imports Microsoft.VisualBasic
Imports System.Net
Imports System.IO
Imports System.Text
Imports System.Web
Imports System.Collections.Generic

Public Class DSNComponenteSunat
    Private myCookie As CookieContainer

    Public Sub SetParamsSearch(ByVal trabajadores As Boolean, ByVal local As Boolean, ByVal representantes As Boolean)
        myCookie = Nothing
        myCookie = New CookieContainer()
        ServicePointManager.Expect100Continue = True
        ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3
    End Sub

    Private Function ValidarCertificado(ByVal sender As Object, ByVal certificate As System.Security.Cryptography.X509Certificates.X509Certificate, ByVal chain As System.Security.Cryptography.X509Certificates.X509Chain, ByVal sslPolicyErrors As System.Net.Security.SslPolicyErrors) As Boolean
        Return True
    End Function

    Public Function GetConnectionSunat(ByVal numRuc As String, ByVal Capcha As String) As HttpWebRequest

        Dim myUrl As String = String.Format("http://www.sunat.gob.pe/cl-ti-itmrconsruc/jcrS00Alias?accion=consPorRuc&nroRuc={0}&numRnd={1}", numRuc, Capcha)
        Dim myWebRequest As HttpWebRequest = WebRequest.Create(myUrl)
        myWebRequest.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64; rv:23.0) Gecko/20100101 Firefox/23.0"
        myWebRequest.CookieContainer = myCookie
        myWebRequest.Credentials = CredentialCache.DefaultCredentials
        myWebRequest.Proxy = Nothing
        Return myWebRequest
    End Function

    Public Function GetInformacionPersona(ByVal NumDoc As String) As DSNStructInfoRuc
        System.Net.ServicePointManager.ServerCertificateValidationCallback = New System.Net.Security.RemoteCertificateValidationCallback(AddressOf ValidarCertificado)
        Dim myWebRequest As HttpWebRequest = WebRequest.Create("http://www.sunat.gob.pe/cl-ti-itmrconsruc/captcha?accion=random")
        myWebRequest.CookieContainer = myCookie
        myWebRequest.Proxy = Nothing
        myWebRequest.Credentials = CredentialCache.DefaultCredentials

        Dim myWebResponse As HttpWebResponse = myWebRequest.GetResponse()
        Dim myImgStream As Stream = myWebResponse.GetResponseStream()
        Dim streamReader As StreamReader = New StreamReader(myImgStream)
        Dim Cookie As String = streamReader.ReadToEnd()
        Dim con As HttpWebRequest = GetConnectionSunat(NumDoc, Cookie)
        Dim myHttpWebResponse As HttpWebResponse = con.GetResponse()
        Dim myStream As Stream = myHttpWebResponse.GetResponseStream()
        Dim myStreamReader As StreamReader = New StreamReader(myStream)
        Dim line As String = ""
        Dim cad As StringBuilder = New StringBuilder()
        Dim xDat As String = HttpUtility.HtmlDecode(myStreamReader.ReadToEnd())
        Dim util As New DSNDatosValores
        Dim info As DSNStructInfoRuc = New DSNStructInfoRuc()
        Dim field As List(Of DSNDatosValores) = DSNDatosValores.GetDatos(xDat)

        Dim res As New StringBuilder
        Dim recorre As IEnumerator(Of DSNDatosValores) = field.GetEnumerator()
        While recorre.MoveNext()
            info.SetValue(recorre.Current)
            ''  Dim f As String = recorre.Current.Dato.ToLower()
            ''  Dim valores As List(Of String) = recorre.Current.Valor

            ''  If valores.Count > 0 Then
            ''  res.Append(valores(0).ToUpper().Trim())
            '' End If

            ''  res.Append(info.NombreRazonSocial)

            ' info.NombreRazonSocial = res.ToString()
        End While
        '  info.NombreRazonSocial = recorre.Current.Valor(0) ' field(0).Valor(0)
        Return info
    End Function

End Class
