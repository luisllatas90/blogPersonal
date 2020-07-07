﻿Imports System.Data
Partial Class FrmConfigurarCategoriaProgPresupuestal
    Inherits System.Web.UI.Page

    Private Sub ListarCategoria()
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Dim tb As New Data.DataTable
        tb = obj.TraerDataTable("POA_CategoriaProgProyActividad", "0", "0", "0", "0", "CPP")

        If tb.Rows.Count > 0 Then
            For i As Integer = 0 To tb.Rows.Count - 1
                Me.ddlCategoria.Items.Add(New ListItem(tb.Rows(i).Item("descripcion"), tb.Rows(i).Item("codigo")))
            Next
        End If
        obj.CerrarConexion()
    End Sub

    Private Sub ListarProgramaPresupuestal()
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Dim tb As New Data.DataTable
        tb = obj.TraerDataTable("PRESU_ConsultarProgramapresupuestal", "3", "0")

        If tb.Rows.Count > 0 Then
            For i As Integer = 0 To tb.Rows.Count - 1
                Me.ddlProgramaPresupuestal.Items.Add(New ListItem(tb.Rows(i).Item("descripcion"), tb.Rows(i).Item("codigo")))
            Next
        End If
        obj.CerrarConexion()
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (Session("id_per") Is Nothing) Then
            Response.Redirect("../../../sinacceso.html")
        End If
        If IsPostBack = False Then

            ListarCategoria()
            ListarProgramaPresupuestal()
            ConsultarConfiguracion()
            'ListarUniversidades()
        Else
            'mt_RefreshGrid()
        End If
    End Sub

    'Protected Sub ddlTipo_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlTipo.SelectedIndexChanged
    '    Me.ddlDepartamentoArea.Items.Clear()
    '    If Me.ddlTipo.SelectedValue = "1" Then  ' SI ES DOCENTE
    '        Me.lblDepartamentoArea.Text = "Departamento académico"
    '        ConsultarDepartamentoAcademico()
    '    ElseIf Me.ddlTipo.SelectedValue = "2" Then ' SI ES ADMINISTRATIVO
    '        Me.lblDepartamentoArea.Text = "Área"
    '        ConsultarAreas()
    '    Else
    '        Me.lblDepartamentoArea.Text = "Área/Departamento"
    '        Me.ddlDepartamentoArea.Items.Add(New ListItem("[-- SELECCIONE --]", ""))
    '    End If

    '    Me.ddlPersonal.Items.Clear()
    '    Me.ddlPersonal.Items.Add(New ListItem("[-- SELECCIONE --]", ""))
    '    Me.chkEducacion.Visible = False
    '    Me.btnNuevo.Visible = False
    '    Me.gvGradosTitulosPersonal.DataSource = ""
    '    Me.gvGradosTitulosPersonal.DataBind()

    '    Me.ddlTipoPrograma.Items.Add(New ListItem("<<Seleccione>>", ""))

    '    Me.ddlPrograma.Items.Add(New ListItem("<<Seleccione>>", "0"))

    '    Me.ddlSituacion.Items.Clear()
    '    Me.ddlSituacion.Items.Add(New ListItem("<<Seleccione>>", ""))
    '    Me.ddlSituacion.Items.Add(New ListItem("Culminado", "1"))
    '    Me.ddlSituacion.Items.Add(New ListItem("En Proceso", "2"))

    'End Sub

    'Private Sub ConsultarAreas()
    '    Dim obj As New ClsConectarDatos
    '    obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
    '    obj.AbrirConexion()
    '    Dim tb As New Data.DataTable
    '    tb = obj.TraerDataTable("PER_ListarAreasPersonalAdministrativo")
    '    Me.ddlDepartamentoArea.Items.Add(New ListItem("[-- SELECCIONE --]", ""))
    '    If tb.Rows.Count > 0 Then
    '        For i As Integer = 0 To tb.Rows.Count - 1
    '            Me.ddlDepartamentoArea.Items.Add(New ListItem(tb.Rows(i).Item("descripcion_cco"), tb.Rows(i).Item("codigo_Cco")))
    '        Next
    '    End If
    '    obj.CerrarConexion()
    'End Sub

    'Private Sub ConsultarDepartamentoAcademico()
    '    Dim obj As New ClsConectarDatos
    '    obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
    '    obj.AbrirConexion()
    '    Dim tb As New Data.DataTable
    '    tb = obj.TraerDataTable("TES_ConsultarDepartamentoAcademico", "1", 0, 0)
    '    Me.ddlDepartamentoArea.Items.Add(New ListItem("[-- SELECCIONE --]", ""))
    '    If tb.Rows.Count > 0 Then
    '        For i As Integer = 0 To tb.Rows.Count - 1
    '            Me.ddlDepartamentoArea.Items.Add(New ListItem(tb.Rows(i).Item("descripcion"), tb.Rows(i).Item("codigo")))
    '        Next
    '    End If
    '    obj.CerrarConexion()
    'End Sub


    Private Sub ConsultarConfiguracion()
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Dim tb As New Data.DataTable
        tb = obj.TraerDataTable("POA_ListarCategoriaProgPresupuestal", "%")
        If tb.Rows.Count > 0 Then
            Me.gvConfiguracion.DataSource = tb
        Else
            Me.gvConfiguracion.DataSource = ""
        End If
        Me.gvConfiguracion.DataBind()
        obj.CerrarConexion()
    End Sub

    'Private Sub ConsultarFormacionAcademicaDocente(ByVal codigo_per As Integer)
    '    Dim obj As New ClsConectarDatos
    '    obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
    '    obj.AbrirConexion()
    '    Dim tb As New Data.DataTable
    '    tb = obj.TraerDataTable("ListarGradosTitulosFADVincular", codigo_per)
    '    If tb.Rows.Count > 0 Then
    '        Me.gvFormacionAcademica.DataSource = tb
    '    Else
    '        Me.gvFormacionAcademica.DataSource = ""
    '    End If
    '    Me.gvFormacionAcademica.DataBind()
    '    obj.CerrarConexion()
    'End Sub

    'Protected Sub ddlDepartamentoArea_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlDepartamentoArea.SelectedIndexChanged
    '    Me.ddlPersonal.Items.Clear()
    '    Me.chkEducacion.Visible = False
    '    Me.btnNuevo.Visible = False
    '    Me.gvGradosTitulosPersonal.DataSource = ""
    '    Me.gvGradosTitulosPersonal.DataBind()

    '    If Me.ddlDepartamentoArea.SelectedValue <> "" Then
    '        If Me.ddlTipo.SelectedValue = "1" Then  ' SI ES DOCENTE
    '            ConsultarDocentesAdscriptos(Me.ddlDepartamentoArea.SelectedValue)
    '            Me.ddlDepartamentoArea.Visible = True
    '        ElseIf Me.ddlTipo.SelectedValue = "2" Then ' SI ES ADMINISTRATIVO
    '            ConsultarPersonalxAreaCeco(Me.ddlDepartamentoArea.SelectedValue)
    '        End If
    '    Else
    '        Me.ddlPersonal.Items.Add(New ListItem("[-- SELECCIONE --]", ""))
    '    End If

    'End Sub

    Private Function GuardarConfiguracion() As Data.DataTable
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Dim tb As New Data.DataTable
        tb = obj.TraerDataTable("POA_AgregarCategoriaProgPresupuestal", Me.ddlCategoria.SelectedValue, Me.ddlProgramaPresupuestal.SelectedValue, Session("id_per"))
        obj.CerrarConexion()
        Return tb
    End Function

    Protected Sub btnGuardarRegistro_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardarRegistro.Click
        If (Session("id_per") Is Nothing) Then
            Response.Redirect("../../../sinacceso.html")
        End If
        If Me.ddlCategoria.SelectedValue <> "0" Then
            If Me.ddlProgramaPresupuestal.SelectedValue <> "0" Then
                Dim dt As New Data.DataTable
                dt = GuardarConfiguracion()
                If dt.Rows("0").Item("respuesta") = "1" Then
                    ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "Nuevo", "fnMensaje('success','" + dt.Rows("0").Item("mensaje").ToString + "')", True)
                    ConsultarConfiguracion()
                    Me.ddlProgramaPresupuestal.SelectedValue = 0
                    Me.ddlCategoria.SelectedValue = 0
                Else
                    ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "Nuevo", "fnMensaje('error','" + dt.Rows("0").Item("mensaje").ToString + "')", True)
                End If
            Else
                ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "Nuevo", "fnMensaje('error','Seleccione un Programa Presupuestal')", True)
            End If
        Else
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "Nuevo", "fnMensaje('error','Seleccione una categoría')", True)
        End If
    End Sub

    Private Function EliminarConfiguracion(ByVal codigo_cpp As Integer) As Data.DataTable
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Dim tb As New Data.DataTable
        tb = obj.TraerDataTable("POA_EliminarCategoriaProgPresupuestal", codigo_cpp, Session("id_per"))
        obj.CerrarConexion()
        Return tb
    End Function

    Protected Sub gvConfiguracion_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvConfiguracion.RowCommand
        If (Session("id_per") Is Nothing) Then
            Response.Redirect("../../../sinacceso.html")
        End If
        If e.CommandName = "Eliminar" Then
            Dim obj As New ClsGestionInvestigacion
            Dim dt As New Data.DataTable
            dt = EliminarConfiguracion(Me.gvConfiguracion.DataKeys(e.CommandArgument).Values("codigo_cpp"))
            If dt.Rows(0).Item("Respuesta") = 1 Then
                Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "fnMensaje('success','" + dt.Rows(0).Item("mensaje").ToString + "')", True)
                ConsultarConfiguracion()
            Else
                Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "fnMensaje('error','" + dt.Rows(0).Item("mensaje").ToString + "')", True)
            End If
        End If
    End Sub
End Class

