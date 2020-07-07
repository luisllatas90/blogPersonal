
Partial Class academico_CalendarioAulaVirtual_calendarxestudiante
    Inherits System.Web.UI.Page

    Function crearJS() As String
        Dim cadena As String
        Try
            Dim obj As New ClsConectarDatos
            Dim dt As New Data.DataTable
            Dim nombre_cur As String = ""
            Dim color As String = ""
            Dim j As Integer = 0
            Dim leyenda As String = ""
            Dim dtColor As New Data.DataTable
            dtColor.Columns.Add("color", GetType(String))
            dtColor.Rows.Add("#9889B2")
            dtColor.Rows.Add("#9BBB59")
            dtColor.Rows.Add("#4BACC6")
            dtColor.Rows.Add("#DB8181")
            dtColor.Rows.Add("#90A479")
            dtColor.Rows.Add("#F79646")
            dtColor.Rows.Add("#5093E0")
            dtColor.Rows.Add("#4F81BD")
            dtColor.Rows.Add("#CC6600")

            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            dt = obj.TraerDataTable("Moodle_ConsultarEventos", CInt(Session("codigo_alu")))
            obj.CerrarConexion()
            cadena = "$(document).ready(function() {$('#calendar').fullCalendar({header: {left: 'prev,next today',center: 'title',right: 'month,basicWeek,basicDay'},editable: false, "
            If dt.Rows.Count Then
                cadena &= "events: ["
                For i As Integer = 0 To dt.Rows.Count - 1
                    If nombre_cur <> dt.Rows(i).Item("nombre_cur").ToString Then
                        color = dtColor.Rows(j).Item("color").ToString
                        j += 1
                        leyenda &= "<tr><td style=""max-width:13px;height:25px; background-color:" & color & """>&nbsp;&nbsp;&nbsp;&nbsp;</td><td>" & dt.Rows(i).Item("nombre_cur").ToString & "</td></tr>"
                    End If
                    cadena &= "{"
                    cadena &= "title: '" & dt.Rows(i).Item("name").ToString & "',"
                    cadena &= "start: new Date(" & (dt.Rows(i).Item("timestart_anno")).ToString & ", " & (CInt(dt.Rows(i).Item("timestart_mes")) - 1).ToString & "," & (dt.Rows(i).Item("timestart_dia")).ToString & " ," & (dt.Rows(i).Item("timestart_hora")) & "," & (dt.Rows(i).Item("timestart_minuto")) & "),"
                    cadena &= "end: new Date(" & (dt.Rows(i).Item("timestart_anno")).ToString & ", " & (CInt(dt.Rows(i).Item("timestart_mes")) - 1).ToString & "," & (dt.Rows(i).Item("timestart_dia")).ToString & " ," & (dt.Rows(i).Item("timestart_hora")) & "," & (dt.Rows(i).Item("timestart_minuto")) & "),"
                    cadena &= "allDay: false"
                    cadena &= ", backgroundColor: '" & color & "'"
                    cadena &= ", borderColor: '" & color & "'"
                    cadena &= "},"
                    nombre_cur = dt.Rows(i).Item("nombre_cur").ToString
                Next
                cadena = cadena.Substring(0, cadena.Length - 1)
                cadena &= "]});});"
                cadena &= " var leyenda = '" & leyenda & "';"
            End If
            dt = Nothing
        Catch ex As Exception
            cadena = ex.Message & " - " & ex.StackTrace
        End Try

        Return cadena
    End Function
    Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Response.ContentType = "text/javascript"
        Response.Write(crearJS)
    End Sub
End Class
