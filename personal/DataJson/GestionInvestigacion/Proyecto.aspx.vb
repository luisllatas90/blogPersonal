﻿Imports System.IO
Imports System.Web.HttpRequest
Imports System.Collections.Generic
Imports System.Data
Partial Class DataJson_GestionInvestigacion_Proyecto
    Inherits System.Web.UI.Page

    Dim ruta As String = ConfigurationManager.AppSettings("SharedFiles")

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim obj As New ClsGestionInvestigacion
        Dim Data As New Dictionary(Of String, Object)()
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()
        Try
            Dim k As String = "0" 'Request("k")
            Dim f As String = ""

            Select Case obj.DecrytedString64(Request("action"))
                Case "Listar"
                    Dim cod As Integer
                    If Request("cboPersonal") <> "0" Then
                        cod = obj.DecrytedString64(Request("cboPersonal"))
                    Else
                        cod = Request("cboPersonal")
                    End If
                    Dim ctf As Integer = Request("ctf")
                    Dim estado As Integer = obj.DecrytedString64(Request("cboEstado"))
                    ListarProyecto("L", 0, cod, 0, estado, Session("id_per"), ctf)
                    'Case "ListarTotales"
                    '    Dim cod As Integer
                    '    cod = objT.DecrytedString64(Request("codE"))
                    '    ListaDetalleEvaluacion("LT", cod)
                Case "Registrar"
                    If Request("hdcod") <> "0" Then
                        k = obj.DecrytedString64(Request("hdcod"))
                    Else
                        k = Request("hdcod")
                    End If
                    Dim codigo_tin As Integer = Request("hdcodTin")
                    Dim titulo As String = Request("txttitulo")
                    Dim codigo_lin As Integer = obj.DecrytedString64(Request("cboLinea"))
                    Dim codigo_dis As Integer = 0
                    If Request("cboDisciplina") Then
                        codigo_dis = Request("cboDisciplina")
                    End If

                    Dim fechaini As String = Request("txtfecini")
                    Dim fechafin As String = Request("txtfecfin")
                    'Dim presupuesto As String = Request("txtpresupuesto")
                    Dim archivopto As String = ""
                    If Request("filepto") <> "" Then
                        archivopto = Request("filepto")
                    End If
                    Dim presupuesto As String = 0
                    'Dim financiamiento As String = Request("cboFinanciamiento")
                    Dim financiamiento_multiple As String = ""
                    Dim financiamiento_externo As String = ""
                    If Request("chkPropio") = "on" Then
                        If financiamiento_multiple = "" Then
                            financiamiento_multiple = financiamiento_multiple + "P"
                        Else
                            financiamiento_multiple = financiamiento_multiple + ",P"
                        End If
                    End If
                    If Request("chkUsat") = "on" Then
                        If financiamiento_multiple = "" Then
                            financiamiento_multiple = financiamiento_multiple + "U"
                        Else
                            financiamiento_multiple = financiamiento_multiple + ",U"
                        End If
                    End If
                    If Request("chkExterno") = "on" Then
                        If financiamiento_multiple = "" Then
                            financiamiento_multiple = financiamiento_multiple + "E"
                            financiamiento_externo = Request("txtexterno")
                        Else
                            financiamiento_multiple = financiamiento_multiple + ",E"
                            financiamiento_externo = Request("txtexterno")
                        End If
                    End If
                    Dim avance As String = Request("txtavance")
                    Dim estadoavance As String = Request("cboAvance")
                    Dim informe As String = ""
                    If Request("fileinforme") <> "" Then
                        informe = Request("fileinforme")
                    End If
                    Dim ctf As String = Request("ctf")
                    presupuesto = Request("txtpresupuesto")
                    ActualizarProyecto(k, codigo_tin, titulo, codigo_lin, codigo_dis, fechaini, fechafin, presupuesto, archivopto, financiamiento_multiple, financiamiento_externo, avance, estadoavance, informe, Session("id_per"), ctf, "T")
                Case "RegistrarObjetivos"
                    k = obj.DecrytedString64(Request("hdcodP"))
                    Dim objetivos() As Object = serializer.DeserializeObject(Request("array"))
                    ActualizarObjetivos(k, objetivos, Session("id_per"))
                Case "RegistrarEquipo"
                    k = obj.DecrytedString64(Request("hdcodP"))
                    Dim equipo() As Object = serializer.DeserializeObject(Request("array"))
                    ActualizarEquipo(k, equipo, Session("id_per"), "T")
                Case "SurbirArchivo"
                    Dim ArchivoASubir As HttpPostedFile = HttpContext.Current.Request.Files("ArchivoASubir")
                    Dim codigo As String = obj.DecrytedString64(Request("codigo")).ToString
                    Dim tipo As String = Request("tipo")
                    SubirArchivoMH(codigo, ArchivoASubir)
                Case "Editar"
                    k = obj.DecrytedString64(Request("hdcod"))
                    Dim ctf As Integer = Request("ctf")
                    ListarProyecto("E", k, 0, 0, 0, Session("id_per"), ctf)
                Case "ListaObjetivos"
                    k = obj.DecrytedString64(Request("hdcod"))
                    ListarObjetivos(k)
                Case "ListarAutorProyecto"
                    k = obj.DecrytedString64(Request("hdcod"))
                    ListarEquipo(k)
                Case "ActualizarInstanciaEstado"
                    k = obj.DecrytedString64(Request("cod"))
                    Dim veredicto As Integer = Request("veredicto")
                    Dim ctf As Integer = Request("ctf")
                    Dim observacion As String = Request("txtobservacion")
                    ActualizarInstanciaEstadoProyecto(k, veredicto, observacion, Session("id_per"), ctf)
                Case "CargarFiltroEstado"
                    Dim ctf As Integer = Request("ctf")
                    CargarFiltroEstado(Session("id_per"), ctf)
                Case "ListarObservaciones"
                    k = obj.DecrytedString64(Request("cod"))
                    ListarObservaciones(k)
                Case "Eliminar"
                    k = obj.DecrytedString64(Request("cod"))
                    EliminarProyecto(k)
                Case "EnviarMail"
                    EnviarEmail()
            End Select
        Catch ex As Exception

            Data.Add("idper", Session("id_per"))
            Data.Add("rpta", ex.Message & "0 - LOAD")
            list.Add(Data)
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)
        End Try
    End Sub

    Private Sub ActualizarProyecto(ByVal codigo_pro As Integer, ByVal codigo_tin As Integer, ByVal titulo As String, ByVal codigo_lin As Integer, ByVal codigo_dis As Integer, ByVal fechaini As String, ByVal fechafin As String, ByVal presupuesto As Decimal, ByVal archivopto As String, ByVal financiamiento As String, ByVal financiamiento_externo As String, ByVal avance As Decimal, ByVal estado_avance As String, ByVal informe As String, ByVal id As Integer, ByVal ctf As Integer, ByVal tipo As String)
        Dim obj As New ClsGestionInvestigacion
        Dim Data As New Dictionary(Of String, Object)()
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()
        Try
            Dim dt As New Data.DataTable
            dt = obj.ActualizarProyecto(codigo_pro, codigo_tin, titulo, codigo_lin, codigo_dis, fechaini, fechafin, presupuesto, archivopto, financiamiento, financiamiento_externo, avance, estado_avance, informe, id, ctf, tipo)
            Data.Add("cod", obj.EncrytedString64(dt.Rows(0).Item("cod")))
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

    Private Sub ActualizarObjetivos(ByVal codigo_pro As Integer, ByVal objetivos() As Object, ByVal id As Integer)
        Dim obj As New ClsGestionInvestigacion
        Dim Data2 As New Dictionary(Of String, Object)()
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()
        Try
            'For i As Integer = 0 To arr.Count - 1
            '    Data.Add(i, arr(i))
            'Next
            'list.Add(Data)
            Dim dt As New Data.DataTable
            Dim codigo_obj As Integer
            For i As Integer = 0 To objetivos.Length - 1
                Dim Data As New Dictionary(Of String, Object)()
                Dim opcion As Integer = 0
                If objetivos(i).Item("cod") <> "0" Then
                    codigo_obj = obj.DecrytedString64(objetivos(i).Item("cod"))
                Else
                    codigo_obj = objetivos(i).Item("cod")
                End If
                dt = obj.ActualizarObjetivo(codigo_obj, codigo_pro, objetivos(i).Item("descripcion"), objetivos(i).Item("codtipo"), objetivos(i).Item("estado"), id)
                Data.Add("rpta", dt.Rows(0).Item("Respuesta"))
                Data.Add("msje", dt.Rows(0).Item("Mensaje").ToString)
                list.Add(Data)
            Next

            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)
        Catch ex As Exception
            Data2.Add("rpta", "0 - REG")
            Data2.Add("msje", ex.Message)
            list.Add(Data2)
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)
        End Try
    End Sub

    Private Sub ActualizarEquipo(ByVal codigo_pro As Integer, ByVal equipo() As Object, ByVal id As Integer, ByVal tipo As String)
        Dim obj As New ClsGestionInvestigacion
        Dim Data2 As New Dictionary(Of String, Object)()
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()
        Try
            'For i As Integer = 0 To arr.Count - 1
            '    Data.Add(i, arr(i))
            'Next
            'list.Add(Data)
            Dim dt As New Data.DataTable
            Dim codigo_aut As Integer
            For i As Integer = 0 To equipo.Length - 1
                Dim Data As New Dictionary(Of String, Object)()
                If equipo(i).Item("cod_aut") <> "0" Then
                    codigo_aut = obj.DecrytedString64(equipo(i).Item("cod_aut"))
                Else
                    codigo_aut = equipo(i).Item("cod_aut")
                End If
                dt = obj.ActualizarAutorProyecto(codigo_aut, codigo_pro, 0, obj.DecrytedString64(equipo(i).Item("cod")), obj.DecrytedString64(equipo(i).Item("codtipo")), equipo(i).Item("estado"), id, tipo)
                Data.Add("rpta", dt.Rows(0).Item("Respuesta"))
                Data.Add("msje", dt.Rows(0).Item("Mensaje").ToString)
                list.Add(Data)
            Next

            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)
        Catch ex As Exception
            Data2.Add("rpta", "0 - REG")
            Data2.Add("msje", ex.Message)
            list.Add(Data2)
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)
        End Try
    End Sub

    Private Sub SubirArchivo(ByVal codigo As String, ByVal ArchivoSubir As HttpPostedFile, ByVal tipo As String)
        Dim obj As New ClsGestionInvestigacion
        Dim Data As New Dictionary(Of String, Object)()
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()
        Dim linea_error As String = ""
        Try
            '1.nombre de archivo
            Dim nomArchivo As String = System.IO.Path.GetFileName(ArchivoSubir.FileName).Substring(0, System.IO.Path.GetFileName(ArchivoSubir.FileName).IndexOf(System.IO.Path.GetExtension(ArchivoSubir.FileName).ToString)) + Now.ToString("yyyyMMddHmmss")
            '2.ruta a guardar
            '2.1.verificamos si hay una carpeta para el codigo del proyecto,sino se crea
            Dim strRutaArchivo As String
            strRutaArchivo = Server.MapPath("../../GestionInvestigacion/Archivos/Proyectos/" + codigo)
            linea_error = "1 - " + strRutaArchivo
            If Directory.Exists(strRutaArchivo) Then
            Else
                Directory.CreateDirectory(strRutaArchivo)
            End If
            linea_error = "2 - " + strRutaArchivo
            '2.2.Verificamos si existe carpeta para el tipo de archivo a guardar,sino se crea
            strRutaArchivo = strRutaArchivo + "/" + tipo
            If Directory.Exists(strRutaArchivo) Then
            Else
                Directory.CreateDirectory(strRutaArchivo)
            End If
            linea_error = "3 - " + strRutaArchivo
            '3.Nombre Final
            nomArchivo = nomArchivo & System.IO.Path.GetExtension(ArchivoSubir.FileName)
            linea_error = "4 - " + strRutaArchivo
            '4.Guardamos Archivo
            linea_error = "5.1 - " + strRutaArchivo & "/" & nomArchivo
            ArchivoSubir.SaveAs(strRutaArchivo & "/" & nomArchivo)

            ActualizarArchivosProyecto(codigo, "/" & tipo & "/" & nomArchivo, tipo)

            linea_error = "5 - " + strRutaArchivo
            Data.Add("msje", "OK")
            Data.Add("alert", "success")
            list.Add(Data)
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)
        Catch ex As Exception
            Data.Add("msje", ex.Message)
            Data.Add("alert", "error" + linea_error)
            list.Add(Data)
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)
        End Try
    End Sub

    Private Sub ListarProyecto(ByVal tipo As String, ByVal codigo As Integer, ByVal codigo_per As Integer, ByVal codigo_alu As Integer, ByVal estado As Integer, ByVal id As Integer, ByVal ctf As Integer)
        Dim obj As New ClsGestionInvestigacion
        Dim Data1 As New Dictionary(Of String, Object)()
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()
        Try
            Dim dt As New Data.DataTable
            dt = obj.ListarProyecto(tipo, codigo, codigo_per, codigo_alu, estado, id, ctf)
            If dt.Rows.Count > 0 Then
                For i As Integer = 0 To dt.Rows.Count - 1
                    Dim data As New Dictionary(Of String, Object)()
                    data.Add("cod", obj.EncrytedString64(dt.Rows(i).Item("codigo_pro")))
                    data.Add("titulo", dt.Rows(i).Item("titulo_pro"))
                    data.Add("coordinador", dt.Rows(i).Item("coordinador"))
                    data.Add("fecini", dt.Rows(i).Item("fechaini_pro"))
                    data.Add("fecfin", dt.Rows(i).Item("fechafin_pro"))
                    data.Add("instancia", dt.Rows(i).Item("instancia"))
                    data.Add("estado", dt.Rows(i).Item("estado"))
                    data.Add("cod_iea", dt.Rows(i).Item("codetapa"))
                    If tipo = "E" Then
                        data.Add("linea", obj.EncrytedString64(dt.Rows(i).Item("codigo_lin")))
                        data.Add("nombrelinea", dt.Rows(i).Item("nombre"))
                        data.Add("financiamiento", dt.Rows(i).Item("financiamiento_pro"))
                        data.Add("presupuesto", dt.Rows(i).Item("presupuesto_pro"))
                        data.Add("avance", dt.Rows(i).Item("avance_pro"))
                        data.Add("cod_dis", dt.Rows(i).Item("codigo_dis_ocde"))
                        data.Add("cod_sub", dt.Rows(i).Item("codigo_sub_ocde"))
                        data.Add("cod_area", dt.Rows(i).Item("codigo_area_ocde"))
                        data.Add("financia_ext", dt.Rows(i).Item("financia_ext").ToString)
                        data.Add("estadoavance", dt.Rows(i).Item("estadoavance_pro"))
                        If dt.Rows(i).Item("file_pto") = "" Then
                            data.Add("rutapto", "")
                        Else
                            data.Add("rutapto", "../GestionInvestigacion/Archivos/Proyectos/" + codigo.ToString + dt.Rows(i).Item("file_pto"))
                        End If
                        If dt.Rows(i).Item("file_informe") = "" Then
                            data.Add("rutainforme", "")
                        Else
                            'data.Add("rutainforme", "../GestionInvestigacion/Archivos/Proyectos/" + codigo.ToString + dt.Rows(i).Item("file_informe"))
                            data.Add("rutainforme", dt.Rows(i).Item("file_informe"))
                        End If

                    End If
                    list.Add(data)
                Next
            End If
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)
        Catch ex As Exception
            Data1.Add("rpta", "1 - LIST")
            Data1.Add("msje", ex.Message)
            list.Add(Data1)
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)
        End Try
    End Sub

    Private Sub ListarObjetivos(ByVal codigo As Integer)
        Dim obj As New ClsGestionInvestigacion
        Dim Data1 As New Dictionary(Of String, Object)()
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()
        Try
            Dim dt As New Data.DataTable
            dt = obj.ListarObjetivos(codigo)
            If dt.Rows.Count > 0 Then
                For i As Integer = 0 To dt.Rows.Count - 1
                    Dim data As New Dictionary(Of String, Object)()
                    data.Add("cod", obj.EncrytedString64(dt.Rows(i).Item("codigo")))
                    data.Add("des", dt.Rows(i).Item("descripcion"))
                    data.Add("codtipo", dt.Rows(i).Item("tipo_obj"))
                    data.Add("tipo", dt.Rows(i).Item("tipo"))
                    data.Add("estado", dt.Rows(i).Item("estado_obj"))
                    list.Add(data)
                Next
            End If
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)
        Catch ex As Exception
            Data1.Add("rpta", "1 - LIST")
            Data1.Add("msje", ex.Message)
            list.Add(Data1)
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)
        End Try
    End Sub


    Private Sub ListarObservaciones(ByVal codigo As Integer)
        Dim obj As New ClsGestionInvestigacion
        Dim Data1 As New Dictionary(Of String, Object)()
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()
        Try
            Dim dt As New Data.DataTable
            dt = obj.ListarObservaciones(codigo)
            If dt.Rows.Count > 0 Then
                For i As Integer = 0 To dt.Rows.Count - 1
                    Dim data As New Dictionary(Of String, Object)()
                    data.Add("cod", obj.EncrytedString64(dt.Rows(i).Item("codigo_obs")))
                    data.Add("des", dt.Rows(i).Item("descripcion_obs"))
                    data.Add("sol", dt.Rows(i).Item("resuelto_obs"))
                    data.Add("fec", dt.Rows(i).Item("fecha_reg").ToString)
                    list.Add(data)
                Next
            End If
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)
        Catch ex As Exception
            Data1.Add("rpta", "1 - LIST")
            Data1.Add("msje", ex.Message)
            list.Add(Data1)
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)
        End Try
    End Sub
    Private Sub ListarEquipo(ByVal codigo As Integer)
        Dim obj As New ClsGestionInvestigacion
        Dim Data1 As New Dictionary(Of String, Object)()
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()
        Try
            Dim dt As New Data.DataTable
            dt = obj.ListarAutorProyecto(codigo)
            If dt.Rows.Count > 0 Then
                For i As Integer = 0 To dt.Rows.Count - 1
                    Dim data As New Dictionary(Of String, Object)()
                    data.Add("cod", obj.EncrytedString64(dt.Rows(i).Item("codigo")))
                    data.Add("codper", obj.EncrytedString64(dt.Rows(i).Item("codigo_per")))
                    data.Add("autor", dt.Rows(i).Item("autor"))
                    data.Add("codtipo", obj.EncrytedString64(dt.Rows(i).Item("codigo_tip")))
                    data.Add("tipo", dt.Rows(i).Item("descripcion"))
                    data.Add("estado", dt.Rows(i).Item("estado_aut"))
                    list.Add(data)
                Next
            End If
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)
        Catch ex As Exception
            Data1.Add("rpta", "1 - LIST")
            Data1.Add("msje", ex.Message)
            list.Add(Data1)
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)
        End Try
    End Sub

    Private Sub ActualizarArchivosProyecto(ByVal codigo_pro As Integer, ByVal ruta As String, ByVal tipo As String)
        Dim obj As New ClsGestionInvestigacion
        Dim Data1 As New Dictionary(Of String, Object)()
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()
        Try
            Dim dt As New Data.DataTable
            dt = obj.ActualizarArchivosdeProyecto(codigo_pro, ruta, tipo)
            Dim data As New Dictionary(Of String, Object)()
            data.Add("cod", obj.EncrytedString64(codigo_pro))
            data.Add("ruta", ruta)
            list.Add(data)
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)
        Catch ex As Exception
            Data1.Add("rpta", "1 - LIST")
            Data1.Add("msje", ex.Message)
            list.Add(Data1)
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)
        End Try
    End Sub

    Private Sub ActualizarInstanciaEstadoProyecto(ByVal codigo As Integer, ByVal veredicto As Integer, ByVal observacion As String, ByVal id As Integer, ByVal ctf As Integer)
        Dim obj As New ClsGestionInvestigacion
        Dim Data1 As New Dictionary(Of String, Object)()
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()
        Try
            Dim dt As New Data.DataTable
            Dim data As New Dictionary(Of String, Object)()
            dt = obj.ActualizarInstanciaEstado(codigo, veredicto, observacion, id, ctf)
            data.Add("rpta", dt.Rows(0).Item("Respuesta"))
            data.Add("msje", dt.Rows(0).Item("Mensaje").ToString)
            data.Add("EnviaCorreo", dt.Rows(0).Item("enviacorreo"))
            list.Add(data)
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)
        Catch ex As Exception
            Data1.Add("rpta", "1 - ACTUALIZAR INSTANCIA")
            Data1.Add("msje", ex.Message)
            list.Add(Data1)
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)
        End Try
    End Sub

    Private Sub CargarFiltroEstado(ByVal id As Integer, ByVal ctf As Integer)
        Dim obj As New ClsGestionInvestigacion
        Dim Data1 As New Dictionary(Of String, Object)()
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()
        Try
            Dim dt As New Data.DataTable
            dt = obj.CargarFiltroEstado(id, ctf)
            If dt.Rows.Count > 0 Then
                For i As Integer = 0 To dt.Rows.Count - 1
                    Dim data As New Dictionary(Of String, Object)()
                    data.Add("cod", obj.EncrytedString64(dt.Rows(i).Item("codigo")))
                    data.Add("nombre", dt.Rows(i).Item("nombre"))
                    list.Add(data)
                Next
            End If
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)
        Catch ex As Exception
            Data1.Add("rpta", "1 - LIST")
            Data1.Add("msje", ex.Message)
            list.Add(Data1)
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)
        End Try
    End Sub
    Private Sub EliminarProyecto(ByVal codigo As Integer)
        Dim obj As New ClsGestionInvestigacion
        Dim Data1 As New Dictionary(Of String, Object)()
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()
        Try
            Dim dt As New Data.DataTable
            Dim data As New Dictionary(Of String, Object)()
            dt = obj.EliminarProyecto(codigo)
            data.Add("rpta", dt.Rows(0).Item("Respuesta"))
            data.Add("msje", dt.Rows(0).Item("Mensaje").ToString)
            list.Add(data)
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)
        Catch ex As Exception
            Data1.Add("rpta", "1 - LIST")
            Data1.Add("msje", ex.Message)
            list.Add(Data1)
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)
        End Try
    End Sub

    Sub EnviarEmail()
        Dim JSONresult As String = ""
        Dim blnResultado As Boolean = False

        Dim obj1 As New ClsGestionInvestigacion
        Dim obj As New ClsConectarDatos
        Dim tb As New Data.DataTable

        Dim objemail As New ClsMail
        Try
            Dim receptor, AsuntoCorreo As String
            Dim mensaje As String = ""
            Dim descripcion As String = ""

            Dim Codigo As Integer = obj1.DecrytedString64(Request("param1"))
            Dim veredicto As Integer = Request("param2")

            tb = obj1.EnviarMailProyecto(Codigo)

            AsuntoCorreo = "Evaluación de Proyectos de Investigación - Sistema de Gestión de Investigación USAT"

            Dim list As New List(Of Dictionary(Of String, Object))()
            Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
            For i As Integer = 0 To tb.Rows.Count - 1
                receptor = tb.Rows(i).Item("receptor").ToString()
                descripcion = tb.Rows(i).Item("mensaje").ToString()
            Next

            'receptor = "freddy.seclen@usat.edu.pe"
            receptor = "hcano@usat.edu.pe"

            mensaje = mensaje + "<html><head><meta http-equiv='Content-Type' content='text/html; charset=iso-8859-1' />"
            mensaje = mensaje + "<title>Evaluación de Proyecto</title>"
            mensaje = mensaje + "<style type='text/css'>.usat { font-family:Calibri;color:#F1132A;font-size:25px;font-weight: bold;} "
            mensaje = mensaje + ".bolsa{color:#F1132A;font-family:Calibri;font-size: 13px;font-weight: 500;}</style></head>"
            mensaje = mensaje + "<body>"
            mensaje = mensaje + "<div style='text-align:center;width:100%'>"
            mensaje = mensaje + "<table border='0' width='70%' cellpadding='0' cellspacing='0'><tr><td>"
            mensaje = mensaje + "<div style='width:70%;margin:0 auto;text-align:center;'><img src='https://intranet.usat.edu.pe/campusestudiante/assets/images/logousat.png' width='100' height='100' ></div>"
            mensaje = mensaje + "<div style='width:70%;margin:0 auto;text-align:center;'><div class='usat'>SISTEMA DE GESTIÓN DE INVESTIGACIÓN</div></div></td></tr></table>"
            mensaje = mensaje + "<table border='0' width='70%' cellpadding='0' cellspacing='0'><tr>"
            mensaje = mensaje + "<td style = 'background:none;border-bottom:1px solid #F1132A;height:1px;width:50%;margin:0px 0px 0px 0px' > &nbsp;</td></tr></table><br />"
            mensaje = mensaje + "<table border='0' width='70%' cellpadding='0' cellspacing='0'><tr><td>"
            mensaje = mensaje + "<div style='width:70%;margin:0 auto;text-align:center;'><div class='usat'>" + descripcion + "</div></div>"
            If (veredicto = 0) Then
                mensaje = mensaje + "<div style='text-align:center;color:gray;font-family:Calibri'><b>Ingrese al módulo de Gestion de Investigación para visualizar.</b></div>"
            End If
            mensaje = mensaje + "</td></tr></tbody></table>"
            mensaje = mensaje + "<table border='0' width='70%' cellpadding='0' cellspacing='0'><tr><td>"
            mensaje = mensaje + "<div style='width:100%;margin-top:10px;text-align:center;color:white'>"
            mensaje = mensaje + "</div></td></tr></table>"
            mensaje = mensaje + "<table border='0' width='70%' cellpadding='0' cellspacing='0'>"
            mensaje = mensaje + "<tr><td style='background:none;border-bottom:1px solid #F1132A;height:1px;width:50%;margin:0px 0px 0px 0px' > &nbsp;</td></tr></table><br />"
            mensaje = mensaje + "<table border='0' width='70%' cellpadding='0' cellspacing='0'><tr><td>"
            mensaje = mensaje + "<div style='margin:0 auto;text-align:center;color:gray;font-family:Calibri '><b>VICERRECTORADO INVESTIGACIÓN USAT</b></div><br /></td></tr></table>"
            mensaje = mensaje + "<table border='0' width='70%' cellpadding='0' cellspacing='0'><tr><td>"
            mensaje = mensaje + "<div style='text-align:center;font-size:11px;color:gray;font-family:Calibri '><div>Av. San Josemaría Escrivá de Balaguer Nº 855 Chiclayo - Perú | Teléfono: 606200 - anexo: 1291"
            mensaje = mensaje + "<a href='mailto:vri@usat.edu.pe' style='color:gray;text-decoration:none;' target='_blank'><br/><b>vri@usat.edu.pe</b></a></div> "
            mensaje = mensaje + "<div style='font-family:Calibri'>© Copyright 2018: USAT - Todos los derechos reservados</div>"
            mensaje = mensaje + "</td></tr></table></div></body></html>"

            objemail.EnviarMail("campusvirtual@usat.edu.pe", "GESTIÓN DE INVESTIGACIÓN", receptor, AsuntoCorreo, mensaje, True, "", "pdiaz@usat.edu.pe")

            Dim dict As New Dictionary(Of String, Object)()
            dict.Add("rpta", 1)
            dict.Add("msje", "E-mail enviado a Coordinador General correctamente")
            list.Add(dict)
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)
        Catch ex As Exception
            Dim dict As New Dictionary(Of String, Object)()
            Dim list As New List(Of Dictionary(Of String, Object))()
            Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
            dict.Add("msje", ex.Message())
            dict.Add("rpta", 0)
            list.Add(dict)
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)
        End Try
    End Sub

    Sub SubirArchivoMH(ByVal codigo As String, ByVal ArchivoSubir As HttpPostedFile)
        Try
            Dim post As HttpPostedFile = ArchivoSubir
            Dim codigo_con As String = codigo
            Dim NroRend As String = 2
            Dim Fecha As String = Date.Now.ToString("dd/MM/yyyy")
            Dim Usuario As String = Session("perlogin").ToString
            Dim Input(post.ContentLength) As Byte
            ' Dim b As New BinaryReader(post.InputStream)
            '  Dim by() As Byte = b.ReadByte(post.ContentLength)


            Dim b As New BinaryReader(post.InputStream)
            Dim binData As Byte() = b.ReadBytes(post.InputStream.Length)
            Dim base64 = System.Convert.ToBase64String(binData)

            Dim wsCloud As New ClsArchivosCompartidos
            Dim list As New Dictionary(Of String, String)
            '  Dim list As New List(Of Dictionary(Of String, String))()
            list.Add("Fecha", Fecha)
            list.Add("Extencion", System.IO.Path.GetExtension(post.FileName))
            'list.Add("Nombre", System.IO.Path.GetFileName(post.FileName.Replace("&", "_").Replace("'", "_").Replace("*", "_").Replace("á", "_").Replace("é", "_").Replace("í", "_").Replace("ó", "_").Replace("ú", "_").Replace("Á", "_").Replace("É", "_").Replace("Í", "_").Replace("Ó", "_").Replace("Ú", "_").Replace("Ñ", "_").Replace("ñ", "_").Replace(",", "_")))
            'list.Add("Nombre", Regex.Replace(System.IO.Path.GetFileName(post.FileName), "[^0-9A-Za-z._ ]", "_"))
            list.Add("Nombre", "archivo.pdf")
            list.Add("TransaccionId", codigo_con)
            list.Add("TablaId", "9")
            list.Add("NroOperacion", NroRend)
            list.Add("Archivo", base64)
            list.Add("Usuario", Usuario)
            list.Add("Equipo", "")
            list.Add("Ip", "")
            list.Add("param8", Usuario)

            Dim envelope As String = wsCloud.SoapEnvelope(list)
            Dim result As String = wsCloud.PeticionRequestSoap(ruta, envelope, "http://usat.edu.pe/UploadFile", Usuario)
            ActualizarArchivoConcurso(9, codigo_con, NroRend)
            Response.Write(result)
        Catch ex As Exception
            Dim Data1 As New Dictionary(Of String, Object)()
            Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
            Dim JSONresult As String = ""
            Dim list As New List(Of Dictionary(Of String, Object))()
            Data1.Add("rpta", "1 - LIST")
            Data1.Add("msje", ex.Message)
            list.Add(Data1)
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)
        End Try

    End Sub

    Private Sub ActualizarArchivoConcurso(ByVal idtabla As Integer, ByVal idtransaccion As String, ByVal idoperacion As String)
        Dim obj As New ClsGestionInvestigacion
        Dim Data1 As New Dictionary(Of String, Object)()
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()
        Try
            Dim dt As New Data.DataTable
            dt = obj.ActualizarIDArchivoCompartido(idtabla, idtransaccion, idoperacion)
            Dim data As New Dictionary(Of String, Object)()
            data.Add("cod", obj.EncrytedString64(idtransaccion))
            'data.Add("ruta", ruta)
            list.Add(data)
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)
        Catch ex As Exception
            Data1.Add("rpta", "1 - LIST")
            Data1.Add("msje", ex.Message)
            list.Add(Data1)
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)
        End Try
    End Sub

End Class
