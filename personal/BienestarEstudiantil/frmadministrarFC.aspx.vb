
Partial Class frmadministrarFC
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "Loading", "fnLoading(true)", True)
        If IsPostBack = False Then

            Me.GvAlumnos.visible = False

            ocultar()

        End If
    End Sub

    Private Sub ocultar()
        Me.FotoAlumno.Visible = False
        lbltitCodigo.visible = False
        lbltitAlumno.visible = False
        lbltitEscuela.visible = False
        lbltitCicloIngreso.visible = False
        lbltitPlan.visible = False

        lblcodigo.visible = False
        lblalumno.visible = False
        lblescuela.visible = False
        lblcicloingreso.visible = False
        lblPlan.visible = False

        updMatriculas.visible = False
        divMatricula.visible = False

        updLista.visible = False
        updLista1.visible = False

    End Sub

    Private Sub mostrar()

        lbltitCodigo.visible = True
        lbltitAlumno.visible = True
        lbltitEscuela.visible = True
        lbltitCicloIngreso.visible = True
        lbltitPlan.visible = True

        lblcodigo.visible = True
        lblalumno.visible = True
        lblescuela.visible = True
        lblcicloingreso.visible = True
        lblPlan.visible = True

        updMatriculas.visible = True
        divMatricula.visible = True

        'updLista.visible = True
        'updLista1.visible = True

    End Sub

    Private Sub Listar()
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Dim dt As New Data.DataTable
        dt = obj.TraerDataTable("[BE_ConsultarCursoFormacionComplementaria]", Me.hdcod_univ.value, 486, Me.hdciclo.value, "D")

        If dt.Rows.Count > 0 Then
            Me.gvLista.DataSource = dt
            Me.gvLista.DataBind()
        Else
            Me.gvLista.DataSource = Nothing
            Me.gvLista.DataBind()
        End If

        obj.CerrarConexion()
    End Sub

    Private Sub Listar1()
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Dim dt As New Data.DataTable
        dt = obj.TraerDataTable("[BE_ConsultarCursoFormacionComplementaria]", Me.hdcod_univ.value, 486, hdciclo.value, "M")

        If dt.Rows.Count > 0 Then
            Me.gvLista1.DataSource = dt
            Me.gvLista1.DataBind()
        Else
            Me.gvLista1.DataSource = Nothing
            Me.gvLista1.DataBind()
        End If

        obj.CerrarConexion()
    End Sub

    Private Sub ListarMat()
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Dim dt As New Data.DataTable
        dt = obj.TraerDataTable("[BE_ConsultarMatriculasFormacionComplementaria]", Me.hdcod_univ.value)

        If dt.Rows.Count > 0 Then
            Me.gvmatriculas.DataSource = dt
            Me.gvmatriculas.DataBind()
        Else
            Me.gvmatriculas.DataSource = Nothing
            Me.gvmatriculas.DataBind()
        End If

        obj.CerrarConexion()
    End Sub


    Protected Sub lbNuevo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbNuevo.Click
        Me.Lista.Visible = False
        Me.DivMantenimiento.Visible = True
        Me.txtDescripcion.Text = ""        
        Me.hdc.Value = 0
        Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "Loading", "fnLoading(false)", True)

    End Sub

    Protected Sub lbRetiro_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbRetiro.Click
        Me.Lista1.Visible = False
        Me.DivMantenimiento1.Visible = True
        Me.txtDescripcion1.Text = ""        
        Me.hdc1.Value = 0
        Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "Loading", "fnLoading(false)", True)

    End Sub

    Protected Sub lbCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbCancelar.Click
        Me.Lista.Visible = True
        Me.DivMantenimiento.Visible = False
        Me.txtDescripcion.Text = ""        
        Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "Loading", "fnLoading(false)", True)

    End Sub

    Protected Sub lbCancelar1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbCancelar1.Click
        Me.Lista1.Visible = True
        Me.DivMantenimiento1.Visible = False
        Me.txtDescripcion1.Text = ""        
        Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "Loading", "fnLoading(false)", True)

    End Sub

    Protected Sub gvLista_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvLista.RowCommand
        'If (Session("id_per") Is Nothing) Then
        '    Response.Redirect("../../../sinacceso.html")
        'End If
        Try
            'If (e.CommandName = "Editar") Then
            '    Me.Lista.Visible = False
            '    Me.DivMantenimiento.Visible = True
            '    Me.hdc.Value = Me.gvLista.DataKeys(e.CommandArgument).Values("codigo_tio")
            '    Me.txtDescripcion.Text = Me.gvLista.DataKeys(e.CommandArgument).Values("nombre_tio")
            '    Me.txtDetalle.Text = Me.gvLista.DataKeys(e.CommandArgument).Values("descripcion_tio")
            'End If
            'If (e.CommandName = "Eliminar") Then
            '    Dim codigo As Integer = Me.gvLista.DataKeys(e.CommandArgument).Values("codigo_tio")
            '    EliminarTipo(codigo, Session("id_per"))
            'End If
            Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "Loading", "fnLoading(false)", True)

        Catch ex As Exception

            Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "fnMensaje('error','" + ex.Message.ToString + "')", True)

        End Try
    End Sub

    Private Sub ActualizarTipo(ByVal codigo As Integer, ByVal descripcion As String, ByVal detalle As String, ByVal usuario As Integer)
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Dim dt As New Data.DataTable
        dt = obj.TraerDataTable("[OAC_ActualizarTipoOrientacion]", codigo, descripcion, detalle, usuario)
        obj.CerrarConexion()
        If dt.Rows(0).Item("Respuesta") = 1 Then
            Me.DivMantenimiento.Visible = False
            Me.Lista.Visible = True
            'Listar()
            Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "alert1", "fnMensaje('success','" + dt.Rows(0).Item("Mensaje") + "')", True)
            Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "Loading1", "fnLoading(false)", True)
        Else
            Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "alert2", "fnMensaje('error','" + dt.Rows(0).Item("Mensaje") + "')", True)
        End If
    End Sub

    Protected Sub lbGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbGuardar.Click
        If Me.txtDescripcion.Text.Trim = "" Then
            Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "alert2", "fnMensaje('error','Ingrese Motivo')", True)
        Else

            'ActualizarTipo(Me.hdc.Value, Me.txtDescripcion.Text, Me.txtDescripcion.Text, Session("id_per"))
        End If
    End Sub

    Protected Sub lbGuardar1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbGuardar1.Click
        If Me.txtDescripcion1.Text.Trim = "" Then
            Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "alert2", "fnMensaje('error','Ingrese Motivo')", True)
        Else

            'ActualizarTipo(Me.hdc.Value, Me.txtDescripcion.Text, Me.txtDescripcion.Text, Session("id_per"))
        End If
    End Sub

    Private Sub EliminarTipo(ByVal codigo As Integer, ByVal usuario As Integer)
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Dim dt As New Data.DataTable
        dt = obj.TraerDataTable("[OAC_EliminarTipoOrientacion]", codigo, usuario)
        obj.CerrarConexion()
        If dt.Rows(0).Item("Respuesta") = 1 Then
            'Listar()
            Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "alert3", "fnMensaje('success','" + dt.Rows(0).Item("Mensaje") + "')", True)
            Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "Loading2", "fnLoading(false)", True)
        Else
            Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "alert4", "fnMensaje('error','" + dt.Rows(0).Item("Mensaje") + "')", True)
        End If
    End Sub

    Private Sub VerDetalle()
        'If IsPostBack = False Then

        Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("cnxBDUSAT").ConnectionString)
        Dim Tbl As New Data.DataTable

        Tbl = obj.TraerDataTable("consultaracceso", "E", Me.txtcodigo.Text.Trim, 0)
        Me.lblMensaje.Visible = False

        If Tbl.Rows.Count > 0 Then
            Me.lblcodigo.text = Tbl.Rows(0).Item("Codigouniver_alu")
            Me.lblalumno.Text = Tbl.Rows(0).Item("alumno")
            Me.lblescuela.Text = Tbl.Rows(0).Item("nombre_cpf")
            Me.lblcicloingreso.Text = Tbl.Rows(0).Item("cicloing_alu")
            Me.lblPlan.Text = Tbl.Rows(0).Item("descripcion_pes")

            'Cargar la Foto
            Dim ruta As String
            Dim obEnc As Object
            obEnc = Server.CreateObject("EncriptaCodigos.clsEncripta")

            ruta = obEnc.CodificaWeb("069" & Me.txtcodigo.Text.Trim)
            'ruta = "http://www.usat.edu.pe/imgestudiantes/" & ruta
            ruta = "..\..\imgestudiantes\" & ruta
            Me.FotoAlumno.ImageUrl = ruta
            obEnc = Nothing

            divMatricula.visible = True
            ListarMat() 'Lista matriculas

            lbltitCodigo.visible = True
            lbltitAlumno.visible = True
            lbltitEscuela.visible = True
            lbltitCicloIngreso.visible = True
            lbltitPlan.visible = True

            'updLista.visible = True
            'updLista1.visible = True

            'Listar()
            'Listar1()

            Me.FotoAlumno.Visible = True

        Else
            Me.lblMensaje.Text = "El estudiante no existe en la Base de datos"
            Me.FotoAlumno.Visible = False
        End If
        Tbl.Dispose()
        obj = Nothing
        'End If
    End Sub

    Protected Sub GvAlumnos_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GvAlumnos.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim Fila As Data.DataRowView
            Fila = e.Row.DataItem
            e.Row.Attributes.Add("OnMouseOver", "Resaltar(1,this,'S')")
            e.Row.Attributes.Add("OnMouseOut", "Resaltar(0,this,'S')")
            e.Row.Attributes.Add("OnClick", "javascript:__doPostBack('GvAlumnos','Select$" & e.Row.RowIndex & "')")
            'If e.Row.Cells(4).Text = 1 Then
            '    e.Row.Cells(4).Text = "Activo"
            'Else
            '    e.Row.Cells(4).Text = "Inactivo"
            'End If
        End If
    End Sub

    Protected Sub GvAlumnos_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GvAlumnos.SelectedIndexChanged

        'Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
        'Dim datos As New Data.DataTable

        'Consulta plan de estudio por codigo_univer
        Me.hdcod_univ.value = Me.GvAlumnos.DataKeys.Item(Me.GvAlumnos.SelectedIndex).Values(1).ToString()
        'Response.Write(hdcod_univ.value)
        'Session("codigo_alu") = codigo_alu

        'datos = obj.TraerDataTable() 'Me.GvAlumnos.Rows(Me.GvAlumnos.SelectedIndex).Cells(1).Text
        'With datos.Rows(0)
        '    If datos.Rows.Count > 0 Then
        '        Panel1.Visible = True                           

        '    End If
        'End With

        Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("cnxBDUSAT").ConnectionString)
        Dim Tbl As New Data.DataTable

        Tbl = obj.TraerDataTable("consultaracceso", "E", Me.hdcod_univ.value, 0)
        Me.lblMensaje.Visible = False

        If Tbl.Rows.Count > 0 Then
            Me.lblcodigo.text = Tbl.Rows(0).Item("Codigouniver_alu")
            Me.lblalumno.Text = Tbl.Rows(0).Item("alumno")
            Me.lblescuela.Text = Tbl.Rows(0).Item("nombre_cpf")
            Me.lblcicloingreso.Text = Tbl.Rows(0).Item("cicloing_alu")
            Me.lblPlan.Text = Tbl.Rows(0).Item("descripcion_pes")

            'Cargar la Foto
            Dim ruta As String
            Dim obEnc As Object
            obEnc = Server.CreateObject("EncriptaCodigos.clsEncripta")

            ruta = obEnc.CodificaWeb("069" & Me.txtcodigo.Text.Trim)
            'ruta = "http://www.usat.edu.pe/imgestudiantes/" & ruta
            ruta = "..\..\imgestudiantes\" & ruta
            Me.FotoAlumno.ImageUrl = ruta
            obEnc = Nothing

            divMatricula.visible = True
            ListarMat() 'Lista matriculas

            mostrar()

            'updLista.visible = True
            'updLista1.visible = True

            'Listar()
            'Listar1()

            Me.FotoAlumno.Visible = True

        Else
            Me.lblMensaje.Text = "El estudiante no existe en la Base de datos"
            Me.FotoAlumno.Visible = False
        End If

        Tbl.Dispose()
        obj = Nothing


    End Sub

    Protected Sub gvmatriculas_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvmatriculas.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim Fila As Data.DataRowView
            Fila = e.Row.DataItem
            e.Row.Attributes.Add("OnMouseOver", "Resaltar(1,this,'S')")
            e.Row.Attributes.Add("OnMouseOut", "Resaltar(0,this,'S')")
            e.Row.Attributes.Add("OnClick", "javascript:__doPostBack('gvmatriculas','Select$" & e.Row.RowIndex & "')")
        End If
    End Sub

    Protected Sub gvmatriculas_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvmatriculas.SelectedIndexChanged

        'Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
        'Dim datos As New Data.DataTable  

        'Me.hdcod_alu.value = gvmatriculas.DataKeys.Item(Me.gvmatriculas.SelectedIndex).Values(0).ToString()
        Me.hdciclo.value = gvmatriculas.DataKeys.Item(Me.gvmatriculas.SelectedIndex).Values(0).ToString()
        'Response.Write(Me.hdciclo.value)

        updLista.visible = True
        updLista1.visible = True

        'Listar()
        'Listar1()
        'Response.Write(hdcod_univ.value)

        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Dim dt As New Data.DataTable
        dt = obj.TraerDataTable("[BE_ConsultarCursoFormacionComplementaria]", Me.hdcod_univ.value, 486, Me.hdciclo.value, "D")

        If dt.Rows.Count > 0 Then
            Me.gvLista.DataSource = dt
            Me.gvLista.DataBind()
        Else
            Me.gvLista.DataSource = Nothing
            Me.gvLista.DataBind()
        End If

        obj.CerrarConexion()

        'Codigo de Listar1
        Dim obj1 As New ClsConectarDatos
        obj1.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj1.AbrirConexion()
        Dim dt1 As New Data.DataTable
        dt1 = obj1.TraerDataTable("[BE_ConsultarCursoFormacionComplementaria]", Me.hdcod_univ.value, 486, hdciclo.value, "M")

        If dt1.Rows.Count > 0 Then
            Me.gvLista1.DataSource = dt1
            Me.gvLista1.DataBind()
        Else
            Me.gvLista1.DataSource = Nothing
            Me.gvLista1.DataBind()
        End If

        obj1.CerrarConexion()

    End Sub

    Protected Sub cmdBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdBuscar.Click

        Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)

        Me.GvAlumnos.DataSource = obj.TraerDataTable("EVE_ConsultarAlumnosPorModulo_v2", -1, trim(txtcodigo.text))

        Me.GvAlumnos.visible = True

        Me.GvAlumnos.DataBind()

        ocultar()

    End Sub
End Class

