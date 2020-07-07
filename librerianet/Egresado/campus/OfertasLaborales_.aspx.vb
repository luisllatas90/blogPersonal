Partial Class Egresado_campus_OfertasLaborales
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            CargarOfertas()
            codigo_ofeMail.Value = 0
            Session("codigo_alu") = Session("codigo_alu")
        End If
    End Sub

    Sub CargarOfertas()
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Dim dt As Data.DataTable
        dt = obj.TraerDataTable("ALUMNI_ListaOfertasxCarreraAlu", Session("codigo_alu"), "H")
        If dt.Rows.Count Then
            Me.gvOfertas.DataSource = dt
            Me.gvOfertas.DataBind()
        End If
        dt.Dispose()
        obj.CerrarConexion()
        obj = Nothing
        Me.PanelDetalles.Visible = False
        Me.PanelPostular.Visible = False
    End Sub

    Sub CargarOfertasDetalle(ByVal ofe As Integer)
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Dim dt As Data.DataTable
        dt = obj.TraerDataTable("ALUMNI_ListaOfertasxCarreraAlu", ofe, "D")
        If dt.Rows.Count Then
            Me.lblNombreOferta.Text = "Detalles de la Oferta Seleccionada: " & dt.Rows(0).Item("titulo_ofe").ToString
            Me.lblDescripcion.Text = dt.Rows(0).Item("descripcion_ofe").ToString
            Me.lblRequisitos.Text = dt.Rows(0).Item("requisitos_ofe").ToString
            Me.lblCorreo.Text = dt.Rows(0).Item("correocontacto_ofe").ToString
            Me.lblTipoTrabajo.Text = dt.Rows(0).Item("tipotrabajo_ofe").ToString
            Me.lblDuracion.Text = dt.Rows(0).Item("duracion_ofe").ToString
            Me.PanelDetalles.Visible = True
            Me.PanelPostular.Visible = False
        End If
        dt.Dispose()
        obj.CerrarConexion()
        obj = Nothing
    End Sub
    Sub CargarDatosEmail(ByVal ofeCorreo As String, ByVal codigo_ofe As Integer)
        codigo_ofeMail.Value = codigo_ofe
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Me.lblAviso2.Text = ""
        '#Verificar si ha postulado a la oferta seleccionada
        Dim dtV As Data.DataTable
        dtV = obj.TraerDataTable("Alumni_BuscarEnvioCV", CInt(Session("codigo_alu")), codigo_ofe)
        If dtV.Rows.Count Then
            Me.lblAviso2.Text = "Ya has enviado tu CV para esta oferta seleccionada"
            Me.btnEnviar.Enabled = False
            Me.PanelPostular.Visible = False
            dtV.Dispose()
            obj.CerrarConexion()
            obj = Nothing
            Exit Sub
        Else
            Dim dt As Data.DataTable
            dt = obj.TraerDataTable("ALUMNI_CargarDatosPostular", CInt(Session("codigo_alu")))
            If dt.Rows.Count Then
                Me.PanelPostular.Visible = True
                Me.lblDe.Text = "campusvirtual@usat.edu.pe"
                Me.lblPara.Text = "yperez@usat.edu.pe" 'ofeCorreo
                Me.lblAsunto.Text = " EGRESADOS USAT - Postular a Oferta Laboral "
                Me.txtMensaje.Text = ""
                Me.lblResponderA.Text = dt.Rows(0).Item("emailPrincipal_Pso").ToString
                Me.lblCC.Text = "yperez@usat.edu.pe" ' dt.Rows(0).Item("emailPrincipal_Pso").ToString
                Dim aviso As String = ""
                If Me.lblResponderA.Text = "" Then
                    aviso &= "</br> - No tienes registrado un email, actualiza tus datos personales."
                End If
                If dt.Rows(0).Item("cv_Ega").ToString = "" Then
                    aviso &= "</br> - No has cargado tu CV, actualiza tus datos profesionales."

                    Me.lblNombreCV.Text = "No se ha registrado CV"
                Else
                    Dim documento As String
                    Dim matriz As Array
                    documento = dt.Rows(0).Item("cv_Ega").ToString
                    Me.enlaceAdjunto.Attributes.Add("href", "../curriculum/" & documento)
                    matriz = Split(documento, ".")
                    Me.lblNombreCV.Text = "CV - " & dt.Rows(0).Item("Egresado").ToString & "." & matriz(1).ToString
                    Dim ruta As String = ""
                    ruta = Server.MapPath("../curriculum/" & documento)
                    Me.RutaAdjunto.Value = ruta
                End If

                If aviso <> "" Then
                    aviso = " </br>Estimado Egresado USAT, no es posible postular a esta oferta laboral </br>" & aviso
                    Me.lblAviso.Text = aviso
                    Me.btnEnviar.Enabled = False
                Else
                    Me.btnEnviar.Enabled = True
                End If
                Me.PanelDetalles.Visible = False
            End If
            dt.Dispose()
            obj.CerrarConexion()
            obj = Nothing
        End If
    End Sub

    Protected Sub verDetalles_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Try
            Dim ofe As Integer = 0
            Dim verDetalles As ImageButton
            Dim row As GridViewRow
            verDetalles = sender
            row = verDetalles.NamingContainer
            ofe = CInt(gvOfertas.DataKeys.Item(row.RowIndex).Values("codigo_ofe").ToString())
            Me.PanelDetalles.Visible = False
            Me.PanelPostular.Visible = False
            If ofe > 0 Then
                CargarOfertasDetalle(ofe)
            End If

        Catch ex As Exception
            Response.Write(ex.Message & " - " & ex.StackTrace)
        End Try
    End Sub
    Protected Sub Postular_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Try
            Dim ofeCorreo As String
            Dim ofe As Integer = 0
            Dim Postular As ImageButton
            Dim row As GridViewRow
            Postular = sender
            row = Postular.NamingContainer
            ofe = CInt(gvOfertas.DataKeys.Item(row.RowIndex).Values("codigo_ofe").ToString())
            ofeCorreo = (gvOfertas.DataKeys.Item(row.RowIndex).Values("correocontacto_ofe").ToString())
            Me.PanelDetalles.Visible = False
            Me.PanelPostular.Visible = False
            If ofe > 0 Then
                CargarDatosEmail(ofeCorreo, ofe)
            End If

        Catch ex As Exception
            Response.Write(ex.Message & " - "  & ex.StackTrace)
        End Try
    End Sub

    Protected Sub btnEnviar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEnviar.Click
        '#Enviar CV al contacto de la oferta

        Dim Mail As New ClsMail

        If (Mail.EnviarMailAd(Me.lblDe.Text, "ALUMNI USAT", Me.lblPara.Text, Me.lblAsunto.Text, Me.txtMensaje.Text, True, Me.lblCC.Text, Me.lblResponderA.Text, Me.RutaAdjunto.Value, lblNombreCV.Text)) Then
            '#Registrar el envío de CV
            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            If obj.Ejecutar("Alumni_RegistrarEgresadoEnviaCV", Session("codigo_alu"), Me.codigo_ofeMail.Value) Then
                Response.Write("<script>alert('Se ha enviado tu CV');</script>")
                Me.btnEnviar.Enabled = False
                CargarOfertas()
            End If
            obj.CerrarConexion()
            obj = Nothing
        End If

    End Sub
End Class
