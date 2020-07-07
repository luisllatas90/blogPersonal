
Partial Class _COntenido
    Inherits System.Web.UI.Page

    Protected Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim fila As Data.DataRowView
            fila = e.Row.DataItem 'Los datos devueltos (cuando quiero saber la data que llega)
            e.Row.Cells(0).Text = (e.Row.RowIndex + 1).ToString
            e.Row.Attributes.Add("OnMouseOver", "Resaltar(1,this,'S')")
            e.Row.Attributes.Add("OnMouseOut", "Resaltar(0,this,'S')")
            e.Row.Attributes.Add("OnClick", "ActivarBotonModificar(); ActivarSubir(); ResaltarfilaDetalle_net('',this,'datos_investigacion.aspx?codigo_Inv=" & fila.Row("codigo_inv").ToString & "');ResaltarPestana_1('0','','')")
            e.Row.Attributes.Add("id", "fila" & fila.Row("codigo_inv").ToString & "")
            e.Row.Attributes.Add("Class", "Sel")
            e.Row.Attributes.Add("Typ", "Sel")
            'e.Row.ToolTip = fila.Row("nombre_eti").ToString & " - " & fila.Row("descripcion_ein").ToString & " - TIPO: " & fila.Row("descripcion_tin").ToString
        End If

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Session("codigo_per") = Request.QueryString("id")
        Dim tipo As String
        Dim estado As String
        tipo = Request.QueryString("tipo")
        estado = Request.QueryString("estado")

        Me.CmdResumen.Visible = False
        Me.CmdModificar.Visible = False
        Me.CmdAvance.Visible = False
        Me.CmdAvances.Visible = False
        Me.CmdInforme.Visible = False
        Me.CmdProyecto.Visible = False

        If tipo = 1 Then
            Select Case estado
                Case 1
                    Me.LblTitulo.Text = "Perfil Por Revisar por Director de Departamento"
                Case 2
                    Me.CmdModificar.Visible = True
                    Me.CmdModificar.Enabled = False
                    Me.LblTitulo.Text = "Perfil Observados"
                Case 3
                    Me.LblTitulo.Text = "Perfil Por Revisar por Director de Investigacion"
                Case 4
                    Me.LblTitulo.Text = "Perfil Desaprobado por Director de Departamento"
                Case 7
                    Me.LblTitulo.Text = "Perfil Aprobado por Director de Investigacion"
                    Me.CmdProyecto.Visible = True
                    Me.CmdProyecto.Enabled = False
                Case 8
                    Me.LblTitulo.Text = "Perfil Desaprobado por Director de Investigacion"
            End Select
        End If

        If tipo = 2 Then
            Me.CmdNuevo.Visible = False
            Select Case estado
                Case 1
                    Me.LblTitulo.Text = "Proyectos Por Revisar por Director de Departamento"
                Case 2
                    Me.CmdModificar.Visible = True
                    Me.CmdModificar.Enabled = False
                    Me.LblTitulo.Text = "Proyectos Observados"
                Case 3
                    Me.LblTitulo.Text = "Proyectos Por Revisar por Director de Investigacion"
                Case 4
                    Me.LblTitulo.Text = "Proyectos Desaprobados por Director de Departamento"
                Case 6
                    Me.LblTitulo.Text = "Proyectos en Ejecucion"
                    Me.CmdAvances.Visible = True
                    Me.CmdInforme.Visible = True
                    Me.CmdAvances.Enabled = False
                    Me.CmdInforme.Enabled = False
                Case 7
                    Me.LblTitulo.Text = "Proyectos Aprobados por Director de Investigacion"
                    Me.CmdAvance.Visible = True
                    Me.CmdAvance.Enabled = False
                Case 8
                    Me.LblTitulo.Text = "Proyectos Desaprobados por Director de Investigacion"
            End Select
        End If

        If tipo = 3 Then
            Me.CmdNuevo.Visible = False
            Select Case estado
                Case 1
                    Me.LblTitulo.Text = "Informes Por Revisar por Director de Departamento"
                Case 2
                    Me.CmdModificar.Visible = True
                    Me.CmdModificar.Enabled = False
                    Me.LblTitulo.Text = "Informes Observados"
                Case 3
                    Me.LblTitulo.Text = "Informes Por Revisar por Director de Investigacion"
                Case 4
                    Me.LblTitulo.Text = "Informes Desaprobados por Director de Departamento"
                Case 7
                    Me.LblTitulo.Text = "Informes Aprobados por Director de Investigacion"
                    Me.CmdResumen.Visible = True
                    Me.CmdResumen.Enabled = False
                Case 8
                    Me.LblTitulo.Text = "Informes Desaprobados por Director de Investigacion"
                Case 9
                    Me.LblTitulo.Text = "Investigaciones Finalizadas"
            End Select
        End If

        If IsPostBack = False Then
            Me.CmdNuevo.Attributes.Add("Onclick", "AbrirPopUp('frmInvestigacion1.aspx?id=" & Request.QueryString("id") & "','480','650'); return false;")
            'Me.CmdNuevo.Attributes.Add("Onclick", "AbrirPopUp('frminvestigacionotros.aspx?id=" & Request.QueryString("id") & "','480','650'); return false;")
            Me.CmdModificar.Attributes.Add("OnClick", "ModificarInvestigacion(" & Request.QueryString("tipo") & "," & Request.QueryString("id") & "); return false;")
            Me.CmdProyecto.Attributes.Add("OnClick", "SubirProyecto(" & Request.QueryString("tipo") & "," & Request.QueryString("tipo") & "," & Request.QueryString("tipo") & "," & Request.QueryString("tipo") & "); return false;")
            Me.CmdAvance.Attributes.Add("OnClick", "SubirAvances(); return false;")
            Me.CmdAvances.Attributes.Add("OnClick", "SubirAvances(); return false;")
            Me.CmdInforme.Attributes.Add("OnClick", "SubirInforme(); return false;")
            Me.CmdResumen.Attributes.Add("OnClick", "SubirResumen(); return false;")
        End If
    End Sub

End Class
