Partial Class librerianet_frmpersona
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            Dim usuario As String = Request.QueryString("op")
            Dim pec As String = Request.QueryString("pec")
            Dim cco As String = Request.QueryString("cco")
            Dim tbl As Data.DataTable
            obj.AbrirConexion()
            'Si no hay centro de costos consultar en la base de datos por el Plan: PEC
            tbl = obj.TraerDataTable("PEC_ConsultarProgramaEC", 11, pec, 0, 0)
            If tbl.Rows.Count > 0 Then
                cco = tbl.Rows(0).Item("codigo_cco")
                Me.lblCentroCosto.Text = tbl.Rows(0).Item("descripcion_cco")
                Me.hdcodigo_cco.Value = tbl.Rows(0).Item("codigo_cco")
                Me.hcodigo_cac.Value = tbl.Rows(0).Item("codigo_cac")

                tbl.Dispose()
                'Si hay cco asignado
                ClsFunciones.LlenarListas(Me.dpprovincia, obj.TraerDataTable("ConsultarLugares", 3, 13, 0), "codigo_pro", "nombre_pro", "--Seleccione--")

                'Cargar los datos del alumno, siempre y cuando se quieran modificar.
                Dim tipo As String = Request.QueryString("t") 'tipo: E=Estudiante, P=Personal; O=Otros
                Dim cli As String = Request.QueryString("cl") 'Id del estudiante, persona u otro
                Dim pso As String = Request.QueryString("pso") 'Id de persona


                Me.grwServicios.DataSource = obj.TraerDataTable("TSO_ConsultarItemsCentroCostos", 1, cco, tipo, cli)
                Me.grwServicios.DataBind()
                Me.cmdGuardar.Enabled = Me.grwServicios.Rows.Count > 0

                'Cargar datos del operador
                tbl = obj.TraerDataTable("ConsultarPersonal", "UN", usuario)
                If tbl.Rows.Count > 0 Then
                    Me.hdlogin_per.Value = Mid(tbl.Rows(0).Item("login_per"), 6, 50)
                Else
                    Page.RegisterStartupScript("Error", "<script>alert('Verifique los items (servicios) asociados al Centro de Costos. Consulte en la Of. de Contabilidad (Sr. Miguel Rentería)')</script>")
                    Exit Sub
                End If
                tbl.Dispose()

                ''Si se modificarán los datos de la persona --->TEMPORAL
                If Request.QueryString("accion") = "A" Then
                    Me.tblPersona.Visible = True
                Else
                    Me.tblPersona.Visible = False
                End If
            Else
                Page.RegisterStartupScript("Erro", "<script>alert('Verifique la creación del Centro de Costos asociados al Programa. Caso contrario consultar con el Sr. Miguel Rentería (Contabilidad)')</script>")
            End If
            obj.CerrarConexion()
            obj = Nothing
        End If
    End Sub
    Protected Sub cmdGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdGuardar.Click
        Dim I As Integer
        Dim Fila As GridViewRow
        Dim valoresdevueltos(2) As Integer
        Dim mensaje(1) As String
        Dim usuario As String = Request.QueryString("op")
        Dim cco As String = Request.QueryString("cco")
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        'Try
        obj.AbrirConexion()
        'Grabar primero a la persona
        'obj.Ejecutar("PSO_Agregarpersona", UCase(Me.txtAPaterno.Text.Trim), UCase(Me.txtAMaterno.Text.Trim), UCase(Me.txtNombres.Text.Trim), CDate(Me.txtFechaNac.Text.Trim), Me.dpSexo.SelectedValue, Me.dpTipo.SelectedValue, Me.txtdni.Text.Trim, LCase(Me.txtemail1.Text.Trim), LCase(Me.txtemail2.Text.Trim), UCase(Me.txtdireccion.Text.Trim), Me.dpdistrito.SelectedValue, Me.txttelefono.Text.Trim, Me.txtcelular.Text.Trim, usuario, 0).copyto(valoresdevueltos, 0)

        ''If valoresdevueltos(0) = -1 Then
        ''    Me.lblmensaje.Text = "Ocurrió un error al registrar los datos Persona. Contáctese con desarrollosistemas@usat.edu.pe"
        ''    Exit Sub
        ''End If
        Dim alumno As Integer = Request.QueryString("cl")
        Dim personal As Integer
        Dim otro As Integer
        ''Grabar al estudiante
        'obj.Ejecutar("PEC_AgregarMatriculaPEC", Request.QueryString("pec"), UCase(Me.txtAPaterno.Text.Trim), UCase(Me.txtAMaterno.Text.Trim), UCase(Me.txtNombres.Text.Trim), Me.dpSexo.SelectedValue, Me.GeneraClave(), usuario, valoresdevueltos(0), "").copyto(mensaje, 0)
        'If mensaje(0).ToString <> "" Then
        '    Me.lblmensaje.Text = mensaje(0)
        'End If
        valoresdevueltos(0) = 0 'Temporal

        'Verificar marcados
        Dim elegidos As Int16 = 0
        For I = 0 To Me.grwServicios.Rows.Count - 1
            Fila = Me.grwServicios.Rows(I)
            If Fila.RowType = DataControlRowType.DataRow Then
                If CType(Fila.FindControl("chkElegir"), CheckBox).Checked = True Then
                    elegidos = elegidos + 1
                End If
            End If
        Next
        If elegidos = 0 Then
            Page.RegisterStartupScript("Error", "<script>alert('Debe elegir los items que desea registrar');</script>")
            Exit Sub
        End If

        'Grabar los servicios de la persona asignada
        For I = 0 To Me.grwServicios.Rows.Count - 1
            Fila = Me.grwServicios.Rows(I)
            If Fila.RowType = DataControlRowType.DataRow Then
                If CType(Fila.FindControl("chkElegir"), CheckBox).Checked = True Then
                    '==================================
                    ' Guardar los datos
                    '==================================
                    Dim monto As Double = Fila.Cells(2).Text
                    Dim generamora As Boolean
                    Dim fechavencimiento, fechainicio As Date
                    Dim nropartes As Int16 = IIf(Fila.Cells(5).Text <> "", Fila.Cells(5).Text, 1)
                    Dim codigo_sco As Integer = Me.grwServicios.DataKeys.Item(Fila.RowIndex).Values("codigo_sco")

                    generamora = IIf(Fila.Cells(6).Text = "", 0, 1)
                    fechavencimiento = CType(Fila.FindControl("txtfechavencimiento"), TextBox).Text.Trim
                    If IsDate(fechavencimiento) = False Then fechavencimiento = Now.Date.ToShortDateString

                    fechainicio = CType(Fila.FindControl("txtfechainiciocobro"), TextBox).Text.Trim
                    If IsDate(fechainicio) = False Then fechainicio = Now.Date.ToShortDateString

                    obj.Ejecutar("AgregarDeuda_v2", Now.Date.ToShortDateString, "", Request.QueryString("t"), alumno, DBNull.Value, DBNull.Value, _
                                                           codigo_sco, Me.hcodigo_cac.Value, "Mod. PEC por " & Me.hdlogin_per.Value, _
                                                           monto, "S", monto, "P", generamora, _
                                                           fechavencimiento, Me.hdcodigo_cco.Value, _
                                                           1, DBNull.Value, DBNull.Value, 0, 1, nropartes, fechainicio, 0).copyto(valoresdevueltos, 0)

                    If valoresdevueltos(1) = -1 Then
                        Me.lblmensaje.Text = "Ocurrió un error al registrar los datos de Pago. Contáctese con desarrollosistemas@usat.edu.pe"
                        Exit Sub
                    End If
                    'Registrar en bitácora
                    Dim StrBitacora As String
                    Dim cliente As String
                    If alumno <> 0 Then cliente = alumno
                    If personal <> 0 Then cliente = personal
                    If otro <> 0 Then cliente = otro

                    StrBitacora = "TipoCliente=" & Request.QueryString("t") & "||codigo_cli=" & cliente & "||codigo_sco=" & codigo_sco & "||total=" & monto

                    obj.Ejecutar("RegistrarBitacoraCaja", "DEUDA", valoresdevueltos(1), "AGREGAR", "P", Request.QueryString("op"), "", StrBitacora, "MOD-PEC Caja||frmpersona||" & Me.hdlogin_per.Value)

                End If
            End If
        Next
        obj.CerrarConexion()
        obj = Nothing

        Dim pagina As String

        If Request.QueryString("accion") = "A" Then
            pagina = "location.href=registromatriculapec.aspx?id=" & usuario & "&ctf=" & Request.QueryString("ctf")
        Else
            pagina = "self.parent.tb_remove();"
        End If

        Page.RegisterStartupScript("CambioEstado", "<script>alert('Se hn guardar los datos correctamente');" & pagina & "</script>")

        'Catch ex As Exception
        '    'obj.AbortarTransaccion()
        '    Me.cmdGuardar.Visible = False
        '    Me.lblmensaje.Text = "Ocurrió un Error al Registrar el estado. Intente mas tarde." & Chr(13) & ex.Message
        '    obj = Nothing
        'End Try
    End Sub
    Protected Sub grwServicios_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grwServicios.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim fila As Data.DataRowView
            fila = e.Row.DataItem
            e.Row.Attributes.Add("OnMouseOver", "Resaltar(1,this,'S')")
            e.Row.Attributes.Add("OnMouseOut", "Resaltar(0,this,'S')")

            'Obtener el abono realizado
            If fila.Row("montototal_deu").ToString <> "" Then
                e.Row.Cells(9).Text = fila.Row("montototal_deu") - fila.Row("saldo_deu")
                e.Row.Cells(0).Text = ""
            End If

            'Validar valores nulos
            If fila.Row("fechainicio_sco").ToString = "" Then
                e.Row.Cells(4).Text = Now.Date.ToShortDateString
            End If
        End If
    End Sub
    Private Function GeneraLetra() As String
        GeneraLetra = Chr(((Rnd() * 100) Mod 25) + 65)
    End Function
    Private Function GeneraClave() As String
        Dim Letras As String
        Dim Numeros As String
        Letras = GeneraLetra() & GeneraLetra()
        Numeros = Format((Rnd() * 8888) + 1111, "0000")
        GeneraClave = Letras & Numeros
    End Function
    Protected Sub dpprovincia_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dpprovincia.SelectedIndexChanged
        Me.dpdistrito.Items.Clear()
        If Me.dpprovincia.SelectedValue <> -1 Then
            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            ClsFunciones.LlenarListas(Me.dpdistrito, obj.TraerDataTable("ConsultarLugares", 4, Me.dpprovincia.SelectedValue, 0), "codigo_dis", "nombre_dis", "--Seleccione--")
            obj.CerrarConexion()
            obj = Nothing
        End If
    End Sub
End Class