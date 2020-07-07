<!--#include file="../../../../funciones.asp"-->
<%
codigo_alu=request.querystring("codigo_alu")
codigouniver_alu=request.querystring("codigouniver_alu")
alumno=request.querystring("alumno")
nombre_cpf=request.querystring("nombre_cpf")

Public Sub PonderadoCAC(ByVal NotaCrd,ByVal TotalCrd,ByVal Limite)
' response.Write NotaCrd  & " " & TotalCrd  & " " & Limite
	Dim PondCalc,Ponderado,Cadena
		if cdbl(NotaCrd)<>0 then
			PondCalc=cdbl(NotaCrd)/cdbl(TotalCrd)
			Ponderado=VerColorNota("T",PondCalc,Limite,true)
			'response.write Limite & "-" & Ponderado
		'Else
			'Ponderado="<font color=""#FF0000"">0,00</font>"
		end if
		response.write "<tr class=""etabla"">"
	    response.write "<td height=""14"" colspan=""5"" align=""right"">TOTAL </td>"
		response.write "<td height=""14"" align=""center"">" & TotalCrd & "</td>"
		response.write "<td height=""14"">&nbsp;</td>"
		response.write "<td height=""14"">&nbsp;</td>"
		response.write "<td height=""14"">" & Ponderado & "</td>"
		response.write "<td height=""14"">&nbsp;</td>"
		response.write "<td height=""14"">&nbsp;</td></tr>"  
End Sub

Public function Mensajevd(nummat)
	if nummat<>0 then
	   Mensajevd=nummat
	else
	   Mensajevd="&nbsp;"
	end if
end function
	
Public Function VerColorNota(ByVal TipoNota,ByVal Nota,ByVal Limite,ByVal AplicaDecimales)
		Dim Color
			Nota=cdbl(Nota)
			If TipoNota="I" then 'Ponderado por curso
				Color=IIF(Limite="D","#FF0000","#0000FF")
			Else'Ponderado por semestre
				Color=IIF(Nota<cdbl(Limite),"#FF0000","#0000FF")				
			End if
			Nota=IIF(AplicaDecimales=true,FormatNumber(Nota,2),Nota)
		VerColorNota="<font color=""" & Color & """>" & Nota & "</font>"
End Function

Public Function VerCursoConvalidado(ByVal Curso,ByVal tipomatricula_dma,ByRef RetornaNombre)
		select case tipomatricula_dma
			case "C" 'Convalidado
				RetornaNombre=Curso & " <font color=""#008080"">(Convalidado)</font>"
				VerCursoConvalidado=True
			case "S" 'Suficiencia
				RetornaNombre="<font color=""#996633"">" & Curso & "</font>"
				VerCursoConvalidado=false			
			case "U" 'Ex�men de Ubicaci�n
				RetornaNombre="<font color=""#9900CC"">" & Curso & "</font>"
				VerCursoConvalidado=False
			case else
				RetornaNombre=Curso
				VerCursoConvalidado=false
		end select
End Function

%>
<html>
<head>
<meta http-equiv="Content-Language" content="es">
<meta name="GENERATOR" content="Microsoft FrontPage 12.0">
<meta name="ProgId" content="FrontPage.Editor.Document">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<link rel="stylesheet" type="text/css" href="../private/estilo.css">
<!--link rel="stylesheet" type="text/css" href="../../../../private/estiloimpresion.css" media="print"/-->
<script language="JavaScript" src="../../../../private/funciones.js"></script>
</head>
<body bgcolor="#EEEEEE">
<%
	Dim Matricula
	Dim TotalCreditos,NotaXcredito,Semestre
	Dim NombreCurso,EsConvalidado
 
	Set Obj= Server.CreateObject("PryUSAT.clsDatNotas")
		Set rsHistorial=Obj.ConsultarNotas("RS","TX",codigo_alu,session("Codigo_Cac"),"")
	Set Obj=nothing

	If rsHistorial.BOF and rsHistorial.EOF then
		response.write "<p class=""usatsugerencia"">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;No se han registrado Historial Acad�mico para el estudiante.</p>"
	Else %>
<table border="1" cellpadding="3" cellspacing="0" style="border-collapse: collapse" 
        bordercolor="#808080" width="95%" bgcolor="#FFFFFF" align="center">
  <THEAD>
  <tr bgcolor="#3366CC" style="color: #FFFFFF"><!--class="etabla"-->
    <td height="14" >Semestre</td>
    <td height="14" >�rea</td>
    <td height="14" >C�digo</td>
    <td height="14" >Nombre del Curso</td>
    <td height="14" >Ciclo</td>
    <td height="14" >Cr�ditos</td>
    <td height="14" >Grupo Horario</td>
    <td height="14" >N� Matr�culas</td>
    <td height="14" >Promedio</td>
    <td height="14" >Observaci�n</td>

    <td height="14" width="5%" >&nbsp;</td>

  </tr>
  </THEAD>
  <%
   	Semestre=rsHistorial("descripcion_cac")
 	TotalCreditos=0:NotaXcredito=0
 	TotalCreditos2=0:SumaTotal=0
        If CInt(rsHistorial.recordcount) > 0 then
         TotalCreditos2=rsHistorial("creditosTotal_mat")
         SumaTotal=rsHistorial("sumaTotal_mat")
        else
         TotalCreditos2=1
         SumaTotal=0
        end if
       
        
  	Do while not rsHistorial.eof
  		i=i+1
	  	EsConvalidado=VerCursoConvalidado(rsHistorial("nombre_cur"),rsHistorial("tipomatricula_dma"),NombreCurso)
       
       
                
	  	If Semestre<>rsHistorial("descripcion_cac") then	  	
	  		Semestre=rsHistorial("descripcion_cac")
	    	'Call PonderadoCAC(NotaXcredito,TotalCreditos,rsHistorial("notaminima_cac"))
	    	'response.Write SumaTotal  & " " & TotalCreditos2  & " " & rsHistorial("notaminima_cac")
	    	Call PonderadoCAC(cdbl(SumaTotal),cdbl(TotalCreditos2),cdbl(rsHistorial("notaminima_cac")))
  			'Reinicia variables que Agrupan datos por semestre acad�mico
  			TotalCreditos=0:NotaXcredito=0
  			
  			 TotalCreditos2=rsHistorial("creditosTotal_mat")
            SumaTotal=rsHistorial("sumaTotal_mat")
  		End if
  		
  		If EsConvalidado=False and rsHistorial("estado_dma")<>"R" then
			TotalCreditos=TotalCreditos + cdbl(rsHistorial("creditocur_dma")) 'Sumatoria de Cr�ditos matriculados
			NotaXcredito=NotaXcredito + cdbl(rsHistorial("notacredito"))  'Sumatorio de Nota * Cr�dito(Calculado)
	  	End if
	  	
	  	notaciclo="-"

		if rsHistorial("estadonota_cup")<>"P" and rsHistorial("estado_dma")<>"R" then
			notaciclo=VerColorNota("I",rsHistorial("notafinal_dma"),rsHistorial("condicion_Dma"),False)
		end if

	  	
	  	%>
  <tr >
<!--  	  	class="curso<%=rsHistorial("estado_dma")%>" id="fila<%=i%>" -->

    <td height="14"><%=rsHistorial("descripcion_cac")%></td>
    <td height="14"><%=rsHistorial("tipoCurso_Dma")%></td>
    <td height="14"><%=rsHistorial("identificador_Cur")%></td>
    <td height="14"><%=NombreCurso%></td>
    <td align="center" height="14"><%=ConvRomano(rsHistorial("ciclo_cur"))%></td>
    <td align="center" height="14"><%=rsHistorial("creditocur_dma")%></td>
    <td align="center" height="14"><%=rsHistorial("grupohor_cup")%></td>
    <td align="center" height="14" class="rojo"><%=mensajevd(rsHistorial("vecesCurso_Dma"))%></td>
    <td align="center" height="14"><%=notaciclo%></td>
    <td align="center" height="14"> <b><font color="red"> <%   if rsHistorial("estado_dma")="R" then response.write("Retirado")%>
    </font></b></td>
    <td align="center" height="14">
    <img border="0" src="../../../../images/atencion.gif" width="15" height="12"   alt="Haga click aqu� para ver detalles" onclick="javascript:window.open('frmdetalles.asp?codigo_dma=<%=rsHistorial("codigo_dma")%>&codigouniver_alu=<%=rsHistorial("codigouniver_Alu")%>' ,'TT','toolbar=no, width=1200, height=400')"></p>
    </td>
  </tr>
  		<%
	  	If i=rsHistorial.recordcount then
  			' Call PonderadoCAC(cdbl(NotaXcredito),cdbl(TotalCreditos),cdbl(rsHistorial("notaminima_cac")))
  			
  			'response.Write "FIN" & SumaTotal  & " " & TotalCreditos2  & " " & rsHistorial("notaminima_cac")
  			Call PonderadoCAC(cdbl(SumaTotal),cdbl(TotalCreditos2),cdbl(rsHistorial("notaminima_cac")))
  			
	  	End if
  	
  		rsHistorial.movenext
	Loop%>
  </table>
  		<%end if
%>
</body>
</html>
		<%
Set rsHistorial=nothing
%>