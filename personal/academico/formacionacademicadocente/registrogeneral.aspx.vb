Imports System.Data
Partial Class academico_formacionacademicadocente_registrogeneral
    Inherits System.Web.UI.Page
    Public Enum MessageType
        Success
        Errors
        Info
        Warning
    End Enum
    Protected Sub ShowMessage(ByVal Message As String, ByVal type As MessageType)
        Page.RegisterStartupScript("Mensaje", "<script>ShowMessage('" & Message & "','" & type & "');</script>")
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            CargarDptos()
            ' CargarDocentes()
            CargarProgramas()
            CargarUniversidad()
            'CargarGrados()

        End If
    End Sub
    Sub CargarDptos()
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Dim tb As New Data.DataTable
        tb = obj.TraerDataTable("FAD_ConsultarDptoAcad", Request.QueryString("id"))
        If tb.Rows.Count > 0 Then
            Me.ddlDpto.DataSource = tb
            Me.ddlDpto.DataTextField = "nombre_Dac"
            Me.ddlDpto.DataValueField = "codigo_Dac"
            Me.ddlDpto.DataBind()
            CargarDocentes()
        Else
            '            ShowMessage("Usuario no tiene permisos por departamento. ", MessageType.Errors)
            Response.Write("Usuario no tiene permisos por departamento. ")
        End If
        
        obj.CerrarConexion()
        obj = Nothing
    End Sub
    Sub limpiar()
        Me.ddlPrograma.SelectedIndex = 0
        Me.ddlUniversidad.SelectedIndex = 0
        Me.txtProgramaExtranjero.Text = ""
        Me.txtUniversidadExtranjero.Text = ""
    End Sub
    Sub CargarDocentes()
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Dim tb As New Data.DataTable
        tb = obj.TraerDataTable("Encuesta_ListarDocentes", Me.ddlDpto.SelectedValue, Me.ddlEstado.SelectedValue)
        If tb.Rows.Count > 0 Then
            Me.ddlDocente.DataSource = tb
            Me.ddlDocente.DataTextField = "Personal"
            Me.ddlDocente.DataValueField = "codigo_Per"
            Me.ddlDocente.DataBind()
            limpiar()
            CargarGrados()
        Else
            ShowMessage("Usuario no tiene permisos por departamento. ", MessageType.Errors)
        End If
        obj.CerrarConexion()
        obj = Nothing
    End Sub
    Sub CargarProgramas()
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Dim tb As New Data.DataTable
        tb = obj.TraerDataTable("FAD_ConsultarProgramas", Me.ddlNivel.SelectedValue)
        Me.ddlPrograma.DataSource = tb
        Me.ddlPrograma.DataTextField = "nombre_pro"
        Me.ddlPrograma.DataValueField = "codigo_pro"
        Me.ddlPrograma.DataBind()
        obj.CerrarConexion()
        obj = Nothing
    End Sub
    Sub CargarUniversidad()
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Dim tb As New Data.DataTable
        tb = obj.TraerDataTable("FAD_ConsultarUniversidad")
        Me.ddlUniversidad.DataSource = tb
        Me.ddlUniversidad.DataTextField = "nombre_uni"
        Me.ddlUniversidad.DataValueField = "codigo_uni"
        Me.ddlUniversidad.DataBind()
        obj.CerrarConexion()
        obj = Nothing
    End Sub
    Protected Sub ddlDpto_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlDpto.SelectedIndexChanged
        Try
            CargarDocentes()
        Catch ex As Exception

        End Try

    End Sub

    Protected Sub ddlEstado_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlEstado.SelectedIndexChanged
        Try
            CargarDocentes()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub ddlNivel_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlNivel.SelectedIndexChanged
        Try
            CargarProgramas()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub ddlPrograma_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPrograma.SelectedIndexChanged
        Try
            If (Me.ddlNivel.SelectedValue = 2 And Me.ddlPrograma.SelectedValue = 1822) Or (Me.ddlNivel.SelectedValue = 9 And Me.ddlPrograma.SelectedValue = 5181) Or (Me.ddlNivel.SelectedValue = 11 And Me.ddlPrograma.SelectedValue = 5586) Then
                Me.ddlUniversidad.SelectedValue = 144
                Me.txtUniversidadExtranjero.Enabled = True
                Me.txtProgramaExtranjero.Enabled = True
                Me.txtProgramaExtranjero.Focus()

            Else
                Me.txtProgramaExtranjero.Enabled = False
                Me.txtUniversidadExtranjero.Enabled = False
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub ddlUniversidad_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlUniversidad.SelectedIndexChanged
        Try
            If Me.ddlUniversidad.SelectedValue = 144 Then
                Me.txtUniversidadExtranjero.Enabled = True
                Me.txtUniversidadExtranjero.Focus()
            Else
                Me.txtUniversidadExtranjero.Enabled = False
            End If
        Catch ex As Exception

        End Try
    End Sub

    Sub CargarGrados()
        Try
            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            Dim tb As New Data.DataTable
            tb = obj.TraerDataTable("FAD_ConsultarGrado", Me.ddlDocente.SelectedValue)
                 If tb.Rows.Count Then
                Me.gvGrados.DataSource = tb
            Else
                Me.gvGrados.DataSource = Nothing
            End If
            Me.gvGrados.DataBind()
            obj.CerrarConexion()
            obj = Nothing
        Catch ex As Exception
            'Response.Write(ex.Message)
        End Try
       
    End Sub
    Protected Sub btnAgregar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAgregar.Click
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        obj.Ejecutar("FAD_RegistrarGrado", Me.ddlDocente.SelectedValue, Me.ddlPrograma.SelectedValue, Me.ddlUniversidad.SelectedValue, Me.txtProgramaExtranjero.Text, Me.txtUniversidadExtranjero.Text, Request.QueryString("id"))
        obj.CerrarConexion()
        obj = Nothing
        limpiar()
        CargarGrados()
    End Sub


    Protected Sub gvGrados_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles gvGrados.RowDeleting
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        obj.Ejecutar("FAD_BorrarGrado", gvGrados.DataKeys(e.RowIndex).Values("codigo_gra"), Request.QueryString("id"))
        obj.CerrarConexion()
        obj = Nothing
        CargarGrados()
    End Sub

   
    Protected Sub ddlDocente_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlDocente.SelectedIndexChanged
        Try
            limpiar()
            CargarGrados()
        Catch ex As Exception
        End Try
    End Sub

   
End Class
