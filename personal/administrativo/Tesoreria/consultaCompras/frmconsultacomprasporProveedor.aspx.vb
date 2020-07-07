
Partial Class frmconsultacomprasporProveedor
    Inherits System.Web.UI.Page

    Dim tipo As String
    Dim cn As New clsaccesodatos
    Dim cencrip As New EncriptaCodigos.clsEncripta
    

    Protected Sub lstinformacion_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles lstinformacion.RowDataBound

        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes.Add("onmouseenter", "resaltar(this,1)")
            e.Row.Attributes.Add("onmouseleave", "resaltar(this,0)")
            tipo = cencrip.Codifica("069" & "2")
            e.Row.Cells(11).Attributes.Add("onclick", "window.open('frmverdeudapagar2.aspx?id=" & cencrip.Codifica("069" & e.Row.Cells(0).Text.ToString) & "&tipo=" & tipo & "','','toolbar=no; height=320; width=1050')")
            e.Row.Cells(12).Attributes.Add("onclick", "window.open('frmdocumentologistica.aspx?id=" & cencrip.Codifica("069" & e.Row.Cells(0).Text.ToString) & "','','toolbar=no; height=550; width=1050px')")
        End If
    End Sub

    Protected Sub lstinformacion_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstinformacion.SelectedIndexChanged

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Me.IsPostBack = False Then
            Me.opconsultarnumero.Attributes.Add("onclick", "pnlconsultarpornumero.style.display='block';")
            'Me.chkfechas.Attributes.Add("onclick", "HabilitarIntervalo(this)")
        End If
    End Sub


    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbuscacliente.Click
        Response.Write("<script>window.open('frmbuscacliente.aspx','t','toolbar=no, width=800, height=500')</script>")
    End Sub

    Protected Sub cmdbuscarporproveedor_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdbuscarporproveedor.Click
        Dim dts As New System.Data.DataSet
        Dim datosconsulta() As String
        Try
            datosconsulta = Split(Me.hdtxtcodigo_tcl.Value, "//")

            If datosconsulta(0) = "" Then Me.lblobservacion.Text = "Seleccione Proveedor" : Exit Sub
            cn.abrirconexion()
            If Me.chkfechas.Checked = False Then
                dts = cn.consultar("dbo.sp_ConsultarRegistrocompra", "IMPFIC", datosconsulta(0), "", "")
            Else
                dts = cn.consultar("dbo.sp_ConsultarRegistrocompra", "IMPCPPIF", datosconsulta(0), Me.txtfechainicial.Text, Me.txtfechafinal.Text)
            End If
            cn.cerrarconexion()
            Me.lstinformacion.DataSource = dts.Tables("consulta")
            Me.lstinformacion.DataBind()

            Me.txtcodigo_tcl.Text = datosconsulta(0)
            Me.txtproveedor.Text = datosconsulta(1)
            Me.lblobservacion.Text = dts.Tables("consulta").Rows.Count & " Documento(s) encontrados"
        Catch ex As Exception
            Me.lblobservacion.Text = "Ocurrió un error al procesar la consulta"
        End Try


    End Sub

    Protected Sub chkfechas_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkfechas.CheckedChanged
        Dim datosconsulta() As String
        datosconsulta = Split(Me.hdtxtcodigo_tcl.Value, "//")

        If UBound(datosconsulta) > 0 Then
            Me.txtcodigo_tcl.Text = datosconsulta(0)
            Me.txtproveedor.Text = datosconsulta(1)
        End If

        Me.txtfechafinal.Enabled = Me.chkfechas.Checked
        Me.txtfechainicial.Enabled = Me.chkfechas.Checked
    End Sub
End Class
