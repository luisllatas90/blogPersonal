
Partial Class administrativo_SISREQ_SisSolicitudes_FrmConfiguraSolicitud
    Inherits System.Web.UI.Page

    Public Enum MessageType
        Success
        [Error]
        Info
        Warning
    End Enum

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (Session("id_per") Is Nothing) Then
            Response.Redirect("../../../../sinacceso.html")
        End If

        If (IsPostBack = False) Then
            CargaTipoEstudio()
            CargaTipoEstudioForm()
            CargaTipoPersonal()
            If (cboTipoEstudio.Items.Count > 0) Then
                CargarGrid()   'Por defecto cargar pregrado
            End If
        End If
    End Sub

    Protected Sub ShowMessage(ByVal Message As String, ByVal type As MessageType)
        Page.RegisterStartupScript("Mensaje", "<script>ShowMessage('" & Message & "','" & type & "');</script>")
    End Sub

    Private Sub CargaTipoEstudio()
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        Try
            obj.AbrirConexion()
            dt = obj.TraerDataTable("ACAD_ConsultarTipoEstudio", "TO", 0)
            obj.CerrarConexion()

            Me.cboTipoEstudio.DataSource = dt
            Me.cboTipoEstudio.DataTextField = "descripcion_test"
            Me.cboTipoEstudio.DataValueField = "codigo_test"
            Me.cboTipoEstudio.DataBind()

            If (cboTipoEstudio.Items.Count > 0) Then
                Me.cboTipoEstudio.SelectedValue = 2
                CargaCarreraForm(2)
            End If
        Catch ex As Exception
            ShowMessage("Error: " & ex.Message.Replace("'", ""), MessageType.Error)
        End Try
    End Sub

    Private Sub CargaTipoEstudioForm()
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        Try
            obj.AbrirConexion()
            dt = obj.TraerDataTable("ACAD_ConsultarTipoEstudio", "TO", 0)
            obj.CerrarConexion()

            Me.cboTipoEstudio2.DataSource = dt
            Me.cboTipoEstudio2.DataTextField = "descripcion_test"
            Me.cboTipoEstudio2.DataValueField = "codigo_test"
            Me.cboTipoEstudio2.DataBind()

            If (cboTipoEstudio2.Items.Count > 0) Then
                Me.cboTipoEstudio2.SelectedValue = 2
            End If
        Catch ex As Exception
            ShowMessage("Error: " & ex.Message.Replace("'", ""), MessageType.Error)
        End Try
    End Sub

    Protected Sub btnBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBuscar.Click
        If (cboTipoEstudio.Items.Count > 0) Then
            CargarGrid()
        End If
    End Sub

    Protected Sub btnAgregar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAgregar.Click
        Page.RegisterStartupScript("Pop", "<script>openModal();</script>")

    End Sub

    Protected Sub cboTipoEstudio_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboTipoEstudio.SelectedIndexChanged

    End Sub

    Private Sub CargarGrid()
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        Try
            obj.AbrirConexion()
            dt = obj.TraerDataTable("SOL_ConfiguraSolicitud", Me.cboTipoEstudio.SelectedValue, Me.txtDescripcion.Text)
            obj.CerrarConexion()

            Me.gvCarreras.DataSource = dt
            Me.gvCarreras.DataBind()
        Catch ex As Exception
            ShowMessage("Error: " & ex.Message.Replace("'", ""), MessageType.Error)
        End Try
    End Sub

    Protected Sub cboTipoEstudio2_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboTipoEstudio2.SelectedIndexChanged
        If (Me.cboTipoEstudio2.Items.Count > 0) Then
            CargaCarreraForm(Me.cboTipoEstudio2.SelectedValue)        
        End If
        Page.RegisterStartupScript("Pop", "<script>openModal();</script>")
    End Sub

    Protected Sub cboTipoPersonal_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboTipoPersonal.SelectedIndexChanged
        If (Me.cboTipoPersonal.Items.Count > 0) Then
            CargaPersonal(Me.cboTipoPersonal.SelectedValue)
        End If
        Page.RegisterStartupScript("Pop", "<script>openModal();</script>")
    End Sub

    Private Sub CargaCarreraForm(ByVal TipoEstudio As Integer)
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        Try
            obj.AbrirConexion()
            dt = obj.TraerDataTable("ACAD_BuscaEscuelaProfesional_v2", "TO", 0, TipoEstudio, "")
            obj.CerrarConexion()

            Me.cboCarrera.DataSource = dt
            Me.cboCarrera.DataTextField = "nombre_Cpf"
            Me.cboCarrera.DataValueField = "codigo_Cpf"
            Me.cboCarrera.DataBind()
        Catch ex As Exception
            ShowMessage("Error: " & ex.Message.Replace("'", ""), MessageType.Error)
        End Try        
    End Sub

    Private Sub CargaTipoPersonal()
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        Try
            obj.AbrirConexion()
            dt = obj.TraerDataTable("ConsultarTipoPersonal", "TO", "")
            obj.CerrarConexion()

            Me.cboTipoPersonal.DataSource = dt
            Me.cboTipoPersonal.DataTextField = "descripcion_Tpe"
            Me.cboTipoPersonal.DataValueField = "codigo_Tpe"
            Me.cboTipoPersonal.DataBind()

            If (Me.cboTipoPersonal.Items.Count > 0) Then
                Me.cboTipoPersonal.SelectedIndex = 0
                CargaPersonal(Me.cboTipoPersonal.SelectedValue)
            End If
        Catch ex As Exception
            ShowMessage("Error: " & ex.Message.Replace("'", ""), MessageType.Error)
        End Try
    End Sub

    Public Sub CargaPersonal(ByVal TipoPersonal As Integer)
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        Try
            obj.AbrirConexion()
            dt = obj.TraerDataTable("SOL_ConsultarPersonalActivo", TipoPersonal)
            obj.CerrarConexion()

            Me.cboPersonal.DataSource = dt
            Me.cboPersonal.DataTextField = "personal"
            Me.cboPersonal.DataValueField = "codigo_per"
            Me.cboPersonal.DataBind()
        Catch ex As Exception
            ShowMessage("Error: " & ex.Message.Replace("'", ""), MessageType.Error)
        End Try
    End Sub    

    Protected Sub btnGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardar.Click
        Dim obj As New ClsConectarDatos        
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        Try
            obj.AbrirConexion()
            obj.Ejecutar("SOL_InsertaCarreraSolicitud", Me.cboCarrera.SelectedValue, Me.cboPersonal.SelectedValue, Request.QueryString("id"))
            obj.CerrarConexion()

            ShowMessage("Registro guardado", MessageType.Success)
            CargarGrid()
        Catch ex As Exception
            ShowMessage("Error: " & ex.Message.Replace("'", ""), MessageType.Error)
        End Try
    End Sub
End Class
