
Partial Class GestionCurricular_FrmConfigurarVRA
    Inherits System.Web.UI.Page

    Private cod_user As Integer '= 684
    Private obj As ClsConectarDatos

    Public Enum MessageType
        Success
        [Error]
        Info
        Warning
    End Enum

#Region "Eventos"

    Public Sub New()
        If obj Is Nothing Then
            obj = New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If (Session("id_per") Is Nothing) Then
                Response.Redirect("../../../sinacceso.html")
            End If
            cod_user = Session("id_per") 'Request.QueryString("id")
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub Page_LoadComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.LoadComplete
        If IsPostBack = False Then
            Dim dtFecha As Data.DataTable = New Data.DataTable("dtFecha")
            dtFecha.Columns.Add("codigo")
            dtFecha.Columns.Add("descripcion")
            dtFecha.Columns.Add("fecha")

            Call mt_CargarSemestre()
            Call mt_CargarCortes(Me.ddlSemestre.SelectedValue, Me.ddlTipoCorte.SelectedValue)
            Call mt_CargarCondiciones()
            Call mt_CargarNiveles()
        End If
    End Sub

    Protected Sub ddlSemestre_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlSemestre.SelectedIndexChanged
        Call mt_GetLastWeek()
        Call mt_CargarCortes(Me.ddlSemestre.SelectedValue, Me.ddlTipoCorte.SelectedValue)
    End Sub

    Protected Sub ddlTipoCorte_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlTipoCorte.SelectedIndexChanged
        Call mt_CargarCortes(Me.ddlSemestre.SelectedValue, Me.ddlTipoCorte.SelectedValue)
    End Sub

    Protected Sub ddlNewSemana_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim row As GridViewRow = TryCast((TryCast(sender, DropDownList)).NamingContainer, GridViewRow)
        Dim ddl As DropDownList
        Dim txt As TextBox
        Dim codigo_cor As Integer = 0

        If row.RowIndex > -1 Then
            ddl = CType(gvCorte.Rows(row.RowIndex).FindControl("ddlSemana"), DropDownList)
            txt = CType(gvCorte.Rows(row.RowIndex).FindControl("txtFecha"), TextBox)
            codigo_cor = gvCorte.DataKeys(row.RowIndex).Item("codigo_cor").ToString()
        Else
            ddl = CType(gvCorte.FooterRow.FindControl("ddlNewSemana"), DropDownList)
            txt = CType(gvCorte.FooterRow.FindControl("txtNewFecha"), TextBox)
        End If

        If ddl.SelectedIndex > 0 Then
            Call fc_GetSemanas(codigo_cor)

            Dim dt As Data.DataTable = TryCast(ViewState("dtFecha"), Data.DataTable)
            Dim dr As Data.DataRow() = dt.Select("codigo = " & ddl.SelectedValue.ToString)

            If dr.Length > 0 Then
                txt.Text = dr(0).Item(2).ToString
            End If
        Else
            txt.Text = ""
        End If

        updCorte.Update()
    End Sub

#End Region

#Region "Cortes"

    Protected Sub gvCorte_RowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs) Handles gvCorte.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim ddl As DropDownList = CType(e.Row.FindControl("ddlSemana"), DropDownList)

            If Not ddl Is Nothing Then
                ddl.DataSource = fc_GetSemanas(gvCorte.DataKeys(e.Row.RowIndex).Item("codigo_cor").ToString)
                ddl.DataValueField = "codigo"
                ddl.DataTextField = "descripcion"
                ddl.DataBind()

                'Agregar fila en blanco
                ddl.Items.Insert(0, New ListItem("[-- Seleccione una Semana --]", "-1"))
                ddl.SelectedValue = gvCorte.DataKeys(e.Row.RowIndex).Item("numeroSemana_cor").ToString()
            End If
        End If

        If e.Row.RowType = DataControlRowType.Footer Then
            Dim ddl As DropDownList = CType(e.Row.FindControl("ddlNewSemana"), DropDownList)

            If ddl IsNot Nothing Then
                ddl.DataSource = fc_GetSemanas("0")
                ddl.DataValueField = "codigo"
                ddl.DataTextField = "descripcion"
                ddl.DataBind()

                'Agregar fila en blanco
                ddl.Items.Insert(0, New ListItem("[-- Seleccione una Semana --]", "-1"))
            End If
        End If
    End Sub

    Protected Sub gvCorte_RowEditing(ByVal sender As Object, ByVal e As GridViewEditEventArgs)
        gvCorte.EditIndex = e.NewEditIndex
        Call mt_CargarCortes(Me.ddlSemestre.SelectedValue, Me.ddlTipoCorte.SelectedValue)
    End Sub

    Protected Sub OnNewCorte(ByVal sender As Object, ByVal e As EventArgs)
        Try
            Dim txtFecha As TextBox = CType(gvCorte.FooterRow.FindControl("txtNewFecha"), TextBox)
            Dim ddlSemana As DropDownList = CType(gvCorte.FooterRow.FindControl("ddlNewSemana"), DropDownList)

            If fc_ValidarCorte(txtFecha, ddlSemana) Then
                obj.AbrirConexion()
                obj.Ejecutar("DEA_RegistrarCorteSemestre", ddlSemestre.SelectedValue, ddlTipoCorte.SelectedValue, ddlSemana.SelectedValue, txtFecha.Text, cod_user)
                obj.CerrarConexion()

                gvCorte.EditIndex = -1
                Call mt_CargarCortes(Me.ddlSemestre.SelectedValue, Me.ddlTipoCorte.SelectedValue)

                Call mt_ShowMessage("Corte registrado con éxito", MessageType.Success)
            End If
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", ""), MessageType.Warning)
        End Try
    End Sub

    Protected Sub OnUpdateCorte(ByVal sender As Object, ByVal e As EventArgs)
        Try
            Dim row As GridViewRow = TryCast((TryCast(sender, LinkButton)).NamingContainer, GridViewRow)

            Dim txtFecha As TextBox = CType(gvCorte.Rows(row.RowIndex).FindControl("txtFecha"), TextBox)
            Dim ddlSemana As DropDownList = CType(gvCorte.Rows(row.RowIndex).FindControl("ddlSemana"), DropDownList)

            If fc_ValidarCorte(txtFecha, ddlSemana) Then
                Dim dt As New Data.DataTable
                Dim codigo_cor As String = gvCorte.DataKeys(row.RowIndex).Item("codigo_cor").ToString

                obj.AbrirConexion()
                dt = obj.TraerDataTable("DEA_ActualizarCorteSemestre", codigo_cor, ddlSemestre.SelectedValue, ddlTipoCorte.SelectedValue, ddlSemana.SelectedValue, txtFecha.Text, cod_user)
                obj.CerrarConexion()

                If dt.Rows.Count > 0 Then
                    If dt.Rows(0).Item(0).ToString.Equals("1") Then
                        gvCorte.EditIndex = -1
                        Call mt_CargarCortes(Me.ddlSemestre.SelectedValue, Me.ddlTipoCorte.SelectedValue)
                        Call mt_ShowMessage(dt.Rows(0).Item(1).ToString, MessageType.Success)
                    Else
                        Call mt_ShowMessage(dt.Rows(0).Item(1).ToString, MessageType.Info)
                    End If
                End If
            End If
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", ""), MessageType.Warning)
        End Try
    End Sub

    Protected Sub OnCancelCorte(ByVal sender As Object, ByVal e As EventArgs)
        gvCorte.EditIndex = -1
        Call mt_CargarCortes(Me.ddlSemestre.SelectedValue, Me.ddlTipoCorte.SelectedValue)
    End Sub

    Protected Sub OnDeleteCorte(ByVal sender As Object, ByVal e As EventArgs)
        Dim row As GridViewRow = TryCast((TryCast(sender, LinkButton)).NamingContainer, GridViewRow)
        Dim codigo_cor As String = gvCorte.DataKeys(row.RowIndex).Item("codigo_cor").ToString

        If Not String.IsNullOrEmpty(codigo_cor) Then
            Dim dt As New Data.DataTable

            obj.AbrirConexion()
            dt = obj.TraerDataTable("DEA_EliminarCorteSemestre", Me.ddlSemestre.SelectedValue, ddlTipoCorte.SelectedValue, codigo_cor, cod_user)
            obj.CerrarConexion()

            If dt.Rows.Count > 0 Then
                If dt.Rows(0).Item(0).ToString.Equals("1") Then
                    gvCorte.EditIndex = -1
                    Call mt_CargarCortes(Me.ddlSemestre.SelectedValue, Me.ddlTipoCorte.SelectedValue)
                    Call mt_ShowMessage(dt.Rows(0).Item(1).ToString, MessageType.Success)
                Else
                    Call mt_ShowMessage(dt.Rows(0).Item(1).ToString, MessageType.Info)
                End If
            End If
        Else
            Call mt_ShowMessage("No existe código de corte para eliminar", MessageType.Info)
        End If
    End Sub

#End Region

#Region "Condiciones"

    Protected Sub gvCondicion_RowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs) Handles gvCondicion.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim ddl As DropDownList = CType(e.Row.FindControl("ddlTipoCon"), DropDownList)

            If Not ddl Is Nothing Then
                'Agregar fila en blanco
                ddl.Items.Insert(0, New ListItem("[-- Seleccione un Tipo --]", "-1"))
                ddl.SelectedValue = gvCondicion.DataKeys(e.Row.RowIndex).Item("tipo").ToString()
            End If
        End If

        If e.Row.RowType = DataControlRowType.Footer Then
            Dim ddl As DropDownList = CType(e.Row.FindControl("ddlNewTipoCon"), DropDownList)

            If ddl IsNot Nothing Then
                'Agregar fila en blanco
                ddl.Items.Insert(0, New ListItem("[-- Seleccione un Tipo --]", "-1"))
            End If
        End If
    End Sub

    Protected Sub gvCondicion_RowEditing(ByVal sender As Object, ByVal e As GridViewEditEventArgs)
        gvCondicion.EditIndex = e.NewEditIndex
        Call mt_CargarCondiciones()
    End Sub

    Protected Sub OnNewCondicion(ByVal sender As Object, ByVal e As EventArgs)
        Try
            Dim ddlTipoCon As DropDownList = CType(gvCondicion.FooterRow.FindControl("ddlNewTipoCon"), DropDownList)
            Dim txtDescripcionCon As TextBox = CType(gvCondicion.FooterRow.FindControl("txtNewDescripcionCon"), TextBox)

            If fc_ValidarCondicion(ddlTipoCon, txtDescripcionCon) Then
                obj.AbrirConexion()
                obj.Ejecutar("DEA_RegistrarCondicionEstudiante", ddlTipoCon.SelectedValue, txtDescripcionCon.Text, cod_user)
                obj.CerrarConexion()

                gvCondicion.EditIndex = -1
                Call mt_CargarCondiciones()

                Call mt_ShowMessage("Condición del Estudiante registrado con éxito", MessageType.Success)
            End If
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", ""), MessageType.Warning)
        End Try
    End Sub

    Protected Sub OnUpdateCondicion(ByVal sender As Object, ByVal e As EventArgs)
        Try
            Dim row As GridViewRow = TryCast((TryCast(sender, LinkButton)).NamingContainer, GridViewRow)

            Dim ddlTipoCon As DropDownList = CType(gvCondicion.Rows(row.RowIndex).FindControl("ddlTipoCon"), DropDownList)
            Dim txtDescripcionCon As TextBox = CType(gvCondicion.Rows(row.RowIndex).FindControl("txtDescripcionCon"), TextBox)

            If fc_ValidarCondicion(ddlTipoCon, txtDescripcionCon) Then
                Dim codigo_con As String = gvCondicion.DataKeys(row.RowIndex).Item("codigo_con").ToString

                obj.AbrirConexion()
                obj.Ejecutar("DEA_ActualizarCondicionEstudiante", codigo_con, ddlTipoCon.SelectedValue, txtDescripcionCon.Text, cod_user)
                obj.CerrarConexion()

                gvCondicion.EditIndex = -1
                Call mt_CargarCondiciones()

                Call mt_ShowMessage("Condición del Estudiante actualizado con éxito", MessageType.Success)
            End If
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", ""), MessageType.Warning)
        End Try
    End Sub

    Protected Sub OnCancelCondicion(ByVal sender As Object, ByVal e As EventArgs)
        gvCondicion.EditIndex = -1
        Call mt_CargarCondiciones()
    End Sub

    Protected Sub OnDeleteCondicion(ByVal sender As Object, ByVal e As EventArgs)
        Dim row As GridViewRow = TryCast((TryCast(sender, LinkButton)).NamingContainer, GridViewRow)
        Dim codigo_con As String = gvCondicion.DataKeys(row.RowIndex).Item("codigo_con").ToString

        If Not String.IsNullOrEmpty(codigo_con) Then
            obj.AbrirConexion()
            obj.Ejecutar("DEA_EliminarCondicionEstudiante", codigo_con, cod_user)
            obj.CerrarConexion()

            gvCondicion.EditIndex = -1
            Call mt_CargarCondiciones()
            Call mt_ShowMessage("Condición del Estudiante eliminado con éxito", MessageType.Success)
        Else
            Call mt_ShowMessage("No existe código de Condición del Estudiante para eliminar", MessageType.Warning)
        End If
    End Sub

#End Region

#Region "Niveles"

    Protected Sub gvNivelLogro_RowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs) Handles gvNivelLogro.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim ddlDesde As DropDownList = CType(e.Row.FindControl("ddlDesde"), DropDownList)
            Dim ddlHasta As DropDownList = CType(e.Row.FindControl("ddlHasta"), DropDownList)

            If Not ddlDesde Is Nothing Then
                ddlDesde.DataSource = fc_GetNivel(gvNivelLogro.DataKeys(e.Row.RowIndex).Item("codigo_niv").ToString)
                ddlDesde.DataValueField = "numero"
                ddlDesde.DataTextField = "numero"
                ddlDesde.DataBind()

                'Agregar fila en blanco
                ddlDesde.Items.Insert(0, New ListItem("[-- Seleccione un rango --]", "-1"))
                ddlDesde.SelectedValue = gvNivelLogro.DataKeys(e.Row.RowIndex).Item("rangoDesde_niv").ToString()
            End If

            If Not ddlHasta Is Nothing Then
                ddlHasta.DataSource = fc_GetNivel(gvNivelLogro.DataKeys(e.Row.RowIndex).Item("codigo_niv").ToString)
                ddlHasta.DataValueField = "numero"
                ddlHasta.DataTextField = "numero"
                ddlHasta.DataBind()

                'Agregar fila en blanco
                ddlHasta.Items.Insert(0, New ListItem("[-- Seleccione un rango --]", "-1"))
                ddlHasta.SelectedValue = gvNivelLogro.DataKeys(e.Row.RowIndex).Item("rangoHasta_niv").ToString()
            End If
        End If

        If e.Row.RowType = DataControlRowType.Footer Then
            Dim ddlDesde As DropDownList = CType(e.Row.FindControl("ddlNewDesde"), DropDownList)
            Dim ddlHasta As DropDownList = CType(e.Row.FindControl("ddlNewHasta"), DropDownList)

            If ddlDesde IsNot Nothing Then
                ddlDesde.DataSource = fc_GetNivel("-1")
                ddlDesde.DataValueField = "numero"
                ddlDesde.DataTextField = "numero"
                ddlDesde.DataBind()

                'Agregar fila en blanco
                ddlDesde.Items.Insert(0, New ListItem("[-- Seleccione un rango --]", "-1"))
            End If

            If ddlHasta IsNot Nothing Then
                ddlHasta.DataSource = fc_GetNivel("-1")
                ddlHasta.DataValueField = "numero"
                ddlHasta.DataTextField = "numero"
                ddlHasta.DataBind()

                'Agregar fila en blanco
                ddlHasta.Items.Insert(0, New ListItem("[-- Seleccione un rango --]", "-1"))
            End If
        End If
    End Sub

    Protected Sub gvNivelLogro_RowEditing(ByVal sender As Object, ByVal e As GridViewEditEventArgs)
        gvNivelLogro.EditIndex = e.NewEditIndex
        Call mt_CargarNiveles()
    End Sub

    Protected Sub OnNewNivel(ByVal sender As Object, ByVal e As EventArgs)
        Try
            Dim ddlDesde As DropDownList = CType(gvNivelLogro.FooterRow.FindControl("ddlNewDesde"), DropDownList)
            Dim ddlHasta As DropDownList = CType(gvNivelLogro.FooterRow.FindControl("ddlNewHasta"), DropDownList)
            Dim txtNombre As TextBox = CType(gvNivelLogro.FooterRow.FindControl("txtNewNombre"), TextBox)
            Dim txtDescripcion As TextBox = CType(gvNivelLogro.FooterRow.FindControl("txtNewDescripcion"), TextBox)
            Dim txtColor As HtmlInputText = CType(gvNivelLogro.FooterRow.FindControl("txtNewColor"), HtmlInputText)
            Dim chkLogro As CheckBox = CType(gvNivelLogro.FooterRow.FindControl("chkNewLogro"), CheckBox)

            If fc_ValidarNiveles(ddlDesde, ddlHasta, txtNombre, txtDescripcion) Then
                Dim dt As New Data.DataTable
                Dim chk As Int16 = IIf(chkLogro.Checked, 1, 0)
                Dim color As Object
                color = IIf(String.IsNullOrEmpty(txtColor.Value), DBNull.Value, txtColor.Value)

                obj.AbrirConexion()
                dt = obj.TraerDataTable("DEA_RegistrarNivelLogroAprendizaje", txtNombre.Text.ToUpper, txtDescripcion.Text, ddlDesde.SelectedValue, ddlHasta.SelectedValue, color, chk, cod_user)
                obj.CerrarConexion()

                If dt.Rows.Count > 0 Then
                    If dt.Rows(0).Item(0).ToString.Equals("1") Then
                        gvNivelLogro.EditIndex = -1
                        Call mt_CargarNiveles()

                        Call mt_ShowMessage(dt.Rows(0).Item(1).ToString, MessageType.Success)
                    Else
                        Call mt_ShowMessage(dt.Rows(0).Item(1).ToString, MessageType.Warning)
                    End If

                    dt.Dispose()
                End If
            End If
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", ""), MessageType.Warning)
        End Try
    End Sub

    Protected Sub OnUpdateNivel(ByVal sender As Object, ByVal e As EventArgs)
        Try
            Dim row As GridViewRow = TryCast((TryCast(sender, LinkButton)).NamingContainer, GridViewRow)

            Dim ddlDesde As DropDownList = CType(gvNivelLogro.Rows(row.RowIndex).FindControl("ddlDesde"), DropDownList)
            Dim ddlHasta As DropDownList = CType(gvNivelLogro.Rows(row.RowIndex).FindControl("ddlHasta"), DropDownList)
            Dim txtNombre As TextBox = CType(gvNivelLogro.Rows(row.RowIndex).FindControl("txtNombre"), TextBox)
            Dim txtDescripcion As TextBox = CType(gvNivelLogro.Rows(row.RowIndex).FindControl("txtDescripcion"), TextBox)
            Dim txtColor As HtmlInputText = CType(gvNivelLogro.Rows(row.RowIndex).FindControl("txtColor"), HtmlInputText)
            Dim chkLogro As CheckBox = CType(gvNivelLogro.Rows(row.RowIndex).FindControl("chkLogro"), CheckBox)

            If fc_ValidarNiveles(ddlDesde, ddlHasta, txtNombre, txtDescripcion) Then
                Dim dt As New Data.DataTable
                Dim codigo_niv As String = gvNivelLogro.DataKeys(row.RowIndex).Item("codigo_niv").ToString
                Dim chk As Int16 = IIf(chkLogro.Checked, 1, 0)
                Dim color As Object
                color = IIf(String.IsNullOrEmpty(txtColor.Value), DBNull.Value, txtColor.Value)

                obj.AbrirConexion()
                dt = obj.TraerDataTable("DEA_ActualizarNivelLogroAprendizaje", codigo_niv, txtNombre.Text.ToUpper, txtDescripcion.Text, ddlDesde.SelectedValue, ddlHasta.SelectedValue, color, chk, cod_user)
                obj.CerrarConexion()

                If dt.Rows.Count > 0 Then
                    If dt.Rows(0).Item(0).ToString.Equals("1") Then
                        gvNivelLogro.EditIndex = -1
                        Call mt_CargarNiveles()

                        Call mt_ShowMessage(dt.Rows(0).Item(1).ToString, MessageType.Success)
                    Else
                        Call mt_ShowMessage(dt.Rows(0).Item(1).ToString, MessageType.Warning)
                    End If

                    dt.Dispose()
                End If
            End If
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", ""), MessageType.Warning)
        End Try
    End Sub

    Protected Sub OnCancelNivel(ByVal sender As Object, ByVal e As EventArgs)
        gvNivelLogro.EditIndex = -1
        Call mt_CargarNiveles()
    End Sub

    Protected Sub OnDeleteNivel(ByVal sender As Object, ByVal e As EventArgs)
        Dim row As GridViewRow = TryCast((TryCast(sender, LinkButton)).NamingContainer, GridViewRow)
        Dim codigo_niv As String = gvNivelLogro.DataKeys(row.RowIndex).Item("codigo_niv").ToString

        If Not String.IsNullOrEmpty(codigo_niv) Then
            obj.AbrirConexion()
            obj.Ejecutar("DEA_EliminarNivelLogroAprendizaje", codigo_niv, cod_user)
            obj.CerrarConexion()

            gvNivelLogro.EditIndex = -1
            Call mt_CargarNiveles()
            Call mt_ShowMessage("Nivel de Logro eliminado con éxito", MessageType.Success)
        Else
            Call mt_ShowMessage("No existe código de Nivel de Logro para eliminar", MessageType.Warning)
        End If
    End Sub

#End Region

#Region "Métodos"

    Private Sub mt_CargarCortes(ByVal codigo_cac As String, ByVal tipo_corte As String)
        Dim dt As New Data.DataTable

        Try
            codigo_cac = IIf(String.IsNullOrEmpty(codigo_cac), "-1", codigo_cac)

            If codigo_cac <> "-1" Then
                obj.AbrirConexion()
                dt = obj.TraerDataTable("DEA_ListarCorteSemestre", codigo_cac, tipo_corte)
                obj.CerrarConexion()

                If dt.Rows.Count > 0 Then
                    gvCorte.DataSource = dt
                    gvCorte.DataBind()
                Else
                    dt.Rows.Add(dt.NewRow())
                    gvCorte.DataSource = dt
                    gvCorte.DataBind()

                    gvCorte.Rows(0).Cells.Clear()
                End If
            Else
                gvCorte.DataSource = Nothing
                gvCorte.DataBind()
            End If

            dt.Dispose()
            updCorte.Update()
            Call fc_CheckWeeks()
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Private Sub mt_CargarCondiciones()
        Dim dt As New Data.DataTable

        Try
            obj.AbrirConexion()
            dt = obj.TraerDataTable("DEA_ListarCondicionEstudiante")
            obj.CerrarConexion()
            gvCondicion.DataSource = dt
            gvCondicion.DataBind()
            dt.Dispose()
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Private Sub mt_CargarNiveles()
        Dim dt As New Data.DataTable

        Try
            obj.AbrirConexion()
            dt = obj.TraerDataTable("DEA_ListarNivelLogroAprendizaje")
            obj.CerrarConexion()

            If dt.Rows.Count > 0 Then
                gvNivelLogro.DataSource = dt
                gvNivelLogro.DataBind()
            Else
                dt.Rows.Add(dt.NewRow())
                gvNivelLogro.DataSource = dt
                gvNivelLogro.DataBind()

                gvNivelLogro.Rows(0).Cells.Clear()
            End If

            dt.Dispose()
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Private Sub mt_CargarSemestre()
        Dim dt As New Data.DataTable

        Try
            obj.AbrirConexion()
            dt = obj.TraerDataTable("ConsultarCicloAcademico", "DA", "")
            obj.CerrarConexion()
            Call mt_CargarCombo(Me.ddlSemestre, dt, "codigo_Cac", "descripcion_Cac")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub mt_CargarCombo(ByVal cbo As DropDownList, ByVal dt As Data.DataTable, ByVal datavalue As String, ByVal datatext As String)
        cbo.DataSource = dt
        cbo.DataTextField = datatext
        cbo.DataValueField = datavalue
        cbo.DataBind()
    End Sub

    Protected Sub mt_ShowMessage(ByVal Message As String, ByVal type As MessageType)
        Page.RegisterStartupScript("Mensaje", "<script>ShowMessage('" & Message & "','" & type.ToString & "');</script>")
    End Sub

#End Region

#Region "Funciones"

    Private Function fc_GetSemanas(ByVal codigo_cor As String) As Data.DataTable
        Dim dt As New Data.DataTable

        obj.AbrirConexion()
        codigo_cor = IIf(String.IsNullOrEmpty(codigo_cor), "-1", codigo_cor)

        dt = obj.TraerDataTable("DEA_ListarSemanasCicloAcademico", ddlSemestre.SelectedValue, ddlTipoCorte.SelectedValue, codigo_cor)
        obj.CerrarConexion()

        ViewState("dtFecha") = dt

        Return dt
    End Function

    Private Function fc_GetNivel(ByVal codigo_niv As String) As Data.DataTable
        Dim dt As New Data.DataTable

        obj.AbrirConexion()
        codigo_niv = IIf(String.IsNullOrEmpty(codigo_niv), "-1", codigo_niv)

        dt = obj.TraerDataTable("DEA_ListarRangoDias", codigo_niv)
        obj.CerrarConexion()

        Return dt
    End Function

    Function fc_ValidarCorte(ByVal txtFecha As TextBox, ByVal ddlSemana As DropDownList) As Boolean
        Dim isOk As Boolean = False

        If ddlSemana.SelectedValue = "-1" Then
            Call mt_ShowMessage("Seleccione una Semana válida", MessageType.Warning)
            'ClientScript.RegisterClientScriptBlock(Me.GetType, "popUp", "<script>alert('a')</script>")
        Else
            If String.IsNullOrEmpty(txtFecha.Text) Then
                Call mt_ShowMessage("Seleccione una fecha", MessageType.Warning)
                'ClientScript.RegisterClientScriptBlock(Me.GetType, "popUp", "<script>alert('b')</script>")
            Else
                Dim fecha As Object = Nothing

                Try
                    fecha = CDate(txtFecha.Text)
                Catch ex As Exception
                    Call mt_ShowMessage("Ingrese una fecha válida", MessageType.Warning)
                    'ClientScript.RegisterClientScriptBlock(Me.GetType, "popUp", "<script>alert('c')</script>")
                End Try

                If fecha IsNot Nothing Then
                    isOk = True
                End If
            End If
        End If

        Return isOk
    End Function

    Function fc_ValidarCondicion(ByVal ddlTipoCon As DropDownList, ByVal txtDescripcionCon As TextBox) As Boolean
        Dim isOk As Boolean = False

        If ddlTipoCon.SelectedValue = "-1" Then
            Call mt_ShowMessage("Seleccione un Tipo de Condición", MessageType.Warning)
        Else
            If String.IsNullOrEmpty(txtDescripcionCon.Text) Then
                Call mt_ShowMessage("Ingrese una descripción", MessageType.Warning)
            Else
                isOk = True
            End If
        End If

        Return isOk
    End Function

    Function fc_ValidarNiveles(ByVal ddlDesde As DropDownList, ByVal ddlHasta As DropDownList, ByVal txtNombre As TextBox, ByVal txtDescripcion As TextBox) As Boolean
        Dim isOk As Boolean = False

        If ddlDesde.SelectedValue = "-1" Or ddlHasta.SelectedValue = "-1" Then
            Call mt_ShowMessage("Seleccione un valor válido del rango", MessageType.Warning)
        Else
            If String.IsNullOrEmpty(txtNombre.Text) Then
                Call mt_ShowMessage("Ingrese nombre del Nivel", MessageType.Warning)
                Return isOk
            End If

            If String.IsNullOrEmpty(txtDescripcion.Text) Then
                Call mt_ShowMessage("Ingrese una descripción", MessageType.Warning)
                Return isOk
            End If

            If String.IsNullOrEmpty(ddlDesde.SelectedValue) Or String.IsNullOrEmpty(ddlHasta.SelectedValue) Then
                Call mt_ShowMessage("Seleccione un valor del rango", MessageType.Warning)
            Else
                If CInt(ddlDesde.SelectedValue.ToString) > CInt(ddlHasta.SelectedValue.ToString) Then
                    Call mt_ShowMessage("El valor [desde] no puede ser mayor al valor [hasta]", MessageType.Warning)
                Else
                    isOk = True
                End If
            End If
        End If

        Return isOk
    End Function

    Private Sub mt_GetLastWeek()
        Dim dt As New Data.DataTable

        obj.AbrirConexion()
        dt = obj.TraerDataTable("DEA_GetCicloAcademicoConf", ddlSemestre.SelectedValue)
        obj.CerrarConexion()

        ViewState("semana") = dt.Rows(0).Item(0).ToString
        dt.Dispose()
    End Sub

    Private Function fc_CheckWeeks() As Boolean
        Dim lblSemana As Label = Nothing
        Dim sem As String = TryCast(ViewState("semana"), String)
        sem = IIf(sem Is Nothing, "", sem)
        Dim alerta As Boolean = True
        Dim i As Integer = 0

        If Not String.IsNullOrEmpty(sem) Then
            For i = 0 To gvCorte.Rows.Count - 1
                lblSemana = CType(gvCorte.Rows(i).FindControl("lblSemana"), Label)
                If lblSemana IsNot Nothing AndAlso lblSemana.Text.Equals(sem) Then
                    alerta = False
                    Exit For
                End If
            Next
        End If

        If i > 1 Then
            alerta = alerta
        Else
            alerta = False
        End If

        Me.divAlertModal.Visible = alerta
        updCorte.Update()
        Return alerta
    End Function

#End Region

End Class
