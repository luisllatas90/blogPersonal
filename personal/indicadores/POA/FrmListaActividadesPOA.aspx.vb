
Partial Class indicadores_POA_PROTOTIPOS_Registrar_POA
    Inherits System.Web.UI.Page
    'Dim usuario, ctf As Integer

    Sub CargarPoas(ByVal plan As Integer, ByVal ejercicio As Integer, ByVal vigencia As String)
        Dim obj As New clsPlanOperativoAnual
        Dim dt As New Data.DataTable
        '4 Lista Planes Operativos Con Limite Presupuestal Asignado y Centros de Costo Asignados.
        dt = obj.POA_ListaPoasActividad(plan, ejercicio, Session("id_per"), Request.QueryString("ctf"), vigencia)
        Me.lblMensajeFormulario.Text = "Se encontraron " & dt.Rows.Count.ToString & " registro(s)."
        If dt.Rows.Count = 0 Then
            'Me.lblMensajeFormulario.Text = "No se encontraron registros"
            Me.dgvpoa.DataSource = Nothing
        Else
            Me.dgvpoa.DataSource = dt
            'Me.dgvAsignar.Columns.Item(4).Visible = False 'codigo_pla
        End If
        Me.dgvpoa.DataBind()
        dt = Nothing
    End Sub

    Function Carga_ActividadesxPOA(ByVal codigo_poa As Integer, ByVal codigo_per As Integer, ByVal ctf As Integer) As Data.DataTable
        Dim obj As New clsPlanOperativoAnual
        Dim dt As New Data.DataTable
        Me.lblfilas_dap.Text = ""
        dt = obj.ListaActividadesxPOA(codigo_poa, 0, codigo_per, ctf)
        If (Request.QueryString("id") <> Me.dgvpoa.SelectedDataKey.Item("responsable_poa").ToString) And Request.QueryString("ctf") <> 1 Then 'And Request.QueryString("ctf") <> 23 Then
            Me.btnNuevo.Visible = False
        Else
            Me.btnNuevo.Visible = True
        End If
        If dt.Rows.Count > 0 Then
            Me.lblfilas_dap.Text = "Se encontraron " & dt.Rows.Count.ToString & " registro(s)."
        End If
        'Me.lblMensajeFormulario.Text = "Se encontraron " & dtt.Rows.Count & " registro(s)."
        Return dt
    End Function

    Protected Sub btnNuevo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNuevo.Click
        Dim codigo_poa As Integer
        Dim nombre_poa As String

        If Me.dgvpoa.SelectedIndex.ToString <> -1 Then
            codigo_poa = Me.dgvpoa.DataKeys(Me.dgvpoa.SelectedIndex).Value
            nombre_poa = Me.dgvpoa.Rows(Me.dgvpoa.SelectedIndex).Cells(0).Text
            Response.Redirect("FrmMantenimientoActividadesPOA.aspx?id=" & Request.QueryString("id") & "&ctf=" & Request.QueryString("ctf") & "&codigo_poa=" & codigo_poa & "&codigo_acp=0&cb1=" & Me.ddlPlan.SelectedValue & "&cb2=" & Me.ddlEjercicio.SelectedValue & "&cb3=" & Me.ddlestado.SelectedValue & "&index_poa=" & Me.dgvpoa.SelectedIndex)
        Else
            Me.lblmensaje.Text = "Debe Seleccionar un Plan Operativo Anual."
            Me.aviso.Attributes.Add("class", "mensajeError")
        End If
    End Sub

    Sub wf_cargarPEI()
        Dim obj As New clsPlanOperativoAnual
        Dim dtPEI As New Data.DataTable
        dtPEI = obj.ListaPeis
        Me.ddlPlan.DataSource = dtPEI
        Me.ddlPlan.DataTextField = "descripcion"
        Me.ddlPlan.DataValueField = "codigo"
        Me.ddlPlan.DataBind()
        dtPEI.Dispose()
        obj = Nothing
    End Sub

    Sub wf_cargarEjercicioPresupuestal()
        Dim obj As New clsPlanOperativoAnual
        Dim dtt As New Data.DataTable
        dtt = obj.ListaEjercicio
        Me.ddlEjercicio.DataSource = dtt
        Me.ddlEjercicio.DataTextField = "descripcion"
        Me.ddlEjercicio.DataValueField = "codigo"
        Me.ddlEjercicio.DataBind()
        dtt.Dispose()
        obj = Nothing
        Me.ddlEjercicio.SelectedIndex = Me.ddlEjercicio.Items.Count - 1
    End Sub

    Protected Sub btnBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBuscar.Click
        Dim obj As New clsPlanOperativoAnual
        Dim dt As New Data.DataTable
        dt = obj.POA_ListaPoasActividad(Me.ddlPlan.SelectedValue, Me.ddlEjercicio.SelectedValue, Request.QueryString("id"), Request.QueryString("ctf"), Me.ddlestado.SelectedValue)
        Me.dgvpoa.SelectedIndex = -1
        Me.dgvpoa.DataSource = dt
        Me.dgvpoa.DataBind()
        Me.lblMensajeFormulario.Text = "Se encontraron " & dt.Rows.Count & " registro(s)."
        Me.dgvactividades.EmptyDataText = ""
        Me.dgvactividades.DataBind()
        Me.DetProgramasProyectos.Visible = False
    End Sub

    Protected Sub dgvactividades_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles dgvactividades.RowCommand
        Try
            If (e.CommandName.Equals("Select")) Then 'comprueba que sea el boton de seleccion   
                Dim seleccion As GridViewRow
                Dim codigo_acp, codigo_poa As Integer
                Dim nombre_poa As String
                seleccion = DirectCast(e.CommandSource, GridView).Rows(e.CommandArgument)
                codigo_poa = Me.dgvpoa.SelectedDataKey.Value()
                nombre_poa = Me.dgvpoa.Rows(Me.dgvpoa.SelectedIndex).Cells(0).Text
                codigo_acp = Convert.ToInt32(Me.dgvactividades.DataKeys(seleccion.RowIndex).Values("codigo_acp").ToString)
                Response.Redirect("FrmMantenimientoActividadesPOA.aspx?id=" & Request.QueryString("id") & "&ctf=" & Request.QueryString("ctf") & "&codigo_poa=" & codigo_poa & "&codigo_acp=" & codigo_acp & "&cb1=" & Me.ddlPlan.SelectedValue & "&cb2=" & Me.ddlEjercicio.SelectedValue & "&cb3=" & Me.ddlestado.SelectedValue & "&index_poa=" & Me.dgvpoa.SelectedIndex)

            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try

    End Sub

    Protected Sub dgvpoa_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgvpoa.SelectedIndexChanged
        Try
            carga_Actividades()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Sub carga_Actividades()
        Dim codigo_poa As Integer
        codigo_poa = Me.dgvpoa.SelectedDataKey.Value()

        'codigo_poa = Convert.ToInt32(dgvpoa.DataKeys(Me.dgvpoa.SelectedRow.Cells("codigo_poa").ToString))
        Dim dt As New Data.DataTable
        dt = Carga_ActividadesxPOA(codigo_poa, Request.QueryString("id"), Request.QueryString("ctf"))
        If dt.Rows.Count = 0 Then
            Me.dgvactividades.DataSource = Nothing
            Me.dgvactividades.EmptyDataText = "Plan No cuenta Con Programas y/o Proyectos Asignados"
        Else
            Me.dgvactividades.DataSource = dt
        End If
        Me.DetProgramasProyectos.Visible = True
        Me.dgvactividades.DataBind()
        'dt.Dispose()
        Me.lblmensaje.Text = ""
        Me.aviso.Attributes.Clear()
    End Sub

    Dim ingresos As Decimal = 0
    Dim egresos As Decimal = 0
    Dim disponible As Decimal = 0

    Protected Sub dgvactividades_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles dgvactividades.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim objcrm As New ClsCRM
            Dim codigo_iep As String = DataBinder.Eval(e.Row.DataItem, "codigo_iep").ToString()
            If codigo_iep <> 1 Then
                If codigo_iep = 3 Then 'Modificado fatima.vasquez 09-08-2018
                    e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFC0CB")
                    e.Row.Cells(10).Text = ""
                    e.Row.Cells(11).Text = ""
                ElseIf codigo_iep = 6 Then 'Modificado fatima.vasquez 09-08-2018
                    e.Row.BackColor = System.Drawing.Color.LightBlue
                    e.Row.Cells(7).Text = ""
                    e.Row.Cells(8).Text = ""
                    e.Row.Cells(9).Text = ""
                    e.Row.Cells(10).Text = ""
                    e.Row.Cells(11).Text = ""
                ElseIf codigo_iep = 22 Then 'Modificado fatima.vasquez 09-08-2018
                    e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#FA5858")
                    e.Row.Cells(10).Text = ""
                    e.Row.Cells(11).Text = ""
                ElseIf codigo_iep = 21 Then 'Modificado fatima.vasquez 09-08-2018
                    e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#8181F7")
                    e.Row.Cells(7).Text = ""
                    e.Row.Cells(8).Text = ""
                    e.Row.Cells(9).Text = ""
                    e.Row.Cells(10).Text = ""
                    e.Row.Cells(11).Text = ""
                Else
                    'If codigo_iep = 9 Then 'Modificado fatima.vasquez 09-08-2018
                    'e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#0B6138")
                    'Else
                    e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#C8FFC8")
                    'End If
                    e.Row.Cells(7).Text = ""
                    e.Row.Cells(8).Text = ""
                    e.Row.Cells(9).Text = ""
                    Dim tipo_actividad As String = DataBinder.Eval(e.Row.DataItem, "descripcion_tac").ToString()
                    If codigo_iep = 8 And tipo_actividad = "PROYECTO" Then
                        e.Row.Cells(11).Attributes.Add("OnClick", "javascript:ModalAdjuntar2('" & objcrm.EncrytedString64(DataBinder.Eval(e.Row.DataItem, "codigo_acp").ToString()) & "')")
                        'e.Row.Cells(11).Text = "<button onclick='fnDownload(" & objcrm.EncrytedString64(DataBinder.Eval(e.Row.DataItem, "codigo_acp").ToString()) & ");' id='BtnAdjuntar' class='btn btn-primary' runat='server'><i class='ion-android-attach'><span></span></i></button>"
                        Dim imagen As System.Web.UI.HtmlControls.HtmlImage = DirectCast(e.Row.FindControl("CerrarProy"), System.Web.UI.HtmlControls.HtmlImage)
                        Dim cerrado As Integer = DataBinder.Eval(e.Row.DataItem, "cerrado")
                        If (cerrado = 0) Then
                            imagen.Src = "../../Images/candadoabierto.png"
                            imagen.Attributes.Remove("title")
                            imagen.Attributes.Add("title", "Cerrar Proyecto")
                            imagen.Attributes.Add("OnClick", "javascript:ModalAdjuntar('" & objcrm.EncrytedString64(DataBinder.Eval(e.Row.DataItem, "codigo_acp").ToString()) & "')")
                            imagen.Attributes.Add("cp", objcrm.EncrytedString64(DataBinder.Eval(e.Row.DataItem, "codigo_acp").ToString()))
                        Else
                            imagen.Src = "../../Images/candadocerrado.png"
                            imagen.Attributes.Remove("title")
                            imagen.Attributes.Add("title", "Proyecto Cerrado")
                            imagen.Attributes.Add("cp", objcrm.EncrytedString64(DataBinder.Eval(e.Row.DataItem, "codigo_acp").ToString()))
                        End If

                    Else
                        e.Row.Cells(10).Text = ""
                        e.Row.Cells(11).Text = ""
                        'e.Row.Cells(10).Attributes.Remove("OnClick")
                        e.Row.Cells(11).Attributes.Remove("OnClick")
                    End If
                End If
        Else
            e.Row.Cells(10).Text = ""
            e.Row.Cells(11).Text = ""
        End If
            '------OCULTAR CUANDO TEXTO CONTIENE NOMBRE PLANILLA
            Dim nombre_actividad As String = DataBinder.Eval(e.Row.DataItem, "resumen_acp").ToString()
            'BUSCA LA POSISCION DE LA PALABRA SI LA ENCUENTRA ES MAYOR A 0
            If InStr(nombre_actividad, "PLANILLA") > 0 Then
                e.Row.Cells(7).Text = ""
                e.Row.Cells(8).Text = ""
                e.Row.Cells(9).Text = ""
                e.Row.Cells(10).Text = ""
                e.Row.Cells(11).Text = ""
                'e.Row.Cells(10).Attributes.Remove("OnClick")
                e.Row.Cells(11).Attributes.Remove("OnClick")
            End If
            '--------FIN OCULTAR
            ' DIFERENTE A ADMINISTRADOR Y DECANO DE FACULTAD
            'If Request.QueryString("ctf") <> 1 And Request.QueryString("ctf") <> 23 Then
            'DIFERENTE A RESPONSABLE DEL POA
            If Session("id_per") = Me.dgvpoa.SelectedDataKey.Item("responsable_poa").ToString Or Request.QueryString("ctf") = 1 Then

            Else
                e.Row.Cells(9).Text = ""
                e.Row.Cells(10).Text = ""
                e.Row.Cells(11).Text = ""
                'e.Row.Cells(10).Attributes.Remove("OnClick")
                e.Row.Cells(11).Attributes.Remove("OnClick")
            End If
            'End If

            '================ PARA FOOTER DE GRID ============================================
            ingresos += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "ingresos_acp"))
            egresos += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "egresos_acp"))

            disponible = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "limite_egreso"))

            '========= Pintar Fila Editada Al Regresar ==============
            If Me.hd_seleccion_codigoacp.Value = e.Row.DataItem("codigo_acp") Then
                e.Row.Font.Bold = True
                e.Row.Font.Italic = True
            End If
            '========= Fin Pintar Fila Editada Al Regresar ==============

        ElseIf e.Row.RowType = DataControlRowType.Footer Then
            e.Row.Cells(1).Text = "TOTALES "
            e.Row.Cells(1).HorizontalAlign = HorizontalAlign.Center
            e.Row.Cells(1).ColumnSpan = 3
            e.Row.Cells(2).Visible = False
            e.Row.Cells(3).Visible = False
            e.Row.Cells(1).ForeColor = Drawing.Color.Blue

            e.Row.Cells(4).Text = FormatNumber(ingresos.ToString(), 2)
            e.Row.Cells(4).HorizontalAlign = HorizontalAlign.Right
            e.Row.Cells(4).ForeColor = Drawing.Color.Blue

            e.Row.Cells(5).Text = FormatNumber(egresos.ToString(), 2)
            e.Row.Cells(5).HorizontalAlign = HorizontalAlign.Right
            e.Row.Cells(5).ForeColor = Drawing.Color.Blue

            e.Row.Cells(6).Text = "PRESUPUESTO DISPONIBLE (S/.) :  " & FormatNumber(disponible, 2).ToString
            e.Row.Cells(6).HorizontalAlign = HorizontalAlign.Center
            e.Row.Cells(6).ColumnSpan = 4
            e.Row.Cells(7).Visible = False
            e.Row.Cells(8).Visible = False
            e.Row.Cells(9).Visible = False
            e.Row.Cells(6).ForeColor = Drawing.Color.Green
            e.Row.Font.Bold = True
            '================ FIN FOOTER DE GRID ============================================
        End If
    End Sub

    Protected Sub dgvactividades_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles dgvactividades.RowDeleting
        Try
            Dim fila, Codigo_acp As Integer
            Dim mensaje As String
            Dim obj As New clsPlanOperativoAnual
            fila = e.RowIndex
            Codigo_acp = Me.dgvactividades.DataKeys(fila).Value
            mensaje = obj.EliminarActividadPoa(Codigo_acp, Session("id_per"))
            'Response.Write(mensaje)
            carga_Actividades()
            If mensaje = "1" Then
                Me.lblmensaje.Text = "Actividad Eliminada Correctamente."
                Me.aviso.Attributes.Add("class", "mensajeEliminado")
            ElseIf mensaje = 0 Then
                Me.lblmensaje.Text = "No se pudo Eliminar Actividad"
                Me.aviso.Attributes.Add("class", "mensajeError")
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub dgvactividades_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles dgvactividades.RowEditing
        Try
            Dim fila, Codigo_acp As Integer
            Dim mensaje As String
            Dim obj As New clsPlanOperativoAnual
            fila = e.NewEditIndex
            Codigo_acp = Me.dgvactividades.DataKeys(fila).Value

            ''Verifica si han seleccionado la opcion Requiere_pto y los importes de ingreso o egreso sean mayores a 0
            Dim valor As String = obj.POA_verificarPtoyLimites(Codigo_acp)
            If valor = "1" Then
                carga_Actividades()
                Me.lblmensaje.Text = "No se pudo Enviar a Evaluación, Revise los Importes de Ingreso y Egreso"
                Me.aviso.Attributes.Add("class", "mensajeError")
                'Exit Sub
            Else
                Dim codigo_poa As Integer
                codigo_poa = Me.dgvpoa.DataKeys(Me.dgvpoa.SelectedIndex).Value
                '==================Agregado fatima.vasquez 09-08-2018=======================
                Dim tipo_cco As Integer = obj.POA_verificarCentroCosto(codigo_poa, Codigo_acp)
                Dim codigo_iep As Integer
                If tipo_cco = 1 Then 'Programa de Educación Continua para aprobación de Marketing
                    codigo_iep = 21 'Dirección de Marketing
                Else
                    codigo_iep = 6 'Dirección de Planificación
                End If
                'mensaje = obj.InstanciaEstadoActividad(Codigo_acp, 6, Request.QueryString("id")) 
                mensaje = obj.InstanciaEstadoActividad(Codigo_acp, codigo_iep, Request.QueryString("id"))
                '==================Fin fatima.vasquez 09-08-2018=============================
                carga_Actividades()
                If mensaje = "1" Then
                    Me.lblmensaje.Text = "Actividad Enviada a Evaluación."
                    Me.aviso.Attributes.Add("class", "mensajeExito")
                    'Actualizo Estado de Observaciones de la Actividad a 1 Como Resueltas
                    'Comento Usuario no debe actualizar Observacion
                    'obj.POA_ActualizarObservacion(Codigo_acp, "", Request.QueryString("id"), 1)
                ElseIf mensaje = "0" Then
                    Me.lblmensaje.Text = "No se pudo Enviar a Evaluación"
                    Me.aviso.Attributes.Add("class", "mensajeError")
                ElseIf mensaje = "2" Then
                    Me.lblmensaje.Text = "No se pudo Enviar, Actividad No Tiene Asignado el Alineamiento, Verificar."
                    Me.aviso.Attributes.Add("class", "mensajeError")
                ElseIf mensaje = "3" Then
                    Me.lblmensaje.Text = "No se pudo Enviar, Actividad No Tiene Asignado Objetivos, Verificar."
                    Me.aviso.Attributes.Add("class", "mensajeError")
                ElseIf mensaje = "4" Then
                    Me.lblmensaje.Text = "No se pudo Enviar, Programa/Proyecto No Tiene Asignado Actividades, Verificar."
                    Me.aviso.Attributes.Add("class", "mensajeError")
                ElseIf mensaje = "5" Then
                    Me.lblmensaje.Text = "No se pudo Enviar, Programa/Proyecto No Tiene Ninguna Actividad con Requerimiento de Presupuesto"
                    Me.aviso.Attributes.Add("class", "mensajeError")
                End If
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

   
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("id_per") = "" Or Request.QueryString("id") = "" Then
            Response.Redirect("../../../sinacceso.html")
        End If

        Try
            If Not IsPostBack Then
                'Dim codigo_poa As Integer
                Dim dt As New Data.DataTable
                wf_cargarPEI()
                wf_cargarEjercicioPresupuestal()
                'usuario = Request.QueryString("id")
                'ctf = Request.QueryString("ctf")

                'Me.lblMensajeFormulario.Text = ""
                CargarPoas(Me.ddlPlan.SelectedValue, Me.ddlEjercicio.SelectedValue, Me.ddlestado.SelectedValue)
                If Request.QueryString("accion") = "C" Then
                    If Request.QueryString("cb1") <> "" Then
                        Me.ddlPlan.SelectedValue = Request.QueryString("cb1")
                    End If
                    If Request.QueryString("cb2") <> "" Then
                        Me.ddlEjercicio.SelectedValue = Request.QueryString("cb2")
                    End If
                    If Request.QueryString("cb2") <> "" Then
                        Me.ddlestado.SelectedValue = Request.QueryString("cb3")
                    End If
                    CargarPoas(Me.ddlPlan.SelectedValue, Me.ddlEjercicio.SelectedValue, Me.ddlestado.SelectedValue)
                    If Me.dgvpoa.Rows.Count > 0 Then
                        If Request.QueryString("index_poa") <> "" Then
                            Me.dgvpoa.SelectedIndex = Request.QueryString("index_poa")
                            Me.hd_seleccion_codigoacp.Value = Request.QueryString("index_acp")
                            carga_Actividades()
                            Me.hd_seleccion_codigoacp.Value = -1
                        End If
                    End If
                Else
                    CargarPoas(Me.ddlPlan.SelectedValue, Me.ddlEjercicio.SelectedValue, Me.ddlestado.SelectedValue)
                End If
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub ddlPlan_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPlan.SelectedIndexChanged
        btnBuscar_Click(sender, e)
    End Sub

    Protected Sub ddlEjercicio_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlEjercicio.SelectedIndexChanged
        btnBuscar_Click(sender, e)
    End Sub

    Protected Sub ddlestado_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlestado.SelectedIndexChanged
        btnBuscar_Click(sender, e)
    End Sub

    Protected Sub ibtnCerrarProy_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Try
            'If (Session("estadoPto") = "REGISTRO PRESUPUESTO" And Session("instanciaPto") = "RESPONSABLE") Or _
            '    (Session("estadoPto") = "OBSERVACION DIR. AREA" And Session("instanciaPto") = "RESPONSABLE" And hddEvaPto.Value = 0) Or _
            '    (Session("estadoPto") = "OBSERVACION FINAL" And Session("instanciaPto") = "DIRECCION DE AREA" And hddEvaPto.Value = 0) Then
            '    ClientScript.RegisterStartupScript(Me.GetType, "Mostrar", "TdNuevo.style.visibility='visible'", True)
            'Else
            '    ClientScript.RegisterStartupScript(Me.GetType, "Mostrar", "TdNuevo.style.visibility='hidden';", True)
            'End If
            'Dim obj As New ClsPresupuesto
            'Dim codigo_dap, codigo_ejp, codigo_cco As Integer
            'Dim combo As DropDownList
            'Dim row As GridViewRow
            'Dim ibtnMover As ImageButton
            'ibtnMover = sender
            'row = ibtnMover.NamingContainer
            'combo = DirectCast(Me.gvCabecera.Rows(row.RowIndex).FindControl("cboMesIni"), DropDownList)
            'If combo.SelectedIndex <> -1 Then

            '    If combo.SelectedValue = CInt(Me.gvCabecera.DataKeys(row.RowIndex).Item("fecini_dap").Substring(3, 2)) Then
            '        ClientScript.RegisterStartupScript(Me.GetType, "mensaje", "alert('No se Pudo mover Actividad, Mes De Inicio es El Mismo');", True)
            '    Else
            '        codigo_dap = Me.gvCabecera.DataKeys(row.RowIndex).Item("codigo_Dap")
            '        codigo_ejp = Me.gvCabecera.DataKeys(row.RowIndex).Item("codigo_Ejp")
            '        codigo_cco = Me.gvCabecera.DataKeys(row.RowIndex).Item("codigo_cco")

            '        Dim mensajerpta As String = obj.POA_MoverActividadPOA(codigo_dap, codigo_ejp, codigo_cco, combo.SelectedValue)

            '        If mensajerpta = "0" Then
            '            Me.gvDetalle.DataBind()
            '            ClientScript.RegisterStartupScript(Me.GetType, "Mensaje", "alert('No se Pudo Mover Actividad, Contactarse con Área de Desarrollo');", True)

            '        Else
            '            gvCabecera.DataBind()
            '            gvDetalle.DataBind()
            '            ClientScript.RegisterStartupScript(Me.GetType, "mensaje", "alert('" & mensajerpta & "');", True)
            '        End If
            '    End If
            'End If
            'Response.Write("<script>alert('OK')</script>")
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub
End Class
