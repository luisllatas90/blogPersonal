Imports System.IO
Imports System.Web.HttpRequest
Imports System.Collections.Generic
Imports System.Data

Partial Class DataJson_crm_Evento
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim objCRM As New ClsCRM
        Dim Data As New Dictionary(Of String, Object)()
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""

        Try
            Dim k As String = "0" 'Request("k")
            Dim f As String = ""

            Select Case objCRM.DecrytedString64(Request("action"))
                Case "Listar"
                    f = objCRM.DecrytedString64(Request("cboConvocatoria"))
                    ListaEvento("L", k, "%", f)
                Case "Registrar"
                    Dim codigo_con As Integer = objCRM.DecrytedString64(Request("cboConvocatoriaR"))
                    Dim codigo_acp As Integer = objCRM.DecrytedString64(Request("cboActividadPOAR"))
                    Dim nombre As String = Request("txtnombre")
                    Dim detalle As String = Request("txtdetalle")
                    Dim fecini As String = Request("txtfecini")
                    Dim fecfin As String = Request("txtfecfin")
                    Dim estado As Integer
                    If Request("chkestado") = "" Then
                        estado = 0
                    Else
                        estado = 1
                    End If
                    RegistrarEvento(k, codigo_con, codigo_acp, nombre, detalle, fecini, fecfin, estado, Session("id_per"))
                Case "Editar"
                    k = objCRM.DecrytedString64(Request("hdcod"))
                    ListaEvento("E", k, "%", f)
                Case "Modificar"
                    k = objCRM.DecrytedString64(Request("hdcod"))
                    'ListaEvento("E", k, "%", f)

                    Dim codigo_con As Integer = objCRM.DecrytedString64(Request("cboConvocatoriaR"))
                    Dim codigo_acp As Integer = objCRM.DecrytedString64(Request("cboActividadPOAR"))
                    Dim nombre As String = Request("txtnombre")
                    Dim detalle As String = Request("txtdetalle")
                    Dim fecini As String = Request("txtfecini")
                    Dim fecfin As String = Request("txtfecfin")
                    Dim estado As Integer
                    If Request("chkestado") = "" Then
                        estado = 0
                    Else
                        estado = 1
                    End If
                    RegistrarEvento(k, codigo_con, codigo_acp, nombre, detalle, fecini, fecfin, estado, Session("id_per"))
                Case "Eliminar"
                    k = objCRM.DecrytedString64(Request("hdcod"))
                    EliminarConvocatoria(k)
                Case "ListarPorInteresado"
                    Dim codigo_int As Integer = objCRM.DecrytedString64(Request("codigoInt"))
                    ListaEventosPorInteresado(codigo_int)
            End Select

        Catch ex As Exception

            Data.Add("msje", ex.Message)
            Data.Add("rpta", "0 - LOAD")
            Dim list As New List(Of Dictionary(Of String, Object))()
            list.Add(Data)
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)
        End Try
    End Sub

    Private Sub ListaEvento(ByVal tipo As String, ByVal codigo As Integer, ByVal cod_test As String, ByVal cod_conv As String)
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()
        Try

            Dim obj As New ClsCRM
            Dim tb As New Data.DataTable
            tb = obj.ListaEventos(tipo, codigo, cod_test, cod_conv)

            If tb.Rows.Count > 0 Then
                For i As Integer = 0 To tb.Rows.Count - 1
                    Dim data As New Dictionary(Of String, Object)()
                    data.Add("cCod", obj.EncrytedString64(tb.Rows(i).Item("codigo_eve")))
                    data.Add("cEvento", tb.Rows(i).Item("evento"))
                    data.Add("cConvocatoria", tb.Rows(i).Item("convocatoria"))
                    data.Add("cActividad", tb.Rows(i).Item("actividad"))
                   
                    If tipo = "E" Then
                        data.Add("cDescripcion", tb.Rows(i).Item("descripcion_eve"))
                        data.Add("cFecini", tb.Rows(i).Item("fechaini_eve"))
                        data.Add("cFecfin", tb.Rows(i).Item("fechafin_eve"))
                        data.Add("cEstado", tb.Rows(i).Item("estado_eve"))

                        data.Add("cCon", obj.EncrytedString64(tb.Rows(i).Item("codigo_con")))
                        data.Add("cAcp", obj.EncrytedString64(tb.Rows(i).Item("codigo_acp")))
                    End If
                    list.Add(data)
                Next
            End If
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)
        Catch ex As Exception
            Dim data1 As New Dictionary(Of String, Object)()
            data1.Add("msje", ex.Message)
            data1.Add("rpta", "0")
            list.Add(data1)
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)
        End Try
    End Sub

    Private Sub RegistrarEvento(ByVal cod As Integer, ByVal codigo_con As Integer, ByVal codigo_acp As Integer, ByVal nombre As String, ByVal detalle As String, ByVal fecini As String, ByVal fecfin As String, ByVal estado As Integer, ByVal user_reg As Integer)
        Dim obj As New ClsCRM
        Dim Data As New Dictionary(Of String, Object)()
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()
        Try
            Dim dt As New Data.DataTable
            dt = obj.ActualizarEvento(cod, nombre, detalle, codigo_con, codigo_acp, fecini, fecfin, estado, user_reg, "")
            Data.Add("rpta", dt.Rows(0).Item("Respuesta"))
            Data.Add("msje", dt.Rows(0).Item("Mensaje").ToString)
            list.Add(Data)
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)
        Catch ex As Exception
            Data.Add("rpta", "0 - REG")
            Data.Add("msje", ex.Message)
            list.Add(Data)
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)
        End Try
    End Sub

    Private Sub EliminarConvocatoria(ByVal cod As Integer)
        Dim obj As New ClsCRM
        Dim Data As New Dictionary(Of String, Object)()
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()
        Try

            Dim dt As New Data.DataTable
            dt = obj.EliminarEvento(cod)
            Data.Add("rpta", dt.Rows(0).Item("Respuesta"))
            Data.Add("msje", dt.Rows(0).Item("Mensaje").ToString)
            list.Add(Data)
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)
        Catch ex As Exception
            Data.Add("rpta", "0 - REG")
            Data.Add("msje", ex.Message)
            list.Add(Data)
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)
        End Try
    End Sub

    Private Sub ListaEventosPorInteresado(ByVal codigoInt As Integer)
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()
        Try

            Dim obj As New ClsCRM
            Dim tb As New Data.DataTable
            tb = obj.ListaEventos("LXI", "0", codigoInt, "")

            If tb.Rows.Count > 0 Then
                For i As Integer = 0 To tb.Rows.Count - 1
                    Dim data As New Dictionary(Of String, Object)()
                    data.Add("cCod", obj.EncrytedString64(tb.Rows(i).Item("codigo_eve").ToString))
                    data.Add("cEvento", tb.Rows(i).Item("nombre_eve").ToString)
                    data.Add("cOrigen", tb.Rows(i).Item("descripcion_ori").ToString)
                    data.Add("cFecha", tb.Rows(i).Item("fecha_reg").ToString)
                    data.Add("cConsulta", tb.Rows(i).Item("consulta").ToString)
                    data.Add("cUsuario", tb.Rows(i).Item("usuario_per").ToString)
                    list.Add(data)
                Next
            End If
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)
        Catch ex As Exception
            Dim data1 As New Dictionary(Of String, Object)()
            data1.Add("msje", ex.Message)
            data1.Add("rpta", "0")
            list.Add(data1)
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)
        End Try
    End Sub

End Class
