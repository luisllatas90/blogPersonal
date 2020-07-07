
Partial Class administrativo_pec2_frmMaterialEvento
    Inherits System.Web.UI.Page

    
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load        
        If IsPostBack = False Then
            CargaComboMaterial()
            If (Request.QueryString("cco") <> Nothing) Then
                Me.Hdcodigo_cco.Value = Request.QueryString("cco")
                CargaGrid()
            End If
        End If
    End Sub

    Protected Sub cmdAgregar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdAgregar.Click
        Try            
            If (validaForm() = True) Then
                Dim obj As New ClsConectarDatos
                obj.CadenaConexion = ConfigurationManager.ConnectionStrings("cnxBDUSAT").ConnectionString
                obj.AbrirConexion()
                obj.Ejecutar("EVE_AgregaMaterialEvento", Me.cboMaterial.SelectedValue, Me.Hdcodigo_cco.Value, Me.txtFecha.Text)
                obj.CerrarConexion()
                CargaGrid()
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
        Else
            Response.Write("<script>alert('Formato de Fecha Incorrecto')</script>")
            Me.txtFecha.Focus()
            Return False
        End If

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
        Response.Redirect("../../rptusat/?/PRIVADOS/EVENTOS/LOG_RptEntregaMaterial&mod=" & Request.QueryString("mod") & "&ctf=" & Request.QueryString("ctf") & "&id=" & Request.QueryString("id") & "&codigo_cco=" & Me.Hdcodigo_cco.Value)
    End Sub

    Protected Sub cmdRptEntregaResumen_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdRptEntregaResumen.Click
        Response.Redirect("../../rptusat/?/PRIVADOS/EVENTOS/LOG_RptEntregaMaterialResumen&mod=" & Request.QueryString("mod") & "&ctf=" & Request.QueryString("ctf") & "&id=" & Request.QueryString("id") & "&codigo_cco=" & Me.Hdcodigo_cco.Value)
    End Sub
End Class
