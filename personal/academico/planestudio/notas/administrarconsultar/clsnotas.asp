<!--#include file="../../../../funciones.asp"-->
<!--#include file="../../../../clsmensajes.asp"-->
<%
'***************************************************************************************
'CV-USAT
'Archivo			: clsnotas
'Autor				: USAT
'Fecha de Creación	: 09/11/200511:09:58 a.m.
'Observaciones		: Clase para procesos de módulo de notas
'***************************************************************************************

Class clsNotas

Public Function ConsultarDocente(byval tipo,byval param1,byval param2)
	Set Obj=Server.CreateObject("PryUSAT.clsDatDocente")
	Set ConsultarDocente= Obj.ConsultarDocente("RS",tipo,param1,param2)
	Set Obj=nothing
End Function

Public Function ConsultarCicloAcademico(byval tipo,byval param1)
	Set Obj=Server.CreateObject("PryUSAT.clsDatCicloAcademico")
	'Set rsDoc=Server.CreateObject("ADODB.Recordset")
	Set ConsultarCicloAcademico= Obj.ConsultarCicloAcademico ("RS",tipo,param1)
	Set Obj=nothing
End Function

Public function ConsultarCargaAcademica(Byval codigo_cac,Byval codigo_per,byval modo)
	Set Obj=Server.CreateObject("PryUSAT.clsDatDocente")
	Set ConsultarCargaAcademica=Obj.ConsultarCargaAcademica("RS",modo,codigo_cac,codigo_per)
	Set Obj=nothing
End function


Public function ConsultarRegistroNotas(Byval codigo_cup,Byval tipo)
	Set Obj=Server.CreateObject("PryUSAT.clsDatMatricula")
	Set ConsultarRegistroNotas=Obj.ConsultarAlumnosMatriculados("RS",tipo,codigo_cup,"","")
	Set Obj=nothing
End Function

Public function ConsultarCronograma(Byval codigo_cac)
	Set Obj=Server.CreateObject("PryUSAT.clsDatCronograma")
	Set ConsultarCronograma=Obj.ConsultarCronograma("RS","RN",codigo_cac)
	Set Obj=nothing
End Function

Public function ConsultarNotas(ByVal tipo,Byval param1,Byval param2, Byval param3)
	Set Obj=Server.CreateObject("PryUSAT.clsDatNotas")
		Set ConsultarNotas=Obj.ConsultarNotas("RS",tipo,param1,param2,param3)
	Set Obj=nothing
End function

Public function Colornota(Byval notaactual,byval notaminima)
	if cdbl(notaactual)>cdbl(notaminima) then
		Colornota="class=""azul"" "
	else
		Colornota="class=""rojo"" "
	end if
end function

Public function CondicionNota(byval fila,byval cond,Byval estado)
	dim mensaje,clase
	if estado="R" then
		mensaje="Retirado"
		clase="class=""usatLinkCelda"""
	elseif cond="A" then
		mensaje="Aprobado"
		clase="class=""azul"""
		else
			mensaje="Desaprobado"
			clase="class=""rojo"""		
	end if
	
	CondicionNota="<td " & clase & " id=""msgcondicion_dma" & fila & """ width=""15%"">" & mensaje & "</td>"
End function

Public Function VerCursoConvalidado(ByVal Curso,ByVal Convalidado,ByRef RetornaNombre)
	if Convalidado="C" then
		RetornaNombre=Curso & "&nbsp;<font color='#009933'><b>*</b></font>"
		VerCursoConvalidado=True
	else
		RetornaNombre=Curso
		VerCursoConvalidado=False
	end if
End Function

Public function ConsultarDptoAcademico(Byval modo,ByVal param)
	Set objDpto=Server.CreateObject("PryUSAT.clsDatDepartamentoAcademico")
		Set ConsultarDptoAcademico=objDpto.ConsultarDepartamentoAcademico("RS",modo,param)
	Set ObjDpto=nothing
end function

Sub CargaAcademica(ByVal modo,ByVal codigo_cac,ByVal codigo_per,ByVal nombre_usu,ByVal pagina,byval lista)
	dim rsCarga

	Set Obj=Server.CreateObject("PryUSAT.clsAccesoDatos")
		obj.AbrirConexion
			Set rsCarga=Obj.Consultar("ConsultarCargaAcademica","FO","12",codigo_cac,codigo_per)
		obj.CerrarConexion
  	
  		if (rsCarga.BOF and rsCarga.EOF) then
  			response.write "<p class=""usatsugerencia"">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;El docente, no tiene carga académica para el ciclo académico seleccionado</p>"
	  	else
  	%>
<html>
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<body>
<table border="1" cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%">
  <tr class="usatCeldaTitulo">
	<%if modo="R" then%> 
  	<th width="2%" align="center">ACCION</th>
  	<%end if%>
    <th width="5%">N</th>
    <th width="10%">CODIGO</th>
    <th width="30%">ASIGNATURA</th>
    <th width="10%">GRUPO HORARIO</th>
    <th width="5%">CICLO</th>
    <th width="20%">ESCUELA PROFESIONAL</th>
    <th width="5%">MATRICULADOS</th>
    <th width="5%">RETIRADOS</th>
  </tr>
  <%
  Do while not rsCarga.EOF
  	i=i+1
  %>
  <tr id="fila<%=i%>" codigo_cup="<%=rsCarga("codigo_cup")%>" onMouseOver="Resaltar(1,this,'S')" onMouseOut="Resaltar(0,this,'S')">
  	<%if modo="R" then%>
  	<td width="2%" align="center" style="cursor:hand">
	<%if rsCarga("codigo_aut")="0" and rsCarga("estadonota_cup")<>"P" then%>
  	<img src="../../../../images/bien.gif" onclick="AbrirAutorizacionNota('A',fila<%=i%>)" alt="AUTORIZAR LLENADO DE REGISTRO DE NOTAS">
	<%elseif rsCarga("estadonota_cup")<>"P" then%>
  	<img src="../../../../images/mal.gif" onclick="AbrirAutorizacionNota('D',fila<%=i%>)" alt="QUITAR AUTORIZACION DE LLENADO DE REGISTRO DE NOTAS">
	<%end if
	
	if rsCarga("estadonota_cup")<>"P" then%>
	<img src="../../../../images/menus/advertencia.gif" alt="BITACORA DE REGISTRO DE NOTAS" onclick="AbrirPopUp('../administrarconsultar/bitacoranotas.asp?codigo_cup=<%=rsCarga("codigo_cup")%>','400','600','yes','yes','yes')">
	<%end if%>
  	</td>
  	<%end if%>
    <td width="7%"><%=i%>&nbsp;</td>
    <td width="10%"><%=rsCarga("identificador_Cur")%>&nbsp;</td>
    <td width="30%"><%=AbrirRegistro(modo,rsCarga("nombre_Cur"),i,codigo_cac,codigo_per,nombre_usu,pagina,lista)%></td>
    <td width="10%"><%=rsCarga("grupoHor_Cup")%>&nbsp;</td>
    <td width="5%"><%=ConvRomano(rsCarga("ciclo_cur"))%>&nbsp;</td>
    <td width="20%"><%=rsCarga("nombre_cpf")%>&nbsp;</td>
    <td width="8%"><%=rsCarga("matriculados")%>&nbsp;</td>
    <td width="8%"><%=rsCarga("retirados")%>&nbsp;</td>
  </tr>
  	<%
  	rsCarga.movenext
  loop
  %>
</table>
</body>
</html>
<%end if
	Set rsCarga=nothing
  Set Obj=nothing
end sub

Private function AbrirRegistro(ByVal modo,ByVal texto,byVal fila,byval codigo_cac,Byval codigo_per,byVal nombre_usu,byVal pagina,byval lista)
	if modo="C" then
		AbrirRegistro=texto
	else
		AbrirRegistro="<a href=""Javascript:AbrirCargaAcademica(fila" & fila & ",'" & codigo_cac & "','" & codigo_per & "','" & nombre_usu & "','" & pagina & "','" & lista & "')"" title=""Haga clic aquí para ver el registro de alumnos matriculados"">" & texto & "</a>"
	end if
end function

End Class
%>