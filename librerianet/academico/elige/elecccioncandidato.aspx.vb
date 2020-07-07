
Partial Class academico_votacionAlumno
    Inherits System.Web.UI.Page
    Dim carrera As Integer
    Dim proceso As Integer '' identificador del proceso de votación
    Dim codigo_alu As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        carrera = 11
        proceso = 4
        ' Response.Write(Request.QueryString("ctm") & "<br>")

        'codigo_alu = Request.QueryString("codigo_alu")

        Dim objEnc As New EncriptaCodigos.clsEncripta
        Dim obj As New ClsConectarDatos
        Dim rs As New Data.DataTable
        'Response.Write(Request.QueryString("ctm"))

        codigo_alu = Mid(objEnc.Decodifica(Request.QueryString("ctm")), 4)
        codigo_alu = Mid(objEnc.Decodifica(codigo_alu), 4)

        ' Response.Write(codigo_alu)
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        rs = obj.TraerDataTable("VOT_GestionaVotacion", "C", codigo_alu, 0, carrera, proceso)
        obj.CerrarConexion()
        'Response.Write(Mid(objEnc.Decodifica(Request.QueryString("ctm")), 4))
        'Response.Write("<br>" & rs.Rows(0).Item(0))
        If rs.Rows(0).Item(0) = 3 Then
            lblMensaje.Visible = False
            cmdVotar.Enabled = False
            rbCandidato1.Enabled = False
            rbCandidato2.Enabled = False

            'rbCandidato3.Enabled = False
            'rbCandidato4.Enabled = False

            rbCandidato5.Enabled = False
            lblMensaje2.visible = True
        Else
            lblMensaje2.visible = False
            If rs.Rows(0).Item(0) = 1 Then
                lblMensaje.Visible = True
                cmdVotar.Enabled = False
                rbCandidato1.Enabled = False
                rbCandidato2.Enabled = False

                'rbCandidato3.Enabled = False
                'rbCandidato4.Enabled = False

                rbCandidato5.Enabled = False
            Else
                lblMensaje.Visible = False
                cmdVotar.Enabled = True
                rbCandidato1.Enabled = True
                rbCandidato2.Enabled = True

                'rbCandidato3.Enabled = True
                'rbCandidato4.Enabled = True

                rbCandidato5.Enabled = True
            End If
        End If


    End Sub

    Protected Sub cmdVotar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdVotar.Click
        Dim Votocodigo_alu As String
        Votocodigo_alu = 0 'inicializamos para el caso de voto en blanco

        If Me.rbCandidato1.Checked = True Then
            Votocodigo_alu = 9821
        End If
        If Me.rbCandidato2.Checked = True Then
            Votocodigo_alu = 14660
        End If
        'If Me.rbCandidato3.Checked = True Then
        '    Votocodigo_alu = 0
        'End If
        'If Me.rbCandidato4.Checked = True Then
        '    Votocodigo_alu = 0
        'End If

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
        'rbCandidato3.Enabled = False
        'rbCandidato4.Enabled = False
        rbCandidato5.Enabled = False
    End Sub



End Class
