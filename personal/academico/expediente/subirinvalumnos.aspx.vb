Partial Class subirinvalumnos
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            Me.TxtDescripcion.Attributes.Add("OnKeyUp", "ContarTextArea(this,'500',LblMuestra)")
            Me.LblDocente.Text = Request.QueryString("nombre_per")
            Me.LblAsignatura.Text = Request.QueryString("nombre_cur")
            Me.CmdSubir.Attributes.Add("onclick", "AbrirPopUp('subir.aspx?codigo_per=" & Request.QueryString("codigo_per") & "&codigo_cac=" & Request.QueryString("codigo_cac") & "&codigo_cup=" & Request.QueryString("codigo_cup") & "','180','388');return false;")
            Me.CmdGuardar.Enabled = False
        End If
        Me.GridInv.DataBind()
    End Sub

    Private Sub LlenaAlumnos()
        Dim ObjAlu As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
        Dim objLista As New ClsFunciones
        ClsFunciones.LlenarListas(Me.ListaDe, ObjAlu.TraerDataTable("INVALU_ConsultarAlumnosMatTodos", Request.QueryString("codigo_cup")), "codigo_dma", "alumno")
        ClsFunciones.LlenarListas(Me.ListaPara, ObjAlu.TraerDataTable("INVALU_ConsultarAlumnosRegistrados", Me.HddCodigoInv.Value), "codigo_dma", "alumno")
        ObjAlu = Nothing
        objLista = Nothing
    End Sub

    Protected Sub GridInv_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridInv.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim fila As Data.DataRowView
            Dim ext As String
            fila = e.Row.DataItem 'Los datos devueltos (cuando quiero saber la data que llega)
            ext = Right(fila.Row(3).ToString, 3)
            e.Row.Attributes.Add("OnClick", "javascript:__doPostBack('GridInv','Select$" & e.Row.RowIndex & "')")
            e.Row.Style.Add("Cursor", "hand")
            e.Row.Cells(5).Text = "<img border='0' src='../../../images/ext/" & ext & ".gif'>"
        End If
    End Sub

    Protected Sub GridInv_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridInv.SelectedIndexChanged
        Dim Obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
        Dim Datos As New Data.DataTable
        Datos = Obj.TraerDataTable("INVALU_ConsultarInvestigacion", Me.GridInv.SelectedValue)
        Me.HddCodigoInv.Value = Me.GridInv.SelectedValue
        Me.TxtTitulo.Text = Datos.Rows(0).Item("titulo").ToString
        Me.TxtDescripcion.Text = Datos.Rows(0).Item("descripcion").ToString
        Me.Linkprevio.NavigateUrl = "../../../INV/" & Datos.Rows(0).Item("ruta").ToString.Replace("\", "/")
        Response.Write("<script>parent.rightFrame.location.href='vacio.htm'</script>")
        LlenaAlumnos()
        Obj = Nothing
        Datos = Nothing
        Me.CmdGuardar.Enabled = True
    End Sub

    Protected Sub CmdAgregar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdAgregar.Click
        For i As Int32 = 0 To Me.ListaDe.GetSelectedIndices.Length - 1
            Dim ItemList As New System.Web.UI.WebControls.ListItem
            ItemList.Value = Me.ListaDe.Items(Me.ListaDe.GetSelectedIndices.GetValue(i)).Value
            ItemList.Text = Me.ListaDe.Items(Me.ListaDe.GetSelectedIndices.GetValue(i)).Text
            Me.ListaPara.Items.Add(ItemList)
        Next
        For i As Int32 = Me.ListaDe.GetSelectedIndices.Length - 1 To 0 Step -1
            Me.ListaDe.Items.RemoveAt(Me.ListaDe.GetSelectedIndices.GetValue(i))
        Next
    End Sub

    Protected Sub CmdRetirar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdRetirar.Click
        For i As Int32 = 0 To Me.ListaPara.GetSelectedIndices.Length - 1
            Dim ItemList As New System.Web.UI.WebControls.ListItem
            ItemList.Value = Me.ListaPara.Items(Me.ListaPara.GetSelectedIndices.GetValue(i)).Value
            ItemList.Text = Me.ListaPara.Items(Me.ListaPara.GetSelectedIndices.GetValue(i)).Text
            Me.ListaDe.Items.Add(ItemList)
        Next
        For i As Int32 = Me.ListaPara.GetSelectedIndices.Length - 1 To 0 Step -1
            Me.ListaPara.Items.RemoveAt(Me.ListaPara.GetSelectedIndices.GetValue(i))
        Next
    End Sub

    Protected Sub CmdGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdGuardar.Click
        Dim ObjInv As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
        Dim Check As Boolean
        Check = True
        Try
            ObjInv.IniciarTransaccion()
            ObjInv.Ejecutar("INVALU_ActualizarTitulo", Me.TxtTitulo.Text.Trim.ToUpper, Me.TxtDescripcion.Text.Trim, Check, HddCodigoInv.Value)
            For i As Int32 = 0 To Me.ListaPara.Items.Count - 1
                ObjInv.Ejecutar("INVALU_AgregarResponsables", Me.ListaPara.Items(i).Value, Me.HddCodigoInv.Value)
            Next
            ObjInv.TerminarTransaccion()
            Me.GridInv.DataBind()
            Response.Write("<script>alert('Se ha guardado la investigacion satisfactoriamente')</script>")
        Catch ex As Exception
            Response.Write("<script>alert('Ocurrio un error al procesar los datos')</script>")
            ObjInv.AbortarTransaccion()
        End Try
    End Sub
End Class

