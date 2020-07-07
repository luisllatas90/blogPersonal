﻿
Partial Class indicadores_POA_FrmMantenimientoCategoriaProgProy
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (Session("id_per") Is Nothing) Then
            Response.Redirect("../../../sinacceso.html")
        End If

        If IsPostBack = False Then
            hdvigencia.Value = Request.QueryString("vigencia")

            Dim codigo_cap As String = Request.QueryString("codigo_cap")
            Dim nombre_cap As String = txtNombrePoa.Text.ToUpper
            Dim estado_cap As Integer = 1
            Dim usuario_reg As Integer = Request.QueryString("id")
            Dim fecha_reg As String = "07/08/2017"
            Dim tipo As String = Request.QueryString("tipo")

            If tipo = "E" Then
                Dim obj As New clsPlanOperativoAnual
                Dim dt As New Data.DataTable
                dt = obj.POA_CategoriaProgProy(codigo_cap, nombre_cap, estado_cap, usuario_reg, fecha_reg, "L")
                Me.txtNombrePoa.Text = dt.Rows(0).Item("nombre_cap")
            End If
            txtNombrePoa.Focus()
        End If
    End Sub

    Protected Sub cmdGuardarPoa_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdGuardarPoa.Click
        Try
            Dim dt As New Data.DataTable
            Dim obj As New clsPlanOperativoAnual
            If txtNombrePoa.Text = "" Then
                Response.Write("<script>alert('Ingrese Nombre de Categoría')</script>")
                Return
            End If
            '    'Nuevo registro
            Dim tipo As String = Request.QueryString("tipo")
            If tipo = "I" Then
                Dim codigo_cap As Integer = 0
                Dim nombre_cap As String = txtNombrePoa.Text.ToUpper
                Dim estado_cap As Integer = 1
                Dim usuario_reg As Integer = Request.QueryString("id")
                Dim fecha_reg As String = "07/08/2017"
                obj.POA_CategoriaProgProy(codigo_cap, nombre_cap, estado_cap, usuario_reg, fecha_reg, tipo)

                Call Limpiar()
                Response.Redirect("FrmListaCategoriaProgProy.aspx?id=" & Request.QueryString("id") & "&vigencia=" & hdvigencia.Value & "&tipo=" & tipo & "&msj='R'")

            ElseIf tipo = "E" Then
                Dim codigo_cap As String = Request.QueryString("codigo_cap")
                Dim nombre_cap As String = txtNombrePoa.Text.ToUpper
                Dim estado_cap As Integer = 1
                Dim usuario_reg As Integer = Request.QueryString("id")
                Dim fecha_reg As String = "07/08/2017"
                obj.POA_CategoriaProgProy(codigo_cap, nombre_cap, estado_cap, usuario_reg, fecha_reg, tipo)

                Call Limpiar()
                Response.Redirect("FrmListaCategoriaProgProy.aspx?id=" & Request.QueryString("id") & "&vigencia=" & hdvigencia.Value & "&tipo=" & tipo & "&msj='E'")
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Sub Limpiar()
        txtNombrePoa.Text = ""
    End Sub

    Protected Sub cmdCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdCancelar.Click
        Response.Redirect("FrmListaCategoriaProgProy.aspx?id=" & Request.QueryString("id") & "&vigencia=" & hdvigencia.Value & "&tipo=" & Request.QueryString("tipo") & "&msj='T'")
    End Sub
End Class
