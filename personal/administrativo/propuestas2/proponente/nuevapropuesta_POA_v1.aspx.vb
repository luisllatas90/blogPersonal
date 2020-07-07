Partial Class nuevapropuesta
    Inherits System.Web.UI.Page

    Dim qtyTotal As Decimal = 0
    Dim grQtyTotal As Decimal = 0
    Dim storid As Integer = 0
    Dim rowIndex As Integer = 1

    Dim subtotal As Decimal = 0
    Dim subtotalIngreso As Decimal = 0

    Dim PorcentajeEgreso As Decimal = 0

    Dim LastTipo As String = String.Empty
    Dim LastTipoIngreso As String = String.Empty

    Dim LastActividad As String = String.Empty
    Dim LastActividadIngreso As String = String.Empty

    Dim CurrentRow As Integer = -1
    Dim CurrentRow2 As Integer = -1

    Dim CurrentRowIngreso As Integer = -1
    Dim CurrentRow2Ingreso As Integer = -1

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim idUsu As Integer
        idUsu = Request.QueryString("idUsu")
        Me.lblIdUsuario.Text = idUsu

        If Not IsPostBack Then
            Try
                txtPropuesta.Visible = False
                ddl_propuesta.Visible = True

                If Request.QueryString("codigo_prp") <> "" Then
                    Me.lblIdPropuesta.Text = Request.QueryString("codigo_prp")
                End If

                Dim ObjCnx As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
                Dim rsMoneda As New Data.DataTable
                Dim rsCambio As New Data.DataTable
                Dim rsCentroCostos As New Data.DataTable
                Dim rsCentroCostosUsuario As New Data.DataTable
                Dim rsTipoPropuesta As New Data.DataTable
                Dim rsFacultad As New Data.DataTable

                ' --------------------
                ' consultar los datos de la última versión de la propuesta
                Dim UltimaVersion As Integer
                Dim rsVersiones As New Data.DataTable
                If Request.QueryString("codigo_prp") <> "" Then
                    rsVersiones = ObjCnx.TraerDataTable("CONSULTARVERSIONESPROPUESTA", "ES", Me.lblIdPropuesta.Text, 0)
                    UltimaVersion = rsVersiones.Rows(0).Item("version_dap")
                    If rsVersiones.Rows(0).Item("instancia_Prp") = "P" Then
                        Response.Write("<b>Registro de propuesta</B>")
                    Else
                        Response.Write("<B>Registro de la Versión " & UltimaVersion + 1 & " de la propuesta</B>")
                    End If
                End If
                ' --------------------

                'consulta de la moneda y tipo de cambio
                rsMoneda = ObjCnx.TraerDataTable("consultarMoneda", 1, "")
                ClsFunciones.LlenarListas(Me.ddlMoneda, rsMoneda, "codigo_tip", "descripcion_tip")
                lblMoneda.Text = "SOLES"
                ddlMoneda.Visible = False

                rsCambio = ObjCnx.TraerDataTable("ConsultarCambioDelDia", ddlMoneda.SelectedValue)
                Me.lblTipoCambioSimbolo.Text = rsCambio.Rows(0).Item(2) & " "
                Me.lblTipoCambio.Text = FormatNumber(rsCambio.Rows(0).Item(3), 2)

                ''Listar Proyectos POA
                Dim dtProyecto As New Data.DataTable
                If Request.QueryString("accion") = "M" Then
                    dtProyecto = ObjCnx.TraerDataTable("PRO_listaProyectosPoa2", idUsu, Request.QueryString("codigo_prp"))
                Else
                    dtProyecto = ObjCnx.TraerDataTable("PRO_listaProyectosPoa", idUsu, 0)
                End If

                If dtProyecto.Rows.Count = 0 Then
                    Response.Write("<script>alert('No se han Registrado Proyectos con este Usuario')</script>")
                    Response.Redirect("contenido_POA_v1.aspx?id=" & Me.lblIdUsuario.Text & "&instancia=" & Request.QueryString("instancia"))
                    Return
                Else
                    ClsFunciones.LlenarListas(Me.ddl_propuesta, dtProyecto, "codigo_acp", "resumen_acp")
                End If

                'Muestra si es Rectorado o Facultad
                Dim dttDerivar As New Data.DataTable
                dttDerivar = ObjCnx.TraerDataTable("PRP_Consultar_Derivar", ddl_propuesta.SelectedValue.ToString)
                If dttDerivar.Rows.Count > 0 Then
                    lblFacultad.Text = "FACULTAD"
                    lblFacultadID.Text = dttDerivar.Rows(0).Item("codigo_Fac")
                Else
                    lblFacultad.Text = "RECTORADO"
                    lblFacultadID.Text = 0
                End If

                '' ''Listar Proyectos POA
                Dim dtArea As New Data.DataTable
                dtArea = ObjCnx.TraerDataTable("PRO_listaAreasPoa", ddl_propuesta.SelectedValue)
                If dtArea.Rows.Count = 0 Then

                Else
                    Me.hdcodigo_cco.Value = dtArea.Rows(0).Item("codigo_cco")
                    Me.lblArea.Text = dtArea.Rows(0).Item("nombre_poa")
                End If

                ''consulta del tipo de propuesta
                rsTipoPropuesta = ObjCnx.TraerDataTable("ConsultarTipoPropuestas", "TC")
                ClsFunciones.LlenarListas(Me.ddlTipoPropuesta, rsTipoPropuesta, "codigo_tpr", "descripcion_tpr")

                'consultar facultad
                rsFacultad = ObjCnx.TraerDataTable("ConsultarFacultad", "AR", Me.ddlCentroCosto.SelectedValue)

                If rsFacultad.Rows.Count = 0 Then
                    Me.lblFacultadID.Text = 0
                    Me.lblFacultad.Text = "Usted no está asignado a ninguna facultad"
                    Me.cmdEnviar.Enabled = False
                    Me.cmdAdjuntar.Enabled = False
                    Me.lblFacultad.ForeColor = Drawing.Color.Red
                    Me.cmdGuardar.Enabled = False
                    Me.CmdAgregar.Enabled = False
                Else
                    Me.cmdEnviar.Enabled = True
                    Me.cmdAdjuntar.Enabled = True
                    Me.lblFacultad.ForeColor = Drawing.Color.Black
                    Me.cmdGuardar.Enabled = True
                    Me.CmdAgregar.Enabled = True
                End If

                Me.lbl_Propuesta.Visible = False
                'Call wf_ocultaControles(True)

                If Request.QueryString("accion") = "M" Then
                    Dim rsVersion As New Data.DataTable

                    rsVersion = ObjCnx.TraerDataTable("CONSULTARVERSIONESPROPUESTA", "DA", Me.lblIdPropuesta.Text, UltimaVersion)
                    Me.ddl_propuesta.SelectedValue = rsVersion.Rows(0).Item("codigo_acp")

                    Me.ddl_propuesta.Visible = False
                    Me.lbl_Propuesta.Visible = True
                    Me.lbl_Propuesta.Text = ddl_propuesta.SelectedItem.ToString
                    txtPropuesta.Text = rsVersion.Rows(0).Item("nombre_prp")

                    'Response.Write("<script>alert('" & rsVersion.Rows(0).Item("codigo_tpr") & "')</script>")
                    Me.ddlTipoPropuesta.SelectedValue = rsVersion.Rows(0).Item("codigo_tpr")

                    Me.txtResumen.Text = rsVersion.Rows(0).Item("beneficios_dap")
                    Me.txtImportancia.Text = rsVersion.Rows(0).Item("importancia_dap")
                    Me.txtIngreso.Text = rsVersion.Rows(0).Item("ingreso_dap")
                    Me.txtEgreso.Text = rsVersion.Rows(0).Item("egreso_dap")
                    Me.lblUtilidad.Text = rsVersion.Rows(0).Item("utilidad_dap")
                    Me.lblIngresoMN.Text = FormatNumber(CDbl(rsVersion.Rows(0).Item("ingresoMN_dap")))
                    Me.lblEgresoMN.Text = FormatNumber(CDbl(rsVersion.Rows(0).Item("egresoMN_dap")))
                    Me.lblUtilidadMN.Text = FormatNumber(CDbl(rsVersion.Rows(0).Item("utilidadMN_dap")))
                    Me.lblTipoCambio.Text = rsVersion.Rows(0).Item("tipocambio_dap")
                    Me.ddlMoneda.SelectedValue = rsVersion.Rows(0).Item("codigo_tip")
                    Me.lblTipoCambioSimbolo.Text = rsVersion.Rows(0).Item("simbolo_moneda")

                    'Obtener el tipo de Propuesta correcto
                    If ddlTipoPropuesta.SelectedValue.ToString = 3 Or ddlTipoPropuesta.SelectedValue.ToString = 17 Then
                        Me.lbl_Propuesta.Visible = False
                        txtPropuesta.Visible = True
                        ddl_propuesta.Visible = True

                        ddl_propuesta.Visible = True
                        lbl_Propuesta.Visible = False

                        imgPresupuesto.Visible = False
                        btn_MostrarPresupuesto.Visible = False

                        Me.TDPresupuesto.Attributes.Remove("Style")
                        Me.TDPresupuesto.Attributes.Add("Style", "display:none")

                        Me.TDMargen.Attributes.Remove("Style")
                        Me.TDMargen.Attributes.Add("Style", "display:none")

                        Me.TDRentabilidad.Attributes.Remove("Style")
                        Me.TDRentabilidad.Attributes.Add("Style", "display:none")

                        Me.TDImportes.Attributes.Remove("Style")
                        Me.TDImportes.Attributes.Add("Style", "display:none")

                        dgv_PresupuestoIngreso.Visible = False
                        dgv_Presupuesto.Visible = False

                    ElseIf ddlTipoPropuesta.SelectedValue.ToString = 0 Then
                        txtPropuesta.Visible = False
                        ddl_propuesta.Visible = False
                        Me.lbl_Propuesta.Visible = False
                    Else
                        Me.lbl_Propuesta.Visible = False
                        txtPropuesta.Visible = False
                        ddl_propuesta.Visible = True
                        wf_mostraretiquetapresupuesto()
                    End If

                    If Request.QueryString("instancia") = "P" Then
                        Dim rsDatos As New Data.DataTable
                        Dim codigo_dap, codigo_cop, codigo_dip As Integer

                        rsDatos = ObjCnx.TraerDataTable("PRP_ConsultarDatosCodigosPropuesta", Me.lblIdPropuesta.Text, UltimaVersion)
                        If rsDatos.Rows(0).Item("codigo_dap").ToString <> "" Then
                            codigo_dap = rsDatos.Rows(0).Item("codigo_dap")
                            codigo_cop = rsDatos.Rows(0).Item("codigo_cop")
                            codigo_dip = 0

                            Call ListarArchivos(codigo_cop, codigo_dap, codigo_dip)
                        End If
                    End If
                End If

                Call wf_ObtenerImportes()

                If dgv_PresupuestoIngreso.Rows.Count = 0 Then
                    lblMsgRentabilidad.Visible = True
                    lblMargen.Visible = False
                    lblRentabilidad.Visible = False

                    lblMsgUtilidad.Visible = True
                    lblUtilidadMN.Visible = False
                Else
                    ''Obtener Margen y Rentabilidad
                    Dim dtMargen As New Data.DataTable
                    dtMargen = ObjCnx.TraerDataTable("PRP_ObtenerMargen", ddl_propuesta.SelectedValue.ToString)
                    lblMargen.Text = FormatNumber(CDbl(dtMargen.Rows(0).Item("margen")))

                    lblRentabilidad.Text = dtMargen.Rows(0).Item("rentabilidad").ToString
                    If lblRentabilidad.Text >= 35 Then
                        lblRentabilidad.ForeColor = Drawing.Color.Blue
                    Else
                        lblRentabilidad.ForeColor = Drawing.Color.Red
                    End If

                    lblMsgRentabilidad.Visible = False
                    lblMargen.Visible = True
                    lblRentabilidad.Visible = True

                    lblMsgUtilidad.Visible = False
                    lblUtilidadMN.Visible = True
                End If
            Catch ex As Exception
                Response.Write(ex.Message)
            End Try

        End If
        If Request.QueryString("accion") = "M" And Request.QueryString("instancia") = "P" Then
            Me.cmdEnviar.Enabled = True
        Else
            Me.cmdEnviar.Enabled = False
        End If
    End Sub

    Protected Sub ddlMoneda_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlMoneda.SelectedIndexChanged
        Try
            Dim ObjCnx As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
            Dim rsCambio As New Data.DataTable
            rsCambio = ObjCnx.TraerDataTable("ConsultarCambioDelDia", ddlMoneda.SelectedValue)
            If rsCambio.Rows.Count = 0 Then
                Me.lblTipoCambio.Text = "Tipo de cambio no asignado, favor de comunicarse con Dirección de Contabilidad"
                Me.lblTipoCambio.ForeColor = Drawing.Color.Red
                Me.cmdEnviar.Enabled = False
                Me.cmdAdjuntar.Enabled = False
                Me.cmdGuardar.Enabled = False
                Me.CmdAgregar.Enabled = False
            Else
                Me.lblTipoCambioSimbolo.Text = rsCambio.Rows(0).Item(2) & " "
                Me.lblTipoCambio.Text = FormatNumber(rsCambio.Rows(0).Item(3), 2)
                Me.lblTipoCambio.ForeColor = Drawing.Color.Black
                Me.cmdEnviar.Enabled = True
                Me.cmdAdjuntar.Enabled = True
                Me.cmdGuardar.Enabled = True
                Me.CmdAgregar.Enabled = True
                CalcularPresupuesto()
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub CalcularPresupuesto()
        'If Not IsPostBack Then
        If IsNumeric(Me.txtIngreso.Text) = False Then
            Me.txtIngreso.Text = 0
        End If

        If IsNumeric(Me.txtEgreso.Text) = False Then
            Me.txtEgreso.Text = 0
        End If

        Me.lblIngresoMN.Text = FormatNumber(CDbl(Me.txtIngreso.Text) * CDbl(Me.lblTipoCambio.Text), 2)
        Me.lblEgresoMN.Text = FormatNumber(CDbl(Me.txtEgreso.Text) * CDbl(Me.lblTipoCambio.Text), 2)
        Me.lblUtilidad.Text = FormatNumber(CDbl(Me.txtIngreso.Text) - CDbl(Me.txtEgreso.Text), 2)
        Me.lblUtilidadMN.Text = FormatNumber(CDbl(Me.lblIngresoMN.Text) - CDbl(Me.lblEgresoMN.Text), 2)

        If CDbl(Me.lblUtilidad.Text) >= 0 Then
            Me.lblUtilidad.ForeColor = Drawing.Color.Blue
            Me.lblUtilidadMN.ForeColor = Drawing.Color.Blue
        Else
            Me.lblUtilidad.ForeColor = Drawing.Color.Red
            Me.lblUtilidadMN.ForeColor = Drawing.Color.Red
        End If
        'End If
    End Sub

    Protected Sub txtIngreso_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtIngreso.TextChanged
        CalcularPresupuesto()
    End Sub

    Protected Sub txtEgreso_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtEgreso.TextChanged
        CalcularPresupuesto()
    End Sub

    Protected Sub cmdPrioridad_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdPrioridad.Click
        If Me.lblPrioridad.Visible = False Then
            lblPrioridad.Visible = True
        Else
            lblPrioridad.Visible = False
        End If
    End Sub

    Protected Sub cmdEnviar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdEnviar.Click
        Try
            Call RegistrarPropuesta()

            Dim ObjCnx As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
            ObjCnx.Ejecutar("PRP_CalificarPropuestas_v1", Me.lblIdUsuario.Text, Me.lblIdPropuesta.Text, "P", "P", "P")

            'Si Proponente es Igual a Director de Propuesta (Responsable POA), se debe asiganar el consejo Administrativo, Consejo de facultad según corresponda
            Dim dtt As New Data.DataTable
            dtt = ObjCnx.TraerDataTable("PRP_ProponenteDirector", Me.lblIdUsuario.Text, Me.lblIdPropuesta.Text)
            If dtt.Rows.Count = 0 Then
            Else
                If dtt.Rows(0).Item(0).ToString = 1 Then
                    ObjCnx.Ejecutar("PRP_CalificarPropuestas_v1", Me.lblIdUsuario.Text, Me.lblIdPropuesta.Text, "D", "D", "C")
                End If
            End If
            Response.Redirect("contenido_POA_v1.aspx?id=" & Me.lblIdUsuario.Text & "&instancia=" & Request.QueryString("instancia"))
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub RegistrarPropuesta()
        Try
            Dim codigo_prp, codigo_dap As String
            Dim rsDatos As New Data.DataTable
            Dim ObjCnx As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
            Dim VarPrioridad As String

            If Me.lblPrioridad.Visible = True Then
                VarPrioridad = "A"
            Else
                VarPrioridad = "N"
            End If

            Dim destino_prp As String = IIf(Left(lblFacultad.Text, 1) = "R", "R", "F")
            Dim Propuesta As String = ""

            If ddl_propuesta.SelectedValue.ToString = 0 Then
                Response.Write("<script>alert('Seleccione una Propuesta')</script>")
            End If

            If ddlTipoPropuesta.SelectedValue.ToString = 0 Then
                Response.Write("<script>alert('Seleccione un tipo de Propuesta')</script>")
            End If


            If ddlTipoPropuesta.SelectedValue.ToString = 3 Or ddlTipoPropuesta.SelectedValue.ToString = 17 Then
                Propuesta = txtPropuesta.Text.ToString.ToUpper
                If txtPropuesta.Text.ToString.Trim = "" Then
                    Propuesta = ddl_propuesta.SelectedItem.ToString
                End If
            Else
                Propuesta = ddl_propuesta.SelectedItem.ToString
            End If

            If Me.lblIdPropuesta.Text.Trim = "" Then
                codigo_prp = ObjCnx.TraerValor("PRP_RegistraPropuesta_v1", "PR", Propuesta, "P", "P", VarPrioridad, _
                                Me.ddlTipoPropuesta.SelectedValue, Me.lblFacultadID.Text, System.DBNull.Value, "SI", Me.hdcodigo_cco.Value, destino_prp, _
                                Me.txtIngreso.Text, Me.txtEgreso.Text, Me.lblUtilidad.Text, Me.txtResumen.Text, Me.txtImportancia.Text, 1, _
                                Me.ddlMoneda.SelectedValue, lblTipoCambio.Text, Me.lblIngresoMN.Text, Me.lblEgresoMN.Text, Me.lblUtilidadMN.Text, _
                                Me.lblIdUsuario.Text, Me.hdcodigo_cco.Value, ddl_propuesta.SelectedValue)

                Me.lblIdPropuesta.Text = codigo_prp
            Else
                codigo_prp = Me.lblIdPropuesta.Text

                'Eliminar todo y enviar a su revision a director de área
                ObjCnx.Ejecutar("PRP_EliminarProponentesSegunDestino", Me.lblIdPropuesta.Text.Trim, destino_prp, Me.lblIdUsuario.Text)

                Dim UltimaVersion As Integer
                Dim rsVersiones As New Data.DataTable

                rsVersiones = ObjCnx.TraerDataTable("CONSULTARVERSIONESPROPUESTA", "ES", Me.lblIdPropuesta.Text, 0)
                UltimaVersion = rsVersiones.Rows(0).Item("version_dap")

                rsDatos = ObjCnx.TraerDataTable("PRP_listarDatosPropuesta", codigo_prp)
                codigo_dap = rsDatos.Rows(0).Item("codigo_dap")
                '    codigo_cop = rsDatos.Rows(0).Item("codigo_cop")
                '    codigo_dip = 0

                'Response.Write("PRP_ActualizarResponsableTipoPropuesta" & "," & Me.lblIdPropuesta.Text & "," & Me.ddlTipoPropuesta.SelectedValue)
                ObjCnx.Ejecutar("PRP_ActualizarResponsableTipoPropuesta", Me.lblIdPropuesta.Text, Me.ddlTipoPropuesta.SelectedValue)


                'Response.Write("ActualizarPropuesta_v1" & "," & "TO" & "," & Me.lblIdPropuesta.Text & "," & Propuesta & "," & "" & "," & "" & "," & VarPrioridad & "," & Me.ddlTipoPropuesta.SelectedValue & "," & destino_prp & "," & ddl_propuesta.SelectedValue.ToString)
                ObjCnx.Ejecutar("ActualizarPropuesta_v1", "TO", Me.lblIdPropuesta.Text, Propuesta, "", "", VarPrioridad, Me.ddlTipoPropuesta.SelectedValue, destino_prp, ddl_propuesta.SelectedValue)

                If rsVersiones.Rows(0).Item("instancia_Prp") = "P" Then
                    ObjCnx.Ejecutar("ActualizarDatosPropuesta_v1", codigo_dap, Me.txtIngreso.Text, Me.txtEgreso.Text, Me.lblUtilidad.Text, Me.txtResumen.Text, Me.txtImportancia.Text, UltimaVersion, Me.ddlMoneda.SelectedValue, Me.lblTipoCambio.Text, Me.lblIngresoMN.Text, Me.lblEgresoMN.Text, Me.lblUtilidadMN.Text)
                Else
                    If Me.lblNuevoDap.Text.Trim = "" Then
                        Me.lblNuevoDap.Text = ObjCnx.Ejecutar("PRP_RegistrarVersionPropuesta", codigo_prp, Me.txtIngreso.Text, Me.txtEgreso.Text, Me.lblUtilidad.Text, Me.txtResumen.Text, Me.txtImportancia.Text, UltimaVersion + 1, Me.ddlMoneda.SelectedValue, Me.lblTipoCambio.Text, Me.lblIngresoMN.Text, Me.lblEgresoMN.Text, Me.lblUtilidadMN.Text, 0)
                    Else
                        ObjCnx.Ejecutar("ActualizarDatosPropuesta_v1", codigo_dap, Me.txtIngreso.Text, Me.txtEgreso.Text, Me.lblUtilidad.Text, Me.txtResumen.Text, Me.txtImportancia.Text, UltimaVersion, Me.ddlMoneda.SelectedValue, Me.lblTipoCambio.Text, Me.lblIngresoMN.Text, Me.lblEgresoMN.Text, Me.lblUtilidadMN.Text)
                    End If
                End If
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub CmdAgregar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdAgregar.Click
        Dim strruta As String = ""
        Dim nombrearchivo1 As String = ""
        Dim codigo_prp, codigo_cop, modifica As String
        Dim codigo_dap As String = ""
        Dim codigo_dip As String = ""

        codigo_dip = ""
        Call RegistrarPropuesta()

        Dim rsDatos As New Data.DataTable
        Dim ObjCnx As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)

        If Me.lblIdPropuesta.Text = "" Then
            Call RegistrarPropuesta()
            codigo_prp = Me.lblIdPropuesta.Text
        Else
            codigo_prp = Me.lblIdPropuesta.Text
        End If

        Dim UltimaVersion As Integer
        Dim rsVersiones As New Data.DataTable
        rsVersiones = ObjCnx.TraerDataTable("CONSULTARVERSIONESPROPUESTA", "ES", codigo_prp, 0)
        UltimaVersion = rsVersiones.Rows(0).Item("version_dap")

        rsDatos = ObjCnx.TraerDataTable("PRP_ConsultarDatosCodigosPropuesta", codigo_prp, UltimaVersion)
        codigo_dap = rsDatos.Rows(0).Item("codigo_dap").ToString
        codigo_cop = rsDatos.Rows(0).Item("codigo_cop").ToString

        modifica = Request.QueryString("modifica")

        If codigo_dip = "" Then : codigo_dip = "0" : End If
        If codigo_dap = "" Then : codigo_dap = "0" : End If

        strruta = Server.MapPath("../../../../filespropuestas")
        Dim Carpeta As New System.IO.DirectoryInfo(strruta & "\" & codigo_prp.ToString)
        If Carpeta.Exists = False Then
            Carpeta.Create()
        End If

        Dim Obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)

        If Me.FileArchivo.HasFile Then
            Try
                nombrearchivo1 = codigo_prp.ToString & Now.Day.ToString & Now.Month.ToString & Now.Year.ToString & Now.Hour.ToString & Now.Minute.ToString & Now.Second.ToString & System.IO.Path.GetExtension(Me.FileArchivo.FileName).ToString
                Me.FileArchivo.PostedFile.SaveAs(strruta & "\" & codigo_prp.ToString & "\" & nombrearchivo1)

                Obj.IniciarTransaccion()
                If codigo_dap = "0" And codigo_dip = "0" Then
                    'Response.Write("<script>alert('codigo_dap=0')</script>")
                    codigo_dap = "C" & codigo_cop
                    ' Response.Write("RegistraArchivoPropuesta" & " " & codigo_dap & ", " & nombrearchivo1 & ", " & Me.TxtNombre.Text)
                    Obj.Ejecutar("RegistraArchivoPropuesta", codigo_dap, nombrearchivo1, Me.TxtNombre.Text)
                    codigo_dap = "0"
                    codigo_dip = "0"
                End If

                If codigo_dap <> "0" Then
                    Obj.Ejecutar("RegistraArchivoPropuesta", codigo_dap, nombrearchivo1, Me.TxtNombre.Text)
                End If

                If codigo_dip <> "0" Then
                    Obj.Ejecutar("RegistraArchivoPropuestaInforme", codigo_dip, nombrearchivo1, Me.TxtNombre.Text)
                End If
                Obj.TerminarTransaccion()

                Call ListarArchivos(codigo_cop, codigo_dap, codigo_dip)
                Me.TxtNombre.Text = ""

                Me.LblMensaje.ForeColor = Drawing.Color.Blue
                Me.LblMensaje.Text = "Se agregó el archivo satisfactoriamente."

            Catch ex As Exception
                Me.LblMensaje.ForeColor = Drawing.Color.Red
                Me.LblMensaje.Text = "Ocurrió un error al procesar el archivo."
            End Try

        End If
    End Sub

    Private Sub ListarArchivos(ByVal codigo_cop As String, ByVal codigo_dap As String, ByVal codigo_dip As String)
        Try
            Dim Obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
            Dim Datos As Data.DataTable

            If codigo_cop <> "" Then
                Datos = Obj.TraerDataTable("ConsultarArchivosPropuesta", "TO", codigo_cop)
            Else
                If codigo_dap <> "0" Then
                    Datos = Obj.TraerDataTable("ConsultarArchivosPropuesta", "CP", codigo_dap)
                Else
                    Datos = Obj.TraerDataTable("ConsultarArchivosPropuesta", "CI", codigo_dip)
                End If
            End If
            Me.GridView1.DataSource = Datos
            Me.GridView1.DataBind()
            Obj = Nothing
            Datos.Dispose()
            Datos = Nothing
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub


    Protected Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim fila As Data.DataRowView
            Dim ext As String
            fila = e.Row.DataItem 'Los datos devueltos (cuando quiero saber la data que llega)
            e.Row.Attributes.Add("OnMouseOver", "Resaltar(1,this,'S')")
            e.Row.Attributes.Add("OnMouseOut", "Resaltar(0,this,'S')")
            e.Row.Attributes.Add("OnClick", "ponervalortext('" & fila.Row("nombre_apr").ToString & "','" & fila.Row("codigo_apr").ToString & "')")
            e.Row.Attributes.Add("Class", "Sel")
            e.Row.Attributes.Add("Typ", "Sel")
            e.Row.Attributes.Add("id", "fila" & fila.Row("codigo_apr").ToString & "")
            ext = Right(fila.Row(2).ToString, 3)

            'Dim cadena As String = fila.Row(2).ToString
            'Dim TestPos As Integer
            'TestPos = InStr(1, cadena, ".")
            'Dim xPos As Integer = 0
            'xPos = Len(fila.Row(2).ToString) - TestPos
            'ext = Right(fila.Row(2).ToString, xPos)
            'Response.Write("<script>alert('" & ext & "')</script>")

            e.Row.Cells(4).Attributes.Add("onclick", "return confirm('¿Está seguro que desea elmininar este archivo?')")
        End If
    End Sub

    Protected Sub GridView1_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles GridView1.RowDeleting
        Try
            Dim Obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
            Dim codigo_prp, codigo_dap, codigo_dip, codigo_cop As String
            Dim rsDatos As New Data.DataTable
            codigo_prp = Me.lblIdPropuesta.Text
            rsDatos = Obj.TraerDataTable("PRP_ConsultarDatosCodigosPropuesta", codigo_prp, 1)
            codigo_dap = rsDatos.Rows(0).Item("codigo_dap")
            codigo_cop = rsDatos.Rows(0).Item("codigo_cop")
            codigo_dip = 0

            Obj.Ejecutar("EliminarArchivoPropuesta", Me.GridView1.DataKeys(e.RowIndex).Value)
            Call ListarArchivos(codigo_cop, codigo_dap, codigo_dip)
            e.Cancel = True
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub cmdGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdGuardar.Click
        'Validar q seleccione un Tipo de Propuesta
        If ddlTipoPropuesta.SelectedValue = 0 Then
            Response.Write("<script>alert('Seleccione un Tipo de Propuesta')</script>")
            ddlTipoPropuesta.Focus()
            Return
        End If

        'Validar q seleccione una Propuesta
        If ddl_propuesta.SelectedValue = 0 Then
            Response.Write("<script>alert('Seleccione una Propuesta')</script>")
            ddl_propuesta.Focus()
            Return
        End If

        Call RegistrarPropuesta()
        Response.Redirect("contenido_POA_v1.aspx?id=" & Me.lblIdUsuario.Text & "&instancia=" & Request.QueryString("instancia"))
    End Sub

    Protected Sub cmdAdjuntar0_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSalir.Click
        Response.Redirect("contenido_POA_v1.aspx?id=" & Me.lblIdUsuario.Text & "&instancia=" & Request.QueryString("instancia"))
    End Sub

    Protected Sub ddl_propuesta_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddl_propuesta.SelectedIndexChanged
        Call ListarPropuestas()
    End Sub

    Sub ListarPropuestas()
        Try
            'Listar Proyectos POA
            Dim dtt As New Data.DataTable
            Dim ObjCnx As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)

            If ddl_propuesta.SelectedValue.ToString <> 0 Then
                'Response.Write("PRO_listaAreasPoa " & ddl_propuesta.SelectedValue.ToString)
                dtt = ObjCnx.TraerDataTable("PRO_listaAreasPoa", ddl_propuesta.SelectedValue.ToString)

                Me.hdcodigo_cco.Value = dtt.Rows(0).Item("codigo_cco")
                Me.lblArea.Text = dtt.Rows(0).Item("nombre_poa")

                Dim dttDerivar As New Data.DataTable
                dttDerivar = ObjCnx.TraerDataTable("PRP_Consultar_Derivar", ddl_propuesta.SelectedValue.ToString)
                If dttDerivar.Rows.Count > 0 Then
                    lblFacultad.Text = "FACULTAD"
                    lblFacultadID.Text = dttDerivar.Rows(0).Item("codigo_Fac")
                Else
                    lblFacultad.Text = "RECTORADO"
                    lblFacultadID.Text = 0
                End If

                'Response.Write(ddlTipoPropuesta.SelectedValue.ToString)
                'Actividades No Academicas
                If ddlTipoPropuesta.SelectedValue.ToString = 18 Then
                    lblFacultad.Text = "RECTORADO"
                    lblFacultadID.Text = 0
                End If

                Call wf_ObtenerImportes()
                Call wf_MostrarEtiquetaPresupuesto()

                If dgv_PresupuestoIngreso.Rows.Count = 0 Then
                    lblMsgRentabilidad.Text = "PROYECTO NO PRESENTA RENTABILIDAD"
                    lblMsgRentabilidad.Visible = True
                    lblMargen.Visible = False
                    lblRentabilidad.Visible = False

                    lblMsgUtilidad.Text = "NO PRESENTA"
                    lblMsgUtilidad.Visible = True
                    lblUtilidadMN.Visible = False
                Else
                    lblMsgRentabilidad.Visible = False
                    lblMargen.Visible = True
                    lblRentabilidad.Visible = True

                    lblMsgUtilidad.Visible = False
                    lblUtilidadMN.Visible = True
                End If
            End If

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Sub wf_ObtenerImportes()
        Try
            Dim dtt As New Data.DataTable
            Dim ObjCnx As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
            dtt = ObjCnx.TraerDataTable("POA_ListarDetalleActividad", ddl_propuesta.SelectedValue.ToString)

            Dim lrd_Ingresos As Decimal = 0
            Dim lrd_Egresos As Decimal = 0

            For i As Integer = 0 To dtt.Rows.Count - 1
                lrd_Ingresos = lrd_Ingresos + dtt.Rows(i).Item("ingresos")
                lrd_Egresos = lrd_Egresos + dtt.Rows(i).Item("egresos")
            Next
            txtIngreso.Text = lrd_Ingresos
            txtEgreso.Text = lrd_Egresos

            lblIngreso.Text = lrd_Ingresos
            lblEgreso.Text = lrd_Egresos

            Call CalcularPresupuesto()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Sub wf_mostraretiquetapresupuesto()
        Try
            Dim dtt As New Data.DataTable
            Dim objcnx As New ClsSqlServer(ConfigurationManager.ConnectionStrings("cnxbdusat").ConnectionString)
            dtt = objcnx.TraerDataTable("prp_consultaritemdetallepresupuesto", ddl_propuesta.SelectedValue.ToString)

            If dtt.Rows(0).Item(0).ToString = 0 Then
                Response.Write("no se han registrado item's en el presupuesto")
                dgv_Presupuesto.Visible = False
                dgv_PresupuestoIngreso.Visible = False

                imgPresupuesto.Visible = False
                btn_MostrarPresupuesto.Visible = False

                Me.TDPresupuesto.Attributes.Remove("style")
                Me.TDPresupuesto.Attributes.Add("style", "display:none")

                Me.TDMargen.Attributes.Remove("style")
                Me.TDMargen.Attributes.Add("style", "display:none")

                Me.TDRentabilidad.Attributes.Remove("style")
                Me.TDRentabilidad.Attributes.Add("style", "display:none")

                Me.TDImportes.Attributes.Remove("style")
                Me.TDImportes.Attributes.Add("style", "display:none")

                dgv_PresupuestoIngreso.Visible = False
                dgv_Presupuesto.Visible = False

                lblMargen.Text = 0
                lblRentabilidad.Text = 0
                lblIngreso.Text = 0
                lblEgreso.Text = 0
            Else
                Call wf_llenarItemPresupuesto()

                If ddlTipoPropuesta.SelectedValue.ToString = 3 Or ddlTipoPropuesta.SelectedValue.ToString = 17 Then 'programas
                    imgPresupuesto.Visible = False
                    btn_MostrarPresupuesto.Visible = False
                ElseIf ddlTipoPropuesta.SelectedValue.ToString = 0 Then
                    imgPresupuesto.Visible = False
                    btn_MostrarPresupuesto.Visible = False
                Else
                    imgPresupuesto.Visible = True
                    btn_MostrarPresupuesto.Visible = True
                End If
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Sub wf_llenarItemPresupuesto()
        Try
            Dim codigo_acp As Integer = 0
            If ddl_propuesta.SelectedValue.ToString <> "" Then
                codigo_acp = ddl_propuesta.SelectedValue.ToString
            End If

            ''Llenar Grid de Items Presupuestados
            Dim ObjCnx As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)

            If dgv_PresupuestoIngreso.Rows.Count = 0 Then
                lblMsgRentabilidad.Text = "PROYECTO NO PRESENTA RENTABILIDAD"
                lblMsgRentabilidad.Visible = True
                lblMargen.Visible = False
                lblRentabilidad.Visible = False

                lblMsgUtilidad.Text = "NO PRESENTA"
                lblMsgUtilidad.Visible = True
                lblUtilidadMN.Visible = False
            Else
                ''Obtener Margen y Rentabilidad
                Dim dtMargen As New Data.DataTable
                dtMargen = ObjCnx.TraerDataTable("PRP_ObtenerMargen", codigo_acp)
                lblMargen.Text = FormatNumber(CDbl(dtMargen.Rows(0).Item("margen")))
                'FormatNumber(CDbl(Me.lblIngresoMN.Text) 

                lblRentabilidad.Text = dtMargen.Rows(0).Item("rentabilidad").ToString
                If lblRentabilidad.Text >= 35 Then
                    lblRentabilidad.ForeColor = Drawing.Color.Blue
                Else
                    lblRentabilidad.ForeColor = Drawing.Color.Red
                End If
                lblMsgRentabilidad.Visible = False
                lblMargen.Visible = True
                lblRentabilidad.Visible = True

                lblMsgUtilidad.Visible = False
                lblUtilidadMN.Visible = True
            End If

            Dim dtPresupuestoIngreso As New Data.DataTable
            dtPresupuestoIngreso = ObjCnx.TraerDataTable("PRP_ListaItemsPresupuestoIngreso", codigo_acp)
            Me.dgv_PresupuestoIngreso.DataSource = dtPresupuestoIngreso
            Me.dgv_PresupuestoIngreso.DataBind()


            Dim dtPresupuesto As New Data.DataTable
            dtPresupuesto = ObjCnx.TraerDataTable("PRP_ListaItemsPresupuesto", codigo_acp)
            Me.dgv_Presupuesto.DataSource = dtPresupuesto
            Me.dgv_Presupuesto.DataBind()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub dgv_Presupuesto_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles dgv_Presupuesto.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then

            'Crear Sub Totales en Columna de grid, revisar el evento RowCreated
            storid = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "codigo_dap"))
            Dim tmpTotal As Decimal = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "subtotal"))
            qtyTotal += tmpTotal
            grQtyTotal += tmpTotal
            'Fin

            Dim row As Data.DataRowView = CType(e.Row.DataItem, Data.DataRowView)
            If LastTipo = row("tipo") Then
                If (dgv_Presupuesto.Rows(CurrentRow).Cells(0).RowSpan = 0) Then
                    dgv_Presupuesto.Rows(CurrentRow).Cells(0).RowSpan = 2
                Else
                    dgv_Presupuesto.Rows(CurrentRow).Cells(0).RowSpan += 1
                End If
                e.Row.Cells(0).Visible = False

                If LastActividad = row("actividad") Then
                    If (dgv_Presupuesto.Rows(CurrentRow2).Cells(1).RowSpan = 0) Then
                        dgv_Presupuesto.Rows(CurrentRow2).Cells(1).RowSpan = 2
                    Else
                        dgv_Presupuesto.Rows(CurrentRow2).Cells(1).RowSpan += 1
                    End If
                    e.Row.Cells(1).Visible = False
                Else
                    e.Row.VerticalAlign = VerticalAlign.Middle
                    LastActividad = row("actividad").ToString()
                    CurrentRow2 = e.Row.RowIndex
                End If
            Else
                e.Row.VerticalAlign = VerticalAlign.Middle
                LastTipo = row("tipo").ToString()
                CurrentRow = e.Row.RowIndex

                e.Row.VerticalAlign = VerticalAlign.Middle
                LastActividad = row("actividad").ToString()
                CurrentRow2 = e.Row.RowIndex
            End If
            subtotal += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "subtotal"))
            PorcentajeEgreso += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "porcentaje"))

            If dgv_PresupuestoIngreso.Rows.Count = 0 Then
                'dgv_Presupuesto.Columns(7).Visible = False
                e.Row.Cells(7).Visible = False
            End If

        ElseIf e.Row.RowType = DataControlRowType.Footer Then
            e.Row.Cells(1).Text = "TOTALES "
            e.Row.Cells(1).HorizontalAlign = HorizontalAlign.Center
            e.Row.Cells(1).ColumnSpan = 5
            e.Row.Cells(1).ForeColor = Drawing.Color.Red

            e.Row.Cells(2).Text = "S/. " + FormatNumber(subtotal.ToString(), 2)
            e.Row.Cells(2).HorizontalAlign = HorizontalAlign.Right
            e.Row.Cells(2).ForeColor = Drawing.Color.Red

            e.Row.Cells(3).Text = FormatNumber(PorcentajeEgreso.ToString(), 2)
            e.Row.Cells(3).HorizontalAlign = HorizontalAlign.Right
            e.Row.Cells(3).ForeColor = Drawing.Color.Red

            e.Row.Font.Bold = True
            e.Row.Height = 20
            'e.Row.Cells(3).Visible = False
            e.Row.Cells(4).Visible = False
            e.Row.Cells(5).Visible = False
            e.Row.Cells(6).Visible = False
            e.Row.Cells(7).Visible = False

            If dgv_PresupuestoIngreso.Rows.Count = 0 Then
                'dgv_Presupuesto.Columns(7).Visible = False
                e.Row.Cells(3).Visible = False
            End If
        End If
    End Sub

    Protected Sub imgPresupuesto_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgPresupuesto.Click
        Try
            dgv_Presupuesto.Visible = True
            dgv_PresupuestoIngreso.Visible = True
            Call wf_llenarItemPresupuesto()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub dgv_PresupuestoIngreso_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles dgv_PresupuestoIngreso.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim row As Data.DataRowView = CType(e.Row.DataItem, Data.DataRowView)
            If LastTipoIngreso = row("tipo") Then
                If (dgv_PresupuestoIngreso.Rows(CurrentRowIngreso).Cells(0).RowSpan = 0) Then
                    dgv_PresupuestoIngreso.Rows(CurrentRowIngreso).Cells(0).RowSpan = 2
                Else
                    dgv_PresupuestoIngreso.Rows(CurrentRowIngreso).Cells(0).RowSpan += 1
                End If
                e.Row.Cells(0).Visible = False

                If LastActividadIngreso = row("actividad") Then
                    If (dgv_PresupuestoIngreso.Rows(CurrentRow2Ingreso).Cells(1).RowSpan = 0) Then
                        dgv_PresupuestoIngreso.Rows(CurrentRow2Ingreso).Cells(1).RowSpan = 2
                    Else
                        dgv_PresupuestoIngreso.Rows(CurrentRow2Ingreso).Cells(1).RowSpan += 1
                    End If
                    e.Row.Cells(1).Visible = False
                Else
                    e.Row.VerticalAlign = VerticalAlign.Middle
                    LastActividadIngreso = row("actividad").ToString()
                    CurrentRow2Ingreso = e.Row.RowIndex
                End If
            Else
                e.Row.VerticalAlign = VerticalAlign.Middle
                LastTipoIngreso = row("tipo").ToString()
                CurrentRowIngreso = e.Row.RowIndex

                e.Row.VerticalAlign = VerticalAlign.Middle
                LastActividadIngreso = row("actividad").ToString()
                CurrentRow2Ingreso = e.Row.RowIndex
            End If
            subtotalIngreso += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "SUBTOTAL"))

        ElseIf e.Row.RowType = DataControlRowType.Footer Then
            e.Row.Cells(1).Text = "TOTALES "
            e.Row.Cells(1).HorizontalAlign = HorizontalAlign.Center
            e.Row.Cells(1).ColumnSpan = 5
            e.Row.Cells(1).ForeColor = Drawing.Color.Red

            e.Row.Cells(2).Text = "S/. " + FormatNumber(subtotalIngreso.ToString(), 2)
            e.Row.Cells(2).HorizontalAlign = HorizontalAlign.Right
            e.Row.Cells(2).ForeColor = Drawing.Color.Red

            e.Row.Font.Bold = True
            e.Row.Height = 20
            e.Row.Cells(3).Visible = False
            e.Row.Cells(4).Visible = False
            e.Row.Cells(5).Visible = False
            e.Row.Cells(6).Visible = False
        End If
    End Sub

    Protected Sub dgv_Presupuesto_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles dgv_Presupuesto.RowCreated
        Dim newRow As Boolean = False
        If (storid > 0) AndAlso (DataBinder.Eval(e.Row.DataItem, "codigo_dap") IsNot Nothing) Then
            If storid <> Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "codigo_dap").ToString()) Then
                newRow = True
            End If
        End If
        If (storid > 0) AndAlso (DataBinder.Eval(e.Row.DataItem, "codigo_dap") Is Nothing) Then
            newRow = True
            rowIndex = 0
        End If
        If newRow Then
            Dim GridView1 As GridView = DirectCast(sender, GridView)
            Dim NewTotalRow As New GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert)
            NewTotalRow.Font.Bold = True
            NewTotalRow.ForeColor = Drawing.Color.Blue

            Dim HeaderCell As New TableCell()
            HeaderCell.Text = "Sub Total"
            HeaderCell.HorizontalAlign = HorizontalAlign.Center
            HeaderCell.ColumnSpan = 5

            NewTotalRow.Cells.Add(HeaderCell)
            HeaderCell = New TableCell()
            HeaderCell.Text = "S/. " + FormatNumber(CDbl(qtyTotal.ToString()))
            HeaderCell.HorizontalAlign = HorizontalAlign.Right

            NewTotalRow.Cells.Add(HeaderCell)
            HeaderCell = New TableCell()
            HeaderCell.Text = ""
            HeaderCell.HorizontalAlign = HorizontalAlign.Right

            NewTotalRow.Cells.Add(HeaderCell)
            GridView1.Controls(0).Controls.AddAt(e.Row.RowIndex + rowIndex, NewTotalRow)
            rowIndex += 1
            qtyTotal = 0
        End If
    End Sub

    Protected Sub ddlTipoPropuesta_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlTipoPropuesta.SelectedIndexChanged
        Try
            Dim ObjCnx As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
            'Listar Proyectos POA
            Dim dtProyecto As New Data.DataTable

            'Response.Write(ddlTipoPropuesta.SelectedValue.ToString)
            If ddlTipoPropuesta.SelectedValue.ToString = 3 Or ddlTipoPropuesta.SelectedValue.ToString = 17 Then 'Filtra Solo Programas
                ''3:CONVENIOS - 17:REGLAMENTO
                If Request.QueryString("accion") <> "M" Then
                    dtProyecto = ObjCnx.TraerDataTable("PRO_listaProyectosPoa", Request.QueryString("idUsu"), 2)
                    If dtProyecto.Rows.Count = 0 Then
                        Response.Write("No Tiene propuestas Aprobadas")
                    Else
                        ClsFunciones.LlenarListas(Me.ddl_propuesta, dtProyecto, "codigo_acp", "resumen_acp")
                    End If
                End If

                txtPropuesta.Text = ""
                txtPropuesta.Visible = True

                ddl_propuesta.Visible = True
                lbl_Propuesta.Visible = False

                imgPresupuesto.Visible = False
                btn_MostrarPresupuesto.Visible = False

                Me.TDPresupuesto.Attributes.Remove("Style")
                Me.TDPresupuesto.Attributes.Add("Style", "display:none")

                Me.TDMargen.Attributes.Remove("Style")
                Me.TDMargen.Attributes.Add("Style", "display:none")

                Me.TDRentabilidad.Attributes.Remove("Style")
                Me.TDRentabilidad.Attributes.Add("Style", "display:none")

                Me.TDImportes.Attributes.Remove("Style")
                Me.TDImportes.Attributes.Add("Style", "display:none")

                dgv_PresupuestoIngreso.Visible = False
                dgv_Presupuesto.Visible = False

            ElseIf ddlTipoPropuesta.SelectedValue.ToString = 0 Then
                txtPropuesta.Text = ""
                txtPropuesta.Visible = False

                imgPresupuesto.Visible = False
                btn_MostrarPresupuesto.Visible = False

                Me.TDPresupuesto.Attributes.Remove("Style")
                Me.TDPresupuesto.Attributes.Add("Style", "display:none")

                Me.TDMargen.Attributes.Remove("Style")
                Me.TDMargen.Attributes.Add("Style", "display:none")

                Me.TDRentabilidad.Attributes.Remove("Style")
                Me.TDRentabilidad.Attributes.Add("Style", "display:none")

                Me.TDImportes.Attributes.Remove("Style")
                Me.TDImportes.Attributes.Add("Style", "display:none")

                dgv_PresupuestoIngreso.Visible = False
                dgv_Presupuesto.Visible = False

                lblMargen.Text = 0
                lblRentabilidad.Text = 0
                lblIngreso.Text = 0
                lblEgreso.Text = 0
            Else
                If Request.QueryString("accion") <> "M" Then
                    'Response.Write("PRO_listaProyectosPoa " & Request.QueryString("idUsu") & ", " & 1)
                    dtProyecto = ObjCnx.TraerDataTable("PRO_listaProyectosPoa", Request.QueryString("idUsu"), 1)    'Filtra Solo Proyectos
                    If dtProyecto.Rows.Count = 0 Then
                        Response.Write("No Tiene propuestas Aprobadas")
                    Else
                        ClsFunciones.LlenarListas(Me.ddl_propuesta, dtProyecto, "codigo_acp", "resumen_acp")
                    End If
                End If
                txtPropuesta.Visible = False
                ddl_propuesta.Visible = True

                imgPresupuesto.Visible = True
                btn_MostrarPresupuesto.Visible = True

                Me.TDPresupuesto.Attributes.Remove("Style")
                Me.TDPresupuesto.Attributes.Add("Style", "display:block")

                Me.TDMargen.Attributes.Remove("Style")
                Me.TDMargen.Attributes.Add("Style", "display:block")

                Me.TDRentabilidad.Attributes.Remove("Style")
                Me.TDRentabilidad.Attributes.Add("Style", "display:block")

                Me.TDImportes.Attributes.Remove("Style")
                Me.TDImportes.Attributes.Add("Style", "display:block")

                dgv_PresupuestoIngreso.Visible = True
                dgv_Presupuesto.Visible = True
            End If
            'Call ListarPropuestas()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub btn_MostrarPresupuesto_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_MostrarPresupuesto.Click
        Try
            dgv_Presupuesto.Visible = True
            dgv_PresupuestoIngreso.Visible = True
            Call wf_llenarItemPresupuesto()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub
End Class
