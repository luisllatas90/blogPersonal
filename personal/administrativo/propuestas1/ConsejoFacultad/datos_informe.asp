<html>
<head>
<title>Datos del Informe</title>
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
.Estilo5 {
	color: #990000;
	font-weight: bold;
}
-->
</style>
<STYLE type="text/css">
BODY {
scrollbar-face-color:#A6D6FF;
scrollbar-highlight-color:#FFFFFF;
scrollbar-3dlight-color:#FFFFFF;
scrollbar-darkshadow-color:#FFFFFF;
scrollbar-shadow-color:#FFFFFF;
scrollbar-arrow-color:#000000;

scrollbar-track-color:#FFFFFF;
}
a:link {text-decoration: none; color: #00080; }
a:visited {text-decoration: none; color: #000080; }
a:hover {text-decoration: none; black; }
a:hover{color: black; text-decoration: none; }
</STYLE>
<script language="JavaScript" src="../../../../private/funciones.js"></script>
<script language="JavaScript" src="../../../../private/tooltip.js"></script>



<script>

	
function enviarVersion(codigo_prp,version){
	fraversion.location.href="datosVersioninforme.asp" + "?codigo_prp=" + codigo_prp + "&version=" +  version
}
function VerPropuestaPadre(propuestaPadre){
		AbrirPopUps("datos_propuesta.asp?vistaPrevia=SI&codigo_prp=" + propuestaPadre,450,700,false,false,true)
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
        <td colspan="2" valign="top">&nbsp;</td>
      </tr>
      <tr>
        <td width="20%" valign="top"><span class="Estilo1">Denominaci&oacute;n</span></td>
        <td width="2%" valign="top"><span class="Estilo1">: </span></td>
        <td width="78%" colspan="2" valign="top"><div align="justify" class="Estilo1">
            <%Response.write(propuesta("nombre_Prp"))%>
        </div></td>
      </tr>
      <tr>
        <td valign="top"><span class="Estilo2"><strong>&Aacute;rea:</strong></span></td>
        <td valign="top"><span class="Estilo2"></span></td>
        <td valign="top"><span class="Estilo2">
          <%Response.write(propuesta("descripcion_Cco"))%>
        </span></td>
        <td rowspan="2" align="center" valign="middle"><table width="2%" border="0" cellpadding="0" cellspacing="0" class="contornotabla">
          <tr>
            <td><input name="Submit" type="button" class="buscar1" value="   Ver Propuesta" onClick="VerPropuestaPadre('<%=propuesta("codigoReferencia_prp")%>')" tooltip="Consulte la propuesta correspondiente al informe."></td>
          </tr>
        </table></td>
      </tr>
      <tr>
        <td valign="top"><span class="Estilo2"><strong>Proponente </strong></span></td>
        <td valign="top"><span class="Estilo2"><strong>: </strong></span></td>
        <td valign="top"><table width="100%" border="0" cellspacing="0" cellpadding="0">
          <tr>
            <td class="Estilo2"><strong>
                <%
					Set objInt=Server.CreateObject("PryUSAT.clsAccesoDatos")
					objInt.AbrirConexiontrans
					set interesado=objInt.Consultar("ConsultarResponsablesPropuesta","FO","PR",codigo_prp,0)
					objInt.CerrarConexiontrans
					set objInt=nothing
					
					do while not interesado.eof
						%>

                    <%response.write(interesado(0))%>
                  </span>
                  <%
						interesado.movenext()
					loop
					set interesado = nothing					
					%>
            </strong></td>
          </tr>
        </table></td>
        </tr>
      <tr>
        <td valign="top"><span class="Estilo2"><strong>Tipo de Propuesta </strong></span></td>
        <td valign="top"><span class="Estilo2"><strong>: </strong></span></td>
        <td colspan="2" valign="top"><span class="Estilo2">
          <%Response.write(propuesta("Descripcion_Tpr"))%>
        </span></td>
      </tr>
      <tr>
        <td colspan="4" valign="top"><table width="100%" border="0" cellspacing="0" cellpadding="0">
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
              <td width="33%" valign="top" class="Estilo2">
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
				end select			
				%>              </td>
            </tr>
            <tr>
              <td valign="top" class="Estilo2"><strong>Fecha</strong></td>
              <td valign="top" class="Estilo2"><strong>: </strong></td>
              <td valign="top" class="Estilo2"><%response.write(propuesta("fecha_prp"))%></td>
              <td valign="top" class="Estilo2"><strong>Prioridad</strong></td>
              <td valign="top" class="Estilo2"><strong>: </strong></td>
              <td valign="top" class="Estilo2"><%
				select case propuesta("prioridad_Prp")
		  		case "A"
					response.write("Alta")
		  		case else 
					response.write("-")
				end select					
				%></td>
            </tr>
            <tr>
              <td valign="top" class="Estilo2"><strong>Estado</strong></td>
              <td valign="top" class="Estilo2"><strong>: </strong></td>
              <td valign="top" class="Estilo2"><strong>
                <%
				estado=propuesta("estado_Prp")
				select case UCase(propuesta("estado_Prp"))
		  		case "A"
					response.write("Aprobada")
		  		case "R"
					response.write("Denegada")
				case "O"
					response.write("Observada")
		  		case "C"
					response.write("Corregido")
		  		case "P"
					response.write("Pendiente")
				end select
				
				%>
              </strong></td>
              <td valign="top" class="Estilo2">&nbsp;</td>
              <td valign="top" class="Estilo2">&nbsp;</td>
              <td valign="top" class="Estilo2">&nbsp;</td>
            </tr>
            <tr>
              <td height="18" valign="top" class="Estilo2"><strong>Versiones</strong></td>
              <td valign="top" class="Estilo2"><strong>:</strong></td>
              <td colspan="3" valign="top" class="Estilo2">
                <%
		Set objVERSION=Server.CreateObject("PryUSAT.clsAccesoDatos")
		objVERSION.AbrirConexion
		Set RsVersiones=objVERSION.Consultar("ConsultarInformePropuesta","FO","ES",codigo_prp,0)
		objVERSION.CerrarConexion
		set objVERSION=nothing
  		if RsVersiones.recordcount =0 then
			Response.Write("-")
			
		else
			Do While not RsVersiones.EOF
				i=i+1
				if i=1 then%>
					<span onClick="enviarVersion('<%=codigo_prp%>','<%Response.Write(RsVersiones("version_Dip"))%>')"  style="cursor:hand"> <%Response.Write("Inicial: " & RsVersiones("version_Dip"))%> 
					</span>
					<%else
					if i=2 then
						Response.Write("Final: ")
					else
						Response.Write(" - ") 
					end if%>
					<span onClick="enviarVersion('<%=codigo_prp%>','<%Response.Write(RsVersiones("version_Dip"))%>')"  style="cursor:hand"> <%Response.Write(RsVersiones("version_Dip"))%> 
					</span>
					<%''Response.Write(RsVersiones("version_Dip") )
				end if
				RsVersiones.MoveNext
			loop
		end if
%>              <input type="hidden"  name="txtVersiones" <%if RsVersiones.recordcount =0 then%> value="NO" <%else%>value="SI"<%end if%> ></td>
              <td align="right" valign="top" class="Estilo2">&nbsp;</td>
            </tr>
        </table></td>
      </tr>
    </table></td>
  </tr> 
  <tr>
  	<%	if RsVersiones.RecordCount=0 then%>
	<td  align="center" valign="top"> <p class="Estilo2">&nbsp;</p>
      <table width="70%" border="0" cellpadding="0" cellspacing="3" class="contornotabla">
        <tr>
          <td align="center"><span class="Estilo5">IMPORTANTE</span></td>
        </tr>
        <tr>
          <td align="center">&nbsp;</td>
        </tr>
        <tr>
          <td><p align="justify" class="Estilo2">Le ha sido asignada la ejecuci&oacute;n de esta propuesta, s&iacute;rvase a registrar los datos del <span class="Estilo5">Informe Inicial</span>, hacer clic en &quot;<span class="Estilo5">Nueva Versi&oacute;n</span>&quot;, llenar los datos y <span class="Estilo5">Guardar </span></p>
          <p align="justify" class="Estilo2">Una vez concluida su ejecuci&oacute;n deber&aacute; registrar el <span class="Estilo5">Informe Final</span>, hacer clic en &quot;<span class="Estilo5">Nueva Versi&oacute;n</span>&quot; , llenar los datos y <span class="Estilo5">Enviar</span></p></td>
        </tr>
      </table></td>
	<%else
	RsVersiones.movelast%>
    <td  align="center" valign="top">
	<iframe height="180" src="datosVersionInforme.asp?codigo_prp=<%=codigo_prp%>&version=<%=RsVersiones("version_Dip")%>&instancia=<%=instancia%>" frameborder="0" class="contornotabla" width="98%" id="fraversion" name="fraversion" marginheight="0"> </iframe></td>
	<%end if%>
  </tr>
</table>
</body>
<%end if%>
</html>
