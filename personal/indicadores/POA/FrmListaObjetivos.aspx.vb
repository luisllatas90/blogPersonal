Partial Class indicadores_POA_PROTOTIPOS_Registrar_POA
    Inherits System.Web.UI.Page
    Dim UltimoPoa As String = String.Empty
    Dim FilaPoa As Integer = -1

    Dim UltimoTac As String = String.Empty
    Dim FilaTac As Integer = -1

    Dim UltimoAcp As String = String.Empty
    Dim FilaAcp As Integer = -1

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("id_per") = "" Then
            Response.Redirect("../../../sinacceso.html")
        End If

        If IsPostBack = False Then

            Call wf_cargarPEI()
            Call wf_cargarEjercicioPresupuestal()
            Call wf_cargarTipoActividad()

            'MsgBox(Request.QueryString("plan"))
            'MsgBox(Request.QueryString("ejercicio"))
            'MsgBox(Request.QueryString("tipoActividad"))


            ddlPlan.SelectedValue = Request.QueryString("plan")
            ddlEjercicio.SelectedValue = Request.QueryString("ejercicio")
            ddl_tipoActividad.SelectedValue = Request.QueryString("tipoActividad")

            'Call wf_cargarPEI()
            'Call wf_cargarEjercicioPresupuestal()
            'Call wf_cargarTipoActividad()

            btnBuscar_Click(sender, e)
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

    Sub wf_cargarTipoActividad()
        Dim obj As New clsPlanOperativoAnual
        Dim dtt As New Data.DataTable
        dtt = obj.POA_ListaTipoActividad

        Me.ddl_tipoActividad.DataSource = dtt
        Me.ddl_tipoActividad.DataTextField = "descripcion"
        Me.ddl_tipoActividad.DataValueField = "codigo"
        Me.ddl_tipoActividad.DataBind()

        dtt.Dispose()
        obj = Nothing

    End Sub

    Protected Sub btnBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBuscar.Click
        Me.lblMensajeFormulario.Text = ""
        Me.aviso.Visible = False
        Dim dt As New Data.DataTable
        dt = CargarGrid()
        If dt.Rows.Count = 0 Then
            Me.lblMensajeFormulario.Text = "No se encontraron registros"
            Me.gvwObjetivos.DataSource = Nothing
        Else
            Me.gvwObjetivos.DataSource = dt
            Me.gvwObjetivos.Columns.Item(6).Visible = False 'codigo_pla
            Me.gvwObjetivos.Columns.Item(7).Visible = False 'codigo_ejp
            Me.gvwObjetivos.Columns.Item(8).Visible = False 'codigo_arp
        End If
        Me.gvwObjetivos.DataBind()
        dt.Dispose()
    End Sub


    Function CargarGrid() As Data.DataTable
        Dim obj As New clsPlanOperativoAnual
        Dim dtt As New Data.DataTable

        dtt = obj.POA_ListarPOASProgramas(IIf(Me.ddlPlan.SelectedIndex = 0, "%", Me.ddlPlan.SelectedValue.ToString), _
                                             IIf(Me.ddlEjercicio.SelectedIndex = 0, "%", Me.ddlEjercicio.SelectedValue.ToString), _
                                             "%", _
                                             IIf(Me.ddl_tipoActividad.SelectedIndex = 0, "%", Me.ddl_tipoActividad.SelectedValue.ToString), _
                                             "1")


        obj = Nothing
        Me.lblMensajeFormulario.Text = "Se encontraron " & dtt.Rows.Count & " registro(s)."
        Return dtt

    End Function


    Protected Sub gvwPOA_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvwObjetivos.RowDataBound
        ''If e.Row.RowType = DataControlRowType.DataRow Then
        ''    Dim row As Data.DataRowView = CType(e.Row.DataItem, Data.DataRowView)
        ''    If LastNombrePOA = row("nombre_poa") Then
        ''        If (gvwObjetivos.Rows(CurrentRow).Cells(0).RowSpan = 0) Then
        ''            gvwObjetivos.Rows(CurrentRow).Cells(0).RowSpan = 2
        ''        Else
        ''            gvwObjetivos.Rows(CurrentRow).Cells(0).RowSpan += 1
        ''        End If
        ''        e.Row.Cells.RemoveAt(0)
        ''    Else
        ''        e.Row.VerticalAlign = VerticalAlign.Middle
        ''        LastNombrePOA = row("nombre_poa").ToString()
        ''        CurrentRow = e.Row.RowIndex
        ''    End If
        ''End If

        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim row As Data.DataRowView = CType(e.Row.DataItem, Data.DataRowView)

            If UltimoPoa = row("nombre_poa") Then
                If (gvwObjetivos.Rows(FilaPoa).Cells(0).RowSpan = 0) Then '1
                    gvwObjetivos.Rows(FilaPoa).Cells(0).RowSpan = 2  '1

                    If UltimoTac = row("actividad") Then '2
                        If (gvwObjetivos.Rows(FilaTac).Cells(1).RowSpan = 0) Then '2
                            gvwObjetivos.Rows(FilaTac).Cells(1).RowSpan = 2 '2

                            'Combinar Actividad
                            If UltimoAcp = row("resumen_acp") Then
                                If (gvwObjetivos.Rows(FilaAcp).Cells(2).RowSpan = 0) Then
                                    gvwObjetivos.Rows(FilaAcp).Cells(2).RowSpan = 2
                                Else
                                    gvwObjetivos.Rows(FilaAcp).Cells(2).RowSpan += 1
                                End If
                                e.Row.Cells(2).Visible = False
                            Else
                                UltimoAcp = row("resumen_acp").ToString()
                                FilaAcp = e.Row.RowIndex
                            End If
                        Else
                            gvwObjetivos.Rows(FilaTac).Cells(1).RowSpan += 1 '2

                            'Combinar Actividad
                            If UltimoAcp = row("resumen_acp") Then
                                If (gvwObjetivos.Rows(FilaAcp).Cells(2).RowSpan = 0) Then
                                    gvwObjetivos.Rows(FilaAcp).Cells(2).RowSpan = 2
                                Else
                                    gvwObjetivos.Rows(FilaAcp).Cells(2).RowSpan += 1
                                End If
                                e.Row.Cells(2).Visible = False
                            Else
                                UltimoAcp = row("resumen_acp").ToString()
                                FilaAcp = e.Row.RowIndex
                            End If
                        End If
                        e.Row.Cells(1).Visible = False '2
                    Else
                        'Cierra celda Tipo Actividad
                        UltimoTac = row("actividad").ToString()
                        FilaTac = e.Row.RowIndex
                        'Cierra celda Actividad
                        UltimoAcp = row("resumen_acp").ToString()
                        FilaAcp = e.Row.RowIndex

                    End If
                Else  '1
                    gvwObjetivos.Rows(FilaPoa).Cells(0).RowSpan += 1  '1
                    If UltimoTac = row("actividad") Then
                        If (gvwObjetivos.Rows(FilaTac).Cells(1).RowSpan = 0) Then '1
                            gvwObjetivos.Rows(FilaTac).Cells(1).RowSpan = 2  '1
                            'Combinar Actividad
                            If UltimoAcp = row("resumen_acp") Then
                                If (gvwObjetivos.Rows(FilaAcp).Cells(2).RowSpan = 0) Then
                                    gvwObjetivos.Rows(FilaAcp).Cells(2).RowSpan = 2
                                Else
                                    gvwObjetivos.Rows(FilaAcp).Cells(2).RowSpan += 1
                                End If
                                e.Row.Cells(2).Visible = False
                            Else
                                UltimoAcp = row("resumen_acp").ToString()
                                FilaAcp = e.Row.RowIndex

                            End If
                        Else
                            gvwObjetivos.Rows(FilaTac).Cells(1).RowSpan += 1
                            'Combinar Actividad
                            If UltimoAcp = row("resumen_acp") Then
                                If (gvwObjetivos.Rows(FilaAcp).Cells(2).RowSpan = 0) Then
                                    gvwObjetivos.Rows(FilaAcp).Cells(2).RowSpan = 2
                                Else
                                    gvwObjetivos.Rows(FilaAcp).Cells(2).RowSpan += 1
                                End If
                                e.Row.Cells(2).Visible = False
                            Else
                                UltimoAcp = row("resumen_acp").ToString()
                                FilaAcp = e.Row.RowIndex

                            End If
                        End If
                        e.Row.Cells(1).Visible = False
                    Else
                        UltimoTac = row("actividad").ToString()
                        FilaTac = e.Row.RowIndex
                        UltimoAcp = row("resumen_acp").ToString()
                        FilaAcp = e.Row.RowIndex

                    End If
                End If
                e.Row.Cells(0).Visible = False '1
            Else
                e.Row.VerticalAlign = VerticalAlign.Middle
                UltimoPoa = row("nombre_poa").ToString()
                UltimoTac = row("actividad").ToString()
                UltimoAcp = row("resumen_acp").ToString()
                FilaPoa = e.Row.RowIndex
                FilaTac = e.Row.RowIndex
                FilaAcp = e.Row.RowIndex
            End If
        End If
    End Sub

    Protected Sub gvwPOA_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvwObjetivos.SelectedIndexChanged
        Dim ls_tipo As String = "Editar"
        Dim li_codigo_acp As Integer = Me.gvwObjetivos.DataKeys.Item(Me.gvwObjetivos.SelectedIndex).Values(2)
        Dim ls_codigo_pobj As Integer = Me.gvwObjetivos.DataKeys.Item(Me.gvwObjetivos.SelectedIndex).Values(3)

        Dim plan As String = ddlPlan.SelectedValue.ToString
        Dim ejercicio As String = ddlEjercicio.SelectedValue.ToString
        Dim tipoActividad As String = ddl_tipoActividad.SelectedValue.ToString

        Response.Redirect("FrmMantenimientoObjetivos.aspx?id=" & Request.QueryString("id") & "&ctf=" & Request.QueryString("ctf") & "&codigo_acp=" & li_codigo_acp & _
                          "&codigo_pobj=" & ls_codigo_pobj & "&tipo=" & ls_tipo & "&plan=" & plan & "&ejercicio=" & ejercicio & "&tipoActividad=" & tipoActividad)

    End Sub

    Protected Sub btnNuevo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNuevo.Click
        If ddlPlan.SelectedItem.Value = 0 Then
            Response.Write("<script>alert('Debe Seleccionar un POA')</script>")
            Return
        End If

        Dim ls_tipo As String = "Nuevo"
        Dim li_codigo_plan As Integer = ddlPlan.SelectedItem.Value
        Response.Redirect("FrmMantenimientoObjetivos.aspx?id=" & Request.QueryString("id") & "&ctf=" & Request.QueryString("ctf") & "&codigo_plan=" & li_codigo_plan & "&tipo=" & ls_tipo)
    End Sub
End Class
