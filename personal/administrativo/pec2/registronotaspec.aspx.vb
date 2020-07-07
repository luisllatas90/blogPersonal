Partial Class registronotaspec
    Inherits System.Web.UI.Page
    Dim crd, ht, hi, ha, hep, th As Integer
    Dim ap, dp, np As Double
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If IsPostBack = False Then
            Dim codigo_tfu As Int16 = Request.QueryString("ctf")
            Dim codigo_usu As Integer = Request.QueryString("id")

            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            ClsFunciones.LlenarListas(Me.dpPrograma, obj.TraerDataTable("PEC_ConsultarProgramaEC", 8, codigo_usu,codigo_tfu,"FC"), "codigo_pec", "descripcion_pes", "--Seleccione--")
            obj.CerrarConexion()
            obj = Nothing
        End If
    End Sub

    Protected Sub dpPrograma_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dpPrograma.SelectedIndexChanged
        MostrarModulosVersionPEC(dpPrograma.SelectedValue)
    End Sub

    Protected Sub grwModulosPEC_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grwModulosPEC.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim fila As Data.DataRowView
            fila = e.Row.DataItem

          
            crd += fila("creditos_cur")
            ht += fila("horasteo_cur")
            hi += fila("horaspra_cur")
            ha += fila("horasase_cur")
            hep += fila("horaslab_cur")
            th += fila("totalhoras_cur")
            ap += fila("aprobados")
            dp += fila("desaprobados")
            np += fila("pendientes")
            e.Row.Cells(1).Text = "<a target='_blank' href='registronotasmodulopec.aspx?id=" & Request.QueryString("id") & "&p=" & Me.dpPrograma.SelectedValue & "&c=" & fila.Row("codigo_cup") & "'>" & fila.Row("nombre_cur") & "</a>"
        ElseIf e.Row.RowType = DataControlRowType.Footer Then
            e.Row.Cells(1).Text = "TOTAL"
            e.Row.Cells(1).HorizontalAlign = HorizontalAlign.Right
            e.Row.Cells(2).Text = crd
            e.Row.Cells(3).Text = ht
            e.Row.Cells(4).Text = hi
            e.Row.Cells(5).Text = ha
            e.Row.Cells(6).Text = hep
            e.Row.Cells(7).Text = th
            e.Row.Cells(8).Text = ap
            e.Row.Cells(9).Text = dp
            e.Row.Cells(10).Text = np
        End If
    End Sub

    Private Sub MostrarModulosVersionPEC(ByVal id As Integer)
        Me.grwModulosPEC.Visible = False
        If id <> -1 Then
            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            Me.grwModulosPEC.DataSource = obj.TraerDataTable("PEC_ConsultarEvaluacionPEC", 0, id, 0, 0)
            Me.grwModulosPEC.DataBind()
            Me.grwModulosPEC.Visible = True
        End If
    End Sub
End Class