
Partial Class Equipo_Administrarsolicitud

    Inherits System.Web.UI.Page

    Protected Sub gvsolicitud_databound(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvSolicitud.DataBound
        Me.LblRegistros.Text = "total de registros: " & Me.gvSolicitud.Rows.Count
    End Sub

    Protected Sub CmdGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdGuardar.Click
        Dim Objcnx As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
        Objcnx.IniciarTransaccion()
        Try
            If Me.gvSolicitud.Rows.Count > 0 Then
                For j As Integer = 0 To Me.gvSolicitud.Rows.Count
                    For i As Integer = 0 To Me.gvSolicitud.Controls(0).Controls(j).Controls(5).Controls.Count - 1
                        Dim controlCombo As DropDownList
                        Dim ControlTexto As TextBox
                        Dim CajaOculta As HiddenField
                        CajaOculta = Me.gvSolicitud.Controls(0).Controls(j).Controls(11).Controls(i)
                        controlCombo = Me.gvSolicitud.Controls(0).Controls(j).Controls(5).Controls(i)
                        ControlTexto = Me.gvSolicitud.Controls(0).Controls(j).Controls(10).Controls(i)
                        ' El numero de la solicitud es celda.text
                        ' El valor del combo a grabar es controlcombo.selectedvalue 
                        Objcnx.Ejecutar("paReq_ActualizarEstadoSolicitud", CajaOculta.Value.Replace(",", ""), controlCombo.SelectedValue, Request.QueryString("id"), ControlTexto.Text)
                        'para actualizar bitacora - codigo_per
                        'Response.Write(controlCombo.SelectedValue & "--" & ControlTexto.Text & "--" & CajaOculta.Value.Replace(",", "") & "<br>") '& celda.Text & "<br>")
                    Next
                Next
                Response.Write("<SCRIPT>alert('Se actualizaron los datos correctamente'); </SCRIPT>")
            Else
                Response.Write("<script>alert('No hay registros para guardar')</script>")
            End If
            Objcnx.TerminarTransaccion()
            Objcnx = Nothing
        Catch ex As Exception
            Objcnx.AbortarTransaccion()
            Response.Write("<SCRIPT>alert('Ocurrio un error al procesar los datos')</SCRIPT>")
        End Try
    End Sub


    Protected Sub CboTabla_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CboTabla.SelectedIndexChanged
        Dim ObjCnx As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
        If Me.CboTabla.SelectedValue = 0 Then
            Me.CboCampo.Visible = True
            'Me.CboTabla.SelectedValue = 0
        ElseIf CboTabla.SelectedValue = 1 Then
            Me.CboCampo.Visible = True
            ClsFunciones.LlenarListas(Me.CboCampo, ObjCnx.TraerDataTable("paReq_ConsultarEstado"), "id_est", "descripcion_est", "-- Seleccionar Estado --")
            Me.CboCampo.SelectedValue = 0
        ElseIf CboTabla.SelectedValue = 2 Then
            Me.CboCampo.Visible = True
            ClsFunciones.LlenarListas(Me.CboCampo, ObjCnx.TraerDataTable("paReq_ConsultarCentroCosto"), "codigo_cco", "descripcion_cco", "-- Seleccionar Área --")
            Me.CboCampo.SelectedValue = 0
        End If
        ObjCnx = Nothing
    End Sub

    Protected Sub CmdBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdBuscar.Click
        'Dim Objcnx As New ClsSqlServer(ConfigurationManager.ConnectionStringS("CNXBDUSAT").ConnectionString)
        'Me.gvSolicitud.DataSourceID = Nothing
        'Me.gvSolicitud.DataSource = Objcnx.TraerDataTable("paReq_SolicitudPorResponsable", Request.QueryString("id"), Me.CboCampo.SelectedValue, Me.CboTabla.SelectedValue)
        'Me.gvSolicitud.DataBind()
        '---
        'Dim obj As New clsRequerimientos
        'Dim valor As Int16
        'Me.gvSolicitud.DataSourceID = Nothing
        'If Me.CboTabla.SelectedValue = -1 Then
        '    valor = 0
        'Else
        '    valor = Me.CboTabla.SelectedValue
        'End If
        'Me.gvSolicitud.DataSource = obj.obtieneSolicitudesPorResponsableDeCronograma(Request.QueryString("id"), Me.CboCampo.SelectedValue, valor)
        'Me.gvSolicitud.DataBind()
        'obj = Nothing
    End Sub

    Protected Sub gvSolicitud_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvSolicitud.Load
        Me.LblRegistros.Text = "Total de Registros: " & Me.gvSolicitud.Rows.Count.ToString & "  "
    End Sub

    Protected Sub gvSolicitud_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvSolicitud.RowCreated
        Dim Objcnx As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
        Dim datos As Data.DataTable

        datos = Objcnx.TraerDataTable("dbo.paReq_ConsultarEstado")

        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim fila As Data.DataRowView
            Dim cadena As String
            fila = e.Row.DataItem
            Dim CajaOculta As New HiddenField
            Dim Combo As New DropDownList
            Dim CajaTexto As New TextBox
            e.Row.Attributes.Add("onMouseOver", "pintarcelda(this)")
            e.Row.Attributes.Add("onMouseOut", "despintarcelda(this)")
            cadena = "'AdministrarSolicitud.aspx?cod_sol=" & e.Row.Cells(0).Text & "'"
            e.Row.Attributes.Add("onClick", "javascript:ifDatos.document.location.href=" & cadena)
            Combo.ID = "valor"
            CajaOculta.ID = "valor1"
            CajaTexto.ID = "valor2"

            CajaOculta.Value = e.Row.Cells(0).Text
            CajaTexto.Text = Trim(Server.HtmlDecode(e.Row.Cells(10).Text))
            CajaTexto.Width = 200
            ClsFunciones.LlenarListas(Combo, datos, "id_est", "descripcion_est")
            Combo.SelectedValue = e.Row.Cells(5).Text

            e.Row.Cells(0).Text = e.Row.RowIndex + 1
            e.Row.Cells(11).Controls.Add(CajaOculta)
            e.Row.Cells(5).Controls.Add(Combo)
            e.Row.Cells(10).Controls.Add(CajaTexto)
        End If
    End Sub

    Protected Sub gvSolicitud_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvSolicitud.RowDataBound
        Dim Objcnx As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
        Dim datos As Data.DataTable

        datos = Objcnx.TraerDataTable("dbo.paReq_ConsultarEstado")

        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim fila As Data.DataRowView
            Dim cadena As String
            fila = e.Row.DataItem
            Dim CajaOculta As New HiddenField
            Dim Combo As New DropDownList
            Dim CajaTexto As New TextBox
            e.Row.Attributes.Add("onMouseOver", "pintarcelda(this)")
            e.Row.Attributes.Add("onMouseOut", "despintarcelda(this)")
            cadena = "'DatosSolicitud.aspx?cod_sol=" & e.Row.Cells(0).Text & "'"
            e.Row.Attributes.Add("onClick", "javascript:ifDatos.document.location.href=" & cadena)
            Combo.ID = "valor"
            CajaOculta.ID = "valor1"
            CajaTexto.ID = "valor2"

            CajaOculta.Value = e.Row.Cells(0).Text
            CajaTexto.Text = Trim(Server.HtmlDecode(e.Row.Cells(10).Text))
            CajaTexto.Width = 200
            ClsFunciones.LlenarListas(Combo, datos, "id_est", "descripcion_est")
            Combo.SelectedValue = e.Row.Cells(5).Text

            e.Row.Cells(0).Text = e.Row.RowIndex + 1
            e.Row.Cells(11).Controls.Add(CajaOculta)
            e.Row.Cells(5).Controls.Add(Combo)
            e.Row.Cells(10).Controls.Add(CajaTexto)

        End If
    End Sub

    'Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
    '    Dim Objcnx As New ClsSqlServer(ConfigurationManager.ConnectionStringS("CNXBDUSAT").ConnectionString)
    '    Objcnx.IniciarTransaccion()
    '    Try
    '        'Dim valor As String
    '        If Me.gvSolicitud.Rows.Count > 0 Then
    '            For j As Integer = 0 To Me.gvSolicitud.Rows.Count
    '                For i As Integer = 0 To Me.gvSolicitud.Controls(0).Controls(j).Controls(5).Controls.Count - 1
    '                    Dim controlCombo As DropDownList
    '                    Dim ControlTexto As TextBox
    '                    Dim CajaOculta As HiddenField
    '                    'Dim celda As TableCell
    '                    'celda = Me.gvSolicitud.Controls(0).Controls(j).Controls(0)
    '                    CajaOculta = Me.gvSolicitud.Controls(0).Controls(j).Controls(11).Controls(i)
    '                    controlCombo = Me.gvSolicitud.Controls(0).Controls(j).Controls(5).Controls(i)
    '                    ControlTexto = Me.gvSolicitud.Controls(0).Controls(j).Controls(10).Controls(i)
    '                    'valor = controlCombo.ToolTip & "<br>"
    '                    ' El numero de la solicitud es celda.text
    '                    ' El valor del combo a grabar es controlcombo.selectedvalue 
    '                    'Objcnx.Ejecutar("paReq_ActualizarEstadoSolicitud", celda.Text, controlCombo.SelectedValue, Request.QueryString("id"), ControlTexto.Text)
    '                    'para actualizar bitacora - codigo_per
    '                    Response.Write(controlCombo.SelectedValue & "--" & ControlTexto.Text & "--" & CajaOculta.Value.Replace(",", "") & "<br>") '& celda.Text & "<br>")
    '                Next
    '            Next
    '            Response.Write("<SCRIPT>alert('Se actualizaron los datos correctamente'); </SCRIPT>")
    '        Else
    '            Response.Write("<script>alert('No hay registros para guardar')</script>")
    '        End If
    '        Objcnx.TerminarTransaccion()
    '        Objcnx = Nothing
    '    Catch ex As Exception
    '        Objcnx.AbortarTransaccion()
    '        'Response.Write("<SCRIPT>alert('Ocurrio un error al procesar los datos')</SCRIPT>")
    '        Response.Write(ex.Message)
    '    End Try
    'End Sub

End Class

