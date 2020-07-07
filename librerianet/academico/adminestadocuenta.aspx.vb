﻿'----------------------------------------------------------------------
'Fecha: 30.10.2012
'Usuario: gcastillo
'Motivo: Cambio de URL del servidor de la WebUSAT
'----------------------------------------------------------------------
Partial Class librerianet_academico_adminestadocuenta
    Inherits System.Web.UI.Page
    Dim Cargo As Double
    Dim Pago As Double
    Dim Saldo As Double
    Dim Mora As Double
    Dim SubTotal As Double
    Dim vencidas As Double

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'Try
        '          If Page.Request.UrlReferrer Is Nothing OrElse Page.Request.UrlReferrer.ToString.Contains("../..") = False Then
        '              Response.Write("<br>Acceso Denegado")
        '              Form.Controls.Clear()
        '              Exit Sub
        '          End If
        '      Catch ex As Exception
        '          Response.Write("Ocurrió un error, inténtelo nuevamente")
        '      End Try
        Try
            'Response.Write("Sesion: " & Session.Count & "<br/>")


            'Response.Write(Session("codigo_alu"))

            If (Session("codigo_alu") Is Nothing) Then
                Response.Redirect("../ErrorSistema.aspx")
            End If

            Me.btnExportar.Visible = False
            If IsPostBack = False Then
                Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("cnxBDUSAT").ConnectionString)
                Dim Tbl As New Data.DataTable
                Dim id As String
                Dim codigo_test As Integer

                'id = Request.QueryString("id")
                id = Session("codigo_alu")

                If Request.QueryString("tipo") = "IIND" Then
                    Me.cmdRegresar.Visible = True
                Else
                    Me.cmdRegresar.Visible = False
                End If

                If Request.QueryString("VerDatos") = "0" Then
                    pnlDatos.Visible = False
                Else
                    pnlDatos.Visible = True
                End If
                If (id <> "") Then
                    'Dim objEnc As New EncriptaCodigos.clsEncripta
                    'Dim ObjCif As New PryCifradoNet.ClsCifradoNet
                    'Dim codUniver As String
                    'codUniver = Mid(objEnc.Decodifica(Request.QueryString("cod")), 4)

                    'Dim CadReci As String
                    'CadReci = Request.QueryString("cod").Trim
                    '********* SERVIDOR REAL ********* 
                    ' codUniver = ObjCif.DesCifrado(CadReci.Substring(16, 16), CadReci.Substring(0, 16)).ToString.Substring(6, 10)

                    '********* SERVIDOR DE PRUEBAS ********* 
                    'codUniver = CadReci

                    'Response.Write("Cod. Univ.:" & codUniver)
                    'Tbl = obj.TraerDataTable("consultaracceso", "E", codUniver, 0)
                    Me.dpEstado.SelectedValue = "P"
                    'Else
                    Tbl = obj.TraerDataTable("consultaracceso", "C", id, 0)
                    ' End If

                    Me.lblMensaje.Visible = False
                    Me.pnlBanco.Visible = True
                    Me.pnlDeudas.Visible = False
                    If Tbl.Rows.Count > 0 Then
                        Me.hddcodigo_alu.Value = Tbl.Rows(0).Item("codigo_alu")
                        Me.lblcodigo.Text = Tbl.Rows(0).Item("codigouniver_alu")
                        Me.lblalumno.Text = Tbl.Rows(0).Item("alumno")
                        Me.lblescuela.Text = Tbl.Rows(0).Item("nombre_cpf")
                        Me.lblcicloingreso.Text = Tbl.Rows(0).Item("cicloing_alu")
                        Me.lblPlan.Text = Tbl.Rows(0).Item("descripcion_pes")
                        codigo_test = Tbl.Rows(0).Item("codigo_test")
                        'Cargar la Foto
                        Dim ruta As String
                        Dim obEnc As Object
                        obEnc = Server.CreateObject("EncriptaCodigos.clsEncripta")

                        ruta = obEnc.CodificaWeb("069" & Tbl.Rows(0).Item("codigouniver_alu").ToString)
                        'ruta = "http://www.usat.edu.pe/imgestudiantes/" & ruta
                        ruta = "//intranet.usat.edu.pe/imgestudiantes/" & ruta
                        Me.FotoAlumno.ImageUrl = ruta
                        obEnc = Nothing

                        If codigo_test = 2 Then
                            'RegisterStartupScript("visible", "<script> document.form1.divObs.style.visibility='visible' </script>")
                            'Me.Panel1.Visible = True
                            Me.pnlAviso.Visible = True
                        Else
                            'Me.Panel1.Visible = False
                            Me.pnlAviso.Visible = True
                            'RegisterStartupScript("oculto", "<script> document.form1.divObs.style.visibility='hidden' </script>")
                        End If
                        'Me.gvPagoVsDeuda.Visible = False

                        'Cargar estado de cuenta
                        vencidas = 0
                        Me.grwPagos.DataSource = obj.TraerDataTable("ConsultarMovimientosAlumno_v2", Tbl.Rows(0).Item("codigouniver_alu").ToString, Me.dpEstado.SelectedValue)
                        Me.grwPagos.DataBind()
                        Me.lblVencidas.Text = "TOTAL DE DEUDAS VENCIDAS QUE GENERAN MORA: S/." & vencidas.ToString
                        Me.FotoAlumno.Visible = True

                        'Mostrar convenios de estudiantes
                        Dim tblconvenio As Data.DataTable

                        tblconvenio = obj.TraerDataTable("ConsultarConvenioPago", "CO", "E", Tbl.Rows(0).Item("codigo_alu").ToString)
                        If tblconvenio.Rows.Count > 0 Then
                            Me.lblconvenios.Text = tblconvenio.Rows(0).Item("nroconvenios").ToString
                            If Int(tblconvenio.Rows(0).Item("nroconvenios")) > 0 Then
                                Me.lblconvenios.Text = Me.lblconvenios.Text & "&nbsp;(Haga clic aquí)"
                                Me.lblconvenios.Attributes.Add("style", "cursor:hand")
                                Me.lblconvenios.Attributes.Add("onclick", "AbrirPopUp('conveniopensiones.aspx?codigo_alu=" + Tbl.Rows(0).Item("codigo_alu").ToString + "&codigouniver_alu=" + Tbl.Rows(0).Item("codigouniver_alu").ToString + "&tipo=E&alumno=" + Tbl.Rows(0).Item("alumno").ToString & "&nombre_cpf=" + Tbl.Rows(0).Item("nombre_cpf").ToString & "','550','900','no','no','yes','convenio')")
                            End If
                        End If
                        tblconvenio.Dispose()
                        lnkBanco_Click(sender, e)
                        Me.btnExportar.OnClientClick = "AbrirPopUp('rptListaPagosDeuda.aspx?al=" & Me.hddcodigo_alu.Value & "','600','800','yes','yes','yes','yes')"
                    Else
                        Me.lblMensaje.Text = "El estudiante no existe en la Base de datos"
                        Me.FotoAlumno.Visible = False
                    End If
                    Tbl.Dispose()
                    obj = Nothing

                    CalculaPagos()
                End If
            End If
        Catch ex As Exception
            Response.Write("Error al cargar: " & ex.StackTrace)
        End Try
    End Sub

    Private Sub CalculaPagos()
        Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("cnxBDUSAT").ConnectionString)
        Dim dt As New Data.DataTable
        Try            
            dt = obj.TraerDataTable("ACAD_CalculaCargoPensionAlumno", Session("codigo_alu"))

            If (dt.Rows.Count > 0) Then
                Me.lblNCuotas.Text = dt.Rows(0).Item("NroCuotas")
                Me.lblCostoPension.Text = FormatNumber(dt.Rows(0).Item("CostoTotal"), 2)
                Me.lblCostoComp.Text = FormatNumber(dt.Rows(0).Item("Complementarios"), 2)
                Me.lblCostoTotal.Text = FormatNumber(Double.Parse(Me.lblCostoPension.Text) + Double.Parse(Me.lblCostoComp.Text), 2)
                Me.lblCuotaMensual.Text = FormatNumber(Double.Parse(Me.lblCostoTotal.Text) / Double.Parse(Me.lblNCuotas.Text), 2)
            End If
        Catch ex As Exception
            Response.Write("Error al calcular pagos: " & ex.Message)
        End Try
    End Sub

    Protected Sub grwPagos_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grwPagos.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim fila As Data.DataRowView
            fila = e.Row.DataItem

            'If fila.Row("generaMora_sco") = True Then
            e.Row.Cells(1).Text = fila.Row("servicio").ToString & "(*)"

            If fila.Row("fechaVencimiento_sco") <= Date.Now.ToShortDateString Then
                vencidas = vencidas + FormatNumber(CDbl(fila.Row("saldo")) + CDbl(fila.Row("mora_deu")), 2)
                e.Row.CssClass = "rojo"

            End If

            'Else
            'e.Row.Cells(0).Text = ""
            'End If
            If fila.Row("estado_deu") = "P" Then
                e.Row.Cells(2).Text = "Pendiente"
            ElseIf fila.Row("estado_deu") = "C" Then
                e.Row.Cells(2).Text = "Cancelada"
            Else
                e.Row.Cells(2).Text = "Consideración Esp."
            End If

            If fila.Row("generainteres_sco") = True And fila.Row("esProgramaEspecial_Sco") = True Then
                e.Row.Cells(8).Text = "<a href='listaintereses.aspx?s=" & fila.Row("codigo_sco") & "&id=" & Request.QueryString("id") & "'> Ver interes </a>"
            Else
                'Verificar si tiene relacion con un pedido odontologico
                If (e.Row.Cells(9).Text.Trim <> "0") Then
                    e.Row.Cells(8).Text = "<a target='_blank' href='frmMuestraPedidoOdonto.aspx?x=" & e.Row.Cells(9).Text & "'> " & fila.Row("OBSERVACION").ToString & " </a>"
                Else
                    e.Row.Cells(8).Text = fila.Row("OBSERVACION").ToString
                End If
            End If

            e.Row.Cells(7).Text = FormatNumber(CDbl(fila.Row("saldo")) + CDbl(fila.Row("mora_deu")), 2)
            Cargo += fila.Row("cargo")
            Pago += fila.Row("Pagos")
            Saldo += fila.Row("saldo")
            Mora += fila.Row("mora_deu")
            SubTotal += CDbl(fila.Row("saldo")) + CDbl(fila.Row("mora_deu"))
        ElseIf e.Row.RowType = DataControlRowType.Footer Then
            e.Row.Cells(2).Text = "TOTAL:"
            e.Row.Cells(3).Text = FormatNumber(Cargo, 2)
            e.Row.Cells(4).Text = FormatNumber(Pago, 2)
            e.Row.Cells(5).Text = FormatNumber(Saldo, 2)
            e.Row.Cells(6).Text = FormatNumber(Mora, 2)
            e.Row.Cells(7).Text = FormatNumber(SubTotal, 2)

            e.Row.Cells(2).HorizontalAlign = HorizontalAlign.Right
            e.Row.Cells(3).HorizontalAlign = HorizontalAlign.Right
            e.Row.Cells(4).HorizontalAlign = HorizontalAlign.Right
            e.Row.Cells(5).HorizontalAlign = HorizontalAlign.Right
            e.Row.Cells(6).HorizontalAlign = HorizontalAlign.Right
            e.Row.Cells(7).HorizontalAlign = HorizontalAlign.Right
        End If
        'e.Row.Cells(9).Visible = False
    End Sub

    Protected Sub dpEstado_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dpEstado.SelectedIndexChanged
        Try
            Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("cnxBDUSAT").ConnectionString)
            Me.gvPagoVsDeuda.Visible = False
            If dpEstado.SelectedValue <> "D" Then
                If (Me.dpEstado.SelectedValue = "A") Then
                    Me.gvPagoVsDeuda.DataSource = obj.TraerDataTable("CAJ_EstadoCuentaAlumno", Me.hddcodigo_alu.Value)
                    Me.gvPagoVsDeuda.DataBind()
                    Me.gvPagoVsDeuda.Visible = True
                    Me.btnExportar.Visible = True
                Else
                    'Cargar estado de cuenta
                    Me.grwPagos.DataSource = obj.TraerDataTable("ConsultarMovimientosAlumno_v2", Me.lblcodigo.Text, Me.dpEstado.SelectedValue)
                    Me.grwPagos.DataBind()
                    Me.grwPagos.Visible = True
                End If
            Else
                Me.grwPagos.Visible = False
                Me.DocEmitidos.DataSource = obj.TraerDataTable("ConsultarDocumentosEmitidos", "E", Me.hddcodigo_alu.Value)
                Me.DocEmitidos.DataBind()
                Me.DocEmitidos.Visible = True
            End If
            obj = Nothing
            Me.pnlBanco.Visible = False
            Me.pnlDeudas.Visible = True
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub cmdRegresar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdRegresar.Click
        Response.Redirect("../../personal/administrativo/controlpagos/vstdeudascobrarIndustrial.asp?codigo_sco=" & Request.QueryString("codigo_sco") & "&edicion=" & Request.QueryString("edicion") & "&codigo_pes=" & Request.QueryString("codigo_pes"))
    End Sub

    Protected Sub lnkDeudas_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkDeudas.Click

        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("cnxBDUSAT").ConnectionString
        obj.AbrirConexion()
        Me.grwPagos.DataSource = obj.TraerDataTable("ConsultarMovimientosAlumno_v2", Me.lblcodigo.Text, Me.dpEstado.SelectedValue)
        Me.grwPagos.DataBind()
        obj.CerrarConexion()
        Me.pnlBanco.Visible = False
        Me.pnlDeudas.Visible = True
    End Sub

    Protected Sub lnkBanco_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkBanco.Click
        Me.pnlBanco.Visible = True
        Me.pnlDeudas.Visible = False
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("cnxBDUSAT").ConnectionString
        obj.AbrirConexion()
        Me.gvDatosBanco.DataSource = obj.TraerDataTable("ConsultarDeuda", "BK", Me.lblcodigo.Text)
        Me.gvDatosBanco.DataBind()
        obj.CerrarConexion()
    End Sub

End Class
