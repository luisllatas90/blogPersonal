'22/12/2014 treyes Solicitud de Anulacion
Partial Class administrativo_pec_frmSolicitarAnulacion
    Inherits System.Web.UI.Page
    Dim Total As Decimal

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            'Response.Write(Request.QueryString("participante"))
            Me.lblTitulo.Text = "Solicitud de Anulación : " & Request.QueryString("participante")
            Call ConsultarCargosAbonosPersona()

            If Me.gvResultado.Rows.Count = 0 Then
                Me.lblmensaje.Text = "Saldo en cero, no hay información para mostrar"
            End If
            CargaComboMotivo()
        End If
    End Sub

    Protected Sub gvResultado_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvResultado.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim cantidad As New TextBox
            Dim obj As New ClsConectarDatos
            Dim datos As New Data.DataTable

            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            obj.CerrarConexion()

            Response.Write("EVE_ConsultarSolicitudAnulacion " & Request.QueryString("pso"))
            datos = obj.TraerDataTable("EVE_ConsultarSolicitudAnulacion", Request.QueryString("pso"))

            For I As Int16 = 0 To datos.Rows.Count - 1
                If datos.Rows(I)(0).ToString.Trim = Me.gvResultado.DataKeys.Item(e.Row.RowIndex).Values("codigo_Deu").ToString.Trim And (datos.Rows(I)(1).ToString.Trim = "0") Then
                    CType(e.Row.FindControl("txtCantidad"), TextBox).Text = datos.Rows(I)(2).ToString
                    If datos.Rows(I)(1).ToString.Trim = "0" Then 'Solicitud de anulacion generada
                        CType(e.Row.FindControl("txtCantidad"), TextBox).BackColor = System.Drawing.Color.Orange
                        CType(e.Row.FindControl("txtCantidad"), TextBox).Enabled = False
                        Me.lblInformacion.Text = "Deuda tiene solicitud de anulación"
                    End If
                End If
            Next

            If CType(e.Row.FindControl("txtCantidad"), TextBox).Text = "" Then
                CType(e.Row.FindControl("txtCantidad"), TextBox).Text = e.Row.Cells(6).Text
                cantidad = CType(e.Row.FindControl("txtCantidad"), TextBox)
                Total += CType(cantidad.Text, Decimal)
                btnAnular.Enabled = True
            End If
            datos.Dispose()

        ElseIf e.Row.RowType = DataControlRowType.Footer Then
            e.Row.Cells(0).Text = "Total"
            CType(e.Row.FindControl("txtTotal"), TextBox).Enabled = False
            CType(e.Row.FindControl("txtTotal"), TextBox).Text = Total.ToString

        End If
    End Sub
    Private Sub CargaComboMotivo()
        Dim obj As New ClsConectarDatos
        Dim dtMotivo As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString 'cambiar
        obj.AbrirConexion()
        dtMotivo = obj.TraerDataTable("EVE_ListarMotivoAnulacion")
        Me.cboMotivo.DataSource = dtMotivo
        Me.cboMotivo.DataValueField = "codigo_Mno"
        Me.cboMotivo.DataTextField = "descripcion_Mno"
        Me.cboMotivo.DataBind()
        obj.CerrarConexion()
        dtMotivo.Dispose()
    End Sub

    Private Sub ConsultarCargosAbonosPersona()
        Dim objfun As New ClsFunciones
        Dim obj As New ClsConectarDatos
        Dim datos As New Data.DataTable

        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()

        'Cargar Datos Centro Costos

        'Response.Write("EVE_ConsultarCargosPersona " & Request.QueryString("pso") & "," & Request.QueryString("cco"))


        datos = obj.TraerDataTable("EVE_ConsultarCargosPersona", Request.QueryString("pso"), Request.QueryString("cco"))

        'Response.Write("  Numero de Registros" & datos.Rows.Count.ToString)
        If datos.Rows.Count > 0 Then
            gvResultado.DataSource = datos
            gvResultado.DataBind()
        End If
        
        datos.Dispose()
        obj.CerrarConexion()
    End Sub

    Protected Sub btnAnular_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAnular.Click
        Dim Fila As GridViewRow
        Dim obj As New ClsConectarDatos
        Dim Suma As Decimal
        Dim codigo_San(1) As String
        Dim msj_Dsa(1) As String
        Dim datos As New Data.DataTable
        Dim codigo_usu As Integer = Request.QueryString("id")

        For I As Int16 = 0 To Me.gvResultado.Rows.Count - 1
            Fila = Me.gvResultado.Rows(I)
            If Fila.RowType = DataControlRowType.DataRow Then
                If (CType(Fila.FindControl("txtCantidad"), TextBox).text <> "" And CType(Fila.FindControl("txtCantidad"), TextBox).Enabled = True) Then
                    Suma += CType(CType(Fila.FindControl("txtCantidad"), TextBox).text, Decimal)
                End If
            End If
        Next

        If Suma > 0 Then

            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString 'cambiar
            obj.IniciarTransaccion()
            '=========================================
            'Guardar Solicitud de Anulacion y Detalle
            '=========================================

            obj.Ejecutar("EVE_RegistrarSolicitudAnulacion", Request.QueryString("pso"), Me.cboMotivo.SelectedValue, Me.txtObservacion.text, Suma, codigo_usu, 0).copyto(codigo_San, 0)

            If (codigo_San(0).ToString = "0") Then
                obj.AbortarTransaccion()
                Me.lblmensaje.Text = "Ocurrió un error al registrar la Solicitud de Anulación. Contáctese con desarrollosistemas@usat.edu.pe"
                Exit Sub
            End If

            For I As Int16 = 0 To Me.gvResultado.Rows.Count - 1
                Fila = Me.gvResultado.Rows(I)
                If Fila.RowType = DataControlRowType.DataRow And CType(Fila.FindControl("txtCantidad"), TextBox).Enabled = True Then
                    If CType(Fila.FindControl("txtCantidad"), TextBox).text <> "" Then
                        If CType(CType(Fila.FindControl("txtCantidad"), TextBox).text, Decimal) > 0 Then
                            obj.Ejecutar("EVE_RegistrarDetalleSolicitudAnulacion", codigo_San(0), Me.gvResultado.DataKeys.Item(Fila.RowIndex).Values("codigo_Deu"), Fila.Cells(3).Text, CType(CType(Fila.FindControl("txtCantidad"), TextBox).text, Decimal), codigo_usu, 0).copyto(msj_Dsa, 0)
                            If (msj_Dsa(0).ToString = "0") Then
                                obj.AbortarTransaccion()
                                Me.lblmensaje.Text = "Ocurrió un error al registrar la Solicitud de Anulación. Contáctese con desarrollosistemas@usat.edu.pe"
                                Exit Sub
                            End If
                        End If
                    End If
                End If
            Next
            obj.TerminarTransaccion()
            obj = Nothing
            Response.Write("<script languaje='javascript'>self.parent.tb_remove();</script>")
            Response.Write("<script languaje='javascript'>alert('La Solicitud de Anulación se registró correctamente');</script>")
        End If
    End Sub

End Class