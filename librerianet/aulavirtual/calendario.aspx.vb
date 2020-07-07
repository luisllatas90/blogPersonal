
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
            If Request.QueryString("codigo_tfu") = 3 Then
                Me.hdIdcursoVirtual.Value = Request.QueryString("idcursovirtual")
                Me.hdIdUsuario.Value = Request.QueryString("idusuario")
                Me.hdIdAgenda.Value = 0
                Me.hdcodigo_tfu.Value = Request.QueryString("codigo_tfu")
                Me.CargarActividadesMensuales(Month(Now), Year(Now))
            Else
                Response.Write("<font color='red'>Usted no tiene acceso para visualizar esta información</font>")
            End If
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
 
        obj = Nothing
    End Sub
  
End Class
