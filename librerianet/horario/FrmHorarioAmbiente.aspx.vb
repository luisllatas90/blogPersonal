
Partial Class horario_FrmHorarioAmbiente
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim strTable As String = ""
        Dim strCabecera As String = ""
        Dim strCuerpo As String = ""
        strTable = "<table width='100%'>"
        'Cabecera
        strCabecera = "<tr height='25px' align='Center'>"
        strCabecera = strCabecera & RetornaCabecera()
        strCabecera = strCabecera & "</tr>"
        strCabecera = strCabecera & "</table>"

        'Ambientes
        strCuerpo = strCuerpo & RetornaAmbientes()

        'Agrupando
        strTable = strTable & strCabecera
        strTable = strTable & strCuerpo
        Me.tabla.InnerHtml = strTable

        Me.lblFecha.Text = "Actividades para el " & Date.Today.ToString("ddddd dd MMMM yyyy")
    End Sub

    Public Function RetornaCabecera() As String
        Dim hora As Integer = Date.Now.Hour        
        Dim strHoras As String = ""
        Try
            hora = Date.Now.Hour
            If (Date.Now.Minute > 50) Then
                hora = hora + 1
            End If

            strHoras = strHoras & "<td style='background:#e33439;color:White;'><div id='TituloAmbiente'> Ambiente <div></td>"
            While hora < Date.Now.Hour + 6                
                strHoras = strHoras & "<td width='10%' style='background:#e33439;color:White;'>" & hora & ":00</td>"
                hora = hora + 1
            End While

            Return strHoras
        Catch ex As Exception
            Response.Write("Error al asignar la hora")
            Return "<td colspan='6'>Error!</td>"
        End Try
    End Function

    Public Function RetornaAmbientes() As String
        Dim strColumnas As String = ""
        Dim strDatos As String = ""
        Dim hora As Integer = RetornaHora()
        Dim dt, dtHorario As New Data.DataTable
        Dim fila As Integer = 0
        Dim contador As Integer = 0
        Dim colspan As Integer = 0
        Dim strColor As String = ""
        Dim Ubicacion As Integer = 1
        Dim Piso As Integer = 0
        Dim swCorte As Integer = 1
        Dim cuentaFilas As Integer = 0
        Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("cnxBDUSAT").ConnectionString)
        Try
            dt = obj.TraerDataTable("HorarioPE_ConsultarAmbientes_v2")
            dtHorario = obj.TraerDataTable("HOR_MuestraHorarioAmbiente", RetornaDia, hora, hora + 5)

            For i As Integer = 0 To dt.Rows.Count - 1
                cuentaFilas = cuentaFilas + 1
                If (swCorte = 1) Then
                    fila = i
                    cuentaFilas = 0
                    strColumnas = strColumnas & "<input type='hidden' id='Ub-" & contador & "' value='" & dt.Rows(i).Item("descripcion_ube") & "' />"
                    strColumnas = strColumnas & "<input type='hidden' id='Pi-" & contador & "' value='" & dt.Rows(i).Item("piso_amb") & "' />"
                    strColumnas = strColumnas & "<div id='tabla1'>"
                    strColumnas = strColumnas & "<table id='tb-" & contador & "' name='tblhorario' width='100%' value='" & contador & "' style='display:none'>"
                    contador = contador + 1
                    swCorte = 0
                    Ubicacion = dt.Rows(i).Item("ordenEdificio")
                    Piso = dt.Rows(i + 1).Item("piso_amb")
                End If

                strColumnas = strColumnas & "<tr height='30px'>"
                strColumnas = strColumnas & "<td width='40%' class='tbDetalle'>" & dt.Rows(i).Item("ambiente") & "</td>"

                For j As Integer = hora To hora + 5
                    strDatos = RetornaDatos(dtHorario, j, dt.Rows(i).Item("codigo_amb"))
                    colspan = RetornaColspan(dtHorario, j, dt.Rows(i).Item("codigo_amb"))
                    strColor = RetornaColor(dtHorario, j, dt.Rows(i).Item("codigo_amb"))

                    If (colspan < 1) Then
                        strColumnas = strColumnas & "<td width='10%' id='c" & i & "f" & j & "' class='celda' style='background:" & strColor & "'>" & strDatos & "</td>"
                    Else
                        strColumnas = strColumnas & "<td name='bloqueHora' width='" & colspan * 10 & "%' id='c" & i & "f" & j & "' class='celda' colspan='" & colspan & "' style='background:" & strColor & "'>" & strDatos & "</td>"
                        j = j + (colspan - 1)
                    End If

                Next
                strColumnas = strColumnas & "</tr>"

                If (i < dt.Rows.Count - 1) Then
                    If (Ubicacion <> dt.Rows(i + 1).Item("ordenEdificio")) Then
                        Ubicacion = dt.Rows(i).Item("ordenEdificio")
                        'fila = fila + (12 - (i Mod 12))
                        swCorte = 1
                        cuentaFilas = 11
                        Piso = 1
                    Else
                        If (Piso <> dt.Rows(i + 1).Item("piso_amb") And _
                            dt.Rows(i + 1).Item("ordenEdificio") < 4) Then
                            'fila = fila + (12 - (i Mod 12))
                            swCorte = 1
                            cuentaFilas = 11
                            Piso = dt.Rows(i + 1).Item("piso_amb")
                        Else
                            If (cuentaFilas = 10) Then
                                swCorte = 1
                                cuentaFilas = 11
                                Piso = dt.Rows(i + 1).Item("piso_amb")
                            Else
                                swCorte = 0
                            End If

                        End If
                    End If
                End If

                If (swCorte = 1) Then
                    cuentaFilas = 0
                    strColumnas = strColumnas & "</table></div>"
                End If
            Next

            Return strColumnas
        Catch ex As Exception
            Response.Write(ex.Message)
            Return "<tr><td colspan='6'>Error!</td></tr>"
        End Try
    End Function

    Private Function RetornaDia() As String
        Select Case Date.Now.DayOfWeek
            Case 1 : Return "LU"
            Case 2 : Return "MA"
            Case 3 : Return "MI"
            Case 4 : Return "JU"
            Case 5 : Return "VI"
        End Select

        Return ""
    End Function

    Private Function RetornaHora() As Integer
        Dim hora As Integer = Date.Now.Hour
        hora = Date.Now.Hour
        If (Date.Now.Minute > 50) Then
            hora = hora + 1
        End If

        Return hora
    End Function

    Private Function RetornaDatos(ByVal dt As Data.DataTable, ByVal hora As Integer, ByVal ambiente As Integer) As String
        Dim horaInicioTabla As Integer
        Dim horaFinTabla As Integer
        For i As Integer = 0 To dt.Rows.Count - 1
            horaInicioTabla = dt.Rows(i).Item("nombre_hor").ToString.Substring(0, 2)
            horaFinTabla = dt.Rows(i).Item("horaFin_Lho").ToString.Substring(0, 2)

            If (dt.Rows(i).Item("codigo_amb") = ambiente) Then
                If (hora >= horaInicioTabla And hora < horaFinTabla) Then
                    Return dt.Rows(i).Item("nombre_cur") & " - " & dt.Rows(i).Item("grupoHor_cup") & " (" & dt.Rows(i).Item("abreviatura_cpf") & ")<br/>" & dt.Rows(i).Item("docente")
                End If
            End If
        Next

        Return "&nbsp;"
    End Function

    Private Function RetornaColspan(ByVal dt As Data.DataTable, _
                                    ByVal hora As Integer, _
                                    ByVal ambiente As Integer) As Integer
        Dim horaInicioTabla, horaFinTabla As Integer
        Dim colspan As Integer = 0
        For i As Integer = 0 To dt.Rows.Count - 1            
            If (dt.Rows(i).Item("codigo_amb") = ambiente) Then
                horaInicioTabla = dt.Rows(i).Item("nombre_hor").ToString.Substring(0, 2)
                horaFinTabla = dt.Rows(i).Item("horaFin_Lho").ToString.Substring(0, 2)

                If (hora >= horaInicioTabla And hora < horaFinTabla) Then
                    'colspan = colspan + 1
                    colspan = horaFinTabla - horaInicioTabla
                End If
            End If
        Next

        If (colspan <= 1) Then
            Return colspan = 0
        End If
        Return colspan
    End Function

    Private Function RetornaColor(ByVal dt As Data.DataTable, _
                                    ByVal hora As Integer, _
                                    ByVal ambiente As Integer) As String
        Dim horaInicioTabla, horaFinTabla As Integer        
        For i As Integer = 0 To dt.Rows.Count - 1
            If (dt.Rows(i).Item("codigo_amb") = ambiente) Then
                horaInicioTabla = dt.Rows(i).Item("nombre_hor").ToString.Substring(0, 2)
                horaFinTabla = dt.Rows(i).Item("horaFin_Lho").ToString.Substring(0, 2)

                If (hora >= horaInicioTabla And hora < horaFinTabla) Then                    
                    If (dt.Rows(i).Item("codigo_ambts").ToString.Trim = "") Then
                        Select Case dt.Rows(i).Item("codigo_test").ToString.Trim
                            Case "1" : Return "#DEB0EF" 'Escuela Pre
                            Case "2" : Return "#E3CD54" 'PreGrado
                            Case "3" : Return "#A2A0A2" 'Profesionalización
                            Case "5" : Return "#96C1EB" 'PostGrado
                            Case "8" : Return "#FFA200" '2da. Especialidad
                            Case Else : Return "#56FFEE"
                        End Select
                    Else
                        Select Case dt.Rows(i).Item("codigo_ambts").ToString
                            Case "1", "2", "3", "4", "8", "11" : Return "#36A168" 'Academico
                            Case "5", "6", "7", "9" : Return "#BC995C" 'Evento
                            Case "10" : Return "#FF2D29" 'Sustentacion de tesis
                            Case Else : Return "#D3D3D3"
                        End Select
                    End If

                End If
            End If
        Next

        Return "#FFFFFF"
    End Function
End Class
