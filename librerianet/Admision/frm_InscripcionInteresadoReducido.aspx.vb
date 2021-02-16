Imports System.Xml
Imports System.Data
Imports System.Collections.Generic
Imports System.Web.Script.Serialization
Imports System.Net
Imports System.Security.Cryptography

Partial Class frm_InscripcionInteresadoReducido
    Inherits System.Web.UI.Page

#Region "Variables"
    'Modelo
    Private mo_VariablesGlobales As New Dictionary(Of String, String)

    Private lst_Departamento As New List(Of e_ListItem)
    Private lst_Provincia As New List(Of e_ListItem)
    Private lst_Distrito As New List(Of e_ListItem)
    'Otros
    Private mo_SOAP As New ClsAdmisionSOAP
    Private mn_Usuario As Integer = 4877 '´codigo_Per
    Private mn_CodigoPai As Integer = 156 'Perú
    Private mn_DefaultCodigoDep As Integer = 13 'Por defecto: Lambayeque
    Private mn_codigoTest As Integer = 1
#End Region

#Region "Eventos"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                'Obtengo los valores de la WEB USAT
                hddTokenCco.Value = IIf(Request.Form("tokenCco") Is Nothing, "", Request.Form("tokenCco"))
                hddNombreEve.Value = IIf(Request.Form("nombreEve") Is Nothing, "", Request.Form("nombreEve"))
                hddDescripcionCac.Value = IIf(Request.Form("descripcionCac") Is Nothing, "", Request.Form("descripcionCac"))
                hddCodigoMin.Value = IIf(Request.Form("codigoMin") Is Nothing, "", Request.Form("codigoMin"))
                hddPreferente.Value = IIf(Request.Form("preferente") Is Nothing, "0", Request.Form("preferente"))
                hddNivelesEstudio.Value = IIf(Request.Form("nivelesEstudio") Is Nothing, "", Request.Form("nivelesEstudio"))
                hddCodigosIed.Value = IIf(Request.Form("codigosIed") Is Nothing, "", Request.Form("codigosIed"))
                hddCodigosCpf.Value = IIf(Request.Form("codigosCpf") Is Nothing, "", Request.Form("codigosCpf"))
                hddDatosApod.Value = IIf(Request.Form("datosApod") Is Nothing, "0", Request.Form("datosApod"))
                hddDatosProf.Value = IIf(Request.Form("datosProf") Is Nothing, "0", Request.Form("datosProf"))
                hddConsultas.Value = IIf(Request.Form("consultas") Is Nothing, "0", Request.Form("consultas"))
                hddTitulo.Value = IIf(Request.Form("titulo") Is Nothing, "", Request.Form("titulo"))

                hddTextoBtnInformacion.Value = IIf(Request.Form("textoBtnInformacion") Is Nothing, "", Request.Form("textoBtnInformacion"))

                Dim ls_MsgExitoInformacion As String = IIf(Request.Form("msgExitoInformacion") Is Nothing, "", Request.Form("msgExitoInformacion"))
                ViewState("msgExitoInformacion") = ls_MsgExitoInformacion 'Mantengo el mensaje encriptado en el estado sino da error, lo desencripto cuando lo uso

                hddCampoAdicional1.Value = IIf(Request.Form("campoAdicional1") Is Nothing, "", Request.Form("campoAdicional1"))
                hddCampoAdicional2.Value = IIf(Request.Form("campoAdicional2") Is Nothing, "", Request.Form("campoAdicional2"))
                hddCampoAdicional3.Value = IIf(Request.Form("campoAdicional3") Is Nothing, "", Request.Form("campoAdicional3"))
                hddCampoAdicional4.Value = IIf(Request.Form("campoAdicional4") Is Nothing, "", Request.Form("campoAdicional4"))
                hddCampoAdicional5.Value = IIf(Request.Form("campoAdicional5") Is Nothing, "", Request.Form("campoAdicional5"))
                hddCampoAdicional6.Value = IIf(Request.Form("campoAdicional6") Is Nothing, "", Request.Form("campoAdicional6"))

                hTitulo.InnerHtml = hddTitulo.Value
                btnFakeInformarme.InnerHtml = hddTextoBtnInformacion.Value

                divDatosApoderado.Visible = IIf(hddDatosApod.Value = "1", True, False)
                divDatosProfesion.Visible = IIf(hddDatosProf.Value = "1", True, False)
                divConsultas.Visible = IIf(hddConsultas.Value = "1", True, False)

                lr_CargarCombos()
            Else
                errorMensaje.InnerHtml = ""
                mensajeServer.Attributes.Remove("data-operacion-realizada")
                udpMensajeServer.Update()
            End If
        Catch ex As Exception
            errorMensaje.InnerHtml = ex.Message
        End Try
    End Sub

    Protected Sub cmbDepartamento_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbDepartamento.SelectedIndexChanged
        lr_CargarComboProvincias(cmbProvincia, cmbDepartamento.SelectedValue, "-- Provincia (*) --")
        udpProvincia.Update()
        lr_CargarComboDistritos(cmbDistrito, cmbProvincia.SelectedValue, "-- Distrito (*) --")
        udpDistrito.Update()
    End Sub

    Protected Sub cmbProvincia_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbProvincia.SelectedIndexChanged
        lr_CargarComboDistritos(cmbDistrito, cmbProvincia.SelectedValue, "-- Distrito (*) --")
        udpDistrito.Update()
    End Sub

    Protected Sub cmbDepartamentoInstEduc_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbDepartamentoInstEduc.SelectedIndexChanged
        Try
            lr_CargarComboInstitucionEducativa(cmbInstitucionEducativa, "DEP", cmbDepartamentoInstEduc.SelectedValue, "-- Colegio (*) --")
            udpInstitucionEducativa.Update()
        Catch ex As Exception
            errorMensaje.InnerHtml = ex.Message
        End Try
    End Sub

    Protected Sub txtEmail_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtEmail.TextChanged
        lr_BuscarEmailCoincidente(txtEmail.Text.Trim)
    End Sub

    Protected Sub btnInformarme_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnInformarme.Click
        Try
            lr_RegistrarInteresado()
        Catch ex As Exception
            errorMensaje.InnerHtml = ex.Message
            Throw ex
        End Try
    End Sub
#End Region

#Region "Metodos"
    Public Sub New()
        mo_VariablesGlobales = New Dictionary(Of String, String)

        'Origen WEB USAT
        mo_VariablesGlobales.Item("codigoOrigenWeb") = "4"

        'Origen CAMPUS USAT
        mo_VariablesGlobales.Item("codigoOrigenCampus") = "11"

        'Ruta Servicio
        mo_VariablesGlobales.Item("servicioUrl") = ConfigurationManager.AppSettings("RutaCampusLocal") & "WSUSAT/WSUSAT.asmx"
    End Sub

    Public Function ObtenerVariableGlobal(ByVal cadena As String) As Object
        Return mo_VariablesGlobales.Item(cadena)
    End Function

    Private Sub lr_CargarCombos()
        lr_CargarComboDepartamentos(cmbDepartamento, mn_CodigoPai, "-- Departamento (*) --")
        'cmbDepartamento.SelectedValue = mn_DefaultCodigoDep
        cmbDepartamento_SelectedIndexChanged(Nothing, Nothing)

        lr_CargarComboDepartamentos(cmbDepartamentoInstEduc, mn_CodigoPai, "-- Departamento de tu colegio (*) --")
        cmbDepartamentoInstEduc_SelectedIndexChanged(Nothing, Nothing)

        lr_CargarComboCarreraProfesional(cmbCarreraProfesional, "-- Carrera Profesional (*) --")

        If hddNivelesEstudio.Value.Trim.Length > 0 Then
            Dim lo_NivelesEstudio() As String = hddNivelesEstudio.Value.Split(",")
            For Each lo_Item As ListItem In cmbAnioEstudio.Items()
                Dim lb_Agregar As Boolean = False
                For Each ls_NivelEstudio As String In lo_NivelesEstudio
                    lb_Agregar = (lo_Item.Value = "-1" OrElse ls_NivelEstudio.Trim = lo_Item.Value.Trim)
                    If lb_Agregar Then Exit For
                Next
                lo_Item.Enabled = lb_Agregar
            Next
        End If
    End Sub

    Private Sub lr_CargarComboDepartamentos(ByVal cmbCombo As DropDownList, ByVal ls_CodigoPai As String, Optional ByVal ls_Default As String = "")
        Dim lo_Datos As New Dictionary(Of String, String) : lo_Datos.Add("codigoPai", ls_CodigoPai)
        Dim lo_RespuestaSOAP As New XmlDocument : lo_RespuestaSOAP.LoadXml(mo_SOAP.lr_RealizarPeticionSOAP(ObtenerVariableGlobal("servicioUrl"), "ListarDepartamento", lo_Datos))

        Dim lo_Namespace As XmlNamespaceManager = New XmlNamespaceManager(lo_RespuestaSOAP.NameTable) : lo_Namespace.AddNamespace("ns", "http://tempuri.org/")
        Dim ls_RutaNodos As String = "//ns:ListarDepartamentoResponse/ns:ListarDepartamentoResult/ns:e_ListItem"
        Dim lst_Nodo As XmlNodeList = lo_RespuestaSOAP.DocumentElement.SelectNodes(ls_RutaNodos, lo_Namespace)

        Dim lo_DsDepartamento As New Data.DataTable
        lo_DsDepartamento.Columns.Add("Valor")
        lo_DsDepartamento.Columns.Add("Nombre")
        For Each _Nodo As XmlNode In lst_Nodo
            lo_DsDepartamento.Rows.Add(_Nodo.Item("Valor").InnerText.ToString, _Nodo.Item("Nombre").InnerText.ToString)
        Next
        Dim ls_Seleccione As String = IIf(String.IsNullOrEmpty(ls_Default), "-- Seleccione --", ls_Default)
        ClsFunciones.LlenarListas(cmbCombo, lo_DsDepartamento, "Valor", "Nombre", ls_Seleccione)
    End Sub

    Private Sub lr_CargarComboProvincias(ByVal cmbCombo As DropDownList, ByVal ls_CodigoDep As String, Optional ByVal ls_Default As String = "")
        Dim lo_Datos As New Dictionary(Of String, String) : lo_Datos.Add("codigoDep", ls_CodigoDep)
        Dim lo_RespuestaSOAP As New XmlDocument : lo_RespuestaSOAP.LoadXml(mo_SOAP.lr_RealizarPeticionSOAP(ObtenerVariableGlobal("servicioUrl"), "ListarProvincia", lo_Datos))

        Dim lo_Namespace As XmlNamespaceManager = New XmlNamespaceManager(lo_RespuestaSOAP.NameTable) : lo_Namespace.AddNamespace("ns", "http://tempuri.org/")
        Dim ls_RutaNodos As String = "//ns:ListarProvinciaResponse/ns:ListarProvinciaResult/ns:e_ListItem"
        Dim lst_Nodo As XmlNodeList = lo_RespuestaSOAP.DocumentElement.SelectNodes(ls_RutaNodos, lo_Namespace)

        Dim lo_DsProvincia As New Data.DataTable
        lo_DsProvincia.Columns.Add("Valor")
        lo_DsProvincia.Columns.Add("Nombre")
        For Each _Nodo As XmlNode In lst_Nodo
            lo_DsProvincia.Rows.Add(_Nodo.Item("Valor").InnerText.ToString, _Nodo.Item("Nombre").InnerText.ToString)
        Next
        Dim ls_Seleccione As String = IIf(String.IsNullOrEmpty(ls_Default), "-- Seleccione --", ls_Default)
        ClsFunciones.LlenarListas(cmbCombo, lo_DsProvincia, "Valor", "Nombre", ls_Seleccione)
    End Sub

    Private Sub lr_CargarComboDistritos(ByVal cmbCombo As DropDownList, ByVal ls_CodigoPro As String, Optional ByVal ls_Default As String = "")
        Dim lo_Datos As New Dictionary(Of String, String) : lo_Datos.Add("codigoPro", ls_CodigoPro)
        Dim lo_RespuestaSOAP As New XmlDocument : lo_RespuestaSOAP.LoadXml(mo_SOAP.lr_RealizarPeticionSOAP(ObtenerVariableGlobal("servicioUrl"), "ListarDistrito", lo_Datos))

        Dim lo_Namespace As XmlNamespaceManager = New XmlNamespaceManager(lo_RespuestaSOAP.NameTable) : lo_Namespace.AddNamespace("ns", "http://tempuri.org/")
        Dim ls_RutaNodos As String = "//ns:ListarDistritoResponse/ns:ListarDistritoResult/ns:e_ListItem"
        Dim lst_Nodo As XmlNodeList = lo_RespuestaSOAP.DocumentElement.SelectNodes(ls_RutaNodos, lo_Namespace)

        Dim lo_DsDistrito As New Data.DataTable
        lo_DsDistrito.Columns.Add("Valor")
        lo_DsDistrito.Columns.Add("Nombre")
        For Each _Nodo As XmlNode In lst_Nodo
            lo_DsDistrito.Rows.Add(_Nodo.Item("Valor").InnerText.ToString, _Nodo.Item("Nombre").InnerText.ToString)
        Next
        Dim ls_Seleccione As String = IIf(String.IsNullOrEmpty(ls_Default), "-- Seleccione --", ls_Default)
        ClsFunciones.LlenarListas(cmbCombo, lo_DsDistrito, "Valor", "Nombre", ls_Seleccione)
    End Sub

    Private Sub lr_CargarComboInstitucionEducativa(ByVal cmbCombo As DropDownList, ByVal ls_Tipo As String, ByVal ls_Codigo As String, Optional ByVal ls_Default As String = "")
        Try
            Dim lo_Datos As New Dictionary(Of String, String) : lo_Datos.Add("tipo", ls_Tipo) : lo_Datos.Add("codigo", ls_Codigo)
            Dim lo_RespuestaSOAP As New XmlDocument : lo_RespuestaSOAP.LoadXml(mo_SOAP.lr_RealizarPeticionSOAP(ObtenerVariableGlobal("servicioUrl"), "ListarInstitucionEducativa", lo_Datos))

            Dim lo_Namespace As XmlNamespaceManager = New XmlNamespaceManager(lo_RespuestaSOAP.NameTable) : lo_Namespace.AddNamespace("ns", "http://tempuri.org/")
            Dim ls_RutaNodos As String = "//ns:ListarInstitucionEducativaResponse/ns:ListarInstitucionEducativaResult/ns:e_ListItem"
            Dim lst_Nodo As XmlNodeList = lo_RespuestaSOAP.DocumentElement.SelectNodes(ls_RutaNodos, lo_Namespace)

            Dim lo_DsInstitucionEducativa As New Data.DataTable
            lo_DsInstitucionEducativa.Columns.Add("Valor")
            lo_DsInstitucionEducativa.Columns.Add("Nombre")
            lo_DsInstitucionEducativa.Columns.Add("Direccion")
            lo_DsInstitucionEducativa.Columns.Add("Distrito")

            'Filtro por codigosIed y por convenio
            Dim lo_CodigosIed() As String = hddCodigosIed.Value.Split(",")
            For Each _Nodo As XmlNode In lst_Nodo
                Dim lb_Agregar As Boolean = False
                For i As Integer = 0 To lo_CodigosIed.Length - 1
                    If String.IsNullOrEmpty(hddCodigosIed.Value) OrElse _Nodo.Item("Valor").InnerText.ToString.Trim = lo_CodigosIed(i).Trim Then
                        lb_Agregar = True
                        Exit For
                    End If
                Next
                If hddPreferente.Value = "1" AndAlso _Nodo.Item("Adicional3").InnerText.ToString.Trim <> hddPreferente.Value Then
                    lb_Agregar = False
                End If

                If lb_Agregar Then
                    lo_DsInstitucionEducativa.Rows.Add(_Nodo.Item("Valor").InnerText.ToString, _Nodo.Item("Nombre").InnerText.ToString, _Nodo.Item("Adicional").InnerText.ToString, _Nodo.Item("Adicional2").InnerText.ToString)
                End If
            Next
            Dim ls_Seleccione As String = IIf(String.IsNullOrEmpty(ls_Default), "-- Seleccione --", ls_Default)
            ClsFunciones.LlenarListas(cmbCombo, lo_DsInstitucionEducativa, "Valor", "Nombre", ls_Seleccione)

            Dim dr() As System.Data.DataRow
            For Each item As ListItem In cmbInstitucionEducativa.Items
                dr = lo_DsInstitucionEducativa.Select("Valor='" & item.Value & "'")
                If dr.Length > 0 Then
                    Dim lo_DrInstEducativa As Data.DataRow = dr(0)
                    item.Attributes.Add("data-tokens", lo_DrInstEducativa.Item("Nombre").ToString)
                    Dim ls_Subtext As String = ""
                    If Not String.IsNullOrEmpty(lo_DrInstEducativa.Item("Direccion")) Then
                        ls_Subtext &= " | " & lo_DrInstEducativa.Item("Direccion")
                    End If
                    If Not String.IsNullOrEmpty(lo_DrInstEducativa.Item("Distrito")) Then
                        ls_Subtext &= " - <b>" & lo_DrInstEducativa.Item("Distrito") & "</b>"
                    End If
                    item.Attributes.Add("data-subtext", ls_Subtext)
                End If
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub lr_CargarComboCarreraProfesional(ByVal cmbCombo As DropDownList, Optional ByVal ls_Default As String = "")
        Dim lo_RespuestaSOAP As New XmlDocument : lo_RespuestaSOAP.LoadXml(mo_SOAP.lr_RealizarPeticionSOAP(ObtenerVariableGlobal("servicioUrl"), "ListarCarreraProfesional"))

        Dim lo_Namespace As XmlNamespaceManager = New XmlNamespaceManager(lo_RespuestaSOAP.NameTable) : lo_Namespace.AddNamespace("ns", "http://tempuri.org/")
        Dim ls_RutaNodos As String = "//ns:ListarCarreraProfesionalResponse/ns:ListarCarreraProfesionalResult/ns:e_ListItem"
        Dim lst_Nodo As XmlNodeList = lo_RespuestaSOAP.DocumentElement.SelectNodes(ls_RutaNodos, lo_Namespace)

        Dim lo_DsCarreraProfesional As New Data.DataTable
        lo_DsCarreraProfesional.Columns.Add("Valor")
        lo_DsCarreraProfesional.Columns.Add("Nombre")

        'Filtro por codigosCpf
        Dim lo_CodigosCpf() As String = hddCodigosCpf.Value.Split(",")
        For Each _Nodo As XmlNode In lst_Nodo
            For i As Integer = 0 To lo_CodigosCpf.Length - 1
                If String.IsNullOrEmpty(hddCodigosCpf.Value) OrElse _Nodo.Item("Valor").InnerText.ToString.Trim = lo_CodigosCpf(i).Trim Then
                    lo_DsCarreraProfesional.Rows.Add(_Nodo.Item("Valor").InnerText.ToString, _Nodo.Item("Nombre").InnerText.ToString)
                    Exit For
                End If
            Next
        Next
        Dim ls_Seleccione As String = IIf(String.IsNullOrEmpty(ls_Default), "-- Seleccione --", ls_Default)
        ClsFunciones.LlenarListas(cmbCombo, lo_DsCarreraProfesional, "Valor", "Nombre", ls_Seleccione)
    End Sub

    Private Sub lr_BuscarEmailCoincidente(ByVal email As String)
        Dim lo_Datos As New Dictionary(Of String, String) : lo_Datos.Add("email", email)
        Dim lo_RespuestaSOAP As New XmlDocument : lo_RespuestaSOAP.LoadXml(mo_SOAP.lr_RealizarPeticionSOAP(ObtenerVariableGlobal("servicioUrl"), "BuscarEmailCoincidente", lo_Datos))

        Dim lo_Namespace As XmlNamespaceManager = New XmlNamespaceManager(lo_RespuestaSOAP.NameTable) : lo_Namespace.AddNamespace("ns", "http://tempuri.org/")
        Dim ls_RutaNodos As String = "//ns:BuscarEmailCoincidenteResponse/ns:BuscarEmailCoincidenteResult/ns:e_ListItem"
        Dim lst_Nodo As XmlNodeList = lo_RespuestaSOAP.DocumentElement.SelectNodes(ls_RutaNodos, lo_Namespace)

        Dim coincide As Boolean = False
        Dim verificado As Boolean = False

        For Each _nodo As XmlNode In lst_Nodo
            'Response.Write(_nodo.InnerText)
            coincide = True
            If Not String.IsNullOrEmpty(_nodo.Item("Valor").InnerText) Then
                verificado = _nodo.Item("Valor").InnerText
                If verificado Then
                    Exit For
                End If
            End If
        Next
        hddEmailCoincidente.Value = IIf(coincide, "1", "0")
        hddEmailVerificado.Value = IIf(verificado, "1", "0")

        udpEmail.Update()
    End Sub

    Private Sub lr_RegistrarInteresado()
        Try
            Dim lo_ValidacionFormulario As Dictionary(Of String, String) = lf_ValidarFormulario()
            If lo_ValidacionFormulario.Item("rpta") <> "1" Then
                lr_GenerarMensajesValidacion(lo_ValidacionFormulario.Item("rpta"), lo_ValidacionFormulario.Item("msg"), lo_ValidacionFormulario.Item("control"))
            Else
                Dim corigoOri As Integer = ObtenerVariableGlobal("codigoOrigenWeb")

                Dim lo_Datos As New Dictionary(Of String, String)
                With lo_Datos
                    .Add("codigoOri", corigoOri)
                    .Add("tokenCco", hddTokenCco.Value)
                    .Add("nombreEve", hddNombreEve.Value)
                    .Add("descripcionCac", hddDescripcionCac.Value)
                    .Add("codigoTest", mn_codigoTest)
                    .Add("codigoMin", hddCodigoMin.Value)
                    .Add("codigoDoci", "1")
                    .Add("numerodocInt", txtDNI.Text.Trim)
                    .Add("apepaternoInt", UCase(txtApellidoPaterno.Text.Trim))
                    .Add("apematernoInt", UCase(txtApellidoMaterno.Text.Trim))
                    .Add("nombresInt", UCase(txtNombres.Text.Trim))
                    .Add("fechanacimientoInt", dtpFecNacimiento.Text.Trim)
                    '.Add("sexoPso", cmbSexo.SelectedValue)
                    .Add("gradoInt", cmbAnioEstudio.SelectedValue)
                    .Add("codigoIed", cmbInstitucionEducativa.SelectedValue)
                    .Add("codigoCpf", cmbCarreraProfesional.SelectedValue)
                    .Add("estadoInt", "1")
                    .Add("telNumeroTei", txtNumFijo.Text.Trim)
                    .Add("celNumeroTei", txtNumCelular.Text.Trim)
                    .Add("descripcionEmi", UCase(txtEmail.Text.Trim))
                    .Add("codigoDep", cmbDepartamento.SelectedValue)
                    .Add("codigoPro", cmbProvincia.SelectedValue)
                    .Add("codigoDis", cmbDistrito.SelectedValue)
                    .Add("direccionDin", UCase(txtDireccion.Text.Trim))
                    .Add("numerodocFin", UCase(txtDNIApoderado.Text.Trim))
                    .Add("apepaternoFin", UCase(txtApePatApoderado.Text.Trim))
                    .Add("apematernoFin", UCase(txtApeMatApoderado.Text.Trim))
                    .Add("nombresFin", UCase(txtNombresApoderado.Text.Trim))
                    .Add("celularFin", UCase(txtNumCelApoderado.Text.Trim))
                    .Add("emailFin", UCase(txtEmailApoderado.Text.Trim))
                    .Add("consulta", UCase(txtConsultas.Text.Trim))
                    .Add("usuarioReg", mn_Usuario)
                    .Add("verificadoEmi", hddEmailVerificado.Value)
                End With
                
                Dim ls_NombreNodo As String = "GuardarInteresado"
                Dim lo_RespuestaSOAP As New XmlDocument : lo_RespuestaSOAP.LoadXml(mo_SOAP.lr_RealizarPeticionSOAP(ObtenerVariableGlobal("servicioUrl"), ls_NombreNodo, lo_Datos))
                Dim lo_Namespace As XmlNamespaceManager = New XmlNamespaceManager(lo_RespuestaSOAP.NameTable) : lo_Namespace.AddNamespace("ns", "http://tempuri.org/")
                Dim ls_RutaNodos As String = "//ns:" & ls_NombreNodo & "Response /ns:" & ls_NombreNodo & "Result"
                Dim ls_CodigoInt As String = lo_RespuestaSOAP.DocumentElement.SelectSingleNode(ls_RutaNodos, lo_Namespace).InnerText

                Dim lo_Serializer As New JavaScriptSerializer()
                Dim lo_Dict As Dictionary(Of String, String) = lo_Serializer.Deserialize(Of Dictionary(Of String, String))(ls_CodigoInt)

                Dim ls_Mensaje As String = ""
                errorMensaje.InnerHtml = lo_Dict.Item("msg")

                Select Case lo_Dict.Item("rpta")
                    Case "-1"
                        ls_Mensaje = "No se pudo registrar la información"
                    Case "1"
                        ls_Mensaje = DesencriptaTexto(ViewState("msgExitoInformacion"))
                        lr_ProcesoMarketing("CONSULTA")
                        lr_LimpiarFormulario()
                    Case "0"
                        ls_Mensaje = lo_Dict.Item("msg")
                End Select
                lr_GenerarMensajes(lo_Dict.Item("rpta"), ls_Mensaje)
            End If
        Catch ex As Exception
            lr_GenerarMensajes("-1", "Ha ocurrido un error en el servidor")
            errorMensaje.InnerHtml = ex.Message
            Throw ex
        End Try
    End Sub

    Private Function lf_ValidarFormulario() As Dictionary(Of String, String)
        Try
            Dim lo_Resultado As New Dictionary(Of String, String)
            lo_Resultado.Add("rpta", "1")
            lo_Resultado.Add("msg", "")
            lo_Resultado.Add("control", "")

            If String.IsNullOrEmpty(txtDNI.Text.Trim) Then
                lo_Resultado.Item("rpta") = "0"
                lo_Resultado.Item("msg") = "Debe ingresar un número de DNI"
                lo_Resultado.Item("control") = "txtDNI"
                Return lo_Resultado
            End If

            If String.IsNullOrEmpty(txtApellidoPaterno.Text.Trim) Then
                lo_Resultado.Item("rpta") = "0"
                lo_Resultado.Item("msg") = "Debe ingresar un apellido paterno"
                lo_Resultado.Item("control") = "txtApellidoPaterno"
                Return lo_Resultado
            End If

            If String.IsNullOrEmpty(txtApellidoMaterno.Text.Trim) Then
                lo_Resultado.Item("rpta") = "0"
                lo_Resultado.Item("msg") = "Debe ingresar un apellido materno"
                lo_Resultado.Item("control") = "txtApellidoMaterno"
                Return lo_Resultado
            End If

            If String.IsNullOrEmpty(txtNombres.Text.Trim) Then
                lo_Resultado.Item("rpta") = "0"
                lo_Resultado.Item("msg") = "Debe ingresar sus nombres"
                lo_Resultado.Item("control") = "txtNombres"
                Return lo_Resultado
            End If

            'If cmbSexo.SelectedIndex = 0 Then
            '    lo_Resultado.Item("rpta") = "0"
            '    lo_Resultado.Item("msg") = "Debe seleccionar un sexo"
            '    lo_Resultado.Item("control") = "cmbSexo"
            '    Return lo_Resultado
            'End If

            If String.IsNullOrEmpty(txtNumCelular.Text.Trim) Then
                lo_Resultado.Item("rpta") = "0"
                lo_Resultado.Item("msg") = "Debe ingresar un número de celular"
                lo_Resultado.Item("control") = "txtNumCelular"
                Return lo_Resultado
            End If

            If String.IsNullOrEmpty(txtEmail.Text.Trim) Then
                lo_Resultado.Item("rpta") = "0"
                lo_Resultado.Item("msg") = "Debe ingresar un correo electrónico"
                lo_Resultado.Item("control") = "txtEmail"
                Return lo_Resultado
            End If

            Dim ls_FormatEmail As String = "^([0-9a-zA-Z]([-\.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$"
            If Not Regex.IsMatch(txtEmail.Text.Trim, ls_FormatEmail) Then
                lo_Resultado.Item("rpta") = "0"
                lo_Resultado.Item("msg") = "El correo electrónico no es válido"
                lo_Resultado.Item("control") = "txtEmail"
                Return lo_Resultado
            End If

            'If String.IsNullOrEmpty(txtDireccion.Text.Trim) Then
            '    lo_Resultado.Item("rpta") = "0"
            '    lo_Resultado.Item("msg") = "Debe ingresar una dirección"
            '    lo_Resultado.Item("control") = "txtDireccion"
            '    Return lo_Resultado
            'End If

            'If cmbDistrito.SelectedValue < 0 Then
            '    lo_Resultado.Item("rpta") = "0"
            '    lo_Resultado.Item("msg") = "Debe seleccionar un distrito"
            '    lo_Resultado.Item("control") = "cmbDistrito"
            '    Return lo_Resultado
            'End If

            If cmbInstitucionEducativa.SelectedValue < 0 Then
                lo_Resultado.Item("rpta") = "0"
                lo_Resultado.Item("msg") = "Debe seleccionar un colegio"
                lo_Resultado.Item("control") = "cmbInstitucionEducativa"
                Return lo_Resultado
            End If

            If cmbCarreraProfesional.SelectedValue < 0 Then
                lo_Resultado.Item("rpta") = "0"
                lo_Resultado.Item("msg") = "Debe seleccionar una carrera profesional"
                lo_Resultado.Item("control") = "cmbCarreraProfesional"
                Return lo_Resultado
            End If

            If Not chkTerminosCondiciones.Checked Then
                lo_Resultado.Item("rpta") = "0"
                lo_Resultado.Item("msg") = "Debe aceptar los términos y condiciones"
                lo_Resultado.Item("control") = "chkTerminosCondiciones"
                Return lo_Resultado
            End If

            Return lo_Resultado

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub lr_GenerarMensajes(ByVal ls_TipoMensaje As String, ByVal ls_Mensaje As String)
        mensajeServer.InnerHtml = ls_Mensaje
        Select Case ls_TipoMensaje
            Case "-1"
                mensajeServer.Attributes.Item("class") = "alert alert-danger"
            Case "1"
                mensajeServer.Attributes.Item("class") = ""
            Case "0"
                mensajeServer.Attributes.Item("aclass") = "alert alert-warning"
        End Select
        mensajeServer.Attributes.Item("data-operacion-realizada") = "true"
        udpMensajeServer.Update()
    End Sub

    Private Sub lr_GenerarMensajesValidacion(ByVal ls_TipoMensaje As String, ByVal ls_Mensaje As String, ByVal ls_Control As String)
        lr_GenerarMensajes(ls_TipoMensaje, ls_Mensaje)
        If mensajeServer.Attributes.Item("data-control") Is Nothing Then
            mensajeServer.Attributes.Add("data-control", ls_Control)
        Else
            mensajeServer.Attributes.Item("data-control") = ls_Control
        End If
        mensajeServer.Attributes.Item("data-operacion-realizada") = True
        udpMensajeServer.Update()
    End Sub

    Private Sub lr_ProcesoMarketing(ByVal ls_Tipo As String)
        Try
            'Obtengo datos adicionales de la institución educativa
            Dim departamentoIed As String = ""
            Dim provinciaIed As String = ""
            Dim distritoIed As String = ""

            Dim lo_Datos As New Dictionary(Of String, String)
            With lo_Datos
                .Add("tipoConsulta", "GEN")
                .Add("codigoIed", cmbInstitucionEducativa.SelectedValue)
            End With

            Dim ls_NombreNodo As String = "ConsultarInstitucionEducativa"
            Dim lo_RespuestaSOAP As New XmlDocument : lo_RespuestaSOAP.LoadXml(mo_SOAP.lr_RealizarPeticionSOAP(ObtenerVariableGlobal("servicioUrl"), ls_NombreNodo, lo_Datos))
            Dim lo_Namespace As XmlNamespaceManager = New XmlNamespaceManager(lo_RespuestaSOAP.NameTable) : lo_Namespace.AddNamespace("ns", "http://tempuri.org/")
            Dim ls_RutaNodos As String = "//ns:" & ls_NombreNodo & "Response /ns:" & ls_NombreNodo & "Result"
            Dim ls_Respuesta As String = lo_RespuestaSOAP.DocumentElement.SelectSingleNode(ls_RutaNodos, lo_Namespace).InnerText

            Dim lo_Serializer As New JavaScriptSerializer()
            Dim lo_Result As List(Of Dictionary(Of String, Object)) = lo_Serializer.Deserialize(Of List(Of Dictionary(Of String, Object)))(ls_Respuesta)

            If lo_Result.Count > 0 Then
                If lo_Result(0).Item("codigo_ied") <> "-1" Then
                    departamentoIed = lo_Result(0).Item("nombre_dep")
                    provinciaIed = lo_Result(0).Item("nombre_pro")
                    distritoIed = lo_Result(0).Item("nombre_dis")
                End If
            End If
            '----------------

            Using _client As New Net.WebClient
                Dim lo_Credentials As New NetworkCredential("marketing", "USAT2015")
                _client.Credentials = lo_Credentials
                Dim lo_ReqParam As New Specialized.NameValueCollection
                lo_ReqParam.Add("dniApoderado", txtDNIApoderado.Text.Trim)
                lo_ReqParam.Add("apePatApoderado", txtApePatApoderado.Text.Trim)
                lo_ReqParam.Add("apeMatApoderado", txtApeMatApoderado.Text.Trim)
                lo_ReqParam.Add("nombresApoderado", txtNombresApoderado.Text.Trim)
                lo_ReqParam.Add("numCelApoderado", txtNumCelApoderado.Text.Trim)
                lo_ReqParam.Add("emailApoderado", txtEmailApoderado.Text.Trim)
                lo_ReqParam.Add("dni", txtDNI.Text.Trim)
                lo_ReqParam.Add("apellidoPaterno", txtApellidoPaterno.Text.Trim)
                lo_ReqParam.Add("apellidoMaterno", txtApellidoMaterno.Text.Trim)
                lo_ReqParam.Add("nombres", txtNombres.Text.Trim)
                lo_ReqParam.Add("numCelular", txtNumCelular.Text.Trim)
                lo_ReqParam.Add("numFijo", txtNumFijo.Text.Trim)
                lo_ReqParam.Add("email", txtEmail.Text.Trim)
                lo_ReqParam.Add("direccion", txtDireccion.Text.Trim)

                If cmbDepartamento.SelectedValue <> -1 Then
                    lo_ReqParam.Add("departamento", cmbDepartamento.SelectedItem.Text)
                Else
                    lo_ReqParam.Add("departamento", "")
                End If

                If cmbProvincia.SelectedValue <> -1 Then
                    lo_ReqParam.Add("provincia", cmbProvincia.SelectedItem.Text)
                Else
                    lo_ReqParam.Add("provincia", "")
                End If

                If cmbDistrito.SelectedValue <> -1 Then
                    lo_ReqParam.Add("distrito", cmbDistrito.SelectedItem.Text)
                Else
                    lo_ReqParam.Add("distrito", "")
                End If

                lo_ReqParam.Add("fecNacimiento", dtpFecNacimiento.Text.Trim)

                If cmbSexo.SelectedValue <> -1 Then
                    lo_ReqParam.Add("sexo", cmbSexo.SelectedItem.Text)
                Else
                    lo_ReqParam.Add("sexo", "")
                End If

                If cmbAnioEstudio.SelectedValue <> "-1" Then
                    lo_ReqParam.Add("anioEstudio", cmbAnioEstudio.SelectedValue)
                Else
                    lo_ReqParam.Add("anioEstudio", "")
                End If

                lo_ReqParam.Add("centroLabores", txtCentroLabores.Text.Trim)
                lo_ReqParam.Add("cargo", txtCargo.Text.Trim)
                lo_ReqParam.Add("ruc", txtRuc.Text.Trim)
                lo_ReqParam.Add("departamentoInstEduc", departamentoIed)
                lo_ReqParam.Add("provinciaInstEduc", provinciaIed)
                lo_ReqParam.Add("distritoInstEduc", distritoIed)
                lo_ReqParam.Add("institucionEducativa", cmbInstitucionEducativa.SelectedItem.Text)
                lo_ReqParam.Add("carreraProfesional", cmbCarreraProfesional.SelectedItem.Text)
                lo_ReqParam.Add("consultas", txtConsultas.Text.Trim)
                lo_ReqParam.Add("tipo", ls_Tipo)
                lo_ReqParam.Add("campoAdicional1", hddCampoAdicional1.Value)
                lo_ReqParam.Add("campoAdicional2", hddCampoAdicional2.Value)
                lo_ReqParam.Add("campoAdicional3", hddCampoAdicional3.Value)
                lo_ReqParam.Add("campoAdicional4", hddCampoAdicional4.Value)
                lo_ReqParam.Add("campoAdicional5", hddCampoAdicional5.Value)
                lo_ReqParam.Add("campoAdicional6", hddCampoAdicional6.Value)
                Dim lo_ResponseBytes As Byte() = _client.UploadValues("http://www.tuproyectodevida.pe/autorespuesta/auto_prueba.php", "POST", lo_ReqParam)
                Dim lo_ResponseBody As String = (New Text.UTF8Encoding).GetString(lo_ResponseBytes)
            End Using
        Catch ex As Exception
            'Throw ex
        End Try
    End Sub

    Private Sub lr_LimpiarFormulario()
        'Datos apoderado
        txtDNIApoderado.Text = ""
        txtApePatApoderado.Text = ""
        txtApeMatApoderado.Text = ""
        txtNombresApoderado.Text = ""
        txtNumCelApoderado.Text = ""
        txtEmailApoderado.Text = ""
        'Datos personales
        txtDNI.Text = ""
        txtApellidoPaterno.Text = ""
        txtApellidoMaterno.Text = ""
        txtNombres.Text = ""
        txtNumCelular.Text = ""
        txtNumFijo.Text = ""
        txtEmail.Text = ""
        cmbDepartamento.SelectedValue = "-1" 'Combos provincia y distrito deben cambiar automáticamente
        txtDireccion.Text = ""
        dtpFecNacimiento.Text = ""
        cmbSexo.SelectedValue = "-1"
        cmbAnioEstudio.SelectedValue = "-1"
        'Datos laborales
        txtCentroLabores.Text = ""
        txtCargo.Text = ""
        txtRuc.Text = ""
        'Datos de institución educativa
        cmbDepartamentoInstEduc.SelectedValue = "-1"
        cmbInstitucionEducativa.SelectedValue = "-1"
        cmbCarreraProfesional.SelectedValue = "-1"
        'Otros
        txtConsultas.Text = ""
        chkTerminosCondiciones.Checked = False
        hddEmailCoincidente.Value = "0"
        hddEmailVerificado.Value = "0"

        udpForm.Update()
    End Sub

    Public Function EncriptaTexto(ByVal base64Decoded As String) As String
        Dim base64Encoded As String = ""
        Try
            Dim data As Byte()
            data = System.Text.UTF8Encoding.UTF8.GetBytes(base64Decoded)
            base64Encoded = System.Convert.ToBase64String(data)
        Catch ex As Exception

        End Try
        Return base64Encoded
    End Function

    Public Function DesencriptaTexto(ByVal base64Encoded As String) As String
        Dim base64Decoded As String = ""
        Try
            Dim data() As Byte
            data = System.Convert.FromBase64String(base64Encoded)
            base64Decoded = System.Text.UTF8Encoding.UTF8.GetString(data)
        Catch ex As Exception

        End Try
        Return base64Decoded
    End Function
#End Region
End Class
