Partial Class rptActualizacionFoto
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
    Function CargarGrid(ByVal SP As String) As Data.DataTable
        Dim obj As New ClsConectarDatos
        Dim dtTabla As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        dtTabla = obj.TraerDataTable(SP, _
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

    
    
    Protected Sub btnBusca_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBusca.Click
        Me.lblMensajeFormulario.Text = ""
        Dim dt As New Data.DataTable
        Dim html As String = ""
        Dim nuevafila As Boolean = True
        Dim ruta As String = "../../librerianet/egresado/fotos/"
        Dim rutaSin As String = "img/"
        dt = CargarGrid("ALUMNI_BuscaBitacoraFotos")
        If dt.Rows.Count Then
            html &= "<table id=""TablaFotos"">"
            For i As Integer = 0 To dt.Rows.Count - 1
                If i Mod 5 = 0 Then
                    nuevafila = True
                    html &= IIf(nuevafila, "<tr>", "")
                Else
                    nuevafila = False
                    html &= IIf(nuevafila, "</tr>", "")
                End If

                html &= "<td>"
                html &= "<br />"
                html &= "<label><b>" & dt.Rows(i).Item("Apellidos").ToString & "<br /> " & dt.Rows(i).Item("Nombres").ToString & "</b></label><br /><br />"
                If dt.Rows(i).Item("foto_ega").ToString = "" Then
                    dt.Rows(i).Item("foto_ega") = IIf(dt.Rows(i).Item("sexo_pso").ToString = "F", "female.png", "male.png")
                    html &= "<img alt=""Sin Foto"" src=""" & rutaSin & dt.Rows(i).Item("foto_ega").ToString & """ style=""height: 85px; width: 80px"" /><br /><br />"
                Else
                    html &= "<img alt=""Actualizada el " & dt.Rows(i).Item("FOTO").ToString & """ src=""" & ruta & dt.Rows(i).Item("foto_Ega").ToString & """ style=""height: 85px; width: 80px"" /><br /><br />"
                End If
                html &= "<label><b>" & dt.Rows(i).Item("AÑO EGR.").ToString & "</b></label>"
                html &= "</td> "

            Next
            html &= "</table>"
            Me.listaFotos.InnerHtml = html
            html = ""
        Else
            Me.listaFotos.InnerHtml = "No se encontraron registros"
        End If
        dt.Dispose()
    End Sub

    Protected Sub chkIncluirFechas_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkIncluirFechas.CheckedChanged
        lblhasta.Visible = Me.chkIncluirFechas.Checked
        lbldesde.Visible = Me.chkIncluirFechas.Checked
        txtDesde.Visible = Me.chkIncluirFechas.Checked
        txtHasta.Visible = Me.chkIncluirFechas.Checked
    End Sub

    Protected Sub btnExportar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExportar.Click
        Dim dt As New Data.DataTable
        dt = CargarGrid("ALUMNI_BuscaBitacoraFotosExportar")
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
        Response.AddHeader("Content-Disposition", "attachment;filename=rptActualizacionFoto.aspx" & ".xls")
        Response.Charset = "UTF-8"
        Response.ContentEncoding = Encoding.Default
        Response.Write(sb.ToString())
        Response.End()
        'Me.gvEgresados.DataSource = Nothing
    End Sub
End Class



