Imports System.Data


Partial Class Consultas_listadocentesNombre
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
            Dim objtitulo As New Personal
            Dim Tabla As New DataTable
            '  Tabla = objtitulo.DocentesNombres(Me.txtNombre.Text)
            '    Response.Write(Me.txtNombre.Text)
            'DocentesLista = Tabla
            '   Response.Write(Tabla.Rows.Count)

            '  Me.LblDepartamento.Text = Tabla.Rows(1).Item(0).ToString()
        Catch ex As Exception
            ' Me.LblDepartamento.Text = "No se encontro personal adscrito al area seleccionada."
        End Try
    End Sub
End Class
