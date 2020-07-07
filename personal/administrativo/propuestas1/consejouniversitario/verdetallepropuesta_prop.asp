<HTML>
<head>
<title>PROPUESTAS</title>
<link href="../../../../private/funciones.js" rel="stylesheet" type="text/css" />
<link href="../../../../private/estilo.css" rel="stylesheet" type="text/css">
</head>

<body>
			 <div align="center">
			   <p>
			     <% prop=request.querystring("propuesta")
					Set objCom=Server.CreateObject("PryUSAT.clsAccesoDatos")
					objCom.AbrirConexiontrans
					set comentario1=objCom.Consultar("ConsultarComentarios","FO","PC",prop)
					objCom.CerrarConexiontrans
					set objCom=nothing
					coment1=comentario1(0)
					set comentario1 = nothing			
					//response.write (coment1)
					%>
		       <span class="usatTituloAplicacion">Consulta de Propuesta</span></p>
</div>
			 <center>
  <p>
    <%  
	// response.write(request.querystring("propuesta")) 
			Set objProp=Server.CreateObject("PryUSAT.clsAccesoDatos")
	     	objProp.AbrirConexiontrans
			set propuesta=objProp.Consultar("ConsultarPropuestas","FO","CP",prop,"","","")
	    	objProp.CerrarConexiontrans
			set objProP=nothing
	%></p>
  <table width="100%" border="0" cellspacing="0" cellpadding="0">
    <tr>
      <td rowspan="2" valign="top"><table width="95%" border="0" align="center" cellpadding="0" cellspacing="0" class="contornotabla">
        <tr>
          <td width="27%" align="right" valign="middle" class="usatCeldaTitulo"><strong> &nbsp;<img src="../../../../images/html.gif" width="16" height="16"></strong></td>
          <td width="73%" align="left" valign="middle" class="usatCeldaTitulo"><strong>&nbsp;Datos Generales de Propuesta</strong></td>
        </tr>
        <tr>
          <td height="127" colspan="2" align="left"><table width="95%" border="0" align="center" cellpadding="0" cellspacing="0">
            <tr>
              <td width="35%" valign="top"><strong>Nombre Propuesta</strong></td>
                <td width="3%" valign="top"><strong>: </strong></td>
                <td width="62%" valign="top"><div align="justify">
                  <%response.write(propuesta(0))%>
                </div></td>
            </tr>
            <tr>
              <td valign="top"><strong>Proponente</strong></td>
                <td valign="top"><strong>: </strong></td>
                <td valign="top">
				<table width="100%" border="0" cellspacing="0" cellpadding="0">
                  
				  <tr>
                    <td><TABLE cellpadding="0" cellspacing="0">
                      <%
					Set objInt=Server.CreateObject("PryUSAT.clsAccesoDatos")
					objInt.AbrirConexiontrans
					set interesado=objInt.Consultar("ConsultarResponsablesPropuesta","FO","PR",prop,0)
					objInt.CerrarConexiontrans
					set objInt=nothing
					
					do while not interesado.eof
						%>
						<TR>
						<TD valign="TOP">
						-
						</TD>
						<TD>
						<%response.write(interesado(0))%>
						</TD>
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
              <td valign="top"><strong>Tipo Propuesta </strong></td>
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
				select case propuesta(3)
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
              <td valign="top"><strong>Instancia</strong></td>
                <td valign="top"><strong>: </strong></td>
                <td valign="top">
<%
				select case propuesta(4)
		  		case "S"
					response.write("Secretaría")
		  		case "R"
					response.write("Revisión")
		  		case "C"
					response.write("Consejo")
				end select			
				%>				</td>
            </tr>
            <tr>
              <td valign="top"><strong>Prioridad</strong></td>
                <td valign="top"><strong>: </strong></td>
                <td valign="top"><%
				select case propuesta(5)
		  		case "S"
					response.write("Alta")
		  		case "M"
					response.write("Media")
		  		case "B"
					response.write("Baja")
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
			  response.write(propuesta(6))
			  end if
			  SET propuesta = NOTHING
			  %>
              </div></td>
            </tr>
          </table>            
          <br></td>
        </tr>        
      </table></td>
      <td width="50%" align="center" valign="top"><table width="95%" border="0" cellpadding="0" cellspacing="0" class="contornotabla">
        <tr>
          <td width="31%" align="right" valign="middle" class="usatCeldaTitulo"><img src="../../../../images/contargrupo.gif" width="15" height="12"></td>
          <td width="69%" align="left" valign="middle" class="usatCeldaTitulo"><strong> &nbsp;Revisores de Propuesta </strong><strong> </strong></td>
        </tr>
        <tr>
          <td align="center">&nbsp;</td>
          <td align="center">&nbsp;</td>
        </tr>
        <tr>
          <td colspan="2" align="center"><table width="95%" border="0" align="center" cellpadding="0" cellspacing="0">
              <tr>
                <td colspan="2" align="left"><strong>Revisores</strong></td>
                <td width="25%" align="center">&nbsp;</td>
              </tr>
              <%
					Set objRev=Server.CreateObject("PryUSAT.clsAccesoDatos")
					objRev.AbrirConexiontrans
					set revisor=objRev.Consultar("ConsultarResponsablesPropuesta","FO","RC",prop,0)
					objRev.CerrarConexiontrans
					set objRev=nothing
					
					do while not revisor.eof
						if revisor(1)="R" then
						%>
			  <tr>
                <td width="4%" align="left">-</td>
                <td width="71%" align="left"><%response.write(revisor(0))%></td>
                <td align="center">
<%
				select case revisor(2)
		  		case "S"
					response.write("Pendiente")
		  		case "C"
					response.write("Conforme")
		  		case "N"
					response.write("No Conforme")
				end select					
								
				
				%></td>
              </tr>
			  						<% end if
						revisor.movenext()
					loop						
					%>
              <tr>
                <td colspan="2" align="left"><strong>Consejo universitario </strong></td>
                <td align="center">&nbsp;</td>
              </tr>
<%
					revisor.movefirst()
					do while not revisor.eof
						if revisor(1)="C" then
%>              
			  <tr>
                <td align="left">-</td>
                <td align="left"><%response.write(revisor(0))%></td>
                <td align="center"><%
				select case revisor(2)
		  		case "S"
					response.write("Pendiente")
		  		case "C"
					response.write("Conforme")
		  		case "N"
					response.write("No Conforme")
				end select					
								
				
				%></td>
              </tr>
			  <tr>
			    <td align="left">&nbsp;</td>
			    <td align="left">&nbsp;</td>
			    <td align="center">&nbsp;</td>
		    </tr>
			  <% end if
						revisor.movenext()
					loop						
					%>
			  <% set revisor = nothing %>
          </table></td>
        </tr>
      </table>
      <br></td>
    </tr>
    <tr>
      <td align="center" valign="top">
	  <table width="95%" border="0" cellpadding="0" cellspacing="0" class="contornotabla">
        <tr>
          <td width="34%" align="right" valign="middle" class="usatCeldaTitulo"><strong><img src="../../../../images/icon-paperclip.gif" width="10" height="16"></strong></td>
          <td width="66%" align="left" valign="middle" class="usatCeldaTitulo"><strong>&nbsp; Ar</strong><strong>chivos de Propuesta </strong></td>
        </tr>
        <tr>
          <td colspan="2" align="center">
		  <table width="95%" border="0" align="center" cellpadding="0" cellspacing="0">
              <tr>
                <td align="left">&nbsp;</td>
                <td align="left">&nbsp;</td>
              </tr>
<%
					
					Set objArchivo=Server.CreateObject("PryUSAT.clsAccesoDatos")
					objArchivo.AbrirConexiontrans
					set rsArchivosPrp=objArchivo.Consultar("ConsultarArchivosPropuesta","FO","TO",coment1)
					objArchivo.CerrarConexiontrans
					set objArchivo=nothing					
			  		do while not rsArchivosPrp.eof
			  %>
              
			  <tr>
                <td width="8%" align="left">				 
				 <a href="../../../../filespropuestas/<%=prop%>/<%=rsArchivosPrp("nombre_apr")%>" target="_blank">
				 <img src="../../../../images/ext/<%=right(rsArchivosPrp(2),3)%>.gif" width="16" height="16" border="0">
			    </a> </td>
                <td width="92%" align="left"><%response.write(rsArchivosPrp(3))%></td>
              </tr>
              <%
			  	rsArchivosPrp.movenext()
				loop						
			  %>
			  <tr>
                <td align="left">&nbsp;</td>
                <td align="center">&nbsp;</td>
              </tr>
          </table>
		  </td>
        </tr>
      </table></td>
    </tr>
    <tr>
      <td align="center" valign="top">&nbsp;</td>
      <td align="center" valign="top">&nbsp;</td>
    </tr>
  </table>
  <table width="80%" border="0" align="center" cellpadding="0" cellspacing="0">
    <tr>
      <td width="88%" align="center">&nbsp;</td>
      <td width="12%" align="center">&nbsp;</td>
    </tr>
    <tr>
      <td align="center"><span class="usattitulousuario">Observaciones</span></td>
      <td align="center"><a href="javascript:history.back()"><img src="../../../../images/back.gif" width="18" height="18" border="0"></a></td>
    </tr>
    <tr>
      <td align="center">&nbsp;</td>
      <td align="center">Volver</td>
    </tr>
  </table>
  <p>
    <%CargarComentarios coment1%>
  </p>
  <p class="usatTituloAplicacion">&nbsp;</p>
</center>


</body>
</html>
<%
 Public Sub CargarComentarios(byval codigorespuesta_cop)
dim objCom1  
dim RsComentarios
 //consulta mi recordset
 					Set objCom1=Server.CreateObject("PryUSAT.clsAccesoDatos")
					objCom1.AbrirConexiontrans
					set RsComentarios=objCom1.Consultar("ConsultarComentarios","FO","TO",codigorespuesta_cop)
					objCom1.CerrarConexiontrans
					set objCom1=nothing%>
<table  width="97%" align="right" cellspacing="0" border="0" cellpadding="0">
		<%
		do while not  RsComentarios.eof
		o=o+1
		%>
<tr>
<td>
		<%
			if RsComentarios("HIJOS")>0 then
				//response.write(RsComentarios("asunto_cop"))
				//response.write("<br>")
				//inicio %>
	<table width="100%" border="0" cellpadding="0" cellspacing="0" class="contornotabla">
    <tr>
      <td width="323" align="left" valign="middle" class="bordeinf"><strong>De: </strong><%response.write(RsComentarios("nombre"))%></td>
      <td width="380" align="right" valign="middle" class="bordeinf"><strong>Enviado el: </strong><%response.write(RsComentarios("fecha_cop"))%></td>
      <td width="23" align="right" valign="top" class="bordeizq"><strong><img src="../../../../images/icon-paperclip.gif" width="10" height="16"></strong></td>
      <td width="135" align="left" valign="top"><strong>Ar</strong><strong>chivos Adjuntos </strong></td>
    </tr>
    <tr>
      <td colspan="2" align="left" class="bordeinf"><div align="justify"><strong>Asunto: </strong><%response.write(RsComentarios("asunto_cop"))%></div></td>
      <td colspan="2" rowspan="2" align="center" valign="top" class="bordeizq">
	 <table width="95%" border="0" align="center" cellpadding="0" cellspacing="0">
              <tr>
                <td align="left">&nbsp;</td>
                <td align="left">&nbsp;</td>
              </tr>
<%
					
					Set objArchivo=Server.CreateObject("PryUSAT.clsAccesoDatos")
					objArchivo.AbrirConexiontrans
					set rsArchivosPrp=objArchivo.Consultar("ConsultarArchivosPropuesta","FO","TO",RsComentarios("codigo_cop"))
					objArchivo.CerrarConexiontrans
					set objArchivo=nothing					
			  		do while not rsArchivosPrp.eof
			  %>
              
			  <tr>
                <td width="8%" align="left">
			
									
			<a href="../../../../filespropuestas/<%=prop%>/<%=rsArchivosPrp("nombre_apr")%>" target="_blank">
				<img src="../../../../images/ext/<%=right(rsArchivosPrp(2),3)%>.gif" width="16" height="16"  border="0">
				</a>				</td>
                <td width="92%" align="left"><%response.write(rsArchivosPrp(3))%></td>
              </tr>
              <%
			  	rsArchivosPrp.movenext()
				loop						
			  %>
			  <tr>
                <td align="left">&nbsp;</td>
                <td align="center">&nbsp;</td>
              </tr>
          </table>	
	 </td>
    </tr>
    <tr>
      <td colspan="2" align="left"><div align="justify"><strong>Descripci&oacute;n: </strong><%response.write(RsComentarios("comentario_cop"))%></div></td>
    </tr>
    <tr>
      <td colspan="4" align="center" class="bordesup"><table width="100%" border="0" align="left" cellpadding="0" cellspacing="0">
        <tr>
          <td width="5%" align="right">&nbsp;</td>
          <td width="95%">&nbsp;</td>
        </tr>
      </table></td>
    </tr>
  </table>
				<%//fin
				
				call CargarComentarios(RsComentarios("codigo_cop"))				
			else
			
			//inicio
			%>

			  <table width="100%" border="0" cellpadding="0" cellspacing="0" class="contornotabla" align="right">



    <tr>
      <td width="323" align="left" valign="middle" class="bordeinf"><strong>De: </strong><%response.write(RsComentarios("nombre"))%></td>
      <td width="380" align="right" valign="middle" class="bordeinf"><strong>Enviado el: </strong><%response.write(RsComentarios("fecha_cop"))%></td>
      <td width="23" align="right" valign="top" class="bordeizq"><strong><img src="../../../../images/icon-paperclip.gif" width="10" height="16"></strong></td>
      <td width="135" align="left" valign="top"><strong>Ar</strong><strong>chivos Adjuntos </strong></td>
    </tr>
    <tr>
      <td colspan="2" align="left" class="bordeinf"><div align="justify"><strong>Asunto: </strong><%response.write(RsComentarios("asunto_cop"))%></div></td>
      <td colspan="2" rowspan="2" align="center" valign="top" class="bordeizq">
	  
<table width="95%" border="0" align="center" cellpadding="0" cellspacing="0">
              <tr>
                <td align="left">&nbsp;</td>
                <td align="left">&nbsp;</td>
              </tr>
			  		<%		
					Set objArchivo=Server.CreateObject("PryUSAT.clsAccesoDatos")
					objArchivo.AbrirConexiontrans
					set rsArchivosPrp=objArchivo.Consultar("ConsultarArchivosPropuesta","FO","TO",RsComentarios("codigo_cop"))
					objArchivo.CerrarConexiontrans
					set objArchivo=nothing					
			  		do while not rsArchivosPrp.eof
	   			    %>
              
			  <tr>
                <td width="8%" align="left">
			
									
			<a href="../../../../filespropuestas/<%=prop%>/<%=rsArchivosPrp("nombre_apr")%>" target="_blank">
				<img src="../../../../images/ext/<%=right(rsArchivosPrp(2),3)%>.gif" width="16" height="16" border="0">				
				</a>
				</td>
                <td width="92%" align="left"><%response.write(rsArchivosPrp(3))%></td>
              </tr>
              <%
			  	rsArchivosPrp.movenext()
				loop						
			  %>
			  <tr>
                <td align="left">&nbsp;</td>
                <td align="center">&nbsp;</td>
              </tr>
          </table>	  
	  
	  </td>
    </tr>
    <tr>
      <td colspan="2" align="left">
	  <div align="justify"><strong>Descripci&oacute;n: </strong><%response.write(RsComentarios("comentario_cop"))%></div>
	  </td>
    </tr>
    <tr>
      <td colspan="4" align="center" class="bordesup">
	  <table width="100%" border="0" align="left" cellpadding="0" cellspacing="0">
       <tr>
          <td width="5%" align="right">&nbsp;</td>
          <td width="95%">&nbsp;</td>
        </tr>
      </table>
	  </td>
    </tr>

  </table>

			
			
<%//		fin
//		response.write(RsComentarios("asunto_cop"))
	//			response.write("<br>")
			end if				
			RsComentarios.movenext
		%>					
		</td><tr>		
		<%
		loop %>
	</table>
		<%
		set RsComentarios = nothing	
  end Sub
%>