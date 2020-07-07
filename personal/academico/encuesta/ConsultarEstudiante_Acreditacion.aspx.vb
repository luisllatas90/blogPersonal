
Partial Class Encuesta_ConsultarEstudiante_Acreditacion
    Inherits System.Web.UI.Page


    Protected Sub RbtVer_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles RbtVer.SelectedIndexChanged
        If RbtVer.SelectedValue = 0 Then
            Me.CboTipoBusqueda.Visible = False
            Me.TxtTextoBusqueda.Visible = False
        Else
            Me.CboTipoBusqueda.Visible = True
            Me.TxtTextoBusqueda.Visible = True
        End If
    End Sub

    Protected Sub CboTipoBusqueda_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CboTipoBusqueda.SelectedIndexChanged
        If CboTipoBusqueda.SelectedValue = 1 Then
            Me.TxtTextoBusqueda.MaxLength = 10
        Else
            Me.TxtTextoBusqueda.MaxLength = 100
        End If
    End Sub

    Protected Sub CmdBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdBuscar.Click
        Dim objcnx As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
        Dim Datos As Data.DataTable
        Try

            If RbtVer.SelectedValue = 0 Then
                '***Busca todos los estudiantes faltantes***
                Datos = objcnx.TraerDataTable("AUN_ConsultarFaltantesPorLlenarEncuesta", "TOD", "")
            Else
                If CboTipoBusqueda.SelectedValue = 0 Then
                    Datos = objcnx.TraerDataTable("AUN_ConsultarFaltantesPorLlenarEncuesta", "NOM", Me.TxtTextoBusqueda.Text)
                Else
                    Datos = objcnx.TraerDataTable("AUN_ConsultarFaltantesPorLlenarEncuesta", "COD", Me.TxtTextoBusqueda.Text)
                End If
            End If
            If Datos.Rows.Count > 0 Then
                Me.GvDatos.DataSource = Datos
            End If
            Me.GvDatos.DataBind()

        Catch ex As Exception
            Response.Write(ex.Message)
            ClientScript.RegisterStartupScript(Me.GetType, "error", "alert('Ocurrió un error al procesar los datos')", True)
        End Try
    End Sub

    Protected Sub GvDatos_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GvDatos.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then

            'e.Row.Attributes.Add("OnMouseOver", "Resaltar(1,this,'S','')")
            'e.Row.Attributes.Add("OnMouseOut", "Resaltar(0,this,'S','')")
            e.Row.Attributes.Add("onMouseOver", "pintarcelda(this)")
            e.Row.Attributes.Add("onMouseOut", "despintarcelda(this)")
            e.Row.Style.Add("cursor", "hand")
            Dim fila As Data.DataRowView
            fila = e.Row.DataItem
            'e.Row.Cells.Item(4).Text = "<a href='AcreditacionUniversitaria_generales.aspx?sesion=" & fila.Row("codigo_alu").ToString & "'> Ir a ...</a>"
        End If
    End Sub

End Class
