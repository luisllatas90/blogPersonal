Imports Microsoft.VisualBasic   
Imports System.Data


Public MustInherit Class ClsDatos

#Region "Declaracion de Variables"
    ' ##############################################################################
    Protected mServidor As String
    Protected mBase As String
    Protected mCadenaConexion As String
    Protected mConexion As IDbConnection
    Protected mUser As String
    Protected mPass As String
    ' ##############################################################################
#End Region

#Region "Constructores"

#End Region

#Region "Propiedades"
    ' ##############################################################################
    'Nombre del Servidor de Base de Datos
    Public Property Servidor() As String
        Get
            Return mServidor
        End Get
        Set(ByVal Value As String)
            mServidor = Value
        End Set
    End Property

    'Nombre del Usuario
    Public Property User() As String
        Get
            Return mUser
        End Get
        Set(ByVal value As String)
            mUser = value
        End Set
    End Property

    'Nombre de la contraseña
    Public Property Password() As String
        Get
            Return mPass
        End Get
        Set(ByVal value As String)
            mPass = value
        End Set
    End Property

    'Nombre de la base de Datos
    Public Property Base() As String
        Get
            Return mBase
        End Get
        Set(ByVal Value As String)
            mBase = Value
        End Set
    End Property

    ' Definición de la cadena de Conexión
    Public MustOverride Property CadenaConexion() As String

    ' ##############################################################################
#End Region

#Region "Acciones"
    ' ##############################################################################
    Protected MustOverride Function CrearConexion(ByVal Cadena As String) As System.Data.IDbConnection
    Protected MustOverride Function Comando(ByVal ProcedimientoAlmacenado As String) As System.Data.IDbCommand
    Protected MustOverride Function CrearDataAdapter(ByVal ProcedimientoAlmacenado As String, ByVal ParamArray Args() As System.Object) As System.Data.IDataAdapter
    Protected MustOverride Sub CargarParametros(ByVal Comando As System.Data.IDbCommand, ByVal Args() As System.Object)
    ' ##############################################################################
#End Region


#Region "Privadas"
    ' ##############################################################################
    Protected ReadOnly Property Conexion() As System.Data.IDbConnection
        Get
            If mConexion Is Nothing Then ' si no existe 
                ' llama al método de la clase que lo hereda
                mConexion = CrearConexion(Me.CadenaConexion)
            End If
            With mConexion
                ' Controla que la conexión esté abierta
                If .State <> ConnectionState.Open Then .Open()
            End With
            Return mConexion
        End Get
    End Property
    ' ##############################################################################
#End Region

#Region "Lecturas"
    ' ##############################################################################
    Public Overloads Function TraerDataSet(ByVal ProcedimientoAlmacenado As String) As System.Data.DataSet
        'Se crea el Dataset que luego será llenado y retornado
        Dim mDataSet As New System.Data.DataSet()
        CrearDataAdapter(ProcedimientoAlmacenado).Fill(mDataSet)
	If Entransaccion = false then
		mconexion.close
	end if
        Return mDataSet

    End Function

    Public Overloads Function TraerDataSet(ByVal ProcedimientoAlmacenado As String, ByVal ParamArray Argumentos() As System.Object) As System.Data.DataSet
        Dim mDataSet As New System.Data.DataSet()
        CrearDataAdapter(ProcedimientoAlmacenado, Argumentos).Fill(mDataSet)

	If Entransaccion = false then
		mconexion.close
	end if

        Return mDataSet
    End Function

    Public Overloads Function TraerDataTable(ByVal ProcedimientoAlmacenado As String) As System.Data.DataTable
        Return TraerDataSet(ProcedimientoAlmacenado).Tables(0).Copy
    End Function

    Public Overloads Function TraerDataTable(ByVal ProcedimientoAlmacenado As String, ByVal ParamArray Argumentos() As System.Object) As System.Data.DataTable
        Return TraerDataSet(ProcedimientoAlmacenado, Argumentos).Tables(0).Copy
    End Function

    Public Overloads Function TraerValor(ByVal ProcedimientoAlmacenado As String) As System.Object
        With Comando(ProcedimientoAlmacenado)
            .ExecuteNonQuery()
            Dim oPar As System.Data.IDataParameter
            For Each oPar In .Parameters
                If oPar.Direction = ParameterDirection.InputOutput Or oPar.Direction = ParameterDirection.Output Then
                    Return oPar.Value
                    Exit For
                End If
            Next
        End With
    End Function

    Public Overloads Function TraerValor(ByVal ProcedimientoAlmacenado As String, ByVal ParamArray Argumentos() As System.Object) As System.Object
        Dim mCom As System.Data.IDbCommand = Comando(ProcedimientoAlmacenado)
        CargarParametros(mCom, Argumentos)
        With mCom
            .ExecuteNonQuery()
            Dim oPar As System.Data.IDataParameter
            For Each oPar In .Parameters
                If oPar.Direction = ParameterDirection.InputOutput Or oPar.Direction = ParameterDirection.Output Then
                    Return oPar.Value
                    Exit For
                End If
            Next
        End With

    End Function

    Public Overloads Function Ejecutar(ByVal ProcedimientoAlmacenado As String) As Integer
        Return Comando(ProcedimientoAlmacenado).ExecuteNonQuery
    End Function

    Public Overloads Function Ejecutar(ByVal ProcedimientoAlmacenado As String, ByVal ParamArray Argumentos() As System.Object) As System.Object
        Dim mCom As System.Data.SqlClient.SqlCommand = Comando(ProcedimientoAlmacenado)
        Dim valorreturn As System.Object = Nothing
        CargarParametros(mCom, Argumentos)
        'Resp = mCom.ExecuteNonQuery
        'Dim oPar As System.Data.SqlClient.SqlParameter
        'Dim i As Integer
        'For i = 0 To mCom.Parameters.Count - 1
        '    With mCom.Parameters(i)
        '        If .Direction = ParameterDirection.InputOutput Or .Direction = ParameterDirection.Output Then
        '            Argumentos.SetValue(.Value, i - 1)
        '        End If
        '    End With
        'Next
        With mCom
            valorreturn = .ExecuteNonQuery()
            Dim oPar As System.Data.IDataParameter
            For Each oPar In .Parameters
                If oPar.Direction = ParameterDirection.Output Or oPar.Direction = ParameterDirection.InputOutput Then
                    valorreturn = oPar.Value
                    Exit For
                    'valorreturn = Nothing
                End If
            Next
        End With
        Return valorreturn
    End Function

    ' ##############################################################################
#End Region

#Region "Transaccion"
    ' ##############################################################################
    Protected mTransaccion As System.Data.IDbTransaction
    Protected EnTransaccion As Boolean

    Public Sub IniciarTransaccion()
        mTransaccion = Me.Conexion.BeginTransaction
        EnTransaccion = True
    End Sub

    Public Sub TerminarTransaccion()
        Try
            mTransaccion.Commit()
        Catch ex As System.Exception
            'Throw ex
        Finally
            EnTransaccion = False
            mTransaccion = Nothing
        End Try

    End Sub

    Public Sub AbortarTransaccion()
        Try
            mTransaccion.Rollback()
        Catch Ex As System.Exception
            'Throw Ex
        Finally
            mTransaccion = Nothing
            EnTransaccion = False
        End Try
    End Sub
    ' ##############################################################################
#End Region

End Class
