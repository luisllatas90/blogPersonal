
Partial Class academico_Vacantes_FrmMantenimientoVacantesPosgrado
    Inherits System.Web.UI.Page
    Dim validacustom As Boolean = True
    Dim codigoTest As Integer = 5

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            CargarCicloAcademico()
            CargarCarreraProfesional()
            CargarModalidad()
            'Busqueda
            CargarCicloAcademico2()
            CargarCarreraProfesional2()
            CargarModalidad2()
            chkestado.Checked = True
        End If
    End Sub
    Private Sub CargarCicloAcademico()
        Dim obj As New ClsConectarDatos
        Dim objfun As New ClsFunciones
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        objfun.CargarListas(cboCac, obj.TraerDataTable("ConsultarCicloAcademico", "TO", ""), "codigo_cac", "descripcion_cac", "--Seleccione--")
        obj.CerrarConexion()
        obj = Nothing
    End Sub
    Private Sub CargarCarreraProfesional()
        Dim obj As New ClsConectarDatos
        Dim objfun As New ClsFunciones
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        objfun.CargarListas(cboCpf, obj.TraerDataTable("ConsultarCarreraProfesional", "TEV", codigoTest), "codigo_cpf", "nombre_cpf", "--Seleccione--")
        obj.CerrarConexion()
        obj = Nothing
    End Sub
    Private Sub CargarModalidad()
        Dim obj As New ClsConectarDatos
        Dim objfun As New ClsFunciones
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        objfun.CargarListas(cboMin, obj.TraerDataTable("ConsultarModalidadIngreso", "CT", codigoTest), "codigo_min", "nombre_Min")
        obj.CerrarConexion()
        obj = Nothing
    End Sub
    '---Busqueda
    Private Sub CargarCicloAcademico2()
        Dim obj As New ClsConectarDatos
        Dim objfun As New ClsFunciones
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        objfun.CargarListas(cboCac2, obj.TraerDataTable("ConsultarCicloAcademico", "TO", ""), "codigo_cac", "descripcion_cac", "--Seleccione--")
        obj.CerrarConexion()
        obj = Nothing
    End Sub
    Private Sub CargarCarreraProfesional2()
        Dim obj As New ClsConectarDatos
        Dim objfun As New ClsFunciones
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        objfun.CargarListas(cboCpf2, obj.TraerDataTable("ConsultarCarreraProfesional", "TEV", codigoTest), "codigo_cpf", "nombre_cpf", "--Seleccione--")
        obj.CerrarConexion()
        obj = Nothing
    End Sub
    Private Sub CargarModalidad2()
        Dim obj As New ClsConectarDatos
        Dim objfun As New ClsFunciones
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        objfun.CargarListas(cbomin2, obj.TraerDataTable("ConsultarModalidadIngreso", "CT", codigoTest), "codigo_min", "nombre_Min")
        obj.CerrarConexion()
        obj = Nothing
    End Sub
    '-------
    Protected Sub btnlimpiar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnlimpiar.Click
        limpiar()
    End Sub
    Private Sub limpiar()
        txtvacantes.Text = ""
        CargarCicloAcademico()
        CargarCarreraProfesional()
        CargarModalidad()
        avisos.Attributes.Clear()
        Image1.Attributes.Clear()
        lblmensaje.Text = ""
    End Sub

    Protected Sub btnguardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnguardar.Click
        Try
            Dim dts As New Data.DataTable
            Dim obj As New ClsVacantes
            Dim estado_vac As Integer
            lblmensaje.Visible = True
            If Page.IsValid Then        'Si no ha ingresado cadenas inválidas (select, php, script)
                'Nuevo registro
                If chkestado.Checked = True Then
                    estado_vac = 1
                Else
                    estado_vac = 0
                End If
                If lblCodigoVac.Text = "" Then

                    lblmensaje.Text = obj.InsertarVacantes(cboCac.SelectedValue, cboCpf.SelectedValue, cboMin.SelectedValue, txtvacantes.Text, estado_vac, Request.QueryString("id"))
                Else
                    'Edicion de registro
                    lblmensaje.Text = obj.actualizarVacantes(lblCodigoVac.Text, cboCac.SelectedValue, cboCpf.SelectedValue, cboMin.SelectedValue, txtvacantes.Text, estado_vac, Request.QueryString("id"))
                End If

                If lblmensaje.Text = "Datos Ingresados Correctamente." Or lblmensaje.Text = "Datos Actualizados Correctamente." Then
                    'Para avisos
                    Me.Image1.Attributes.Add("src", "../../Images/accept.png")
                    Me.avisos.Attributes.Add("class", "mensajeExito")
                Else
                    'Para avisos
                    Me.Image1.Attributes.Add("src", "../../Images/exclamation.png")
                    Me.avisos.Attributes.Add("class", "mensajeError")
                End If
                'Refrescar listado
                'CargarCicloAcademico()
                'CargarCarreraProfesional()
                CargarModalidad()
                'Refresca Busqueda
                Buscar_Vacantes(cboCac2.SelectedValue, cboCpf2.SelectedValue, cbomin2.SelectedValue)
                chkestado.Checked = True
                txtvacantes.Text = ""
                txtnroingresantes.Text = ""
                lblCodigoVac.Text = ""
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub btnbuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnbuscar.Click
        Buscar_Vacantes(cboCac2.SelectedValue, cboCpf2.SelectedValue, cbomin2.SelectedValue)
    End Sub
    Private Sub Buscar_Vacantes(ByVal codigo_cac As Integer, ByVal codigo_cpf As Integer, ByVal codigo_min As Integer)
        Try
            Dim dts As New Data.DataTable
            Dim obj As New ClsVacantes

            dts = obj.ConsultarVacantes(codigo_cac, codigo_cpf, codigo_min)

            gvVacantes.DataSource = dts
            gvVacantes.DataBind()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub cboCac_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboCac.SelectedIndexChanged
        Me.Image1.Attributes.Clear()
        Me.avisos.Attributes.Clear()
        lblmensaje.Text = ""
    End Sub

    Protected Sub cboCpf_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboCpf.SelectedIndexChanged
        Me.Image1.Attributes.Clear()
        Me.avisos.Attributes.Clear()
        lblmensaje.Text = ""
    End Sub

    Protected Sub cboMin_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboMin.SelectedIndexChanged
        Me.Image1.Attributes.Clear()
        Me.avisos.Attributes.Clear()
        lblmensaje.Text = ""
    End Sub

    Protected Sub gvVacantes_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvVacantes.RowCommand
        Try
            If (e.CommandName.Equals("Select")) Then 'comprueba que sea el boton de seleccion                

                Dim seleccion As GridViewRow
                Dim estado As Integer
                Dim obj As New ClsVacantes
                '1. Obtengo la linea del gridview que fue cliqueada
                seleccion = DirectCast(e.CommandSource, GridView).Rows(e.CommandArgument)
                limpiar()
                '2. Obtengo el datakey de la linea que donde está el boton que cliqueé
                lblCodigoVac.Text = gvVacantes.DataKeys(seleccion.RowIndex).Values("codigo_Vac")
                cboCac.SelectedValue = gvVacantes.DataKeys(seleccion.RowIndex).Values("codigo_cac")
                cboCpf.SelectedValue = gvVacantes.DataKeys(seleccion.RowIndex).Values("codigo_cpf")
                cboMin.SelectedValue = gvVacantes.DataKeys(seleccion.RowIndex).Values("codigo_min")
                txtvacantes.Text = gvVacantes.Rows(seleccion.RowIndex).Cells.Item(4).Text
                estado = gvVacantes.DataKeys(seleccion.RowIndex).Values("estado_vac")
                txtnroingresantes.Text = obj.ConsultarCantidadIngresantes(cboCac.SelectedValue, cboCpf.SelectedValue, cboMin.SelectedValue, txtvacantes.Text)
                If estado = 1 Then
                    chkestado.Checked = True
                Else
                    chkestado.Checked = False
                End If
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub gvVacantes_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles gvVacantes.RowDeleting
        Try
            Dim obj As New ClsVacantes
            lblmensaje.Text = obj.EliminarVacantes(gvVacantes.DataKeys(e.RowIndex).Value.ToString(), Request.QueryString("id"))
            lblmensaje.Visible = True

            If lblmensaje.Text = "Registro eliminado con éxito." Then
                'Para avisos
                Me.Image1.Attributes.Add("src", "../../Images/accept.png")
                Me.avisos.Attributes.Add("class", "mensajeExito")
            Else
                'Para avisos
                Me.Image1.Attributes.Add("src", "../../Images/exclamation.png")
                Me.avisos.Attributes.Add("class", "mensajeError")
            End If
            'Refresca Busqueda
            Buscar_Vacantes(cboCac2.SelectedValue, cboCpf2.SelectedValue, cbomin2.SelectedValue)
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try

    End Sub
End Class
