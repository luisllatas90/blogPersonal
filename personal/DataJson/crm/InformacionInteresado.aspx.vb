Imports System.IO
Imports System.Web.HttpRequest
Imports System.Collections.Generic
Imports System.Data

Partial Class DataJson_crm_InformacionInteresado
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
                    f = Request("doc")
                    Historial(f)
                Case "Editar"
                    f = objCRM.DecrytedString64(Session("crm_CodigoInteresado"))
                    ListaInteresado("BXC", f, "", "", "", "", "")
                Case "PerfilInteresado"
                    Dim filtros As String = Request("hdFiltros")
                    Dim codigo As String = ""
                    PerfilInteresado(codigo, filtros)
                Case "IdSessionInteresado"
                    ObtenerIdSessionInteresado()
                Case "RequisitosIngresante"
                    Dim codigoInt As Integer = objCRM.DecrytedString64(Request("codigoInt"))
                    Dim codigoCon As Integer = objCRM.DecrytedString64(Request("codigoCon"))
                    ListarRequisitosEntregadosInteresado(codigoInt, codigoCon)
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

    Private Sub PerfilInteresado(ByVal codigo_interesado As String, ByVal filtros As String)
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()
        Dim data As New Dictionary(Of String, Object)()
        'Session("crm_CodigoInteresado") = codigo_interesado
        'If Session("crm_CodigoInteresado") <> "0" Then
        data.Add("msje", True)
        data.Add("link", "FrmListaInteresadosEvento.aspx" + filtros)
        'Else
        'data.Add("msje", False)
        'data.Add("link", "")
        'End If
        list.Add(data)
        JSONresult = serializer.Serialize(list)
        Response.Write(JSONresult)
    End Sub

    Private Sub ObtenerIdSessionInteresado()
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()
        Dim data As New Dictionary(Of String, Object)()
        'Session("crm_CodigoInteresado") = codigo_interesado
        'If Session("crm_CodigoInteresado") <> "0" Then
        If Session("crm_CodigoInteresado") = "" Then
            data.Add("cod", "0")
            data.Add("filtros", "")
        Else
            data.Add("cod", Session("crm_CodigoInteresado"))
            data.Add("filtros", Session("crm_FiltrosListaInteresado"))
        End If

        Dim obj As New ClsCRM
        data.Add("codigoTest", obj.EncrytedString64(Session("crm_FiltroCodigoTest")))
        data.Add("codigoCon", obj.EncrytedString64(Session("crm_FiltroCodigoCon")))
        data.Add("codigoEve", obj.EncrytedString64(Session("crm_FiltroCodigoEve")))

        list.Add(data)
        JSONresult = serializer.Serialize(list)
        Response.Write(JSONresult)
    End Sub
 
    'Private Sub ListaInteresado(ByVal tipo As String, ByVal codigo As String, ByVal cod_interesado As String)
    Private Sub ListaInteresado(ByVal tipo As String, ByVal cod_interesado As String, ByVal tipo_doc As String, ByVal num_doc As String, ByVal apepat As String, ByVal apemat As String, ByVal nombre As String)
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()

        Try
            Dim obj As New ClsCRM
            Dim tb As New Data.DataTable
            tb = obj.ListaInteresados(tipo, cod_interesado, tipo_doc, num_doc, apepat, apemat, nombre)

            If tb.Rows.Count > 0 Then
                For i As Integer = 0 To tb.Rows.Count - 1
                    Dim data As New Dictionary(Of String, Object)()

                    data.Add("cNumDoc", tb.Rows(i).Item("numerodoc_int"))
                    data.Add("cApePat", tb.Rows(i).Item("apepaterno_int"))
                    data.Add("cApeMat", tb.Rows(i).Item("apematerno_int"))
                    data.Add("cNombres", tb.Rows(i).Item("nombres_int"))
                    data.Add("cFecNac", tb.Rows(i).Item("fecha_nac")) 'data.Add("cFecNac", tb.Rows(i).Item("fechanacimiento_int"))
                    data.Add("cDireccion", tb.Rows(i).Item("direccion")) 'data.Add("cDireccion", tb.Rows(i).Item("direccion_din"))
                    data.Add("cTelefono", tb.Rows(i).Item("telefono")) 'data.Add("cTelefono", tb.Rows(i).Item("numero_tei"))
                    data.Add("cEmail", tb.Rows(i).Item("email"))
                    data.Add("cInsEdu", tb.Rows(i).Item("Nombre_ied")) 'data.Add("cInsEdu", tb.Rows(i).Item("institucionEducativa"))
                    data.Add("cCodCarPro", obj.EncrytedString64(tb.Rows(i).Item("codigo_cpf")))
                    data.Add("cCarPro", tb.Rows(i).Item("nombre_cpf")) 'data.Add("cCarPro", tb.Rows(i).Item("CarreraProfesional"))

                    Dim codigoCpf As Integer = tb.Rows(i).Item("codigo_Cpf")
                    Dim esMedicina As Boolean = (codigoCpf = 24)
                    data.Add("cEsMedicina", esMedicina)

                    'Adicionado por @jquepuy | 07ENE2019
                    data.Add("cUsuario", tb.Rows(i).Item("usuario_per").ToString().ToUpper())
                    data.Add("cFechaReg", tb.Rows(i).Item("fecha_reg"))

                    'Adicionado por @jquepuy | 22ENE2019
                    data.Add("cUbiInsEdu", tb.Rows(i).Item("ubigeo"))
                    data.Add("cDirInsEdu", tb.Rows(i).Item("Direccion_ied"))
                    data.Add("cGrado", tb.Rows(i).Item("grado"))
                    data.Add("cCodigoGrado", tb.Rows(i).Item("Grado_int"))
                    data.Add("cUbiInt", tb.Rows(i).Item("nombre_Dis").ToString().ToUpper() & " - " & tb.Rows(i).Item("nombre_Pro").ToString().ToUpper() & " - " & tb.Rows(i).Item("nombre_Dep").ToString().ToUpper())
                    data.Add("cCelular", tb.Rows(i).Item("celular"))
                    data.Add("cEdad", tb.Rows(i).Item("edad"))
                    data.Add("cSexo", tb.Rows(i).Item("sexo"))
                    data.Add("cAnioEgreso", tb.Rows(i).Item("anioegreso_int"))

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

    Private Sub Historial(ByVal doc As String)
        Dim data As New Dictionary(Of String, Object)()
        Dim list As New List(Of Dictionary(Of String, Object))()
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""

        Try
            Dim obj As New ClsCRM
            Dim tb As New Data.DataTable
            tb = obj.ListaInformacionGeneral(doc)

            If tb.Rows.Count > 0 Then
                For i As Integer = 0 To tb.Rows.Count - 1
                    data = New Dictionary(Of String, Object)()
                    data.Add("cIngreso", tb.Rows(i).Item("ingreso"))
                    data.Add("cEgreso", tb.Rows(i).Item("egreso"))
                    data.Add("cPrograma", tb.Rows(i).Item("programa"))
                    data.Add("cModalidad", tb.Rows(i).Item("modalidad"))
                    data.Add("cEstadoActual", tb.Rows(i).Item("estadoActual"))
                    data.Add("cEstadoDeuda", tb.Rows(i).Item("estadoDeuda"))
                    List.Add(data)
                Next
            End If

            JSONresult = serializer.Serialize(List)
            Response.Write(JSONresult)
        Catch ex As Exception
            data = New Dictionary(Of String, Object)()
            data.Add("msje", ex.Message)
            data.Add("rpta", "0")
            List.Add(data)
            JSONresult = serializer.Serialize(List)
            Response.Write(JSONresult)
        End Try
    End Sub

    Private Sub ListarRequisitosEntregadosInteresado(ByVal codigoInt As Integer, ByVal codigoCon As Integer)
        Dim data As New Dictionary(Of String, Object)()
        Dim list As New List(Of Dictionary(Of String, Object))()
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""

        Try
            Dim obj As New ClsCRM
            Dim tb As New Data.DataTable
            tb = obj.ListarRequisitosEntregadosInteresado(codigoInt, codigoCon)

            For i As Integer = 0 To tb.Rows.Count - 1
                data = New Dictionary(Of String, Object)()
                data.Item("codigo_Alu") = obj.EncrytedString64(tb.Rows(i).Item("codigo_Alu").ToString)
                data.Item("codigo_aluAsi") = obj.EncrytedString64(tb.Rows(i).Item("codigo_aluAsi").ToString)
                data.Item("descripcion") = tb.Rows(i).Item("descripcion").ToString
                data.Item("fecha_asi") = tb.Rows(i).Item("fecha_asi").ToString
                list.Add(data)
            Next

            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)
        Catch ex As Exception
            data = New Dictionary(Of String, Object)()
            data.Add("msje", ex.Message)
            data.Add("rpta", "0")
            list.Add(data)
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)
        End Try
    End Sub

    'RegistrarComunicacion(k, codigo_tcom, codigo_int, codigo_mot, detalle, codigo_per, Session("id_per"))

    'Private Sub RegistrarComunicacion(ByVal cod As Integer, ByVal codigo_tcom As Integer, ByVal codigo_int As Integer, ByVal codigo_mot As Integer, _
    '                                  ByVal detalle As String, ByVal codigo_per As Integer, ByVal estado_com As Integer, ByVal user_reg As Integer)


    '    Dim obj As New ClsCRM
    '    Dim Data As New Dictionary(Of String, Object)()
    '    Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
    '    Dim JSONresult As String = ""
    '    Dim list As New List(Of Dictionary(Of String, Object))()
    '    Try
    '        Dim dt As New Data.DataTable
    '        dt = obj.ActualizarComunicacion(cod, codigo_tcom, codigo_int, codigo_mot, detalle, codigo_per, estado_com, user_reg)
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

    'Private Sub EliminarConvocatoria(ByVal cod As Integer)
    '    Dim obj As New ClsCRM
    '    Dim Data As New Dictionary(Of String, Object)()
    '    Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
    '    Dim JSONresult As String = ""
    '    Dim list As New List(Of Dictionary(Of String, Object))()
    '    Try

    '        Dim dt As New Data.DataTable
    '        dt = obj.EliminarEvento(cod)
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

End Class
