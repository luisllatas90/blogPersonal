Imports System.IO
Imports System.Web.HttpRequest
Imports System.Collections.Generic
Imports System.Data

Partial Class DataJson_crm_Comunicacion
    Inherits System.Web.UI.Page

    Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim objCRM As New ClsCRM
        Dim Data As New Dictionary(Of String, Object)()
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""

        Try
            Dim k As String = "0" 'Request("k")
            Dim f As String = ""

            Select Case objCRM.DecrytedString64(Request("action"))
                Case "Listar"
                    'f = objCRM.DecrytedString64(Session("crm_CodigoInteresado"))
                    ListaComunicacion("L", objCRM.DecrytedString64(Request("hdcodintC")))
                Case "Registrar"
                    If Session("id_per") = "" Then
                        Throw New Exception("Se ha perdido la sesión, por favor vuelva a ingresar")
                    End If

                    Dim codigo_mot As Integer = objCRM.DecrytedString64(Request("cboMotivoR"))
                    Dim codigo_tcom As Integer = objCRM.DecrytedString64(Request("cboTipoComunicacionR"))
                    Dim codigo_ecom As Integer
                    If Request("cboEstadoComunicacion") = "0" Then
                        codigo_ecom = Request("cboEstadoComunicacion")
                    Else
                        codigo_ecom = objCRM.DecrytedString64(Request("cboEstadoComunicacion"))
                    End If
                    Dim detalle As String = Request("txtdetalle")
                    Dim codigo_int As Integer = objCRM.DecrytedString64(Request("hdcodintC"))
                    Dim codigo_per As Integer = Session("id_per")
                    Dim estado_com As Integer = 1
                    Dim codigo_eve As Integer = 0
                    If Not String.IsNullOrEmpty(Request("cboEvento")) Then
                        codigo_eve = objCRM.DecrytedString64(Request("cboEvento"))
                    End If

                    Dim destinatario As String
                    Dim remitente As String
                    If codigo_tcom = 2 Then
                        remitente = Request("txtNroAnexo")
                        destinatario = Request("txtNroInteresado")
                    Else
                        remitente = Request("txtNroInteresado")
                        destinatario = Request("txtNroAnexo")
                    End If

                    RegistrarComunicacion(k, codigo_tcom, codigo_int, codigo_mot, detalle, codigo_per, estado_com, Session("id_per"), codigo_ecom, codigo_eve, destinatario, remitente)
                Case "Editar"
                    k = objCRM.DecrytedString64(Request("hdcod"))
                    ListaComunicacion("E", k)
                Case "Modificar"
                    If Session("id_per") = "" Then
                        Throw New Exception("Se ha perdido la sesión, por favor vuelva a ingresar")
                    End If

                    k = objCRM.DecrytedString64(Request("hdcod"))
                    Dim codigo_mot As Integer = objCRM.DecrytedString64(Request("cboMotivoR"))
                    Dim codigo_tcom As Integer = objCRM.DecrytedString64(Request("cboTipoComunicacionR"))
                    Dim codigo_ecom As Integer
                    If Request("cboEstadoComunicacion") = "0" Then
                        codigo_ecom = Request("cboEstadoComunicacion")
                    Else
                        codigo_ecom = objCRM.DecrytedString64(Request("cboEstadoComunicacion"))
                    End If
                    Dim detalle As String = Request("txtdetalle")
                    Dim codigo_int As Integer = objCRM.DecrytedString64(Request("hdcodintC"))
                    Dim codigo_per As Integer = Session("id_per")
                    Dim estado_com As Integer = 1
                    Dim codigo_eve As Integer = 0
                    If Not String.IsNullOrEmpty(Request("cboEvento")) Then
                        codigo_eve = objCRM.DecrytedString64(Request("cboEvento"))
                    End If

                    Dim destinatario As String
                    Dim remitente As String
                    If codigo_tcom = 2 Then
                        remitente = Request("txtNroAnexo")
                        destinatario = Request("txtNroInteresado")
                    Else
                        remitente = Request("txtNroInteresado")
                        destinatario = Request("txtNroAnexo")
                    End If

                    RegistrarComunicacion(k, codigo_tcom, codigo_int, codigo_mot, detalle, codigo_per, estado_com, Session("id_per"), codigo_ecom, codigo_eve, destinatario, remitente)
                Case "Eliminar"
                    k = objCRM.DecrytedString64(Request("hdID"))
                    EliminarComunicacion(k)
                Case "ReenviarComunicacion"
                    k = objCRM.DecrytedString64(Request("hdID"))
                    ReenviarComunicacion(k, Session("id_per"))
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

    Private Sub ListaComunicacion(ByVal tipo As String, ByVal codigo As Integer)
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()
        Try

            Dim obj As New ClsCRM
            Dim tb As New Data.DataTable
            tb = obj.ListaComunicacion(tipo, codigo)

            If tb.Rows.Count > 0 Then
                For i As Integer = 0 To tb.Rows.Count - 1
                    Dim data As New Dictionary(Of String, Object)()
                    data.Add("cCod", obj.EncrytedString64(tb.Rows(i).Item("codigo_com")))
                    data.Add("cFecha", tb.Rows(i).Item("fecha_com"))
                    data.Add("cDetalle", tb.Rows(i).Item("detalle_com").ToString)

                    Dim codigoTcom As String = tb.Rows(i).Item("codigo_tcom")

                    data.Add("cTip", obj.EncrytedString64(codigoTcom))
                    If codigoTcom = "6" Or codigoTcom = "7" Then
                        data.Add("mostrarEnvioManual", "1")
                    Else
                        data.Add("mostrarEnvioManual", "0")
                    End If

                    If tipo = "E" Then
                        data.Add("cMot", obj.EncrytedString64(tb.Rows(i).Item("codigo_mot")))
                        data.Add("cCodigoEve", obj.EncrytedString64(tb.Rows(i).Item("codigo_eve")))
                        If tb.Rows(i).Item("codigo_ecom") = "0" Then
                            data.Add("cEst", tb.Rows(i).Item("codigo_ecom"))
                        Else
                            data.Add("cEst", obj.EncrytedString64(tb.Rows(i).Item("codigo_ecom")))
                        End If
                        data.Add("cRemitente", tb.Rows(i).Item("remitente_com"))
                        data.Add("cDestinatario", tb.Rows(i).Item("destinatario_com"))
                        data.Add("cVerifCall", tb.Rows(i).Item("verificacionCallcenter_com"))
                    Else
                        data.Add("cMotivoCom", tb.Rows(i).Item("descripcion_mot"))
                        data.Add("cTipoCom", tb.Rows(i).Item("descripcion_tcom"))
                        data.Add("cUsuario", tb.Rows(i).Item("usuario_per"))
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

    Private Sub RegistrarComunicacion(ByVal cod As Integer, ByVal codigo_tcom As Integer, ByVal codigo_int As Integer, ByVal codigo_mot As Integer, _
                                      ByVal detalle As String, ByVal codigo_per As Integer, ByVal estado_com As Integer, _
                                      ByVal user_reg As Integer, ByVal codigo_ecom As Integer, ByVal codigo_eve As Integer, ByVal destinatario_com As String, ByVal remitente_com As String)

        Dim obj As New ClsCRM
        Dim Data As New Dictionary(Of String, Object)()
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()
        Try
            Dim dt As New Data.DataTable
            dt = obj.ActualizarComunicacion(cod, codigo_tcom, codigo_int, codigo_mot, detalle, codigo_per, estado_com, user_reg, codigo_ecom, codigo_eve, destinatario_com, remitente_com)
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

    'Private Sub RegistrarMotivo(ByVal cod As Integer, ByVal nombre As String, ByVal estado As Integer, ByVal user_reg As Integer)
    '    Dim obj As New ClsCRM
    '    Dim Data As New Dictionary(Of String, Object)()
    '    Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
    '    Dim JSONresult As String = ""
    '    Dim list As New List(Of Dictionary(Of String, Object))()
    '    Try
    '        Dim dt As New Data.DataTable
    '        dt = obj.ActualizarMotivo(cod, nombre, estado, user_reg)
    '        Data.Add("rpta", dt.Rows(0).Item("Respuesta"))
    '        Data.Add("msje", dt.Rows(0).Item("Mensaje").ToString)
    '        list.Add(Data)
    '        JSONresult = serializer.Serialize(list)
    '        Response.Write(JSONresult)
    '    Catch ex As Exception
    '        Data.Add("rpta", "0 - REG")
    '        Data.Add("msje", ex.Message)
    '        list.Add(Data)
    '        JSONresult = serializer.Serialize(list)
    '        Response.Write(JSONresult)
    '    End Try
    'End Sub

    Private Sub EliminarComunicacion(ByVal cod As Integer)
        Dim obj As New ClsCRM
        Dim Data As New Dictionary(Of String, Object)()
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()
        Try

            Dim dt As New Data.DataTable
            dt = obj.EliminarComunicacion(cod)
            Data.Add("rpta", dt.Rows(0).Item("Respuesta"))
            Data.Add("msje", dt.Rows(0).Item("Mensaje").ToString)
            list.Add(Data)
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)
        Catch ex As Exception
            Data.Add("rpta", "0 - DEL")
            Data.Add("msje", ex.Message)
            list.Add(Data)
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)
        End Try
    End Sub

    Private Sub ReenviarComunicacion(ByVal cod As Integer, ByVal codigoPer As Integer)
        Dim obj As New ClsCRM
        Dim Data As New Dictionary(Of String, Object)()
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()
        Try

            Dim dt As New Object()
            dt = obj.ReenviarComunicacion(cod, codigoPer)
            Data.Add("rpta", dt(0).ToString)
            Data.Add("msje", dt(1).ToString)
            list.Add(Data)
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)
        Catch ex As Exception
            Data.Add("rpta", "0 - DEL")
            Data.Add("msje", ex.Message)
            list.Add(Data)
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)
        End Try
    End Sub


End Class
