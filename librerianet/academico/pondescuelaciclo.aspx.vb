Partial Class pondescuelaciclo
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Page.RegisterStartupScript("bloquea0", "<script type='text/javascript'>Ocultar();</script>")
        If IsPostBack = False Then
            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            Dim tbl As New Data.DataTable
            Dim cls As New ClsFunciones
            Dim codigo_per As Integer = Request.QueryString("id")
            Dim codigo_tfu As Int16 = Request.QueryString("ctf")
            If codigo_tfu = 1 Or codigo_tfu = 7 Or codigo_tfu = 16 Then
                If Request("mod") = 10 Then ' PARA GO
                    tbl = obj.TraerDataTable("ConsultarCarreraProfesional", "GO", 0) '
                Else '
                    tbl = obj.TraerDataTable("ConsultarCarreraProfesional", "MA", 0)
                End If '

            Else
                tbl = obj.TraerDataTable("consultaracceso", "ESC", Request.QueryString("mod"), codigo_per)
            End If
            cls.CargarListas(Me.dpCiclo, obj.TraerDataTable("ConsultarCicloAcademico", "FR", 0), "codigo_cac", "descripcion_cac")
            cls.CargarListas(Me.dpEscuela, tbl, "codigo_cpf", "nombre_cpf", "--Seleccione la Carrera Profesional--")
            cls = Nothing

            obj.CerrarConexion()
            obj = Nothing
        End If
    End Sub

    Protected Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowDataBound
        Dim lbl As Label
        Dim Cadena As String

        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Cells(0).Text = e.Row.RowIndex + 1
            Cadena = "codigo_alu=" & GridView1.DataKeys(e.Row.RowIndex).Value & "&codigouniver_alu=" & e.Row.Cells(1).Text & "&alumno=" & e.Row.Cells(2).Text & "&nombre_cpf=" & Me.dpEscuela.SelectedItem.Text

            lbl = e.Row.FindControl("lblHistorial")
            lbl.Attributes.Add("onclick", "AbrirPopUp('../../personal/academico/estudiante/historial.asp?" & Cadena & "','600','650','yes','yes','yes');return(false);")
            lbl = Nothing
            lbl = e.Row.FindControl("lblBeca")
            lbl.Attributes.Add("onclick", "AbrirPopUp('detallebeneficio.aspx?" & Cadena & "','500','600','no','no','yes');return(false);")
        End If
    End Sub

    Protected Sub cmdExportar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdExportar.Click
        Dim sb As StringBuilder = New StringBuilder()
        Dim SW As System.IO.StringWriter = New System.IO.StringWriter(sb)
        Dim htw As HtmlTextWriter = New HtmlTextWriter(SW)
        Dim Page As Page = New Page()
        Dim form As HtmlForm = New HtmlForm()
        Me.GridView1.EnableViewState = False
        Page.EnableEventValidation = False
        Page.DesignerInitialize()
        Page.Controls.Add(form)
        form.Controls.Add(Me.GridView1)
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

    'Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
    '    Me.Form.Attributes.Add("OnSubmit", "document.all.GridView1.style.display='none';document.all.tblCriterios.style.display='none';document.all.tblmensaje.style.display=''")
    'End Sub

    Protected Sub dpEscuela_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dpEscuela.SelectedIndexChanged
        If dpEscuela.SelectedValue <> -1 Then
            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()

            Dim cls As New ClsFunciones
            cls.CargarListas(Me.dpIngreso, obj.TraerDataTable("ConsultarCicloAcademico", "CIF", Me.dpEscuela.SelectedValue), "cicloIng_Alu", "cicloIng_Alu", "--TODOS--")
            cls = Nothing
            obj.CerrarConexion()
            obj = Nothing
        End If
    End Sub

    Protected Sub cmdBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdBuscar.Click
        'Response.Write("<script type='text/javascript'>MascaraEspera('1');</script>")
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        Try
            'Page.RegisterStartupScript("bloquea0", "<script type='text/javascript'>MascaraEspera('0');</script>")            
            obj.AbrirConexion()
            If Me.dpCiclo.SelectedValue >= 69 Then
                dt = obj.TraerDataTable("ACAD_MeritosEstudiante2019_v2", Me.dpEscuela.SelectedValue, Me.dpIngreso.SelectedValue, _
                                   Me.dpCiclo.SelectedValue, Me.dpFiltro.SelectedValue)
            Else
                dt = obj.TraerDataTable("ConsultarPonderadoOficial", Me.dpEscuela.SelectedValue, Me.dpIngreso.SelectedValue, _
                                   Me.dpCiclo.SelectedValue, Me.dpFiltro.SelectedValue)
            End If

            obj.CerrarConexion()

            Me.GridView1.DataSource = dt
            Me.GridView1.DataBind()

            Page.RegisterStartupScript("bloquea0", "<script type='text/javascript'>Ocultar();</script>")
        Catch ex As Exception
            Response.Write("Error: " & ex.Message)
        End Try
        'Response.Write("<script type='text/javascript'>MascaraEspera('0');</script>")
    End Sub
End Class