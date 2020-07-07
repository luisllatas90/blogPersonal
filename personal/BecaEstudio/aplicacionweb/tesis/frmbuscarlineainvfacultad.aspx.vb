
Partial Class frmbuscarresponsabletesis
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If IsPostBack = False Then
            Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
            Dim tbl As New Data.DataTable
            Dim facultadautor As Integer

            tbl = obj.TraerDataTable("TES_ConsultarResponsableTesis", 4, Request.QueryString("codigo_tes"), 0, 0)
            facultadautor = tbl.Rows(0).Item("facultadautor")
            ClsFunciones.LlenarListas(Me.dpFacultad, tbl, "codigo_fac", "nombre_fac")
            dpFacultad.SelectedValue = facultadautor
            Me.BuscarLineasInvestigacion(facultadautor)
            obj = Nothing

            Me.cmdRegresar.OnClientClick = "location.href='lsttesis.aspx?id=" & Request.QueryString("id") & "';return(false)"
        End If
    End Sub

    Protected Sub dpFacultad_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dpFacultad.SelectedIndexChanged
        BuscarLineasInvestigacion(dpFacultad.SelectedValue)
    End Sub
    Private Sub BuscarLineasInvestigacion(ByVal id As Integer)
        Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
        Dim tbl As New Data.DataTable

        tbl = obj.TraerDataTable("TES_ConsultarAreaInvestigacionTesis", 0, Request.QueryString("codigo_tes"), 3, id)

        Me.GridView1.DataSource = tbl
        Me.GridView1.DataBind()

        If tbl.Rows.Count > 0 Then
            Me.cmdGuardar.Visible = True
        End If
        obj = Nothing
    End Sub
    Protected Sub cmdGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdGuardar.Click
        Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
        Dim chk As CheckBox

        Try
            obj.IniciarTransaccion()

            obj.Ejecutar("TES_ModificarTesis", Request.QueryString("codigo_tes"), Session("titulo_tes"), Session("problema_tes"), Session("resumen_tes"), CDate(Session("fechainicio_tes")), CDate(Session("fechafin_tes")), Request.QueryString("id"))

            For Each fila As GridViewRow In Me.GridView1.Rows
                chk = CType(fila.FindControl("chk"), CheckBox)
                'Guardas las lineas marcadas
                If chk.Checked = True Then
                    obj.Ejecutar("TES_AgregarAreaInvestigacionTesis", Me.GridView1.DataKeys(Convert.ToInt32(fila.RowIndex)).Value, Request.QueryString("codigo_tes"))
                End If
            Next
            obj.TerminarTransaccion()
            Response.Redirect("frmtesis.aspx?codigo_tes=" & Request.QueryString("codigo_tes") & "&accion=M&id=" & Request.QueryString("id"))

        Catch ex As Exception
            obj.AbortarTransaccion()
            Me.lblError.Text = "Ha ocurrido un error en la transacción<br>" & ex.Message
            obj = Nothing
        End Try
    End Sub
    'Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
    '    Dim i As Integer
    '    Dim row As GridViewRow
    '    Dim ch As CheckBox
    '    For i = 0 To GridView1.Rows.Count - 1
    '        row = GridView1.Rows(i)
    '        If row.RowType = DataControlRowType.DataRow Then
    '            ch = row.FindControl("chk")
    '            ch.ID = "chk" & i + 1
    '            ch.Attributes.Add("onclick", "javascript:valida(this.name)")
    '        End If
    '    Next
    'End Sub

    '    function validate(form){
    'for(var i = 0; i < form.choice.length; i++){
    'if(form.choice[i].checked)return true;
    '}
    'alert('Debes seleccionar al menos una opcion');
    'return false;
    '} 
End Class
