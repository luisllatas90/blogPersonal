﻿
Partial Class indicadores_POA_FrmListaCategoriaProgProy
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("id_per") Is Nothing Or Request.QueryString("id") = "" Then
            Response.Redirect("../../../sinacceso.html")
        End If

        If IsPostBack = False Then
            If Request.QueryString("msj") = "R" Then
                Me.ddlVigencia.SelectedValue = Request.QueryString("vigencia")
                'Me.lblrpta.Text = "Datos Registrados Correctamente"
                'Me.aviso.Attributes.Add("class", "mensajeExito")
            ElseIf Request.QueryString("msj") = "E" Then
                Me.ddlVigencia.SelectedValue = Request.QueryString("vigencia")
                'Me.lblrpta.Text = "Datos Actualizados Correctamente"
                'Me.aviso.Attributes.Add("class", "mensajeExito")
            ElseIf Request.QueryString("msj") = "T" Then
                Me.ddlVigencia.SelectedValue = Request.QueryString("vigencia")
            End If
            CargarGrid(Me.ddlVigencia.SelectedValue)
        End If
    End Sub

    Protected Sub btnNuevo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNuevo.Click
        Dim tipo As String = "I"
        Dim codigo_cap As Integer = 0
        Response.Redirect("FrmMantenimientoCategoriaProgProy.aspx?id=" & Request.QueryString("id") & "&vigencia=" & Me.ddlVigencia.SelectedValue & "&tipo=" & tipo & "&codigo_cap=" & codigo_cap)
    End Sub

    Sub CargarGrid(ByVal vigencia As Integer)
        Try
            Dim obj As New clsPlanOperativoAnual
            Dim dt As New Data.DataTable

            Dim codigo_cap As Integer = 0
            Dim nombre_cap As String = ""
            Dim usuario_reg As Integer = Request.QueryString("id")
            Dim fecha_reg As String = "07/08/2017"
            Dim tipo As String = Request.QueryString("tipo")
            dt = obj.POA_CategoriaProgProy(codigo_cap, nombre_cap, vigencia, usuario_reg, fecha_reg, "L")
            obj = Nothing
            If dt.Rows.Count = 0 Then
                Me.lblMensajeFormulario.Text = "No se encontraron registros"
                Me.dgv_Categoria.DataSource = Nothing
            Else
                Me.dgv_Categoria.DataSource = dt
            End If
            Me.dgv_Categoria.DataBind()
            dt.Dispose()
            Me.lblMensajeFormulario.Text = "Se encontraron " & dt.Rows.Count & " registro(s)."
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub dgv_Categoria_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles dgv_Categoria.RowCommand
        Try
            If (e.CommandName.Equals("Select")) Then
                Dim seleccion As GridViewRow
                Dim codigo_cap As Integer = 0
                Dim obj As New clsPlanOperativoAnual
                '1. Obtengo la linea del gridview que fue cliqueada
                seleccion = DirectCast(e.CommandSource, GridView).Rows(e.CommandArgument)
                '2. Obtengo el datakey de la linea que donde está el boton que cliqueé
                codigo_cap = Me.dgv_Categoria.DataKeys(seleccion.RowIndex).Values("codigo_cap")
                Dim tipo As String = "E"

                'Response.Write("FrmMantenimientoCategoriaProgProy.aspx id=" & Request.QueryString("id") & " vigencia=" & Me.ddlVigencia.SelectedValue & "&tipo=" & tipo & "&codigo_cap=" & codigo_cap)
                Response.Redirect("FrmMantenimientoCategoriaProgProy.aspx?id=" & Request.QueryString("id") & "&vigencia=" & Me.ddlVigencia.SelectedValue & "&tipo=" & tipo & "&codigo_cap=" & codigo_cap)
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub dgv_Categoria_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles dgv_Categoria.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim estado As String = DataBinder.Eval(e.Row.DataItem, "estado_cap").ToString()
            Dim vigencia As Integer = ddlVigencia.SelectedValue
            If estado = 0 Then
                e.Row.Cells(2).Text = ""
                e.Row.Cells(3).Text = ""
            End If

            ' when mouse is over the row, save original color to new attribute, and change it to highlight color
            e.Row.Attributes.Add("onmouseover", "this.originalstyle=this.style.backgroundColor;this.style.backgroundColor='#EEFFAA'")

            ' when mouse leaves the row, change the bg color to its original value   
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalstyle;")

        End If

        'If e.Row.RowType = System.Web.UI.WebControls.DataControlRowType.DataRow Then
        '    ' when mouse is over the row, save original color to new attribute, and change it to highlight color
        '    e.Row.Attributes.Add("onmouseover", "this.originalstyle=this.style.backgroundColor;this.style.backgroundColor='#EEFFAA'")

        '    ' when mouse leaves the row, change the bg color to its original value   
        '    e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalstyle;")
        'End If

    End Sub

    Protected Sub dgv_Categoria_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles dgv_Categoria.RowDeleting
        Try
            Dim obj As New clsPlanOperativoAnual
            Dim codigo_cap As Integer = 0
            Dim nombre_cap As String = ""
            Dim estado_cap As Integer = 0
            Dim usuario_reg As Integer = Request.QueryString("id")
            Dim fecha_reg As String = "07/08/2017"
            Dim tipo As String = Request.QueryString("tipo")

            'Validar que no se encuentre asignado a categorias
            Dim dtt As New Data.DataTable
            'Response.Write("POA_CategoriaProgProy " & dgv_Categoria.DataKeys(e.RowIndex).Value.ToString() & ", " & nombre_cap & ", " & estado_cap & ", " & usuario_reg & ", " & fecha_reg & ", " & "V")
            dtt = obj.POA_CategoriaProgProy(dgv_Categoria.DataKeys(e.RowIndex).Value.ToString(), nombre_cap, estado_cap, usuario_reg, fecha_reg, "V")
            If dtt.Rows(0).Item("valor").ToString = "S" Then
                Response.Write("<script>alert('No se Puede Eliminar la Categoria de Prog./Proy.: " & dgv_Categoria.Rows(e.RowIndex).Cells(0).Text & "')</script>")
            Else
                obj.POA_CategoriaProgProy(dgv_Categoria.DataKeys(e.RowIndex).Value.ToString(), nombre_cap, estado_cap, usuario_reg, fecha_reg, "D")
            End If
            Call CargarGrid(Me.ddlVigencia.SelectedValue)
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub btnBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBuscar.Click
        CargarGrid(Me.ddlVigencia.SelectedValue)
    End Sub

    Protected Sub dgv_Categoria_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgv_Categoria.SelectedIndexChanged

    End Sub
End Class
