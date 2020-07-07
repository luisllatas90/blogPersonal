
Partial Class Investigador_responsables_investigacion
    Inherits System.Web.UI.Page

    Protected Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim fila As Data.DataRowView
            fila = e.Row.DataItem
            If fila.Row("datos_per") IsNot System.DBNull.Value Then
                e.Row.Cells(5).Text = fila.Row("datos_per").ToString
                e.Row.Cells(3).Text = "Personal sin Linea"
            ElseIf fila.Row("datos2_per") IsNot System.DBNull.Value Then
                e.Row.Cells(5).Text = fila.Row("datos2_per").ToString & " - " & fila.Row("nombre_lin").ToString
                e.Row.Cells(3).Text = "Personal con Linea"
            ElseIf fila.Row("datos_alu") IsNot System.DBNull.Value Then
                e.Row.Cells(5).Text = fila.Row("datos_alu").ToString
                e.Row.Cells(3).Text = "Alumno"
            ElseIf fila.Row("datos_ext") IsNot System.DBNull.Value Then
                e.Row.Cells(5).Text = fila.Row("datos_ext").ToString
                e.Row.Cells(3).Text = "Personal Externo"
            End If
            e.Row.Attributes.Add("OnMouseOver", "Resaltar(1,this,'S')")
            e.Row.Attributes.Add("OnMouseOut", "Resaltar(0,this,'S')")
            e.Row.Attributes.Add("id", "fila" & fila.Row("codigo_res").ToString & "")
            e.Row.Attributes.Add("Class", "Sel")
            e.Row.Attributes.Add("Typ", "Sel")
            e.Row.Cells(0).Text = e.Row.RowIndex + 1

            If e.Row.RowIndex = 0 Then
                e.Row.Cells(9).Text = ""
            End If

        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            Me.CmdAgregar.Attributes.Add("OnClick", "AbrirPopUp('agrega_responsables.aspx?codigo_Inv=" & Request.QueryString("codigo_inv") & "','480','650'); return false;")
        End If
    End Sub

    Protected Sub GridView1_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles GridView1.RowDeleting
        Dim ObjResp As New Investigacion
        ObjResp.EliminarResponsableInv(e.Keys.Item(0))
        ObjResp = Nothing
        e.Cancel = True
        Me.GridView1.DataBind()
    End Sub
End Class
