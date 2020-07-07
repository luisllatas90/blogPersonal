
Partial Class proponente_contenido
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.lblUsuario.Text = Request.QueryString("id")

        Dim ObjCnx As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)

        Me.cmdDerivar.Visible = True ' ObjCnx.TraerValor("PRP_VerificarSecretariasConsejos", Request.QueryString("id"), 0)
        Dim rsFac As New Data.DataTable
        rsFac = ObjCnx.TraerDataTable("PRP_ConsutarReunionesConsejo", "FA", Request.QueryString("id"), "", "")
        If rsFac.Rows.Count > 0 Then
            Me.txtfacultad.Text = rsFac.Rows(0).Item("codigo_fac")
        End If
        If Me.ddlInstanciaPropuesta.SelectedValue = "F" Then
            Me.cmdDerivar.Visible = True
        Else
            Me.cmdDerivar.Visible = False
        End If

    End Sub

    Protected Sub dgvPropuestas_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles dgvPropuestas.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim fila As Data.DataRowView
            fila = e.Row.DataItem
            e.Row.Attributes.Add("id", "" & fila.Row("codigo_prp").ToString & "")
            e.Row.Attributes.Add("OnMouseOver", "Resaltar(1,this,'S')")
            e.Row.Attributes.Add("OnMouseOut", "Resaltar(0,this,'S')")
            e.Row.Attributes.Add("OnClick", "HabilitarBoton('M',this)")
            e.Row.Attributes.Add("Class", "Sel")
            e.Row.Attributes.Add("Typ", "Sel")
        End If
    End Sub


    Protected Sub cmdDerivar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdDerivar.Click
        Dim ObjCnx As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
        ObjCnx.Ejecutar("PRP_DerivarPropuesta", Me.txtelegido.Value)
        Response.Write("Propuesta derivada a rectorado")
    End Sub

    Protected Sub ddlInstanciaPropuesta_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlInstanciaPropuesta.SelectedIndexChanged
        Me.txtelegido.Value = ""
    End Sub



End Class
