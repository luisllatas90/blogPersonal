Imports System.Net
Imports System.IO
Imports System.Data
Imports System.Collections.Generic
Imports System.Web.Script.Serialization

Partial Class GraduacionTitulacion_Tramite_frmEmitirDiplomasProveedor
    Inherits System.Web.UI.Page

#Region "Declaracion de Variables"
    'ENTIDADES    
    Dim me_SesionConsejoUniv_GYT As e_SesionConsejoUniv_GYT
    Dim me_TipoDenominacionGradoTitulo As e_TipoDenominacionGradoTitulo
    Dim me_EnvioDiplomasProveedor As e_EnvioDiplomasProveedor
    Dim me_EnvioDiplomasProveedorDetalle As e_EnvioDiplomasProveedorDetalle


    'DATOS    
    Dim md_SesionConsejoUniv_GYT As New d_SesionConsejoUniv_GYT
    Dim md_TipoDenominacionGradoTitulo As New d_TipoDenominacionGradoTitulo
    Dim md_EnvioDiplomasProveedor As New d_EnvioDiplomasProveedor
    Dim md_EnvioDiplomasProveedorDetalle As New d_EnvioDiplomasProveedorDetalle    
    Dim md_Funciones As New d_Funciones

    'VARIABLES
    Dim cod_user As Integer = 0    

    Dim codigoOperacionGrupo As String = String.Empty
    Dim estadoOperacionGrupo As String = String.Empty
    Dim mensajeOperacionGrupo As String = String.Empty

    Public Enum MessageType
        success
        [error]
        info
        warning
    End Enum

#End Region

#Region "Eventos"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If (Session("id_per") Is Nothing OrElse Session("perlogin") Is Nothing) Then
                Response.Redirect("../../../sinacceso.html")
            End If

            cod_user = Session("id_per")

            If IsPostBack = False Then
                Call mt_CargarComboSesionConsejo()
                Call mt_CargarComboTipoDenominacion()
                Me.lblTipoEmisionFiltro.Visible = False
                Me.cmbTipoEmisionFiltro.Visible = False
            End If
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Protected Sub cmbTipoDiplomaFiltro_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbTipoDiplomaFiltro.SelectedIndexChanged
        Try
            If cmbTipoDiplomaFiltro.SelectedValue.Equals("E") Then
                Me.lblTipoEmisionFiltro.Visible = True
                Me.cmbTipoEmisionFiltro.SelectedValue = String.Empty
                Me.cmbTipoEmisionFiltro.Visible = True
                Me.btnEnviar.Visible = True
                Me.btnExportarRegistrar.Visible = False
            ElseIf cmbTipoDiplomaFiltro.SelectedValue.Equals("F") Then
                Me.lblTipoEmisionFiltro.Visible = False
                Me.cmbTipoEmisionFiltro.SelectedValue = String.Empty
                Me.cmbTipoEmisionFiltro.Visible = False
                Me.btnEnviar.Visible = False
                Me.btnExportarRegistrar.Visible = True
            Else
                Me.lblTipoEmisionFiltro.Visible = False
                Me.cmbTipoEmisionFiltro.SelectedValue = String.Empty
                Me.cmbTipoEmisionFiltro.Visible = False
                Me.btnEnviar.Visible = False
                Me.btnExportarRegistrar.Visible = False
            End If

            Call mt_LimpiarListas()
            Call mt_UpdatePanel("Filtros")
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Protected Sub cmbSesionConsejoFiltro_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbSesionConsejoFiltro.SelectedIndexChanged
        Try
            Call mt_LimpiarListas()
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Protected Sub cmbTipoDenominacionFiltro_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbTipoDenominacionFiltro.SelectedIndexChanged
        Try
            Call mt_LimpiarListas()
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Protected Sub cmbTipoEmisionFiltro_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbTipoEmisionFiltro.SelectedIndexChanged
        Try
            Call mt_LimpiarListas()
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Protected Sub btnListar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnListar.Click
        Try
            Call mt_CargarDatos()
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Protected Sub btnExportar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExportar.Click
        Try
            Call mt_Exportar("N")
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Protected Sub btnExportarRegistrar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExportarRegistrar.Click
        Try
            Call mt_Exportar("R")
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Protected Sub btnEnviar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEnviar.Click
        Try
            If fu_ValidarCargarDatos() Then
                Call mt_EnviarDiplomasProveedor()
            End If
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

#End Region

#Region "Metodos"

    Private Sub mt_ShowMessage(ByVal Message As String, ByVal type As MessageType)
        Try
            Me.udpScripts.Update()
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "showMessage", "showMessage('" & Message & "','" & type.ToString & "');", True)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub mt_UpdatePanel(ByVal ls_panel As String)
        Try
            Select Case ls_panel
                Case "Filtros"
                    Me.udpFiltros.Update()
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "udpFiltrosUpdate", "udpFiltrosUpdate();", True)

                Case "Lista"
                    Me.udpLista.Update()
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "udpListaUpdate", "udpListaUpdate();", True)

            End Select
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Private Sub mt_FlujoTabs(ByVal ls_tab As String)
        Try
            Select Case ls_tab
                Case "Listado"
                    Me.udpScripts.Update()
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "flujoTabs", "flujoTabs('listado-tab');", True)

            End Select
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Private Sub mt_CargarComboSesionConsejo()
        Try
            Dim dt As New Data.DataTable : me_SesionConsejoUniv_GYT = New e_SesionConsejoUniv_GYT

            With me_SesionConsejoUniv_GYT
                .operacion = "CON"
                .abreviatura_con = "CU"
                '.operacion = "L"
                '.codigo_scu = "%"
            End With
            dt = md_SesionConsejoUniv_GYT.ListarSesionesConsejo(me_SesionConsejoUniv_GYT)

            Call md_Funciones.CargarCombo(Me.cmbSesionConsejoFiltro, dt, "codigo_scu", "descripcion_scu", True, "[-- SELECCIONE --]", "")
            dt.Dispose()
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Private Sub mt_CargarComboTipoDenominacion()
        Try
            Dim dt As New Data.DataTable : me_TipoDenominacionGradoTitulo = New e_TipoDenominacionGradoTitulo

            With me_TipoDenominacionGradoTitulo
                .operacion = "GYT"
            End With
            dt = md_TipoDenominacionGradoTitulo.ConsultarTipoDenominacion(me_TipoDenominacionGradoTitulo)

            Call md_Funciones.CargarCombo(Me.cmbTipoDenominacionFiltro, dt, "codigo", "nombre", True, "[-- SELECCIONE --]", "")
            dt.Dispose()
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Private Sub mt_LimpiarListas()
        Try
            Me.grwListaFisico.DataSource = Nothing : Me.grwListaFisico.DataBind()
            Me.grwListaElectronico.DataSource = Nothing : Me.grwListaElectronico.DataBind()

            Call md_Funciones.AgregarHearders(grwListaFisico)
            Call md_Funciones.AgregarHearders(grwListaElectronico)

            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "mostrarBotonTodos", "mostrarBotonTodos('O');", True)

            Call mt_UpdatePanel("Lista")
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Private Sub mt_CargarDatos()
        Try
            If Not fu_ValidarCargarDatos() Then Exit Sub

            Dim dt As New DataTable : me_EnvioDiplomasProveedor = New e_EnvioDiplomasProveedor

            If Me.grwListaFisico.Rows.Count > 0 Then Me.grwListaFisico.DataSource = Nothing : Me.grwListaFisico.DataBind()
            If Me.grwListaElectronico.Rows.Count > 0 Then Me.grwListaElectronico.DataSource = Nothing : Me.grwListaElectronico.DataBind()

            If Me.cmbTipoDiplomaFiltro.SelectedValue.Equals("F") Then
                With me_EnvioDiplomasProveedor
                    .operacion = "FIS"
                    .codigo_scu = Me.cmbSesionConsejoFiltro.SelectedValue
                    .codigo_tdg = Me.cmbTipoDenominacionFiltro.SelectedValue
                End With

                dt = md_EnvioDiplomasProveedor.FormatoTramaEmisionDiploma(me_EnvioDiplomasProveedor)

                Me.grwListaFisico.DataSource = dt
            ElseIf Me.cmbTipoDiplomaFiltro.SelectedValue.Equals("E") Then
                With me_EnvioDiplomasProveedor
                    .operacion = "ELE"
                    .codigo_scu = Me.cmbSesionConsejoFiltro.SelectedValue
                    .codigo_tdg = Me.cmbTipoDenominacionFiltro.SelectedValue
                    .tipo_emision = Me.cmbTipoEmisionFiltro.SelectedValue
                End With

                dt = md_EnvioDiplomasProveedor.FormatoTramaEmisionDiploma(me_EnvioDiplomasProveedor)

                Me.grwListaElectronico.DataSource = dt

                If Me.cmbTipoDenominacionFiltro.SelectedValue = 3 OrElse _
                    Me.cmbTipoDenominacionFiltro.SelectedValue = 4 Then
                    Me.grwListaElectronico.Columns(31).Visible = False 'FACULTAD
                    Me.grwListaElectronico.Columns(32).Visible = False 'ESCUELA                
                Else
                    Me.grwListaElectronico.Columns(31).Visible = True 'FACULTAD
                    Me.grwListaElectronico.Columns(32).Visible = True 'ESCUELA   
                End If

                If Me.cmbTipoEmisionFiltro.SelectedValue = "D" Then
                    Me.grwListaElectronico.Columns(47).Visible = True 'FECHA_DUPLICADO
                Else
                    Me.grwListaElectronico.Columns(47).Visible = False 'FECHA_DUPLICADO
                End If
            End If

            If dt.Rows.Count = 0 Then
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "mostrarBotonTodos", "mostrarBotonTodos('O');", True)
            Else
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "mostrarBotonTodos", "mostrarBotonTodos('M');", True)
            End If

            Me.grwListaFisico.DataBind()
            Me.grwListaElectronico.DataBind()

            Call md_Funciones.AgregarHearders(grwListaFisico)
            Call md_Funciones.AgregarHearders(grwListaElectronico)

            Call mt_UpdatePanel("Lista")
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Private Function fu_ValidarCargarDatos() As Boolean
        Try
            If String.IsNullOrEmpty(Me.cmbTipoDiplomaFiltro.SelectedValue) Then mt_ShowMessage("Debe seleccionar un tipo de diploma.", MessageType.warning) : Me.cmbTipoDiplomaFiltro.Focus() : Return False
            If String.IsNullOrEmpty(Me.cmbSesionConsejoFiltro.SelectedValue) OrElse Me.cmbSesionConsejoFiltro.SelectedValue = 0 Then mt_ShowMessage("Debe seleccionar una sesión de consejo.", MessageType.warning) : Me.cmbSesionConsejoFiltro.Focus() : Return False
            If String.IsNullOrEmpty(Me.cmbTipoDenominacionFiltro.SelectedValue) OrElse Me.cmbTipoDenominacionFiltro.SelectedValue = 0 Then mt_ShowMessage("Debe seleccionar una denominación.", MessageType.warning) : Me.cmbTipoDenominacionFiltro.Focus() : Return False
            If Me.cmbTipoDiplomaFiltro.SelectedValue = "E" AndAlso String.IsNullOrEmpty(Me.cmbTipoEmisionFiltro.SelectedValue) Then mt_ShowMessage("Debe seleccionar un tipo de emisión.", MessageType.warning) : Me.cmbTipoEmisionFiltro.Focus() : Return False

            Return True
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Function

    Private Function mt_EnviarDiplomasProveedor() As Boolean
        Try
            If Not fu_ValidarEnviarDiplomasProveedor() Then Return False

            Dim dt As New DataTable
            Dim cantidad_no_enviados As Integer = 0

            me_EnvioDiplomasProveedor = md_EnvioDiplomasProveedor.GetEnvioDiplomasProveedor(0)

            With me_EnvioDiplomasProveedor
                .operacion = "I"
                .cod_user = cod_user
                .tipo_edp = Me.cmbTipoDiplomaFiltro.SelectedValue
                .codigo_scu = Me.cmbSesionConsejoFiltro.SelectedValue
                .codigo_tdg = Me.cmbTipoDenominacionFiltro.SelectedValue
                .tipo_emision = Me.cmbTipoEmisionFiltro.SelectedValue
            End With

            dt = md_EnvioDiplomasProveedor.RegistrarEnvioDiplomasProveedor(me_EnvioDiplomasProveedor)

            If dt.Rows.Count > 0 Then
                Dim codigo_edp As Integer = CInt(dt.Rows(0).Item("codigo_edp").ToString)

                For Each Fila As GridViewRow In Me.grwListaElectronico.Rows
                    If CType(Fila.FindControl("chkElegir"), CheckBox).Checked Then
                        If mt_EnviarDiploma(Fila) Then
                            If String.IsNullOrEmpty(codigoOperacionGrupo) OrElse String.IsNullOrEmpty(estadoOperacionGrupo) Then
                                cantidad_no_enviados += 1
                            Else
                                me_EnvioDiplomasProveedorDetalle = md_EnvioDiplomasProveedorDetalle.GetEnvioDiplomasProveedorDetalle(0)

                                With me_EnvioDiplomasProveedorDetalle
                                    .operacion = "I"
                                    .cod_user = cod_user
                                    .codigo_edp = codigo_edp
                                    .codigo_trl = CInt(Me.grwListaElectronico.DataKeys(Fila.RowIndex).Item("codigo_trl").ToString)
                                    .codigo_egr = CInt(Me.grwListaElectronico.DataKeys(Fila.RowIndex).Item("codigo_egr").ToString)
                                    .codigoOperacionGrupo = codigoOperacionGrupo
                                    .estadoOperacionGrupo = estadoOperacionGrupo
                                    .mensajeOperacionGrupo = mensajeOperacionGrupo
                                End With

                                md_EnvioDiplomasProveedorDetalle.RegistrarEnvioDiplomasProveedorDetalle(me_EnvioDiplomasProveedorDetalle)
                            End If
                        Else
                            cantidad_no_enviados += 1
                        End If
                    End If
                Next
            Else
                mt_ShowMessage("No pudo registrar el envío de diplomas al proveedor.", MessageType.error)
                Return False
            End If

            If cantidad_no_enviados > 0 Then
                mt_ShowMessage("La cantidad de diplomas que no pudieron ser enviados al proveedor es: " & CStr(cantidad_no_enviados), MessageType.warning)
                Return True
            End If

            mt_ShowMessage("¡El envío de diplomas al proveedor se realizó exitosamente!", MessageType.success)

            Me.udpScripts.Update()            
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "presionarBotonListar", "presionarBotonListar();", True)

            Return True
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Function

    Private Function fu_ValidarEnviarDiplomasProveedor() As Boolean
        Try
            If Not Me.cmbTipoDiplomaFiltro.SelectedValue.Equals("E") Then mt_ShowMessage("Solo puede enviar diplomas de tipo electrónico al proveedor.", MessageType.warning) : Me.cmbTipoDiplomaFiltro.Focus() : Return False
            If Me.grwListaElectronico.Rows.Count = 0 Then mt_ShowMessage("Deben existir registros de diplomas en la lista.", MessageType.warning) : Return False

            Dim lb_seleccionado As Boolean = False

            For Each Fila As GridViewRow In Me.grwListaElectronico.Rows
                If CType(Fila.FindControl("chkElegir"), CheckBox).Checked Then
                    lb_seleccionado = True
                End If
            Next

            If Not lb_seleccionado Then mt_ShowMessage("Debe seleccionar al menos un registro de la lista.", MessageType.warning) : Return False

            Return True
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Function

    Private Function mt_EnviarDiploma(ByVal FilaGrilla As GridViewRow) As Boolean
        Try
            Dim key As String = mt_ObtenerCodigoAutorizacion()

            'Call mt_ShowMessage(key, MessageType.error)
            'Return False

            codigoOperacionGrupo = String.Empty
            estadoOperacionGrupo = String.Empty
            mensajeOperacionGrupo = String.Empty

            If String.IsNullOrEmpty(key) Then Return False

            Dim request As HttpWebRequest
            Dim response As HttpWebResponse
            Dim reader As StreamReader
            Dim rawresponse As String
            Dim correo_prueba As String = String.Empty

            If Not (ConfigurationManager.AppSettings("CorreoUsatActivo") = 1) Then 'DEV
                request = DirectCast(WebRequest.Create("http://34.198.218.135:8080/wsplussignertitulousat/FirmaDigital/iniciarGrupoOperacionFirmaCliente/"), HttpWebRequest)
                correo_prueba = "jbanda@usat.edu.pe"
            Else 'PRD
                request = DirectCast(WebRequest.Create("https://services.plussigner.com/wsplussignertitulousat/FirmaDigital/iniciarGrupoOperacionFirmaCliente/"), HttpWebRequest)
            End If

            'Obtener IdGrupo
            Dim IdGrupo As String = String.Empty
            Dim IdTipoDenominacion As String = Me.cmbTipoDenominacionFiltro.SelectedValue
            Dim TipoEmision As String = Me.cmbTipoEmisionFiltro.SelectedValue

            If TipoEmision.Equals("O") Then
                Select Case IdTipoDenominacion
                    Case "1" : IdGrupo = "1" 'BACHILLER
                    Case "2" : IdGrupo = "4" 'TITULO PROFESIONAL
                    Case "3" : IdGrupo = "3" 'MAESTRO
                    Case "4" : IdGrupo = "2" 'DOCTOR
                    Case "5" : IdGrupo = "5" 'SEGUNDO ESPECIALIDAD
                End Select
            ElseIf TipoEmision.Equals("D") Then
                Select Case IdTipoDenominacion
                    Case "1" : IdGrupo = "6" 'BACHILLER
                    Case "2" : IdGrupo = "9" 'TITULO PROFESIONAL
                    Case "3" : IdGrupo = "8" 'MAESTRO
                    Case "4" : IdGrupo = "7" 'DOCTOR
                    Case "5" : IdGrupo = "10" 'SEGUNDO ESPECIALIDAD
                End Select
            End If

            request.Method = "POST"            
            request.ContentType = "application/json"            
            request.Headers.Add("Authorization", key)
            request.Accept = "application/json"

            Dim postdata As String = String.Empty

            With FilaGrilla
                postdata = "{ ""IdentificadorCliente"": ""USAT"", " + _
                        " ""Documentos"": [ { " + _
                            " ""IdArchivo"": """ + Me.grwListaElectronico.DataKeys(FilaGrilla.RowIndex).Item("codigo_trl").ToString + """, " + _
                            " ""DataValidacion"": [ " + _
                                " { ""key"": ""grado_academico"", ""value"": """ + Server.HtmlDecode(.Cells(26).Text) + """ }, " + _
                                " { ""key"": ""Fec_Opto_Dia"", ""value"": """ + Server.HtmlDecode(.Cells(27).Text) + """ }, " + _
                                " { ""key"": ""Fec_Opto_Mes"", ""value"": """ + Server.HtmlDecode(.Cells(28).Text) + """ }, " + _
                                " { ""key"": ""Fec_Opto_Año"", ""value"": """ + Server.HtmlDecode(.Cells(29).Text) + """ }, " + _
                                IIf("1 2 5".Contains(IdTipoDenominacion), " { ""key"": ""Facultad"", ""value"": """ + Server.HtmlDecode(.Cells(31).Text) + """ }, ", "") + _
                                IIf("1 2 5".Contains(IdTipoDenominacion), " { ""key"": ""Escuela"", ""value"": """ + Server.HtmlDecode(.Cells(32).Text) + """ }, ", "") + _
                                " { ""key"": ""Fec_Emi_Dia"", ""value"": """ + Server.HtmlDecode(.Cells(33).Text) + """ }, " + _
                                " { ""key"": ""Fec_Emi_Mes"", ""value"": """ + Server.HtmlDecode(.Cells(34).Text) + """ }, " + _
                                " { ""key"": ""Fec_Emi_Año"", ""value"": """ + Server.HtmlDecode(.Cells(35).Text) + """ }, " + _
                                " { ""key"": ""cod_univ"", ""value"": """ + Server.HtmlDecode(.Cells(36).Text) + """ }, " + _
                                " { ""key"": ""tipo_doc"", ""value"": """ + Server.HtmlDecode(.Cells(2).Text) + """ }, " + _
                                IIf("2".Contains(IdTipoDenominacion), " { ""key"": ""abreviatura_titulo"", ""value"": """, " { ""key"": ""abreviatura_grado"", ""value"": """) + Server.HtmlDecode(.Cells(37).Text) + """ }, " + _
                                IIf("2".Contains(IdTipoDenominacion), " { ""key"": ""titulo_obt_por"", ""value"": """, " { ""key"": ""grado_obt_por"", ""value"": """) + Server.HtmlDecode(.Cells(38).Text) + """ }, " + _
                                " { ""key"": ""modalidad_estudios"", ""value"": """ + Server.HtmlDecode(.Cells(39).Text) + """ }, " + _
                                " { ""key"": ""numero_resolucion"", ""value"": """ + Server.HtmlDecode(.Cells(40).Text) + """ }, " + _
                                " { ""key"": ""fecha_resolucion"", ""value"": """ + Server.HtmlDecode(.Cells(41).Text) + """ }, " + _
                                " { ""key"": ""numero_diploma"", ""value"": """ + Server.HtmlDecode(.Cells(42).Text) + """ }, " + _
                                " { ""key"": ""tipo_emision_diploma"", ""value"": """ + Server.HtmlDecode(.Cells(43).Text) + """ }, " + _
                                " { ""key"": ""libro"", ""value"": """ + Server.HtmlDecode(.Cells(44).Text) + """ }, " + _
                                " { ""key"": ""folio"", ""value"": """ + Server.HtmlDecode(.Cells(45).Text) + """ }, " + _
                                " { ""key"": ""registro"", ""value"": """ + Server.HtmlDecode(.Cells(46).Text) + """ } " + _
                                IIf(TipoEmision.Equals("D"), ", { ""key"": ""fecha_duplicado"", ""value"": """ + Server.HtmlDecode(.Cells(47).Text) + """ } ", "") + _
                                " ] } ], " + _
                        " ""UsuarioFirmante"": [ " + _
                            " { ""NombreUsuarioFirmante"": """ + Server.HtmlDecode(.Cells(7).Text) + """, ""DocUsuarioFirmante"": """ + Server.HtmlDecode(.Cells(6).Text) + """, ""CargoFirmante"": """ + Server.HtmlDecode(.Cells(8).Text) + """, ""correo"": """ + IIf(String.IsNullOrEmpty(correo_prueba), Server.HtmlDecode(.Cells(9).Text), correo_prueba) + """, ""tipo"": ""1"" }, " + _
                            " { ""NombreUsuarioFirmante"": """ + Server.HtmlDecode(.Cells(11).Text) + """, ""DocUsuarioFirmante"": """ + Server.HtmlDecode(.Cells(10).Text) + """, ""CargoFirmante"": """ + Server.HtmlDecode(.Cells(12).Text) + """, ""correo"": """ + IIf(String.IsNullOrEmpty(correo_prueba), Server.HtmlDecode(.Cells(13).Text), correo_prueba) + """, ""tipo"": ""1"" }, " + _
                            " { ""NombreUsuarioFirmante"": """ + Server.HtmlDecode(.Cells(15).Text) + """, ""DocUsuarioFirmante"": """ + Server.HtmlDecode(.Cells(14).Text) + """, ""CargoFirmante"": """ + Server.HtmlDecode(.Cells(16).Text) + """, ""correo"": """ + IIf(String.IsNullOrEmpty(correo_prueba), Server.HtmlDecode(.Cells(17).Text), correo_prueba) + """, ""tipo"": ""1"" }, " + _
                            " { ""NombreUsuarioFirmante"": """ + Server.HtmlDecode(.Cells(19).Text) + """, ""DocUsuarioFirmante"": """ + Server.HtmlDecode(.Cells(18).Text) + """, ""CargoFirmante"": """ + Server.HtmlDecode(.Cells(20).Text) + """, ""correo"": """ + IIf(String.IsNullOrEmpty(correo_prueba), Server.HtmlDecode(.Cells(21).Text), correo_prueba) + """, ""tipo"": ""1"" }, " + _
                            " { ""NombreUsuarioFirmante"": """ + Server.HtmlDecode(.Cells(3).Text) + """, ""DocUsuarioFirmante"": """ + Server.HtmlDecode(.Cells(1).Text) + """, ""CargoFirmante"": ""Estudiante"", ""correo"": """ + IIf(String.IsNullOrEmpty(correo_prueba), Server.HtmlDecode(.Cells(4).Text), correo_prueba) + """, ""tipo"": ""2"" } ], " + _
                        " ""ConfiguracionFirma"": { ""IdGrupo"": """ + IdGrupo + """ } }"
            End With

            'Call mt_ShowMessage(postdata, MessageType.error)
            'Return False

            'request.ContentLength = postdata.Length
            'Dim requestWriter As StreamWriter = New StreamWriter(request.GetRequestStream(), Encoding.UTF8)
            'requestWriter.Write(postdata)
            'requestWriter.Close()

            Dim jsonBytes = Encoding.UTF8.GetBytes(postdata)
            request.ContentLength = jsonBytes.Length

            Using requestWriter = request.GetRequestStream()
                requestWriter.Write(jsonBytes, 0, jsonBytes.Length)
                requestWriter.Close()
            End Using


            response = DirectCast(request.GetResponse(), HttpWebResponse)
            reader = New StreamReader(response.GetResponseStream())

            rawresponse = reader.ReadToEnd()

            Dim json As String = rawresponse

            Dim jss As New JavaScriptSerializer()
            Dim dict As Dictionary(Of String, String) = jss.Deserialize(Of Dictionary(Of String, String))(rawresponse)

            codigoOperacionGrupo = dict("codigoOperacionGrupo")
            estadoOperacionGrupo = dict("estadoOperacionGrupo")
            mensajeOperacionGrupo = dict("mensajeOperacionGrupo")

            Return True
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Function

    Private Function mt_ObtenerCodigoAutorizacion() As String
        Try
            Dim key As String = String.Empty

            Dim request As HttpWebRequest
            Dim response As HttpWebResponse
            Dim reader As StreamReader
            Dim rawresponse As String

            If Not (ConfigurationManager.AppSettings("CorreoUsatActivo") = 1) Then
                'DEV
                request = DirectCast(WebRequest.Create("http://34.198.218.135:8080/wsplussignertitulousat/FirmaDigital/autenticar/1/USAT"), HttpWebRequest)
            Else
                'PRD
                request = DirectCast(WebRequest.Create("https://services.plussigner.com/wsplussignertitulousat/FirmaDigital/autenticar/1/USAT"), HttpWebRequest)
            End If

            request.Method = "GET"

            response = DirectCast(request.GetResponse(), HttpWebResponse)
            reader = New StreamReader(response.GetResponseStream())

            rawresponse = reader.ReadToEnd()

            Dim json As String = rawresponse

            Dim jss As New JavaScriptSerializer()
            Dim dict As Dictionary(Of String, String) = jss.Deserialize(Of Dictionary(Of String, String))(rawresponse)

            key = dict("key")

            Return key
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
            Return String.Empty
        End Try
    End Function

    Private Sub mt_Exportar(ByVal tipo_exportar As String)
        Try
            If fu_ValidarCargarDatos() Then
                If (Me.cmbTipoDiplomaFiltro.SelectedValue = "E" AndAlso Me.grwListaElectronico.Rows.Count = 0) OrElse _
                    (Me.cmbTipoDiplomaFiltro.SelectedValue = "F" AndAlso Me.grwListaFisico.Rows.Count = 0) Then

                    mt_ShowMessage("No existen registros en la lista a exportar.", MessageType.warning)
                    Exit Sub
                Else
                    Session("frmDescargarExcel.formulario") = "frmEmitirDiplomasProveedor"
                    Session("frmDescargarExcel.nombre_archivo") = "Listado de Diplomas a Emitir"
                    Session("frmDescargarExcel.param01") = Me.cmbTipoDiplomaFiltro.SelectedValue
                    Session("frmDescargarExcel.param02") = Me.cmbSesionConsejoFiltro.SelectedValue
                    Session("frmDescargarExcel.param03") = Me.cmbTipoDenominacionFiltro.SelectedValue
                    Session("frmDescargarExcel.param04") = Me.cmbTipoEmisionFiltro.SelectedValue
                    Session("frmDescargarExcel.param05") = tipo_exportar

                    Me.udpScripts.Update()
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "openwindows", "window.open('../../frmDescargarExcel.aspx');", True)
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "presionarBotonListar", "presionarBotonListar();", True)
                End If
            End If
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Private Function mt_ConverToUTF8(ByVal s As String) As String
        Try
            Dim utf8 As New UTF8Encoding(True, True)

            ' We need to dimension the array, since we'll populate it with 2 method calls.
            Dim bytes(utf8.GetByteCount(s) + utf8.GetPreamble().Length - 1) As Byte
            ' Encode the string.
            Array.Copy(utf8.GetPreamble(), bytes, utf8.GetPreamble().Length)
            utf8.GetBytes(s, 0, s.Length, bytes, utf8.GetPreamble().Length)

            ' Decode the byte array.
            Dim s2 As String = utf8.GetString(bytes, 0, bytes.Length)

            Return s2
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
            Return String.Empty
        End Try
    End Function

#End Region

End Class
