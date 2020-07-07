Imports System.Data
Partial Class Egresados_frmOfertasLaborales
    Inherits System.Web.UI.Page
#Region "Eventos"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Me.IsPostBack Then
            Dim PrimerDiaDelMes As String
            'UltimoDiaDelMes = DateSerial(Year(dtmFecha), Month(dtmFecha) + 1, 0)
            PrimerDiaDelMes = DateSerial(Year(DateTime.Now), Month(DateTime.Now), 1)
            'Me.txtPrueba.Text = PrimerDiaDelMes
            txtFechIni.Text = PrimerDiaDelMes '"01/10/2019" DateTime.Now.ToString("dd/MM/yyyy")
            txtFechFin.Text = DateTime.Now.ToString("dd/MM/yyyy")
            CargarGrillaOfertas()
        End If
    End Sub
    Protected Sub btnCrear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCrear.Click
        'Response.Redirect("frmOfertaLaboralV2.aspx?id=" & Request.QueryString("id") & "&ctf=" & Request.QueryString("ctf"))
        Dim des = ""
        Try
            ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script>openModal('nuevo', '" & des & "');</script>")
        Catch ex As Exception
            Response.Write(ex.Message.ToString)
        End Try

        Dim sb As New StringBuilder
9292:
    End Sub
    Protected Sub btnBuscaL_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBuscaL.Click
        CargarGrillaOfertas()
    End Sub
    'Protected Sub gvwOfertas_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gvwOfertas.PageIndexChanging
    '    Me.gvwOfertas.PageIndex = e.NewPageIndex
    '    CargarGrillaOfertas()
    '    Me.gvwOfertas.DataBind()
    'End Sub
#End Region

#Region "Funciones"
    Function CargarDatos() As Data.DataTable
        Dim dtDatos As New Data.DataTable
        Dim obj As New ClsConectarDatos
        Try
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            dtDatos = obj.TraerDataTable("")
            obj.CerrarConexion()
            obj = Nothing
        Catch ex As Exception
            Response.Write(ex.Message.ToString)
        End Try
        Return dtDatos
    End Function
    Function CargarGrillaOfer() As Data.DataTable
        Dim dtOfertas As New Data.DataTable
        Dim obj As New ClsConectarDatos
        Try
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            dtOfertas = obj.TraerDataTable("ALUMNI_ListaOfertasByFechasAndCarrera", Me.txtFechIni.Text.Trim, Me.txtFechFin.Text.Trim)
            obj.CerrarConexion()
            obj = Nothing
        Catch ex As Exception
            Response.Write(ex.Message.ToString)
        End Try
        Return dtOfertas
    End Function

#End Region

#Region "Metodos"
    Private Sub CargarGrillaOfertas()
        Try
            Dim dt As New Data.DataTable
            dt = CargarGrillaOfer()
            If dt.Rows.Count = 0 Then
                'Me.txtPrueba.Text = "No se encontraron registros"
                'Me.gvwOfertas.DataSource = Nothing
                Me.gvwOfertasDt.DataSource = Nothing
            Else
                'Me.gvwOfertas.DataSource = dt
                Me.gvwOfertasDt.DataSource = dt
            End If
            'Me.gvwOfertas.DataBind()
            Me.gvwOfertasDt.DataBind()
            dt.Dispose()
        Catch ex As Exception
            Response.Write(ex.Message.ToString)
        End Try
    End Sub
#End Region
    Protected Sub bntModOlGuarda_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bntModOlGuarda.Click


        Dim des = ""
        'Try
        '    ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script>openModalEmp('nuevo', '" & des & "');</script>")
        'Catch ex As Exception
        '    Response.Write(ex.Message.ToString)
        'End Try
        Try
            Dim sb As System.Text.StringBuilder = New System.Text.StringBuilder()
            'sb.Append("<script type='text/javascript'>")
            sb.Append("<script>")
            'sb.Append("$('#modalSelecEmpresa').modal('show');")
            sb.Append("openModalEmp('nuevo', '" & des & "');")
            sb.Append("</script>")
            'ScriptManager.RegisterClientScriptBlock(Me, Me.[GetType](), "AddShowModalScript", sb.ToString(), False)
            ScriptManager.RegisterClientScriptBlock(Me, Me.[GetType](), "AddShowModalScript", sb.ToString(), False)

        Catch ex As Exception
            Response.Write(ex.Message.ToString)
        End Try









    End Sub
End Class
