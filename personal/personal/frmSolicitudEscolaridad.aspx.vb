Imports System.Net
Imports System.IO
Imports System.Data
Imports System.Collections.Generic

Partial Class personal_frmSolicitudEscolaridad
    Inherits System.Web.UI.Page

#Region "Declaracion de Variables"
    'ENTIDADES
    Dim me_DerechoHabientes As e_DerechoHabientes
    Dim me_SolicitudEscolaridad As e_SolicitudEscolaridad

    'DATOS
    Dim md_DerechoHabientes As New d_DerechoHabientes
    Dim md_SolicitudEscolaridad As New d_SolicitudEscolaridad
    Dim md_Funciones As New d_Funciones
    Dim md_ArchivoCompartido As New d_ArchivoCompartido
    Dim md_ArchivoCompartidoDetalle As New d_ArchivoCompartidoDetalle    

    'VARIABLES
    Dim cod_user As Integer = 0
    Dim cod_ctf As Integer = 0
    Dim lst_DerechoHabientes As New List(Of e_DerechoHabientes)

    Dim lb_Cronograma As Boolean = False
    Dim lb_Requisitos As Boolean = False
    Dim lb_RegistroSolicitud As Boolean = False

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
                Response.Redirect("../../sinacceso.html")
            End If

            If IsPostBack = False Then
                Session("lst_DerechoHabientes") = lst_DerechoHabientes
            End If

            cod_user = Session("id_per")
            cod_ctf = Request.QueryString("ctf")

            lb_Cronograma = fu_ValidarCronograma()
            lb_Requisitos = fu_ValidarRequisitos()

            Call fu_ValidarRestricciones()

            Call fu_MostrarBotones()

            If IsPostBack = False Then                                                
                Call mt_CargarDatos()
                Call mt_CargarDatosSolicitud()
                Call mt_CargarComboNivelEscolaridad()
            End If
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Protected Sub cmbNivelEscolaridad_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbNivelEscolaridad.SelectedIndexChanged
        Try
            Call mt_CargarComboGrado()

            Call mt_UpdatePanel("AgregarDerechoHabiente")
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Protected Sub grwLista_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grwLista.RowCommand
        Try
            Dim index As Integer = 0 : index = CInt(e.CommandArgument)
            Dim codigo_dhab As Integer = CInt(Me.grwLista.DataKeys(index).Values("codigo_dhab").ToString)
            Dim nombre As String = Me.grwLista.DataKeys(index).Values("derechohabiente").ToString
            Dim edad As String = Me.grwLista.DataKeys(index).Values("Edad").ToString
            Dim IdArchivosCompartidosRecibo As Integer = CInt(Me.grwLista.DataKeys(index).Values("IdArchivosCompartidosRecibo").ToString)
            Dim IdArchivosCompartidosDNI As Integer = CInt(Me.grwLista.DataKeys(index).Values("IdArchivosCompartidosDNI").ToString)

            Select Case e.CommandName
                Case "Agregar"
                    Call mt_LimpiarControles("AgregarDerechoHabiente")

                    Me.txtNombre.Text = nombre
                    Me.txtCodigoDhab.Value = codigo_dhab
                    Me.txtEdad.Text = edad

                    Call mt_UpdatePanel("AgregarDerechoHabiente")

                    Call mt_FlujoModal("AgregarDerechoHabiente", "open")

                Case "Recibo"
                    Call mt_DescargarArchivo(IdArchivosCompartidosRecibo)

                Case "DNI"
                    Call mt_DescargarArchivo(IdArchivosCompartidosDNI)

            End Select
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Protected Sub btnAgregarDerechoHabiente_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try            
            If Not mt_AgregarDerechoHabiente() Then
                Call mt_CargarDatos()
                Call mt_CargarDatosSolicitud()                                
                Call mt_UpdatePanel("AgregarDerechoHabiente")
                Call mt_FlujoModal("AgregarDerechoHabiente", "open")
                Exit Sub
            End If

            Call fu_MostrarBotones()

            Call mt_FlujoModal("AgregarDerechoHabiente", "close")
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Protected Sub grwListaSolicitud_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grwListaSolicitud.RowCommand
        Try
            Dim index As Integer = 0 : index = CInt(e.CommandArgument)
            Dim codigo_dhab As Integer = CInt(Me.grwListaSolicitud.DataKeys(index).Values("codigo_dhab").ToString)
            Dim IdArchivosCompartidosRecibo As Integer = CInt(Me.grwListaSolicitud.DataKeys(index).Values("IdArchivosCompartidosRecibo").ToString)
            Dim IdArchivosCompartidosDNI As Integer = CInt(Me.grwListaSolicitud.DataKeys(index).Values("IdArchivosCompartidosDNI").ToString)

            Select Case e.CommandName
                Case "Quitar"
                    Call mt_QuitarDerechoHabiente(codigo_dhab)
                    Call mt_CargarDatosSolicitud()
                    Call fu_MostrarBotones()

                Case "Recibo"
                    Call mt_DescargarArchivo(IdArchivosCompartidosRecibo)

                Case "DNI"
                    Call mt_DescargarArchivo(IdArchivosCompartidosDNI)

            End Select
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Protected Sub btnGenerar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGenerar.Click
        Try            
            If mt_RegistrarSolicitud() Then
                lst_DerechoHabientes = New List(Of e_DerechoHabientes)
                Session("lst_DerechoHabientes") = Me.lst_DerechoHabientes

                Call mt_CargarDatosSolicitud()                
            End If

            Call fu_MostrarBotones()
            Call mt_CargarDatos()            
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Protected Sub grwLista_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grwLista.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then                
                Dim IdArchivosCompartidosRecibo As Integer = CInt(e.Row.DataItem("IdArchivosCompartidosRecibo").ToString)
                Dim IdArchivosCompartidosDNI As Integer = CInt(e.Row.DataItem("IdArchivosCompartidosDNI").ToString)

                Dim estado_soe As String = e.Row.DataItem("estado_soe").ToString
                Dim codigo_dhab As String = e.Row.DataItem("codigo_dhab").ToString

                Dim btnVerRecibo As LinkButton
                btnVerRecibo = e.Row.Cells(4).FindControl("btnVerRecibo")

                Dim btnVerDNI As LinkButton
                btnVerDNI = e.Row.Cells(4).FindControl("btnVerDNI")

                Dim btnAgregar As LinkButton
                btnAgregar = e.Row.Cells(4).FindControl("btnAgregar")

                If IdArchivosCompartidosRecibo = 0 Then
                    btnVerRecibo.Style.Add("display", "none")
                End If

                If IdArchivosCompartidosDNI = 0 Then
                    btnVerDNI.Style.Add("display", "none")
                End If

                If Not String.IsNullOrEmpty(estado_soe) OrElse Not lb_Cronograma OrElse _
                    Not lb_Requisitos OrElse lb_RegistroSolicitud Then
                    btnAgregar.Style.Add("display", "none")
                Else
                    If Not Session("lst_DerechoHabientes") Is Nothing Then
                        Me.lst_DerechoHabientes = Session("lst_DerechoHabientes")

                        If lst_DerechoHabientes.Count > 0 Then
                            Dim lb_encontro As Boolean = False

                            For Each le_DerechoHabientes As e_DerechoHabientes In lst_DerechoHabientes
                                If le_DerechoHabientes.codigo_dhab = codigo_dhab Then lb_encontro = True
                            Next

                            If lb_encontro Then
                                btnAgregar.Style.Add("display", "none")
                            Else
                                btnAgregar.Style.Add("display", "initial")
                            End If
                        Else
                            btnAgregar.Style.Add("display", "initial")
                        End If
                    Else
                        btnAgregar.Style.Add("display", "initial")
                    End If
                End If
            End If
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Protected Sub btnDescargarPDF_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDescargarPDF.Click
        Try
            If fu_ValidarRestricciones() Then
                Me.udpScripts.Update()
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "openwindows", "window.open('../../librerianet/escolaridad/frmSolEscolaridad.aspx?id=" & cod_user & "&ctf=" & cod_ctf & "&xdownload=yes');", True)
            End If

            Call mt_CargarDatos()
            Call mt_CargarDatosSolicitud()
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
                Case "Lista"
                    Me.udpLista.Update()
                    If Me.grwLista.Rows.Count > 0 Then ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "udpListaUpdate", "udpListaUpdate();", True)

                Case "ListaSolicitud"
                    Me.udpListaSolicitud.Update()
                    If Me.grwListaSolicitud.Rows.Count > 0 Then ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "udpListaSolicitudUpdate", "udpListaSolicitudUpdate();", True)

                Case "AgregarDerechoHabiente"
                    Me.udpAgregarDerechoHabiente.Update()
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "udpAgregarDerechoHabienteUpdate", "udpAgregarDerechoHabienteUpdate();", True)

                Case "Botones"
                    Me.udpBotones.Update()

            End Select
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Private Sub mt_FlujoModal(ByVal ls_modal As String, ByVal ls_accion As String)
        Try
            Select Case ls_accion
                Case "open"
                    Me.udpScripts.Update()
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "openModal", "openModal('" & ls_modal & "');", True)

                Case "close"
                    Me.udpScripts.Update()
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "closeModal", "closeModal('" & ls_modal & "');", True)

            End Select
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Private Sub mt_LimpiarControles(ByVal ls_tab As String)
        Try
            Select Case ls_tab
                Case "AgregarDerechoHabiente"
                    Me.txtCodigoDhab.Value = 0
                    Me.txtNombre.Text = String.Empty
                    Me.txtEdad.Text = String.Empty
                    Me.cmbNivelEscolaridad.SelectedValue = 0
                    Me.cmbGrado.SelectedValue = 0
                    Me.txtCentroEstudios.Text = String.Empty
                    Me.txtArchivoRecibo.Dispose()
                    Me.txtArchivoDNI.Dispose()

            End Select
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Private Sub mt_CargarComboNivelEscolaridad()
        Try
            Dim dt As New Data.DataTable : me_SolicitudEscolaridad = New e_SolicitudEscolaridad

            dt = md_SolicitudEscolaridad.ListarNivelEscolaridad(me_SolicitudEscolaridad)

            Call md_Funciones.CargarCombo(Me.cmbNivelEscolaridad, dt, "Codigo_niv", "Descripcion_niv", False, "-- SELECCIONE --", "0")
            dt.Dispose()
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Private Sub mt_CargarComboGrado()
        Try
            Dim dt As New Data.DataTable : me_SolicitudEscolaridad = New e_SolicitudEscolaridad

            With me_SolicitudEscolaridad
                .tipocentroestudio_soe = Me.cmbNivelEscolaridad.SelectedValue
            End With

            dt = md_SolicitudEscolaridad.ListarGrados(me_SolicitudEscolaridad)

            Call md_Funciones.CargarCombo(Me.cmbGrado, dt, "Codigo_gra", "Rango_gra", True, "-- SELECCIONE --", "0")
            dt.Dispose()
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Private Sub mt_CargarDatos()
        Try
            Dim dt As New DataTable : me_DerechoHabientes = New e_DerechoHabientes

            If Me.grwLista.Rows.Count > 0 Then Me.grwLista.DataSource = Nothing : Me.grwLista.DataBind()

            With me_DerechoHabientes
                .operacion = "SOL"
                .codigo_per = cod_user
            End With

            dt = md_DerechoHabientes.ListarDerechoHabientes(me_DerechoHabientes)

            Me.grwLista.DataSource = dt
            Me.grwLista.DataBind()

            Call md_Funciones.AgregarHearders(grwLista)

            Call mt_UpdatePanel("Lista")
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Private Sub mt_CargarDatosSolicitud()
        Try
            Me.lst_DerechoHabientes  = Session("lst_DerechoHabientes") 
            Dim dt As New DataTable()

            dt.Columns.Add("codigo_dhab", Type.GetType("System.String"))
            dt.Columns.Add("derechohabiente", Type.GetType("System.String"))
            dt.Columns.Add("Edad", Type.GetType("System.String"))
            dt.Columns.Add("codigo_niv", Type.GetType("System.String"))
            dt.Columns.Add("nivel", Type.GetType("System.String"))
            dt.Columns.Add("codigo_gra", Type.GetType("System.String"))
            dt.Columns.Add("grado", Type.GetType("System.String"))
            dt.Columns.Add("centro_estudios", Type.GetType("System.String"))
            dt.Columns.Add("IdArchivosCompartidosRecibo", Type.GetType("System.String"))
            dt.Columns.Add("IdArchivosCompartidosDNI", Type.GetType("System.String"))

            If Me.lst_DerechoHabientes.Count > 0 Then
                For Each le_DerechoHabientes As e_DerechoHabientes In lst_DerechoHabientes
                    Dim row As DataRow = dt.NewRow
                    row("codigo_dhab") = le_DerechoHabientes.codigo_dhab
                    row("derechohabiente") = le_DerechoHabientes.nombre
                    row("Edad") = le_DerechoHabientes.edad
                    row("codigo_niv") = le_DerechoHabientes.codigo_niv
                    row("nivel") = le_DerechoHabientes.nivel.ToUpper()
                    row("codigo_gra") = le_DerechoHabientes.codigo_gra
                    row("grado") = le_DerechoHabientes.grado.ToUpper()
                    row("centro_estudios") = le_DerechoHabientes.centro_estudios.ToUpper()
                    row("IdArchivosCompartidosRecibo") = le_DerechoHabientes.IdArchivosCompartidosRecibo
                    row("IdArchivosCompartidosDNI") = le_DerechoHabientes.IdArchivosCompartidosDNI
                    dt.Rows.Add(row)
                Next            
            End If

            Me.grwListaSolicitud.DataSource = dt
            Me.grwListaSolicitud.DataBind()

            If dt.Rows.Count > 0 Then Call md_Funciones.AgregarHearders(grwListaSolicitud)

            Call mt_UpdatePanel("ListaSolicitud")
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Private Function mt_AgregarDerechoHabiente() As Boolean
        Try
            Me.lst_DerechoHabientes = Session("lst_DerechoHabientes")

            If Not fu_ValidarAgregarDerechoHabiente() Then Return False

            Dim dt As DataTable
            Dim me_ArchivoCompartidoRecibo As e_ArchivoCompartido = md_ArchivoCompartido.GetArchivoCompartido(0)

            'Archivo adjunto File Shared
            If Me.txtArchivoRecibo.HasFile Then
                dt = New DataTable

                With me_ArchivoCompartidoRecibo
                    .fecha = Date.Now.ToString("dd/MM/yyyy")
                    .ruta_archivo = ConfigurationManager.AppSettings("SharedFiles")
                    .nombre_archivo = Me.txtArchivoRecibo.FileName
                    .id_tabla = md_ArchivoCompartido.ObtenerIdTabla("NX2W1Q3UN4") 'SolicitudEscolaridad
                    .usuario_reg = Session("perlogin").ToString
                    .cod_user = cod_user
                End With

                'Realizar la carga del archivo compartido
                dt = md_ArchivoCompartido.CargarArchivoCompartido(me_ArchivoCompartidoRecibo, Me.txtArchivoRecibo.PostedFile)

                If dt.Rows.Count = 0 Then mt_ShowMessage("No se ha podido cargar el archivo de recibo adjunto.", MessageType.error) : Return False

                'Obtener el id y ruta del archivo compartido
                me_ArchivoCompartidoRecibo.id_archivos_compartidos = dt.Rows(0).Item("IdArchivosCompartidos").ToString
            End If

            Dim me_ArchivoCompartidoDNI As e_ArchivoCompartido = md_ArchivoCompartido.GetArchivoCompartido(0)

            'Archivo adjunto File Shared
            If Me.txtArchivoDNI.HasFile Then
                dt = New DataTable

                With me_ArchivoCompartidoDNI
                    .fecha = Date.Now.ToString("dd/MM/yyyy")
                    .ruta_archivo = ConfigurationManager.AppSettings("SharedFiles")
                    .nombre_archivo = Me.txtArchivoDNI.FileName
                    .id_tabla = md_ArchivoCompartido.ObtenerIdTabla("NX2W1Q3UN4") 'SolicitudEscolaridad
                    .usuario_reg = Session("perlogin").ToString
                    .cod_user = cod_user
                End With

                'Realizar la carga del archivo compartido
                dt = md_ArchivoCompartido.CargarArchivoCompartido(me_ArchivoCompartidoDNI, Me.txtArchivoDNI.PostedFile)

                If dt.Rows.Count = 0 Then mt_ShowMessage("No se ha podido cargar el archivo de DNI adjunto.", MessageType.error) : Return False

                'Obtener el id y ruta del archivo compartido
                me_ArchivoCompartidoDNI.id_archivos_compartidos = dt.Rows(0).Item("IdArchivosCompartidos").ToString
            End If

            me_DerechoHabientes = md_DerechoHabientes.GetDerechoHabientes(0)

            With me_DerechoHabientes
                .codigo_dhab = Me.txtCodigoDhab.Value
                .nombre = Me.txtNombre.Text.Trim
                .edad = Me.txtEdad.Text.Trim
                .codigo_niv = Me.cmbNivelEscolaridad.SelectedValue
                .nivel = Me.cmbNivelEscolaridad.SelectedItem.Text.ToUpper
                .codigo_gra = Me.cmbGrado.SelectedValue
                .grado = Me.cmbGrado.SelectedItem.Text.ToUpper
                .centro_estudios = txtCentroEstudios.Text.Trim
                .IdArchivosCompartidosRecibo = me_ArchivoCompartidoRecibo.id_archivos_compartidos
                .IdArchivosCompartidosDNI = me_ArchivoCompartidoDNI.id_archivos_compartidos
            End With

            lst_DerechoHabientes.Add(me_DerechoHabientes)
            Session("lst_DerechoHabientes") = Me.lst_DerechoHabientes

            Call mt_CargarDatos()
            Call mt_CargarDatosSolicitud()

            Return True
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Function

    Private Function fu_ValidarAgregarDerechoHabiente() As Boolean
        Try
            Me.lst_DerechoHabientes = Session("lst_DerechoHabientes")

            If String.IsNullOrEmpty(Me.txtCodigoDhab.Value) OrElse Me.txtCodigoDhab.Value = 0 Then mt_ShowMessage("El código de derecho habiente no ha sido encontrado.", MessageType.warning) : Return False
            If String.IsNullOrEmpty(Me.txtNombre.Text.Trim) Then mt_ShowMessage("El nombre debe ser diferente de vacío.", MessageType.warning) : Return False
            If String.IsNullOrEmpty(Me.txtEdad.Text.Trim) Then mt_ShowMessage("La edad debe ser diferente de vacío.", MessageType.warning) : Return False
            If String.IsNullOrEmpty(Me.cmbNivelEscolaridad.SelectedValue) OrElse Me.cmbNivelEscolaridad.SelectedValue = 0 Then mt_ShowMessage("Debe seleccionar un nivel de escolaridad.", MessageType.warning) : Return False
            If String.IsNullOrEmpty(Me.cmbGrado.SelectedValue) OrElse Me.cmbGrado.SelectedValue = 0 Then mt_ShowMessage("Debe seleccionar un grado.", MessageType.warning) : Return False
            If String.IsNullOrEmpty(Me.txtCentroEstudios.Text.Trim) Then mt_ShowMessage("Debe ingresar un centro de estudios.", MessageType.warning) : Return False

            If Me.txtArchivoRecibo.HasFile Then
                Dim ls_extensiones As String = ".pdf"
                If Not ls_extensiones.Contains(System.IO.Path.GetExtension(Me.txtArchivoRecibo.FileName).ToString().Trim.ToLower) Then mt_ShowMessage("Debe cargar un archivo con extensión .pdf .", MessageType.warning) : Return False
            Else
                mt_ShowMessage("Debe cargar el recibo de matrícula en formato pdf.", MessageType.warning)
                Return False
            End If

            If Me.txtArchivoDNI.HasFile Then
                Dim ls_extensiones As String = ".pdf"
                If Not ls_extensiones.Contains(System.IO.Path.GetExtension(Me.txtArchivoDNI.FileName).ToString().Trim.ToLower) Then mt_ShowMessage("Debe cargar un archivo con extensión .pdf .", MessageType.warning) : Return False
            Else
                mt_ShowMessage("Debe cargar la copia de DNI en formato pdf.", MessageType.warning)
                Return False
            End If

            For Each le_DerechoHabientes As e_DerechoHabientes In lst_DerechoHabientes
                If le_DerechoHabientes.codigo_dhab = Me.txtCodigoDhab.Value Then
                    mt_ShowMessage("El derecho habiente ya se encuentra en la lista.", MessageType.warning)
                    Return False
                End If
            Next

            Return True
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Function

    Private Function mt_QuitarDerechoHabiente(ByVal codigo_dhab As Integer) As Boolean
        Try            
            Me.lst_DerechoHabientes = Session("lst_DerechoHabientes")

            Dim le_DerechoHabientes As New e_DerechoHabientes

            For Each le_DH As e_DerechoHabientes In lst_DerechoHabientes
                If le_DH.codigo_dhab = codigo_dhab Then
                    le_DerechoHabientes = le_DH
                End If
            Next

            lst_DerechoHabientes.Remove(le_DerechoHabientes)

            Session("lst_DerechoHabientes") = Me.lst_DerechoHabientes

            Call mt_CargarDatos()

            Return True
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Function

    Private Sub mt_DescargarArchivo(ByVal IdArchivosCompartidos As Integer)
        Try
            If IdArchivosCompartidos = 0 Then mt_ShowMessage("No presenta archivo asociado.", MessageType.warning) : Exit Sub

            Me.udpScripts.Update()
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "openwindows", "window.open('../frmDescargarArchivoCompartido.aspx?Id=" & IdArchivosCompartidos & "');", True)
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Private Function mt_RegistrarSolicitud() As Boolean
        Try
            If Not fu_ValidarRegistrarSolicitud() Then Return False

            Dim dt As DataTable
            Me.lst_DerechoHabientes = Session("lst_DerechoHabientes")

            For Each le_DerechoHabientes As e_DerechoHabientes In lst_DerechoHabientes
                me_SolicitudEscolaridad = md_SolicitudEscolaridad.GetSolicitudEscolaridad(0)

                With me_SolicitudEscolaridad
                    .operacion = "I"
                    .codigo_dhab = CInt(le_DerechoHabientes.codigo_dhab)
                    .estado_soe = "P"
                    .tipocentroestudio_soe = le_DerechoHabientes.codigo_niv
                    .nombrecentroestudio_soe = le_DerechoHabientes.centro_estudios.ToUpper()
                    .grado_soe = le_DerechoHabientes.codigo_gra
                    .centroaplicacion_soe = False
                    .documentosadjuntos_soe = "RECIBO DE MATRÍCULA Y COPIA DE DNI"
                    .IdArchivosCompartidosRecibo = CInt(le_DerechoHabientes.IdArchivosCompartidosRecibo)
                    .IdArchivosCompartidosDNI = CInt(le_DerechoHabientes.IdArchivosCompartidosDNI)
                End With

                dt = New Data.DataTable
                dt = md_SolicitudEscolaridad.RegistrarSolicitudEscolaridad(me_SolicitudEscolaridad)

                Dim me_ArchivoCompartidoDetalle As New e_ArchivoCompartidoDetalle

                '==================
                '       RECIBO
                '==================
                Dim me_ArchivoCompartidoRecibo As e_ArchivoCompartido = md_ArchivoCompartido.GetArchivoCompartido(0)

                With me_ArchivoCompartidoRecibo
                    .fecha = Date.Now.ToString("dd/MM/yyyy")
                    .ruta_archivo = ConfigurationManager.AppSettings("SharedFiles")
                    .nombre_archivo = Me.txtArchivoRecibo.FileName
                    .id_tabla = md_ArchivoCompartido.ObtenerIdTabla("NX2W1Q3UN4") 'SolicitudEscolaridad
                    .usuario_reg = Session("perlogin").ToString
                    .cod_user = cod_user
                    .id_archivos_compartidos = CInt(le_DerechoHabientes.IdArchivosCompartidosRecibo)
                End With

                me_ArchivoCompartidoDetalle = md_ArchivoCompartidoDetalle.GetArchivoCompartidoDetalle(0)

                With me_ArchivoCompartidoDetalle
                    .operacion = "I"
                    .tabla_acd = "SolicitudEscolaridad"
                    .codigoTabla_acd = dt.Rows(0).Item("codigo_soe")
                End With

                me_ArchivoCompartidoRecibo.detalles.Add(me_ArchivoCompartidoDetalle)

                'Registrar en la tabla detalle del archivo compartido
                md_ArchivoCompartidoDetalle.RegistrarArchivoCompartidoDetalle(me_ArchivoCompartidoRecibo)

                '==================
                '       DNI
                '==================
                Dim me_ArchivoCompartidoDNI As e_ArchivoCompartido = md_ArchivoCompartido.GetArchivoCompartido(0)

                With me_ArchivoCompartidoDNI
                    .fecha = Date.Now.ToString("dd/MM/yyyy")
                    .ruta_archivo = ConfigurationManager.AppSettings("SharedFiles")
                    .nombre_archivo = Me.txtArchivoDNI.FileName
                    .id_tabla = md_ArchivoCompartido.ObtenerIdTabla("NX2W1Q3UN4") 'SolicitudEscolaridad
                    .usuario_reg = Session("perlogin").ToString
                    .cod_user = cod_user
                    .id_archivos_compartidos = CInt(le_DerechoHabientes.IdArchivosCompartidosDNI)
                End With

                me_ArchivoCompartidoDetalle = md_ArchivoCompartidoDetalle.GetArchivoCompartidoDetalle(0)

                With me_ArchivoCompartidoDetalle
                    .operacion = "I"
                    .tabla_acd = "SolicitudEscolaridad"
                    .codigoTabla_acd = dt.Rows(0).Item("codigo_soe")
                End With

                me_ArchivoCompartidoDNI.detalles.Add(me_ArchivoCompartidoDetalle)

                'Registrar en la tabla detalle del archivo compartido
                md_ArchivoCompartidoDetalle.RegistrarArchivoCompartidoDetalle(me_ArchivoCompartidoDNI)
            Next

            Call mt_ShowMessage("¡El registro se realizó exitosamente!", MessageType.success)

            Return True
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Function

    Private Function fu_ValidarRegistrarSolicitud() As Boolean
        Try
            Me.lst_DerechoHabientes = Session("lst_DerechoHabientes")

            If Not fu_ValidarRestricciones() Then Return False
            If lst_DerechoHabientes.Count = 0 Then mt_ShowMessage("Debe agregar al menos un derecho habiente a la solicitud.", MessageType.warning) : Return False

            Return True
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Function

    Private Function fu_ValidarRequisitos() As Boolean
        Try
            Dim le_DerechoHabientes As New e_DerechoHabientes
            Dim dt As New DataTable

            With le_DerechoHabientes
                .operacion = "VAL"
                .codigo_per = cod_user
            End With

            dt = md_DerechoHabientes.ListarDerechoHabientes(le_DerechoHabientes)

            If dt.Rows.Count > 0 Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Function

    Private Function fu_ValidarCronograma() As Boolean
        Try
            Dim le_DerechoHabientes As New e_DerechoHabientes
            Dim dt As New DataTable

            With le_DerechoHabientes
                .operacion = "CRO"
            End With

            dt = md_DerechoHabientes.ListarDerechoHabientes(le_DerechoHabientes)

            If dt.Rows.Count > 0 Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Function

    Private Function fu_ValidarRestricciones() As Boolean
        Try
            If Not lb_Cronograma Then mt_ShowMessage("Se encuentra fuera del cronograma de registro de solicitud de escolaridad.", MessageType.warning) : Return False
            If Not lb_Requisitos Then mt_ShowMessage("<ol><li>Debe ser trabajador docente, administrativo o de servicios a tiempo completo, medio tiempo o tiempo parcial mayor a veinte horas.</li><br/><li>Debe tener un mínimo de incorporación a la universidad de 6 meses continuos.</li></ol>", MessageType.warning) : Return False

            Return True
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Function

    Private Sub fu_MostrarBotones()
        Try
            Me.btnGenerar.Visible = True
            Me.btnDescargarPDF.Visible = False

            If Not Session("lst_DerechoHabientes") Is Nothing Then
                Me.lst_DerechoHabientes = Session("lst_DerechoHabientes")

                If lst_DerechoHabientes.Count = 0 Then
                    Me.btnGenerar.Visible = False
                End If
            Else
                Me.btnGenerar.Visible = False
            End If

            Dim dt As New DataTable : me_DerechoHabientes = New e_DerechoHabientes

            With me_DerechoHabientes
                .operacion = "SOL"
                .codigo_per = cod_user
            End With

            dt = md_DerechoHabientes.ListarDerechoHabientes(me_DerechoHabientes)

            For Each fila As DataRow In dt.Rows
                If Not String.IsNullOrEmpty(fila("estado_soe").ToString) Then
                    lb_RegistroSolicitud = True
                    Me.btnDescargarPDF.Visible = True
                End If
            Next

            Call mt_UpdatePanel("Botones")
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

#End Region

End Class