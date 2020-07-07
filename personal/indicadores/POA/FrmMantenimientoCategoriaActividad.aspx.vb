
Partial Class indicadores_POA_FrmMantenimientoCategoriaActividad
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (Session("id_per") Is Nothing) Then
            Response.Redirect("../../../sinacceso.html")
        End If

        If IsPostBack = False Then
            hdvigencia.Value = Request.QueryString("vigencia")
            Dim codigo_cat As Integer = Request.QueryString("codigo_cat")
            Dim nombre_cat As String = txtNombrePoa.Text.ToUpper
            Dim estado_cat As Integer = 1
            Dim usuario_reg As Integer = Request.QueryString("id")
            Dim fecha_reg As String = "07/08/2017"
            Dim tipo As String = Request.QueryString("tipo")

            If tipo = "E" Then
                Dim obj As New clsPlanOperativoAnual
                Dim dt As New Data.DataTable

                'Response.Write(codigo_cat & "," & nombre_cat & "," & estado_cat & "," & usuario_reg & "," & fecha_reg & "," & "L")

                dt = obj.POA_CategoriaActividad(codigo_cat, nombre_cat, estado_cat, usuario_reg, fecha_reg, "L")
                Me.txtNombrePoa.Text = dt.Rows(0).Item("nombre_cat")
            End If
            txtNombrePoa.Focus()
        End If
    End Sub

    Protected Sub cmdGuardarPoa_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdGuardarPoa.Click
        Try
            Dim dt As New Data.DataTable
            Dim obj As New clsPlanOperativoAnual

            If txtNombrePoa.Text = "" Then
                Response.Write("<script>alert('Ingrese Actividad')</script>")
                txtNombrePoa.Focus()
                Return
            End If

            Dim tipo As String = Request.QueryString("tipo")
            If tipo = "I" Then
                Dim codigo_cat As Integer = 0
                Dim nombre_cat As String = txtNombrePoa.Text.ToUpper
                Dim estado_cat As Integer = 1
                Dim usuario_reg As Integer = Request.QueryString("id")
                Dim fecha_reg As String = "07/08/2017"
                obj.POA_CategoriaActividad(codigo_cat, nombre_cat, estado_cat, usuario_reg, fecha_reg, tipo)

                Call Limpiar()
                Response.Redirect("FrmListaCategoriaActividad.aspx?id=" & Request.QueryString("id") & "&vigencia=" & hdvigencia.Value & "&tipo=" & tipo & "&msj='R'")

            ElseIf tipo = "E" Then
                Dim codigo_cat As Integer = Request.QueryString("codigo_cat")
                Dim nombre_cat As String = txtNombrePoa.Text.ToUpper
                Dim estado_cat As Integer = 1
                Dim usuario_reg As Integer = Request.QueryString("id")
                Dim fecha_reg As String = "07/08/2017"
                obj.POA_CategoriaActividad(codigo_cat, nombre_cat, estado_cat, usuario_reg, fecha_reg, tipo)

                Call Limpiar()
                Response.Redirect("FrmListaCategoriaActividad.aspx?id=" & Request.QueryString("id") & "&vigencia=" & hdvigencia.Value & "&tipo=" & tipo & "&msj='E'")
            End If



            '        If mensaje = "1" Then
            '            Response.Redirect("FrmListaPlanOperativoAnual.aspx?id=" & Request.QueryString("id") & "&ctf=" & Request.QueryString("ctf") & "&cb1=" & Me.ddlPlan.SelectedValue & "&cb2=" & Me.ddlEjercicio.SelectedValue & "&cb3=" & Request.QueryString("cb3") & "&msj=R")
            '            'Me.lblmensaje.Text = "Datos Registrados Correctamente"
            '            'Me.aviso.Attributes.Add("class", "mensajeExito")
            '            'Limpiar()
            '        ElseIf mensaje = "0" Then
            '            Me.lblmensaje.Text = "No se pudo Registrar, Error al Registrar"
            '            Me.aviso.Attributes.Add("class", "mensajeError")
            '        ElseIf mensaje = "2" Then
            '            Me.lblmensaje.Text = "No se Pudo Registrar, Existe un Plan Creado Para El Area y Ejercicio Seleccionados"
            '            Me.aviso.Attributes.Add("class", "mensajeError")
            '        ElseIf mensaje = "3" Then
            '            Me.lblmensaje.Text = "No se Pudo Actualizar, Plan Operativo Cuenta con Centros de Costo Asignados"
            '            Me.aviso.Attributes.Add("class", "mensajeError")
            '        End If
            '    Else
            '        mensaje = obj.AtualizarPoa(Me.ddlEjercicio.SelectedValue, Me.ddlPlan.SelectedValue, Me.hdcodarea.Value, Me.txtNombrePoa.Text.Trim.ToUpper.ToString, Me.ddlResponsable.SelectedValue, Request.QueryString("id"), vigencia, Me.hdcodigopoa.Value)
            '        If mensaje = "1" Then
            '            Response.Redirect("FrmListaPlanOperativoAnual.aspx?id=" & Request.QueryString("id") & "&ctf=" & Request.QueryString("ctf") & "&cb1=" & Me.ddlPlan.SelectedValue & "&cb2=" & Me.ddlEjercicio.SelectedValue & "&cb3=" & Request.QueryString("cb3") & "&msj=M")
            '            'Me.lblmensaje.Text = "Datos Actualizados Correctamente"
            '            'Me.aviso.Attributes.Add("class", "mensajeExito")
            '            'Limpiar()
            '        ElseIf mensaje = "0" Then
            '            Me.lblmensaje.Text = "No se Pudo Modificar, Error al Modificar"
            '            Me.aviso.Attributes.Add("class", "mensajeError")
            '        ElseIf mensaje = "2" Then
            '            Me.lblmensaje.Text = "No se Pudo Modificar, Existe un Plan Creado Para El Area y Ejercicio Seleccionados"
            '            Me.aviso.Attributes.Add("class", "mensajeError")
            '        ElseIf mensaje = "3" Then
            '            Me.lblmensaje.Text = "No se Pudo Actualizar, Plan Operativo Cuenta con Centros de Costo Asignados"
            '            Me.aviso.Attributes.Add("class", "mensajeError")
            '        End If
            '    End If
            '    Me.treePrueba.CollapseAll()
            'Else 'Limpia la cadena inválida de la caja de texto
            ''txtDescripcionPers.Text = ""
            'End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Sub Limpiar()
        txtNombrePoa.Text = ""
    End Sub

    Protected Sub cmdCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdCancelar.Click
        Response.Redirect("FrmListaCategoriaActividad.aspx?id=" & Request.QueryString("id") & "&vigencia=" & hdvigencia.Value & "&tipo=" & Request.QueryString("tipo") & "&msj='T'")
    End Sub

End Class
