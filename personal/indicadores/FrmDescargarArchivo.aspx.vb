
Partial Class indicadores_FrmDescargarArchivo
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            CargarComboPlan()
        End If
    End Sub

    Private Sub CargaArchivos(ByVal codigo_pla As Integer)
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        Try
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            dt = obj.TraerDataTable("IND_ListaDescargaArchivosPlan", codigo_pla, Request.QueryString("id"), Request.QueryString("ctf"))
            obj.CerrarConexion()

            Me.gvArchivos.DataSource = dt
            Me.gvArchivos.DataBind()

        Catch ex As Exception
            Response.Write("Error: " & ex.Message)
        End Try
    End Sub

    Private Sub CargarComboPlan()
        Try
            Dim obj As New clsIndicadores
            Dim dts As New Data.DataTable
            dts = obj.ListaPlanes(Me.Request.QueryString("ctf"), Request.QueryString("id"))

            If dts.Rows.Count > 0 Then
                cboPlan.DataSource = dts
                cboPlan.DataTextField = "Descripcion"
                cboPlan.DataValueField = "Codigo"
                cboPlan.DataBind()
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub cboPlan_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboPlan.SelectedIndexChanged
        If (Me.cboPlan.Items.Count > 0) Then
            CargaArchivos(Me.cboPlan.SelectedValue)
        End If
    End Sub

    Protected Sub gvArchivos_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvArchivos.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            'Muestra al usuario si la variable tiene registrados valores en los diferentes periodos.
            If e.Row.Cells(2).Text <> "" Then
                e.Row.Cells(2).Text = "<center><a href='archivos/" & e.Row.Cells(2).Text & "'><img src='../images/desc.png' style='border: 0px' alt='Descargar archivo'/></a></center>"
            Else
                e.Row.Cells(2).Text = "-"
            End If

        End If
    End Sub
End Class
