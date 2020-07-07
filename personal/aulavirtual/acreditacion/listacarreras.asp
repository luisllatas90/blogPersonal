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
		ArrDatos=.ConsultarEscuelas("TEFAC",0,0,0)
	end with
	Set acreditacion=nothing
%>
<HTML>
	<HEAD>
		<Title>Acceso al Sistema</Title>
		<link rel="stylesheet" type="text/css" href="../../../private/estiloaulavirtual.css">
			<script language="JavaScript" src="../../../private/funcionesaulavirtual.js"></script>
			<script language="Javascript">
				function AbrirEscuela()
				{
					var control=document.all.idescuela
					location.href="listamodelos.asp?idEscuela=" + control.value + "&nombreEscuela=" + control.options[control.selectedIndex].text
				}
			</script>
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
			<%If IsEmpty(ArrDatos)=true then%>
			<h5>No se han registrado Escuelas Profesionales para Proceso de Acreditación o Funcionamiento</h5>
			<%else%> 
			  <p class="e1">Seleccione la Escuela Profesional</p>
					<SELECT name="idescuela" multiple style="width: 400; height: 200">
							<%for i=lbound(Arrdatos,2) to Ubound(Arrdatos,2)%>
								<OPTION value="<%=Arrdatos(0,I)%>" <%=seleccionar(idEscuela,ArrDatos(0,I))%>><%=Arrdatos(1,I)%></OPTION>
							<%next%>
					</SELECT>
					<br>
					<INPUT class="buscar" style="WIDTH: 100px" onclick="AbrirEscuela()" type="button" value="Ingresar" name="cmdIngresar">
					<INPUT class="cerrar" style="WIDTH: 100px" onclick="cerrarSistema()" type="button" value="Salir" name="cmdSalir">
		</center>
		<%end if%>
	</BODY>
</HTML>