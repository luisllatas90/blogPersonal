Partial Class proponente_contenido
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.lblUsuario.Text = Request.QueryString("id")
        
        Dim ObjCnx As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
        'Me.cmdDerivar.Visible = True ' ObjCnx.TraerValor("PRP_VerificarSecretariasConsejos", Request.QueryString("id"), 0)
        Dim rsFac As New Data.DataTable

        '' Si es Secretario y pertenece a una facultad
        rsFac = ObjCnx.TraerDataTable("PRP_ConsutarReunionesConsejo_POA", "FA", Request.QueryString("id"), "", "")
        If rsFac.Rows.Count > 0 Then
            Me.txtfacultad.Text = rsFac.Rows(0).Item("codigo_fac")
        Else
            Me.txtfacultad.Text = Request.QueryString("id")
        End If

        'If Me.ddlInstanciaPropuesta.SelectedValue = "F" Then
        '    Me.cmdDerivar.Visible = True
        'Else
        '    Me.cmdDerivar.Visible = False
        'End If
    End Sub

    Protected Sub dgvPropuestas_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles dgvPropuestas.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim fila As Data.DataRowView
            fila = e.Row.DataItem
            e.Row.Attributes.Add("id", "" & fila.Row("codigo_prp").ToString & "")
            e.Row.Attributes.Add("OnMouseOver", "Resaltar(1,this,'S')")
            e.Row.Attributes.Add("OnMouseOut", "Resaltar(0,this,'S')")

            'Response.Write("<script>alert('" & fila.Row("fechainicio_Ipr").ToString.Substring(6, 4) & "')</script>")

            Dim anio As Integer = fila.Row("fechainicio_Ipr").ToString.Substring(6, 4)
            If anio >= 2017 Then
                e.Row.Attributes.Add("OnClick", "HabilitarBoton('M',this)")
            Else
                e.Row.Attributes.Add("OnClick", "HabilitarBoton('N',this)")
            End If

            e.Row.Attributes.Add("Class", "Sel")
            e.Row.Attributes.Add("Typ", "Sel")
        End If
    End Sub

    Protected Sub cmdDerivar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdDerivar.Click
        Try
            Dim ObjCnx As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
            ObjCnx.Ejecutar("PRP_DerivarPropuesta_v1", Me.txtelegido.Value)

            dgvPropuestas.DataBind()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub ddlInstanciaPropuesta_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlInstanciaPropuesta.SelectedIndexChanged
        Me.txtelegido.Value = ""
    End Sub

    'Protected Sub CmdVer_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdVer.Click
    '    Try
    '        Dim dtt As New Data.DataTable
    '        Dim ObjCnx As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
    '        dtt = ObjCnx.TraerDataTable("PRP_ConsultarItemDetallePresupuesto", Me.txtelegido.Value)

    '        ''Response.Write(dtt.Rows(0).Item(0).ToString)
    '        If dtt.Rows(0).Item(0).ToString = 0 Then
    '            Response.Write("No se han registrado Item's en el Presupuesto")
    '            dgv_Presupuesto.Visible = False
    '        Else
    '            Call wf_llenarItemPresupuesto()
    '            dgv_Presupuesto.Visible = True
    '        End If
    '    Catch ex As Exception

    '    End Try
    'End Sub

    Sub wf_llenarItemPresupuesto()
        ''Llenar Grid de Items Presupuestados
        Dim dtPresupuesto As New Data.DataTable
        Dim ObjCnx As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
        dtPresupuesto = ObjCnx.TraerDataTable("PRP_ListaItemsPresupuesto", Me.txtelegido.Value)
        Me.dgv_Presupuesto.DataSource = dtPresupuesto
        Me.dgv_Presupuesto.DataBind()
        dgv_Presupuesto.Visible = False
    End Sub

    'Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
    '    Response.Write("<script>alert('PRP_ConsultarPropuesta_ant S, " & txtfacultad.Text & ", x, " & ddlInstanciaPropuesta.SelectedValue & ", x')</script>")

    'End Sub
End Class
