Imports System.Collections.Generic
Imports System.Globalization

Partial Class PlanProyecto_frmCalendario
    Inherits System.Web.UI.Page

    <System.Web.Services.WebMethod(True)> _
    Public Shared Sub fn_ActualizaFecha(ByVal codigo_act As Integer, ByVal dias As Integer)
        Dim EventDao As New clsPlanCalendario
        EventDao.ActualizaDatos(codigo_act, dias)
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load        
        If (IsPostBack = False) Then
            Session("id_per") = Request.QueryString("id")
            Me.btnRefrescar.Attributes.Add("onclick", "location.reload();")
            CargaProyectos(0)
            If (Session("cod_pro") IsNot Nothing) Then
                Me.dpProyecto.SelectedValue = Session("cod_pro")
            Else
                Session("cod_pro") = 0
            End If
        End If
    End Sub

    Private Sub CargaProyectos(ByVal codigo_pro As Integer)
        Dim obj As New ClsConectarDatos
        Dim dtPlan As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        dtPlan = obj.TraerDataTable("PLAN_BuscaProyecto", codigo_pro, "", 0)
        obj.CerrarConexion()

        ClsFunciones.LlenarListas(Me.dpProyecto, dtPlan, "codigo_pro", "titulo_pro", "TODOS")
        dtPlan.Dispose()
        obj = Nothing
    End Sub

    Protected Sub dpProyecto_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dpProyecto.SelectedIndexChanged
        If (Me.dpProyecto.SelectedItem.Text = "TODOS") Then
            Session("cod_pro") = 0
        Else
            Session("cod_pro") = Me.dpProyecto.SelectedValue
        End If
    End Sub
End Class
