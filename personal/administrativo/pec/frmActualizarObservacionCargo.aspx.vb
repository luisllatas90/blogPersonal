
Partial Class administrativo_pec_frmActualizarObservacionCargo
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            txtObservacion.Text = Request.QueryString("observacion")            
        End If
    End Sub

    Protected Sub btnGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardar.Click
        Try
            Dim codigo_deu As Integer = Request.QueryString("id")
            Dim obj As New ClsConectarDatos
            Dim rspta As Integer
            Dim dt As New Data.DataTable

            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            obj.IniciarTransaccion()

            dt = obj.TraerDataTable("EPRE_ActualizarObservacion", codigo_deu, txtObservacion.Text, Request.UserHostAddress, Request.QueryString("usuario"))

            lblMensaje.Text = dt.Rows(0).Item("mensaje")
            If dt.Rows(0).Item("mensaje") = 1 Then
                lblMensaje.Text = lblMensaje.Text & "Datos guardados correctamente."
                Me.mensajes.Attributes.Add("class", "mensajeExito")
                Image1.Attributes.Add("src", "../../Images/accept.png")
            ElseIf rspta = 0 Then
                lblMensaje.Text = lblMensaje.Text & "Hubo un error en la operación, no se guardaron los datos."
                mensajes.Attributes.Add("class", "mensajeError")
                Me.Image1.Attributes.Add("src", "../../Images/exclamation.png")
            End If

            If dt.Rows.Count() > 0 Then
                Me.grw.DataSource = obj.TraerDataTable("BuscarObsDeuda", codigo_deu)
                Me.grw.DataBind()
            End If

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

End Class
