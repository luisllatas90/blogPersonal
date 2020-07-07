﻿
Partial Class proponente_contenido
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim idUsu As Integer
        idUsu = Request.QueryString("id")
        Me.lblUsuario.Text = Request.QueryString("id")
        MostrarInstancias()
        Dim ObjCnx As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
        Me.cmdDerivar.Visible = (ObjCnx.TraerValor("PRP_VerificarSecretariasConsejos", idUsu, 0))

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
        Dim idUsu As Integer
        idUsu = Request.QueryString("id")

        Response.Redirect("nuevapropuesta.aspx?idUsu=" & idUsu)
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
        Response.Redirect("nuevapropuesta.aspx?idUsu=" & idUsu & "&codigo_prp=" & Me.txtelegido.Value & "&accion=M&instancia=" & Me.ddlInstanciaPropuesta.SelectedValue)
    End Sub

    Protected Sub ddlInstanciaRevision_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlInstanciaRevision.SelectedIndexChanged
        MostrarInstancias()
    End Sub


    Protected Sub cmdConforme_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdConforme.Click
        Dim ObjCnx As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
        ObjCnx.Ejecutar("PRP_CalificarPropuestas", Request.QueryString("id"), Me.txtelegido.Value, Me.ddlInstanciaRevision.SelectedValue, Me.ddlInstanciaRevision.SelectedValue, "C")

    End Sub

    Protected Sub cmdObservado_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdObservado.Click
        Dim ObjCnx As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
        ObjCnx.Ejecutar("PRP_CalificarPropuestas", Request.QueryString("id"), Me.txtelegido.Value, Me.ddlInstanciaRevision.SelectedValue, Me.ddlInstanciaRevision.SelectedValue, "O")

    End Sub

    Protected Sub cmdNoConforme_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdNoConforme.Click
        Dim ObjCnx As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
        ObjCnx.Ejecutar("PRP_CalificarPropuestas", Request.QueryString("id"), Me.txtelegido.Value, Me.ddlInstanciaRevision.SelectedValue, Me.ddlInstanciaRevision.SelectedValue, "N")

    End Sub

    Protected Sub cmdDerivar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdDerivar.Click
        Dim ObjCnx As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
        ObjCnx.Ejecutar("PRP_CalificarPropuestas", Request.QueryString("id"), Me.txtelegido.Value, Me.ddlInstanciaRevision.SelectedValue, Me.ddlInstanciaRevision.SelectedValue, "D")

    End Sub

    Protected Sub cmdModificar0_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdModificar0.Click
        Response.Redirect("../ayuda/faq.htm")
    End Sub
End Class
