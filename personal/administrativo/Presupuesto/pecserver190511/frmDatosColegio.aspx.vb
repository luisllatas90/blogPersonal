Imports System.Data

Partial Class administrativo_pec_frmDatosColegio
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then

            If Request.QueryString("accion") = "M" Then 'Modificar
                Me.CmdCancelar.Attributes.Add("onclick", "self.parent.tb_remove();")

                cargarDatos(Request.QueryString("pk"))
            Else
                cargarDepartamento()
                cboDpto.SelectedValue = Request.QueryString("dep")
                cargarProvincia(cboDpto.SelectedValue)
                cargarDistrito(cboProv.SelectedValue)

                cboDist.Enabled = False
                cboTipo.Enabled = False
                txtNombre.Enabled = False
                CmdGuardar.Enabled = False


                Me.CmdCancelar.UseSubmitBehavior = True
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

    Protected Sub cboDpto_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboDpto.SelectedIndexChanged
        cargarProvincia(cboDpto.SelectedValue)
    End Sub



    Private Sub cargarDepartamento()
        'Cargar Dpto
        Dim obj As New ClsConectarDatos
        Dim objfun As New ClsFunciones

        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()

        objfun.CargarListas(cboDpto, obj.TraerDataTable("sp_ver_departamento"), "codigo_Dep", "nombre_Dep")


        obj.CerrarConexion()
        obj = Nothing
    End Sub

    Private Sub cargarProvincia(ByVal codigo_Dep As Integer)

        'Cargar Provincias
        Dim obj As New ClsConectarDatos
        Dim objfun As New ClsFunciones
        Dim datos As DataTable

        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        datos = obj.TraerDataTable("ConsultarLugares", "3", codigo_Dep, "")
        objfun.CargarListas(cboProv, datos, "codigo_Pro", "nombre_Pro", "--Seleccione--")

        obj.CerrarConexion()
        obj = Nothing

    End Sub


    Private Sub cargarDistrito(ByVal codigo_pro As Integer)

        'Cargar distritos
        Dim obj As New ClsConectarDatos
        Dim objfun As New ClsFunciones
        Dim datos As DataTable

        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        datos = obj.TraerDataTable("ConsultarLugares", "4", codigo_pro, "")
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
        '        rs = clsCnx.Consultar("ConsultarColegio", "ST", cboDepartamento.ItemData(cboDepartamento.ListIndex), txtColegio.Text)

        Dim ObjGuarda As New ClsConectarDatos
        ObjGuarda.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        If Request.QueryString("accion") = "A" Then
            Try
                ObjGuarda.AbrirConexion()
                ObjGuarda.Ejecutar("CAJ_AgregarInstitucionEducativa", txtNombre.Text.ToUpper, cboDist.SelectedValue, cboNivel.SelectedValue, cboTipo.SelectedValue)
                ObjGuarda.CerrarConexion()

                If Request.QueryString("box") = "S" Then
                    Page.RegisterStartupScript("ok", "<script>alert('Datos registrados correctamente');self.parent.tb_remove();</script>")
                Else
                    Page.RegisterStartupScript("ok", "<script>alert('Datos registrados correctamente');location.href='lstColegios.aspx?box=" & Request.QueryString("box") & "&id=" & Request.QueryString("id") & "&ctf=" & Request.QueryString("ctf") & "'</script>")
                End If
            Catch ex As Exception
                Me.LblMensaje.Text = "Ocurrió un error al regitrar los datos, inténtelo nuevamente"
                Me.LblMensaje.ForeColor = Drawing.Color.Red
            End Try
        Else
            Try
                ObjGuarda.AbrirConexion()
                ObjGuarda.Ejecutar("CAJ_ModificarInstitucionEducativa", Request.QueryString("pk"), txtNombre.Text, cboDist.SelectedValue, cboNivel.SelectedValue, cboTipo.SelectedValue)
                ObjGuarda.CerrarConexion()
                Page.RegisterStartupScript("ok", "<script>alert('Datos modificados correctamente');window.parent.location.reload();self.parent.tb_remove()</script>")

            Catch ex As Exception
                Me.LblMensaje.Text = "Ocurrió un error al modificar los datos, inténtelo nuevamente"
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
        'Cargar distritos
        Dim obj As New ClsConectarDatos
        Dim objfun As New ClsFunciones
        Dim datos As DataTable

        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        datos = obj.TraerDataTable("EVE_ConsultarDatosColegio", codigo_col)

        If datos.Rows.Count > 0 Then
            cargarDepartamento()
            cboDpto.SelectedValue = datos.Rows(0).Item("codigo_Dep")
            cargarProvincia(datos.Rows(0).Item("codigo_Dep"))
            cboProv.SelectedValue = datos.Rows(0).Item("codigo_Pro")
            cargarDistrito(datos.Rows(0).Item("codigo_Pro"))
            cboDist.SelectedValue = datos.Rows(0).Item("codigo_Dis")

            txtNombre.Text = datos.Rows(0).Item("nombre_col")
            cboTipo.Text = "" & datos.Rows(0).Item("tipo_col")
        End If

        obj.CerrarConexion()
        obj = Nothing

    End Sub
    Protected Sub cmdCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdCancelar.Click
        Response.Redirect("lstColegios.aspx?id=" & Request.QueryString("id") & "&ctf=" & Request.QueryString("ctf"))
    End Sub
End Class
