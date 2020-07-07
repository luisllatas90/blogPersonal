
Partial Class registronotasmodulopec
    Inherits System.Web.UI.Page
    Dim a, d, p As Int16
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            'Habilitar controles
            Me.dpProfesor.Visible = True
            Me.lblProfesor.Visible = False
            Me.txtInicio.Enabled = True
            Me.txtFin.Enabled = True
            'Me.grwParticipantes.Columns(7).Visible = True 'Mostrar Columna Retirar
            MostrarDatosModulo()
        End If
    End Sub

    Protected Sub grwParticipantes_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grwParticipantes.RowCommand
        If e.CommandName = "Modificar" Then

            'Convert.ToString(Me.grwPEC.DataKeys(Convert.ToInt32(e.CommandArgument)).Value)
            Page.RegisterStartupScript("Retiro", "<script>alert('Necesita solicitar acceso a ViceRectorado Académico para cambiar Notas')</script>")
        End If

        If e.CommandName = "Retirar" Then
            Page.RegisterStartupScript("Retiro", "<script>alert('No está habilitado el acceso para realizar retiros ')</script>")
        End If
    End Sub

    Protected Sub grwParticipantes_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grwParticipantes.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim fila As Data.DataRowView
            fila = e.Row.DataItem

            e.Row.Cells(0).Text = e.Row.RowIndex + 1
            e.Row.Attributes.Add("OnMouseOver", "Resaltar(1,this,'S')")
            e.Row.Attributes.Add("OnMouseOut", "Resaltar(0,this,'S')")

            '========================================
            'Validar Si está RETIRADO
            '========================================
            If fila("estado_dma").ToString = "R" Then
                CType(e.Row.Cells(4).FindControl("txtNota"), TextBox).Visible = False 'Ocultar casilla
                CType(e.Row.Cells(4).FindControl("lblNota"), Label).Visible = True 'Ocultar casilla
                'e.Row.Cells(6).Text = "" 'Ocultar botón Edición
            ElseIf fila("condicion_dma").ToString = "P" Then
                CType(e.Row.Cells(4).FindControl("txtNota"), TextBox).Visible = True 'Ocultar casilla
                CType(e.Row.Cells(4).FindControl("lblNota"), Label).Visible = False 'Ocultar casilla
                p = p + 1
            Else
                '========================================
                'Asignar CSS a las notas registradas
                '========================================
                If fila("condicion_dma").ToString = "A" Then
                    a = a + 1
                    e.Row.Cells(5).ForeColor = Drawing.Color.Blue
                Else
                    d = d + 1
                    e.Row.Cells(5).ForeColor = Drawing.Color.Red
                End If

                CType(e.Row.Cells(4).FindControl("txtNota"), TextBox).Visible = False 'Ocultar casilla
                CType(e.Row.Cells(4).FindControl("lblNota"), Label).Visible = True 'Ocultar casilla
                'e.Row.Cells(7).Text = "" 'Ocultar botón Retiro
            End If
        End If
    End Sub

    Protected Sub cmdGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdGuardar.Click
        Dim I As Integer
        Dim NotasPendientes As Double 'Almacenar si guardan notas pendientes
        Dim Fila As GridViewRow
        Dim obj As New ClsConectarDatos
        Dim codigo_usu As Integer = Request.QueryString("id")
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        Try
            If Me.dpProfesor.SelectedValue.ToString = "-1" Then
                Page.RegisterStartupScript("CompletarDatos", "<script>alert('Debe seleccionar el Profesor principal que desarrolló la asignatura')</script>")
                Exit Sub
            End If

            obj.AbrirConexion()
            '========================================
            'Guardar CUP si su estadoNOTA es P
            '========================================
            If Me.dpProfesor.Visible = True Then
                obj.Ejecutar("PEC_ActualizarDatosModuloVersionPEC", Request.QueryString("c"), Request.QueryString("p"), Me.txtInicio.Text, Me.txtFin.Text, Me.dpProfesor.SelectedValue, codigo_usu)
            End If
            '========================================
            'Guardar Notas de participantes
            '========================================
            For I = 0 To Me.grwParticipantes.Rows.Count - 1
                Fila = Me.grwParticipantes.Rows(I)
                If Fila.RowType = DataControlRowType.DataRow Then
                    Dim notafinal As Double
                    notafinal = CType(Fila.FindControl("txtNota"), TextBox).Text

                    obj.Ejecutar("PEC_AgregarNotasModuloVersionPEC", Me.grwParticipantes.DataKeys.Item(Fila.RowIndex).Values("codigo_dma"), Request.QueryString("c"), Request.QueryString("p"), Me.grwParticipantes.DataKeys.Item(Fila.RowIndex).Values("codigo_alu"), notafinal, codigo_usu)
                    If notafinal = -1 Then
                        NotasPendientes = NotasPendientes + 1
                    End If
                End If
            Next

            '======================================================
            'Actualizar EstadoNota_cup: Si se han registrado notas
            '======================================================
            If NotasPendientes <> Me.grwParticipantes.Rows.Count Then
                obj.Ejecutar("ActualizarEstadoCursoProgramado", "R", Request.QueryString("c"), codigo_usu)
            End If
            obj.CerrarConexion()
            obj = Nothing

            Me.MostrarDatosModulo()

            'Cerrar la página
            'Page.RegisterStartupScript("CerrarVentana", "<script>window.close();</script>")
        Catch ex As Exception
            obj = Nothing
            Me.lblmensaje.Text = "Ocurrió un error en el sistema. Contáctese con desarrollosistemas@usat.edu.pe. Envíe el siguiente texto:<Br/>" & ex.Message
        End Try
    End Sub

    Private Sub MostrarDatosModulo()
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Dim tblmodulopec As Data.DataTable
        tblmodulopec = obj.TraerDataTable("PEC_ConsultarProgramacionPEC", 1, Request.QueryString("p"), Request.QueryString("c"), 0)

        If tblmodulopec.Rows.Count > 0 Then
            If tblmodulopec.Rows(0).Item("estadonota_cup") = "P" Then
                '================================================
                'Mostrar profesores para Asignar
                '================================================
                ClsFunciones.LlenarListas(Me.dpProfesor, obj.TraerDataTable("ConsultarPersonal", "PFC", 1), "codigo_per", "personal", "--Seleccione--")
                'Me.grwParticipantes.Columns(6).Visible = False ''Quitar columna de edición de NOTA
            Else
                Me.lblProfesor.Text = tblmodulopec.Rows(0).Item("profesor")
                'Me.lblmensaje.Text = "<li>Para modificar Notas, haga clic en el ícono de la columna 'Modificar'</li>"
                'Me.grwParticipantes.Columns(6).Visible = True
                Me.lblProfesor.Visible = True
                Me.dpProfesor.Visible = False
                Me.txtInicio.Enabled = False
                Me.txtFin.Enabled = False
            End If
            'Mostrar datos del módulo
            Me.lblDescripcion_pes.Text = tblmodulopec.Rows(0).Item("descripcion_pes")
            Me.lblnombre_cur.Text = tblmodulopec.Rows(0).Item("nombre_cur")
            Me.txtInicio.Text = tblmodulopec.Rows(0).Item("fechainicio_cup")
            Me.txtFin.Text = tblmodulopec.Rows(0).Item("fechafin_cup")
            Me.dpProfesor.SelectedValue = tblmodulopec.Rows(0).Item("codigo_per")
            Me.hdestadonota_cup.Value = tblmodulopec.Rows(0).Item("estadonota_cup")

            'Mostrar datos del curso
            Page.Title = "Acta de Registro de Notas: " & Me.lblnombre_cur.Text.ToString

            'Mostrar participantes
            Me.grwParticipantes.DataSource = obj.TraerDataTable("PEC_NotasFinalesModulosPEC", Request.QueryString("p"), Request.QueryString("c"))
            Me.grwParticipantes.DataBind()
        End If
        tblmodulopec.Dispose()
        obj.CerrarConexion()
        obj = Nothing

        'Mostrar Botón guardar sólo si hay notas pendientes por asignar
        Me.cmdGuardar.Visible = (p > 0)
        'Mostrar totales
        Me.lblP.Text = "Pendientes: " & p.ToString
        Me.lblA.Text = "Aprobados: " & a.ToString
        Me.lblD.Text = "Desaprobados: " & d.ToString
    End Sub
End Class