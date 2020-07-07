
Partial Class indicadores_frmMantenimientoNivelesEstructura
    Inherits System.Web.UI.Page

    Dim usuario As Integer
    Dim validacustom As Boolean = True
    'Dim validacustom2 As Boolean = True

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                'ConsultarGridRegistros()
                'CargarComboPeriodicidad()
                'CargarComboCategoria()
                CargarNivelesDimension(0)
                'Debe tomar del inicio de sesión
                usuario = 684
            End If

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    'Private Sub ConsultarGridRegistros()
    '    Try
    '        Dim dts As New Data.DataTable
    '        Dim obj As New clsIndicadores

    '        'Se envía valor 0 para que liste todos los registros
    '        dts = obj.ConsultarVariable("0")
    '        gvVariable.DataSource = dts
    '        gvVariable.DataBind()
    '    Catch ex As Exception
    '        Response.Write(ex.Message)
    '    End Try
    'End Sub


    'Private Sub CargarComboCategoria()
    '    Try
    '        Dim obj As New clsIndicadores
    '        Dim dts As New Data.DataTable
    '        dts = obj.ConsultarCategoria(0)
    '        ddlCategorias.DataSource = dts
    '        ddlCategorias.DataTextField = "Descripcion"
    '        ddlCategorias.DataValueField = "Codigo"
    '        ddlCategorias.DataBind()

    '    Catch ex As Exception
    '        Response.Write(ex.Message)
    '    End Try
    'End Sub


    Private Sub CargarNivelesDimension(ByVal codigo_naux As Integer)
        Try

            Dim obj As New clsIndicadores
            Dim dts As New Data.DataTable
            dts = obj.ConsultarNivelesDimension(0)
            gvNivelesDim.DataSource = dts
            gvNivelesDim.DataBind()

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    '********** Se está limitando configurar a la variable con 5 niveles de dimension *************
    'El proced. soporta comparar hasta con 5 codigos_dim
    'Se corre el riesgo de que los Niveles de Dimension lleguen a ser mas de 5. 
    'Hasta la fecha solo hay 2: Escuela y Departamento.
    'Private Sub CargarNivelesSubdimension(ByVal codigo_dim1 As Integer, ByVal codigo_dim2 As Integer, ByVal codigo_dim3 As Integer, ByVal codigo_dim4 As Integer, ByVal codigo_dim5 As Integer)
    '    Try

    '        Dim obj As New clsIndicadores
    '        Dim dts As New Data.DataTable
    '        dts = obj.ConsultarNivelesSubdimension(codigo_dim1, codigo_dim2, codigo_dim3, codigo_dim4, codigo_dim5)
    '        gvNivelesSub.DataSource = dts
    '        gvNivelesSub.DataBind()

    '        'chNivelesSubdimension.DataSource = dts
    '        'chNivelesSubdimension.DataTextField = "Descripcion"
    '        'chNivelesSubdimension.DataValueField = "Codigo"
    '        'chNivelesSubdimension.DataBind()

    '    Catch ex As Exception
    '        Response.Write(ex.Message)
    '    End Try
    'End Sub
    Protected Sub cmdGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdGuardar.Click

        Try
            Dim dts As New Data.DataTable
            Dim obj As New clsIndicadores            
            Dim bandera As Integer
            Dim codigo_nid As Integer
            Dim codigo_csub As Integer
            Dim codigo_naux As Integer

            bandera = 0

            lblMensaje.Visible = True


            '****************** VALIDAR ******************************************************
            '*** 1. Validar que por lo menos haya marcado algún nivel

            'Valido que haya marcado Niveles Dimension
            For Each row As GridViewRow In gvNivelesDim.Rows
                Dim check As CheckBox = TryCast(row.FindControl("chkSeleccion2"), CheckBox)

                If check.Checked Then
                    bandera = 1
                End If
            Next        
            
            'bandera = 0
            'Valido que haya marcado Niveles Subdimension
            'For Each row As GridViewRow In gvNivelesSub.Rows
            '    Dim check As CheckBox = TryCast(row.FindControl("chkSeleccion"), CheckBox)

            '    If check.Checked Then
            '        bandera = 1
            '    End If
            'Next

            If bandera = 0 Then                
                lblMensaje.Text = "Debe seleccionar los Niveles de Dimensión para la Subvariable."

                'Para avisos
                Me.Image1.Attributes.Add("src", "../Images/exclamation.png")
                Me.avisos.Attributes.Add("class", "mensajeError")

                Exit Sub
            End If
           
            '** GUARDAR EN BASE *******************************************************************
            bandera = 0
            If validacustom Then 'Si no ha ingresado cadenas inválidas (select, php, script)                

                'Nuevo registro
                If lblCodigo.Text = "" Then

                    'Retorna el codigo de la variable 
                    codigo_naux = obj.InsertarNivelAux(txtDescripcionAux.Text, txtAbreviaturaAux.Text, usuario)

                    If codigo_naux <> 0 Then
                        lblMensaje.Text = "Datos registrados correctamente."

                        'Para avisos
                        Me.Image1.Attributes.Add("src", "../Images/accept.png")
                        Me.avisos.Attributes.Add("class", "mensajeExito")

                        'Insertar Configuracion Dimensiones
                        For Each row As GridViewRow In gvNivelesDim.Rows
                            Dim check As CheckBox = TryCast(row.FindControl("chkSeleccion2"), CheckBox)

                            If check.Checked Then
                                codigo_nid = Convert.ToInt32(gvNivelesDim.DataKeys(row.RowIndex).Value)
                                bandera = obj.InsertarNivelesDim_para_NivelAux(codigo_naux, codigo_nid)

                                'If bandera = 0 Then
                                '    lblMensaje.Text = lblMensaje.Text & " La configuración ya estaba registrada."
                                'End If

                            End If
                        Next

                        'Insertar Configuracion Subdimensiones
                        'For Each row As GridViewRow In gvNivelesSub.Rows
                        '    Dim check As CheckBox = TryCast(row.FindControl("chkSeleccion"), CheckBox)

                        '    If check.Checked Then
                        '        codigo_csub = Convert.ToInt32(gvNivelesSub.DataKeys(row.RowIndex).Value)
                        '        bandera = obj.InsertarConfiguracionSubdimensiones(codigo_var, codigo_csub, gvNivelesSub.DataKeys(row.RowIndex).Values("Codigonid"), usuario)

                        '        If bandera = 0 Then
                        '            lblMensaje.Text = lblMensaje.Text & " La configuración ya estaba registrada para esa Variable."
                        '        End If

                        '    End If
                        'Next

                        'lblMensaje.ForeColor = Drawing.Color.Green

                    Else
                        lblMensaje.Text = "El registro ya existe. No se insertaron los datos."

                        'Para avisos
                        Me.Image1.Attributes.Add("src", "../Images/exclamation.png")
                        Me.avisos.Attributes.Add("class", "mensajeError")
                    End If

                    '***********************************************************************************
                    '***********************  Edicion de registro **************************************
                    '***********************************************************************************
                    'Else
                    '    Dim nsub As Integer = 0
                    '    Dim ndim As Integer = 0
                    '    Dim naux As Integer = 0
                    '    Dim dtsresultado As New Data.DataTable

                    '    lblMensaje.Text = obj.ModificarVariable(lblCodigo.Text, ddlCategorias.SelectedValue, ddlPeriodicidad.SelectedValue, txtDescripcion.Text.ToUpper, usuario)

                    '    If lblMensaje.Text = "Datos modificados correctamente." Then
                    '        'Para avisos
                    '        Me.Image1.Attributes.Add("src", "../Images/accept.png")
                    '        Me.avisos.Attributes.Add("class", "mensajeExito")
                    '        lblMensaje.ForeColor = Drawing.Color.Black
                    '    Else
                    '        'Para avisos
                    '        Me.Image1.Attributes.Add("src", "../Images/exclamation.png")
                    '        Me.avisos.Attributes.Add("class", "mensajeError")
                    '        lblMensaje.ForeColor = Drawing.Color.Black
                    '    End If

                    '    'Modificar Configuracion Subvariables
                    '    'Recorro Subvariables del checkboxlist
                    '    For Each row As GridViewRow In gvNivelesAux.Rows
                    '        Dim check As CheckBox = TryCast(row.FindControl("chkSeleccion"), CheckBox)
                    '        codigo_naux = Convert.ToInt32(gvNivelesAux.DataKeys(row.RowIndex).Value)

                    '        'Si está seleccionada, la guardo en la base
                    '        If check.Checked Then
                    '            bandera = obj.InsertarConfiguracionSubvariables(lblCodigo.Text, codigo_naux, usuario)
                    '        End If

                    '        'Si no está seleccionado: 
                    '        'Se elimina la configuracion (cambia estado a 1) de la subvariable, y config. de dim y subdim relacionadas, asi como las subvariables, dimensiones y subdimensiones creadas con esos niveles configurados.
                    '        If check.Checked = False Then
                    '            dtsresultado = obj.EliminarSubvariableConfigurada(lblCodigo.Text, codigo_naux, usuario, gvNivelesAux.DataKeys(row.RowIndex).Values("Abreviatura"))
                    '            naux = naux + dtsresultado.Rows(0).Item("NumeroAux")
                    '            ndim = ndim + dtsresultado.Rows(0).Item("NumeroDim")
                    '            nsub = nsub + dtsresultado.Rows(0).Item("NumeroSub")
                    '        End If
                    '    Next


                    '    'Modificar Configuracion Dimensiones
                    '    'Recorro Dimensiones del checkboxlist
                    '    For Each row As GridViewRow In gvNivelesDim.Rows
                    '        Dim check As CheckBox = TryCast(row.FindControl("chkSeleccion2"), CheckBox)

                    '        codigo_nid = Convert.ToInt32(gvNivelesDim.DataKeys(row.RowIndex).Value)
                    '        codigo_naux = Convert.ToInt32(gvNivelesDim.DataKeys(row.RowIndex).Values("codigo_naux"))

                    '        'Si está seleccionada, la guardo en la base
                    '        If check.Checked Then
                    '            bandera = obj.InsertarConfiguracionDimensiones(lblCodigo.Text, codigo_nid, codigo_naux, usuario)
                    '        End If

                    '        'Si no está seleccionado, se comprueba si ha estado configurada. Se elimina la configuracion, si no se esta utilizando
                    '        If check.Checked = False Then
                    '            dtsresultado = obj.EliminarDimensionConfigurada(lblCodigo.Text, codigo_nid, usuario, gvNivelesDim.DataKeys(row.RowIndex).Values("Abreviatura"))
                    '            ndim = ndim + dtsresultado.Rows(0).Item("NumeroDim")
                    '            nsub = nsub + dtsresultado.Rows(0).Item("NumeroSub")
                    '        End If
                    '    Next

                    '    If ndim > 0 Or nsub > 0 Then
                    '        lblMensaje.Visible = True
                    '        lblMensaje.Text = lblMensaje.Text & " Se eliminaron " & ndim & " dimensiones y " & nsub & " subdimensiones."

                    '    End If

                    '    nsub = 0

                    '    'Modificar Configuracion Subdimensiones
                    '    'Recorro subdimensiones del checkboxlist

                    '    '###1- Elimino todas las subdimensiones configuradas
                    '    nsub = nsub + obj.EliminarTodasSubdimensionesConfiguradas(lblCodigo.Text, usuario)

                    '    '###2- Inserto las nuevas subdimensiones
                    '    For Each row As GridViewRow In gvNivelesSub.Rows
                    '        Dim check As CheckBox = TryCast(row.FindControl("chkSeleccion"), CheckBox)
                    '        codigo_csub = Convert.ToInt32(gvNivelesSub.DataKeys(row.RowIndex).Value)
                    '        codigo_nid = Convert.ToInt32(gvNivelesSub.DataKeys(row.RowIndex).Values("Codigonid"))

                    '        'Si está seleccionada, la guardo en la base
                    '        If check.Checked Then
                    '            bandera = obj.InsertarConfiguracionSubdimensiones(lblCodigo.Text, codigo_csub, codigo_nid, usuario)
                    '        End If

                    '        ''Si no está seleccionado, se comprueba si ha estado configurada. Se elimina la configuracion, si no se esta utilizando
                    '        'If check.Checked = False Then
                    '        '    nsub = nsub + obj.EliminarSubdimensionConfigurada(lblCodigo.Text, codigo_csub, usuario)
                    '        'End If
                    '    Next

                    '    'If nsub > 0 Then
                    '    '    lblMensaje.Text = lblMensaje.Text & " Se eliminaron " & nsub & " subdimensiones."
                    '    'End If

                    '    '**********************************************************************************
                    '    'ESTRUCTURA AL INSERTAR:
                    '    'ESTRUCTURA: Bloque que verifica si va a generar la estructura para la variable 
                    '    '----------------------------------------------------------------------------------
                    '    If chkEstructura.Checked = True Then
                    '        If lblCodigo.Text <> "0" Then
                    '            Dim dtsEs As New Data.DataTable
                    '            Dim vEstructura As Integer = 1
                    '            dtsEs = obj.GeneraEstructuraVariable(lblCodigo.Text, vEstructura)
                    '        End If
                    '    Else
                    '        Dim vEstructura As Integer = 0
                    '        Dim dtsEs As New Data.DataTable
                    '        dtsEs = obj.GeneraEstructuraVariable(lblCodigo.Text, vEstructura)
                    '    End If

                    '    'Cambio nombre del boton Modificar por Guardar
                    '    cmdGuardar.Text = "   Guardar"

                End If

                'Refrescar listado
                'ConsultarGridRegistros()
                limpiarcajas()

            Else 'Limpia la cadena inválida de la caja de texto
                txtDescripcionAux.Text = ""
            End If

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    'Protected Sub gvVariable_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvVariable.RowDataBound
    '    If e.Row.RowType = DataControlRowType.DataRow Then
    '        e.Row.Cells(0).Text = e.Row.RowIndex + 1
    '    End If
    'End Sub

    'Protected Sub gvCategoria_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles gvVariable.RowDeleting
    '    Try
    '        Dim obj As New clsIndicadores

    '        'lblMensaje.Text = obj.EliminarCategoria(gvCategoria.Rows(e.RowIndex).Cells(0).Text)

    '        'La columna Codigo esta oculta. Por eso uso el DataKey de la fila seleccionada
    '        lblMensaje.Text = obj.EliminarVariable(gvVariable.DataKeys(e.RowIndex).Value.ToString())
    '        lblMensaje.Visible = True

    '        If lblMensaje.Text = "Registro eliminado con éxito." Then

    '            'Para avisos
    '            Me.Image1.Attributes.Add("src", "../Images/accept.png")
    '            Me.avisos.Attributes.Add("class", "mensajeExito")

    '        Else

    '            'Para avisos
    '            Me.Image1.Attributes.Add("src", "../Images/exclamation.png")
    '            Me.avisos.Attributes.Add("class", "mensajeError")

    '        End If

    '        'Refrescar campos
    '        limpiarcajas()
    '    Catch ex As Exception
    '        Response.Write(ex.Message)
    '    End Try
    '    ConsultarGridRegistros()
    'End Sub

    'Protected Sub gvCategoria_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvVariable.RowCommand
    '    Try
    '        If (e.CommandName.Equals("Select")) Then 'comprueba que sea el boton de seleccion                

    '            Dim seleccion As GridViewRow
    '            Dim codigovar_seleccion As String

    '            'Borro checkboxlist de Niveles Dimension y subdimension
    '            BorrarCheckBoxListDim()
    '            BorrarCheckBoxListSub()

    '            '1. Obtengo la linea del gridview que fue cliqueada
    '            seleccion = DirectCast(e.CommandSource, GridView).Rows(e.CommandArgument)
    '            codigovar_seleccion = gvVariable.DataKeys(seleccion.RowIndex).Values("Codigo")

    '            '2. Grabo en lbl oculto, para luego poder modificar                
    '            lblCodigo.Text = codigovar_seleccion

    '            txtDescripcion.Text = HttpUtility.HtmlDecode(gvVariable.Rows(seleccion.RowIndex).Cells.Item(3).Text)
    '            ddlCategorias.SelectedValue = Convert.ToInt32(gvVariable.DataKeys(seleccion.RowIndex).Values("CodigoCat"))
    '            ddlPeriodicidad.SelectedValue = Convert.ToInt32(gvVariable.DataKeys(seleccion.RowIndex).Values("CodigoPeri"))

    '            '3. Obtengo subvariables configuradas y lleno checkboxlist                
    '            Dim obj As New clsIndicadores
    '            Dim dts As New Data.DataTable
    '            Dim contador As Integer
    '            Dim bandera As Integer = 0

    '            dts = obj.ConsultarSubvariablesConfiguradas(gvVariable.DataKeys(seleccion.RowIndex).Values("Codigo"))
    '            LimpiarCheckBoxListAux()
    '            lblMensaje.Text = ""

    '            'Si hay subvariables configuradas
    '            If dts.Rows.Count > 0 Then
    '                'Recorro filas del listado de subvariables configuradas
    '                For i As Integer = 0 To dts.Rows.Count - 1
    '                    'Response.Write(dts.Rows(i).Item(0))

    '                    'Recorro dimensiones del checkboxlist
    '                    For Each row As GridViewRow In gvNivelesAux.Rows
    '                        Dim check As CheckBox = TryCast(row.FindControl("chkSeleccion"), CheckBox)
    '                        'Response.Write(gvNivelesSub.DataKeys(row.RowIndex).Value())

    '                        'Coincidencia de codigo_naux de la Configuracion, y codigo_naux del checkboxlist
    '                        'Es el nivel configurado
    '                        If gvNivelesAux.DataKeys(row.RowIndex).Value() = dts.Rows(i).Item(0) Then
    '                            check.Checked = True
    '                            'Si la subvariable esta siendo utilizada, se bloquea para impedir que se modifique
    '                            'Response.Write(codigovar_seleccion)
    '                            'Response.Write(dts.Rows(i).Item(2))
    '                            contador = obj.ConsultarNivelesAuxiliarUtilizados(codigovar_seleccion, dts.Rows(i).Item(2))

    '                            'Cargo niveles dimension
    '                            CargarNivelesDimension(dts.Rows(i).Item(0))

    '                            'Solo se bloquea si ha sido utilizado
    '                            If contador > 0 Then
    '                                check.Enabled = False
    '                                bandera = 1
    '                            End If
    '                        Else
    '                            'No es el nivel configurado
    '                            '### Se bloquea, porque hay otro nivel configurado                              
    '                            check.Enabled = False
    '                        End If
    '                    Next
    '                Next

    '                If bandera = 1 Then
    '                    lblMensaje.Text = "Las Subvariables bloqueadas indican que están siendo utilizadas. Por lo tanto, no se pueden modificar."
    '                    lblMensaje.Visible = True
    '                    lblMensaje.ForeColor = Drawing.Color.Orange

    '                End If
    '                bandera = 0
    '                contador = 0
    '            End If

    '            '4. Obtengo dimensiones configuradas y lleno checkboxlist

    '            '#### si selecciona tb otra dimension, se deben mantener bloqueadas las SUBDIMENSIONES que lo estaban
    '            '********** Se está limitando configurar a la variable con 5 niveles de dimension *************            
    '            Dim nivelesdim(4) As Integer
    '            dts = obj.ConsultarDimensionesConfiguradas(gvVariable.DataKeys(seleccion.RowIndex).Values("Codigo"))
    '            LimpiarCheckBoxListDim()
    '            lblMensaje.Text = ""

    '            'Si hay dimensiones configuradas
    '            If dts.Rows.Count > 0 Then
    '                'Cargar Niveles Dimension en 0
    '                Dim x As Integer
    '                For Each x In nivelesdim
    '                    nivelesdim(x) = 0
    '                Next

    '                'Recorro filas del listado de Dimensiones configuradas
    '                For i As Integer = 0 To dts.Rows.Count - 1
    '                    'Response.Write(dts.Rows(i).Item(0))
    '                    nivelesdim(i) = dts.Rows(i).Item(0)
    '                    'Recorro dimensiones del checkboxlist
    '                    For Each row As GridViewRow In gvNivelesDim.Rows
    '                        Dim check As CheckBox = TryCast(row.FindControl("chkSeleccion2"), CheckBox)
    '                        'Response.Write(gvNivelesSub.DataKeys(row.RowIndex).Value())

    '                        'Coincidencia de codigo_nid de la Configuracion, y codigo_nid del checkboxlist
    '                        'Es el Nivel  de Dimension configurado
    '                        If gvNivelesDim.DataKeys(row.RowIndex).Value() = dts.Rows(i).Item(0) Then
    '                            check.Checked = True
    '                            'Si la dimension esta siendo utilizada, se bloquea para impedir que se modifique
    '                            'Response.Write(codigovar_seleccion)
    '                            'Response.Write(dts.Rows(i).Item(2))
    '                            contador = obj.ConsultarNivelesDimensionUtilizados(codigovar_seleccion, dts.Rows(i).Item(2))

    '                            If contador > 0 Then
    '                                check.Enabled = False
    '                                bandera = 1
    '                            End If
    '                        End If
    '                    Next
    '                Next

    '                '********** Se está limitando configurar a la variable con 5 niveles de dimension *************
    '                'Cargo niveles subdimension
    '                CargarNivelesSubdimension(nivelesdim(0), nivelesdim(1), nivelesdim(2), nivelesdim(3), nivelesdim(4))

    '                If bandera = 1 Then
    '                    lblMensaje.Text = "Las Dimensiones bloqueadas indican que están siendo utilizadas. Por lo tanto, no se pueden modificar."
    '                    lblMensaje.Visible = True
    '                    lblMensaje.ForeColor = Drawing.Color.Orange

    '                End If
    '                bandera = 0
    '                contador = 0
    '            End If

    '            '5. Obtengo subdimensiones configuradas y lleno checkbox.              
    '            'Listo las subdimensiones configuradas de la variable seleccionada
    '            dts = obj.ConsultarSubdimensionesConfiguradas(codigovar_seleccion)
    '            LimpiarCheckBoxListSub()

    '            'Eliminar toda la configuracion de subdimensiones
    '            'Insertar nuevo

    '            'Recorro filas del listado de subdimensiones configuradas
    '            For i As Integer = 0 To dts.Rows.Count - 1
    '                'Response.Write(dts.Rows(i).Item(0))

    '                'Recorro subdimensiones del checkboxlist
    '                For Each row As GridViewRow In gvNivelesSub.Rows
    '                    Dim check As CheckBox = TryCast(row.FindControl("chkSeleccion"), CheckBox)
    '                    'Response.Write(gvNivelesSub.DataKeys(row.RowIndex).Value())
    '                    'Response.Write("::")
    '                    'Response.Write(dts.Rows(i).Item(0))
    '                    'Response.Write("--")

    '                    'Coincidencia de codigo_csub de la Configuracion, y codigo_csub del checkboxlist
    '                    'y codigo_nid de la Configuracion, y codigo_nid del checkboxlist
    '                    If gvNivelesSub.DataKeys(row.RowIndex).Value() = dts.Rows(i).Item(0) _
    '                    And gvNivelesSub.DataKeys(row.RowIndex).Values("Codigonid") = dts.Rows(i).Item(3) Then
    '                        check.Checked = True
    '                        'Si la subdimension esta siendo utilizada, se bloquea para impedir que se modifique
    '                        'Response.Write(codigovar_seleccion)
    '                        'Response.Write("--")
    '                        'Response.Write(dts.Rows(i).Item(2))
    '                        contador = obj.ConsultarNivelesSubdimensionUtilizados(codigovar_seleccion, dts.Rows(i).Item(2), dts.Rows(i).Item(3))
    '                        'Response.Write(contador)
    '                        If contador > 0 Then
    '                            check.Enabled = False
    '                            bandera = 1
    '                        End If
    '                    End If
    '                Next
    '            Next

    '            If bandera = 1 Then
    '                lblMensaje.Text = "Las subdimensiones bloqueadas indican que están siendo utilizadas. Por lo tanto, no se pueden modificar."
    '                lblMensaje.Visible = True
    '                lblMensaje.ForeColor = Drawing.Color.Orange
    '            End If

    '            'Cambio nombre del boton Guardar por Modificar
    '            cmdGuardar.Text = "   Modificar"
    '        End If
    '    Catch ex As Exception
    '        Response.Write(ex.Message)
    '    End Try
    'End Sub

    Private Sub limpiarcajas()
        lblCodigo.Text = ""
        txtDescripcionAux.Text = ""
        txtAbreviaturaAux.Text = ""
        'LimpiarCheckBoxListSub()
        LimpiarCheckBoxListDim()        
        'BorrarCheckBoxListSub()

        lblND.Text = ""
        'lblValidacionCheckboxlist.Text = ""

        'Cambio nombre del boton Guardar por Modificar
        cmdGuardar.Text = "   Guardar"
    End Sub

    Private Sub LimpiarCampos()

        limpiarcajas()
        lblMensaje.Text = ""
        Me.Image1.Attributes.Add("src", "../Images/beforelastchild.GIF")
        Me.avisos.Attributes.Add("class", "none")
    End Sub


    'Private Sub LimpiarCheckBoxListSub()
    '    'chNivelesSubdimension.ClearSelection()
    '    For Each row As GridViewRow In gvNivelesSub.Rows
    '        Dim check As CheckBox = TryCast(row.FindControl("chkSeleccion"), CheckBox)
    '        check.Checked = False
    '        check.Enabled = True
    '    Next
    'End Sub

    Private Sub LimpiarCheckBoxListDim()
        'chNivelesSubdimension.ClearSelection()
        For Each row As GridViewRow In gvNivelesDim.Rows
            Dim check As CheckBox = TryCast(row.FindControl("chkSeleccion2"), CheckBox)
            check.Checked = False
            check.Enabled = True
        Next
    End Sub

    Protected Sub cmdCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdCancelar.Click
        LimpiarCampos()
        lblMensaje.Text = ""
    End Sub


    'Protected Sub btnBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBuscar.Click
    '    Try
    '        'Para Validar Caja de Busqueda

    '        If Page.IsValid Then        'Si no ha ingresado cadenas inválidas (select, php, script)
    '            Dim dts As New Data.DataTable
    '            Dim obj As New clsIndicadores

    '            dts = obj.ConsultarVariablePorNombre(txtBuscar.Text)
    '            gvVariable.DataSource = dts
    '            gvVariable.DataBind()
    '        Else 'Limpia la cadena inválida de la caja de texto
    '            txtBuscar.Text = ""
    '        End If

    '    Catch ex As Exception
    '        Response.Write(ex.Message)
    '    End Try
    'End Sub

    Protected Sub CustomValidator1_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles CustomValidator1.ServerValidate
        Try
            Dim cadena As String

            cadena = txtDescripcionAux.Text

            If InStr(1, cadena, "select") > 0 Or InStr(1, cadena, "script") > 0 Or InStr(1, cadena, "php") > 0 Then
                args.IsValid = False
                validacustom = False
                Exit Sub
            End If


        Catch ex As Exception
            Response.Write(ex.Message)
            'args.IsValid = False
            Exit Sub
        End Try
        'args.IsValid = True
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

End Class




