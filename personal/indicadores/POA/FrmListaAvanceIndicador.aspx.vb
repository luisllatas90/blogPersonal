Partial Class indicadores_POA_FrmListaAvanceIndicador
    Inherits System.Web.UI.Page

    Dim UltimoActividad As String = String.Empty
    Dim FilaActividad As Integer = -1

    Dim UltimoObjetivo As String = String.Empty
    Dim FilaObjetivo As Integer = -1

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            Call wf_cargarPEI()
            Call wf_cargarEjercicioPresupuestal()
            Call wf_CargaPoas("%", "%")
            Call wf_cargar_tipoActividad("%")
        End If
    End Sub

    Sub wf_cargarPEI()
        Dim obj As New clsPlanOperativoAnual
        Dim dtPEI As New Data.DataTable
        dtPEI = obj.ListaPeis
        Me.ddlplan.DataSource = dtPEI
        Me.ddlplan.DataTextField = "descripcion"
        Me.ddlplan.DataValueField = "codigo"
        Me.ddlplan.DataBind()
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

    Sub wf_CargaPoas(ByVal codigo_pla As String, ByVal codigo_ejp As String)
        Dim obj As New clsPlanOperativoAnual
        Dim dtt As New Data.DataTable
        dtt = obj.POA_ListaPoas_Plan_Ejercicio(codigo_pla, codigo_ejp)
        Me.ddlPoa.DataSource = dtt
        Me.ddlPoa.DataTextField = "descripcion"
        Me.ddlPoa.DataValueField = "codigo"
        Me.ddlPoa.DataBind()
        dtt.Dispose()
        obj = Nothing
        ''Me.ddlPoa.Items.Insert(0, New ListItem("--SELECCIONE--", "0"))
    End Sub

    Sub wf_cargar_tipoActividad(ByVal codigo_poa As String)
        Dim obj As New clsPlanOperativoAnual
        Dim dtt As New Data.DataTable
        dtt = obj.POA_tipoActividad(codigo_poa)
        Me.ddlActividad.DataSource = dtt
        Me.ddlActividad.DataTextField = "descripcion"
        Me.ddlActividad.DataValueField = "codigo"
        Me.ddlActividad.DataBind()
        dtt.Dispose()
        obj = Nothing
    End Sub


    Protected Sub btnBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBuscar.Click
        Me.aviso.Visible = False
        Dim dt As New Data.DataTable
        dt = CargarGrid()
        If dt.Rows.Count = 0 Then
            Me.dgv_ListaAvance.DataSource = Nothing
        Else
            Me.dgv_ListaAvance.DataSource = dt
        End If
        Me.dgv_ListaAvance.DataBind()
        dt.Dispose()

    End Sub

    Function CargarGrid() As Data.DataTable
        Dim obj As New clsPlanOperativoAnual
        Dim dtt As New Data.DataTable
        Dim ls_plan As String = IIf(ddlplan.SelectedValue = 0, "%", ddlplan.SelectedValue)
        Dim ejercicio As String = IIf(ddlEjercicio.SelectedValue = 0, "%", ddlEjercicio.SelectedValue)
        Dim poa As String = IIf(ddlPoa.SelectedValue = 0, "%", ddlPoa.SelectedValue)
        Dim actividad As String = IIf(ddlActividad.SelectedValue = 0, "%", ddlActividad.SelectedValue)

        dtt = obj.POA_avanceIndicador(IIf(ddlplan.SelectedValue = 0, "%", ddlplan.SelectedValue), _
                                     IIf(ddlEjercicio.SelectedValue = 0, "%", ddlEjercicio.SelectedValue), _
                                     IIf(ddlPoa.SelectedValue = 0, "%", ddlPoa.SelectedValue), _
                                     IIf(ddlActividad.SelectedValue = 0, "%", ddlActividad.SelectedValue))

        obj = Nothing
        Return dtt

    End Function

    Protected Sub dgv_ListaAvance_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles dgv_ListaAvance.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim row As Data.DataRowView = CType(e.Row.DataItem, Data.DataRowView)

            If UltimoActividad = row("actividad") Then
                If (dgv_ListaAvance.Rows(FilaActividad).Cells(0).RowSpan = 0) Then '1
                    dgv_ListaAvance.Rows(FilaActividad).Cells(0).RowSpan = 2  '1

                    If UltimoObjetivo = row("objetivo") Then '2
                        If (dgv_ListaAvance.Rows(FilaObjetivo).Cells(1).RowSpan = 0) Then '2
                            dgv_ListaAvance.Rows(FilaObjetivo).Cells(1).RowSpan = 2 '2
                        Else
                            dgv_ListaAvance.Rows(FilaObjetivo).Cells(1).RowSpan += 1 '2
                        End If
                        e.Row.Cells(1).Visible = False '2
                    Else
                        UltimoObjetivo = row("objetivo").ToString()
                        FilaObjetivo = e.Row.RowIndex
                    End If
                Else  '1
                    dgv_ListaAvance.Rows(FilaActividad).Cells(0).RowSpan += 1  '1

                    If UltimoObjetivo = row("objetivo") Then
                        If (dgv_ListaAvance.Rows(FilaObjetivo).Cells(1).RowSpan = 0) Then '1
                            dgv_ListaAvance.Rows(FilaObjetivo).Cells(1).RowSpan = 2  '1
                        Else
                            dgv_ListaAvance.Rows(FilaObjetivo).Cells(1).RowSpan += 1
                        End If
                        e.Row.Cells(1).Visible = False
                    Else
                        UltimoObjetivo = row("objetivo").ToString()
                        FilaObjetivo = e.Row.RowIndex
                    End If
                End If
                e.Row.Cells(0).Visible = False '1
            Else
                e.Row.VerticalAlign = VerticalAlign.Middle
                UltimoActividad = row("actividad").ToString()
                UltimoObjetivo = row("objetivo").ToString()
                FilaActividad = e.Row.RowIndex
                FilaObjetivo = e.Row.RowIndex
            End If
        End If

        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim codmeta1 As String = DataBinder.Eval(e.Row.DataItem, "codmeta1").ToString()
            Dim codmeta2 As String = DataBinder.Eval(e.Row.DataItem, "codmeta2").ToString()
            Dim codmeta3 As String = DataBinder.Eval(e.Row.DataItem, "codmeta3").ToString()
            Dim codmeta4 As String = DataBinder.Eval(e.Row.DataItem, "codmeta4").ToString()

            If codmeta1 = 0 Then
                e.Row.Cells(3).Text = "0.00"
            End If

            If codmeta2 = 0 Then
                e.Row.Cells(5).Text = "0.00"
            End If

            If codmeta3 = 0 Then
                e.Row.Cells(7).Text = "0.00"
            End If

            If codmeta4 = 0 Then
                e.Row.Cells(9).Text = "0.00"
            End If
        End If
    End Sub

    Protected Sub ddlplan_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlplan.SelectedIndexChanged
        wf_CargaPoas(Me.ddlplan.SelectedValue, Me.ddlEjercicio.SelectedValue)
        btnBuscar_Click(sender, e)
    End Sub

    Protected Sub ddlPoa_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPoa.SelectedIndexChanged
        Call wf_cargar_tipoActividad(Me.ddlPoa.SelectedValue)
        btnBuscar_Click(sender, e)
    End Sub

    Protected Sub ddlEjercicio_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlEjercicio.SelectedIndexChanged
        btnBuscar_Click(sender, e)
    End Sub

    Protected Sub dgv_ListaAvance_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles dgv_ListaAvance.RowEditing
        Try
            Dim obj As New clsPlanOperativoAnual
            Dim lrd_avance1, lrd_avance2, lrd_avance3, lrd_avance4 As TextBox

            lrd_avance1 = DirectCast(Me.dgv_ListaAvance.Rows(e.NewEditIndex).FindControl("txtavance1"), TextBox)
            lrd_avance2 = DirectCast(Me.dgv_ListaAvance.Rows(e.NewEditIndex).FindControl("txtavance2"), TextBox)
            lrd_avance3 = DirectCast(Me.dgv_ListaAvance.Rows(e.NewEditIndex).FindControl("txtavance3"), TextBox)
            lrd_avance4 = DirectCast(Me.dgv_ListaAvance.Rows(e.NewEditIndex).FindControl("txtavance4"), TextBox)

            If lrd_avance1.Text <> "" Then
                If Me.dgv_ListaAvance.DataKeys(e.NewEditIndex).Values("codmeta1") > 0 Then
                    Dim dttAvance1 As New Data.DataTable
                    dttAvance1 = obj.POA_verExiste_AvanceIndicador(Me.dgv_ListaAvance.DataKeys(e.NewEditIndex).Values("codavance1").ToString)
                    If dttAvance1.Rows(0).Item(0) = 0 Then
                        obj.POA_InsertarAvanceIndicadorPOA(lrd_avance1.Text, "T1", Request.QueryString("id"), Me.dgv_ListaAvance.DataKeys(e.NewEditIndex).Values("codmeta1").ToString)
                    Else
                        obj.POA_ActualizarAvanceIndicadorPOA(Me.dgv_ListaAvance.DataKeys(e.NewEditIndex).Values("codavance1").ToString, lrd_avance1.Text, "T1", Request.QueryString("id"), Me.dgv_ListaAvance.DataKeys(e.NewEditIndex).Values("codmeta1").ToString)
                    End If
                    'obj.POA_InsertarAvanceIndicadorPOA(Me.dgv_ListaAvance.DataKeys(e.NewEditIndex).Values("codavance1").ToString, lrd_avance1.Text, "T1", Request.QueryString("id"), Me.dgv_ListaAvance.DataKeys(e.NewEditIndex).Values("codmeta1").ToString)
                End If
            End If


            If lrd_avance2.Text <> "" Then
                If Me.dgv_ListaAvance.DataKeys(e.NewEditIndex).Values("codmeta2") > 0 Then
                    Dim dttAvance2 As New Data.DataTable
                    dttAvance2 = obj.POA_verExiste_AvanceIndicador(Me.dgv_ListaAvance.DataKeys(e.NewEditIndex).Values("codavance2").ToString)
                    If dttAvance2.Rows(0).Item(0) = 0 Then
                        obj.POA_InsertarAvanceIndicadorPOA(lrd_avance2.Text, "T2", Request.QueryString("id"), Me.dgv_ListaAvance.DataKeys(e.NewEditIndex).Values("codmeta2").ToString)
                    Else
                        obj.POA_ActualizarAvanceIndicadorPOA(Me.dgv_ListaAvance.DataKeys(e.NewEditIndex).Values("codavance2").ToString, lrd_avance2.Text, "T2", Request.QueryString("id"), Me.dgv_ListaAvance.DataKeys(e.NewEditIndex).Values("codmeta2").ToString)
                    End If
                    'obj.POA_InsertarAvanceIndicadorPOA(Me.dgv_ListaAvance.DataKeys(e.NewEditIndex).Values("codavance1").ToString, lrd_avance1.Text, "T1", Request.QueryString("id"), Me.dgv_ListaAvance.DataKeys(e.NewEditIndex).Values("codmeta1").ToString)
                End If
            End If


            If lrd_avance3.Text <> "" Then
                If Me.dgv_ListaAvance.DataKeys(e.NewEditIndex).Values("codmeta3") > 0 Then
                    Dim dttAvance3 As New Data.DataTable
                    dttAvance3 = obj.POA_verExiste_AvanceIndicador(Me.dgv_ListaAvance.DataKeys(e.NewEditIndex).Values("codavance3").ToString)
                    If dttAvance3.Rows(0).Item(0) = 0 Then
                        obj.POA_InsertarAvanceIndicadorPOA(lrd_avance3.Text, "T3", Request.QueryString("id"), Me.dgv_ListaAvance.DataKeys(e.NewEditIndex).Values("codmeta3").ToString)
                    Else
                        obj.POA_ActualizarAvanceIndicadorPOA(Me.dgv_ListaAvance.DataKeys(e.NewEditIndex).Values("codavance3").ToString, lrd_avance3.Text, "T3", Request.QueryString("id"), Me.dgv_ListaAvance.DataKeys(e.NewEditIndex).Values("codmeta3").ToString)
                    End If
                    'obj.POA_InsertarAvanceIndicadorPOA(Me.dgv_ListaAvance.DataKeys(e.NewEditIndex).Values("codavance1").ToString, lrd_avance1.Text, "T1", Request.QueryString("id"), Me.dgv_ListaAvance.DataKeys(e.NewEditIndex).Values("codmeta1").ToString)
                End If
            End If


            If lrd_avance4.Text <> "" Then
                If Me.dgv_ListaAvance.DataKeys(e.NewEditIndex).Values("codmeta4") > 0 Then
                    Dim dttAvance4 As New Data.DataTable
                    dttAvance4 = obj.POA_verExiste_AvanceIndicador(Me.dgv_ListaAvance.DataKeys(e.NewEditIndex).Values("codavance4").ToString)
                    If dttAvance4.Rows(0).Item(0) = 0 Then
                        obj.POA_InsertarAvanceIndicadorPOA(lrd_avance4.Text, "T4", Request.QueryString("id"), Me.dgv_ListaAvance.DataKeys(e.NewEditIndex).Values("codmeta4").ToString)
                    Else
                        obj.POA_ActualizarAvanceIndicadorPOA(Me.dgv_ListaAvance.DataKeys(e.NewEditIndex).Values("codavance4").ToString, lrd_avance4.Text, "T4", Request.QueryString("id"), Me.dgv_ListaAvance.DataKeys(e.NewEditIndex).Values("codmeta4").ToString)
                    End If
                    'obj.POA_InsertarAvanceIndicadorPOA(Me.dgv_ListaAvance.DataKeys(e.NewEditIndex).Values("codavance1").ToString, lrd_avance1.Text, "T1", Request.QueryString("id"), Me.dgv_ListaAvance.DataKeys(e.NewEditIndex).Values("codmeta1").ToString)
                End If
            End If

            btnBuscar_Click(sender, e)
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

   
End Class
