
Partial Class academico_estudiante_separacion_Separacion
    Inherits System.Web.UI.Page

    Protected Sub CmdBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdBuscar.Click
        Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)        

        If Me.CmbBuscarpor.SelectedValue = 1 Then
            'Me.GvAlumnos.DataSource = obj.TraerDataTable("ConsultarAlumno", "NOM", Me.TxtBuscar.Text.ToString.Replace(" ", "%"))
            Me.GvAlumnos.DataSource = obj.TraerDataTable("ConsultaAlumno", "NOM", Me.TxtBuscar.Text.ToString.Replace(" ", "%"), Request.QueryString("mod"))
        Else
            'Me.GvAlumnos.DataSource = obj.TraerDataTable("ConsultarAlumno", "COD", Me.TxtBuscar.Text.ToString.Trim.Replace(" ", "%"))
            Me.GvAlumnos.DataSource = obj.TraerDataTable("ConsultaAlumno", "COD", Me.TxtBuscar.Text.ToString.Trim.Replace(" ", "%"), Request.QueryString("mod"))
        End If
        Me.GvAlumnos.DataBind()
        Me.gvHistorico.DataSource = Nothing
        Me.gvHistorico.DataBind()
        Panel1.Visible = False        
    End Sub

    Protected Sub CmbBuscarpor_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmbBuscarpor.SelectedIndexChanged
        If CmbBuscarpor.SelectedValue = 1 Then
            Me.TxtBuscar.MaxLength = 100
        Else
            Me.TxtBuscar.MaxLength = 10
        End If
    End Sub

    Protected Sub GvAlumnos_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GvAlumnos.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim Fila As Data.DataRowView
            Fila = e.Row.DataItem
            e.Row.Attributes.Add("OnMouseOver", "Resaltar(1,this,'S')")
            e.Row.Attributes.Add("OnMouseOut", "Resaltar(0,this,'S')")
            e.Row.Attributes.Add("OnClick", "javascript:__doPostBack('GvAlumnos','Select$" & e.Row.RowIndex & "')")
            If e.Row.Cells(4).Text = 1 Then
                e.Row.Cells(4).Text = "Activo"
            Else
                e.Row.Cells(4).Text = "Inactivo"
            End If
        End If
    End Sub

    Protected Sub GvAlumnos_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GvAlumnos.SelectedIndexChanged
        'Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
        Dim objCnx As New ClsConectarDatos
        Dim datos As New Data.DataTable
        Dim codigo_alu As Int32, codigo_pes As Int16
        Dim Ruta As New EncriptaCodigos.clsEncripta
        'Dim objFun As New ClsFunciones
        'Consulta plan de estudio por codigo_univer
        LimpiaDatos()
        objCnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        codigo_alu = Me.GvAlumnos.DataKeys.Item(Me.GvAlumnos.SelectedIndex).Values(0).ToString()
        Session("codigo_alu") = codigo_alu
        codigo_pes = Me.GvAlumnos.DataKeys.Item(Me.GvAlumnos.SelectedIndex).Values(1).ToString()
        objCnx.AbrirConexion()
        datos = objCnx.TraerDataTable("GyT_ConsultarPlanEstudioMatricula_V2", "PE", codigo_alu, codigo_pes) 'Me.GvAlumnos.Rows(Me.GvAlumnos.SelectedIndex).Cells(1).Text
        objCnx.CerrarConexion()

        If datos.Rows.Count > 0 Then
            With datos.Rows(0)
                Panel1.Visible = True
                Me.cboCicloAcademico.enabled = True
                Me.LblCodigoUniv.Text = .Item("codigouniver_alu").ToString

                Me.LblNombres.Text = .Item("nombres").ToString
                Me.LblPlanEstudio.Text = .Item("descripcion_pes").ToString
                Me.LblEstado.Text = Me.GvAlumnos.SelectedRow.Cells(4).Text
                Me.LblEstado.ForeColor = IIf(Me.GvAlumnos.SelectedRow.Cells(4).Text = "Activo", Drawing.Color.Blue, Drawing.Color.Red)
                If .Item("foto_alu") = 1 Then
                    '---------------------------------------------------------------------------------------------------------------
                    'Fecha: 29.10.2012
                    'Usuario: dguevara
                    'Modificacion: Se modifico el http://www.usat.edu.pe por http://intranet.usat.edu.pe
                    '---------------------------------------------------------------------------------------------------------------

                    ImgFoto.ImageUrl = "//intranet.usat.edu.pe/imgestudiantes/" & Ruta.CodificaWeb("069" & .Item("codigouniver_alu").ToString)
                Else
                    ImgFoto.ImageUrl = Request.ApplicationPath & "/images/Sin_foto.jpg"
                End If
                'Session("codigo_alu") = datos.Rows(0).Item("codigo_alu").ToString
                Me.Panel1.Visible = True
                CargarCombos()
            End With
        Else

        End If

        objCnx.AbrirConexion()
        gvHistorico.DataSource = objCnx.TraerDataTable("ACAD_ConsultarHistoricoSeparacion", Me.GvAlumnos.SelectedValue)
        'objFun.CargarListas(cboTipo, objCnx.TraerDataTable("ACAD_ConsultarTipoSeparacion"), "codigo_tse", "descripcion_tse")
        objCnx.TraerDataTable("ACAD_ConsultarHistoricoSeparacion", Me.GvAlumnos.SelectedValue)
        objCnx.CerrarConexion()
        gvHistorico.DataBind()
        Me.txtMotivo.Text = ""
        Me.txtFecha.Text = ""
        lnkHistorico_Click(sender, e)
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (Session("id_per") Is Nothing) Then
            Response.Redirect("../../../../sinacceso.html")
        End If

        If Not IsPostBack Then
            Me.pnlDatos.Visible = False
            Me.pnlHistorico.Visible = True
            Me.pnlAdministrar.Visible = False
        End If

        'Solo para dirección académica y Adm. de sistema
        If (Request.QueryString("ctf") = 1 Or Request.QueryString("ctf") = 85 _
            Or Request.QueryString("ctf") = 138 Or Request.QueryString("ctf") = 181) Then
            Me.lnkAdministrar.Visible = True
        Else
            Me.lnkAdministrar.Visible = False
        End If
    End Sub
    Private Sub CargarCombos()

        Dim objcnx As New ClsConectarDatos
        Dim objFun As New ClsFunciones
        objcnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        objcnx.AbrirConexion()
        objFun.CargarListas(Me.cboCicloAcademico, objcnx.TraerDataTable("ConsultarCicloAcademico", "DA", ""), "codigo_cac", "descripcion_cac")
        objcnx.CerrarConexion()

    End Sub

    Protected Sub lnkHistorico_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkHistorico.Click
        Try
            Dim objCnx As New ClsConectarDatos
            Dim objFun As New ClsFunciones
            objCnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            objCnx.AbrirConexion()
            gvHistorico.DataSource = objCnx.TraerDataTable("ACAD_ConsultarHistoricoSeparacion", Me.GvAlumnos.SelectedValue)
            objCnx.CerrarConexion()
            gvHistorico.DataBind()
            Me.pnlHistorico.Visible = True
            Me.pnlDatos.Visible = False
            Me.pnlAdministrar.Visible = False
            Me.lnkHistorico.ForeColor = Drawing.Color.Blue
            Me.lnkDatos.ForeColor = Drawing.Color.Purple
            Me.lnkHistorico.Font.Size = 10
            Me.lnkDatos.Font.Size = 8
            Me.lnkDatos.Font.Bold = False
            Me.lnkHistorico.Font.Bold = True
            Me.txtFecha.Enabled = False
        Catch ex As Exception
            Response.Write("Error: " & ex.Message)
        End Try        
    End Sub

    Protected Sub lnkDatos_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkDatos.Click

        Try

            Dim datos As New Data.DataTable
            Dim objCnx As New ClsConectarDatos
            Dim objFun As New ClsFunciones
            Dim verificar As New Data.DataTable
            Dim codigo_act As Integer = 0

            '### Consultar separación vigente ### 
            objCnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            objCnx.AbrirConexion()
            codigo_act = Me.cboCicloAcademico.SelectedValue
            verificar = objCnx.TraerDataTable("ConsultarCronogramaSeparacionv2", codigo_act, "REG. SEP", Request.QueryString("mod"))
            If verificar.Rows.Count > 0 Then
            
                If CType(Date.Now(), Date) >= CType(verificar.Rows(0).Item("fechaIni_Cro"), Date) And CType(Date.Now(), Date) <= CType(verificar.Rows(0).Item("fechaFin_Cro"), Date) Then

                    Me.cboCicloAcademico.enabled = False
                    Me.pnlHistorico.Visible = False
                    Me.pnlAdministrar.Visible = False
                    Me.pnlDatos.Visible = True
                    Me.txtMotivo.Text = ""
                    Me.txtFecha.Text = ""

                  

                    datos = objCnx.TraerDataTable("ACAD_ConsultarSeparacionVigente", Me.GvAlumnos.SelectedValue)
                    'objFun.CargarListas(cboTipo, objCnx.TraerDataTable("ACAD_ConsultarTipoSeparacionV2"), "codigo_tse", "descripcion_tse", "<-- Seleccione -->")
                    objFun.CargarListas(cboTipo, objCnx.TraerDataTable("ACAD_ConsultarTipoSeparacion", Request.QueryString("ctf")), "codigo_tse", "descripcion_tse", "<-- Seleccione -->")
                    objCnx.CerrarConexion()

                    'If datos.Rows.Count > 0 Then
                    '    lblNuevo.Text = "Vigente: "
                    '    lblNuevo.ForeColor = Drawing.Color.Green
                    '    With datos.Rows(0)
                    '        cboTipo.SelectedValue = .Item("codigo_tse")
                    '        If .Item("codigo_tse") = 2 Then
                    '            Me.txtFecha.Text = ""
                    '            Me.txtFecha.Enabled = False
                    '        Else
                    '            Me.txtFecha.Text = .Item("fechaFin_sep")
                    '            Me.txtFecha.Enabled = True
                    '        End If
                    '        Me.txtMotivo.Text = .Item("motivo_sep")
                    '        Me.txtNroResolucion.Text = .Item("nroResolucion")
                    '        Me.hddCodigo_sep.Value = .Item("codigo_sep")
                    '    End With
                    'Else
                    lblNuevo.Text = "Nuevo: "
                    lblNuevo.ForeColor = Drawing.Color.Blue
                    Me.cboTipo.SelectedValue = -1
                    Me.hddCodigo_sep.Value = 0
                    'End If

                    Me.lnkHistorico.ForeColor = Drawing.Color.Purple
                    Me.lnkDatos.ForeColor = Drawing.Color.Blue
                    Me.lnkHistorico.Font.Size = 8
                    Me.lnkDatos.Font.Size = 10
                    Me.lnkDatos.Font.Bold = True
                    Me.lnkHistorico.Font.Bold = False
                    Me.txtFecha.Text = Date.Now.AddYears(1).ToString("dd/MM/yyyy")
                    'Response.Write(Me.GvAlumnos.SelectedValue)



                Else
                    Me.cboCicloAcademico.enabled = True
                    ClientScript.RegisterStartupScript(Me.GetType, "Fuera", "alert('La separación de alumnos se podrá registrar dentro del siguiente rango de fechas: " + verificar.Rows(0).Item("fechaIni_Cro").ToString + " hasta el " + verificar.Rows(0).Item("fechaFin_Cro").ToString + "')", True)
                End If
            Else
                ClientScript.RegisterStartupScript(Me.GetType, "Rango", "alert('Aún no se ha registrado en el cronograma el rango de fechas para registro de Separaciones')", True)
            End If
        Catch ex As Exception
            Response.Write("No se pudo realizar la operación")
            'Response.Write(ex.Message.ToString)
        End Try
    End Sub

    Protected Sub cmdGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdGuardar.Click        
        Try
            Dim objCnx As New ClsConectarDatos
            Dim valoresdevueltos(1) As Boolean
            Dim rpta As Boolean


            If Me.cboCicloAcademico.SelectedValue = -1 Then

                ClientScript.RegisterStartupScript(Me.GetType, "Fuera", "alert('Seleccione Semestre Academico')", True)
                Exit Sub
            End If


            If Me.cboTipo.SelectedValue <> 2 Or Me.cboTipo.SelectedValue <> 4 Then
                If CDate(Me.txtFecha.Text) < Now.Date Then
                    ClientScript.RegisterStartupScript(Me.GetType, "Fecha", "alert('La fecha final de la separación debe ser mayor a la fecha actual')", True)
                    Exit Sub
                End If
            End If



            objCnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            objCnx.AbrirConexion()
            If Me.cboTipo.SelectedValue = 2 Or Me.cboTipo.SelectedValue = 4 Then 'separacion definitiva
                'ClientScript.RegisterStartupScript(Me.GetType, "Formulario", "window.open('frmSeparacionDefinitiva.aspx?codUniv=" & Me.LblCodigoUniv.Text & "&res=" & Me.txtNroResolucion.Text & "')", True)
                'Response.Write("<script>window.open('frmSeparacionDefinitiva.aspx?codUniv=" & Me.LblCodigoUniv.Text & "&res=" & Me.txtNroResolucion.Text & "&tipo=" & Me.cboTipo.SelectedValue & "&codSep=''')</script>")
                'objCnx.Ejecutar("ACAD_AgregarSeparacionAlumno", Me.txtMotivo.Text, DBNull.Value, Me.GvAlumnos.SelectedValue, Session("id_per"), Me.cboTipo.SelectedValue, Me.hddCodigo_sep.Value, Me.txtNroResolucion.Text, 0).copyto(valoresdevueltos, 0)
                objCnx.Ejecutar("ACAD_AgregarSeparacionAlumnov2", Me.cboCicloAcademico.SelectedValue, Me.txtMotivo.Text, DBNull.Value, Me.GvAlumnos.SelectedValue, Session("id_per"), Me.cboTipo.SelectedValue, Me.hddCodigo_sep.Value, Me.txtNroResolucion.Text, 0).copyto(valoresdevueltos, 0)
            Else
                'ClientScript.RegisterStartupScript(Me.GetType, "Formulario", "window.open('frmSeparacionDefinitiva.aspx?codUniv=" & Me.LblCodigoUniv.Text & "&res=" & Me.txtNroResolucion.Text & "')", True)
                'Response.Write("<script>window.open('frmSeparacionDefinitiva.aspx?codUniv=" & Me.LblCodigoUniv.Text & "&res=" & Me.txtNroResolucion.Text & "&tipo=" & Me.cboTipo.SelectedValue & "&codSep=''')</script>")
                'objCnx.Ejecutar("ACAD_AgregarSeparacionAlumno", Me.txtMotivo.Text, Me.txtFecha.Text, Me.GvAlumnos.SelectedValue, Session("id_per"), Me.cboTipo.SelectedValue, Me.hddCodigo_sep.Value, Me.txtNroResolucion.Text, 0).copyto(valoresdevueltos, 0)
                objCnx.Ejecutar("ACAD_AgregarSeparacionAlumnov2", Me.cboCicloAcademico.SelectedValue, Me.txtMotivo.Text, Me.txtFecha.Text, Me.GvAlumnos.SelectedValue, Session("id_per"), Me.cboTipo.SelectedValue, Me.hddCodigo_sep.Value, Me.txtNroResolucion.Text, 0).copyto(valoresdevueltos, 0)
            End If
            objCnx.CerrarConexion()
            rpta = valoresdevueltos(0)
            'Response.Write(rpta)
            If rpta = True Then
                Me.cboCicloAcademico.enabled = True
                ClientScript.RegisterStartupScript(Me.GetType, "Guardado", "alert('Se guardaron correctamente los datos')", True)
            Else
                ClientScript.RegisterStartupScript(Me.GetType, "Actualizado", "alert('Se actualizaron los datos de la separación vigente del estudiante')", True)
            End If
            lnkHistorico_Click(sender, e)
        Catch ex As Exception
            ClientScript.RegisterStartupScript(Me.GetType, "Error", "alert('" & ex.Message.Replace("'", " ").Replace(Chr(10), " ").Replace(Chr(13), " ") & "')", True)
        End Try
    End Sub

    Protected Sub cboTipo_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboTipo.SelectedIndexChanged
        If Me.cboTipo.SelectedValue = 2 Or Me.cboTipo.SelectedValue = 4 Then
            Me.rfvFecha.Enabled = False
            Me.txtFecha.Enabled = False
            Me.Label1.Visible = False
            'ClientScript.RegisterStartupScript(Me.GetType, "Actualizado", "document.form1.Button1.enabled=false;", True)
            RegisterStartupScript("k", "<script>document.form1.Button1.disabled=true;</script>")
        Else
            Me.rfvFecha.Enabled = True
            Me.txtFecha.Enabled = True
            Me.Label1.Visible = True
            'ClientScript.RegisterStartupScript(Me.GetType, "ok", "document.form1.Button1.enabled=true;", True)
            RegisterStartupScript("k", "<script>document.form1.Button1.disabled=false;</script>")
        End If
    End Sub

    Private Sub LimpiaDatos()
        Me.txtFecha.Text = ""
        Me.txtMotivo.Text = ""
        Me.txtNroResolucion.Text = ""
        Me.gvHistorico.DataSource = Nothing
        Me.gvHistorico.DataBind()
    End Sub

    
    Protected Sub gvHistorico_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvHistorico.RowDataBound
        'e.Row.Cells(6).Text = "VER"
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim pagina As String = ""
            Dim id As String = e.Row.Cells(8).Text

            If (e.Row.Cells(5).Text = False) Then
                e.Row.Cells(5).Text = "NO"
            Else
                e.Row.Cells(5).Text = "SI"
            End If

            '<script>window.open('frmSeparacionDefinitiva.aspx?codUniv=" & Me.LblCodigoUniv.Text & "&res=''&tipo=0&codSep=" & e.Row.Cells(7).Text & "')</script>
            pagina = "<a href='frmSeparacionDefinitiva.aspx?codUniv=" & Me.LblCodigoUniv.Text & "&tipo=0&codSep=" & id & "&res=0' target='_blank'>Resolución</a>"

            If (e.Row.Cells(5).Text = "SI") Then
                e.Row.Cells(8).Text = pagina
            Else
                e.Row.Cells(8).Text = ""
            End If
        End If
    End Sub

    Protected Sub lnkAdministrar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkAdministrar.Click        
        Dim objCnx As New ClsConectarDatos
        objCnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        Me.pnlDatos.Visible = False
        Me.pnlHistorico.Visible = False
        Me.pnlAdministrar.Visible = True
        Try
            objCnx.AbrirConexion()
            gvAdministrar.DataSource = objCnx.TraerDataTable("ACAD_ConsultarHistoricoSeparacion", Me.GvAlumnos.SelectedValue)
            objCnx.CerrarConexion()
            gvAdministrar.DataBind()

            BloquearDatosAdministrar(False)
        Catch ex As Exception
            Response.Write("Error: " & ex.Message)
        End Try
    End Sub

    Protected Sub gvAdministrar_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvAdministrar.SelectedIndexChanged
        Me.hddCodigo_sep.Value = Me.gvAdministrar.SelectedRow.Cells(0).Text
        Me.rbLista.Items(1).Selected = True
        BloquearDatosAdministrar(True)
    End Sub

    Protected Sub gvAdministrar_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvAdministrar.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            If (e.Row.Cells(4).Text = False) Then
                e.Row.Cells(4).Text = "NO"
            Else
                e.Row.Cells(4).Text = "SI"
            End If
        End If        
    End Sub

    Protected Sub btnActualiza_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnActualiza.Click
        Dim obj As New ClsConectarDatos
        Dim tipo As String = "E"
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        Try
            If (rbLista.Items(1).Selected = True) Then
                tipo = "D"

                If (Me.txtRevocatoria.Text.Trim = "") Then
                    Me.lblError.Text = "Debe ingresar la resolución"
                    Exit Sub
                End If
            End If

            obj.AbrirConexion()
            obj.Ejecutar("SEP_InactivaSeparacion", tipo, Me.hddCodigo_sep.Value, Me.txtRevocatoria.Text, Me.txtObservacion.Text, Session("id_per"))
            obj.CerrarConexion()

            Me.txtObservacion.Text = ""
            Me.rbLista.Items(1).Selected = True
            Me.lblError.Text = ""
            Me.hddCodigo_sep.Value = 0

            Response.Write("<script>alert('Separación actualizada')</script>")

            lnkAdministrar_Click(sender, e)
            Me.gvAdministrar.SelectedIndex = -1
        Catch ex As Exception
            Me.lblError.Text = "Error: " & ex.Message
        End Try
    End Sub

    Protected Sub gvAdministrar_SelectedIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSelectEventArgs) Handles gvAdministrar.SelectedIndexChanging
        If (gvAdministrar.SelectedIndex <> -1) Then
            gvAdministrar.Rows(gvAdministrar.SelectedIndex).BackColor = Drawing.Color.White
        End If
        gvAdministrar.Rows(e.NewSelectedIndex).BackColor = Drawing.Color.SkyBlue
    End Sub

    Private Sub BloquearDatosAdministrar(ByVal sw As Boolean)
        Me.rbLista.Enabled = sw
        Me.txtObservacion.Enabled = sw
        Me.btnActualiza.Enabled = sw
        Me.txtRevocatoria.Enabled = sw
    End Sub

    Protected Sub rbLista_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbLista.SelectedIndexChanged
        If (rbLista.Items(0).Selected = True) Then
            Me.txtRevocatoria.Enabled = False
            Me.txtRevocatoria.Text = ""
        Else
            Me.txtRevocatoria.Enabled = True
        End If
    End Sub
End Class
