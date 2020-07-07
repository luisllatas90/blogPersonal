﻿Partial Class indicadores_POA_FrmListaPlanOperativoAnual
    Inherits System.Web.UI.Page
    Dim LastCategory As String = String.Empty
    Dim CurrentRow As Integer = -1

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("id_per") Is Nothing Or Request.QueryString("id") = "" Then
            Response.Redirect("../../../sinacceso.html")
        End If

        If IsPostBack = False Then
            Call wf_cargarPEI()
            Call wf_cargarEjercicioPresupuestal()
            If Request.QueryString("msj") = "R" Then
                Me.ddlPlan.SelectedValue = Request.QueryString("cb1")
                Me.ddlEjercicio.SelectedValue = Request.QueryString("cb2")
                Me.ddlVigencia.SelectedValue = Request.QueryString("cb3")
                Me.lblrpta.Text = "Datos Registrados Correctamente"
                Me.aviso.Attributes.Add("class", "mensajeExito")
            ElseIf Request.QueryString("msj") = "M" Then
                Me.ddlPlan.SelectedValue = Request.QueryString("cb1")
                Me.ddlEjercicio.SelectedValue = Request.QueryString("cb2")
                Me.ddlVigencia.SelectedValue = Request.QueryString("cb3")
                Me.lblrpta.Text = "Datos Actualizados Correctamente"
                Me.aviso.Attributes.Add("class", "mensajeExito")
            ElseIf Request.QueryString("msj") = "C" Then
                Me.ddlPlan.SelectedValue = Request.QueryString("cb1")
                Me.ddlEjercicio.SelectedValue = Request.QueryString("cb2")
                Me.ddlVigencia.SelectedValue = Request.QueryString("cb3")
            End If
            CargarGrid(Me.ddlPlan.SelectedValue, Me.ddlEjercicio.SelectedValue, Me.ddlVigencia.SelectedValue)
        End If
    End Sub

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
        Try
            'Para Cantidad de Registros
            'Me.lblMensajeFormulario.Text = ""
            'Me.aviso.Visible = False
            CargarGrid(Me.ddlPlan.SelectedValue, Me.ddlEjercicio.SelectedValue, Me.ddlVigencia.SelectedValue)
            Me.aviso.Attributes.Clear()
            Me.lblrpta.Text = ""
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try

    End Sub

    Sub CargarGrid(ByVal plan As Integer, ByVal ejercicio As Integer, ByVal vigencia As Integer)
        Dim obj As New clsPlanOperativoAnual
        Dim dt As New Data.DataTable
        dt = obj.POA_ListarPOAS(IIf(plan = 0, "%", plan), IIf(ejercicio = 0, "%", ejercicio), vigencia)
        obj = Nothing
        If dt.Rows.Count = 0 Then
            'Me.lblMensajeFormulario.Text = "No se encontraron registros"
            Me.gvwPOA.DataSource = Nothing
        Else
            Me.gvwPOA.DataSource = dt
            Me.gvwPOA.Columns.Item(4).Visible = False 'codigo_pla
            Me.gvwPOA.Columns.Item(5).Visible = False 'codigo_ejp
            Me.gvwPOA.Columns.Item(6).Visible = False 'codigo_arp
        End If
        Me.gvwPOA.DataBind()
        dt.Dispose()
        'Me.lblMensajeFormulario.Text = "Se encontraron " & dtt.Rows.Count & " registro(s)."
    End Sub

    Protected Sub btnNuevo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNuevo.Click
        Response.Redirect("FrmMantenimiento_POA.aspx?id=" & Request.QueryString("id") & "&ctf=" & Request.QueryString("ctf") & "&cb1=" & Me.ddlPlan.SelectedValue & "&cb2=" & Me.ddlEjercicio.SelectedValue & "&cb3=" & Me.ddlVigencia.SelectedValue)
    End Sub


    Protected Sub gvwPOA_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvwPOA.RowCommand
        Try
            If (e.CommandName.Equals("Select")) Then
                Dim seleccion As GridViewRow
                Dim codigo_poa As Integer
                Dim obj As New clsPlanOperativoAnual
                '1. Obtengo la linea del gridview que fue cliqueada
                seleccion = DirectCast(e.CommandSource, GridView).Rows(e.CommandArgument)
                '2. Obtengo el datakey de la linea que donde está el boton que cliqueé
                codigo_poa = Me.gvwPOA.DataKeys(seleccion.RowIndex).Values("codigo_poa")
                Response.Redirect("FrmMantenimiento_POA.aspx?id=" & Request.QueryString("id") & "&ctf=" & Request.QueryString("ctf") & "&cp=" & codigo_poa & "&cb1=" & Me.ddlPlan.SelectedValue & "&cb2=" & Me.ddlEjercicio.SelectedValue & "&cb3=" & Me.ddlVigencia.SelectedValue)
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub gvwPOA_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvwPOA.RowDataBound

        If e.Row.RowType = DataControlRowType.DataRow Then

            Dim row As Data.DataRowView = CType(e.Row.DataItem, Data.DataRowView)
            If LastCategory = row("pei") Then

                If (gvwPOA.Rows(CurrentRow).Cells(0).RowSpan = 0) Then
                    gvwPOA.Rows(CurrentRow).Cells(0).RowSpan = 2
                Else
                    gvwPOA.Rows(CurrentRow).Cells(0).RowSpan += 1
                End If
                e.Row.Cells.RemoveAt(0)
            Else
                e.Row.VerticalAlign = VerticalAlign.Middle
                LastCategory = row("pei").ToString()
                CurrentRow = e.Row.RowIndex
            End If
        End If
    End Sub


    Protected Sub gvwPOA_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles gvwPOA.RowDeleting
        Try
            Dim obj As New clsPlanOperativoAnual
            Dim mensaje As String
            mensaje = obj.EliminarPoa(gvwPOA.DataKeys(e.RowIndex).Value.ToString(), Request.QueryString("id"))
            If mensaje = "1" Then
                'Para avisos
                Me.lblrpta.Text = "Registro Eliminado con éxito."
                Me.aviso.Attributes.Add("class", "mensajeEliminado")
            ElseIf mensaje = "2" Then
                'Para avisos
                Me.lblrpta.Text = "No se Pudo Eliminar Registro, Plan Operativo Anual Cuenta Con Centros de Costo Asignados."
                Me.aviso.Attributes.Add("class", "mensajeError")
            Else
                'Para avisos
                Me.lblrpta.Text = "No se Pudo Eliminar Registro."
                Me.aviso.Attributes.Add("class", "mensajeError")
            End If
            'Refresca Busqueda
            CargarGrid(Me.ddlPlan.SelectedValue, Me.ddlEjercicio.SelectedValue, Me.ddlVigencia.SelectedValue)
            'End If

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

End Class
