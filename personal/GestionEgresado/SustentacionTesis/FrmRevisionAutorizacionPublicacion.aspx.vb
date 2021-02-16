Imports System.Data
Imports System.IO
Imports System.Collections.Generic

Partial Class FrmRevisionAutorizacionPublicacion
    Inherits System.Web.UI.Page
    Dim ruta As String = ConfigurationManager.AppSettings("SharedFiles")

    Private Sub ConsultarTesis(ByVal estado As String)
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Dim dt As New Data.DataTable
        dt = obj.TraerDataTable("SUST_ListarRevisionAutorizacion", estado)
        If dt.Rows.Count > 0 Then
            Me.gvTesis.DataSource = dt
            Me.gvTesis.DataBind()
        Else
            Me.gvTesis.DataSource = Nothing
            Me.gvTesis.DataBind()
        End If
        obj.CerrarConexion()
    End Sub

    'Private Sub ConsultarJuradoTesis(ByVal codigo_tes As Integer)
    '    Dim obj As New ClsConectarDatos
    '    obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
    '    obj.AbrirConexion()
    '    Dim dt As New Data.DataTable
    '    dt = obj.TraerDataTable("SUST_DatosJuradoSustentacion", codigo_tes)
    '    If dt.Rows.Count > 0 Then
    '        Me.gvJurado.DataSource = dt
    '        Me.gvJurado.DataBind()
    '    Else
    '        Me.gvJurado.DataSource = Nothing
    '        Me.gvJurado.DataBind()
    '    End If
    '    obj.CerrarConexion()
    'End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (Session("id_per") Is Nothing) Then
            Response.Redirect("../../../sinacceso.html")
        End If
        Try
            If IsPostBack = False Then
                ConsultarTesis(Me.ddlEstado.SelectedValue)
            End If
        Catch ex As Exception
            Response.Write(ex.Message.ToString)
        End Try
    End Sub


    Protected Sub ddlEstado_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlEstado.SelectedIndexChanged
        If (Session("id_per") Is Nothing) Then
            Response.Redirect("../../../sinacceso.html")
        End If
        Try
            ConsultarTesis(Me.ddlEstado.SelectedValue)
            Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "LoadingD", "fnLoading(false)", True)
            Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "LoadingEstado", "LoadingEstado();", True)

        Catch ex As Exception
            Response.Write(ex.Message.ToString)
        End Try
    End Sub


    Private Sub ConsultarDatosTesis(ByVal codigo_tes As Integer)
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Dim dt As New Data.DataTable
        Dim validar As Integer = 0
        Dim mismoregistro As Integer = 0
        dt = obj.TraerDataTable("SUST_DatosAutorizacionPublicacion", codigo_tes)
        If dt.Rows.Count > 0 Then
            Dim str As String = ""
            str += "<hr style='border-top:1px solid #000;margin-top: 10px;margin-bottom: 10px;'>"
            For i As Integer = 0 To dt.Rows.Count - 1
                str += "<div class='form-group'>"
                str += "<div class='col-sm-12 col-md-12'>"
                str += "<div class='alert bg-danger text-white'><strong>Autor N° " + (i + 1).ToString + "</strong></div>"
                str += "</div>"
                str += "</div>"
                str += "<div class='form-group'>"
                str += "<label class='col-sm-3 col-md-3 control-label'>Código universitario</label>"
                str += "<div class='col-sm-3 col-md-2'>"
                str += "<input type='text' class='form-control' value='" + dt.Rows(i).Item("codigoUniver_Alu").ToString + "' readonly='readonly' >"
                str += "</div>"
                str += "<label class='col-sm-1 col-md-1 control-label'>Bachiller</label>"
                str += "<div class='col-sm-5 col-md-6'>"
                str += "<input type='text' class='form-control' value='" + dt.Rows(i).Item("alumno").ToString + "' readonly='readonly' >"
                str += "</div>"
                str += "</div>"
                str += "<div class='form-group'>"
                str += "<label class='col-sm-3 col-md-3 control-label'>Teléfono</label>"
                str += "<div class='col-sm-5 col-md-5'>"
                str += "<input type='text' class='form-control' value='" + dt.Rows(i).Item("telefonomovil").ToString + " / " + dt.Rows(i).Item("telefono").ToString + " / " + dt.Rows(i).Item("telefonocasa").ToString + " ' readonly='readonly' >"
                str += "</div>"
                str += "</div>"
                str += "<div class='form-group'>"
                str += "<label class='col-xs-3 col-sm-3 col-md-3 control-label'>Correo electrónico</label>"
                str += "<div class='col-sm-5 col-md-5'>"
                str += "<input type='text' class='form-control' value='" + dt.Rows(i).Item("correoalumno").ToString + " ' readonly='readonly' >"
                str += "</div>"
                str += "</div>"
                str += "<div class='form-group'>"
                If dt.Rows(i).Item("tipo_dap") <> "" Then
                    str += "<label class='col-xs-3 col-sm-3 col-md-3 control-label' style='color:red; font-weight:bold;'>Autorización</label>"
                    str += "<div class='col-sm-2 col-md-2'>"
                    str += "<input type='text' class='form-control' value='" + dt.Rows(i).Item("tipo_dap").ToString + " ' readonly='readonly' >"
                    str += "</div>"
                Else
                    str += "<label class='col-xs-3 col-sm-3 col-md-2 control-label' style='color:red; font-weight:bold;'>&nbsp;</label>"
                    str += "<label class='col-sm-4 col-md-4' style='color:red; font-weight:bold;'>No cuenta con una autorización registrada</label>"
                    validar = validar + 1
                End If
                If dt.Rows(i).Item("mesesrestriccion") <> "" Then
                    str += "<label class='col-xs-2 col-sm-2 col-md-2 control-label'>Meses restricción</label>"
                    str += "<div class='col-sm-1 col-md-1'>"
                    str += "<input type='text' class='form-control' value='" + dt.Rows(i).Item("mesesrestriccion").ToString + " ' readonly='readonly' >"
                    str += "</div>"
                End If
                If dt.Rows(i).Item("archivo") <> "" Then
                    str += "<div class='col-sm-3 col-md-2'>"
                    str += "<button type='button' class='btn btn-sm btn-success btn-radius' onclick='fnDescargar(""../../DescargarArchivo.aspx?Id=" + dt.Rows(i).Item("archivo") + "&idt=32"");return false;' >Documento de Justificación</button>"
                    str += "</div>"
                End If
                str += "</div>"
                str += "<hr style='border-top:1px solid #000;margin-top: 10px;margin-bottom: 10px;'>"
                'Verificar si autorizaciones son iguales
                If i > 0 Then
                    If dt.Rows(i).Item("tipo_dap") <> dt.Rows(i - 1).Item("tipo_dap") Or dt.Rows(i).Item("mesesrestriccion") <> dt.Rows(i - 1).Item("mesesrestriccion") Then
                        mismoregistro = mismoregistro + 1
                    End If
                End If
            Next
            Me.alumnos.InnerHtml = str
            Me.txtTitulo.Text = dt.Rows(0).Item("titulo").ToString
            Me.txtCarrera.Text = dt.Rows(0).Item("nombreOficial_cpf").ToString
            Me.txtlinea.Text = dt.Rows(0).Item("linea").ToString
            'Me.txtarea.Text = dt.Rows(0).Item("area").ToString
            'Me.txtsubarea.Text = dt.Rows(0).Item("subarea").ToString
            Me.txtdisciplina.Text = dt.Rows(0).Item("disciplina").ToString
            Me.txtasesor.Text = dt.Rows(0).Item("asesor").ToString
            Me.txtorcid.Text = dt.Rows(0).Item("orcid").ToString
            'If validar = 0 Then
            Me.btnConformidad.Visible = True
            Me.btnRechazar.Visible = True
            'If mismoregistro = 0 Then  'Si es administrador o director de biblioteca podrá rechazar
            '    Me.btnConformidad.Visible = True
            '    Me.btnRechazar.Visible = False
            '    If Request("ctf") = 1 Or Request("ctf") = 76 Then
            '        btnRechazar.Visible = True
            '    End If
            'Else
            '    Me.btnRechazar.Visible = True
            '    Me.btnConformidad.Visible = False
            'End If
            'Else
            'Me.btnConformidad.Visible = False
            'Me.btnRechazar.Visible = False
            'End If

        End If
        obj.CerrarConexion()
    End Sub

    Protected Sub gvTesis_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvTesis.RowCommand
        If (Session("id_per") Is Nothing) Then
            Response.Redirect("../../../sinacceso.html")
        End If
        Try
            If (e.CommandName = "VerDatos") Then
                Me.Lista.Visible = False
                Me.DivAutorizacion.Visible = True
                ConsultarDatosTesis(Me.gvTesis.DataKeys(e.CommandArgument).Values("codigo_apu"))
                Me.hddta.Value = Me.gvTesis.DataKeys(e.CommandArgument).Values("codigo_dta")
                Me.hdpst.Value = Me.gvTesis.DataKeys(e.CommandArgument).Values("codigo_pst")
                Me.hdtes.Value = Me.gvTesis.DataKeys(e.CommandArgument).Values("codigo_Tes")
                Me.hdapu.Value = Me.gvTesis.DataKeys(e.CommandArgument).Values("codigo_apu")
                Me.hdtest.Value = Me.gvTesis.DataKeys(e.CommandArgument).Values("codigo_test")

                Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "LoadingEstado", "LoadingEstado();", True)

                Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "LoadingD", "fnLoading(false)", True)

            End If
            If (e.CommandName = "Anular") Then
                
                Dim dt As New Data.DataTable
                dt = RevertirInstanciaTramite(Me.gvTesis.DataKeys(e.CommandArgument).Values("codigo_dta"), 1, -1)
                If dt.Rows.Count > 0 Then ' SI HAY RESPUESTA DEL CAMBIO DE ETAPA
                    If dt.Rows(0).Item("revision") = True Then ' SI SE HIZO LA REVISIÓN
                        Dim dtAnular As Data.DataTable
                        dtAnular = AnularAutorizacionPublicacion(Me.gvTesis.DataKeys(e.CommandArgument).Values("codigo_apu"))
                        If dtAnular.Rows.Count > 0 Then
                            If dtAnular.Rows(0).Item("Respuesta").ToString = 1 Then
                                Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "alert65", "fnMensaje('success','Se revirtió etapa de trámite correctamente')", True)
                            Else
                                Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "alert53", "fnMensaje('error','No se pudo revertir autorización de publicación')", True)
                            End If
                        Else
                            Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "alert53", "fnMensaje('error','No se pudo revertir autorización de publicación')", True)

                        End If
                        ConsultarTesis(Me.ddlEstado.SelectedValue)
                    Else ' NO SE ACTUALIZO LA ETAPA DEL TRAMITE
                        Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "alert53", "fnMensaje('error','No se pudo revertir etapa de trámite')", True)
                    End If
                Else
                    Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "alert8", "fnMensaje('error','No se pudo actualizar etapa de trámite')", True)
                End If
                Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "LoadingEstado", "LoadingEstado();", True)
                Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "LoadingD", "fnLoading(false)", True)

            End If
        Catch ex As Exception
            Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "LoadingD", "fnMensaje('error','" + ex.Message.ToString + "')", True)

        End Try

    End Sub

    Protected Sub btnCerrar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCerrar.Click
        If (Session("id_per") Is Nothing) Then
            Response.Redirect("../../../sinacceso.html")
        End If
        Me.Lista.Visible = True
        Me.DivAutorizacion.Visible = False
        Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "LoadingEstado", "LoadingEstado();", True)

        Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "LoadingD", "fnLoading(false)", True)

    End Sub

    Protected Sub btnConformidad_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnConformidad.Click
        If (Session("id_per") Is Nothing) Then
            Response.Redirect("../../../sinacceso.html")
        End If
        Dim codigo_Dot As Integer = 0
        codigo_Dot = GenerarAutorizacionPublicacion(Me.hdtes.Value, Session("id_per"), Session("perlogin").ToString, Me.hdtest.Value)
        If codigo_Dot > 0 Then
            Dim dta As New Data.DataTable
            dta = ACtualizarCodigoDocumento(Me.hdapu.Value, codigo_Dot)
            If dta.Rows(0).Item("Respuesta") = 1 Then
                Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "alert2", "fnMensaje('success','se generó correctamente documento de autorización de publicación')", True)
                'ACTUALIZAR ETAPA DE TRÁMITE
                Dim dttramite As New Data.DataTable
                dttramite = ActualizarEtapaTramite(Me.hddta.Value, "1", "A")
                If dttramite.Rows.Count > 0 Then ' SI HAY RESPUESTA DEL CAMBIO DE ETAPA
                    If dttramite.Rows(0).Item("revision") = True Then ' SI SE HIZO LA REVISIÓN
                        If dttramite.Rows(0).Item("email") = True Then 'SI SE ENVIO EL CORREO(INSER´CIÓN CORREOMASIVO)
                            Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "alert6", "fnMensaje('success','Se actualizó etapa de trámite correctamente')", True)
                        Else  ' NO SE INSERTO ENVIO CORREO MASIVO
                            Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "alert7", "fnMensaje('success','Se actualizó Etapa de trámite correctamente, pero no se pudo realizar el envío de correo correctament')", True)
                        End If
                    Else ' NO SE ACTUALIZO LA ETAPA DEL TRAMITE
                        Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "alert5", "fnMensaje('error','No se pudo finalizar trámite')", True)
                    End If
                Else
                    Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "alert8", "fnMensaje('error','No se pudo actualizar etapa de trámite')", True)
                End If
                ConsultarTesis(Me.ddlEstado.SelectedValue)
                Me.Lista.Visible = true
                Me.DivAutorizacion.Visible = False
            Else
                Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "alert2", "fnMensaje('error','No se pudo generar documento de autorización de publicación')", True)
            End If
        Else
            Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "error", "fnMensaje('error','No se pudo generar documento de autorización de publicación')", True)
        End If

        Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "LoadingEstado", "LoadingEstado();", True)

        Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "LoadingD", "fnLoading(false)", True)

    End Sub

    Private Function ACtualizarCodigoDocumento(ByVal codigo_apu As Integer, ByVal codigo_dot As Integer) As Data.DataTable
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Dim dt As New Data.DataTable
        dt = obj.TraerDataTable("SUST_ActualizarCodigoDocumentoAutorizacion", codigo_apu, codigo_dot)
        obj.CerrarConexion()
        Return dt
    End Function

    Private Function GenerarAutorizacionPublicacion(ByVal codigo_Tes As Integer, ByVal codigo_per As Integer, ByVal usuario_per As String, ByVal codigo_Test As Integer) As Integer
        'Dim codigo_dot As Integer
        Dim codigo_cda As Integer = 6 ''-- Configuracion del documento
        Dim codigo_dot As Integer = 0 '- Codigo del doumento generado en la tabla DOC_documentoArchivo
        Dim serieCorrelativoDoc As String '- Serie o numeracion del comprobante
        Try
            'Dim obj As New clsDocumentacion
            ''''**** 1. GENERA CORRELATIVO DEL DOCUMENTO CONFIGURADO *******************************************************
            serieCorrelativoDoc = clsDocumentacion.ObtenerSerieCorrelativoDocPorCda(codigo_cda, Year(Now), codigo_per)

            ''''******* GENERA DOCUMENTO PDF *****************************************************************************
            If serieCorrelativoDoc <> "" Then
                '--------necesarios
                Dim arreglo As New Dictionary(Of String, String)
                arreglo.Add("nombreArchivo", "AutorPublTesis")
                arreglo.Add("sesionUsuario", usuario_per)
                '-----------------                
                arreglo.Add("codigo_tes", codigo_Tes)
                arreglo.Add("tipoEstudio", codigo_Test)
                '******** 2. GENERA DOCUMENTO PDF **************************************************************
                codigo_dot = clsDocumentacion.generarDocumentoPdf(serieCorrelativoDoc, arreglo)
                'codigo_dot = clsDocumentacion.generarDocumentoPdf(serieCorrelativoDoc, memory, arreglo)
                '**********************************************************************************************
            Else
                codigo_dot = 0
            End If

        Catch ex As Exception
            codigo_dot = 0
        End Try
        Return codigo_dot
    End Function

    Protected Sub gvTesis_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvTesis.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            If Me.gvTesis.DataKeys(e.Row.RowIndex).Values("documento") <> "0" Then
                Dim btn As LinkButton = DirectCast(e.Row.Cells(3).FindControl("btnVer"), LinkButton)
                btn.Text = "<span class='fa fa-download'></span>"
                btn.OnClientClick = "fnDescargar('../../DescargarArchivo.aspx?Id=" + Me.gvTesis.DataKeys(e.Row.RowIndex).Values("documento") + "&idt=30');return false;"
                btn.CssClass = "btn btn-sm btn-info btn-radius"
                btn.ToolTip = "Descargar documento de autorización"
                If Request("ctf") = 1 Or Request("ctf") = 76 Then 'ADMINISTRADOR O DIRECTOR DE BIBLIOTECA
                    Me.gvTesis.Columns(4).Visible = True
                Else
                    Me.gvTesis.Columns(4).Visible = False
                End If
            Else
                Me.gvTesis.Columns(4).Visible = False
            End If
            If Me.gvTesis.DataKeys(e.Row.RowIndex).Values("informe") <> "0" Then
                Dim btn As LinkButton = DirectCast(e.Row.Cells(2).FindControl("btnDescargar"), LinkButton)
                btn.OnClientClick = "fnDescargar('../../DescargarArchivo.aspx?Id=" + Me.gvTesis.DataKeys(e.Row.RowIndex).Values("informe") + "&idt=23');return false;"
            End If
           
        End If
    End Sub

    Private Function ActualizarEtapaTramite(ByVal codigo_dta As Integer, ByVal tipooperacion As String, ByVal estadoaprobacion As String) As Data.DataTable
        Dim dt As New Data.DataTable
        dt.Columns.Add("revision")
        dt.Columns.Add("registros")
        dt.Columns.Add("email")
        Try
            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            Dim dtdatos As New Data.DataTable
            dtdatos = obj.TraerDataTable("SUST_DatosTramitesAutores", codigo_dta)
            obj.CerrarConexion()
            For i As Integer = 0 To dtdatos.Rows.Count - 1
                Dim cmp As New clsComponenteTramiteVirtualCVE
                Dim objcmp As New List(Of Dictionary(Of String, Object))()
                'cmp._codigo_dta = codigo_dta
                cmp._codigo_dta = dtdatos.Rows(i).Item("codigo_dta")
                'cmp.tipoOperacion = "1"
                cmp.tipoOperacion = tipooperacion
                cmp._codigo_per = Session("id_per")
                cmp._codigo_tfu = 47 ' tipo funcion BIBLIOTECA
                If estadoaprobacion = "A" Then
                    cmp._estadoAprobacion = "A" ' DA CONFORMIDAD OSEA APRUEBA
                    cmp._observacionEvaluacion = "Se aprueba trámite"
                Else
                    cmp._estadoAprobacion = "R"
                    cmp._observacionEvaluacion = "Se rechaza trámite"
                End If
                objcmp = cmp.mt_EvaluarTramite()

                For Each fila As Dictionary(Of String, Object) In objcmp
                    dt.Rows.Add(fila.Item("evaluacion"), fila.Item("registos evaluados").ToString, fila.Item("email"))
                Next
            Next
            Return dt
        Catch ex As Exception
            dt.Rows.Add(False, "", False)
            Return dt
        End Try
    End Function

    Private Function RechazarAutorizacion(ByVal codigo_apu As Integer, ByVal motivo As String, ByVal usuario As Integer) As Data.DataTable
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Dim dt As New Data.DataTable
        dt = obj.TraerDataTable("SUST_RechazarAutorizacionBiblioteca", codigo_apu, motivo, usuario)
        obj.CerrarConexion()
        Return dt
    End Function

    Private Sub EnviarNotificacionRechazo(ByVal codigo_per_emisor As Integer, ByVal codigo_tfu_emisor As Integer, ByVal codigo_apl As Integer, ByVal codigo_Tes As Integer)
        Try
            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            Dim dt As New Data.DataTable
            dt = obj.TraerDataTable("SUST_DatosNotificacionRechazoAutorizacion", codigo_Tes, Me.txtMotivoRechazo.Text)
            obj.CerrarConexion()

            If dt.Rows.Count > 0 Then

                Dim codigo_envio As Integer = ClsComunicacionInstitucional.ObtenerCodigoEnvio(codigo_per_emisor, codigo_tfu_emisor, codigo_apl)
                Dim correo_destino As String = ""
                Dim mensaje As String = ""
                Dim bandera As Integer = 0

                For i As Integer = 0 To dt.Rows.Count - 1
                    If ConfigurationManager.AppSettings("CorreoUsatActivo") = 1 Then
                        correo_destino = dt.Rows(i).Item("correobachiller")
                    Else
                        correo_destino = "hcano@usat.edu.pe,ravalos@usat.edu.pe,csenmache@usat.edu.pe,yperez@usat.edu.pe"
                    End If
                    If ClsComunicacionInstitucional.EnviarNotificacionEmail(codigo_envio, "SUST", "RECHBIB", "1", codigo_per_emisor, "codigo_alu", dt.Rows(i).Item("codigo_alu"), codigo_apl, correo_destino, "", "RECHAZO DE AUTORIZACIÓN DE PUBLICACIÓN", "", dt.Rows(i).Item("bachiller"), Me.txtMotivoRechazo.Text) Then
                        mensaje += "<br>Notificación enviada al bachiller : " + dt.Rows(i).Item("bachiller")
                    Else
                        mensaje += "<br>No se pudo enviar notificación al bachiller: " + dt.Rows(i).Item("bachiller")
                        bandera = bandera + 1
                    End If
                Next
                If bandera = 0 Then
                    ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "valida1", "fnMensaje('success','" + mensaje + "');", True)
                Else
                    ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "valida2", "fnMensaje('error','" + mensaje + "');", True)
                End If
            End If
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "valida3", "fnMensaje('error','Operación no se ejecuto correctamente');", True)
        End Try

    End Sub

    Private Function RevertirInstanciaTramite(ByVal codigo_dta As Integer, ByVal nroinstancias As Integer, ByVal codigo_tfu As Integer) As Data.DataTable
        Dim dt As New Data.DataTable
        dt.Columns.Add("revision")
        dt.Columns.Add("registros")
        dt.Columns.Add("email")
        Try
            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            Dim dtdatos As New Data.DataTable
            dtdatos = obj.TraerDataTable("SUST_DatosTramitesAutoresRevertir", codigo_dta, codigo_tfu)
            obj.CerrarConexion()
            For i As Integer = 0 To dtdatos.Rows.Count - 1

                Dim cmp As New clsComponenteTramiteVirtualCVE
                Dim objcmp As New List(Of Dictionary(Of String, Object))()
                'cmp._codigo_dta = codigo_dta
                cmp._codigo_dta = dtdatos.Rows(i).Item("codigo_dta")
                cmp._codigo_tfu = dtdatos.Rows(i).Item("codigo_tfu")
                cmp._numeroinstanciareversa_dft = nroinstancias ' Número de instancias que va a retornar
                cmp.tipoOperacion = 1
                objcmp = cmp.mt_EvaluarTramite()

                For Each fila As Dictionary(Of String, Object) In objcmp
                    dt.Rows.Add(fila.Item("evaluacion"), fila.Item("registos evaluados").ToString, fila.Item("email"))
                    'dt.Rows.Add(True, "ok", True)
                Next
            Next
            Return dt
        Catch ex As Exception
            dt.Rows.Add(False, "", False)
            Return dt
        End Try
    End Function

    Private Function AnularAutorizacionPublicacion(ByVal codigo_apu As Integer) As Data.DataTable
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Dim dt As New Data.DataTable
        dt = obj.TraerDataTable("SUST_AnularAutorizacionPublicacion", codigo_apu, Session("id_per"))
        obj.CerrarConexion()
        Return dt

    End Function

    Protected Sub btnEnviarRechazar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEnviarRechazar.Click
        If (Session("id_per") Is Nothing) Then
            Response.Redirect("../../../sinacceso.html")
        End If
        Dim dt As New Data.DataTable
        dt = RechazarAutorizacion(Me.hdapu.Value, Me.txtMotivoRechazo.Text, Session("id_per"))
        If dt.Rows(0).Item("Respuesta") = 1 Then
            Me.Lista.Visible = True
            Me.DivAutorizacion.Visible = False
            EnviarNotificacionRechazo(Session("id_per"), Request("ctf"), 47, Me.hdtes.Value)
            ConsultarTesis(Me.ddlEstado.SelectedValue)
            Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "alert0", "fnMensaje('success','" + dt.Rows(0).Item("Mensaje").ToString + "')", True)
        Else
            Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "alert1", "fnMensaje('error','" + dt.Rows(0).Item("Mensaje").ToString + "')", True)
        End If
        Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "LoadingEstado", "LoadingEstado();", True)

        Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "LoadingD", "fnLoading(false)", True)

    End Sub
End Class


