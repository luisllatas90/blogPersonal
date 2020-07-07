
Partial Class academico_notas_administrarconsultar_lstalumnosmatriculados
    Inherits System.Web.UI.Page

    Public Enum MessageType
        Success
        [Error]
        Info
        Warning
    End Enum

    Protected Sub ShowMessage(ByVal Message As String, ByVal type As MessageType)
        Page.RegisterStartupScript("Mensaje", "<script>ShowMessage('" & Message.ToString.Replace("'", "") & "','" & type & "');</script>")
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If (Session("id_per") Is Nothing) Then
            Response.Redirect("../../../ErrorSistema.aspx")
        End If
        If IsPostBack = False Then
            If Session("cup") Is Nothing Then
                ShowMessage("Curso no encontrado", MessageType.Error)
            Else
                Me.HdCodigoCup.Value = Session("cup")
                VerificaSiEsRecuperacion()

                'Yperez
                Session("Ncodigo_aut") = Not_ConsultarAutorizacion()

                If VerificaCursoDocente() = True Then
                    If ValidaAcceso() = True Then
                        Me.btnGuardar.Visible = True
                    Else
                        Me.btnGuardar.Visible = False
                    End If

                    ListaAlumnos()


                Else
                    ShowMessage("Ud. no es el docente del curso", MessageType.Error)
                End If
            End If

        End If
    End Sub

    Private Sub VerificaSiEsRecuperacion()
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        Try
            obj.AbrirConexion()
            dt = obj.TraerDataTable("ACAD_EsCursoRecuperacion", Me.HdCodigoCup.Value)
            obj.CerrarConexion()

            If (dt.Rows.Count > 0) Then
                Me.HdEsRecuperacion.Value = "S"
            Else
                Me.HdEsRecuperacion.Value = "N"
            End If

        Catch ex As Exception
            ShowMessage("Error: " & ex.Message.Replace("'", ""), MessageType.Error)
        End Try
    End Sub

    Private Function VerificaCursoDocente() As Boolean
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        Try
            obj.AbrirConexion()
            dt = obj.TraerDataTable("ACAD_VerificaCursoDocente", Me.HdCodigoCup.Value, Session("id_perNota"))
            'dt = obj.TraerDataTable("ValidarCargaProfesor", Session("id_per"), Me.HdCodigoCup.Value)
            obj.CerrarConexion()

            If dt.Rows.Count = 0 Then
                Return False
            End If

            Return True
        Catch ex As Exception
            ShowMessage("Error: " & ex.Message.Replace("'", ""), MessageType.Error)
            Return False
        End Try
    End Function

    Private Function ValidaAcceso() As Boolean
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        Try
            obj.AbrirConexion()
            dt = obj.TraerDataTable("NOT_VerificaAccesoNotas", Me.HdCodigoCup.Value, Session("id_perNota"))
            obj.CerrarConexion()            

            If dt.Rows.Count = 0 Then
                ShowMessage("No se pudo validar el acceso", MessageType.Error)
                Return False
            Else
                Me.lblCurso.Text = "(" & dt.Rows(0).Item("descripcion_cac") & ")  " & dt.Rows(0).Item("nombre_cur") & " - Grupo: " & dt.Rows(0).Item("grupohor_cup")
                Me.lblDocente.Text = dt.Rows(0).Item("nombre_per")
                Me.HdEstadoCurso.Value = dt.Rows(0).Item("estadoNota_Cup")
                spAprobados.InnerText = dt.Rows(0).Item("Aprobados")
                spDesaprobados.InnerText = dt.Rows(0).Item("Desaprobados")
                spRetirados.InnerText = dt.Rows(0).Item("Retirados")
                spInhabilitados.InnerText = dt.Rows(0).Item("Inhabilitados")

                If CInt(Session("Ncodigo_aut")) <= 0 Then
                    If dt.Rows(0).Item("mensajeProfesor").ToString.Trim <> "" Then
                        ShowMessage(dt.Rows(0).Item("mensajeProfesor"), MessageType.Error)
                        Return False
                    End If
                End If
            End If
            Return True
        Catch ex As Exception
            ShowMessage("Error: " & ex.Message.Replace("'", ""), MessageType.Error)
            Return False
        End Try
    End Function

    Private Sub ListaAlumnos()
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        Try
            obj.AbrirConexion()
            dt = obj.TraerDataTable("ACAD_ListaAlumnosMatriculadosxCup", Me.HdCodigoCup.Value)
            obj.CerrarConexion()

            Me.gvAlumnos.DataSource = dt
            Me.gvAlumnos.DataBind()
        Catch ex As Exception
            ShowMessage("Error: " & ex.Message.Replace("'", ""), MessageType.Error)
        End Try
    End Sub    

    Protected Sub gvAlumnos_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvAlumnos.RowDataBound
        If (e.Row.RowType.ToString = "DataRow") Then


            If gvAlumnos.DataKeys(e.Row.RowIndex).Values(1) = 0 Then
                If e.Row.Cells(7).Text = "M" Or e.Row.Cells(7).Text = "Matriculado" Then
                    e.Row.Cells(7).Text = "Matriculado"
                ElseIf e.Row.Cells(7).Text = "R" Or e.Row.Cells(7).Text = "Retirado" Then
                    e.Row.Cells(7).Text = "Retirado"
                    e.Row.Cells(5).Text = "-"
                    e.Row.Cells(5).Enabled = False
                    e.Row.Cells(9).Text = ""
                End If
            Else
                e.Row.Cells(7).Text = "Inhabilitado"
                e.Row.Cells(5).Text = "-"
                e.Row.Cells(5).Enabled = False
                e.Row.Cells(9).Text = ""
            End If


            If CInt(Session("Ncodigo_aut")) = 0 Then
                e.Row.Cells(9).Text = ""
            Else
                Me.btnGuardar.Visible = False            
            End If

            If Me.btnGuardar.Visible = False Then
                Dim tx As TextBox = e.Row.Cells(5).FindControl("txtNota")
                If e.Row.Cells(7).Text = "Matriculado" Then
                    tx.ReadOnly = True
                    tx.BorderStyle = BorderStyle.None
                End If

            Else
                Dim tx As TextBox = e.Row.Cells(5).FindControl("txtNota")
                If e.Row.Cells(7).Text = "Matriculado" Then
                    If tx.Text.ToString = "0.00" Then
                        tx.Text = "0"
                    End If
                End If

            End If

            e.Row.Cells(1).Text = e.Row.RowIndex + 1

           


            If e.Row.Cells(6).Text = "Retirado" Then
                e.Row.Cells(6).ForeColor = System.Drawing.Color.Black                
            ElseIf e.Row.Cells(6).Text = "Desaprobado" Then
                e.Row.Cells(6).ForeColor = System.Drawing.Color.Red
                e.Row.Cells(5).ForeColor = System.Drawing.Color.Red
            ElseIf e.Row.Cells(6).Text = "Aprobado" Then
                e.Row.Cells(6).ForeColor = System.Drawing.Color.Blue
                e.Row.Cells(5).ForeColor = System.Drawing.Color.Blue
            End If


            If e.Row.Cells(7).Text = "Inhabilitado" Then
                e.Row.Cells(7).ForeColor = System.Drawing.Color.Orange
            ElseIf e.Row.Cells(7).Text = "Retirado" Then
                e.Row.Cells(7).ForeColor = System.Drawing.Color.Black
            ElseIf e.Row.Cells(7).Text = "Matriculado" Then
                e.Row.Cells(7).ForeColor = System.Drawing.Color.Green
            End If

         

        End If
    End Sub

    Protected Sub btnGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardar.Click
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        Try
            If ValidarDatos() = True Then
                Dim nota_dma As Double = 0
                Dim condicion_dma As String = ""
                Dim swRegNotas As Boolean = False
                Dim txtNota As TextBox
                For i As Integer = 0 To Me.gvAlumnos.Rows.Count - 1

                    If gvAlumnos.Rows(i).Cells(7).Text = "Matriculado" Then
                        swRegNotas = True
                        txtNota = TryCast(gvAlumnos.Rows(i).FindControl("txtNota"), TextBox)
                        nota_dma = txtNota.Text
                        If nota_dma >= 14 Then
                            condicion_dma = "A"
                        Else
                            condicion_dma = "D"
                        End If

                        obj.AbrirConexion()
                        obj.Ejecutar("ActualizarNotaEstudiante", Session("id_per") _
                                     , gvAlumnos.DataKeys(i).Values("codigo_dma"), nota_dma _
                                     , condicion_dma, Session("id_per"), "Registro de Notas", "")
                        obj.CerrarConexion()
                    End If
                Next

                If swRegNotas = True Then
                    obj.AbrirConexion()
                    obj.Ejecutar("ACAD_CierraCursoProgramado", Me.HdCodigoCup.Value)
                    obj.CerrarConexion()
                    ShowMessage("Notas Registradas", MessageType.Success)
                    Me.btnGuardar.Visible = False
                    For i As Integer = 0 To Me.gvAlumnos.Rows.Count - 1
                        txtNota = TryCast(gvAlumnos.Rows(i).FindControl("txtNota"), TextBox)
                        txtNota.Enabled = False
                        txtNota.ReadOnly = True
                        txtNota.BorderStyle = BorderStyle.None
                    Next
                Else
                    ShowMessage("No se pudo registrar", MessageType.Error)
                End If

                ListaAlumnos()


                'Response.Write("<script>alert('Notas registradas');</script>")
                'Response.Redirect("../profesor/miscursos.aspx?id=" & Session("id_per") & "&ctf=" & Session("ctf_notas") & "&")

                obj = Nothing
            End If            
        Catch ex As Exception
            ShowMessage("Error: " & ex.Message.Replace("'", ""), MessageType.Error)
        End Try
    End Sub

    Public Function ValidarDatos() As Boolean
        For i As Integer = 0 To Me.gvAlumnos.Rows.Count - 1

            If gvAlumnos.Rows(i).Cells(7).Text = "Matriculado" Then
                Dim txtNota As TextBox = TryCast(gvAlumnos.Rows(i).FindControl("txtNota"), TextBox)
                If ParseaNumero(txtNota.Text) = False Then
                    ShowMessage("Nota no válida en " & gvAlumnos.Rows(i).Cells(4).Text, MessageType.Error)
                    Return False
                End If

                If txtNota.Text > 14 And HdEsRecuperacion.Value = "S" Then
                    ShowMessage("La nota no puede exceder 14", MessageType.Error)
                    Return False
                End If
            End If
        Next

        Return True
    End Function

    Public Function ParseaNumero(ByVal dato As String) As Boolean
        Try
            Integer.Parse(dato)
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Protected Sub btnRegresar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRegresar.Click
        Response.Redirect("../profesor/miscursos.aspx?id=" & Session("id_perNota") & "&ctf=" & Session("ctf_notas"))
    End Sub

    Private Function Not_ConsultarAutorizacion() As Integer
        'Yperez
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        Try
            obj.AbrirConexion()
            dt = obj.TraerDataTable("NOT_ConsultarRegistroNotas", 1, Me.HdCodigoCup.Value, Session("id_perNota"))
            obj.CerrarConexion()

            If dt.Rows.Count = 0 Then
                Return 0
            Else
                Return dt.Rows(0).Item("codigo_aut").toString
            End If

            Return True
        Catch ex As Exception
            ShowMessage("Error: " & ex.Message.Replace("'", ""), MessageType.Error)
            Return False
        End Try

    End Function

    Protected Sub gvAlumnos_RowCommand1(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvAlumnos.RowCommand
        Dim index As Integer = Convert.ToInt32(e.CommandArgument)
        If (e.CommandName = "ModificarNota") Then
            Dim txtNota As TextBox
            txtNota = CType(gvAlumnos.Rows(index).FindControl("txtNota"), TextBox)
            Page.RegisterStartupScript("Pop", "<script>openModal('myModal');</script>")
            Me.lblCodigo.Text = Me.gvAlumnos.Rows(index).Cells(2).Text
            Me.lblNombre.Text = Me.gvAlumnos.Rows(index).Cells(4).Text
            Me.lblNotaAnterior.text = FormatNumber(txtNota.text, 0)
            Me.lblCondicionAnterior.text = Me.gvAlumnos.Rows(index).Cells(6).Text
            Session("Ncodigo_dma") = gvAlumnos.DataKeys(index).Values("codigo_dma")
            'Cargar la Foto
            Dim ruta As String
            Dim obEnc As Object
            obEnc = Server.CreateObject("EncriptaCodigos.clsEncripta")

            ruta = obEnc.CodificaWeb("069" & Me.gvAlumnos.Rows(index).Cells(2).Text)
            ruta = "//intranet.usat.edu.pe/imgestudiantes/" & ruta
            Me.FotoAlumno.ImageUrl = ruta
            obEnc = Nothing

            Me.txtNotaNueva.focus()
        End If
    End Sub

    Protected Sub btnGuardarCambio_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardarCambio.Click
        If Me.txtNotaNueva.Text.Trim = "" Then
            ShowMessage("Falta ingresar la nueva nota.", MessageType.Error)
        ElseIf Me.txtMotivo.Text.Trim = "" Then
            ShowMessage("Debe ingresar el motivo.", MessageType.Error)
        Else
            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            obj.Ejecutar("ActualizarNotaEstudiante", Session("Ncodigo_aut"), Session("Ncodigo_dma"), Me.txtNotaNueva.Text, IIf(CInt(Me.txtNotaNueva.Text) < 14, "D", "A"), Session("id_per"), Me.txtMotivo.Text, "")
            obj.CerrarConexion()
        End If
    End Sub
End Class
