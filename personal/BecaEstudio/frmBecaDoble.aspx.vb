
Partial Class BecaEstudio_frmOtorgarBeca
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            CargarCombos()

        Else
            Session.Clear()
        End If
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
        If Session("codigo_cac") IsNot Nothing Then
            Me.ddlCiclo.SelectedValue = Session("codigo_cac")
        End If
        obj.CerrarConexion()
        obj = Nothing
    End Sub

    Protected Sub btnBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBuscar.Click
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        Try
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            dt = obj.TraerDataTable("Beca_ConsultarDobleSolicitud", Me.ddlCiclo.SelectedValue)
            obj.CerrarConexion()

            Me.gvBecas.DataSource = dt
            Me.gvBecas.DataBind()
            Me.gvBecas.GridLines = GridLines.Both
            Me.lblnumero.Text = "Existen " & dt.Rows.Count & " registros."

            If dt.Rows.Count Then
                Me.cmdExportar.Visible = True
            Else
                Me.cmdExportar.Visible = False
            End If

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub




    Protected Sub gvBecas_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvBecas.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Cells(0).Text = e.Row.RowIndex + 1
        End If

    End Sub

    Protected Sub cmdExportar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdExportar.Click
        Dim sb As StringBuilder = New StringBuilder()
        Dim SW As System.IO.StringWriter = New System.IO.StringWriter(sb)
        Dim htw As HtmlTextWriter = New HtmlTextWriter(SW)
        Dim Page As Page = New Page()
        Dim form As HtmlForm = New HtmlForm()
        Me.gvBecas.EnableViewState = False
        Page.EnableEventValidation = False
        Page.DesignerInitialize()
        Page.Controls.Add(form)
        form.Controls.Add(Me.gvBecas)
        Page.RenderControl(htw)
        Response.Clear()
        Response.Buffer = True
        Response.ContentType = "application/vnd.ms-excel"
        Response.AddHeader("Content-Disposition", "attachment;filename=reportepromedios.xls")
        Response.Charset = "UTF-8"
        Response.ContentEncoding = Encoding.Default
        Response.Write(sb.ToString())
        Response.End()
    End Sub

    Protected Sub gvBecas_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles gvBecas.RowDeleting
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        obj.Ejecutar("BECA_RechazaSolicitud", Me.gvBecas.DataKeys.Item(e.RowIndex).Values(0))
        btnBuscar_Click(sender, e)
        obj.CerrarConexion()
        obj = Nothing



    End Sub
End Class
