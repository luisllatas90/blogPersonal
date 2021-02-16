Imports System.Data
Imports System.Collections.Generic

Partial Class FiltrarInteresados
    Inherits System.Web.UI.Page
    Private C As ClsConectarDatos
    Private tipo As String = "1"
    Private cod_pers As String = "1"
    Private centro_costo As String = " "
    Private test As String = "1"
    Private visibilidad As Boolean = True
    Private tipoUso As String = ""
    Private codigoPro As String = ""

    Sub New()
        If C Is Nothing Then
            C = New ClsConectarDatos
            C.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        End If
    End Sub

#Region "Eventos"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            tipoUso = Request.QueryString("tipo")

            If Not IsPostBack Then
                ViewState.Item("data") = Nothing

                If tipoUso = "M" Then 'Manual
                    'UpdatePanelDest.Visible = False
                End If

                Call CargarTipoEstudio()
                Call CargarCarreraProfesional()
                Call CargarCentroCostos()

                rbtPreferenteTodos.Checked = True

                codigoPro = Request.QueryString("codigoPro")
                If Not String.IsNullOrEmpty(codigoPro) AndAlso codigoPro <> "0" Then
                    CargarDataProgramacion()
                End If
            End If

        Catch ex As Exception
            ClientScript.RegisterStartupScript(Me.GetType, "Pop", "<script>mostrarMensaje('" & ex.Message & "', 'danger');</script>")
        End Try
    End Sub

    Protected Sub Page_LoadComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.LoadComplete
        Call PublicarFiltros()
        MostrarTotalInteresados()
    End Sub

    Protected Sub ddlTipoEstudio_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlTipoEstudio.SelectedIndexChanged
        If Me.ddlTipoEstudio.SelectedValue <> "" Then
            Call CargarConvocatorias("O")
            ddlConvocatoria_SelectedIndexChanged(Nothing, Nothing)

            Call CargarEventos("O")
        Else
            Me.ddlConvocatoria.Items.Clear()
            Me.ddlConvocatoria.Items.Add(New ListItem("--Seleccione--", ""))
        End If
    End Sub

    Protected Sub ddlConvocatoria_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlConvocatoria.SelectedIndexChanged
        If Me.ddlConvocatoria.SelectedValue <> "" Then
            'Call CargarEventos("O")
        Else
            Me.ddlEvento.Items.Clear()
            Me.ddlEvento.Items.Add(New ListItem("--Seleccione--", ""))
        End If

        Call CargarEventos("O")

        Dim tipo As String = "ALL"
        Dim codigo As Integer = 0
        Dim soloSecundaria As Boolean = True
        Dim codigoCon As String = ddlConvocatoria.SelectedValue
        Call CargarInstitucionEducativa(tipo, codigo, soloSecundaria, codigoCon)
    End Sub

    'Protected Sub ddlTipoEstudioDest_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlTipoEstudioDest.SelectedIndexChanged
    '    Me.ddlEventoDest.Items.Clear()
    '    Me.ddlEventoDest.Items.Add(New ListItem("--Seleccione--", ""))
    '    If Me.ddlTipoEstudioDest.SelectedValue <> "" Then
    '        Call CargarConvocatorias("D")
    '    Else
    '        Me.ddlConvocatoriaDest.Items.Clear()
    '        Me.ddlConvocatoriaDest.Items.Add(New ListItem("--Seleccione--", ""))
    '    End If
    'End Sub

    'Protected Sub ddlConvocatoriaDest_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlConvocatoriaDest.SelectedIndexChanged
    '    If Me.ddlConvocatoriaDest.SelectedValue <> "" Then
    '        Call CargarEventos("D")
    '    Else
    '        Me.ddlEventoDest.Items.Clear()
    '        Me.ddlEventoDest.Items.Add(New ListItem("--Seleccione--", ""))
    '    End If
    'End Sub
#End Region

#Region "Métodos"

    Private Sub CargarDataProgramacion()
        C.AbrirConexion()
        Dim dt As Data.DataTable = C.TraerDataTable("PRO_DatosProgramacionEvento", codigoPro)
        If dt.Rows.Count > 0 Then
            Dim filtros As String() = dt.Rows(0).Item("filtros_pro").ToString.Split("|")
            For Each _filtro As String In filtros
                Dim filtro As String() = _filtro.Split("=")
                Dim itemFiltro As String = filtro(0)
                Dim valueFiltro As String = filtro(1)

                Select Case itemFiltro
                    Case "codigo_test"
                        ddlTipoEstudio.SelectedValue = valueFiltro
                        ddlTipoEstudio_SelectedIndexChanged(Nothing, Nothing)
                    Case "codigo_con"
                        ddlConvocatoria.SelectedValue = valueFiltro
                        ddlConvocatoria_SelectedIndexChanged(Nothing, Nothing)
                    Case "codigo_eve"
                        SetValueListbox(ddlEvento, valueFiltro)
                    Case "grado"
                        SetValueListbox(ddlGrados, valueFiltro)
                    Case "codigo_cpf"
                        SetValueListbox(ddlCarreraProfesional, valueFiltro)
                    Case "codigo_ied"
                        SetValueListbox(ddlInstitucionEducativa, valueFiltro)
                    Case "preferente_ied"
                        Select Case valueFiltro
                            Case "-1"
                                rbtPreferenteTodos.Checked = True
                            Case "0"
                                rbtSoloNoPreferente.Checked = True
                            Case "1"
                                rbtSoloPreferente.Checked = True
                        End Select

                    Case "alumno"
                        If valueFiltro = "1" Then
                            cboFiltro.Value = "A"
                        Else
                            cboFiltro.Value = "I"
                        End If
                    Case "codigo_cco"
                        SetValueListbox(ddlCentroCosto, valueFiltro)
                End Select
            Next
        End If
        C.CerrarConexion()
    End Sub

    Private Sub CargarTipoEstudio()
        Dim obj As New ClsCRM
        Dim dt As New Data.DataTable
        dt = obj.ListaTipoEstudio("TO", 0)

        Me.ddlTipoEstudio.Items.Clear()
        'Me.ddlTipoEstudio.Items.Add(New ListItem("--Seleccione--", ""))

        'Me.ddlTipoEstudioDest.Items.Clear()
        'Me.ddlTipoEstudioDest.Items.Add(New ListItem("--Seleccione--", ""))

        If dt.Rows.Count > 0 Then
            For i As Integer = 0 To dt.Rows.Count - 1
                Dim Lista As New ListItem(dt.Rows(i).Item("descripcion_test").ToString, dt.Rows(i).Item("codigo_test").ToString)
                Me.ddlTipoEstudio.Items.Add(Lista)
                Me.ddlTipoEstudio.SelectedValue = 2
                Call ddlTipoEstudio_SelectedIndexChanged(Nothing, Nothing)

                'Me.ddlTipoEstudioDest.Items.Add(Lista)
            Next
            Me.ddlTipoEstudio.Items.Add(New ListItem("TODOS", "%"))
            'Me.ddlTipoEstudioDest.Items.Add(New ListItem("TODOS", "%"))
        End If
    End Sub

    Private Sub CargarConvocatorias(ByVal tipo As String)
        Dim obj As New ClsCRM
        Dim dt As New Data.DataTable

        If tipo.Equals("O") Then 'Origen
            dt = obj.ListaConvocatorias("C", 0, Me.ddlTipoEstudio.SelectedValue)
            Me.ddlConvocatoria.Items.Clear()
            Me.ddlConvocatoria.Items.Add(New ListItem("--Seleccione--", ""))
        Else
            'dt = obj.ListaConvocatorias("C", 0, Me.ddlTipoEstudioDest.SelectedValue)
            'Me.ddlConvocatoriaDest.Items.Clear()
            'Me.ddlConvocatoriaDest.Items.Add(New ListItem("--Seleccione--", ""))
        End If

        If dt.Rows.Count > 0 Then
            For i As Integer = 0 To dt.Rows.Count - 1
                Dim Lista As New ListItem(dt.Rows(i).Item("descripcion").ToString, dt.Rows(i).Item("codigo").ToString)
                Me.ddlConvocatoria.Items.Add(Lista)
            Next
            If tipo.Equals("O") Then
            Else
                'Me.ddlConvocatoriaDest.Items.Add(New ListItem("TODOS", "%"))
            End If
        End If
        udpConvocatoria.Update()
    End Sub

    Private Sub CargarEventos(ByVal tipo As String)
        Dim obj As New ClsCRM
        Dim dt As New Data.DataTable

        If tipo.Equals("O") Then 'Origen
            dt = obj.ListaEventos("C", 0, Me.ddlTipoEstudio.SelectedValue, Me.ddlConvocatoria.SelectedValue)
            Me.ddlEvento.Items.Clear()
            'Me.ddlEvento.Items.Add(New ListItem("--Seleccione--", ""))
        Else
            'dt = obj.ListaEventos("C", 0, Me.ddlTipoEstudioDest.SelectedValue, Me.ddlConvocatoriaDest.SelectedValue)
            'Me.ddlEventoDest.Items.Clear()
            'Me.ddlEventoDest.Items.Add(New ListItem("--Seleccione--", "-1"))
        End If

        If dt.Rows.Count > 0 Then
            For i As Integer = 0 To dt.Rows.Count - 1
                Dim Lista As New ListItem(dt.Rows(i).Item("descripcion").ToString, dt.Rows(i).Item("codigo").ToString)
                Me.ddlEvento.Items.Add(Lista)
            Next
            If tipo.Equals("O") Then
            Else
                'UpdatePanelDest.Update()
            End If
        End If

        udpEvento.Update()
    End Sub

    Private Sub CargarCarreraProfesional()
        Dim dt As New Data.DataTable
        C.AbrirConexion()
        dt = C.TraerDataTable("CRM_ListaCarreraxEventoxTest", "T", 0, 2, "")
        Me.ddlCarreraProfesional.Items.Clear()
        If dt.Rows.Count > 0 Then
            For i As Integer = 0 To dt.Rows.Count - 1
                Dim Lista As New ListItem(dt.Rows(i).Item("nombre_Cpf").ToString, dt.Rows(i).Item("codigo_Cpf").ToString)
                Me.ddlCarreraProfesional.Items.Add(Lista)
            Next
        End If
        C.CerrarConexion()
    End Sub

    Private Sub CargarCentroCostos()
        Dim dt As New Data.DataTable
        C.AbrirConexion()
        dt = C.TraerDataTable("EVE_ConsultarCentroCostosXPermisosXVisibilidad", tipo, cod_pers, centro_costo, test, visibilidad)
        Me.ddlCentroCosto.Items.Clear()
        If dt.Rows.Count > 0 Then
            For i As Integer = 0 To dt.Rows.Count - 1
                Dim Lista As New ListItem(dt.Rows(i).Item("Nombre").ToString, dt.Rows(i).Item("codigo_cco").ToString)
                Me.ddlCentroCosto.Items.Add(Lista)
            Next
            'Me.ddlCentroCosto.Items.Add(New ListItem("TODOS", "%"))
            'Me.ddlCentroCosto.SelectedValue = "%"
        End If
        C.CerrarConexion()
    End Sub

    Private Sub CargarInstitucionEducativa(ByVal tipo As String, ByVal codigo As Integer, ByVal solo_secundaria As Boolean, ByVal codigo_con As String)
        Dim dt As New Data.DataTable
        C.AbrirConexion()

        Me.ddlInstitucionEducativa.Items.Clear()

        If Not String.IsNullOrEmpty(codigo_con) Then
            dt = C.TraerDataTable("CRM_InstitucionEducativaInteresadoPorubicacion", tipo, codigo, solo_secundaria, codigo_con)

            If dt.Rows.Count > 0 Then
                For i As Integer = 0 To dt.Rows.Count - 1
                    Dim Lista As New ListItem(dt.Rows(i).Item("nombre_ubicacion").ToString, dt.Rows(i).Item("codigo_ied").ToString)
                    Me.ddlInstitucionEducativa.Items.Add(Lista)
                Next
            End If
        End If
        udpInstitucionEducativa.Update()
        C.CerrarConexion()
    End Sub

    Private Sub PublicarFiltros()
        Dim codigoTest As String = ddlTipoEstudio.SelectedValue
        Dim codigoCon As String = ddlConvocatoria.SelectedValue
        Dim codigoEve As String = GetValueListbox(ddlEvento)
        Dim codigoOri As String = ""
        Dim grado As String = GetValueListbox(ddlGrados)
        Dim codigoCpf As String = GetValueListbox(ddlCarreraProfesional)
        Dim codigoIed As String = GetValueListbox(ddlInstitucionEducativa)

        Dim preferenteIed As Integer = -1
        If rbtSoloPreferente.Checked Then
            preferenteIed = 1
        End If
        If rbtSoloNoPreferente.Checked Then
            preferenteIed = 0
        End If

        Dim alumno As Integer = IIf(cboFiltro.Value = "A", 1, 0)
        Dim codigoCco As String = GetValueListbox(ddlCentroCosto)
        Dim incluyeNoInteresado As String = "0"

        'andy.diaz  22/04/2020
        Dim fechaDesde As String = txtFechaDesde.Text.Trim()
        Dim fechaHasta As String = txtFechaHasta.Text.Trim()

        hddFiltrosSel.Value = "codigo_test=" & codigoTest _
            & "|" & "codigo_con=" & codigoCon _
            & "|" & "codigo_eve=" & codigoEve _
            & "|" & "codigo_ori=" & codigoOri _
            & "|" & "grado=" & grado _
            & "|" & "codigo_cpf=" & codigoCpf _
            & "|" & "codigo_ied=" & codigoIed _
            & "|" & "preferente_ied=" & preferenteIed _
            & "|" & "alumno=" & alumno _
            & "|" & "codigo_cco=" & codigoCco _
            & "|" & "incluye_no_interesado=" & incluyeNoInteresado _
            & "|" & "fecha_desde=" & fechaDesde _
            & "|" & "fecha_hasta=" & fechaHasta
        udpFiltrosSel.Update()
    End Sub

    Public Sub MostrarTotalInteresados()
        Dim obj As New ClsCRM
        Dim dt As New Data.DataTable

        Dim codigoCon As String = ddlConvocatoria.SelectedValue
        If Not String.IsNullOrEmpty(codigoCon) Then
            dt = obj.ListarInteresadosPorFiltros(hddFiltrosSel.Value)
            Dim cantidad As Integer = dt.Rows.Count

            If cantidad > 0 Then
                Dim texto As String = IIf(cantidad > 1, " Interesados encontrados", " Interesado encontrado")
                totalInteresados.InnerText = dt.Rows.Count & texto
            Else
                totalInteresados.InnerText = "Ningún interesado encontrado"
            End If
        Else
            totalInteresados.InnerText = ""
        End If
        udpTotalInteresados.Update()
    End Sub

    Private Function GetValueListbox(ByVal lbox As ListBox) As String
        Dim value As String = ""
        For Each _item As ListItem In lbox.Items
            If _item.Selected Then
                If Not String.IsNullOrEmpty(value) Then
                    value &= ","
                End If
                value &= _item.Value
            End If
        Next
        Return value
    End Function

    Private Sub SetValueListbox(ByVal lbox As ListBox, ByVal commaValues As String)
        Dim values As String() = commaValues.Split(",")

        For Each _item As ListItem In lbox.Items
            _item.Selected = False
            For Each _value As String In values
                If _value = _item.Value Then
                    _item.Selected = True
                End If
            Next
        Next
    End Sub
#End Region
End Class
