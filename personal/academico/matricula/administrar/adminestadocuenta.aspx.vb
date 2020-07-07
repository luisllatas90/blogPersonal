﻿
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
        '          If Page.Request.UrlReferrer Is Nothing OrElse Page.Request.UrlReferrer.ToString.Contains("../../../..") = False Then
        '              Response.Write("<br>Acceso Denegado")
        '              Form.Controls.Clear()
        '              Exit Sub
        '          End If
        '      Catch ex As Exception
        '          Response.Write("Ocurrió un error, inténtelo nuevamente")
        '      End Try
        Me.btnExportar.Visible = False
        If IsPostBack = False Then
            Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("cnxBDUSAT").ConnectionString)
            Dim Tbl As New Data.DataTable
            Dim id As String
            Dim codigo_test As Integer

            id = Request.QueryString("id")

            If Request.QueryString("VerDatos") = "0" Then
                pnlDatos.Visible = False
            Else
                pnlDatos.Visible = True
            End If
            If (Request.QueryString("cod") <> "") Then
                Dim objEnc As New EncriptaCodigos.clsEncripta
                Dim ObjCif As New PryCifradoNet.ClsCifradoNet
                Dim codUniver As String
                'codUniver = mid(objEnc.Decodifica(Request.QueryString("cod")), 4)

                Dim CadReci As String
                CadReci = Request.QueryString("cod").Trim
                codUniver = ObjCif.DesCifrado(CadReci.Substring(16, 16), CadReci.Substring(0, 16)).ToString.Substring(6, 10)
                Tbl = obj.TraerDataTable("consultaracceso", "E", codUniver, 0)
            Else
                Tbl = obj.TraerDataTable("consultaracceso", "C", id, 0)
            End If

            Me.lblMensaje.Visible = False
            Me.pnlBanco.Visible = True
            Me.pnlDeudas.Visible = False
            If Tbl.Rows.Count > 0 Then
                Me.hddcodigo_alu.value = Tbl.Rows(0).Item("codigo_alu")
                Me.lblcodigo.text = Tbl.Rows(0).Item("codigouniver_alu")
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
                '---------------------------------------------------------------------------------------------------------------
                'Fecha: 29.10.2012
                'Usuario: dguevara
                'Modificacion: Se modifico el http://www.usat.edu.pe por http://intranet.usat.edu.pe
                '---------------------------------------------------------------------------------------------------------------
                ruta = "//intranet.usat.edu.pe/imgestudiantes/" & ruta
                Me.FotoAlumno.ImageUrl = ruta
                obEnc = Nothing

                If codigo_test = 2 Then
                    'RegisterStartupScript("visible", "<script> document.form1.divObs.style.visibility='visible' </script>")
                    Me.Panel1.Visible = True
                    Me.pnlAviso.Visible = True
                Else
                    Me.Panel1.Visible = False
                    Me.pnlAviso.Visible = True
                    'RegisterStartupScript("oculto", "<script> document.form1.divObs.style.visibility='hidden' </script>")
                End If
                'Me.gvPagoVsDeuda.Visible = False

                'Cargar estado de cuenta
                vencidas = 0
                Me.grwPagos.DataSource = obj.TraerDataTable("ConsultarMovimientosAlumno_v2", Tbl.Rows(0).Item("codigouniver_alu").ToString, "P")
                Me.grwPagos.DataBind()
                Me.lblVencidas.Text = "TOTAL DE DEUDAS VENCIDAS QUE GENERAN MORA: S/." & vencidas.ToString
                Me.FotoAlumno.Visible = True

                'Mostrar convenios de estudiantes                
                lnkDeudas_Click(sender, e)
                Me.btnExportar.OnClientClick = "AbrirPopUp('rptListaPagosDeuda.aspx?al=" & Me.hddcodigo_alu.Value & "','600','800','yes','yes','yes','yes')"
            Else
                Me.lblMensaje.Text = "El estudiante no existe en la Base de datos"
                Me.FotoAlumno.Visible = False
            End If
            Tbl.Dispose()
            obj = Nothing
        End If
    End Sub

    Protected Sub grwPagos_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grwPagos.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim fila As Data.DataRowView
            fila = e.Row.DataItem

            If fila.Row("generaMora_sco") = True Then
                e.Row.Cells(1).Text = fila.Row("servicio").ToString & "(*)"

                If fila.row("fechaVencimiento_sco") <= Date.Now.toShortDateString Then
                    vencidas = vencidas + FormatNumber(CDbl(fila.Row("saldo")) + CDbl(fila.Row("mora_deu")), 2)
                    e.row.cssclass = "rojo"

                End If

            Else
                e.Row.Cells(0).Text = ""
            End If
            If fila.Row("estado_deu") = "P" Then
                e.Row.Cells(2).Text = "Pendiente"
            ElseIf fila.Row("estado_deu") = "C" Then
                e.Row.Cells(2).Text = "Cancelada"
            ElseIf fila.Row("estado_deu") = "S" Then
                e.Row.Cells(2).Text = "Suspendida"
            Else
                e.Row.Cells(2).Text = "Consideración Esp."
            End If

            If fila.Row("generainteres_sco") = True And fila.row("esProgramaEspecial_Sco") = True Then
                e.Row.Cells(8).Text = "<a href='listaintereses.aspx?s=" & fila.row("codigo_sco") & "&id=" & Request.QueryString("id") & "'>Ver interes</a>"
            Else
                e.Row.Cells(8).Text = ""
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
    End Sub

    Protected Sub lnkDeudas_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkDeudas.Click
       
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("cnxBDUSAT").ConnectionString
        obj.AbrirConexion()
        Me.grwPagos.DataSource = obj.TraerDataTable("ConsultarMovimientosAlumno_v2", Me.lblcodigo.Text, "S")
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
