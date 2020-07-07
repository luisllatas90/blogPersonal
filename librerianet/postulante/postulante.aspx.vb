
Partial Class librerianet_postulante_postulante
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            'Cargar Listas
            Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("cnxBDUSAT").ConnectionString)
            Dim tblLugares As Data.DataTable
            ClsFunciones.LlenarListas(Me.dpEscuela, obj.TraerDataTable("ConsultarCarreraProfesional", "IV", 0), "codigo_cpf", "nombre_cpf", "-Seleccione-")
            tblLugares = obj.TraerDataTable("ConsultarLugares", 2, 156, 0)
            ClsFunciones.LlenarListas(Me.dpcodigo_vfam, obj.TraerDataTable("spPla_ConsultarVinculoFamiliar", 1, 0), "codigo_vfam", "nombre_vfam", "-Seleccione el vínculo familiar-")
            ClsFunciones.LlenarListas(Me.chkRadio, obj.TraerDataTable("ConsultarDatosPostulanteOtros", "1", 0, 0, 0), "codigo_rad", "nombre_rad", "-Seleccione-")
            'Cargar datos
            Me.GridView1.DataSource = obj.TraerDataTable("ConsultarDatosPostulanteOtros", Me.dpcampo.SelectedValue, Me.txtbuscar.Text.Trim, 0, 0)
            Me.GridView1.DataBind()
            obj = Nothing

            ClsFunciones.LlenarListas(Me.dpDpto, tblLugares, "codigo_Dep", "nombre_dep", "-Seleccione-")
            ClsFunciones.LlenarListas(Me.dpDptoColegio, tblLugares, "codigo_Dep", "nombre_dep", "-Seleccione-")
           
            Me.dpDpto.Items.Add("-Seleccione-")
            Me.dpProvincia.Items.Add("-Seleccione-")
            Me.dpDistrito.Items.Add("-Seleccione-")
            Me.dpDptoColegio.Items.Add("-Seleccione-")
            Me.dpProvinciaColegio.Items.Add("-Seleccione-")
            Me.dpDistritoColegio.Items.Add("-Seleccione-")
            Me.dpColegio.Items.Add("-Seleccione el Colegio-")

            Me.DDlAño.Items.Add("Año")
            For i As Integer = (Now.Year - 15) To 1980 Step -1
                Me.DDlAño.Items.Add(i.ToString)
            Next

            'Cajas
            Me.txttelefono_Pot.Attributes.Add("onkeypress", "validarnumero()")
            Me.txttelefonoMovil_Pot.Attributes.Add("onkeypress", "validarnumero()")

        End If
    End Sub
    Private Sub BuscarDirectorioEstudiantes()
        Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("cnxBDUSAT").ConnectionString)
        Dim termino As String

        termino = LCase(Me.txtbuscar.Text.Trim)
        'termino = Replace(termino, "á", "a")
        'termino = Replace(termino, "é", "e")
        'termino = Replace(termino, "í", "i")
        'termino = Replace(termino, "ó", "o")
        'termino = Replace(termino, "ú", "u")

        Me.GridView1.DataSource = obj.TraerDataTable("ConsultarDatosPostulanteOtros", Me.dpcampo.SelectedValue, termino, 0, 0)
        Me.GridView1.DataBind()
        Me.GridView1.Visible = True
        obj = Nothing
    End Sub
    Protected Sub cmdBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdBuscar.Click
        BuscarDirectorioEstudiantes()
    End Sub

    Protected Sub GridView1_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridView1.RowCommand
        If e.CommandName = "CargarFichaDatos" Then
            'Dim cmd As ImageButton = DirectCast(e.CommandSource, ImageButton)
            'Dim gvr As GridViewRow = DirectCast(cmd.NamingContainer, GridViewRow)
            Dim id As Integer '= Convert.ToInt32(Me.GridView1.DataKeys(gvr.RowIndex).Value)
            'Me.hdCodigo_pot.Value = id 'Convert.ToString(Me.GridView1.DataKeys(Convert.ToInt32(e.CommandArgument)).Value)
            'Me.hdCodigo_pot.Value = Convert.ToString(Me.GridView1.DataKeys(Convert.ToInt32(e.CommandArgument)).Value)
            id = Convert.ToString(Me.GridView1.DataKeys(Convert.ToInt32(e.CommandArgument)).Value)

            Response.Redirect("frmmodificarpostulante.aspx?codigo_pot=" & id & "&id=" & Request.QueryString("id"))
        End If
    End Sub
    Protected Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim fila As Data.DataRowView
            fila = e.Row.DataItem
            e.Row.Cells(0).Text = e.Row.RowIndex + 1 'Me.GridView1.DataKeys(e.Row.RowIndex).Value
            e.Row.Attributes.Add("OnMouseOver", "Resaltar(1,this,'S')")
            e.Row.Attributes.Add("OnMouseOut", "Resaltar(0,this,'S')")
            'CType(e.Row.FindControl("cmdVer"), Button).Attributes.Add("OnClick", "AbrirPopUp('frmcambiardatosalumno.aspx?c=" & fila.Row("codigouniver_alu") & "&x=" & fila.Row("codigo_alu").ToString & "&id=" & ID & "','550','650','yes','yes');return(false);")
            'e.Row.Cells(7).Attributes.Add("onclick", "location.href='frmmodificarpostulante.aspx?id=" & Request.QueryString("id") & "&codigo_pot=" + Me.GridView1.DataKeys(e.Row.RowIndex).Value.ToString & "'")
            'CType(e.Row.FindControl("LnkModificar"), ImageButton).Attributes.Add("onclick", "MostrarPopUp('" + Me.GridView1.DataKeys(e.Row.RowIndex).Value.ToString + "')")
        End If
    End Sub

    Protected Sub dpDptoColegio_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dpDptoColegio.SelectedIndexChanged
        'If dpDpto.SelectedValue <> -2 Then
        Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("cnxBDUSAT").ConnectionString)
        ClsFunciones.LlenarListas(Me.dpProvinciaColegio, obj.TraerDataTable("ConsultarLugares", 3, Me.dpDptoColegio.SelectedValue, 0), "codigo_pro", "nombre_pro", "-Seleccione-")
        obj = Nothing
        'End If
    End Sub

    Protected Sub cmdNuevo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdNuevo.Click
        Me.hdCodigo_pot.Value = 0
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

        Try
            obj.IniciarTransaccion()
            codigo_pot = obj.Ejecutar("AgregarDatosPostulanteOtros", Me.hdCodigo_pot.Value, _
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
                RegisterStartupScript("MsjGuardar", "<script>alert('Los datos se han guardado correctamente')</script>")
                Me.mpeFicha.Hide()
                BuscarDirectorioEstudiantes()
            Else
                RegisterStartupScript("MsjGuardar", "<script>alert('Los datos que intenta registrar ya existen. Consulte la lista de registros.')</script>")
                Me.mpeFicha.Hide()
            End If
        Catch ex As Exception
            obj.AbortarTransaccion()
            obj = Nothing
            RegisterStartupScript("MsjGuardar", "<script>alert('Ocurrió un Error al Guardar los datos. Intente denuevo')</script>")
        End Try

    End Sub

    Protected Sub cmdCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdCancelar.Click
        Me.mpeFicha.Hide()
    End Sub

    Protected Sub cmdExportar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdExportar.Click
        Dim sb As StringBuilder = New StringBuilder()
        Dim SW As System.IO.StringWriter = New System.IO.StringWriter(sb)
        Dim htw As HtmlTextWriter = New HtmlTextWriter(SW)
        Dim Page As Page = New Page()
        Dim form As HtmlForm = New HtmlForm()
        Me.GridView1.EnableViewState = False
        Page.EnableEventValidation = False
        Page.DesignerInitialize()
        Page.Controls.Add(form)
        form.Controls.Add(Me.GridView1)
        Page.RenderControl(htw)
        Response.Clear()
        Response.Buffer = True
        Response.ContentType = "application/vnd.ms-excel"
        Response.AddHeader("Content-Disposition", "attachment;filename=directorioestudiantes.xls")
        Response.Charset = "UTF-8"
        Response.ContentEncoding = Encoding.Default
        Response.Write(sb.ToString())
        Response.End()
    End Sub
End Class
