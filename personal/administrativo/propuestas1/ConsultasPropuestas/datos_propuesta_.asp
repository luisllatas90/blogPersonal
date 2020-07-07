<html>
<head>
<title>Documento sin t&iacute;tulo</title>
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
</head>


<p>
  <%if Request.QueryString("codigo_prp")<>"" then
	codigo_prp=Request.QueryString("codigo_prp")
	 Set objProp=Server.CreateObject("PryUSAT.clsAccesoDatos")
	     	objProp.AbrirConexiontrans
			set propuesta=objProp.Consultar("ConsultarPropuestas","FO","CP",codigo_prp,"","","")
	    	objProp.CerrarConexiontrans
			set objProP=nothing
	%>
<body topmargin="0" leftmargin="0" rightmargin="0">  
  	
</p>
<table width="100%" height="100%" border="0" align="center" cellpadding="0" cellspacing="0">
  <tr>
    <td width="76%" rowspan="3" align="left" valign="top"><table width="95%" border="0" align="center" cellpadding="0" cellspacing="1">
      <tr>
        <td valign="top">&nbsp;</td>
        <td valign="top">&nbsp;</td>
        <td valign="top">&nbsp;</td>
      </tr>
      <tr>
        <td width="35%" valign="top"><strong>Nombre de la Propuesta</strong></td>
        <td width="3%" valign="top"><strong>: </strong></td>
        <td width="62%" valign="top"><div align="justify">
          <%response.write(propuesta(0))%>
        </div></td>
      </tr>
      <tr>
        <td valign="top"><strong>&Aacute;rea:</strong></td>
        <td valign="top">&nbsp;</td>
        <td valign="top"><%response.write(propuesta("centrocostos"))%></td>
      </tr>
      <tr>
        <td valign="top"><strong>Proponente </strong></td>
        <td valign="top"><strong>: </strong></td>
        <td valign="top"><table width="100%" border="0" cellspacing="0" cellpadding="0">
          <tr>
            <td><TABLE cellpadding="0" cellspacing="0">
              <%
					Set objInt=Server.CreateObject("PryUSAT.clsAccesoDatos")
					objInt.AbrirConexiontrans
					set interesado=objInt.Consultar("ConsultarResponsablesPropuesta","FO","PR",codigo_prp,0)
					objInt.CerrarConexiontrans
					set objInt=nothing
					
					do while not interesado.eof
						%>
              <TR>
                <TD valign="TOP"> - </TD>
                <TD><%response.write(interesado(0))%>                </TD>
              </TR>
              <%
						interesado.movenext()
					loop
					set interesado = nothing					
					%>
            </TABLE></td>
          </tr>
        </table></td>
      </tr>
      <tr>
        <td valign="top"><strong>Tipo de Propuesta </strong></td>
        <td valign="top"><strong>: </strong></td>
        <td valign="top"><%response.write(propuesta(7))%></td>
      </tr>
      <tr>
        <td valign="top"><strong>Enviado el</strong></td>
        <td valign="top"><strong>: </strong></td>
        <td valign="top"><%response.write(propuesta(2))%></td>
      </tr>
      <tr>
        <td valign="top"><strong>Estado</strong></td>
        <td valign="top"><strong>: </strong></td>
        <td valign="top"><strong>
          <%
				select case UCase(propuesta(3))
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
      </tr>
      <tr>
        <td valign="top"><strong>Instancia actual </strong></td>
        <td valign="top"><strong>: </strong></td>
        <td valign="top"><font color="#990000"><%
				select case UCase(propuesta("instancia_prp"))
		  		case "S"
					Response.write("Secretar&iacute;a General")
		  		case "R"
					Response.write("Revisi&oacute;n")
		  		case "C"
					Response.write("Consejo Universitario")
				case "D"
					Response.write("Director de Área")
				case "P"
					Response.write("Proponente")
				end select			
				%>        </font></td>
      </tr>
      <tr>
        <td valign="top"><strong>Prioridad</strong></td>
        <td valign="top"><strong>: </strong></td>
        <td valign="top"><%
				select case propuesta(5)
		  		case "A"
					response.write("Alta")
		  		case else 
					response.write("-")
				end select					
				%></td>
      </tr>
      <tr>
        <td valign="top"><strong>Descripci&oacute;n</strong></td>
        <td valign="top"><strong>: </strong></td>
        <td valign="top"><div align="justify">
          <%response.write(propuesta(1))%>
        </div></td>
      </tr>
      <tr>
        <td valign="top"><strong>Reuni&oacute;n de Consejo</strong></td>
        <td valign="top"><strong>: </strong></td>
        <td valign="top"><div align="justify">
          <%
			  if isnull((propuesta(6))) then
  			  response.write("No Programada")
			  else

					Set objRec=Server.CreateObject("PryUSAT.clsAccesoDatos")
					objRec.AbrirConexiontrans
					set RsReunionConsejo=objRec.Consultar("ConsultarReunionConsejo","FO","PR",propuesta(6),0)
					objRec.CerrarConexiontrans
					set objRec=nothing
			  			  
''			  response.write()
			  Response.Write(RsReunionConsejo(1))
			  end if
			  SET propuesta = NOTHING
			  %>
        </div></td>
      </tr>
    </table>    </td>
    <td width="7%" height="29" align="right" valign="middle" class="bordeizq"><img src="../../../../images/menus/attachfiles_small.gif"></td>
    <td width="17%" align="left" valign="middle"> <strong>&nbsp;Datos adjuntos </strong></td>
  </tr>
  <tr>
    <td height="22" colspan="2" align="center" valign="top" class="bordeizqinf">
				    <p>
				      <%
					Set objCom=Server.CreateObject("PryUSAT.clsAccesoDatos")
					objCom.AbrirConexiontrans
					set comentario1=objCom.Consultar("ConsultarComentarios","FO","PC",codigo_prp,0)
					objCom.CerrarConexiontrans
					set objCom=nothing
					if comentario1.EOF then
					else
						coment1=comentario1(0)
					end if
					
					set comentario1 = nothing
					%></p>
				    <table width="95%" border="0" align="center" cellpadding="0" cellspacing="2">

                     <%
					Set objArchivo=Server.CreateObject("PryUSAT.clsAccesoDatos")
					objArchivo.AbrirConexiontrans
					set rsArchivosPrp=objArchivo.Consultar("ConsultarArchivosPropuesta","FO","TO",coment1)
					objArchivo.CerrarConexiontrans
					set objArchivo=nothing	
					if rsArchivosPrp.eof = true then
						Response.Write("No presenta datos adjuntos")
					end if		
			  		do while not rsArchivosPrp.eof
			  		%>
                      <tr>
                        <td width="8%" align="left"><a href="../../../../filespropuestas/<%=codigo_prp%>/<%=rsArchivosPrp("nombre_apr")%>" target="_blank"> <img src="../../../../images/ext/<%=right(rsArchivosPrp(2),3)%>.gif" width="16" height="16" border="0"> </a> </td>
                        <td width="92%" align="left"><%response.write(rsArchivosPrp(3))%></td>
                      </tr>
                      <%
			  	rsArchivosPrp.movenext()
				loop	
				set rsArchivosPrp=nothing			
			  %>
      </table>
    </td>
  </tr>
  <tr>
    <td height="127" colspan="2" align="center" valign="top">&nbsp;</td>
  </tr>
</table>
</body>
<%end if%>
</html>
