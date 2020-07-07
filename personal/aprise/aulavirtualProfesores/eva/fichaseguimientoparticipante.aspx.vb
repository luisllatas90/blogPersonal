
Partial Class librerianet_aulavirtual_eva_fichaseguimientoparticipante
    Inherits System.Web.UI.Page

   
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            Dim idusuario As String = Request.QueryString("idusuario")
            Dim idcursovirtual As String

            idcursovirtual = IIf(Request.QueryString("idcursovirtual") = "", Session("idcursovirtual2"), Request.QueryString("idcursovirtual"))
            If idcursovirtual = "" Then Response.Write("Ingrese denuevo a la opción de menú") : Exit Sub

            Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXCMUSAT").ConnectionString)
            Dim tbl As Data.DataTable

            Dim ruta As String = "../../../images/fotovacia.gif"

            tbl = obj.TraerDataTable("ConsultarUsuario", 8, idusuario, "", "")

            If tbl.Rows.Count > 0 Then
                Me.Label1.Text = tbl.Rows(0).Item("idusuario")
                Me.Label2.Text = tbl.Rows(0).Item("Nombreusuario")
                Me.Label3.Text = tbl.Rows(0).Item("email")
                'Cargar la foto
                Select Case tbl.Rows(0).Item("idtipousuario")
                    Case 2
                        Dim obEnc As Object
                        obEnc = Server.CreateObject("EncriptaCodigos.clsEncripta")

                        ruta = obEnc.CodificaWeb("069" & tbl.Rows(0).Item("idusuario").ToString)
                        '---------------------------------------------------------------------------------------------------------------
                        'Fecha: 29.10.2012
                        'Usuario: dguevara
                        'Modificacion: Se modifico el http://www.usat.edu.pe por http://intranet.usat.edu.pe
                        '---------------------------------------------------------------------------------------------------------------
                        ruta = "//intranet.usat.edu.pe/imgestudiantes/" & ruta
                        Me.imgFoto.ImageUrl = ruta
                        obEnc = Nothing

                    Case 4
                        Me.imgFoto.ImageUrl = "../../../personal/imgpersonal/" & tbl.Rows(0).Item("codigo_per") & ".jpg"

                    Case Else
                        Me.imgFoto.ImageUrl = ruta
                End Select
                Me.Visitas.DataSource = obj.TraerDataTable("ConsultarDesempeno", 1, idusuario, Session("idcursovirtual2"), "")
                Me.Visitas.DataBind()

                If Me.Visitas.Rows.Count > 0 Then
                    Me.lbldocumentos.Visible = True

                    Me.Documentos.DataSource = obj.TraerDataTable("ConsultarDesempeno", 0, idusuario, idcursovirtual, "")
                    Me.Documentos.DataBind()
                    
                End If
            End If
            obj = Nothing
        End If

    End Sub

    Protected Sub Documentos_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Documentos.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Cells(0).Text = e.Row.RowIndex + 1
        End If
    End Sub
End Class
