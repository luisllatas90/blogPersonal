
Partial Class administrativo_pec_frmSolicitarAmbiente
    Inherits System.Web.UI.Page

    Sub CargarDatos()
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        ClsFunciones.LlenarListas(Me.ddlAudio, obj.TraerDataTable("Ambiente_ListarCaracteristicas", "audio", 0, "T"), "codigo_camb", "descripcion_cam")
        ClsFunciones.LlenarListas(Me.ddlDis, obj.TraerDataTable("Ambiente_ListarCaracteristicas", "distribución", 0, "T"), "codigo_camb", "descripcion_cam")
        ClsFunciones.LlenarListas(Me.ddlOtros, obj.TraerDataTable("Ambiente_ListarCaracteristicas", "otros", 0, "T"), "codigo_camb", "descripcion_cam")
        ClsFunciones.LlenarListas(Me.ddlSillas, obj.TraerDataTable("Ambiente_ListarCaracteristicas", "sillas", 0, "T"), "codigo_camb", "descripcion_cam")
        ClsFunciones.LlenarListas(Me.ddlVideo, obj.TraerDataTable("Ambiente_ListarCaracteristicas", "video", 0, "T"), "codigo_camb", "descripcion_cam")
        ClsFunciones.LlenarListas(Me.ddlVenti, obj.TraerDataTable("Ambiente_ListarCaracteristicas", "ventilación", 0, "T"), "codigo_camb", "descripcion_cam")
        Me.ddlHorarios.DataSource = obj.TraerDataTable("HorarioPE_Consultar", Session("h_codigo_cup"), "S")
        Me.ddlHorarios.DataTextField = "dia"
        Me.ddlHorarios.DataValueField = "codigo_lho"
        Me.ddlHorarios.DataBind()
        If Session("h_codigolho") <> "0" Then
            Me.ddlHorarios.SelectedValue = Session("h_codigolho")
        End If

        obj.CerrarConexion()
        obj = Nothing
        Me.Label1.Text = Session("h_nombre_cur")
    End Sub

    Protected Sub DropDownList6_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlOtros.SelectedIndexChanged

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            CargarDatos()
        End If

        'Para asignar o solicitar
        If Page.Request.QueryString("codigo_amb") > 0 And Page.Request.QueryString("estado") <> "" Then
            Dim codigo_lho As Integer = 0
            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            codigo_lho = Me.ddlHorarios.SelectedValue
            obj.AbrirConexion()
            obj.Ejecutar("HorarioPE_RegistrarAmbienteSol", codigo_lho, CInt(Page.Request.QueryString("codigo_amb")), Page.Request.QueryString("estado"))
            obj.CerrarConexion()
            obj = Nothing
            'CargarDatos()
            'Button1_Click(sender, e)
            ' Response.Redirect("http://server-test/campusvirtual/personal/administrativo/pec/frmprogramacioncademicaeve.aspx?mod=" & Session("h_mod") & "&id=" & Session("h_id") & "&ctf=" & Session("h_ctf"))
            'pec/frmprogramacioncademicaeve.aspx?mod=6&id=684&ctf=1
            'history.back(1)
            Session("h_codigolho") = 0
            Me.RegisterStartupScript("cerrar", "<script>alert('Ambiente registrado'); window.close();</script>")
        End If
    End Sub

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString

        Dim tb As New Data.DataTable
        Dim tbAmb As New Data.DataTable
        Dim idsamb As String = ""
        obj.AbrirConexion()


        tb = obj.TraerDataTable("HorarioPE_ConsultarAsignacionAmbienteSol", ddlHorarios.SelectedValue, ddlAudio.SelectedValue, ddlVideo.SelectedValue, ddlSillas.SelectedValue, ddlDis.SelectedValue, ddlOtros.SelectedValue, "C", Me.ddlVenti.SelectedValue)

        ' Response.Write(Me.ddlHorarios.SelectedValue & "," & ddlAudio.SelectedValue & "," & ddlVideo.SelectedValue & "," & ddlSillas.SelectedValue & "," & ddlDis.SelectedValue & "," & ddlOtros.SelectedValue & "," & "C")

        obj.CerrarConexion()
        If tb.Rows.Count Then
            For i As Integer = 0 To tb.Rows.Count - 1
                idsamb = idsamb & tb.Rows(i).Item("codigo_amb").ToString & ","
            Next
            idsamb = idsamb.ToString.Substring(0, idsamb.Length - 1)
            obj.AbrirConexion()
            Me.gridAmbientes.DataSource = obj.TraerDataTable("Ambiente_ListarAmbienteCaracSol", idsamb)
            Me.gridAmbientes.DataBind()
            obj.CerrarConexion()
        Else
            Me.gridAmbientes.DataSource = Nothing
            Me.gridAmbientes.DataBind()
        End If

        '#Sin filtros
        tb.Dispose()
        tb = New Data.DataTable
        obj.AbrirConexion()
        tb = obj.TraerDataTable("HorarioPE_ConsultarAsignacionAmbienteSol", ddlHorarios.SelectedValue, ddlAudio.SelectedValue, ddlVideo.SelectedValue, ddlSillas.SelectedValue, ddlDis.SelectedValue, ddlOtros.SelectedValue, idsamb)
        obj.CerrarConexion()
        idsamb = ""
        If tb.Rows.Count Then
            For i As Integer = 0 To tb.Rows.Count - 1
                idsamb = idsamb & tb.Rows(i).Item("codigo_amb").ToString & ","
            Next
            If idsamb <> "" Then
                idsamb = idsamb.ToString.Substring(0, idsamb.Length - 1)
            End If
            obj.AbrirConexion()
            Me.gridotrosAmbientes.DataSource = obj.TraerDataTable("Ambiente_ListarAmbienteCaracSol", idsamb)
            Me.gridotrosAmbientes.DataBind()
            obj.CerrarConexion()
        Else
            Me.gridotrosAmbientes.DataSource = Nothing
            Me.gridotrosAmbientes.DataBind()
        End If


        obj = Nothing
    End Sub

    Protected Sub gridAmbientes_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridAmbientes.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            If gridAmbientes.DataKeys(e.Row.RowIndex).Values("accion").ToString > 0 Then
                Dim tb As New Data.DataTable
                Dim ultimo As Integer = e.Row.Cells.Count
                tb = gridAmbientes.DataSource

                For i As Integer = 2 To tb.Columns.Count - 1 '+ 1
                    If e.Row.Cells(i - 1).Text = "1" Then
                        e.Row.Cells(i - 1).Text = "<img src='image/yes.png' title=""si"">"
                    End If
                    If e.Row.Cells(i - 1).Text = "0" Then
                        e.Row.Cells(i - 1).Text = "<img src='image/no.png'  title=""no"" >"
                    End If
                    'Preferencial
                    If e.Row.Cells(0).Text = "S" Then
                        e.Row.Cells(0).Text = "<img src='image/star.png' title='Ambiente preferencial'>"
                        e.Row.Cells(ultimo - 1).Text = "<a href=""" & "frmSolicitarAmbiente.aspx?codigo_amb=" & gridAmbientes.DataKeys(e.Row.RowIndex).Values("accion").ToString & "&estado=P""><img src=""image/savego.png"" style=""border:0px;"" alt=""Solicitar""></img> Solicitar</a>"
                    End If
                    'Normal
                    If e.Row.Cells(0).Text = "N" Then
                        e.Row.Cells(0).Text = "<img src='image/door.png' title='Ambiente'>"
                        e.Row.Cells(ultimo - 1).Text = "<a href=""" & "frmSolicitarAmbiente.aspx?codigo_amb=" & gridAmbientes.DataKeys(e.Row.RowIndex).Values("accion").ToString & "&estado=A""><img src=""image/save.png"" style=""border:0px;"" alt=""Asignar""></img> Asignar</a>"
                    End If
                Next
                'No mostrar nombres reales
                e.Row.Cells(2).Text = "Ambiente Disponible N° " & (e.Row.RowIndex + 1).ToString

                tb.Dispose()
            End If
        End If
    End Sub

    Protected Sub gridOtrosAmbientes_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridotrosAmbientes.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            If gridotrosAmbientes.DataKeys(e.Row.RowIndex).Values("accion").ToString > 0 Then
                Dim tb As New Data.DataTable
                Dim ultimo As Integer = e.Row.Cells.Count
                tb = gridotrosAmbientes.DataSource

                For i As Integer = 2 To tb.Columns.Count - 1 '+ 1
                    If e.Row.Cells(i - 1).Text = "1" Then
                        e.Row.Cells(i - 1).Text = "<img src='image/yes.png'>"
                    End If
                    If e.Row.Cells(i - 1).Text = "0" Then
                        e.Row.Cells(i - 1).Text = "<img src='image/no.png'>"
                    End If
                    'Preferencial
                    If e.Row.Cells(0).Text = "S" Then
                        e.Row.Cells(0).Text = "<img src='image/star.png' title='Ambiente preferencial'>"
                        e.Row.Cells(ultimo - 1).Text = "<a href=""" & "frmSolicitarAmbiente.aspx?codigo_amb=" & gridotrosAmbientes.DataKeys(e.Row.RowIndex).Values("accion").ToString & "&estado=P""><img src=""image/savego.png"" style=""border:0px;"" alt=""Solicitar""></img> Solicitar</a>"
                    End If
                    'Normal
                    If e.Row.Cells(0).Text = "N" Then
                        e.Row.Cells(0).Text = "<img src='image/door.png' title='Ambiente'>"
                        e.Row.Cells(ultimo - 1).Text = "<a href=""" & "frmSolicitarAmbiente.aspx?codigo_amb=" & gridotrosAmbientes.DataKeys(e.Row.RowIndex).Values("accion").ToString & "&estado=R""><img src=""image/save.png"" style=""border:0px;"" alt=""Asignar""></img> Asignar</a>"
                    End If
                Next
                'No mostrar nombres reales
                e.Row.Cells(1).Text = "Ambiente Disponible N° " & (e.Row.RowIndex + 1).ToString

                tb.Dispose()
            End If
        End If
    End Sub


End Class
