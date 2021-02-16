Imports System.Net
Imports System.IO
Imports System.Data
Imports System.Collections.Generic

Partial Class administrativo_activofijo_DatosContables_DatosContables
    Inherits System.Web.UI.Page
  
 #Region "Declaracion de Variables"
 Dim md_Funciones As New d_Funciones

  Public Enum MessageType
        success
        [error]
        info
        warning
    End Enum
#End Region
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
            Me.grwLista.DataSource = dt
            Me.grwLista.DataBind()
            Call md_Funciones.AgregarHearders(grwLista)
            Call mt_UpdatePanel("Lista")
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub mt_UpdatePanel(ByVal ls_panel As String)
        Try
            Select Case ls_panel
                Case "Filtros"
                    'Me.udpFiltros.Update()
                    'ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "udpFiltrosUpdate", "udpFiltrosUpdate();", True)

                Case "Lista"
                'Console.WriteLine("ssss")
                'Console.ReadLine()
                 'Console.Write("Hola mundo")
                     Me.udpLista.Update()
                     
                     ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "udpListaUpdate", "udpListaUpdate();", true)
                    
                Case "Registro"
                    'Me.udpRegistro.Update()
                    'ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "udpRegistroUpdate", "udpRegistroUpdate();", True)

            End Select
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Private Sub mt_ShowMessage(ByVal Message As String, ByVal type As MessageType)
        Try
            Me.udpScripts.Update()
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "showMessage", "showMessage('" & Message & "','" & type.ToString & "');", True)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Protected Sub btnListar_Click(ByVal sender As Object, ByVal e As System.EventArgs)  Handles btnListar.Click
        Try            
           Call mt_CargarDatos()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class
