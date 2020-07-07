Partial Class DirectorInvestigacion_convocatorias
    Inherits System.Web.UI.Page

    Protected Sub CmdGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdGuardar.Click
        Dim Obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
        Dim Ruta As String
        Dim NombreArchivo As String
        Ruta = Server.MapPath("")
        NombreArchivo = "convocatoria" & Now.Day.ToString & Now.Month.ToString & Now.Year.ToString & Now.Hour.ToString & Now.Minute.ToString & Now.Second.ToString & System.IO.Path.GetExtension(Me.FileArchivo.FileName).ToLower()

        If Me.GridView1.SelectedIndex = -1 Then
            Try
                If Me.FileArchivo.HasFile Then
                    Me.FileArchivo.PostedFile.SaveAs(Ruta & "\convocatorias\" & NombreArchivo)
                End If
                
		Obj.Ejecutar("INV_AgregarConvocatoria", Me.TxtTitulo.Text.Trim, Me.TxtInstitucion.Text.Trim, Me.TxtDescripcion.Text.Trim, "", Me.TxtTemas.Text.Trim, CDate(Me.TxtFecha.Text), NombreArchivo)
               
 		Obj = Nothing
                Me.GridView1.DataBind()
                Page.RegisterStartupScript("Exito", "<script>alert('Se REGISTRÓ la convocatoria con satisfacción.')</script>")
            Catch ex As Exception
		'response.write(ex.message)
                Page.RegisterStartupScript("Error", "<script>alert('" & ex.Message & "')</script>")
            End Try
        Else
            Try
                If Me.FileArchivo.HasFile Then
                    Me.FileArchivo.PostedFile.SaveAs(Ruta & "\convocatorias\" & NombreArchivo)
                End If
                Obj.Ejecutar("INV_ModificarConvocatoria", Me.GridView1.DataKeys.Item(0).Value, Me.TxtTitulo.Text.Trim, Me.TxtInstitucion.Text.Trim, Me.TxtDescripcion.Text.Trim, "", Me.TxtTemas.Text.Trim, CDate(Me.TxtFecha.Text), NombreArchivo)
                Obj = Nothing
                Me.GridView1.DataBind()

                Me.TxtTitulo.Text = ""
                Me.TxtTemas.Text = ""
                Me.TxtFecha.Text = ""
                Me.TxtDescripcion.Text = ""
                Me.TxtInstitucion.Text = ""
                Me.PanelFormulario.Visible = False
                Me.GridView1.SelectedIndex = -1

                Page.RegisterStartupScript("Exito", "<script>alert('Se MODIFICÓ la convocatoria con satisfacción.')</script>")
            Catch ex As Exception
                Page.RegisterStartupScript("Error", "<script>alert('Ocurrió un error al procesar los datos.')</script>")
            End Try
        End If

    End Sub

    
    Protected Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowDataBound

        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim Fila As Data.DataRowView
            Fila = e.Row.DataItem

            e.Row.Cells(8).Text = "<a target='_blank' href='convocatorias/" & Fila.Item("RutaArchivo_Con").ToString & "'><img border='0' src='../../../../images/download.gif'></a>"
            e.Row.Attributes.Add("OnMouseOver", "Resaltar(1,this,'S')")
            e.Row.Attributes.Add("OnMouseOut", "Resaltar(0,this,'S')")
            e.Row.Attributes.Add("OnClick", "javascript:__doPostBack('GridView1','Select$" & e.Row.RowIndex & "')")
            
        End If

    End Sub

    Protected Sub CmdNuevo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdNuevo.Click
        Me.GridView1.SelectedIndex = -1
        Me.LblTitulo.Text = "Registrar Nueva Convocatoria"
        Me.CmdGuardar.Text = "Registrar"
        Me.PanelFormulario.Visible = True
        Me.TxtTitulo.Text = ""
        Me.TxtTemas.Text = ""
        Me.TxtFecha.Text = ""
        Me.TxtDescripcion.Text = ""
        Me.TxtInstitucion.Text = ""
    End Sub

    Protected Sub GridView1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridView1.SelectedIndexChanged
        Me.CmdGuardar.Text = "Actualizar"
        Me.LblTitulo.Text = "Actualizar Convocatoria"
        Me.PanelFormulario.Visible = True
        Me.TxtTitulo.Text = Server.HtmlDecode(Me.GridView1.SelectedRow.Cells(1).Text)
        Me.TxtInstitucion.Text = Server.HtmlDecode(Me.GridView1.SelectedRow.Cells(2).Text)
        Me.TxtDescripcion.Text = Server.HtmlDecode(Me.GridView1.SelectedRow.Cells(3).Text)
        Me.TxtTemas.Text = Server.HtmlDecode(Me.GridView1.SelectedRow.Cells(5).Text)
        Me.TxtFecha.Text = Me.GridView1.SelectedRow.Cells(7).Text

    End Sub

    Protected Sub CmdBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdBuscar.Click
        Me.GridView1.SelectedIndex = -1
        Me.PanelFormulario.Visible = False
        

    End Sub

    Protected Sub CmdEliminar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdEliminar.Click
        Dim Obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
        Try
            Obj.Ejecutar("INV_EliminarConvocatoria", Me.GridView1.SelectedValue)
            Me.GridView1.DataBind()
            Me.GridView1.SelectedIndex = -1
            Me.PanelFormulario.Visible = False
            Me.TxtTitulo.Text = ""
            Me.TxtTemas.Text = ""
            Me.TxtFecha.Text = ""
            Me.TxtDescripcion.Text = ""
            Me.TxtInstitucion.Text = ""
            Page.RegisterStartupScript("Exito", "<script>alert('Se ELIMINÓ la convocatoria correctamente.')</script>")

        Catch ex As Exception
            Page.RegisterStartupScript("Error", "<script>alert('Ocurrió un error al procesar los datos.')</script>")
        End Try
    End Sub
End Class
