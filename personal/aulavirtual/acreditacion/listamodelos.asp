<!--#include file="../NoCache.asp"-->
<!--#include file="clsAcreditacion.asp"-->
<%
if session("codigo_usu")="" then response.Redirect "../tiempofinalizado.asp"

dim acreditacion
Set acreditacion=new clsacreditacion
idEscuela=request.QueryString("idEscuela")
NombreEscuela=request.QueryString("NombreEscuela")

if idEscuela="" then idEscuela=5

	with acreditacion
		ArrModelo=.ConsultarModelo(5,idEscuela,0,0)
		
		if IsEmpty(ArrModelo)=false then
			if Ubound(ArrModelo,2)=0 then
				response.redirect ("abrirmodelo.asp?idacreditacion=" & ArrModelo(0,0) & "&idmodelo=" & ArrModelo(1,0) & "&titulomodelo=" & ArrModelo(5,0) & "&idcarrera=" & idEscuela & "&nombrecarrera=" & nombreescuela)
			end if
		end if
	end with
	Set acreditacion=nothing
%>
<HTML>
	<HEAD>
		<Title>Acceso al Sistema</Title>
		<link rel="stylesheet" type="text/css" href="../../../private/estiloaulavirtual.css">
			<script language="JavaScript" src="../../../private/funcionesaulavirtual.js"></script>
	</HEAD>
	<BODY topmargin="0" leftmargin="0">
		<div id="encabezado">
			<table width="100%" border="0" style="BORDER-COLLAPSE: collapse" bordercolor="#111111" cellpadding="0" cellspacing="0">
				<tr>
					<th scope="row" width="20%" rowspan="4">
						<img src="../../../images/logo.jpg"></th>
				</tr>
				<tr>
					<td class="e1" height="40" align="left" width="80%" colspan="2">Documentos para la 
						Acreditación o Funcionamiento de Escuelas Profesionales
					</td>
				</tr>
				<tr>
					<td height="15" align="right" width="10%"><b>Usuario </b>
					</td>
					<td height="15" align="left" width="90%" class="aviso">:<%=session("nombre_usu")%></td>
				</tr>
			</table>
		</div>
		<br>
		<center>
				<%if IsEmpty(Arrmodelo) then
					response.Write "<h5>No se han registrado Modelos de Acreditación para la Escuela Profesional seleccionada</h5>"
					else%>
					<p class="e1" align="left">&nbsp;&nbsp;&nbsp; Escuela Profesional: <%=nombreescuela%></p>
				<TABLE border="1" cellpadding="5" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="95%">
					<TR class="etabla">
						<TD width="5%"></TD>
						<TD width="5%">#</TD>
						<TD width="76%">Descripción del Modelo</TD>
						<TD width="94%">Fecha inicio -fin</TD>
						<TD width="85%">Vigencia</TD>
					</TR>
					<%for j=lbound(ArrModelo,2) to Ubound(ArrModelo,2)%>
					<TR style="cursor:hand" onClick="location.href='abrirmodelo.asp?idacreditacion=<%=ArrModelo(0,j)%>&idmodelo=<%=ArrModelo(1,j)%>&titulomodelo=<%=ArrModelo(5,j)%>&idcarrera=<%=idEscuela%>&nombrecarrera=<%=nombreescuela%>'" onMouseOver="Resaltar(1,this)" onMouseOut="Resaltar(0,this)">
						<TD width="5%"><img src="../../../images/abrircarpeta.gif"/></TD>
						<TD width="5%"><%=j+1%>&nbsp;</TD>
						<TD width="76%"><%=ArrModelo(5,j)%>&nbsp;</TD>
						<TD width="94%"><%=ArrModelo(3,j)%> - <%=ArrModelo(4,j)%>&nbsp;</TD>
						<TD width="85%"><%=ArrModelo(6,j)%>&nbsp;</TD>
					</TR>
						<%next%>
					</TABLE>
					<br>
					<%if session("idautorizacion")>0 then%>
					<INPUT class="salir" onclick="history.back()" type="button" value="Regresar" name="cmdregresar">
					<%end if%>
					<INPUT class="cerrar" onclick="top.window.close()" type="button" value="Salir" name="cmdSalir">
					<%end if%>
		</center>
	</BODY>
</HTML>