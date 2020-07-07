Imports System.Data
Imports System.Collections.Generic
Imports System.Web
Partial Class processreserva
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim cDatos As New cDatos
        Dim JSONresult As String = ""
        If Request("param0") = "lstReserva" Then
            'Session("id_per") 'Codigo de Personal
            Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
            Dim dts As New DataTable
            dts = cDatos.ConsultarSuspension(Session("id_per"), Request("fecha"))
            If dts.Rows.Count > 0 Then
                If dts.Rows(0).Item("flag_equipo") = 1 Then
                    Dim list As New List(Of Dictionary(Of String, Object))()
                    Dim dict As New Dictionary(Of String, Object)()
                    dict.Add("r", True)
                    dict.Add("alert", "success")
                    dict.Add("msje", "Se consultó con exito")
                    dict.Add("codigo", "0")
                    list.Add(dict)
                    JSONresult = serializer.Serialize(list)
                Else
                    Dim dtr As New DataTable
                    dtr = cDatos.ConsultaReserva(Request.QueryString("fecha"))
                    Dim list As New List(Of Dictionary(Of String, Object))
                    For i As Integer = 0 To dtr.Rows.Count - 1
                        Dim dict As New Dictionary(Of String, Object)()
                        dict.Add("idReserva", dtr.Rows(i).Item("reserva_id"))
                        dict.Add("hora_inicio", dtr.Rows(i).Item("hora_inicio"))
                        dict.Add("hora_fin", dtr.Rows(i).Item("hora_fin"))
                        dict.Add("equipo_id", dtr.Rows(i).Item("equipo_id"))
                        dict.Add("estado", dtr.Rows(i).Item("estado"))
                        list.Add(dict)
                    Next

                    Dim dte As New DataTable
                    dte = cDatos.ConsultaEquipos(Request("ambiente"))
                    Dim listEquipos As New List(Of Dictionary(Of String, Object))()
                    For i As Integer = 0 To dte.Rows.Count - 1
                        Dim dict As New Dictionary(Of String, Object)()
                        dict.Add("equipo_id", dte.Rows(i).Item("equipo_id"))
                        dict.Add("nombre_equipo", dte.Rows(i).Item("nombre_equipo"))
                        dict.Add("estado_equipo", dte.Rows(i).Item("estado_equipo"))
                        dict.Add("ambiente_id", dte.Rows(i).Item("ambiente_id"))
                        listEquipos.Add(dict)
                    Next

                    Dim Arreglo As New ArrayList
                    Arreglo.Insert(0, list)
                    Arreglo.Insert(1, listEquipos)
                    JSONresult = serializer.Serialize(Arreglo)
                End If
            Else
                Dim dtr As New DataTable
                dtr = cDatos.ConsultaReserva(Request.QueryString("fecha"))
                Dim list As New List(Of Dictionary(Of String, Object))
                For i As Integer = 0 To dtr.Rows.Count - 1
                    Dim dict As New Dictionary(Of String, Object)()
                    dict.Add("idReserva", dtr.Rows(i).Item("reserva_id"))
                    dict.Add("hora_inicio", dtr.Rows(i).Item("hora_inicio"))
                    dict.Add("hora_fin", dtr.Rows(i).Item("hora_fin"))
                    dict.Add("equipo_id", dtr.Rows(i).Item("equipo_id"))
                    dict.Add("estado", dtr.Rows(i).Item("estado"))
                    dict.Add("user", Session("id_per"))
                    list.Add(dict)
                Next

                Dim dte As New DataTable
                dte = cDatos.ConsultaEquipos(Request("ambiente"))
                Dim listEquipos As New List(Of Dictionary(Of String, Object))()
                For i As Integer = 0 To dte.Rows.Count - 1
                    Dim dict As New Dictionary(Of String, Object)()
                    dict.Add("equipo_id", dte.Rows(i).Item("equipo_id"))
                    dict.Add("nombre_equipo", dte.Rows(i).Item("nombre_equipo"))
                    dict.Add("estado_equipo", dte.Rows(i).Item("estado_equipo"))
                    dict.Add("ambiente_id", dte.Rows(i).Item("ambiente_id"))
                    listEquipos.Add(dict)
                Next

                Dim Arreglo As New ArrayList
                Arreglo.Insert(0, list)
                Arreglo.Insert(1, listEquipos)
                JSONresult = serializer.Serialize(Arreglo)
            End If
            
        ElseIf Request("param0") = "addReserva" Then
            Dim list As New List(Of Dictionary(Of String, Object))()
            Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
            Dim dictr As New Dictionary(Of String, Object)()

            Dim dt As New DataTable
            dt = cDatos.ConsultarReservaByColaboradorFecha(Session("id_per"), Request("fecha_reserva"))
            If dt.Rows.Count >= 2 Then                
                dictr.Add("r", "2")
                dictr.Add("alert", "success")
                dictr.Add("msje", "Usted ya cuenta con el máximo de reservas por día (2).")
                list.Add(dictr)
            Else
                Dim respuesta As New Object
                respuesta = cDatos.RegistrarReserva(Request("idAlumno"), Session("id_per"), Request("fecha_reserva"), Request("hora_inicio"), Request("hora_fin"), Request("estado"), Request("equipo_id"))

                If respuesta(0) = "-1" Then
                    dictr.Add("r", "2")
                    dictr.Add("alert", "success")
                    dictr.Add("msje", "Registro fuera de fecha")
                ElseIf respuesta(0) = "-2" Then
                    dictr.Add("r", "2")
                    dictr.Add("alert", "success")
                    dictr.Add("msje", "El usuario se encuentra registrado en otro equipo")
                Else
                    dictr.Add("r", True)
                    dictr.Add("alert", "success")
                    dictr.Add("msje", "Se registro con exito")
                End If
                list.Add(dictr)
            End If
            JSONresult = serializer.Serialize(list)
        ElseIf Request("param0") = "conslColaborador" Then
                Dim dt As New DataTable
                dt = cDatos.ConsultaReservaByColaborador(Session("id_per"), Request.QueryString("fecha"))
                Dim list As New List(Of Dictionary(Of String, Object))()
                Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()

                For i As Integer = 0 To dt.Rows.Count - 1
                    Dim dictc As New Dictionary(Of String, Object)()
                    dictc.Add("reserva_id", dt.Rows(i).Item("reserva_id"))
                    dictc.Add("idAlumno", dt.Rows(i).Item("idAlumno"))
                    dictc.Add("fecha", dt.Rows(i).Item("fecha"))
                    dictc.Add("hora_inicio", dt.Rows(i).Item("hora_inicio"))
                    dictc.Add("hora_fin", dt.Rows(i).Item("hora_fin"))
                    dictc.Add("estado", dt.Rows(i).Item("estado"))
                    dictc.Add("nombre_equipo", dt.Rows(i).Item("nombre_equipo"))
                    dictc.Add("nombre_ambiente", dt.Rows(i).Item("nombre_ambiente"))
                    list.Add(dictc)
                Next
                JSONresult = serializer.Serialize(list)
        ElseIf Request("param0") = "anularReserva" Then
                Dim list As New List(Of Dictionary(Of String, Object))()
                Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
                Dim dictr As New Dictionary(Of String, Object)()
                cDatos.AnularReserva(Request("reserva_id"), Request("estado"))
                dictr.Add("r", True)
                dictr.Add("alert", "success")
                dictr.Add("msje", "Se anuló con exito")
                list.Add(dictr)
                JSONresult = serializer.Serialize(list)
        End If
        Response.Write(JSONresult)
    End Sub
End Class
