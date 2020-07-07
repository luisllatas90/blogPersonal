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

End Class
