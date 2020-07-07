
Partial Class logistica_OrdenesObservadas
    Inherits System.Web.UI.Page

    Protected Sub gvCabOrden_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvCabOrden.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes.Add("OnMouseOver", "Resaltar(1,this,'S')")
            e.Row.Attributes.Add("OnMouseOut", "Resaltar(0,this,'S')")
            e.Row.Attributes.Add("OnClick", "javascript:__doPostBack('gvCabOrden','Select$" & e.Row.RowIndex & "');")
            e.Row.Style.Add("cursor", "hand")
        End If
    End Sub

    Protected Sub gvCabOrden_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvCabOrden.SelectedIndexChanged
        Dim log As New ClsLogistica
        Dim fun As New ClsFunciones
        Dim datos, datosDerivar As New Data.DataTable
        Dim tipoOrden As String
        Dim codigo_rco As Int64
        codigo_rco = gvCabOrden.DataKeys.Item(gvCabOrden.SelectedIndex).Values(0)
        Me.lblObservacionesOrden.Text = gvCabOrden.DataKeys.Item(gvCabOrden.SelectedIndex).Values(4)
        If gvCabOrden.DataKeys.Item(gvCabOrden.SelectedIndex).Values(2).ToString.Trim = "O/C" Then
            tipoOrden = "C"
        Else
            tipoOrden = "S"
        End If
    End Sub

    Protected Sub cmdGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdGuardar.Click
        Dim objCnx As New ClsConectarDatos
        objCnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        objCnx.AbrirConexion()
        objCnx.TraerDataTable("LOG_ActualizarOrdenObservada", gvCabOrden.DataKeys.Item(gvCabOrden.SelectedIndex).Values(3), Me.txtObservacion.Text)
        objCnx.CerrarConexion()
        ClientScript.RegisterStartupScript(Me.GetType, "Alerta", "alert('Se guardaron los datos satisfactoriamente');", True)
        EnviarMailOrdenObservada(gvCabOrden.DataKeys.Item(gvCabOrden.SelectedIndex).Values(0), cboEstado.SelectedItem.Text)
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
                Mensaje = Mensaje & "<tr><td colspan=2>La orden de compra número <b>" & datos.Rows(0).Item("numerodoc_rco") & _
                "</b> ha sido observada por " & Revisor & ", sirvase verificar el documento observado para proceder con la evaluación. </td></tr>"
                AsuntoCorreo = "(" & datos.Rows(0).Item("numerodoc_rco") & ") Orden de Compra observada para revisión"
            Else
                Mensaje = Mensaje & "<tr><td colspan=2>La orden de servicio número <b>" & datos.Rows(0).Item("numerodoc_rco") & _
                "</b> ha sido observada por " & Revisor & ", sirvase verificar el documento observado para proceder con la evaluación. </td></tr>"
                AsuntoCorreo = "(" & datos.Rows(0).Item("numerodoc_rco") & ") Orden de Servicio observada para revisión"
            End If

            Mensaje = Mensaje & "<tr><td colspan=2></br></td></tr>"
            Mensaje = Mensaje & "<tr><td colspan=2><font color=#004080>------------------------------------------------------------</font></td></tr>"
            Mensaje = Mensaje & "<tr><td colspan=2><b><font color=#004080> Sistema de Compras - Campus Virtual </font></b></td></tr>"
            Mensaje = Mensaje & "</table>"

            'Correo =  "tyong@usat.edu.pe"
            Correo = "hreyes@usat.edu.pe"

            ObjMail.EnviarMail("campusvirtual@usat.edu.pe", "Sistema de Compras", Correo, AsuntoCorreo, Mensaje, True)
        End If
    End Sub

    Protected Sub lnkRevisiones_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkRevisiones.Click
        Me.pnlObservaciones.Visible = True
        Me.pnlDetalle.Visible = False
    End Sub

    Protected Sub lnkDatosGenerales_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkDatosGenerales.Click
        Me.pnlObservaciones.Visible = False
        Me.pnlDetalle.Visible = True
    End Sub

End Class
