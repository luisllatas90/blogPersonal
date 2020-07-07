
Partial Class frmconsultaplanilla
    Inherits System.Web.UI.Page
    Dim cn As New clsaccesodatos
    Dim tipo As String
    Dim cencrip As New EncriptaCodigos.clsEncripta
    Dim codigo_plla As Integer
    Sub mostrartipoPlanilla()
        Dim dts As New System.Data.DataSet
        cn.abrirconexion()
        dts = cn.consultar("dbo.spPla_ConsultarTipoPlanilla", "to", "", "", "", "", "", "")
        cn.cerrarconexion()


        Me.cbotipoplanilla.DataTextField = "descripcion_tplla"
        Me.cbotipoplanilla.DataValueField = "codigo_tplla"
        Me.cbotipoplanilla.DataSource = dts.Tables("consulta")
        Me.cbotipoplanilla.DataBind()

    End Sub


    Sub mostrarañomes()
        Dim i As Integer
        For i = 1 To 12
            Me.cboaño.Items.Add(2007 + i)
            Me.cbomes.Items.Add(MonthName(i))
        Next
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Me.IsPostBack = False Then
            Me.mostrartipoPlanilla()
            Me.mostrarañomes()

        End If
    End Sub

    Protected Sub cmdbuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdbuscar.Click
        Dim dtsPlanilla As New System.Data.DataSet
        cn.abrirconexion()
        dtsPlanilla = cn.consultar("dbo.spConsultarPlanilla", "1", Me.cbotipoplanilla.SelectedValue, Me.cboaño.Text, Me.cbomes.SelectedIndex + 1, "", "", "")
        cn.cerrarconexion()
        'codigo_plla = dtsPlanilla.Tables("consulta").Rows(0).Item("codigo_plla")
        Me.lstinformacion.DataSource = dtsPlanilla.Tables("consulta")
        Me.lstinformacion.DataBind()



    End Sub

    Protected Sub lstinformacion_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles lstinformacion.RowDataBound
        Dim CENCRIP As New EncriptaCodigos.clsEncripta
        If e.Row.RowType = DataControlRowType.DataRow Then
            'e.Row.Cells(7).Text = "Ver detalles"
            'Dim hl As New HyperLink()
            'hl.NavigateUrl = ""
            'hl.ImageUrl = "~/iconos/buscar.gif"
            'e.Row.Cells(7).Controls.Add(hl)
            'e.Row.Cells(7).Text = "~/imagenes/buscar.gif"

            e.Row.Attributes.Add("onmouseenter", "resaltar(this,1)")
            e.Row.Attributes.Add("onmouseleave", "resaltar(this,0)")
            tipo = CENCRIP.Codifica("069" & "11")
            e.Row.Cells(7).Attributes.Add("onclick", "window.open('.../consultarcompras/frmverdeudapagar2.aspx?id=" & CENCRIP.Codifica("069" & e.Row.Cells(0).Text.ToString) & "&tipo=" & tipo & "','','toolbar=no; height=360; width=1050')")
            e.Row.Cells(8).Attributes.Add("onclick", "window.open('frmboleta.aspx?codigo_dplla=" & CENCRIP.Codifica("069" & e.Row.Cells(0).Text.ToString) & "','','toolbar=no; height=860; width=850')")
        End If
    End Sub

    Protected Sub lstinformacion_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstinformacion.SelectedIndexChanged

    End Sub
End Class
