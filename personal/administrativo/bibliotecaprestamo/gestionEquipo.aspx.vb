Imports System.Data
Partial Class gestionEquipo
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim cDatos As New cDatos
        Dim dt As New DataTable
        dt = cDatos.ConsultaAmbienteActivos()
        For i As Integer = 0 To dt.Rows.Count - 1
            Me.DropDownList1.Items.Insert(i, New ListItem(dt.Rows(i).Item("nombre_ambiente"), dt.Rows(i).Item("ambiente_id")))
        Next
    End Sub
End Class
