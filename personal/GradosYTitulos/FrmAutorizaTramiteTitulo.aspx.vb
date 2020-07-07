
Partial Class GradosYTitulos_FrmAutorizaTramiteTitulo
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

    Private Sub ListaCarrerasxSesion(ByVal codigo_scu As String)
        Dim obj As New ClsGradosyTitulos
        Dim dt As New Data.DataTable
        dt = obj.ConsultarCarreraxSesion(codigo_scu)

        Me.cboCarrera.Items.Clear()
        Me.cboCarrera.Items.Add(New ListItem("--Seleccione--", ""))

        For i As Integer = 0 To dt.Rows.Count - 1
            Me.cboCarrera.Items.Add(New ListItem(dt.Rows(i).Item("nombre_Cpf").ToString, dt.Rows(i).Item("codigo_cpf")))
        Next

        Me.cboCarrera.DataBind()

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            ListarSesionesConsejo()
        End If
    End Sub

    Protected Sub cboSesion_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboSesion.SelectedIndexChanged
        If Me.cboSesion.SelectedValue <> "" Then
            ListaCarrerasxSesion(Me.cboSesion.SelectedValue)
        Else
            Me.cboCarrera.Items.Clear()
            Me.cboCarrera.Items.Add(New ListItem("--Seleccione--", ""))
            Me.cboCarrera.DataBind()
        End If
    End Sub

    Protected Sub btnBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBuscar.Click
        contador = 0
        Me.lblcontador.Text = ""
        If Validar() = True Then
            Me.divMensaje.InnerHtml = ""
            Me.divMensaje.Attributes.Remove("Class")

            Dim obj As New ClsGradosyTitulos
            Dim dt As New Data.DataTable
            dt = obj.ListarAlumnosAutorizarTramite(Me.cboSesion.SelectedValue, Me.cboCarrera.SelectedValue, Me.txtBusqueda.Text)
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


        End If
    End Sub

    Protected Sub btnAutorizar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAutorizar.Click
        Try
            If gvAlumnos.Rows.Count > 0 Then

                Me.divMensaje.InnerHtml = ""
                Me.divMensaje.Attributes.Remove("Class")

                Dim codigos_egr As String = ""
                Dim codigo_egr_QUITAR As String = ""
                For i As Integer = 0 To gvAlumnos.Rows.Count - 1
                    Dim chkAutoriza As CheckBox = (CType(gvAlumnos.Rows(i).FindControl("chkAutorizar"), CheckBox))
                    If chkAutoriza.Checked = True Then
                        If codigos_egr = "" Then
                            codigos_egr = Me.gvAlumnos.DataKeys(i).Values("codigo_egr").ToString
                        Else
                            codigos_egr = codigos_egr + "," + Me.gvAlumnos.DataKeys(i).Values("codigo_egr").ToString
                        End If
                    Else
                        If codigo_egr_QUITAR = "" Then
                            codigo_egr_QUITAR = Me.gvAlumnos.DataKeys(i).Values("codigo_egr").ToString
                        Else
                            codigo_egr_QUITAR = codigo_egr_QUITAR + "," + Me.gvAlumnos.DataKeys(i).Values("codigo_egr").ToString
                        End If
                    End If
                Next

                Dim obj As New ClsGradosyTitulos
                Dim dt As New Data.DataTable
                dt = obj.AutorizarTramiteTitulo(codigos_egr, codigo_egr_QUITAR)
                If dt.Rows(0).Item("Respuesta").ToString = "1" Then
                    Me.divMensaje.InnerHtml = dt.Rows(0).Item("Mensaje").ToString
                    Me.divMensaje.Attributes.Add("Class", "alert alert-success")
                Else
                    Me.divMensaje.InnerHtml = dt.Rows(0).Item("Mensaje").ToString
                    Me.divMensaje.Attributes.Add("Class", "alert alert-danger")
                End If
            Else
                Me.divMensaje.InnerHtml = "Debe Realizar Búsqueda, Ningún Egresado en Lista para Actualizar."
                Me.divMensaje.Attributes.Add("Class", "alert alert-danger")
            End If

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
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


    Function Validar() As Boolean
        If Me.cboSesion.SelectedValue = "" And Me.cboCarrera.SelectedValue = "" Then
            If Me.txtBusqueda.Text.Length < 3 Then
                Me.divMensaje.InnerHtml = "Debe Ingresar al menos 3 Caracteres para Buscar Sin Filtros."
                Me.divMensaje.Attributes.Add("Class", "alert alert-danger")
                ScriptManager.GetCurrent(Me.Page).SetFocus(Me.txtBusqueda)
                Me.gvAlumnos.DataSource = Nothing
                Return False
            End If
        End If
        Return True
    End Function

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


End Class
