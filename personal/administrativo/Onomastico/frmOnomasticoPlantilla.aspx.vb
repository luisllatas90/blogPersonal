Imports System.Net
Imports System.IO
Imports System.Data

Partial Class administrativo_Onomastico_frmOnomasticoPlantilla
    Inherits System.Web.UI.Page

#Region "Declaracion de Variables"
    'ENTIDADES
    Dim me_PlantillaOnomastico As e_PlantillaOnomastico

    'DATOS
    Dim md_Funciones As New d_Funciones
    Dim md_PlantillaOnomastico As New d_PlantillaOnomastico

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
            If (Session("id_per") Is Nothing OrElse Session("perlogin") Is Nothing) Then
                Response.Redirect("../../../sinacceso.html")
            End If

            cod_user = Session("id_per")

            If IsPostBack = False Then
                Call mt_CargarFormularioPlantilla()
            End If
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Protected Sub btnGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            If Not mt_RegistrarPlantilla() Then
                Exit Sub
            End If

            Call mt_CargarFormularioPlantilla()
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Protected Sub btnVerHeader_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnVerHeader.Click
        Try
            Call mt_FlujoModal("Header", "open")
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Protected Sub btnVerFooter_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnVerFooter.Click
        Try
            Call mt_FlujoModal("Footer", "open")
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Protected Sub btnVisualizar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnVisualizar.Click
        Try
            Call mt_FlujoModal("Tarjeta", "open")
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

    Private Sub mt_LimpiarControles()
        Try
            Me.txtHeader.Text = String.Empty
            Me.imgHeader.ImageUrl = g_VariablesGlobales.RutaPlantillaOnomastico + "sinimagen.png"
            Me.imgTarjetaHeader.ImageUrl = g_VariablesGlobales.RutaPlantillaOnomastico + "sinimagen.png"
            Me.txtFooter.Text = String.Empty
            Me.imgFooter.ImageUrl = g_VariablesGlobales.RutaPlantillaOnomastico + "sinimagen.png"
            Me.imgTarjetaFooter.ImageUrl = g_VariablesGlobales.RutaPlantillaOnomastico + "sinimagen.png"
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Private Function mt_RegistrarPlantilla() As Boolean
        Try
            If Not fu_ValidarRegistrarPlantilla() Then Return False

            me_PlantillaOnomastico = md_PlantillaOnomastico.GetPlantillaOnomastico(0)

            Dim ls_ruta As String = Server.MapPath("..\..\..\filesOnomastico\img")
            Dim ls_nombreHeader As String = String.Empty
            Dim ls_nombreFooter As String = String.Empty

            'Cargar Header
            If (Me.txtCargarHeader.HasFile = True) Then
                ls_nombreHeader = "HEADER-" & Date.Today.Year.ToString & Date.Today.Month.ToString & Date.Today.Day.ToString & _
                                   Date.Today.Hour.ToString & Date.Today.Minute.ToString & DateTime.Now.Millisecond.ToString & _
                                   System.IO.Path.GetExtension(Me.txtCargarHeader.FileName).ToString

                Me.txtCargarHeader.PostedFile.SaveAs(ls_ruta & "\" & ls_nombreHeader)
            Else
                ls_nombreHeader = Me.txtHeader.Text.Trim
            End If

            'Cargar Footer
            If (Me.txtCargarFooter.HasFile = True) Then
                ls_nombreFooter = "FOOTER-" & Date.Today.Year.ToString & Date.Today.Month.ToString & Date.Today.Day.ToString & _
                                   Date.Today.Hour.ToString & Date.Today.Minute.ToString & DateTime.Now.Millisecond.ToString & _
                                   System.IO.Path.GetExtension(Me.txtCargarFooter.FileName).ToString

                Me.txtCargarFooter.PostedFile.SaveAs(ls_ruta & "\" & ls_nombreFooter)
            Else
                ls_nombreFooter = Me.txtFooter.Text.Trim
            End If

            With me_PlantillaOnomastico
                .operacion = "I"
                .imgHeader_plo = ls_nombreHeader
                .imgFooter_plo = ls_nombreFooter
                .cod_user = cod_user
            End With

            md_PlantillaOnomastico.RegistrarPlantillaOnomastico(me_PlantillaOnomastico)

            Call mt_ShowMessage("¡La plantilla se registro exitosamente!", MessageType.success)

            Return True
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Function

    Private Function fu_ValidarRegistrarPlantilla() As Boolean
        Try
            Dim ls_extensiones As String = ".png .jpeg .jpg"

            If Not Me.txtCargarHeader.HasFile AndAlso String.IsNullOrEmpty(Me.txtHeader.Text.Trim) Then mt_ShowMessage("Debe ingresar una cabecera", MessageType.warning) : Return False
            If Not Me.txtCargarFooter.HasFile AndAlso String.IsNullOrEmpty(Me.txtFooter.Text.Trim) Then mt_ShowMessage("Debe ingresar un pie de página", MessageType.warning) : Return False
            If Me.txtCargarHeader.HasFile AndAlso Not ls_extensiones.Contains(System.IO.Path.GetExtension(Me.txtCargarHeader.FileName).ToString().Trim.ToLower) Then mt_ShowMessage("Debe ingresar una cabecera con extensión .png o .jpg o .jpeg .", MessageType.warning) : Return False
            If Me.txtCargarFooter.HasFile AndAlso Not ls_extensiones.Contains(System.IO.Path.GetExtension(Me.txtCargarFooter.FileName).ToString().Trim.ToLower) Then mt_ShowMessage("Debe ingresar un pie de página con extensión .png o .jpg o .jpeg .", MessageType.warning) : Return False

            Return True
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Function

    Private Function mt_CargarFormularioPlantilla() As Boolean
        Try
            Dim dt As New Data.DataTable : me_PlantillaOnomastico = New e_PlantillaOnomastico

            Call mt_LimpiarControles()

            With me_PlantillaOnomastico
                .operacion = "GEN"
                .vigente_plo = "S"
            End With

            dt = md_PlantillaOnomastico.ListarPlantillaOnomastico(me_PlantillaOnomastico)

            If dt.Rows.Count > 0 Then
                With dt.Rows(0)
                    Me.txtHeader.Text = .Item("imgHeader_plo").ToString.Trim
                    Me.imgHeader.ImageUrl = g_VariablesGlobales.RutaPlantillaOnomastico + Me.txtHeader.Text
                    Me.imgTarjetaHeader.ImageUrl = g_VariablesGlobales.RutaPlantillaOnomastico + Me.txtHeader.Text
                    Me.txtFooter.Text = .Item("imgFooter_plo").ToString.Trim
                    Me.imgFooter.ImageUrl = g_VariablesGlobales.RutaPlantillaOnomastico + Me.txtFooter.Text
                    Me.imgTarjetaFooter.ImageUrl = g_VariablesGlobales.RutaPlantillaOnomastico + Me.txtFooter.Text
                End With
            End If

            Return True
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Function

#End Region

End Class
