<!--#include file="../../../../funciones.asp"-->
<%
Response.ContentType = "application/vnd.ms-excel"
Response.AddHeader "Content-Disposition","attachment;filename=rptegrupos.xls"

codigo_cac=request.querystring("codigo_cac")
codigo_cpf=request.querystring("codigo_cpf")
usuario=session("codigo_usu")

if codigo_cac="" then codigo_cac=session("codigo_cac")
if codigo_cac="" then codigo_cac="-2"
if codigo_cpf="" then codigo_cpf="-2"


Set objEscuela=Server.CreateObject("PryUSAT.clsAccesoDatos")
	objEscuela.AbrirConexion
		if codigo_cac<>"-2" and codigo_cpf<>"-2" then
			Set rsCursoProg= objEscuela.Consultar("ConsultarAlumnosMatriculados","FO",10,codigo_cpf,codigo_cac,0)
			
			if Not(rsCursoProg.BOF and rsCursoProg.EOF) then
				activo=true
			end if
		end if
	objEscuela.CerrarConexion
Set objEscuela=nothing
%>
<html>
<head>
<title>Matriculados por Asignatura y Ciclo Académico</title>
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<script language="JavaScript" src="../../../../private/funciones.js"></script>
<style type="text/css">
.style1 {
	text-align: center;
	background-color: #CCCCCC;
	font-weight: bold;
}
.style2 {
	text-align: right;
}
</style>
</head>
<body>
<table border="0" cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%" <%=alto%>>
  <tr>
    <td width="100%" height="5%">
	<h2>Matriculados por Grupo Horario</h2>
	</td>
  </tr>
  <%if activo=true then%>
  <tr>
    <td width="100%" height="50%">
    <table border="1" cellpadding="2" cellspacing="0" style="border-collapse: collapse" bordercolor="#808080" width="100%"  <%=alto%> id="tblcursoprogramado">
      <tr class="style1">
        <td width="5%" height="3%" rowspan="2" style="height: 6%">Ciclo</td>
        <td width="10%" height="3%" rowspan="2" style="height: 6%">Código</td>
        <td width="35%" height="3%" rowspan="2" style="height: 6%">Nombre del Curso</td>
        <td width="5%" height="3%" rowspan="2" style="height: 6%">Crd.</td>
        <td width="5%" height="3%" rowspan="2" style="height: 6%">GH</td>
        <td width="5%" height="3%"  rowspan="2" style="height: 6%">Pre</td>
        <td width="5%" height="3%" rowspan="2" style="height: 6%">Mat</td>
        <td width="5%" height="3%" rowspan="2" style="height: 6%">Total</td>
        <td width="5%" height="3%" colspan="2">Vacantes</td>
        <td width="5%" height="3%" rowspan="2" style="height: 6%">Acción</td>        
      </tr>
      <tr class="style1">
        <td width="5%" height="3%" align="center">T</td>
        <td width="5%" height="3%"  align="center">F</td>
      </tr>
		<%	
			i=0:n=0:p=0
			Ciclo=1
			Do while not rsCursoProg.eof
				i=i+1
				subtotal=0
				subtotal=rsCursoProg("pre") + rsCursoProg("mat")
				faltantes=rsCursoProg("vacantes_cup")-subtotal
						
		%>
			<tr height="20px" id="fila<%=i%>">
			<td  width="5%" class="style2"><%=ConvRomano(rsCursoProg("ciclo_Cur"))%>&nbsp;</td>
			<td  width="10%"><%=rsCursoProg("identificador_Cur")%>&nbsp;</td>			
			<td  width="38%"><%=rsCursoProg("nombre_Cur")%>&nbsp;</td>
			<td  align="center" width="5%"><%=rsCursoProg("creditos_cur")%>&nbsp;</td>
			<td  align="center" width="6%"><%=rsCursoProg("grupohor_cup")%>&nbsp;</td>
			<td  align="center" width="5%">
			<%=rsCursoProg("pre")%>&nbsp;
			</td>
			<td  align="center" width="5%">
			<%=rsCursoProg("mat")%>&nbsp;		
			</td>
			<td  align="center" width="5%"><%=subtotal%></td>
			<td  align="center" width="5%"><%=rsCursoProg("vacantes_cup")%></td>
			<td  align="center" width="5%"><%=faltantes%></td>	
			</tr>
				<%rsCursoProg.movenext
			loop

		%>
      </table>
  </td>
  </tr>
  <%end if%>
</table>
<span id="mensaje" class="rojo"></span>
</body>
</html>