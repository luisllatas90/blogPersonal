
Partial Class proponente_comentarios
    Inherits System.Web.UI.Page

    Protected Sub LinkButton1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LinkButton1.Click
        Response.Redirect("reuniones.aspx?id=" & Request.QueryString("codigo_per"))
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim ObjCnx As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)

        Me.txtInstancia.Text = Request.QueryString("instancia_rec")
        Select Case Request.QueryString("instancia_rec")
            Case "K" : Me.lblInstancia.Text = "RECTORADO"
            Case "F" : Me.lblInstancia.Text = "CONSEJO DE FACULTAD"
            Case "C" : Me.lblInstancia.Text = "CONSEJO UNIVERSITARIO"
        End Select

        If Request.QueryString("instancia_rec") = "K" Then
            Me.dgvPendientesRectorado.Visible = True
            Me.dgvPendientesConsejo.Visible = False
        End If

        If Request.QueryString("instancia_rec") = "C" Then
            Me.dgvPendientesRectorado.Visible = False
            Me.dgvPendientesConsejo.Visible = True
        End If

        If Request.QueryString("instancia_rec") = "F" Then
            Me.dgvPendientesRectorado.Visible = False
            Me.dgvPendientesConsejo.Visible = False

            Dim rsFac As New Data.DataTable
            rsFac = ObjCnx.TraerDataTable("PRP_ConsutarReunionesConsejo", "FA", Request.QueryString("codigo_per"), "", "")
            If rsFac.Rows.Count > 0 Then
                Me.lblFacultad.Text = rsFac.Rows(0).Item("nombre_fac")
                Me.txtcodigo_Fac.Text = rsFac.Rows(0).Item("codigo_fac")
            Else
                Me.cmdRegistrar.Enabled = False
                Me.txtnombrereunion.Enabled = False
                Me.txtlugarreunion.Enabled = False
                Me.ddlTiporeunion0.Enabled = False
                Me.calFecha.Enabled = False
                Me.dgvPendientes.Enabled = False
                Response.Write("<B>Usted no es miembro de un consejo de Facultad</B>")
            End If
        Else
            'para los accesos a consejo de rectorado y universitario
            Dim rsRec As New Data.DataTable
            rsRec = ObjCnx.TraerDataTable("PRP_ConsutarReunionesConsejo", "RE", Request.QueryString("codigo_per"), Request.QueryString("instancia_rec"), "")
            If rsRec.Rows.Count > 0 Then
                'Me.lblFacultad.Text = rsRec.Rows(0).Item("nombre_fac")
            Else
                Me.cmdRegistrar.Enabled = False
                Me.txtnombrereunion.Enabled = False
                Me.txtlugarreunion.Enabled = False
                Me.ddlTiporeunion0.Enabled = False
                Me.calFecha.Enabled = False
                Me.dgvPendientes.Enabled = False
                Response.Write("<B>Usted no es miembro de esta instancia de revisión</B>")
            End If
        End If
        If Not IsPostBack Then
            Me.txtReunion.Text = Request.QueryString("id_rec")
        End If


        If txtReunion.Text <> "" Then
            Dim rsReunion As New Data.DataTable

            rsReunion = ObjCnx.TraerDataTable("PRP_ConsutarReunionesConsejo", "CA", Me.txtReunion.Text, "", "")
            Me.txtReunion.Text = rsReunion.Rows(0).Item("id_rec")
            Me.txtnombrereunion.Text = rsReunion.Rows(0).Item("agenda_rec")
            Me.txtlugarreunion.Text = rsReunion.Rows(0).Item("lugar_rec")
            Me.calFecha.Text = rsReunion.Rows(0).Item("Fecha")
            Me.ddlTiporeunion0.SelectedValue = rsReunion.Rows(0).Item("tipo_rec")
        End If

    End Sub

    Protected Sub cmdRegistrar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdRegistrar.Click
        Dim rsReunion As New Data.DataTable

        Dim ObjCnx As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)

        If Me.txtReunion.Text = "" Then
            'registrar cabecera
            Me.txtReunion.Text = ObjCnx.TraerValor("RegistrarReunionConsejo", Me.txtnombrereunion.Text, Me.calFecha.Text, Me.txtlugarreunion.Text, "", "", Me.ddlTiporeunion0.SelectedValue, txtcodigo_Fac.Text, Request.QueryString("instancia_rec"), 0)
        Else
            'actualizar cabecera
            ObjCnx.Ejecutar("PRP_ActualizarReunionConsejo", Me.txtReunion.Text, Me.txtnombrereunion.Text, Me.calFecha.Text, Me.txtlugarreunion.Text, Me.ddlTiporeunion0.SelectedValue)
        End If
    End Sub

    Protected Sub dgvPendientes_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles dgvPendientes.RowCommand
        Dim ObjCnx As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)

        If e.CommandName = "cmdAsignar" Then
            Dim index As Integer = Convert.ToInt32(e.CommandArgument)
            ' Response.Write(Me.dgvPendientes.DataKeys(index).Values("codigo_prp").ToString)
            If Me.txtReunion.Text = "" Then
                Me.txtReunion.Text = ObjCnx.TraerValor("RegistrarReunionConsejo", Me.txtnombrereunion.Text, Me.calFecha.Text, Me.txtlugarreunion.Text, "", "", Me.ddlTiporeunion0.SelectedValue, txtcodigo_Fac.Text, Request.QueryString("instancia_rec"), 0)
                ' Response.Write(Me.txtReunion.Text)
            End If
            ObjCnx.Ejecutar("PRP_RegistrarReunionConsejoPropuesta", "NU", Me.txtReunion.Text, Me.dgvPendientes.DataKeys(index).Values("codigo_prp").ToString, "A")
            Me.dgvProgramadas.DataBind()
            Me.dgvPendientes.DataBind()
            Me.dgvPendientesConsejo.DataBind()
            Me.dgvPendientesRectorado.DataBind()
        End If
    End Sub

    Protected Sub dgvPendientes_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgvPendientes.SelectedIndexChanged

    End Sub

    Protected Sub dgvProgramadas_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles dgvProgramadas.RowCommand
        Dim ObjCnx As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)

        If e.CommandName = "cmdQuitar" Then
            Dim index As Integer = Convert.ToInt32(e.CommandArgument)
            ' Response.Write(Me.dgvProgramadas.DataKeys(index).Values("codigo_Rcp").ToString)

            ObjCnx.Ejecutar("PRP_RegistrarReunionConsejoPropuesta", "EL", Me.dgvProgramadas.DataKeys(index).Values("codigo_Rcp").ToString, 0, "")
            Me.dgvProgramadas.DataBind()
            Me.dgvPendientes.DataBind()
            Me.dgvPendientesConsejo.DataBind()
            Me.dgvPendientesRectorado.DataBind()
        End If
    End Sub

    Protected Sub dgvPendientesRectorado_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles dgvPendientesRectorado.RowCommand
        Dim ObjCnx As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)

        If e.CommandName = "cmdAsignar" Then
            Dim index As Integer = Convert.ToInt32(e.CommandArgument)
            'Response.Write(Me.dgvPendientesRectorado.DataKeys(index).Values("codigo_prp").ToString)
            If Me.txtReunion.Text = "" Then
                Me.txtReunion.Text = ObjCnx.TraerValor("RegistrarReunionConsejo", Me.txtnombrereunion.Text, Me.calFecha.Text, Me.txtlugarreunion.Text, "", "", Me.ddlTiporeunion0.SelectedValue, 0, Request.QueryString("instancia_rec"), 0)
                ' Response.Write(Me.txtReunion.Text)
            End If
            ObjCnx.Ejecutar("PRP_RegistrarReunionConsejoPropuesta", "NU", Me.txtReunion.Text, Me.dgvPendientesRectorado.DataKeys(index).Values("codigo_prp").ToString, "A")
            Me.dgvProgramadas.DataBind()
            Me.dgvPendientes.DataBind()
            Me.dgvPendientesConsejo.DataBind()
            Me.dgvPendientesRectorado.DataBind()
        End If
    End Sub

    Protected Sub dgvPendientesConsejo_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles dgvPendientesConsejo.RowCommand
        Dim ObjCnx As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)

        If e.CommandName = "cmdAsignar" Then
            Dim index As Integer = Convert.ToInt32(e.CommandArgument)
            'Response.Write(Me.dgvPendientesConsejo.DataKeys(index).Values("codigo_prp").ToString)
            If Me.txtReunion.Text = "" Then
                Me.txtReunion.Text = ObjCnx.TraerValor("RegistrarReunionConsejo", Me.txtnombrereunion.Text, Me.calFecha.Text, Me.txtlugarreunion.Text, "", "", Me.ddlTiporeunion0.SelectedValue, 0, Request.QueryString("instancia_rec"), 0)
                '  Response.Write(Me.txtReunion.Text)
            End If
            ObjCnx.Ejecutar("PRP_RegistrarReunionConsejoPropuesta", "NU", Me.txtReunion.Text, Me.dgvPendientesConsejo.DataKeys(index).Values("codigo_prp").ToString, "A")
            Me.dgvProgramadas.DataBind()
            Me.dgvPendientes.DataBind()
            Me.dgvPendientesConsejo.DataBind()
            Me.dgvPendientesRectorado.DataBind()
        End If
    End Sub


    Protected Sub dgvProgramadas_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgvProgramadas.SelectedIndexChanged

    End Sub
End Class
