Imports System.Net
Imports System.IO
Imports System.Data
Imports System.Collections.Generic

Partial Class personal_frmActualizarDatosPersonal
    Inherits System.Web.UI.Page

#Region "Declaracion de Variables"
    'ENTIDADES
    Dim me_DatosPersonal As e_DatosPersonal
    Dim me_Departamento As e_Departamento
    Dim me_Provincia As e_Provincia
    Dim me_Distrito As e_Distrito

    'DATOS
    Dim md_Funciones As New d_Funciones
    Dim md_DatosPersonal As New d_DatosPersonal
    Dim md_Departamento As New d_Departamento
    Dim md_Provincia As New d_Provincia
    Dim md_Distrito As New d_Distrito

    'VARIABLES
    Dim cod_user As Integer = 0

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
            If String.IsNullOrEmpty(Request.QueryString("codigo_per")) Then
                Response.Redirect("../../sinacceso.html")
            End If

            cod_user = Request.QueryString("codigo_per").ToString

            If IsPostBack = False Then
                Call mt_CargarComboOperadorMovil()
                Call mt_CargarComboOperadorInternet()
                Call mt_CargarComboDepartamento()
                Call mt_CargarFormularioRegistro(cod_user)
            End If
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Private Sub cmbOperadorInternet_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbOperadorInternet.SelectedIndexChanged
        Try
            Me.txtOperadorInternet.Text = String.Empty

            If cmbOperadorInternet.SelectedValue.Equals("OTRO") Then
                Me.txtOperadorInternet.ReadOnly = False
                Me.txtOperadorInternet.Visible = True
                ScriptManager1.SetFocus(txtOperadorInternet)
            Else
                Me.txtOperadorInternet.ReadOnly = True
                Me.txtOperadorInternet.Visible = False
            End If

            Call mt_UpdatePanel("Registro")

        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Private Sub cmbOperadorMovil_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbOperadorMovil.SelectedIndexChanged
        Try
            Me.txtOperadorMovil.Text = String.Empty

            If cmbOperadorMovil.SelectedValue.Equals("OTRO") Then
                Me.txtOperadorMovil.ReadOnly = False
                Me.txtOperadorMovil.Visible = True
                ScriptManager1.SetFocus(txtOperadorMovil)
            Else
                Me.txtOperadorMovil.ReadOnly = True
                Me.txtOperadorMovil.Visible = False
            End If

            Call mt_UpdatePanel("Registro")
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Protected Sub cmbDepartamento_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbDepartamento.SelectedIndexChanged
        Try
            Call mt_CargarComboProvincia()

            Call mt_UpdatePanel("Registro")
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Protected Sub cmbProvincia_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbProvincia.SelectedIndexChanged
        Try
            Call mt_CargarComboDistrito()

            Call mt_UpdatePanel("Registro")
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Protected Sub btnGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardar.Click
        Try
            If mt_RegistrarDatos(cod_user) Then

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
                Case "Registro"
                    Me.udpRegistro.Update()
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "udpRegistroUpdate", "udpRegistroUpdate();", True)

            End Select
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Private Sub mt_CargarComboOperadorMovil()
        Try
            Dim dt As New Data.DataTable : md_Funciones = New d_Funciones
            dt = md_Funciones.ObtenerDataTable("OPERADOR_MOVIL")

            Call md_Funciones.CargarCombo(Me.cmbOperadorMovil, dt, "codigo", "nombre", True, "[-- SELECCIONE --]", "")
            dt.Dispose()
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Private Sub mt_CargarComboOperadorInternet()
        Try
            Dim dt As New Data.DataTable : md_Funciones = New d_Funciones
            dt = md_Funciones.ObtenerDataTable("OPERADOR_INTERNET")

            Call md_Funciones.CargarCombo(Me.cmbOperadorInternet, dt, "codigo", "nombre", True, "[-- SELECCIONE --]", "")
            dt.Dispose()
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Private Sub mt_CargarComboDepartamento()
        Try
            Dim dt As New Data.DataTable : me_Departamento = New e_Departamento

            With me_Departamento
                .operacion = "GEN"
                .codigo_pai = "156"
            End With
            dt = md_Departamento.ListarDepartamento(me_Departamento)

            Call md_Funciones.CargarCombo(Me.cmbDepartamento, dt, "codigo_Dep", "nombre_Dep", True, "[-- SELECCIONE --]", "0")
            dt.Dispose()
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Private Sub mt_CargarComboProvincia()
        Try
            Dim dt As New Data.DataTable : me_Provincia = New e_Provincia

            With me_Provincia
                .operacion = "GEN"
                If Not String.IsNullOrEmpty(Me.cmbDepartamento.SelectedValue) Then .codigo_dep = Me.cmbDepartamento.SelectedValue
            End With
            dt = md_Provincia.ListarProvincia(me_Provincia)

            Call md_Funciones.CargarCombo(Me.cmbProvincia, dt, "codigo_Pro", "nombre_Pro", True, "[-- SELECCIONE --]", "0")
            dt.Dispose()
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Private Sub mt_CargarComboDistrito()
        Try
            Dim dt As New Data.DataTable : me_Distrito = New e_Distrito

            With me_Distrito
                .operacion = "GEN"
                If Not String.IsNullOrEmpty(Me.cmbProvincia.SelectedValue) Then .codigo_pro = Me.cmbProvincia.SelectedValue
            End With
            dt = md_Distrito.ListarDistrito(me_Distrito)

            Call md_Funciones.CargarCombo(Me.cmbDistrito, dt, "codigo_Dis", "nombre_Dis", True, "[-- SELECCIONE --]", "0")
            dt.Dispose()
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Private Sub mt_LimpiarControles()
        Try
            Me.txtEmailPrincipal.Text = String.Empty
            Me.txtEmailAlternativo.Text = String.Empty
            Me.cmbOperadorInternet.SelectedValue = String.Empty
            Me.txtOperadorInternet.Text = String.Empty
            Me.txtOperadorInternet.ReadOnly = True
            Me.txtOperadorInternet.Visible = False
            Me.cmbOperadorMovil.SelectedValue = String.Empty
            Me.txtOperadorMovil.Text = String.Empty
            Me.txtOperadorMovil.ReadOnly = True
            Me.txtOperadorMovil.Visible = False
            Me.txtCelular.Text = String.Empty
            Me.txtTelefono.Text = String.Empty
            Me.txtDireccion.Text = String.Empty

            Me.cmbDepartamento.SelectedValue = "0"
            Call cmbDepartamento_SelectedIndexChanged(Nothing, Nothing)
            Call cmbProvincia_SelectedIndexChanged(Nothing, Nothing)
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Private Function mt_CargarFormularioRegistro(ByVal codigo_per As Integer) As Boolean
        Try
            me_DatosPersonal = md_DatosPersonal.GetDatosPersonal(codigo_per)

            If me_DatosPersonal.codigo_per = 0 Then mt_ShowMessage("El registro no ha sido encontrado.", MessageType.warning) : Return False

            Call mt_LimpiarControles()

            With me_DatosPersonal                
                Me.txtEmailPrincipal.Text = .email_per
                Me.txtEmailAlternativo.Text = .email_alternativo_per

                If Not String.IsNullOrEmpty(.operadorInternet_per) Then
                    Me.cmbOperadorInternet.Text = .operadorInternet_per

                    If String.IsNullOrEmpty(Me.cmbOperadorInternet.SelectedValue) OrElse _
                        Me.cmbOperadorInternet.SelectedValue.Equals("OTRO") Then
                        Me.cmbOperadorInternet.SelectedValue = "OTRO"
                        Me.txtOperadorInternet.Text = .operadorInternet_per

                        Me.txtOperadorInternet.ReadOnly = False
                        Me.txtOperadorInternet.Visible = True
                    End If
                End If

                If Not String.IsNullOrEmpty(.operadorCelular_per) Then
                    Me.cmbOperadorMovil.Text = .operadorCelular_per

                    If String.IsNullOrEmpty(Me.cmbOperadorMovil.SelectedValue) OrElse _
                        Me.cmbOperadorMovil.SelectedValue.Equals("OTRO") Then
                        Me.cmbOperadorMovil.SelectedValue = "OTRO"
                        Me.txtOperadorMovil.Text = .operadorCelular_per

                        Me.txtOperadorMovil.ReadOnly = False
                        Me.txtOperadorMovil.Visible = True
                    End If
                End If

                Me.txtCelular.Text = .celular_per
                Me.txtTelefono.Text = .telefono_per
                Me.txtDireccion.Text = .direccion_per

                'Obtener datos de ubigeo
                If .distrito <> 0 Then
                    Dim le_Distrito As e_Distrito = md_Distrito.GetDistrito(.distrito)

                    If le_Distrito.codigo_pro <> 0 Then
                        Dim le_Provincia As e_Provincia = md_Provincia.GetProvincia(le_Distrito.codigo_pro)

                        If le_Provincia.codigo_dep <> 0 Then
                            'Llenar los combos de ubigeo
                            Me.cmbDepartamento.SelectedValue = le_Provincia.codigo_dep
                            Call cmbDepartamento_SelectedIndexChanged(Nothing, Nothing)

                            Me.cmbProvincia.SelectedValue = le_Provincia.codigo_pro
                            Call cmbProvincia_SelectedIndexChanged(Nothing, Nothing)

                            Me.cmbDistrito.SelectedValue = le_Distrito.codigo_dis
                        End If
                    End If
                End If
            End With

            Call mt_UpdatePanel("Registro")

            Return True
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Function

    Private Function mt_RegistrarDatos(ByVal codigo_per As Integer) As Boolean
        Try
            If Not fu_ValidarRegistrarDatos() Then Return False

            me_DatosPersonal = md_DatosPersonal.GetDatosPersonal(codigo_per)

            With me_DatosPersonal
                .operacion = "U"
                .codigo_per = codigo_per
                .actualizoDatos_per = "S"
                .email_per = Me.txtEmailPrincipal.Text.Trim
                .email_alternativo_per = Me.txtEmailAlternativo.Text.Trim
                .operadorInternet_per = IIf(cmbOperadorInternet.SelectedValue.Trim.Equals("OTRO"), Me.txtOperadorInternet.Text.Trim.ToUpper, cmbOperadorInternet.SelectedValue.Trim)
                .operadorCelular_per = IIf(cmbOperadorMovil.SelectedValue.Trim.Equals("OTRO"), Me.txtOperadorMovil.Text.Trim.ToUpper, cmbOperadorMovil.SelectedValue.Trim)
                .celular_per = Me.txtCelular.Text.Trim
                .telefono_per = Me.txtTelefono.Text.Trim
                .codigo_pro = Me.cmbProvincia.SelectedValue
                .distrito = Me.cmbDistrito.SelectedValue
                .direccion_per = Me.txtDireccion.Text.Trim.ToUpper
            End With

            md_DatosPersonal.RegistrarDatosPersonal(me_DatosPersonal)

            Call mt_ShowMessage("¡Los datos fueron registrados exitosamente!", MessageType.success)

            Return True
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Function

    Private Function fu_ValidarRegistrarDatos() As Boolean
        Try
            If String.IsNullOrEmpty(Me.txtEmailPrincipal.Text.Trim) OrElse Not md_Funciones.ValidarEmail(Me.txtEmailPrincipal.Text.Trim) Then mt_ShowMessage("Debe ingresar un email principal válido.", MessageType.warning) : ScriptManager1.SetFocus(txtEmailPrincipal) : Return False
            If String.IsNullOrEmpty(Me.txtEmailAlternativo.Text.Trim) OrElse Not md_Funciones.ValidarEmail(Me.txtEmailAlternativo.Text.Trim) Then mt_ShowMessage("Debe ingresar un email alternativo válido.", MessageType.warning) : ScriptManager1.SetFocus(txtEmailAlternativo) : Return False
            If String.IsNullOrEmpty(Me.cmbOperadorInternet.SelectedValue.Trim) Then mt_ShowMessage("Debe seleccionar un operador de internet.", MessageType.warning) : ScriptManager1.SetFocus(cmbOperadorInternet) : Return False
            If Me.cmbOperadorInternet.SelectedValue.Trim.Equals("OTRO") AndAlso String.IsNullOrEmpty(Me.txtOperadorInternet.Text.Trim) Then mt_ShowMessage("Debe ingresar un operador de internet.", MessageType.warning) : ScriptManager1.SetFocus(txtOperadorInternet) : Return False
            If String.IsNullOrEmpty(Me.cmbOperadorMovil.SelectedValue.Trim) Then mt_ShowMessage("Debe seleccionar un operador de telefonía celular.", MessageType.warning) : ScriptManager1.SetFocus(cmbOperadorMovil) : Return False
            If Me.cmbOperadorMovil.SelectedValue.Trim.Equals("OTRO") AndAlso String.IsNullOrEmpty(Me.txtOperadorMovil.Text.Trim) Then mt_ShowMessage("Debe ingresar un operador de telefonía celular.", MessageType.warning) : ScriptManager1.SetFocus(txtOperadorMovil) : Return False
            If String.IsNullOrEmpty(Me.txtCelular.Text.Trim) Then mt_ShowMessage("Debe ingresar un número de celular.", MessageType.warning) : ScriptManager1.SetFocus(txtCelular) : Return False
            If String.IsNullOrEmpty(Me.txtTelefono.Text.Trim) Then mt_ShowMessage("Debe ingresar un número de télefono.", MessageType.warning) : ScriptManager1.SetFocus(txtTelefono) : Return False
            If String.IsNullOrEmpty(Me.cmbDepartamento.SelectedValue.Trim) OrElse cmbDepartamento.SelectedValue = "0" Then mt_ShowMessage("Debe seleccionar un departamento.", MessageType.warning) : ScriptManager1.SetFocus(cmbDepartamento) : Return False
            If String.IsNullOrEmpty(Me.cmbProvincia.SelectedValue.Trim) OrElse cmbProvincia.SelectedValue = "0" Then mt_ShowMessage("Debe seleccionar una provincia.", MessageType.warning) : ScriptManager1.SetFocus(cmbProvincia) : Return False
            If String.IsNullOrEmpty(Me.cmbDistrito.SelectedValue.Trim) OrElse cmbDistrito.SelectedValue = "0" Then mt_ShowMessage("Debe seleccionar un distrito.", MessageType.warning) : ScriptManager1.SetFocus(cmbDistrito) : Return False
            If String.IsNullOrEmpty(Me.txtDireccion.Text.Trim) Then mt_ShowMessage("Debe ingresar una dirección.", MessageType.warning) : ScriptManager1.SetFocus(txtDireccion) : Return False

            Return True
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Function

#End Region

End Class
