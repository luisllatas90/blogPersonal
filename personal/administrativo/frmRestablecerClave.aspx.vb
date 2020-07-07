
Partial Class administrativo_frmRestablecerClave
    Inherits System.Web.UI.Page

    Public Enum MessageType
        Success
        [Error]
        Info
        Warning
    End Enum

    Protected Sub ShowMessage(ByVal Message As String, ByVal type As MessageType)        
        Page.RegisterStartupScript("Mensaje", "<script>ShowMessage('" & Message & "','" & type & "');</script>")
    End Sub

    Protected Sub btnBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBuscar.Click
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        Dim dt As New Data.DataTable
        Try
            If (Me.txtBuscar.Text.Trim <> "") Then
                obj.AbrirConexion()
                dt = obj.TraerDataTable("ACAD_BuscarAlumno", Me.txtBuscar.Text)
                obj.CerrarConexion()

                Me.gvDatos.DataSource = dt
                Me.gvDatos.DataBind()
            Else
                ShowMessage("No se encontraron registros", MessageType.Warning)
            End If

            obj = Nothing
        Catch ex As Exception
            ShowMessage("Error: " & ex.Message.Replace("'", ""), MessageType.Error)
        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (Session("id_per") Is Nothing) Then
            Response.Redirect("../../sinacceso.html")
        End If

        If (IsPostBack = False) Then
            aviso.Visible = False
        End If
    End Sub

    Protected Sub btnRestablece_Click(ByVal sender As Object, ByVal e As System.EventArgs)

    End Sub

    Protected Sub gvDatos_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles gvDatos.RowEditing
        Dim codigo_alu As Integer
        Dim obj As New ClsConectarDatos        
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        Try
            codigo_alu = gvDatos.DataKeys.Item(e.NewEditIndex).Values("codigo_alu")

            obj.AbrirConexion()
            obj.Ejecutar("ACAD_RestableceClave", codigo_alu)
            obj.CerrarConexion()
            aviso.Visible = True
            ShowMessage("Se restableció la clave", MessageType.Success)
        Catch ex As Exception
            ShowMessage("Error: " & ex.Message, MessageType.Error)
        End Try
    End Sub
End Class
