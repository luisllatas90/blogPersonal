Imports System.Collections.Generic
Partial Class administrativo_pec_js_Interesados_ajax
    Inherits System.Web.UI.Page

    Public Function ToJSON(ByVal dato As String) As String
        Dim jsonSerializer As New System.Web.Script.Serialization.JavaScriptSerializer()
        Return jsonSerializer.Serialize(dato)
    End Function

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim usuario As Integer
        Dim accion As String
        Dim page As String
        accion = Request("action")
        page = Request("page")
        Dim obj As New ClsCRM
        Dim dt As New Data.DataTable
        Dim tabla As String = ""
        Dim JSONresult As String = ""
        Dim body As String = ""
        If accion = "load" Then
            dt = obj.ListaInteresados(0)
            Dim list As New List(Of Dictionary(Of String, Object))()
            Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
            For i As Integer = 0 To dt.Rows.Count - 1
                Dim dict As New Dictionary(Of String, Object)()
                dict.Add("numdoc", Trim(dt.Rows(i).Item("NumeroDocumento")))
                dict.Add("nombre", dt.Rows(i).Item("Interesado"))
                dict.Add("email", dt.Rows(i).Item("email"))
                dict.Add("cod", obj.EncrytedString64(dt.Rows(i).Item("Codigo")))
                list.Add(dict)
            Next
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)

            'If dt.Rows.Count > 0 Then
        ElseIf accion = "Com" Then
            dt = obj.ListaComunicacion(obj.DecrytedString64(page))
            Dim list As New List(Of Dictionary(Of String, Object))()
            Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
            For i As Integer = 0 To dt.Rows.Count - 1
                Dim dict As New Dictionary(Of String, Object)()
                dict.Add("nro", i + 1)
                dict.Add("carrera", Trim(dt.Rows(i).Item("carrera_com")))
                dict.Add("tipocom", dt.Rows(i).Item("descripcion_tcom"))
                dict.Add("detalle", dt.Rows(i).Item("detalle_com"))
                dict.Add("fecha", dt.Rows(i).Item("fechareg_com"))
                dict.Add("cod", obj.EncrytedString64(dt.Rows(i).Item("codigo_com")))
                list.Add(dict)
            Next
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)

        ElseIf accion = "dep" Then
            dt = obj.ListaDepartamentos()
            Dim list As New List(Of Dictionary(Of String, Object))()
            Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
            For i As Integer = 0 To dt.Rows.Count - 1
                Dim dict As New Dictionary(Of String, Object)()
                dict.Add("cod", Trim(dt.Rows(i).Item("codigo_Dep")))
                dict.Add("des", dt.Rows(i).Item("nombre_Dep"))
                list.Add(dict)
            Next
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)

        ElseIf accion = "prov" Then
            dt = obj.ListaProvincias(page)
            Dim list As New List(Of Dictionary(Of String, Object))()
            Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
            For i As Integer = 0 To dt.Rows.Count - 1
                Dim dict As New Dictionary(Of String, Object)()
                dict.Add("cod", Trim(dt.Rows(i).Item("codigo_Pro")))
                dict.Add("des", dt.Rows(i).Item("nombre_Pro"))
                list.Add(dict)
            Next
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)
        ElseIf accion = "dis" Then
            dt = obj.ListaDistritos(page)
            Dim list As New List(Of Dictionary(Of String, Object))()
            Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
            For i As Integer = 0 To dt.Rows.Count - 1
                Dim dict As New Dictionary(Of String, Object)()
                dict.Add("cod", Trim(dt.Rows(i).Item("codigo_dis")))
                dict.Add("des", dt.Rows(i).Item("nombre_dis"))
                list.Add(dict)
            Next
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)
        ElseIf accion = "TipoDoc" Then
            dt = obj.ListaTipoDocumento()
            Dim list As New List(Of Dictionary(Of String, Object))()
            Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
            For i As Integer = 0 To dt.Rows.Count - 1
                Dim dict As New Dictionary(Of String, Object)()
                dict.Add("cod", Trim(dt.Rows(i).Item("codigo_doci")))
                dict.Add("des", dt.Rows(i).Item("nombre_doci"))
                list.Add(dict)
            Next
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)
        ElseIf accion = "TipoCom" Then
            dt = obj.ListaTipoComunicacion("1", "")
            Dim list As New List(Of Dictionary(Of String, Object))()
            Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
            For i As Integer = 0 To dt.Rows.Count - 1
                Dim dict As New Dictionary(Of String, Object)()
                dict.Add("cod", Trim(dt.Rows(i).Item("codigo")))
                dict.Add("des", dt.Rows(i).Item("descripcion"))
                list.Add(dict)
            Next
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)

        ElseIf accion = "Eliminar" Then ' Eliminar Interesado
            Dim codigo As Integer
            codigo = obj.DecrytedString64(Request("id_int"))
            Response.Write(obj.EliminarInteresado(codigo))
        ElseIf accion = "Eliminar_Com" Then ' Eliminar Interesado
            Dim codigo As Integer
            codigo = obj.DecrytedString64(Request("id_com"))
            Response.Write(obj.EliminarComunicacion(codigo))
        ElseIf accion = "CargaDatos" Then ' Carga Datos de Interesado
            Dim codigo As Integer
            codigo = obj.DecrytedString64(Request("id"))
            dt = obj.ListaInteresados(codigo)
            '
            'Dim list As New List(Of Dictionary(Of String, Object))()
            Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
            'list = dtCursos

            Dim dict As New Dictionary(Of String, Object)()

            dict.Add("tdo", dt.Rows(0).Item("tipodoc_int"))
            dict.Add("ndo", dt.Rows(0).Item("numerodoc_int").ToString)
            dict.Add("ap", dt.Rows(0).Item("apepaterno_int").ToString)
            dict.Add("am", dt.Rows(0).Item("apematerno_int").ToString)
            dict.Add("nm", dt.Rows(0).Item("nombres_int").ToString)
            dict.Add("dp", dt.Rows(0).Item("departamento_int").ToString)
            dict.Add("prov", dt.Rows(0).Item("provincia_int").ToString)
            dict.Add("dis", dt.Rows(0).Item("distrito_int").ToString)
            dict.Add("dr", dt.Rows(0).Item("direccion_int").ToString)
            dict.Add("tl", dt.Rows(0).Item("telfijo_int").ToString)
            dict.Add("em", dt.Rows(0).Item("email_int").ToString)
            dict.Add("pro", dt.Rows(0).Item("procedencia_int").ToString)
            dict.Add("gra", dt.Rows(0).Item("Grado_int").ToString)
            dict.Add("car", dt.Rows(0).Item("carrera_int").ToString)
            dict.Add("ce", dt.Rows(0).Item("telcelular_int").ToString)
            'list.Add(dict)

            JSONresult = serializer.Serialize(dict)
            Response.Write(JSONresult)

        ElseIf accion = "Guardar" Then ' Guardar Interesado
            Dim tipo_doc, cbodepartamento, cboprovincia, cbodistrito, cboTipocomunicacion As Integer
            Dim codigo, numdoc, apepat, apemat, nombres, direccion, telefono, email, celular As String
            Dim txtcomunicacion, procedencia, grado, carrera As String

            codigo = Request("codigo_int")
            tipo_doc = Request("cboTipoDocumento")
            numdoc = Request("numdoc")
            apepat = Request("apepat")
            apemat = Request("apemat")
            nombres = Request("nombres")
            cbodepartamento = Request("cbodepartamento")
            cboprovincia = Request("cboprovincia")
            cbodistrito = Request("cbodistrito")
            direccion = Request("direccion")
            telefono = Request("telefono")
            email = Request("email")
            celular = Request("celular")
            procedencia = Request("procedencia")
            grado = Request("grado")
            carrera = Request("carrera")
            cboTipocomunicacion = Request("cboTipocomunicacion")
            txtcomunicacion = Request("txtcomunicacion")
            usuario = Session("id_per")



            If codigo = "0" Then ' Registrar Interesado
                Response.Write(obj.InsertarInteresado(tipo_doc, numdoc, apepat, apemat, nombres, cbodepartamento, cboprovincia, cbodistrito, direccion, telefono, email, celular, procedencia, grado, carrera, usuario))
            Else 'Editar Interesado
                codigo = obj.DecrytedString64(codigo)
                Response.Write(obj.ActualizarInteresado(codigo, tipo_doc, numdoc, apepat, apemat, nombres, cbodepartamento, cboprovincia, cbodistrito, direccion, telefono, email, celular, procedencia, grado, carrera, usuario))

            End If


        ElseIf accion = "Guardar_Com" Then ' Guardar Interesado
            Dim tipo_com As Integer
            Dim cod_int, detalle, carrera As String
            carrera = Request("carrera_2")
            tipo_com = Request("cboTipoComunicacion")
            detalle = Request("txtcomunicacion")
            cod_int = Request("codigo_int")
            cod_int = obj.DecrytedString64(cod_int)
            Response.Write(obj.InsertarComunicacion(tipo_com, detalle, carrera, cod_int, Session("id_per")))
        Else
            Response.Write("No se pudo Completar Operación")
        End If
    End Sub

    Private Function Guardar(ByVal codigo As Integer) As String
        Dim rpta As String
        If codigo = 0 Then
            rpta = "GUARDAR"
        Else
            rpta = "EDITAR"
        End If
        Return rpta
    End Function
End Class
