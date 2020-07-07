﻿
Partial Class indicadores_POA_FrmListaEvaluarPresupuesto
    Inherits System.Web.UI.Page
    Dim nombre_poa As String = String.Empty
    Dim CurrentRow1 As Integer = -1
    'Dim usuario, ctf As Integer

    Sub CargaPlanes()
        Dim obj As New clsPlanOperativoAnual
        Dim dtPEI As New Data.DataTable
        dtPEI = obj.ListaPeis
        Me.ddlplan.DataSource = dtPEI
        Me.ddlplan.DataTextField = "descripcion"
        Me.ddlplan.DataValueField = "codigo"
        Me.ddlplan.DataBind()
        dtPEI.Dispose()
        obj = Nothing
    End Sub

    Sub CargaEjercicio()
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

    Sub CargaPoas(ByVal codigo_pla As Integer, ByVal codigo_ejp As Integer)
        Dim obj As New clsPlanOperativoAnual
        Dim dtt As New Data.DataTable
        dtt = obj.ListaPoasxInstanciaEstado(codigo_pla, codigo_ejp, "PTO", Request.QueryString("id"), Request.QueryString("ctf"))
        Me.ddlPoa.DataSource = dtt
        Me.ddlPoa.DataTextField = "descripcion"
        Me.ddlPoa.DataValueField = "codigo"
        Me.ddlPoa.DataBind()
        dtt.Dispose()
        obj = Nothing
        'Me.ddlPoa.Items.Insert(0, New ListItem("--SELECCIONE--", "0"))
    End Sub

    Sub CargaActividades(ByVal codigo_pla As Integer, ByVal codigo_poa As Integer, ByVal codigo_ejp As Integer)
        Dim obj As New clsPlanOperativoAnual
        Dim dt As New Data.DataTable
        dt = obj.POA_PermisosEdicionPresupuesto(codigo_pla, codigo_poa, codigo_ejp, Request.QueryString("id"), Request.QueryString("ctf"))
        Me.dgvActividades.DataSource = dt
        Me.dgvActividades.DataBind()
        obj = Nothing
    End Sub

    Protected Sub form1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles form1.Load
        Try
            If Session("id_per") = "" Or Request.QueryString("id") = "" Then
                Response.Redirect("../../../sinacceso.html")
            End If

            If IsPostBack = False Then
                CargaPlanes()
                CargaEjercicio()
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub btnBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBuscar.Click
        Try
            Me.aviso.Attributes.Remove("class")
            Me.lblrpta.Text = ""
            CargaActividades(Me.ddlplan.SelectedValue, Me.ddlPoa.SelectedValue, Me.ddlEjercicio.SelectedValue)
            If Me.dgvActividades.Rows.Count > 0 Then
                Me.cmdGuardar.Visible = True
            Else
                Me.cmdGuardar.Visible = False
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try

    End Sub


    Protected Sub ddlplan_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlplan.SelectedIndexChanged
        CargaPoas(Me.ddlplan.SelectedValue, Me.ddlEjercicio.SelectedValue)
    End Sub

    Protected Sub ddlEjercicio_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlEjercicio.SelectedIndexChanged
        CargaPoas(Me.ddlplan.SelectedValue, Me.ddlEjercicio.SelectedValue)
    End Sub

    Protected Sub chbSeleccionTodo_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
        For Each fila As GridViewRow In Me.dgvActividades.Rows
            Dim seleccion As CheckBox = DirectCast(fila.FindControl("checkbody"), CheckBox)
            If DirectCast(sender, CheckBox).Checked = True Then
                seleccion.Checked = True
            Else

                seleccion.Checked = False
            End If
        Next
    End Sub

    Protected Sub cmdGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdGuardar.Click
        Dim obj As New clsPlanOperativoAnual
        Dim dt As New Data.DataTable
        Try

            For i As Integer = 0 To Me.dgvActividades.Rows.Count - 1
                Dim chk As CheckBox = DirectCast(dgvActividades.Rows(i).Cells(2).FindControl("checkbody"), CheckBox)
                If chk.Checked Then
                    dt = obj.POA_ActivaEdicionPresupuesto(Me.dgvActividades.DataKeys.Item(i).Value, 1)
                Else
                    dt = obj.POA_ActivaEdicionPresupuesto(Me.dgvActividades.DataKeys.Item(i).Value, 0)
                End If
            Next
            Me.aviso.Attributes.Add("class", "mensajeExito")
            Me.lblrpta.Text = "Se Concedió Permisos Correctamente"
        Catch ex As Exception
            Me.aviso.Attributes.Add("class", "mensajeError")
            Me.lblrpta.Text = "No se Pudo Conceder Permisos"
            Response.Write(ex.Message)
        End Try
        
    End Sub
End Class
