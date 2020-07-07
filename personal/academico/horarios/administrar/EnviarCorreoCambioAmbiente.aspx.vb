
Partial Class academico_horarios_administrar_EnviarCorreoTest
    Inherits System.Web.UI.Page

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        If EnviarCorreo() Then
            Me.GridView1.DataSource = CargarEnviarNotificacionCambioAmbiente()
            Me.GridView1.DataBind()
        Else
            Response.Write("Ocurrió un error")
        End If

    End Sub
    Function CargarEnviarNotificacionCambioAmbiente() As Data.DataTable
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        Dim tbLista As New Data.DataTable
        obj.AbrirConexion()
        tbLista = obj.TraerDataTable("EnviarNotificacionCambioAmbiente", Me.ddlCiclo.selectedvalue)
        obj.CerrarConexion()
        obj = Nothing
        Me.lblExisten.Text = "Existen " & tbLista.Rows.Count.ToString & "  notificaciones por enviar."
        Return tbLista
    End Function
    Sub Marcar()

        For Each row As GridViewRow In GridView1.Rows

            Dim cb As CheckBox = DirectCast(row.FindControl("CheckSel"), CheckBox)
            If cb IsNot Nothing Then
                cb.Checked = Not cb.Checked
            End If
        Next


    End Sub
    Function EnviarCorreo() As Boolean

        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        Dim tbCorreo As New Data.DataTable
        tbCorreo = CargarEnviarNotificacionCambioAmbiente()
        Dim objCorreo As New ClsEnvioMailAmbiente
        Dim bodycorreo As String
        If tbCorreo.Rows.Count Then
            For i As Integer = 0 To tbCorreo.Rows.Count - 1
                bodycorreo = "<html>"
                bodycorreo = bodycorreo & "<body style=""font-size:12px;text-align:justify; font-family:Tahoma;""> <div style=""color:#284775; Background-color:white; border-color:#284775; border:1px solid; padding:10px;"">"
                bodycorreo = bodycorreo & "<p><b>Estimado(a): " & tbCorreo.Rows(i).Item("Estimado") & "</b></p>"
                bodycorreo = bodycorreo & "<p>Se ha realizado cambio de ambiente de la asignatura:</p>"
                bodycorreo = bodycorreo & "<table style=""font-size:12px;font-family:Tahoma;border:#99bae2 1px solid;"" cellSpacing=0 cellPadding=4 border=""1"">"
                bodycorreo = bodycorreo & "<tr style=""color:  #284775; background-color:#E8EEF7; font-weight:bold;""><td>Nombre</td><td>Horario</td><td>Escuela</td><td>Nuevo Ambiente</td></tr>"
                bodycorreo = bodycorreo & "<tr><td>" & tbCorreo.Rows(i).Item("Curso") & "</td><td>" & tbCorreo.Rows(i).Item("Horario") & "</td><td>" & tbCorreo.Rows(i).Item("Escuela") & "</td><td>" & tbCorreo.Rows(i).Item("ambActual") & "</td></tr>"
                bodycorreo = bodycorreo & "</table>"
                bodycorreo = bodycorreo & "<p>Anteriormente tenía asignado el ambiente: " & tbCorreo.Rows(i).Item("ambAnterior") & ", el ambiente ACTUAL asignado es:<b> " & tbCorreo.Rows(i).Item("ambActual") & "</b></p>"
                bodycorreo = bodycorreo & "<p> Atte,<br/><b>Dirección Académica - Campus Virtual</b></p>"
                bodycorreo = bodycorreo & "</div></body></html>"
                Try

                    'tbCorreo.Rows(i).Item("CorreoA") = "yperez@usat.edu.pe"
                    tbCorreo.Rows(i).Item("CC1") = tbCorreo.Rows(i).Item("CC1") & "," & tbCorreo.Rows(i).Item("CC2") & ""
                    objCorreo.EnviarMailAd("campusvirtual@usat.edu.pe", "Dirección Académica - Campus Virtual", tbCorreo.Rows(i).Item("CorreoA"), "Notificación: Cambio de Ambiente", bodycorreo, True, tbCorreo.Rows(i).Item("cc1"))

                    obj.AbrirConexion()
                    obj.TraerDataTable("ActualizarNotificacionCambioAmbiente", tbCorreo.Rows(i).Item("codigo_lho"))
                    obj.CerrarConexion()
                    
                Catch ex As Exception
                    Response.Write("<script>alert('" & ex.Message & "')</script>")
				   Return False
                End Try
            Next    
			Return True			
        Else
            Me.lblExisten.Text = "No existen notificaciones por enviar"
            Return True
        End If
    End Function

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (Session("id_per") Is Nothing) Then
            Response.Redirect("../../../../sinacceso.html")
        End If

        If Not Page.IsPostBack Then           
            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            ClsFunciones.LlenarListas(Me.ddlCiclo, obj.TraerDataTable("AsignarAmbiente_ListarCiclos"), "codigo_cac", "descripcion_cac")
            obj.CerrarConexion()
            obj = Nothing

        End If
    End Sub

   
    Protected Sub ddlCiclo_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlCiclo.SelectedIndexChanged
        Me.GridView1.DataSource = CargarEnviarNotificacionCambioAmbiente()
        Me.GridView1.DataBind()
    End Sub

   
    Protected Sub GridView1_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridView1.RowCommand


        Dim index As Integer = Convert.ToInt32(e.CommandArgument)
        If (e.CommandName = "Descartar") Then
            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            obj.TraerDataTable("ActualizarNotificacionCambioAmbiente", GridView1.DataKeys(index).Values("codigo_lho"))
            obj.CerrarConexion()
            obj = Nothing

            Me.GridView1.DataSource = CargarEnviarNotificacionCambioAmbiente()
            Me.GridView1.DataBind()


        End If
    End Sub
End Class