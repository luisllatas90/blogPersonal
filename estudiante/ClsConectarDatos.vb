Imports Microsoft.VisualBasic
Imports System.Data  
Imports System.Data.SqlClient
 
Public Class ClsConectarDatos

#Region "Declaracion de Variables"
    Private Cnx As SqlConnection     ' Controlar las conexiones
    Private Trans As SqlTransaction  ' Controlar las trasnacciones
    Private EnTransaccion As Boolean ' Define si se encuentra en transaccion un comando
    Private strCadena As String ' Cadena de conexion
#End Region

#Region "Propiedades"
    Public WriteOnly Property CadenaConexion() As String   'Propiedad de solo lectura
        Set(ByVal value As String)
            strCadena = value
        End Set
    End Property
#End Region

#Region "Metodos"

    Public Sub AbrirConexion()
        Cnx = New SqlConnection
        Cnx.ConnectionString = strCadena
        If Cnx.State = ConnectionState.Closed Then
            Cnx.Open()
        End If
    End Sub

    Public Sub CerrarConexion()
        If Cnx.State = ConnectionState.Open Then
            Cnx.Close()
            Cnx.Dispose()
        End If
    End Sub

    Public Sub IniciarTransaccion()
        Call AbrirConexion()
        EnTransaccion = True
        Trans = Cnx.BeginTransaction
    End Sub

    Public Sub TerminarTransaccion()
        If EnTransaccion = True Then
            Trans.Commit()
            EnTransaccion = False
        End If
        Call CerrarConexion()
    End Sub

    Public Sub AbortarTransaccion()
        If EnTransaccion = True Then
            Trans.Rollback()
            EnTransaccion = False
        End If
        Call CerrarConexion()
    End Sub

    Public Function TraerDataTable(ByVal nombreprocedimiento As String) As DataTable
        Dim Cmd As SqlCommand
        Dim Adaptador As SqlDataAdapter
        Dim DatosDevueltos As DataTable

        Cmd = New SqlCommand
        Cmd.Connection = Cnx        'Asigno la conexion
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.CommandTimeout = 0
        Cmd.CommandText = nombreprocedimiento

        If EnTransaccion = True Then
            Cmd.Transaction = Trans
        End If

        Adaptador = New SqlDataAdapter
        DatosDevueltos = New DataTable
        Adaptador.SelectCommand = Cmd
        Adaptador.Fill(DatosDevueltos)

        Return DatosDevueltos

    End Function

    Public Function TraerDataTable(ByVal nombreprocedimiento As String, ByVal ParamArray Parametros() As Object) As DataTable
        Dim Cmd As SqlCommand
        Dim Adaptador As SqlDataAdapter
        Dim DatosDevueltos As DataTable

        Cmd = New SqlCommand
        Cmd.Connection = Cnx        'Asigno la conexion
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.CommandTimeout = 0
        Cmd.CommandText = nombreprocedimiento

        If EnTransaccion = True Then
            Cmd.Transaction = Trans
        End If

        SqlClient.SqlCommandBuilder.DeriveParameters(Cmd)
        For i As Integer = 0 To Parametros.GetUpperBound(0)
            Cmd.Parameters(i + 1).Value = Parametros(i)
        Next

        Adaptador = New SqlDataAdapter
        DatosDevueltos = New DataTable
        Adaptador.SelectCommand = Cmd
        Adaptador.Fill(DatosDevueltos)

        Return DatosDevueltos

    End Function

    Public Function Ejecutar(ByVal nombreprocedimiento As String) As Integer
        Dim cmd As SqlCommand
        Dim FilasAfectadas As Integer

        cmd = New SqlCommand
        cmd.Connection = Cnx
        cmd.CommandType = CommandType.StoredProcedure
        cmd.CommandText = nombreprocedimiento
        If EnTransaccion = True Then
            cmd.Transaction = Trans
        End If

        FilasAfectadas = cmd.ExecuteNonQuery()
        Return FilasAfectadas

    End Function

    Public Function Ejecutar(ByVal nombreprocedimiento As String, ByVal ParamArray Parametros() As Object) As Object
        Dim cmd As SqlCommand
        Dim FilasAfectadas As Integer
        Dim ValoresDevueltos() As Object

        cmd = New SqlCommand
        cmd.Connection = Cnx
        cmd.CommandType = CommandType.StoredProcedure
        cmd.CommandText = nombreprocedimiento
        If EnTransaccion = True Then
            cmd.Transaction = Trans
        End If

        SqlClient.SqlCommandBuilder.DeriveParameters(cmd)
        For i As Integer = 0 To Parametros.GetUpperBound(0)
            cmd.Parameters(i + 1).Value = Parametros(i)
        Next

        FilasAfectadas = cmd.ExecuteNonQuery
        Dim Par As System.Data.IDataParameter

        Dim Contador As Integer = 0   'Cantidad de parametros de salida para el usuario
        For Each Par In cmd.Parameters
            If Par.Direction = ParameterDirection.Output Or Par.Direction = ParameterDirection.InputOutput Then
                ReDim Preserve ValoresDevueltos(Contador)
                ValoresDevueltos.SetValue(Par.Value, Contador)
                Contador = Contador + 1
            End If
        Next

        If ValoresDevueltos IsNot Nothing AndAlso ValoresDevueltos.Length > 0 Then
            Return ValoresDevueltos   ' Si existen variables output devuelve el array
        Else
            Return FilasAfectadas     ' Si no devuelve el numero de filas afectadas
        End If


    End Function

#End Region


End Class
