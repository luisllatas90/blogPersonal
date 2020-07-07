﻿
Partial Class indicadores_POA_FrmMantenimientoAsignarFecha
    Inherits System.Web.UI.Page
    'Dim usuario, ctf As Integer
    Protected Sub form1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles form1.Load
        If (Session("id_per") = "") Then
            Response.Redirect("../../../sinacceso.html")
        End If

        Try
            If IsPostBack = False Then
                Dim codigo_dap As Integer
                codigo_dap = Request.QueryString("cdap")
                CargaDatos(codigo_dap)
                Me.divCalendario.Visible = False
                'usuario = Request.QueryString("id")
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub
    Sub CargaDatos(ByVal codigo_dap As Integer)
        Dim obj As New clsPlanOperativoAnual
        Dim dt As New Data.DataTable
        dt = obj.CargaDatosDetActividad(codigo_dap)
        If dt.Rows.Count > 0 Then
            Me.lblpoa.Text = dt.Rows(0).Item("nombre_poa").ToString.ToUpper
            Me.lblacp.Text = dt.Rows(0).Item("actividad").ToString.ToUpper
            Me.lbldap.Text = dt.Rows(0).Item("descripcion_dap").ToString.ToUpper
            Me.lblfecini.Text = dt.Rows(0).Item("fecini_dap").ToString
            Me.lblfecfin.Text = dt.Rows(0).Item("fecfin_dap").ToString
            Me.txtfecrevisa.Text = dt.Rows(0).Item("hito_dap").ToString
            Me.hdcodigo_dap.Value = dt.Rows(0).Item("codigo_dap").ToString
            If Me.txtfecrevisa.Text <> "" Then
                Dim fec() As String = Me.txtfecrevisa.Text.Split("/")
                Me.Calendario.SelectedDate = New DateTime(fec(2), fec(1), fec(0))
                Me.Calendario.VisibleDate = New DateTime(fec(2), fec(1), fec(0))
            Else
                Dim fec() As String = Me.lblfecini.Text.Split("/")
                'Me.Calendario.SelectedDate = New DateTime(fec(2), fec(1), fec(0))
                Me.Calendario.VisibleDate = New DateTime(fec(2), fec(1), fec(0))
            End If

        End If

    End Sub

    Protected Sub Calendario_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Calendario.SelectionChanged
        If Me.Calendario.SelectedDate >= Me.lblfecini.Text And Me.Calendario.SelectedDate <= Me.lblfecfin.Text Then
            Me.txtfecrevisa.Text = Me.Calendario.SelectedDate.ToString("dd/MM/yyyy")
            Me.divCalendario.Visible = False
            Me.aviso.Attributes.Clear()
            Me.lblmensaje.Text = ""
        Else
            Me.lblmensaje.Text = "Error, Fecha Fuera de Rango de Duración"
            Me.aviso.Attributes.Add("class", "mensajeError")
            Me.Calendario.SelectedDate = New Date(1990, 1, 1)
            Me.txtfecrevisa.Text = ""
            Exit Sub
        End If

    End Sub

    Protected Sub btnGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardar.Click
        Try
            Dim obj As New clsPlanOperativoAnual
            Dim dt As New Data.DataTable
            Dim hdcodigo_dap As Integer
            hdcodigo_dap = Me.hdcodigo_dap.Value
            dt = obj.AsignaFechaRevision(hdcodigo_dap, Me.txtfecrevisa.Text, Request.QueryString("id"))
            If dt.Rows(0).Item("mensaje") = 0 Then
                Me.lblmensaje.Text = "Error, No se Pudo Asignar Fecha de Revisión."
                Me.aviso.Attributes.Add("class", "mensajeError")
            ElseIf dt.Rows(0).Item("mensaje") = 1 Then
                Response.Redirect("FrmListaAsignarRevision.aspx?id=" & Request.QueryString("id") & "&ctf=" & Request.QueryString("ctf") & "&cb1=" & Request.QueryString("cb1") & "&cb2=" & Request.QueryString("cb2") & "&cb3=" & Request.QueryString("cb3") & "&cb4=" & Request.QueryString("cb4") & "&msj=R")
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub btnCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        Response.Redirect("FrmListaAsignarRevision.aspx?id=" & Request.QueryString("id") & "&ctf=" & Request.QueryString("ctf") & "&cb1=" & Request.QueryString("cb1") & "&cb2=" & Request.QueryString("cb2") & "&cb3=" & Request.QueryString("cb3") & "&cb4=" & Request.QueryString("cb4") & "&msj=C")
    End Sub

    Protected Sub btnCalendario_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCalendario.Click
        If Me.divCalendario.Visible = False Then
            Me.divCalendario.Visible = True
        Else
            Me.divCalendario.Visible = False
        End If
        Me.aviso.Attributes.Clear()
        Me.lblmensaje.Text = ""
    End Sub
End Class
