
Partial Class frmcambiarasesor
    Inherits System.Web.UI.Page

    Protected Sub cmdBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdBuscar.Click
        BuscarTesisCodigo()
    End Sub
    Private Sub BuscarTesisCodigo()
        Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
        Dim Tbl As Data.DataTable

        Tbl = obj.TraerDataTable("TES_ConsultarTesis", 4, Me.txtTermino.Text.Trim, 0, 0)
        If Tbl.Rows.Count > 0 Then
            Me.hdCodigo_Tes.Value = Tbl.Rows(0).Item("codigo_tes").ToString
            Me.lblcodigo.Text = Tbl.Rows(0).Item("codigoreg_tes").ToString
            Me.lblFase.Text = Tbl.Rows(0).Item("nombre_Eti")
            Me.lblTitulo.Text = Tbl.Rows(0).Item("Titulo_Tes")
            Me.lblProblema.Text = Tbl.Rows(0).Item("Problema_Tes")

            '*******************************************
            'CARGAR AUTORES DE LA TESIS
            '*******************************************
            Me.lstAutores.DataSource = Tbl
            Me.lstAutores.DataBind()

            '*******************************************
            'CARGAR ASESORES
            '*******************************************
            Me.dlAsesores.DataSource = obj.TraerDataTable("TES_ConsultarResponsableTesis", 5, Tbl.Rows(0).Item("codigo_tes"), 0, 1)
            Me.dlAsesores.DataBind()

        End If
        Tbl.Dispose()
        obj = Nothing
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub dlAsesores_DeleteCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataListCommandEventArgs) Handles dlAsesores.DeleteCommand
        If Me.dlAsesores.DataKeys.Count >= 1 Then
            Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
            obj.IniciarTransaccion()
            obj.Ejecutar("CambiarAsesorTesis", hdCodigo_Tes.Value, CType(e.Item.FindControl("hdCodigoR_tes"), HiddenField).Value)
            obj.TerminarTransaccion()
            Me.dlAsesores.DataBind()
            'Me.CargarDatosTesis()
        End If
    End Sub

End Class
