
Partial Class GestionDocumentaria_frmCronogramaSesiones
    Inherits System.Web.UI.Page
#Region "eventos"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (Session("id_per") Is Nothing) Then
            Response.Redirect("../../sinacceso.html")
        End If
        Try
            If IsPostBack = False Then
                mt_ListarSesiones()
            End If
        Catch ex As Exception
            Response.Write(ex.Message.ToString)
        End Try
    End Sub
    Protected Sub btnAgregar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAgregar.Click
        Try

            Me.Lista.Visible = False
            Me.DivAsesorias.Visible = True

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub
    Protected Sub btnAtras_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAtras.Click
        Me.Lista.Visible = True
        Me.DivAsesorias.Visible = False

    End Sub
#End Region
#Region "procedimientos y funciones"
    Private Sub mt_ListarSesiones()
        Dim dt As New Data.DataTable()
        dt.Columns.Add("id")
        dt.Columns.Add("Facultad")
        dt.Columns.Add("Fecha")
        dt.Columns.Add("Tipo")
        dt.Columns.Add("Motivo")

        Dim row As Data.DataRow = dt.NewRow()
        row("id") = "1"
        row("Facultad") = "CIENCIAS EMPRESARIALES"
        row("Fecha") = "01/08/2020 09:00 AM"
        row("Tipo") = "ORDINARIA"
        row("Motivo") = "APROBAR GRADOS"
        dt.Rows.Add(row)

        Dim row1 As Data.DataRow = dt.NewRow()
        row1("id") = "2"
        row1("Facultad") = "CIENCIAS EMPRESARIALES"
        row1("Fecha") = "01/09/2020 10:00 AM"
        row1("Tipo") = "ORDINARIA"
        row1("Motivo") = "APROBAR TÍTULOS"
        dt.Rows.Add(row1)

        Dim row2 As Data.DataRow = dt.NewRow()
        row2("id") = "3"
        row2("Facultad") = "CIENCIAS EMPRESARIALES"
        row2("Fecha") = "01/10/2020 03:00 PM"
        row2("Tipo") = "EXTRAORDINARIA"
        row2("Motivo") = "APROBAR GRADOS"
        dt.Rows.Add(row2)

        If dt.Rows.Count > 0 Then
            Me.gvListaSesiones.DataSource = dt
            Me.gvListaSesiones.DataBind()
        Else
            Me.gvListaSesiones.DataSource = Nothing
            Me.gvListaSesiones.DataBind()
        End If

    End Sub
#End Region
    
    
  
End Class
