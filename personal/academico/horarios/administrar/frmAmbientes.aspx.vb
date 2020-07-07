Partial Class academico_horarios_administrar_frmAmbientes
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            ClsFunciones.LlenarListas(Me.ddlTipoAmbiente, obj.TraerDataTable("AsignarAmbiente_ListarAmbientes"), "codigo_tam", "descripcion_Tam")
            ClsFunciones.LlenarListas(Me.ddlUbicacion, obj.TraerDataTable("Ambiente_ListarUbicacion"), "codigo_ube", "descripcion_ube")
            Me.ddlTipoAmbiente.SelectedValue = Session("ddlTipoAmbiente")
            Me.ddlUbicacion.SelectedValue = Session("ddlUbicacion")
            Me.txtNombre.Text = Session("txtnombre")
            Me.CheckBox1.Checked = Session("CheckBox1")
            obj.CerrarConexion()
            obj = Nothing
            cargarListaAmbientes()
            gridLista.DataBind()
        Else            
            Session("ddlTipoAmbiente") = Me.ddlTipoAmbiente.SelectedValue
            Session("ddlUbicacion") = Me.ddlUbicacion.SelectedValue
            Session("txtnombre") = Me.txtNombre.Text
            Session("CheckBox1") = Me.CheckBox1.Checked

        End If

        Me.txtNombre.Enabled = Me.CheckBox1.Checked
        If Me.CheckBox1.Checked Then
            Me.txtNombre.BackColor = Drawing.Color.White
            Me.txtNombre.Focus()
        Else
            Me.txtNombre.BackColor = Drawing.Color.WhiteSmoke
            Me.txtNombre.Text = ""
        End If
    End Sub
    Sub cargarListaAmbientes()
        Try
            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            Dim texto As String
            If Me.CheckBox1.Checked Then texto = Me.txtNombre.Text.Trim Else texto = "%"
            Me.gridLista.DataSource = obj.TraerDataTable("Ambiente_ListarAmbienteCarac", Me.ddlTipoAmbiente.SelectedValue, Me.ddlUbicacion.SelectedValue, IIf(Me.chkActivo.Checked = True, 1, 0), Me.txtCapacidad.Text, texto)         
            gridLista.DataBind()
            lblContador.Text = "Existen (" & gridLista.Rows.Count & ") ambientes."
            obj.CerrarConexion()
            obj = Nothing
        Catch ex As Exception
        End Try      
    End Sub

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        cargarListaAmbientes()        
    End Sub

    Protected Sub gridLista_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridLista.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            If gridLista.DataKeys(e.Row.RowIndex).Values("Editar").ToString > 0 Then
                Dim tb As New Data.DataTable
                tb = gridLista.DataSource
                For i As Integer = 5 To tb.Columns.Count - 1 '+ 1
                    If e.Row.Cells(i).Text = "1" Then
                        e.Row.Cells(i).Text = "<img src='images/yes.png'>"
                    Else
                        e.Row.Cells(i).Text = "<img src='images/no.png'>"
                    End If
                Next
                tb.Dispose()
                e.Row.Cells(1).Text = "<a href=""" & "frmAmbienteRegistrar.aspx?codigo_amb=" & gridLista.DataKeys(e.Row.RowIndex).Values("Editar").ToString & """><img src=""images/edit.png"" style=""border:0px;"" alt=""Editar""></img></a>"

                If e.Row.Cells(2).Text = "1" Then
                    e.Row.Cells(2).Text = "<img src='images/star.png' title='Ambiente preferencial'>"
                Else
                    e.Row.Cells(2).Text = "<img src='images/door.png' title='Ambiente preferencial'>"
                End If

                If Not e.Row.Cells(20).Text = "ACTIVO" Then
                    e.Row.Cells(4).ForeColor = Drawing.Color.Gray
                    e.Row.Cells(3).ForeColor = Drawing.Color.Gray
                    e.Row.Cells(20).ForeColor = Drawing.Color.Gray
                End If

            End If
        End If
    End Sub

    Protected Sub gridLista_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles gridLista.RowDeleting
        Try
            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            Dim tb As Data.DataTable
            tb = obj.TraerDataTable("Ambiente_Inactivar", CInt(Me.gridLista.DataKeys.Item(e.RowIndex).Values(0).ToString))
            obj.CerrarConexion()
            e.Cancel = True
            If tb.Rows.Count Then
                Me.RegisterStartupScript("Aviso", "<script>alert('" & tb.Rows(0).Item(0).ToString & "')</script>")
            Else
                cargarListaAmbientes()
                Me.RegisterStartupScript("cerrar", "<script>alert('Se ha inactivado el ambiente.'); window.close();</script>")
            End If
            'ClientScript.RegisterStartupScript(Me.GetType, "cerrar", "<script>window.close();</script>")
        Catch ex As Exception
            Me.RegisterStartupScript("cerrar", "<script>alert('Error');</script>")
        End Try

    End Sub

    Protected Sub CheckBox1_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CheckBox1.CheckedChanged
        If Me.CheckBox1.Checked Then
            Me.txtNombre.BackColor = Drawing.Color.White
            Me.txtNombre.Focus()
        Else
            Me.txtNombre.BackColor = Drawing.Color.WhiteSmoke
        End If
        Me.ddlTipoAmbiente.Enabled = Not CheckBox1.Checked
        Me.ddlUbicacion.Enabled = Not CheckBox1.Checked
        Me.chkActivo.Enabled = Not CheckBox1.Checked
        Me.txtCapacidad.Enabled = Not CheckBox1.Checked
        Me.btnNuevo.Enabled = Not CheckBox1.Checked
    End Sub

 
End Class
