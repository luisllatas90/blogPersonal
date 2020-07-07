
Partial Class distinciones
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Request.QueryString("id") <> "" And Session("id") = "" Then
            Session("Id") = Request.QueryString("id")
        End If

        If IsPostBack = False Then
            Me.LinkAgregaDistinciones.Attributes.Add("OnMouseOver", "tabsobre(this,1)")
            Me.LinkAgregaDistinciones.Attributes.Add("OnMouseOut", "tabsobre(this,2)")
            Me.CmdGuardar.Attributes.Add("OnMouseOver", "tabsobre(this,1)")
            Me.CmdGuardar.Attributes.Add("OnMouseOut", "tabsobre(this,2)")
            Me.TxtFecha.Attributes.Add("OnKeyDown", "javascript:return false;")
            Dim ObjCombos As New Combos
            ObjCombos.LlenaTipoDistincion(Me.DDLDistinciones)

            Me.combo_procedencia.items.add("Nacional")
            Me.combo_procedencia.items.add("Extrajera")

            Me.combo_autoria.items.add("1er autor")
            Me.combo_autoria.items.add("2do autor")
            Me.combo_autoria.items.add("3er autor")

            Me.combo_tipo.items.add("Revista")
            Me.combo_tipo.items.add("Working Paper")
            Me.combo_tipo.items.add("Capítulo de Libro")
            Me.combo_tipo.items.add("Libro")
            Me.combo_tipo.items.add("Otro")

            Me.combo_sino.items.add("Si")
            Me.combo_sino.Items.Add("No")

            CargarPublicaciones()
        End If

    End Sub

    Sub CargarPublicaciones()
        Dim ObjDistin As New Personal
        Dim datos As Data.DataTable
        datos = ObjDistin.ConsultarPublicaciones(CInt(Request.QueryString("id")))
        If datos.Rows.Count > 0 Then
            Me.GridView2.DataSource = datos
            Me.GridView2.DataBind()
        Else
            Me.GridView2.DataSource = Nothing
            Me.GridView2.DataBind()
        End If
    End Sub
    Protected Sub LinkVistaDistinciones_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LinkVistaDistinciones.Click
        Me.LinkVistaDistinciones.CssClass = "tab_seleccionado"
        Me.LinkAgregaDistinciones.CssClass = "tab_normal"
        Me.Panel1.Visible = True
        Me.Panel4.Visible = True

        Me.Panel3.Visible = False
        Me.Panel2.Visible = False

        Me.LinkVistaDistinciones.Attributes.Remove("OnMouseOver")
        Me.LinkVistaDistinciones.Attributes.Remove("OnMouseOut")

        Me.LinkAgregaDistinciones.Attributes.Add("OnMouseOver", "tabsobre(this,1)")
        Me.LinkAgregaDistinciones.Attributes.Add("OnMouseOut", "tabsobre(this,2)")
    End Sub

    Protected Sub LinkAgregaDistinciones_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LinkAgregaDistinciones.Click
        Me.HddDistinciones.Value = "-1"
        Me.LinkVistaDistinciones.CssClass = "tab_normal"
        Me.LinkAgregaDistinciones.CssClass = "tab_seleccionado"

        Me.LinkAgregaDistinciones.Attributes.Remove("OnMouseOver")
        Me.LinkAgregaDistinciones.Attributes.Remove("OnMouseOut")

        Me.LinkVistaDistinciones.Attributes.Add("OnMouseOver", "tabsobre(this,1)")
        Me.LinkVistaDistinciones.Attributes.Add("OnMouseOut", "tabsobre(this,2)")

        Me.Panel3.Visible = True
        Me.Panel2.Visible = True

        Me.Panel1.Visible = False
        Me.Panel4.Visible = False

        Me.LblExperiencia.Text = "Registro de Distinciones"
        Me.LblExperiencia4.Text = "Registro de Publicaciones"



        For Each controles As Control In Me.Panel3.Controls
            If controles.GetType.ToString = "System.Web.UI.WebControls.TextBox" Then
                CType(controles, TextBox).Text = ""
            End If
        Next

        For Each controles As Control In Me.Panel2.Controls
            If controles.GetType.ToString = "System.Web.UI.WebControls.TextBox" Then
                CType(controles, TextBox).Text = ""
            End If
        Next

    End Sub

    Protected Sub GridView1_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles GridView1.RowEditing
        Me.LinkVistaDistinciones.CssClass = "tab_normal"
        Me.LinkAgregaDistinciones.CssClass = "tab_seleccionado"

        Me.LblExperiencia.Text = "Modificar Distinciones"
        Me.LblExperiencia4.Text = "Modificar Publicaciones"
        Me.Panel1.Visible = False
        Me.Panel4.Visible = False

        'Me.Panel3.Visible = True
        'Me.Panel2.Visible = True

        Me.LinkAgregaDistinciones.Attributes.Remove("OnMouseOver")
        Me.LinkAgregaDistinciones.Attributes.Remove("OnMouseOut")

        Me.LinkVistaDistinciones.Attributes.Add("OnMouseOver", "tabsobre(this,1)")
        Me.LinkVistaDistinciones.Attributes.Add("OnMouseOut", "tabsobre(this,2)")


        If Me.GridView1.DataKeys.Item(e.NewEditIndex).Value IsNot Nothing Then

            '------------------------------------------------------------------------------------
            'Esta linea de codigo no estaba, razon por la cual no se podia modificar, 
            'Agragado xDguevara() 23.10.2012
            HddDistinciones.Value = Me.GridView1.DataKeys.Item(e.NewEditIndex).Value
            '------------------------------------------------------------------------------------


            Dim datos As System.Data.DataTable
            Dim ObjDistin As New Personal
            datos = ObjDistin.ObtieneDistinciones(Me.GridView1.DataKeys.Item(e.NewEditIndex).Value, "MO")
            With datos.Rows(0)
                Me.TxtDistincion.Text = .Item("nombre_dis")
                Me.TxtOtorgado.Text = .Item("otorgado_dis")
                Me.TxtCiudad.Text = .Item("ciudad_dis")
                Me.TxtDescripcion.Text = .Item("motivo_dis")
                Me.TxtFecha.Text = .Item("fechaentrega_dis")
                Me.DDLDistinciones.SelectedValue = .Item("codigo_tdis")
            End With
            ObjDistin = Nothing
            datos = Nothing
        End If
        e.Cancel = True
    End Sub

    Private Sub GuardarDistinciones()

        Dim ObjPersonal As New Personal
        ObjPersonal.codigo = Request.QueryString("id")
        Dim valor As Integer

        If Me.HddDistinciones.Value = "-1" Then
            valor = ObjPersonal.GrabarDistinciones(Me.TxtDistincion.Text, Me.TxtOtorgado.Text, Me.TxtCiudad.Text, Me.TxtDescripcion.Text, Me.DDLDistinciones.SelectedValue, CType(Me.TxtFecha.Text, DateTime))
        Else
            valor = ObjPersonal.ModificarDistinciones(Me.TxtDistincion.Text, _
                                                      Me.TxtOtorgado.Text, _
                                                      Me.TxtCiudad.Text, _
                                                      Me.TxtDescripcion.Text, _
                                                      Me.DDLDistinciones.SelectedValue, _
                                                      CType(Me.TxtFecha.Text, DateTime), _
                                                      Me.HddDistinciones.Value)
        End If
        If valor = -1 Then
            Dim MsgError As String
            MsgError = "<script>alert('Ocurrio un error al grabar los datos, intentelo nuevamente')</script>"
            Page.RegisterStartupScript("Error", MsgError)
        Else
            Dim MsgExito As String
            Me.GridView1.DataBind()
            Me.LinkVistaDistinciones.CssClass = "tab_seleccionado"
            Me.LinkAgregaDistinciones.CssClass = "tab_normal"
            Me.Panel1.Visible = True
            Me.Panel4.Visible = True
            Me.Panel3.Visible = False
            Me.Panel2.Visible = False

            Me.LinkVistaDistinciones.Attributes.Remove("OnMouseOver")
            Me.LinkVistaDistinciones.Attributes.Remove("OnMouseOut")

            Me.LinkAgregaDistinciones.Attributes.Add("OnMouseOver", "tabsobre(this,1)")
            Me.LinkAgregaDistinciones.Attributes.Add("OnMouseOut", "tabsobre(this,2)")

            MsgExito = "<script>alert('Se guadaron los datos correctamente')</script>"
            Page.RegisterStartupScript("Exito", MsgExito)
        End If
        ObjPersonal = Nothing
    End Sub

    'Se ejecuta cuando acepta el modal
    Protected Sub btnGuardarInforme_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardarInforme.Click
        Try
            'Response.Write("acepta modal ......")
            Dim objPersonal As New Personal
            Dim i As Integer = objPersonal.ActualizarEstadoDeclaracionJurada(Request.QueryString("id"))
            'Response.Write("<br/>")
            'Response.Write("i=> " & i)
            'Response.Write("<br/>")
            'Response.Write("Session gp =>" & Session("gp"))

            If Session("gp") IsNot Nothing Then
                If Session("gp") = "Si" Then
                    GuardarPublicaciones()
                Else
                    GuardarDistinciones()
                End If
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub CmdGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdGuardar.Click
        '-----------------------------------------------------------------------------------------------
        ' Verifica que el trabajador haya aceptado la declaracion jurada para el registro de sus datos
        '-----------------------------------------------------------------------------------------------
        Session("gp") = "No"        '## no modificar, de lo contrario generará errores ##
        If Page.IsValid = True Then
            Dim objPersonal As New Personal
            Dim dts As New Data.DataTable
            objPersonal.codigo = Request.QueryString("id")
            dts = objPersonal.VerificaDeclaracionJuradaPersonal(Request.QueryString("id"))
            If dts.Rows.Count > 0 Then
                If dts.Rows(0).Item("rpt") = 0 Then
                    Me.lblDeclarante.Text = dts.Rows(0).Item("Declarante").ToString
                    mpeInforme.Show()
                Else
                    GuardarDistinciones()
                End If
            End If
        End If
        '----------::::::::::::
    End Sub

    Protected Sub CmdGuardar0_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdGuardar0.Click
        Response.Redirect("experiencia.aspx?menu=" & Request.QueryString("menu") & "&id=" & Request.QueryString("id") & "&ctf=" & Request.QueryString("ctf"))
    End Sub

    Protected Sub CmdGuardar1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdGuardar1.Click
        Response.Redirect("otros.aspx?menu=" & Request.QueryString("menu") & "&id=" & Request.QueryString("id") & "&ctf=" & Request.QueryString("ctf"))
    End Sub

    Protected Sub btnCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        Try

            ClientScript.RegisterStartupScript(Me.GetType, "FaltanDatos", "alert('Los datos consignados no fueron registrados.');", True)
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub cmd_GuardarPublicacion_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmd_GuardarPublicacion.Click
        Session("gp") = "Si"
        If Page.IsValid = True Then
            Dim objPersonal As New Personal
            Dim dts As New Data.DataTable
            objPersonal.codigo = Request.QueryString("id")
            dts = objPersonal.VerificaDeclaracionJuradaPersonal(Request.QueryString("id"))
            If dts.Rows.Count > 0 Then
                If dts.Rows(0).Item("rpt") = 0 Then
                    Me.lblDeclarante.Text = dts.Rows(0).Item("Declarante").ToString
                    mpeInforme.Show()
                Else
                    GuardarPublicaciones()
                End If
            End If
        End If
    End Sub
    Private Sub GuardarPublicaciones()
        Dim ObjPersonal As New Personal
        ObjPersonal.codigo = Request.QueryString("id")
        Dim valor As Integer

        If Me.HddDistinciones.Value = "-1" Then
            valor = ObjPersonal.GrabarPublicaciones(Me.txtnombre.Text.Trim, Me.txteditorial.Text.Trim, Me.combo_procedencia.SelectedItem.Text, Me.combo_autoria.SelectedItem.Text, Me.combo_tipo.SelectedItem.Text, Me.combo_sino.SelectedItem.Text)
        Else
            valor = ObjPersonal.ModificarPublicaciones(Me.txtnombre.Text.Trim, Me.txteditorial.Text.Trim, Me.combo_procedencia.SelectedItem.Text, Me.combo_autoria.SelectedItem.Text, Me.combo_tipo.SelectedItem.Text, Me.combo_sino.SelectedItem.Text, Me.HddDistinciones.Value)
        End If
        If valor = -1 Then
            Dim MsgError As String
            MsgError = "<script>alert('Ocurrio un error al grabar los datos, intentelo nuevamente')</script>"
            Page.RegisterStartupScript("Error", MsgError)
        Else
            Dim MsgExito As String
            Me.GridView1.DataBind()
            Me.LinkVistaDistinciones.CssClass = "tab_seleccionado"
            Me.LinkAgregaDistinciones.CssClass = "tab_normal"


            Me.Panel1.Visible = True
            Me.Panel4.Visible = True
            Me.Panel3.Visible = False
            Me.Panel2.Visible = False

            Me.LinkVistaDistinciones.Attributes.Remove("OnMouseOver")
            Me.LinkVistaDistinciones.Attributes.Remove("OnMouseOut")


            MsgExito = "<script>alert('Se guadaron los datos correctamente')</script>"
            Page.RegisterStartupScript("Exito", MsgExito)
            CargarPublicaciones()
        End If
        ObjPersonal = Nothing
    End Sub

    Protected Sub combo_tipo_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles combo_tipo.SelectedIndexChanged
        If Me.combo_tipo.selecteditem.text = "Revista" Or Me.combo_tipo.selecteditem.text = "Working Paper" Then
            Me.lblTexto.text = "Indexado"
        Else
            Me.lblTexto.text = "Oficial de la institución"
        End If
    End Sub

    Protected Sub GridView2_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles GridView2.RowEditing
        Me.LinkVistaDistinciones.CssClass = "tab_normal"
        Me.LinkAgregaDistinciones.CssClass = "tab_seleccionado"

        Me.LblExperiencia.Text = "Modificar Distinciones"
        Me.LblExperiencia4.Text = "Modificar Publicaciones"
        Me.Panel1.Visible = False
        Me.Panel4.Visible = False
        Me.Panel3.Visible = True
        Me.Panel2.Visible = True

        Me.LinkAgregaDistinciones.Attributes.Remove("OnMouseOver")
        Me.LinkAgregaDistinciones.Attributes.Remove("OnMouseOut")

        Me.LinkVistaDistinciones.Attributes.Add("OnMouseOver", "tabsobre(this,1)")
        Me.LinkVistaDistinciones.Attributes.Add("OnMouseOut", "tabsobre(this,2)")

        If Me.GridView2.DataKeys.Item(e.NewEditIndex).Value IsNot Nothing Then
            '------------------------------------------------------------------------------------
            'Esta linea de codigo no estaba, razon por la cual no se podia modificar, 
            'Agragado xDguevara() 23.10.2012
            HddDistinciones.Value = Me.GridView2.DataKeys.Item(e.NewEditIndex).Value
            '------------------------------------------------------------------------------------
            Dim datos As System.Data.DataTable
            Dim ObjDistin As New Personal
            datos = ObjDistin.ConsultarPublicaciones(CInt(Request.QueryString("id")), Me.GridView2.DataKeys.Item(e.NewEditIndex).Value)
            With datos.Rows(0)
                Me.txtnombre.Text = .Item("nombre")
                Me.txteditorial.Text = .Item("editorial")
                Me.combo_procedencia.SelectedItem.Text = .Item("procedencia")
                Me.combo_autoria.SelectedItem.Text = .Item("autoria")
                Me.combo_tipo.SelectedItem.Text = .Item("tipo")
                Me.combo_sino.SelectedItem.Text = IIf(.Item("info_pub") = "S", "Si", "No")
            End With
            ObjDistin = Nothing
            datos = Nothing
        End If
        e.Cancel = True
    End Sub

    Protected Sub GridView2_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles GridView2.RowDeleting
        If Me.GridView2.DataKeys.Item(e.RowIndex).Value IsNot Nothing Then
            HddDistinciones.Value = Me.GridView2.DataKeys.Item(e.RowIndex).Value
            Dim ObjDistin As New Personal
            ObjDistin.QuitarPublicaciones(Me.GridView2.DataKeys.Item(e.RowIndex).Value)
            CargarPublicaciones()
        End If
        Me.GridView2.DataBind()
    End Sub

    Protected Sub GridView1_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GridView1.PageIndexChanging
        Me.GridView2.DataBind()
    End Sub

    Protected Sub GridView2_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GridView2.PageIndexChanging
        Me.GridView2.DataBind()
    End Sub
End Class
