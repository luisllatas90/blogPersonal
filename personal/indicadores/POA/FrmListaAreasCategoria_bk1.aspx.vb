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
                CargarCategorias()
                btnBuscar_Click(sender, e)
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
        dtt = obj.ListaPoasxInstanciaEstado(codigo_pla, codigo_ejp, "PL", Request.QueryString("id"), Request.QueryString("ctf"))
        Me.ddlPoa.DataSource = dtt
        Me.ddlPoa.DataTextField = "descripcion"
        Me.ddlPoa.DataValueField = "codigo"
        Me.ddlPoa.DataBind()
        dtt.Dispose()
        obj = Nothing
        'Me.ddlPoa.Items.Insert(0, New ListItem("--SELECCIONE--", "0"))
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

    Protected Sub ddlplan_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlplan.SelectedIndexChanged
        CargaPoas(Me.ddlplan.SelectedValue, Me.ddlEjercicio.SelectedValue)
    End Sub

    Protected Sub ddlEjercicio_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlEjercicio.SelectedIndexChanged
        CargaPoas(Me.ddlplan.SelectedValue, Me.ddlEjercicio.SelectedValue)
    End Sub

    Protected Sub btnNuevo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNuevo.Click
        Try
            Dim dt As New Data.DataTable
            Dim obj As New clsPlanOperativoAnual
            Dim codigo_aca As Integer = 0
            Dim codigo_poa As Integer = ddlPoa.SelectedValue
            Dim codigo_cap As Integer = ddlCategoriasProgramaProyecto.SelectedValue
            Dim usuario_reg As Integer = Request.QueryString("id")

            If codigo_poa = 0 Then
                Response.Write("<script>alert('Debe Seleccionar un POA')</script>")
                Return
            End If

            If codigo_cap = 0 Then
                Response.Write("<script>alert('Debe Seleccionar una Categoría')</script>")
                Return
            End If
            obj.POA_AreasCategoria(codigo_aca, codigo_poa, codigo_cap, usuario_reg, "INS")

            btnBuscar_Click(sender, e)
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub btnBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBuscar.Click
        Try
            Dim obj As New clsPlanOperativoAnual
            Dim dt As New Data.DataTable

            Dim codigo_aca As Integer = 0
            Dim codigo_poa As String = IIf(ddlPoa.SelectedValue.ToString = 0, "%", ddlPoa.SelectedValue.ToString)
            Dim codigo_cap As String = IIf(ddlCategoriasProgramaProyecto.SelectedValue = 0, "%", ddlCategoriasProgramaProyecto.SelectedValue.ToString)

            Dim usuario_reg As Integer = Request.QueryString("id")
            dt = obj.POA_AreasCategoria(codigo_aca, codigo_poa, codigo_cap, usuario_reg, "ALL")
            Me.lblMensajeFormulario.Text = "Se encontraron " & dt.Rows.Count.ToString & " registro(s)."
            If dt.Rows.Count = 0 Then
                '    'Me.lblMensajeFormulario.Text = "No se encontraron registros"
                Me.dgv_Categoria.DataSource = Nothing
            Else
                Me.dgv_Categoria.DataSource = dt
                '    'Me.dgvAsignar.Columns.Item(4).Visible = False 'codigo_pla
            End If
            Me.dgv_Categoria.DataBind()
            dt = Nothing
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub dgv_Categoria_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles dgv_Categoria.RowDeleting
        Try
            Dim obj As New clsPlanOperativoAnual

            Dim codigo_poa As String = "0"
            Dim codigo_cap As String = "0"
            Dim usuario_reg As Integer = Request.QueryString("id")
            obj.POA_AreasCategoria(dgv_Categoria.DataKeys(e.RowIndex).Value.ToString(), codigo_poa, codigo_cap, usuario_reg, "DEL")

            btnBuscar_Click(sender, e)
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

End Class
