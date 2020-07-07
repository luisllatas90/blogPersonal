
Partial Class logistica_frmEvaluacionOrdenCompra
    Inherits System.Web.UI.Page

    Dim _id As Integer
    Dim ctf As Integer

    Protected Sub gvCabOrden_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvCabOrden.RowDataBound
        Dim objL As New ClsLogistica
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes.Add("OnMouseOver", "Resaltar(1,this,'S')")
            e.Row.Attributes.Add("OnMouseOut", "Resaltar(0,this,'S')")
            e.Row.Attributes.Add("OnClick", "javascript:__doPostBack('gvCabOrden','Select$" & e.Row.RowIndex & "');")
            e.Row.Style.Add("cursor", "hand")
            e.Row.Cells(13).Attributes.Add("OnClick", "javascript:ModalAdjuntar2('" & objL.EncrytedString64(DataBinder.Eval(e.Row.DataItem, "codigo_Rco").ToString()) & "')")

        End If

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            gvCabOrden.SelectedIndex = 0
            cboEstado.SelectedValue = 9 ' Pendiente
            If Request.QueryString("nivel") = 1 Then
                CargarVeredicto()
            End If
            'ctf = Request.QueryString("ctf")
            _id = Request.QueryString("id")
            'If ctf <> 5 And id <> 4472 Then
            '    Me.rbtVeredicto.Items.Item(4).Attributes.Add("hidden", "hidden")
            'End If
            Me.pnlObservaciones.Visible = False
        End If
    End Sub

    Sub CargarVeredicto()
        rbtVeredicto.Items.Clear()
        rbtVeredicto.Items.Add("Aprobar")
        rbtVeredicto.Items(0).Value = "A"
        rbtVeredicto.Items.Add("Rechazar")
        rbtVeredicto.Items(1).Value = "D"
        ctf = Request.QueryString("ctf")
        If ctf = 5 Then
            rbtVeredicto.Items.Add("Derivar Adm. General")
            rbtVeredicto.Items(2).Value = "Y"
        End If
    End Sub

    Protected Sub cmdGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdGuardar.Click
        Dim codigo_Rcom, codigo_Rco As Int64
        Dim F As Integer
        'Dim cantidad As Integer
        Dim ObjCnx As New ClsConectarDatos
        Dim codigo As Integer
        Dim rpta As Integer
        Dim valoresdevueltos(1) As Integer
        Try
            rpta = 0
            If gvCabOrden.Rows.Count > 0 Then
                codigo_Rco = gvCabOrden.DataKeys.Item(gvCabOrden.SelectedIndex).Values(0)
                codigo_Rcom = gvCabOrden.DataKeys.Item(gvCabOrden.SelectedIndex).Values(1)
                ObjCnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
                Dim tipoOrden As String
                If gvCabOrden.DataKeys.Item(gvCabOrden.SelectedIndex).Values(2).ToString.Trim = "O/C" Then
                    tipoOrden = "C"
                Else
                    tipoOrden = "S"
                End If

                If Me.rbtVeredicto.SelectedValue = "X" Then
                    ObjCnx.IniciarTransaccion()
                    ObjCnx.Ejecutar("LOG_ActualizarVeredictoOrden", codigo_Rcom, Me.rbtVeredicto.SelectedValue, Me.txtObservacion.Text)
                    ObjCnx.Ejecutar("LOG_DerivarRevisorCompra", cboPersonalDerivar.SelectedValue, codigo_Rco, Me.cboDerivar.SelectedValue)
                    ObjCnx.TerminarTransaccion()
                    'EnviaMailInstanciaSiguiente(codigo_Rco)
                End If

                If Me.rbtVeredicto.SelectedValue = "O" Then
                    ObjCnx.IniciarTransaccion()
                    ObjCnx.Ejecutar("LOG_RegistrarOrdenObservada", codigo_Rcom, Me.txtObservacion.Text)
                    ObjCnx.TerminarTransaccion()
                    EnviarMailOrdenObservada(codigo_Rco, hddPersonal.Value)
                End If
                If rbtVeredicto.SelectedValue = "D" Then
                    ObjCnx.IniciarTransaccion()
                    ObjCnx.Ejecutar("LOG_ActualizarVeredictoOrden", codigo_Rcom, Me.rbtVeredicto.SelectedValue, Me.txtObservacion.Text)
                    ObjCnx.Ejecutar("ActualizarEstadoRegistroCompra", codigo_Rco, "A", 0).copyto(valoresdevueltos, 0)
                    rpta = valoresdevueltos(0)
                    ObjCnx.TerminarTransaccion()
                End If
                If rbtVeredicto.SelectedValue = "A" Then
                    ObjCnx.IniciarTransaccion()
                    ObjCnx.Ejecutar("LOG_ActualizarVeredictoOrden", codigo_Rcom, Me.rbtVeredicto.SelectedValue, Me.txtObservacion.Text)
                    ObjCnx.Ejecutar("ActualizarEstadoRegistroCompra", codigo_Rco, "C", 0).copyto(valoresdevueltos, 0)
                    rpta = valoresdevueltos(0)
                    ObjCnx.TerminarTransaccion()
                End If
                If rbtVeredicto.SelectedValue = "Y" Then
                    _id = Request.QueryString("id")
                    ObjCnx.IniciarTransaccion()
                    ObjCnx.Ejecutar("LOG_registrarRevisionCompra", 0, _id, codigo_Rco, tipoOrden)
                    ObjCnx.TerminarTransaccion()
                    'Response.Write(_id + "-" + codigo_Rco)
                End If
                'EnviaMailInstanciaSiguiente(codigo_Rco, rpta)
                EnviarMail(codigo_Rco, rpta)
                ClientScript.RegisterStartupScript(Me.GetType, "Alerta", "alert('Se calificó el pedido satisfactoriamente');", True)
                ClientScript.RegisterStartupScript(Me.GetType, "siguientepagina", "location.href='frmEvaluacionOrdenCompra.aspx?id=" & Request.QueryString("id") & "';", True)
                Me.gvCabOrden.DataBind()
                Me.gvDetalleOrdenPedido.DataBind()
                Me.txtObservacion.Text = ""
                Me.rbtVeredicto.ClearSelection()

            End If
            ClientScript.RegisterStartupScript(Me.GetType, "Alerta", "alert('Se calificó el pedido satisfactoriamente');", True)
            ClientScript.RegisterStartupScript(Me.GetType, "siguientepagina", "location.href='frmEvaluacionOrdenCompra.aspx?id=" & Request.QueryString("id") & "';", True)
            Me.gvCabOrden.DataBind()
            Me.gvDetalleOrdenPedido.DataBind()
            Me.txtObservacion.Text = ""
            Me.rbtVeredicto.ClearSelection()

            'Me.rbtVeredicto.Items(2).Text = "id: " & _id & " codigo_Rco: " & codigo_Rco

        Catch ex As Exception
            ObjCnx.AbortarTransaccion()
            'Response.Write(ex)
            ScriptManager1.RegisterStartupScript(Me, Me.GetType, "error", "alert('Ocurrió un error al procesar los datos: -" & ex.Message & "')", True)

        End Try
    End Sub

    Private Sub EnviarMail(ByVal codigo_rco As Int64, ByVal rpta As Integer)
        Try
            Dim ObjCnx As New ClsConectarDatos
            Dim dt2, dt3 As System.Data.DataTable
            Dim estado, moneda As String

            ObjCnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            'ScriptManager1.RegisterStartupScript(Me, Me.GetType, "error", "alert('" & rpta & "')", True)

            ''ClientScript.RegisterStartupScript(Me.GetType, "Alerta", "alert('" & rpta & "');", True)

            Select Case rpta
                Case 11 : estado = "APROBADA"
                Case 13 : estado = "RECHAZADA"
            End Select

            Dim dt As New Data.DataTable
            Dim dtEst As New Data.DataTable
            Dim cnx As New ClsConectarDatos
            Dim ObjMailNet As New ClsEnviarCorreoAprobacionOrden 'ClsMail
            Dim correo As String
            Dim asunto As String
            Dim mensaje As String
            Dim firma As String
            Dim mensajeObs As String
            Dim nombreTrabajador As String
            Dim coreeoRevisor As String
            Dim tipoOrden2 As String
            Dim cc As String

            ObjCnx.AbrirConexion()
            dt = ObjCnx.TraerDataTable("dbo.ConsultarPersonaRegistroCompra", codigo_rco)
            ObjCnx.CerrarConexion()

            If (dt.Rows.Count > 0) And rpta <> 0 Then


                If CInt(dt.Rows(0).Item("codigo_Tdo").ToString) = 15 Then
                    tipoOrden2 = "Orden de Compra"
                Else
                    tipoOrden2 = "Orden de Servicio"
                End If
                If dt.Rows(0).Item("moneda_Rco") = "S" Then
                    moneda = "S/ "
                ElseIf dt.Rows(0).Item("moneda_Rco") = "D" Then
                    moneda = "$ "
                Else
                    moneda = ""
                End If

                firma = ""
                correo = dt.Rows(0).Item("correo").ToString
                'coreeoRevisor = "tyong@usat.edu.pe" '---> Director de Logística 'dt.Rows(0).Item("CorreoRevisor").ToString
                nombreTrabajador = dt.Rows(0).Item("personal").ToString
                'revisor = dt.Rows(0).Item("revisor").ToString
                asunto = "Revisión: " & tipoOrden2 & " N° " & dt.Rows(0).Item("numerodoc_rco").ToString

                mensaje = "<table border=0 cellpadding=0>"
                mensaje &= "<tr><td style=""border:none;border-bottom:solid #E33439 1.0pt;padding:.75pt .75pt .75pt .75pt""><p class=""MsoNormal""><span style=""font-size:14pt;font-family:&quot;Calibri&quot;,sans-serif;color:#E33439"">Sistema de Compras - Campus Virtual USAT</span><span style=""font-family:&quot;Calibri&quot;,sans-serif;color:#E33439""><o:p></o:p></span></p></td></tr>"
                mensaje &= "<tr><td style=""padding:.75pt .75pt .75pt .75pt""><p class=""MsoNormal""><b><span style=""font-size:1;font-family:&quot;Calibri&quot;,sans-serif""><o:p>&nbsp;</o:p></span></b></p>"
                mensaje &= "<p class=""MsoNormal""><b><span style=""font-size:11.0pt;font-family:&quot;Calibri&quot;,sans-serif"">Estimado Sr. " & nombreTrabajador & "</span></b><span style=""font-size:11.0pt;font-family:&quot;Calibri&quot;,sans-serif""><o:p></o:p></span></p>"
                mensaje &= "</td></tr>"
                mensaje = mensaje & "<tr><td colspan=2 style=""padding:.75pt .75pt .75pt .75pt""></td></tr>"
                mensaje = mensaje & "<tr><td colspan=2 style=""padding:.75pt .75pt .75pt .75pt""><p class=""MsoNormal""><span style=""font-size:11.0pt;font-family:&quot;Calibri&quot;,sans-serif"">La " & tipoOrden2 & " N° " & dt.Rows(0).Item("numerodoc_rco").ToString & " para el proveedor " & dt.Rows(0).Item("proveedor") & " por el monto de " & moneda & dt.Rows(0).Item("totalCompra_Rco")
                mensaje = mensaje & " ha sido calificada como <b>" & estado & "</b></td></tr></table>"

                mensaje &= "<br><TABLE style=""font-family:Calibri;font-size:11pt;WIDTH:50%;BORDER-COLLAPSE:collapse"" cellSpacing=0 rules=all border=1><TBODY>"
                mensaje &= "<TR style=""COLOR: #ffffff; BACKGROUND-COLOR: #e33439""><TH scope=col>Instancia</TH><TH scope=col>Usuario</TH><TH scope=col>Evaluación</TH><TH scope=col>Observación</TH><TH scope=col>Fecha</TH></TR>"


                ObjCnx.AbrirConexion()
                dtEst = ObjCnx.TraerDataTable("dbo.LOG_ConsultarRevisionesOrdenes", codigo_rco)
                ObjCnx.CerrarConexion()
                If dtEst.Rows.Count > 0 Then
                    For i As Integer = 0 To dtEst.Rows.Count - 1
                        mensaje &= "<tr><td>" & dtEst.Rows(i).Item("Tipo Orden") & "</td>"
                        mensaje &= "<td>" & dtEst.Rows(i).Item("login_Per") & "</td>"
                        mensaje &= "<td>" & dtEst.Rows(i).Item("Evaluación") & "</td>"
                        mensaje &= "<td>" & dtEst.Rows(i).Item("Observacion") & "</td>"
                        mensaje &= "<td>" & Left(dtEst.Rows(i).Item("fecha_Rcom").ToString, 10) & "</td></tr>"
                    Next
                End If

                mensaje &= "</table>"
                mensaje &= "<br><br>" & firma & "</div>"
                'Response.Write(correo)
                ObjMailNet.EnviarMailAd("campusvirtual@usat.edu.pe", "Sistema de Pedidos - Logística", correo, asunto, mensaje, True)
                EnviarMailPedidoOrden(codigo_rco, dt.Rows(0).Item("numerodoc_rco").ToString, tipoOrden2, estado)
            End If
        Catch ex As Exception
            ScriptManager1.RegisterStartupScript(Me, Me.GetType, "error", "alert('Ocurrió un error al procesar los datos: -mail" & ex.Message & "')", True)

        End Try
        

    End Sub

    Private Sub EnviarMailPedidoOrden(ByVal codigo_rco As Int64, ByVal numero_rco As String, ByVal tipo As String, ByVal estado As String)
        Dim ObjCnx As New ClsConectarDatos

        ObjCnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString


        Dim dt As New Data.DataTable
        Dim dtEst As New Data.DataTable
        Dim cnx As New ClsConectarDatos
        Dim ObjMailNet As New ClsEnviarCorreoAprobacionOrden 'ClsMail
        Dim correo As String
        Dim asunto As String
        Dim mensaje As String
        Dim firma As String
        Dim mensajeObs As String
        Dim nombreTrabajador As String
        Dim coreeoRevisor As String
        Dim tipoOrden2 As String
        Dim cc As String


        ObjCnx.AbrirConexion()
        dt = ObjCnx.TraerDataTable("dbo.LOG_ConsultarCorreoPedidoOrden", codigo_rco)
        ObjCnx.CerrarConexion()

        If dt.Rows.Count > 0 Then

            For i As Integer = 1 To dt.Rows.Count
                firma = ""
                correo = dt.Rows(i - 1).Item("correo").ToString
                nombreTrabajador = dt.Rows(i - 1).Item("personal").ToString
                asunto = "Aprobación: " & tipo & " N° " & numero_rco

                mensaje = "<table border=0 cellpadding=0>"
                mensaje &= "<tr><td style=""border:none;border-bottom:solid #E33439 1.0pt;padding:.75pt .75pt .75pt .75pt""><p class=""MsoNormal""><span style=""font-size:14pt;font-family:&quot;Calibri&quot;,sans-serif;color:#E33439"">Sistema de Compras - Campus Virtual USAT</span><span style=""font-family:&quot;Calibri&quot;,sans-serif;color:#E33439""><o:p></o:p></span></p></td></tr>"
                mensaje &= "<tr><td style=""padding:.75pt .75pt .75pt .75pt""><p class=""MsoNormal""><b><span style=""font-size:1;font-family:&quot;Calibri&quot;,sans-serif""><o:p>&nbsp;</o:p></span></b></p>"
                mensaje &= "<p class=""MsoNormal""><b><span style=""font-size:11.0pt;font-family:&quot;Calibri&quot;,sans-serif"">Estimado (a) " & nombreTrabajador & "</span></b><span style=""font-size:11.0pt;font-family:&quot;Calibri&quot;,sans-serif""><o:p></o:p></span></p>"
                mensaje &= "</td></tr>"
                mensaje = mensaje & "<tr><td colspan=2 style=""padding:.75pt .75pt .75pt .75pt""></td></tr>"
                mensaje = mensaje & "<tr><td colspan=2 style=""padding:.75pt .75pt .75pt .75pt""><p class=""MsoNormal""><span style=""font-size:11.0pt;font-family:&quot;Calibri&quot;,sans-serif"">La " & tipo & " N° " & numero_rco & " relacionada con su pedido N° " & dt.Rows(i - 1).Item("pedidos") & " ha sido " & estado & "."
                mensaje = mensaje & "</td></tr></table>"

                mensaje &= "<br><br>" & firma & "</div>"
                'Response.Write(correo)
                ObjMailNet.EnviarMailAd("campusvirtual@usat.edu.pe", "Sistema de Pedidos - Logística", correo, asunto, mensaje, True)

            Next

        End If

    End Sub

    Private Sub EnviarEmailAprobacion(ByVal codigo_rco As Integer)
        '----------------Envío mail --------------------------------------

        Dim dt As New Data.DataTable
        Dim ObjCnx As New ClsConectarDatos
        Dim dtEst As New Data.DataTable
        Dim cnx As New ClsConectarDatos
        Dim ObjMailNet As New ClsEnviarCorreoAprobacionOrden 'ClsMail
        Dim correo As String
        Dim asunto As String
        Dim mensaje As String
        Dim mensajeObs As String
        Dim nombreTrabajador As String
        Dim coreeoRevisor As String
        Dim tipoOrden As String

        ObjCnx.AbrirConexion()
        dt = ObjCnx.TraerDataTable("dbo.ConsultarPersonaRegistroCompra", codigo_rco)
        ObjCnx.CerrarConexion()
        If (dt.Rows.Count > 0) Then

            If CInt(dt.Rows(0).Item("codigo_Tdo").ToString) = 15 Then
                tipoOrden = "Orden de Compra"
            Else
                tipoOrden = "Orden de Servicio"
            End If
            correo = dt.Rows(0).Item("correo").ToString
            coreeoRevisor = "tyong@usat.edu.pe" '---> Director de Logística 'dt.Rows(0).Item("CorreoRevisor").ToString
            nombreTrabajador = dt.Rows(0).Item("personal").ToString
            'revisor = dt.Rows(0).Item("revisor").ToString
            asunto = "Revisión: " & tipoOrden & " " & dt.Rows(0).Item("numerodoc_rco").ToString
            'mensaje = nombreTrabajador & "</BR></BR>El pedido N°" & dt.Rows(0).Item("numerodoc_rco").ToString & " ha sido calificado como " & calificacion & " "
            If (Me.txtObservacion.Text <> "") Then
                mensajeObs = "<br/> Observación: " & Me.txtObservacion.Text
            Else
                mensajeObs = "<br/> Observación: Ninguna"
            End If
            'mensaje = mensaje & "</BR></BR> Atte. </br> Campus Virtual "
            'ObjMailNet.EnviarMailAd("campusvirtual@usat.edu.pe", "Sistema de Pedidos - Logística", correo, asunto, mensaje, True)
            '' para enviar mensaje de alerta a presupuesto solo cuando un pedido observado a sido corregido
            ' If calificacion = "CONFORME" And estadoPedido = 12 Then
            mensaje = "Estimad@ " & nombreTrabajador & " Se ha calificado satisfactoriamente la " & tipoOrden & "N°: " & dt.Rows(0).Item("numerodoc_rco").ToString & ". " & mensajeObs & ". " & "Atte. Campus Virtual"
            ObjMailNet.EnviarMailAd("campusvirtual@usat.edu.pe", "Sistema de Pedidos - Logística", correo, asunto, mensaje, True)

            'Response.Write(correo & " " & asunto & " " & mensaje)
            ' End If
        End If
    End Sub

    Private Sub EnviarMailOrdenObservada(ByVal codigo_Rco As Int64, ByVal Revisor As String)
        Dim ObjCnx As New ClsConectarDatos
        Dim datos, datosOAprob As New Data.DataTable
        Dim ObjMail As New ClsMail
        Dim Mensaje, AsuntoCorreo, Correo As String

        ObjCnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        ObjCnx.AbrirConexion()
        datos = ObjCnx.TraerDataTable("LOG_ConsultarEvaluacionCompra", codigo_Rco)
        ObjCnx.CerrarConexion()

        Mensaje = "<html><head><title>COMPRAS USAT</title><style>"
        Mensaje = Mensaje & ".TablaDetalle{border-width:0; border-color:#000000; border-collapse: collapse; border-style: solid}"
        Mensaje = Mensaje & "td{font-family:MS Sans Serif; font-size:10pt}"
        Mensaje = Mensaje & ".CeldaTitulo{color: #FFFFFF; font-weight: bold; text-align: center; border: 1px solid #000000; background-color: #800000}"
        Mensaje = Mensaje & ".CeldaDetalle{border: 1px solid #000000}"
        Mensaje = Mensaje & ".CeldaDetalle1{border: 1px solid #000000;margin-right: 10;text-align:right}"
        Mensaje = Mensaje & "</style></head><body>"

        If datos.Rows.Count > 0 Then
            AsuntoCorreo = ""
            Mensaje = Mensaje & "<table border=0>"
            Mensaje = Mensaje & "<tr><td><b>Estimado Sr. YONG WONG AUGUSTO ROBERTO</b></td><td></td></tr>"
            Mensaje = Mensaje & "<tr><td colspan=2></br></td></tr>"
            Mensaje = Mensaje & "<tr><td colspan=2></br></td></tr>"
            If Me.gvCabOrden.SelectedRow.Cells(4).Text = "O/C" Then
                Mensaje = Mensaje & "<tr><td colspan=2>La orden de compra número <b>" & datos.Rows(0).Item("numerodoc_rco") & "</b>" & _
                " para el proveedor " & datos.Rows(0).Item("nombrePro") & _
                " ha sido observada por " & Revisor & ", sirvase verificar el documento observado para proceder con la evaluación. </td></tr>"
                AsuntoCorreo = "(" & datos.Rows(0).Item("numerodoc_rco") & ") Orden de Compra observada para revisión"
            Else
                Mensaje = Mensaje & "<tr><td colspan=2>La orden de servicio número <b>" & datos.Rows(0).Item("numerodoc_rco") & "</b>" & _
                " para el proveedor " & datos.Rows(0).Item("nombrePro") & _
                " ha sido observada por " & Revisor & ", sirvase verificar el documento observado para proceder con la evaluación. </td></tr>"
                AsuntoCorreo = "(" & datos.Rows(0).Item("numerodoc_rco") & ") Orden de Servicio observada para revisión"
            End If

            Mensaje = Mensaje & "<tr><td colspan=2></br></td></tr>"
            Mensaje = Mensaje & "<tr><td colspan=2><font color=#004080>------------------------------------------------------------</font></td></tr>"
            Mensaje = Mensaje & "<tr><td colspan=2><b><font color=#004080> Sistema de Compras - Campus Virtual </font></b></td></tr>"
            Mensaje = Mensaje & "</table>"

            Correo = "tyong@usat.edu.pe"
            'Correo = "hreyes@usat.edu.pe"

            ObjMail.EnviarMail("campusvirtual@usat.edu.pe", "Sistema de Compras", Correo, AsuntoCorreo, Mensaje, True)
        End If
    End Sub

    Private Sub EnviaMailInstanciaSiguiente(ByVal codigo_Rco As Integer, Optional ByVal rpta As Integer = 0)

        Dim ObjCnx As New ClsConectarDatos
        Dim datos, datosOAprob As New Data.DataTable
        Dim ObjMail As New ClsMail
        Dim ObjMailAprobacion As New ClsEnviarCorreoAprobacionOrden
        Dim Mensaje, AsuntoCorreo, Correo, Correo2 As String

        ObjCnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        ObjCnx.AbrirConexion()
        datos = ObjCnx.TraerDataTable("LOG_ConsultarEvaluacionCompra", codigo_Rco)
        ObjCnx.CerrarConexion()

        Mensaje = "<html><head><title>COMPRAS USAT</title><style>"
        Mensaje = Mensaje & ".TablaDetalle{border-width:0; border-color:#000000; border-collapse: collapse; border-style: solid}"
        Mensaje = Mensaje & "td{font-family:MS Sans Serif; font-size:10pt}"
        Mensaje = Mensaje & ".CeldaTitulo{color: #FFFFFF; font-weight: bold; text-align: center; border: 1px solid #000000; background-color: #800000}"
        Mensaje = Mensaje & ".CeldaDetalle{border: 1px solid #000000}"
        Mensaje = Mensaje & ".CeldaDetalle1{border: 1px solid #000000;margin-right: 10;text-align:right}"
        Mensaje = Mensaje & "</style></head><body>"

        If datos.Rows.Count > 0 And rpta <> 13 Then
            ObjCnx.AbrirConexion()
            ObjCnx.Ejecutar("LOG_ActualizarNivelActualOrden", codigo_Rco, datos.Rows(0).Item("nivel_cic"))
            ObjCnx.CerrarConexion()
            AsuntoCorreo = ""
            Mensaje = Mensaje & "<table border=0>"
            Mensaje = Mensaje & "<tr><td><b>Estimado Sr. " & datos.Rows(0).Item("Personal") & "</b></td><td></td></tr>"
            Mensaje = Mensaje & "<tr><td colspan=2></br></td></tr>"
            Mensaje = Mensaje & "<tr><td colspan=2></br></td></tr>"
            If Me.gvCabOrden.SelectedRow.Cells(4).Text = "O/C" Then
                Mensaje = Mensaje & "<tr><td colspan=2>Se ha registrado la orden de compra número <b>" & datos.Rows(0).Item("numerodoc_rco") & "</b>" & _
                          " para el proveedor " & datos.Rows(0).Item("nombrePro") & _
                          " con un total de <b> " & IIf(datos.Rows(0).Item("moneda_Rco").ToString.TrimEnd = "S", "S/. ", "$ ") & datos.Rows(0).Item("totalCompra_Rco") & "</b> para su revisión, sirvase verificar en el sistema y proceder con la evaluación. </td></tr>"
                AsuntoCorreo = "(" & datos.Rows(0).Item("numerodoc_rco") & ") Orden de Compra pendiente para revisión"
            ElseIf Me.gvCabOrden.SelectedRow.Cells(4).Text = "O/S" Then
                Mensaje = Mensaje & "<tr><td colspan=2>Se ha registrado la orden de servicio número <b>" & datos.Rows(0).Item("numerodoc_rco") & "</b>" & _
                          " para el proveedor " & datos.Rows(0).Item("nombrePro") & _
                          " con un total de <b> " & IIf(datos.Rows(0).Item("moneda_Rco").ToString.TrimEnd = "S", "S/. ", "$ ") & datos.Rows(0).Item("totalCompra_Rco") & "</b> para su revisión, sirvase verificar en el sistema y proceder con la evaluación. </td></tr>"
                AsuntoCorreo = "(" & datos.Rows(0).Item("numerodoc_rco") & ") Orden de Servicio pendiente para revisión"
            End If
            Mensaje = Mensaje & "<tr><td colspan=2></br></td></tr>"
            Mensaje = Mensaje & "<tr><td colspan=2><font color=#004080>------------------------------------------------------------</font></td></tr>"
            Mensaje = Mensaje & "<tr><td colspan=2><b><font color=#004080> Sistema de Compras - Campus Virtual </font></b></td></tr>"
            Mensaje = Mensaje & "</table>"

            Correo = datos.Rows(0).Item("usuario_per") & "@usat.edu.pe"
            'Response.Write(Correo)
            ObjMail.EnviarMail("campusvirtual@usat.edu.pe", "Sistema de Compras", Correo, AsuntoCorreo, Mensaje, True)
        Else
            Dim dt2 As System.Data.DataTable
            Dim estado As String

            Select Case rpta
                Case 11 : estado = "APROBADA"
                Case 13 : estado = "RECHAZADA"
            End Select

            Dim dt As New Data.DataTable
            Dim dtEst As New Data.DataTable
            Dim cnx As New ClsConectarDatos
            Dim ObjMailNet As New ClsEnviarCorreoAprobacionOrden 'ClsMail
            Dim asunto As String
            Dim firma As String
            Dim mensajeObs As String
            Dim nombreTrabajador As String
            Dim coreeoRevisor As String
            Dim tipoOrden2 As String

            Mensaje = ""
            Correo = ""
            ObjCnx.AbrirConexion()
            dt = ObjCnx.TraerDataTable("dbo.ConsultarPersonaRegistroCompra", codigo_Rco)
            ObjCnx.CerrarConexion()

            If (dt.Rows.Count > 0) And rpta <> 0 Then

                If CInt(dt.Rows(0).Item("codigo_Tdo").ToString) = 15 Then
                    tipoOrden2 = "Orden de Compra"
                Else
                    tipoOrden2 = "Orden de Servicio"
                End If
                firma = "Atte. Campus Virtual"
                Correo = dt.Rows(0).Item("correo").ToString
                coreeoRevisor = "tyong@usat.edu.pe" '---> Director de Logística 'dt.Rows(0).Item("CorreoRevisor").ToString
                nombreTrabajador = dt.Rows(0).Item("personal").ToString
                'revisor = dt.Rows(0).Item("revisor").ToString
                asunto = "Revisión: " & tipoOrden2 & " N° " & dt.Rows(0).Item("numerodoc_rco").ToString

                Mensaje = "<div style=""font-family:Calibri;font-size:11pt;color:#1F497D"">Estimado(a) " & nombreTrabajador & "<br><br>La " & tipoOrden2 & " N° " & dt.Rows(0).Item("numerodoc_rco").ToString & " para el proveedor " & dt.Rows(0).Item("proveedor") & " ha sido calificada como " & estado
                Mensaje &= "<br><br><TABLE style=""font-family:Calibri;color:#1F497D;font-size:11pt;WIDTH:50%;BORDER-COLLAPSE:collapse"" cellSpacing=0 rules=all border=1><TBODY>"
                Mensaje &= "<TR style=""COLOR: #ffffff; BACKGROUND-COLOR: #e33439""><TH scope=col>Instancia</TH><TH scope=col>Usuario</TH><TH scope=col>Evaluación</TH><TH scope=col>Observación</TH><TH scope=col>Fecha</TH></TR>"

                ObjCnx.AbrirConexion()
                dtEst = ObjCnx.TraerDataTable("dbo.LOG_ConsultarRevisionesOrdenes", codigo_Rco)
                ObjCnx.CerrarConexion()
                If dtEst.Rows.Count > 0 Then
                    For i As Integer = 0 To dtEst.Rows.Count - 1
                        Mensaje &= "<tr><td>" & dtEst.Rows(i).Item("Tipo Orden") & "</td>"
                        Mensaje &= "<td>" & dtEst.Rows(i).Item("login_Per") & "</td>"
                        Mensaje &= "<td>" & dtEst.Rows(i).Item("Evaluación") & "</td>"
                        Mensaje &= "<td>" & dtEst.Rows(i).Item("Observacion") & "</td>"
                        Mensaje &= "<td>" & dtEst.Rows(i).Item("fecha_Rcom") & "</td></tr>"
                    Next
                End If

                Mensaje &= "</table>"
                Mensaje &= "<br><br>" & firma & "</div>"
                'Response.Write(Correo)
                ObjMailNet.EnviarMailAd("campusvirtual@usat.edu.pe", "Sistema de Pedidos - Logística", Correo, asunto, Mensaje, True)

            End If
        End If
    End Sub

    Protected Sub cboEstado_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboEstado.SelectedIndexChanged

        If cboEstado.SelectedValue = "P" Or cboEstado.SelectedValue = "O" Then ' 9:pendiente
            rbtVeredicto.Enabled = True
            txtObservacion.Enabled = True
            cmdGuardar.Enabled = True
            txtObservacion.BackColor = Drawing.Color.White
        Else
            rbtVeredicto.Enabled = False
            txtObservacion.Enabled = False
            cmdGuardar.Enabled = False
            txtObservacion.BackColor = Drawing.Color.LightGray
        End If
        If cboEstado.SelectedValue = "P" Then

        End If
        gvCabOrden.DataBind()
        gvDetalleCompra.DataBind()
        gvPedidos.DataBind()
    End Sub


    Protected Sub rbtVeredicto_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbtVeredicto.SelectedIndexChanged
        If rbtVeredicto.SelectedValue = "X" Then
            pnlDerivar.Visible = True
            cboPersonalDerivar.SelectedIndex = 0
        Else
            pnlDerivar.Visible = False
        End If

    End Sub

    Protected Sub gvCabOrden_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvCabOrden.SelectedIndexChanged
        Try
            Dim log As New ClsLogistica
            Dim fun As New ClsFunciones
            Dim datosRelacion As New Data.DataTable
            Dim datos, datosDerivar As New Data.DataTable
            Dim tipoOrden As String
            Dim codigo_rco As Int64
            codigo_rco = gvCabOrden.DataKeys.Item(gvCabOrden.SelectedIndex).Values(0)
            If gvCabOrden.DataKeys.Item(gvCabOrden.SelectedIndex).Values(2).ToString.Trim = "O/C" Then
                tipoOrden = "C"
            Else
                tipoOrden = "S"
            End If
            If gvCabOrden.SelectedRow.Cells(10).Text > 1000 Then
                datos = log.ConsultarNivelInstancia(codigo_rco, Request.QueryString("id"))
                If datos.Rows.Count > 0 Then
                    datosDerivar = log.ConsultarPersonalDerivacionCompras(tipoOrden, datos.Rows(0).Item("nivel_cic"))
                    hddPersonal.Value = datos.Rows(0).Item("Personal")
                    If datosDerivar.Rows.Count > 0 Then
                        fun.CargarListas(cboPersonalDerivar, datosDerivar, "codigo_Per", "persona")
                        fun.CargarListas(cboDerivar, datosDerivar, "codigo_cic", "persona")
                    Else
                        CargarVeredicto()
                    End If
                End If
            End If
            If cboEstado.SelectedValue = "O" Then
                Dim datosOrden As Data.DataTable
                datosOrden = log.ConsultarOrdenObservadaPendiente(gvCabOrden.DataKeys.Item(gvCabOrden.SelectedIndex).Values(1))
                If datosOrden.Rows.Count > 0 Then
                    Me.cmdGuardar.Enabled = False
                Else
                    Me.cmdGuardar.Enabled = True
                End If
            End If
            '10.03.14 para poder modificar el estado de los pedidos cuando la orden sea rechazada
            'fcastillo
            datosRelacion = log.ConsultarDetallePedidosOrden(codigo_rco)
            'Response.Write("cANTIDAD " & codigo_rco)
            Me.gvDetalleOrdenPedido.DataSource = datosRelacion
            Me.gvDetalleOrdenPedido.DataBind()

        Catch ex As Exception
            'Response.Write(ex.Message)
            ScriptManager1.RegisterStartupScript(Me, Me.GetType, "error", "alert('Ocurrió un error al procesar los datos: -" & ex.Message & "')", True)

        End Try
        


    End Sub

    Protected Sub cboPersonalDerivar_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboPersonalDerivar.SelectedIndexChanged
        For i As Int16 = 0 To Me.cboPersonalDerivar.Items.Count - 1
            'Response.Write(Me.cboDerivar.Items(i).Text.Trim)
            If Me.cboDerivar.Items(i).Text.Trim = cboPersonalDerivar.SelectedItem.Text.Trim Then
                Me.cboDerivar.SelectedIndex = i
            End If
        Next
    End Sub

    Protected Sub lnkRevisiones_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkRevisiones.Click
        Me.pnlObservaciones.Visible = True
        Me.pnlDatosGenerales.Visible = False
    End Sub

    Protected Sub lnkDatosGenerales_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkDatosGenerales.Click
        Me.pnlObservaciones.Visible = False
        Me.pnlDatosGenerales.Visible = True
    End Sub

    Protected Sub gvRevisiones_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvRevisiones.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            If e.Row.Cells(2).Text = "Pendiente" Then
                e.Row.ForeColor = Drawing.Color.Red
            ElseIf e.Row.Cells(2).Text = "Conforme" Then
                e.Row.ForeColor = Drawing.Color.Green
            Else
                e.Row.ForeColor = Drawing.Color.Blue

            End If
        End If
    End Sub

    Protected Sub gvCabOrden_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvCabOrden.RowCommand
        Dim lnkView As LinkButton
        Dim gvr As GridViewRow

        ''gvr = CType(CType(sender, Control).NamingContainer, GridViewRow)
        ''ClientScript.RegisterStartupScript(Me.GetType, "Alerta", "alert('" & gvr.RowIndex & "')", True)
        ''ClientScript.RegisterStartupScript(Me.GetType, "Script", "javascript:__doPostBack('gvCabOrden','');", True)
        If e.CommandName = "Obs" Then
            'lnkView = e.CommandSource
            'string dealId = lnkView.CommandArgument;
            gvr = CType(CType(e.CommandSource, Control).NamingContainer, GridViewRow)
            Me.gvCabOrden.SelectedIndex = gvr.RowIndex

            'Response.Write("<script>var obj = document.getElementById('cmdShowDialog');if (obj) {obj.click();} </script>")
            'ClientScript.RegisterStartupScript(Me.GetType, "<script></script>", Script)

        End If
       
    End Sub

End Class
