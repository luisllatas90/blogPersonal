
Partial Class administrativo_activofijo_L_Interfaces_prueba
    Inherits System.Web.UI.Page

    Sub Page_Load(ByVal Sender As System.Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Call mt_CargarDatos()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub



    Private Sub mt_CargarDatos()

        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        'obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        Try
            'obj.AbrirConexion()
            dt.Columns.Add("codigo_af", GetType(Integer))
            dt.Columns.Add("desc_af", GetType(String))
            dt.Columns.Add("resp_bien", GetType(String))
            dt.Columns.Add("ubicacion", GetType(String))

            dt.Rows.Add(1, "Laptop core i5", "Luis Llatas", "Laboratorio de Cómputo")
            dt.Rows.Add(2, "Mouse", "Jorge Rivera", "Laboratorio de Cómputo")

            'obj.CerrarConexion()
            Me.grwListaa.DataSource = dt
            Me.grwListaa.DataBind()

            Call mt_UpdatePanel("Lista")

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub


    Private Sub mt_UpdatePanel(ByVal ls_panel As String)
        Try
            Select Case ls_panel
                Case "Lista"


                    Me.udpDetalle.Update()
                    ' ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "udpListaUpdate", "udpListaUpdate();", True)


            End Select
        Catch ex As Exception
            'Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub


End Class
