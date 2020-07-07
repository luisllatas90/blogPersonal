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


    Public Function ImportarInteresados(ByVal nombre_tabla As String, ByVal datos As DataTable) As String
        Dim linea As String = "0"
        Try
            linea = linea + " - 1"

            'abro la conexión de destino
            'AbrirConexion()

            linea = linea + " - 2"

            ' creo el objeto BulkCopy
            Dim copia As New SqlBulkCopy(Cnx)
            linea = linea + " - 3"

            'le digo la tabla que va migrar
            copia.DestinationTableName = nombre_tabla
            linea = linea + " - 4"

            'copio los datos

            copia.WriteToServer(datos)
            linea = linea + " - 5"

            'cierro la conexión
            'CerrarConexion()

            linea = linea + " - 6"

            Return "Archivo Importado Correctamente. "
        Catch ex As Exception
            Return ex.Message.ToString + linea
        End Try
    End Function


    Public Function ImportarTabla(ByVal nombreprocedimiento As String, ByVal tabla As Data.DataTable) As Integer
        Dim cmd As SqlCommand
        Dim filasafectadas As Integer
        cmd = New SqlCommand
        cmd.Connection = Cnx
        cmd.CommandType = CommandType.StoredProcedure
        cmd.CommandText = nombreprocedimiento
        'NOMBRE DEL PARAMETRO DEL PROCEDIMIENTO CREADO TIENE QUE TENER EL MISMO NOMBRE DEL PARAMETRO QUE SE ASIGNA LA TABLA
        cmd.Parameters.AddWithValue("@param", tabla)
        If EnTransaccion = True Then
            cmd.Transaction = Trans
        End If

        filasafectadas = cmd.ExecuteNonQuery()
        Return filasafectadas

    End Function



#End Region

End Class
