Partial Class frmpersona
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If IsPostBack = False Then
                cargarDatosPagina()
                CargarDatosPersonales()
                CargarDatosEgresado()
            End If
            LoadDJ()
        Catch ex As Exception
            Response.Redirect("sesion.aspx")
            'Response.Write(ex.Message & " -  " & ex.StackTrace)
        End Try

    End Sub
    Sub LoadDJ()
        If Request.Form("aceptaDj") IsNot Nothing Then
            If Request.Form("aceptaDj") = "NO" Then
                InsertaDJ()
                Registrar()
            End If
        End If
        VerificarDJ()
    End Sub

    Sub VerificarDJ()
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Dim dt As New Data.DataTable
        dt = obj.TraerDataTable("Alumni_AceptoDeclaracionJurada", Me.hdcodigo_pso.Value)

        If dt.Rows(0).Item("id") <> "0" Then
            Me.cmdGuardar.CssClass = "guardar"
            Me.aceptaDj.Attributes.Add("value", "SI")
        Else
            Me.lblnombre.Text = dt.Rows(0).Item("nombreEgresado")
            Me.aceptaDj.Attributes.Add("value", "NO")
            Me.cmdGuardar.CssClass = "guardar  CallFancyBox_DeclaracionJurada"
        End If

    End Sub
    Sub InsertaDJ()
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        obj.Ejecutar("ALUMNI_InsertaBitacoraUpdateDatos", Me.hdcodigo_pso.Value, "DJ")
        obj.CerrarConexion()
        obj = Nothing
    End Sub
    Sub cargarDatosPagina()
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        ClsFunciones.LlenarListas(Me.dpSector, obj.TraerDataTable("ALUMNI_BUSCARDATOSFE", 0, "S"), "codigo_sec", "nombre_sec", "--Seleccione--")
        'ClsFunciones.LlenarListas(Me.dpCargo, obj.TraerDataTable("ALUMNI_BUSCARDATOSFE", 0, "C"), "codigo_Cgo", "descripcion_Cgo", "--Seleccione--")
        ClsFunciones.LlenarListas(Me.dpTContrato, obj.TraerDataTable("ALUMNI_BUSCARDATOSFE", 0, "T"), "codigo_Tco", "descripcion_Tco", "--Seleccione--")
        ClsFunciones.LlenarListas(Me.dpcomunidad, obj.TraerDataTable("ALUMNI_BUSCARDATOSFE", 0, "D"), "codigo_Dep", "Nombre_Dep", "--Seleccione--")
        obj.CerrarConexion()
        obj = Nothing
    End Sub
    Sub CargarDatosPersonales()
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Dim tbl As Data.DataTable
        tbl = obj.TraerDataTable("ALUMNI_ConsultarDatosEgresado", Session("codigo_alu"))
        If tbl.Rows.Count Then
            Me.hdcodigo_pso.Value = tbl.Rows(0).Item("codigo_Pso")
        End If
    End Sub

    Sub CargarDatosEgresado()
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        Dim dt_Egresado As New Data.DataTable
        Dim dt_DEgresado As New Data.DataTable
        obj.AbrirConexion()
        dt_Egresado = obj.TraerDataTable("ALUMNI_BuscaDatosEgresado", Me.hdcodigo_pso.Value, "E")
        dt_DEgresado = obj.TraerDataTable("ALUMNI_BuscaDatosEgresado", Me.hdcodigo_pso.Value, "F")
        obj.CerrarConexion()
        obj = Nothing
        If (dt_DEgresado.Rows.Count > 0) Then
            Me.txtcargo.Text = dt_DEgresado.Rows(0).Item("cargodeseado_fe").ToString()
            Me.dpTContrato.SelectedValue = dt_DEgresado.Rows(0).Item("tipocontrato_fe").ToString
            Me.txtarea.Text = dt_DEgresado.Rows(0).Item("areapropuesta_fe").ToString()
            Me.dpcomunidad.SelectedValue = dt_DEgresado.Rows(0).Item("lugardeseado_fe").ToString
            Me.dpSector.SelectedValue = dt_DEgresado.Rows(0).Item("sectordeseado_fe").ToString
            Me.dpexpectativa.SelectedValue = dt_DEgresado.Rows(0).Item("expectativapago_fe").ToString
        Else
            Me.txtcargo.Text = ""
            Me.dpTContrato.SelectedValue = ""
            Me.txtarea.Text = ""
            Me.dpcomunidad.SelectedValue = ""
            Me.dpSector.SelectedValue = ""
            Me.dpexpectativa.SelectedValue = ""
        End If
        dt_DEgresado.Dispose()
        dt_Egresado.Dispose()
    End Sub

    Protected Sub cmdGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdGuardar.Click
        registrar()
    End Sub
    Sub registrar()
        Try
            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            obj.Ejecutar("ALUMNI_ActualizaFuturoEmpleo", Session("codigo_alu"), 1, Me.txtcargo.Text, Me.dpTContrato.SelectedValue, Me.txtarea.Text, Me.dpcomunidad.SelectedValue, Me.dpSector.SelectedValue, Me.dpexpectativa.SelectedValue)
            obj.CerrarConexion()
            Dim sector As Integer
            If (Me.dpSector.SelectedValue = "-1") Then
                sector = 17
            Else
                sector = Me.dpSector.SelectedValue
            End If
            Dim cargo As Integer
            'If (Me.dpCargo.SelectedValue = "-1") Then
            '    cargo = ""
            'Else
            '    cargo = Me.dpCargo.SelectedValue
            'End If
            Dim tcontrato As Integer
            If (Me.dpTContrato.SelectedValue = "-1") Then
                tcontrato = 0
            Else
                tcontrato = Me.dpTContrato.SelectedValue
            End If
            Dim comunidad As Integer
            If (Me.dpcomunidad.SelectedValue = "-1") Then
                comunidad = 0
            Else
                comunidad = Me.dpcomunidad.SelectedValue
            End If
            'Response.Write("<script>alert('Registro de Futuro empleo Actualizado'); location.reload('CVEgresado.aspx')</script>")
            'Response.Write("<script>alert('Registro de Futuro empleo Actualizado')</script>")
            Me.Label1.Text = "Futuro Empleo Registrado"
            CargarDatosPersonales()
            CargarDatosEgresado()
        Catch ex As Exception
            Response.Write("<br /> error" & ex.Message & " -  " & ex.StackTrace)
        End Try
    End Sub


End Class

