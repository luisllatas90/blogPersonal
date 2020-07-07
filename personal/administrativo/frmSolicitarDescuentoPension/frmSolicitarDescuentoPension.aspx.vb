Partial Class SolicitarDescuentoPension
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then

                'If verificarTipoPersonal() Then
                'CargarCombos()
                CargarRegistros()
                ' EstadoConvenio()
                pnlBusqueda.Visible = True
                'pnlPrincipal.Visible = False
                gvLista.SelectedIndex = -1
                Me.txtBuscarAlumno.Text = ""

                If HiddenField.Value = 0 Then
                    btnEnviar.Visible = False

                Else
                    btnEnviar.Visible = True
                End If

                Me.panelTipoPersonal.Visible = False
                'Else
                '    Me.panelTipoPersonal.Visible = True
                'End If
                Me.txtBuscarAlumno.Focus()
            End If
            pnlPrincipal.Visible = True
            pnlPregunta.Visible = False
            '  If Not verificarCronograma() Then
            ' Me.btnBuscar.Enabled = False
            'Me.txtBuscarAlumno.Enabled = False
            ' End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Function verificarCronograma() As Boolean

        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable

        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        dt = obj.TraerDataTable("Beca_ConsultarCronogramaSolicitud")
        obj.CerrarConexion()

        Me.LblCronograma.Text = "Solicitud de Becas disponible desde " & dt.Rows(0).Item("desde") & " al " & dt.Rows(0).Item("hasta")

        If Request.QueryString("ctf") = "19" Or Request.QueryString("ctf") = "1" Then
            Return True
        End If
        If dt.Rows(0).Item("estado") = "N" Then
            Return False
        Else
            Return True
        End If


    End Function

    Private Function verificarTipoPersonal() As Boolean
        Try
            Dim obj As New ClsConectarDatos
            Dim tb As New Data.DataTable
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            tb = obj.TraerDataTable("Beca_TipoPersonal", CInt(Request.QueryString("id")))
            If tb.Rows.Count > 0 Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Function

    Private Sub CargarRegistros()
        Try
            Dim obj As New ClsConectarDatos
            Dim tb As New Data.DataTable
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            tb = obj.TraerDataTable("PEN_descuentoPensionListar", 0)
            If tb.Rows.Count > 0 Then
                Me.gvListaBecas.DataSource = tb
                btnExportar.Visible = True
            Else
                Me.gvListaBecas.DataSource = Nothing
                btnExportar.Visible = False
            End If
            Me.DataBind()

            'If Not Request.QueryString("ctf") = "19" Or Request.QueryString("ctf") = "1" Then
            '    Me.DataBind()
            'End If

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Sub CargarCombos()
        Dim obj As New ClsConectarDatos
        Dim tb As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        tb = obj.TraerDataTable("Beca_ConsultarCicloAcademico")

        obj.CerrarConexion()
        obj = Nothing
    End Sub

    Protected Sub lbkBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbkBuscar.Click
        Try
            pnlBusqueda.Visible = True
            pnlPrincipal.Visible = False
            gvLista.SelectedIndex = -1
            Me.txtBuscarAlumno.Text = ""

            If HiddenField.Value = 0 Then
                btnEnviar.Visible = False

            Else
                btnEnviar.Visible = True
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub BuscarAlumno()
        Try



            Dim obj As New ClsConectarDatos
            Dim dt As Data.DataTable
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            ' Si es direc.pensiones busca cualquier alumno
            ' If Request.QueryString("ctf") = "19" Or Request.QueryString("ctf") = "1" Then
            'dt = obj.TraerDataTable("[Beca_ListaBuscarAlumnos]", txtBuscarAlumno.Text.Trim, ddlTipo.SelectedValue)
            'Else
            dt = obj.TraerDataTable("[PEN_BuscarAlumnosSolDscto]", ddlTipo.SelectedValue, txtBuscarAlumno.Text.Trim, 2, 0)
            'End If

            obj.CerrarConexion()

            If dt.Rows.Count > 0 Then
                gvLista.DataSource = dt
                gvLista.DataBind()
                Me.Label1.Visible = True
                lblbuscar.Visible = False
            Else
                gvLista.DataSource = Nothing
                gvLista.DataBind()
                lblbuscar.Visible = True
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub btnBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBuscar.Click
        Try
            'Me.btnAgregar.Enabled = True


            Me.gvLista.Visible = True
            pnlPrincipal.Visible = True
            Me.pnlDatosAlumno.Visible = False




            BuscarAlumno()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub gvLista_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvLista.RowDataBound
        Try
            Try
                If e.Row.RowType = DataControlRowType.DataRow Then
                    e.Row.Attributes.Add("OnMouseOver", "Resaltar(1,this,'S')")
                    e.Row.Attributes.Add("OnMouseOut", "Resaltar(0,this,'S')")
                    e.Row.Attributes.Add("OnClick", "javascript:__doPostBack('gvLista','Select$" & e.Row.RowIndex & "');")
                    e.Row.Style.Add("cursor", "hand")
                End If
            Catch ex As Exception
                Response.Write(ex.Message)
            End Try
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub gvLista_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvLista.SelectedIndexChanged
        Try
            HiddenField.Value = gvLista.SelectedRow.Cells(0).Text
            'Response.Write(HiddenField.Value)
            If HiddenField.Value > 0 Then
                btnEnviar.Visible = True
                'btnEnviar.Text = "  Enviar"
                'btnEnviar.CssClass = "enviarvac"
                gvLista.SelectedRow.Cells(1).ForeColor = Drawing.Color.Blue
                'Me.lblInstrucciones.Text = "Ud. desea generar una vacante para el trabajador => <b><font color='blue'>" & gvVacantes.SelectedRow.Cells(1).Text & "</font></b><br>Pulse el botón <b><u>Enviar</u><b>."
                btnEnviar.Visible = True
            Else
                btnEnviar.Visible = False
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub btnEnviar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEnviar.Click
        Try
            'pnlBusqueda.Visible = False
            Me.gvLista.Visible = False
            pnlPrincipal.Visible = True
            Me.pnlDatosAlumno.Visible = True
            gvLista.SelectedIndex = -1
            If HiddenField.Value > 0 Then
                DatosAlumno()
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub DatosAlumno()
        Try
            Dim obj As New ClsConectarDatos
            Dim tb As New Data.DataTable
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            tb = obj.TraerDataTable("Beca_DatosAlumno", Me.HiddenField.Value, "D")
            If tb.Rows.Count > 0 Then
                lblAlumno.Text = tb.Rows(0).Item("alumno").ToString
                lblCodigoUniv.Text = tb.Rows(0).Item("codigoUniver_Alu").ToString
                lblcicloingreso.Text = tb.Rows(0).Item("cicloIng_Alu").ToString
                lblEscuelaprofesional.Text = tb.Rows(0).Item("nombre_Cpf").ToString
                lblPlanEstudios.Text = tb.Rows(0).Item("descripcion_Pes").ToString

                Label1.Visible = False
            End If

            obj.CerrarConexion()

            If tb.Rows.Count > 0 Then
                DatosPension()
                GenerarTotales()
            End If

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub DatosPension()
        'gvPensiones
        Dim obj As New ClsConectarDatos
        Dim tb As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        tb = obj.TraerDataTable("PEN_CalculaAdelantoPensionV2", Me.HiddenField.Value)
        obj.CerrarConexion()

        If tb.Rows.Count > 0 Then
            btnAgregar.Enabled = True
        Else
            btnAgregar.Enabled = False
        End If

        gvPensiones.DataSource = tb
        gvPensiones.DataBind()

        tb = Nothing
    End Sub

    Protected Sub btnAgregar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAgregar.Click
        Try
            pnlPrincipal.Visible = False
            pnlPregunta.Visible = True
            Me.spalumno.InnerHtml = lblAlumno.Text.ToString
            lblCostoSinDscto.Text = Me.txttotalsindcto.Text
            lblCostoConDscto.Text = Me.txttotalcondcto.Text
            lblBeneficio.Text = Me.txttotal.Text

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub GenerarTotales()
        Try

            Dim i As Integer = 0
            Dim n As Integer = 0
            Dim total As Double = 0.0
            Dim descuento As Double = 0.0
            n = gvPensiones.Rows.Count - 1


            For i = 0 To n
                'Response.Write(CDbl(gvPensiones.Rows(i).Cells(4).Text) & "<br>")
                descuento = descuento + CDbl(gvPensiones.Rows(i).Cells(5).Text)
                total = total + CDbl(gvPensiones.Rows(i).Cells(4).Text)

            Next


            txttotalsindcto.Text = total
            txttotalcondcto.Text = descuento
            txttotal.Text = CDbl(total) - CDbl(descuento)




        Catch ex As Exception

        End Try
    End Sub

    Private Sub GenerarPensionDescuento()
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        obj.IniciarTransaccion()
        Dim resul As Boolean

        Try

            Dim i As Integer = 0
            Dim n As Integer = 0
            Dim tb As New Data.DataTable
            n = gvPensiones.Rows.Count - 1
          
            For i = 0 To n

                With gvPensiones.Rows(i)
                    If CDbl(.Cells(5).Text) > 0 Then


                        ' Response.Write(Me.HiddenField.Value & "," & .Cells(0).Text & "," & .Cells(1).Text & "," & .Cells(5).Text & "," & .Cells(3).Text & "<br>")
                        'tb = obj.TraerDataTable("PEN_GenerarDeudaPensionDescuento", Me.HiddenField.Value, .Cells(0).Text, .Cells(1).Text, CDbl(.Cells(5).Text), .Cells(3).Text, CInt(Request.QueryString("id")))
                        resul = obj.Ejecutar("PEN_GenerarDeudaPensionDescuento", Me.HiddenField.Value, .Cells(0).Text, .Cells(1).Text, CDbl(.Cells(5).Text), .Cells(3).Text, CInt(Request.QueryString("id")))

                        'Response.Write(i & ": " & resul)

                        If resul = False Then
                            Response.Write("Ocurrió un problema al procesar descuento, comunicarse con la oficina de sistemas")
                            obj.AbortarTransaccion()
                            obj.CerrarConexion()
                            Exit Sub
                        End If

                    End If
                End With

            Next
            obj.TerminarTransaccion()
            obj.CerrarConexion()
            tb = Nothing

        Catch ex As Exception
            obj.AbortarTransaccion()
            obj.CerrarConexion()
            Response.Write(ex.Message & "  " & ex.StackTrace)
        End Try
    End Sub

    Protected Sub btnNo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNo.Click
        pnlPrincipal.Visible = True
        pnlPregunta.Visible = False
    End Sub

    Protected Sub btnSi_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSi.Click
        Try

        
            Dim obj As New ClsConectarDatos
            Dim tb As New Data.DataTable
            Dim valoresdevueltos(1) As Integer
            Dim intConvenio As Integer = 0
            'Dim pagina As String
            'pagina = "frmBecaxPersonal.aspx?id=" & Request.QueryString("id") & "&ctf=" & Request.QueryString("ctf")
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()

            tb = obj.TraerDataTable("PEN_descuentoPension", Me.HiddenField.Value)
            obj.CerrarConexion()
            If tb.Rows(0).Item("rpta") = 0 Then
                Me.ClientScript.RegisterStartupScript(Me.GetType, "Faltan Datos", "alert('Ocurrio un error al tratar de registrar la solicitud.');", True)
                Exit Sub
            Else

                GenerarPensionDescuento()
                CargarRegistros()
                pnlBusqueda.Visible = True
                Me.pnlDatosAlumno.Visible = False
                Me.btnAgregar.Enabled = False

                Me.spalumno.InnerHtml = ""
                lblCostoSinDscto.Text = ""
                lblCostoConDscto.Text = ""
                lblBeneficio.Text = Me.txttotal.Text
                pnlPrincipal.Visible = True
                pnlPregunta.Visible = False
                gvPensiones.DataSource = Nothing
                gvPensiones.DataBind()
                Me.btnEnviar.Visible = False

            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub btnExportar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExportar.Click
        Dim sb As StringBuilder = New StringBuilder()
        Dim SW As System.IO.StringWriter = New System.IO.StringWriter(sb)
        Dim htw As HtmlTextWriter = New HtmlTextWriter(SW)
        Dim Page As Page = New Page()
        Dim form As HtmlForm = New HtmlForm()
        Me.gvListaBecas.EnableViewState = False
        Page.EnableEventValidation = False
        Page.DesignerInitialize()
        Page.Controls.Add(form)
        form.Controls.Add(Me.gvListaBecas)
        Page.RenderControl(htw)
        Response.Clear()
        Response.Buffer = True
        Response.ContentType = "application/vnd.ms-excel"
        Response.AddHeader("Content-Disposition", "attachment;filename=PagoAnticipadoCiclo.xls")
        'Response.Charset = "UTF-8"
        'Response.ContentEncoding = Encoding.Default        
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Response.Write(sb.ToString())
        Response.End()
    End Sub

    Protected Sub gvListaBecas_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles gvListaBecas.RowDeleting

        Try
            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            obj.Ejecutar("PEN_AnularPensionDescuento", Me.gvListaBecas.DataKeys.Item(e.RowIndex).Values(0))
            obj.CerrarConexion()
            obj = Nothing
            CargarRegistros()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub
End Class
