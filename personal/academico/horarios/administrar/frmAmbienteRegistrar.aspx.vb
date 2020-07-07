
Partial Class academico_horarios_administrar_frmAmbienteRegistrar
    Inherits System.Web.UI.Page
    Private Shared prevPage As String = String.Empty
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If IsPostBack = False Then
            prevPage = Request.UrlReferrer.ToString()
            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            ClsFunciones.LlenarListas(Me.ddlTipoAmbiente, obj.TraerDataTable("AsignarAmbiente_ListarAmbientes"), "codigo_tam", "descripcion_Tam")
            ClsFunciones.LlenarListas(Me.ddlUbicacion, obj.TraerDataTable("Ambiente_ListarUbicacion"), "codigo_ube", "descripcion_ube")
            'Me.ddlTipoAmbiente.SelectedValue = 3 'Aula                  
            Me.ddlTipoAmbiente.SelectedValue = Session("ddlTipoAmbiente")
            Me.ddlUbicacion.SelectedValue = Session("ddlUbicacion")  ' 1 Edificio antiguo
            obj.CerrarConexion()
            Me.chckActivo.Checked = True
            CargarCaracteristicas()
            If Page.Request.QueryString("codigo_amb") > 0 Then
                obj.AbrirConexion()
                Dim tb As New Data.DataTable
                tb = obj.TraerDataTable("Ambiente_ConsultarAmbiente", "A", CInt(Page.Request.QueryString("codigo_amb")))
                obj.CerrarConexion()
                Me.txtNombreReal.Text = tb.Rows(0).Item("descripcionReal_Amb").ToString
                Me.txtAbrevReal.Text = tb.Rows(0).Item("abreviaturaReal_Amb").ToString
                Me.txtNombreFicticio.Text = tb.Rows(0).Item("descripcion_Amb").ToString
                Me.txtAbrevFicticia.Text = tb.Rows(0).Item("abreviatura_Amb").ToString
                Me.ddlTipoAmbiente.SelectedValue = tb.Rows(0).Item("codigo_Tam").ToString
                Me.ddlUbicacion.SelectedValue = tb.Rows(0).Item("codigo_ube").ToString
                Me.txtCapacidad.Text = tb.Rows(0).Item("capacidad_Amb").ToString
                Me.chckActivo.Checked = tb.Rows(0).Item("estado_amb").ToString
                Me.chkPreferencial.Checked = tb.Rows(0).Item("preferencial_amb").ToString
                Me.chkDisponible.checked = tb.rows(0).item("disponible_solicitud").ToString ' 06/01/2020
                tb.Dispose()
            End If
            obj = Nothing
        End If
    End Sub
    Sub CargarCaracteristicas()        
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Me.gridAudio.DataSource = obj.TraerDataTable("Ambiente_ListarCaracteristicas", "audio")
        Me.gridAudio.DataBind()
        Me.gridDistribucion.DataSource = obj.TraerDataTable("Ambiente_ListarCaracteristicas", "distribución")
        Me.gridDistribucion.DataBind()
        Me.gridOtros.DataSource = obj.TraerDataTable("Ambiente_ListarCaracteristicas", "otros")
        Me.gridOtros.DataBind()
        Me.gridSillas.DataSource = obj.TraerDataTable("Ambiente_ListarCaracteristicas", "sillas")
        Me.gridSillas.DataBind()
        Me.gridVideo.DataSource = obj.TraerDataTable("Ambiente_ListarCaracteristicas", "video")
        Me.gridVideo.DataBind()
        Me.gridVenti.DataSource = obj.TraerDataTable("Ambiente_ListarCaracteristicas", "ventilación")
        Me.gridVenti.DataBind()
        obj.CerrarConexion()
        obj = Nothing
    End Sub

    Protected Sub btnAceptar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAceptar.Click
        Dim Fila As GridViewRow
        Dim codigo_camb As Integer = 0
        Dim codigo_amb As Integer = 0
        Dim valor As Boolean
        Dim tb As New Data.DataTable
        Dim tbDupli As New Data.DataTable
        Dim obj As New ClsConectarDatos

        'Verificar duplicidad de nombres en ambientes'
        codigo_amb = Page.Request.QueryString("codigo_amb")
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        tbDupli = obj.TraerDataTable("Ambiente_BuscarDuplicidad", txtNombreFicticio.Text.Trim, txtAbrevFicticia.Text.Trim, Me.txtNombreReal.Text.Trim, txtAbrevReal.Text.Trim, codigo_amb)
        obj.CerrarConexion()

        If tbDupli.Rows.Count = 0 Then
            If Page.Request.QueryString("codigo_amb") > 0 Then
                codigo_amb = Page.Request.QueryString("codigo_amb")
                obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
                obj.AbrirConexion()
                obj.TraerDataTable("Ambiente_Actualizar", codigo_amb, Me.txtNombreReal.Text.Trim, txtAbrevReal.Text.Trim, txtNombreFicticio.Text.Trim, txtAbrevFicticia.Text.Trim, Me.ddlTipoAmbiente.SelectedValue, Me.ddlUbicacion.SelectedValue, IIf(Me.chckActivo.Checked, 1, 0), IIf(Me.chkPreferencial.Checked, 1, 0), CInt(txtCapacidad.Text.Trim), IIf(Me.chkDisponible.Checked, 1, 0))
                obj.CerrarConexion()
            Else
                obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
                obj.AbrirConexion()
                tb = obj.TraerDataTable("Ambiente_Registrar", Me.txtNombreReal.Text.Trim, txtAbrevReal.Text.Trim, txtNombreFicticio.Text.Trim, txtAbrevFicticia.Text.Trim, Me.ddlTipoAmbiente.SelectedValue, Me.ddlUbicacion.SelectedValue, IIf(Me.chckActivo.Checked, 1, 0), IIf(Me.chkPreferencial.Checked, 1, 0), CInt(txtCapacidad.Text.Trim), IIf(Me.chkDisponible.Checked, 1, 0))
                obj.CerrarConexion()
                If tb.Rows.Count Then
                    codigo_amb = CInt(tb.Rows(0).Item(0).ToString)
                Else
                    Me.lblMensaje.Text = "Ha ocurrido un error."
                    Exit Sub
                End If
            End If

            If codigo_amb > 0 Then
                With Me.gridAudio
                    For i As Integer = 0 To .Rows.Count - 1
                        Fila = .Rows(i)
                        codigo_camb = .DataKeys(i).Values("codigo_camb").ToString
                        valor = CType(Fila.FindControl("chkElegirAudio"), CheckBox).Checked
                        obj.AbrirConexion()
                        obj.Ejecutar("Ambiente_RegistrarCaracteristica", codigo_camb, codigo_amb, IIf(valor, 1, 0))
                        obj.CerrarConexion()
                    Next
                End With

                With Me.gridVideo
                    For i As Integer = 0 To .Rows.Count - 1
                        Fila = .Rows(i)
                        codigo_camb = .DataKeys(i).Values("codigo_camb").ToString
                        valor = CType(Fila.FindControl("chkElegirVideo"), CheckBox).Checked
                        obj.AbrirConexion()
                        obj.Ejecutar("Ambiente_RegistrarCaracteristica", codigo_camb, codigo_amb, IIf(valor, 1, 0))
                        obj.CerrarConexion()
                    Next
                End With

                With Me.gridSillas
                    For i As Integer = 0 To .Rows.Count - 1
                        Fila = .Rows(i)
                        codigo_camb = .DataKeys(i).Values("codigo_camb").ToString
                        valor = CType(Fila.FindControl("chkElegirSillas"), CheckBox).Checked
                        obj.AbrirConexion()
                        obj.Ejecutar("Ambiente_RegistrarCaracteristica", codigo_camb, codigo_amb, IIf(valor, 1, 0))
                        obj.CerrarConexion()
                    Next
                End With

                With Me.gridDistribucion
                    For i As Integer = 0 To .Rows.Count - 1
                        Fila = .Rows(i)
                        codigo_camb = .DataKeys(i).Values("codigo_camb").ToString
                        valor = CType(Fila.FindControl("chkElegirDistribucion"), CheckBox).Checked
                        obj.AbrirConexion()
                        obj.Ejecutar("Ambiente_RegistrarCaracteristica", codigo_camb, codigo_amb, IIf(valor, 1, 0))
                        obj.CerrarConexion()
                    Next
                End With

                With Me.gridOtros
                    For i As Integer = 0 To .Rows.Count - 1
                        Fila = .Rows(i)
                        codigo_camb = .DataKeys(i).Values("codigo_camb").ToString
                        valor = CType(Fila.FindControl("chkElegirOtros"), CheckBox).Checked
                        obj.AbrirConexion()
                        obj.Ejecutar("Ambiente_RegistrarCaracteristica", codigo_camb, codigo_amb, IIf(valor, 1, 0))
                        obj.CerrarConexion()
                    Next
                End With

                With Me.gridVenti
                    For i As Integer = 0 To .Rows.Count - 1
                        Fila = .Rows(i)
                        codigo_camb = .DataKeys(i).Values("codigo_camb").ToString
                        valor = CType(Fila.FindControl("chkElegirVenti"), CheckBox).Checked
                        obj.AbrirConexion()
                        obj.Ejecutar("Ambiente_RegistrarCaracteristica", codigo_camb, codigo_amb, IIf(valor, 1, 0))
                        obj.CerrarConexion()
                    Next
                End With
                'Me.lblMensaje.Text = "Se ha registrado el ambiente correctamente."
                obj = Nothing
                'Me.RegisterStartupScript("cerrar", "<script>alert('Se ha " & IIf(Page.Request.QueryString("codigo_amb") > 0, "actualizado", "registrado") & " el ambiente.');window.location.href='frmAmbientes.aspx';</script>")

            End If
            tbDupli.Dispose()
            Response.Redirect(prevPage)
          
        Else
            tbDupli.Dispose()
            Me.lblMensaje.Text = "Nombres duplicados>> Datos del Ambiente>> Nombre real:. " & tbDupli.Rows(0).Item("descripcionReal_Amb") & " / Abreviatura Real:. " & tbDupli.Rows(0).Item("abreviaturaReal_Amb") & " / Nombre Ficticio:. " & tbDupli.Rows(0).Item("descripcion_Amb") & " / Abreviatura Ficticia:. " & tbDupli.Rows(0).Item("abreviatura_Amb")
        End If


    End Sub

    
    Protected Sub gridAudio_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridAudio.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            If Page.Request.QueryString("codigo_amb") > 0 Then
                Dim obj As New ClsConectarDatos
                obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
                obj.AbrirConexion()
                Dim tb As New Data.DataTable
                Dim check As New CheckBox
                check = e.Row.FindControl("chkElegirAudio")
                tb = obj.TraerDataTable("Ambiente_ListarCaracteristicas", "audio", Page.Request.QueryString("codigo_amb"))
                If tb.Rows.Count Then                   
                    For i As Integer = 0 To tb.Rows.Count - 1
                        If tb.Rows(i).Item("codigo_camb") = gridAudio.DataKeys(e.Row.RowIndex).Values("codigo_camb").ToString Then
                            check.Checked = True
                        End If
                    Next
                End If
                obj.CerrarConexion()
                obj = Nothing
            End If
        End If
    End Sub

    Protected Sub gridVideo_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridVideo.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            If Page.Request.QueryString("codigo_amb") > 0 Then
                Dim obj As New ClsConectarDatos
                obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
                obj.AbrirConexion()
                Dim tb As New Data.DataTable
                Dim check As New CheckBox
                check = e.Row.FindControl("chkElegirVideo")
                tb = obj.TraerDataTable("Ambiente_ListarCaracteristicas", "Video", Page.Request.QueryString("codigo_amb"))
                If tb.Rows.Count Then
                    For i As Integer = 0 To tb.Rows.Count - 1
                        If tb.Rows(i).Item("codigo_camb") = gridVideo.DataKeys(e.Row.RowIndex).Values("codigo_camb").ToString Then
                            check.Checked = True
                        End If
                    Next
                End If
                obj.CerrarConexion()
                obj = Nothing
                tb.Dispose()
            End If
        End If
    End Sub

    Protected Sub gridSillas_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridSillas.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            If Page.Request.QueryString("codigo_amb") > 0 Then
                Dim obj As New ClsConectarDatos
                obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
                obj.AbrirConexion()
                Dim tb As New Data.DataTable
                Dim check As New CheckBox
                check = e.Row.FindControl("chkElegirSillas")
                tb = obj.TraerDataTable("Ambiente_ListarCaracteristicas", "Sillas", Page.Request.QueryString("codigo_amb"))
                If tb.Rows.Count Then
                    For i As Integer = 0 To tb.Rows.Count - 1
                        If tb.Rows(i).Item("codigo_camb") = gridSillas.DataKeys(e.Row.RowIndex).Values("codigo_camb").ToString Then
                            check.Checked = True
                        End If
                    Next
                End If
                obj.CerrarConexion()
                obj = Nothing
                tb.Dispose()
            End If
        End If
    End Sub

    Protected Sub gridDistribucion_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridDistribucion.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            If Page.Request.QueryString("codigo_amb") > 0 Then
                Dim obj As New ClsConectarDatos
                obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
                obj.AbrirConexion()
                Dim tb As New Data.DataTable
                Dim check As New CheckBox
                check = e.Row.FindControl("chkElegirDistribucion")
                tb = obj.TraerDataTable("Ambiente_ListarCaracteristicas", "Distribución", Page.Request.QueryString("codigo_amb"))
                If tb.Rows.Count Then
                    For i As Integer = 0 To tb.Rows.Count - 1
                        If tb.Rows(i).Item("codigo_camb") = gridDistribucion.DataKeys(e.Row.RowIndex).Values("codigo_camb").ToString Then
                            check.Checked = True
                        End If
                    Next
                End If
                obj.CerrarConexion()
                obj = Nothing
                tb.Dispose()
            End If
        End If
    End Sub

    Protected Sub gridOtros_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridOtros.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            If Page.Request.QueryString("codigo_amb") > 0 Then
                Dim obj As New ClsConectarDatos
                obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
                obj.AbrirConexion()
                Dim tb As New Data.DataTable
                Dim check As New CheckBox
                check = e.Row.FindControl("chkElegirOtros")
                tb = obj.TraerDataTable("Ambiente_ListarCaracteristicas", "Otros", Page.Request.QueryString("codigo_amb"))
                If tb.Rows.Count Then
                    For i As Integer = 0 To tb.Rows.Count - 1
                        If tb.Rows(i).Item("codigo_camb") = gridOtros.DataKeys(e.Row.RowIndex).Values("codigo_camb").ToString Then
                            check.Checked = True
                        End If
                    Next
                End If
                obj.CerrarConexion()
                obj = Nothing
                tb.Dispose()
            End If
        End If
    End Sub



    Protected Sub gridVenti_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridVenti.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            If Page.Request.QueryString("codigo_amb") > 0 Then
                Dim obj As New ClsConectarDatos
                obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
                obj.AbrirConexion()
                Dim tb As New Data.DataTable
                Dim check As New CheckBox
                check = e.Row.FindControl("chkElegirVenti")
                tb = obj.TraerDataTable("Ambiente_ListarCaracteristicas", "Ventilación", Page.Request.QueryString("codigo_amb"))
                If tb.Rows.Count Then
                    For i As Integer = 0 To tb.Rows.Count - 1
                        If tb.Rows(i).Item("codigo_camb") = gridVenti.DataKeys(e.Row.RowIndex).Values("codigo_camb").ToString Then
                            check.Checked = True
                        End If
                    Next
                End If
                obj.CerrarConexion()
                obj = Nothing
                tb.Dispose()
            End If
        End If
    End Sub

   
End Class
