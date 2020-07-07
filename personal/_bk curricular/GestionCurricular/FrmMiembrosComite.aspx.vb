﻿
Partial Class GestionCurricular_FrmMiembrosComite
    Inherits System.Web.UI.Page

#Region "Variables"

    Private C As ClsConectarDatos
    Private nuevo As Boolean = False
    Private cod_user As Integer '= 684
    Private cod_ctf As Integer '= 1
    Private ruta As String = ConfigurationManager.AppSettings("SharedFiles") '"http://localhost/campusvirtual/ArchivosCompartidos/SharedFiles.asmx"
    Private idTabla As Integer = 17 ' Desarrollo
    
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
                Dim dt As Data.DataTable = New Data.DataTable()
                Dim dtElim As Data.DataTable = New Data.DataTable()

                dt.Columns.Add("codigo_com")
                dt.Columns.Add("codigo_per")
                dt.Columns.Add("nombre_per")
                dt.Columns.Add("codigo_mie")
                dt.Columns.Add("rol_mie")
                dt.Columns.Add("vigente_mie")
                ViewState("dt") = dt

                dtElim.Columns.Add("codigo_mie")
                dtElim.Columns.Add("codigo_com")
                ViewState("dtElim") = dtElim

                btnFuArchivo.Attributes.Add("onClick", "document.getElementById('" + fuArchivo.ClientID + "').click();")

                Call BindGrid()
                Call mt_CargarCarreras()
                Call mt_CargarSemestre()
                Call mt_LimpiarFormulario()
                Call mt_MostrarDetalle(ddlCarreraProf.SelectedValue)
            Else
                Call RefreshGrid()
            End If
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", ""), MessageType.Error)
        End Try
    End Sub

    Protected Sub ddlCarreraProf_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlCarreraProf.SelectedIndexChanged
        Call mt_MostrarDetalle(Me.ddlCarreraProf.SelectedValue)
    End Sub

    Protected Sub grwResultado_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grwResultado.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim celda As TableCellCollection = e.Row.Cells
            Dim com As String = Me.grwResultado.DataKeys(e.Row.RowIndex).Values.Item("codigo_com")
            Dim nom As String = Me.grwResultado.DataKeys(e.Row.RowIndex).Values.Item("nombre_com")
            Dim cpf As String = Me.grwResultado.DataKeys(e.Row.RowIndex).Values.Item("codigo_cpf")
            Dim des As String = Me.grwResultado.DataKeys(e.Row.RowIndex).Values.Item("nombre_cpf")
            Dim tdc As String = Me.grwResultado.DataKeys(e.Row.RowIndex).Values.Item("bloqueado")
            Dim arc As String = Me.grwResultado.DataKeys(e.Row.RowIndex).Values.Item("idArchivo")
            Dim idx As Integer = e.Row.RowIndex + 1

            Dim btnEditar As New HtmlButton
            With btnEditar
                .ID = "btnEditar" & idx
                .Attributes.Add("type", "button")
                .Attributes.Add("codigo_com", com)
                .Attributes.Add("nombre_com", nom)
                .Attributes.Add("codigo_cpf", cpf)
                .Attributes.Add("nombre_cpf", des)

                If CInt(tdc) = 0 Then
                    .Attributes.Remove("disabled")
                    .Attributes.Add("class", "btn btn-primary btn-sm")
                    .Attributes.Add("title", "Editar Comité")
                    .InnerHtml = "<i class='fa fa-edit' title='Editar Comité'></i>"
                Else
                    .Attributes.Add("disabled", True)
                    .Attributes.Add("class", "btn btn-primary btn-sm")
                    .Attributes.Add("title", "La edición del comité está bloqueada")
                    .InnerHtml = "<i class='fa fa-edit' title='La edición del comité está bloqueada'></i>"
                End If

                AddHandler .ServerClick, AddressOf btnEditar_Click
            End With
            celda(3).Controls.Add(btnEditar)

            Dim btnDescargar As New HtmlButton
            With btnDescargar
                .ID = "btnDescargar" & idx
                .Attributes.Add("class", "btn btn-info btn-sm")
                .Attributes.Add("type", "button")
                .Attributes.Add("codigo_com", com)
                .Attributes.Add("idArchivo", arc)
                .Attributes.Add("title", "Descargar resolución")
                .InnerHtml = "<i class='fa fa-download' title='Descargar resolución'></i>"

                AddHandler .ServerClick, AddressOf btnDescargar_Click
            End With
            celda(4).Controls.Add(btnDescargar)
        End If
    End Sub

    Protected Sub btnCrear_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim cpf As String = Me.ddlCarreraProf.SelectedValue.ToString()
            Dim des As String = Me.ddlCarreraProf.SelectedItem.Text

            Session("codigo_com") = ""
            Session("codigo_cpf") = cpf

            ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script>openModal('nuevo', '" & des & "');</script>")
            Call mt_CargarDatos()

            Call ddlCarreraProf_SelectedIndexChanged(sender, e)
            Call mt_MostrarDetalle(cpf)
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", ""), MessageType.Error)
        End Try
    End Sub

    Protected Sub btnEditar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim button As HtmlButton = DirectCast(sender, HtmlButton)
            Dim com As String = button.Attributes("codigo_com")
            Dim nom As String = button.Attributes("nombre_com")
            Dim cpf As String = button.Attributes("codigo_cpf")
            Dim des As String = button.Attributes("nombre_cpf")

            Session("codigo_com") = com
            Session("codigo_cpf") = cpf

            ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script>openModal('editar','');</script>")
            Call mt_LlenarFormulario()
            Call mt_CargarDatos()

            Call ddlCarreraProf_SelectedIndexChanged(sender, e)
            Call mt_MostrarDetalle(cpf)
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", ""), MessageType.Error)
        End Try
    End Sub

    Protected Sub btnValidar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnValidar.ServerClick
        Try
            Dim valid As Generic.Dictionary(Of String, String) = fc_Validar()

            If valid.Item("rpta") = 1 Then
                Me.divAlertModal.Visible = False
                Me.validar.Value = "1"
                updMensaje.Update()
            Else
                ScriptManager.GetCurrent(Me.Page).SetFocus(Me.divAlertModal)
                ScriptManager.GetCurrent(Me.Page).SetFocus(Me.lblMensaje)

                Call mt_ShowMessage(valid.Item("msg"), MessageType.Info, True)
            End If
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", ""), MessageType.Error, True)
        End Try
    End Sub

    Protected Sub btnAceptar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAceptar.Click
        Try
            Dim dt As New Data.DataTable
            Dim flag As Boolean = False
            Dim cod_cpf As Object = IIf(String.IsNullOrEmpty(Session("codigo_cpf")), "", Session("codigo_cpf"))
            Dim cod_com As Object = IIf(String.IsNullOrEmpty(Session("codigo_com")), "", Session("codigo_com"))

            C.IniciarTransaccion()

            If String.IsNullOrEmpty(cod_com) Then
                nuevo = True
                dt = C.TraerDataTable("COM_RegistrarComiteCurricular", cod_cpf, txtNombre.Text.ToUpper, CDate(txtIniAprobacion.Text), CDate(txtFinAprobacion.Text), ddlSemestre.SelectedValue, txtNroDecreto.Text.ToUpper, 0, cod_user)
                If dt.Rows.Count > 0 Then
                    cod_com = dt.Rows(0).Item(0).ToString
                    Session("codigo_com") = cod_com
                    flag = True
                End If
            End If

            If Me.fuArchivo.HasFile Then
                Dim Archivos As HttpFileCollection = Request.Files
                For i As Integer = 0 To Archivos.Count - 1
                    fc_SubirArchivo(idTabla, cod_com, Archivos(i))
                Next
            End If

            dt = C.TraerDataTable("COM_ActualizarComiteCurricular", cod_com, cod_cpf, txtNombre.Text.ToUpper, CDate(txtIniAprobacion.Text), CDate(txtFinAprobacion.Text), ddlSemestre.SelectedValue, txtNroDecreto.Text.ToUpper, 0, idTabla, cod_user)
            If dt.Rows.Count > 0 Then
                flag = True
            End If

            If flag Then
                Dim dtDet As Data.DataTable = TryCast(ViewState("dt"), Data.DataTable)
                Dim dtElim As Data.DataTable = TryCast(ViewState("dtElim"), Data.DataTable)

                '--> Confirmar eliminación de miembros del comité
                For i As Integer = 0 To dtElim.Rows.Count - 1
                    Dim codigo_mie As Object
                    codigo_mie = dtElim.Rows(i).Item(0).ToString
                    codigo_mie = IIf(String.IsNullOrEmpty(codigo_mie), "0", codigo_mie)
                    C.Ejecutar("COM_EliminarComiteCurricularMiembros", codigo_mie, cod_user)
                Next

                If dtDet.Rows.Count > 0 Then
                    '--> Confirmar registro o actualización de miembros del comité
                    For i As Integer = 0 To dtDet.Rows.Count - 1
                        Dim codigo_per As Integer
                        Dim rol_mie As String
                        Dim codigo_mie As Object
                        codigo_mie = dtDet.Rows(i).Item(3).ToString
                        codigo_mie = IIf(String.IsNullOrEmpty(codigo_mie), "0", codigo_mie)

                        codigo_per = dtDet.Rows(i).Item(1).ToString
                        rol_mie = dtDet.Rows(i).Item(4).ToString

                        C.Ejecutar("COM_RegistrarMiembrosComiteCurricular", cod_com, codigo_mie, codigo_per, rol_mie, cod_user)
                    Next
                End If
            End If

            C.TerminarTransaccion()

            If flag Then
                Call mt_MostrarDetalle(cod_cpf)

                If nuevo Then
                    Call mt_ShowMessage("Registro satisfactorio del comité", MessageType.Success)
                Else
                    Call mt_ShowMessage("Actualización satisfactoria del comité", MessageType.Success)
                End If
            End If
        Catch ex As Exception
            C.AbortarTransaccion()
            Call mt_ShowMessage(ex.Message.Replace("'", ""), MessageType.Error, True)
        End Try
    End Sub

    Protected Sub btnDescargar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim button As HtmlButton = DirectCast(sender, HtmlButton)
            Dim com As String = button.Attributes("codigo_com")
            Dim idArchivo As Long
            idArchivo = CLng(button.Attributes("idArchivo"))
            If idArchivo = 0 Then Throw New Exception("Archivo de resolución no disponible")
            Call mt_DescargarArchivo(com)
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", ""), MessageType.Info)
        End Try
    End Sub

    Protected Sub btnLimpiar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnLimpiar.Click
        Try
            Call mt_MostrarDetalle(Me.ddlCarreraProf.SelectedValue)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Protected Sub grwComite_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles grwComite.RowEditing
        grwComite.EditIndex = e.NewEditIndex
        Call BindGrid()
    End Sub

    Protected Sub grwComite_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grwComite.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim ddlRol As DropDownList = CType(e.Row.FindControl("ddlRol"), DropDownList)
            Dim ddlDocente As DropDownList = CType(e.Row.FindControl("ddlDocente"), DropDownList)

            If Not ddlRol Is Nothing Then
                ddlRol.SelectedValue = grwComite.DataKeys(e.Row.RowIndex).Values(1).ToString()
            End If

            If Not ddlDocente Is Nothing Then
                ddlDocente.DataSource = fc_GetDocentes()
                ddlDocente.DataValueField = "codigo_Per"
                ddlDocente.DataTextField = "docente"
                ddlDocente.DataBind()

                'Agregar fila en blanco
                ddlDocente.Items.Insert(0, New ListItem("[-- Seleccione un Docente --]", ""))
                ddlDocente.SelectedValue = grwComite.DataKeys(e.Row.RowIndex).Values(2).ToString()
            End If
        End If

        If e.Row.RowType = DataControlRowType.Footer Then
            Dim ddlDocente As DropDownList = CType(e.Row.FindControl("ddlNewDocente"), DropDownList)

            If ddlDocente IsNot Nothing Then
                ddlDocente.DataSource = fc_GetDocentes()
                ddlDocente.DataValueField = "codigo_Per"
                ddlDocente.DataTextField = "docente"
                ddlDocente.DataBind()

                'Agregar fila en blanco
                ddlDocente.Items.Insert(0, New ListItem("[-- Seleccione un Docente --]", ""))
            End If
        End If
    End Sub

    Protected Sub OnUpdate(ByVal sender As Object, ByVal e As EventArgs)
        Try
            Dim dt As Data.DataTable = TryCast(ViewState("dt"), Data.DataTable)
            Dim row As GridViewRow = TryCast((TryCast(sender, LinkButton)).NamingContainer, GridViewRow)
            Dim ddlDocente As DropDownList = CType(grwComite.Rows(row.RowIndex).FindControl("ddlDocente"), DropDownList)
            Dim ddlRol As DropDownList = CType(grwComite.Rows(row.RowIndex).FindControl("ddlRol"), DropDownList)

            Dim valid As Generic.Dictionary(Of String, String) = fc_ValidarMiembros(ddlRol.SelectedItem.Text, ddlDocente.SelectedItem.Text)

            If valid.Item("rpta") = 1 Then
                dt.Rows(row.RowIndex)("codigo_per") = ddlDocente.SelectedValue
                dt.Rows(row.RowIndex)("nombre_per") = ddlDocente.SelectedItem.Text
                dt.Rows(row.RowIndex)("rol_mie") = ddlRol.SelectedItem.Text
                ViewState("dt") = dt
                grwComite.EditIndex = -1
                Call BindGrid()
            Else
                Call mt_ShowMessage(valid.Item("msg"), MessageType.Info, True)
            End If
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", ""), MessageType.Info, True)
        End Try
    End Sub

    Protected Sub OnCancel(ByVal sender As Object, ByVal e As EventArgs)
        grwComite.EditIndex = -1
        Call BindGrid()
    End Sub

    Protected Sub OnDelete(ByVal sender As Object, ByVal e As EventArgs)
        Try
            Dim valor As String = "1"
            Dim rpta As String = ""
            Dim dt As Data.DataTable = TryCast(ViewState("dt"), Data.DataTable)
            Dim row As GridViewRow = TryCast((TryCast(sender, LinkButton)).NamingContainer, GridViewRow)
            Dim codigo_mie As String = grwComite.DataKeys(row.RowIndex).Item("codigo_mie").ToString

            If Not String.IsNullOrEmpty(codigo_mie) AndAlso Not codigo_mie.Equals("0") Then
                Dim dtRpta As New Data.DataTable
                Dim codigo_com As Object
                codigo_com = IIf(String.IsNullOrEmpty(Session("codigo_com")), "-1", Session("codigo_com"))

                C.AbrirConexion()
                dtRpta = C.TraerDataTable("COM_VerificarComiteCurricularMiembros", codigo_com)
                C.CerrarConexion()

                valor = dtRpta.Rows(0).Item(0).ToString
                rpta = dtRpta.Rows(0).Item(1).ToString
                dtRpta.Dispose()

                If valor.Equals("1") Then
                    Dim dtElim As Data.DataTable = TryCast(ViewState("dtElim"), Data.DataTable)
                    dtElim.Rows.Add(codigo_mie, codigo_com)
                    ViewState("dtElim") = dtElim
                Else
                    Call mt_ShowMessage(rpta, MessageType.Info, True)
                    Return
                End If
            End If

            If valor.Equals("1") Then
                dt.Rows.RemoveAt(row.RowIndex)
                ViewState("dt") = dt
                grwComite.EditIndex = -1
                Call BindGrid()
            End If
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Info, True)
        End Try
    End Sub

    Protected Sub OnNew(ByVal sender As Object, ByVal e As EventArgs)
        Try
            Dim dt As Data.DataTable = TryCast(ViewState("dt"), Data.DataTable)
            Dim ddlDocente As DropDownList = CType(grwComite.FooterRow.FindControl("ddlNewDocente"), DropDownList)
            Dim ddlRol As DropDownList = CType(grwComite.FooterRow.FindControl("ddlNewRol"), DropDownList)

            Dim valid As Generic.Dictionary(Of String, String) = fc_ValidarMiembros(ddlRol.SelectedItem.Text, ddlDocente.SelectedItem.Text)
            Dim cod_com As Object = IIf(String.IsNullOrEmpty(Session("codigo_com")), 0, Session("codigo_com"))

            If valid.Item("rpta") = 1 Then
                If String.IsNullOrEmpty(dt.Rows(0).Item(1).ToString) Then
                    dt.Rows.RemoveAt(0)
                End If

                dt.Rows.Add(cod_com, ddlDocente.SelectedValue.ToString, ddlDocente.SelectedItem.Text, 0, ddlRol.SelectedItem.Text, 1)
                ViewState("dt") = dt
                grwComite.EditIndex = -1
                Call BindGrid()
            Else
                Call mt_ShowMessage(valid.Item("msg"), MessageType.Info, True)
            End If
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", ""), MessageType.Info, True)
        End Try
    End Sub

#End Region

#Region "Métodos"

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

    Private Sub mt_CargarSemestre()
        Try
            Dim dt As New Data.DataTable("data")

            C.AbrirConexion()
            dt = C.TraerDataTable("ConsultarCicloAcademico", "DA", "")
            C.CerrarConexion()

            ddlSemestre.DataSource = dt
            ddlSemestre.DataTextField = "descripcion_Cac"
            ddlSemestre.DataValueField = "codigo_Cac"
            ddlSemestre.DataBind()

            dt.Dispose()
        Catch ex As Exception
            ClientScript.RegisterStartupScript(Me.GetType, "Pop", "<script>mostrarMensaje('" & ex.Message.Replace("'", "") & "', 'danger');</script>")
        End Try
    End Sub

    Private Sub mt_CargarCarreras()
        Try
            Dim dt As New Data.DataTable("Data")
            C.AbrirConexion()

            dt = C.TraerDataTable("COM_ListarCarreraProfesional", cod_user, cod_ctf)

            Me.ddlCarreraProf.DataSource = dt
            Me.ddlCarreraProf.DataValueField = "codigo_Cpf"
            Me.ddlCarreraProf.DataTextField = "nombre_Cpf"
            Me.ddlCarreraProf.DataBind()

            dt.Dispose()
            C.CerrarConexion()
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", ""), MessageType.Error)
        End Try
    End Sub

    Private Sub mt_MostrarDetalle(ByVal codigo_cpf As String)
        Try
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
        Dim cod_com As Object = IIf(String.IsNullOrEmpty(Session("codigo_com")), DBNull.Value, Session("codigo_com"))

        Try
            C.AbrirConexion()
            dt = C.TraerDataTable("COM_ListarMiembrosComiteCurricular", cod_com, cod_user)
            C.CerrarConexion()

            ViewState("dt") = dt
            Call BindGrid()
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Private Sub BindGrid()
        Try
            Dim dt As Data.DataTable = TryCast(ViewState("dt"), Data.DataTable)

            If dt.Rows.Count > 0 Then
                grwComite.DataSource = dt
                grwComite.DataBind()
            Else
                dt.Rows.Add(dt.NewRow())
                grwComite.DataSource = dt
                grwComite.DataBind()

                grwComite.Rows(0).Cells.Clear()

                'Dim totalColumns As Integer = grwComite.Rows(0).Cells.Count
                'grwComite.Rows(0).Cells.Clear()
                'grwComite.Rows(0).Cells.Add(New TableCell())
                'grwComite.Rows(0).Cells(0).ColumnSpan = totalColumns
                'grwComite.Rows(0).Cells(0).Style.Add("text-align", "center")
                'grwComite.Rows(0).Cells(0).Text = "No se ha registrado participantes"
            End If

            udpComite.Update()

            Me.divAlertModal.Visible = False
            Me.lblMensaje.InnerText = ""
            updMensaje.Update()
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", ""), MessageType.Error, True)
        End Try
    End Sub

    Private Sub mt_LimpiarFormulario()
        txtIniAprobacion.Text = ""
        txtFinAprobacion.Text = ""
        ddlSemestre.SelectedValue = 0
        txtNroDecreto.Text = ""
        spnFile.InnerText = "No se eligió resolución"
        hf.Value = "0"
    End Sub

    Private Sub mt_LlenarFormulario()
        Try
            Dim dt As New Data.DataTable("data")
            Dim cod_com As Object = IIf(String.IsNullOrEmpty(Session("codigo_com")), "-1", Session("codigo_com"))

            C.AbrirConexion()
            dt = C.TraerDataTable("COM_ObtenerDatosComiteCurricular", cod_com, idTabla)
            C.CerrarConexion()

            spnFile.InnerText = "No se eligió resolución"

            If dt.Rows.Count > 0 Then
                txtNombre.Text = dt.Rows(0).Item("nombre_com").ToString.Trim
                txtIniAprobacion.Text = dt.Rows(0).Item("fechaAprob_com").ToString.Trim
                txtFinAprobacion.Text = dt.Rows(0).Item("fechaTermino_com").ToString.Trim
                ddlSemestre.SelectedValue = dt.Rows(0).Item("codigo_cac").ToString.Trim
                txtNroDecreto.Text = dt.Rows(0).Item("nroDecreto_com").ToString.Trim
                spnFile.InnerText = dt.Rows(0).Item("archivo").ToString.Trim

                If spnFile.InnerText.Trim().Equals("No se eligió resolución") Then
                    hf.Value = "0"
                Else
                    hf.Value = "1"
                End If
            End If

            dt.Dispose()
        Catch ex As Exception
            ClientScript.RegisterStartupScript(Me.GetType, "Pop", "<script>mostrarMensaje('" & ex.Message.Replace("'", "") & "', 'danger');</script>")
        End Try
    End Sub

    'Este método me permite llamar manualmente al evento RowDataBound que vuelve a reenderizar los botones de acción
    Private Sub RefreshGrid()
        For Each _Row As GridViewRow In grwResultado.Rows
            grwResultado_RowDataBound(grwResultado, New GridViewRowEventArgs(_Row))
        Next
    End Sub

    Private Sub mt_DescargarArchivo(ByVal IdArchivo As Long)
        Try
            Dim wsCloud As New ClsArchivosCompartidosV2
            Dim list As New Generic.Dictionary(Of String, String)
            Dim obj As New ClsConectarDatos
            Dim tb As New Data.DataTable
            Dim cif As New ClsCRM
            'Dim usuario As String = "USAT\ESAAVEDRA"
            Dim usuario As String = Session("perlogin") '"USAT\ESAAVEDRA" 
            'Dim _id As String = ""
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            tb = obj.TraerDataTable("ArchivosCompartidos_Listar2", 1, idTabla, IdArchivo, "FWLY7K6WZE")
            obj.CerrarConexion()
            If tb.Rows.Count = 0 Then Throw New Exception("¡ No se encontró el archivo !")
            'If usuario <> "" Then Throw New Exception(usuario & "-" & tb.Rows(0).Item("IdArchivo").ToString)
            '_id = cif.DesencriptaTexto(tb.Rows(0).Item("IdArchivo").ToString)
            list.Add("IdArchivo", tb.Rows(0).Item("IdArchivo").ToString)
            list.Add("Usuario", usuario)
            list.Add("Token", "FWLY7K6WZE")
            Dim envelope As String = wsCloud.SoapEnvelopeDescarga(list)
            Dim result As String = wsCloud.PeticionRequestSoap(ruta, envelope, "http://usat.edu.pe/DownloadFile", usuario)
            Dim imagen As String = fc_ResultFile(result)
            If tb.Rows.Count > 0 Then
                Dim extencion As String
                extencion = tb.Rows(0).Item("Extencion")

                Select Case tb.Rows(0).Item("Extencion")
                    Case ".txt"
                        extencion = "text/plain"
                    Case ".doc"
                        extencion = "application/ms-word"
                    Case ".xls"
                        extencion = "application/vnd.ms-excel"
                    Case ".gif"
                        extencion = "image/gif"
                    Case ".jpg"
                    Case ".jpeg"
                    Case "jpeg"
                        extencion = "image/jpeg"
                    Case "png"
                        extencion = "image/png"
                    Case ".bmp"
                        extencion = "image/bmp"
                    Case ".wav"
                        extencion = "audio/wav"
                    Case ".ppt"
                        extencion = "application/mspowerpoint"
                    Case ".dwg"
                        extencion = "image/vnd.dwg"
                    Case ".pdf"
                        extencion = "application/pdf"
                    Case Else
                        extencion = "application/octet-stream"
                End Select

                Dim bytes As Byte() = Convert.FromBase64String(imagen)
                Response.Clear()
                Response.Buffer = False
                Response.Charset = ""
                Response.Cache.SetCacheability(HttpCacheability.NoCache)
                Response.ContentType = extencion
                Response.AddHeader("content-disposition", "attachment;filename=" + tb.Rows(0).Item("NombreArchivo").ToString)
                Response.AppendHeader("Content-Length", bytes.Length.ToString())
                Response.BinaryWrite(bytes)
                Response.End()
            End If
            'Response.Write(envelope)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Funciones"

    Private Function fc_GetDocentes() As Data.DataTable
        Dim dt As New Data.DataTable("data")

        C.AbrirConexion()
        dt = C.TraerDataTable("COM_ListarDocentes")
        C.CerrarConexion()

        Return dt
    End Function

    Private Function fc_SubirArchivo(ByVal idTabla As String, ByVal codigo As String, ByVal file As HttpPostedFile) As String
        Dim list As New Generic.Dictionary(Of String, String)
        Try
            Dim _TablaId As String = idTabla
            Dim _Archivo As HttpPostedFile = file
            Dim _TransaccionId As String = codigo
            Dim _Fecha As String = Date.Now.ToString("dd/MM/yyyy")
            Dim _Usuario As String = Session("perlogin").ToString
            'Dim _Usuario As String = "USAT\ESAAVEDRA"
            Dim Input(_Archivo.ContentLength) As Byte

            Dim br As New IO.BinaryReader(_Archivo.InputStream)
            Dim binData As Byte() = br.ReadBytes(_Archivo.InputStream.Length)
            'Dim base64 = Convert.ToBase64String(binData)
            Dim _Nombre As String = IO.Path.GetFileName(_Archivo.FileName.Replace("&", "_").Replace("'", "_").Replace("*", "_"))

            Dim wsCloud As New ClsArchivosCompartidos

            list.Add("Fecha", _Fecha)
            list.Add("Extencion", IO.Path.GetExtension(_Archivo.FileName))
            list.Add("Nombre", _Nombre)
            list.Add("TransaccionId", _TransaccionId)
            list.Add("TablaId", _TablaId)
            list.Add("NroOperacion", "")
            list.Add("Archivo", Convert.ToBase64String(binData, 0, binData.Length))
            list.Add("Usuario", _Usuario)
            list.Add("Equipo", "")
            list.Add("Ip", "")
            list.Add("param8", _Usuario)

            Dim envelope As String = wsCloud.SoapEnvelope(list)
            Dim result As String = wsCloud.PeticionRequestSoap(ruta, envelope, "http://usat.edu.pe/UploadFile", _Usuario)

            Return fc_ResultFile(result)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Function fc_ResultFile(ByVal cadXml As String) As String
        Try
            Dim xError As String()
            Dim nsMgr As System.Xml.XmlNamespaceManager
            Dim xml As System.Xml.XmlDocument = New System.Xml.XmlDocument()
            xml.LoadXml(cadXml)
            nsMgr = New System.Xml.XmlNamespaceManager(xml.NameTable)
            nsMgr.AddNamespace("soap", "http://schemas.xmlsoap.org/soap/envelope/")
            Dim res As System.Xml.XmlNode = xml.DocumentElement.SelectSingleNode("/soap:Envelope/soap:Body", nsMgr)
            xError = res.InnerText.Split(":")
            If xError.Length = 2 Then
                Throw New Exception(res.InnerText)
            End If
            Return res.InnerText
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Function fc_Validar() As Generic.Dictionary(Of String, String)
        Dim valid As New Generic.Dictionary(Of String, String)
        Dim err As Boolean = False
        valid.Add("rpta", 1)
        valid.Add("msg", "")
        valid.Add("control", "")

        If Not err And String.IsNullOrEmpty(Request("txtNombre")) Then
            If Not err Then
                valid.Item("rpta") = 0
                valid.Item("msg") = "Debe ingresar un nombre del comité"
                valid.Item("control") = "txtNombre"
                err = True
                ScriptManager.GetCurrent(Me.Page).SetFocus(Me.txtNombre)
            End If
            txtNombre.Attributes.Item("data-error") = "true"
        Else
            txtNombre.Attributes.Item("data-error") = "false"
        End If

        If Not err And String.IsNullOrEmpty(Request("txtIniAprobacion")) Then
            If Not err Then
                valid.Item("rpta") = 0
                valid.Item("msg") = "Debe ingresar la fecha de inicio de la aprobación"
                valid.Item("control") = "txtIniAprobacion"
                err = True
                ScriptManager.GetCurrent(Me.Page).SetFocus(Me.txtIniAprobacion)
            End If
            txtIniAprobacion.Attributes.Item("data-error") = "true"
        Else
            txtIniAprobacion.Attributes.Item("data-error") = "false"
        End If

        If Not err And String.IsNullOrEmpty(Request("txtFinAprobacion")) Then
            If Not err Then
                valid.Item("rpta") = 0
                valid.Item("msg") = "Debe ingresar la fecha de término de la aprobación"
                valid.Item("control") = "txtFinAprobacion"
                err = True
                ScriptManager.GetCurrent(Me.Page).SetFocus(Me.txtFinAprobacion)
            End If
            txtFinAprobacion.Attributes.Item("data-error") = "true"
        Else
            txtFinAprobacion.Attributes.Item("data-error") = "false"
        End If

        If Not err Then
            Try
                Dim fecha As Date = CDate(txtIniAprobacion.Text)
            Catch ex As Exception
                valid.Item("rpta") = 0
                valid.Item("msg") = "La fecha de inicio de aprobación no tiene el formato de fecha correcto"
                valid.Item("control") = "txtIniAprobacion"
                err = True
                ScriptManager.GetCurrent(Me.Page).SetFocus(Me.txtIniAprobacion)
                txtIniAprobacion.Attributes.Item("data-error") = "true"
            End Try
        End If

        If Not err Then
            Try
                Dim fecha As Date = CDate(txtFinAprobacion.Text)
            Catch ex As Exception
                valid.Item("rpta") = 0
                valid.Item("msg") = "La fecha de termino de aprobación no tiene el formato de fecha correcto"
                valid.Item("control") = "txtFinAprobacion"
                err = True
                ScriptManager.GetCurrent(Me.Page).SetFocus(Me.txtFinAprobacion)
                txtFinAprobacion.Attributes.Item("data-error") = "true"
            End Try
        End If

        If Not err Then
            Dim desde As Date = CDate(txtIniAprobacion.Text)
            Dim hasta As Date = CDate(txtFinAprobacion.Text)
            If desde > hasta Then
                valid.Item("rpta") = 0
                valid.Item("msg") = "La fecha de término de la aprobación no puede ser menor a la fecha de inicio"
                valid.Item("control") = "txtFinAprobacion"
                err = True
                ScriptManager.GetCurrent(Me.Page).SetFocus(Me.txtFinAprobacion)
                txtFinAprobacion.Attributes.Item("data-error") = "true"
            Else
                txtFinAprobacion.Attributes.Item("data-error") = "false"
            End If
        End If

        If Not err And ddlSemestre.SelectedValue = "-1" Then
            If Not err Then
                valid.Item("rpta") = 0
                valid.Item("msg") = "Seleccione el semestre de creación"
                valid.Item("control") = "ddlSemestre"
                err = True
                ScriptManager.GetCurrent(Me.Page).SetFocus(Me.ddlSemestre)
            End If
            ddlSemestre.Attributes.Item("data-error") = "true"
        Else
            ddlSemestre.Attributes.Item("data-error") = "false"
        End If

        If Not err And String.IsNullOrEmpty(Request("txtNroDecreto")) Then
            If Not err Then
                valid.Item("rpta") = 0
                valid.Item("msg") = "Debe ingresar el número de resolución"
                valid.Item("control") = "txtNroDecreto"
                err = True
                ScriptManager.GetCurrent(Me.Page).SetFocus(Me.txtNroDecreto)
            End If
            txtNroDecreto.Attributes.Item("data-error") = "true"
        Else
            txtNroDecreto.Attributes.Item("data-error") = "false"
        End If

        Dim dtDet As Data.DataTable = TryCast(ViewState("dt"), Data.DataTable)
        If Not err And dtDet.Rows.Count <= 1 Then
            Dim cod_mie As String = dtDet.Rows(0).Item(0).ToString
            If String.IsNullOrEmpty(cod_mie) Then
                valid.Item("rpta") = 0
                valid.Item("msg") = "Debe ingresar a los miembros del comité"
                valid.Item("control") = "grwComite"
                err = True
            End If
            grwComite.Attributes.Item("data-error") = "true"
        Else
            grwComite.Attributes.Item("data-error") = "false"
        End If

        'If Not err AndAlso (String.IsNullOrEmpty(spnFile.InnerText) Or spnFile.InnerText.Trim().Equals("No se eligió resolución")) Then
        If Not err AndAlso hf.Value = "0" Then
            If Not err Then
                valid.Item("rpta") = 0
                valid.Item("msg") = "Debe seleccionar el archivo de resolución del comité"
                valid.Item("control") = "fuArchivo"
                err = True
            End If
            fuArchivo.Attributes.Item("data-error") = "true"
        Else
            fuArchivo.Attributes.Item("data-error") = "false"
        End If

        Return valid
    End Function

    Private Function fc_ValidarMiembros(ByVal rol As String, ByVal docente As String) As Generic.Dictionary(Of String, String)
        Dim valid As New Generic.Dictionary(Of String, String)
        valid.Add("rpta", 1)
        valid.Add("msg", "")
        valid.Add("control", "")

        If String.IsNullOrEmpty(docente) Or docente.StartsWith("[--") Then
            valid.Item("rpta") = 0
            valid.Item("msg") = "Debe seleccionar un Docente"
            valid.Item("control") = "grwComite"

        ElseIf String.IsNullOrEmpty(rol) Or rol.StartsWith("[--") Then
            valid.Item("rpta") = 0
            valid.Item("msg") = "Debe seleccionar un Rol"
            valid.Item("control") = "grwComite"

        Else
            For i As Integer = 0 To grwComite.Rows.Count - 1
                Dim lblRol As Label
                lblRol = CType(grwComite.Rows(i).FindControl("lblRol"), Label)

                Dim lblDocente As Label
                lblDocente = CType(grwComite.Rows(i).FindControl("lblDocente"), Label)

                If lblDocente IsNot Nothing Then
                    If lblDocente.Text.Contains(docente) Then
                        valid.Item("rpta") = 0
                        valid.Item("msg") = "No se puede registrar al mismo docente 2 veces en el mismo comité"
                        valid.Item("control") = "grwComite"

                        Exit For
                    End If
                End If

                If rol.Equals("PRESIDENTE") Or rol.Equals("SECRETARIO") Then
                    If lblRol IsNot Nothing Then
                        If lblRol.Text.Contains(rol) Then
                            valid.Item("rpta") = 0
                            valid.Item("msg") = "Ya existe un miembro del tipo " & rol
                            valid.Item("control") = "grwComite"

                            Exit For
                        End If
                    End If
                End If
            Next
        End If

        Return valid
    End Function

#End Region

End Class
