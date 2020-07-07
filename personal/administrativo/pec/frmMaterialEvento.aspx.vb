
Partial Class administrativo_pec2_frmMaterialEvento
    Inherits System.Web.UI.Page

    
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load        
        If IsPostBack = False Then
            CargaComboMaterial()
            If (Request.QueryString("cco") <> Nothing) Then
                Me.Hdcodigo_cco.Value = Request.QueryString("cco")                
                Me.btnRefrescar.Attributes.Add("onclick", "location.reload();")
                Me.txtFecha.Attributes.Add("onkeyup", "mascara(this,'/',patron,true)")
                CargaGrid()
                VerificaCheckPermisos()
            End If
        End If
    End Sub

    Private Sub ActualizaEstadoPermiso(ByVal estado As Boolean)
        Try
            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("cnxBDUSAT").ConnectionString
            obj.AbrirConexion()
            obj.Ejecutar("EVE_ActualizaMaterialesEventos", Me.Hdcodigo_cco.Value, estado)
            obj.CerrarConexion()
            obj = Nothing
        Catch ex As Exception
            Response.Write("Error al actualizar permisos. " & ex.Message)
        End Try
    End Sub

    Private Sub VerificaCheckPermisos()
        Try
            Dim obj As New ClsConectarDatos
            Dim dt As New Data.DataTable
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("cnxBDUSAT").ConnectionString
            obj.AbrirConexion()
            dt = obj.TraerDataTable("EVE_ConsultaPermisosEvento", Me.Hdcodigo_cco.Value)
            obj.CerrarConexion()

            If ((dt.Rows(0).Item("entregaMaterial_dev").ToString = "") _
                    Or (dt.Rows(0).Item("entregaMaterial_dev").ToString = "false")) Then
                Me.chkPermiso.Checked = False
            Else
                Me.chkPermiso.Checked = True
            End If

            obj = Nothing
            dt.Dispose()
        Catch ex As Exception
            Response.Write("Error al verificar permisos")
        End Try
    End Sub

    Private Function ValidaSiExiste(ByVal codigo_mat As Integer, ByVal codigo_cco As Integer, ByVal fecha As String) As Boolean
        Try
            Dim obj As New ClsConectarDatos
            Dim dt As New Data.DataTable
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("cnxBDUSAT").ConnectionString
            obj.AbrirConexion()
            dt = obj.TraerDataTable("EVE_ValidaMaterialesxEvento", codigo_cco, codigo_mat, fecha)
            obj.CerrarConexion()
            obj = Nothing

            If (dt.Rows.Count = 0) Then
                Return True
            End If
            Return False
        Catch ex As Exception
            Return False
        End Try
    End Function

    Protected Sub cmdAgregar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdAgregar.Click
        Try            
            If (validaForm() = True) Then
                If (ValidaSiExiste(Me.cboMaterial.SelectedValue, Me.Hdcodigo_cco.Value, txtFecha.Text)) Then
                    If (Me.chkPermiso.Checked = True) Then
                        ActualizaEstadoPermiso(1)
                    Else
                        ActualizaEstadoPermiso(0)
                    End If

                    Dim obj As New ClsConectarDatos
                    obj.CadenaConexion = ConfigurationManager.ConnectionStrings("cnxBDUSAT").ConnectionString
                    obj.AbrirConexion()
                    obj.Ejecutar("EVE_AgregaMaterialEvento", Me.cboMaterial.SelectedValue, Me.Hdcodigo_cco.Value, Me.txtFecha.Text)
                    obj.CerrarConexion()
                    CargaGrid()
                    VerificaCheckPermisos()
                End If
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub CargaGrid()
        Dim obj As New ClsConectarDatos
        Dim dtMateriales As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("cnxBDUSAT").ConnectionString
        obj.AbrirConexion()
        dtMateriales = obj.TraerDataTable("EVE_BuscaMaterialEvento", Me.Hdcodigo_cco.Value)
        Me.gvMaterialEvento.DataSource = dtMateriales        
        Me.gvMaterialEvento.DataBind()
        obj.CerrarConexion()
        dtMateriales.Dispose()        
    End Sub

    Private Function validaForm() As Boolean
        If (Me.cboMaterial.SelectedValue.Trim = "") Then
            Response.Write("<script>alert('Debe seleccionar un Material')</script>")
            Me.cboMaterial.Focus()            
            Return False
        End If

        If (Me.txtFecha.Text.Length = 10) Then
            If (Me.txtFecha.Text.Trim = "") Then
                Response.Write("<script>alert('Debe ingresar una fecha')</script>")
                Me.txtFecha.Focus()
                Return False
            End If
        End If

        Try
            Date.Parse(Me.txtFecha.Text)            
        Catch ex As Exception
            Me.txtFecha.Text = ""
            Response.Write("<script>alert('Formato de fecha incorrecto')</script>")
            Return False
        End Try

        Return True
    End Function

    Private Sub CargaComboMaterial()
        Dim obj As New ClsConectarDatos
        Dim dtMaterial As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("cnxBDUSAT").ConnectionString
        obj.AbrirConexion()
        dtMaterial = obj.TraerDataTable("EVE_BuscaMaterial", 0, "")
        Me.cboMaterial.DataSource = dtMaterial
        Me.cboMaterial.DataValueField = "codigo_Mat"
        Me.cboMaterial.DataTextField = "titulo_Mat"
        Me.cboMaterial.DataBind()
        obj.CerrarConexion()
        dtMaterial.Dispose()
    End Sub

    Protected Sub gvMaterialEvento_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles gvMaterialEvento.RowDeleting        
        Dim obj As New ClsConectarDatos
        Try            
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            obj.Ejecutar("EVE_EliminaMaterialEvento", Me.gvMaterialEvento.DataKeys(e.RowIndex).Item(0).ToString)
            obj.CerrarConexion()
            CargaGrid()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub cmdRptEntrega_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdRptEntrega.Click                
        Response.Redirect(" " & ConfigurationManager.AppSettings("RutaReporte") & "PRIVADOS/EVENTOS/LOG_RptEntregaMaterial&mod=" & Request.QueryString("mod") & "&ctf=" & Request.QueryString("ctf") & "&id=" & Request.QueryString("id") & "&codigo_cco=" & Me.Hdcodigo_cco.Value)
        'Response.Redirect("../../../../reportServer/?/PRIVADOS/EVENTOS/LOG_RptEntregaMaterial&codigo_cco=" & Me.Hdcodigo_cco.Value)
    End Sub

    Protected Sub cmdRptEntregaResumen_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdRptEntregaResumen.Click
        Response.Redirect(" " & ConfigurationManager.AppSettings("RutaReporte") & "PRIVADOS/EVENTOS/LOG_RptEntregaMaterialResumen&mod=" & Request.QueryString("mod") & "&ctf=" & Request.QueryString("ctf") & "&id=" & Request.QueryString("id") & "&codigo_cco=" & Me.Hdcodigo_cco.Value)
        'PostBackUrl = "../../../rptusat/?/PRIVADOS/ACADEMICO/IND_GraficoBarrasUnIndicador"
    End Sub

End Class
