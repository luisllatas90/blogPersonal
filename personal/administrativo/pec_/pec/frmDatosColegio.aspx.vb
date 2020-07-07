Imports System.Data

Partial Class administrativo_pec_frmDatosColegio
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (Session("id_per") Is Nothing) Then
            Response.Redirect("../../../../sinacceso.html")
        End If

        If Not IsPostBack Then
            CargaCategoria()
            If Request.QueryString("accion") = "M" Then 'Modificar
                Me.cmdCancelar.Attributes.Add("onclick", "self.parent.tb_remove();")

                cargarDatos(Request.QueryString("pk"))
            Else
                CargarPais()
                cboPais.SelectedValue = Request.QueryString("pai")
                cargarDepartamento(cboPais.SelectedValue)
                cboDpto.SelectedValue = Request.QueryString("dep")
                cargarProvincia(cboDpto.SelectedValue)
                cargarDistrito(cboProv.SelectedValue)

                cboDist.Enabled = False
                cboTipo.Enabled = False
                txtNombre.Enabled = False
                CmdGuardar.Enabled = False


                Me.cmdCancelar.UseSubmitBehavior = True
            End If

            'Si llaman a esta página como LigthBox : Gerardo
            If Request.QueryString("box") = "S" Then
                Me.cmdCancelar.UseSubmitBehavior = False
                Me.cmdCancelar.Attributes.Add("onclick", "self.parent.tb_remove();")
                Me.cboProv.SelectedValue = Request.QueryString("pro")
                cargarDistrito(cboProv.SelectedValue)

                Me.cboDist.SelectedValue = Request.QueryString("dis")
                Me.cboDpto.Enabled = False
                Me.cboProv.Enabled = False
                Me.cboDist.Enabled = False
                Me.cboTipo.Enabled = True
            End If
        End If
    End Sub

    Private Sub CargaCategoria()
        Dim obj As New ClsConectarDatos
        Dim objfun As New ClsFunciones

        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        objfun.CargarListas(cboCategoria, obj.TraerDataTable("CategoriaPensionConsultar", 0, "%"), "codigo", "categoria")
        obj.CerrarConexion()
        obj = Nothing

        cboCategoria.SelectedValue = 10 'Por defecto No Definido
    End Sub

    Private Sub RetornaDescripcion(ByVal Codigo As Integer)
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable

        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        dt = obj.TraerDataTable("CategoriaPensionConsultar", Codigo, "%")
        obj.CerrarConexion()
        obj = Nothing

        If (dt.Rows.Count > 0) Then
            lblCategoria.Text = dt.Rows(0).Item("observacion_catp")
        Else
            lblCategoria.Text = "(Sin Descripcion)"
        End If
    End Sub

    Protected Sub cboDpto_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboDpto.SelectedIndexChanged
        cargarProvincia(cboDpto.SelectedValue)
    End Sub

    Private Sub cargarDepartamento(ByVal codigo_Pai As Integer)
        'Cargar Dpto
        Dim obj As New ClsConectarDatos
        Dim objfun As New ClsFunciones

        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        If (codigo_Pai = 156) Then
            objfun.CargarListas(cboDpto, obj.TraerDataTable("ConsultarLugares", "U", codigo_Pai, ""), "codigo_Dep", "nombre_Dep", "-- Seleccione --")
        Else
            objfun.CargarListas(cboDpto, obj.TraerDataTable("ConsultarLugares", "2", codigo_Pai, ""), "codigo_Dep", "nombre_Dep", "-- Seleccione --")
        End If

        obj.CerrarConexion()
        obj = Nothing
    End Sub

    Private Sub cargarProvincia(ByVal codigo_Dep As String)
        'Cargar Provincias
        Dim obj As New ClsConectarDatos
        Dim objfun As New ClsFunciones
        Dim datos As DataTable

        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        datos = obj.TraerDataTable("ConsultarLugares", "P", codigo_Dep, "")
        objfun.CargarListas(cboProv, datos, "codigo_Pro", "nombre_Pro", "--Seleccione--")

        obj.CerrarConexion()
        obj = Nothing
    End Sub


    Private Sub cargarDistrito(ByVal codigo_pro As String)
        'Cargar distritos
        Dim obj As New ClsConectarDatos
        Dim objfun As New ClsFunciones
        Dim datos As DataTable

        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        datos = obj.TraerDataTable("ConsultarLugares", "I", codigo_pro, "")
        objfun.CargarListas(cboDist, datos, "codigo_Dis", "nombre_Dis", "-- Seleccione --")

        obj.CerrarConexion()
        obj = Nothing

        If codigo_pro = -1 Then
            cboDist.Enabled = False
            txtNombre.Enabled = False
            cboTipo.Enabled = False
            CmdGuardar.Enabled = False
        Else
            cboDist.SelectedValue = -1
            cboDist.Enabled = True
        End If
    End Sub

    Protected Sub cboProv_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboProv.SelectedIndexChanged
        cargarDistrito(cboProv.SelectedValue)
    End Sub

    Protected Sub CmdGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdGuardar.Click
        Dim ObjGuarda As New ClsConectarDatos
        ObjGuarda.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        'Response.Write("b")
        If Request.QueryString("accion") = "A" Then
            Try
                ObjGuarda.AbrirConexion()
                ObjGuarda.Ejecutar("CAJ_AgregarInstitucionEducativa", txtNombre.Text.ToUpper, cboDist.SelectedValue, cboNivel.SelectedValue, cboTipo.SelectedValue, cboCategoria.SelectedValue, Request.QueryString("id"))
                ObjGuarda.CerrarConexion()

                If Request.QueryString("box") = "S" Then
                    Page.RegisterStartupScript("Aviso1", "<script>alert('Datos registrados correctamente');self.parent.tb_remove();</script>")
                Else
                    Page.RegisterStartupScript("Aviso2", "<script>alert('Datos registrados correctamente');location.href='lstColegios.aspx?box=" & Request.QueryString("box") & "&id=" & Request.QueryString("id") & "&ctf=" & Request.QueryString("ctf") & "'</script>")
                End If
            Catch ex As Exception
                Me.LblMensaje.Text = "Ocurrió un error al regitrar los datos, inténtelo nuevamente"
                Me.LblMensaje.ForeColor = Drawing.Color.Red
            End Try
        Else
            Try
                ObjGuarda.AbrirConexion()
                ObjGuarda.Ejecutar("CAJ_ModificarInstitucionEducativa", Request.QueryString("pk"), txtNombre.Text, cboDist.SelectedValue, cboNivel.SelectedValue, cboTipo.SelectedValue, Me.cboCategoria.SelectedValue, Request.QueryString("id"))
                ObjGuarda.CerrarConexion()
                Page.RegisterStartupScript("Aviso3", "<script>alert('Datos modificados correctamente');window.parent.location.reload();self.parent.tb_remove()</script>")

            Catch ex As Exception
                Me.LblMensaje.Text = "Error: " & ex.Message
                Me.LblMensaje.ForeColor = Drawing.Color.Red
            End Try

        End If


    End Sub

    Protected Sub cboTipo_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboTipo.SelectedIndexChanged
        If cboTipo.SelectedValue = "-1" Then
            txtNombre.Enabled = False
            CmdGuardar.Enabled = False
        Else
            txtNombre.Enabled = True
            CmdGuardar.Enabled = True
        End If
    End Sub

    Protected Sub cboDist_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboDist.SelectedIndexChanged
        If cboDist.SelectedValue = -1 Then
            cboTipo.Enabled = False
            txtNombre.Enabled = False
            CmdGuardar.Enabled = False
        Else
            cboTipo.Enabled = True
        End If
    End Sub

    Private Sub cargarDatos(ByVal codigo_col As Integer)
        Dim obj As New ClsConectarDatos
        Dim objfun As New ClsFunciones
        Dim datos As DataTable

        Try
            'Cargar distritos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            datos = obj.TraerDataTable("EVE_ConsultarDatosColegio", codigo_col)

            If datos.Rows.Count > 0 Then
                CargarPais()
                cboPais.SelectedValue = datos.Rows(0).Item("codigo_Pai")
                cargarDepartamento(cboPais.SelectedValue)
                cboDpto.SelectedValue = datos.Rows(0).Item("codigo_Dep")
                cargarProvincia(datos.Rows(0).Item("codigo_Dep"))
                cboProv.SelectedValue = datos.Rows(0).Item("codigo_Pro")
                cargarDistrito(datos.Rows(0).Item("codigo_Pro"))
                cboDist.SelectedValue = datos.Rows(0).Item("codigo_Dis")
                txtNombre.Text = datos.Rows(0).Item("nombre_col")
                cboTipo.Text = "" & datos.Rows(0).Item("tipo_col")
                If (datos.Rows(0).Item("codigo_catp") <> 0) Then
                    cboCategoria.SelectedValue = datos.Rows(0).Item("codigo_catp")
                Else
                    cboCategoria.SelectedValue = 10 'Por defecto No Definido
                End If
            End If

            obj.CerrarConexion()
            obj = Nothing
        Catch ex As Exception
            Me.LblMensaje.Text = "Error: " & ex.Message
            Me.LblMensaje.ForeColor = Drawing.Color.Red
        End Try
    End Sub
    Protected Sub cmdCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdCancelar.Click
        Response.Redirect("lstColegios.aspx?id=" & Request.QueryString("id") & "&ctf=" & Request.QueryString("ctf"))
    End Sub

    Protected Sub cboCategoria_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboCategoria.SelectedIndexChanged
        If (Me.cboCategoria.Items.Count > 0) Then
            RetornaDescripcion(Me.cboCategoria.SelectedValue)
        End If
    End Sub
    Private Sub CargarPais()
        Dim obj As New ClsConectarDatos
        Dim objfun As New ClsFunciones
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()        
        objfun.CargarListas(cboPais, obj.TraerDataTable("ConsultarPais", "T", ""), "codigo_pai", "nombre_pai", "--Seleccione--")
        'objfun.CargarListas(cboDpto, obj.TraerDataTable("sp_ver_departamento"), "codigo_Dep", "nombre_Dep")
        obj.CerrarConexion()
        obj = Nothing
    End Sub

    Protected Sub cboPais_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboPais.SelectedIndexChanged
        cargarDepartamento(cboPais.SelectedValue)
    End Sub

    Protected Sub txtNombre_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtNombre.TextChanged
        'CmdGuardar_Click(sender, e)
    End Sub

End Class
