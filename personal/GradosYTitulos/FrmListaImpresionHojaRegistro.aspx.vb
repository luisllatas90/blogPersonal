
Partial Class GradosYTitulos_FrmListaImpresionHojaRegistro
    Inherits System.Web.UI.Page

    Protected Sub btnBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBuscar.Click

        If Me.txtBusqueda.Text.Length > 2 Then
            Dim obj As New ClsGradosyTitulos
            Dim dt As New Data.DataTable
            dt = obj.ConsultarEgresadoHojaRegistro("5", Me.txtBusqueda.Text, "0", "0")
            Me.gvAlumnos.DataSource = dt
            Me.gvAlumnos.DataBind()
        Else
            Me.divMensaje.InnerHtml = "para Realizar Busqueda necesita Ingresar por lo menos 3 caracteres."
            Me.divMensaje.Attributes.Add("Class", "alert alert-danger")
            ScriptManager.GetCurrent(Me.Page).SetFocus(Me.txtBusqueda)
        End If
    End Sub



    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            ScriptManager.GetCurrent(Me.Page).SetFocus(Me.txtBusqueda)
        End If
    End Sub
End Class
