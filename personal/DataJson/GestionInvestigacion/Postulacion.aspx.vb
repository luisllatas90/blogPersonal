Imports System.IO
Imports System.Web.HttpRequest
Imports System.Collections.Generic
Imports System.Data
Partial Class DataJson_GestionInvestigacion_Postulacion
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
                    Dim codigo_con As Integer = obj.DecrytedString64(Request("hdcod"))
                    Listar("LXP", 0, codigo_con, Session("id_per"), ctf)
                Case "ListarPostulacionExterna"
                    Dim ctf As Integer = Request("ctf")
                    Dim codigo_con As Integer = obj.DecrytedString64(Request("hdcod"))
                    Listar("LPEX", 0, codigo_con, Session("id_per"), ctf)
                Case "Registrar"
                    If Request("hdcodPos") <> "0" Then
                        k = obj.DecrytedString64(Request("hdcodPos"))
                    Else
                        k = Request("hdcodPos")
                    End If
                    Dim codigo_con As Integer = 0
                    If Request("hdcodCon") <> "0" Then
                        codigo_con = obj.DecrytedString64(Request("hdcodCon"))
                    Else
                        codigo_con = Request("hdcodCon")
                    End If
                    Dim titulo As String = Request("txttituloPos")
                    Dim codigo_linea As String = obj.DecrytedString64(Request("cboLinea"))
                    Dim codigo_dis As Integer = 0
                    If Request("cboDisciplina") <> "" Then
                        codigo_dis = Request("cboDisciplina")
                    End If
                    Dim codigo_reg As Integer = Request("cboRegion")
                    'Dim codigo_prov As Integer = Request("cboProvincia")
                    Dim codigo_dist As Integer = Request("cboDistrito")
                    Dim lugar As String = Request("txtLugar")
                    Dim codigo_per As Integer = 0
                    If Request("rbtipo") = 0 Then
                        codigo_per = Session("id_per")
                    End If
                    Dim codigo_gru As Integer = 0
                    If Request("rbtipo") = 1 Or Request("rbtipo") = 2 Then
                        codigo_gru = Request("cboGrupo")
                    End If
                    Dim resumen As String = Request("txtresumen")
                    Dim palabras As String = Request("txtpalabras")
                    Dim justificacion As String = Request("txtjustificacion")
                    Dim fechaini As String = Request("txtfeciniPos")
                    Dim fechafin As String = Request("txtfecfinPos")

                    Dim ctf As String = Request("ctf")
                    Dim codigo_alu As Integer = 0
                    Dim codigo_doc As Integer = 0
                    Actualizar(k, codigo_con, codigo_per, codigo_gru, codigo_alu, titulo, codigo_doc, codigo_linea, codigo_dis, codigo_reg, codigo_dist, lugar, resumen, palabras, justificacion, fechaini, fechafin, Session("id_per"), ctf)
                Case "RegistrarObjetivos"
                    k = obj.DecrytedString64(Request("hdcodP"))
                    Dim objetivos() As Object = serializer.DeserializeObject(Request("array"))
                    ActualizarObjetivos(k, objetivos, Session("id_per"))
                Case "RegistrarEquipo"
                    k = obj.DecrytedString64(Request("hdcodP"))
                    Dim equipo() As Object = serializer.DeserializeObject(Request("array"))
                    ActualizarEquipo(k, equipo, Session("id_per"))
                Case "SurbirArchivo"
                    Dim ArchivoASubir As HttpPostedFile = HttpContext.Current.Request.Files("ArchivoASubir")
                    'HCano 22/06/18
                    Dim codigo As String
                    If Request("codigo") = "" Then
                        codigo = Request("codigo")
                    Else
                        codigo = obj.DecrytedString64(Request("codigo")).ToString
                    End If
                    'Fin Hcano 22/06/18
                    Dim tipo As String = Request("tipo")
                    SubirArchivoMH(codigo, ArchivoASubir, tipo)
                Case "Editar" ' EN ESTE CASO SOLO SERA VISUALIZACION
                    Dim ctf As Integer = Request("ctf")
                    Dim codigo_pos As Integer = obj.DecrytedString64(Request("hdcodPost"))
                    Listar("E", codigo_pos, 0, Session("id_per"), ctf)
                Case "ListaObjetivos"
                    Dim codigo_pos As Integer = obj.DecrytedString64(Request("hdcodPost"))
                    ListarObjetivos(codigo_pos)
                Case "ListarEquipoPostulacion"
                    Dim codigo_pos As Integer = obj.DecrytedString64(Request("hdcodPost"))
                    ListarEquipo(codigo_pos)
                Case "ListarPostulaciones"
                    Dim ctf As Integer = Request("ctf")
                    Dim codigo_con As Integer = obj.DecrytedString64(Request("hdcod"))
                    Listar("L", 0, codigo_con, Session("id_per"), ctf)
                Case "ActualizarEtapa"
                    Dim ctf As Integer = Request("ctf")
                    Dim codigo_pos As Integer = obj.DecrytedString64(Request("hdcodPos"))
                    Dim codigo_Etapa As Integer = Request("cod_etapa")
                    ActualizarEtapa(codigo_pos, codigo_Etapa, Session("id_per"), ctf)
                Case "ListarPostulacionEvaluacionExterna"
                    Dim ctf As Integer = Request("ctf")
                    Dim codigo_con As Integer = obj.DecrytedString64(Request("hdcod"))
                    Listar("LPE", 0, codigo_con, Session("id_per"), ctf)
                Case "ListaEvaluadores"
                    Dim codigo_pos As Integer = obj.DecrytedString64(Request("hdcodPos"))
                    ListarEvaluadores(codigo_pos)
                Case "AsignarEvaluador"
                    Dim codigo_pos As Integer = obj.DecrytedString64(Request("hdcod"))
                    Dim codigo_Etapa As Integer = obj.DecrytedString64(Request("cboEvaluador"))
                    AsignarEvaluador(codigo_pos, codigo_Etapa, Session("id_per"))
                Case "QuitarEvaluador"
                    Dim codigo_eva As Integer = obj.DecrytedString64(Request("hdcod"))
                    QuitarEvaluador(codigo_eva, Session("id_per"))

                Case "RegistrarPostulacionExterna"
                    If Request("hdcodPos") <> "0" Then
                        k = obj.DecrytedString64(Request("hdcodPos"))
                    Else
                        k = Request("hdcodPos")
                    End If
                    Dim codigo_con As Integer = 0
                    If Request("hdcodCon") <> "0" Then
                        codigo_con = obj.DecrytedString64(Request("hdcodCon"))
                    Else
                        codigo_con = Request("hdcodCon")
                    End If
                    Dim titulo As String = "POSTULACIÓN CONVOCATORIA EXTERNA"
                    Dim codigo_linea As String = 0
                    Dim codigo_dis As Integer = 0
                    Dim codigo_reg As Integer = 0
                    Dim codigo_dist As Integer = 0
                    Dim lugar As String = ""
                    Dim codigo_per As Integer = 0
                    'If Request("rbtipo") = 0 Then
                    codigo_per = Session("id_per")
                    'End If
                    Dim codigo_gru As Integer = 0
                    'If Request("rbtipo") = 1 Or Request("rbtipo") = 2 Then
                    '    codigo_gru = Request("cboGrupo")
                    'End If
                    Dim resumen As String = ""
                    Dim palabras As String = ""
                    Dim justificacion As String = ""
                    Dim fechaini As String = Date.Now.ToString("dd\/MM\/yyyy")
                    Dim fechafin As String = Date.Now.ToString("dd\/MM\/yyyy")

                    Dim ctf As String = Request("ctf")
                    Dim codigo_alu As Integer = 0
                    Dim codigo_doc As Integer = 0
                    Actualizar(k, codigo_con, codigo_per, codigo_gru, codigo_alu, titulo, codigo_doc, codigo_linea, codigo_dis, codigo_reg, codigo_dist, lugar, resumen, palabras, justificacion, fechaini, fechafin, Session("id_per"), ctf)
            End Select
        Catch ex As Exception
            Data.Add("idper", Session("id_per"))
            Data.Add("rpta", ex.Message & "0 - LOAD" & Request("referencia"))
            list.Add(Data)
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)
        End Try
    End Sub

    Private Sub Listar(ByVal tipo As String, ByVal codigo_pos As Integer, ByVal codigo_con As Integer, ByVal id As Integer, ByVal ctf As Integer)
        Dim obj As New ClsGestionInvestigacion
        Dim Data1 As New Dictionary(Of String, Object)()
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()
        Try
            Dim dt As New Data.DataTable
            dt = obj.ListarPostulacion(tipo, codigo_pos, codigo_con, id, ctf)
            If dt.Rows.Count > 0 Then
                For i As Integer = 0 To dt.Rows.Count - 1
                    Dim data As New Dictionary(Of String, Object)()
                    data.Add("cod", obj.EncrytedString64(dt.Rows(i).Item("codigo_pos")))
                    data.Add("codSE", dt.Rows(i).Item("codigo_pos"))
                    data.Add("titulo", dt.Rows(i).Item("titulo_pos"))
                    data.Add("des_etapa", dt.Rows(i).Item("descripcion_eta"))
                    data.Add("fechareg", dt.Rows(i).Item("fechareg"))
                    If tipo = "LPE" Then
                        data.Add("cantidad", dt.Rows(i).Item("cantidad"))
                        data.Add("orden", dt.Rows(i).Item("orden_rco"))
                        data.Add("email", dt.Rows(i).Item("email"))
                        data.Add("responsable", dt.Rows(i).Item("responsable"))
                    End If
                    If tipo = "L" Then
                        data.Add("coord", dt.Rows(i).Item("Coordinador"))
                        data.Add("archivo", dt.Rows(i).Item("archivo"))
                        data.Add("tipo", dt.Rows(i).Item("tipo"))
                    End If
                    If tipo = "LXP" Or tipo = "LPEX" Then
                        data.Add("personal", dt.Rows(i).Item("Personal"))
                    End If
                    'data.Add("fecini", dt.Rows(i).Item("fechaini_con"))
                    'data.Add("fecfin", dt.Rows(i).Item("fechafin_con"))
                    'data.Add("tipo", dt.Rows(i).Item("tipo_con"))
                    If tipo = "E" Then
                        data.Add("inv", dt.Rows(i).Item("codigo_inv"))
                        data.Add("gru", dt.Rows(i).Item("codigo_gru"))
                        data.Add("linea", obj.EncrytedString64(dt.Rows(i).Item("codigo_lin")))
                        data.Add("cod_dis", dt.Rows(i).Item("codigo_dis"))
                        data.Add("region", dt.Rows(i).Item("codigo_reg"))
                        data.Add("distrito", dt.Rows(i).Item("codigo_distrito"))
                        data.Add("provincia", dt.Rows(i).Item("codigo_pro"))
                        data.Add("lugar", dt.Rows(i).Item("lugar_pos"))
                        data.Add("resumen", dt.Rows(i).Item("resumen_pos"))
                        data.Add("palabras", dt.Rows(i).Item("palabrasclave_pos"))
                        data.Add("justificacion", dt.Rows(i).Item("justificacion_pos"))
                        data.Add("fechaini", dt.Rows(i).Item("fechaini"))
                        data.Add("fechafin", dt.Rows(i).Item("fechafin"))
                        data.Add("nom", dt.Rows(i).Item("nombre"))
                        data.Add("dina", dt.Rows(i).Item("dina"))
                        data.Add("cod_sub", dt.Rows(i).Item("codigo_sa_ocde"))
                        data.Add("cod_area", dt.Rows(i).Item("codigo_ocde"))
                        data.Add("es_alumno", dt.Rows(i).Item("es_alumno"))
                        data.Add("etapa", dt.Rows(i).Item("codigo_eta"))
                        data.Add("tipo_gru", dt.Rows(i).Item("tipo_gru"))
                        If dt.Rows(i).Item("es_alumno") = 1 Then
                            data.Add("asesor", dt.Rows(i).Item("asesor"))
                            data.Add("facultad", dt.Rows(i).Item("nombre_Fac"))
                            data.Add("cpf", dt.Rows(i).Item("nombre_Cpf"))
                            data.Add("nrodoc", dt.Rows(i).Item("nroDocIdent_Alu"))
                            data.Add("fechanac", dt.Rows(i).Item("fechaNacimiento_Alu"))
                            data.Add("email", dt.Rows(i).Item("Email"))
                            data.Add("telfijo", dt.Rows(i).Item("TelFijo"))
                            data.Add("alumno", dt.Rows(i).Item("alumno"))

                        End If
                        If dt.Rows(i).Item("resultados") = "" Then
                            data.Add("rutaresultados", "")
                        Else
                            If dt.Rows(i).Item("es_alumno") = 1 Then
                                data.Add("rutaresultados", dt.Rows(i).Item("resultados"))
                               Else
                                data.Add("rutaresultados", dt.Rows(i).Item("resultados"))
                            End If
                        End If

                        If dt.Rows(i).Item("pto") = "" Then
                            data.Add("rutapto", "")
                        Else
                            If dt.Rows(i).Item("es_alumno") = 1 Then
                                data.Add("rutapto", dt.Rows(i).Item("pto"))
                               Else
                                data.Add("rutapto", dt.Rows(i).Item("pto"))
                            End If
                        End If
                        If dt.Rows(i).Item("cronograma") = "" Then
                            data.Add("rutacronograma", "")
                        Else
                            If dt.Rows(i).Item("es_alumno") = 1 Then
                               data.Add("rutacronograma", dt.Rows(i).Item("cronograma"))
                                Else
                                data.Add("rutacronograma", dt.Rows(i).Item("cronograma"))
                            End If
                        End If
                        If dt.Rows(i).Item("informe") = "" Then
                            data.Add("rutainforme", "")
                        Else
                            If dt.Rows(i).Item("es_alumno") = 1 Then
                                data.Add("rutainforme", dt.Rows(i).Item("informe"))
                               Else
                                data.Add("rutainforme", dt.Rows(i).Item("informe"))
                               End If
                        End If
                        If dt.Rows(i).Item("declaracion") = "" Then
                            data.Add("rutadeclaracion", "")
                        Else
                            If dt.Rows(i).Item("es_alumno") = 1 Then
                                data.Add("rutadeclaracion", dt.Rows(i).Item("declaracion"))
                                 Else
                                data.Add("rutadeclaracion", dt.Rows(i).Item("declaracion"))
                            End If
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

    Private Sub Actualizar(ByVal codigo_pos As Integer, ByVal codigo_con As Integer, ByVal codigo_per As Integer, ByVal codigo_gru As Integer, ByVal codigo_alu As Integer, ByVal titulo As String, ByVal codigo_doc As Integer, ByVal codigo_linea As Integer, ByVal codigo_dis As Integer, ByVal codigo_region As Integer, ByVal codigo_dist As Integer, ByVal lugar As String, ByVal resumen As String, ByVal palabras As String, ByVal justificacion As String, ByVal fechaini As String, ByVal fechafin As String, ByVal id As Integer, ByVal ctf As Integer)
        Dim obj As New ClsGestionInvestigacion
        Dim Data As New Dictionary(Of String, Object)()
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()
        Try
            Dim dt As New Data.DataTable
            dt = obj.ActualizarPostulacion(codigo_pos, codigo_con, codigo_per, codigo_gru, codigo_alu, titulo, codigo_doc, codigo_linea, codigo_dis, codigo_region, codigo_dist, lugar, resumen, palabras, justificacion, fechaini, fechafin, id, ctf)
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
                'dt = obj.ActualizarObjetivoPostulacion(codigo_obj, codigo_pro, objetivos(i).Item("descripcion"), objetivos(i).Item("codtipo"), objetivos(i).Item("estado"), id)
                dt = obj.ActualizarObjetivoPostulacion(codigo_obj, codigo_pro, objetivos(i).Item("descripcion"), objetivos(i).Item("codtipo"), 1, id)
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

    Private Sub ActualizarEquipo(ByVal codigo_pos As Integer, ByVal equipo() As Object, ByVal id As Integer)
        Dim obj As New ClsGestionInvestigacion
        Dim Data2 As New Dictionary(Of String, Object)()
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()
        Try

            Dim dt As New Data.DataTable
            Dim cod_inv As Integer = 0
            Dim codigo_alu As Integer = 0
            Dim codigo_per As Integer = 0
            Dim codigo_rol As Integer = 0
            For i As Integer = 0 To equipo.Length - 1
                Dim Data As New Dictionary(Of String, Object)()

                If equipo(i).Item("cod_inv") <> "0" Then
                    cod_inv = obj.DecrytedString64(equipo(i).Item("cod_inv"))
                Else
                    cod_inv = equipo(i).Item("cod_inv")
                End If

                If equipo(i).Item("cod_per") <> "0" Then
                    codigo_per = obj.DecrytedString64(equipo(i).Item("cod_per"))
                Else
                    codigo_per = equipo(i).Item("cod_per")
                End If

                If equipo(i).Item("cod_alu") <> "0" Then
                    codigo_alu = obj.DecrytedString64(equipo(i).Item("cod_alu"))
                Else
                    codigo_alu = equipo(i).Item("cod_alu")
                End If

                If equipo(i).Item("cod_rol") <> "0" Then
                    codigo_rol = obj.DecrytedString64(equipo(i).Item("cod_rol"))
                Else
                    codigo_rol = equipo(i).Item("cod_rol")
                End If

                Dim dedicacion As String = equipo(i).Item("dedicacion")
                'If cod_inv = 0 Then
                '    codigo_per = obj.DecrytedString64(equipo(i).Item("cod_doc"))
                'End If

                'Si tiene Rol 0 y es el primero del equipo - va como coordinador general, los demas como personal de apoyo
                If codigo_rol = 0 And i = 0 Then
                    codigo_rol = 1
                End If
                If codigo_rol = 0 And i > 0 Then
                    codigo_rol = 4
                End If

                If equipo(i).Item("estado") = 1 Then
                    dt = obj.ActualizarEquipoPostulacion(codigo_pos, cod_inv, codigo_per, codigo_alu, codigo_rol, dedicacion, id)
                End If

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
            strRutaArchivo = Server.MapPath("../../GestionInvestigacion/Archivos/Postulacion/" + codigo)
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

            ActualizarArchivosPostulacion(codigo, "/" & tipo & "/" & nomArchivo, tipo)

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
    Private Sub ActualizarArchivosPostulacion(ByVal codigo_pos As Integer, ByVal ruta As String, ByVal tipo As String)
        Dim obj As New ClsGestionInvestigacion
        Dim Data1 As New Dictionary(Of String, Object)()
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()
        Try
            Dim dt As New Data.DataTable
            dt = obj.ActualizarArchivosdePostulacion(codigo_pos, ruta, tipo)
            Dim data As New Dictionary(Of String, Object)()
            data.Add("cod", obj.EncrytedString64(codigo_pos))
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

    Private Sub ListarObjetivos(ByVal codigo As Integer)
        Dim obj As New ClsGestionInvestigacion
        Dim Data1 As New Dictionary(Of String, Object)()
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()
        Try
            Dim dt As New Data.DataTable
            dt = obj.ListarObjetivosPostulacion(codigo)
            If dt.Rows.Count > 0 Then
                For i As Integer = 0 To dt.Rows.Count - 1
                    Dim data As New Dictionary(Of String, Object)()
                    data.Add("cod", obj.EncrytedString64(dt.Rows(i).Item("codigo")))
                    data.Add("des", dt.Rows(i).Item("descripcion"))
                    data.Add("codtipo", dt.Rows(i).Item("tipo_opo"))
                    data.Add("tipo", dt.Rows(i).Item("tipo"))
                    data.Add("estado", dt.Rows(i).Item("estado_opo"))
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
            dt = obj.ListarEquipoPostulacion(codigo)
            If dt.Rows.Count > 0 Then
                For i As Integer = 0 To dt.Rows.Count - 1
                    Dim data As New Dictionary(Of String, Object)()
                    'data.Add("cod", obj.EncrytedString64(dt.Rows(i).Item("codigo")))
                    data.Add("nombre", dt.Rows(i).Item("nombre"))
                    data.Add("dina", dt.Rows(i).Item("dina"))
                    data.Add("rol", dt.Rows(i).Item("rol"))
                    data.Add("dedicacion", dt.Rows(i).Item("dedicacion"))
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
    Private Sub ActualizarEtapa(ByVal codigo_pos As Integer, ByVal codigo_Etapa As Integer, ByVal id As Integer, ByVal ctf As Integer)
        Dim obj As New ClsGestionInvestigacion
        Dim Data2 As New Dictionary(Of String, Object)()
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()
        Try

            Dim dt As New Data.DataTable
            Dim Data As New Dictionary(Of String, Object)()
            dt = obj.ActualizarEtapaPostulacion(codigo_pos, codigo_Etapa, id, ctf)
            Data.Add("rpta", dt.Rows(0).Item("Respuesta"))
            Data.Add("msje", dt.Rows(0).Item("Mensaje").ToString)
            list.Add(Data)

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

    Private Sub ListarEvaluadores(ByVal codigo As Integer)
        Dim obj As New ClsGestionInvestigacion
        Dim Data1 As New Dictionary(Of String, Object)()
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()
        Try
            Dim dt As New Data.DataTable
            dt = obj.ListarEvaluadoresPostulacion(codigo)
            If dt.Rows.Count > 0 Then
                For i As Integer = 0 To dt.Rows.Count - 1
                    Dim data As New Dictionary(Of String, Object)()
                    data.Add("cod", obj.EncrytedString64(dt.Rows(i).Item("codigo")))
                    data.Add("nombre", dt.Rows(i).Item("nombre"))
                    data.Add("dina", dt.Rows(i).Item("dina"))
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

    Private Sub AsignarEvaluador(ByVal codigo_pos As Integer, ByVal codigo_eva As Integer, ByVal id As Integer)
        Dim obj As New ClsGestionInvestigacion
        Dim Data2 As New Dictionary(Of String, Object)()
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()
        Try
            Dim dt As New Data.DataTable
            Dim Data As New Dictionary(Of String, Object)()
            dt = obj.AsignarEvaluador(codigo_pos, codigo_eva, id)
            Data.Add("rpta", dt.Rows(0).Item("Respuesta"))
            Data.Add("msje", dt.Rows(0).Item("Mensaje").ToString)
            list.Add(Data)
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
    Private Sub QuitarEvaluador(ByVal codigo_eva As Integer, ByVal id As Integer)
        Dim obj As New ClsGestionInvestigacion
        Dim Data2 As New Dictionary(Of String, Object)()
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()
        Try
            Dim dt As New Data.DataTable
            Dim Data As New Dictionary(Of String, Object)()
            dt = obj.QuitarEvaluador(codigo_eva, id)
            Data.Add("rpta", dt.Rows(0).Item("Respuesta"))
            Data.Add("msje", dt.Rows(0).Item("Mensaje").ToString)
            list.Add(Data)
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


    Sub SubirArchivoMH(ByVal codigo As String, ByVal ArchivoSubir As HttpPostedFile, ByVal tipo As String)
        Try
            Dim post As HttpPostedFile = ArchivoSubir
            Dim codigo_con As String = codigo
            Dim NroRend As String = 0
            If tipo = "PRESUPUESTO" Then
                NroRend = 4
            End If
            If tipo = "CRONOGRAMA" Then
                NroRend = 5
            End If
            If tipo = "PROPUESTA" Then
                NroRend = 6
            End If
            If tipo = "DECLARACION" Then
                NroRend = 7
            End If
            If tipo = "RESULTADOSESPERADOS" Then
                NroRend = 8
            End If
            'HCano 22/06/18
            If tipo = "RUBRICA" Then
                NroRend = 10
                Dim obj As New ClsGestionInvestigacion
                If Request("cod_evaluador") <> "" Then
                    codigo_con = obj.DecrytedString64(Request("cod_evaluador"))
                End If
            End If
            'Fin HCano 22/06/18


            Dim Fecha As String = Date.Now.ToString("dd/MM/yyyy")
            Dim Usuario As String = Session("perlogin").ToString
            Dim Input(post.ContentLength) As Byte
            ' Dim b As New BinaryReader(post.InputStream)
            '  Dim by() As Byte = b.ReadByte(post.ContentLength)


            Dim b As New BinaryReader(post.InputStream)
            Dim binData As Byte() = b.ReadBytes(post.InputStream.Length)
            Dim base64 = System.Convert.ToBase64String(binData)

            Dim wsCloud As New ClsArchivosCompartidosV2
            Dim list As New Dictionary(Of String, String)
            '  Dim list As New List(Of Dictionary(Of String, String))()
            list.Add("Fecha", Fecha)
            list.Add("Extencion", System.IO.Path.GetExtension(post.FileName))
            'list.Add("Nombre", System.IO.Path.GetFileName(post.FileName.Replace("&", "_").Replace("'", "_").Replace("*", "_").Replace("á", "_").Replace("é", "_").Replace("í", "_").Replace("ó", "_").Replace("ú", "_").Replace("Á", "_").Replace("É", "_").Replace("Í", "_").Replace("Ó", "_").Replace("Ú", "_").Replace("Ñ", "_").Replace("ñ", "_").Replace(",", "_"))) 'HCano 31/05/18
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
            Data1.Add("rpta", "1 - SUBIR ARCHIVO")
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
            Data1.Add("rpta", "1 - ACTUALIZAR ARCHIVO COMP." + idtransaccion)
            Data1.Add("msje", ex.Message)
            list.Add(Data1)
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)
        End Try
    End Sub

End Class