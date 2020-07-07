Imports System.Data


Partial Class Consultas_listadocentes
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim objtitulo As New Personal
            Dim Tabla As New DataTable
            Tabla = objtitulo.DocentesDeparAcad(Me.DDLCentroCotos.SelectedValue)
            Me.LblDepartamento.Text = Tabla.Rows(1).Item(0).ToString()
        Catch ex As Exception
            Me.LblDepartamento.Text = "No se encontro personal adscrito al area seleccionada."
        End Try
        If IsPostBack = False Then
            Try
                Dim objCombos As New Combos
                objCombos.LlenaCentroCostos(Me.DDLCentroCotos, Request.QueryString("id"), "CP")
                objCombos = Nothing
            Catch ex As Exception

            End Try
        End If

    End Sub
End Class
