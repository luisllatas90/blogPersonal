Imports Microsoft.VisualBasic

Public Class ClsCentroCosto
    'Declarar un Arraylist para contener los registros
    Private Shared Detalle As New ArrayList

    'Crear Atributos
    Private m_codigo_cco As Integer = 0
    Private m_descripcion_cco As String = ""
    Private m_codigo_tac As Integer = 0
    Private m_descripcion_tac As String = ""
    Private m_codigo_asp As Integer = 0


#Region "Propiedades de los Campos en las Grillas"

    Public Property codigo_cco() As Integer
        Get
            Return m_codigo_cco
        End Get
        Set(ByVal value As Integer)
            m_codigo_cco = value
        End Set
    End Property

    Public Property descripcion_cco() As String
        Get
            Return m_descripcion_cco
        End Get
        Set(ByVal value As String)
            m_descripcion_cco = value
        End Set
    End Property

    Public Property codigo_tac() As Integer
        Get
            Return m_codigo_tac
        End Get
        Set(ByVal value As Integer)
            m_codigo_tac = value
        End Set
    End Property

    Public Property descripcion_tac() As String
        Get
            Return m_descripcion_tac
        End Get
        Set(ByVal value As String)
            m_descripcion_tac = value
        End Set
    End Property

    Public Property codigo_asp() As Integer
        Get
            Return m_codigo_asp
        End Get
        Set(ByVal value As Integer)
            m_codigo_asp = value
        End Set
    End Property
#End Region

    Public Sub AgregarItemDetalle(ByVal codigo_cco As Integer, ByVal descripcion_cco As String, ByVal codigo_tac As Integer, ByVal descripcion_tac As String, ByVal codigo_asp As Integer)
        Me.codigo_cco = codigo_cco
        Me.descripcion_cco = descripcion_cco
        Me.codigo_tac = codigo_tac
        Me.descripcion_tac = descripcion_tac
        Me.codigo_asp = codigo_asp

        Detalle.Add(Me)
    End Sub

    Public Function ConsultarDetalle() As ArrayList
        Return Detalle
    End Function

    Public Sub wf_limpiarGridView()
        Detalle.Clear()
    End Sub

    'Public Sub wf_EliminarItem(ByVal id As Integer)
    '    'Dim obj As ClsCentroCosto
    '    'If Detalle.Count > -1 Then
    '    '    For x As Integer = 0 To Detalle.Count - 1
    '    '        obj = Detalle.Item(x)
    '    '        If obj.codigo_cco = id Then
    '    '            Detalle.RemoveAt(x)
    '    '            Exit For
    '    '        End If
    '    '    Next
    '    'End If

    '    Dim obj As ClsCentroCosto
    '    For x As Integer = 0 To Detalle.Count - 1
    '        obj = Detalle.Item(x)
    '        If obj.codigo_cco = id Then
    '            Detalle.RemoveAt(x)
    '            Exit For
    '        End If
    '    Next

    'End Sub

    Public Sub wf_EliminarItem(ByVal codigo_cco As Integer)
        'Dim obj As ClsCentroCosto
        'If Detalle.Count > -1 Then
        '    For x As Integer = 0 To Detalle.Count - 1
        '        obj = Detalle.Item(x)
        '        If obj.codigo_cco = id Then
        '            Detalle.RemoveAt(x)
        '            Exit For
        '        End If
        '    Next
        'End If
        Dim obj As ClsCentroCosto
        For x As Integer = 0 To Detalle.Count - 1
            obj = Detalle.Item(x)
            If obj.m_codigo_cco = codigo_cco Then
                Detalle.RemoveAt(x)
                Exit For
            End If
        Next
    End Sub


    Public Function f_verificarCodigo(ByVal id As Integer) As Boolean
        Dim obj As ClsCentroCosto
        For x As Integer = 0 To Detalle.Count - 1
            obj = Detalle.Item(x)
            If obj.codigo_cco = id Then
                Return False
                Exit For
            End If
        Next

        Return True
    End Function

End Class
