Imports System.Data
Partial Class educacionuniversitaria
    Inherits System.Web.UI.Page

    Public tipoDato As String
    Public codigo_grado As Integer

    Protected Sub GridView1_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles GridView1.RowDeleting
        Dim objpersonal As New Personal
        objpersonal.codigo = CInt(Session("id"))
        objpersonal.QuitarTitulos(e.Keys.Item(0))
        objpersonal = Nothing
        e.Cancel = True
        Me.GridView1.DataBind()
    End Sub

    Protected Sub GridView2_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles GridView2.RowDeleting
        Dim objpersonal As New Personal
        objpersonal.codigo = CInt(Session("id"))
        objpersonal.QuitarGrados(e.Keys.Item(0))
        objpersonal = Nothing
        e.Cancel = True
        Me.GridView2.DataBind()
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Request.QueryString("id") <> "" And Session("id") = "" Then
            Session("Id") = Request.QueryString("id")
        End If

        If IsPostBack = False Then

            Me.LinkAgregaTitulo.Attributes.Add("OnMouseOver", "tabsobre(this,1)")
            Me.LinkAgregaTitulo.Attributes.Add("OnMouseOut", "tabsobre(this,2)")

            Me.LinkAgregaGrado.Attributes.Add("OnMouseOver", "tabsobre(this,1)")
            Me.LinkAgregaGrado.Attributes.Add("OnMouseOut", "tabsobre(this,2)")

            Me.CmdGuardar.Attributes.Add("OnMouseOver", "tabsobre(this,1)")
            Me.CmdGuardar.Attributes.Add("OnMouseOut", "tabsobre(this,2)")

            Me.CmdGrabarGrado.Attributes.Add("OnMouseOver", "tabsobre(this,1)")
            Me.CmdGrabarGrado.Attributes.Add("OnMouseOut", "tabsobre(this,2)")

            Call CargarInicioTitulos()
            Call CargarInicioGrados()
        End If


    End Sub

    Protected Sub LinkVistaTitulo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LinkVistaTitulo.Click
        Me.LinkVistaTitulo.CssClass = "tab_seleccionado"
        Me.LinkAgregaTitulo.CssClass = "tab_normal"
        Me.Panel1.Visible = True
        Me.Panel3.Visible = False

        Me.LinkVistaTitulo.Attributes.Remove("OnMouseOver")
        Me.LinkVistaTitulo.Attributes.Remove("OnMouseOut")

        Me.LinkAgregaTitulo.Attributes.Add("OnMouseOver", "tabsobre(this,1)")
        Me.LinkAgregaTitulo.Attributes.Add("OnMouseOut", "tabsobre(this,2)")

    End Sub

    Protected Sub LinkAgregaTitulo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LinkAgregaTitulo.Click
        Me.HddTitulo.Value = "-1"
        Me.LinkVistaTitulo.CssClass = "tab_normal"
        Me.LinkAgregaTitulo.CssClass = "tab_seleccionado"

        Me.LinkAgregaTitulo.Attributes.Remove("OnMouseOver")
        Me.LinkAgregaTitulo.Attributes.Remove("OnMouseOut")

        Me.LinkVistaTitulo.Attributes.Add("OnMouseOver", "tabsobre(this,1)")
        Me.LinkVistaTitulo.Attributes.Add("OnMouseOut", "tabsobre(this,2)")

        Me.Panel3.Visible = True
        Me.Panel1.Visible = False
        Me.LblTitulo.Text = "Registro de Titulos"

        For Each controles As Control In Me.Panel3.Controls
            If controles.GetType.ToString = "System.Web.UI.WebControls.DropDownList" Then
                CType(controles, DropDownList).SelectedIndex = -1
            End If
        Next
        Me.TxtOtros.Text = ""
        Me.TxtOtrosTitulo.Text = ""
    End Sub

    Protected Sub DDLInstitucion_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DDLInstitucion.SelectedIndexChanged
        Dim objcombo As New Combos
        objcombo.LlenaInstitucion(Me.DDLCentro, Me.DDLInstitucion.SelectedValue, Me.DDLProcedencia.SelectedValue)
        objcombo = Nothing
    End Sub

    Protected Sub DDLProcedencia_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DDLProcedencia.SelectedIndexChanged
        Dim objcombo As New Combos
        objcombo.LlenaInstitucion(Me.DDLCentro, Me.DDLInstitucion.SelectedValue, Me.DDLProcedencia.SelectedValue)
        objcombo = Nothing
    End Sub

    Protected Sub CargarInicioTitulos()
        Dim objCOmbo As New Combos
        objCOmbo.llenaTitulo(Me.DDLTitulo)
        objCOmbo.LlenaTipoInstitucion(Me.DDLInstitucion)
        objCOmbo.LlenaSituacion(Me.DDLSituacion)
        Me.DDLTitulo.Attributes.Add("onchange", "mostrarcaja();return false;")
        Me.DDLCentro.Attributes.Add("onchange", "mostrarcaja2();return false;")

        Me.DDLATitulo.Items.Add("Pendiente")
        Me.DDLATitulo.Items(0).Value = "3000" '"3000"


        Me.DDLAEgreso.Items.Add("Pendiente")
        Me.DDLAEgreso.Items(0).Value = "3000"  'staba 3000
        For i As Int16 = Year(Now) To 1940 Step -1
            Me.DDlAIngreso.Items.Add(i)
            Me.DDLAEgreso.Items.Add(i)
            Me.DDLATitulo.Items.Add(i)
        Next
        objCOmbo.LlenaInstitucion(Me.DDLCentro, "1", "1")
    End Sub

    Protected Sub CargarInicioGrados()
        Me.DDLGrado.Attributes.Add("onchange", "mostrarcajagrado();return false;")
        Me.DDLCentroGrado.Attributes.Add("onchange", "mostrarcaja2grado(); return false;")
        Dim objCOmbo As New Combos
        objCOmbo.LlenaTipoInstitucion(Me.DDLTipoInsGrado)
        objCOmbo.LlenaSituacion(Me.DDLSitGrado)
        Me.DDLAIngGrado.Items.Add("Pendiente")
        Me.DDLAIngGrado.Items(0).Value = "3000"
        Me.DDLAEgrGrado.Items.Add("Pendiente")
        Me.DDLAEgrGrado.Items(0).Value = "3000"
        Me.DDLATitGrado.Items.Add("Pendiente")
        Me.DDLATitGrado.Items(0).Value = "3000"
        For i As Int16 = Year(Now) To 1940 Step -1
            Me.DDLAIngGrado.Items.Add(i)
            Me.DDLAEgrGrado.Items.Add(i)
            Me.DDLATitGrado.Items.Add(i)
        Next
        objCOmbo.LlenaInstitucion(Me.DDLCentroGrado, "1", "1")
        objCOmbo.LlenaGrados(Me.DDLGrado, Me.DDLTipoGrado.SelectedValue)
    End Sub

    Protected Sub LinkAgregaGrado_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LinkAgregaGrado.Click
        Me.HddGrado.Value = "-1"


        Me.LinkVistaGrado.CssClass = "tab_normal"
        Me.LinkAgregaGrado.CssClass = "tab_seleccionado"

        Me.LinkAgregaGrado.Attributes.Remove("OnMouseOver")
        Me.LinkAgregaGrado.Attributes.Remove("OnMouseOut")

        Me.LinkVistaGrado.Attributes.Add("OnMouseOver", "tabsobre(this,1)")
        Me.LinkVistaGrado.Attributes.Add("OnMouseOut", "tabsobre(this,2)")


        Me.LblGrado.Text = "Registro de Grados Académicos"
        Me.Panel4.Visible = True
        Me.Panel2.Visible = False

        For Each controles As Control In Me.Panel4.Controls
            If controles.GetType.ToString = "System.Web.UI.WebControls.DropDownList" Then
                CType(controles, DropDownList).SelectedIndex = -1
            End If
        Next

        Me.TxtOtrosGrados.Text = ""
        Me.TxtMención.Text = ""
        Me.TxtOtrosCentroGrados.Text = ""

    End Sub

    Protected Sub LinkVistaGrado_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LinkVistaGrado.Click
        Me.LinkVistaGrado.CssClass = "tab_seleccionado"
        Me.LinkAgregaGrado.CssClass = "tab_normal"
        Me.Panel2.Visible = True
        Me.Panel4.Visible = False

        Me.LinkVistaGrado.Attributes.Remove("OnMouseOver")
        Me.LinkVistaGrado.Attributes.Remove("OnMouseOut")

        Me.LinkAgregaGrado.Attributes.Add("OnMouseOver", "tabsobre(this,1)")
        Me.LinkAgregaGrado.Attributes.Add("OnMouseOut", "tabsobre(this,2)")

        Me.HddGrado.Value = "-1"
    End Sub

    Protected Sub GridView1_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles GridView1.RowEditing

        Me.LinkVistaTitulo.CssClass = "tab_normal"
        Me.LinkAgregaTitulo.CssClass = "tab_seleccionado"

        Me.LblTitulo.Text = "Modificar un Título"

        Me.Panel1.Visible = False
        Me.Panel3.Visible = True

        Me.LinkAgregaTitulo.Attributes.Remove("OnMouseOver")
        Me.LinkAgregaTitulo.Attributes.Remove("OnMouseOut")

        Me.LinkVistaTitulo.Attributes.Add("OnMouseOver", "tabsobre(this,1)")
        Me.LinkVistaTitulo.Attributes.Add("OnMouseOut", "tabsobre(this,2)")

        Dim ObjCombo As New Combos
        If Me.GridView1.DataKeys.Item(e.NewEditIndex).Value IsNot Nothing Then
            Dim objTitulo As New Personal
            Dim datos As DataTable
            '---------------------------------------------------------------------
            'Me.HddTitulo.Value = Me.GridView1.DataKeys.Item(0).Value
            '---------------------------------------------------------------------
            datos = objTitulo.ObtieneDatosTitulos(Me.GridView1.DataKeys.Item(e.NewEditIndex).Value, "MO")
            With datos.Rows(0)

                'Codigo Principal
                '---------------------------------------------------------------
                Me.HddTitulo.Value = .Item("codigo_tpr").ToString
                '---------------------------------------------------------------

                Me.DDLTitulo.SelectedValue = .Item("codigo_tpf").ToString
                Me.DDLInstitucion.SelectedValue = .Item("codigo_tis").ToString
                Me.DDLSituacion.SelectedValue = .Item("codigo_sit").ToString
                Me.DDlAIngreso.SelectedValue = .Item("anioingreso_tpr").ToString


                'xDguevara para subsanar la programacion encontrada.
                '-----------------------------------------------------------------------
                If .Item("anioegreso_tpr").ToString = "0" Then
                    Me.DDLAEgreso.SelectedValue = "3000"
                Else
                    Me.DDLAEgreso.SelectedValue = .Item("anioegreso_tpr").ToString
                End If

                '-----------------------------------------------------------------------
                If .Item("aniograd_tpr").ToString = "0" Then
                    Me.DDLATitulo.SelectedValue = "3000"
                Else
                    Me.DDLATitulo.SelectedValue = .Item("aniograd_tpr").ToString
                End If
                '------------------------------------------------------------------------




                Me.TxtOtrosTitulo.Text = .Item("nombretitulo_tpr").ToString
                Me.TxtOtros.Text = .Item("universidad_tpr").ToString
                If datos.Rows(0).Item("codigo_pai").ToString = "156" Then
                    Me.DDLProcedencia.SelectedValue = 1
                    ObjCombo.LlenaInstitucion(Me.DDLCentro, .Item("codigo_tis").ToString, "1")
                Else
                    Me.DDLProcedencia.SelectedValue = 2
                    ObjCombo.LlenaInstitucion(Me.DDLCentro, .Item("codigo_tis").ToString, "2")
                End If
                Me.DDLCentro.SelectedValue = .Item("codigo_ins").ToString()
            End With
            objTitulo = Nothing
        Else
            ObjCombo.LlenaInstitucion(Me.DDLCentro, "1", "1")
        End If
        e.Cancel = True
    End Sub

    Public Sub GuardarTitulos()
        Dim objpersonal As New Personal
        Dim codTpf, anoing, anoegr, anotitu, codins, codsit As Integer
        Dim strinstitucion, strtitulo As String
        Dim valor As Int16
        Try
            codTpf = CInt(Me.DDLTitulo.SelectedValue)
            anoing = CInt(Me.DDlAIngreso.SelectedValue)
            anoegr = CInt(Me.DDLAEgreso.SelectedValue)
            If anoegr = 3000 Then
                anoegr = 0
            End If
            anotitu = CInt(Me.DDLATitulo.SelectedValue)
            If anotitu = 3000 Then
                anotitu = 0
            End If
            codins = CInt(Me.DDLCentro.SelectedValue)
            codsit = CInt(Me.DDLSituacion.SelectedValue)
            strinstitucion = Me.TxtOtros.Text
            strtitulo = Me.TxtOtrosTitulo.Text
            objpersonal.codigo = CInt(Request.QueryString("id"))
            If Me.HddTitulo.Value = "-1" Then
                valor = objpersonal.GrabarTitulos(codTpf, strtitulo, anoing, anoegr, anotitu, strinstitucion, codsit, codins)
            Else
                valor = objpersonal.ModificaTitulos(codTpf, strtitulo, anoing, anoegr, anotitu, strinstitucion, codsit, codins, Me.HddTitulo.Value)
            End If

            If valor = -1 Then
                Dim mensaje As String
                mensaje = "<script>alert('Ocurrió un error al procesar los datos.')</script>"
                Page.RegisterStartupScript("Error", mensaje)
            Else
                Me.GridView1.DataBind()

                Me.LinkVistaTitulo.CssClass = "tab_seleccionado"
                Me.LinkAgregaTitulo.CssClass = "tab_normal"
                Me.Panel1.Visible = True
                Me.Panel3.Visible = False

                Me.LinkVistaTitulo.Attributes.Remove("OnMouseOver")
                Me.LinkVistaTitulo.Attributes.Remove("OnMouseOut")

                Me.LinkAgregaTitulo.Attributes.Add("OnMouseOver", "tabsobre(this,1)")
                Me.LinkAgregaTitulo.Attributes.Add("OnMouseOut", "tabsobre(this,2)")

                Dim mensaje As String
                mensaje = "<script>alert('Se guardaron los datos correctamente.')</script>"
                Page.RegisterStartupScript("Exito", mensaje)

            End If
        Catch ex As Exception
            Dim mensaje As String
            mensaje = "<script>alert('Ocurrió un error al procesar los datos.')</script>"
            Page.RegisterStartupScript("Error", mensaje)
        End Try
    End Sub

    'Se ejecuta cuando acepta el modal
    Protected Sub btnGuardarInforme_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardarInforme.Click
        Try
            Dim objPersonal As New Personal
            Dim i As Integer = objPersonal.ActualizarEstadoDeclaracionJurada(Request.QueryString("id"))

            If hfTipo.Value = "T" Then
                GuardarTitulos()
            Else
                GuardarGrado()
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub btnCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        Try
            ClientScript.RegisterStartupScript(Me.GetType, "FaltanDatos", "alert('Los datos consignados no fueron registrados.');", True)
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub CmdGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdGuardar.Click
        '-----------------------------------------------------------------------------------------------
        ' Verifica que el trabajador haya aceptado la declaracion jurada para el registro de sus datos
        '-----------------------------------------------------------------------------------------------
        hfTipo.Value = "T"
        Dim objPersonal As New Personal
        Dim dts As New Data.DataTable
        objPersonal.codigo = Request.QueryString("id")
        dts = objPersonal.VerificaDeclaracionJuradaPersonal(Request.QueryString("id"))
        If dts.Rows.Count > 0 Then
            If dts.Rows(0).Item("rpt") = 0 Then
                Me.lblDeclarante.Text = dts.Rows(0).Item("Declarante").ToString
                tipoDato = "T"
                mpeInforme.Show()
            Else
                GuardarTitulos()
            End If
        End If
        '-----------------------------------------------------------------------------------------------
    End Sub

    Protected Sub GridView2_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles GridView2.RowEditing


        Me.HddGrado.Value = Me.GridView2.DataKeys.Item(0).Value

        Me.LinkVistaGrado.CssClass = "tab_normal"
        Me.LinkAgregaGrado.CssClass = "tab_seleccionado"

        Me.LinkAgregaGrado.Attributes.Remove("OnMouseOver")
        Me.LinkAgregaGrado.Attributes.Remove("OnMouseOut")

        Me.LinkVistaGrado.Attributes.Add("OnMouseOver", "tabsobre(this,1)")
        Me.LinkVistaGrado.Attributes.Add("OnMouseOut", "tabsobre(this,2)")

        Me.LblGrado.Text = "Modificar un Grado Académico"

        Me.Panel4.Visible = True
        Me.Panel2.Visible = False

        Dim Objcombo As New Combos
        If Me.GridView2.DataKeys.Item(e.NewEditIndex).Value IsNot Nothing Then
            Dim objGrados As New Personal
            Dim Datos As DataTable
            '---------------------------------------------------------------------------
            'lblCod.Text = Me.GridView2.DataKeys.Item(0).Value
            'Me.HddGrado.Value = Me.GridView2.DataKeys.Item(0).Value
            '---------------------------------------------------------------------------

            Datos = objGrados.ObtieneDatosGrados(Me.GridView2.DataKeys.Item(e.NewEditIndex).Value, "MO")
            With Datos.Rows(0)
                'He tenido que poner este bloque debido a que estaba fallando al momneto de editar
                '-------------------------------------------------------------------------------------
                Me.HddGrado.Value = .Item("codigo_gpr")
                lblCod.Text = HddGrado.Value
                '-------------------------------------------------------------------------------------


                Me.DDLTipoGrado.SelectedValue = .Item("codigo_tgr")
                Objcombo.LlenaGrados(Me.DDLGrado, Me.DDLTipoGrado.SelectedValue)
                Me.DDLGrado.SelectedValue = .Item("codigo_gra")
                Me.TxtOtrosGrados.Text = .Item("desgrado_gpr")
                Me.TxtMención.Text = .Item("mencion_gpr")
                Me.DDLAIngGrado.SelectedValue = .Item("anioingreso_gpr")

                If .Item("anioegreso_gpr").ToString = "0" Then
                    Me.DDLAEgrGrado.SelectedValue = "3000"
                Else
                    Me.DDLAEgrGrado.SelectedValue = .Item("anioegreso_gpr").ToString
                End If

                If .Item("aniograd_gpr").ToString = "0" Then
                    Me.DDLATitGrado.SelectedValue = "3000"
                Else
                    Me.DDLATitGrado.SelectedValue = .Item("aniograd_gpr").ToString
                End If



                If Datos.Rows(0).Item("codigo_pai").ToString = "156" Then
                    Me.DDLProcedencia.SelectedValue = 1
                    Objcombo.LlenaInstitucion(Me.DDLCentroGrado, .Item("codigo_tis").ToString, "1")
                Else
                    Me.DDLProcedencia.SelectedValue = 2
                    Objcombo.LlenaInstitucion(Me.DDLCentroGrado, .Item("codigo_tis").ToString, "2")
                End If
                Me.DDLCentroGrado.SelectedValue = .Item("codigo_ins")
                Me.DDLSitGrado.SelectedValue = .Item("codigo_sit")
                Me.TxtOtrosCentroGrados.Text = .Item("universidad_gpr")
            End With
            objGrados = Nothing
        End If
        Objcombo = Nothing

        e.Cancel = True
    End Sub

    Protected Sub DDLTipoInsGrado_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DDLTipoInsGrado.SelectedIndexChanged
        Dim objcombo As New Combos
        objcombo.LlenaInstitucion(Me.DDLCentroGrado, Me.DDLTipoInsGrado.SelectedValue, Me.DDLProcedienciaGrado.SelectedValue)
        objcombo = Nothing
    End Sub

    Protected Sub DDLProcedienciaGrado_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DDLProcedienciaGrado.SelectedIndexChanged
        Dim objcombo As New Combos
        objcombo.LlenaInstitucion(Me.DDLCentroGrado, Me.DDLTipoInsGrado.SelectedValue, Me.DDLProcedienciaGrado.SelectedValue)
        objcombo = Nothing
    End Sub

    Protected Sub DDLTipoGrado_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DDLTipoGrado.SelectedIndexChanged
        Dim objCombo As New Combos
        objCombo.LlenaGrados(Me.DDLGrado, Me.DDLTipoGrado.SelectedValue)
        objCombo = Nothing
    End Sub

    Public Sub GuardarGrado()

        Dim objpersonal As New Personal
        Dim codGpr, anoing, anoegr, anotitu, codins, codsit As Integer
        Dim strinstitucion As String
        Dim StrMencion As String
        Dim strgrado As String
        Dim valor As Int16
        Try
            codGpr = CInt(Me.DDLGrado.SelectedValue)
            anoing = CInt(Me.DDLAIngGrado.SelectedValue)
            anoegr = CInt(Me.DDLAEgrGrado.SelectedValue)
            If anoegr = 3000 Then
                anoegr = 0
            End If
            anotitu = CInt(Me.DDLATitGrado.SelectedValue)
            If anotitu = 3000 Then
                anotitu = 0
            End If
            codins = CInt(Me.DDLCentroGrado.SelectedValue)
            codsit = CInt(Me.DDLSitGrado.SelectedValue)
            strinstitucion = Me.TxtOtrosCentroGrados.Text
            StrMencion = Me.TxtMención.Text
            strgrado = Me.TxtOtrosGrados.Text
            objpersonal.codigo = CInt(Request.QueryString("id"))

            If Me.HddGrado.Value = "-1" Then
                valor = objpersonal.GrabarGrados(codGpr, strgrado, anoing, anoegr, anotitu, StrMencion, strinstitucion, codsit, codins)
            Else
                valor = objpersonal.ModificaGrados(codGpr, strgrado, anoing, anoegr, anotitu, StrMencion, strinstitucion, codsit, codins, Me.HddGrado.Value)
            End If

            If valor = -1 Then
                Dim MensajeErr As String
                MensajeErr = "<script>alert('Ocurrio un error, intentelo nuevamente')</script>"
                Page.RegisterStartupScript("Error", MensajeErr)
            Else
                Me.GridView2.DataBind()
                Me.LinkVistaGrado.CssClass = "tab_seleccionado"
                Me.LinkAgregaGrado.CssClass = "tab_normal"
                Me.Panel2.Visible = True
                Me.Panel4.Visible = False

                Me.LinkVistaGrado.Attributes.Remove("OnMouseOver")
                Me.LinkVistaGrado.Attributes.Remove("OnMouseOut")

                Me.LinkAgregaGrado.Attributes.Add("OnMouseOver", "tabsobre(this,1)")
                Me.LinkAgregaGrado.Attributes.Add("OnMouseOut", "tabsobre(this,2)")

                Dim mensaje As String
                mensaje = "<script>alert('Se guardaron los datos correctamente.')</script>"
                Page.RegisterStartupScript("Exito", mensaje)
            End If
        Catch ex As Exception
            Dim MensajeErr As String
            MensajeErr = "<script>alert('Ocurrio un error, intentelo nuevamente')</script>"
            Page.RegisterStartupScript("Error", MensajeErr)
            objpersonal = Nothing
        End Try

    End Sub

    Protected Sub CmdGrabarGrado_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdGrabarGrado.Click
        '-----------------------------------------------------------------------------------------------
        ' Verifica que el trabajador haya aceptado la declaracion jurada para el registro de sus datos
        '-----------------------------------------------------------------------------------------------
        If Page.IsValid = True Then
            hfTipo.Value = "G"
            Dim objPersonal As New Personal
            Dim dts As New Data.DataTable
            objPersonal.codigo = Request.QueryString("id")
            dts = objPersonal.VerificaDeclaracionJuradaPersonal(Request.QueryString("id"))
            If dts.Rows.Count > 0 Then
                If dts.Rows(0).Item("rpt") = 0 Then
                    Me.lblDeclarante.Text = dts.Rows(0).Item("Declarante").ToString
                    mpeInforme.Show()
                Else
                    GuardarGrado()
                End If
            End If
        End If
        '-----------------------------------------------------------------------------------------------
    End Sub



    Protected Sub CmdGuardar0_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdGuardar0.Click
        Response.Redirect("perfil.aspx?menu=" & Request.QueryString("menu") & "&id=" & Request.QueryString("id") & "&ctf=" & Request.QueryString("ctf"))
    End Sub

    Protected Sub CmdGuardar1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdGuardar1.Click
        Response.Redirect("idiomas.aspx?menu=" & Request.QueryString("menu") & "&id=" & Request.QueryString("id") & "&ctf=" & Request.QueryString("ctf"))
    End Sub

    Protected Sub GridView2_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridView2.SelectedIndexChanged

    End Sub

    Protected Sub GridView1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridView1.SelectedIndexChanged

    End Sub
End Class
