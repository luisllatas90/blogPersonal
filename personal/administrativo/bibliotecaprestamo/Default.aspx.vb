Imports System.Data
Partial Class _Default
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (Session("id_per") Is Nothing) Then
            Response.Redirect("../../../sinacceso.html")
        End If

        If IsPostBack = False Then           
            Dim cDatos As New cDatos
            Dim dt As New DataTable
            dt = cDatos.ConsultaAmbienteActivos()
            For i As Integer = 0 To dt.Rows.Count - 1
                Me.DropDownList1.Items.Insert(i, New ListItem(dt.Rows(i).Item("nombre_ambiente"), dt.Rows(i).Item("ambiente_id")))
            Next
        End If
    End Sub
End Class
