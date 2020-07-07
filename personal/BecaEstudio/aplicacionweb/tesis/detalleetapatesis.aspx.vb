
Partial Class detalleetapatesis
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            CargarEtapaTesis()
        End If
    End Sub

    Protected Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Cells(0).Text = e.Row.RowIndex + 1

            If Me.GridView1.DataKeys(e.Row.RowIndex).Values("codigo_Eti") = 4 Then
                e.Row.Cells(4).Text = ""
            Else
                e.Row.Cells(4).Attributes.Add("onclick", "return confirm('¿Esta seguro que desea eliminar el registro?');")
            End If
        End If
    End Sub

    Protected Sub GridView1_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles GridView1.RowDeleting
        Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
        Dim mensaje As String
        obj.IniciarTransaccion()
        mensaje = obj.Ejecutar("TES_EliminarEtapaInvestigacionTesis", Me.GridView1.DataKeys(e.RowIndex).Values(0), "")
        obj.TerminarTransaccion()
        obj = Nothing
        e.Cancel = True
        If mensaje <> "" Then
            Me.lblEstado.Visible = True
            Me.lblEstado.Text = mensaje
        Else
            Me.CargarEtapaTesis()
        End if
    End Sub
    Private Sub CargarEtapaTesis()
        Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
        Dim tbl As New Data.DataTable

        tbl = obj.TraerDataTable("TES_ConsultarEtapaInvestigacionTesis", 2, Request.QueryString("codigo_tes"), 0, 0)

        If tbl.Rows.Count > 0 Then
            Me.GridView1.DataSource = tbl
            Me.GridView1.DataBind()
            If tbl.Rows(0).Item("UltimaEtapa").ToString = "7" Then
                Me.cmdNuevo.Visible = False
                Me.lblEstado.Visible = True
            Else
                Me.lblEstado.Visible = False
                Me.cmdNuevo.Visible = True
            End If
        End If
        obj = Nothing
        Me.cmdNuevo.Attributes.Add("OnClick", "AbrirPopUp('frmetapainvestigaciontesis.aspx?accion=A&codigo_tes=" + Request.QueryString("codigo_tes") + "&codigo_per=" + Request.QueryString("codigo_per") + "','380','500');return(false)")

    End Sub
End Class
