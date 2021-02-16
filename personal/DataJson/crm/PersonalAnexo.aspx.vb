Imports System.Collections.Generic

Partial Class DataJson_crm_PersonalAnexo
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim objCRM As New ClsCRM
        Dim Data As New Dictionary(Of String, Object)()
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""

        Try
            Select Case objCRM.DecrytedString64(Request("action"))
                Case "Listar"
                    Dim codigoPer As String = Session("id_per")
                    ListarAnexo("GEN", codigoPer)

                Case "Registrar"
                    Dim numeroPea As String = Request("txtNumeroAnexo")
                    Dim codUsuario As String = Session("id_per")
                    AsignarAnexo(codUsuario, numeroPea, codUsuario)
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

    Private Sub ListarAnexo(ByVal tipoConsulta As String, ByVal codigoPer As Integer)
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()
        Try

            Dim codigoPea As Integer = 0
            Dim numeroPea As String = ""
            Dim estadoPea As String = "A"
            Dim fechaRegPea As String = ""
            Dim usuarioRegPea As Integer = 0
            Dim fechaModPea As String = ""
            Dim usuarioModPea As Integer = 0


            Dim obj As New ClsCRM
            Dim tb As New Data.DataTable
            tb = obj.ListarAnexo(tipoConsulta, codigoPea, codigoPer, numeroPea, estadoPea, fechaRegPea, usuarioRegPea, fechaModPea, usuarioModPea)

            If tb.Rows.Count > 0 Then
                For i As Integer = 0 To tb.Rows.Count - 1
                    Dim data As New Dictionary(Of String, Object)()
                    If tipoConsulta = "GEN" Then
                        data.Add("cNumero", tb.Rows(i).Item("numero_pea"))
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

    Private Sub AsignarAnexo(ByVal codigoPer As Integer, ByVal numeroPea As String, ByVal codUsuario As Integer)
        Dim obj As New ClsCRM
        Dim Data As New Dictionary(Of String, Object)()
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()

        Try
            If codigoPer = 0 Then
                Throw New Exception("Sesión expirada, recargue la página")
            End If

            Dim operacion As String = "I"
            Dim codigoPea As Integer = 0
            Dim estadoPea As String = "A"

            Dim loResultado As Object()
            loResultado = obj.AsignarAnexo(operacion, codigoPea, codigoPer, numeroPea, estadoPea, codUsuario)
            Data.Add("rpta", loResultado(0).ToString())
            Data.Add("msje", loResultado(1).ToString())
            Data.Add("cod", loResultado(2).ToString())
            list.Add(Data)
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)

        Catch ex As Exception
            Data = New Dictionary(Of String, Object)()
            Data.Add("rpta", "0 - REG")
            Data.Add("msje", ex.Message)
            Data.Add("cod", "0")
            list.Add(Data)
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)
        End Try
    End Sub
End Class