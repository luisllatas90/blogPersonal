
Partial Class academico_horarios_lsthorarioregistradoV2
    Inherits System.Web.UI.Page

    Public modo As String = ""
    Public codigo_cac As String = ""
    Public codigo_amb As String = ""
    Public codigo_cup As String = ""
    Public codigo_per As String = ""
    Public codigo_usu As String = ""
    Public codigo_cpf As String = ""
    Public dia As String = ""



    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            modo = Request.QueryString("modo")
            codigo_cac = Request.QueryString("codigo_cac")
            codigo_amb = Request.QueryString("codigo_amb")
            codigo_cup = Request.QueryString("codigo_cup")
            codigo_per = Request.QueryString("codigo_per")
            codigo_usu = Request.QueryString("codigo_usu")
            codigo_cpf = Request.QueryString("codigo_cpf")
            dia = Request.QueryString("dia")

           

            If IsPostBack = False Then
                Session("Agregar") = False
                Session("Modificar") = False
                Session("Eliminar") = False
                Session("HayReg") = False

                Session("Xcodigo_lho") = 0


                Dim param1 As String = 0, param2 As String = 0, titulo As String = ""
                Select Case modo
                    Case "CU"
                        modo = 2
                        param1 = codigo_cup
                        param2 = codigo_per
                        titulo = "Horario registrado por Curso [día " & UCase(ConvDia(dia)) & "]"
                    Case "AU"
                        modo = 3
                        param1 = codigo_amb
                        param2 = codigo_cac
                        titulo = "Horario registrado por Ambiente [día " & UCase(ConvDia(dia)) & "]"
                    Case "PR"
                        modo = 4
                        param1 = codigo_per
                        param2 = codigo_cac
                        titulo = "Horario registrado por Profesor [día " & UCase(ConvDia(dia)) & "]"
                End Select

                Dim obj As New ClsConectarDatos
                obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
                obj.AbrirConexion()

                Dim tbHorario As New Data.DataTable, tbPermisos As New Data.DataTable
                tbHorario = obj.TraerDataTable("ConsultarHorariosAmbiente", modo, param1, param2, dia, codigo_per)

                tbPermisos = obj.TraerDataTable("ValidarPermisoAccionesEnProcesoMatricula", "0", codigo_cac, codigo_usu, "lineahorario")
                obj.CerrarConexion()

                If tbPermisos.Rows.Count Then

                    Session("Agregar") = CBool(tbPermisos.Rows(0).Item("agregar_acr"))
                    Session("Modificar") = CBool(tbPermisos.Rows(0).Item("modificar_acr"))
                    Session("Eliminar") = CBool(tbPermisos.Rows(0).Item("eliminar_acr"))

                    If tbHorario.Rows.Count Then
                        Session("HayReg") = True
                        Me.lblTitulo.Text = titulo
                        Me.GridView1.DataSource = tbHorario
                        Me.GridView1.DataBind()
                    Else
                        Session("HayReg") = False
                        Me.lblTitulo.Attributes.Remove("Class")
                        Me.lblTitulo.Attributes.Add("Class", "alert alert-danger")
                        Me.lblTitulo.Text = "No se encontró horario registrado, según el criterio seleccionado."
                    End If

                Else
                    Session("HayReg") = False
                    Me.lblTitulo.Attributes.Remove("Class")
                    Me.lblTitulo.Attributes.Add("Class", "alert alert-danger")
                    Me.lblTitulo.Text = "Usuario no tiene permisos para modificar horarios."

                End If

            End If

        Catch ex As Exception
            Response.Write(ex.Message)
            Response.Write(ex.StackTrace)
            Response.Write(ex.Source)

        End Try
    End Sub

    Function ConvDia(ByVal digitos As String) As String
        Dim devuelve As String = ""
        Select Case digitos
            Case "LU"
                devuelve = "Lunes"
            Case "MA"
                devuelve = "Martes"
            Case "MI"
                devuelve = "Miércoles"
            Case "JU"
                devuelve = "Jueves"
            Case "VI"
                devuelve = "Viernes"
            Case "SA"
                devuelve = "Sábado"
            Case "DO"
                devuelve = "Domingo"
        End Select
        Return devuelve
    End Function

    Sub ConsultarAmbiente()
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()

        Dim tbDatos As New Data.DataTable
        tbDatos = obj.TraerDataTable("ConsultarHorariosAmbiente", 0, codigo_amb, codigo_cac, codigo_cpf, 0)
        obj.CerrarConexion()

        If tbDatos.Rows.Count Then
            Me.ddlAmbiente.DataSource = tbDatos
            Me.ddlAmbiente.DataValueField = "codigo_daa"
            Me.ddlAmbiente.DataTextField = "ambienteReal2"

        Else
            Me.lblEditar.Attributes.Remove("Class")
            Me.lblEditar.Attributes.Add("Class", "alert alert-danger")
            Me.lblEditar.Text = "No hay ambientes asignados."
        End If

        Me.ddlAmbiente.DataBind()
    End Sub
    

    Protected Sub GridView1_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridView1.RowCommand
        Dim index As Integer = Convert.ToInt32(e.CommandArgument)
        If (e.CommandName = "EditarLinea") Then
            Me.Mensaje.InnerHtml = ""
            ConsultarAmbiente()
            Me.PanelEditar.Visible = True
            Session("Xcodigo_lho") = Me.GridView1.DataKeys.Item(index).Values("codigo_lho").ToString
            Me.lblEditar.Text = "Editando Línea Horario: " & Me.GridView1.DataKeys.Item(index).Values("dia_lho").ToString & " de " & Me.GridView1.DataKeys.Item(index).Values("nombre_hor").ToString & "hrs. a " & Me.GridView1.DataKeys.Item(index).Values("horafin_lho").ToString & "hrs."
            Me.ddlDia.SelectedValue = Me.GridView1.DataKeys.Item(index).Values("dia_lho").ToString
            Me.ddlInicio.SelectedValue = Me.GridView1.DataKeys.Item(index).Values("nombre_hor").ToString
            Me.ddlFin.SelectedValue = Me.GridView1.DataKeys.Item(index).Values("horafin_lho").ToString
            Me.ddlAmbiente.Focus()
        
            difHoras.Value = CInt(Left(Me.GridView1.DataKeys.Item(index).Values("horafin_lho").ToString, 2)) - CInt(Left(Me.GridView1.DataKeys.Item(index).Values("nombre_hor").ToString, 2))
            'Response.Write(difHoras)
        Else
            Session("Xcodigo_lho") = 0
            If (e.CommandName = "EliminarLinea") Then

                Me.PanelEditar.Visible = False
                If CBool(Session("Eliminar")) = True Then
                    Try

                        Dim obj As New ClsConectarDatos
                        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
                        obj.AbrirConexion()

                        If CInt(GridView1.DataKeys(index).Values("codigo_cpf")) = codigo_cpf Then

                            If obj.TraerDataTable("Acad_EliminarHorario_Verificar", CInt(Me.GridView1.DataKeys.Item(index).Values("codigo_lho").ToString)).Rows.Count = 0 Then
                                obj.Ejecutar("EliminarHorario", CInt(Me.GridView1.DataKeys.Item(index).Values("codigo_lho").ToString), codigo_per)
                                ClientScript.RegisterStartupScript(GetType(Object), "cerrar", "<script>alert('Se ha inactivado el ambiente.'); window.close();</script>")
                            Else
                                Me.Mensaje.InnerHtml = "<div class='alert alert-danger'>No se puede eliminar porque existen docentes asignados en este horario. Coordinar con Dpto. Académico.</div>"
                            End If
                        Else
                            Me.Mensaje.InnerHtml = "<div class='alert alert-danger'>Bloqueado para eliminar horarios.</div>"
                        End If
                        obj.CerrarConexion()
                    Catch ex As Exception
                        ClientScript.RegisterStartupScript(GetType(Object), "cerrar", "<script>alert('" & ex.Message & "');</script>")
                    End Try
                Else
                    Me.Mensaje.InnerHtml = "<div class='alert alert-danger'>No tiene persmisos para eliminar horarios.</div>"
                End If
            End If
        End If
    End Sub


    ''' <summary>
    ''' EPENA 31/10/2019 ID: 28331  Modificar horarios, validar horas verano 
    ''' </summary>
    ''' <returns>true: si cumple y False si no cumple la diferencia de horas</returns>
    ''' <remarks></remarks>
    Private Function fnValidarDifHoras() As Boolean
        Try
            Dim hI As Integer
            Dim hF As Integer

            hI = CInt(Left(Me.ddlInicio.SelectedValue, 2))
            hF = CInt(Left(Me.ddlFin.SelectedValue, 2))

            difHorasMod.Value = hF - hI

            If CInt(difHorasMod.Value) <= CInt(difHoras.Value) Then
                Return True
            Else
                Me.Mensaje.InnerHtml = "<div class='alert alert-danger'>Ha superado la diferencia de horas en el bloque seleccionado. (Máx horas: " & Me.difHoras.Value & " )</div>"
                Return False
            End If

        Catch ex As Exception
            Return False
        End Try
    End Function

    Protected Sub btnGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardar.Click

        If Session("Xcodigo_lho") = 0 Then            
            Me.lblTitulo.Attributes.Remove("Class")
            Me.lblTitulo.Attributes.Add("Class", "alert alert-danger")
            Me.lblTitulo.Text = "Vuelva a ingresar"
            Exit Sub
        End If


        If fnValidarDifHoras() Then 'EPENA 31/10/2019 ID: 28331  Modificar horarios, validar horas verano 

            Dim obj As New ClsConectarDatos
            Dim tb As New Data.DataTable

            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            tb = obj.TraerDataTable("Acad_ActualizarHorario", Session("Xcodigo_lho"), Me.ddlDia.SelectedValue, Me.ddlInicio.SelectedValue, ddlFin.SelectedValue, Me.ddlAmbiente.SelectedValue, codigo_usu, codigo_cac)
            obj.CerrarConexion()
            obj = Nothing

            If tb.Rows.Count Then
                If tb.Rows(0).Item("resultado").ToString = "1" Then
                    Me.Mensaje.InnerHtml = "<div class='alert alert-danger'>No se puede actualizar porque existe cruce de horario en el ambiente seleccionado.</div>"
                ElseIf tb.Rows(0).Item("resultado").ToString = "2" Then
                    Me.Mensaje.InnerHtml = "<div class='alert alert-danger'>No se puede actualizar porque existe cruce de horario del docente.</div>"

                ElseIf tb.Rows(0).Item("resultado").ToString = "0" Then
                    Me.Mensaje.InnerHtml = "<div class='alert alert-danger'>Ha ocurrido un error [1]</div>"
                ElseIf tb.Rows(0).Item("resultado").ToString = "-1" Then
                    Me.Mensaje.InnerHtml = "<div class='alert alert-danger'>La capacidad del ambiente seleccionado es menor que las vacantes del curso programado.</div>"
                Else
                    Me.Mensaje.InnerHtml = "<div class='alert alert-success'>Horario Actualizado...</div>"
                    ClientScript.RegisterStartupScript(GetType(Object), "cerrar", "<script>alert('Horario Actualizado...'); window.close();</script>")
                    Me.GridView1.DataBind()
                    Me.PanelEditar.Visible = False

                End If
                Me.GridView1.DataBind()
            Else
                Me.Mensaje.InnerHtml = "<div class='alert alert-danger'>Ha ocurrido un error.[2]</div>"
            End If



        End If 'EPENA 31/10/2019 ID: 28331  Modificar horarios, validar horas verano 
    End Sub

    Protected Sub btnCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        Me.PanelEditar.Visible = False        
    End Sub

    Protected Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            If codigo_cpf > 0 Then               
                If codigo_cpf <> CInt(GridView1.DataKeys(e.Row.RowIndex).Values("codigo_cpf").ToString) Then
                    e.Row.Cells(1).Text = "[Bloqueado para eliminar horarios.]"
                    e.Row.Cells(1).ForeColor = Drawing.Color.Red
                    e.Row.Cells(14).Text = "[Bloqueado para editar horarios.]"
                    e.Row.Cells(14).ForeColor = Drawing.Color.Red
                End If
            End If
        End If
    End Sub
End Class
