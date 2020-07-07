Imports System.Collections.Generic


Partial Class DataJson_Logistica_processProcedimientoCEFO
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer
        Dim JSONresult As String = ""
        Dim Data As New Dictionary(Of String, Object)()
        Dim List As New List(Of Dictionary(Of String, Object))()
        Try
            Dim varPost As String = Request("param")
            Dim codigo_test As Integer = Request("param1")
            Select Case varPost
                Case "lstProcedimiento"
                    mt_ListarProcedimiento(codigo_test)
                Case "lstTratamiento"
                    mt_ListarTratamiento(codigo_test)
            End Select
        Catch ex As Exception
            Data.Add("Mensaje Error", ex.Message & " - " & ex.StackTrace)
            List.Add(Data)
            JSONresult = serializer.Serialize(List)
            Response.Write(JSONresult)
        End Try
    End Sub

    Private Sub mt_ListarProcedimiento(ByVal codigo_test As Integer)
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer
        Dim JSONresult As String = ""
        Dim Data As New Dictionary(Of String, Object)()
        Dim List As New List(Of Dictionary(Of String, Object))()
        Try
            Dim obj As New ClsConectarDatos
            Dim dt As Data.DataTable
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            obj.AbrirConexion()
            dt = obj.TraerDataTable("ODO_ListarProcedimiento", -1, codigo_test, 1, "")
            obj.CerrarConexion()
            For i As Integer = 0 To dt.Rows.Count - 1
                Dim row As New Dictionary(Of String, Object)()
                row.Add("cod", dt.Rows(i).Item("codigo_pro"))
                row.Add("nom", dt.Rows(i).Item("nombre_pro"))
                List.Add(row)
            Next
            JSONresult = serializer.Serialize(List)
            Response.Write(JSONresult)
        Catch ex As Exception
            Data.Add("Mensaje Error", ex.Message & " - " & ex.StackTrace)
            List.Add(Data)
            JSONresult = serializer.Serialize(List)
            Response.Write(JSONresult)
        End Try
    End Sub

    Private Sub mt_ListarTratamiento(ByVal codigo_test As Integer)
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer
        Dim JSONresult As String = ""
        Dim Data As New Dictionary(Of String, Object)()
        Dim List As New List(Of Dictionary(Of String, Object))()
        Try
            Dim obj As New ClsConectarDatos
            Dim dt As Data.DataTable
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            obj.AbrirConexion()
            dt = obj.TraerDataTable("ODO_BuscaPaqueteOdonto_V2", -1, codigo_test, "")
            obj.CerrarConexion()
            For i As Integer = 0 To dt.Rows.Count - 1
                Dim row As New Dictionary(Of String, Object)()
                row.Add("cod", dt.Rows(i).Item("codigo_paq"))
                row.Add("nom", dt.Rows(i).Item("nombre_paq"))
                List.Add(row)
            Next
            JSONresult = serializer.Serialize(List)
            Response.Write(JSONresult)
        Catch ex As Exception
            Data.Add("Mensaje Error", ex.Message & " - " & ex.StackTrace)
            List.Add(Data)
            JSONresult = serializer.Serialize(List)
            Response.Write(JSONresult)
        End Try
    End Sub

End Class
