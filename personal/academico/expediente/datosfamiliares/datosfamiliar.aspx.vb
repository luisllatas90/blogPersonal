
Partial Class datosfamiliar
    Inherits System.Web.UI.Page
    Public Accion As String ' N: nuevo M: Modificar
    Public codigo_dhab As Integer
    Dim idUsu As String


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Accion = Request.QueryString("tipo")
        Dim ObjCnx As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)

        If Accion = "N" Then
            Me.lbltipo.text = "REGISTRO"
        Else
            Me.lbltipo.text = "ACTUALIZACIÓN"
        End If
        codigo_dhab = Request.QueryString("field")
        idUsu = Request.QueryString("id")


        If Not IsPostBack Then
            txtDocumuento.Enabled = False
            txtNumeroFono.Enabled = True	'MNeciosup 30/10/2019
            chkUsat.Visible = False
            'ddlVinculoFamiliar.Enabled = False     'MNeciosup 23/01/2020

            'ClsFunciones.LlenarListas(Me.lstEstudiante, ObjCnx.TraerDataTable("FAM_ConsultarFamiliaUSAT", "AL"), "codigo_alu", "alumno")
            'ClsFunciones.LlenarListas(Me.lstPersonal, ObjCnx.TraerDataTable("FAM_ConsultarFamiliaUSAT", "PE"), "codigo_PER", "PERSONAL")

            Dim objCombos As New Combos
            objCombos.LlenaPais(Me.ddlNacionalidad)
            objCombos.LlenaDepartamento(Me.ddlDepartamento1)
            objCombos.LlenaProvincia(Me.ddlProvincia1, 0)
            objCombos.LlenaDistrito(Me.ddlDistrito1, 0)

            objCombos.LlenaDepartamento(Me.ddlDepartamento2)
            objCombos.LlenaProvincia(Me.ddlProvincia2, 0)
            objCombos.LlenaDistrito(Me.ddlDistrito2, 0)

            'Cargamos los combos del tipo de via
            objCombos.LlenaTipoVia(ddlTipoVia1)
            objCombos.LlenaTipoVia(ddlTipoVia2)

            'Cargamos los combos del tipo de zona.
            objCombos.LLenaTipoZona(ddlTipoZona1)
            objCombos.LLenaTipoZona(ddlTipoZona2)

            'Cargamos el tipo de documento identidad
            objCombos.LlenaTipoDocumento(Me.ddlTipoDocumento)

            'Cargamos Pais emisor del documento
            objCombos.LlenaPaisEmisor(ddlPaisEmisor)

            'Cargamos vinculo familiar
            objCombos.LlenaVinculoFamiliar(ddlVinculoFamiliar)

            'Documento que sustenta vinvulo familiar
            objCombos.LlenaDocumentoSustentaVinculoFamiliar(Me.ddlDocVinFamiliar)

            'Motivo Baja DH
            objCombos.LlenaMotivoBahaDH(Me.ddlMotivoBaja)

            'Codigos Larga Distancia
            objCombos.LlenaCodigoLargaDistancia(Me.ddlCodigoLargaDistancia)

            '-----------------------------------------'
            'Muestra los datos cuando se va modificar '
            '-----------------------------------------'
            If Accion = "M" Then
                CargarDatos()
            End If
        End If
    End Sub


    Private Function validaciones() As Boolean
        Dim obj As New clsInvestigacion
        Dim dts As New Data.DataTable
        Dim sw As Byte = 0

        sw = 1
        If (sw = 1) Then
            Return True
        End If
        Return False
    End Function

    Private Sub vEstadoControl(ByVal Estado As Boolean)
        Try
            ddlVinculoFamiliar.Enabled = Estado
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub CargarDatos()
        Dim ObjCnx As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
        Dim rs As New data.DataTable
        rs = ObjCnx.traerdatatable("FAM_ConsultarDatosFamilia", "DH", codigo_dhab)

        'vEstadoControl(False)              'MNeciosup 23/01/2020

        Me.txtPaterno.Text = rs.Rows(0).Item(0)
        Me.txtMaterno.Text = rs.Rows(0).Item(1)
        Me.txtnombres.text = rs.rows(0).item(2)
        Me.ddlSexo.selectedvalue = rs.rows(0).item(3)

        Me.rblVinculo.SelectedValue = rs.Rows(0).Item(4)

        'If rs.Rows(0).Item(4) = 5  Then           
        If rs.Rows(0).Item(4) = 5 Or rs.Rows(0).Item(4) = 6 Then    ''MNeciosup 24/01/2020
            Me.lblVinculo.Text = "HIJO(A)"
            '''ddlVinculoFamiliar.SelectedValue = 5                 
            ddlVinculoFamiliar.SelectedValue = rs.Rows(0).Item(4)   ''MNeciosup 24/01/2020
            Me.rblVinculo.SelectedValue = 5                         ''MNeciosup 24/01/2020
        End If
        If rs.Rows(0).Item(4) = 2 Then
            Me.lblVinculo.Text = "CONYUGE"
            ddlVinculoFamiliar.SelectedValue = 2
        End If

        '*** 28.01.2014 dguevara se agrego el tipo TUTORADO, el cual no es considerado para el TRegistro
        If rs.Rows(0).Item(4) = 1 Then
            Me.lblVinculo.Text = "TUTORADO"
            ddlVinculoFamiliar.SelectedValue = 1
        End If
        '**--

        If rs.Rows(0).Item(5) Is System.DBNull.Value Then
            Me.txtfecha.Text = ""
        Else
            Me.txtfecha.Text = rs.Rows(0).Item(5)
        End If

        If rs.Rows(0).Item(6) Is System.DBNull.Value Then
            Me.txtfechaMat.Text = ""
        Else
            Me.txtfechaMat.Text = rs.Rows(0).Item(6)
        End If
        Me.ddlEstudios.SelectedValue = rs.Rows(0).Item(7)

        'Agregado por Dguevara 28.08.2012 06:10pm
        txtDocumuento.Text = rs.Rows(0).Item("nrodocumento_dhab").ToString
        txtemail.Text = rs.Rows(0).Item("email_dhab").ToString
        ddlPaisEmisor.SelectedValue = rs.Rows(0).Item("paisEmisor_dhab").ToString
        ddlTipoDocumento.SelectedValue = rs.Rows(0).Item("tipodocumento_dhab").ToString
        txtFechaAlta.Text = rs.Rows(0).Item("fechaAlta_dhab").ToString
        ddlSituacion.SelectedValue = rs.Rows(0).Item("situacion_dhab").ToString
        ddlVinculoFamiliar.SelectedValue = rs.Rows(0).Item("vinculoFamiliar_dhab")
        ddlDocVinFamiliar.SelectedValue = rs.Rows(0).Item("tipdocSustentaVFam_dhab").ToString
        txtNumeroDocumento.Text = rs.Rows(0).Item("nrodocSustentaVFam_dhab").ToString
        ddlNacionalidad.SelectedValue = rs.Rows(0).Item("nacionalidad_dhab").ToString
        ddlIndicadorDomicilio.SelectedValue = rs.Rows(0).Item("indicadorDomicilio_dhab").ToString
        txtFechaBaja.Text = rs.Rows(0).Item("fechaBaja_dhab").ToString
        ddlMotivoBaja.SelectedValue = rs.Rows(0).Item("tipoBaja_dhab").ToString
        ddlTipoVia1.SelectedValue = rs.Rows(0).Item("domicilioTipoVia_dhab").ToString
        txtNombreVia.Text = rs.Rows(0).Item("domicilioNombreVia_dhab").ToString
        txtNumero1.Text = rs.Rows(0).Item("domicilioNroVia_dhab").ToString
        txtInterior1.Text = rs.Rows(0).Item("domicilioInterior_dhab").ToString
        ddlTipoZona1.SelectedValue = rs.Rows(0).Item("domicilioTipoZona_dhab").ToString
        txtNombreZona1.Text = rs.Rows(0).Item("domicilioNombreZona_dhab").ToString
        txtReferencia1.Text = rs.Rows(0).Item("domicilioReferencia_dhab").ToString
        ddlDepartamento1.SelectedValue = rs.Rows(0).Item("domicilioUbiGeo_dhab").ToString

        '------------------------------------------------------------------------------------------
        'Ubigeo direccion - Direcion 01
        '------------------------------------------------------------------------------------------
        Dim obj As New Personal
        Dim objcombo As New Combos
        Dim dts As New Data.DataTable
        Dim dtsx As New Data.DataTable

        If rs.Rows(0).Item("domicilioUbiGeo_dhab").ToString <> "" Then
            dts = obj.DatosUbigeo(rs.Rows(0).Item("domicilioUbiGeo_dhab"))
            If dts.Rows.Count > 0 Then
                With dts.Rows(0)
                    'Cargamos el departamento
                    ddlDepartamento1.SelectedValue = .Item("codigo_Dep")
                    'Cargamos la lista de provincias que tiene el departamento
                    objcombo.LlenaProvincia(ddlProvincia1, .Item("codigo_Dep"))
                    'Apuntams la provincia que le corresponde.
                    ddlProvincia1.SelectedValue = .Item("codigo_Pro")
                    'Cargamos la lista de distritos que tiene esa provincia.
                    objcombo.LlenaDistrito(ddlDistrito1, .Item("codigo_Pro"))
                    'Apuntamos el distrito que tiene registrado
                    ddlDistrito1.SelectedValue = .Item("codigo_Dis")
                End With
            End If
        Else
            ddlDepartamento1.SelectedValue = 0
            ddlProvincia1.SelectedValue = 0
            ddlDistrito1.SelectedValue = 0
        End If


        ddlTipoVia2.SelectedValue = rs.Rows(0).Item("domicilioTipoVia2_dhab").ToString
        txtNombreVia2.Text = rs.Rows(0).Item("domicilioNombreVia2_dhab").ToString
        txtNumero2.Text = rs.Rows(0).Item("domicilioNroVia2_dhab").ToString
        txtInterior2.Text = rs.Rows(0).Item("domicilioInterior2_dhab").ToString
        ddlTipoZona2.SelectedValue = rs.Rows(0).Item("domicilioTipoZona2_dhab").ToString
        txtNombreZona2.Text = rs.Rows(0).Item("domicilioNombreZona2_dhab").ToString
        txtReferencia2.Text = rs.Rows(0).Item("domicilioReferencia2_dhab").ToString

        'Direccion 2
        If rs.Rows(0).Item("domicilioUbiGeo2_dhab").ToString <> "" Then
            dtsx = obj.DatosUbigeo(rs.Rows(0).Item("domicilioUbiGeo2_dhab"))
            If dtsx.Rows.Count > 0 Then
                With dtsx.Rows(0)
                    'Cargamos el departamento
                    ddlDepartamento2.SelectedValue = .Item("codigo_Dep")
                    'Cargamos la lista de provincias que tiene el departamento
                    objcombo.LlenaProvincia(ddlProvincia2, .Item("codigo_Dep"))
                    'Apuntams la provincia que le corresponde.
                    ddlProvincia2.SelectedValue = .Item("codigo_Pro")
                    'Cargamos la lista de distritos que tiene esa provincia.
                    objcombo.LlenaDistrito(ddlDistrito2, .Item("codigo_Pro"))
                    'Apuntamos el distrito que tiene registrado
                    ddlDistrito2.SelectedValue = .Item("codigo_Dis")
                End With
            End If
        Else
            ddlDepartamento2.SelectedValue = 0
            ddlProvincia2.SelectedValue = 0
            ddlDistrito2.SelectedValue = 0
        End If

        ddlCodigoLargaDistancia.SelectedValue = rs.Rows(0).Item("codigoLdn_dhab").ToString
        txtNumeroFono.Text = rs.Rows(0).Item("telefono_dhab").ToString
    End Sub

    Protected Sub cmdCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdCancelar.Click

        'If MsgBox("¿Está seguro que desea cancelar el registro del familiar?", MsgBoxStyle.YesNo, "Campus Virtual") = MsgBoxResult.Yes Then
        Response.Redirect("datosfamiliares.aspx?id=" & Request.QueryString("id"))
        '            Page.RegisterStartupScript("CANCEL", "<SCRIPT>document.all.cmdFechaMat.disabled='disabled'</SCRIPT>")
        'End If
    End Sub


    Private Sub GuardarDerechoHabientes()

        Dim ObjCnx As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
        Dim tipo As String
        Dim idDet As Object
        Dim TipoPersona As Object
        Dim CodigoUsat As Object
        Dim vinculo As Object


        If chkUsat.Checked = True Then
            If chkUsat.Text = "Personal USAT" Then
                TipoPersona = "P"
            Else
                TipoPersona = "E"
            End If
            CodigoUsat = Me.txtCodigo.Text
        Else
            TipoPersona = System.DBNull.Value
            CodigoUsat = System.DBNull.Value
        End If


        idDet = Request.QueryString("field")
        tipo = Request.QueryString("tipo")
        Response.Write(tipo)

        Dim fechamat As Object
        If idDet Is Nothing Then
            idDet = System.DBNull.Value
        End If

        If Me.txtfechaMat.Text.Trim = "" Then
            fechamat = System.DBNull.Value
        Else
            fechamat = Me.txtfechaMat.Text.Trim
        End If

        'Fecha Nacimiento
        Dim fechanac As Object
        If txtfecha.Text.Trim = "" Then
            fechanac = System.DBNull.Value
        Else
            fechanac = txtfecha.Text.Trim
        End If

        Dim fechaAlta As Object
        If txtFechaAlta.Text.Trim = "" Then
            fechaAlta = System.DBNull.Value
        Else
            fechaAlta = txtFechaAlta.Text.Trim
        End If

        Dim fechaBaja As Object
        If txtFechaBaja.Text.Trim = "" Then
            fechaBaja = System.DBNull.Value
        Else
            fechaBaja = txtFechaBaja.Text.Trim
        End If
        'rblVinculo.SelectedValue, _

        ' Response.Write(Request.QueryString("id"))
        ObjCnx.Ejecutar("FAM_ActualizarDatosFamiliares", tipo, _
                        idDet, _
                        Me.txtPaterno.Text, _
                        Me.txtMaterno.Text, _
                        Me.txtNombres.Text, _
                        fechanac, _
                        Me.ddlSexo.SelectedValue, _
                        ddlVinculoFamiliar.SelectedValue, _
                        TipoPersona, _
                        CodigoUsat, _
                        Me.ddlEstudios.SelectedValue, _
                        fechamat, _
                        idUsu, _
                        ddlTipoDocumento.SelectedValue, _
                        txtDocumuento.Text.Trim, _
                        ddlPaisEmisor.SelectedValue, _
                        ddlDocVinFamiliar.SelectedValue, _
                        txtNumeroDocumento.Text, _
                        fechaAlta, _
                        ddlSituacion.SelectedValue, _
                        ddlMotivoBaja.SelectedValue, _
                        fechaBaja, _
                        ddlIndicadorDomicilio.SelectedValue, _
                        ddlNacionalidad.SelectedValue, _
                        ddlTipoVia1.SelectedValue, _
                        txtNombreVia.Text.Trim, _
                        txtNumero1.Text.Trim, _
                        txtInterior1.Text.Trim, _
                        ddlTipoZona1.SelectedValue, _
                        txtNombreZona1.Text.Trim, _
                        txtReferencia1.Text.Trim, _
                        ddlDistrito1.SelectedValue, _
                        ddlTipoVia2.SelectedValue, _
                        txtNombreVia2.Text.Trim, _
                        txtNumero2.Text.Trim, _
                        txtInterior2.Text.Trim, _
                        ddlTipoZona2.SelectedValue, _
                        txtNombreZona2.Text, _
                        txtReferencia2.Text.Trim, _
                        ddlDistrito2.SelectedValue, _
                        txtemail.Text.Trim, _
                        ddlCodigoLargaDistancia.SelectedValue, _
                        txtNumeroFono.Text.Trim)
        Response.Redirect("datosfamiliares.aspx?act=" & tipo & "&id=" & idUsu)
    End Sub

    Protected Sub cmdAceptar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdAceptar.Click
        '-----------------------------------------------------------------------------------------------
        ' Verifica que el trabajador haya aceptado la declaracion jurada para el registro de sus datos
        '-----------------------------------------------------------------------------------------------
        Dim objPersonal As New Personal
        Dim dts As New Data.DataTable
        objPersonal.codigo = Request.QueryString("id")
        dts = objPersonal.VerificaDeclaracionJuradaPersonal(Request.QueryString("id"))
        If dts.Rows.Count > 0 Then
            If dts.Rows(0).Item("rpt") = 0 Then
                Me.lblDeclarante.Text = dts.Rows(0).Item("Declarante").ToString
                mpeInforme.Show()
            Else
                GuardarDerechoHabientes()
            End If
        End If
        '--------------------
    End Sub

    'Se ejecuta cuando acepta el modal
    Protected Sub btnGuardarInforme_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardarInforme.Click
        Try
            Dim objPersonal As New Personal
            Dim i As Integer = objPersonal.ActualizarEstadoDeclaracionJurada(Request.QueryString("id"))
            GuardarDerechoHabientes()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    'Cancela la declaracion jurada.
    Protected Sub btnCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        ClientScript.RegisterStartupScript(Me.GetType, "FaltanDatos", "alert('Los datos consignados no fueron registrados.');", True)
    End Sub

    Protected Sub lnkEstudiante_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkEstudiante.Click
        Me.PanelEstudiante.Visible = True
        Me.PanelPersonal.Visible = False
        Me.chkUsat.Visible = True
        Me.chkUsat.Checked = True
        Me.chkUsat.Text = "Estudiante USAT"
        Me.txtNombres.Text = ""
        Me.txtPaterno.Text = ""
        Me.txtMaterno.Text = ""
        Me.txtCodigo.Text = ""
    End Sub

    Protected Sub lnkPersonal_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkPersonal.Click
        Me.PanelEstudiante.Visible = False
        Me.PanelPersonal.Visible = True
        Me.chkUsat.Visible = True
        Me.chkUsat.Checked = True
        Me.chkUsat.Text = "Personal USAT"
        Me.txtNombres.Text = ""
        Me.txtPaterno.Text = ""
        Me.txtMaterno.Text = ""
        Me.txtCodigo.Text = ""
    End Sub

    Protected Sub chkUsat_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkUsat.CheckedChanged
        If Me.chkUsat.Checked = False Then
            Me.PanelPersonal.Visible = False
            Me.PanelEstudiante.Visible = False
            Me.txtCodigo.Text = ""
            Me.txtPaterno.Text = ""
            Me.txtMaterno.Text = ""
            Me.txtNombres.Text = ""
            Me.chkUsat.Visible = False
        End If
    End Sub




    Protected Sub cmdBuscarPersonal_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdBuscarPersonal.Click
        Dim ObjCnx As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
        Me.dgvAlumnos.DataSourceID = Nothing
        Me.dgvAlumnos.DataSource = ObjCnx.TraerDataTable("FAM_ConsultarFamiliaUSAT", "AL", txtAlumno.Text)
        Me.dgvAlumnos.DataBind()

    End Sub


    Protected Sub dgvAlumnos_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles dgvAlumnos.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim fila As Data.DataRowView
            fila = e.Row.DataItem
            'e.Row.ID = "tab" & e.Row.RowIndex.ToString
            e.Row.Attributes.Add("OnMouseOver", "Resaltar(1,this,'S')")
            e.Row.Attributes.Add("OnMouseOut", "Resaltar(0,this,'S')")
            e.Row.Attributes.Add("OnClick", "javascript:__doPostBack('dgvAlumnos','Select$" & e.Row.RowIndex & "'); SeleccionarFila();")
            e.Row.Attributes.Add("Class", "Sel")
            e.Row.Attributes.Add("Typ", "Sel")
            e.Row.Style.Add("cursor", "hand")
        End If
    End Sub

    Protected Sub dgvAlumnos_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgvAlumnos.SelectedIndexChanged

        Me.txtCodigo.Text = Me.dgvAlumnos.DataKeys.Item(Me.dgvAlumnos.SelectedIndex).Value  'Server.HtmlDecode(Me.dgvAlumnos.SelectedRow.Cells(0).Text)
        Me.txtPaterno.Text = Server.HtmlDecode(Me.dgvAlumnos.SelectedRow.Cells(1).Text)
        Me.txtMaterno.Text = Server.HtmlDecode(Me.dgvAlumnos.SelectedRow.Cells(2).Text)
        Me.txtNombres.Text = Server.HtmlDecode(Me.dgvAlumnos.SelectedRow.Cells(3).Text)
        Me.txtfecha.Text = Server.HtmlDecode(Me.dgvAlumnos.SelectedRow.Cells(5).Text)
        If Server.HtmlDecode(Me.dgvAlumnos.SelectedRow.Cells(4).Text) = "MASCULINO" Then
            Me.ddlSexo.SelectedValue = 1
        Else
            Me.ddlSexo.SelectedValue = 2
        End If
    End Sub

    Protected Sub cmdBuscarPersonal0_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdBuscarPersonal0.Click
        Dim ObjCnx As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
        Me.dgvPersonal.DataSourceID = Nothing
        Me.dgvPersonal.DataSource = ObjCnx.TraerDataTable("FAM_ConsultarFamiliaUSAT", "PE", txtpersonal.Text)
        Me.dgvPersonal.DataBind()
    End Sub

    Protected Sub dgvPersonal_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles dgvPersonal.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim fila As Data.DataRowView
            fila = e.Row.DataItem
            'e.Row.ID = "tab" & e.Row.RowIndex.ToString
            e.Row.Attributes.Add("OnMouseOver", "Resaltar(1,this,'S')")
            e.Row.Attributes.Add("OnMouseOut", "Resaltar(0,this,'S')")
            e.Row.Attributes.Add("OnClick", "javascript:__doPostBack('dgvPersonal','Select$" & e.Row.RowIndex & "'); SeleccionarFila();")
            e.Row.Attributes.Add("Class", "Sel")
            e.Row.Attributes.Add("Typ", "Sel")
            e.Row.Style.Add("cursor", "hand")
        End If
    End Sub

    Protected Sub dgvPersonal_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgvPersonal.SelectedIndexChanged
        Me.txtCodigo.Text = Me.dgvPersonal.DataKeys.Item(Me.dgvPersonal.SelectedIndex).Value  'Server.HtmlDecode(Me.dgvAlumnos.SelectedRow.Cells(0).Text)
        Me.txtPaterno.Text = Server.HtmlDecode(Me.dgvPersonal.SelectedRow.Cells(1).Text)
        Me.txtMaterno.Text = Server.HtmlDecode(Me.dgvPersonal.SelectedRow.Cells(2).Text)
        Me.txtNombres.Text = Server.HtmlDecode(Me.dgvPersonal.SelectedRow.Cells(3).Text)
        Me.txtfecha.Text = Server.HtmlDecode(Me.dgvPersonal.SelectedRow.Cells(5).Text)
        If Server.HtmlDecode(Me.dgvPersonal.SelectedRow.Cells(4).Text) = "MASCULINO" Then
            Me.ddlSexo.SelectedValue = 1
        Else
            Me.ddlSexo.SelectedValue = 2
        End If

    End Sub

    Protected Sub rblVinculo_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rblVinculo.SelectedIndexChanged
        If Me.rblVinculo.SelectedValue = 2 Then
            Me.lblVinculo.Text = "CONYUGE"
            'ddlVinculoFamiliar.Enabled = False             'MNeciosup 23/01/2020
            ddlVinculoFamiliar.SelectedValue = 2
        End If

        If Me.rblVinculo.SelectedValue = 5 Then
            Me.lblVinculo.Text = "HIJO(A)"
            'ddlVinculoFamiliar.Enabled = False             'MNeciosup 23/01/2020
            ddlVinculoFamiliar.SelectedValue = 5
        End If

        '** 28.01.2014 dguevara **
        If Me.rblVinculo.SelectedValue = 0 Then
            Me.lblVinculo.Text = "TUTORADO"
            'ddlVinculoFamiliar.Enabled = False             'MNeciosup 23/01/2020
            ddlVinculoFamiliar.SelectedValue = 1
        End If
    End Sub

    Protected Sub ddlDepartamento1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlDepartamento1.SelectedIndexChanged
        Try
            Dim objCombos As New Combos
            If ddlDepartamento1.SelectedValue <> 0 Then
                objCombos.LlenaProvincia(Me.ddlProvincia1, ddlDepartamento1.SelectedValue)
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub ddlProvincia1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlProvincia1.SelectedIndexChanged
        Try
            Dim objCombos As New Combos
            If ddlDepartamento1.SelectedValue <> 0 And ddlProvincia1.SelectedValue <> 0 Then
                objCombos.LlenaDistrito(Me.ddlDistrito1, ddlProvincia1.SelectedValue)
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub ddlDepartamento2_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlDepartamento2.SelectedIndexChanged
        Try
            Dim objCombos As New Combos
            If ddlDepartamento2.SelectedValue <> 0 Then
                objCombos.LlenaProvincia(Me.ddlProvincia2, ddlDepartamento2.SelectedValue)
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub ddlProvincia2_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlProvincia2.SelectedIndexChanged
        Try
            Dim objCombos As New Combos
            If ddlDepartamento2.SelectedValue <> 0 And ddlProvincia2.SelectedValue <> 0 Then
                objCombos.LlenaDistrito(Me.ddlDistrito2, ddlProvincia2.SelectedValue)
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub ddlTipoDocumento_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlTipoDocumento.SelectedIndexChanged
        Try
            If ddlTipoDocumento.SelectedValue = 1 Then
                txtDocumuento.Enabled = True
                txtDocumuento.MaxLength = 8         'DNI
                ddlPaisEmisor.Enabled = False
                ddlPaisEmisor.SelectedValue = "604" 'PERU
                txtDocumuento.Focus()
            Else
                ddlPaisEmisor.Enabled = True
                ddlPaisEmisor.SelectedValue = "0"
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub ddlCodigoLargaDistancia_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlCodigoLargaDistancia.SelectedIndexChanged
        Try
            txtNumeroFono.Enabled = True
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub
End Class
