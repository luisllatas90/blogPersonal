Imports System.IO
Imports System.Web.HttpRequest
Imports System.Collections.Generic
Imports System.Data
Partial Class DataJson_GestionInvestigacion_Tesis
    Inherits System.Web.UI.Page

    Dim ruta As String = ConfigurationManager.AppSettings("SharedFiles")
    Dim rutareporte As String = ConfigurationManager.AppSettings("RutaReporte")

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim obj As New ClsGestionInvestigacion
        Dim Data As New Dictionary(Of String, Object)()
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()
        Try
            Dim k As String = "0" 'Request("k")
            Dim f As String = ""

            'Select Case obj.DecrytedString64(Request("action"))
            Select Case Request("action")
                Case "lSemestre"
                    Dim tipo As String = Request("tipo")
                    Dim param1 As String = Request("param1")
                    ListaSemestre(tipo, param1)
                Case "lCarrera"
                    Dim codigo_cac As Integer = obj.DecrytedString64(Request("cac"))
                    Dim etapa As String = Request("etapa")
                    Dim codigo_per As Integer = Request("id")
                    Dim ctf As Integer = Request("ctf")
                    ListaCarreraProfesional(codigo_cac, etapa, codigo_per, ctf)
                Case "lCursosxDocente"
                    Dim codigo_cac As Integer = obj.DecrytedString64(Request("cboSemestre"))
                    Dim etapa As String = Request("cboEtapa")
                    Dim codigo_per As Integer = Request("id")
                    Dim ctf As Integer = Request("ctf")
                    Dim codigo_cpf As Integer = obj.DecrytedString64(Request("cboCarrera"))
                    ListaCursosxDocente(codigo_cac, etapa, codigo_per, ctf, codigo_cpf)
                Case "lAlumnosxCurso"
                    Dim codigo_cup As Integer = obj.DecrytedString64(Request("cboCurso"))
                    ListaAlumnosxCurso(codigo_cup)
                Case "lTipoInvestigacion"
                    Dim tipo As String = Request("tipo")
                    Dim param1 As String = Request("param1")
                    ListaTipoInvestigacion(tipo, param1)
                Case "lTipoParticipante"
                    Dim tipo As String = Request("tipo")
                    Dim param1 As String = Request("param1")
                    ListaTipoParticipante(tipo, param1)
                Case "lDepartamentoAcademico"
                    Dim tipo As String = Request("tipo")
                    Dim param1 As String = ""
                    Dim param2 As String = ""
                    ListaDepartamentoAcademico(tipo, param1, param2)
                Case "lPersonalDepartamentoAcademico"
                    Dim codigo_dac As String
                    If Request("param1") <> "%" Then
                        codigo_dac = obj.DecrytedString64(Request("param1"))
                    Else
                        codigo_dac = Request("param1")
                    End If

                    ListaPersonalxDepartamentoAcad(codigo_dac)
                Case "lDatosAlumno"
                    Dim tipo As String = "CA"
                    Dim param1 As Integer = Request("param1")
                    Dim param2 As Integer = obj.DecrytedString64(Request("param2"))
                    Dim param3 As String = obj.DecrytedString64(Request("param3"))
                    ConsultarTesis(tipo, param1, param2, param3)
                Case "lDatosTesis"
                    Dim tipo As String = "CD"
                    Dim param1 As Integer = obj.DecrytedString64(Request("param1"))
                    Dim param2 As Integer = obj.DecrytedString64(Request("param2"))
                    Dim param3 As String = obj.DecrytedString64(Request("param3"))
                    ConsultarTesis(tipo, param1, param2, param3)
                Case "Registrar"
                    If Request("hdcod") <> "0" Then
                        k = obj.DecrytedString64(Request("hdcod"))
                    Else
                        k = Request("hdcod")
                    End If
                    Dim codigo_tin As Integer = obj.DecrytedString64(Request("cbotipoInvestigacion"))
                    Dim problema As String = "" ' POR SI SE AGREGA
                    Dim resumen As String = "" ' POR SI SE AGREGA
                    Dim titulo As String = Request("txtTitulo")
                    Dim fechaini As String = Request("txtfeciniTes")
                    Dim fechafin As String = Request("txtfecfinTes")
                    'Dim fechaSustentacionI As String = Request("txtFechaSustentacionI")
                    Dim codigo_eti As Integer = 4 'PROYECTO DE INVESTIGACION / TABLA Etapainvestigacion
                    Dim financiamiento_multiple As String = ""
                    Dim financiamiento_externo As String = ""
                    Dim financiamiento_usat As String = ""
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
                            financiamiento_usat = Request("txtusat")
                        Else
                            financiamiento_multiple = financiamiento_multiple + ",U"
                            financiamiento_usat = Request("txtusat")
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
                    Dim presupuesto As String = Request("txtpresupuesto")

                    Dim fechaSustentacionP As String = Request("txtFechaSustentacionP")

                    Dim NotaSustentacionP As String = Request("txtNotaSustentacionP")

                    Dim avance As String = Request("txtavance")

                    'Dim proyecto As HttpPostedFile = Nothing
                    'If Request.Files("file_proyecto").ContentLength > 0 Then
                    '    proyecto = HttpContext.Current.Request.Files("file_proyecto")
                    'End If
                    'Dim acta As HttpPostedFile = Nothing
                    'If Request.Files("file_acta").ContentLength > 0 Then
                    '    acta = HttpContext.Current.Request.Files("file_acta")
                    'End If
                    Dim codigo_lin As Integer = obj.DecrytedString64(Request("cboLinea"))
                    Dim codigo_dis_ocde As Integer = Request("cboDisciplina")
                    '    Dim estadoavance As String = Request("cboAvance")
                    '    Dim informe As String = ""
                    '    If Request("fileinforme") <> "" Then
                    '        informe = Request("fileinforme")
                    '    End If
                    '    Dim ctf As String = Request("ctf")
                    '    presupuesto = Request("txtpresupuesto")
                    ActualizarTesis(k, codigo_tin, titulo, problema, resumen, codigo_lin, codigo_dis_ocde, fechaini, fechafin, codigo_eti, Session("id_per"), presupuesto, avance, financiamiento_multiple, financiamiento_usat, financiamiento_externo, fechaSustentacionP, NotaSustentacionP)
                Case "lAutor"
                    Dim codigo_tes As Integer = obj.DecrytedString64(Request("param1"))
                    ListaAutorxTesis(codigo_tes)
                Case "RegAutor"
                    k = obj.DecrytedString64(Request("hdcod")) ' Codigo de Tesis
                    Dim equipo() As Object = serializer.DeserializeObject(Request("array"))
                    ActualizarAutor(k, equipo, Session("id_per"))
                Case "lObjetivos"
                    Dim codigo_tes As Integer = obj.DecrytedString64(Request("param1"))
                    ListaObjetivosxTesis(codigo_tes)
                Case "RegObjetivos"
                    k = obj.DecrytedString64(Request("hdcod")) ' Codigo de Tesis
                    Dim objetivos() As Object = serializer.DeserializeObject(Request("array"))
                    ActualizarObjetivos(k, objetivos, Session("id_per"))
                Case "lParticipante"
                    Dim codigo_tes As Integer = obj.DecrytedString64(Request("param1"))
                    Dim tipo_tpi As String = Request("param2")
                    Dim abreviatura_etapa As String = Request("param3")
                    ListarParticipantesxTesis(codigo_tes, tipo_tpi, abreviatura_etapa)
                Case "RegAsesor"
                    k = obj.DecrytedString64(Request("hdcod")) ' Codigo de Tesis
                    Dim abreviatura_etapa As String = Request("abreviatura_eta")
                    Dim equipo() As Object = serializer.DeserializeObject(Request("array"))
                    ActualizarAsesor(k, abreviatura_etapa, equipo, Session("id_per"))
                    'Case "RegistrarObjetivos"
                    '    k = obj.DecrytedString64(Request("hdcodP"))
                    '    Dim objetivos() As Object = serializer.DeserializeObject(Request("array"))
                    '    ActualizarObjetivos(k, objetivos, Session("id_per"))
                    'Case "RegistrarEquipo"
                    '    k = obj.DecrytedString64(Request("hdcodP"))
                    '    Dim equipo() As Object = serializer.DeserializeObject(Request("array"))
                    '    ActualizarEquipo(k, equipo, Session("id_per"), "T")
                Case "SurbirArchivo"
                    Dim ArchivoASubir As HttpPostedFile = HttpContext.Current.Request.Files("ArchivoASubir")
                    Dim codigo As String = obj.DecrytedString64(Request("codigo")).ToString
                    Dim tipo As String = Request("tipo")
                    SubirArchivo(codigo, ArchivoASubir, tipo)
                Case "GenerarActa"
                    Dim codigo_tes As Integer = obj.DecrytedString64(Request("param1"))
                    Dim codigo_alu As Integer = obj.DecrytedString64(Request("param2"))
                    Dim etapa As String = Request("etapa")
                    GenerarActa(codigo_tes, codigo_alu, etapa)
                Case "ActualizarAsesor"
                    k = obj.DecrytedString64(Request("hdcod")) ' Codigo de Tesis
                    Dim abreviatura_etapa As String = Request("abreviatura_eta")
                    Dim codigo_per As Integer = obj.DecrytedString64(Request("codigo_per"))
                    RegistrarAsesor(k, abreviatura_etapa, codigo_per, Session("id_per"))
                Case "ListarObservacionesJurado"
                    k = obj.DecrytedString64(Request("hdcod")) ' Codigo de Tesis
                    Dim codigo_jurado As Integer = obj.DecrytedString64(Request("codigo_jur"))
                    ListarObservaciones(k, codigo_jurado)
                    'Case "ActualizarDatosTesis"
                    '    k = obj.DecrytedString64(Request("hdcod")) ' Codigo de Tesis
                    '    Dim fecha As String = Request("fec")
                    '    ActualizarDatosTesis(k, fecha)
                Case "AsignarDatosSustentacionInforme"
                    k = obj.DecrytedString64(Request("hdcod")) ' Codigo de Tesis
                    Dim nota As String = Request("nota")
                    Dim fecha As String = Request("fec")
                    AsignarDatosSustentacionInforme(k, nota, fecha)
                Case "EnviarTesis"
                    k = obj.DecrytedString64(Request("hdcod")) ' Codigo de Tesis
                    Dim codigo_alu As Integer = obj.DecrytedString64(Request("hdalu")) ' Codigo de alumno
                    Dim tipo As Integer = Request("tipo")
                    Dim etapa As String = Request("etapa")
                    EnviarTesis(k, Session("id_per"), codigo_alu, tipo, etapa)
                Case "RegObservacionDocente"
                    k = obj.DecrytedString64(Request("hdoTes")) ' Codigo de Tesis
                    Dim codigo_alu As Integer = obj.DecrytedString64(Request("hdoAlu"))
                    Dim descripcion As String = Request("txtObservacion")
                    Dim etapa As String = Request("hdoEtapa")
                    RegistrarObservacionDocente(k, Session("id_per"), codigo_alu, descripcion, etapa)
                Case "lstObservacionDocente"
                    k = obj.DecrytedString64(Request("hdoTes")) ' Codigo de Tesis
                    Dim etapa As String = Request("hdoEtapa")
                    ListarObservacionesDocente(k, etapa)
                Case "EliminarObservacionDocente"
                    k = obj.DecrytedString64(Request("hdcod")) ' Codigo de observacion docente
                    EliminarObservacionDocente(k)
                Case "lstAreaOCDExLineaUSAT"
                    k = obj.DecrytedString64(Request("cdlinea")) ' Codigo de linea    
                    ListarAreaOCDExLineaUSAT(k)
                Case "ActualizarLinkInforme"
                    k = obj.DecrytedString64(Request("hdcod")) ' Codigo de tesis 
                    Dim link As String = Request("link")
                    ActualizarLinkInforme(k, link)
            End Select
        Catch ex As Exception
            Data.Add("idper", Session("id_per"))
            Data.Add("rpta", ex.Message & "0 - LOAD")
            Data.Add("LINEA", ex.StackTrace & "0 - LOAD")
            list.Add(Data)
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)
        End Try
    End Sub

    Private Sub ListaSemestre(ByVal tipo As String, ByVal param1 As String)
        Dim obj As New ClsGestionInvestigacion
        Dim Data1 As New Dictionary(Of String, Object)()
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()
        Try
            Dim dt As New Data.DataTable
            dt = obj.ConsultarCicloAcademico(tipo, param1)
            If dt.Rows.Count > 0 Then
                For i As Integer = 0 To dt.Rows.Count - 1
                    Dim data As New Dictionary(Of String, Object)()
                    data.Add("cod", obj.EncrytedString64(dt.Rows(i).Item("codigo_cac")))
                    data.Add("des", dt.Rows(i).Item("descripcion_cac"))
                    data.Add("vig", dt.Rows(i).Item("vigencia_cac"))
                    list.Add(data)
                Next
            End If
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)
        Catch ex As Exception
            Data1.Add("rpta", "ListaSemestre")
            Data1.Add("msje", ex.Message)
            list.Add(Data1)
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)
        End Try
    End Sub

    Private Sub ListaCarreraProfesional(ByVal codigo_cac As Integer, ByVal etapa As String, ByVal codigo_per As Integer, ByVal ctf As Integer)
        Dim obj As New ClsGestionInvestigacion
        Dim Data1 As New Dictionary(Of String, Object)()
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()
        Try
            Dim dt As New Data.DataTable
            dt = obj.ConsultarCarreraProfesionalTesisxDocente(codigo_cac, etapa, codigo_per, ctf)
            If dt.Rows.Count > 0 Then
                For i As Integer = 0 To dt.Rows.Count - 1
                    Dim data As New Dictionary(Of String, Object)()
                    data.Add("cod", obj.EncrytedString64(dt.Rows(i).Item("codigo_cpf")))
                    data.Add("des", dt.Rows(i).Item("nombre_cpf"))
                    list.Add(data)
                Next
            End If
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)
        Catch ex As Exception
            Data1.Add("rpta", "ListaCarrera")
            Data1.Add("msje", ex.Message)
            list.Add(Data1)
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)
        End Try
    End Sub

    Private Sub ListaCursosxDocente(ByVal codigo_cac As Integer, ByVal etapa As String, ByVal codigo_per As Integer, ByVal ctf As Integer, ByVal codigo_cpf As Integer)
        Dim obj As New ClsGestionInvestigacion
        Dim Data1 As New Dictionary(Of String, Object)()
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()
        Try
            Dim dt As New Data.DataTable
            dt = obj.ConsultarCursosTesisxDocente(codigo_cac, etapa, codigo_per, ctf, codigo_cpf)
            If dt.Rows.Count > 0 Then
                For i As Integer = 0 To dt.Rows.Count - 1
                    Dim data As New Dictionary(Of String, Object)()
                    data.Add("cod", obj.EncrytedString64(dt.Rows(i).Item("codigo")))
                    data.Add("des", dt.Rows(i).Item("descripcion"))
                    list.Add(data)
                Next
            End If
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)
        Catch ex As Exception
            Data1.Add("rpta", "ListaCursosxDocente")
            Data1.Add("msje", ex.Message)
            list.Add(Data1)
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)
        End Try
    End Sub

    Private Sub ListaAlumnosxCurso(ByVal codigo_cup As Integer)
        Dim obj As New ClsGestionInvestigacion
        Dim Data1 As New Dictionary(Of String, Object)()
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()
        Try
            Dim dt As New Data.DataTable
            dt = obj.ConsultarAlumnosxCurso(codigo_cup)
            If dt.Rows.Count > 0 Then
                For i As Integer = 0 To dt.Rows.Count - 1
                    Dim data As New Dictionary(Of String, Object)()
                    data.Add("cod_cup", obj.EncrytedString64(dt.Rows(i).Item("codigo_cup")))
                    'data.Add("nom", dt.Rows(i).Item("nombre_cur"))
                    'data.Add("grupo", dt.Rows(i).Item("grupoHor_Cup"))
                    data.Add("cod_alu", obj.EncrytedString64(dt.Rows(i).Item("codigo_alu")))
                    data.Add("coduniver", dt.Rows(i).Item("codigouniver_alu"))
                    data.Add("alumno", dt.Rows(i).Item("alumno"))
                    If dt.Rows(i).Item("tesis") = "0" Then
                        data.Add("cod_tes", dt.Rows(i).Item("tesis"))
                    Else
                        data.Add("cod_tes", obj.EncrytedString64(dt.Rows(i).Item("tesis")))
                    End If
                    data.Add("etapa", dt.Rows(i).Item("etapa"))
                    data.Add("fecBloqueoP", dt.Rows(i).Item("FechaBloqueoProyecto"))
                    data.Add("fecBloqueoI", dt.Rows(i).Item("FechaBloqueoInforme"))
                    data.Add("notaP", dt.Rows(i).Item("notaP"))
                    data.Add("notaI", dt.Rows(i).Item("notaI"))
                    data.Add("notaEjecucion", dt.Rows(i).Item("notaEjecucion"))
                    data.Add("notaInforme", dt.Rows(i).Item("notaInforme"))
                    list.Add(data)
                Next
            End If
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)
        Catch ex As Exception
            Data1.Add("rpta", "ListaAlumnosxCurso")
            Data1.Add("msje", ex.Message)
            list.Add(Data1)
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)
        End Try
    End Sub

    Private Sub ListaTipoInvestigacion(ByVal tipo As String, ByVal param1 As String)
        Dim obj As New ClsGestionInvestigacion
        Dim Data1 As New Dictionary(Of String, Object)()
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()
        Try
            Dim dt As New Data.DataTable
            dt = obj.ConsultarTipoinvestigacion(tipo, param1)
            If dt.Rows.Count > 0 Then
                For i As Integer = 0 To dt.Rows.Count - 1
                    Dim data As New Dictionary(Of String, Object)()
                    data.Add("cod", obj.EncrytedString64(dt.Rows(i).Item("codigo")))
                    data.Add("des", dt.Rows(i).Item("descripcion"))
                    list.Add(data)
                Next
            End If
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)
        Catch ex As Exception
            Data1.Add("rpta", "TipoInvestigacion")
            Data1.Add("msje", ex.Message)
            list.Add(Data1)
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)
        End Try
    End Sub

    Private Sub ListaTipoParticipante(ByVal tipo As String, ByVal param1 As String)
        Dim obj As New ClsGestionInvestigacion
        Dim Data1 As New Dictionary(Of String, Object)()
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()
        Try
            Dim dt As New Data.DataTable
            dt = obj.ConsultarTipoParticipante(tipo, param1)
            If dt.Rows.Count > 0 Then
                For i As Integer = 0 To dt.Rows.Count - 1
                    Dim data As New Dictionary(Of String, Object)()
                    data.Add("cod", obj.EncrytedString64(dt.Rows(i).Item("codigo")))
                    data.Add("des", dt.Rows(i).Item("descripcion"))
                    list.Add(data)
                Next
            End If
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)
        Catch ex As Exception
            Data1.Add("rpta", "TipoParticipante")
            Data1.Add("msje", ex.Message)
            list.Add(Data1)
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)
        End Try
    End Sub

    Private Sub ListaDepartamentoAcademico(ByVal tipo As String, ByVal param1 As String, ByVal param2 As String)
        Dim obj As New ClsGestionInvestigacion
        Dim Data1 As New Dictionary(Of String, Object)()
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()
        Try
            Dim dt As New Data.DataTable
            dt = obj.ConsultarDepartamentosAcademicos(tipo, param1, param2)
            If dt.Rows.Count > 0 Then
                For i As Integer = 0 To dt.Rows.Count - 1
                    Dim data As New Dictionary(Of String, Object)()
                    data.Add("cod", obj.EncrytedString64(dt.Rows(i).Item("codigo")))
                    data.Add("des", dt.Rows(i).Item("descripcion"))
                    list.Add(data)
                Next
            End If
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)
        Catch ex As Exception
            Data1.Add("rpta", "DptoAcad")
            Data1.Add("msje", ex.Message)
            list.Add(Data1)
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)
        End Try
    End Sub

    Private Sub ListaPersonalxDepartamentoAcad(ByVal codigo_Dac As String)
        Dim obj As New ClsGestionInvestigacion
        Dim Data1 As New Dictionary(Of String, Object)()
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()
        Try
            Dim dt As New Data.DataTable
            dt = obj.ConsultarPersonalxDepartamentoAcademico(codigo_Dac)
            If dt.Rows.Count > 0 Then
                For i As Integer = 0 To dt.Rows.Count - 1
                    Dim data As New Dictionary(Of String, Object)()
                    data.Add("cod", obj.EncrytedString64(dt.Rows(i).Item("codigo")))
                    data.Add("des", dt.Rows(i).Item("descripcion"))
                    list.Add(data)
                Next
            End If
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)
        Catch ex As Exception
            Data1.Add("rpta", "TipoInvestigacion")
            Data1.Add("msje", ex.Message)
            list.Add(Data1)
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)
        End Try
    End Sub

    Private Sub ConsultarTesis(ByVal tipo As String, ByVal param1 As String, ByVal param2 As String, ByVal param3 As String)
        Dim obj As New ClsGestionInvestigacion
        Dim Data1 As New Dictionary(Of String, Object)()
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()
        Try
            Dim dt As New Data.DataTable
            dt = obj.Consultartesis(tipo, param1, param2, param3)
            If dt.Rows.Count > 0 Then
                For i As Integer = 0 To dt.Rows.Count - 1
                    Dim data As New Dictionary(Of String, Object)()
                    data.Add("nom_alu", dt.Rows(i).Item("alumno"))
                    data.Add("coduniver", dt.Rows(i).Item("codigouniver_alu"))
                    data.Add("carrera", dt.Rows(i).Item("nombre_cpf"))
                    data.Add("facultad", dt.Rows(i).Item("nombre_fac"))
                    data.Add("cod_dac", obj.EncrytedString64(dt.Rows(i).Item("codigo_dac")))
                    If tipo = "CA" Then
                        data.Add("fecini_cac", dt.Rows(i).Item("fecini_cac"))
                    End If
                    If tipo = "CD" Then
                        data.Add("cod_tes", obj.EncrytedString64(dt.Rows(i).Item("codigo_tes")))
                        data.Add("cod_tin", obj.EncrytedString64(dt.Rows(i).Item("codigo_tin")))
                        data.Add("titulo", dt.Rows(i).Item("titulo_tes"))
                        data.Add("cod_linea", obj.EncrytedString64(dt.Rows(i).Item("cod_linea")))
                        data.Add("des_linea", dt.Rows(i).Item("nombre_linea"))
                        data.Add("fec_ini", dt.Rows(i).Item("fecha_ini"))
                        data.Add("fec_fin", dt.Rows(i).Item("fecha_fin"))
                        data.Add("financ", dt.Rows(i).Item("financiamiento_tes"))
                        data.Add("financexterno", dt.Rows(i).Item("financiamientoexterno_tes"))
                        data.Add("financusat", dt.Rows(i).Item("financiamientousat_tes"))
                        data.Add("presu", dt.Rows(i).Item("presupuesto_tes"))
                        data.Add("avance", dt.Rows(i).Item("avance_tes"))
                        data.Add("abrev_etapa", dt.Rows(i).Item("abrev_etapa"))
                        data.Add("etapa", dt.Rows(i).Item("nombre_etapa"))
                        data.Add("proyecto", dt.Rows(i).Item("proyecto"))
                        data.Add("similitudproyecto", dt.Rows(i).Item("similitudproyecto"))
                        data.Add("actaproyecto", dt.Rows(i).Item("actaproyecto"))
                        data.Add("preinforme", dt.Rows(i).Item("preinforme"))
                        data.Add("informe", dt.Rows(i).Item("informe"))
                        data.Add("linkinforme", dt.Rows(i).Item("linkinforme"))
                        data.Add("actainforme", dt.Rows(i).Item("actainforme"))
                        data.Add("similitudinforme", dt.Rows(i).Item("similitudinforme"))
                        data.Add("cod_dis", dt.Rows(i).Item("codigo_dis_ocde"))
                        data.Add("cod_sub", dt.Rows(i).Item("codigo_sa_ocde"))
                        data.Add("cod_area", dt.Rows(i).Item("codigo_ocde"))
                        'data.Add("fec_acta", dt.Rows(i).Item("fechaApruebaActa_tes"))
                        data.Add("fec_susP", dt.Rows(i).Item("fec_SustentacionP"))
                        data.Add("nota_E", dt.Rows(i).Item("notaEjecucion"))
                        data.Add("porcentaje_E", dt.Rows(i).Item("porcentajeEjecucion"))
                        data.Add("nota_I", dt.Rows(i).Item("notaInforme"))
                        data.Add("porcentaje_I", dt.Rows(i).Item("porcentajeInforme"))

                        If dt.Rows(i).Item("nota_sustentacionP") = 0 Then
                            data.Add("nota_susP", "")
                        Else
                            data.Add("nota_susP", dt.Rows(i).Item("nota_sustentacionP"))
                        End If

                        data.Add("fec_susI", dt.Rows(i).Item("fec_SustentacionI"))

                        If dt.Rows(i).Item("nota_sustentacionI") = 0 Then
                            data.Add("nota_susI", "")
                        Else
                            data.Add("nota_susI", dt.Rows(i).Item("nota_sustentacionI"))
                        End If

                        If dt.Rows(i).Item("Asesor") = "0" Then
                            data.Add("asesor", "")
                        Else
                            data.Add("asesor", obj.EncrytedString64(dt.Rows(i).Item("Asesor")))

                        End If
                    End If
                    list.Add(data)
                Next
            End If
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)
        Catch ex As Exception
            Data1.Add("rpta", "CargarDatTesis")
            Data1.Add("msje", ex.Message)
            list.Add(Data1)
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)
        End Try
    End Sub


    'Private Sub ActualizarDatosTesis(ByVal codigo_tes As Integer, ByVal fecha As String)
    '    Dim obj As New ClsGestionInvestigacion
    '    Dim Data As New Dictionary(Of String, Object)()
    '    Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
    '    Dim JSONresult As String = ""
    '    Dim list As New List(Of Dictionary(Of String, Object))()
    '    Try
    '        Dim dt As New Data.DataTable
    '        dt = obj.ActualizarDatosTesis(codigo_tes, fecha)
    '        'Data.Add("cod", obj.EncrytedString64(dt.Rows(0).Item("cod")))
    '        Data.Add("rpta", dt.Rows(0).Item("Respuesta"))
    '        Data.Add("msje", dt.Rows(0).Item("Mensaje").ToString)
    '        list.Add(Data)

    '        'If informe.ContentLength > 0 Then
    '        '    SubirArchivo(dt.Rows(0).Item("cod"), informe, "INFORME", "P", codigo_per)
    '        'End If

    '        'If acta.ContentLength > 0 Then
    '        '    SubirArchivo(dt.Rows(0).Item("cod"), acta, "ACTA", "P", codigo_per)
    '        'End If

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

    Private Sub AsignarDatosSustentacionInforme(ByVal codigo_tes As Integer, ByVal nota As String, ByVal fecha As String)
        Dim obj As New ClsGestionInvestigacion
        Dim Data As New Dictionary(Of String, Object)()
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()
        Try
            Dim dt As New Data.DataTable
            dt = obj.AsignarDatosSustentacionInforme(codigo_tes, nota, fecha)
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

    Private Sub ActualizarLinkInforme(ByVal codigo_tes As Integer, ByVal link As String)
        Dim obj As New ClsGestionInvestigacion
        Dim Data As New Dictionary(Of String, Object)()
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()
        Try
            Dim dt As New Data.DataTable
            dt = obj.ActualizarLinkInforme(codigo_tes, link)
            Data.Add("rpta", dt.Rows(0).Item("Respuesta"))
            Data.Add("msje", dt.Rows(0).Item("Mensaje").ToString)
            list.Add(Data)

            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)
        Catch ex As Exception
            Data.Add("rpta", "0 - ACTLINKINF")
            Data.Add("msje", ex.Message)
            list.Add(Data)
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)
        End Try
    End Sub

    Private Sub EnviarTesis(ByVal codigo_tes As Integer, ByVal usuario As Integer, ByVal codigo_alu As Integer, ByVal tipo As Integer, ByVal etapa As String)
        Dim obj As New ClsGestionInvestigacion
        Dim Data As New Dictionary(Of String, Object)()
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()
        Try
            Dim dt As New Data.DataTable
            dt = obj.EnviarTesis(codigo_tes, usuario, codigo_alu, tipo, etapa)
            Data.Add("rpta", dt.Rows(0).Item("Respuesta"))
            Data.Add("msje", dt.Rows(0).Item("Mensaje").ToString)
            list.Add(Data)

            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)
        Catch ex As Exception
            Data.Add("rpta", "0 - ENVTES")
            Data.Add("msje", ex.Message)
            list.Add(Data)
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)
        End Try
    End Sub


    Private Sub ActualizarTesis(ByVal codigo_tes As Integer, ByVal codigo_tin As Integer, ByVal titulo As String, ByVal problema As String, ByVal resumen As String, ByVal codigo_lin As Integer, ByVal codigo_dis_ocde As Integer, ByVal fechaini As String, ByVal fechafin As String, ByVal codigo_eti As Integer, ByVal codigo_per As Integer, ByVal presupuesto As String, ByVal avance As String, ByVal financiamiento As String, ByVal financiausat As String, ByVal financiaexterno As String, ByVal fechasustentacionP As String, ByVal notasustentacionP As String)
        Dim obj As New ClsGestionInvestigacion
        Dim Data As New Dictionary(Of String, Object)()
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()
        Try
            Dim dt As New Data.DataTable
            dt = obj.ActualizarTesis(codigo_tes, codigo_tin, titulo, problema, resumen, codigo_lin, codigo_dis_ocde, fechaini, fechafin, codigo_eti, codigo_per, presupuesto, avance, financiamiento, financiausat, financiaexterno, fechasustentacionP, notasustentacionP)
            Data.Add("cod", obj.EncrytedString64(dt.Rows(0).Item("cod")))
            Data.Add("rpta", dt.Rows(0).Item("Respuesta"))
            Data.Add("msje", dt.Rows(0).Item("Mensaje").ToString)
            list.Add(Data)

            'If informe.ContentLength > 0 Then
            '    SubirArchivo(dt.Rows(0).Item("cod"), informe, "INFORME", "P", codigo_per)
            'End If

            'If acta.ContentLength > 0 Then
            '    SubirArchivo(dt.Rows(0).Item("cod"), acta, "ACTA", "P", codigo_per)
            'End If

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

    Private Sub ListaAutorxTesis(ByVal codigo_tes As Integer)
        Dim obj As New ClsGestionInvestigacion
        Dim Data1 As New Dictionary(Of String, Object)()
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()
        Try
            Dim dt As New Data.DataTable
            dt = obj.ListarAutorTesis(codigo_tes)
            If dt.Rows.Count > 0 Then
                For i As Integer = 0 To dt.Rows.Count - 1
                    Dim data As New Dictionary(Of String, Object)()
                    data.Add("cod_rtes", obj.EncrytedString64(dt.Rows(i).Item("codigo_rtes")))
                    data.Add("cod_alu", obj.EncrytedString64(dt.Rows(i).Item("codigo_alu")))
                    data.Add("coduni", dt.Rows(i).Item("codigoUniver_Alu"))
                    data.Add("nom", dt.Rows(i).Item("alumno"))
                    data.Add("codtipo", obj.EncrytedString64(dt.Rows(i).Item("codigo_Tpi")))
                    data.Add("destipo", dt.Rows(i).Item("descripcion_tpi"))
                    list.Add(data)
                Next
            End If
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)
        Catch ex As Exception
            Data1.Add("rpta", "ListaAlumnosxCurso")
            Data1.Add("msje", ex.Message)
            list.Add(Data1)
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)
        End Try
    End Sub

    Private Sub ListaObjetivosxTesis(ByVal codigo_tes As Integer)
        Dim obj As New ClsGestionInvestigacion
        Dim Data1 As New Dictionary(Of String, Object)()
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()
        Try
            Dim dt As New Data.DataTable
            dt = obj.ListarObjetivosTesis(codigo_tes)
            If dt.Rows.Count > 0 Then
                For i As Integer = 0 To dt.Rows.Count - 1
                    Dim data As New Dictionary(Of String, Object)()
                    data.Add("cod_obj", obj.EncrytedString64(dt.Rows(i).Item("codigo_obj")))
                    data.Add("des_obj", dt.Rows(i).Item("descripcion_obj"))
                    data.Add("codtipo_obj", dt.Rows(i).Item("codtipo_obj"))
                    data.Add("tipo_obj", dt.Rows(i).Item("tipo_obj"))
                    list.Add(data)
                Next
            End If
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)
        Catch ex As Exception
            Data1.Add("rpta", "ListaAlumnosxCurso")
            Data1.Add("msje", ex.Message)
            list.Add(Data1)
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)
        End Try
    End Sub

    Private Sub ListarParticipantesxTesis(ByVal codigo_tes As Integer, ByVal tipo_tpi As String, ByVal abreviatura_etapa As String)
        Dim obj As New ClsGestionInvestigacion
        Dim Data1 As New Dictionary(Of String, Object)()
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()
        Try
            Dim dt As New Data.DataTable
            dt = obj.ListarParticipantesTesis(codigo_tes, tipo_tpi, abreviatura_etapa)
            If dt.Rows.Count > 0 Then
                For i As Integer = 0 To dt.Rows.Count - 1
                    Dim data As New Dictionary(Of String, Object)()
                    data.Add("cod_jur", obj.EncrytedString64(dt.Rows(i).Item("codigo_jur")))
                    data.Add("cod_per", obj.EncrytedString64(dt.Rows(i).Item("codigo_per")))
                    data.Add("nom", dt.Rows(i).Item("Docente"))
                    data.Add("codtipo", obj.EncrytedString64(dt.Rows(i).Item("codigo_Tpi")))
                    data.Add("destipo", dt.Rows(i).Item("descripcion_tpi"))
                    data.Add("cod_etapa", dt.Rows(i).Item("codigo_eti"))
                    data.Add("aprueba_dir", dt.Rows(i).Item("apruebadirector"))
                    list.Add(data)
                Next
            End If
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)
        Catch ex As Exception
            Data1.Add("rpta", "Asesor")
            Data1.Add("msje", ex.Message)
            list.Add(Data1)
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)
        End Try
    End Sub

    Private Sub ActualizarAutor(ByVal codigo_tes As Integer, ByVal Autores() As Object, ByVal id As Integer)
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
            For i As Integer = 0 To Autores.Length - 1
                Dim Data As New Dictionary(Of String, Object)()
                Dim opcion As Integer = 0
                If Autores(i).Item("cod_aut") <> "0" Then
                    codigo_aut = obj.DecrytedString64(Autores(i).Item("cod_aut"))
                Else
                    codigo_aut = Autores(i).Item("cod_aut")
                End If
                dt = obj.ActualizarAutorTesis(codigo_aut, codigo_tes, obj.DecrytedString64(Autores(i).Item("cod")), obj.DecrytedString64(Autores(i).Item("codtipo")), Autores(i).Item("estado"), id)
                Data.Add("rpta", dt.Rows(0).Item("Respuesta"))
                Data.Add("msje", dt.Rows(0).Item("Mensaje").ToString)
                list.Add(Data)
            Next

            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)
        Catch ex As Exception
            Data2.Add("rpta", "0 - REGAUTOR")
            Data2.Add("msje", ex.Message)
            list.Add(Data2)
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)
        End Try
    End Sub

    Private Sub ActualizarObjetivos(ByVal codigo_tes As Integer, ByVal objetivos() As Object, ByVal id As Integer)
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
                dt = obj.ActualizarObjetivoTesis(codigo_obj, codigo_tes, objetivos(i).Item("descripcion"), objetivos(i).Item("codtipo"), objetivos(i).Item("estado"), id)
                Data.Add("rpta", dt.Rows(0).Item("Respuesta"))
                Data.Add("msje", dt.Rows(0).Item("Mensaje").ToString)
                list.Add(Data)
            Next

            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)
        Catch ex As Exception
            Data2.Add("rpta", "0 - REGOBJ")
            Data2.Add("msje", ex.Message)
            list.Add(Data2)
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)
        End Try
    End Sub

    Private Sub ActualizarAsesor(ByVal codigo_tes As Integer, ByVal abreviatura_etapa As String, ByVal Asesores() As Object, ByVal id As Integer)
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
            Dim codigo_jur As Integer
            For i As Integer = 0 To Asesores.Length - 1
                Dim Data As New Dictionary(Of String, Object)()
                Dim opcion As Integer = 0
                If Asesores(i).Item("cod_jur") <> "0" Then
                    codigo_jur = obj.DecrytedString64(Asesores(i).Item("cod_jur"))
                Else
                    codigo_jur = Asesores(i).Item("cod_jur")
                End If
                dt = obj.ActualizarAsesorTesis(codigo_jur, codigo_tes, obj.DecrytedString64(Asesores(i).Item("cod")), abreviatura_etapa, obj.DecrytedString64(Asesores(i).Item("codtipo")), Asesores(i).Item("estado"), id)
                Data.Add("rpta", dt.Rows(0).Item("Respuesta"))
                Data.Add("msje", dt.Rows(0).Item("Mensaje").ToString)
                list.Add(Data)
            Next

            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)
        Catch ex As Exception
            Data2.Add("rpta", "0 - REGASESOR")
            Data2.Add("msje", ex.Message)
            list.Add(Data2)
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)
        End Try
    End Sub

    
    Sub SubirArchivo(ByVal codigo As String, ByVal ArchivoSubir As HttpPostedFile, ByVal tipo As String)
        Try
            Dim idtabla As Integer = 23
            Dim post As HttpPostedFile = ArchivoSubir
            Dim cod_operacion As String = 0
            Dim Fecha As String = Date.Now.ToString("dd/MM/yyyy")
            Dim Usuario As String = Session("perlogin").ToString
            Dim Input(post.ContentLength) As Byte
            Dim etapa As String = "P"

            If tipo = "PROYECTO" Then
                cod_operacion = 1
            End If
            If tipo = "ACTA" Then
                cod_operacion = 2
            End If

            If tipo = "SIMILITUDPROYECTO" Then
                cod_operacion = 3
            End If

            If tipo = "PREINFORME" Then
                cod_operacion = 4
                etapa = "E"
            End If

            If tipo = "INFORME" Then
                cod_operacion = 5
                etapa = "I"
            End If

            If tipo = "ACTAINFORME" Then
                cod_operacion = 6
                etapa = "I"
            End If
            If tipo = "SIMILITUDINFORME" Then
                cod_operacion = 7
                etapa = "I"
            End If

            Dim b As New BinaryReader(post.InputStream)
            Dim binData As Byte() = b.ReadBytes(post.InputStream.Length)
            Dim base64 = System.Convert.ToBase64String(binData)

            Dim wsCloud As New ClsArchivosCompartidos
            Dim list As New Dictionary(Of String, String)
            '  Dim list As New List(Of Dictionary(Of String, String))()
            list.Add("Fecha", Fecha)
            list.Add("Extencion", System.IO.Path.GetExtension(post.FileName))
            'list.Add("Nombre", System.IO.Path.GetFileName(post.FileName).Replace("&", "_").Replace("'", "_").Replace("*", "_").Replace("á", "_").Replace("é", "_").Replace("í", "_").Replace("ó", "_").Replace("ú", "_").Replace("Á", "_").Replace("É", "_").Replace("Í", "_").Replace("Ó", "_").Replace("Ú", "_").Replace("Ñ", "_").Replace("ñ", "_").Replace(",", "_"))
            list.Add("Nombre", Regex.Replace(System.IO.Path.GetFileName(post.FileName), "[^0-9A-Za-z._ ]", "").Replace(",", ""))
            list.Add("TransaccionId", codigo)
            list.Add("TablaId", idtabla)
            list.Add("NroOperacion", cod_operacion)
            list.Add("Archivo", System.Convert.ToBase64String(binData, 0, binData.Length))
            list.Add("Usuario", Usuario)
            list.Add("Equipo", "")
            list.Add("Ip", "")
            list.Add("param8", Usuario)

            Dim envelope As String = wsCloud.SoapEnvelope(list)
            Dim result As String = wsCloud.PeticionRequestSoap(ruta, envelope, "http://usat.edu.pe/UploadFile", Usuario)

            ActualizarArchivoTesis(idtabla, codigo, cod_operacion, etapa, Session("id_per"))

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


    Private Sub ActualizarArchivoTesis(ByVal idtabla As Integer, ByVal idtransaccion As String, ByVal idoperacion As String, ByVal abreviatura As String, ByVal codigo_per As Integer)
        Dim obj As New ClsGestionInvestigacion
        Dim Data1 As New Dictionary(Of String, Object)()
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()
        Try
            Dim dt As New Data.DataTable
            dt = obj.ActualizarArchivosTesis(idtabla, idtransaccion, idoperacion, abreviatura, codigo_per)
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



    Private Sub GenerarActa(ByVal codigo_tes As Integer, ByVal codigo_alu As Integer, ByVal etapa As String)
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()
        Dim data As New Dictionary(Of String, Object)()
        Dim obj As New ClsGestionInvestigacion
        Try
            If etapa = "P" Then
                data.Add("ok", True)
                'data.Add("link", rutareporte + "PRIVADOS/TESIS/TES_ActaSustentacionProyecto&a=" + codigo_alu.ToString + "&t=" + codigo_tes.ToString + "&hora=" + Date.Now.ToString("yyyy/MM/dd HH:mm:ss") + "&rs:Format=PDF")
                data.Add("link", "DescargarActa.aspx?a=" + codigo_alu.ToString + "&t=" + codigo_tes.ToString + "&e=P")
            ElseIf etapa = "I" Then
                data.Add("ok", True)
                'data.Add("link", rutareporte + "PRIVADOS/TESIS/TES_ActaSustentacionInforme&a=" + codigo_alu.ToString + "&t=" + codigo_tes.ToString + "&hora=" + Date.Now.ToString("yyyy/MM/dd HH:mm:ss") + "&rs:Format=PDF")
                data.Add("link", "DescargarActa.aspx?a=" + codigo_alu.ToString + "&t=" + codigo_tes.ToString + "&e=I")

            Else
                data.Add("ok", False)
            End If
            list.Add(data)
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)
        Catch ex As Exception
            data.Clear()
            data.Add("ok", False)
            data.Add("msje", ex.Message.ToString)
            list.Add(data)
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)
        End Try
    End Sub


    Private Sub RegistrarAsesor(ByVal codigo_tes As Integer, ByVal abreviatura_etapa As String, ByVal codigo_per As Integer, ByVal id As Integer)
        Dim obj As New ClsGestionInvestigacion
        Dim Data2 As New Dictionary(Of String, Object)()
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()
        Try
            Dim data As New Dictionary(Of String, Object)()
            Dim dt As New Data.DataTable
            dt = obj.RegistrarAsesorTesis(codigo_tes, codigo_per, abreviatura_etapa, id)
            data.Add("rpta", dt.Rows(0).Item("Respuesta"))
            data.Add("msje", dt.Rows(0).Item("Mensaje").ToString)
            list.Add(data)
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)
        Catch ex As Exception
            Data2.Add("rpta", "0 - REGASESOR")
            Data2.Add("msje", ex.Message)
            list.Add(Data2)
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)
        End Try
    End Sub

    Private Sub ListarObservaciones(ByVal codigo_tes As Integer, ByVal codigo_jur As Integer)
        Dim obj As New ClsGestionInvestigacion
        Dim Data1 As New Dictionary(Of String, Object)()
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()
        Try
            Dim dt As New Data.DataTable
            dt = obj.ListarObservacionesJurado(codigo_tes, codigo_jur, 0)
            If dt.Rows.Count > 0 Then
                For i As Integer = 0 To dt.Rows.Count - 1
                    Dim data As New Dictionary(Of String, Object)()
                    data.Add("tipo", dt.Rows(i).Item("tipo_dot"))
                    data.Add("descripcion", dt.Rows(i).Item("descripcion_dot"))
                    data.Add("fecha", dt.Rows(i).Item("fecha_reg").ToString)
                    list.Add(data)
                Next
            End If
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)
        Catch ex As Exception
            Data1.Add("rpta", "observaciones")
            Data1.Add("msje", ex.Message)
            list.Add(Data1)
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)
        End Try
    End Sub


    Private Sub ListarObservacionesDocente(ByVal codigo_tes As Integer, ByVal abreviatura As String)
        Dim obj As New ClsGestionInvestigacion
        Dim Data1 As New Dictionary(Of String, Object)()
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()
        Try
            Dim dt As New Data.DataTable
            dt = obj.ListarObservacionDocente(codigo_tes, abreviatura)
            If dt.Rows.Count > 0 Then
                For i As Integer = 0 To dt.Rows.Count - 1
                    Dim data As New Dictionary(Of String, Object)()
                    data.Add("cod", obj.EncrytedString64(dt.Rows(i).Item("codigo_odt")))
                    data.Add("descripcion", dt.Rows(i).Item("descripcion_odt"))
                    data.Add("resuelto", dt.Rows(i).Item("resuelto"))
                    data.Add("fecha", dt.Rows(i).Item("fecha_reg").ToString)
                    list.Add(data)
                Next
            End If
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)
        Catch ex As Exception
            Data1.Add("rpta", "lst obs docente")
            Data1.Add("msje", ex.Message)
            list.Add(Data1)
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)
        End Try
    End Sub

    Private Sub RegistrarObservacionDocente(ByVal codigo_tes As Integer, ByVal codigo_per As Integer, ByVal codigo_alu As Integer, ByVal descripcion As String, ByVal abreviatura As String)
        Dim obj As New ClsGestionInvestigacion
        Dim Data2 As New Dictionary(Of String, Object)()
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()
        Try
            Dim data As New Dictionary(Of String, Object)()
            Dim dt As New Data.DataTable
            dt = obj.RegistrarObservacionDocente(codigo_tes, codigo_per, codigo_alu, descripcion, abreviatura)
            data.Add("rpta", dt.Rows(0).Item("Respuesta"))
            data.Add("msje", dt.Rows(0).Item("Mensaje").ToString)
            list.Add(data)
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)
        Catch ex As Exception
            Data2.Add("rpta", "0 - REG obs doc")
            Data2.Add("msje", ex.Message)
            list.Add(Data2)
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)
        End Try
    End Sub

    Private Sub EliminarObservacionDocente(ByVal codigo As Integer)
        Dim obj As New ClsGestionInvestigacion
        Dim Data2 As New Dictionary(Of String, Object)()
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()
        Try
            Dim data As New Dictionary(Of String, Object)()
            Dim dt As New Data.DataTable
            dt = obj.EliminarObservacionDocente(codigo)
            data.Add("rpta", dt.Rows(0).Item("Respuesta"))
            data.Add("msje", dt.Rows(0).Item("Mensaje").ToString)
            list.Add(data)
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)
        Catch ex As Exception
            Data2.Add("rpta", "0 - elimin obs doc")
            Data2.Add("msje", ex.Message)
            list.Add(Data2)
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)
        End Try
    End Sub

    Private Sub ListarAreaOCDExLineaUSAT(ByVal codigo_lin As Integer)
        Dim obj As New ClsGestionInvestigacion
        Dim Data1 As New Dictionary(Of String, Object)()
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()
        Try
            Dim dt As New Data.DataTable
            dt = obj.ListarAreasOCDExLineaUSAT(codigo_lin)
            If dt.Rows.Count > 0 Then
                For i As Integer = 0 To dt.Rows.Count - 1
                    Dim data As New Dictionary(Of String, Object)()
                    data.Add("cod", dt.Rows(i).Item("codigo_ocde"))
                    data.Add("des", dt.Rows(i).Item("descripcion_ocde"))
                    list.Add(data)
                Next
            End If
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)
        Catch ex As Exception
            Data1.Add("rpta", "lst obs docente")
            Data1.Add("msje", ex.Message)
            list.Add(Data1)
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)
        End Try
    End Sub

End Class
