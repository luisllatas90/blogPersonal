
Partial Class academico_estudiante_ListaInasistencia
    Inherits System.Web.UI.Page

    Public Enum MessageType
        Success
        Errors
        Info
        Warning
    End Enum

    Protected Sub ShowMessage(ByVal Message As String, ByVal type As MessageType)
        Page.RegisterStartupScript("Mensaje", "<script>ShowMessage('" & Message & "','" & type & "');</script>")
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (Session("id_per") Is Nothing) Then
            Response.Redirect("../../ErrorSistema.aspx")
        End If

        If Me.Request.QueryString("mod") = 2 Then
            Me.btnBuscar.Visible = False
            Me.btnInactivar.Visible = False
            ShowMessage("Aviso: Para Pre grado regular, las inhabilitaciones son automáticas", MessageType.Info)
        Else
            Me.btnBuscar.Visible = True
            Me.btnInactivar.Visible = True
        End If

        Try
            If IsPostBack = False Then
                'AvisoNavegador()                
                ActivaTab(1)

                CargaSemestre()

                Me.gvDatos.DataSource = Nothing
                Me.gvDatos.DataBind()

                If (Request.QueryString("ctf") = 1 Or Request.QueryString("ctf") = 85 Or Request.QueryString("ctf") = 181) Then
                    CargaCarreraProfesional()
                    Me.cboCarrera.Enabled = True
                    Me.cboCarreraAnula.Enabled = True
                Else
                    Dim codigo_cpf As Integer
                    codigo_cpf = RetornaCarrera()

                    If codigo_cpf <> -1 Then
                        CargaCarreraProfesional()
                        Me.cboCarrera.SelectedValue = codigo_cpf
                        Me.cboCarreraAnula.SelectedValue = codigo_cpf
                        ValidaCronograma("I")
                    Else
                        Me.cboCarrera.DataSource = Nothing
                        Me.cboCarrera.DataBind()

                        Me.btnBuscar.Enabled = False
                        Me.btnInactivar.Enabled = False
                        Me.btnBuscarAnula.Enabled = False
                        Me.btnAnula.Enabled = False
                        ShowMessage("Estimado, no se encontró una carrera profesional relacionada", MessageType.Errors)
                    End If

                    'Me.cboCarrera.Enabled = False
                    'Me.cboCarreraAnula.Enabled = False
                End If
            End If

            
        Catch ex As Exception
            ShowMessage("Error: " & ex.Message, MessageType.Errors)
        End Try
    End Sub

    Private Function RetornaCarrera() As Integer
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        Try
            obj.AbrirConexion()
            dt = obj.TraerDataTable("ACAD_ResponsableCarrera", Request.QueryString("mod"), Request.QueryString("id"))
            obj.CerrarConexion()

            If dt.Rows.Count > 0 Then
                Return dt.Rows(0).Item("codigo_cpf")
            Else
                Return -1
            End If

        Catch ex As Exception
            ShowMessage("Error: " & ex.Message, MessageType.Errors)
            Return -1
        End Try
    End Function

    Private Sub AvisoNavegador()
        'Response.Write(Request.Browser.Version.ToString)
        If Request.Browser.Browser.ToString = "IE" And Request.Browser.Version.ToString <= "8.0" Then
            ShowMessage("Versión antigua de navegador", MessageType.Errors)
        End If
        
    End Sub

    Protected Sub btnBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBuscar.Click
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        Try            
            If Me.cboCarrera.Items.Count > 0 Then
                obj.AbrirConexion()
                If Request.QueryString("mod") = 2 Then
                    dt = obj.TraerDataTable("Alumno_CalcularFaltasJustificadas", 0, Me.cboSemestre.SelectedValue, Me.cboCarrera.SelectedValue, Me.txtAlumno.Text.Replace(" ", "%"), Request.QueryString("mod"))
                Else
                    dt = obj.TraerDataTable("Alumno_CalcularFaltasJustificadasGO", 0, Me.cboSemestre.SelectedValue, Me.cboCarrera.SelectedValue, Me.txtAlumno.Text.Replace(" ", "%"), Request.QueryString("mod"))
                End If

                obj.CerrarConexion()

                Me.gvDatos.DataSource = dt
                Me.gvDatos.DataBind()
            Else
                ShowMessage("Seleccione una carrera profesional", MessageType.Errors)
            End If

            ActivaTab(1)
        Catch ex As Exception
            ShowMessage("Error: " & ex.Message, MessageType.Errors)
        End Try
    End Sub

    Private Sub CargaCarreraProfesional()
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        Try
            obj.AbrirConexion()
            If (Request.QueryString("ctf") = 1 Or Request.QueryString("ctf") = 85 Or Request.QueryString("ctf") = 181) Then
                dt = obj.TraerDataTable("ACAD_BuscaCarreraProfesional", "T", Request.QueryString("mod"), 0, "", 0)
            Else
                dt = obj.TraerDataTable("ACAD_BuscaCarreraProfesional", "", Request.QueryString("mod"), 0, "", Session("id_per"))
            End If
            obj.CerrarConexion()

            Me.cboCarrera.DataSource = dt
            Me.cboCarrera.DataValueField = "codigo_cpf"
            Me.cboCarrera.DataTextField = "nombre_cpf"
            Me.cboCarrera.DataBind()

            Me.cboCarreraAnula.DataSource = dt
            Me.cboCarreraAnula.DataValueField = "codigo_cpf"
            Me.cboCarreraAnula.DataTextField = "nombre_cpf"
            Me.cboCarreraAnula.DataBind()

            obj = Nothing
        Catch ex As Exception
            ShowMessage("Error: " & ex.Message, MessageType.Errors)
        End Try
    End Sub

    Private Sub CargaSemestre()
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        Try
            obj.AbrirConexion()
            dt = obj.TraerDataTable("ACAD_BuscaCicloAcademico", 0, "")
            obj.CerrarConexion()

            Me.cboSemestre.DataSource = dt
            Me.cboSemestre.DataValueField = "codigo_cac"
            Me.cboSemestre.DataTextField = "descripcion_cac"
            Me.cboSemestre.DataBind()

            Me.cboSemestreAnula.DataSource = dt
            Me.cboSemestreAnula.DataValueField = "codigo_cac"
            Me.cboSemestreAnula.DataTextField = "descripcion_cac"
            Me.cboSemestreAnula.DataBind()

            obj = Nothing
        Catch ex As Exception
            ShowMessage("Error: " & ex.Message, MessageType.Errors)
        End Try
    End Sub

    Protected Sub btnInactivar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnInactivar.Click
        Dim sw As Byte = 0
        Dim obj As New ClsConectarDatos        
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        Try
            For Each row As GridViewRow In gvDatos.Rows
                If row.RowType = DataControlRowType.DataRow Then
                    Dim chkSel As CheckBox = TryCast(row.Cells(0).FindControl("chkAccept"), CheckBox)
                    If chkSel.Checked = True Then
                        sw = 1
                        Dim codigo_dma As Integer = gvDatos.DataKeys(row.RowIndex).Value
                        obj.AbrirConexion()
                        obj.Ejecutar("ACAD_InactivaDetalleMatricula", codigo_dma, Session("id_per"))
                        obj.CerrarConexion()
                    End If
                End If
            Next

            obj = Nothing

            'Si encontró movimientos, vuelve a cargar la consulta
            If (sw = 1) Then
                btnBuscar_Click(sender, e)
            End If
        Catch ex As Exception
            ShowMessage("Error: " & ex.Message, MessageType.Errors)
        End Try
    End Sub

    Protected Sub gvDatos_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvDatos.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow And e.Row.RowType <> DataControlRowType.Header _
                And e.Row.RowType <> DataControlRowType.Footer Then
            If e.Row.Cells(0).Text <> "0 registros!" Then
                '5 Faltas permitidas
                Dim tope As Integer = e.Row.Cells(5).Text
                '6 Faltas
                Dim falta As Integer = e.Row.Cells(6).Text
                '7 Justificada
                Dim justificado As Integer = e.Row.Cells(7).Text
                '9 Columna del check
                Dim chkSel As CheckBox = TryCast(e.Row.FindControl("chkAccept"), CheckBox)

                If tope <= falta - justificado Then
                    chkSel.Enabled = True
                Else
                    chkSel.Enabled = False
                End If
            End If
        End If
    End Sub

    Protected Sub cboSemestre_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboSemestre.SelectedIndexChanged
        ValidaCronograma("I")   
        ActivaTab(1)
    End Sub

    'I. Inhabilitado        A. Anula Inhabilitados
    Private Sub ValidaCronograma(ByVal tipo As String)
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        Try
            If Request.QueryString("ctf") = 1 Or Request.QueryString("ctf") = 85 _
                    Or Request.QueryString("ctf") = 181 Then
                Me.btnBuscar.Enabled = True
                Me.btnInactivar.Enabled = True
            Else
                obj.AbrirConexion()
                If tipo = "I" Then
                    dt = obj.TraerDataTable("ACAD_InhabilitacionApta", Me.cboSemestre.SelectedValue, Request.QueryString("mod"))
                Else
                    dt = obj.TraerDataTable("ACAD_InhabilitacionApta", Me.cboSemestreAnula.SelectedValue, Request.QueryString("mod"))
                End If

                obj.CerrarConexion()

                If dt.Rows.Count > 0 Then
                    If tipo = "I" Then
                        Me.btnBuscar.Enabled = True
                        Me.btnInactivar.Enabled = True
                    Else
                        Me.btnBuscarAnula.Enabled = True
                        Me.btnAnula.Enabled = True
                    End If                    
                Else
                    ShowMessage("Cronograma para registrar inhabilitaciones " & Me.cboSemestre.SelectedItem.Text & " no está disponible. Comunicarse con Dirección Académica.", MessageType.Info)
                    Me.btnBuscar.Enabled = False
                    Me.btnInactivar.Enabled = False
                    Me.btnBuscarAnula.Enabled = False
                    Me.btnAnula.Enabled = False
                End If
            End If

            dt.Dispose()
            obj = Nothing
        Catch ex As Exception
            ShowMessage("Error: " & ex.Message, MessageType.Errors)
        End Try
    End Sub

    Protected Sub btnBuscarAnula_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBuscarAnula.Click
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        Dim rnd As New Random        
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        Try           
            obj.AbrirConexion()
            dt = obj.TraerDataTable("ACAD_ListaAlumnosInhabilitados", Me.cboSemestreAnula.SelectedValue, Me.cboCarreraAnula.SelectedValue, Me.txtAlumnoAnula.Text, Request.QueryString("mod"))
            obj.CerrarConexion()

            Me.gvAnula.DataSource = dt
            Me.gvAnula.DataBind()
            ActivaTab(2)
        Catch ex As Exception
            ShowMessage("Error: " & ex.Message, MessageType.Errors)
        End Try
    End Sub

    Protected Sub btnAnula_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAnula.Click
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        Try
            For Each row As GridViewRow In gvAnula.Rows
                If row.RowType = DataControlRowType.DataRow Then                    
                    Dim chkSel As CheckBox = TryCast(row.FindControl("chkAcceptAnula"), CheckBox)                                        
                    If chkSel.Checked = True Then
                        Dim codigo_dma As Integer = gvAnula.DataKeys(row.RowIndex).Value
                        obj.AbrirConexion()
                        obj.Ejecutar("ACAD_AnulaInhabilitacion", codigo_dma, Session("id_per"))
                        obj.CerrarConexion()
                    End If
                End If
            Next

            btnBuscarAnula_Click(sender, e)
        Catch ex As Exception
            ShowMessage("Error: " & ex.Message, MessageType.Errors)
        End Try
    End Sub

    Protected Sub cboSemestreAnula_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboSemestreAnula.SelectedIndexChanged
        ValidaCronograma("A")
        ActivaTab(2)
    End Sub

    Private Sub ActivaTab(ByVal NroTab As Integer)
        Dim rnd As New Random
        If NroTab = 1 Then
            Page.RegisterStartupScript(rnd.NextDouble.ToString, "<script>$('li:last').removeClass();</script>")
            Page.RegisterStartupScript(rnd.NextDouble.ToString, "<script>$('li:first').addClass( 'active' );</script>")
            Page.RegisterStartupScript(rnd.NextDouble.ToString, "<script>$('#tab2default').removeClass().addClass( 'tab-pane fade' );</script>")
            Page.RegisterStartupScript(rnd.NextDouble.ToString, "<script>$('#tab1default').addClass( 'tab-pane active fade in' );</script>")
        ElseIf NroTab = 2 Then
            Page.RegisterStartupScript(rnd.NextDouble.ToString, "<script>$('li:first').removeClass();</script>")
            Page.RegisterStartupScript(rnd.NextDouble.ToString, "<script>$('li:last').addClass( 'active' );</script>")
            Page.RegisterStartupScript(rnd.NextDouble.ToString, "<script>$('#tab1default').removeClass().addClass( 'tab-pane fade' );</script>")
            Page.RegisterStartupScript(rnd.NextDouble.ToString, "<script>$('#tab2default').addClass( 'tab-pane active fade in' );</script>")
        End If
        rnd = Nothing
    End Sub

End Class
