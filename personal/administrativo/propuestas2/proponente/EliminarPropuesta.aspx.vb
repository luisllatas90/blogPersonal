
Partial Class administrativo_propuestas2_proponente_TipoPropuestaLista
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (IsPostBack = False) Then
            Call wf_CargarListas()
        End If
    End Sub

    Sub wf_CargarListas()
        Try
            Dim dtConsultar As New Data.DataTable
            Dim obj As New ClsConectarDatos

            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            dtConsultar = obj.TraerDataTable("PRP_ListaPropuestasPOA", "%", 1)
            obj.CerrarConexion()

            dgvPropuestas.DataSource = dtConsultar
            dgvPropuestas.DataBind()
            dgvPropuestas.Dispose()
            obj = Nothing

            'Me.lbl_Mensaje.Text = nContador.ToString & " REGISTROS ACTIVOS  DE " & Me.dgv_Personal.Rows.Count.ToString & " REGISTROS ENCONTRADOS"
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    'Protected Sub dgvPropuestas_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles dgvPropuestas.RowDeleting
    '    Try
    '        Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
    '        Dim codigo_prp As Integer = txtelegido.Value
    '        obj.Ejecutar("PRP_EliminarPropuestaPOA", codigo_prp)

    '        Call wf_CargarListas()
    '    Catch ex As Exception
    '        Response.Write(ex.Message)
    '    End Try
    'End Sub


    'Protected Sub dgvPropuestas_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles dgvPropuestas.RowDataBound
    '    Try
    '        If e.Row.RowType = DataControlRowType.DataRow Then
    '            Dim fila As Data.DataRowView
    '            fila = e.Row.DataItem
    '            e.Row.Attributes.Add("id", "" & fila.Row("codigo_prp").ToString & "")
    '            e.Row.Attributes.Add("OnMouseOver", "Resaltar(1,this,'S')")
    '            e.Row.Attributes.Add("OnMouseOut", "Resaltar(0,this,'S')")
    '            e.Row.Attributes.Add("OnClick", "HabilitarBoton('M',this)")
    '            e.Row.Attributes.Add("Class", "Sel")
    '            e.Row.Attributes.Add("Typ", "Sel")
    '        End If
    '    Catch ex As Exception
    '        Response.Write(ex.Message)
    '    End Try
    'End Sub

    ''Protected Sub cmdConsultar0_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdConsultar0.Click
    ''    Call wf_CargarListas()
    ''End Sub
End Class
