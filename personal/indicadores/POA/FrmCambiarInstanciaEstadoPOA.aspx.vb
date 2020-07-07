
Partial Class indicadores_POA_FrmCambiarInstanciaEstadoPOA
    Inherits System.Web.UI.Page

    Sub CargaEjercicio()
        Dim obj As New clsPlanOperativoAnual
        Dim dt As New Data.DataTable
        dt = obj.ListaEjercicio
        Me.ddlEjercicio.DataSource = dt
        Me.ddlEjercicio.DataTextField = "descripcion"
        Me.ddlEjercicio.DataValueField = "codigo"
        Me.ddlEjercicio.DataBind()
        dt.Dispose()
        obj = Nothing
    End Sub

    Sub Carga_Peis()
        Dim obj As New clsPlanOperativoAnual
        Dim dtPEI As New Data.DataTable
        dtPEI = obj.ListaPeis
        Me.ddlPlan.DataSource = dtPEI
        Me.ddlPlan.DataTextField = "descripcion"
        Me.ddlPlan.DataValueField = "codigo"
        Me.ddlPlan.DataBind()
        dtPEI.Dispose()
        obj = Nothing
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (Session("id_per") Is Nothing) Then
            Response.Redirect("../../../sinacceso.html")
        End If
        Try
            If IsPostBack = False Then
                CargaEjercicio()
                Carga_Peis()

            End If
        Catch ex As Exception
            Response.Write(ex.Message.ToString)
        End Try

    End Sub

End Class
