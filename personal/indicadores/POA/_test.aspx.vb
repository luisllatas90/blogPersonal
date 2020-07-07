﻿
Partial Class indicadores_POA_PROTOTIPOS_Registrar_POA
    Inherits System.Web.UI.Page
    
    Sub wf_cargarPEI()
        Dim obj As New clsPlanOperativoAnual
        Dim dtPEI As New Data.DataTable
        dtPEI = obj.ListaPeis
        Me.ddlPlan.DataSource = dtPEI
        Me.ddlPlan.DataTextField = "descripcion"
        Me.ddlPlan.DataValueField = "codigo"
        Me.ddlPlan.DataBind()
        dtPEI.Dispose()
        obj = Nothing
    End Sub

    Sub wf_cargarEjercicioPresupuestal()
        Dim obj As New clsPlanOperativoAnual
        Dim dtt As New Data.DataTable
        dtt = obj.ListaEjercicio
        Me.ddlEjercicio.DataSource = dtt
        Me.ddlEjercicio.DataTextField = "descripcion"
        Me.ddlEjercicio.DataValueField = "codigo"
        Me.ddlEjercicio.DataBind()
        dtt.Dispose()
        obj = Nothing
        Me.ddlEjercicio.SelectedIndex = Me.ddlEjercicio.Items.Count - 1

    End Sub

    Protected Sub btnBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBuscar.Click
        Dim obj As New clsPlanOperativoAnual
        Dim dt As New Data.DataTable
        dt = obj.POA_ListaPoasActividad(Me.ddlPlan.SelectedValue, Me.ddlEjercicio.SelectedValue, Request.QueryString("id"), Request.QueryString("ctf"), Me.ddlestado.SelectedValue)
        Me.dgvpoa.SelectedIndex = -1
        Me.dgvpoa.DataSource = dt
        Me.dgvpoa.DataBind()
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'If Session("id_per") = "" Or Request.QueryString("id") = "" Then
        '    Response.Redirect("../../../sinacceso.html")
        'End If
        Try
            If IsPostBack = False Then
                wf_cargarPEI()
                wf_cargarEjercicioPresupuestal()
                Me.dgvActividades.DataBind()
            End If

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub dgvpoa_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles dgvpoa.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes.Add("OnMouseOver", "Resaltar(1,this,'S')")
            e.Row.Attributes.Add("OnMouseOut", "Resaltar(0,this,'S')")
            e.Row.Attributes.Add("OnClick", "javascript:__doPostBack('dgvpoa','Select$" & e.Row.RowIndex & "');")
            e.Row.Attributes.Add("Class", "Sel")
            e.Row.Attributes.Add("Typ", "Sel")

            e.Row.Style.Add("cursor", "hand")
        End If

    End Sub

   
    Protected Sub dgvpoa_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgvpoa.SelectedIndexChanged
        Me.hdcodigo_poa.Value = Me.dgvpoa.SelectedDataKey("codigo_poa").ToString

        ClientScript.RegisterStartupScript(Me.GetType, "Mostrar", "TdDetalle.style.visibility='visible';TdNuevo.style.visibility='hidden';", True)
        Me.dgvActividades.DataBind()
    End Sub
End Class
