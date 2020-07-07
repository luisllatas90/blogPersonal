﻿
Partial Class administrativo_pec_FrmCombinacionescursos
    Inherits System.Web.UI.Page

#Region "Variables"

    Private C As ClsConectarDatos
    Private nuevo As Boolean = False
    Private cod_user As Integer '= 684
    Private cod_ctf As Integer '= 1
    'Private ruta As String = ConfigurationManager.AppSettings("SharedFiles") '"http://localhost/campusvirtual/ArchivosCompartidos/SharedFiles.asmx"
    Dim LastCategory As String = String.Empty
    Dim CurrentRow As Integer = -1

    Public Enum MessageType
        Success
        [Error]
        Info
        Warning
    End Enum

#End Region

#Region "Eventos"

    Sub New()
        If C Is Nothing Then
            C = New ClsConectarDatos
            C.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If (Session("id_per") Is Nothing) Then
                Response.Redirect("../../../sinacceso.html")
            End If

            cod_user = Session("id_per")
            cod_ctf = Request.QueryString("ctf")

            If Not IsPostBack Then
                'Dim dt As Data.DataTable = New Data.DataTable()
                'Dim dtElim As Data.DataTable = New Data.DataTable()

                'dt.Columns.Add("codigo_com")
                'dt.Columns.Add("codigo_per")
                'dt.Columns.Add("nombre_per")
                'dt.Columns.Add("codigo_mie")
                'dt.Columns.Add("rol_mie")
                'dt.Columns.Add("vigente_mie")
                'ViewState("dt") = dt

                'dtElim.Columns.Add("codigo_mie")
                'dtElim.Columns.Add("codigo_com")
                'ViewState("dtElim") = dtElim

                'btnFuArchivo.Attributes.Add("onClick", "document.getElementById('" + fuArchivo.ClientID + "').click();")

                'Call BindGrid()
                Call mt_CargarCarreras()
                Call mt_CargarSemestre()
                'Call mt_LimpiarFormulario()
                'Call mt_MostrarDetalle(ddlCarreraProf.SelectedValue)
                mt_MostrarDiv("0")

            Else
                'Call RefreshGrid()
            End If
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", ""), MessageType.Error)
        End Try
    End Sub

    'Protected Sub ddlCarreraProf_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlCarreraProf.SelectedIndexChanged
    '    Call mt_MostrarDetalle(Me.ddlCarreraProf.SelectedValue)
    'End Sub
    Protected Sub grwResultado_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grwResultado.RowCommand
        Try
            Dim index As Integer = Convert.ToInt32(e.CommandArgument)
            If (e.CommandName = "Editar") Then

                Me.hcomb.value = CInt(grwResultado.DataKeys(index).Values("id").ToString())

                Dim dt As New Data.DataTable

                C.AbrirConexion()
                dt = C.TraerDataTable("Escuela_combListar", "1", Me.hcomb.value, 0, 0)
                C.CerrarConexion()


                If dt.Rows.Count > 0 Then
                    Me.ddlEscuelaReg.SelectedValue = dt.Rows(0).Item("codigo_cpf")
                    Me.ddlCicloReg.SelectedValue = dt.Rows(0).Item("codigo_cac")
                    Me.txtnrocomb.Text = dt.Rows(0).Item("nrocombinacion")
                    If dt.rows(0).item("estado") = "Activo" Then
                        Me.chkActivo.checked = True
                    Else
                        Me.chkActivo.checked = False
                    End If
                    Call mt_MostrarDiv("1")
                    Me.ddlEscuelaReg.enabled = False
                    Me.ddlCicloReg.enabled = False
                Else
                    Call mt_ShowMessage("No se encontro datos", MessageType.Error)
                End If

            ElseIf (e.CommandName = "CombDet") Then
                Me.hcomb.value = CInt(grwResultado.DataKeys(index).Values("id").ToString())


                Dim codigo_cac As Integer = CInt(grwResultado.DataKeys(index).Values("codigo_cac").ToString())

                ' response.write(ID_COMB & "<br>")
                ' response.write(codigo_cac & "<br>")
                'response.write(CInt(gDataComb.DataKeys(index).Values("codigo_cpf").ToString()) & "<br>")


                Dim NRO_COMB As Integer = 0
                NRO_COMB = CInt(grwResultado.DataKeys(index).Values("nrocombinacion").ToString())
                'nrocombinacion
                mt_MostrarDiv("2")
                Me.lblEscuela.Value = grwResultado.DataKeys(index).Values("nombre_Cpf").ToString()
                Me.lblCiclo.Value = grwResultado.DataKeys(index).Values("cicloacademico").ToString()

                C.AbrirConexion()
                Dim objFun As New ClsFunciones

                objFun.CargarListas(Me.ddlPlanEstudio, C.TraerDataTable("ConsultarPlanEstudio", "CT", CInt(grwResultado.DataKeys(index).Values("codigo_cpf").ToString()), 2), "codigo_Pes", "descripcion_Pes")
                objFun.CargarListas(Me.ddlCurso, C.TraerDataTable("Escuela_combCursoProgListar", "1", 0, ddlPlanEstudio.SelectedValue, codigo_cac), "codigo_Cup", "descripcion_cup")

                C.CerrarConexion()


                mt_LlenarComboCombinacion(NRO_COMB)
                mt_consultarDetCombinacion()
             
                Me.btnDetCerrar.Visible = False
                Me.divInfoEdit.Visible = False
                Me.txtinfoeditar.Text = ""
                'consultarDetCombinacion()
            End If

        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", ""), MessageType.Error)
        End Try
    End Sub
    Protected Sub grwResultado_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grwResultado.RowDataBound
        'If e.Row.RowType = DataControlRowType.DataRow Then
        '    Dim celda As TableCellCollection = e.Row.Cells
        '    Dim com As String = Me.grwResultado.DataKeys(e.Row.RowIndex).Values.Item("codigo_com")
        '    Dim nom As String = Me.grwResultado.DataKeys(e.Row.RowIndex).Values.Item("nombre_com")
        '    Dim cpf As String = Me.grwResultado.DataKeys(e.Row.RowIndex).Values.Item("codigo_cpf")
        '    Dim des As String = Me.grwResultado.DataKeys(e.Row.RowIndex).Values.Item("nombre_cpf")
        '    Dim tdc As String = Me.grwResultado.DataKeys(e.Row.RowIndex).Values.Item("bloqueado")
        '    Dim arc As String = Me.grwResultado.DataKeys(e.Row.RowIndex).Values.Item("idArchivo")
        '    Dim idx As Integer = e.Row.RowIndex + 1

        '    Dim btnEditar As New HtmlButton
        '    With btnEditar
        '        .ID = "btnEditar" & idx
        '        .Attributes.Add("type", "button")
        '        .Attributes.Add("codigo_com", com)
        '        .Attributes.Add("nombre_com", nom)
        '        .Attributes.Add("codigo_cpf", cpf)
        '        .Attributes.Add("nombre_cpf", des)

        '        If CInt(tdc) = 0 Then
        '            .Attributes.Remove("disabled")
        '            .Attributes.Add("class", "btn btn-primary btn-sm")
        '            .Attributes.Add("title", "Editar Comité")
        '            .InnerHtml = "<i class='fa fa-edit' title='Editar Comité'></i>"
        '        Else
        '            .Attributes.Add("disabled", True)
        '            .Attributes.Add("class", "btn btn-primary btn-sm")
        '            .Attributes.Add("title", "La edición del comité está bloqueada")
        '            .InnerHtml = "<i class='fa fa-edit' title='La edición del comité está bloqueada'></i>"
        '        End If

        '        AddHandler .ServerClick, AddressOf btnEditar_Click
        '    End With

        '    celda(3).Controls.Add(btnEditar)

        '    Dim btnDescargar As New HtmlButton
        '    With btnDescargar
        '        .ID = "btnDescargar" & idx
        '        .Attributes.Add("class", "btn btn-info btn-sm")
        '        .Attributes.Add("type", "button")
        '        .Attributes.Add("codigo_com", com)
        '        .Attributes.Add("idArchivo", arc)
        '        .Attributes.Add("title", "Descargar resolución")
        '        .InnerHtml = "<i class='fa fa-download' title='Descargar resolución'></i>"

        '        AddHandler .ServerClick, AddressOf btnDescargar_Click
        '    End With
        '    celda(4).Controls.Add(btnDescargar)
        'End If
    End Sub
    Protected Sub gDataCombDet_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles gDataCombDet.RowDeleting
        Try
            Dim script As String = ""

            C.AbrirConexion()
            script = "Combinación eliminada con éxito"
            C.Ejecutar("Escuela_detcombReg", "E", CInt(gDataCombDet.DataKeys(e.RowIndex).Value.ToString()), 0, 0, 0, 0, 0, Session("perlogin"), 0)
            C.CerrarConexion()




            'ScriptManager.RegisterStartupScript(Me, GetType(Page), "alert", script, True)
            'fnNotificacion(script)
            Call mt_ShowMessage(script, MessageType.Success)
            mt_consultarDetCombinacion()



        Catch ex As Exception
             Call mt_ShowMessage(ex.Message.Replace("'", ""), MessageType.Error)
        End Try
    End Sub
    Protected Sub gDataCombDet_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gDataCombDet.RowCommand
        Try


            If (e.CommandName.Equals("Select")) Then
                Dim seleccion As GridViewRow
                ' Dim ID_COMBDET As Integer

                '1. Obtengo la linea del gridview que fue cliqueada
                seleccion = DirectCast(e.CommandSource, GridView).Rows(e.CommandArgument)
                '2. Obtengo el datakey de la linea que donde está el boton que cliqueé

                Me.hcombdet.value = CInt(Me.gDataCombDet.DataKeys(seleccion.RowIndex).Values("id").ToString())


                Dim dt As New Data.DataTable

                C.AbrirConexion()
                dt = C.TraerDataTable("Escuela_combDetListar", "1", CInt(Me.hcombdet.value), 0, 0, 0)
                ' Session("dtDetalle") = dt
                C.CerrarConexion()

                If dt.Rows.Count > 0 Then
                    Me.ddlCurso.SelectedValue = dt.Rows(0).Item("codigo_cup")
                    Me.ddlCombinacion.SelectedValue = dt.Rows(0).Item("nrocombinacion")
                    Me.txtdetnumero.Text = dt.Rows(0).Item("nroestudantes")

                    Me.btnDetCerrar.Visible = True
                    Me.divInfoEdit.Visible = True
                    Me.txtinfoeditar.Text = "Se va a editar: " & dt.Rows(0).Item("curso").ToString() & " [" & dt.Rows(0).Item("grupo").ToString() & "], COMBINACION Nro: " & dt.Rows(0).Item("nrocombinacion").ToString() & ",  Nro Estudiantes: " & dt.Rows(0).Item("nroestudantes").ToString()
                Else
                    Me.btnDetCerrar.Visible = False
                    Me.divInfoEdit.Visible = False
                    Me.txtinfoeditar.Text = ""
                End If

                mt_ValidaGridDetalle()
            End If


        Catch ex As Exception
            Dim script As String = "fnMensaje('error','1" & ex.Message & "')"
            ScriptManager.RegisterStartupScript(Me, GetType(Page), "alert", script, True)
        End Try
    End Sub


    Protected Sub gDataCombDet_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gDataCombDet.RowDataBound
        Try

            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim row As Data.DataRowView = CType(e.Row.DataItem, Data.DataRowView)
                If LastCategory = row("nrocombinacion").ToString Then
                    If (gDataCombDet.Rows(CurrentRow).Cells(0).RowSpan = 0) Then
                        gDataCombDet.Rows(CurrentRow).Cells(0).RowSpan = 2
                    Else
                        gDataCombDet.Rows(CurrentRow).Cells(0).RowSpan += 1
                    End If
                    'e.Row.Cells.RemoveAt(0)
                    e.Row.Cells(0).Visible = False
                Else
                    e.Row.VerticalAlign = VerticalAlign.Middle
                    LastCategory = row("nrocombinacion").ToString()
                    CurrentRow = e.Row.RowIndex
                End If
            End If
            mt_ValidaGridDetalle()
            'If e.Row.RowIndex >= 0 Then
            '    If e.Row.Cells(4).Text = "0" Then
            '        e.Row.Cells(5).Text = ""
            '        e.Row.Cells(6).Text = ""
            '    End If
            '    If e.Row.Cells(3).Text <> e.Row.Cells(4).Text Then
            '        'e.Row.Cells(5).Text = ""
            '        e.Row.Cells(6).Text = ""
            '    End If
            'End If
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", ""), MessageType.Error)
        End Try
    End Sub

    Protected Sub btnCrear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCrear.Click
        Try
            Dim cpf As String = Me.ddlEscuela.SelectedValue
            Dim ciclo As String = Me.ddlCiclo.SelectedValue
            Me.ddlEscuelaReg.enabled = True
            Me.ddlCicloReg.enabled = True
            Me.ddlEscuelaReg.selectedValue = cpf
            Me.ddlCicloReg.selectedValue = ciclo

            'Session("codigo_com") = ""
            'Session("codigo_cpf") = cpf

            'ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script>openModal('nuevo', '" & des & "');</script>")
            'Call mt_CargarDatos()
            'Call ddlCarreraProf_SelectedIndexChanged(sender, e)
            'Call mt_MostrarDetalle(cpf)
            Call mt_MostrarDiv("1")
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", ""), MessageType.Error)
        End Try
    End Sub

    Protected Sub btnConsultar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnConsultar.Click
       mt_CargarDatos
    End Sub

    Protected Sub btnDetRegresar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDetRegresar.Click
        Call mt_MostrarDiv("3")
        Call mt_LimpiarForm("1")
    End Sub
    Protected Sub btnDetConsultar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDetConsultar.Click
        mt_consultarDetCombinacion()
    End Sub
    Protected Sub ddlCiclo_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlCiclo.SelectedIndexChanged
        Call mt_CargarDatos()

    End Sub
    Protected Sub ddlEscuela_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlEscuela.SelectedIndexChanged
        Call mt_CargarDatos()

    End Sub
    Protected Sub btnGrabar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGrabar.Click
        Try

            If fc_ValidarRegistroCombinacion() Then

                Dim ope As String = ""
                Dim script As String = ""
                Dim idComb As Integer = Me.hcomb.value

                If Me.hcomb.value > 0 Then
                    ope = "A"
                    script = "Se modificó con éxito"
                Else
                    ope = "I"
                    script = "Se registró con éxito"
                End If
                C.AbrirConexion()
                C.Ejecutar("Escuela_combReg", ope, idComb, Me.ddlEscuelaReg.SelectedValue, Me.ddlCicloReg.SelectedValue, CInt(Me.txtnrocomb.Text), Session("perlogin"), Me.chkActivo.Checked)
                C.CerrarConexion()

                Call mt_ShowMessage(script.ToString, MessageType.Success)
                ' ScriptManager.RegisterStartupScript(Me, GetType(Page), "alert", script, True)
                Call mt_MostrarDiv("0")
                Call mt_LimpiarForm("0")
                Call mt_CargarDatos()


            End If

         
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", ""), MessageType.Error)
        End Try
    End Sub

    Protected Sub btnDetAgregar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDetAgregar.Click
        mt_RegistrarDetalleCombinacion()
        mt_ValidaGridDetalle()
    End Sub
    Protected Sub btnCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        Try
            'Dim button As HtmlButton = DirectCast(sender, HtmlButton)
            'Dim com As String = button.Attributes("codigo_com")
            'Dim nom As String = button.Attributes("nombre_com")
            'Dim cpf As String = button.Attributes("codigo_cpf")
            'Dim des As String = button.Attributes("nombre_cpf")

            'Session("codigo_com") = com
            'Session("codigo_cpf") = cpf

            'ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script>openModal('editar','');</script>")
            'Call mt_LlenarFormulario()
            'Call mt_CargarDatos()

            'Call ddlCarreraProf_SelectedIndexChanged(sender, e)
            'Call mt_MostrarDetalle(cpf)
            Call mt_MostrarDiv("0")
            Call mt_LimpiarForm("0")
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", ""), MessageType.Error)
        End Try
    End Sub

    Protected Sub btnDetCerrar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDetCerrar.Click
        mt_LimpiarForm("1")
        mt_ValidaGridDetalle()
    End Sub

    'Protected Sub btnValidar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnValidar.ServerClick
    '    Try
    '        Dim valid As Generic.Dictionary(Of String, String) = fc_Validar()

    '        If valid.Item("rpta") = 1 Then
    '            Me.divAlertModal.Visible = False
    '            Me.validar.Value = "1"
    '            updMensaje.Update()
    '        Else
    '            ScriptManager.GetCurrent(Me.Page).SetFocus(Me.divAlertModal)
    '            ScriptManager.GetCurrent(Me.Page).SetFocus(Me.lblMensaje)

    '            Call mt_ShowMessage(valid.Item("msg"), MessageType.Info, True)
    '        End If
    '    Catch ex As Exception
    '        Call mt_ShowMessage(ex.Message.Replace("'", ""), MessageType.Error, True)
    '    End Try
    'End Sub

    'Protected Sub btnAceptar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAceptar.Click
    'Try
    '    Dim dt As New Data.DataTable
    '    Dim flag As Boolean = False
    '    Dim cod_cpf As Object = IIf(String.IsNullOrEmpty(Session("codigo_cpf")), "", Session("codigo_cpf"))
    '    Dim cod_com As Object = IIf(String.IsNullOrEmpty(Session("codigo_com")), "", Session("codigo_com"))

    '    C.IniciarTransaccion()

    '    If String.IsNullOrEmpty(cod_com) Then
    '        nuevo = True
    '        dt = C.TraerDataTable("COM_RegistrarComiteCurricular", cod_cpf, txtNombre.Text.ToUpper, CDate(txtIniAprobacion.Text), CDate(txtFinAprobacion.Text), ddlSemestre.SelectedValue, txtNroDecreto.Text.ToUpper, 0, cod_user)
    '        If dt.Rows.Count > 0 Then
    '            cod_com = dt.Rows(0).Item(0).ToString
    '            Session("codigo_com") = cod_com
    '            flag = True
    '        End If
    '    End If

    '    If Me.fuArchivo.HasFile Then
    '        Dim Archivos As HttpFileCollection = Request.Files
    '        For i As Integer = 0 To Archivos.Count - 1
    '            fc_SubirArchivo(idTabla, cod_com, Archivos(i))
    '        Next
    '    End If

    '    dt = C.TraerDataTable("COM_ActualizarComiteCurricular", cod_com, cod_cpf, txtNombre.Text.ToUpper, CDate(txtIniAprobacion.Text), CDate(txtFinAprobacion.Text), ddlSemestre.SelectedValue, txtNroDecreto.Text.ToUpper, 0, idTabla, cod_user)
    '    If dt.Rows.Count > 0 Then
    '        flag = True
    '    End If

    '    If flag Then
    '        Dim dtDet As Data.DataTable = TryCast(ViewState("dt"), Data.DataTable)
    '        Dim dtElim As Data.DataTable = TryCast(ViewState("dtElim"), Data.DataTable)

    '        '--> Confirmar eliminación de miembros del comité
    '        For i As Integer = 0 To dtElim.Rows.Count - 1
    '            Dim codigo_mie As Object
    '            codigo_mie = dtElim.Rows(i).Item(0).ToString
    '            codigo_mie = IIf(String.IsNullOrEmpty(codigo_mie), "0", codigo_mie)
    '            C.Ejecutar("COM_EliminarComiteCurricularMiembros", codigo_mie, cod_user)
    '        Next

    '        If dtDet.Rows.Count > 0 Then
    '            '--> Confirmar registro o actualización de miembros del comité
    '            For i As Integer = 0 To dtDet.Rows.Count - 1
    '                Dim codigo_per As Integer
    '                Dim rol_mie As String
    '                Dim codigo_mie As Object
    '                codigo_mie = dtDet.Rows(i).Item(3).ToString
    '                codigo_mie = IIf(String.IsNullOrEmpty(codigo_mie), "0", codigo_mie)

    '                codigo_per = dtDet.Rows(i).Item(1).ToString
    '                rol_mie = dtDet.Rows(i).Item(4).ToString

    '                C.Ejecutar("COM_RegistrarMiembrosComiteCurricular", cod_com, codigo_mie, codigo_per, rol_mie, cod_user)
    '            Next
    '        End If
    '    End If

    '    C.TerminarTransaccion()

    '    If flag Then
    '        Call mt_MostrarDetalle(cod_cpf)

    '        If nuevo Then
    '            Call mt_ShowMessage("Registro satisfactorio del comité", MessageType.Success)
    '        Else
    '            Call mt_ShowMessage("Actualización satisfactoria del comité", MessageType.Success)
    '        End If
    '    End If
    'Catch ex As Exception
    '    C.AbortarTransaccion()
    '    Call mt_ShowMessage(ex.Message.Replace("'", ""), MessageType.Error, True)
    'End Try
    'End Sub

    Protected Sub btnDescargar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        'Try
        '    Dim button As HtmlButton = DirectCast(sender, HtmlButton)
        '    Dim com As String = button.Attributes("codigo_com")
        '    Dim idArchivo As Long
        '    idArchivo = CLng(button.Attributes("idArchivo"))
        '    If idArchivo = 0 Then Throw New Exception("Archivo de resolución no disponible")
        '    Call mt_DescargarArchivo(com)
        'Catch ex As Exception
        '    Call mt_ShowMessage(ex.Message.Replace("'", ""), MessageType.Info)
        'End Try
    End Sub

    'Protected Sub btnLimpiar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnLimpiar.Click
    'Try
    '    Call mt_MostrarDetalle(Me.ddlCarreraProf.SelectedValue)
    'Catch ex As Exception
    '    Throw ex
    'End Try
    'End Sub

    'Protected Sub grwComite_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles grwComite.RowEditing
    'grwComite.EditIndex = e.NewEditIndex
    'Call BindGrid()
    'End Sub

    'Protected Sub grwComite_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grwComite.RowDataBound
    'If e.Row.RowType = DataControlRowType.DataRow Then
    '    Dim ddlRol As DropDownList = CType(e.Row.FindControl("ddlRol"), DropDownList)
    '    Dim ddlDocente As DropDownList = CType(e.Row.FindControl("ddlDocente"), DropDownList)

    '    If Not ddlRol Is Nothing Then
    '        ddlRol.SelectedValue = grwComite.DataKeys(e.Row.RowIndex).Values(1).ToString()
    '    End If

    '    If Not ddlDocente Is Nothing Then
    '        ddlDocente.DataSource = fc_GetDocentes()
    '        ddlDocente.DataValueField = "codigo_Per"
    '        ddlDocente.DataTextField = "docente"
    '        ddlDocente.DataBind()

    '        'Agregar fila en blanco
    '        ddlDocente.Items.Insert(0, New ListItem("[-- Seleccione un Docente --]", ""))
    '        ddlDocente.SelectedValue = grwComite.DataKeys(e.Row.RowIndex).Values(2).ToString()
    '    End If
    'End If

    'If e.Row.RowType = DataControlRowType.Footer Then
    '    Dim ddlDocente As DropDownList = CType(e.Row.FindControl("ddlNewDocente"), DropDownList)

    '    If ddlDocente IsNot Nothing Then
    '        ddlDocente.DataSource = fc_GetDocentes()
    '        ddlDocente.DataValueField = "codigo_Per"
    '        ddlDocente.DataTextField = "docente"
    '        ddlDocente.DataBind()

    '        'Agregar fila en blanco
    '        ddlDocente.Items.Insert(0, New ListItem("[-- Seleccione un Docente --]", ""))
    '    End If
    'End If
    'End Sub

    Protected Sub OnUpdate(ByVal sender As Object, ByVal e As EventArgs)
        'Try
        '    Dim dt As Data.DataTable = TryCast(ViewState("dt"), Data.DataTable)
        '    Dim row As GridViewRow = TryCast((TryCast(sender, LinkButton)).NamingContainer, GridViewRow)
        '    Dim ddlDocente As DropDownList = CType(grwComite.Rows(row.RowIndex).FindControl("ddlDocente"), DropDownList)
        '    Dim ddlRol As DropDownList = CType(grwComite.Rows(row.RowIndex).FindControl("ddlRol"), DropDownList)

        '    Dim valid As Generic.Dictionary(Of String, String) = fc_ValidarMiembros(ddlRol.SelectedItem.Text, ddlDocente.SelectedItem.Text)

        '    If valid.Item("rpta") = 1 Then
        '        dt.Rows(row.RowIndex)("codigo_per") = ddlDocente.SelectedValue
        '        dt.Rows(row.RowIndex)("nombre_per") = ddlDocente.SelectedItem.Text
        '        dt.Rows(row.RowIndex)("rol_mie") = ddlRol.SelectedItem.Text
        '        ViewState("dt") = dt
        '        grwComite.EditIndex = -1
        '        Call BindGrid()
        '    Else
        '        Call mt_ShowMessage(valid.Item("msg"), MessageType.Info, True)
        '    End If
        'Catch ex As Exception
        '    Call mt_ShowMessage(ex.Message.Replace("'", ""), MessageType.Info, True)
        'End Try
    End Sub

    Protected Sub OnCancel(ByVal sender As Object, ByVal e As EventArgs)
        'grwComite.EditIndex = -1
        'Call BindGrid()
    End Sub

    Protected Sub OnDelete(ByVal sender As Object, ByVal e As EventArgs)
        'Try
        '    Dim valor As String = "1"
        '    Dim rpta As String = ""
        '    Dim dt As Data.DataTable = TryCast(ViewState("dt"), Data.DataTable)
        '    Dim row As GridViewRow = TryCast((TryCast(sender, LinkButton)).NamingContainer, GridViewRow)
        '    Dim codigo_mie As String = grwComite.DataKeys(row.RowIndex).Item("codigo_mie").ToString

        '    If Not String.IsNullOrEmpty(codigo_mie) AndAlso Not codigo_mie.Equals("0") Then
        '        Dim dtRpta As New Data.DataTable
        '        Dim codigo_com As Object
        '        codigo_com = IIf(String.IsNullOrEmpty(Session("codigo_com")), "-1", Session("codigo_com"))

        '        C.AbrirConexion()
        '        dtRpta = C.TraerDataTable("COM_VerificarComiteCurricularMiembros", codigo_com)
        '        C.CerrarConexion()

        '        valor = dtRpta.Rows(0).Item(0).ToString
        '        rpta = dtRpta.Rows(0).Item(1).ToString
        '        dtRpta.Dispose()

        '        If valor.Equals("1") Then
        '            Dim dtElim As Data.DataTable = TryCast(ViewState("dtElim"), Data.DataTable)
        '            dtElim.Rows.Add(codigo_mie, codigo_com)
        '            ViewState("dtElim") = dtElim
        '        Else
        '            Call mt_ShowMessage(rpta, MessageType.Info, True)
        '            Return
        '        End If
        '    End If

        '    If valor.Equals("1") Then
        '        dt.Rows.RemoveAt(row.RowIndex)
        '        ViewState("dt") = dt
        '        grwComite.EditIndex = -1
        '        Call BindGrid()
        '    End If
        'Catch ex As Exception
        '    Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Info, True)
        'End Try
    End Sub

    Protected Sub OnNew(ByVal sender As Object, ByVal e As EventArgs)
        'Try
        '    Dim dt As Data.DataTable = TryCast(ViewState("dt"), Data.DataTable)
        '    Dim ddlDocente As DropDownList = CType(grwComite.FooterRow.FindControl("ddlNewDocente"), DropDownList)
        '    Dim ddlRol As DropDownList = CType(grwComite.FooterRow.FindControl("ddlNewRol"), DropDownList)

        '    Dim valid As Generic.Dictionary(Of String, String) = fc_ValidarMiembros(ddlRol.SelectedItem.Text, ddlDocente.SelectedItem.Text)
        '    Dim cod_com As Object = IIf(String.IsNullOrEmpty(Session("codigo_com")), 0, Session("codigo_com"))

        '    If valid.Item("rpta") = 1 Then
        '        If String.IsNullOrEmpty(dt.Rows(0).Item(1).ToString) Then
        '            dt.Rows.RemoveAt(0)
        '        End If

        '        dt.Rows.Add(cod_com, ddlDocente.SelectedValue.ToString, ddlDocente.SelectedItem.Text, 0, ddlRol.SelectedItem.Text, 1)
        '        ViewState("dt") = dt
        '        grwComite.EditIndex = -1
        '        Call BindGrid()
        '    Else
        '        Call mt_ShowMessage(valid.Item("msg"), MessageType.Info, True)
        '    End If
        'Catch ex As Exception
        '    Call mt_ShowMessage(ex.Message.Replace("'", ""), MessageType.Info, True)
        'End Try
    End Sub

#End Region

#Region "Métodos"

    Protected Sub mt_ShowMessage(ByVal Message As String, ByVal type As MessageType, Optional ByVal modal As Boolean = False)
  
        Page.RegisterStartupScript("Mensaje", "<script>ShowMessage('" & Message & "','" & type.ToString & "');</script>")

    End Sub

    Private Sub mt_RegistrarDetalleCombinacion()
        Try
            'Response.Write("ID_COMB: " & Session("ID_COMB") & "<br>")
            'Response.Write("ID_COMBDET: " & Session("ID_COMBDET") & "<br>")

            If mt_fnValidarRegistroDetComb() Then

                ' Dim sw As Boolean = True
                Dim script As String = ""


                'dt = obj.TraerDataTable("Escuela_combListar", "1", Me.ddlEscuela.SelectedValue, Me.ddlCiclo.SelectedValue)
                Dim ope As String = ""

                If Me.hcombdet.value = "" Or Me.hcombdet.value = "0" Then
                    ope = "I"
                    script = "Combinación registrada con éxito"
                Else
                    ope = "A"
                    script = "Combinación modificada con éxito"
                End If

                C.AbrirConexion()
                C.Ejecutar("Escuela_detcombReg", ope, Me.hcombdet.value, Me.hcomb.value, Me.ddlCurso.SelectedValue, Me.ddlCombinacion.SelectedValue, CInt(Me.txtdetnumero.Text), CInt(Me.txtdetnumero.Text), Session("perlogin"), 1)
                C.CerrarConexion()


                If Me.hcombdet.value <> "" Or Me.hcombdet.value > 0 Then
                    Me.btnDetCerrar.Visible = False
                    Me.divInfoEdit.Visible = False
                    Me.txtinfoeditar.Text = ""
                End If

                ' ScriptManager.RegisterStartupScript(Me, GetType(Page), "alert", script, True)

                'VisibleDiv(True)
                mt_consultarDetCombinacion()
                mt_LimpiarForm("1")

                'fnNotificacion(script)
                Call mt_ShowMessage(script, MessageType.Success)
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
            Response.Write(ex.StackTrace)

            Call mt_ShowMessage(ex.Message.Replace("'", ""), MessageType.Error)
        End Try
    End Sub

    Private Sub mt_CargarSemestre()
        Try
            Dim dt As New Data.DataTable("data")

            C.AbrirConexion()
            dt = C.TraerDataTable("ConsultarCicloAcademico", "DA", "")
            C.CerrarConexion()

            ddlCiclo.DataSource = dt
            ddlCiclo.DataTextField = "descripcion_Cac"
            ddlCiclo.DataValueField = "codigo_Cac"
            ddlCiclo.DataBind()

            ddlCicloReg.DataSource = dt
            ddlCicloReg.DataTextField = "descripcion_Cac"
            ddlCicloReg.DataValueField = "codigo_Cac"
            ddlCicloReg.DataBind()

            dt.Dispose()
        Catch ex As Exception

            Call mt_ShowMessage(ex.Message.Replace("'", ""), MessageType.Error)
        End Try
    End Sub

    Private Sub mt_CargarCarreras()
        Try
            Dim dt As New Data.DataTable("Data")
            C.AbrirConexion()

            'dt = C.TraerDataTable("COM_ListarCarreraProfesional", cod_user, cod_ctf)
            'dt = C.TraerDataTable("ConsultarEscuelaPorPersonal", "2", CInt(Session("id_per").ToString()), 2)
            dt = C.TraerDataTable("ConsultarEscuelaPorPersonalV2", "3", CInt(Session("id_per").ToString()), 2, CInt(Me.Request.QueryString("ctf").ToString))
            Me.ddlEscuela.DataSource = dt
            Me.ddlEscuela.DataValueField = "codigo_Cpf"
            Me.ddlEscuela.DataTextField = "nombre_Cpf"
            Me.ddlEscuela.DataBind()

            dt.rows(0).item("nombre_Cpf") = "[ --- Seleccione Escuela --- ]"

            Me.ddlEscuelaReg.DataSource = dt
            Me.ddlEscuelaReg.DataValueField = "codigo_Cpf"
            Me.ddlEscuelaReg.DataTextField = "nombre_Cpf"
            Me.ddlEscuelaReg.DataBind()



            dt.Dispose()
            C.CerrarConexion()
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", ""), MessageType.Error)
        End Try
    End Sub

    Private Sub mt_MostrarDetalle()
        Try
            Dim codigo_cpf As Integer
            Dim dt As New Data.DataTable("Data")
            C.AbrirConexion()

            codigo_cpf = IIf(String.IsNullOrEmpty(codigo_cpf), "", codigo_cpf)
            Session("codigo_cpf") = codigo_cpf

            dt = C.TraerDataTable("COM_ListarComiteCurricular", codigo_cpf)

            Me.grwResultado.DataSource = dt
            Me.grwResultado.DataBind()
            dt.Dispose()
            C.CerrarConexion()
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", ""), MessageType.Error)
        End Try
    End Sub

    Private Sub mt_CargarDatos()
        Dim dt As New Data.DataTable("data")
        Dim cod_cpf As Integer = Me.ddlEscuela.selectedvalue
        Dim cod_cac As Integer = Me.ddlCiclo.selectedvalue

        Try
            C.AbrirConexion()
            'dt = C.TraerDataTable("Escuela_combListar", "1", 0, cod_cpf, cod_cac)
            dt = C.TraerDataTable("Escuela_combListarV2", "1", 0, cod_cpf, cod_cac, CInt(Session("id_per")), 2)
            Me.grwResultado.DataSource = dt
            Me.grwResultado.DataBind()
            C.CerrarConexion()
            'ViewState("dt") = dt
            'Call BindGrid()
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub
    Private Sub mt_consultarDetCombinacion()


        If Me.hcomb.value = "0" Or Me.hcomb.value = "" Then
            Dim script As String = ""
            Call mt_ShowMessage("seleccione nuevamente Escuela profesional", MessageType.Warning)
        Else
            Dim dt As New Data.DataTable
            C.AbrirConexion()
            dt = C.TraerDataTable("Escuela_combDetListar", "1", 0, CInt(Me.hcomb.value), 0, 0)
            'Session("dtDetalle") = dtDetalle
           
            C.CerrarConexion()
            Me.gDataCombDet.DataSource = dt
            Me.gDataCombDet.DataBind()
            mt_ValidaGridDetalle()
            ' gDataCombDet.Columns(7).Visible = True
            'CalcularVacantesDisponibles()
        End If
    End Sub
    Private Sub BindGrid()
        'Try
        '    Dim dt As Data.DataTable = TryCast(ViewState("dt"), Data.DataTable)

        '    If dt.Rows.Count > 0 Then
        '        grwComite.DataSource = dt
        '        grwComite.DataBind()
        '    Else
        '        dt.Rows.Add(dt.NewRow())
        '        grwComite.DataSource = dt
        '        grwComite.DataBind()

        '        grwComite.Rows(0).Cells.Clear()

        '        'Dim totalColumns As Integer = grwComite.Rows(0).Cells.Count
        '        'grwComite.Rows(0).Cells.Clear()
        '        'grwComite.Rows(0).Cells.Add(New TableCell())
        '        'grwComite.Rows(0).Cells(0).ColumnSpan = totalColumns
        '        'grwComite.Rows(0).Cells(0).Style.Add("text-align", "center")
        '        'grwComite.Rows(0).Cells(0).Text = "No se ha registrado participantes"
        '    End If

        '    udpComite.Update()

        '    Me.divAlertModal.Visible = False
        '    Me.lblMensaje.InnerText = ""
        '    updMensaje.Update()
        'Catch ex As Exception
        '    Call mt_ShowMessage(ex.Message.Replace("'", ""), MessageType.Error, True)
        'End Try
    End Sub

    'Private Sub mt_LimpiarFormulario(ByVal opc As String)
    '    If opc = "0" Then
    '        ddlEscuelareg.SelectedValue = 0
    '        ddlCicloreg.SelectedValue = 0
    '        txtnrocomb.text = ""
    '        chkActivo.checked = True
    '        hf.Value = "0"
    '        hcomb.value = "0"
    '    ElseIf opc = "1" Then
    '        Me.hcombdet.value = "0"

    '        Me.txtdetnumero.Text = ""
    '        Me.btnDetCerrar.Visible = False
    '        Me.divInfoEdit.Visible = False
    '        Me.txtinfoeditar.Text = ""

    '    End If
    'End Sub

    Private Sub mt_LlenarFormulario()
        'Try
        '    Dim dt As New Data.DataTable("data")
        '    Dim cod_com As Object = IIf(String.IsNullOrEmpty(Session("codigo_com")), "-1", Session("codigo_com"))

        '    C.AbrirConexion()
        '    dt = C.TraerDataTable("COM_ObtenerDatosComiteCurricular", cod_com, idTabla)
        '    C.CerrarConexion()

        '    spnFile.InnerText = "No se eligió resolución"

        '    If dt.Rows.Count > 0 Then
        '        txtNombre.Text = dt.Rows(0).Item("nombre_com").ToString.Trim
        '        txtIniAprobacion.Text = dt.Rows(0).Item("fechaAprob_com").ToString.Trim
        '        txtFinAprobacion.Text = dt.Rows(0).Item("fechaTermino_com").ToString.Trim
        '        ddlSemestre.SelectedValue = dt.Rows(0).Item("codigo_cac").ToString.Trim
        '        txtNroDecreto.Text = dt.Rows(0).Item("nroDecreto_com").ToString.Trim
        '        spnFile.InnerText = dt.Rows(0).Item("archivo").ToString.Trim

        '        If spnFile.InnerText.Trim().Equals("No se eligió resolución") Then
        '            hf.Value = "0"
        '        Else
        '            hf.Value = "1"
        '        End If
        '    End If

        '    dt.Dispose()
        'Catch ex As Exception
        '    ClientScript.RegisterStartupScript(Me.GetType, "Pop", "<script>mostrarMensaje('" & ex.Message.Replace("'", "") & "', 'danger');</script>")
        'End Try
    End Sub

    'Este método me permite llamar manualmente al evento RowDataBound que vuelve a reenderizar los botones de acción
    Private Sub RefreshGrid()
        'For Each _Row As GridViewRow In grwResultado.Rows
        '    grwResultado_RowDataBound(grwResultado, New GridViewRowEventArgs(_Row))
        'Next
    End Sub

    Private Sub mt_LimpiarForm(ByVal opc As String)
        If opc = "0" Then
            Me.hcomb.value = "0"
            Me.ddlEscuelaReg.selectedIndex = 0
            Me.ddlCicloReg.selectedIndex = 0
            Me.txtnrocomb.text = ""
            Me.chkActivo.checked = True

        ElseIf opc = "1" Then
            Me.hcombdet.value = "0"

            Me.txtdetnumero.Text = ""
            Me.btnDetCerrar.Visible = False
            Me.divInfoEdit.Visible = False
            Me.txtinfoeditar.Text = ""


        End If
    End Sub

    Private Sub mt_MostrarDiv(ByVal opc As String)
        If opc = "0" Then
            divListarCombinacion.visible = True
            divRegistrarCombinacion.visible = False
            divListarCombinacionDet.visible = False
        ElseIf opc = "1" Then
            divListarCombinacion.visible = False
            divRegistrarCombinacion.visible = True
            divListarCombinacionDet.visible = False
        ElseIf opc = "2" Then
            divListarCombinacion.visible = False
            divListarCombinacionDet.visible = True
            divRegistrarCombinacion.visible = False
        ElseIf opc = "3" Then
            divListarCombinacion.visible = True
            divListarCombinacionDet.visible = False
            divRegistrarCombinacion.visible = False
        End If

    End Sub
#End Region

#Region "Funciones"

    Private Function fc_GetDocentes() As Data.DataTable
        'Dim dt As New Data.DataTable("data")

        'C.AbrirConexion()
        'dt = C.TraerDataTable("COM_ListarDocentes")
        'C.CerrarConexion()

        'Return dt
    End Function

    Private Sub mt_LlenarComboCombinacion(ByVal N As Integer)

        Dim dt As Data.DataTable
        dt = New Data.DataTable("Tabla")

        dt.Columns.Add("Codigo")
        dt.Columns.Add("Descripcion")


        For i As Integer = 1 To N
            Dim dr As Data.DataRow
            dr = dt.NewRow()
            dr("Codigo") = i
            dr("Descripcion") = i
            dt.Rows.Add(dr)
        Next
        'Dim dr As Data.DataRow

        'dr = dt.NewRow()
        'dr("Codigo") = "M"
        'dr(1) = "Masculino"
        'dt.Rows.Add(dr)

        'dr = dt.NewRow()
        'dr(0) = "F"
        'dr(1) = "Femenino"
        'dt.Rows.Add(dr)

        Me.ddlCombinacion.DataSource = dt
        Me.ddlCombinacion.DataValueField = "Codigo"
        Me.ddlCombinacion.DataTextField = "Descripcion"
        Me.ddlCombinacion.DataBind()
    End Sub

    Private Function fc_ValidarRegistroCombinacion() As Boolean
        Try
            Dim script As String = ""
            If ddlEscuelaReg.SelectedValue = "0" Then
                Call mt_ShowMessage("Seleccione Escuela Profesional", MessageType.Warning)
                ddlEscuelaReg.focus()
                Return False
            End If
            If ddlCicloReg.SelectedValue = "-1" Then
                Call mt_ShowMessage("Seleccione Ciclo Academico", MessageType.Warning)
                ddlCicloReg.focus()
                Return False
            End If
            If txtnrocomb.Text = "0" Or txtnrocomb.Text = "" Then
                Call mt_ShowMessage("Ingrese numero de combinación", MessageType.Warning)
                txtnrocomb.focus()
                Return False
            End If

            If hcomb.value = "0" Or hcomb.value = "" Then

                Dim dt As New Data.DataTable
               
                C.AbrirConexion()
                dt = C.TraerDataTable("Escuela_combListar", "2", 0, Me.ddlEscuelaReg.SelectedValue, Me.ddlCicloReg.SelectedValue)
                C.CerrarConexion()

                If dt.Rows.Count > 0 Then
                    Call mt_ShowMessage("Ya se encuentra registrado este grupo", MessageType.Warning)
                    Return False
                End If
            End If
            Return True
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", ""), MessageType.Error)
        End Try

    End Function
    Private Sub mt_ValidaGridDetalle()
        For i As Integer = 0 To gDataCombDet.Rows.Count - 1
            If gDataCombDet.Rows(i).Cells(9).Text = "0" Then
                gDataCombDet.Rows(i).Cells(10).Text = ""
                gDataCombDet.Rows(i).Cells(11).Text = ""
            End If


        Next
        gDataCombDet.Columns(8).Visible = False
    End Sub
    Private Function mt_fnValidarRegistroDetComb() As Boolean
        Try
            Dim script As String = ""

            If txtdetnumero.Text = "0" Or txtdetnumero.Text = "" Then

                script = "Ingrese numero de ingresantes para combinación"
                ' ScriptManager.RegisterStartupScript(Me, GetType(Page), "alert", script, True)
                'fnNotificacion(script)
                Call mt_ShowMessage(script, MessageType.Warning)
                Return False
            End If

            Dim dt As New Data.DataTable
            Dim dtAgrupado As New Data.DataTable

            If Me.hcombdet.value = "0" Or Me.hcombdet.value = "" Then
                C.AbrirConexion()
                dt = C.TraerDataTable("Escuela_combDetListar", "2", 0, Me.hcomb.value, CInt(ddlCurso.SelectedValue), CInt(ddlCombinacion.SelectedValue))
                C.CerrarConexion()
                If dt.Rows.Count > 0 Then
                    script = "Ya se encuentra registrado el curso [" & dt.Rows(0).Item("nombre_Cur").ToString() & "] en la combinacion [" & dt.Rows(0).Item("nrocombinacion").ToString() & "]"
                    'fnNotificacion(script)
                    Call mt_ShowMessage(script, MessageType.Warning)

                    Return False
                End If
            End If

            C.AbrirConexion()
            dt = C.TraerDataTable("Escuela_combCursoProgListar", "2", CInt(ddlCurso.SelectedValue), CInt(ddlPlanEstudio.SelectedValue), CInt(ddlCiclo.SelectedValue))
            C.CerrarConexion()

            Dim NRO_VACANTES_DISP As Integer = 0
            Dim NRO_VACANTES_AGRUP As Integer = 0

            'Response.Write("ddlCurso.SelectedItem.Text: " & ddlCurso.SelectedItem.Text & "<br>")
            'Response.Write("contains: " & ddlCurso.SelectedItem.Text.contains("Agrupado") & "p<br>")

            If CBool(ddlCurso.SelectedItem.Text.contains("Agrupado")) Then
                C.AbrirConexion()
                dtAgrupado = C.TraerDataTable("Escuela_combDetListar", "5", 0, CInt(Me.hcomb.value), CInt(ddlCurso.SelectedValue), 0)
                C.CerrarConexion()

                If dtAgrupado.rows.count > 0 Then

                    NRO_VACANTES_AGRUP = dtAgrupado.rows(0).item("vacantes_agrupado")
                End If
            End If

            ' Response.Write("NRO_VACANTES_AGRUP: " & NRO_VACANTES_AGRUP & "<br>")

            If dt.Rows.Count > 0 AndAlso (CInt(Me.txtdetnumero.Text) > CInt(dt.Rows(0).Item("vacantes"))) Then
                script = "El Numero de ingresantes supera las " & dt.Rows(0).Item("vacantes").ToString() & " vacantes"
                'fnNotificacion(script)
                Call mt_ShowMessage(script, MessageType.Warning)

                Return False
            End If

            NRO_VACANTES_DISP = dt.Rows(0).Item("vacantes")

            If Me.hcomb.value = "0" Or Me.hcomb.value = "" Then
                script = "Vuelva a seleccionar escuela profesional, haga click en [<- Regresar]"
                'ScriptManager.RegisterStartupScript(Me, GetType(Page), "alert", script, True)
                'fnNotificacion(script)
                Call mt_ShowMessage(script, MessageType.Warning)
                Return False
            End If

            'Response.Write("hcombdet: " & Me.hcombdet.value & "<br>")

            If Me.hcombdet.value = "0" Or Me.hcombdet.value = "" Then
                Dim total_vac_comb As Integer = fc_Vacantes("TOTAL")

                Dim txting As Integer = CInt(Me.txtdetnumero.Text)
                Dim vacantes_disp As Integer = 0 ' NRO_VACANTES_DISP - total_vac_comb - txting

                vacantes_disp = total_vac_comb + txting + NRO_VACANTES_AGRUP
                vacantes_disp = NRO_VACANTES_DISP - vacantes_disp
                'vacantes_disp = vacantes_disp - txting

                'Response.Write("total_vac_comb: " & total_vac_comb & "<br>")
                'Response.Write("txting: " & txting & "<br>")
                'Response.Write("<br>1 NRO_VACANTES_DISP: " & NRO_VACANTES_DISP & "<br>")
                'Response.Write("vacantes_disp: " & vacantes_disp & "<br>")

                If vacantes_disp < 0 Then
                    'Dim difvacante As String = NRO_VACANTES_DISP - txting
                    'script = "El Numero de ingresantes supera las " & NRO_VACANTES_DISP.ToString() & "  disponibles"
                    'fnNotificacion(script)
                    script = txting.ToString & " excede las vacantes"
                    If NRO_VACANTES_AGRUP > 0 Then
                        script = script & ", ya se han asignado <b>" & NRO_VACANTES_AGRUP & "</b> en otras escuelas"
                    End If
                    Call mt_ShowMessage(script, MessageType.Warning)
                    Return False
                End If

            Else
                'Dim total_vac_comb_dif As Integer = 0
                Dim total_vac_comb As Integer = fc_Vacantes("TOTAL")
                Dim vacante_actual As Integer = fc_Vacantes("TOTALEDIT")
                Dim txting As Integer = CInt(Me.txtdetnumero.Text)
                Dim vacantes_disp As Integer = 0
                Dim total_vac_comb_dif As Integer = 0 ' total_vac_comb - vacante_actual '- txting

                'vacantes_disp = NRO_VACANTES_DISP - (total_vac_comb + txting) - vacante_actual
                'total_vac_comb_dif = NRO_VACANTES_DISP - vacante_actual

                vacantes_disp = (total_vac_comb + NRO_VACANTES_AGRUP) - vacante_actual
                vacantes_disp = NRO_VACANTES_DISP - vacantes_disp
                vacantes_disp = vacantes_disp - txting

                'Response.Write("<br>NRO_VACANTES_DISP: " & NRO_VACANTES_DISP & "<br>")
                'Response.Write("total_vac_comb: " & total_vac_comb & "<br>")
                'Response.Write("vacante_actual: " & vacante_actual & "<br>")
                'Response.Write("txting: " & txting & "<br>")
                'Response.Write("total_vac_comb_dif: " & total_vac_comb_dif & "<br>")
                'Response.Write("vacantes_disp: " & vacantes_disp & "<br>")

                If vacantes_disp < 0 Then
                    'script = "El Numero de ingresantes supera las " & (NRO_VACANTES_DISP - total_ing_dt + total_ing).ToString() & "  disponibles"
                    script = txting.ToString & " excede las vacantes "
                    If NRO_VACANTES_AGRUP > 0 Then
                        script = script & ", ya se han asignado <b>" & NRO_VACANTES_AGRUP & "</b> en otras escuelas"
                    End If
                    Call mt_ShowMessage(script, MessageType.Warning)
                    Return False
                End If


            End If

            Return True
        Catch ex As Exception
            Response.Write(ex.StackTrace)
            Call mt_ShowMessage(ex.Message.Replace("'", ""), MessageType.Error)
        End Try

    End Function

    Private Function fc_Vacantes(ByVal opc As String) As Integer
        Dim vacantes As Integer = 0
        Dim dt As New data.datatable
        Dim i As Integer = 0
        Try
            If opc = "TOTAL" Then

                C.AbrirConexion()
                dt = C.TraerDataTable("Escuela_combDetListar", "3", 0, CInt(Me.hcomb.value), CInt(ddlCurso.selectedvalue), 0)
                C.CerrarConexion()

                If dt.rows.count > 0 Then
                    vacantes = dt.rows(0).item("vacantes_comb")
                End If

            ElseIf opc = "TOTALEDIT" Then
                C.AbrirConexion()
                dt = C.TraerDataTable("Escuela_combDetListar", "4", CInt(Me.hcombdet.value), 0, 0, 0)
                C.CerrarConexion()

                If dt.rows.count > 0 Then
                    vacantes = dt.rows(0).item("vacantes_comb")
                End If
            End If

            Return vacantes
        Catch ex As Exception
            Return vacantes
        End Try

    End Function


    Private Function fc_Validar() As Generic.Dictionary(Of String, String)
        Dim valid As New Generic.Dictionary(Of String, String)
        Dim err As Boolean = False
        'valid.Add("rpta", 1)
        'valid.Add("msg", "")
        'valid.Add("control", "")

        'If Not err And String.IsNullOrEmpty(Request("txtNombre")) Then
        '    If Not err Then
        '        valid.Item("rpta") = 0
        '        valid.Item("msg") = "Debe ingresar un nombre del comité"
        '        valid.Item("control") = "txtNombre"
        '        err = True
        '        ScriptManager.GetCurrent(Me.Page).SetFocus(Me.txtNombre)
        '    End If
        '    txtNombre.Attributes.Item("data-error") = "true"
        'Else
        '    txtNombre.Attributes.Item("data-error") = "false"
        'End If

        'If Not err And String.IsNullOrEmpty(Request("txtIniAprobacion")) Then
        '    If Not err Then
        '        valid.Item("rpta") = 0
        '        valid.Item("msg") = "Debe ingresar la fecha de inicio de la aprobación"
        '        valid.Item("control") = "txtIniAprobacion"
        '        err = True
        '        ScriptManager.GetCurrent(Me.Page).SetFocus(Me.txtIniAprobacion)
        '    End If
        '    txtIniAprobacion.Attributes.Item("data-error") = "true"
        'Else
        '    txtIniAprobacion.Attributes.Item("data-error") = "false"
        'End If

        'If Not err And String.IsNullOrEmpty(Request("txtFinAprobacion")) Then
        '    If Not err Then
        '        valid.Item("rpta") = 0
        '        valid.Item("msg") = "Debe ingresar la fecha de término de la aprobación"
        '        valid.Item("control") = "txtFinAprobacion"
        '        err = True
        '        ScriptManager.GetCurrent(Me.Page).SetFocus(Me.txtFinAprobacion)
        '    End If
        '    txtFinAprobacion.Attributes.Item("data-error") = "true"
        'Else
        '    txtFinAprobacion.Attributes.Item("data-error") = "false"
        'End If

        'If Not err Then
        '    Try
        '        Dim fecha As Date = CDate(txtIniAprobacion.Text)
        '    Catch ex As Exception
        '        valid.Item("rpta") = 0
        '        valid.Item("msg") = "La fecha de inicio de aprobación no tiene el formato de fecha correcto"
        '        valid.Item("control") = "txtIniAprobacion"
        '        err = True
        '        ScriptManager.GetCurrent(Me.Page).SetFocus(Me.txtIniAprobacion)
        '        txtIniAprobacion.Attributes.Item("data-error") = "true"
        '    End Try
        'End If

        'If Not err Then
        '    Try
        '        Dim fecha As Date = CDate(txtFinAprobacion.Text)
        '    Catch ex As Exception
        '        valid.Item("rpta") = 0
        '        valid.Item("msg") = "La fecha de termino de aprobación no tiene el formato de fecha correcto"
        '        valid.Item("control") = "txtFinAprobacion"
        '        err = True
        '        ScriptManager.GetCurrent(Me.Page).SetFocus(Me.txtFinAprobacion)
        '        txtFinAprobacion.Attributes.Item("data-error") = "true"
        '    End Try
        'End If

        'If Not err Then
        '    Dim desde As Date = CDate(txtIniAprobacion.Text)
        '    Dim hasta As Date = CDate(txtFinAprobacion.Text)
        '    If desde > hasta Then
        '        valid.Item("rpta") = 0
        '        valid.Item("msg") = "La fecha de término de la aprobación no puede ser menor a la fecha de inicio"
        '        valid.Item("control") = "txtFinAprobacion"
        '        err = True
        '        ScriptManager.GetCurrent(Me.Page).SetFocus(Me.txtFinAprobacion)
        '        txtFinAprobacion.Attributes.Item("data-error") = "true"
        '    Else
        '        txtFinAprobacion.Attributes.Item("data-error") = "false"
        '    End If
        'End If

        'If Not err And ddlSemestre.SelectedValue = "-1" Then
        '    If Not err Then
        '        valid.Item("rpta") = 0
        '        valid.Item("msg") = "Seleccione el semestre de creación"
        '        valid.Item("control") = "ddlSemestre"
        '        err = True
        '        ScriptManager.GetCurrent(Me.Page).SetFocus(Me.ddlSemestre)
        '    End If
        '    ddlSemestre.Attributes.Item("data-error") = "true"
        'Else
        '    ddlSemestre.Attributes.Item("data-error") = "false"
        'End If

        'If Not err And String.IsNullOrEmpty(Request("txtNroDecreto")) Then
        '    If Not err Then
        '        valid.Item("rpta") = 0
        '        valid.Item("msg") = "Debe ingresar el número de resolución"
        '        valid.Item("control") = "txtNroDecreto"
        '        err = True
        '        ScriptManager.GetCurrent(Me.Page).SetFocus(Me.txtNroDecreto)
        '    End If
        '    txtNroDecreto.Attributes.Item("data-error") = "true"
        'Else
        '    txtNroDecreto.Attributes.Item("data-error") = "false"
        'End If

        'Dim dtDet As Data.DataTable = TryCast(ViewState("dt"), Data.DataTable)
        'If Not err And dtDet.Rows.Count <= 1 Then
        '    Dim cod_mie As String = dtDet.Rows(0).Item(0).ToString
        '    If String.IsNullOrEmpty(cod_mie) Then
        '        valid.Item("rpta") = 0
        '        valid.Item("msg") = "Debe ingresar a los miembros del comité"
        '        valid.Item("control") = "grwComite"
        '        err = True
        '    End If
        '    grwComite.Attributes.Item("data-error") = "true"
        'Else
        '    grwComite.Attributes.Item("data-error") = "false"
        'End If

        ''If Not err AndAlso (String.IsNullOrEmpty(spnFile.InnerText) Or spnFile.InnerText.Trim().Equals("No se eligió resolución")) Then
        'If Not err AndAlso hf.Value = "0" Then
        '    If Not err Then
        '        valid.Item("rpta") = 0
        '        valid.Item("msg") = "Debe seleccionar el archivo de resolución del comité"
        '        valid.Item("control") = "fuArchivo"
        '        err = True
        '    End If
        '    fuArchivo.Attributes.Item("data-error") = "true"
        'Else
        '    fuArchivo.Attributes.Item("data-error") = "false"
        'End If

        Return valid
    End Function

    Private Function fc_ValidarMiembros(ByVal rol As String, ByVal docente As String) As Generic.Dictionary(Of String, String)
        Dim valid As New Generic.Dictionary(Of String, String)
        'valid.Add("rpta", 1)
        'valid.Add("msg", "")
        'valid.Add("control", "")

        'If String.IsNullOrEmpty(docente) Or docente.StartsWith("[--") Then
        '    valid.Item("rpta") = 0
        '    valid.Item("msg") = "Debe seleccionar un Docente"
        '    valid.Item("control") = "grwComite"

        'ElseIf String.IsNullOrEmpty(rol) Or rol.StartsWith("[--") Then
        '    valid.Item("rpta") = 0
        '    valid.Item("msg") = "Debe seleccionar un Rol"
        '    valid.Item("control") = "grwComite"

        'Else
        '    For i As Integer = 0 To grwComite.Rows.Count - 1
        '        Dim lblRol As Label
        '        lblRol = CType(grwComite.Rows(i).FindControl("lblRol"), Label)

        '        Dim lblDocente As Label
        '        lblDocente = CType(grwComite.Rows(i).FindControl("lblDocente"), Label)

        '        If lblDocente IsNot Nothing Then
        '            If lblDocente.Text.Contains(docente) Then
        '                valid.Item("rpta") = 0
        '                valid.Item("msg") = "No se puede registrar al mismo docente 2 veces en el mismo comité"
        '                valid.Item("control") = "grwComite"

        '                Exit For
        '            End If
        '        End If

        '        If rol.Equals("PRESIDENTE") Or rol.Equals("SECRETARIO") Then
        '            If lblRol IsNot Nothing Then
        '                If lblRol.Text.Contains(rol) Then
        '                    valid.Item("rpta") = 0
        '                    valid.Item("msg") = "Ya existe un miembro del tipo " & rol
        '                    valid.Item("control") = "grwComite"

        '                    Exit For
        '                End If
        '            End If
        '        End If
        '    Next
        'End If

        Return valid
    End Function

#End Region

End Class
