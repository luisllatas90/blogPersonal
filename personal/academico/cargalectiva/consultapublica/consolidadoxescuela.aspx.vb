﻿
Partial Class academico_cargalectiva_consultapublica_consolidadoxescuela
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            Dim objFun As New ClsFunciones
            objFun.CargarListas(Me.ddlEscuela, obj.TraerDataTable("EVE_ConsultarCarreraProfesional", "2", 1, 684), "codigo_cpf", "nombre_cpf")
            objFun.CargarListas(Me.ddlCiclo, obj.TraerDataTable("ConsultarCicloAcademico", "TO", ""), "codigo_cac", "descripcion_cac")
            Me.ddlEscuela.SelectedValue = Session("codigo_cac")
            obj.CerrarConexion()
            obj = Nothing
            CargaPlan()
        End If

    End Sub
    Sub CargaPlan()
        Try
            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            Dim objFun As New ClsFunciones
            objFun.CargarListas(Me.ddlPlan, obj.TraerDataTable("ConsultarPlanEstudio", "AC", Me.ddlEscuela.SelectedValue.ToString, "2"), "codigo_pes", "descripcion_pes")
            obj.CerrarConexion()
            obj = Nothing
        Catch ex As Exception
            Response.Write(ex.Message)

        End Try
    End Sub
    Protected Sub ddlEscuela_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlEscuela.SelectedIndexChanged
        CargaPlan()

    End Sub

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim obj As New ClsConectarDatos
        Dim tb As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        tb = obj.TraerDataTable("acad_consolidadoxescuela_padres", "P", Me.ddlCiclo.selectedvalue, Me.ddlPlan.SelectedValue)
        Me.gData.DataSource = tb
        Me.gData.DataBind()
        obj.CerrarConexion()
        obj = Nothing
    End Sub



    Protected Sub gData_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gData.RowCommand
        Dim index As Integer = Convert.ToInt32(e.CommandArgument)
        If (e.CommandName = "MostrarDetalle") Then
            Dim obj As New ClsConectarDatos
            Dim tb As New Data.DataTable
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            tb = obj.TraerDataTable("acad_consolidadoxescuela_padres", "H", gData.DataKeys(index).Values("codigo_cup"), 0)
            Me.gDataDetalle.DataSource = tb
            Me.gDataDetalle.DataBind()
            obj.CerrarConexion()
            obj = Nothing
        End If
    End Sub
End Class
