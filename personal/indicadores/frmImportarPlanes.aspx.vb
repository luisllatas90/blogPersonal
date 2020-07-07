
Partial Class indicadores_frmImportarPlanes
    Inherits System.Web.UI.Page



    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                lstMultipleValues.Attributes.Add("onclick", "FindSelectedItems(this," + txtSelectedMLValues.ClientID + ");")
                lstMultipleValues2.Attributes.Add("onclick", "FindSelectedItems(this," + txtSelectedMLValues2.ClientID + ");")

                CargarComboPlanes()
                EstadoControles(True)
            End If

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub CargarComboAniosOrigen()
        Try
            Dim obj As New clsIndicadores
            Dim dts As New Data.DataTable

            If ddlPlan.SelectedValue <> 0 Then
                dts = obj.ListaAniosObjetivosSegunPlan(ddlPlan.SelectedValue)
                'Response.Write(dts.Rows.Count)
                If dts.Rows.Count > 0 Then
                    lstMultipleValues2.DataSource = dts
                    lstMultipleValues2.DataTextField = "anio"
                    lstMultipleValues2.DataValueField = "anio"
                    lstMultipleValues2.DataBind()
                End If
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub CargarComboAniosDestino(ByVal Codigo_pla As Integer)
        Try
            Dim obj As New clsIndicadores
            Dim dts As New Data.DataTable

            dts = obj.ListaAnios(Codigo_pla)

            If dts.Rows.Count > 0 Then
                lstMultipleValues.DataSource = dts
                lstMultipleValues.DataTextField = "anio"
                lstMultipleValues.DataValueField = "anio"
                lstMultipleValues.DataBind()
            End If

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub


    Private Sub CargarComboPlanes()
        Try
            Dim obj As New clsIndicadores
            Dim dts As New Data.DataTable
            dts = obj.ListaPlanes(Request.QueryString("ctf"), Request.QueryString("id"))

            If dts.Rows.Count > 0 Then
                ddlPlan.DataSource = dts
                ddlPlan.DataTextField = "Descripcion"
                ddlPlan.DataValueField = "Codigo"
                ddlPlan.DataBind()

            End If

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub btnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmit.Click
        Try
            Dim dts As New Data.DataTable
            Dim obj As New clsIndicadores

            If Me.ddlPlan.SelectedValue <> 0 And CType(txtSelectedMLValues2.Value, String) <> "" And CType(txtSelectedMLValues.Value, String) <> "" Then
                'Ejecuta el procedimiento para importar.
                dts = obj.ImportarPlan(ddlPlan.SelectedValue, CType(txtSelectedMLValues2.Value, String), CType(txtSelectedMLValues.Value, String), Request.QueryString("id"))

                If dts.Rows.Count > 0 Then
                    lblcnt_Obj.Text = dts.Rows(0).Item("cnt_Obj").ToString
                    lblcnt_Obj.ForeColor = Drawing.Color.Blue

                    lblcnt_meta_obj.Text = dts.Rows(0).Item("cnt_meta_obj").ToString
                    lblcnt_meta_obj.ForeColor = Drawing.Color.Blue

                    lblcnt_ind.Text = dts.Rows(0).Item("cnt_ind").ToString
                    lblcnt_ind.ForeColor = Drawing.Color.Blue

                    lblcnt_meta_ind.Text = dts.Rows(0).Item("cnt_meta_ind").ToString
                    lblcnt_meta_ind.ForeColor = Drawing.Color.Blue

                    lblcnt_formula.Text = dts.Rows(0).Item("cnt_formula").ToString
                    lblcnt_formula.ForeColor = Drawing.Color.Blue

                End If

                lblMensaje.Visible = True
                lblMensaje.ForeColor = Drawing.Color.Black
                lblMensaje.Text = "El proceso de importación se ha realizado correctamente."
                Me.Image1.Attributes.Add("src", "../Images/accept.png")
                Me.avisos.Attributes.Add("class", "mensajeExito")

                btnSubmit.Enabled = False
                EstadoControles(True)
                ddlPlan.Enabled = False

                'Where 1000 is in milliseconds ( 1 second)
                'Page.ClientScript.RegisterStartupScript(Me.GetType(), "refresh", "window.setTimeout('var url = window.location.href;window.location.href = url',3000);", True)

            Else
                lblMensaje.Visible = True
                lblMensaje.ForeColor = Drawing.Color.Black
                lblMensaje.Text = "Favor de seleccionar los datos pertinentes de los cuadros desplegables, para ejecutar el proceso de importación."
                Me.Image1.Attributes.Add("src", "../Images/exclamation.png")
                Me.avisos.Attributes.Add("class", "mensajeError")
                btnSubmit.Enabled = True
                EstadoControles(False)

                Exit Sub
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub EstadoControles(ByVal vEstado As Boolean)
        Try
            Me.txtSelectedMLValues.Disabled = vEstado
            Me.txtSelectedMLValues2.Disabled = vEstado
            Me.imgShowHide.Disabled = vEstado
            Me.imgShowHide2.Disabled = vEstado
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub ddlPlan_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPlan.SelectedIndexChanged
        Try
            Dim obj As New clsIndicadores
            Dim dts As New Data.DataTable

            CargarComboAniosOrigen()
            CargarComboAniosDestino(ddlPlan.SelectedValue)

            If ddlPlan.SelectedValue = 0 Then
                EstadoControles(True)
            Else
                EstadoControles(False)
            End If

            lblMensaje.Text = ""
            Me.Image1.Attributes.Add("src", "../Images/beforelastchild.GIF")
            Me.avisos.Attributes.Add("class", "none")

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub cmdCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdCancelar.Click
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "refresh", "window.setTimeout('var url = window.location.href;window.location.href = url',0);", True)
    End Sub
End Class
