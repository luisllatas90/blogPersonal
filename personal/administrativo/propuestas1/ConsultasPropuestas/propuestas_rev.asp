<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />
<title>PROPUESTAS</title>
<link href="../../../../private/estilo.css" rel="stylesheet" type="text/css" />
<script language="JavaScript" src="../../../../private/funciones.js"></script>
<script type="text/JavaScript">
<!--
function MM_jumpMenu(targ,selObj,restore){ //v3.0
  eval(targ+".location='"+selObj.options[selObj.selectedIndex].value+"'");
  if (restore) selObj.selectedIndex=0;
}
//-->
</script>
</head>

<body>
<form>

<center>
<%
if request.querystring("instancia")="C" then
tipo_instancia="como miembro de Consejo Universitario"
end if
if request.querystring("instancia")="R" then
tipo_instancia="como Revisor"
end if
%>
  <p class="usatTituloAplicacion"> Propuestas recibidas 
    <%response.write(tipo_instancia)%> </p>
  <p align="left" class="usatsugerencia"> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Seleccione la instancia de revisi&oacute;n de las propuestas que desea consultar y hacer clic en ver el &iacute;cono de &quot;Ver detalle&quot; para consultar el contenido de la propuesta </p>
  <table width="100%" border="0" cellspacing="0" cellpadding="0">
    <tr>
      <td width="10%">Instancia</td>
      <td width="35%"><select name="cbo_instancia" onChange="actualizarlista('propuestas_rev.asp?instancia='+this.value)" >
        <option value="S">---Seleccione una instancia---</option>
        <option value="R">Revisor</option>
        <option value="C">Consejo Universitario</option>
                        </select></td>
      <td width="55%" align="left"><center>
      </center></td>
    </tr>
    <tr>
      <td>&nbsp;</td>
      <td>&nbsp;</td>
      <td align="left">&nbsp;</td>
    </tr>
  </table>
</center>
  <% 
  if request.querystring("instancia")="s" then 
  
  else
  Set objCC=Server.CreateObject("PryUSAT.clsAccesoDatos")
	     	ObjCC.AbrirConexiontrans
			set propuesta=objCC.Consultar("ConsultarPropuestas","FO","RE",session("codigo_Usu"),request.querystring("instancia"),"","")
	    	ObjCC.CerrarConexiontrans
			set objCC=nothing
  %>
<table width="100%" border="0" align="center" cellpadding="3" cellspacing="0" bordercolor="#FFFFFF" class="contornotabla">
  <tr>
    <td colspan="2" align="center" class="usatCeldaTabActivo">Propuesta</td>
    <td width="22%" align="center" class="usatCeldaTabActivo">Enviada el </td>
    <td width="31%" align="center" class="usatCeldaTabActivo">Proponente </td>
    <td width="10%" align="center" class="usatCeldaTabActivo">Ver detalle </td>
  </tr>
  <% do while not propuesta.eof								
  %>
  <tr onMouseOver="Resaltar(1,this)" onMouseOut="Resaltar(0,this)">
    <td width="3%"><img src="../../../../images/adelante.gif" width="16" height="16"></td>
    <td width="34%"><% response.write(propuesta(1))%></td>
    <td><% response.write(propuesta(2))%></td>
    <td><TABLE cellpadding="0" cellspacing="0">
      <%
					Set objInt=Server.CreateObject("PryUSAT.clsAccesoDatos")
					objInt.AbrirConexiontrans
					set interesado=objInt.Consultar("ConsultarResponsablesPropuesta","FO","PR",propuesta(0),0)
					objInt.CerrarConexiontrans
					set objInt=nothing
					
					do while not interesado.eof
						%>
      <TR>
        <TD valign="TOP"> - </TD>
        <TD><%response.write(interesado(0))%>        </TD>
      </TR>
      <%
						interesado.movenext()
					loop
					set interesado = nothing					
					%>
    </TABLE>    </td>
    <td align="center"><a href="verdetallepropuesta_rev.asp?propuesta=<%=propuesta(0)%>&instancia=<%=request.querystring("instancia")%>"><img src="../../../../images/readebook.gif" width="20" height="20" border="0"></a></td>
  </tr>
  <% 
  propuesta.movenext()
  loop 
  set propuesta = nothing 
  end if
%>
</table>
<p></p>
</form>
</body>
</html>
