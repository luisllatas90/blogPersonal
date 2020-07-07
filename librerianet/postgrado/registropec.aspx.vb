Partial Class registropec
    Inherits System.Web.UI.Page

    Private tblTempModulos As Data.DataTable
    Private tblTempModulosView As Data.DataView

    Private Sub CargarModulosPECTemporal()
        Me.grwModulosPEC.DataSource = tblTempModulosView
        Me.grwModulosPEC.DataBind()

        Dim crd, ht, hi, ha, hep As Int16
        For i As Int16 = 0 To Session("tblTempModulosData").Rows.Count - 1
            crd += Session("tblTempModulosData").Rows(i).Item("creditos_cur")
            ht += Session("tblTempModulosData").Rows(i).Item("horasteo_cur")
            hi += Session("tblTempModulosData").Rows(i).Item("horaspra_cur")
            ha += Session("tblTempModulosData").Rows(i).Item("horasase_cur")
            hep += Session("tblTempModulosData").Rows(i).Item("horaslab_cur")
        Next
        Me.lbltotales.Text = "Créditos: " & crd.ToString & " | Horas: Teoría (HT): " & ht & " | Investigación (HI): " & hi.ToString & " | Asesoría (HA): " & ha.ToString & " | Estudio Personal (HEP): " & hep.ToString & " | Total Hrs. (TH): " & ht + hi + ha + hep
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("tblTempModulosData") Is Nothing Then

            tblTempModulos = New Data.DataTable()
            tblTempModulos.Columns.Add(New Data.DataColumn("codigo_cup", GetType(Integer)))
            tblTempModulos.Columns.Add(New Data.DataColumn("nombre_cur", GetType(String)))
            tblTempModulos.Columns.Add(New Data.DataColumn("ciclo_cur", GetType(Int16)))
            tblTempModulos.Columns.Add(New Data.DataColumn("creditos_cur", GetType(Int16)))
            tblTempModulos.Columns.Add(New Data.DataColumn("horasteo_cur", GetType(Int16)))
            tblTempModulos.Columns.Add(New Data.DataColumn("horaspra_cur", GetType(Int16)))
            tblTempModulos.Columns.Add(New Data.DataColumn("horasAse_cur", GetType(Int16)))
            tblTempModulos.Columns.Add(New Data.DataColumn("horaslab_cur", GetType(Int16)))
            tblTempModulos.Columns.Add(New Data.DataColumn("totalhoras_cur", GetType(Int16)))

            Session("tblTempModulosData") = tblTempModulos
        Else
            tblTempModulos = Session("tblTempModulosData")
        End If
        tblTempModulosView = New Data.DataView(tblTempModulos)

        If IsPostBack = False Then
            Dim codigo_tfu As Int16 = Request.QueryString("ctf")
            Dim codigo_usu As Integer = Request.QueryString("id")

            '=================================
            'Permisos por Escuela
            '=================================
            Dim fila As Data.DataRow

            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()

            'Cargar Centro de Costos
            Dim tblcco As Data.DataTable

            tblcco = obj.TraerDataTable("PRESU_ConsultarCentroCostosXPermisos", codigo_tfu, codigo_usu, "")
            fila = tblcco.NewRow()
            fila("codigo_cco") = -1
            fila("nombre") = "--Seleccionar--"
            'Añadir fila
            tblcco.Rows.Add(fila)

            Me.dpCodigo_cco.Items.Clear()
            Me.dpCodigo_cco0.Items.Clear()
            Me.dpCodigo_cco.DataSource = tblcco
            Me.dpCodigo_cco0.DataSource = tblcco
            Me.dpCodigo_cco.DataBind()
            Me.dpCodigo_cco0.DataBind()
            Me.dpCodigo_cco.SelectedValue = -1
            Me.dpCodigo_cco0.SelectedValue = -1
            tblcco.Dispose()

            'Cargar Personal
            Dim tblpersonal As Data.DataTable

            tblpersonal = obj.TraerDataTable("ConsultarPersonal", "TPA", 1)
            fila = tblpersonal.NewRow()
            fila("codigo_per") = -1
            fila("personal") = "--Seleccionar--"
            'Añadir fila
            tblpersonal.Rows.Add(fila)
            Me.dpCodigo_per.Items.Clear()
            Me.dpCodigo_per0.Items.Clear()
            Me.dpCodigo_per.DataSource = tblpersonal
            Me.dpCodigo_per0.DataSource = tblpersonal
            Me.dpCodigo_per.DataBind()
            Me.dpCodigo_per0.DataBind()
            Me.dpCodigo_per.SelectedValue = -1
            Me.dpCodigo_per0.SelectedValue = -1
            tblpersonal.Dispose()

            ClsFunciones.LlenarListas(Me.dpPrograma, obj.TraerDataTable("PEC_ConsultarProgramaEC", 8, codigo_usu, codigo_tfu, 0), "codigo_pec", "descripcion_pes", "--Seleccione--")
            ClsFunciones.LlenarListas(Me.dpCodigo_Tpec, obj.TraerDataTable("PEC_ConsultarProgramaEC", 0, "PG", 0, 0), "codigo_tpec", "descripcion_tpec")
            ClsFunciones.LlenarListas(Me.dpCodigo_epec, obj.TraerDataTable("PEC_ConsultarProgramaEC", 1, 0, 0, 0), "codigo_epec", "descripcion_epec")

            'Cargar Programas
            Me.grwPEC.DataSource = obj.TraerDataTable("PEC_ConsultarProgramaEC", 2, codigo_usu, codigo_tfu, "PG")
            Me.grwPEC.DataBind()

            obj.CerrarConexion()
            obj = Nothing

            LimpiarTablasTemporales()
        End If
    End Sub

    Private Sub CargarDatosPEC(ByVal codigo_pec As Integer)

        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Dim tblpec As Data.DataTable
        tblpec = obj.TraerDataTable("PEC_ConsultarProgramaEC", 3, codigo_pec, 0, 1)

        Me.hdcodigo_pec.Value = codigo_pec
        Me.dpCodigo_Tpec.SelectedValue = tblpec.Rows(0).Item("codigo_tpec")
        Me.txtDescripcion_pes.Text = tblpec.Rows(0).Item("descripcion_pes")
        Me.lbledicion_pet.Text = tblpec.Rows(0).Item("version_pec")
        Me.dpCodigo_epec.SelectedValue = tblpec.Rows(0).Item("codigo_epec")
        Me.txtInicio.Text = tblpec.Rows(0).Item("fechainicio_pec")
        Me.txtFin.Text = tblpec.Rows(0).Item("fechafin_pec")
        Me.txtResolucion_pec.Text = tblpec.Rows(0).Item("nroresolucion_pec")
        Me.txthorarios.Text = tblpec.Rows(0).Item("horarios_pec")
        Me.dpCodigo_cco.SelectedValue = IIf(tblpec.Rows(0).Item("codigo_cco") = 0, -1, tblpec.Rows(0).Item("codigo_cco"))
        Me.dpCodigo_per.SelectedValue = IIf(tblpec.Rows(0).Item("codigo_per") = 0, -1, tblpec.Rows(0).Item("codigo_per"))
        Me.lblOperador.Text = tblpec.Rows(0).Item("operador")

        Dim tblmodulos As Data.DataTable
        tblmodulos = obj.TraerDataTable("PEC_ConsultarProgramacionPEC", 0, tblpec.Rows(0).Item("codigo_pec"), 0, 1)
        If tblmodulos.Rows.Count = 0 Then
            CargarPrimerModuloTemporal()
        Else
            For i As Int16 = 0 To tblmodulos.Rows.Count - 1
                Me.AgregarModulosPEC(tblmodulos.Rows(i).Item("codigo_cup"), tblmodulos.Rows(i).Item("ciclo_cur"), tblmodulos.Rows(i).Item("nombre_cur"), tblmodulos.Rows(i).Item("creditos_cur"), tblmodulos.Rows(i).Item("horasteo_cur"), tblmodulos.Rows(i).Item("horaspra_cur"), tblmodulos.Rows(i).Item("horasase_cur"), tblmodulos.Rows(i).Item("horaslab_cur"))
            Next
            Me.CargarModulosPECTemporal()
        End If
        obj.CerrarConexion()
        obj = Nothing
        tblpec.Dispose()
        tblmodulos.Dispose()
    End Sub

    Protected Sub cmdAgregar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdAgregar.Click
        Me.HabilitarPanelesPEC(False, False)
        Me.fraPregunta.Visible = True
    End Sub

    Private Sub HabilitarPanelesPEC(ByVal MostrarLista As Boolean, ByVal MostrarDetalle As Boolean)
        'Lista de PEC
        Me.lblProgramas.Visible = MostrarLista
        Me.cmdAgregar.Visible = MostrarLista
        Me.grwPEC.Visible = MostrarLista

        'Lista Módulos PEC
        Me.grwModulosPEC.ShowFooter = False

        'Detalle de PEC
        Me.fraDetallePEC.Visible = MostrarDetalle

        'Limpiar Detalle de PEC
        Me.hdcodigo_pec.Value = 0
        'Me.dpCodigo_Tpec.SelectedValue = 1
        Me.txtDescripcion_pes.Text = ""
        Me.txtInicio.Text = ""
        Me.txtFin.Text = ""
        Me.txtResolucion_pec.Text = ""
        Me.lbledicion_pet.Text = 1
        Me.dpCodigo_epec.SelectedValue = 1
        Me.dpCodigo_cco.SelectedValue = -1
        Me.dpCodigo_per.SelectedValue = -1
        Me.cmdGuardar.Text = "Guardar"
    End Sub
    Private Sub CargarPrimerModuloTemporal()
        If Session("tblTempModulosData").rows.count = 0 Then
            Me.AgregarModulosPEC(0, 1, "-", 0, 0, 0, 0, 0)
        End If
        Me.CargarModulosPECTemporal()
    End Sub

    Protected Sub grwModulosPEC_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grwModulosPEC.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Cells(9).Attributes.Add("onclick", "return confirm('¿Esta seguro que desea eliminar el módulo?');")
        End If
    End Sub

    Protected Sub grwModulosPEC_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles grwModulosPEC.RowDeleting
        If grwModulosPEC.DataKeys(e.RowIndex).Value <> 0 Then
            Dim obj As New ClsConectarDatos
            Dim mensaje(1) As String
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            obj.Ejecutar("PEC_EliminarModuloPEC", grwModulosPEC.DataKeys(e.RowIndex).Value, 0).copyto(mensaje, 0)
            obj.CerrarConexion()
            obj = Nothing
            If mensaje(0).ToString = "" Then
                tblTempModulos.Rows.RemoveAt(e.RowIndex)
            Else
                Me.lblmensaje.Text = mensaje(0)
            End If
        Else
            tblTempModulos.Rows.RemoveAt(e.RowIndex)
        End If
        Me.CargarPrimerModuloTemporal()
        e.Cancel = True
    End Sub
    Protected Sub grwModulosPEC_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles grwModulosPEC.RowEditing
        Me.grwModulosPEC.EditIndex = e.NewEditIndex
        Me.CargarModulosPECTemporal()
    End Sub

    Protected Sub grwModulosPEC_RowUpdating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewUpdateEventArgs) Handles grwModulosPEC.RowUpdating
        Dim fila As GridViewRow = Me.grwModulosPEC.Rows(e.RowIndex)
        ModificarModulosPEC(e.RowIndex, CType(fila.Cells(0).FindControl("dpciclo_cur"), DropDownList).SelectedValue, CType(fila.Cells(1).FindControl("txtnombre_cur"), TextBox).Text.Trim, CType(fila.Cells(2).FindControl("txtcreditos_cur"), TextBox).Text, CType(fila.Cells(3).FindControl("txtht"), TextBox).Text, CType(fila.Cells(4).FindControl("txthi"), TextBox).Text, CType(fila.Cells(5).FindControl("txtha"), TextBox).Text, CType(fila.Cells(6).FindControl("txthep"), TextBox).Text)
        Me.grwModulosPEC.EditIndex = -1
        Me.CargarModulosPECTemporal()
    End Sub

    Protected Sub grwModulosPEC_RowCancelingEdit(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCancelEditEventArgs) Handles grwModulosPEC.RowCancelingEdit
        Me.grwModulosPEC.EditIndex = -1
        Me.CargarModulosPECTemporal()
    End Sub

    Private Sub LimpiarTablasTemporales()
        Session("tblTempModulosData") = Nothing
        tblTempModulos.Dispose()
        Me.grwModulosPEC.ShowFooter = False
        Me.grwModulosPEC.DataBind()
        Me.hdcodigo_pec.Value = 0

        'Limpiar valores de pregunta
        Me.dpCodigo_cco0.SelectedValue = -1
        Me.txtInicio0.Text = ""
        Me.txtFin0.Text = ""
        Me.txtResolucion_pec0.Text = ""
        Me.dpCodigo_per0.SelectedValue = -1
    End Sub

    Private Sub AgregarModulosPEC(ByVal codigo_cup As Int32, ByVal ciclo_cur As Int16, ByVal nombre_cur As String, ByVal crd As Int16, ByVal ht As Int16, ByVal hi As Int16, ByVal ha As Int16, ByVal hep As Int16)
        Dim fila As Data.DataRow

        fila = tblTempModulos.NewRow()

        fila("codigo_cup") = codigo_cup
        fila("nombre_cur") = nombre_cur
        fila("ciclo_cur") = ciclo_cur
        fila("creditos_cur") = crd
        fila("horasteo_cur") = ht
        fila("horaspra_cur") = hi
        fila("horasase_cur") = ha
        fila("horaslab_cur") = hep
        fila("totalhoras_cur") = ht + hi + ha + hep

        'Añadir fila
        tblTempModulos.Rows.Add(fila)

    End Sub

    Private Sub ModificarModulosPEC(ByVal ID As Int32, ByVal ciclo_cur As Int16, ByVal nombre_cur As String, ByVal crd As Int16, ByVal ht As Int16, ByVal hi As Int16, ByVal ha As Int16, ByVal hep As Int16)
        Dim fila As Data.DataRow

        fila = tblTempModulos.Rows(ID)

        tblTempModulos.Rows(ID).Item("nombre_cur") = nombre_cur
        tblTempModulos.Rows(ID).Item("ciclo_cur") = ciclo_cur
        tblTempModulos.Rows(ID).Item("creditos_cur") = crd
        tblTempModulos.Rows(ID).Item("horasteo_cur") = ht
        tblTempModulos.Rows(ID).Item("horaspra_cur") = hi
        tblTempModulos.Rows(ID).Item("horasase_cur") = ha
        tblTempModulos.Rows(ID).Item("horaslab_cur") = hep
        tblTempModulos.Rows(ID).Item("totalhoras_cur") = ht + hi + ha + hep
    End Sub

    Protected Sub imgGuardar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Dim nombre_cur As String
        Dim ciclo_cur As Int16
        Dim creditos_cur As String
        Dim horasteo_cur As String
        Dim horaspra_cur As String
        Dim horasAse_cur As String
        Dim horaslab_cur As String

        ciclo_cur = CType(Me.grwModulosPEC.FooterRow.Cells(0).FindControl("dpciclo_cur"), DropDownList).SelectedValue
        nombre_cur = CType(Me.grwModulosPEC.FooterRow.Cells(1).FindControl("txtnombre_cur"), TextBox).Text.Trim
        creditos_cur = CType(Me.grwModulosPEC.FooterRow.Cells(2).FindControl("txtcreditos_cur"), TextBox).Text
        horasteo_cur = CType(Me.grwModulosPEC.FooterRow.Cells(3).FindControl("txtht"), TextBox).Text
        horaspra_cur = CType(Me.grwModulosPEC.FooterRow.Cells(4).FindControl("txthi"), TextBox).Text
        horasAse_cur = CType(Me.grwModulosPEC.FooterRow.Cells(5).FindControl("txtha"), TextBox).Text
        horaslab_cur = CType(Me.grwModulosPEC.FooterRow.Cells(6).FindControl("txthep"), TextBox).Text

        Me.AgregarModulosPEC(0, ciclo_cur, nombre_cur, creditos_cur, horasteo_cur, horaspra_cur, horasAse_cur, horaslab_cur)
        'Response.Write(ciclo_cur & "--" & nombre_cur & "--" & creditos_cur & "--" & horasteo_cur & "--" & horaspra_cur & "--" & horasAse_cur & "--" & horaslab_cur)

        Me.grwModulosPEC.ShowFooter = False
        Me.CargarModulosPECTemporal()
    End Sub

    Protected Sub cmdCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdCancelar.Click
        Me.fraPregunta.Visible = False
        Me.HabilitarPanelesPEC(True, False)
        Me.LimpiarTablasTemporales()
    End Sub

    Protected Sub cmdNuevoModulo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdNuevoModulo.Click
        Me.grwModulosPEC.ShowFooter = True
        Me.CargarModulosPECTemporal()
    End Sub
    Protected Sub cmdGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdGuardar.Click
        Dim obj As New ClsConectarDatos
        Try
            'Validar módulos registrados
            For I As Int16 = 0 To tblTempModulos.Rows.Count - 1
                If tblTempModulos.Rows(I).Item("horasTeo_Cur") = 0 Then
                    Page.RegisterStartupScript("FaltaModulos", "<script>alert('Debe registar correctamente los módulos del Programa.\n Como mínimo registrar el nombre del módulo y las horas de teoría')</script>")
                    Exit Sub
                End If
            Next

            'Validar Centro Costos y Responsable
            If Me.dpCodigo_per.SelectedValue = -1 Or dpCodigo_Tpec.SelectedValue = -1 Then
                Page.RegisterStartupScript("CompletarDatos", "<script>alert('Complete correctamente el Tipo, Centro de Costos y el Responsable del Programa')</script>")
                Exit Sub
            End If

            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            Dim codigo_usu As Integer = Request.QueryString("id")
            Dim codigo_tfu As Integer = Request.QueryString("ctf")
            Dim valoresdevueltos(1) As String

            obj.IniciarTransaccion()
            '===================================================
            'Grabar Datos del Programa
            '===================================================
            If Me.cmdGuardar.Text = "Guardar" Then
                obj.Ejecutar("PEC_AgregarProgramaEC", Me.dpCodigo_Tpec.SelectedValue, UCase(Me.txtDescripcion_pes.Text.Trim), Me.dpCodigo_cco.SelectedValue, CDate(Me.txtInicio.Text), CDate(Me.txtFin.Text), Me.txtResolucion_pec.Text, 0, Me.txthorarios.Text.Trim, Me.lbledicion_pet.Text, Me.dpCodigo_epec.SelectedValue, Me.dpCodigo_per.SelectedValue, codigo_usu,25,"").copyto(valoresdevueltos, 0)
                'Response.Write(Me.dpCodigo_Tpec.SelectedValue & "," & Me.txtDescripcion_pes.Text.Trim & "," & dpCodigoAsignado_cco.SelectedValue & "," & Me.txtInicio.Text & "," & Me.txtFin.Text & "," & Me.txtResolucion_pec.Text & "," & Me.dpEdicion.SelectedValue & "," & Me.dpCodigo_epec.SelectedValue & "," & Me.dpCodigo_per.SelectedValue)
            Else
                obj.Ejecutar("PEC_ModificarProgramaEC", Me.hdcodigo_pec.Value, Me.dpCodigo_Tpec.SelectedValue, UCase(Me.txtDescripcion_pes.Text.Trim), Me.dpCodigo_cco.SelectedValue, CDate(Me.txtInicio.Text), CDate(Me.txtFin.Text), Me.txtResolucion_pec.Text, 0, Me.txthorarios.Text.Trim, Me.lbledicion_pet.Text, Me.dpCodigo_epec.SelectedValue, Me.dpCodigo_per.SelectedValue, codigo_usu)
                valoresdevueltos(0) = hdcodigo_pec.Value
            End If
            '===================================================
            'Grabar Módulos del Programa
            '===================================================
            If valoresdevueltos(0).ToString <> "" Then
                For I As Int16 = 0 To tblTempModulos.Rows.Count - 1
                    If tblTempModulos.Rows(I).Item("codigo_cup") = 0 Then
                        obj.Ejecutar("PEC_AgregarModulosProgramaEC", valoresdevueltos(0), UCase(tblTempModulos.Rows(I).Item("nombre_cur")), tblTempModulos.Rows(I).Item("ciclo_Cur"), tblTempModulos.Rows(I).Item("creditos_Cur"), tblTempModulos.Rows(I).Item("horasTeo_Cur"), tblTempModulos.Rows(I).Item("horasPra_Cur"), tblTempModulos.Rows(I).Item("horasLab_Cur"), tblTempModulos.Rows(I).Item("horasAse_Cur"), codigo_usu)
                    Else
                        obj.Ejecutar("PEC_ModificarModulosProgramaEC", Me.hdcodigo_pec.Value, tblTempModulos.Rows(I).Item("codigo_cup"), UCase(tblTempModulos.Rows(I).Item("nombre_cur")), tblTempModulos.Rows(I).Item("ciclo_Cur"), tblTempModulos.Rows(I).Item("creditos_Cur"), tblTempModulos.Rows(I).Item("horasTeo_Cur"), tblTempModulos.Rows(I).Item("horasPra_Cur"), tblTempModulos.Rows(I).Item("horasLab_Cur"), tblTempModulos.Rows(I).Item("horasAse_Cur"), codigo_usu)
                    End If
                Next
            Else
                obj.AbortarTransaccion()
                obj = Nothing
                Me.lblmensaje.Text = "No se guardaron los datos. Verifique las fechas acorde a los ciclo académico actual."
                Exit Sub
            End If
            obj.TerminarTransaccion()
            obj.AbrirConexion()
            obj.Ejecutar("ActualizarDatosPlanEstudio", "P", valoresdevueltos(0))
            Me.grwPEC.DataSource = obj.TraerDataTable("PEC_ConsultarProgramaEC", 2, codigo_usu, codigo_tfu, 0)
            Me.grwPEC.DataBind()
            obj.CerrarConexion()
            obj = Nothing

            Me.HabilitarPanelesPEC(True, False)
            Me.LimpiarTablasTemporales()

        Catch ex As Exception
            obj.AbortarTransaccion()
            obj = Nothing
            Me.lblmensaje.Text = "Ocurrió un error al Guardar. Contáctese con desarrollosistemas@usat.edu.pe, enviando el siguiente mensaje de error:<br /> " + ex.Message
        End Try
    End Sub
    Protected Sub grwPEC_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grwPEC.RowCommand
        Me.hdcodigo_pec.Value = 0
        If (e.CommandName = "editar") Then
            Me.HabilitarPanelesPEC(False, True)
            Me.CargarDatosPEC(Convert.ToString(Me.grwPEC.DataKeys(Convert.ToInt32(e.CommandArgument)).Value))
            Me.cmdGuardar.Text = "  Actualizar"
        End If
    End Sub

    Protected Sub grwPEC_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grwPEC.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            'Dim fila As Data.DataRowView
            'fila = e.Row.DataItem
            e.Row.Attributes.Add("OnMouseOver", "Resaltar(1,this,'S')")
            e.Row.Attributes.Add("OnMouseOut", "Resaltar(0,this,'S')")
            e.Row.Cells(0).Text = e.Row.RowIndex + 1
            e.Row.Cells(11).Attributes.Add("onClick", "return confirm('Acción irreversible: ¿Está seguro que desea eliminar el Programa seleccionado?')")
        End If
    End Sub

    Protected Sub cmdRegresar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdRegresar.Click
        Me.cmdCancelar_Click(sender, e)
    End Sub

    Protected Sub cmdContinuar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdContinuar.Click
        If Opt1.Checked = True Then
            Me.fraPregunta.Visible = False
            Me.HabilitarPanelesPEC(False, True)
            Me.CargarPrimerModuloTemporal()
        End If
        If Opt2.Checked = True Then
            If Me.dpPrograma.SelectedValue = -1 Or Me.txtResolucion_pec0.Text.Trim = "" Or _
                Me.txtInicio0.Text = "" Or Me.txtFin0.Text = "" Or _
                Me.dpCodigo_cco0.SelectedValue = -1 Or Me.dpCodigo_per0.SelectedValue = -1 Then
                Page.RegisterStartupScript("CompletarVersion", "<script>alert('Complete todos los datos solicitados para generar una nueva versión del Programa')</script>")
            Else
                '===================================================
                'Crear Nueva Versión del Programa
                '===================================================
                Dim obj As New ClsConectarDatos
                Try
                    obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
                    Dim codigo_usu As Integer = Request.QueryString("id")
                    Dim codigo_tfu As Integer = Request.QueryString("ctf")
                    obj.AbrirConexion()
                    obj.Ejecutar("PEC_CopiarProgramaEC", Me.dpPrograma.SelectedValue, Me.dpCodigo_cco0.SelectedValue, CDate(Me.txtInicio0.Text), CDate(Me.txtFin0.Text), Me.txtResolucion_pec0.Text, Me.dpCodigo_per0.SelectedValue, codigo_usu)

                    'Mostrar los datos registrados
                    Me.grwPEC.DataSource = obj.TraerDataTable("PEC_ConsultarProgramaEC", 2, codigo_usu, codigo_tfu)
                    Me.grwPEC.DataBind()
                    obj.CerrarConexion()
                    obj = Nothing
                    Me.fraPregunta.Visible = False
                    Me.HabilitarPanelesPEC(True, False)
                    Me.LimpiarTablasTemporales()
                    Page.RegisterStartupScript("OkVersion", "<script>alert('Se ha registrado correctamente una NUEVA EDICIÓN del Programa seleccionado')</script>")
                Catch ex As Exception
                    obj = Nothing
                    Me.lblmensaje0.Text = "Ocurrió un error al Guardar. Contáctese con desarrollosistemas@usat.edu.pe, enviando el siguiente mensaje de error:<br /> " + ex.Message
                End Try
            End If
        End If
    End Sub

    Protected Sub grwPEC_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles grwPEC.RowDeleting
        Dim obj As New ClsConectarDatos
        Dim mensaje(1) As String
        Dim codigo_usu As Integer = Request.QueryString("id")
        Dim codigo_tfu As Integer = Request.QueryString("ctf")
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        obj.Ejecutar("PEC_EliminarProgramaEC", grwPEC.DataKeys(e.RowIndex).Value, "").copyto(mensaje, 0)
        If mensaje(0).ToString <> "" Then
            obj.CerrarConexion()
            Page.RegisterStartupScript("EliminarPEC", "<script>alert('" & mensaje(0).ToString & "')</script>")
        Else
            'Mostrar los datos registrados
            Me.grwPEC.DataSource = obj.TraerDataTable("PEC_ConsultarProgramaEC", 2, codigo_usu, codigo_tfu, "PG")
            Me.grwPEC.DataBind()
            obj.CerrarConexion()
        End If
        obj = Nothing
        e.Cancel = True
    End Sub
End Class