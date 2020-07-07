Partial Class Indicadores_Formularios_frmMantenimientoVariable
    Inherits System.Web.UI.Page

    Dim usuario As Integer
    Dim validacustom As Boolean = True
    'Dim validacustom2 As Boolean = True

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                ConsultarGridRegistros()
                CargarComboPeriodicidad()
                CargarComboCategoria()
                CargarNivelesSubvariable()
                'Debe tomar del inicio de sesión
                usuario = Request.QueryString("id")
            End If

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub ConsultarGridRegistros()
        Try
            Dim dts As New Data.DataTable
            Dim obj As New clsIndicadores

            'Se envía valor 0 para que liste todos los registros
            dts = obj.ConsultarVariable("0")
            gvVariable.DataSource = dts
            gvVariable.DataBind()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub CargarComboPeriodicidad()
        Try
            Dim obj As New clsIndicadores
            Dim dts As New Data.DataTable
            dts = obj.ListarPeriodicidad()
            ddlPeriodicidad.DataSource = dts
            ddlPeriodicidad.DataTextField = "Descripcion"
            ddlPeriodicidad.DataValueField = "Codigo"
            ddlPeriodicidad.DataBind()

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub CargarComboCategoria()
        Try
            Dim obj As New clsIndicadores
            Dim dts As New Data.DataTable
            dts = obj.ConsultarCategoria(0)
            ddlCategorias.DataSource = dts
            ddlCategorias.DataTextField = "Descripcion"
            ddlCategorias.DataValueField = "Codigo"
            ddlCategorias.DataBind()

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub CargarNivelesSubvariable()
        Try
            Dim obj As New clsIndicadores
            Dim dts As New Data.DataTable
            dts = obj.ConsultarNivelesSubvariable()
            gvNivelesAux.DataSource = dts
            gvNivelesAux.DataBind()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub CargarNivelesDimension(ByVal codigo_naux As Integer)
        Try

            Dim obj As New clsIndicadores
            Dim dts As New Data.DataTable
            dts = obj.ConsultarNivelesDimension(codigo_naux)
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
    Private Sub CargarNivelesSubdimension(ByVal codigo_dim1 As Integer, ByVal codigo_dim2 As Integer, ByVal codigo_dim3 As Integer, ByVal codigo_dim4 As Integer, ByVal codigo_dim5 As Integer)
        Try

            Dim obj As New clsIndicadores
            Dim dts As New Data.DataTable
            dts = obj.ConsultarNivelesSubdimension(codigo_dim1, codigo_dim2, codigo_dim3, codigo_dim4, codigo_dim5)
            gvNivelesSub.DataSource = dts
            gvNivelesSub.DataBind()

            'chNivelesSubdimension.DataSource = dts
            'chNivelesSubdimension.DataTextField = "Descripcion"
            'chNivelesSubdimension.DataValueField = "Codigo"
            'chNivelesSubdimension.DataBind()

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub
    Protected Sub cmdGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdGuardar.Click

        Try
            Dim dts As New Data.DataTable
            Dim obj As New clsIndicadores
            Dim codigo_var As String
            Dim bandera As Integer
            Dim codigo_nid As Integer
            Dim codigo_csub As Integer
            Dim codigo_naux As Integer

            ' bandera = 0

            lblMensaje.Visible = True

            'lblValidarNAux.Text = ""
            'lblND.Text = ""
            'lblValidacionCheckboxlist.Text = ""

            'If chNivelesSubdimension.SelectedIndex = -1 Then
            '    lblValidacionCheckboxlist.Text = "(*) Debe seleccionar los Niveles de Subdimensión de la Variable."
            '    lblValidacionCheckboxlist.ForeColor = Drawing.Color.Red
            'End If

            '****************** VALIDAR ******************************************************
            ''*** 1. Si checó Generar Estructura, debe validar que por lo menos haya marcado algún nivel
            'If chkEstructura.Checked Then
            '    'Valido que haya marcado Niveles Subvariable
            '    For Each row As GridViewRow In gvNivelesAux.Rows
            '        Dim check As CheckBox = TryCast(row.FindControl("chkSeleccion"), CheckBox)

            '        If check.Checked Then
            '            bandera = 1
            '        End If
            '    Next

            '    'If bandera = 0 Then
            '    '    lblValidarNAux.Text = "(*) Para generar la Estructura debe marcar los Niveles de Subvariable."
            '    '    lblValidarNAux.ForeColor = Drawing.Color.Red
            '    '    Exit Sub
            '    'End If

            '    'bandera = 0
            '    'Valido que haya marcado Niveles Dimension
            '    For Each row As GridViewRow In gvNivelesDim.Rows
            '        Dim check As CheckBox = TryCast(row.FindControl("chkSeleccion2"), CheckBox)

            '        If check.Checked Then
            '            bandera = 1
            '        End If
            '    Next

            '    'If bandera = 0 Then
            '    '    lblND.Text = "(*) Para generar la Estructura debe marcar los Niveles de Dimensión."
            '    '    lblND.ForeColor = Drawing.Color.Red
            '    '    Exit Sub
            '    'End If

            '    'bandera = 0
            '    'Valido que haya marcado Niveles Subdimension
            '    For Each row As GridViewRow In gvNivelesSub.Rows
            '        Dim check As CheckBox = TryCast(row.FindControl("chkSeleccion"), CheckBox)

            '        If check.Checked Then
            '            bandera = 1
            '        End If
            '    Next

            '    If bandera = 0 Then
            '        'lblValidacionCheckboxlist.Text = "(*) Para generar la Estructura debe marcar los Niveles de Subdimensión."
            '        'lblValidacionCheckboxlist.ForeColor = Drawing.Color.Red
            '        lblMensaje.Text = "Si desea generar la estructura, debe configurar al menos el Nivel de Subvariable."

            '        'Para avisos
            '        Me.Image1.Attributes.Add("src", "../Images/exclamation.png")
            '        Me.avisos.Attributes.Add("class", "mensajeError")

            '        Exit Sub
            '    End If                        
            'End If

            '*** 2. Si checó Area Administrativa, debe validar que por lo menos haya marcado algún item

            bandera = 0

            For Each row As GridViewRow In gvNivelesAux.Rows
                Dim check As CheckBox = TryCast(row.FindControl("chkSeleccion"), CheckBox)

                If gvNivelesAux.DataKeys(row.RowIndex).Values("Abreviatura") = "AA" Then
                    If check.Checked Then
                        'Dim chklist As CheckBoxList = TryCast(gvNivelesAux.Rows(i).Cells(1).FindControl("chklstAuxItems"), CheckBoxList)
                        'Dim chklst As CheckBoxList = TryCast(gvNivelesAux.Rows(row.RowIndex).Cells(1).FindControl("chklstAuxItems"), CheckBoxList)
                        Dim chklst As CheckBoxList = TryCast(row.FindControl("chklstAuxItems"), CheckBoxList)

                        For i As Integer = 0 To chklst.Items.Count - 1
                            If chklst.Items(i).Selected Then
                                bandera = 1
                            End If
                        Next

                        If bandera = 0 Then
                            'lblValidacionCheckboxlist.Text = "(*) Para generar la Estructura debe marcar los Niveles de Subdimensión."
                            'lblValidacionCheckboxlist.ForeColor = Drawing.Color.Red
                            lblMensaje.Text = "Si desea generar Subvariables con el nivel de Area Administrativa, debe seleccionar las Áreas administrativas."

                            'Para avisos
                            Me.Image1.Attributes.Add("src", "../Images/exclamation.png")
                            Me.avisos.Attributes.Add("class", "mensajeError")

                            Exit Sub
                        End If

                    End If
                End If
            Next

            ''******** VALIDAR: SI MARCA NIVEL DE AUXILIAR (SUBVARIABLE), DEBE MARCAR TAMBIEN NIVEL SUBDIMENSION
            'bandera = 0
            'For Each fila As GridViewRow In gvNivelesAux.Rows
            '    Dim chk As CheckBox = TryCast(fila.FindControl("chkSeleccion"), CheckBox)
            '    If chk.Checked Then                    
            '        For Each row As GridViewRow In gvNivelesDim.Rows
            '            Dim check As CheckBox = TryCast(row.FindControl("chkSeleccion2"), CheckBox)
            '            If check.Checked Then
            '                bandera = 1
            '            End If
            '        Next

            '        If bandera = 0 Then
            '            lblMensaje.Text = "Debe marcar por lo menos un Nivel de Dimensión, ya que ha seleccionado un Nivel de Subvariable."
            '            lblMensaje.ForeColor = Drawing.Color.Red
            '            lblMensaje.Visible = True
            '            Exit Sub
            '        End If

            '    End If
            'Next

            ''******** VALIDAR QUE MARQUE POR LO MENOS 1 NIVEL DE SUBDIMENSION POR CADA NIVEL DE DIMENSION **
            ''Busco niveles dimension que estan checados
            'For Each row As GridViewRow In gvNivelesDim.Rows
            '    Dim check As CheckBox = TryCast(row.FindControl("chkSeleccion2"), CheckBox)
            '    codigo_nid = Convert.ToInt32(gvNivelesDim.DataKeys(row.RowIndex).Value)
            '    bandera = 0

            '    'Response.Write("Dimension:")
            '    'Response.Write(codigo_nid)
            '    'Response.Write(gvNivelesDim.DataKeys(row.RowIndex).Values("Abreviatura"))
            '    'Response.Write("SUBDIMENSIONES:::")
            '    'Por cada nivel de dimension checado, busco si se ha marcado algun nivel de subdimension
            '    If check.Checked Then                    
            '        For Each fila As GridViewRow In gvNivelesSub.Rows
            '            Dim nid As Integer
            '            nid = gvNivelesSub.DataKeys(fila.RowIndex).Values("Codigonid")

            '            'Response.Write("del codigonid:")
            '            'Response.Write(nid)

            '            If nid = codigo_nid Then
            '                Dim chk As CheckBox = TryCast(fila.FindControl("chkSeleccion"), CheckBox)

            '                codigo_csub = Convert.ToInt32(gvNivelesSub.DataKeys(fila.RowIndex).Value)

            '                'Response.Write("Nivelsub: ")
            '                'Response.Write(gvNivelesSub.DataKeys(fila.RowIndex).Value)
            '                'Response.Write(":::")
            '                'Response.Write(gvNivelesSub.DataKeys(fila.RowIndex).Values("Abreviatura"))

            '                If chk.Checked Then
            '                    'Response.Write("CHECADO")
            '                    bandera = 1
            '                Else
            '                    'Response.Write("NOCHECADO")
            '                End If
            '            Else
            '                'Response.Write("-NO CORRESPONDE codigonid--")
            '            End If
            '        Next
            '        'Response.Write("bandera:::")
            '        'Response.Write(bandera)
            '        'Si no encontro ningun nivel subdimension checado, muestra alerta
            '        If bandera = 0 Then
            '            lblMensaje.Text = "Debe marcar por lo menos un Nivel de Subdimensión por cada Nivel de Dimensión checado."
            '            lblMensaje.ForeColor = Drawing.Color.Red
            '            lblMensaje.Visible = True
            '            Exit Sub
            '        End If

            '    End If
            'Next

            '** GUARDAR EN BASE *******************************************************************
            bandera = 0
            If validacustom Then 'Si no ha ingresado cadenas inválidas (select, php, script)
                'If validacustom And validacustom2 Then 'Si no ha ingresado cadenas inválidas (select, php, script)
                'Nuevo registro
                If lblCodigo.Text = "" Then

                    'Retorna el codigo de la variable 



                    codigo_var = obj.InsertarVariable(ddlCategorias.SelectedValue, _
                                                      ddlPeriodicidad.SelectedValue, _
                                                      txtDescripcion.Text.ToUpper, _
                                                      usuario, IIf(chkSumatoria.Checked, "1", "0"))

                    If codigo_var <> "0" Then
                        lblMensaje.Text = "Datos registrados correctamente."

                        'Para avisos
                        Me.Image1.Attributes.Add("src", "../Images/accept.png")
                        Me.avisos.Attributes.Add("class", "mensajeExito")

                        'Insertar Configuracion Subvariable
                        For Each row As GridViewRow In gvNivelesAux.Rows
                            Dim check As CheckBox = TryCast(row.FindControl("chkSeleccion"), CheckBox)

                            If check.Checked Then
                                codigo_naux = Convert.ToInt32(gvNivelesAux.DataKeys(row.RowIndex).Value)
                                bandera = obj.InsertarConfiguracionSubvariables(codigo_var, codigo_naux, usuario)

                                If bandera = 0 Then
                                    lblMensaje.Text = lblMensaje.Text & " La configuración ya estaba registrada."
                                End If

                                'Insertar elementos del nivel subvariable Area administrativa
                                If gvNivelesAux.DataKeys(row.RowIndex).Values("Abreviatura") = "AA" Then
                                    Dim chklst As CheckBoxList = TryCast(row.FindControl("chklstAuxItems"), CheckBoxList)

                                    For i As Integer = 0 To chklst.Items.Count - 1
                                        If chklst.Items(i).Selected Then
                                            obj.InsertarDetalleConfiguracionSubvariable(codigo_var, codigo_naux, chklst.Items(i).Value, usuario)                                            
                                        End If
                                    Next

                                End If
                            End If
                        Next

                        bandera = 0
                        'Insertar Configuracion Dimensiones
                        For Each row As GridViewRow In gvNivelesDim.Rows
                            Dim check As CheckBox = TryCast(row.FindControl("chkSeleccion2"), CheckBox)

                            If check.Checked Then
                                codigo_nid = Convert.ToInt32(gvNivelesDim.DataKeys(row.RowIndex).Value)
                                bandera = obj.InsertarConfiguracionDimensiones(codigo_var, codigo_nid, codigo_naux, usuario)

                                If bandera = 0 Then
                                    lblMensaje.Text = lblMensaje.Text & " La configuración ya estaba registrada."
                                End If

                            End If
                        Next

                        'Insertar Configuracion Subdimensiones
                        For Each row As GridViewRow In gvNivelesSub.Rows
                            Dim check As CheckBox = TryCast(row.FindControl("chkSeleccion"), CheckBox)

                            If check.Checked Then
                                codigo_csub = Convert.ToInt32(gvNivelesSub.DataKeys(row.RowIndex).Value)
                                bandera = obj.InsertarConfiguracionSubdimensiones(codigo_var, codigo_csub, gvNivelesSub.DataKeys(row.RowIndex).Values("Codigonid"), usuario)

                                If bandera = 0 Then
                                    lblMensaje.Text = lblMensaje.Text & " La configuración ya estaba registrada para esa Variable."
                                End If

                            End If
                        Next

                        'lblMensaje.ForeColor = Drawing.Color.Green

                        '**********************************************************************************
                        'ESTRUCTURA AL INSERTAR:
                        'ESTRUCTURA: Bloque que verifica si va a generar la estructura para la variable 
                        '----------------------------------------------------------------------------------
                        'If chkEstructura.Checked = True Then
                        If codigo_var <> "0" Then
                            Dim dtsEs As New Data.DataTable
                            Dim dtsVal As New Data.DataTable
                            Dim vEstructura As Integer = 1
                            dtsEs = obj.GeneraEstructuraVariable(codigo_var, vEstructura, "I")

                            ''---//Primero Verificamos si esta generado los valores en 0
                            'dtsVal = obj.VerificarValoresVariable(codigo_var)
                            dtsVal = obj.ValorCeroVariable(codigo_var, "I")

                        End If
                        'Else
                        '    Dim vEstructura As Integer = 0
                        '    Dim dtsEs As New Data.DataTable
                        '    dtsEs = obj.GeneraEstructuraVariable(codigo_var, vEstructura)
                        'End If

                    Else
                        lblMensaje.Text = "El registro ya existe. No se insertaron los datos."

                        'Para avisos
                        Me.Image1.Attributes.Add("src", "../Images/exclamation.png")
                        Me.avisos.Attributes.Add("class", "mensajeError")
                    End If

                    '***********************************************************************************
                    '***********************  Edicion de registro **************************************
                    '***********************************************************************************
                Else
                    Dim nsub As Integer = 0
                    Dim ndim As Integer = 0
                    Dim naux As Integer = 0
                    Dim dtsresultado As New Data.DataTable

                    lblMensaje.Text = obj.ModificarVariable(lblCodigo.Text, _
                                                            ddlCategorias.SelectedValue, _
                                                            ddlPeriodicidad.SelectedValue, _
                                                            txtDescripcion.Text.ToUpper, _
                                                            usuario, IIf(chkSumatoria.Checked, "1", "0"))

                    If lblMensaje.Text = "Datos modificados correctamente." Then
                        'Para avisos
                        Me.Image1.Attributes.Add("src", "../Images/accept.png")
                        Me.avisos.Attributes.Add("class", "mensajeExito")
                        lblMensaje.ForeColor = Drawing.Color.Black
                    Else
                        'Para avisos
                        Me.Image1.Attributes.Add("src", "../Images/exclamation.png")
                        Me.avisos.Attributes.Add("class", "mensajeError")
                        lblMensaje.ForeColor = Drawing.Color.Black
                    End If

                   
                    'Modificar Configuracion Subvariables
                    'Recorro Subvariables del checkboxlist
                    For Each row As GridViewRow In gvNivelesAux.Rows
                        Dim check As CheckBox = TryCast(row.FindControl("chkSeleccion"), CheckBox)
                        codigo_naux = Convert.ToInt32(gvNivelesAux.DataKeys(row.RowIndex).Value)

                        'Si está seleccionada, la guardo en la base
                        If check.Checked Then
                            bandera = obj.InsertarConfiguracionSubvariables(lblCodigo.Text, codigo_naux, usuario)

                            'Insertar elementos del nivel subvariable Area administrativa
                            If gvNivelesAux.DataKeys(row.RowIndex).Values("Abreviatura") = "AA" Then
                                Dim chklst As CheckBoxList = TryCast(row.FindControl("chklstAuxItems"), CheckBoxList)

                                For i As Integer = 0 To chklst.Items.Count - 1
                                    If chklst.Items(i).Selected Then
                                        obj.InsertarDetalleConfiguracionSubvariable(lblCodigo.Text, codigo_naux, chklst.Items(i).Value, usuario)
                                    End If
                                Next

                            End If
                        End If

                        'Si no está seleccionado: 
                        'Se elimina la configuracion (cambia estado a 1) de la subvariable, y config. de dim y subdim relacionadas, asi como las subvariables, dimensiones y subdimensiones creadas con esos niveles configurados.
                        'Tambien se elimina el detalle de elementos del nivel subvariable AA
                        If check.Checked = False Then
                            dtsresultado = obj.EliminarSubvariableConfigurada(lblCodigo.Text, codigo_naux, usuario, gvNivelesAux.DataKeys(row.RowIndex).Values("Abreviatura"))
                            obj.EliminarDetalleElementosSubvariableConfigurada(lblCodigo.Text, codigo_naux)

                            naux = naux + dtsresultado.Rows(0).Item("NumeroAux")
                            ndim = ndim + dtsresultado.Rows(0).Item("NumeroDim")
                            nsub = nsub + dtsresultado.Rows(0).Item("NumeroSub")
                        End If
                    Next


                    'Modificar Configuracion Dimensiones
                    'Recorro Dimensiones del checkboxlist
                    For Each row As GridViewRow In gvNivelesDim.Rows
                        Dim check As CheckBox = TryCast(row.FindControl("chkSeleccion2"), CheckBox)

                        codigo_nid = Convert.ToInt32(gvNivelesDim.DataKeys(row.RowIndex).Value)
                        codigo_naux = Convert.ToInt32(gvNivelesDim.DataKeys(row.RowIndex).Values("codigo_naux"))

                        'Si está seleccionada, la guardo en la base
                        If check.Checked Then
                            bandera = obj.InsertarConfiguracionDimensiones(lblCodigo.Text, codigo_nid, codigo_naux, usuario)
                        End If

                        'Si no está seleccionado, se comprueba si ha estado configurada. Se elimina la configuracion, si no se esta utilizando
                        If check.Checked = False Then
                            dtsresultado = obj.EliminarDimensionConfigurada(lblCodigo.Text, codigo_nid, usuario, gvNivelesDim.DataKeys(row.RowIndex).Values("Abreviatura"))
                            ndim = ndim + dtsresultado.Rows(0).Item("NumeroDim")
                            nsub = nsub + dtsresultado.Rows(0).Item("NumeroSub")
                        End If
                    Next

                    If ndim > 0 Or nsub > 0 Then
                        lblMensaje.Visible = True
                        lblMensaje.Text = lblMensaje.Text & " Se eliminaron " & ndim & " dimensiones y " & nsub & " subdimensiones."

                    End If

                    nsub = 0

                    'Modificar Configuracion Subdimensiones
                    'Recorro subdimensiones del checkboxlist

                    '###1- Elimino todas las subdimensiones configuradas
                    nsub = nsub + obj.EliminarTodasSubdimensionesConfiguradas(lblCodigo.Text, usuario)

                    '###2- Inserto las nuevas subdimensiones
                    For Each row As GridViewRow In gvNivelesSub.Rows
                        Dim check As CheckBox = TryCast(row.FindControl("chkSeleccion"), CheckBox)
                        codigo_csub = Convert.ToInt32(gvNivelesSub.DataKeys(row.RowIndex).Value)
                        codigo_nid = Convert.ToInt32(gvNivelesSub.DataKeys(row.RowIndex).Values("Codigonid"))

                        'Si está seleccionada, la guardo en la base
                        If check.Checked Then
                            bandera = obj.InsertarConfiguracionSubdimensiones(lblCodigo.Text, codigo_csub, codigo_nid, usuario)
                        End If

                        ''Si no está seleccionado, se comprueba si ha estado configurada. Se elimina la configuracion, si no se esta utilizando
                        'If check.Checked = False Then
                        '    nsub = nsub + obj.EliminarSubdimensionConfigurada(lblCodigo.Text, codigo_csub, usuario)
                        'End If
                    Next

                    'If nsub > 0 Then
                    '    lblMensaje.Text = lblMensaje.Text & " Se eliminaron " & nsub & " subdimensiones."
                    'End If

                    '--------------------------------------------------------------------------------------------------
                    '-------------------------- Validacion de estructura----------------------------------------------
                    'Validamos la variable, si ya tiene valores registrados la estructura no puede modificarse
                    '--------------------------------------------------------------------------------------------------
                    Dim objx As New clsIndicadores
                    Dim dtsx As New Data.DataTable
                    dtsx = objx.VerificaValoresRegistradosVariable(lblCodigo.Text)
                    If dtsx.Rows.Count > 0 Then
                        If dtsx.Rows(0).Item("valor") = 0 Then
                            '**********************************************************************************
                            'ESTRUCTURA AL MODIFICAR:
                            'ESTRUCTURA: Bloque que verifica si va a generar la estructura para la variable 
                            '----------------------------------------------------------------------------------
                            If lblCodigo.Text <> "0" Then
                                Dim dtsEs As New Data.DataTable
                                Dim dtsVal As New Data.DataTable

                                Dim vEstructura As Integer = 1
                                '++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
                                ' Verificando para no duplicar 
                                '++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
                                dtsEs = obj.GeneraEstructuraVariable(lblCodigo.Text, vEstructura, "M")

                                ''---//Primero Verificamos si esta generado los valores en 0
                                dtsVal = obj.ValorCeroVariable(lblCodigo.Text, "M")
                            End If
                        End If
                    End If
                    '+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
                    'Cambio nombre del boton Modificar por Guardar
                    cmdGuardar.Text = "   Guardar"
                End If

                'Refrescar listado
                btnBuscar_Click(sender, e)
                'ConsultarGridRegistros()
                limpiarcajas()
            Else 'Limpia la cadena inválida de la caja de texto
                txtDescripcion.Text = ""
            End If


            'Habilitar grillas de detalle
            gvNivelesAux.Enabled = True
            gvNivelesDim.Enabled = True
            gvNivelesSub.Enabled = True

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub gvVariable_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvVariable.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes.Add("OnMouseOver", "Resaltar(1,this,'S')")
            e.Row.Attributes.Add("OnMouseOut", "Resaltar(0,this,'S')")
            e.Row.Cells(0).Text = e.Row.RowIndex + 1

            'Asignamos un color al codigo de la variable.
            e.Row.Cells(1).ForeColor = Drawing.Color.Blue

            If e.Row.Cells(9).Text = "1" Then
                e.Row.Cells(9).Text = "<center><img src='../images/y_sumatoria.png' style='border: 0px' alt='Sumatoria'/></center>"
            Else
                e.Row.Cells(9).Text = "<center><img src='../images/n_sumatoria.png' style='border: 0px' alt='Sin Sumatoria'/></center>"
            End If

            'Muestra al usuario si la variable tiene registrados valores en los diferentes periodos.
            If e.Row.Cells(5).Text = "SI" Then
                e.Row.Cells(5).Text = "<center><img src='../images/y_sumatoria.png' style='border: 0px' alt='Variable con Valor'/></center>"
            Else
                e.Row.Cells(5).Text = "<center><img src='../images/N_Exist.png' style='border: 0px' alt='Variable sin valor'/></center>"
            End If

            'Si la variable tiene valores, no puede modificar su estructura, esto debido a que alteraria los valores y datos 
            ' que se encuentran amarrado a formulas.
            Dim Var_valor As Decimal = CType(sender.DataKeys(e.Row.RowIndex).Item("Var_valor"), Decimal)
            If Var_valor > 0 Then
                'Muestra icono de bloqueado.
                'e.Row.Cells(10).Text = "<center><img src='../images/closed.png' style='border: 0px' alt='Variable Bloqueda - Editar'/></center>"
                'e.Row.Cells(11).Text = "<center><img src='../images/closed.png' style='border: 0px' alt='Variable Bloqueda - Eliminar'/></center>"

                'e.Row.Cells(10).Enabled = False
                'e.Row.Cells(11).Enabled = False

                'Editar
                e.Row.Cells(10).BackColor = Drawing.Color.Red

                'Eliminar
                e.Row.Cells(11).BackColor = Drawing.Color.Red
                e.Row.Cells(11).Enabled = False
            Else
                'e.Row.Cells(10).Enabled = True  'Editar 
                e.Row.Cells(11).Enabled = True  'Eliminar
                e.Row.Cells(11).BackColor = Drawing.Color.Green
                e.Row.Cells(10).BackColor = Drawing.Color.Green
            End If

            'Si la variable pertenece a una formula, no podra ser eliminada - Modificada
            '02.05.2013 xDguevara
            Dim PerteneceFormula As Integer = CType(sender.DataKeys(e.Row.RowIndex).Item("PertenceFormula"), Integer)
            If PerteneceFormula = 1 Then
                'Editar
                e.Row.Cells(10).BackColor = Drawing.Color.Red
                e.Row.Cells(10).Enabled = False
                'Eliminar
                e.Row.Cells(11).BackColor = Drawing.Color.Red
                e.Row.Cells(11).Enabled = False
            End If

        End If
    End Sub

    Protected Sub gvCategoria_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles gvVariable.RowDeleting
        Try
            Dim obj As New clsIndicadores

            'lblMensaje.Text = obj.EliminarCategoria(gvCategoria.Rows(e.RowIndex).Cells(0).Text)

            'La columna Codigo esta oculta. Por eso uso el DataKey de la fila seleccionada
            lblMensaje.Text = obj.EliminarVariable(gvVariable.DataKeys(e.RowIndex).Value.ToString(), usuario)
            lblMensaje.Visible = True

            If lblMensaje.Text = "Registro eliminado con éxito." Then

                'Para avisos
                Me.Image1.Attributes.Add("src", "../Images/accept.png")
                Me.avisos.Attributes.Add("class", "mensajeExito")

            Else

                'Para avisos
                Me.Image1.Attributes.Add("src", "../Images/exclamation.png")
                Me.avisos.Attributes.Add("class", "mensajeError")

            End If

            'Refrescar campos
            limpiarcajas()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
        ConsultarGridRegistros()
    End Sub

    Protected Sub gvCategoria_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvVariable.RowCommand
        Try
            If (e.CommandName.Equals("Select")) Then 'comprueba que sea el boton de seleccion                

                LimpiarCampos()

                Dim seleccion As GridViewRow
                Dim codigovar_seleccion As String

                'Borro checkboxlist de Niveles Dimension y subdimension
                BorrarCheckBoxListDim()
                BorrarCheckBoxListSub()

                '1. Obtengo la linea del gridview que fue cliqueada
                seleccion = DirectCast(e.CommandSource, GridView).Rows(e.CommandArgument)
                codigovar_seleccion = gvVariable.DataKeys(seleccion.RowIndex).Values("Codigo")


                'Si la variable, tiene registrados valores ya no podra modicar su estructura para mantener la integridad de los datos.
                Me.lblDnt.Text = codigovar_seleccion
                Dim objx As New clsIndicadores
                Dim dtsx As New Data.DataTable
                dtsx = objx.VerificaValoresRegistradosVariable(codigovar_seleccion)
                If dtsx.Rows.Count > 0 Then
                    If dtsx.Rows(0).Item("Valor") > 0 Then
                        gvNivelesAux.Enabled = False
                        gvNivelesDim.Enabled = False
                        gvNivelesSub.Enabled = False
                    Else
                        gvNivelesAux.Enabled = True
                        gvNivelesDim.Enabled = True
                        gvNivelesSub.Enabled = True
                    End If
                End If
                '--------------------------------------------------------------------------------------------------------------


                '2. Grabo en lbl oculto, para luego poder modificar                
                lblCodigo.Text = codigovar_seleccion

                txtDescripcion.Text = HttpUtility.HtmlDecode(gvVariable.Rows(seleccion.RowIndex).Cells.Item(3).Text)
                ddlCategorias.SelectedValue = Convert.ToInt32(gvVariable.DataKeys(seleccion.RowIndex).Values("CodigoCat"))
                ddlPeriodicidad.SelectedValue = Convert.ToInt32(gvVariable.DataKeys(seleccion.RowIndex).Values("CodigoPeri"))

                Dim v_sumatoria As String = gvVariable.DataKeys(seleccion.RowIndex).Values("sumatoria_var").ToString
                If v_sumatoria = "1" Then
                    chkSumatoria.Checked = True
                Else
                    chkSumatoria.Checked = False
                End If

                '3. Obtengo subvariables configuradas y lleno checkboxlist                
                Dim obj As New clsIndicadores
                Dim dts As New Data.DataTable
                Dim contador As Integer
                Dim bandera As Integer = 0

                dts = obj.ConsultarSubvariablesConfiguradas(gvVariable.DataKeys(seleccion.RowIndex).Values("Codigo"))
                LimpiarCheckBoxListAux()

                'Si hay subvariables configuradas
                If dts.Rows.Count > 0 Then
                    'Recorro filas del listado de subvariables configuradas
                    For i As Integer = 0 To dts.Rows.Count - 1
                        'Response.Write(dts.Rows(i).Item(0))

                        'Recorro checkboxlist de subvariables
                        For Each row As GridViewRow In gvNivelesAux.Rows
                            Dim check As CheckBox = TryCast(row.FindControl("chkSeleccion"), CheckBox)

                            'Coincidencia de codigo_naux de la Configuracion, y codigo_naux del checkboxlist
                            'SI Es el nivel configurado
                            If gvNivelesAux.DataKeys(row.RowIndex).Value() = dts.Rows(i).Item(0) Then
                                check.Checked = True
                                'Si la subvariable esta siendo utilizada, se bloquea para impedir que se modifique

                                'Response.Write(codigovar_seleccion)
                                'Response.Write(dts.Rows(i).Item(2))
                                contador = obj.ConsultarNivelesAuxiliarUtilizados(codigovar_seleccion, dts.Rows(i).Item(2))

                                'Cargo niveles dimension
                                CargarNivelesDimension(dts.Rows(i).Item(0))

                                'Solo se bloquea si ha sido utilizado
                                If contador > 0 Then
                                    check.Enabled = False
                                End If


                                'Marco los elementos del nivel, si es Area Administrativa
                                If gvNivelesAux.DataKeys(row.RowIndex).Values("Abreviatura") = "AA" Then

                                    'Consulto los elementos del nivel Area Administrativa configuradis

                                    Dim dts2 As New Data.DataTable

                                    dts2 = obj.ConsultarDetalleConfiguracionAux_Variable(codigovar_seleccion, dts.Rows(i).Item(0))

                                    If dts2.Rows.Count Then
                                        For x As Integer = 0 To dts2.Rows.Count - 1

                                            'Comparo con checkboxlist de elementos configurados

                                            Dim chklst As CheckBoxList = TryCast(row.FindControl("chklstAuxItems"), CheckBoxList)

                                            For c As Integer = 0 To chklst.Items.Count - 1

                                                'Si hay coincidencia, lo marco
                                                If chklst.Items(c).Value = dts2.Rows(x).Item(0) Then
                                                    chklst.Items(c).Selected = True

                                                    'Si el nivel de subvariable esta bloqueado, tambien se bloquean sus elementos

                                                    If bandera = 1 Then
                                                        chklst.Items(c).Enabled = True
                                                    End If

                                                End If
                                            Next
                                        Next
                                    End If
                                End If
                            Else
                                'No es el nivel configurado
                                ''### Se bloquea, porque hay otro nivel configurado                              
                                'check.Enabled = False
                            End If
                        Next
                    Next

                    If contador > 0 Then
                        bandera = 1
                        lblMensaje.Visible = True
                        lblMensaje.Text = "La(s) Subvariable(s) bloqueada(s) se utiliza(n) en " & contador & " fórmula(s). "

                        'Para avisos
                        Me.Image1.Attributes.Add("src", "../Images/error2.png")
                        Me.avisos.Attributes.Add("class", "mensajeAviso")
                        lblMensaje.ForeColor = Drawing.Color.Black
                    End If

                    contador = 0
                End If

                '4. Obtengo dimensiones configuradas y lleno checkboxlist

                '#### si selecciona tb otra dimension, se deben mantener bloqueadas las SUBDIMENSIONES que lo estaban
                '********** Se está limitando configurar a la variable con 5 niveles de dimension *************            
                Dim nivelesdim(4) As Integer
                dts = obj.ConsultarDimensionesConfiguradas(gvVariable.DataKeys(seleccion.RowIndex).Values("Codigo"))
                LimpiarCheckBoxListDim()

                'Si hay dimensiones configuradas
                If dts.Rows.Count > 0 Then
                    'Cargar Niveles Dimension en 0
                    Dim x As Integer
                    For Each x In nivelesdim
                        nivelesdim(x) = 0
                    Next

                    'Recorro filas del listado de Dimensiones configuradas
                    For i As Integer = 0 To dts.Rows.Count - 1
                        'Response.Write(dts.Rows(i).Item(0))
                        nivelesdim(i) = dts.Rows(i).Item(0)
                        'Recorro dimensiones del checkboxlist
                        For Each row As GridViewRow In gvNivelesDim.Rows
                            Dim check As CheckBox = TryCast(row.FindControl("chkSeleccion2"), CheckBox)
                            'Response.Write(gvNivelesSub.DataKeys(row.RowIndex).Value())

                            'Coincidencia de codigo_nid de la Configuracion, y codigo_nid del checkboxlist
                            'Es el Nivel  de Dimension configurado
                            If gvNivelesDim.DataKeys(row.RowIndex).Value() = dts.Rows(i).Item(0) Then
                                check.Checked = True
                                'Si la dimension esta siendo utilizada, se bloquea para impedir que se modifique
                                'Response.Write(codigovar_seleccion)
                                'Response.Write(dts.Rows(i).Item(2))
                                contador = obj.ConsultarNivelesDimensionUtilizados(codigovar_seleccion, dts.Rows(i).Item(2))

                                If contador > 0 Then
                                    check.Enabled = False
                                    bandera = 1
                                End If
                            End If
                        Next
                    Next

                    '********** Se está limitando configurar a la variable con 5 niveles de dimension *************
                    'Cargo niveles subdimension
                    CargarNivelesSubdimension(nivelesdim(0), nivelesdim(1), nivelesdim(2), nivelesdim(3), nivelesdim(4))

                    If contador > 0 Then
                        lblMensaje.Text = lblMensaje.Text & "La(s) Dimension(es) bloqueada(s) se utiliza(n) en " & contador & " fórmula (s)."
                        lblMensaje.Visible = True

                        'Para avisos
                        Me.Image1.Attributes.Add("src", "../Images/error2.png")
                        Me.avisos.Attributes.Add("class", "mensajeAviso")
                        lblMensaje.ForeColor = Drawing.Color.Black
                    End If

                    contador = 0
                End If

                '5. Obtengo subdimensiones configuradas y lleno checkbox.              
                'Listo las subdimensiones configuradas de la variable seleccionada
                dts = obj.ConsultarSubdimensionesConfiguradas(codigovar_seleccion)
                LimpiarCheckBoxListSub()

                'Recorro filas del listado de subdimensiones configuradas
                For i As Integer = 0 To dts.Rows.Count - 1
                    'Response.Write(dts.Rows(i).Item(0))

                    'Recorro subdimensiones del checkboxlist
                    For Each row As GridViewRow In gvNivelesSub.Rows
                        Dim check As CheckBox = TryCast(row.FindControl("chkSeleccion"), CheckBox)
                        'Response.Write(gvNivelesSub.DataKeys(row.RowIndex).Value())
                        'Response.Write("::")
                        'Response.Write(dts.Rows(i).Item(0))
                        'Response.Write("--")

                        'Coincidencia de codigo_csub de la Configuracion, y codigo_csub del checkboxlist
                        'y codigo_nid de la Configuracion, y codigo_nid del checkboxlist
                        If gvNivelesSub.DataKeys(row.RowIndex).Value() = dts.Rows(i).Item(0) _
                        And gvNivelesSub.DataKeys(row.RowIndex).Values("Codigonid") = dts.Rows(i).Item(3) Then
                            check.Checked = True
                            'Si la subdimension esta siendo utilizada, se bloquea para impedir que se modifique
                            'Response.Write(codigovar_seleccion)
                            'Response.Write("--")
                            'Response.Write(dts.Rows(i).Item(2))
                            'contador = obj.ConsultarNivelesSubdimensionUtilizados(codigovar_seleccion, dts.Rows(i).Item(2), dts.Rows(i).Item(3))
                            contador = obj.ConsultarNivelesSubdimensionUtilizados(codigovar_seleccion, dts.Rows(i).Item(0), dts.Rows(i).Item(3))
                            'Response.Write(contador)
                            If contador > 0 Then
                                check.Enabled = False
                                bandera = 1
                            End If
                        End If
                    Next
                Next

                If contador > 0 Then
                    lblMensaje.Text = lblMensaje.Text & " La(s) subdimension(es) bloqueada(s) se utiliza(n) en " & contador & " fórmula(s). "
                    lblMensaje.Visible = True

                    'Para avisos
                    Me.Image1.Attributes.Add("src", "../Images/error2.png")
                    Me.avisos.Attributes.Add("class", "mensajeAviso")
                    lblMensaje.ForeColor = Drawing.Color.Black
                End If

                If bandera = 1 Then
                    lblMensaje.Text = lblMensaje.Text & "Por lo tanto, no se puede(n) modificar."
                End If


                'Cambio nombre del boton Guardar por Modificar
                cmdGuardar.Text = "   Modificar"

            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub limpiarcajas()
        lblCodigo.Text = ""
        txtDescripcion.Text = ""
        ddlPeriodicidad.SelectedValue = 0        
        LimpiarCheckBoxListAux()
        'LimpiarCheckBoxListSub()
        'LimpiarCheckBoxListDim()
        BorrarCheckBoxListDim()
        BorrarCheckBoxListSub()
        LimpiarCheckBoxListAA()

        lblND.Text = ""
        lblValidacionCheckboxlist.Text = ""

        'Cambio nombre del boton Guardar por Modificar
        cmdGuardar.Text = "   Guardar"
    End Sub

    Private Sub LimpiarCampos()

        limpiarcajas()
        lblMensaje.Text = ""
        Me.Image1.Attributes.Add("src", "../Images/beforelastchild.GIF")
        Me.avisos.Attributes.Add("class", "none")        
    End Sub


    Private Sub LimpiarCheckBoxListAux()
        'chNivelesSubdimension.ClearSelection()
        For Each row As GridViewRow In gvNivelesAux.Rows
            Dim check As CheckBox = TryCast(row.FindControl("chkSeleccion"), CheckBox)
            check.Checked = False
            check.Enabled = True            
        Next
    End Sub

    Private Sub LimpiarCheckBoxListSub()
        'chNivelesSubdimension.ClearSelection()
        For Each row As GridViewRow In gvNivelesSub.Rows
            Dim check As CheckBox = TryCast(row.FindControl("chkSeleccion"), CheckBox)
            check.Checked = False
            check.Enabled = True
        Next
    End Sub

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


    Protected Sub btnBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBuscar.Click
        Try
            'Para Validar Caja de Busqueda
            If Page.IsValid Then        'Si no ha ingresado cadenas inválidas (select, php, script)
                Dim dts As New Data.DataTable
                Dim obj As New clsIndicadores

                dts = obj.ConsultarVariablePorNombre(txtBuscar.Text)
                gvVariable.DataSource = dts
                gvVariable.DataBind()
                'gvVariable_RowDataBound(sender, e)
            Else 'Limpia la cadena inválida de la caja de texto
                txtBuscar.Text = ""
            End If

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub CustomValidator1_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles CustomValidator1.ServerValidate
        Try
            Dim cadena As String

            cadena = txtDescripcion.Text

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

    'Protected Sub chkSeleccion_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs)
    '    Dim bandera As Integer = 0
    '    Dim chk As CheckBox = sender

    '    Dim fila As GridViewRow = chk.NamingContainer

    '    'Response.Write(chk.Checked)
    '    'Response.Write(gvNivelesAux.DataKeys(fila.RowIndex).Value())

    '    If chk.Checked = True Then
    '        CargarNivelesDimension(gvNivelesAux.DataKeys(fila.RowIndex).Value())

    '        ''*** SOLO PUEDE HABER UN NIVEL DE AUXILIAR. SE BLOQUEAN LOS DEMAS.
    '        'For Each row As GridViewRow In gvNivelesAux.Rows
    '        '    Dim check As CheckBox = TryCast(row.FindControl("chkSeleccion"), CheckBox)

    '        '    If gvNivelesAux.DataKeys(row.RowIndex).Value() <> gvNivelesAux.DataKeys(fila.RowIndex).Value() Then
    '        '        lblMensaje.Text = "Solo puede seleccionar un Nivel. Si desea elegir otro Nivel, debe deseleccionar el que marcó."
    '        '        lblMensaje.ForeColor = Drawing.Color.Orange
    '        '        lblMensaje.Visible = True
    '        '        check.Enabled = False
    '        '    End If
    '        'Next
    '    Else
    '        BorrarCheckBoxListDim()
    '        BorrarCheckBoxListSub()
    '        lblMensaje.Text = ""
    '        For Each row As GridViewRow In gvNivelesAux.Rows
    '            Dim check As CheckBox = TryCast(row.FindControl("chkSeleccion"), CheckBox)
    '            check.Enabled = True
    '        Next
    '    End If
    'End Sub


    Protected Sub chkSeleccion_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs)

        Dim chk As CheckBox = sender
        Dim fila As GridViewRow = chk.NamingContainer

        '********** Se está limitando configurar a la variable con 5 niveles aux *************
        Dim nivelesaux(4) As Integer
        Dim contador As Integer = 0

        'Cargar Niveles Aux en 0
        Dim i As Integer
        For Each i In nivelesaux
            nivelesaux(i) = 0
        Next

        'Response.Write(chk.Checked)
        'Response.Write(gvNivelesDim.DataKeys(fila.RowIndex).Value())
        'If chk.Checked Then

        '1. Guardar en arreglo los niveles aux checados
        For Each row As GridViewRow In gvNivelesAux.Rows
            Dim check As CheckBox = TryCast(row.FindControl("chkSeleccion"), CheckBox)

            If check.Checked Then
                nivelesaux(contador) = gvNivelesAux.DataKeys(row.RowIndex).Value()
                contador = contador + 1

                '2. Marcar o desmarcar los elementos del nivel aux "area administrativa"
                '2. a Marcar
                If gvNivelesAux.DataKeys(row.RowIndex).Values("Abreviatura") = "AA" Then
                    Dim chklst As CheckBoxList = TryCast(row.FindControl("chklstAuxItems"), CheckBoxList)

                    For x As Integer = 0 To chklst.Items.Count - 1
                        chklst.Items(x).Selected = True
                    Next
                End If
            Else
                '2.b Desmarcar
                If gvNivelesAux.DataKeys(row.RowIndex).Values("Abreviatura") = "AA" Then
                    Dim chklst As CheckBoxList = TryCast(row.FindControl("chklstAuxItems"), CheckBoxList)

                    For x As Integer = 0 To chklst.Items.Count - 1
                        chklst.Items(x).Selected = False
                    Next
                End If
            End If
        Next
        'End If

        '2. CARGAR NUEVAMENTE EL CHECKBOXLIST DE DIMENSIONES
        contador = 0

        '2.1. Salvar las dimensiones marcadas y bloqueadas

        'Contar numero de niveles de dimension del checkboxlist
        Dim total As Integer        
        total = 5

        'Response.Write(total)
        Dim nivelesdim(total, 3) As Integer
        Dim j As Integer

        'Cargar Niveles dimension en 0
        For Each j In nivelesdim
            nivelesdim(j, 0) = 0
        Next
        total = gvNivelesDim.Rows.Count

        For Each row As GridViewRow In gvNivelesDim.Rows
            Dim check As CheckBox = TryCast(row.FindControl("chkSeleccion2"), CheckBox)

            If check.Checked Then
                'guardo el id codigo_nid
                nivelesdim(contador, 0) = gvNivelesDim.DataKeys(row.RowIndex).Value()

                'guardo el id codigo_naux
                nivelesdim(contador, 1) = gvNivelesDim.DataKeys(row.RowIndex).Values("codigo_naux")

                'guardo el valor de enabled
                nivelesdim(contador, 2) = check.Enabled
                contador = contador + 1
            End If
        Next

        '********** Se está limitando configurar a la variable con 5 niveles de dimension *************
        '2.2 Cargar checkboxlist

        CargarNivelesDimension(nivelesaux(0), nivelesaux(1), nivelesaux(2), nivelesaux(3), nivelesaux(4))
        Dim nuevosnivelesdim(5) As Integer

        For x As Integer = 0 To 5
            nuevosnivelesdim(x) = 0
        Next


        '2.3. Checar y/o bloquear
        For Each row As GridViewRow In gvNivelesDim.Rows
            Dim check As CheckBox = TryCast(row.FindControl("chkSeleccion2"), CheckBox)

            For j = 0 To total - 1
                ' For j = 0 To nivelessub.Length - 1
                'Coincidencia de id del valor salvado checado, con id del item del checkboxlist
                'Response.Write(nivelessub(j, 1))
                If nivelesdim(j, 0) = gvNivelesDim.DataKeys(row.RowIndex).Value And _
                nivelesdim(j, 1) = gvNivelesDim.DataKeys(row.RowIndex).Values("Codigonaux") Then
                    'Lo marco
                    check.Checked = True

                    'Lo bloqueo, si corresponde: Asigno el valor del enabled: 
                    check.Enabled = nivelesdim(j, 2)

                    nuevosnivelesdim(j) = nivelesdim(j, 1)
                End If
            Next j
        Next

        '3. Volver a cargar CheckboxList Subdimensiones
        RefrescarChkBoxSubdimensiones(nuevosnivelesdim(0), nuevosnivelesdim(1), nuevosnivelesdim(2), nuevosnivelesdim(3), nuevosnivelesdim(4))

    End Sub

    Private Sub BorrarCheckBoxListDim()
        gvNivelesDim.DataSource = Nothing
        gvNivelesDim.DataBind()
        'chNivelesSubdimension.ClearSelection()
        'For Each row As GridViewRow In gvNivelesDim.Rows
        '    Dim check As CheckBox = TryCast(row.FindControl("chkSeleccion"), CheckBox)

        '    If check.Checked Then
        '        check.Checked = False
        '        check.Enabled = True
        '    End If
        'Next
    End Sub

    Private Sub BorrarCheckBoxListSub()
        gvNivelesSub.DataSource = Nothing
        gvNivelesSub.DataBind()
    End Sub

    Private Sub LimpiarCheckBoxListAA()
        If Me.gvNivelesAux.Rows.Count Then
            For i As Integer = 0 To Me.gvNivelesAux.Rows.Count - 1
                If Me.gvNivelesAux.DataKeys(i).Values("Abreviatura") = "AA" Then
                    Dim chklist As CheckBoxList = TryCast(gvNivelesAux.Rows(i).Cells(1).FindControl("chklstAuxItems"), CheckBoxList)
                    For c As Integer = 0 To chklist.Items.Count - 1
                        chklist.Items(c).Selected = False
                        chklist.Items(c).Enabled = True
                    Next
                End If
            Next
        End If
    End Sub

    'Carga nuevamente el CHECKBOXLIST DE SUBDIMENSIONES
    Private Sub RefrescarChkBoxSubdimensiones(ByVal dim1 As Integer, ByVal dim2 As Integer, ByVal dim3 As Integer, ByVal dim4 As Integer, ByVal dim5 As Integer)

        Dim contador As Integer = 0

        '1. Salvar las Subdimensiones marcadas y/o bloqueadas

        '1.1 Cargar Niveles Subdimension en 0

        '1.1.a Contar numero de niveles de subdimension del checkboxlist
        Dim total As Integer
        total = gvNivelesSub.Rows.Count
        'Response.Write(total)
        Dim nivelessub(total, 3) As Integer
        Dim j As Integer

        '1.1.b Cargar niveles de subdimension del checkboxlist en 0
        For Each j In nivelessub
            nivelessub(j, 0) = 0
        Next

        '1.2 Salvar subdimensiones marcadas y/o bloqueadas
        For Each row As GridViewRow In gvNivelesSub.Rows
            Dim check As CheckBox = TryCast(row.FindControl("chkSeleccion"), CheckBox)

            If check.Checked Then
                'guardo el id codigo_csub
                nivelessub(contador, 0) = gvNivelesSub.DataKeys(row.RowIndex).Value()

                'guardo el id codigo_nid
                nivelessub(contador, 1) = gvNivelesSub.DataKeys(row.RowIndex).Values("Codigonid")

                'guardo el valor de enabled
                nivelessub(contador, 2) = check.Enabled
                contador = contador + 1
            End If
        Next

        '********** Se está limitando configurar a la variable con 5 niveles de dimension *************
        '---2. Cargar checkboxlist
        
        CargarNivelesSubdimension(dim1, dim2, dim3, dim4, dim5)

        '---3. Checar y/o bloquear
        For Each row As GridViewRow In gvNivelesSub.Rows
            Dim check As CheckBox = TryCast(row.FindControl("chkSeleccion"), CheckBox)

            For j = 0 To total - 1
                ' For j = 0 To nivelessub.Length - 1
                'Coincidencia de id del valor salvado checado, con id del item del checoxlist
                'Response.Write(nivelessub(j, 1))
                If nivelessub(j, 0) = gvNivelesSub.DataKeys(row.RowIndex).Value And _
                nivelessub(j, 1) = gvNivelesSub.DataKeys(row.RowIndex).Values("Codigonid") Then
                    'Lo marco
                    check.Checked = True

                    'Lo bloqueo, si corresponde: Asigno el valor del enabled: 
                    check.Enabled = nivelessub(j, 2)
                End If
            Next j
        Next

    End Sub

    Protected Sub chkSeleccion2_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs)

        Dim chk As CheckBox = sender
        Dim fila As GridViewRow = chk.NamingContainer

        '********** Se está limitando configurar a la variable con 5 niveles de dimension *************
        Dim nivelesdim(4) As Integer
        Dim contador As Integer = 0

        'Cargar Niveles Dimension en 0
        Dim i As Integer
        For Each i In nivelesdim
            nivelesdim(i) = 0
        Next

        'Response.Write(chk.Checked)
        'Response.Write(gvNivelesDim.DataKeys(fila.RowIndex).Value())
        'If chk.Checked Then

        'Guardar en arreglo los niveles de dimension checados 
        For Each row As GridViewRow In gvNivelesDim.Rows
            Dim check As CheckBox = TryCast(row.FindControl("chkSeleccion2"), CheckBox)

            If check.Checked Then
                nivelesdim(contador) = gvNivelesDim.DataKeys(row.RowIndex).Value()
                contador = contador + 1
            End If
        Next
        'End If

        'CARGAR NUEVAMENTE EL CHECKBOXLIST DE SUBDIMENSIONES
        '********** Se está limitando configurar a la variable con 5 niveles de dimension *************
        RefrescarChkBoxSubdimensiones(nivelesdim(0), nivelesdim(1), nivelesdim(2), nivelesdim(3), nivelesdim(4))

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

    '********** Se está limitando configurar a la variable con 5 niveles aux *************
    'El proced. soporta comparar hasta con 5 codigos_aux
    'Se corre el riesgo de que los Niveles Aux lleguen a ser mas de 5. 
    'Hasta la fecha solo hay 2: Facultad y Area Administrativa.
    Private Sub CargarNivelesDimension(ByVal codigo_aux1 As Integer, ByVal codigo_aux2 As Integer, ByVal codigo_aux3 As Integer, ByVal codigo_aux4 As Integer, ByVal codigo_aux5 As Integer)
        Try

            Dim obj As New clsIndicadores
            Dim dts As New Data.DataTable
            dts = obj.ConsultarNivelesDimension(codigo_aux1, codigo_aux2, codigo_aux3, codigo_aux4, codigo_aux5)

            gvNivelesDim.DataSource = dts
            gvNivelesDim.DataBind()

            'chNivelesSubdimension.DataSource = dts
            'chNivelesSubdimension.DataTextField = "Descripcion"
            'chNivelesSubdimension.DataValueField = "Codigo"
            'chNivelesSubdimension.DataBind()

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub


    Protected Sub gvNivelesAux_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvNivelesAux.DataBound

        If Me.gvNivelesAux.Rows.Count Then
            For i As Integer = 0 To Me.gvNivelesAux.Rows.Count - 1

                If Me.gvNivelesAux.DataKeys(i).Values("Abreviatura") = "AA" Then
                    Dim chklist As CheckBoxList = TryCast(gvNivelesAux.Rows(i).Cells(1).FindControl("chklstAuxItems"), CheckBoxList)

                    Dim obj As New clsIndicadores
                    Dim dts As New Data.DataTable
                    dts = obj.ConsultarAreasAdministrativas()

                    If dts.Rows.Count > 0 Then

                        'Assigning DataSource to checkboxlist   

                        chklist.DataSource = dts
                        chklist.DataTextField = "descripcion_aa"
                        chklist.DataValueField = "codigo_aa"
                        chklist.DataBind()
                        'chklist.Items.Insert(0, New ListItem("All", "All"))

                    End If
                    'chklist.Attributes.Add("onclick", "chkboxlistchecking('" + chklist.ClientID.ToString() + "','" + chklist.Items.Count + "')")

                End If
            Next

        End If
    End Sub

    Protected Sub chklstAuxItems_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)

        Dim check As CheckBoxList = sender

        For Each row As GridViewRow In gvNivelesAux.Rows
            If gvNivelesAux.DataKeys(row.RowIndex).Values("Abreviatura") = "AA" Then
                Dim chk As CheckBox = TryCast(row.FindControl("chkSeleccion"), CheckBox)
                chk.Checked = True
            End If

        Next

    End Sub

    Protected Sub gvVariable_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvVariable.SelectedIndexChanged

    End Sub

    Protected Sub gvNivelesSub_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvNivelesSub.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then

                e.Row.Attributes.Add("OnMouseOver", "Resaltar(1,this,'S')")
                e.Row.Attributes.Add("OnMouseOut", "Resaltar(0,this,'S')")
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub gvNivelesDim_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvNivelesDim.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                e.Row.Attributes.Add("OnMouseOver", "Resaltar(1,this,'S')")
                e.Row.Attributes.Add("OnMouseOut", "Resaltar(0,this,'S')")
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub gvNivelesAux_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvNivelesAux.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                e.Row.Attributes.Add("OnMouseOver", "Resaltar(1,this,'S')")
                e.Row.Attributes.Add("OnMouseOut", "Resaltar(0,this,'S')")
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub gvNivelesSub_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvNivelesSub.SelectedIndexChanged

    End Sub

    Protected Sub gvNivelesAux_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvNivelesAux.SelectedIndexChanged

    End Sub
End Class



