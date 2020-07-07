Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient

Public Class Academico
    Private _strCadena As String = ConfigurationManager.ConnectionStrings("BDUSATConnectionString").ConnectionString
    Public Function ConsultarPromedio(ByVal tipo As String, ByVal param1 As String, ByVal param2 As String, ByVal param3 As String) As DataTable
        Dim obj As New ClsSqlServer(_strCadena)
        Try
            Return obj.TraerDataTable("ConsultarPonderado", tipo, param1, param2, param3)
        Catch ex As Exception
            Return Nothing
        End Try
        obj = Nothing
    End Function
End Class
