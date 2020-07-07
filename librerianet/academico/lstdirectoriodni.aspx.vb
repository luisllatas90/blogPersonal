
Partial Class lstdirectoriodni
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            'Cargar el ciclo académico
            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            ClsFunciones.LlenarListas(Me.dpCodigo_cpf, obj.TraerDataTable("ConsultarcarreraProfesional", "MA", 0), "codigo_cpf", "nombre_cpf", "--Todos--")
            obj.CerrarConexion()
            obj = Nothing
            'Response.Write("--->" & Session("ALU") & "- " & Request.QueryString("Tipo"))
            If Request.QueryString("Tipo") <> "P" Then
                If Session("ALU") Is Nothing Then
                    Response.Redirect("identificacion.aspx")
                End If
            End If
            Me.lblNroRegistros.Text = ""
        End If
    End Sub
    Private Sub BuscarDirectorioEstudiantes()
        Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("cnxBDUSAT").ConnectionString)
        If Me.chkActualizados.Checked = True Then
            Me.grwAlumnos.DataSource = obj.TraerDataTable("ConsultarDirectorioAlumnos", 4, Me.txtbuscar.Text.Trim, Me.dpCodigo_cpf.SelectedValue, 0, 0)
        Else
            Me.grwAlumnos.DataSource = obj.TraerDataTable("ConsultarDirectorioAlumnos", 3, Me.txtbuscar.Text.Trim, Me.dpCodigo_cpf.SelectedValue, 0, 0)
        End If
        Me.grwAlumnos.DataBind()
        Me.lblNroRegistros.Text = grwAlumnos.Rows.Count.ToString & " Registros"
        obj = Nothing
    End Sub

    Protected Sub cmdBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdBuscar.Click
        BuscarDirectorioEstudiantes()
        grwAlumnos.Columns(8).Visible = True
    End Sub
    Protected Sub grwAlumnos_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grwAlumnos.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim fila As Data.DataRowView
            Dim TipoOperador As String
            fila = e.Row.DataItem
            e.Row.Attributes.Add("id", "" & fila.Row("codigo_alu").ToString & "")
            e.Row.Attributes.Add("OnMouseOver", "Resaltar(1,this,'S')")
            e.Row.Attributes.Add("OnMouseOut", "Resaltar(0,this,'S')")
            TipoOperador = Request.QueryString("Tipo") ' A: alumno, P: Personal
            Dim objEnc As New EncriptaCodigos.clsEncripta
            e.Row.Cells(8).Text = "<a href='frmactualizardni.aspx?accion=M&c=" & objEnc.Codifica("069" & fila.Row("codigo_alu")) & "&ctf=" & Request.QueryString("ctf") & "&x=" & objEnc.Codifica("069" & Request.QueryString("id")) & "&Tipo=" & Request.QueryString("Tipo") & "&KeepThis=true&TB_iframe=true&height=600&width=800&modal=true' title='Cambiar estado' class='thickbox'>&nbsp;<img src='../../images/editar.gif' border=0 /><a/>"
            e.Row.Cells(9).Text = "<a href='JustificacionDni.aspx?accion=M&c=" & objEnc.Codifica("069" & fila.Row("codigo_alu")) & "&ctf=" & Request.QueryString("ctf") & "&x=" & objEnc.Codifica("069" & Request.QueryString("id")) & "&Tipo=" & Request.QueryString("Tipo") & "&KeepThis=true&TB_iframe=true&height=360&width=500&modal=true' title='Autorizacion' class='thickbox'>&nbsp;<img src='../../images/forward.gif' border=0 /><a/>"
            e.Row.Cells(0).Text = e.Row.RowIndex + 1
        End If
    End Sub

    Protected Sub cmdExportar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdExportar.Click
        cmdBuscar_Click(sender, e)
        Axls("ReportePresupuesto", grwAlumnos, "Reporte: Alumnos sin DNI", "Sistema Académico - Campus Virtual USAT")
    End Sub

    Private Sub Axls(ByVal nombrearchivo As String, ByRef grid As GridView, ByVal titulo As String, ByVal piedepagina As String)
        Response.Clear()
        Response.Buffer = True
        Response.ContentType = "application/vnd.ms-xls"
        Response.AddHeader("Content-Disposition", "attachment; filename=" & nombrearchivo & ".xls")
        Response.Charset = "UTF-8"
        Response.ContentEncoding = System.Text.Encoding.Default
        grid.HeaderRow.BackColor = Drawing.Color.FromName("#3366CC")
        grid.HeaderRow.ForeColor = Drawing.Color.White
        grid.Columns(8).Visible = False
        Response.Write(ClsFunciones.HTML(grid, titulo, piedepagina))
        Response.End()
        grid.Columns(8).Visible = True
    End Sub

    Protected Sub lnkReniec_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkReniec.Click
        ClientScript.RegisterStartupScript(Me.GetType, "reniec", "window.open('https://cel.reniec.gob.pe/valreg/valreg.do?accion=ini')", True)
    End Sub

    Protected Sub grwAlumnos_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grwAlumnos.SelectedIndexChanged

    End Sub
End Class
