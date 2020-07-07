Imports ClsPensiones
Imports System.Web.Script.Serialization

Partial Class administrativo_gestion_educativa_frmConfigCargosIngresantesPregrado
    Inherits System.Web.UI.Page

#Region "Propiedades"
    Private md_ConfiguracionProgramacionCargo As New d_PenConfiguracionProgramacionCargo
#End Region

#Region "Eventos"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Session("id_per") = "684" 'PENDIENTE BORRAR!
        If Not IsPostBack Then
            If Not (Session("id_per") <> "" Or Request.QueryString("id") <> "") Then
                Response.Redirect("../../../sinacceso.html")
            End If

            Dim ln_CodigoCpc As String = IIf(String.IsNullOrEmpty(Request.QueryString("cpc")), Request.QueryString("cpc"), 0)
            mt_InitForm(ln_CodigoCpc)
        Else
            If Not (Session("id_per") <> "" Or Request.QueryString("id") <> "") Then
                mt_GenerarMensajeServidor("Advertencia", 0, "Se ha perdido la sesión")
            End If

            mt_LimpiarParametros()
            mt_LimpiarMensajeServidor()
        End If
    End Sub
#End Region

#Region "Métodos"
    Private Sub mt_InitForm(ByVal ln_CodigoCpc As Integer)
        Try
            ln_CodigoCpc = 1 'PENDIENTE BORRAR!

            Dim lo_ConfiguracionProgramacionCargo As New e_PenConfiguracionProgramacionCargo
            With lo_ConfiguracionProgramacionCargo
                .operacion = "GEN"
                .codigo_cpc = ln_CodigoCpc
            End With

            Dim lo_Dt As Data.DataTable = md_ConfiguracionProgramacionCargo.Listar(lo_ConfiguracionProgramacionCargo)
            If lo_Dt.Rows.Count > 0 Then
                hddJsonConfig.Value = lo_Dt.Rows(0).Item("otrosParametros_cpc")
            End If
        Catch ex As Exception
            mt_GenerarMensajeServidor("Error", -1, ex.Message)
        End Try
    End Sub

    Private Sub mt_GenerarFilas(ByVal ln_CodigoCpc As Integer)
        Try
            If ln_CodigoCpc = 0 Then
                '1 Fila por defecto
            Else
                'Cargo las filas dinámicamente
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub mt_LimpiarParametros()
        hddTipoVista.Value = ""
        hddParamsToastr.Value = ""
        udpParams.Update()
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
#End Region

End Class
