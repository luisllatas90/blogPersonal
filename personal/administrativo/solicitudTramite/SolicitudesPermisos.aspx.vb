Partial Class _SolicitudesPermisos
    Inherits System.Web.UI.Page
    Dim fecha_busca As Date
    Dim estado, trabajador As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If (Session("id_per") Is Nothing) Then

            Response.Redirect("../../../sinacceso.html")

        End If

        If Not IsPostBack Then

            'Me.txtFecha.Text = Date.Today
            'Me.txtFecha.Text = "15/09/2018"
            Me.lblDia.Text = Date.Today

            Me.lblMensaje.Visible = False 'Se añadió

            ConsultaListaPermisos()
            Me.lblLista.Text = "L"

            Me.divConfirmaRegistroRetorno.Visible = False
            Me.divConfirmaRegistroSalida.Visible = False
        End If

    End Sub

    Private Sub ConsultaListaPermisos()

        Me.gvCarga.DataSource = Nothing
        Me.gvCarga.DataBind()
        'Me.celdaGrid.Visible = True
        'Me.celdaGrid.InnerHtml = ""

        'fecha_busca = FormatDateTime(Me.txtFecha.Text, DateFormat.ShortDate)
        fecha_busca = CDate(Me.lblDia.Text)
        estado = Me.ddlEstado.SelectedValue

        Try
            Dim dt As New Data.DataTable
            Dim obj As New ClsConectarDatos

            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            dt = obj.TraerDataTable("ListaSolicitudTramiteVigilancia", fecha_busca, estado)

            If dt.Rows.Count > 0 Then
                Me.gvCarga.DataSource = dt
                Me.gvCarga.DataBind()
            Else
                Me.gvCarga.DataSource = Nothing
                Me.gvCarga.DataBind()
                'Me.celdaGrid.Visible = True
                'Me.celdaGrid.InnerHtml = "** AVISO :  NO EXISTEN PERMISOS CON LOS PARÁMETROS SELECCIONADOS"
                Me.lblMensaje.Visible = True
                Me.lblMensaje.Text = "** AVISO :  No Existen Permisos por Horas con los Parámetros seleccionados"

            End If

            obj.CerrarConexion()

            'Me.gvCarga.DataSource = Nothing
            'Me.gvCarga.DataBind()
            'Me.celdaGrid.Visible = True

        Catch ex As Exception
            Me.lblMensaje.Text = ex.Message & " - " & ex.StackTrace '"Error al consultar.."
        End Try


    End Sub

    Protected Sub ddlEstado_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlEstado.SelectedIndexChanged

        ConsultaListaPermisos()
        Me.lblLista.Text = "L"
        Me.txtPersonal.Text = ""

    End Sub

    Protected Sub gvCarga_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvCarga.RowDataBound

        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim fila As Data.DataRowView
            fila = e.Row.DataItem

            'Dim chk As CheckBox = CType(e.Row.FindControl("CheckBox1"), CheckBox)
            'chk.Visible = False
            'e.Row.Cells(1).Text = e.Row.RowIndex + 1

            Dim cod As Integer
            cod = fila.Row("codigo_ST")
            Dim valor As Integer

            If e.Row.Cells(3).Text = "Urgente" Then
                e.Row.Cells(3).Font.Bold = False
                e.Row.Cells(3).ForeColor = System.Drawing.Color.Blue
            End If

            If fila.Row("Hora_IniAu") = "" Then
                e.Row.Cells(6).Text = "" 'Check Verde
                'e.Row.Cells(11).Text = "" 'Desactiva el segundo Botón
            Else
                e.Row.Cells(7).Text = "" 'EL Botón
            End If

            If fila.Row("Hora_FinAu") = "" Then
                e.Row.Cells(10).Text = "" 'Check Verde en blanco
            Else
                If fila.Row("Hora_IniAu") = "" Then
                    e.Row.Cells(5).BackColor = Drawing.Color.LightCoral
                    e.Row.Cells(7).Text = "" 'EL Botón
                End If
                e.Row.Cells(11).Text = "" 'EL Botón
            End If

                'e.Row.Cells(10).Text = IIf(fila.Row("Hora_FinAu") = "", "", "1") 'Asigno valor 2 a columna v
                'e.Row.Cells(11).Attributes("ImageUrl") = IIf(fila.Row("Hora_FinAu") <> "", "../../images/check.gif", "../../images/check - azul.gif")
                'CType(e.Row.FindControl("ImgEstado"), Image).Attributes("ImageUrl") = "../../images/CV.gif"
                'e.Row.Cells(6).Text = IIf(fila.Row("Hora_IniAu") = "", "", "1") 'Asigno valor 2 a columna w
                'e.Row.Cells(7).Attributes("ImageUrl") = IIf(e.Row.Cells(6).Text = 2, "../../images/check.gif", "../../images/check - azul.gif")

                'CType(e.Row.FindControl("ImgEstado"), Image).Attributes.Add("OnClick", "Cambia_Check(this.parentNode.parentNode,2)")
                'If e.Row.Cells(5).Text = "" And e.Row.Cells(9).Text = "" Then
                'e.Row.Cells(12).Attributes("ImageUrl") = "../../images/CZ.gif"
                'End If

                If fila.Row("Hora_IniAu") = "" And fila.Row("Hora_FinAu") = "" Then
                    'CType(e.Row.FindControl("ImgEstado"), Image).Attributes.Add("ImageUrl", "../../images/CR.gif")
                    'e.Row.Cells(0).Controls.Clear()
                    valor = 1
                    e.Row.Cells(12).BackColor = System.Drawing.Color.Blue
                    Dim imagen As System.Web.UI.WebControls.Image = DirectCast(e.Row.FindControl("ImgEstado"), System.Web.UI.WebControls.Image)
                    imagen.ImageUrl = "../../images/flecha-ingreso.jpg" 'Color azul
                    'imagen.ImageUrl = "../../images/CZ.gif"

                ElseIf fila.Row("Hora_IniAu") <> "" And fila.Row("Hora_FinAu") = "" Then
                    'e.Row.Cells(12).Attributes.Add("ImageUrl", "../../images/CR.gif") 'Cuadro Rojo
                    valor = 2
                    e.Row.Cells(12).BackColor = System.Drawing.Color.Red
                    Dim imagen As System.Web.UI.WebControls.Image = DirectCast(e.Row.FindControl("ImgEstado"), System.Web.UI.WebControls.Image)
                    imagen.ImageUrl = "../../images/candadoabierto_1.png"
                    'imagen.ImageUrl = "../../images/CR.gif"

                ElseIf fila.Row("Hora_IniAu") <> "" And fila.Row("Hora_FinAu") <> "" Then
                    valor = 3
                    e.Row.Cells(12).BackColor = System.Drawing.Color.Green
                    Dim imagen As System.Web.UI.WebControls.Image = DirectCast(e.Row.FindControl("ImgEstado"), System.Web.UI.WebControls.Image)
                    imagen.ImageUrl = "../../images/candadocerrado_2.png"

                ElseIf fila.Row("Hora_IniAu") = "" And fila.Row("Hora_FinAu") <> "" Then
                    valor = 3
                    e.Row.Cells(12).BackColor = System.Drawing.Color.Green
                    Dim imagen As System.Web.UI.WebControls.Image = DirectCast(e.Row.FindControl("ImgEstado"), System.Web.UI.WebControls.Image)
                    imagen.ImageUrl = "../../images/candadocerrado_2.png"

                End If

                'e.Row.Cells(11).Text = IIf(fila.Row("Estado") <> "Generado", "<a href='SolicitudTramite.aspx?cod=" + cod + "'>" + e.Row.Cells(11).Text + "</a>", e.Row.Cells(11).Text)

        End If
    End Sub

    Protected Sub gvCarga_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvCarga.RowCommand

        Try
            'Dim mensaje As String
            Dim index As Integer = Convert.ToInt32(e.CommandArgument)
            If (e.CommandName = "registra_salida") Then

                'Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
                
                Me.divListado.Visible = False
                Me.divConfirmaRegistroSalida.Visible = True

                Me.lblCod_ST.Text = gvCarga.DataKeys(index).Values("codigo_ST")
                Me.lblTrabajador.Text = gvCarga.DataKeys(index).Values("Colaborador")
                'obj = Nothing
                'index.Cancel = True

            ElseIf (e.CommandName = "registra_retorno") Then

                Me.divListado.Visible = False
                Me.divConfirmaRegistroRetorno.Visible = True

                Me.lblCod_ST.Text = gvCarga.DataKeys(index).Values("codigo_ST")
                Me.lblTrabajador1.Text = gvCarga.DataKeys(index).Values("Colaborador")

            End If

            'If gvCarga.Rows(index).Cells(6).Text = "2" Then
            '    gvCarga.Rows(index).Cells(7).Attributes("ImageUrl") = "../../images/check.gif"
            'End If

            'If gvCarga.Rows(index).Cells(5).Text = "" And gvCarga.Rows(index).Cells(9).Text = "" Then
            '    CType(gvCarga.Rows(index).FindControl("ImgEstado"), Image).Attributes.Add("ImageUrl", "../../images/CV.gif")
            'End If
            'If gvCarga.Rows(index).Cells(5).Text <> "" And gvCarga.Rows(index).Cells(9).Text = "" Then
            '    'CType(e.Row.FindControl("ImgEstado"), Image).Attributes("ImageUrl") = "../../images/CR.gif"
            '    CType(gvCarga.Rows(index).FindControl("ImgEstado"), Image).Attributes.Add("ImageUrl", "../../images/CR.gif")
            'End If

        Catch ex As Exception
            Response.Write(ex.Message & " - " & ex.StackTrace)
        End Try

        'hddMatriculados.Value = Me.grwGruposProgramados.DataKeys(Convert.ToInt32(e.CommandArgument)).Value
        If Me.lblLista.Text = "B" Then
            Busqueda()
        Else
            ConsultaListaPermisos()
        End If

    End Sub

    Private Sub Busqueda()

        If Trim(Me.txtPersonal.Text) = "" Then
            Me.lblMensaje0.Text = "** Aviso: No ha ingresado el APELLIDO o NOMBRE del trabajador"
            ConsultaListaPermisos()
            Exit Sub
        End If

        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()

        Dim tb As New Data.DataTable
        tb = obj.TraerDataTable("BuscaPermisoVigilancia", CDate(Me.lblDia.Text), Trim(Me.txtPersonal.Text))

        If tb.Rows.Count Then
            Me.gvCarga.DataSource = tb
            Me.lblLista.Text = "B" 'De búsqueda
        Else
            Me.gvCarga.DataSource = Nothing
            'Me.celdaGrid.InnerHtml = "** AVISO :  No Existen PERMISOS con los PARÁMETROS seleccionados"
            Me.lblMensaje.Visible = True
            Me.lblMensaje.Text = "** AVISO :  No Existen Solicitudes con los Parámetros seleccionados"
            Me.lblLista.Text = "L" 'Lista general
        End If

        Me.gvCarga.DataBind()
        obj.CerrarConexion()
        obj = Nothing

    End Sub

    Protected Sub Unnamed1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnBuscar.Click, btnBuscar.Click

        Busqueda()

    End Sub

    Protected Sub Unnamed3_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnActualizar.Click, btnActualizar.Click

        Me.txtPersonal.Text = "" 'Borra el buscar por Nombre o apellido
        ConsultaListaPermisos()
        Me.lblLista.Text = "L"
        Me.lblMensaje0.Text = ""
    End Sub

    Protected Sub Unnamed2_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnLimpiar.Click
        Me.txtPersonal.Text = ""
        Me.ddlEstado.SelectedValue = "TO"
        ConsultaListaPermisos()
        Me.lblLista.Text = "L"
        Me.lblMensaje0.Text = ""
    End Sub

    Protected Sub btnConfirmaSalidaNO_Cick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnConfirmaSalidaNO.Click
        Me.divListado.Visible = True
        Me.divConfirmaRegistroSalida.Visible = False
        ConsultaListaPermisos()
        Me.lblMensaje0.Text = "** Nota : Operación Cancelada"

    End Sub

    Protected Sub btnConfirmaSalidaSI_Cick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnConfirmaSalidaSI.Click
        Me.divListado.Visible = True
        Me.divConfirmaRegistroSalida.Visible = False
        parteSalida()
        ConsultaListaPermisos()
    End Sub

    Private Sub parteSalida()

        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        obj.Ejecutar("SolicitudPermisoVigilancia", Me.lblCod_ST.Text, "S")
        obj.CerrarConexion()

    End Sub

    Protected Sub btnConfirmaRetornoNO_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnConfirmaRetornoNO.Click
        Me.divListado.Visible = True
        Me.divConfirmaRegistroRetorno.Visible = False
        ConsultaListaPermisos()
        Me.lblMensaje0.Text = "** Nota : Operación Cancelada"

    End Sub

    Protected Sub btnConfirmaRetornoSI_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnConfirmaRetornoSI.Click
        Me.divListado.Visible = True
        Me.divConfirmaRegistroRetorno.Visible = False
        parteRetorno()
        ConsultaListaPermisos()
    End Sub

    Private Sub parteRetorno()

        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        obj.Ejecutar("SolicitudPermisoVigilancia", Me.lblCod_ST.Text, "R")
        obj.CerrarConexion()

    End Sub

End Class

