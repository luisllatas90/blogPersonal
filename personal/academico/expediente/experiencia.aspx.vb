Imports System.Data
Partial Class experiencia
    Inherits System.Web.UI.Page

    Protected Sub GridViewUnv_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles GridViewUnv.RowDeleting
        Try
            Dim objpersonal As New Personal
            objpersonal.codigo = CInt(Session("Id"))
            objpersonal.QuitarExperiencia(e.Keys.Item(0))
            objpersonal = Nothing
            e.Cancel = True
            Me.GridViewUnv.DataBind()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub GridView1_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles GridView1.RowDeleting
        Dim objpersonal As New Personal
        objpersonal.codigo = CInt(Session("Id"))
        objpersonal.QuitarExperiencia(e.Keys.Item(0))
        objpersonal = Nothing
        e.Cancel = True
        Me.GridView1.DataBind()
    End Sub

    Protected Sub GridView2_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles GridView2.RowDeleting
        Dim ObjPersonal As New Personal
        Dim tipo As Integer
        ObjPersonal.codigo = CInt(Session("id"))
        If e.Keys.Item(1) = "Academico" Then
            tipo = 1
        Else
            tipo = 2
        End If
        ObjPersonal.QuitarEventos(e.Keys.Item(0), tipo)
        ObjPersonal = Nothing
        e.Cancel = True
        Me.GridView2.DataBind()
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Request.QueryString("id") <> "" And Session("id") = "" Then
            Session("Id") = Request.QueryString("id")
        End If

        If IsPostBack = False Then

            Me.LinkAgregaExperiencia.Attributes.Add("OnMouseOver", "tabsobre(this,1)")
            Me.LinkAgregaExperiencia.Attributes.Add("OnMouseOut", "tabsobre(this,2)")

            Me.LinkAgregaEventos.Attributes.Add("OnMouseOver", "tabsobre(this,1)")
            Me.LinkAgregaEventos.Attributes.Add("OnMouseOut", "tabsobre(this,2)")

            Me.CmdGuardar.Attributes.Add("OnMouseOver", "tabsobre(this,1)")
            Me.CmdGuardar.Attributes.Add("OnMouseOut", "tabsobre(this,2)")

            Me.CmdBuscar.Attributes.Add("OnMouseOver", "tabsobre(this,1)")
            Me.CmdBuscar.Attributes.Add("OnMouseOut", "tabsobre(this,2)")

            Me.CmdDetalle.Attributes.Add("OnMouseOver", "tabsobre(this,1)")
            Me.CmdDetalle.Attributes.Add("OnMouseOut", "tabsobre(this,2)")

            Me.CmdGuardarEvento.Attributes.Add("OnMouseOver", "tabsobre(this,1)")
            Me.CmdGuardarEvento.Attributes.Add("OnMouseOut", "tabsobre(this,2)")

            Call CargaInicioExperiencia()
            Call CargaInicioEventos()

            'Add dguevara 04.10.2013
            Call CargaInicioExperienciaUnv()
        End If
    End Sub

    Protected Sub LinkAgregaExperiencia_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LinkAgregaExperiencia.Click
        Me.HddExperiencia.Value = "-1"
        Me.LinkVistaExperiencia.CssClass = "tab_normal"
        Me.LinkAgregaExperiencia.CssClass = "tab_seleccionado"

        Me.LinkAgregaExperiencia.Attributes.Remove("OnMouseOver")
        Me.LinkAgregaExperiencia.Attributes.Remove("OnMouseOut")

        Me.LinkVistaExperiencia.Attributes.Add("OnMouseOver", "tabsobre(this,1)")
        Me.LinkVistaExperiencia.Attributes.Add("OnMouseOut", "tabsobre(this,2)")

        Me.Panel3.Visible = True
        Me.Panel1.Visible = False
        Me.LblExperiencia.Text = "Registro de Experiencia Profesional"

        For Each controles As Control In Me.Panel3.Controls
            If controles.GetType.ToString = "System.Web.UI.WebControls.DropDownList" Then
                CType(controles, DropDownList).SelectedIndex = -1
            End If
        Next
        Me.TxtEmpresa.Text = ""
        Me.TxtCiudad.Text = ""
        Me.TxtFuncion.Text = ""
        Me.TxtDescripcion.Text = ""

    End Sub

    Protected Sub LinkVistaExperiencia_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LinkVistaExperiencia.Click
        Me.LinkVistaExperiencia.CssClass = "tab_seleccionado"
        Me.LinkAgregaExperiencia.CssClass = "tab_normal"
        Me.Panel1.Visible = True
        Me.Panel3.Visible = False

        Me.LinkVistaExperiencia.Attributes.Remove("OnMouseOver")
        Me.LinkVistaExperiencia.Attributes.Remove("OnMouseOut")

        Me.LinkAgregaExperiencia.Attributes.Add("OnMouseOver", "tabsobre(this,1)")
        Me.LinkAgregaExperiencia.Attributes.Add("OnMouseOut", "tabsobre(this,2)")
    End Sub

    Protected Sub LinkVistaEventos_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LinkVistaEventos.Click
        Me.LinkVistaEventos.CssClass = "tab_seleccionado"
        Me.LinkAgregaEventos.CssClass = "tab_normal"
        Me.Panel2.Visible = True
        Me.Panel4.Visible = False

        Me.LinkVistaEventos.Attributes.Remove("OnMouseOver")
        Me.LinkVistaEventos.Attributes.Remove("OnMouseOut")

        Me.LinkAgregaEventos.Attributes.Add("OnMouseOver", "tabsobre(this,1)")
        Me.LinkAgregaEventos.Attributes.Add("OnMouseOut", "tabsobre(this,2)")
    End Sub

    Protected Sub LinkAgregaEventos_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LinkAgregaEventos.Click
        Me.HddEvento.Value = "-1"
        Me.LinkVistaEventos.CssClass = "tab_normal"
        Me.LinkAgregaEventos.CssClass = "tab_seleccionado"

        Me.LinkAgregaEventos.Attributes.Remove("OnMouseOver")
        Me.LinkAgregaEventos.Attributes.Remove("OnMouseOut")

        Me.LinkVistaEventos.Attributes.Add("OnMouseOver", "tabsobre(this,1)")
        Me.LinkVistaEventos.Attributes.Add("OnMouseOut", "tabsobre(this,2)")


        Me.LblEventos.Text = "Registro de Asistencia a Eventos"
        Me.Panel4.Visible = True
        Me.Panel2.Visible = False

        For Each controles As Control In Me.Panel4.Controls
            If controles.GetType.ToString = "System.Web.UI.WebControls.DropDownList" Then
                CType(controles, DropDownList).SelectedIndex = -1
            End If
        Next
        Page.RegisterStartupScript("Inicio", "<script>otros.style.visibility='hidden'</script>")
    End Sub

    Protected Sub CargaInicioExperienciaUnv()
        Try
            Dim objCombo As New Combos
            objCombo.LlenaCargosUnv(Me.DDLCargoUnv)
            objCombo.LlenaTipoContrato(Me.DDLContratoUnv)

            Me.DDLAnioFinUnv.Items.Add("En Curso")
            Me.DDLAnioFinUnv.Items(0).Value = 0
            For i As Integer = Now.Year To 1960 Step -1
                Me.DDLAnioIniUnv.Items.Add(i)
                Me.DDLAnioFinUnv.Items.Add(i)
            Next
            Me.DDLMesFinUnv.Attributes.Add("onchange", "activarUnv();")
            Me.DDLAnioFinUnv.Attributes.Add("onchange", "activarUnv();")

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub CargaInicioExperiencia()
        Dim objCombo As New Combos
        objCombo.LlenaCargos(Me.DDLCargo)
        objCombo.LlenaTipoContrato(Me.DDLContrato)
        Me.DDLAnioFin.Items.Add("En Curso")
        Me.DDLAnioFin.Items(0).Value = 0
        For i As Integer = Now.Year To 1960 Step -1
            Me.DDLAnioIni.Items.Add(i)
            Me.DDLAnioFin.Items.Add(i)
        Next
        Me.DDLMesFin.Attributes.Add("onchange", "activar();")
        Me.DDLAnioFin.Attributes.Add("onchange", "activar();")
    End Sub


    Protected Sub GridViewUnv_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles GridViewUnv.RowEditing
        Try
            Me.LinkVistaExperienciaUnv.CssClass = "tab_normal"
            Me.LinkAgregaExperienciaUnv.CssClass = "tab_seleccionado"

            Me.LblExperienciaUnv.Text = "Modificar Experiencia Académica Universitaria"

            Me.Panel6.Visible = False
            Me.Panel5.Visible = True

            Me.LinkAgregaExperienciaUnv.Attributes.Remove("OnMouseOver")
            Me.LinkAgregaExperienciaUnv.Attributes.Remove("OnMouseOut")

            Me.LinkVistaExperienciaUnv.Attributes.Add("OnMouseOver", "tabsobre(this,1)")
            Me.LinkVistaExperienciaUnv.Attributes.Add("OnMouseOut", "tabsobre(this,2)")

            If Me.GridViewUnv.DataKeys.Item(e.NewEditIndex).Value IsNot Nothing Then
                Dim Datos As New DataTable
                Dim ObjExperiencia As New Personal
                Me.HddExperienciaUnv.Value = Me.GridViewUnv.DataKeys(e.NewEditIndex).Value

                'MO
                Datos = ObjExperiencia.ObtieneDatosExperiencia(Me.GridViewUnv.DataKeys.Item(e.NewEditIndex).Value, "MA")

                With Datos.Rows(0)
                    Me.TxtEmpresaUnv.Text = .Item("empresa")
                    Me.TxtCiudadUnv.Text = .Item("ciudad")
                    Me.DDLContratoUnv.SelectedValue = .Item("codigo_tco")
                    Me.DDLCargoUnv.SelectedValue = .Item("codigo_car")
                    Me.TxtFuncionUnv.Text = .Item("funcion_exp")
                    Me.DDLMesIniUnv.SelectedValue = .Item("mesini")
                    Me.DDLAnioIniUnv.SelectedValue = .Item("anioini")
                    Me.DDLMesFinUnv.SelectedValue = .Item("mesfin")
                    Me.DDLAnioFinUnv.SelectedValue = .Item("aniofin")
                    Me.DDLCeseUnv.SelectedValue = .Item("motivocese")
                    Me.TxtDescripcionUnv.Text = .Item("descripcion_exp")
                End With
            End If

            e.Cancel = True

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub GridView1_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles GridView1.RowEditing
        Me.LinkVistaExperiencia.CssClass = "tab_normal"
        Me.LinkAgregaExperiencia.CssClass = "tab_seleccionado"

        Me.LblExperiencia.Text = "Modificar un Idioma"

        Me.Panel1.Visible = False
        Me.Panel3.Visible = True

        Me.LinkAgregaExperiencia.Attributes.Remove("OnMouseOver")
        Me.LinkAgregaExperiencia.Attributes.Remove("OnMouseOut")

        Me.LinkVistaExperiencia.Attributes.Add("OnMouseOver", "tabsobre(this,1)")
        Me.LinkVistaExperiencia.Attributes.Add("OnMouseOut", "tabsobre(this,2)")

        If Me.GridView1.DataKeys.Item(e.NewEditIndex).Value IsNot Nothing Then
            Dim Datos As New DataTable
            Dim ObjExperiencia As New Personal
            Me.HddExperiencia.Value = Me.GridView1.DataKeys(e.NewEditIndex).Value
            Datos = ObjExperiencia.ObtieneDatosExperiencia(Me.GridView1.DataKeys.Item(e.NewEditIndex).Value, "MO")
            With Datos.Rows(0)
                Me.TxtEmpresa.Text = .Item("empresa")
                Me.TxtCiudad.Text = .Item("ciudad")
                Me.DDLContrato.SelectedValue = .Item("codigo_tco")
                Me.DDLCargo.SelectedValue = .Item("codigo_car")
                Me.TxtFuncion.Text = .Item("funcion_exp")
                Me.DDLMesIni.SelectedValue = .Item("mesini")
                Me.DDLAnioIni.SelectedValue = .Item("anioini")
                Me.DDLMesFin.SelectedValue = .Item("mesfin")
                Me.DDLAnioFin.SelectedValue = .Item("aniofin")
                Me.DDLCese.SelectedValue = .Item("motivocese")
                Me.TxtDescripcion.Text = .Item("descripcion_exp")
            End With
        End If

        e.Cancel = True

    End Sub

    Private Sub GuardarExperienciaAcademica()
        Dim strEmpresa, strCiudad, strFuncion, strDescripcion, strCese As String
        Dim FechaIni, FechaFin As Date
        Dim intContrato, intcargo, anioFin, valor As Int16
        Dim ObjPersonal As New Personal
        ObjPersonal.codigo = Request.QueryString("Id")
        Try
            strEmpresa = Me.TxtEmpresaUnv.Text
            strCiudad = Me.TxtCiudadUnv.Text
            strFuncion = Me.TxtFuncionUnv.Text
            strDescripcion = Me.TxtDescripcionUnv.Text
            strCese = Me.DDLCeseUnv.SelectedValue
            intContrato = Me.DDLContratoUnv.SelectedValue

            '**** verificar *** -----------------------------
            intcargo = Me.DDLCargoUnv.SelectedValue
            '------------------------------------------------

            FechaIni = CDate("01/" & Me.DDLMesIniUnv.SelectedValue & "/" & Me.DDLAnioIniUnv.SelectedValue)
            anioFin = Me.DDLAnioFinUnv.SelectedValue
            If anioFin = 0 Then
                FechaFin = #1/1/1900#
            Else
                FechaFin = CDate("01/" & Me.DDLMesFinUnv.SelectedValue & "/" & Me.DDLAnioFinUnv.SelectedValue)
            End If

            If Me.HddExperienciaUnv.Value = "-1" Then
                valor = ObjPersonal.GrabarExperiencia(intcargo, strFuncion, FechaIni, FechaFin, strDescripcion, intContrato, strCese, strCiudad, strEmpresa, "EA")
            Else
                valor = ObjPersonal.Modificarexperiencia(intcargo, strFuncion, FechaIni, FechaFin, strDescripcion, intContrato, strCese, strCiudad, strEmpresa, Me.HddExperienciaUnv.Value, "EA")
            End If

            If valor = -1 Then
                Dim scriptError As String
                scriptError = "<script>alert('0.Ocurrió un error, intentelo nuevamente')</script>"
                Page.RegisterStartupScript("Error", scriptError)
            Else
                Dim scriptExito As String
                Me.GridViewUnv.DataBind()

                Me.LinkVistaExperienciaUnv.CssClass = "tab_seleccionado"
                Me.LinkAgregaExperienciaUnv.CssClass = "tab_normal"

                Me.Panel5.Visible = False
                Me.Panel6.Visible = True

                Me.LinkVistaExperienciaUnv.Attributes.Remove("OnMouseOver")
                Me.LinkVistaExperienciaUnv.Attributes.Remove("OnMouseOut")

                Me.LinkAgregaExperienciaUnv.Attributes.Add("OnMouseOver", "tabsobre(this,1)")
                Me.LinkAgregaExperienciaUnv.Attributes.Add("OnMouseOut", "tabsobre(this,2)")

                scriptExito = "<script>alert('Se guardaron los datos correctamente.')</script>"
                Page.RegisterStartupScript("Error", scriptExito)
            End If
        Catch ex As Exception
            Dim scriptError As String
            scriptError = "<script>alert('1.Ocurrió un error, intentelo nuevamente')</script>"
            Page.RegisterStartupScript("Error", scriptError)
        End Try

        ObjPersonal = Nothing
    End Sub


    Private Sub GuardarExperiencia()
        Dim strEmpresa, strCiudad, strFuncion, strDescripcion, strCese As String
        Dim FechaIni, FechaFin As Date
        Dim intContrato, intcargo, anioFin, valor As Int16
        Dim ObjPersonal As New Personal
        ObjPersonal.codigo = Request.QueryString("Id")
        Try
            strEmpresa = Me.TxtEmpresa.Text
            strCiudad = Me.TxtCiudad.Text
            strFuncion = Me.TxtFuncion.Text
            strDescripcion = Me.TxtDescripcion.Text
            strCese = Me.DDLCese.SelectedValue
            intContrato = Me.DDLContrato.SelectedValue
            intcargo = Me.DDLCargo.SelectedValue
            FechaIni = CDate("01/" & Me.DDLMesIni.SelectedValue & "/" & Me.DDLAnioIni.SelectedValue)
            anioFin = Me.DDLAnioFin.SelectedValue
            If anioFin = 0 Then
                FechaFin = #1/1/1900#
            Else
                FechaFin = CDate("01/" & Me.DDLMesFin.SelectedValue & "/" & Me.DDLAnioFin.SelectedValue)
            End If
            If Me.HddExperiencia.Value = "-1" Then
                valor = ObjPersonal.GrabarExperiencia(intcargo, strFuncion, FechaIni, FechaFin, strDescripcion, intContrato, strCese, strCiudad, strEmpresa, "EL")
            Else
                valor = ObjPersonal.Modificarexperiencia(intcargo, strFuncion, FechaIni, FechaFin, strDescripcion, intContrato, strCese, strCiudad, strEmpresa, Me.HddExperiencia.Value, "EL")
            End If

            If valor = -1 Then
                Dim scriptError As String
                scriptError = "<script>alert('Ocurrió un error, intentelo nuevamente')</script>"
                Page.RegisterStartupScript("Error", scriptError)
            Else
                Dim scriptExito As String
                Me.GridView1.DataBind()

                Me.LinkVistaExperiencia.CssClass = "tab_seleccionado"
                Me.LinkAgregaExperiencia.CssClass = "tab_normal"
                Me.Panel1.Visible = True
                Me.Panel3.Visible = False

                Me.LinkVistaExperiencia.Attributes.Remove("OnMouseOver")
                Me.LinkVistaExperiencia.Attributes.Remove("OnMouseOut")

                Me.LinkAgregaExperiencia.Attributes.Add("OnMouseOver", "tabsobre(this,1)")
                Me.LinkAgregaExperiencia.Attributes.Add("OnMouseOut", "tabsobre(this,2)")

                scriptExito = "<script>alert('Se guardaron los datos correctamente.')</script>"
                Page.RegisterStartupScript("Error", scriptExito)
            End If
        Catch ex As Exception
            Dim scriptError As String
            scriptError = "<script>alert('Ocurrió un error, intentelo nuevamente')</script>"
            Page.RegisterStartupScript("Error", scriptError)
        End Try

        ObjPersonal = Nothing
    End Sub

    'Se ejecuta cuando acepta el modal
    Protected Sub btnGuardarInforme_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardarInforme.Click
        Try
            Dim objPersonal As New Personal
            Dim i As Integer = objPersonal.ActualizarEstadoDeclaracionJurada(Request.QueryString("id"))

            'If hfTipo.Value = "E" Then
            '    GuardarExperiencia()
            'Else
            '    GuardarEvento()
            'End If

            Select Case hfTipo.Value
                Case "E"
                    GuardarExperiencia()
                Case "A"
                    GuardarExperienciaAcademica()
                Case Else
                    GuardarEvento()
            End Select
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub CmdGuardarUnv_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdGuardarUnv.Click
        Try
            hfTipo.Value = "A"
            Dim objPersonal As New Personal
            Dim dts As New Data.DataTable
            objPersonal.codigo = Request.QueryString("id")
            dts = objPersonal.VerificaDeclaracionJuradaPersonal(Request.QueryString("id"))
            If dts.Rows.Count > 0 Then
                If dts.Rows(0).Item("rpt") = 0 Then
                    Me.lblDeclarante.Text = dts.Rows(0).Item("Declarante").ToString
                    mpeInforme.Show()
                Else
                    GuardarExperienciaAcademica()
                End If
            End If
            'GuardarExperienciaAcademica()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub


    Protected Sub CmdGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdGuardar.Click
        '-----------------------------------------------------------------------------------------------
        ' Verifica que el trabajador haya aceptado la declaracion jurada para el registro de sus datos
        '-----------------------------------------------------------------------------------------------
        hfTipo.Value = "E"
        Dim objPersonal As New Personal
        Dim dts As New Data.DataTable
        objPersonal.codigo = Request.QueryString("id")
        dts = objPersonal.VerificaDeclaracionJuradaPersonal(Request.QueryString("id"))
        If dts.Rows.Count > 0 Then
            If dts.Rows(0).Item("rpt") = 0 Then
                Me.lblDeclarante.Text = dts.Rows(0).Item("Declarante").ToString
                mpeInforme.Show()
            Else
                GuardarExperiencia()
            End If
        End If
        '--------------------------------
    End Sub

    Protected Sub CmdBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdBuscar.Click
        Dim ObjCombos As New Combos
        Dim tipo As Integer
        If Me.RbAcademico.Checked = True Then
            tipo = 1
        Else
            tipo = 2
        End If
        ObjCombos.LlenaEventos(Me.LstEventos, Me.TxtBuscar.Text, Me.DDLTIpo.SelectedValue, Me.DDLClaseEven.SelectedValue, tipo)
        ObjCombos = Nothing
    End Sub

    Protected Sub RbAcademico_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles RbAcademico.CheckedChanged
        Dim objcombo As New Combos
        objcombo.LlenaTipoEvento(Me.DDLTIpo, 1)
        objcombo.LlenaEventos(Me.LstEventos, "", "", "", 1)
        objcombo = Nothing
        Me.DDLClaseEven.Visible = True
        '** add dguevara 04.10.2013
        DDLProcedenciaEve.Visible = True
        lblProcedencia.Visible = True
    End Sub

    Protected Sub RbSocial_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles RbSocial.CheckedChanged
        Dim objcombo As New Combos
        objcombo.LlenaTipoEvento(Me.DDLTIpo, 2)
        objcombo.LlenaEventos(Me.LstEventos, "", "", "", 2)
        objcombo = Nothing
        Me.DDLClaseEven.Visible = False

        '**add dguevara 04.10.2013
        DDLProcedenciaEve.Visible = False
        lblProcedencia.Visible = False
    End Sub

    Protected Sub CargaInicioEventos()
        Me.TxtHoras.Attributes.Add("OnKeyPress", "validarnumero()")
        Me.LstEventos.Attributes.Add("onchange", "vernuevo()")
        Me.Img.Attributes.Add("onmouseover", "ddrivetip('Seleccione tipo de evento e ingrese un texto de busqueda para mostrar la lista de eventos registrados.')")
        Me.Img.Attributes.Add("onMouseout", "hideddrivetip()")
        Dim ObjCombo As New Combos
        ObjCombo.LlenaClaseEvento(Me.DDLClaseEven)
        ObjCombo.LlenaTipoEvento(Me.DDLTIpo, 1)
        ObjCombo.LlenaTipoParticipacion(Me.ChkParticipa)

        Me.DDLFinAño.Items.Add("Año")
        Me.DDLIniAño.Items.Add("Año")
        Me.DDLIniAño.Items(0).Value = 0
        Me.DDLFinAño.Items(0).Value = 0
        For i As Int16 = Year(Now) To 1940 Step -1
            Me.DDLIniAño.Items.Add(i)
            Me.DDLFinAño.Items.Add(i)
        Next
        Me.HddLista.Value = Me.ChkParticipa.Items.Count

        Me.LstEventos.Items.Clear()
        Me.LstEventos.Items.Add("<---- Otros ---->")
        Me.LstEventos.Items(0).Value = 0
        Me.DDLTIpo.SelectedValue = 7
        ObjCombo.LlenaEventos(Me.LstEventos, "a", 7, 2, 1)

    End Sub

    Private Sub GuardarEvento()
        Dim ObjPersonal As New Personal
        Dim cadena As String = Nothing
        Dim tipoInserta As String
        Dim tipoEven, claseEven, evento, strNombre, strorganizado, strProcencia As String
        Dim Fini, FFin As Date
        Dim intDuracion, tipoDuracion, valor As Integer
        strNombre = ""
        strorganizado = ""
        intDuracion = 0
        Try
            For i As Integer = 0 To Me.ChkParticipa.Items.Count - 1
                If Me.ChkParticipa.Items(i).Selected = True Then
                    cadena = cadena & Me.ChkParticipa.Items(i).Value & ","
                End If
            Next
            If Me.RbAcademico.Checked = True Then
                tipoInserta = "AC"  'Academico
            Else
                tipoInserta = "SO"  'Social
            End If
            tipoEven = Me.DDLTIpo.SelectedValue
            claseEven = Me.DDLClaseEven.SelectedValue

            '**Procencia: Nacional - Extranjera ------------------------------------------
            strProcencia = CType(Me.DDLProcedenciaEve.SelectedValue, String)   'Add dguevara 04.10.2013 
            '-----------------------------------------------------------------------------

            evento = Me.LstEventos.SelectedValue
            If evento = 0 Then
                strNombre = Me.TxtOtro.Text
                strorganizado = Me.TxtOrganizado.Text
                Fini = CType(Me.DDLIniDia.SelectedValue & "/" & Me.DDLIniMes.SelectedValue & "/" & Me.DDLIniAño.SelectedValue, Date)
                FFin = CType(Me.DDLFinDIa.SelectedValue & "/" & Me.DDLFinMes.SelectedValue & "/" & Me.DDLFinAño.SelectedValue, Date)
                intDuracion = CInt(Me.TxtHoras.Text)
                tipoDuracion = Me.DDLDuracion.SelectedValue
            Else
                Fini = #1/1/1900#
                FFin = #1/1/1900#
            End If

            '-----------------------------------------------------------------
            'Para indetificar dond ta saliendo el error de mela.
            'Valores:
            'Response.Write("tipoInserta: " & tipoInserta)
            'Response.Write("<br>")
            'Response.Write("strNombre: " & strNombre)
            'Response.Write("<br>")
            'Response.Write("Fini: " & Fini)
            'Response.Write("<br>")
            'Response.Write("FFin: " & FFin)
            'Response.Write("<br>")
            'Response.Write("claseEven: " & claseEven)
            'Response.Write("<br>")
            'Response.Write("tipoEven: " & tipoEven)
            'Response.Write("<br>")
            'Response.Write("strorganizado: " & strorganizado)
            'Response.Write("<br>")
            'Response.Write("intDuracion: " & intDuracion)
            'Response.Write("<br>")
            'Response.Write("tipoDuracion: " & tipoDuracion)
            'Response.Write("<br>")
            'Response.Write("cadena: " & cadena)
            'Response.Write("<br>")
            'Response.Write("evento: " & evento)
            'Response.Write("<br>")
            'Response.Write("strProcencia: " & strProcencia)
            '-----------------------------------------------------------------


            ObjPersonal.codigo = Request.QueryString("Id")
            valor = ObjPersonal.GrabarEventos(tipoInserta, _
                                              strNombre, _
                                              Fini, _
                                              FFin, _
                                              claseEven, _
                                              tipoEven, _
                                              strorganizado, _
                                              intDuracion, _
                                              tipoDuracion, _
                                              cadena, _
                                              evento, strProcencia)

            If valor = -1 Then
                Dim scriptMsg As String
                scriptMsg = "<script>alert('1.Ocurio un error al insertar los datos, intentelo nuevamente')</script>"
                Page.RegisterStartupScript("Error", scriptMsg)
            Else
                ObjPersonal = Nothing

                Me.GridView2.DataBind()
                Me.LinkVistaEventos.CssClass = "tab_seleccionado"
                Me.LinkVistaEventos.CssClass = "tab_normal"
                Me.Panel2.Visible = True
                Me.Panel4.Visible = False

                Me.LinkVistaEventos.Attributes.Remove("OnMouseOver")
                Me.LinkVistaEventos.Attributes.Remove("OnMouseOut")

                Me.LinkAgregaEventos.Attributes.Add("OnMouseOver", "tabsobre(this,1)")
                Me.LinkAgregaEventos.Attributes.Add("OnMouseOut", "tabsobre(this,2)")

                Dim scriptExito As String
                scriptExito = "<script>alert('Se guardaron los datos correctamente.')</script>"
                Page.RegisterStartupScript("Error", scriptExito)
            End If

        Catch ex As Exception
            ObjPersonal = Nothing
            Dim scriptMsg As String
            scriptMsg = "<script>alert('2.Ocurio un error al insertar los datos, intentelo nuevamente')</script>"
            Page.RegisterStartupScript("Error", scriptMsg)
        End Try

    End Sub

    Protected Sub CmdGuardarEvento_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdGuardarEvento.Click
        '-----------------------------------------------------------------------------------------------
        ' Verifica que el trabajador haya aceptado la declaracion jurada para el registro de sus datos
        '-----------------------------------------------------------------------------------------------
        hfTipo.Value = "V"
        Dim objPersonal As New Personal
        Dim dts As New Data.DataTable
        objPersonal.codigo = Request.QueryString("id")
        dts = objPersonal.VerificaDeclaracionJuradaPersonal(Request.QueryString("id"))
        If dts.Rows.Count > 0 Then
            If dts.Rows(0).Item("rpt") = 0 Then
                Me.lblDeclarante.Text = dts.Rows(0).Item("Declarante").ToString
                mpeInforme.Show()
            Else
                GuardarEvento()
            End If
        End If
        '------------------
    End Sub

    Protected Sub CmdGuardar0_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdGuardar0.Click
        Response.Redirect("idiomas.aspx?menu=" & Request.QueryString("menu") & "&id=" & Request.QueryString("id") & "&ctf=" & Request.QueryString("ctf"))
    End Sub

    Protected Sub CmdGuardar1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdGuardar1.Click
        Response.Redirect("distinciones.aspx?menu=" & Request.QueryString("menu") & "&id=" & Request.QueryString("id") & "&ctf=" & Request.QueryString("ctf"))
    End Sub

    Protected Sub GridView1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridView1.SelectedIndexChanged

    End Sub

    Protected Sub btnCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        Try
            ClientScript.RegisterStartupScript(Me.GetType, "FaltanDatos", "alert('Los datos consignados no fueron registrados.');", True)
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub


    Protected Sub LinkVistaExperienciaUnv_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LinkVistaExperienciaUnv.Click
        Try
            Panel5.Visible = False
            Panel6.Visible = True
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub LinkAgregaExperienciaUnv_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LinkAgregaExperienciaUnv.Click
        Try
            Panel5.Visible = True
            Panel6.Visible = False

            'Me.Panel3.Visible = True
            'Me.Panel1.Visible = False


            Me.HddExperienciaUnv.Value = "-1"
            Me.LinkVistaExperienciaUnv.CssClass = "tab_normal"
            Me.LinkAgregaExperienciaUnv.CssClass = "tab_seleccionado"

            Me.LinkAgregaExperienciaUnv.Attributes.Remove("OnMouseOver")
            Me.LinkAgregaExperienciaUnv.Attributes.Remove("OnMouseOut")

            Me.LinkVistaExperienciaUnv.Attributes.Add("OnMouseOver", "tabsobre(this,1)")
            Me.LinkVistaExperienciaUnv.Attributes.Add("OnMouseOut", "tabsobre(this,2)")

            Me.LblExperienciaUnv.Text = "Registro de Experiencia Académica Universitaria"

            For Each controles As Control In Me.Panel5.Controls
                If controles.GetType.ToString = "System.Web.UI.WebControls.DropDownList" Then
                    CType(controles, DropDownList).SelectedIndex = -1
                End If
            Next
            Me.TxtEmpresaUnv.Text = ""
            Me.TxtCiudadUnv.Text = ""
            Me.TxtFuncionUnv.Text = ""
            Me.TxtDescripcionUnv.Text = ""


        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub



   
   
  
   
End Class
