
Partial Class academico_encuesta_EncuestaEvaluacionDocente_ResponderEncuestaAuto
    Inherits System.Web.UI.Page
    Public Enum MessageType
        Success
        [Error]
        Info
        Warning
    End Enum
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then

            If CInt(Session("id_per")) = 0 Then
                ShowMessage("La sesión ha expirado, por favor volver a ingresar al campus virtual", MessageType.Error)
                Me.panelCabecera.Visible = False
                Me.panelCursos.Visible = False
                Me.PanelRpta.Visible = False
            Else
                Session("eva_Tipo") = Request.QueryString("tipoEval").ToString
                ConsultarAcceso()
                PanelRpta.Visible = False
            End If


        End If
    End Sub
    Sub llenarEscala()
        Me.GridView1.DataSource = Nothing
        Dim tbEscla As New Data.DataTable
        tbEscla.Columns.Add("1")
        tbEscla.Columns.Add("2")
        tbEscla.Columns.Add("3")
        tbEscla.Columns.Add("4")
        tbEscla.Columns.Add("5")
        tbEscla.Rows.Add("Nunca", "La mayoría de veces", "Algunas veces sí", "La mayoría de veces si", "Siempre")
        Me.GridView1.DataSource = tbEscla
        Me.GridView1.DataBind()
    End Sub
    Sub ConsultarAcceso()
        Try
            Dim dt As New Data.DataTable
            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            dt = obj.TraerDataTable("EAD_ConsultarEvaluacionVigenteXTipo", Session("eva_Tipo"))
            Session("eva_codigocev") = dt.Rows(0).Item("codigo_cev").ToString
            obj.CerrarConexion()

            If dt.Rows.Count > 0 Then
                obj.AbrirConexion()
                dt = obj.TraerDataTable("EVAL_ConsultarAcceso", Session("eva_Tipo"), CInt(Session("id_per")))
                obj.CerrarConexion()

                If dt.Rows.Count > 0 Then
                    Cargarfoto()
                    llenarEscala()

                    Me.lblSemestre.Text = dt.Rows(0).Item("descripcion_cac")
                    Me.lblDocente.Text = dt.Rows(0).Item("Personal")
                    Me.lblDpto.Text = dt.Rows(0).Item("dpto")
                    Me.lblDedicacion.Text = dt.Rows(0).Item("descripcion_Ded")

                    CargarCursos()

                    gvPreguntas.DataSource = Nothing
                    Me.gvPreguntas.DataBind()
                Else
                    ShowMessage("No se encontraron cursos para realizar la autoevaluación.", MessageType.Warning)
                    Me.panelCabecera.Visible = False
                    Me.panelCursos.Visible = False
                    Me.PanelRpta.Visible = False
                    Exit Sub
                End If
            Else
                ShowMessage("La autoevaluación no está disponible.", MessageType.Warning)
                Me.panelCabecera.Visible = False
                Me.panelCursos.Visible = False
                Me.PanelRpta.Visible = False
                Exit Sub
            End If
        Catch ex As Exception
        End Try
    End Sub
    Sub CargarCursos()
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString

        obj.AbrirConexion()
        dt = obj.TraerDataTable("EVAL_ConsultarAcceso", "C", CInt(Session("id_per")), Session("eva_Tipo"))
        obj.CerrarConexion()
        If dt.Rows.Count Then
            Me.gvCursos.DataSource = dt

        Else
            Me.gvCursos.DataSource = Nothing

        End If
        Me.gvCursos.DataBind()
    End Sub
    Sub Cargarfoto()
        Dim strFoto As Boolean = False
        Dim strRutaFoto As String = Server.MapPath("..\..\..\imgpersonal\" & CInt(Session("id_per")) & ".jpg")
        strFoto = (System.IO.File.Exists(strRutaFoto))
        If strFoto = True Then
            Me.imagePer.ImageUrl = "..\imgpersonal\" & CInt(Session("id_per")) & ".jpg"
        Else
            Me.imagePer.ImageUrl = "https://intranet.usat.edu.pe/campusvirtual/personal/imgpersonal/fotovacia3.png"
        End If
    End Sub
    Sub CargarPreguntas()
        Try
            If CInt(Session("id_per")) = 0 Then
                ShowMessage("La sesión ha expirado, por favor volver a ingresar al campus virtual", MessageType.Error)
                Me.panelCabecera.Visible = False
                Me.panelCabecera.Visible = False
                Me.PanelRpta.Visible = False
            Else
                gvPreguntas.DataSource = Nothing
                Me.gvPreguntas.DataBind()
                Dim dt As New Data.DataTable
                Dim obj As New ClsConectarDatos
                obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
                obj.AbrirConexion()
                dt = obj.TraerDataTable("EAD_ConsultarEvaluacionDesempenio", Session("eva_Tipo"), Session("eva_codigocev"), "C")
                obj.CerrarConexion()
                If dt.Rows.Count Then
                    Me.gvPreguntas.DataSource = dt
                Else
                    Me.gvPreguntas.DataSource = Nothing
                End If
                Me.gvPreguntas.DataBind()
            End If
        Catch ex As Exception

        End Try

    End Sub
    Function validar() As Boolean

        Dim nropreguntas As Integer = 0
        
        nropreguntas = 24


        Dim respuesta_eva As Integer = 0
        Dim respondio As Integer = 0
        For Each row As GridViewRow In gvPreguntas.Rows
            respuesta_eva = 0

            Dim rbUno As Boolean = CType(row.FindControl("rbUno"), RadioButton).Checked
            Dim rbDos As Boolean = CType(row.FindControl("rbDos"), RadioButton).Checked
            Dim rbTres As Boolean = CType(row.FindControl("rbTres"), RadioButton).Checked
            Dim rbCuatro As Boolean = CType(row.FindControl("rbCuatro"), RadioButton).Checked
            Dim rbCinco As Boolean = CType(row.FindControl("rbCinco"), RadioButton).Checked



            If rbUno Then
                respuesta_eva = 1
            ElseIf rbDos Then
                respuesta_eva = 1
            ElseIf rbTres Then
                respuesta_eva = 1
            ElseIf rbCuatro Then
                respuesta_eva = 1
            ElseIf rbCinco Then
                respuesta_eva = 1
            End If


            If respuesta_eva > 0 Then
                respondio += 1
            End If
        Next

        If respondio = nropreguntas Then
            Return True
        ElseIf respondio = 0 Then
            Return False
        End If
    End Function
    Protected Sub ShowMessage(ByVal Message As String, ByVal type As MessageType)
        Page.RegisterStartupScript("Mensaje", "<script>ShowMessage('" & Message & "','" & type & "');</script>")
    End Sub
    Protected Sub btnGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardar.Click
        registrar()

    End Sub
    Protected Sub btnGuardar2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardar2.Click
        registrar()

    End Sub
    Sub registrar()



        If validar() Then
            Dim cod_per As Integer
            cod_per = CInt(Session("id_per"))
            If cod_per > 0 Then
                Dim objcnx As New ClsConectarDatos
                objcnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
                Try

                    Dim codigo_eva As Integer
                    Dim codigo_eed As Integer
                    Dim tabla As Data.DataTable
                    Dim respuesta_eva As Integer = 0
                    Dim datos As New Data.DataTable
                    Dim rbUno As Boolean = False
                    Dim rbDos As Boolean = False
                    Dim rbTres As Boolean = False
                    Dim rbCuatro As Boolean = False
                    Dim rbCinco As Boolean = False

                    objcnx.IniciarTransaccion()

                    tabla = objcnx.TraerDataTable("EAD_AgregarEncuestaEvaluacionDDV2", cod_per, Me.gvCursos.DataKeys.Item(Me.gvCursos.SelectedIndex).Values("codigo_cup").ToString(), cod_per, Session("eva_Tipo"), Session("eva_codigocev"), 0, 0, Session("origen"))

                    If tabla.Rows.Count > 0 Then

                        For i As Integer = 0 To tabla.Rows.Count - 1

                            codigo_eed = tabla.Rows(i).Item("codigo_eed")
                            For Each row As GridViewRow In gvPreguntas.Rows

                                codigo_eva = gvPreguntas.DataKeys.Item(row.RowIndex).Values("codigo_eva")
                                rbUno = CType(row.FindControl("rbUno"), RadioButton).Checked
                                rbDos = CType(row.FindControl("rbDos"), RadioButton).Checked
                                rbTres = CType(row.FindControl("rbTres"), RadioButton).Checked
                                rbCuatro = CType(row.FindControl("rbCuatro"), RadioButton).Checked
                                rbCinco = CType(row.FindControl("rbCinco"), RadioButton).Checked

                                If rbUno Then
                                    respuesta_eva = 1
                                ElseIf rbDos Then
                                    respuesta_eva = 2
                                ElseIf rbTres Then
                                    respuesta_eva = 3
                                ElseIf rbCuatro Then
                                    respuesta_eva = 4
                                ElseIf rbCinco Then
                                    respuesta_eva = 5
                                End If
                                objcnx.Ejecutar("EAD_AgregarRespuestaEvaluacion", codigo_eva, respuesta_eva, codigo_eed)

                            Next
                        Next
                        objcnx.TerminarTransaccion()
                    End If
                    If codigo_eed = 0 Then
                        objcnx.AbortarTransaccion()
                    Else
                        ShowMessage("Se registraron las respuestas correctamente.", MessageType.Success)
                    End If
                    objcnx = Nothing

                    CargarCursos()
                    Me.gvPreguntas.DataSource = Nothing
                    Me.gvPreguntas.DataBind()
                    PanelRpta.Visible = False

                Catch ex As Exception
                    objcnx.AbortarTransaccion()
                    ShowMessage("Ha ocurrido un error al registrar las respuestas", MessageType.Error)
                End Try
            Else
                ShowMessage("La sesión ha expirado, por favor volver a ingresar al campus virtual", MessageType.Error)
                Me.panelCabecera.Visible = False
                Me.panelCursos.Visible = False
                Me.PanelRpta.Visible = False
            End If
        Else
            ShowMessage("Por favor complete todas las preguntas", MessageType.Warning)
        End If
    End Sub
  
    Protected Sub gvPreguntas_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvPreguntas.PreRender
        If gvPreguntas.Rows.Count > 0 Then
            gvPreguntas.UseAccessibleHeader = True
            gvPreguntas.HeaderRow.TableSection = TableRowSection.TableHeader
        End If
    End Sub


    Protected Sub gvPreguntas_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvPreguntas.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then

                Dim ddlUno As RadioButton, ddlDos As RadioButton, ddlTres As RadioButton, ddlCuatro As RadioButton, ddlCinco As RadioButton
                ddlUno = TryCast(e.Row.FindControl("rbUno"), RadioButton)
                ddlDos = TryCast(e.Row.FindControl("rbDos"), RadioButton)
                ddlTres = TryCast(e.Row.FindControl("rbTres"), RadioButton)
                ddlCuatro = TryCast(e.Row.FindControl("rbCuatro"), RadioButton)
                ddlCinco = TryCast(e.Row.FindControl("rbCinco"), RadioButton)

                If Request.Browser.IsMobileDevice Then
                    ddlUno.CssClass = "radiorespuesta"
                    ddlDos.CssClass = "radiorespuesta"
                    ddlTres.CssClass = "radiorespuesta"
                    ddlCuatro.CssClass = "radiorespuesta"
                    ddlCinco.CssClass = "radiorespuesta"
                    Session("origen") = 1
                Else
                    ddlUno.CssClass = "radionormal"
                    ddlDos.CssClass = "radionormal"
                    ddlTres.CssClass = "radionormal"
                    ddlCuatro.CssClass = "radionormal"
                    ddlCinco.CssClass = "radionormal"
                    Session("origen") = 0
                End If
            End If

        Catch ex As Exception

        End Try
        
    End Sub

    Protected Sub gvCursos_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvCursos.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim Fila As Data.DataRowView
            Fila = e.Row.DataItem
            e.Row.Attributes.Add("OnClick", "javascript:__doPostBack('gvCursos','Select$" & e.Row.RowIndex & "')")
            If (e.Row.Cells(3).Text).ToString <> "PENDIENTE" Then
                e.Row.Cells(4).Text = ""
            End If
        End If
    End Sub

    Protected Sub gvCursos_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvCursos.SelectedIndexChanged
        Try
            PanelRpta.Visible = True
            CargarPreguntas()
            lblNombrecurso.Text = Me.gvCursos.SelectedRow.Cells(2).Text & " (" & Me.gvCursos.SelectedRow.Cells(1).Text & ")"
            gvCursos_RowDataBound(sender, e)
        Catch ex As Exception

        End Try

    End Sub
End Class
