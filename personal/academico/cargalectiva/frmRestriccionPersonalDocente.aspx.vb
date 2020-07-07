
Partial Class academico_cargalectiva_frmRestriccionPersonalDocente
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim tempMeta As New HtmlMeta
        Dim tempHead As HtmlHead = Page.Header
        With tempMeta
            .Attributes.Add("http-equiv", "X-UA-Compatible")
            .Content = "IE=edge"
        End With
        tempHead.Controls.Add(tempMeta)


        If IsPostBack = False Then
            llenarTiempos()
            'Response.Write("<meta http-equiv='X-UA-Compatible' content='IE=edge'>")
            mostrar(1)
            Dim codigo_tfu As Int16 = Request.QueryString("ctf")
            Dim codigo_usu As Integer = Request.QueryString("id")
            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            ClsFunciones.LlenarListas(Me.ddlCiclo, obj.TraerDataTable("ACAD_CicloAcademicoHorarioDisponible"), "codigo_cac", "descripcion_cac")

            obj.CerrarConexion()
            fnLoading(True)
            fnListarDocentes()
            ' response.Write("<meta http-equiv='X-UA-Compatible' content='IE=edge'>")
        End If
    End Sub

    Private Sub mostrar(ByVal tipo As Int16)
        If tipo = 1 Then
            PanelLista.Visible = True
            PanelDisponibildiad.Visible = False
        ElseIf tipo = 2 Then
            PanelLista.Visible = False
            PanelDisponibildiad.Visible = True
        ElseIf tipo = 3 Then
            PanelDisponibildiadLista.Visible = True
            PanelDisponibildiadRegistro.Visible = False
        ElseIf tipo = 4 Then
            PanelDisponibildiadLista.Visible = False
            PanelDisponibildiadRegistro.Visible = True
        End If

    End Sub

    Private Sub fnLoading(ByVal sw As Boolean)
        If sw Then
            ' Response.Write(1 & "<br>")
            Me.loading.Attributes.Remove("class")
            Me.loading.Attributes.Add("class", "hidden")
        Else
            ' Response.Write(0 & "<br>")
            Me.loading.Attributes.Remove("class")
            Me.loading.Attributes.Add("class", "")
            ' Me.loading.Attributes.Add("class", "show")
        End If
    End Sub


    Private Sub fnListarDocentes()

        ScriptManager.RegisterStartupScript(Me, Me.[GetType](), "DivLoad", "fnDivLoad('report',1000);", True)
        fnLoading(False)
        Try

            Dim obj As New ClsConectarDatos
            Dim tb As New Data.DataTable
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            ' tb = obj.TraerDataTable("acad_consolidadoxescuela_padres", "P", Me.ddlCiclo.selectedvalue, Me.ddlPlan.SelectedValue)
            tb = obj.TraerDataTable("ACAD_RestriccionPersonal_Consultar", "3", 0, 0, Me.ddlCiclo.SelectedValue, CInt(Session("id_per")), 0, "")
            If tb.Rows.Count > 0 Then
                If tb.Rows(0).Item("codigo_Ded") = 2 Or tb.Rows(0).Item("codigo_Ded") = 3 Or tb.Rows(0).Item("codigo_Ded") = 7 Then
                    Me.gData.DataSource = tb
                    Me.gData.DataBind()
                Else
                    Me.MensajeForm.InnerHtml = "<div class='alert alert-danger'>Estimado docente: Su tipo de Dedicación no le permite registrar la Disponibilidad de Horario.</div>"
                End If
            End If

            obj.CerrarConexion()
            obj = Nothing

        Catch ex As Exception
            Dim script As String = "fnMensaje('error','" & ex.Message & "'); "
            ScriptManager.RegisterStartupScript(Me, GetType(Page), "alert", script, True)
        End Try

        fnLoading(True)
    End Sub


    Protected Sub btnConsultar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnConsultar.Click
        Try

        
            If Session("id_per").ToString = "" Then
                Dim script As String = "fnMensaje('error','Su sesión ha expirado, favor de ingresar nuevamente al campus virtual.'); "
                ScriptManager.RegisterStartupScript(Me, GetType(Page), "alert", script, True)

            Else
                If fnValidadCronograma() Then
                    gData.Visible = True
                    fnListarDocentes()
                Else
                    'BLOQUEO CRONOGRAMA'
                    gData.Visible = False
                    Me.MensajeForm.InnerHtml = "<div class='alert alert-warning'>El registro de Disponibilidad Docente no se encuentra disponible. Consultar sobre el cronograma de registro con Dirección Académica</div>"
                End If
            End If
        Catch ex As Exception
            Dim script As String = "fnMensaje('error','Su sesión ha expirado, favor de ingresar nuevamente al campus virtual.'); "
            ScriptManager.RegisterStartupScript(Me, GetType(Page), "alert", script, True)
        End Try
    End Sub


    Private Function fnValidadCronograma() As Boolean
        Try

            Dim obj As New ClsConectarDatos
            Dim tb As New Data.DataTable
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            ' tb = obj.TraerDataTable("acad_consolidadoxescuela_padres", "P", Me.ddlCiclo.selectedvalue, Me.ddlPlan.SelectedValue)
            tb = obj.TraerDataTable("ConsultarCronograma", "HD", Me.ddlCiclo.SelectedValue)
            obj.CerrarConexion()
            obj = Nothing

            If tb.Rows.Count > 0 Then
                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            Return False
        End Try

    End Function



    Protected Sub gData_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles gData.PreRender
        If gData.Rows.Count > 0 Then
            gData.UseAccessibleHeader = True
            gData.HeaderRow.TableSection = TableRowSection.TableHeader
        End If

    End Sub

    Protected Sub gData_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gData.RowCommand

        Try



            Dim index As Integer = Convert.ToInt32(e.CommandArgument)
            Dim st0 As New StringBuilder
            Dim st As New StringBuilder
            Dim st2 As New StringBuilder
            Dim st3 As New StringBuilder
            fnLoading(False)
            ScriptManager.RegisterStartupScript(Me, Me.[GetType](), "DivLoad", "fnDivLoad('report',1000);", True)
            If (e.CommandName = "Registrar") Then
                Me.lidocente.InnerHtml = "<b>Docente: </b>" & gData.DataKeys(index).Values("Personal").ToString
                Me.lidepartamento.InnerHtml = "<b>Departamento: </b>" & gData.DataKeys(index).Values("departamento").ToString
                Me.lisemestre.InnerHtml = "<b>Semestre: </b>" & gData.DataKeys(index).Values("cicloacademico").ToString
                Me.lidescripcion.InnerHtml = "<b>Descripcion: </b>" & gData.DataKeys(index).Values("Descripcion_Ded").ToString
                mostrar(2)
                'Agregar valores en session
                Session.Add("RESTHOR_docente", gData.DataKeys(index).Values("codigo_Per"))
                Session.Add("RESTHOR_cac", gData.DataKeys(index).Values("codigo_cac"))
                ConsultarDetalle()
            End If
            fnLoading(True)
        Catch ex As Exception
            Dim script As String = "fnMensaje('error','" & ex.Message & "'); "
            ScriptManager.RegisterStartupScript(Me, GetType(Page), "alert", script, True)
        End Try

    End Sub

    Protected Sub btnCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        mostrar(1)



        Session.Remove("RESTHOR_docente")
        Session.Remove("RESTHOR_cac")




    End Sub

    Protected Sub btnConsultar2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnConsultar2.Click
        Try


            If Session("perlogin").ToString() = "" Then
                Dim script As String = "fnMensaje('error','Verificar tu sessión');"
                ScriptManager.RegisterStartupScript(Me, GetType(Page), "alert", script, True)
            Else
                ConsultarDetalle()
            End If
        Catch ex As Exception
            Dim script As String = "fnMensaje('error','Verificar tu sessión');"
            ScriptManager.RegisterStartupScript(Me, GetType(Page), "alert", script, True)
        End Try
    End Sub

    Private Sub ConsultarDetalle()
        Try

            Dim obj As New ClsConectarDatos
            Dim tb As New Data.DataTable

            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            ' tb = obj.TraerDataTable("acad_consolidadoxescuela_padres", "P", Me.ddlCiclo.selectedvalue, Me.ddlPlan.SelectedValue)

            tb = obj.TraerDataTable("ACAD_RestriccionPersonal_Consultar", "2", 0, 0, Session("RESTHOR_cac"), Session("RESTHOR_docente"), 0, "")

            Me.gDetalle.DataSource = tb

            Me.gDetalle.DataBind()
            obj.CerrarConexion()
            obj = Nothing
        Catch ex As Exception
            Dim script As String = "fnMensaje('error','" & ex.Message & "'); "
            ScriptManager.RegisterStartupScript(Me, GetType(Page), "alert", script, True)
        End Try
    End Sub

    Protected Sub btnAgregar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAgregar.Click
        BotonNuevo()

    End Sub



    Sub BotonNuevo()
        Session.Add("RESTHOR_codigo", 0)
        Me.Registro.Visible = True
        Me.gDetalle.Enabled = False
        Me.btnConsultar.Enabled = False
        Me.btnAgregar.Enabled = False
        Me.btnConsultar2.Enabled = False
        Me.btnCancelar.Enabled = False
        Me.tdRegistro.InnerHtml = "Nuevo Registro"
        'Me.ddpPuede.Focus()
        Me.ddpDia.Focus()
    End Sub

    Sub BotonEditar(ByVal codigo As Integer)
        Session.Add("RESTHOR_codigo", codigo)
        Me.Registro.Visible = True
        Me.gDetalle.Enabled = False
        Me.btnConsultar.Enabled = False
        Me.btnAgregar.Enabled = False
        Me.btnConsultar2.Enabled = False
        Me.btnCancelar.Enabled = False
        Me.tdRegistro.InnerHtml = "Nuevo Registro"
        'Me.ddpPuede.Focus()
        Me.ddpDia.Focus()
    End Sub

    Protected Sub btnCancelarDetalle_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelarDetalle.Click
        BotonCancelar()
    End Sub
    Sub BotonCancelar()
        Me.btnConsultar.Enabled = True
        Me.Registro.Visible = False
        Me.gDetalle.Enabled = True
        Me.btnAgregar.Enabled = True
        Me.tdRegistro.InnerHtml = String.Empty
        Me.btnConsultar2.Enabled = True
        Me.btnCancelar.Enabled = True
        Session.Remove("RESTHOR_codigo")

        limpiar()
        'lstGrupo.HeaderRow.TableSection = TableRowSection.TableHeader
    End Sub
    Sub limpiar()
        'Me.ddpPuede.SelectedValue = ""
        Me.ddpDia.SelectedValue = ""
        Me.ddpHoraI.SelectedValue = ""
        Me.ddpHoraF.SelectedValue = ""
        Me.tdRegistro.InnerHtml = String.Empty
    End Sub

    Sub llenarTiempos()
        Dim i As Integer = 1

        For i = 6 To 23
            ddpHoraI.Items.Add(New ListItem(String.Format("{0:00}", i), String.Format("{0:00}", i)))
            ddpHoraF.Items.Add(New ListItem(String.Format("{0:00}", i), String.Format("{0:00}", i)))
        Next


    End Sub

    Protected Sub btnGrabarDetalle_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGrabarDetalle.Click

        Try
            If Session("perlogin").ToString() = "" Then
                Dim script As String = "fnMensaje('error','Verificar tu sessión');"
                ScriptManager.RegisterStartupScript(Me, GetType(Page), "alert", script, True)
            Else
                GuardarDetalle()
            End If
        Catch ex As Exception
            Dim script As String = "fnMensaje('error','Verificar tu sessión');"
            ScriptManager.RegisterStartupScript(Me, GetType(Page), "alert", script, True)
        End Try


    End Sub

    Private Sub GuardarDetalle()

        Try
            Dim tipo As String = ""
            Dim codigo_rper As String = ""
            Dim codigo_cac As Integer = 0
            Dim codigo_per As Integer = 0
            Dim puede As String = ""
            Dim dias As String = ""
            Dim horaI As String = ""
            Dim horaF As String = ""


            If fnValidarRegistro() Then


                Dim lblResultado As Boolean
                codigo_rper = Session("RESTHOR_codigo")

                If codigo_rper = 0 Then
                    tipo = "I"
                Else
                    tipo = "A"
                End If



                codigo_cac = Session("RESTHOR_cac")
                codigo_per = Session("RESTHOR_docente")
                puede = "S" ' ddpPuede.SelectedValue
                dias = ddpDia.SelectedValue
                horaI = ddpHoraI.SelectedValue
                horaF = ddpHoraF.SelectedValue



                Dim obj As New ClsConectarDatos
                obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
                obj.AbrirConexion()
                lblResultado = obj.Ejecutar("ACAD_RestriccionPersonal_Registrar", tipo, codigo_rper, codigo_cac, codigo_per, puede, dias, horaI, horaF, Session("perlogin"))
                obj.CerrarConexion()

                If lblResultado Then

                    Dim script As String = "fnMensaje('success','Se ha registrado su Disponibilidad de Horario satisfactoriamente.'); "
                    ScriptManager.RegisterStartupScript(Me, GetType(Page), "alert", script, True)
                    BotonCancelar()
                    ConsultarDetalle()

                Else
                    Dim script As String = "fnMensaje('error','Error al registrar horario'); "
                    ScriptManager.RegisterStartupScript(Me, GetType(Page), "alert", script, True)
                End If


            End If


        Catch ex As Exception
            Dim script As String = "fnMensaje('error','" & ex.Message & "'); "
            ScriptManager.RegisterStartupScript(Me, GetType(Page), "alert", script, True)
        End Try

    End Sub
    Public Function fnValidarRegistro() As Boolean
        Try

            If Session("perlogin").ToString() = "" Then

                Dim script As String = "fnMensaje('error','Verificar sessión');"
                ScriptManager.RegisterStartupScript(Me, GetType(Page), "alert", script, True)
                Return False
            End If

            'If ddpPuede.SelectedValue = "" Then
            '    Dim script As String = "fnMensaje('warning','Seleccione si puede dictar el curso'); fnFoco('ddpPuede')"
            '    ScriptManager.RegisterStartupScript(Me, GetType(Page), "alert", script, True)
            '    Return False
            'End If

            If ddpDia.SelectedValue = "" Then
                Dim script As String = "fnMensaje('warning','Seleccione dia'); fnFoco('ddpDia')"
                ScriptManager.RegisterStartupScript(Me, GetType(Page), "alert", script, True)
                Return False
            End If

            If ddpHoraI.SelectedValue = "" Then
                Dim script As String = "fnMensaje('warning','Seleccione Hora Inicio'); fnFoco('ddpHoraI')"
                ScriptManager.RegisterStartupScript(Me, GetType(Page), "alert", script, True)
                Return False
            End If
            If ddpHoraF.SelectedValue = "" Then
                Dim script As String = "fnMensaje('warning','Seleccione Hora Fin'); fnFoco('ddpHoraF')"
                ScriptManager.RegisterStartupScript(Me, GetType(Page), "alert", script, True)
                Return False
            End If


            If CInt(ddpHoraI.SelectedValue) >= CInt(ddpHoraF.SelectedValue) Then

                Dim script As String = "fnMensaje('warning','Hora de inicio debe ser menor que hora fin'); fnFoco('ddpHoraI')"
                ScriptManager.RegisterStartupScript(Me, GetType(Page), "alert", script, True)
                Return False
            End If
            Return True

        Catch ex As Exception

            Return False
        End Try

    End Function

    Protected Sub gDetalle_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gDetalle.RowCommand
        Try
            Dim index As Integer = Convert.ToInt32(e.CommandArgument)
            Dim codigo_rper As Integer = 0

            If (e.CommandName = "Editar") Then

                codigo_rper = gDetalle.DataKeys(index).Values("codigo_rper")


                BotonEditar(codigo_rper)
                tdRegistro.InnerHtml = "Editar Registro"

                'Me.ddpPuede.SelectedValue = gDetalle.DataKeys(index).Values("puede").ToString()
                Me.ddpDia.SelectedValue = gDetalle.DataKeys(index).Values("dia").ToString()
                Me.ddpHoraI.SelectedValue = gDetalle.DataKeys(index).Values("horaInicio").ToString()
                Me.ddpHoraF.SelectedValue = gDetalle.DataKeys(index).Values("horaFin").ToString()



            ElseIf (e.CommandName = "Eliminar") Then
                Dim obj As New ClsConectarDatos
                Dim lblResultado As Boolean
                codigo_rper = gDetalle.DataKeys(index).Values("codigo_rper")
                'Response.Write("id: " & Desencriptar(txtid.Value))
                obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
                obj.AbrirConexion()
                lblResultado = obj.Ejecutar("ACAD_RestriccionPersonal_Registrar", "E", codigo_rper, 0, 0, "", "", "", "", Session("perlogin"))
                obj.CerrarConexion()

                If lblResultado Then
                    Dim script As String = "fnMensaje('success','Horario eliminado satisfactoriamente'); "
                    ScriptManager.RegisterStartupScript(Me, GetType(Page), "alert", script, True)
                    ConsultarDetalle()
                Else
                    Dim script As String = "fnMensaje('error','Error al eliminar horario'); "
                    ScriptManager.RegisterStartupScript(Me, GetType(Page), "alert", script, True)
                End If
            End If

            ' gvCronograma.HeaderRow.TableSection = TableRowSection.TableHeader
        Catch ex As Exception
            Dim script As String = "fnMensaje('error','" & ex.Message & "'); "
            ScriptManager.RegisterStartupScript(Me, GetType(Page), "alert", script, True)

        End Try
    End Sub

End Class
