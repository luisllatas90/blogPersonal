Imports System.Xml
Imports System.Data
Imports System.Collections.Generic
Imports System.Web.Script.Serialization

Partial Class frm_InscripcionInteresadoPEC
    Inherits System.Web.UI.Page

#Region "Variables"
    'Modelo
    Private lst_Departamento As New List(Of e_ListItem)
    Private lst_Provincia As New List(Of e_ListItem)
    Private lst_Distrito As New List(Of e_ListItem)
    Private ms_ServicioURL As String = "http://serverdev/campusvirtual/WSUSAT/WSUSAT.asmx"
    'Otros
    Private mo_SOAP As New ClsAdmisionSOAP
    Private ms_Usuario As String = "4877" '´codigo_Per
    Private ms_CodigoPai As String = "156" 'Perú
    Private ms_DefaultCodigoDep As String = "13" 'Por defecto: Lambayeque

    Private ms_codigoTest As String = "6"
#End Region

#Region "Eventos"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            mensajeServer.Attributes.Remove("data-operacion-realizada")
            udpMensajeServer.Update()

            If Not IsPostBack Then
                'PRUEBAS
                hddTipo.Value = "1"
                hddTokenCco.Value = "MMQ9M4PY"
                hddCodigoEve.Value = "11"
                hddCodigoCac.Value = "69"
                hddCodigoMin.Value = "1"

                btnFakeInscribirme.Visible = IIf(hddTipo.Value = "1", True, False)

                'Obtengo los valores de la WEB USAT
                'hddTipo.Value = Request.Params("tipo")
                'hddTokenCco.Value = Request.Params("tokenCco")
                'hddCodigoEve.Value = Request.Params("codigoEve")
                'hddCodigoCac.Value = Request.Params("codigoCac")
                'hddCodigoMin.Value = Request.Params("codigoMin")


                lr_CargarCombos()
            Else
                errorMensaje.InnerHtml = ""
            End If
        Catch ex As Exception
            errorMensaje.InnerHtml = ex.Message
        End Try
    End Sub

    Protected Sub cmbDepartamento_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbDepartamento.SelectedIndexChanged
        lr_CargarComboProvincias(cmbProvincia, cmbDepartamento.SelectedValue, "-- Provincia (*) --")
        lr_CargarComboDistritos(cmbDistrito, cmbProvincia.SelectedValue, "-- Distrito (*) --")
        udpProvincia.Update()
    End Sub

    Protected Sub cmbProvincia_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbProvincia.SelectedIndexChanged
        lr_CargarComboDistritos(cmbDistrito, cmbProvincia.SelectedValue, "-- Distrito (*) --")
        udpDistrito.Update()
    End Sub

    Protected Sub btnInformarme_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnInformarme.Click
        Try
            lr_RegistrarInteresado()
        Catch ex As Exception
            errorMensaje.InnerHtml = ex.Message
            Throw ex
        End Try
    End Sub

    Protected Sub btnInscribirme_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnInscribirme.Click
        Try
            lr_InscribirInteresado()
        Catch ex As Exception
            errorMensaje.InnerHtml = ex.Message
            Throw ex
        End Try
    End Sub
#End Region

#Region "Metodos"
    Private Sub lr_CargarCombos()
        lr_CargarComboDepartamentos(cmbDepartamento, ms_CodigoPai, "-- Departamento (*) --")
        cmbDepartamento.SelectedValue = ms_DefaultCodigoDep
        cmbDepartamento_SelectedIndexChanged(Nothing, Nothing)
    End Sub

    Private Sub lr_CargarComboDepartamentos(ByVal cmbCombo As DropDownList, ByVal ls_CodigoPai As String, Optional ByVal ls_Default As String = "")
        Dim lo_Datos As New Dictionary(Of String, String) : lo_Datos.Add("codigoPai", ls_CodigoPai)
        Dim lo_RespuestaSOAP As New XmlDocument : lo_RespuestaSOAP.LoadXml(mo_SOAP.lr_RealizarPeticionSOAP(ms_ServicioURL, "ListarDepartamento", lo_Datos))

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
        Dim lo_RespuestaSOAP As New XmlDocument : lo_RespuestaSOAP.LoadXml(mo_SOAP.lr_RealizarPeticionSOAP(ms_ServicioURL, "ListarProvincia", lo_Datos))

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
        Dim lo_RespuestaSOAP As New XmlDocument : lo_RespuestaSOAP.LoadXml(mo_SOAP.lr_RealizarPeticionSOAP(ms_ServicioURL, "ListarDistrito", lo_Datos))

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

    Private Sub lr_CargarComboCarreraProfesional(ByVal cmbCombo As DropDownList, Optional ByVal ls_Default As String = "")
        Dim lo_RespuestaSOAP As New XmlDocument : lo_RespuestaSOAP.LoadXml(mo_SOAP.lr_RealizarPeticionSOAP(ms_ServicioURL, "ListarCarreraProfesional"))

        Dim lo_Namespace As XmlNamespaceManager = New XmlNamespaceManager(lo_RespuestaSOAP.NameTable) : lo_Namespace.AddNamespace("ns", "http://tempuri.org/")
        Dim ls_RutaNodos As String = "//ns:ListarCarreraProfesionalResponse/ns:ListarCarreraProfesionalResult/ns:e_ListItem"
        Dim lst_Nodo As XmlNodeList = lo_RespuestaSOAP.DocumentElement.SelectNodes(ls_RutaNodos, lo_Namespace)

        Dim lo_DsCarreraProfesional As New Data.DataTable
        lo_DsCarreraProfesional.Columns.Add("Valor")
        lo_DsCarreraProfesional.Columns.Add("Nombre")
        For Each _Nodo As XmlNode In lst_Nodo
            lo_DsCarreraProfesional.Rows.Add(_Nodo.Item("Valor").InnerText.ToString, _Nodo.Item("Nombre").InnerText.ToString)
        Next
        Dim ls_Seleccione As String = IIf(String.IsNullOrEmpty(ls_Default), "-- Seleccione --", ls_Default)
        ClsFunciones.LlenarListas(cmbCombo, lo_DsCarreraProfesional, "Valor", "Nombre", ls_Seleccione)
    End Sub

    Private Sub lr_RegistrarInteresado()
        Try
            Dim lo_ValidacionFormulario As Dictionary(Of String, String) = lf_ValidarFormulario()
            If lo_ValidacionFormulario.Item("rpta") <> "1" Then
                lr_GenerarMensajesValidacion(lo_ValidacionFormulario.Item("rpta"), lo_ValidacionFormulario.Item("msg"), lo_ValidacionFormulario.Item("control"))
            Else
                Dim lo_Datos As New Dictionary(Of String, String)
                With lo_Datos
                    .Add("codigoEve", hddCodigoEve.Value)
                    .Add("codigoMin", hddCodigoMin.Value)
                    .Add("codigoDoci", "1")
                    .Add("numerodocInt", txtDNI.Text.Trim)
                    .Add("apepaternoInt", UCase(txtApellidoPaterno.Text.Trim))
                    .Add("apematernoInt", UCase(txtApellidoMaterno.Text.Trim))
                    .Add("nombresInt", UCase(txtNombres.Text.Trim))
                    .Add("fechanacimientoInt", dtpFecNacimiento.Text.Trim)
                    .Add("sexoPso", cmbSexo.SelectedValue)
                    .Add("gradoInt", "P")
                    .Add("codigoIed", "0")
                    .Add("codigoCpf", "0")
                    .Add("estadoInt", "1")
                    .Add("telNumeroTei", txtNumFijo.Text.Trim)
                    .Add("celNumeroTei", txtNumCelular.Text.Trim)
                    .Add("descripcionEmi", UCase(txtEmail.Text.Trim))
                    .Add("codigoDep", cmbDepartamento.SelectedValue)
                    .Add("codigoPro", cmbProvincia.SelectedValue)
                    .Add("codigoDis", cmbDistrito.SelectedValue)
                    .Add("direccionDin", UCase(txtDireccion.Text.Trim))
                    .Add("numerodocFin", "")
                    .Add("apepaternoFin", "")
                    .Add("apematernoFin", "")
                    .Add("nombresFin", "")
                    .Add("celularFin", "")
                    .Add("emailFin", "")
                    .Add("usuarioReg", ms_Usuario)
                End With
                Dim lo_RespuestaSOAP As New XmlDocument : lo_RespuestaSOAP.LoadXml(mo_SOAP.lr_RealizarPeticionSOAP(ms_ServicioURL, "GuardarInteresado", lo_Datos))

                Dim lo_Namespace As XmlNamespaceManager = New XmlNamespaceManager(lo_RespuestaSOAP.NameTable) : lo_Namespace.AddNamespace("ns", "http://tempuri.org/")
                Dim ls_RutaNodos As String = "//ns:GuardarInteresadoResponse /ns:GuardarInteresadoResult"
                Dim ls_CodigoInt As String = lo_RespuestaSOAP.DocumentElement.SelectSingleNode(ls_RutaNodos, lo_Namespace).InnerText

                Dim lo_Serializer As New JavaScriptSerializer()
                Dim lo_Dict As Dictionary(Of String, String) = lo_Serializer.Deserialize(Of Dictionary(Of String, String))(ls_CodigoInt)

                Dim ls_Mensaje As String = ""
                errorMensaje.InnerHtml = lo_Dict.Item("msg")
                Select Case lo_Dict.Item("rpta")
                    Case "-1"
                        ls_Mensaje = "No se pudo registrar la información"
                    Case "1"
                        ls_Mensaje = "Información registrada correctamente"
                    Case "0"
                        ls_Mensaje = lo_Dict.Item("msg")
                End Select
                lr_GenerarMensajes(lo_Dict.Item("rpta"), ls_Mensaje)
            End If
        Catch ex As Exception
            errorMensaje.InnerHtml = ex.Message
            Throw ex
        End Try
    End Sub

    Private Sub lr_InscribirInteresado()
        Try
            Dim lo_ValidacionFormulario As Dictionary(Of String, String) = lf_ValidarFormulario()
            If lo_ValidacionFormulario.Item("rpta") <> "1" Then
                lr_GenerarMensajesValidacion(lo_ValidacionFormulario.Item("rpta"), lo_ValidacionFormulario.Item("msg"), lo_ValidacionFormulario.Item("control"))
            Else
                '   ms_codigoTest = "1" 'Siempre es escuela PRE?

                Dim lo_Datos As New Dictionary(Of String, String)
                With lo_Datos
                    .Add("codigoEve", hddCodigoEve.Value)
                    .Add("codigoDoci", "1")
                    .Add("numerodocInt", txtDNI.Text.Trim)
                    .Add("apepaternoInt", UCase(txtApellidoPaterno.Text.Trim))
                    .Add("apematernoInt", UCase(txtApellidoMaterno.Text.Trim))
                    .Add("nombresInt", UCase(txtNombres.Text.Trim))
                    .Add("fechanacimientoInt", dtpFecNacimiento.Text.Trim)
                    .Add("sexoPso", cmbSexo.SelectedValue)
                    .Add("gradoInt", "P")
                    .Add("codigoIed", "0")
                    .Add("codigoCpf", "0")
                    .Add("estadoInt", "1")
                    .Add("telNumeroTei", txtNumFijo.Text.Trim)
                    .Add("celNumeroTei", txtNumCelular.Text.Trim)
                    .Add("descripcionEmi", UCase(txtEmail.Text.Trim))
                    .Add("codigoDep", cmbDepartamento.SelectedValue)
                    .Add("codigoPro", cmbProvincia.SelectedValue)
                    .Add("codigoDis", cmbDistrito.SelectedValue)
                    .Add("direccionDin", UCase(txtDireccion.Text.Trim))
                    .Add("tokenCco", hddTokenCco.Value)
                    .Add("codigoCac", hddCodigoCac.Value)
                    .Add("codigoMin", hddCodigoMin.Value)
                    .Add("codigoTest", ms_codigoTest)
                    .Add("descripcion_emi2", "")
                    .Add("estadocivil", cmbEstadoCivil.SelectedValue)
                    .Add("nroruc", txtRuc.Text.Trim)
                    .Add("centrolabores", UCase(txtCentroLabores.Text.Trim))
                    .Add("cargoactual", UCase(txtCargo.Text.Trim))
                    .Add("usuarioReg", ms_Usuario)
                    .Add("accion", "N") 'Accion "Normal"?
                End With

                Dim lo_RespuestaSOAP As New XmlDocument : lo_RespuestaSOAP.LoadXml(mo_SOAP.lr_RealizarPeticionSOAP(ms_ServicioURL, "InscribirInteresado", lo_Datos))

                Dim lo_Namespace As XmlNamespaceManager = New XmlNamespaceManager(lo_RespuestaSOAP.NameTable) : lo_Namespace.AddNamespace("ns", "http://tempuri.org/")
                Dim ls_RutaNodos As String = "//ns:InscribirInteresadoResponse /ns:InscribirInteresadoResult"
                Dim ls_CodigoInt As String = lo_RespuestaSOAP.DocumentElement.SelectSingleNode(ls_RutaNodos, lo_Namespace).InnerText

                Dim lo_Serializer As New JavaScriptSerializer()
                Dim lo_Dict As Dictionary(Of String, String) = lo_Serializer.Deserialize(Of Dictionary(Of String, String))(ls_CodigoInt)

                Dim ls_Mensaje As String = ""
                Select Case lo_Dict.Item("rpta")
                    Case "-1"
                        ls_Mensaje = "No se pudo registrar la información"
                    Case "1"
                        ls_Mensaje = lo_Dict.Item("msg")
                    Case "0"
                        ls_Mensaje = lo_Dict.Item("msg")
                End Select
                lr_GenerarMensajes(lo_Dict.Item("rpta"), ls_Mensaje)
            End If
        Catch ex As Exception
            errorMensaje.InnerHtml = ex.Message
        End Try
    End Sub

    Private Function lf_ValidarFormulario() As Dictionary(Of String, String)
        Try
            Dim lo_Resultado As New Dictionary(Of String, String)
            lo_Resultado.Add("rpta", "1")
            lo_Resultado.Add("msg", "")
            lo_Resultado.Add("control", "")

            If hddTipo.Value = "1" Then
                If String.IsNullOrEmpty(hddTokenCco.Value) Then
                    lo_Resultado.Item("rpta") = "-1"
                    lo_Resultado.Item("msg") = "No se ha cargado el centro de costo, vuelva a ingresar"
                    Return lo_Resultado
                End If

                If String.IsNullOrEmpty(hddCodigoCac.Value) Then
                    lo_Resultado.Item("rpta") = "-1"
                    lo_Resultado.Item("msg") = "No se ha cargado el ciclo académico, vuelva a ingresar"
                    Return lo_Resultado
                End If

                If String.IsNullOrEmpty(hddCodigoMin.Value) Then
                    lo_Resultado.Item("rpta") = "-1"
                    lo_Resultado.Item("msg") = "No se ha cargado la modalidad de ingreso, vuelva a ingresar"
                    Return lo_Resultado
                End If
            End If

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

            If cmbSexo.SelectedIndex = 0 Then
                lo_Resultado.Item("rpta") = "0"
                lo_Resultado.Item("msg") = "Debe seleccionar un sexo"
                lo_Resultado.Item("control") = "cmbSexo"
                Return lo_Resultado
            End If

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

            If cmbDistrito.SelectedValue < 0 Then
                lo_Resultado.Item("rpta") = "0"
                lo_Resultado.Item("msg") = "Debe seleccionar un distrito"
                lo_Resultado.Item("control") = "cmbDistrito"
                Return lo_Resultado
            End If

            If String.IsNullOrEmpty(txtDireccion.Text.Trim) Then
                lo_Resultado.Item("rpta") = "0"
                lo_Resultado.Item("msg") = "Debe ingresar una dirección"
                lo_Resultado.Item("control") = "txtDireccion"
                Return lo_Resultado
            End If

            If cmbEstadoCivil.SelectedValue = "E" Then
                lo_Resultado.Item("rpta") = "0"
                lo_Resultado.Item("msg") = "Debe seleccionar un estado civil"
                lo_Resultado.Item("control") = "cmbEstadoCivil"
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
                mensajeServer.Attributes.Item("data-clase-rpta") = "alert-danger"
            Case "1"
                mensajeServer.Attributes.Item("data-clase-rpta") = "alert-success"
            Case "0"
                mensajeServer.Attributes.Item("data-clase-rpta") = "alert-warning"
        End Select
        mensajeServer.Attributes.Item("data-operacion-realizada") = True
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
#End Region
End Class
