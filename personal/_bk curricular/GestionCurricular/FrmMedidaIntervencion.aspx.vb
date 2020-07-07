﻿
Partial Class GestionCurricular_FrmMedidaIntervencion
    Inherits System.Web.UI.Page

#Region "Declaracion de Variables"

    Dim cod_user As Integer '= 359

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
            cod_user = Session("id_per")

            If Not IsPostBack Then
                Call mt_CargarSemestre()
                Call mt_CargarDatos("0", "0", "0", cod_user)
            End If
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", ""), MessageType.Warning)
        End Try
    End Sub

    Protected Sub cboSemestre_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboSemestre.SelectedIndexChanged
        Try
            Call mt_CargarCarreraProfesional(cboSemestre.SelectedValue, cod_user)
            Call mt_CargarMomentos(cboSemestre.SelectedValue)
            Call mt_CargarDatos(cboSemestre.SelectedValue, cboCarrProf.SelectedValue, cboMomento.SelectedValue, cod_user)
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", ""), MessageType.Warning)
        End Try
    End Sub

    Protected Sub cboCarrProf_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboCarrProf.SelectedIndexChanged
        Try
            Call mt_CargarDatos(cboSemestre.SelectedValue, cboCarrProf.SelectedValue, cboMomento.SelectedValue, cod_user)
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", ""), MessageType.Warning)
        End Try
    End Sub

    Protected Sub cboMomento_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboMomento.SelectedIndexChanged
        Try
            Call mt_CargarDatos(cboSemestre.SelectedValue, cboCarrProf.SelectedValue, cboMomento.SelectedValue, cod_user)
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", ""), MessageType.Warning)
        End Try
    End Sub

    Protected Sub gvAsignatura_RowEditing(ByVal sender As Object, ByVal e As GridViewEditEventArgs)
        gvAsignatura.EditIndex = e.NewEditIndex
        Call mt_CargarDatos(cboSemestre.SelectedValue, cboCarrProf.SelectedValue, cboMomento.SelectedValue, cod_user)
    End Sub

    Protected Sub gvAsignatura_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gvAsignatura.PageIndexChanging
        Call mt_CargarDatos(cboSemestre.SelectedValue, cboCarrProf.SelectedValue, cboMomento.SelectedValue, cod_user)

        Me.gvAsignatura.PageIndex = e.NewPageIndex
        Me.gvAsignatura.DataBind()
    End Sub

    Protected Sub OnUpdateMedida(ByVal sender As Object, ByVal e As EventArgs)
        Try
            Dim row As GridViewRow = TryCast((TryCast(sender, LinkButton)).NamingContainer, GridViewRow)
            Dim codigo_cup As String = gvAsignatura.DataKeys(row.RowIndex).Item("codigo_cup").ToString
            Dim codigo_med As String = gvAsignatura.DataKeys(row.RowIndex).Item("codigo_med").ToString
            Dim txtMedida As TextBox = CType(gvAsignatura.Rows(row.RowIndex).FindControl("txtMedida"), TextBox)
            Dim rbtSI As RadioButton = CType(gvAsignatura.Rows(row.RowIndex).FindControl("rbtSI"), RadioButton)
            Dim chk As Int16 = IIf(rbtSI.Checked, 1, 0)

            If chk = 1 And String.IsNullOrEmpty(txtMedida.Text) Then
                Call mt_ShowMessage("Debe ingresar una descripción de intervención", MessageType.Info)
            Else
                Dim obj As New ClsConectarDatos
                Dim dt As New Data.DataTable
                obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString

                obj.AbrirConexion()
                If String.IsNullOrEmpty(codigo_med) Or codigo_med.Equals("0") Then
                    dt = obj.TraerDataTable("DEA_RegistrarMedidaIntervencion", codigo_cup, cboMomento.SelectedValue, txtMedida.Text, chk, cod_user)
                Else
                    dt = obj.TraerDataTable("DEA_ActualizarMedidaIntervencion", codigo_med, cboMomento.SelectedValue, txtMedida.Text, chk, cod_user)
                End If
                obj.CerrarConexion()

                Dim rpta As String = dt.Rows(0).Item(0).ToString
                rpta = IIf(String.IsNullOrEmpty(rpta), "0", rpta)

                If CInt(rpta) > 0 Then
                    Call mt_ShowMessage("Medida de Intervención " & IIf(String.IsNullOrEmpty(codigo_med), "registrada", "actualizada") & " con éxito", MessageType.Success)
                Else
                    Call mt_ShowMessage("No se realizó ningún cambio en la Medida de Intervención", MessageType.Warning)
                End If
                dt.Dispose()

                gvAsignatura.EditIndex = -1
                Call mt_CargarDatos(cboSemestre.SelectedValue, cboCarrProf.SelectedValue, cboMomento.SelectedValue, cod_user)
            End If
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", ""), MessageType.Warning)
        End Try
    End Sub

    Protected Sub OnCancelMedida(ByVal sender As Object, ByVal e As EventArgs)
        gvAsignatura.EditIndex = -1
        Call mt_CargarDatos(cboSemestre.SelectedValue, cboCarrProf.SelectedValue, cboMomento.SelectedValue, cod_user)
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

            obj.AbrirConexion()
            dt = obj.TraerDataTable("DEA_ListarCarrerasProfesionales", codigo_cac, user, 2)
            obj.CerrarConexion()

            mt_CargarCombo(Me.cboCarrProf, dt, "codigo_Cpf", "nombre_Cpf")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub mt_CargarMomentos(ByVal codigo_cac As String)
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
        Try
            codigo_cac = IIf(String.IsNullOrEmpty(codigo_cac), "0", codigo_cac)

            obj.AbrirConexion()
            dt = obj.TraerDataTable("DEA_ListarCorteSemestre", codigo_cac, "D")
            obj.CerrarConexion()

            mt_CargarCombo(Me.cboMomento, dt, "codigo_cor", "nombre_sem")
            cboMomento.Items.Insert(0, New ListItem("[--- Seleccione corte ---]", "-1"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub mt_CargarDatos(ByVal codigo_cac As String, ByVal codigo_cpf As String, ByVal codigo_cor As String, ByVal user As Integer)
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
        Try
            codigo_cac = IIf(String.IsNullOrEmpty(codigo_cac), "0", codigo_cac)
            codigo_cpf = IIf(String.IsNullOrEmpty(codigo_cpf), "0", codigo_cpf)
            codigo_cor = IIf(String.IsNullOrEmpty(codigo_cor), "0", codigo_cor)

            If CInt(codigo_cac) > 0 And CInt(codigo_cpf) > 0 And CInt(codigo_cor) > 0 Then
                obj.AbrirConexion()
                dt = obj.TraerDataTable("DEA_ListarMedidaIntervencion", codigo_cac, codigo_cpf, codigo_cor, user, 2)
                obj.CerrarConexion()
            End If

            Me.gvAsignatura.DataSource = dt
            Me.gvAsignatura.DataBind()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

End Class
