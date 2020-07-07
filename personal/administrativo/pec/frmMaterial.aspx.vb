
Partial Class administrativo_pec2_frmMaterial
    Inherits System.Web.UI.Page

    Protected Sub cmdGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdGuardar.Click
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        Try
            If (ValidaIngreso() = True) Then
                If (Me.HdCodigo_Mat.Value = "0") Then   'Nuevo Registro
                    obj.AbrirConexion()
                    obj.Ejecutar("EVE_AgregarMaterial", Me.txtTitulo.Text, Me.txtDescripcion.Text, Me.cboTipo.SelectedValue)
                    obj.CerrarConexion()
                    Response.Write("<SCRIPT LANGUAGE=""JavaScript"">alert('Registro Guardado')</SCRIPT>")
                    LimpiaControles()
                End If

            End If
        Catch ex As Exception
            Response.Write("<SCRIPT LANGUAGE=""JavaScript"">alert('Error al guardar')</SCRIPT>")
            obj.CerrarConexion()
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub LimpiaControles()
        Me.txtTitulo.Text = ""
        Me.txtDescripcion.Text = ""        
    End Sub

    Private Function ValidaIngreso() As Boolean
        If (Me.txtTitulo.Text.Trim = "") Then
            Response.Write("<SCRIPT LANGUAGE=""JavaScript"">alert('Debe ingresar el titulo del material')</SCRIPT>")
            Me.txtTitulo.Focus()
            Return False
        End If

        If (Me.txtDescripcion.Text.Trim = "") Then
            Response.Write("<SCRIPT LANGUAGE=""JavaScript"">alert('Debe ingresar la descripcion del material')</SCRIPT>")
            Me.txtDescripcion.Focus()
            Return False
        End If

        If (Me.cboTipo.SelectedValue.Trim = "") Then
            Response.Write("<SCRIPT LANGUAGE=""JavaScript"">alert('Debe seleccionar el tipo de material')</SCRIPT>")
            Me.cboTipo.Focus()
            Return False
        End If

        Return True
    End Function

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (IsPostBack = False) Then
            CargaComboTipo()

            cmdCancelar.Attributes.Add("onclick", "~/administrativo/pec/frmMaterialEvento.getElementById('btnRefrescar').click();") '25/11

            Me.cmdCancelar.Attributes.Add("onclick", "self.parent.tb_remove();")

            If (Request.QueryString("cod_mat") = Nothing) Then
                Me.HdCodigo_Mat.Value = 0
            Else
                Me.HdCodigo_Mat.Value = Request.QueryString("cod_mat")
            End If
        End If
    End Sub

    Private Sub CargaComboTipo()
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        dt = obj.TraerDataTable("EVE_BuscaTipoMaterial", 0, "")
        obj.CerrarConexion()
        Me.cboTipo.DataSource = dt
        Me.cboTipo.DataTextField = "descripcion_TMa"
        Me.cboTipo.DataValueField = "codigo_TMa"
        Me.cboTipo.DataBind()
    End Sub

    Protected Sub cmdCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdCancelar.Click
        'Session("lstMat") = True

        'ClientScript.RegisterStartupScript(Me.GetType(), "alert", "frmMaterialEvento.getElementById(reloadPage();)")
    End Sub

End Class
