Imports System.IO
Imports System.Web.HttpRequest
Imports System.Collections.Generic
Imports System.Data
Partial Class DataJson_GradosYTitulos_SesionConsejoUniversitario
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim objGyt As New ClsGradosyTitulos
        Dim Data As New Dictionary(Of String, Object)()
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Try
            Dim k As String = "0" 'Request("k")
            Dim f As String = ""

            Select Case objGyt.DecrytedString64(Request("action"))
                Case "Listar"
                    If Request("hdcod") <> "%" Then
                        k = objGyt.DecrytedString64(Request("hdcod"))
                    Else
                        k = Request("hdcod")
                    End If
                    Listar("L", k)
                Case "ListaPreEgresadosConConsejo"
                    Dim var As String()
                    Dim codigos As String = ""
                    Dim i As Integer
                    var = Request("hdcod").Split(",")
                    For i = 0 To var.Length - 1
                        If i = (var.Length - 1) Then
                            codigos += objGyt.DecrytedString64(var(i)).ToString
                        Else
                            codigos += objGyt.DecrytedString64(var(i)).ToString + ","
                        End If
                    Next
                    k = codigos.ToString
                    ListarPreEgresadosConConsejo("PCC", k)
                Case "ListaPreEgresadosSinConsejo"
                    ListarPreEgresadosConConsejo("PSC", 0)
                Case "Registrar"
                    Dim codigo As Integer = 0
                    Dim fecha As String = Request("txtfecha")
                    Dim cod_per As Integer = Session("id_per")

                    Registrar(codigo, fecha, cod_per)
                Case "Mover"
                    Dim var As String()
                    Dim i As Integer
                    Dim codigos As String = ""
                    var = Request("hdcod").Split(",")
                    For i = 0 To var.Length - 1
                        If i = (var.Length - 1) Then
                            codigos += objGyt.DecrytedString64(var(i)).ToString
                        Else
                            codigos += objGyt.DecrytedString64(var(i)).ToString + ","
                        End If
                    Next
                    k = codigos.ToString
                    Dim cod_session As Integer = objGyt.DecrytedString64(Request("hdcods"))
                    Dim tipo As String = Request("tipo")
                    Mover(codigos, cod_session, tipo)
                Case "ListaSesionCorrelativos"
                    If Request("hdcod") <> "%" Then
                        k = objGyt.DecrytedString64(Request("hdcod"))
                    Else
                        k = Request("hdcod")
                    End If
                    Listar("G", k)

                Case "Modificar"
                    Dim codigo As Integer = objGyt.DecrytedString64(Request("hdcod"))
                    Dim fecha As String = Request("txtfecha")
                    Dim cod_per As Integer = Session("id_per")
                    Registrar(codigo, fecha, cod_per)
                Case "SolicitarResolucionConsejoUniv"
                    Dim codigo As Integer = objGyt.DecrytedString64(Request("hdcod"))
                    Dim cod_per As Integer = Session("id_per")
                    SolicitarResolucionConsejoUniversitario(codigo, cod_per)

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

    Private Sub Listar(ByVal opcion As String, ByVal codigo As String)
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()

        Dim obj As New ClsGradosyTitulos
        Dim dt As New Data.DataTable
        dt = obj.ListaSesionConsejoU(opcion, codigo)
        If dt.Rows.Count > 0 Then
            'If codigo = "%" Then
            '    Dim data1 As New Dictionary(Of String, Object)()
            '    data1.Add("cod", obj.EncrytedString64("T"))
            '    data1.Add("nom", "TODOS")
            '    data1.Add("fec", "%")
            '    list.Add(data1)
            'End If
            For i As Integer = 0 To dt.Rows.Count - 1
                Dim data As New Dictionary(Of String, Object)()
                data.Add("cod", obj.EncrytedString64(dt.Rows(i).Item("codigo_scu")))
                data.Add("nom", dt.Rows(i).Item("descripcion_scu"))
                data.Add("fec", dt.Rows(i).Item("fecha_scu"))
                list.Add(data)
            Next
        End If
        JSONresult = serializer.Serialize(list)
        Response.Write(JSONresult)
    End Sub
    Private Sub ListarPreEgresadosConConsejo(ByVal opcion As String, ByVal codigo As String)
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()

        Dim obj As New ClsGradosyTitulos
        Dim dt As New Data.DataTable
        dt = obj.ListaSesionConsejoU(opcion, codigo)
        If dt.Rows.Count > 0 Then
            'If codigo = "%" Then
            '    Dim data1 As New Dictionary(Of String, Object)()
            '    data1.Add("cod", obj.EncrytedString64("T"))
            '    data1.Add("nom", "TODOS")
            '    data1.Add("fec", "%")
            '    list.Add(data1)
            'End If
            For i As Integer = 0 To dt.Rows.Count - 1
                Dim data As New Dictionary(Of String, Object)()
                data.Add("cod", obj.EncrytedString64(dt.Rows(i).Item("codigo_egr")))
                data.Add("Alumno", dt.Rows(i).Item("alumno"))
                data.Add("NroExp", dt.Rows(i).Item("NroExpediente_egr"))
                data.Add("nom_dgt", dt.Rows(i).Item("descripcion_dgt"))
                data.Add("enviado", dt.Rows(i).Item("enviado"))
                'data.Add("nom", dt.Rows(i).Item("descripcion_scu"))
                'data.Add("fec", dt.Rows(i).Item("fecha_scu"))
                list.Add(data)
            Next
        End If
        JSONresult = serializer.Serialize(list)
        Response.Write(JSONresult)
    End Sub
    Private Sub ListarPreEgresadosSinConsejo(ByVal opcion As String, ByVal codigo As String)
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()

        Dim obj As New ClsGradosyTitulos
        Dim dt As New Data.DataTable
        dt = obj.ListaSesionConsejoU(opcion, codigo)
        If dt.Rows.Count > 0 Then
            'If codigo = "%" Then
            '    Dim data1 As New Dictionary(Of String, Object)()
            '    data1.Add("cod", obj.EncrytedString64("T"))
            '    data1.Add("nom", "TODOS")
            '    data1.Add("fec", "%")
            '    list.Add(data1)
            'End If
            For i As Integer = 0 To dt.Rows.Count - 1
                Dim data As New Dictionary(Of String, Object)()
                data.Add("cod", obj.EncrytedString64(dt.Rows(i).Item("codigo_egr")))
                data.Add("Alumno", dt.Rows(i).Item("alumno"))
                data.Add("NroExp", dt.Rows(i).Item("NroExpediente_egr"))

                'data.Add("nom", dt.Rows(i).Item("descripcion_scu"))
                'data.Add("fec", dt.Rows(i).Item("fecha_scu"))
                list.Add(data)
            Next
        End If
        JSONresult = serializer.Serialize(list)
        Response.Write(JSONresult)
    End Sub

    Private Sub Registrar(ByVal codigo As Integer, ByVal fecha As String, ByVal user_reg As Integer)
        Dim obj As New ClsGradosyTitulos
        Dim Data As New Dictionary(Of String, Object)()
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()
        Try

            Dim dt As New Data.DataTable
            dt = obj.Actualizar_sesion(codigo, fecha, user_reg)
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


    Private Sub Mover(ByVal codigos As String, ByVal cod_Sesion As Integer, ByVal tipo As String)
        Dim obj As New ClsGradosyTitulos
        Dim Data As New Dictionary(Of String, Object)()
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()
        Try

            Dim dt As New Data.DataTable
            dt = obj.MoverAlumno(codigos, cod_Sesion, tipo)
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

    Private Sub SolicitarResolucionConsejoUniversitario(ByVal codigo As Integer, ByVal usuario As Integer)
        Dim obj As New ClsGradosyTitulos
        Dim Data As New Dictionary(Of String, Object)()
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()
        Try

            Dim dt As New Data.DataTable
            dt = obj.SolicitarResolucionConsejoUniv(codigo, usuario)
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
End Class
