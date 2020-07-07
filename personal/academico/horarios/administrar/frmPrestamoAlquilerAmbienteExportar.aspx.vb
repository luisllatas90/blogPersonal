
Partial Class academico_horarios_frmPrestamoAlquilerAmbiente
    Inherits System.Web.UI.Page
    Public Hcodigo_cup As Integer = 381553 'Real
    Public Acodigo_cup As Integer = 381740 'ReaL
    Public Hcodigo_pes As Integer = 198
    Public Hcodigo_cac As Integer = 52
    Public Errores As String
 

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            consultarHorarios()
            exportar()
        End If
    End Sub
    Sub consultarHorarios()
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        'Response.Write("id: " & CInt(Request.QueryString("id")) & "</br>")
        'Response.Write("val1: " & CInt(Request.QueryString("val1")) & "</br>")
        'Response.Write("val2: " & CInt(Request.QueryString("val2")) & "</br>")

        Me.gridHorario.DataSource = obj.TraerDataTable("HorarioPE_ConsultarSol", CInt(Request.QueryString("id")), CInt(Request.QueryString("val1")), CInt(Request.QueryString("val2")))
        obj.CerrarConexion()
        Me.gridHorario.DataBind()
        obj = Nothing
    End Sub

    Protected Sub gridHorario_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridHorario.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then           
            e.Row.Cells(2).Font.Bold = True
            e.Row.Cells(5).Font.Bold = True


            If e.Row.Cells(5).Text = "Pendiente" Then
                e.Row.Cells(6).ForeColor = Drawing.Color.Green
            Else
                e.Row.Cells(6).ForeColor = Drawing.Color.Blue
            End If

            If CDate(e.Row.Cells(1).Text) < CDate(Today) Then
                e.Row.Cells(6).ForeColor = Drawing.Color.Gray
                e.Row.Cells(6).Text = e.Row.Cells(6).Text & " - Finalizado"
               
            End If
            If CDate(e.Row.Cells(1).Text) = CDate(Today) Then
                e.Row.Cells(6).ForeColor = Drawing.Color.IndianRed
                e.Row.Cells(6).Text = e.Row.Cells(6).Text & " - Hoy"
            End If
        End If
    End Sub

    Sub exportar()

        Dim sb As StringBuilder = New StringBuilder()
        Dim SW As System.IO.StringWriter = New System.IO.StringWriter(sb)
        Dim htw As HtmlTextWriter = New HtmlTextWriter(SW)
        Dim Page As Page = New Page()
        Dim form As HtmlForm = New HtmlForm()
        Page.EnableEventValidation = False
        Page.DesignerInitialize()
        Page.Controls.Add(form)
        form.Controls.Add(Me.gridHorario)
        Page.RenderControl(htw)
        Response.Clear()
        Response.Buffer = True
        Response.ContentType = "application/vnd.ms-excel"
        Response.AddHeader("Content-Disposition", "attachment;filename=MisSolicitudesdeAmbientes-" & Date.Now.Day & Date.Now.Month & Date.Now.Year & ".xls")
        Response.Charset = "UTF-8"
        Response.ContentEncoding = Encoding.Default
        Response.Write(sb.ToString())
        Response.End()

    End Sub
End Class
