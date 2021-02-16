Imports Microsoft.VisualBasic
Imports System

Public Class ClsDataSesion

    Public nCodUsuario As Integer
    Public cPerLogin As String
    Public cDesUsuario As String

    Public Sub New(p_nCodUsuario As Integer, p_nPerLogin As String, p_cDesUsuario As String)
        nCodUsuario = p_nCodUsuario
        cPerLogin = p_nPerLogin
        cDesUsuario = p_cDesUsuario
        ExisteSesion()
    End Sub
    Public Sub New()
        nCodUsuario = 0
        cPerLogin = ""
        cDesUsuario = ""
    End Sub

    Private Sub ExisteSesion()
        If nCodUsuario = 0 Then
            Throw New Exception("NO EXISTE SESION ACTUALMENTE VOLVERSE A LOGEAR")
        End If
    End Sub

End Class
