Imports System.IO
Imports System.Web.HttpRequest
Imports System.Collections.Generic
Partial Class select_ajax
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim Data As New Dictionary(Of String, Object)()
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Try
            Dim k As String = Request("k")
            Dim f As String = CInt(Request("f"))

            Select Case Request("action")

                Case "cicloacad"
                    ListarCicloAcademico("TO", "")
                Case "depacad"
                    ListarDepartamentoAcademico(0, "")
                Case "depdoc"
                    ListarDepartamentoAcademicoDocente("1", k, f)
                Case "ope"
                    TiposOperacion()
            End Select

        Catch ex As Exception
            Data.Add("msje", ex.Message)
            JSONresult = serializer.Serialize(Data)
            Response.Write(JSONresult)
        End Try
    End Sub
    Private Sub TiposOperacion()
        Try
            Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
            Dim JSONresult As String = ""
            Dim obj As New ClsCRM
         
            Dim data As New Dictionary(Of String, Object)()
            data.Add("reg", obj.EncrytedString64("Reg"))
            data.Add("mod", obj.EncrytedString64("Mod"))
            data.Add("eli", obj.EncrytedString64("Eli"))
            data.Add("lst", obj.EncrytedString64("Listar"))
            data.Add("lstFl", obj.EncrytedString64("ListarFiles"))
            data.Add("upl", obj.EncrytedString64("Upload"))
            data.Add("dwl", obj.EncrytedString64("Download"))
    
            JSONresult = serializer.Serialize(data)
            Response.Write(JSONresult)

        Catch ex As Exception

        End Try
    End Sub
    Private Sub ListarCicloAcademico(ByVal tipo As String, ByVal param As String)
        Try
            Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
            Dim JSONresult As String = ""

            Dim list As New List(Of Dictionary(Of String, Object))()

            Dim obj As New ClsConectarDatos
            Dim tb As New Data.DataTable
            Dim cn As New clsaccesodatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            tb = obj.TraerDataTable("dbo.ConsultarCicloAcademico", tipo, param)
            obj.CerrarConexion()

            If tb.Rows.Count > 0 Then
                For i As Integer = 0 To tb.Rows.Count - 1
                    Dim data As New Dictionary(Of String, Object)()
                    data.Add("cCiclo", tb.Rows(i).Item("codigo_Cac"))
                    data.Add("nCiclo", tb.Rows(i).Item("descripcion_Cac"))
                    list.Add(data)
                Next
            End If
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)

        Catch ex As Exception

        End Try
    End Sub

    Private Sub ListarDepartamentoAcademico(ByVal cod As Integer, ByVal param As String)
        Try
            Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
            Dim JSONresult As String = ""

            Dim list As New List(Of Dictionary(Of String, Object))()

            Dim obj As New ClsConectarDatos
            Dim tb As New Data.DataTable
            Dim cn As New clsaccesodatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            tb = obj.TraerDataTable("dbo.ACAD_BuscaDepartamentoAcademico", cod, param)
            obj.CerrarConexion()

            If tb.Rows.Count > 0 Then
                For i As Integer = 0 To tb.Rows.Count - 1
                    Dim data As New Dictionary(Of String, Object)()
                    data.Add("cDac", tb.Rows(i).Item("codigo_Dac"))
                    data.Add("nDac", tb.Rows(i).Item("nombre_Dac"))
                    data.Add("abrDac", tb.Rows(i).Item("nombre_Dac"))
                    list.Add(data)
                Next
            End If
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)

        Catch ex As Exception

        End Try
    End Sub

    Private Sub ListarDepartamentoAcademicoDocente(ByVal tipo As String, ByVal k As Integer, ByVal f As String)
        Try
            Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
            Dim JSONresult As String = ""

            Dim list As New List(Of Dictionary(Of String, Object))()

            Dim obj As New ClsConectarDatos
            Dim tb As New Data.DataTable
            Dim cn As New clsaccesodatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            tb = obj.TraerDataTable("dbo.ACAD_DocenteporDepAcademico", tipo, k, f)
            obj.CerrarConexion()

            If tb.Rows.Count > 0 Then
                For i As Integer = 0 To tb.Rows.Count - 1
                    Dim data As New Dictionary(Of String, Object)()
                    data.Add("cDac", tb.Rows(i).Item("codigo_Dac"))
                    data.Add("cPer", tb.Rows(i).Item("codigo_Per"))
                    data.Add("nPer", tb.Rows(i).Item("Personal"))
                    list.Add(data)
                Next
            End If
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)

        Catch ex As Exception

        End Try
    End Sub

End Class
