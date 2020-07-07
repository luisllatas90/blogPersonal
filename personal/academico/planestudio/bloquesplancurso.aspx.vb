
Partial Class academico_cargalectiva_bloquesplancurso
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            CargarCarrera()
            CargarPlanEstudio()
        End If


    End Sub
    Sub CargarCarrera()
        Dim codigo_tfu As Int16 = Request.QueryString("ctf")
        Dim codigo_usu As Integer = Request.QueryString("id")
        Dim Modulo As Integer = Request.QueryString("mod")
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()

        Dim objFun As New ClsFunciones
        objFun.CargarListas(Me.ddlCarreraProfesional, obj.TraerDataTable("EVE_ConsultarCarreraProfesional", Modulo, codigo_tfu, codigo_usu), "codigo_cpf", "nombre_cpf")
        objFun = Nothing
        obj.CerrarConexion()

        obj = Nothing
    End Sub
    Sub CargarPlanEstudio()
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()

        Dim objFun As New ClsFunciones
        objFun.CargarListas(Me.ddlPlanEstudio, obj.TraerDataTable("ConsultarPlanEstudio", "AC", Me.ddlCarreraProfesional.selectedvalue, ""), "codigo_pes", "descripcion_pes")
        objFun = Nothing
        obj.CerrarConexion()

        obj = Nothing
    End Sub
    Sub CargarData()
        Try
            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            Dim pes As Integer = 0
            If Me.ddlPlanEstudio.SelectedValue = "" Then
                pes = 0
            Else
                pes = Me.ddlPlanEstudio.SelectedValue
            End If
            Me.dgvData.DataSource = obj.TraerDataTable("HConsultar_planCursoBloquesAsignados", Me.ddlCarreraProfesional.SelectedValue, pes, Me.ddlCicloEstudios.SelectedValue)
            Me.dgvData.DataBind()
            obj.CerrarConexion()

            obj = Nothing
        Catch ex As Exception
            Response.Write(ex.Message & "<br>")
            Response.Write(Me.ddlCarreraProfesional.SelectedValue & "," & Me.ddlPlanEstudio.SelectedValue & "," & Me.ddlCicloEstudios.SelectedValue)
        End Try
        
    End Sub


    Protected Sub ddlCarreraProfesional_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlCarreraProfesional.SelectedIndexChanged
        CargarPlanEstudio()
        CargarData()
    End Sub

    Protected Sub ddlPlanEstudio_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPlanEstudio.SelectedIndexChanged
        CargarData()
    End Sub

    Protected Sub ddlCicloEstudios_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlCicloEstudios.SelectedIndexChanged
        CargarData()
    End Sub

    Protected Sub dgvData_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgvData.SelectedIndexChanged
        Try
            Me.txtNroHoras.Text = ""
            Me.lblMensaje.Text = ""
            Me.lblCiclo.Text = dgvData.SelectedRow.Cells(2).Text
            Me.lblCurso.Text = dgvData.SelectedRow.Cells(3).Text
            Me.lblNroBloque.Text = CInt(dgvData.SelectedRow.Cells(6).Text) + 1
            Me.tdHeader.Attributes.Remove("class")
            Me.tdHeader.Attributes.Add("class", "Mostrar")
            Me.tdContent.Attributes.Remove("class")
            Me.tdContent.Attributes.Add("class", "Mostrar")
            Me.tdFooter.Attributes.Remove("class")
            Me.tdFooter.Attributes.Add("class", "Mostrar")
            Me.txtNroHoras.Focus()
            Me.hdTotalHoras.Value = dgvData.SelectedRow.Cells(4).Text
            Me.hdHorasAsignadas.Value = dgvData.SelectedRow.Cells(5).Text

        Catch ex As Exception

        End Try


    End Sub

    Protected Sub btnRegistrar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRegistrar.Click
        Try
            Dim maxh As Integer = 0
            Me.hdTotalHoras.Value = dgvData.SelectedRow.Cells(4).Text
            Me.hdHorasAsignadas.Value = dgvData.SelectedRow.Cells(5).Text
            maxh = CInt(Me.hdTotalHoras.Value) - CInt(Me.hdHorasAsignadas.Value)
            If CInt(Me.txtNroHoras.Text) > maxh Then
                Me.lblMensaje.Text = "Aviso: No puede sobrepasar el límite de horas por curso. ( Max. " & Me.hdTotalHoras.Value & " horas) "
            Else
                Me.lblMensaje.Text = ""
                Dim obj As New ClsConectarDatos
                obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
                obj.AbrirConexion()
                obj.Ejecutar("Hregistrar_planCursoBloques", dgvData.DataKeys(dgvData.SelectedIndex).Values("codigo_Pes"), dgvData.DataKeys(dgvData.SelectedIndex).Values("codigo_Cur"), CInt(Me.txtNroHoras.Text), 0)
                obj.CerrarConexion()
                CargarData()
                Me.gvBloque.DataBind()
                obj = Nothing
            End If
        Catch ex As Exception

        End Try


    End Sub


    Protected Sub gvBloque_RowDeleted(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeletedEventArgs) Handles gvBloque.RowDeleted
        Me.lblMensaje.Text = ""
        Me.hdTotalHoras.Value = dgvData.SelectedRow.Cells(4).Text
        Me.hdHorasAsignadas.Value = dgvData.SelectedRow.Cells(5).Text
        CargarData()
    End Sub

    'Protected Sub gvBloque_RowUpdated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewUpdatedEventArgs) Handles gvBloque.RowUpdated
    '    CargarData()
    '    Me.lblMensaje.Text = ""
    '    Me.hdTotalHoras.Value = dgvData.SelectedRow.Cells(4).Text
    '    Me.hdHorasAsignadas.Value = dgvData.SelectedRow.Cells(5).Text
    'End Sub
End Class
