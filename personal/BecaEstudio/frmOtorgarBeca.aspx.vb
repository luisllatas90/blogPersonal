
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
        If Session("Beca_codigo_cac") IsNot Nothing Then
            Me.ddlCiclo.SelectedValue = Session("codigo_cac")
        End If

        tb = obj.TraerDataTable("Beca_ConsultarCarreraProfesional")
        Me.ddlEscuela.DataSource = tb
        Me.ddlEscuela.DataTextField = "nombre_cpf"
        Me.ddlEscuela.DataValueField = "codigo_cpf"
        Me.ddlEscuela.DataBind()

        If Session("codigo_cpf") IsNot Nothing Then
            Me.ddlEscuela.SelectedValue = Session("codigo_cpf")
        Else
            Me.ddlEscuela.SelectedValue = 0
        End If

        tb = obj.TraerDataTable("Beca_ConsultarBecaTipo")
        Me.ddlTipoBeca.DataSource = tb
        Me.ddlTipoBeca.DataTextField = "descripcion_bec"
        Me.ddlTipoBeca.DataValueField = "codigo_bec"
        Me.ddlTipoBeca.DataBind()
        If Session("codigo_bec") IsNot Nothing Then
            Me.ddlTipoBeca.SelectedValue = Session("codigo_bec")
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
            dt = obj.TraerDataTable("BECA_ListaOtorgaBeca", Me.ddlCiclo.SelectedValue, Me.ddlEscuela.SelectedValue, Me.ddlTipoBeca.SelectedValue)
            obj.CerrarConexion()

            Me.gvBecas.DataSource = dt
            Me.gvBecas.DataBind()

            Me.lblnumero.Text = "Existen " & dt.Rows.Count & " registros."

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub btnConfirmar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnConfirmar.Click
        Dim obj As New ClsConectarDatos
        Try
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            Dim Fila As GridViewRow
            For i As Integer = 0 To Me.gvBecas.Rows.Count - 1
                Fila = Me.gvBecas.Rows(i)
                Dim valor As Boolean = CType(Fila.FindControl("chkElegir"), CheckBox).Checked
                If (valor = True) Then
                    obj.AbrirConexion()
                    obj.TraerDataTable("BECA_ConfirmaBeca", Me.gvBecas.DataKeys(i).Values("codigo_bso"))
                    obj.CerrarConexion()
                End If
            Next

            btnBuscar_Click(sender, e)
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
        
    End Sub

    Protected Sub btnProcesar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnProcesar.Click
        Dim obj As New ClsConectarDatos
        Try
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            obj.Ejecutar("BECA_GeneraProceso", Me.ddlCiclo.SelectedValue)
            obj.Ejecutar("BECA_GeneraProcesoEducacion", Me.ddlCiclo.SelectedValue)
            obj.CerrarConexion()
            btnBuscar_Click(sender, e)
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub gvBecas_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvBecas.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Cells(1).Text = e.Row.RowIndex + 1
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
End Class
