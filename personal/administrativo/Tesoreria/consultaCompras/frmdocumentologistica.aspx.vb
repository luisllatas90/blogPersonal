
Partial Class frmdocumentologistica
    Inherits System.Web.UI.Page
    Dim cn As New clsaccesodatos
    Sub mostrarcompras(ByVal codigo_rco As Integer)
        Dim dtscabecera As System.Data.DataSet, dts As New System.Data.DataSet
        cn.abrirconexion()

        dtscabecera = cn.consultar("dbo.spConsultarLogística", "CPCORCO", codigo_rco.ToString, "", "", "", "")
        dts = cn.consultar("dbo.ConsultarDetalleCompra", codigo_rco)
        cn.cerrarconexion()


        Me.lbldocumento.Text = dtscabecera.Tables("consulta").Rows(0).Item("descripcion_tdo")
        Me.lblnumero.Text = dtscabecera.Tables("consulta").Rows(0).Item("numerodoc_rco")
        Me.lblproveedor.Text = dtscabecera.Tables("consulta").Rows(0).Item("nombres")
        Me.lbltotalcompra.Text = Format(dtscabecera.Tables("consulta").Rows(0).Item("totalcompra_rco"), "###,##0.00")
        Me.lbltotalfisico.Text = Format(dtscabecera.Tables("consulta").Rows(0).Item("totalfisico_rco"), "###,##0.00")
        Me.lbldescuento.Text = Format(dtscabecera.Tables("consulta").Rows(0).Item("descuento_rco"), "###,##0.00")
        Me.lblmoneda.Text = dtscabecera.Tables("consulta").Rows(0).Item("descripcion_tip")
        Me.lblobservacion.Text = dtscabecera.Tables("consulta").Rows(0).Item("referencia_Rco")
        Me.lblfechareg.Text = dtscabecera.Tables("consulta").Rows(0).Item("fechareg_rco")
        Me.lblfecha.Text = dtscabecera.Tables("consulta").Rows(0).Item("fechadoc_rco")
        Me.lbltipo.Text = dtscabecera.Tables("consulta").Rows(0).Item("descripcion_tcom")
        Me.lblequipo.Text = dtscabecera.Tables("consulta").Rows(0).Item("equipo_rco")
        Me.lstinformacion.DataSource = dts.Tables("consulta")

        Me.lstinformacion.DataBind()

    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim clsEncr As New EncriptaCodigos.clsEncripta, Envcodigo As String
        If Me.IsPostBack = False Then
            Envcodigo = Mid(clsEncr.Decodifica(Me.Request.QueryString("id")), 4)
            Me.mostrarcompras(Envcodigo)
        End If
    End Sub
End Class
