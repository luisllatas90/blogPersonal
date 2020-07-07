
Partial Class Egresado_DetOfertas
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (IsPostBack = False) Then
            If (Request.QueryString("ofe") <> Nothing) Then
                AsignaDatos(Request.QueryString("ofe"))
            End If
        End If
    End Sub

    Private Sub AsignaDatos(ByVal oferta As Integer)
        Dim dtDatos As New Data.DataTable
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        dtDatos = obj.TraerDataTable("ALUMNI_RetornaOferta", oferta)
        obj.CerrarConexion()

        If (dtDatos.Rows.Count > 0) Then
            Me.HdCodigo_ofe.Value = dtDatos.Rows(0).Item("codigo_ofe")
            Me.lblTitulo.Text = dtDatos.Rows(0).Item("titulo_ofe")
            Me.LnkEmpresa.Text = dtDatos.Rows(0).Item("nombrePro")
            Me.HdCodigo_pro.Value = dtDatos.Rows(0).Item("idPro")
            Me.lblContactos.Text = dtDatos.Rows(0).Item("contacto_ofe")
            Me.lblTelefono.Text = dtDatos.Rows(0).Item("telefonocontacto_ofe")
            Me.lblDescripcion.Text = dtDatos.Rows(0).Item("descripcion_ofe")
            Me.lblRequisitos.Text = dtDatos.Rows(0).Item("requisitos_ofe")
            Me.lblCorreo.Text = dtDatos.Rows(0).Item("correocontacto_ofe")
            Me.lblLugar.Text = (dtDatos.Rows(0).Item("lugar_ofe"))
            Me.lblTrabajo.Text = dtDatos.Rows(0).Item("tipotrabajo_ofe")
            Me.lblDuracion.Text = dtDatos.Rows(0).Item("duracion_ofe")
            Me.chlMostrar.Checked = dtDatos.Rows(0).Item("visible_ofe")
            Me.lblSector.Text = dtDatos.Rows(0).Item("nombre_sec")

            'Detalles de la Oferta
            Dim dtDetalle As New Data.DataTable
            obj.AbrirConexion()
            dtDetalle = obj.TraerDataTable("ALUMNI_DetalleOferta", Me.HdCodigo_ofe.Value)
            obj.CerrarConexion()
            Me.gvwCarreras.DataSource = dtDetalle
            Me.gvwCarreras.DataBind()

            dtDetalle.Dispose()
        End If

        dtDatos.Dispose()
        obj = Nothing
    End Sub

    Protected Sub LnkEmpresa_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LnkEmpresa.Click
        Dim Enc As New EncriptaCodigos.clsEncripta
        Response.Redirect("frmFichaEmpresa.aspx?pro=" & Enc.Codifica("069" & Me.HdCodigo_pro.Value) & "&ofe=" & Enc.Codifica("069" & Me.HdCodigo_ofe.Value))
        'Response.Redirect("frmFichaEmpresa.aspx?pro=" & Enc.Codifica("069" & Me.HdCodigo_pro.Value) & "&KeepThis=true&TB_iframe=true&height=200&width=200&modal=true'")        
    End Sub
End Class
