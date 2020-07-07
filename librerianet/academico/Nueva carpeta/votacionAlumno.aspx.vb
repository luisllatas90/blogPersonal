
Partial Class academico_votacionAlumno
    Inherits System.Web.UI.Page
    Dim carrera As Integer
    Dim proceso As Integer '' identificador del proceso de votación
    Dim codigo_alu As Integer

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        carrera = 32
        proceso = 1
        codigo_alu = Request.QueryString("codigo_alu")

        Dim obj As New ClsConectarDatos
        Dim rs As New Data.DataTable

        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        rs = obj.TraerDataTable("VOT_GestionaVotacion", "C", codigo_alu, 0, carrera, proceso)
        obj.CerrarConexion()
        If rs.Rows(0).Item(0) = True Then
            lblMensaje.Visible = True
            cmdVotar.Enabled = False
            rbCandidato1.Enabled = False
            rbCandidato2.Enabled = False
            rbCandidato3.Enabled = False
            rbCandidato4.Enabled = False
            rbCandidato5.Enabled = False
        Else
            lblMensaje.Visible = False
            cmdVotar.Enabled = True
            rbCandidato1.Enabled = True
            rbCandidato2.Enabled = True
            rbCandidato3.Enabled = True
            rbCandidato4.Enabled = True
            rbCandidato5.Enabled = True
        End If

    End Sub

    Protected Sub cmdVotar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdVotar.Click
        Dim Votocodigo_alu As Integer
        Votocodigo_alu = 0 'inicializamos para el caso de voto en blanco

        If Me.rbCandidato1.Checked = True Then
            Votocodigo_alu = 11343
        End If
        If Me.rbCandidato2.Checked = True Then
            Votocodigo_alu = 11796
        End If
        If Me.rbCandidato3.Checked = True Then
            Votocodigo_alu = 11399
        End If
        If Me.rbCandidato4.Checked = True Then
            Votocodigo_alu = 15045
        End If
        If Me.rbCandidato5.Checked = True Then
            Votocodigo_alu = 0
        End If
        'Response.Write(Votocodigo_alu)

        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        obj.Ejecutar("VOT_GestionaVotacion", "R", codigo_alu, Votocodigo_alu, carrera, proceso)
        obj.CerrarConexion()
        lblMensaje.Visible = True
        cmdVotar.Enabled = False
        rbCandidato1.Enabled = False
        rbCandidato2.Enabled = False
        rbCandidato3.Enabled = False
        rbCandidato4.Enabled = False
        rbCandidato5.Enabled = False
    End Sub
End Class
