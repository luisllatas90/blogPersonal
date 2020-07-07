<!--#include file="../../../funciones.asp"-->
<%

'If session("codigo_usu")="" then response.redirect "../../../tiempofinalizado.asp"
if session("codigo_tfu") = "" then
    Response.Redirect("../../../sinacceso.html")
end if

codigo_alu=request.querystring("codigo_alu")
modo=request.querystring("modo")
quitar=request.querystring("quitar")
boton=request.querystring("boton")

Public Sub PonderadoCAC(ByVal NotaCrd,ByVal TotalCrd,ByVal Limite)
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
		response.write "<td height=""14"">" & Ponderado & "</td></tr>"
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

if boton ="NO" then
    boton="style='display:none'"
    session("codigo_usu")=codigo_alu
end if

if codigo_alu="" and modo="E" then
	codigo_alu=session("codigo_usu")
	modo="resultado"
end if

%>
<html>
<head>
<meta http-equiv="Content-Language" content="es">
<meta name="GENERATOR" content="Microsoft FrontPage 5.0">
<meta name="ProgId" content="FrontPage.Editor.Document">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<link rel="stylesheet" type="text/css" href="../../../private/estilo.css">
<link rel="stylesheet" type="text/css" href="../../../private/estiloimpresion.css" media="print"/>
<script language="JavaScript" src="../../../private/funciones.js"></script>
</head>
<body onload="document.all.txtcodigouniver_alu.focus()">
<table border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%">
  <tr>
    <td width="30%" height="30" class="usattitulo">Historial Acad�mico</td>
    <%if quitar="B" then%>
    <td width="43%" height="30" class="usattitulo" class="NoImprimir" <%=boton%>>
     <%call buscaralumno("estudiante/historial2.asp","../",-1)%>
    </td>
    <%end if
    if modo="resultado" then%>
    <td width="14%" height="30" class="NoImprimir" align="right">
    <input onclick="imprimir('N')" type="button" value="    Imprimir" name="cmdImprimir" class="usatimprimir"></td>
    <%end if%>
  </tr>
  </table>
<%
if modo="resultado" then
	Dim Matricula
	Dim TotalCreditos,NotaXcredito,Semestre
	Dim NombreCurso,EsConvalidado
 	
	Set Obj= Server.CreateObject("PryUSAT.clsDatNotas")
		Set rsHistorial=Obj.ConsultarNotas("RS","TO",codigo_alu,session("Codigo_Cac"),"")
	Set Obj=nothing

		If rsHistorial.BOF and rsHistorial.EOF then
				response.write "<p class=""usatsugerencia"">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;No se han registrado Historial Acad�mico para el estudiante.</p>"
		ElseIf (session("estadodeuda_alu")=1 and session("codigo_tfu")<>1 and session("codigo_tfu")<>25 and session("codigo_tfu")<>9 and session("codigo_tfu")<>26 and session("codigo_tfu")<>30 and session("codigo_tfu")<>35 and session("codigo_tfu")<>16) then%>
				<script>
					var mensaje='Lo sentimos no se puede mostrar su historial acad�mico del Estudiante, por favor '
					mensaje=mensaje + '\n comun�quese con la Oficina de Pensiones para que se le indique el motivo.'
					alert(mensaje)
					history.back(-1)
				</script>
				<%
		Else%>
		<br>
		<!--#include file="../fradatos.asp"-->
		<br>
<table border="1" cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="#808080" width="100%">
  <THEAD>
  <tr class="etabla">
    <td height="14">Semestre</td>
    <td height="14">�rea</td>
    <td height="14">C�digo</td>
    <td height="14">Nombre del Curso</td>
    <td height="14">Ciclo</td>
    <td height="14">Cr�ditos</td>
    <td height="14">Grupo Horario</td>
    <td height="14">Veces Desaprob.</td>
    <td height="14">Promedio</td>
  </tr>
  </THEAD>
  <%
   	Semestre=rsHistorial("descripcion_cac")
 	TotalCreditos=0:NotaXcredito=0
	Dim TotalGral
	Dim TotalAprobados
	
	Dim sumaTotal_mat
	dim creditosTotal_mat

	TotalGral=0
  
  	Do while not rsHistorial.eof
  		i=i+1
	  	EsConvalidado=VerCursoConvalidado(rsHistorial("nombre_cur"),rsHistorial("tipomatricula_dma"),NombreCurso)
        
        'response.write rsHistorial.Fields("descripcion_cac").Value
        
	  	If Semestre<>rsHistorial("descripcion_cac") then
	  		Semestre=rsHistorial("descripcion_cac")
	    	'Call PonderadoCAC(NotaXcredito,TotalCreditos,rsHistorial("notaminima_cac"))
	    	
	        rsHistorial.moveprevious
	    	Call PonderadoCAC(cdbl(rsHistorial("sumaTotal_mat")),cdbl(rsHistorial("creditosTotal_mat")),rsHistorial("notaminima_cac"))
	    	rsHistorial.movenext
	    	
	    		    	
			TotalGral=cdbl(TotalGral) + cdbl(TotalCreditos)
			TotalAprobados=cdbl(TotalAprobados) + cdbl(TotalAprobadosCrd)

  			'Reinicia variables que Agrupan datos por semestre acad�mico
  			TotalCreditos=0:NotaXcredito=0:TotalAprobadosCrd=0
  		End if
  		
  		
  		If EsConvalidado=False then
			TotalCreditos=TotalCreditos + cdbl(rsHistorial("creditocur_dma")) 'Sumatoria de Cr�ditos matriculados
			NotaXcredito=NotaXcredito + cdbl(rsHistorial("notacredito"))  'Sumatorio de Nota * Cr�dito(Calculado)
	  	End if

		if rsHistorial("condicion_dma")="A" and rsHistorial("estado_dma")<>"R" then
			TotalAprobadosCrd=TotalAprobadosCrd+cdbl(rsHistorial("creditocur_dma"))
		end if
	  	
	  	notaciclo="-"

		if rsHistorial("estado_dma")<>"R" then
			notaciclo=VerColorNota("I",rsHistorial("notafinal_dma"),rsHistorial("condicion_Dma"),False)
		end if
	  	%>
  <tr>
    <td height="14"><%=rsHistorial("descripcion_cac")%></td>
    <td height="14"><%=rsHistorial("tipoCurso_Dma")%></td>
    <td height="14"><%=rsHistorial("identificador_Cur")%></td>
    <td height="14"><%=NombreCurso%></td>
    <td align="center" height="14"><%=ConvRomano(rsHistorial("ciclo_cur"))%></td>
    <td align="center" height="14"><%=rsHistorial("creditocur_dma")%></td>
    <td align="center" height="14"><%=rsHistorial("grupohor_cup")%></td>
    <td align="center" height="14" class="rojo"><%=mensajevd(rsHistorial("vecesCurso_Dma"))%></td>
    <td align="center" height="14"><%=notaciclo%></td>
  </tr>
  		<%
	  	If i=rsHistorial.recordcount then
	  	
  			'Call PonderadoCAC(cdbl(NotaXcredito),cdbl(TotalCreditos),cdbl(rsHistorial("notaminima_cac")))
  			Call PonderadoCAC(cdbl(rsHistorial("sumaTotal_mat")),cdbl(rsHistorial("creditosTotal_mat")),rsHistorial("notaminima_cac"))
  			
			TotalGral=cdbl(TotalGral) + cdbl(TotalCreditos)
			TotalAprobados=cdbl(TotalAprobados) + cdbl(TotalAprobadosCrd)
	  	End if
  	
  		rsHistorial.movenext
	Loop%>
  </table>
  		<%end if
end if%>
	<h5>Total de Cr�ditos Matriculados sin Convalidaciones: <%=TotalGral%></h5>
	<h5>Total de Cr�ditos Aprobados (incluye convalidaci�n): <%=TotalAprobados%></h5>
</body>
</html>
		<%
Set rsHistorial=nothing
%>