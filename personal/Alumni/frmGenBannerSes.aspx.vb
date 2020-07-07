
Partial Class Alumni_frmGenBannerSes
    Inherits System.Web.UI.Page
    Dim me_Empresa As New e_Empresa
    Dim md_Empresa As New d_Empresa
    Dim me_ArchivoCompartido As New e_ArchivoCompartido
    Dim md_ArchivoCompartido As New d_ArchivoCompartido
    Dim md_Funciones As New d_Funciones

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Me.txtCodigo_ofe.Text = Session("codigo_ofe")
            Dim md_oferta As New d_Oferta
            Dim me_oferta As New e_Oferta
            Dim cod_empresa As Integer
            me_oferta.codigo_ofe = Session("codigo_ofe")
            Dim dt As New Data.DataTable

            dt = md_oferta.ListaOfertaByCodOfe(me_oferta)

            With dt.Rows(0)
                Me.lblTituloOfe.Text = .Item("titulo_ofe")
                Me.txt_codigo_emp.Text = .Item("codigo_emp")
                'If (.Item("desc_banner")) = "" Then
                '    Me.lblRequiere.Text = .Item("descripcion_ofe")
                'Else
                '    Me.lblRequiere.Text = .Item("desc_banner")
                'End If
                Me.lblRequisitos.Text = Replace(.Item("requisitos_ofe"), "-", "<li>") & "</li>"
                cod_empresa = .Item("codigo_emp")
            End With

            Dim dtEmpresa As New Data.DataTable : me_Empresa = New e_Empresa

            With me_Empresa
                .operacion = "GEN"
                .codigo_emp = cod_empresa
            End With

            dtEmpresa = md_Empresa.ListarEmpresa(me_Empresa)

            If dtEmpresa.Rows.Count = 0 Then Me.imgLogo.ImageUrl = "../Alumni/img/logoEmpDefault.jpg" : Exit Sub

            With dtEmpresa.Rows(0)

                'Obtener el logo del Shared Files
                If String.IsNullOrEmpty(.Item("IdArchivosCompartidos").ToString.Trim) Then Me.imgLogo.ImageUrl = "../Alumni/img/logoEmpDefault.jpg" : Exit Sub
                Dim ln_ArchivoCompartido As Integer = Integer.Parse(.Item("IdArchivosCompartidos").ToString.Trim)
                If ln_ArchivoCompartido = 0 Then Me.imgLogo.ImageUrl = "../Alumni/img/logoEmpDefault.jpg" : Exit Sub

                'Obtener los datos del archivo compartido
                me_ArchivoCompartido = md_ArchivoCompartido.GetArchivoCompartido(ln_ArchivoCompartido)
                Dim ls_extensiones As String = ".png .jpg .jpeg"

                'Comprobar que el archivo compartido tenga la extencion de imagen
                If Not ls_extensiones.Contains(me_ArchivoCompartido.extencion.ToLower) Then Me.imgLogo.ImageUrl = "../Alumni/img/logoEmpDefault.jpg" : Exit Sub

                me_ArchivoCompartido.usuario_act = Session("perlogin")
                me_ArchivoCompartido.ruta_archivo = ConfigurationManager.AppSettings("SharedFiles")

                Dim archivo As Byte() = md_ArchivoCompartido.ObtenerArchivoCompartido(me_ArchivoCompartido)

                Dim ms As New IO.MemoryStream(CType(archivo, Byte()))

                me_ArchivoCompartido.content_type = md_Funciones.ObtenerContentType(me_ArchivoCompartido.extencion)

                Me.imgLogo.ImageUrl = "data:" + me_ArchivoCompartido.content_type + ";base64," + Convert.ToBase64String(ms.ToArray(), 0, ms.ToArray().Length)
            End With

        Catch ex As Exception
            Response.Write(ex)
        End Try
        




    End Sub
End Class
