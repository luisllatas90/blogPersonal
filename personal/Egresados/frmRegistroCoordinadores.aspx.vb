
Partial Class Egresados_frmRegistroCoordinadores
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (IsPostBack = False) Then
            wf_CargarListas()
        End If
    End Sub

    Sub wf_CargarListas()
        Dim obj As New ClsConectarDatos
        Dim dt As Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()

        '#Coordinadores
        dt = New Data.DataTable
        dt = obj.TraerDataTable("ConsultarPersonal", "ALPE", "")
        Me.ddlPersonal.DataSource = dt
        Me.ddlPersonal.DataTextField = "persona"
        Me.ddlPersonal.DataValueField = "codigo"
        Me.ddlPersonal.DataBind()
        'Me.ddlPersonal.SelectedValue = 0
        dt.Dispose()

        '#Escuela
        dt = New Data.DataTable
        dt = obj.TraerDataTable("ALUMNI_ListarCarreraProfesional", "2", "2")
        Me.ddlEscuela.DataSource = dt
        Me.ddlEscuela.DataTextField = "nombre"
        Me.ddlEscuela.DataValueField = "codigo"
        Me.ddlEscuela.DataBind()
        dt.Dispose()
        obj.CerrarConexion()
        obj = Nothing
    End Sub

    Private Function wf_validate() As Boolean
        If ddlPersonal.SelectedValue = 0 Then
            lbl_Mensaje.Text = "Debe Seleccionar un Personal"
            Return False
        End If

        If ddlEscuela.SelectedItem.ToString.ToUpper = "TODOS" Then
            lbl_Mensaje.Text = "Debe Seleccionar una Escuela"
            Return False
        End If

        '
        Dim dtt As New Data.DataTable
        Dim obj As New ClsConectarDatos

        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        dtt = obj.TraerDataTable("ALUMNI_ConsultarEstadoCoordinador", ddlPersonal.SelectedValue, ddlEscuela.SelectedValue)
        obj.CerrarConexion()

        If dtt.Rows(0).Item("dato").ToString > 0 Then
            lbl_Mensaje.Text = "Ya se encuentra Registrado al Coordinador: " + ddlPersonal.SelectedItem.ToString
            Return False
        End If
        'lbl_msgbox.Text = "Personal: " + ddlPersonal.SelectedValue.ToString + " - Escuela: " + ddlEscuela.SelectedValue.ToString + " Count: " + dtt.Rows(0).Item("dato").ToString
        obj = Nothing
        
        Me.lbl_Mensaje.Text = ""

        Return True
    End Function

    Protected Sub btnNuevo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNuevo.Click
        If wf_validate() = True Then
            Dim dt As New Data.DataTable
            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString

            obj.AbrirConexion()
            obj.Ejecutar("ALUMNI_RegistrarCoordinador", ddlPersonal.SelectedValue, ddlEscuela.SelectedValue)
            obj.CerrarConexion()

            obj.AbrirConexion()
            obj.Ejecutar("agregarusuarioaplicacion", "1", ddlPersonal.SelectedValue, 47, 145, 0, 0)
            obj.CerrarConexion()

            obj = Nothing
        End If
        Call wf_consultar("0")
    End Sub

    Sub wf_consultar(ByVal as_par As String)
        Try
            Dim dtConsultar As New Data.DataTable
            Dim obj As New ClsConectarDatos

            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()

            Dim ls_personal As String = IIf(ddlPersonal.SelectedItem.ToString.Trim = "TODOS", "%", ddlPersonal.SelectedValue)
            Dim ls_escuela As String = IIf(ddlEscuela.SelectedItem.ToString.Trim = "TODOS", "%", ddlEscuela.SelectedValue)

            If as_par = "1" Then
                dtConsultar = obj.TraerDataTable("ALUMNI_ListarPersonalEscuela", ls_personal, ls_escuela)
            Else
                dtConsultar = obj.TraerDataTable("ALUMNI_ListarPersonalEscuela", "%", "%")
            End If
            obj.CerrarConexion()

            dgv_Personal.DataSource = dtConsultar
            dgv_Personal.DataBind()
            dgv_Personal.Dispose()
            obj = Nothing

            Dim nContador As Integer = 0
            For i As Integer = 0 To dgv_Personal.Rows.Count - 1
                'Response.Write("<script>alert('" & Me.dgv_Personal.Rows(0).Cells(3).Text & "')</script>")
                If dgv_Personal.Rows(i).Cells("estado").ToString = "ACTIVO" Then
                    nContador = nContador + 1
                End If
            Next

            Me.lbl_Mensaje.Text = Me.dgv_Personal.Rows.Count.ToString & " REGISTROS ACTIVOS  DE " & Me.dgv_Personal.Rows.Count.ToString & " REGISTROS ENCONTRADOS"

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnBusca_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBusca.Click
        Call wf_consultar("1")
        'Call wf_ocultarColumnas()

        'lbl_Mensaje.Text = Me.dgv_Personal.Rows(0).Cells(5).Text
        'Response.Write("<script>alert('" & Me.dgv_Personal.Rows(0).Cells(5).Text & "')</script>")

    End Sub

    Protected Sub dgv_Personal_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles dgv_Personal.RowDeleting
        'Try
        '    Dim obj As New ClsConectarDatos
        '    obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        '    obj.AbrirConexion()

        '    'codigo_per,codigo_cpf

        '    'Response.Write("ALUMNI_ActualizarPersonalEscuela " & Me.dgv_Personal.Rows(e.RowIndex).Cells("codigo_alucor").Text)
        '    lblTitulo.Text = Me.dgv_Personal.Rows(e.RowIndex).Cells("codigo_alucor").Text




        '    'Dim li_codigo As Integer = Me.dgv_Personal.Rows(e.RowIndex).Cells("codigo_alucor").Text
        '    'obj.Ejecutar("ALUMNI_ActualizarPersonalEscuela", li_codigo)
        '    'obj.CerrarConexion()

        '    ' Eliminar en Acceso al modulo de Alumni
        '    Dim dtt As New Data.DataTable
        '    obj.AbrirConexion()

        '    Response.Write("ALUMNI_ConsultarEstadoCoordinador " & Me.dgv_Personal.DataKeys.Item(e.RowIndex).Values("codigo_per").ToString & "," & Me.dgv_Personal.DataKeys.Item(e.RowIndex).Values("codigo_cpf").ToString)


        '    '"ALUMNI_ConsultarEstadoCoordinador", Me.dgv_Personal.DataKeys.Item(e.RowIndex).Values("codigo_per").ToString, _
        '    'Me.dgv_Personal.DataKeys.Item(e.RowIndex).Values("codigo_cpf").ToString()

        '    'dtt = obj.TraerDataTable("ALUMNI_ConsultarEstadoCoordinador", Me.dgv_Personal.DataKeys.Item(e.RowIndex).Values("codigo_per").ToString, _
        '    '                         Me.dgv_Personal.DataKeys.Item(e.RowIndex).Values("codigo_cpf").ToString)

        '    'obj.CerrarConexion()


        '    If dtt.Rows(0).Item("dato").ToString = 0 Then
        '        'Eliminar en Tabla usuarioaplicacion
        '        'obj.AbrirConexion()

        '        Response.Write("ALUMNI_EliminarUsuarioAplicacion " & Me.dgv_Personal.DataKeys.Item(e.RowIndex).Values("codigo_per").ToString)



        '        'Dim ls_codigo_per As String = Me.dgv_Personal.DataKeys.Item(e.RowIndex).Values(0).ToString
        '        'dtt = obj.Ejecutar("ALUMNI_EliminarUsuarioAplicacion", ls_codigo_per)
        '        'obj.CerrarConexion()
        '    End If
        '    obj = Nothing

        '    'Call wf_consultar("0")
        'Catch ex As Exception

        'End Try
    End Sub

    Protected Sub ibtnElimina_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Try
            Dim ibtnRechaza As ImageButton
            Dim row As GridViewRow
            ibtnRechaza = sender
            row = ibtnRechaza.NamingContainer

            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            Dim li_codigo As Integer = Me.dgv_Personal.DataKeys.Item(row.RowIndex).Values("codigo_alucor")
            obj.Ejecutar("ALUMNI_ActualizarPersonalEscuela", li_codigo)
            obj.CerrarConexion()

            ' Eliminar en Acceso al modulo de Alumni
            Dim dtt As New Data.DataTable
            obj.AbrirConexion()
            dtt = obj.TraerDataTable("ALUMNI_ConsultarEstadoCoordinador", Me.dgv_Personal.DataKeys.Item(row.RowIndex).Values("codigo_per").ToString, _
                                     Me.dgv_Personal.DataKeys.Item(row.RowIndex).Values("codigo_cpf").ToString)
            obj.CerrarConexion()

            If dtt.Rows.Count > 0 Then
                'Eliminar en Tabla usuarioaplicacion
                obj.AbrirConexion()

                Dim ls_codigo_per As String = Me.dgv_Personal.DataKeys.Item(row.RowIndex).Values("codigo_per").ToString
                obj.Ejecutar("ALUMNI_EliminarUsuarioAplicacion", ls_codigo_per)
                obj.CerrarConexion()
            End If
            obj = Nothing

            Call wf_consultar("0")

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub
End Class
