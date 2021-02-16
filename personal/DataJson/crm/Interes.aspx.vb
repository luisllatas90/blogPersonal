Imports System.IO
Imports System.Web.HttpRequest
Imports System.Collections.Generic
Imports System.Data

Partial Class DataJson_crm_Interes
    Inherits System.Web.UI.Page

    Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim obj As New ClsCRM
        Dim data As New Dictionary(Of String, Object)()
        Dim list As New List(Of Dictionary(Of String, Object))()
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""

        Try
            Dim cod_int, codigo_ori As Integer
            Dim eventos, checked, des_conv, tipo As String

            Select Case obj.DecrytedString64(Request("action"))
                Case "TipoEstudio"
                    Call TipoEstudio()
                Case "Convocatoria"
                    tipo = Request("tipo") 'obj.DecrytedString64(Request("tipo"))
                    Call Convocatoria(tipo)
                Case "Listar"
                    cod_int = CInt(obj.DecrytedString64(Request("codigo")))
                    des_conv = Request("convocatoria") 'obj.DecrytedString64(Request("convocatoria"))
                    Call ListaInteres(cod_int, des_conv)
                Case "Registrar"
                    cod_int = CInt(obj.DecrytedString64(Request("codigo")))
                    codigo_ori = CInt(obj.DecrytedString64(Request("codigo_ori")))
                    eventos = Request("eventos")
                    checked = Request("checked")
                    Call GuardarInteres(cod_int, codigo_ori, eventos, checked, Session("id_per"))
            End Select

        Catch ex As Exception
            data.Add("rpta", "0 - LOAD")
            data.Add("msje", ex.Message)
            list.Add(data)
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)
        End Try
    End Sub

    Private Sub TipoEstudio()
        Dim data As New Dictionary(Of String, Object)()
        Dim list As New List(Of Dictionary(Of String, Object))()
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""

        Try
            Dim obj As New ClsCRM
            Dim tb As New Data.DataTable("Data")
            tb = obj.ListaTipoEstudio("TO", 0)

            data = New Dictionary(Of String, Object)()
            data.Add("cCodigo", "")
            data.Add("cEvento", "--Seleccione--")
            list.Add(data)

            If tb.Rows.Count > 0 Then
                For i As Integer = 0 To tb.Rows.Count - 1
                    data = New Dictionary(Of String, Object)()
                    data.Add("cCodigo", tb.Rows(i).Item("codigo_test").ToString())
                    data.Add("cEvento", tb.Rows(i).Item("descripcion_test").ToString())
                    list.Add(data)
                Next

                data = New Dictionary(Of String, Object)()
                data.Add("cCodigo", "%")
                data.Add("cEvento", "TODOS")
                list.Add(data)
            End If

            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)
        Catch ex As Exception
            data = New Dictionary(Of String, Object)()
            data.Add("rpta", "0")
            data.Add("msje", ex.Message)
            list.Add(data)
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)
        End Try
    End Sub

    Private Sub Convocatoria(ByVal tipo As String)
        Dim data As New Dictionary(Of String, Object)()
        Dim list As New List(Of Dictionary(Of String, Object))()
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""

        Try
            Dim obj As New ClsCRM
            Dim tb As New Data.DataTable("Data")
            tb = obj.ListaConvocatorias("C", 0, tipo)

            data = New Dictionary(Of String, Object)()
            data.Add("cCodigo", "")
            data.Add("cConvocatoria", "--Seleccione--")
            list.Add(data)

            If tb.Rows.Count > 0 Then
                For i As Integer = 0 To tb.Rows.Count - 1
                    data = New Dictionary(Of String, Object)()
                    data.Add("cCodigo", tb.Rows(i).Item("codigo").ToString())
                    data.Add("cConvocatoria", tb.Rows(i).Item("descripcion").ToString())
                    list.Add(data)
                Next

                data = New Dictionary(Of String, Object)()
                data.Add("cCodigo", "%")
                data.Add("cConvocatoria", "TODOS")
                list.Add(data)
            End If

            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)
        Catch ex As Exception
            data = New Dictionary(Of String, Object)()
            data.Add("rpta", "0")
            data.Add("msje", ex.Message)
            list.Add(data)
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)
        End Try
    End Sub

    Private Sub ListaInteres(ByVal cod_interesado As Integer, ByVal convocatoria As String)
        Dim data As New Dictionary(Of String, Object)()
        Dim list As New List(Of Dictionary(Of String, Object))()
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""

        Try
            Dim obj As New ClsCRM
            Dim tb As New Data.DataTable("Data")

            If convocatoria.ToUpper.Equals("TODOS") Then
                convocatoria = "%"
            End If

            tb = obj.ListaInteres(cod_interesado, convocatoria)

            If tb.Rows.Count > 0 Then
                For i As Integer = 0 To tb.Rows.Count - 1
                    data = New Dictionary(Of String, Object)()
                    data.Add("cCod", tb.Rows(i).Item("codigo")) 'data.Add("cCod", obj.EncrytedString64(tb.Rows(i).Item("codigo")))
                    data.Add("cEvento", tb.Rows(i).Item("evento"))
                    data.Add("cCheck", tb.Rows(i).Item("chk"))
                    data.Add("cConvocatoria", tb.Rows(i).Item("convocatoria"))
                    list.Add(data)
                Next
            End If

            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)
        Catch ex As Exception
            data = New Dictionary(Of String, Object)()
            data.Add("rpta", "0")
            data.Add("msje", ex.Message)
            list.Add(data)
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)
        End Try
    End Sub

    Private Sub GuardarInteres(ByVal cod_interesado As Integer, ByVal codigo_ori As Integer, ByVal eventos As String, ByVal checked As String, ByVal user_reg As Integer)
        Dim obj As New ClsCRM
        Dim data As New Dictionary(Of String, Object)()
        Dim list As New List(Of Dictionary(Of String, Object))()
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""

        Try
            Dim loResultado As Object()
            loResultado = obj.ActualizarInteres(cod_interesado, codigo_ori, eventos, checked, user_reg)
            data.Add("rpta", loResultado(0).ToString())
            data.Add("msje", loResultado(1).ToString())
            data.Add("cod", loResultado(2).ToString())
            list.Add(data)
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)
        Catch ex As Exception
            data = New Dictionary(Of String, Object)()
            data.Add("rpta", "0 - REG")
            data.Add("msje", ex.Message)
            data.Add("cod", "0")
            list.Add(data)
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)
        End Try
    End Sub

End Class
