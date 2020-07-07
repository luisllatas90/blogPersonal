﻿Imports System.IO
Imports System.Security.Cryptography

Partial Class administrativo_tramite_FrmListaTramite
    Inherits System.Web.UI.Page

    Public Enum MessageType
        Success
        [Error]
        Info
        Warning
    End Enum

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (Session("id_per") Is Nothing) Then
            Response.Redirect("../../../../sinacceso.html")
        End If

        Try

            cpf.Value = Me.ddlEscuela.SelectedValue

            If cpf.Value = "" Then
                cpf.Value = 0

            End If


            ctr.Value = Me.ddlconceptotramite.SelectedValue
            If ctr.Value = "" Then
                ctr.Value = 0
            Else
                Me.ddlconceptotramite.SelectedValue = ctr.Value
            End If



            Dim obj As New ClsConectarDatos
            Dim objFun As New ClsFunciones
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            Me.ddlEscuela.Items.Clear()
            Me.ddlEscuela.DataBind()
            'objFun.CargarListas(Me.ddlEscuela, obj.TraerDataTable("ConsultarEscuelaPorPersonal", "3", CInt(Session("id_per").ToString()), 2), "codigo_Cpf", "nombre_Cpf")
            objFun.CargarListasGrupo(Me.ddlEscuela, obj.TraerDataTable("ConsultarEscuelaPorPersonal", "3", CInt(Session("id_per").ToString()), 2), "codigo_Cpf", "nombre_Cpf", "TipoEstudio")

            Page.RegisterStartupScript("Combo", "<script> GroupDropdownlist();</script>")
            obj.CerrarConexion()
            obj = Nothing
            objFun = Nothing


            If IsPostBack = False Then






                fnMostrarEvaluar(False)
                fnMostrarEvaluarFlujo(False)
                Me.cboEstado.SelectedIndex = 1
                ActualizaFechas()
                CargaDatos()
                Me.lnkSgt.Visible = True
                Session("trm_sco") = 0
                Me.rowObservacion.Visible = False
            End If

        Catch ex As Exception
            ShowMessage("Error: " & ex.Message.Replace("'", ""), MessageType.Error)
        End Try
    End Sub



    Private Sub ActualizaFechas()
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        Try
            obj.AbrirConexion()
            obj.Ejecutar("TRL_ActualizaFechaAtencion", 0)
            obj.CerrarConexion()
        Catch ex As Exception
            ShowMessage("Error: " & ex.Message.Replace("'", ""), MessageType.Error)
        End Try
    End Sub

    Protected Sub btnBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBuscar.Click
        CargaDatos()

    End Sub

    Private Sub CargaDatos()



        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        Dim dt2 As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        Try
            obj.AbrirConexion()
            'dt = obj.TraerDataTable("TRL_BuscaTramiteLinea", Me.txtAlumno.Text.Replace(" ", "%"), Me.cboEstado.SelectedValue, Me.Request.QueryString("ctf"), Me.Request.QueryString("id"))
            fnAgruparTramitesSelect()
            dt = obj.TraerDataTable("TRL_BuscaTramiteLinea", Me.txtAlumno.Text.Replace(" ", "%"), Me.cboEstado.SelectedValue, Me.Request.QueryString("ctf"), Me.Request.QueryString("id"), cpf.Value, ctr.Value)
            dt2 = obj.TraerDataTable("TRL_BuscaTramiteLinea", Me.txtAlumno.Text.Replace(" ", "%"), Me.cboEstado.SelectedValue, Me.Request.QueryString("ctf"), Me.Request.QueryString("id"), cpf.Value, 0)
            obj.CerrarConexion()

            fnAgruparTramitesSelect2(dt2)


            Me.gvDatos.DataSource = dt
            Me.gvDatos.DataBind()

            gvExportar.DataSource = dt
            Me.gvExportar.DataBind()



        Catch ex As Exception
            ShowMessage("Error_CargaDatos(): " & ex.Message.Replace("'", ""), MessageType.Error)
        End Try
    End Sub
    Private Sub fnAgruparTramitesSelect()
        Try


            Dim objFun As New ClsFunciones

            Dim dtconcepto As New Data.DataTable
            dtconcepto.Columns.Add("codigo_ctr")
            dtconcepto.Columns.Add("descripcion_ctr")


            Dim i As Integer = 0


            Dim row1 As Data.DataRow = dtconcepto.NewRow()
            row1("codigo_ctr") = 0
            row1("descripcion_ctr") = "TODOS"
            dtconcepto.Rows.Add(row1)

           
            objFun.CargarListas(Me.ddlconceptotramite, dtconcepto, "codigo_ctr", "descripcion_ctr")

        Catch ex As Exception
            ShowMessage("fnAgruparTramitesSelect(): " & ex.Message.Replace("'", ""), MessageType.Error)
        End Try

    End Sub
    Private Sub fnAgruparTramitesSelect2(ByVal dt As Data.DataTable)
        Try


            Dim objFun As New ClsFunciones

            Dim dtconcepto As New Data.DataTable
            dtconcepto.Columns.Add("codigo_ctr")
            dtconcepto.Columns.Add("descripcion_ctr")


            Dim i As Integer = 0


            Dim row1 As Data.DataRow = dtconcepto.NewRow()
            row1("codigo_ctr") = 0
            row1("descripcion_ctr") = "TODOS"
            dtconcepto.Rows.Add(row1)

            For i = 0 To dt.Rows.Count - 1
                ' Response.Write(dt.Rows(i).Item("codigo_ctr"))
                If fnAgruparTramitesSelectValida(dt.Rows(i).Item("codigo_ctr"), dtconcepto) Then
                    ' Response.Write(i & "<br>")
                    Dim row As Data.DataRow = dtconcepto.NewRow()
                    row("codigo_ctr") = dt.Rows(i).Item("codigo_ctr")
                    row("descripcion_ctr") = dt.Rows(i).Item("descripcion_Sco")
                    dtconcepto.Rows.Add(row)
                End If
            Next
            'Response.Write(dtconcepto.Rows.Count)
            objFun.CargarListas(Me.ddlconceptotramite, dtconcepto, "codigo_ctr", "descripcion_ctr")

        Catch ex As Exception
            ShowMessage("fnAgruparTramitesSelect2(): " & ex.Message.Replace("'", ""), MessageType.Error)
        End Try

    End Sub

    Private Function fnAgruparTramitesSelectValida(ByVal codigo_ctr As Integer, ByVal dtconcepto As Data.DataTable) As Boolean
        Try
            Dim i As Integer = 0
            Dim existe As Boolean = False


            For i = 0 To dtconcepto.Rows.Count - 1
                If dtconcepto.Rows(i).Item("codigo_ctr") = codigo_ctr Then
                    existe = True
                End If
            Next

            If existe Then
                Return False
            Else
                Return True
            End If


        Catch ex As Exception
            Return False
        End Try
    End Function

    Protected Sub gvDatos_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvDatos.PreRender
        If gvDatos.Rows.Count > 0 Then
            gvDatos.UseAccessibleHeader = True
            gvDatos.HeaderRow.TableSection = TableRowSection.TableHeader
        End If
    End Sub

    Protected Sub gvDatos_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvDatos.RowCommand
        Try

            'Response.Write(e.CommandArgument)
            Dim index As Integer = Convert.ToInt32(e.CommandArgument)


            If (e.CommandName = "Evaluar") Then
                Dim codigo_dta As Integer = 0
                hddtareq.Value = Encriptar(gvDatos.DataKeys(index).Values("codigo_dta").ToString)
                codigo_dta = gvDatos.DataKeys(index).Values("codigo_dta")

                Dim sco As Integer = 0
                sco = CInt(gvDatos.DataKeys(index).Values("sco").ToString)
                Session("trm_sco") = sco


                fnInformacionTramite(CInt(gvDatos.DataKeys(index).Values("codigo_dta").ToString))

                If sco = 1 Then
                    fnMostrarEvaluar(True)
                    fnLineaDeTiempo(codigo_dta)
                    fnListarRequisitos(codigo_dta)
                    fnMostrarConfirmacionReq(False)
                Else
                    Me.txtobservacionaprobacion.Enabled = True
                    Me.rblEstado.Enabled = True
                    Me.txtUltimaFechaAsistencia.Enabled = True
                    MostrarConceptoTramiteInfo("", False)
                    fnMostrarEvaluarFlujo(True)
                    ifrHistorial.Visible = False
                    ifrInformes.Visible = False
                    ifrAccion.Visible = False
                    ifrGeneraDeudaPorSemestre.Visible = False
                    fnLineaDeTiempoFlujo(codigo_dta)
                    fnListarFlujo(codigo_dta)
                End If

            End If
        Catch ex As Exception
            Response.Write("Error gvDatos_RowCommand: " & ex.Message & " - " & ex.StackTrace)
        End Try
    End Sub

    Private Sub fnInformacionTramite(ByVal codigo_dta As Integer)
        'Response.Write(codigo_dta)

        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        Dim str As String = ""
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString

        Try
            obj.AbrirConexion()
            dt = obj.TraerDataTable("TRL_informaciontramite", "1", codigo_dta)
            obj.CerrarConexion()

            If dt.Rows.Count > 0 Then

                Me.lblInfEstNumero.Text = dt.Rows(0).Item("numero").ToString()
                Me.lblInfEstEscuela.Text = dt.Rows(0).Item("escuela").ToString()
                Me.lblInfEstAlumno.Text = dt.Rows(0).Item("estudiante").ToString()
                Me.lblInfEstEmail.Text = dt.Rows(0).Item("email").ToString()
                Me.lblInfEstTelefono.Text = dt.Rows(0).Item("telefonoMovil_Dal").ToString()
                Me.lblInfEstCodUni.Text = dt.Rows(0).Item("codigouniver_alu").ToString()
            End If



        Catch ex As Exception
            ShowMessage("Error: " & ex.Message.Replace("'", ""), MessageType.Error)
        Finally
            obj = Nothing
        End Try

    End Sub

    Protected Sub gvDatos_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvDatos.RowDataBound
        If (e.Row.RowType.ToString = "DataRow") Then    'Solo las filas con los datos            
            'Guardamos el ID en el tooltip
            Dim lnk As LinkButton
            lnk = TryCast(e.Row.FindControl("lnkButton"), LinkButton)
            lnk.ToolTip = e.Row.Cells(0).Text

            'tiene requisito: gvDatos.DataKeys(e.Row.RowIndex).Values(1).ToString()
            'tiene sco: gvDatos.DataKeys(e.Row.RowIndex).Values(5).ToString()

            Dim lsDataKeyValue As String = gvDatos.DataKeys(e.Row.RowIndex).Values(1).ToString()
            Dim lsDataKeyValuesco As String = gvDatos.DataKeys(e.Row.RowIndex).Values(5).ToString()

            'Response.Write("<br>" & lsDataKeyValue)
            'Response.Write("<br>" & lsDataKeyValuesco)

            'Mostramos los controles segpun control
            If (e.Row.Cells(7).Text = "PENDIENTE") Then
                e.Row.FindControl("btnEdit").Visible = False    'Entregar    

                If (lsDataKeyValue = "True") Or (lsDataKeyValuesco = "0") Then
                    e.Row.FindControl("btnDelete").Visible = False  'Finalizar
                    e.Row.FindControl("Image1").Visible = True
                    e.Row.FindControl("btnEvaluar").Visible = True    'Evaluar    
                Else
                    e.Row.FindControl("btnDelete").Visible = True  'Finalizar
                    e.Row.FindControl("Image1").Visible = True
                    e.Row.FindControl("btnEvaluar").Visible = False    'Evaluar    
                End If
            End If

            If (e.Row.Cells(7).Text = "FINALIZADO") Then
                e.Row.FindControl("btnDelete").Visible = False  'Finalizar
                e.Row.FindControl("Image1").Visible = False
                e.Row.FindControl("btnEdit").Visible = True
                e.Row.FindControl("btnEvaluar").Visible = False

                'Entregar
            End If

            If (e.Row.Cells(7).Text = "ENTREGADO") Then
                e.Row.FindControl("btnDelete").Visible = False  'Finalizar
                e.Row.FindControl("Image1").Visible = False
                e.Row.FindControl("btnEdit").Visible = False    'Entregar

                e.Row.FindControl("btnEvaluar").Visible = False

            End If

            If (e.Row.Cells(7).Text = "ANULADO") Then
                e.Row.FindControl("btnDelete").Visible = False  'Finalizar
                e.Row.FindControl("Image1").Visible = False
                e.Row.FindControl("btnEdit").Visible = False    'Entregar
                e.Row.FindControl("btnEvaluar").Visible = False
            End If
        End If

    End Sub

    Protected Sub gvDatos_RowDeleted(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeletedEventArgs) Handles gvDatos.RowDeleted

    End Sub

    Protected Sub gvDatos_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles gvDatos.RowDeleting
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        Dim str As String = ""
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString        
        LimpiaAtencion()
        Try            
            Me.HdTramite.Value = gvDatos.DataKeys.Item(e.RowIndex).Values("codigo_dta")
            If (TieneDetallePendiente(Me.HdTramite.Value) = False) Then
                Page.RegisterStartupScript("CloseA", "<script>closeModal();</script>")
                ShowMessage("Su solicitud ya fue atendida", MessageType.Success)
            Else
                obj.AbrirConexion()
                dt = obj.TraerDataTable("TRL_RetornaRecogerTramite", Me.HdTramite.Value)
                obj.CerrarConexion()

                If (dt.Rows.Count > 0) Then
                    str = "Puedes recoger tu documento en " & dt.Rows(0).Item("ubicacion_ctr").ToString
                    str = str & " Previa presentación de tu DNI y copia. En caso el documento lo recoja un tercero, deberá traer la copia de DNI de ambos"


                    Me.txtObservacion.Text =str
                End If
            End If
        Catch ex As Exception
            ShowMessage("Error gvDatos_RowDeleting: " & ex.Message.Replace("'", ""), MessageType.Error)
        End Try
    End Sub

    Protected Sub gvDatos_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles gvDatos.RowEditing
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        Try            
            Me.HdTramite.Value = gvDatos.DataKeys.Item(e.NewEditIndex).Values("codigo_dta")
            If (TieneDetalleAtendido(Me.HdTramite.Value) = False) Then
                Page.RegisterStartupScript("CloseE", "<script>closeModal();</script>")
                ShowMessage("Su solicitud debe estar atendida", MessageType.Success)
                Exit Sub
            End If

            obj.AbrirConexion()
            obj.Ejecutar("TRL_EntregaTramite", Me.HdTramite.Value, Request.QueryString("ctf"), _
                         Request.QueryString("id"))
            obj.CerrarConexion()

            EnviaCorreo(Me.HdTramite.Value, "E")
            CargaDatos()

            ShowMessage("Entrega registrada", MessageType.Success)
        Catch ex As Exception
            ShowMessage("Error gvDatos_RowEditing: " & ex.Message.Replace("'", ""), MessageType.Error)
        End Try
    End Sub

    Protected Sub btnEvaluar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        'Page.RegisterStartupScript("Pop", "<script>alert('evaluar');</script>")
        'Response.Write(Session("trm_sco"))
        'If Session("trm_sco") = 1 Then

        '    fnMostrarEvaluar(False)

        'Else
        '    fnMostrarEvaluarFlujo(False)

        'End If
    End Sub

    Protected Sub btnAtender_Click(ByVal sender As Object, ByVal e As System.EventArgs)        
        Page.RegisterStartupScript("Pop", "<script>openModal();</script>")
    End Sub

    Protected Sub Image1_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        gvDatos_SelectedIndexChanged(sender, e)
        txtObservacionAlumno.text = ""
        Page.RegisterStartupScript("Pop", "<script>openModalFecha();</script>")
    End Sub

    Protected Sub btnEntregar_Click(ByVal sender As Object, ByVal e As System.EventArgs)

    End Sub

    Protected Sub ShowMessage(ByVal Message As String, ByVal type As MessageType)
        Page.RegisterStartupScript("Mensaje", "<script>ShowMessage('" & Message & "','" & type & "');</script>")
    End Sub
    Protected Sub ShowMessage2(ByVal Message As String, ByVal type As String)
        Page.RegisterStartupScript("Mensaje", "<script>ShowMessage2('" & Message & "','" & type & "');</script>")
    End Sub

    Protected Sub btnGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardar.Click
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        Try
            obj.AbrirConexion()
            obj.Ejecutar("TRL_AtiendeTramite", Me.HdTramite.Value, Request.QueryString("ctf"), _
                         Me.txtObservacion.Text, Request.QueryString("id"))
            obj.CerrarConexion()

            EnviaCorreo(Me.HdTramite.Value, "T")
            CargaDatos()
            Page.RegisterStartupScript("bloquea0", "<script type='text/javascript'>MascaraEsperaModal('0');</script>")
            ShowMessage("Se registró correctamente", MessageType.Success)
        Catch ex As Exception
            ShowMessage("Error: " & ex.Message.Replace("'", ""), MessageType.Error)
        End Try
    End Sub

    Private Function TieneDetallePendiente(ByVal codigo_dta As Integer) As Boolean
        Dim dt As New Data.DataTable
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        Try
            obj.AbrirConexion()
            dt = obj.TraerDataTable("TRL_TieneTramitePendiente", codigo_dta)
            obj.CerrarConexion()

            If (dt.Rows.Count = 0) Then
                Return False
            End If

            Return True
        Catch ex As Exception
            ShowMessage("Error: " & ex.Message.Replace("'", ""), MessageType.Error)
            Return False
        End Try
    End Function

    Private Function TieneDetalleAtendido(ByVal codigo_dta As Integer) As Boolean
        Dim dt As New Data.DataTable
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        Try
            obj.AbrirConexion()
            dt = obj.TraerDataTable("TRL_TieneTramiteAtendido", codigo_dta)
            obj.CerrarConexion()

            If (dt.Rows.Count > 0) Then
                Return True
            End If

            Return False
        Catch ex As Exception
            ShowMessage("Error TieneDetalleAtendido: " & ex.Message.Replace("'", ""), MessageType.Error)
            Return False
        End Try
    End Function

    Private Sub LimpiaAtencion()
        Me.HdTramite.Value = 0
        Me.txtObservacion.Text = ""
    End Sub

    Protected Sub btnGuardaFecha_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardaFecha.Click
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        Try            
            obj.AbrirConexion()
            obj.Ejecutar("TRL_ActualizaFechaTramite", Me.HdTramite.Value, Me.txtFecha.Value, _
                         Me.txtObservacionAlumno.Text, Session("id_per"))
            obj.CerrarConexion()
            EnviaCorreo(Me.HdTramite.Value, "F")
            CargaDatos()
            Page.RegisterStartupScript("bloquea0", "<script type='text/javascript'>MascaraEspera('0');</script>")
            ShowMessage("Se registró correctamente", MessageType.Success)

        Catch ex As Exception
            ShowMessage("Error: " & ex.Message.Replace("'", ""), MessageType.Error)
        End Try
    End Sub

    Private Function EnviaCorreo(ByVal dta As Integer, ByVal tipo As String) As Boolean
        Dim cls As New ClsMail
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        Dim strMensaje As String = ""
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString

        Dim EmailDestino As String = ""

        Try
            obj.AbrirConexion()
            dt = obj.TraerDataTable("TRL_DatosAlumnoxDetalle", dta)
            obj.CerrarConexion()

            ' If (dt.Rows.Count > 0) Then
            If dt.Rows(0).Item("UserPrincipalName").ToString <> "" Then
                EmailDestino = dt.Rows(0).Item("UserPrincipalName")
            End If

            'Correo de Fecha de Entrega
            If tipo = "F" Then
                strMensaje = "Estimado(a) " & dt.Rows(0).Item("nombres_Alu") & " " & dt.Rows(0).Item("apellidoPat_Alu") & " " & dt.Rows(0).Item("apellidoMat_Alu") & ": <br/><br/>"
                strMensaje = strMensaje & "El trámite " & dt.Rows(0).Item("glosaCorrelativo_trl")
                strMensaje = strMensaje & " ha sido actualizada la fecha de entrega para el día "
                strMensaje = strMensaje & dt.Rows(0).Item("fechaFin_dta").ToString & ".<br/><br/>"
                strMensaje = strMensaje & "<em>" & dt.Rows(0).Item("observacionAlumno_dft").ToString & "</em>"
                cls.EnviarMail("campusvirtual@usat.edu.pe", "Campus Virtual", EmailDestino, "Fecha de Entrega Trámite", strMensaje, True, "", "")
            End If

            'Correo de Finaliza Tramite
            If tipo = "T" Then
                strMensaje = "Estimado(a) " & dt.Rows(0).Item("nombres_Alu") & " " & dt.Rows(0).Item("apellidoPat_Alu") & " " & dt.Rows(0).Item("apellidoMat_Alu") & ": <br/><br/>"
                strMensaje = strMensaje & "El documento solicitado <b>" & dt.Rows(0).Item("descripcion_ctr").ToString.ToUpper() & "</b> ya se encuentra disponible.<br/><br/>"
                strMensaje = strMensaje & "Puedes recoger tu documento en: <br/>"
                strMensaje = strMensaje & "<em>" & dt.Rows(0).Item("ubicacion_ctr") & " - " & dt.Rows(0).Item("observacionAlumno_dft").ToString & "</em><br/>"
                cls.EnviarMail("campusvirtual@usat.edu.pe", "Campus Virtual", EmailDestino, "Entrega Trámite", strMensaje, True, "", "")
            End If

            'Correo de Entrega Tramite
            If tipo = "E" Then
                strMensaje = "Estimado(a) " & dt.Rows(0).Item("nombres_Alu") & " " & dt.Rows(0).Item("apellidoPat_Alu") & " " & dt.Rows(0).Item("apellidoMat_Alu") & ": <br/><br/>"
                strMensaje = strMensaje & "El documento solicitado <b>" & dt.Rows(0).Item("descripcion_ctr").ToString.ToUpper() & "</b> ya ha sido entregado.<br/><br/>"
                cls.EnviarMail("campusvirtual@usat.edu.pe", "Campus Virtual", EmailDestino, "Entrega Trámite", strMensaje, True, "", "")
            End If
            '  End If
            '  End If

            cls = Nothing
            obj = Nothing
        Catch ex As Exception
            ShowMessage("Error: " & ex.Message.Replace("'", ""), MessageType.Error)
        End Try
    End Function

    Protected Sub gvDatos_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvDatos.SelectedIndexChanged
        Dim lnk As New LinkButton
        Dim dt, dtH As New Data.DataTable
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        Try            
            lnk = TryCast(sender, LinkButton)
            Me.HdTramite.Value = lnk.ToolTip

            'Carga Datos del detalle del tramite
            obj.AbrirConexion()
            dt = obj.TraerDataTable("TRL_RetornaDetalleTramite", 0, Me.HdTramite.Value)
            dtH = obj.TraerDataTable("TRL_MuestraHistorialFechas", Me.HdTramite.Value)
            obj.CerrarConexion()

            Me.gvHistorial.DataSource = dtH
            Me.gvHistorial.DataBind()

            If (dt.Rows.Count > 0) Then
                If (dt.Rows(0).Item("fechaFin_dta").ToString() <> "") Then
                    Me.txtFecha.Value = dt.Rows(0).Item("fechaFin_dta").ToString
                End If
            End If
        Catch ex As Exception
            ShowMessage("Error gvDatos_SelectedIndexChanged: " & ex.Message.Replace("'", ""), MessageType.Error)
        End Try
    End Sub

    Protected Sub btnExportar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExportar.Click
        Dim sb As StringBuilder = New StringBuilder()
        Dim sw As StringWriter = New StringWriter(sb)
        Dim htw As HtmlTextWriter = New HtmlTextWriter(sw)
        Dim pagina As Page = New Page
        Dim form = New HtmlForm
        gvExportar.EnableViewState = False
        gvExportar.Visible = True
        pagina.EnableEventValidation = False
        pagina.DesignerInitialize()
        pagina.Controls.Add(form)
        form.Controls.Add(gvExportar)
        pagina.RenderControl(htw)
        Response.Clear()
        Response.Buffer = True
        Response.ContentType = "application/vnd.ms-excel"
        Response.AddHeader("Content-Disposition", "attachment;filename=data.xls")
        Response.Charset = "UTF-8"

        Response.ContentEncoding = Encoding.Default
        Response.Write(sb.ToString())
        Response.End()
        gvExportar.Visible = False
    End Sub

    Private Sub fnMostrarEvaluar(ByVal sw As Boolean)

        If sw Then
            Me.pnlLista.Visible = False
            Me.pnlRegistro.Visible = True
        Else
            Me.pnlLista.Visible = True
            Me.pnlRegistro.Visible = False
        End If

    End Sub

    Private Sub fnMostrarConfirmacionReq(ByVal sw As Boolean)

        If sw Then
            Me.colConfirmaReq.Visible = True
            Me.colBotonReq.Visible = False
        Else
            Me.colConfirmaReq.Visible = False
            Me.colBotonReq.Visible = True
        End If

    End Sub
    Private Sub fnMostrarEvaluarFlujo(ByVal sw As Boolean)

        If sw Then
            Me.pnlLista.Visible = False
            Me.pnlRegistro2.Visible = True
        Else
            Me.pnlLista.Visible = True
            Me.pnlRegistro2.Visible = False
        End If

    End Sub

    Protected Sub lnkAnt_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkAnt.Click


        fnMostrarEvaluar(False)


    End Sub

    Public Function Encriptar(ByVal Input As String) As String

        Dim IV() As Byte = ASCIIEncoding.ASCII.GetBytes("qualityi") 'La clave debe ser de 8 caracteres
        Dim EncryptionKey() As Byte = Convert.FromBase64String("rpaSPvIvVLlrcmtzPU9/c67Gkj7yL1S5") 'No se puede alterar la cantidad de caracteres pero si la clave
        Dim buffer() As Byte = Encoding.UTF8.GetBytes(Input)
        Dim des As TripleDESCryptoServiceProvider = New TripleDESCryptoServiceProvider
        des.Key = EncryptionKey
        des.IV = IV

        Return Convert.ToBase64String(des.CreateEncryptor().TransformFinalBlock(buffer, 0, buffer.Length()))

    End Function

    Public Function Desencriptar(ByVal Input As String) As String
        Dim IV() As Byte = ASCIIEncoding.ASCII.GetBytes("qualityi") 'La clave debe ser de 8 caracteres
        Dim EncryptionKey() As Byte = Convert.FromBase64String("rpaSPvIvVLlrcmtzPU9/c67Gkj7yL1S5") 'No se puede alterar la cantidad de caracteres pero si la clave
        Dim buffer() As Byte = Convert.FromBase64String(Input)
        Dim des As TripleDESCryptoServiceProvider = New TripleDESCryptoServiceProvider
        des.Key = EncryptionKey
        des.IV = IV
        Return Encoding.UTF8.GetString(des.CreateDecryptor().TransformFinalBlock(buffer, 0, buffer.Length()))

    End Function

    Protected Sub lnkSgt_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkSgt.Click
        If (Session("id_per") Is Nothing) Then
            'Response.Write("Sesion Finalizada")
            Response.Redirect("../../ErrorSistema.aspx")
        Else
            fnMostrarConfirmacionReq(True)
            Me.gvRequisitos.Enabled = False
            fnListaRequerimientoSeleccionado()
        End If


    End Sub

    Protected Sub btnNoReq_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNoReq.Click
        fnMostrarConfirmacionReq(False)
        Me.gvRequisitos.Enabled = True
    End Sub

    Protected Sub btnSiReq_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSiReq.Click

        fnAprobarRequisito()
        fnMostrarConfirmacionReq(False)

    End Sub

    Private Sub fnListaRequerimientoSeleccionado()
        Try

        
            Dim Fila As GridViewRow
            Dim str As New StringBuilder
            str.Append("<ul style='text-align:left;'>")
            For i As Integer = 0 To Me.gvRequisitos.Rows.Count - 1
                Fila = Me.gvRequisitos.Rows(i)
                'Response.Write(CType(Fila.FindControl("chkElegir"), CheckBox).Checked)
                Dim valor As Boolean = CType(Fila.FindControl("chkElegir"), CheckBox).Checked
                If (valor = True And CType(Fila.FindControl("chkElegir"), CheckBox).Enabled = True) Then

                    str.Append("<li>" & Me.gvRequisitos.DataKeys(i).Values("nombre_tre").ToString & "</li>")

                End If
            Next
            str.Append("</ul>")
            'Response.Write(str.ToString)
            ulselreq.InnerHtml = str.ToString
        Catch ex As Exception
            Response.Write(ex.Message & "  -  " & ex.StackTrace)
        End Try
    End Sub

    Private Sub fnAprobarRequisito()
        Try
            Dim obj As New ClsConectarDatos
            Dim lblResultado As Boolean = False
            Dim idta As Integer = 0
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()

            idta = CInt(Desencriptar(hddtareq.Value.ToString))
            'Response.Write(idta & "<br>")
            Dim _hddtareq As String = ""
            Dim ctf As String = ""
            Dim per As Integer = 0

            Dim tieneEntrega As Int16

            ctf = Me.Request.QueryString("ctf")
            per = CInt(Me.Request.QueryString("id"))

            'Response.Write(fnValidarCheckRequisito)

            If fnValidarCheckRequisitoFuncion() Then

                If ctf = Session("req_tfu").ToString Or ctf = 1 Then
                    If hddtareq.Value <> "" Then
                        ' lblResultado = obj.Ejecutar("GrupoAvisoDetalleIAE", "E", 0, idg, 0)
                        Dim Fila As GridViewRow
                        For i As Integer = 0 To Me.gvRequisitos.Rows.Count - 1
                            Fila = Me.gvRequisitos.Rows(i)
                            'Response.Write(CType(Fila.FindControl("chkElegir"), CheckBox).Checked)
                            Dim valor As Boolean = CType(Fila.FindControl("chkElegir"), CheckBox).Checked
                            If (valor = True And CType(Fila.FindControl("chkElegir"), CheckBox).Enabled = True) Then

                                tieneEntrega = Me.gvFlujoTramite.DataKeys(i).Values("tieneEntrega")
                                lblResultado = obj.Ejecutar("TRL_TramiteRequisito_Registrar", "A", idta, CInt(Me.gvRequisitos.DataKeys(i).Values("codigo_dre").ToString), CInt(Me.gvRequisitos.DataKeys(i).Values("codigo_dft").ToString), 1, "F", per)

                                If lblResultado Then

                                    If tieneEntrega = 0 Then

                                        EnviaCorreo(idta, "E")

                                    End If
                                End If
                            End If
                        Next
                    End If
                Else
                    ShowMessage("No está autorizado para evaluar requisitos de tramites", MessageType.Warning)
                End If

            Else
                ShowMessage("ADVERTENCIA: Todos los requisitos son obligatorios a verificar según su Perfil. ", MessageType.Error)


            End If

            'Me.gvRequisitos.DataBind()
            'Me.gvRequisitos.Visible = True
            obj.CerrarConexion()
            ' lblResultado = False

            Me.gvRequisitos.Enabled = True
            _hddtareq = Desencriptar(hddtareq.Value.ToString)

            fnListarRequisitos(_hddtareq)


           

            If lblResultado Then


                gvRequisitos.DataSource = Nothing
                gvRequisitos.DataBind()
                ShowMessage("HAZ CULMINADO DE EVALUAR SATISFACTORIAMENTE EL TRÁMITE", MessageType.Success)
                'Me.gvRequisitos.Enabled = True
                '_hddtareq = Desencriptar(hddtareq.Value.ToString)
                'fnListarRequisitos(_hddtareq)
                fnLineaDeTiempo(_hddtareq)
                CargaDatos()
            End If

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Function fnValidarCheckRequisitoFuncion() As Boolean
        Dim ctf As Integer = 0
        ctf = Me.Request.QueryString("ctf")
        Dim rpta As Boolean = False
        Dim timelinechk As Integer = 0
        Dim timelinetotal As Integer = 0
        Dim i As Integer = 0
        For Each row As GridViewRow In gvRequisitos.Rows
            If row.RowType = DataControlRowType.DataRow Then
                Dim chkRow As CheckBox = TryCast(row.Cells(0).FindControl("chkElegir"), CheckBox)

                If chkRow.Enabled = True Then
                    'Response.Write("<br>" & Me.gvRequisitos.DataKeys(i).Values("codigo_tfu_req") & "  =  " & ctf)
                    If Me.gvRequisitos.DataKeys(i).Values("codigo_tfu_req").ToString = ctf Then
                        timelinetotal = timelinetotal + 1
                        If chkRow.Checked = True Then
                            timelinechk = timelinechk + 1
                        End If

                    End If
                End If
            End If
            i = i + 1
        Next
        Me.hdtimelinechk.Value = timelinechk
        'Response.Write((CInt(Me.timelineactive.Text)))
        'Response.Write(" <= ")
        'Response.Write((CInt(Me.timelinechk.Text)) + 1)

        'If (CInt(Me.hdtimelineactive.Value)) <= (CInt(Me.hdtimelinechk.Value) + 1) Then
        '    rpta = True
        'Else
        '    rpta = False
        'End If

        'Response.Write("<br>" & "disponible: " & timelinechk.ToString & "  ==  total" & timelinetotal.ToString)
        If timelinetotal > 0 AndAlso timelinetotal = timelinechk Then
            rpta = True
        Else
            rpta = False
        End If
        '  Response.Write((CInt(Me.timelinechk.Text)))
        Return rpta
    End Function
    'Private Function fnValidarCheckRequisito() As Boolean
    '    Dim rpta As Boolean = False
    '    Dim timelinechk As Integer = 0
    '    For Each row As GridViewRow In gvRequisitos.Rows
    '        If row.RowType = DataControlRowType.DataRow Then
    '            Dim chkRow As CheckBox = TryCast(row.Cells(0).FindControl("chkElegir"), CheckBox)
    '            If chkRow.Checked = False Then
    '                timelinechk = timelinechk + 1
    '            End If
    '        End If
    '    Next
    '    Me.hdtimelinechk.Value = timelinechk
    '    'Response.Write((CInt(Me.timelineactive.Text)))
    '    'Response.Write(" <= ")
    '    'Response.Write((CInt(Me.timelinechk.Text)) + 1)

    '    If (CInt(Me.hdtimelineactive.Value)) <= (CInt(Me.hdtimelinechk.Value) + 1) Then
    '        rpta = True
    '    Else
    '        rpta = False
    '    End If
    '    '  Response.Write((CInt(Me.timelinechk.Text)))
    '    Return rpta
    'End Function

    Private Sub fnListarFlujo(ByVal codigo_dta As Integer)
        Try
            Dim dt As New Data.DataTable
            Dim obj As New ClsConectarDatos
            Dim ctf As String = ""
            Dim j As Integer = 0
            ctf = Me.Request.QueryString("ctf")
            HdAccion.Value = ""
            'Response.Write("codigo_dta: " & codigo_dta)

            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            dt = obj.TraerDataTable("TRL_TramiteFlujo_Listar", "2", codigo_dta)

            obj.CerrarConexion()

            Me.gvFlujoTramite.DataSource = dt
            Me.gvFlujoTramite.DataBind()

            Session("dft_tfu") = ""
            If dt.Rows.Count > 0 Then
                'Response.Write(dt.Rows.Count.ToString & "<br>")
                For j = 0 To dt.Rows.Count - 1
                    ' Response.Write(dt.Rows(j).Item("estado_dft").ToString & "-" & dt.Rows(j).Item("cumple_dft").ToString & "<br>")
                    If dt.Rows(j).Item("estado_dft").ToString = "P" And dt.Rows(j).Item("cumple_dft").ToString = "0" Then
                        Session("dft_tfu") = dt.Rows(j).Item("codigo_tfu").ToString
                        Exit For
                    End If
                Next




            End If

            'Response.Write(ctf & " = " & Session("dft_tfu"))
            If ctf = Session("dft_tfu").ToString Or ctf = 1 Then
                Me.lnkSgt2.Visible = True
                Me.lnkSgt2.Enabled = True
            Else
                Me.lnkSgt2.Visible = False
                Me.lnkSgt2.Enabled = False

            End If
            If dt.Rows.Count > 0 Then
                If dt.Rows(dt.Rows.Count - 1).Item("estado_dft").ToString = "F" And dt.Rows(dt.Rows.Count - 1).Item("cumple_dft").ToString = "1" Then
                    Me.lnkSgt2.Visible = False
                    Me.lnkSgt2.Enabled = False
                End If

            End If


        Catch ex As Exception
            ShowMessage("fnListarFlujo(): " & ex.Message.Replace("'", ""), MessageType.Error)
        End Try
    End Sub

    Private Sub fnListarRequisitos(ByVal codigo_dta As Integer)
        Dim dt As New Data.DataTable
        Dim obj As New ClsConectarDatos
        Dim ctf As String = ""
        ctf = Me.Request.QueryString("ctf")
        Try

            Dim i As Integer = gvDatos.Rows.Item(0).RowIndex
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            dt = obj.TraerDataTable("TRL_TramiteRequisito_Listar", "2", codigo_dta)
            obj.CerrarConexion()

            'Response.Write("TRL_TramiteRequisito_Listar 2," & codigo_dta.ToString)
            Session("req_tfu") = ""
            If dt.Rows.Count > 0 Then
                Session("req_tfu") = dt.Rows(0).Item("codigo_tfu").ToString
            End If

            'Response.Write(ctf & " = " & Session("req_tfu"))
            If ctf = Session("req_tfu").ToString Or ctf = 1 Then
                Me.lnkSgt.Visible = True
                Me.lnkSgt.Enabled = True
            Else
                Me.lnkSgt.Visible = False
                Me.lnkSgt.Enabled = False
            End If

            Me.gvRequisitos.DataSource = dt
            Me.gvRequisitos.DataBind()
        Catch ex As Exception
            ShowMessage("Error: " & ex.Message.Replace("'", ""), MessageType.Error)
        End Try
    End Sub

    Private Sub fnLineaDeTiempo(ByVal codigo_dta As Integer)
        Dim dt As New Data.DataTable
        Dim obj As New ClsConectarDatos
        Dim s As New StringBuilder
        Dim timelineactive As Integer = 0
        Try

            Dim i As Integer = gvDatos.Rows.Item(0).RowIndex
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            dt = obj.TraerDataTable("TRL_TramiteRequisito_Listar", "0", codigo_dta)
            obj.CerrarConexion()
            s.Append("<ul class='timeline timeline-horizontal'>")
            For j As Integer = 0 To dt.Rows.Count - 1

                If dt.Rows(j).Item("estado_time").ToString <> "success" Then
                    timelineactive = timelineactive + 1
                End If


                s.Append("<li class='timeline-item' style='height:0px; width:0px'>")
                s.Append("<div class='timeline-badge " & dt.Rows(j).Item("estado_time").ToString & "'>")
                s.Append("<i class='glyphicon glyphicon-check'></i>")
                s.Append("</div>")

                s.Append("<div class='timeline-heading'>")
                s.Append("<h5 class='timeline-title'>" & dt.Rows(j).Item("descripcion_Tfu").ToString & "</h5>")
                s.Append("<p>")

                If dt.Rows(j).Item("estado_dft").ToString = "F" Then
                    s.Append("<small class='text-muted'><i class='glyphicon glyphicon-time'></i> " & dt.Rows(j).Item("fecha_timeline").ToString & "</small>")
                End If

                s.Append("</p>")
                s.Append("</div>")
                s.Append("<div class='timeline-body'>")
                s.Append("<p><br><br><br><br>")
                'Mussum ipsum cacilds, vidis litro abertis. Consetis faiz elementum girarzis, nisieros gostis.
                s.Append("</p>")
                s.Append("</div>")
                s.Append("</li>")
            Next
            s.Append("</ul>")

            divTimeline.InnerHtml = s.ToString

            Me.hdtimelineactive.Value = timelineactive
            If dt.Rows(dt.Rows.Count - 1).Item("estado_dft").ToString = "F" Then
                lnkSgt.Visible = False
            Else
                lnkSgt.Visible = True
            End If

            dt = Nothing

        Catch ex As Exception
            ShowMessage("Error: " & ex.Message.Replace("'", ""), MessageType.Error)
        End Try
    End Sub

    Private Sub fnLineaDeTiempoFlujo(ByVal codigo_dta As Integer)
        Dim dt As New Data.DataTable
        Dim obj As New ClsConectarDatos
        Dim s As New StringBuilder
        Try

            Dim i As Integer = gvDatos.Rows.Item(0).RowIndex
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            dt = obj.TraerDataTable("TRL_TramiteFlujo_Listar", "0", codigo_dta)
            obj.CerrarConexion()
            s.Append("<ul class='timeline timeline-horizontal'>")
            For j As Integer = 0 To dt.Rows.Count - 1
                s.Append("<li class='timeline-item' style='height:0px; width:0px'>")
                s.Append("<div class='timeline-badge " & dt.Rows(j).Item("estado_time").ToString & "'>")
                s.Append("<i class='glyphicon glyphicon-check'></i>")
                s.Append("</div>")

                s.Append("<div class='timeline-heading'>")
                s.Append("<h5 class='timeline-title'>" & dt.Rows(j).Item("descripcion_Tfu").ToString & "</h5>")
                s.Append("<p>")

                If dt.Rows(j).Item("estado_dft").ToString = "F" Then
                    s.Append("<small class='text-muted'><i class='glyphicon glyphicon-time'></i> " & dt.Rows(j).Item("fecha_timeline").ToString & "</small>")
                End If
                s.Append("<br>")
                If dt.Rows(j).Item("estadoAprobacion").ToString = "Aprobado" Then
                    s.Append("<small class='text-muted'><i class='glyphicon glyphicon-thumbs-up'></i> " & dt.Rows(j).Item("estadoAprobacion").ToString & "</small>")
                ElseIf dt.Rows(j).Item("estadoAprobacion").ToString = "Rechazado" Then
                    s.Append("<small class='text-muted'><i class='glyphicon glyphicon-thumbs-down'></i> " & dt.Rows(j).Item("estadoAprobacion").ToString & "</small>")
                End If
                s.Append("<br>")
                If dt.Rows(j).Item("observacionAprobacion").ToString <> "" Then
                    s.Append("<small class='text-muted'><i class='glyphicon glyphicon-pencil'></i> " & dt.Rows(j).Item("observacionAprobacion").ToString & "</small>")
                End If

                s.Append("</p>")
                s.Append("</div>")
                s.Append("<div class='timeline-body'>")
                s.Append("<p><br><br><br><br>")
                'Mussum ipsum cacilds, vidis litro abertis. Consetis faiz elementum girarzis, nisieros gostis.
                s.Append("</p>")
                s.Append("</div>")
                s.Append("</li>")
            Next
            s.Append("</ul>")

            divTimelineFlujo.InnerHtml = s.ToString


            If dt.Rows(dt.Rows.Count - 1).Item("estado_dft").ToString = "F" Then
                lnkSgt.Visible = False
            Else
                lnkSgt.Visible = True
            End If

            dt = Nothing

        Catch ex As Exception
            ShowMessage("fnLineaDeTiempoFlujo()" & ex.Message.Replace("'", ""), MessageType.Error)
        End Try
    End Sub

    Protected Sub gvRequisitos_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvRequisitos.RowDataBound
        Try

        
            Dim ctf As Integer
            ctf = CInt(Me.Request.QueryString("ctf"))
            Dim index As Integer = 0
            Dim timelinechk As Integer = 0
            If e.Row.RowType = DataControlRowType.DataRow Then

                index = e.Row.RowIndex
                Dim checkAcceso As CheckBox
                checkAcceso = e.Row.FindControl("chkElegir")

                If gvRequisitos.DataKeys(index).Values("cumple_dre") = 0 Then
                    checkAcceso.Checked = False
                    If gvRequisitos.DataKeys(index).Values("codigo_tfu_req") = ctf Then
                        checkAcceso.Enabled = True
                    Else
                        checkAcceso.Enabled = False
                    End If

                Else
                    If gvRequisitos.DataKeys(index).Values("codigo_tfu_req") = ctf Then
                        checkAcceso.Checked = True
                        checkAcceso.Enabled = False
                    Else
                        checkAcceso.Checked = True
                        checkAcceso.Enabled = False
                    End If


                    timelinechk = timelinechk + 1
                End If




            End If
            Me.hdtimelinechk.Value = timelinechk
        Catch ex As Exception
            Response.Write(ex.Message & "  -  " & ex.StackTrace)
        End Try
    End Sub

    Protected Sub gvFlujoTramite_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvFlujoTramite.RowDataBound
        Dim index As Integer = 0
        Dim rowtfu As Integer = 0
        Dim DetAcad As Integer = 0
        Dim DetAdm As Integer = 0
        Dim DetAccion As Integer = 0
        Dim DetTipo As String = ""


        Dim codigoUniver As String = ""
        Dim codigoAlu As Integer = 0

        Dim codigo_trl As Integer = 0
        Dim codigo_dta As Integer = 0
        Dim proceso As String = ""
        Dim codigo_dft As Integer = 10

        Dim ctf As Integer = Me.Request.QueryString("ctf")
        Dim bloquear As Boolean = False
        Dim tramite As String = ""
        Dim tramiteObservacion As String = ""


        If e.Row.RowType = DataControlRowType.DataRow Then
         
            index = e.Row.RowIndex
            Dim checkAcceso As CheckBox
            checkAcceso = e.Row.FindControl("chkElegir")
            ''Response.Write(gvFlujoTramite.DataKeys(index).Values("codigo_tfu").ToString())
            rowtfu = gvFlujoTramite.DataKeys(index).Values("codigo_tfu")

            DetAcad = CInt(gvFlujoTramite.DataKeys(index).Values("verDetAcad_ftr").ToString)
            DetAdm = CInt(gvFlujoTramite.DataKeys(index).Values("verDetAdm_ftr").ToString)
            DetAccion = CInt(gvFlujoTramite.DataKeys(index).Values("accionURL_ftr").ToString)
            codigoUniver = gvFlujoTramite.DataKeys(index).Values("codigoUniver_Alu")
            codigoAlu = gvFlujoTramite.DataKeys(index).Values("codigo_Alu")
            DetTipo = gvFlujoTramite.DataKeys(index).Values("tipo")
            codigo_trl = gvFlujoTramite.DataKeys(index).Values("codigo_trl")
            proceso = gvFlujoTramite.DataKeys(index).Values("proceso")
            codigo_dta = gvFlujoTramite.DataKeys(index).Values("codigo_dta")
            codigo_dft = gvFlujoTramite.DataKeys(index).Values("codigo_dft")
            tramite = gvFlujoTramite.DataKeys(index).Values("descripcion_ctr")
            tramiteObservacion = gvFlujoTramite.DataKeys(index).Values("observacion_trl")

            txtcodalu.Value = codigoAlu
            If ctf = rowtfu Or ctf = 1 Then
                bloquear = True
            End If

            'Response.Write(gvFlujoTramite.DataKeys(index).Values("cumple_dft") & "<br>")
            If gvFlujoTramite.DataKeys(index).Values("cumple_dft") = 0 Then
                checkAcceso.Checked = True

                If ctf = rowtfu Or ctf = 1 Then
                    verConceptoTramiteInfo(codigo_dft)
                    e.Row.Font.Bold = True
                    e.Row.BackColor = Drawing.Color.AliceBlue
                    ' Response.Write("CTF")
                    ' Response.Write(gvFlujoTramite.DataKeys(index).Values("accionURL_ftr") & "<br>")
                    ' Response.Write(DetAcad)
                    'Response.Write(DetAcad)
                    'Response.Write(DetAdm)
                    'Response.Write(DetAccion)

                    If (DetAcad = 1) Then
                        verDetAcad(codigoUniver)
                    End If
                    If (DetAdm = 1) Then
                        verDetAdm(codigoUniver)
                    End If

                    accionURL(codigoUniver, codigoAlu, DetTipo, codigo_trl, codigo_dta, tramite, tramiteObservacion)
                    If (DetAccion = 1) Then
                        ' Response.Write("accion: " & codigo_dta & "  " & DetAccion)
                        'accionURL(codigoUniver, codigoAlu, DetTipo, codigo_trl, codigo_dta)

                        Select Case proceso
                            Case "RETIRAR SEMESTRE", "RETIRO DEFINITIVO"
                                Me.rowObservacion.Visible = False
                            Case "INACTIVAR ALUMNO"
                                Me.rowObservacion.Visible = False
                            Case "ACTIVAR ALUMNO"
                                Me.rowObservacion.Visible = False
                            Case "GENERAR DEUDA POR CADA SEMESTRE"
                                Me.ifrGeneraDeudaPorSemestre.Visible = True
                                PrevisualizarDeudaPorSemestre(codigo_dft)
                            Case Else
                                Me.rowObservacion.Visible = True
                        End Select

                    End If
                Else
                    e.Row.Visible = False
                End If
                checkAcceso.Enabled = bloquear

            Else
                'If bloquear = False Then
                checkAcceso.Checked = True
                checkAcceso.Enabled = False
                ' End If


                If ctf = rowtfu Or ctf = 1 Then
                    If (DetAcad = 1) Then
                        verDetAcad(codigoUniver)
                    End If
                    If (DetAdm = 1) Then
                        verDetAdm(codigoUniver)
                    End If

                    accionURL(codigoUniver, codigoAlu, DetTipo, codigo_trl, codigo_dta, tramite, tramiteObservacion)
                End If

            End If

            'proceso'

            'Select Case proceso
            '    Case "RETIRAR SEMESTRE", "RETIRO DEFINITIVO"
            '        fnVerCursosUltimaAsistencia(Me.ddlCiclo.SelectedValue)
            '        ' Me.rowObservacion.Visible = False
            '    Case "INACTIVAR ALUMNO"

            '        ' Me.rowObservacion.Visible = False
            '    Case "ACTIVAR ALUMNO"

            '        ' Me.rowObservacion.Visible = False
            '    Case "GENERAR DEUDA POR CADA SEMESTRE"

            '    Case Else
            '        ' Me.rowObservacion.Visible = True
            'End Select

        End If

    End Sub

    Private Sub fnVerCursosUltimaAsistencia()
        Try
            Dim obj As New ClsConectarDatos
            Dim dt As New Data.DataTable
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString

            Dim codigo_alu As Integer = 0
            codigo_alu = CInt(txtcodalu.Value)
            Dim codigo_cac As Integer = 0
            codigo_cac = Me.ddlCiclo.SelectedValue

            obj.AbrirConexion()
            'dt = obj.TraerDataTable("TRL_BuscaTramiteLinea", Me.txtAlumno.Text.Replace(" ", "%"), Me.cboEstado.SelectedValue, Me.Request.QueryString("ctf"), Me.Request.QueryString("id"))
            dt = obj.TraerDataTable("TRL_CursoMatriculaAsistencia", "1", codigo_alu, codigo_cac)
            obj.CerrarConexion()

            Me.gvCursosMatriculadosAsistencia.DataSource = dt
            Me.gvCursosMatriculadosAsistencia.DataBind()

            dt = Nothing

        Catch ex As Exception
            Response.Write("fnVerCursosUltimaAsistencia: " & ex.Message)
        End Try

    End Sub

    Private Sub PrevisualizarDeudaPorSemestre(ByVal codigo_dft As Integer)
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        Try
            obj.AbrirConexion()
            'dt = obj.TraerDataTable("TRL_BuscaTramiteLinea", Me.txtAlumno.Text.Replace(" ", "%"), Me.cboEstado.SelectedValue, Me.Request.QueryString("ctf"), Me.Request.QueryString("id"))
            dt = obj.TraerDataTable("TRL_conceptotramiteDeudaSemestre_Listar", "1", codigo_dft)
            obj.CerrarConexion()

            Me.gvDeudaPorSemestre.DataSource = dt
            Me.gvDeudaPorSemestre.DataBind()

            dt = Nothing

        Catch ex As Exception
            ShowMessage("PrevisualizarDeudaPorSemestre(): " & ex.Message.Replace("'", ""), MessageType.Error)
        End Try
    End Sub

    Private Sub MostrarConceptoTramiteInfo(ByVal opc As String, ByVal sw As Boolean)
        If opc = "ALU_ULTFECASIST" Then

            Me.ifrRetCiclo.Visible = sw
        Else
            Me.ifrRetCiclo.Visible = sw
        End If


    End Sub

    Protected Sub lnkAnt2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkAnt2.Click
        fnMostrarEvaluarFlujo(False)
    End Sub

    Private Sub verDetAcad(ByVal codigoUniversitario As String)
        Try

            ' Response.Write(codigoUniversitario)

            ifrHistorial.Visible = True
            'Response.Write("verDetAdm")
            Page.RegisterStartupScript("frame2", "<script>frameHistorial.document.location.href='../SISREQ/SisSolicitudes/clsbuscaralumno.asp?codigouniver_alu=" & codigoUniversitario.ToString & "&pagina=historial.asp'</script>")

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
        
    End Sub

    Private Sub verDetAdm(ByVal codigoUniversitario As String)
        Try
            ' Response.Write("verDetAcad")
            ifrInformes.Visible = True
            Dim dt As New Data.DataTable
            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            dt = obj.TraerDataTable("ConsultarMovimientosAlumno", codigoUniversitario, 0, 0, 0, 0, "P")
            'Response.Write(dt.Rows.Count)
            obj.CerrarConexion()
            Me.GvEstadoCue.DataSource = dt
            Me.GvEstadoCue.DataBind()
            dt.Dispose()

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try

    End Sub

    Private Sub accionURL(ByVal codigoUniversitario As String, ByVal codigoAlu As Integer, ByVal DetTipo As String, ByVal codigo_trl As Integer, ByVal codigo_dta As Integer, ByVal tramite As String, ByVal tramiteObservacion As String)
        Try
            ddlCiclo.Items.Clear()
            ifrAccion.Visible = True

            SelectCiclosMatriculados(codigoAlu)
            MostrarInformacionAdicional(codigo_trl, codigo_dta)
            Me.lblTramite.Text = tramite
            Me.txtTramiteObservacion.Text = tramiteObservacion
            If DetTipo = "" Then
                Me.ddlCiclo.Visible = False

            Else
                Me.ddlCiclo.Visible = True
                HdAccion.Value = "S"
                ddlAccion.SelectedValue = DetTipo
                If DetTipo = "T" Then
                    SelectCiclosMatriculados(codigoAlu)
                End If
            End If

        Catch ex As Exception

        End Try

    End Sub

    Private Sub SelectCiclosMatriculados(ByVal codigo_alu As Integer)

        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Dim objFun As New ClsFunciones

        objFun.CargarListas(Me.ddlCiclo, obj.TraerDataTable("ConsultarMatricula", "35", codigo_alu, "0", "0"), "codigo_Cac", "descripcion_Cac")
        obj.CerrarConexion()
        obj = Nothing

    End Sub

    Private Sub MostrarInformacionAdicional(ByVal codigo_trl As Integer, ByVal codigo_dta As Integer)
        Try
            Dim dt As New Data.DataTable
            Dim obj As New ClsConectarDatos

            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            dt = obj.TraerDataTable("TRL_TramiteAdicionalInfo", "1", codigo_trl, codigo_dta)
            obj.CerrarConexion()

            Me.gDatosAdicional.DataSource = dt
            Me.gDatosAdicional.DataBind()

            CalcularTotalesInformacionAdicional()

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub CalcularTotalesInformacionAdicional()
        Dim semestre As Integer = 0
        With gDatosAdicional
            For i As Integer = 0 To .Rows.Count - 1
                If .DataKeys(i).Values("tabla").ToString = "SEMESTRE" Then
                    semestre = semestre + 1
                End If

            Next
        End With
        If semestre > 0 Then
            lblNumSemestre.Text = semestre.ToString & " semestres académicos"
        End If

    End Sub

    Protected Sub ddlAccion_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlAccion.SelectedIndexChanged
        Try
            If ddlAccion.SelectedValue = "T" Then
                trcicloacad.Visible = True
                ddlAccion.Visible = True
            Else
                trcicloacad.Visible = False
                ddlAccion.Visible = False
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub verConceptoTramiteInfo(ByVal codigo_dft As Integer)
        Try

            Dim dt As New Data.DataTable
            Dim obj As New ClsConectarDatos

            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            dt = obj.TraerDataTable("TRL_conceptotramiteInfo_listar", "1", codigo_dft)
            obj.CerrarConexion()

            For i As Integer = 0 To dt.Rows.Count - 1

                If dt.Rows(i).Item("abreviatura").ToString = "ALU_ULTFECASIST" Then

                    MostrarConceptoTramiteInfo("ALU_ULTFECASIST", True)
                    fnVerCursosUltimaAsistencia()
                    If dt.Rows(i).Item("lectura") = 1 Then
                        Me.txtUltimaFechaAsistencia.Text = dt.Rows(i).Item("valor").ToString
                        Me.txtUltimaFechaAsistencia.ReadOnly = True
                    Else
                        If dt.Rows(i).Item("valor").ToString <> "" Then
                            Me.txtUltimaFechaAsistencia.Text = dt.Rows(i).Item("valor").ToString
                            Me.txtUltimaFechaAsistencia.Visible = False
                        Else
                            Me.txtUltimaFechaAsistencia.Visible = True
                        End If
                        Me.txtUltimaFechaAsistencia.ReadOnly = False
                    End If
                End If
            Next

            Session("conceptotramiteInfo") = dt

            dt = Nothing
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    

    Private Function fnValidaRegistroConceptoTramiteInfo(ByVal codigo_dft As Integer) As Boolean
        Try

            Dim rpta As Boolean = True
            Dim msje As String = ""

            Dim dt As New Data.DataTable
            dt = CType(Session("conceptotramiteInfo"), Data.DataTable)



            For i As Integer = 0 To dt.Rows.Count - 1

                If dt.Rows(i).Item("abreviatura").ToString = "ALU_ULTFECASIST" Then

                    If dt.Rows(i).Item("lectura").ToString = 1 Then
                        rpta = True
                        Exit For
                    Else
                        If txtUltimaFechaAsistencia.Text.Trim = "" Then
                            ShowMessage2("Ingrese Ultima Fecha Asistencia", "alert-warning")
                            Session("REG_ALU_ULTFECASIST") = False
                            txtUltimaFechaAsistencia.Focus()
                            rpta = False
                        Else
                            msje = ""
                            Session("REG_ALU_ULTFECASIST") = True
                            rpta = True
                        End If
                        Exit For
                    End If
                End If
            Next

            dt = Nothing


            Return rpta

        Catch ex As Exception
            Dim msje As String = ""
            msje = ex.Message.ToString
            ShowMessage2(msje, "alert-warning")
            Return False
        End Try


    End Function

    Protected Sub lnkSgt2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkSgt2.Click
        'Response.Write(rblEstado.SelectedValue)
        'Response.Write("id_per: " & Session("id_per"))
        If (Session("id_per") Is Nothing) Then
            'Response.Write("Sesion Finalizada")
            Response.Redirect("../../ErrorSistema.aspx")
        Else
            If fnValidaEvaluacionFlujo() Then
                fnEvaluacionFlujo()
            End If
            '  Response.Write("OK")
        End If



    End Sub

    Private Function fnValidaEvaluacionFlujo() As Boolean
        Try
            If rblEstado.SelectedIndex < 0 Then
                ShowMessage2("Seleccione aprobacion <i class=\'glyphicon glyphicon-thumbs-up\'></i> ó <i class=\'glyphicon glyphicon-thumbs-down\'></i>", "alert-warning")
                rblEstado.Focus()
                Return False
            End If



            Return True


        Catch ex As Exception
            Return False
        End Try

    End Function

    Sub fnEvaluacionFlujo()
        Dim _hddtareq As String = ""
        Dim ctf As String = ""
        Dim per As Integer = 0
        Dim mensaje(1) As String
        Dim idta As Integer = 0
        Dim obj As New ClsConectarDatos
        Dim lblResultado As Boolean = False

        Dim tieneEntrega As Int16 = 0
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        'obj.AbrirConexion()
        obj.IniciarTransaccion()
        Try

            idta = CInt(Desencriptar(hddtareq.Value.ToString))

            ctf = Me.Request.QueryString("ctf")
            per = CInt(Me.Request.QueryString("id"))


            If ctf = Session("dft_tfu").ToString Or ctf = 1 Then
                If hddtareq.Value <> "" Then
                    Dim Fila As GridViewRow
                    For i As Integer = 0 To Me.gvFlujoTramite.Rows.Count - 1
                        Fila = Me.gvFlujoTramite.Rows(i)
                        Dim valor As Boolean = CType(Fila.FindControl("chkElegir"), CheckBox).Checked
                        If (valor = True And CType(Fila.FindControl("chkElegir"), CheckBox).Enabled = True) Then
                            If fnValidaRegistroConceptoTramiteInfo(CInt(Me.gvFlujoTramite.DataKeys(i).Values("codigo_dft").ToString)) Then
                                'Inicio Proceso Detalle Flujo {
                                tieneEntrega = Me.gvFlujoTramite.DataKeys(i).Values("tieneEntrega")
                                lblResultado = obj.Ejecutar("TRL_DetalleFlujoTramite_Registrar", "A", idta, CInt(Me.gvFlujoTramite.DataKeys(i).Values("codigo_dft").ToString), "F", per, rblEstado.SelectedValue, Me.txtobservacionaprobacion.Text)
                                If lblResultado Then
                                    If Session("REG_ALU_ULTFECASIST") Then
                                        obj.Ejecutar("TRL_conceptotramiteInfo_Registrar", "1", CInt(Me.gvFlujoTramite.DataKeys(i).Values("codigo_dft").ToString), Me.txtUltimaFechaAsistencia.Text.Trim, "").copyto(mensaje, 0)

                                        If mensaje(0).Contains("correctamente") Then
                                            ShowMessage2("Se registro Ultima Fecha de Asistencia", "alert-success")
                                            txtUltimaFechaAsistencia.ReadOnly = True
                                        End If
                                    End If

                                    If Me.rblEstado.SelectedValue = "A" Then ' INICIO APROBAR

                                        ' Acciones que algunos flujos tramites estan configurados
                                        Select Case Me.gvFlujoTramite.DataKeys(i).Values("proceso").ToString
                                            Case "RETIRAR SEMESTRE"
                                                obj.Ejecutar("AnularMatriculaV2", ddlAccion.SelectedValue, CInt(Me.ddlCiclo.SelectedValue), CInt(Me.gvFlujoTramite.DataKeys(i).Values("codigo_Alu").ToString), per, Me.txtobservacionaprobacion.Text, CInt(Me.gvFlujoTramite.DataKeys(i).Values("codigo_dft").ToString), "").copyto(mensaje, 0)

                                                'If mensaje(0).Contains("correctamente") Then
                                                '    fnMostrarEvaluarFlujo(False)
                                                'End If

                                            Case "INACTIVAR ALUMNO"
                                                'Response.Write("TRL_InactivarAlumno " & ddlAccion.SelectedValue & ", " & CInt(Me.gvFlujoTramite.DataKeys(i).Values("codigo_Alu").ToString) & ", " & per & ", " & "''")
                                                obj.Ejecutar("TRL_InactivarAlumno", ddlAccion.SelectedValue, CInt(Me.gvFlujoTramite.DataKeys(i).Values("codigo_Alu").ToString), per, "").copyto(mensaje, 0)

                                                'If mensaje(0).Contains("ok") Then
                                                '    fnMostrarEvaluarFlujo(False)
                                                'End If
                                            Case "ACTIVAR ALUMNO"
                                                obj.Ejecutar("TRL_ActivarAlumno", "", CInt(Me.gvFlujoTramite.DataKeys(i).Values("codigo_Alu").ToString), per, "").copyto(mensaje, 0)
                                                '  Response.Write(mensaje(0))
                                                'If mensaje(0).Contains("ok") Then
                                                '    fnMostrarEvaluarFlujo(False)
                                                'End If

                                            Case "RETIRO DEFINITIVO"
                                                obj.Ejecutar("AnularMatriculaV2", ddlAccion.SelectedValue, 0, CInt(Me.gvFlujoTramite.DataKeys(i).Values("codigo_Alu").ToString), per, Me.txtobservacionaprobacion.Text, CInt(Me.gvFlujoTramite.DataKeys(i).Values("codigo_dft").ToString), "").copyto(mensaje, 0)

                                                'If mensaje(0).Contains("correctamente") Then
                                                '    fnMostrarEvaluarFlujo(False)
                                                'End If

                                            Case "GENERAR DEUDA POR CADA SEMESTRE"
                                                obj.Ejecutar("TRL_conceptotramiteDeudaSemestre_Registrar", "1", CInt(Me.gvFlujoTramite.DataKeys(i).Values("codigo_dft").ToString), "").copyto(mensaje, 0)

                                                'If mensaje(0).Contains("correctamente") Then
                                                '    fnMostrarEvaluarFlujo(False)
                                                'End If
                                            Case Else


                                        End Select

                                    End If ' FIN APROBAR
                                    If tieneEntrega = 0 Then

                                        EnviaCorreo(idta, "E")

                                    End If
                                    'Enviar correo segun su tiene entrega o no


                                End If ' FIN lblResultado
                                '} Fin Proceso Detalle Flujo
                            End If

                        End If
                    Next
                End If
            Else
                ShowMessage("No está autorizado para evaluar el flujo de tramites", MessageType.Warning)
            End If

            obj.TerminarTransaccion()

            If lblResultado Then
                ' EnviaCorreo(idta, "T")
                ShowMessage("HAZ CULMINADO DE EVALUAR SATISFACTORIAMENTE EL TRÁMITE", MessageType.Success)
                fnMostrarEvaluarFlujo(True)
                _hddtareq = Desencriptar(hddtareq.Value.ToString)
                fnListarFlujo(_hddtareq)
                fnLineaDeTiempoFlujo(_hddtareq)

                HdAccion.Value = ""
                Me.txtobservacionaprobacion.Enabled = False
                Me.rblEstado.Enabled = False
                Me.txtUltimaFechaAsistencia.Enabled = False
                CargaDatos()
            End If

            'obj.CerrarConexion() 
            obj = Nothing
        Catch ex As Exception
            Response.Write("fnEvaluacionFlujo()" & ex.Message & " -- " & ex.Source & " -- " & ex.StackTrace)
            obj.AbortarTransaccion()

        Finally



        End Try

    End Sub

 
    Protected Sub gDatosAdicional_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gDatosAdicional.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
           
            e.Row.Cells(0).Font.Bold = True
            e.Row.Cells(0).BackColor = Drawing.Color.AliceBlue

            e.Row.Cells(1).Font.Size = 10
            e.Row.Cells(1).BackColor = Drawing.Color.Linen
            e.Row.Cells(2).Font.Size = 10
            e.Row.Cells(2).BackColor = Drawing.Color.Linen
            If e.Row.Cells(0).Text = "ARCHIVO" Then
                Dim myLink As HyperLink = New HyperLink()
                myLink.NavigateUrl = "javascript:void(0)"
                myLink.Text = "Descargar"
                myLink.CssClass = "btn btn-primary"
                myLink.Attributes.Add("onclick", "DescargarArchivo('" & e.Row.Cells(1).Text & "')")


                e.Row.Cells(1).Controls.Add(myLink)
            End If

        End If
    End Sub

   

End Class
