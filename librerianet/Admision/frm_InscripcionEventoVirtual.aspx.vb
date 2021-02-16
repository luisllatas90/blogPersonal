Imports System.Collections.Generic
Imports System.Xml
Imports System.Web.Script.Serialization
Imports System.Globalization

Partial Class Admision_frm_InscripcionEventoVirtual
    Inherits System.Web.UI.Page

#Region "Propiedades"
    'Otros
    Private mo_SOAP As New ClsAdmisionSOAP
    Private mn_Usuario As Integer = 4877 'codigo_Per
    Private ms_RutaServicio As String = ConfigurationManager.AppSettings("RutaCampusLocal") & "WSUSAT/WSUSAT.asmx"
#End Region

#Region "Eventos"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                Dim ls_CodigoEvi As String = IIf(Not String.IsNullOrEmpty(Request.Form("evi")), Request.Form("evi"), Request.QueryString("evi"))
                hddEvi.Value = mt_Decodificar(ls_CodigoEvi)
                If String.IsNullOrEmpty(hddEvi.Value) Then
                    Response.Redirect("../../sinacceso.html")
                End If
                lr_Init()
            Else
                mt_LimpiarToastServidor()
                mt_LimpiarMensajeServidor()
            End If
        Catch ex As Exception
            mt_GenerarMensajeServidor("Error", -1, ex.Message)
        End Try
    End Sub

    Protected Sub btnEnviar_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEnviar.ServerClick
        Try
            mt_ProcesarInscripcion()
        Catch ex As Exception
            mt_GenerarMensajeServidor("Error", -1, ex.Message)
        End Try
    End Sub
#End Region

#Region "Métodos"
    Private Sub lr_Init()
        Try
            mt_CargarDatosEvento(hddEvi.Value)
            mt_CargarComboTipoParticipantes("GEN", "1,3,6", "")

        Catch ex As Exception
            mt_GenerarMensajeServidor("Error", -1, ex.Message)
        End Try
    End Sub

    Private Function mt_Decodificar(ByVal ls_CodigoEvi As String) As String
        Dim base64Decoded As String = ""
        Try
            Dim data() As Byte
            data = System.Convert.FromBase64String(ls_CodigoEvi)
            base64Decoded = System.Text.ASCIIEncoding.ASCII.GetString(data)
            Return base64Decoded
        Catch ex As Exception
            mt_GenerarMensajeServidor("Error", -1, ex.Message)
        End Try
        Return base64Decoded
    End Function

    Private Sub mt_CargarDatosEvento(ByVal ln_CodigoEvi As Integer)
        Try
            Dim lo_DtDatosEvento As Data.DataTable = mt_ConsultarEventoVirtual(ln_CodigoEvi)
            If lo_DtDatosEvento.Rows.Count > 0 Then
                hddNombre.Value = lo_DtDatosEvento.Rows(0).Item("nombre_evi")
                hddUrl.Value = lo_DtDatosEvento.Rows(0).Item("url_evi")
            End If
            hTitulo.InnerHtml = hddNombre.Value

        Catch ex As Exception
            mt_GenerarMensajeServidor("Error", -1, ex.Message)
        End Try
    End Sub

    Private Sub mt_CargarComboTipoParticipantes(ByVal ls_TipoConsulta As String, ByVal ls_CodigoTpar As String, ByVal ls_DescripcionTpar As String, Optional ByVal ls_Default As String = "")
        Try
            Dim lo_Datos As New Dictionary(Of String, String)
            lo_Datos.Item("tipoConsulta") = ls_TipoConsulta
            lo_Datos.Item("codigoTpar") = ls_CodigoTpar
            lo_Datos.Item("descripcionTpar") = ls_DescripcionTpar

            Dim lo_RespuestaSOAP As New XmlDocument : lo_RespuestaSOAP.LoadXml(mo_SOAP.lr_RealizarPeticionSOAP(ms_RutaServicio, "ListarTipoParticipante", lo_Datos))

            Dim lo_Namespace As XmlNamespaceManager = New XmlNamespaceManager(lo_RespuestaSOAP.NameTable) : lo_Namespace.AddNamespace("ns", "http://tempuri.org/")
            Dim ls_RutaNodos As String = "//ns:ListarTipoParticipanteResponse/ns:ListarTipoParticipanteResult/ns:e_ListItem"
            Dim lst_Nodo As XmlNodeList = lo_RespuestaSOAP.DocumentElement.SelectNodes(ls_RutaNodos, lo_Namespace)

            Dim lo_DsTipoParticipante As New Data.DataTable
            lo_DsTipoParticipante.Columns.Add("Valor")
            lo_DsTipoParticipante.Columns.Add("Nombre")
            For Each _Nodo As XmlNode In lst_Nodo
                lo_DsTipoParticipante.Rows.Add(_Nodo.Item("Valor").InnerText.ToString, _Nodo.Item("Nombre").InnerText.ToString)
            Next
            Dim ls_Seleccione As String = IIf(String.IsNullOrEmpty(ls_Default), "-- Seleccione --", ls_Default)
            ClsFunciones.LlenarListas(cmbTipoParticipante, lo_DsTipoParticipante, "Valor", "Nombre", ls_Seleccione)
        Catch ex As Exception
            mt_GenerarMensajeServidor("Error", -1, ex.Message)
        End Try
    End Sub

    Private Sub mt_ProcesarInscripcion()
        Try
            If Not mt_ValidarForm() Then
                mt_EstadoSubmit(True)
                Exit Sub
            End If

            Dim lo_Datos As New Dictionary(Of String, String)
            With lo_Datos
                .Item("operacion") = "I"
                .Item("codigo_iev") = 0
                .Item("codigo_alu") = 0
                .Item("codigo_evi") = hddEvi.Value
                .Item("nombreCompleto_iev") = txtNombreCompleto.Value.Trim.ToUpper
                .Item("numDocIdentidad_iev") = txtDocIdentidad.Value.Trim.ToUpper
                .Item("email_iev") = txtEmail.Value.Trim.ToUpper
                .Item("celular_iev") = txtCelular.Value.Trim.ToUpper
                .Item("estaTrabajando_iev") = XmlConvert.ToString(rbtTrabajandoSi.Checked)
                .Item("empresa_iev") = txtEmpresa.Value.Trim.ToUpper
                .Item("cargo_iev") = txtCargo.Value.Trim.ToUpper

                .Item("medioIngresoLaboral_iev") = ""
                If rbtOfertaLaboralAlumni.Checked Then
                    .Item("medioIngresoLaboral_iev") = rbtOfertaLaboralAlumni.Value
                End If
                If rbtOfertaLaboralExterna.Checked Then
                    .Item("medioIngresoLaboral_iev") = rbtOfertaLaboralExterna.Value
                End If

                .Item("codigo_tpar") = cmbTipoParticipante.Value
                .Item("obtenerConstancia_iev") = XmlConvert.ToString(rbtConstanciaSi.Checked)
                .Item("medioInscripcion_iev") = "WEB"
                .Item("cod_usuario") = mn_Usuario
            End With

            Dim lo_RespuestaSOAP As New XmlDocument : lo_RespuestaSOAP.LoadXml(mo_SOAP.lr_RealizarPeticionSOAP(ms_RutaServicio, "InscripcionEventoVirtual", lo_Datos))
            Dim lo_Namespace As XmlNamespaceManager = New XmlNamespaceManager(lo_RespuestaSOAP.NameTable) : lo_Namespace.AddNamespace("ns", "http://tempuri.org/")
            Dim ls_RutaNodos As String = "//ns:InscripcionEventoVirtualResponse /ns:InscripcionEventoVirtualResult"
            Dim ls_Respuesta As String = lo_RespuestaSOAP.DocumentElement.SelectSingleNode(ls_RutaNodos, lo_Namespace).InnerText

            Dim lo_Serializer As New JavaScriptSerializer()
            Dim lo_Dict As Dictionary(Of String, String) = lo_Serializer.Deserialize(Of Dictionary(Of String, String))(ls_Respuesta)

            Dim ls_rpta As String = lo_Dict.Item("rpta")
            Dim ls_msg As String = ""
            Select Case ls_rpta
                Case "-1"
                    ls_msg = lo_Dict.Item("msg")
                Case "1"
                    ls_msg = "¡La inscripción se realizó correctamente!"
                Case "0"
                    ls_msg = lo_Dict.Item("msg")
            End Select
            mt_GenerarMensajeServidor("Respuesta", ls_rpta, ls_msg)

            If ls_rpta = "1" Then
                mt_EnviarEmail()
                mt_LimpiarForm()
            Else
                mt_EstadoSubmit(True)
            End If

        Catch ex As Exception
            mt_GenerarMensajeServidor("Error", -1, ex.Message)
        End Try
    End Sub

    Private Function mt_ValidarForm() As Boolean
        Try
            If String.IsNullOrEmpty(txtNombreCompleto.Value.Trim) Then
                mt_GenerarToastServidor(0, "Debe ingresar su nombre completo", "txtNombreCompleto")
                Return False
            End If

            If Not Regex.IsMatch(txtNombreCompleto.Value, "^[A-zÀ-ú\s]+$") Then
                mt_GenerarToastServidor(0, "El nombre solo debe contener letras", "txtNombreCompleto")
                Return False
            End If

            If String.IsNullOrEmpty(txtDocIdentidad.Value.Trim) Then
                mt_GenerarToastServidor(0, "Debe ingresar su DNI", "txtDocIdentidad")
                Return False
            End If

            If Not Regex.IsMatch(txtDocIdentidad.Value.Trim, "^\d+$") Then
                mt_GenerarToastServidor(0, "El DNI debe contener solo dígitos", "txtDocIdentidad")
                Return False
            End If

            If String.IsNullOrEmpty(txtEmail.Value.Trim) Then
                mt_GenerarToastServidor(0, "Debe ingresar su correo electrónico", "txtEmail")
                Return False
            End If

            If Not Regex.IsMatch(txtEmail.Value, "^([0-9a-zA-Z]([-\.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$") Then
                mt_GenerarToastServidor(0, "El email enviado no es válido", "txtEmail")
                Return False
            End If

            If String.IsNullOrEmpty(txtCelular.Value.Trim) Then
                mt_GenerarToastServidor(0, "Debe ingresar su número de celular", "txtCelular")
                Return False
            End If

            If Not Regex.IsMatch(txtCelular.Value.Trim, "^\d+$") Then
                mt_GenerarToastServidor(0, "El celular debe contener solo dígitos", "txtCelular")
                Return False
            End If

            If cmbTipoParticipante.Value = -1 Then
                mt_GenerarToastServidor(0, "Debe indicar qué tipo de participante es", "cmbTipoParticipante")
                Return False
            End If

            If Not (rbtConstanciaSi.Checked OrElse rbtConstanciaNo.Checked) Then
                mt_GenerarToastServidor(0, "Debe indicar si desea constancia", "rbtConstanciaSi")
                Return False
            End If

            If Not (rbtTrabajandoSi.Checked OrElse rbtTrabajandoNo.Checked) Then
                mt_GenerarToastServidor(0, "Debe indicar si se encuentra trabajando", "rbtTrabajandoSi")
                Return False
            End If

            If rbtTrabajandoSi.Checked Then
                If String.IsNullOrEmpty(txtEmpresa.Value.Trim) Then
                    mt_GenerarToastServidor(0, "Debe ingresar el nombre de la empresa donde labora", "txtEmpresa")
                    Return False
                End If

                If String.IsNullOrEmpty(txtCargo.Value.Trim) Then
                    mt_GenerarToastServidor(0, "Debe indicar el cargo que ocupa", "txtCargo")
                    Return False
                End If

                If Not (rbtOfertaLaboralAlumni.Checked OrElse rbtOfertaLaboralExterna.Checked) Then
                    mt_GenerarToastServidor(0, "Debe indicar el medio por el que ingresó a laborar", "rbtOfertaLaboralAlumni")
                    Return False
                End If
            End If

            If Not chkTerminosCondiciones.Checked Then
                mt_GenerarToastServidor(0, "Debe aceptar los términos y condiciones", "chkTerminosCondiciones")
                Return False
            End If

            Return True
        Catch ex As Exception
            mt_GenerarMensajeServidor("Error", -1, ex.Message)
        End Try
    End Function

    Private Sub mt_LimpiarForm()
        Try
            txtNombreCompleto.Value = ""
            txtDocIdentidad.Value = ""
            txtEmail.Value = ""
            txtCelular.Value = ""
            cmbTipoParticipante.Value = -1
            rbtConstanciaSi.Checked = False
            rbtConstanciaNo.Checked = False
            rbtTrabajandoSi.Checked = False
            rbtTrabajandoNo.Checked = False
            txtEmpresa.Value = ""
            txtCargo.Value = ""
            rbtOfertaLaboralAlumni.Checked = False
            rbtOfertaLaboralExterna.Checked = False
            chkTerminosCondiciones.Checked = False
            udpForm.Update()

            mt_EstadoSubmit(False)
        Catch ex As Exception
            mt_GenerarMensajeServidor("Error", -1, ex.Message)
        End Try
    End Sub

    Private Sub mt_EstadoSubmit(ByVal lb_Activo As Boolean)
        Try
            If lb_Activo Then
                btnFakeEnviar.Attributes.Remove("disabled")
            Else
                btnFakeEnviar.Attributes.Item("disabled") = "disabled"
            End If
            udpControls.Update()
        Catch ex As Exception
            mt_GenerarMensajeServidor("Error", -1, ex.Message)
        End Try
    End Sub

    Public Function mt_EnviarEmail() As Boolean
        Try
            Dim ls_Email As String = txtEmail.Value.Trim

            If String.IsNullOrEmpty(ls_Email) Then
                Return False
            End If

            Dim lo_DtDatosEvento As Data.DataTable = mt_ConsultarEventoVirtual(hddEvi.Value)
            If lo_DtDatosEvento.Rows.Count > 0 Then
                Dim ls_Fecha As String = ""
                Dim ls_NombreEvento As String = ""
                Dim ls_Url As String = ""

                With lo_DtDatosEvento.Rows(0)
                    ls_Fecha = .Item("fechaHoraInicio_evi")
                    Dim ld_Fecha As DateTime
                    If DateTime.TryParse(ls_Fecha, ld_Fecha) Then
                        ls_Fecha = ld_Fecha.ToString("dddd dd 'de' MMMM 'a horas' hh:MM tt")
                    End If

                    ls_NombreEvento = .Item("nombre_evi")
                    ls_Url = .Item("url_evi")
                End With

                Dim Mail As New ClsMail
                Dim ls_Asunto As String = "[USAT-CV] Envio de Enlace a Evento " & hddNombre.Value
                Dim ls_Mensaje As String = "<div style='font-size: 13px; font-family: Verdana,Geneva,sans-serif;'><p><b><i>&#x1F64C;&#x1F3FD;&#xA1;Gracias por inscribirte! </i></b></p>"
                ls_Mensaje &= "<p>Te esperamos el día <b>" & ls_Fecha & "</b> a la Conferencia Virtual <i>""" & ls_NombreEvento & """.</i></p>"
                ls_Mensaje &= "<p>&#x1F4CD;Para participar deber&#xE1;s ingresar al siguiente link:<br>"
                ls_Mensaje &= "<a href='" & ls_Url & "' target='_blank'>" & ls_Url & "</a><br/></p>"
                ls_Mensaje &= "<p>¡Nos vemos!</p></div>"

                Return Mail.EnviarMail("campusvirtual@usat.edu.pe", "USAT", ls_Email, ls_Asunto, ls_Mensaje, True, "", "")
            End If
            Return False

        Catch ex As Exception
            mt_GenerarMensajeServidor("Error", -1, ex.Message)
        End Try
    End Function

    Private Function mt_ConsultarEventoVirtual(ByVal ln_CodigoEvi As Integer) As Data.DataTable
        Dim lo_DsEventoVirtual As New Data.DataTable
        Try
            Dim lo_Datos As New Dictionary(Of String, String)
            lo_Datos.Item("tipoConsulta") = "GEN"
            lo_Datos.Item("codigo_evi") = ln_CodigoEvi
            lo_Datos.Item("nombre_evi") = ""
            lo_Datos.Item("nombrePonente_evi") = ""
            lo_Datos.Item("fechaHoraInicio_evi") = ""
            lo_Datos.Item("fechaHoraFin_evi") = ""
            lo_Datos.Item("url_evi") = ""
            lo_Datos.Item("tipo_evi") = ""
            lo_Datos.Item("estado_evi") = ""
            lo_Datos.Item("usuarioReg_evi") = "0"
            lo_Datos.Item("fechaHoraReg_evi") = ""
            lo_Datos.Item("usuarioMod_evi") = "0"
            lo_Datos.Item("fechaHoraMod_evi") = ""
            lo_Datos.Item("tipoOrden") = "D"
            lo_Datos.Item("tipoListado") = "C"

            Dim lo_RespuestaSOAP As New XmlDocument : lo_RespuestaSOAP.LoadXml(mo_SOAP.lr_RealizarPeticionSOAP(ms_RutaServicio, "ConsultarEventoVirtual", lo_Datos))

            Dim lo_Namespace As XmlNamespaceManager = New XmlNamespaceManager(lo_RespuestaSOAP.NameTable) : lo_Namespace.AddNamespace("ns", "http://tempuri.org/")
            Dim ls_RutaNodos As String = "//ns:ConsultarEventoVirtualResponse/ns:ConsultarEventoVirtualResult/ns:e_EventoVirtual"
            Dim lst_Nodo As XmlNodeList = lo_RespuestaSOAP.DocumentElement.SelectNodes(ls_RutaNodos, lo_Namespace)


            lo_DsEventoVirtual.Columns.Add("codigo_evi")
            lo_DsEventoVirtual.Columns.Add("nombre_evi")
            lo_DsEventoVirtual.Columns.Add("nombrePonente_evi")
            lo_DsEventoVirtual.Columns.Add("fechaHoraInicio_evi")
            lo_DsEventoVirtual.Columns.Add("fechaHoraFin_evi")
            lo_DsEventoVirtual.Columns.Add("url_evi")
            For Each _Nodo As XmlNode In lst_Nodo
                lo_DsEventoVirtual.Rows.Add(_Nodo.Item("codigo_evi").InnerText.ToString, _
                                            _Nodo.Item("nombre_evi").InnerText.ToString, _
                                            _Nodo.Item("nombrePonente_evi").InnerText.ToString, _
                                            _Nodo.Item("fechaHoraInicio_evi").InnerText.ToString, _
                                            _Nodo.Item("fechaHoraFin_evi").InnerText.ToString, _
                                            _Nodo.Item("url_evi").InnerText.ToString)
            Next
        Catch ex As Exception
            mt_GenerarMensajeServidor("Error", -1, ex.Message)
        End Try

        Return lo_DsEventoVirtual
    End Function

    Private Sub mt_GenerarMensajeServidor(ByVal ls_Titulo As String, ByVal ln_Rpta As Integer, ByVal ls_Mensaje As String)
        Try
            divMenServParametros.Attributes.Item("data-mostrar") = "true"
            divMenServParametros.Attributes.Item("data-rpta") = ln_Rpta
            udpMenServParametros.Update()

            spnMenServTitulo.InnerHtml = ls_Titulo
            udpMenServHeader.Update()

            divMenServMensaje.InnerHtml = ls_Mensaje
            udpMenServBody.Update()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub mt_LimpiarMensajeServidor()
        Try
            divMenServParametros.Attributes.Item("data-mostrar") = "false"
            divMenServParametros.Attributes.Item("data-rpta") = ""
            udpMenServParametros.Update()

            spnMenServTitulo.InnerHtml = ""
            udpMenServHeader.Update()

            divMenServMensaje.InnerHtml = ""
            udpMenServBody.Update()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub mt_GenerarToastServidor(ByVal rpta As String, ByVal msg As String, Optional ByVal control As String = "")
        Try
            hddParamsToastr.Value = "rpta=" & rpta & "|msg=" & msg & "|control=" & control
            udpParams.Update()
        Catch ex As Exception
            mt_GenerarMensajeServidor("Error", -1, ex.Message)
        End Try
    End Sub

    Private Sub mt_LimpiarToastServidor()
        Try
            hddParamsToastr.Value = ""
            udpParams.Update()
        Catch ex As Exception
            mt_GenerarMensajeServidor("Error", -1, ex.Message)
        End Try
    End Sub

#End Region

End Class
