<html>
<head>
<title>Datos de la Propuesta</title>
<link href="../../../../private/estilo.css" rel="stylesheet" type="text/css" />
<style type="text/css">
<!--
body {
	background-color: #FFFFFF;
}
-->
</style>
<link href="../../../../private/estilo.css" rel="stylesheet" type="text/css">
<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1">
<style type="text/css">
<!--
.Estilo1 {
	color: #000000;
	font-weight: bold;
}
.Estilo2 {color: #000000}
.Estilo3 {
	color: #990000;
	font-weight: bold;
	font-size: 12pt;
}
-->
</style>
<script language="JavaScript" src="../../../../private/funciones.js"></script>
<script>
function enviarVersion(codigo_prp,version){
	fraversion.location.href="datosVersion.asp" + "?codigo_prp=" + codigo_prp + "&version=" +  version
}
	function AbrirPopUps(pagina,alto,ancho,ajustable,bestado,barras)
{
   izq = (screen.width-ancho)/2
   arriba= (screen.height-alto)/2

   var ventana=window.open(pagina,"popup","height="+alto+",width="+ancho+",statusbar="+bestado+",scrollbars="+barras+",top" + arriba + ",left" + izq + ",resizable="+ajustable+",toolbar=no,menubar=no");
   ventana.location.href=pagina
   ventana=null
}
</script>


</head>


<p>
  <%if Request.QueryString("codigo_prp")<>"" then
	codigo_prp=Request.QueryString("codigo_prp")
	 Set objProp=Server.CreateObject("PryUSAT.clsAccesoDatos")
	     	objProp.AbrirConexion
			set propuesta=objProp.Consultar("ConsultarPropuestas","FO","CP","","",codigo_prp,"","")
	    	objProp.CerrarConexion
			set objProP=nothing
	%>
<body topmargin="0" leftmargin="0" rightmargin="0">   	
</p>
<table width="100%" height="100%" border="0" align="center" cellpadding="0" cellspacing="0">
  <tr>
    <td align="left" valign="top"><table width="100%" border="0" align="center" cellpadding="0" cellspacing="1">
      <tr>
        <td valign="top">&nbsp;</td>
        <td valign="top">&nbsp;</td>
        <td colspan="3" valign="top" align="right"><span class="Estilo3">
	        <%
		if propuesta("destino_prp")="U" then
			Response.Write("Consejo Universitario")
		else
			Response.Write("Consejo de Facultad")
		end if
		%>
		   </span></td>
      </tr>
      <tr>
        <td width="20%" valign="top"><span class="Estilo1">Denominaci&oacute;n</span></td>
        <td width="2%" valign="top"><span class="Estilo1">: </span></td>
        <td colspan="3" valign="top"><div align="justify" class="Estilo1">
            <%Response.write(propuesta("nombre_Prp"))%>
        </div></td>
      </tr>
      <tr>
        <td valign="top"><span class="Estilo2"><strong>&Aacute;rea:</strong></span></td>
        <td valign="top"><span class="Estilo2"></span></td>
        <td colspan="3" valign="top"><span class="Estilo2">
          <%Response.write(propuesta("descripcion_Cco"))%>
        </span></td>
      </tr>
      <tr>
        <td valign="top"><span class="Estilo2"><strong>Proponente </strong></span></td>
        <td valign="top"><span class="Estilo2"><strong>: </strong></span></td>
        <td colspan="3" valign="top"><table width="100%" border="0" cellspacing="0" cellpadding="0">
            <tr>
              <td class="Estilo2"><strong>
                  <%
					Set objInt=Server.CreateObject("PryUSAT.clsAccesoDatos")
					objInt.AbrirConexiontrans
					set interesado=objInt.Consultar("ConsultarResponsablesPropuesta","FO","PR",codigo_prp,0)
					objInt.CerrarConexiontrans
					set objInt=nothing
						%>
					<span class="Estilo2">
                      <%response.write(interesado(0))%>                    
                      </span>
                  <%

					set interesado = nothing					
					%>           
                  </strong></td>
            </tr>
        </table></td>
      </tr>
      <tr>
        <td valign="top"><span class="Estilo2"><strong>Director </strong></span></td>
        <td valign="top"><span class="Estilo2"><strong>: </strong></span></td>
        <td width="34%" valign="top">
          <span class="Estilo2">
          <%
					Set objInt=Server.CreateObject("PryUSAT.clsAccesoDatos")
					objInt.AbrirConexiontrans
					set director=objInt.Consultar("ConsultarResponsablesPropuesta","FO","Pd",codigo_prp,0)
					objInt.CerrarConexiontrans
					set objInt=nothing
					

						%>

              <%response.write(director(0))%>

            <%
					set director = nothing					
					%>
          </span></td>
        <td width="20%" valign="top"><span class="Estilo2"><strong>Tipo de Propuesta :</strong></span></td>
        <td width="24%" valign="top"><span class="Estilo2 Estilo2">
          <%Response.write(propuesta("Descripcion_Tpr"))%>
        </span></td>
      </tr>
      <tr>
        <td colspan="5" valign="top"><table width="100%" border="0" cellspacing="0" cellpadding="0">
            <tr>
              <td width="20%" valign="top" class="Estilo2"><strong>Facultad</strong></td>
              <td width="2%" valign="top" class="Estilo2"><strong>:</strong></td>
              <td width="26%" valign="top" class="Estilo2"><%
			  if propuesta("codigo_Fac") = -2 then
					Response.Write("Todas")
			  else
				  	response.write(propuesta("nombre_Fac"))
 			  end if
					%></td>
			 
              <td width="18%" valign="top" class="Estilo2"><strong>Instancia actual </strong></td>
              <td width="1%" valign="top" class="Estilo2"><strong>: </strong></td>
              <td colspan="2" valign="top" class="Estilo2">
                <%
				instancia_PRP=propuesta("instancia_Prp")
				select case UCase(propuesta("instancia_Prp"))
		  		case "S"
					Response.write("Secretar&iacute;a General")
		  		case "R"
					Response.write("Revisi&oacute;n")
		  		case "C"
					Response.write("Consejo Universitario")
				case "D"
					Response.write("Director de &Aacute;rea")
				case "P"
					Response.write("Proponente")
				case "F"
					Response.write("Consejo de Facultad")
				end select			
				%>              </td>
            </tr>
              <td height="18" valign="top" class="Estilo2"><strong>Versiones present. </strong></td>
              <td valign="top" class="Estilo2"><strong>:</strong></td>
              <td valign="top" class="Estilo2"><strong>
               <span style="background-color:#FFFF99">
			    <%
		Set objVERSION=Server.CreateObject("PryUSAT.clsAccesoDatos")
		objVERSION.AbrirConexion
		Set RsVersiones=objVERSION.Consultar("CONSULTARVERSIONESPROPUESTA","FO","ES",codigo_prp,0)
		objVERSION.CerrarConexion
		set objVERSION=nothing
		Do While not RsVersiones.EOF
			i=i+1
			if i=1 then%>
                <span onClick="enviarVersion('<%=codigo_prp%>','<%Response.Write(RsVersiones("version_Dap"))%>')"  style="cursor:hand;text-decoration:underline"> 
				<%Response.Write(RsVersiones("version_Dap"))%> 
				</span>
			    <%else
				Response.Write(" - ") %>
				<span onClick="enviarVersion('<%=codigo_prp%>','<%Response.Write(RsVersiones("version_Dap"))%>')"  style="cursor:hand; text-decoration:underline"> <%Response.Write(RsVersiones("version_Dap"))%> 
				</span>
				<%''Response.Write(RsVersiones("version_Dap") )
			end if
			RsVersiones.MoveNext
		loop
%>              </span>
				</strong></td>
              <td colspan="3" valign="top" class="Estilo2"><p><span class="rojo"><strong>
                <%Response.write(propuesta("Observacion_Prp"))%>
                </strong></span></p>                </td>
              <td width="14%" align="right" valign="top" class="Estilo2"><%
		Set objNuevaVersion=Server.CreateObject("PryUSAT.clsAccesoDatos")
		objNuevaVersion.AbrirConexion
		Set RsNuevaVersion=objNuevaVersion.Consultar("ConsultarEsObservada","FO",codigo_prp)
		objNuevaVersion.CerrarConexion
		set objNuevaVersion=nothing
  	    if RsNuevaVersion.RecordCount>0 then
	 %>
             <%IF propuesta("estado_prp")="P" then%>
			    <input name="button" type="button" class="nuevaversion" id="txtVersion" onClick="AbrirPopUps('registraversion.asp?codigo_prp=<%=codigo_prp%>','550','750')" value=" Nueva Versi&oacute;n" border="" name="txtVersion" <%if instancia_PRP="P"  or estado="A" then%>disabled="disabled" <%end if%>></td>
			 <%end if%>
			  <%end if%>
            </tr>
        </table></td>
      </tr>
    </table></td>
  </tr> 
  <tr>
  	<%RsVersiones.MoveFirst%>
    <td  align="center" valign="top"><iframe height="250" src="datosVersion.asp?codigo_prp=<%=codigo_prp%>&version=<%=RsVersiones("version_Dap")%>&instancia=<%=instancia%>" frameborder="0" class="contornotabla" width="98%" id="fraversion" name="fraversion" marginheight="0"> </iframe></td>
  </tr>
</table>
</body>
<%end if%>
</html>