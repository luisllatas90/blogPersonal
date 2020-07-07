﻿Imports System.IO
Imports System.Collections.Generic
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
            Response.Redirect("../../../sinacceso.html")
        End If

        Try
            'Dim cmp As New clsComponenteTramiteVirtualCVE
            'Dim objcmp As New List(Of Dictionary(Of String, Object))()
            'cmp._codigo_dta = 59822
            'cmp.tipoOperacion = "1"
            'cmp._codigo_per = 684
            'cmp._codigo_tfu = 9
            'cmp._estadoAprobacion = "A"
            ''cmp._estadoAprobacion = "R"
            'cmp._observacionEvaluacion = "Aprobar componente"
            ''cmp._observacionEvaluacion = "Rechazar componente"
            'objcmp = cmp.mt_EvaluarTramite()
            ''Response.Write(objcmp)

            'Dim javaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
            'Dim myObjectJson As String = javaScriptSerializer.Serialize(objcmp)
            'Response.Clear()
            'Response.ContentType = "application/json; charset=utf-8"
            'Response.Write(myObjectJson)
            'Response.[End]()

            'Exit Sub

            'Response.Write(ConfigurationManager.AppSettings("CorreoUsatActivo"))
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
            objFun.CargarListasGrupo(Me.ddlEscuela, obj.TraerDataTable("ConsultarEscuelaPorPersonalV2", "3", CInt(Session("id_per").ToString()), 0, CInt(Me.Request.QueryString("ctf").ToString)), "codigo_Cpf", "nombre_Cpf", "TipoEstudio")

            Page.RegisterStartupScript("Combo", "<script> GroupDropdownlist();</script>")
            obj.CerrarConexion()
            obj = Nothing
            objFun = Nothing

            'Dim cls As New ClsMail
            'Dim rptaEmail As String = ""
            'rptaEmail = cls.EnviarMailVariosV3("campusvirtual@usat.edu.pe", "epena@usat.edu.pe", "ASUNTO", "MENSAJE", True)
            'Response.Write("rpta: " & rptaEmail)

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
            dt2 = dt ' obj.TraerDataTable("TRL_BuscaTramiteLinea", Me.txtAlumno.Text.Replace(" ", "%"), Me.cboEstado.SelectedValue, Me.Request.QueryString("ctf"), Me.Request.QueryString("id"), cpf.Value, 0)
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
                'Response.Write(dt.Rows(i).Item("codigo_ctr") & "<br>")
                If fnAgruparTramitesSelectValida(dt.Rows(i).Item("codigo_ctr"), dtconcepto) Then
                    ' Response.Write(i & "<br>")
                    'Response.Write("---> " & dt.Rows(i).Item("codigo_ctr") & " - " & dt.Rows(i).Item("descripcion_Sco") & " - " & dt.Rows(i).Item("tipoestudio") & "<br>")
                    'Response.Write(dt.Rows(i).Item("codigo_ctr") & "<br>")
                    Dim row As Data.DataRow = dtconcepto.NewRow()
                    row("codigo_ctr") = dt.Rows(i).Item("codigo_ctr")
                    row("descripcion_ctr") = dt.Rows(i).Item("descripcion_Sco") & " - " & dt.Rows(i).Item("tipoestudio") & " - " & dt.Rows(i).Item("egresado")
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
                'Response.Write("EVALUAR<br>")
                rblEstado.SelectedIndex = -1
                ifrGeneraDeudaPorSemestre.Visible = False
                ifrAccion.Visible = False
                ifrHistorial.Visible = False
                ifrInformes.Visible = False
                Me.lblNumSemestre.Text = ""
                Me._FlujoCheck.Text = 0
                Me.gvDeudaPorSemestre.DataSource = Nothing
                Me.gvDeudaPorSemestre.DataBind()
                Me.lblSemestreMatriculado.Visible = False
                Me.lblTotalSemestre.Visible = False
                Me.lblInfoAdicional.Visible = False

                MostrarConceptoTramiteInfo("", False)

                Dim codigo_dta As Integer = 0
                hddtareq.Value = Encriptar(gvDatos.DataKeys(index).Values("codigo_dta").ToString)
                codigo_dta = gvDatos.DataKeys(index).Values("codigo_dta")
                Me.nroTramite.Value = gvDatos.DataKeys(index).Values("glosaCorrelativo_trl")

                Dim sco As Integer = 0
                sco = CInt(gvDatos.DataKeys(index).Values("sco").ToString)
                Session("trm_sco") = sco

                'Response.Write("fnListarFlujo1<br>")
                fnListarFlujo(codigo_dta)
                'Response.Write("fnListarFlujo2<br>")

                fnInformacionTramite(CInt(gvDatos.DataKeys(index).Values("codigo_dta").ToString))

                If sco = 1 Then
                    fnInformacionTramiteRequisito(codigo_dta)
                    fnMostrarEvaluar(True)
                    fnLineaDeTiempo(codigo_dta)
                    fnListarRequisitos(codigo_dta)
                    fnMostrarConfirmacionReq(False)
                Else
                    Me.txtobservacionaprobacion.Enabled = True
                    Me.rblEstado.Enabled = True
                    Me.txtUltimaFechaAsistencia.Enabled = True
                    '                    MostrarConceptoTramiteInfo("", False)
                    fnMostrarEvaluarFlujo(True)
                    '                    ifrGeneraDeudaPorSemestre.Visible = False
                    fnLineaDeTiempoFlujo(codigo_dta)

                End If

            End If
        Catch ex As Exception
            Response.Write("Error gvDatos_RowCommand: " & ex.Message & " - " & ex.StackTrace)
        End Try
    End Sub

    Private Sub fnInformacionTramiteRequisito(ByVal codigo_dta As Integer)
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        Dim str As String = ""
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString

        Try

            obj.AbrirConexion()
            dt = obj.TraerDataTable("TRL_informaciontramite", "2", codigo_dta)
            obj.CerrarConexion()

            If dt.Rows.Count > 0 Then
                Me.lblInfEstNumero3.Text = dt.Rows(0).Item("numero").ToString()
                Me.lblInfEstEscuela3.Text = dt.Rows(0).Item("escuela").ToString()
                Me.lblInfEstAlumno3.Text = dt.Rows(0).Item("estudiante").ToString()
                Me.lblInfEstEmail3.Text = dt.Rows(0).Item("UserPrincipalName").ToString()
                Me.lblInfEstEmailp3.Text = dt.Rows(0).Item("eMail_Alu").ToString()

                Me.lblInfEstTelefono3.Text = dt.Rows(0).Item("telefonoMovil_Dal").ToString()
                Me.lblInfEstCodUni3.Text = dt.Rows(0).Item("codigouniver_alu").ToString()
                Me.lblInfEstTramite3.Text = dt.Rows(0).Item("descripcion_ctr").ToString()
                Me.lblInfEstDescripcion3.Text = dt.Rows(0).Item("observacion_trl").ToString()
                Me.lblInfEstFecNac3.Text = dt.Rows(0).Item("fechaNacimiento_Alu").ToString()
                Me.lblInfEstDocIden3.Text = dt.Rows(0).Item("nroDocIdent_Alu").ToString()
            End If

        Catch ex As Exception
            Response.Write("itr" & ex.Message)
        Finally
            obj = Nothing
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
                Me.lblInfEstEmail.Text = dt.Rows(0).Item("UserPrincipalName").ToString()
                Me.lblInfEstEmailP.Text = dt.Rows(0).Item("eMail_Alu").ToString()
                Me.lblInfEstTelefono.Text = dt.Rows(0).Item("telefonoMovil_Dal").ToString()
                Me.lblInfEstCodUni.Text = dt.Rows(0).Item("codigouniver_alu").ToString()
                Me.lblInfEstFecNac.Text = dt.Rows(0).Item("fechaNacimiento_Alu").ToString()
                Me.lblInfEstTramite.Text = dt.Rows(0).Item("descripcion_ctr").ToString()
                Me.lblInfEstDescripcion.Text = dt.Rows(0).Item("observacion_trl").ToString()
                Me.lblInfEstDocIden.Text = dt.Rows(0).Item("nroDocIdent_Alu").ToString()
            End If



        Catch ex As Exception
            Response.Write("1" & ex.Message)
        Finally
            obj = Nothing
        End Try

    End Sub
    Private Sub fnInformacionTramite2(ByVal codigo_dta As Integer)
        'Response.Write(codigo_dta)

        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        Dim str As String = ""
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        Dim codigo_trl As Integer = 0
        Try

            obj.AbrirConexion()
            dt = obj.TraerDataTable("TRL_informaciontramite", "2", codigo_dta)
            obj.CerrarConexion()

            If dt.Rows.Count > 0 Then
                Dim obEnc As New Object
                Dim codigofoto As String
                obEnc = Server.CreateObject("EncriptaCodigos.clsEncripta")
                codigofoto = obEnc.CodificaWeb("069" & dt.Rows(0).Item("codigouniver_alu").ToString())
                obEnc = Nothing
                Me.lblInfEstNumero2.Text = dt.Rows(0).Item("numero").ToString()

                Me.lblInfEstTramite2.Text = dt.Rows(0).Item("descripcion_ctr").ToString()
                Me.lblInfEstDescripcion2.Text = dt.Rows(0).Item("observacion_trl").ToString()

                Me.lblInfEstDocIden2.Text = dt.Rows(0).Item("nroDocIdent_Alu").ToString()
                Me.lblInfEstEscuela2.Text = dt.Rows(0).Item("escuela").ToString()
                Me.lblInfEstAlumno2.Text = dt.Rows(0).Item("estudiante").ToString()
                'Me.lblInfEstEmail2.Text = dt.Rows(0).Item("email").ToString()
                '#EPENA 20082019 Redmine: Tareas #4193{
                Me.lblInfEstEmail2P.Text = dt.Rows(0).Item("eMail_Alu").ToString()
                Me.lblInfEstEmail2E.Text = dt.Rows(0).Item("UserPrincipalName").ToString()
                '}#EPENA 20082019
                Me.lblInfEstTelefono2.Text = dt.Rows(0).Item("telefonoMovil_Dal").ToString()
                Me.lblInfEstFecNac2.Text = dt.Rows(0).Item("fechaNacimiento_Alu").ToString()
                Me.lblInfEstCodUni2.Text = dt.Rows(0).Item("codigouniver_alu").ToString()
                Me.tdFotoAlu.InnerHtml = "<img border='1' src='https://intranet.usat.edu.pe/imgestudiantes/" & codigofoto & "' width='100' height='118' alt='Sin Foto'> "
                codigo_trl = dt.Rows(0).Item("codigo_trl")

                obj.AbrirConexion()
                dt = obj.TraerDataTable("TRL_informacionEstudianteDeuda", "1", codigo_trl, codigo_dta)
                obj.CerrarConexion()

                Me.gdDeudaTramite.DataSource = dt
                Me.gdDeudaTramite.DataBind()

            End If



        Catch ex As Exception
            Response.Write(ex.Message)
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

            Dim lnk2 As LinkButton
            lnk2 = TryCast(e.Row.FindControl("lnkButton2"), LinkButton)
            lnk2.ToolTip = e.Row.Cells(0).Text

            Dim lnk3 As LinkButton
            lnk3 = TryCast(e.Row.FindControl("lnkButton3"), LinkButton)
            lnk3.ToolTip = e.Row.Cells(0).Text

            'tiene requisito: gvDatos.DataKeys(e.Row.RowIndex).Values(1).ToString()
            'tiene sco: gvDatos.DataKeys(e.Row.RowIndex).Values(5).ToString()

            Dim lsDataKeyValue As String = gvDatos.DataKeys(e.Row.RowIndex).Values(1).ToString()
            Dim lsDataKeyValuesco As String = gvDatos.DataKeys(e.Row.RowIndex).Values(5).ToString()
            Dim nTieneEvaluacionPersonal As Integer = gvDatos.DataKeys(e.Row.RowIndex).Values(8).ToString()
            Dim valorEvaluacionPersonal As String = gvDatos.DataKeys(e.Row.RowIndex).Values(9).ToString()
            Dim FuncionEvaluacionPersonal As Integer = gvDatos.DataKeys(e.Row.RowIndex).Values(3).ToString()
            'Solo que evalue ADMIN(1) y Personal Especialista Asignado


            ' Response.Write("<br>" & gvDatos.DataKeys(e.Row.RowIndex).Values(8).ToString())
            ' Response.Write("<br>" & gvDatos.DataKeys(e.Row.RowIndex).Values(9).ToString())
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

           

            If nTieneEvaluacionPersonal = 1 And (Request.QueryString("ctf") = 1 Or Request.QueryString("ctf") = FuncionEvaluacionPersonal) Then
                If Request.QueryString("ctf") = 1 Then
                    e.Row.Visible = True
                Else
                    ' Response.Write("<br>" & Session("id_per"))
                    ' Response.Write("<br>" & valorEvaluacionPersonal)
                    If Session("id_per") = valorEvaluacionPersonal Then
                        e.Row.Visible = True
                    Else
                        e.Row.Visible = False
                    End If
                End If
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
                    str = str & ", previa presentación de tu DNI. " & dt.Rows(0).Item("mensaje").ToString
                    Me.txtObservacion.Text = str
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
            Dim RptaEmail As Boolean = False
            Dim listaEmail As String = ""
            Me.HdTramite.Value = gvDatos.DataKeys.Item(e.NewEditIndex).Values("codigo_dta")
            If (TieneDetalleAtendido(Me.HdTramite.Value) = False) Then
                Page.RegisterStartupScript("CloseE", "<script>closeModal();</script>")
                ShowMessage("Su solicitud debe estar atendida", MessageType.Success)
                Exit Sub
            End If

            obj.AbrirConexion()
            obj.Ejecutar("TRL_EntregaTramite", Me.HdTramite.Value, Request.QueryString("ctf"), Request.QueryString("id"))
            obj.CerrarConexion()

            RptaEmail = EnviaCorreo(Me.HdTramite.Value, "E", 0)

            If RptaEmail Then
                ShowMessageEmail("<i class=\'glyphicon glyphicon-envelope\'></i> Se ha enviado correctamente el correo de evaluación del trámite", "success")
            Else
                ShowMessageEmail("No se ha podido enviar los correos de evaluación del trámite", "Error")
            End If

            listaEmail = divEmailErroneo.InnerHtml.ToString
            ShowMessageEmailDestino(listaEmail, "info")
            divEmailErroneo.InnerHtml = ""
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

    ''' <summary>
    ''' Modal para editar fecha y observacion
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Image1_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Me._FechaEmail.Value = "F"
        gvDatos_SelectedIndexChanged(sender, e)
        txtObservacionAlumno.Text = ""
        h4Fecha_Mail.InnerHtml = "<i class='glyphicon glyphicon-calendar'></i> ACTUALIZAR FECHA DE ENTREGA"
        divHeadFecha_Mail.Style.Item("color") = "White"
        divHeadFecha_Mail.Style.Item("background-color") = "#D9534F"
        divModalFecha.Style.Item("display") = "block"
        divModalMensaje.Style.Item("display") = "none"

        Page.RegisterStartupScript("Pop", "<script>openModalFecha();</script>")
    End Sub

    ''' <summary>
    ''' Modal para enviar Email
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Image2_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Me._FechaEmail.Value = "E"
        gvDatos_SelectedIndexChanged(sender, e)
        txtObservacionAlumno.Text = ""
        h4Fecha_Mail.InnerHtml = "<i class='glyphicon glyphicon-comment'></i> ENVIAR MENSAJE POR CORREO A ESTUDIANTE"
        divHeadFecha_Mail.Style.Item("color") = "White"
        divHeadFecha_Mail.Style.Item("background-color") = "#5bc0de"
        divModalFecha.Style.Item("display") = "none"
        divModalMensaje.Style.Item("display") = "block"

        Page.RegisterStartupScript("Pop", "<script>openModalFecha();</script>")
    End Sub

    ''' <summary>
    ''' Modal para ver datos del estudiante
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Image3_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        gvDatos_SelectedIndexChanged(sender, e)
        fnInformacionTramite2(CInt(Me.HdTramite.Value))

        Page.RegisterStartupScript("Pop", "<script>openModalEstudiante();</script>")
    End Sub



    Public Function FormatUrl(ByVal pm1 As Object, ByVal pm2 As Object) As String
        'Response.Write(pm1)
        'Response.Write(pm2)
        Return ""
    End Function

    Protected Sub btnEntregar_Click(ByVal sender As Object, ByVal e As System.EventArgs)

    End Sub

    Protected Sub ShowMessage(ByVal Message As String, ByVal type As MessageType)
        Page.RegisterStartupScript("Mensaje", "<script>ShowMessage('" & Message & "','" & type & "');</script>")
    End Sub

    Protected Sub ShowMessage2(ByVal Message As String, ByVal type As String)
        Page.RegisterStartupScript("Mensaje", "<script>ShowMessage2('" & Message & "','" & type & "');</script>")
    End Sub

    Protected Sub ShowMessageEmail(ByVal Message As String, ByVal type As String)
        Page.RegisterStartupScript("Mensaje2", "<script>ShowMessageEmail('" & Message & "','" & type & "');</script>")
    End Sub
    Protected Sub ShowMessageEmailDestino(ByVal Message As String, ByVal type As String)
        Page.RegisterStartupScript("Mensaje3", "<script>ShowMessageEmailDestino('" & Message & "','" & type & "');</script>")
    End Sub

    Protected Sub ShowMessageSMS(ByVal Message As String, ByVal type As String)
        Page.RegisterStartupScript("Mensaje4", "<script>ShowMessageSMS('" & Message & "','" & type & "');</script>")
    End Sub


    Protected Sub btnGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardar.Click
        Dim obj As New ClsConectarDatos
        Dim RptaEmail As Boolean = False
        Dim listaEmail As String = ""

        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        Try
            obj.AbrirConexion()
            obj.Ejecutar("TRL_AtiendeTramite", Me.HdTramite.Value, Request.QueryString("ctf"), Me.txtObservacion.Text, Request.QueryString("id"))
            obj.CerrarConexion()
            'Response.Write(Me.HdTramite.Value)

            RptaEmail = EnviaCorreoMensaje(CInt(Me.HdTramite.Value), "T", Me.txtObservacion.Text, 0)

            If RptaEmail Then
                ShowMessageEmail("<i class=\'glyphicon glyphicon-envelope\'></i> Se ha enviado correctamente el correo de evaluación del trámite", "success")
            Else

                ShowMessageEmail("No se ha podido enviar los correos de evaluación del trámite", "error")
            End If
            listaEmail = divEmailErroneo.InnerHtml.ToString
            ShowMessageEmailDestino(listaEmail, "info")
            divEmailErroneo.InnerHtml = ""

            EnviarSMS(txtcodalu.Value, CInt(Me.HdTramite.Value), 0, 0, "Finalizado")

            CargaDatos()

            Page.RegisterStartupScript("bloquea0", "<script type='text/javascript'>MascaraEsperaModal('0');</script>")
            ShowMessage2("<i class=\'glyphicon glyphicon-alert\'></i>  Se registró correctamente", "alert-success")

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
            Dim RptaEmail As Boolean = False
            If Me._FechaEmail.Value = "F" Then  'FECHA

                obj.Ejecutar("TRL_ActualizaFechaTramite", Me.HdTramite.Value, Me.txtFecha.Value, _
                             Me.txtObservacionAlumno.Text, Session("id_per"))
            ElseIf Me._FechaEmail.Value = "E" Then 'EMAIL

                obj.Ejecutar("TRL_ActualizaObservacionTramite", Me.HdTramite.Value, Me.txtFecha.Value, _
                             Me.txtObservacionAlumno.Text, Session("id_per"))
            End If
            obj.CerrarConexion()
            If Me._FechaEmail.Value = "F" Then 'EMAIL FECHA
                RptaEmail = EnviaCorreo(Me.HdTramite.Value, "F", 0)

                If RptaEmail Then
                    ShowMessageEmail("<i class=\'glyphicon glyphicon-envelope\'></i> Se ha enviado correctamente el correo de evaluación del trámite", "success")
                Else
                    ShowMessageEmail("No se ha podido enviar los correos de evaluación del trámite", "error")
                End If

            ElseIf Me._FechaEmail.Value = "E" Then 'EMAIL MENSAJE 
                RptaEmail = EnviaCorreo(Me.HdTramite.Value, "C", 0)
                If RptaEmail Then
                    ShowMessageEmail("<i class=\'glyphicon glyphicon-envelope\'></i> Se ha enviado correctamente el correo de evaluación del trámite", "success")
                Else
                    ShowMessageEmail("No se ha podido enviar los correos de evaluación del trámite", "error")
                End If
            End If


            CargaDatos()
            Page.RegisterStartupScript("bloquea0", "<script type='text/javascript'>MascaraEspera('0');</script>")
            ShowMessage("Se registró correctamente", MessageType.Success)

        Catch ex As Exception
            ShowMessage("Error: " & ex.Message.Replace("'", ""), MessageType.Error)
        End Try
    End Sub

    Private Function EnviaCorreo(ByVal dta As Integer, ByVal tipo As String, ByVal dft As Integer) As Boolean
        Dim cls As New ClsMail
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        Dim strMensaje As String = ""
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString

        Dim De, Asunto As String
        Dim EmailDestino As String = ""
        Dim EmailDestino2 As String = ""
        Dim EmailVarios As String = ""

        Dim rpta As Boolean = False
        Dim rptaEmail As String = ""

        Try
            obj.AbrirConexion()
            dt = obj.TraerDataTable("TRL_DatosAlumnoxDetalle", dta)
            obj.CerrarConexion()

            'If (dt.Rows.Count > 0) Then
            If dt.Rows(0).Item("eMail_Alu").ToString <> "" Then
                EmailDestino = dt.Rows(0).Item("eMail_Alu")

                If dt.Rows(0).Item("UserPrincipalName").ToString <> "" Then
                    EmailDestino2 = dt.Rows(0).Item("UserPrincipalName")
                    'EmailVarios = EmailDestino2			
                End If
            End If

			If ConfigurationManager.AppSettings("CorreoUsatActivo") = 1 Then
                EmailVarios = EmailDestino & ";" & EmailDestino2
            Else
                EmailVarios = "epena@usat.edu.pe;fatima.vasquez@usat.edu.pe"
            End If

            De = ""
            Asunto = ""

            'Correo de Fecha de Entrega
            If tipo = "F" Then
                De = "campusvirtual@usat.edu.pe"
                Asunto = "[" & dt.Rows(0).Item("glosaCorrelativo_trl") & "] " & "Fecha de Entrega Trámite"

                strMensaje = "Estimado(a) " & dt.Rows(0).Item("nombres_Alu") & " " & dt.Rows(0).Item("apellidoPat_Alu") & " " & dt.Rows(0).Item("apellidoMat_Alu") & ": <br/><br/>"
                strMensaje = strMensaje & "El trámite " & dt.Rows(0).Item("glosaCorrelativo_trl")
                strMensaje = strMensaje & " ha sido actualizada la fecha de entrega para el día "
                strMensaje = strMensaje & dt.Rows(0).Item("fechaFin_dta").ToString & ".<br/><br/>"
                strMensaje = strMensaje & "<em>" & dt.Rows(0).Item("observacionAlumno_dft").ToString & "</em>"
                ' cls.EnviarMail("campusvirtual@usat.edu.pe", "Campus Virtual", EmailVarios, "Fecha de Entrega Trámite", strMensaje, True, "", "")
                'rpta = cls.EnviarMailVariosV2(De, EmailVarios, Asunto, strMensaje, True)
                rptaEmail = cls.EnviarMailVariosV3(De, EmailVarios, Asunto, strMensaje, True)
            End If
            'Correo de Mensaje de Email
            If tipo = "C" Then
                De = "campusvirtual@usat.edu.pe"
                Asunto = "[" & dt.Rows(0).Item("glosaCorrelativo_trl") & "] " & "Mensaje de Trámite"

                strMensaje = "Estimado(a) " & dt.Rows(0).Item("nombres_Alu") & " " & dt.Rows(0).Item("apellidoPat_Alu") & " " & dt.Rows(0).Item("apellidoMat_Alu") & ": <br/><br/>"
                strMensaje = strMensaje & "El trámite " & dt.Rows(0).Item("glosaCorrelativo_trl")
                strMensaje = strMensaje & " ha enviado el siguiente mensaje:  <br/><br/>"
                strMensaje = strMensaje & "<em>" & dt.Rows(0).Item("observacionAlumno_dft").ToString & "</em>"
                ' cls.EnviarMail("campusvirtual@usat.edu.pe", "Campus Virtual", EmailVarios, "Fecha de Entrega Trámite", strMensaje, True, "", "")
               'rpta = cls.EnviarMailVariosV2(De, EmailVarios, Asunto, strMensaje, True)
                rptaEmail = cls.EnviarMailVariosV3(De, EmailVarios, Asunto, strMensaje, True)
            End If
            'Correo de Finaliza Tramite
            If tipo = "T" Then
                De = "campusvirtual@usat.edu.pe"
                Asunto = "[" & dt.Rows(0).Item("glosaCorrelativo_trl") & "] " & "Trámite Finalizado"

                strMensaje = "Estimado(a) " & dt.Rows(0).Item("nombres_Alu") & " " & dt.Rows(0).Item("apellidoPat_Alu") & " " & dt.Rows(0).Item("apellidoMat_Alu") & ": <br/><br/>"
                strMensaje = strMensaje & "El documento solicitado <b>" & dt.Rows(0).Item("descripcion_ctr").ToString.ToUpper() & "</b> ya se encuentra disponible.<br/><br/>"
                strMensaje = strMensaje & "Puedes recoger tu documento en: <br/>"
                strMensaje = strMensaje & "<em>" & dt.Rows(0).Item("ubicacion_ctr") & " - " & dt.Rows(0).Item("observacionAlumno_dft").ToString & "</em><br/>"
                'cls.EnviarMail("campusvirtual@usat.edu.pe", "Campus Virtual", EmailVarios, "Entrega Trámite", strMensaje, True, "", "")
                'rpta = cls.EnviarMailVariosV2(De, EmailVarios, Asunto, strMensaje, True)
                rptaEmail = cls.EnviarMailVariosV3(De, EmailVarios, "Trámite Finalizado", strMensaje, True)
            End If

            'Correo de Entrega Tramite
            If tipo = "E" Then
                De = "campusvirtual@usat.edu.pe"
                Asunto = "[" & dt.Rows(0).Item("glosaCorrelativo_trl") & "] " & "Entrega Trámite"

                strMensaje = "Estimado(a) " & dt.Rows(0).Item("nombres_Alu") & " " & dt.Rows(0).Item("apellidoPat_Alu") & " " & dt.Rows(0).Item("apellidoMat_Alu") & ": <br/><br/>"
                strMensaje = strMensaje & "El documento solicitado <b>" & dt.Rows(0).Item("descripcion_ctr").ToString.ToUpper() & "</b> ya ha sido entregado.<br/><br/>"
                'cls.EnviarMail("campusvirtual@usat.edu.pe", "Campus Virtual", EmailVarios, "Entrega Trámite", strMensaje, True, "", "")
                'rpta = cls.EnviarMailVariosV2(De, EmailVarios, Asunto, strMensaje, True)
                rptaEmail = cls.EnviarMailVariosV3(De, EmailVarios, Asunto, strMensaje, True)
            End If
            '  End If
            '  End If

            Dim codigoEmail As String = ""
            Dim msgEmail As String = ""

            codigoEmail = fnObtenerRespuestaEmail(0, rptaEmail)
            msgEmail = fnObtenerRespuestaEmail(1, rptaEmail)

            If codigoEmail = "1" Then
                rpta = True
            Else
                rpta = False
            End If

            RegistroBitacoraCorreo(De, EmailVarios, Asunto, strMensaje, rpta, dft, dta, codigoEmail, msgEmail)

            cls = Nothing
            obj = Nothing

            Return rpta

        Catch ex As Exception
            ShowMessage("Error: " & ex.Message.Replace("'", ""), MessageType.Error)
            Return False
        End Try
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="tipo">Indice 0: obtiene codigo, 1: obtiene mensaje </param>
    ''' <param name="respuesta">Cadena completa de respuesta de la funcion enviarcorreo</param>
    ''' <returns>1: se envio bien el email, >1: es el numero de error al enviar el correo</returns>
    ''' <returns>Mensaje de respuesta del servidor al enviar el email</returns>
    ''' <remarks></remarks>
    Private Function fnObtenerRespuestaEmail(ByVal tipo As Int16, ByVal respuesta As String) As String
        Try
            Dim Respuestas() As String
            Respuestas = Split(respuesta, ",")
            Return Respuestas(tipo).ToString


        Catch ex As Exception
            Return ""
        End Try
    End Function

    Private Function EnviaCorreoMensaje(ByVal dta As Integer, ByVal tipo As String, ByVal mensaje As String, ByVal dft As Integer) As Boolean
        Dim cls As New ClsMail
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        Dim strMensaje As String = ""
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString

        Dim De, Asunto As String
        Dim EmailDestino As String = ""
        Dim EmailDestino2 As String = ""
        Dim EmailVarios As String = ""
        Dim NroTramite As String = ""
        Dim rpta As Boolean = False
        Dim rptaEmail As String = ""

        Try
            obj.AbrirConexion()
            dt = obj.TraerDataTable("TRL_DatosAlumnoxDetalle", dta)
            obj.CerrarConexion()

            'If (dt.Rows.Count > 0) Then

            If dt.Rows(0).Item("eMail_Alu").ToString <> "" Then
                NroTramite = dt.Rows(0).Item("glosaCorrelativo_trl")
                Me.txtcodalu.Value = dt.Rows(0).Item("codigoalu")
                EmailDestino = dt.Rows(0).Item("eMail_Alu")
                If dt.Rows(0).Item("UserPrincipalName").ToString <> "" Then
                    EmailDestino2 = dt.Rows(0).Item("UserPrincipalName")
                End If
            End If

            If ConfigurationManager.AppSettings("CorreoUsatActivo") = 1 Then
                EmailVarios = EmailDestino & ";" & EmailDestino2
            Else
                EmailVarios = "epena@usat.edu.pe;fatima.vasquez@usat.edu.pe"
            End If

            De = ""
            Asunto = ""

            'Correo de Fecha de Entrega
            If tipo = "F" Then

                De = "campusvirtual@usat.edu.pe"
                Asunto = "[" & NroTramite & "] " & "Entrega Trámite"

                strMensaje = "Estimado(a) " & dt.Rows(0).Item("nombres_Alu") & " " & dt.Rows(0).Item("apellidoPat_Alu") & " " & dt.Rows(0).Item("apellidoMat_Alu") & ": <br/><br/>"
                strMensaje = strMensaje & "El trámite " & dt.Rows(0).Item("glosaCorrelativo_trl")
                strMensaje = strMensaje & " ha sido actualizada la fecha de entrega para el día "
                strMensaje = strMensaje & dt.Rows(0).Item("fechaFin_dta").ToString & ".<br/><br/>"
                strMensaje = strMensaje & "<em>" & mensaje.ToString & "</em>"
                'cls.EnviarMail("campusvirtual@usat.edu.pe", "Campus Virtual", EmailDestino, "Fecha de Entrega Trámite", strMensaje, True, "", "")
                'rpta = cls.EnviarMailVariosV2(De, EmailVarios, Asunto, strMensaje, True)
                rptaEmail = cls.EnviarMailVariosV3(De, EmailVarios, Asunto, strMensaje, True)


            End If

            'Correo de Finaliza Tramite
            If tipo = "T" Then
                De = "campusvirtual@usat.edu.pe"
                Asunto = "[" & NroTramite & "] " & "Entrega Trámite"

                strMensaje = "Estimado(a) " & dt.Rows(0).Item("nombres_Alu") & " " & dt.Rows(0).Item("apellidoPat_Alu") & " " & dt.Rows(0).Item("apellidoMat_Alu") & ": <br/><br/>"

                If dt.Rows(0).Item("codigo_tctr") = 3 Then
                    strMensaje = strMensaje & "El documento solicitado <b>" & dt.Rows(0).Item("descripcion_ctr").ToString.ToUpper() & "</b> ya se encuentra aprobado.<br/><br/>"
                Else
                    strMensaje = strMensaje & "El documento solicitado <b>" & dt.Rows(0).Item("descripcion_ctr").ToString.ToUpper() & "</b> ya se encuentra disponible.<br/><br/>"
                End If

                strMensaje = strMensaje & mensaje.ToString & " <br/>"
                strMensaje = strMensaje & "<em>" & dt.Rows(0).Item("ubicacion_ctr") & " - " & dt.Rows(0).Item("observacionAlumno_dft").ToString & "</em><br/>"
                'cls.EnviarMail("campusvirtual@usat.edu.pe", "Campus Virtual", EmailDestino, "Entrega Trámite", strMensaje, True, "", "")
                'rpta = cls.EnviarMailVariosV2(De, EmailVarios, Asunto, strMensaje, True)
                rptaEmail = cls.EnviarMailVariosV3(De, EmailVarios, Asunto, strMensaje, True)
            End If

            'Correo de Entrega Tramite
            If tipo = "E" Then
                De = "campusvirtual@usat.edu.pe"
                Asunto = "[" & NroTramite & "] " & "Entrega Trámite"

                strMensaje = "Estimado(a) " & dt.Rows(0).Item("nombres_Alu") & " " & dt.Rows(0).Item("apellidoPat_Alu") & " " & dt.Rows(0).Item("apellidoMat_Alu") & ": <br/><br/>"
                strMensaje = strMensaje & "El documento solicitado <b>" & dt.Rows(0).Item("descripcion_ctr").ToString.ToUpper() & "</b> ya ha sido entregado.<br/><br/>"
                'cls.EnviarMail("campusvirtual@usat.edu.pe", "Campus Virtual", EmailDestino, "Entrega Trámite", strMensaje, True, "", "")
                'rpta = cls.EnviarMailVariosV2(De, EmailVarios, Asunto, strMensaje, True)
                rptaEmail = cls.EnviarMailVariosV3(De, EmailVarios, Asunto, strMensaje, True)
            End If
            '  End If
            '  End If

            Dim codigoEmail As String = ""
            Dim msgEmail As String = ""

            codigoEmail = fnObtenerRespuestaEmail(0, rptaEmail)
            msgEmail = fnObtenerRespuestaEmail(1, rptaEmail)

            If codigoEmail = "1" Then
                rpta = True
            Else
                rpta = False
            End If

            RegistroBitacoraCorreo(De, EmailVarios, Asunto, strMensaje, rpta, dft, dta, codigoEmail, msgEmail)

            cls = Nothing
            obj = Nothing
            Return rpta
        Catch ex As Exception
            ShowMessage("Error: " & ex.Message.Replace("'", ""), MessageType.Error)
            Return rpta
        End Try
    End Function

    Private Function EnviaCorreoAprobacion(ByVal dta As Integer, ByVal tipo As String, ByVal dft As Integer) As Boolean



        Dim cls As New ClsMail
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        Dim strMensaje As String = ""
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString

        Dim De, Asunto As String
        Dim EmailDestino As String = ""
        Dim EmailDestino2 As String = ""
        Dim EmailVarios As String = ""

        Dim EmailDestino_tipo As String = ""
        Dim EmailDestino2_tipo As String = ""
        Dim EmailVarios_tipo As String = ""

        Dim rpta As Boolean = False
        Dim rptaEmail As String = ""

        Dim tfu As String = Me.hdTfu.value
        Dim tfudes As String = Me.hdTfuDesc.value
        Dim tfuorden As String = Me.hdOrden.value
        Dim Nombrectr As String = Me.hdNombrectr.value

        Dim alumno, escuela As String


        Try
            obj.AbrirConexion()
            dt = obj.TraerDataTable("TRL_DatosAlumnoxDetalle", dta)
            obj.CerrarConexion()

            alumno = dt.Rows(0).Item("apellidoPat_Alu").ToString & " " & dt.Rows(0).Item("apellidoMat_Alu").ToString & " " & dt.Rows(0).Item("nombres_Alu").ToString
            escuela = dt.Rows(0).Item("nombre_Cpf").ToString

            If dt.Rows(0).Item("eMail_Alu").ToString <> "" Then
                EmailDestino = dt.Rows(0).Item("eMail_Alu")

                If dt.Rows(0).Item("UserPrincipalName").ToString <> "" Then
                    EmailDestino2 = dt.Rows(0).Item("UserPrincipalName")

                End If
            End If

            If ConfigurationManager.AppSettings("CorreoUsatActivo") = 1 Then
                EmailVarios = EmailDestino & ";" & EmailDestino2
            Else
                EmailVarios = "epena@usat.edu.pe;fatima.vasquez@usat.edu.pe"
            End If
            'Correo de evaluacion Rechazada


            De = ""
            Asunto = ""
            'Response.Write(tipo)
            If tipo = "R" Then

                '----------------------------------------------------------------------------------------------------
                ''De = "campusvirtual@usat.edu.pe"
                ''Asunto = "[" & Me.nroTramite.Value & "] " & "Trámite Virtual Estudiante: Estado Aprobación"

                ''strMensaje = "Estimado(a) <b>" & dt.Rows(0).Item("nombres_Alu") & " " & dt.Rows(0).Item("apellidoPat_Alu") & " " & dt.Rows(0).Item("apellidoMat_Alu") & "</b>: <br/><br/>"
                ''strMensaje = strMensaje & "El proceso solicitado <b>" & dt.Rows(0).Item("descripcion_ctr").ToString.ToUpper() & "</b> del Trámite Virtual <b>N°: " & dt.Rows(0).Item("glosaCorrelativo_trl").ToString & ", </b> ha sido rechazado.<br/><br/>"
                ''strMensaje = strMensaje & "<br><br>"
                ''strMensaje = strMensaje & txtmensajeemail.Text.ToString & " <br/>"
                ''strMensaje = strMensaje & "<br>" & dt.Rows(0).Item("ubicacion_ctr").ToString.ToUpper()
                '----------------------------------------------------------------------------------------------------

                'cls.EnviarMail("campusvirtual@usat.edu.pe", "Campus Virtual", EmailDestino, "TRÁMITE VIRTUAL: " & dt.Rows(0).Item("descripcion_ctr").ToString.ToUpper(), strMensaje, True, "", "")

                'rpta = cls.EnviarMailVariosV2(De, EmailVarios, Asunto, strMensaje, True)
                'rptaEmail = cls.EnviarMailVariosV3(De, EmailVarios, Asunto, strMensaje, True)

                Dim codigo_envio As Integer = ClsComunicacionInstitucional.ObtenerCodigoEnvio(CInt(Me.Request.QueryString("id")), tfu, 64)
                rpta = ClsComunicacionInstitucional.EnviarNotificacionEmail(codigo_envio, "TRVE", "RTRL", 1, CInt(Me.Request.QueryString("id")), "codigo_alu", dt.Rows(0).Item("codigoalu"), 64, EmailVarios, "serviciosti@usat.edu.pe", "", "", escuela, alumno, tfuorden, tfudes, Nombrectr)



            ElseIf tipo = "A" Then
                '----------------------------------------------------------------------------------------------------
                ''De = "campusvirtual@usat.edu.pe"
                ''Asunto = "[" & Me.nroTramite.Value & "] " & "Trámite Virtual Estudiante: Estado Aprobación"

                ''strMensaje = "Estimado(a) <b>" & dt.Rows(0).Item("nombres_Alu") & " " & dt.Rows(0).Item("apellidoPat_Alu") & " " & dt.Rows(0).Item("apellidoMat_Alu") & "</b>: <br/><br/>"
                ''strMensaje = strMensaje & "El proceso solicitado <b>" & dt.Rows(0).Item("descripcion_ctr").ToString.ToUpper() & "</b> del Trámite Virtual <b>N°: " & dt.Rows(0).Item("glosaCorrelativo_trl").ToString & ", </b> ha sido aceptado.<br/><br/>"
                ''strMensaje = strMensaje & "<br><br>"
                ''strMensaje = strMensaje & txtmensajeemail.Text.ToString & " <br/>"
                ''strMensaje = strMensaje & "<br>" & dt.Rows(0).Item("ubicacion_ctr").ToString.ToUpper()
                '----------------------------------------------------------------------------------------------------

                'cls.EnviarMail("campusvirtual@usat.edu.pe", "Campus Virtual", EmailDestino, "TRÁMITE VIRTUAL: " & dt.Rows(0).Item("descripcion_ctr").ToString.ToUpper(), strMensaje, True, "", "")
                'rpta = cls.EnviarMailVariosV2(De, EmailVarios, Asunto, strMensaje, True)
                'rptaEmail = cls.EnviarMailVariosV3(De, EmailVarios, Asunto, strMensaje, True)


                Dim codigo_envio As Integer = ClsComunicacionInstitucional.ObtenerCodigoEnvio(CInt(Me.Request.QueryString("id")), tfu, 64)
                rpta = ClsComunicacionInstitucional.EnviarNotificacionEmail(codigo_envio, "TRVE", "ATRL", 1, CInt(Me.Request.QueryString("id")), "codigo_alu", dt.Rows(0).Item("codigoalu"), 64, EmailVarios, "serviciosti@usat.edu.pe", "", "", escuela, alumno, tfuorden, tfudes, Nombrectr)

            ElseIf tipo = "O" Then
                '----------------------------------------------------------------------------------------------------
                ''De = "campusvirtual@usat.edu.pe"
                ''Asunto = "[" & Me.nroTramite.Value & "] " & "Trámite Virtual Estudiante: Estado Observado"

                ''strMensaje = "Estimado(a) <b>" & dt.Rows(0).Item("nombres_Alu") & " " & dt.Rows(0).Item("apellidoPat_Alu") & " " & dt.Rows(0).Item("apellidoMat_Alu") & "</b>: <br/><br/>"
                ''strMensaje = strMensaje & "El proceso solicitado <b>" & dt.Rows(0).Item("descripcion_ctr").ToString.ToUpper() & "</b> del Trámite Virtual <b>N°: " & dt.Rows(0).Item("glosaCorrelativo_trl").ToString & ", </b> ha sido observado.<br/><br/>"
                ''strMensaje = strMensaje & "<br><br>"
                ''strMensaje = strMensaje & txtobservacionaprobacion.Text.ToString & " <br/>"
                '----------------------------------------------------------------------------------------------------

                'strMensaje = strMensaje & "<br>" & dt.Rows(0).Item("ubicacion_ctr").ToString.ToUpper()
                'cls.EnviarMail("campusvirtual@usat.edu.pe", "Campus Virtual", EmailDestino, "TRÁMITE VIRTUAL: " & dt.Rows(0).Item("descripcion_ctr").ToString.ToUpper(), strMensaje, True, "", "")
                'rpta = cls.EnviarMailVariosV2(De, EmailVarios, Asunto, strMensaje, True)
                'rptaEmail = cls.EnviarMailVariosV3(De, EmailVarios, Asunto, strMensaje, True)

                Dim codigo_envio As Integer = ClsComunicacionInstitucional.ObtenerCodigoEnvio(CInt(Me.Request.QueryString("id")), tfu, 64)
                rpta = ClsComunicacionInstitucional.EnviarNotificacionEmail(codigo_envio, "TRVE", "OTRL", 1, CInt(Me.Request.QueryString("id")), "codigo_alu", dt.Rows(0).Item("codigoalu"), 64, EmailVarios, "serviciosti@usat.edu.pe", "", "", escuela, alumno, tfuorden, tfudes, Nombrectr)


            End If
            '  End If
            '  End If

            Dim codigoEmail As String = ""
            Dim msgEmail As String = ""

            codigoEmail = fnObtenerRespuestaEmail(0, rptaEmail)
            msgEmail = fnObtenerRespuestaEmail(1, rptaEmail)

            'If codigoEmail = "1" Then
            '    rpta = True
            'Else
            '    rpta = False
            'End If

            ' Response.Write("rpta")
            'Response.Write(rpta)
            'RegistroBitacoraCorreo(De, EmailVarios, Asunto, strMensaje, rpta, dft, dta, codigoEmail, msgEmail)

            cls = Nothing
            obj = Nothing
            Return rpta
        Catch ex As Exception
            ShowMessage("Error: " & ex.Message.Replace("'", ""), MessageType.Error)
            Return False

        End Try
    End Function

  

    Private Function EnviarCorreoPlanEstudio(ByVal dft As Integer) As Boolean
        Dim cls As New ClsMail
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        Dim strMensaje As String = ""

        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString

        Dim De, Asunto As String
        Dim EmailDestino As String = ""
        Dim EmailDestino2 As String = ""
        Dim EmailVarios As String = ""
        Dim rpta As Boolean = False
        Dim rptaEmail As String = ""
        Dim dta As Integer = 0

        Try
            obj.AbrirConexion()
            dt = obj.TraerDataTable("TRL_DatosPlanEstudioxDetalle", dft)
            obj.CerrarConexion()

            If dt.Rows(0).Item("eMail_Alu").ToString <> "" Then
                EmailDestino = dt.Rows(0).Item("eMail_Alu")
                If dt.Rows(0).Item("UserPrincipalName").ToString <> "" Then
                    EmailDestino2 = dt.Rows(0).Item("UserPrincipalName")
                End If
            End If

            If ConfigurationManager.AppSettings("CorreoUsatActivo") = 1 Then
                EmailVarios = EmailDestino & ";" & EmailDestino2
            Else
                EmailVarios = "epena@usat.edu.pe;fatima.vasquez@usat.edu.pe"
            End If

            dta = CInt(Me.HdTramite.Value.ToString)
            De = "campusvirtual@usat.edu.pe"
            Asunto = "[" & Me.nroTramite.Value & "] " & "Trámite Virtual Estudiante: Plan de Estudios"

            strMensaje = "Estimado(a) <b>" & dt.Rows(0).Item("nombres_Alu") & " " & dt.Rows(0).Item("apellidoPat_Alu") & " " & dt.Rows(0).Item("apellidoMat_Alu") & "</b>: <br/><br/>"
            strMensaje = strMensaje & "El proceso solicitado <b>" & dt.Rows(0).Item("descripcion_ctr").ToString.ToUpper() & "</b> del Trámite Virtual <b>N°: " & dt.Rows(0).Item("glosaCorrelativo_trl").ToString & ", </b> ha actualizado su Plan de Estudios.<br/><br/>"
            strMensaje = strMensaje & "<br><br>"
            strMensaje = strMensaje & "Plan de Estudios: <b>" & dt.Rows(0).Item("descripcion_Pes").ToString.ToUpper() & "</b><br><br>"
            strMensaje = strMensaje & "Verificar su plan de estudios en el campus virtual"

            'rpta = cls.EnviarMailVariosV2(De, EmailVarios, Asunto, strMensaje, True)
            rpta = cls.EnviarMailVariosV3(De, EmailVarios, Asunto, strMensaje, True)



            Dim codigoEmail As String = ""
            Dim msgEmail As String = ""

            codigoEmail = fnObtenerRespuestaEmail(0, rptaEmail)
            msgEmail = fnObtenerRespuestaEmail(1, rptaEmail)

            If codigoEmail = "1" Then
                rpta = True
            Else
                rpta = False
            End If

            RegistroBitacoraCorreo(De, EmailVarios, Asunto, strMensaje, rpta, dft, dta, codigoEmail, msgEmail)

            cls = Nothing
            obj = Nothing
            Return rpta
        Catch ex As Exception
            ShowMessage("Error: " & ex.Message.Replace("'", ""), MessageType.Error)
            Return rpta
        End Try
    End Function

    Private Function EnviarCorreoGyT(ByVal idta As Integer, ByVal dft As Integer) As Boolean
        Dim cls As New ClsMail
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        Dim strMensaje As String = ""

        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString

        Dim De, Asunto As String
        Dim EmailDestino As String = ""
        Dim EmailDestino2 As String = ""
        Dim EmailVarios As String = ""
        Dim EmailGYT As String = ""
        Dim GradoAcademico As String = ""

        Dim rpta As Boolean = False
        Dim rptaEmail As String = ""

        Try
            obj.AbrirConexion()
            dt = obj.TraerDataTable("TRL_DatosGyTxDetalle", idta)
            obj.CerrarConexion()

            If dt.Rows(0).Item("eMail_Alu").ToString <> "" Then
                EmailDestino = dt.Rows(0).Item("eMail_Alu")
                If dt.Rows(0).Item("UserPrincipalName").ToString <> "" Then
                    EmailDestino2 = dt.Rows(0).Item("UserPrincipalName")
                End If
            End If

			If ConfigurationManager.AppSettings("CorreoUsatActivo") = 1 Then
				EmailGYT = "pdiaz@usat.edu.pe;vtaboada@usat.edu.pe"
                EmailVarios = EmailDestino & ";" & EmailDestino2 & ";" & EmailGYT
            Else
                EmailVarios = "epena@usat.edu.pe;fatima.vasquez@usat.edu.pe"
            End If

            De = "campusvirtual@usat.edu.pe"
            Asunto = "[" & Me.nroTramite.Value & "] " & "Trámite Virtual Estudiante: " & dt.Rows(0).Item("descripcion_ctr").ToString

            If dt.Rows(0).Item("descripcion_ctr").ToString.Contains("BACHILLER") Then
                GradoAcademico = "Grado Académico de Bachiller"
			ElseIf dt.Rows(0).Item("descripcion_ctr").ToString.Contains("MAESTRÍA") Or dt.Rows(0).Item("descripcion_ctr").ToString.Contains("TITULO") Then
                GradoAcademico = "Grado Académico de Maestro"
			ElseIf dt.Rows(0).Item("descripcion_ctr").ToString.Contains("DOCTOR") Or dt.Rows(0).Item("descripcion_ctr").ToString.Contains("TITULO") Then
                GradoAcademico = "Grado Académico de Doctor"
            ElseIf dt.Rows(0).Item("descripcion_ctr").ToString.Contains("TÍTULO") Or dt.Rows(0).Item("descripcion_ctr").ToString.Contains("TITULO") Then
                GradoAcademico = "Título Profesional"
            End If

            strMensaje = "<p style='text-align:justify'>"
            strMensaje = strMensaje & "Estimado(a) <b>" & dt.Rows(0).Item("nombres_Alu") & " " & dt.Rows(0).Item("apellidoPat_Alu") & " " & dt.Rows(0).Item("apellidoMat_Alu") & "</b>: <br/><br/>"

            strMensaje = strMensaje & "Se les invita a recoger su " & GradoAcademico & " en la Oficina de Grados y Títulos (2do. Piso del edificio de Gobierno), <b> portando su DNI original </b> (Documento Nacional de  Identidad) <b>requisito indispensable</b>. El horario de oficina es de lunes a viernes:"
            strMensaje = strMensaje & "</p>"


            strMensaje = strMensaje & "<p>"
            strMensaje = strMensaje & "<center>"
            strMensaje = strMensaje & "<table style='border:1px solid black; width:50%'>"
            strMensaje = strMensaje & "<tr>"
            strMensaje = strMensaje & "<td style='width:50%'>"
            strMensaje = strMensaje & "Mañana"
            strMensaje = strMensaje & "</td>"
            strMensaje = strMensaje & "<td style='width:50%'>"
            strMensaje = strMensaje & "8:30 am a 1:00 pm"
            strMensaje = strMensaje & "</td>"
            strMensaje = strMensaje & "</tr>"

            strMensaje = strMensaje & "<tr>"
            strMensaje = strMensaje & "<td style='width:50%'>"
            strMensaje = strMensaje & "Tarde"
            strMensaje = strMensaje & "</td>"
            strMensaje = strMensaje & "<td style='width:50%'>"
            strMensaje = strMensaje & "3:00 pm a 4:00 pm"
            strMensaje = strMensaje & "</td>"
            strMensaje = strMensaje & "</tr>"
            strMensaje = strMensaje & "</table>"
            strMensaje = strMensaje & "</center>"
            strMensaje = strMensaje & "</p>"

            strMensaje = strMensaje & "<p style='text-align:justify'>"
            strMensaje = strMensaje & "*<span style='font-weight:bold; text-decoration: underline;'>De Enero a Marzo solo turno mañana</span>"
            strMensaje = strMensaje & "</p>"

            strMensaje = strMensaje & "<b>A TENER EN CUENTA:</b>"
            strMensaje = strMensaje & "<p style='text-align:justify'>"

            strMensaje = strMensaje & "<ul  style='text-align:justify'>"
            strMensaje = strMensaje & "<li>"
            strMensaje = strMensaje & "Indicar en portería que se acercarán a esta oficina a recabar su diploma, por tanto necesitarán su DNI para presentarlo en la Oficina de Grados y Títulos."
            strMensaje = strMensaje & "</li>"
            strMensaje = strMensaje & "</ul>"
            strMensaje = strMensaje & "<ul  style='text-align:justify'>"
            strMensaje = strMensaje & "<li>"
            strMensaje = strMensaje & "La entrega del diploma es personal.<br>"
            strMensaje = strMensaje & "*<b>SOLO para las personas que NO LES SEA POSIBLE recoger su diploma personalmente,<span style='text-decoration: underline;'> descargar el formato de CARTA PODER en el campus</span></b> (Operaciones en línea>>Trámites Virtuales>>Requisitos para trámites) para ser llenada y firmada por su persona, también debe firmarse por el notario del lugar donde actualmente residen (Si reside en el exterior, el documento adjunto lo debe hacer visar por el Consulado del país donde Ud. actualmente reside). La persona que Ud. designe para recoger su diploma deberá presentarse en esta oficina portando la carta <b>original</b> y DNI para la identificación respectiva."
            strMensaje = strMensaje & "</li>"
            strMensaje = strMensaje & "</ul>"
            strMensaje = strMensaje & "<ul  style='text-align:justify'>"
            strMensaje = strMensaje & "<li>"
            strMensaje = strMensaje & "Para visualizar la inscripción de Diploma en la página de SUNEDU, el tiempo establecido es de 55 días hábiles a partir del día siguiente de haberse efectuadola Sesión del Consejo Universitario. (Incluye los días que SUNEDU toma para subir la información)."
            strMensaje = strMensaje & "</li>"
            strMensaje = strMensaje & "</ul>"

            strMensaje = strMensaje & "Gracias,<br>"
            strMensaje = strMensaje & "Oficina de Grados y Títulos"
            strMensaje = strMensaje & "</p>"

            'rpta = cls.EnviarMailVariosV2(De, EmailVarios, Asunto.ToUpper, strMensaje, True)
            rptaEmail = cls.EnviarMailVariosV3(De, EmailVarios, Asunto.ToUpper, strMensaje, True)

            Dim codigoEmail As String = ""
            Dim msgEmail As String = ""

            codigoEmail = fnObtenerRespuestaEmail(0, rptaEmail)
            msgEmail = fnObtenerRespuestaEmail(1, rptaEmail)

            If codigoEmail = "1" Then
                rpta = True
            Else
                rpta = False
            End If

            RegistroBitacoraCorreo(De, EmailVarios, Asunto, strMensaje, rpta, dft, idta, codigoEmail, msgEmail)

            Return rpta
        Catch ex As Exception
            ShowMessage("Error: " & ex.Message.Replace("'", ""), MessageType.Error)
            Return rpta
        End Try
    End Function

    Private Function EnviarCorreoPrecioCredito(ByVal dft As Integer) As Boolean
        Dim cls As New ClsMail
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        Dim strMensaje As String = ""

        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        Dim De, Asunto As String
        Dim EmailDestino As String = ""
        Dim EmailDestino2 As String = ""
        Dim EmailVarios As String = ""
        Dim rpta As Boolean = False
        Dim rptaEmail As String = ""
        Dim dta As Integer = 0

        Try
            obj.AbrirConexion()
            dt = obj.TraerDataTable("TRL_DatosPrecioCreditoxDetalle", dft)
            obj.CerrarConexion()

            If dt.Rows(0).Item("eMail_Alu").ToString <> "" Then
                EmailDestino = dt.Rows(0).Item("eMail_Alu")
                If dt.Rows(0).Item("UserPrincipalName").ToString <> "" Then
                    EmailDestino2 = dt.Rows(0).Item("UserPrincipalName")
                End If
            End If

            If ConfigurationManager.AppSettings("CorreoUsatActivo") = 1 Then
                EmailVarios = EmailDestino & ";" & EmailDestino2 
            Else
                EmailVarios = "epena@usat.edu.pe;fatima.vasquez@usat.edu.pe"
            End If

            De = "campusvirtual@usat.edu.pe"
            Asunto = "[" & Me.nroTramite.Value & "] " & "Trámite Virtual Estudiante: Precio Crédito"
            dta = CInt(Me.HdTramite.Value)


            strMensaje = "Estimado(a) <b>" & dt.Rows(0).Item("nombres_Alu") & " " & dt.Rows(0).Item("apellidoPat_Alu") & " " & dt.Rows(0).Item("apellidoMat_Alu") & "</b>: <br/><br/>"
            strMensaje = strMensaje & "El proceso solicitado <b>" & dt.Rows(0).Item("descripcion_ctr").ToString.ToUpper() & "</b> del Trámite Virtual <b>N°: " & dt.Rows(0).Item("glosaCorrelativo_trl").ToString & ", </b> ha actualizado su Precio Crédito.<br/><br/>"
            strMensaje = strMensaje & "<br><br>"

            'strMensaje = strMensaje & "Precio Crédito: <b>" & dt.Rows(0).Item("preciocred").ToString.ToUpper() & "</b>"

            'rpta = cls.EnviarMailVariosV2(De, EmailVarios, Asunto, strMensaje, True)
            rptaEmail = cls.EnviarMailVariosV3(De, EmailVarios, Asunto, strMensaje, True)

            Dim codigoEmail As String = ""
            Dim msgEmail As String = ""

            codigoEmail = fnObtenerRespuestaEmail(0, rptaEmail)
            msgEmail = fnObtenerRespuestaEmail(1, rptaEmail)

            If codigoEmail = "1" Then
                rpta = True
            Else
                rpta = False
            End If

            RegistroBitacoraCorreo(De, EmailVarios, Asunto, strMensaje, rpta, dft, dta, codigoEmail, msgEmail)

            cls = Nothing
            obj = Nothing
            Return rpta

        Catch ex As Exception
            ShowMessage("Error: " & ex.Message.Replace("'", ""), MessageType.Error)
            Return rpta
        End Try
    End Function

    Private Sub RegistroBitacoraCorreo(ByVal De As String, ByVal EmailVarios As String, ByVal asunto As String, ByVal mensaje As String, ByVal estadoenvio As Boolean, ByVal codigo_dft As Integer, ByVal codigo_dta As Integer, ByVal codenvioEmail As Integer, ByVal msgenvioEmail As String)
        Dim emailLista As String = ""
        Dim emailListaEstado As String = ""
        Dim emailListaTipo As String = ""
        Dim emailAlert As String = ""


        Dim Destinos() As String

        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString

        Try
            Me.divEmailErroneo.InnerHtml = ""

            Destinos = Split(EmailVarios, ";")

            For i As Integer = 0 To Destinos.Length - 1
                emailLista = emailLista & Trim(Destinos(i)) & ","

                If Trim(Destinos(i)) <> "" Then
                    If fnValidarMail(Trim(Destinos(i))) Or fnValidarMail2(Trim(Destinos(i))) Then
                        emailListaEstado = emailListaEstado & "1,"
                        emailAlert = emailAlert & "<i class=\'glyphicon glyphicon-user\'></i> " & Trim(Destinos(i)) & " <i class=\'glyphicon glyphicon-thumbs-up\' style=\'color: #3c763d\'></i> <br>"
                    Else
                        emailListaEstado = emailListaEstado & "0,"
                        emailAlert = emailAlert & "<i class=\'glyphicon glyphicon-user\'></i> " & Trim(Destinos(i)) & " <i class=\'glyphicon glyphicon-thumbs-down\' style=\'color: #d9534f\'></i> <br>"
                    End If
                End If

            Next

            Me.divEmailErroneo.InnerHtml = emailAlert.ToString

            obj.AbrirConexion()
            obj.Ejecutar("TRL_TramiteAlumnoEnvioMailRegistro", "1", 0, codigo_dft, codigo_dta, De, asunto, mensaje, estadoenvio, CInt(Session("id_per").ToString()), emailLista, emailListaEstado, codenvioEmail, msgenvioEmail)
            obj.CerrarConexion()
            obj = Nothing

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub EnviarSMS(ByVal codigo_Alu As Integer, ByVal codigo_dta As Integer, ByVal codigo_dft As Integer, ByVal mensaje As String, ByVal tipo As String)
        Try
            'Dim obj As New ClsConectarDatos
            'Dim dt As New Data.DataTable
            'obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            'obj.AbrirConexion()
            'dt = obj.TraerDataTable("TRL_EnvioMsjeTextoTramiteAlumno", codigo_Alu, "", codigo_dta, codigo_dft, CInt(Session("id_per").ToString), tipo)

            'If dt.Rows.Count > 0 Then
                'If dt.Rows(0).Item("rpta") = 1 Then
                    'ShowMessageSMS("<i class=\'glyphicon glyphicon-phone\'></i> " & dt.Rows(0).Item("msg").ToString, "success")
                'Else
                    'ShowMessageSMS("<i class=\'glyphicon glyphicon-phone\'></i> " & dt.Rows(0).Item("msg").ToString, "error")
                'End If
            'End If

            'obj.CerrarConexion()
            'obj = Nothing
            'dt = Nothing
        Catch ex As Exception

        End Try

    End Sub

    Protected Sub gvDatos_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvDatos.SelectedIndexChanged
        Dim lnk As New LinkButton
        Dim dt, dtH As New Data.DataTable
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        Try
            lnk = TryCast(sender, LinkButton)
            Me.HdTramite.Value = lnk.ToolTip

            Me.gvHistorialFecha.DataSource = Nothing
            Me.gvHistorialFecha.DataBind()

            Me.gvHistorialEmail.DataSource = Nothing
            Me.gvHistorialEmail.DataBind()
            'Carga Datos del detalle del tramite
            obj.AbrirConexion()
            dt = obj.TraerDataTable("TRL_RetornaDetalleTramite", 0, Me.HdTramite.Value)
            If Me._FechaEmail.Value = "F" Then

                dtH = obj.TraerDataTable("TRL_MuestraHistorialFechasEmail", "F", Me.HdTramite.Value)
            ElseIf Me._FechaEmail.Value = "E" Then

                dtH = obj.TraerDataTable("TRL_MuestraHistorialFechasEmail", "E", Me.HdTramite.Value)
            End If

            obj.CerrarConexion()

            Dim i As Integer = 0


            If Me._FechaEmail.Value = "F" Then
                Me.gvHistorialFecha.DataSource = dtH
                Me.gvHistorialFecha.DataBind()
                Me.gvHistorialEmail.Visible = False
                Me.gvHistorialFecha.Visible = True

            ElseIf Me._FechaEmail.Value = "E" Then
                Me.gvHistorialEmail.DataSource = dtH
                Me.gvHistorialEmail.DataBind()
                Me.gvHistorialFecha.Visible = False
                Me.gvHistorialEmail.Visible = True


            End If


            If (dt.Rows.Count > 0) Then
                If (dt.Rows(0).Item("fechaFin_dta").ToString() <> "") Then
                    Me.txtFecha.Value = Format(CDate(dt.Rows(0).Item("fechaFin_dta")), "dd/MM/yyyy")
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
            Dim listaEmail As String = ""

            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()

            idta = CInt(Desencriptar(hddtareq.Value.ToString))
            'Response.Write(idta & "<br>")
            Dim _hddtareq As String = ""
            Dim ctf As String = ""
            Dim per As Integer = 0

            Dim tieneEntrega As Integer

            Dim mensajereq As String = ""
            Dim codigo_dft As Integer = 0

            Dim aReq As Boolean = False
            Dim RptaEmail As Boolean = False

            ctf = Me.Request.QueryString("ctf")
            per = CInt(Me.Request.QueryString("id"))

            'Response.Write(fnValidarCheckRequisito)

            If fnValidarCheckRequisitoFuncion() Then
                If ctf = Session("req_tfu").ToString Or ctf = 1 Then
                    If hddtareq.Value <> "" Then
                        ' lblResultado = obj.Ejecutar("GrupoAvisoDetalleIAE", "E", 0, idg, 0)
                        'Response.Write("hddtareq:" & hddtareq.Value.ToString & "---" & "  NUM REQ: " & Me.gvRequisitos.Rows.Count.ToString & "<br>")
                        Dim Fila As GridViewRow
                        For i As Integer = 0 To Me.gvRequisitos.Rows.Count - 1

                            Fila = Me.gvRequisitos.Rows(i)

                            Dim valor As Boolean = CType(Fila.FindControl("chkElegir"), CheckBox).Checked

                            If (valor = True And CType(Fila.FindControl("chkElegir"), CheckBox).Enabled = True) Then
                                'Response.Write("*5<br>----")
                                'Response.Write("*6Tiene entrega: " & Session("tieneEntrega").ToString & "<br>")
                                tieneEntrega = Session("tieneEntrega") ' CInt(Me.gvFlujoTramite.DataKeys(i).Values("tieneEntrega").ToString)
                                'Response.Write("<br>*7Tiene entrega: " & tieneEntrega.ToString & "<br>")
                                codigo_dft = CInt(Me.gvRequisitos.DataKeys(i).Values("codigo_dft").ToString)
                                lblResultado = obj.Ejecutar("TRL_TramiteRequisito_Registrar", "A", idta, CInt(Me.gvRequisitos.DataKeys(i).Values("codigo_dre").ToString), codigo_dft, 1, "F", per)
                                'lblResultado = True
                                If lblResultado Then
                                    aReq = True

                                End If
                            End If
                        Next

                        If aReq Then
                            If tieneEntrega = 0 Then
                                RptaEmail = EnviaCorreo(idta, "E", codigo_dft)
                                Session("tieneEntrega") = 1

                                If RptaEmail Then

                                    ShowMessageEmail("<i class=\'glyphicon glyphicon-envelope\'></i> Se ha enviado correctamente el correo de evaluación del trámite", "success")
                                Else

                                    ShowMessageEmail("No se ha podido enviar los correos de evaluación del trámite", "error")
                                End If
                                listaEmail = divEmailErroneo.InnerHtml.ToString
                                ShowMessageEmailDestino(listaEmail, "info")
                                divEmailErroneo.InnerHtml = ""
                            End If

                            If fnTieneEmailRespuesta("A") Then
                                'Response.Write("ENIVAR EMAIL APROBACION<br>")
                                RptaEmail = EnviaCorreoAprobacion(idta, "A", codigo_dft)

                                If RptaEmail Then

                                    ShowMessageEmail("<i class=\'glyphicon glyphicon-envelope\'></i> Se ha enviado correctamente el correo de evaluación del trámite", "success")
                                Else

                                    ShowMessageEmail("No se ha podido enviar los correos de evaluación del trámite", "error")
                                End If
                                listaEmail = divEmailErroneo.InnerHtml.ToString
                                ShowMessageEmailDestino(listaEmail, "info")
                                divEmailErroneo.InnerHtml = ""
                            End If

                            If _tEmail_send.Text = "1" Then

                                mensajereq = "<hr><center><p style='text-align: center'><h3>" & txtmensajemailreqcab.Text & "</h3></p></center> "
                                mensajereq = mensajereq & "<p style='text-align: center'>" & txtmensajeemailreq.Text & "</p>"
                                mensajereq = mensajereq & "<p style='text-align: center;font-weight: bold;'>" & txtmensajemailreqpie.Text & "</p><hr><br>"

                                'mensajereq = Me.txtmensajeemailreq.Text

                                RptaEmail = EnviaCorreoMensaje(idta, "T", mensajereq.ToString, codigo_dft)


                                If RptaEmail Then
                                    _tEmail_send.Text = ""
                                    _tEmail_config.Text = ""
                                    _tEmail_cabecera.Text = ""
                                    _tEmail_cuerpo.Text = ""
                                    _tEmail_pie.Text = ""

                                    ShowMessageEmail("<i class=\'glyphicon glyphicon-envelope\'></i> Se ha enviado correctamente el correo de evaluación del trámite", "success")
                                Else

                                    ShowMessageEmail("No se ha podido enviar los correos de evaluación del trámite", "error")
                                End If

                                listaEmail = divEmailErroneo.InnerHtml.ToString
                                ShowMessageEmailDestino(listaEmail, "info")
                                divEmailErroneo.InnerHtml = ""
                            End If

                            If _tieneEmailGyT.Text = "1" Then
                                RptaEmail = EnviarCorreoGyT(idta, codigo_dft)
                                If RptaEmail Then
                                    ShowMessageEmail("<i class=\'glyphicon glyphicon-envelope\'></i> Se ha enviado correctamente el correo de evaluación del trámite para GyT", "success")
                                Else
                                    ShowMessageEmail("No se ha podido enviar los correos de evaluación del trámite para GyT", "error")
                                End If

                                listaEmail = divEmailErroneo.InnerHtml.ToString
                                ShowMessageEmailDestino(listaEmail, "info")
                                divEmailErroneo.InnerHtml = ""
                            End If

                            'Verificamos si estamos en el ultimo flujo 
                            'verificamos que este aprobado 
                            'Y verificamos que 
                            'IF
                            'Response.Write(fnUltimoFlujoTramite() & "<br>")
                            'Response.Write(Me.rblEstado.SelectedValue & "<br>")
                            'Response.Write(tieneEntrega.ToString & "<br>")
                            If fnUltimoFlujoTramite() Then
                                'Response.Write("OK<br>")
                                EnviarSMS(txtcodalu.Value, idta, codigo_dft, "", "Aprobado")
                            End If
                            'END IF 
                        End If


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
                ShowMessage2("<i class=\'glyphicon glyphicon-alert\'></i> Haz culminado de evaluar satisfactoriamente", "alert-success")
                'Me.gvRequisitos.Enabled = True
                '_hddtareq = Desencriptar(hddtareq.Value.ToString)
                'fnListarRequisitos(_hddtareq)
                rblEstado.SelectedIndex = -1
                fnLineaDeTiempo(_hddtareq)
                CargaDatos()
            End If

        Catch ex As Exception
            Response.Write("fnAprobarRequisito: " & ex.Message & "--" & ex.StackTrace)
        End Try
    End Sub

    Private Function fnValidarCheckRequisitoFuncion() As Boolean
        Try


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
        Catch ex As Exception
            Response.Write("fnValidarCheckRequisitoFuncion: " & ex.Message & "--" & ex.StackTrace)
        End Try
    End Function

    Private Function fnValidarMail(ByVal sMail As String) As Boolean
        ' retorna true o false   
        'Return Regex.IsMatch(sMail, _
        '        "^([\w-]+\.)*?[\w-]+@[\w-]+\.([\w-]+\.)*?[\w]+$")

        Return Regex.IsMatch(sMail, "^[_a-z0-9-]+(\.[_a-z0-9-]+)*@[a-z0-9-]+(\.[a-z0-9-]+)(\.[a-z]{2,4})$")
    End Function
    Private Function fnValidarMail2(ByVal sMail As String) As Boolean

        Return Regex.IsMatch(sMail, "^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)$")
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

    Sub fnListarFlujo(ByVal codigo_dta As Integer)
        Try
            Dim dt As New Data.DataTable
            Dim obj As New ClsConectarDatos
            Dim ctf As String = ""
            Dim j As Integer = 0
            ctf = Me.Request.QueryString("ctf")
            HdAccion.Value = ""
            'Response.Write("codigo_dta: " & codigo_dta & "<br>")

            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            dt = obj.TraerDataTable("TRL_TramiteFlujo_Listar", "2", codigo_dta)
            obj.CerrarConexion()
            'Response.Write("dt1<br>")
            'Response.Write("dt: " & dt.Rows.Count.ToString & "<br>")

            Session("dft_tfu") = ""

            If dt.Rows.Count > 0 Then
                'Response.Write(dt.Rows.Count.ToString & "<br>")
                For j = 0 To dt.Rows.Count - 1
                    'Response.Write(dt.Rows(j).Item("estado_dft").ToString & "-" & dt.Rows(j).Item("cumple_dft").ToString & "<br>")
                    If dt.Rows(j).Item("estado_dft").ToString = "P" And dt.Rows(j).Item("cumple_dft").ToString = "0" Then
                        Session("dft_tfu") = dt.Rows(j).Item("codigo_tfu").ToString
                        hdTfu.value = dt.Rows(j).Item("codigo_tfu").ToString
                        hdTfuDesc.value = dt.Rows(j).Item("descripcion_Tfu").ToString
                        hdOrden.value = dt.Rows(j).Item("orden_ftr").ToString
                        hdNombrectr.value = dt.Rows(j).Item("descripcion_ctr").ToString

                        'Response.Write(Session("dft_tfu") & "<br>")
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

            Me.gvFlujoTramite.DataSource = dt
            Me.gvFlujoTramite.DataBind()

            dt = Nothing
        Catch ex As Exception
            ShowMessage("fnListarFlujo(): " & ex.Message.Replace("'", "") & "--" & ex.StackTrace.ToString, MessageType.Error)
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
        Try

            Dim index As Integer = 0
            Dim rowtfu As Integer = 0
            Dim DetAcad As Integer = 0
            Dim DetAdm As Integer = 0
            Dim DetPryTesis As Integer = 0
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
            Dim nFlujoUlt As Integer
            Dim tfFlujoUlt As Integer
            Dim tieneEmailAprobacion As Integer
            Dim tieneEmailRechazo As Integer
            Dim tieneEmailPlanEstudio As Integer = 0
            Dim tieneEmailPrecioCredito As Integer = 0
            Dim tieneEmailGyT As Integer = 0

            Dim tieneMsjTextoRechazo As Integer = 0
            Dim tieneMsjTextoAprobacion As Integer = 0

            Dim orden_ftr As Integer = 0

            Dim estaChekado As Integer = 0

            'Dim tieneconfigemail As Integer

            'Response.Write("PASO 1")
            Session("tieneEntrega") = 1

            'Response.Write("<br>tieneEntrega" & Session("tieneEntrega").ToString)
            If e.Row.RowType = DataControlRowType.DataRow Then

                index = e.Row.RowIndex
                Dim checkAcceso As CheckBox
                checkAcceso = e.Row.FindControl("chkElegir")
                ''Response.Write(gvFlujoTramite.DataKeys(index).Values("codigo_tfu").ToString())
                rowtfu = gvFlujoTramite.DataKeys(index).Values("codigo_tfu")

                DetAcad = CInt(gvFlujoTramite.DataKeys(index).Values("verDetAcad_ftr").ToString)
                DetAdm = CInt(gvFlujoTramite.DataKeys(index).Values("verDetAdm_ftr").ToString)
                DetPryTesis = CInt(gvFlujoTramite.DataKeys(index).Values("verDetProyectotesis").ToString)
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

                nFlujoUlt = gvFlujoTramite.DataKeys(index).Values("nFlujoUlt")
                tfFlujoUlt = gvFlujoTramite.DataKeys(index).Values("tfFlujoUlt")

                tieneEmailAprobacion = gvFlujoTramite.DataKeys(index).Values("tieneEmailAprobacion")
                tieneEmailRechazo = gvFlujoTramite.DataKeys(index).Values("tieneEmailRechazo")

                tieneEmailPlanEstudio = gvFlujoTramite.DataKeys(index).Values("tieneEmailPlanEstudio").ToString
                tieneEmailPrecioCredito = gvFlujoTramite.DataKeys(index).Values("tieneEmailPrecioCredito").ToString
                tieneEmailGyT = gvFlujoTramite.DataKeys(index).Values("tieneEmailGyT").ToString

                tieneMsjTextoRechazo = gvFlujoTramite.DataKeys(index).Values("tieneMsjTextoRechazo").ToString
                tieneMsjTextoAprobacion = gvFlujoTramite.DataKeys(index).Values("tieneMsjTextoAprobacion").ToString
                orden_ftr = gvFlujoTramite.DataKeys(index).Values("orden_ftr").ToString

                '_tieneEmailRechazo.Text = tieneEmailRechazo.ToString
                '_tieneEmailAprobacion.Text = tieneEmailAprobacion.ToString
                '_tieneEmailGyT.Text = tieneEmailGyT.ToString

              
             

                txtcodalu.Value = codigoAlu

                'Response.Write("<br>*tieneEntrega" & gvFlujoTramite.DataKeys(index).Values("tieneEntrega").ToString)
                'Response.Write("<br>**tieneEntrega" & Session("tieneEntrega").ToString)
                If ctf = rowtfu Or ctf = 1 Then
                    bloquear = True

                End If

                If gvFlujoTramite.DataKeys(index).Values("cumple_dft") = 0 And bloquear Then

                    _FlujoCheck.Text = CInt(_FlujoCheck.Text) + 1
                End If





                'Response.Write("<br>" & gvFlujoTramite.DataKeys(index).Values("cumple_dft") & "<br>")
                If gvFlujoTramite.DataKeys(index).Values("cumple_dft") = 0 And bloquear And CInt(_FlujoCheck.Text) = 1 Then
                    checkAcceso.Checked = True

                    _nFlujoUlt.Text = nFlujoUlt.ToString
                    _nFlujoAct.Text = orden_ftr.ToString
                    _tfFlujoUlt.Text = tfFlujoUlt.ToString
                    _tfFlujoAct.Text = ctf.ToString


                    If gvFlujoTramite.DataKeys(index).Values("tieneEntrega") = 1 Then
                        Session("tieneEntrega") = 1
                    Else
                        Session("tieneEntrega") = 0
                    End If


                    _tieneEmailRechazo.Text = tieneEmailRechazo.ToString
                    _tieneEmailAprobacion.Text = tieneEmailAprobacion.ToString

                    _tieneMsjTextoRechazo.Text = tieneMsjTextoRechazo.ToString
                    _tieneMsjTextoAprobacion.Text = tieneMsjTextoAprobacion.ToString

                    _tieneEmailPlanEstudio.Text = tieneEmailPlanEstudio.ToString
                    _tieneEmailPrecioCredito.Text = tieneEmailPrecioCredito.ToString
                    _tieneEmailGyT.Text = tieneEmailGyT.ToString

                    'Response.Write(tieneEmailPlanEstudio.ToString & "<br>")
                    'Response.Write(tieneEmailPrecioCredito.ToString & "<br>")
                    'Response.Write(tieneEmailGyT.ToString & "<br>")

                    'Response.Write(gvFlujoTramite.DataKeys(index).Values("codigo_ftr").ToString & " = " & gvFlujoTramite.DataKeys(index).Values("codigo_ftr_email") & "<br>")

                    'If gvFlujoTramite.DataKeys(index).Values("cabecera_email") <> "" Then
                    '    txtobservacionaprobacion.text = gvFlujoTramite.DataKeys(index).Values("cabecera_email") & Chr(13) & gvFlujoTramite.DataKeys(index).Values("cuerpo_email") & Chr(13) & gvFlujoTramite.DataKeys(index).Values("pie_email")
                    'Else
                    '    txtobservacionaprobacion.text = ""
                    'End If

                    If gvFlujoTramite.DataKeys(index).Values("codigo_ftr") = gvFlujoTramite.DataKeys(index).Values("codigo_ftr_email") Then
                        _tEmail_send.Text = 1
                        _tEmail_config.Text = gvFlujoTramite.DataKeys(index).Values("config_email")
                        _tEmail_cabecera.Text = gvFlujoTramite.DataKeys(index).Values("cabecera_email")
                        _tEmail_cuerpo.Text = gvFlujoTramite.DataKeys(index).Values("cuerpo_email")
                        _tEmail_pie.Text = gvFlujoTramite.DataKeys(index).Values("pie_email")
                        Me.pnlMensajeEmailReq.Visible = False   ' true  02/07/2020

                        Me.txtmensajemailreqcab.Text = _tEmail_cabecera.Text.ToString
                        Me.txtmensajeemailreq.Text = _tEmail_cuerpo.Text.ToString
                        Me.txtmensajemailreqpie.Text = _tEmail_pie.Text.ToString
                        'Me.txtmensajeemailreq.Text = Me.txtmensajeemailreq.Text.ToString & _tEmail_cuerpo.Text.ToString & Chr(13)
                        'Me.txtmensajeemailreq.Text = Me.txtmensajeemailreq.Text.ToString & _tEmail_pie.Text.ToString

                        Page.RegisterStartupScript("MensajeReq", "<script>fnCorreoReq('" & True & "');</script>")

                    Else
                        _tEmail_send.Text = 0
                        _tEmail_config.Text = ""
                        _tEmail_cabecera.Text = ""
                        _tEmail_cuerpo.Text = ""
                        _tEmail_pie.Text = ""
                        Me.pnlMensajeEmailReq.Visible = False
                        Me.txtmensajeemailreq.Text = ""

                        Page.RegisterStartupScript("MensajeReq", "<script>fnCorreoReq('" & False & "');</script>")
                    End If


                    If ctf = rowtfu Or ctf = 1 Then

                        e.Row.Font.Bold = True
                        e.Row.BackColor = Drawing.Color.AliceBlue

                        'Response.Write("CTF")
                        'Response.Write(gvFlujoTramite.DataKeys(index).Values("accionURL_ftr") & "<br>")
                        'Response.Write(DetAcad)
                        'Response.Write(DetAdm)
                        'Response.Write(DetAccion)
                        'Response.Write("<br>codigoUniver: " & codigoUniver)

                        If (DetAcad = 1) Then
                            verDetAcad(codigoUniver)
                        End If
                        If (DetAdm = 1) Then
                            verDetAdm(codigoUniver)
                        End If

                        If DetPryTesis = 1 Then
                            verDetPryTesis(codigoAlu)
                        End If

                        accionURL(codigoUniver, codigoAlu, DetTipo, codigo_trl, codigo_dta, tramite, tramiteObservacion)
                        verConceptoTramiteInfo(codigo_dft)
                        If (DetAccion = 1) Then
                            ' Response.Write("accion: " & codigo_dta & "  " & DetAccion)
                            'accionURL(codigoUniver, codigoAlu, DetTipo, codigo_trl, codigo_dta)
                            ifrGeneraDeudaPorSemestre.Visible = False
                            Select Case proceso
                                Case "RETIRAR SEMESTRE", "RETIRO DEFINITIVO"
                                    ifrGeneraDeudaPorSemestre.Visible = False
                                    Me.rowObservacion.Visible = False
                                Case "INACTIVAR ALUMNO"
                                    ifrGeneraDeudaPorSemestre.Visible = False
                                    Me.rowObservacion.Visible = False
                                Case "ACTIVAR ALUMNO"
                                    ifrGeneraDeudaPorSemestre.Visible = False
                                    Me.rowObservacion.Visible = False
                                Case "GENERAR DEUDA POR CADA SEMESTRE"
                                    Me.ifrGeneraDeudaPorSemestre.Visible = True
                                    PrevisualizarDeudaPorSemestre(codigo_dft)
                                    Me.rowObservacion.Visible = False
                                Case "GENERAR DEUDA POR TRASLADO"
                                    Me.ifrGeneraDeudaPorSemestre.Visible = True
                                    Me.rowObservacion.Visible = False
                                    PrevisualizarDeudaPorEscuelaProfesional(codigo_dft)
                                Case "GENERAR DEUDA POR REINGRESO", "GENERAR DEUDA POR TRAMITE"
                                    Me.ifrGeneraDeudaPorSemestre.Visible = True
                                    Me.rowObservacion.Visible = False
                                    PrevisualizarDeudaPorTramite(codigo_dft)
                                Case "TRASLADO INTERNO"
                                    Me.rowObservacion.Visible = False
                                Case Else
                                    ifrGeneraDeudaPorSemestre.Visible = False
                                    Me.rowObservacion.Visible = True
                            End Select

                        End If
                    Else
                        e.Row.Visible = False
                    End If
                    checkAcceso.Enabled = bloquear

                    'Exit Sub
                Else
                    'If bloquear = False Then
                    checkAcceso.Checked = True
                    checkAcceso.Enabled = False
                    ' End If


                    'If ctf = rowtfu Or ctf = 1 Then
                    '    If (DetAcad = 1) Then
                    '        verDetAcad(codigoUniver)
                    '    End If
                    '    If (DetAdm = 1) Then
                    '        verDetAdm(codigoUniver)
                    '    End If

                    '    accionURL(codigoUniver, codigoAlu, DetTipo, codigo_trl, codigo_dta, tramite, tramiteObservacion)
                    'End If

                End If

                'proceso'

                'Select Case proceso
                '    Case "RETIRAR SEMESTRE", "RETIRO DEFINITIVO"
                '        '  fnVerCursosUltimaAsistencia(Me.ddlCiclo.SelectedValue)
                '        Me.rowObservacion.Visible = False
                '        ifrGeneraDeudaPorSemestre.Visible = False
                '    Case "INACTIVAR ALUMNO"
                '        ifrGeneraDeudaPorSemestre.Visible = False
                '        Me.rowObservacion.Visible = False
                '    Case "ACTIVAR ALUMNO"
                '        ifrGeneraDeudaPorSemestre.Visible = False
                '        ' Me.rowObservacion.Visible = False
                '    Case "GENERAR DEUDA POR CADA SEMESTRE"
                '        Me.ifrGeneraDeudaPorSemestre.Visible = True

                '    Case "GENERAR DEUDA POR TRASLADO"
                '        Me.ifrGeneraDeudaPorSemestre.Visible = True
                '    Case Else
                '        ' Me.rowObservacion.Visible = True
                '        ifrGeneraDeudaPorSemestre.Visible = False
                'End Select

            End If


        Catch ex As Exception
            Response.Write(ex.Message & " -- " & ex.StackTrace)
        End Try
    End Sub

    Private Sub fnVerCursosUltimaAsistencia()
        Try
            'Response.Write("fnVerCursosUltimaAsistencia")
            Dim obj As New ClsConectarDatos
            Dim dt As New Data.DataTable
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString

            Dim codigo_alu As Integer = 0
            codigo_alu = CInt(txtcodalu.Value)
            Dim codigo_cac As Integer = 0
            If ddlCiclo.SelectedIndex > -1 Then
                codigo_cac = CInt(Me.ddlCiclo.SelectedValue.ToString)
            End If
            'Response.Write("cac: " & codigo_cac)
            obj.AbrirConexion()
            'dt = obj.TraerDataTable("TRL_BuscaTramiteLinea", Me.txtAlumno.Text.Replace(" ", "%"), Me.cboEstado.SelectedValue, Me.Request.QueryString("ctf"), Me.Request.QueryString("id"))
            dt = obj.TraerDataTable("TRL_CursoMatriculaAsistencia", "1", codigo_alu, codigo_cac)

            obj.CerrarConexion()


            Me.gvCursosMatriculadosAsistencia.DataSource = dt
            Me.gvCursosMatriculadosAsistencia.DataBind()

            dt = Nothing

        Catch ex As Exception
            Response.Write("2." & ex.Message)
        End Try

    End Sub

    Private Sub fnVerCursosUltimaAsistenciaV2(ByVal codigo_dft As Integer)
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
            dt = obj.TraerDataTable("TRL_CursoMatriculaAsistenciav2", "1", codigo_dft, codigo_cac)
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

    Private Sub PrevisualizarDeudaPorEscuelaProfesional(ByVal codigo_dft As Integer)
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        Try
            obj.AbrirConexion()
            'dt = obj.TraerDataTable("TRL_BuscaTramiteLinea", Me.txtAlumno.Text.Replace(" ", "%"), Me.cboEstado.SelectedValue, Me.Request.QueryString("ctf"), Me.Request.QueryString("id"))
            dt = obj.TraerDataTable("TRL_conceptotramiteDeudaEscuelaProfesional_Listar", "1", codigo_dft)
            obj.CerrarConexion()

            Me.gvDeudaPorSemestre.DataSource = dt
            Me.gvDeudaPorSemestre.DataBind()

            dt = Nothing

        Catch ex As Exception
            ShowMessage("PrevisualizarDeudaPorEscuelaProfesional(): " & ex.Message.Replace("'", ""), MessageType.Error)
        End Try

    End Sub

    Private Sub PrevisualizarDeudaPorTramite(ByVal codigo_dft As Integer)
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        Try
            obj.AbrirConexion()
            'dt = obj.TraerDataTable("TRL_BuscaTramiteLinea", Me.txtAlumno.Text.Replace(" ", "%"), Me.cboEstado.SelectedValue, Me.Request.QueryString("ctf"), Me.Request.QueryString("id"))
            dt = obj.TraerDataTable("TRL_conceptotramiteDeudaPorTramite_Listar", "1", codigo_dft)
            obj.CerrarConexion()

            Me.gvDeudaPorSemestre.DataSource = dt
            Me.gvDeudaPorSemestre.DataBind()

            dt = Nothing

        Catch ex As Exception
            ShowMessage("PrevisualizarDeudaPorTramite(): " & ex.Message.Replace("'", ""), MessageType.Error)
        End Try

    End Sub

    Private Sub MostrarConceptoTramiteInfo(ByVal opc As String, ByVal sw As Boolean)
        If opc = "ALU_ULTFECASIST" Then
            'Response.Write("1")
            Me.ifrRetCiclo.Visible = sw
        ElseIf opc = "DOC_ESPCELISTA" Then
            Me.ifrDocEspecialista.Visible = sw
        Else
            'Response.Write("2")
            Me.ifrRetCiclo.Visible = sw
            Me.ifrDocEspecialista.Visible = sw
        End If



    End Sub

    Protected Sub lnkAnt2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkAnt2.Click
        fnMostrarEvaluarFlujo(False)
    End Sub

    Private Sub verDetAcad(ByVal codigoUniversitario As String)
        Try
            ifrHistorial.Visible = True
            'Response.Write("verDetAdm")
            Page.RegisterStartupScript("frame2", "<script>frameHistorial.document.location.href='../SISREQ/SisSolicitudes/clsbuscaralumno.asp?codigouniver_alu=" & codigoUniversitario.ToString & "&pagina=historial.asp'</script>")

        Catch ex As Exception
            Response.Write("3." & ex.Message)
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
            Response.Write("4" & ex.Message)
        End Try

    End Sub

    Private Sub verDetPryTesis(ByVal codigo_alu As Integer)
        divInfoProyectoTesis.Visible = True
        Dim dt As New Data.DataTable
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        dt = obj.TraerDataTable("TRL_InformacionProyectoTesis", codigo_alu)
        'Response.Write(dt.Rows.Count)
        obj.CerrarConexion()

        If dt.rows.count > 0 Then

            Me.lblPry_Solicitud.text = dt.Rows(0).Item("alumno").ToString
            Me.lblPry_Facultad.text = dt.Rows(0).Item("facultad").ToString
            Me.lblPry_Programa.text = dt.Rows(0).Item("nombre_Cpf").ToString
            Me.lblPry_tesis.text = dt.Rows(0).Item("Titulo_Tes").ToString
            Me.lblPry_presupuesto.text = dt.Rows(0).Item("presupuesto").ToString
            Me.lblPry_objetivog.text = dt.Rows(0).Item("objetivogeneral").ToString
            Me.lblPry_objetivoe.text = dt.Rows(0).Item("objetivoespecifico").ToString
            Me.lblPry_fechaaprobacion.text = dt.Rows(0).Item("FechasustentacionP").ToString


        End If



        dt.Dispose()

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
                Me.lblSemestreMatriculado.Visible = True
                Me.ddlCiclo.Visible = True
                HdAccion.Value = "S"
                ddlAccion.SelectedValue = DetTipo
                If DetTipo = "T" Then
                    SelectCiclosMatriculados(codigoAlu)
                End If
            End If

        Catch ex As Exception
            Response.Write(ex.Message & "--" & ex.StackTrace)
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

            If dt.Rows.Count > 0 Then
                Me.lblInfoAdicional.Visible = True
            Else
                Me.lblInfoAdicional.Visible = False
            End If

            CalcularTotalesInformacionAdicional()

        Catch ex As Exception
            Response.Write("5." & ex.Message)
        End Try
    End Sub

    Private Sub CalcularTotalesInformacionAdicional()
        Try


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
                Me.lblTotalSemestre.Visible = True
            Else
                Me.lblTotalSemestre.Visible = False
            End If
        Catch ex As Exception
            Response.Write("6." & ex.Message)
        End Try
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

    Private Sub ListarDocenteEspecialista()
        Try
            Dim obj As New ClsConectarDatos
            Dim objFun As New ClsFunciones
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            Me.cboDocente.Items.Clear()
            Me.cboDocente.DataBind()
            objFun.CargarListas(Me.cboDocente, obj.TraerDataTable("TRL_conceptotramiteInfo_DocenteEspecialista", "1", 0, 226), "codigo_Per", "nombreusuario", "--Seleccione Docente--")
            obj.CerrarConexion()


        Catch ex As Exception
            Response.Write(ex.Message)
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
                'Response.Write(dt.Rows(i).Item("abreviatura").ToString)
                If dt.Rows(i).Item("abreviatura").ToString = "ALU_ULTFECASIST" Then

                    MostrarConceptoTramiteInfo("ALU_ULTFECASIST", True)
                    'Response.Write("VI1<br>")
                    fnVerCursosUltimaAsistencia()
                    'Response.Write("VI2<br>")
                    'Response.Write("lec:  " & dt.Rows(i).Item("lectura") & "<br>")
                    If dt.Rows(i).Item("lectura") = 1 Then

                        Me.txtUltimaFechaAsistencia.Text = dt.Rows(i).Item("valor").ToString
                        Me.txtUltimaFechaAsistencia.ReadOnly = True
                        Me.txtUltimaFechaAsistencia.Visible = True
                        MostrarConceptoTramiteInfo("ALU_ULTFECASIST", True)
                    Else

                        If dt.Rows(i).Item("valor").ToString <> "" Then
                            Me.txtUltimaFechaAsistencia.Text = dt.Rows(i).Item("valor").ToString
                            Me.txtUltimaFechaAsistencia.Visible = False

                        Else

                            Me.txtUltimaFechaAsistencia.Visible = True
                        End If
                        Me.txtUltimaFechaAsistencia.ReadOnly = False
                    End If
                ElseIf dt.Rows(i).Item("abreviatura").ToString = "DOC_ESPCELISTA" Then
                    MostrarConceptoTramiteInfo("DOC_ESPCELISTA", True)
                    ListarDocenteEspecialista()
                    If dt.Rows(i).Item("lectura") = 1 Or dt.Rows(i).Item("valor").ToString <> "" Then

                        Me.cboDocente.Enabled = False
                        Me.cboDocente.SelectedValue = dt.Rows(i).Item("valor").ToString

                    Else
                        Me.cboDocente.Enabled = True

                    End If
                End If
            Next

            Session("conceptotramiteInfo") = dt
            'Dim dc As Data.DataColumn
            'Dim dr As Data.DataRow
            'For Each dr In dt.Rows
            '    For Each dc In dt.Columns
            '        Response.Write(dr(dc.ColumnName).ToString() & "<br>")
            '    Next
            'Next


            dt = Nothing
        Catch ex As Exception
            Response.Write("7." & ex.Message)
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
                ElseIf dt.Rows(i).Item("abreviatura").ToString = "DOC_ESPCELISTA" Then
                    If dt.Rows(i).Item("lectura").ToString = 1 Then
                        rpta = True
                        Exit For
                    Else

                        If Me.cboDocente.SelectedValue = -1 Then
                            ShowMessage2("Seleccione Docente Especialista", "alert-warning")
                            Session("REG_DOC_ESPCELISTA") = False
                            cboDocente.Focus()
                            Me.rblEstado.Enabled = True
                            Me.txtobservacionaprobacion.Enabled = True
                            rpta = False
                        Else
                            msje = ""
                            Session("REG_DOC_ESPCELISTA") = True
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
            'Response.Write("OK")
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

    Public Sub fnEvaluacionFlujo()

        Dim _hddtareq As String = ""
        Dim ctf As String = ""
        Dim per As Integer = 0
        Dim mensaje(1) As String
        Dim idta As Integer = 0
        Dim obj As New ClsConectarDatos
        Dim lblResultado As Boolean = False
        Dim RptaEmail As Boolean = False
        Dim tieneEntrega As Integer = 1
        Dim codigo_dft As Integer = 0
        Dim listaEmail As String = ""
        Dim rptaTramiteInfo As Boolean = True 'EPENA: VALIDAR DATOS ADICIONALES

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
                            codigo_dft = CInt(Me.gvFlujoTramite.DataKeys(i).Values("codigo_dft").ToString)

                            'Inicio Proceso Detalle Flujo {
                            tieneEntrega = Me.gvFlujoTramite.DataKeys(i).Values("tieneEntrega")

                            rptaTramiteInfo = fnValidaRegistroConceptoTramiteInfo(codigo_dft) 'EPENA: VALIDAR DATOS ADICIONALES
                            'EPENA: VALIDAR DATOS ADICIONALES{
                            If rptaTramiteInfo Then
                                'lblResultado = True
                                lblResultado = obj.Ejecutar("TRL_DetalleFlujoTramite_Registrar", "A", idta, CInt(Me.gvFlujoTramite.DataKeys(i).Values("codigo_dft").ToString), "F", per, rblEstado.SelectedValue, Me.txtobservacionaprobacion.Text)
                            End If
                            '}EPENA: VALIDAR DATOS ADICIONALES

                            If lblResultado And rptaTramiteInfo Then 'EPENA: VALIDAR DATOS ADICIONALES


                                If Me.rblEstado.SelectedValue = "A" Then ' INICIO APROBAR

                                    If fnValidaRegistroConceptoTramiteInfo(codigo_dft) Then
                                        If Session("REG_ALU_ULTFECASIST") Then
                                            obj.Ejecutar("TRL_conceptotramiteInfo_Registrar", "1", codigo_dft, Me.txtUltimaFechaAsistencia.Text.Trim, "").copyto(mensaje, 0)

                                            If mensaje(0).Contains("correctamente") Then
                                                ShowMessage2("Se registró Ultima Fecha de Asistencia", "alert-success")
                                                txtUltimaFechaAsistencia.ReadOnly = True
                                                'Else
                                                '    ShowMessage2("No se registró Ultima Fecha de Asistencia", "alert-danger")

                                            End If
                                        End If
                                        If Session("REG_DOC_ESPCELISTA") Then
                                            obj.Ejecutar("TRL_conceptotramiteInfo_Registrar", "1", codigo_dft, Me.cboDocente.SelectedValue.ToString, "").copyto(mensaje, 0)

                                            If mensaje(0).Contains("correctamente") Then
                                                ShowMessage2("Se asignó docente especialista", "alert-success")
                                                txtUltimaFechaAsistencia.ReadOnly = True
                                                'Else
                                                '    ShowMessage2("No se asignó docente especialista", "alert-danger")
                                            End If
                                        End If

                                        ' Acciones que algunos flujos tramites estan configurados
                                        Select Case Me.gvFlujoTramite.DataKeys(i).Values("proceso").ToString
                                            Case "RETIRAR SEMESTRE"
                                                obj.Ejecutar("AnularMatriculaV2", ddlAccion.SelectedValue, CInt(Me.ddlCiclo.SelectedValue), CInt(Me.gvFlujoTramite.DataKeys(i).Values("codigo_Alu").ToString), per, Me.txtobservacionaprobacion.Text, codigo_dft, "").copyto(mensaje, 0)
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
                                                obj.Ejecutar("AnularMatriculaV2", ddlAccion.SelectedValue, 0, CInt(Me.gvFlujoTramite.DataKeys(i).Values("codigo_Alu").ToString), per, Me.txtobservacionaprobacion.Text, codigo_dft, "").copyto(mensaje, 0)
                                                'If mensaje(0).Contains("correctamente") Then
                                                '    fnMostrarEvaluarFlujo(False)
                                                'End If
                                            Case "GENERAR DEUDA POR CADA SEMESTRE"
                                                obj.Ejecutar("TRL_conceptotramiteDeudaSemestre_Registrar", "1", codigo_dft, "").copyto(mensaje, 0)
                                                'If mensaje(0).Contains("correctamente") Then
                                                '    fnMostrarEvaluarFlujo(False)
                                                'End If
                                            Case "GENERAR DEUDA POR TRASLADO"
                                                obj.Ejecutar("TRL_conceptotramiteDeudaEscuelaProfesional_Registrar", "1", codigo_dft, "").copyto(mensaje, 0)
                                                'If mensaje(0).Contains("correctamente") Then
                                                '    fnMostrarEvaluarFlujo(False)
                                                'End If
                                            Case "GENERAR DEUDA POR REINGRESO", "GENERAR DEUDA POR TRAMITE"
                                                obj.Ejecutar("TRL_conceptotramiteDeudaPorTramite_Registrar", "1", codigo_dft, "").copyto(mensaje, 0)
                                                'If mensaje(0).Contains("correctamente") Then
                                                '    fnMostrarEvaluarFlujo(False)
                                                'End If
                                            Case "TRASLADO INTERNO"
                                                obj.Ejecutar("TRL_TrasladoInterno_Registrar", "1", codigo_dft, "").copyto(mensaje, 0)
                                                'If mensaje(0).Contains("correctamente") Then
                                                '    fnMostrarEvaluarFlujo(False)
                                                'End If
                                            Case "RESTAURA MATRICULA"
                                                obj.Ejecutar("TRL_RestaurarMatricula", "", idta, "").copyto(mensaje, 0)
                                                'If mensaje(0).Contains("correctamente") Then
                                                '    fnMostrarEvaluarFlujo(False)
                                                'End If
                                            Case Else


                                        End Select

                                        ' Response.Write("tieneEmailAprobacion: " & Me.gvFlujoTramite.DataKeys(i).Values("tieneEmailAprobacion"))
                                        ' Response.Write("<br>")
                                        ' Response.Write("verDetProyectotesis: " & Me.gvFlujoTramite.DataKeys(i).Values("verDetProyectotesis"))

                                        If Me.gvFlujoTramite.DataKeys(i).Values("tieneEmailAprobacion") = 1 Then

                                            If Me.gvFlujoTramite.DataKeys(i).Values("verDetProyectotesis") = 1 Then

                                                Dim blMail As Boolean
                                                Dim clsMail As New clsComponenteTramiteVirtualEmailCVE
                                                clsMail.codigo_per = per
                                                clsMail.codigo_dta = idta
                                                clsMail.codigo_tfu = CInt(Me.gvFlujoTramite.DataKeys(i).Values("codigo_tfu"))
                                                clsMail.codigo_apl = 72
                                                clsMail.cin_abreviatura = Me.gvFlujoTramite.DataKeys(i).Values("procesoEmailAprobacion")
                                                blMail = clsMail.mt_EnviarCorreoEvaluacionFinalSustentacion()
                                               

                                                'Dim javaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
                                                'Dim myObjectJson As String = javaScriptSerializer.Serialize(clsMail)
                                                'Response.Clear()
                                                'Response.ContentType = "application/json; charset=utf-8"
                                                'Response.Write(myObjectJson)
                                                'Response.[End]()

                                                If blMail Then
                                                    ShowMessageEmail("<i class=\'glyphicon glyphicon-envelope\'></i> Se ha enviado correctamente el correo de evaluación del trámite", "success")
                                                Else
                                                    ShowMessageEmail("No se ha podido enviar los correos de evaluación del trámite", "info")
                                                End If

                                            Else
                                                'Response.Write("tieneEmailAprobacion: " & Me.gvFlujoTramite.DataKeys(i).Values("tieneEmailAprobacion"))


                                                RptaEmail = EnviaCorreoAprobacion(idta, "A", codigo_dft)



                                                If RptaEmail Then

                                                    ShowMessageEmail("<i class=\'glyphicon glyphicon-envelope\'></i> Se ha enviado correctamente el correo de evaluación del trámite", "success")
                                                Else

                                                    ShowMessageEmail("No se ha podido enviar los correos de evaluación del trámite", "error")

                                                End If

                                                listaEmail = divEmailErroneo.InnerHtml.ToString
                                                ShowMessageEmailDestino(listaEmail, "info")
                                                divEmailErroneo.InnerHtml = ""


                                            End If


                                        End If



                                        If _tieneEmailPlanEstudio.Text = "1" Then
                                            RptaEmail = EnviarCorreoPlanEstudio(codigo_dft)
                                            If RptaEmail Then

                                                ShowMessageEmail("<i class=\'glyphicon glyphicon-envelope\'></i> Se ha enviado correctamente el correo de evaluación del trámite Cambio de Plan de Estudios", "success")
                                            Else

                                                ShowMessageEmail("No se ha podido enviar los correos de evaluación del trámite Cambio de Plan de Estudios", "error")
                                            End If
                                            listaEmail = divEmailErroneo.InnerHtml.ToString
                                            ShowMessageEmailDestino(listaEmail, "info")
                                            divEmailErroneo.InnerHtml = ""


                                        End If

                                        If _tieneEmailPrecioCredito.Text = "1" Then
                                            RptaEmail = EnviarCorreoPrecioCredito(codigo_dft)
                                            If RptaEmail Then

                                                ShowMessageEmail("<i class=\'glyphicon glyphicon-envelope\'></i> Se ha enviado correctamente el correo de evaluación del trámite Cambio de Precio Credito", "success")
                                            Else

                                                ShowMessageEmail("No se ha podido enviar los correos de evaluación del trámite Cambio de Precio Credito", "error")
                                            End If
                                            listaEmail = divEmailErroneo.InnerHtml.ToString
                                            ShowMessageEmailDestino(listaEmail, "info")
                                            divEmailErroneo.InnerHtml = ""

                                        End If

                                        'Verificamos si estamos en el ultimo flujo
                                        'verificamos que este aprobado
                                        'Y verificamos que 
                                        'IF
                                        'Response.Write(fnUltimoFlujoTramite() & "<br>")
                                        'Response.Write(Me.rblEstado.SelectedValue & "<br>")
                                        'Response.Write(tieneEntrega.ToString & "<br>")
                                        If fnUltimoFlujoTramite() And Me.rblEstado.SelectedValue = "A" And tieneEntrega = 0 Then
                                            'Response.Write("OK<br>")
                                            EnviarSMS(txtcodalu.Value, idta, codigo_dft, "", "Aprobado")
                                        End If
                                        'END IF 
                                    End If
                                ElseIf Me.rblEstado.SelectedValue = "R" Then ' INICIO RECHAZAR ' SI RECHAZA LA EVALUACION
                                    'veificar si el flujo tramite tiene el activado el campo: tieneEmailRechazo 

                                    'If Me.gvFlujoTramite.DataKeys(i).Values("tieneEmailRechazo") = 1 Then
                                    '    EnviaCorreoAprobacion(idta, "R")
                                    'End If

                                    'lblResultado = obj.Ejecutar("TRL_DetalleFlujoTramite_Registrar", "R", idta, CInt(Me.gvFlujoTramite.DataKeys(i).Values("codigo_dft").ToString), "F", per, rblEstado.SelectedValue, Me.txtobservacionaprobacion.Text)
                                    'lblResultado = True


                                    'Response.Write("tieneEmailRechazo: " & Me.gvFlujoTramite.DataKeys(i).Values("tieneEmailRechazo"))

                                    If Me.gvFlujoTramite.DataKeys(i).Values("tieneEmailRechazo") = 1 Then

                                        If Me.gvFlujoTramite.DataKeys(i).Values("verDetProyectotesis") = 1 Then

                                            Dim blMail As Boolean
                                            Dim clsMail As New clsComponenteTramiteVirtualEmailCVE
                                            clsMail.codigo_per = per
                                            clsMail.codigo_dta = idta
                                            clsMail.codigo_tfu = CInt(Me.gvFlujoTramite.DataKeys(i).Values("codigo_tfu"))
                                            clsMail.codigo_apl = 72
                                            clsMail.cin_abreviatura = Me.gvFlujoTramite.DataKeys(i).Values("procesoEmailRechazo")


                                            blMail = clsMail.mt_EnviarCorreoEvaluacionFinalSustentacion()

                                            'Response.Write("blMail")
                                            'Response.Write(blMail)

                                            'Dim javaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
                                            'Dim myObjectJson As String = javaScriptSerializer.Serialize(clsMail)
                                            'Response.Clear()
                                            'Response.ContentType = "application/json; charset=utf-8"
                                            'Response.Write(myObjectJson)
                                            'Response.[End]()
                                            If blMail Then
                                                ShowMessageEmail("<i class=\'glyphicon glyphicon-envelope\'></i> Se ha enviado correctamente el correo de evaluación del trámite", "success")
                                            Else
                                                ShowMessageEmail("No se ha podido enviar los correos de evaluación del trámite", "info")
                                            End If
                                        Else
                                            RptaEmail = EnviaCorreoAprobacion(idta, "R", codigo_dft)


                                            If RptaEmail Then
                                                ShowMessageEmail("<i class=\'glyphicon glyphicon-envelope\'></i> Se ha enviado correctamente el correo de evaluación del trámite", "success")
                                            Else
                                                ShowMessageEmail("No se ha podido enviar los correos de evaluación del trámite", "info")
                                            End If

                                            listaEmail = divEmailErroneo.InnerHtml.ToString
                                            ShowMessageEmailDestino(listaEmail, "info")
                                            divEmailErroneo.InnerHtml = ""

                                        End If


                                    End If

                                  
                                    'EnviarSMS(txtcodalu.Value, idta, codigo_dft, "", "Rechazado")

                                ElseIf Me.rblEstado.SelectedValue = "O" Then ' INICIO OBSERVAR ' SI OBSERVA LA EVALUACION
                                    RptaEmail = EnviaCorreoAprobacion(idta, "O", codigo_dft)
                                    If RptaEmail Then
                                        ShowMessageEmail("<i class=\'glyphicon glyphicon-envelope\'></i> Se ha enviado correctamente el correo de evaluación del trámite", "success")
                                    Else
                                        ShowMessageEmail("No se ha podido enviar los correos de evaluación del trámite", "info")
                                    End If

                                    listaEmail = divEmailErroneo.InnerHtml.ToString
                                    ShowMessageEmailDestino(listaEmail, "info")
                                    divEmailErroneo.InnerHtml = ""
                                    EnviarSMS(txtcodalu.Value, idta, codigo_dft, "", "Observado")

                                End If ' FIN APROBAR



                                If tieneEntrega = 0 Then
                                    'EnviaCorreo(idta, "E")
                                    If fnTieneEntrega() Then
                                        tieneEntrega = 1
                                    End If
                                    'Session("tieneEntrega") = 1
                                End If
                                'Enviar correo segun su tiene entrega o no

                            End If ' FIN lblResultado
                            '} Fin Proceso Detalle Flujo


                        End If
                    Next
                End If
            Else
                ShowMessage2("No está autorizado para evaluar el flujo de tramites", "alert-warning")
            End If

            obj.TerminarTransaccion()

            If lblResultado Then
                ' EnviaCorreo(idta, "T")
                ShowMessage2("<i class=\'glyphicon glyphicon-alert\'></i> Haz culminado de evaluar satisfactoriamente", "alert-success")
                fnMostrarEvaluarFlujo(True)
                _hddtareq = Desencriptar(hddtareq.Value.ToString)
                fnListarFlujo(_hddtareq)
                fnLineaDeTiempoFlujo(_hddtareq)
                rblEstado.SelectedIndex = -1
                HdAccion.Value = ""

                Me.txtobservacionaprobacion.Enabled = False 'EPENA: VALIDAR DATOS ADICIONALES
                Me.rblEstado.Enabled = False 'EPENA: VALIDAR DATOS ADICIONALES
                Me.txtUltimaFechaAsistencia.Enabled = False 'EPENA: VALIDAR DATOS ADICIONALES
                'Response.Write("NSDHFJNDSJKN")
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

    Private Function fnTieneEntrega() As Boolean
        Try
            If _tfFlujoAct.Text = _tfFlujoUlt.Text Then
                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            Return False
        End Try

    End Function

    Private Function fnTieneFlujoTramiteConfiguracionEmail() As Boolean
        Try

            If CInt(_tEmail_config.Text) > 0 Then


                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            Return False
        End Try

    End Function

    Private Function fnUltimoFlujoTramite() As Boolean
        Try
            'Response.Write(_tfFlujoAct.Text & "===" & _tfFlujoUlt.Text & "<br>")
            If _tfFlujoAct.Text = _tfFlujoUlt.Text And _nFlujoUlt.Text = _nFlujoAct.Text Then

                Return True
            Else
                Return False
            End If

        Catch ex As Exception

        End Try
    End Function


    Private Function fnTieneEmailRespuesta(ByVal tipo As String) As Boolean
        Try
            'Response.Write(tipo & "<br>")
            'Response.Write(_tfFlujoAct.Text & "=" & _tfFlujoUlt.Text & "<br>")

            If _tfFlujoAct.Text = _tfFlujoUlt.Text Then
                If tipo = "A" Then
                    If _tieneEmailAprobacion.Text = 1 Then
                        Return True
                    Else
                        Return False
                    End If
                ElseIf tipo = "R" Then
                    If _tieneEmailRechazo.Text = 1 Then
                        Return True
                    Else
                        Return False
                    End If
                End If
            Else
                If tipo = "R" Then
                    'Response.Write(_tieneEmailRechazo.Text & "<br>")
                    If _tieneEmailRechazo.Text = 1 Then
                        Return True
                    Else
                        Return False
                    End If

                Else
                    If _tieneEmailAprobacion.Text = 1 Then
                        Return True
                    Else
                        Return False
                    End If
                End If
            End If
        Catch ex As Exception
            Return False
        End Try

    End Function

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

    Protected Sub gdDeudaTramite_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gdDeudaTramite.RowDataBound
        Try

            e.Row.Cells(1).Style.Item("text-align") = "right"
            e.Row.Cells(2).Style.Item("text-align") = "right"
            e.Row.Cells(3).Style.Item("text-align") = "right"

            If e.Row.RowIndex >= 0 Then

                e.Row.Cells(1).Text = e.Row.Cells(1).Text
                e.Row.Cells(3).Text = CInt(e.Row.Cells(1).Text) * CDec(e.Row.Cells(2).Text)
                e.Row.Cells(2).Text = "S/ " & e.Row.Cells(2).Text
                e.Row.Cells(3).Text = "S/ " & e.Row.Cells(3).Text


            End If
        Catch ex As Exception

        End Try
    End Sub
End Class
