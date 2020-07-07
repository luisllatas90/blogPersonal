﻿
Partial Class indicadores_frmTabPerspectivas
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                CargarComboPlanes()
                CargarComboPeriodos()
                EstadosControlesPrincipal(False)
                tabs.Visible = False
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub EstadosControlesPrincipal(ByVal vEstado As Boolean)
        Try
            ddlCentroCostoPlan.Enabled = vEstado
            ddlPeriodo.Enabled = vEstado
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub


    Private Sub CargarComboPlanes()
        Try
            Dim obj As New clsIndicadores
            Dim dts As New Data.DataTable

            dts = obj.ListarPlanes(Me.Request.QueryString("ctf"), Me.Request.QueryString("id"))

            If dts.Rows.Count > 0 Then
                ddlPlan.DataSource = dts
                ddlPlan.DataValueField = "Codigo"
                ddlPlan.DataTextField = "Descripcion"
                ddlPlan.DataBind()
            End If

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub lnkPers1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkPers1.Click
        Try
            'Esta pagina necesita el codigo_pers y el codigo_plan para cargar sus objetivos - Indicadores
            'EnviarAPagina("frmDetallePerspectivas.aspx?codigo_plan=", lblPers1.Text)
            lnkPers1.ForeColor = Drawing.Color.Red
            lnkPers2.ForeColor = Drawing.Color.Blue
            lnkPers3.ForeColor = Drawing.Color.Blue
            lnkPers4.ForeColor = Drawing.Color.Blue
            lnkPers5.ForeColor = Drawing.Color.Blue
            lnkPers6.ForeColor = Drawing.Color.Blue

            EnviarAResumenPerspectivas("frmResumenPerspectivas.aspx")

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub


    Private Sub EnviarAResumenPerspectivas(ByVal pagina As String)
        Try
            'Me.fradetalle.Attributes("src") = pagina & "&id=" & Request.QueryString("id") & "&ctf=" & Request.QueryString("ctf") & "&cco=" & Me.cboCecos.SelectedValue
            Me.fradetalle.Attributes("src") = pagina & "?Codigo_pla=" & ddlPlan.SelectedValue & "&anio=" & ddlPeriodo.SelectedValue & "&codigo_cco=" & ddlCentroCostoPlan.SelectedValue
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub lnkPers2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkPers2.Click
        Try

            'Esta pagina necesita el codigo_pers y el codigo_plan para cargar sus objetivos - Indicadores
            lnkPers1.ForeColor = Drawing.Color.Blue
            lnkPers2.ForeColor = Drawing.Color.Red
            lnkPers3.ForeColor = Drawing.Color.Blue
            lnkPers4.ForeColor = Drawing.Color.Blue
            lnkPers5.ForeColor = Drawing.Color.Blue
            lnkPers6.ForeColor = Drawing.Color.Blue

            'EnviarAPagina("frmDetallePerspectivas.aspx?codigo_plan=", lblPers2.Text)

            '---------------------------------------------------------------------------------------------------------------
            'Fecha: 29.10.2012
            'Usuario: dguevara
            'Modificacion: Se modifico el http://www.usat.edu.pe por http://intranet.usat.edu.pe
            '----------------------
            'Response.Write("plan: " & ddlPlan.SelectedValue)
            'Response.Write("<br />")
            'Response.Write("pers: " & lblPers2.Text)
            'Response.Write("<br />")
            'Response.Write("ceco: " & ddlCentroCostoPlan.SelectedValue)
            'Response.Write("<br />")
            'Response.Write("Año: " & Me.ddlPeriodo.SelectedValue)

            Me.fradetalle.Attributes("src") = "//intranet.usat.edu.pe/rptusat/?/PRIVADOS/ACADEMICO/IND_CuadroMandoIntegral" & "&codigo_pla=" & ddlPlan.SelectedValue & "&codigo_pers=" & lblPers2.Text & "&anio=" & Me.ddlPeriodo.SelectedValue & "&codigo_cco=" & ddlCentroCostoPlan.SelectedValue

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub lnkPers3_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkPers3.Click
        Try
            'Esta pagina necesita el codigo_pers y el codigo_plan para cargar sus objetivos - Indicadores
            lnkPers1.ForeColor = Drawing.Color.Blue
            lnkPers2.ForeColor = Drawing.Color.Blue
            lnkPers3.ForeColor = Drawing.Color.Red
            lnkPers4.ForeColor = Drawing.Color.Blue
            lnkPers5.ForeColor = Drawing.Color.Blue
            lnkPers6.ForeColor = Drawing.Color.Blue

            Me.fradetalle.Attributes("src") = "//intranet.usat.edu.pe/rptusat/?/PRIVADOS/ACADEMICO/IND_CuadroMandoIntegral" & "&codigo_pla=" & ddlPlan.SelectedValue & "&codigo_pers=" & lblPers3.Text & "&anio=" & Me.ddlPeriodo.SelectedValue & "&codigo_cco=" & ddlCentroCostoPlan.SelectedValue
            'EnviarAPagina("frmDetallePerspectivas.aspx?codigo_plan=", lblPers3.Text)
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub lnkPers4_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkPers4.Click
        Try
            'Esta pagina necesita el codigo_pers y el codigo_plan para cargar sus objetivos - Indicadores
            lnkPers1.ForeColor = Drawing.Color.Blue
            lnkPers2.ForeColor = Drawing.Color.Blue
            lnkPers3.ForeColor = Drawing.Color.Blue
            lnkPers4.ForeColor = Drawing.Color.Red
            lnkPers5.ForeColor = Drawing.Color.Blue
            lnkPers6.ForeColor = Drawing.Color.Blue

            Me.fradetalle.Attributes("src") = "//intranet.usat.edu.pe/rptusat/?/PRIVADOS/ACADEMICO/IND_CuadroMandoIntegral" & "&codigo_pla=" & ddlPlan.SelectedValue & "&codigo_pers=" & lblPers4.Text & "&anio=" & Me.ddlPeriodo.SelectedValue & "&codigo_cco=" & ddlCentroCostoPlan.SelectedValue
            'EnviarAPagina("frmDetallePerspectivas.aspx?codigo_plan=", lblPers4.Text)
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub lnkPers5_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkPers5.Click
        Try
            'Esta pagina necesita el codigo_pers y el codigo_plan para cargar sus objetivos - Indicadores
            lnkPers1.ForeColor = Drawing.Color.Blue
            lnkPers2.ForeColor = Drawing.Color.Blue
            lnkPers3.ForeColor = Drawing.Color.Blue
            lnkPers4.ForeColor = Drawing.Color.Blue
            lnkPers5.ForeColor = Drawing.Color.Red
            lnkPers6.ForeColor = Drawing.Color.Blue

            Me.fradetalle.Attributes("src") = "//intranet.usat.edu.pe/rptusat/?/PRIVADOS/ACADEMICO/IND_CuadroMandoIntegral" & "&codigo_pla=" & ddlPlan.SelectedValue & "&codigo_pers=" & lblPers5.Text & "&anio=" & Me.ddlPeriodo.SelectedValue & "&codigo_cco=" & ddlCentroCostoPlan.SelectedValue
            'EnviarAPagina("frmDetallePerspectivas.aspx?codigo_plan=", lblPers5.Text)
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub lnkPers6_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkPers6.Click
        Try
            'Esta pagina necesita el codigo_pers y el codigo_plan para cargar sus objetivos - Indicadores
            lnkPers1.ForeColor = Drawing.Color.Blue
            lnkPers2.ForeColor = Drawing.Color.Blue
            lnkPers3.ForeColor = Drawing.Color.Blue
            lnkPers4.ForeColor = Drawing.Color.Blue
            lnkPers5.ForeColor = Drawing.Color.Blue
            lnkPers6.ForeColor = Drawing.Color.Red

            Me.fradetalle.Attributes("src") = "//intranet.usat.edu.pe/rptusat/?/PRIVADOS/ACADEMICO/IND_CuadroMandoIntegral" & "&codigo_pla=" & ddlPlan.SelectedValue & "&codigo_pers=" & lblPers6.Text & "&anio=" & Me.ddlPeriodo.SelectedValue & "&codigo_cco=" & ddlCentroCostoPlan.SelectedValue
            'EnviarAPagina("frmDetallePerspectivas.aspx?codigo_plan=", lblPers6.Text)
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    'Private Sub EnviarAPagina(ByVal pagina As String, ByVal vTab As Integer)
    '    Try
    '        'Me.fradetalle.Attributes("src") = pagina & "&id=" & Request.QueryString("id") & "&ctf=" & Request.QueryString("ctf") & "&cco=" & Me.cboCecos.SelectedValue
    '        Me.fradetalle.Attributes("src") = pagina & ddlPlan.SelectedValue & "&Codigo_pers=" & vTab & "&anio=" & ddlPeriodo.SelectedValue
    '    Catch ex As Exception
    '        Response.Write(ex.Message)
    '    End Try
    'End Sub


    Private Sub CargarComboPeriodos()
        Try
            Dim obj As New clsIndicadores
            Dim dts As New Data.DataTable

            dts = obj.ConsultarPeriodosPosteriores(1, 0)

            If dts.Rows.Count > 0 Then
                ddlPeriodo.DataSource = dts
                ddlPeriodo.DataValueField = "Codigo"
                ddlPeriodo.DataTextField = "Descripcion"
                ddlPeriodo.DataBind()
            End If

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub ddlPlan_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPlan.SelectedIndexChanged
        Try
            'Response.Write(ddlPlan.SelectedValue)

            If ddlPlan.SelectedValue <> 0 Then
                CargarCentroCostosPlan(ddlPlan.SelectedValue)
                ddlCentroCostoPlan.Enabled = True
            Else
                EstadosControlesPrincipal(False)
            End If

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub CargarCentroCostosPlan(ByVal vcodigo_pla As Integer)
        Try
            Dim obj As New clsIndicadores
            Dim dts As New Data.DataTable


            If vcodigo_pla > 0 Then
                dts = obj.ListaCecosPlan(vcodigo_pla)
                If dts.Rows.Count > 0 Then
                    ddlCentroCostoPlan.DataSource = dts
                    ddlCentroCostoPlan.DataTextField = "Descripcion"
                    ddlCentroCostoPlan.DataValueField = "Codigo"
                    ddlCentroCostoPlan.DataBind()
                End If
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub ddlCentroCostoPlan_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlCentroCostoPlan.SelectedIndexChanged
        Try
            ddlPeriodo.Enabled = True
            If ddlPeriodo.SelectedValue <> 0 Then
                lnkPers1_Click(sender, e)
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub ddlPeriodo_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPeriodo.SelectedIndexChanged
        Try
            If ddlCentroCostoPlan.SelectedValue <> 0 And ddlPeriodo.SelectedValue <> 0 And ddlPlan.SelectedValue <> 0 Then
                Dim obj As New clsIndicadores
                Dim dts As New Data.DataTable

                dts = obj.ListaPerspectivasPlanCeco(ddlPlan.SelectedValue, ddlCentroCostoPlan.SelectedValue)

                If dts.Rows.Count > 1 Then
                    For i As Integer = 0 To dts.Rows.Count - 1
                        'Response.Write(dts.Rows(i).Item("Descripcion"))
                        'Response.Write("<br />")
                        Select Case i
                            Case 0
                                lnkPers1.Text = dts.Rows(i).Item("Descripcion").ToString
                                lblPers1.Text = dts.Rows(i).Item("Codigo").ToString
                            Case 1
                                lnkPers2.Text = dts.Rows(i).Item("Descripcion").ToString
                                lblPers2.Text = dts.Rows(i).Item("Codigo").ToString
                            Case 2
                                lnkPers3.Text = dts.Rows(i).Item("Descripcion").ToString
                                lblPers3.Text = dts.Rows(i).Item("Codigo").ToString
                            Case 3
                                lnkPers4.Text = dts.Rows(i).Item("Descripcion").ToString
                                lblPers4.Text = dts.Rows(i).Item("Codigo").ToString
                            Case 4
                                lnkPers5.Text = dts.Rows(i).Item("Descripcion").ToString
                                lblPers5.Text = dts.Rows(i).Item("Codigo").ToString
                            Case 5
                                lnkPers6.Text = dts.Rows(i).Item("Descripcion").ToString
                                lblPers6.Text = dts.Rows(i).Item("Codigo").ToString
                        End Select
                    Next
                    tabs.Visible = True
                    lnkPers1_Click(sender, e)
                End If
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    
End Class
