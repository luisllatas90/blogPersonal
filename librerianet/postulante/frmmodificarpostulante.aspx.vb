
Partial Class frmmodificarpostulante
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            'Cargar Listas
            Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("cnxBDUSAT").ConnectionString)
            Dim tblDpto As Data.DataTable
            Dim tblProv As Data.DataTable
            Dim tblDis As Data.DataTable

            ClsFunciones.LlenarListas(Me.dpEscuela, obj.TraerDataTable("ConsultarCarreraProfesional", "IV", 0), "codigo_cpf", "nombre_cpf", "-Seleccione-")
            tblDpto = obj.TraerDataTable("ConsultarLugares", 2, 156, 0)
            
            ClsFunciones.LlenarListas(Me.dpcodigo_vfam, obj.TraerDataTable("spPla_ConsultarVinculoFamiliar", 1, 0), "codigo_vfam", "nombre_vfam", "-Seleccione el vínculo familiar-")
            ClsFunciones.LlenarListas(Me.chkRadio, obj.TraerDataTable("ConsultarDatosPostulanteOtros", "1", 0, 0, 0), "codigo_rad", "nombre_rad", "-Seleccione-")
            ClsFunciones.LlenarListas(Me.dpDpto, tblDpto, "codigo_Dep", "nombre_dep", "-Seleccione-")
            ClsFunciones.LlenarListas(Me.dpDptoColegio, tblDpto, "codigo_Dep", "nombre_dep", "-Seleccione-")

            
            'Cargar Datos
            Dim tbl As Data.DataTable

            tbl = obj.TraerDataTable("ConsultarDatosPostulanteOtros", 2, Request.QueryString("codigo_pot"), 0, 0)


            Me.txtapellidoPatRazSoc_Pot.Text = tbl.Rows(0).Item("apellidoPatRazSoc_Pot").ToString
            Me.txtapellidoMat_Pot.Text = tbl.Rows(0).Item("apellidoMat_Pot").ToString
            Me.txtnombres_Pot.Text = tbl.Rows(0).Item("nombres_pot").ToString
            Dim fecha As DateTime
            fecha = tbl.Rows(0).Item("fechaNacimiento_Pot").ToString

            Me.DDlAño.Items.Add("Año")
            For i As Integer = (Now.Year - 15) To 1980 Step -1
                Me.DDlAño.Items.Add(i.ToString)
            Next

            Me.DDlDia.SelectedValue = Day(fecha)
            Me.DDLMes.SelectedValue = Month(fecha)
            Me.DDlAño.SelectedValue = Year(fecha)
            Me.txtemail_Pot.Text = tbl.Rows(0).Item("email_pot").ToString
            Me.txtdireccion_Pot.Text = tbl.Rows(0).Item("direccion_pot").ToString
            Me.txturbanizacion_pot.Text = tbl.Rows(0).Item("urbanizacion_pot").ToString()

            Me.txttelefono_Pot.Text = tbl.Rows(0).Item("telefono_Pot").ToString()
            Me.txttelefonoMovil_Pot.Text = tbl.Rows(0).Item("telefonoMovil_pot").ToString()
            Me.txtapoderado_dpo.Text = tbl.Rows(0).Item("apoderado_dpo").ToString
            Me.dpcodigo_vfam.SelectedValue = tbl.Rows(0).Item("codigo_vfam")

            Me.dpEscuela.SelectedValue = tbl.Rows(0).Item("codigo_cpf")
            Me.chkRadio.SelectedValue = tbl.Rows(0).Item("codigo_rad")
            Me.rdGrado.SelectedValue = tbl.Rows(0).Item("grado_dpo")
            Me.rdSexo.SelectedValue = tbl.Rows(0).Item("sexo_pot")
            'Cargar combos
            Me.dpDpto.SelectedValue = tbl.Rows(0).Item("codigo_dep")
            ClsFunciones.LlenarListas(Me.dpProvincia, obj.TraerDataTable("ConsultarLugares", 3, Me.dpDpto.SelectedValue, 0), "codigo_pro", "nombre_pro", "-Seleccione-")

            Me.dpProvincia.SelectedValue = tbl.Rows(0).Item("codigo_pro")
            ClsFunciones.LlenarListas(Me.dpDistrito, obj.TraerDataTable("ConsultarLugares", 4, Me.dpProvincia.SelectedValue, 0), "codigo_dis", "nombre_dis", "-Seleccione-")
            Me.dpDistrito.SelectedValue = tbl.Rows(0).Item("codigo_dis")

            Me.dpDptoColegio.SelectedValue = tbl.Rows(0).Item("codigo_depcol")
            ClsFunciones.LlenarListas(Me.dpProvinciaColegio, obj.TraerDataTable("ConsultarLugares", 3, Me.dpDptoColegio.SelectedValue, 0), "codigo_pro", "nombre_pro", "-Seleccione-")

            Me.dpProvinciaColegio.SelectedValue = tbl.Rows(0).Item("codigo_procol")
            ClsFunciones.LlenarListas(Me.dpDistritoColegio, obj.TraerDataTable("ConsultarLugares", 4, Me.dpProvinciaColegio.SelectedValue, 0), "codigo_dis", "nombre_dis", "-Seleccione-")

            Me.dpDistritoColegio.SelectedValue = tbl.Rows(0).Item("codigo_discol")
            ClsFunciones.LlenarListas(Me.dpColegio, obj.TraerDataTable("ConsultarLugares", 9, Me.dpDistritoColegio.SelectedValue, 0), "codigo_Col", "nombre_Col", "-Seleccione el Colegio-")

            Me.dpColegio.SelectedValue = tbl.Rows(0).Item("codigo_col")

            tbl.Dispose()

            'Cajas
            Me.txttelefono_Pot.Attributes.Add("onkeypress", "validarnumero()")
            Me.txttelefonoMovil_Pot.Attributes.Add("onkeypress", "validarnumero()")

            obj = Nothing
        End If
    End Sub
    Protected Sub dpDptoColegio_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dpDptoColegio.SelectedIndexChanged
        'If dpDpto.SelectedValue <> -2 Then
        Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("cnxBDUSAT").ConnectionString)
        ClsFunciones.LlenarListas(Me.dpProvinciaColegio, obj.TraerDataTable("ConsultarLugares", 3, Me.dpDptoColegio.SelectedValue, 0), "codigo_pro", "nombre_pro", "-Seleccione-")
        obj = Nothing
        'End If
    End Sub
    Protected Sub dpDpto_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dpDpto.SelectedIndexChanged
        'If dpDpto.SelectedValue <> -2 Then
        Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("cnxBDUSAT").ConnectionString)
        ClsFunciones.LlenarListas(Me.dpProvincia, obj.TraerDataTable("ConsultarLugares", 3, Me.dpDpto.SelectedValue, 0), "codigo_pro", "nombre_pro", "-Seleccione-")
        obj = Nothing
        'End If
    End Sub

    Protected Sub dpProvincia_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dpProvincia.SelectedIndexChanged
        'If dpProvincia.SelectedValue <> -2 Then
        Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("cnxBDUSAT").ConnectionString)
        ClsFunciones.LlenarListas(Me.dpDistrito, obj.TraerDataTable("ConsultarLugares", 4, Me.dpProvincia.SelectedValue, 0), "codigo_dis", "nombre_dis", "-Seleccione-")
        obj = Nothing
        'End If
    End Sub

    Protected Sub dpProvinciaColegio_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dpProvinciaColegio.SelectedIndexChanged
        'If dpProvinciaColegio.SelectedValue <> -2 Then
        Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("cnxBDUSAT").ConnectionString)
        ClsFunciones.LlenarListas(Me.dpDistritoColegio, obj.TraerDataTable("ConsultarLugares", 4, Me.dpProvinciaColegio.SelectedValue, 0), "codigo_dis", "nombre_dis", "-Seleccione-")
        obj = Nothing
        'End If
    End Sub

    Protected Sub dpDistritoColegio_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dpDistritoColegio.SelectedIndexChanged
        'If dpDistritoColegio.SelectedValue <> -2 Then
        Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("cnxBDUSAT").ConnectionString)
        ClsFunciones.LlenarListas(Me.dpColegio, obj.TraerDataTable("ConsultarLugares", 9, Me.dpDistritoColegio.SelectedValue, 0), "codigo_Col", "nombre_Col", "-Seleccione el Colegio-")
        obj = Nothing
        'End If
    End Sub

    Protected Sub cmdGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdGuardar.Click

        Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("cnxBDUSAT").ConnectionString)
        Dim codigo_pot As Integer
        Dim id As Integer

        id = Request.QueryString("id")
        codigo_pot = Request.QueryString("codigo_pot")

        Try
            obj.IniciarTransaccion()
            obj.Ejecutar("AgregarDatosPostulanteOtros", codigo_pot, _
                        UCase(txtapellidoPatRazSoc_Pot.Text.Trim), _
                        UCase(txtapellidoMat_Pot.Text.Trim), _
                        UCase(txtnombres_Pot.Text.Trim), _
                        UCase(txtdireccion_Pot.Text.Trim), _
                        UCase(txturbanizacion_pot.Text.Trim), _
                        txttelefono_Pot.Text.Trim, _
                        dpEscuela.SelectedValue, _
                        False, _
                        CDate(Me.DDlDia.SelectedValue & "/" & Me.DDLMes.SelectedValue & "/" & Me.DDlAño.SelectedValue), _
                        txttelefonoMovil_Pot.Text.Trim, _
                        rdSexo.SelectedValue, _
                        dpDistrito.SelectedValue, _
                        LCase(txtemail_Pot.Text.Trim), _
                        "SOLTERO", _
                        UCase(txtapoderado_dpo.Text.Trim), _
                        dpcodigo_vfam.SelectedValue, _
                        dpColegio.SelectedValue, _
                        rdGrado.SelectedValue, _
                        id, 0)
            obj.Ejecutar("AgregarRadioPostulanteOtros", codigo_pot, Me.chkRadio.SelectedValue)
            obj.TerminarTransaccion()
            obj = Nothing
            If codigo_pot <> -1 Then
                RegisterStartupScript("MsjGuardar", "<script>alert('Los datos se han guardado correctamente');location.href='postulante.aspx?id=" & Request.QueryString("id") & "'</script>")
            Else
                RegisterStartupScript("MsjGuardar", "<script>alert('Los datos que intenta registrar ya existen. Consulte la lista de registros.')</script>")
            End If

        Catch ex As Exception
            obj.AbortarTransaccion()
            obj = Nothing
            RegisterStartupScript("MsjGuardar", "<script>alert('Ocurrió un Error al Guardar los datos. Intente denuevo')</script>")
        End Try

    End Sub

    Protected Sub cmdCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdCancelar.Click
        Response.Redirect("postulante.aspx?" & Request.QueryString("id"))
    End Sub
End Class
