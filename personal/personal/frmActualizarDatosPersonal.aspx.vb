Imports System.Net
Imports System.IO
Imports System.Data
Imports System.Collections.Generic

Partial Class personal_frmActualizarDatosPersonal
    Inherits System.Web.UI.Page

#Region "Declaracion de Variables"
    'ENTIDADES
    Dim me_DatosPersonal As e_DatosPersonal

    'DATOS
    Dim md_Funciones As New d_Funciones
    Dim md_DatosPersonal As New d_DatosPersonal

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
                ScriptManager1.SetFocus(txtOperadorInternet)
            Else
                Me.txtOperadorInternet.ReadOnly = True
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
                ScriptManager1.SetFocus(txtOperadorMovil)
            Else
                Me.txtOperadorMovil.ReadOnly = True                
            End If

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

    Private Sub mt_LimpiarControles()
        Try
            Me.txtEmailPrincipal.Text = String.Empty
            Me.txtEmailAlternativo.Text = String.Empty
            Me.cmbOperadorInternet.SelectedValue = String.Empty
            Me.txtOperadorInternet.Text = String.Empty
            Me.cmbOperadorMovil.SelectedValue = String.Empty
            Me.txtOperadorMovil.Text = String.Empty
            Me.txtCelular.Text = String.Empty
            Me.txtTelefono.Text = String.Empty
            Me.txtDireccion.Text = String.Empty
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

                    If String.IsNullOrEmpty(Me.cmbOperadorInternet.SelectedValue) Then
                        Me.txtOperadorInternet.Text = .operadorInternet_per
                        Me.txtOperadorInternet.ReadOnly = False
                        Me.cmbOperadorInternet.Text = "OTRO"
                    End If
                End If

                If Not String.IsNullOrEmpty(.operadorCelular_per) Then
                    Me.cmbOperadorMovil.Text = .operadorCelular_per

                    If String.IsNullOrEmpty(Me.cmbOperadorMovil.SelectedValue) Then
                        Me.txtOperadorMovil.Text = .operadorCelular_per
                        Me.txtOperadorMovil.ReadOnly = False
                        Me.cmbOperadorMovil.Text = "OTRO"
                    End If
                End If

                Me.txtCelular.Text = .celular_per
                Me.txtTelefono.Text = .telefono_per
                Me.txtDireccion.Text = .direccion_per
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
                .operadorInternet_per = IIf(cmbOperadorInternet.SelectedValue.Trim.Equals("OTRO"), Me.txtOperadorInternet.Text.Trim, cmbOperadorInternet.SelectedValue.Trim)
                .operadorCelular_per = IIf(cmbOperadorMovil.SelectedValue.Trim.Equals("OTRO"), Me.txtOperadorMovil.Text.Trim, cmbOperadorMovil.SelectedValue.Trim)
                .celular_per = Me.txtCelular.Text.Trim
                .telefono_per = Me.txtTelefono.Text.Trim
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
            If String.IsNullOrEmpty(Me.txtDireccion.Text.Trim) Then mt_ShowMessage("Debe ingresar una dirección.", MessageType.warning) : ScriptManager1.SetFocus(txtDireccion) : Return False

            Return True
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Function

#End Region

End Class
