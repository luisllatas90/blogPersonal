
Partial Class academico_horarios_administrar_EnviarCorreoTest
    Inherits System.Web.UI.Page

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        EnviarCorreo(107244)
    End Sub

    Function EnviarCorreo(ByVal codigo_lho As Integer) As Boolean
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        Dim tbCorreo As New Data.DataTable
        obj.AbrirConexion()
        tbCorreo = obj.TraerDataTable("HorarioPE_EnviarCorreo", codigo_lho)
        obj.CerrarConexion()
        obj = Nothing
        Dim objCorreo As New ClsEnvioMailAmbiente
        Dim bodycorreo As String
        If tbCorreo.Rows.Count Then
            bodycorreo = "<html>"
            bodycorreo = bodycorreo & "<body style=""font-size:12px;text-align:justify; font-family:Tahoma;""> <div style=""color:#284775; Background-color:white; border-color:#284775; border:1px solid; padding:10px;"">"
            bodycorreo = bodycorreo & "<p><b>" & tbCorreo.Rows(0).Item("header") & "</b></p>"
            bodycorreo = bodycorreo & "<p>" & tbCorreo.Rows(0).Item("cco") & "</p>"
            bodycorreo = bodycorreo & "<p>" & tbCorreo.Rows(0).Item("descripcion") & "</p>"
            bodycorreo = bodycorreo & "<p>" & tbCorreo.Rows(0).Item("body") & "</p>"
            bodycorreo = bodycorreo & "<table style=""font-size:12px;font-family:Tahoma;border:#99bae2 1px solid;"" cellSpacing=0 cellPadding=4 border=""0"">"
            bodycorreo = bodycorreo & "<tr style=""color:  #284775; background-color:#E8EEF7; font-weight:bold;""><td>Día</td><td>Fecha</td><td>Ambiente</td><td>Horario</td><td>Capacidad</td><td>Ubicación</td></tr>"
            bodycorreo = bodycorreo & "<tr><td>" & tbCorreo.Rows(0).Item("dia") & "</td><td>" & tbCorreo.Rows(0).Item("fechaInicio") & "</td><td>" & tbCorreo.Rows(0).Item("Ambiente") & "</td><td>" & tbCorreo.Rows(0).Item("Horario") & "</td><td style=""text-align:center;"">" & tbCorreo.Rows(0).Item("capacidad") & "</td><td>" & tbCorreo.Rows(0).Item("ubicacion") & "</td></tr>"
            bodycorreo = bodycorreo & "</table>"
            bodycorreo = bodycorreo & "<p>" & tbCorreo.Rows(0).Item("footer") & "</p>"
            bodycorreo = bodycorreo & "<p> Atte,<br/><b>" & tbCorreo.Rows(0).Item("firma") & "</b></p>"
            bodycorreo = bodycorreo & "</div></body></html>"
            Try
                tbCorreo.Rows(0).Item("EnviarA") = "yperez@usat.edu.pe"
                tbCorreo.Rows(0).Item("cc") = "epena@usat.edu.pe"

                objCorreo.EnviarMailAd("campusvirtual@usat.edu.pe", tbCorreo.Rows(0).Item("firma"), tbCorreo.Rows(0).Item("EnviarA"), tbCorreo.Rows(0).Item("SubjectA") & " - Módulo de Solicitud de Ambientes", bodycorreo, True, tbCorreo.Rows(0).Item("cc"))
                Return True
            Catch ex As Exception
                Response.Write("<script>alert('" & ex.Message & "')</script>")
            End Try
        Else
            objCorreo.EnviarMailAd("campusvirtual@usat.edu.pe", "error", "yperez@usat.edu.pe", "error - Módulo de Solicitud de Ambientes", codigo_lho, True, "")
            Return True
        End If
    End Function
End Class
