
Partial Class Indicadores_Formularios_frmImportarDatosVariable
    Inherits System.Web.UI.Page
    Dim usuario As Integer

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If (IsPostBack = False) Then
                CargarComboCategoria()
                CargarComboPeriodicidad()

                'CP -> Filtra la lista de variables por categoria y periodicidad
                If (ddlCategoria.SelectedValue <> 0) And (ddlPeriodicidad.SelectedValue <> 0) Then
                    ListaVarCategoria(ddlCategoria.SelectedValue, ddlPeriodicidad.SelectedValue, "CP")
                End If
            End If

            'Debe tomar del inicio de sesión
            usuario = Request.QueryString("id")

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub CargarComboCategoria()
        Try
            Dim obj As New clsIndicadores
            Dim dts As New Data.DataTable

            dts = obj.ListarCategoriasVariables()
            If dts.Rows.Count > 0 Then
                ddlCategoria.DataSource = dts
                ddlCategoria.DataTextField = "Descripcion"
                ddlCategoria.DataValueField = "Codigo"
                ddlCategoria.DataBind()
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub ConsultarGridRegistros(ByVal vEstado As Boolean, ByVal vTipo As String, ByVal vCodigo_peri As Integer)
        Try
            Dim dts As New Data.DataTable
            Dim obj As New clsIndicadores

            'Se envía valor 0 para que liste todos los registros
            If vEstado = True Then
                dts = obj.ConsultarVariableImportar(vTipo, vCodigo_peri)
                'Response.Write(dts.Rows.Count)
                If dts.Rows.Count > 0 Then
                    gvVariable.DataSource = dts
                    gvVariable.DataBind()
                End If
            Else
                dts = obj.ConsultarVariableImportar(vTipo, vCodigo_peri)
                If dts.Rows.Count > 0 Then
                    gvVariable.DataSource = dts
                    gvVariable.DataBind()
                End If
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub CargarComboPeriodicidad()
        Try
            Dim obj As New clsIndicadores
            Dim dts As New Data.DataTable
            dts = obj.ConsultarPeriodicidadImportar
            If dts.Rows.Count > 0 Then
                ddlPeriodicidad.DataSource = dts
                ddlPeriodicidad.DataTextField = "Descripcion"
                ddlPeriodicidad.DataValueField = "Codigo"
                ddlPeriodicidad.DataBind()
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub CargarComboSemestre(ByVal vPeriodicidad As Integer)
        Try
            Dim obj As New clsIndicadores
            Dim dts As New Data.DataTable

            dts = obj.ConsultarSemestre(vPeriodicidad)

            ddlSemestre.DataSource = dts
            ddlSemestre.DataTextField = "Descripcion"
            ddlSemestre.DataValueField = "Codigo"
            ddlSemestre.DataBind()

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub


    Protected Sub cmdGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdGuardar.Click
        Try


            If (validaCheckActivo() = True) Then
                lblMensaje.Visible = False
                lblMensaje.Text = ""
                'Carga todas las variables segun filtro
                'ConsultarGridRegistros(False, "UN", ddlPeriodicidad.SelectedValue)

                lblMensaje.Visible = True
                lblMensaje.ForeColor = Drawing.Color.Black
                lblMensaje.Text = "Importacion Finalizada."
                Me.Image1.Attributes.Add("src", "../Images/accept.png")
                Me.avisos.Attributes.Add("class", "mensajeExito")

                'Response.Redirect("frmVerDatosImportados.aspx")

            Else
                lblMensaje.Visible = True
                lblMensaje.ForeColor = Drawing.Color.Black
                lblMensaje.Text = "(*) Debe seleccionar una variable como mínimo"
                Me.Image1.Attributes.Add("src", "../Images/exclamation.png")
                Me.avisos.Attributes.Add("class", "mensajeError")
            End If
            'LimpiarControles()

            'CP -> Filtra la lista de variables por categoria y periodicidad
            If ddlCategoria.SelectedValue <> 0 And ddlPeriodicidad.SelectedValue <> 0 Then
                ListaVarCategoria(ddlCategoria.SelectedValue, ddlPeriodicidad.SelectedValue, "CP")
            End If

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Function validaCheckActivo() As Boolean
        Dim obj As New clsIndicadores
        Dim dts As New Data.DataTable
        Dim dtsV As New Data.DataTable
        Dim dtsPeriodo As New Data.DataTable


        Dim Fila As GridViewRow
        Dim sw As Byte = 0
        Dim vCodigo_var As String

        For i As Integer = 0 To Me.gvVariable.Rows.Count - 1
            'Capturamos las filas que estan activas
            Fila = Me.gvVariable.Rows(i)
            Dim valor As Boolean = CType(Fila.FindControl("chkElegir"), CheckBox).Checked
            If (valor = True) Then
                sw = 1
                'Con este codigo recuperamos los datos de una determinada celda de la fila
                'Me.lblDestinatario.Text = Me.gvVariable.Rows(i).Cells(1).Text

                'Seleccionaloes el codigo de la variable, para poder generar los valores 
                vCodigo_var = Convert.ToString(gvVariable.DataKeys(Fila.RowIndex).Value)

                '---------------------------------------------------------------------------------------------------------------------
                '####################### PARA UN SOLO PERIODO  #######################################################################
                '---------------------------------------------------------------------------------------------------------------------
                'Validamos si ya esta importado este periodo
                If ddlSemestre.SelectedValue <> "PP" And ddlSemestre.SelectedValue <> "P0" Then
                    dtsV = obj.ValidarImportacionPorPeriodo(vCodigo_var, ddlSemestre.SelectedValue)
                    If dtsV.Rows.Count = 0 Then
                        dts = obj.RecuperarScriptsVariable(vCodigo_var)
                        If dts.Rows.Count > 0 Then
                            For v As Integer = 0 To dts.Rows.Count - 1
                                obj.ImportarDataVariable(dts.Rows(v).Item("Script").ToString, vCodigo_var, ddlSemestre.SelectedValue)
                            Next
                        End If
                    End If
                End If
                '---------------------------------------------------------------------------------------------------------------------

                obj.NotificacionVariable(vCodigo_var, ddlSemestre.SelectedValue)

                '---------------------------------------------------------------------------------------------------------------------
                '####################### PARA TODOS LOS PERIODOS  ###################################################################
                '---------------------------------------------------------------------------------------------------------------------
                If ddlSemestre.SelectedValue = "PP" And ddlSemestre.SelectedValue <> "P0" Then
                    dtsPeriodo = obj.ListaCodigoPeriodo()
                    If dtsPeriodo.Rows.Count > 0 Then
                        For j As Integer = 0 To dtsPeriodo.Rows.Count - 1
                            dtsV = obj.ValidarImportacionPorPeriodo(vCodigo_var, dtsPeriodo.Rows(j).Item("codigo_pdo").ToString)
                            If dtsV.Rows.Count = 0 Then
                                dts = obj.RecuperarScriptsVariable(vCodigo_var)
                                If dts.Rows.Count > 0 Then
                                    For v As Integer = 0 To dts.Rows.Count - 1
                                        obj.ImportarDataVariable(dts.Rows(v).Item("Script").ToString, vCodigo_var, dtsPeriodo.Rows(j).Item("codigo_pdo").ToString)
                                    Next
                                End If
                            End If
                        Next
                    End If

                End If
                '-------------------------------------------------------------------------------------------------------------------------------------------------------

            End If
        Next

        If (sw = 1) Then
            lblMensaje.Text = "Proceso terminado satisfactoriamnete..."
            Return True
        End If

        Return False
    End Function

    Protected Sub gvVariable_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvVariable.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim fila As Data.DataRowView
                fila = e.Row.DataItem
                'e.Row.Attributes.Add("OnMouseOver", "Resaltar(1,this,'S')")
                'e.Row.Attributes.Add("OnMouseOut", "Resaltar(0,this,'S')")

                Dim Codigo_var As String = e.Row.Cells(1).Text.Trim.ToString
                e.Row.Cells(7).Text = "<a href='frmVerDatosImportados.aspx?codigo_var=" & Codigo_var & "&KeepThis=true&TB_iframe=true&height=600&width=850&modal=true' title='Ver Resultados' class='thickbox'> <img src='../Iconos/resultados.gif' border=0 /><a/>"
                e.Row.Cells(8).Text = "<a href='frmPeriodosImportados.aspx?codigo_var=" & Codigo_var & "&KeepThis=true&TB_iframe=true&height=600&width=850&modal=true' title='Ver Resultados' class='thickbox'> <img src='../Iconos/resultados.gif' border=0 /><a/>"
            End If

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub ddlPeriodicidad_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPeriodicidad.SelectedIndexChanged
        Try
            Dim obj As New clsIndicadores
            Dim dts As New Data.DataTable


            'Para cargar el cbo semestre
            If ddlPeriodicidad.SelectedValue <> 0 Then
                dts = obj.ListarPeriodoSegunPeriodicidad(ddlPeriodicidad.SelectedValue)
                If dts.Rows.Count > 0 Then
                    ddlSemestre.DataSource = dts
                    ddlSemestre.DataTextField = "Descripcion"
                    ddlSemestre.DataValueField = "Codigo"
                    ddlSemestre.DataBind()
                Else
                    ddlSemestre.DataSource = Nothing
                    ddlSemestre.DataBind()
                End If
            End If

            'CP -> Filtra la lista de variables por categoria y periodicidad
            If (ddlCategoria.SelectedValue <> 0) And (ddlPeriodicidad.SelectedValue <> 0) Then
                ListaVarCategoria(ddlCategoria.SelectedValue, ddlPeriodicidad.SelectedValue, "CP")
            End If

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub


    Private Sub ListaVarCategoria(ByVal codigo_cat As Integer, ByVal codigo_peri As Integer, ByVal vTipo As String)
        Try
            Dim obj As New clsIndicadores
            Dim dts As New Data.DataTable

            dts = obj.ListaVariablesPorCategoria(codigo_cat, codigo_peri, vTipo)

            If dts.Rows.Count > 0 Then
                gvVariable.Visible = True
                gvVariable.DataSource = dts
                gvVariable.DataBind()
            Else
                gvVariable.Visible = False
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub ddlCategoria_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlCategoria.SelectedIndexChanged
        Try
            'SC -> Filtra la lista de variables solo por categoria
            If ddlCategoria.SelectedValue <> 0 Then
                ListaVarCategoria(ddlCategoria.SelectedValue, 0, "SC")
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub LimpiarControles()
        Try
            ddlCategoria.SelectedValue = 0
            ddlPeriodicidad.SelectedValue = 0
            ddlSemestre.SelectedValue = "P0"
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub cmdValores_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdValores.Click
        Try
            Response.Redirect("frmValoresVariables.aspx")
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub cmdCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdCancelar.Click
        Try
            ddlCategoria.SelectedValue = 0
            'ddlSemestre.SelectedValue = "PP"
            ddlPeriodicidad.SelectedValue = 0
            lblMensaje.Text = ""
            Me.Image1.Attributes.Add("src", "../Images/beforelastchild.GIF")
            Me.avisos.Attributes.Add("class", "none")

            gvVariable.DataSource = Nothing
            gvVariable.DataBind()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub


   
    Protected Sub Save_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdGuardar.Click
        Try
            If (validaCheckActivo() = True) Then
                lblMensaje.Visible = False
                lblMensaje.Text = ""
                'Carga todas las variables segun filtro
                'ConsultarGridRegistros(False, "UN", ddlPeriodicidad.SelectedValue)

                lblMensaje.Visible = True
                lblMensaje.ForeColor = Drawing.Color.Black
                lblMensaje.Text = "Importacion Finalizada."
                Me.Image1.Attributes.Add("src", "../Images/accept.png")
                Me.avisos.Attributes.Add("class", "mensajeExito")

                'Response.Redirect("frmVerDatosImportados.aspx")

            Else
                lblMensaje.Visible = True
                lblMensaje.ForeColor = Drawing.Color.Black
                lblMensaje.Text = "(*) Debe seleccionar una variable como mínimo"
                Me.Image1.Attributes.Add("src", "../Images/exclamation.png")
                Me.avisos.Attributes.Add("class", "mensajeError")
            End If
            'LimpiarControles()

            'CP -> Filtra la lista de variables por categoria y periodicidad
            If ddlCategoria.SelectedValue <> 0 And ddlPeriodicidad.SelectedValue <> 0 Then
                ListaVarCategoria(ddlCategoria.SelectedValue, ddlPeriodicidad.SelectedValue, "CP")
            End If

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub
End Class
