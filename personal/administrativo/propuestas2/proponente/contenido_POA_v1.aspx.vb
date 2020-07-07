Partial Class proponente_contenido
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.lblUsuario.Text = Request.QueryString("id")
        txtUsuario.Value = Request.QueryString("id")

        Call MostrarInstancias()
        Dim ObjCnx As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
        Me.cmdDerivar.Visible = ObjCnx.TraerValor("PRP_VerificarSecretariasConsejos_v1", Request.QueryString("id"), 0)
        If ddlInstanciaRevision.SelectedValue <> "P" Then
            Me.cmdModificar.Enabled = False
        End If

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
            ' Me.cmdDerivar.Visible = False
        Else
            Me.cmdConforme.Visible = True
            Me.cmdObservado.Visible = True
            Me.cmdNoConforme.Visible = True
            'Me.cmdDerivar.Visible = True
        End If

        If ddlInstanciaRevision.SelectedIndex = 0 Then
            ddlInstanciaPropuesta.Enabled = True
            ddlEstadoRevision.Enabled = False
        Else
            ddlInstanciaPropuesta.Enabled = False
            ddlInstanciaPropuesta.SelectedIndex = 0
            ddlEstadoRevision.Enabled = True
        End If
    End Sub

    Sub CargarComboInstanciaRevision()
        Dim ObjCnx As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
        ''Llenar Combo Propuestas 
        Dim dttpropuesta As New Data.DataTable
        dttpropuesta = ObjCnx.TraerDataTable("PRP_ListarInstancias", Request.QueryString("id"))
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
            'Me.cmdDerivar.Visible = False
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

            Response.Redirect("nuevapropuesta_POA_v1.aspx?idUsu=" & idUsu)
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
                    Else

                    End If
                End If
                e.Row.Attributes.Add("OnClick", "HabilitarBoton('M',this,'" + rpta + "')")
                e.Row.Attributes.Add("Class", "Sel")
                e.Row.Attributes.Add("Typ", "Sel")
            End If
        Catch ex As Exception
            Response.Write(ex)
        End Try
    End Sub

    Protected Sub cmdModificar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdModificar.Click
        Dim idUsu As Integer
        idUsu = Request.QueryString("id")
        Response.Redirect("nuevapropuesta_POA_v1.aspx?idUsu=" & idUsu & "&codigo_prp=" & Me.txtelegido.Value & "&accion=M&instancia=" & Me.ddlInstanciaPropuesta.SelectedValue)
    End Sub

    Protected Sub ddlInstanciaRevision_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlInstanciaRevision.SelectedIndexChanged
        Me.txtelegido.Value = ""
        MostrarInstancias()
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
            'Response.Write("PRP_CalificarPropuestas_v1" & " " & Request.QueryString("id") & "," & Me.txtelegido.Value & "," & Instancia & "," & Instancia & "," & "C")

            ObjCnx.Ejecutar("PRP_CalificarPropuestas_v1", Request.QueryString("id"), Me.txtelegido.Value, Instancia, Instancia, "C")
            ObjCnx.Ejecutar("PRP_ConsultarInvolucradosPendientesRectorado_v1", Me.txtelegido.Value)
            dgvPropuestas.DataBind()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub cmdObservado_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdObservado.Click
        Try
            Dim ObjCnx As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
            'Response.Write("PRP_CalificarPropuestas_v1 " & Request.QueryString("id") & "," & Me.txtelegido.Value & ",'" & Me.ddlInstanciaRevision.SelectedValue & "','" & Me.ddlInstanciaRevision.SelectedValue & "'," & "'O'")

            ObjCnx.Ejecutar("PRP_CalificarPropuestas_v1", Request.QueryString("id"), Me.txtelegido.Value, Me.ddlInstanciaRevision.SelectedValue, Me.ddlInstanciaRevision.SelectedValue, "O")

            dgvPropuestas.DataBind()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub cmdNoConforme_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdNoConforme.Click
        Try
            Dim ObjCnx As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
            ObjCnx.Ejecutar("PRP_CalificarPropuestas_v1", Request.QueryString("id"), Me.txtelegido.Value, Me.ddlInstanciaRevision.SelectedValue, Me.ddlInstanciaRevision.SelectedValue, "N")

            dgvPropuestas.DataBind()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub cmdDerivar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdDerivar.Click
        Try
            Dim ObjCnx As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
            ObjCnx.Ejecutar("PRP_DerivarPropuesta_v1", Me.txtelegido.Value)

            dgvPropuestas.DataBind()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
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
                'select COUNT(*) from InvolucradoPropuesta where codigo_Prp=3378 and veredicto_Ipr='P' and instancia_Ipr='P'
                dtResp = ObjCnx.TraerDataTable("PRP_ConsultarEnvioPropuesta", Request.QueryString("id"))
                If dtResp.Rows.Count > 0 Then
                    'Response.Write("PRP_CalificarPropuestas_v1" & "," & Request.QueryString("id") & "," & Me.txtelegido.Value & "," & "P" & "," & "P" & "," & "P")
                    ObjCnx.Ejecutar("PRP_CalificarPropuestas_v1", Request.QueryString("id"), Me.txtelegido.Value, "P", "P", "P")

                    ''Si Proponente es Igual a Director de Propuesta (Responsable POA), se debe asignar el consejo Administrativo, Consejo de facultad según corresponda
                    Dim dtt As New Data.DataTable
                    dtt = ObjCnx.TraerDataTable("PRP_ProponenteDirector", Request.QueryString("id"), Me.txtelegido.Value)
                    If dtt.Rows.Count = 0 Then
                    Else
                        If dtt.Rows(0).Item(0).ToString = 1 Then
                            ObjCnx.Ejecutar("PRP_CalificarPropuestas_v1", Request.QueryString("id"), Me.txtelegido.Value, "D", "D", "C")
                        End If
                    End If

                End If
                cmdEnviar.Enabled = False
            End If
            dgvPropuestas.DataBind()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub ddlInstanciaPropuesta_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlInstanciaPropuesta.SelectedIndexChanged
        'If Me.ddlInstanciaPropuesta.SelectedValue = "F" Then
        '    Dim codigo_per As Integer = Request.QueryString("id")
        '    'Response.Write(codigo_per)

        '    'ACTIVA EL BTTN DERIVAR CUANDO PERTENECEN AL CCO: DIRECCION DE POSTGRADO
        '    Me.cmdDerivar.Enabled = ObjCnx.TraerValor("PRP_ActivarBtnDerivar", codigo_per, 0)
        'End If
    End Sub

    Protected Sub ddlEstadoRevision_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlEstadoRevision.SelectedIndexChanged
        Me.txtelegido.Value = ""
    End Sub

    Protected Sub cmdModificar1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdModificar1.Click
        Response.Write("PRP_ConsultarPropuesta_v1 '" & Me.ddlInstanciaRevision.SelectedValue & "','" & Request.QueryString("id") & "','P','" & ddlInstanciaPropuesta.SelectedValue & "','" & ddlEstadoRevision.SelectedValue & "'")
    End Sub
End Class