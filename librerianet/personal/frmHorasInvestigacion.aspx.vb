
Partial Class personal_frmHorasInvestigacion
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                CargaPeridoLaboral()
                CargaDptoAcad()
                CargarDatos()
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub CargaPeridoLaboral()
        Try
            Dim obj As New clsPersonal
            Dim dts As New Data.DataTable
            dts = obj.CargaPeridoLaboral
            If dts.Rows.Count > 0 Then
                Me.ddlPeriodoLaboral.DataSource = dts
                Me.ddlPeriodoLaboral.DataTextField = "descripcion_Pel"
                Me.ddlPeriodoLaboral.DataValueField = "codigo_Pel"
                Me.ddlPeriodoLaboral.DataBind()
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub
    Private Sub CargaDptoAcad()
        Try
            Dim obj As New clsPersonal
            Dim dts As New Data.DataTable
            dts = obj.CargaDptoAcad
            If dts.Rows.Count > 0 Then
                Me.ddlDptAcad.DataSource = dts
                Me.ddlDptAcad.DataTextField = "nombre_Dac"
                Me.ddlDptAcad.DataValueField = "codigo_dac"
                Me.ddlDptAcad.DataBind()
                Me.ddlDptAcad.SelectedValue = "%"
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub CargarDatos()
        Dim cnx As New ClsConectarDatos
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString        
        Try
            cnx.AbrirConexion()
            dts = cnx.TraerDataTable("PER_HorasInvestigacion", ddlPeriodoLaboral.SelectedValue, Request.QueryString("id"), Request.QueryString("ctf"), IIf(chkFomrato.Checked = True, "HD", "HM"), Me.ddlDptAcad.SelectedItem.Text)
            cnx.CerrarConexion()

            Me.gvActividad.DataSource = dts
            Me.gvActividad.DataBind()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub ddlDptAcad_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlDptAcad.SelectedIndexChanged
        Try
            CargarDatos()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub ddlPeriodoLaboral_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPeriodoLaboral.SelectedIndexChanged
        Try
            CargarDatos()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub btnExportar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExportar.Click
        Try
            Dim responsePage As HttpResponse = Response
            Dim sb As StringBuilder = New StringBuilder()
            Dim SW As System.IO.StringWriter = New System.IO.StringWriter(sb)
            Dim htw As HtmlTextWriter = New HtmlTextWriter(SW)
            Dim Page As Page = New Page()
            Dim form As HtmlForm = New HtmlForm()
            Me.gvActividad.EnableViewState = False
            Page.EnableEventValidation = False
            Page.DesignerInitialize()
            Page.Controls.Add(form)
            form.Controls.Add(Me.gvActividad)

            responsePage.Clear()
            responsePage.Buffer = True
            responsePage.ContentType = "application/vnd.ms-excel"
            responsePage.AddHeader("Content-Disposition", "attachment;filename=TipoActividad" & ".xls")
            responsePage.Charset = "UTF-8"
            responsePage.ContentEncoding = Encoding.Default
            Page.RenderControl(htw)
            responsePage.Write(sb.ToString())
            responsePage.Flush()
            responsePage.SuppressContent = True
            HttpContext.Current.ApplicationInstance.CompleteRequest()

            'responsePage.End()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub
End Class
