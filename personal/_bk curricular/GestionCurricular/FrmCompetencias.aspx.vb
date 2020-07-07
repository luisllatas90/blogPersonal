Imports System.Collections.Generic
Imports System.IO

Partial Class GestionCurricular_FrmCompetencias
    Inherits System.Web.UI.Page

#Region "Declaracion de Variables"

    Dim cod_user As Integer

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
                Response.Redirect("../../../sinacceso.html")
            End If

            cod_user = Request.QueryString("id")

            If IsPostBack = False Then
                Call mt_CargarTipo()
                Call mt_CargarCategoria()
                Call mt_CargarDatos()
            Else
                Call RefreshGrid()
            End If

        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub btnListar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnListar.Click
        Call mt_CargarDatos()
    End Sub

    Protected Sub btnAgregar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAgregar.Click
        Try
            Me.hdCodigoCompetencia.Value = ""

            Page.RegisterStartupScript("Pop", "<script>openModal('nuevo', '', '');</script>")
        Catch ex As Exception
            Call mt_ShowMessage("Error_CargaDatos(): " & ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub btnEditar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            If Me.gvCompetencia.Rows.Count = 0 Then
                Page.RegisterStartupScript("alerta", "<script>alert('No existe Competencias para editar');</script>")
                Me.hdCodigoCompetencia.Value = ""

                Me.btnListar.Focus()
            Else
                Dim button As HtmlButton = DirectCast(sender, HtmlButton)
                Dim tip, cat As String

                Me.hdCodigoCompetencia.Value = button.Attributes("codigo_com")
                Me.txtNombre.Text = button.Attributes("nombre")

                tip = button.Attributes("tipo")
                cat = button.Attributes("categoria")

                Page.RegisterStartupScript("Pop", "<script>openModal('editar', '" & tip & "', '" & cat & "');</script>")
            End If

        Catch ex As Exception
            Call mt_ShowMessage("Error_CargaDatos(): " & ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub btnAceptar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAceptar.Click
        Dim obj As New ClsConectarDatos
        Dim codigo_com As Integer = 0
        Dim dt As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString

        Try
            If Not (IsDBNull(Session("perlogin"))) And Session("perlogin").ToString <> "" Then
                obj.IniciarTransaccion()

                If String.IsNullOrEmpty(Me.hdCodigoCompetencia.Value) Then
                    dt = obj.TraerDataTable("COM_RegistrarCompetenciaAprendizaje", Me.txtNombre.Text, Me.ddlTipoCompetencia.SelectedValue, Me.ddlCategoria.SelectedValue, cod_user)

                    If dt.Rows.Count > 0 Then
                        Call mt_ShowMessage("Competencia registrada con éxito", MessageType.Success)
                    End If
                Else
                    codigo_com = CInt(Me.hdCodigoCompetencia.Value)

                    obj.Ejecutar("COM_ActualizarCompetenciaAprendizaje", codigo_com, Me.txtNombre.Text, Me.ddlTipoCompetencia.SelectedValue, Me.ddlCategoria.SelectedValue, cod_user)

                    Call mt_ShowMessage("Competencia actualizada con éxito", MessageType.Success)
                End If

                obj.TerminarTransaccion()
                Call mt_CargarDatos()
            Else
                Throw New Exception("Inicie Sesión")
            End If
        Catch ex As Exception
            obj.AbortarTransaccion()
            Call mt_ShowMessage("Error_CargaDatos(): " & ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

#End Region

#Region "Metodos"

    Protected Sub mt_ShowMessage(ByVal Message As String, ByVal type As MessageType)
        Page.RegisterStartupScript("Mensaje", "<script>ShowMessage('" & Message & "','" & type.ToString & "');</script>")
    End Sub

    Private Sub mt_CargarTipo()
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString

        Try
            obj.AbrirConexion()
            dt = obj.TraerDataTable("COM_ListarTipoCompetencia")
            obj.CerrarConexion()

            Call mt_CargarCombo(Me.ddlTipoCompetencia, dt, "codigo_tcom", "nombre")
            dt.Dispose()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub mt_CargarCategoria()
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString

        Try
            obj.AbrirConexion()
            dt = obj.TraerDataTable("COM_ListarCategoriaCompetencia")
            obj.CerrarConexion()

            Call mt_CargarCombo(Me.ddlCategoria, dt, "codigo_cat", "nombre")
            dt.Dispose()
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

    Private Sub mt_CargarDatos()
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString

        Try
            obj.AbrirConexion()
            dt = obj.TraerDataTable("COM_ListarCompetenciaAprendizaje")
            obj.CerrarConexion()
            Me.gvCompetencia.DataSource = dt
            Me.gvCompetencia.DataBind()
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub gvCompetencia_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvCompetencia.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim celda As TableCellCollection = e.Row.Cells
            Dim tipo As String = Me.gvCompetencia.DataKeys(e.Row.RowIndex).Values.Item("codigo_tcom")
            Dim categoria As String = Me.gvCompetencia.DataKeys(e.Row.RowIndex).Values.Item("codigo_cat")
            Dim nombre As String = Me.gvCompetencia.DataKeys(e.Row.RowIndex).Values.Item("nombre_com")
            Dim idx As Integer = e.Row.RowIndex + 1

            Dim btnEditar As New HtmlButton
            With btnEditar
                .ID = "btnEditar" & idx
                .Attributes.Add("class", "btn btn-primary btn-sm")
                .Attributes.Add("type", "button")
                .Attributes.Add("codigo_com", e.Row.Cells(0).Text.Trim())
                .Attributes.Add("tipo", tipo)
                .Attributes.Add("categoria", categoria)
                .Attributes.Add("nombre", nombre)
                .Attributes.Add("title", "Editar Competencia")
                .InnerHtml = "<i class='fa fa-edit' title='Editar Competencia'></i>"

                AddHandler .ServerClick, AddressOf btnEditar_Click
            End With
            celda(4).Controls.Add(btnEditar)

            gvCompetencia.HeaderRow.TableSection = TableRowSection.TableHeader
        End If
    End Sub

    'Este método me permite llamar manualmente al evento RowDataBound que vuelve a reenderizar los botones de acción
    Private Sub RefreshGrid()
        For Each _Row As GridViewRow In gvCompetencia.Rows
            gvCompetencia_RowDataBound(gvCompetencia, New GridViewRowEventArgs(_Row))
        Next
    End Sub

#End Region

End Class
