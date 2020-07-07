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
                    f = Request("cboTipo")
                    Dim codigo_per As Integer = 0   'Alumno
                    Dim tipo_per As Integer = 0     'Alumno

                    'Response.Write(Session("descripcion_apl"))

                    'Response.Write("Hola Ceci")
                    'Response.Write(Session("id_per"))

                    If Session("descripcion_apl") = "PERSONAL" Then
                        codigo_per = Session("id_per")
                        tipo_per = 1                'Personal
                    End If

                    Lista("L", k, f, codigo_per, tipo_per) 'L es para listar

                Case "Registrar"
                    Dim telefono_def As Integer = Request("txtTelefono")
                    Dim mail_def As String = Request("txtEmail")
                    Dim detalle_def As String = Request("txtDetalle")
                    Dim tipo_def As String = Request("cboTipoR")
                    Dim usuario_reg As Integer = Session("id_per")
                    Dim codigo_alu As Integer = 0
                    Dim codigo_per As Integer = Session("id_per")
                    Registrar(k, telefono_def, mail_def, detalle_def, tipo_def, usuario_reg, codigo_alu, codigo_per)

                Case "Editar"
                    k = objCRM.DecrytedString64(Request("hdcod"))
                    'Dim codigo_per As Integer = Session("id_per")

                    Dim codigo_per As Integer = 0   'Alumno
                    Dim tipo_per As Integer = 0     'Alumno
                    If Session("descripcion_apl") = "PERSONAL" Then
                        codigo_per = Session("id_per")
                        tipo_per = 1                'Personal
                    End If
                    Lista("E", k, f, codigo_per, tipo_per)

                Case "Modificar"
                    k = objCRM.DecrytedString64(Request("hdcod"))
                    Dim telefono_def As String = Request("txtTelefono")
                    Dim mail_def As String = Request("txtEmail")
                    Dim detalle_def As String = Request("txtDetalle")
                    Dim tipo_def As String = Request("cboTipoR")
                    Dim usuario_reg As Integer = Session("id_per")
                    Dim codigo_alu As Integer = 0
                    Dim codigo_per As Integer = Session("id_per")
                    Registrar(k, telefono_def, mail_def, detalle_def, tipo_def, usuario_reg, codigo_alu, codigo_per)

                Case "Eliminar"
                    k = objCRM.DecrytedString64(Request("hdcod"))
                    Eliminar(k)

                Case "Consultar"
                    f = Request("cboTipo")
                    Dim fecini As String = "01/06/2017"  '27-02  ¿Por qué ponen fechas definidas?
                    Dim fecfin As String = CDate(Today()) '"30/06/2019"  '27-02  ¿Por qué ponen fechas definidas?
                    Consular(fecini, fecfin, f)

                Case "RegistrarRespuesta"  '' 23/01/2020
                    Dim codigo_def As Integer = objCRM.DecrytedString64(Request("hdcod"))
                    Dim respuesta_rde As String = Request("txtRespuesta")
                    Dim usuario_reg As Integer = Session("id_per")
                    Dim codigo_per As Integer = Session("id_per")
                    RegistrarRespuesta(k, codigo_def, respuesta_rde, usuario_reg, codigo_per)

                    'Case "ModificarRespuesta"
                    '    k = objCRM.DecrytedString64(Request("hdcod"))
                    '    Dim codigo_def As Integer = 0
                    '    Dim respuesta_rde As String = 0
                    '    Dim usuario_reg As Integer = Session("id_per")
                    '    Dim codigo_per As Integer = Session("id_per")
                    '    RegistrarRespuesta(k, codigo_def, respuesta_rde, usuario_reg, codigo_per)

                Case "ConsultarRespuesta"
                    Dim codigo_def As Integer = objCRM.DecrytedString64(Request("hdcod"))
                    'Dim codigo_def As Integer = objCRM.DecrytedString64()
                    ConsultarRespuesta(codigo_def)

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

    Private Sub Lista(ByVal tipo As String, ByVal codigo As String, ByVal tipo_def As String, ByVal codigo_per As Integer, ByVal tipo_per As Integer)
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()
        Try
            Dim obj As New ClsDefensoria
            Dim tb As New Data.DataTable
            tb = obj.ListaDefensoria(tipo, codigo, tipo_def, codigo_per, tipo_per)

            If tb.Rows.Count > 0 Then
                For i As Integer = 0 To tb.Rows.Count - 1
                    Dim data As New Dictionary(Of String, Object)()
                    data.Add("cCod", obj.EncrytedString64(tb.Rows(i).Item("codigo_def")))
                    data.Add("cFecha", tb.Rows(i).Item("fecha_def"))
                    data.Add("cTelefono", tb.Rows(i).Item("telefono_def"))
                    data.Add("cMail", tb.Rows(i).Item("mail_def"))
                    data.Add("cDetalle", tb.Rows(i).Item("detalle_def"))
                    Dim TipoNombre As String = IIf(tb.Rows(i).Item("tipo_def") = "C", "CONSULTA", IIf(tb.Rows(i).Item("tipo_def") = "R", "RECLAMO", "DENUNCIA"))
                    data.Add("cTipoNombre", TipoNombre)

                    If tipo = "L" Then 'se refiere a Listar
                        data.Add("cRes", tb.Rows(i).Item("num_respuesta")) '19/03/2020
                    End If
                    If tipo = "E" Then 'se refiere a Editar
                        data.Add("cTipo", tb.Rows(i).Item("tipo_def"))
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

    Private Sub Registrar(ByVal codigo_def As Integer, ByVal telefono_def As String, ByVal mail_def As String, ByVal detalle_def As String, _
                                ByVal tipo_def As String, ByVal usuario_reg As Integer, ByVal codigo_alu As Integer, ByVal codigo_per As Integer)

        Dim obj As New ClsDefensoria
        Dim Data As New Dictionary(Of String, Object)()
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()
        Try
            Dim dt As New Data.DataTable
            dt = obj.InsertarDefensoria(codigo_def, telefono_def, mail_def, detalle_def, tipo_def, usuario_reg, codigo_alu, codigo_per)
            Data.Add("rpta", dt.Rows(0).Item("Respuesta"))
            Data.Add("msje", dt.Rows(0).Item("Mensaje").ToString)
            list.Add(Data)
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)
            Call Enviar(telefono_def, mail_def, detalle_def, codigo_alu, codigo_per)

        Catch ex As Exception
            Data.Add("rpta", "0 - REG")
            Data.Add("msje", ex.Message)
            list.Add(Data)
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)
        End Try
    End Sub

    Private Sub RegistrarRespuesta(ByVal codigo_rde As Integer, ByVal codigo_def As Integer, ByVal respuesta_rde As String, ByVal usuario_reg As Integer, ByVal codigo_per As Integer)

        Dim obj As New ClsDefensoria
        Dim Data As New Dictionary(Of String, Object)()
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()
        Try
            Dim dt As New Data.DataTable
            dt = obj.InsertarRespuestaDefensoria(codigo_rde, codigo_def, respuesta_rde, usuario_reg, codigo_per)
            Data.Add("rpta", dt.Rows(0).Item("Respuesta"))
            Data.Add("msje", dt.Rows(0).Item("Mensaje").ToString)
            list.Add(Data)
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)
            'Call Enviar(telefono_def, mail_def, detalle_def, codigo_alu, codigo_per)

        Catch ex As Exception
            Data.Add("rpta", "0 - REG")
            Data.Add("msje", ex.Message)
            list.Add(Data)
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)
        End Try
    End Sub

    Private Sub Eliminar(ByVal cod As Integer)
        Dim obj As New ClsDefensoria
        Dim Data As New Dictionary(Of String, Object)()
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()
        Try

            Dim dt As New Data.DataTable
            dt = obj.EliminarDefensoria(cod)
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

    Sub Enviar(ByVal telefono_def As String, ByVal mail_def As String, ByVal detalle_def As String, ByVal codigo_alu As Integer, ByVal codigo_per As Integer)
        Try
            Dim obj As New ClsDefensoria
            Dim dtt As New Data.DataTable

            'Enviar Mail
            Dim de As String = mail_def
            Dim nombreEnvia As String = ""

            'dtt = obj.ListaNombreUsuario(IIf(codigo_alu = 0, 1, 0), IIf(codigo_alu = 0, codigo_per, codigo_alu))
            dtt = obj.ListaNombreUsuario(1, codigo_per)
            If dtt.Rows.Count > 0 Then
                nombreEnvia = dtt.Rows(0).Item("nombre")
            End If

            Dim para As String = "defensoriauniversitaria@usat.edu.pe"
            Dim asunto As String = "Mensaje de Defensoría Universitaria"
            Dim copia As String = "cfarfan@usat.edu.pe"
            Dim strRuta As String = ""
            Dim replyto As String = ""
            Dim Mensaje As String = ""
            Mensaje = "<font face='Trebuchet MS'>"
            Mensaje &= "Teléfono: " & telefono_def
            Mensaje &= "E-Mail:   " & mail_def & ":<br /><br />"
            Mensaje &= detalle_def
            Mensaje &= "</font>"

            EnviarMensaje(de, nombreEnvia, para, asunto, Mensaje, copia, strRuta, "", replyto)
        Catch ex As Exception

        End Try

    End Sub

    Function EnviarMensaje(ByVal de As String, ByVal nombreEnvia As String, ByVal para As String, ByVal asunto As String, ByVal mensaje As String, _
                           ByVal copia As String, ByVal rutaarchivo As String, ByVal nombrearchivo As String, ByVal replyto As String) As Boolean
        Try
            Dim cls As New ClsEnvioMailAlumni
            'If cls.EnviarMailAd("alumni@usat.edu.pe", "alumniUSAT", para, asunto, mensaje, True, copia, replyto, rutaarchivo, nombrearchivo) Then
            If cls.EnviarMailAd(de, nombreEnvia, para, asunto, mensaje, True, copia, replyto, rutaarchivo, nombrearchivo) Then
                cls = Nothing
                Return True
            Else
                cls = Nothing
                Return False
            End If
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Sub Consular(ByVal tipo As String, ByVal fecini As String, ByVal fecfin As String)
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()
        Try

            Dim obj As New ClsDefensoria
            Dim tb As New Data.DataTable
            tb = obj.ConsultaRegistroDefensoria(tipo, fecini, fecfin)

            If tb.Rows.Count > 0 Then
                For i As Integer = 0 To tb.Rows.Count - 1
                    Dim data As New Dictionary(Of String, Object)()
                    data.Add("cCod", obj.EncrytedString64(tb.Rows(i).Item("codigo_def"))) '13-03
                    data.Add("cFecha", tb.Rows(i).Item("fecha_def"))
                    data.Add("cTelefono", tb.Rows(i).Item("telefono_def"))
                    data.Add("cMail", tb.Rows(i).Item("mail_def"))
                    data.Add("cDetalle", tb.Rows(i).Item("detalle_def"))
                    data.Add("cTipo", tb.Rows(i).Item("tipo_def"))
                    data.Add("cPersona", tb.Rows(i).Item("persona"))
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

    '28-02
    Private Sub ConsultarRespuesta(ByVal codigo_def As Integer)
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()
        Try

            Dim obj As New ClsDefensoria
            Dim tb As New Data.DataTable
            tb = obj.ConsultarRespuesta(codigo_def)

            If tb.Rows.Count > 0 Then
                For i As Integer = 0 To tb.Rows.Count - 1
                    Dim data As New Dictionary(Of String, Object)()
                    data.Add("cRespuesta", tb.Rows(i).Item("respuesta"))
                    data.Add("cFecha", tb.Rows(i).Item("fecha"))
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
