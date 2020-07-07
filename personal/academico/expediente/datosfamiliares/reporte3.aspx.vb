
Partial Class datosfamiliares_reporte3
    Inherits System.Web.UI.Page

    Protected Sub cmdEnviar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdEnviar.Click
        Dim mail As New ClsMail
        Dim ObjCnx As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
        Dim RS As New Data.DataTable
        Dim cadenasDest As String
        cadenasDest = ""
        RS = ObjCnx.TraerDataTable("FAM_NoActualizoDatosFamiliares", "NO", 0)
        Dim i As Integer
        For i = 0 To RS.rows.count - 1
            Me.lbldestinatario.Text = RS.Rows(i).Item("correo")

            cadenasDest = "<html><head><meta http-equiv='Content-Type' content='text/html; charset=iso-8859-1' /><title>Mail Datos Familiares</title><style type='text/css'>"
            cadenasDest = cadenasDest + ".Estilo1 {font-family: Verdana, Arial, Helvetica, sans-serif;font-size: 12px;}</style></head><body>"
            cadenasDest = cadenasDest + "ESTIMADO(A). " & RS.Rows(i).Item("nombres_Per") & " " & RS.Rows(i).Item("apellidoPat_Per") & " " & RS.Rows(i).Item("apellidoMat_Per")
            cadenasDest = cadenasDest + "<br><br>" & Me.txtMensaje.Text
            cadenasDest = cadenasDest + "<br><br>Atentamente."
            cadenasDest = cadenasDest + "<br>Ana María Olguín Britto"
            cadenasDest = cadenasDest + "<br>Directora del Instituto de Ciencias para el Matrimonio y la Familia"
            cadenasDest = cadenasDest + "</body></html>"

            If Me.lbldestinatario.Text.Trim <> "@usat.edu.pe" Then
                'mail.EnviarMail("aolguin@usat.edu.pe", Me.lbldestinatario.Text, Me.txtAsunto.Text, Server.HtmlEncode(cadenasDest), False)
                mail.EnviarMail("aolguin@usat.edu.pe", RS.Rows(i).Item("correo"), Me.txtAsunto.Text, cadenasDest, True)
            End If
        Next
        lblMensaje.Visible = True
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim ObjCnx As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
        Dim RS As New Data.DataTable
        RS = ObjCnx.TraerDataTable("FAM_NoActualizoDatosFamiliares", "US", Request.QueryString("ID"))
        Me.lblRemitente.Text = "Ana María Olguín Britto" 'RS.Rows(0).Item(0)
        Me.lblRemitenteCorreo.Text = "aolguin@usat.edu.pe" ' RS.Rows(0).Item(1)
    End Sub
End Class
