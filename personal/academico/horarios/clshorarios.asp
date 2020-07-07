<!--#include file="../../../funciones.asp"-->
<%
'***************************************************************************************
'CV-USAT
'Archivo			: clshorarios
'Autor				: Gerardo Chunga Chinguel
'Fecha de Creación	: 09/03/2006 11:09:58 p.m.
'Observaciones		: Clase para procesos de módulo de horarios
'***************************************************************************************

Function VistaHorario(ByVal rs)
	Dim Cadena
	Dim nFila
	Dim hora,Celdas

	cadena = CabezeraTabla
	'Crear filas
    nFila = 7
    Hora = 6
    For nFila = 0 To 15
        Hora = Hora + 1
        cadena = cadena & "<TR>" & vbCrLf & CrearCeldas(0, Hora, nFila, rs) & "</TR>" & vbCrLf
    Next
	VistaHorario = Cadena & PieTabla
End Function

Function vstCursoProfesor(ByVal rs)
	Dim Cadena
	Dim nFila
	Dim hora,Celdas

    nFila = 7
	Hora = 6
    For nFila = 0 To 15
        Hora = Hora + 1
        cadena = cadena & "<TR>" & vbCrLf & CrearCeldas(0, Hora, nFila, rs) & "</TR>" & vbCrLf
    Next
	vstCursoProfesor=Cadena
End Function

Function CabezeraTabla()
	dim cadena
	cadena = "<TABLE id='tblhorario' width='100%' style='border-collapse: collapse' class=contornotabla bordercolor='#F8F8F8' cellpadding='2' cellspacing='0' border='1'>"
	Cadena = Cadena & "<TR>"
	Cadena = Cadena & "<TD class='EncabezadoHorario' width='12%' height='21'>HORA</TD>"
	Cadena = Cadena & "<TD class='EncabezadoHorario' width='12%' height='21'>LUNES</TD>"
	Cadena = Cadena & "<TD class='EncabezadoHorario' width='12%' height='21'>MARTES</TD>"
	Cadena = Cadena & "<TD class='EncabezadoHorario' width='12%' height='21'>MIÉRCOLES</TD>"
	Cadena = Cadena & "<TD class='EncabezadoHorario' width='12%' height='21'>JUEVES</TD>"
	Cadena = Cadena & "<TD class='EncabezadoHorario' width='12%' height='21'>VIERNES</TD>"
	Cadena = Cadena & "<TD class='EncabezadoHorario' width='12%' height='21'>SÁBADO</TD>"
	Cadena = Cadena & "</TR>" & vbCrLf

	CabezeraTabla=cadena
End function

function PieTabla()
	PieTabla="</TABLE>" & vbCrLf
end function

Function CrearCeldas(ByVal ColHoras, ByVal CeldaHora, ByVal nFila, ByVal rs)
    Dim nCol, Dia, HoraIni, HoraFin, NumHoras
    Dim Cadena
    Dim HayHorarios
    
    HayHorarios = False
    NumHoras = 0
    
    If Not (rs.BOF And rs.EOF) Then
        HayHorarios = True
    End If

    For nCol = 0 To 6
        If ColHoras = nCol Then
            Cadena = Cadena & "<TD>" & ExtraerHora(CeldaHora) & "</TD>" & vbCrLf
        Else
            Cadena = Cadena & "<TD" 'Abrir Celda
            If HayHorarios = True Then
                rs.MoveFirst
                'Contador = 0
                Do While Not rs.EOF
                    Dia = ExtraerDia(rs("dia_Lho"))
                    HoraIni = Left(rs("nombre_hor"), 2)
                    HoraFin = Left(rs("horafin_lho"), 2)
    
                    'Verificar el día coincidente
                    If (Dia = nCol) Then
                        If (CInt(CeldaHora) = CInt(HoraIni)) Then
                            NumHoras = (HoraFin - HoraIni)
                            'Combinar <TD> en la primera fila coincidente
                            if Numhoras<=1 then
                            	if Right(trim(Cadena),1)<>")" then
                                    if rs("estadoHorario_lho") ="A"  then
                                        Cadena = Cadena & " class=" & rs("dia_Lho") & ">" & rs("profesor") & "<br><font color='blue'>(" & UCase(rs("ambiente")) & ")</font></TD>"
                                    else
		                                Cadena = Cadena & " class=" & rs("dia_Lho") & ">" & rs("profesor") & "<br><font color='red'>(" & UCase(rs("ambiente")) & ")</font></TD>"
		                            end if 
		                        end if
                            else
                            	'-->Falta agregar cuando se cruzan horarios
                                if rs("estadoHorario_lho") ="A"  then
                                    Cadena = Cadena & " rowspan=" & NumHoras & " class=" & rs("dia_Lho") & ">" & rs("profesor") & "<br><font color='blue'>(" & UCase(rs("ambiente")) & ")</font></TD>"
                                else
	                                Cadena = Cadena & " rowspan=" & NumHoras & " class=" & rs("dia_Lho") & ">" & rs("profesor") & "<br><font color='red'>(" & UCase(rs("ambiente")) & ")</font></TD>"
	                            end if 
	                        end if
                        ElseIf (CInt(CeldaHora)>=CInt(HoraIni)) And (CInt(CeldaHora) <CInt(HoraFin)) Then
                            'Verifica que la celda este combinada de acuerdo al rango,
                            'de horas y saltar el <TD>
                            If Right(Cadena, 3) = "<TD" Then
                                Cadena = Left(Cadena, Len(Cadena) - 3) 'Eliminar celda por crear, saltando el <TD rowspan
                            End If
                            'Debug.Print Cadena
                        End If
                    End If
                    rs.MoveNext
                Loop
            End If
            If Right(Cadena, 3) = "<TD" Then
                Cadena = Cadena & "></TD>" & vbCrLf 'Cerrar Celda
            End If
        End If
    Next
    CrearCeldas = Cadena
    'Debug.Print Cadena
End Function

Function vstCursoCiclo(ByVal rs)
	Dim Cadena
	Dim nFila
	Dim hora,Celdas

    nFila = 7
	Hora = 6
    For nFila = 0 To 15
        Hora = Hora + 1
        cadena = cadena & "<TR>" & vbCrLf & CrearCeldasCiclo(0, Hora, nFila, rs) & "</TR>" & vbCrLf
    Next
	vstCursoCiclo=Cadena
End Function

Function CrearCeldasCiclo(ByVal ColHoras, ByVal CeldaHora, ByVal nFila, ByVal rs)
    Dim nCol, Dia, HoraIni, HoraFin, NumHoras
    Dim Cadena,Contador,HorarioCurso
    Dim HayHorarios
    
    HayHorarios = False
    NumHoras = 0
    
    If Not (rs.BOF And rs.EOF) Then
        HayHorarios = True
    End If

    For nCol = 0 To 6
        If ColHoras = nCol Then
            Cadena = Cadena & "<TD>" & ExtraerHora(CeldaHora) & "</TD>" & vbCrLf
        Else
            Cadena = Cadena & "<TD" 'Abrir Celda
            If HayHorarios = True Then
                rs.MoveFirst
                Contador = 0
                Do While Not rs.EOF
                    Dia = rs("codigo_dia")
                    HoraIni = Left(rs("nombre_hor"), 2)
                    HoraFin = Left(rs("horafin_lho"), 2)
                    contador=contador+1
    
                    'Verificar el día coincidente
                    If (Cint(Dia) = Cint(nCol)) Then
						If (CInt(CeldaHora)>=CInt(HoraIni)) And (CInt(CeldaHora) <CInt(HoraFin)) Then
	                         cadena=cadena & " class=" & rs("dia_Lho") & ">" & rs("nombre_cur") & "<br>(Grupo " & rs("grupohor_cup") &  ")</TD>"
                        End If
                    End If
                    rs.MoveNext                    
                Loop
            End If
            If Right(Cadena, 3) = "<TD" Then
                Cadena = Cadena & "></TD>" & vbCrLf 'Cerrar Celda
            End If
            
        End If
    Next
    CrearCeldasCiclo= Cadena
End Function

Function ExtraerDia(ByVal Cadena)
    Select Case Cadena
        Case "LU": ExtraerDia = 1
        Case "MA": ExtraerDia = 2
        Case "MI": ExtraerDia = 3
        Case "JU": ExtraerDia = 4
        Case "VI": ExtraerDia = 5
        Case "SA": ExtraerDia = 6
        Case "DO": ExtraerDia = 7
    End Select
End Function

Function ExtraerHora(ByVal Cadena)
Dim HoraNormal, Tipo
    If (Cadena <> "" And Cadena <> 0) Then
        If Int(Cadena) > 12 Then
            HoraNormal = CInt(Cadena) - 12
            'Tipo = "p.m."
        Else
            HoraNormal = Cadena
            'Tipo = "a.m."
        End If
    End If
    if len(HoraNormal)=1 then HoraNormal="0" & HoraNormal
    
    '=====xxx CLASE UTILIZADA xxxx======= [ 08/11/2011 modificado ]
    'ExtraerHora = HoraNormal & ":10 " & Tipo & " - " & (HoraNormal + 1 ) & ":00 " & Tipo       'linea anterior con los 10 min
    ExtraerHora = HoraNormal & ":00 " & Tipo & " - " & (HoraNormal + 1 ) & ":00 " & Tipo        'linea para mostrar horas exactas 08/11/2011
End Function
%>