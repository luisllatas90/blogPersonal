Imports System.Data
Partial Class academico_horarios_administrar_frmAmbientes
    Inherits System.Web.UI.Page


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
			If IsPostBack = False Then
					cargarListaAmbientes()
			end if
    End Sub
    Function AnchoHora(ByVal cad)
        If len(cad) < 2 Then
            AnchoHora = "0" & cad
        Else
            anchohora = cad
        End If
    End Function
    Sub cargarListaAmbientes()
        Try
            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            Dim texto As String
            Dim tb As New Data.DataTable
            'Me.gridLista.DataSource = obj.TraerDataTable("ACAD_HorariosAmbiente", 0, 1, 73, 0, 0)
            tb = obj.TraerDataTable("ACAD_HorariosAmbiente", 0, 1, 73, 0, 0)
            Dim dia, hora
            Dim diaBD, inicioBD, finBD
            Dim TextoCelda
            Dim marcas
            Dim clase
            Dim strAmbiente

           

            marcas = 0
            hora = 0

            '******************************************************
            'Imprimir cabezeras de días
            '******************************************************
            Response.Write("<table id='tblHorario' cellpadding=2 style='border-collapse: collapse;' border='1' bgcolor='white' bordercolor='#CCCCCC'>" & vbcrlf)
            Response.Write(vbtab & "<thead>" & vbcrlf)
            Response.Write(vbtab & "<tr class='etiquetaTabla'>" & vbcrlf)
            Response.Write(vbtab & "<th rowspan='2' width='300px'>AMBIENTE</th>" & vbcrlf)
            Response.Write(vbtab & "<th colspan='15'>LUNES</th>" & vbcrlf)
            Response.Write(vbtab & "<th colspan='15'>MARTES</th>" & vbcrlf)
            Response.Write(vbtab & "<th colspan='15'>MIÉRCOLES</th>" & vbcrlf)
            Response.Write(vbtab & "<th colspan='15'>JUEVES</th>" & vbcrlf)
            Response.Write(vbtab & "<th colspan='15'>VIERNES</th>" & vbcrlf)
            Response.Write(vbtab & "<th colspan='15'>SÁBADO</th>" & vbcrlf)
            Response.Write(vbtab & "</tr>" & vbcrlf)
            '******************************************************
            'Imprimir cabezeras de horas
            '******************************************************
            Dim i As Integer = 0
            Dim c As Integer = 0

            Response.Write(vbtab & "<tr class='etiquetaTabla'>" & vbcrlf)
            If (tb.Rows.Count > 0) Then
                For c = 0 To 89
                    'clase=""
                    If (hora Mod 21) = 0 Then
                        hora = 7
                    Else
                        hora = hora + 1
                    End If

                    hora = AnchoHora(hora)

                    Response.Write(vbtab & "<td" & clase & " height='10px'>" & hora & "</td>" & vbcrlf)
                Next
            Else
                Response.Write(vbtab & "<td colspan='91'" & clase & "'>" & hora & "</td>" & vbcrlf)
            End If

            Response.Write(vbtab & "</tr>" & vbcrlf)
            Response.Write(vbtab & "</thead>" & vbcrlf)
            Response.Write(vbtab & "<tbody>" & vbcrlf)


            strAmbiente = ""
          

            If (tb.Rows.Count > 0) Then

                For i = 0 To tb.Rows.Count - 1

                    For c = 0 To 90


                        If c = 0 Then
                            'Response.Write("algo2")
                            Response.Write("<td width='300px' class='etiquetaTabla2' style='text-align:left'>" & "[" & tb.Rows(i).Item("ambiente_real") & "]</td>")
                        Else
                            'Response.Write("algo3")
                            If c = 1 Then dia = "LU"
                            If c = 16 Then dia = "MA"
                            If c = 31 Then dia = "MI"
                            If c = 46 Then dia = "JU"
                            If c = 61 Then dia = "VI"
                            If c = 76 Then dia = "SA"

                        End If
                    Next
                Next
            Else
                Response.Write("<tr height='30px'><td></td><td colspan='91' style='text-align:left' class='usattitulousat'>No se han encontrado horarios registrados en la base de datos</td></tr>")
            End If

           
            Response.Write(vbtab & "</tbody>" & vbcrlf)
            Response.Write("</table>")
           
          
            obj.CerrarConexion()
            obj = Nothing
        Catch ex As Exception
            Response.Write(ex.message)
        End Try
    End Sub

      
End Class

