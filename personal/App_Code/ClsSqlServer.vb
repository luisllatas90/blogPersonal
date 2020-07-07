
Imports Microsoft.VisualBasic  
   
Public Class ClsSqlServer        
    Inherits ClsDatos   

    Sub New()          
    End Sub

    Sub New(ByVal CadenaConexion As String)    
        Me.New()
        Me.CadenaConexion = CadenaConexion
    End Sub

    Sub New(ByVal Servidor As String, ByVal Base As String, ByVal Usuario As String, ByVal Password As String)
        Me.New()
        Me.Base = Base
        Me.Servidor = Servidor
        Me.User = Usuario
        Me.Password = Password
    End Sub

    Public Overrides Property CadenaConexion() As String
        Get
            If Len(MyBase.mCadenaConexion) = 0 Then
                If Len(Me.Servidor) <> 0 And Len(Me.Base) <> 0 Then
                    Dim sCadena As New System.Text.StringBuilder( _
                    "data source=<SERVIDOR>;" & _
                    "initial catalog=<BASE>;password=<PASS>;" & _
                    "persist security info=True;" & _
                    "user id=<USER>;packet size=4096")
                    sCadena.Replace("<SERVIDOR>", Me.Servidor)
                    sCadena.Replace("<BASE>", Me.Base)
                    sCadena.Replace("<PASS>", Me.Password)
                    sCadena.Replace("<USER>", Me.User)
                    mCadenaConexion = sCadena.ToString
                Else
                    Throw New System.Exception("No se puede establecer la cadena de conexión")
                End If
            End If
            Return mCadenaConexion
        End Get
        Set(ByVal Value As String)
            mCadenaConexion = Value
        End Set
    End Property

    Protected Overrides Sub CargarParametros(ByVal Comando As System.Data.IDbCommand, ByVal Args() As Object)
        Dim i As Integer
        With Comando
            For i = 0 To Args.GetUpperBound(0)
                .Parameters(i + 1).Value = Args(i)
            Next
        End With

    End Sub

    Shared mColComandos As New System.Collections.Hashtable()

    Protected Overrides Function Comando(ByVal ProcedimientoAlmacenado As String) As System.Data.IDbCommand
        Dim mComando As System.Data.SqlClient.SqlCommand
        If mColComandos.Contains(ProcedimientoAlmacenado) Then
            mComando = CType(mColComandos.Item(ProcedimientoAlmacenado), System.Data.SqlClient.SqlCommand)
        Else
            Dim oConexion2 As New System.Data.SqlClient.SqlConnection(CadenaConexion)
            oConexion2.Open()
            mComando = New System.Data.SqlClient.SqlCommand(ProcedimientoAlmacenado, oConexion2)
            Dim mConstructor As New System.Data.SqlClient.SqlCommandBuilder()
            mComando.CommandType = Data.CommandType.StoredProcedure
            'mConstructor.DeriveParameters(mComando)
            Data.SqlClient.SqlCommandBuilder.DeriveParameters(mComando)
            oConexion2.Close()
            mColComandos.Add(ProcedimientoAlmacenado, mComando)
        End If
        With mComando
            .Connection = Me.Conexion
            .Transaction = MyBase.mTransaccion
        End With
        Return mComando
    End Function

    Protected Overrides Function CrearConexion(ByVal Cadena As String) As System.Data.IDbConnection
        Return New System.Data.SqlClient.SqlConnection(Cadena)
    End Function

    Protected Overrides Function CrearDataAdapter(ByVal ProcedimientoAlmacenado As String, ByVal ParamArray Args() As Object) As System.Data.IDataAdapter
        Dim mCom As System.Data.SqlClient.SqlCommand = Comando(ProcedimientoAlmacenado)
        ' Si se han recibido Argumentos, 
        'se procede a asignar los valores correspondientes
        If Not Args Is Nothing Then
            CargarParametros(mCom, Args)
        End If
        Return New System.Data.SqlClient.SqlDataAdapter(mCom)
    End Function

End Class
