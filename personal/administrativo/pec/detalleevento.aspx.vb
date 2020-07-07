Partial Class evento
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            Dim objfun As New ClsFunciones
            Dim obj As New ClsConectarDatos
            Dim tbl As Data.DataTable
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            
            tbl = obj.TraerDataTable("EVE_ConsultarEventos", 2, Me.Request.QueryString("cco"), 0)
            If (tbl.Rows.Count > 0) Then
                Me.Label1.Text = tbl.Rows(0).Item("nombre_dev")
                Me.Label2.Text = tbl.Rows(0).Item("nroresolucion_dev")
                Me.Label4.Text = tbl.Rows(0).Item("coordinador")
                Me.Label5.Text = tbl.Rows(0).Item("apoyo")
                Me.Label6.Text = tbl.Rows(0).Item("nroparticipantes_dev")
                Me.Label7.Text = tbl.Rows(0).Item("preciounitcontado_dev")
                Me.Label8.Text = tbl.Rows(0).Item("preciounitfinanciado_dev")
                Me.Label9.Text = tbl.Rows(0).Item("montocuotainicial_dev")
                Me.Label10.Text = tbl.Rows(0).Item("nrocuotas_dev")
                Me.Label11.Text = tbl.Rows(0).Item("porcentajedescpersonalusat_dev")
                Me.Label12.Text = tbl.Rows(0).Item("porcentajedescalumnousat_dev")
                Me.Label13.Text = tbl.Rows(0).Item("porcentajedesccorportativo_dev")
                Me.Label20.Text = tbl.Rows(0).Item("porcentajedescegresado_dev")
                Me.Label14.Text = IIf(tbl.Rows(0).Item("gestionanotas_dev") = True, "Sí", "No")
                Me.Label15.Text = tbl.Rows(0).Item("horarios_dev").ToString
                Me.Label16.Text = tbl.Rows(0).Item("obs_dev").ToString
                Me.Label17.Text = CDate(tbl.Rows(0).Item("fechainiciopropuesta_dev")).ToShortDateString
                Me.Label18.Text = CDate(tbl.Rows(0).Item("fechafinpropuesta_dev")).ToShortDateString
                Me.Label19.Text = tbl.Rows(0).Item("nombre_cpf").ToString
                
            End If

            obj.CerrarConexion()
            obj = Nothing
            objfun = Nothing
        End If
    End Sub
End Class