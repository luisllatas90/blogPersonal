
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
                btnNuevo.Visible = False
                btnClonar.Visible = False
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

        Me.ddlPoaClonar.DataSource = dtt
        Me.ddlPoaClonar.DataTextField = "descripcion"
        Me.ddlPoaClonar.DataValueField = "codigo"
        Me.ddlPoaClonar.DataBind()

        dtt.Dispose()
        obj = Nothing
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

            For Each row As GridViewRow In dgv_Categoria.Rows
                Dim check As CheckBox = TryCast(row.FindControl("chkSeleccion"), CheckBox)
                Dim codigo_aca As Integer = 0
                Dim codigo_poa As Integer = ddlPoa.SelectedValue
                Dim codigo_cap As Integer = dgv_Categoria.DataKeys(row.RowIndex).Values("codigo_cap")
                Dim usuario_reg As Integer = Request.QueryString("id")

                'Consultar si existe
                dt = obj.POA_AreasCategoria(codigo_aca, codigo_poa, codigo_cap, usuario_reg, "CON", 0)
                If dt.Rows.Count > 0 Then
                    'Si existe actualizar el estado a 0 y solo los chekeados a 1
                    obj.POA_AreasCategoria(dgv_Categoria.DataKeys(row.RowIndex).Values("codigo_aca"), codigo_poa, codigo_cap, usuario_reg, "UPD", 0)
                    If check.Checked Then
                        obj.POA_AreasCategoria(dgv_Categoria.DataKeys(row.RowIndex).Values("codigo_aca"), codigo_poa, codigo_cap, usuario_reg, "UPD", 1)
                    End If
                End If

                'sino existe  insertarlos
                If check.Checked Then
                    codigo_aca = dgv_Categoria.DataKeys(row.RowIndex).Values("codigo_aca")
                    obj.POA_AreasCategoria(codigo_aca, codigo_poa, codigo_cap, usuario_reg, "INS", 0)
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

            btnNuevo.Visible = True
            btnClonar.Visible = True
            Me.TablaClonarPoa.Visible = False
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Sub CargarGrid(ByVal vigencia As Integer)
        Dim obj As New clsPlanOperativoAnual
        Dim dt As New Data.DataTable

        Dim codigo_aca As Integer = 0
        Dim codigo_poa As String = IIf(ddlPoa.SelectedValue.ToString = 0, "%", ddlPoa.SelectedValue.ToString)
        Dim codigo_cap As String = "%"
        Dim usuario_reg As Integer = Request.QueryString("id")

        'Response.Write("POA_AreasCategoria " & codigo_aca & ", " & codigo_poa & ", " & codigo_cap & ", " & usuario_reg & ", " & "'ALL'")
        dt = obj.POA_AreasCategoria(codigo_aca, codigo_poa, codigo_cap, usuario_reg, "ALL", 0)
        If dt.Rows.Count = 0 Then
            Me.lblMensajeFormulario.Text = "No se encontraron registros"
            Me.dgv_Categoria.DataSource = Nothing
        Else
            Me.dgv_Categoria.DataSource = dt
        End If
        Me.dgv_Categoria.DataBind()
        dt.Dispose()
        Me.lblMensajeFormulario.Text = "Se encontraron " & dt.Rows.Count & " registro(s)."
        lblPOA.Text = ddlPoa.SelectedItem.ToString

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
            Me.lblMensajeFormulario.Text = Me.lblMensajeFormulario.Text + "  y " + i.ToString.ToUpper + " Categorías seleccionadas"
        End If
    End Sub

    Protected Sub dgv_Categoria_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles dgv_Categoria.RowDataBound
        If e.Row.RowType = System.Web.UI.WebControls.DataControlRowType.DataRow Then
            ' when mouse is over the row, save original color to new attribute, and change it to highlight color
            e.Row.Attributes.Add("onmouseover", "this.originalstyle=this.style.backgroundColor;this.style.backgroundColor='#EEFFAA'")

            ' when mouse leaves the row, change the bg color to its original value   
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalstyle;")
        End If
    End Sub

    Protected Sub btnClonar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClonar.Click
        Me.TablaClonarPoa.Visible = True
        ddlPoaClonar.SelectedIndex = 0
        ddlPoaClonar.Focus()
    End Sub

    Protected Sub btnRegistrarClon_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRegistrarClon.Click
        Try
            Dim dt As New Data.DataTable
            Dim obj As New clsPlanOperativoAnual

            ''Validar que Poa y Poa a clonar sea Diferentes sino msg
            Dim poa As Integer = ddlPoa.SelectedValue.ToString
            Dim poaColnar As Integer = ddlPoaClonar.SelectedValue.ToString
            If poa = poaColnar Then
                Response.Write("<script>alert('No se puede Clonar la Categoría Origen: " & ddlPoa.SelectedItem.ToString & "')</script>")
                Return
            End If

            Dim usuario_reg As Integer = Request.QueryString("id")
            obj.POA_ClonarCaterogiasPOA(poa, poaColnar, usuario_reg)

            Response.Write("<script>alert('Se Registro las Categoría: " & ddlPoaClonar.SelectedItem.ToString & "')</script>")
            Me.TablaClonarPoa.Visible = False
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub btnCancelarClon_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelarClon.Click
        Me.TablaClonarPoa.Visible = False
    End Sub
End Class
