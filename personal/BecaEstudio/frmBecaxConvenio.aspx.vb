
Partial Class BecaEstudio_frmBecaxConvenio
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                CargarCombos()
                CargarRegistros()
                EstadoConvenio()
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub CargarRegistros()
        Try
            Dim obj As New ClsConectarDatos
            Dim tb As New Data.DataTable
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            tb = obj.TraerDataTable("Beca_ListaBecaSolicitud", Me.ddlCiclo.SelectedValue)
            'Response.Write(tb.Rows.Count)
            If tb.Rows.Count > 0 Then
                Me.gvListaBecas.DataSource = tb
                Me.DataBind()
            End If
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

        Me.ddlCiclo.DataSource = tb
        Me.ddlCiclo.DataTextField = "descripcion_cac"
        Me.ddlCiclo.DataValueField = "codigo_cac"
        Me.ddlCiclo.DataBind()
        If Session("Beca_codigo_cac") IsNot Nothing Then
            Me.ddlCiclo.SelectedValue = Session("codigo_cac")
        End If

        tb = obj.TraerDataTable("BECA_ListaTipoConvenio")
        Me.ddlConvenio.DataSource = tb
        Me.ddlConvenio.DataTextField = "descripcion_Mno"
        Me.ddlConvenio.DataValueField = "codigo_Mno"
        Me.ddlConvenio.DataBind()

        tb = obj.TraerDataTable("Beca_ListaCobertura")
        Me.ddlCobertura.DataSource = tb
        Me.ddlCobertura.DataTextField = "porcentaje_bco"
        Me.ddlCobertura.DataValueField = "codigo_bco"
        Me.ddlCobertura.DataBind()

        tb = obj.TraerDataTable("Beca_ListaTipoBecaConvenio")
        Me.ddlTipoBeca.DataSource = tb
        Me.ddlTipoBeca.DataTextField = "descripcion_bec"
        Me.ddlTipoBeca.DataValueField = "codigo_bec"
        Me.ddlTipoBeca.DataBind()

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
                btnAgregar.Enabled = True
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
            dt = obj.TraerDataTable("Beca_ListaBuscarAlumnos", txtBuscarAlumno.Text.Trim, ddlTipo.SelectedValue)
            obj.CerrarConexion()

            If dt.Rows.Count > 0 Then
                gvLista.DataSource = dt
                gvLista.DataBind()
            Else
                gvLista.DataSource = Nothing
                gvLista.DataBind()
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub btnBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBuscar.Click
        Try
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
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub btnEnviar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEnviar.Click
        Try
            pnlBusqueda.Visible = False
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

                '::: Llamamos a ver si cumple con los requisitos :::
                Requisitos()
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub


    Private Sub Requisitos()
        Try
            Dim obj As New ClsConectarDatos
            Dim dt As Data.DataTable
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            dt = obj.TraerDataTable("Beca_ConsultarRequisitoAlumno", Me.HiddenField.Value)
            obj.CerrarConexion()

            'Response.Write(dt.Rows.Count)


            Dim htmlTb As String = ""
            Dim img As String = ""
            Dim pass As Integer = 1
            Dim f As Byte = 0
            If dt.Rows.Count Then
                For i As Integer = 0 To dt.Rows.Count - 1
                    htmlTb = htmlTb & "<tr class=""row-" & f.ToString & """> "
                    htmlTb = htmlTb & "<td class=""cell cell2"">" & dt.Rows(i).Item("descripcion_req") & "</td>"
                    htmlTb = htmlTb & "<td class=""cell cell3"">" & IIf(dt.Rows(i).Item("valord_bsr").ToString = "1", "Sí", dt.Rows(i).Item("valord_bsr").ToString) & "</td>"
                    htmlTb = htmlTb & "<td class=""cell cell4"">" & dt.Rows(i).Item("valora_bsr").ToString & "</td>"
                    img = IIf(dt.Rows(i).Item("cumple_bsr").ToString = "1", "check.png", "exclamation.png")
                    htmlTb = htmlTb & "<td class=""cell cell5""><img src=""images/" & img & """</td>"
                    htmlTb = htmlTb & "</tr>"
                    pass = pass * dt.Rows(i).Item("cumple_bsr")
                Next                
            End If
            tb.InnerHtml = htmlTb
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub btnAgregar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAgregar.Click
        Try
            Dim obj As New ClsConectarDatos
            Dim tb As New Data.DataTable
            Dim valoresdevueltos(1) As Integer
            Dim intConvenio As Integer = 0

            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()

            If ddlConvenio.Visible = True Then
                intConvenio = ddlConvenio.SelectedValue
            End If


            'Aqui las validadciones'
            '==================================
            '::Insercion ::'
            tb = obj.TraerDataTable("Beca_RegistraSolictudBecaPorConvenio", _
                                    Me.ddlTipoBeca.SelectedValue, _
                                    intConvenio, _
                                    Me.HiddenField.Value, _
                                    ddlCobertura.SelectedValue, _
                                    ddlCiclo.SelectedValue, CInt(Request.QueryString("id")))

            If tb.Rows(0).Item("rpta") = -1 Then
                Me.ClientScript.RegisterStartupScript(Me.GetType, "Faltan Datos", "alert('Ocurrio un error al tratar de registrar la solicitud.');", True)
                Exit Sub
            Else
                If tb.Rows(0).Item("rpta") = 999999 Then
                    Me.ClientScript.RegisterStartupScript(Me.GetType, "Faltan Datos", "alert('El alumno ya fue asignado a una beca, favor de verificar.');", True)
                Else
                    Me.ClientScript.RegisterStartupScript(Me.GetType, "Faltan Datos", "alert('La solicitud fue registrada correctamente.');", True)
                End If
            End If
            ':: Listamos todas los registrso ::
            CargarRegistros()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub gvListaBecas_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles gvListaBecas.RowDeleting        
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()        
        obj.Ejecutar("BECA_RechazaSolicitud", Me.gvListaBecas.DataKeys.Item(e.RowIndex).Values(0))
        obj.CerrarConexion()
        obj = Nothing

        CargarRegistros()
    End Sub

    Protected Sub ddlTipoBeca_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlTipoBeca.SelectedIndexChanged
        Try
            EstadoConvenio()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub EstadoConvenio()
        Try
            If ddlTipoBeca.SelectedValue = 4 Or ddlTipoBeca.SelectedValue = 8 Or ddlTipoBeca.SelectedValue = 9 Then
                Me.lblConvenio.Visible = False
                Me.ddlConvenio.Visible = False
            Else
                Me.lblConvenio.Visible = True
                Me.ddlConvenio.Visible = True
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub gvListaBecas_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvListaBecas.SelectedIndexChanged

    End Sub

    Protected Sub ddlCiclo_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlCiclo.SelectedIndexChanged
        CargarRegistros()
    End Sub
End Class
