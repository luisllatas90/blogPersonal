﻿Imports System.Data
Partial Class FrmRegistroGradosTitulosPersonal
    Inherits System.Web.UI.Page

    Private Sub ListarTipoInstitucion()
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Dim tb As New Data.DataTable
        tb = obj.TraerDataTable("ListarTipoInstitucion", "TO")
        Me.ddlTipoInstitucion.Items.Add(New ListItem("<<Seleccione>>", ""))
        If tb.Rows.Count > 0 Then
            For i As Integer = 0 To tb.Rows.Count - 1
                Me.ddlTipoInstitucion.Items.Add(New ListItem(tb.Rows(i).Item("descripcion_tis"), tb.Rows(i).Item("codigo_tis")))
            Next
        End If
        obj.CerrarConexion()
    End Sub

    Private Sub ListarModalidadEstudio()
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Dim tb As New Data.DataTable
        tb = obj.TraerDataTable("HV_ConsultarModalidades", "TO")
        Me.ddlModalidad.Items.Add(New ListItem("<<Seleccione>>", ""))
        If tb.Rows.Count > 0 Then
            For i As Integer = 0 To tb.Rows.Count - 1
                Me.ddlModalidad.Items.Add(New ListItem(tb.Rows(i).Item("descripcion_mod"), tb.Rows(i).Item("codigo_mod")))
            Next
        End If
        obj.CerrarConexion()
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (Session("id_per") Is Nothing) Then
            Response.Redirect("../../../sinacceso.html")
        End If
        If IsPostBack = False Then
            Me.chkEducacion.Visible = False
            Me.btnNuevo.Visible = False
            ListarTipoInstitucion()
            ListarModalidadEstudio()
            Me.ddlUniversidad.Items.Add(New ListItem("<<Seleccione>>", "0"))
            'ListarUniversidades()
        Else
            mt_RefreshGrid()
        End If
    End Sub

    Protected Sub ddlTipo_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlTipo.SelectedIndexChanged
        Me.ddlDepartamentoArea.Items.Clear()
        If Me.ddlTipo.SelectedValue = "1" Then  ' SI ES DOCENTE
            Me.lblDepartamentoArea.Text = "Departamento académico"
            ConsultarDepartamentoAcademico()
        ElseIf Me.ddlTipo.SelectedValue = "2" Then ' SI ES ADMINISTRATIVO
            Me.lblDepartamentoArea.Text = "Área"
            ConsultarAreas()
        Else
            Me.lblDepartamentoArea.Text = "Área/Departamento"
            Me.ddlDepartamentoArea.Items.Add(New ListItem("[-- SELECCIONE --]", ""))
        End If

        Me.ddlPersonal.Items.Clear()
        Me.ddlPersonal.Items.Add(New ListItem("[-- SELECCIONE --]", ""))
        Me.chkEducacion.Visible = False
        Me.btnNuevo.Visible = False
        Me.gvGradosTitulosPersonal.DataSource = ""
        Me.gvGradosTitulosPersonal.DataBind()

        Me.ddlTipoPrograma.Items.Add(New ListItem("<<Seleccione>>", ""))

        Me.ddlPrograma.Items.Add(New ListItem("<<Seleccione>>", "0"))

        Me.ddlSituacion.Items.Clear()
        Me.ddlSituacion.Items.Add(New ListItem("<<Seleccione>>", ""))
        Me.ddlSituacion.Items.Add(New ListItem("Culminado", "1"))
        Me.ddlSituacion.Items.Add(New ListItem("En Proceso", "2"))

    End Sub

    Private Sub ConsultarAreas()
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Dim tb As New Data.DataTable
        tb = obj.TraerDataTable("PER_ListarAreasPersonalAdministrativo")
        Me.ddlDepartamentoArea.Items.Add(New ListItem("[-- SELECCIONE --]", ""))
        If tb.Rows.Count > 0 Then
            For i As Integer = 0 To tb.Rows.Count - 1
                Me.ddlDepartamentoArea.Items.Add(New ListItem(tb.Rows(i).Item("descripcion_cco"), tb.Rows(i).Item("codigo_Cco")))
            Next
        End If
        obj.CerrarConexion()
    End Sub

    Private Sub ConsultarDepartamentoAcademico()
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Dim tb As New Data.DataTable
        tb = obj.TraerDataTable("TES_ConsultarDepartamentoAcademico", "1", 0, 0)
        Me.ddlDepartamentoArea.Items.Add(New ListItem("[-- SELECCIONE --]", ""))
        If tb.Rows.Count > 0 Then
            For i As Integer = 0 To tb.Rows.Count - 1
                Me.ddlDepartamentoArea.Items.Add(New ListItem(tb.Rows(i).Item("descripcion"), tb.Rows(i).Item("codigo")))
            Next
        End If
        obj.CerrarConexion()
    End Sub

    Private Sub ConsultarDocentesAdscriptos(ByVal codigo_dac As Integer)
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Dim tb As New Data.DataTable
        tb = obj.TraerDataTable("PER_ListarPersonalAdscritoxDepartamento", codigo_dac, "%")
        Me.ddlPersonal.Items.Add(New ListItem("[-- SELECCIONE --]", ""))
        If tb.Rows.Count > 0 Then
            For i As Integer = 0 To tb.Rows.Count - 1
                Me.ddlPersonal.Items.Add(New ListItem(tb.Rows(i).Item("nombre"), tb.Rows(i).Item("codigo")))
            Next
        End If
        obj.CerrarConexion()
    End Sub

    Private Sub ConsultarPersonalxAreaCeco(ByVal codigo_cco As Integer)
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Dim tb As New Data.DataTable
        tb = obj.TraerDataTable("PER_ListarPersonalxAreaCco", codigo_cco)
        Me.ddlPersonal.Items.Add(New ListItem("[-- SELECCIONE --]", ""))
        If tb.Rows.Count > 0 Then
            For i As Integer = 0 To tb.Rows.Count - 1
                Me.ddlPersonal.Items.Add(New ListItem(tb.Rows(i).Item("nombre"), tb.Rows(i).Item("codigo")))
            Next
        End If
        obj.CerrarConexion()
    End Sub


    Private Sub ConsultarGradosTitulosPersonal(ByVal codigo_per As Integer)
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Dim tb As New Data.DataTable
        tb = obj.TraerDataTable("PER_ListarGradosTitulosPersonal", codigo_per)
        If tb.Rows.Count > 0 Then
            Me.gvGradosTitulosPersonal.DataSource = tb
        Else
            Me.gvGradosTitulosPersonal.DataSource = ""
        End If
        Me.gvGradosTitulosPersonal.DataBind()

        tb = obj.TraerDataTable("ConsultarCheckEducacionEnPeru", codigo_per)

        If tb.Rows.Count > 0 Then
            If tb.Rows(0).Item("EducacionEnPeru_per").ToString = "1" Then
                Me.chkEducacion.Checked = True
            Else
                Me.chkEducacion.Checked = False
            End If
        End If

        obj.CerrarConexion()
    End Sub

    Private Sub ConsultarFormacionAcademicaDocente(ByVal codigo_per As Integer)
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Dim tb As New Data.DataTable
        tb = obj.TraerDataTable("ListarGradosTitulosFADVincular", codigo_per)
        If tb.Rows.Count > 0 Then
            Me.gvFormacionAcademica.DataSource = tb
        Else
            Me.gvFormacionAcademica.DataSource = ""
        End If
        Me.gvFormacionAcademica.DataBind()
        obj.CerrarConexion()
    End Sub

    Protected Sub ddlDepartamentoArea_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlDepartamentoArea.SelectedIndexChanged
        Me.ddlPersonal.Items.Clear()
        Me.chkEducacion.Visible = False
        Me.btnNuevo.Visible = False
        Me.gvGradosTitulosPersonal.DataSource = ""
        Me.gvGradosTitulosPersonal.DataBind()

        If Me.ddlDepartamentoArea.SelectedValue <> "" Then
            If Me.ddlTipo.SelectedValue = "1" Then  ' SI ES DOCENTE
                ConsultarDocentesAdscriptos(Me.ddlDepartamentoArea.SelectedValue)
                Me.ddlDepartamentoArea.Visible = True
            ElseIf Me.ddlTipo.SelectedValue = "2" Then ' SI ES ADMINISTRATIVO
                ConsultarPersonalxAreaCeco(Me.ddlDepartamentoArea.SelectedValue)
            End If
        Else
            Me.ddlPersonal.Items.Add(New ListItem("[-- SELECCIONE --]", ""))
        End If

    End Sub

    Protected Sub ddlPersonal_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPersonal.SelectedIndexChanged
        If Me.ddlPersonal.SelectedValue <> "" Then
            Me.chkEducacion.Visible = True
            Me.btnNuevo.Visible = True
            ConsultarGradosTitulosPersonal(Me.ddlPersonal.SelectedValue)
        Else
            Me.chkEducacion.Visible = False
            Me.btnNuevo.Visible = False
            Me.gvGradosTitulosPersonal.DataSource = ""
            Me.gvGradosTitulosPersonal.DataBind()
        End If
    End Sub

    Protected Sub gvGradosTitulosPersonal_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvGradosTitulosPersonal.RowCommand
        If e.CommandName = "Validar" Then
            Dim boton As LinkButton = DirectCast(Me.gvGradosTitulosPersonal.Rows(e.CommandArgument).Cells(6).FindControl("btnValidar"), LinkButton)
            If boton.ToolTip = "Validar" Then
                validar(Me.gvGradosTitulosPersonal.DataKeys(e.CommandArgument).Values("codigo"), Me.gvGradosTitulosPersonal.DataKeys(e.CommandArgument).Values("tipoGradoTitulo"), 1)
            Else
                validar(Me.gvGradosTitulosPersonal.DataKeys(e.CommandArgument).Values("codigo"), Me.gvGradosTitulosPersonal.DataKeys(e.CommandArgument).Values("tipoGradoTitulo"), 0)
            End If
            ConsultarGradosTitulosPersonal(Me.ddlPersonal.SelectedValue)
        End If

        If e.CommandName = "Editar" Then
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MostrarmodalVIncular", "MostrarModal('mdEditar');Calendario();initCombo('ddlUniversidad');", True)
            Limpiar()
            Me.hdc.Value = Me.gvGradosTitulosPersonal.DataKeys(e.CommandArgument).Values("codigo")
            Me.hdt.Value = Me.gvGradosTitulosPersonal.DataKeys(e.CommandArgument).Values("tipoGradoTitulo")
            Me.hdfa.Value = Me.gvGradosTitulosPersonal.DataKeys(e.CommandArgument).Values("codigogra_fad")
            mt_RefreshGrid()
            CargarDatos(Me.hdc.Value, Me.hdt.Value)
        End If

        If e.CommandName = "Vincular" Then
            Dim boton As LinkButton = DirectCast(Me.gvGradosTitulosPersonal.Rows(e.CommandArgument).Cells(7).FindControl("btnVincular"), LinkButton)
            If boton.ToolTip = "Vincular" Then
                ConsultarFormacionAcademicaDocente(Me.gvGradosTitulosPersonal.DataKeys(e.CommandArgument).Values("codigo_per"))
                ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MostrarmodalVIncular", "MostrarModal('mdVincular')", True)
                Me.hdc.Value = Me.gvGradosTitulosPersonal.DataKeys(e.CommandArgument).Values("codigo")
                Me.hdt.Value = Me.gvGradosTitulosPersonal.DataKeys(e.CommandArgument).Values("tipoGradoTitulo")
                Me.hdfa.Value = Me.gvGradosTitulosPersonal.DataKeys(e.CommandArgument).Values("codigogra_fad")
            Else
                DesvincularGradoTitulo(Me.gvGradosTitulosPersonal.DataKeys(e.CommandArgument).Values("codigo"), Me.gvGradosTitulosPersonal.DataKeys(e.CommandArgument).Values("tipoGradoTitulo"))
                ConsultarGradosTitulosPersonal(Me.ddlPersonal.SelectedValue)
            End If

            mt_RefreshGrid()
            mt_RefreshGrid2()
        End If

        If e.CommandName = "Eliminar" Then
            Dim dt As New Data.DataTable
            EliminarRegistro(Me.gvGradosTitulosPersonal.DataKeys(e.CommandArgument).Values("codigo"), Me.gvGradosTitulosPersonal.DataKeys(e.CommandArgument).Values("tipoGradoTitulo"), Me.gvGradosTitulosPersonal.DataKeys(e.CommandArgument).Values("codigogra_fad"))
            ConsultarGradosTitulosPersonal(Me.ddlPersonal.SelectedValue)
            mt_RefreshGrid()
        End If
    End Sub

    Private Sub CargarDatos(ByVal codigo As Integer, ByVal tipo As String)
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Dim tb As New Data.DataTable
        tb = obj.TraerDataTable("PER_DatosGradosTitulosPersonal", codigo, tipo)
        If tb.Rows.Count > 0 Then
            Me.ddlTipoInstitucion.SelectedValue = tb.Rows(0).Item("codigo_tins")
            Me.ddlModalidad.SelectedValue = tb.Rows(0).Item("codigo_mod").ToString
            CambioTipoInstitucion()
            Me.ddlTipoPrograma.SelectedValue = tb.Rows(0).Item("tipoprograma")
            CambiarTipoPrograma()
            If tb.Rows(0).Item("tipoprogramapregrado") <> "" Then
                Me.ddlTipoProgramaPregrado.SelectedValue = tb.Rows(0).Item("tipoprogramapregrado")
            End If
            Me.ddlPrograma.SelectedValue = tb.Rows(0).Item("codigo_pro")

            If tb.Rows(0).Item("Nombreprograma") <> "" Then
                Me.divProgramaEx.Visible = True
                Me.txtNombreProgramaEx.Text = tb.Rows(0).Item("Nombreprograma").ToString
            Else
                Me.divProgramaEx.Visible = False
            End If

            If tb.Rows(0).Item("Nombreinstitucion") <> "" Then
                Me.divUniversidadEx.Visible = True
                Me.txtNombreUniversidadEx.Text = tb.Rows(0).Item("Nombreinstitucion").ToString
            Else
                Me.divUniversidadEx.Visible = False
            End If

            Me.ddlUniversidad.SelectedValue = tb.Rows(0).Item("codigo_uni")
            Me.ddlSituacion.SelectedValue = tb.Rows(0).Item("codigo_Sit")
            cambiarSituacion()
            Me.txtAnioIngreso.Text = tb.Rows(0).Item("anioingreso").ToString
            Me.txtAnioEgreso.Text = tb.Rows(0).Item("anioegreso").ToString
            Me.txtFechaEmision.Text = tb.Rows(0).Item("fechaemision")


        End If
        obj.CerrarConexion()
    End Sub

    Private Sub EliminarRegistro(ByVal codigo As Integer, ByVal tipo As String, ByVal codigo_fad As Integer)
        Try
            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            Dim tb As New Data.DataTable
            tb = obj.TraerDataTable("EliminarGradoTituloPersonal", codigo, tipo, codigo_fad)
            If tb.Rows.Count > 0 Then
                ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "confirmaciónVinculo", "alert('" + tb.Rows(0).Item("msje").ToString + "')", True)
            End If
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "confirmaciónVinculo", "alert('No se pudo eliminar registro')", True)

        End Try
 
    End Sub


    Protected Sub gvFormacionAcademica_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvFormacionAcademica.RowCommand
        If e.CommandName = "Seleccionar" Then
            'ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MostrarmodalVIncular2", "alert('" + Me.gvFormacionAcademica.DataKeys(e.CommandArgument).Values("codigo_gra").ToString + "')", True)

            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            obj.Ejecutar("PER_VincularGradosTitulosPersonal", Me.hdt.Value, Me.hdc.Value, Me.gvFormacionAcademica.DataKeys(e.CommandArgument).Values("codigo_gra"))
            obj.CerrarConexion()
            ConsultarGradosTitulosPersonal(Me.ddlPersonal.SelectedValue)
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "confirmaciónVinculo", "alert('Vinculo realizado correctamente')", True)

        End If
    End Sub


    Protected Sub ddlTipoPrograma_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlTipoPrograma.SelectedIndexChanged
        CambiarTipoPrograma()
        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "Nuevo", "initCombo('ddlUniversidad');", True)
    End Sub

    Private Sub CambiarTipoPrograma()
        Me.divProgramaEx.Visible = False
        Me.TipoProgramaPregrado.Visible = False
        Me.ddlTipoProgramaPregrado.SelectedValue = ""
        If Me.ddlTipoPrograma.SelectedValue <> "" Then
            ListarProgramas(Me.ddlTipoPrograma.SelectedValue)
            If Me.ddlTipoPrograma.SelectedValue = 2 Then
                TipoProgramaPregrado.Visible = True
            Else
                TipoProgramaPregrado.Visible = False
            End If
        Else
            Me.ddlPrograma.Items.Clear()
            Me.ddlPrograma.Items.Add(New ListItem("<<Seleccione>>", "0"))
        End If
    End Sub

    Private Sub DesvincularGradoTitulo(ByVal codigo As Integer, ByVal tipo As String)
        Me.ddlPrograma.Items.Clear()
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Dim tb As New Data.DataTable
        tb = obj.TraerDataTable("QuitarVinculoGradosTitulosPersonal", codigo, tipo)
        obj.CerrarConexion()
        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "GuardarDatos", "alert('" + tb.Rows(0).Item("msje").ToString + "')", True)
    End Sub

    Private Sub ListarProgramas(ByVal codigo_test As Integer)
        Me.ddlPrograma.Items.Clear()
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Dim tb As New Data.DataTable
        If Me.ddlTipoInstitucion.SelectedItem.Text = "INSTITUTO" Then
            tb = obj.TraerDataTable("FAD_ConsultarProgramas", 0)
        Else
            tb = obj.TraerDataTable("FAD_ConsultarProgramas", codigo_test)
        End If
        If tb.Rows.Count > 0 Then
            For i As Integer = 0 To tb.Rows.Count - 1
                Me.ddlPrograma.Items.Add(New ListItem(tb.Rows(i).Item("nombre_pro"), tb.Rows(i).Item("codigo_pro")))
            Next
        End If
        obj.CerrarConexion()
    End Sub

    Private Sub ListarUniversidades()
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Dim tb As New Data.DataTable
        Me.ddlUniversidad.Items.Clear()
        tb = obj.TraerDataTable("FAD_ConsultarUniversidad")
        If tb.Rows.Count > 0 Then
            For i As Integer = 0 To tb.Rows.Count - 1
                If Me.ddlTipoInstitucion.SelectedItem.Text = "INSTITUTO" Then
                    If tb.Rows(i).Item("nombre_uni") = "000 - Instituto" Or i = 0 Then
                        Me.ddlUniversidad.Items.Add(New ListItem(tb.Rows(i).Item("nombre_uni"), tb.Rows(i).Item("codigo_uni")))
                    End If
                Else
                    If tb.Rows(i).Item("nombre_uni") <> "000 - Instituto" Then
                        Me.ddlUniversidad.Items.Add(New ListItem(tb.Rows(i).Item("nombre_uni"), tb.Rows(i).Item("codigo_uni")))
                    End If
                End If

            Next
        End If
        obj.CerrarConexion()
    End Sub

    Protected Sub ddlPrograma_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPrograma.SelectedIndexChanged
        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "Nuevo", "initCombo('ddlUniversidad');", True)
        Dim lblPrograma As String = Me.ddlPrograma.SelectedItem.Text.ToLower
        If lblPrograma = "programas del extranjero - 999999" Or lblPrograma = "programas de instituto - 000000" Or lblPrograma = "otros programas de segunda especialidad - 000000" Then
            Me.txtNombreProgramaEx.Text = ""
            Me.divProgramaEx.Visible = True
        Else
            Me.divProgramaEx.Visible = False
        End If
    End Sub

    Protected Sub ddlUniversidad_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlUniversidad.SelectedIndexChanged
        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "Nuevo", "initCombo('ddlUniversidad');", True)
        If Me.ddlUniversidad.SelectedValue = 144 Or Me.ddlUniversidad.SelectedItem.Text = "000 - Instituto" Then
            Me.txtNombreUniversidadEx.Text = ""
            Me.divUniversidadEx.Visible = True
        Else
            Me.divUniversidadEx.Visible = False
        End If
    End Sub


    Protected Sub btnNuevo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNuevo.Click
        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "Nuevo", "initCombo('ddlUniversidad');MostrarModal('mdEditar');Calendario();", True)
        Limpiar()
    End Sub

    Private Sub Limpiar()
        Me.hdc.Value = "0"
        Me.hdt.Value = "0"
        Me.hdfa.Value = "0"
        Me.ddlTipoInstitucion.SelectedValue = ""
        Me.ddlModalidad.SelectedValue = ""

        Me.ddlTipoPrograma.Items.Clear()
        Me.ddlTipoPrograma.Items.Add(New ListItem("<<Seleccione>>", ""))
        Me.ddlTipoPrograma.SelectedValue = ""
        Me.ddlTipoProgramaPregrado.SelectedValue = ""
        Me.TipoProgramaPregrado.Visible = False

        Me.ddlPrograma.Items.Clear()
        Me.ddlPrograma.Items.Add(New ListItem("<<Seleccione>>", "0"))
        Me.divProgramaEx.Visible = False
        Me.ddlUniversidad.Items.Clear()
        Me.ddlUniversidad.Items.Add(New ListItem("<<Seleccione>>", "0"))
        Me.divUniversidadEx.Visible = False
        Me.txtAnioIngreso.Text = ""
        Me.txtAnioEgreso.Text = ""
        Me.txtFechaEmision.Text = ""
        Me.ddlSituacion.SelectedValue = ""
    End Sub

    Function validar() As Boolean
        If Me.ddlTipoInstitucion.SelectedValue = "" Then
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "Nuevo", "alert('Seleccione el tipo de institución')", True)
            Return False
        End If
        If Me.ddlModalidad.SelectedValue = "" Then
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "Nuevo", "alert('Seleccione la modalidad de estudios')", True)
            Return False
        End If
        If Me.ddlTipoPrograma.SelectedValue = "" Then
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "Nuevo", "alert('Seleccione el nivel de Programa')", True)
            Return False
        End If
        If Me.ddlTipoPrograma.SelectedValue = "2" And Me.ddlTipoProgramaPregrado.SelectedValue = "" Then
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "Nuevo", "alert('Seleccione el Tipo')", True)
            Return False
        End If
        If Me.ddlPrograma.SelectedValue = "0" Then
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "Nuevo", "alert('Seleccione el Programa')", True)
            Return False
        End If
        If Me.ddlPrograma.SelectedItem.Text.ToLower = "otros programas de segunda especialidad - 000000" And Me.txtNombreProgramaEx.Text = "" Then
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "Nuevo", "alert('Ingrese nombre de programa de segunda especialidad')", True)
            ScriptManager.GetCurrent(Me.Page).SetFocus(Me.txtNombreProgramaEx)
            Return False
        End If
        If Me.ddlPrograma.SelectedItem.Text.ToLower = "programas del extranjero - 999999" And Me.txtNombreProgramaEx.Text = "" Then
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "Nuevo", "alert('Ingrese nombre de programa Extranjero')", True)
            ScriptManager.GetCurrent(Me.Page).SetFocus(Me.txtNombreProgramaEx)
            Return False
        End If
        If Me.ddlPrograma.SelectedItem.Text.ToLower = "programas de instituto - 000000" And Me.txtNombreProgramaEx.Text = "" Then
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "Nuevo", "alert('Ingrese nombre de programa')", True)
            ScriptManager.GetCurrent(Me.Page).SetFocus(Me.txtNombreProgramaEx)
            Return False
        End If
        If Me.ddlUniversidad.SelectedValue = "0" Then
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "Nuevo", "alert('Seleccione una institución')", True)
            Return False
        End If
        If Me.ddlUniversidad.SelectedValue = 144 And Me.txtNombreUniversidadEx.Text = "" Then
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "Nuevo", "alert('Ingrese nombre de Universidad extranjera')", True)
            ScriptManager.GetCurrent(Me.Page).SetFocus(Me.txtNombreUniversidadEx)
            Return False
        End If
        If Me.ddlUniversidad.SelectedItem.Text = "000 - Instituto" And Me.txtNombreUniversidadEx.Text = "" Then
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "Nuevo", "alert('Ingrese nombre de Instituto')", True)
            ScriptManager.GetCurrent(Me.Page).SetFocus(Me.txtNombreUniversidadEx)
            Return False
        End If
        If Me.ddlSituacion.SelectedValue = "" Then
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "Nuevo", "alert('Seleccione situación del grado/título')", True)
            Return False
        End If
        If Me.txtAnioIngreso.Text = "" Then
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "Nuevo", "alert('Ingrese año de ingreso')", True)
            ScriptManager.GetCurrent(Me.Page).SetFocus(Me.txtAnioIngreso)
            Return False
        End If
        If Not IsNumeric(Me.txtAnioIngreso.Text) Then
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "Nuevo", "alert('Año de ingreso solo permite números')", True)
            ScriptManager.GetCurrent(Me.Page).SetFocus(Me.txtAnioIngreso)
            Return False
        End If
        If Me.txtAnioEgreso.Text = "" And Me.ddlSituacion.SelectedValue = "1" Then
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "Nuevo", "alert('Ingrese año de egreso')", True)
            ScriptManager.GetCurrent(Me.Page).SetFocus(Me.txtAnioEgreso)
            Return False
        End If
        If Not IsNumeric(Me.txtAnioEgreso.Text) And Me.ddlSituacion.SelectedValue = "1" Then
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "Nuevo", "alert('Año de egreso solo permite números')", True)
            ScriptManager.GetCurrent(Me.Page).SetFocus(Me.txtAnioEgreso)
            Return False
        End If
        If Me.txtFechaEmision.Text = "" And Me.ddlSituacion.SelectedValue = "1" Then
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "Nuevo", "alert('Seleccione la fecha de emisión de diploma')", True)
            Return False
        End If

        Return True
    End Function


    Protected Sub btnGuardarRegistro_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardarRegistro.Click
        If (Session("id_per") Is Nothing) Then
            Response.Redirect("../../../sinacceso.html")
        End If

        If validar() = True Then
            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            Dim dt As New Data.DataTable

            Dim nombreprograma As String = ""
            If Me.txtNombreProgramaEx.Text <> "" Then
                nombreprograma = Me.txtNombreProgramaEx.Text
            Else
                nombreprograma = Me.ddlPrograma.SelectedItem.Text
            End If

            Dim nombreUniversidad As String = ""
            If Me.txtNombreUniversidadEx.Text <> "" Then
                nombreUniversidad = Me.txtNombreUniversidadEx.Text
            Else
                nombreUniversidad = Me.ddlUniversidad.SelectedItem.Text
            End If

            Dim anioegreso As Integer = 0
            If Me.txtAnioEgreso.Text <> "" Then
                anioegreso = Me.txtAnioEgreso.Text
            End If

            Dim fechaemision As String = ""
            Dim anioemision As Integer = 0
            If Me.txtFechaEmision.Text <> "" Then
                fechaemision = Me.txtFechaEmision.Text
                anioemision = CDate(Me.txtFechaEmision.Text).Year
            End If

            If Me.hdc.Value = "0" Then 'Nuevo
                If Me.ddlTipoPrograma.SelectedValue = "2" Then ' Pregrado
                    If Me.ddlTipoProgramaPregrado.SelectedValue = "T" Then ' titulo
                        dt = obj.TraerDataTable("AgregarTituloFADPersonal", Me.ddlPersonal.SelectedValue, Me.ddlPrograma.SelectedValue, 65, nombreprograma, Me.txtAnioIngreso.Text, anioegreso, anioemision, Me.ddlUniversidad.SelectedValue, nombreUniversidad, Me.ddlSituacion.SelectedValue, 1, Me.ddlModalidad.SelectedValue, fechaemision, Session("id_per"))
                    ElseIf Me.ddlTipoProgramaPregrado.SelectedValue = "B" Then ' bachiller
                        dt = obj.TraerDataTable("AgregarGradoFADPersonal", Me.ddlPrograma.SelectedValue, 26, nombreprograma, Me.ddlPersonal.SelectedValue, Me.txtAnioIngreso.Text, anioegreso, anioemision, "", Me.ddlUniversidad.SelectedValue, nombreUniversidad, Me.ddlSituacion.SelectedValue, 1, Me.ddlModalidad.SelectedValue, fechaemision, Session("id_per"))
                    End If
                ElseIf Me.ddlTipoPrograma.SelectedValue = "8" Then ' Segunda Especialidad
                    dt = obj.TraerDataTable("AgregarTituloFADPersonal", Me.ddlPersonal.SelectedValue, Me.ddlPrograma.SelectedValue, 65, nombreprograma, Me.txtAnioIngreso.Text, anioegreso, anioemision, Me.ddlUniversidad.SelectedValue, nombreUniversidad, Me.ddlSituacion.SelectedValue, 1, Me.ddlModalidad.SelectedValue, fechaemision, Session("id_per"))
                ElseIf Me.ddlTipoPrograma.SelectedValue = "9" Then ' Maestria
                    dt = obj.TraerDataTable("AgregarGradoFADPersonal", Me.ddlPrograma.SelectedValue, 27, nombreprograma, Me.ddlPersonal.SelectedValue, Me.txtAnioIngreso.Text, anioegreso, anioemision, "", Me.ddlUniversidad.SelectedValue, nombreUniversidad, Me.ddlSituacion.SelectedValue, 1, Me.ddlModalidad.SelectedValue, fechaemision, Session("id_per"))
                ElseIf Me.ddlTipoPrograma.SelectedValue = "11" Then ' Doctorado
                    dt = obj.TraerDataTable("AgregarGradoFADPersonal", Me.ddlPrograma.SelectedValue, 28, nombreprograma, Me.ddlPersonal.SelectedValue, Me.txtAnioIngreso.Text, anioegreso, anioemision, "", Me.ddlUniversidad.SelectedValue, nombreUniversidad, Me.ddlSituacion.SelectedValue, 1, Me.ddlModalidad.SelectedValue, fechaemision, Session("id_per"))
                End If
            Else ' Edición
                If Me.ddlTipoPrograma.SelectedValue = "2" Then ' Pregrado
                    If Me.ddlTipoProgramaPregrado.SelectedValue = "T" Then ' titulo
                        dt = obj.TraerDataTable("ActualizarTituloFADPersonal", Me.hdc.Value, Me.hdfa.Value, Me.ddlPersonal.SelectedValue, Me.ddlPrograma.SelectedValue, 65, nombreprograma, Me.txtAnioIngreso.Text, anioegreso, anioemision, Me.ddlUniversidad.SelectedValue, nombreUniversidad, Me.ddlSituacion.SelectedValue, 1, Me.ddlModalidad.SelectedValue, fechaemision, Session("id_per"), Me.hdt.Value)
                    ElseIf Me.ddlTipoProgramaPregrado.SelectedValue = "B" Then ' bachiller
                        dt = obj.TraerDataTable("ActualizarGradoFADPersonal", Me.hdc.Value, Me.hdfa.Value, Me.ddlPrograma.SelectedValue, 26, nombreprograma, Me.ddlPersonal.SelectedValue, Me.txtAnioIngreso.Text, anioegreso, anioemision, "", Me.ddlUniversidad.SelectedValue, nombreUniversidad, Me.ddlSituacion.SelectedValue, 1, Me.ddlModalidad.SelectedValue, fechaemision, Session("id_per"), Me.hdt.Value)
                    End If
                ElseIf Me.ddlTipoPrograma.SelectedValue = "8" Then 'Segunda Especialidad
                    dt = obj.TraerDataTable("ActualizarTituloFADPersonal", Me.hdc.Value, Me.hdfa.Value, Me.ddlPersonal.SelectedValue, Me.ddlPrograma.SelectedValue, 65, nombreprograma, Me.txtAnioIngreso.Text, anioegreso, anioemision, Me.ddlUniversidad.SelectedValue, nombreUniversidad, Me.ddlSituacion.SelectedValue, 1, Me.ddlModalidad.SelectedValue, fechaemision, Session("id_per"), Me.hdt.Value)
                ElseIf Me.ddlTipoPrograma.SelectedValue = "9" Then ' Maestria
                    dt = obj.TraerDataTable("ActualizarGradoFADPersonal", Me.hdc.Value, Me.hdfa.Value, Me.ddlPrograma.SelectedValue, 27, nombreprograma, Me.ddlPersonal.SelectedValue, Me.txtAnioIngreso.Text, anioegreso, anioemision, "", Me.ddlUniversidad.SelectedValue, nombreUniversidad, Me.ddlSituacion.SelectedValue, 1, Me.ddlModalidad.SelectedValue, fechaemision, Session("id_per"), Me.hdt.Value)
                ElseIf Me.ddlTipoPrograma.SelectedValue = "11" Then ' Doctorado
                    dt = obj.TraerDataTable("ActualizarGradoFADPersonal", Me.hdc.Value, Me.hdfa.Value, Me.ddlPrograma.SelectedValue, 28, nombreprograma, Me.ddlPersonal.SelectedValue, Me.txtAnioIngreso.Text, anioegreso, anioemision, "", Me.ddlUniversidad.SelectedValue, nombreUniversidad, Me.ddlSituacion.SelectedValue, 1, Me.ddlModalidad.SelectedValue, fechaemision, Session("id_per"), Me.hdt.Value)
                End If
            End If
            obj.CerrarConexion()

            If dt.Rows(0).Item("rpta") = "1" Then
                ConsultarGradosTitulosPersonal(Me.ddlPersonal.SelectedValue)
                ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "GuardarDatos", "alert('" + dt.Rows(0).Item("msje").ToString + "');$('#mdEditar').hide();", True)
            Else
                ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "GuardarDatos", "alert('" + dt.Rows(0).Item("msje").ToString + "')", True)
            End If

            'ConsultarGradosTitulosPersonal(Me.ddlPersonal.SelectedValue)
            'ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "GuardarDatos", "alert('Grado/Título Registrado correctamente');$('#mdEditar').hide();", True)

        End If
    End Sub

    Protected Sub ddlTipoInstitucion_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlTipoInstitucion.SelectedIndexChanged
        CambioTipoInstitucion()
        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "Nuevo", "initCombo('ddlUniversidad');", True)
    End Sub

    Private Sub CambioTipoInstitucion()
        Me.ddlTipoProgramaPregrado.Items.Clear()
        Me.ddlTipoPrograma.Items.Clear()
        Me.ddlTipoProgramaPregrado.Items.Add(New ListItem("<<Seleccione>>", ""))
        Me.ddlTipoPrograma.Items.Add(New ListItem("<<Seleccione>>", ""))
        Me.ddlPrograma.Items.Clear()
        Me.ddlPrograma.Items.Add(New ListItem("<<Seleccione>>", "0"))
        If Me.ddlTipoInstitucion.SelectedItem.Text = "INSTITUTO" Then
            Me.ddlTipoPrograma.Items.Add(New ListItem("Pregrado", "2"))
            Me.ddlTipoProgramaPregrado.Items.Add(New ListItem("Título", "T"))
        Else
            Me.ddlTipoProgramaPregrado.Items.Add(New ListItem("Bachiller", "B"))
            Me.ddlTipoProgramaPregrado.Items.Add(New ListItem("Título", "T"))
            Me.ddlTipoPrograma.Items.Add(New ListItem("Pregrado", "2"))
            Me.ddlTipoPrograma.Items.Add(New ListItem("Segunda Especialidad", "8"))
            Me.ddlTipoPrograma.Items.Add(New ListItem("Maestría", "9"))
            Me.ddlTipoPrograma.Items.Add(New ListItem("Doctorado", "11"))
        End If
        ListarUniversidades()
    End Sub


    Protected Sub gvGradosTitulosPersonal_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvGradosTitulosPersonal.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            If Me.gvGradosTitulosPersonal.DataKeys(e.Row.RowIndex).Values("codigogra_fad") <> "0" Then
                Dim boton As LinkButton = DirectCast(e.Row.Cells(7).FindControl("btnVincular"), LinkButton)
                boton.CssClass = "btn btn-warning btn-sm"
                boton.ToolTip = "Desvincular"
                boton.Text = "<span class='fa fa-chain-broken'></span>"
                boton.OnClientClick = "return confirm('¿Esta seguro que desea quitar vínculo del registro?')"
            End If
            If Me.gvGradosTitulosPersonal.DataKeys(e.Row.RowIndex).Values("validado") <> "0" Then
                Dim boton As LinkButton = DirectCast(e.Row.Cells(6).FindControl("btnValidar"), LinkButton)
                boton.CssClass = "btn btn-warning btn-sm"
                boton.ToolTip = "Quitar validación"
                boton.Text = "<span class='fa fa-times-circle'></span>"
                boton.OnClientClick = "return confirm('¿Esta seguro que desea quitar validación del registro?')"
            End If
        End If

    End Sub


    Private Sub mt_RefreshGrid()
        Try
            For Each _Row As GridViewRow In Me.gvGradosTitulosPersonal.Rows
                gvGradosTitulosPersonal_RowDataBound(Me.gvGradosTitulosPersonal, New GridViewRowEventArgs(_Row))
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub mt_RefreshGrid2()
        Try
            For Each _Row As GridViewRow In Me.gvFormacionAcademica.Rows
                gvFormacionAcademica_RowDataBound(Me.gvFormacionAcademica, New GridViewRowEventArgs(_Row))
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Protected Sub gvFormacionAcademica_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvFormacionAcademica.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            If Me.gvFormacionAcademica.DataKeys(e.Row.RowIndex).Values("vinculado") <> "0" Then
                Dim boton As LinkButton = DirectCast(e.Row.Cells(0).FindControl("btnSeleccionarVinculo"), LinkButton)
                boton.OnClientClick = "return false;"
                boton.CssClass = "btn btn-primary btn-sm"
                boton.ToolTip = "Vinculado"
                boton.Text = "<span class='fa fa-link'></span>"

            End If
        End If
    End Sub

    Private Sub validar(ByVal codigo As String, ByVal tipo As String, ByVal estado As Integer)
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Dim tb As New Data.DataTable
        tb = obj.TraerDataTable("ValidarGradosTitulosPersonal", codigo, tipo, estado)
        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "GuardarDatos", "alert('" + tb.Rows(0).Item("msje").ToString + "')", True)
        obj.CerrarConexion()
    End Sub

    Protected Sub chkEducacion_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkEducacion.CheckedChanged
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Dim tb As New Data.DataTable
        Dim check As Integer = 0
        If Me.chkEducacion.Checked = True Then
            check = 1
        Else
            check = 0
        End If
        tb = obj.TraerDataTable("CheckEducacionEnPeru", Me.ddlPersonal.SelectedValue, check)
        obj.CerrarConexion()
    End Sub

    Protected Sub ddlSituacion_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlSituacion.SelectedIndexChanged
        cambiarSituacion()
    End Sub
    Private Sub cambiarSituacion()
        Me.txtFechaEmision.Text = ""
        Me.txtAnioEgreso.Text = ""
        If Me.ddlSituacion.SelectedValue = "2" Then
            Me.txtFechaEmision.Text = ""
            Me.txtAnioEgreso.Text = ""
            Me.txtFechaEmision.ReadOnly = True
            Me.txtAnioEgreso.ReadOnly = True
        Else
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MostrarmodalVIncular", "Calendario();", True)
            Me.txtFechaEmision.ReadOnly = False
            Me.txtAnioEgreso.ReadOnly = False
        End If
    End Sub

End Class

