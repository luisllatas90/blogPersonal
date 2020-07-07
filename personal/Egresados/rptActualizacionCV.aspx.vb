﻿Partial Class rptActualizacionCV
    Inherits System.Web.UI.Page
    Public nro As Integer = 0
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (IsPostBack = False) Then

            CargarListas()
       

            '#Años
            Me.ddlEgreso.Items.Add("TODOS")
            For i As Integer = 2004 To Date.Today.Year
                Me.ddlEgreso.Items.Add(i)            
            Next
            Me.ddlEgreso.SelectedIndex = 0

        End If
    End Sub
   
    Sub CargarListas()
        Dim obj As New ClsConectarDatos
        Dim dt As Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        '#Escuela
        dt = New Data.DataTable
        dt = obj.TraerDataTable("ALUMNI_ListarCarreraProfesional")
        Me.ddlEscuela.DataSource = dt
        Me.ddlEscuela.DataTextField = "nombre"
        Me.ddlEscuela.DataValueField = "codigo"
        Me.ddlEscuela.DataBind()
        dt.Dispose()
        obj.CerrarConexion()
        obj = Nothing       
    End Sub
    Function CargarGrid() As Data.DataTable
        Dim obj As New ClsConectarDatos
        Dim dtTabla As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        dtTabla = obj.TraerDataTable("ALUMNI_BuscaBitacoraUpdateDatos", _
                                     IIf(Me.ddlEscuela.SelectedIndex = 0, "0", Me.ddlEscuela.SelectedValue), _
                                     IIf(Me.txtApellidoNombre.Text.Trim = "", "%", Me.txtApellidoNombre.Text.Trim), _
                                     IIf(Me.chkIncluirFechas.Checked, txtDesde.Value.Trim, "%"), _
                                     IIf(Me.chkIncluirFechas.Checked, txtHasta.Value.Trim, "%"), _
                                     IIf(Me.ddlEgreso.SelectedIndex = 0, "%", Me.ddlEgreso.SelectedValue))
        obj.CerrarConexion()
        obj = Nothing
        Me.lblMensajeFormulario.Text = "Se encontraron " & dtTabla.Rows.Count & " registro(s)."
        Return dtTabla
    End Function
    
    Protected Sub btnExportar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExportar.Click
        Dim dt As New Data.DataTable
        dt = CargarGrid()
        If (dt.Rows.Count > 0) Then
            Me.gvEgresados.DataSource = dt
            Me.gvEgresados.DataBind()
            Axls()
        Else
            Response.Write("<script> alert('La tabla no tiene datos')</script>")
        End If
    End Sub
    Private Sub Axls()
        Dim sb As StringBuilder = New StringBuilder()
        Dim SW As System.IO.StringWriter = New System.IO.StringWriter(sb)
        Dim htw As HtmlTextWriter = New HtmlTextWriter(SW)
        Dim Page As Page = New Page()
        Dim form As HtmlForm = New HtmlForm()
        Me.gvEgresados.EnableViewState = False
        Page.EnableEventValidation = False
        Page.DesignerInitialize()
        Page.Controls.Add(form)
        form.Controls.Add(Me.gvEgresados)
        Page.RenderControl(htw)
        Response.Clear()
        Response.Buffer = True
        Response.ContentType = "application/vnd.ms-excel"
        Response.AddHeader("Content-Disposition", "attachment;filename=rptActualizacionCV.aspx" & ".xls")
        Response.Charset = "UTF-8"
        Response.ContentEncoding = Encoding.Default
        Response.Write(sb.ToString())
        Response.End()
    End Sub
    Protected Sub btnBusca_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBusca.Click
        Me.lblMensajeFormulario.Text = ""
        Dim dt As New Data.DataTable
        dt = CargarGrid()

        If dt.Rows.Count = 0 Then
            Me.lblMensajeFormulario.Text = "No se encontraron registros"
            Me.gvEgresados.DataSource = Nothing
        Else
            Me.gvEgresados.DataSource = dt
            'Me.gvEgresados.Columns.Item(4).Visible = False
        End If

        Me.gvEgresados.DataBind()

        dt.Dispose()
    End Sub
    Function encode(ByVal str As String) As String
        Return (Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(str)))
    End Function
    Protected Sub gvwEgresados_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvEgresados.RowDataBound
        Dim rutaCV As String = ""



        rutaCV = "../../librerianet/egresado/alumniusat.aspx?xcod=" & encode(e.Row.Cells(12).Text)
        e.Row.Cells(12).Text = "<center><a target=""_blank"" href=""" & rutaCV & """>CV</a></center>"
        'If (e.Row.RowIndex <> -1) Then
        '    If (data(1).ToString <> "") Then
        '        e.Row.Cells(12).Text = "<center><a target=""_blank"" href=""" & rutaCV & """><img src=""../../librerianet/Egresado/fotos/""" & data(1).ToString & """ Height=""28px"" Width=""25px"" /></a></center>"
        '    Else
        '        If data(2).ToString = "F" Then
        '            e.Row.Cells(12).Text = "<center><a target=""_blank"" href=""" & rutaCV & """><img src=""../../librerianet/Egresado/archivos/female.png"" Height=""28px"" Width=""25px""  /></a></center>"
        '        Else
        '            e.Row.Cells(12).Text = "<center><a target=""_blank"" href=""" & rutaCV & """><img src=""../../librerianet/Egresado/archivos/male.png"" Height=""28px"" Width=""25px""  /></a></center>"
        '        End If
        '    End If
        'End If

    End Sub

    Protected Sub chkIncluirFechas_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkIncluirFechas.CheckedChanged
        lblhasta.Visible = Me.chkIncluirFechas.Checked
        lbldesde.Visible = Me.chkIncluirFechas.Checked
        txtDesde.Visible = Me.chkIncluirFechas.Checked
        txtHasta.Visible = Me.chkIncluirFechas.Checked
    End Sub
End Class



