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
    End Sub

    Private Sub MostrarInstancias()
        Dim idUsu As Integer
        idUsu = Request.QueryString("id")
        If Me.ddlInstanciaRevision.SelectedValue = "P" Then
            Me.lblEtiquetaInstancia.Visible = True
            Me.ddlInstanciaPropuesta.Visible = True
            Me.lblEtiquetaEstado.Visible = False
            Me.ddlEstadoRevision.Visible = False
            ' Botones de calificación
            Me.cmdConforme.Visible = False
            Me.cmdObservado.Visible = False
            Me.cmdNoConforme.Visible = False
            Me.cmdDerivar.Visible = False
        Else
            Me.lblEtiquetaInstancia.Visible = False
            Me.ddlInstanciaPropuesta.Visible = False
            Me.lblEtiquetaEstado.Visible = True
            Me.ddlEstadoRevision.Visible = True
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

            Dim dtProyecto As New Data.DataTable
            Dim ObjCnx As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)

            dtProyecto = ObjCnx.TraerDataTable("PRO_listaProyectosPoa", idUsu)
            If dtProyecto.Rows.Count = 0 Then
                Response.Write("<script>alert('No se han Registrado Programas/Proyectos con este Usuario')</script>")
                Return
            End If

            Response.Redirect("nuevapropuesta_POA_v1.aspx?idUsu=" & idUsu)
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub dgvPropuestas_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles dgvPropuestas.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim fila As Data.DataRowView
            fila = e.Row.DataItem
            e.Row.Attributes.Add("id", "" & fila.Row("codigo_prp").ToString & "")
            e.Row.Attributes.Add("OnMouseOver", "Resaltar(1,this,'S')")
            e.Row.Attributes.Add("OnMouseOut", "Resaltar(0,this,'S')")
            e.Row.Attributes.Add("OnClick", "HabilitarBoton('M',this)")
            e.Row.Attributes.Add("Class", "Sel")
            e.Row.Attributes.Add("Typ", "Sel")
            'e.Row.Cells(0).Text = e.Row.RowIndex + 1
        End If
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

            ' Response.Write("PRP_CalificarPropuestas_v1" & ", " & Request.QueryString("id") & ", " & Me.txtelegido.Value & ", " & Me.ddlInstanciaRevision.SelectedValue & ", " & Me.ddlInstanciaRevision.SelectedValue & ", " & "C")

            ObjCnx.Ejecutar("PRP_CalificarPropuestas_v1", Request.QueryString("id"), Me.txtelegido.Value, Me.ddlInstanciaRevision.SelectedValue, Me.ddlInstanciaRevision.SelectedValue, "C")
            ObjCnx.Ejecutar("PRP_ConsultarInvolucradosPendientesRectorado_v1", Me.txtelegido.Value)

            dgvPropuestas.DataBind()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub cmdObservado_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdObservado.Click
        Try
            Dim ObjCnx As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
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
        If Me.txtelegido.Value = "" Then
            Response.Write("<H3>SELECCIONE UNA PROPUESTA PARA ENVIAR</H3>")
        Else
            Dim ObjCnx As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)

            ' Response.Write("PRP_CalificarPropuestas_v1" & "," & Request.QueryString("id") & "," & Me.txtelegido.Value & "," & "P" & "," & "P" & "," & "P")

            ObjCnx.Ejecutar("PRP_CalificarPropuestas_v1", Request.QueryString("id"), Me.txtelegido.Value, "P", "P", "P")

            'Si Proponente es Igual a Director de Propuesta (Responsable POA), se debe asignar el consejo Administrativo, Consejo de facultad según corresponda
            Dim dtt As New Data.DataTable
            dtt = ObjCnx.TraerDataTable("PRP_ProponenteDirector", Request.QueryString("id"), Me.txtelegido.Value)
            If dtt.Rows.Count = 0 Then
            Else
                If dtt.Rows(0).Item(0).ToString = 1 Then
                    ObjCnx.Ejecutar("PRP_CalificarPropuestas_v1", Request.QueryString("id"), Me.txtelegido.Value, "D", "D", "C")
                End If
            End If
        End If
        dgvPropuestas.DataBind()
    End Sub

    Protected Sub ddlInstanciaPropuesta_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlInstanciaPropuesta.SelectedIndexChanged
        Dim ObjCnx As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
        Me.txtelegido.Value = ""

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
        Response.Write("PRP_ConsultarPropuesta_v1 '" & ddlInstanciaRevision.SelectedValue & "','" & Request.QueryString("id") & "','P','" & ddlInstanciaPropuesta.SelectedValue & "','" & ddlEstadoRevision.SelectedValue & "'")
    End Sub
End Class