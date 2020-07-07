Imports Microsoft.VisualBasic

Public Class ClsDetalleActividadPOA
    'Declarar un Arraylist para contener los registros
    Private Shared Detalle As New ArrayList

    'Crear Atributos
    Private m_codigo_dap As Integer = 0
    Private m_descripcion_dap As String = ""
    Private m_meta_dap As Decimal = 0
    Private m_avance_dap As Decimal = 0
    Private m_fecini_dap As String = ""
    Private m_fecfin_dap As String = ""
    Private m_responsable_dap As Integer = 0
    Private m_nombreresponsable_dap As String = ""
    Private m_codigo_acp As Integer = 0
    Private m_requiere_pto As String = ""

#Region "Propiedades de los Campos en las Grillas"
    Public Property codigo_dap() As Integer
        Get
            Return m_codigo_dap
        End Get
        Set(ByVal value As Integer)
            m_codigo_dap = value
        End Set
    End Property

    Public Property descripcion_dap() As String
        Get
            Return m_descripcion_dap
        End Get
        Set(ByVal value As String)
            m_descripcion_dap = value
        End Set
    End Property

    Public Property meta_dap() As Decimal
        Get
            Return m_meta_dap
        End Get
        Set(ByVal value As Decimal)
            m_meta_dap = value
        End Set
    End Property


    Public Property avance_dap() As Decimal
        Get
            Return m_avance_dap
        End Get
        Set(ByVal value As Decimal)
            m_avance_dap = value
        End Set
    End Property

    Public Property fecini_dap() As String
        Get
            Return m_fecini_dap
        End Get
        Set(ByVal value As String)
            m_fecini_dap = value
        End Set
    End Property

    Public Property fecfin_dap() As String
        Get
            Return m_fecfin_dap
        End Get
        Set(ByVal value As String)
            m_fecfin_dap = value
        End Set
    End Property

    Public Property responsable_dap() As Integer
        Get
            Return m_responsable_dap
        End Get
        Set(ByVal value As Integer)
            m_responsable_dap = value
        End Set
    End Property

    Public Property nombreresponsable_dap() As String
        Get
            Return m_nombreresponsable_dap
        End Get
        Set(ByVal value As String)
            m_nombreresponsable_dap = value
        End Set
    End Property

    Public Property codigo_acp() As Integer
        Get
            Return m_codigo_acp
        End Get
        Set(ByVal value As Integer)
            m_codigo_acp = value
        End Set
    End Property

    Public Property requiere_pto() As String
        Get
            Return m_requiere_pto
        End Get
        Set(ByVal value As String)
            m_requiere_pto = value
        End Set
    End Property

#End Region
   
    Public Sub AgregarItemDetalle(ByVal codigo_dap As Integer, ByVal descripcion_dap As String, ByVal meta_dap As Decimal, _
                                  ByVal avance_dap As Decimal, ByVal fecini_dap As String, ByVal fecfin_dap As String, _
                                  ByVal responsable_dap As Integer, ByVal nombreresponsable_dap As String, _
                                  ByVal codigo_acp As Integer, ByVal requiere_pto As String)

        Me.codigo_dap = codigo_dap
        Me.descripcion_dap = descripcion_dap
        Me.meta_dap = meta_dap
        Me.avance_dap = avance_dap
        Me.fecini_dap = fecini_dap
        Me.fecfin_dap = fecfin_dap
        Me.responsable_dap = responsable_dap
        Me.nombreresponsable_dap = nombreresponsable_dap
        Me.codigo_acp = codigo_acp
        Me.requiere_pto = requiere_pto

        Detalle.Add(Me)
    End Sub

    Public Function ConsultarDetalle() As ArrayList
        Return Detalle
    End Function

    Public Sub wf_limpiarGridView()
        Detalle.Clear()
    End Sub


    Public Sub wf_EliminarItem(ByVal idx As Integer)
        Detalle.RemoveAt(idx)
    End Sub


    Public Function f_verificarCodigo(ByVal id As Integer) As Boolean
        Return True
    End Function
End Class
