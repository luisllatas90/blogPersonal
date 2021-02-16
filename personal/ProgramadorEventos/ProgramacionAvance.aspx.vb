Imports System.Data

Partial Class Programacion
    Inherits System.Web.UI.Page
    Private C As ClsConectarDatos
    Private isPost As Boolean = False

    Sub New()
        If C Is Nothing Then
            C = New ClsConectarDatos
            C.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSATIMPORT").ConnectionString
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        AddHandler btnFiltrar.ServerClick, AddressOf btnFiltrar_Click

        If Not IsPostBack Then
            Call CargarTipoEstudio()
            If Not isPost Then Call CargarConvocatorias()
            If Not isPost Then Call CargarEventos()
            Call MostrarDetalle()
        End If

        isPost = True
    End Sub

    Protected Sub ddlTipoEstudio_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlTipoEstudio.SelectedIndexChanged
        Me.ddlEvento.Items.Clear()
        Me.ddlEvento.Items.Add(New ListItem("--Seleccione--", ""))
        If Me.ddlTipoEstudio.SelectedValue <> "" Then
            Call CargarConvocatorias()
            Call CargarEventos()
        Else
            Me.ddlConvocatoria.Items.Clear()
            Me.ddlConvocatoria.Items.Add(New ListItem("--Seleccione--", ""))
        End If
    End Sub

    Protected Sub ddlConvocatoria_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlConvocatoria.SelectedIndexChanged
        If Me.ddlConvocatoria.SelectedValue <> "" Then
            Call CargarEventos()
        Else
            Me.ddlEvento.Items.Clear()
            Me.ddlEvento.Items.Add(New ListItem("--Seleccione--", ""))
        End If
    End Sub

    Private Sub CargarTipoEstudio()
        Dim obj As New ClsCRM
        Dim dt As New Data.DataTable
        dt = obj.ListaTipoEstudio("TO", 0)
        Me.ddlTipoEstudio.Items.Clear()
        Me.ddlTipoEstudio.Items.Add(New ListItem("--Seleccione--", ""))
        If dt.Rows.Count > 0 Then
            For i As Integer = 0 To dt.Rows.Count - 1
                Dim Lista As New ListItem(dt.Rows(i).Item("descripcion_test").ToString, dt.Rows(i).Item("codigo_test").ToString)
                Me.ddlTipoEstudio.Items.Add(Lista)
            Next
            Me.ddlTipoEstudio.Items.Add(New ListItem("TODOS", "%"))
            Me.ddlTipoEstudio.SelectedValue = "2" 'PREGRADO
        End If

        'If Not isPost Then Me.ddlTipoEstudio.SelectedIndex = Me.ddlTipoEstudio.Items.Count - 1
    End Sub

    Private Sub CargarConvocatorias()
        Dim obj As New ClsCRM
        Dim dt As New Data.DataTable
        dt = obj.ListaConvocatorias("C", 0, Me.ddlTipoEstudio.SelectedValue)
        Me.ddlConvocatoria.Items.Clear()
        Me.ddlConvocatoria.Items.Add(New ListItem("--Seleccione--", ""))
        If dt.Rows.Count > 0 Then
            For i As Integer = 0 To dt.Rows.Count - 1
                Dim Lista As New ListItem(dt.Rows(i).Item("descripcion").ToString, dt.Rows(i).Item("codigo").ToString)
                Me.ddlConvocatoria.Items.Add(Lista)
            Next
            Me.ddlConvocatoria.Items.Add(New ListItem("TODOS", "%"))
            Me.ddlConvocatoria.SelectedIndex = 0
        End If
    End Sub

    Private Sub CargarEventos()
        Dim obj As New ClsCRM
        Dim dt As New Data.DataTable
        dt = obj.ListaEventos("C", 0, Me.ddlTipoEstudio.SelectedValue, Me.ddlConvocatoria.SelectedValue)
        Me.ddlEvento.Items.Clear()
        Me.ddlEvento.Items.Add(New ListItem("--Seleccione--", ""))
        If dt.Rows.Count > 0 Then
            For i As Integer = 0 To dt.Rows.Count - 1
                Dim Lista As New ListItem(dt.Rows(i).Item("descripcion").ToString, dt.Rows(i).Item("codigo").ToString)
                Me.ddlEvento.Items.Add(Lista)
            Next
            Me.ddlEvento.Items.Add(New ListItem("TODOS", "%"))
            Me.ddlEvento.SelectedValue = "%"
        End If

        'If Not isPost Then Me.ddlEvento.SelectedIndex = Me.ddlEvento.Items.Count - 1
    End Sub

    Protected Sub btnFiltrar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Call MostrarDetalle()
        Catch ex As Exception
            Response.Write("<script>alert(' " & ex.Message.ToString() & " ');</script>")
        End Try
    End Sub

    Private Sub MostrarDetalle()
        Try
            Dim dt As New Data.DataTable("")
            Dim cod_conv As String = "-1"
            Dim evento As String = "-1"

            If Me.ddlConvocatoria.SelectedValue <> "" Then
                If Me.ddlConvocatoria.SelectedValue.Equals("%") Then
                    cod_conv = "%"
                Else
                    cod_conv = Me.ddlConvocatoria.SelectedItem.ToString.Trim
                End If
            End If

            If Me.ddlEvento.SelectedValue <> "" Then
                If Me.ddlEvento.SelectedValue.Equals("%") Then
                    evento = "%"
                Else
                    evento = Me.ddlEvento.SelectedItem.ToString.Trim
                End If
            End If

            C.AbrirConexion()
            dt = C.TraerDataTable("PRO_ProgramacionEventoAvance", cod_conv, evento)

            Me.grwEvento.DataSource = dt
            Me.grwEvento.DataBind()
            dt.Dispose()
            C.CerrarConexion()

            udpEvento.Update()
        Catch ex As Exception
            Response.Write("<script>alert('" & ex.Message & "');</script>")
        End Try
    End Sub

    Public Function PintarAvanceDIV(ByVal porc As Object) As String
        Dim style As String = ""

        If porc Is Nothing Then
            Return ""
        End If

        If CInt(porc) <= 25 Then
            style = "progress-bar-danger"
        ElseIf CInt(porc) > 25 And CInt(porc) <= 50 Then
            style = "progress-bar-warning"
        ElseIf CInt(porc) > 50 And CInt(porc) <= 75 Then
            style = "progress-bar-info"
        Else
            style = "progress-bar-success"
        End If

        Return style
    End Function

    Public Function CalcularPorcentaje(ByVal enviados As Integer, ByVal total As Integer) As Decimal
        If total = 0 Then
            Return 0
        End If
        Return (enviados / total) * 100
    End Function

End Class
