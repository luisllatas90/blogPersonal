
Partial Class PlanProyecto_frmRegistraPlan
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (IsPostBack = False) Then
            MuestraFiltroBusqueda(False)
            cargaCentros()
        End If
    End Sub

    Private Sub cargaCentros()
        Dim objpre As New ClsPresupuesto
        Dim objfun As New ClsFunciones
        Dim datos As New Data.DataTable
        datos = objpre.ObtenerListaCentroCostos("1", 523, Request.QueryString("id"))
        objfun.CargarListas(Me.dpCentroCosto, datos, "codigo_Cco", "descripcion_Cco", ">> Seleccione<<")
    End Sub

    Private Sub CargaGridPersonal(ByVal strBusqueda As String)
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        Dim dtPersonal As New Data.DataTable
        obj.AbrirConexion()
        dtPersonal = obj.TraerDataTable("PLAN_BuscaPersonal", Me.txtFiltro.Text.Trim, Me.txtFiltro.Text.Replace(" ", "%"))
        obj.CerrarConexion()
        Me.gvPersonal.DataSource = dtPersonal
        Me.gvPersonal.DataBind()
        dtPersonal.Dispose()
        obj = Nothing
    End Sub

    Protected Sub btnBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBuscar.Click
        Me.txtFiltro.Text = Me.txtPersonal.Text
        MuestraFiltroBusqueda(True)
    End Sub

    Private Sub MuestraFiltroBusqueda(ByVal sw As Boolean)
        Me.txtFiltro.Visible = sw
        Me.lblFiltro.Visible = sw
        Me.btnFiltro.Visible = sw
        Me.gvPersonal.Visible = sw
        Me.btnBuscar.Visible = Not sw
    End Sub

    Protected Sub btnFiltro_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFiltro.Click
        If (Me.txtFiltro.Text.Trim <> "") Then
            CargaGridPersonal(Me.txtFiltro.Text)
        Else
            Me.lblMensaje.Text = "Debe ingresar un fitro de busqueda"
            Me.txtFiltro.Focus()
        End If
    End Sub

    Protected Sub gvPersonal_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gvPersonal.PageIndexChanging
        gvPersonal.PageIndex = e.NewPageIndex()
        CargaGridPersonal(Me.txtFiltro.Text)
    End Sub

    Protected Sub gvPersonal_SelectedIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSelectEventArgs) Handles gvPersonal.SelectedIndexChanging
        Me.HdCodigo_per.Value = gvPersonal.DataKeys(e.NewSelectedIndex).Values(0)
        Me.txtPersonal.Text = gvPersonal.Rows(e.NewSelectedIndex).Cells(1).Text
        MuestraFiltroBusqueda(False)
        Me.gvPersonal.DataSource = Nothing
        Me.gvPersonal.DataBind()
    End Sub

    Private Function validaFormulario() As Boolean        
        If (Me.txtTitulo.Text.Trim = "") Then
            Me.lblMensaje.Text = "Ingrese el titulo del plan"
            Me.txtTitulo.Focus()
            Return False
        End If

        If (Me.txtDescripcion.Text.Trim = "") Then
            Me.lblMensaje.Text = "Ingrese la descripción del plan"
            Me.txtDescripcion.Focus()
            Return False
        End If

        If (Me.txtInicio.Text.Trim = "") Then
            Me.lblMensaje.Text = "Ingrese la fecha de inicio"
            Me.txtInicio.Focus()
            Return False
        End If

        If (Me.txtFinal.Text.Trim = "") Then
            Me.lblMensaje.Text = "Ingrese la fecha final"
            Me.txtFinal.Focus()
            Return False
        End If

        If (Me.dpCentroCosto.SelectedValue = 0) Then
            Me.lblMensaje.Text = "Ingrese el centro de costo"
            Me.dpCentroCosto.Focus()
            Return False
        End If

        If (Me.HdCodigo_per.Value = "") Then
            Me.lblMensaje.Text = "Seleccione al responsable del plan"
            Me.txtPersonal.Text = ""
            Me.txtPersonal.Focus()
            Return False
        End If

        Me.lblMensaje.Text = ""
        Return True
    End Function

    Protected Sub btnGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardar.Click
        Try
            If (validaFormulario() = True) Then
                Dim Codigo As String = "0"
                If (Me.HdCodigo_pro.Value <> "") Then
                    Codigo = Me.HdCodigo_pro.Value
                End If

                Dim obj As New ClsConectarDatos
                obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
                obj.AbrirConexion()
                obj.Ejecutar("PLAN_RegistraProyecto", Codigo, Me.HdCodigo_per.Value, Me.txtTitulo.Text, _
                             Me.txtDescripcion.Text, Me.txtInicio.Text, Me.txtFinal.Text, _
                             Me.dpCentroCosto.SelectedValue, Session("id_per"))
                obj.CerrarConexion()
                obj = Nothing

                Me.lblMensaje.Text = "Registro Guardado."
            End If
        Catch ex As Exception
            Me.lblMensaje.Text = "Error al guardar el Plan"
        End Try        
    End Sub

    Protected Sub btnCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        LimpiaControles()
    End Sub

    Private Sub LimpiaControles()
        Me.txtTitulo.Text = ""
        Me.txtDescripcion.Text = ""
        Me.txtInicio.Text = ""
        Me.txtFinal.Text = ""
        Me.txtPersonal.Text = ""
        Me.dpCentroCosto.SelectedIndex = -1
    End Sub
End Class
