Imports System.Data

Partial Class _Default
    Inherits System.Web.UI.Page

    Dim dts As New DataSet
    Dim codigo_per As String

    Sub mostrarregistros()
        Dim cn As New clsaccesodatos

        If codigo_per Is Nothing Then
            codigo_per = ""
        End If
'	response.write(codigo_per & "')</script>");
        cn.abrirconexion()
        dts = cn.consultar("dbo.sp_verrendicion", "PEC", Me.cboestado.SelectedValue,codigo_per,  "", "", "", "")
        cn.cerrarconexion()

        Me.lstinformacion.DataSource = dts
        Me.lstinformacion.DataBind()


        If dts.Tables("consulta").Rows.Count = 0 Then
            Me.lblmensaje.Text = "  No se han encontrado información" & codigo_per & "  -- " & Me.cboestado.SelectedValue
        Else
            Me.lblmensaje.Text = dts.Tables("consulta").Rows.Count.ToString & " registros encontrado(s) "
        End If
    End Sub

    Protected Sub cboestado_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboestado.SelectedIndexChanged
        mostrarregistros()
    End Sub

    Protected Sub lstinformacion_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles lstinformacion.RowDataBound
        
        If e.Row.RowType = DataControlRowType.DataRow Then

            e.Row.Attributes.Add("seleccionado", 0)
            e.Row.Cells(10).Attributes.Add("onclick", "MostrarDetalleRendicion('" & e.Row.Cells(0).Text.ToString & "')")
            'e.Row.Attributes.Add("onclick", "window.open('frmadjuntararchivo.aspx?codigo_dren=1','as','toolbar=no')")
            'e.Row.Attributes.Add("onmouseenter", "resaltar(this,1)")

            e.Row.Attributes.Add("onmouseenter", "resaltar(this,1)")
            e.Row.Attributes.Add("onmouseleave", "resaltar(this,0)")
        End If



    End Sub

    Protected Sub lstinformacion_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstinformacion.SelectedIndexChanged

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            codigo_per = ""
codigo_per = Me.Request("id")
        If Me.IsPostBack = False Then

            
            

            Call mostrarregistros()

        End If
    End Sub
End Class
