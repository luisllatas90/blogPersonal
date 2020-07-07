
Partial Class academico_horarios_administrar_frmRptAmbientesxFechaPostGrado
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        If Not Page.IsPostBack Then
            obj.AbrirConexion()
            ClsFunciones.LlenarListas(Me.ddlCco, obj.TraerDataTable("HorariosPE_ConsultarCcoSol", "G"), "codigo_cpf", "nombre_cpf")
            'Response.Write(Me.ddlCco.SelectedValue)
            ClsFunciones.LlenarListas(Me.ddlPrograma, obj.TraerDataTable("HorariosPE_ConsultarProgramaxCco", Me.ddlCco.SelectedValue, "G"), "codigo_cco", "descripcion_Cco")

            Me.txtDesde.Value = DateSerial(Now.Date.Year, Now.Month, 1)
            Me.txtHasta.Value = DateSerial(Year(Now.Date), Month(Now.Date) + 1, 0)
        End If

    End Sub

    Protected Sub btnBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBuscar.Click
        Dim obj As New ClsConectarDatos
        Dim tb As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        tb = obj.TraerDataTable("HorarioPE_ConsultarHorario_PostGrado", 0, Me.txtDesde.Value, Me.txtHasta.Value, Me.ddlPrograma.SelectedValue, Me.ddlEstado.SelectedValue, -1, 0, Me.ddlCco.SelectedValue)
        Me.gridAmbientes.DataSource = tb
        Me.gridAmbientesExportar.DataSource = tb
        'Response.Write(Me.ddlAmbiente.SelectedValue & "," & Me.txtDesde.Value & "," & Me.txtHasta.Value & "," & Me.ddlCco.SelectedValue & "," & Me.ddlEstado.SelectedValue & "," & Me.ddlTipoEstudio.SelectedValue & "," & CInt(Me.checkPreferencial.Checked))
        Me.gridAmbientes.DataBind()
        Me.gridAmbientesExportar.DataBind()
        obj.CerrarConexion()
        obj = Nothing
    End Sub


    Protected Sub gridAmbientes_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridAmbientes.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            If e.Row.Cells(0).Text = "1" Then
                e.Row.Cells(0).Text = "<img src='images/star.png' title='Ambiente preferencial' alt='Ambiente Preferencial'>"
            Else
                e.Row.Cells(0).Text = "<img src='images/door.png' title='Ambiente' alt='Ambiente '>"
            End If

            If e.Row.Cells(12).Text = "ASIGNADO" Then
                e.Row.Cells(1).ForeColor = Drawing.Color.Navy
            Else
                e.Row.Cells(1).ForeColor = Drawing.Color.Green
            End If
        End If
    End Sub
    Protected Sub ddlCco_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlCco.SelectedIndexChanged
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        ClsFunciones.LlenarListas(Me.ddlPrograma, obj.TraerDataTable("HorariosPE_ConsultarProgramaxCco", Me.ddlCco.SelectedValue, "G"), "codigo_cco", "descripcion_Cco")
        obj.CerrarConexion()
    End Sub

    Protected Sub btnExportar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExportar.Click
        Me.gridAmbientesExportar.Visible = True
        Dim sb As StringBuilder = New StringBuilder()
        Dim SW As System.IO.StringWriter = New System.IO.StringWriter(sb)
        Dim htw As HtmlTextWriter = New HtmlTextWriter(SW)
        Dim Page As Page = New Page()
        Dim form As HtmlForm = New HtmlForm()
        Page.EnableEventValidation = False
        Page.DesignerInitialize()
        Page.Controls.Add(form)
        form.Controls.Add(Me.gridAmbientesExportar)
        Page.RenderControl(htw)
        Response.Clear()
        Response.Buffer = True
        Response.ContentType = "application/vnd.ms-excel"
        Response.AddHeader("Content-Disposition", "attachment;filename=Consulta_de_Ambientes_PostGrado" & ".xls")
        Response.Charset = "UTF-8"
        Response.ContentEncoding = Encoding.Default
        Response.Write(sb.ToString())
        Response.End()
        Me.gridAmbientesExportar.Visible = False
    End Sub

    Protected Sub gridAmbientesExportar_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridAmbientesExportar.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            If e.Row.Cells(11).Text = "ASIGNADO" Then
                e.Row.Cells(0).ForeColor = Drawing.Color.Navy
            Else
                e.Row.Cells(0).ForeColor = Drawing.Color.Green
            End If
        End If
    End Sub
End Class
