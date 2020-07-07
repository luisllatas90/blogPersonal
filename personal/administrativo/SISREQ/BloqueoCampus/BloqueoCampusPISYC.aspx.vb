
Partial Class Consultas_BloqueoCampusPISYC
    Inherits System.Web.UI.Page

    Protected Sub gvEstudiantes_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvEstudiantes.RowCreated
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim chkDeuda, chkDocumentos As New CheckBox

            e.Row.Attributes.Add("OnMouseOver", "Resaltar(1,this,'S')")
            e.Row.Attributes.Add("OnMouseOut", "Resaltar(0,this,'S')")

            chkDeuda.Checked = gvEstudiantes.DataKeys.Item(e.Row.RowIndex).Values(1)  ' e.Row.Cells(1).Text
            chkDocumentos.Checked = gvEstudiantes.DataKeys.Item(e.Row.RowIndex).Values(2) 'e.Row.Cells(2).Text
            e.Row.Cells(1).Text = ""
            e.Row.Cells(2).Text = ""
            e.Row.Cells(0).Text = e.Row.RowIndex + 1
            chkDeuda.ID = "chkDeuda"
            chkDocumentos.ID = "chkDocumento"
            e.Row.Cells(1).Controls.Add(chkDeuda)
            e.Row.Cells(2).Controls.Add(chkDocumentos)
        End If
    End Sub

    Protected Sub cmdConsultar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdConsultar.Click
        Dim Objcnx As New ClsConectarDatos
        Objcnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        Objcnx.AbrirConexion()
        gvEstudiantes.DataSource = Objcnx.TraerDataTable("PEISYC_ConsultarAlumnosPorEdicion", 129, Me.cboPrograma.SelectedValue)
        Objcnx.CerrarConexion()
        gvEstudiantes.DataBind()
    End Sub

    Protected Sub lnkActualizar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkActualizar.Click
        Dim Objcnx As New ClsConectarDatos
        Dim codigo_alu As Integer
        Dim chkDeuda, chkDocumentos As New CheckBox
        Try

            Objcnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            Objcnx.AbrirConexion()
            For i As Int16 = 1 To Me.gvEstudiantes.Rows.Count
                codigo_alu = Me.gvEstudiantes.DataKeys.Item(i - 1).Values(0)
                chkDeuda = Me.gvEstudiantes.Controls(0).Controls(i).Controls(1).Controls(0)
                chkDocumentos = Me.gvEstudiantes.Controls(0).Controls(i).Controls(2).Controls(0)
                'Response.Write(chkDeuda.Checked) 'Me.gvEstudiantes.Controls(0).Controls(i).Controls(2).Controls.Count)
                'Response.Write(i & " - " & codigo_alu & "-" & chkDeuda.Checked & " - " & chkDocumentos.Checked & " - ")
                Objcnx.Ejecutar("PEISYC_BloquearYDesbloquearPorEdicion", codigo_alu, IIf(chkDeuda.Checked = True, 1, 0), IIf(chkDocumentos.Checked = True, 1, 0))
            Next
            Objcnx.CerrarConexion()
            cmdConsultar_Click(sender, e)
            Response.Write("Se actualizaron correctamente los datos")
        Catch ex As Exception
            Objcnx = Nothing
            Response.Write("Ocurrió un error al actualizar los datos")
        End Try
    End Sub


    Protected Sub gvEstudiantes_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvEstudiantes.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim chkDeuda, chkDocumentos As New CheckBox

            e.Row.Attributes.Add("OnMouseOver", "Resaltar(1,this,'S')")
            e.Row.Attributes.Add("OnMouseOut", "Resaltar(0,this,'S')")

            chkDeuda.Checked = gvEstudiantes.DataKeys.Item(e.Row.RowIndex).Values(1)  ' e.Row.Cells(1).Text
            chkDocumentos.Checked = gvEstudiantes.DataKeys.Item(e.Row.RowIndex).Values(2) 'e.Row.Cells(2).Text
            e.Row.Cells(1).Text = ""
            e.Row.Cells(2).Text = ""
            e.Row.Cells(0).Text = e.Row.RowIndex + 1
            chkDeuda.ID = "chkDeuda"
            chkDocumentos.ID = "chkDocumento"
            e.Row.Cells(1).Controls.Add(chkDeuda)
            e.Row.Cells(2).Controls.Add(chkDocumentos)
        End If
    End Sub
End Class
