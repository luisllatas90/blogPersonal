Imports System.Data
Imports System.IO
Imports System.Web.UI.WebControls
Imports System.Globalization
Imports System.Web
Imports AjaxControlToolkit

Partial Class frmGestionInvestigacionX
    Inherits System.Web.UI.Page

#Region "Métodos y Funciones del Formulario"

    Protected Sub form1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles form1.Load
        System.Threading.Thread.CurrentThread.CurrentCulture = New System.Globalization.CultureInfo("es-PE")
        System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.CurrencyDecimalSeparator = "."
        System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.CurrencyGroupSeparator = ","
        System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator = "."
        System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.NumberGroupSeparator = ","
        If Not IsPostBack Then
            CargarInvestigaciones()
            CargarUsuario()
            EstadoControlesSegunPerfil(Request.QueryString("ctf"))
        End If
        'btnConforme.Attributes.Add("onclick", "if (!confirm('Ud. desea dar conformidad a los proyectos de investigación seleccionados?')) return false;")
    End Sub

    Private Sub EstadoControlesSegunPerfil(ByVal ctf As Integer)
        Try
            Me.lblTipDocumento.Visible = False
            Me.btnTipDocumento.Visible = False
            Select Case ctf
                Case 1      'Administradores
                    btnEnviar.Enabled = True
                    btnEnviar.Visible = True
                    btnObservar.Visible = True
                    btnRechazar.Visible = True
                    btnConforme.Visible = True

                Case 64      'Administradores
                    btnEnviar.Enabled = True
                    btnEnviar.Visible = True
                    btnObservar.Visible = True
                    btnRechazar.Visible = True
                    btnConforme.Visible = True
                    btnNuevo.Visible = False

                Case 132    'Coordinadores
                    If gvInvestigacion.Rows.Count = 0 Then
                        btnEnviar.Enabled = False
                        btnEnviar.ToolTip = "No se encontraron investigaciones a enviar"
                        btnObservar.Enabled = False
                        btnRechazar.Enabled = False
                        btnConforme.Enabled = False
                        btnNuevo.Visible = False
                    Else
                        btnEnviar.Enabled = True
                        btnEnviar.ToolTip = "Enviar Investigaciones"
                        btnObservar.Enabled = True
                        btnRechazar.Enabled = True
                        btnConforme.Enabled = True
                        btnNuevo.Visible = False
                    End If

                Case 131    'Solo Revisores
                    If gvInvestigacion.Rows.Count = 0 Then
                        btnEnviar.Enabled = False
                        btnEnviar.Visible = False
                        btnNuevo.Visible = False

                        btnObservar.Enabled = False
                        btnRechazar.Enabled = False
                        btnConforme.Enabled = False
                    Else
                        btnObservar.Enabled = True
                        btnRechazar.Enabled = True
                        btnConforme.Enabled = True

                        btnNuevo.Visible = False
                        btnEnviar.Visible = False

                    End If
                Case 13     'Profesores - Autores
                    btnEnviar.Enabled = False
                    btnEnviar.Visible = False

                    btnObservar.Visible = False
                    btnRechazar.Visible = False
                    btnConforme.Visible = False

            End Select
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub


    Protected Sub gvInvestigacion_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvInvestigacion.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.Header Then
                Select Case Request.QueryString("ctf")
                    Case 64, 131, 132
                        e.Row.Cells(7).Visible = False
                        e.Row.Cells(8).Visible = True
                    Case 13
                        e.Row.Cells(8).Visible = False
                    Case Else
                        e.Row.Cells(8).Visible = True
                End Select
            End If

            If e.Row.RowType = DataControlRowType.DataRow Then
                Select Case Request.QueryString("ctf")
                    Case 64, 131, 132
                        e.Row.Cells(7).Visible = False
                        e.Row.Cells(8).Visible = True
                    Case 13
                        e.Row.Cells(8).Visible = False
                    Case Else
                        e.Row.Cells(8).Visible = True
                End Select
                e.Row.Attributes.Add("OnMouseOver", "Resaltar(1,this,'S')")
                e.Row.Attributes.Add("OnMouseOut", "Resaltar(0,this,'S')")
                e.Row.Attributes.Add("OnClick", "javascript:__doPostBack('gvInvestigacion','Select$" & e.Row.RowIndex & "');")
                e.Row.Style.Add("cursor", "hand")
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub gvInvestigacion_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvInvestigacion.SelectedIndexChanged
        Try
            pnlDetalle.CssClass = ""
            hfCodInvestigacion.Value = gvInvestigacion.SelectedRow.Cells(0).Text

            Dim objInv As New clsInvestigacion
            Dim dt As New DataTable
            'dt = objInv.ConsultaInvestigaciones(hfCodInvestigacion.Value, Request.QueryString("ctf").ToString, Request.QueryString("id").ToString)
            '-------------------------------------------------------------------------------------------------------------------------------------------------
            'Consulta la ultima version de la tabla investigacion_versiones
            dt = objInv.ConsultaDatosInvestigaciones(hfCodInvestigacion.Value, Request.QueryString("ctf").ToString, Request.QueryString("id").ToString)
            '-------------------------------------------------------------------------------------------------------------------------------------------------

            lblTitulo.Text = dt.Rows(0).Item("titulo")
            lblFechaRegistro.Text = dt.Rows(0).Item("fechaRegistro")
            lblFecIni.Text = dt.Rows(0).Item("fechaInicio")
            lblFecFin.Text = dt.Rows(0).Item("fechaFin")
            lblPresupuesto.Text = dt.Rows(0).Item("presupuesto")
            lblFinanci.Text = dt.Rows(0).Item("tipoFinanciamiento")
            lblAmbito.Text = dt.Rows(0).Item("Ambito")
            lblLinea.Text = dt.Rows(0).Item("linea")
            lblEtapa.Text = dt.Rows(0).Item("etapa")
            lblTipo.Text = dt.Rows(0).Item("tipo")
            lblInstancia.Text = dt.Rows(0).Item("instancia")
            lblBeneficiarios.Text = dt.Rows(0).Item("beneficiarios")
            lblResumen.Text = dt.Rows(0).Item("resumen")

            '---------------------------------------------------------------------
            lblEstadoEnvio.Text = dt.Rows(0).Item("enviado").ToString
            lblEstadoEnvio.Font.Bold = True
            If dt.Rows(0).Item("enviado").ToString() = "Enviado" Then
                lblEstadoEnvio.ForeColor = Drawing.Color.Blue
            Else
                lblEstadoEnvio.ForeColor = Drawing.Color.Red
            End If

            If Request.QueryString("ctf") = 13 Then
                If dt.Rows(0).Item("investigacion_instancia_id") = 4 Then
                    Me.lblTipDocumento.Visible = True
                    Me.btnTipDocumento.Visible = True
                    Me.lblTipDocumento.ForeColor = Drawing.Color.Blue
                    Me.lblTipDocumento.Text = "Subir Informe"
                Else
                    Me.lblTipDocumento.Visible = False
                    Me.btnTipDocumento.Visible = False
                    Me.lblTipDocumento.Text = ""
                End If
            End If

            If Request.QueryString("ctf") = 64 Then
                If dt.Rows(0).Item("investigacion_instancia_id") = 4 Then
                    Me.lblTipDocumento.Visible = True
                    Me.btnTipDocumento.Visible = True
                    Me.lblTipDocumento.ForeColor = Drawing.Color.Blue
                    Me.lblTipDocumento.Text = "Asignar NºResolución"
                Else
                    Me.lblTipDocumento.Visible = False
                    Me.btnTipDocumento.Visible = False
                    Me.lblTipDocumento.Text = ""
                End If
            End If
            '---------------------------------------------------------------------

            'Creando la tabla Documentos
            Dim dtDocumentos As New Data.DataTable
            dtDocumentos.Columns.Add("extension", GetType(String))
            dtDocumentos.Columns.Add("nombre", GetType(String))
            dtDocumentos.Columns.Add("ruta", GetType(String))
            dtDocumentos.Columns.Add("documento", GetType(String))

            If dt.Rows(0).Item("rutaInforme") <> "" Then
                Dim myrow As Data.DataRow
                myrow = dtDocumentos.NewRow
                myrow("extension") = dt.Rows(0).Item("extInf")
                myrow("nombre") = "Informe"
                myrow("ruta") = dt.Rows(0).Item("rutaInforme")
                myrow("documento") = dt.Rows(0).Item("docInf")
                dtDocumentos.Rows.Add(myrow)
            End If
            If dt.Rows(0).Item("rutaProyecto") <> "" Then
                Dim myrow As Data.DataRow
                myrow = dtDocumentos.NewRow
                myrow("extension") = dt.Rows(0).Item("extPro")
                myrow("nombre") = "Proyecto"
                myrow("ruta") = dt.Rows(0).Item("rutaProyecto")
                myrow("documento") = dt.Rows(0).Item("docPro")
                dtDocumentos.Rows.Add(myrow)
            End If
            gvDocumentos.DataSource = dtDocumentos
            gvDocumentos.DataBind()

            gvBitacora.DataSource = objInv.ConsultaBitacora(hfCodInvestigacion.Value)
            gvBitacora.DataBind()

            CargarResumen()
            CargarNroVersiones()

            CargarObservacionesProyecto()

            txtObs.Text = ""
            lblDocumento.Text = ""

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub CargarResumen()
        Session("hito_id") = 0
        gvResumen.SelectedIndex = -1
        Dim objInv As New clsInvestigacion
        Dim dt As New DataTable
        dt = objInv.ConsultaResumen(hfCodInvestigacion.Value, True)
        gvResumen.DataSource = dt
        gvResumen.DataBind()
    End Sub

    Private Sub CargarNroVersiones()
        Dim objInv As New clsInvestigacion
        Dim objfun As New ClsFunciones
        Dim dt As New DataTable
        dt = objInv.ConsultaNroVersiones(hfCodInvestigacion.Value, Request.QueryString("ctf"))
        objfun.CargarListas(ddlVersion, dt, "id", "nro")
        If dt.Rows.Count > 0 Then
            ddlVersion.SelectedIndex = dt.Rows.Count - 1
        End If
    End Sub

    Protected Sub gvInvestigacion_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gvInvestigacion.PageIndexChanging
        gvInvestigacion.PageIndex = e.NewPageIndex
        CargarInvestigaciones()
    End Sub

    Protected Sub Menu1_MenuItemClick(ByVal sender As Object, _
      ByVal e As MenuEventArgs) Handles Menu1.MenuItemClick
        MultiView1.ActiveViewIndex = Int32.Parse(e.Item.Value)
        Dim i As Integer
        'Make the selected menu item reflect the correct imageurl
        For i = 0 To Menu1.Items.Count - 1
            If i = e.Item.Value Then
                If i = 0 Then
                    Menu1.Items(i).ImageUrl = "~/Images/TagButtons/btnInvDGSel.png"
                ElseIf i = 1 Then
                    Menu1.Items(i).ImageUrl = "~/Images/TagButtons/btnInvBitacoraSel.png"
                ElseIf i = 2 Then
                    Menu1.Items(i).ImageUrl = "~/Images/TagButtons/btnInvResumenSel.png"
                End If
            Else
                If i = 0 Then
                    Menu1.Items(i).ImageUrl = "~/Images/TagButtons/btnInvDG.png"
                ElseIf i = 1 Then
                    Menu1.Items(i).ImageUrl = "~/Images/TagButtons/btnInvBitacora.png"
                ElseIf i = 2 Then
                    Menu1.Items(i).ImageUrl = "~/Images/TagButtons/btnInvResumen.png"
                End If
            End If
        Next
    End Sub

    Protected Sub gvBitacora_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gvBitacora.PageIndexChanging
        gvBitacora.PageIndex = e.NewPageIndex
        Dim objInv As New clsInvestigacion
        gvBitacora.DataSource = objInv.ConsultaBitacora(hfCodInvestigacion.Value)
        gvBitacora.DataBind()
    End Sub

    Protected Sub gvResumen_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gvResumen.PageIndexChanging
        gvResumen.PageIndex = e.NewPageIndex
        CargarResumen()
    End Sub

    Protected Sub gvResumen_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvResumen.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                e.Row.Attributes.Add("OnMouseOver", "Resaltar(1,this,'S')")
                e.Row.Attributes.Add("OnMouseOut", "Resaltar(0,this,'S')")
                e.Row.Attributes.Add("OnClick", "javascript:__doPostBack('gvResumen','Select$" & e.Row.RowIndex & "');")
                e.Row.Style.Add("cursor", "hand")
                If Session("hito_id") = DataBinder.Eval(e.Row.DataItem, "hito_id").ToString Then
                    Dim tbHito As Panel
                    tbHito = e.Row.FindControl("pnlHito")
                    tbHito.CssClass = "hidden"
                End If
                Session("hito_id") = DataBinder.Eval(e.Row.DataItem, "hito_id").ToString
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub gvResumen_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvResumen.SelectedIndexChanged
        Dim hfA As New HiddenField
        Dim objInv As New clsInvestigacion
        hfA = gvResumen.SelectedRow.FindControl("hfAvance_id")
        dlObservaciones.DataSource = objInv.ConsultaObservacion(hfCodInvestigacion.Value, hfA.Value)
        dlObservaciones.DataBind()
    End Sub

#End Region

#Region "Métodos y Funciones de Usuario"

    Private Sub CargarObservacionesProyecto()
        Dim objInv As New clsInvestigacion
        dlObservaciones.DataSource = objInv.ConsultaObservacion(hfCodInvestigacion.Value, 0)
        dlObservaciones.DataBind()
    End Sub

    Private Sub CargarInvestigaciones()
        Dim objInv As New clsInvestigacion
        gvInvestigacion.DataSource = objInv.ConsultaInvestigaciones(0, Request.QueryString("ctf").ToString, Request.QueryString("id").ToString)
        gvInvestigacion.DataBind()
    End Sub

    Private Sub CargarUsuario()
        Dim objPre As New ClsPresupuesto
        Dim dt As New DataTable
        dt = objPre.ObtenerDatosUsuario(Request.QueryString("id"))
        If dt.Rows.Count > 0 Then
            hfUsuReg.Value = dt.Rows(0).Item("usuario_per")
        End If
    End Sub

#End Region

    Protected Sub cmdGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdGuardar.Click
        If txtObs.Text = "" Then
            lblDocumento.Text = "Debe de Ingresar una Observación."
            Exit Sub
        End If

        Dim objInv As New clsInvestigacion
        Dim hfA As New HiddenField
        hfA = gvResumen.SelectedRow.FindControl("hfAvance_id")
        If hfA.Value = "0" Then
            lblDocumento.Text = "Debe seleccionar un Hito que contenga un avance."
            Exit Sub
        End If
        Dim filePath, archivo As String
        archivo = "\" & Now.Day.ToString & Now.Month.ToString & Now.Year.ToString & Now.Hour.ToString & Now.Minute.ToString & Now.Second.ToString & System.IO.Path.GetExtension(afuObservacion.PostedFile.FileName).ToString
        filePath = Server.MapPath("../../filesInvestigacion")
        If afuObservacion.HasFile Then
            filePath = filePath & "\" & hfCodInvestigacion.Value
            Dim carpeta As New System.IO.DirectoryInfo(filePath)
            If carpeta.Exists = False Then
                carpeta.Create()
            End If
            'Session("ruta") = filePath & archivo
            afuObservacion.SaveAs(filePath & archivo)
        End If
        objInv.AbrirTransaccionCnx()
        objInv.AgregaObservacion(hfCodInvestigacion.Value, hfA.Value, txtObs.Text, filePath & archivo, 215, hfUsuReg.Value) ' Request.QueryString("id").ToString, hfUsuReg.Value)
        objInv.CerrarTransaccionCnx()

        CargarResumen()
        dlObservaciones.DataSource = objInv.ConsultaObservacion(hfCodInvestigacion.Value, hfA.Value)
        dlObservaciones.DataBind()
        lblDocumento.Text = ""
        txtObs.Text = ""
        hfDocumento.Value = ""
    End Sub


    Protected Sub btnNuevo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNuevo.Click
        'ClientScript.RegisterStartupScript(Me.GetType, "siguientepagina", "location.href='frmRegistrarInvestigaciones.aspx?tipo=N&id=" & Request.QueryString("id").ToString & "';", True)
        ClientScript.RegisterStartupScript(Me.GetType, "siguientepagina", "location.href='frmRegistrarInvestigaciones.aspx?tipo=N&id=" & Request.QueryString("id").ToString & "&ctf=" & Request.QueryString("ctf") & "';", True)
    End Sub

    Protected Sub lbModificar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim row As GridViewRow
        Dim lbModificar As LinkButton
        lbModificar = sender
        row = lbModificar.NamingContainer
        'ClientScript.RegisterStartupScript(Me.GetType, "siguientepagina", "location.href='frmRegistrarInvestigaciones.aspx?idInv=" & row.Cells.Item(0).Text & "&id=" & Request.QueryString("id").ToString & "&tipo=M';", True)
        ClientScript.RegisterStartupScript(Me.GetType, "siguientepagina", "location.href='frmRegistrarInvestigaciones.aspx?idInv=" & row.Cells.Item(0).Text & "&id=" & Request.QueryString("id").ToString & "&ctf=" & Request.QueryString("ctf") & "&tipo=M';", True)
    End Sub

    Protected Sub hlNuevoHito_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        txtDesHito.Text = ""
        txtFechaHito.Text = ""
        hfTransaccion.Value = "N"
        mpeDescripcion.Show()
    End Sub

    Protected Sub ibtnVer_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        mpeDescripcion.Show()
    End Sub

    Protected Sub btnGuardarHito_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardarHito.Click
        'Try
        Dim objInv As New clsInvestigacion
        If hfTransaccion.Value = "N" Then
            objInv.AbrirTransaccionCnx()
            objInv.AgregaHito(hfCodInvestigacion.Value, txtDesHito.Text, txtFechaHito.Text, hfUsuReg.Value)
            objInv.CerrarTransaccionCnx()
        ElseIf hfTransaccion.Value = "M" Then
            objInv.AbrirTransaccionCnx()
            objInv.ActualizaHito(hfHito_id.Value, hfCodInvestigacion.Value, txtDesHito.Text, txtFechaHito.Text)
            objInv.CerrarTransaccionCnx()
        End If
        CargarResumen()
        txtDesHito.Text = ""
        txtFechaHito.Text = ""
        mpeDescripcion.Hide()
        'Catch ex As Exception
        '    Response.Write(ex.Message)
        'End Try
    End Sub

    Protected Sub hlNuevoAvance_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim lbNuevoAvance As LinkButton
        Dim row As GridViewRow
        Dim hfH, hfAv As HiddenField
        lbNuevoAvance = sender
        row = lbNuevoAvance.NamingContainer
        hfH = row.FindControl("hfHito_id")
        hfAv = row.FindControl("hfAvance_id")
        hfHito_id.Value = hfH.Value
        hfAvance_id.Value = hfAv.Value
        mpeAvance.Show()
    End Sub

    'Private Sub afuAvance_UploadedComplete(ByVal sender As Object, ByVal e As AjaxControlToolkit.AsyncFileUploadEventArgs) Handles afuAvance.UploadedComplete

    'End Sub

    Protected Sub btnGuardarAvance_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardarAvance.Click
        Try
            Dim filePath As String
            Dim archivo As String = "\" & Now.Day.ToString & Now.Month.ToString & Now.Year.ToString & Now.Hour.ToString & Now.Minute.ToString & Now.Second.ToString & System.IO.Path.GetExtension(afuAvance.PostedFile.FileName).ToString
            If afuAvance.HasFile Then
                filePath = Server.MapPath("../../filesInvestigacion")
                filePath = filePath & "\" & hfCodInvestigacion.Value
                Dim carpeta As New System.IO.DirectoryInfo(filePath)
                If carpeta.Exists = False Then
                    carpeta.Create()
                End If
                'Session("ruta") = filePath & archivo
                afuAvance.SaveAs(filePath & archivo)
            End If
            Dim objInv As New clsInvestigacion
            objInv.AbrirTransaccionCnx()
            objInv.AgregaAvance(hfCodInvestigacion.Value, hfHito_id.Value, filePath & archivo, txtFechaAvance.Text, hfUsuReg.Value)
            objInv.CerrarTransaccionCnx()
            txtDocAvance.Text = ""
            txtFechaAvance.Text = ""
            CargarResumen()
            mpeAvance.Hide()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub lbModificarHito_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim lbNuevoHito As LinkButton
        Dim row As GridViewRow
        Dim hf As HiddenField
        Dim lblFecha, lblDes As Label
        lbNuevoHito = sender
        row = lbNuevoHito.NamingContainer
        hf = row.FindControl("hfHito_id")
        lblFecha = row.FindControl("lblFechaHito")
        lblDes = row.FindControl("lblDesHito")
        hfHito_id.Value = hf.Value
        txtDesHito.Text = lblDes.Text
        txtFechaHito.Text = lblFecha.Text
        hfTransaccion.Value = "M"
        mpeDescripcion.Show()
    End Sub

    Protected Sub lbEliminarHito_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim lbNuevoHito As LinkButton
        Dim row As GridViewRow
        Dim hf As HiddenField
        Dim objInv As New clsInvestigacion
        lbNuevoHito = sender
        row = lbNuevoHito.NamingContainer
        hf = row.FindControl("hfHito_id")
        objInv.AbrirTransaccionCnx()
        objInv.EliminaHito(hf.Value, hfCodInvestigacion.Value)
        objInv.CerrarTransaccionCnx()
        CargarResumen()
    End Sub

    Protected Sub hlEnviar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim row As GridViewRow
            Dim hlEnviar As LinkButton
            Dim obj As New clsInvestigacion
            Dim dts As New Data.DataTable

            hlEnviar = sender
            row = hlEnviar.NamingContainer
            'Lo ejecute en un evento debido a que me estaba dando problemas ejecutarlo aqui!!
            EjecucionEnvios(row.Cells.Item(0).Text, Me.Request.QueryString("id"))
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub EjecucionEnvios(ByVal id_investigacion As Integer, ByVal codigo_per As Integer)
        Try
            Dim exec As Integer = 0
            Dim obj As New clsInvestigacion

            obj.AbrirTransaccionCnx()
            exec = obj.EjecutaEnvioInvestigacion(id_investigacion, codigo_per, 0)
            obj.CerrarTransaccionCnx()
            obj = Nothing

            If exec > 0 Then
                ClientScript.RegisterStartupScript(Me.GetType, "FaltanDatos", "alert('El envio se ha realizado de forma satisfactoria.');", True)

                CargarInvestigaciones()
                EnviarMensaje(id_investigacion, Request.QueryString("ctf"), "")
            Else
                ClientScript.RegisterStartupScript(Me.GetType, "FaltanDatos", "alert('Usted no es el Autor del proyecto de Investigación, no podrá realizar el envio.');", True)
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    'xD
    Private Sub EnviarMensaje(ByVal id_investigacion As Integer, ByVal ctf As Integer, ByVal tipo As String)
        Try
            Dim ObjMailNet As New ClsMail
            Dim mensaje As String = ""
            Dim para As String = ""
            Dim obj As New clsInvestigacion
            Dim dts As New Data.DataTable

            Select Case tipo
                Case "V"
                    '---------------------------------------------'
                    'Para versiones del proyecto de investigacion '
                    '---------------------------------------------'
                    dts = obj.ListaInvestigacionRevisores(id_investigacion, ctf)
                    For i As Integer = 0 To dts.Rows.Count - 1
                        If dts.Rows(i).Item("email_Per") <> "" Then
                            para = "</br><font face='Courier'>" & "Estimado(a): <b>" & dts.Rows(i).Item("nombre_revisor").ToString.ToUpper & "</b>"
                            mensaje = "</br></br><P><ALIGN='justify'> Se le comunica que, el proyecto de investigación " & dts.Rows(i).Item("nombre_investigacion").ToString.ToUpper & " ha sido modificado y enviado para su revisión." & "</P>"
                            mensaje = mensaje & "</br> Atte.<br><br>Campus Virtual - USAT.</font>"
                            ObjMailNet.EnviarMail("campusvirtual@usat.edu.pe", "Investigaciones", dts.Rows(i).Item("email_Per").ToString, "Miembro revisor de investigaciones USAT", para & mensaje, True)
                        End If
                    Next
                Case "O"
                    '--------------------------------------------------------------------------------------'
                    'Envia Email al autor y al coordinar indicando que la investigacion ha sido observada  '
                    '--------------------------------------------------------------------------------------'
                    Dim dt As New Data.DataTable
                    dt = obj.RecuperaAutorCoodinardorInvestigacion(id_investigacion, ctf)
                    If dt.Rows.Count > 0 Then
                        For i As Integer = 0 To dt.Rows.Count - 1
                            If dt.Rows(i).Item("coordinador") = 1 Then
                                'Coordinador
                                para = "</br><font face='Courier'>" & "Estimado(a) Coordinador de Investigaciones USAT: <b>" & dt.Rows(i).Item("nombre_completo").ToString.ToUpper & "</b>"
                                mensaje = "</br></br><P><ALIGN='justify'> Se le comunica que, el proyecto de investigación " & dt.Rows(i).Item("titulo").ToString.ToUpper & " ha sido <b>Observado</b> por un miembro revisor del proyecto." & "</P>"
                                mensaje = mensaje & "</br> Atte.<br><br>Campus Virtual - USAT.</font>"
                                ObjMailNet.EnviarMail("campusvirtual@usat.edu.pe", "Investigaciones", dt.Rows(i).Item("email").ToString, "Investigaciones USAT", para & mensaje, True)
                            Else
                                'Autor
                                para = "</br><font face='Courier'>" & "Estimado(a): <b>" & dt.Rows(i).Item("nombre_completo").ToString.ToUpper & "</b>"
                                mensaje = "</br></br><P><ALIGN='justify'> Se le comunica que, el proyecto de investigación " & dt.Rows(i).Item("titulo").ToString.ToUpper & " ha sido <b>Observado</b> por un miembro revisor del proyecto." & "</P>"
                                mensaje = mensaje & "</br> Atte.<br><br>Campus Virtual - USAT.</font>"
                                ObjMailNet.EnviarMail("campusvirtual@usat.edu.pe", "Investigaciones", dt.Rows(i).Item("email").ToString, "Investigaciones USAT", para & mensaje, True)
                            End If
                        Next
                    End If

                Case "R"
                    '--------------------------------------------------------------------------------------'
                    'Envia Email al autor y al coordinar indicando que la investigacion ha sido Rechazado  '
                    '--------------------------------------------------------------------------------------'
                    Dim dt As New Data.DataTable
                    dt = obj.RecuperaAutorCoodinardorInvestigacion(id_investigacion, ctf)
                    If dt.Rows.Count > 0 Then
                        For i As Integer = 0 To dt.Rows.Count - 1
                            If dt.Rows(i).Item("coordinador") = 1 Then
                                'Coordinador
                                para = "</br><font face='Courier'>" & "Estimado(a) Coordinador de Investigaciones USAT: <b>" & dt.Rows(i).Item("nombre_completo").ToString.ToUpper & "</b>"
                                mensaje = "</br></br><P><ALIGN='justify'> Se le comunica que, el proyecto de investigación " & dt.Rows(i).Item("titulo").ToString.ToUpper & " ha sido <b>Rechazada</b> por un miembro revisor del proyecto." & "</P>"
                                mensaje = mensaje & "</br> Atte.<br><br>Campus Virtual - USAT.</font>"
                                ObjMailNet.EnviarMail("campusvirtual@usat.edu.pe", "Investigaciones", dt.Rows(i).Item("email").ToString, "Investigaciones USAT", para & mensaje, True)
                            Else
                                'Autor
                                para = "</br><font face='Courier'>" & "Estimado(a): <b>" & dt.Rows(i).Item("nombre_completo").ToString.ToUpper & "</b>"
                                mensaje = "</br></br><P><ALIGN='justify'> Se le comunica que, el proyecto de investigación " & dt.Rows(i).Item("titulo").ToString.ToUpper & " ha sido <b>Rechazada</b> por un miembro revisor del proyecto." & "</P>"
                                mensaje = mensaje & "</br> Atte.<br><br>Campus Virtual - USAT.</font>"
                                ObjMailNet.EnviarMail("campusvirtual@usat.edu.pe", "Investigaciones", dt.Rows(i).Item("email").ToString, "Investigaciones USAT", para & mensaje, True)
                            End If
                        Next
                    End If


                Case "C"
                    '--------------------------------------------------------------------------------------'
                    'Envia Email al autor y al coordinar indicando que la investigacion ha sido Aprobada   '
                    '--------------------------------------------------------------------------------------'
                    Dim dt As New Data.DataTable
                    dt = obj.RecuperaAutorCoodinardorInvestigacion(id_investigacion, ctf)
                    If dt.Rows.Count > 0 Then
                        For i As Integer = 0 To dt.Rows.Count - 1
                            If dt.Rows(i).Item("coordinador") = 1 Then
                                'Coordinador
                                para = "</br><font face='Courier'>" & "Estimado(a) Coordinador de Investigaciones USAT: <b>" & dt.Rows(i).Item("nombre_completo").ToString.ToUpper & "</b>"
                                mensaje = "</br></br><P><ALIGN='justify'> Se le comunica que, el proyecto de investigación " & dt.Rows(i).Item("titulo").ToString.ToUpper & " ha sido <b>Aprobada</b> por un miembro revisor del proyecto." & "</P>"
                                mensaje = mensaje & "</br> Atte.<br><br>Campus Virtual - USAT.</font>"
                                ObjMailNet.EnviarMail("campusvirtual@usat.edu.pe", "Investigaciones", dt.Rows(i).Item("email").ToString, "Investigaciones USAT", para & mensaje, True)
                            Else
                                'Autor
                                para = "</br><font face='Courier'>" & "Estimado(a): <b>" & dt.Rows(i).Item("nombre_completo").ToString.ToUpper & "</b>"
                                mensaje = "</br></br><P><ALIGN='justify'> Se le comunica que, el proyecto de investigación " & dt.Rows(i).Item("titulo").ToString.ToUpper & " ha sido <b>Aprobado</b> por un miembro revisor del proyecto." & "</P>"
                                mensaje = mensaje & "</br> Atte.<br><br>Campus Virtual - USAT.</font>"
                                ObjMailNet.EnviarMail("campusvirtual@usat.edu.pe", "Investigaciones", dt.Rows(i).Item("email").ToString, "Investigaciones USAT", para & mensaje, True)
                            End If
                        Next
                    End If

                Case "E"
                    'Ejecuta cuando el coordinador realiza el envio para pasar de instancia el proyecto.
                    Dim dts_instancia As New Data.DataTable
                    dts_instancia = obj.InstanciaInvestigacion(id_investigacion)
                    If dts_instancia.Rows.Count > 0 Then
                        Select Case dts_instancia.Rows(0).Item("investigacion_instancia_id")
                            'Instancia Vicerrectorado
                            Case 4
                                'Email al director de investigaciones
                                Dim dts_dir As New Data.DataTable
                                dts_dir = obj.ConsultaDirectorInvestigacion(id_investigacion)
                                If dts_dir.Rows.Count > 0 Then
                                    para = "</br><font face='Courier'>" & "Estimado(a): <b>" & dts_dir.Rows(0).Item("Director_inv").ToString.ToUpper & "</b>"
                                    mensaje = "</br></br><P><ALIGN='justify'> Se le comunica que, la investigación " & dts_dir.Rows(0).Item("titulo").ToString.ToUpper & " se encuentra el la instancia" & dts_dir.Rows(0).Item("instancia").ToString.ToUpper & "</P>"
                                    mensaje = mensaje & "</br> Atte.<br><br>Campus Virtual - USAT.</font>"
                                    ObjMailNet.EnviarMail("campusvirtual@usat.edu.pe", "Investigaciones", dts_dir.Rows(0).Item("email_Per").ToString, "Director de investigaciones USAT", para & mensaje, True)
                                End If

                                'Email al Autor
                                Dim dtsA As New Data.DataTable
                                dtsA = obj.ConsultaAutorInvestigacion(id_investigacion, Me.Request.QueryString("ctf"))
                                If (Request.QueryString("ctf") = 132) Then
                                    If dtsA.Rows.Count > 0 Then
                                        para = "</br><font face='Courier'>" & "Estimado(a): <b>" & dtsA.Rows(0).Item("Autor").ToString.ToUpper & "</b>"
                                        mensaje = "</br></br><P><ALIGN='justify'> Se le comunica que, su investigación " & dtsA.Rows(0).Item("titulo").ToString.ToUpper & " a sido <b> aprobada </b> y se encuentra el la instancia" & dtsA.Rows(0).Item("instancia").ToString.ToUpper & "</P>"
                                        mensaje = mensaje & "</br> Atte.<br><br>Campus Virtual - USAT.</font>"
                                        ObjMailNet.EnviarMail("campusvirtual@usat.edu.pe", "Investigaciones", dtsA.Rows(0).Item("email_Per").ToString, "Investigaciones USAT", para & mensaje, True)
                                    End If
                                End If

                            Case Else
                                'Lista de miebros de la instancia en la que se encuentra el proyecto.
                                dts = obj.ListaInvestigacionRevisores(id_investigacion, ctf)
                                For i As Integer = 0 To dts.Rows.Count - 1
                                    If dts.Rows(i).Item("email_Per") <> "" Then
                                        para = "</br><font face='Courier'>" & "Estimado(a): <b>" & dts.Rows(i).Item("nombre_revisor").ToString.ToUpper & "</b>"
                                        mensaje = "</br></br><P><ALIGN='justify'> Se le comunica que, usted es miembro " & dts.Rows(i).Item("coordinador").ToString.ToUpper & " del " & dts.Rows(i).Item("nombre_comite").ToString.ToUpper & " y tiene como pendiente de revisión la investigación " & dts.Rows(i).Item("nombre_investigacion").ToString.ToUpper & "</P>"
                                        mensaje = mensaje & "</br> Atte.<br><br>Campus Virtual - USAT.</font>"
                                        ObjMailNet.EnviarMail("campusvirtual@usat.edu.pe", "Investigaciones", dts.Rows(i).Item("email_Per").ToString, "Miembro revisor de investigaciones USAT", para & mensaje, True)
                                    End If
                                Next
                                'Autor
                                Dim dtsA As New Data.DataTable
                                dtsA = obj.ConsultaAutorInvestigacion(id_investigacion, Me.Request.QueryString("ctf"))
                                If (Request.QueryString("ctf") = 132) Then
                                    If dtsA.Rows.Count > 0 Then
                                        para = "</br><font face='Courier'>" & "Estimado(a): <b>" & dtsA.Rows(0).Item("Autor").ToString.ToUpper & "</b>"
                                        mensaje = "</br></br><P><ALIGN='justify'> Se le comunica que, su investigación " & dtsA.Rows(0).Item("titulo").ToString.ToUpper & " a sido <b> aprobada </b> y se encuentra el la instancia" & dtsA.Rows(0).Item("instancia").ToString.ToUpper & "</P>"
                                        mensaje = mensaje & "</br> Atte.<br><br>Campus Virtual - USAT.</font>"
                                        ObjMailNet.EnviarMail("campusvirtual@usat.edu.pe", "Investigaciones", dtsA.Rows(0).Item("email_Per").ToString, "Investigaciones USAT", para & mensaje, True)
                                    End If
                                End If
                        End Select
                    End If

                Case Else
                    '--------------------------------------------------------------------------------------------------------------------------'
                    ' Comunica a los miembros revisores del proyecto de investigacion, cundo el autor envia por primera vez la investigación   '
                    '--------------------------------------------------------------------------------------------------------------------------'
                    dts = obj.ListaInvestigacionRevisores(id_investigacion, ctf)
                    For i As Integer = 0 To dts.Rows.Count - 1
                        If dts.Rows(i).Item("email_Per") <> "" Then
                            para = "</br><font face='Courier'>" & "Estimado(a): <b>" & dts.Rows(i).Item("nombre_revisor").ToString.ToUpper & "</b>"
                            mensaje = "</br></br><P><ALIGN='justify'> Se le comunica que, usted es miembro " & dts.Rows(i).Item("coordinador").ToString.ToUpper & " del " & dts.Rows(i).Item("nombre_comite").ToString.ToUpper & " y tiene como pendiente de revisión la investigación " & dts.Rows(i).Item("nombre_investigacion").ToString.ToUpper & "</P>"
                            mensaje = mensaje & "</br> Atte.<br><br>Campus Virtual - USAT.</font>"
                            ObjMailNet.EnviarMail("campusvirtual@usat.edu.pe", "Investigaciones", dts.Rows(i).Item("email_Per").ToString, "Miembro revisor de investigaciones USAT", para & mensaje, True)
                        End If
                    Next
            End Select
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    'Actualizar 05.09.2012
    Protected Sub btnEnviar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEnviar.Click
        Try
            ' 132 Perfil Coordinador de comité
            ' 131 Perfil Revisor Investigacion
            ' 64  Directir de Investigacion - Vicerectorado VRPI
            If (Request.QueryString("ctf") = 132 Or Request.QueryString("ctf") = 131 Or Request.QueryString("ctf") = 64) Then
                If (validaCheckActivo() = True) Then
                    ClientScript.RegisterStartupScript(Me.GetType, "FaltanDatos", "alert('El envio fue realizado correctamente.');", True)
                    CargarInvestigaciones()
                Else
                    ClientScript.RegisterStartupScript(Me.GetType, "FaltanDatos", "alert('Favor de marcar con un check el proyecto de investigación que desea enviar.');", True)
                End If
            Else
                ClientScript.RegisterStartupScript(Me.GetType, "FaltanDatos", "alert('Usted no esta considerado dentro de los perfiles válidos para enviar investigaciones.');", True)
                Exit Sub
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    'xD
    Private Function validaCheckActivo() As Boolean
        Dim obj As New clsInvestigacion
        Dim dts As New Data.DataTable
        Dim exec As Integer = 0

        Dim Fila As GridViewRow
        Dim sw As Byte = 0

        For i As Integer = 0 To Me.gvInvestigacion.Rows.Count - 1
            'Capturamos las filas que estan activas
            Fila = gvInvestigacion.Rows(i)
            Dim valor As Boolean = CType(Fila.FindControl("chkElegir"), CheckBox).Checked
            If (valor = True) Then
                sw = 1
                obj.AbrirTransaccionCnx()
                exec = obj.CambiaInstanciaInvestigacion(gvInvestigacion.Rows(i).Cells(0).Text, Me.Request.QueryString("ctf"), Me.Request.QueryString("id"), 0)
                obj.CerrarTransaccionCnx()
                obj = Nothing
                If exec = 1 Then
                    EnviarMensaje(gvInvestigacion.Rows(i).Cells(0).Text, Request.QueryString("ctf"), "E")
                Else
                    Exit For
                    sw = 0
                    ClientScript.RegisterStartupScript(Me.GetType, "FaltanDatos", "alert('No se pudo terminar el envio, favor de volver a intentar.');", True)
                End If
            End If
        Next
        If (sw = 1) Then
            Return True
        End If
        Return False
    End Function

    Protected Sub hlVersion_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim row As GridViewRow
            Dim hlVersion As LinkButton
            hlVersion = sender
            row = hlVersion.NamingContainer
            ClientScript.RegisterStartupScript(Me.GetType, "siguientepagina", "location.href='frmRegistrarInvestigaciones.aspx?idInv=" & row.Cells.Item(0).Text & "&id=" & Request.QueryString("id").ToString & "&ctf=" & Request.QueryString("ctf") & "&tipo=V';", True)
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub
    '--------------------------------------

    Protected Sub btnObservar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnObservar.Click
        Try
            lblTitPopUp.Text = "Observar"
            hfTipoRevisor.Value = "O"

            If (validaCheckCalifica() = True) Then
                mpeRevisor.Show()
            Else
                ClientScript.RegisterStartupScript(Me.GetType, "FaltanDatos", "alert('Favor de marcar con un check sólo un proyecto de investigación para poder Observar');", True)
            End If

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Function validaCheckCalifica() As Boolean
        Try
            Dim exec As Integer = 0
            Dim Contador As Integer = 0
            Dim Fila As GridViewRow
            Dim sw As Byte = 0

            For i As Integer = 0 To Me.gvInvestigacion.Rows.Count - 1
                Fila = gvInvestigacion.Rows(i)
                Dim valor As Boolean = CType(Fila.FindControl("chkElegir"), CheckBox).Checked
                If (valor = True) Then
                    Contador = Contador + 1
                End If
            Next
            If Contador = 1 Then
                sw = 1
            End If

            If (sw = 1) Then
                Return True
            End If

            Return False
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Function

    Protected Sub btnRechazar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRechazar.Click
        Try
            lblTitPopUp.Text = "Rechazar"
            hfTipoRevisor.Value = "R"

            If (validaCheckCalifica() = True) Then
                mpeRevisor.Show()
            Else
                ClientScript.RegisterStartupScript(Me.GetType, "FaltanDatos", "alert('Favor de marcar con un check sólo un proyecto de investigación para poder Rechazar');", True)
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    'Actualizado 05.08.2012
    Protected Sub btnGuardarRevisor_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardarRevisor.Click
        Try
            '---------------------------------------------------------------------------------------------------
            '## Para Observar/Rechazar permite solo elegir un registro, debido al archivo que va adjuntar.
            '---------------------------------------------------------------------------------------------------
            Dim obj As New clsInvestigacion
            Dim dts As New Data.DataTable
            Dim exec As Integer = 0
            Dim Fila As GridViewRow
            Dim sw As Byte = 0
            Dim ruta As String

            For i As Integer = 0 To Me.gvInvestigacion.Rows.Count - 1
                Fila = gvInvestigacion.Rows(i)
                Dim valor As Boolean = CType(Fila.FindControl("chkElegir"), CheckBox).Checked
                If (valor = True) Then
                    sw = 1

                    'Sube el archivo
                    If FileArchivo.HasFile Then
                        Dim filePath As String
                        Dim archivo As String = "\" & Now.Day.ToString & Now.Month.ToString & Now.Year.ToString & Now.Hour.ToString & Now.Minute.ToString & Now.Second.ToString & System.IO.Path.GetExtension(FileArchivo.FileName).ToString
                        filePath = Server.MapPath("../../filesInvestigacion")
                        filePath = filePath & "\" & gvInvestigacion.Rows(i).Cells(0).Text
                        Dim carpeta As New System.IO.DirectoryInfo(filePath)
                        If carpeta.Exists = False Then
                            carpeta.Create()
                        End If
                        FileArchivo.PostedFile.SaveAs(filePath & archivo)
                        ruta = filePath & archivo
                        'Actualiza el estado.
                        obj.AbrirTransaccionCnx()
                        exec = obj.CalificaInvestigacion(gvInvestigacion.Rows(i).Cells(0).Text, Me.Request.QueryString("ctf"), Me.Request.QueryString("id"), hfTipoRevisor.Value, txtRevisor.Text.Trim, ruta, 0)
                        obj.CerrarTransaccionCnx()
                        obj = Nothing
                    Else
                        'Actualiza el estado.
                        obj.AbrirTransaccionCnx()
                        exec = obj.CalificaInvestigacion(gvInvestigacion.Rows(i).Cells(0).Text, Me.Request.QueryString("ctf"), Me.Request.QueryString("id"), hfTipoRevisor.Value, txtRevisor.Text.Trim, "", 0)
                        obj.CerrarTransaccionCnx()
                        obj = Nothing
                    End If
                    '------------------------------------------------------------------------------------------------------------------------'
                    'Envia email al autor de la investigacion y al  - coordinador de la Instancia en la que se encuentra la Investigacion    '
                    If exec = 1 Then
                        EnviarMensaje(gvInvestigacion.Rows(i).Cells(0).Text, Request.QueryString("ctf"), hfTipoRevisor.Value)
                    End If
                    '------------------------------------------------------------------------------------------------------------------------'
                End If
            Next
            If sw = 1 Then
                ClientScript.RegisterStartupScript(Me.GetType, "FaltanDatos", "alert('Proceso terminado correctamente.');", True)
            Else
                ClientScript.RegisterStartupScript(Me.GetType, "FaltanDatos", "alert('No se pudo terminar el proceso, favor volver a intentar.');", True)
            End If
            txtRevisor.Text = ""
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub


    Protected Sub lbEliminarAvance_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim lbEliminarAvance As LinkButton
        Dim row As GridViewRow
        Dim hf, hfA As HiddenField
        Dim objInv As New clsInvestigacion
        lbEliminarAvance = sender
        row = lbEliminarAvance.NamingContainer
        hf = row.FindControl("hfHito_id")
        hfA = row.FindControl("hfAvance_id")
        objInv.AbrirTransaccionCnx()
        objInv.EliminaAvance(hfA.Value, hfCodInvestigacion.Value, hf.Value)
        objInv.CerrarTransaccionCnx()
        CargarResumen()
    End Sub

    Protected Sub ddlVersion_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlVersion.SelectedIndexChanged
        Dim objInv As New clsInvestigacion
        Dim dt As New DataTable
        dt = objInv.ConsultaVersion(ddlVersion.SelectedValue)
        lblTitulo.Text = dt.Rows(0).Item("titulo")
        lblFechaRegistro.Text = dt.Rows(0).Item("fechaRegistro")
        lblFecIni.Text = dt.Rows(0).Item("fechaInicio")
        lblFecFin.Text = dt.Rows(0).Item("fechaFin")
        lblPresupuesto.Text = dt.Rows(0).Item("presupuesto")
        lblFinanci.Text = dt.Rows(0).Item("tipoFinanciamiento")
        lblAmbito.Text = dt.Rows(0).Item("Ambito")
        lblLinea.Text = dt.Rows(0).Item("linea")
        lblEtapa.Text = dt.Rows(0).Item("etapa")
        lblTipo.Text = dt.Rows(0).Item("tipo")
        lblInstancia.Text = dt.Rows(0).Item("instancia")
        lblBeneficiarios.Text = dt.Rows(0).Item("beneficiarios")
        lblResumen.Text = dt.Rows(0).Item("resumen")

        '---------------------------------------------------------------------
        lblEstadoEnvio.Text = dt.Rows(0).Item("enviado").ToString
        lblEstadoEnvio.Font.Bold = True
        If dt.Rows(0).Item("enviado").ToString() = "Enviado" Then
            lblEstadoEnvio.ForeColor = Drawing.Color.Blue
        Else
            lblEstadoEnvio.ForeColor = Drawing.Color.Red
        End If
        '---------------------------------------------------------------------

        'Creando la tabla Documentos
        Dim dtDocumentos As New Data.DataTable
        dtDocumentos.Columns.Add("extension", GetType(String))
        dtDocumentos.Columns.Add("nombre", GetType(String))
        dtDocumentos.Columns.Add("ruta", GetType(String))
        dtDocumentos.Columns.Add("documento", GetType(String))

        If dt.Rows(0).Item("rutaInforme") <> "" Then
            Dim myrow As Data.DataRow
            myrow = dtDocumentos.NewRow
            myrow("extension") = dt.Rows(0).Item("extInf")
            myrow("nombre") = "Informe"
            myrow("ruta") = dt.Rows(0).Item("rutaInforme")
            myrow("documento") = dt.Rows(0).Item("docInf")
            dtDocumentos.Rows.Add(myrow)
        End If
        If dt.Rows(0).Item("rutaProyecto") <> "" Then
            Dim myrow As Data.DataRow
            myrow = dtDocumentos.NewRow
            myrow("extension") = dt.Rows(0).Item("extPro")
            myrow("nombre") = "Proyecto"
            myrow("ruta") = dt.Rows(0).Item("rutaProyecto")
            myrow("documento") = dt.Rows(0).Item("docPro")
            dtDocumentos.Rows.Add(myrow)
        End If
        gvDocumentos.DataSource = dtDocumentos
        gvDocumentos.DataBind()
    End Sub

    Protected Sub lbNuevo_Hito_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        mpeDescripcion.Show()
        hfTransaccion.Value = "N"
    End Sub


    'Nuevo
    Protected Sub hlEnviarVersion_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim row As GridViewRow
            Dim hlEnviarVersion As LinkButton
            Dim obj As New clsInvestigacion
            Dim dts As New Data.DataTable

            hlEnviarVersion = sender
            row = hlEnviarVersion.NamingContainer
            EjecucionEnviosVersiones(row.Cells.Item(0).Text, Me.Request.QueryString("id"))
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    '06.08.2012 Procediemiento nuevo
    Protected Sub hlModificarVersion_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim row As GridViewRow
            Dim hlModificarVersion As LinkButton
            hlModificarVersion = sender
            row = hlModificarVersion.NamingContainer
            ClientScript.RegisterStartupScript(Me.GetType, "siguientepagina", "location.href='frmRegistrarInvestigaciones.aspx?idInv=" & row.Cells.Item(0).Text & "&id=" & Request.QueryString("id").ToString & "&ctf=" & Request.QueryString("ctf") & "&tipo=MV';", True)
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    'Nuevo
    Private Sub EjecucionEnviosVersiones(ByVal id_investigacion As Integer, ByVal codigo_per As Integer)
        Try
            Dim exec As Integer = 0
            Dim obj As New clsInvestigacion

            obj.AbrirTransaccionCnx()
            exec = obj.EjecutaEnvioInvestigacionVersiones(id_investigacion, codigo_per, 0)
            obj.CerrarTransaccionCnx()
            obj = Nothing

            If exec > 0 Then
                ClientScript.RegisterStartupScript(Me.GetType, "FaltanDatos", "alert('La nueva versión fue enviada correctamente.');", True)
                CargarInvestigaciones()
                EnviarMensaje(id_investigacion, Request.QueryString("ctf"), "V")
            Else
                ClientScript.RegisterStartupScript(Me.GetType, "FaltanDatos", "alert('Usted no es el Autor del proyecto de Investigación, no podrá realizar el envio de la nueva versión.');", True)
            End If

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Function validaCheckConforme() As Boolean
        Try
            Dim obj As New clsInvestigacion
            Dim exec As Integer = 0
            Dim Fila As GridViewRow
            Dim sw As Byte = 0

            For i As Integer = 0 To Me.gvInvestigacion.Rows.Count - 1
                Fila = gvInvestigacion.Rows(i)
                Dim valor As Boolean = CType(Fila.FindControl("chkElegir"), CheckBox).Checked
                If (valor = True) Then
                    sw = 1
                    obj.AbrirTransaccionCnx()
                    exec = obj.CalificaInvestigacion(gvInvestigacion.Rows(i).Cells(0).Text, Me.Request.QueryString("ctf"), Me.Request.QueryString("id"), "C", "", "", 0)
                    obj.CerrarTransaccionCnx()
                    obj = Nothing
                    EnviarMensaje(gvInvestigacion.Rows(i).Cells(0).Text, Request.QueryString("ctf"), "C")
                End If
            Next

            If (sw = 1) Then
                Return True
            End If

            Return False
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Function

    Protected Sub btnConforme_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnConforme.Click
        Try
            If validaCheckConforme() = True Then
                ClientScript.RegisterStartupScript(Me.GetType, "FaltanDatos", "alert('Proceso realizado correctamente.');", True)
            Else
                ClientScript.RegisterStartupScript(Me.GetType, "FaltanDatos", "alert('Marque con un Check los proyectos de investigación que desea dar Conformidad.');", True)
            End If

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub btnTipDocumento_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnTipDocumento.Click
        Try
            If Request.QueryString("ctf") = 13 Then
                lblTitPopUpInforme.Text = "Subir Informe"
                hfTipoInforme.Value = "IF"
                mpeInforme.Show()
            End If
            If Request.QueryString("ctf") = 64 Then
                lblTitPopUpResolucion.Text = "Resolución"
                hfTipoResolucion.Value = "RS"
                mpeResolucion.Show()
            End If
            
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub btnGuardarInforme_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardarInforme.Click
        Try
            'hfCodInvestigacion.Value 
            'Sube el archivo

            Dim obj As New clsInvestigacion
            Dim dts As New Data.DataTable
            Dim exec As Integer = 0
            Dim sw As Byte = 0
            Dim ruta As String

            'Sube el archivo
            If FileArchivoInforme.HasFile Then
                Dim filePath As String
                Dim archivo As String = "\" & Now.Day.ToString & Now.Month.ToString & Now.Year.ToString & Now.Hour.ToString & Now.Minute.ToString & Now.Second.ToString & System.IO.Path.GetExtension(FileArchivo.FileName).ToString
                filePath = Server.MapPath("../../filesInvestigacion")
                filePath = filePath & "\" & hfCodInvestigacion.Value.ToString
                Dim carpeta As New System.IO.DirectoryInfo(filePath)
                If carpeta.Exists = False Then
                    carpeta.Create()
                End If
                FileArchivoInforme.PostedFile.SaveAs(filePath & archivo)
                ruta = filePath & archivo
                'Actualiza el estado.
                obj.AbrirTransaccionCnx()
                exec = obj.ActualizarInforme(hfCodInvestigacion.Value, ruta, 0)
                obj.CerrarTransaccionCnx()
                obj = Nothing
            End If

            If exec = 1 Then
                ClientScript.RegisterStartupScript(Me.GetType, "FaltanDatos", "alert('Se cargo en informe al proyecto de investigación');", True)
            Else
                ClientScript.RegisterStartupScript(Me.GetType, "FaltanDatos", "alert('No se pudo cargar el informe al proyecto de investigación, intentelo nuevamente.');", True)
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub btnGuardarResolucion_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardarResolucion.Click
        Try
            'Guarda nro resolucion y archivo.

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

End Class
