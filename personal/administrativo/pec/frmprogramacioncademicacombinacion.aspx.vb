Imports System.Drawing

Partial Class administrativo_pec_frmprogramacioncademicacombinacion
    Inherits System.Web.UI.Page
    Public Shared ID_COMB As Integer = 0
    Public Shared ID_COMBDET As Integer = 0
    Dim LastCategory As String = String.Empty
    Dim CurrentRow As Integer = -1
    Public Shared dtDetalle As New Data.DataTable

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("id_per") = "" Then
            Response.Redirect("../../../sinacceso.html")
        End If
        If Not Page.IsPostBack Then
            'Response.Write(Session("id_per").ToString())
            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            Dim objFun As New ClsFunciones

            objFun.CargarListas(Me.ddlEscuela, obj.TraerDataTable("ConsultarEscuelaPorPersonal", "2", CInt(Session("id_per").ToString()), 2), "codigo_Cpf", "nombre_Cpf")
            objFun.CargarListas(Me.ddlCiclo, obj.TraerDataTable("ConsultarCicloAcademico", "TO", ""), "codigo_cac", "descripcion_cac")
            objFun.CargarListas(Me.ddlEscuelaReg, obj.TraerDataTable("ConsultarEscuelaPorPersonal", "2", CInt(Session("id_per").ToString()), 2), "codigo_Cpf", "nombre_Cpf")
            objFun.CargarListas(Me.ddlCicloReg, obj.TraerDataTable("ConsultarCicloAcademico", "TO", ""), "codigo_cac", "descripcion_cac")
            Me.ddlCiclo.SelectedValue = Session("codigo_cac")
            obj.CerrarConexion()
            obj = Nothing
            chkActivo.Visible = True

            Session("ID_COMB") = 0
            Session("ID_COMBDET") = 0
            Session("dtDetalle") = 0
            VisibleDiv(True)
            VisibleDetDiv(False)
            consultarCombinacion()
            'VisibleDivGlobal(True)
        End If
    End Sub

    'Private Sub VisibleDivGlobal(ByVal sw As Boolean)
    '    VisibleDiv(sw)
    '    VisibleDetDiv(Not sw)
    'End Sub

    Private Sub VisibleDiv(ByVal sw As Boolean)
        Me.divBuscarCombinacion.Visible = sw
        Me.divListarCombinacion.Visible = sw
        Me.divRegistrarCombinacion.Visible = Not sw
    End Sub
    Private Sub VisibleDetDiv(ByVal sw As Boolean)
        Me.divBuscarCombinacion.Visible = Not sw
        Me.divListarCombinacion.Visible = Not sw

        Me.divListarCombinacionDet.Visible = sw
        'Me.divRegistrarCombinacion.Visible = Not sw
    End Sub
    Protected Sub btnConsultar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnConsultar.Click
        consultarCombinacion()
    End Sub
    Private Sub consultarCombinacion()
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        dt = obj.TraerDataTable("Escuela_combListar", "1", 0, Me.ddlEscuela.SelectedValue, Me.ddlCiclo.SelectedValue)
        Me.gDataComb.DataSource = dt
        Me.gDataComb.DataBind()
        obj.CerrarConexion()
        obj = Nothing
    End Sub
    Private Sub LimpiarCombinacion()
        ID_COMB = 0
        Session("ID_COMB") = 0
        Me.ddlEscuelaReg.SelectedIndex = 0
        Me.txtnrocomb.Text = ""
    End Sub
    Private Sub LimpiarDetalleCombinacion()

        ID_COMBDET = 0

        Me.txtdetnumero.Text = ""
        Me.btnDetCerrar.Visible = False
        Me.divInfoEdit.Visible = False
        Me.txtinfoeditar.Text = ""
        'Session("ID_COMB") = 0
        Session("ID_COMBDET") = 0
    End Sub
    Private Function fnValidarRegistroComb() As Boolean
        Try
            Dim script As String = ""
            If ddlEscuelaReg.SelectedValue = "0" Then
                script = "fnMensaje('warning','Seleccione Escuela Profesional')"
                ScriptManager.RegisterStartupScript(Me, GetType(Page), "alert", script, True)

                Return False
            End If
            If txtnrocomb.Text = "0" Or txtnrocomb.Text = "" Then
                script = "fnMensaje('warning','Ingrese numero de combinación')"
                ScriptManager.RegisterStartupScript(Me, GetType(Page), "alert", script, True)

                Return False
            End If

            If Session("ID_COMB") = 0 Then

                Dim dt As New Data.DataTable
                Dim obj As New ClsConectarDatos
                obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
                obj.AbrirConexion()
                dt = obj.TraerDataTable("Escuela_combListar", "2", 0, Me.ddlEscuelaReg.SelectedValue, Me.ddlCicloReg.SelectedValue)
                obj.CerrarConexion()
                obj = Nothing

                If dt.Rows.Count > 0 Then
                    script = "fnMensaje('warning','Ya se encuentra registrado este grupo')"
                    ScriptManager.RegisterStartupScript(Me, GetType(Page), "alert", script, True)

                    Return False
                End If
            End If
            Return True
        Catch ex As Exception
            Dim script As String = "fnMensaje('error','" & ex.Message & "')"
            ScriptManager.RegisterStartupScript(Me, GetType(Page), "alert", script, True)
        End Try

    End Function

    Private Sub RegistrarCombinacion()
        Try

            If fnValidarRegistroComb() Then

                Dim script As String = ""

                Dim obj As New ClsConectarDatos
                obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
                obj.AbrirConexion()
                'dt = obj.TraerDataTable("Escuela_combListar", "1", Me.ddlEscuela.SelectedValue, Me.ddlCiclo.SelectedValue)
                Dim ope As String = ""


                If Session("ID_COMB") > 0 Then
                    ope = "A"
                    script = "fnMensaje('success','Se modificó con éxito')"
                Else
                    ope = "I"
                    script = "fnMensaje('success','Se registró con éxito')"
                End If

                obj.Ejecutar("Escuela_combReg", ope, Session("ID_COMB"), Me.ddlEscuelaReg.SelectedValue, Me.ddlCicloReg.SelectedValue, CInt(Me.txtnrocomb.Text), Session("perlogin"), Me.chkActivo.Checked)
                obj.CerrarConexion()
                obj = Nothing

                ' ScriptManager.RegisterStartupScript(Me, GetType(Page), "alert", script, True)
                VisibleDiv(True)
                fnNotificacion(script)

                LimpiarCombinacion()
                consultarCombinacion()
            End If
        Catch ex As Exception

            Dim script As String = "fnMensaje('error','" & ex.Message & "')"
            ScriptManager.RegisterStartupScript(Me, GetType(Page), "alert", script, True)
        End Try
    End Sub

    Private Sub fnNotificacion(ByVal script As String)

        ScriptManager.RegisterStartupScript(Me, GetType(Page), "alert", script, True)
    End Sub

    Protected Sub btnAgregar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAgregar.Click
        VisibleDiv(False)
        ID_COMB = 0
        LimpiarCombinacion()
    End Sub

    Protected Sub btnCombCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCombCancelar.Click
        VisibleDiv(True)
    End Sub

    Protected Sub btnCombGrabar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCombGrabar.Click
        RegistrarCombinacion()

    End Sub

    Protected Sub gDataComb_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles gDataComb.PreRender
        If gDataComb.Rows.Count > 0 Then
            gDataComb.UseAccessibleHeader = True
            gDataComb.HeaderRow.TableSection = TableRowSection.TableHeader
        End If

    End Sub

    Protected Sub gDataComb_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gDataComb.RowCommand
        Try
            Dim index As Integer = Convert.ToInt32(e.CommandArgument)
            If (e.CommandName = "Editar") Then

                ID_COMB = CInt(gDataComb.DataKeys(index).Values("id").ToString())
                Session("ID_COMB") = CInt(gDataComb.DataKeys(index).Values("id").ToString())
                Dim obj As New ClsConectarDatos
                Dim dt As New Data.DataTable
                obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
                obj.AbrirConexion()
                dt = obj.TraerDataTable("Escuela_combListar", "1", ID_COMB, 0, 0)
                obj.CerrarConexion()
                obj = Nothing
                VisibleDiv(False)

                If dt.Rows.Count > 0 Then
                    Me.ddlEscuelaReg.SelectedValue = dt.Rows(0).Item("codigo_cpf")
                    Me.ddlCicloReg.SelectedValue = dt.Rows(0).Item("codigo_cac")
                    Me.txtnrocomb.Text = dt.Rows(0).Item("nrocombinacion")
                End If
            ElseIf (e.CommandName = "CombDet") Then
                ID_COMB = CInt(gDataComb.DataKeys(index).Values("id").ToString())
                Session("ID_COMB") = CInt(gDataComb.DataKeys(index).Values("id").ToString())

                Dim codigo_cac As Integer = CInt(gDataComb.DataKeys(index).Values("codigo_cac").ToString())

                ' response.write(ID_COMB & "<br>")
                ' response.write(codigo_cac & "<br>")
                'response.write(CInt(gDataComb.DataKeys(index).Values("codigo_cpf").ToString()) & "<br>")


                Dim NRO_COMB As Integer = 0
                NRO_COMB = CInt(gDataComb.DataKeys(index).Values("nrocombinacion").ToString())
                'nrocombinacion
                VisibleDetDiv(True)
                Me.lblEscuela.Value = gDataComb.DataKeys(index).Values("nombre_Cpf").ToString()
                Me.lblCiclo.Value = gDataComb.DataKeys(index).Values("cicloacademico").ToString()

                Dim obj As New ClsConectarDatos
                obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
                obj.AbrirConexion()
                Dim objFun As New ClsFunciones

                objFun.CargarListas(Me.ddlPlanEstudio, obj.TraerDataTable("ConsultarPlanEstudio", "CT", CInt(gDataComb.DataKeys(index).Values("codigo_cpf").ToString()), 2), "codigo_Pes", "descripcion_Pes")
                objFun.CargarListas(Me.ddlCurso, obj.TraerDataTable("Escuela_combCursoProgListar", "1", 0, ddlPlanEstudio.SelectedValue, codigo_cac), "codigo_Cup", "descripcion_cup")

                obj.CerrarConexion()
                obj = Nothing

                LlenarComboCombinacion(NRO_COMB)

                'For i As Integer = 1 To NRO_COMB
                '    ddlCombinacion.Items.Add(

                'Next
                Me.btnDetCerrar.Visible = False
                Me.divInfoEdit.Visible = False
                Me.txtinfoeditar.Text = ""
                consultarDetCombinacion()

            End If
        Catch ex As Exception
            Dim script As String = "fnMensaje('error','" & ex.Message & "')"

            'Dim script As String = "fnMensaje('error','" & ex.Message & "  " & ex.StackTrace & "')"
            ScriptManager.RegisterStartupScript(Me, GetType(Page), "alert", script, True)
        End Try
    End Sub

    Private Sub LlenarComboCombinacion(ByVal N As Integer)

        Dim dt As Data.DataTable
        dt = New Data.DataTable("Tabla")

        dt.Columns.Add("Codigo")
        dt.Columns.Add("Descripcion")


        For i As Integer = 1 To N
            Dim dr As Data.DataRow
            dr = dt.NewRow()
            dr("Codigo") = i
            dr("Descripcion") = i
            dt.Rows.Add(dr)
        Next
        'Dim dr As Data.DataRow

        'dr = dt.NewRow()
        'dr("Codigo") = "M"
        'dr(1) = "Masculino"
        'dt.Rows.Add(dr)

        'dr = dt.NewRow()
        'dr(0) = "F"
        'dr(1) = "Femenino"
        'dt.Rows.Add(dr)

        Me.ddlCombinacion.DataSource = dt
        Me.ddlCombinacion.DataValueField = "Codigo"
        Me.ddlCombinacion.DataTextField = "Descripcion"
        Me.ddlCombinacion.DataBind()
    End Sub

    Protected Sub gDataComb_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gDataComb.RowDataBound
        Try
            If e.Row.RowIndex >= 0 Then
                If e.Row.Cells(3).Text = "Inactivo" Then
                    e.Row.ForeColor = Drawing.Color.Red

                End If
            End If

        Catch ex As Exception
            Dim script As String = "fnMensaje('error','" & ex.Message & "')"
            ScriptManager.RegisterStartupScript(Me, GetType(Page), "alert", script, True)
        End Try

    End Sub

    Protected Sub btnDetRegresar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDetRegresar.Click
        VisibleDetDiv(False)
        LimpiarDetalleCombinacion()
    End Sub

    Protected Sub btnDetConsultar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDetConsultar.Click
        consultarDetCombinacion()
    End Sub

    Private Sub consultarDetCombinacion()


        If Session("ID_COMB") = 0 Then
            Dim script As String = ""
            script = "fnMensaje('warning','seleccione nuevamente Escuela profesional')"
            ' ScriptManager.RegisterStartupScript(Me, GetType(Page), "alert", script, True)
            fnNotificacion(script)
        Else
            Dim obj As New ClsConectarDatos
            Dim dt As New Data.DataTable
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            dtDetalle = obj.TraerDataTable("Escuela_combDetListar", "1", 0, Session("ID_COMB"), 0, 0)
            Session("dtDetalle") = dtDetalle
            Me.gDataCombDet.DataSource = dtDetalle
            Me.gDataCombDet.DataBind()
            obj.CerrarConexion()
            obj = Nothing
            ValidaGridDetalle()
            ' gDataCombDet.Columns(7).Visible = True
            CalcularVacantesDisponibles()
        End If
    End Sub

    Sub CalcularVacantesDisponibles()

        For i As Integer = 0 To gDataCombDet.Rows.Count - 1
            If gDataCombDet.Rows(i).Cells(9).Text = "0" Then
                gDataCombDet.Rows(i).Cells(10).Text = ""
                gDataCombDet.Rows(i).Cells(11).Text = ""


            End If
            gDataCombDet.Rows(i).Cells(6).Text = fnCalcularVacantesDisponibles(i)
            gDataCombDet.Columns(7).Visible = False


        Next

    End Sub

    Private Function fnCalcularVacantesDisponibles(ByVal RowIndex As Integer) As Integer
        Dim codigo_cupL As Label = CType(gDataCombDet.Rows(RowIndex).FindControl("lblcup"), Label)
        Dim script As String = ""
        Dim str As String = ""
        Dim dt As Data.DataTable = CType(Session("dtDetalle"), Data.DataTable)
        Dim result() As Data.DataRow = dt.Select("codigo_cup = " & codigo_cupL.Text)
        Dim ingresantes As Integer = 0


        For Each row As Data.DataRow In result
            'Console.WriteLine("{0}, {1}", row(0), row(1))
            ingresantes = ingresantes + CInt(row(6))
        Next

        Return ingresantes
        'script = "fnMensaje('warning','" & ingresantes.ToString & "')"
        'fnNotificacion(script)
    End Function
    Private Function fnCalcularVacantesDisponiblesAdd(ByVal tipo As String, ByVal codigo_cup As Integer, Optional ByVal nrocomb As Integer = 0) As Integer

        Dim script As String = ""
        Dim str As String = ""
        Dim dt As Data.DataTable = CType(Session("dtDetalle"), Data.DataTable)
        Dim result() As Data.DataRow = dt.Select("codigo_cup = " & codigo_cup.ToString)

        Dim ingresantes As Integer = 0

        ' Response.Write("<br>" & dt.rows.count)
        If tipo = "I" Then
            For i As Integer = 0 To dt.Rows.Count - 1
                If dt.Rows(i).Item(2) = codigo_cup Then
                    'Response.Write("<br>" & i.ToString & dt.rows(i).item(2).tostring())
                    ' Response.Write("<br>" & i.ToString & dt.rows(i).item(4).tostring())
                    ingresantes = ingresantes + CInt(dt.Rows(i).Item(6))
                End If

            Next

        ElseIf tipo = "I_EDIT" Then
            For Each row As Data.DataRow In result
                'Console.WriteLine("{0}, {1}", row(0), row(1))
                'Response.Write("<br>" & row(6))
                ingresantes = ingresantes + CInt(row(6))
                'Response.Write("<br>" & row(0).tostring())
                'Response.Write("<br>" & row(6).tostring())
            Next
            ' Response.Write("ESTA ES DT: " & ingresantes.ToString)
        ElseIf tipo = "I_ACTUAL" Then
            For i As Integer = 0 To dt.Rows.Count - 1
                If dt.Rows(i).Item(2) = codigo_cup And dt.Rows(i).Item(3) = nrocomb Then
                    'Response.Write("<br>" & i.ToString & dt.rows(i).item(2).tostring())
                    ' Response.Write("<br>" & i.ToString & dt.rows(i).item(4).tostring())
                    ingresantes = CInt(dt.Rows(i).Item(6))
                End If

            Next

            ' Response.Write("ESTA ES DT: " & ingresantes.ToString)
        Else
            For Each row As Data.DataRow In result
                'Console.WriteLine("{0}, {1}", row(0), row(1))
                'Response.Write(row(6))
                If tipo = "I" Then
                    ingresantes = ingresantes + CInt(row(6))
                    'Response.Write("<br>" & row(0).tostring())
                    ' Response.Write("<br>" & row(6).tostring())
                ElseIf tipo = "V" Then
                    ingresantes = CInt(row(11))
                ElseIf tipo = "F" Then
                    ingresantes = CInt(row(7))
                    ' Response.Write("<br>" & row(7).ToString())
                End If

            Next
        End If






        Return ingresantes
        'script = "fnMensaje('warning','" & ingresantes.ToString & "')"
        'fnNotificacion(script)
    End Function
    Protected Sub gDataCombDet_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles gDataCombDet.PreRender
        If gDataCombDet.Rows.Count > 0 Then
            gDataCombDet.UseAccessibleHeader = True
            gDataCombDet.HeaderRow.TableSection = TableRowSection.TableHeader
        End If
    End Sub

    Protected Sub btnDetAgregar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDetAgregar.Click
        RegistrarDetalleCombinacion()
        ValidaGridDetalle()
    End Sub

    Private Sub RegistrarDetalleCombinacion()
        Try
            'Response.Write("ID_COMB: " & Session("ID_COMB") & "<br>")
            'Response.Write("ID_COMBDET: " & Session("ID_COMBDET") & "<br>")

            If fnValidarRegistroDetComb() Then

                ' Dim sw As Boolean = True
                Dim script As String = ""

                Dim obj As New ClsConectarDatos
                obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
                obj.AbrirConexion()
                'dt = obj.TraerDataTable("Escuela_combListar", "1", Me.ddlEscuela.SelectedValue, Me.ddlCiclo.SelectedValue)
                Dim ope As String = ""

                If Session("ID_COMBDET") > 0 Then
                    ope = "A"
                    script = "fnMensaje('success','Combinación modificada con éxito')"
                Else
                    ope = "I"
                    script = "fnMensaje('success','Combinación registrada con éxito')"
                End If


                obj.Ejecutar("Escuela_detcombReg", ope, Session("ID_COMBDET"), Session("ID_COMB"), Me.ddlCurso.SelectedValue, Me.ddlCombinacion.SelectedValue, CInt(Me.txtdetnumero.Text), CInt(Me.txtdetnumero.Text), Session("perlogin"), 1)


                obj.CerrarConexion()
                obj = Nothing

                If Session("ID_COMBDET") > 0 Then
                    Me.btnDetCerrar.Visible = False
                    Me.divInfoEdit.Visible = False
                    Me.txtinfoeditar.Text = ""
                End If

                ' ScriptManager.RegisterStartupScript(Me, GetType(Page), "alert", script, True)

                'VisibleDiv(True)
                consultarDetCombinacion()
                LimpiarDetalleCombinacion()

                fnNotificacion(script)

            End If
        Catch ex As Exception
            Response.Write(ex.Message)
            Response.Write(ex.StackTrace)
            Dim script As String = "fnMensaje('error','" & ex.StackTrace & "')"
            ScriptManager.RegisterStartupScript(Me, GetType(Page), "alert", script, True)
        End Try
    End Sub


    Private Function fnValidarRegistroDetComb() As Boolean
        Try
            Dim script As String = ""

            If txtdetnumero.Text = "0" Or txtdetnumero.Text = "" Then

                script = "fnMensaje('warning','Ingrese numero de ingresantes para combinación')"
                ' ScriptManager.RegisterStartupScript(Me, GetType(Page), "alert", script, True)
                fnNotificacion(script)

                Return False
            End If
            Dim obj As New ClsConectarDatos
            Dim dt As New Data.DataTable
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString

            If Session("ID_COMBDET") = 0 Then
                obj.AbrirConexion()
                dt = obj.TraerDataTable("Escuela_combDetListar", "2", 0, Session("ID_COMB"), CInt(ddlCurso.SelectedValue), CInt(ddlCombinacion.SelectedValue))
                obj.CerrarConexion()
                If dt.Rows.Count > 0 Then
                    script = "fnMensaje('warning','Ya se encuentra registrado el curso [" & dt.Rows(0).Item("nombre_Cur").ToString() & "] en la combinacion [" & dt.Rows(0).Item("nrocombinacion").ToString() & "] ')"

                    fnNotificacion(script)
                    obj = Nothing
                    Return False
                End If
            End If



            'If Session("ID_COMBDET") > 0 Then
            '    Dim rowFaltan As Integer = fnCalcularVacantesDisponiblesAdd("F", Me.ddlCurso.SelectedValue)
            '    'Response.Write("rowFaltan: " & rowFaltan & "<br>")
            '    If CInt(Me.txtdetnumero.Text) < rowFaltan Then
            '        script = "fnMensaje('warning','El Numero de " & Me.txtdetnumero.Text.ToString & " ingresantes supera a los " & rowFaltan.ToString() & " ingresantes ya matriculados en la combinacion ')"
            '        fnNotificacion(script)

            '        Return False
            '    End If
            'End If


            obj.AbrirConexion()
            dt = obj.TraerDataTable("Escuela_combCursoProgListar", "2", CInt(ddlCurso.SelectedValue), 0, 0)
            obj.CerrarConexion()

            Dim NRO_VACANTES_DISP As Integer = 0

            If dt.Rows.Count > 0 AndAlso (CInt(Me.txtdetnumero.Text) > CInt(dt.Rows(0).Item("vacantes"))) Then
                script = "fnMensaje('warning','El Numero de ingresantes supera las " & dt.Rows(0).Item("vacantes").ToString() & " vacantes ')"
                fnNotificacion(script)
                obj = Nothing
                Return False
            End If

            NRO_VACANTES_DISP = dt.Rows(0).Item("vacantes")

            If Session("ID_COMB") = 0 Then
                script = "fnMensaje('warning','Vuelva a seleccionar escuela profesional, haga click en [<- Regresar]')"
                'ScriptManager.RegisterStartupScript(Me, GetType(Page), "alert", script, True)
                fnNotificacion(script)
                Return False
            End If


            If Session("ID_COMBDET") = 0 Then
                Dim total_ing As Integer = fnCalcularVacantesDisponiblesAdd("I", Me.ddlCurso.SelectedValue)
                Dim txting As Integer = CInt(Me.txtdetnumero.Text)
                Dim vacantes_disp As Integer = NRO_VACANTES_DISP - total_ing - txting

                'Response.Write("<br>1 NRO_VACANTES_DISP: " & NRO_VACANTES_DISP & "<br>")
                'Response.Write("total_ing VANCATNES ASIG: " & total_ing & "<br>")
                'Response.Write("txting: " & txting & "<br>")
                'Response.Write("vacantes_disp: " & vacantes_disp & "<br>")

                If vacantes_disp < 0 Then
                    Dim difvacante As String = NRO_VACANTES_DISP - total_ing
                    script = "fnMensaje('warning','El Numero de ingresantes supera las " & NRO_VACANTES_DISP.ToString() & "  disponibles')"
                    fnNotificacion(script)
                    Return False
                End If

            Else
                Dim total_ing As Integer = fnCalcularVacantesDisponiblesAdd("I_ACTUAL", Me.ddlCurso.SelectedValue, ddlCombinacion.SelectedValue)
                Dim total_ing_dt As Integer = fnCalcularVacantesDisponiblesAdd("I_EDIT", Me.ddlCurso.SelectedValue)

                Dim txting As Integer = CInt(Me.txtdetnumero.Text)
                Dim vacantes_disp As Integer = NRO_VACANTES_DISP - total_ing

                'Response.Write("<br>2 NRO_VACANTES_DISP: " & NRO_VACANTES_DISP & "<br>")
                'Response.Write("total_ing_dt: " & total_ing_dt.ToString & "<br>")
                'Response.Write("total_ing: " & total_ing & "<br>")
                'Response.Write("txting: " & txting & "<br>")
                'Response.Write("vacantes_disp: " & vacantes_disp & "<br>")

                'Response.Write("IF  " & (NRO_VACANTES_DISP - (total_ing_dt - total_ing)).ToString & "<br>")
                'Response.Write("FIN  " & txting.ToString & ">" & ((NRO_VACANTES_DISP - (total_ing_dt - total_ing))).ToString & "<br>")

                If (txting > (NRO_VACANTES_DISP - (total_ing_dt - total_ing))) Then
                    script = "fnMensaje('warning','El Numero de ingresantes supera las " & (NRO_VACANTES_DISP - total_ing_dt + total_ing).ToString() & "  disponibles')"
                    fnNotificacion(script)
                    Return False
                End If


            End If

            Return True
        Catch ex As Exception
            Response.Write(ex.StackTrace)
            Dim script As String = "fnMensaje('error','" & ex.Message & "')"
            'Dim script = "fnMensaje('warning','El Numero de ingresantes debe ser mayor o igual  las " & dt.Rows(0).Item("faltan").ToString() & " ingrersantes que faltan nma ')"
            ScriptManager.RegisterStartupScript(Me, GetType(Page), "alert", script, True)
        End Try

    End Function



    Protected Sub gDataCombDet_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gDataCombDet.RowCommand
        Try
       

            If (e.CommandName.Equals("Select")) Then
                Dim seleccion As GridViewRow
                ' Dim ID_COMBDET As Integer

                '1. Obtengo la linea del gridview que fue cliqueada
                seleccion = DirectCast(e.CommandSource, GridView).Rows(e.CommandArgument)
                '2. Obtengo el datakey de la linea que donde está el boton que cliqueé

                Session("ID_COMBDET") = CInt(Me.gDataCombDet.DataKeys(seleccion.RowIndex).Values("id").ToString())

                Dim obj As New ClsConectarDatos
                Dim dt As New Data.DataTable
                obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
                obj.AbrirConexion()
                dt = obj.TraerDataTable("Escuela_combDetListar", "1", Session("ID_COMBDET"), 0, 0, 0)
                ' Session("dtDetalle") = dt
                obj.CerrarConexion()
                obj = Nothing
                If dt.Rows.Count > 0 Then
                    Me.ddlCurso.SelectedValue = dt.Rows(0).Item("codigo_cup")
                    Me.ddlCombinacion.SelectedValue = dt.Rows(0).Item("nrocombinacion")
                    Me.txtdetnumero.Text = dt.Rows(0).Item("nroestudantes")

                    Me.btnDetCerrar.Visible = True
                    Me.divInfoEdit.Visible = True
                    Me.txtinfoeditar.Text = "Se va a editar: " & dt.Rows(0).Item("curso").ToString() & " [" & dt.Rows(0).Item("grupo").ToString() & "], COMBINACION Nro: " & dt.Rows(0).Item("nrocombinacion").ToString() & ",  Nro Estudiantes: " & dt.Rows(0).Item("nroestudantes").ToString()
                Else
                    Me.btnDetCerrar.Visible = False
                    Me.divInfoEdit.Visible = False
                    Me.txtinfoeditar.Text = ""
                End If

                ValidaGridDetalle()
            End If


        Catch ex As Exception
            Dim script As String = "fnMensaje('error','1" & ex.Message & "')"
            ScriptManager.RegisterStartupScript(Me, GetType(Page), "alert", script, True)
        End Try
    End Sub

    Protected Sub gDataCombDet_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gDataCombDet.RowDataBound
        Try

            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim row As Data.DataRowView = CType(e.Row.DataItem, Data.DataRowView)
                If LastCategory = row("nrocombinacion").ToString Then
                    If (gDataCombDet.Rows(CurrentRow).Cells(0).RowSpan = 0) Then
                        gDataCombDet.Rows(CurrentRow).Cells(0).RowSpan = 2
                    Else
                        gDataCombDet.Rows(CurrentRow).Cells(0).RowSpan += 1
                    End If
                    'e.Row.Cells.RemoveAt(0)
                    e.Row.Cells(0).Visible = False
                Else
                    e.Row.VerticalAlign = VerticalAlign.Middle
                    LastCategory = row("nrocombinacion").ToString()
                    CurrentRow = e.Row.RowIndex
                End If
            End If
            ValidaGridDetalle()
            'If e.Row.RowIndex >= 0 Then
            '    If e.Row.Cells(4).Text = "0" Then
            '        e.Row.Cells(5).Text = ""
            '        e.Row.Cells(6).Text = ""
            '    End If
            '    If e.Row.Cells(3).Text <> e.Row.Cells(4).Text Then
            '        'e.Row.Cells(5).Text = ""
            '        e.Row.Cells(6).Text = ""
            '    End If
            'End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub
    

    Private Sub ValidaGridDetalle()
        For i As Integer = 0 To gDataCombDet.Rows.Count - 1
            If gDataCombDet.Rows(i).Cells(9).Text = "0" Then
                gDataCombDet.Rows(i).Cells(10).Text = ""
                gDataCombDet.Rows(i).Cells(11).Text = ""
            End If


        Next

    End Sub


    Protected Sub gDataCombDet_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles gDataCombDet.RowDeleting
        Try
            Dim script As String = ""

            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            script = "fnMensaje('success','Combinación eliminada con éxito')"
            obj.Ejecutar("Escuela_detcombReg", "E", CInt(gDataCombDet.DataKeys(e.RowIndex).Value.ToString()), 0, 0, 0, 0, 0, Session("perlogin"), 0)
            obj.CerrarConexion()
            obj = Nothing



            'ScriptManager.RegisterStartupScript(Me, GetType(Page), "alert", script, True)
            fnNotificacion(script)
            consultarDetCombinacion()



        Catch ex As Exception
            Dim script As String = "fnMensaje('error','1" & ex.Message & "')"
            ScriptManager.RegisterStartupScript(Me, GetType(Page), "alert", script, True)
        End Try
    End Sub

    Protected Sub btnDetCerrar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDetCerrar.Click
        LimpiarDetalleCombinacion()
        ValidaGridDetalle()
    End Sub
End Class
