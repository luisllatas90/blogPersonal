Imports System.Net
Imports System.ServiceModel

Partial Class _Default
    Inherits System.Web.UI.Page

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try

        
            'Dim basicAuthBinding As New BasicHttpBinding(BasicHttpSecurityMode.TransportCredentialOnly)
            'basicAuthBinding.Security.Transport.ClientCredentialType = HttpClientCredentialType.Basic

            'Dim basicAuthEndpoint As New EndpointAddress("http://serverdev/campusvirtual/ArchivosCompartidos/SharedFiles.asmx")
            'Dim obj As New SharedFiles.SharedFilesSoapClient(basicAuthBinding, basicAuthEndpoint)
            Dim obj As New SharedFiles.SharedFilesSoapClient()
            Dim rpt As String = "OK"
            'Dim cred As New NetworkCredential("USAT\esaavedra", "esaavedra")
            'Dim cred As New NetworkCredential("serverdev\esaavedra", "esaavedra", "usat.edu.pe")

            'obj.Endpoint.Binding = basicAuthBinding

            obj.ClientCredentials.UserName.UserName = "SERVERDEV\esaavedra"
            obj.ClientCredentials.UserName.Password = "esaavedra"

            rpt = obj.DownloadFile("0x01000000F7D0D88E5BD96B01A501A79099C3EA56930B04544C8CDB50", "USAT\esaavedra", "BUJMP5SUYJ", "D:\demo.xlsx")
            Response.Write(rpt)
        Catch ex As Exception
            Response.Write(ex.Message.ToString & " --- " & ex.StackTrace.ToString)
        End Try
    End Sub

    Protected Sub Button2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button2.Click
        Dim obj As New SharedLocal.SharedFilesSoapClient
        Dim rpt As String = "OK"
        'Dim cred As New NetworkCredential("USAT\esaavedra", "esaavedra", "USAT")
        'obj.Credentials = cred
        'obj.Proxy.Credentials = cred
        rpt = obj.DownloadFile("0x0100000020316639297345CFCDBEE5DEB6A5999F62C582F708FC2F92", "SERVERDEV\esaavedra", "MLKAPL5N9B", "D:\743.pdf") 'Pruebas localhost         
        Response.Write(rpt)
    End Sub

    Protected Sub Button3_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button3.Click

    End Sub
End Class
