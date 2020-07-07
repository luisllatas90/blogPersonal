﻿
Partial Class GestionCurricular_FrmGrupoAsignatura
    Inherits System.Web.UI.Page

#Region "Declaracion de Variables"

    Dim cod_user As Integer
    Dim cod_ctf As Integer

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

            If Not String.IsNullOrEmpty(Session("cod_ctf")) Then
                cod_ctf = Session("cod_ctf")
            Else
                cod_ctf = Request.QueryString("ctf")
            End If

            Me.ClientScript.GetPostBackEventReference(Me, String.Empty)

            If Not IsPostBack Then
                Dim dtElimAlu As Data.DataTable = New Data.DataTable("dtElimAlu")
                dtElimAlu.Columns.Add("codigo_dma")
                ViewState("dtElimAlu") = dtElimAlu

                Session("gc_auth") = Nothing
                Call mt_CargarSemestre()
                Call mt_CargarDatos("0", "0", cod_user)
                tabDetalle.Visible = False
            Else
                Dim eventTarget As String
                Dim eventArgument As String

                If Me.Request("__EVENTTARGET") Is Nothing Then eventTarget = String.Empty Else eventTarget = Me.Request("__EVENTTARGET")
                If Me.Request("__EVENTARGUMENT") Is Nothing Then eventArgument = String.Empty Else eventArgument = Me.Request("__EVENTARGUMENT")

                If eventTarget = "returnOpt" Then
                    Dim auth As String = eventArgument
                    Session("gc_auth") = auth
                    Call onEliminarGrupo(sender, e)
                End If
            End If
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", ""), MessageType.Warning)
        End Try
    End Sub

    Protected Sub Page_LoadComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.LoadComplete
        Session("cod_ctf") = cod_ctf
    End Sub

    Protected Sub cboSemestre_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboSemestre.SelectedIndexChanged
        Try
            Call mt_CargarCarreraProfesional(cboSemestre.SelectedValue, cod_user)
            Call mt_CargarDatos(cboSemestre.SelectedValue, cboCarrProf.SelectedValue, cod_user)
            Call mt_CargarDatosDetalle(0)
            tabDetalle.Visible = False
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", ""), MessageType.Warning)
        End Try
    End Sub

    Protected Sub cboCarrProf_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboCarrProf.SelectedIndexChanged
        Try
            Call mt_CargarDatos(cboSemestre.SelectedValue, cboCarrProf.SelectedValue, cod_user)
            Call mt_CargarDatosDetalle(0)
            tabDetalle.Visible = False
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", ""), MessageType.Warning)
        End Try
    End Sub

    Protected Sub gvAsignatura_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gvAsignatura.PageIndexChanging
        Call mt_CargarDatos(cboSemestre.SelectedValue, cboCarrProf.SelectedValue, cod_user)

        Me.gvAsignatura.PageIndex = e.NewPageIndex
        Me.gvAsignatura.DataBind()
    End Sub

    Protected Sub OnEditarGrupo(ByVal sender As Object, ByVal e As EventArgs)
        Try
            Dim row As GridViewRow = TryCast((TryCast(sender, LinkButton)).NamingContainer, GridViewRow)
            Dim codigo_cup As String = gvAsignatura.DataKeys(row.RowIndex).Item("codigo_cup").ToString
            Dim codigo_gru As String = gvAsignatura.DataKeys(row.RowIndex).Item("grupoHor_Cup").ToString
            Dim ciclo_des As String = gvAsignatura.DataKeys(row.RowIndex).Item("ciclo_Cur").ToString
            Dim curso_des As String = gvAsignatura.DataKeys(row.RowIndex).Item("nombre_Cur").ToString
            Dim total_gru As Integer = CInt(gvAsignatura.DataKeys(row.RowIndex).Item("totalGrupo").ToString)

            total_gru += 1
            lblCurso.Text = ciclo_des & ", " & curso_des
            txtGrupo.Text = codigo_gru & total_gru

            Session("gc_codigoCup") = codigo_cup

            Call mt_CargarDatosDetalle(codigo_cup)
            tabDetalle.Visible = True
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", ""), MessageType.Warning)
        End Try
    End Sub

    Protected Sub onAgregarGrupo(ByVal sender As Object, ByVal e As EventArgs)
        Try
            Dim rpta As String, msje As String
            Dim obj As New ClsConectarDatos
            Dim dt As New Data.DataTable
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString

            obj.AbrirConexion()
            dt = obj.TraerDataTable("DEA_subGrupo_insertar", Session("gc_codigoCup"), cod_user)
            obj.CerrarConexion()

            If dt.Rows.Count > 0 Then
                rpta = dt.Rows(0).Item(0).ToString
                msje = dt.Rows(0).Item(1).ToString

                If rpta.Equals("0") Then
                    Call mt_ShowMessage(msje, MessageType.Warning)
                Else
                    txtGrupo.Text = msje
                    Call mt_ShowMessage("Subgrupo registrado con éxito", MessageType.Success)
                End If
            Else
                Call mt_ShowMessage("La sesión se ha perdido, el registro no fue procesado", MessageType.Warning)
            End If
            dt.Dispose()

            Call mt_CargarDatos(cboSemestre.SelectedValue, cboCarrProf.SelectedValue, cod_user)
            Call mt_CargarDatosDetalle(Session("gc_codigoCup"))
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", ""), MessageType.Warning)
        End Try
    End Sub

    Protected Sub onAgregarAlumno(ByVal sender As Object, ByVal e As EventArgs)
        Try
            Dim row As GridViewRow = TryCast((TryCast(sender, LinkButton)).NamingContainer, GridViewRow)
            Dim codigo_cup As String = Session("gc_codigoCup")
            Dim codigo_sgr As String = gvDetalle.DataKeys(row.RowIndex).Item("codigo_sgr").ToString
            Session("gc_codigoSgr") = codigo_sgr

            Call mt_CargarDatosAlumno(codigo_cup, cboSemestre.SelectedValue, codigo_sgr)
            Page.RegisterStartupScript("Pop", "<script>openModal();</script>")
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", ""), MessageType.Warning)
        End Try
    End Sub

    Protected Sub onEliminarGrupo(ByVal sender As Object, ByVal e As EventArgs)
        Try
            Dim auth As String = "NO"

            If Session("gc_codigoCup") Is Nothing Then
                Call mt_ShowMessage("La sesión se ha perdido, vuelva a recargar la página", MessageType.Error)
                Return
            End If

            If Session("gc_auth") IsNot Nothing Then
                auth = IIf(Session("gc_auth") = "yes", "SI", "NO")
            Else
                Dim row As GridViewRow = TryCast((TryCast(sender, LinkButton)).NamingContainer, GridViewRow)
                Dim codigo_cup As String = Session("gc_codigoCup")
                Dim codigo_sgr As String = gvDetalle.DataKeys(row.RowIndex).Item("codigo_sgr").ToString

                Session("gc_codigoSgr") = codigo_sgr
            End If

            Dim obj As New ClsConectarDatos
            Dim dt As New Data.DataTable
            Dim rpta As Integer = 0, msje As String = "", grupo As String = ""
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString

            obj.AbrirConexion()
            dt = obj.TraerDataTable("DEA_subGrupo_eliminar", Session("gc_codigoSgr"), Session("gc_codigoCup"), cod_user, auth)
            obj.CerrarConexion()

            If dt.Rows.Count > 0 Then
                rpta = dt.Rows(0).Item(0).ToString
                msje = dt.Rows(0).Item(1).ToString
                grupo = dt.Rows(0).Item(2).ToString
            End If
            dt.Dispose()

            If Not String.IsNullOrEmpty(msje) Then
                Session("gc_auth") = Nothing

                If rpta = 2 Then
                    Call mt_ShowMessage(msje, MessageType.Warning)
                ElseIf rpta = 1 Then
                    txtGrupo.Text = grupo
                    Call mt_ShowMessage(msje, MessageType.Success)

                    Call mt_CargarDatos(cboSemestre.SelectedValue, cboCarrProf.SelectedValue, cod_user)
                    Call mt_CargarDatosDetalle(Session("gc_codigoCup"))
                Else
                    If auth.Equals("NO") Then
                        ClientScript.RegisterStartupScript(Me.GetType, "Pop", "<script>mostrarMensaje('yes', '" & msje & "', 'danger');</script>")
                    End If
                End If
            End If
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", ""), MessageType.Warning)
        End Try
    End Sub

    Protected Sub OnCheck(ByVal sender As Object, ByVal e As EventArgs)
        Dim chk As CheckBox = TryCast(sender, CheckBox)
        Dim row As GridViewRow = TryCast(chk.NamingContainer, GridViewRow)
        Dim codigo_dma As String = gvAlumno.DataKeys(row.RowIndex).Item("codigo_dma").ToString

        If Not String.IsNullOrEmpty(codigo_dma) And Not chk.Checked AndAlso chk.Enabled Then
            Dim dtElimAlu As Data.DataTable = TryCast(ViewState("dtElimAlu"), Data.DataTable)
            dtElimAlu.Rows.Add(codigo_dma)
            ViewState("dtElimAlu") = dtElimAlu
            Me.udpAlumno.Update()
        End If

        Call mt_ContarAlumnos()
    End Sub

    Protected Sub btnGrabar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGrabar.Click
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString

        Try
            obj.IniciarTransaccion()

            'If Not (IsDBNull(Session("perlogin"))) And Session("perlogin").ToString <> "" Then
            Dim codigo_dma As String
            Dim codigos As String = ""
            Dim dtElimAlu As Data.DataTable = TryCast(ViewState("dtElimAlu"), Data.DataTable)

            '--> Confirmar eliminación de Alumnos
            For i As Integer = 0 To dtElimAlu.Rows.Count - 1
                codigo_dma = dtElimAlu.Rows(i).Item(0).ToString
                obj.Ejecutar("DEA_matriculaSubGrupo_eliminar", Session("gc_codigoSgr"), codigo_dma, cod_user)
            Next

            '--> Confirmar registro de Alumnos
            For i As Integer = 0 To gvAlumno.Rows.Count - 1
                Dim chkSelec As CheckBox = CType(gvAlumno.Rows(i).FindControl("ChkSelec"), CheckBox)

                If chkSelec.Enabled And chkSelec.Checked Then
                    codigo_dma = gvAlumno.DataKeys(i).Item("codigo_dma").ToString
                    codigos = IIf(String.IsNullOrEmpty(codigos), codigo_dma, codigos & "," & codigo_dma)
                End If
            Next

            obj.Ejecutar("DEA_matriculaSubGrupo_insertar", Session("gc_codigoSgr"), Session("gc_codigoCup"), codigos, cod_user)
            obj.TerminarTransaccion()

            Call mt_CargarDatosDetalle(Session("gc_codigoCup"))
            'Else
            'Throw New Exception("Inicie Sesión")
            'End If
        Catch ex As Exception
            obj.AbortarTransaccion()
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

#End Region

#Region "Métodos"

    Protected Sub mt_ShowMessage(ByVal Message As String, ByVal type As MessageType)
        Page.RegisterStartupScript("Mensaje", "<script>ShowMessage('" & Message & "','" & type.ToString & "');</script>")
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
            dt = obj.TraerDataTable("ConsultarCicloAcademico", "DA", "")
            obj.CerrarConexion()

            mt_CargarCombo(Me.cboSemestre, dt, "codigo_Cac", "descripcion_Cac")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub mt_CargarCarreraProfesional(ByVal codigo_cac As String, ByVal user As Integer)
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
        Try
            codigo_cac = IIf(String.IsNullOrEmpty(codigo_cac), "0", codigo_cac)

            Dim codigo_dac As Integer = 7
            If cod_ctf = 1 Then 'Cargar carreras al usuario administrador. Por Q.T| 23DIC2019
                codigo_dac = -1
                user = -1
            End If

            obj.AbrirConexion()
            dt = obj.TraerDataTable("DEA_ListarCarrerasProfesionales", codigo_cac, user, 2, 1, codigo_dac)
            obj.CerrarConexion()

            mt_CargarCombo(Me.cboCarrProf, dt, "codigo_Cpf", "nombre_Cpf")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub mt_CargarDatos(ByVal codigo_cac As String, ByVal codigo_cpf As String, ByVal user As Integer)
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
        Try
            codigo_cac = IIf(String.IsNullOrEmpty(codigo_cac), "0", codigo_cac)
            codigo_cpf = IIf(String.IsNullOrEmpty(codigo_cpf), "0", codigo_cpf)

            If CInt(codigo_cac) > 0 And CInt(codigo_cpf) > 0 Then
                obj.AbrirConexion()
                dt = obj.TraerDataTable("DEA_subGrupoProgramado_listar", codigo_cac, codigo_cpf, user, "")
                obj.CerrarConexion()
            End If

            Me.gvAsignatura.DataSource = dt
            Me.gvAsignatura.DataBind()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub mt_CargarDatosDetalle(ByVal codigo_cup As Integer)
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
        Try
            obj.AbrirConexion()
            dt = obj.TraerDataTable("DEA_subGrupoDetalle_listar", codigo_cup)
            obj.CerrarConexion()

            Me.gvDetalle.DataSource = dt
            Me.gvDetalle.DataBind()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub mt_CargarDatosAlumno(ByVal codigo_cup As Integer, ByVal codigo_cac As Integer, ByVal codigo_sgr As Integer)
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString

        Try
            obj.AbrirConexion()
            dt = obj.TraerDataTable("DEA_subGrupoAlumno_listar", codigo_cup, codigo_cac, codigo_sgr, "D")
            obj.CerrarConexion()

            Me.gvAlumno.DataSource = dt
            Me.gvAlumno.DataBind()

            Call mt_ContarAlumnos()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub mt_ContarAlumnos()
        Dim sel As Integer = 0

        'For i As Integer = 0 To gvAlumno.Rows.Count - 1
        '    'Dim aaa As CheckBox = TryCast(gvAlumno.Rows(i).Cells(0), CheckBox)
        '    Dim aux As CheckBox = TryCast(gvAlumno.DataKeys(row.RowIndex).Item("habilitar"), CheckBox)
        'Next


        For Each rows As GridViewRow In gvAlumno.Rows
            Dim aux As CheckBox = TryCast(rows.FindControl("chkSelec"), CheckBox)
            If aux IsNot Nothing AndAlso aux.Enabled AndAlso aux.Checked Then
                sel += 1
            End If
        Next

        lblAlumnos.InnerText = String.Format("Usted ha seleccionado {0} alumno(s)", sel)
        updSeleccion.Update()
    End Sub

#End Region

End Class
