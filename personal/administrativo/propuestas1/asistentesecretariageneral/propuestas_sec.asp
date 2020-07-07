<!--#include file="../../../../funciones.asp"-->


<html>
<head>

<title></title>
<link href="../../../private/estilo.css" rel="stylesheet" type="text/css" />
<link href="../../../../private/estilo.css" rel="stylesheet" type="text/css">
</head>
<script language="JavaScript" src="private/validarpropuestas.js"></script>
<script language="JavaScript" src="../../../../private/funciones.js"></script>
<body>
<form>

<center>
<%
tipo_instancia=" Secretaría General"
if request.querystring("instancia")="C" then
tipo_instancia=" Consejo Universitario"
end if
if request.querystring("instancia")="R" then
tipo_instancia=" Revisor"
end if
if request.querystring("instancia")="S" then
tipo_instancia=" Secretaría General"
end if%>
  <p class="usatTituloAplicacion"> Propuestas en instancia: 
    <%response.write(tipo_instancia)%> </p>
  <p align="left" class="usatsugerencia"> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Seleccione la instancia de revisi&oacute;n de las propuestas que desea consultar y hHacer clic en ver el &iacute;cono de &quot;Ver detalle&quot; para consultar el contenido de la propuesta </p>
  <table width="100%" border="0" cellspacing="0" cellpadding="0">
    <tr>
      <td width="10%">Instancia</td>
      <td width="35%"><select name="cbo_instancia" onChange="actualizarlista('propuestas_sec.asp?instancia='+this.value)" >
        <option value="S" <%IF request.querystring("instancia")="S" then %>selected="selected"<% END IF%>>Secretar&iacute;a General</option>
        <option value="C" <%IF request.querystring("instancia")="C" then %>selected="selected" <% END IF%>>Consejo Universitario</option>
        <option value="R" <%IF request.querystring("instancia")="R" then %>selected="selected" <% END IF%>>Revisor</option>
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
 ' if request.querystring("instancia")="s" then 
  
 ' else
  Set objCC=Server.CreateObject("PryUSAT.clsAccesoDatos")
	     	ObjCC.AbrirConexiontrans
			set propuesta=objCC.Consultar("ConsultarPropuestas","FO","se",session("codigo_Usu"),request.querystring("instancia"),"","")
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
    <td align="center"><a href="verdetallepropuesta_sec.asp?propuesta=<%=propuesta(0)%>&instancia=<%=request.querystring("instancia")%>"><img src="../../../../images/readebook.gif" width="20" height="20" border="0"></a></td>
  </tr>
  <% 
  propuesta.movenext()
  loop 
  set propuesta = nothing 
  'end if
%>
</table>
<table width="80%" border="0" align="center" cellpadding="0" cellspacing="0">
  <tr>
    <td align="center">&nbsp;</td>
  </tr>
  <tr>
    <td align="center"><a href="javascript:history.back()"><img src="../../../../images/back.gif" width="18" height="18" border="0"></a></td>
  </tr>
  <tr>
    <td align="center">Volver</td>
  </tr>
</table>
<p></p>
</form>
</body>
</html>
