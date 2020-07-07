﻿
Partial Class GestionCurricular_FrmAdicionarFechasSesion
    Inherits System.Web.UI.Page

#Region "Declaracion de Variables"

    Private cod_user As Integer
    Private codigo_cac, codigo_dis, codigo_cur, codigo_cup, codigo_uni As String
    Private uni_aux As String = ""
    Private tot_uni As Integer = 1

    Public Enum MessageType
        Success
        [Error]
        Info
        Warning
    End Enum

#End Region

#Region "Eventos"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If (Session("id_per") Is Nothing) Then
                Response.Redirect("../../sinacceso.html")
            End If

            cod_user = Session("id_per")

            codigo_cac = Session("codigo_cac")
            codigo_dis = Session("codigo_dis")
            codigo_cur = Session("codigo_cur")
            codigo_cup = Session("codigo_cup")

            If IsPostBack = False Then
                Dim dtFecha As Data.DataTable = New Data.DataTable("dtFecha")
                dtFecha.Columns.Add("fechas")
                dtFecha.Columns.Add("evento")
                dtFecha.Columns.Add("es_feriado")
                dtFecha.Columns.Add("tipos")
                ViewState("dtFecha") = dtFecha

                Session("dia_fec") = Nothing

                Dim curso As String = Session("curso")
                Dim grupo As String = Session("grupo")

                If Not String.IsNullOrEmpty(curso) Then
                    Me.lblCursoA.InnerText = curso & " (" & grupo & ")"
                End If

                Call mt_CargarUnidad(codigo_dis)
                Call mt_CargarDatosSesion(codigo_dis, codigo_cur, codigo_cup, ddlUnidad.SelectedValue)
            End If

        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub ddlUnidad_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlUnidad.SelectedIndexChanged
        Try
            Call mt_CargarDatosSesion(codigo_dis, codigo_cur, codigo_cup, ddlUnidad.SelectedValue)
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub btnBack_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBack.Click
        Try
            Response.Redirect("~/GestionCurricular/FrmSilaboGeneral.aspx")
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub gvSesion_RowCancelingEdit(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCancelEditEventArgs) Handles gvSesion.RowCancelingEdit
        gvSesion.EditIndex = -1
        Call mt_CargarDatosSesion(codigo_dis, codigo_cur, codigo_cup, ddlUnidad.SelectedValue)
    End Sub

    Protected Sub gvSesion_OnRowCreated(ByVal sender As Object, ByVal e As GridViewRowEventArgs) Handles gvSesion.RowCreated
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim numero_uni As String = gvSesion.DataKeys(e.Row.RowIndex).Item("numero_uni").ToString
                Dim unidad_des As String = gvSesion.DataKeys(e.Row.RowIndex).Item("descripcion_uni").ToString

                If Not numero_uni.Equals(uni_aux) Then
                    uni_aux = numero_uni
                    Dim objGridView As GridView = CType(sender, GridView)
                    Dim objGridViewRow As GridViewRow = New GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert)
                    Dim objTableCell As TableCell = New TableCell()
                    Dim fila As Integer = objGridView.Rows.Count

                    Call mt_AgregarCabecera(objGridViewRow, objTableCell, gvSesion.Columns.Count, uni_aux & ": " & unidad_des, "#E3DFDF", True)
                    objGridView.Controls(0).Controls.AddAt(fila + tot_uni, objGridViewRow)

                    tot_uni += 1
                End If
            End If
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub gvSesion_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles gvSesion.RowEditing
        gvSesion.EditIndex = e.NewEditIndex
        Session("dia_fec") = gvSesion.DataKeys(gvSesion.EditIndex).Values(2).ToString()
        Call mt_CargarDatosSesion(codigo_dis, codigo_cur, codigo_cup, ddlUnidad.SelectedValue)
    End Sub

    Protected Sub gvSesion_RowUpdating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewUpdateEventArgs) Handles gvSesion.RowUpdating
        If gvSesion.EditIndex > -1 Then
            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString

            Try
                Dim codigo_sgr As Object
                Dim dateValue As String = ""
                Dim dateText As String = ""
                Dim ddl As ListBox 'DropDownList
                ddl = CType(gvSesion.Rows(e.RowIndex).FindControl("ddlFecha"), ListBox)

                codigo_sgr = gvSesion.DataKeys(e.RowIndex).Item("codigo_sgr").ToString
                If String.IsNullOrEmpty(codigo_sgr) Then
                    codigo_sgr = DBNull.Value
                End If

                For Each item As ListItem In ddl.Items
                    If item.Selected AndAlso item.Value <> "" Then
                        If dateValue.Length > 0 Then dateValue &= "|"
                        If dateText.Length > 0 Then dateText &= "|"
                        dateValue &= item.Value
                        dateText &= item.Text
                    End If
                Next

                'If ddl.SelectedValue <> "" Then
                If Not String.IsNullOrEmpty(dateValue) Then
                    Dim fecha As String = ""
                    Dim flag As Boolean = False
                    Dim motivo As String = ""
                    Dim tipo As String = ""
                    Dim dt As Data.DataTable = TryCast(ViewState("dtFecha"), Data.DataTable)

                    If dt IsNot Nothing Then
                        For i As Integer = 0 To dt.Rows.Count - 1
                            For j As Integer = 0 To dateValue.Split("|").Length - 1
                                If dt.Rows(i).Item(2) = True And dt.Rows(i).Item(0).ToString.Contains(dateValue.Split("|")(j)) Then
                                    fecha = dateText.Split("|")(j)
                                    motivo = dt.Rows(i).Item(1).ToString
                                    tipo = dt.Rows(i).Item(3).ToString
                                    tipo = IIf(tipo.Equals("FI"), "Feriado Institucional", IIf(tipo.Equals("FC"), "Feriado Calendario", "Suspensión por Horas"))

                                    flag = True
                                    Exit For
                                End If
                            Next

                            If flag Then Exit For

                            'If dt.Rows(i).Item(2) = True And dt.Rows(i).Item(0).ToString.Contains(ddl.SelectedItem.Value.ToString()) Then
                            '    fecha = ddl.SelectedItem.Text.ToString().Substring(0, 15)
                            '    motivo = dt.Rows(i).Item(1).ToString
                            '    tipo = dt.Rows(i).Item(3).ToString
                            '    tipo = IIf(tipo.Equals("FI"), "Feriado Institucional", IIf(tipo.Equals("FC"), "Feriado Calendario", "Suspensión por Horas"))

                            '    flag = True
                            '    Exit For
                            'End If
                        Next
                        dt.Dispose()
                    End If

                    If flag Then
                        Call mt_ShowMessage("La fecha " & fecha.Substring(0, 15) & ". No puede ser seleccionada porque es " & tipo, MessageType.Info, True)
                        Return
                    End If

                    Dim codigo_ses As String = gvSesion.DataKeys(e.RowIndex).Values(0).ToString()
                    Dim codigo_fec As String = gvSesion.DataKeys(e.RowIndex).Values(3).ToString()
                    codigo_fec = IIf(String.IsNullOrEmpty(codigo_fec), "0", codigo_fec)

                    'obj.AbrirConexion()
                    'obj.Ejecutar("COM_ActualizarFechaSesion", codigo_fec, codigo_ses, Me.hdCodigoCup.Value, ddl.SelectedValue, ddl.SelectedItem.ToString, cod_user)
                    'obj.CerrarConexion()

                    obj.AbrirConexion()
                    obj.Ejecutar("COM_ActualizarFechaSesion", codigo_fec, codigo_ses, codigo_cup, dateValue, dateText, cod_user, codigo_sgr)
                    obj.CerrarConexion()

                    gvSesion.EditIndex = -1
                    Call mt_CargarDatosSesion(codigo_dis, codigo_cur, codigo_cup, ddlUnidad.SelectedValue)
                Else
                    Call mt_ShowMessage("Seleccione una fecha válida", MessageType.Info, True)
                End If
            Catch ex As Exception
                Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Info, True)
            End Try
        End If
    End Sub

    Protected Sub gvSesion_RowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs) Handles gvSesion.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim ddl As ListBox = CType(e.Row.FindControl("ddlFecha"), ListBox)

            If Not ddl Is Nothing Then
                Dim codigo_uni As String = gvSesion.DataKeys(e.Row().RowIndex).Item("codigo_uni").ToString()
                Dim codigo_sgr As String = gvSesion.DataKeys(e.Row().RowIndex).Item("codigo_sgr").ToString()
                If String.IsNullOrEmpty(codigo_uni) Then
                    codigo_uni = "-1"
                End If
                If String.IsNullOrEmpty(codigo_sgr) Then
                    codigo_sgr = "-1"
                End If

                ddl.SelectionMode = ListSelectionMode.Multiple
                ddl.DataSource = fc_CargarHorario(codigo_uni, codigo_sgr)
                ddl.DataValueField = "fechas"
                ddl.DataTextField = "descripcion"
                ddl.DataBind()

                'Agregar fila en blanco
                ddl.Items.Insert(0, New ListItem("[-- Seleccione una fecha --]", ""))

                Dim fec As String = gvSesion.DataKeys(e.Row.RowIndex).Values(2).ToString()
                If Not String.IsNullOrEmpty(fec) Then
                    For Each item As ListItem In ddl.Items
                        For i As Integer = 0 To fec.Split("|").Length - 1
                            If item.Value.Trim = fec.Split("|")(i).Trim Then
                                item.Selected = True
                            End If
                        Next
                    Next
                End If

                'ddl.SelectedValue = gvSesion.DataKeys(e.Row.RowIndex).Values(2).ToString()

                'Seleccionar por defecto el id actual
                'Dim fecha As String = CType(e.Row.FindControl("lblFecha"), Label).Text
                'ddl.Items.FindByValue(fecha).Selected = True
                'dt.Dispose()
            End If
        End If
    End Sub

    Protected Sub OnDeleteFecha(ByVal sender As Object, ByVal e As EventArgs)
        Dim dt As Data.DataTable = TryCast(ViewState("dtDesarrollo"), Data.DataTable)
        Dim row As GridViewRow = TryCast((TryCast(sender, LinkButton)).NamingContainer, GridViewRow)
        Dim codigo_ses As String = gvSesion.DataKeys(row.RowIndex).Item("codigo_ses").ToString
        Dim codigo_fec As String = gvSesion.DataKeys(row.RowIndex).Item("codigo_fec").ToString

        If Not String.IsNullOrEmpty(codigo_ses) And Not String.IsNullOrEmpty(codigo_fec) Then
            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString

            obj.AbrirConexion()
            obj.Ejecutar("COM_EliminarFechaSesion", codigo_ses, codigo_cup, codigo_fec) 'dia_fec
            obj.CerrarConexion()

            gvSesion.EditIndex = -1
            Call mt_CargarDatosSesion(codigo_dis, codigo_cur, codigo_cup, ddlUnidad.SelectedValue)
        End If
    End Sub

#End Region

#Region "Metodos"

    Protected Sub mt_ShowMessage(ByVal Message As String, ByVal type As MessageType, Optional ByVal modal As Boolean = False)
        If modal Then
            Me.divAlertModal.Visible = True
            Me.lblMensaje.InnerText = Message
            Me.divAlertModal.Focus()
            Me.lblMensaje.Focus()
            ScriptManager.GetCurrent(Me.Page).SetFocus(Me.divAlertModal)
            ScriptManager.GetCurrent(Me.Page).SetFocus(Me.lblMensaje)
            updMensaje.Update()
        Else
            Page.RegisterStartupScript("Mensaje", "<script>ShowMessage('" & Message & "','" & type.ToString & "');</script>")
        End If
    End Sub

    Private Sub mt_CargarCombo(ByVal cbo As DropDownList, ByVal dt As Data.DataTable, ByVal datavalue As String, ByVal datatext As String)
        cbo.DataSource = dt
        cbo.DataTextField = datatext
        cbo.DataValueField = datavalue
        cbo.DataBind()
    End Sub

    Private Sub mt_CargarDatosSesion(ByVal codigo_dis As String, ByVal codigo_cur As String, ByVal codigo_cup As String, Optional ByVal codigo_uni As Integer = -1)
        Dim dtSesion As Data.DataTable = fc_GetSesion(codigo_dis, codigo_cur, codigo_cup, codigo_uni)
        Try
            Me.gvSesion.DataSource = dtSesion
            Me.gvSesion.DataBind()

            If Me.gvSesion.Rows.Count > 0 Then
                Call mt_AgruparFilas(Me.gvSesion.Rows, 0, 5)
            End If

            Me.udpSesion.Update()

            Me.divAlertModal.Visible = False
            Me.lblMensaje.InnerText = ""
            Me.updMensaje.Update()
        Catch ex As Exception
            Throw New Exception(ex.Message.ToString(), ex)
        End Try
    End Sub

    Private Sub mt_CargarUnidad(ByVal codigo_dis As String)
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
        Try
            obj.AbrirConexion()
            codigo_dis = IIf(String.IsNullOrEmpty(codigo_dis), "-1", codigo_dis)

            dt = obj.TraerDataTable("COM_ListarUnidades", codigo_dis, "S")
            obj.CerrarConexion()
            Call mt_CargarCombo(Me.ddlUnidad, dt, "codigo_uni", "descripcion")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub mt_AgruparFilas(ByVal gridViewRows As GridViewRowCollection, ByVal startIndex As Integer, ByVal totalColumns As Integer)
        If totalColumns = 0 Then Return
        Dim i As Integer, count As Integer = 1
        Dim lst As ArrayList = New ArrayList()
        lst.Add(gridViewRows(0))
        Dim ctrl As TableCell
        ctrl = gridViewRows(0).Cells(startIndex)
        For i = 1 To gridViewRows.Count - 1
            Dim nextTbCell As TableCell = gridViewRows(i).Cells(startIndex)
            If ctrl.Text = nextTbCell.Text Then
                count += 1
                nextTbCell.Visible = False
                lst.Add(gridViewRows(i))
            Else
                If count > 1 Then
                    ctrl.RowSpan = count
                    Call mt_AgruparFilas(New GridViewRowCollection(lst), startIndex + 1, totalColumns - 1)
                End If
                count = 1
                lst.Clear()
                ctrl = gridViewRows(i).Cells(startIndex)
                lst.Add(gridViewRows(i))
            End If
        Next
        If count > 1 Then
            ctrl.RowSpan = count
            Call mt_AgruparFilas(New GridViewRowCollection(lst), startIndex + 1, totalColumns - 1)
        End If
        count = 1
        lst.Clear()
    End Sub

    Private Function fc_CargarHorario(ByVal codigo_uni As String, ByVal codigo_sgr As String) As Data.DataTable
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable("data")
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString

        Try
            Dim dia_fec As String = Session("dia_fec")
            dia_fec = IIf(String.IsNullOrEmpty(dia_fec), "", dia_fec)

            obj.AbrirConexion()
            dt = obj.TraerDataTable("COM_ListarHorarioDocente", codigo_cup, codigo_cac, dia_fec, "1", codigo_uni, codigo_sgr)

            Dim columns As String() = {"fechas", "evento", "es_feriado", "tipo"}
            Dim dtFecha As Data.DataTable = New Data.DataView(dt).ToTable(False, columns)

            ViewState("dtFecha") = dtFecha

            obj.CerrarConexion()
        Catch ex As Exception
            Throw ex
        End Try

        Return dt
    End Function

    Private Function fc_GetSesion(ByVal codigo_dis As String, ByVal codigo_cur As String, ByVal codigo_cup As String, Optional ByVal codigo_uni As Integer = -1) As Data.DataTable
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString

        uni_aux = ""
        tot_uni = 1

        obj.AbrirConexion()
        codigo_dis = IIf(String.IsNullOrEmpty(codigo_dis), "-1", codigo_dis)
        codigo_cur = IIf(String.IsNullOrEmpty(codigo_cur), "-1", codigo_cur)
        codigo_cup = IIf(String.IsNullOrEmpty(codigo_cup), "-1", codigo_cup)
        codigo_uni = IIf(String.IsNullOrEmpty(codigo_uni), "-1", codigo_uni)

        dt = obj.TraerDataTable("COM_ListarFechaSesion", codigo_dis, codigo_cur, codigo_cup, codigo_uni, "GR")
        obj.CerrarConexion()

        Return dt
    End Function

    Protected Sub mt_AgregarCabecera(ByVal objgridviewrow As GridViewRow, ByVal objtablecell As TableCell, ByVal colspan As Integer, ByVal celltext As String, ByVal backcolor As String, Optional ByVal paint As Boolean = False)
        objtablecell = New TableCell()
        objtablecell.Text = celltext
        objtablecell.ColumnSpan = colspan

        If paint Then
            objtablecell.Style.Add("background-color", backcolor)
            objtablecell.Style.Add("font-weight", "600")
        End If

        objtablecell.HorizontalAlign = HorizontalAlign.Center
        objgridviewrow.Cells.Add(objtablecell)
    End Sub

#End Region

End Class
