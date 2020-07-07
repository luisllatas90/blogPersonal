﻿
Partial Class indicadores_POA_FrmListaCategoriaActividadProyecto
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("id_per") = "" Or Request.QueryString("id") = "" Then
            Response.Redirect("../../../sinacceso.html")
        End If

        Try
            If Not IsPostBack Then
                Call CargarCategorias()
                btnNuevo.Visible = False
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Sub CargarCategorias()
        Try
            Dim obj As New clsPlanOperativoAnual
            Dim dt As New Data.DataTable
            dt = obj.POA_CategoriaProgProyActividad(0, 0, 0, 0, "CPP")

            Me.ddlCategoriasProgramaProyecto.DataSource = dt
            Me.ddlCategoriasProgramaProyecto.DataTextField = "descripcion"
            Me.ddlCategoriasProgramaProyecto.DataValueField = "codigo"
            Me.ddlCategoriasProgramaProyecto.DataBind()
            dt.Dispose()
            obj = Nothing
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub btnNuevo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNuevo.Click
        Try
            Dim dt As New Data.DataTable
            Dim obj As New clsPlanOperativoAnual

            For Each row As GridViewRow In dgv_Categoria.Rows
                Dim check As CheckBox = TryCast(row.FindControl("chkSeleccion"), CheckBox)

                'If check.Checked Then
                Dim codigo_cpa As Integer = 0
                Dim codigo_cap As Integer = ddlCategoriasProgramaProyecto.SelectedValue
                Dim codigo_cat As Integer = dgv_Categoria.DataKeys(row.RowIndex).Values("codigo_cat")
                Dim usuario_reg As Integer = Request.QueryString("id")

                If codigo_cap = 0 Then
                    Response.Write("<script>alert('Debe Seleccionar una Categoría')</script>")
                    Return
                End If

                If dgv_Categoria.DataKeys(row.RowIndex).Values("codigo_cpa").ToString > 0 Then
                    'Response.Write("POA_CategoriaProgProyActividad " & dgv_Categoria.DataKeys(row.RowIndex).Values("codigo_cpa") & ", " & codigo_cap & ", " & codigo_cat & ", " & usuario_reg & ", " & "'DEL'")
                    obj.POA_CategoriaProgProyActividad(dgv_Categoria.DataKeys(row.RowIndex).Values("codigo_cpa"), codigo_cap, codigo_cat, usuario_reg, "DEL") '
                End If

                If check.Checked Then
                    obj.POA_CategoriaProgProyActividad(codigo_cpa, codigo_cap, codigo_cat, usuario_reg, "INS")
                End If
            Next
            'Response.Write("<script>alert('Debe Seleccionar una Categoría')</script>")
            Response.Write("<script>alert('Se asignaron las actividades correctamente para la Categoría: " & ddlCategoriasProgramaProyecto.SelectedItem.ToString & "')</script>")

            Call CargarGrid(1)

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub btnBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBuscar.Click
        Try
            Dim codigo_cap As Integer = ddlCategoriasProgramaProyecto.SelectedValue
            If codigo_cap = 0 Then
                Me.dgv_Categoria.Visible = False
                Me.lblMensajeFormulario.Text = "No se encontraron registros"
                btnNuevo.Visible = False
                Response.Write("<script>alert('Debe Seleccionar una Categoría')</script>")
                Return
            Else
                Me.dgv_Categoria.Visible = True
                Call CargarGrid(1)
                btnNuevo.Visible = True
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Sub CargarGrid(ByVal vigencia As Integer)
        Dim obj As New clsPlanOperativoAnual
        Dim dt As New Data.DataTable

        Dim codigo_cpa As String = 0
        Dim codigo_cap As Integer = ddlCategoriasProgramaProyecto.SelectedValue
        Dim codigo_cat As String = 0
        Dim usuario_reg As Integer = Request.QueryString("id")

        'Response.Write("POA_CategoriaProgProyActividad " + codigo_cpa & ", " & codigo_cap & ", " & codigo_cat & ", " & usuario_reg & ", " & "ALL")
        dt = obj.POA_CategoriaProgProyActividad(codigo_cpa, codigo_cap, codigo_cat, usuario_reg, "ALL")
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

        'Call LimpiarCheckBoxList()

        If dgv_Categoria.Rows.Count > 0 Then
            Dim i As Integer = 0
            For Each row As GridViewRow In dgv_Categoria.Rows
                Dim check As CheckBox = TryCast(row.FindControl("chkSeleccion"), CheckBox)
                Dim estado As Integer = dt.Rows(row.RowIndex).Item("estado").ToString

                If estado = 0 Then
                    check.Checked = False
                Else
                    i = i + 1
                    check.Checked = True
                End If
            Next
            Me.lblMensajeFormulario.Text = Me.lblMensajeFormulario.Text + "  y " & i.ToString.ToUpper & " Actividades seleccionadas."
        End If
    End Sub


    Protected Sub dgv_Categoria_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles dgv_Categoria.RowDataBound
        Try
            If e.Row.RowType = System.Web.UI.WebControls.DataControlRowType.DataRow Then
                ' when mouse is over the row, save original color to new attribute, and change it to highlight color
                e.Row.Attributes.Add("onmouseover", "this.originalstyle=this.style.backgroundColor;this.style.backgroundColor='#EEFFAA'")

                ' when mouse leaves the row, change the bg color to its original value   
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalstyle;")
            End If

            'If e.Row.RowType = DataControlRowType.DataRow Then

            '    e.Row.Attributes.Add("OnMouseOver", "Resaltar(1,this,'S')")
            '    e.Row.Attributes.Add("OnMouseOut", "Resaltar(0,this,'S')")
            'End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub dgv_Categoria_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgv_Categoria.SelectedIndexChanged

    End Sub
End Class
