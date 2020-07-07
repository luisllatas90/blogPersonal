Imports System.Web
Imports System.Web.Script
Imports System.Data
Imports System.Data.DataTable
Imports System.Collections.Generic
Imports System.Data.DataRow
Imports System.Data.DataColumn
Partial Class academico_cargalectiva_consultapublica_post_json
    Inherits System.Web.UI.Page
    Public Function DataTableToJSON(ByVal opc As String, ByVal param1 As String, ByVal param2 As String, ByVal param3 As String) As Object

        Dim list As New List(Of Dictionary(Of String, Object))()


        Select opc
            Case "plnEst"
                Dim dt As New DataTable
                Dim obj As New ClsConectarDatos
                obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
                obj.AbrirConexion()
                Dim objFun As New ClsFunciones
				
                dt = obj.TraerDataTable("ConsultarPlanEstudio", "AC", param2, param3)
                obj.CerrarConexion()
                obj = Nothing
                Dim i As Integer = 0

                For Each oRecord As DataRow In dt.Rows
                    If oRecord("codigo_test").ToString = "2" Then
                        Dim dict As New Dictionary(Of String, Object)()
                        dict("codigo") = oRecord("codigo_Pes").ToString
                        dict("nombre") = oRecord("descripcion_Pes").ToString
                        list.Add(dict)

                    End If

                    'Response.Write(oRecord("codigo_Pes").ToString() + " -> " + oRecord("descripcion_Pes").ToString() + "</br>")
                Next
            Case "vacCur"
                Dim dt As New DataTable
                Dim obj As New ClsConectarDatos
                obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
                obj.AbrirConexion()
                Dim objFun As New ClsFunciones
                dt = obj.TraerDataTable("cursosestimarvacantes", param1, "55", param3)
                obj.CerrarConexion()
                obj = Nothing
         

                For Each oRecord As DataRow In dt.Rows
                    Dim dict As New Dictionary(Of String, Object)()
                    dict("nombre") = oRecord("nombre_cur").ToString
                    dict("ciclo") = oRecord("ciclo_cur").ToString
                    dict("creditos") = oRecord("creditos_cur").ToString
                    dict("tipo") = oRecord("tipo_Cur").ToString
                    dict("necca") = oRecord("necca").ToString
                    dict("tdca") = oRecord("tdca").ToString
                    dict("neccapr") = oRecord("neccapr").ToString
                    dict("tdcapr") = oRecord("tdcapr").ToString
                    dict("nenmap") = oRecord("nenmap").ToString
                    dict("nenmin") = oRecord("nenmin").ToString
                    list.Add(dict)
                    'Response.Write(oRecord("codigo_Pes").ToString() + " -> " + oRecord("descripcion_Pes").ToString() + "</br>")
                Next

        End Select

        ' dict("codigo") = cod
        ' dict("nombre") = nom



        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()

        Return serializer.Serialize(list)

    End Function
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim JSONresult As String
        'JSONresult = DataTableToJSON(dt, Request("cup"))
        'JSONresult = DataTableToJSON(dt, 405206)
        'MsgBox(Request("opc"))
        JSONresult = DataTableToJSON(Request("opc"), Request("param1"), Request("param2"), Request("param3"))
        'JSONresult = DataTableToJSON("plnEst", "", "1", "")
        '405206
        Response.Write(JSONresult)
    End Sub
End Class
