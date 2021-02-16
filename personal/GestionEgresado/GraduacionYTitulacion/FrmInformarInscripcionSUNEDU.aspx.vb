Imports System.Collections.Generic

Partial Class GradosYTitulos_FrmInformarInscripcionSUNEDU
    Inherits System.Web.UI.Page
    Dim contador As Integer = 0

    Private Sub ListarSesionesConsejo()
        Dim dt As New Data.DataTable
        Dim obj As New ClsGradosyTitulos
        dt = obj.ListaSesionConsejoU("L", "%")

        For i As Integer = 0 To dt.Rows.Count - 1
            Me.cboSesion.Items.Add(New ListItem(dt.Rows(i).Item("descripcion_scu").ToString, dt.Rows(i).Item("codigo_scu")))
        Next
        Me.cboSesion.DataBind()
    End Sub

    Private Sub ListarTipoDenominacion()
        Dim dt As New Data.DataTable
        Dim obj As New ClsGradosyTitulos
        dt = obj.ConsultarTipoDenominacion("GYT", "")

        For i As Integer = 0 To dt.Rows.Count - 1
            Me.cboTipoDenominacion.Items.Add(New ListItem(dt.Rows(i).Item("nombre").ToString, dt.Rows(i).Item("codigo")))
        Next
        Me.cboTipoDenominacion.DataBind()
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            Me.btnEnviarCorreo.Visible = False
            ListarSesionesConsejo()
            ListarTipoDenominacion()
        End If
    End Sub

    Function Validar() As Boolean
        If Me.cboSesion.SelectedValue = "" And Me.cboTipoDenominacion.SelectedValue = "" Then
            If Me.ddlTipo.SelectedValue = "F" Then
                If Me.txtBusqueda.Text.Length < 3 Then
                    'Me.divMensaje.InnerHtml = "Debe Ingresar al menos 3 Caracteres para Buscar Sin Filtros."
                    'Me.divMensaje.Attributes.Add("Class", "alert alert-danger")

                    ScriptManager.GetCurrent(Me.Page).SetFocus(Me.txtBusqueda)
                    Me.gvAlumnos.DataSource = Nothing
                    Me.gvAlumnos.DataBind()
                    Me.btnEnviarCorreo.Visible = False
                    Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "fnMensaje('error','Debe Ingresar al menos 3 Caracteres para Buscar Sin Filtros.')", True)

                    Return False
                End If
            End If
        End If
        Return True
    End Function


    Protected Sub btnBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBuscar.Click
   
        contador = 0
        Me.lblcontador.Text = ""
        If Validar() = True Then
            Me.divMensaje.InnerHtml = ""
            Me.divMensaje.Attributes.Remove("Class")

            Dim obj As New ClsGradosyTitulos
            Dim dt As New Data.DataTable

            Me.hdTipo.Value = Me.ddlTipo.SelectedValue

            If Me.ddlTipo.SelectedValue = "F" Then
                If Me.cboEstado.SelectedValue = "T" Then
                    dt = obj.ConsultarInformarInscripcionSUNEDU(Me.cboSesion.SelectedValue, Me.cboTipoDenominacion.SelectedValue, Me.txtBusqueda.Text)
                Else
                    dt = obj.ConsultarInscripcionSuneduxEstado(Me.cboEstado.SelectedValue, Me.cboSesion.SelectedValue, Me.cboTipoDenominacion.SelectedValue, Me.txtBusqueda.Text)
                End If
            Else
                dt = obj.ConsultarInformarInscripcionSUNEDUElectronico(Me.cboEstado.SelectedValue, Me.cboTipoDenominacion.SelectedValue, Me.txtBusqueda.Text)
            End If
            Me.gvAlumnos.DataSource = dt
            Me.gvAlumnos.DataBind()

            Me.lblcontador.Text = " Filas seleccionadas de " + Me.gvAlumnos.Rows.Count.ToString + ""

            For Each row As GridViewRow In Me.gvAlumnos.Rows
                Dim chckrw As CheckBox = CType(row.FindControl("chkAutorizar"), CheckBox)
                If chckrw.Checked Then
                    contador += 1
                    row.ControlStyle.BackColor = Drawing.Color.AntiqueWhite
                    row.ControlStyle.Font.Bold = True
                Else
                    row.ControlStyle.BackColor = Drawing.Color.White
                    row.ControlStyle.Font.Bold = False
                End If
            Next
            Me.lblContadorSeleccionado.Text = contador.ToString
            If dt.Rows.Count > 0 Then
                Me.btnEnviarCorreo.Visible = True
            Else
                Me.btnEnviarCorreo.Visible = False
            End If



        End If
        Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "loading", "fnLoading(false)", True)

    End Sub

    Protected Sub chckchanged(ByVal sender As Object, ByVal e As EventArgs)
        Dim chckheader As CheckBox = CType(gvAlumnos.HeaderRow.FindControl("chkall"), CheckBox)
        contador = 0
        For Each row As GridViewRow In gvAlumnos.Rows
            Dim chckrw As CheckBox = CType(row.FindControl("chkAutorizar"), CheckBox)
            If chckheader.Checked = True Then
                chckrw.Checked = True
            Else
                chckrw.Checked = False
            End If
            If chckrw.Checked = True Then
                contador = contador + 1
                row.ControlStyle.BackColor = Drawing.Color.AntiqueWhite
                row.ControlStyle.Font.Bold = True
            Else
                row.ControlStyle.BackColor = Drawing.Color.White
                row.ControlStyle.Font.Bold = False
            End If
        Next
        Me.lblContadorSeleccionado.Text = contador.ToString
    End Sub

    Public Sub lnkContar_Click(ByVal sender As Object, ByVal e As EventArgs)

        For Each row As GridViewRow In Me.gvAlumnos.Rows
            Dim chckrw As CheckBox = CType(row.FindControl("chkAutorizar"), CheckBox)
            If chckrw.Checked Then
                contador += 1
                row.ControlStyle.BackColor = Drawing.Color.AntiqueWhite
                row.ControlStyle.Font.Bold = True
            Else
                row.ControlStyle.BackColor = Drawing.Color.White
                row.ControlStyle.Font.Bold = False
            End If

        Next
        Me.lblContadorSeleccionado.Text = contador.ToString
    End Sub


    Public Sub btnEnviarCorreo_Click(ByVal sender As Object, ByVal e As EventArgs)
        EnviarEmail(sender, e)
    End Sub

    Sub EnviarEmail(ByVal sender As Object, ByVal e As EventArgs)
        Me.divMensaje.InnerHtml = ""
        Me.divMensaje.Attributes.Remove("Class")

        Dim JSONresult As String = ""

        Dim obj As New ClsConectarDatos
        Dim tb As New Data.DataTable

        Try

            Dim codigos As String = ""
            For Each row As GridViewRow In Me.gvAlumnos.Rows
                Dim chckrw As CheckBox = CType(row.FindControl("chkAutorizar"), CheckBox)
                If chckrw.Checked Then
                    If codigos = "" Then
                        codigos += Me.gvAlumnos.DataKeys(row.RowIndex).Values("codigo_egr").ToString
                    Else
                        codigos += "," + Me.gvAlumnos.DataKeys(row.RowIndex).Values("codigo_egr").ToString
                    End If
                End If
            Next

            If codigos <> "" Then

                Dim objGyt As New ClsGradosyTitulos
                Dim dtr As New Data.DataTable
                dtr = objGyt.InformarInscripcionSUNEDU(codigos, Session("id_per"), Request("ctf"), Me.hdTipo.Value)
                If dtr.Rows(0).Item("rpta") = 1 Then
                    Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "alertok", "fnMensaje('success','" + dtr.Rows(0).Item("msje").ToString + "')", True)
                    btnBuscar_Click(sender, e)
                Else
                    Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "alerterr", "fnMensaje('error','" + dtr.Rows(0).Item("msje").ToString + "')", True)

                End If
            Else
                Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "alert2", "fnMensaje('error','Debe seleccionar al menos un egresado')", True)
            End If
            Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "loadingEnvio", "fnLoading(false)", True)

        Catch ex As Exception
            Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "alert2", "fnMensaje('error','" + ex.Message.ToString + "')", True)

        End Try
    End Sub

    Protected Sub gvAlumnos_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvAlumnos.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim email As String = DataBinder.Eval(e.Row.DataItem, "email_alu").ToString()
            'e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFC0CB")
            If email = "" Or validarEmail(email) = False Then
                e.Row.Cells(0).ForeColor = Drawing.Color.Red
                e.Row.Cells(1).ForeColor = Drawing.Color.Red
                e.Row.Cells(2).ForeColor = Drawing.Color.Red
                e.Row.Cells(3).ForeColor = Drawing.Color.Red
                e.Row.Cells(4).ForeColor = Drawing.Color.Red
                e.Row.Cells(5).ForeColor = Drawing.Color.Red
                e.Row.Font.Bold = True
            End If
        End If
    End Sub

    Function validarEmail(ByVal email As String) As Boolean
        Try
            Dim estructura As String = "^[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?$"
            Dim match As Match = Regex.Match(email.Trim(), estructura, RegexOptions.IgnoreCase)

            If match.Success Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Return False
        End Try

    End Function

    Protected Sub ddlTipo_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlTipo.SelectedIndexChanged
        If Me.ddlTipo.SelectedValue = "E" Then
            Me.cboSesion.SelectedValue = ""
            Me.cboSesion.Enabled = False
        Else
            Me.cboSesion.SelectedValue = ""
            Me.cboSesion.Enabled = True
        End If
    End Sub
End Class
