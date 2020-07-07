
Partial Class indicadores_POA_FrmListaAreasCategoria
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("id_per") = "" Or Request.QueryString("id") = "" Then
            Response.Redirect("../../../sinacceso.html")
        End If

        Try
            If Not IsPostBack Then
                Call CargarActividades()
                btnNuevo.Visible = False
                btnClonar.Visible = False
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Sub CargarActividades()
        Try
            Dim obj As New clsPlanOperativoAnual
            Dim dt As New Data.DataTable
            dt = obj.POA_CategoriaProgProyActividad(0, 0, 0, 0, "CCA")

            Me.ddlActividades.DataSource = dt
            Me.ddlActividades.DataTextField = "descripcion"
            Me.ddlActividades.DataValueField = "codigo"
            Me.ddlActividades.DataBind()

            Me.ddlActividadesClonar.DataSource = dt
            Me.ddlActividadesClonar.DataTextField = "descripcion"
            Me.ddlActividadesClonar.DataValueField = "codigo"
            Me.ddlActividadesClonar.DataBind()

            dt.Dispose()
            obj = Nothing
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub btnBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBuscar.Click
        Try
            Dim codigo_act As Integer = ddlActividades.SelectedValue
            If codigo_act = 0 Then
                Response.Write("<script>alert('Debe Seleccionar una Actividad')</script>")
                Return
            End If

            If ddlTipo.SelectedValue = "R" Then
                If ddlMovimiento.SelectedValue = "%" Then
                    Response.Write("<script>alert('Debe Seleccionar una Si es Rubro de Ingreso/Egreso')</script>")
                    Return
                End If

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
        Try
            Dim obj As New clsPlanOperativoAnual
            Dim dt As New Data.DataTable

            Dim codigo_cip As Integer = 0
            Dim codigo_cat As String = IIf(ddlActividades.SelectedValue.ToString = 0, "%", ddlActividades.SelectedValue.ToString)
            Dim codigoitem As String = "%" ' 
            Dim precio_cip As Decimal = 0
            Dim codigo_art As String = "0"
            Dim tipo_cip As String = ddlTipo.SelectedValue.ToString
            Dim movimiento_cip As String = ddlMovimiento.SelectedValue.ToString
            Dim estado_cip As Integer = 0
            Dim usuario_reg As Integer = Request.QueryString("id")
            Dim tipo As String = "ALL"
            Dim texto As String = txt_texto.Text.ToUpper

            'Response.Write("POA_CategoriaItem " & codigo_cip & "," & codigo_cat & ",'" & codigoitem & "'," & precio_cip & "," & codigo_art & ",'" & tipo_cip & "','" & movimiento_cip & "'," & estado_cip & "," & usuario_reg & ",'" & tipo & "','" & texto & "'")

            dt = obj.POA_CategoriaItem(codigo_cip, codigo_cat, codigoitem, precio_cip, codigo_art, tipo_cip, movimiento_cip, estado_cip, usuario_reg, tipo, texto)
            If dt.Rows.Count = 0 Then
                Me.lblMensajeFormulario.Text = "No se encontraron registros"
                Me.dgv_Categoria.DataSource = Nothing
            Else
                Me.dgv_Categoria.DataSource = dt
            End If
            Me.dgv_Categoria.DataBind()
            dt.Dispose()
            Me.lblMensajeFormulario.Text = "Se encontraron " & dt.Rows.Count & " registro(s)."
            lblPOA.Text = ddlActividades.SelectedItem.ToString

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
                Me.lblMensajeFormulario.Text = Me.lblMensajeFormulario.Text + "  y " + i.ToString.ToUpper + " Categorías seleccionadas"
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub dgv_Categoria_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles dgv_Categoria.RowDataBound
        If e.Row.RowType = System.Web.UI.WebControls.DataControlRowType.DataRow Then
            ' when mouse is over the row, save original color to new attribute, and change it to highlight color
            e.Row.Attributes.Add("onmouseover", "this.originalstyle=this.style.backgroundColor;this.style.backgroundColor='#EEFFAA'")

            ' when mouse leaves the row, change the bg color to its original value   
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalstyle;")
        End If
    End Sub

    Protected Sub btnNuevo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNuevo.Click
        Try
            Dim dt As New Data.DataTable
            Dim obj As New clsPlanOperativoAnual
            Dim codigo_cip As Integer = 0
            Dim codigo_cat As String = IIf(ddlActividades.SelectedValue.ToString = 0, "%", ddlActividades.SelectedValue.ToString)
            'Dim tipo_cip As String = "A"
            'Dim movimiento_cip As String = "I"
            Dim estado_cip As Integer = 0
            Dim usuario_reg As Integer = Request.QueryString("id")
            Dim texto As String = txt_texto.Text.ToUpper

            For Each row As GridViewRow In dgv_Categoria.Rows
                Dim check As CheckBox = TryCast(row.FindControl("chkSeleccion"), CheckBox)
                Dim codigoitem As String = dgv_Categoria.DataKeys(row.RowIndex).Values("codigoitem")
                Dim precio_cip As TextBox
                precio_cip = DirectCast(Me.dgv_Categoria.Rows(row.RowIndex).FindControl("txtprecio"), TextBox)
                Dim codigo_art As String = dgv_Categoria.DataKeys(row.RowIndex).Values("codigocon")

                Dim tipo_cip As String = dgv_Categoria.DataKeys(row.RowIndex).Values("tipo")
                Dim movimiento_cip As String = dgv_Categoria.DataKeys(row.RowIndex).Values("id")

                'Consultar si existe
                dt = obj.POA_CategoriaItem(codigo_cip, codigo_cat, codigoitem, precio_cip.Text, codigo_art, tipo_cip, movimiento_cip, estado_cip, usuario_reg, "CON", texto)
                If dt.Rows.Count > 0 Then
                    'Si existe actualizar el estado a 0 y solo los chekeados a 1
                    If dgv_Categoria.DataKeys(row.RowIndex).Values("codigo_cip") > 0 Then
                        obj.POA_CategoriaItem(dgv_Categoria.DataKeys(row.RowIndex).Values("codigo_cip"), codigo_cat, codigoitem, precio_cip.Text, codigo_art, tipo_cip, movimiento_cip, 0, usuario_reg, "UPD", texto)
                    End If
                    If check.Checked Then
                        obj.POA_CategoriaItem(dgv_Categoria.DataKeys(row.RowIndex).Values("codigo_cip"), codigo_cat, codigoitem, precio_cip.Text, codigo_art, tipo_cip, movimiento_cip, 1, usuario_reg, "UPD", texto)
                    End If
                End If

                'sino existe  insertarlos
                If check.Checked Then
                    codigo_cip = dgv_Categoria.DataKeys(row.RowIndex).Values("codigo_cip")
                    dt = obj.POA_CategoriaItem(codigo_cip, codigo_cat, codigoitem, precio_cip.Text, codigo_art, tipo_cip, movimiento_cip, estado_cip, usuario_reg, "INS", texto)
                End If
            Next
            Response.Write("<script>alert('Se Actualizaron correctamente los Items para la Actividad: " & ddlActividades.SelectedItem.ToString & "')</script>")
            Call CargarGrid(1)
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub btnClonar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClonar.Click
        Me.TablaClonarPoa.Visible = True
        ddlActividadesClonar.SelectedIndex = 0
        ddlActividadesClonar.Focus()
    End Sub

    Protected Sub btnRegistrarClon_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRegistrarClon.Click
        Try
            Dim obj As New clsPlanOperativoAnual

            ''Validar que Poa y Poa a clonar sea Diferentes sino msg
            Dim Actividades As Integer = ddlActividades.SelectedValue.ToString
            Dim ActividadesColnar As Integer = ddlActividadesClonar.SelectedValue.ToString
            If Actividades = ActividadesColnar Then
                Response.Write("<script>alert('No se puede Clonar la Actividad Origen: " & ddlActividades.SelectedItem.ToString & "')</script>")
                Return
            End If

            Dim usuario_reg As Integer = Request.QueryString("id")
            obj.POA_ClonarActividadPOA(Actividades, ActividadesColnar, usuario_reg)

            Response.Write("<script>alert('Se Registro la Actividad: " & ddlActividadesClonar.SelectedItem.ToString & "')</script>")
            Me.TablaClonarPoa.Visible = False
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub btnCancelarClon_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelarClon.Click
        Me.TablaClonarPoa.Visible = False
    End Sub
End Class
