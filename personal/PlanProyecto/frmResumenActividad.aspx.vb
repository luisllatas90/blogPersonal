
Partial Class PlanProyecto_frmResumen
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (IsPostBack = False) Then
            If (Session("id_per") IsNot Nothing) Then
                If (Request.QueryString("apr") IsNot Nothing) Then
                    Me.HdCodigo_apr.Value = Request.QueryString("apr")
                    AsignaActividad()
                    CargaDetalle(0, Me.HdCodigo_apr.Value)
                    Me.lblMensaje.Text = ""
                Else
                    Me.lblMensaje.Text = "No se encontró relación con ningun plan"
                End If
            Else
                Me.lblMensaje.Text = "Su sesión ha expirado"
                Me.btnEditar.Visible = False
                Me.btnEliminar.Visible = False
                'Response.Write("<script>window.close();</script>")
            End If
            
        End If
    End Sub

    Private Sub AsignaActividad()
        Dim obj As New ClsConectarDatos
        Dim dtActividad As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        dtActividad = obj.TraerDataTable("PLAN_BuscaActividadProyecto", Me.HdCodigo_apr.Value, 0, "")
        obj.CerrarConexion()
        If (dtActividad.Rows.Count > 0) Then
            Me.HdCodigo_pro.Value = dtActividad.Rows(0).Item("codigo_pro").ToString
            Me.txtTitulo.Text = dtActividad.Rows(0).Item("titulo_apr").ToString
            Me.txtDescripcion.Text = dtActividad.Rows(0).Item("descripcion_apr").ToString
            Me.txtFechaInicio.Text = dtActividad.Rows(0).Item("fechaInicio_apr").ToString
            Me.txtFechaFin.Text = dtActividad.Rows(0).Item("fechaFin_apr").ToString
            Me.dpPrioridad.SelectedValue = dtActividad.Rows(0).Item("prioridad_apr").ToString
            Me.txtAvance.Text = dtActividad.Rows(0).Item("avance_apr").ToString & "%"
            Me.txtOrden.Text = dtActividad.Rows(0).Item("nroOrden").ToString
            CargaProyecto(Me.HdCodigo_pro.Value)

            If (dtActividad.Rows(0).Item("proceso").ToString.ToUpper = "S") Then
                Me.chkProceso.Checked = True
            End If

            Me.chkDias.Checked = dtActividad.Rows(0).Item("mostrarDias_apr")
            Me.chkVisible.Checked = dtActividad.Rows(0).Item("visibilidad_apr")

            Dim dtDepende As New Data.DataTable            
            obj.AbrirConexion()
            dtActividad = obj.TraerDataTable("PLAN_BuscaActividadProyecto", 0, HdCodigo_pro.Value, "")
            obj.CerrarConexion()

            If (dtDepende.Rows.Count > 0) Then
                Me.txtDepende.Text = "Ninguno"
                For i As Integer = 0 To dtDepende.Rows.Count
                    If (dtDepende.Rows(i).Item("codigo_apr") = dtActividad.Rows(0).Item("codigoDepende")) Then
                        Me.txtDepende.Text = dtDepende.Rows(i).Item("titulo_apr")
                    End If
                Next
            End If
            dtActividad.Dispose()
            obj = Nothing

        End If
    End Sub

    Private Sub CargaProyecto(ByVal codigo_pro As Integer)
        Dim obj As New ClsConectarDatos
        Dim dtProyecto As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        dtProyecto = obj.TraerDataTable("PLAN_BuscaProyecto", codigo_pro, "", 0)
        If (dtProyecto.Rows.Count > 0) Then
            Me.lblProyecto.Text = dtProyecto.Rows(0).Item("titulo_pro")
        Else
            Me.lblMensaje.Text = "No se encontró relación con ningun plan"
        End If
        obj.CerrarConexion()
        obj = Nothing
        dtProyecto.Dispose()
    End Sub

    Private Sub CargaDetalle(ByVal codigo_res As Integer, ByVal codigo_apr As Integer)
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        Try
            Dim dtResponsables As Data.DataTable
            obj.AbrirConexion()
            dtResponsables = obj.TraerDataTable("PLAN_BuscaResponsables", codigo_res, codigo_apr)
            obj.CerrarConexion()
            gvDetalle.DataSource = dtResponsables
            gvDetalle.DataBind()
            dtResponsables.Dispose()
            obj = Nothing
        Catch ex As Exception
            Me.lblMensaje.Text = "Error al cargar la lista de responsables"
        End Try
    End Sub

    Private Function ReemplazaTildes(ByVal cadena As String) As String
        Dim NuevaCadena As String
        NuevaCadena = cadena
        NuevaCadena = NuevaCadena.Replace("&#193;", "A")
        NuevaCadena = NuevaCadena.Replace("&#201;", "E")
        NuevaCadena = NuevaCadena.Replace("&#205;", "I")
        NuevaCadena = NuevaCadena.Replace("&#211;", "O")
        NuevaCadena = NuevaCadena.Replace("&#217;", "U")
        NuevaCadena = NuevaCadena.Replace("&#218;", "U")
        NuevaCadena = NuevaCadena.Replace("&#209;", "Ñ")
        NuevaCadena = NuevaCadena.Replace("&amp;", "&")
        NuevaCadena = NuevaCadena.Replace("&#180;", "'")
        NuevaCadena = NuevaCadena.Replace("&quot;", "'")
        NuevaCadena = NuevaCadena.Replace("&#186;", "º")
        Return NuevaCadena
    End Function

    Private Sub LimpiaControles()
        Me.HdCodigo_apr.Value = ""        
        Me.txtAvance.Text = ""
        Me.txtDescripcion.Text = ""
        Me.txtFechaFin.Text = ""
        Me.txtFechaInicio.Text = ""                
        Me.txtTitulo.Text = ""
        Me.gvDetalle.DataSource = Nothing
        Me.gvDetalle.DataBind()        
    End Sub

    Protected Sub btnEditar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEditar.Click
        Response.Redirect("frmRegistraActividad.aspx?apr=" & Request.QueryString("apr"))
    End Sub

    Protected Sub btnEliminar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEliminar.Click
        EliminaActividad(Request.QueryString("apr"))
        Response.Write("<script>window.close();</script>")
    End Sub

    Private Sub EliminaActividad(ByVal codigo_apr As String)
        Try
            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString            
            obj.AbrirConexion()
            obj.TraerDataTable("PLAN_EliminaActividad", codigo_apr)
            obj.CerrarConexion()            
            obj = Nothing
        Catch ex As Exception
            Me.lblMensaje.Text = "Error al eliminar el registro"
        End Try
    End Sub
End Class
