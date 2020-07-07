Imports System.IO

Partial Class personal_frmHorarioListaTrabajadores
    Inherits System.Web.UI.Page

    Dim codigo_pel As Integer

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                CargaEstadoHorario()
                CargaCboTipoPersonal()
                CargaCboDedicacion()
                CargaCentroCostos()
                CargaCboCalificarHorario()

                'Inicializa el nombre del Boton
                cmdEjecutar.Text = "Finalizar Enviar"

                vEstadoObservacion(False)
                'vEstadoRadioButton(False)


                CargaCboEstadoPlanillaCampus()
                CargaCboEstadoPlanillaCampusFiltro()
                CargaCboEstadoEnvioDirectorPersonal()

                If chkTodos.Checked = False Then
                    chkTodos.Text = "Habilitar Envios:"
                    chkenvioDirector.Visible = False
                    chlenvioDirPersonal.Visible = False
                End If

                ddlEstadoCampusF.SelectedValue = 1      'Cuando Cargue apunte solo activos al campus
                ddlEstadoPlanillaF.SelectedValue = 1    'Cuando Cargue apunte solo activos en planilla

                CargaGridPersonal(1, ddlEstadoHorario.SelectedValue.Trim, Me.ddlTipoPersonal.SelectedValue.Trim, Me.ddlDedicacion.SelectedValue.Trim, Me.ddlCentroCosto.SelectedValue, ddlEstadoPlanillaF.SelectedValue, Me.ddlEstadoCampusF.SelectedValue, "%", "%")
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub CargaCboEstadoEnvioDirectorPersonal()
        Try
            Dim obj As New clsPersonal
            Dim dts As New Data.DataTable

            dts = obj.ListaEstadoPerHorario(2)
            If dts.Rows.Count > 0 Then
                ddlEstadoEnvioDirector.DataSource = dts
                ddlEstadoEnvioDirector.DataTextField = "Descripcion"
                ddlEstadoEnvioDirector.DataValueField = "Codigo"
                ddlEstadoEnvioDirector.DataBind()

                ddlEnvioDirPersonal.DataSource = dts
                ddlEnvioDirPersonal.DataTextField = "Descripcion"
                ddlEnvioDirPersonal.DataValueField = "Codigo"
                ddlEnvioDirPersonal.DataBind()
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
            Response.Write("<script>alert('Ocurrio un error al procesar los datos, intentelo nuevamente')</script>")
        End Try
    End Sub


    Private Sub CargaCboEstadoPlanillaCampus()
        Try
            Dim obj As New clsPersonal
            Dim dts As New Data.DataTable


            dts = obj.ListaEstadoPerHorario(1)
            If dts.Rows.Count > 0 Then
                ddlEstadoPlanilla.DataSource = dts
                ddlEstadoPlanilla.DataTextField = "Descripcion"
                ddlEstadoPlanilla.DataValueField = "Codigo"
                ddlEstadoPlanilla.DataBind()

                ddlEstadoCampus.DataSource = dts
                ddlEstadoCampus.DataTextField = "Descripcion"
                ddlEstadoCampus.DataValueField = "Codigo"
                ddlEstadoCampus.DataBind()
            End If

        Catch ex As Exception
            Response.Write(ex.Message)
            Response.Write("<script>alert('Ocurrio un error al procesar los datos, intentelo nuevamente')</script>")
        End Try
    End Sub

    Private Sub CargaCboEstadoPlanillaCampusFiltro()
        Try
            Dim obj As New clsPersonal
            Dim dts As New Data.DataTable


            dts = obj.ListaEstadoPerHorario(3)
            If dts.Rows.Count > 0 Then
                ddlEstadoPlanillaF.DataSource = dts
                ddlEstadoPlanillaF.DataTextField = "Descripcion"
                ddlEstadoPlanillaF.DataValueField = "Codigo"
                ddlEstadoPlanillaF.DataBind()

                ddlEstadoCampusF.DataSource = dts
                ddlEstadoCampusF.DataTextField = "Descripcion"
                ddlEstadoCampusF.DataValueField = "Codigo"
                ddlEstadoCampusF.DataBind()
            End If

        Catch ex As Exception
            Response.Write(ex.Message)
            Response.Write("<script>alert('Ocurrio un error al procesar los datos, intentelo nuevamente')</script>")
        End Try
    End Sub



    Private Sub CargaCboEstados(ByVal vcbo As String, ByVal vTipo As Integer)
        Try
            Dim obj As New clsPersonal
            Dim dtsA As New Data.DataTable
            Dim dtsB As New Data.DataTable

            If vcbo = "PLLA" Then
                dtsA = obj.ListaEstadoPerHorario(vTipo)
                If dtsA.Rows.Count > 0 Then
                    ddlEstadoPlanilla.DataSource = dtsA
                    ddlEstadoPlanilla.DataTextField = "Descripcion"
                    ddlEstadoPlanilla.DataValueField = "Codigo"
                    ddlEstadoPlanilla.DataBind()
                End If
            End If

            If vcbo = "CPUS" Then
                dtsA = obj.ListaEstadoPerHorario(vTipo)
                If dtsA.Rows.Count > 0 Then
                    ddlEstadoCampus.DataTextField = "Descripcion"
                    ddlEstadoCampus.DataValueField = "Codigo"
                    ddlEstadoCampus.DataBind()
                End If
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
            Response.Write("<script>alert('Ocurrio un error al procesar los datos, intentelo nuevamente')</script>")
        End Try
    End Sub

    

    Private Sub vEstadoObservacion(ByVal vEstado As Boolean)
        Try
            txtObservacion.Enabled = vEstado
            txtAsunto.Enabled = vEstado
        Catch ex As Exception
            Response.Write(ex.Message)
            Response.Write("<script>alert('Ocurrio un error al procesar los datos, intentelo nuevamente')</script>")
        End Try
    End Sub

    Private Sub CargaCboCalificarHorario()
        Try
            Dim obj As New clsPersonal
            Dim dts As New Data.DataTable

            dts = obj.CalificarHorario()
            If dts.Rows.Count > 0 Then
                ddlEvaluacionHorario.DataSource = dts
                ddlEvaluacionHorario.DataTextField = "Descripcion"
                ddlEvaluacionHorario.DataValueField = "Codigo"
                ddlEvaluacionHorario.DataBind()
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
            Response.Write("<script>alert('Ocurrio un error al procesar los datos, intentelo nuevamente')</script>")
        End Try
    End Sub

    Private Sub CargaEstadoHorario()
        Try
            Dim obj As New clsPersonal
            Dim dts As New Data.DataTable
            dts = obj.ListaEstadoHorario()

            If dts.Rows.Count > 0 Then
                ddlEstadoHorario.DataSource = dts
                ddlEstadoHorario.DataTextField = "Descripcion"
                ddlEstadoHorario.DataValueField = "Codigo"
                ddlEstadoHorario.DataBind()
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
            Response.Write("<script>alert('Ocurrio un error al procesar los datos, intentelo nuevamente')</script>")
        End Try
    End Sub

    Private Sub CargaCentroCostos()
        Try
            'Carga CC del personal que tiene registrado horario personal
            Dim dts As New Data.DataTable
            Dim obj As New clsPersonal
            dts = obj.ConsultarCentroCostosPer()
            ddlCentroCosto.DataSource = dts
            ddlCentroCosto.DataTextField = "descripcion_Cco"
            ddlCentroCosto.DataValueField = "codigo_cco"
            ddlCentroCosto.DataBind()
        Catch ex As Exception
            Response.Write(ex.Message)
            Response.Write("<script>alert('Ocurrio un error al procesar los datos, intentelo nuevamente')</script>")
        End Try
    End Sub

    Private Sub CargaCboDedicacion()
        Try
            Dim obj As New clsPersonal
            Dim dts As New Data.DataTable

            dts = obj.ListaDedicacion()
            If dts.Rows.Count > 0 Then
                ddlDedicacion.DataSource = dts
                ddlDedicacion.DataTextField = "Descripcion"
                ddlDedicacion.DataValueField = "Codigo"
                ddlDedicacion.DataBind()
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub CargaCboTipoPersonal()
        Try
            Dim obj As New clsPersonal
            Dim dts As New Data.DataTable

            dts = obj.ListaTipoPersonal()

            If dts.Rows.Count > 0 Then
                ddlTipoPersonal.DataSource = dts
                ddlTipoPersonal.DataTextField = "Descripcion"
                ddlTipoPersonal.DataValueField = "Codigo"
                ddlTipoPersonal.DataBind()
            End If

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub



    Private Sub CargaGridPersonal(ByVal vFiltro As Integer, ByVal estado_hop As String, _
                                  ByVal tipopersonal As String, ByVal dedicacion As String, ByVal ceco As String, _
                                  ByVal estado_Per As String, ByVal codigo_Est As String, _
                                  ByVal envioDirector_Per As String, ByVal envioDirPersonal_Per As String)
        Try
            Dim obj As New clsPersonal
            Dim dts As New Data.DataTable

            LimpiaGridView()
            dts = obj.CargarListaPersonal(vFiltro, estado_hop, tipopersonal, dedicacion, ceco, estado_Per, codigo_Est, envioDirector_Per, envioDirPersonal_Per)
            'Response.Write("<br\>")
            'Response.Write(dts.Rows.Count)

            If dts.Rows.Count > 0 Then
                gvListaTrabajadores.DataSource = dts
                gvListaTrabajadores.DataBind()


            End If
        Catch ex As Exception
            Response.Write(ex.Message)
            Response.Write("<script>alert('Ocurrio un error al procesar los datos, intentelo nuevamente')</script>")
        End Try
    End Sub

    Private Sub LimpiaGridView()
        Try
            gvListaTrabajadores.DataSource = Nothing
            gvListaTrabajadores.DataBind()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub ddlCentroCosto_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlCentroCosto.SelectedIndexChanged
        Try
            Dim envioDirector As String
            Dim envioDirPersonal As String

            If chkTodos.Checked = True Then

                If chkenvioDirector.Checked = True Then
                    envioDirector = "1"
                Else
                    envioDirector = "0"
                End If

                If chlenvioDirPersonal.Checked = True Then
                    envioDirPersonal = "1"
                Else
                    envioDirPersonal = "0"
                End If

                CargaGridPersonal(1, ddlEstadoHorario.SelectedValue.Trim, Me.ddlTipoPersonal.SelectedValue.Trim, Me.ddlDedicacion.SelectedValue.Trim, Me.ddlCentroCosto.SelectedValue, ddlEstadoPlanillaF.SelectedValue, Me.ddlEstadoCampusF.SelectedValue, envioDirector, envioDirPersonal)
            Else
                CargaGridPersonal(1, ddlEstadoHorario.SelectedValue.Trim, Me.ddlTipoPersonal.SelectedValue.Trim, Me.ddlDedicacion.SelectedValue.Trim, Me.ddlCentroCosto.SelectedValue, ddlEstadoPlanillaF.SelectedValue, Me.ddlEstadoCampusF.SelectedValue, "%", "%")
            End If


            Me.lblMensaje.Text = ""

            'CargaGridPersonal(1, ddlEstadoHorario.SelectedValue.Trim, Me.ddlTipoPersonal.SelectedValue.Trim, Me.ddlDedicacion.SelectedValue.Trim, Me.ddlCentroCosto.SelectedValue, ddlEstadoPlanillaF.SelectedValue, Me.ddlEstadoCampusF.SelectedValue, envioDirector, envioDirPersonal)
            'Response.Write(ddlCentroCosto.SelectedValue)
            'Response.Write("<br\>")
        Catch ex As Exception
            Response.Write(ex.Message)
            Response.Write("<script>alert('Ocurrio un error al procesar los datos, intentelo nuevamente')</script>")
        End Try
    End Sub

    Protected Sub ddlEstadoHorario_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlEstadoHorario.SelectedIndexChanged
        Try
            Dim envioDirector As String
            Dim envioDirPersonal As String

            If chkTodos.Checked = True Then

                If chkenvioDirector.Checked = True Then
                    envioDirector = "1"
                Else
                    envioDirector = "0"
                End If

                If chlenvioDirPersonal.Checked = True Then
                    envioDirPersonal = "1"
                Else
                    envioDirPersonal = "0"
                End If

                CargaGridPersonal(1, ddlEstadoHorario.SelectedValue.Trim, Me.ddlTipoPersonal.SelectedValue.Trim, Me.ddlDedicacion.SelectedValue.Trim, Me.ddlCentroCosto.SelectedValue, ddlEstadoPlanillaF.SelectedValue, Me.ddlEstadoCampusF.SelectedValue, envioDirector, envioDirPersonal)
            Else
                CargaGridPersonal(1, ddlEstadoHorario.SelectedValue.Trim, Me.ddlTipoPersonal.SelectedValue.Trim, Me.ddlDedicacion.SelectedValue.Trim, Me.ddlCentroCosto.SelectedValue, ddlEstadoPlanillaF.SelectedValue, Me.ddlEstadoCampusF.SelectedValue, "%", "%")
            End If

            Me.lblMensaje.Text = ""

            'Response.Write(ddlEstadoHorario.SelectedValue)
            'Response.Write("<br\>")
        Catch ex As Exception
            Response.Write(ex.Message)
            Response.Write("<script>alert('Ocurrio un error al procesar los datos, intentelo nuevamente')</script>")
        End Try
    End Sub

    Protected Sub ddlTipoPersonal_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlTipoPersonal.SelectedIndexChanged
        Try
            'CargaGridPersonal(1, ddlEstadoHorario.SelectedValue.Trim, Me.ddlTipoPersonal.SelectedValue.Trim, "%", "%")

            Dim envioDirector As String
            Dim envioDirPersonal As String

            If chkTodos.Checked = True Then

                If chkenvioDirector.Checked = True Then
                    envioDirector = "1"
                Else
                    envioDirector = "0"
                End If

                If chlenvioDirPersonal.Checked = True Then
                    envioDirPersonal = "1"
                Else
                    envioDirPersonal = "0"
                End If

                CargaGridPersonal(1, ddlEstadoHorario.SelectedValue.Trim, Me.ddlTipoPersonal.SelectedValue.Trim, Me.ddlDedicacion.SelectedValue.Trim, Me.ddlCentroCosto.SelectedValue, ddlEstadoPlanillaF.SelectedValue, Me.ddlEstadoCampusF.SelectedValue, envioDirector, envioDirPersonal)
            Else
                CargaGridPersonal(1, ddlEstadoHorario.SelectedValue.Trim, Me.ddlTipoPersonal.SelectedValue.Trim, Me.ddlDedicacion.SelectedValue.Trim, Me.ddlCentroCosto.SelectedValue, ddlEstadoPlanillaF.SelectedValue, Me.ddlEstadoCampusF.SelectedValue, "%", "%")
            End If


            Me.lblMensaje.Text = ""

            'CargaGridPersonal(1, ddlEstadoHorario.SelectedValue.Trim, Me.ddlTipoPersonal.SelectedValue.Trim, Me.ddlDedicacion.SelectedValue.Trim, Me.ddlCentroCosto.SelectedValue, ddlEstadoPlanillaF.SelectedValue, Me.ddlEstadoCampusF.SelectedValue, envioDirector, envioDirPersonal)
            'Response.Write(Me.ddlTipoPersonal.SelectedValue.Trim)
            'Response.Write("<br\>")
        Catch ex As Exception
            Response.Write(ex.Message)
            Response.Write("<script>alert('Ocurrio un error al procesar los datos, intentelo nuevamente')</script>")
        End Try
    End Sub

    Protected Sub gvListaTrabajadores_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gvListaTrabajadores.PageIndexChanging
        Try

            Dim envioDirector As String
            Dim envioDirPersonal As String


            gvListaTrabajadores.PageIndex = e.NewPageIndex
            If chkTodos.Checked = True Then

                If chkenvioDirector.Checked = True Then
                    envioDirector = "1"
                Else
                    envioDirector = "0"
                End If

                If chlenvioDirPersonal.Checked = True Then
                    envioDirPersonal = "1"
                Else
                    envioDirPersonal = "0"
                End If

                CargaGridPersonal(1, ddlEstadoHorario.SelectedValue.Trim, Me.ddlTipoPersonal.SelectedValue.Trim, Me.ddlDedicacion.SelectedValue.Trim, Me.ddlCentroCosto.SelectedValue, ddlEstadoPlanillaF.SelectedValue, Me.ddlEstadoCampusF.SelectedValue, envioDirector, envioDirPersonal)
            Else
                CargaGridPersonal(1, ddlEstadoHorario.SelectedValue.Trim, Me.ddlTipoPersonal.SelectedValue.Trim, Me.ddlDedicacion.SelectedValue.Trim, Me.ddlCentroCosto.SelectedValue, ddlEstadoPlanillaF.SelectedValue, Me.ddlEstadoCampusF.SelectedValue, "%", "%")
            End If


        Catch ex As Exception
            Response.Write(ex.Message)
            Response.Write("<script>alert('Ocurrio un error al procesar los datos, intentelo nuevamente')</script>")
        End Try
    End Sub

    Protected Sub gvListaTrabajadores_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvListaTrabajadores.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then

                'string strScript = "SelectDeSelectHeader(" + ((CheckBox)e.Row.Cells[0].FindControl("chkSelect")).ClientID + ");";
                '((CheckBox)e.Row.Cells[0].FindControl("chkSelect")).Attributes.Add("onclick", strScript);                

                'Dim strScript="SelectDeSelectHeader(" + ((CheckBox)e.Row.Cells[0].FindControl("chkSelect")).ClientID + ");"((CheckBox)e.Row.Cells[0].FindControl("chkSelect")).Attributes.Add("onclick", strScript)                

                'Pinta el Texto de Rojo o Azul de EnvioDirector
                If e.Row.Cells(9).Text.Substring(0, 1) = "N" Then
                    e.Row.Cells(9).ForeColor = Drawing.Color.Red   'Solo el texto
                Else
                    e.Row.Cells(9).ForeColor = Drawing.Color.Blue   'Solo el texto
                End If


                'Pinta el Texto de Rojo o Azul de EnvioPersonal
                If e.Row.Cells(10).Text.Substring(0, 1) = "N" Then
                    e.Row.Cells(10).ForeColor = Drawing.Color.Red   'Solo el texto
                Else
                    e.Row.Cells(10).ForeColor = Drawing.Color.Blue   'Solo el texto
                End If

                'Pintar los registros que en envioPersonal =1, sin embargo en envioDirector=0
                If e.Row.Cells(10).Text.Substring(0, 1) = "E" And e.Row.Cells(9).Text.Substring(0, 1) = "N" Then
                    e.Row.BackColor = Drawing.Color.Tomato       'Pinta toda la fila
                    e.Row.ForeColor = Drawing.Color.Black
                End If

                'Pinta la Fila que tenga estado Horario Observado y tienen los dos envios en 1
                If (e.Row.Cells(8).Text.Substring(0, 1) = "O" Or e.Row.Cells(8).Text.Substring(0, 1) = "P") And e.Row.Cells(9).Text.Substring(0, 1) = "E" And e.Row.Cells(10).Text.Substring(0, 1) = "E" Then
                    e.Row.BackColor = Drawing.Color.Pink       'Pinta toda la fila
                    e.Row.ForeColor = Drawing.Color.Black
                End If

                '====================================================================================================================================================
                'Oculta los checkbox, si tienen EnvioDirector =1/ EnvioDirPersonal=1 y Estado Conforme.
                'Debido a que se quiere habilitar los horarios de los trabajaderes de CC.SA
                'Se comentara este bloeque. 24.04.2013
                '====================================================================================================================================================
                'If e.Row.Cells(8).Text.Substring(0, 1) = "C" And e.Row.Cells(9).Text.Substring(0, 1) = "E" And e.Row.Cells(10).Text.Substring(0, 1) = "E" Then
                '    Dim Cb As CheckBox
                '    Cb = e.Row.FindControl("chkSel")
                '    Cb.Visible = False
                'End If
                '====================================================================================================================================================

                e.Row.Cells(0).Text = e.Row.RowIndex + 1
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
            Response.Write("<script>alert('Ocurrio un error al procesar los datos, intentelo nuevamente')</script>")
        End Try
    End Sub

    Protected Sub ddlDedicacion_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlDedicacion.SelectedIndexChanged
        Try
            'CargaGridPersonal(1, ddlEstadoHorario.SelectedValue.Trim, Me.ddlTipoPersonal.SelectedValue.Trim, Me.ddlDedicacion.SelectedValue.Trim, "%")

            Dim envioDirector As String
            Dim envioDirPersonal As String

            If chkTodos.Checked = True Then

                If chkenvioDirector.Checked = True Then
                    envioDirector = "1"
                Else
                    envioDirector = "0"
                End If

                If chlenvioDirPersonal.Checked = True Then
                    envioDirPersonal = "1"
                Else
                    envioDirPersonal = "0"
                End If

                CargaGridPersonal(1, ddlEstadoHorario.SelectedValue.Trim, Me.ddlTipoPersonal.SelectedValue.Trim, Me.ddlDedicacion.SelectedValue.Trim, Me.ddlCentroCosto.SelectedValue, ddlEstadoPlanillaF.SelectedValue, Me.ddlEstadoCampusF.SelectedValue, envioDirector, envioDirPersonal)
            Else
                CargaGridPersonal(1, ddlEstadoHorario.SelectedValue.Trim, Me.ddlTipoPersonal.SelectedValue.Trim, Me.ddlDedicacion.SelectedValue.Trim, Me.ddlCentroCosto.SelectedValue, ddlEstadoPlanillaF.SelectedValue, Me.ddlEstadoCampusF.SelectedValue, "%", "%")
            End If


            Me.lblMensaje.Text = ""

            'CargaGridPersonal(1, ddlEstadoHorario.SelectedValue.Trim, Me.ddlTipoPersonal.SelectedValue.Trim, Me.ddlDedicacion.SelectedValue.Trim, Me.ddlCentroCosto.SelectedValue, ddlEstadoPlanillaF.SelectedValue, Me.ddlEstadoCampusF.SelectedValue, envioDirector, envioDirPersonal)
            'Response.Write(Me.ddlDedicacion.SelectedValue.Trim)
            'Response.Write("<br\>")
        Catch ex As Exception
            Response.Write(ex.Message)
            Response.Write("<script>alert('Ocurrio un error al procesar los datos, intentelo nuevamente')</script>")
        End Try
    End Sub


    Protected Sub cmdCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdCancelar.Click
        Try
            Me.ddlCentroCosto.SelectedValue = "%"
            Me.ddlTipoPersonal.SelectedValue = "%"
            Me.ddlDedicacion.SelectedValue = "%"
            Me.ddlEstadoHorario.SelectedValue = "%"
            Me.ddlEstadoCampusF.SelectedValue = "%"
            Me.ddlEstadoPlanillaF.SelectedValue = "%"

            CargaGridPersonal(1, "%", "%", "%", "%", "%", "%", "%", "%")
        Catch ex As Exception
            Response.Write(ex.Message)
            Response.Write("<script>alert('Ocurrio un error al procesar los datos, intentelo nuevamente')</script>")
        End Try
    End Sub


    Protected Sub ddlEvaluacionHorario_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlEvaluacionHorario.SelectedIndexChanged
        Try

            'Response.Write(ddlEvaluacionHorario.SelectedValue)

            'Cuando la operacion es OBSERBAR: EnvioDirector=0 / EnvioDirPersonal=0
            If ddlEvaluacionHorario.SelectedValue = "O" Then
                vEstadoObservacion(True)
                'txtObservacion.Focus()
                txtAsunto.Focus()


                cmdEjecutar.Text = "Observar"
                ddlEstadoEnvioDirector.SelectedValue = 0
                ddlEnvioDirPersonal.SelectedValue = 0

                txtAsunto.Text = "Registro de Horario Observado"

                ddlEnvioDirPersonal.Enabled = False
                ddlEstadoEnvioDirector.Enabled = False

                lblAccion.ForeColor = Drawing.Color.Purple
                lblAccion.Text = "PERMITE DESBLOQUEAR EL HORARIO, ANULA EL ENVIO A PERSONAL Y ENVIA UN EMAIL AL TRABAJADOR(ES) SELECCIONADO(S)."
                lblAccion.Font.Bold = True
                lblMensaje.Text = ""

                Exit Sub
            End If

            'Cuando la operacion es COMUNICAR: No cambia los estados de envio -> EnvioDirector / EnvioDirPersonal
            If ddlEvaluacionHorario.SelectedValue = "N" Then
                vEstadoObservacion(True)
                'txtObservacion.Focus()
                txtAsunto.Focus()


                cmdEjecutar.Text = "Comunicar"
                ddlEstadoEnvioDirector.SelectedValue = -1
                ddlEnvioDirPersonal.SelectedValue = -1
                ddlEnvioDirPersonal.Enabled = False
                ddlEstadoEnvioDirector.Enabled = False

                txtAsunto.Text = "Aviso - Registro Horarios"

                lblAccion.ForeColor = Drawing.Color.Brown
                lblAccion.Text = "PERMITE ENVIAR EMAIL A LOS TRABAJADORES, SIN REALIZAR ACCION ALGUNA."
                lblAccion.Font.Bold = True
                lblMensaje.Text = ""

                Exit Sub
            End If
            '-----------------------------------------------------------------------------------------------------



            'Cuando la operacion CONFORME: EnvioDirector=1 / EnvioDirPersonal=1
            If ddlEvaluacionHorario.SelectedValue = "C" Then
                ddlEstadoEnvioDirector.SelectedValue = 1
                ddlEstadoEnvioDirector.Enabled = False
                ddlEnvioDirPersonal.SelectedValue = 1
                ddlEnvioDirPersonal.Enabled = False

                lblAccion.ForeColor = Drawing.Color.Blue
                lblAccion.Text = "FINALIZA EL PROCESO, BLOQUEANDO EL HORARIO, ANULA LA OBSERVACION, ENVIA LA CONFORMIDAD A PERSONAL Y LE HACE LLEGAR UN EMAIL AL TRABAJADOR."
                lblAccion.Font.Bold = True

                txtAsunto.Text = ""
                txtObservacion.Text = ""
                cmdEjecutar.Text = "Finalizar Enviar"
                lblMensaje.Text = ""
                Exit Sub
            End If
            '-------------------------------------------------------------------------


            'Cuando la operacion BLOQUEAR HORARIO: EnvioDirector=1 / EnvioDirPersonal=0
            If ddlEvaluacionHorario.SelectedValue = "B" Then
                ddlEstadoEnvioDirector.SelectedValue = 1
                ddlEstadoEnvioDirector.Enabled = False
                ddlEnvioDirPersonal.SelectedValue = 0
                ddlEnvioDirPersonal.Enabled = False

                lblAccion.ForeColor = Drawing.Color.Red
                lblAccion.Text = "BLOQUEA EL HORARIO DEL TRABAJADOR Y ANULA EL ENVIO DE CONFORMIDAD A PERSONAL."
                lblAccion.Font.Bold = True

                txtAsunto.Text = ""
                txtObservacion.Text = ""
                cmdEjecutar.Text = "Finalizar Enviar"
                lblMensaje.Text = ""

                Exit Sub
            End If
            '-------------------------------------------------------------------------

            'Cuando la operacion DESBLOQUEAR HORARIO: EnvioDirector=1 / EnvioDirPersonal=0
            If ddlEvaluacionHorario.SelectedValue = "D" Then
                ddlEstadoEnvioDirector.SelectedValue = 0
                ddlEstadoEnvioDirector.Enabled = False
                ddlEnvioDirPersonal.SelectedValue = 0
                ddlEnvioDirPersonal.Enabled = False

                lblAccion.ForeColor = Drawing.Color.Green
                lblAccion.Text = "DESBLOQUEA EL HORARIO DEL TRABAJADOR Y ANULA EL ENVIO DE CONFORMIDAD A PERSONAL."
                lblAccion.Font.Bold = True

                txtAsunto.Text = ""
                txtObservacion.Text = ""
                lblMensaje.Text = ""
                cmdEjecutar.Text = "Finalizar Enviar"
                Exit Sub
            End If
            '-------------------------------------------------------------------------

            If ddlEvaluacionHorario.SelectedValue <> "O" Then
                vEstadoObservacion(False)
                cmdEjecutar.Text = "Finalizar Enviar"
                txtObservacion.Text = ""
                txtAsunto.Text = ""
                lblAccion.Text = ""
                lblMensaje.Text = ""

                ddlEstadoEnvioDirector.SelectedValue = -1
                ddlEstadoEnvioDirector.Enabled = True
                ddlEnvioDirPersonal.SelectedValue = -1
                ddlEnvioDirPersonal.Enabled = True
            End If
            '---------------------------------------------------------------------------------------


        Catch ex As Exception
            Response.Write(ex.Message)
            Response.Write("<script>alert('Ocurrio un error al procesar los datos, intentelo nuevamente')</script>")
        End Try
    End Sub

    Private Function validaSeleccionItem() As Boolean

        Dim obj As New clsPersonal
        Dim Fila As GridViewRow
        Dim sw As Byte = 0
        Dim vCodigo_for As Integer

        For i As Integer = 0 To gvListaTrabajadores.Rows.Count - 1
            'Capturamos las filas que estan activas
            Fila = gvListaTrabajadores.Rows(i)
            Dim valor As Boolean = CType(Fila.FindControl("chkSel"), CheckBox).Checked
            If (valor = True) Then
                sw = 1
                vCodigo_for = Convert.ToInt32(gvListaTrabajadores.DataKeys(Fila.RowIndex).Value)
            End If
        Next

        If (sw = 1) Then
            Return True
        End If
        Return False
    End Function

    Protected Sub cmdEjecutar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdEjecutar.Click
        Try
            Dim idPer As Integer
            idPer = Request.QueryString("id")

            Me.lblMensaje.Text = ""

            '--- Si no pasa esta validacion, cortamos todo el proceso
            If (validaSeleccionItem() = False) Then
                'Response.Write("<script>alert('Debe Seleccionar por lo menos un trabajador, para ejecutar el proceso.')</script>")
                lblMensaje.Visible = True
                lblMensaje.ForeColor = Drawing.Color.Red
                lblMensaje.Text = "Debe Seleccionar por lo menos un trabajador y alguna de las operaciones, para ejecutar el proceso"
                Exit Sub
            End If

            If (ddlEvaluacionHorario.SelectedValue = "X") Then
                lblMensaje.Visible = True
                lblMensaje.ForeColor = Drawing.Color.Red
                lblMensaje.Text = "Debe calificar el horario para ejecutar el proceso"
                Exit Sub
            End If

            '----------------------------------------------------------------------------------------------------------------------


            'Proces para la Evaluacion de los horarios
            If ddlEvaluacionHorario.SelectedValue <> "X" Then
                '### CONFORME ###
                If ddlEvaluacionHorario.SelectedValue = "C" Then
                    If (validaCheckActivoConforme() = True) Then
                        txtObservacion.Text = ""
                        txtObservacion.Enabled = False
                        ddlEvaluacionHorario.SelectedValue = "X"
                        'Response.Write("<script>alert('Proceso Ejecutado Correctamente')</script>")
                        lblMensaje.Visible = True
                        lblMensaje.ForeColor = Drawing.Color.Blue
                        lblMensaje.Text = "Proceso Ejecutado Correctamente, para verificar los cambios actualize la página."
                    End If
                End If

                If ddlEvaluacionHorario.SelectedValue = "O" Then

                    If txtAsunto.Text.Trim = "" Then
                        'Response.Write("<script>alert('Ingrese la Observación')</script>")
                        lblMensaje.Visible = True
                        lblMensaje.ForeColor = Drawing.Color.Red
                        lblMensaje.Text = "Ingrese el Asunto del aviso"
                        Exit Sub
                    End If

                    If txtObservacion.Text.Trim = "" Then
                        'Response.Write("<script>alert('Ingrese la Observación')</script>")
                        lblMensaje.Visible = True
                        lblMensaje.ForeColor = Drawing.Color.Red
                        lblMensaje.Text = "Ingrese la Observación"
                        Exit Sub
                    End If
                    If (validaCheckActivoObservacion() = True) Then
                        lblMensaje.Visible = True
                        lblMensaje.ForeColor = Drawing.Color.Blue
                        lblMensaje.Text = "Proceso Ejecutado Correctamente, para verificar los cambios actualize la página."
                    End If
                End If

                'Enviar una mensaje de COMUNICADO al usuario, pero sin realizar ninguna operacion en concreto
                If ddlEvaluacionHorario.SelectedValue = "N" Then
                    If txtAsunto.Text.Trim = "" Then
                        'Response.Write("<script>alert('Ingrese la Observación')</script>")
                        lblMensaje.Visible = True
                        lblMensaje.ForeColor = Drawing.Color.Red
                        lblMensaje.Text = "Ingrese el Asunto del aviso"
                        Exit Sub
                    End If

                    If txtObservacion.Text.Trim = "" Then
                        'Response.Write("<script>alert('Ingrese la Observación')</script>")
                        lblMensaje.Visible = True
                        lblMensaje.ForeColor = Drawing.Color.Red
                        lblMensaje.Text = "Ingrese el Aviso"
                        Exit Sub
                    End If

                    If (validaCheckActivoComunicar() = True) Then
                        lblMensaje.Visible = True
                        lblMensaje.ForeColor = Drawing.Color.Blue
                        lblMensaje.Text = "Proceso Ejecutado Correctamente, para verificar los cambios actualize la página."
                    End If
                End If

            End If



            '---------------------------------------------------------------------------------------------------------------------------

            If (ddlEvaluacionHorario.SelectedValue <> "O" Or ddlEvaluacionHorario.SelectedValue <> "C" Or ddlEvaluacionHorario.SelectedValue <> "X") Then
                '---bloquear horario
                If ddlEvaluacionHorario.SelectedValue = "B" Then
                    If (validaCheckOperaciones(ddlEvaluacionHorario.SelectedValue) = True) Then
                        lblMensaje.Visible = True
                        lblMensaje.ForeColor = Drawing.Color.Blue
                        lblMensaje.Text = "Proceso Ejecutado Correctamente, para verificar los cambios actualize la página."
                    End If
                End If

                '--desbloquear horario
                If ddlEvaluacionHorario.SelectedValue = "D" Then
                    If (validaCheckOperaciones(ddlEvaluacionHorario.SelectedValue) = True) Then
                        lblMensaje.Visible = True
                        lblMensaje.ForeColor = Drawing.Color.Blue
                        lblMensaje.Text = "Proceso Ejecutado Correctamente, para verificar los cambios actualize la página."
                    End If
                End If
            End If

            'Proceso de Estado Planilla
            If ddlEstadoPlanilla.SelectedValue <> -1 Then
                If (validaCheckActivoEstados(1, ddlEstadoPlanilla.SelectedValue) = True) Then
                    lblMensaje.Visible = True
                    lblMensaje.ForeColor = Drawing.Color.Blue
                    lblMensaje.Text = "Proceso Ejecutado Correctamente, para verificar los cambios actualize la página."
                End If
            End If

            'Proceso EstadoCpus
            If ddlEstadoCampus.SelectedValue <> -1 Then
                If (validaCheckActivoEstados(2, ddlEstadoCampus.SelectedValue) = True) Then
                    lblMensaje.Visible = True
                    lblMensaje.ForeColor = Drawing.Color.Blue
                    lblMensaje.Text = "Proceso Ejecutado Correctamente, para verificar los cambios actualize la página."
                End If
            End If
            '---------------------------------------------------------------------------------------------------


            'Actualiza el estado de envio del trabajador
            If ddlEstadoEnvioDirector.SelectedValue <> -1 Then
                If (validaCheckActivoEstados(3, ddlEstadoEnvioDirector.SelectedValue) = True) Then
                    lblMensaje.Visible = True
                    lblMensaje.ForeColor = Drawing.Color.Blue
                    lblMensaje.Text = "Proceso Ejecutado Correctamente, para verificar los cambios actualize la página."
                End If
            End If


            'Actualiza el estado del envio del jefe al area del personal
            If ddlEnvioDirPersonal.SelectedValue <> -1 Then
                If (validaCheckActivoEstados(4, ddlEnvioDirPersonal.SelectedValue) = True) Then
                    lblMensaje.Visible = True
                    lblMensaje.ForeColor = Drawing.Color.Blue
                    lblMensaje.Text = "Proceso Ejecutado Correctamente, para verificar los cambios actualize la página."
                End If
            End If
            '-----------------------------------------------------------------------------------------------------

            'Cargamos el gridview
            'CargaGridPersonal(1, ddlEstadoHorario.SelectedValue.Trim, Me.ddlTipoPersonal.SelectedValue.Trim, Me.ddlDedicacion.SelectedValue.Trim, Me.ddlCentroCosto.SelectedValue)

            ddlEnvioDirPersonal.SelectedValue = -1
            ddlEstadoEnvioDirector.SelectedValue = -1
            ddlEstadoCampus.SelectedValue = -1
            ddlEstadoPlanilla.SelectedValue = -1
        Catch ex As Exception
            Response.Write(ex.Message)
            Response.Write("<script>alert('Ocurrio un error al procesar los datos, intentelo nuevamente')</script>")
        End Try
    End Sub

    Private Function validaCheckOperaciones(ByVal vTipo As String) As Boolean
        Dim obj As New clsPersonal
        Dim Fila As GridViewRow
        Dim sw As Byte = 0
        Dim vCodigo_per As Integer
        Dim Trabajador As String
        Dim dts As New Data.DataTable

        Dim idPer As Integer
        idPer = Request.QueryString("id")

        'Recuperamos el codigo del periodo vigente
        codigo_pel = obj.ConsultarPeridoLaborable


        For i As Integer = 0 To gvListaTrabajadores.Rows.Count - 1
            'Capturamos las filas que estan activas
            Fila = gvListaTrabajadores.Rows(i)
            Dim valor As Boolean = CType(Fila.FindControl("chkSel"), CheckBox).Checked
            If (valor = True) Then
                sw = 1
                'Con este codigo recuperamos los datos de una determinada celda de la fila
                'Me.lblDestinatario.Text = Me.gvVariable.Rows(i).Cells(1).Text

                'Seleccionaloes el codigo de la variable, para poder generar los valores 
                vCodigo_per = Convert.ToInt32(gvListaTrabajadores.DataKeys(Fila.RowIndex).Value)
                Trabajador = Me.gvListaTrabajadores.Rows(i).Cells(3).Text

                'Modifica estadohorario
                'obj.EnviarHorarioDirector(vCodigo_per, 0, ddlEvaluacionHorario.SelectedValue)

                obj.EjecutaOperacionesMasivas(vTipo, vCodigo_per, _
                                              ddlEstadoEnvioDirector.SelectedValue, _
                                              ddlEnvioDirPersonal.SelectedValue, _
                                              ddlEstadoPlanilla.SelectedValue, _
                                              ddlEstadoCampus.SelectedValue, _
                                              Request.QueryString("id"))

                ''Enviar mail de conformidad 
                'obj.AprobarHorario(vCodigo_per, Trabajador, idPer)
                ''Registramos en la bitacora 
                'obj.registrarBitacoraHorario(vCodigo_per, codigo_pel, idPer)

            End If
        Next

        If (sw = 1) Then
            Return True
        End If

        Return False
    End Function


    Private Function validaCheckActivoComunicar() As Boolean
        Dim obj As New clsPersonal
        Dim Fila As GridViewRow
        Dim sw As Byte = 0
        Dim vCodigo_per As Integer
        Dim Trabajador As String
        Dim dts As New Data.DataTable

        Dim idPer As Integer
        idPer = Request.QueryString("id")

        'Recuperamos el codigo del periodo vigente
        codigo_pel = obj.ConsultarPeridoLaborable


        For i As Integer = 0 To gvListaTrabajadores.Rows.Count - 1
            'Capturamos las filas que estan activas
            Fila = gvListaTrabajadores.Rows(i)
            Dim valor As Boolean = CType(Fila.FindControl("chkSel"), CheckBox).Checked
            If (valor = True) Then
                sw = 1

                'Seleccionaloes el codigo de la variable, para poder generar los valores 
                vCodigo_per = Convert.ToInt32(gvListaTrabajadores.DataKeys(Fila.RowIndex).Value)
                Trabajador = Me.gvListaTrabajadores.Rows(i).Cells(3).Text

                'Envia un Email de comunicado, y no realiza ninguna accion. 
                obj.EnviarEmailComunicado(vCodigo_per, txtObservacion.Text.Trim, Trabajador, idPer, txtAsunto.Text.Trim)

                If ddlEvaluacionHorario.SelectedValue = "N" Then
                    cmdEjecutar.Text = "Comunicar"
                Else

                    cmdEjecutar.Text = "Finalizar Enviar"
                End If

            End If
        Next

        If (sw = 1) Then
            Return True
        End If

        Return False
    End Function


    Private Function validaCheckActivoEstados(ByVal vTipo As Integer, ByVal vEstado As Integer) As Boolean
        Dim obj As New clsPersonal
        Dim Fila As GridViewRow
        Dim sw As Byte = 0
        Dim vCodigo_per As Integer


        For i As Integer = 0 To gvListaTrabajadores.Rows.Count - 1
            'Capturamos las filas que estan activas
            Fila = gvListaTrabajadores.Rows(i)
            Dim valor As Boolean = CType(Fila.FindControl("chkSel"), CheckBox).Checked
            If (valor = True) Then
                sw = 1
                vCodigo_per = Convert.ToInt32(gvListaTrabajadores.DataKeys(Fila.RowIndex).Value)
                obj.ActualizaEstadoPersonal(vTipo, vEstado, vCodigo_per, Request.QueryString("id"))

            End If
        Next

        If (sw = 1) Then
            Return True
        End If

        Return False
    End Function


    Private Function validaCheckActivoObservacion() As Boolean
        Dim obj As New clsPersonal
        Dim Fila As GridViewRow
        Dim sw As Byte = 0
        Dim vCodigo_per As Integer
        Dim Trabajador As String
        Dim dts As New Data.DataTable

        Dim idPer As Integer
        idPer = Request.QueryString("id")

        'Recuperamos el codigo del periodo vigente
        codigo_pel = obj.ConsultarPeridoLaborable


        For i As Integer = 0 To gvListaTrabajadores.Rows.Count - 1
            'Capturamos las filas que estan activas
            Fila = gvListaTrabajadores.Rows(i)
            Dim valor As Boolean = CType(Fila.FindControl("chkSel"), CheckBox).Checked
            If (valor = True) Then
                sw = 1

                'Seleccionaloes el codigo de la variable, para poder generar los valores 
                vCodigo_per = Convert.ToInt32(gvListaTrabajadores.DataKeys(Fila.RowIndex).Value)
                Trabajador = Me.gvListaTrabajadores.Rows(i).Cells(3).Text

                'Actualiza el estado_hop de horario
                obj.EnviarHorarioDirector(vCodigo_per, 0, ddlEvaluacionHorario.SelectedValue, Request.QueryString("id"))

                'Registramos en la bitacora 
                obj.registrarBitacoraHorario(vCodigo_per, codigo_pel, idPer)

                'Envia el correo de obsevacion - Anterior aqui no se especificaba el asunto.
                'obj.ObservarHorario(vCodigo_per, txtObservacion.Text.Trim, Trabajador, idPer)

                'Envia el correo de obsevacion
                'EnviarEmailTesis
                'obj.EnviarEmailObservacion(vCodigo_per, txtObservacion.Text.Trim, Trabajador, idPer, txtAsunto.Text.Trim)
                obj.EnviarEmailComunicado(vCodigo_per, txtObservacion.Text.Trim, Trabajador, idPer, txtAsunto.Text.Trim)


                If ddlEvaluacionHorario.SelectedValue = "O" Then
                    cmdEjecutar.Text = "Observar"
                Else

                    cmdEjecutar.Text = "Finalizar Enviar"
                End If

            End If
        Next

        If (sw = 1) Then
            Return True
        End If

        Return False
    End Function

    Private Function validaCheckActivoConforme() As Boolean
        Dim obj As New clsPersonal
        Dim Fila As GridViewRow
        Dim sw As Byte = 0
        Dim vCodigo_per As Integer
        Dim Trabajador As String
        Dim dts As New Data.DataTable

        Dim idPer As Integer
        idPer = Request.QueryString("id")

        'Recuperamos el codigo del periodo vigente
        codigo_pel = obj.ConsultarPeridoLaborable


        For i As Integer = 0 To gvListaTrabajadores.Rows.Count - 1
            'Capturamos las filas que estan activas
            Fila = gvListaTrabajadores.Rows(i)
            Dim valor As Boolean = CType(Fila.FindControl("chkSel"), CheckBox).Checked
            If (valor = True) Then
                sw = 1
                'Con este codigo recuperamos los datos de una determinada celda de la fila
                'Me.lblDestinatario.Text = Me.gvVariable.Rows(i).Cells(1).Text

                'Seleccionaloes el codigo de la variable, para poder generar los valores 
                vCodigo_per = Convert.ToInt32(gvListaTrabajadores.DataKeys(Fila.RowIndex).Value)
                Trabajador = Me.gvListaTrabajadores.Rows(i).Cells(3).Text

                'Modifica estadohorario
                'obj.EnviarHorarioDirector(vCodigo_per, 0, ddlEvaluacionHorario.SelectedValue)

                obj.EjecutaOperacionesMasivas("C", vCodigo_per, _
                                              ddlEstadoEnvioDirector.SelectedValue, _
                                              ddlEnvioDirPersonal.SelectedValue, _
                                              ddlEstadoPlanilla.SelectedValue, _
                                              ddlEstadoCampus.SelectedValue, _
                                              Request.QueryString("id"))

                'Enviar mail de conformidad 
                obj.AprobarHorario(vCodigo_per, Trabajador, idPer)
                'Registramos en la bitacora 
                obj.registrarBitacoraHorario(vCodigo_per, codigo_pel, idPer)

            End If
        Next

        If (sw = 1) Then
            Return True
        End If

        Return False
    End Function

    Protected Sub cmdEjecutar0_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdEjecutar0.Click
        Try
            Dim obj As New clsPersonal
            Dim Fila As GridViewRow
            Dim sw As Byte = 0
            Dim vCodigo_per As Integer
            Dim dts As New Data.DataTable


            For i As Integer = 0 To gvListaTrabajadores.Rows.Count - 1
                'Capturamos las filas que estan activas
                Fila = gvListaTrabajadores.Rows(i)
                Dim valor As Boolean = CType(Fila.FindControl("chkSel"), CheckBox).Checked
                If (valor = True) Then
                    sw = 1
                    'Con este codigo recuperamos los datos de una determinada celda de la fila
                    'Me.lblDestinatario.Text = Me.gvVariable.Rows(i).Cells(1).Text

                    'Seleccionaloes el codigo de la variable, para poder generar los valores 
                    vCodigo_per = Convert.ToInt32(gvListaTrabajadores.DataKeys(Fila.RowIndex).Value)
                    obj.HabilitarModificarHorarioPersonal(vCodigo_per, "P", Request.QueryString("id"))
                End If
            Next

            If (sw = 1) Then
                'Cargamos el gridview

                Dim envioDirector As String
                Dim envioDirPersonal As String


                If chkenvioDirector.Checked = True Then
                    envioDirector = "1"
                Else
                    envioDirector = "1"
                End If

                If chlenvioDirPersonal.Checked = True Then
                    envioDirPersonal = "1"
                Else
                    envioDirPersonal = "0"
                End If


                CargaGridPersonal(1, ddlEstadoHorario.SelectedValue.Trim, Me.ddlTipoPersonal.SelectedValue.Trim, Me.ddlDedicacion.SelectedValue.Trim, Me.ddlCentroCosto.SelectedValue, ddlEstadoPlanillaF.SelectedValue, Me.ddlEstadoCampusF.SelectedValue, envioDirector, envioDirPersonal)

                Response.Write("<script>alert('Proceso realizado correctamente.')</script>")
            End If

        Catch ex As Exception
            Response.Write(ex.Message)
            Response.Write("<script>alert('Ocurrio un error al procesar los datos, intentelo nuevamente')</script>")
        End Try
    End Sub


    Protected Sub ddlEstadoPlanillaF_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlEstadoPlanillaF.SelectedIndexChanged
        Try
            Dim envioDirector As String
            Dim envioDirPersonal As String

            lblPruebas.Text = ddlEstadoPlanillaF.SelectedValue


            If chkTodos.Checked = True Then

                If chkenvioDirector.Checked = True Then
                    envioDirector = "1"
                Else
                    envioDirector = "0"
                End If

                If chlenvioDirPersonal.Checked = True Then
                    envioDirPersonal = "1"
                Else
                    envioDirPersonal = "0"
                End If

                CargaGridPersonal(1, ddlEstadoHorario.SelectedValue.Trim, Me.ddlTipoPersonal.SelectedValue.Trim, Me.ddlDedicacion.SelectedValue.Trim, Me.ddlCentroCosto.SelectedValue, ddlEstadoPlanillaF.SelectedValue, Me.ddlEstadoCampusF.SelectedValue, envioDirector, envioDirPersonal)
            Else
                CargaGridPersonal(1, ddlEstadoHorario.SelectedValue.Trim, _
                                  Me.ddlTipoPersonal.SelectedValue.Trim, _
                                  Me.ddlDedicacion.SelectedValue.Trim, _
                                  Me.ddlCentroCosto.SelectedValue, _
                                  ddlEstadoPlanillaF.SelectedValue, _
                                  Me.ddlEstadoCampusF.SelectedValue, _
                                  "%", "%")
            End If

            Me.lblMensaje.Text = ""

        Catch ex As Exception
            Response.Write(ex.Message)
            Response.Write("<script>alert('Ocurrio un error al procesar los datos, intentelo nuevamente')</script>")
        End Try
    End Sub

    Protected Sub ddlEstadoCampusF_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlEstadoCampusF.SelectedIndexChanged
        Try
            Dim envioDirector As String
            Dim envioDirPersonal As String

            lblPruebas2.Text = ddlEstadoCampusF.SelectedValue

            If chkTodos.Checked = True Then

                If chkenvioDirector.Checked = True Then
                    envioDirector = "1"
                Else
                    envioDirector = "0"
                End If

                If chlenvioDirPersonal.Checked = True Then
                    envioDirPersonal = "1"
                Else
                    envioDirPersonal = "0"
                End If

                CargaGridPersonal(1, ddlEstadoHorario.SelectedValue.Trim, Me.ddlTipoPersonal.SelectedValue.Trim, Me.ddlDedicacion.SelectedValue.Trim, Me.ddlCentroCosto.SelectedValue, ddlEstadoPlanillaF.SelectedValue, Me.ddlEstadoCampusF.SelectedValue, envioDirector, envioDirPersonal)
            Else
                CargaGridPersonal(1, ddlEstadoHorario.SelectedValue.Trim, Me.ddlTipoPersonal.SelectedValue.Trim, Me.ddlDedicacion.SelectedValue.Trim, Me.ddlCentroCosto.SelectedValue, ddlEstadoPlanillaF.SelectedValue, Me.ddlEstadoCampusF.SelectedValue, "%", "%")
            End If


            Me.lblMensaje.Text = ""

        Catch ex As Exception
            Response.Write(ex.Message)
            Response.Write("<script>alert('Ocurrio un error al procesar los datos, intentelo nuevamente')</script>")
        End Try
    End Sub

    Protected Sub chkenvioDirector_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkenvioDirector.CheckedChanged
        If chkenvioDirector.Checked = True And chlenvioDirPersonal.Checked = False Then
            chkenvioDirector.Text = "Envió Trabajador"
            chkenvioDirector.ForeColor = Drawing.Color.Blue

            chlenvioDirPersonal.Text = "No Envió Director"
            chlenvioDirPersonal.ForeColor = Drawing.Color.Red

            CargaGridPersonal(1, ddlEstadoHorario.SelectedValue.Trim, Me.ddlTipoPersonal.SelectedValue.Trim, Me.ddlDedicacion.SelectedValue.Trim, Me.ddlCentroCosto.SelectedValue, ddlEstadoPlanillaF.SelectedValue, Me.ddlEstadoCampusF.SelectedValue, "1", "0")
        End If

        If chkenvioDirector.Checked = False And chlenvioDirPersonal.Checked = True Then
            chkenvioDirector.Text = "No Envió Trabajador"
            chkenvioDirector.ForeColor = Drawing.Color.Red

            chlenvioDirPersonal.Text = "Envió Director"
            chlenvioDirPersonal.ForeColor = Drawing.Color.Blue

            CargaGridPersonal(1, ddlEstadoHorario.SelectedValue.Trim, Me.ddlTipoPersonal.SelectedValue.Trim, Me.ddlDedicacion.SelectedValue.Trim, Me.ddlCentroCosto.SelectedValue, ddlEstadoPlanillaF.SelectedValue, Me.ddlEstadoCampusF.SelectedValue, "0", "1")
        End If

        If chkenvioDirector.Checked = False And chlenvioDirPersonal.Checked = False Then
            chkenvioDirector.Text = "No Envió Trabajador"
            chkenvioDirector.ForeColor = Drawing.Color.Red

            chlenvioDirPersonal.Text = "No Envió Director"
            chlenvioDirPersonal.ForeColor = Drawing.Color.Red


            CargaGridPersonal(1, ddlEstadoHorario.SelectedValue.Trim, Me.ddlTipoPersonal.SelectedValue.Trim, Me.ddlDedicacion.SelectedValue.Trim, Me.ddlCentroCosto.SelectedValue, ddlEstadoPlanillaF.SelectedValue, Me.ddlEstadoCampusF.SelectedValue, "0", "0")
        End If

        If chkenvioDirector.Checked = True And chlenvioDirPersonal.Checked = True Then
            chkenvioDirector.Text = "Envió Trabajador"
            chkenvioDirector.ForeColor = Drawing.Color.Blue

            chlenvioDirPersonal.Text = "Envió Director"
            chlenvioDirPersonal.ForeColor = Drawing.Color.Blue


            CargaGridPersonal(1, ddlEstadoHorario.SelectedValue.Trim, Me.ddlTipoPersonal.SelectedValue.Trim, Me.ddlDedicacion.SelectedValue.Trim, Me.ddlCentroCosto.SelectedValue, ddlEstadoPlanillaF.SelectedValue, Me.ddlEstadoCampusF.SelectedValue, "1", "1")
        End If

    End Sub

    Protected Sub chlenvioDirPersonal_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chlenvioDirPersonal.CheckedChanged
        If chkenvioDirector.Checked = True And chlenvioDirPersonal.Checked = False Then
            chkenvioDirector.Text = "Envió Trabajador"
            chkenvioDirector.ForeColor = Drawing.Color.Blue

            chlenvioDirPersonal.Text = "No Envió Director"
            chlenvioDirPersonal.ForeColor = Drawing.Color.Red

            CargaGridPersonal(1, ddlEstadoHorario.SelectedValue.Trim, Me.ddlTipoPersonal.SelectedValue.Trim, Me.ddlDedicacion.SelectedValue.Trim, Me.ddlCentroCosto.SelectedValue, ddlEstadoPlanillaF.SelectedValue, Me.ddlEstadoCampusF.SelectedValue, "1", "0")
        End If

        If chkenvioDirector.Checked = False And chlenvioDirPersonal.Checked = True Then
            chkenvioDirector.Text = "No Envió Trabajador"
            chkenvioDirector.ForeColor = Drawing.Color.Red

            chlenvioDirPersonal.Text = "Envió Director"
            chlenvioDirPersonal.ForeColor = Drawing.Color.Blue

            CargaGridPersonal(1, ddlEstadoHorario.SelectedValue.Trim, Me.ddlTipoPersonal.SelectedValue.Trim, Me.ddlDedicacion.SelectedValue.Trim, Me.ddlCentroCosto.SelectedValue, ddlEstadoPlanillaF.SelectedValue, Me.ddlEstadoCampusF.SelectedValue, "0", "1")
        End If

        If chkenvioDirector.Checked = False And chlenvioDirPersonal.Checked = False Then
            chkenvioDirector.Text = "No Envió Trabajador"
            chkenvioDirector.ForeColor = Drawing.Color.Red

            chlenvioDirPersonal.Text = "No Envió Director"
            chlenvioDirPersonal.ForeColor = Drawing.Color.Red


            CargaGridPersonal(1, ddlEstadoHorario.SelectedValue.Trim, Me.ddlTipoPersonal.SelectedValue.Trim, Me.ddlDedicacion.SelectedValue.Trim, Me.ddlCentroCosto.SelectedValue, ddlEstadoPlanillaF.SelectedValue, Me.ddlEstadoCampusF.SelectedValue, "0", "0")
        End If

        If chkenvioDirector.Checked = True And chlenvioDirPersonal.Checked = True Then
            chkenvioDirector.Text = "Envió Trabajador"
            chkenvioDirector.ForeColor = Drawing.Color.Blue

            chlenvioDirPersonal.Text = "Envió Director"
            chlenvioDirPersonal.ForeColor = Drawing.Color.Blue


            CargaGridPersonal(1, ddlEstadoHorario.SelectedValue.Trim, Me.ddlTipoPersonal.SelectedValue.Trim, Me.ddlDedicacion.SelectedValue.Trim, Me.ddlCentroCosto.SelectedValue, ddlEstadoPlanillaF.SelectedValue, Me.ddlEstadoCampusF.SelectedValue, "1", "1")
        End If
    End Sub

    Protected Sub chkTodos_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkTodos.CheckedChanged

        If chkTodos.Checked = True Then
            'Dim envioDirector As String
            'Dim envioDirPersonal As String

            chkenvioDirector.Visible = True
            chkenvioDirector.Checked = True
            chkenvioDirector.ForeColor = Drawing.Color.Blue

            chlenvioDirPersonal.Visible = True
            chlenvioDirPersonal.Checked = True
            chlenvioDirPersonal.ForeColor = Drawing.Color.Blue

            chkTodos.Text = "Deshabilitar Envios:"
            CargaGridPersonal(1, ddlEstadoHorario.SelectedValue.Trim, Me.ddlTipoPersonal.SelectedValue.Trim, Me.ddlDedicacion.SelectedValue.Trim, Me.ddlCentroCosto.SelectedValue, ddlEstadoPlanillaF.SelectedValue, Me.ddlEstadoCampusF.SelectedValue, "1", "1")

        Else
            chkenvioDirector.Visible = False
            chlenvioDirPersonal.Visible = False
            'chkTodos.Text = "Deshabilitar Envios:"
            chkTodos.Text = "Habilitar Envios:"
            CargaGridPersonal(1, ddlEstadoHorario.SelectedValue.Trim, Me.ddlTipoPersonal.SelectedValue.Trim, Me.ddlDedicacion.SelectedValue.Trim, Me.ddlCentroCosto.SelectedValue, ddlEstadoPlanillaF.SelectedValue, Me.ddlEstadoCampusF.SelectedValue, "%", "%")
        End If
    End Sub

    Protected Sub cmdExportar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdExportar.Click
        Try
            Dim sb As StringBuilder = New StringBuilder()
            Dim SW As System.IO.StringWriter = New System.IO.StringWriter(sb)
            Dim htw As HtmlTextWriter = New HtmlTextWriter(SW)
            Dim Page As Page = New Page()
            Dim form As HtmlForm = New HtmlForm()
            gvListaTrabajadores.EnableViewState = False
            Page.EnableEventValidation = False
            Page.DesignerInitialize()
            Page.Controls.Add(form)
            form.Controls.Add(Me.gvListaTrabajadores)
            Page.RenderControl(htw)
            Response.Clear()
            Response.Buffer = True
            Response.ContentType = "application/vnd.ms-excel"
            Response.AddHeader("Content-Disposition", "attachment;filename=ReporteHorario" & ".xls")
            Response.Charset = "UTF-8"
            Response.ContentEncoding = Encoding.Default
            Response.Write(sb.ToString())
            Response.End()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

End Class
