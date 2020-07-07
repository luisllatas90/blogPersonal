
Partial Class academico_notas_frmReporteNotas
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If (Request.QueryString("id") IsNot Nothing) Then
                If (IsPostBack = False) Then
                    CargaCiclo()
                    CargaEscuelas()
                End If
            Else
                Response.Redirect("http://www.usat.edu.pe")
            End If
            
        Catch ex As Exception
            Me.lblMensaje.Text = "Error al cargar página."
        End Try
    End Sub

    Public Sub CargaCiclo()
        Try
            Dim dt As New Data.DataTable
            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            dt = obj.TraerDataTable("ConsultarCicloAcademico", "TO", 0)
            obj.CerrarConexion()

            Me.cboCiclo.DataTextField = "descripcion_cac"
            Me.cboCiclo.DataValueField = "codigo_cac"
            Me.cboCiclo.DataSource = dt
            Me.cboCiclo.DataBind()
        Catch ex As Exception
            Me.lblMensaje.Text = "Error al cargar el ciclo"
        End Try
    End Sub

    Public Sub CargaEscuelas()
        Try
            Dim dt As New Data.DataTable
            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            dt = obj.TraerDataTable("ConsultarCarreraProfesional", "MA", 0)
            obj.CerrarConexion()

            Me.cboEscuela.DataValueField = "codigo_cpf"
            Me.cboEscuela.DataTextField = "nombre_cpf"
            Me.cboEscuela.DataSource = dt
            Me.cboEscuela.DataBind()
        Catch ex As Exception
            Me.lblMensaje.Text = "Error al cargar las escuelas profesionales"
        End Try
    End Sub

    Protected Sub btnMostrar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnMostrar.Click        
        EnviarAPagina("frmReporteNotasDet.aspx?cup=" & 0)
        CargaGrid()
    End Sub

    'Protected Sub gvCursos_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gvCursos.PageIndexChanging
    'Me.gvCursos.PageIndex = e.NewPageIndex()
    'cargaGrid()
    'End Sub

    Private Sub CargaGrid()
        Try
            Dim dt As New Data.DataTable
            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            dt = obj.TraerDataTable("ACAD_ConsultarRegistroNotas", Me.cboCiclo.SelectedValue, Me.cboEscuela.SelectedValue, Me.txtCurso.Text, _
                                    IIf(Me.txtFechaInicio.Text.Trim = "", "01/01/1990", Me.txtFechaInicio.Text), _
                                    IIf(Me.txtFechaFin.Text.Trim = "", Date.Today, Me.txtFechaFin.Text))
            obj.CerrarConexion()
            Me.gvCursos.DataSource = dt
            Me.gvCursos.DataBind()
        Catch ex As Exception
            Me.lblMensaje.Text = "Error al cargar busqueda: " & ex.Message
        End Try
    End Sub

    Private Sub EnviarAPagina(ByVal pagina As String)
        Me.fraDetalle.Attributes("src") = pagina & "&id=" & Request.QueryString("id") & "&ctf=" & Request.QueryString("ctf")
    End Sub

    Protected Sub gvCursos_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles gvCursos.RowEditing
        EnviarAPagina("frmReporteNotasDet.aspx?cup=" & Me.gvCursos.DataKeys.Item(e.NewEditIndex).Values(0))
    End Sub
End Class
