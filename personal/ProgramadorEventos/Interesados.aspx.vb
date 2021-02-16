Imports System.Data
Imports System.Collections.Generic

Partial Class Interesados
    Inherits System.Web.UI.Page
    Private C As ClsConectarDatos
    Private tipo As String = "1"
    Private cod_pers As String = "1"
    Private centro_costo As String = " "
    Private test As String = "1"
    Private visibilidad As Boolean = True
    Private tipoUso As String = ""

    Private mn_Filas As Integer = 0
    Private mn_FilasPorPagina As Integer = 0
    Private mn_Pagina As Integer = 1 'Pagina actual
    Private mn_RangoPaginas As Integer = 5 'Máxima cantidad de botones a la derecha e izquierda en el paginador

    Sub New()
        If C Is Nothing Then
            C = New ClsConectarDatos
            C.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        End If
    End Sub

#Region "Eventos"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            AddHandler btnRegistrar.ServerClick, AddressOf btnRegistrar_Click

            tipoUso = Request.QueryString("tipo")

            If Not IsPostBack Then
                ViewState.Item("data") = Nothing

                If tipoUso = "M" Then 'Manual
                    UpdatePanelDest.Visible = False
                    pageHeader.Visible = False

                    Dim codigoPro As Integer = IIf(Request.QueryString("codigoPro") IsNot Nothing, Request.QueryString("codigoPro"), 0)
                End If

                Call CargarTipoEstudio()
            End If
        Catch ex As Exception
            ClientScript.RegisterStartupScript(Me.GetType, "Pop", "<script>mostrarMensaje('" & ex.Message & "', 'danger');</script>")
        End Try
    End Sub

    'Protected Sub Page_LoadComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.LoadComplete
    '    ViewState.Item("Pagina") = mn_Pagina
    'End Sub

    Private Sub btnRegistrar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim aFiltros() As String = hddFiltros.Value.Split("|")
            For Each _aFiltro As String In aFiltros
                Dim aFiltro() As String = _aFiltro.Split("=")
                If aFiltro(0) = "codigo_con" AndAlso String.IsNullOrEmpty(aFiltro(1)) Then
                    ScriptManager.RegisterStartupScript(Me.Page, Me.UpdatePanelDest.GetType, "Pop", "<script>mostrarMensaje('Por favor, seleccione una convocatoria de origen', 'danger');</script>", False)
                    UpdatePanelDest.Update()
                    Return
                End If
            Next

            If Me.ddlEventoDest.SelectedValue = "" Then
                ScriptManager.RegisterStartupScript(Me.Page, Me.UpdatePanelDest.GetType, "Pop", "<script>mostrarMensaje('Por favor, seleccione un evento destino', 'danger');</script>", False)
                UpdatePanelDest.Update()
                Return
            End If

            Dim rpta As Integer
            Dim msg As String = ""
            Dim oSalida As New Object()

            C.IniciarTransaccion()

            Dim codigoEveDest As Integer = Me.ddlEventoDest.SelectedValue 'Código evento destino
            Dim filtros As String = Me.hddFiltros.Value
            Dim codigoPer As Integer = Session("id_per")
            oSalida = C.Ejecutar("PRO_EnviarInteresadosPotenciales", codigoEveDest, filtros, codigoPer, "0", "")
            rpta = oSalida(0).ToString()

            If oSalida.Length > 0 Then
                If rpta = 1 Then
                    ScriptManager.RegisterStartupScript(Me.Page, Me.UpdatePanelDest.GetType, "Pop", "<script>mostrarMensaje('La operación fue realizada con éxito', 'success');</script>", False)
                    LimpiarControlesDestino()
                Else
                    msg = oSalida(1).ToString()

                    ScriptManager.RegisterStartupScript(Me.Page, Me.UpdatePanelDest.GetType, "Pop", "<script>mostrarMensaje('" & msg & "', 'danger');</script>", False)
                End If
            End If
            C.TerminarTransaccion()

        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me.Page, Me.UpdatePanelDest.GetType, "Pop", "<script>mostrarMensaje('" & ex.Message & "', 'danger');</script>", False)
        End Try
    End Sub

    Protected Sub ddlTipoEstudioDest_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlTipoEstudioDest.SelectedIndexChanged
        Me.ddlEventoDest.Items.Clear()
        Me.ddlEventoDest.Items.Add(New ListItem("--Seleccione--", ""))
        If Me.ddlTipoEstudioDest.SelectedValue <> "" Then
            Call CargarConvocatorias("D")
        Else
            Me.ddlConvocatoriaDest.Items.Clear()
            Me.ddlConvocatoriaDest.Items.Add(New ListItem("--Seleccione--", ""))
        End If
    End Sub

    Protected Sub ddlConvocatoriaDest_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlConvocatoriaDest.SelectedIndexChanged
        If Me.ddlConvocatoriaDest.SelectedValue <> "" Then
            Call CargarEventos("D")
        Else
            Me.ddlEventoDest.Items.Clear()
            Me.ddlEventoDest.Items.Add(New ListItem("--Seleccione--", ""))
        End If
    End Sub
#End Region

#Region "Métodos"
    Private Sub CargarTipoEstudio()
        Dim obj As New ClsCRM
        Dim dt As New Data.DataTable
        dt = obj.ListaTipoEstudio("TO", 0)

        Me.ddlTipoEstudioDest.Items.Clear()
        Me.ddlTipoEstudioDest.Items.Add(New ListItem("--Seleccione--", ""))

        If dt.Rows.Count > 0 Then
            For i As Integer = 0 To dt.Rows.Count - 1
                Dim Lista As New ListItem(dt.Rows(i).Item("descripcion_test").ToString, dt.Rows(i).Item("codigo_test").ToString)
                Me.ddlTipoEstudioDest.Items.Add(Lista)
            Next
            Me.ddlTipoEstudioDest.Items.Add(New ListItem("TODOS", "%"))
            Call ddlTipoEstudioDest_SelectedIndexChanged(Nothing, Nothing)
        End If
    End Sub

    Private Sub CargarConvocatorias(ByVal tipo As String)
        Dim obj As New ClsCRM
        Dim dt As New Data.DataTable

        dt = obj.ListaConvocatorias("C", 0, Me.ddlTipoEstudioDest.SelectedValue)
        Me.ddlConvocatoriaDest.Items.Clear()
        Me.ddlConvocatoriaDest.Items.Add(New ListItem("--Seleccione--", ""))

        If dt.Rows.Count > 0 Then
            For i As Integer = 0 To dt.Rows.Count - 1
                Dim Lista As New ListItem(dt.Rows(i).Item("descripcion").ToString, dt.Rows(i).Item("codigo").ToString)
                Me.ddlConvocatoriaDest.Items.Add(Lista)
            Next
            Me.ddlConvocatoriaDest.Items.Add(New ListItem("TODOS", "%"))
            UpdatePanelDest.Update()
        End If
    End Sub

    Private Sub CargarEventos(ByVal tipo As String)
        Dim obj As New ClsCRM
        Dim dt As New Data.DataTable

        dt = obj.ListaEventos("C", 0, Me.ddlTipoEstudioDest.SelectedValue, Me.ddlConvocatoriaDest.SelectedValue)
        Me.ddlEventoDest.Items.Clear()
        Me.ddlEventoDest.Items.Add(New ListItem("--Seleccione--", ""))

        If dt.Rows.Count > 0 Then
            For i As Integer = 0 To dt.Rows.Count - 1
                Dim Lista As New ListItem(dt.Rows(i).Item("descripcion").ToString, dt.Rows(i).Item("codigo").ToString)
                Me.ddlEventoDest.Items.Add(Lista)
            Next
            UpdatePanelDest.Update()
        End If
    End Sub

    Private Sub LimpiarControlesDestino()
        'ddlConvocatoriaDest.SelectedValue = ""
        'ddlConvocatoriaDest_SelectedIndexChanged(Nothing, Nothing)
        ddlEventoDest.SelectedValue = ""
        UpdatePanelDest.Update()
    End Sub
#End Region
End Class
