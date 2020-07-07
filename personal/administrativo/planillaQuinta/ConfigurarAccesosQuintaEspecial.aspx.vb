
Partial Class personal_administrativo_SISREQ_PlanillaQuinta_ConfigurarAccesosQuintaEspecial
    Inherits System.Web.UI.Page

    Protected Sub lnkBusquedaAvanzada_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkBusquedaAvanzada.Click
        If lnkBusquedaAvanzada.Text.Trim = "Busqueda Simple" Then
            MostrarBusquedaCeCos(False)
            lnkBusquedaAvanzada.Text = "Busqueda Avanzada"
        Else
            MostrarBusquedaCeCos(True)
            lnkBusquedaAvanzada.Text = "Busqueda Simple"
        End If
    End Sub

    Private Sub MostrarBusquedaCeCos(ByVal valor As Boolean)
        Me.txtBuscaCecos.Visible = valor
        Me.ImgBuscarCecos.Visible = valor
        Me.lblTextBusqueda.Visible = valor
        Me.cboCecos.Visible = Not (valor)
    End Sub

    Protected Sub ImgBuscarCecos_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImgBuscarCecos.Click
        BuscarCeCos()
    End Sub

    Private Sub BuscarCeCos()
        Dim objPre As ClsPresupuesto
        objPre = New ClsPresupuesto
        gvCecos.DataSource = objPre.ConsultaCentroCostosConPermisos(Request.QueryString("ctf"), Request.QueryString("id"), txtBuscaCecos.Text)
        gvCecos.DataBind()
        objPre = Nothing
        Panel3.Visible = True
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim objpre As New ClsPresupuesto
        Dim objPlla As New clsPlanilla
        Dim objfun As New ClsFunciones
        Dim datos As New Data.DataTable
        If Not IsPostBack Then
            '**** Cargar Datos Centro Costos ****
            datos = objpre.ObtenerListaCentroCostos_v2("PQ", Request.QueryString("id"))
            objfun.CargarListas(cboCecos, datos, "codigo_Cco", "descripcion_Cco", ">> Seleccione <<")
            MostrarBusquedaCeCos(False)

            '**** Cargar datos responsable de registro y aprobación **** 
            datos = objPlla.ConsultarPersonal("TO")
            objfun.CargarListas(cboRegistro, datos, "codigo_per", "nombres_per", ">> Seleccione <<")
            objfun.CargarListas(cboAprobacion, datos, "codigo_per", "nombres_per", ">> Seleccione <<")
            Me.Panel3.Visible = False
            ConsultarCentrodeCostosAsignados()
        End If
    End Sub

    Protected Sub gvCecos_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvCecos.SelectedIndexChanged
        cboCecos.SelectedValue = Me.gvCecos.DataKeys.Item(Me.gvCecos.SelectedIndex).Values(0)
        MostrarBusquedaCeCos(False)
        lnkBusquedaAvanzada.Text = "Busqueda Avanzada"
        Panel3.Visible = False
        cboCecos_SelectedIndexChanged(sender, e)
    End Sub

    Protected Sub cboCecos_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboCecos.SelectedIndexChanged
        gvCecos.DataBind()
    End Sub

    Protected Sub cmdGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdGuardar.Click
        Dim objPlla As New clsPlanilla
        Dim rpta As Int32
        rpta = objPlla.AgregarAccesoQuinta(Me.cboCecos.SelectedValue, Me.cboRegistro.SelectedValue, Me.cboAprobacion.SelectedValue)
        If rpta = 0 Then
            lblMensaje.Text = "Este centro de costos ya tiene una persona asignada para aprobación y registro"
            lblMensaje.ForeColor = Drawing.Color.Red
        ElseIf rpta = 1 Then
            lblMensaje.Text = "Se registraron los datos correctamente"
            lblMensaje.ForeColor = Drawing.Color.Blue
        ElseIf rpta = -1 Then
            lblMensaje.Text = "Error al registrar nuevo acceso"
            lblMensaje.ForeColor = Drawing.Color.Red
        End If
        ConsultarCentrodeCostosAsignados()
    End Sub

    Private Sub LimpiarControles()
        Me.cboCecos.SelectedValue = -1
        Me.cboAprobacion.SelectedValue = -1
        Me.cboRegistro.SelectedValue = -1
        Me.lblMensaje.Text = ""
    End Sub

    Protected Sub gvCecosAsignados_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles gvCecosAsignados.RowDeleting
        Dim objPlla As New clsPlanilla
        Dim rpta As String
        rpta = objPlla.QuitarAccesoQuinta(Me.gvCecosAsignados.DataKeys.Item(e.RowIndex).Values(0))
        If rpta <> "" Then
            Me.lblMensaje.Text = rpta
            Me.lblMensaje.ForeColor = Drawing.Color.Blue
        Else
            Me.lblMensaje.Text = "Ocurrió un error al procesar los datos"
            Me.lblMensaje.ForeColor = Drawing.Color.Red
        End If
        ConsultarCentrodeCostosAsignados()
    End Sub

    Private Sub ConsultarCentrodeCostosAsignados()
        Dim objPlla As New clsPlanilla
        gvCecosAsignados.DataSource = objPlla.ConsultarAccesoQuinta
        gvCecosAsignados.DataBind()
    End Sub
 
 
    Protected Sub cmdLimpiar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdLimpiar.Click
        LimpiarControles()
    End Sub
End Class
