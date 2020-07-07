
Partial Class Indicadores_Formularios_frmConfiguracionPeriodoFormula
    Inherits System.Web.UI.Page

    Dim usuario As Integer
    Dim vInsert As Boolean
    Protected widestData As Integer


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                CargaComboPeriodo()

                'Agregado 12.06.2012 xDguevara
                CargarComboPlanes()

                pnlGeneral.Visible = False
                EstadoFormulasConfiguradas(False)
            End If

            'Debe tomar del inicio de sesión
            usuario = Request.QueryString("id")

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub


    Private Sub CargarComboPlanes()
        Try
            Dim obj As New clsIndicadores
            Dim dts As New Data.DataTable
            dts = obj.ListaPlanes(Request.QueryString("ctf"), Request.QueryString("id"))

            If dts.Rows.Count > 0 Then
                ddlPlan.DataSource = dts
                ddlPlan.DataTextField = "Descripcion"
                ddlPlan.DataValueField = "Codigo"
                ddlPlan.DataBind()
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub CargarListaFormulasRegistradas(ByVal vParametro As String, _
                                               ByVal codigo_pla As Integer, _
                                               ByVal anio As String, _
                                               ByVal nombre_obj As String, ByVal sintaxis As String)
        Try
            Dim obj As New clsIndicadores
            Dim dts As New Data.DataTable

            dts = obj.ListaFormulasRegistradas(vParametro.Trim, codigo_pla, anio.Trim, nombre_obj.Trim, sintaxis)
            If dts.Rows.Count > 0 Then
                gvListaFormulas.DataSource = dts
                gvListaFormulas.DataBind()
            Else
                gvListaFormulas.DataSource = Nothing
                gvListaFormulas.DataBind()
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub CargaComboPeriodo()
        Try
            Dim obj As New clsIndicadores
            Dim dts As New Data.DataTable
            dts = obj.ListaPeriodos()
            'Response.Write(dts.Rows.Count)
            If dts.Rows.Count > 0 Then
                ddlPeriodo.DataSource = dts
                ddlPeriodo.DataTextField = "Descripcion"
                ddlPeriodo.DataValueField = "Codigo"
                ddlPeriodo.DataBind()
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub


    Private Function VerificaSeleccionItem() As Boolean
        Dim Fila As GridViewRow
        Dim sw As Byte = 0

        For i As Integer = 0 To gvListaFormulas.Rows.Count - 1

            Fila = gvListaFormulas.Rows(i)
            Dim valor As Boolean = CType(Fila.FindControl("chkElegir"), CheckBox).Checked
            If (valor = True) Then
                sw = 1
            End If
        Next

        If (sw = 1) Then
            Return True
        End If

        Return False
    End Function


    Private Function VerificaDuplicadoFormulaPeriodo() As Boolean
        Dim Fila As GridViewRow
        Dim sw As Byte = 0
        Dim vCodigo_for As Integer

        Dim obj As New clsIndicadores
        Dim dts As New Data.DataTable


        For i As Integer = 0 To gvListaFormulas.Rows.Count - 1

            Fila = gvListaFormulas.Rows(i)
            Dim valor As Boolean = CType(Fila.FindControl("chkElegir"), CheckBox).Checked
            If (valor = True) Then
                vCodigo_for = Convert.ToInt32(gvListaFormulas.DataKeys(Fila.RowIndex).Value)
                dts = obj.ValidaInsercionFormulaPeriodo(CType(Me.ddlPeriodo.SelectedValue, String), vCodigo_for)
                If dts.Rows(0).Item("Cantidad") = 1 Then
                    Exit Function
                Else
                    sw = 1
                End If
            End If
        Next
        If (sw = 1) Then
            Return True
        End If

        Return False
    End Function

    Private Function validaCheckActivo() As Boolean
        'Lo usamos para ver si existen formulas ya registradas para un periodo dado
        vInsert = False

        Dim obj As New clsIndicadores
        Dim dts As New Data.DataTable

        Dim Fila As GridViewRow
        Dim sw As Byte = 0
        Dim vCodigo_for As Integer

        For i As Integer = 0 To gvListaFormulas.Rows.Count - 1
            'Capturamos las filas que estan activas
            Fila = gvListaFormulas.Rows(i)
            Dim valor As Boolean = CType(Fila.FindControl("chkElegir"), CheckBox).Checked
            If (valor = True) Then
                sw = 1
                vCodigo_for = Convert.ToInt32(gvListaFormulas.DataKeys(Fila.RowIndex).Value)
                'dts = obj.ValidaInsercionFormulaPeriodo(CType(Me.ddlPeriodo.SelectedValue, String), vCodigo_for)
                'Response.Write(dts.Rows.Count)
                'If dts.Rows(0).Item("Cantidad") <> 1 Then
                obj.InsertaFormulaPeriodo(CType(Me.ddlPeriodo.SelectedValue, String), vCodigo_for, usuario)
                vInsert = True
                'End If
            End If
        Next

        If (sw = 1) Then
            Return True
        End If

        Return False
    End Function


    Private Function validaCheckActivoFormulas() As Boolean
        'Lo usamos para ver si existen formulas ya registradas para un periodo dado
        vInsert = False

        Dim obj As New clsIndicadores
        Dim dts As New Data.DataTable

        Dim Fila As GridViewRow
        Dim sw As Byte = 0

        For i As Integer = 0 To gvPeriodoFormula.Rows.Count - 1
            'Capturamos las filas que estan activas
            Fila = gvPeriodoFormula.Rows(i)
            Dim valor As Boolean = CType(Fila.FindControl("chkElegir01"), CheckBox).Checked
            If (valor = True) Then
                sw = 1
            End If
        Next

        If (sw = 1) Then
            Return True
        End If

        Return False
    End Function



    Protected Sub cmdGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdGuardar.Click
        Try
            'Validaciones:
            '-1) Verifica que tenga seleccionado un periodo.
            
            If ddlPeriodo.SelectedValue.ToString.Trim = "P0" Then
                ClientScript.RegisterStartupScript(Me.GetType, "FaltanDatos", "alert('Ud. debe seleccionar un Periodo.');", True)
                ddlPeriodo.Focus()
                Exit Sub
            End If

            '-2) Verifica que se hayan seleccionado items.
            If VerificaSeleccionItem() = False Then
                ClientScript.RegisterStartupScript(Me.GetType, "FaltanDatos", "alert('Debe marcar almenos un registro para poder asignar al periodo seleccionado.');", True)
                Exit Sub
            End If

            '3-) Verifica que los items seleccionados no se encuentres ya registrados.
            If VerificaDuplicadoFormulaPeriodo() = False Then
                ClientScript.RegisterStartupScript(Me.GetType, "FaltanDatos", "alert('La fórmula seleccionada ya se encuentra asiganda al periodo.');", True)
                Exit Sub
            End If

            If (validaCheckActivo() = True) Then
                If ddlPeriodo.SelectedValue <> "P0" Then
                    FiltroFormulasConfiguradas()
                End If

                'LimpiarControles()
                If vInsert = True Then
                    'lblMensaje.Visible = True
                    'lblMensaje.ForeColor = Drawing.Color.Black
                    'lblMensaje.Text = "Los items fueron registrados correctamente para el periodo seleccionado."
                    'Me.Image1.Attributes.Add("src", "../Images/accept.png")
                    'Me.avisos.Attributes.Add("class", "mensajeExito")

                    rbtTodos.Checked = False
                    rbtErrores.Checked = False
                    rbtProcesadas.Checked = False
                    rbtNoProcesadas.Checked = True

                    EstadoListaFormulas(False)
                    EstadoFormulasConfiguradas(True)
                    FiltroFormulasConfiguradas()

                    '====================================================================================================================================================================
                    ClientScript.RegisterStartupScript(Me.GetType, "FaltanDatos", "alert('Los items fueron registrados correctamente para el periodo seleccionado.');", True)
                    '====================================================================================================================================================================
                End If
            Else

                'lblMensaje.Visible = True
                'lblMensaje.ForeColor = Drawing.Color.Black
                'lblMensaje.Text = "Debe seleccionar una item como mínimo para poder agregar al Periodo seleccionado."
                'Me.Image1.Attributes.Add("src", "../Images/exclamation.png")
                'Me.avisos.Attributes.Add("class", "mensajeError")


                EstadoListaFormulas(True)
                EstadoFormulasConfiguradas(False)
                FiltroFormulasConfiguradas()

                '====================================================================================================================================================================
                ClientScript.RegisterStartupScript(Me.GetType, "FaltanDatos", "alert('Debe seleccionar una item como mínimo para poder agregar al Periodo seleccionado.');", True)
                '====================================================================================================================================================================
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub LimpiarControles()
        Try
            'ddlPeriodo.SelectedValue = "P0"
            'CargarListaFormulasRegistradas("O", "%")
            lblMensaje.Visible = False
            lblMensaje.Text = ""
            Me.Image1.Attributes.Add("src", "../Images/beforelastchild.GIF")
            Me.avisos.Attributes.Add("class", "none")
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub ListaFormulasConfiguradas(ByVal codigo_pdo As String, _
                                          ByVal nombre_ind As String, _
                                          ByVal codigo_pla As Integer, _
                                          ByVal anio As String, _
                                          ByVal nombre_obj As String)
        Try
            Dim obj As New clsIndicadores
            Dim dts As New Data.DataTable
            Dim sintaxis As String = ""

            'Opcion:
            '------------------------------------------------------------------------------
            If rbtErrores.Checked = True Then sintaxis = "E" 'Errores
            If rbtNoProcesadas.Checked = True Then sintaxis = "N" 'Sin Procesar
            If rbtProcesadas.Checked = True Then sintaxis = "C" 'Procesadas
            If rbtTodos.Checked = True Then sintaxis = "%"
            '------------------------------------------------------------------------------


            gvPeriodoFormula.DataSource = Nothing
            gvPeriodoFormula.DataBind()


            'Response.Write("codigo_pdo: " & codigo_pdo)
            'Response.Write("<br />")
            'Response.Write("nombre_ind: " & nombre_ind)
            'Response.Write("<br />")
            'Response.Write("codigo_pla: " & codigo_pla)
            'Response.Write("<br />")
            'Response.Write("anio: " & anio)
            'Response.Write("<br />")
            'Response.Write("nombre_obj: " & nombre_obj)
            'Response.Write("<br />")
            'Response.Write("sintaxis: " & sintaxis)
            'Response.Write("<br />")



            dts = obj.ListaFormulasConfiguradasPeriodo(codigo_pdo, nombre_ind, codigo_pla, anio, nombre_obj, sintaxis)
            If dts.Rows.Count > 0 Then
                gvPeriodoFormula.DataSource = dts
                gvPeriodoFormula.DataBind()
            End If

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub ddlPeriodo_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPeriodo.SelectedIndexChanged
        Try
            FiltroFormulasConfiguradas()

            'Response.Write("ddlPeriodo: " & ddlPeriodo.SelectedValue.ToString)
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub


    Private Sub FiltroFormulasConfiguradas()
        Try
            '-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
            ListaFormulasConfiguradas(CType(ddlPeriodo.SelectedValue, String), Me.txtBuscar.Text.Trim, Me.ddlPlan.SelectedValue, ddlAnioBus.SelectedValue, ddlListaObjetivos.SelectedValue)
            '-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub


    Protected Sub gvPeriodoFormula_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvPeriodoFormula.RowCommand
        Try
            '-----------------------------------------------------------------------------------------------------------------
            'El nombre de esta propiedad debe ser igual en la parte del diseño, verificar para su correcto funcionamiento
            'ButtonField = e.CommandName = "PSDO" 
            'xDguevara 26.03.2012
            '-----------------------------------------------------------------------------------------------------------------
            Dim obj As New clsIndicadores
            Dim dts As New Data.DataTable
            Dim seleccion As GridViewRow
            Dim codigo_for As Integer
            Dim codigo_pdo As String

            '-------------------------------
            'PROCESAMIENTO DE LAS FORMULAS
            '-------------------------------

            If e.CommandName = "Procesar" Then

                seleccion = DirectCast(e.CommandSource, GridView).Rows(e.CommandArgument)
                codigo_for = Convert.ToInt32(gvPeriodoFormula.DataKeys(seleccion.RowIndex).Values("codigo_for"))
                codigo_pdo = gvPeriodoFormula.DataKeys(seleccion.RowIndex).Values("codigo_pdo").ToString

                '// Procesa una fórmula para el periodo de selección

                'Response.Write("codigo_for: " & codigo_for)
                'Response.Write("<br>")
                'Response.Write("codigo_pdo: " & codigo_pdo)
                'Response.Write("<br>")

                'Antes funcionaba esta linea: 24.09.2013
                'dts = obj.ProcesaFormula(codigo_for, ddlPeriodo.SelectedValue.ToString)
                '----------------------------------------------------------------------------------------
                'Linea en funcionamiento para 25.09.2013 : xDguevara
                dts = obj.ProcesaFormula(codigo_for, codigo_pdo)
                '----------------------------------------------------------------------------------------

                'Response.Write("ValorFormula_vf: " & dts.Rows(0).Item("ValorFormula_vf"))
                'Response.Write("<br>")
                'Response.Write("Sintaxis: " & dts.Rows(0).Item("Sintaxis"))

                rbtTodos.Checked = True
                rbtErrores.Checked = False
                rbtProcesadas.Checked = False
                rbtNoProcesadas.Checked = False
                FiltroFormulasConfiguradas()


                'Alerta para el usuario.
                If dts.Rows(0).Item("VerificaProceso").ToString.Trim = "E" Then
                    ClientScript.RegisterStartupScript(Me.GetType, "FaltanDatos", "alert('El procesamiento de la fórmula detecto errores en los valores, favor de verificar.');", True)
                Else
                    ClientScript.RegisterStartupScript(Me.GetType, "FaltanDatos", "alert('El procesamiento de la fórmula fue correcto.');", True)
                End If
            End If

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub


    Protected Sub gvListaFormulas_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvListaFormulas.RowDataBound
        Try

            If e.Row.RowType = DataControlRowType.DataRow Then

                e.Row.Attributes.Add("OnMouseOver", "Resaltar(1,this,'S')")
                e.Row.Attributes.Add("OnMouseOut", "Resaltar(0,this,'S')")

                'If (e.Row.Cells(10).Text = "C" Or e.Row.Cells(10).Text = "E") Then
                'If (e.Row.Cells(10).Text = "E") Then
                '    Dim Cb As CheckBox
                '    Cb = e.Row.FindControl("chkElegir")
                '    Cb.Visible = False
                'End If


                Select Case e.Row.Cells(10).Text
                    Case "E"        ' Formula con Errores en la sintaxis
                        e.Row.Cells(10).Text = "<center><img src='../images/bola_roja.gif' style='border: 0px'/></center>"
                        e.Row.Cells(9).ForeColor = Drawing.Color.Red
                    Case "C"        ' Formula Correcta
                        e.Row.Cells(10).Text = "<center><img src='../images/bola_verde.gif' style='border: 0px'/></center>"
                        e.Row.Cells(9).ForeColor = Drawing.Color.Blue
                    Case "N"        ' Formula sin procesar, por definir su sintaxis. Le asigna 'N', por defecto cuando se inserta la formula.
                        e.Row.Cells(10).Text = "<center><img src='../images/bola_naranja.gif' style='border: 0px'/></center>"
                End Select



                'Recorremos la formula, para darle formato. 
                If e.Row.Cells(9).Text <> "" Then
                    Dim vStrCadena As String = ""
                    Dim vStr As String = e.Row.Cells(9).Text

                    For i As Integer = 0 To vStr.Length - 1
                        'Response.Write(vStr.Chars(i))
                        'Response.Write("<br />")
                        If (vStr.Chars(i) = "+" Or vStr.Chars(i) = "-" Or vStr.Chars(i) = "*" Or vStr.Chars(i) = "/") Then
                            vStrCadena = vStrCadena + " " + vStr.Chars(i).ToString
                        Else
                            vStrCadena = vStrCadena + vStr.Chars(i).ToString
                        End If
                    Next
                    e.Row.Cells(9).Text = vStrCadena
                End If

            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub


    Protected Sub gvPeriodoFormula_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvPeriodoFormula.RowDataBound
        Try
            'Importante este IF DataControlRowType.DataRow para no evaluar la cabezera del gridview xDguevara.
            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim fila As Data.DataRowView
                fila = e.Row.DataItem

                e.Row.Attributes.Add("OnMouseOver", "Resaltar(1,this,'S')")
                e.Row.Attributes.Add("OnMouseOut", "Resaltar(0,this,'S')")

                Select Case e.Row.Cells(11).Text
                    Case -1
                        e.Row.Cells(11).Text = "<center><img src='../images/bola_roja.gif' style='border: 0px'/></center>"
                    Case 0
                        e.Row.Cells(11).Text = "<center><img src='../images/bola_naranja.gif' style='border: 0px'/></center>"
                    Case 1
                        e.Row.Cells(11).Text = "<center><img src='../images/bola_verde.gif' style='border: 0px'/></center>"
                End Select


                '--------------------------------------------------------------------------------------------------------

                'Verifica si la Formula presenta Errores, Lo pinta de color rojo para indicar su estado.
                If e.Row.Cells(8).Text.Substring(0, 1) = "E" Then
                    'e.Row.Cells(3).BackColor = Drawing.Color.Red   'Pinta la celda indicada
                    'e.Row.Cells(4).ForeColor = Drawing.Color.Red   'Solo el texto
                    e.Row.BackColor = Drawing.Color.Gold      'Pinta toda la fila
                    e.Row.ForeColor = Drawing.Color.Red
                End If

                '--------------------------------------------------------------------------------------------------------

                'Response.Write(e.Row.Cells(1).Text)
                Dim Codigo_fp As Integer = CType(e.Row.Cells(1).Text, Integer)
                Dim Codigo_pdo As String '= ddlPeriodo.SelectedValue.ToString

                Codigo_pdo = gvPeriodoFormula.DataKeys(e.Row.RowIndex).Values("codigo_pdo").ToString
                e.Row.Cells(10).Text = "<a href='frmDetalleFormulaPeriodo.aspx?Codigo_fp=" & Codigo_fp & "&codigo_pdo=" & Codigo_pdo & "&KeepThis=true&TB_iframe=true&height=600&width=800&modal=true' title='Ver Detalle Fórmula' class='thickbox'><img src='../images/tabla.gif' border=0 /><a/>"


                'Recorremos la formula, para darle formato. dscvariable.png
                If e.Row.Cells(7).Text <> "" Then
                    Dim vStrCadena As String = ""
                    Dim vStr As String = e.Row.Cells(7).Text

                    For i As Integer = 0 To vStr.Length - 1
                        'Response.Write(vStr.Chars(i))
                        'Response.Write("<br />")
                        If (vStr.Chars(i) = "+" Or vStr.Chars(i) = "-" Or vStr.Chars(i) = "*" Or vStr.Chars(i) = "/") Then
                            vStrCadena = vStrCadena + " " + vStr.Chars(i).ToString
                        Else
                            vStrCadena = vStrCadena + vStr.Chars(i).ToString
                        End If
                    Next
                    e.Row.Cells(7).Text = vStrCadena
                End If



                'Recorremos la formula, para darle formato. dscvariable.png
                If e.Row.Cells(8).Text <> "" Then
                    Dim vStrCadena As String = ""
                    Dim vStr As String = e.Row.Cells(8).Text

                    For i As Integer = 0 To vStr.Length - 1
                        'Response.Write(vStr.Chars(i))
                        'Response.Write("<br />")
                        If (vStr.Chars(i) = "+" Or vStr.Chars(i) = "-" Or vStr.Chars(i) = "*" Or vStr.Chars(i) = "/") Then
                            vStrCadena = vStrCadena + " " + vStr.Chars(i).ToString
                        Else
                            vStrCadena = vStrCadena + vStr.Chars(i).ToString
                        End If
                    Next
                    e.Row.Cells(8).Text = vStrCadena
                End If


                'Linea para crear el correlativo del registro.
                e.Row.Cells(0).Text = e.Row.RowIndex + 1
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub


    Protected Sub gvPeriodoFormula_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gvPeriodoFormula.PageIndexChanging
        Try
            gvPeriodoFormula.PageIndex = e.NewPageIndex
            FiltroFormulasConfiguradas()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub


    Protected Sub gvPeriodoFormula_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles gvPeriodoFormula.RowDeleting
        Try
            'Eli

            Dim obj As New clsIndicadores
            Dim dts As New Data.DataTable

            dts = obj.EliminaPeriodoFormula(gvPeriodoFormula.DataKeys(e.RowIndex).Value.ToString())
            If dts.Rows(0).Item("rpt").ToString = "1" Then
                ClientScript.RegisterStartupScript(Me.GetType, "FaltanDatos", "alert('El registro fue eliminado correctamente.');", True)
                'lblMensaje.Visible = True
                'lblMensaje.ForeColor = Drawing.Color.Green
                'lblMensaje.Text = dts.Rows(0).Item("Mensaje").ToString
            Else
                ClientScript.RegisterStartupScript(Me.GetType, "FaltanDatos", "alert('Ocurrio un error al tratar de eliminar el registro.');", True)
                'lblMensaje.Visible = True
                'lblMensaje.ForeColor = Drawing.Color.Red
                'lblMensaje.Text = dts.Rows(0).Item("Mensaje").ToString
            End If

            'Consultamos la lista de registros
            'If ddlPeriodo.SelectedValue <> "P0" Then
            ' FiltroFormulasConfiguradas()
            ' End If

            FiltroFormulasConfiguradas()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub gvListaFormulas_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gvListaFormulas.PageIndexChanging
        Try
            gvListaFormulas.PageIndex = e.NewPageIndex
            'CargarListaFormulasRegistradas("O", "%")
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub



    Protected Sub gvListaFormulas_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles gvListaFormulas.RowDeleting
        Try
            Dim obj As New clsIndicadores
            Dim dts As New Data.DataTable
            Dim dtsv As New Data.DataTable

            'Validacion: Si la formula esta amarrada a un periodo laboral, no se puede eliminar.
            dtsv = obj.ValidarInclusionFormulaPeriodo(gvListaFormulas.DataKeys(e.RowIndex).Value)
            If dtsv.Rows(0).Item("Mensaje").ToString.Trim.Length > 0 Then
                lblMensaje.Visible = True
                lblMensaje.ForeColor = Drawing.Color.Black
                lblMensaje.Text = dtsv.Rows(0).Item("Mensaje").ToString.Trim
                Me.Image1.Attributes.Add("src", "../Images/exclamation.png")
                Me.avisos.Attributes.Add("class", "mensajeError")
                Exit Sub
            End If
            '--- fin validacion

            dts = obj.EliminaFormula(gvListaFormulas.DataKeys(e.RowIndex).Value)
            If dts.Rows(0).Item("rpt").ToString = "1" Then
                lblMensaje.Visible = True
                lblMensaje.ForeColor = Drawing.Color.Green
                lblMensaje.Text = dts.Rows(0).Item("Mensaje").ToString
                Me.Image1.Attributes.Add("src", "../Images/accept.png")
                Me.avisos.Attributes.Add("class", "mensajeExito")
            Else
                lblMensaje.Visible = True
                lblMensaje.ForeColor = Drawing.Color.Red
                lblMensaje.Text = dts.Rows(0).Item("Mensaje").ToString
                Me.Image1.Attributes.Add("src", "../Images/exclamation.png")
                Me.avisos.Attributes.Add("class", "mensajeError")
            End If

            'Consultamos la lista de registros
            'CargarListaFormulasRegistradas("O", "%")
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub EjecutaFiltros()
        Try

            Dim sintaxis As String = ""
            If rbtErrores.Checked = True Then sintaxis = "E" 'Errores
            If rbtNoProcesadas.Checked = True Then sintaxis = "N" 'Sin Procesar
            If rbtProcesadas.Checked = True Then sintaxis = "C" 'Procesadas
            If rbtTodos.Checked = True Then sintaxis = "%"

            CargarListaFormulasRegistradas(Me.txtBuscar.Text.Trim, ddlPlan.SelectedValue, ddlAnioBus.SelectedValue, ddlListaObjetivos.Text, sintaxis)

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub


    Protected Sub cmdBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdBuscar.Click
        Try
            If Page.IsValid Then        'Si no ha ingresado cadenas inválidas (select, php, script)

                If ddlPlan.SelectedValue = 0 Then
                    ClientScript.RegisterStartupScript(Me.GetType, "FaltanDatos", "alert('Seleccione un Plan Estratégico.');", True)
                    ddlPlan.Focus()
                    Exit Sub
                End If

                If ddlPlan.SelectedValue <> 0 Then


                    '--------------------------------------------------------------------
                    Dim sintaxis As String = ""

                    'Opcion:
                    If rbtErrores.Checked = True Then sintaxis = "E" 'Errores
                    If rbtNoProcesadas.Checked = True Then sintaxis = "N" 'Sin Procesar
                    If rbtProcesadas.Checked = True Then sintaxis = "C" 'Procesadas
                    If rbtTodos.Checked = True Then sintaxis = "%"
                    '--------------------------------------------------------------------

                    If txtBuscar.Text = "" Then
                        CargarListaFormulasRegistradas("%", _
                                                       ddlPlan.SelectedValue, _
                                                       ddlAnioBus.SelectedValue, _
                                                       ddlListaObjetivos.SelectedValue, sintaxis)
                    Else
                        CargarListaFormulasRegistradas(Me.txtBuscar.Text.Trim, _
                                                       ddlPlan.SelectedValue, _
                                                       ddlAnioBus.SelectedValue, _
                                                       ddlListaObjetivos.Text, sintaxis)
                    End If
                End If

            Else 'Limpia la cadena inválida de la caja de texto
                txtBuscar.Text = ""
            End If

            EstadoListaFormulas(True)
            EstadoFormulasConfiguradas(False)
            pnlGeneral.Visible = True

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub cmdCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdCancelar.Click
        Try
            Me.LimpiarControles()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    'Para Validar Caja de Busqueda
    Protected Sub CustomValidator2_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles CustomValidator2.ServerValidate
        Try
            Dim obj As New clsIndicadores
            Dim dts As New Data.DataTable

            dts = obj.ValidarPalabrasReservadas(args.Value.ToString.Trim)
            If dts.Rows(0).Item("Encontro") > 0 Then
                args.IsValid = False
            Else
                args.IsValid = True
            End If

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub


    Protected Sub ddlPlan_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPlan.SelectedIndexChanged
        Try
            'Response.Write("Codigo_pla: " & ddlPlan.SelectedValue)
            If ddlPlan.SelectedValue <> 0 Then
                AniosObjetivo()
                ListaObjetivosBusqueda()
                EjecutaFiltros()

                pnlGeneral.Visible = True
                EstadoListaFormulas(True)
                EstadoFormulasConfiguradas(False)
            Else
                pnlGeneral.Visible = False
                EstadoListaFormulas(False)
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub ListaObjetivosBusqueda()
        Try
            Dim dts2 As New Data.DataTable
            Dim obj As New clsIndicadores

            If ddlAnioBus.SelectedValue = "%" Then
                dts2 = obj.ListaObjetivosBusqueda(Me.ddlPlan.SelectedValue, "%")
                If dts2.Rows.Count > 0 Then
                    ddlListaObjetivos.DataSource = dts2
                    ddlListaObjetivos.DataTextField = "Descripcion"
                    ddlListaObjetivos.DataValueField = "Codigo"
                    ddlListaObjetivos.DataBind()
                End If
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub AniosObjetivo()
        Try
            Dim dts As New Data.DataTable
            Dim obj As New clsIndicadores

            If ddlPlan.SelectedValue <> 0 Then
                dts = obj.ListaAniosObjetivosBusqueda(ddlPlan.SelectedValue)
                If dts.Rows.Count > 0 Then
                    ddlAnioBus.DataSource = dts
                    ddlAnioBus.DataTextField = "Descripcion"
                    ddlAnioBus.DataValueField = "Codigo"
                    ddlAnioBus.DataBind()
                End If
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub ddlAnioBus_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlAnioBus.SelectedIndexChanged
        Try
            Dim dts As New Data.DataTable
            Dim obj As New clsIndicadores

            ''Response.Write(ddlAnioBus.SelectedValue)
            If ddlPlan.SelectedValue <> 0 Then
                dts = obj.ListaObjetivosBusqueda(Me.ddlPlan.SelectedValue, Me.ddlAnioBus.SelectedValue)
                ddlListaObjetivos.DataSource = dts
                ddlListaObjetivos.DataTextField = "Descripcion"
                ddlListaObjetivos.DataValueField = "Codigo"
                ddlListaObjetivos.DataBind()
            End If

            EjecutaFiltros()
            FiltroFormulasConfiguradas()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub


    Protected Sub ddlListaObjetivos_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlListaObjetivos.SelectedIndexChanged
        Try
            'Response.Write(ddlListaObjetivos.Text)
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    

    Protected Sub lnkConsulta_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkConsulta.Click
        Try
            EstadoListaFormulas(False)
            EstadoFormulasConfiguradas(True)

            'Lista Datos
            FiltroFormulasConfiguradas()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub EstadoListaFormulas(ByVal vEstado As Boolean)
        Try
            pnlListaFormulas.Visible = vEstado
            pnlOpcionesListaFormula.Visible = vEstado

            If vEstado = True Then
                lnkConfiguracion.ForeColor = Drawing.Color.Red
            Else
                lnkConfiguracion.ForeColor = Drawing.Color.Blue
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub EstadoFormulasConfiguradas(ByVal vEstado As Boolean)
        Try
            pnlFormulasConf.Visible = vEstado

            If vEstado = True Then
                lnkConsulta.ForeColor = Drawing.Color.Red
            Else
                lnkConsulta.ForeColor = Drawing.Color.Blue
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub lnkConfiguracion_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkConfiguracion.Click
        Try
            EstadoFormulasConfiguradas(False)
            EstadoListaFormulas(True)

            EjecutaFiltros()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub rbtTodos_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbtTodos.CheckedChanged
        Try
            If rbtTodos.Checked Then
                FiltroFormulasConfiguradas()

                If ddlPlan.SelectedValue = 0 Then
                    ClientScript.RegisterStartupScript(Me.GetType, "FaltanDatos", "alert('Seleccione un Plan Estratégico.');", True)
                    ddlPlan.Focus()
                    Exit Sub
                End If
                EjecutaFiltros()
                pnlGeneral.Visible = True
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub rbtProcesadas_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbtProcesadas.CheckedChanged
        Try



            If rbtProcesadas.Checked Then

                'Response.Write("ENTRA PROCES")
                'FiltroFormulasConfiguradas()

                'If ddlPlan.SelectedValue = 0 Then
                '    ClientScript.RegisterStartupScript(Me.GetType, "FaltanDatos", "alert('Seleccione un Plan Estratégico.');", True)
                '    ddlPlan.Focus()
                '    Exit Sub
                'End If

                FiltroFormulasConfiguradas()
                EjecutaFiltros()
                pnlGeneral.Visible = True
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub rbtNoProcesadas_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbtNoProcesadas.CheckedChanged
        Try
            If rbtNoProcesadas.Checked Then
                'FiltroFormulasConfiguradas()

                'If ddlPlan.SelectedValue = 0 Then
                '    ClientScript.RegisterStartupScript(Me.GetType, "FaltanDatos", "alert('Seleccione un Plan Estratégico.');", True)
                '    ddlPlan.Focus()
                '    Exit Sub
                'End If

                FiltroFormulasConfiguradas()
                EjecutaFiltros()
                pnlGeneral.Visible = True
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub rbtErrores_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbtErrores.CheckedChanged
        Try
            If rbtErrores.Checked Then
                'If ddlPlan.SelectedValue = 0 Then
                '    ClientScript.RegisterStartupScript(Me.GetType, "FaltanDatos", "alert('Seleccione un Plan Estratégico.');", True)
                '    ddlPlan.Focus()
                '    Exit Sub
                'End If
                EjecutaFiltros()
                FiltroFormulasConfiguradas()
                pnlGeneral.Visible = True
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub btnEliminar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEliminar.Click
        Try
            If (validaCheckActivoFormulas() = True) Then
                'Response.Write("elimina.....")
                If EjecutaProcesoLote("E") = True Then
                    rbtErrores.Checked = False
                    rbtNoProcesadas.Checked = False
                    rbtProcesadas.Checked = False
                    rbtTodos.Checked = True
                    FiltroFormulasConfiguradas()
                    ClientScript.RegisterStartupScript(Me.GetType, "FaltanDatos", "alert('La eliminación se realizo correctamente.');", True)
                End If
            Else
                ClientScript.RegisterStartupScript(Me.GetType, "FaltanDatos", "alert('Debe marcar con check uno o varios registros para continuar con el proceso.');", True)
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub btnProcesar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnProcesar.Click
        Try
            If (validaCheckActivoFormulas() = True) Then
                'Response.Write("elimina.....")
                If EjecutaProcesoLote("P") = True Then
                    rbtErrores.Checked = False
                    rbtNoProcesadas.Checked = False
                    rbtProcesadas.Checked = False
                    rbtTodos.Checked = True
                    FiltroFormulasConfiguradas()
                    ClientScript.RegisterStartupScript(Me.GetType, "FaltanDatos", "alert('El procesamiento se realizo correctamente.');", True)
                End If
            Else
                ClientScript.RegisterStartupScript(Me.GetType, "FaltanDatos", "alert('Debe marcar con check uno o varios registros para continuar con el proceso.');", True)
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub


    Private Function EjecutaProcesoLote(ByVal tipo As String) As Boolean
        vInsert = False

        Dim obj As New clsIndicadores
        Dim dts As New Data.DataTable

        Dim Fila As GridViewRow
        Dim sw As Byte = 0

        For i As Integer = 0 To gvPeriodoFormula.Rows.Count - 1
            'Capturamos las filas que estan activas
            Fila = gvPeriodoFormula.Rows(i)
            Dim valor As Boolean = CType(Fila.FindControl("chkElegir01"), CheckBox).Checked
            If (valor = True) Then
                If tipo = "E" Then  'Elimina registro de fórmulas en lote
                    obj.EliminaPeriodoFormula(gvPeriodoFormula.DataKeys.Item(Fila.RowIndex).Values("Codigo"))
                    sw = 1
                Else
                    obj.ProcesaFormula(gvPeriodoFormula.DataKeys.Item(Fila.RowIndex).Values("codigo_for"), gvPeriodoFormula.DataKeys.Item(Fila.RowIndex).Values("codigo_pdo"))
                    sw = 2
                End If
            End If
        Next

        If (sw = 1 Or sw = 2) Then
            Return True
        End If

        Return False
    End Function

    
End Class
