
Partial Class GestionCurricular_FrmAdicionarInstrumentosContenido
    Inherits System.Web.UI.Page

#Region "Declaracion de Variables"

    Dim cod_user As Integer '= 1971
    Private codigo_cac, codigo_dis, codigo_cur, codigo_cup, codigo_uni As String

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
                Response.Redirect("../../../../sinacceso.html")
            End If

            cod_user = Session("id_per")

            codigo_cac = Session("codigo_cac")
            codigo_dis = Session("codigo_dis")
            codigo_cur = Session("codigo_cur")
            codigo_cup = Session("codigo_cup")

            If IsPostBack = False Then
                Dim curso As String
                curso = Session("curso")

                If Not String.IsNullOrEmpty(curso) Then
                    Me.titulo.InnerText = " Instrumentos de Evaluación de " & curso
                Else
                    Me.titulo.InnerText = " Instrumentos de Evaluación "
                End If

                Call mt_CargarDatos(codigo_dis, "-1")
            End If

        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub btnSalir_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSalir.Click
        Page.RegisterStartupScript("Pop", "<script>closeModal();</script>")
        Call mt_CargarDatos(codigo_dis, "-1")
    End Sub

    Protected Sub gvResultados_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvResultados.RowCommand
        Try
            Dim index As Integer = CInt(e.CommandArgument)

            If e.CommandName.Equals("EditarGrupo") Then
                Me.hdCodigoGru.Value = Me.gvResultados.DataKeys(index).Values("codigo_gru")
                codigo_uni = Me.gvResultados.DataKeys(index).Values("codigo_uni")
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
            obj.AbrirConexion()
           
            If e.CommandName.Equals("AddNew") Then
                Dim ddl As DropDownList = CType(gvInstrumento.FooterRow.FindControl("ddlNewInstrumento"), DropDownList)
                Dim txt As TextBox = CType(Me.gvInstrumento.FooterRow.FindControl("txtNewEvaluacion"), TextBox)
                If ddl IsNot Nothing Then
                    If ddl.SelectedIndex > 0 Then
                        obj.Ejecutar("COM_AgregarEvaluacionCurso", codigo_cup, ddl.SelectedValue, Me.hdCodigoGru.Value, cod_user, txt.Text.Trim)

                        Call mt_CargarDatosEvaluacion(Me.hdCodigoGru.Value)
                    Else
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), Guid.NewGuid().ToString("N"), "<script>alert('Seleccione el instrumento de evaluación');</script>", True)
                    End If
                End If
            ElseIf e.CommandName.Equals("Delete") Then
                Dim index As Integer = Convert.ToInt32(e.CommandArgument)
                Dim codigo_eva As String = gvInstrumento.DataKeys(index).Values(0).ToString()

                If Not String.IsNullOrEmpty(codigo_eva) Then
                    obj.Ejecutar("COM_QuitarEvaluacionCurso", codigo_eva)

                    Call mt_CargarDatosEvaluacion(Me.hdCodigoGru.Value)
                End If
            End If

            obj.CerrarConexion()
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

                dt = mt_CargarDatosInstrumento(codigo_uni)

                ddl.DataSource = dt
                ddl.DataValueField = "codigo_ins"
                ddl.DataTextField = "descripcion_ins"
                ddl.DataBind()

                'Agregar fila en blanco
                ddl.Items.Insert(0, New ListItem("[ --- Seleccione un instrumento --- ]", ""))

                'Seleccionar por defecto el id actual
                Dim codigo_ins As String = CType(e.Row.FindControl("lblCodigoIns"), Label).Text
                ddl.Items.FindByValue(codigo_ins).Selected = True
                dt.Dispose()
            End If
        End If

        If e.Row.RowType = DataControlRowType.Footer Then
            Dim ddln As DropDownList = CType(e.Row.FindControl("ddlNewInstrumento"), DropDownList)
            Dim dtn As New Data.DataTable("data")

            ' _codigo_uni = CInt(Me.gvResultados.DataKeys(e.Row.RowIndex).Values("codigo_uni"))

            If ddln IsNot Nothing Then

                dtn = mt_CargarDatosInstrumento(codigo_uni)

                ddln.DataSource = dtn
                ddln.DataValueField = "codigo_ins"
                ddln.DataTextField = "descripcion_ins"
                ddln.DataBind()

                'Agregar fila en blanco
                ddln.Items.Insert(0, New ListItem("[ --- Seleccione un instrumento --- ]", ""))
                dtn.Dispose()
            End If
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

    Private Sub mt_CargarDatos(ByVal codigo_dis As String, ByVal codigo_uni As String)
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
        Try
            obj.AbrirConexion()
            codigo_dis = IIf(String.IsNullOrEmpty(codigo_dis), "-1", codigo_dis)
            codigo_uni = IIf(String.IsNullOrEmpty(codigo_uni), "0", codigo_uni)

            dt = obj.TraerDataTable("COM_ListarContenidoAsignatura", codigo_dis, codigo_uni, codigo_cup)
            obj.CerrarConexion()




            Me.gvResultados.DataSource = dt
            Me.gvResultados.DataBind()

            If dt.Rows.Count > 0 Then
                Call mt_AgruparFilas(Me.gvResultados.Rows, 0, 1)
            End If

        Catch ex As Exception
            Throw New Exception(ex.Message.ToString(), ex)
        End Try
    End Sub

    Private Sub mt_CargarDatosEvaluacion(ByVal codigo_gru As String)
        Dim dtContenido As Data.DataTable = mt_GetEvaluacion(codigo_gru)
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

    Public Function mt_GetEvaluacion(ByVal codigo_gru As String) As Data.DataTable
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString

        obj.AbrirConexion()
        codigo_gru = IIf(String.IsNullOrEmpty(codigo_gru), "-1", codigo_gru)

        dt = obj.TraerDataTable("COM_ListarEvaluacionCurso", codigo_gru, cod_user)
        obj.CerrarConexion()

        Return dt
    End Function

    Private Function mt_CargarDatosInstrumento(ByVal codigo_uni As Integer) As Data.DataTable
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable("data")
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString

        Try
            obj.AbrirConexion()
            dt = obj.TraerDataTable("COM_ListarInstrumentos", codigo_dis, codigo_uni)
            obj.CerrarConexion()
        Catch ex As Exception
            Throw ex
        End Try

        Return dt
    End Function

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

#End Region

End Class
