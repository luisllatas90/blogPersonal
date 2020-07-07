
Partial Class _COntenido
    Inherits System.Web.UI.Page

    Protected Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim fila As Data.DataRowView
                fila = e.Row.DataItem 'Los datos devueltos (cuando quiero saber la data que llega)
                e.Row.Attributes.Add("OnMouseOver", "Resaltar(1,this,'S')")
                e.Row.Attributes.Add("OnMouseOut", "Resaltar(0,this,'S')")
                e.Row.Attributes.Add("OnClick", "ActivarBotonModificar(); ActivarSubir(); ResaltarfilaDetalle_net('',this,'datos_investigacion.aspx?codigo_Inv=" & fila.Row("codigo_inv").ToString & "');ResaltarPestana_1('0','','')")
                e.Row.Attributes.Add("id", "fila" & fila.Row("codigo_inv").ToString & "")
                e.Row.Attributes.Add("Class", "Sel")
                e.Row.Attributes.Add("Typ", "Sel")
                'e.Row.ToolTip = fila.Row("nombre_eti").ToString & " - " & fila.Row("descripcion_ein").ToString & " - TIPO: " & fila.Row("descripcion_tin").ToString
                e.Row.Cells(0).Text = e.Row.RowIndex + 1
            End If
        Catch ex As Exception

        End Try
        
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim tipo As String
        Dim estado As String
        tipo = Request.QueryString("tipo")
        estado = Request.QueryString("estado")
        Session("codigo_per") = Request.QueryString("id")
        Me.CmdObservar.Visible = False
        Me.CmdAprobar.Visible = False
        Me.CmdDesaprobar.Visible = False

        If tipo = 1 Then
            Me.CmdAprobar.Attributes.Remove("OnClick")
            Me.CmdAprobar.Attributes.Add("OnClick", "return confirm('¿Desea APROBAR la Investigación?');")
            Select Case estado
                Case 1
                    Me.LblTitulo.Text = "Perfil Por Revisar por Director de Departamento"
                Case 2
                    Me.LblTitulo.Text = "Perfil Observados"
                Case 3
                    Me.LblTitulo.Text = "Perfil Por Revisar por Director de Investigacion"
                    Me.CmdObservar.Visible = True
                    Me.CmdAprobar.Visible = True
                    Me.CmdDesaprobar.Visible = True
                    Me.CmdObservar.Enabled = False
                    Me.CmdAprobar.Enabled = False
                    Me.CmdDesaprobar.Enabled = False
                Case 4
                    Me.LblTitulo.Text = "Perfil Desaprobado por Director de Departamento"
                Case 7
                    Me.LblTitulo.Text = "Perfil Aprobado por Director de Investigacion"
                Case 8
                    Me.LblTitulo.Text = "Perfil Desaprobado por Director de Investigacion"
            End Select
        End If

        If tipo = 2 Then
            Me.CmdAprobar.Attributes.Remove("OnClick")
            Me.CmdAprobar.Attributes.Add("OnClick", "Decreto(); return false;")
            Select Case estado
                Case 1
                    Me.LblTitulo.Text = "Proyectos Por Revisar por Director de Departamento"
                Case 2
                    Me.LblTitulo.Text = "Proyectos Observados"
                Case 3
                    Me.LblTitulo.Text = "Proyectos Por Revisar por Director de Investigacion"
                    Me.CmdObservar.Visible = True
                    Me.CmdAprobar.Visible = True
                    Me.CmdDesaprobar.Visible = True
                    Me.CmdObservar.Enabled = False
                    Me.CmdAprobar.Enabled = False
                    Me.CmdDesaprobar.Enabled = False
                Case 4
                    Me.LblTitulo.Text = "Proyectos Desaprobados por Director de Departamento"
                Case 6
                    Me.LblTitulo.Text = "Proyectos en Ejecucion"
                Case 7
                    Me.LblTitulo.Text = "Proyectos Aprobados por Director de Investigacion"
                Case 8
                    Me.LblTitulo.Text = "Proyectos Desaprobados por Director de Investigacion"
            End Select
        End If

        If tipo = 3 Then
            Me.CmdAprobar.Attributes.Remove("OnClick")
            Me.CmdAprobar.Attributes.Add("OnClick", "return confirm('¿Desea APROBAR la Investigación?');")
            Select Case estado
                Case 1
                    Me.LblTitulo.Text = "Informes Por Revisar por Director de Departamento"
                Case 2
                    Me.LblTitulo.Text = "Informes Observados"
                Case 3
                    Me.LblTitulo.Text = "Informes Por Revisar por Director de Investigacion"
                    Me.CmdObservar.Visible = True
                    Me.CmdAprobar.Visible = True
                    Me.CmdDesaprobar.Visible = True
                    Me.CmdObservar.Enabled = False
                    Me.CmdAprobar.Enabled = False
                    Me.CmdDesaprobar.Enabled = False
                Case 4
                    Me.LblTitulo.Text = "Informes Desaprobados por Director de Departamento"
                Case 7
                    Me.LblTitulo.Text = "Informes Aprobados por Director de Investigacion"
                Case 8
                    Me.LblTitulo.Text = "Informes Desaprobados por Director de Investigacion"
                Case 9
                    Me.LblTitulo.Text = "Investigaciones Finalizadas"
            End Select
        End If

        If IsPostBack = False Then
            Me.CmdObservar.Attributes.Add("OnClick", "Observar(); return false;")
            Me.CmdDesaprobar.Attributes.Add("OnClick", "return confirm('Se dispone a DESAPROBAR la Investigación. ¿Desea Continuar?, no podrá deshacer los cambios.')")
        End If
    End Sub

    Protected Sub CmdAprobar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdAprobar.Click
        Dim ObjInv As New Investigacion
        If ObjInv.CambiarestadoInvestigacion(CInt(Right(Me.txtelegido.Value, Len(Me.txtelegido.Value) - 4)), 7, Request.QueryString("id"), Request.QueryString("tipo")) = -1 Then
            Me.LblMensaje.ForeColor = Drawing.Color.Red
            Me.LblMensaje.Text = "Ocurrió un error al aprobar la investigación, inténtelo nuevamente."
        Else
            Me.LblMensaje.ForeColor = Drawing.Color.Blue
            Me.LblMensaje.Text = "Se aprobó la investigación con satisfacción"
            Me.GridView1.DataBind()
        End If
        ObjInv = Nothing
    End Sub

    Protected Sub CmdDesaprobar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdDesaprobar.Click
        Dim ObjInv As New Investigacion
        If ObjInv.CambiarestadoInvestigacion(CInt(Right(Me.txtelegido.Value, Len(Me.txtelegido.Value) - 4)), 8, Request.QueryString("id")) = -1 Then
            Me.LblMensaje.ForeColor = Drawing.Color.Red
            Me.LblMensaje.Text = "Ocurrió un error al desaprobar la investigación, inténtelo nuevamente."
        Else
            Me.LblMensaje.ForeColor = Drawing.Color.Blue
            Me.LblMensaje.Text = "Se DESAPROBÓ la Investigación con Satisfacción"
            Me.GridView1.DataBind()
        End If
        ObjInv = Nothing

    End Sub

   
End Class
