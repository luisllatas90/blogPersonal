Partial Class proponente_contenido
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.lblUsuario.Text = Request.QueryString("id")
        txtUsuario.Value = Request.QueryString("id")

        Call MostrarInstancias()

        Dim ObjCnx As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
        If ObjCnx.TraerValor("PRP_VerificarSecretariasConsejos_v1", Request.QueryString("id"), 0) = True Then
            cmdConforme.Text = "          Derivar"
        End If
        cmdDerivar.Visible = False


        If Me.ddlInstanciaRevision.SelectedValue = "P" And Me.ddlInstanciaPropuesta.SelectedValue = "P" Then
            Me.cmdEnviar.Visible = True
        Else
            Me.cmdEnviar.Visible = False
        End If

        If ddlInstanciaRevision.SelectedIndex = 5 Then
            ddlInstanciaPropuesta.SelectedIndex = 0
        End If

        If ddlInstanciaRevision.SelectedIndex = 0 Then
            ddlInstanciaPropuesta.Enabled = True
        Else
            ddlInstanciaPropuesta.Enabled = False
            ddlInstanciaPropuesta.SelectedIndex = 0
        End If

        If IsPostBack = False Then
            Call CargarComboInstanciaRevision()
            If Me.ddlInstanciaRevision.SelectedValue = "P" And Me.ddlInstanciaPropuesta.SelectedValue = "P" Then
                Me.cmdEnviar.Visible = True
            Else
                Me.cmdEnviar.Visible = False
            End If
        End If

        If Me.ddlInstanciaRevision.SelectedValue = "P" And Me.ddlInstanciaPropuesta.SelectedValue = "P" And ddlEstadoRevision.SelectedValue = "P" Then
            Me.cmdConforme.Visible = False
            Me.cmdObservado.Visible = False
            Me.cmdNoConforme.Visible = False
        Else
            Me.cmdConforme.Visible = True
            Me.cmdObservado.Visible = True
            Me.cmdNoConforme.Visible = True
        End If

        If ddlInstanciaRevision.SelectedIndex = 0 Then
            ddlInstanciaPropuesta.Enabled = True
            ddlEstadoRevision.Enabled = False
        Else
            ddlInstanciaPropuesta.Enabled = False
            ddlInstanciaPropuesta.SelectedIndex = 0
            ddlEstadoRevision.Enabled = True
        End If

        If Request.QueryString("id") = "3941" Then
            ddlInstanciaRevision.Enabled = False
            Me.cmdConforme.Visible = False
            Me.cmdObservado.Visible = False
            Me.cmdNoConforme.Visible = False
        End If

        'Response.Write("PRP_ConsultarPropuesta_v2 '" + ddlInstanciaRevision.SelectedValue + "', " + Request.QueryString("id") + ", 'P', '" + _
        '               ddlInstanciaPropuesta.SelectedValue + "', '" + ddlEstadoRevision.SelectedValue + "'")



    End Sub

    Sub CargarComboInstanciaRevision()
        Dim ObjCnx As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
        ''Llenar Combo Propuestas 
        Dim dttpropuesta As New Data.DataTable
        dttpropuesta = ObjCnx.TraerDataTable("PRP_ListarInstancias_v2", Request.QueryString("id"))
        ClsFunciones.LlenarListas(Me.ddlInstanciaRevision, dttpropuesta, "codigo", "nombre")
    End Sub

    Private Sub MostrarInstancias()
        Dim idUsu As Integer
        idUsu = Request.QueryString("id")
        If Me.ddlInstanciaRevision.SelectedValue = "P" Then
            Me.lblEtiquetaInstancia.Visible = True
            Me.lblEtiquetaEstado.Visible = False
            ' Botones de calificación
            Me.cmdConforme.Visible = False
            Me.cmdObservado.Visible = False
            Me.cmdNoConforme.Visible = False
        Else
            Me.lblEtiquetaInstancia.Visible = False
            Me.lblEtiquetaEstado.Visible = True
            ' Botones de calificación
            Me.cmdConforme.Visible = True
            Me.cmdObservado.Visible = True
            Me.cmdNoConforme.Visible = True
        End If
    End Sub

    Protected Sub cmdNuevo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdNuevo.Click
        Try
            Dim idUsu As Integer
            idUsu = Request.QueryString("id")

            Dim dtt As New Data.DataTable
            Dim ObjCnx As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)

            dtt = ObjCnx.TraerDataTable("PRO_listaProyectosPoa", idUsu, 0)
            If dtt.Rows.Count = 0 Then
                Response.Write("<script>alert('No se han Registrado Programas/Proyectos con este Usuario')</script>")
                Return
            End If

            Response.Redirect("nuevapropuesta_POA_v2.aspx?idUsu=" & idUsu)
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub dgvPropuestas_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles dgvPropuestas.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim fila As Data.DataRowView
                fila = e.Row.DataItem
                e.Row.Attributes.Add("id", "" & fila.Row("codigo_prp").ToString & "")
                e.Row.Attributes.Add("OnMouseOver", "Resaltar(1,this,'S')")
                e.Row.Attributes.Add("OnMouseOut", "Resaltar(0,this,'S')")

                Dim dtt As New Data.DataTable
                Dim rpta As String = "NO"
                If ddlInstanciaPropuesta.SelectedValue = "F" Or ddlInstanciaPropuesta.SelectedValue = "A" Then
                    Dim ObjCnx As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
                    dtt = ObjCnx.TraerDataTable("PRP_ConsultarEspecialista", fila.Row("codigo_prp").ToString)
                    If dtt.Rows.Count > 0 Then
                        rpta = dtt.Rows(0).Item("rpta").ToString()
                    End If
                Else
                    If Me.ddlInstanciaPropuesta.SelectedValue = "D" Then
                        Dim ObjCnx As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
                        dtt = ObjCnx.TraerDataTable("PRP_VerificarProponenteDirector", fila.Row("codigo_prp").ToString)
                        If dtt.Rows.Count > 0 Then
                            rpta = dtt.Rows(0).Item("rpta").ToString()
                        End If
                    End If
                End If

                Dim InstanciaPropuesta As String = IIf(ddlInstanciaPropuesta.SelectedValue = "P", "SI", "NO")
                Dim estadoConforme As String = IIf(ddlEstadoRevision.SelectedValue = "C", "SI", "NO")

                'Response.Write("<script>alert('" & InstanciaPropuesta & "')</script>")
                'Response.Write("<script>alert('" & estadoConforme & "')</script>")

                e.Row.Attributes.Add("OnClick", "HabilitarBoton('M',this,'" + rpta + "','" + estadoConforme + "','" & InstanciaPropuesta & "')")
                e.Row.Attributes.Add("Class", "Sel")
                e.Row.Attributes.Add("Typ", "Sel")
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    'Protected Sub cmdModificar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdModificar.Click

    '    'Dim idUsu As Integer
    '    'idUsu = Request.QueryString("id")

    '    'Response.Write("Hotel = " & idUsu)
    '    ''propuestas2/proponente/contenido_POA_v2.aspx?id=234&instancia=F

    '    'Response.Write("<script>alert('" & "nuevapropuesta_POA_v2.aspx?idUsu=" & idUsu & "&codigo_prp=" & Me.txtelegido.Value & "&accion=M&instancia=" & Me.ddlInstanciaPropuesta.SelectedValue & "')</script>")
    '    'Response.Redirect("nuevapropuesta_POA_v2.aspx?idUsu=" & idUsu & "&codigo_prp=" & Me.txtelegido.Value & "&accion=M&instancia=" & Me.ddlInstanciaPropuesta.SelectedValue)

    'End Sub

    Protected Sub ddlInstanciaRevision_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlInstanciaRevision.SelectedIndexChanged
        Me.txtelegido.Value = ""
        MostrarInstancias()
        Call wf_validarBtnModificar()
    End Sub

    Protected Sub cmdConforme_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdConforme.Click
        Try
            Dim ObjCnx As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
            Dim Instancia As String = ""
            If Me.ddlInstanciaRevision.SelectedValue <> "P" And Me.ddlInstanciaRevision.SelectedValue <> "D" And Me.ddlInstanciaRevision.SelectedValue <> "F" And Me.ddlInstanciaRevision.SelectedValue <> "K" And Me.ddlInstanciaRevision.SelectedValue <> "A" Then
                Instancia = "I"
            Else
                Instancia = Me.ddlInstanciaRevision.SelectedValue
            End If

            ''Response.Write("PRP_CalificarPropuestas_v3 " & Request.QueryString("id") & "," & Me.txtelegido.Value & ",'" & Instancia & "','" & Instancia & "','" & "C'")
            '''''ObjCnx.Ejecutar("PRP_CalificarPropuestas_v2", Request.QueryString("id"), Me.txtelegido.Value, Instancia, Instancia, "C")
            ObjCnx.Ejecutar("PRP_CalificarPropuestas_v3", Request.QueryString("id"), Me.txtelegido.Value, Instancia, Instancia, "C")


            '''''Response.Write("PRP_ConsultarInvolucradosPendientesRectorado_v2 '" & Me.txtelegido.Value & "'")
            '''''ObjCnx.Ejecutar("PRP_ConsultarInvolucradosPendientesRectorado_v2", Me.txtelegido.Value)

            '''''ObjCnx.Ejecutar("PRP_ActualizarAprobacion", Me.txtelegido.Value)

            dgvPropuestas.DataBind()

            ''HCano 12-03-2019
            'If Me.ddlInstanciaRevision.SelectedValue = "D" Then
            '    Dim dt As New Data.DataTable
            '    dt = ObjCnx.TraerDataTable("ListaRevisoresPropuestas", Me.txtelegido.Value)
            '    EnviarCorreo(dt)
            'End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub


    Private Sub EnviarCorreo(ByVal dt As Data.DataTable)

        Dim objemail As New ClsMail
        Dim receptor, AsuntoCorreo As String
        Dim mensaje As String = ""
        Dim descripcion As String = ""


        AsuntoCorreo = "Revisión de módulo de Propuestas"

        For i As Integer = 0 To dt.Rows.Count - 1

            descripcion = ""

            descripcion += "Estimado Sr(a): <b>" + dt.Rows(i).Item("Personal").ToString + "</b>"
            descripcion += "<br><br> La Propuesta <b>" + dt.Rows(i).Item("propuesta").ToString + "</b>"
            descripcion += ", del área <b>" + dt.Rows(i).Item("area").ToString + "</b> ha sido enviada para su revisión, favor sírvase a evaluarla.<br><br>"
            descripcion += "Atentamente<br><br>"
            descripcion += "Campus Virtual"

            receptor = "hcano@usat.edu.pe"
            'receptor = dt.Rows(i).Item("email")

            mensaje = ""

            mensaje = mensaje + "<html><head><meta http-equiv='Content-Type' content='text/html; charset=iso-8859-1' />"
            mensaje = mensaje + "<title>Evaluación de Propuestas</title>"
            mensaje = mensaje + "<style type='text/css'>.usat { font-family:Humnst777 Lt BT;font-size:14px;} "
            mensaje = mensaje + ".bolsa{color:#F1132A;font-family:Calibri;font-size: 13px;font-weight: 500;}</style></head>"
            mensaje = mensaje + "<body>"
            mensaje = mensaje + "<div style='text-align:left;width:100%'>"
            mensaje = mensaje + "<table border='0' width='90%' cellpadding='0' cellspacing='0'><tr><td>"
            mensaje = mensaje + "<div style='width:70%;margin:0 auto;text-align:left;'><div class='usat'>" + descripcion + "</div></div>"
            mensaje = mensaje + "</td></tr></tbody></table>"
            mensaje = mensaje + "</div></body></html>"

            objemail.EnviarMail("campusvirtual@usat.edu.pe", "Revisión de módulo de Propuestas", receptor, AsuntoCorreo, mensaje, True)
        Next
    End Sub


    Protected Sub cmdObservado_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdObservado.Click
        Try
            Dim ObjCnx As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
            ObjCnx.Ejecutar("PRP_CalificarPropuestas_v3", Request.QueryString("id"), Me.txtelegido.Value, Me.ddlInstanciaRevision.SelectedValue, Me.ddlInstanciaRevision.SelectedValue, "O")
            'ObjCnx.Ejecutar("PRP_CalificarPropuestas_v3", Request.QueryString("id"), Me.txtelegido.Value, Instancia, Instancia, "C")

            dgvPropuestas.DataBind()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub cmdNoConforme_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdNoConforme.Click
        Try
            Dim ObjCnx As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
            'ObjCnx.Ejecutar("PRP_CalificarPropuestas_v2", Request.QueryString("id"), Me.txtelegido.Value, Me.ddlInstanciaRevision.SelectedValue, Me.ddlInstanciaRevision.SelectedValue, "N")
            ObjCnx.Ejecutar("PRP_CalificarPropuestas_v3", Request.QueryString("id"), Me.txtelegido.Value, Me.ddlInstanciaRevision.SelectedValue, Me.ddlInstanciaRevision.SelectedValue, "N")
            dgvPropuestas.DataBind()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub cmdDerivar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdDerivar.Click
        'Try
        '    Dim ObjCnx As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)

        '    Response.Write("PRP_DerivarPropuesta_v2 " & Me.txtelegido.Value)

        '    'ObjCnx.Ejecutar("PRP_DerivarPropuesta_v2", Me.txtelegido.Value)
        '    dgvPropuestas.DataBind()
        'Catch ex As Exception
        '    Response.Write(ex.Message)
        'End Try
    End Sub

    Protected Sub cmdModificar0_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdModificar0.Click
        Response.Redirect("../ayuda/faq.htm")
    End Sub

    Protected Sub cmdEnviar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdEnviar.Click
        Try
            cmdEnviar.Enabled = False
            If Me.txtelegido.Value = "" Then
                Response.Write("<H3>SELECCIONE UNA PROPUESTA PARA ENVIAR</H3>")
            Else
                Dim ObjCnx As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
                'Verificar que aún el Proponente no se haya enviado la propuesta
                Dim dtResp As New Data.DataTable
                dtResp = ObjCnx.TraerDataTable("PRP_ConsultarEnvioPropuesta", Request.QueryString("id"))
                If dtResp.Rows.Count > 0 Then

                    'Response.Write(" :1- PRP_CalificarPropuestas_v3 " & Request.QueryString("id") & "," & Me.txtelegido.Value & ",'P','P','P'")
                    ObjCnx.Ejecutar("PRP_CalificarPropuestas_v3", Request.QueryString("id"), Me.txtelegido.Value, "P", "P", "P")

                    ''Si Proponente es Igual a Director de Propuesta (Responsable POA), se debe asignar el consejo Administrativo, Consejo de facultad según corresponda
                    Dim dtt As New Data.DataTable
                    'Response.Write(" :2- PRP_ProponenteDirector " & Request.QueryString("id") & "," & Me.txtelegido.Value)
                    dtt = ObjCnx.TraerDataTable("PRP_ProponenteDirector", Request.QueryString("id"), Me.txtelegido.Value)
                    If dtt.Rows.Count = 0 Then

                    Else
                        If dtt.Rows(0).Item(0).ToString = 1 Then
                            'Response.Write(" :3- PRP_CalificarPropuestas_v3 " & Request.QueryString("id") & "," & Me.txtelegido.Value & ",'D','D','C'")
                            ObjCnx.Ejecutar("PRP_CalificarPropuestas_v3", Request.QueryString("id"), Me.txtelegido.Value, "D", "D", "C")
                        End If
                    End If
                    '
                    ''Response.Write(" PRP_ActualizarAprobacion " & Me.txtelegido.Value)
                    ''ObjCnx.Ejecutar("PRP_ActualizarAprobacion", Me.txtelegido.Value)
                End If
                cmdEnviar.Enabled = False
            End If
            dgvPropuestas.DataBind()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub ddlInstanciaPropuesta_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlInstanciaPropuesta.SelectedIndexChanged
        Call wf_validarBtnModificar()
    End Sub

    Sub wf_validarBtnModificar()
        If ddlInstanciaRevision.SelectedValue = "P" Then
            Me.cmdModificar.Visible = True
        ElseIf ddlInstanciaPropuesta.SelectedValue <> "P" Then
            Me.cmdModificar.Visible = False
        ElseIf ddlInstanciaRevision.SelectedValue <> "P" And ddlInstanciaPropuesta.SelectedValue = "P" Then
            Me.cmdModificar.Visible = False
        Else
            Me.cmdModificar.Visible = True
        End If

        If ddlInstanciaRevision.SelectedValue = "P" And ddlInstanciaPropuesta.SelectedValue <> "P" Then
            cmdConforme.Visible = False
            cmdNoConforme.Visible = False
            cmdObservado.Visible = False
        ElseIf ddlInstanciaRevision.SelectedValue <> "P" And ddlInstanciaPropuesta.SelectedValue = "P" Or ddlEstadoRevision.SelectedValue <> "C" Then
            cmdConforme.Visible = True
            cmdNoConforme.Visible = True
            cmdObservado.Visible = True
        End If
    End Sub

    Protected Sub ddlEstadoRevision_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlEstadoRevision.SelectedIndexChanged
        Me.txtelegido.Value = ""
        Call wf_validarBtnModificar()
    End Sub

    'Protected Sub cmdModificar1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdModificar1.Click
    '    Response.Write("PRP_ConsultarPropuesta_v2 '" & Me.ddlInstanciaRevision.SelectedValue & "','" & Request.QueryString("id") & "','P','" & ddlInstanciaPropuesta.SelectedValue & "','" & ddlEstadoRevision.SelectedValue & "'")
    'End Sub

    Protected Sub dgvPropuestas_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgvPropuestas.SelectedIndexChanged

    End Sub

    Protected Sub cmdModificar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdModificar.Click
        Try
            Dim idUsu As Integer
            idUsu = Request.QueryString("id")

            'Response.Write("<script>alert('" & "nuevapropuesta_POA_v2.aspx?idUsu=" & idUsu & "&codigo_prp=" & Me.txtelegido.Value & "&accion=M&instancia=" & Me.ddlInstanciaPropuesta.SelectedValue & "')</script>")
            'Response.Redirect("nuevapropuesta_POA_v2.aspx?idUsu=" & idUsu & "&codigo_prp=" & Me.txtelegido.Value & "&accion=M&instancia=" & Me.ddlInstanciaPropuesta.SelectedValue)


            Response.Write("<script>alert('Mensaje Código de Usuario3333')</script>")

            Response.Redirect("nuevapropuesta_POA_v2.aspx?idUsu=" & idUsu & "&codigo_prp=" & Me.txtelegido.Value & "&accion=M&instancia=" & Me.ddlInstanciaPropuesta.SelectedValue)
            'Response.Redirect("nuevapropuesta_POA_v2.aspx")
        Catch ex As Exception
            Response.Write("<script>alert('Mensaje Código de Usuario: " & ex.Message & "')</script>")
        End Try
    End Sub
End Class