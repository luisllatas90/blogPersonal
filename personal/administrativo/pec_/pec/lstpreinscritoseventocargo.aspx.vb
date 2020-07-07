
Partial Class lstpreinscritoseventocargo
    Inherits System.Web.UI.Page
    Protected Sub cmdNuevo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdNuevo.Click
        Dim pagina As String
        Me.ValidarSegunModulo(pagina)
        If pagina <> "" Then
            Response.Redirect(pagina & "&accion=A")
        End If
    End Sub
    Protected Sub grwListaPersonas_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grwListaPersonas.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim fila As Data.DataRowView
            fila = e.Row.DataItem

            e.Row.Cells(10).Text = "<a href='frmVerCargosAbonos.aspx?cco=" & Request.QueryString("cco") & "&pso=" & fila.Row("codigo_pso") & "&KeepThis=true&TB_iframe=true&height=400&width=700&modal=true' title='Cambiar estado' class='thickbox'>&nbsp;<img src='../../../images/previo.gif' border=0 /><a/>"
            Dim pagina As String
            Me.ValidarSegunModulo(pagina)
            If pagina <> "" Then
                e.Row.Cells(11).Text = "<a href='" & pagina & "&accion=M&pso=" & fila.Row("codigo_pso") & "&tcl=" & fila.Row("tcl") & "&cli=" & fila.Row("cli") & "&KeepThis=true&TB_iframe=true&height=460&width=700&modal=true' title='Cambiar estado' class='thickbox'>&nbsp;<img src='../../../images/editar.gif' border=0 /><a/>"
            End If
            e.Row.Cells(0).Text = e.Row.RowIndex + 1
        End If
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            CargarInscritosConCargo()
            ConsultarDatosEvento()
            Me.cmdNuevoJuridica.Visible = Request.QueryString("mod") = 0
            Me.cmdReporte.OnClientClick = "AbrirPopUp('rpteinscritoseventocargo.aspx?cco=" & Request.QueryString("cco") & "','600','800','yes','yes','yes','yes')"
            If Request.QueryString("ctf") = 1 Then
                Me.cmdNuevoPersonaSinCargo.Visible = True
            End If
        End If
    End Sub
    Protected Sub cmdActualizar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdActualizar.Click
        CargarInscritosConCargo()
    End Sub
    Private Sub CargarInscritosConCargo()
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Me.grwListaPersonas.DataSource = obj.TraerDataTable("EVE_ConsultarInscritos", Request.QueryString("cco"))
        Me.grwListaPersonas.DataBind()
        obj.CerrarConexion()
        obj = Nothing
    End Sub
    Private Sub ValidarSegunModulo(ByRef Resultado As String)
        'Valida según el módulo para mostrar el botón de inscripción de Persona / E-PRE
        Select Case Request.QueryString("mod")
            Case "1" 'Epre
                Resultado = "../frmpersonaepre.aspx?mod=" & Request.QueryString("mod") & "&cco=" & Me.Request.QueryString("cco") & "&ctf=" & Request.QueryString("ctf") & "&id=" & Request.QueryString("id")
            Case Else 'Persona siempre
                Resultado = "../frmpersona.aspx?mod=" & Request.QueryString("mod") & "&cco=" & Me.Request.QueryString("cco") & "&ctf=" & Request.QueryString("ctf") & "&id=" & Request.QueryString("id")
        End Select
    End Sub
    Protected Sub cmdNuevoJuridica_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdNuevoJuridica.Click
        Response.Redirect("../frmpersonajuridica.aspx?mod=" & Request.QueryString("mod") & "&cco=" & Me.Request.QueryString("cco") & "&ctf=" & Request.QueryString("ctf") & "&id=" & Request.QueryString("id"))
    End Sub
    Protected Sub cmdNuevoPersonaSinCargo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdNuevoPersonaSinCargo.Click
        Response.Redirect("../frmpersonasincargo.aspx?accion=A&mod=" & Request.QueryString("mod") & "&cco=" & Me.Request.QueryString("cco") & "&ctf=" & Request.QueryString("ctf") & "&id=" & Request.QueryString("id"))
    End Sub
    Private Sub ConsultarDatosEvento()
        Dim obj As New ClsConectarDatos
        Dim dtsDatosEvento As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        dtsDatosEvento = obj.TraerDataTable("[EVE_ConsultarEventos]", 0, Request.QueryString("cco"), "")
        obj.CerrarConexion()
        obj = Nothing
        If DateAdd(DateInterval.Day, 1, dtsDatosEvento.Rows(0).Item("fechafinpropuesta_dev")) < Now() Then
            cmdNuevo.Enabled = False
            cmdNuevoJuridica.Enabled = False
        End If
    End Sub
End Class
