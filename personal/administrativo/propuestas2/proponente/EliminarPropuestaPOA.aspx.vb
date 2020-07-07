
Partial Class administrativo_propuestas2_proponente_EliminarPropuestaPOA
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ''Call wf_CargarListas()
    End Sub

   
    'Protected Sub btnConsultar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnConsultar.Click

    'End Sub

    Sub wf_CargarListas()
        'Try
        '    Dim dtConsultar As New Data.DataTable
        '    Dim obj As New ClsConectarDatos

        '    obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        '    obj.AbrirConexion()
        '    dtConsultar = obj.TraerDataTable("PRP_ListaPropuestasPOA", "%", 1)
        '    obj.CerrarConexion()

        '    dgvPropuestas.DataSource = dtConsultar
        '    dgvPropuestas.DataBind()
        '    dgvPropuestas.Dispose()
        '    obj = Nothing

        '    'Me.lbl_Mensaje.Text = nContador.ToString & " REGISTROS ACTIVOS  DE " & Me.dgv_Personal.Rows.Count.ToString & " REGISTROS ENCONTRADOS"
        'Catch ex As Exception
        '    Response.Write(ex.Message)
        'End Try
    End Sub

    Protected Sub btnConsultar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnConsultar.Click

    End Sub
End Class
