Imports System.IO
Imports System.Web.HttpRequest
Imports System.Collections.Generic
Imports System.Data
Imports System.Xml

Partial Class DataJson_GestionInvestigacion_Concurso
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
                    Dim ctf As Integer = Request("ctf")
                    Dim dirigidoa As String = Request("dir")
                    Dim texto As String = Request("txtBusqueda")
                    Dim estado As String = Request("cboEstado")
                    Dim ambito As String = "%"
                    If Request("ambito") <> "" Then
                        ambito = Request("ambito")
                    End If

                    Listar("L", ambito, texto, estado, dirigidoa, Session("id_per"), ctf)
                Case "Registrar"
                    If Request("hdcod") <> "0" Then
                        k = obj.DecrytedString64(Request("hdcod"))
                    Else
                        k = Request("hdcod")
                    End If
                    Dim titulo As String = Request("txttitulo")
                    Dim descripcion As String = Request("txtdescripcion")
                    Dim ambito As String = Request("cboAmbito")
                    Dim fechaini As String = Request("txtfecini")
                    Dim fechafin As String = Request("txtfecfin")
                    Dim fechaeva As String = Request("txtfecfineva")
                    Dim fechares As String = Request("txtfecres")
                    Dim tipo As String = Request("cbotipo")
                    Dim ctf As String = Request("ctf")
                    Dim dirigidoa As Integer = 2
                    Dim innovacion As Integer = 0
                    Actualizar(k, titulo, descripcion, ambito, fechaini, fechafin, fechaeva, fechares, tipo, dirigidoa, innovacion, Session("id_per"), ctf)
                Case "SurbirArchivo"
                    Dim ArchivoASubir As HttpPostedFile = HttpContext.Current.Request.Files("ArchivoASubir")
                    Dim codigo As String = obj.DecrytedString64(Request("codigo")).ToString
                    SubirArchivoMH(codigo, ArchivoASubir)
                Case "Editar"
                    k = obj.DecrytedString64(Request("hdcod"))
                    Dim ctf As Integer = Request("ctf")
                    Listar("E", k, "", 0, 0, Session("id_per"), ctf)
                Case "Eliminar"
                    k = obj.DecrytedString64(Request("cod"))
                    Eliminar(k, Session("id_per"))
                Case "ListarConcursosPostulacion"
                    Dim ctf As Integer = Request("ctf")
                    Dim texto As String = Request("txtBusqueda")
                    Dim estado As String = Request("cboEstado")
                    Dim dirigidoa As String = Request("dir")
                    Listar("LCP", 0, texto, estado, dirigidoa, Session("id_per"), ctf)
            End Select
        Catch ex As Exception

            Data.Add("idper", Session("id_per"))
            Data.Add("rpta", ex.Message & "0 - LOAD")
            list.Add(Data)
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)
        End Try
    End Sub

    Private Sub Listar(ByVal tipo As String, ByVal codigo As String, ByVal texto As String, ByVal estado As String, ByVal dirigidoa As String, ByVal id As Integer, ByVal ctf As Integer)
        Dim obj As New ClsGestionInvestigacion
        Dim Data1 As New Dictionary(Of String, Object)()
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()
        Try
            Dim dt As New Data.DataTable
            dt = obj.ListarConcurso(tipo, codigo, texto, estado, dirigidoa, id, ctf)
            If dt.Rows.Count > 0 Then
                For i As Integer = 0 To dt.Rows.Count - 1
                    Dim data As New Dictionary(Of String, Object)()
                    data.Add("cod", obj.EncrytedString64(dt.Rows(i).Item("codigo_con")))
                    data.Add("titulo", dt.Rows(i).Item("titulo_con"))
                    data.Add("fecini", dt.Rows(i).Item("fechaini_con"))
                    data.Add("fecfin", dt.Rows(i).Item("fechafin_con"))
                    If tipo <> "LCP" Then
                        data.Add("ambito", dt.Rows(i).Item("ambito_con"))

                    End If
                    data.Add("tipo", dt.Rows(i).Item("tipo_con"))
                    If tipo = "L" Then
                        data.Add("nro_pos", dt.Rows(i).Item("nro_postulacion"))
                        data.Add("estado", dt.Rows(i).Item("estado_con"))
                    End If
                    If tipo = "E" Then
                        data.Add("des", dt.Rows(i).Item("descripcion_con"))

                        data.Add("cerrado", dt.Rows(i).Item("cerrado"))
                        data.Add("iniciado", dt.Rows(i).Item("iniciado"))
                        data.Add("fecfineva", dt.Rows(i).Item("fechafinevaluacion_con"))
                        data.Add("fecres", dt.Rows(i).Item("fecharesultados_con"))
                        If dt.Rows(i).Item("bases_con") = "0" Then
                            data.Add("rutabases", "")
                        Else
                            'data.Add("rutabases", "../GestionInvestigacion/Archivos/Concursos/Bases/" + codigo.ToString + dt.Rows(i).Item("bases_con"))
                            data.Add("rutabases", dt.Rows(i).Item("bases_con"))
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

    Private Sub Actualizar(ByVal codigo_con As Integer, ByVal titulo As String, ByVal descripcion As String, ByVal ambito As String, ByVal fechaini As String, ByVal fechafin As String, ByVal fechaeva As String, ByVal fecharesultados As String, ByVal tipo As String, ByVal dirigidoa As Integer, ByVal innovacion As Integer, ByVal id As Integer, ByVal ctf As Integer)
        Dim obj As New ClsGestionInvestigacion
        Dim Data As New Dictionary(Of String, Object)()
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()
        Try
            Dim dt As New Data.DataTable
            dt = obj.ActualizarConcurso(codigo_con, titulo, descripcion, ambito, fechaini, fechafin, fechaeva, fecharesultados, tipo, dirigidoa, innovacion, id, ctf)
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

    'Private Sub SubirArchivo(ByVal codigo As String, ByVal ArchivoSubir As HttpPostedFile)
    '    Dim obj As New ClsGestionInvestigacion
    '    Dim Data As New Dictionary(Of String, Object)()
    '    Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
    '    Dim JSONresult As String = ""
    '    Dim list As New List(Of Dictionary(Of String, Object))()
    '    Dim linea_error As String = ""
    '    Try
    '        '1.nombre de archivo
    '        Dim nomArchivo As String = System.IO.Path.GetFileName(ArchivoSubir.FileName).Substring(0, System.IO.Path.GetFileName(ArchivoSubir.FileName).IndexOf(System.IO.Path.GetExtension(ArchivoSubir.FileName).ToString)) + Now.ToString("yyyyMMddHmmss")
    '        '2.ruta a guardar
    '        '2.1.verificamos si hay una carpeta para el codigo del concurso,sino se crea
    '        Dim strRutaArchivo As String
    '        strRutaArchivo = Server.MapPath("../../GestionInvestigacion/Archivos/Concursos/Bases/" + codigo)
    '        linea_error = "1 - " + strRutaArchivo
    '        If Directory.Exists(strRutaArchivo) Then
    '        Else
    '            Directory.CreateDirectory(strRutaArchivo)
    '        End If
    '        '3.Nombre Final
    '        nomArchivo = nomArchivo & System.IO.Path.GetExtension(ArchivoSubir.FileName)
    '        linea_error = "4 - " + strRutaArchivo
    '        '4.Guardamos Archivo
    '        linea_error = "5.1 - " + strRutaArchivo & "/" & nomArchivo
    '        ArchivoSubir.SaveAs(strRutaArchivo & "/" & nomArchivo)

    '        'ActualizarArchivosConcurso(codigo, "/" & nomArchivo)

    '        linea_error = "5 - " + strRutaArchivo
    '        Data.Add("msje", "OK")
    '        Data.Add("alert", "success")
    '        list.Add(Data)
    '        JSONresult = serializer.Serialize(list)
    '        Response.Write(JSONresult)
    '    Catch ex As Exception
    '        Data.Add("msje", ex.Message)
    '        Data.Add("alert", "error" + linea_error)
    '        list.Add(Data)
    '        JSONresult = serializer.Serialize(list)
    '        Response.Write(JSONresult)
    '    End Try
    'End Sub

    Sub SubirArchivoMH(ByVal codigo As String, ByVal ArchivoSubir As HttpPostedFile)
        Try
            Dim post As HttpPostedFile = ArchivoSubir
            Dim codigo_con As String = codigo
            Dim NroRend As String = 3
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
            'list.Add("Nombre", System.IO.Path.GetFileName(post.FileName).Replace(",", ""))
            list.Add("Nombre", Regex.Replace(System.IO.Path.GetFileName(post.FileName), "[^0-9A-Za-z._ ]", "_").Replace(",", "")) '15/10/2019
            list.Add("TransaccionId", codigo_con)
            list.Add("TablaId", "9")
            list.Add("NroOperacion", NroRend)
            list.Add("Archivo", System.Convert.ToBase64String(binData, 0, binData.Length))
            list.Add("Usuario", Usuario)
            list.Add("Equipo", "")
            list.Add("Ip", "")
            list.Add("param8", Usuario)

            Dim envelope As String = wsCloud.SoapEnvelope(list)
            Dim result As String = wsCloud.PeticionRequestSoap(ruta, envelope, "http://usat.edu.pe/UploadFile", Session("perlogin").ToString)
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


    Private Sub Eliminar(ByVal codigo As Integer, ByVal codigo_per As Integer)
        Dim obj As New ClsGestionInvestigacion
        Dim Data1 As New Dictionary(Of String, Object)()
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()
        Try
            Dim dt As New Data.DataTable
            Dim data As New Dictionary(Of String, Object)()
            dt = obj.EliminarConcurso(codigo, codigo_per)
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

End Class
