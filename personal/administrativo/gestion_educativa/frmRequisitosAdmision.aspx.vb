Imports System.Collections.Generic

Partial Class administrativo_pec_test_frmRequisitosAdmision
    Inherits System.Web.UI.Page

#Region "Variables"
    Private mo_Cnx As New ClsConectarDatos
    Private mo_RepoAdmision As New ClsAdmision
    Private ms_CodigoAlu As String = "0"
    Public mdt_RequisitosAdmision As New Data.DataTable
    Public lst_RequisitosAdmisionEntregados As New List(Of String)
#End Region

#Region "Eventos"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'If Session("id_per") = "" Or Request.QueryString("id") = "" Then
        '    Response.Redirect("../../../sinacceso.html")
        'End If

        Try
            If Not String.IsNullOrEmpty(Request.QueryString("alu")) Then
                ms_CodigoAlu = Request.QueryString("alu")
            End If

            If Not String.IsNullOrEmpty(Request.QueryString("modal")) Then
                Dim lb_Modal As Boolean = Request.QueryString("modal")
                If lb_Modal Then
                    botonesFormulario.Attributes.Item("class") &= " d-none"
                End If
            End If

            mdt_RequisitosAdmision = ListarRequisitosAdmision(ms_CodigoAlu, False)

            If Not IsPostBack Then 'Se carga la página por primera vez
                'Cargo los controles
                Dim ldt_RequisitosAdmisionEntregados As Data.DataTable = ListarRequisitosAdmision(ms_CodigoAlu, True)
                For Each _Row As Data.DataRow In ldt_RequisitosAdmisionEntregados.Rows
                    lst_RequisitosAdmisionEntregados.Add(_Row.Item("codigo_req"))
                Next
            Else
                'Aqui obtengo los valores enviados desde el formulario por POST
                lst_RequisitosAdmisionEntregados.Clear()
                Dim postValues As String() = Request.Form.AllKeys
                For Each _value As String In postValues
                    If _value IsNot Nothing AndAlso _value.StartsWith("chkRequisito") Then
                        lst_RequisitosAdmisionEntregados.Add(Request.Form.Item(_value))
                    End If
                Next
            End If

            GenerarControlesDinamicos()
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Protected Sub btnGuardar_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardar.ServerClick
        Guardar()
        udpRequisitos.Update()
    End Sub
#End Region

#Region "Métodos"
    Private Function ListarRequisitosAdmision(ByVal ls_CodigoAlu As String, ByVal lb_Entregados As Boolean) As Data.DataTable
        Try
            Return mo_RepoAdmision.ListarRequisitosAdmision(ls_CodigoAlu, lb_Entregados)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function VerificarRequisito(ByVal codigo_req As String) As Boolean
        Return lst_RequisitosAdmisionEntregados.Contains(codigo_req)
    End Function

    Private Sub GenerarControlesDinamicos()
        For Each _Row As Data.DataRow In mdt_RequisitosAdmision.Rows
            'Genero los controles dinámicamente
            Dim _liItem As New HtmlGenericControl
            _liItem.TagName = "li"
            _liItem.Attributes.Item("class") = "list-group-item"

            Dim _CustomControl As New HtmlGenericControl
            _CustomControl.TagName = "div"
            _CustomControl.Attributes.Item("class") = "custom-control custom-checkbox"

            Dim _CheckBox As New HtmlGenericControl
            _CheckBox.TagName = "input"
            _CheckBox.Attributes("type") = "checkbox"
            _CheckBox.Attributes.Item("class") = "custom-control-input"
            _CheckBox.Attributes.Item("value") = _Row.Item("codigo_req")
            _CheckBox.Attributes.Item("name") = "chkRequisito" & _Row.Item("codigo_req")
            _CheckBox.ID = "chkRequisito" & _Row.Item("codigo_req")
            If VerificarRequisito(_Row.Item("codigo_req")) Then
                _CheckBox.Attributes.Item("checked") = "checked"
            End If

            Dim _Label As New HtmlGenericControl
            _Label.TagName = "label"
            _Label.Attributes.Item("for") = "chkRequisito" & _Row.Item("codigo_req")
            _Label.Attributes.Item("class") = "custom-control-label"
            _Label.InnerText = _Row.Item("descripcion_req")

            _CustomControl.Controls.Add(_CheckBox)
            _CustomControl.Controls.Add(_Label)
            _liItem.Controls.Add(_CustomControl)
            listaRequisitos.Controls.Add(_liItem)
        Next
    End Sub

    Private Sub Guardar()
        Try
            If mdt_RequisitosAdmision.Rows.Count = 0 Then
                With respuestaPostback.Attributes
                    .Item("data-ispostback") = True
                    .Item("data-rpta") = "0"
                    .Item("data-msg") = "No hay documentos por registrar"
                End With
                Exit Sub
            End If

            Dim ls_CodigoAlu As String = Request.Params("alu")
            Dim ls_UsuarioReg As String = Request.Params("id")

            Dim ls_Requisitos As String = ""
            Dim ln_Contador As Integer = 0
            For Each _requisito As String In lst_RequisitosAdmisionEntregados
                ls_Requisitos &= _requisito
                ln_Contador += 1
                If lst_RequisitosAdmisionEntregados.Count > ln_Contador Then
                    ls_Requisitos &= ","
                End If
            Next

            Dim lo_Respuesta As Dictionary(Of String, String) = mo_RepoAdmision.GuardarRequisitosAdmision(ls_CodigoAlu, ls_Requisitos, ls_UsuarioReg)
            With respuestaPostback.Attributes
                .Item("data-ispostback") = True
                .Item("data-rpta") = lo_Respuesta.Item("rpta")
                If lo_Respuesta.Item("rpta") = "-1" Then
                    .Item("data-msg") = "Ha ocurrido un error en el servidor"
                    mensajeError.InnerHtml = ls_Requisitos
                Else
                    .Item("data-msg") = lo_Respuesta.Item("msg")
                End If
            End With
        Catch ex As Exception
            With respuestaPostback.Attributes
                .Item("data-ispostback") = True
                .Item("data-rpta") = "-1"
                .Item("data-msg") = "Ha ocurrido un error en el servidor"
            End With
            Throw ex
        End Try
    End Sub
#End Region
End Class
