Partial Class campus_frmActualizarDatos
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            'Response.Write("<script>alert('Envio: " & Session("codigo_alu") & "')</script>")
            txt_Fecha.Text = DateTime.Now.ToString("dd/MM/yyyy")

            Call wf_DatosPersonales()
            Call CargaPaises()
        End If
    End Sub

    Sub wf_DatosPersonales()
        Dim obj As New ClsConectarDatos
        Dim dtt As Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        dtt = obj.TraerDataTable("ALUMNI_ObtenerDatosPersona", Session("codigo_alu"))
        obj.CerrarConexion()

        txt_correo.Text = dtt.Rows(0).Item("emailPrincipal_Pso").ToString
        txt_telefono.Text = dtt.Rows(0).Item("telefonoFijo_Pso").ToString
        txt_celular.Text = dtt.Rows(0).Item("telefonoCelular_Pso").ToString
        txt_direccion.Text = dtt.Rows(0).Item("direccion_Pso").ToString
        hdcodigo_pso.Value = dtt.Rows(0).Item("codigo_pso").ToString
        txt_empresa.Text = dtt.Rows(0).Item("EmpresaLabora_Ega").ToString
        txt_cargo.Text = dtt.Rows(0).Item("cargoActual_Ega").ToString
        'txt_Fecha.Text = dtt.Rows(0).Item("fechaActualizacion_Ega").ToString
    End Sub

    Sub CargaPaises()
        Dim obj As New ClsConectarDatos
        Dim dtPais As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()

        ''#Escuela
        dtPais = obj.TraerDataTable("ConsultarPais", "T", "")
        dpPais.DataSource = dtPais
        dpPais.DataTextField = "nombre_pai"
        dpPais.DataValueField = "codigo_pai"
        dpPais.SelectedIndex = 155

        dpPais.DataBind()
        dpPais.Dispose()
        obj.CerrarConexion()
        obj = Nothing
    End Sub

    Function wf_validar() As Boolean
        If (Me.txt_correo.Text.Trim = "") Then
            Response.Write("<script>alert('Debe ingresar un correo electronico')</script>")
            Me.txt_correo.Focus()
            Return False
        End If

        If (Me.txt_telefono.Text.Trim = "") Then
            Response.Write("<script>alert('Debe ingresar el número Telefónico')</script>")
            Me.txt_telefono.Focus()
            Return False
        End If

         If Me.txt_telefono.Text.Trim.Length < 6 Then
            Response.Write("<script>alert('El número Telefónico es incorrecto')</script>")
            Me.txt_telefono.Focus()
            Return False
        End If

        If Me.txt_celular.Text.Trim.Length < 9 Then
            Response.Write("<script>alert('El número de Celular es incorrecto')</script>")
            Me.txt_celular.Focus()
            Return False
        End If

        If (Me.txt_direccion.Text.Trim = "") Then
            Response.Write("<script>alert('Debe ingresar su dirección')</script>")
            Me.txt_direccion.Focus()
            Return False
        End If

        Return True
    End Function

    Protected Sub btn_Guardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Guardar.Click
        If (wf_validar() = True) Then
            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            obj.Ejecutar("Alumni_ActualizaCorreoTelefono", "A", hdcodigo_pso.Value, txt_correo.Text.Trim, "", txt_telefono.Text.Trim, txt_celular.Text.Trim, txt_direccion.Text.Trim)

            obj.CerrarConexion()
            
            obj.AbrirConexion()
            obj.Ejecutar("ALUMNI_ActualizaDatosEgresadoAlumni", hdcodigo_pso.Value, txt_empresa.Text.Trim.ToUpper, txt_Fecha.Text, txt_cargo.Text.Trim.ToUpper, dpPais.SelectedValue.ToString)
            obj.CerrarConexion()

            btn_Salir_Click(sender, e)
        End If
    End Sub

    Protected Sub btn_Salir_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Salir.Click
        Response.Write("<script languaje=javascript>window.close();</script>")
    End Sub
End Class
