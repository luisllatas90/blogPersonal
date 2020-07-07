
Partial Class indicadores_frmVistaEvaluacionAnual
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                CargarAñosRegistrados()
                Listar()
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub


    Private Sub CargarAñosRegistrados()
        Try
            Dim dts As New Data.DataTable
            Dim obj As New clsIndicadores

            dts = obj.CargarAñosRegistrados()
            If dts.Rows.Count > 0 Then
                Me.ddlAnio.DataSource = dts
                Me.ddlAnio.DataTextField = "descripcion"
                Me.ddlAnio.DataValueField = "codigo"
                Me.ddlAnio.DataBind()
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub Listar()
        Try
            Dim obj As New clsIndicadores
            Dim dts As New Data.DataTable

            dts = obj.ListaInformesEvaluacionAnual(Me.Request.QueryString("id"), Me.Request.QueryString("ctf"), Me.ddlAnio.SelectedValue)
            If dts.Rows.Count > 0 Then
                gvLista.DataSource = dts
                gvLista.DataBind()
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    'Agregado el 13.01.2014 dguevara
    Protected Sub ddlAnio_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlAnio.SelectedIndexChanged
        Try
            Listar()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub
End Class
