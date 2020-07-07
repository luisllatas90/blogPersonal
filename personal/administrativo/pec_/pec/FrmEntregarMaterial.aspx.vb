
Partial Class administrativo_pec_FrmEntregarMaterial
    Inherits System.Web.UI.Page

    Private Sub CargaCentroCostos()
        Dim objfun As New ClsFunciones
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        objfun.CargarListas(Me.ddpCentroCostos, obj.TraerDataTable("EVE_ConsultarCentroCostosXPermisos", Request.QueryString("ctf"), Request.QueryString("id"), "", Request.QueryString("mod")), "codigo_Cco", "Nombre", ">> Seleccione<<")
        obj.CerrarConexion()
        obj = Nothing
    End Sub

    Private Sub VerificaCheckPermisos()
        Try
            Dim obj As New ClsConectarDatos
            Dim dt As New Data.DataTable
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("cnxBDUSAT").ConnectionString
            obj.AbrirConexion()
            dt = obj.TraerDataTable("EVE_ConsultaPermisosEvento", Me.ddpCentroCostos.SelectedValue)
            obj.CerrarConexion()

            If (dt.Rows(0).Item("entregaMaterial_dev") = True) Then                
                Me.HdPermisos.Value = True
            Else
                Me.HdPermisos.Value = False
            End If

            obj = Nothing
            dt.Dispose()
        Catch ex As Exception
            Response.Write("Error al verificar permisos")
        End Try
    End Sub

    Private Sub CargaDocumento()
        Me.ddpDocumento.Items.Add("DNI")
        Me.ddpDocumento.Items.Add("PASAPORTE")
        Me.ddpDocumento.Items.Add("CARNE EXTRANJERIA")
    End Sub

    Private Sub CargaMateriales(ByVal codigo_cco As Integer, ByVal fecha As Date)
        Try            
            Dim obj As New ClsConectarDatos
            Dim dt As New Data.DataTable
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            dt = obj.TraerDataTable("EVE_ConsultarMaterialesEvento", codigo_cco, fecha)
            obj.CerrarConexion()
            obj = Nothing

            Me.gvMateriales.DataSource = dt
            Me.gvMateriales.DataBind()
            dt.Dispose()

        Catch ex As Exception
            Me.lblAviso.Text = "Error al cargar materiales del evento."
        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (IsPostBack = False) Then
            Me.txtDocumento.Attributes.Add("onkeyup", "seleccionaFoco()")
            Me.calFecha.SelectedDate = Date.Today.ToString()
            CargaCentroCostos()
            CargaDocumento()
            CargaCicloActual()
        End If        
        Me.txtDocumento.Focus()
    End Sub

    Private Sub CargaCicloActual()
        Dim obj As New ClsConectarDatos                
        Dim dt_Cac As New Data.DataTable        
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        dt_Cac = obj.TraerDataTable("EVE_CicloAcademicoActual")
        obj.CerrarConexion()

        If (dt_Cac.Rows.Count > 0) Then
            Me.Hd_CicloAcademico.Value = dt_Cac.Rows(0).Item("codigo_Cac")
        End If
    End Sub

    Protected Sub ddpCentroCostos_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddpCentroCostos.SelectedIndexChanged
        If (Me.ddpCentroCostos.SelectedIndex <> -1) Then
            CargaMateriales(Me.ddpCentroCostos.SelectedValue, Me.calFecha.SelectedDate)
        End If
    End Sub

    Protected Sub calFecha_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles calFecha.SelectionChanged
        If (Me.ddpCentroCostos.SelectedIndex <> -1) Then
            CargaMateriales(Me.ddpCentroCostos.SelectedValue, Me.calFecha.SelectedDate)
        End If
        'Me.txtDocumento.Focus()
    End Sub

    Protected Sub btnEntregar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEntregar.Click
        Try
            'If (Me.txtDocumento.Text.Trim <> "") Then            
            If (Me.gvMateriales.Rows.Count > 0) Then                
                Dim obj As New ClsConectarDatos
                Dim dtPersona As New Data.DataTable
                obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
                obj.AbrirConexion()
                dtPersona = obj.TraerDataTable("EVE_BuscaPersonaxDocumento", Me.txtDocumento.Text)
                obj.CerrarConexion()

                If (dtPersona.Rows.Count = 0) Then
                    Me.lblAviso.ForeColor = Drawing.Color.Red
                    Me.lblAviso.Text = "No se encontro a la persona con DNI: " & Me.txtDocumento.Text
                    Me.lblGuardo.Text = ""
                Else
                    Dim cod_pso As Integer
                    cod_pso = Integer.Parse(dtPersona.Rows(0).Item("codigo_pso").ToString())
                    'If (Me.ddpCentroCostos.SelectedIndex <> -1) Then

                    'End If

                    Dim dt_Deuda As New Data.DataTable
                    Dim blnExisteDeuda As Boolean = False
                    obj.AbrirConexion()
                    dt_Deuda = obj.TraerDataTable("EVE_BuscaDeudaPersonaPagoWeb", cod_pso, Me.Hd_CicloAcademico.Value, Me.ddpCentroCostos.SelectedValue)
                    obj.CerrarConexion()

                    If (dt_Deuda.Rows.Count = 0) Then
                        Me.lblAviso.Text = "No se encontro inscrita en este curso"
                        Me.lblAviso.ForeColor = Drawing.Color.Red
                        Me.lblGuardo.Text = ""
                        blnExisteDeuda = True
                    Else
                        For i As Integer = 0 To dt_Deuda.Rows.Count - 1
                            If (Double.Parse(dt_Deuda.Rows(i).Item("saldo_Deu").ToString) > 0) And (Double.Parse(dt_Deuda.Rows(i).Item("montopagoweb").ToString) = 0) Then
                                blnExisteDeuda = True
                                Me.lblAviso.Font.Size = 20
                                Me.lblAviso.ForeColor = Drawing.Color.Green
                                Me.lblAviso.Text = "Tiene Deuda Pendiente, debe ir a caja a cancelar.<br/>" & dtPersona.Rows(0).Item("NombreCompleto") & " - " & Me.txtDocumento.Text
                                Me.lblGuardo.Text = ""
                                'Me.txtDocumento.Focus()
                            End If
                        Next
                    End If

                    VerificaCheckPermisos()
                    If (blnExisteDeuda = False And Me.HdPermisos.Value = True) Then
                        For i As Integer = 0 To Me.gvMateriales.Rows.Count - 1
                            Dim Fila As GridViewRow
                            Fila = Me.gvMateriales.Rows(i)
                            Dim valor As Boolean = CType(Fila.FindControl("chkElegir"), CheckBox).Checked

                            If (valor = True) Then
                                Dim dtMaterial As New Data.DataTable
                                obj.AbrirConexion()
                                dtMaterial = obj.TraerDataTable("EVE_VerificaEntregaMaterial", Me.ddpCentroCostos.SelectedValue, cod_pso, Me.gvMateriales.Rows(i).Cells(1).Text)
                                obj.CerrarConexion()

                                If (dtMaterial.Rows.Count = 0) Then
                                    obj.AbrirConexion()
                                    obj.Ejecutar("EVE_InsertaEntregaMaterial", Me.gvMateriales.Rows(i).Cells(1).Text, cod_pso, "")
                                    obj.CerrarConexion()


                                    Me.lblGuardo.Text = "Registro guardado.<br/>" & dtPersona.Rows(0).Item("NombreCompleto") & " - " & Me.txtDocumento.Text
                                    Me.lblAviso.Text = ""
                                    Me.txtDocumento.Text = ""

                                Else
                                    Me.lblAviso.Text = "Ya se le entregó material"
                                    Me.lblAviso.Font.Size = 20
                                    Me.lblAviso.ForeColor = Drawing.Color.Red
                                    Me.lblGuardo.Text = ""
                                End If
                            End If
                        Next
                    End If
                End If
                dtPersona.Dispose()
                obj = Nothing
            Else
                Me.lblGuardo.Text = ""
                Me.lblAviso.Text = "No existen materiales para este evento"
                Me.lblAviso.ForeColor = Drawing.Color.Red
            End If
            'Else
            'Me.lblAviso.Text = "Debe ingresar el DNI del participante"
            'Me.lblAviso.ForeColor = Drawing.Color.Red
            'End If
            Me.txtDocumento.Text = ""
            'txtDocumento.Focus()
        Catch ex As Exception
            Me.lblGuardo.Text = ""
            Me.lblAviso.Text = "Error al guardar entrega de material " & ex.Message
            Me.lblAviso.ForeColor = Drawing.Color.Red
        End Try
    End Sub
End Class
