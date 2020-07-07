
Partial Class Indicadores_Formularios_frmValoresVariables
    Inherits System.Web.UI.Page

    Dim usuario As Integer


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then                                
                ''Debe tomar del inicio de sesión
                Response.Write(Request.QueryString("id"))
                usuario = Request.QueryString("id")
                lblCodigo_per.Text = Request.QueryString("id")

                ''****************
                llenarselect(cboVariable)
                llenarselect(cboPeriodo)
                ''************             


                'Estado Controles
                cboSubvariable.Enabled = False
                cboDimension.Enabled = False
                cboSubdimension.Enabled = False
                cboPeriodo.Enabled = True
            End If

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub llenarselect(ByVal combo As DropDownList)
        Dim obj As New clsIndicadores
        Dim dts As New Data.DataTable
        Dim dt As New Data.DataTable
        Dim periodicidad As Integer

        If combo.ID = "cboVariable" Then
            dts = obj.ListarVariables(Request.QueryString("id"), 1)
        End If

        If combo.ID = "cboSubvariable" Then
            'Response.Write("Subvariable")
            dts = obj.ListarSubvariables_Variable(cboVariable.SelectedValue, Request.QueryString("id"), 1)
        End If

        If combo.ID = "cboDimension" Then
            dts = obj.ListarDimension_Subvariable(cboSubvariable.SelectedValue, Request.QueryString("id"), 1)
        End If

        If combo.ID = "cboSubdimension" Then
            dts = obj.ListarSubdimension_Dimension(cboDimension.SelectedValue, Request.QueryString("id"), 1)
        End If

        If combo.ID = "cboPeriodo" Then
            'If cboVariable.SelectedValue <> "%" Then
            '    dt = obj.ConsultarVariable(cboVariable.SelectedValue)
            '    periodicidad = dt.Rows(0).Item("CodigoPeri")
            '    dts = obj.ConsultarPeriodos(periodicidad)
            'Else
            dts = obj.ConsultarPeriodos(0)
            'End If
        End If

        combo.DataSource = dts
        combo.DataTextField = "DescripcionCorta"
        combo.DataValueField = "Codigo"
        combo.DataBind()
        combo.Enabled = True

    End Sub

    Protected Sub cboVariable_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboVariable.SelectedIndexChanged
        '*************PARA CONSULTAR: SI SELECCIONA TODOS,NO PUEDE SELECCIONAR sub, dim, sub o periodo

        If cboVariable.SelectedValue <> "%" Then

            llenarselect(cboSubvariable)
            '-------------------------------------------------- agregado xDguevara 08.05.2012
            llenarselect(cboDimension)
            llenarselect(cboSubdimension)
            '--------------------------------------------------------------------------------

            'llenarselect(cboPeriodo)
            '==================================
            'Pruebas Dguevara 18.04.2013
            'Response.Write("Entra cbovariable")
            'CargarDataVariables()
            '==================================
        Else
            cboSubvariable.Items.Clear()
            cboSubvariable.Items.Add("Todos")
            cboSubvariable.Items(0).Value = "%"
            'cboSubvariable.Enabled = False

            '-------------------------------------------------- agregado xDguevara 08.05.2012
            cboDimension.Items.Clear()
            cboDimension.Items.Add("Todos")
            cboDimension.Items(0).Value = "%"
            'cboDimension.Enabled = False

            cboSubdimension.Items.Clear()
            cboSubdimension.Items.Add("Todos")
            cboSubdimension.Items(0).Value = "%"
            'cboSubdimension.Enabled = False

            '--------------------------------------------------

            'cboPeriodo.Items.Clear()
            'cboPeriodo.Items.Add("Todos")
            'cboPeriodo.Items(0).Value = "%"
            'cboPeriodo.Enabled = False
        End If

        If cboSubvariable.Enabled = False Then
            cboDimension.Enabled = False
        End If

        'CargarDataVariables()

    End Sub

   

    Protected Sub cboSubvariable_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboSubvariable.SelectedIndexChanged
        llenarselect(cboDimension)
        If cboDimension.Enabled = False Then
            cboSubdimension.Enabled = False
        End If
    End Sub

    Protected Sub cboDimension_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboDimension.SelectedIndexChanged
        llenarselect(cboSubdimension)
    End Sub

    Protected Sub gvValores_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvValores.RowDataBound
        'Try
        '    If e.Row.RowType = DataControlRowType.DataRow Then
        '        e.Row.Attributes.Add("OnMouseOver", "Resaltar(1,this,'S')")
        '        e.Row.Attributes.Add("OnMouseOut", "Resaltar(0,this,'S')")

        '        '' ---------------- Validacion segun el nivel y ultimo nivel de la variable -----------------------------------
        '        '
        '        If sender.DataKeys(e.Row.RowIndex).Item("existebd_var").ToString <> "S" Then
        '            If sender.DataKeys(e.Row.RowIndex).Item("sumatoria_var").ToString <> "0" Then
        '                'Se desbloquea, cuando los niveles son iguales y tiene sumatoria.
        '                If (sender.DataKeys(e.Row.RowIndex).Item("DetalleNivel") = sender.DataKeys(e.Row.RowIndex).Item("UltimoNivel").ToString) And (sender.DataKeys(e.Row.RowIndex).Item("sumatoria_var").ToString = "1") And (sender.DataKeys(e.Row.RowIndex).Item("desc_niv2").ToString.Trim <> "AA") Then
        '                    e.Row.Cells(13).Enabled = True
        '                Else
        '                    If (sender.DataKeys(e.Row.RowIndex).Item("desc_niv2").ToString.Trim <> "AA") Then
        '                        e.Row.Cells(13).Text = "<center><img src='../images/closed.png' style='border: 0px' alt='Bloqueado'/></center>"
        '                        e.Row.Cells(13).Enabled = False
        '                    End If
        '                End If
        '            End If
        '        Else
        '            ''existebd_var='S'-> es una variable importada por el sistema la cual no puede ser modificada.
        '            e.Row.Cells(13).Text = "<center><img src='../images/var_sistema.png' style='border: 0px' alt='Variable del Sistema'/></center>"
        '            e.Row.Cells(13).Enabled = False
        '        End If

        '        '' ---   Pintar niveles x color.  --
        '        Select Case sender.DataKeys(e.Row.RowIndex).Item("DetalleNivel")
        '            Case 1
        '                e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#95EDFB")
        '            Case 2
        '                If (sender.DataKeys(e.Row.RowIndex).Item("desc_niv2").ToString.Trim = "AA") Then
        '                    e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#EC9494")
        '                    e.Row.Cells(13).Enabled = True
        '                Else
        '                    e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#D8FCAE")
        '                End If
        '            Case 3
        '                e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#FCF9B9")
        '        End Select
        '    End If
        'Catch ex As Exception
        '    Response.Write(ex.Message)
        'End Try
    End Sub

    
    Protected Sub btnBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBuscar.Click
        Try
            CargarDataVariables()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub CargarDataVariables()
        Try
            Dim obj As New clsIndicadores
            Dim dts As New Data.DataTable


            Response.Write("cboVariable.SelectedValue: " & cboVariable.SelectedValue)
            Response.Write("<br/>")
            Response.Write("cboSubvariable.SelectedValue: " & cboSubvariable.SelectedValue)
            Response.Write("<br/>")
            Response.Write("cboDimension.SelectedValue: " & cboDimension.SelectedValue)
            Response.Write("<br/>")
            Response.Write("cboSubdimension.SelectedValue: " & cboSubdimension.SelectedValue)
            Response.Write("<br/>")
            Response.Write("cboPeriodo.SelectedValue: " & cboPeriodo.SelectedValue)
            Response.Write("<br/>")
            Response.Write("Codigo_per: " & Request.QueryString("id"))

            dts = obj.ListarValoresVariable(cboVariable.SelectedValue, _
                                            cboSubvariable.SelectedValue, _
                                            cboDimension.SelectedValue, _
                                            cboSubdimension.SelectedValue, _
                                            cboPeriodo.SelectedValue, Request.QueryString("id"))
            'If dts.Rows.Count > 0 Then
            '    gvValores.DataSource = dts
            '    gvValores.DataBind()
            'End If

            'gvPrueba.DataSource = dts
            'sgvPrueba.DataBind()

            Response.Write("Cantidad datos: " & dts.Rows.Count)

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

End Class

