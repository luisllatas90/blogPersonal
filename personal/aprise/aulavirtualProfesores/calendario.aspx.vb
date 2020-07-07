
Partial Class librerianet_aulavirtual_calendario
    Inherits System.Web.UI.Page
    Protected tblActividades As Data.DataTable
    Protected Sub Calendar1_DayRender(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DayRenderEventArgs) Handles Calendar1.DayRender
        'e.Cell.VerticalAlign = VerticalAlign.Top
        If Not tblActividades Is Nothing Then
            For i As Integer = 0 To tblActividades.Rows.Count - 1
                If e.Day.DayNumberText = Day(tblActividades.Rows(i).Item("fechainicio")) And _
                    e.Day.Date.Month = Month(tblActividades.Rows(i).Item("fechainicio")) And _
                    e.Day.Date.Year = Year(tblActividades.Rows(i).Item("fechainicio")) Then
                    'e.Cell.Controls.Add(New LiteralControl("<br />" & Mid(tblActividades.Rows(i).Item("descripcion").ToString, 1, 15) & ".."))
                    e.Cell.BorderColor = Drawing.Color.Red
                    e.Cell.BorderStyle = BorderStyle.Solid
                    e.Cell.BackColor = Drawing.Color.Orange
                    e.Cell.ForeColor = Drawing.Color.White
                    e.Cell.BorderWidth = 1
                    e.Cell.ToolTip = "Actividades registradas"
                    'e.Cell.Attributes.Add("onClick", "alert('**********ASESORÍA DE TESIS********** \nTema: Definir título de investigación \nLugar: Oficina\nHora: 03:00 p.m.');return(false)")
                End If
            Next
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            Me.hdIdcursoVirtual.Value = request.querystring("idcursovirtual")
            Me.hdIdUsuario.Value = request.querystring("idusuario")
            Me.hdIdAgenda.Value = 0
            Me.hdcodigo_tfu.Value = request.querystring("codigo_tfu")

            Me.txtFechaInicio.Attributes.Add("OnKeyDown", "return false")
            Me.txtFechaFin.Attributes.Add("OnKeyDown", "return false")

            Me.cmdAgregar.Visible = Me.hdcodigo_tfu.Value = 1

            Me.CargarActividadesMensuales(Month(Now), Year(Now))
        End If
    End Sub

    Protected Sub Calendar1_VisibleMonthChanged(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.MonthChangedEventArgs) Handles Calendar1.VisibleMonthChanged
        'Response.Write(e.NewDate.Month.ToString)
        CargarActividadesMensuales(e.NewDate.Month.ToString, e.NewDate.Year.ToString)
    End Sub

    Private Sub CargarActividadesMensuales(ByVal Mes As Integer, ByVal Anio As Integer)
        Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("cnxCMUSAT").ConnectionString)

        tblActividades = obj.TraerDataTable("ConsultarAgenda", 4, Me.hdIdcursoVirtual.Value, Mes, Anio, 0, 0)
        Me.DataList1.DataSource = tblActividades
        Me.DataList1.DataBind()
        Me.lbltotal.Text = "TOTAL: " & Me.DataList1.Items.Count & " actividades."

        If IsPostBack = False Then
            ClsFunciones.LlenarListas(Me.cboidcategoria, obj.TraerDataTable("ConsultarCategoria", 1, "agenda", 0, 0), "idcategoria", "nombrecategoria")
        End If
        obj = Nothing
    End Sub
    Protected Sub cmdAgregar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdAgregar.Click
        RegistroActividades(True, False)
    End Sub
    Protected Sub cmdGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdGuardar.Click
        Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("cnxCMUSAT").ConnectionString)

        Try
            Dim finicio, ffin As String

            finicio = CDate(Me.txtFechaInicio.Text & " " & Me.HoraIni.Text & ":" & Me.MinIni.Text & ":00")
            ffin = CDate(Me.txtFechaFin.Text & " " & Me.HoraFin.Text & ":" & Me.MinFin.Text & ":00")

            ''Grabar información
            obj.IniciarTransaccion()

            If hdIdAgenda.Value > 0 Then
                obj.Ejecutar("ModificarAgenda", Me.hdIdAgenda.Value, Me.txttituloagenda.Text.Trim, finicio, ffin, Me.txtlugar.Text.Trim, Me.txtdescripcion.Text.Trim, "", Me.cboidcategoria.SelectedValue, 1)
            Else
                obj.Ejecutar("AgregarAgenda", Me.txttituloagenda.Text.Trim, finicio, ffin, Me.txtlugar.Text.Trim, Me.txtdescripcion.Text.Trim, "", Me.cboidcategoria.SelectedValue, Me.hdIdcursoVirtual.Value, Me.hdIdUsuario.Value, 1, 1, 0)
            End If
            obj.TerminarTransaccion()

            RegistroActividades(False, True)
            'CargarActividadesMensuales(Me.Calendar1.VisibleDate.Month, Me.Calendar1.VisibleDate.Year)
            Me.CargarActividadesMensuales(Month(Now), Year(Now))
            Me.lblmensaje.Text = "Los datos se han guardado correctamente."

            'Me.DataList1.DataBind()
            obj = Nothing
        Catch ex As Exception
            Me.lblmensaje.Text = "Ha ocurrido un error " & ex.Message
            obj = Nothing
        End Try
        
    End Sub

    Protected Sub cmdCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdCancelar.Click
        RegistroActividades(False, True)
    End Sub

    Protected Sub DataList1_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataListCommandEventArgs) Handles DataList1.ItemCommand
        Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("cnxCMUSAT").ConnectionString)

        If e.CommandName = "Editar" Then
            Me.hdIdAgenda.Value = (CInt(DataList1.DataKeys(e.Item.ItemIndex)))

            Dim tbl As Data.DataTable

            tbl = obj.TraerDataTable("ConsultarAgenda", 3, hdIdAgenda.Value, 0, 0, 0, 0)

            Me.txttituloagenda.Text = tbl.Rows(0).Item("tituloagenda")
            Me.txtdescripcion.Text = tbl.Rows(0).Item("descripcion").ToString
            Me.txtlugar.Text = tbl.Rows(0).Item("lugar")
            Me.cboidcategoria.SelectedValue = tbl.Rows(0).Item("idcategoria")

            Me.txtFechaInicio.Text = CDate(tbl.Rows(0).Item("fechainicio").ToString).ToShortDateString
            Me.txtFechaFin.Text = CDate(tbl.Rows(0).Item("fechafin").ToString).ToShortDateString
            'Me.HoraIni.Text = Hour(CDate(tbl.Rows(0).Item("fechainicio").ToString).ToShortTimeString)
            'Me.HoraFin.Text = Mid(tbl.Rows(0).Item("fechafin").ToString, 12, 2)
            'Me.MinIni.Text = Mid(tbl.Rows(0).Item("fechainicio").ToString, 15, 2)
            'Me.MinFin.Text = Mid(tbl.Rows(0).Item("fechafin").ToString, 15, 2)

            'Response.Write(Hour(CDate(tbl.Rows(0).Item("fechainicio").ToString).ToShortTimeString))
            tbl.Dispose()
            obj = Nothing

            Me.DataList1.Visible = False
            Me.fraNuevo.Visible = True
        End If
        If e.CommandName = "Eliminar" Then
            obj.IniciarTransaccion()
            obj.Ejecutar("EliminarAgenda", CInt(DataList1.DataKeys(e.Item.ItemIndex)))
            obj.TerminarTransaccion()
            obj = Nothing

            ''Cargar con los valores anteriores
            Me.CargarActividadesMensuales(Month(Now), Year(Now))
        End If

    End Sub

    Private Sub RegistroActividades(ByVal Nuevo As Boolean, ByVal Lista As Boolean)
        Me.fraNuevo.Visible = Nuevo
        Me.DataList1.Visible = Lista

        Me.txttituloagenda.Text = ""
        Me.txtdescripcion.Text = ""
        Me.txtlugar.Text = ""
        Me.txtFechaInicio.Text = Now.Date()
        Me.txtFechaFin.Text = Now.Date()
        Me.hdIdAgenda.Value = 0
        Me.lblmensaje.Text = ""
    End Sub
End Class
