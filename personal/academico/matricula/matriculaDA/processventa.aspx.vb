Imports System.Web.Security
Imports System.Data
Imports cLogica
Imports System.Data.DataTable
Imports System.Collections.Generic
Imports System.Data.DataRow
Imports System.Data.DataColumn
Imports cEntidad
Imports System.IO
Imports System.Web.HttpRequest
Imports ws = wdsFacElect
Imports System.Diagnostics
Imports System.Xml
Imports System.Xml.Serialization
Partial Class processventa
    Inherits System.Web.UI.Page
    'Dim _server As Boolean = False

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim JSONresult As String = ""
        If Me.Page.User.Identity.IsAuthenticated Then
            ' Response.Write("1")
            If Request("param0") = "lstEvent" Then

                Dim i As Integer = 0
                Dim dt As New DataTable
                Dim oe As New eServicioCentroCosto
                Dim ol As New lServicioCentroCosto
                oe.tipooperacion = "1"
                oe.codigo_Alu = Session("codigo_Alu")
                oe.fechaInicio_Sco = Date.Today
                oe.vigencia_Sco = 1
                dt = ol.ConsultarEventos(oe)
                'Dim dict As New Dictionary(Of String, Object)()
                Dim list As New List(Of Dictionary(Of String, Object))()
                Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
                'list = dtCursos
                If Session("codigo_Alu") = 43791 Then


                    For i = 0 To dt.Rows.Count - 1
                        Dim dict As New Dictionary(Of String, Object)()
                        dict.Add("codscc", dt.Rows(i).Item("codigo_Scc"))
                        dict.Add("codcco", dt.Rows(i).Item("codigo_Cco"))
                        dict.Add("etiqueta", dt.Rows(i).Item("etiqueta_scc").ToString)
                        dict.Add("precio", dt.Rows(i).Item("precio_Sco"))
                        dict.Add("fecini", dt.Rows(i).Item("fechaInicio_Sco").ToString)
                        dict.Add("fecfin", dt.Rows(i).Item("fechaVencimiento_Sco").ToString)
                        dict.Add("cuotas", dt.Rows(i).Item("nroPartes_Sco"))
                        If CInt(dt.Rows(i).Item("nroPartes_Sco")) > 1 Then
                            dict.Add("lblcuotas", "cuotas")
                        Else
                            dict.Add("lblcuotas", "cuota")
                        End If
                        If dt.Rows(i).Item("codigo_Cco") = 5029 Then
                            dict.Add("img", "turismo.jpg")
                        ElseIf dt.Rows(i).Item("codigo_Cco") = 5030 Then
                            dict.Add("img", "marketing.jpg")
                        ElseIf dt.Rows(i).Item("codigo_Cco") = 5031 Then
                            dict.Add("img", "socialmedia.jpg")
                        ElseIf dt.Rows(i).Item("codigo_Cco") = 5032 Then
                            dict.Add("img", "empresarios.jpg")
                        End If
                        list.Add(dict)
                    Next
                End If

                JSONresult = serializer.Serialize(list)

            ElseIf Request("param0") = "regEvent" Then

                Dim i As Integer = 0
                Dim dt As New DataTable
                Dim oe As New eDeuda
                Dim ol As New lDeuda
                oe.alumno.codigo_Alu = CInt(Session("codigo_Alu"))
                oe.codigo_cac = CInt(Session("Codigo_Cac"))
                oe.codigo_scc = CInt(Request("param1"))
                oe.cantidad = CInt(Request("param2"))
                dt = ol.RegistrarDeudaEvento(oe)
                'Dim dict As New Dictionary(Of String, Object)()
                Dim list As New List(Of Dictionary(Of String, Object))()
                Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
                'list = dtCursos


                If dt.Rows.Count > 0 Then
                    Dim dict As New Dictionary(Of String, Object)()
                    ' If dt.Rows(0).Item(0).ToString().Contains("0,Procesando las deudas") Then
                    dict.Add("r", True)
                    dict.Add("alert", "success")
                    dict.Add("msje", "Se registro con exito, revisa tu estado de cuenta")
                    'Else
                    '  dict.Add("r", False)
                    ' End If
                    list.Add(dict)
                Else
                    Dim dict As New Dictionary(Of String, Object)()
                    dict.Add("r", False)
                    dict.Add("alert", "error")
                    dict.Add("msje", "Surgieron problemas en la inscripci&oacute;n")
                    list.Add(dict)
                End If
                JSONresult = serializer.Serialize(list)

            ElseIf Request("param0") = "lstPedOdon" Then

                Dim i As Integer = 0
                Dim dt As New DataTable
                Dim oe As New ePedidoOdontologico
                Dim ol As New lPedidoOdontologico
                oe.codigo_alu = Session("codigo_Alu")
                oe.codigo_pod = CInt(Request("param1"))
                dt = ol.ConsultarEventos(oe)
                'Dim dict As New Dictionary(Of String, Object)()
                Dim list As New List(Of Dictionary(Of String, Object))()
                Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
                'list = dtCursos
                For i = 0 To dt.Rows.Count - 1
                    Dim dict As New Dictionary(Of String, Object)()
                    dict.Add("material", dt.Rows(i).Item("descripcionResumidaArt"))
                    dict.Add("cantidad", dt.Rows(i).Item("cantidad_dpo"))
                    dict.Add("precuni", dt.Rows(i).Item("precio_dpo"))
                    dict.Add("entregado", dt.Rows(i).Item("cantidadEntrega_dpo"))
                    dict.Add("pendiente", dt.Rows(i).Item("cantidadFaltaEntregar"))
                    dict.Add("estado", dt.Rows(i).Item("estado_pod"))

                    dict.Add("fecha", dt.Rows(i).Item("fechaReg_pod"))
                    If dt.Rows(i).Item("nroHistoria_pod") > 0 Then
                        dict.Add("historia", dt.Rows(i).Item("nroHistoria_pod"))
                    Else
                        dict.Add("historia", "")
                    End If

                    dict.Add("total", dt.Rows(i).Item("precioTotal_pod"))



                    list.Add(dict)
                Next
                JSONresult = serializer.Serialize(list)
            ElseIf Request("param0") = "lstOftLb" Then
                Dim olF As New lFunciones

                Dim i As Integer = 0
                Dim dt As New DataTable
                Dim oe As New eOferta
                Dim ol As New lOferta
                oe.tipo = "C"
                oe.codigo_alu = Session("codigo_Alu")
                oe.codigo_cpf = Session("codigo_Cpf")
                dt = ol.Consultar(oe)
                'Dim dict As New Dictionary(Of String, Object)()
                Dim list As New List(Of Dictionary(Of String, Object))()
                Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
                'list = dtCursos
                For i = 0 To dt.Rows.Count - 1
                    Dim dict As New Dictionary(Of String, Object)()
                    dict.Add("cod", olF.EncrytedString64(dt.Rows(i).Item("codigo_ofe")))
                    dict.Add("oferta", dt.Rows(i).Item("oferta").ToString.ToUpper)
                    dict.Add("descripcion", dt.Rows(i).Item("descripcion_ofe").ToString)
                    dict.Add("lugar", dt.Rows(i).Item("lugar").ToString)
                    dict.Add("fecini", dt.Rows(i).Item("fechaInicioAnuncio").ToString)
                    dict.Add("fecfin", dt.Rows(i).Item("fechaFinAnuncio").ToString)
                    dict.Add("requisitos", dt.Rows(i).Item("requisitos_ofe").ToString)
                    dict.Add("contacto", dt.Rows(i).Item("contacto").ToString)
                    dict.Add("modopostular", dt.Rows(i).Item("modopostular_ofe").ToString)
                    dict.Add("tipotrabajo", dt.Rows(i).Item("tipotrabajo_ofe").ToString)
                    dict.Add("empresa", dt.Rows(i).Item("empresa").ToString)

                    If dt.Rows(i).Item("enviado") > 0 Then
                        dict.Add("enviado", False)
                    Else
                        dict.Add("enviado", True)

                    End If


                    dict.Add("webofe", dt.Rows(i).Item("web_ofe").ToString)

                    If dt.Rows(i).Item("vigente") = 1 Then
                        dict.Add("vigentedesc", "VIGENTE")
                        dict.Add("color", "#2196f3")
                    Else
                        dict.Add("vigentedesc", "FINALIZADO")
                        dict.Add("color", "#df6c6e")
                    End If

                    list.Add(dict)
                Next
                JSONresult = serializer.Serialize(list)


            ElseIf Request("param0") = "regOftLb" Then

                Dim list As New List(Of Dictionary(Of String, Object))()
                Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()

                Dim lFun As New lFunciones

                Dim i As Integer = 0
                Dim oePost As New eOferta
                Dim olPost As New lOferta
                Dim dtPost As New DataTable
                oePost.codigo_ofe = lFun.DecrytedString64(Request("param1"))
                oePost.codigo_alu = Session("codigo_Alu")
                dtPost = olPost.Postular(oePost)

                If dtPost.Rows.Count > 0 Then
                    If dtPost.Rows(0).Item("ID") > 0 Then
                        Dim dict As New Dictionary(Of String, Object)()
                        dict.Add("r", True)
                        dict.Add("alert", "success")
                        dict.Add("msje", "Se registro con &eacute;xito la postulaci&oacute;n")

                        Dim oeAlumno As New eAlumno
                        Dim olAlumno As New lAlumno
                        Dim dtAlu As New DataTable
                        oeAlumno.codigo_Alu = Session("codigo_Alu")
                        oeAlumno.tipoOperacion = "RG"
                        dtAlu = olAlumno.consultarAlumno(oeAlumno)


                        Dim oeOferta As New eOferta
                        Dim olOferta As New lOferta
                        Dim dtOferta As New DataTable
                        oeOferta.tipo = "C"
                        oeOferta.codigo_ofe = lFun.DecrytedString64(Request("param1"))
                        dtOferta = olOferta.Consultar(oeOferta)

                        Dim Email As String = dtOferta.Rows(i).Item("contacto").ToString
                        Dim blnResultado As Boolean = False
                        Dim Asunto, mensaje, origen, destino As String
                        Dim ObjMail As New clsMailNet
                        Asunto = "Postulo a: " & dtOferta.Rows(0).Item("oferta").ToString & " - " & dtAlu.Rows(0).Item("ApellidosNombres").ToString
                        mensaje = "<html><head><meta http-equiv='Content-Type' content='text/html; charset=iso-8859-1' /><title>Postulacion Oferta Laboral</title></head><body style='font-family: Verdana, Geneva, sans-serif;'>"

                        mensaje = mensaje & "<center><h1>" & dtOferta.Rows(0).Item("oferta").ToString & "</h1></center><br>"
                        mensaje = mensaje & "<table cellpadding=0 cellspacing =0  style='width:100%; border-top:1px solid black;border-left:1px solid black;border-right:1px solid black;border-bottom:1px solid black;'>"

                        mensaje = mensaje & "<tr>"
                        mensaje = mensaje & "<td style='text-align:left;background-color:#E33439;font-weight:bold;color:white;width:35%'>Carrera Profesional"
                        mensaje = mensaje & "</td>"
                        mensaje = mensaje & "<td style='background-color:#faebcc;font-size:11px;;width:65%'>"
                        mensaje = mensaje & "&nbsp;&nbsp;" & dtAlu.Rows(0).Item("EscuelaProfesional").ToString
                        mensaje = mensaje & "</td>"
                        mensaje = mensaje & "</tr>"

                        mensaje = mensaje & "<tr>"
                        mensaje = mensaje & "<td style='text-align:left;background-color:#E33439;font-weight:bold;color:white;'>C&oacute;digo Universitario"
                        mensaje = mensaje & "</td>"
                        mensaje = mensaje & "<td style='background-color:#faebcc;font-size:11px;'>"
                        mensaje = mensaje & "&nbsp;&nbsp;" & dtAlu.Rows(0).Item("codigoUniver_Alu").ToString
                        mensaje = mensaje & "</td>"
                        mensaje = mensaje & "</tr>"

                        mensaje = mensaje & "<tr>"
                        mensaje = mensaje & "<td style='text-align:left;background-color:#E33439;font-weight:bold;color:white;'>Estudiante"
                        mensaje = mensaje & "</td>"
                        mensaje = mensaje & "<td style='background-color:#faebcc;font-size:11px;'>"
                        mensaje = mensaje & "&nbsp;&nbsp;" & dtAlu.Rows(0).Item("ApellidosNombres").ToString
                        mensaje = mensaje & "</td>"
                        mensaje = mensaje & "</tr>"

                        mensaje = mensaje & "<tr>"
                        mensaje = mensaje & "<td style='text-align:left;background-color:#E33439;font-weight:bold;color:white;'>Doc. Identidad"
                        mensaje = mensaje & "</td>"
                        mensaje = mensaje & "<td style='background-color:#faebcc;font-size:11px;'>"
                        mensaje = mensaje & "&nbsp;&nbsp;" & dtAlu.Rows(0).Item("nroDocIdent_Alu").ToString
                        mensaje = mensaje & "</td>"
                        mensaje = mensaje & "</tr>"

                        mensaje = mensaje & "<tr>"
                        mensaje = mensaje & "<td style='text-align:left;background-color:#E33439;font-weight:bold;color:white;'>Fecha Nacimiento"
                        mensaje = mensaje & "</td>"
                        mensaje = mensaje & "<td style='background-color:#faebcc;font-size:11px;'>"
                        mensaje = mensaje & "&nbsp;&nbsp;" & dtAlu.Rows(0).Item("fechaNacimiento_Alu").ToString
                        mensaje = mensaje & "</td>"
                        mensaje = mensaje & "</tr>"

                        mensaje = mensaje & "<tr>"
                        mensaje = mensaje & "<td style='text-align:left;background-color:#E33439;font-weight:bold;color:white;'>Ciclo de Ingreso"
                        mensaje = mensaje & "</td>"
                        mensaje = mensaje & "<td style='background-color:#faebcc;font-size:11px;'>"
                        mensaje = mensaje & "&nbsp;&nbsp;" & dtAlu.Rows(0).Item("SemIngreso").ToString
                        mensaje = mensaje & "</td>"
                        mensaje = mensaje & "</tr>"

                        mensaje = mensaje & "<tr>"
                        mensaje = mensaje & "<td style='text-align:left;background-color:#E33439;font-weight:bold;color:white;'>Tel&eacute;fono Casa"
                        mensaje = mensaje & "</td>"
                        mensaje = mensaje & "<td style='background-color:#faebcc;font-size:11px;'>"
                        mensaje = mensaje & "&nbsp;&nbsp;" & dtAlu.Rows(0).Item("telefonoCasa_Dal").ToString
                        mensaje = mensaje & "</td>"
                        mensaje = mensaje & "</tr>"

                        mensaje = mensaje & "<tr>"
                        mensaje = mensaje & "<td style='text-align:left;background-color:#E33439;font-weight:bold;color:white;'>Tel&eacute;fono M&oacute;vil"
                        mensaje = mensaje & "</td>"
                        mensaje = mensaje & "<td style='background-color:#faebcc;font-size:11px;'>"
                        mensaje = mensaje & "&nbsp;&nbsp;" & dtAlu.Rows(0).Item("telefonoMovil_Dal").ToString
                        mensaje = mensaje & "</td>"
                        mensaje = mensaje & "</tr>"

                        mensaje = mensaje & "<tr>"
                        mensaje = mensaje & "<td style='text-align:left;background-color:#E33439;font-weight:bold;color:white;'>Email Principal"
                        mensaje = mensaje & "</td>"
                        mensaje = mensaje & "<td style='background-color:#faebcc;font-size:11px;'>"
                        mensaje = mensaje & "&nbsp;&nbsp;" & dtAlu.Rows(0).Item("eMail_Alu").ToString
                        mensaje = mensaje & "</td>"
                        mensaje = mensaje & "</tr>"

                        mensaje = mensaje & "<tr>"
                        mensaje = mensaje & "<td style='text-align:left;background-color:#E33439;font-weight:bold;color:white;'>Email Alternativo"
                        mensaje = mensaje & "</td>"
                        mensaje = mensaje & "<td style='background-color:#faebcc;font-size:11px;'>"
                        mensaje = mensaje & "&nbsp;&nbsp;" & dtAlu.Rows(0).Item("email2_Alu").ToString
                        mensaje = mensaje & "</td>"
                        mensaje = mensaje & "</tr>"

                        mensaje = mensaje & "<tr>"
                        mensaje = mensaje & "<td style='text-align:left;background-color:#E33439;font-weight:bold;color:white;'>Direcci&oacute;n"
                        mensaje = mensaje & "</td>"
                        mensaje = mensaje & "<td style='background-color:#faebcc;font-size:11px;'>"
                        mensaje = mensaje & "&nbsp;&nbsp;" & dtAlu.Rows(0).Item("direccion_Dal").ToString
                        mensaje = mensaje & "</td>"
                        mensaje = mensaje & "</tr>"

                        mensaje = mensaje & "<tr>"
                        mensaje = mensaje & "<td style='text-align:left;background-color:#E33439;font-weight:bold;color:white;'>Centro de Trabajo"
                        mensaje = mensaje & "</td>"
                        mensaje = mensaje & "<td style='background-color:#faebcc;font-size:11px;'>"
                        mensaje = mensaje & "&nbsp;&nbsp;" & dtAlu.Rows(0).Item("centroTrabajo_Dal").ToString
                        mensaje = mensaje & "</td>"
                        mensaje = mensaje & "</tr>"

                        mensaje = mensaje & "</table><br>"
                        mensaje = mensaje & "Enviado desde: <b>Campus Virtual Estudiante</b><br>"
                        mensaje = mensaje & "</body></hmtl>"
                        origen = "desarrollosistemas@usat.edu.pe"
                        destino = Trim(Email)
                        blnResultado = ObjMail.EnviarMail("campusvirtual@usat.edu.pe", "Desarrollo de Sistemas", destino, Asunto, mensaje, True, "Desarrollo de Sistemas", destino)
                        If (blnResultado = True) Then
                            dict.Add("email", Email)
                            dict.Add("destino", destino)
                            dict.Add("aviso", "Correo enviado correctamente")
                            dict.Add("envio", "OK")
                        Else
                            dict.Add("aviso", "Error al enviar correo con contrase&ntilde;a")
                            dict.Add("envio", "ERROR")
                        End If


                        list.Add(dict)
                    Else
                        Dim dict As New Dictionary(Of String, Object)()
                        dict.Add("r", False)
                        dict.Add("alert", "error")
                        dict.Add("msje", "Surgieron problemas en la postulaci&oacute;n")
                        list.Add(dict)
                    End If
                Else
                    Dim dict As New Dictionary(Of String, Object)()
                    dict.Add("r", False)
                    dict.Add("alert", "error")
                    dict.Add("msje", "Surgieron problemas en la postulaci&oacute;n!")
                    list.Add(dict)
                End If


                JSONresult = serializer.Serialize(list)
            ElseIf Request("param0") = "viewFact" Then
                Try


                    Dim olF As New lFunciones

                    Dim res As New ws.DownloadingFiles
                    Dim params As New ws.ParameterType

                    Dim path As String = "C:"
                    Dim nombre As String = ""
                    Dim oeCin As New eCajaIngreso
                    Dim olCin As New lCajaIngreso
                    Dim dtCin As New DataTable
                    oeCin.tipo = "V"
                    oeCin.fecha_Cin = "#01/01/1901#"
                    oeCin.codigo_Cin = olF.DecrytedString64(Request("param1"))
                    dtCin = olCin.consultarCajaIngreso(oeCin)
                    With params

                        If dtCin.Rows(0).Item("codigo_Tdo").ToString = "7" Then  ' Boleta
                            .Serie = "B" & dtCin.Rows(0).Item("serieCin").ToString
                        ElseIf dtCin.Rows(0).Item("codigo_Tdo").ToString = "9" Then ' Factura
                            .Serie = "F" & dtCin.Rows(0).Item("serieCin").ToString
                        End If


                        .Numero = dtCin.Rows(0).Item("numeroCin").ToString
                        .Ruc = dtCin.Rows(0).Item("rucUSAT").ToString
                        .Importe = CDec(dtCin.Rows(0).Item("total_Cin"))
                        .Fecha = CStr(CDate(dtCin.Rows(0).Item("fecha_Cin")).ToString("yyyy-MM-dd"))
                        .codigo = dtCin.Rows(0).Item("codigoSUNAT_Tdo").ToString.Trim
                        ' path = "C:\" & .Serie & "-" & .Numero & ".pdf"
                        '.codigo = "03"
                        '.Serie = "B001"
                        '.Numero = "000108"
                        '.Importe = 212
                        '.Fecha = "22-11-2016"
                        nombre = .Serie & "-" & .Numero

                    End With

                    Dim cadenas As ws.ResultMessage = res.OnlineRecoveryPDFto64(params)

                    Dim list As New List(Of Dictionary(Of String, Object))()
                    Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()

                    Dim dict As New Dictionary(Of String, Object)()

                    If cadenas.Status = "OK" Then
                        Dim image As Byte() = Convert.FromBase64String(cadenas.PdfBase64)
                        dict.Add("R", True)
                        dict.Add("Msg", "Se ha descargado el archivo " & params.Serie & "-" & params.Numero & ".pdf")
                        dict.Add("Alert", "success")
                        dict.Add("bin", cadenas.PdfBase64)
                        dict.Add("Nombre", params.Serie & "-" & params.Numero & ".pdf")
                    Else
                        dict.Add("R", False)
                        dict.Add("Msg", cadenas.StatusBody.Message)
                        dict.Add("Alert", "warning")
                    End If
                    list.Add(dict)
                    JSONresult = serializer.Serialize(list)
                Catch ex As Exception
                    Response.Write(ex.Message)
                End Try

            ElseIf Request("param0") = "lstCod" Then
                Try


                    Dim olF As New lFunciones

                    Dim oeAlumno As New eAlumno
                    Dim olAlumno As New lAlumno
                    Dim dt As New DataTable

                    oeAlumno.tipoOperacion = "PSO"
                    oeAlumno.codigo_Alu = Session("codigo_Alu")
                    dt = olAlumno.consultarAlumno(oeAlumno)

                    Dim list As New List(Of Dictionary(Of String, Object))()
                    Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()

                    For i As Integer = 0 To dt.Rows.Count - 1
                        Dim dict As New Dictionary(Of String, Object)()
                        dict.Add("reset", False)
                        dict.Add("c", olF.EncrytedString64(dt.Rows(i).Item("codigo").ToString()))
                        dict.Add("coduniv", dt.Rows(i).Item("codigouniversitario").ToString())
                        dict.Add("cp", olF.EncrytedString64(dt.Rows(i).Item("cp").ToString()))
                        dict.Add("escuela", dt.Rows(i).Item("escuela").ToString())
                        If dt.Rows(i).Item("codigouniversitario").ToString() = Session("codigoUniver_Alu").ToString() Then
                            dict.Add("selected", "selected")
                        Else
                            dict.Add("selected", "")
                        End If

                        list.Add(dict)
                    Next

                    JSONresult = serializer.Serialize(list)
                Catch ex As Exception
                    'Response.Write(ex.Message)
                    Dim list As New List(Of Dictionary(Of String, Object))()
                    Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
                    Dim dict As New Dictionary(Of String, Object)()
                    dict.Add("reset", True)
                    dict.Add("msje", ex.Message())
                    list.Add(dict)
                    JSONresult = serializer.Serialize(list)
                End Try

            ElseIf Request("param0") = "lstmsjinf" Then
                Try


                    Dim olF As New lFunciones

                    Dim oe As New eServicioCentroCosto
                    Dim ol As New lServicioCentroCosto
                    Dim dt As New DataTable

                    oe.tipooperacion = "1"
                    oe.param0 = 0
                    oe.param1 = olF.DecrytedString64(Request("param1"))
                    dt = ol.ConsultarConceptoTramiteMensajeInfo(oe)

                    Dim list As New List(Of Dictionary(Of String, Object))()
                    Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()

                    For i As Integer = 0 To dt.Rows.Count - 1
                        Dim dict As New Dictionary(Of String, Object)()

                        dict.Add("c", olF.EncrytedString64(dt.Rows(i).Item("codigo_mctr").ToString()))
                        dict.Add("info", dt.Rows(i).Item("descripcion").ToString())
                        
                        list.Add(dict)
                    Next

                    JSONresult = serializer.Serialize(list)
                Catch ex As Exception
                    'Response.Write(ex.Message)
                    Dim list As New List(Of Dictionary(Of String, Object))()
                    Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
                    Dim dict As New Dictionary(Of String, Object)()
                    dict.Add("reset", True)
                    dict.Add("msje", ex.Message())
                    list.Add(dict)
                    JSONresult = serializer.Serialize(list)
                End Try
                'INICIO TRAMITES
            ElseIf Request("param0") = "lstSCTR" Then  'seleccionar servicios
                Try
                    Dim olF As New lFunciones
                    Dim oe As New eServicioCentroCosto
                    Dim ol As New lServicioCentroCosto
                    Dim dt As New DataTable
                    oe.tipooperacion = "1"
                    oe.codigo_ctr = 0 ' se envia como parametro de codigo_ctr

                    oe.numtramite = "" 'Request("param2").ToString()
                    oe.codigo_Alu = olF.DecrytedString64(Request("param1").ToString())
                    'oe.codigo_Alu = Session("codigo_Alu")
                    dt = ol.ConsultarServiciosTramite(oe)
                    Session.Add("lstServiciosTramite", dt)
                    Dim list As New List(Of Dictionary(Of String, Object))()
                    Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
                    For i As Integer = 0 To dt.Rows.Count - 1
                        If dt.Rows(i).Item("cumple") = 1 And dt.Rows(i).Item("mostrar") = 1 Then
                            Dim dict As New Dictionary(Of String, Object)()
                            ' If dt.Rows(i).Item("codigo_sco").ToString() <> "1099" Then
                            'If dt.Rows(i).Item("codigo_sco").ToString() = "1099" Or dt.Rows(i).Item("codigo_sco").ToString() = "1826" Then
                            dict.Add("idx", i)
                            If dt.Rows(i).Item("codigo_sco").ToString() = "1099" Then
                                dict.Add("sw", False)
                            Else
                                dict.Add("sw", True)
                            End If
                            dict.Add("c", olF.EncrytedString64(dt.Rows(i).Item("codigo_ctr").ToString()))
                            dict.Add("csc", olF.EncrytedString64(dt.Rows(i).Item("codigo_sco").ToString()))
                            dict.Add("precio", CDec(dt.Rows(i).Item("precio_ctr")))
                            dict.Add("cant", CInt(dt.Rows(i).Item("cantMax_ctr")))
                            If dt.Rows(i).Item("cantMax_ctr") = 0 Then
                                dict.Add("cbo", False)
                            Else
                                dict.Add("cbo", True)
                            End If
                            If dt.Rows(i).Item("codigo_tctr") = 1 Then
                                dict.Add("bold", True)
                            Else
                                dict.Add("bold", False)
                            End If
                            'dict.Add("tramite", dt.Rows(i).Item("descripcion_Sco").ToString().ToUpper())
                            dict.Add("tramite", dt.Rows(i).Item("descripcion_ctr").ToString().ToUpper())
                            dict.Add("tipo", dt.Rows(i).Item("nombre_tctr").ToString().ToUpper())

                            If dt.Rows(i).Item("tieneRequisito") And dt.Rows(i).Item("rutaImg").ToString <> "" Then
                                dict.Add("swTR", True)
                            Else
                                dict.Add("swTR", False)
                            End If

                            If dt.Rows(i).Item("tieneSolicitudVirtual") Then
                                dict.Add("swSV", True)
                            Else
                                dict.Add("swSV", False)
                            End If
                            If dt.Rows(i).Item("bloquear") Then
                                dict.Add("swBlq", True)
                            Else
                                dict.Add("swBlq", False)
                            End If

                            If dt.Rows(i).Item("tieneNotaAbonoAutomatica") Then
                                dict.Add("swna", True)
                            Else
                                dict.Add("swna", False)
                            End If
                            If dt.Rows(i).Item("tieneMensajeInformativo") Then
                                dict.Add("swmi", True)
                            Else
                                dict.Add("swmi", False)
                            End If


                            If dt.Rows(i).Item("grupo_ctr") <> "" Then
                                dict.Add("swGrp", True)
                                Dim cad As String = ""
                                ' dict.Add("ctrGrp", dt.Rows(i).Item("grupo_ctr"))
                                'Dim input As String = "ABC;PQR;XYZ"
                                'Dim x As New List(Of String)(input.Split(";"c))
                                Dim input As String = dt.Rows(i).Item("grupo_ctr").ToString
                                Dim x As New List(Of String)(input.Split(","c))

                                For j As Int32 = 0 To x.Count - 1
                                    cad = cad & olF.EncrytedString64(x(j).ToString) & ","
                                Next
                                dict.Add("ctrGrp", cad)


                            Else
                                dict.Add("swGrp", False)
                                dict.Add("ctrGrp", dt.Rows(i).Item("grupo_ctr"))
                            End If
                            dict.Add("ruta", dt.Rows(i).Item("rutaImg").ToString)

                            If dt.Rows(i).Item("tieneArchivo") Then
                                dict.Add("swFl", True)
                            Else
                                dict.Add("swFl", False)
                            End If

                            list.Add(dict)
                            'End If
                        End If
                    Next
                    JSONresult = serializer.Serialize(list)
                Catch ex As Exception
                    'Response.Write(ex.Message)
                    Dim list As New List(Of Dictionary(Of String, Object))()
                    Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
                    Dim dict As New Dictionary(Of String, Object)()
                    dict.Add("reset", True)
                    list.Add(dict)
                    JSONresult = serializer.Serialize(list)
                End Try
            ElseIf Request("param0") = "lstC_TR" Then  ' listar cabeceras de tramites
                Dim olF As New lFunciones
                Try

                    Dim list As New List(Of Dictionary(Of String, Object))()
                    Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
                    Dim trl_hoy As Int16 = 0
                    If Session("TRL_bloqueo") = False Then
                        Dim oe As New eServicioCentroCosto
                        Dim ol As New lServicioCentroCosto
                        Dim est As String = String.Empty
                        Dim dt As New DataTable
                        Dim chk As Boolean = False
                        oe.tipooperacion = "3"
                        oe.codigo_ctr = 0 ' se envia como parametro de codigo_ctr
                        oe.numtramite = CStr(Request("param2").ToString())
                        oe.codigo_Alu = CInt(olF.DecrytedString64(Request("param1").ToString()))

                        If Request("est0").ToString() <> "" Then
                            est = est & Request("est0").ToString() & ","
                            chk = True
                        End If
                        If Request("est1").ToString() <> "" Then
                            est = est & Request("est1").ToString() & ","
                            chk = True
                        End If
                        If Request("est2").ToString() <> "" Then
                            est = est & Request("est2").ToString() & ","
                            chk = True
                        End If
                        If Request("est3").ToString() <> "" Then
                            est = est & Request("est3").ToString() & ","
                            chk = True
                        End If

                        If chk Then
                            est = Mid(est, 1, Len(est.ToString) - 1)
                        Else
                            est = "0"
                        End If

                        oe.estado = est


                        dt = ol.ConsultarServiciosTramite(oe)



                        For i As Integer = 0 To dt.Rows.Count - 1

                            '##
                            If dt.Rows(i).Item("fechaRegistro").ToString() = Date.Today().ToString("dd/MM/yyyy") Then
                                trl_hoy = trl_hoy + 1
                            End If

                            Dim dict As New Dictionary(Of String, Object)()
                            dict.Add("ctr", olF.EncrytedString64(dt.Rows(i).Item("codigo_trl").ToString()))
                            dict.Add("cd", olF.EncrytedString64(dt.Rows(i).Item("codigo_dta").ToString()))
                            'dict.Add("cd", "")
                            dict.Add("sol", dt.Rows(i).Item("glosaCorrelativo_trl").ToString().ToUpper())
                            dict.Add("fecha", dt.Rows(i).Item("fechaRegistro").ToString().ToUpper())
                            dict.Add("fechavenc", dt.Rows(i).Item("fechavence").ToString().ToUpper())

                            If dt.Rows(i).Item("estado_ctr").ToString() = "ENTREGADO" Then
                                dict.Add("fechaent", dt.Rows(i).Item("fechaentrega").ToString().ToUpper())
                            Else
                                dict.Add("fechaent", "")
                            End If

                            dict.Add("obsc", dt.Rows(i).Item("observacion_trl").ToString().ToUpper())
                            dict.Add("tramite", dt.Rows(i).Item("descripcion_ctr").ToString().ToUpper())
                            dict.Add("cant", CInt(dt.Rows(i).Item("cantidad_dta")))
                            dict.Add("prec", CDec(dt.Rows(i).Item("precio_ctr")))
                            dict.Add("obsd", dt.Rows(i).Item("observacion_dft").ToString().ToUpper())
                            dict.Add("estado", dt.Rows(i).Item("estado_ctr").ToString().ToUpper())
                            dict.Add("orden", CInt(dt.Rows(i).Item("correlativo_trl")))

                            If dt.Rows(i).Item("estado_ctr").ToString().ToUpper() = "PENDIENTE" Then
                                dict.Add("swa", True)
                            Else
                                dict.Add("swa", False)
                            End If

                            '#EPENA 12/09/2019  Mostrar el flujo siempre, si es que el tramite esta configurado para evaluar
                            'If (dt.Rows(i).Item("tieneRequisito").ToString = "True" Or dt.Rows(i).Item("flujo") = 1) And dt.Rows(i).Item("estado_ctr").ToString() <> "PENDIENTE" Then
                            If (dt.Rows(i).Item("tieneRequisito").ToString = "True" Or dt.Rows(i).Item("flujo") = 1) Then
                                dict.Add("req", True)
                            Else
                                dict.Add("req", False)
                            End If

                            'dict.Add("requisito", dt.Rows(i).Item("tieneRequisito").ToString)
                            If dt.Rows(i).Item("estado_ctr").ToString() = "PENDIENTE" Then
                                dict.Add("color", "White")
                                dict.Add("bg", "#E33439")
                                dict.Add("estadofecha", dt.Rows(i).Item("observacionAlumno_dft").ToString())
                            ElseIf dt.Rows(i).Item("estado_ctr").ToString() = "EN TRAMITE" Then
                                dict.Add("color", "White")
                                dict.Add("bg", "#FFA500")
                                dict.Add("estadofecha", dt.Rows(i).Item("observacionAlumno_dft").ToString())
                            ElseIf dt.Rows(i).Item("estado_ctr").ToString() = "POR ENTREGAR" Then
                                dict.Add("color", "Black")
                                dict.Add("bg", "#F4E9A6")
                                dict.Add("estadofecha", dt.Rows(i).Item("observacion_dft").ToString())
                            ElseIf dt.Rows(i).Item("estado_ctr").ToString() = "ENTREGADO" Or dt.Rows(i).Item("estado_ctr").ToString() = "FINALIZADO" Then
                                dict.Add("color", "Black")
                                dict.Add("bg", "#AAF196")
                                dict.Add("estadofecha", "")
                            End If

                            list.Add(dict)

                        Next
                    End If

                    '#Epena 26-04-2019{
                    '#Epena Busca si el alumno tiene un tramite en el dia para saber si ha confirmado telefono y correo electronico 
                    Dim dict2 As New Dictionary(Of String, Object)()
                    Dim listConfirma As New List(Of Dictionary(Of String, Object))()
                    'Response.Write("trl_hoy: " & trl_hoy)
                    If trl_hoy >= 1 Then
                        Dim dictConfirma1 As New Dictionary(Of String, Object)()
                        dictConfirma1.Add("confirma", 1) ' confirma email
                        Dim dictConfirma2 As New Dictionary(Of String, Object)()
                        dictConfirma2.Add("confirma", 1) ' confirma telefono

                        listConfirma.Add(dictConfirma1)
                        listConfirma.Add(dictConfirma2)
                        dict2.Add("cnfdata", True)
                    Else
                        dict2.Add("cnfdata", False)
                    End If

                    list.Add(dict2)
                    Session("lstConfirmaTramite") = listConfirma
                    '}#Epena 26-04-2019

                    JSONresult = serializer.Serialize(list)
                Catch ex As Exception
                    'Response.Write(ex.Message)
                    Dim list As New List(Of Dictionary(Of String, Object))()
                    Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
                    Dim dict As New Dictionary(Of String, Object)()
                    dict.Add("reset", True)
                    dict.Add("msje", ex.Message & "   " & ex.StackTrace)
                    list.Add(dict)
                    JSONresult = serializer.Serialize(list)
                End Try
            ElseIf Request("param0") = "lstD_TR" Then  ' mostrar en modal detalle de tramites
                Dim olF As New lFunciones
                Try

                    Dim oe As New eServicioCentroCosto
                    Dim ol As New lServicioCentroCosto
                    Dim dt As New DataTable
                    oe.tipooperacion = "2"
                    oe.codigo_ctr = CInt(olF.DecrytedString64(Request("param1").ToString())) ' PARAMETRO ID CAB TRAMITE
                    oe.numtramite = "" 'Request("param2").ToString()
                    oe.codigo_Alu = 0 'olF.EncrytedString64(Request("param1").ToString())
                    dt = ol.ConsultarServiciosTramite(oe)

                    Dim list As New List(Of Dictionary(Of String, Object))()
                    Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
                    For i As Integer = 0 To dt.Rows.Count - 1

                        Dim dict As New Dictionary(Of String, Object)()
                        dict.Add("tramite", dt.Rows(i).Item("descripcion_Sco").ToString().ToUpper())
                        dict.Add("num", dt.Rows(i).Item("glosaCorrelativo_trl").ToString().ToUpper())
                        dict.Add("estado", dt.Rows(i).Item("estado_ctr").ToString().ToUpper())
                        list.Add(dict)

                    Next
                    JSONresult = serializer.Serialize(list)
                Catch ex As Exception
                    'Response.Write(ex.Message)
                    Dim list As New List(Of Dictionary(Of String, Object))()
                    Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
                    Dim dict As New Dictionary(Of String, Object)()
                    dict.Add("reset", True)
                    list.Add(dict)
                    JSONresult = serializer.Serialize(list)
                End Try
            ElseIf Request("param0") = "gTramite" Then
                Try
                    Dim olF As New lFunciones
                    Dim oe As New eServicioCentroCosto
                    Dim ol As New lServicioCentroCosto
                    Dim dt As New DataTable

                    Dim sco_cod As Integer

                    sco_cod = fnBuscarCodigoTramite("1099")
                    Dim codigos_ctr As String = ""
                    Dim cantidades_dta As String = ""
                    Dim num As Integer = CInt(Request("num"))
                    Dim i As Integer = 0

                    Dim sw As Boolean = False
                    Dim swVTDA As Boolean = False
                    Dim swConfirmaDatos As Boolean = False
                    Dim tsv As Boolean = False
                    Dim detalleul As String = "<ul>"
                    Dim detallesv As String = ""
                    Dim detalle As String = ""

                    For i = 0 To num - 1
                        If Request("txtchk[" & i & "]") = "S" Then
                            If CInt(Request("txtcant[" & i & "]")) > 0 Then
                                sw = True

                                If tsv = False Then
                                    'Response.Write(tsv.ToString & "-tsv1-")
                                    'Response.Write(olF.DecrytedString64(Request("txtc[" & i & "]")).ToString & "-cod" & i.ToString & "-")
                                    tsv = fnTieneSolicitudVirtual(CInt(olF.DecrytedString64(Request("txtc[" & i & "]")).ToString))
                                    'Response.Write(tsv.ToString & "-tsv2-")
                                End If


                                codigos_ctr = codigos_ctr & olF.DecrytedString64(Request("txtc[" & i & "]")).ToString & ","
                                cantidades_dta = cantidades_dta & Request("txtcant[" & i & "]").ToString & ","
                                detalle = detalle & "<li>(" & Request("txtcant[" & i & "]").ToString & ") " & fnBuscarNombreTramite(olF.DecrytedString64(Request("txtc[" & i & "]")).ToString) & "</li>"
                            End If
                        End If
                    Next
                    detalle = detalle & "</ul><br>"
                    'Response.Write(sco_cod.ToString & ")<br>")

                    If sco_cod > 0 Then
                        If tsv Then
                            detallesv = detallesv & "<li><i>(*) " & fnBuscarNombreTramite(1) & "</i></li>"
                            codigos_ctr = sco_cod.ToString & "," & codigos_ctr
                            cantidades_dta = "1," & cantidades_dta
                        End If
                    End If

                    'Response.Write(detallesv.ToString & "<br>")
                    detalleul = detalleul & detallesv & detalle

                    oe.codigo_Alu = olF.DecrytedString64(Request("cboRegCodigo"))
                    oe.codigo_Cac = CInt(Session("Codigo_Cac"))
                    oe.estado = "P"
                    oe.observacion = Request("txtregobserv").ToString
                    oe.codigos_ctr = codigos_ctr
                    oe.cantidades_dta = cantidades_dta
                    oe.param1 = Request("param1")
                    oe.param2 = Request("param2")

                    If Request("param2_text") IsNot Nothing Then
                        'Response.Write(Request("param2_text").ToString())
                        oe.param2_text = Request("param2_text")
                    End If

                    oe.param3 = Request("param3")
                    oe.param4 = Request("param4")
                    oe.param5 = Request("param5")

                    Dim Email As String = ""
                    Dim Email2 As String = ""
                    Dim msje As String = ""

                    Email2 = Request("txtdaEmail")
                    'Email2 = "fatimavv_10@hotmail.com"
                    Email2 = fnLimpiarEspaciosEnBlanco(Email2)

                    If olF.DecrytedString64(Request("reguser")) = "" Then
                        Email = Request("txtdaEmail")
                    Else
                        'Email = "fatima.vasquez@usat.edu.pe"
                        Email = olF.DecrytedString64(Request("reguser"))
                        Email = fnLimpiarEspaciosEnBlanco(Email)
                    End If

                    Dim list As New List(Of Dictionary(Of String, Object))()
                    Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
                    Dim dict As New Dictionary(Of String, Object)()
                    Dim codigo_trl As Integer = 0
                    Dim codigo_dta As Integer = 0

                    'Validar si se han seleccionado datos adicionales
                    Dim listVTDA As New List(Of Dictionary(Of String, Object))()
                    listVTDA = Session("lstValidaTramite")

                    If listVTDA IsNot Nothing AndAlso listVTDA.Count > 0 Then
                        swVTDA = True
                    End If

                    'Validar si se han confirmado los datos de telefono y correo
                    Dim listConfirma As New List(Of Dictionary(Of String, Object))()
                    listConfirma = Session("lstConfirmaTramite")

                    If listConfirma IsNot Nothing AndAlso listConfirma.Count >= 2 Then
                        swConfirmaDatos = True
                    End If

                    If sw Then

                        If swVTDA Then

                            Dim strVTDA As String = "<ul>"


                            For Each Entry As Dictionary(Of String, Object) In listVTDA
                                For Each kvp As KeyValuePair(Of String, Object) In Entry
                                    Dim v1 As String = kvp.Key
                                    Dim v2 As String = kvp.Value
                                    ' Debug.WriteLine("Key: " + v1.ToString + " Value: " + v2)
                                    strVTDA = strVTDA & "<li> <i class='ion-android-warning'></i> " & v2.ToString & "</li>"
                                Next
                            Next

                            strVTDA = strVTDA & "</ul>"


                            dict.Add("sw", False)
                            dict.Add("msje", strVTDA)
                            dict.Add("alert", "default")
                            list.Add(dict)

                        Else

                            If swConfirmaDatos Then


                                ''''''''''''''''''''''''''''''SI SE CUMPLE SW: Selecion de tramite{
                                dt = ol.RegistrarTramite(oe)
                                msje = "Registrado satisfactoriamente. "
                                If dt.Rows(0).Item("RPTA").ToString() = "OK" Then
                                    codigo_trl = dt.Rows(0).Item("ID").ToString()
                                    codigo_dta = dt.Rows(0).Item("CODIGO_DTA").ToString()

                                    If Email Is Nothing Or Email = "" Then
                                        dict.Add("sw", True)
                                        dict.Add("msje", msje & " Pero no tienes correo registrado")
                                        dict.Add("alert", "success")
                                    Else
                                        Dim totalTramite As String = Request("mon")

                                        Dim blnResultado As Boolean = False
                                        Dim blnResultado2 As String = ""
                                        Dim Asunto, mensaje, origen, destino As String
                                        Dim ObjMail As New clsMailNet
                                        Asunto = "Constancia de Registro de Trámite Virtual - Campus Estudiante"
                                        mensaje = "<html><head><meta http-equiv='Content-Type' content='text/html; charset=iso-8859-1' /><title>Constancia de Registro de Tr&aacute;mite Virtual</title><style type='text/css'>"
                                        mensaje = mensaje + ".Estilo1 {font-family: Verdana, Arial, Helvetica, sans-serif;font-size: 12px;}</style></head><body><table width='100%' border='0'>"
                                        mensaje = mensaje + "<tr><td colspan='3'>&nbsp;</td></tr><tr><td><span class='Estilo1'>Estimado(a) <b>" & Session("nombreCompleto").ToString & "</b></span></td></tr>"
                                        mensaje = mensaje + "<td>&nbsp;</td><td>&nbsp;</td></tr><tr><td><br><span class='Estilo1'>El día de Hoy " + CDate(Date.Today).ToString("dddd").ToString + " " + CStr(CDate(Date.Today).ToString("dd/MM/yyyy")) + " a las " + Date.Now.ToString("hh:mm tt").ToString + " horas se ha registrado la solicitud de trámite N° <b>" + dt.Rows(0).Item("NRO").ToString + "</b> para:"
                                        mensaje = mensaje + detalleul

                                        If totalTramite <> "S/. 0.00" Then
                                            mensaje = mensaje + "<br>Tienes hasta el día <b>" + dt.Rows(0).Item("FECHA_VENCE").ToString + "</b> para cancelar los <b>" + Request("mon") + "</b> por el concepto de los trámites correspondientes."
                                            mensaje = mensaje + "<br>Puedes realizar los pagos en cualquier canal de atención del Banco de Crédito o Banco Continental (Agencias, agentes BCP, Banca por Internet y Banca Móvil) o pago con tarjeta a través del campusvirtual o pago con tarjeta a través del campusvirtual"
                                            mensaje = mensaje + "<br><br>(*)Recuerda que de no cancelar hasta la fecha indicada la solicitud será anulada."

                                        End If

                                        mensaje = mensaje + "</span></td><td>&nbsp;</td><td>&nbsp;</td></tr></table></body></hmtl>"
                                        origen = "serviciosti@usat.edu.pe"
                                        destino = Trim(Email)
                                        If Email2 <> "" Then
                                            destino = Trim(Email) & ";" & Trim(Email2)
                                            ' blnResultado2 = ObjMail.fnEnviarMailVariosCad("serviciosti@usat.edu.pe", "Servicios TI", destino, Asunto, mensaje, True, "Servicios TI", destino)
                                            blnResultado2 = ObjMail.fnEnviarMailVariosCad("serviciosti@usat.edu.pe", "Servicios TI", destino, Asunto, mensaje, True, "Servicios TI", "serviciosti@usat.edu.pe")
                                            If (blnResultado2 = "OK") Then
                                                blnResultado = True
                                            End If
                                            ' blnResultado = True
                                        Else
                                            blnResultado = ObjMail.EnviarMail("serviciosti@usat.edu.pe", "Servicios TI", destino, Asunto, mensaje, True, "Servicios TI", destino)
                                        End If
                                        dict.Add("destino", destino)
                                        dict.Add("blnResultado", blnResultado)
                                        dict.Add("blnResultado2", blnResultado2)

                                        If (blnResultado = True) Then
                                            dict.Add("sw", True)
                                            dict.Add("msje", msje & ", revisar bandeja de correo " & Email.ToString)
                                            dict.Add("alert", "success")
                                            dict.Add("totalTramite", totalTramite)
                                            dict.Add("nom", Request("nom"))
                                            dict.Add("nom2", CDec(Request("nom")))
                                        Else
                                            dict.Add("sw", True)
                                            dict.Add("msje", msje & ", verificar su correo si es correcto")
                                            dict.Add("alert", "error")
                                        End If
                                        Dim listConfirmaVacio As New List(Of Dictionary(Of String, Object))()
                                        Session("lstConfirmaTramite") = listConfirmaVacio

                                    End If
                                    dict.Add("trl", codigo_trl)
                                    dict.Add("ope", codigo_dta)
                                    list.Add(dict)

                                Else
                                    dict.Add("sw", True)
                                    dict.Add("msje", ".Hay incovenientes en el proceso, comunicarse con serviciosti@usat.edu.pe ")
                                    dict.Add("alert", "error")
                                    list.Add(dict)
                                End If

                                ''''''''''''''''''''''''''''''}SI SE CUMPLE SW: Selecion de tramite
                            Else
                                'NO CUMPLE DATOS DE CONFIEMACION: TELEFONO Y CORREO

                                dict.Add("sw", False)
                                dict.Add("msje", "Confirme Teléfono y Email Personal")
                                dict.Add("alert", "default")
                                list.Add(dict)
                            End If
                        End If






                    Else
                        dict.Add("sw", False)
                        dict.Add("msje", "Seleccione Tr&aacute;mites")
                        dict.Add("alert", "warning")
                        list.Add(dict)
                    End If

                    JSONresult = serializer.Serialize(list)
                Catch ex As Exception
                    'Response.Write(ex.Message)
                    Dim list As New List(Of Dictionary(Of String, Object))()
                    Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
                    Dim dict As New Dictionary(Of String, Object)()
                    dict.Add("sw", True)
                    dict.Add("msje", "Hay incovenientes en el proceso, comunicarse con serviciosti@usat.edu.pe")
                    dict.Add("alert", "error")
                    dict.Add("error", ex.Message & "  " & ex.StackTrace)

                    list.Add(dict)
                    JSONresult = serializer.Serialize(list)
                End Try
            ElseIf Request("param0") = "aTramite" Then
                Dim list As New List(Of Dictionary(Of String, Object))()
                Dim dict As New Dictionary(Of String, Object)()
                Dim oeIncidencia As New eIncidencia
                Dim olIncidencia As New lIncidencia

                Dim olF As New lFunciones
                Dim oe As New eServicioCentroCosto
                Dim ol As New lServicioCentroCosto
                Dim dt As New DataTable

                With oe
                    .tipooperacion = ""
                    .codigo_dta = olF.DecrytedString64(Request("param1").ToString)
                    .codigo_Alu = Session("codigo_Alu")

                End With

                dt = ol.AnularTramite(oe)

                If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                    If dt.Rows(0).Item("RPTA").ToString = "OK" Then
                        dict.Add("r", True)
                        dict.Add("msje", "Se ha anulado correctamente el trámite")
                        dict.Add("alert", "success")
                    Else
                        dict.Add("r", False)
                        dict.Add("msje", "Se produjo un problema al anular trámite")
                        dict.Add("alert", "error")
                    End If


                End If
                'dict.Add("h", "hola")


                'list.Add(dict)
                Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
                'Response.write(serializer.Serialize(list))
                ' MsgBox(serializer.Serialize(list))
                JSONresult = serializer.Serialize(dict)

            ElseIf Request("param0") = "regAluf" Then
                Dim list As New List(Of Dictionary(Of String, Object))()
                Dim dict As New Dictionary(Of String, Object)()
                Dim oeIncidencia As New eIncidencia
                Dim olIncidencia As New lIncidencia
                Dim dt As New DataTable
                With oeIncidencia
                    .codigo_cac = Session("codigo_Cac")
                    .codigo_alu = Session("codigo_Alu")
                    .asunto = Request("asunto").ToString
                    .mensaje = Request("mensaje").ToString

                End With

                dt = olIncidencia.InsertaIncidenteForoAlumno(oeIncidencia)

                If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                    If dt.Rows(0).Item("ID") > 0 Then
                        dict.Add("r", True)
                        dict.Add("c", dt.Rows(0).Item("ID"))
                        dict.Add("n", dt.Rows(0).Item("NUMERO"))
                        dict.Add("msje", "Se ha registrado correctamente")
                        dict.Add("alert", "success")
                    Else
                        dict.Add("r", False)
                        dict.Add("c", CInt(dt.Rows(0).Item("ID")))
                        dict.Add("n", dt.Rows(0).Item("NUMERO"))
                        dict.Add("msje", "Se produjo un problema al registrar")
                        dict.Add("alert", "error")
                    End If


                End If
                'dict.Add("h", "hola")


                'list.Add(dict)
                Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
                'Response.write(serializer.Serialize(list))
                ' MsgBox(serializer.Serialize(list))
                JSONresult = serializer.Serialize(dict)


            ElseIf Request("param0") = "lstTREQ" Then



                Try
                    Dim olF As New lFunciones
                    Dim oe As New eServicioCentroCosto
                    Dim ol As New lServicioCentroCosto
                    Dim dt As New DataTable
                    oe.tipooperacion = "1"
                    oe.codigo_ctr = olF.DecrytedString64(Request("param1").ToString) ' se envia como parametro de codigo_ctr                    
                    dt = ol.ConsultarServiciosTramiteRequisitos(oe)
                    Session.Add("lstTramiteRequisitos", dt)
                    Dim list As New List(Of Dictionary(Of String, Object))()
                    Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
                    Dim ctre As Integer = 0
                    Dim i As Integer = 0
                    Dim j As Integer = 0
                    Dim j2 As Integer = 0
                    For i = 0 To dt.Rows.Count - 1
                        Dim dict As New Dictionary(Of String, Object)()


                        ' Response.Write(i.ToString)
                        dict.Add("cp", dt.Rows(i).Item("codigo_ctre").ToString)
                        dict.Add("ch", 0)
                        dict.Add("desc", dt.Rows(i).Item("nombre_tre").ToString)
                        dict.Add("t", "P")
                        ctre = dt.Rows(i).Item("codigo_ctre")
                        list.Add(dict)

                        For j = i To dt.Rows.Count - 1

                            If dt.Rows(j).Item("codigo_ctre_esp") = ctre Then
                                Dim dict2 As New Dictionary(Of String, Object)()
                                dict2.Add("cp", dt.Rows(j).Item("codigo_ctre").ToString)
                                dict2.Add("ch", dt.Rows(j).Item("codigo_ctre_esp").ToString)
                                dict2.Add("desc", dt.Rows(j).Item("descripcion_resp").ToString)
                                dict2.Add("t", "H")
                                list.Add(dict2)
                                i = j
                            End If

                        Next

                        'If j2 > 0 Then
                        'i = j2 + 1
                        ' End If

                    Next
                    JSONresult = serializer.Serialize(list)
                Catch ex As Exception
                    'Response.Write(ex.Message)
                    Dim list As New List(Of Dictionary(Of String, Object))()
                    Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
                    Dim dict As New Dictionary(Of String, Object)()
                    dict.Add("reset", True)
                    dict.Add("msje", ex.Message)
                    list.Add(dict)
                    JSONresult = serializer.Serialize(list)
                End Try

                '' Documentos a descargar por tramite
            ElseIf Request("param0") = "lstfctr" Then
                Try
                    Dim olF As New lFunciones
                    Dim oe As New eServicioCentroCosto
                    Dim ol As New lServicioCentroCosto
                    Dim dt As New DataTable

                    oe.tipooperacion = "1"
                    oe.estado = "A"
                    oe.codigo_ctr = olF.DecrytedString64(Request("param1").ToString) ' se envia como parametro de codigo_ctr                    
                    dt = ol.ConsultarArchivosTramite(oe)
                    Dim list As New List(Of Dictionary(Of String, Object))()
                    Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
                    Dim ctre As Integer = 0
                    Dim i As Integer = 0

                    If dt IsNot Nothing Then

                        For i = 0 To dt.Rows.Count - 1
                            Dim dict As New Dictionary(Of String, Object)()
                            dict.Add("cod", olF.EncrytedString64(dt.Rows(i).Item("codigo_ctra").ToString))
                            dict.Add("nombre", dt.Rows(i).Item("nombreFile").ToString)
                            dict.Add("ruta", dt.Rows(i).Item("rutaFile").ToString)
                            dict.Add("icono", dt.Rows(i).Item("icono").ToString)
                            list.Add(dict)
                        Next
                    End If
                    JSONresult = serializer.Serialize(list)
                Catch ex As Exception
                    'Response.Write(ex.Message)
                    Dim list As New List(Of Dictionary(Of String, Object))()
                    Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
                    Dim dict As New Dictionary(Of String, Object)()
                    dict.Add("reset", True)
                    dict.Add("msje", ex.Message & " " & ex.StackTrace)
                    list.Add(dict)
                    JSONresult = serializer.Serialize(list)
                End Try

                ' FIN TRAMITES

            ElseIf Request("param0") = "segReq" Then
                Try
                    Dim olF As New lFunciones
                    Dim oe As New eServicioCentroCosto
                    Dim ol As New lServicioCentroCosto
                    Dim dt As New DataTable
                    oe.tipooperacion = "0"
                    oe.codigo_dta = olF.DecrytedString64(Request("param1").ToString) ' se envia como parametro de codigo_ctr                    
                    dt = ol.consultarLineaDeTiempo(oe)
                    Dim list As New List(Of Dictionary(Of String, Object))()
                    Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
                    Dim ctre As Integer = 0
                    Dim i As Integer = 0

                    For i = 0 To dt.Rows.Count - 1
                        Dim dict As New Dictionary(Of String, Object)()
                        dict.Add("estado", dt.Rows(i).Item("estado_time").ToString)
                        dict.Add("aprobacion", dt.Rows(i).Item("aprobacion").ToString)
                        dict.Add("funcion", dt.Rows(i).Item("descripcion_Tfu").ToString)
                        dict.Add("fecha", dt.Rows(i).Item("fecha_timeline").ToString)
                        list.Add(dict)
                    Next

                    JSONresult = serializer.Serialize(list)
                Catch ex As Exception
                    'Response.Write(ex.Message)
                    Dim list As New List(Of Dictionary(Of String, Object))()
                    Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
                    Dim dict As New Dictionary(Of String, Object)()
                    dict.Add("reset", True)
                    dict.Add("msje", ex.Message & " " & ex.StackTrace)
                    list.Add(dict)
                    JSONresult = serializer.Serialize(list)
                End Try



            ElseIf Request("param0") = "lstAluf" Then
                Dim list As New List(Of Dictionary(Of String, Object))()
                Dim dict As New Dictionary(Of String, Object)()

                Dim olFunciones As New lFunciones
                Dim cod As String

                Dim oeIncidencia As New eIncidencia
                Dim olIncidencia As New lIncidencia
                Dim dt As New DataTable
                With oeIncidencia
                    .codigo_inc = CInt(Request("param1"))
                    .codigo_cac = Session("codigo_Cac")
                    .codigo_alu = Session("codigo_Alu")
                    .estado = "%"
                    .instancia = "%"
                End With

                dt = olIncidencia.ListaIncidenteForoAlumno(oeIncidencia)
                Dim i As Integer

                If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then

                    For i = 0 To dt.Rows.Count - 1
                        cod = olFunciones.EncrytedString64(dt.Rows(i).Item("codigo_incidencia").ToString)
                        Dim dict2 As New Dictionary(Of String, Object)()
                        dict2.Add("r", True)
                        dict2.Add("ci", dt.Rows(i).Item("codigo_incidencia"))
                        dict2.Add("asunto", dt.Rows(i).Item("asunto").ToString.ToUpper)
                        dict2.Add("fecha", dt.Rows(i).Item("fecha").ToString)
                        dict2.Add("msje", dt.Rows(i).Item("mensaje").ToString.ToUpper)
                        dict2.Add("estado", dt.Rows(i).Item("estadonom").ToString.ToUpper)
                        If dt.Rows(i).Item("adjunto").ToString = "" Then
                            dict2.Add("file", False)
                        Else
                            dict2.Add("file", False)
                            dict2.Add("url", "filesIncidentes/" & cod & "-" & dt.Rows(i).Item("adjunto").ToString)
                        End If
                        dict2.Add("download", dt.Rows(i).Item("adjunto").ToString)
                        If dt.Rows(i).Item("estado").ToString = "P" Then
                            dict2.Add("color", "label label-warning")
                        Else
                            dict2.Add("color", "label label-success")
                        End If

                        'Dim value As String = dt.Rows(i).Item("fecha").ToString
                        'Dim time As DateTime = DateTime.Parse(value)

                        'dict2.Add("dtunix", CDate(time))

                        list.Add(dict2)
                    Next
                End If

                Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
                JSONresult = serializer.Serialize(list)

            ElseIf Request("param0") = "lstAlufref" Then
                Dim list As New List(Of Dictionary(Of String, Object))()
                Dim dict As New Dictionary(Of String, Object)()
                Try

                    Dim oeIncidencia As New eIncidencia
                    Dim olIncidencia As New lIncidencia
                    Dim dt As New DataTable
                    Dim olFunciones As New lFunciones
                    Dim cod As String
                    With oeIncidencia
                        .codigo_inc = CInt(Request("param1"))
                        .codigo_IncRef = CInt(Request("param2"))
                        .estado = "%"
                        .instancia = "%"
                    End With

                    dt = olIncidencia.ListaIncidenteForoAlumnoRef(oeIncidencia)
                    Dim i As Integer

                    If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then

                        For i = 0 To dt.Rows.Count - 1
                            cod = olFunciones.EncrytedString64(dt.Rows(i).Item("incidencia_raiz").ToString)
                            Dim dict2 As New Dictionary(Of String, Object)()
                            dict2.Add("r", True)
                            dict2.Add("ci", dt.Rows(i).Item("codigo_incidencia"))
                            dict2.Add("k", dt.Rows(i).Item("calificacionRpta"))
                            dict2.Add("asunto", dt.Rows(i).Item("asunto").ToString.ToUpper)
                            dict2.Add("fecha", dt.Rows(i).Item("fecha").ToString)
                            dict2.Add("msje", dt.Rows(i).Item("mensaje").ToString.ToUpper)
                            dict2.Add("incfile", olFunciones.EncrytedString64(dt.Rows(i).Item("fileinc").ToString))
                            dict2.Add("nomfile", olFunciones.EncrytedString64(dt.Rows(i).Item("filenom").ToString))
                            dict2.Add("tf", olFunciones.EncrytedString64(dt.Rows(i).Item("fileext").ToString))
                            'EPENA 02082019{
                            If dt.Rows(i).Item("adjunto").ToString = "" Then
                                'If dt.Rows(i).Item("fileinc") = 0 Then
                                dict2.Add("file", False)
                            Else
                                dict2.Add("file", True)
                                ' dict2.Add("url", "http://serverdev/campusvirtual/personal/academico/matricula/foro/archivos/" & cod & "-" & dt.Rows(i).Item("adjunto").ToString)
                                dict2.Add("url", "filesIncidentes/" & cod & "-" & dt.Rows(i).Item("adjunto").ToString)
                            End If
                            '}EPENA 02082019
                            dict2.Add("download", dt.Rows(i).Item("adjunto").ToString)
                            If dt.Rows(i).Item("estado").ToString = "P" Then
                                dict2.Add("color", "label label-warning")
                            Else
                                dict2.Add("color", "label label-success")
                            End If
                            If dt.Rows(i).Item("instancia").ToString = "D" Then
                                dict2.Add("rev", "Direcci&oacute;n Acad&eacute;mica")
                            ElseIf dt.Rows(i).Item("instancia").ToString = "E" Then
                                dict2.Add("rev", "Direcci&oacute;n de Escuela")
                            ElseIf dt.Rows(i).Item("instancia").ToString = "S" Then
                                dict2.Add("rev", "Campus Virtual")
                            End If

                            list.Add(dict2)
                        Next
                    End If

                    Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
                    JSONresult = serializer.Serialize(list)
                Catch ex As Exception
                    dict.Add("rev", ex.Message & "--" & ex.StackTrace)
                    list.Add(dict)
                    Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
                    JSONresult = serializer.Serialize(list)
                End Try

                'rCalF
            ElseIf Request("param0") = "rCalF" Then
                Dim list As New List(Of Dictionary(Of String, Object))()
                Dim dict As New Dictionary(Of String, Object)()
                Dim oeIncidencia As New eIncidencia
                Dim olIncidencia As New lIncidencia
                Dim dt As New DataTable


                With oeIncidencia
                    .codigo_inc = CInt(Request("param1"))
                    .calificacion = CInt(Request("param2"))
                End With

                dt = olIncidencia.CalificarIncidenteForoAlumno(oeIncidencia)


                If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                    If dt.Rows(0).Item("rpta").ToString = "OK" Then
                        dict.Add("rpta", True)
                    Else
                        dict.Add("rpta", False)
                    End If

                Else
                    dict.Add("rpta", False)
                End If
                list.Add(dict)
                Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
                JSONresult = serializer.Serialize(list)

            ElseIf Request("param0") = "UpFileInc" Then
                Try
                    Dim post As HttpPostedFile = HttpContext.Current.Request.Files("UploadedImage")
                    Dim codigo As String = Request("param1")
                    Dim Numero As String = Request("param2")
                    Dim Fecha As String = CStr(CDate(Date.Today).ToString("yyyy-MM-dd"))
                    Dim Usuario As String = "USAT\"
                    'Dim Fecha As String = "#2017-09-14#"
                    'Dim Usuario As String = Session("codigo_Alu")
                    Dim Input(post.ContentLength) As Byte

                    Dim b As New BinaryReader(post.InputStream)
                    Dim binData As Byte() = b.ReadBytes(post.InputStream.Length)
                    'Dim base64 = System.Convert.ToBase64String(binData)

                    Dim wsCloud As New ClsArchivosCompartidos
                    Dim list As New Dictionary(Of String, String)
                    list.Add("Fecha", Fecha)
                    list.Add("Extencion", System.IO.Path.GetExtension(post.FileName))
                    list.Add("Nombre", System.IO.Path.GetFileName(post.FileName))
                    list.Add("TransaccionId", codigo)
                    list.Add("TablaId", "8")
                    list.Add("NroOperacion", Numero)
                    list.Add("Archivo", System.Convert.ToBase64String(binData, 0, binData.Length))
                    list.Add("Usuario", Usuario)
                    list.Add("Equipo", "CVE")
                    list.Add("Ip", "")
                    list.Add("param8", Usuario)
                    Dim envelope As String = wsCloud.SoapEnvelope(list)
                    Dim result As String = ""
                    'If _server Then
                     'result = wsCloud.PeticionRequestSoap(" http://localhost/campusvirtual/ArchivosCompartidos/SharedFiles.asmx", envelope, "http://usat.edu.pe/UploadFile", Usuario)
                   'Else
                   '    result = wsCloud.PeticionRequestSoap("http://serverdev/campusvirtual/ArchivosCompartidos/SharedFiles.asmx", envelope, "http://usat.edu.pe/UploadFile", Usuario)
                   'End If
                   result = wsCloud.PeticionRequestSoap( ConfigurationManager.AppSettings("SharedFiles"), envelope, "http://usat.edu.pe/UploadFile", Usuario)


                    Response.Write(result)
                Catch ex As Exception
                    Dim list As New List(Of Dictionary(Of String, Object))()
                    Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
                    Dim dict As New Dictionary(Of String, Object)()
                    dict.Add("r", False)
                    dict.Add("msje", ex.Message)
                    dict.Add("alert", "error")
                    list.Add(dict)
                    JSONresult = serializer.Serialize(list)
                End Try

            ElseIf Request("param0") = "DwnFileInc" Then
                Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
                Dim dict As New Dictionary(Of String, Object)()
                Dim olFunciones As New lFunciones
                Dim IdArchivo As String = ""
                Dim nombreArchivo As String = ""
                Dim tipoArchivo As String = ""
                Dim ruta As String = ""
                Dim rutadwn As String = ""

                IdArchivo = olFunciones.DecrytedString64(Request("param1"))
                nombreArchivo = olFunciones.DecrytedString64(Request("param2")).ToString
                tipoArchivo = olFunciones.DecrytedString64(Request("param3"))
                ruta = Server.MapPath("filesIncidentes/") & nombreArchivo
                'ruta = Server.MapPath("filesIncidentes/") & nombreArchivo

                'If _server Then
                '    rutadwn = "http://serverdev/campusvirtual/CampusVirtualEstudiante/CampusVirtualEstudiante/filesIncidentes/" & nombreArchivo
                'Else
                    'rutadwn = "http://localhost/campusestudiante/filesIncidentes/" & nombreArchivo
                'End If
                 rutadwn = ConfigurationManager.AppSettings("RutaCampusLocal") + "filesIncidentes/" & nombreArchivo 


                Try



                    Dim wsCloud As New ClsArchivosCompartidos
                    Dim list As New Dictionary(Of String, String)
                    Dim Usuario As String = "USAT\"
                    list.Add("IdArchivo", olFunciones.DecrytedString64(Request("param1")))
                    list.Add("Usuario", Usuario)

                    Dim envelope As String = wsCloud.SoapEnvelopeDescarga(list)
                    Dim result As String = ""
                    'If _server Then
                    '    result = wsCloud.PeticionRequestSoap("http://localhost/campusvirtual/ArchivosCompartidos/SharedFiles.asmx", envelope, "http://usat.edu.pe/DownloadFile", Usuario)
                    'Else
                    '    result = wsCloud.PeticionRequestSoap("http://serverdev/campusvirtual/ArchivosCompartidos/SharedFiles.asmx", envelope, "http://usat.edu.pe/DownloadFile", Usuario)
                    'End If
					result = wsCloud.PeticionRequestSoap(ConfigurationManager.AppSettings("SharedFiles"), envelope, "http://usat.edu.pe/UploadFile", Usuario)

                    Dim imagen As String = ResultFile(result)

                    Dim tempBytes As Byte() = Convert.FromBase64String(imagen)

                    Using fs As New FileStream(ruta, FileMode.Create)

                        fs.Write(tempBytes, 0, tempBytes.Length)

                        fs.Close()

                    End Using

                    dict.Add("r", True)
                    dict.Add("alert", "success")
                    dict.Add("msje", "Descargando archivos")
                    dict.Add("File", imagen)
                    dict.Add("url", rutadwn)
                    dict.Add("Extension", tipoArchivo.ToString.Trim)

                    JSONresult = serializer.Serialize(dict)
                Catch ex As Exception
                    dict.Add("r", False)
                    dict.Add("alert", "error")
                    dict.Add("msje", ex.Message)
                    dict.Add("File", rutadwn)
                    dict.Add("url", "")

                    JSONresult = serializer.Serialize(dict)
                End Try
            ElseIf Request("param0") = "dv" Then
                Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
                Dim dict As New Dictionary(Of String, Object)()
                Try
                    Dim dt As New DataTable
                    Dim oeDeuda As New eDeuda
                    Dim olDeuda As New lDeuda
                    oeDeuda.codigo_cac = Session("Codigo_Cac")
                    oeDeuda.alumno.codigo_Alu = Session("Codigo_Alu")
                    dt = olDeuda.ConsultaDeudaExamenRecup(oeDeuda)
                    If dt.Rows.Count > 0 Then
                        dict.Add("r", True)
                    Else
                        dict.Add("r", False)
                    End If
                    JSONresult = serializer.Serialize(dict)

                Catch ex As Exception
                    dict.Add("r", False)
                    JSONresult = serializer.Serialize(dict)
                End Try
                'Tramites Datos Adicional {
            ElseIf Request("param0") = "lstAdd" Then
                Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
                Dim list As New List(Of Dictionary(Of String, Object))()

                Try


                    Dim lstValidaTramite As New List(Of Dictionary(Of String, Object))()

                    Session("lstValidaTramite") = lstValidaTramite

                    Dim strNumSemestres As Boolean = False
                    Dim textoNumSemestres As String = ""
                    Dim dt As New DataTable
                    Dim oe As New eServicioCentroCosto
                    Dim ol As New lServicioCentroCosto
                    Dim olf As New lFunciones
                    Dim codigo_alu As String

                    Dim varcodigo_ctr As String = ""
                    varcodigo_ctr = olf.DecrytedString64(Request("param2"))


                    oe.tipooperacion = "L"
                    oe.param1 = varcodigo_ctr
                    dt = ol.DatosAdicionalesPorConceptoTramite(oe)

                    If dt.Rows.Count > 0 Then
                        codigo_alu = olf.DecrytedString64(Request("param1"))
                        For i As Integer = 0 To dt.Rows.Count - 1

                            Dim dict As New Dictionary(Of String, Object)()
                            dict.Add("r", True)

                            dict.Add("label", dt.Rows(i).Item("nombre_ctrad"))

                            If dt.Rows(i).Item("tablacolcheckbox") = 1 Then
                                dict.Add("chk", True)
                            Else
                                dict.Add("chk", False)
                            End If

                            If dt.Rows(i).Item("descripcion_ctr").ToString = "REINCORPORACIÓN" Then
                                strNumSemestres = True
                                textoNumSemestres = "semestres a reincorporar"
                            End If


                            Select Case dt.Rows(i).Item("codigo_ctrad")
                                Case 1 'SEMESTRE
                                    dict.Add("input", fnTRLsemestre(codigo_alu, dt.Rows(i).Item("tablatipo").ToString, dt.Rows(i).Item("tablacolcheckbox"), dt.Rows(i).Item("opcion"), dt.Rows(i).Item("multiple"), dt.Rows(i).Item("todo"), dt.Rows(i).Item("lectura"), varcodigo_ctr).ToString)'#EPENA 12/09/2019
                                    dict.Add("nombre", fnTRLsemestreNombre(codigo_alu, dt.Rows(i).Item("tablatipo").ToString, dt.Rows(i).Item("tablacolcheckbox")).ToString)
                                    dict.Add("param", "param1")
                                    dict.Add("text", dt.Rows(i).Item("tablacolcheckboxtext"))
                                    dict.Add("textotros", "")
                                    dict.Add("numcac", strNumSemestres)
                                    dict.Add("numcactext", textoNumSemestres)

                                Case 2 'MOTIVOS
                                    dict.Add("input", fnTRLmotivotramite(codigo_alu, dt.Rows(i).Item("tablatipo").ToString, dt.Rows(i).Item("tablacolcheckbox"), varcodigo_ctr).ToString)
                                    dict.Add("nombre", fnTRLmotivotramiteNombre(codigo_alu, dt.Rows(i).Item("tablatipo").ToString, dt.Rows(i).Item("tablacolcheckbox"), varcodigo_ctr).ToString)
                                    dict.Add("param", "param2")
                                    dict.Add("text", dt.Rows(i).Item("tablacolcheckboxtext"))
                                    dict.Add("textotros", "txtaddmotivo")
                                    dict.Add("numcac", False)
                                    dict.Add("numcactext", "")

                                Case 3 'CARRERA PROFESIONAL'
                                    dict.Add("input", fnTRLcarreraprofesional(codigo_alu, dt.Rows(i).Item("tablatipo").ToString, dt.Rows(i).Item("tablacolcheckbox"), dt.Rows(i).Item("opcion")).ToString)
                                    dict.Add("nombre", fnTRLcarreraprofesionalNombre(codigo_alu, dt.Rows(i).Item("tablatipo").ToString, dt.Rows(i).Item("tablacolcheckbox")).ToString)
                                    dict.Add("param", "param3")
                                    dict.Add("text", dt.Rows(i).Item("tablacolcheckboxtext"))
                                    dict.Add("textotros", "")
                                    dict.Add("numcac", False)
                                    dict.Add("numcactext", "")
                                Case 4 'OBSERVACION
                                    dict.Add("input", fnTRLobservacion(codigo_alu, dt.Rows(i).Item("tablatipo").ToString, dt.Rows(i).Item("tablacolcheckbox")).ToString)
                                    dict.Add("nombre", fnTRLobservacionNombre(codigo_alu, dt.Rows(i).Item("tablatipo").ToString, dt.Rows(i).Item("tablacolcheckbox")).ToString)
                                    dict.Add("param", "param4")
                                    dict.Add("text", dt.Rows(i).Item("tablacolcheckboxtext"))
                                    dict.Add("textotros", "")
                                    dict.Add("numcac", False)
                                    dict.Add("numcactext", "")
                                Case 5 'ARCHIVO
                                    dict.Add("input", fnTRLarchivo(codigo_alu, dt.Rows(i).Item("tablatipo").ToString, dt.Rows(i).Item("tablacolcheckbox")).ToString)
                                    dict.Add("nombre", fnTRLarchivoNombre(codigo_alu, dt.Rows(i).Item("tablatipo").ToString, dt.Rows(i).Item("tablacolcheckbox")).ToString)
                                    dict.Add("param", "param5")
                                    dict.Add("text", dt.Rows(i).Item("tablacolcheckboxtext"))
                                    dict.Add("textotros", "")
                                    dict.Add("numcac", False)
                                    dict.Add("numcactext", "")
                            End Select
                            list.Add(dict)
                        Next
                    End If
                    JSONresult = serializer.Serialize(list)

                Catch ex As Exception
                    Dim dict As New Dictionary(Of String, Object)()
                    dict.Add("r", False)
                    dict.Add("msje", ex.Message)
                    list.Add(dict)
                    JSONresult = serializer.Serialize(list)
                End Try

                '}Tramites Datos Adicional
            ElseIf Request("param0") = "gInscVera" Then
                Try
                    Dim olF As New lFunciones
                    Dim oe As New eDeuda
                    Dim ol As New lDeuda
                    Dim dt As New DataTable


                    oe.alumno.codigo_Alu = Session("codigo_Alu")
                    oe.codigo_cac = Session("Codigo_Cac")
                    oe.codigo_sco = 246
                    dt = ol.RegistrarDeudaInscripcionVerano(oe)

                    Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
                    Dim dict As New Dictionary(Of String, Object)()

                    If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

                        If dt.Rows(0).Item("coddeuda") > 0 Then

                            'Dim oeMat As New eMatricula
                            'Dim olMat As New lMatricula
                            'Dim dtMat As New DataTable
                            'oeMat.codigo_Alu = Session("codigo_Alu")
                            'oeMat.codigo_Cac = Session("Codigo_Cac")
                            'oeMat.codigo_sco = 246
                            'dtMat = olMat.RegistrarBitacoraCartaCompromiso(oeMat)

                            'If dtMat IsNot Nothing AndAlso dtMat.Rows.Count > 0 Then
                            '    If dtMat.Rows(0).Item("cod") > 0 Then
                            '        dict.Add("cc", dtMat.Rows(0).Item("cod"))
                            '    Else
                            '        dict.Add("cc", 0)
                            '    End If
                            'End If

                            dict.Add("sw", True)
                            dict.Add("msje", "Se generó cargo de inscripción de cursos de verano")
                            dict.Add("alert", "success")


                            Dim Email As String = ""
														
							If ConfigurationManager.AppSettings("CorreoUsatActivo") = 1 Then
								Email = Session("email")
							else
								Email = "fatima.vasquez@usat.edu.pe"
							end if
                            
                            If Email IsNot Nothing Or Email <> "" Then
                                Dim blnResultado As Boolean = False
                                Dim Asunto, mensaje, origen, destino As String
                                Dim ObjMail As New clsMailNet
                                Asunto = "Constancia de Activación de Inscripción de cursos de verano 2018 - Campus Estudiante"
                                mensaje = "<html><head><meta http-equiv='Content-Type' content='text/html; charset=iso-8859-1' /><title>Constancia de Inscripci&oacute;n de cursos de verano - Campus Estudiante</title><style type='text/css'>"
                                mensaje = mensaje + ".Estilo1 {font-family: Verdana, Arial, Helvetica, sans-serif;font-size: 12px;}</style></head><body><table width='100%' border='0'>"
                                mensaje = mensaje + "<tr><td colspan='3'>&nbsp;</td></tr><tr><td><span class='Estilo1'>Estimado(a) <b>" & Session("nombreCompleto").ToString & "</b></span></td></tr>"
                                mensaje = mensaje + "<td>&nbsp;</td><td>&nbsp;</td></tr><tr><td><br><span class='Estilo1'>El día de Hoy " + CDate(Date.Today).ToString("dddd").ToString + " " + CStr(CDate(Date.Today).ToString("dd/MM/yyyy")) + " a las " + Date.Now.ToString("hh:mm tt").ToString + " horas se ha realizado la <b>ACTIVACIÓN  de la Inscripción de Cursos de Verano</b> cuyo cargo es de <b>S/. 100</b> soles. "


                                'mensaje = mensaje + "<br><br>(*)Recuerda que de no cancelar hasta la fecha indicada la solicitud será anulada."
                                mensaje = mensaje + "</span></td><td>&nbsp;</td><td>&nbsp;</td></tr></table></body></hmtl>"
                                origen = "serviciosti@usat.edu.pe"
                                destino = Trim(Email)
                                blnResultado = ObjMail.EnviarMail("serviciosti@usat.edu.pe", "Servicios TI", destino, Asunto, mensaje, True, "Servicios TI", destino)
                                dict.Add("mail", Email)
                                If (blnResultado) Then
                                    dict.Add("envio", True)
                                Else
                                    dict.Add("envio", False)
                                End If

                            End If





                        ElseIf dt.Rows(0).Item("coddeuda") = -1 Then
                            dict.Add("sw", False)
                            dict.Add("msje", "Ya tiene generada la deuda")
                            dict.Add("alert", "warning")
                        Else
                            dict.Add("sw", False)
                            dict.Add("msje", "Problemas al generar inscripción a cursos de verano")
                            dict.Add("alert", "warning")
                        End If

                    Else
                        dict.Add("sw", False)
                        dict.Add("msje", "Problemas al generar inscripción a cursos de verano")
                        dict.Add("alert", "warning")
                    End If

                    JSONresult = serializer.Serialize(dict)
                    olF = Nothing
                    oe = Nothing
                    ol = Nothing
                    dt = Nothing

                Catch ex As Exception
                    'Response.Write(ex.Message)
                    Dim list As New List(Of Dictionary(Of String, Object))()
                    Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
                    Dim dict As New Dictionary(Of String, Object)()
                    dict.Add("reset", True)
                    dict.Add("msje", ex.Message)
                    list.Add(dict)
                    JSONresult = serializer.Serialize(list)
                End Try
            ElseIf Request("param0") = "UpFileTRL" Then
                Try
                    Dim post As HttpPostedFile = HttpContext.Current.Request.Files("UploadedImage")
                    Dim codigo As String = Request("param1")
                    Dim Numero As String = Request("param2")
                    Dim Fecha As String = CStr(CDate(Date.Today).ToString("yyyy-MM-dd"))
                    Dim Usuario As String = "USAT\"
                    'Dim Fecha As String = "#2017-09-14#"
                    'Dim Usuario As String = Session("codigo_Alu")
                    Dim Input(post.ContentLength) As Byte

                    Dim b As New BinaryReader(post.InputStream)
                    Dim binData As Byte() = b.ReadBytes(post.InputStream.Length)
                    'Dim base64 = System.Convert.ToBase64String(binData)

                    Dim wsCloud As New ClsArchivosCompartidos
                    Dim list As New Dictionary(Of String, String)
                    list.Add("Fecha", Fecha)
                    list.Add("Extencion", System.IO.Path.GetExtension(post.FileName))
                    'EPENA 25042019 {
                    'list.Add("Nombre", System.IO.Path.GetFileName(post.FileName))
                    list.Add("Nombre", Regex.Replace(System.IO.Path.GetFileName(post.FileName), "[^0-9A-Za-z._]", "_"))
                    '} EPENA 25042019
                    list.Add("TransaccionId", codigo)
                    list.Add("TablaId", "13")
                    list.Add("NroOperacion", Numero)
                    list.Add("Archivo", System.Convert.ToBase64String(binData, 0, binData.Length))
                    list.Add("Usuario", Usuario)
                    list.Add("Equipo", "CVE")
                    list.Add("Ip", Request.UserHostAddress.ToString)
                    list.Add("param8", Usuario)
                    Dim envelope As String = wsCloud.SoapEnvelope(list)
                    Dim result As String = ""

                    'If _server Then
                    '    result = wsCloud.PeticionRequestSoap("http://localhost/campusvirtual/ArchivosCompartidos/SharedFiles.asmx", envelope, "http://usat.edu.pe/UploadFile", Usuario)
                    'Else
                    '    result = wsCloud.PeticionRequestSoap("http://serverdev/campusvirtual/ArchivosCompartidos/SharedFiles.asmx", envelope, "http://usat.edu.pe/UploadFile", Usuario)
                    'End If

                    'result = wsCloud.PeticionRequestSoap("http://serverdev/campusvirtual/ArchivosCompartidos/SharedFiles.asmx", envelope, "http://usat.edu.pe/UploadFile", Usuario)
                    result = wsCloud.PeticionRequestSoap(ConfigurationManager.AppSettings("SharedFiles"), envelope, "http://usat.edu.pe/UploadFile", Usuario)
                    Response.Write(result)
                Catch ex As Exception
                    Dim list As New List(Of Dictionary(Of String, Object))()
                    Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
                    Dim dict As New Dictionary(Of String, Object)()
                    dict.Add("r", False)
                    dict.Add("msje", ex.Message)
                    dict.Add("alert", "error")
                    list.Add(dict)
                    JSONresult = serializer.Serialize(list)
                End Try

            End If
            Response.Write(JSONresult)
        End If

    End Sub



#Region "Funciones Datos Adicionales"
    Private Function fnTRLsemestre(ByVal codigo_alu As String, ByVal tablatipo As String, ByVal tablacolcheckbox As Int32, ByVal opcion As String, ByVal multiple As Int16, ByVal todo As Int16, ByVal lectura As Int16, ByVal varcodigo_ctr As Integer) As String '#EPENA 12/09/2019
        Dim html As String = ""
        Try
            Dim list As New List(Of Dictionary(Of String, Object))()
            list = Session("lstValidaTramite")

            Dim dt As New DataTable
            Dim oe As New eServicioCentroCosto
            Dim ol As New lServicioCentroCosto
            Dim olf As New lFunciones

            oe.tipooperacion = "LD"
            oe.param0 = opcion
            oe.param1 = "1"
            oe.param2 = codigo_alu
            oe.param3 = varcodigo_ctr '#EPENA 12/09/2019
            dt = ol.DatosAdicionalesPorConceptoTramite(oe)
            Dim H As Int32 = 20
            Dim L As Integer = 0

            Dim strMultiple As String = ""
            Dim strTodo As String = ""
            Dim strSoloLectura As String = ""


            Select Case tablatipo
                Case "select"
                    If multiple = 1 Then
                        L = dt.Rows.Count
                        strMultiple = "multiple='multiple' style='height:" & (H * L) & "px;'"
                    Else
                        strMultiple = ""
                        'html = "<select id='selectaddsemestre' name='selectaddsemestre' class='form-control'>"
                    End If

                    If todo = 1 Then
                        strTodo = "selected='selected'"
                    Else
                        strTodo = ""
                    End If

                    If lectura = 1 Then
                        strSoloLectura = "onfocus=""fnLectura('selectaddsemestre','s'," & multiple.ToString & ")"" onchange=""fnLectura('selectaddsemestre','s'," & multiple.ToString & ")"""
                    Else
                        strSoloLectura = ""
                    End If


                    html = "<select id='selectaddsemestre' name='selectaddsemestre'" & strSoloLectura & " class='form-control' " & strMultiple & ">"

                    ' html = "<select id='selectaddsemestre' name='selectaddsemestre' class='form-control'>"
                    For i As Integer = 0 To dt.Rows.Count - 1
                        If i = 0 Then
                            html = html & "<option value='" & dt.Rows(i).Item("codigo_Cac").ToString & "' selected='selected'>" & dt.Rows(i).Item("descripcion_Cac").ToString & "</option>"
                        Else
                            html = html & "<option value='" & dt.Rows(i).Item("codigo_Cac").ToString & "' " & strTodo & " >" & dt.Rows(i).Item("descripcion_Cac").ToString & "</option>"
                        End If
                    Next
                    html = html & "</select>"

                Case "table"
                    html = "<table id='tableaddsemestre' name='tableaddsemestre'>"
                    html = html & "<thead>"
                    If tablacolcheckbox = 1 Then
                        html = html & "<th style='width:5%'>Sel</th>"
                        html = html & "<th style='width:95%'>Semestre</th>"
                    End If

                    html = html & "</thead>"
                    html = html & "<tbody>"
                    For i As Integer = 0 To dt.Rows.Count - 1
                        If tablacolcheckbox = 1 Then
                            html = html & "<td style='width:5%'>"
                            html = html & "<input type='checkbox' />"
                            html = html & "</td>"
                        End If
                        html = html & "<td style='width:95%'>"

                        html = html & "</td>"
                    Next
                    html = html & "</tbody>"
                    html = html & "</table>"

                Case "textarea"
                    html = html & ""


            End Select

            If dt.Rows.Count = 0 Then
                Dim dict As New Dictionary(Of String, Object)()
                dict.Add("mensaje", "No presenta semestres académicos para realizar trámite")
                list.Add(dict)
                Session("lstValidaTramite") = list
            End If



            Return html
        Catch ex As Exception

            Return html
        End Try
    End Function
    Private Function fnTRLmotivotramite(ByVal codigo_alu As String, ByVal tablatipo As String, ByVal tablacolcheckbox As Int32, ByVal codigo_ctr As Integer) As String
        Dim html As String = ""
        Try

            Dim dt As New DataTable
            Dim oe As New eServicioCentroCosto
            Dim ol As New lServicioCentroCosto
            Dim olf As New lFunciones


            oe.tipooperacion = "LD"
            oe.param1 = "2"
            oe.param4 = codigo_ctr.ToString
            dt = ol.DatosAdicionalesPorConceptoTramite(oe)

            Select Case tablatipo
                Case "select"
                    html = "<select id='selectaddmotivo' name='selectaddmotivo'  class='form-control'>"
                    For i As Integer = 0 To dt.Rows.Count - 1
                        If i = 0 Then
                            html = html & "<option value='" & dt.Rows(i).Item("codigo_st").ToString & "' selected='selected'>" & dt.Rows(i).Item("nombre_st").ToString & "</option>"
                        Else
                            html = html & "<option value='" & dt.Rows(i).Item("codigo_st").ToString & "'>" & dt.Rows(i).Item("nombre_st").ToString & "</option>"
                        End If


                    Next
                    html = html & "</select>"

                Case "table"
                    html = "<table id='tableaddmotivo' name='tableaddmotivo' style='width:100%'>"
                    html = html & "<thead style='border-bottom: 1px solid black'>"
                    html = html & "<tr>"
                    If tablacolcheckbox = 1 Then
                        html = html & "<th style='width:5%;border-right: 1px solid black'>&nbsp;</th>"
                        html = html & "<th style='width:95%'>Motivo</th>"
                    End If
                    html = html & "</tr>"
                    html = html & "</thead>"
                    html = html & "<tbody>"
                    For i As Integer = 0 To dt.Rows.Count - 1
                        html = html & "<tr valign='top'>"
                        If tablacolcheckbox = 1 Then

                            html = html & "<td style='width:5%;border-right: 1px solid black'>"
                            html = html & "<input type='checkbox' id='chkaddmotivo[" & i & "]' name='chkaddmotivo[" & i & "]' c='" & dt.Rows(i).Item("codigo_st").ToString & "'style='display:block' class='form-control'/>"
                            html = html & "</td>"

                        End If
                        html = html & "<td style='width:95%'>"
                        If dt.Rows(i).Item("nombre_st").ToString = "OTROS" Then
                            html = html & "<textarea id='txtaddmotivo' name='txtaddmotivo' style='width:100%' placeholder='OTROS' class='form-control'></textarea>"
                        Else
                            html = html & dt.Rows(i).Item("nombre_st").ToString
                        End If


                        html = html & "</td>"
                        html = html & "</tr>"
                    Next
                    html = html & "</tbody>"
                    html = html & "</table>"
                    html = html & "<input type='hidden' id='numchkaddmotivo' name='numchkaddmotivo' value='" & dt.Rows.Count.ToString & "' >"

                Case "textarea"
                    html = html & ""


            End Select


            Return html
        Catch ex As Exception

            Return html
        End Try
    End Function
    Private Function fnTRLcarreraprofesional(ByVal codigo_alu As String, ByVal tablatipo As String, ByVal tablacolcheckbox As Int32, ByVal opcion As String) As String
        Dim html As String = ""
        Try
            Dim dt As New DataTable
            Dim oe As New eServicioCentroCosto
            Dim ol As New lServicioCentroCosto
            Dim olf As New lFunciones

            oe.tipooperacion = "LD"
            oe.param0 = opcion
            oe.param1 = "3"
            oe.param2 = ""
            oe.param3 = Session("codigo_test")
            dt = ol.DatosAdicionalesPorConceptoTramite(oe)

            Dim sel As Boolean = False
            Dim sel2 As Boolean = True

            Select Case tablatipo
                Case "select"
                    html = "<input type='hidden' id='vcpf' name='vcpf' value='1' /><select id='selectaddescuela' name='selectaddescuela' class='form-control'>"
                    html = html & "<option value='' selected='selected'> << SELECCIONE CARRERA PROFESIONAL >></option>"
                    For i As Integer = 0 To dt.Rows.Count - 1

                        If dt.Rows(i).Item("codigo_Cpf") <> Session("codigo_cpf") Then
                            sel = True
                        Else
                            sel = False
                        End If

                        If sel = True Then

                            If sel2 = True Then
                                html = html & "<option value='" & dt.Rows(i).Item("codigo_Cpf").ToString & "' >" & dt.Rows(i).Item("nombre_Cpf").ToString & "</option>"
                                sel2 = False
                            Else
                                html = html & "<option value='" & dt.Rows(i).Item("codigo_Cpf").ToString & "'>" & dt.Rows(i).Item("nombre_Cpf").ToString & "</option>"
                            End If

                        End If

                    Next
                    html = html & "</select>"

                Case "table"
                    html = html & ""
                Case "textarea"
                    html = html & ""
            End Select


            Return html
        Catch ex As Exception

            Return html
        End Try

    End Function
    Private Function fnTRLobservacion(ByVal codigo_alu As String, ByVal tablatipo As String, ByVal tablacolcheckbox As Int32) As String
        Dim html As String = ""
        Try
            Select Case tablatipo
                Case "textarea"
                    html = html & "<textarea id='txtaddobservacion' name='txtaddobservacion' onkeyup='fnObservacion();' style='width:100%' placeholder='OBSERVACION' class='form-control' rows='3'></textarea>"
            End Select


            Return html
        Catch ex As Exception

            Return html
        End Try

    End Function
    Private Function fnTRLarchivo(ByVal codigo_alu As String, ByVal tablatipo As String, ByVal tablacolcheckbox As Int32) As String
        Dim html As String = ""
        Try
            Select Case tablatipo
                Case "file"
                    html = html & "<input type='file' id='txtaddarchivo' name='txtaddarchivo' placeholder='Adjuntar Archivo' onchange='fnSWfile()' class='form-control' />"
            End Select


            Return html
        Catch ex As Exception

            Return html
        End Try

    End Function
    Private Function fnTRLsemestreNombre(ByVal codigo_alu As String, ByVal tablatipo As String, ByVal tablacolcheckbox As Int32) As String
        Dim nombre As String = ""
        Try

            Dim dt As New DataTable
            Dim oe As New eServicioCentroCosto
            Dim ol As New lServicioCentroCosto
            Dim olf As New lFunciones


            oe.tipooperacion = "LD"
            oe.param1 = "1"
            oe.param2 = codigo_alu
            dt = ol.DatosAdicionalesPorConceptoTramite(oe)

            Select Case tablatipo
                Case "select"
                    nombre = "selectaddsemestre"
                    
                Case "table"

                    nombre = "tableaddsemestre"


                Case "textarea"
                    nombre = "txtaddsemestre"


            End Select


            Return nombre
        Catch ex As Exception

            Return nombre
        End Try
    End Function
    Private Function fnTRLmotivotramiteNombre(ByVal codigo_alu As String, ByVal tablatipo As String, ByVal tablacolcheckbox As Int32, ByVal codigo_ctr As Integer) As String
        Dim nombre As String = ""
        Try


            Select Case tablatipo
                Case "select"
                    nombre = "selectaddmotivo"


                Case "table"
                    If tablacolcheckbox = 1 Then
                        nombre = "chkaddmotivo"
                    Else
                        nombre = "tableaddmotivo"
                    End If



                Case "textarea"
                    nombre = ""


            End Select


            Return nombre
        Catch ex As Exception

            Return nombre
        End Try
    End Function
    Private Function fnTRLcarreraprofesionalNombre(ByVal codigo_alu As String, ByVal tablatipo As String, ByVal tablacolcheckbox As Int32) As String
        Dim nombre As String = ""
        Try

            Select Case tablatipo
                Case "select"
                    nombre = "selectaddescuela"
                    

                Case "table"
                    nombre = ""
                Case "textarea"
                    nombre = ""
            End Select


            Return nombre
        Catch ex As Exception

            Return nombre
        End Try

    End Function
    Private Function fnTRLobservacionNombre(ByVal codigo_alu As String, ByVal tablatipo As String, ByVal tablacolcheckbox As Int32) As String
        Dim nombre As String = ""
        Try
            Select Case tablatipo
                Case "textarea"
                    nombre = "txtaddobservacion"
            End Select


            Return nombre
        Catch ex As Exception

            Return nombre
        End Try

    End Function
    Private Function fnTRLarchivoNombre(ByVal codigo_alu As String, ByVal tablatipo As String, ByVal tablacolcheckbox As Int32) As String
        Dim nombre As String = ""
        Try
            Select Case tablatipo
                Case "file"
                    nombre = "txtaddarchivo"
            End Select


            Return nombre
        Catch ex As Exception

            Return nombre
        End Try

    End Function
#End Region

    Private Function fnBuscarCodigoTramite(ByVal codigo_sco As Integer) As Integer

        Dim dt As Data.DataTable = CType(Session("lstServiciosTramite"), Data.DataTable)
        Dim result() As Data.DataRow = dt.Select("codigo_sco = " & codigo_sco.ToString)
        Dim codigo As Integer = 0

        For Each row As Data.DataRow In result
            codigo = CInt(row("codigo_ctr"))
        Next

        Return codigo
        'script = "fnMensaje('warning','" & ingresantes.ToString & "')"
        'fnNotificacion(script)
    End Function

    Private Function fnTieneSolicitudVirtual(ByVal codigo_sco As Integer) As Boolean

        Dim dt As Data.DataTable = CType(Session("lstServiciosTramite"), Data.DataTable)
        Dim result() As Data.DataRow = dt.Select("codigo_ctr = " & codigo_sco.ToString)


        For Each row As Data.DataRow In result
            'Response.Write(row("descripcion_ctr").ToString & "-<descripcion_ctr>")
            'Response.Write(row("tieneSolicitudVirtual").ToString & "--<tieneSolicitudVirtual>")
            If row("tieneSolicitudVirtual") Then
                Return True
            End If

        Next

        Return False
        'script = "fnMensaje('warning','" & ingresantes.ToString & "')"
        'fnNotificacion(script)
    End Function

    Private Function fnBuscarNombreTramite(ByVal codigo_ctr As Integer) As String

        Dim dt As Data.DataTable = CType(Session("lstServiciosTramite"), Data.DataTable)
        Dim result() As Data.DataRow = dt.Select("codigo_ctr = " & codigo_ctr.ToString)
        Dim nombre_ctr As String = ""

        For Each row As Data.DataRow In result
            nombre_ctr = row("descripcion_ctr")
        Next

        Return nombre_ctr
        'script = "fnMensaje('warning','" & ingresantes.ToString & "')"
        'fnNotificacion(script)
    End Function
    Function ResultFile(ByVal cadXml As String) As String
        Dim nsMgr As XmlNamespaceManager
        Dim xml As XmlDocument = New XmlDocument()
        xml.LoadXml(cadXml)
        nsMgr = New XmlNamespaceManager(xml.NameTable)
        nsMgr.AddNamespace("soap", "http://schemas.xmlsoap.org/soap/envelope/")
        Dim res As XmlNode = xml.DocumentElement.SelectSingleNode("/soap:Envelope/soap:Body", nsMgr)
        '  Dim mNombre = xml.ReadElementString("nombre")
        Return res.InnerText
        '   Response.Write("dd" + res.InnerText)
    End Function

    Private Function fnLimpiarEspaciosEnBlanco(ByVal texto As String) As String

        Dim textonuevo As String = ""
        Dim i As Integer = 0

        Try

            Dim words As String() = texto.Split(" "c)
            Dim word As String
            For Each word In words
                'System.Console.WriteLine($"<{word}>")
                textonuevo = textonuevo & word

            Next
            Return textonuevo

        Catch ex As Exception
            Return texto


        End Try
    End Function

End Class
