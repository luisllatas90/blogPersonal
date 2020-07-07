
Partial Class nuevapropuesta
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim idUsu As Integer
        idUsu = Request.QueryString("idUsu")




        Me.lblIdUsuario.Text = idUsu
        If Not IsPostBack Then
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
                    ' Response.Write(rsVersiones.Rows(0).Item("instancia_Prp"))
                    Response.Write("<b>Registro de propuesta</B>")
                Else
                    Response.Write("<B>Registro de la Versión " & UltimaVersion + 1 & " de la propuesta</B>")
                End If
            End If
            ' --------------------

            'consulta de la moneda y tipo de cambio

            rsMoneda = ObjCnx.TraerDataTable("consultarMoneda", 1, "")
            ClsFunciones.LlenarListas(Me.ddlMoneda, rsMoneda, "codigo_tip", "descripcion_tip")
            rsCambio = ObjCnx.TraerDataTable("ConsultarCambioDelDia", ddlMoneda.SelectedValue)
            Me.lblTipoCambioSimbolo.Text = rsCambio.Rows(0).Item(2) & " "
            Me.lblTipoCambio.Text = FormatNumber(rsCambio.Rows(0).Item(3), 2)

            'Consulta del Centro de costos
            rsCentroCostos = ObjCnx.TraerDataTable("ConsultarCentroCosto", "TO", 0)
            ClsFunciones.LlenarListas(Me.ddlArea, rsCentroCostos, "codigo_cco", "descripcion_cco")
            rsCentroCostosUsuario = ObjCnx.TraerDataTable("ConsultarCentroCosto", "PR", idUsu)
            Me.ddlArea.SelectedValue = rsCentroCostosUsuario.Rows(0).Item("codigo_cco")

            'consulta del tipo de propuesta
            rsTipoPropuesta = ObjCnx.TraerDataTable("ConsultarTipoPropuestas", "TO")
            ClsFunciones.LlenarListas(Me.ddlTipoPropuesta, rsTipoPropuesta, "codigo_tpr", "descripcion_tpr")

            'consultar facultad
            rsFacultad = ObjCnx.TraerDataTable("ConsultarFacultad", "PE", idUsu)
            If rsFacultad.Rows.Count = 0 Then
                Me.lblFacultadID.Text = 0
                Me.lblFacultad.Text = "Usted no está asignado a ninguna facultad"
                Me.cmdEnviar.Enabled = False
                Me.cmdAdjuntar.Enabled = False
                Me.lblFacultad.ForeColor = Drawing.Color.Red
                Me.cmdGuardar.Enabled = False
                Me.CmdAgregar.Enabled = False
            Else
                Me.lblFacultadID.Text = rsFacultad.Rows(0).Item("codigo_fac")
                Me.lblFacultad.Text = rsFacultad.Rows(0).Item("nombre_fac")
                Me.cmdEnviar.Enabled = True
                Me.cmdAdjuntar.Enabled = True
                Me.lblFacultad.ForeColor = Drawing.Color.Black
                Me.cmdGuardar.Enabled = True
                Me.CmdAgregar.Enabled = True
            End If

            If Request.QueryString("accion") = "M" Then
                'Response.Write(Request.QueryString("accion"))
                Dim rsVersion As New Data.DataTable
                rsVersion = ObjCnx.TraerDataTable("CONSULTARVERSIONESPROPUESTA", "DA", Me.lblIdPropuesta.Text, UltimaVersion)
                ' Response.Write(rsVersion.Rows(0).Item("nombre_prp"))
                Me.txtPropuesta.Text = rsVersion.Rows(0).Item("nombre_prp")
                Me.ddlTipoPropuesta.SelectedValue = rsVersion.Rows(0).Item("codigo_tpr")
                Me.txtResumen.Text = rsVersion.Rows(0).Item("beneficios_dap")
                Me.txtImportancia.Text = rsVersion.Rows(0).Item("importancia_dap")
                Me.txtIngreso.Text = rsVersion.Rows(0).Item("ingreso_dap")
                Me.txtEgreso.Text = rsVersion.Rows(0).Item("egreso_dap")
                Me.lblUtilidad.Text = rsVersion.Rows(0).Item("utilidad_dap")
                Me.lblIngresoMN.Text = rsVersion.Rows(0).Item("ingresoMN_dap")
                Me.lblEgresoMN.Text = rsVersion.Rows(0).Item("egresoMN_dap")
                Me.lblUtilidadMN.Text = rsVersion.Rows(0).Item("utilidadMN_dap")
                Me.lblTipoCambio.Text = rsVersion.Rows(0).Item("tipocambio_dap")
                Me.ddlMoneda.SelectedValue = rsVersion.Rows(0).Item("codigo_tip")
                Me.lblTipoCambioSimbolo.Text = rsVersion.Rows(0).Item("simbolo_moneda")


                If Request.QueryString("instancia") = "P" Then
                    Dim rsDatos As New Data.DataTable
                    Dim codigo_dap, codigo_cop, codigo_dip As Integer
                    rsDatos = ObjCnx.TraerDataTable("PRP_ConsultarDatosCodigosPropuesta", Me.lblIdPropuesta.Text, UltimaVersion)
                    codigo_dap = rsDatos.Rows(0).Item("codigo_dap")
                    codigo_cop = rsDatos.Rows(0).Item("codigo_cop")
                    codigo_dip = 0

                    Call ListarArchivos(codigo_cop, codigo_dap, codigo_dip)
                End If
            End If

        End If
    End Sub

    Protected Sub ddlMoneda_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlMoneda.SelectedIndexChanged
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
        'If Not IsPostBack Then
        If Me.lblPrioridad.Visible = False Then
            lblPrioridad.Visible = True
        Else
            lblPrioridad.Visible = False
        End If
        ' End If
    End Sub

    Protected Sub cmdEnviar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdEnviar.Click
        RegistrarPropuesta()
        Dim ObjCnx As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
        ObjCnx.Ejecutar("PRP_CalificarPropuestas", Me.lblIdUsuario.Text, Me.lblIdPropuesta.Text, "P", "P", "P")
        Response.Redirect("contenido.aspx?id=" & Me.lblIdUsuario.Text & "&instancia=" & Request.QueryString("instancia"))

    End Sub
    Private Sub RegistrarPropuesta()

        Dim codigo_prp, codigo_dap, codigo_dip, codigo_cop, modifica As String
        Dim rsDatos As New Data.DataTable
        Dim ObjCnx As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
        Dim VarPrioridad As String
        ' Response.Write(Me.lblIdPropuesta.Text)
        If Me.lblPrioridad.Visible = True Then
            VarPrioridad = "A"
        Else
            VarPrioridad = "N"
        End If
        If Me.lblIdPropuesta.Text.Trim = "" Then
            codigo_prp = ObjCnx.TraerValor("PRP_RegistraPropuesta", "PR", Me.txtPropuesta.Text, "P", "P", VarPrioridad, Me.ddlTipoPropuesta.SelectedValue, Me.lblFacultadID.Text, System.DBNull.Value, "SI", ddlArea.SelectedValue, Me.txtIngreso.Text, Me.txtEgreso.Text, Me.lblUtilidad.Text, Me.txtResumen.Text, Me.txtImportancia.Text, 1, Me.ddlMoneda.SelectedValue, lblTipoCambio.Text, Me.lblIngresoMN.Text, Me.lblEgresoMN.Text, Me.lblUtilidadMN.Text, Me.lblIdUsuario.Text, Me.lblFacultadID.Text)
            Me.lblIdPropuesta.Text = codigo_prp
        Else
            codigo_prp = Me.lblIdPropuesta.Text
            Dim UltimaVersion As Integer
            Dim rsVersiones As New Data.DataTable
            rsVersiones = ObjCnx.TraerDataTable("CONSULTARVERSIONESPROPUESTA", "ES", Me.lblIdPropuesta.Text, 0)
            UltimaVersion = rsVersiones.Rows(0).Item("version_dap")


            rsDatos = ObjCnx.TraerDataTable("PRP_ConsultarDatosCodigosPropuesta", codigo_prp, UltimaVersion)
            codigo_dap = rsDatos.Rows(0).Item("codigo_dap")
            codigo_cop = rsDatos.Rows(0).Item("codigo_cop")
            codigo_dip = 0

            ObjCnx.Ejecutar("ActualizarPropuesta", "TO", Me.lblIdPropuesta.Text, Me.txtPropuesta.Text, "", VarPrioridad, Me.ddlTipoPropuesta.SelectedValue)

            If rsVersiones.Rows(0).Item("instancia_Prp") = "P" Then
                ObjCnx.Ejecutar("ActualizarDatosPropuesta", codigo_dap, Me.txtIngreso.Text, Me.txtEgreso.Text, Me.lblUtilidad.Text, Me.txtResumen.Text, Me.txtImportancia.Text, UltimaVersion, Me.ddlMoneda.SelectedValue, Me.lblTipoCambio.Text, Me.lblIngresoMN.Text, Me.lblEgresoMN.Text, Me.lblUtilidadMN.Text)

            Else
                If Me.lblNuevoDap.Text.Trim = "" Then
                    Me.lblNuevoDap.Text = ObjCnx.Ejecutar("PRP_RegistrarVersionPropuesta", codigo_prp, Me.txtIngreso.Text, Me.txtEgreso.Text, Me.lblUtilidad.Text, Me.txtResumen.Text, Me.txtImportancia.Text, UltimaVersion + 1, Me.ddlMoneda.SelectedValue, Me.lblTipoCambio.Text, Me.lblIngresoMN.Text, Me.lblEgresoMN.Text, Me.lblUtilidadMN.Text, 0)
                Else
                    ObjCnx.Ejecutar("ActualizarDatosPropuesta", codigo_dap, Me.txtIngreso.Text, Me.txtEgreso.Text, Me.lblUtilidad.Text, Me.txtResumen.Text, Me.txtImportancia.Text, UltimaVersion, Me.ddlMoneda.SelectedValue, Me.lblTipoCambio.Text, Me.lblIngresoMN.Text, Me.lblEgresoMN.Text, Me.lblUtilidadMN.Text)
                End If
            End If
        End If

    End Sub


    Protected Sub CmdAgregar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdAgregar.Click
        Dim strruta As String
        Dim nombrearchivo1 As String
        Dim codigo_prp, codigo_dap, codigo_dip, codigo_cop, modifica As String
        codigo_dip = ""
        Dim rsDatos As New Data.DataTable
        Dim ObjCnx As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)


        If Me.lblIdPropuesta.Text = "" Then
            RegistrarPropuesta()
            codigo_prp = Me.lblIdPropuesta.Text
        Else
            codigo_prp = Me.lblIdPropuesta.Text
        End If
        Dim UltimaVersion As Integer
        Dim rsVersiones As New Data.DataTable
        rsVersiones = ObjCnx.TraerDataTable("CONSULTARVERSIONESPROPUESTA", "ES", codigo_prp, 0)
        UltimaVersion = rsVersiones.Rows(0).Item("version_dap")

        rsDatos = ObjCnx.TraerDataTable("PRP_ConsultarDatosCodigosPropuesta", codigo_prp, UltimaVersion)
        codigo_dap = rsDatos.Rows(0).Item("codigo_dap")
        codigo_cop = rsDatos.Rows(0).Item("codigo_cop")

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
                    codigo_dap = "C" & codigo_cop
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
            e.Row.Cells(4).Attributes.Add("onclick", "return confirm('¿Está seguro que desea elmininar este archivo?')")
        End If
    End Sub

    Protected Sub GridView1_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles GridView1.RowDeleting
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
    End Sub


    Protected Sub cmdGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdGuardar.Click
        RegistrarPropuesta()
    End Sub

    Protected Sub cmdAdjuntar0_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSalir.Click
        Response.Redirect("contenido.aspx?id=" & Me.lblIdUsuario.Text & "&instancia=" & Request.QueryString("instancia"))

    End Sub
End Class
