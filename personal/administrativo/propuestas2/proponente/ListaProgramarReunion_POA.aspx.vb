
Partial Class administrativo_propuestas2_proponente_ListaProgramarReunion_POA
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (IsPostBack = False) Then
            HD_Usuario.Value = Request.QueryString("id")
            Dim tipo As Integer = 0

            Dim ObjCnx As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
            Dim rsFac As New Data.DataTable

            '' Si es Secretario y pertenece a una facultad
            'Response.Write("PRP_ConsutarReunionesConsejo_POA" & "," & "FA" & "," & Request.QueryString("id") & "," & "" & "," & "")
            rsFac = ObjCnx.TraerDataTable("PRP_ConsutarReunionesConsejo_POA", "FA", Request.QueryString("id"), "", "")
            If rsFac.Rows.Count > 0 Then
                txtelegido.Value = rsFac.Rows(0).Item("codigo_fac")
                tipo = 1
            Else
                txtelegido.Value = HD_Usuario.Value
                tipo = 0
            End If

            Call wf_CargarListas(txtelegido.Value, tipo)
        End If
    End Sub

    Sub wf_CargarListas(ByVal facultad As Integer, ByVal tipo As Integer)
        Dim obj As New ClsConectarDatos
        Dim dtt As New Data.DataTable

        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()

        Me.dgvResolucion.DataSource = Nothing
        dtt = obj.TraerDataTable("PRP_ListaProgramacionReuniones", tipo, facultad)
        Me.dgvResolucion.DataSource = dtt
        Me.dgvResolucion.DataBind()

        dtt.Dispose()
        obj.CerrarConexion()
        obj = Nothing
    End Sub

    Protected Sub cmdConsultar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdConsultar.Click
        Dim tipo As String = "N"
        Response.Redirect("ProgramarReunion_POA.aspx?idUsu=" & HD_Usuario.Value & "&tipo=" & tipo)
    End Sub

    Protected Sub dgvResolucion_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgvResolucion.SelectedIndexChanged
        Dim id_rec As Integer
        Dim tipo As String = "M"

        id_rec = Me.dgvResolucion.SelectedDataKey.Value()

        Response.Redirect("ProgramarReunion_POA.aspx?idUsu=" & HD_Usuario.Value & "&id_rec=" & id_rec & "&tipo=" & tipo)
    End Sub
End Class
