
Partial Class Indicadores_Formularios_frmConfiguracionPeriodoFormula
    Inherits System.Web.UI.Page

    Dim usuario As Integer
    Dim vInsert As Boolean
    Protected widestData As Integer


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                'CargaComboConfiguracionPerspectivaPlan()
                CargaComboPeriodo()
                'ConsultarGridRegistros("%")

                '---------------------------------------------------------------------------
                'Lista todas las formulas registradas con estado activo
                'CargarListaFormulasRegistradas("O", "%")
                '---------------------------------------------------------------------------


                'Agregado 12.06.2012 xDguevara
                CargarComboPlanes()
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
            dts = obj.ListaPlanes("T")

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
                                               ByVal nombre_obj As String)
        Try
            Dim obj As New clsIndicadores
            Dim dts As New Data.DataTable

            'Response.Write(vParametro)
            ''Response.Write("<br />")
            'Response.Write(codigo_pla)
            'Response.Write("<br />")
            'Response.Write(anio)
            'Response.Write("<br />")
            'Response.Write(nombre_obj)
            'Response.Write("<br />")


            dts = obj.ListaFormulasRegistradas(vParametro.Trim, codigo_pla, anio.Trim, nombre_obj.Trim)

            'Response.Write(dts.Rows.Count)

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

                dts = obj.ValidaInsercionFormulaPeriodo(CType(Me.ddlPeriodo.SelectedValue, String), vCodigo_for)
                'Response.Write(dts.Rows.Count)

                If dts.Rows(0).Item("Cantidad") <> 1 Then
                    obj.InsertaFormulaPeriodo(CType(Me.ddlPeriodo.SelectedValue, String), vCodigo_for, usuario)
                    vInsert = True
                End If
            End If
        Next

        If (sw = 1) Then
            Return True
        End If

        Return False
    End Function

    Protected Sub cmdGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdGuardar.Click
        Try
            If (validaCheckActivo() = True) Then
                If ddlPeriodo.SelectedValue <> "P0" Then
                    ListaFormulasConfiguradas(CType(ddlPeriodo.SelectedValue, String))
                End If
                'LimpiarControles()
                If vInsert = True Then
                    lblMensaje.Visible = True
                    lblMensaje.ForeColor = Drawing.Color.Black
                    lblMensaje.Text = "Los items fueron registrados correctamente para el periodo seleccionado."
                    Me.Image1.Attributes.Add("src", "../Images/accept.png")
                    Me.avisos.Attributes.Add("class", "mensajeExito")
                End If
            Else
                lblMensaje.Visible = True
                lblMensaje.ForeColor = Drawing.Color.Black
                lblMensaje.Text = "Debe seleccionar una item como mínimo para poder agregar al Periodo seleccionado."
                Me.Image1.Attributes.Add("src", "../Images/exclamation.png")
                Me.avisos.Attributes.Add("class", "mensajeError")
            End If

            'CargarListaFormulasRegistradas("O", "%")
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

    Private Sub ListaFormulasConfiguradas(ByVal codigo_pdo As String)
        Try
            Dim obj As New clsIndicadores
            Dim dts As New Data.DataTable

            gvPeriodoFormula.DataSource = Nothing
            gvPeriodoFormula.DataBind()

            dts = obj.ListaFormulasConfiguradasPeriodo(codigo_pdo)
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
            'CargarListaFormulasRegistradas("O", "%")
            ListaFormulasConfiguradas(CType(ddlPeriodo.SelectedValue, String))
            'Response.Write(ddlPeriodo.SelectedValue)
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

            If e.CommandName = "Procesar" Then
                Dim seleccion As GridViewRow
                seleccion = DirectCast(e.CommandSource, GridView).Rows(e.CommandArgument)
                Dim codigo_for As Integer = Convert.ToInt32(gvPeriodoFormula.DataKeys(seleccion.RowIndex).Values("codigo_for"))

                Dim obj As New clsIndicadores
                Dim dts As New Data.DataTable

                '// Procesa una fórmula para el periodo de selección
                dts = obj.ProcesaFormula(codigo_for, ddlPeriodo.SelectedValue.ToString)

                ListaFormulasConfiguradas(CType(ddlPeriodo.SelectedValue, String))
                'Lista todas las formulas registradas con estado activo
                'CargarListaFormulasRegistradas("O", "%")
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
                    e.Row.BackColor = Drawing.Color.Tomato        'Pinta toda la fila
                    e.Row.ForeColor = Drawing.Color.White
                End If

                '--------------------------------------------------------------------------------------------------------

                'Response.Write(e.Row.Cells(1).Text)
                Dim Codigo_fp As Integer = CType(e.Row.Cells(1).Text, Integer)
                Dim Codigo_pdo As String = ddlPeriodo.SelectedValue.ToString

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
            ListaFormulasConfiguradas(CType(ddlPeriodo.SelectedValue, String))
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

   
    Protected Sub gvPeriodoFormula_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles gvPeriodoFormula.RowDeleting
        Try
            Dim obj As New clsIndicadores
            Dim dts As New Data.DataTable

            dts = obj.EliminaPeriodoFormula(gvPeriodoFormula.DataKeys(e.RowIndex).Value.ToString())

            If dts.Rows(0).Item("rpt").ToString = "1" Then
                lblMensaje.Visible = True
                lblMensaje.ForeColor = Drawing.Color.Green
                lblMensaje.Text = dts.Rows(0).Item("Mensaje").ToString
            Else
                lblMensaje.Visible = True
                lblMensaje.ForeColor = Drawing.Color.Red
                lblMensaje.Text = dts.Rows(0).Item("Mensaje").ToString
            End If

            'Consultamos la lista de registros
            If ddlPeriodo.SelectedValue <> "P0" Then
                ListaFormulasConfiguradas(CType(ddlPeriodo.SelectedValue, String))
            End If

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


 
    Protected Sub cmdBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdBuscar.Click
        Try
            If Page.IsValid Then        'Si no ha ingresado cadenas inválidas (select, php, script)
                If ddlPlan.SelectedValue <> 0 Then
                    If txtBuscar.Text = "" Then
                        CargarListaFormulasRegistradas("%", _
                                                       ddlPlan.SelectedValue, _
                                                       ddlAnioBus.SelectedValue, _
                                                       ddlListaObjetivos.SelectedValue)
                    Else
                        CargarListaFormulasRegistradas(Me.txtBuscar.Text.Trim, _
                                                       ddlPlan.SelectedValue, _
                                                       ddlAnioBus.SelectedValue, _
                                                       ddlListaObjetivos.Text)
                    End If
                End If

            Else 'Limpia la cadena inválida de la caja de texto
                txtBuscar.Text = ""
            End If
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
            Dim dts As New Data.DataTable
            Dim dts2 As New Data.DataTable
            Dim obj As New clsIndicadores

            'Response.Write(ddlPlan.SelectedValue)
            If ddlPlan.SelectedValue <> 0 Then
                dts = obj.ListaAniosObjetivosBusqueda(ddlPlan.SelectedValue)
                If dts.Rows.Count > 0 Then
                    ddlAnioBus.DataSource = dts
                    ddlAnioBus.DataTextField = "Descripcion"
                    ddlAnioBus.DataValueField = "Codigo"
                    ddlAnioBus.DataBind()
                End If

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

    Protected Sub ddlAnioBus_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlAnioBus.SelectedIndexChanged
        Try
            Dim dts As New Data.DataTable
            Dim obj As New clsIndicadores

            'Response.Write(ddlAnioBus.SelectedValue)
            If ddlPlan.SelectedValue <> 0 Then
                dts = obj.ListaObjetivosBusqueda(Me.ddlPlan.SelectedValue, Me.ddlAnioBus.SelectedValue)
                ddlListaObjetivos.DataSource = dts
                ddlListaObjetivos.DataTextField = "Descripcion"
                ddlListaObjetivos.DataValueField = "Codigo"
                ddlListaObjetivos.DataBind()
            End If
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

    Protected Sub gvPeriodoFormula_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvPeriodoFormula.SelectedIndexChanged

    End Sub
End Class
