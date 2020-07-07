
Partial Class administrativo_frmReportePostulantes
    Inherits System.Web.UI.Page
    Public modulo As Integer ' 1 pensiones 2 epu

    Protected Sub grwListaPersonas_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grwListaPersonas.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim fila As Data.DataRowView
            fila = e.Row.DataItem
            e.Row.Cells(0).Text = e.Row.RowIndex + 1
        End If
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try
            ConsultarCeco()
            If IsPostBack = False Then
                CargarComboProcesosAdmision()
                CargarComboCeco()
                CargarComboModalidadIngreso()
                CargarComboFiltro()
            End If

            btnAccion.Attributes.Add("language", "javascript")
            btnAccion.Attributes.Add("OnClick", "return confirm('¿Está seguro que desea ejecutar esta acción?');")
            'btnBuscar.Attributes.Add("onclick", "javascript:DisplayIddleWarning();")            
            'btnAccion.Attributes.Add("onclick", "javascript:DisplayIddleWarning()")                        

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try

    End Sub

    Private Sub ConsultarCeco()
        Try
            Dim obj As New ClsConectarDatos
            Dim tbl As Data.DataTable
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()

            tbl = obj.TraerDataTable("CEC_ConsultarPersonalEnCeCo", Request.QueryString("id"), "pensiones")

            If tbl.Rows(0).Item("codigo_Per") <> 0 Then
                modulo = 1
            End If

            tbl = obj.TraerDataTable("CEC_ConsultarPersonalEnCeCo", Request.QueryString("id"), "escuela pre")
            If tbl.Rows(0).Item("codigo_Per") <> 0 Then
                modulo = 2
            End If

            obj.CerrarConexion()
            obj = Nothing

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub
    Private Sub CargarComboProcesosAdmision()
        Try
            Dim objfun As New ClsFunciones
            Dim obj As New ClsConectarDatos

            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            objfun.CargarListas(ddlProceso, obj.TraerDataTable("ConsultarCicloAcademico", "CI2", ""), "cicloIng_Alu", "cicloIng_Alu", ">> Seleccione<<")

            obj.CerrarConexion()
            obj = Nothing
            objfun = Nothing
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub CargarComboCeco()
        Try
            Dim objfun As New ClsFunciones
            Dim obj As New ClsConectarDatos

            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            objfun.CargarListas(ddlCeco, obj.TraerDataTable("EVE_ConsultarCentroCostosXPermisos", Request.QueryString("ctf"), Request.QueryString("id"), "", Request.QueryString("mod")), "codigo_Cco", "Nombre", ">> Seleccione<<")
            obj.CerrarConexion()
            obj = Nothing
            objfun = Nothing
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub CargarComboModalidadIngreso()
        Try
            Dim objfun As New ClsFunciones
            Dim obj As New ClsConectarDatos

            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            objfun.CargarListas(ddlModalidad, obj.TraerDataTable("ConsultarModalidadIngreso", "TO", ""), "codigo_Min", "nombre_Min", ">> Seleccione<<")

            obj.CerrarConexion()
            obj = Nothing
            objfun = Nothing
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try

    End Sub

    Private Sub CargarInscritosConCargo()
        Try
            Dim obj As New ClsConectarDatos

            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()

            'Envío mod para que solo liste alummos con cecos de escuela pre 
            Me.grwListaPersonas.DataSource = obj.TraerDataTable("EPRE_ListarPostulantes", IIf(ddlProceso.SelectedValue = "-1", "%", ddlProceso.SelectedValue), IIf(ddlCeco.SelectedValue = -1, 0, ddlCeco.SelectedValue), IIf(ddlModalidad.SelectedValue = -1, 0, ddlModalidad.SelectedValue), txtDNI.Text.Trim, txtCodigoUni.Text, txtNombres.Text, ddlEstPostulacion.SelectedValue, Request.QueryString("mod"), 0)

            If modulo = 1 Then
                grwListaPersonas.Columns(15).Visible = True
            ElseIf modulo = 2 Then
                grwListaPersonas.Columns(15).Visible = False
            End If

            Me.grwListaPersonas.DataBind()
            obj.CerrarConexion()
            obj = Nothing
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try

    End Sub

    Private Sub CargarComboFiltro()
        Try

            If modulo = 1 Then      ' pensiones
                ddlAccion.Items.Add(New ListItem("Categorizar", "C"))
                ddlAccion.Items.Add(New ListItem("Imprimir Carta", "I"))
            End If

            If modulo = 2 Then      ' epu
                ddlAccion.Items.Add(New ListItem("Activar Ingreso", "AI"))
                ddlAccion.Items.Add(New ListItem("Retirar", "R"))
            End If

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    'Protected Sub cmdExportar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdExportar.Click
    '    Dim sb As StringBuilder = New StringBuilder()
    '    Dim SW As System.IO.StringWriter = New System.IO.StringWriter(sb)
    '    Dim htw As HtmlTextWriter = New HtmlTextWriter(SW)
    '    Dim Page As Page = New Page()
    '    Dim form As HtmlForm = New HtmlForm()
    '    Me.grwListaPersonas.EnableViewState = False
    '    Page.EnableEventValidation = False
    '    Page.DesignerInitialize()
    '    Page.Controls.Add(form)
    '    form.Controls.Add(Me.grwListaPersonas)
    '    Page.RenderControl(htw)
    '    Response.Clear()
    '    Response.Buffer = True
    '    Response.ContentType = "application/vnd.ms-excel"
    '    Response.AddHeader("Content-Disposition", "attachment;filename=inscritos_cargo" & ".xls")
    '    Response.Charset = "UTF-8"
    '    Response.ContentEncoding = Encoding.Default
    '    Response.Write(sb.ToString())
    '    Response.End()
    'End Sub

    Protected Sub ddlFiltro_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlFiltro.SelectedIndexChanged
        Try
            If ddlFiltro.SelectedValue = "D" Then
                txtDNI.Visible = True
                txtNombres.Visible = False
                txtCodigoUni.Visible = False
                txtNombres.Text = ""
                txtCodigoUni.Text = ""
            ElseIf ddlFiltro.SelectedValue = "N" Then
                txtDNI.Visible = False
                txtNombres.Visible = True
                txtCodigoUni.Visible = False
                txtDNI.Text = ""
                txtCodigoUni.Text = ""
            ElseIf ddlFiltro.SelectedValue = "CU" Then
                txtDNI.Visible = False
                txtNombres.Visible = False
                txtCodigoUni.Visible = True
                txtDNI.Text = ""
                txtNombres.Text = ""
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub btnBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBuscar.Click
        Try

            divresumen.InnerHtml = ""
            CargarInscritosConCargo()
            'HabilitarDeshabilitarCheckBox()

            '==========================================================
            'Cerrar la ventana modal cuando culmine la carga del grid
            '==========================================================
            'Dim sbMensaje2 As New StringBuilder
            ''Mostramos mensaje con javascript, como ventana no modal
            'sbMensaje2.Append("<script type='text/javascript'>")
            'sbMensaje2.AppendFormat("HideIddleWarning();")
            'sbMensaje2.Append("</script>")
            ''Registramos el Script escrito en el StringBuilder        
            'ScriptManager.RegisterStartupScript(btnBuscar, Me.GetType(), "mensaje", sbMensaje2.ToString, False)

            ''If (Not ClientScript.IsStartupScriptRegistered("JSScript")) Then
            ''    ClientScript.RegisterStartupScript(Me.GetType(), "JSScript", sbMensaje.ToString())
            ''End If

            '==========================================================

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    'Protected Sub chkSel_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs)

    '    Dim sbMensaje As New StringBuilder

    '    Dim chk As CheckBox = sender
    '    Dim fila As GridViewRow = chk.NamingContainer
    '    Dim idalu_seleccionado As Integer
    '    Dim idalu_fila As Integer
    '    Dim codigopso_seleccionado As Integer
    '    Dim codigopso_fila As Integer
    '    Dim mensaje As String

    '    idalu_seleccionado = grwListaPersonas.DataKeys(fila.RowIndex).Values("codigo_Alu")
    '    codigopso_seleccionado = grwListaPersonas.DataKeys(fila.RowIndex).Values("codigo_pso")

    '    divresumen.InnerHtml = ""
    '    mensaje = ""

    '    '========================================================
    '    'Verificar que no exista en la base de datos la misma persona, con estado ingresante
    '    '========================================================
    '    Dim obj As New ClsConectarDatos
    '    Dim tbl As Data.DataTable
    '    obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
    '    obj.AbrirConexion()

    '    tbl = obj.TraerDataTable("EPRE_ListarAlumnosPorPersona", ddlProceso.SelectedValue, fila.Cells(2).Text, "I", Request.QueryString("mod"), idalu_seleccionado)

    '    If tbl.Rows.Count() > 0 Then
    '        mensaje = mensaje & "No se puede " & _
    '                 "asignar como Ingresante al participante Número " & _
    '                 fila.RowIndex + 1 & " porque ya se ha elegido como ingresante" & _
    '                 " en otra modalidad y/o proceso de admisión"

    '        'Mostramos mensaje con javascript, como ventana no modal
    '        sbMensaje.Append("<script type='text/javascript'>")
    '        sbMensaje.AppendFormat("apprise('{0}');", mensaje)
    '        sbMensaje.Append("</script>")
    '        'Registramos el Script escrito en el StringBuilder        
    '        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "mensaje", sbMensaje.ToString, False)

    '        'Tambien mostramos mensaje en la página
    '        divresumen.InnerHtml = divresumen.InnerHtml & mensaje

    '        chk.Checked = False
    '        Exit Sub
    '    End If

    '    If grwListaPersonas.DataKeys(fila.RowIndex).Values("otroalu") = 1 Then
    '        If ddlAccion.SelectedValue = "AI" Then
    '            'Response.Write("alu: ")
    '            'Response.Write(idalu_seleccionado)
    '            'Response.Write("pso: ")
    '            'Response.Write(codigopso_seleccionado)

    '            '========================================================
    '            'Verificar que solo sea un Ingresante por Alumno
    '            '========================================================

    '            If chk.Checked Then

    '                For Each row As GridViewRow In grwListaPersonas.Rows
    '                    idalu_fila = grwListaPersonas.DataKeys(row.RowIndex).Values("codigo_Alu")
    '                    codigopso_fila = grwListaPersonas.DataKeys(row.RowIndex).Values("codigo_pso")

    '                    'Response.Write("alufila: ")
    '                    'Response.Write(idalu_fila)
    '                    'Response.Write("psofila: ")
    '                    'Response.Write(codigopso_fila)

    '                    If codigopso_seleccionado = codigopso_fila And idalu_seleccionado <> idalu_fila Then
    '                        Dim check As CheckBox = TryCast(row.FindControl("chkSel"), CheckBox)

    '                        'Verificar que no hay otra fila con el mismo codigo_pso marcada como ingresante
    '                        If check.Checked Then
    '                            mensaje = mensaje & "No se puede " & _
    '                            "asignar como Ingresante al participante Número " & _
    '                            fila.RowIndex + 1 & " porque ya se ha elegido como ingresante" & _
    '                            " en otra modalidad y/o proceso de admisión"

    '                            'Mostramos mensaje con javascript, como ventana no modal
    '                            sbMensaje.Append("<script type='text/javascript'>")
    '                            sbMensaje.AppendFormat("apprise('{0}');", mensaje)
    '                            sbMensaje.Append("</script>")
    '                            'Registramos el Script escrito en el StringBuilder        
    '                            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "mensaje", sbMensaje.ToString, False)

    '                            'Tambien mostramos mensaje en la página
    '                            divresumen.InnerHtml = divresumen.InnerHtml & mensaje

    '                            chk.Checked = False
    '                        End If

    '                        'Verificar que no hay otra fila con el mismo codigo_pso con estadopostulacion de ingresante
    '                        If grwListaPersonas.DataKeys(row.RowIndex).Values("EstadoPostulacion") = "I" Then

    '                            mensaje = mensaje & "No se puede " & _
    '                            "asignar como Ingresante al participante Número " & _
    '                            fila.RowIndex + 1 & " porque ya se ha elegido como ingresante" & _
    '                            " en otra modalidad y/o proceso de admisión"

    '                            'Mostramos mensaje con javascript, como ventana no modal
    '                            sbMensaje.Append("<script type='text/javascript'>")
    '                            sbMensaje.AppendFormat("apprise('{0}');", mensaje)
    '                            sbMensaje.Append("</script>")
    '                            'Registramos el Script escrito en el StringBuilder        
    '                            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "mensaje", sbMensaje.ToString, False)

    '                            'Tambien mostramos mensaje en la página
    '                            divresumen.InnerHtml = divresumen.InnerHtml & mensaje

    '                            chk.Checked = False
    '                        End If


    '                    End If
    '                Next
    '                '========================================================
    '                'Verificar que no se pueda quitar el estado Ingresante cuando tenga categorizacion y carta impresa
    '                '========================================================
    '                'Else
    '                '    If grwListaPersonas.DataKeys(fila.RowIndex).Values("imprimiocartacat_Dal") = 1 Then

    '                '        mensaje = mensaje & _
    '                '         "No se puede quitar el Estado de ingresante al participante Número " & _
    '                '            fila.RowIndex + 1 & _
    '                '            " porque cuenta con categorización y/o carta de categorización impresa."

    '                '        'Mostramos mensaje con javascript, como ventana no modal
    '                '        sbMensaje.Append("<script type='text/javascript'>")
    '                '        sbMensaje.AppendFormat("apprise('{0}');", mensaje)
    '                '        sbMensaje.Append("</script>")
    '                '        'Registramos el Script escrito en el StringBuilder        
    '                '        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "mensaje", sbMensaje.ToString, False)

    '                '        'Tambien mostramos mensaje en la página
    '                '        divresumen.InnerHtml = divresumen.InnerHtml & mensaje

    '                '        chk.Checked = False
    '                '    End If

    '            End If
    '        End If
    '    End If

    '    '========================================================
    '    'Verificar que no se pueda retirar cuando tenga categorizacion y carta impresa
    '    '========================================================

    '    'If ddlAccion.SelectedValue = "R" Then
    '    '    If chk.Checked Then
    '    '        If grwListaPersonas.DataKeys(fila.RowIndex).Values("imprimiocartacat_Dal") = 1 _
    '    '            Or grwListaPersonas.DataKeys(fila.RowIndex).Values("Categorizacion") > 0 Then

    '    '            mensaje = mensaje & _
    '    '                "No se puede retirar al participante Número " & _
    '    '                fila.RowIndex + 1 & _
    '    '                " porque cuenta con categorización y/o carta de categorización impresa."

    '    '            'Mostramos mensaje con javascript, como ventana no modal
    '    '            sbMensaje.Append("<script type='text/javascript'>")
    '    '            sbMensaje.AppendFormat("apprise('{0}');", mensaje)
    '    '            sbMensaje.Append("</script>")
    '    '            'Registramos el Script escrito en el StringBuilder        
    '    '            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "mensaje", sbMensaje.ToString, False)

    '    '            'Tambien mostramos mensaje en la página
    '    '            divresumen.InnerHtml = divresumen.InnerHtml & mensaje

    '    '            chk.Checked = False
    '    '        End If
    '    '    End If
    '    'End If

    '    '========================================================
    '    'Verificar que no se pueda volver a categorizar cuando tenga carta impresa o cuando el participate esté como postulante
    '    '========================================================
    '    If ddlAccion.SelectedValue = "C" Then
    '        If chk.Checked Then
    '            If grwListaPersonas.DataKeys(fila.RowIndex).Values("imprimiocartacat_Dal") = 1 Then
    '                mensaje = mensaje & _
    '                    "No se puede volver a categorizar al participante Número " & _
    '                    fila.RowIndex + 1 & _
    '                    ", pues ya cuenta con carta de categorización impresa."

    '                'Mostramos mensaje con javascript, como ventana no modal
    '                sbMensaje.Append("<script type='text/javascript'>")
    '                sbMensaje.AppendFormat("apprise('{0}');", mensaje)
    '                sbMensaje.Append("</script>")
    '                'Registramos el Script escrito en el StringBuilder        
    '                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "mensaje", sbMensaje.ToString, False)

    '                'Tambien mostramos mensaje en la página
    '                divresumen.InnerHtml = divresumen.InnerHtml & mensaje


    '                chk.Checked = False
    '            End If

    '            If grwListaPersonas.DataKeys(fila.RowIndex).Values("EstadoPostulacion") = "P" Then
    '                mensaje = mensaje & _
    '                    "No se puede categorizar al participante Número " & _
    '                    fila.RowIndex + 1 & _
    '                    " porque no es Ingresante."

    '                'Mostramos mensaje con javascript, como ventana no modal
    '                sbMensaje.Append("<script type='text/javascript'>")
    '                sbMensaje.AppendFormat("apprise('{0}');", mensaje)
    '                sbMensaje.Append("</script>")
    '                'Registramos el Script escrito en el StringBuilder        
    '                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "mensaje", sbMensaje.ToString, False)

    '                'Tambien mostramos mensaje en la página
    '                divresumen.InnerHtml = divresumen.InnerHtml & mensaje

    '                chk.Checked = False
    '            End If
    '        End If
    '    End If

    '    ''==========================================================
    '    ''Cerrar la ventana modal cuando culmine la función
    '    ''==========================================================

    '    ''Mostramos mensaje con javascript, como ventana no modal
    '    'sbMensaje.Append("<script type='text/javascript'>")
    '    'sbMensaje.AppendFormat("HideIddleWarning();")
    '    'sbMensaje.Append("</script>")
    '    ''Registramos el Script escrito en el StringBuilder        
    '    'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "mensaje", sbMensaje.ToString, False)
    '    ''==========================================================

    'End Sub

    'Protected Sub btnMensajeJavascript_Click(ByVal sender As Object, ByVal e As System.EventArgs)

    '    Dim sbMensaje As New StringBuilder
    '    sbMensaje.Append("<script type='text/javascript'>")
    '    sbMensaje.AppendFormat("alert('{0}');", txtMensaje.Text)
    '    sbMensaje.Append("</script>")
    '    'Registramos el Script escrito en el StringBuilder
    '    ClientScript.RegisterClientScriptBlock(Me.GetType(), "mensaje", sbMensaje.ToString())
    'End Sub

    Protected Sub btnAccion_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAccion.Click
        Try
            ValidarSeleccionGrid()

            'Dim objfun As New ClsFunciones
            Dim obj As New ClsConectarDatos
            Dim codigoAlu_fila As Integer
            Dim sbMensaje As New StringBuilder

            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString

            obj.IniciarTransaccion()

            If ddlAccion.SelectedValue = "AI" Then
                For Each row As GridViewRow In grwListaPersonas.Rows
                    codigoAlu_fila = grwListaPersonas.DataKeys(row.RowIndex).Values("codigo_Alu")
                    Dim check As CheckBox = TryCast(row.FindControl("chkSel"), CheckBox)
                    Dim txtobs As TextBox = TryCast(row.FindControl("txtObservacion"), TextBox)
                    Dim txtnota As TextBox = TryCast(row.FindControl("txtNota"), TextBox)

                    If check.Checked Then
                        obj.Ejecutar("EPU_ActivarEstadoPostulacion", codigoAlu_fila, "I", txtobs.Text, txtnota.Text)
                    ElseIf check.Checked = False And check.Visible = True Then
                        obj.Ejecutar("EPU_ActivarEstadoPostulacion", codigoAlu_fila, "P", txtobs.Text, txtnota.Text)
                    End If
                Next
            End If

            If ddlAccion.SelectedValue = "R" Then

                For Each row As GridViewRow In grwListaPersonas.Rows
                    codigoAlu_fila = grwListaPersonas.DataKeys(row.RowIndex).Values("codigo_Alu")
                    Dim check As CheckBox = TryCast(row.FindControl("chkSel"), CheckBox)
                    Dim txtobs As TextBox = TryCast(row.FindControl("txtObservacion"), TextBox)

                    If check.Checked Then
                        obj.Ejecutar("EPU_ActivarEstadoPostulacion", codigoAlu_fila, "R", txtobs.Text)
                    End If
                Next
            End If

            If ddlAccion.SelectedValue = "C" Then
                For Each row As GridViewRow In grwListaPersonas.Rows
                    codigoAlu_fila = grwListaPersonas.DataKeys(row.RowIndex).Values("codigo_Alu")
                    Dim check As CheckBox = TryCast(row.FindControl("chkSel"), CheckBox)
                    Dim txtcat As TextBox = TryCast(row.FindControl("txtCategorizacion"), TextBox)

                    If check.Checked Then
                        obj.Ejecutar("EPU_AsignarCategorizacion", codigoAlu_fila, CDec(txtcat.Text) / 5)
                    End If
                Next
            End If

            If ddlAccion.SelectedValue = "I" Then
                For Each row As GridViewRow In grwListaPersonas.Rows
                    codigoAlu_fila = grwListaPersonas.DataKeys(row.RowIndex).Values("codigo_Alu")
                    Dim check As CheckBox = TryCast(row.FindControl("chkSel"), CheckBox)
                    Dim txtcat As TextBox = TryCast(row.FindControl("txtCategorizacion"), TextBox)
                    Dim txtobs As TextBox = TryCast(row.FindControl("txtObservacion"), TextBox)

                    If check.Checked Then
                        obj.Ejecutar("PENS_ActivarEstadoImpresion", codigoAlu_fila, txtobs.Text)
                    End If
                Next
            End If

            obj.TerminarTransaccion()
            obj = Nothing
            'objfun = Nothing


            CargarInscritosConCargo()

            ''==========================================================
            ''Cerrar la ventana modal cuando culmine la carga del grid
            ''==========================================================            
            ''Mostramos mensaje con javascript, como ventana no modal
            'sbMensaje.Append("<script type='text/javascript'>")
            'sbMensaje.AppendFormat("HideIddleWarning();")
            'sbMensaje.Append("</script>")
            ''Registramos el Script escrito en el StringBuilder        
            'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "mensaje", sbMensaje.ToString, False)
            ''==========================================================

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    '========================================================
    'Ocultar check, cajas de categorizacion
    '========================================================

    Protected Sub grwListaPersonas_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles grwListaPersonas.DataBound

        If Me.grwListaPersonas.Rows.Count Then

            For Each row As GridViewRow In grwListaPersonas.Rows
                Dim chk As CheckBox
                Dim txt As TextBox
                chk = TryCast(row.FindControl("chkSel"), CheckBox)
                txt = TryCast(row.FindControl("txtCategorizacion"), TextBox)

                If ddlProceso.SelectedValue = "-1" Then
                    chk.Enabled = False
                Else

                    If grwListaPersonas.DataKeys(row.RowIndex).Values("imprimiocartacat_Dal") = 1 Then
                        row.Cells(16).Text = "Impresa"
                    ElseIf grwListaPersonas.DataKeys(row.RowIndex).Values("imprimiocartacat_Dal") = 0 Then
                        row.Cells(16).Text = "No Impresa"
                    End If

                    If Trim(grwListaPersonas.DataKeys(row.RowIndex).Values("EstadoPostulacion")) = "I" Then
                        row.Cells(11).Text = "Ingresante"
                    ElseIf Trim(grwListaPersonas.DataKeys(row.RowIndex).Values("EstadoPostulacion")) = "P" Then
                        row.Cells(11).Text = "Postulante"
                    ElseIf Trim(grwListaPersonas.DataKeys(row.RowIndex).Values("EstadoPostulacion")) = "R" Then
                        row.Cells(11).Text = "Retirado"
                    End If

                    '========================================================
                    'Si está impresa la carta de categorización
                    '========================================================
                    If grwListaPersonas.DataKeys(row.RowIndex).Values("imprimiocartacat_Dal") = 1 Then
                        chk.Visible = False
                        txt.Visible = True
                        txt.Enabled = False
                    ElseIf grwListaPersonas.DataKeys(row.RowIndex).Values("imprimiocartacat_Dal") = 0 Then

                        '========================================================
                        'Si el participante está retirado
                        '========================================================
                        If Trim(grwListaPersonas.DataKeys(row.RowIndex).Values("EstadoPostulacion")) = "R" Then
                            chk.Visible = False
                            txt.Visible = False

                        ElseIf Trim(grwListaPersonas.DataKeys(row.RowIndex).Values("EstadoPostulacion")) = "P" Then
                            '========================================================
                            'Si el participante es Postulante, ocultar checks de catego. e impresión
                            '========================================================
                            If modulo = 2 Then 'escuela pre
                                chk.Visible = True
                            ElseIf modulo = 1 Then 'pensiones
                                chk.Visible = False
                                txt.Visible = False
                            End If
                        ElseIf Trim(grwListaPersonas.DataKeys(row.RowIndex).Values("EstadoPostulacion")) = "I" Then
                            '========================================================
                            'Opción de impresión solo cuando tienen categorización
                            '========================================================
                            txt.Visible = True
                            If Trim(grwListaPersonas.DataKeys(row.RowIndex).Values("Categorizacion")) > 0 Then
                                txt.Enabled = False

                                If ddlAccion.SelectedValue = "I" Then
                                    chk.Visible = True
                                Else
                                    chk.Visible = False
                                End If
                            Else
                                If ddlAccion.SelectedValue = "I" Then
                                    chk.Visible = False
                                    txt.Enabled = False
                                Else
                                    chk.Visible = True
                                End If
                            End If
                        End If
                    End If
                End If
            Next
        End If
    End Sub

    Protected Sub ddlAccion_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlAccion.SelectedIndexChanged
        Try
            ' ''==========================================================
            ' ''Cerrar la ventana modal cuando culmine la función
            ' ''==========================================================

            ' ''Mostramos mensaje con javascript, como ventana no modal
            'Dim sbMensaje As New StringBuilder
            'sbMensaje.Append("<script type='text/javascript'>")
            'sbMensaje.AppendFormat("DisplayIddleWarning();")
            'sbMensaje.Append("</script>")
            ''Registramos el Script escrito en el StringBuilder        
            'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "mensaje", sbMensaje.ToString, False)
            ' ''==========================================================

            CargarInscritosConCargo()

            ' ''==========================================================
            ' ''Cerrar la ventana modal cuando culmine la función
            ' ''==========================================================

            ' ''Mostramos mensaje con javascript, como ventana no modal
            'Dim sbMensaje2 As New StringBuilder
            'sbMensaje2.Append("<script type='text/javascript'>")
            'sbMensaje2.AppendFormat("HideIddleWarning();")
            'sbMensaje2.Append("</script>")
            ''Registramos el Script escrito en el StringBuilder        
            'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "mensaje", sbMensaje2.ToString, False)
            ' ''==========================================================
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub ValidarSeleccionGrid()
        Try
            Dim sbMensaje As New StringBuilder

            'Dim chk As CheckBox = sender
            'Dim fila As GridViewRow = chk.NamingContainer
            Dim idalu_seleccionado As Integer
            Dim idalu_fila As Integer
            Dim codigopso_seleccionado As Integer
            Dim codigopso_fila As Integer
            Dim mensaje As String
            Dim obj As New ClsConectarDatos
            Dim tbl As Data.DataTable
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()

            divresumen.InnerHtml = ""
            mensaje = ""

            'Recorrer grid
            For Each fila As GridViewRow In grwListaPersonas.Rows
                Dim chk As CheckBox = TryCast(fila.FindControl("chkSel"), CheckBox)
                idalu_seleccionado = grwListaPersonas.DataKeys(fila.RowIndex).Values("codigo_Alu")
                codigopso_seleccionado = grwListaPersonas.DataKeys(fila.RowIndex).Values("codigo_pso")

                If chk.Visible = True Then

                    '========================================================
                    'Validar un solo ingresante
                    '========================================================

                    '________________________________________________________
                    '1. Verificar en la base de datos                
                    tbl = obj.TraerDataTable("EPRE_ListarAlumnosPorPersona", ddlProceso.SelectedValue, fila.Cells(2).Text, "I", Request.QueryString("mod"), idalu_seleccionado)

                    If tbl.Rows.Count() > 0 Then

                        mensaje = mensaje & "No se puede " & _
                                 "asignar como Ingresante al participante Número " & _
                                 fila.RowIndex + 1 & " porque ya se ha elegido como ingresante" & _
                                 " en otra modalidad y/o proceso de admisión"
                        chk.Checked = False

                        'Mostramos mensaje con javascript, como ventana no modal
                        sbMensaje.Append("<script type='text/javascript'>")
                        sbMensaje.AppendFormat("apprise('{0}');", mensaje)
                        sbMensaje.Append("</script>")
                        'Registramos el Script escrito en el StringBuilder        
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "mensaje", sbMensaje.ToString, False)

                        'Tambien mostramos mensaje en la página
                        divresumen.InnerHtml = divresumen.InnerHtml & mensaje
                        Exit Sub
                    End If

                    '________________________________________________________
                    '2. Verificar en el grid

                    If grwListaPersonas.DataKeys(fila.RowIndex).Values("otroalu") = 1 Then
                        If ddlAccion.SelectedValue = "AI" Then
                            'Response.Write("alu: ")
                            'Response.Write(idalu_seleccionado)
                            'Response.Write("pso: ")
                            'Response.Write(codigopso_seleccionado)

                            '========================================================
                            'Verificar que solo sea un Ingresante por Alumno
                            '========================================================

                            If chk.Checked Then

                                For Each row As GridViewRow In grwListaPersonas.Rows
                                    idalu_fila = grwListaPersonas.DataKeys(row.RowIndex).Values("codigo_Alu")
                                    codigopso_fila = grwListaPersonas.DataKeys(row.RowIndex).Values("codigo_pso")

                                    'Response.Write("alufila: ")
                                    'Response.Write(idalu_fila)
                                    'Response.Write("psofila: ")
                                    'Response.Write(codigopso_fila)

                                    If codigopso_seleccionado = codigopso_fila And idalu_seleccionado <> idalu_fila Then
                                        Dim check As CheckBox = TryCast(row.FindControl("chkSel"), CheckBox)

                                        'Verificar que no hay otra fila con el mismo codigo_pso marcada como ingresante
                                        If check.Checked Then
                                            mensaje = mensaje & "No se puede " & _
                                            "asignar como Ingresante al participante Número " & _
                                            fila.RowIndex + 1 & " porque ya se ha elegido como ingresante" & _
                                            " en otra modalidad y/o proceso de admisión"

                                            chk.Checked = False
                                        End If
                                    End If
                                Next

                            End If
                        End If
                    End If


                    '========================================================
                    'Verificar que no se pueda volver a categorizar cuando tenga carta impresa o cuando el participate esté como postulante
                    '========================================================
                    If ddlAccion.SelectedValue = "C" Then
                        If chk.Checked Then
                            If grwListaPersonas.DataKeys(fila.RowIndex).Values("imprimiocartacat_Dal") = 1 Then
                                mensaje = mensaje & _
                                    "No se puede volver a categorizar al participante Número " & _
                                    fila.RowIndex + 1 & _
                                    ", pues ya cuenta con carta de categorización impresa."

                                chk.Checked = False
                            End If

                            If grwListaPersonas.DataKeys(fila.RowIndex).Values("EstadoPostulacion") = "P" Then
                                mensaje = mensaje & _
                                    "No se puede categorizar al participante Número " & _
                                    fila.RowIndex + 1 & _
                                    " porque no es Ingresante."

                                chk.Checked = False
                            End If
                        End If
                    End If
                End If

            Next

            If mensaje = "" Then
                mensaje = "La acción se ejecutó correctamente."
            End If
            'Mostramos mensaje con javascript, como ventana no modal
            sbMensaje.Append("<script type='text/javascript'>")
            sbMensaje.AppendFormat("apprise('{0}');", mensaje)
            sbMensaje.Append("</script>")
            'Registramos el Script escrito en el StringBuilder        
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "mensaje", sbMensaje.ToString, False)

            'Tambien mostramos mensaje en la páginas
            divresumen.InnerHtml = divresumen.InnerHtml & mensaje

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

End Class



