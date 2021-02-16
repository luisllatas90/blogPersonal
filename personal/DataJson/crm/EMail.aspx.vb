Imports System.IO
Imports System.Web.HttpRequest
Imports System.Collections.Generic
Imports System.Data
Imports System.Xml
Imports System.Web.Script.Serialization

Partial Class EMail
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
                    f = objCRM.DecrytedString64(Request("hdcodiEMail"))
                    ListaEMail("L", k, f)
                Case "Registrar"
                    Dim codigo_int As Integer = objCRM.DecrytedString64(Request("hdcodiEMail"))
                    Dim TipoEMail As String = Request("cboTipoEMail")
                    Dim Descripcion As String = Request("txtDescripcionEMail")
                    Dim Detalle As String = Request("txtDetalleEMail")
                    Dim vigencia As Integer
                    If Request("chkVigenciaEMail") = "" Then
                        vigencia = 0
                    Else
                        vigencia = 1
                    End If
                    Dim cod_per As Integer = Session("id_per")
                    Dim verificado As Boolean = (Request("chkVerificadoEMail") <> "")

                    RegistrarEMail(k, codigo_int, TipoEMail, Descripcion, Detalle, vigencia, cod_per, verificado)

                Case "Editar"
                    k = objCRM.DecrytedString64(Request("hdcod_EMail"))
                    ListaEMail("E", k, f)
                Case "Modificar"
                    k = objCRM.DecrytedString64(Request("hdcod_EMail"))
                    Dim codigo_int As Integer = objCRM.DecrytedString64(Request("hdcodiEMail"))
                    Dim TipoEMail As String = Request("cboTipoEMail")
                    Dim Descripcion As String = Request("txtDescripcionEMail")
                    Dim Detalle As String = Request("txtDetalleEMail")
                    Dim vigencia As Integer
                    If Request("chkVigenciaEMail") = "" Then
                        vigencia = 0
                    Else
                        vigencia = 1
                    End If
                    Dim cod_per As Integer = Session("id_per")
                    Dim verificado As Boolean = (Request("chkVerificadoEMail") <> "")

                    RegistrarEMail(k, codigo_int, TipoEMail, Descripcion, Detalle, vigencia, cod_per, verificado)
                    'Case "Eliminar"
                    '    k = objCRM.DecrytedString64(Request("hdcod"))
                    '    EliminarConvocatoria(k)
                Case "ValidarEmail"
                    Dim email As String = Request("email")
                    ValidarEmail(email)
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

    Private Sub ListaEMail(ByVal tipo As String, ByVal codigo_emi As Integer, ByVal codigo_interesado As String)
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim obj As New ClsCRM
        Dim tb As New Data.DataTable
        Dim list As New List(Of Dictionary(Of String, Object))()
        Try

            'Dim cn As New clsaccesodatos
            tb = obj.ListaEMail(tipo, codigo_emi, codigo_interesado)

            If tb.Rows.Count > 0 Then
                For i As Integer = 0 To tb.Rows.Count - 1
                    Dim data As New Dictionary(Of String, Object)()
                    'If i = 0 Then data.Add("sw", True)
                    data.Add("cod", obj.EncrytedString64(tb.Rows(i).Item("codigo_emi")))
                    data.Add("tip", tb.Rows(i).Item("tipo_emi"))
                    data.Add("des", tb.Rows(i).Item("descripcion_emi"))
                    data.Add("det", tb.Rows(i).Item("detalle_emi"))
                    data.Add("vig", tb.Rows(i).Item("vigencia_emi"))
                    data.Add("fec", tb.Rows(i).Item("fecha_reg"))
                    data.Add("vrf", tb.Rows(i).Item("verificado_emi"))

                    'If tipo = "E" Then
                    '    data.Add("cDetalle", tb.Rows(i).Item("descripcion_con"))
                    '    data.Add("cTes", obj.EncrytedString64(tb.Rows(i).Item("codigo_test")))
                    '    data.Add("cCac", obj.EncrytedString64(tb.Rows(i).Item("codigo_Cac")))
                    'End If

                    'If tb.Rows(i).Item("activo") = 1 Then
                    '    data.Add("est", True)
                    'Else
                    '    data.Add("est", False)
                    'End If
                    'data.Add("nFiles", tb.Rows(i).Item("canarchivos"))
                    list.Add(data)
                Next
            End If
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)
        Catch ex As Exception
            Dim data1 As New Dictionary(Of String, Object)()
            data1.Add("rpta", "0 - REG")
            data1.Add("msje", ex.Message)
            list.Add(data1)
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)
        End Try
    End Sub

    Private Sub RegistrarEMail(ByVal cod As Integer, ByVal codigo_int As Integer, ByVal TipoEMail As String, ByVal Descripcion As String, ByVal Detalle As String, _
                              ByVal vigencia As Integer, ByVal user_reg As Integer, ByVal verificado As Boolean)
        Dim obj As New ClsCRM
        Dim Data As New Dictionary(Of String, Object)()
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()
        Try

            Dim dt As New Data.DataTable
            dt = obj.ActualizarEMail(cod, codigo_int, TipoEMail, Descripcion, Detalle, vigencia, user_reg, verificado)
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

    'Private Sub ModificarConvocatoria(ByVal cod As Integer, ByVal codigo_test As Integer, ByVal codigo_cac As Integer, ByVal nombre As String, ByVal detalle As String, ByVal fecini As String, ByVal fecfin As String, ByVal estado As Integer, ByVal user_reg As Integer)
    '    Dim obj As New ClsCRM
    '    Dim Data As New Dictionary(Of String, Object)()
    '    Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
    '    Dim JSONresult As String = ""
    '    Dim list As New List(Of Dictionary(Of String, Object))()
    '    Try
    '        'For i As Integer = 0 To arr.Count - 1
    '        '    Data.Add(i, arr(i))
    '        'Next
    '        'list.Add(Data)
    '        Dim dt As New Data.DataTable
    '        dt = obj.ActualizarConvocatoria(cod, codigo_test, codigo_cac, nombre, detalle, fecini, fecfin, estado, user_reg)
    '        Data.Add("rpta", dt.Rows(0).Item("Respuesta"))
    '        Data.Add("msje", dt.Rows(0).Item("Mensaje").ToString)
    '        list.Add(Data)
    '        JSONresult = serializer.Serialize(list)
    '        Response.Write(JSONresult)
    '    Catch ex As Exception
    '        Data.Add("rpta", "0 - MOD")
    '        Data.Add("msje", ex.Message)
    '        list.Add(Data)
    '        JSONresult = serializer.Serialize(list)
    '        Response.Write(JSONresult)
    '    End Try
    'End Sub

    'Private Sub EliminarConvocatoria(ByVal cod As Integer)
    '    Dim obj As New ClsCRM
    '    Dim Data As New Dictionary(Of String, Object)()
    '    Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
    '    Dim JSONresult As String = ""
    '    Dim list As New List(Of Dictionary(Of String, Object))()
    '    Try

    '        Dim dt As New Data.DataTable
    '        dt = obj.EliminarConvocatoria(cod)
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

    'Private Sub ValidarEmail(ByVal email As String)
    '    Dim JSONresult As String = ""
    '    Dim lo_Serializer As New JavaScriptSerializer()
    '    Dim lo_Respuesta As New Dictionary(Of String, String)
    '    'Dim ls_RutaServicio As String = "https://localhost:50493/WSUSAT/WSUSAT.asmx"
    '    Dim ls_RutaServicio As String = ConfigurationManager.AppSettings("RutaCampus") & "WSUSAT/WSUSAT.asmx"



    '    Dim lo_SOAP As New ClsSOAP
    '    Dim lo_Datos As New Dictionary(Of String, String)
    '    lo_Datos.Item("email") = email


    '    Dim ls_RespuestaServicio As String

    '    ls_RespuestaServicio = lo_SOAP.lr_RealizarPeticionSOAP(ls_RutaServicio, "ValidarEmail", lo_Datos)
    '    Try
    '        Dim lo_RespuestaSOAP As New XmlDocument : lo_RespuestaSOAP.LoadXml(ls_RespuestaServicio)

    '        Dim lo_Namespace As XmlNamespaceManager = New XmlNamespaceManager(lo_RespuestaSOAP.NameTable) : lo_Namespace.AddNamespace("ns", "http://tempuri.org/")
    '        Dim ls_RutaNodos As String = "//ns:ValidarEmailResponse /ns:ValidarEmailResult"
    '        Dim ls_Respuesta As String = lo_RespuestaSOAP.DocumentElement.SelectSingleNode(ls_RutaNodos, lo_Namespace).InnerText
    '        lo_Respuesta = lo_Serializer.Deserialize(Of Dictionary(Of String, String))(ls_Respuesta)

    '    Catch ex As Exception
    '        lo_Respuesta.Item("rpta") = "0 - REG"
    '        lo_Respuesta.Item("msje") = ex.Message
    '    End Try

    '    JSONresult = lo_Serializer.Serialize(ls_RespuestaServicio)
    '    Response.Write(JSONresult)
    'End Sub

    Private Sub ValidarEmail(ByVal email As String)
        Dim lo_Adm As New ClsAdmision
        Dim lo_Serializer As New JavaScriptSerializer()
        Dim ls_RespuestaServicio As Dictionary(Of String, String) = lo_Adm.ValidarEmail(email)

        Dim JSONresult As String = lo_Serializer.Serialize(ls_RespuestaServicio)
        Response.Write(JSONresult)
    End Sub
End Class
