Imports System.IO
Imports System.Web.HttpRequest
Imports System.Collections.Generic
Imports System.Data
Partial Class DataJson_crm_Interesado
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        mainForm.Visible = False

        Dim objCRM As New ClsCRM
        Dim Data As New Dictionary(Of String, Object)()
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Try
            Dim k As String = "0" 'Request("k")
            Dim f As String = ""

            Select Case objCRM.DecrytedString64(Request("action"))
                Case "Listar"
                    Dim cod_test As String = objCRM.DecrytedString64(Request("cboTipoEstudio"))
                    Dim cod_con As String = objCRM.DecrytedString64(Request("cboConvocatoria"))

                    Dim codigo_eve As String = ""
                    If Request("cboEvento") <> "" Then
                        Dim aCodigosEve As String() = Request("cboEvento").Split(",")
                        For Each _codigoEve As String In aCodigosEve
                            If Not String.IsNullOrEmpty(codigo_eve) Then
                                codigo_eve &= ","
                            End If
                            codigo_eve &= objCRM.DecrytedString64(_codigoEve).ToString
                        Next
                    End If


                    Dim letra_ini As String = Request("txtletraini")
                    Dim letra_fin As String = Request("txtletrafin")
                    Dim texto As String = Request("txttexto")
                    Dim cod_sin As String = objCRM.DecrytedString64(Request("cboTipoPersona"))

                    Dim comunicacion As String
                    If Request("cboComunicacion") = "-1" Then
                        comunicacion = Request("cboComunicacion")
                    Else
                        comunicacion = objCRM.DecrytedString64(Request("cboComunicacion"))
                    End If

                    Dim Acuerdo As String = Request("cboAcuerdo")

                    Dim codigos_cpf As String = ""
                    If Request("cboCarreraProfesional") <> "" Then
                        Dim aCodigosCpf As String() = Request("cboCarreraProfesional").Split(",")
                        For Each _codigoCpf As String In aCodigosCpf
                            If Not String.IsNullOrEmpty(codigos_cpf) Then
                                codigos_cpf &= ","
                            End If
                            codigos_cpf &= objCRM.DecrytedString64(_codigoCpf).ToString
                        Next
                    End If

                    Dim codigo_cco As String = ""
                    If Request("cboCentroCosto") <> "" Then
                        Dim aCodigosCco As String() = Request("cboCentroCosto").Split(",")
                        For Each _codigoCco As String In aCodigosCco
                            If Not String.IsNullOrEmpty(codigo_cco) Then
                                codigo_cco &= ","
                            End If
                            codigo_cco &= objCRM.DecrytedString64(_codigoCco).ToString
                        Next
                    End If

                    Dim codigo_req As String = ""
                    If Request("cboRequisitoAdmision") <> "" Then
                        Dim aCodigosReq As String() = Request("cboRequisitoAdmision").Split(",")
                        For Each _codigoReq As String In aCodigosReq
                            If Not String.IsNullOrEmpty(codigo_req) Then
                                codigo_req &= ","
                            End If
                            codigo_req &= objCRM.DecrytedString64(_codigoReq).ToString
                        Next
                    End If

                    Dim grados As String = Request("cboGrados")

                    Dim strFechaDesde As String = Request("txtFechaDesde")
                    Dim fechaDesde As Date
                    If String.IsNullOrEmpty(strFechaDesde) Then
                        fechaDesde = Date.Parse("01/01/1900")
                    Else
                        fechaDesde = Date.Parse(strFechaDesde)
                    End If

                    Dim strFechaHasta As String = Request("txtFechaHasta")
                    Dim fechaHasta As Date
                    If String.IsNullOrEmpty(strFechaHasta) Then
                        fechaHasta = Date.Parse("01/01/1900")
                    Else
                        fechaHasta = Date.Parse(strFechaHasta)
                    End If

                    'Dim tokenOri As String = objCRM.DecrytedString64(Request("cboFiltroOrigen"))
                    'Dim codigoOri As Integer = IIf(Not String.IsNullOrEmpty(tokenOri), tokenOri, 0)
                    Dim codigoOri As String = objCRM.DecrytedString64(Request("cboFiltroOrigen"))

                    Dim strFechaAcuerdo As String = Request("txtFechaAcuerdo")
                    Dim fechaAcuerdo As Date
                    If String.IsNullOrEmpty(strFechaAcuerdo) Then
                        fechaAcuerdo = Date.Parse("01/01/1900")
                    Else
                        fechaAcuerdo = Date.Parse(strFechaAcuerdo)
                    End If

                    Dim prioridad As Integer = Request("cboFiltroPrioridad")

                    Dim usuarioAcuerdo As Integer = 0
                    If Not String.IsNullOrEmpty(Request("chkMisAcuerdos")) Then
                        usuarioAcuerdo = Session("id_per")
                    End If

                    Dim codigos_ied As String = ""
                    If Request("cboFiltroColegio") <> "" Then
                        Dim aCodigosIed As String() = Request("cboFiltroColegio").Split(",")
                        For Each _codigoIed As String In aCodigosIed
                            If Not String.IsNullOrEmpty(codigos_ied) Then
                                codigos_ied &= ","
                            End If
                            codigos_ied &= objCRM.DecrytedString64(_codigoIed).ToString
                        Next
                    End If

                    Dim origenInscripcion_alu As String = Request("cboOrigenAdmision")
                    Dim requestId As String = Request("requestId")

                    ListaInteresadosxEvento_V2(requestId, "L", cod_test, cod_con, codigo_eve, letra_ini, letra_fin, texto, cod_sin, comunicacion, Acuerdo, codigos_cpf, codigo_cco, grados, fechaDesde, fechaHasta, codigoOri, fechaAcuerdo, prioridad, usuarioAcuerdo, codigos_ied, origenInscripcion_alu, codigo_req)

                Case "BuscaxTipoyNumDoc"
                    Dim tipo_doc As Integer = objCRM.DecrytedString64(Request("cboTipoDocumento"))
                    Dim num_doc As String = Request("txtnum_doc")
                    Dim opcion As String = "B"
                    Dim codigo As String = 0
                    BuscaInteresado(opcion, codigo, tipo_doc, num_doc)
                Case "BuscaCoincidencia"
                    Dim apepat As String = Request("txtapepat")
                    Dim apemat As String = Request("txtapemat")
                    Dim nombre As String = Request("txtnombre")
                    BuscaCoincidencias(apepat, apemat, nombre)
                Case "SeleccionCoincidencia"
                    Dim codigo As String = objCRM.DecrytedString64(Request("cod"))
                    Dim opcion As String = "BXC"
                    BuscaInteresado(opcion, codigo, "", "")
                Case "BuscaInstitucionEducativa"
                    Dim cod_Reg As String = objCRM.DecrytedString64(Request("cboRegionIE"))
                    'Dim texto As String = Request("txtnombreIE")
                    Dim texto As String = "%"
                    BuscaInstitucionEducativa(cod_Reg, texto)
                Case "ValidaDuplicado"
                    Dim codigo As String = 0
                    If Request("hdcod_i") <> 0 Then
                        codigo = objCRM.DecrytedString64(Request("hdcod_i"))
                    End If
                    Dim tipo_doc As Integer = objCRM.DecrytedString64(Request("cboTipoDocumento"))
                    Dim num_doc As String = Request("txtnum_doc")
                    Dim opcion As String = "D"
                    BuscaDuplicado(opcion, codigo, tipo_doc, num_doc)
                Case "Registrar"
                    Dim codigo As String = 0
                    Dim cod_ie As String = 0
                    Dim cod_cpf As String = 0
                    Dim cod_eve As String = 0
                    If Request("hdcod_i") <> 0 Then
                        codigo = objCRM.DecrytedString64(Request("hdcod_i"))
                    End If
                    If Request("codie") <> "0" Then
                        cod_ie = objCRM.DecrytedString64(Request("codie"))
                    End If
                    If Request("codcpf") <> "0" Then
                        cod_cpf = objCRM.DecrytedString64(Request("codcpf"))
                    End If
                    Dim tipo_doc As Integer = objCRM.DecrytedString64(Request("cboTipoDocumento"))
                    Dim num_doc As String = Request("txtnum_doc")
                    Dim apepat As String = Request("txtapepat")
                    Dim apemat As String = Request("txtapemat")
                    Dim nombre As String = Request("txtnombre")
                    Dim fecnac As String = Request("txtfecnac")
                    Dim grado As String = Request("cboGradoEstudios")
                    Dim cod_per As Integer = Session("id_per")
                    Dim num_doc_confirmado As Boolean = (Request("chkConfirmado") IsNot Nothing)
                    Dim anioEgreso As String = Request("txtAnioEgreso")
                    RegistrarInteresado(codigo, tipo_doc, num_doc, apepat, apemat, nombre, fecnac, cod_ie, cod_cpf, 1, grado, cod_per, num_doc_confirmado, anioEgreso)
                Case "ProcesoMarketing"
                    Dim codigoInt As Integer = objCRM.DecrytedString64(Request("codigoInt"))
                    Dim nombreOri As String = Request("nombreOri")
                    ProcesoMarketing(codigoInt, nombreOri)
                Case "Modificar"
                    Dim codigo As String = 0
                    Dim cod_ie As String = 0
                    Dim cod_cpf As String = 0
                    If Request("hdcod_i") <> "0" Then
                        codigo = objCRM.DecrytedString64(Request("hdcod_i"))
                    End If
                    If Request("codie") <> "0" Then
                        cod_ie = objCRM.DecrytedString64(Request("codie"))
                    End If
                    If Request("codcpf") <> "0" Then
                        cod_cpf = objCRM.DecrytedString64(Request("codcpf"))
                    End If
                    Dim tipo_doc As Integer = objCRM.DecrytedString64(Request("cboTipoDocumento"))
                    Dim num_doc As String = Request("txtnum_doc")
                    Dim apepat As String = Request("txtapepat")
                    Dim apemat As String = Request("txtapemat")
                    Dim nombre As String = Request("txtnombre")
                    Dim fecnac As String = Request("txtfecnac")
                    Dim grado As String = Request("cboGradoEstudios")
                    Dim cod_per As Integer = Session("id_per")
                    Dim num_doc_confirmado As Boolean = (Request("chkConfirmado") IsNot Nothing)
                    Dim anioEgreso As String = Request("txtAnioEgreso")

                    RegistrarInteresado(codigo, tipo_doc, num_doc, apepat, apemat, nombre, fecnac, cod_ie, cod_cpf, 1, grado, cod_per, num_doc_confirmado, anioEgreso)

                    Dim cod_eve As String = objCRM.DecrytedString64(Request("codeve"))
                    Dim cod_ori As String = objCRM.DecrytedString64(Request("cboOrigen"))
                    InscribirInteresado(codigo, cod_eve, cod_ori, cod_per, False)

                Case "InscribirInteresado"
                    Dim codigo As String = objCRM.DecrytedString64(Request("hdcod_i"))
                    Dim cod_eve As String = objCRM.DecrytedString64(Request("codeve"))
                    Dim cod_ori As String = objCRM.DecrytedString64(Request("cboOrigen"))
                    Dim cod_per As Integer = Session("id_per")
                    InscribirInteresado(codigo, cod_eve, cod_ori, cod_per)
                Case "PerfilInteresado"
                    Dim codigo As String = Request("cod_int")
                    Dim filtros As String = "?" + Request("cboTipoEstudio")
                    filtros = filtros + "|" + Request("cboConvocatoria")
                    filtros = filtros + "|" + Request("cboEvento")
                    filtros = filtros + "|" + Request("txttexto")
                    filtros = filtros + "|" + Request("cboFiltroOrigen")
                    filtros = filtros + "|" + Request("cboGrados")

                    filtros = filtros + "|" + Request("cboTipoPersona")
                    filtros = filtros + "|" + Request("cboComunicacion")
                    filtros = filtros + "|" + Request("cboAcuerdo")
                    filtros = filtros + "|" + Request("txtFechaAcuerdo")
                    filtros = filtros + "|" + Request("chkMisAcuerdos")
                    filtros = filtros + "|" + Request("cboCarreraProfesional")
                    filtros = filtros + "|" + Request("cboFiltroPrioridad")
                    filtros = filtros + "|" + Request("cboCentroCosto")
                    filtros = filtros + "|" + Request("txtFechaDesde")
                    filtros = filtros + "|" + Request("txtFechaHasta")
                    filtros = filtros + "|" + Request("txtletraini")
                    filtros = filtros + "|" + Request("txtletrafin")
                    filtros = filtros + "|" + Request("cboFiltroColegio")
                    filtros = filtros + "|" + Request("cboRequisitoAdmision")

                    PerfilInteresado(codigo, filtros)
                Case "CargaFiltros"
                Case "ExportarInteresados"
                    Dim requestId As String = IIf(Request("requestId") IsNot Nothing, Request("requestId"), "")
                    ExportarInteresados(requestId)
                Case "Eliminar"
                    Dim codigoInt As Integer = objCRM.DecrytedString64(Request("codigoInt"))
                    EliminarInteresado(codigoInt)
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

    Private Sub ListaInteresadosxEvento(ByVal tipo As String, ByVal codigo_test As String, ByVal codigo_con As String, ByVal codigo_eve As String, ByVal letra_ini As String, ByVal letra_fin As String, ByVal texto As String, ByVal cod_sin As String, ByVal comunicacion As String, ByVal Acuerdo As String, ByVal codigo_cpf As String, ByVal codigo_cco As Integer, ByVal grados As String, ByVal fechaDesde As String, ByVal fechaHasta As String, ByVal codigoOri As String, ByVal fechaAcuerdo As String, ByVal prioridad As Integer, ByVal usuarioAcuerdo As Integer, ByVal codigos_ied As String)
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim result As New Dictionary(Of String, Object)
        Dim list As New List(Of Dictionary(Of String, Object))()

        Session("crm_FiltroCodigoTest") = codigo_test
        Session("crm_FiltroCodigoCon") = codigo_con
        Session("crm_FiltroCodigoEve") = codigo_eve

        Try
            If Session("iteracionBloque") Is Nothing Then
                Session("iteracionBloque") = 1
            Else
                Session("iteracionBloque") = Session("iteracionBloque") + 1
            End If
            Dim iteracionBloque As Integer = Session("iteracionBloque")

            Dim obj As New ClsCRM
            Dim tb As New Data.DataTable
            If iteracionBloque = 1 Then 'Solo listo en la primera llamada
                Session("dataInteresados") = obj.ListaInteresadosxEvento(tipo, codigo_test, codigo_con, codigo_eve, letra_ini, letra_fin, texto, cod_sin, comunicacion, Acuerdo, codigo_cpf, codigo_cco, grados, fechaDesde, fechaHasta, codigoOri, fechaAcuerdo, prioridad, usuarioAcuerdo, codigos_ied)
            End If
            tb = Session("dataInteresados")

            Dim filas As Integer = tb.Rows.Count
            result.Item("filas") = filas

            Dim filasPorBloque As Integer = 5000 'MÁXIMO DE FILAS A DEVOLVER POR ITERACIÓN
            Dim inicioBloque As Integer = (iteracionBloque - 1) * filasPorBloque
            Dim finBloque As Integer = Math.Min(iteracionBloque * filasPorBloque, filas) - 1

            If filasPorBloque > (filas - inicioBloque) Then
                Session.Remove("iteracionBloque")
                Session.Remove("dataInteresados")
                result.Item("continuar") = False
            Else
                result.Item("continuar") = True
            End If

            If filas > 0 Then
                For i As Integer = inicioBloque To finBloque
                    Dim data As New Dictionary(Of String, Object)()
                    'If i = 0 Then data.Add("sw", True)
                    data.Add("cod", obj.EncrytedString64(tb.Rows(i).Item("codigo_int")))
                    data.Add("codeve", obj.EncrytedString64(tb.Rows(i).Item("codigo_eve")))
                    'data.Add("eve", tb.Rows(i).Item("nombre_eve"))
                    data.Add("ctdoc", obj.EncrytedString64(tb.Rows(i).Item("codigo_doci")))
                    data.Add("tdoc", tb.Rows(i).Item("siglas_doci"))
                    data.Add("ndoc", tb.Rows(i).Item("numerodoc_int"))
                    data.Add("apepat", tb.Rows(i).Item("apepaterno_int"))
                    data.Add("apemat", tb.Rows(i).Item("apematerno_int"))
                    data.Add("nom", tb.Rows(i).Item("nombres_int"))
                    data.Add("situ", tb.Rows(i).Item("situacion"))
                    data.Add("porcen", tb.Rows(i).Item("porcentaje"))
                    data.Add("fecreg", DirectCast(tb.Rows(i).Item("fecha_reg"), Date).ToString("dd/MM/yyyy"))
                    data.Add("ecom", tb.Rows(i).Item("descripcion_ecom"))
                    data.Add("carr", tb.Rows(i).Item("nombre_Cpf"))
                    data.Add("acrd", tb.Rows(i).Item("detalle_acu"))
                    list.Add(data)
                Next
            End If

            result.Item("result") = list
            JSONresult = serializer.Serialize(result)
            'Response.AddHeader("Content-Encoding", "gzip")
            Response.Write(JSONresult)
        Catch ex As Exception
            Dim data As New Dictionary(Of String, Object)()
            data.Add("msje", ex.Message)
            data.Add("rpta", "0 - Servidor")
            list.Add(data)
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)
        End Try
    End Sub

    Private Sub ListaInteresadosxEvento_V2(ByVal rqId As String, ByVal tipo As String, ByVal codigo_test As String, ByVal codigo_con As String, ByVal codigo_eve As String, _
                                           ByVal letra_ini As String, ByVal letra_fin As String, ByVal texto As String, ByVal cod_sin As String, _
                                           ByVal comunicacion As String, ByVal Acuerdo As String, ByVal codigo_cpf As String, ByVal codigo_cco As String, _
                                           ByVal grados As String, ByVal fechaDesde As String, ByVal fechaHasta As String, ByVal codigoOri As String, _
                                           ByVal fechaAcuerdo As String, ByVal prioridad As Integer, ByVal usuarioAcuerdo As Integer, ByVal codigos_ied As String, ByVal origenInscripcion_alu As String, ByVal codigo_req As String)
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim result As New Dictionary(Of String, Object)
        Dim list As New List(Of Dictionary(Of String, Object))()

        Session("crm_FiltroCodigoTest") = codigo_test
        Session("crm_FiltroCodigoCon") = codigo_con
        Session("crm_FiltroCodigoEve") = codigo_eve

        Dim keyIteracionBloque As String = ""
        Dim keyDataInteresados As String = ""

        Try
            If String.IsNullOrEmpty(rqId) Then
                rqId = (New Random()).Next().ToString()
                Session("requestId") = rqId
            End If

            keyIteracionBloque = "iteracionBloque" + rqId
            keyDataInteresados = "dataInteresados" + rqId

            If Session(keyIteracionBloque) Is Nothing Then
                Session(keyIteracionBloque) = 1
            Else
                Session(keyIteracionBloque) = Session(keyIteracionBloque) + 1
            End If
            Dim iteracionBloque As Integer = Session(keyIteracionBloque)

            Dim obj As New ClsCRM
            Dim tb As New Data.DataTable

            If iteracionBloque = 1 Then 'Solo listo en la primera llamada
                Session(keyDataInteresados) = obj.ListaInteresadosxEvento_V2(tipo, codigo_test, codigo_con, codigo_eve, letra_ini, letra_fin, texto, cod_sin, _
                                                                             comunicacion, Acuerdo, codigo_cpf, codigo_cco, grados, fechaDesde, fechaHasta, codigoOri, _
                                                                             fechaAcuerdo, prioridad, usuarioAcuerdo, codigos_ied, origenInscripcion_alu, codigo_req)
            End If
            tb = Session(keyDataInteresados)

            Dim filas As Integer = tb.Rows.Count
            result.Item("filas") = filas

            Dim filasPorBloque As Integer = 5000 'MÁXIMO DE FILAS A DEVOLVER POR ITERACIÓN
            result.Item("iteracionBloque") = iteracionBloque
            Dim inicioBloque As Integer = (iteracionBloque - 1) * filasPorBloque
            Dim finBloque As Integer = Math.Min(iteracionBloque * filasPorBloque, filas) - 1

            If filasPorBloque > (filas - inicioBloque) Then
                Session.Remove(keyIteracionBloque)
                'Session.Remove(keyDataInteresados)
                result.Item("continuar") = False
            Else
                result.Item("continuar") = True
            End If

            If filas > 0 Then
                For i As Integer = inicioBloque To finBloque
                    Dim data As New Dictionary(Of String, Object)()
                    'If i = 0 Then data.Add("sw", True)
                    data.Add("cod", obj.EncrytedString64(tb.Rows(i).Item("codigo_int")))
                    data.Add("codeve", obj.EncrytedString64(tb.Rows(i).Item("codigo_eve")))
                    'data.Add("eve", tb.Rows(i).Item("nombre_eve"))
                    data.Add("ctdoc", obj.EncrytedString64(tb.Rows(i).Item("codigo_doci")))
                    data.Add("tdoc", tb.Rows(i).Item("siglas_doci"))
                    data.Add("ndoc", tb.Rows(i).Item("numerodoc_int"))
                    data.Add("apepat", tb.Rows(i).Item("apepaterno_int"))
                    data.Add("apemat", tb.Rows(i).Item("apematerno_int"))
                    data.Add("nom", tb.Rows(i).Item("nombres_int"))
                    data.Add("situ", tb.Rows(i).Item("situacion"))
                    data.Add("porcen", tb.Rows(i).Item("porcentaje"))
                    data.Add("fecreg", DirectCast(tb.Rows(i).Item("fecha_reg"), Date).ToString("dd/MM/yyyy"))
                    data.Add("ecom", tb.Rows(i).Item("descripcion_ecom"))
                    data.Add("feccom", tb.Rows(i).Item("fecha_com"))
                    data.Add("carr", tb.Rows(i).Item("nombre_Cpf"))
                    data.Add("acrd", tb.Rows(i).Item("detalle_acu"))
                    data.Add("reqf", tb.Rows(i).Item("requisitos_faltantes"))
                    list.Add(data)
                Next
            End If

            result.Item("result") = list
            result.Item("requestId") = rqId
            JSONresult = serializer.Serialize(result)
            'Response.AddHeader("Content-Encoding", "gzip")
            Response.Write(JSONresult)
        Catch ex As Exception
            Session.Remove(keyIteracionBloque)

            Dim data As New Dictionary(Of String, Object)()
            data.Add("msje", ex.Message)
            data.Add("rpta", "0 - Servidor")
            list.Add(data)
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)
        End Try
    End Sub

    Private Sub BuscaInteresado(ByVal opcion As String, ByVal codigo As String, ByVal tipo As String, ByVal num_doc As String)
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()
        Try
            Dim obj As New ClsCRM
            Dim tb As New Data.DataTable
            tb = obj.BuscaxTipoyNumDoc(opcion, codigo, tipo, num_doc, "", "", "")

            If tb.Rows.Count > 0 Then
                For i As Integer = 0 To tb.Rows.Count - 1
                    Dim data As New Dictionary(Of String, Object)()
                    'If i = 0 Then data.Add("sw", True)
                    data.Add("cod", obj.EncrytedString64(tb.Rows(i).Item("codigo_int")))
                    data.Add("cTdi", obj.EncrytedString64(tb.Rows(i).Item("codigo_doci")))
                    data.Add("cNdi", tb.Rows(i).Item("numerodoc_int"))
                    data.Add("cNdiConf", tb.Rows(i).Item("numerodoc_confirmado_int"))
                    data.Add("cApi", tb.Rows(i).Item("apepaterno_int"))
                    data.Add("cAmi", tb.Rows(i).Item("apematerno_int"))
                    data.Add("cNi", tb.Rows(i).Item("nombres_int"))
                    data.Add("cFn", tb.Rows(i).Item("fecha_nac"))
                    data.Add("cied", obj.EncrytedString64(tb.Rows(i).Item("codigo_ied")))
                    data.Add("ied", tb.Rows(i).Item("Nombre_ied"))
                    data.Add("dir", tb.Rows(i).Item("direccion"))
                    data.Add("tel", tb.Rows(i).Item("telefono"))
                    data.Add("tlv", tb.Rows(i).Item("numeroVigente"))
                    data.Add("ema", tb.Rows(i).Item("email"))
                    data.Add("ccp", obj.EncrytedString64(tb.Rows(i).Item("codigo_cpf")))
                    data.Add("cp", tb.Rows(i).Item("nombre_Cpf"))
                    data.Add("grado", tb.Rows(i).Item("grado_int"))

                    data.Item("cOri") = ""
                    If Not IsDBNull(tb.Rows(i).Item("codigo_ori")) Then
                        data.Item("cOri") = obj.EncrytedString64(tb.Rows(i).Item("codigo_ori"))
                    End If

                    data.Add("anioEgre", tb.Rows(i).Item("anioegreso_int"))
                    list.Add(data)
                Next
            End If
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)
        Catch ex As Exception
            Dim data As New Dictionary(Of String, Object)()
            data.Add("msje", ex.Message)
            data.Add("rpta", "0 - Servidor")
            list.Add(data)
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)
        End Try
    End Sub

    Private Sub BuscaCoincidencias(ByVal apepat As String, ByVal apemat As String, ByVal nombre As String)
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()
        Try
            Dim obj As New ClsCRM
            Dim tb As New Data.DataTable
            tb = obj.BuscaxTipoyNumDoc("C", 0, "", "", apepat, apemat, nombre)

            If tb.Rows.Count > 0 Then
                For i As Integer = 0 To tb.Rows.Count - 1
                    Dim data As New Dictionary(Of String, Object)()
                    'If i = 0 Then data.Add("sw", True)
                    data.Add("cod", obj.EncrytedString64(tb.Rows(i).Item("codigo_int")))
                    data.Add("tdoc", tb.Rows(i).Item("siglas_doci"))
                    data.Add("ndoc", tb.Rows(i).Item("numerodoc_int"))
                    data.Add("apepat", tb.Rows(i).Item("apepaterno_int"))
                    data.Add("apemat", tb.Rows(i).Item("apematerno_int"))
                    data.Add("nom", tb.Rows(i).Item("nombres_int"))
                    list.Add(data)
                Next
            End If
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)
        Catch ex As Exception
            Dim data As New Dictionary(Of String, Object)()
            data.Add("msje", ex.Message)
            data.Add("rpta", "0 - Servidor")
            list.Add(data)
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)
        End Try
    End Sub

    Private Sub BuscaDuplicado(ByVal opcion As String, ByVal codigo As String, ByVal tipo As String, ByVal num_doc As String)
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()
        Try
            Dim obj As New ClsCRM
            Dim tb As New Data.DataTable
            tb = obj.BuscaxTipoyNumDoc(opcion, codigo, tipo, num_doc, "", "", "")

            Dim data As New Dictionary(Of String, Object)()
            data.Add("cont", tb.Rows(0).Item("contador"))
            list.Add(data)
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)
        Catch ex As Exception
            Dim data As New Dictionary(Of String, Object)()
            data.Add("msje", ex.Message)
            data.Add("rpta", "0 - Servidor")
            list.Add(data)
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)
        End Try
    End Sub

    Private Sub BuscaInstitucionEducativa(ByVal codigo_region As String, ByVal texto As String)
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()
        Try
            Dim obj As New ClsCRM
            Dim tb As New Data.DataTable
            tb = obj.BuscaInstitucionEducativa(codigo_region, "", "", texto)

            If tb.Rows.Count > 0 Then
                For i As Integer = 0 To tb.Rows.Count - 1
                    Dim data As New Dictionary(Of String, Object)()
                    'If i = 0 Then data.Add("sw", True)
                    data.Add("cod", obj.EncrytedString64(tb.Rows(i).Item("codigo_ied")))
                    data.Add("nom", tb.Rows(i).Item("Nombre_ied"))
                    data.Add("dir", tb.Rows(i).Item("Direccion_ied"))
                    data.Add("reg", tb.Rows(i).Item("Departamento"))
                    data.Add("prov", tb.Rows(i).Item("Provincia"))
                    data.Add("dis", tb.Rows(i).Item("Distrito"))
                    list.Add(data)
                Next
            End If
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)
        Catch ex As Exception
            Dim data As New Dictionary(Of String, Object)()
            data.Add("msje", ex.Message)
            data.Add("rpta", "0 - Servidor")
            list.Add(data)
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)
        End Try
    End Sub

    Private Sub RegistrarInteresado(ByVal codigo As String, ByVal tipo As String, ByVal num_doc As String, ByVal apepat As String, ByVal apemat As String, _
                                    ByVal nombre As String, ByVal fecnac As String, ByVal cod_ie As Integer, ByVal cod_cpf As Integer, ByVal estado_int As Integer, _
                                    ByVal grado As String, ByVal user_reg As Integer, ByVal num_doc_confirmado As Boolean, ByVal anioEgreso As String)
        Dim obj As New ClsCRM
        Dim Data As New Dictionary(Of String, Object)()
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()
        Try
            'For i As Integer = 0 To arr.Count - 1
            '    Data.Add(i, arr(i))
            'Next
            'list.Add(Data)
            Dim dt As New Data.DataTable
            dt = obj.ActualizarInteresado(codigo, tipo, num_doc, apepat, apemat, nombre, fecnac, cod_ie, cod_cpf, estado_int, grado, user_reg, num_doc_confirmado, anioEgreso)
            Data.Add("rpta", dt.Rows(0).Item("Respuesta"))
            Data.Add("msje", dt.Rows(0).Item("Mensaje").ToString)
            Data.Add("cod", obj.EncrytedString64(dt.Rows(0).Item("cod")))
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

    Private Sub InscribirInteresado(ByVal codigo_int As Integer, ByVal codigo_eve As Integer, ByVal codigo_ori As Integer, ByVal user_reg As Integer, Optional ByVal generar_respuesta As Boolean = True)
        Dim obj As New ClsCRM
        Dim Data As New Dictionary(Of String, Object)()
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()
        Try
            'For i As Integer = 0 To arr.Count - 1
            '    Data.Add(i, arr(i))
            'Next
            'list.Add(Data)
            Dim dt As New Data.DataTable
            dt = obj.InscribirInteresado(codigo_int, codigo_eve, codigo_ori, user_reg)
            Data.Item("rpta") = dt.Rows(0).Item("Respuesta")
            Data.Item("msje") = dt.Rows(0).Item("Mensaje").ToString
            list.Add(Data)
            
        Catch ex As Exception
            Data.Item("rpta") = "0 - REG"
            Data.Item("msje") = ex.Message
            list.Add(Data)
        End Try
        If generar_respuesta Then
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)
        End If
    End Sub

    Private Sub ProcesoMarketing(ByVal codigo_int As Integer, ByVal nombre_ori As String)
        Dim obj As New ClsCRM
        Dim objadmision As New ClsAdmision
        Dim Data As New Dictionary(Of String, Object)()
        Dim list As New List(Of Dictionary(Of String, Object))()
        Dim JSONresult As String = ""
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()

        Try
            'Obtengo los datos necesarios para enviar a marketing
            Dim dtDatos As New Data.DataTable
            dtDatos = obj.ListaInteresados("BXC", codigo_int, "", "", "", "", "")

            If dtDatos.Rows.Count > 0 Then
                Dim rowDatos As Data.DataRow = dtDatos.Rows(0)

                Dim distritoIed As String = ""
                Dim provinciaIed As String = ""
                Dim departamentoIed As String = ""

                Dim codigoIed As Integer = rowDatos.Item("codigo_ied")
                If codigoIed <> 0 Then
                    'Obtengo datos de la institución educativa

                    Dim dtDatosIed As New Data.DataTable
                    dtDatosIed = objadmision.ConsultarInstitucionEducativa("GEN", codigoIed)
                    If dtDatosIed.Rows.Count > 0 Then
                        distritoIed = dtDatosIed.Rows(0).Item("nombre_dis")
                        provinciaIed = dtDatosIed.Rows(0).Item("nombre_pro")
                        departamentoIed = dtDatosIed.Rows(0).Item("nombre_dep")
                    End If
                End If

                Dim sexo As String = ""
                If rowDatos.Item("sexo") = "M" Then
                    sexo = "MASCULINO"
                End If
                If rowDatos.Item("sexo") = "F" Then
                    sexo = "FEMENINO"
                End If

                Dim respuestaEnvio As String = objadmision.EnviarParametrosMarketing( _
                        "", _
                        "", _
                        "", _
                        "", _
                        "", _
                        "", _
                        rowDatos.Item("numerodoc_int").ToString, _
                        rowDatos.Item("apepaterno_int").ToString, _
                        rowDatos.Item("apematerno_int").ToString, _
                        rowDatos.Item("nombres_int").ToString, _
                        rowDatos.Item("celular").ToString, _
                        rowDatos.Item("telefono").ToString, _
                        rowDatos.Item("email").ToString, _
                        rowDatos.Item("direccion").ToString, _
                        rowDatos.Item("nombre_Dep").ToString, _
                        rowDatos.Item("nombre_Pro").ToString, _
                        rowDatos.Item("nombre_Dis").ToString, _
                        rowDatos.Item("fecha_nac").ToString, _
                        sexo, _
                        rowDatos.Item("Grado_int").ToString, _
                        "", _
                        "", _
                        "", _
                        departamentoIed, _
                        provinciaIed, _
                        distritoIed, _
                        rowDatos.Item("Nombre_ied").ToString, _
                        rowDatos.Item("nombre_Cpf").ToString, _
                        "", _
                        "INTERESADO", _
                        "", _
                        "", _
                        "INTERESADO", _
                        "PRESENCIAL", _
                        nombre_ori, _
                        "" _
                    )
                Data.Item("rpta") = "1"
                Data.Item("msg") = "Proceso culminado correctamente"
                Data.Item("rpta_envio") = respuestaEnvio
                list.Add(Data)
            End If
        Catch ex As Exception
            Data.Item("rpta") = "0"
            Data.Item("msje") = ex.Message
            list.Add(Data)
        End Try
        JSONresult = serializer.Serialize(list)
        Response.Write(JSONresult)
    End Sub

    Private Sub PerfilInteresado(ByVal codigo_interesado As String, ByVal filtros As String)
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()
        Dim data As New Dictionary(Of String, Object)()
        Session("crm_CodigoInteresado") = codigo_interesado
        Session("crm_FiltrosListaInteresado") = filtros
        If Session("crm_CodigoInteresado") <> "0" Then
            data.Add("msje", True)
            data.Add("link", "FrmListaInformacionInteresado.aspx")
        Else
            data.Add("msje", False)
            data.Add("link", "")
        End If
        list.Add(data)
        JSONresult = serializer.Serialize(list)
        Response.Write(JSONresult)
    End Sub

    Private Sub ExportarInteresados(ByVal requestId As String)
        Try
            If Request.Cookies("fileDownload") Is Nothing Then
                Dim aCookie As New HttpCookie("fileDownload")
                aCookie.Value = "true"
                aCookie.Path = "/"
                Response.Cookies.Add(aCookie)
            End If

            Dim keyDataInteresados As String = "dataInteresados" + requestId

            If Session(keyDataInteresados) Is Nothing Then
                Throw New Exception("No existe la variable de sesión " + keyDataInteresados)
            End If

            Dim dtInteresados As Data.DataTable = Session(keyDataInteresados)
            Dim dtCopy As New Data.DataTable
            dtCopy.Columns.Add("codigo_int")
            dtCopy.Columns.Add("codigo_Alu")
            dtCopy.Columns.Add("codigo_doci")
            dtCopy.Columns.Add("siglas_doci")
            dtCopy.Columns.Add("numerodoc_int")
            dtCopy.Columns.Add("apepaterno_int")
            dtCopy.Columns.Add("apematerno_int")
            dtCopy.Columns.Add("nombres_int")
            dtCopy.Columns.Add("situacion")
            dtCopy.Columns.Add("descripcion_ecom")
            dtCopy.Columns.Add("fecha_com")
            dtCopy.Columns.Add("porcentaje")
            dtCopy.Columns.Add("fecha_reg")
            dtCopy.Columns.Add("nombre_Cpf")
            dtCopy.Columns.Add("detalle_acu")
            dtCopy.Columns.Add("noInteresado")
            dtCopy.Columns.Add("codigo_eve")
            dtCopy.Columns.Add("requisitos_faltantes")

            For Each _row As Data.DataRow In dtInteresados.Rows
                dtCopy.Rows.Add(_row.ItemArray)
            Next

            If dtInteresados IsNot Nothing AndAlso dtInteresados.Rows.Count > 0 Then
                grwInteresados.DataSource = dtCopy
                grwInteresados.DataBind()

                grwInteresados.GridLines = GridLines.Both

                Dim sb As StringBuilder = New StringBuilder()
                Dim SW As System.IO.StringWriter = New System.IO.StringWriter(sb)
                Dim htw As HtmlTextWriter = New HtmlTextWriter(SW)
                Dim Page As Page = New Page()
                Dim form As HtmlForm = New HtmlForm()

                Page.EnableEventValidation = False
                Page.DesignerInitialize()
                Page.Controls.Add(form)
                form.Controls.Add(grwInteresados)
                Page.RenderControl(htw)

                Response.Clear()
                Response.AddHeader("content-disposition", "attachment; filename=Interesados.xls")
                Response.ContentType = "text/xml"
                Response.Write(sb.ToString())
                'Response.End()
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try

    End Sub

    Private Sub EliminarInteresado(ByVal codigoInt As Integer)
        Dim obj As New ClsCRM
        Dim data As New Dictionary(Of String, Object)()
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Try
            Dim dt As New Object()
            dt = obj.EliminarInteresadoV2(codigoInt)
            data.Item("rpta") = dt(0).ToString
            data.Item("msje") = dt(1).ToString
            JSONresult = serializer.Serialize(data)
            Response.Write(JSONresult)
        Catch ex As Exception
            data.Item("rpta") = "0 - DEL"
            data.Item("msje") = ex.Message
            JSONresult = serializer.Serialize(data)
            Response.Write(JSONresult)
        End Try
    End Sub

    'Private Sub CargaFiltros()
    '    Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
    '    Dim JSONresult As String = ""
    '    Dim list As New List(Of Dictionary(Of String, Object))()
    '    Dim data As New Dictionary(Of String, Object)()
    '    If Session("crm_FiltrosListaInteresado") <> "" Then

    '    Else
    '        data.Add("msje", False)
    '        data.Add("link", "")
    '    End If
    '    list.Add(data)
    '    JSONresult = serializer.Serialize(list)
    '    Response.Write(JSONresult)
    'End Sub
    'Private Sub RegistrarConvocatoria(ByVal cod As Integer, ByVal codigo_test As Integer, ByVal codigo_cac As Integer, ByVal nombre As String, ByVal detalle As String, ByVal fecini As String, ByVal fecfin As String, ByVal estado As Integer, ByVal user_reg As Integer)
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
    '        Data.Add("rpta", "0 - REG")
    '        Data.Add("msje", ex.Message)
    '        list.Add(Data)
    '        JSONresult = serializer.Serialize(list)
    '        Response.Write(JSONresult)
    '    End Try
    'End Sub

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

#Region "Eventos"
    Protected Sub grwInteresados_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grwInteresados.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim _cellsRow As TableCellCollection = e.Row.Cells
            Dim ln_Index As Integer = e.Row.RowIndex + 1
            Dim ln_Columnas As Integer = grwInteresados.Columns.Count

            _cellsRow(0).Text = ln_Index
            _cellsRow(7).Text = String.Format("{0} %", Math.Round(Decimal.Parse(_cellsRow(7).Text), 2) * 100)
            grwInteresados.HeaderRow.TableSection = TableRowSection.TableHeader
        End If

    End Sub
#End Region
End Class
