Imports Microsoft.VisualBasic

Public Class ClsCarrerasProfesionales
    'Declarar un Arraylist para contener los registros
    Private Shared Detalle As New ArrayList

    'Crear Atributos
    Private m_codigo_ofe As Integer = 0
    Private m_nombre_cpf As String = ""
    Private m_codigo_cpf As Integer = 0
    Private m_codigo_ofc As Integer = 0

#Region "Propiedades de los Campos en las Grillas"
    Public Property Codigo_ofe() As Integer
        Get
            Return m_codigo_ofe
        End Get
        Set(ByVal value As Integer)
            m_codigo_ofe = value
        End Set
    End Property

    Public Property Nombre_cpf() As String
        Get
            Return m_nombre_cpf
        End Get
        Set(ByVal value As String)
            m_nombre_cpf = value
        End Set
    End Property

    Public Property Codigo_cpf() As Integer
        Get
            Return m_codigo_cpf
        End Get
        Set(ByVal value As Integer)
            m_codigo_cpf = value
        End Set
    End Property

    Public Property Codigo_ofc() As Integer
        Get
            Return m_codigo_ofc
        End Get
        Set(ByVal value As Integer)
            m_codigo_ofc = value
        End Set
    End Property
#End Region

    Public Sub AgregarItemDetalle(ByVal codigo_ofe As Integer, ByVal nombre_cpf As String, ByVal codigo_cpf As Integer, ByVal codigo_ofc As Integer)
        Me.Codigo_ofe = codigo_ofe
        Me.Nombre_cpf = nombre_cpf
        Me.Codigo_cpf = codigo_cpf
        Me.Codigo_ofc = codigo_ofc

        Detalle.Add(Me)
    End Sub

    Public Function ConsultarDetalle() As ArrayList
        Return Detalle
    End Function

    Public Sub wf_limpiarGridView()
        Detalle.Clear()
    End Sub

    Public Sub wf_EliminarItem(ByVal li_item As Integer)
        If Detalle.Count > -1 Then
            For i As Integer = 0 To Detalle.Count - 1
                If i = li_item Then
                    Detalle.RemoveAt(li_item)
                    Exit For
                End If
            Next
        End If
    End Sub


End Class
