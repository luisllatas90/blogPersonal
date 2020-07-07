
Partial Class detalleavancetesis
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            CargarAsesorias()
        End If
    End Sub
    Private Sub CargarAsesorias()
        Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
        Dim Tbl As New Data.DataTable
        Dim codigo_tes As Integer
        codigo_tes = Request.QueryString("codigo_tes")

        Me.DataList1.DataSource = obj.TraerDataTable("TES_ConsultarAvanceTesis", 1, codigo_tes, 0, 0)
        Me.DataList1.DataBind()
        obj = Nothing

        Me.cmdNuevo.Attributes.Add("OnClick", "AbrirPopUp('frmavancetesis.aspx?accion=A&codigo_tes=" + Request.QueryString("codigo_tes") + "','450','500');return(false)")
        Me.lbltitulo.Text = "Registros de Asesoría de Tesis (" & Me.DataList1.Items.Count & ")"
    End Sub

    Protected Sub CmdCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdCancelar.Click
        Response.Redirect("lsttesisasesoria.aspx?id=" & Session("codigo_usu2"))
    End Sub

    Protected Sub DataList1_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataListCommandEventArgs) Handles DataList1.ItemCommand
        If CDate(CType(e.Item.FindControl("lblfecha"), Label).Text).Date.ToShortDateString <> Date.Today.ToShortDateString Then
            Page.RegisterStartupScript("BloqueoEliminar", "<script>alert('Ud. sólo pueden eliminar asesorias registradas en el día de Hoy: " & Date.Today.ToShortDateString & "')</script>")
        Else
            Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
            obj.IniciarTransaccion()
            obj.Ejecutar("TES_EliminarAvanceTesis", DataList1.DataKeys(e.Item.ItemIndex))
            obj.TerminarTransaccion()
            obj = Nothing
            Dim Ruta As String = "T:\documentos aula virtual\archivoscv\tesis\" & CType(e.Item.FindControl("lnkruta"), HyperLink).Text
            If FileIO.FileSystem.FileExists(Ruta) = True Then
                Kill(Ruta)
            End If
            Me.CargarAsesorias()
        End If
    End Sub
End Class
