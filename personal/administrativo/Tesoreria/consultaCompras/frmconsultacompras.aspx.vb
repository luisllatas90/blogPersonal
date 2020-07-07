
Partial Class frmconsultacompras
    Inherits System.Web.UI.Page
    Dim tipo As String
    Dim cn As New clsaccesodatos
    Dim cencrip As New EncriptaCodigos.clsEncripta
    Protected Sub cmdbusacrpornumero_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdbusacrpornumero.Click


        Dim dts As New System.Data.DataSet
        Try
            cn.abrirconexion()
            dts = cn.consultar("dbo.sp_ConsultarRegistrocompra", "6", "%" & Me.txtnumerodocumento.Text & "%", "", "", "", "", "")
            cn.cerrarconexion()
            Me.lstinformacion.DataSource = dts.Tables("consulta")
            Me.lstinformacion.DataBind()

            Me.lblobservacion.Text = dts.Tables("consulta").Rows.Count & " Documento(s) encontrados"
        Catch ex As Exception
            Me.lblobservacion.Text = "Ocurrió un error al procesar la consulta"
        End Try

    End Sub

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
        'Me.opconsultarnumero.Attributes.Add("onclick", "pnlconsultarpornumero.style.display='block';")
        
    End Sub

End Class
