
Partial Class Equipo_Administrarsolicitudes
    Inherits System.Web.UI.Page


    Protected Sub CboTabla_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CboTabla.SelectedIndexChanged
        Dim ObjCnx As New ClsSqlServer(ConfigurationManager.ConnectionStrings(1).ConnectionString)
        If Me.CboTabla.SelectedValue = 0 Then
            Me.CboCampo.Visible = False
            'Me.CboTabla.SelectedValue = 0
        ElseIf CboTabla.SelectedValue = 1 Then
            Me.CboCampo.Visible = True
            ClsFunciones.LlenarListas(Me.CboCampo, ObjCnx.TraerDataTable("paReq_ListaEstadosAdministrar"), "id_est", "descripcion_est", "-- Seleccionar Estado --")
            Me.CboCampo.SelectedValue = -1
        ElseIf CboTabla.SelectedValue = 2 Then
            Me.CboCampo.Visible = True
            ClsFunciones.LlenarListas(Me.CboCampo, ObjCnx.TraerDataTable("paReq_ConsultarCentroCosto"), "codigo_cco", "descripcion_cco", "-- Seleccionar Área --")
            Me.CboCampo.SelectedValue = -1
        End If
        ObjCnx = Nothing
    End Sub

    Protected Sub gvSolicitud_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvSolicitud.RowCreated
        Dim Objcnx As New ClsSqlServer(ConfigurationManager.ConnectionStrings(1).ConnectionString)
        Dim datos As Data.DataTable

        datos = Objcnx.TraerDataTable("dbo.paReq_ListaEstadosAdministrar")

        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim fila As Data.DataRowView
            Dim cadena As String
            fila = e.Row.DataItem
            Dim CajaOculta, tipo As New HiddenField
            Dim Combo As New DropDownList
            Dim CajaTexto As New TextBox
            e.Row.Attributes.Add("onMouseOver", "pintarcelda(this)")
            e.Row.Attributes.Add("onMouseOut", "despintarcelda(this)")
            cadena = "'DatosSolicitud.aspx?cod_sol=" & e.Row.Cells(0).Text & "'"
            e.Row.Attributes.Add("onClick", "javascript:ifDatos.document.location.href=" & cadena)
            Combo.ID = "valor"
            CajaOculta.ID = "valor1"
            CajaTexto.ID = "valor2"
            tipo.ID = "valor3"

            CajaOculta.Value = e.Row.Cells(8).Text
            tipo.Value = e.Row.Cells(6).Text
            CajaTexto.Text = Trim(Server.HtmlDecode(e.Row.Cells(10).Text))
            CajaTexto.Width = 200
            ClsFunciones.LlenarListas(Combo, datos, "id_est", "descripcion_est")
            Combo.SelectedValue = e.Row.Cells(5).Text

            e.Row.Cells(0).Text = e.Row.RowIndex + 1
            e.Row.Cells(11).Controls.Add(CajaOculta)
            e.Row.Cells(5).Controls.Add(Combo)
            e.Row.Cells(10).Controls.Add(CajaTexto)
            e.Row.Cells(8).Text = "&nbsp;"
            e.Row.Cells(6).Controls.Add(tipo)

        End If
    End Sub

    Protected Sub gvSolicitud_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvSolicitud.RowDataBound
        Dim Objcnx As New ClsSqlServer(ConfigurationManager.ConnectionStrings(1).ConnectionString)
        Dim datos As Data.DataTable

        datos = Objcnx.TraerDataTable("dbo.paReq_ListaEstadosAdministrar")

        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim fila As Data.DataRowView
            Dim cadena As String
            fila = e.Row.DataItem
            Dim CajaOculta, tipo As New HiddenField
            Dim Combo As New DropDownList
            Dim CajaTexto As New TextBox
            If fila.Row.Item("tipo").ToString = "s" Then
                e.Row.ForeColor = Drawing.Color.FromArgb(61, 132, 211)
            Else
                e.Row.ForeColor = Drawing.Color.FromArgb(26, 132, 26)
            End If
            e.Row.Attributes.Add("onMouseOver", "pintarcelda(this)")
            e.Row.Attributes.Add("onMouseOut", "despintarcelda(this)")
            cadena = "'DatosSolicitud.aspx?cod_sol=" & e.Row.Cells(0).Text & "'"
            e.Row.Attributes.Add("onClick", "javascript:ifDatos.document.location.href=" & cadena)
            Page.RegisterStartupScript("Visible", "<script>CelDatos.style.visibility='visible'</script>")
            Combo.ID = "valor"
            CajaOculta.ID = "valor1"
            CajaTexto.ID = "valor2"
            tipo.ID = "valor3"

            CajaOculta.Value = e.Row.Cells(8).Text
            tipo.Value = e.Row.Cells(6).Text

            CajaTexto.Text = Trim(Server.HtmlDecode(e.Row.Cells(10).Text))
            CajaTexto.Width = 200
            ClsFunciones.LlenarListas(Combo, datos, "id_est", "descripcion_est")
            Combo.SelectedValue = e.Row.Cells(5).Text

            e.Row.Cells(0).Text = e.Row.RowIndex + 1
            e.Row.Cells(11).Controls.Add(CajaOculta)
            e.Row.Cells(5).Controls.Add(Combo)
            e.Row.Cells(10).Controls.Add(CajaTexto)
            e.Row.Cells(8).Text = "&nbsp;"
            e.Row.Cells(6).Controls.Add(tipo)

        End If
    End Sub

    Protected Sub gvSolicitud_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvSolicitud.Load
        Me.LblRegistros.Text = "Total de Registros: " & Me.gvSolicitud.Rows.Count.ToString & "  "
    End Sub

    Protected Sub CmdGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdGuardar.Click
        Dim Objcnx As New ClsSqlServer(ConfigurationManager.ConnectionStrings(1).ConnectionString)
        Try
            If Me.gvSolicitud.Rows.Count > 0 Then
                Objcnx.IniciarTransaccion()
                For j As Integer = 1 To Me.gvSolicitud.Rows.Count
                    For i As Integer = 0 To Me.gvSolicitud.Controls(0).Controls(j).Controls(5).Controls.Count - 1
                        Dim controlCombo As DropDownList
                        Dim ControlTexto As TextBox
                        Dim CajaOculta, tipo As HiddenField
                        CajaOculta = Me.gvSolicitud.Controls(0).Controls(j).Controls(11).Controls(i)
                        tipo = Me.gvSolicitud.Controls(0).Controls(j).Controls(6).Controls(i)
                        controlCombo = Me.gvSolicitud.Controls(0).Controls(j).Controls(5).Controls(i)
                        ControlTexto = Me.gvSolicitud.Controls(0).Controls(j).Controls(10).Controls(i)

                        'El numero de la solicitud es celda.text
                        'El valor del combo a grabar es controlcombo.selectedvalue 

                        Objcnx.Ejecutar("paReq_ActualizarEstadoSolicitud", CajaOculta.Value.Replace(",", ""), controlCombo.SelectedValue, Request.QueryString("id"), ControlTexto.Text, tipo.Value)
                        'para actualizar bitacora - codigo_per
                        'Response.Write(controlCombo.SelectedValue & "--" & ControlTexto.Text & "--" & CajaOculta.Value.Replace(",", "") & "---> " & tipo.Value & "<br>") '& celda.Text & "<br>")
                        'Response.Write(tipo.Value & "<br>") '& celda.Text & "<br>")
                    Next
                Next
                Objcnx.TerminarTransaccion()
                Page.RegisterStartupScript("Exito", "<SCRIPT>alert('Se actualizaron los datos correctamente'); </SCRIPT>")
            Else
                Page.RegisterStartupScript("SinRegistros", "<script>alert('No hay registros para guardar')</script>")
            End If
        Catch ex As Exception
            Objcnx.AbortarTransaccion()
            Page.RegisterStartupScript("Fallo", "<script>alert('Ocurrió un error al grabar los datos')</script>")
        Finally
            Objcnx = Nothing
        End Try
    End Sub

End Class
