
Partial Class administrativo_propuestas2_proponente_resolucion_POA
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (IsPostBack = False) Then
            'Response.Write("<script>alert('" & Request.QueryString("id") & "')</script>")
            Dim ObjCnx As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
            Dim rsFac As New Data.DataTable

            '' Si es Secretario y pertenece a una facultad
            rsFac = ObjCnx.TraerDataTable("PRP_ConsutarReunionesConsejo_POA", "FA", Request.QueryString("id"), "", "")
            If rsFac.Rows.Count > 0 Then
                txtelegido.Value = rsFac.Rows(0).Item("codigo_fac")
            Else
                txtelegido.Value = Request.QueryString("id")
            End If

            Call wf_CargarListas()

            Me.PanelRegistro.Visible = False
        End If
    End Sub

    Sub wf_CargarListas()
        Dim obj As New ClsConectarDatos
        Dim dtt As New Data.DataTable

        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Me.dgvResolucion.DataSource = Nothing

        'Response.Write("<script>alert('PRP_ListaPropuestaResolucion, " & txtelegido.Value & ", K, 0 " & "')</script>")
        dtt = obj.TraerDataTable("PRP_ListaPropuestaResolucion", txtelegido.Value, "K", "0")
        Me.dgvResolucion.DataSource = dtt
        Me.dgvResolucion.DataBind()

        dtt.Dispose()
        obj.CerrarConexion()
        obj = Nothing
    End Sub

    Protected Sub cmdConsultar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdConsultar.Click
        Try
            Call wf_CargarListas()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        Me.PanelConsulta.Visible = True
        Me.PanelRegistro.Visible = False
    End Sub

    Protected Sub btnRegistrar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRegistrar.Click
        Try
            Dim codigo_prp As Integer = txtCodigo_prp.Text
            Dim Resolucion As String = txtResolucion.Text

            'Actualizar N° de Resolución
            Dim obj As New ClsConectarDatos
            Dim dtt As New Data.DataTable

            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            obj.Ejecutar("PRP_ActualizarResolucion", codigo_prp, Resolucion)
            obj.CerrarConexion()
            obj = Nothing
            cmdConsultar_Click(sender, e)
            Me.PanelConsulta.Visible = True
            Me.PanelRegistro.Visible = False
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub dgvResolucion_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles dgvResolucion.RowCommand
        Try
            If (e.CommandName.Equals("Select")) Then 'comprueba que sea el boton de seleccion   
                Dim seleccion As GridViewRow
                Dim codigo_prp As Integer = 0
                Dim resolucion As String = ""
                seleccion = DirectCast(e.CommandSource, GridView).Rows(e.CommandArgument)

                codigo_prp = Convert.ToInt32(Me.dgvResolucion.DataKeys(seleccion.RowIndex).Values("codigo_prp").ToString)
                txtCodigo_prp.Text = codigo_prp
                lblPropuesta.Text = dgvResolucion.Rows(seleccion.RowIndex).Cells(0).Text

                If dgvResolucion.Rows(seleccion.RowIndex).Cells(2).Text = "&nbsp;" Then
                    txtResolucion.Text = ""
                Else
                    txtResolucion.Text = dgvResolucion.Rows(seleccion.RowIndex).Cells(2).Text
                End If
                txtResolucion.Focus()

            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub dgvResolucion_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgvResolucion.SelectedIndexChanged
        Try
            Me.PanelConsulta.Visible = False
            Me.PanelRegistro.Visible = True
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub
End Class
