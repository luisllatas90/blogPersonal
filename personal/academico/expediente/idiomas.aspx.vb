Imports System.Data
Partial Class idiomas
    Inherits System.Web.UI.Page

    Public Tipo As String

    Protected Sub GridView1_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles GridView1.RowDeleting
        Dim objpersonal As New Personal
        objpersonal.codigo = CInt(Session("Id"))
        objpersonal.Quitaridiomas(e.Keys.Item(0))
        objpersonal = Nothing
        e.Cancel = True
        Me.GridView1.DataBind()
    End Sub

    Protected Sub GridView2_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles GridView2.RowDeleting
        Dim objpersonal As New Personal
        objpersonal.codigo = CInt(Session("Id"))
        objpersonal.QuitarOtros(e.Keys.Item(0))
        objpersonal = Nothing
        e.Cancel = True
        Me.GridView2.DataBind()
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Request.QueryString("id") <> "" And Session("id") = "" Then
            Session("Id") = Request.QueryString("id")
        End If

        If IsPostBack = False Then

            Me.LinkAgregaIdioma.Attributes.Add("OnMouseOver", "tabsobre(this,1)")
            Me.LinkAgregaIdioma.Attributes.Add("OnMouseOut", "tabsobre(this,2)")

            Me.LinkAgregaOtros.Attributes.Add("OnMouseOver", "tabsobre(this,1)")
            Me.LinkAgregaOtros.Attributes.Add("OnMouseOut", "tabsobre(this,2)")

            Me.CmdGuardar.Attributes.Add("OnMouseOver", "tabsobre(this,1)")
            Me.CmdGuardar.Attributes.Add("OnMouseOut", "tabsobre(this,2)")

            Me.CmdGuardarOtros.Attributes.Add("OnMouseOver", "tabsobre(this,1)")
            Me.CmdGuardarOtros.Attributes.Add("OnMouseOut", "tabsobre(this,2)")

            Call CargaInicioIdiomas()
            Call CargaInicioOtros()
        End If

    End Sub

    Protected Sub LinkVistaIdioma_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LinkVistaIdioma.Click
        Me.LinkVistaIdioma.CssClass = "tab_seleccionado"
        Me.LinkAgregaIdioma.CssClass = "tab_normal"
        Me.Panel1.Visible = True
        Me.Panel3.Visible = False

        Me.LinkVistaIdioma.Attributes.Remove("OnMouseOver")
        Me.LinkVistaIdioma.Attributes.Remove("OnMouseOut")

        Me.LinkAgregaIdioma.Attributes.Add("OnMouseOver", "tabsobre(this,1)")
        Me.LinkAgregaIdioma.Attributes.Add("OnMouseOut", "tabsobre(this,2)")
    End Sub

    Protected Sub LinkAgregaIdioma_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LinkAgregaIdioma.Click
        Me.HddIdioma.Value = "-1"
        Me.LinkVistaIdioma.CssClass = "tab_normal"
        Me.LinkAgregaIdioma.CssClass = "tab_seleccionado"

        Me.LinkAgregaIdioma.Attributes.Remove("OnMouseOver")
        Me.LinkAgregaIdioma.Attributes.Remove("OnMouseOut")

        Me.LinkVistaIdioma.Attributes.Add("OnMouseOver", "tabsobre(this,1)")
        Me.LinkVistaIdioma.Attributes.Add("OnMouseOut", "tabsobre(this,2)")

        Me.Panel3.Visible = True
        Me.Panel1.Visible = False
        Me.LblIdioma.Text = "Registro de Idiomas"

        For Each controles As Control In Me.Panel3.Controls
            If controles.GetType.ToString = "System.Web.UI.WebControls.DropDownList" Then
                CType(controles, DropDownList).SelectedIndex = -1
            End If
        Next
        Me.TxtOtros.Text = ""
        Me.TxtOtros.Text = ""
    End Sub

    Protected Sub LinkVistaOtros_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LinkVistaOtros.Click
        Me.LinkVistaOtros.CssClass = "tab_seleccionado"
        Me.LinkAgregaOtros.CssClass = "tab_normal"
        Me.Panel2.Visible = True
        Me.Panel4.Visible = False

        Me.LinkVistaOtros.Attributes.Remove("OnMouseOver")
        Me.LinkVistaOtros.Attributes.Remove("OnMouseOut")

        Me.LinkAgregaOtros.Attributes.Add("OnMouseOver", "tabsobre(this,1)")
        Me.LinkAgregaOtros.Attributes.Add("OnMouseOut", "tabsobre(this,2)")
    End Sub

    Protected Sub LinkAgregaOtros_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LinkAgregaOtros.Click
        Me.HddOtros.Value = "-1"
        Me.LinkVistaOtros.CssClass = "tab_normal"
        Me.LinkAgregaOtros.CssClass = "tab_seleccionado"

        Me.LinkAgregaOtros.Attributes.Remove("OnMouseOver")
        Me.LinkAgregaOtros.Attributes.Remove("OnMouseOut")

        Me.LinkVistaOtros.Attributes.Add("OnMouseOver", "tabsobre(this,1)")
        Me.LinkVistaOtros.Attributes.Add("OnMouseOut", "tabsobre(this,2)")


        Me.LblOtros.Text = "Registro de Otros Estudios Realizados"
        Me.Panel4.Visible = True
        Me.Panel2.Visible = False

        For Each controles As Control In Me.Panel4.Controls
            If controles.GetType.ToString = "System.Web.UI.WebControls.DropDownList" Then
                CType(controles, DropDownList).SelectedIndex = -1
            End If
        Next
        Me.TxtEstudio.Text = ""
        Me.TxtOtroCentroArea.Text = ""
        Me.TxtObservacionArea.Text = ""
        TxtOtroCentroArea.Enabled = True
    End Sub

    Protected Sub CargaInicioIdiomas()
        Dim objCombo As New Combos
        Dim i As Int16
        For i = Year(Date.Now) To 1960 Step -1
            Me.DDlAno.Items.Add(i)
        Next
        Me.DDLCentro.Attributes.Add("onchange", "mostrarcaja2(); return false;")
        objCombo.LlenaIdiomas(Me.DDLIdioma, "")
        objCombo.LlenaTipoInstitucion(Me.DDLInstitucion)
        objCombo.LlenaSituacion(Me.DDLSituacion)
        objCombo.LlenaInstitucion(Me.DDLCentro, "1", "1")
    End Sub

    Protected Sub CargaInicioOtros()
        Dim ObjCombo As New Combos
        Me.DDLCentro.Attributes.Add("onchange", "mostrarcaja2(); return false;")
        ObjCombo.LlenaAreaEstudio(Me.DDLArea)
        ObjCombo.LlenaTipoInstitucion(Me.DDLTipoInsArea)
        ObjCombo.LlenaSituacion(Me.DDLSitArea)
        Me.DDLAnioFin.Items.Add("En Curso")
        Me.DDLAnioFin.Items(0).Value = 0
        For i As Integer = Now.Year To 1980 Step -1
            Me.DDLAnioIni.Items.Add(i)
            Me.DDLAnioFin.Items.Add(i)
        Next
        ObjCombo.LlenaInstitucion(Me.DDLCentroArea, "1", "1")
    End Sub

    Protected Sub GridView1_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles GridView1.RowEditing
        Me.LinkVistaIdioma.CssClass = "tab_normal"
        Me.LinkAgregaIdioma.CssClass = "tab_seleccionado"

        Me.LblIdioma.Text = "Modificar un Idioma"

        Me.Panel1.Visible = False
        Me.Panel3.Visible = True

        Me.LinkAgregaIdioma.Attributes.Remove("OnMouseOver")
        Me.LinkAgregaIdioma.Attributes.Remove("OnMouseOut")

        Me.LinkVistaIdioma.Attributes.Add("OnMouseOver", "tabsobre(this,1)")
        Me.LinkVistaIdioma.Attributes.Add("OnMouseOut", "tabsobre(this,2)")

        Dim objCombo As New Combos
        If Me.GridView1.DataKeys(e.NewEditIndex).Value IsNot Nothing Then
            Me.HddIdioma.Value = Me.GridView1.DataKeys(e.NewEditIndex).Value
            Dim ObjPersonal As New Personal
            Dim Datos As DataTable
            Datos = ObjPersonal.ObtieneDatosIdiomas(Me.GridView1.DataKeys(e.NewEditIndex).Value, "MO")
            With Datos.Rows(0)
                Me.DDLIdioma.SelectedValue = .Item("codigo_idi")
                Me.DDlAno.SelectedValue = .Item("aniograduacion")
                Me.DDLSituacion.SelectedValue = .Item("codigo_sit")
                Me.DDLInstitucion.SelectedValue = .Item("codigo_tis")
                If .Item("codigo_pai") = "156" Then
                    Me.DDLProcedencia.SelectedValue = 1
                    objCombo.LlenaInstitucion(Me.DDLCentro, .Item("codigo_tis"), "1")
                Else
                    Me.DDLProcedencia.SelectedValue = 2
                    objCombo.LlenaInstitucion(Me.DDLCentro, .Item("codigo_tis"), "2")
                End If
                Me.DDLCentro.SelectedValue = .Item("codigo_ins")
                Me.TxtOtros.Text = .Item("centroestudios").ToString
                Me.TXtObservaciones.Text = .Item("observaciones").ToString
                Me.DDLLee.SelectedValue = .Item("lee")
                Me.DDLHabla.SelectedValue = .Item("habla")
                Me.DDLEscribe.SelectedValue = .Item("escribe")
            End With
        End If

        e.Cancel = True
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

    Protected Sub GridView2_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles GridView2.RowEditing
        Me.LinkVistaOtros.CssClass = "tab_normal"
        Me.LinkAgregaOtros.CssClass = "tab_seleccionado"

        Me.LinkAgregaOtros.Attributes.Remove("OnMouseOver")
        Me.LinkAgregaOtros.Attributes.Remove("OnMouseOut")

        Me.LinkVistaOtros.Attributes.Add("OnMouseOver", "tabsobre(this,1)")
        Me.LinkVistaOtros.Attributes.Add("OnMouseOut", "tabsobre(this,2)")

        Me.LblOtros.Text = "Modificar Otros Estudios Realizados"

        Me.Panel4.Visible = True
        Me.Panel2.Visible = False

        Dim objcombo As New Combos
        If Me.GridView2.DataKeys.Item(e.NewEditIndex).Value IsNot Nothing Then
            Dim ObjOtros As New Personal
            Dim datos As New DataTable
            Me.HddOtros.Value = Me.GridView2.DataKeys.Item(e.NewEditIndex).Value
            datos = ObjOtros.ObtieneDatosOtros(Me.GridView2.DataKeys.Item(e.NewEditIndex).Value, "MO")
            With datos.Rows(0)
                Me.DDLArea.SelectedValue = .Item("codigo_areaes")
                Me.TxtEstudio.Text = .Item("nombre_est")
                Me.DDLTipoInsArea.SelectedValue = .Item("codigo_tis")
                If .Item("codigo_pai") = "156" Then
                    Me.DDLProcArea.SelectedValue = 1
                    objcombo.LlenaInstitucion(Me.DDLCentroArea, .Item("codigo_tis"), "1")
                Else
                    Me.DDLProcedencia.SelectedValue = 2
                    objcombo.LlenaInstitucion(Me.DDLCentroArea, .Item("codigo_tis"), "2")
                End If
                Me.TxtOtros.Text = .Item("nombre_ins")
                Me.DDLMesIni.Text = .Item("mes_inicio")
                Me.DDLAnioIni.SelectedValue = .Item("anio_inicio")
                Me.DDLMesFin.Text = .Item("mes_fin")
                Me.DDLAnioFin.SelectedValue = .Item("anio_fin")
                Me.DDLModalidad.SelectedValue = .Item("tipo_mod")
                Me.DDLCursa.SelectedValue = .Item("actual_estudio")
                Me.DDLSitArea.SelectedValue = .Item("codigo_sit")
                Me.DDLCentroArea.SelectedValue = .Item("codigo_ins")
                Me.TxtObservacionArea.Text = .Item("observacion")
            End With
        End If

        e.Cancel = True
    End Sub

    Public Sub GuardarIdiomasExtrangeros()
        Dim Objpersonal As Personal
        Dim intIdioma, intAno, intTipoIns, intCentro, valor As Int16
        Dim intLee, intHabla, intEscribe, intSituacion As Int16
        Dim strOtros = "", strObservaciones As String
        Try
            intIdioma = Me.DDLIdioma.SelectedValue
            intAno = Me.DDlAno.SelectedValue
            intTipoIns = Me.DDLInstitucion.SelectedValue
            intCentro = Me.DDLCentro.SelectedValue
            If intCentro = 1 Or (intCentro >= 190 And intCentro <= 204) Then
                strOtros = Me.TxtOtros.Text
            End If
            intLee = Me.DDLLee.SelectedValue
            intHabla = Me.DDLHabla.SelectedValue
            intEscribe = Me.DDLEscribe.SelectedValue
            strObservaciones = Me.TXtObservaciones.Text
            intSituacion = Me.DDLSituacion.SelectedValue
            Objpersonal = New Personal
            Objpersonal.codigo = Request.QueryString("id")
            If Me.HddIdioma.Value = "-1" Then
                valor = Objpersonal.GrabarIdiomas(intSituacion, intIdioma, strOtros, intAno, strObservaciones, intLee, intHabla, intEscribe, intCentro)
            Else
                valor = Objpersonal.ModificaIdiomas(intSituacion, intIdioma, strOtros, intAno, strObservaciones, intLee, intHabla, intEscribe, intCentro, Me.HddIdioma.Value)
            End If
            Objpersonal = Nothing
            If valor = -1 Then
                Dim MsgError As String
                MsgError = "<script>alert('Ocurrió un error, intentelo nuevamente.')</script>"
                Page.RegisterStartupScript("Error", MsgError)
            Else
                Dim MsgExito As String
                Me.GridView1.DataBind()

                Me.LinkVistaIdioma.CssClass = "tab_seleccionado"
                Me.LinkAgregaIdioma.CssClass = "tab_normal"
                Me.Panel1.Visible = True
                Me.Panel3.Visible = False

                Me.LinkVistaIdioma.Attributes.Remove("OnMouseOver")
                Me.LinkVistaIdioma.Attributes.Remove("OnMouseOut")

                Me.LinkAgregaIdioma.Attributes.Add("OnMouseOver", "tabsobre(this,1)")
                Me.LinkAgregaIdioma.Attributes.Add("OnMouseOut", "tabsobre(this,2)")

                MsgExito = "<script>alert('Se guardaron los datos satisfactoriamente.')</script>"
                Page.RegisterStartupScript("Error", MsgExito)
            End If
        Catch ex As Exception
            Objpersonal = Nothing
            Dim MsgError As String
            MsgError = "<script>alert('Ocurrió un error, intentelo nuevamente.')</script>"
            Page.RegisterStartupScript("Error", MsgError)
        End Try
    End Sub


    'Se ejecuta cuando acepta el modal
    Protected Sub btnGuardarInforme_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardarInforme.Click
        Try
            Dim objPersonal As New Personal
            Dim i As Integer = objPersonal.ActualizarEstadoDeclaracionJurada(Request.QueryString("id"))
            If Me.hfTipo.Value = "I" Then
                GuardarIdiomasExtrangeros()
            Else
                GuardarOtros()
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub CmdGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdGuardar.Click
        '-----------------------------------------------------------------------------------------------
        ' Verifica que el trabajador haya aceptado la declaracion jurada para el registro de sus datos
        '-----------------------------------------------------------------------------------------------
        If Page.IsValid = True Then
            Me.hfTipo.Value = "I"
            Dim objPersonal As New Personal
            Dim dts As New Data.DataTable
            objPersonal.codigo = Request.QueryString("id")
            dts = objPersonal.VerificaDeclaracionJuradaPersonal(Request.QueryString("id"))
            If dts.Rows.Count > 0 Then
                If dts.Rows(0).Item("rpt") = 0 Then
                    Me.lblDeclarante.Text = dts.Rows(0).Item("Declarante").ToString
                    mpeInforme.Show()
                Else
                    GuardarIdiomasExtrangeros()
                End If
            End If
        End If
        '-----------------------------------------------------------------------------------------------
    End Sub

    Public Sub GuardarOtros()
        Dim Objpersonal As Personal

        Dim intArea, intAnioIni, intAnioFin, valor, intTipo, intCiclo, intsituacion As Int16

        Dim intCentro As Integer
        Dim strMesIni, strMesFin As String
        Dim strOtros As String = ""
        Dim strObservaciones, strEstudio As String

        Try
            intArea = Me.DDLArea.SelectedValue
            strEstudio = Me.TxtEstudio.Text

            intCentro = Me.DDLCentroArea.SelectedValue
            If (intCentro = 1 Or (intCentro >= 190 And intCentro <= 204)) Then
                strOtros = Me.TxtOtroCentroArea.Text.ToString
            End If

            intTipo = Me.DDLModalidad.SelectedValue
            intsituacion = Me.DDLSitArea.SelectedValue
            strObservaciones = Me.TxtObservacionArea.Text
            intCiclo = Me.DDLCursa.SelectedValue
            strMesIni = Me.DDLMesIni.SelectedValue
            strMesFin = Me.DDLMesFin.SelectedValue
            intAnioIni = Me.DDLAnioIni.SelectedValue
            intAnioFin = Me.DDLAnioFin.SelectedValue

            Objpersonal = New Personal
            Objpersonal.codigo = Request.QueryString("id")
            If Me.HddOtros.Value = "-1" Then
                valor = Objpersonal.GrabarOtros(intArea, _
                                                intCentro, _
                                                strOtros, _
                                                strEstudio, _
                                                strMesIni, _
                                                intAnioIni, _
                                                strMesFin, _
                                                intAnioFin, _
                                                intTipo, _
                                                intCiclo, _
                                                strObservaciones, _
                                                intsituacion)
            Else
                valor = Objpersonal.ModificaOtrosEstudios(intArea, _
                                                          intCentro, _
                                                          strOtros, _
                                                          strEstudio, _
                                                          strMesIni, _
                                                          intAnioIni, _
                                                          strMesFin, _
                                                          intAnioFin, _
                                                          intTipo, _
                                                          intCiclo, _
                                                          strObservaciones, _
                                                          intsituacion, _
                                                          Me.HddOtros.Value)
            End If
            If valor = -1 Then
                Dim MsgError As String
                MsgError = "<script>alert('Ocurrió un error, intentelo nuevamente.')</script>"
                Page.RegisterStartupScript("Error", MsgError)
            Else
                Me.GridView2.DataBind()
                Me.LinkVistaOtros.CssClass = "tab_seleccionado"
                Me.LinkVistaOtros.CssClass = "tab_normal"
                Me.Panel2.Visible = True
                Me.Panel4.Visible = False

                Me.LinkVistaOtros.Attributes.Remove("OnMouseOver")
                Me.LinkVistaOtros.Attributes.Remove("OnMouseOut")

                Me.LinkAgregaOtros.Attributes.Add("OnMouseOver", "tabsobre(this,1)")
                Me.LinkAgregaOtros.Attributes.Add("OnMouseOut", "tabsobre(this,2)")

                Dim mensaje As String
                mensaje = "<script>alert('Se guardaron los datos correctamente.')</script>"
                Page.RegisterStartupScript("Exito", mensaje)
            End If
        Catch ex As Exception
            Dim MsgError As String
            MsgError = "<script>alert('Ocurrió un error, intentelo nuevamente.')</script>"
            Page.RegisterStartupScript("Error", MsgError)
        End Try
        Objpersonal = Nothing
    End Sub

    Protected Sub CmdGuardarOtros_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdGuardarOtros.Click
       
        If Page.IsValid Then
            Me.hfTipo.Value = "O"
            Dim objPersonal As New Personal
            Dim dts As New Data.DataTable
            objPersonal.codigo = Request.QueryString("id")
            dts = objPersonal.VerificaDeclaracionJuradaPersonal(Request.QueryString("id"))
            If dts.Rows.Count > 0 Then
                If dts.Rows(0).Item("rpt") = 0 Then
                    Me.lblDeclarante.Text = dts.Rows(0).Item("Declarante").ToString
                    mpeInforme.Show()
                Else
                    GuardarOtros()
                End If
            End If
        End If
        '-----------------------------------------------------------------------------------------------
    End Sub

    Protected Sub DDLTipoInsArea_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DDLTipoInsArea.SelectedIndexChanged
        Dim objcombo As New Combos
        objcombo.LlenaInstitucion(Me.DDLCentroArea, Me.DDLTipoInsArea.SelectedValue, Me.DDLProcArea.SelectedValue)
        objcombo = Nothing
    End Sub

    Protected Sub DDLProcArea_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DDLProcArea.SelectedIndexChanged
        Dim objcombo As New Combos
        objcombo.LlenaInstitucion(Me.DDLCentroArea, Me.DDLTipoInsArea.SelectedValue, Me.DDLProcArea.SelectedValue)
        objcombo = Nothing
    End Sub

    Protected Sub CmdGuardar0_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdGuardar0.Click
        Response.Redirect("educacionuniversitaria.aspx?menu=" & Request.QueryString("menu") & "&id=" & Request.QueryString("id") & "&ctf=" & Request.QueryString("ctf"))
    End Sub

    Protected Sub CmdGuardar1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdGuardar1.Click
        Response.Redirect("experiencia.aspx?menu=" & Request.QueryString("menu") & "&id=" & Request.QueryString("id") & "&ctf=" & Request.QueryString("ctf"))
    End Sub

    Protected Sub btnCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        Try
            ClientScript.RegisterStartupScript(Me.GetType, "FaltanDatos", "alert('Los datos consignados no fueron registrados.');", True)
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub DDLCentroArea_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DDLCentroArea.SelectedIndexChanged
        lblCod.Text = DDLCentroArea.SelectedValue
        TxtOtroCentroArea.Enabled = True
    End Sub
End Class
