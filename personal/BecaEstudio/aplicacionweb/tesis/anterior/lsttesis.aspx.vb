
Partial Class lsttesis
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
            Dim tblescuela As Data.DataTable
            ClsFunciones.LlenarListas(Me.dpFase, obj.TraerDataTable("TES_ConsultarEtapaInvestigacionTesis", 0, 0, 0, 0), "codigo_Eti", "nombre_Eti")
            'ClsFunciones.LlenarListas(Me.dpEstado, obj.TraerDataTable("TES_ConsultarEtapaInvestigacionTesis", 1, 0, 0, 0), "codigo_Ein", "descripcion_Ein")
            If Request.QueryString("ctf") = 1 Then
                Me.cmdEliminar.Visible = True
                tblescuela = obj.TraerDataTable("ConsultarCarreraProfesional", "MA", 0)
                ClsFunciones.LlenarListas(Me.dpEscuela, tblescuela, "codigo_cpf", "nombre_cpf", "-Seleccione la Escuela Profesional")
            Else
                tblescuela = obj.TraerDataTable("consultaracceso", "ESC", "", Request.QueryString("id"))
                ClsFunciones.LlenarListas(Me.dpEscuela, tblescuela, "codigo_cpf", "nombre_cpf")
            End If
            Me.dpEstado.SelectedValue = 1
            Me.dpEscuela.SelectedValue = 1
            BuscarTesisRegistradas()
        End If
    End Sub
    Private Sub BuscarTesisRegistradas()
        Me.hdcodigo_per.Value = Request.QueryString("id")
        Me.txtelegido.Value = 0
        If Me.dpEscuela.SelectedValue <> -1 Then
            Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
            Me.GridView1.DataSource = obj.TraerDataTable("TES_ConsultarTesis", 2, Me.dpFase.SelectedValue, 1, Me.dpEscuela.SelectedValue)
            Me.GridView1.DataBind()
            obj = Nothing
        End If
    End Sub

    Protected Sub cmdNuevo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdNuevo.Click
        Response.Redirect("frmtesis.aspx?accion=A&codigo_tes=0&id=" & Request.QueryString("id"))
    End Sub

    Protected Sub cmdModificar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdModificar.Click
        Response.Redirect("frmtesis.aspx?accion=M&codigo_tes=" & Me.txtelegido.Value & "&id=" & Request.QueryString("id"))
    End Sub

    Protected Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim fila As Data.DataRowView
            fila = e.Row.DataItem
            e.Row.Attributes.Add("id", "" & fila.Row("codigo_tes").ToString & "")
            e.Row.Attributes.Add("OnMouseOver", "Resaltar(1,this,'S')")
            e.Row.Attributes.Add("OnMouseOut", "Resaltar(0,this,'S')")
            e.Row.Attributes.Add("OnClick", "HabilitarBoton('M',this)")
            e.Row.Attributes.Add("Class", "Sel")
            e.Row.Attributes.Add("Typ", "Sel")
            e.Row.Cells(0).Text = e.Row.RowIndex + 1
        End If
    End Sub

    Protected Sub CmdBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdBuscar.Click
        BuscarTesisRegistradas()
    End Sub

    Protected Sub cmdEliminar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdEliminar.Click
        Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
        obj.IniciarTransaccion()
        obj.Ejecutar("EliminarTesis", Me.txtelegido.Value)
        obj.TerminarTransaccion()
        obj = Nothing
        BuscarTesisRegistradas()
    End Sub
End Class
