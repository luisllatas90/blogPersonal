
Partial Class indicadores_POA_frmAsignaMetasPOA
    Inherits System.Web.UI.Page

    Dim LastProyecto As String = String.Empty
    Dim CurrentRow As Integer = -1

    Dim LastTermino As String = String.Empty
    Dim CurrentRowTermino As Integer = -1

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Session("id_per") = "" Or Request.QueryString("id") = "" Then
                Response.Redirect("../../../sinacceso.html")
            End If

            If IsPostBack = False Then
                Call CargaPlanes()
                Call CargaEjercicio()
                If Me.ddlplan.SelectedValue <> 0 And Me.ddlEjercicio.SelectedValue <> 0 Then
                    Call CargaPoas(Me.ddlplan.SelectedValue, Me.ddlEjercicio.SelectedValue)
                End If
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Sub CargaPlanes()
        Dim obj As New clsPlanOperativoAnual
        Dim dtPEI As New Data.DataTable
        'dtPEI = obj.ListaPeis
        dtPEI = obj.POA_ListaPeisVigentesMetas(Request.QueryString("id"), Request.QueryString("ctf"))
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

    Sub CargaPoas(ByVal codigo_pla As Integer, ByVal codigo_ejp As Integer)
        Dim obj As New clsPlanOperativoAnual
        Dim dtt As New Data.DataTable
        'dtt = obj.ListaPoasxInstanciaEstado(codigo_pla, codigo_ejp, "PTO", Request.QueryString("id"), Request.QueryString("ctf"))
        dtt = obj.POA_ListaPoasxPEIyEjercicio(Request.QueryString("id"), Request.QueryString("ctf"), codigo_pla, codigo_ejp)
        Me.ddlPoa.Items.Clear()
        If dtt.Rows.Count > 0 Then
            Me.ddlPoa.DataSource = dtt
            Me.ddlPoa.DataTextField = "descripcion"
            Me.ddlPoa.DataValueField = "codigo"
            Me.ddlPoa.DataBind()
            If dtt.Rows.Count > 1 Then
                Me.ddlPoa.Items.Add(New ListItem("TODOS", "0"))
            End If
        Else
            Me.ddlPoa.Items.Add(New ListItem("--SELECCIONE--", "0"))
        End If
        dtt.Dispose()
        obj = Nothing

        'Me.ddlPoa.Items.Insert(0, New ListItem("--SELECCIONE--", "0"))
    End Sub

    Protected Sub ddlplan_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlplan.SelectedIndexChanged
        CargaPoas(Me.ddlplan.SelectedValue, Me.ddlEjercicio.SelectedValue)
    End Sub

    Protected Sub ddlEjercicio_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlEjercicio.SelectedIndexChanged
        CargaPoas(Me.ddlplan.SelectedValue, Me.ddlEjercicio.SelectedValue)
    End Sub

    Protected Sub btnBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBuscar.Click
        Try
            ''Llenar Grilla de Pendientes por iniciar
            Dim obj As New clsPlanOperativoAnual
            Dim dtIniciar As New Data.DataTable

            dtIniciar = obj.POA_AsignarMetasPorIniciar_v1(Me.ddlPoa.SelectedValue, "1", "", Request.QueryString("id"), Request.QueryString("ctf"), Me.ddlplan.SelectedValue, Me.ddlEjercicio.SelectedValue)
            Me.dgv_termino.SelectedIndex = -1
            Me.dgv_iniciar.SelectedIndex = -1
            Me.dgv_iniciar.DataSource = dtIniciar
            Me.dgv_iniciar.DataBind()

            Dim dtTerminar As New Data.DataTable
            dtTerminar = obj.POA_AsignarMetasPorIniciar_v1(Me.ddlPoa.SelectedValue, "2", ddlEstado.SelectedValue, Request.QueryString("id"), Request.QueryString("ctf"), Me.ddlplan.SelectedValue, Me.ddlEjercicio.SelectedValue)
            Me.dgv_termino.SelectedIndex = -1
            Me.dgv_termino.DataSource = dtTerminar
            Me.dgv_termino.DataBind()

            If Me.ddlPoa.SelectedItem.Text <> "--SELECCIONE--" And dtIniciar.Rows.Count = 0 And dtTerminar.Rows.Count = 0 Then
                Me.lblmensaje.Text = "El " + Me.ddlPoa.SelectedItem.Text + " NO cuenta con Proyectos Aprobados, Cualquier duda visualizar en."
                Me.aviso1.Attributes.Add("class", "mensajeError")
                Me.aviso1.Visible = True
                Dim link As New LinkButton
                link.Text = "Resumen POA"
                link.ID = "link"
                link.Attributes.Add("href", "../../../../rptusat/?/PRIVADOS/POA/POA_Resumen&id=" + Request.QueryString("id") + "&ctf=" + Request.QueryString("ctf"))
                'Me.aviso1.Attr"<a title='Resumen POA' href="& ">Resumen Poa</a>")
                Me.aviso1.Controls.Add(link)
            Else
                Me.lblmensaje.Text = ""
                Me.aviso1.Visible = False
                Me.aviso1.Attributes.Remove("class")

            End If

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub dgv_iniciar_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles dgv_iniciar.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then

            Dim row As Data.DataRowView = CType(e.Row.DataItem, Data.DataRowView)
            If LastProyecto = row("proyecto") Then
                If (dgv_iniciar.Rows(CurrentRow).Cells(0).RowSpan = 0) Then
                    dgv_iniciar.Rows(CurrentRow).Cells(0).RowSpan = 2
                Else
                    dgv_iniciar.Rows(CurrentRow).Cells(0).RowSpan += 1
                End If
                e.Row.Cells(0).Visible = False
            Else
                e.Row.VerticalAlign = VerticalAlign.Middle
                LastProyecto = row("proyecto").ToString()
                
                CurrentRow = e.Row.RowIndex
            End If

            If row("ene").ToString() = "1" Then
                e.Row.Cells(2).BackColor = Drawing.Color.FromName("#CCFFCC")
                e.Row.Cells(2).ForeColor = Drawing.Color.FromName("#CCFFCC")
            Else
                e.Row.Cells(2).BackColor = Drawing.Color.White
                e.Row.Cells(2).ForeColor = Drawing.Color.White
            End If

            If row("feb").ToString() = "1" Then
                e.Row.Cells(3).BackColor = Drawing.Color.FromName("#CCFFCC")
                e.Row.Cells(3).ForeColor = Drawing.Color.FromName("#CCFFCC")
            Else
                e.Row.Cells(3).BackColor = Drawing.Color.White
                e.Row.Cells(3).ForeColor = Drawing.Color.White
            End If

            If row("mar").ToString() = "1" Then
                e.Row.Cells(4).BackColor = Drawing.Color.FromName("#CCFFCC")
                e.Row.Cells(4).ForeColor = Drawing.Color.FromName("#CCFFCC")
            Else
                e.Row.Cells(4).BackColor = Drawing.Color.White
                e.Row.Cells(4).ForeColor = Drawing.Color.White
            End If

            If row("abr").ToString() = "1" Then
                e.Row.Cells(5).BackColor = Drawing.Color.FromName("#CCFFCC")
                e.Row.Cells(5).ForeColor = Drawing.Color.FromName("#CCFFCC")
            Else
                e.Row.Cells(5).BackColor = Drawing.Color.White
                e.Row.Cells(5).ForeColor = Drawing.Color.White
            End If

            If row("may").ToString() = "1" Then
                e.Row.Cells(6).BackColor = Drawing.Color.FromName("#CCFFCC")
                e.Row.Cells(6).ForeColor = Drawing.Color.FromName("#CCFFCC")
            Else
                e.Row.Cells(6).BackColor = Drawing.Color.White
                e.Row.Cells(6).ForeColor = Drawing.Color.White
            End If
            If row("jun").ToString() = "1" Then
                e.Row.Cells(7).BackColor = Drawing.Color.FromName("#CCFFCC")
                e.Row.Cells(7).ForeColor = Drawing.Color.FromName("#CCFFCC")
            Else
                e.Row.Cells(7).BackColor = Drawing.Color.White
                e.Row.Cells(7).ForeColor = Drawing.Color.White
            End If
            If row("jul").ToString() = "1" Then
                e.Row.Cells(8).BackColor = Drawing.Color.FromName("#CCFFCC")
                e.Row.Cells(8).ForeColor = Drawing.Color.FromName("#CCFFCC")
            Else
                e.Row.Cells(8).BackColor = Drawing.Color.White
                e.Row.Cells(8).ForeColor = Drawing.Color.White
            End If
            If row("ago").ToString() = "1" Then
                e.Row.Cells(9).BackColor = Drawing.Color.FromName("#CCFFCC")
                e.Row.Cells(9).ForeColor = Drawing.Color.FromName("#CCFFCC")
            Else
                e.Row.Cells(9).BackColor = Drawing.Color.White
                e.Row.Cells(9).ForeColor = Drawing.Color.White
            End If
            If row("set").ToString() = "1" Then
                e.Row.Cells(10).BackColor = Drawing.Color.FromName("#CCFFCC")
                e.Row.Cells(10).ForeColor = Drawing.Color.FromName("#CCFFCC")
            Else
                e.Row.Cells(10).BackColor = Drawing.Color.White
                e.Row.Cells(10).ForeColor = Drawing.Color.White
            End If
            If row("oct").ToString() = "1" Then
                e.Row.Cells(11).BackColor = Drawing.Color.FromName("#CCFFCC")
                e.Row.Cells(11).ForeColor = Drawing.Color.FromName("#CCFFCC")
            Else
                e.Row.Cells(11).BackColor = Drawing.Color.White
                e.Row.Cells(11).ForeColor = Drawing.Color.White
            End If
            If row("nov").ToString() = "1" Then
                e.Row.Cells(12).BackColor = Drawing.Color.FromName("#CCFFCC")
                e.Row.Cells(12).ForeColor = Drawing.Color.FromName("#CCFFCC")
            Else
                e.Row.Cells(12).BackColor = Drawing.Color.White
                e.Row.Cells(12).ForeColor = Drawing.Color.White
            End If
            If row("dic").ToString() = "1" Then
                e.Row.Cells(13).BackColor = Drawing.Color.FromName("#CCFFCC")
                e.Row.Cells(13).ForeColor = Drawing.Color.FromName("#CCFFCC")
            Else
                e.Row.Cells(13).BackColor = Drawing.Color.White
                e.Row.Cells(13).ForeColor = Drawing.Color.White
            End If

            e.Row.Cells(2).Text = ""
            e.Row.Cells(3).Text = ""
            e.Row.Cells(4).Text = ""
            e.Row.Cells(5).Text = ""
            e.Row.Cells(6).Text = ""
            e.Row.Cells(7).Text = ""
            e.Row.Cells(8).Text = ""
            e.Row.Cells(9).Text = ""
            e.Row.Cells(10).Text = ""
            e.Row.Cells(11).Text = ""
            e.Row.Cells(12).Text = ""
            e.Row.Cells(13).Text = ""

        End If
    End Sub

    Protected Sub dgv_termino_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles dgv_termino.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim row As Data.DataRowView = CType(e.Row.DataItem, Data.DataRowView)
            If LastTermino = row("proyecto") Then
                If (dgv_termino.Rows(CurrentRowTermino).Cells(0).RowSpan = 0) Then
                    dgv_termino.Rows(CurrentRowTermino).Cells(0).RowSpan = 2
                Else
                    dgv_termino.Rows(CurrentRowTermino).Cells(0).RowSpan += 1
                End If
                e.Row.Cells(0).Visible = False
            Else
                e.Row.VerticalAlign = VerticalAlign.Middle
                LastTermino = row("proyecto").ToString()

                CurrentRowTermino = e.Row.RowIndex
            End If

            If row("ene").ToString() = "1" Then
                If ddlEstado.SelectedValue = "F" Then
                    e.Row.Cells(2).BackColor = Drawing.Color.FromName("#F8E3E3")
                    e.Row.Cells(2).ForeColor = Drawing.Color.FromName("#F8E3E3")
                Else
                    e.Row.Cells(2).BackColor = Drawing.Color.FromName("#CCFFCC")
                    e.Row.Cells(2).ForeColor = Drawing.Color.FromName("#CCFFCC")
                End If
            Else
                e.Row.Cells(2).BackColor = Drawing.Color.White
                e.Row.Cells(2).ForeColor = Drawing.Color.White
            End If

            If row("feb").ToString() = "1" Then
                If ddlEstado.SelectedValue = "F" Then
                    e.Row.Cells(3).BackColor = Drawing.Color.FromName("#F8E3E3")
                    e.Row.Cells(3).ForeColor = Drawing.Color.FromName("#F8E3E3")
                Else
                    e.Row.Cells(3).BackColor = Drawing.Color.FromName("#CCFFCC")
                    e.Row.Cells(3).ForeColor = Drawing.Color.FromName("#CCFFCC")
                End If
            Else
                e.Row.Cells(3).BackColor = Drawing.Color.White
                e.Row.Cells(3).ForeColor = Drawing.Color.White
            End If

            If row("mar").ToString() = "1" Then
                If ddlEstado.SelectedValue = "F" Then
                    e.Row.Cells(4).BackColor = Drawing.Color.FromName("#F8E3E3")
                    e.Row.Cells(4).ForeColor = Drawing.Color.FromName("#F8E3E3")
                Else
                    e.Row.Cells(4).BackColor = Drawing.Color.FromName("#CCFFCC")
                    e.Row.Cells(4).ForeColor = Drawing.Color.FromName("#CCFFCC")
                End If
            Else
                e.Row.Cells(4).BackColor = Drawing.Color.White
                e.Row.Cells(4).ForeColor = Drawing.Color.White
            End If

            If row("abr").ToString() = "1" Then
                If ddlEstado.SelectedValue = "F" Then
                    e.Row.Cells(5).BackColor = Drawing.Color.FromName("#F8E3E3")
                    e.Row.Cells(5).ForeColor = Drawing.Color.FromName("#F8E3E3")
                Else
                    e.Row.Cells(5).BackColor = Drawing.Color.FromName("#CCFFCC")
                    e.Row.Cells(5).ForeColor = Drawing.Color.FromName("#CCFFCC")
                End If
            Else
                e.Row.Cells(5).BackColor = Drawing.Color.White
                e.Row.Cells(5).ForeColor = Drawing.Color.White
            End If

            If row("may").ToString() = "1" Then
                If ddlEstado.SelectedValue = "F" Then
                    e.Row.Cells(6).BackColor = Drawing.Color.FromName("#F8E3E3")
                    e.Row.Cells(6).ForeColor = Drawing.Color.FromName("#F8E3E3")
                Else
                    e.Row.Cells(6).BackColor = Drawing.Color.FromName("#CCFFCC")
                    e.Row.Cells(6).ForeColor = Drawing.Color.FromName("#CCFFCC")
                End If
            Else
                e.Row.Cells(6).BackColor = Drawing.Color.White
                e.Row.Cells(6).ForeColor = Drawing.Color.White
            End If
            If row("jun").ToString() = "1" Then
                If ddlEstado.SelectedValue = "F" Then
                    e.Row.Cells(7).BackColor = Drawing.Color.FromName("#F8E3E3")
                    e.Row.Cells(7).ForeColor = Drawing.Color.FromName("#F8E3E3")
                Else
                    e.Row.Cells(7).BackColor = Drawing.Color.FromName("#CCFFCC")
                    e.Row.Cells(7).ForeColor = Drawing.Color.FromName("#CCFFCC")
                End If
            Else
                e.Row.Cells(7).BackColor = Drawing.Color.White
                e.Row.Cells(7).ForeColor = Drawing.Color.White
            End If
            If row("jul").ToString() = "1" Then
                If ddlEstado.SelectedValue = "F" Then
                    e.Row.Cells(8).BackColor = Drawing.Color.FromName("#F8E3E3")
                    e.Row.Cells(8).ForeColor = Drawing.Color.FromName("#F8E3E3")
                Else
                    e.Row.Cells(8).BackColor = Drawing.Color.FromName("#CCFFCC")
                    e.Row.Cells(8).ForeColor = Drawing.Color.FromName("#CCFFCC")
                End If
            Else
                e.Row.Cells(8).BackColor = Drawing.Color.White
                e.Row.Cells(8).ForeColor = Drawing.Color.White
            End If
            If row("ago").ToString() = "1" Then
                If ddlEstado.SelectedValue = "F" Then
                    e.Row.Cells(9).BackColor = Drawing.Color.FromName("#F8E3E3")
                    e.Row.Cells(9).ForeColor = Drawing.Color.FromName("#F8E3E3")
                Else
                    e.Row.Cells(9).BackColor = Drawing.Color.FromName("#CCFFCC")
                    e.Row.Cells(9).ForeColor = Drawing.Color.FromName("#CCFFCC")
                End If
            Else
                e.Row.Cells(9).BackColor = Drawing.Color.White
                e.Row.Cells(9).ForeColor = Drawing.Color.White
            End If
            If row("set").ToString() = "1" Then
                If ddlEstado.SelectedValue = "F" Then
                    e.Row.Cells(10).BackColor = Drawing.Color.FromName("#F8E3E3")
                    e.Row.Cells(10).ForeColor = Drawing.Color.FromName("#F8E3E3")
                Else
                    e.Row.Cells(10).BackColor = Drawing.Color.FromName("#CCFFCC")
                    e.Row.Cells(10).ForeColor = Drawing.Color.FromName("#CCFFCC")
                End If
            Else
                e.Row.Cells(10).BackColor = Drawing.Color.White
                e.Row.Cells(10).ForeColor = Drawing.Color.White
            End If
            If row("oct").ToString() = "1" Then
                If ddlEstado.SelectedValue = "F" Then
                    e.Row.Cells(11).BackColor = Drawing.Color.FromName("#F8E3E3")
                    e.Row.Cells(11).ForeColor = Drawing.Color.FromName("#F8E3E3")
                Else
                    e.Row.Cells(11).BackColor = Drawing.Color.FromName("#CCFFCC")
                    e.Row.Cells(11).ForeColor = Drawing.Color.FromName("#CCFFCC")
                End If
            Else
                e.Row.Cells(11).BackColor = Drawing.Color.White
                e.Row.Cells(11).ForeColor = Drawing.Color.White
            End If
            If row("nov").ToString() = "1" Then
                If ddlEstado.SelectedValue = "F" Then
                    e.Row.Cells(12).BackColor = Drawing.Color.FromName("#F8E3E3")
                    e.Row.Cells(12).ForeColor = Drawing.Color.FromName("#F8E3E3")
                Else
                    e.Row.Cells(12).BackColor = Drawing.Color.FromName("#CCFFCC")
                    e.Row.Cells(12).ForeColor = Drawing.Color.FromName("#CCFFCC")
                End If
            Else
                e.Row.Cells(12).BackColor = Drawing.Color.White
                e.Row.Cells(12).ForeColor = Drawing.Color.White
            End If
            If row("dic").ToString() = "1" Then
                If ddlEstado.SelectedValue = "F" Then
                    e.Row.Cells(13).BackColor = Drawing.Color.FromName("#F8E3E3")
                    e.Row.Cells(13).ForeColor = Drawing.Color.FromName("#F8E3E3")
                Else
                    e.Row.Cells(13).BackColor = Drawing.Color.FromName("#CCFFCC")
                    e.Row.Cells(13).ForeColor = Drawing.Color.FromName("#CCFFCC")
                End If
            Else
                e.Row.Cells(13).BackColor = Drawing.Color.White
                e.Row.Cells(13).ForeColor = Drawing.Color.White
            End If

            e.Row.Cells(2).Text = ""
            e.Row.Cells(3).Text = ""
            e.Row.Cells(4).Text = ""
            e.Row.Cells(5).Text = ""
            e.Row.Cells(6).Text = ""
            e.Row.Cells(7).Text = ""
            e.Row.Cells(8).Text = ""
            e.Row.Cells(9).Text = ""
            e.Row.Cells(10).Text = ""
            e.Row.Cells(11).Text = ""
            e.Row.Cells(12).Text = ""
            e.Row.Cells(13).Text = ""

            If ddlEstado.SelectedValue = "F" Then
                e.Row.Cells(14).Text = ""
            End If
        End If
    End Sub

    Protected Sub dgv_iniciar_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles dgv_iniciar.RowEditing
        Try
            Dim fila As Integer
            Dim Codigo_dap As Integer = 0
            Dim obj As New clsPlanOperativoAnual

            fila = e.NewEditIndex
            Codigo_dap = Me.dgv_iniciar.DataKeys(fila).Value

            Dim dtt As New Data.DataTable
            dtt = obj.POA_InicioActividades(0, 1, 0, Request.QueryString("id"), Codigo_dap)
            btnBuscar_Click(sender, e)
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub ddlEstado_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlEstado.SelectedIndexChanged
        btnBuscar_Click(sender, e)
    End Sub

    Protected Sub dgv_termino_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles dgv_termino.RowEditing
        Try
            Dim obj As New clsPlanOperativoAnual
            Dim Codigo_dap As Integer = Convert.ToInt32(dgv_termino.DataKeys(e.NewEditIndex).Values(0))
            Dim codigo_iac As Integer = Convert.ToInt32(dgv_termino.DataKeys(e.NewEditIndex).Values(1))

            ' Actualizar Estado
            Dim dtt As New Data.DataTable
            dtt = obj.POA_InicioActividades(codigo_iac, 1, 1, Request.QueryString("id"), Codigo_dap)
            btnBuscar_Click(sender, e)
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

  
End Class
