
Partial Class SolicitudRequerimientos
    Inherits System.Web.UI.Page
    Dim cod_per As Int32

    Protected Sub CboCampo_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CboCampo.SelectedIndexChanged
        Dim Objcnx As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
        Me.CboValor.Items.Clear()
        Select Case Me.CboCampo.SelectedValue
            Case 0
                'Me.GvSolicitudes.DataSourceID = Nothing
                Me.GvSolicitudes.DataSource = Objcnx.TraerDataTable("paReq_ConsultarSolicitudesEnEspera", cod_per, 1)
                Me.CboValor.Visible = False
            Case 1
                'Me.GvSolicitudes.DataSourceID = Nothing
                Me.GvSolicitudes.DataSource = Objcnx.TraerDataTable("paReq_ConsultarSolicitudPorModulo", cod_per, "NUE", 1)
                Me.CboValor.Visible = False
            Case 2
                ClsFunciones.LlenarListas(Me.CboValor, Objcnx.TraerDataTable("paReq_consultaraplicacion"), "codigo_apl", "descripcion_apl", "-- Seleccione módulo --")
                Me.CboValor.Visible = True
        End Select
        Me.GvSolicitudes.DataBind()
        Objcnx = Nothing
    End Sub

    Protected Sub CboValor_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CboValor.SelectedIndexChanged
        Dim Objcnx As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
        Me.GvSolicitudes.DataSourceID = Nothing
        Me.GvSolicitudes.DataSource = Objcnx.TraerDataTable("paReq_ConsultarSolicitudPorModulo", cod_per, "MOD", Me.CboValor.SelectedValue)
        Me.GvSolicitudes.DataBind()
        Objcnx = Nothing
    End Sub


    Protected Sub frmRegRequerimientos_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles frmRegRequerimientos.Load
        cod_per = CInt(Request.QueryString("id"))
        Dim ObjCnx As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
        If Not IsPostBack Then
            Me.CboValor.Visible = False
            Me.GvSolicitudes.DataSource = ObjCnx.TraerDataTable("paReq_ConsultarSolicitudesenEspera", CInt(Request.QueryString("id").ToString), 1)
            Me.GvSolicitudes.DataBind()
        End If
    End Sub

    Protected Sub GvSolicitudes_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GvSolicitudes.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim fila As Data.DataRowView
            fila = e.Row.DataItem
            e.Row.Cells(14).Text = "<a href='ListaRequerimientos.aspx?id_sol=" & fila.Row("id_sol").ToString & "&id=" & cod_per.ToString & "'><img border=0 src='../images/kappfinder.gif'></a>"
        End If
    End Sub


End Class
