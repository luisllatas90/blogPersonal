Imports System.Xml
Imports System.IO
Imports System.Collections.Generic

Partial Class administrativo_gestion_educativa_frmImportarPostulantes
    Inherits System.Web.UI.Page

#Region "Variables de Clase"
    Private mo_RepoAdmision As New ClsAdmision

    Private ms_CodigoTfu As String = ""
    Private ms_CodigoPer As String = ""
    Private ms_SharedFilesPath As String = mo_RepoAdmision.ObtenerVariableGlobal("sharedFilesPath")
#End Region

#Region "Eventos"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'If Session("id_per") = "" Or Request.QueryString("id") = "" Then
        '    Response.Redirect("../../../sinacceso.html")
        'End If

        ms_CodigoTfu = Request.QueryString("ctf")
        ms_CodigoPer = Request.QueryString("id")

        respuestaPostback.Attributes.Item("data-enviado") = "false" : udpMensajeServidor.Update()
        AddHandler btnValidar.ServerClick, AddressOf btnValidar_Click

        If Not IsPostBack Then 'La pagina se carga por primera vez
            CargarCombos()
        End If
    End Sub

    Protected Sub cmbTipoEstudio_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbTipoEstudio.SelectedIndexChanged
        CargarComboCentroCosto()
        CargarComboCarreraProfesional()
        CargarComboModalidadAdmision()
    End Sub

    Protected Sub cmbCentroCosto_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbCentroCosto.SelectedIndexChanged
        CargarComboModalidadAdmision()
    End Sub

    Protected Sub cmbCarreraProfesional_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbCarreraProfesional.SelectedIndexChanged
        CargarComboModalidadAdmision()
    End Sub

    Protected Sub cmbCicloAcademico_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbCicloAcademico.SelectedIndexChanged
        CargarComboModalidadAdmision()
    End Sub

    Private Sub btnValidar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Validar()
    End Sub

    Protected Sub btnEnviar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEnviar.Click
        Enviar()
    End Sub
#End Region

#Region "Métodos"
    Private Sub CargarCombos()
        'Tipos de estudio
        cmbTipoEstudio_SelectedIndexChanged(cmbTipoEstudio, New System.EventArgs)

        'Ciclos académicos
        Dim lo_DtCicloIngreso As Data.DataTable = mo_RepoAdmision.ListarProcesoAdmisionV3()
        ClsFunciones.LlenarListas(cmbCicloAcademico, lo_DtCicloIngreso, "codigo_Cac", "descripcion_Cac", "-- Seleccione --")
        For Each _Row As Data.DataRow In lo_DtCicloIngreso.Rows
            cmbCicloAcademico.SelectedValue = _Row.Item("codigo_Cac")
            Exit For
        Next
        cmbCicloAcademico_SelectedIndexChanged(Nothing, Nothing)
    End Sub

    Private Sub CargarComboCentroCosto()
        Dim ls_CodigoTest As String = cmbTipoEstudio.SelectedValue
        Dim lo_DtCentroCosto As New Data.DataTable
        If ls_CodigoTest <> "-1" Then
            lo_DtCentroCosto = mo_RepoAdmision.ListarCentroCosto(ms_CodigoTfu, ms_CodigoPer, ls_CodigoTest)
        End If
        ClsFunciones.LlenarListas(cmbCentroCosto, lo_DtCentroCosto, "codigo_Cco", "Nombre", "-- Seleccione --")
        cmbCentroCosto.Attributes.Item("data-live-search") = True
        udpCentroCosto.Update()
    End Sub

    Private Sub CargarComboCarreraProfesional()
        Dim ls_CodigoTestPreGrado As String = "2"
        ClsFunciones.LlenarListas(cmbCarreraProfesional, mo_RepoAdmision.ListarCarreraProfesional(ls_CodigoTestPreGrado), "codigo_Cpf", "nombre_Cpf", "-- Seleccione --")
        cmbCarreraProfesional.Attributes.Item("data-live-search") = True
        udpCarreraProfesional.Update()
    End Sub

    Private Sub CargarComboModalidadAdmision()
        Dim ls_CodigoTest As String = IIf(cmbTipoEstudio.SelectedValue <> "-1", cmbTipoEstudio.SelectedValue, "0")
        Dim ls_Tipo As String = IIf(ls_CodigoTest = "1", "77", "7")
        Dim ls_CodigoCac As String = IIf(cmbCicloAcademico.SelectedValue <> "-1", cmbCicloAcademico.SelectedValue, "0")
        Dim ls_CodicoCpf As String = IIf(cmbCarreraProfesional.SelectedValue <> "-1", cmbCarreraProfesional.SelectedValue, "0")
        ClsFunciones.LlenarListas(cmbModalidadIngreso, mo_RepoAdmision.ListarModalidadIngreso(ls_Tipo, ls_CodigoTest, ls_CodigoCac, ls_CodicoCpf), "codigo_Min", "nombre_Min", "-- Seleccione --")
        udpModalidadIngreso.Update()
    End Sub

    Private Sub Validar()
        Try
            Dim lo_Validacion As Dictionary(Of String, String) = ValidarFormulario()
            With respuestaPostback.Attributes
                .Item("data-enviado") = "true"
                .Item("data-rpta") = lo_Validacion.Item("rpta")
                .Item("data-msg") = lo_Validacion.Item("msg")
                .Item("data-control") = lo_Validacion.Item("control")
            End With
        Catch ex As Exception
            With respuestaPostback.Attributes
                .Item("data-enviado") = "true"
                .Item("data-rpta") = "-1"
                .Item("data-msg") = "Ha ocurrido un error en el servidor"
            End With
        Finally
            udpMensajeServidor.Update()
        End Try
    End Sub

    Private Sub Enviar()
        Try
            'If Session("id_per") = "" Or Request.QueryString("id") = "" Then
            '    Response.Redirect("../../../sinacceso.html")
            'End If

            Dim lo_Validacion As Dictionary(Of String, String) = ValidarFormulario()
            If lo_Validacion.Item("rpta") = 1 Then
                'Guardar Archivo
                Dim ls_RutaArchivo As String = ""
                Dim ls_NombreFinal As String = ""
                Dim ln_IdArchivoCompartido As Integer
                Dim ls_Extension As String = ""
                Dim ln_IdTabla As Integer = mo_RepoAdmision.ObtenerVariableGlobal("notasAdmision|codigoTablaArchivo")
                Dim ln_IdTransaccion As Integer = 0 ' ID DE TABLA RELACIONADA (EN ESTE CASO NO TENEMOS UN SOLO REGISTRO SINO VARIOS SE CONSIDERA 0)
                Dim ln_NroOperacion As Integer = 0 ' ID DE TABLA RELACIONADA OPCIONAL (EN ESTE CASO NO TENEMOS UN SOLO REGISTRO SINO VARIOS SE CONSIDERA 0)
                Dim lo_Archivo As HttpPostedFile = HttpContext.Current.Request.Files(fluNotas.ID)
                Dim ln_CodigoPer As Integer = Request.QueryString("id")
                Dim lb_Resultado As Boolean = SubirArchivo(ln_IdTabla, 0, lo_Archivo, 0)

                If lb_Resultado Then
                    Dim dta As New Data.DataTable
                    dta = mo_RepoAdmision.ObtenerUltimoIDArchivoCompartido(ln_IdTabla, ln_IdTransaccion, ln_NroOperacion)
                    ls_RutaArchivo = dta.Rows(0).Item("ruta").ToString
                    ls_NombreFinal = dta.Rows(0).Item("NombreArchivo").ToString
                    ln_IdArchivoCompartido = dta.Rows(0).Item("idarchivo")
                    ls_Extension = dta.Rows(0).Item("Extension")

                    'Obtenemos Respuesta de Migración en una tabla
                    Dim lo_Resultado As New Dictionary(Of String, String)
                    lo_Resultado = mo_RepoAdmision.CargarExcelNotas(ls_RutaArchivo, cmbCentroCosto.SelectedValue, ln_IdArchivoCompartido, ln_CodigoPer)

                    With respuestaPostback.Attributes
                        .Item("data-enviado") = "true"
                        .Item("data-rpta") = lo_Resultado.Item("rpta")
                        If lo_Resultado.Item("rpta") = "-1" Then
                            .Item("data-msg") = "Se ha producido un error al cargar el archivo"
                            divErrorMessage.InnerHtml = lo_Resultado.Item("msg")
                        Else
                            .Item("data-msg") = lo_Resultado.Item("msg")
                            divErrorMessage.InnerHtml = ""
                        End If
                        .Item("data-control") = ""
                    End With
                Else
                    With respuestaPostback.Attributes
                        .Item("data-enviado") = "true"
                        .Item("data-rpta") = "-1"
                        .Item("data-msg") = "No se ha podido subir el archivo al servidor"
                    End With
                End If
            Else
                With respuestaPostback.Attributes
                    .Item("data-enviado") = "false"
                    .Item("data-rpta") = lo_Validacion.Item("rpta")
                    .Item("data-msg") = lo_Validacion.Item("msg")
                    .Item("data-control") = ""
                End With
            End If

        Catch ex As Exception
            Response.Write(ex.Message)
            Throw ex
        End Try
    End Sub

    Private Function ValidarFormulario() As Dictionary(Of String, String)
        Dim lo_Validacion As New Dictionary(Of String, String)
        lo_Validacion.Add("rpta", 1)
        lo_Validacion.Add("msg", "")
        lo_Validacion.Add("control", "")

        If cmbTipoEstudio.SelectedValue = "-1" Then
            lo_Validacion.Item("rpta") = 0
            lo_Validacion.Item("msg") = "Debe seleccionar un tipo de estudio"
            lo_Validacion.Item("control") = "cmbTipoEstudio"
            Return lo_Validacion
        End If

        If cmbCentroCosto.SelectedValue = "-1" Then
            lo_Validacion.Item("rpta") = 0
            lo_Validacion.Item("msg") = "Debe seleccionar un centro de costo"
            lo_Validacion.Item("control") = "cmbCentroCosto"
            Return lo_Validacion
        End If

        If Not ScriptManager.GetCurrent(Page).IsInAsyncPostBack Then 'Solo en full request
            If fluNotas.HasFile Then
                If System.IO.Path.GetExtension(fluNotas.FileName) <> ".csv" Then
                    lo_Validacion.Item("rpta") = 0
                    lo_Validacion.Item("msg") = "Solo se permiten archivos en formato .csv"
                    lo_Validacion.Item("control") = "fluNotas"
                    Return lo_Validacion
                End If
            Else
                lo_Validacion.Item("rpta") = 0
                lo_Validacion.Item("msg") = "Debe seleccionar un archivo"
                lo_Validacion.Item("control") = "fluNotas"
                Return lo_Validacion
            End If
        End If

        Return lo_Validacion
    End Function

    Function SubirArchivo(ByVal ls_IdTabla As Integer, ByVal ls_NroTransaccion As String, ByVal lo_Archivo As HttpPostedFile, ByVal tipo As String) As Boolean
        Try
            Dim ls_NroOperacion As String = ""
            Dim id_tablaProviene As String = ls_NroTransaccion

            Dim ld_Fecha As String = Date.Now.ToString("dd/MM/yyyy")
            Dim ls_Usuario As String = Session("perlogin").ToString
            Dim lo_Input(lo_Archivo.ContentLength) As Byte

            Dim lo_Br As New BinaryReader(lo_Archivo.InputStream)
            Dim lo_BinData As Byte() = lo_Br.ReadBytes(lo_Archivo.InputStream.Length)

            Dim lo_WsCloud As New ClsArchivosCompartidos
            Dim list As New Dictionary(Of String, String)

            Dim ls_NombreArchivo As String = System.IO.Path.GetFileName(lo_Archivo.FileName.Replace("&", "_").Replace("'", "_").Replace("*", "_"))

            list.Add("Fecha", ld_Fecha)
            list.Add("Extencion", System.IO.Path.GetExtension(lo_Archivo.FileName))
            list.Add("Nombre", ls_NombreArchivo)
            list.Add("TransaccionId", id_tablaProviene)
            list.Add("TablaId", ls_IdTabla)
            list.Add("NroOperacion", ls_NroOperacion)
            list.Add("Archivo", System.Convert.ToBase64String(lo_BinData, 0, lo_BinData.Length))
            list.Add("Usuario", ls_Usuario)
            list.Add("Equipo", "")
            list.Add("Ip", "")
            list.Add("param8", ls_Usuario)

            Dim envelope As String = lo_WsCloud.SoapEnvelope(list)
            Dim lo_RespuestaSOAP As New XmlDocument
            lo_RespuestaSOAP.LoadXml(lo_WsCloud.PeticionRequestSoap(ms_SharedFilesPath, envelope, "http://usat.edu.pe/UploadFile", ls_Usuario))

            Dim lo_Namespace As XmlNamespaceManager = New XmlNamespaceManager(lo_RespuestaSOAP.NameTable)
            lo_Namespace.AddNamespace("ns", "http://usat.edu.pe")

            Dim ls_RutaNodos As String = "//ns:UploadFileResponse/ns:UploadFileResult"
            Dim ls_Status As String = lo_RespuestaSOAP.DocumentElement.SelectSingleNode(ls_RutaNodos & "/ns:Status", lo_Namespace).InnerText
            Dim ls_Code As String = lo_RespuestaSOAP.DocumentElement.SelectSingleNode(ls_RutaNodos & "/ns:StatusBody/ns:Code ", lo_Namespace).InnerText

            If ls_Status = "OK" And ls_Code = "0" Then
                Return True
            End If

        Catch ex As Exception
            Response.Write(ex.Message)
            Throw ex
        End Try

        Return False

    End Function
#End Region
End Class
