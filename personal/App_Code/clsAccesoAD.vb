Imports System.DirectoryServices
Imports Microsoft.VisualBasic

Public Class clsAccesoAD

    Public Function ValidarUsuario(ByVal usuario As String, ByVal clave As String) As Boolean
        Dim strDominio As String = ""
        Dim strArray() As String
        Dim i As Integer

        Try
            strArray = Split("USAT.EDU.PE", ".")
            strDominio = "LDAP://DC=" & strArray(LBound(strArray))
            For i = LBound(strArray) + 1 To UBound(strArray)
                strDominio = strDominio & ",DC=" & strArray(i)
            Next

            Dim objUser As DirectoryEntry
            Dim objDirectoryEntry As New DirectoryEntry(strDominio, usuario, clave)
            Dim objDirectorySearcher As New DirectorySearcher(objDirectoryEntry)
            Dim objSearchResult As SearchResult

            'objDirectorySearcher.Filter = "(SAMAccountName=" & "USAT\CSENMACHE" & ")"
            objSearchResult = objDirectorySearcher.FindOne()
            objUser = objSearchResult.GetDirectoryEntry()

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function CambiarClave(ByVal usuario As String, ByVal clave As String, ByVal nuevaclave As String) As String
        Dim strDominio As String = ""
        Dim strArray() As String
        Dim i As Integer
        Try
            strArray = Split("USAT.EDU.PE", ".")
            strDominio = "LDAP://DC=" & strArray(LBound(strArray))
            For i = LBound(strArray) + 1 To UBound(strArray)
                strDominio = strDominio & ",DC=" & strArray(i)
            Next

            Dim objUser As DirectoryEntry
            Dim objDirectoryEntry As New DirectoryEntry(strDominio, usuario, clave)
            Dim objDirectorySearcher As New DirectorySearcher(objDirectoryEntry)
            Dim objSearchResult As SearchResult

            'objDirectorySearcher.Filter = "(SAMAccountName=" & "USAT\CSENMACHE" & ")"
            objSearchResult = objDirectorySearcher.FindOne()
            objUser = objSearchResult.GetDirectoryEntry()
            Dim args As Object() = {"" & clave & "", "" & nuevaclave & ""}
            objUser.InvokeSet("ChangePassword", args)
            objUser.CommitChanges()


            'strArray = Split("USAT.EDU.PE", ".")
            'strDominio = "LDAP://DC=" & strArray(LBound(strArray))

            'Dim de As New DirectoryEntry(strDominio, usuario, clave)
            'Dim ds As New DirectorySearcher(de)
            'Dim qry As String = String.Format("(&(objectCategory=User)(sAMAccountName={0}))", usuario)
            'ds.Filter = qry
            'ds.Sort.PropertyName = "cn"

            'Dim sr As SearchResult = ds.FindOne
            'Dim user As DirectoryEntry = sr.GetDirectoryEntry()
            'Dim args As Object() = {"" & clave & "", "" & nuevaclave & ""}
            'user.Invoke("ChangePassword", args)
            'user.CommitChanges()

            Return "Ok"
        Catch ex As Exception
            Return ex.Message
        End Try
    End Function
End Class
