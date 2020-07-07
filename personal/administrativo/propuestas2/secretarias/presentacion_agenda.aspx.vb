
Partial Class secretarias_presentacion_intro
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim ObjCnx As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
        Dim rsReunion As New Data.DataTable
        rsReunion = ObjCnx.TraerDataTable("PRP_ConsutarReunionesConsejo", "CA", Request.QueryString("id_rec"), 0, 0)

        Me.lblReunion.Text = rsReunion.Rows(0).Item("agenda_Rec").ToString.ToUpper
        Me.lblFecha.Text = rsReunion.Rows(0).Item("fecha")
        Me.lblFacultad.Text = rsReunion.Rows(0).Item("facultad")

    End Sub
End Class
