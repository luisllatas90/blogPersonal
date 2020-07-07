
Partial Class login_loginpersona
    Inherits System.Web.UI.Page

    Protected Sub GridView1_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GridView1.PageIndexChanging
        GridView1.PageIndex = e.NewPageIndex
        CargaGrid()
    End Sub

    Protected Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim fila As Data.DataRowView
            fila = e.Row.DataItem

            If fila.Row("estado") = False Then
                e.Row.Font.Strikeout = True
                e.Row.ForeColor = Drawing.Color.Red
            End If
        End If
    End Sub

    Protected Sub FormView1_ItemUpdating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.FormViewUpdateEventArgs) Handles FormView1.ItemUpdating

        Dim rptta As Integer
        Dim objLogin As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
        Try
            objLogin.IniciarTransaccion()
            rptta = objLogin.TraerValor("ActualizarPersonalLogin", CInt(e.NewValues.Item(0)), e.NewValues.Item(5).ToString.Trim) 'Cambia Ejecutar po TraerValor
            objLogin.TerminarTransaccion()

            If rptta = 0 Then
                Me.LblError.ForeColor = Drawing.color.red
                Me.LblError.Text = "*Aviso: Ya existe un usuario igual, no se actualizó"
            ElseIf rptta = 1 Then
                Me.LblError.ForeColor = Drawing.Color.green
                Me.LblError.Text = "*Aviso: Datos grabados con éxito"
            ElseIf rptta = 2 Then
                Me.LblError.ForeColor = Drawing.Color.orange 'skyblue
                Me.LblError.Text = "*Aviso: El usuario ingresado es el mismo"
            End If

            Me.GridView1.DataBind()
        Catch ex As Exception
            Me.LblError.ForeColor = Drawing.Color.Red
            Me.LblError.Text = "Error: " & ex.Message
        End Try
        e.Cancel = True

    End Sub

    Protected Sub GridView1_RowUpdated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewUpdatedEventArgs) Handles GridView1.RowUpdated
        'Page.RegisterStartupScript("Ejecuta1", "<script>$('#divGrid').removeClass('col-md-12').addClass('col-md-9');</script>")
        'Page.RegisterStartupScript("Ejecuta2", "<script>$('#divFormEdit').removeClass('col-md-0').addClass('col-md-3');</script>")
        'Page.RegisterStartupScript("Ejecuta3", "<script>$('#divFormEdit').removeClass('ocultar');</script>")
        'Page.RegisterStartupScript("Ejecuta4", "<script>$('#FormView1').show();</script>")
        mt_MostrarDivEdicion(True)
    End Sub

    Protected Sub GridView1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridView1.SelectedIndexChanged
        Me.LblError.Text = ""
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("id_per") Is Nothing Then
            Response.Redirect("../../../../sinacceso.html")
        End If

        If IsPostBack = False Then
            mt_MostrarDivEdicion(False)

            CargaTipoPersonal()
            CargaDedicacion()
            CargaCentroCosto()

            cboTipo.SelectedIndex = 0
            cboDedicacion.SelectedIndex = 0
            cboClave.SelectedIndex = 0
        End If
    End Sub    

    Private Sub CargaCentroCosto()        
        Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
        Dim dt As New Data.DataTable
        Try            
            dt = obj.TraerDataTable("PER_ListaCentroCostoPersonal")
            Me.cboCentroCosto.DataSource = dt
            Me.cboCentroCosto.DataTextField = "descripcion_cco"
            Me.cboCentroCosto.DataValueField = "codigo_cco"
            Me.cboCentroCosto.DataBind()
        Catch ex As Exception
            Me.LblError.ForeColor = Drawing.Color.Red
            Me.LblError.Text = "Error: " & ex.Message
        End Try
    End Sub

    Private Sub CargaTipoPersonal()
        Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
        Dim dt As New Data.DataTable
        Try
            dt = obj.TraerDataTable("ListaTipoPersonal")
            Me.cboTipo.DataSource = dt
            Me.cboTipo.DataTextField = "Descripcion_Tpe"
            Me.cboTipo.DataValueField = "Codigo_Tpe"
            Me.cboTipo.DataBind()
        Catch ex As Exception
            Me.LblError.ForeColor = Drawing.Color.Red
            Me.LblError.Text = "Error: " & ex.Message
        End Try
    End Sub

    Private Sub CargaDedicacion()
        Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
        Dim dt As New Data.DataTable
        Try
            dt = obj.TraerDataTable("SOL_ListaDedicacion", "C")
            Me.cboDedicacion.DataSource = dt
            Me.cboDedicacion.DataTextField = "descripcion"
            Me.cboDedicacion.DataValueField = "Codigo"
            Me.cboDedicacion.DataBind()
        Catch ex As Exception
            Me.LblError.ForeColor = Drawing.Color.Red
            Me.LblError.Text = "Error: " & ex.Message
        End Try
    End Sub

    Protected Sub btnBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBuscar.Click
        Me.GridView1.PageIndex = 0
        CargaGrid()
        'Page.RegisterStartupScript("Ejecuta1", "<script>$('#divGrid').removeClass('col-md-9').addClass('col-md-12');</script>")
        'Page.RegisterStartupScript("Ejecuta2", "<script>$('#divFormEdit').removeClass('col-md-3').addClass('col-md-0');</script>")
        'Page.RegisterStartupScript("Ejecuta3", "<script>$('#divFormEdit').addClass('ocultar');</script>")
        'Page.RegisterStartupScript("Ejecuta4", "<script>$('#FormView1').hide();</script>")
        mt_MostrarDivEdicion(False)
        Me.LblError.text = ""  'limpia
    End Sub

    Private Sub mt_MostrarDivEdicion(ByVal mostrar As Boolean)
        Try
            If mostrar Then
                Page.RegisterStartupScript("Ejecuta1", "<script>$('#divGrid').removeClass('col-md-12').addClass('col-md-9');</script>")
                Page.RegisterStartupScript("Ejecuta2", "<script>$('#divFormEdit').removeClass('col-md-0').addClass('col-md-3');</script>")
                Page.RegisterStartupScript("Ejecuta3", "<script>$('#divFormEdit').removeClass('ocultar');</script>")
                Page.RegisterStartupScript("Ejecuta4", "<script>$('#FormView1').show();</script>")
            Else
                Page.RegisterStartupScript("Ejecuta1", "<script>$('#divGrid').removeClass('col-md-9').addClass('col-md-12');</script>")
                Page.RegisterStartupScript("Ejecuta2", "<script>$('#divFormEdit').removeClass('col-md-3').addClass('col-md-0');</script>")
                Page.RegisterStartupScript("Ejecuta3", "<script>$('#divFormEdit').addClass('ocultar');</script>")
                Page.RegisterStartupScript("Ejecuta4", "<script>$('#FormView1').hide();</script>")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CargaGrid()
        If Not fu_ValidarCargarGrid() Then Exit Sub

        Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
        Dim dt As New Data.DataTable
        Try
            'Response.Write("ConsultarPersonalLogin_v2 " & Me.TextBox1.Text & "," & Me.cboEstado.SelectedValue _
            '               & ", " & Me.cboTipo.SelectedValue & ", " & IIf(Me.cboDedicacion.SelectedValue = -1, 0, Me.cboDedicacion.SelectedValue) _
            '               & "," & Me.cboCentroCosto.SelectedValue & "," & IIf(Me.chkClave.Checked = True, 1, 0))

            'dt = obj.TraerDataTable("ConsultarPersonalLogin_v3", Me.TextBox1.Text.ToString, _
            '                        Me.cboEstado.SelectedValue, _
            '                        Me.cboTipo.SelectedValue, _
            '                        IIf(Me.cboDedicacion.SelectedValue = -1, 0, Me.cboDedicacion.SelectedValue), _
            '                        Me.cboCentroCosto.SelectedValue, _
            '                        Me.cboClave.SelectedValue)

            'SELECCION MULTIPLE
            Dim codigo_tpe As String = String.Empty
            Dim todos_codigo_tpe As Boolean = False

            Dim codigo_ded As String = String.Empty
            Dim todos_codigo_ded As Boolean = False

            Dim dias_vencidos As String = String.Empty            

            'COMBO TIPO
            For Each _Item As ListItem In cboTipo.Items
                If _Item.Selected AndAlso _Item.Value = "0" Then
                    todos_codigo_tpe = True
                End If
            Next

            For Each _Item As ListItem In cboTipo.Items
                If (todos_codigo_tpe OrElse _Item.Selected) AndAlso _Item.Value <> "0" Then
                    If codigo_tpe.Length > 0 Then codigo_tpe &= ","
                    codigo_tpe &= _Item.Value
                End If
            Next

            'COMBO DEDICACIÓN
            For Each _Item As ListItem In cboDedicacion.Items
                If _Item.Selected AndAlso _Item.Value = "-1" Then
                    todos_codigo_ded = True
                End If
            Next

            For Each _Item As ListItem In cboDedicacion.Items
                If (todos_codigo_ded OrElse _Item.Selected) AndAlso _Item.Value <> "-1" Then
                    If codigo_ded.Length > 0 Then codigo_ded &= ","
                    codigo_ded &= _Item.Value
                End If
            Next

            'COMBO CLAVE
            For Each _Item As ListItem In cboClave.Items
                If _Item.Selected Then
                    If dias_vencidos.Length > 0 Then dias_vencidos &= ","
                    dias_vencidos &= _Item.Value
                End If
            Next

            'dt = obj.TraerDataTable("ConsultarPersonalLogin_v3", Me.TextBox1.Text.ToString, _
            '                        Me.cboEstado.SelectedValue, _
            '                        codigo_tpe, _
            '                        IIf(codigo_ded = -1, 0, codigo_ded), _
            '                        Me.cboCentroCosto.SelectedValue, _
            '                        dias_vencidos)

            dt = obj.TraerDataTable("CONF_PersonalLogin", _
                                    Me.TextBox1.Text.ToString, _
                                    Me.cboEstado.SelectedValue, _
                                    codigo_tpe, _
                                    codigo_ded, _
                                    Me.cboCentroCosto.SelectedValue, _
                                    dias_vencidos)

            Me.GridView1.DataSource = dt
            Me.GridView1.DataBind()

            Me.gvExportar.DataSource = dt
            Me.gvExportar.DataBind()

            Page.RegisterStartupScript("Mensaje", "<script>$('#spNroRegistros').text('Se encontraron " & dt.Rows.Count & " registros');</script>")
            'Page.RegisterStartupScript("Mensaje", "<script>$('#spNroRegistros').text('Se encontraron " & dt.Rows.Count & " <" & Me.cboClave.SelectedValue & ">   " & " registros');</script>")
        Catch ex As Exception
            Me.LblError.ForeColor = Drawing.Color.Red
            Me.LblError.Text = "Error: " & ex.Message
        End Try
    End Sub

    Protected Sub GridView1_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles GridView1.Sorting
        CargaGrid()
    End Sub

    Protected Sub btnExportar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExportar.Click
        Dim sb As StringBuilder = New StringBuilder()
        Dim SW As System.IO.StringWriter = New System.IO.StringWriter(sb)
        Dim htw As HtmlTextWriter = New HtmlTextWriter(SW)
        Dim Page As Page = New Page()
        Dim form As HtmlForm = New HtmlForm()
        Me.gvExportar.Visible = True
        Me.gvExportar.EnableViewState = False
        Page.EnableEventValidation = False
        Page.DesignerInitialize()
        Page.Controls.Add(form)
        form.Controls.Add(Me.gvExportar)
        Page.RenderControl(htw)
        Response.Clear()
        Response.Buffer = True
        Response.ContentType = "application/vnd.ms-excel"
        Response.AddHeader("Content-Disposition", "attachment;filename=ListaUsuarioAD" & ".xls")
        Response.Charset = "UTF-8"
        Response.ContentEncoding = Encoding.Default
        Response.Write(sb.ToString())
        Response.End()
        Me.gvExportar.Visible = False
    End Sub

    Protected Sub cboTipo_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboTipo.SelectedIndexChanged
        Try
            Dim lb_todos As Boolean = False

            For Each _Item As ListItem In cboTipo.Items
                If _Item.Selected AndAlso _Item.Value = "0" Then
                    lb_todos = True
                End If
            Next

            If lb_todos Then
                For Each _Item As ListItem In cboTipo.Items
                    If _Item.Selected AndAlso _Item.Value <> "0" Then
                        _Item.Selected = False
                    End If
                Next
            End If

            mt_MostrarDivEdicion(False)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Protected Sub cboDedicacion_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboDedicacion.SelectedIndexChanged
        Try
            Dim lb_todos As Boolean = False

            For Each _Item As ListItem In cboDedicacion.Items
                If _Item.Selected AndAlso _Item.Value = "-1" Then
                    lb_todos = True
                End If
            Next

            If lb_todos Then
                For Each _Item As ListItem In cboDedicacion.Items
                    If _Item.Selected AndAlso _Item.Value <> "-1" Then
                        _Item.Selected = False
                    End If
                Next
            End If

            mt_MostrarDivEdicion(False)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Protected Sub cboClave_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboClave.SelectedIndexChanged
        Try
            Dim lb_todos As Boolean = False

            For Each _Item As ListItem In cboClave.Items
                If _Item.Selected AndAlso _Item.Value = "-1" Then
                    lb_todos = True
                End If
            Next

            If lb_todos Then
                For Each _Item As ListItem In cboClave.Items
                    If _Item.Selected AndAlso _Item.Value <> "-1" Then
                        _Item.Selected = False
                    End If
                Next
            End If

            mt_MostrarDivEdicion(False)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Function fu_ValidarCargarGrid() As Boolean
        Try
            'SELECCION MULTIPLE
            Dim codigo_tpe As String = String.Empty
            Dim codigo_ded As String = String.Empty
            Dim dias_vencidos As String = String.Empty

            'COMBO TIPO
            For Each _Item As ListItem In cboTipo.Items
                If _Item.Selected Then
                    If codigo_tpe.Length > 0 Then codigo_tpe &= ","
                    codigo_tpe &= _Item.Value
                End If
            Next

            'COMBO DEDICACIÓN
            For Each _Item As ListItem In cboDedicacion.Items
                If _Item.Selected Then
                    If codigo_ded.Length > 0 Then codigo_ded &= ","
                    codigo_ded &= _Item.Value
                End If
            Next

            'COMBO CLAVE
            For Each _Item As ListItem In cboClave.Items
                If _Item.Selected Then
                    If dias_vencidos.Length > 0 Then dias_vencidos &= ","
                    dias_vencidos &= _Item.Value
                End If
            Next

            If String.IsNullOrEmpty(dias_vencidos) Then
                Page.RegisterStartupScript("Mensaje", "<script>$('#spNroRegistros').text('Seleccione un criterio de clave');</script>")
                Return False
            ElseIf String.IsNullOrEmpty(codigo_tpe) Then
                Page.RegisterStartupScript("Mensaje", "<script>$('#spNroRegistros').text('Seleccione un tipo');</script>")
                Return False
            ElseIf String.IsNullOrEmpty(codigo_ded) Then
                Page.RegisterStartupScript("Mensaje", "<script>$('#spNroRegistros').text('Seleccione una dedicación');</script>")
                Return False
            End If

            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

End Class
