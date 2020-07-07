Imports Microsoft.VisualBasic

Public Class ClsPropuesta
    'Declarar un Arraylist para contener los registros
    Private Shared Detalle As New ArrayList

    'Crear Atributos
    Private m_codigo_prp As Integer = 0
    Private m_nombre_prp As String = ""

#Region "Propiedades de los Campos en las Grillas"
    Public Property codigo_prp() As Integer
        Get
            Return m_codigo_prp
        End Get
        Set(ByVal value As Integer)
            m_codigo_prp = value
        End Set
    End Property

    Public Property nombre_prp() As String
        Get
            Return m_nombre_prp
        End Get
        Set(ByVal value As String)
            m_nombre_prp = value
        End Set
    End Property

#End Region

    Public Sub AgregarItemDetalle(ByVal codigo_prp As Integer, ByVal nombre_prp As String)
        Me.codigo_prp = codigo_prp
        Me.nombre_prp = nombre_prp
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
