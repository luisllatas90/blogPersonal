Imports System.Data
Partial Class presupuesto_areas_AgregarPresupuesto
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim objFun As ClsFunciones
            Dim datos As New DataTable
            Dim objPre As ClsPresupuesto
            objFun = New ClsFunciones
            objPre = New ClsPresupuesto

            Me.lblCentroCostos.Text = Request.QueryString("cecos")
            hddCecos.Value = Request.QueryString("cco")
            datos = objPre.ObtenerListaProcesos()
            If datos.Rows.Count > 0 Then
                objFun.CargarListas(cboProceso, datos, "codigo_pct", "descripcion_pct")
            End If
        End If
    End Sub

    Protected Sub cmdAceptar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdAceptar.Click
        Dim objPre As ClsPresupuesto
        Dim rpta As String
        objPre = New ClsPresupuesto
        rpta = objPre.AgregarPresupuesto(hddCecos.Value, cboProceso.SelectedValue, TxtFechaIni.Text, TxtFechaFin.Text, _
                                   Request.QueryString("id"), txtObservacion.Text, 0, 0)
        If rpta.Trim = "1" Then
            ClientScript.RegisterStartupScript(Me.GetType, "correcto", "alert('Se guardaron correctamente los datos');", True)
            ClientScript.RegisterStartupScript(Me.GetType, "redireccionar", "location.href='registrarpresupuesto.aspx?cco=" & hddCecos.Value & "&ctf=" & Request.QueryString("ctf") & "&id=" & Request.QueryString("id") & "';", True)
        ElseIf rpta = "0" Then
            ClientScript.RegisterStartupScript(Me.GetType, "no disponible", "alert('Proceso no disponible')", True)
        Else
            'Response.Write(rpta)
            ClientScript.RegisterStartupScript(Me.GetType, "error", "alert('" & rpta.ToString & "');", True)
        End If
    End Sub

    Protected Sub cmdCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdCancelar.Click
        Response.Redirect("registrarpresupuesto.aspx?cco=" & hddCecos.Value & "&ctf=" & Request.QueryString("ctf") & "&id=" & Request.QueryString("id"))
    End Sub
End Class
