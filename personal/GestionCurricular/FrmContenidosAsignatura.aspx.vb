

Partial Class GestionCurricular_FrmContenidosAsignatura
    Inherits System.Web.UI.Page

#Region "Declaracion de Variables"

    Private cod_user As Integer

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
            Else
                cod_user = Session("id_per") 'Request.QueryString("id")
            End If
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub Page_LoadComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.LoadComplete
        If IsPostBack = False Then
            Dim dtContenido As Data.DataTable = New Data.DataTable("dtContenido")
            Dim dtActividad As Data.DataTable = New Data.DataTable("dtActividad")
            Dim dtSesion As Data.DataTable = New Data.DataTable("dtSesion")

            Dim dtElimContenido As Data.DataTable = New Data.DataTable("dtElimContenido")
            Dim dtElimActividad As Data.DataTable = New Data.DataTable("dtElimActividad")
            Dim dtElimSesion As Data.DataTable = New Data.DataTable("dtElimSesion")

            dtContenido.Columns.Add("codigo_gru")
            dtContenido.Columns.Add("codigo_con")
            dtContenido.Columns.Add("contenido")
            ViewState("dtContenido") = dtContenido

            dtActividad.Columns.Add("codigo_gru")
            dtActividad.Columns.Add("codigo_act")
            dtActividad.Columns.Add("actividad")
            ViewState("dtActividad") = dtActividad

            dtSesion.Columns.Add("codigo")
            dtSesion.Columns.Add("sesion")
            dtSesion.Columns.Add("descripcion")
            dtSesion.Columns.Add("unidad")
            dtSesion.Columns.Add("seleccion")
            dtSesion.Columns.Add("habilitado")
            dtSesion.Columns.Add("codigo_ses")
            ViewState("dtSesion") = dtSesion

            dtElimContenido.Columns.Add("codigo")
            ViewState("dtElimContenido") = dtElimContenido

            dtElimActividad.Columns.Add("codigo")
            ViewState("dtElimActividad") = dtElimActividad

            dtElimSesion.Columns.Add("codigo")
            dtElimSesion.Columns.Add("nombre_ses")
            ViewState("dtElimSesion") = dtElimSesion

            'Call BindGridContenido()
            'Call BindGridActividad()
            'Call BindGridSesion()
            Session("mensaje") = Nothing

            Call mt_CargarSemestre()

            If Not String.IsNullOrEmpty(Session("gc_codigo_dis")) Then
                If Me.ddlSemestre.Items.Count > 0 Then Me.ddlSemestre.SelectedValue = Session("gc_codigo_cac") ': mt_CargarDiseñoAsignatura(Session("gc_codigo_cac"))
                'If Me.ddlDiseñoAprendizaje.Items.Count > 0 Then Me.ddlDiseñoAprendizaje.SelectedValue = Session("gc_codigo_dis")
                Call mt_CargarUnidad(Session("gc_codigo_dis"))
                Call mt_mostrarMinimoMaximoSesiones(Session("gc_codigo_dis"))
                Me.ddlSemestre.Enabled = False
                Me.lblAsignatura.Visible = False
                Me.ddlDiseñoAprendizaje.Visible = False
                Me.btnBack.Visible = True
                Me.btnSeguir.Visible = True
                Me.lblCurso.InnerText = "Diseñar sesiones (contenidos y actividades): " & Session("gc_nombre_cur")
            Else
                Me.ddlSemestre.Enabled = True
                Me.lblAsignatura.Visible = True
                Me.ddlDiseñoAprendizaje.Visible = True
                Me.btnBack.Visible = False
                Me.btnSeguir.Visible = False
            End If

            Me.btnCombinar.Visible = False
        End If
    End Sub

    Protected Sub ddlSemestre_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlSemestre.SelectedIndexChanged
        Try
            Call mt_CargarDiseñoAsignatura(Me.ddlSemestre.SelectedValue)
            Call mt_CargarUnidad(Me.ddlDiseñoAprendizaje.SelectedValue)
            Call mt_CargarDatos(Me.ddlDiseñoAprendizaje.SelectedValue, Me.ddlUnidad.SelectedValue)
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub ddlDiseñoAprendizaje_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlDiseñoAprendizaje.SelectedIndexChanged
        Try
            Call mt_CargarUnidad(Me.ddlDiseñoAprendizaje.SelectedValue)
            Call mt_CargarDatos(Me.ddlDiseñoAprendizaje.SelectedValue, Me.ddlUnidad.SelectedValue)
            Call mt_CargarDatosContenido("-1")
            Call mt_CargarDatosActividad("-1")
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub ddlUnidad_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlUnidad.SelectedIndexChanged
        Dim _codigo_dis As Integer
        Try
            If String.IsNullOrEmpty(Me.ddlDiseñoAprendizaje.SelectedValue) Then
                _codigo_dis = IIf(String.IsNullOrEmpty(Session("gc_codigo_dis")), -1, Session("gc_codigo_dis"))
            Else
                _codigo_dis = IIf(String.IsNullOrEmpty(Session("gc_codigo_dis")), Me.ddlDiseñoAprendizaje.SelectedValue, Session("gc_codigo_dis"))
            End If

            Call mt_CargarDatos(_codigo_dis, Me.ddlUnidad.SelectedValue)
            Call mt_CargarDatosContenido("-1")
            Call mt_CargarDatosActividad("-1")
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub btnAgregarGrupo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAgregarGrupo.Click
        Dim _codigo_dis As Integer
        Try
            If Me.ddlSemestre.SelectedValue <= 0 Then
                Call mt_ShowMessage("Seleccione el semestre", MessageType.Error)
                Me.ddlSemestre.Focus()
                Return
            End If

            _codigo_dis = IIf(String.IsNullOrEmpty(Session("gc_codigo_dis")), Me.ddlDiseñoAprendizaje.SelectedIndex, Session("gc_codigo_dis"))

            If _codigo_dis = -1 Then
                Call mt_ShowMessage("Seleccione el diseño de aprendizaje", MessageType.Error)
                Me.ddlDiseñoAprendizaje.Focus()
                Return
            End If

            If Me.ddlUnidad.SelectedValue <= 0 Then
                Call mt_ShowMessage("Seleccione la unidad", MessageType.Error)
                Me.ddlUnidad.Focus()
                Return
            End If

            Session("codigo_gru") = Nothing
            Call mt_CargarDatosContenido(Session("codigo_gru"))
            Call mt_CargarDatosActividad(Session("codigo_gru"))
            Call mt_CargarDatosSesion(Me.ddlUnidad.SelectedValue, Session("codigo_gru"))

            Page.RegisterStartupScript("Pop", "<script>openModal();</script>")
            'Call mt_CargarDatos(_codigo_dis, Me.ddlUnidad.SelectedValue)
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub btnCombinar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCombinar.Click
        Dim totalChk, pre, pos, cod_grupo1, cod_grupo2 As Integer
        cod_grupo1 = -1 : cod_grupo2 = -1 : totalChk = 0 : pre = -1 : pos = -1

        Try
            For i As Integer = 0 To gvResultados.Rows.Count - 1
                Dim chk As CheckBox = CType(gvResultados.Rows(i).FindControl("chkCombinar"), CheckBox)
                If chk.Enabled AndAlso chk.Checked Then
                    pre = IIf(pre = -1, i, pre)
                    pos = IIf(pos = -1 And pre <> i, i, pos)
                    totalChk += 1
                End If
            Next

            If totalChk = 0 Then
                Call mt_ShowMessage("No se ha seleccionado ningún grupo. Por favor, seleccione los grupos que desea unir", MessageType.Info)
                Return
            ElseIf totalChk <> 2 Then
                Call mt_ShowMessage("Solo se permite seleccionar hasta 2 grupos", MessageType.Info)
                Return
            End If

            'If pos - pre <> 1 Then
            '    Call mt_ShowMessage("Solo se permite unir grupos consecutivos", MessageType.Info)
            '    Return
            'End If

            cod_grupo1 = CInt(Me.gvResultados.DataKeys(pre).Values("codigo_gru").ToString)
            cod_grupo2 = CInt(Me.gvResultados.DataKeys(pos).Values("codigo_gru").ToString)

            'Throw New Exception("codigo_gru1: " & cod_grupo1 & " codigo_gru2:" & cod_grupo2)
            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            obj.AbrirConexion()
            obj.Ejecutar("DEA_CombinarGrupoContenido", cod_grupo1, cod_grupo2, cod_user)
            obj.CerrarConexion()
            ddlUnidad_SelectedIndexChanged(Nothing, Nothing)
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub btnValidar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnValidar.ServerClick
        Try
            Dim dtContenido As Data.DataTable = TryCast(ViewState("dtContenido"), Data.DataTable)
            Dim dtActividad As Data.DataTable = TryCast(ViewState("dtActividad"), Data.DataTable)
            Dim dtSesion As Data.DataTable = TryCast(ViewState("dtSesion"), Data.DataTable)

            If Not fc_Validar(dtContenido, dtActividad, dtSesion) Then
                Call mt_ShowMessage(Session("mensaje"), MessageType.Info, True)
            Else
                Session("mensaje") = Nothing
                Me.divAlertModal.Visible = False
                Me.validar.Value = "1"
                updMensaje.Update()
            End If
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", ""), MessageType.Error, True)
        End Try
    End Sub

    Protected Sub btnGrabar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGrabar.Click
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        Dim sesion_ref As String = ""
        Dim codigo_gru As Object = Session("codigo_gru")
        Dim _codigo_dis As Integer
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString

        Try
            obj.IniciarTransaccion()

            Dim dtContenido As Data.DataTable = TryCast(ViewState("dtContenido"), Data.DataTable)
            Dim dtActividad As Data.DataTable = TryCast(ViewState("dtActividad"), Data.DataTable)
            Dim dtSesion As Data.DataTable = TryCast(ViewState("dtSesion"), Data.DataTable)

            Dim dtElimContenido As Data.DataTable = TryCast(ViewState("dtElimContenido"), Data.DataTable)
            Dim dtElimActividad As Data.DataTable = TryCast(ViewState("dtElimActividad"), Data.DataTable)
            Dim dtElimSesion As Data.DataTable = TryCast(ViewState("dtElimSesion"), Data.DataTable)

            '--> Confirmar eliminación de Contenidos
            For i As Integer = 0 To dtElimContenido.Rows.Count - 1
                Dim codigo As Object
                codigo = dtElimContenido.Rows(i).Item(0).ToString
                obj.Ejecutar("COM_EliminarCA", "C", codigo, cod_user)
            Next

            '--> Confirmar eliminación de Actividades
            For i As Integer = 0 To dtElimActividad.Rows.Count - 1
                Dim codigo As Object
                codigo = dtElimActividad.Rows(i).Item(0).ToString
                obj.Ejecutar("COM_EliminarCA", "A", codigo, cod_user)
            Next

            '--> Confirmar eliminación de Sesiones
            For i As Integer = 0 To dtElimSesion.Rows.Count - 1
                Dim codigo As Object
                codigo = dtElimSesion.Rows(i).Item(0).ToString
                sesion_ref = dtElimSesion.Rows(i).Item(1).ToString
                obj.Ejecutar("COM_EliminarCA", "S", codigo, cod_user)
            Next

            codigo_gru = IIf(String.IsNullOrEmpty(codigo_gru), DBNull.Value, codigo_gru)

            '--> Confirmar registro de Contenidos
            For i As Integer = 0 To Me.gvContenido.Rows.Count - 1
                Dim codigo, contenido As Object
                codigo = dtContenido.Rows(i).Item(1).ToString
                codigo = IIf(String.IsNullOrEmpty(codigo), DBNull.Value, codigo)
                contenido = dtContenido.Rows(i).Item(2).ToString.Trim
                dt = obj.TraerDataTable("COM_RegistrarCA", "C", codigo_gru, Me.ddlUnidad.SelectedValue, codigo, contenido, cod_user)

                If dt.Rows.Count > 0 Then
                    codigo_gru = dt.Rows(0).Item(0).ToString
                Else
                    codigo_gru = DBNull.Value
                End If
            Next

            '--> Confirmar registro de Actividades
            For i As Integer = 0 To Me.gvActividad.Rows.Count - 1
                Dim codigo, actividad As Object
                codigo = dtActividad.Rows(i).Item(1).ToString
                codigo = IIf(String.IsNullOrEmpty(codigo), DBNull.Value, codigo)
                actividad = dtActividad.Rows(i).Item(2).ToString.Trim
                obj.Ejecutar("COM_RegistrarCA", "A", codigo_gru, Me.ddlUnidad.SelectedValue, codigo, actividad, cod_user)
            Next

            '--> Confirmar registro de las Sesiones
            For i As Integer = 0 To Me.gvSesion.Rows.Count - 1
                Dim chk As CheckBox = CType(gvSesion.Rows(i).FindControl("chkSeleccionado"), CheckBox)
                'If dtSesion.Rows(i).Item(4) Then
                If chk.Checked And chk.Enabled Then
                    Dim codigo_ses As Object
                    Dim nombre_ses, orden_ses As String
                    codigo_ses = dtSesion.Rows(i).Item(6).ToString
                    codigo_ses = IIf(String.IsNullOrEmpty(codigo_ses), 0, codigo_ses)
                    nombre_ses = dtSesion.Rows(i).Item(2).ToString.Trim
                    orden_ses = dtSesion.Rows(i).Item(0).ToString

                    obj.Ejecutar("COM_RegistrarSesion", codigo_ses, codigo_gru, nombre_ses, orden_ses, cod_user)
                End If
            Next

            If String.IsNullOrEmpty(Session("codigo_gru")) Then
                Call mt_ShowMessage("El contenido y las actividades fueron registradas con éxito", MessageType.Success)
            Else
                Call mt_ShowMessage("El contenido y las actividades fueron actualizadas con éxito", MessageType.Success)
            End If

            obj.TerminarTransaccion()
            _codigo_dis = IIf(String.IsNullOrEmpty(Session("gc_codigo_dis")), Me.ddlDiseñoAprendizaje.SelectedValue, Session("gc_codigo_dis"))
            Call mt_CargarDatos(_codigo_dis, Me.ddlUnidad.SelectedValue)
        Catch ex As Exception
            obj.AbortarTransaccion()

            Dim msj As String = ex.Message.Replace("'", " ").Replace("""", " ").Replace(vbCr, " ").Replace(vbLf, "")

            If msj.Contains("codigo_ses") Then
                Call mt_ShowMessage("No se puede proceder con la acción porque usted intenta eliminar o desasignar la sesión (" & sesion_ref & ") que actualmente cuenta con fechas programadas en el Sílabo. Si desea proceder, primero quite las fechas de esta sesión en la opción Gestión del Sílabo > Asignar fechas", MessageType.Error)
            Else
                Call mt_ShowMessage(msj, MessageType.Error)
            End If
        End Try
    End Sub

    Protected Sub gvResultados_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvResultados.RowCommand
        Try
            Dim index As Integer = CInt(e.CommandArgument)
            Session("codigo_gru") = Me.gvResultados.DataKeys(index).Values("codigo_gru")

            If e.CommandName.Equals("EditarGrupo") Then
                Call mt_CargarDatosContenido(Session("codigo_gru"))
                Call mt_CargarDatosActividad(Session("codigo_gru"))
                Call mt_CargarDatosSesion(Me.ddlUnidad.SelectedValue, Session("codigo_gru"))

                Page.RegisterStartupScript("Pop", "<script>openModal();</script>")
            ElseIf e.CommandName.Equals("EliminarGrupo") Then
                Dim obj As New ClsConectarDatos
                Dim dt As New Data.DataTable

                obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
                obj.AbrirConexion()

                dt = obj.TraerDataTable("COM_EliminarSesion", Session("codigo_gru"), cod_user)

                If dt.Rows(0).Item(0).ToString.Equals("0") Then
                    Call mt_ShowMessage(dt.Rows(0).Item(1).ToString, MessageType.Warning)
                Else
                    Call mt_ShowMessage(dt.Rows(0).Item(1).ToString, MessageType.Success)
                End If
                dt.Dispose()

                obj.CerrarConexion()

                Dim _codigo_dis As Integer = IIf(String.IsNullOrEmpty(Session("gc_codigo_dis")), Me.ddlDiseñoAprendizaje.SelectedValue, Session("gc_codigo_dis"))
                Call mt_CargarDatos(_codigo_dis, Me.ddlUnidad.SelectedValue)
            ElseIf e.CommandName.Equals("Combinar") Then
                Call mt_CombinarSesion(index)
            ElseIf e.CommandName.Equals("Descombinar") Then
                Call mt_DescombinarSesion(index)
            End If
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub btnBack_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBack.Click
        Try
            Response.Redirect("~/gestioncurricular/frmReferenciaBibliografica.aspx")
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub btnSeguir_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSeguir.Click
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
        Try
            obj.AbrirConexion()
            dt = obj.TraerDataTable("COM_ListarContenidoAsignatura", Session("gc_codigo_dis"), -1, 0)
            obj.CerrarConexion()
            If dt.Rows.Count = 0 Then Throw New Exception("¡ Ingrese al menos un contenido por unidad de la asignatura !")
            Dim _flag As Boolean = False
            Dim _unidad As String
            For i As Integer = 0 To Me.ddlUnidad.Items.Count - 1
                If Me.ddlUnidad.Items(i).Value <> 0 Then
                    _flag = False
                    _unidad = Me.ddlUnidad.Items(i).Text
                    For j As Integer = 0 To dt.Rows.Count - 1
                        If Me.ddlUnidad.Items(i).Value = dt.Rows(j).Item(1) Then
                            _flag = True
                            Exit For
                        End If
                    Next
                    If Not _flag Then
                        Throw New Exception("¡ Ingrese contenido a la Unidad: " & _unidad & " !")
                    End If
                End If
            Next
            Response.Redirect("~/gestioncurricular/frmConfigurarAsignatura.aspx")
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

#End Region

#Region "Contenido"

    Protected Sub gvContenido_RowEditing(ByVal sender As Object, ByVal e As GridViewEditEventArgs)
        gvContenido.EditIndex = e.NewEditIndex
        BindGridContenido()
    End Sub

    Protected Sub OnUpdateContenido(ByVal sender As Object, ByVal e As EventArgs)
        Dim dt As Data.DataTable = TryCast(ViewState("dtContenido"), Data.DataTable)
        Dim row As GridViewRow = TryCast((TryCast(sender, LinkButton)).NamingContainer, GridViewRow)
        Dim txtContenido As TextBox = CType(gvContenido.Rows(row.RowIndex).FindControl("txtContenido"), TextBox)

        dt.Rows(row.RowIndex)("contenido") = txtContenido.Text
        ViewState("dtContenido") = dt
        gvContenido.EditIndex = -1
        BindGridContenido()
    End Sub

    Protected Sub OnCancelContenido(ByVal sender As Object, ByVal e As EventArgs)
        gvContenido.EditIndex = -1
        BindGridContenido()
    End Sub

    Protected Sub OnDeleteContenido(ByVal sender As Object, ByVal e As EventArgs)
        Try
            Dim dt As Data.DataTable = TryCast(ViewState("dtContenido"), Data.DataTable)
            Dim row As GridViewRow = TryCast((TryCast(sender, LinkButton)).NamingContainer, GridViewRow)
            Dim codigo_con As String = gvContenido.DataKeys(row.RowIndex).Item("codigo_con").ToString

            If Not String.IsNullOrEmpty(codigo_con) And Not codigo_con.Equals("0") Then
                Dim dtElimContenido As Data.DataTable = TryCast(ViewState("dtElimContenido"), Data.DataTable)
                dtElimContenido.Rows.Add(codigo_con)
                ViewState("dtElimContenido") = dtElimContenido
            End If

            dt.Rows.RemoveAt(row.RowIndex)
            ViewState("dtContenido") = dt
            gvContenido.EditIndex = -1
            BindGridContenido()
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub OnNewContenido(ByVal sender As Object, ByVal e As EventArgs)
        Dim dt As Data.DataTable = TryCast(ViewState("dtContenido"), Data.DataTable)
        Dim txtContenido As TextBox = CType(gvContenido.FooterRow.FindControl("txtNewContenido"), TextBox)

        If String.IsNullOrEmpty(txtContenido.Text) Then
            Throw New Exception("Ingrese un valor al Contenido")
            Return
        Else
            If String.IsNullOrEmpty(dt.Rows(0).Item(1).ToString) Then
                dt.Rows.RemoveAt(0)
            End If

            dt.Rows.Add(0, 0, txtContenido.Text)
            ViewState("dtContenido") = dt
            gvContenido.EditIndex = -1
            BindGridContenido()
        End If
    End Sub

    Protected Sub BindGridContenido()
        Dim dt As Data.DataTable = TryCast(ViewState("dtContenido"), Data.DataTable)

        If dt.Rows.Count > 0 Then
            gvContenido.DataSource = dt
            gvContenido.DataBind()
        Else
            dt.Rows.Add(dt.NewRow())
            gvContenido.DataSource = dt
            gvContenido.DataBind()

            gvContenido.Rows(0).Cells.Clear()
        End If

        Me.udpContenido.Update()
    End Sub

#End Region

#Region "Actividad"

    Protected Sub gvActividad_RowEditing(ByVal sender As Object, ByVal e As GridViewEditEventArgs)
        gvActividad.EditIndex = e.NewEditIndex
        BindGridActividad()
    End Sub

    Protected Sub OnUpdateActividad(ByVal sender As Object, ByVal e As EventArgs)
        Dim dt As Data.DataTable = TryCast(ViewState("dtActividad"), Data.DataTable)
        Dim row As GridViewRow = TryCast((TryCast(sender, LinkButton)).NamingContainer, GridViewRow)
        Dim txtActividad As TextBox = CType(gvActividad.Rows(row.RowIndex).FindControl("txtActividad"), TextBox)

        dt.Rows(row.RowIndex)("actividad") = txtActividad.Text
        ViewState("dtActividad") = dt
        gvActividad.EditIndex = -1
        BindGridActividad()
    End Sub

    Protected Sub OnCancelActividad(ByVal sender As Object, ByVal e As EventArgs)
        gvActividad.EditIndex = -1
        BindGridActividad()
    End Sub

    Protected Sub OnDeleteActividad(ByVal sender As Object, ByVal e As EventArgs)
        Try
            Dim dt As Data.DataTable = TryCast(ViewState("dtActividad"), Data.DataTable)
            Dim row As GridViewRow = TryCast((TryCast(sender, LinkButton)).NamingContainer, GridViewRow)
            Dim codigo_act As String = gvActividad.DataKeys(row.RowIndex).Item("codigo_act").ToString '(CType(row.Cells(0).Controls(0), TextBox)).Text

            If Not String.IsNullOrEmpty(codigo_act) And Not codigo_act.Equals("0") Then
                Dim dtElimActividad As Data.DataTable = TryCast(ViewState("dtElimActividad"), Data.DataTable)
                dtElimActividad.Rows.Add(codigo_act)
                ViewState("dtElimActividad") = dtElimActividad
            End If

            dt.Rows.RemoveAt(row.RowIndex)
            ViewState("dtActividad") = dt
            gvActividad.EditIndex = -1
            BindGridActividad()
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub OnNewActividad(ByVal sender As Object, ByVal e As EventArgs)
        Dim dt As Data.DataTable = TryCast(ViewState("dtActividad"), Data.DataTable)
        Dim txtActividad As TextBox = CType(gvActividad.FooterRow.FindControl("txtNewActividad"), TextBox)

        If String.IsNullOrEmpty(txtActividad.Text) Then
            Throw New Exception("Ingrese un valor a la Actividad")
            Return
        Else
            If String.IsNullOrEmpty(dt.Rows(0).Item(1).ToString) Then
                dt.Rows.RemoveAt(0)
            End If

            dt.Rows.Add(0, 0, txtActividad.Text)
            ViewState("dtActividad") = dt
            gvActividad.EditIndex = -1
            BindGridActividad()
        End If
    End Sub

    Protected Sub BindGridActividad()
        Dim dt As Data.DataTable = TryCast(ViewState("dtActividad"), Data.DataTable)

        If dt.Rows.Count > 0 Then
            gvActividad.DataSource = dt
            gvActividad.DataBind()
        Else
            dt.Rows.Add(dt.NewRow())
            gvActividad.DataSource = dt
            gvActividad.DataBind()

            gvActividad.Rows(0).Cells.Clear()
        End If

        Me.udpActividad.Update()
    End Sub

#End Region

#Region "Sesión"

    Protected Sub gvSesion_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs)
        gvSesion.EditIndex = e.NewEditIndex
        BindGridSesion()
    End Sub

    Protected Sub OnCheck(ByVal sender As Object, ByVal e As EventArgs)
        Dim dt As Data.DataTable = TryCast(ViewState("dtSesion"), Data.DataTable)

        If dt.Rows.Count > 0 Then
            Dim chk As CheckBox = TryCast(sender, CheckBox)
            Dim row As GridViewRow = TryCast(chk.NamingContainer, GridViewRow)

            If Not chk.Checked Then
                Dim codigo_ses As String = gvSesion.DataKeys(row.RowIndex).Item("codigo_ses").ToString
                Dim nombre_ses As String = gvSesion.DataKeys(row.RowIndex).Item("descripcion").ToString

                If Not String.IsNullOrEmpty(codigo_ses) Or Not codigo_ses.Equals("0") Then
                    Dim dtElimSesion As Data.DataTable = TryCast(ViewState("dtElimSesion"), Data.DataTable)
                    dtElimSesion.Rows.Add(codigo_ses, nombre_ses)
                    ViewState("dtElimSesion") = dtElimSesion
                End If
            End If

            dt.Rows(row.RowIndex)("unidad") = "" ' IIf(chk.Checked, ddlUnidad.SelectedItem.ToString.Split("-")(0).Trim(), "")
            dt.Rows(row.RowIndex)("seleccion") = chk.Checked

            ViewState("dtSesion") = dt
            BindGridSesion()
        End If
    End Sub

    Protected Sub OnUpdateSesion(ByVal sender As Object, ByVal e As EventArgs)
        Dim dt As Data.DataTable = TryCast(ViewState("dtSesion"), Data.DataTable)
        Dim row As GridViewRow = TryCast((TryCast(sender, LinkButton)).NamingContainer, GridViewRow)
        Dim txtDescripcion As TextBox = CType(gvSesion.Rows(row.RowIndex).FindControl("txtDescripcion"), TextBox)

        dt.Rows(row.RowIndex)("descripcion") = txtDescripcion.Text
        dt.Rows(row.RowIndex)("unidad") = "" ' ddlUnidad.SelectedItem.ToString.Split("-")(0).Trim()
        dt.Rows(row.RowIndex)("seleccion") = True
        ViewState("dtSesion") = dt
        gvSesion.EditIndex = -1
        BindGridSesion()
    End Sub

    Protected Sub OnCancelSesion(ByVal sender As Object, ByVal e As EventArgs)
        gvSesion.EditIndex = -1
        BindGridSesion()
    End Sub

    Protected Sub BindGridSesion()
        Dim dt As Data.DataTable = TryCast(ViewState("dtSesion"), Data.DataTable)

        If dt.Rows.Count > 0 Then
            gvSesion.DataSource = dt
            gvSesion.DataBind()
        Else
            dt.Rows.Add(dt.NewRow())
            gvSesion.DataSource = dt
            gvSesion.DataBind()

            gvSesion.Rows(0).Cells.Clear()

            'Dim totColumns As Integer = gvSesion.Rows(0).Cells.Count
            'gvSesion.Rows(0).Cells.Clear()
            'gvSesion.Rows(0).Cells.Add(New TableCell())
            'gvSesion.Rows(0).Cells(0).ColumnSpan = totColumns
            'gvSesion.Rows(0).Cells(0).Style.Add("text-align", "center")
            'gvSesion.Rows(0).Cells(0).Style.Add("color", "red")
            'gvSesion.Rows(0).Cells(0).Text = "El curso no ha sido programado"
        End If

        Me.udpSesion.Update()
    End Sub

#End Region

#Region "Metodos"

    Protected Sub mt_ShowMessage(ByVal Message As String, ByVal type As MessageType, Optional ByVal modal As Boolean = False)
        If modal Then
            Me.divAlertModal.Visible = True
            Me.lblMensaje.InnerText = Message
            Me.validar.Value = "0"
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

    Private Sub mt_CargarSemestre()
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
        Try
            obj.AbrirConexion()
            dt = obj.TraerDataTable("ConsultarCicloAcademico", "DAN", "")
            obj.CerrarConexion()
            Call mt_CargarCombo(Me.ddlSemestre, dt, "codigo_Cac", "descripcion_Cac")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub mt_CargarDiseñoAsignatura(ByVal codigo_cac As String)
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
        Try
            obj.AbrirConexion()
            codigo_cac = IIF(String.IsNullOrEmpty(codigo_cac), "-1", codigo_cac)

            dt = obj.TraerDataTable("DiseñoAsignatura_Listar", "", -1, -1, -1, codigo_cac, -1)
            obj.CerrarConexion()
            Call mt_CargarCombo(Me.ddlDiseñoAprendizaje, dt, "codigo_dis", "nombre_Cur")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub mt_CargarUnidad(ByVal codigo_dis As String)
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
        Try
            obj.AbrirConexion()
            codigo_dis = IIF(String.IsNullOrEmpty(codigo_dis), "-1", codigo_dis)

            dt = obj.TraerDataTable("COM_ListarUnidades", codigo_dis)
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
            obj.AbrirConexion()
            codigo_dis = IIF(String.IsNullOrEmpty(codigo_dis), "-1", codigo_dis)
            codigo_uni = IIF(String.IsNullOrEmpty(codigo_uni), "0", codigo_uni)

            dt = obj.TraerDataTable("COM_ListarContenidoAsignatura", codigo_dis, codigo_uni, 0, "NG")
            obj.CerrarConexion()
            Me.gvResultados.DataSource = dt
            Me.gvResultados.DataBind()

            If gvResultados.Rows.Count > 0 Then
                Call mt_AgruparFilas(Me.gvResultados.Rows, 0, 1, "N", 1)
                Call mt_AgruparFilas(Me.gvResultados.Rows, 2, 5)

                'Acceso a: administrador, coord académico de asignatura y comité curricular
                If (Session("cod_ctf") = 1 Or Session("cod_ctf") = 217 Or Session("cod_ctf") = 218) Then
                    Me.btnCombinar.Visible = True
                End If

                'If fc_EsDocenteCoordinador(codigo_dis, cod_user) Then
                '    Me.btnCombinar.Visible = True
                'End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub mt_CargarDatosContenido(ByVal codigo_gru As String)
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString

        Try
            obj.AbrirConexion()
            codigo_gru = IIf(String.IsNullOrEmpty(codigo_gru), "-1", codigo_gru)

            dt = obj.TraerDataTable("COM_ListarContenido", codigo_gru, cod_user)
            obj.CerrarConexion()

            ViewState("dtContenido") = dt
            BindGridContenido()
            dt.Dispose()

            Me.divAlertModal.Visible = False
            Me.lblMensaje.InnerText = ""
            updMensaje.Update()
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Public Sub mt_CargarDatosActividad(ByVal codigo_gru As String)
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString

        Try
            obj.AbrirConexion()
            codigo_gru = IIf(String.IsNullOrEmpty(codigo_gru), "-1", codigo_gru)

            dt = obj.TraerDataTable("COM_ListarActividad", codigo_gru, cod_user)
            obj.CerrarConexion()

            ViewState("dtActividad") = dt
            BindGridActividad()
            dt.Dispose()

            Me.divAlertModal.Visible = False
            Me.lblMensaje.InnerText = ""
            updMensaje.Update()
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Private Sub mt_CargarDatosSesion(ByVal codigo_uni As String, ByVal codigo_gru As String)
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString

        Try
            obj.AbrirConexion()
            codigo_uni = IIf(String.IsNullOrEmpty(codigo_uni), "-1", codigo_uni)
            codigo_gru = IIf(String.IsNullOrEmpty(codigo_gru), "-1", codigo_gru)

            dt = obj.TraerDataTable("COM_SesionAsignatura", codigo_uni, codigo_gru)
            obj.CerrarConexion()

            ViewState("dtSesion") = dt
            BindGridSesion()
            dt.Dispose()

            Me.divAlertModal.Visible = False
            Me.lblMensaje.InnerText = ""
            updMensaje.Update()
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Private Sub mt_mostrarMinimoMaximoSesiones(ByVal codigo_uni As String)
        Dim grupoTot, grupoMen, grupoMay, minimoSes, maximoSes As String
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString

        Try
            grupoTot = "0" : minimoSes = "0" : maximoSes = "0" : grupoMen = "" : grupoMay = ""
            codigo_uni = IIf(String.IsNullOrEmpty(codigo_uni), "0", codigo_uni)

            obj.AbrirConexion()
            dt = obj.TraerDataTable("DEA_minimoMaximoSesiones", codigo_uni)
            obj.CerrarConexion()

            If dt.Rows.Count > 0 Then
                grupoTot = dt.Rows(0).Item(0).ToString
                grupoMen = dt.Rows(0).Item(1).ToString
                minimoSes = dt.Rows(0).Item(2).ToString
                grupoMay = dt.Rows(0).Item(3).ToString
                maximoSes = dt.Rows(0).Item(4).ToString
            End If

            lblTotalGrupos.InnerText = grupoTot
            lblGrupoMenor.InnerText = grupoMen
            lblMinimoClases.InnerText = minimoSes
            lblGrupoMayor.InnerText = grupoMay
            lblMaximoClases.InnerText = maximoSes

            dt.Dispose()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub mt_AgruparFilas(ByVal gridViewRows As GridViewRowCollection, ByVal startIndex As Integer, ByVal totalColumns As Integer, Optional ByVal all As String = "S", Optional ByVal columnIndex As Integer = 0)
        If totalColumns = 0 Then Return
        Dim i As Integer, count As Integer = 1, ant As Integer = 0
        Dim lst As ArrayList = New ArrayList()
        lst.Add(gridViewRows(0))
        Dim ctrl As TableCell
        Dim equalCtrl As Integer
        ctrl = gridViewRows(0).Cells(startIndex)
        equalCtrl = CInt(gvResultados.DataKeys(0).Values("codigo_gru").ToString)

        For i = 1 To gridViewRows.Count - 1
            Dim nextTbCell As TableCell = gridViewRows(i).Cells(startIndex)
            Dim equalTbCell As Integer = CInt(gvResultados.DataKeys(i).Values("codigo_gru").ToString)

            If all.Equals("S") Then
                If ctrl.Text = nextTbCell.Text Then
                    count += 1
                    nextTbCell.Visible = False
                    lst.Add(gridViewRows(i))
                Else
                    If count > 1 Then
                        ctrl.RowSpan = count
                        ctrl.VerticalAlign = VerticalAlign.Middle

                        mt_AgruparFilas(New GridViewRowCollection(lst), startIndex + 1, totalColumns - 1, all)
                    End If

                    count = 1
                    lst.Clear()
                    ctrl = gridViewRows(i).Cells(startIndex)

                    lst.Add(gridViewRows(i))
                End If
            Else
                If equalCtrl = equalTbCell Then
                    count += 1
                    nextTbCell.Visible = False
                    lst.Add(gridViewRows(i))
                Else
                    If count > 1 Then
                        ctrl.RowSpan = count
                        ctrl.VerticalAlign = VerticalAlign.Middle

                        mt_AgruparFilas(New GridViewRowCollection(lst), startIndex + 1, totalColumns - 1, all)
                    End If

                    count = 1
                    lst.Clear()
                    ctrl = gridViewRows(i).Cells(startIndex)
                    
                    lst.Add(gridViewRows(i))
                End If
            End If

            equalCtrl = CInt(gvResultados.DataKeys(i).Values("codigo_gru").ToString)
        Next

        If count > 1 Then
            ctrl.RowSpan = count
            ctrl.VerticalAlign = VerticalAlign.Middle

            mt_AgruparFilas(New GridViewRowCollection(lst), startIndex + 1, totalColumns - 1, all)
        End If

        count = 1
        lst.Clear()
    End Sub

    Private Sub mt_CombinarSesion(ByVal index As Integer)
        Dim cod_grupo, tmp_grupo, totalChk, pre, pos, sesionA, sesionB As Integer
        cod_grupo = -1 : tmp_grupo = -1 : totalChk = 0 : pre = -1 : pos = -1 : sesionA = 0 : sesionB = 0
        cod_grupo = CInt(Me.gvResultados.DataKeys(index).Values("codigo_gru").ToString)

        For i As Integer = 0 To gvResultados.Rows.Count - 1
            Dim chk As CheckBox = CType(gvResultados.Rows(i).FindControl("chkSesion"), CheckBox)
            If chk.Enabled AndAlso chk.Checked Then
                If CInt(Me.gvResultados.DataKeys(i).Values("codigo_gru").ToString) = cod_grupo Then
                    pre = IIf(pre = -1, i, pre)
                    pos = IIf(pos = -1 And pre <> i, i, pos)
                    totalChk += 1
                Else
                    tmp_grupo = CInt(Me.gvResultados.DataKeys(i).Values("codigo_gru").ToString)
                    Exit For
                End If
            End If
        Next

        If tmp_grupo <> -1 AndAlso tmp_grupo <> cod_grupo Then
            Call mt_ShowMessage("Solo se permite unir sesiones del mismo grupo", MessageType.Info)
            Return
        End If

        If totalChk = 0 Then
            Call mt_ShowMessage("No se ha seleccionado ninguna sesión. Por favor, seleccione 2 sesiones del mismo grupo que desea unir", MessageType.Info)
            Return
        ElseIf totalChk <> 2 Then
            Call mt_ShowMessage("Solo se permite seleccionar hasta 2 sesiones dentro del mismo grupo", MessageType.Info)
            Return
        End If

        If pos - pre <> 1 Then
            Call mt_ShowMessage("Solo se permite unir sesiones consecutivas dentro del mismo grupo", MessageType.Info)
            Return
        End If

        sesionA = CInt(Me.gvResultados.DataKeys(pre).Values("codigo_ses").ToString)
        sesionB = CInt(Me.gvResultados.DataKeys(pos).Values("codigo_ses").ToString)

        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
        obj.AbrirConexion()
        obj.Ejecutar("DEA_CombinarSesion", cod_grupo, sesionA, sesionB, cod_user)
        obj.CerrarConexion()
        ddlUnidad_SelectedIndexChanged(Nothing, Nothing)
    End Sub

    Private Sub mt_DescombinarSesion(ByVal index As Integer)
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString

        Try
            Dim cod_grupo, tmp_grupo, codigo_ses, total As Integer
            cod_grupo = CInt(Me.gvResultados.DataKeys(index).Values("codigo_gru").ToString)
            tmp_grupo = -1 : codigo_ses = -1 : total = 0

            '--> Validar que se desagrupe sesiones del mismo grupo
            For i As Integer = 0 To gvResultados.Rows.Count - 1
                Dim chk As CheckBox = CType(gvResultados.Rows(i).FindControl("chkSesion"), CheckBox)
                If chk.Enabled AndAlso chk.Checked Then
                    If CInt(Me.gvResultados.DataKeys(i).Values("codigo_gru").ToString) <> cod_grupo Then
                        tmp_grupo = CInt(Me.gvResultados.DataKeys(i).Values("codigo_gru").ToString)
                        Exit For
                    End If
                End If
            Next

            If tmp_grupo <> -1 AndAlso tmp_grupo <> cod_grupo Then
                Call mt_ShowMessage("Solo se permite desunir sesiones del mismo grupo", MessageType.Info)
                Return
            End If

            '--> Desagrupar las sesiones
            obj.IniciarTransaccion()
            For i As Integer = 0 To gvResultados.Rows.Count - 1
                Dim chk As CheckBox = CType(gvResultados.Rows(i).FindControl("chkSesion"), CheckBox)
                If chk.Enabled AndAlso chk.Checked Then
                    If CInt(Me.gvResultados.DataKeys(i).Values("codigo_gru").ToString) = cod_grupo Then
                        'If Me.gvResultados.DataKeys(i).Values("sesion").ToString.Contains("-") Then
                        total += 1
                        codigo_ses = CInt(Me.gvResultados.DataKeys(i).Values("codigo_ses").ToString)
                        obj.Ejecutar("DEA_DescombinarSesion", cod_grupo, codigo_ses, cod_user)
                        'End If
                    End If
                End If
            Next
            obj.TerminarTransaccion()

            If total = 0 Then
                Call mt_ShowMessage("No se ha seleccionado ninguna sesión agrupada para desunir", MessageType.Info)
                Return
            End If

            ddlUnidad_SelectedIndexChanged(Nothing, Nothing)
        Catch ex As Exception
            obj.AbortarTransaccion()
        End Try
    End Sub

#End Region

#Region "Funciones"

    Private Function fc_Validar(ByVal dtContenido As Data.DataTable, ByVal dtActividad As Data.DataTable, ByVal dtSesion As Data.DataTable) As Boolean
        'If dtContenido.Rows.Count <= 0 Or dtActividad.Rows.Count <= 0 Then
        '    Session("mensaje") = "No se ha ingresado Contenidos o Actividades"
        '    Return False
        'End If

        'If String.IsNullOrEmpty(dtContenido.Rows(0).Item(1).ToString) Then
        '    Session("mensaje") = "No se ha ingresado Contenidos"
        '    Return False
        'End If

        If String.IsNullOrEmpty(dtActividad.Rows(0).Item(1).ToString) Then
            Session("mensaje") = "No se ha ingresado Actividades"
            Return False
        End If

        If dtSesion.Rows(0).Item(1).ToString.Equals("--") Then
            Session("mensaje") = "No se ha seleccionado ninguna sesión"
            Return False
        Else
            Dim totalChk As Integer = 0
            For i As Integer = 0 To Me.gvSesion.Rows.Count - 1
                Dim chk As CheckBox = CType(gvSesion.Rows(i).FindControl("chkSeleccionado"), CheckBox)
                If chk.Checked And chk.Enabled Then
                    totalChk += 1
                End If
            Next

            If totalChk = 0 Then
                Session("mensaje") = "No se ha seleccionado ninguna sesión"
                Return False
            End If
        End If

        Return True
    End Function

    Private Function fc_EsDocenteCoordinador(ByVal codigo_dis As String, ByVal codigo_per As String) As Boolean
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString

        Try
            obj.AbrirConexion()
            codigo_dis = IIf(String.IsNullOrEmpty(codigo_dis), "-1", codigo_dis)
            codigo_per = IIf(String.IsNullOrEmpty(codigo_per), "-1", codigo_per)

            dt = obj.TraerDataTable("COM_DocenteCoordinador_Listar", "DA", codigo_dis, "-1", "-1", "-1", codigo_per)
            obj.CerrarConexion()

            If dt.Rows.Count > 0 Then
                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
            Return False
        End Try
    End Function

#End Region

End Class
