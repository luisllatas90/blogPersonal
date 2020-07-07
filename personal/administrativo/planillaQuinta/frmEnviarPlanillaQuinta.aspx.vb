
Partial Class librerianet_planillaQuinta_frmEnviarPlanillaQuinta
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim objpre As New ClsPresupuesto
            Dim objPlla As New clsPlanilla
            Dim objfun As New ClsFunciones
            Dim datos As New Data.DataTable
            'Cargar datos de Proceso presupuestal
            datos = objpre.ObtenerListaProcesos()
            If datos.Rows.Count > 0 Then
                objfun.CargarListas(cboPeriodoPresu, datos, "codigo_ejp", "descripcion_ejp")
            End If
            'Cargar Datos Planilla
            datos = objPlla.ConsultarPlanilla(Request.QueryString("ctf"))
            objfun.CargarListas(cboPlanilla, datos, "codigo_plla", "PLanilla")
        End If
    End Sub

    Protected Sub cmdEnviar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdEnviar.Click
        Dim objPlla As New clsPlanilla
        Dim rpta As Int32
        rpta = objPlla.EnviarTodaPlanillaQuinta(Me.cboPlanilla.SelectedValue, Request.QueryString("id"), Request.QueryString("ctf"))
        If rpta = -1 Then
            'solo el administrador puede realizar esta función
            ClientScript.RegisterStartupScript(Me.GetType, "No Enviado", "alert('Sólo el administrador puede realizar esta función');", True)
        Else
            'Todas las planillas fueron enviadas a dirección de personal
            ClientScript.RegisterStartupScript(Me.GetType, "Enviado", "alert('Todas las planillas fueron enviadas a dirección de personal');", True)
        End If
    End Sub
End Class
