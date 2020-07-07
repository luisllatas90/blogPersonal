Imports System.Data.SqlClient
Imports System.Data

Public Class MKTConexionBD

    Private cn As New SqlConnection
    'Conexion con usuario de SQL SERVER

    'Private cadenaconexion As String = "Data Source=(srvsql);Initial Catalog=cmusat;User ID=iusrvirtualsistema;Password=hacemoslascosasbienylashacemosmejor"
    Private cadenaconexion As String = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString

    'Conexion con usuario de Windows
    ' Private cadenaconexion As String = "Data Source=HECTOR-PC\SQLEXPRESS;Initial Catalog=BDMatricula;Integrated Security=SSPI"

    Private transaccion As Boolean
    Private tsql As SqlTransaction

    Public Sub abrirconexion()
        Try
            transaccion = False

            If cn.State <> ConnectionState.Open Then
                cn.ConnectionString = cadenaconexion
                cn.Open()
            End If
        Catch Ex As Exception
            Throw

        End Try

    End Sub
    Public Sub cerrarconexion()
        Try
            transaccion = False

            If cn.State = ConnectionState.Open Then
                cn.Close()
                cn.Dispose()

            End If
        Catch Ex As Exception
            Throw

        End Try

    End Sub
    Public Sub abrirconexionTrans()
        Try
            If transaccion <> True Then
                abrirconexion()
                tsql = cn.BeginTransaction()
                transaccion = True
            End If
        Catch ex As Exception
            Throw

        End Try

    End Sub
    Public Sub cerrarconexionTrans()

        Try
            If transaccion = True Then
                tsql.Commit()
                cerrarconexion()
                transaccion = False
            End If

        Catch ex As Exception
            Throw

        End Try

    End Sub
    Public Sub cancelarconexionTrans()
        Try

            If transaccion = True Then
                tsql.Rollback()
                cerrarconexion()
                transaccion = False
            End If


        Catch ex As Exception
            Throw

        End Try

    End Sub

    Public Sub conectarEjecutar(ByVal sql As String)
        Try
            abrirconexion()
            ejecutar(sql)
        Catch ex As Exception
            Throw
        Finally
            cerrarconexion()
        End Try
    End Sub

    Public Function conectarConsultar(ByVal sql As String) As DataTable
        Try
            abrirconexion()
            Return consultar(sql)

        Catch ex As Exception
            Throw
        Finally
            cerrarconexion()
        End Try
    End Function

    Public Sub ejecutar(ByVal sql As String) 'INSERT, UPDATE y DELETE

        Try

            Dim cmd As New SqlCommand

            cmd.CommandType = CommandType.Text
            cmd.CommandText = sql
            cmd.Connection = cn

            If transaccion = True Then
                cmd.Transaction = tsql
            End If

            cmd.ExecuteNonQuery()

        Catch ex As Exception
            Throw

        End Try

    End Sub

    Public Function consultar(ByVal sql As String) As DataTable 'SELECT 

        Try

            Dim cmd As New SqlCommand

            Dim adap As New SqlDataAdapter 'Necesitamos este puente para poder colocar el resultado de la consulta en el dataset
            Dim dts As New DataSet
            Dim dtt As New DataTable

            cmd.CommandType = CommandType.Text
            cmd.CommandText = sql
            cmd.Connection = cn

            If transaccion = True Then
                cmd.Transaction = tsql
            End If

            adap.SelectCommand = cmd
            adap.Fill(dts, "Consulta")
            dtt = dts.Tables("Consulta")

            Return dtt

        Catch ex As Exception
            Throw

        End Try

    End Function

    Public Function generarCodigo(ByVal tabla As String, ByVal campo As String) As Object
        Try
            Dim sql As String
            Dim dtt As DataTable
            sql = "Select isnull(max(" & campo & "),0) + 1 from " & tabla

            dtt = conectarConsultar(sql)

            Return dtt.Rows(0).Item(0)

        Catch ex As Exception
            Throw
        End Try

    End Function


    '=================================================================================================================
    'Para Ejecutar Procedimientos almacenados




    Public Function conectarConsultarSP(ByVal sp As String, ByVal ParamArray x() As Object) As DataTable
        Try
            abrirconexion()
            Return consultarSP(sp, x)
        Catch ex As Exception
            Throw
        Finally
            cerrarconexion()
        End Try
    End Function


    Public Function consultarSP(ByVal procedimiento As String, ByVal ParamArray x() As Object) As DataTable

        Try


            Dim cmd As New SqlCommand
            Dim adap As New SqlDataAdapter
            Dim dts As New DataSet
            Dim dtt As New DataTable



            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = procedimiento.Trim
            cmd.CommandTimeout = 0
            cmd.Connection = cn

            If transaccion = True Then
                cmd.Transaction = tsql
            End If



            Dim y As SqlParameter
            SqlCommandBuilder.DeriveParameters(cmd) 'Recupera información de los parámetros del procedimiento almacenado especificado en SqlCommand y rellena la colección de Parameters del objeto SqlCommand especificado.

            Dim i As Integer = 0
            For Each y In cmd.Parameters
                If y.ParameterName <> "@RETURN_VALUE" Then
                    y.Value = x(i)
                    i = i + 1
                End If
            Next
            adap.SelectCommand = cmd
            adap.Fill(dts, "Consulta")
            dtt = dts.Tables("Consulta")

            Return dtt
        Catch ex As Exception
            Throw
        End Try

    End Function

    Public Function conectarEjecutarSP(ByVal sp As String, ByVal devuelve_valor As Boolean, ByVal ParamArray x() As Object) As Integer
        Try
            abrirconexion()
            Return ejecutarSP(sp, devuelve_valor, x)
        Catch ex As Exception
            Throw
        Finally
            cerrarconexion()
        End Try
    End Function
    Public Function ejecutarSP(ByVal procedimiento As String, ByVal devuelve_valor As Boolean, ByVal ParamArray x() As Object) As Integer

        Try

            Dim cmd As New SqlCommand


            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = procedimiento.Trim
            cmd.Connection = cn

            If transaccion = True Then
                cmd.Transaction = tsql
            End If


            Dim y As SqlParameter
            SqlCommandBuilder.DeriveParameters(cmd)

            Dim i As Integer = 0
            For Each y In cmd.Parameters
                If y.ParameterName <> "@RETURN_VALUE" Then
                    y.Value = x(i)
                    i = i + 1
                End If
            Next

            If devuelve_valor = True Then
                'Return cmd.Parameters(cmd.Parameters.Count - 1).Value
                Dim adap As New SqlDataAdapter
                Dim dts As New DataSet
                Dim dtt As New DataTable
                adap.SelectCommand = cmd
                adap.Fill(dts, "Consulta")
                dtt = dts.Tables("Consulta")
                Return dtt.Rows(0).Item(0)
            Else
                cmd.ExecuteNonQuery()
                Return -1
            End If

        Catch ex As Exception
            Throw
        End Try

    End Function


End Class
