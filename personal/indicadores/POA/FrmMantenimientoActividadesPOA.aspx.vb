﻿Partial Class indicadores_POA_PROTOTIPOS_Registrar_POA
    Inherits System.Web.UI.Page
    Dim LastObjetivo As String = String.Empty
    Dim CurrentRow As Integer = -1
    Dim Ingreso As Decimal = 0
    Dim Egreso As Decimal = 0

    Sub CargarDatosActividad(ByVal codigo_acp As Integer, ByVal codigo_poa As Integer)
        Dim obj As New clsPlanOperativoAnual
        Dim dt As New Data.DataTable
        dt = obj.CargaDatosActividad(codigo_acp, codigo_poa)
        Dim nombre_poa As String = dt.Rows(0).Item("nombre_poa")
        Me.lblPoaPaso1.Text = nombre_poa
        Me.lblPoaPaso2.Text = nombre_poa
        Me.lblpoaPaso3.Text = nombre_poa
        Me.lblPoaPaso4.Text = nombre_poa

        If dt.Rows.Count = 1 Then
            If codigo_acp <> 0 Then
                'Edicion de Actividad
                Me.lblNombreActividad.Text = dt.Rows(0).Item("resumen_acp")
                Me.hdcodigo_ejp.Value = dt.Rows(0).Item("codigo_ejp")
                Me.hddescripcion_ejp.Value = dt.Rows(0).Item("descripcion_Ejp")
                Me.ddlTipoActividad.SelectedValue = dt.Rows(0).Item("codigo_tac")
                CargaCecosxPOA(codigo_poa, Me.ddlTipoActividad.SelectedValue, dt.Rows(0).Item("codigo_cco"))
                Me.ddlCeco.SelectedValue = dt.Rows(0).Item("codigo_cco")
                Me.ddlCategoria.SelectedValue = dt.Rows(0).Item("codigo_cpa")
                hdCodigoCat.Value = dt.Rows(0).Item("codigo_cpa")

                'Deshabilita Edicion de Centro de Costo
                Me.ddlCeco.Enabled = False
                Me.ddlTipoActividad.Enabled = False
                'Oculta Boton de Nuevo Centro de Costo
                Me.btnAdicionarCco.Visible = False
                '
                Me.hdcodigo_cco.Value = dt.Rows(0).Item("codigo_cco")
                Me.lblingresospoa.Text = FormatNumber(dt.Rows(0).Item("limite_ingreso"))
                Me.lblpresupuestopoa.Text = FormatNumber(dt.Rows(0).Item("limite_egreso"), 2)
                Me.lblacumulado.Text = FormatNumber(dt.Rows(0).Item("acumulado"), 2)
                Me.lbldisponible.Text = FormatNumber(dt.Rows(0).Item("limite_egreso") - dt.Rows(0).Item("utilizado") + dt.Rows(0).Item("egresos_acp"))
                Me.ddlProgPresupuestal.SelectedValue = dt.Rows(0).Item("codigo_ppr") ' POR DEFECTO 1 FALTA COLUMNA EN CENTRO COSTO
                Me.txtabreviatura.Text = dt.Rows(0).Item("abreviatura_acp")
                Me.txtdescripcion.Text = dt.Rows(0).Item("resumen_acp")
                Me.ddlMesInicio.SelectedValue = Month(dt.Rows(0).Item("fecini_acp"))
                MesFin()
                Me.ddlMesFin.SelectedValue = Month(dt.Rows(0).Item("fecfin_acp"))

                lblfecini.Text = dt.Rows(0).Item("fecini_acp")
                lblfecfin.Text = dt.Rows(0).Item("fecfin_acp")

                Me.txtEgresos.Text = FormatNumber(dt.Rows(0).Item("egresos_acp"), 2)
                Me.txtIngresos.Text = FormatNumber(dt.Rows(0).Item("ingresos_acp"), 2)
                Me.txtUtilidad.Text = FormatNumber(dt.Rows(0).Item("utilidad_acp"), 2)
                Me.hdcodigoPei.Value = dt.Rows(0).Item("codigo_pla")
                Me.lblPeiPaso1.Text = dt.Rows(0).Item("periodo_pla")
                Me.lblPeiPaso2.Text = dt.Rows(0).Item("periodo_pla")
                Me.lblPeiObjetivosPaso3.Text = dt.Rows(0).Item("periodo_pla")
                Me.lblPeiPaso4.Text = dt.Rows(0).Item("periodo_pla")
                Me.ddlResponsable.SelectedValue = dt.Rows(0).Item("responsable_acp")
                Me.ddlApoyo.SelectedValue = dt.Rows(0).Item("apoyo_acp")
                Me.lbltipoactividadPaso1.Text = Me.ddlTipoActividad.SelectedItem.ToString
                If Me.ddlTipoActividad.SelectedValue = 2 Then
                    Me.lblutilidadp1.Text = "Excedente"
                    Me.txtUtilidad.Visible = True
                    If Me.txtUtilidad.Text > 0 Then
                        If Me.txtEgresos.Text > 0 Then
                            Me.lblutilidad_porcentaje.Text = FormatNumber((Me.txtUtilidad.Text / Me.txtEgresos.Text) * 100, 0)
                        Else
                            Me.lblutilidad_porcentaje.Text = Me.txtUtilidad.Text
                        End If
                    Else
                        Me.lblutilidad_porcentaje.Text = 0
                    End If
                    Me.Label4.Text = "%"
                Else
                    Me.lblutilidad_porcentaje.Text = ""
                    Me.Label4.Text = ""
                End If
                Me.lbltipoactividadPaso2.Text = Me.ddlTipoActividad.SelectedItem.ToString
                Me.lbltipoactividadPaso3.Text = Me.ddlTipoActividad.SelectedItem.ToString
                Me.lblTipoactividadPaso4.Text = Me.ddlTipoActividad.SelectedItem.ToString
                Me.ddl_p4_responsable.SelectedValue = Me.ddlResponsable.SelectedValue
                Dim Tipo_actividad As String
                Tipo_actividad = Left(Me.ddlTipoActividad.SelectedItem.ToString, 1) & Right(Me.ddlTipoActividad.SelectedItem.ToString.ToLower, Me.ddlTipoActividad.SelectedItem.ToString.Length - 1)
                Me.lbltituloPaso2.Text = "Alineamiento de " & Tipo_actividad
                Me.lbltituloPaso3.Text = "Objetivos e Indicadores del " & Tipo_actividad
                Me.lbltituloPaso4.Text = "Actividades de " & Tipo_actividad

                'Detalle de Programas o Proyectos
                Call wf_CargarDetalleActividad(codigo_acp)
                'Carga Observaciones
                CargaObservaciones(Me.hdcodigoacp.Value)

                If Request.QueryString("tipo_acc") = "PL" Then

                    If dt.Rows(0).Item("codigo_iep") <> 1 And dt.Rows(0).Item("codigo_iep") <> 3 And dt.Rows(0).Item("codigo_iep") <> 6 And dt.Rows(0).Item("codigo_iep") <> 21 And dt.Rows(0).Item("codigo_iep") <> 22 Then 'Modificado fatima.vasquez 09-08-2018
                        'If dt.Rows(0).Item("codigo_iep") <> 1 And dt.Rows(0).Item("codigo_iep") <> 3 And dt.Rows(0).Item("codigo_iep") <> 6 Then
                        Me.observar.Visible = True
                    ElseIf dt.Rows(0).Item("codigo_iep") = 6 Then
                        Me.observar.Visible = True
                        Me.aprobar.Visible = True
                    ElseIf dt.Rows(0).Item("codigo_iep") = 3 Then
                        Me.observar.Visible = True
                        Me.aprobar.Visible = True
                        '==========Agregado fatima.vasquez 09-08-2018========
                    ElseIf dt.Rows(0).Item("codigo_iep") = 21 Or dt.Rows(0).Item("codigo_iep") = 22 Then
                        Me.observar.Visible = True
                        Me.aprobar.Visible = True
                        '==========Fin fatima.vasquez 09-08-2018========
                    End If
                    Me.btnAdicionarCco.Visible = False
                End If

                ddlCategoria_cco.Enabled = True
                ddlCategoria.Enabled = True
            Else
                ddlCategoria_cco.Enabled = True
                ddlCategoria.Enabled = True

                'Nueva Actividad
                Me.hdcodigo_ejp.Value = dt.Rows(0).Item("codigo_ejp")
                Me.hddescripcion_ejp.Value = dt.Rows(0).Item("descripcion_Ejp")
                Me.lblpresupuestopoa.Text = FormatNumber(dt.Rows(0).Item("limite_egreso"), 2)
                Me.lblPeiPaso1.Text = dt.Rows(0).Item("periodo_pla")
                Me.lblPeiPaso2.Text = dt.Rows(0).Item("periodo_pla")
                Me.lblPeiObjetivosPaso3.Text = dt.Rows(0).Item("periodo_pla")
                Me.lblPeiPaso4.Text = dt.Rows(0).Item("periodo_pla") & dt.Rows(0).Item("descripcion_Ejp")
                Me.hdcodigoPei.Value = dt.Rows(0).Item("codigo_pla")
                Me.lblacumulado.Text = FormatNumber(dt.Rows(0).Item("acumulado"), 2)
                Me.lblingresospoa.Text = FormatNumber(dt.Rows(0).Item("limite_ingreso"))
                If FormatNumber(dt.Rows(0).Item("limite_egreso") - dt.Rows(0).Item("utilizado"), 2) >= 0 Then
                    Me.lbldisponible.Text = FormatNumber(dt.Rows(0).Item("limite_egreso") - dt.Rows(0).Item("utilizado"), 2)
                Else
                    Me.lbldisponible.Text = "0.00"
                    Me.lblexcedido.Text = "(EXCEDIDO : " + FormatNumber(-1 * (dt.Rows(0).Item("limite_egreso") - dt.Rows(0).Item("utilizado")), 2).ToString + ")"
                    Me.lblexcedido.Visible = True
                End If

                Me.tab2.Enabled = False
                Me.tab3.Enabled = False
                Me.tab4.Enabled = False
                Me.lblutilidad_porcentaje.Text = ""
                Me.Label4.Text = ""
                Call wf_habilitarControlesLoad(False)
            End If
            '-Cargar Arbol
            Me.ArbolPaso2.Nodes.Clear()
            Me.ListBox1.Items.Clear()
            CargarInicialArbol()
            '-
        Else
            Response.Write("Se Obtuvo mas de un Resultado, Verificar.")
        End If
    End Sub

    Sub wf_habilitarControlesLoad(ByVal as_flag As Boolean)
        ddlCategoria.Enabled = as_flag
        'ddlProgPresupuestal.Enabled = as_flag
        ddlResponsable.Enabled = as_flag
        'ddlApoyo.Enabled = as_flag
        txtabreviatura.Enabled = as_flag
        txtdescripcion.Enabled = as_flag
        ddlMesInicio.Enabled = as_flag
        ddlMesFin.Enabled = as_flag
        txtIngresos.Enabled = as_flag
        txtEgresos.Enabled = as_flag
        txtUtilidad.Enabled = as_flag
    End Sub

    Sub wf_habilitarControlesPaso1(ByVal as_flag As Boolean)
        ddlApoyo.Enabled = as_flag
        txtabreviatura.Enabled = as_flag
        txtdescripcion.Enabled = Not as_flag
        ddlMesInicio.Enabled = as_flag
        ddlMesFin.Enabled = as_flag
        txtIngresos.Enabled = as_flag
        txtEgresos.Enabled = as_flag
        txtUtilidad.Enabled = as_flag
    End Sub

    Sub CargaObservaciones(ByVal codigo_acp As Integer)
        Dim obj As New clsPlanOperativoAnual
        Dim dt As New Data.DataTable
        Dim Observaciones, colorLetra As String

        Observaciones = "<table width='98%'><tr><td><b>Observaciones : </b></td></tr>"
        dt = obj.ListaObservaciones(codigo_acp)
        If dt.Rows.Count > 0 Then
            For i As Integer = 0 To dt.Rows.Count - 1
                If dt.Rows(i).Item("resuelto_obs").ToString = 0 Then
                    colorLetra = "<div class='observado'>" & "&nbsp;&nbsp; - " & dt.Rows(i).Item("descripcion_obs").ToString & " <b>(Verificar)</b></div>"
                Else
                    colorLetra = "<div class='resuelto'>" & "&nbsp;&nbsp; - " & dt.Rows(i).Item("descripcion_obs").ToString & " <b>(Resuelto)</b></div>"

                End If

                Observaciones = Observaciones & "<tr><td>" & colorLetra & "</td></tr>"
            Next
            Observaciones = Observaciones & "</table>"
            Me.lblobservaciones.InnerHtml = Observaciones
            Me.lblobservaciones.Attributes.Add("class", "Observacion")
        Else
            Me.lblobservaciones.Attributes.Clear()
            Me.lblobservaciones.InnerHtml = ""
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (Session("id_per") Is Nothing) Then
            Response.Redirect("../../../sinacceso.html")
        End If
        'Me.cmdGuardarPaso1.Attributes.Add("onclick", " return ConfirmaExceso();")
        Try
            If IsPostBack = False Then
                'Me.ddlProgPresupuestal.Enabled = False
                Me.paso1.Visible = True
                Me.paso2.Visible = False
                Me.paso3.Visible = False
                Me.paso4.Visible = False

                Dim codigo_poa As Integer
                Me.hdcodigoacp.Value = Request.QueryString("codigo_acp")
                codigo_poa = Request.QueryString("codigo_poa")
                CargaTipoActividad()
                CargaProgramaPresupuestal()
                CargaPersonal(codigo_poa)
                Call CargaCategoriaProgProy()

                If Request.QueryString("codigo_acp") <> 0 Then
                    Me.hdcodigoacp.Value = Request.QueryString("codigo_acp")
                End If

                'Call wf_limpiarGridViewDetalleActividad()
                Call CargarDatosActividad(Me.hdcodigoacp.Value, codigo_poa)
                If Request.QueryString("back") = "pto" Then
                    Me.tab4_Click(sender, e)
                End If
                txt_meta1.Text = 0.0
                txt_meta2.Text = 0.0
                txt_meta3.Text = 0.0
                txt_meta4.Text = 0.0
            End If

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

#Region "PANEL N°1"

    Sub CargaTipoActividad()
        Dim objPoa As New clsPlanOperativoAnual
        Dim dt As New Data.DataTable
        dt = objPoa.ListaTipoActividad()
        Me.ddlTipoActividad.DataSource = dt
        Me.ddlTipoActividad.DataTextField = "descripcion"
        Me.ddlTipoActividad.DataValueField = "codigo"
        Me.ddlTipoActividad.DataBind()

        Me.ddlTipoActividad_cco.DataSource = dt
        Me.ddlTipoActividad_cco.DataTextField = "descripcion"
        Me.ddlTipoActividad_cco.DataValueField = "codigo"
        Me.ddlTipoActividad_cco.DataBind()

        objPoa = Nothing
    End Sub

    Sub CargaProgramaPresupuestal()
        Dim objPoa As New clsPlanOperativoAnual
        Dim dt As New Data.DataTable
        dt = objPoa.CargaProgramaPresupuestal(3, 0)
        If dt.Rows.Count > 0 Then
            Me.ddlProgPresupuestal.DataSource = dt
            Me.ddlProgPresupuestal.DataTextField = "descripcion"
            Me.ddlProgPresupuestal.DataValueField = "codigo"
            Me.ddlProgPresupuestal.DataBind()

            Me.ddlprogpresupuestal_cco.DataSource = dt
            Me.ddlprogpresupuestal_cco.DataTextField = "descripcion"
            Me.ddlprogpresupuestal_cco.DataValueField = "codigo"
            Me.ddlprogpresupuestal_cco.DataBind()
            objPoa = Nothing
            Me.ddlprogpresupuestal_cco.SelectedValue = 1 ' GESTION OPERATIVA
        End If
    End Sub

    Protected Sub ddlTipoActividad_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlTipoActividad.SelectedIndexChanged
        Dim codigo_poa, codigo_tac, nro_act As Integer
        Try
            Dim obj As New clsPlanOperativoAnual
            Dim dt As New Data.DataTable
            codigo_poa = Request.QueryString("codigo_poa")
            codigo_tac = Me.ddlTipoActividad.SelectedValue
            dt = obj.ListaActividadesxPOA(codigo_poa, codigo_tac, 0, 0)
            nro_act = dt.Rows.Count + 1

            CargaCecosxPOA(codigo_poa, codigo_tac, Me.hdcodigo_cco.Value)
            If Me.ddlTipoActividad.SelectedValue > 0 Then
                Me.txtabreviatura.Text = Left(Me.ddlTipoActividad.SelectedItem.ToString, 4) & String.Format("{0:000}", nro_act)
            Else
                Me.txtabreviatura.Text = ""
            End If
            Me.txtdescripcion.Text = ""
            Me.ddlProgPresupuestal.SelectedValue = 0
            Me.ddlResponsable.SelectedValue = 0

            If ddlTipoActividad.SelectedValue <> 0 Then
                Me.lbltipoactividadPaso1.Text = Me.ddlTipoActividad.SelectedItem.ToString
                Me.lbltipoactividadPaso2.Text = Me.ddlTipoActividad.SelectedItem.ToString
                Me.lbltipoactividadPaso3.Text = Me.ddlTipoActividad.SelectedItem.ToString
                Me.lblTipoactividadPaso4.Text = Me.ddlTipoActividad.SelectedItem.ToString
                Dim Tipo_actividad As String
                Tipo_actividad = Left(Me.ddlTipoActividad.SelectedItem.ToString, 1) & Right(Me.ddlTipoActividad.SelectedItem.ToString.ToLower, Me.ddlTipoActividad.SelectedItem.ToString.Length - 1)
                Me.lbltituloPaso2.Text = "Alineamiento de " & Tipo_actividad
                Me.lbltituloPaso3.Text = "Objetivos e Indicadores del " & Tipo_actividad
                Me.lbltituloPaso4.Text = "Actividades de " & Tipo_actividad
                If Me.ddlTipoActividad.SelectedValue = 2 Then
                    Me.lblutilidadp1.Text = "Excedente"
                    Me.txtUtilidad.Visible = True
                    Me.Label4.Text = "%"
                    If Me.txtUtilidad.Text > 0 Then
                        If Me.txtEgresos.Text > 0 Then
                            Me.lblutilidad_porcentaje.Text = FormatNumber((Me.txtUtilidad.Text / Me.txtEgresos.Text) * 100, 0)
                        Else
                            Me.lblutilidad_porcentaje.Text = Me.txtUtilidad.Text
                        End If
                    Else
                        Me.lblutilidad_porcentaje.Text = 0
                    End If
                    Me.ddlMesInicio.SelectedValue = 0
                    Me.ddlMesFin.SelectedValue = 0

                Else
                    Me.lblutilidadp1.Text = ""
                    Me.txtUtilidad.Visible = False
                    Me.lblutilidad_porcentaje.Text = ""
                    Me.Label4.Text = ""
                    If Me.hdcodigoacp.Value = 0 Then
                        Me.ddlMesInicio.SelectedValue = 1 'Enero
                        Me.ddlMesInicio_SelectedIndexChanged(sender, e)
                        Me.ddlMesFin.SelectedValue = 12 ' Diciembre
                    End If

                End If
            End If

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Sub CargaCecosxPOA(ByVal codigo_poa As Integer, ByVal codigo_tac As Integer, ByVal codigo_cco As Integer)
        Dim objPoa As New clsPlanOperativoAnual
        Dim dt As New Data.DataTable
        dt = objPoa.CeCosAsignadosxPOA(codigo_poa, codigo_tac, codigo_cco)
        Me.ddlCeco.DataSource = dt
        Me.ddlCeco.DataTextField = "descripcion"
        Me.ddlCeco.DataValueField = "codigo"
        Me.ddlCeco.DataBind()
        objPoa = Nothing
    End Sub

    Sub CargaCategoriaProgProy()
        Try
            Dim obj As New clsPlanOperativoAnual
            Dim dt As New Data.DataTable
            'dt = obj.POA_CategoriaProgProyActividad(0, 0, 0, 0, "CPP")
            dt = obj.POA_AreasCategoria(0, Request.QueryString("codigo_poa"), 0, 0, "CPP", 0)

            Me.ddlCategoria_cco.DataSource = dt
            Me.ddlCategoria_cco.DataTextField = "descripcion"
            Me.ddlCategoria_cco.DataValueField = "codigo"
            Me.ddlCategoria_cco.DataBind()

            Me.ddlCategoria.DataSource = dt
            Me.ddlCategoria.DataTextField = "descripcion"
            Me.ddlCategoria.DataValueField = "codigo"
            Me.ddlCategoria.DataBind()

            dt.Dispose()
            obj = Nothing
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try

    End Sub

    Sub CargaPersonal(ByVal codigo_poa As Integer)
        Dim objPoa As New clsPlanOperativoAnual
        Dim dt As New Data.DataTable
        dt = objPoa.ListaPoas(codigo_poa)
        If dt.Rows.Count > 0 Then
            dt = objPoa.POA_ListaPersonalxCeco(dt.Rows(0).Item("codigo_cco"), 0)
            Me.ddlResponsable.DataSource = dt
            Me.ddlResponsable.DataTextField = "Personal"
            Me.ddlResponsable.DataValueField = "codigo"
            Me.ddlResponsable.DataBind()

            Me.ddlresponsable_cco.DataSource = dt
            Me.ddlresponsable_cco.DataTextField = "Personal"
            Me.ddlresponsable_cco.DataValueField = "codigo"
            Me.ddlresponsable_cco.DataBind()

            Me.ddlApoyo.DataSource = dt
            Me.ddlApoyo.DataTextField = "Personal"
            Me.ddlApoyo.DataValueField = "codigo"
            Me.ddlApoyo.DataBind()

            Me.ddl_p4_responsable.DataSource = dt
            Me.ddl_p4_responsable.DataTextField = "Personal"
            Me.ddl_p4_responsable.DataValueField = "codigo"
            Me.ddl_p4_responsable.DataBind()

        End If
        objPoa = Nothing
    End Sub

    Protected Sub ddlCeco_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlCeco.SelectedIndexChanged
        Try
            If Me.ddlCeco.SelectedValue = 0 Then
                Me.txtdescripcion.Text = ""
                Me.ddlProgPresupuestal.SelectedValue = 0
                Me.ddlResponsable.SelectedValue = 0
            Else
                Dim obj As New clsPlanOperativoAnual
                Dim dt As Data.DataTable
                dt = obj.ConsultaCecoxcodigo(Me.ddlCeco.SelectedValue)
                Me.txtdescripcion.Text = dt.Rows(0).Item("descripcion_cco")
                'Me.ddlProgPresupuestal.SelectedValue = dt.Rows(0).Item("codigo_ppr")
                If dt.Rows(0).Item("estado_per") = 1 Then
                    Me.ddlResponsable.SelectedValue = dt.Rows(0).Item("codigo_per")
                    Me.ddl_p4_responsable.SelectedValue = Me.ddlResponsable.SelectedValue
                    Me.lblmensaje_cco.Text = ""
                    Me.aviso_cco_per.Attributes.Clear()
                Else
                    Me.lblmensaje_cco.Text = "El Centro de Costo : " & Me.ddlCeco.SelectedItem.ToString & ", No Tiene asignado un Personal o se encuentra Inactivo, Coordinar con Área de Presupuesto "
                    Me.aviso_cco_per.Attributes.Add("class", "mensajeError")

                    Me.ddlResponsable.SelectedValue = 0
                    Me.ddlResponsable.Enabled = False

                End If
            End If

            If ddlTipoActividad.SelectedValue <> 0 And ddlCeco.SelectedValue <> 0 Then
                Call wf_habilitarControlesLoad(True)
                'Me.TDProgramaPresupuestal.Attributes.Remove("Style")
                'Me.TDProgramaPresupuestal.Attributes.Add("Style", "display:none")
                ddlApoyo.Enabled = True
            Else
                Call wf_habilitarControlesLoad(False)
                ddlApoyo.Enabled = False
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub btnAdicionarCco_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAdicionarCco.Click
        Me.tabla_Cco.Visible = True
        Me.tabla_load.Visible = False
        'Me.ddlTipoActividad.SelectedValue = 2
        Me.ddlTipoActividad.Enabled = False
        'Me.ddlprogpresupuestal_cco.SelectedValue = 1 'GESTION OPERATIVA
        Me.ddlCategoria_cco.SelectedValue = 0
        Me.ddlprogpresupuestal_cco.SelectedValue = 0
        Me.ddlprogpresupuestal_cco.Enabled = False
        Call wf_habilitarControlesPaso1(False)
    End Sub

    Protected Sub btnguardar_cco_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnguardar_cco.Click
        Try
            'Dim msj, rpta As String
            'rpta = ""
            'msj = "¿Esta Seguro Que Desea Registrar los siguientes datos? \n \n"
            'msj = msj & "Tipo de Actividad : " & Me.ddlTipoActividad.SelectedItem.ToString & "\n"
            'msj = msj & "Centro de Costos : " & Me.txtdescripcion_cco.Text.ToString.ToUpper & "\n"
            'msj = msj & "Programa Presupuestal : " & Me.ddlprogpresupuestal_cco.SelectedItem.ToString & "\n"
            'msj = msj & "Responsable : " & Me.ddlresponsable_cco.SelectedItem.ToString
            'Response.Write("<script>return confirm('" & msj & "')</script>")
            Dim obj As New clsPlanOperativoAnual
            Dim dt As New Data.DataTable
            Dim codigo_poa, codigo_raiz As Integer
            codigo_poa = Request.QueryString("codigo_poa")
            dt = obj.ListaPoas(codigo_poa)
            codigo_raiz = dt.Rows(0).Item("codigo_cco")
            dt.Clear()

            'If ddlTipoActividad_cco.SelectedValue = 0 Then
            '    Response.Write("Debe Seleccionar un Tipo de Actividad")
            '    Return
            'End If



            Dim tipoActividad As Integer = ddlTipoActividad_cco.SelectedValue
            ddlTipoActividad.Enabled = False
            ddlTipoActividad_cco.Enabled = False

            dt = obj.CrearCostoTemporalPOA(Me.txtdescripcion_cco.Text.ToUpper, codigo_poa, tipoActividad, codigo_raiz, Me.ddlprogpresupuestal_cco.SelectedValue, _
                                           Me.ddlresponsable_cco.SelectedValue, Request.QueryString("id"), ddlCategoria_cco.SelectedValue)

            'Me.ddlTipoActividad.SelectedValue = 2
            Me.ddlTipoActividad.SelectedValue = tipoActividad

            'CargaCecosxPOA(codigo_poa, 2, dt.Rows(0).Item("codigo_cco_generado"))
            CargaCecosxPOA(codigo_poa, tipoActividad, dt.Rows(0).Item("codigo_cco_generado"))

            'Me.ddlTipoActividad.Enabled = True
            Me.ddlTipoActividad.Enabled = False
            Me.ddlTipoActividad_SelectedIndexChanged(sender, e)

            Me.ddlCeco.SelectedValue = dt.Rows(0).Item("codigo_cco_generado")
            Me.ddlProgPresupuestal.SelectedValue = Me.ddlprogpresupuestal_cco.SelectedValue
            Me.ddlResponsable.SelectedValue = Me.ddlresponsable_cco.SelectedValue
            Me.ddlCategoria.SelectedValue = Me.ddlCategoria_cco.SelectedValue
            Me.ddlCeco_SelectedIndexChanged(sender, e)

            Me.tabla_Cco.Visible = False
            Me.txtdescripcion_cco.Text = ""
            'Me.ddlprogpresupuestal_cco.SelectedValue = 0
            Me.ddlresponsable_cco.SelectedValue = 0

            'ddlCategoria.Enabled = False
            'ddlCategoria_cco.Enabled = False

            Me.tabla_load.Visible = True
            Call wf_habilitarControlesPaso1(True)
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try

    End Sub

    Protected Sub btncancelar_cco_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btncancelar_cco.Click
        Me.tabla_Cco.Visible = False
        Me.ddlprogpresupuestal_cco.SelectedValue = 0
        Me.ddlresponsable_cco.SelectedValue = 0
        Me.txtdescripcion_cco.Text = ""
        Me.tabla_load.Visible = True
        Me.ddlTipoActividad.Enabled = True
        'Me.ddlTipoActividad_SelectedIndexChanged(sender, e)
        Call wf_habilitarControlesPaso1(True)
    End Sub

    Protected Sub cmdGuardarPaso1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdGuardarPaso1.Click
        Try
            Me.lblmensajeDuplicidad.Text = ""
            Me.aviso.Attributes.Clear()
            If Me.btnguardar_cco.Visible = False Then

                Dim obj As New clsPlanOperativoAnual
                Dim codigo_acp As String
                Dim fecha_inicio, fecha_fin As String
                Dim abreviatura, descripcion_acp As String

                If Me.txtEgresos.Text = "" Then
                    Me.txtEgresos.Text = "0.00"
                End If

                If Me.txtIngresos.Text = "" Then
                    Me.txtIngresos.Text = "0.00"
                End If

                If Me.txtUtilidad.Text = "" Then
                    Me.txtUtilidad.Text = "0.00"
                End If
                '''''''''
                fecha_inicio = DateSerial(Me.hddescripcion_ejp.Value, ddlMesInicio.SelectedValue, 1).ToString("dd/MM/yyyy")
                fecha_fin = DateSerial(Me.hddescripcion_ejp.Value, ddlMesFin.SelectedValue + 1, 0).ToString("dd/MM/yyyy")
                lblfecini.Text = fecha_inicio
                lblfecfin.Text = fecha_fin

                'Response.Write("ddlCategoria.SelectedValue")
                'If ddlCategoria.SelectedValue = 0 Then
                '    Me.lblmensajeDuplicidad.Text = "Debe seleccionar una Categoría"
                '    Me.aviso.Attributes.Add("class", "mensajeError")
                '    Return
                'End If

                If Me.hdcodigoacp.Value = 0 Then
                    'Insertar 'datos
                    abreviatura = Me.txtabreviatura.Text
                    descripcion_acp = Me.txtdescripcion.Text
                    codigo_acp = obj.AtualizarActividadPoa(abreviatura, descripcion_acp, Me.txtEgresos.Text, Me.txtIngresos.Text, Me.txtUtilidad.Text, _
                                                           Request.QueryString("id"), Me.ddlTipoActividad.SelectedValue, Me.ddlResponsable.SelectedValue, _
                                                           Me.ddlApoyo.SelectedValue, Me.ddlCeco.SelectedValue, Me.ddlProgPresupuestal.SelectedValue, _
                                                           Request.QueryString("codigo_poa"), fecha_inicio, fecha_fin, Me.hdcodigo_ejp.Value, Me.hdcodigoacp.Value, _
                                                           ddlCategoria.SelectedValue)
                    If codigo_acp > 0 Then
                        Me.hdcodigoacp.Value = codigo_acp
                        'Response.Write("Registro Correcto Actividad")
                        Dim Tipo_actividad As String

                        Tipo_actividad = Left(Me.ddlTipoActividad.SelectedItem.ToString, 1) & Right(Me.ddlTipoActividad.SelectedItem.ToString.ToLower, Me.ddlTipoActividad.SelectedItem.ToString.Length - 1)
                        Me.lbltituloPaso2.Text = "Alineamiento de " & Tipo_actividad
                        Me.lbltituloPaso3.Text = "Objetivos e Indicadores del " & Tipo_actividad
                        Me.lbltituloPaso4.Text = "Actividades de " & Tipo_actividad
                        Me.lblNombreActividad.Text = Me.txtdescripcion.Text
                        Me.lbltipoactividadPaso2.Text = Me.ddlTipoActividad.SelectedItem.ToString
                        Me.lbltipoactividadPaso3.Text = Me.ddlTipoActividad.SelectedItem.ToString
                        Me.lblTipoactividadPaso4.Text = Me.ddlTipoActividad.SelectedItem.ToString

                        Me.lblactividadPaso2.Text = Me.txtdescripcion.Text
                        Me.lblactividadPaso3.Text = Me.txtdescripcion.Text
                        Me.lblactividadPaso4.Text = Me.txtdescripcion.Text
                        Me.tab2.Enabled = True
                    Else
                        If codigo_acp = "-1" Then
                            Me.lblmensajeDuplicidad.Text = "No se Puede Registrar, El Centro de Costo : " & Me.ddlCeco.SelectedItem.ToString & " ya se Encuentra Registrado para el Ejercicio Presupuestal : " & Me.hddescripcion_ejp.Value
                            Me.aviso.Attributes.Add("class", "mensajeError")
                            Exit Sub
                        ElseIf codigo_acp = "-2" Then
                            Me.lblmensajeDuplicidad.Text = "Egresos Exceden a Presupuesto Disponible de Plan Operativo Anual."
                            Me.aviso.Attributes.Add("class", "mensajeError")
                            Exit Sub
                        ElseIf codigo_acp = "-6" Then
                            Me.lblmensajeDuplicidad.Text = "Ejercicio Presupuestal En Etapa de Cierre, No se Permite Registro de Programas/Proyectos"
                            Me.aviso.Attributes.Add("class", "mensajeError")
                            Exit Sub
                        Else
                            Response.Write("-")
                        End If

                    End If
                Else
                    'Modificar
                    abreviatura = Me.txtabreviatura.Text
                    descripcion_acp = Me.txtdescripcion.Text
                    'Response.Write(obj.POA_VerificarCategoriaActividades(hdcodigoacp.Value, ddlCategoria.SelectedValue))

                    Select Case obj.POA_VerificarCategoriaActividades(hdcodigoacp.Value, ddlCategoria.SelectedValue)
                        Case "1"
                            Response.Write("<script>alert('Categoria no se puede Modificar debido a que Actividades tienen asignado Items en el Presupuesto')</script>")
                            Return
                        Case "2"
                            'Response.Write("No cambia de Categoria")
                        Case Else ''"0"
                            'Actualizar Categoria en CentroCosto
                            'obj.POA_ActualizarCategoriaCeCo(Me.hdcodigoacp.Value, ddlCategoria.SelectedValue)
                    End Select

                    codigo_acp = obj.AtualizarActividadPoa(abreviatura, descripcion_acp, Me.txtEgresos.Text, Me.txtIngresos.Text, Me.txtUtilidad.Text, _
                                                           Request.QueryString("id"), Me.ddlTipoActividad.SelectedValue, Me.ddlResponsable.SelectedValue, _
                                                           Me.ddlApoyo.SelectedValue, Me.ddlCeco.SelectedValue, Me.ddlProgPresupuestal.SelectedValue, _
                                                           Request.QueryString("codigo_poa"), fecha_inicio, fecha_fin, Me.hdcodigo_ejp.Value, Me.hdcodigoacp.Value, _
                                                           ddlCategoria.SelectedValue)
                    If codigo_acp = 1 Then
                        'Me.hdcodigoacp.Value = codigo_acp
                        'Response.Write("Actualizacion Correcta Actividad")
                        Dim Tipo_actividad As String
                        Tipo_actividad = Left(Me.ddlTipoActividad.SelectedItem.ToString, 1) & Right(Me.ddlTipoActividad.SelectedItem.ToString.ToLower, Me.ddlTipoActividad.SelectedItem.ToString.Length - 1)
                        Me.lbltituloPaso2.Text = "Alineamiento de " & Tipo_actividad
                        Me.lbltituloPaso3.Text = "Objetivos e Indicadores del " & Tipo_actividad
                        Me.lbltituloPaso4.Text = "Actividades de " & Tipo_actividad
                        Me.lbltipoactividadPaso2.Text = Me.ddlTipoActividad.SelectedItem.ToString
                        Me.lbltipoactividadPaso3.Text = Me.ddlTipoActividad.SelectedItem.ToString
                        Me.lblTipoactividadPaso4.Text = Me.ddlTipoActividad.SelectedItem.ToString

                        Me.lblactividadPaso2.Text = Me.txtdescripcion.Text
                        Me.lblactividadPaso3.Text = Me.txtdescripcion.Text
                        Me.lblactividadPaso4.Text = Me.txtdescripcion.Text
                        Me.tab2.Enabled = True
                    Else
                        If codigo_acp = "-1" Then
                            Me.lblmensajeDuplicidad.Text = "No se Puede Registrar, El Centro de Costo : " & Me.ddlCeco.SelectedItem.ToString & " ya se Encuentra Registrado para el Ejercicio Presupuestal : " & Me.hddescripcion_ejp.Value
                            Me.aviso.Attributes.Add("class", "mensajeError")
                            Exit Sub
                        ElseIf codigo_acp = "-2" Then
                            Me.lblmensajeDuplicidad.Text = "Egresos Exceden a Presupuesto Disponible de Plan Operativo Anual."
                            Me.aviso.Attributes.Add("class", "mensajeError")
                            Exit Sub
                        ElseIf codigo_acp = "-3" Then
                            Me.lblmensajeDuplicidad.Text = "No se Puede Cambiar Centro de Costo y Fechas de Inicio, Actividad Cuenta Con Presupuesto Asignado."
                            Me.aviso.Attributes.Add("class", "mensajeError")
                            Exit Sub
                        ElseIf codigo_acp = "-4" Then
                            Me.lblmensajeDuplicidad.Text = "No se Puede Editar, Monto de Egresos es Menor al Monto Presupuestado en PASO N°4"
                            Me.aviso.Attributes.Add("class", "mensajeError")
                            Exit Sub
                        ElseIf codigo_acp = "-5" Then
                            Me.lblmensajeDuplicidad.Text = "No se Puede Editar, Actividad No Fue creada en Etapa de Ejecucion"
                            Me.aviso.Attributes.Add("class", "mensajeError")
                            Exit Sub
                        ElseIf codigo_acp = "-6" Then
                            Me.lblmensajeDuplicidad.Text = "Ejercicio Presupuestal En Etapa de Cierre, No se Permite Edición de Programas/Proyectos"
                            Me.aviso.Attributes.Add("class", "mensajeError")
                            Exit Sub
                        Else
                            Response.Write("-")
                        End If
                    End If
                End If
                Me.tab1.CssClass = "tab_inactivo"
                Me.tab2.CssClass = "tab_activo"
                Me.paso1.Visible = False
                Me.paso2.Visible = True
                Me.paso3.Visible = False
                Me.paso4.Visible = False
            Else
                Me.lblmensajeDuplicidad.Text = "No Puede Continuar, Registro de Nuevo Centro de costo Activo "
                Me.aviso.Attributes.Add("class", "mensajeError")
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub cmdCancelarPaso1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdCancelarPaso1.Click
        If Request.QueryString("tipo_acc") = "PL" Then
            Response.Redirect("FrmListaEvaluarAlineamiento.aspx?id=" & Request.QueryString("id") & "&ctf=" & Request.QueryString("ctf") & "&cb1=" & Request.QueryString("cb1") & "&cb2=" & Request.QueryString("cb2") & "&cb3=" & Request.QueryString("cb3") & "&cb4=" & Request.QueryString("cb4") & "&accion=C" & "&codigo_acp=" & Request.QueryString("codigo_acp"))
        Else
            Response.Redirect("FrmListaActividadesPOA.aspx?id=" & Request.QueryString("id") & "&ctf=" & Request.QueryString("ctf") & "&cb1=" & Request.QueryString("cb1") & "&cb2=" & Request.QueryString("cb2") & "&cb3=" & Request.QueryString("cb3") & "&index_poa=" & Request.QueryString("index_poa") & "&accion=C&index_acp=" & Me.hdcodigoacp.Value)
        End If

    End Sub

    Protected Sub btnregresarp1_top_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnregresarp1_top.Click
        Me.cmdCancelarPaso1_Click(sender, e)
    End Sub

    Protected Sub btnguardarp1_top_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnguardarp1_top.Click
        Me.cmdGuardarPaso1_Click(sender, e)
    End Sub

#End Region

#Region "PANEL N°2"
    Sub CargarInicialArbol()
        Dim obj As New clsPlanOperativoAnual
        Dim dts As New Data.DataTable
        dts = obj.ConsultaPerspectivasxPEI(Me.hdcodigoPei.Value)
        If dts.Rows.Count > 0 Then
            Dim row As Data.DataRow
            For Each row In dts.Rows
                Dim NewNode As TreeNode = New TreeNode(row("Perspectiva").ToString, row("Codigo").ToString)
                NewNode.PopulateOnDemand = True
                NewNode.SelectAction = TreeNodeSelectAction.Select
                Me.ArbolPaso2.Nodes.Add(NewNode)

            Next
        End If
        Me.ArbolPaso2.ExpandAll()
    End Sub


    Protected Sub ArbolPaso2_TreeNodePopulate(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.TreeNodeEventArgs) Handles ArbolPaso2.TreeNodePopulate
        Try
            If e.Node.ChildNodes.Count = 0 Then
                Select Case e.Node.Depth
                    Case 0
                        ConsultaObjetivosxPers(e.Node)
                    Case 1
                        ConsultaIndicadoresxObj(e.Node)
                    Case 2
                        'AgregarIndicador(e.Node)
                End Select
            End If

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Sub ConsultaObjetivosxPers(ByVal node As TreeNode)
        Try
            Dim obj As New clsPlanOperativoAnual
            Dim dts As New Data.DataTable

            'Cargamos la data de las categorias
            dts = obj.ConsultaObjetivosxPers(node.Value, Me.hddescripcion_ejp.Value)

            If dts.Rows.Count > 0 Then
                Dim row As Data.DataRow
                For Each row In dts.Rows
                    Dim NewNode As TreeNode = New TreeNode(row("descripcion").ToString, row("Codigo").ToString)
                    NewNode.PopulateOnDemand = True
                    NewNode.SelectAction = TreeNodeSelectAction.Select
                    node.ChildNodes.Add(NewNode)
                Next
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try

    End Sub

    Sub ConsultaIndicadoresxObj(ByVal node As TreeNode)
        Try
            Dim obj As New clsPlanOperativoAnual
            Dim dts As New Data.DataTable
            Dim ind As New Data.DataTable
            'Cargamos la data de las categorias
            dts = obj.ConsultaIndicadoresxObj(node.Value)
            ind = obj.IndicadoresAlineados(Me.hdcodigoacp.Value)

            If dts.Rows.Count > 0 Then
                Dim row As Data.DataRow
                For Each row In dts.Rows
                    Dim NewNode As TreeNode = New TreeNode(row("descripcion").ToString, row("Codigo").ToString)
                    NewNode.PopulateOnDemand = False
                    NewNode.ShowCheckBox = True
                    NewNode.SelectAction = TreeNodeSelectAction.Select
                    If ind.Rows.Count > 0 Then
                        Dim row1 As Data.DataRow
                        For Each row1 In ind.Rows
                            If row1("codigo_ind").ToString = row("codigo").ToString Then
                                NewNode.Checked = True
                                'NewNode.Selected = True
                                Me.ListBox1.Items.Add(row1("codigo_ind").ToString + "/" + row1("codigo_ali").ToString)
                            End If
                        Next
                    End If
                    node.ChildNodes.Add(NewNode)
                Next
            End If

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    'Check Marcados
    Public Function RecorreArbol() As ArrayList
        Dim nro_subnodos, nro_nodos As Integer
        Dim i, j, k, nro_pers As Integer
        Dim Checkeados As New ArrayList

        nro_pers = Me.ArbolPaso2.Nodes.Count
        If nro_pers > 0 Then
            'Recorre Perspectivas
            For k = 0 To nro_pers - 1
                nro_nodos = Me.ArbolPaso2.Nodes.Item(k).ChildNodes.Count
                If nro_nodos > 0 Then
                    'Recorre Objetivos
                    For j = 0 To nro_nodos - 1
                        nro_subnodos = Me.ArbolPaso2.Nodes.Item(k).ChildNodes.Item(j).ChildNodes.Count
                        If nro_subnodos > 0 Then
                            'Recorre Indicadores
                            For i = 0 To nro_subnodos - 1
                                'verifica si Tiene Check
                                If Me.ArbolPaso2.Nodes.Item(k).ChildNodes.Item(j).ChildNodes.Item(i).Checked Then
                                    Checkeados.Add(ArbolPaso2.Nodes.Item(k).ChildNodes.Item(j).ChildNodes.Item(i).Value)
                                End If
                            Next
                        End If
                    Next
                End If
            Next
        End If
        Return Checkeados
    End Function

    Protected Sub cmdGuardarPaso2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdGuardarPaso2.Click
        Dim checkeados As New ArrayList
        Dim obj As New clsPlanOperativoAnual
        Dim i, j, k As Integer
        Try
            checkeados = RecorreArbol()
            If checkeados.Count > 0 Then

                ' QUEDAN LOS QUE SE VAN A ELIMINAR - ACTUALIZAR A 0
                If Me.ListBox1.Items.Count > 0 Then
                    For i = 0 To Me.ListBox1.Items.Count - 1
                        For j = 0 To checkeados.Count - 1
                            If checkeados.Item(j).ToString = Mid(Me.ListBox1.Items(i).Value, 1, InStr(Me.ListBox1.Items(i).Text, "/") - 1) Then
                                Me.ListBox1.Items(i).Text = "-"
                                checkeados.Item(j) = "-"
                                Exit For
                            End If
                        Next
                    Next
                End If

                'ACTUALIZAMOS ESTADO A 0
                Dim nro_act As Integer = 0

                For k = 0 To Me.ListBox1.Items.Count - 1
                    If Me.ListBox1.Items(k).Text <> "-" Then
                        'Response.Write(Mid(Me.ListBox1.Items(k).Text, InStr(Me.ListBox1.Items(k).Text, "/") + 1, Me.ListBox1.Items(k).Text.Length))
                        obj.EliminarAlineamiento(Request.QueryString("id"), Mid(Me.ListBox1.Items(k).Text, InStr(Me.ListBox1.Items(k).Text, "/") + 1, Me.ListBox1.Items(k).Text.Length))
                        nro_act = nro_act + 1
                    End If
                Next

                'INSERTAMOS 
                Dim nro_ins As Integer = 0
                For i = 0 To checkeados.Count - 1
                    If checkeados.Item(i).ToString <> "-" Then
                        '    Me.ListBox2.Items.Add(checkeados.Item(i))
                        obj.InsertarAlineamiento(Request.QueryString("id"), checkeados.Item(i), Me.hdcodigoacp.Value)
                        nro_ins = nro_ins + 1
                    End If
                Next

                'SI NO MODIFICO Y/O INSERTO NO CARGA EL ARBOL
                Me.ArbolPaso2.Nodes.Clear()
                Me.ListBox1.Items.Clear()
                CargarInicialArbol()


                Me.paso1.Visible = False
                Me.paso2.Visible = False
                'Me.paso3.Visible = True
                Me.paso4.Visible = True
                Me.tab2.CssClass = "tab_inactivo"
                Me.tab4.CssClass = "tab_activo"
                Me.tab4.Enabled = True
                Me.lblactividadPaso3.Text = Me.txtdescripcion.Text

                ''''''comenteeeeee
                Call wf_CargarListaObjeticosIndicadores(Me.hdcodigoacp.Value)
                Me.lblactividadPaso4.Text = Me.txtdescripcion.Text
                Call wf_CargarDetalleActividad(hdcodigoacp.Value)
            Else
                Response.Write("<script>alert('Debe Seleccionar al menos un objetivo Estratégico.')</script>")
                Exit Sub
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub cmdCancelarPaso2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdCancelarPaso2.Click

        Dim chekeados As New ArrayList
        chekeados = RecorreArbol()
        If chekeados.Count > 0 Then
            Me.cmdGuardarPaso2_Click(sender, e)
            Me.paso1.Visible = True
            Me.paso2.Visible = False
            Me.paso3.Visible = False
            Me.paso4.Visible = False
            Me.tab1.CssClass = "tab_activo"
            Me.tab2.CssClass = "tab_inactivo"
            Me.tab3.CssClass = "tab_inactivo"
            Me.tab4.CssClass = "tab_inactivo"
        Else
            Response.Write("<script>alert('Debe Seleccionar al menos un Indicador')</script>")
        End If
    End Sub

    Protected Sub btnregresarp2_top_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnregresarp2_top.Click
        Me.cmdCancelarPaso2_Click(sender, e)
    End Sub

    Protected Sub btnguardarp2_top_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnguardarp2_top.Click
        Me.cmdGuardarPaso2_Click(sender, e)
    End Sub
#End Region

#Region "PANEL N°3"
    Protected Sub btnNuevo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNuevo.Click
        Me.tabla_obj.Visible = True
        Call wf_habilitaControles(True, 1)
        Call wf_habilitarMetas()
        hdAccion.Value = "NO"
        Me.lblmensaje_p3.Text = ""
        lblmensaje_p3.Visible = False
        Me.p3_aviso.Attributes.Clear()

        txt_p3_Objetivo.Text = ""
        txt_p3_Indicador.Text = ""

        hd_codigo_pobj.Value = 0
        hd_codigo_pind.Value = 0

        txt_meta1.Text = 0
        txt_meta2.Text = 0
        txt_meta3.Text = 0
        txt_meta4.Text = 0
        btnNuevoIndicador.Visible = False

        Call wf_CargarListaObjeticosIndicadores(Me.hdcodigoacp.Value)
        txt_p3_Objetivo.Focus()
    End Sub

    Sub wf_nuevoObjetivo()

    End Sub

    Sub wf_habilitarMetas()
        Dim obj As New clsPlanOperativoAnual
        Dim dtt As New Data.DataTable
        dtt = obj.POA_listar_mesesActividad(hdcodigoacp.Value)
        If (dtt.Rows.Count) > 0 Then
            Dim mesInicio As Integer = dtt.Rows(0).Item(0)
            Dim mesFin As Integer = dtt.Rows(0).Item(1)

            'Response.Write("<script>alert('Mes Inicio: " & mesInicio & " Mes Fin: " & mesFin & "')</script>")
            txt_meta1.Enabled = False
            txt_meta2.Enabled = False
            txt_meta3.Enabled = False
            txt_meta4.Enabled = False

            Dim mesI1 As Boolean = (mesInicio = 1) Or (mesInicio = 2) Or (mesInicio = 3)
            Dim mesI2 As Boolean = (mesInicio = 4) Or (mesInicio = 5) Or (mesInicio = 6)
            Dim mesI3 As Boolean = (mesInicio = 7) Or (mesInicio = 8) Or (mesInicio = 9)
            Dim mesI4 As Boolean = (mesInicio = 10) Or (mesInicio = 11) Or (mesInicio = 12)

            Dim mesF1 As Boolean = (mesFin = 1) Or (mesFin = 2) Or (mesFin = 3)
            Dim mesF2 As Boolean = (mesFin = 4) Or (mesFin = 5) Or (mesFin = 6)
            Dim mesF3 As Boolean = (mesFin = 7) Or (mesFin = 8) Or (mesFin = 9)
            Dim mesF4 As Boolean = (mesFin = 10) Or (mesFin = 11) Or (mesFin = 12)

            If mesI1 = True Then txt_meta1.Enabled = True
            If mesI2 = True Then txt_meta2.Enabled = True
            If mesI3 = True Then txt_meta3.Enabled = True
            If mesI4 = True Then txt_meta4.Enabled = True

            If mesF1 = True Then txt_meta1.Enabled = True
            If mesF2 = True Then txt_meta2.Enabled = True
            If mesF3 = True Then txt_meta3.Enabled = True
            If mesF4 = True Then txt_meta4.Enabled = True

            Select Case True
                Case (mesI1 = True) And (mesF3 = True)
                    txt_meta1.Enabled = True
                    txt_meta2.Enabled = True
                    txt_meta3.Enabled = True

                Case (mesI1 = True) And (mesF4 = True)
                    txt_meta1.Enabled = True
                    txt_meta2.Enabled = True
                    txt_meta3.Enabled = True
                    txt_meta4.Enabled = True

                Case (mesI2 = True) And (mesF4 = True)
                    txt_meta2.Enabled = True
                    txt_meta3.Enabled = True
                    txt_meta4.Enabled = True
            End Select
        End If
    End Sub

    Sub mb(ByVal as_text As String)
        Response.Write("<script>alert('" & as_text & "')</script>")
        Call wf_CargarListaObjeticosIndicadores(Me.hdcodigoacp.Value)
    End Sub

    'Function wf_valida_paso3() As Boolean
    '    If txt_meta1.Text > 100 Then
    '        txt_meta1.Focus()
    '        Return False
    '    End If

    '    If txt_meta2.Text > 100 Then
    '        txt_meta2.Focus()
    '        Return False
    '    End If

    '    If txt_meta3.Text > 100 Then
    '        txt_meta3.Focus()
    '        Return False
    '    End If

    '    If txt_meta4.Text > 100 Then
    '        txt_meta4.Focus()
    '        Return False
    '    End If

    '    Return True
    'End Function

    Function wf_valida_TopesMetas() As Boolean
        If txt_meta1.Text = "" Then
            txt_meta1.Text = 0
        End If
        If txt_meta2.Text = "" Then
            txt_meta2.Text = 0
        End If
        If txt_meta3.Text = "" Then
            txt_meta3.Text = 0
        End If
        If txt_meta4.Text = "" Then
            txt_meta4.Text = 0
        End If

        Dim meta1 As Double = CDbl(txt_meta1.Text)
        Dim meta2 As Double = CDbl(txt_meta2.Text)
        Dim meta3 As Double = CDbl(txt_meta3.Text)
        Dim meta4 As Double = CDbl(txt_meta4.Text)

        Me.p3_aviso.Attributes.Clear()
        lblmensaje_p3.Visible = True

        If txt_meta1.Enabled = True And txt_meta2.Enabled = True Then
            If meta1 > meta2 Then
                Me.p3_aviso.Attributes.Add("class", "mensajeError")
                Me.lblmensaje_p3.Text = "Meta de Trimestre1 NO puede ser Mayor a Meta del Trimestre2"
                Response.Write("<script>alert('" & lblmensaje_p3.Text & "')</script>")
                Return False
            End If
        End If

        If txt_meta2.Enabled = True Then
            If meta1 > meta2 Then
                Me.p3_aviso.Attributes.Add("class", "mensajeError")
                Me.lblmensaje_p3.Text = "Meta de Trimestre2 NO puede ser inferior a Meta del Trimestre1 "
                Response.Write("<script>alert('" & lblmensaje_p3.Text & "')</script>")
                Return False
            End If
        End If

        If txt_meta3.Enabled = True Then
            If meta2 > meta3 Then
                Me.p3_aviso.Attributes.Add("class", "mensajeError")
                Me.lblmensaje_p3.Text = "Meta de Trimestre3 NO puede ser inferior a Meta del Trimestre2 "
                Response.Write("<script>alert('" & lblmensaje_p3.Text & "')</script>")
                Return False
            End If
        End If

        If txt_meta4.Enabled = True Then
            If meta3 > meta4 Then
                Me.p3_aviso.Attributes.Add("class", "mensajeError")
                Me.lblmensaje_p3.Text = "Meta de Trimestre4 NO puede ser inferior a Meta del Trimestre3 "
                Response.Write("<script>alert('" & lblmensaje_p3.Text & "')</script>")
                Return False
            End If
        End If

        Return True
    End Function

    Protected Sub btn_p3_AgregarObjetivo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_p3_AgregarObjetivo.Click
        Try
            If wf_valida_TopesMetas() = False Then
                Return
            End If

            If txt_p3_Objetivo.Text.Trim = "" Then
                Response.Write("<script>alert('Se Debe Ingresar descripción del Objetivo')</script>")
                Me.lblmensaje_p3.Text = "Se Debe Ingresar descripción del Objetivo"
                Me.p3_aviso.Attributes.Add("class", "mensajeError")
                txt_p3_Objetivo.Focus()
                Return
            End If

            If txt_p3_Indicador.Text.Trim = "" Then
                Response.Write("<script>alert('Se Debe Ingresar descripción del Indicador')</script>")
                Me.lblmensaje_p3.Text = "Se Debe Ingresar descripción del Indicador"
                Me.p3_aviso.Attributes.Add("class", "mensajeError")

                txt_p3_Objetivo.Focus()
                Return
            End If

            'If wf_valida_paso3() = False Then
            '    Me.p3_aviso.Attributes.Clear()
            '    lblmensaje_p3.Visible = True

            '    Me.p3_aviso.Attributes.Add("class", "mensajeError")
            '    Me.lblmensaje_p3.Text = "Meta debe ser menor o igual al 100%"
            '    Response.Write("<script>alert('Meta debe ser menor o igual al 100%')</script>")
            '    Return
            'End If

            'Insertar Objetivos
            Dim obj As New clsPlanOperativoAnual
            Dim dtt As New Data.DataTable
            'dtt = obj.InsertarObjetivosPOA(txt_p3_codigo_pobj.Text, txt_p3_Objetivo.Text, Request.QueryString("id"), 1, Me.hdcodigoacp.Value)
            dtt = obj.InsertarObjetivosPOA(hd_codigo_pobj.Value, txt_p3_Objetivo.Text, Request.QueryString("id"), 1, Me.hdcodigoacp.Value)
            hd_codigo_pobj.Value = dtt.Rows(0).Item(0)

            Dim li_codigo_pind As Integer = hd_codigo_pind.Value
            Dim dttIndicador As New Data.DataTable
            dttIndicador = obj.POA_InsertarIndicador(li_codigo_pind, txt_p3_Indicador.Text, Request.QueryString("id"), hd_codigo_pobj.Value)

            If (hdcodigo_meta1.Value = 0 And txt_meta1.Text = 0) Then hdcodigo_meta1.Value = 0
            If (hdcodigo_meta2.Value = 0 And txt_meta2.Text = 0) Then hdcodigo_meta2.Value = 0
            If (hdcodigo_meta3.Value = 0 And txt_meta3.Text = 0) Then hdcodigo_meta3.Value = 0
            If (hdcodigo_meta4.Value = 0 And txt_meta4.Text = 0) Then hdcodigo_meta4.Value = 0

            If txt_meta1.Enabled = True And hdcodigo_meta1.Value <> "" Then
                ' Response.Write("<script>alert('Cod Meta: " & hdcodigo_meta1.Value & " Meta: " & txt_meta1.Text & " Tri: : " & "T1" & " ID: " & Request.QueryString("id") & " COdIndicador: " & dttIndicador.Rows(0).Item(0) & "')</script>")
                obj.InsertarMetasObjetivosPOA(hdcodigo_meta1.Value, txt_meta1.Text, IIf(txt_meta1.Text = 0, "", "T1"), Request.QueryString("id"), dttIndicador.Rows(0).Item(0))
            End If
            If txt_meta2.Enabled = True And hdcodigo_meta2.Value <> "" Then
                obj.InsertarMetasObjetivosPOA(hdcodigo_meta2.Value, txt_meta2.Text, IIf(txt_meta2.Text = 0, "", "T2"), Request.QueryString("id"), dttIndicador.Rows(0).Item(0))
            End If

            If txt_meta3.Enabled = True And hdcodigo_meta3.Value <> "" Then
                obj.InsertarMetasObjetivosPOA(hdcodigo_meta3.Value, txt_meta3.Text, IIf(txt_meta3.Text = 0, "", "T3"), Request.QueryString("id"), dttIndicador.Rows(0).Item(0))
            End If
            If txt_meta4.Enabled = True And hdcodigo_meta4.Value <> "" Then
                obj.InsertarMetasObjetivosPOA(hdcodigo_meta4.Value, txt_meta4.Text, IIf(txt_meta4.Text = 0, "", "T4"), Request.QueryString("id"), dttIndicador.Rows(0).Item(0))
            End If

            Call wf_CargarListaObjeticosIndicadores(Me.hdcodigoacp.Value)
            txt_p3_Objetivo.ReadOnly = True
            btn_p3_AgregarObjetivo.Visible = False
            btn_p3_CancelarObjetivo.Visible = False
            txt_p3_Indicador.Text = ""
            txt_meta1.Text = 0
            txt_meta2.Text = 0
            txt_meta3.Text = 0
            txt_meta4.Text = 0

            btn_p3_AgregarIndicador.Visible = True
            btn_p3_CancelarIndicador.Visible = True

            If hdAccion.Value = "NO" Then
                Response.Write("<script>alert('El Objetivo e Indicador se Registraron Correctamente')</script>")
                Me.lblmensaje_p3.Text = "El Objetivo e Indicador se Registraron Correctamente"
                Me.p3_aviso.Attributes.Add("class", "mensajeExito")
            ElseIf hdAccion.Value = "MO" Then
                Response.Write("<script>alert('El Objetivo e Indicador se Modificaron Correctamente')</script>")
                Me.lblmensaje_p3.Text = "El Objetivo e Indicador se Modificaron Correctamente"
                Me.p3_aviso.Attributes.Add("class", "mensajeExito")
            ElseIf hdAccion.Value = "MI" Then
                Response.Write("<script>alert('El Objetivo e Indicador se Modificaron Correctamente')</script>")
                Me.lblmensaje_p3.Text = "El Objetivo e Indicador se Modificaron Correctamente"
                Me.p3_aviso.Attributes.Add("class", "mensajeExito")
            ElseIf hdAccion.Value = "NI" Then
                Response.Write("<script>alert('El Indicador se Registro Correctamente')</script>")
                Me.lblmensaje_p3.Text = "El Indicador se Registro Correctamente"
                Me.p3_aviso.Attributes.Add("class", "mensajeExito")
            End If

            btnNuevoIndicador.Visible = False
            Me.tabla_obj.Visible = False
            txt_p3_Indicador.Focus()

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try

    End Sub

    Sub wf_ocultarControles(ByVal as_flag As Boolean)
        lbl_objetivo.Visible = as_flag
        txt_p3_Objetivo.Visible = as_flag

        lbl_indicador.Visible = as_flag
        txt_p3_Indicador.Visible = as_flag

        lbl_trimestre1.Visible = as_flag
        lbl_trimestre2.Visible = as_flag
        lbl_trimestre3.Visible = as_flag
        lbl_trimestre4.Visible = as_flag

        txt_meta1.Visible = as_flag
        txt_meta2.Visible = as_flag
        txt_meta3.Visible = as_flag
        txt_meta4.Visible = as_flag
    End Sub

    Sub wf_CargarListaObjeticosIndicadores(ByVal codigo_acp As Integer)
        Try
            Dim obj As New clsPlanOperativoAnual
            Dim dtt As New Data.DataTable
            dgw_p3_Objetivos.DataSource = Nothing

            dtt = obj.POA_ListarObjetivosIndicadores(codigo_acp)
            dgw_p3_Objetivos.DataSource = dtt
            dgw_p3_Objetivos.DataBind()

            dtt.Dispose()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub dgw_p3_Objetivos_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles dgw_p3_Objetivos.RowCommand
        Try
            If (e.CommandName.Equals("Select")) Then 'comprueba que sea el boton de seleccion   
                Dim obj As New clsPlanOperativoAnual

                Dim seleccion As GridViewRow
                seleccion = DirectCast(e.CommandSource, GridView).Rows(e.CommandArgument)
                'txt_p3_codigo_pobj.Text = Convert.ToInt32(dgw_p3_Objetivos.DataKeys(seleccion.RowIndex).Values("codigo_pobj").ToString)
                hd_codigo_pobj.Value = Convert.ToInt32(dgw_p3_Objetivos.DataKeys(seleccion.RowIndex).Values("codigo_pobj").ToString)
                hd_codigo_pind.Value = dgw_p3_Objetivos.DataKeys(seleccion.RowIndex).Values("codigo_pind").ToString
                If hd_codigo_pind.Value = "" Then
                    hd_codigo_pind.Value = 0
                End If

                'Validar que se encuentren datos en tabla MetaIndicador_POA
                If obj.POA_verificarMetaIndicador(hd_codigo_pobj.Value) = "1" Then
                    Response.Write("<script>alert('Debe Eliminar Indicador/Objetivo')</script>")
                    Me.tabla_obj.Visible = False
                    Return
                End If

                hdcodigo_meta1.Value = dgw_p3_Objetivos.DataKeys(seleccion.RowIndex).Values("codmeta1").ToString
                hdcodigo_meta2.Value = dgw_p3_Objetivos.DataKeys(seleccion.RowIndex).Values("codmeta2").ToString
                hdcodigo_meta3.Value = dgw_p3_Objetivos.DataKeys(seleccion.RowIndex).Values("codmeta3").ToString
                hdcodigo_meta4.Value = dgw_p3_Objetivos.DataKeys(seleccion.RowIndex).Values("codmeta4").ToString

                ''Consultar Nombre de Objetivo y Tiempos

                Dim dtt As New Data.DataTable

                'dtt = obj.POA_buscar_objetivo_indicador(hdcodigoacp.Value, txt_p3_codigo_pobj.Text, hd_codigo_pind.Value)
                dtt = obj.POA_buscar_objetivo_indicador(hdcodigoacp.Value, hd_codigo_pobj.Value, hd_codigo_pind.Value)
                If dtt.Rows.Count > 0 Then
                    txt_p3_Objetivo.Text = dtt.Rows(0).Item(0) ' Nombre del Objetivo
                    txt_p3_Indicador.Text = dtt.Rows(0).Item(1)  ' Nombre del Indicador
                    txt_meta1.Text = dtt.Rows(0).Item(2) ' Meta1
                    txt_meta2.Text = dtt.Rows(0).Item(3) ' Meta2
                    txt_meta3.Text = dtt.Rows(0).Item(4) ' Meta3
                    txt_meta4.Text = dtt.Rows(0).Item(5) ' Meta4
                End If

                'Response.Write("<script>alert('Código de Objetivo: " & hd_codigo_pobj.Value & "')</script>")
                txt_p3_Objetivo.Text = dgw_p3_Objetivos.Rows(seleccion.RowIndex).Cells(0).Text
                hdAccion.Value = "MI"
                btnNuevoIndicador.Visible = True

                Me.lblmensaje_p3.Text = ""
                Me.p3_aviso.Attributes.Clear()

                Me.tabla_obj.Visible = True
                Call wf_habilitaControles(True, 1)
                Call wf_habilitarMetas()
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try

    End Sub

    Sub wf_habilitaControles(ByVal as_flag As Boolean, ByVal as_tipo As Integer)
        txt_p3_Objetivo.ReadOnly = False

        lbl_objetivo.Visible = as_flag
        txt_p3_Objetivo.Visible = as_flag
        txt_p3_Objetivo.Enabled = as_flag

        btn_p3_AgregarObjetivo.Visible = as_flag
        btn_p3_CancelarObjetivo.Visible = as_flag

        lbl_indicador.Visible = as_flag
        txt_p3_Indicador.Visible = as_flag
        txt_p3_Indicador.Enabled = as_flag

        btn_p3_AgregarIndicador.Visible = False
        btn_p3_CancelarIndicador.Visible = False

        lbl_trimestre1.Visible = as_flag
        lbl_trimestre2.Visible = as_flag
        lbl_trimestre3.Visible = as_flag
        lbl_trimestre4.Visible = as_flag

        txt_meta1.Visible = as_flag
        txt_meta2.Visible = as_flag
        txt_meta3.Visible = as_flag
        txt_meta4.Visible = as_flag
    End Sub

    Protected Sub dgw_p3_Objetivos_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles dgw_p3_Objetivos.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim row As Data.DataRowView = CType(e.Row.DataItem, Data.DataRowView)
            If LastObjetivo = row("objetivo") Then
                If (dgw_p3_Objetivos.Rows(CurrentRow).Cells(0).RowSpan = 0) Then
                    dgw_p3_Objetivos.Rows(CurrentRow).Cells(0).RowSpan = 2
                    dgw_p3_Objetivos.Rows(CurrentRow).Cells(8).RowSpan = 2  ''Combina Fila Eliminar Objetivo
                Else
                    dgw_p3_Objetivos.Rows(CurrentRow).Cells(0).RowSpan += 1
                    dgw_p3_Objetivos.Rows(CurrentRow).Cells(8).RowSpan += 1  ''Combina Fila Eliminar Objetivo

                End If
                e.Row.Cells(0).Visible = False
                e.Row.Cells(8).Visible = False  ''Combina Fila Eliminar Objetivo

            Else
                e.Row.VerticalAlign = VerticalAlign.Middle
                LastObjetivo = row("objetivo").ToString()
                CurrentRow = e.Row.RowIndex
            End If
        End If
    End Sub


    Protected Sub dgw_p3_Objetivos_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles dgw_p3_Objetivos.RowDeleting
        'Try
        '    'Dim obj As New clsPlanOperativoAnual
        '    'Dim dtt As New Data.DataTable

        '    'dtt = obj.POA_VerificarIndicador(Me.dgw_p3_Objetivos.DataKeys.Item(e.RowIndex()).Values(0))
        '    'If dtt.Rows(0).Item(0).ToString > 0 Then
        '    '    obj.EliminarIndicador(Me.dgw_p3_Objetivos.DataKeys.Item(e.RowIndex()).Values(2), Request.QueryString("id"))
        '    '    Call wf_CargarListaObjeticosIndicadores(Me.hdcodigoacp.Value)
        '    'Else
        '    '    obj.EliminarObjetivo(Me.dgw_p3_Objetivos.DataKeys.Item(e.RowIndex()).Values(0), Request.QueryString("id"))
        '    '    Call wf_CargarListaObjeticosIndicadores(Me.hdcodigoacp.Value)
        '    'End If

        '    'Me.tabla_obj.Visible = False
        '    'btnNuevoIndicador.Visible = False

        'Catch ex As Exception
        '    Response.Write(ex.Message)
        'End Try
    End Sub

    Protected Sub cmdGuardarPaso3_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdGuardarPaso3.Click
        Me.paso1.Visible = False
        Me.paso2.Visible = False
        Me.paso3.Visible = False
        Me.paso4.Visible = True
        Me.tab4.CssClass = "tab_activo"
        Me.tab3.CssClass = "tab_inactivo"
        Me.tab4.Enabled = True
        Me.lblactividadPaso4.Text = Me.txtdescripcion.Text
        Call wf_CargarDetalleActividad(hdcodigoacp.Value)
        'Call wf_btnNuevo_paso4()

        Me.lblmensajeDuplicidad.Text = ""
        Me.aviso.Attributes.Clear()
    End Sub

    Protected Sub cmdCancelarPaso3_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdCancelarPaso3.Click
        Me.paso1.Visible = False
        Me.paso2.Visible = True
        Me.paso3.Visible = False
        Me.paso4.Visible = False
        Me.tab2.CssClass = "tab_activo"
        Me.tab3.CssClass = "tab_inactivo"
        Me.tabla_obj.Visible = False
    End Sub

    Protected Sub cmdCancelarPaso5_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_p3_CancelarIndicador.Click
        Me.p3_aviso.Attributes.Clear()
        lblmensaje_p3.Visible = False
        Call wf_ocultarControles(False)
        btnNuevoIndicador.Visible = False

        Me.tabla_obj.Visible = False
    End Sub

    Protected Sub btn_p3_AgregarIndicador_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_p3_AgregarIndicador.Click
        Try
            If wf_valida_TopesMetas() = False Then
                Return
            End If

            If txt_p3_Objetivo.Text.Trim = "" Then
                Response.Write("<script>alert('Debe Ingresar descripción del Objetivo')</script>")
                Me.lblmensaje_p3.Text = "Debe Ingresar descripción del Objetivo"
                Me.p3_aviso.Attributes.Add("class", "mensajeError")
                txt_p3_Objetivo.Focus()
                Return
            End If

            If txt_p3_Indicador.Text.Trim = "" Then
                Response.Write("<script>alert('Debe Ingresar descripción del Indicador')</script>")
                Me.lblmensaje_p3.Text = "Debe Ingresar descripción del Indicador"
                Me.p3_aviso.Attributes.Add("class", "mensajeError")

                txt_p3_Objetivo.Focus()
                Return
            End If

            'If wf_valida_paso3() = False Then
            '    Me.p3_aviso.Attributes.Clear()
            '    lblmensaje_p3.Visible = True
            '    Response.Write("<script>alert('Meta debe ser menor o igual al 100%')</script>")
            '    Me.p3_aviso.Attributes.Add("class", "mensajeError")
            '    Me.lblmensaje_p3.Text = "Meta debe ser menor o igual al 100%"
            '    Return
            'End If

            ''Insertar Objetivos
            Dim obj As New clsPlanOperativoAnual
            Dim li_codigo_pind As Integer = 0
            Dim dttIndicador As New Data.DataTable
            'dttIndicador = obj.POA_InsertarIndicador(li_codigo_pind, txt_p3_Indicador.Text, Request.QueryString("id"), txt_p3_codigo_pobj.Text)
            dttIndicador = obj.POA_InsertarIndicador(li_codigo_pind, txt_p3_Indicador.Text, Request.QueryString("id"), hd_codigo_pobj.Value)

            If IIf(txt_meta1.Text = 0, "", "T1") <> "" Then obj.InsertarMetasObjetivosPOA(hdcodigo_meta1.Value, txt_meta1.Text, IIf(txt_meta1.Text = 0, "", "T1"), Request.QueryString("id"), dttIndicador.Rows(0).Item(0))
            If IIf(txt_meta2.Text = 0, "", "T2") <> "" Then obj.InsertarMetasObjetivosPOA(hdcodigo_meta2.Value, txt_meta2.Text, IIf(txt_meta2.Text = 0, "", "T2"), Request.QueryString("id"), dttIndicador.Rows(0).Item(0))
            If IIf(txt_meta3.Text = 0, "", "T3") <> "" Then obj.InsertarMetasObjetivosPOA(hdcodigo_meta3.Value, txt_meta3.Text, IIf(txt_meta3.Text = 0, "", "T3"), Request.QueryString("id"), dttIndicador.Rows(0).Item(0))
            If IIf(txt_meta4.Text = 0, "", "T4") <> "" Then obj.InsertarMetasObjetivosPOA(hdcodigo_meta4.Value, txt_meta4.Text, IIf(txt_meta4.Text = 0, "", "T4"), Request.QueryString("id"), dttIndicador.Rows(0).Item(0))

            Call wf_CargarListaObjeticosIndicadores(Me.hdcodigoacp.Value)

            txt_p3_Objetivo.ReadOnly = True
            btn_p3_AgregarObjetivo.Visible = False
            btn_p3_CancelarObjetivo.Visible = False

            txt_p3_Indicador.Text = ""
            txt_meta1.Text = 0
            txt_meta2.Text = 0
            txt_meta3.Text = 0
            txt_meta4.Text = 0

            btn_p3_AgregarIndicador.Visible = True
            btn_p3_CancelarIndicador.Visible = True

            Me.p3_aviso.Attributes.Clear()
            lblmensaje_p3.Visible = False
            btnNuevoIndicador.Visible = False

            Me.tabla_obj.Visible = False
            txt_p3_Indicador.Focus()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub btn_p3_CancelarObjetivo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_p3_CancelarObjetivo.Click
        Me.p3_aviso.Attributes.Clear()
        lblmensaje_p3.Visible = False
        Call wf_ocultarControles(False)
        btnNuevoIndicador.Visible = False
        Me.tabla_obj.Visible = False
    End Sub

    Protected Sub btnregresarp3_top_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnregresarp3_top.Click
        Me.cmdCancelarPaso3_Click(sender, e)
    End Sub

    Protected Sub btnguradarp3_top_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnguradarp3_top.Click
        Me.cmdGuardarPaso3_Click(sender, e)
    End Sub

    Protected Sub btnNuevoIndicador_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNuevoIndicador.Click
        Me.tabla_obj.Visible = True
        txt_p3_Objetivo.ReadOnly = True
        Me.hdAccion.Value = "NI"
        hd_codigo_pind.Value = 0
        txt_p3_Indicador.Text = ""

        txt_meta1.Text = 0
        txt_meta2.Text = 0
        txt_meta3.Text = 0
        txt_meta4.Text = 0

        hdcodigo_meta1.Value = 0
        hdcodigo_meta2.Value = 0
        hdcodigo_meta3.Value = 0
        hdcodigo_meta4.Value = 0
        Call wf_habilitaControles(True, 1)
        Call wf_habilitarMetas()
        lblmensaje_p3.Visible = False
        Me.p3_aviso.Attributes.Clear()
        hd_codigo_pind.Value = 0
        Call wf_CargarListaObjeticosIndicadores(Me.hdcodigoacp.Value)
        txt_p3_Indicador.Focus()
    End Sub

    Protected Sub ibtnEliminaIndicador_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Try
            Dim obj As New clsPlanOperativoAnual
            Dim dtt As New Data.DataTable

            Dim ibtnRechaza As ImageButton
            Dim row As GridViewRow
            ibtnRechaza = sender
            row = ibtnRechaza.NamingContainer

            dtt = obj.POA_VerificarIndicador(Me.dgw_p3_Objetivos.DataKeys.Item(row.RowIndex).Values(0))
            'gvLista.DataKeys.Item(row.RowIndex).Values("codigo_Dma")

            If dtt.Rows(0).Item(0).ToString > 0 Then
                obj.EliminarIndicador(Me.dgw_p3_Objetivos.DataKeys.Item(row.RowIndex()).Values(2), Request.QueryString("id"))

                Me.lblmensaje_p3.Text = "El Indicador se Elimino con Exito"
                Me.p3_aviso.Attributes.Add("class", "mensajeEliminado")
                Response.Write("<script>alert('" & Me.lblmensaje_p3.Text & "')</script>")
            Else
                Me.lblmensaje_p3.Text = "No existe indicador para Eliminar"
                Me.p3_aviso.Attributes.Add("class", "mensajeError")
                Response.Write("<script>alert('" & Me.lblmensaje_p3.Text & "')</script>")
            End If
            Call wf_CargarListaObjeticosIndicadores(Me.hdcodigoacp.Value)
            Me.tabla_obj.Visible = False
            btnNuevoIndicador.Visible = False
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub ibtnEliminaObjetivo_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Try
            Dim obj As New clsPlanOperativoAnual
            Dim dtt As New Data.DataTable

            Dim ibtnRechaza As ImageButton
            Dim row As GridViewRow
            ibtnRechaza = sender
            row = ibtnRechaza.NamingContainer

            'dtt = obj.POA_VerificarIndicador(Me.dgw_p3_Objetivos.DataKeys.Item(row.RowIndex).Values(0))
            'If dtt.Rows(0).Item(0).ToString > 0 Then
            '    Me.p3_aviso.Attributes.Add("class", "mensajeError")
            '    Me.lblmensaje_p3.Text = "Debe Eliminar todos los Indicadores para poder Eliminar el Objetivo"
            '    Response.Write("<script>alert('" & Me.lblmensaje_p3.Text & "')</script>")
            'Else
            '    obj.EliminarObjetivo(Me.dgw_p3_Objetivos.DataKeys.Item(row.RowIndex()).Values(0), Request.QueryString("id"))
            '    Call wf_CargarListaObjeticosIndicadores(Me.hdcodigoacp.Value)

            '    Me.lblmensaje_p3.Text = "El Objetivo se Elimino con Exito"
            '    Me.p3_aviso.Attributes.Add("class", "mensajeEliminado")

            '    Response.Write("<script>alert('" & Me.lblmensaje_p3.Text & "')</script>")
            'End If

            Dim mensaje As String
            mensaje = obj.POA_EliminarObjetivosIndicadores(Me.dgw_p3_Objetivos.DataKeys.Item(row.RowIndex()).Values(0), Request.QueryString("id"))
            If mensaje = "1" Then
                Me.lblmensaje_p3.Text = "El Objetivo se Elimino con Exito"
                Me.p3_aviso.Attributes.Add("class", "mensajeEliminado")
                Response.Write("<script>alert('" & Me.lblmensaje_p3.Text & "')</script>")
                Call wf_CargarListaObjeticosIndicadores(Me.hdcodigoacp.Value)
            ElseIf mensaje = "0" Then
                Me.lblmensaje_p3.Text = "No se puede Eliminar el Objetivo, debido a que se Registrarón Avances"
                Me.p3_aviso.Attributes.Add("class", "mensajeError")
                Response.Write("<script>alert('" & Me.lblmensaje_p3.Text & "')</script>")
            End If

            Me.tabla_obj.Visible = False
            btnNuevoIndicador.Visible = False

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub




#End Region

#Region "PANEL N°4"
    Protected Sub btn_p4_NuevaActividad_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_p4_NuevaActividad.Click
        Me.tabla_Act.Visible = True
        hd_accion.Value = "N"
        hdcodigo_dap.Value = 0
        hdAccion.Value = "AN"
        Call wf_LimpiarControles_paso4()
        Call wf_HabilitarControles_paso4(True)
        Call wf_CargarDetalleActividad(hdcodigoacp.Value)
        txt_p4_descripcion.Focus()
    End Sub

    Sub wf_LimpiarControles_paso4()
        txt_p4_descripcion.Text = ""
        txt_p4_meta.Text = "100.00"
        txt_p4_avance.Text = "0.00"
        txt_p4_fecini.Text = lblfecini.Text
        txt_p4_fecfin.Text = lblfecfin.Text
        chk_requiere_pto.Checked = True
        Me.p4_aviso.Attributes.Clear()
        Me.lblmensaje.Text = ""
    End Sub

    Sub wf_HabilitarControles_paso4(ByVal as_flag As Boolean)
        Me.divCalendario_ini.Visible = False
        Me.divCalendario_fin.Visible = False

        txt_p4_descripcion.Enabled = as_flag
        txt_p4_meta.Enabled = as_flag
        txt_p4_avance.Enabled = False
        txt_p4_fecini.Enabled = as_flag
        txt_p4_fecfin.Enabled = as_flag
        ddl_p4_responsable.Enabled = as_flag
    End Sub


    Protected Sub btnCalendario_ini_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCalendario_ini.Click
        If Me.divCalendario_ini.Visible = False Then
            Me.divCalendario_ini.Visible = True

            Dim fec_ini() As String = Me.txt_p4_fecini.Text.Split("/")
            Me.Calendario_ini.VisibleDate = New DateTime(fec_ini(2), fec_ini(1), fec_ini(0))
        Else
            Me.divCalendario_ini.Visible = False
        End If
        Me.aviso.Attributes.Clear()
        Me.lblmensaje.Text = ""
    End Sub

    Protected Sub Calendario_ini_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Calendario_ini.SelectionChanged
        If Me.Calendario_ini.SelectedDate >= Me.lblfecini.Text And Me.Calendario_ini.SelectedDate <= Me.lblfecfin.Text Then
            Me.txt_p4_fecini.Text = Me.Calendario_ini.SelectedDate.ToString("dd/MM/yyyy")

            Me.divCalendario_ini.Visible = False
            Me.p4_aviso.Attributes.Clear()
            Me.lblmensaje.Text = ""

        Else
            Response.Write("<script>alert('La fecha se encuentra fuera de rango establecido')</script>")
            Me.lblmensaje.Text = "La fecha se encuentra fuera de rango establecido"
            Me.p4_aviso.Attributes.Add("class", "mensajeError")

            Me.Calendario_ini.SelectedDate = New Date(1990, 1, 1)
            txt_p4_fecini.Text = lblfecini.Text
            Exit Sub
        End If
    End Sub

    Protected Sub btnCalendario_fin_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCalendario_fin.Click
        If Me.divCalendario_fin.Visible = False Then
            Me.divCalendario_fin.Visible = True

            Dim fec_fin() As String = Me.txt_p4_fecfin.Text.Split("/")
            Me.Calendario_fin.VisibleDate = New DateTime(fec_fin(2), fec_fin(1), fec_fin(0))

        Else
            Me.divCalendario_fin.Visible = False
        End If
        Me.aviso.Attributes.Clear()
        Me.lblmensaje.Text = ""
    End Sub


    Protected Sub Calendario_fin_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Calendario_fin.SelectionChanged
        If Me.Calendario_fin.SelectedDate >= Me.lblfecini.Text And Me.Calendario_fin.SelectedDate <= Me.lblfecfin.Text Then
            Me.txt_p4_fecfin.Text = Me.Calendario_fin.SelectedDate.ToString("dd/MM/yyyy")

            Me.divCalendario_fin.Visible = False
            Me.p4_aviso.Attributes.Clear()
            Me.lblmensaje.Text = ""

        Else
            Response.Write("<script>alert('La fecha se encuentra fuera de rango establecido')</script>")
            Me.lblmensaje.Text = "La fecha se encuentra fuera de rango establecido"
            Me.p4_aviso.Attributes.Add("class", "mensajeError")

            Me.Calendario_fin.SelectedDate = New Date(1990, 1, 1)
            txt_p4_fecfin.Text = lblfecfin.Text
            Exit Sub
        End If
    End Sub

    Function wf_validate_paso4() As Boolean
        If txt_p4_descripcion.Text.Trim = "" Then
            Response.Write("<script>alert('Debe Ingresar descripción de la Actividad')</script>")
            Me.lblmensaje.Text = "Debe Ingresar descripción de la Actividad"
            Me.p4_aviso.Attributes.Add("class", "mensajeError")
            txt_p4_descripcion.Focus()
            Return False
        End If

        txt_p4_meta.Text = "100.00"

        If txt_p4_fecini.Text.Trim = "" Or Len(txt_p4_fecini.Text.Trim) < 10 Then
            Response.Write("<script>alert('Debe Ingresar Fecha de Inicio')</script>")
            Me.lblmensaje.Text = "Debe Ingresar Fecha de Inicio"
            Me.p4_aviso.Attributes.Add("class", "mensajeError")

            txt_p4_fecini.Focus()
            Return False
        End If

        If txt_p4_fecfin.Text.Trim = "" Or Len(txt_p4_fecfin.Text.Trim) < 10 Then
            Response.Write("<script>alert('Debe Ingresar Fecha Final')</script>")
            Me.lblmensaje.Text = "Debe Ingresar Fecha Final"
            Me.p4_aviso.Attributes.Add("class", "mensajeError")

            txt_p4_fecfin.Focus()
            Return False
        End If

        If Not IsDate(txt_p4_fecini.Text) Then
            Response.Write("<script>alert('La fecha de inicio no es correcta: " & txt_p4_fecini.Text & " ')</script>")
            Return False
        End If


        If Not IsDate(txt_p4_fecfin.Text) Then
            Response.Write("<script>alert('La fecha final no es la correcta: " & txt_p4_fecfin.Text & "')</script>")
            Return False
        End If

        'Validar q las fechas se encuentren entre el rango especificado en el paso1
        Dim mes_ini_p1 As Integer = ddlMesInicio.SelectedValue.ToString
        Dim mes_fin_p1 As Integer = ddlMesFin.SelectedValue.ToString
        Dim mes_ini As Integer = txt_p4_fecini.Text.Substring(3, 2)
        Dim mes_fin As Integer = txt_p4_fecfin.Text.Substring(3, 2)

        Dim fecha_inicial_o As Date = lblfecini.Text
        Dim fecha_inicial_m As Date = txt_p4_fecini.Text

        Dim fecha_final_o As Date = lblfecfin.Text
        Dim fecha_final_m As Date = txt_p4_fecfin.Text

        If ((fecha_inicial_m < fecha_inicial_o) Or (fecha_inicial_m > fecha_final_o)) Then
            Response.Write("<script>alert('Fecha de Inicio se encuentra en un Rango que no coincide con la Fecha del Programa o Proyecto')</script>")
            Me.lblmensaje.Text = "Fecha de Inicio se encuentra en un Rango que no coincide con la Fecha del Programa o Proyecto"
            Me.p4_aviso.Attributes.Add("class", "mensajeError")
            Return False
        End If

        If (fecha_final_m < fecha_inicial_o) Or (fecha_final_m > fecha_final_o) Then
            Response.Write("<script>alert('Fecha de Fin se encuentra en un Rango que no coincide con la Fecha del Programa o Proyecto')</script>")
            Me.lblmensaje.Text = "Fecha de Fin se encuentra en un Rango que no coincide con la Fecha del Programa o Proyecto"
            Me.p4_aviso.Attributes.Add("class", "mensajeError")
            Return False
        End If

        If ddl_p4_responsable.SelectedValue.ToString = 0 Then
            Response.Write("<script>alert('Seleccione un Responsable')</script>")
            Me.lblmensaje.Text = "Seleccione un Responsable"
            Me.p4_aviso.Attributes.Add("class", "mensajeError")
            ddl_p4_responsable.Focus()
            Return False
        End If

        Return True
    End Function

    Protected Sub btn_p4_agregarActividad_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_p4_agregarActividad.Click
        Try
            If wf_validate_paso4() = False Then
                Call wf_CargarDetalleActividad(hdcodigoacp.Value)
                Return
            End If

            Dim codigo_dap As Integer = hdcodigo_dap.Value
            Dim descripcion_dap As String = txt_p4_descripcion.Text.Trim
            Dim meta_dap As Decimal = txt_p4_meta.Text
            'Dim avance_dap As Decimal = txt_p4_avance.Text
            Dim fecini_dap As String = txt_p4_fecini.Text
            Dim fecfin_dap As String = txt_p4_fecfin.Text
            Dim estado_dap As String = "A"
            Dim responsable_dap As Integer = ddl_p4_responsable.SelectedValue
            Dim nombreresponsable_dap As String = ddl_p4_responsable.SelectedItem.ToString
            Dim codigo_acp As Integer = Me.hdcodigoacp.Value
            Dim requiere_pto As String = IIf(chk_requiere_pto.Checked = True, 1, 0)

            Dim obj As New clsPlanOperativoAnual
            obj.POA_InsertaDetalleActividad(codigo_dap, descripcion_dap, meta_dap, fecini_dap, fecfin_dap, estado_dap, Request.QueryString("id"), responsable_dap, codigo_acp, requiere_pto)

            Call wf_CargarDetalleActividad(codigo_acp)
            Call wf_LimpiarControles_paso4()
            Call wf_HabilitarControles_paso4(False)
            Me.p4_aviso.Attributes.Clear()
            hd_accion.Value = "N"

            If hdAccion.Value = "AN" Then
                Response.Write("<script>alert('La Actividad se Registro Correctamente')</script>")
                Me.lblmensaje.Text = "La Actividad se Registro Correctamente"
                Me.p4_aviso.Attributes.Add("class", "mensajeExito")
            ElseIf hdAccion.Value = "AM" Then
                Response.Write("<script>alert('La Actividad se Modifico Correctamente')</script>")
                Me.lblmensaje.Text = "La Actividad se Modifico Correctamente"
                Me.p4_aviso.Attributes.Add("class", "mensajeExito")

            End If

            Me.tabla_Act.Visible = False
            Call wf_CargarDetalleActividad(hdcodigoacp.Value)
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub btn_p4_cancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_p4_cancelar.Click
        Me.tabla_Act.Visible = False
        Call wf_LimpiarControles_paso4()
        Call wf_HabilitarControles_paso4(False)
        Me.p4_aviso.Attributes.Clear()

        Me.lblmensaje.Text = ""
        Call wf_CargarDetalleActividad(hdcodigoacp.Value)
    End Sub

    Protected Sub cmdGuardarPaso4_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdGuardarPaso4.Click
        Call wf_CargarDetalleActividad(Me.hdcodigoacp.Value)

        If Request.QueryString("tipo_acc") = "PL" Then
            Response.Redirect("FrmListaEvaluarAlineamiento.aspx?id=" & Request.QueryString("id") & "&ctf=" & Request.QueryString("ctf") & "&cb1=" & Request.QueryString("cb1") & "&cb2=" & Request.QueryString("cb2") & "&cb3=" & Request.QueryString("cb3") & "&cb4=" & Request.QueryString("cb4") & "&accion=C" & "&codigo_acp=" & Request.QueryString("codigo_acp"))
        Else
            Response.Redirect("FrmListaActividadesPOA.aspx?id=" & Request.QueryString("id") & "&ctf=" & Request.QueryString("ctf") & "&cb1=" & Request.QueryString("cb1") & "&cb2=" & Request.QueryString("cb2") & "&cb3=" & Request.QueryString("cb3") & "&index_poa=" & Request.QueryString("index_poa") & "&accion=C&index_acp=" & Me.hdcodigoacp.Value)
        End If
    End Sub

    Protected Sub cmdCancelarPaso4_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdCancelarPaso4.Click
        Me.paso1.Visible = False
        Me.paso2.Visible = True
        'Me.paso3.Visible = True
        Me.paso4.Visible = False
        Me.tab2.CssClass = "tab_activo"
        Me.tab4.CssClass = "tab_inactivo"
        Call wf_CargarListaObjeticosIndicadores(Me.hdcodigoacp.Value)
    End Sub

    Protected Sub dgv_p4_detalle_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles dgv_p4_detalle.RowCommand
        If (e.CommandName.Equals("Select")) Then 'comprueba que sea el boton de seleccion   
            Dim seleccion As GridViewRow
            seleccion = DirectCast(e.CommandSource, GridView).Rows(e.CommandArgument)

            Dim obj As New clsPlanOperativoAnual
            Dim dttVerificar As New Data.DataTable
            hdcodigo_dap.Value = Convert.ToInt32(dgv_p4_detalle.DataKeys(seleccion.RowIndex).Values("codigo_dap").ToString)
            Dim descripcion_dap As String = dgv_p4_detalle.Rows(seleccion.RowIndex).Cells(0).Text

            dttVerificar = obj.POA_VerificarDetalleActividadPresupuesto(hdcodigo_dap.Value)
            If dttVerificar.Rows(0).Item(0) > 0 Then
                Me.tabla_Act.Visible = False
                Me.lblmensaje.Text = "[ " + descripcion_dap + "]  NO se puede Editar, debido a que cuenta con Presupuesto"
                Me.p4_aviso.Attributes.Add("class", "mensajeError")
                Response.Write("<script>alert('" & Me.lblmensaje.Text & "')</script>")
            Else
                Me.p4_aviso.Attributes.Clear()
                Me.lblmensaje.Text = ""

                hd_indexGrid.Value = seleccion.RowIndex
                hd_accion.Value = "M"
                txt_p4_descripcion.Text = dgv_p4_detalle.Rows(seleccion.RowIndex).Cells(0).Text
                txt_p4_meta.Text = dgv_p4_detalle.Rows(seleccion.RowIndex).Cells(1).Text
                txt_p4_avance.Text = 0
                txt_p4_fecini.Text = dgv_p4_detalle.Rows(seleccion.RowIndex).Cells(3).Text
                txt_p4_fecfin.Text = dgv_p4_detalle.Rows(seleccion.RowIndex).Cells(4).Text
                ddl_p4_responsable.SelectedValue = Convert.ToInt32(dgv_p4_detalle.DataKeys(seleccion.RowIndex).Values("responsable_dap").ToString)

                chk_requiere_pto.Checked = IIf(dgv_p4_detalle.Rows(seleccion.RowIndex).Cells(6).Text = "SI", True, False)

                Me.tabla_Act.Visible = True
                Me.p4_aviso.Attributes.Clear()
                Me.lblmensaje.Text = ""
                hdAccion.Value = "AM"

                Call wf_HabilitarControles_paso4(True)
                txt_p4_descripcion.Focus()
                Call wf_CargarDetalleActividad(hdcodigoacp.Value)
            End If

        End If
    End Sub

    Protected Sub dgv_p4_detalle_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles dgv_p4_detalle.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then

                '========= Pinta Fila cuando se hayan asignado Items al Presupuesto ==========
                Dim obj As New clsPlanOperativoAnual
                Dim codigo_dap As String = DataBinder.Eval(e.Row.DataItem, "codigo_dap").ToString()
                Dim nItem As Integer = obj.POA_VerificaItemPresupuesto(codigo_dap)
                If nItem > 0 Then
                    e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFC0CB")
                End If
                '========= Pinta Fila cuando se hayan asignado Items al Presupuesto ==========

                Ingreso += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "ingresos"))
                Egreso += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "egresos"))
                If Request.QueryString("tipo_acc") = "PL" Then
                    Me.dgv_p4_detalle.Columns(15).Visible = True
                    e.Row.Cells(15).Text = "<a href='FrmMoverDetalleActividad.aspx?codigo_dap=" & e.Row.DataItem("codigo_dap") & "&id=" & Request.QueryString("id") & "&ctf=" & Request.QueryString("ctf") & "&cb1=" & Request.QueryString("cb1") & "&cb2=" & Request.QueryString("cb2") & "&cb3=" & Request.QueryString("cb3") & "&cb4=" & Request.QueryString("cb4") & "&codigo_poa=" & Request.QueryString("codigo_poa") & "&codigo_acp=" & Request.QueryString("codigo_acp") & "' title='Mover Actividad' class='thickbox'><img src='../../images/mover.gif' border=0 /><a/>"
                Else
                    Me.dgv_p4_detalle.Columns(15).Visible = False
                End If

            ElseIf e.Row.RowType = DataControlRowType.Footer Then
                e.Row.Cells(0).Text = "TOTALES "
                e.Row.Cells(0).HorizontalAlign = HorizontalAlign.Center
                e.Row.Cells(0).ColumnSpan = 5

                e.Row.Cells(1).Visible = False
                e.Row.Cells(2).Visible = False
                e.Row.Cells(0).ForeColor = Drawing.Color.Blue

                e.Row.Cells(3).Text = FormatNumber(Ingreso.ToString(), 2)
                e.Row.Cells(3).HorizontalAlign = HorizontalAlign.Right
                e.Row.Cells(3).ForeColor = Drawing.Color.Blue

                e.Row.Cells(4).Text = FormatNumber(Egreso.ToString(), 2)
                e.Row.Cells(4).HorizontalAlign = HorizontalAlign.Right
                e.Row.Cells(4).ForeColor = Drawing.Color.Blue

                e.Row.Cells(7).ColumnSpan = 3
                e.Row.Cells(5).Visible = False
                e.Row.Cells(6).Visible = False
                e.Row.Cells(7).Visible = False
                e.Row.Cells(8).Visible = False

                e.Row.Font.Bold = True
            End If

            If e.Row.Cells(6).Text = "NO" Then
                e.Row.Cells(9).Text = ""
            End If

        Catch err As Exception
            Dim [error] As String = err.Message.ToString() + " - " + err.Source.ToString()
        End Try
    End Sub

    Protected Sub dgv_p4_detalle_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles dgv_p4_detalle.RowDeleting
        Try
            Dim obj As New clsPlanOperativoAnual
            Dim dttVerificar As New Data.DataTable
            Dim codigo_dap As Integer = dgv_p4_detalle.DataKeys(e.RowIndex()).Values("codigo_dap").ToString
            Dim descripcion_dap As String = dgv_p4_detalle.Rows(e.RowIndex()).Cells(0).Text

            dttVerificar = obj.POA_VerificarDetalleActividadPresupuesto(codigo_dap)
            If dttVerificar.Rows(0).Item(0) > 0 Then
                Me.lblmensaje.Text = "[ " + descripcion_dap + "]  NO se puede Eliminar, debido a que cuenta con Presupuesto"
                Me.p4_aviso.Attributes.Add("class", "mensajeError")
                Response.Write("<script>alert('" & Me.lblmensaje.Text & "')</script>")
            Else
                obj.POA_EliminarDetalleActividad(codigo_dap, Request.QueryString("id"))

                Call wf_LimpiarControles_paso4()
                Call wf_CargarDetalleActividad(hdcodigoacp.Value)

                Me.lblmensaje.Text = "La Actividad se Elimino Correctamente"
                Me.p4_aviso.Attributes.Add("class", "mensajeEliminado")
                Response.Write("<script>alert('" & Me.lblmensaje.Text & "')</script>")
            End If

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Sub wf_CargarDetalleActividad(ByVal codigo_acp As Integer)
        Try
            Ingreso = 0
            Egreso = 0
            Dim obj As New clsPlanOperativoAnual
            Dim dttDetalle As New Data.DataTable
            dgv_p4_detalle.DataSource = Nothing
            dgv_p4_detalle.DataSource = obj.POA_ListarDetalleActividad(codigo_acp)
            dgv_p4_detalle.DataBind()
            obj = Nothing
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try

    End Sub

    Protected Sub btnregresarp4_top_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnregresarp4_top.Click
        Me.cmdCancelarPaso4_Click(sender, e)
    End Sub

    Protected Sub btnguardarp4_top_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnguardarp4_top.Click
        Me.cmdGuardarPaso4_Click(sender, e)
    End Sub

    Protected Sub dgv_p4_detalle_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles dgv_p4_detalle.RowEditing
        Try
            Response.Redirect("../../../librerianet/presupuesto/areas/RegistrarPresupuestoDetalle_V2.aspx?tipo=P&idPto=" & Request.QueryString("id") & _
                              "&ctfPto=" & Request.QueryString("ctf") & "&actividadPto=" & txtdescripcion.Text & "&dapPto=" & dgv_p4_detalle.DataKeys(e.NewEditIndex).Values("codigo_dap").ToString & _
                              "&codigo_poa=" & Request.QueryString("codigo_poa") & "&codigo_acp=" & Me.hdcodigoacp.Value & "&cb1=" & Request.QueryString("cb1") & _
                              "&cb2=" & Request.QueryString("cb2") & "&cb3=" & Request.QueryString("cb3") & "&cb4=" & Request.QueryString("cb4") & "&tipo_acc=" & _
                               Request.QueryString("tipo_acc") & "&index_poa=" & Request.QueryString("index_poa"))

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try

    End Sub

    Protected Sub ibtnMoverActividad_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Try
            Dim codigo_dap As String

            codigo_dap = dgv_p4_detalle.SelectedDataKey.Item(0).Values("codigo_dap").ToString
            Response.Write("<script>alert('" & codigo_dap & "')</script>")
            'Response.Redirect("../../../librerianet/presupuesto/areas/RegistrarPresupuestoDetalle_V2.aspx?tipo=P&idPto=" & Request.QueryString("id") & _
            '                 "&ctfPto=" & Request.QueryString("ctf") & "&actividadPto=" & txtdescripcion.Text & "&dapPto=" & dgv_p4_detalle.DataKeys(e.NewEditIndex).Values("codigo_dap").ToString & _
            '                 "&codigo_poa=" & Request.QueryString("codigo_poa") & "&codigo_acp=" & Me.hdcodigoacp.Value & "&cb1=" & Request.QueryString("cb1") & _
            '                 "&cb2=" & Request.QueryString("cb2") & "&cb3=" & Request.QueryString("cb3") & "&cb4=" & Request.QueryString("cb4") & "&tipo_acc=" & _
            'Request.QueryString("tipo_acc") & "&index_poa=" & Request.QueryString("index_poa"))

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

#End Region

    Protected Sub tab1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles tab1.Click
        Dim chekeados As New ArrayList
        chekeados = RecorreArbol()
        If chekeados.Count > 0 Then
            Me.cmdGuardarPaso2_Click(sender, e)
            Me.paso1.Visible = True
            Me.paso2.Visible = False
            Me.paso3.Visible = False
            Me.paso4.Visible = False
            Me.tab1.CssClass = "tab_activo"
            Me.tab2.CssClass = "tab_inactivo"
            Me.tab3.CssClass = "tab_inactivo"
            Me.tab4.CssClass = "tab_inactivo"
            OcultaObservacion()
        Else
            Response.Write("<script>alert('Debe Seleccionar al menos un Indicador')</script>")
        End If
    End Sub

    Protected Sub tab2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles tab2.Click
        If Me.btnguardar_cco.Visible = False Then
            If Me.txtdescripcion.Text <> "" Then
                Me.tab1.CssClass = "tab_inactivo"
                Me.tab2.CssClass = "tab_activo"
                Me.tab3.CssClass = "tab_inactivo"
                Me.tab4.CssClass = "tab_inactivo"
                Me.paso1.Visible = False
                Me.paso2.Visible = True
                Me.paso3.Visible = False
                Me.paso4.Visible = False

                OcultaObservacion()

                If Me.hdcodigoacp.Value <> 0 Then
                    Me.lblactividadPaso2.Text = Me.txtdescripcion.Text
                End If
                Me.lblmensajeDuplicidad.Text = ""
                Me.aviso.Attributes.Clear()
            Else
                Me.lblmensajeDuplicidad.Text = "No Puede Continuar, Debe Guardar los Cambios de Actividad o Proyecto "
                Me.aviso.Attributes.Add("class", "mensajeError")
            End If

        Else
            Me.lblmensajeDuplicidad.Text = "No Puede Continuar, Registro de Nuevo Centro de costo Activo "
            Me.aviso.Attributes.Add("class", "mensajeError")
        End If

    End Sub

    Protected Sub tab3_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles tab3.Click
        Try

            If Me.btnguardar_cco.Visible = False Then

                'If Me.tab1.CssClass = "tab_activo" Then
                '    Me.cmdGuardarPaso1_Click(sender, e)
                'ElseIf Me.tab2.CssClass = "tab_activo" Then
                '    Me.cmdGuardarPaso2_Click(sender, e)
                'ElseIf Me.tab4.CssClass = "tab_activo" Then
                '    Me.cmdGuardarPaso4_Click(sender, e)
                'End If
                If Me.txtdescripcion.Text <> "" Then
                    Dim chekeados As New ArrayList
                    chekeados = RecorreArbol()
                    If chekeados.Count > 0 Then

                        Me.paso1.Visible = False
                        Me.paso2.Visible = False
                        Me.paso3.Visible = True
                        Me.paso4.Visible = False
                        Me.tab1.CssClass = "tab_inactivo"
                        Me.tab2.CssClass = "tab_inactivo"
                        Me.tab3.CssClass = "tab_activo"
                        Me.tab4.CssClass = "tab_inactivo"
                        OcultaObservacion()

                        If Me.hdcodigoacp.Value <> 0 Then
                            Me.lblactividadPaso3.Text = Me.txtdescripcion.Text
                        End If
                        Me.lblmensajeDuplicidad.Text = ""
                        Me.aviso.Attributes.Clear()

                        Me.lblmensaje_p3.Text = ""
                        Me.p3_aviso.Attributes.Clear()

                        Me.cmdGuardarPaso2_Click(sender, e)

                        '' Objetivos
                        Me.tabla_obj.Visible = False
                        Call wf_habilitaControles(True, 1)
                        Call wf_habilitarMetas()
                        Me.p3_aviso.Attributes.Clear()

                        hd_codigo_pobj.Value = 0
                        hd_codigo_pind.Value = 0

                        txt_meta1.Text = 0
                        txt_meta2.Text = 0
                        txt_meta3.Text = 0
                        txt_meta4.Text = 0
                    Else
                        Response.Write("<script>alert('Debe Seleccionar al menos un Indicador')</script>")
                    End If
                Else
                    Me.lblmensajeDuplicidad.Text = "No Puede Continuar, Debe Guardar los Cambios de Actividad o Proyecto "
                    Me.aviso.Attributes.Add("class", "mensajeError")
                End If

            Else
                Me.lblmensajeDuplicidad.Text = "No Puede Continuar, Registro de Nuevo Centro de costo Activo "
                Me.aviso.Attributes.Add("class", "mensajeError")
            End If

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try


    End Sub

    Protected Sub tab4_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles tab4.Click
        If Me.btnguardar_cco.Visible = False Then
            If Me.txtdescripcion.Text <> "" Then
                Dim chekeados As New ArrayList
                chekeados = RecorreArbol()
                If chekeados.Count > 0 Then
                    Me.cmdGuardarPaso2_Click(sender, e)
                    If Me.hdcodigoacp.Value <> 0 Then
                        Me.lblactividadPaso4.Text = Me.txtdescripcion.Text
                    End If
                    Me.paso1.Visible = False
                    Me.paso2.Visible = False
                    Me.paso3.Visible = False
                    Me.paso4.Visible = True

                    Me.tab1.CssClass = "tab_inactivo"
                    Me.tab2.CssClass = "tab_inactivo"
                    Me.tab3.CssClass = "tab_inactivo"
                    Me.tab4.CssClass = "tab_activo"

                    OcultaObservacion()

                    Call wf_CargarDetalleActividad(hdcodigoacp.Value)
                    Me.tabla_Act.Visible = False
                    Me.divCalendario_ini.Visible = False
                    Me.divCalendario_fin.Visible = False
                    Me.p4_aviso.Attributes.Clear()
                    Me.lblmensaje.Text = ""

                    Me.lblmensajeDuplicidad.Text = ""
                    Me.aviso.Attributes.Clear()
                Else
                    Response.Write("<script>alert('Debe Seleccionar al menos un Indicador')</script>")
                End If
            Else
                Me.lblmensajeDuplicidad.Text = "No Puede Continuar, Debe Guardar los Cambios de Actividad o Proyecto "
                Me.aviso.Attributes.Add("class", "mensajeError")
            End If

        Else
            Me.lblmensajeDuplicidad.Text = "No Puede Continuar, Registro de Nuevo Centro de costo Activo "
            Me.aviso.Attributes.Add("class", "mensajeError")
        End If

    End Sub

    Protected Sub ddlMesInicio_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlMesInicio.SelectedIndexChanged
        MesFin()
    End Sub

    Sub MesFin()
        Dim i, a As Integer
        Me.ddlMesFin.Items.Clear()
        Me.ddlMesFin.Items.Add(New ListItem("--Seleccione--", 0))
        a = Me.ddlMesInicio.SelectedValue
        If Me.ddlMesInicio.SelectedValue = 0 Then
            a = 1
        End If
        For i = a To 12
            Me.ddlMesFin.Items.Add(New ListItem(Me.ddlMesInicio.Items(i).Text, Me.ddlMesInicio.Items(i).Value))
        Next
    End Sub

    Protected Sub btnAprobar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAprobar.Click
        Try
            Dim obj As New clsPlanOperativoAnual
            Dim mensaje As String
            Dim chekeados As New ArrayList
            chekeados = RecorreArbol()
            If chekeados.Count > 0 Then
                Me.cmdGuardarPaso2_Click(sender, e)

                '========================Agregado fatima.vasquez 09-08-2018===========================
                Dim codigo_poa As Integer = Request.QueryString("codigo_poa")
                Dim tipo_cco As Integer = obj.POA_verificarCentroCosto(codigo_poa, Me.hdcodigoacp.Value)
                Dim ctf As Integer
                ctf = Request.QueryString("ctf")

                If tipo_cco = 1 And ctf = 187 Then 'Programa de Educación Continua 'Dirección de Marketing
                    'Aprobado por Marketing
                    mensaje = obj.InstanciaEstadoActividad(Me.hdcodigoacp.Value, 6, Request.QueryString("id"))
                    'Comento Usuario no debe actualizar Observacion
                    obj.POA_ActualizarObservacion(Me.hdcodigoacp.Value, " - RESUELTO", Request.QueryString("id"), 1, 22) ' Ultimo valor irrelevante al aprobar, necesario para observar

                    Call wf_RegistrarPresupuesto()

                    '=============== ENVIAR CORREO ===============================
                    Dim objemail As New ClsMail
                    Dim dt As New Data.DataTable
                    Dim cuerpo, receptor, AsuntoCorreo As String
                    dt = obj.ConsultaResponsablesActividad(1, Me.hdcodigoacp.Value, "")

                    cuerpo = "<html>"
                    cuerpo = cuerpo & "<head>"
                    cuerpo = cuerpo & "<title></title>"
                    cuerpo = cuerpo & "<style>"
                    cuerpo = cuerpo & ".FontType{font-family: calibri; font-size:12px; }"
                    cuerpo = cuerpo & "</style>"
                    cuerpo = cuerpo & "</head>"
                    cuerpo = cuerpo & "<body>"
                    cuerpo = cuerpo & "<table border=0 cellpadding=0 class=""FontType"">"
                    cuerpo = cuerpo & "<tr><td colspan=2 ><b>Estimado(a):</b></td></tr>"
                    cuerpo = cuerpo & "<tr><td colspan=2></br></br>El Programa/Proyecto  <b>" & dt.Rows(0).Item("resumen_acp").ToString & "</b>  ha Sido  <b>APROBADO</b>  por el ÁREA DE MARKETING.</td></tr>"
                    cuerpo = cuerpo & "<tr><td colspan=2></br></br>Saludos Cordiales</td></tr>"
                    cuerpo = cuerpo & "</table>"
                    cuerpo = cuerpo & "</body>"
                    cuerpo = cuerpo & "</html>"

                    If ConfigurationManager.AppSettings("CorreoUsatActivo") = 1 Then
                        receptor = dt.Rows(0).Item("receptores").ToString
                    Else
                        receptor = "hcano@usat.edu.pe"
                    End If

                    AsuntoCorreo = "[Evaluación de Programa/Proyecto : " & dt.Rows(0).Item("resumen_acp").ToString & "]"
                    objemail.EnviarMail("campusvirtual@usat.edu.pe", "Evaluación de Programa/Proyecto", receptor, AsuntoCorreo, cuerpo, True)
                    '=============== FIN ENVIAR CORREO ===============================
                Else
                    'Aprobado por Planificacion
                    mensaje = obj.InstanciaEstadoActividad(Me.hdcodigoacp.Value, 9, Request.QueryString("id"))
                    'Comento Usuario no debe actualizar Observacion
                    obj.POA_ActualizarObservacion(Me.hdcodigoacp.Value, " - RESUELTO", Request.QueryString("id"), 1, 3) ' Ultimo valor irrelevante al aprobar, necesario para observar

                    Call wf_RegistrarPresupuesto()

                    '=============== ENVIAR CORREO ===============================
                    Dim objemail As New ClsMail
                    Dim cuerpo, receptor, AsuntoCorreo As String
                    Dim dt As New Data.DataTable
                    dt = obj.ConsultaResponsablesActividad(1, Me.hdcodigoacp.Value, "")

                    cuerpo = "<html>"
                    cuerpo = cuerpo & "<head>"
                    cuerpo = cuerpo & "<title></title>"
                    cuerpo = cuerpo & "<style>"
                    cuerpo = cuerpo & ".FontType{font-family: calibri; font-size:12px; }"
                    cuerpo = cuerpo & "</style>"
                    cuerpo = cuerpo & "</head>"
                    cuerpo = cuerpo & "<body>"
                    cuerpo = cuerpo & "<table border=0 cellpadding=0 class=""FontType"">"
                    cuerpo = cuerpo & "<tr><td colspan=2 ><b>Estimado(a):</b></td></tr>"
                    cuerpo = cuerpo & "<tr><td colspan=2></br></br>El Programa/Proyecto  <b>" & dt.Rows(0).Item("resumen_acp").ToString & "</b>  ha Sido  <b>APROBADO</b>  por el ÁREA DE PLANIFICACIÓN.</td></tr>"
                    cuerpo = cuerpo & "<tr><td colspan=2></br></br>Saludos Cordiales</td></tr>"
                    cuerpo = cuerpo & "</table>"
                    cuerpo = cuerpo & "</body>"
                    cuerpo = cuerpo & "</html>"

                    If ConfigurationManager.AppSettings("CorreoUsatActivo") = 1 Then
                        receptor = dt.Rows(0).Item("receptores").ToString
                    Else
                        receptor = "hcano@usat.edu.pe"
                    End If

                    AsuntoCorreo = "[Evaluación de Programa/Proyecto : " & dt.Rows(0).Item("resumen_acp").ToString & "]"
                    objemail.EnviarMail("campusvirtual@usat.edu.pe", "Evaluación de Programa/Proyecto", receptor, AsuntoCorreo, cuerpo, True)
                    '=============== FIN ENVIAR CORREO ===============================
                End If
                Response.Redirect("FrmListaEvaluarAlineamiento.aspx?id=" & Request.QueryString("id") & "&ctf=" & Request.QueryString("ctf") & "&cb1=" & Me.hdcodigoPei.Value & "&cb2=" & Request.QueryString("cb2") & "&cb3=" & Request.QueryString("cb3") & "&cb4=9&accion=C" & "&codigo_acp=" & Request.QueryString("codigo_acp"))
            Else
                Response.Write("<script>alert('Debe Seleccionar al menos un Indicador')</script>")
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try

    End Sub

    Protected Sub linkAprobar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles linkAprobar.Click
        Try
            Dim obj As New clsPlanOperativoAnual
            Dim mensaje As String
            Dim chekeados As New ArrayList
            chekeados = RecorreArbol()
            If chekeados.Count > 0 Then
                Me.cmdGuardarPaso2_Click(sender, e)

                ''========================Agregado fatima.vasquez 09-08-2018===========================
                Dim codigo_poa As Integer = Request.QueryString("codigo_poa")
                Dim tipo_cco As Integer = obj.POA_verificarCentroCosto(codigo_poa, Me.hdcodigoacp.Value)
                Dim ctf As Integer
                ctf = Request.QueryString("ctf")

                If tipo_cco = 1 And ctf = 187 Then 'Programa de Educación Continua 'Dirección de Marketing
                    'Aprobado por Marketing
                    mensaje = obj.InstanciaEstadoActividad(Me.hdcodigoacp.Value, 6, Request.QueryString("id"))
                    'Comento Usuario no debe actualizar Observacion
                    obj.POA_ActualizarObservacion(Me.hdcodigoacp.Value, " - RESUELTO", Request.QueryString("id"), 1, 22) ' Ultimo valor irrelevante al aprobar, necesario para observar

                    Call wf_RegistrarPresupuesto()

                    '=============== ENVIAR CORREO ===============================
                    Dim objemail As New ClsMail
                    Dim cuerpo, receptor, AsuntoCorreo As String
                    Dim dt As New Data.DataTable
                    dt = obj.ConsultaResponsablesActividad(1, Me.hdcodigoacp.Value, "")

                    cuerpo = "<html>"
                    cuerpo = cuerpo & "<head>"
                    cuerpo = cuerpo & "<title></title>"
                    cuerpo = cuerpo & "<style>"
                    cuerpo = cuerpo & ".FontType{font-family: calibri; font-size:12px; }"
                    cuerpo = cuerpo & "</style>"
                    cuerpo = cuerpo & "</head>"
                    cuerpo = cuerpo & "<body>"
                    cuerpo = cuerpo & "<table border=0 cellpadding=0 class=""FontType"">"
                    cuerpo = cuerpo & "<tr><td colspan=2 ><b>Estimado(a):</b></td></tr>"
                    cuerpo = cuerpo & "<tr><td colspan=2></br></br>El Programa/Proyecto  <b>" & dt.Rows(0).Item("resumen_acp").ToString & "</b>  ha Sido  <b>APROBADO</b>  por el ÁREA DE MARKETING.</td></tr>"
                    cuerpo = cuerpo & "<tr><td colspan=2></br></br>Saludos Cordiales</td></tr>"
                    cuerpo = cuerpo & "</table>"
                    cuerpo = cuerpo & "</body>"
                    cuerpo = cuerpo & "</html>"

                    If ConfigurationManager.AppSettings("CorreoUsatActivo") = 1 Then
                        receptor = dt.Rows(0).Item("receptores").ToString
                    Else
                        receptor = "hcano@usat.edu.pe"
                    End If
                    AsuntoCorreo = "[Evaluación de Programa/Proyecto : " & dt.Rows(0).Item("resumen_acp").ToString & "]"
                    objemail.EnviarMail("campusvirtual@usat.edu.pe", "Evaluación de Programa/Proyecto", receptor, AsuntoCorreo, cuerpo, True)
                    '=============== FIN ENVIAR CORREO ===============================

                Else
                    'Aprobado por Planificacion
                    mensaje = obj.InstanciaEstadoActividad(Me.hdcodigoacp.Value, 9, Request.QueryString("id"))
                    'Comento Usuario no debe actualizar Observacion
                    obj.POA_ActualizarObservacion(Me.hdcodigoacp.Value, " - RESUELTO", Request.QueryString("id"), 1, 3) ' Ultimo valor irrelevante al aprobar, necesario para observar

                    Call wf_RegistrarPresupuesto()

                    '=============== ENVIAR CORREO ===============================
                    Dim objemail As New ClsMail
                    Dim cuerpo, receptor, AsuntoCorreo As String
                    Dim dt As New Data.DataTable
                    dt = obj.ConsultaResponsablesActividad(1, Me.hdcodigoacp.Value, "")

                    cuerpo = "<html>"
                    cuerpo = cuerpo & "<head>"
                    cuerpo = cuerpo & "<title></title>"
                    cuerpo = cuerpo & "<style>"
                    cuerpo = cuerpo & ".FontType{font-family: calibri; font-size:12px; }"
                    cuerpo = cuerpo & "</style>"
                    cuerpo = cuerpo & "</head>"
                    cuerpo = cuerpo & "<body>"
                    cuerpo = cuerpo & "<table border=0 cellpadding=0 class=""FontType"">"
                    cuerpo = cuerpo & "<tr><td colspan=2 ><b>Estimado(a):</b></td></tr>"
                    cuerpo = cuerpo & "<tr><td colspan=2></br></br>El Programa/Proyecto  <b>" & dt.Rows(0).Item("resumen_acp").ToString & "</b>  ha Sido  <b>APROBADO</b>  por el ÁREA DE PLANIFICACIÓN.</td></tr>"
                    cuerpo = cuerpo & "<tr><td colspan=2></br></br>Saludos Cordiales</td></tr>"
                    cuerpo = cuerpo & "</table>"
                    cuerpo = cuerpo & "</body>"
                    cuerpo = cuerpo & "</html>"

                    If ConfigurationManager.AppSettings("CorreoUsatActivo") = 1 Then
                        receptor = dt.Rows(0).Item("receptores").ToString
                    Else
                        receptor = "hcano@usat.edu.pe"
                    End If

                    AsuntoCorreo = "[Evaluación de Programa/Proyecto : " & dt.Rows(0).Item("resumen_acp").ToString & "]"
                    objemail.EnviarMail("campusvirtual@usat.edu.pe", "Evaluación de Programa/Proyecto", receptor, AsuntoCorreo, cuerpo, True)
                    '=============== FIN ENVIAR CORREO ===============================

                    'Me.txtobservacion.Text = ""
                    ''Response.Redirect("FrmListaEvaluarAlineamiento.aspx?id=" & Request.QueryString("id") & "&ctf=" & Request.QueryString("ctf") & "&cb1=" & Request.QueryString("cb1") & "&cb2=" & Request.QueryString("cb2") & "&cb3=" & Request.QueryString("cb3") & "&cb4=3&accion=C" & "&codigo_acp=" & Request.QueryString("codigo_acp"))
                    'Me.aviso_obs.Attributes.Add("class", "mensajeExito")
                    'Me.lblrpta_obs.Text = "Programa/Proyecto Observado Correctamente"

                End If
                Response.Redirect("FrmListaEvaluarAlineamiento.aspx?id=" & Request.QueryString("id") & "&ctf=" & Request.QueryString("ctf") & "&cb1=" & Me.hdcodigoPei.Value & "&cb2=" & Request.QueryString("cb2") & "&cb3=" & Request.QueryString("cb3") & "&cb4=9&accion=C" & "&codigo_acp=" & Request.QueryString("codigo_acp"))
            Else
                Response.Write("<script>alert('Debe Seleccionar al menos un Indicador')</script>")
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub linkObservar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles linkObservar.Click
        Dim chekeados As New ArrayList
        chekeados = RecorreArbol()
        If chekeados.Count > 0 Then
            Me.cmdGuardarPaso2_Click(sender, e)
            Me.paso1.Visible = False
            Me.paso2.Visible = False
            Me.paso3.Visible = False
            Me.paso4.Visible = False
            Me.tab1.CssClass = "tab_inactivo"
            Me.tab2.CssClass = "tab_inactivo"
            Me.tab3.CssClass = "tab_inactivo"
            Me.tab4.CssClass = "tab_inactivo"
            Me.PasoObservacion.Visible = True

            'Call wf_RegistrarPresupuesto()
        Else
            Response.Write("<script>alert('Debe Seleccionar al menos un Indicador')</script>")
        End If
    End Sub

    Protected Sub btnObservar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnObservar.Click
        Try
            Dim chekeados As New ArrayList
            chekeados = RecorreArbol()
            If chekeados.Count > 0 Then
                Me.cmdGuardarPaso2_Click(sender, e)
                Me.paso1.Visible = False
                Me.paso2.Visible = False
                Me.paso3.Visible = False
                Me.paso4.Visible = False
                Me.tab1.CssClass = "tab_inactivo"
                Me.tab2.CssClass = "tab_inactivo"
                Me.tab3.CssClass = "tab_inactivo"
                Me.tab4.CssClass = "tab_inactivo"
                Me.PasoObservacion.Visible = True

                'Call wf_RegistrarPresupuesto()
            Else
                Response.Write("<script>alert('Debe Seleccionar al menos un Indicador')</script>")
            End If
            Me.PasoObservacion.Visible = True
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Sub wf_RegistrarPresupuesto()
        'Insertar Items en Presupuesto
        Dim obj As New clsPlanOperativoAnual
        Dim dt As New Data.DataTable
        Dim dtRpta As New Data.DataTable
        Dim codigoArt, codigoRub, codigoPlla As Int64
        codigoArt = 0
        codigoRub = 0
        codigoPlla = 0
        Dim Meses() As String = New String() {"ENERO", "FEBRERO", "MARZO", "ABRIL", "MAYO", "JUNIO", "JULIO", "AGOSTO", "SETIEMBRE", "OCTUBRE", "NOVIEMBRE", "DICIEMBRE"}

        '*** Guarda cabecera de presupuesto ***
        dt = obj.PRESU_ListaItemActividad(hdcodigoacp.Value)
        For i As Integer = 0 To dt.Rows.Count - 1
            codigoArt = IIf(dt.Rows(i).Item("codigo_Art").ToString.Length > 0, dt.Rows(i).Item("codigo_Art"), 0)
            codigoRub = IIf(dt.Rows(i).Item("codigo_Rub").ToString.Length > 0, dt.Rows(i).Item("codigo_Rub"), 0)
            codigoPlla = IIf(dt.Rows(i).Item("codigo_Cplla").ToString.Length > 0, dt.Rows(i).Item("codigo_Cplla"), 0)

            dtRpta = obj.AgregarPresupuesto_V2(dt.Rows(i).Item("codigo_ejp"), dt.Rows(i).Item("codigo_cco"), codigoArt, _
            codigoRub, codigoPlla, dt.Rows(i).Item("detalle").ToString, dt.Rows(i).Item("iduni"), _
            dt.Rows(i).Item("precio"), dt.Rows(i).Item("cantidad"), dt.Rows(i).Item("vigencia"), dt.Rows(i).Item("tipo"), dt.Rows(i).Item("codigo_Per"), _
            dt.Rows(i).Item("indicoCantidades"), dt.Rows(i).Item("forzarDuplicado"), dt.Rows(i).Item("codigo_dap"))

            If dtRpta.Rows.Count > 0 Then
                For j As Int16 = 1 To 12
                    obj.AgregarDetalleEjecucion_V2(dtRpta.Rows(0).Item("rpta").ToString, Meses(j - 1), dt.Rows(i).Item("precio"), 0)
                Next
            End If
        Next
    End Sub

    Sub OcultaObservacion()
        Me.PasoObservacion.Visible = False
        Me.txtobservacion.Text = ""
        Me.lblrpta_obs.Text = ""
        Me.aviso_obs.Attributes.Clear()
    End Sub

    Protected Sub btncancelarobs_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelarObs.Click
        Me.tab1_Click(sender, e)
    End Sub

    Protected Sub btnGuardarObs_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardarObs.Click
        Try
            Dim obj As New clsPlanOperativoAnual
            Dim mensaje As String
            mensaje = obj.POA_ValidarPresupuesto(Me.hdcodigoacp.Value)
            If mensaje = 0 Then

                '========================Agregado fatima.vasquez 09-08-2018===========================
                Dim codigo_poa As Integer = Request.QueryString("codigo_poa")
                Dim tipo_cco As Integer = obj.POA_verificarCentroCosto(codigo_poa, Me.hdcodigoacp.Value)
                Dim ctf As Integer
                ctf = Request.QueryString("ctf")

                If tipo_cco = 1 And ctf = 187 Then 'Programa de Educación Continua 'Dirección de Marketing
                    ' Cambia Instancia-Estado a Observado por Marketing
                    mensaje = obj.InstanciaEstadoActividad(Me.hdcodigoacp.Value, 22, Request.QueryString("id"))
                    mensaje = obj.POA_ActualizarObservacion(Me.hdcodigoacp.Value, Me.txtobservacion.Text, Request.QueryString("id"), 0, 22)
                    Call CargaObservaciones(Me.hdcodigoacp.Value)

                    '=============== ENVIAR CORREO ===============================
                    Dim objemail As New ClsMail
                    Dim dt As New Data.DataTable
                    Dim cuerpo, receptor, AsuntoCorreo As String
                    dt = obj.ConsultaResponsablesActividad(1, Me.hdcodigoacp.Value, "")
                    cuerpo = ""

                    cuerpo = "<html>"
                    cuerpo = cuerpo & "<head>"
                    cuerpo = cuerpo & "<title></title>"
                    cuerpo = cuerpo & "<style>"
                    cuerpo = cuerpo & ".FontType{font-family: calibri; font-size:12px; }"
                    cuerpo = cuerpo & "</style>"
                    cuerpo = cuerpo & "</head>"
                    cuerpo = cuerpo & "<body>"
                    cuerpo = cuerpo & "<table border=0 cellpadding=0 class=""FontType"">"
                    cuerpo = cuerpo & "<tr><td colspan=2 ><b>Estimado(a):</b></td></tr>"
                    cuerpo = cuerpo & "<tr><td colspan=2></br></br>El Programa/Proyecto  <b>" & dt.Rows(0).Item("resumen_acp").ToString & "</b>  ha Sido  <b>OBSERVADO</b>  por el ÁREA DE MARKETING, indicandose lo siguiente:</td></tr>"
                    cuerpo = cuerpo & "<tr><td colspan=2></br></br>" + Me.txtobservacion.Text + "</td></tr>"
                    cuerpo = cuerpo & "<tr><td colspan=2></br></br><i><b>(*)</b> Para corregir las Observaciones deberá ingresar al Módulo <b>PLAN OPERATIVO ANUAL</b></i></td></tr>"
                    cuerpo = cuerpo & "<tr><td colspan=2></br></br>Saludos Cordiales</td></tr>"
                    cuerpo = cuerpo & "</table>"
                    cuerpo = cuerpo & "</body>"
                    cuerpo = cuerpo & "</html>"

                    If ConfigurationManager.AppSettings("CorreoUsatActivo") = 1 Then
                        receptor = dt.Rows(0).Item("receptores").ToString
                    Else
                        receptor = "hcano@usat.edu.pe"
                    End If
                    AsuntoCorreo = "[Evaluación de Programa/Proyecto : " & dt.Rows(0).Item("resumen_acp").ToString & "]"
                    objemail.EnviarMail("campusvirtual@usat.edu.pe", "Evaluación de Programa/Proyecto", receptor, AsuntoCorreo, cuerpo, True)
                    '=============== FIN ENVIAR CORREO ===============================
                Else

                    ' Cambia Instancia-Estado a Registro Poa - Observado
                    mensaje = obj.InstanciaEstadoActividad(Me.hdcodigoacp.Value, 3, Request.QueryString("id"))
                    'Inserta Observación.
                    'Codigo_iep Observacion Planificacion = 3
                    mensaje = obj.POA_ActualizarObservacion(Me.hdcodigoacp.Value, Me.txtobservacion.Text, Request.QueryString("id"), 0, 3)
                    Call CargaObservaciones(Me.hdcodigoacp.Value)

                    '=============== ENVIAR CORREO ===============================
                    Dim objemail As New ClsMail
                    Dim dt As New Data.DataTable
                    Dim cuerpo, receptor, AsuntoCorreo As String
                    dt = obj.ConsultaResponsablesActividad(1, Me.hdcodigoacp.Value, "")
                    cuerpo = ""

                    cuerpo = "<html>"
                    cuerpo = cuerpo & "<head>"
                    cuerpo = cuerpo & "<title></title>"
                    cuerpo = cuerpo & "<style>"
                    cuerpo = cuerpo & ".FontType{font-family: calibri; font-size:12px; }"
                    cuerpo = cuerpo & "</style>"
                    cuerpo = cuerpo & "</head>"
                    cuerpo = cuerpo & "<body>"
                    cuerpo = cuerpo & "<table border=0 cellpadding=0 class=""FontType"">"
                    cuerpo = cuerpo & "<tr><td colspan=2 ><b>Estimado(a):</b></td></tr>"
                    cuerpo = cuerpo & "<tr><td colspan=2></br></br>El Programa/Proyecto  <b>" & dt.Rows(0).Item("resumen_acp").ToString & "</b>  ha Sido  <b>OBSERVADO</b>  por el ÁREA DE PLANIFICACIÓN, indicandose lo siguiente:</td></tr>"
                    cuerpo = cuerpo & "<tr><td colspan=2></br></br>" + Me.txtobservacion.Text + "</td></tr>"
                    cuerpo = cuerpo & "<tr><td colspan=2></br></br><i><b>(*)</b> Para corregir las Observaciones deberá ingresar al Módulo <b>PLAN OPERATIVO ANUAL</b></i></td></tr>"
                    cuerpo = cuerpo & "<tr><td colspan=2></br></br>Saludos Cordiales</td></tr>"
                    cuerpo = cuerpo & "</table>"
                    cuerpo = cuerpo & "</body>"
                    cuerpo = cuerpo & "</html>"

                    If ConfigurationManager.AppSettings("CorreoUsatActivo") = 1 Then
                        receptor = dt.Rows(0).Item("receptores").ToString
                    Else
                        receptor = "hcano@usat.edu.pe"
                    End If
                    AsuntoCorreo = "[Evaluación de Programa/Proyecto : " & dt.Rows(0).Item("resumen_acp").ToString & "]"
                    objemail.EnviarMail("campusvirtual@usat.edu.pe", "Evaluación de Programa/Proyecto", receptor, AsuntoCorreo, cuerpo, True)
                    '=============== FIN ENVIAR CORREO ===============================
                End If
                Me.txtobservacion.Text = ""
                'Response.Redirect("FrmListaEvaluarAlineamiento.aspx?id=" & Request.QueryString("id") & "&ctf=" & Request.QueryString("ctf") & "&cb1=" & Request.QueryString("cb1") & "&cb2=" & Request.QueryString("cb2") & "&cb3=" & Request.QueryString("cb3") & "&cb4=3&accion=C" & "&codigo_acp=" & Request.QueryString("codigo_acp"))
                Me.aviso_obs.Attributes.Add("class", "mensajeExito")
                Me.lblrpta_obs.Text = "Programa/Proyecto Observado Correctamente"
            Else
                Me.aviso_obs.Attributes.Add("class", "mensajeError")
                Me.lblrpta_obs.Text = "Programa/Proyecto No puede Ser Observado, Cuenta con Presupuesto Asignado"
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Function EnviarMensaje(ByVal de As String, ByVal de_nombre As String, ByVal para As String, ByVal asunto As String, ByVal mensaje As String, ByVal copia As String, ByVal rutaarchivo As String, ByVal nombrearchivo As String, ByVal replyto As String) As Boolean
        Try
            Dim cls As New ClsEnviaMailPOA
            If cls.EnviarMailAd(de, de_nombre, para, asunto, mensaje, True, copia, replyto, rutaarchivo, nombrearchivo) Then
                cls = Nothing
                Return True
            Else
                cls = Nothing
                Return False
            End If
        Catch ex As Exception
            Return False
        End Try
    End Function

    Protected Sub txtEgresos_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtEgresos.TextChanged
        Try
            If IsNumeric(Me.txtEgresos.Text) Then
                If Me.txtEgresos.Text.Contains(".") Then
                Else
                    Me.txtEgresos.Text = Me.txtEgresos.Text & ".00"
                End If
                Me.txtEgresos.Text = FormatNumber(Me.txtEgresos.Text.ToString, 2)
            End If

            If IsNumeric(Me.txtEgresos.Text) And IsNumeric(Me.txtIngresos.Text) Then
                Me.txtUtilidad.Text = Me.txtIngresos.Text - Me.txtEgresos.Text
                Me.txtUtilidad.Text = FormatNumber(Me.txtUtilidad.Text.ToString, 2)
                If Me.ddlTipoActividad.SelectedValue = 2 Then
                    If Me.txtUtilidad.Text > 0 Then
                        If Me.txtEgresos.Text > 0 Then
                            Me.lblutilidad_porcentaje.Text = FormatNumber((Me.txtUtilidad.Text / Me.txtEgresos.Text) * 100, 0)
                        Else
                            Me.lblutilidad_porcentaje.Text = Me.txtUtilidad.Text
                        End If
                    Else
                        Me.lblutilidad_porcentaje.Text = 0
                    End If
                End If
            Else
                Me.txtUtilidad.Text = "0.00"
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub txtIngresos_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtIngresos.TextChanged
        Try
            If IsNumeric(Me.txtIngresos.Text) Then
                If Me.txtIngresos.Text.Contains(".") Then
                Else
                    Me.txtIngresos.Text = Me.txtIngresos.Text & ".00"
                End If
                Me.txtIngresos.Text = FormatNumber(Me.txtIngresos.Text.ToString, 2)
            End If

            If IsNumeric(Me.txtEgresos.Text) And IsNumeric(Me.txtIngresos.Text) Then
                Me.txtUtilidad.Text = Me.txtIngresos.Text - Me.txtEgresos.Text
                Me.txtUtilidad.Text = FormatNumber(Me.txtUtilidad.Text.ToString, 2)
                If Me.ddlTipoActividad.SelectedValue = 2 Then
                    If Me.txtUtilidad.Text > 0 Then
                        If Me.txtEgresos.Text > 0 Then
                            Me.lblutilidad_porcentaje.Text = FormatNumber((Me.txtUtilidad.Text / Me.txtEgresos.Text) * 100, 0)
                        Else
                            Me.lblutilidad_porcentaje.Text = Me.txtUtilidad.Text
                        End If
                    Else
                        Me.lblutilidad_porcentaje.Text = 0
                    End If
                End If
            Else
                Me.txtUtilidad.Text = "0.00"
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub ddlCategoria_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlCategoria.SelectedIndexChanged
        Try
            Me.aviso_cco_per.Attributes.Remove("class")
            If Me.ddlCategoria.SelectedValue = 0 Then
                Me.ddlProgPresupuestal.SelectedValue = 0
            Else
                Dim obj As New clsPlanOperativoAnual
                Dim dt As Data.DataTable
                dt = obj.POA_ConsultarProgPresupuestalxCategoria(Me.ddlCategoria.SelectedValue)
                'Me.txtdescripcion.Text = dt.Rows(0).Item("descripcion_cco")
                If dt.Rows.Count > 0 Then
                    Me.lblmensaje_cco.Text = ""
                    Me.ddlProgPresupuestal.SelectedValue = dt.Rows(0).Item("codigo_ppr")
                Else
                    Me.lblmensaje_cco.Text = "La Categoria no cuenta con un Programa Presupuestal Configurado"
                    Me.aviso_cco_per.Attributes.Add("class", "mensajeError")
                End If

            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub ddlCategoria_cco_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlCategoria_cco.SelectedIndexChanged
        Try
            Me.aviso_cco_per.Attributes.Remove("class")
            If Me.ddlCategoria_cco.SelectedValue = 0 Then
                Me.ddlprogpresupuestal_cco.SelectedValue = 0
            Else
                Dim obj As New clsPlanOperativoAnual
                Dim dt As Data.DataTable
                dt = obj.POA_ConsultarProgPresupuestalxCategoria(Me.ddlCategoria_cco.SelectedValue)
                'Me.txtdescripcion.Text = dt.Rows(0).Item("descripcion_cco")
                If dt.Rows.Count > 0 Then
                    Me.lblmensaje_cco.Text = ""
                    Me.ddlprogpresupuestal_cco.SelectedValue = dt.Rows(0).Item("codigo_ppr")
                Else
                    Me.lblmensaje_cco.Text = "La Categoria no cuenta con un Programa Presupuestal Configurado"
                    Me.aviso_cco_per.Attributes.Add("class", "mensajeError")
                End If

            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub


End Class
