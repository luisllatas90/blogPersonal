Imports Microsoft.VisualBasic
Imports System
Imports System.Reflection

Public Class ClsRespuestaServidor

    Public Resultado As Object
    Public LogError As ClsLogError

    Public Sub New()
        Inicializar()
    End Sub
    Private Sub Inicializar()
        Resultado = vbNull
        LogError = New ClsLogError()
    End Sub

End Class

Public Class ClsLogError

    Public nCodigo As Integer
    Public bFlag As Boolean
    Public nOrigen As Integer
    Public cMensaje As String
    Public oExcepcion As ClsExceptionUSAT

    Public Sub New()
        Inicializar()
    End Sub

    Private Sub Inicializar()
        nCodigo = 0
        bFlag = False
        nOrigen = 0
        cMensaje = "OK"
    End Sub

    Public Sub SetException(Ex As Exception)
        nCodigo = 500
        bFlag = True
        nOrigen = 1
        cMensaje = Ex.Message
        oExcepcion = New ClsExceptionUSAT(Ex)
    End Sub

    Public Sub SetException(Ex As Exception, p_nCodigo As Integer)
        nCodigo = p_nCodigo
        bFlag = True
        nOrigen = 1
        cMensaje = Ex.Message
        oExcepcion = New ClsExceptionUSAT(Ex)
    End Sub

    Public Sub SetException(p_cMensaje As String, Optional p_nCodigo As Integer = 500)
        nCodigo = p_nCodigo
        bFlag = True
        nOrigen = 0
        cMensaje = p_cMensaje
    End Sub

End Class

Public Class ClsExceptionUSAT

    Public Message As String
    Public StackTrace As String
    Public HelpLink As String
    Public Source As String
    Public HResult As Integer

    Public Sub New()

    End Sub
    Public Sub New(Ex As Exception)
        Message = Ex.Message
        StackTrace = Ex.StackTrace
        HelpLink = Ex.HelpLink
        Source = Ex.Source
    End Sub
End Class