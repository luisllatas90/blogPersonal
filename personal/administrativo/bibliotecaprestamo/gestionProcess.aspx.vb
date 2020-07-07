Imports System.Data
Imports System.Collections.Generic
Imports System.Web

Partial Class gestionProcess
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim cDatos As New cDatos
        Dim JSONresult As String = ""
        Dim resp As Integer
        If Request("param0") = "addAmbiente" Then
            resp = cDatos.RegistrarAmbiente(Request("nombreAmbiente"))
            Dim list As New List(Of Dictionary(Of String, Object))()
            Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
            Dim dictr As New Dictionary(Of String, Object)()
            dictr.Add("r", True)
            dictr.Add("alert", "success")
            dictr.Add("msje", "Se registro con exito")
            list.Add(dictr)
            JSONresult = serializer.Serialize(list)
        ElseIf Request("param0") = "updateAmbiente" Then
            resp = cDatos.EditarAmbiente(Request("nombreAmbiente"), Request("estado"), Request("ambiente_id"))
            Dim list As New List(Of Dictionary(Of String, Object))()
            Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
            Dim dictr As New Dictionary(Of String, Object)()
            dictr.Add("r", True)
            dictr.Add("alert", "success")
            dictr.Add("msje", "Se registro con exito")
            list.Add(dictr)
            JSONresult = serializer.Serialize(list)
        ElseIf Request("param0") = "addEquipo" Then
            resp = cDatos.RegistrarEquipo(Request("nombreEquipo"), Request("ambiente_id"))
            Dim list As New List(Of Dictionary(Of String, Object))()
            Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
            Dim dictr As New Dictionary(Of String, Object)()
            dictr.Add("r", True)
            dictr.Add("alert", "success")
            dictr.Add("msje", "Se registro con exito")
            list.Add(dictr)
            JSONresult = serializer.Serialize(list)
        ElseIf Request("param0") = "updateEquipo" Then
            resp = cDatos.EditarEquipo(Request("nombreEquipo"), Request("estado"), Request("ambiente_id"), Request("equipo_id"))
            Dim list As New List(Of Dictionary(Of String, Object))()
            Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
            Dim dictr As New Dictionary(Of String, Object)()
            dictr.Add("r", True)
            dictr.Add("alert", "success")
            dictr.Add("msje", "Se registro con exito")
            list.Add(dictr)
            JSONresult = serializer.Serialize(list)
        ElseIf Request("param0") = "listEquipo" Then
            Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
            Dim dtr As New DataTable
            dtr = cDatos.ConsultaEquipo()
            Dim list As New List(Of Dictionary(Of String, Object))()
            For i As Integer = 0 To dtr.Rows.Count - 1
                Dim dict As New Dictionary(Of String, Object)()
                dict.Add("equipo_id", dtr.Rows(i).Item("equipo_id"))
                dict.Add("nombre_equipo", dtr.Rows(i).Item("nombre_equipo"))
                dict.Add("estado_equipo", dtr.Rows(i).Item("estado_equipo"))
                dict.Add("ambiente_id", dtr.Rows(i).Item("ambiente_id"))
                dict.Add("nombre_ambiente", dtr.Rows(i).Item("nombre_ambiente"))
                list.Add(dict)
            Next
            JSONresult = serializer.Serialize(list)
        ElseIf Request("param0") = "listAmbiente" Then
            Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
            Dim dtr As New DataTable
            dtr = cDatos.ConsultaAmbiente()
            Dim list As New List(Of Dictionary(Of String, Object))()
            For i As Integer = 0 To dtr.Rows.Count - 1
                Dim dict As New Dictionary(Of String, Object)()
                dict.Add("ambiente_id", dtr.Rows(i).Item("ambiente_id"))
                dict.Add("nombre_ambiente", dtr.Rows(i).Item("nombre_ambiente"))
                dict.Add("estado", dtr.Rows(i).Item("estado_ambiente"))
                list.Add(dict)
            Next
            JSONresult = serializer.Serialize(list)
			
			'06/08/2018
		ElseIf Request("param0") = "listTipo" Then
            Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
            Dim dtr As New DataTable
            If Request("tipo") = "1" Then
                dtr = cDatos.ListEstudiantes()
            ElseIf Request("tipo") = "2" Then
                dtr = cDatos.ListColaboradores()
            End If
            Dim list As New List(Of Dictionary(Of String, Object))()
            For i As Integer = 0 To dtr.Rows.Count - 1
                Dim dict As New Dictionary(Of String, Object)()
                dict.Add("codigo", dtr.Rows(i).Item("codigo"))
                dict.Add("nombre", dtr.Rows(i).Item("nombre"))
                dict.Add("suspension_id", dtr.Rows(i).Item("suspension_id"))
                dict.Add("fecha_inicio", dtr.Rows(i).Item("fecha_inicio"))
                dict.Add("fecha_fin", dtr.Rows(i).Item("fecha_fin"))
                dict.Add("usuario_id", dtr.Rows(i).Item("usuario_id"))
                list.Add(dict)
            Next
            JSONresult = serializer.Serialize(list)
        ElseIf Request("param0") = "registerEstudianteSuspension" Then
            Dim list As New List(Of Dictionary(Of String, Object))()
            Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
            If Request("usuario") = "" Then
                Dim dtr As New DataTable
                cDatos.RegisterUsuarioEstudianteSuspension(Request("codigo"))
                dtr = cDatos.ObtenerMaximoUsuario()
                cDatos.RegisterSuspensionByUsuario(dtr.Rows(0).Item("codigo"), Request("fechaInicio"), Request("fechaFin"))
            Else
                cDatos.UpdateUsuarioSuspension(Request("usuario"), Request("flag_equipo"))
                cDatos.RegisterSuspensionByUsuario(Request("usuario"), Request("fechaInicio"), Request("fechaFin"))
            End If
            Dim dictr As New Dictionary(Of String, Object)()
            dictr.Add("r", True)
            dictr.Add("alert", "success")
            dictr.Add("msje", "Se registro con exito")
            List.Add(dictr)
            JSONresult = serializer.Serialize(list)
        ElseIf Request("param0") = "registerColaboradorSuspension" Then
            Dim list As New List(Of Dictionary(Of String, Object))()
            Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
            If Request("usuario") = "" Then
                Dim dtr As New DataTable
                cDatos.RegisterUsuarioColaboradorSuspension(Request("codigo"))
                dtr = cDatos.ObtenerMaximoUsuario()
                cDatos.RegisterSuspensionByUsuario(dtr.Rows(0).Item("codigo"), Request("fechaInicio"), Request("fechaFin"))
            Else
                cDatos.UpdateUsuarioSuspension(Request("usuario"), Request("flag_equipo"))
                cDatos.RegisterSuspensionByUsuario(Request("usuario"), Request("fechaInicio"), Request("fechaFin"))
            End If
            Dim dictr As New Dictionary(Of String, Object)()
            dictr.Add("r", True)
            dictr.Add("alert", "success")
            dictr.Add("msje", "Se registro con exito")
            list.Add(dictr)
            JSONresult = serializer.Serialize(list)
        ElseIf Request("param0") = "habilitarSuspension" Then
            Dim list As New List(Of Dictionary(Of String, Object))()
            Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
            cDatos.UpdateUsuarioSuspension(Request("usuario"), Request("flag_equipo"))
            Dim dictr As New Dictionary(Of String, Object)()
            dictr.Add("r", True)
            dictr.Add("alert", "success")
            dictr.Add("msje", "Se registro con exito")
            list.Add(dictr)
            JSONresult = serializer.Serialize(list)

			
			
			
			
        End If
        Response.Write(JSONresult)
    End Sub
	
	
	
	
End Class
