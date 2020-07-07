
Partial Class USATCRIS_LoginORCID
    Inherits System.Web.UI.Page



    ' Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    '     'To get Access Code
    '     'Note we pass extra params through the querystring to retrive additional objects after postback using UrlEncode
    '     'remember UrlDecode at the other end...
    '     Dim redirectURI As String = String.Format("{0}?client_id={1}&response_type={2}&scope={3}&redirect_uri={4}", AuthorizationEndPoint, ClientID, "code", "/authenticate", HttpUtility.UrlEncode("~/ORCIDAuthentication.aspx?id1=" & 1 & "&id2=" & 2))

    '     'Open Dialog with Url for Access Code -> we use Telerik RadWindow but this can be a page...
    '     'link Button onclick...
    '     lbtnGetAuthenticationProvider.Attributes.Add("onclick", "openRadWindow('" & redirectURI & "', '" & [yourTextHere] & "'); return false;")

    '     'Authorize with ORCID... -> return to RedirectURI...

    '     '''In RedirectURI page [ORCIDAuthentication.aspx] we get extra params from query string & fluent specific objects
    '     'Inside Page Load, if not page.ispostback...

    '     If Not String.IsNullOrWhiteSpace(Code) Then 'code is returned on the QueryString by ORCID

    '         'This must be the same as the redirectURI passed in to get the Access code
    '         Dim redirect_uri As String = HttpUtility.UrlEncode("~/ORCIDAuthentication.aspx?id1=" & 1 & "&id2=" & 2)

    '         ' Get Token
    '         Dim AuthToken As Object = GetAccessToken(Code, ORCIDObject, redirect_uri)

    '         'Make sure token has been returned
    '         If Not AuthToken Is Nothing Then
    '             Dim orcidID As String = AuthToken("orcid")

    '             'Also save token as it exists with long life (as advised by ORCID...)

    '         End If
    '     End If



    ' End Sub

    ' Public Function GetAccessToken(ByVal AuthCode As String, ByVal AuthProvider As AuthenticationProvider, ByVal Redirect_Uri As String, Optional ByVal personId As Integer = 0) As Object

    '     Dim token As New Object
    '     Using client As New WebClient()
    '         Try
    'Dim response As Byte() = client.UploadValues(AuthProvider.TokenEndPoint, New NameValueCollection{
    '	{"client_id", AuthProvider.ClientId},
    '	{"client_secret", AuthProvider.Secret},
    '	{"grant_type", "authorization_code"},
    '	{"code", AuthCode},
    '	{"redirect_uri", Redirect_Uri}
    '})

    '             Dim jsc As New System.Web.Script.Serialization.JavaScriptSerializer()
    '             Dim result As String = System.Text.Encoding.UTF8.GetString(Response)

    '             token = jsc.Deserialize(Of Object)(result)

    '         Catch ex As Exception
    '             Return Nothing
    '         End Try
    '     End Using

    '     Return token

    ' End Function




    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            Try

                'Dim URL As String = "https://orcid.org/oauth/authorize?client_id=APP-ID59GM8T9DT29K07&response_type=code&scope=/authenticate&family_names=Finn&given_names=Huckleberry&email=huck%40mailinator.com&lang=es&redirect_uri=http://serverdev/campusvirtual/personal/USATCRIS/PresentacionORCID.aspx&show_login=true"

                Dim URL As String = "https://sandbox.orcid.org/oauth/authorize?client_id=APP-1F3MW3HOFU4NP0H3&response_type=code&scope=/authenticate&family_names=Finn&given_names=Huckleberry&email=huck%40mailinator.com&lang=es&redirect_uri=http://serverdev/campusvirtual/personal/USATCRIS/PresentacionORCID.aspx&show_login=true"

                'Response.Redirect(URL, False)

                ClientScript.RegisterClientScriptBlock(Me.GetType(), "Prueba", "window.open('" + URL + "', '_blank','toolbar=no, scrollbars=yes, width=500, height=600, top=500, left=500')", True)

            Catch ex As Exception
                Response.Write(ex.Message)
            End Try
        End If
    End Sub
End Class
