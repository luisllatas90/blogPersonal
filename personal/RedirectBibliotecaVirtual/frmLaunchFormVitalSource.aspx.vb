Imports System.Net
Imports System.IO
Imports System.Globalization
Imports System.Security.Cryptography
Imports System.Collections.Generic

Partial Class frmLaunchFormVitalSource
    Inherits NoViewStatePage

#Region "Propiedades"
    Public LaunchUrl As String = "https://bc.vitalsource.com/books/BOOKSHELF-TUTORIAL"
#End Region

#Region "Eventos"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            

            Dim userId As String = 0
            Dim roles As String = ""
            Dim nombreCompleto As String = ""
            Dim apellidos As String = ""
            Dim nombre As String = ""
            Dim email As String = ""

            Dim codigoPer As Integer = IIf(Request.QueryString("per") IsNot Nothing, Request.QueryString("per"), 0)
            If codigoPer <> 0 Then
                Dim repo As New clsPersonal
                Dim data As Data.DataTable = repo.ConsultarDatosPersonales(codigoPer)

                If data.Rows.Count = 0 Then Exit Sub
                Dim row As Data.DataRow = data.Rows(0)

                apellidos = row.Item("paterno") & " " & row.Item("materno")
                nombre = row.Item("nombres")
                nombreCompleto = nombre & " " & apellidos
                email = row.Item("mail1")

                userId = codigoPer
            End If

            GenerarControlesForm(userId, roles, nombreCompleto, apellidos, nombre, email)
        End If
    End Sub
#End Region

#Region "Funciones"
    Private Sub GenerarControlesForm(ByVal userId As String, ByVal roles As String, ByVal nombreCompleto As String, _
                                     ByVal apellidos As String, ByVal nombre As String, ByVal email As String)
        Try
            'Variables requeridas por el servicio

            Dim key As String = "llave compartida"
            Dim secret As String = "Secreto compartido"

            Dim unsortedData As New Dictionary(Of String, String)
            With unsortedData
                .Item("user_id") = userId
                .Item("roles") = roles
                .Item("lis_person_name_full") = nombreCompleto
                .Item("lis_person_name_family") = apellidos
                .Item("lis_person_name_given") = nombre
                .Item("lis_person_contact_email_primary") = email
                .Item("lti_version") = "LTI-1p0"
                .Item("lti_message_type") = "basic-lti-launch-request"
                .Item("oauth_callback") = "about:blank"
                .Item("oauth_consumer_key") = key
                .Item("oauth_callback") = "about:blank"
                .Item("oauth_version") = "1.0"
                .Item("oauth_nonce") = uniqid("", True)
                .Item("oauth_timestamp") = GetUnixMicroTime()
                .Item("oauth_signature_method") = "HMAC-SHA1"
            End With

            'Trabajo en el formulario
            ltiLaunchForm.Action = LaunchUrl

            Dim sortedData As New SortedDictionary(Of String, String)
            For Each kv As KeyValuePair(Of String, String) In unsortedData
                ltiLaunchForm.Controls.Add(GenerarHiddenControl(kv.Key, kv.Value))
                sortedData.Add(kv.Key, kv.Value)
            Next

            Dim launchParams As String() = {}
            For Each kv As KeyValuePair(Of String, String) In unsortedData
                ReDim Preserve launchParams(launchParams.Length)
                launchParams(launchParams.Length - 1) = kv.Key & "=" & RawUrlEncode(HttpUtility.UrlEncode(kv.Value))
            Next

            Dim baseString As String = "POST&" & HttpUtility.UrlEncode(LaunchUrl) & "&" & RawUrlEncode(HttpUtility.UrlEncode(String.Join("&", launchParams)))
            secret = HttpUtility.UrlEncode(secret) & "&"

            Dim signature As String = ToBase64EncodedHmacSha1(baseString, secret, True)
            unsortedData.Item("oauth_signature") = signature

            ltiLaunchForm.Controls.Add(GenerarHiddenControl("oauth_signature", signature))

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Function GenerarHiddenControl(ByVal key As String, ByVal value As String) As HtmlGenericControl
        Dim hddControl As New HtmlGenericControl
        hddControl.TagName = "input"
        hddControl.Attributes.Item("type") = "hidden"
        hddControl.Attributes.Item("name") = key
        hddControl.Attributes.Item("value") = value
        Return hddControl
    End Function

    Private Function uniqid(ByVal prefix As String, ByVal more_entropy As Boolean) As String
        If String.IsNullOrEmpty(prefix) Then
            prefix = String.Empty
        End If

        If Not more_entropy Then
            Return Left(prefix & System.Guid.NewGuid().ToString(), 13)
        Else
            Return Left(prefix & System.Guid.NewGuid().ToString() + System.Guid.NewGuid().ToString(), 23)
        End If
    End Function

    Private Function GetUnixMicroTime() As Long
        Return CLng(DateTime.UtcNow.Subtract(New DateTime(1970, 1, 1)).TotalSeconds)
    End Function

    Public Function ToBase64EncodedHmacSha1(ByVal data As String, ByVal key As String, Optional ByVal rawOutput As Boolean = False) As String
        Dim result As Boolean

        If Boolean.TryParse(key, result) Then
            key = IIf(result, 1.ToString(), 0.ToString())
        End If

        Dim keyBytes As Byte() = Encoding.UTF8.GetBytes(key)
        Dim dataBytes As Byte() = Encoding.UTF8.GetBytes(data)

        Using hmac As New HMACSHA1(keyBytes)
            Dim hash As Byte() = hmac.ComputeHash(dataBytes)

            If rawOutput Then
                Return Convert.ToBase64String(hash)
            End If

            Dim hashString As String = Replace(BitConverter.ToString(hash), "-", "")
            Return hashString.ToLower
        End Using
    End Function

    Private Function RawUrlEncode(ByVal input As String) As String
        Return input.Replace("+", "%20")
    End Function

    Public Function Encripta64(ByVal base64Decoded As String) As String
        Dim base64Encoded As String = ""
        Try
            Dim data As Byte()
            data = System.Text.UTF8Encoding.UTF8.GetBytes(base64Decoded)
            base64Encoded = System.Convert.ToBase64String(data)
        Catch ex As Exception

        End Try
        Return base64Encoded
    End Function

    'Public Function Desencripta64(ByVal base64Encoded As String) As String
    '    Dim base64Decoded As String = ""
    '    Try
    '        Dim data() As Byte
    '        data = System.Convert.FromBase64String(base64Encoded)
    '        base64Decoded = System.Text.UTF8Encoding.UTF8.GetString(data)
    '    Catch ex As Exception

    '    End Try
    '    Return base64Decoded
    'End Function
#End Region
End Class
