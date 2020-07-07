﻿
Partial Class GestionCurricular_FrmAdicionarInstrumentosContenido
    Inherits System.Web.UI.Page

#Region "Declaracion de Variables"

    Private cod_user As Integer '= 684
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
                Dim curso As String = Session("curso")
                Dim grupo As String = Session("grupo")
                
                If Not String.IsNullOrEmpty(curso) Then
                    Me.titulo.InnerText = " Instrumentos de Evaluación de " & curso & " (" & grupo & ")"
                Else
                    Me.titulo.InnerText = " Instrumentos de Evaluación "
                End If

                Call mt_CargarUnidad(codigo_dis)
                'ddlUnidad_SelectedIndexChanged(Nothing, Nothing)
                Call mt_CargarDatos(codigo_dis, "-1")
            End If
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub ddlUnidad_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlUnidad.SelectedIndexChanged
        Try
            Call mt_CargarDatos(codigo_dis, ddlUnidad.SelectedValue)
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub btnSalir_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSalir.Click, btnSalir2.ServerClick
        Page.RegisterStartupScript("Pop", "<script>closeModal();</script>")
        Call mt_CargarDatos(Session("codigo_dis"), ddlUnidad.SelectedValue)
    End Sub

    Protected Sub gvResultados_OnRowCreated(ByVal sender As Object, ByVal e As GridViewRowEventArgs) Handles gvResultados.RowCreated
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim numero_uni As String = gvResultados.DataKeys(e.Row.RowIndex).Item("numero_uni").ToString
                Dim unidad_des As String = gvResultados.DataKeys(e.Row.RowIndex).Item("unidad").ToString.ToUpper

                If Not numero_uni.Equals(uni_aux) Then
                    uni_aux = numero_uni
                    Dim objGridView As GridView = CType(sender, GridView)
                    Dim objGridViewRow As GridViewRow = New GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert)
                    Dim objTableCell As TableCell = New TableCell()
                    Dim fila As Integer = objGridView.Rows.Count

                    Call mt_AgregarCabecera(objGridViewRow, objTableCell, gvResultados.Columns.Count, uni_aux & ": " & unidad_des, "#E3DFDF", True)
                    objGridView.Controls(0).Controls.AddAt(fila + tot_uni, objGridViewRow)

                    tot_uni += 1
                End If
            End If
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub gvResultados_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvResultados.RowCommand
        Try
            Dim index As Integer = CInt(e.CommandArgument)

            If e.CommandName.Equals("EditarGrupo") Then
                Me.hdCodigoGru.Value = Me.gvResultados.DataKeys(index).Values("codigo_gru")
                codigo_uni = Me.gvResultados.DataKeys(index).Values("codigo_uni")
                Session("codigo_uni") = Me.gvResultados.DataKeys(index).Values("codigo_uni")

                Call mt_CargarDatosEvaluacion(Me.hdCodigoGru.Value)

                Page.RegisterStartupScript("Pop", "<script>openModal('Editar');</script>")
            End If
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub gvInstrumento_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)
        Try
            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            
            If e.CommandName.Equals("AddNew") Then
                Dim ddl As DropDownList = CType(gvInstrumento.FooterRow.FindControl("ddlNewInstrumento"), DropDownList)
                Dim ddl2 As DropDownList = CType(gvInstrumento.FooterRow.FindControl("ddlNewFecEva"), DropDownList) ' 20191227 - ENevado
                ' Dim txt As TextBox = CType(Me.gvInstrumento.FooterRow.FindControl("txtNewEvaluacion"), TextBox)
                If ddl IsNot Nothing Then
                    If ddl.SelectedIndex > 0 Then
                        If ddl2.SelectedValue > 0 Then
                            obj.AbrirConexion()
                            'obj.Ejecutar("COM_AgregarEvaluacionCurso", codigo_cup, ddl.SelectedValue, Me.hdCodigoGru.Value, cod_user, txt.Text.Trim)
                            obj.Ejecutar("COM_AgregarEvaluacionCurso", codigo_cup, ddl.SelectedValue, Me.hdCodigoGru.Value, cod_user, ddl.SelectedItem.Text, ddl2.SelectedValue) ' 20191227 - ENevado
                            obj.CerrarConexion()
                            Call mt_CargarDatosEvaluacion(Me.hdCodigoGru.Value)
                        Else
                            ScriptManager.RegisterStartupScript(Me, Me.GetType(), Guid.NewGuid().ToString("N"), "<script>alert('Seleccione una fecha para la evaluación');</script>", True)
                        End If
                    Else
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), Guid.NewGuid().ToString("N"), "<script>alert('Seleccione el instrumento de evaluación');</script>", True)
                    End If
                End If
            ElseIf e.CommandName.Equals("Delete") Then
                Dim index As Integer = Convert.ToInt32(e.CommandArgument)
                Dim codigo_eva As String = gvInstrumento.DataKeys(index).Values(0).ToString()

                If Not String.IsNullOrEmpty(codigo_eva) Then
                    obj.AbrirConexion()
                    obj.Ejecutar("COM_QuitarEvaluacionCurso", codigo_eva, cod_user)
                    obj.CerrarConexion()

                    Call mt_CargarDatosEvaluacion(Me.hdCodigoGru.Value)
                End If
            End If
        Catch ex As Exception
            ClientScript.RegisterStartupScript(Me.GetType, "Pop", "<script>mostrarMensaje('" & ex.Message.Replace("'", " ") & "', 'danger');</script>")
        End Try
    End Sub

    Protected Sub gvInstrumento_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs)

    End Sub

    Protected Sub gvInstrumento_RowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs) Handles gvInstrumento.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim ddl As DropDownList = CType(e.Row.FindControl("ddlInstrumento"), DropDownList)
            Dim dt As New Data.DataTable("data")
            If ddl IsNot Nothing Then
                dt = fc_CargarDatosInstrumento(Session("codigo_uni"))
                ddl.DataSource = dt
                ddl.DataValueField = "codigo_ins"
                ddl.DataTextField = "instrumento"
                ddl.DataBind()
                'Agregar fila en blanco
                ddl.Items.Insert(0, New ListItem("[ --- Seleccione un instrumento --- ]", ""))
                'Seleccionar por defecto el id actual
                Dim codigo_ins As String = CType(e.Row.FindControl("lblCodigoIns"), Label).Text
                ddl.Items.FindByValue(codigo_ins).Selected = True
                dt.Dispose()
            End If
            ' 20191227 - ENevado ---------------------------------------------------------------------------------------\
            Dim ddl2 As dropdownlist = CType(e.row.findcontrol("ddlFecEvaluacion"), dropdownlist)
            Dim dt2 As New data.datatable("data")
            If ddl2 IsNot Nothing Then
                dt = fc_CargarFechas(codigo_cup, Me.hdCodigoGru.Value)
                ddl2.DataSource = dt2
                ddl2.DataValueField = "codigo_fec"
                ddl2.DataTextField = "nombre_fec"
                ddl2.DataBind()
                'Agregar fila en blanco
                ddl2.Items.Insert(0, New ListItem("[ --- Seleccione Fecha --- ]", ""))
                'Seleccionar por defecto el id actual
                Dim codigo_fec As String = CType(e.Row.FindControl("lblCodigoFec"), Label).Text
                ddl2.Items.FindByValue(codigo_fec).Selected = True
                dt2.Dispose()
            End If
            ' ----------------------------------------------------------------------------------------------------------/
        End If

        If e.Row.RowType = DataControlRowType.Footer Then
            Dim ddln As DropDownList = CType(e.Row.FindControl("ddlNewInstrumento"), DropDownList)
            Dim dtn As New Data.DataTable("data")
            If ddln IsNot Nothing Then
                dtn = fc_CargarDatosInstrumento(Session("codigo_uni"))
                ddln.DataSource = dtn
                ddln.DataValueField = "codigo_ins"
                ddln.DataTextField = "instrumento"
                ddln.DataBind()
                'Agregar fila en blanco
                ddln.Items.Insert(0, New ListItem("[ --- Seleccione un instrumento --- ]", ""))
                dtn.Dispose()
            End If

            ' 20191227 - ENevado ---------------------------------------------------------------------------------------\
            Dim ddln2 As DropDownList = CType(e.Row.FindControl("ddlNewFecEva"), DropDownList)
            Dim dtn2 As New Data.DataTable("data")
            If ddln2 IsNot Nothing Then
                dtn2 = fc_CargarFechas(codigo_cup, Me.hdCodigoGru.Value)
                ddln2.DataSource = dtn2
                ddln2.DataValueField = "codigo_fec"
                ddln2.DataTextField = "nombre_fec"
                ddln2.DataBind()

                If ddln2.Items.Count > 1 Then
                    'Si existe más de una fecha, agregar fila en blanco
                    ddln2.Items.Insert(0, New ListItem("[ --- Seleccione Fecha--- ]", "-1"))
                Else
                    ddln2.SelectedIndex = 0
                End If
                dtn2.Dispose()
            End If
            ' ----------------------------------------------------------------------------------------------------------/
        End If
    End Sub

    Protected Sub btnBack_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBack.Click
        Try
            Response.Redirect("~/GestionCurricular/FrmSilaboGeneral.aspx")
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

#End Region

#Region "Metodos"

    Protected Sub mt_ShowMessage(ByVal Message As String, ByVal type As MessageType)
        Page.RegisterStartupScript("Mensaje", "<script>ShowMessage('" & Message & "','" & type.ToString & "');</script>")
    End Sub

    Private Sub mt_CargarCombo(ByVal cbo As DropDownList, ByVal dt As Data.DataTable, ByVal datavalue As String, ByVal datatext As String)
        cbo.DataSource = dt
        cbo.DataTextField = datatext
        cbo.DataValueField = datavalue
        cbo.DataBind()
    End Sub

    Private Sub mt_CargarUnidad(ByVal codigo_dis As String)
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
        Try
            obj.AbrirConexion()
            codigo_dis = IIF(String.IsNullOrEmpty(codigo_dis), "-1", codigo_dis)

            dt = obj.TraerDataTable("COM_ListarUnidades", codigo_dis, "S")
            obj.CerrarConexion()
            Call mt_CargarCombo(Me.ddlUnidad, dt, "codigo_uni", "descripcion")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub mt_CargarDatos(ByVal codigo_dis As String, ByVal codigo_uni As String)
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString

        Try
            uni_aux = ""
            tot_uni = 1

            codigo_dis = IIf(String.IsNullOrEmpty(codigo_dis), "-1", codigo_dis)
            codigo_uni = IIf(String.IsNullOrEmpty(codigo_uni), "-1", codigo_uni)

            obj.AbrirConexion()
            dt = obj.TraerDataTable("COM_ListarContenidoAsignatura", codigo_dis, codigo_uni, codigo_cup)
            obj.CerrarConexion()

            Me.gvResultados.DataSource = dt
            Me.gvResultados.DataBind()

            'If dt.Rows.Count > 0 Then
            '    Call mt_AgruparFilas(Me.gvResultados.Rows, 0, 1)
            'End If
        Catch ex As Exception
            Throw New Exception(ex.Message.ToString(), ex)
        End Try
    End Sub

    Private Sub mt_CargarDatosEvaluacion(ByVal codigo_gru As String)
        Dim dtContenido As Data.DataTable = fc_GetEvaluacion(codigo_gru, Session("codigo_cup"))
        Try
            If dtContenido.Rows.Count > 0 Then
                Me.gvInstrumento.DataSource = dtContenido
                Me.gvInstrumento.DataBind()
            Else
                dtContenido.Rows.Add(dtContenido.NewRow())
                Me.gvInstrumento.DataSource = dtContenido
                Me.gvInstrumento.DataBind()
                gvInstrumento.Rows(0).Cells.Clear()
            End If

            Me.udpInstrumento.Update()
        Catch ex As Exception
            Throw New Exception(ex.Message.ToString(), ex)
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

#Region "Funciones"

    Public Function fc_GetEvaluacion(ByVal codigo_gru As String, ByVal cod_cup As Integer) As Data.DataTable
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
        codigo_gru = IIf(String.IsNullOrEmpty(codigo_gru), "-1", codigo_gru)

        obj.AbrirConexion()
        dt = obj.TraerDataTable("COM_ListarEvaluacionCurso", codigo_gru, cod_user, cod_cup)
        obj.CerrarConexion()

        Return dt
    End Function

    Private Function fc_CargarDatosInstrumento(ByVal codigo_uni As Integer) As Data.DataTable
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable("data")
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString

        Try
            obj.AbrirConexion()
            dt = obj.TraerDataTable("COM_ListarInstrumentos", codigo_dis, -1, codigo_cup)
            obj.CerrarConexion()
        Catch ex As Exception
            Throw ex
        End Try

        Return dt
    End Function

    Private Function fc_CargarFechas(ByVal codigo_cup As Integer, ByVal codigo_gru As Integer) As Data.DataTable
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable("data")
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
        Try
            obj.AbrirConexion()
            dt = obj.TraerDataTable("COM_ListarFechaSesion", -1, -1, codigo_cup, codigo_gru, "F")
            obj.CerrarConexion()
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

End Class
