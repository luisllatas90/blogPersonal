
Partial Class personal_frmCalendarioComputable
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                cargarPeriodoLaborable()
                'función que devuelve el codigo_pel vigente
                Dim obj As New clsPersonal
                Dim codigo_pel As Integer
                codigo_pel = obj.ConsultarPeridoLaborable
                ddlSemana.SelectedValue = "--SELECCIONE--"
                CargaPersonal()                
                CargaParametrosTolerancia()

            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try

    End Sub

    Private Sub cargarPeriodoLaborable()
        Try
            Dim obj As New clsPersonal
            Dim dts As New Data.DataTable
            dts = obj.ConsultarPeriodosLaborables("T", 0)
            ddlPeriodoLaborable.DataSource = dts
            ddlPeriodoLaborable.DataTextField = "descripcion_Pel"
            ddlPeriodoLaborable.DataValueField = "codigo_Pel"
            ddlPeriodoLaborable.DataBind()

            'Combo Periodo2
            ddlPeriodo2.DataSource = dts
            ddlPeriodo2.DataTextField = "descripcion_Pel"
            ddlPeriodo2.DataValueField = "codigo_Pel"
            ddlPeriodo2.DataBind()

            'Combo Periodo3
            ddlPeriodo3.DataSource = dts
            ddlPeriodo3.DataTextField = "descripcion_Pel"
            ddlPeriodo3.DataValueField = "codigo_Pel"
            ddlPeriodo3.DataBind()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try        
    End Sub

    Protected Sub ddlPeriodoLaborable_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPeriodoLaborable.SelectedIndexChanged
        Try
            If ddlPeriodoLaborable.SelectedValue <> 0 Then
                Dim obj As New clsPersonal
                Dim dts As New Data.DataTable
                Dim dtsmesvigente As New Data.DataTable
                'Dim nombremes As String
                dts = obj.ConsultarPeriodosLaborables("A", ddlPeriodoLaborable.SelectedValue)

                'Carga los meses para el periodo seleccionado
                Dim mesdesde As Integer
                Dim meshasta As Integer
                mesdesde = Month(dts.Rows(0).Item("fechainicio_Pel"))
                meshasta = Month(dts.Rows(0).Item("fechafin_Pel"))
                ddlMes.Items.Clear()
                ddlMesVigente.Items.Clear()

                If mesdesde > 0 And meshasta > 0 Then
                    For i As Integer = mesdesde To meshasta
                        Dim item As New ListItem
                        item.Value = i
                        item.Text = obj.ConsultarMes(i)
                        ddlMes.Items.Add(item)
                        ddlMesVigente.Items.Add(item)
                    Next
                End If

                'Cargamos el año del periodo seleccionado
                ddlAño.Items.Clear()
                Dim vAño As Integer = Year(dts.Rows(0).Item("fechainicio_Pel"))
                ddlAño.Items.Add(vAño)
                'Mostramos el rango fechas
                lblRangoFechas.Text = "Desde " & dts.Rows(0).Item("fechainicio_Pel") & " hasta " & dts.Rows(0).Item("fechafin_Pel")
                ConsultarGridCalendarioComputable(ddlPeriodoLaborable.SelectedValue)
                ConsultarGridDiaNoLab(ddlPeriodoLaborable.SelectedValue)
                ConsultarGridPersonalExceptuado()
                ConsultarGridParametroTolerancia()
                CargaPeriodosDependientes()

                'Mes Vigente
                ConsultarMesVigente()
                ConsultarFechaCierre()
                chkMesVigente.Checked = False
                chkFechaCierre.Checked = False
            Else
                ddlMes.Items.Clear()
                ddlAño.Items.Clear()
                lblRangoFechas.Text = ""
            End If

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub btnAñadir_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAñadir.Click
        Try
            'Validar campos
            Dim vMensaje As String = ""
            Dim obj As New clsPersonal

            If ddlPeriodoLaborable.SelectedValue = 0 Then
                vMensaje = "Debe selecionar un periodo laborable."
                Dim myscript As String = "alert('" & vMensaje & "')"
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "myscript", myscript, True)
                Exit Sub
            End If

            If ddlSemana.SelectedValue = "--SELECCIONE--" Then
                vMensaje = "Debe selecionar el Nº de semana."
                Dim myscript As String = "alert('" & vMensaje & "')"
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "myscript", myscript, True)
                Exit Sub
            End If

            If txtdesde.Text = "" Then
                vMensaje = "Debe selecionar la fecha inicio."
                Dim myscript As String = "alert('" & vMensaje & "')"
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "myscript", myscript, True)
                Exit Sub
            End If

            If txthasta.Text = "" Then
                vMensaje = "Debe selecionar la fecha fin."
                Dim myscript As String = "alert('" & vMensaje & "')"
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "myscript", myscript, True)
                Exit Sub
            End If

            If DateTime.Parse(txtdesde.Text) >= DateTime.Parse(txthasta.Text) Then
                vMensaje = "La fecha inicio debe ser menor que la fecha fin."
                Dim myscript As String = "alert('" & vMensaje & "')"
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "myscript", myscript, True)
                Exit Sub
            End If

            'Validamos duplicados
            Dim dtsDuplicado As New Data.DataTable
            dtsDuplicado = obj.ValidaDuplicidad(ddlSemana.SelectedValue, ddlMes.SelectedValue, ddlAño.Text.Trim, ddlPeriodoLaborable.SelectedValue)

            If dtsDuplicado.Rows.Count > 0 Then
                vMensaje = "El registro ya existe, verifique."
                Dim myscript As String = "alert('" & vMensaje & "')"
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "myscript", myscript, True)
                Exit Sub
            Else
                'Inserta el registro
                If ddlMes.Enabled = False Then
                    obj.InsertarSemanasControl(ddlSemana.SelectedValue, 0, ddlAño.Text.Trim, txtdesde.Text, txthasta.Text, ddlPeriodoLaborable.SelectedValue)
                Else
                    obj.InsertarSemanasControl(ddlSemana.SelectedValue, ddlMes.SelectedValue, ddlAño.Text.Trim, txtdesde.Text, txthasta.Text, ddlPeriodoLaborable.SelectedValue)
                End If
                ConsultarGridCalendarioComputable(ddlPeriodoLaborable.SelectedValue)
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub ConsultarGridCalendarioComputable(ByVal codigo_pel As Integer)

        Try
            Dim dts As New Data.DataTable
            Dim obj As New clsPersonal

            dts = obj.ConsultarCalendarioComputable(codigo_pel)
            gvCalendarioComputable.DataSource = dts
            gvCalendarioComputable.DataBind()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try        
    End Sub

    

    Protected Sub gvCalendarioComputable_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles gvCalendarioComputable.RowDeleting
        Try
            Dim obj As New clsPersonal
            obj.EliminarCalendarioComputable(gvCalendarioComputable.Rows(e.RowIndex).Cells(0).Text)
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
        ConsultarGridCalendarioComputable(ddlPeriodoLaborable.SelectedValue)
    End Sub

    
    Protected Sub ddlSemana_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlSemana.SelectedIndexChanged
        Try
            If ddlSemana.SelectedValue = 0 Then
                ddlMes.Enabled = False
            Else
                ddlMes.Enabled = True
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
      
    End Sub

    Protected Sub btnAgregarDiaNoLab_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAgregarDiaNoLab.Click
        Try
            Dim obj As New clsPersonal
            Dim dts As New Data.DataTable
            Dim vMensaje As String = ""

            'Validar
            If ddlPeriodoLaborable.SelectedValue = 0 Then
                vMensaje = "Debe selecionar un periodo laborable."
                Dim myscript As String = "alert('" & vMensaje & "')"
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "myscript", myscript, True)
                Exit Sub
            End If

            If txtDescripcion.Text = "" Then
                vMensaje = "Debe ingresar una Descripción para el dia no laborable."
                Dim myscript As String = "alert('" & vMensaje & "')"
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "myscript", myscript, True)
                Exit Sub
            End If

            If ddlTipoDiaNoLaborable.SelectedValue = "--SELECCIONE--" Then
                vMensaje = "Debe seleccionar un Tipo para el dia no laborable."
                Dim myscript As String = "alert('" & vMensaje & "')"
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "myscript", myscript, True)
                Exit Sub
            End If

            If txtfechanolab.Text = "" Then
                vMensaje = "Debe ingresar la Fecha del dia no laborable."
                Dim myscript As String = "alert('" & vMensaje & "')"
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "myscript", myscript, True)
                Exit Sub
            End If

            'Guardar
            obj.RegistraDiaNoLaborable(txtDescripcion.Text, ddlTipoDiaNoLaborable.SelectedValue, ddlPeriodoLaborable.SelectedValue, txtfechanolab.Text)

            'Devuelve alerta cuando se intenta guardar un registro duplicado
            Dim dtsdianolab As New Data.DataTable

            If dtsdianolab.Rows.Count > 0 Then
                vMensaje = "La fecha ya se encuentra en uso."
                Dim myscript As String = "alert('" & vMensaje & "')"
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "myscript", myscript, True)
                Exit Sub
            End If
            dtsdianolab = obj.ValidaDuplicidadDiaNoLab(txtfechanolab.Text.ToString)

            'Refresar grid
            ConsultarGridDiaNoLab(ddlPeriodoLaborable.SelectedValue)
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub ConsultarGridDiaNoLab(ByVal codigo_pel As Integer)
        Try
            Dim obj As New clsPersonal
            Dim dts As New Data.DataTable
            dts = obj.ConsultarDiaNoLaborable(codigo_pel)
            gvDiaNoLab.DataSource = dts
            gvDiaNoLab.DataBind()

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub gvDiaNoLab_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles gvDiaNoLab.RowDeleting
        Try
            Dim obj As New clsPersonal
            obj.EliminarDiaNoLaborable(gvDiaNoLab.Rows(e.RowIndex).Cells(0).Text)
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
        ConsultarGridDiaNoLab(ddlPeriodoLaborable.SelectedValue)
    End Sub

    'ConsultarPersonalDirectorPersonal_v2
    Private Sub CargaPersonal()
        Try
            Dim obj As New clsPersonal
            Dim dts As New Data.DataTable
            'Carga todos los trabajadores para la configuracion de personal exceptuado        
            dts = obj.ConsultarPersonalDirectorPersonal_v2("%", 1)
            ddlPersonal.DataSource = dts
            ddlPersonal.DataTextField = "personal"
            ddlPersonal.DataValueField = "codigo_per"
            ddlPersonal.DataBind()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try        
    End Sub

    Protected Sub btnAñadirPem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAñadirPem.Click
        Try
            If ddlPeriodoLaborable.SelectedValue = 0 Then
                Dim myscript As String = "alert('Debe elegir el Periodo laborable.')"
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "myscript", myscript, True)
                Exit Sub
            End If

            If ddlPersonal.SelectedValue = 0 Then
                Dim myscript As String = "alert('Por favor seleccione un trabajador.')"
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "myscript", myscript, True)
                Exit Sub
            End If

            If txtFInicio_Pem.Text = "" Then
                Dim myscript As String = "alert('Por favor seleccione la Fecha Inicio.')"
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "myscript", myscript, True)
                Exit Sub
            End If

            If txtFFin_Pem.Text = "" Then
                Dim myscript As String = "alert('Por favor seleccione la Fecha Fin.')"
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "myscript", myscript, True)
                Exit Sub
            End If

            If txtFInicio_Pem.Text > txtFFin_Pem.Text Then
                Dim myscript As String = "alert('La Fecha Inicio debe ser menor que la Fecha Fin.')"
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "myscript", myscript, True)
                Exit Sub
            End If

            Dim obj As New clsPersonal
            Dim mensaje As String
            mensaje = obj.InsertarPersonalExceptuado(ddlPeriodoLaborable.SelectedValue, ddlPersonal.SelectedValue, txtFInicio_Pem.Text, txtFFin_Pem.Text)

            If mensaje = "" Then
                'Refresar grid
                ConsultarGridPersonalExceptuado()
            Else
                Dim myscript As String = "alert('" & mensaje & "')"
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "myscript", myscript, True)
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub ConsultarGridPersonalExceptuado()
        Try
            Dim obj As New clsPersonal
            Dim dts As New Data.DataTable
            dts = obj.ConsultarPersonalExceptuado(ddlPeriodoLaborable.SelectedValue)
            gvPersonalExceptuado.DataSource = dts
            gvPersonalExceptuado.DataBind()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try        
    End Sub

    Protected Sub gvPersonalExceptuado_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles gvPersonalExceptuado.RowDeleting
        Try
            Dim obj As New clsPersonal
            obj.EliminarPersonalExceptuado(gvPersonalExceptuado.Rows(e.RowIndex).Cells(0).Text)
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
        ConsultarGridPersonalExceptuado()        
    End Sub

    Private Sub CargaParametrosTolerancia()
        Try
            Dim obj As New clsPersonal
            Dim dts As New Data.DataTable

            'Carga todos los parametros
            dts = obj.ConsultarParametrosTolerancia(0)
            ddlParametro.DataSource = dts
            ddlParametro.DataTextField = "descripcion_Pap"
            ddlParametro.DataValueField = "abreviatura_Pap"
            ddlParametro.DataBind()

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub btnAñadirPap_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAñadirPap.Click
        Try
            If ddlPeriodoLaborable.SelectedValue = 0 Then
                Dim myscript As String = "alert('Debe elegir el Periodo laborable.')"
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "myscript", myscript, True)
                Exit Sub
            End If
            If ddlParametro.SelectedValue = "S" Then
                Dim myscript As String = "alert('Debe elegir un Parámetro.')"
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "myscript", myscript, True)
                Exit Sub
            End If

            If txtValor.Text = "" Then
                Dim myscript As String = "alert('Por favor ingrese el valor para el Parametro.')"
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "myscript", myscript, True)
                Exit Sub
            End If

            If txtFInicio_Pap.Text = "" Then
                Dim myscript As String = "alert('Por favor seleccione la Fecha Inicio.')"
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "myscript", myscript, True)
                Exit Sub
            End If

            If txtFFin_Pap.Text = "" Then
                Dim myscript As String = "alert('Por favor seleccione la Fecha Fin.')"
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "myscript", myscript, True)
                Exit Sub
            End If

            If txtFInicio_Pap.Text > txtFFin_Pap.Text Then
                Dim myscript As String = "alert('La Fecha Inicio debe ser menor que la Fecha Fin.')"
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "myscript", myscript, True)
                Exit Sub
            End If

            Dim obj As New clsPersonal
            Dim mensaje As String

            mensaje = obj.InsertarParametroTolerancia(ddlPeriodoLaborable.SelectedValue, ddlParametro.SelectedValue, ddlParametro.SelectedItem.Text, txtFInicio_Pap.Text, txtFFin_Pap.Text, CType(txtValor.Text, Integer))

            If mensaje = "" Then
                'Refrescar(grid)
                ConsultarGridParametroTolerancia()
            Else
                Dim myscript As String = "alert('" & mensaje & "')"
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "myscript", myscript, True)
            End If

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub ConsultarGridParametroTolerancia()
        Try
            Dim obj As New clsPersonal
            Dim dts As New Data.DataTable
            dts = obj.ConsultarParametrosTolerancia(ddlPeriodoLaborable.SelectedValue)
            gvParametroTolerancia.DataSource = dts
            gvParametroTolerancia.DataBind()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub


    Protected Sub gvParametroTolerancia_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles gvParametroTolerancia.RowDeleting
        Try
            Dim obj As New clsPersonal
            obj.EliminarParametroTolerancia(gvParametroTolerancia.Rows(e.RowIndex).Cells(0).Text)
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
        ConsultarGridParametroTolerancia()
    End Sub

    Protected Sub btnImportar1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnImportar1.Click
        Try
            Dim mensaje As String

            If ddlPeriodoLaborable.SelectedValue = 0 Then
                Dim script As String = "alert('Debe seleccionar un periodo laborable')"
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "script", script, True)
                Exit Sub
            End If

            If ddlPeriodo1.SelectedValue <> 0 Then
                Dim obj As New clsPersonal

                mensaje = obj.ImportarDiasNoLaborables(ddlPeriodo1.SelectedValue, ddlPeriodoLaborable.SelectedValue)
                ConsultarGridDiaNoLab(ddlPeriodoLaborable.SelectedValue)
            Else
                mensaje = "Debe seleccionar el Periodo laborable a importar."
            End If        
            Dim myscript As String = "alert('" & mensaje & "')"
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "myscript", myscript, True)
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub btnImportar2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnImportar2.Click
        Try
            Dim mensaje As String

            If ddlPeriodoLaborable.SelectedValue = 0 Then
                Dim script As String = "alert('Debe seleccionar un periodo laborable')"
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "script", script, True)
                Exit Sub
            End If

            If ddlPeriodo2.SelectedValue <> 0 Then
                Dim obj As New clsPersonal

                mensaje = obj.ImportarPersonalExceptuado(ddlPeriodo2.SelectedValue, ddlPeriodoLaborable.SelectedValue)
                ConsultarGridPersonalExceptuado()
            Else
                mensaje = "Debe seleccionar el Periodo laborable a importar."
            End If
            Dim myscript As String = "alert('" & mensaje & "')"
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "myscript", myscript, True)
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub
    Private Sub CargaPeriodosDependientes()
        Dim obj As New clsPersonal
        Dim dts As New Data.DataTable        
        dts = obj.ConsultarPeriodosLaborablesDependientes(ddlPeriodoLaborable.SelectedItem.Text)
        'Cargamos los combos de periodo laborable a importar
        'Combo Periodo1
        ddlPeriodo1.DataSource = dts
        ddlPeriodo1.DataTextField = "descripcion_Pel"
        ddlPeriodo1.DataValueField = "codigo_Pel"
        ddlPeriodo1.DataBind()
    End Sub

    Protected Sub btnImportar3_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnImportar3.Click
        Try
            Dim mensaje As String

            If ddlPeriodoLaborable.SelectedValue = 0 Then
                Dim script As String = "alert('Debe seleccionar un periodo laborable')"
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "script", script, True)
                Exit Sub
            End If

            If ddlPeriodo3.SelectedValue <> 0 Then
                Dim obj As New clsPersonal

                mensaje = obj.ImportarParametroTolerancia(ddlPeriodo3.SelectedValue, ddlPeriodoLaborable.SelectedValue)
                ConsultarGridParametroTolerancia()
            Else
                mensaje = "Debe seleccionar el Periodo laborable a importar."
            End If
            Dim myscript As String = "alert('" & mensaje & "')"
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "myscript", myscript, True)
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    'Actualiza Mes Vigente
    Protected Sub btnMesVigente_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnMesVigente.Click
        Try
            Dim mensaje As String = ""

            If ddlMesVigente.SelectedIndex <> -1 Then
                Dim obj As New clsPersonal
                Dim dtsConsultaMesVigente As New Data.DataTable

                dtsConsultaMesVigente = obj.MesVigente(ddlPeriodoLaborable.SelectedValue, ddlMesVigente.SelectedValue, "A")

                If dtsConsultaMesVigente.Rows(0).Item("cantidad") = 0 Then
                    Dim script As String = "alert('El mes seleccionado no se encuentra registrado para el periodo laborable, verifique.')"
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "script", script, True)
                    ddlMesVigente.Enabled = True
                Else
                    ConsultarMesVigente()
                    chkMesVigente.Checked = False
                End If
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub ConsultarMesVigente()
        Dim obj As New clsPersonal
        Dim dtsMesVigente As New Data.DataTable        
        dtsMesVigente = obj.MesVigente(ddlPeriodoLaborable.SelectedValue, 0, "C")

        If dtsMesVigente.Rows.Count > 0 Then
            lblMesVigente.Text = " El mes vigente para el peridodo laborable elegido es: " & dtsMesVigente.Rows(0).Item("mes_sec")
            ddlMesVigente.Enabled = False
            chkMesVigente.Enabled = True
        Else
            lblMesVigente.Text = " No existe ningun mes vigente para el peridodo laborable elegido, verifique."
            ddlMesVigente.Enabled = True
            chkMesVigente.Enabled = False
        End If
    End Sub

    Protected Sub chkMesVigente_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkMesVigente.CheckedChanged
        If chkMesVigente.Checked = True Then ddlMesVigente.Enabled = True
        If chkMesVigente.Checked = False Then ddlMesVigente.Enabled = False

    End Sub


    '----------------------------------------
    Protected Sub btnGuardarFechaCierre_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardarFechaCierre.Click
        Try
            Dim mensaje As String = ""

            If txtFechaCierre.Text = "" Then
                Dim script As String = "alert('Debe seleccionar una Fecha de Cierre, por favor verifique.')"
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "script", script, True)
                Exit Sub
            End If

            If txtFechaCierre.Text <> "" Then
                Dim obj As New clsPersonal
                Dim dts As New Data.DataTable

                dts = obj.FechaCierre(ddlPeriodoLaborable.SelectedValue, txtFechaCierre.Text, "A")

                If dts.Rows(0).Item("sw") = 1 Then
                    Dim script As String = "alert('La fecha seleccionada no corresponde al periodo laborable, verifique.')"
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "script", script, True)
                    ddlMesVigente.Enabled = True
                Else
                    ConsultarFechaCierre()
                    chkFechaCierre.Checked = False
                End If
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try

    End Sub

    Private Sub ConsultarFechaCierre()
        Dim obj As New clsPersonal
        Dim dts As New Data.DataTable

        dts = obj.FechaCierre(ddlPeriodoLaborable.SelectedValue, DateTime.Now, "C")

        If dts.Rows.Count = 0 Then            
            lblFechaCierre.Text = " No se ha registrado ninguna Fecha de Cierre para el peridodo laborable elegido, por favor actualice."
            txtFechaCierre.Enabled = True
            chkFechaCierre.Enabled = False
            Exit Sub
        End If


        If dts.Rows.Count > 0 And dts.Rows(0).Item("fechaCierre").ToString <> "" Then
            lblFechaCierre.Text = " La Fecha de Cierre del peridodo laborable elegido es: " & dts.Rows(0).Item("fechaCierre")
            txtFechaCierre.Enabled = False
            chkFechaCierre.Enabled = True
        Else
            lblFechaCierre.Text = " No se ha registrado ninguna Fecha de Cierre para el peridodo laborable elegido, por favor actualice."
            txtFechaCierre.Enabled = True
            chkFechaCierre.Enabled = False
        End If

    End Sub


    Protected Sub chkFechaCierre_CheckedChanged1(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkFechaCierre.CheckedChanged
        If chkFechaCierre.Checked = True Then
            txtFechaCierre.Enabled = True
            btnFechaCierre.Disabled = False
        End If

        If chkFechaCierre.Checked = False Then
            txtFechaCierre.Enabled = False
            btnFechaCierre.Disabled = True
        End If
    End Sub
End Class






