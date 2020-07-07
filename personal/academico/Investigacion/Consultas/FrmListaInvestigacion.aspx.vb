Imports System.IO
Imports System.Security.Cryptography

Partial Class academico_Investigacion_Consultas_FrmListaInvestigacion
    Inherits System.Web.UI.Page

    Protected Sub btnBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBuscar.Click
        Dim dt As Data.DataTable
        Dim cnx As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
        Dim anio As Integer
        Try
            If (Me.cboAnio.Text = "--- TODOS ---") Then
                anio = 0
            Else
                anio = Me.cboAnio.Text
            End If

            dt = cnx.TraerDataTable("INV_ConsultaInvestigaciones_v2", Me.txtTitulo.Text, anio)
            Me.gvInvestigaciones.DataSource = dt
            Me.gvInvestigaciones.DataBind()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (IsPostBack = False) Then
            CargaAnio()
        End If
    End Sub

    Private Sub CargaAnio()
        Me.cboAnio.Items.Add("--- TODOS ---")
        For i As Integer = 2012 To Date.Today.Year + 1
            Me.cboAnio.Items.Add(i)
        Next
    End Sub

    Protected Sub btnExportar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExportar.Click
        Dim sb As StringBuilder = New StringBuilder()
        Dim sw As StringWriter = New StringWriter(sb)
        Dim htw As HtmlTextWriter = New HtmlTextWriter(sw)
        Dim pagina As Page = New Page
        Dim form = New HtmlForm
        gvInvestigaciones.EnableViewState = False        
        pagina.EnableEventValidation = False
        pagina.DesignerInitialize()
        pagina.Controls.Add(form)
        form.Controls.Add(gvInvestigaciones)
        pagina.RenderControl(htw)
        Response.Clear()
        Response.Buffer = True
        Response.ContentType = "application/vnd.ms-excel"
        Response.AddHeader("Content-Disposition", "attachment;filename=data.xls")
        Response.Charset = "UTF-8"

        Response.ContentEncoding = Encoding.Default
        Response.Write(sb.ToString())
        Response.End()        
    End Sub

    Protected Sub gvInvestigaciones_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvInvestigaciones.RowDataBound        
        If e.Row.RowIndex >= 0 Then
            e.Row.Cells(17).Text = "<a href='../../../../filesInvestigacion/" & e.Row.Cells(0).Text.ToString & e.Row.Cells(16).Text.ToString & "' target='_blank'><img src='../private/paper.png' /></a>"
        End If

        e.Row.Cells(16).Visible = False
    End Sub
End Class
