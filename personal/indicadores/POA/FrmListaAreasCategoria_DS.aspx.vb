﻿
Partial Class indicadores_POA_FrmListaAreasCategoria
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("id_per") = "" Or Request.QueryString("id") = "" Then
            Response.Redirect("../../../sinacceso.html")
        End If

        Try
            If Not IsPostBack Then
                CargaPlanes()
                CargaEjercicio()
                'CargarCategorias()
                btnAgregar.Visible = False
                'btnBuscar_Click(sender, e)
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

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
        dtt = obj.ListaPoasxInstanciaEstado(codigo_pla, codigo_ejp, "AP", Request.QueryString("id"), Request.QueryString("ctf"))
        Me.ddlPoa.DataSource = dtt
        Me.ddlPoa.DataTextField = "descripcion"
        Me.ddlPoa.DataValueField = "codigo"
        Me.ddlPoa.DataBind()
        dtt.Dispose()
        obj = Nothing
        'Me.ddlPoa.Items.Insert(0, New ListItem("--SELECCIONE--", "0"))
    End Sub

    'Sub CargarCategorias()
    '    Try
    '        Dim obj As New clsPlanOperativoAnual
    '        Dim dt As New Data.DataTable
    '        dt = obj.POA_CategoriaProgProyActividad(0, 0, 0, 0, "CPP")

    '        Me.ddlCategoriasProgramaProyecto.DataSource = dt
    '        Me.ddlCategoriasProgramaProyecto.DataTextField = "descripcion"
    '        Me.ddlCategoriasProgramaProyecto.DataValueField = "codigo"
    '        Me.ddlCategoriasProgramaProyecto.DataBind()
    '        dt.Dispose()
    '        obj = Nothing
    '    Catch ex As Exception
    '        Response.Write(ex.Message)
    '    End Try
    'End Sub

    Protected Sub ddlplan_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlplan.SelectedIndexChanged
        CargaPoas(Me.ddlplan.SelectedValue, Me.ddlEjercicio.SelectedValue)
    End Sub

    Protected Sub ddlEjercicio_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlEjercicio.SelectedIndexChanged
        CargaPoas(Me.ddlplan.SelectedValue, Me.ddlEjercicio.SelectedValue)
    End Sub

    Protected Sub btnNuevo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAgregar.Click
        Try
            Dim dt As New Data.DataTable
            Dim obj As New clsPlanOperativoAnual

            For Each row As GridViewRow In dgv_Categoria.Rows
                Dim check As CheckBox = TryCast(row.FindControl("chkSeleccion"), CheckBox)
                Dim codigo_aca As Integer = 0
                Dim codigo_poa As Integer = ddlPoa.SelectedValue
                Dim codigo_cap As Integer = dgv_Categoria.DataKeys(row.RowIndex).Values("codigo_cap")
                Dim usuario_reg As Integer = Request.QueryString("id")

                If dgv_Categoria.DataKeys(row.RowIndex).Values("codigo_aca").ToString > 0 Then
                    'Response.Write("POA_CategoriaProgProyActividad " & dgv_Categoria.DataKeys(row.RowIndex).Values("codigo_cpa") & ", " & codigo_cap & ", " & codigo_cat & ", " & usuario_reg & ", " & "'DEL'")
                    obj.POA_AreasCategoria(dgv_Categoria.DataKeys(row.RowIndex).Values("codigo_aca"), codigo_poa, codigo_cap, usuario_reg, "DEL")
                End If

                If check.Checked Then
                    obj.POA_AreasCategoria(codigo_aca, codigo_poa, codigo_cap, usuario_reg, "INS")
                End If
            Next

            Response.Write("<script>alert('Se Actualizaron correctamente las Categorías para el: " & ddlPoa.SelectedItem.ToString & "')</script>")
            Call CargarGrid(1)
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub btnBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBuscar.Click
        Try
            Dim codigo_poa As Integer = ddlPoa.SelectedValue
            If codigo_poa = 0 Then
                Response.Write("<script>alert('Debe Seleccionar un POA')</script>")
                Return
            End If
            Call CargarGrid(1)

            btnAgregar.Visible = True

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Sub CargarGrid(ByVal vigencia As Integer)
        Dim obj As New clsPlanOperativoAnual
        Dim dt As New Data.DataTable

        Dim codigo_aca As Integer = 0
        Dim codigo_poa As String = IIf(ddlPoa.SelectedValue.ToString = 0, "%", ddlPoa.SelectedValue.ToString)
        Dim codigo_cap As String = "%" ' IIf(ddlCategoriasProgramaProyecto.SelectedValue = 0, "%", ddlCategoriasProgramaProyecto.SelectedValue.ToString)
        Dim usuario_reg As Integer = Request.QueryString("id")

        'Response.Write("POA_AreasCategoria " & codigo_aca & ", " & codigo_poa & ", " & codigo_cap & ", " & usuario_reg & ", " & "'ALL'")
        dt = obj.POA_AreasCategoria(codigo_aca, codigo_poa, codigo_cap, usuario_reg, "ALL")
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
            For Each row As GridViewRow In dgv_Categoria.Rows
                Dim check As CheckBox = TryCast(row.FindControl("chkSeleccion"), CheckBox)
                Dim estado As Integer = dt.Rows(row.RowIndex).Item("estado").ToString

                If estado = 0 Then
                    check.Checked = False
                Else
                    check.Checked = True
                End If
            Next
        End If
    End Sub

    Protected Sub dgv_Categoria_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles dgv_Categoria.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then

                e.Row.Attributes.Add("OnMouseOver", "Resaltar(1,this,'S')")
                e.Row.Attributes.Add("OnMouseOut", "Resaltar(0,this,'S')")
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    'Protected Sub dgv_Categoria_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles dgv_Categoria.RowDeleting
    '    Try
    '        'Dim obj As New clsPlanOperativoAnual

    '        'Dim codigo_poa As String = "0"
    '        'Dim codigo_cap As String = "0"
    '        'Dim usuario_reg As Integer = Request.QueryString("id")
    '        'obj.POA_AreasCategoria(dgv_Categoria.DataKeys(e.RowIndex).Value.ToString(), codigo_poa, codigo_cap, usuario_reg, "DEL")

    '        'btnBuscar_Click(sender, e)
    '    Catch ex As Exception
    '        Response.Write(ex.Message)
    '    End Try
    'End Sub

End Class
