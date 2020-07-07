
Partial Class indicadores_frmListaCecosConfiguradosVariables
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                txtCodigo.Text = Request.QueryString("codigo_var")
                DescripcionVariable()
                CargaListaCentroCostosConfigurados()
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub DescripcionVariable()
        Try
            Dim obj As New clsIndicadores
            Dim dts As New Data.DataTable

            dts = obj.InformacionVariableCeco("I", Request.QueryString("codigo_var"), Request.QueryString("nivel"), 0)
            If dts.Rows.Count = 1 Then
                txtNombre.Text = dts.Rows(0).Item("Nombre").ToString
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub CargaListaCentroCostosConfigurados()
        Try
            Dim obj As New clsIndicadores
            Dim dts As New Data.DataTable
            dts = obj.InformacionVariableCeco("L", Request.QueryString("codigo_var"), Request.QueryString("nivel"), 0)
            If dts.Rows.Count > 0 Then
                gvListaCecos.DataSource = dts
                gvListaCecos.DataBind()
            Else
                gvListaCecos.DataSource = Nothing
                gvListaCecos.DataBind()
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub gvListaCecos_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvListaCecos.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                e.Row.Attributes.Add("OnMouseOver", "Resaltar(1,this,'S')")
                e.Row.Attributes.Add("OnMouseOut", "Resaltar(0,this,'S')")
                e.Row.Cells(0).Text = e.Row.RowIndex + 1
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub gvListaCecos_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles gvListaCecos.RowDeleting
        Try
            Dim obj As New clsIndicadores
            Dim dts As New Data.DataTable

            'La columna Codigo esta oculta. Por eso uso el DataKey de la fila seleccionada
            dts = obj.InformacionVariableCeco("E", Request.QueryString("codigo_var"), Request.QueryString("nivel"), gvListaCecos.DataKeys(e.RowIndex).Value.ToString)

            If dts.Rows.Count > 0 Then
                If dts.Rows(0).Item("rpt") = 1 Then
                    lblMensaje.Visible = True
                    lblMensaje.Text = dts.Rows(0).Item("Mensaje").ToString
                    Me.Image1.Attributes.Add("src", "../Images/accept.png")
                    Me.avisos.Attributes.Add("class", "mensajeExito")
                Else
                    lblMensaje.Visible = True
                    lblMensaje.Text = dts.Rows(0).Item("Mensaje").ToString
                    Me.Image1.Attributes.Add("src", "../Images/exclamation.png")
                    Me.avisos.Attributes.Add("class", "mensajeError")
                End If
            End If

            CargaListaCentroCostosConfigurados()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub btnEliminar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEliminar.Click
        Try
            If (CheckEliminaCeco() = True) Then
                lblMensaje.Visible = True
                lblMensaje.ForeColor = Drawing.Color.Black
                lblMensaje.Text = "Se eliminaron los centros de costo seleccionados"
                Me.Image1.Attributes.Add("src", "../Images/accept.png")
                Me.avisos.Attributes.Add("class", "mensajeExito")
            Else
                lblMensaje.Visible = True
                lblMensaje.ForeColor = Drawing.Color.Black
                lblMensaje.Text = "Error al eliminar, favor de seleccionar los centros de costo.."
                Me.Image1.Attributes.Add("src", "../Images/exclamation.png")
                Me.avisos.Attributes.Add("class", "mensajeError")
            End If

            CargaListaCentroCostosConfigurados()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub


    Private Function CheckEliminaCeco() As Boolean
        Try
            Dim obj As New clsIndicadores
            Dim dts As New Data.DataTable

            Dim Fila As GridViewRow
            Dim sw As Byte = 0
            Dim codigo_acc As String

            For i As Integer = 0 To gvListaCecos.Rows.Count - 1
                'Capturamos las filas que estan activas
                Fila = gvListaCecos.Rows(i)
                Dim valor As Boolean = CType(Fila.FindControl("chkElegir"), CheckBox).Checked

                If (valor = True) Then
                    sw = 1
                    codigo_acc = gvListaCecos.DataKeys(Fila.RowIndex).Value
                    dts = obj.InformacionVariableCeco("E", "", 0, codigo_acc)
                End If
            Next

            If (sw = 1) Then
                Return True
            End If

            Return False

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Function


End Class
