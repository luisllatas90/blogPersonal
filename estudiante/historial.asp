<!--#include file="clsMatricula.asp"-->
<%
codigo_alu=session("codigo_usu")
call Enviarfin(session("codigo_usu"),"../")

'on error resume next

If session("estadodeuda_alu")="1" and session("UltimoEstadoMatricula")="N" then
	response.redirect "mensajes.asp?proceso=H"
end if
%>
<html>
<head>
<meta http-equiv="Content-Language" content="es">
<meta name="GENERATOR" content="Microsoft FrontPage 5.0">
<meta name="ProgId" content="FrontPage.Editor.Document">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<link rel="stylesheet" type="text/css" href="../private/estilo.css">
<link rel="stylesheet" type="text/css" href="../private/estiloimpresion.css" media="print"/>
<script language="JavaScript" src="../private/funciones.js"></script>
</head>
<body>
<table border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%">
  <tr>
    <td width="30%" height="30" class="usattitulo">Historial Académico</td>
    <td width="14%" height="30" class="NoImprimir" align="right">
    <input onclick="imprimir('N',0,'')" type="button" value="    Imprimir" name="cmdImprimir" class="usatimprimir"></td>
  </tr>
  </table>
<%
	Dim Matricula
	Dim TotalCreditos,NotaXcredito,Semestre
	Dim NombreCurso,EsConvalidado
 
	Set Matricula = New clsDatMatricula
		
		with Matricula
			'Set rsHistorial=.HistorialAcademico("TO",codigo_alu,session("Codigo_Cac"),"")
			Set rsHistorial=.HistorialAcademico("TO",session("codigo_alu"),session("Codigo_Cac"),"")
			If rsHistorial.recordcount=0 then
				response.write "<p class=""usatsugerencia"">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;No se han registrado Historial Académico para el estudiante.</p>"
			else%>
		<br>
		<!--#include file="fradatos.asp"-->
		<br>
<table border="1" cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="#808080" width="100%">
  <THEAD>
  <tr class="etabla">
    <td height="14">Semestre</td>
    <td height="14">Tipo</td>
    <td height="14">Código</td>
    <td height="14">Nombre del Curso</td>
    <td height="14">Ciclo</td>
    <td height="14">Créditos</td>
    <td height="14">Grupo Horario</td>
    <td height="14">Veces Desaprob.</td>
    <td height="14">Promedio</td>
  </tr>
  </THEAD>
  <%
   	Semestre=rsHistorial("descripcion_cac")
 	TotalCreditos=0:NotaXcredito=0
  	totalReg=rsHistorial.recordcount
  
  	Do while not rsHistorial.eof
  		i=i+1
	  	EsConvalidado=.VerCursoConvalidado(rsHistorial("nombre_cur"),rsHistorial("tipomatricula_dma"),NombreCurso)

	  	If Semestre<>rsHistorial("descripcion_cac") then
	  		Semestre=rsHistorial("descripcion_cac")
	    	Call .PonderadoCAC(NotaXcredito,TotalCreditos,rsHistorial("notaminima_cac"))
  			'Reinicia variables que Agrupan datos por semestre académico
  			TotalCreditos=0:NotaXcredito=0
  		End if
  		
  		If EsConvalidado=False then
			TotalCreditos=TotalCreditos + cdbl(rsHistorial("creditoCur_Dma")) 'Sumatoria de Créditos matriculados
			NotaXcredito=NotaXcredito + cdbl(rsHistorial("notacredito"))  'Sumatorio de Nota * Crédito(Calculado)
	  	End if%>
  <tr>
    <td height="14"><%=rsHistorial("descripcion_cac")%></td>
    <td height="14"><%=rsHistorial("tipoCurso_Dma")%></td>
    <td height="14"><%=rsHistorial("identificador_Cur")%></td>
    <td height="14"><%=NombreCurso%></td>
    <td align="center" height="14"><%=ConvRomano(rsHistorial("ciclo_cur"))%></td>
    <td align="center" height="14"><%=rsHistorial("creditoCur_Dma")%></td>
    <td align="center" height="14"><%=rsHistorial("grupohor_cup")%></td>
    <td align="center" height="14" class="rojo"><%=.Mensajevd(rsHistorial("vecesCurso_dma"))%></td>
    <td align="center" height="14"><%=.VerColorNota("I",rsHistorial("notafinal_dma"),rsHistorial("condicion_Dma"),False)%></td>
  </tr>
  		<%
	  	If i=totalReg then
  			Call .PonderadoCAC(cdbl(NotaXcredito),cdbl(TotalCreditos),cdbl(rsHistorial("notaminima_cac")))
	  	End if
  	
  		rsHistorial.movenext
	Loop%>
  </table>
  		<%end if
  	end with
%>
</body>
</html>
<%
Set rsHistorial=nothing
Set Matricula=Nothing

If Err.Number<>0 then
    session("pagerror")="estudiante/historial.asp"
    session("nroerror")=err.number
    session("descripcionerror")=err.description    
	response.write("<script>top.location.href='../error.asp'</script>")
End If
%>