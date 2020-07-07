
Partial Class BecaEstudio_frmBecaCalendarioActividades
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim objcnx As New ClsConectarDatos
            Dim objFun As New ClsFunciones
            objcnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            objcnx.AbrirConexion()
            objFun.CargarListas(Me.cboCicloAcademico, objcnx.TraerDataTable("ConsultarCicloAcademico", "TO", ""), "codigo_cac", "descripcion_cac")
            objcnx.CerrarConexion()
        End If
    End Sub

    Protected Sub gvCronograma_RowUpdating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewUpdateEventArgs) Handles gvCronograma.RowUpdating
        Dim objcnx As New ClsConectarDatos
        Dim fechaini, fechafin As Date
        Dim observacion As String
        fechaini = e.NewValues(0)
        fechafin = e.NewValues(1)
        observacion = ""
        observacion = IIf(e.NewValues(2) Is DBNull.Value Or e.NewValues(2) = "", "", e.NewValues(2))
        objcnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        objcnx.AbrirConexion()
        objcnx.Ejecutar("MAT_ActualizarCronograma", fechaini, fechafin, observacion, Me.gvCronograma.DataKeys.Item(e.RowIndex).Values(0).ToString, Page.Request.QueryString("id"))
        objcnx.CerrarConexion()
        e.Cancel = True
        Response.Redirect("frmBecaCalendarioActividades.aspx?id=" & Page.Request.QueryString("id") & "&ctf=" & Page.Request.QueryString("ctf"))
    End Sub
End Class
