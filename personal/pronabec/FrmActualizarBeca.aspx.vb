﻿Imports System.Data
Partial Class FrmActualizarBeca
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (Session("id_per") Is Nothing) Then
            Response.Redirect("../../sinacceso.html")
        End If
        Try
            If Not IsPostBack Then
                'ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "Cargar", "fnLoading(true);", True)
                ListarCarreras(Request("mod"))
                'ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "Cargar", "fnLoading(false);", True)
                ListarCicloAcademico()
                'ListarAplicaciones()
            End If
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "Busqueda", "initCombo('ddlCicloAcademico');", True)
            'ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "Cargar", "fnLoading(true);", True)
            'ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "Busqueda", "initCombo('ddlAplicacion');", True)
        Catch ex As Exception
            Response.Write(ex.Message.ToString)
        End Try
    End Sub

    Private Sub ListarCarreras(ByVal codigo_Test As Integer)
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Dim tb As New Data.DataTable
        tb = obj.TraerDataTable("[ConsultarCarrerasPronabec]", codigo_Test)
        Me.ddlCarrera.Items.Add(New ListItem("<<Seleccione>>", ""))
        If tb.Rows.Count > 0 Then
            For i As Integer = 0 To tb.Rows.Count - 1
                Me.ddlCarrera.Items.Add(New ListItem(tb.Rows(i).Item("nombre_cpf"), tb.Rows(i).Item("codigo_cpf")))
            Next
        End If
        obj.CerrarConexion()
    End Sub

    Private Sub ListarCicloAcademico()
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Dim tb As New Data.DataTable
        tb = obj.TraerDataTable("[ConsultarCicloAcademico]", "U5", "")
        Me.ddlCicloAcademico.Items.Add(New ListItem("<<Seleccione>>", ""))
        If tb.Rows.Count > 0 Then
            For i As Integer = 0 To tb.Rows.Count - 1
                Me.ddlCicloAcademico.Items.Add(New ListItem(tb.Rows(i).Item("descripcion_cac"), tb.Rows(i).Item("codigo_cac")))
            Next
        End If
        obj.CerrarConexion()
    End Sub

    'Private Sub ListarAplicaciones()
    '    Dim obj As New ClsConectarDatos
    '    obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
    '    obj.AbrirConexion()
    '    Dim tb As New Data.DataTable
    '    tb = obj.TraerDataTable("ConsultarAplicacionUsuario", "1", 0, 0, 0)
    '    Me.ddlAplicacion.Items.Add(New ListItem("<<Seleccione>>", ""))
    '    If tb.Rows.Count > 0 Then
    '        For i As Integer = 0 To tb.Rows.Count - 1
    '            Me.ddlAplicacion.Items.Add(New ListItem(tb.Rows(i).Item("descripcion_apl"), tb.Rows(i).Item("codigo_apl")))
    '        Next
    '    End If
    '    obj.CerrarConexion()
    'End Sub

    Protected Sub ddlCarrera_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlCarrera.SelectedIndexChanged
        Try
            'ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "Cargar", "fnLoading(true);", True)
            If Me.ddlCarrera.SelectedValue <> "" Then
                ListarAlumnos(Me.ddlCarrera.SelectedValue)
            Else

                Me.gvAlumnos.DataSource = ""
                Me.gvAlumnos.DataBind()
            End If
            'ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "Busqueda", "initCombo('ddlCarrera');", True)
            'ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "Cargar", "fnLoading(false);", True)
        Catch ex As Exception
            Response.Write(ex.Message.ToString)
        End Try
    End Sub

    Private Sub ListarAlumnos(ByVal codigo_cpf As Integer)
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Dim tb As New Data.DataTable
        tb = obj.TraerDataTable("[ConsultarAlumnosPronabecxCarrera]", codigo_cpf)

        Me.gvAlumnos.DataSource = tb
        Me.gvAlumnos.DataBind()

        obj.CerrarConexion()
    End Sub

    'Private Sub ListarConfiguraciónCargoAplicacionTipoFuncion(ByVal codigo_cgo As Integer)
    '    Dim obj As New ClsConectarDatos
    '    obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
    '    obj.AbrirConexion()
    '    Dim tb As New Data.DataTable
    '    tb = obj.TraerDataTable("ListarConfiguracionCargoAplicacionTipoFuncion", codigo_cgo)

    '    Me.gvAlumnos.DataSource = tb
    '    Me.gvAlumnos.DataBind()

    '    obj.CerrarConexion()
    'End Sub

    'Protected Sub btnAgregar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAgregar.Click
    '    Try
    '        If validar() = True Then
    '            Dim dt As New Data.DataTable
    '            dt = Guardar(Me.ddlCarrera.SelectedValue, Me.ddlAplicacion.SelectedValue, Me.ddlTipoFuncion.SelectedValue, Session("id_per"))
    '            'Response.Write("alert('" + dt.Rows(0).Item("msje").ToString + "')")
    '            If dt.Rows(0).Item("rpta").ToString = "1" Then
    '                ListarConfiguraciónCargoAplicacionTipoFuncion(Me.ddlCarrera.SelectedValue)
    '            End If
    '        End If
    '    Catch ex As Exception
    '        Response.Write(ex.Message.ToString)
    '    End Try
    'End Sub

    'Private Function validar() As Boolean
    '    If Me.ddlCarrera.SelectedValue = "" Then
    '        Return False
    '    End If
    '    If Me.ddlAplicacion.SelectedValue = "" Then
    '        Return False
    '    End If
    '    If Me.ddlTipoFuncion.SelectedValue = "" Then
    '        Return False
    '    End If
    '    Return True
    'End Function

    'Private Function Guardar(ByVal codigo_cgo As Integer, ByVal codigo_apl As Integer, ByVal codigo_tfu As Integer, ByVal codigo_per As Integer) As Data.DataTable
    '    Dim obj As New ClsConectarDatos
    '    obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
    '    obj.AbrirConexion()
    '    Dim tb As New Data.DataTable
    '    tb = obj.TraerDataTable("RegistrarConfiguracionCargoAplicacionTipoFuncion", codigo_cgo, codigo_apl, codigo_tfu, codigo_per)
    '    Return tb
    '    obj.CerrarConexion()
    'End Function

    Protected Sub gvAlumnos_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvAlumnos.RowCommand
        If (Session("id_per") Is Nothing) Then
            Response.Redirect("../../sinacceso.html")
        End If
        If e.CommandName = "Eliminar" Then
            ActualizarEstadoBeca(Me.gvAlumnos.DataKeys(e.CommandArgument).Values("codigouniver_alu"), 0, 0, Session("id_per"))
        End If
    End Sub

    Protected Sub btnAgregar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAgregar.Click
        If (Session("id_per") Is Nothing) Then
            Response.Redirect("../../sinacceso.html")
        End If
        Try
            If validarAgregar() = True Then
                ActualizarEstadoBeca(Me.txtCodigo.Text.Trim, Me.ddlCicloAcademico.SelectedValue, 1, Session("id_per"))
            End If
        Catch ex As Exception
            Response.Write(ex.Message.ToString)
        End Try
    End Sub

    Private Sub ActualizarEstadoBeca(ByVal codigouniver As String, ByVal codigo_cac As Integer, ByVal estado As Integer, ByVal usuario_Reg As Integer)
        Dim dt As New Data.DataTable
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        dt = obj.TraerDataTable("[ActualizarEstadoBecaPronabec]", codigouniver, codigo_cac, estado, usuario_Reg)
        If dt.Rows(0).Item("Respuesta") = "1" Then
            Me.txtCodigo.Text = ""
            Me.ddlCicloAcademico.SelectedValue = ""
            Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "fnMensaje('success','" + dt.Rows(0).Item("Mensaje").ToString + "')", True)
            If Me.ddlCarrera.SelectedValue <> "" Then
                Me.ListarAlumnos(Me.ddlCarrera.SelectedValue)
            Else
                Me.gvAlumnos.DataSource = ""
                Me.gvAlumnos.DataBind()
            End If
        Else
            Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "fnMensaje('error','" + dt.Rows(0).Item("Mensaje").ToString + "')", True)
        End If
    End Sub

    Function validarAgregar() As Boolean
        If Me.txtCodigo.Text.Trim = "" Then
            Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "fnMensaje('error','Ingrese Código Universitario')", True)
            Return False
        End If
        If Me.ddlCicloAcademico.SelectedValue = "" Then
            Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "fnMensaje('error','Seleccione Ciclo académico de inicio de beneficio')", True)
            Return False
        End If
        Return True
    End Function


End Class

