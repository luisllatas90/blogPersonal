
Partial Class administrativo_FrmCategorizarAlumno
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Me.HdModulo.Value = Request.QueryString("mod")
            'If ConsultarCeco() <> 0 Then
            ConsultarCeco()
            If IsPostBack = False Then
                CargarComboProcesosAdmision()
                CargarComboCeco()
                CargarComboModalidadIngreso()
                CargarComboFiltro()

                Me.HdUsuario.Value = Request.QueryString("id")

                Session("usuario") = Me.HdUsuario.Value
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Function ConsultarCeco() As Integer
        Try
            Dim obj As New ClsConectarDatos
            Dim tbl As Data.DataTable
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()

            'Me.HdModulo.Value = 0
            Me.HdModulo.Value = Request.QueryString("mod")

            tbl = obj.TraerDataTable("CEC_ConsultarPersonalEnCeCo", Request.QueryString("id"), "pensiones")

            If tbl.Rows(0).Item("codigo_Per") <> 0 Then
                Me.HdModulo.Value = 1
                cboImpresion.Enabled = True
                cboCategoriza.Enabled = True
                'btnExportar2.Visible = False
            End If

            tbl = obj.TraerDataTable("CEC_ConsultarPersonalEnCeCo", Request.QueryString("id"), "escuela pre")
            If tbl.Rows(0).Item("codigo_Per") <> 0 Then
                Me.HdModulo.Value = 2
                cboImpresion.Enabled = False
                cboCategoriza.Enabled = False
                'btnExportar2.Visible = True
            End If

            obj.CerrarConexion()
            obj = Nothing

            If HdModulo.Value = 1 Then
                cboImpresion.Enabled = True
                cboCategoriza.Enabled = True
            End If
            If HdModulo.Value = 2 Then
                cboImpresion.Enabled = False
                cboCategoriza.Enabled = False
            End If

            Return Me.HdModulo.Value

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Function

    Private Sub CargarComboProcesosAdmision()
        Dim objfun As New ClsFunciones
        Dim obj As New ClsConectarDatos
        Try
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            objfun.CargarListas(Me.cboCiclo, obj.TraerDataTable("ConsultarCicloAcademico", "CI2", ""), "cicloIng_Alu", "cicloIng_Alu", ">> Seleccione<<")

            obj.CerrarConexion()
            obj = Nothing
            objfun = Nothing
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub CargarComboCeco()
        Dim objfun As New ClsFunciones
        Dim obj As New ClsConectarDatos
        Try
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            objfun.CargarListas(Me.cboCentroCosto, obj.TraerDataTable("EVE_ConsultarCentroCostosXPermisos", Request.QueryString("ctf"), Request.QueryString("id"), "", 1), "codigo_Cco", "Nombre", ">> Seleccione<<")
            obj.CerrarConexion()
            obj = Nothing
            objfun = Nothing
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub CargarComboModalidadIngreso()
        Dim objfun As New ClsFunciones
        Dim obj As New ClsConectarDatos
        Try
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            objfun.CargarListas(Me.cboModalidad, obj.TraerDataTable("ConsultarModalidadIngreso", "TO", ""), "codigo_Min", "nombre_Min", ">> Seleccione<<")

            obj.CerrarConexion()
            obj = Nothing
            objfun = Nothing
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub CargarComboFiltro()
        Try

            If Me.HdModulo.Value = 1 Then      ' pensiones
                Me.cboAccion.Items.Add(New ListItem("Categorizar", "C"))
                Me.cboAccion.Items.Add(New ListItem("Imprimir Carta", "I"))
            End If

            If Me.HdModulo.Value = 2 Then      ' epu
                Me.cboAccion.Items.Add(New ListItem("Activar Ingreso", "AI"))
                Me.cboAccion.Items.Add(New ListItem("Retirar", "R"))
            End If

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub btnProcesar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnProcesar.Click
        Try
            'ValidarSeleccionGrid()

            'Dim objfun As New ClsFunciones
            Dim obj As New ClsConectarDatos
            Dim codigoAlu_fila As Integer
            Dim sbMensaje As New StringBuilder

            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString

            'obj.IniciarTransaccion()

            If Me.cboAccion.SelectedValue = "AI" Then
                For Each row As GridViewRow In Me.grwListaPersonas.Rows
                    codigoAlu_fila = Me.grwListaPersonas.DataKeys(row.RowIndex).Values("codigo_Alu")
                    Dim check As CheckBox = TryCast(row.FindControl("chkSel"), CheckBox)
                    Dim txtobs As TextBox = TryCast(row.FindControl("txtObservacion"), TextBox)
                    Dim txtnota As TextBox = TryCast(row.FindControl("txtNota"), TextBox)

                    obj.AbrirConexion()
                    If check.Checked Then
                        obj.Ejecutar("EPU_ActivarEstadoPostulacion", codigoAlu_fila, "I", txtobs.Text, txtnota.Text, Me.HdUsuario.Value, Request.UserHostAddress, Me.cboCiclo.Text)
                    ElseIf check.Checked = False And check.Visible = True Then
                        obj.Ejecutar("EPU_ActivarEstadoPostulacion", codigoAlu_fila, "P", txtobs.Text, txtnota.Text, Me.HdUsuario.Value, Request.UserHostAddress, "")
                    End If
                    obj.CerrarConexion()
                Next
            End If

            If Me.cboAccion.SelectedValue = "R" Then
                For Each row As GridViewRow In Me.grwListaPersonas.Rows
                    codigoAlu_fila = Me.grwListaPersonas.DataKeys(row.RowIndex).Values("codigo_Alu")
                    Dim check As CheckBox = TryCast(row.FindControl("chkSel"), CheckBox)
                    Dim txtobs As TextBox = TryCast(row.FindControl("txtObservacion"), TextBox)

                    obj.AbrirConexion()
                    If check.Checked Then
                        obj.Ejecutar("EPU_ActivarEstadoPostulacion", codigoAlu_fila, "R", txtobs.Text, Me.HdUsuario.Value, Request.UserHostAddress, "")
                    End If
                    obj.CerrarConexion()
                Next
            End If

            If Me.cboAccion.SelectedValue = "C" Then
                For Each row As GridViewRow In Me.grwListaPersonas.Rows
                    codigoAlu_fila = Me.grwListaPersonas.DataKeys(row.RowIndex).Values("codigo_Alu")
                    Dim check As CheckBox = TryCast(row.FindControl("chkSel"), CheckBox)
                    Dim txtcat As TextBox = TryCast(row.FindControl("txtCategorizacion"), TextBox)
                    Dim txtobs As TextBox = TryCast(row.FindControl("txtObservacion"), TextBox)

                    obj.AbrirConexion()
                    If check.Checked Then
                        'obj.Ejecutar("EPU_AsignarCategorizacion", codigoAlu_fila, CDec(txtcat.Text) / 20, txtobs.Text, Request.QueryString("id"), "Web Campus")
                    End If
                    obj.CerrarConexion()
                Next
            End If

            obj = Nothing

            'CargarInscritosConCargo()
        Catch ex As Exception
            Me.lblMensaje.Text = ex.Message
        End Try
    End Sub

    Private Sub CargarInscritosConCargo()
        Try
            Dim obj As New ClsConectarDatos

            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()

            'Envío mod para que solo liste alummos con cecos de escuela pre 
            'Si es Módulo de pensiones, filtra también por ddlimpresion y ddlcategorizado    
            HdModulo.Value = Request.QueryString("mod")

            If Me.HdModulo.Value = 1 Then 'pensiones
                Me.grwListaPersonas.DataSource = obj.TraerDataTable("EPRE_ListarPostulantes", IIf(Me.cboCiclo.SelectedValue = "-1", "%", Me.cboCiclo.SelectedValue), IIf(Me.cboCentroCosto.SelectedValue = -1, 0, Me.cboCentroCosto.SelectedValue), IIf(Me.cboModalidad.SelectedValue = -1, 0, Me.cboModalidad.SelectedValue), "", "", txtBusqueda.Text, Me.cboEstadoPostula.SelectedValue, Request.QueryString("mod"), 0, Me.cboCategoriza.SelectedValue, Me.cboImpresion.SelectedValue)
            ElseIf Me.HdModulo.Value = 2 Then 'escuela pre
                Me.grwListaPersonas.DataSource = obj.TraerDataTable("EPRE_ListarPostulantes", IIf(Me.cboCiclo.SelectedValue = "-1", "%", Me.cboCiclo.SelectedValue), IIf(Me.cboCentroCosto.SelectedValue = -1, 0, Me.cboCentroCosto.SelectedValue), IIf(Me.cboModalidad.SelectedValue = -1, 0, Me.cboModalidad.SelectedValue), "", "", txtBusqueda.Text, Me.cboEstadoPostula.SelectedValue, Request.QueryString("mod"), 0, "%", "%")
            End If
           
            grwListaPersonas.Columns(10).Visible = False
            If Me.HdModulo.Value = 1 Then
                grwListaPersonas.Columns(14).Visible = False
                grwListaPersonas.Columns(15).Visible = True
                grwListaPersonas.Columns(17).Visible = True
            ElseIf Me.HdModulo.Value = 2 Then
                grwListaPersonas.Columns(14).Visible = True
                grwListaPersonas.Columns(15).Visible = False
                grwListaPersonas.Columns(17).Visible = False
            End If

            Me.grwListaPersonas.DataBind()
            obj.CerrarConexion()
            obj = Nothing
        Catch ex As Exception
            Me.lblMensaje.Text = ex.Message
        End Try
    End Sub

    Protected Sub grwListaPersonas_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles grwListaPersonas.DataBound
        If Me.grwListaPersonas.Rows.Count Then

            For Each row As GridViewRow In grwListaPersonas.Rows
                Dim chk As CheckBox
                Dim txt As TextBox
                chk = TryCast(row.FindControl("chkSel"), CheckBox)
                txt = TryCast(row.FindControl("txtCategorizacion"), TextBox)

                If Me.cboCiclo.SelectedValue = "-1" Then
                    chk.Enabled = False
                Else

                    If grwListaPersonas.DataKeys(row.RowIndex).Values("imprimiocartacat_Dal") = 1 Then
                        row.Cells(16).Text = "Impresa"
                    ElseIf grwListaPersonas.DataKeys(row.RowIndex).Values("imprimiocartacat_Dal") = 0 Then
                        row.Cells(16).Text = "No Impresa"
                    End If

                    If Trim(grwListaPersonas.DataKeys(row.RowIndex).Values("EstadoPostulacion")) = "I" Then
                        row.Cells(11).Text = "Ingresante"
                    ElseIf Trim(grwListaPersonas.DataKeys(row.RowIndex).Values("EstadoPostulacion")) = "P" Then
                        row.Cells(11).Text = "Postulante"
                    ElseIf Trim(grwListaPersonas.DataKeys(row.RowIndex).Values("EstadoPostulacion")) = "R" Then
                        row.Cells(11).Text = "Retirado"
                    End If

                    If grwListaPersonas.DataKeys(row.RowIndex).Values("categorizado_Dal") = True _
                    Or grwListaPersonas.DataKeys(row.RowIndex).Values("categorizado_Dal") = 1 Then
                        row.Cells(17).Text = "Si"
                    ElseIf grwListaPersonas.DataKeys(row.RowIndex).Values("categorizado_Dal") = 0 _
                    Or grwListaPersonas.DataKeys(row.RowIndex).Values("categorizado_Dal") = False Then
                        row.Cells(17).Text = "No"
                    End If

                End If
            Next
        End If
    End Sub

    Protected Sub gvLista_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grwListaPersonas.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim fila As Data.DataRowView
            fila = e.Row.DataItem
            e.Row.Cells(0).Text = e.Row.RowIndex + 1
        End If
    End Sub

    Protected Sub btnBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBuscar.Click
        CargarInscritosConCargo()
    End Sub

    Private Sub ValidarSeleccionGrid()
        Try
            Dim sbMensaje As New StringBuilder

            'Dim chk As CheckBox = sender
            'Dim fila As GridViewRow = chk.NamingContainer
            Dim idalu_seleccionado As Integer
            Dim idalu_fila As Integer
            Dim codigopso_seleccionado As Integer
            Dim codigopso_fila As Integer
            Dim mensaje As String = ""
            Dim obj As New ClsConectarDatos
            Dim tbl As Data.DataTable
            Dim seleccionado As Boolean = 0            

            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()

            'Recorrer grid
            For Each fila As GridViewRow In Me.grwListaPersonas.Rows
                Dim chk As CheckBox = TryCast(fila.FindControl("chkSel"), CheckBox)
                idalu_seleccionado = Me.grwListaPersonas.DataKeys(fila.RowIndex).Values("codigo_Alu")
                codigopso_seleccionado = Me.grwListaPersonas.DataKeys(fila.RowIndex).Values("codigo_pso")

                If chk.Visible = True And chk.Checked Then
                    seleccionado = 1

                    '========================================================
                    'Validar un solo ingresante
                    '========================================================
                    If Me.grwListaPersonas.DataKeys(fila.RowIndex).Values("otroalu") = 1 Then

                        If Me.cboAccion.SelectedValue = "AI" Then
                            '________________________________________________________
                            '1. Verificar en la base de datos  
                            tbl = obj.TraerDataTable("EPRE_ListarAlumnosPorPersona", Me.cboCiclo.SelectedValue, fila.Cells(2).Text, "I", Request.QueryString("mod"), idalu_seleccionado)

                            If tbl.Rows.Count() > 0 Then

                                mensaje = mensaje & "No se puede " & _
                                         "asignar como Ingresante al participante Número " & _
                                         fila.RowIndex + 1 & " porque ya se ha elegido como ingresante" & _
                                         " en otra modalidad y/o proceso de admisión.<br />"
                                chk.Checked = False


                            End If
                        End If

                        '________________________________________________________
                        '2. Verificar en el grid, siempre y cuando siga marcado el check
                        If (chk.Checked) Then

                            For Each row As GridViewRow In Me.grwListaPersonas.Rows
                                idalu_fila = Me.grwListaPersonas.DataKeys(row.RowIndex).Values("codigo_Alu")
                                codigopso_fila = Me.grwListaPersonas.DataKeys(row.RowIndex).Values("codigo_pso")

                                If codigopso_seleccionado = codigopso_fila And idalu_seleccionado <> idalu_fila Then
                                    Dim check As CheckBox = TryCast(row.FindControl("chkSel"), CheckBox)

                                    'Verificar que no hay otra fila con el mismo codigo_pso marcada como ingresante
                                    If check.Checked Then
                                        mensaje = mensaje & "No se puede " & _
                                        "asignar como Ingresante al participante Número " & _
                                        fila.RowIndex + 1 & " porque ya se ha elegido como ingresante" & _
                                        " en otra modalidad y/o proceso de admisión.<br />"

                                        chk.Checked = False
                                    End If
                                End If
                            Next
                        End If
                    End If


                    '========================================================
                    'Verificar que no se pueda volver a categorizar cuando tenga carta impresa o cuando el participate esté como postulante
                    '========================================================
                    If Me.cboAccion.SelectedValue = "C" Then
                        If chk.Checked Then
                            seleccionado = 1
                            If Me.grwListaPersonas.DataKeys(fila.RowIndex).Values("imprimiocartacat_Dal") = 1 Then
                                mensaje = mensaje & _
                                    "No se puede volver a categorizar al participante Número " & _
                                    fila.RowIndex + 1 & _
                                    ", pues ya cuenta con carta de categorización impresa.<br />"

                                chk.Checked = False
                            End If

                            If Me.grwListaPersonas.DataKeys(fila.RowIndex).Values("EstadoPostulacion") = "P" Then
                                mensaje = mensaje & _
                                    "No se puede categorizar al participante Número " & _
                                    fila.RowIndex + 1 & _
                                    " porque no es Ingresante.<br />"

                                chk.Checked = False
                            End If
                        End If
                    End If
                End If

            Next

            Me.lblMensaje.Text = mensaje
        Catch ex As Exception
            Me.lblMensaje.Text = ex.Message
        End Try
    End Sub



   
End Class
