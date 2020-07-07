Imports Microsoft.VisualBasic
Imports System.Data
'Imports System.Data.SqlClient

Public Class clsaccesodatos
    ' clase para comunicacion con archivos
    ' desarrollada por jmanay
    ' usat 2007

    Dim cn As New SqlClient.SqlConnection
    Dim tsql As SqlClient.SqlTransaction

    Dim cadenaconexion As String = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString

    Dim transaccion As Boolean

    Public Sub abrirconexion()


        'cn.ConnectionString = "Data Source=srvbd;Initial Catalog=bdusat;Integrated Security=true"
        cn.ConnectionString = cadenaconexion
        cn.Open()
        transaccion = False

    End Sub
    Public Sub cerrarconexion()

        transaccion = False
        cn.Close()
        cn.Dispose()

    End Sub

    Public Sub abrirconexiontrans()

        cn.ConnectionString = cadenaconexion
        'cn.ConnectionString = "Data Source=.;Initial Catalog=BDUSAT1;Integrated Security=true"
        cn.Open()
        tsql = cn.BeginTransaction()

        transaccion = True

    End Sub
    Public Sub cancelarconexiontrans_distribuida()

        transaccion = False
        'tsql.Rollback()
        'tsql_distribuida.Abort(, 0, 0)
        'tsql_distribuida.GetTransactionInfo()

        cn.Close()

    End Sub

    Public Sub cancelarconexiontrans()

        transaccion = False
        If transaccion = True Then
            tsql.Rollback()
        End If
        If cn.State = ConnectionState.Open Then
            cn.Close()
        End If

    End Sub
    Public Sub cerrarconexiontrans()

        transaccion = False
        tsql.Commit()
        cn.Close()

    End Sub

    Public Sub cerrarconexiontrans_distribuida()

        transaccion = False
        'tsql.Commit()
        cn.Close()

    End Sub

    Public Function consultar(ByVal procedimiento As String, ByVal ParamArray x() As Object) As DataSet
        Dim cmd_consulta As New SqlClient.SqlCommand
        Dim adap_consulta As New SqlClient.SqlDataAdapter
        Dim dts_consulta As New DataSet

        cmd_consulta.CommandType = CommandType.StoredProcedure
        cmd_consulta.CommandText = LTrim(RTrim(procedimiento))
        cmd_consulta.Connection = cn

        If transaccion = True Then
            cmd_consulta.Transaction = tsql
        End If

        Dim y As SqlClient.SqlParameter
        SqlClient.SqlCommandBuilder.DeriveParameters(cmd_consulta)
        Dim i As Integer = 0
        For Each y In cmd_consulta.Parameters
            If y.ParameterName <> "@RETURN_VALUE" Then
                y.Value = x(i)
                i = i + 1
            End If
        Next
        adap_consulta.SelectCommand = cmd_consulta
        adap_consulta.Fill(dts_consulta, "Consulta")

        consultar = dts_consulta

    End Function
    Public Function ejecutar(ByVal procedimiento As String, ByVal devuelve_valor As Boolean, ByRef rpta As Integer, ByRef mensaje As String, ByVal ParamArray x() As Object) As Object

        Dim cmd_consulta As New SqlClient.SqlCommand
        Dim dts_consulta As New DataSet

        Dim i As Integer = 0
        cmd_consulta.CommandType = CommandType.StoredProcedure
        cmd_consulta.Connection = cn
        cmd_consulta.CommandText = procedimiento
        If transaccion = True Then
            cmd_consulta.Transaction = tsql
        End If

        Dim prpta As New SqlClient.SqlParameter
        Dim pmensaje As New SqlClient.SqlParameter

        Dim y As SqlClient.SqlParameter
        SqlClient.SqlCommandBuilder.DeriveParameters(cmd_consulta)

        For Each y In cmd_consulta.Parameters
            If y.ParameterName <> "@RETURN_VALUE" Then
                y.Value = x(i)
                i = i + 1
            End If
        Next
        cmd_consulta.ExecuteNonQuery()
        For Each y In cmd_consulta.Parameters
            Select Case y.ParameterName
                Case "@rpta"
                    rpta = y.Value
                Case "@mensaje"
                    mensaje = y.Value
            End Select
        Next
        'ejecutar = 0
        ' hacemos que devuelva el ultimo parametro en la función para darle soporte a los procedimientos antiguos
        If devuelve_valor = True Then
            ejecutar = cmd_consulta.Parameters(cmd_consulta.Parameters.Count - 1).Value
        End If



    End Function
End Class
