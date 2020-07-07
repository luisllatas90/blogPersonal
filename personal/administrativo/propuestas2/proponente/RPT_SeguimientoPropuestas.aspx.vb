
Partial Class administrativo_propuestas2_proponente_RPT_SeguimientoPropuestas
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Request.QueryString("id") = "" Then
            Response.Redirect("../../../../sinacceso.html")
        End If

        If (IsPostBack = False) Then
            Call wf_cargarEjercicioPresupuestal()
            Call wf_cargarPOA()
            Call wf_TipoPropuesta()
        End If
    End Sub

    Sub wf_cargarEjercicioPresupuestal()
        Dim dtt As New Data.DataTable
        Dim obj As New ClsConectarDatos

        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        dtt = obj.TraerDataTable("PEI_ListarEjercicioPresupuestal")
        obj.CerrarConexion()

        Me.ddl_EjercicioPresupuestal.DataSource = dtt
        Me.ddl_EjercicioPresupuestal.DataTextField = "descripcion"
        Me.ddl_EjercicioPresupuestal.DataValueField = "codigo"
        Me.ddl_EjercicioPresupuestal.DataBind()
        dtt.Dispose()
        obj = Nothing

        Me.ddl_EjercicioPresupuestal.SelectedIndex = Me.ddl_EjercicioPresupuestal.Items.Count - 1
    End Sub

    Sub wf_cargarPOA()
        Dim dtt As New Data.DataTable
        Dim obj As New ClsConectarDatos

        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        dtt = obj.TraerDataTable("POA_ListaPoasxInstanciaEstado ", 0, ddl_EjercicioPresupuestal.SelectedValue, "TT", Request.QueryString("id"), Request.QueryString("ctf"))
        obj.CerrarConexion()

        Me.ddl_POA.DataSource = dtt
        Me.ddl_POA.DataTextField = "descripcion"
        Me.ddl_POA.DataValueField = "codigo"
        Me.ddl_POA.DataBind()
        dtt.Dispose()
        obj = Nothing
    End Sub

    Sub wf_TipoPropuesta()
        Dim dtt As New Data.DataTable
        Dim obj As New ClsConectarDatos

        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        dtt = obj.TraerDataTable("ConsultarTipoPropuestas", "TT", 0)
        obj.CerrarConexion()

        Me.ddl_TipoPropuesta.DataSource = dtt
        Me.ddl_TipoPropuesta.DataTextField = "Descripcion_tpr"
        Me.ddl_TipoPropuesta.DataValueField = "codigo_tpr"
        Me.ddl_TipoPropuesta.DataBind()
        dtt.Dispose()
        obj = Nothing
    End Sub

    Protected Sub cmd_Consultar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmd_Consultar.Click
        Me.dgvPropuestas.DataSource = Nothing
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        Try
            obj.AbrirConexion()

            Dim codigo_poa As String = IIf(ddl_POA.SelectedValue = 0, "%", ddl_POA.SelectedValue)
            Dim codigo_tpr As String = IIf(ddl_TipoPropuesta.SelectedValue = 0, "%", ddl_TipoPropuesta.SelectedValue)
            Dim codigo_per As String = Request.QueryString("id")
            Dim instancia As String = IIf(ddl_InstanciaPropuesta.SelectedValue = "0", "%", ddl_InstanciaPropuesta.SelectedValue)

            'Response.Write("PRP_SeguimientoPropuestas_POA " & ddl_EjercicioPresupuestal.SelectedItem.ToString & ",'" & codigo_poa & "','%','" & codigo_per & "'" & instancia & "','" & codigo_tpr & "'," & Request.QueryString("ctf"))
            dt = obj.TraerDataTable("PRP_SeguimientoPropuestas_POA", ddl_EjercicioPresupuestal.SelectedItem.ToString, codigo_poa, "%", codigo_per, instancia, codigo_tpr, Request.QueryString("ctf"))
            obj.CerrarConexion()

            Me.dgvPropuestas.DataSource = dt
            Me.dgvPropuestas.DataBind()

            If dgvPropuestas.Rows.Count = 0 Then
                lbl_numeroItems.Text = "NO se encontraron Propuestas."
            Else
                lbl_numeroItems.Text = "Se encontraron: " & dgvPropuestas.Rows.Count.ToString & " Propuestas."
            End If

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub dgvPropuestas_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgvPropuestas.SelectedIndexChanged
        Call cargarRevisores()
    End Sub

    Sub cargarRevisores()
        Dim codigo_prp As Integer = Me.dgvPropuestas.Rows(Me.dgvPropuestas.SelectedIndex).Cells(0).Text()
        Response.Redirect("revisores_POA.aspx?codigo_prp=" & codigo_prp)
    End Sub

    Protected Sub cmd_Exportar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmd_Exportar.Click
        If dgvPropuestas.Rows.Count > 0 Then
            Dim sb As StringBuilder = New StringBuilder()
            Dim SW As System.IO.StringWriter = New System.IO.StringWriter(sb)
            Dim htw As HtmlTextWriter = New HtmlTextWriter(SW)
            Dim Page As Page = New Page()
            Dim form As HtmlForm = New HtmlForm()
            Me.dgvPropuestas.EnableViewState = False
            Page.EnableEventValidation = False
            Page.DesignerInitialize()
            Page.Controls.Add(form)
            form.Controls.Add(Me.dgvPropuestas)
            Page.RenderControl(htw)
            Response.Clear()
            Response.Buffer = True
            Response.ContentType = "application/vnd.ms-excel"
            Response.AddHeader("Content-Disposition", "attachment;filename=Propuestas" & ".xls")
            Response.Charset = "UTF-8"
            Response.ContentEncoding = Encoding.Default
            Response.Write(sb.ToString())
            Response.End()
        Else
            Dim mensaje As String = ""
            'mensaje = "<body style=" & "background-color:powderblue;" & ">"
            'mensaje = mensaje + "<h1>This is a heading</h1>"
            'mensaje = mensaje + "<p>This is a paragraph.</p>"
            'mensaje = mensaje + "</body>"

            mensaje = "<h3 style=" & "color:blue;" & ">No se puede expotar, NO se encontraron registros</h3>"
            Response.Write(mensaje)
        End If
    End Sub
End Class
