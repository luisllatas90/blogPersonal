﻿
Partial Class academico_horarios_frmConsultarAulasDisponibles
    Inherits System.Web.UI.Page


    Sub CargarCombos()
        Dim obj As New clsaccesodatos
        obj.abrirconexion()
        Me.comboCiclo.DataSource = obj.TraerDataTable("ListarCicloAcademicos")
        obj.cerrarconexion()
        Me.comboCiclo.DataTextField = "descripcion_cac"
        Me.comboCiclo.DataValueField = "codigo_cac"
        Me.comboCiclo.DataBind()

        obj.abrirconexion()
        Me.comboAmbiente.DataSource = obj.TraerDataTable("ListarAmbientes")
        obj.cerrarconexion()
        Me.comboAmbiente.DataTextField = "descripcion_tam"
        Me.comboAmbiente.DataValueField = "codigo_tam"        
        Me.comboAmbiente.DataBind()
        Me.comboAmbiente.SelectedValue = 0

        obj.abrirconexion()
        Me.comboUbicacion.DataSource = obj.TraerDataTable("ListarUbicacion")
        obj.cerrarconexion()
        Me.comboUbicacion.DataTextField = "descripcion_ube"
        Me.comboUbicacion.DataValueField = "codigo_ube"
        Me.comboUbicacion.DataBind()
        Me.comboUbicacion.SelectedValue = -1

        Me.comboDia.Items.Add("LU")
        Me.comboDia.Items.Add("MA")
        Me.comboDia.Items.Add("MI")
        Me.comboDia.Items.Add("JU")
        Me.comboDia.Items.Add("VI")
        Me.comboDia.Items.Add("SA")

        For i As Integer = 1 To 23
            Me.comboDesde.Items.Add(i.ToString)
            Me.comboHasta.Items.Add(i.ToString)
        Next

        Me.comboHasta.SelectedValue = "23"

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (Session("id_per") Is Nothing) Then
            Response.Redirect("../../../sinacceso.html")
        End If

        If IsPostBack = False Then
            CargarCombos()
        End If
    End Sub

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim obj As New clsaccesodatos
        obj.abrirconexion()
        Dim tb As New Data.DataTable
        tb = obj.TraerDataTable("ConsultaAulaDisponible", CInt(Me.comboCiclo.SelectedValue), CInt(Me.comboAmbiente.SelectedValue), CInt(Me.comboUbicacion.SelectedValue), CInt(Me.txtCapacidad.Text), Me.comboDia.SelectedItem.Text, CInt(Me.comboDesde.SelectedItem.Text), CInt(Me.comboHasta.SelectedItem.Text))
        If tb.Rows.Count Then
            Me.lblmensaje.Text = ""
            Me.gridAulas.DataSource = tb
            Me.gridAulas.DataBind()
            Me.lblmensaje.Text = "Se encontraron (" & tb.Rows.Count.ToString & ") ambientes disponibles."
        Else
            Me.gridAulas.DataSource = Nothing
            Me.gridAulas.DataBind()
            Me.lblmensaje.Text = "No se encontraron ambientes disponibles"
        End If
        'Me.lblmensaje.Text = (Me.comboCiclo.SelectedValue) & (Me.comboAmbiente.SelectedValue) & (Me.comboUbicacion.SelectedValue) & (Me.txtCapacidad.Text) & Me.comboDia.SelectedItem.Text & (Me.comboDesde.SelectedItem.Text) & (Me.comboHasta.SelectedItem.Text)
        obj.cerrarconexion()
        obj = Nothing
    End Sub

    Protected Sub gridAulas_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridAulas.RowDataBound
        'Dim strRuta As String = "tblhorarioambienteAsignado.asp?codigo_amb=" & e.Row.Cells(0).Text & "&codigo_cac=" & Me.comboCiclo.SelectedValue

        Dim strRuta As String = "tblhorarioambienteAsignadoNoConfirmado.asp?codigo_amb=" & e.Row.Cells(0).Text & "&codigo_cac=" & Me.comboCiclo.SelectedValue
        e.Row.Cells(6).Text = "<center><a style=""text-decoration:none;"" target='_blank' href='" & strRuta & "'>Horario</a></center>"

    End Sub
End Class
