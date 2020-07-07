
Partial Class indicadores_POA_FrmListaAsignarRevision
    Inherits System.Web.UI.Page
    Dim UltimoPoa As Integer
    Dim UltimoAcp As Integer
    Dim UltimoDap As Integer
    Dim FilaPoa As Integer = -1
    Dim FilaAcp As Integer = -1
    Dim FilaDap As Integer = -1
    'Dim usuario, ctf As Integer

    Sub CargaPlanes()
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

    Sub CargaEjercicio()
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

    Sub cargatabla()
        Dim obj As New clsPlanOperativoAnual
        Me.dgvActividades.SelectedIndex = -1
        Me.dgvActividades.DataSource = obj.ListaAsignaFecha(Me.ddlplan.SelectedValue, Me.ddlEjercicio.SelectedValue, Me.ddlPoa.SelectedValue, "T")
        Me.dgvActividades.DataBind()
    End Sub

    Protected Sub form1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles form1.Load
        If Session("id_per") = "" Or Request.QueryString("id") = "" Then
            Response.Redirect("../../../sinacceso.html")
        End If

        Try
            If IsPostBack = False Then
                CargaPlanes()
                CargaEjercicio()
                'usuario = Request.QueryString("id")
                'ctf = Request.QueryString("ctf")
                'cargatabla()
                'If Request.QueryString("cb1") = "" Then

                'Else
                '    Me.ddlplan.SelectedValue = Request.QueryString("cb1")
                '    Me.ddlEjercicio.SelectedValue = Request.QueryString("cb2")
                '    CargaPoas(Me.ddlplan.SelectedValue, Me.ddlEjercicio.SelectedValue)
                '    Me.ddlPoa.SelectedValue = Request.QueryString("cb3")
                '    'Me.ddlestado.SelectedValue = Request.QueryString("cb4")
                'End If
                'If Request.QueryString("msj") = "R" Then
                '    Me.lblrpta.Text = "Datos Registrados Correctamente"
                '    Me.aviso.Attributes.Add("class", "mensajeExito")
                'End If
                cargatabla()
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Sub CargaPoas(ByVal codigo_pla As Integer, ByVal codigo_ejp As Integer)
        Dim obj As New clsPlanOperativoAnual
        Dim dtt As New Data.DataTable
        dtt = obj.ListaPoasxInstanciaEstado(codigo_pla, codigo_ejp, "AF",Request.QueryString("id"),Request.QueryString("ctf"))

        Me.ddlPoa.DataSource = dtt
        Me.ddlPoa.DataTextField = "descripcion"
        Me.ddlPoa.DataValueField = "codigo"
        Me.ddlPoa.DataBind()
        dtt.Dispose()
        obj = Nothing
        'Me.ddlPoa.Items.Insert(0, New ListItem("--SELECCIONE--", "0"))
        'Me.ddlPoa.SelectedValue = 0
    End Sub

    Protected Sub ddlplan_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlplan.SelectedIndexChanged
        CargaPoas(Me.ddlplan.SelectedValue, Me.ddlEjercicio.SelectedValue)
        Me.aviso.Attributes.Clear()
        Me.lblrpta.Text = ""
    End Sub

    Protected Sub ddlEjercicio_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlEjercicio.SelectedIndexChanged
        CargaPoas(Me.ddlplan.SelectedValue, Me.ddlEjercicio.SelectedValue)
        Me.aviso.Attributes.Clear()
        Me.lblrpta.Text = ""
    End Sub

    Protected Sub btnBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBuscar.Click
        cargatabla()
        Me.aviso.Attributes.Clear()
        Me.lblrpta.Text = ""
    End Sub

    Protected Sub dgvActividades_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles dgvActividades.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim row As Data.DataRowView = CType(e.Row.DataItem, Data.DataRowView)

            If UltimoPoa = row("codigo_poa") Then
                If (dgvActividades.Rows(FilaPoa).Cells(1).RowSpan = 0) Then '1
                    dgvActividades.Rows(FilaPoa).Cells(1).RowSpan = 2  '1

                    If UltimoAcp = row("codigo_acp") Then '2
                        If (dgvActividades.Rows(FilaAcp).Cells(4).RowSpan = 0) Then '2
                            dgvActividades.Rows(FilaAcp).Cells(4).RowSpan = 2 '2
                        Else
                            dgvActividades.Rows(FilaAcp).Cells(4).RowSpan += 1 '2

                        End If
                        e.Row.Cells(4).Visible = False '2
                    Else
                        'Cierra celda Actividad
                        UltimoAcp = row("codigo_acp").ToString()
                        FilaAcp = e.Row.RowIndex
                    End If
                Else  '1
                    dgvActividades.Rows(FilaPoa).Cells(1).RowSpan += 1  '1
                    If UltimoAcp = row("codigo_acp") Then
                        If (dgvActividades.Rows(FilaAcp).Cells(4).RowSpan = 0) Then '1
                            dgvActividades.Rows(FilaAcp).Cells(4).RowSpan = 2  '1

                        Else
                            dgvActividades.Rows(FilaAcp).Cells(4).RowSpan += 1

                        End If
                        e.Row.Cells(4).Visible = False
                    Else
                        UltimoAcp = row("codigo_acp").ToString()
                        FilaAcp = e.Row.RowIndex

                    End If

                End If
                e.Row.Cells(1).Visible = False '1

            Else
                e.Row.VerticalAlign = VerticalAlign.Middle
                UltimoPoa = row("codigo_poa").ToString()
                UltimoAcp = row("codigo_acp").ToString()
                FilaPoa = e.Row.RowIndex
                FilaAcp = e.Row.RowIndex

            End If
        End If

    End Sub

    'Protected Sub dgvActividades_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles dgvActividades.RowCommand
    '    Try
    '        If (e.CommandName.Equals("Select")) Then
    '            Me.lblrpta.Text = ""
    '            Me.aviso.Attributes.Clear()
    '            Dim seleccion As GridViewRow
    '            Dim obj As New clsPlanOperativoAnual
    '            Dim fecfin_dap As String
    '            '1. Obtengo la linea del gridview que fue cliqueada
    '            seleccion = DirectCast(e.CommandSource, GridView).Rows(e.CommandArgument)
    '            '2. Obtengo el datakey de la linea que donde está el boton que cliqueé
    '            fecfin_dap = Me.dgvActividades.DataKeys(seleccion.RowIndex).Values("fecfin_dap")
    '            'Response.Redirect("FrmMantenimientoAsignarFecha.aspx?id=" & Request.QueryString("id") & "&ctf=" & Request.QueryString("ctf") & "&cdap=" & codigo_dap & "&cb1=" & Me.ddlplan.SelectedValue & "&cb2=" & Me.ddlEjercicio.SelectedValue & "&cb3=" & Me.ddlPoa.SelectedValue)
    '            'Response.Write("<script>alert('" & Me.dgvActividades.Rows(seleccion.RowIndex).Cells(8).Text & "')</script>")
    '            Dim caja As TextBox
    '            caja = DirectCast(seleccion.FindControl("txthito_dap"), TextBox)
    '            caja.Text = Me.dgvActividades.Rows(seleccion.RowIndex).Cells(8).Text

    '        End If
    '    Catch ex As Exception
    '        Response.Write(ex.Message)
    '    End Try
    'End Sub


    Protected Sub dgvActividades_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles dgvActividades.RowEditing
        Try
            Dim obj As New clsPlanOperativoAnual
            Dim dt As New Data.DataTable
            Dim codigo_dap As Integer
            Dim caja As TextBox
            Me.lblrpta.Text = ""
            Me.aviso.Attributes.Clear()

            caja = DirectCast(Me.dgvActividades.Rows(e.NewEditIndex).FindControl("txthito_dap"), TextBox)

            If caja.Text <> "" Then ' Caja de Texto Diferente a vacia
                If IsDate(caja.Text) = True Then
                    If CDate(caja.Text) >= CDate(Me.dgvActividades.Rows(e.NewEditIndex).Cells(7).Text) Then
                        'codigo de detalleActividad a Actualizar
                        codigo_dap = Me.dgvActividades.DataKeys(e.NewEditIndex).Value.ToString()
                        'Actualiza
                        dt = obj.AsignaFechaRevision(codigo_dap, caja.Text, Request.QueryString("id"))
                        cargatabla()
                        If dt.Rows(0).Item("mensaje") = 0 Then
                            Me.lblrpta.Text = "Error, No se Pudo Asignar Fecha de Revisión."
                            Me.aviso.Attributes.Add("class", "mensajeError")
                        ElseIf dt.Rows(0).Item("mensaje") = 1 Then
                            Me.lblrpta.Text = "Fecha Asignada Correctamente."
                            Me.aviso.Attributes.Add("class", "mensajeExito")
                        End If
                    Else
                        Me.lblrpta.Text = "Fecha de Revisión debe ser Mayor a Fecha de Inicio."
                        Me.aviso.Attributes.Add("class", "mensajeError")
                    End If
                Else
                    Me.lblrpta.Text = "Ingresar Fecha con Formato Valido 'dd/mm/aaaa'."
                    Me.aviso.Attributes.Add("class", "mensajeError")
                End If
            Else
                Me.lblrpta.Text = "Ingresar Fecha de Revisión."
                Me.aviso.Attributes.Add("class", "mensajeError")
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try

    End Sub

End Class
