
Partial Class medicina_Busqueda
    Inherits System.Web.UI.Page

    
    Protected Sub CmdBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdBuscar.Click
        Dim ObjAlumno As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)

        Me.grid2.DataSource = ObjAlumno.TraerDataTable("MED_ConsultarAlumno", Me.DDLTipo.SelectedValue, Me.TxtNombre.Text)
        Me.grid2.DataBind()

    End Sub

    Protected Sub grid2_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grid2.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim fila As Data.DataRowView
                fila = e.Row.DataItem 'Los datos devueltos (cuando quiero saber la data que llega)
                e.Row.Attributes.Add("OnMouseOver", "Resaltar(1,this,'S')")
                e.Row.Attributes.Add("OnMouseOut", "Resaltar(0,this,'S')")
                e.Row.Attributes.Add("OnClick", "ResaltarfilaDetalle_net('',this,'datos_personales.aspx?codigo_alu=" & fila.Row("codigo_alu").ToString & "');ResaltarPestana_1('0','','')")
                e.Row.Attributes.Add("id", "fila" & fila.Row("codigo_alu").ToString & "")
                e.Row.Attributes.Add("Class", "Sel")
                e.Row.Attributes.Add("Typ", "Sel")
                'e.Row.ToolTip = fila.Row("nombre_eti").ToString & " - " & fila.Row("descripcion_ein").ToString & " - TIPO: " & fila.Row("descripcion_tin").ToString
                e.Row.Cells(0).Text = e.Row.RowIndex + 1
            End If
        Catch ex As Exception

        End Try

    End Sub


    
End Class
