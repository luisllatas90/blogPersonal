
Partial Class PlanProyecto_frmRegistraActividad
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (IsPostBack = False) Then
            If (Session("tipo_pro") = "T") Then
                Response.Write("<script>window.close();</script>")
            Else
                If (Request.QueryString("apr") Is Nothing) Then
                    Me.HdAccion.Value = "N"
                    Me.btnAgregar.Visible = False
                    MostrarControles(False)
                    Me.btnCancelar.Attributes.Add("OnClick", "self.parent.tb_remove();")
                    If (Session("cod_pro") > 0) Then
                        CargaProyecto(Session("cod_pro"))
                        CargaDependientes(Session("cod_pro"))
                        Me.lblMensaje.Text = ""
                    Else
                        Me.lblMensaje.Text = "No se encontró relación con ningun plan"
                    End If
                    Me.lblTitulo.Text = "Registro de Actividad"
                    RetornaOrden()
                Else
                    Me.HdAccion.Value = "M"
                    Me.lblTitulo.Text = "Actualizar Actividad"
                    Me.btnAgregar.Visible = False
                    MostrarControles(False)
                    Me.HdCodigo_apr.Value = Request.QueryString("apr")
                    AsignaActividad(Request.QueryString("apr"))
                End If
            End If
        End If
    End Sub

    Private Sub RetornaOrden()
        Try
            Dim obj As New ClsConectarDatos
            Dim dt As New Data.DataTable
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            dt = obj.TraerDataTable("PLAN_RetornaOrdenProyecto", Session("cod_pro"))
            obj.CerrarConexion()
            If (dt.Rows.Count > 0) Then
                Me.txtOrden.Text = dt.Rows(0).Item("UltimoValor") + 1
            Else
                Me.txtOrden.Text = 1
            End If
        Catch ex As Exception
            Me.lblMensaje.Text = "Error al cargar el orden de la actividad"
        End Try
    End Sub

    Private Sub AsignaActividad(ByVal codigo_apr As Integer)
        Try
            Dim obj As New ClsConectarDatos
            Dim dtActividad As New Data.DataTable
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            dtActividad = obj.TraerDataTable("PLAN_BuscaActividadProyecto", Me.HdCodigo_apr.Value, 0, "")
            obj.CerrarConexion()
            If (dtActividad.Rows.Count > 0) Then
                Session("cod_pro") = dtActividad.Rows(0).Item("codigo_pro")
                Me.txtTitulo.Text = dtActividad.Rows(0).Item("titulo_apr").ToString
                Me.txtDescripcion.Text = dtActividad.Rows(0).Item("descripcion_apr").ToString
                Me.txtFechaInicio.Text = dtActividad.Rows(0).Item("fechaInicio_apr").ToString
                Me.txtFechaFin.Text = dtActividad.Rows(0).Item("fechaFin_apr").ToString
                Me.dpPrioridad.SelectedValue = dtActividad.Rows(0).Item("prioridad_apr").ToString
                Me.txtAvance.Text = dtActividad.Rows(0).Item("avance_apr") & "%"
                Me.txtOrden.Text = dtActividad.Rows(0).Item("nroOrden").ToString

                CargaProyecto(Session("cod_pro"))
                CargaDependientes(Session("cod_pro"))
                If (dtActividad.Rows(0).Item("codigoDepende").ToString <> "") Then
                    Me.dpDependiente.SelectedValue = dtActividad.Rows(0).Item("codigoDepende").ToString
                End If
                dtActividad.Dispose()
                obj = Nothing

                If (dtActividad.Rows(0).Item("proceso").ToString.ToUpper = "S") Then
                    Me.chkProceso.Checked = True
                End If

                Me.chkDias.Checked = dtActividad.Rows(0).Item("mostrarDias_apr")
                Me.chkVisible.Checked = dtActividad.Rows(0).Item("visibilidad_apr")
                Me.chkFeriado.Checked = dtActividad.Rows(0).Item("feriado_apr")
                CargaDetalle(0, Me.HdCodigo_apr.Value)
            End If
        Catch ex As Exception            
            Me.lblMensaje.Text = "Error al asignar datos"
        End Try
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

    Private Sub CargaDependientes(ByVal codigo_pro As Integer)
        Dim obj As New ClsConectarDatos
        Dim dtActividad As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        dtActividad = obj.TraerDataTable("PLAN_BuscaActividadProyecto", 0, codigo_pro, "")
        obj.CerrarConexion()

        Dim itemNinguno As New ListItem
        itemNinguno.Value = 0
        itemNinguno.Text = "NINGUNO"
        Me.dpDependiente.Items.Add(itemNinguno)

        If (dtActividad.Rows.Count > 0) Then
            For i As Integer = 0 To dtActividad.Rows.Count - 1
                Dim item As New ListItem
                item.Value = dtActividad.Rows(i).Item("codigo_apr")
                item.Text = dtActividad.Rows(i).Item("titulo_apr")
                Me.dpDependiente.Items.Add(item)
            Next
        End If        
        dtActividad.Dispose()
        obj = Nothing
    End Sub

    Protected Sub btnGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardar.Click
        If (validaForm("btnGuardar") = True) Then
            AgregarActividad()
            If (Me.gvDetalle.Rows.Count = 0) Then
                RegistraDetalle(0, 0)
            End If
            Response.Write("<script>window.close()</script>")
        End If
    End Sub

    Private Function validaForm(ByVal boton As String) As Boolean
        'Titulo
        If (Me.txtTitulo.Text.Trim = "") Then
            Response.Write("<script>alert('Debe ingresar el titulo del proyecto')</script>")
            Me.txtTitulo.Focus()
            Return False
        End If

        'Orden
        If (Me.txtOrden.Text.Trim = "") Then
            Response.Write("<script>alert('Debe ingresar el orden de la actividad')</script>")
            Me.txtOrden.Focus()
            Return False
        End If

        'Fecha
        If (Me.txtFechaInicio.Text.Trim = "" Or Me.txtFechaFin.Text.Trim = "") Then
            Response.Write("<script>alert('Debe ingresar el titulo del proyecto')</script>")
            Return False
        Else
            If (Date.Parse(Me.txtFechaInicio.Text) > Date.Parse(Me.txtFechaFin.Text)) Then
                Response.Write("<script>alert('La fecha de Inicio debe ser menor a la fecha final')</script>")
                Return False
            End If
        End If

        If (Date.Parse(Me.txtFechaInicio.Text).Year <> Date.Parse(Me.txtFechaFin.Text).Year) Then
            Response.Write("<script>alert('La Fecha Fin no debe exceder del año " & Date.Parse(Me.txtFechaInicio.Text).Year & "')</script>")
            Return False
        End If

        If (Me.txtAvance.Text.Trim = "") Then
            Response.Write("<script>alert('Debe ingresar la cantidad de avance del proyecto')</script>")
            Me.txtAvance.Focus()
            Return False
        Else
            If (Me.txtAvance.Text.Trim = "%") Then
                Response.Write("<script>alert('Debe ingresar la cantidad de avance del proyecto')</script>")
                Me.txtAvance.Focus()
                Return False
            End If
        End If

        If (boton = "btnGuardar") Then
            'If (Me.gvDetalle.Rows.Count = 0) Then
            '    Response.Write("<script>alert('Debe ingresar minimo 1 responsable')</script>")
            '    Return False
            'End If
        End If        
        Return True
    End Function

    Private Sub AgregarActividad()
        Dim obj As New ClsConectarDatos        
        Dim codigo_apr As Integer = 0
        Dim dtCodigo As Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        Try
            obj.AbrirConexion()            
            If (Me.HdCodigo_apr.Value <> "") Then
                codigo_apr = Integer.Parse(Me.HdCodigo_apr.Value)
            End If


            If (Right(Me.txtAvance.Text, 1) = "%") Then Me.txtAvance.Text = Left(Me.txtAvance.Text, Me.txtAvance.Text.Length - 1)

            Dim esProceso As String = "N"
            If chkProceso.Checked = True Then
                esProceso = "S"
            End If

            dtCodigo = obj.TraerDataTable("PLAN_RegistraActividadProyecto", codigo_apr, Session("cod_pro"), Me.txtTitulo.Text, _
                         Me.txtDescripcion.Text, Me.txtFechaInicio.Text, Me.txtFechaFin.Text, _
                         Me.dpPrioridad.SelectedValue, Me.txtAvance.Text, Me.dpDependiente.SelectedValue, _
                         Me.HdCodigo_Res.Value, Me.txtOrden.Text, esProceso, IIf(Me.chkVisible.Checked, 1, 0), _
                         IIf(Me.chkDias.Checked, 1, 0), IIf(Me.chkFeriado.Checked, 1, 0))

            If (dtCodigo.Rows.Count > 0 Or codigo_apr <> 0) Then
                Me.HdCodigo_apr.Value = dtCodigo.Rows(0).Item(0)
            End If
            obj.CerrarConexion()
            Me.lblMensaje.Text = "Actividad registrada."
            obj = Nothing
        Catch ex As Exception
            'Me.lblMensaje.Text = "Error al registrar actividad"
            Me.lblMensaje.Text = ex.Message
            obj = Nothing
        End Try
    End Sub

    Private Sub CargaBusqueda(ByVal codigo_pro As Integer, ByVal nombre As String)
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        Try
            Dim dtResponsables As Data.DataTable
            obj.AbrirConexion()
            dtResponsables = obj.TraerDataTable("PLAN_ListaResponsable", codigo_pro, nombre)
            obj.CerrarConexion()
            gvResponsables.DataSource = dtResponsables
            gvResponsables.DataBind()
            dtResponsables.Dispose()
            obj = Nothing
        Catch ex As Exception
            Me.lblMensaje.Text = "Error al realizar busqueda"
        End Try
    End Sub

    Private Sub CargaDetalle(ByVal codigo_res As Integer, ByVal codigo_apr As Integer)
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        Try
            Dim dtResponsables As New Data.DataTable
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

    Protected Sub btnFiltro_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFiltro.Click
        'If (Session("cod_pro") > 0) Then
        CargaBusqueda(0, Me.txtFiltro.Text)
        'End If
    End Sub

    Private Sub MostrarControles(ByVal sw As Boolean)
        Me.lblFiltro.Visible = sw
        Me.txtFiltro.Visible = sw
        Me.btnFiltro.Visible = sw        
        Me.gvResponsables.Visible = sw
        Me.btnBuscar.Visible = Not sw
    End Sub

    Protected Sub btnBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBuscar.Click
        Me.txtFiltro.Text = Me.txtResponsable.Text
        MostrarControles(True)
    End Sub

    Protected Sub gvResponsables_SelectedIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSelectEventArgs) Handles gvResponsables.SelectedIndexChanging        
        Me.HdCodigo_Res.Value = Me.gvResponsables.DataKeys(e.NewSelectedIndex).Values(0)
        Me.HdTipo_Res.Value = Me.gvResponsables.DataKeys(e.NewSelectedIndex).Values(1)
        Me.txtResponsable.Text = ReemplazaTildes(Me.gvResponsables.Rows(e.NewSelectedIndex).Cells(2).Text)

        MostrarControles(False)
        Me.btnBuscar.Visible = False
        Me.btnAgregar.Visible = True
    End Sub

    Protected Sub btnAgregar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAgregar.Click
        If (validaForm("btnAgregar") = True) Then
            Me.btnBuscar.Visible = True
            Me.btnAgregar.Visible = False

            If (Me.HdCodigo_apr.Value = "") Then
                AgregarActividad()
            End If

            If (Me.HdCodigo_apr.Value <> "") Then
                Dim codigo_per As Integer = 0
                Dim codigo_gpr As Integer = 0

                If (Me.HdTipo_Res.Value = "[Personal]") Then
                    codigo_per = Integer.Parse(Me.HdCodigo_Res.Value)
                    codigo_gpr = 0
                ElseIf (Me.HdTipo_Res.Value = "[Grupo]") Then
                    codigo_gpr = Integer.Parse(Me.HdCodigo_Res.Value)
                    codigo_per = 0
                End If

                RegistraDetalle(codigo_gpr, codigo_per)
            End If

            If (Session("cod_pro") > 0) Then
                CargaDetalle(0, Me.HdCodigo_apr.Value)
            End If
        End If
    End Sub

    Private Sub RegistraDetalle(ByVal codigo_gpr As Integer, ByVal codigo_per As Integer)
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        Try
            obj.AbrirConexion()
            obj.Ejecutar("PLAN_RegistraResponsableAct", codigo_gpr, codigo_per, Me.HdCodigo_apr.Value)
            obj.CerrarConexion()
        Catch ex As Exception
            Me.lblMensaje.Text = "Error al registrar el responsable"
        End Try
    End Sub

    Protected Sub gvResponsables_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gvResponsables.PageIndexChanging
        gvResponsables.PageIndex = e.NewPageIndex()
        If (Session("cod_pro") > 0) Then
            CargaBusqueda(Session("cod_pro"), Me.txtFiltro.Text)
        End If
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

    Protected Sub btnCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        If (Me.HdAccion.Value = "N") Then
            EliminaRegistro(0, Me.HdCodigo_apr.Value)
        End If

        LimpiaControles()
        Response.Write("<script>window.close()</script>")
        Response.Write("<script>self.parent.tb_remove();</script>")
    End Sub

    Protected Sub gvDetalle_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles gvDetalle.RowDeleting        
        EliminaRegistro(Me.gvDetalle.DataKeys(e.RowIndex).Values(0), 0)
        If (Session("cod_pro") > 0) Then
            CargaDetalle(0, Me.HdCodigo_apr.Value)
        End If
    End Sub

    Private Sub EliminaRegistro(ByVal codigo_res As String, ByVal codigo_act As String)
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        Try
            obj.AbrirConexion()
            obj.Ejecutar("PLAN_EliminaResponsable", codigo_res, codigo_act, Session("id_per"))
            obj.CerrarConexion()
        Catch ex As Exception
            Me.lblMensaje.Text = "Error al cancelar el proceso."
        End Try
    End Sub

    Private Sub LimpiaControles()
        Me.HdCodigo_apr.Value = ""
        Me.HdCodigo_Res.Value = ""
        Me.HdTipo_Res.Value = ""
        Me.txtAvance.Text = ""
        Me.txtDescripcion.Text = ""
        Me.txtFechaFin.Text = ""
        Me.txtFechaInicio.Text = ""
        Me.txtFiltro.Text = ""
        Me.txtResponsable.Text = ""
        Me.txtTitulo.Text = ""
        Me.txtOrden.Text = ""
        Me.gvDetalle.DataSource = Nothing
        Me.gvDetalle.DataBind()
        Me.gvResponsables.DataSource = Nothing
        Me.gvResponsables.DataBind()
    End Sub

    Protected Sub gvDetalle_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gvDetalle.PageIndexChanging
        gvDetalle.PageIndex = e.NewPageIndex()
        If (Me.HdCodigo_apr.Value <> "") Then
            CargaDetalle(0, Me.HdCodigo_apr.Value)
        End If
    End Sub
End Class
