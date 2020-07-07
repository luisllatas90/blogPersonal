<%@LANGUAGE="JAVASCRIPT" CODEPAGE="1252"%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />
<title>Documento sin t&iacute;tulo</title>
</head>

<body>
<%

Public Sub CargarComentarios(byval codigorespuesta_cop)
dim objCom1  
dim RsComentarios
 //consulta mi recordset
 					Set objCom1=Server.CreateObject("PryUSAT.clsAccesoDatos")
					objCom1.AbrirConexiontrans
					set RsComentarios=objCom1.Consultar("ConsultarComentarios","FO","TO",codigorespuesta_cop,0)
					objCom1.CerrarConexiontrans
					set objCom1=nothing%>
		<table  width="97%" align="right" cellspacing="0" border="0" cellpadding="0"><%
		do while not  RsComentarios.eof
		o=o+1
		%>
		<tr><td>
		<%
			if RsComentarios("HIJOS")>0 then
				//response.write(RsComentarios("asunto_cop"))
				//response.write("<br>")
				//inicio 
						''response.write(RsComentarios("codigo_cop"))
 					Set objLeido=Server.CreateObject("PryUSAT.clsAccesoDatos")
					objLeido.AbrirConexion
					set RsLeido=objLeido.Consultar("ConsultarComentarios","FO","CP",RsComentarios(0),session("codigo_Usu"))
					objLeido.CerrarConexion
					set objLeido=nothing
				%>
				  <table width="100%"
				  <%if   RsLeido.BOF=true then %> 
			  		bgcolor="#E1F1FB"
			  		<% end if%> 
			 		 width="100%" style="cursor:hand" border="0" cellpadding="0" cellspacing="0" class="contornotabla">
    <tr>
      <td width="85%" align="left" valign="middle" class="bordeinf"><strong>De: </strong><%response.write(RsComentarios("nombre"))%></td>
      <td width="15%" align="center" valign="middle" class="bordeinf">	 
	  	  <%
					if   RsLeido.BOF=true then%>
					<font color="#3399CC"> <strong>No le&iacute;do </strong></font>
					<%else%>
					&nbsp;
					<%end if%>	  
	  </td>
    </tr>
    <tr>
      <td colspan="2" align="left" class="bordeinf"><div align="justify"><strong>Asunto: </strong><%response.write(RsComentarios("asunto_cop"))%></div></td>
      </tr>
    <tr>
      <td colspan="2" align="left" class="bordeinf"><strong>Enviado el: </strong>
        <%response.write(RsComentarios("fecha_cop"))%></td>
      </tr>
    
    <tr>
      <td colspan="2" align="center" class="bordesup"><table width="100%" border="0" align="left" cellpadding="0" cellspacing="0">
        <tr>
          <td align="left"><img src="../../../../images/rpta.GIF">&nbsp;&nbsp;<a href="registraobservacion1.asp?comentario=<%=RsComentarios("codigo_cop")%>&codigo_prop=<%=prop%>" class="usatError"></a></td>
          </tr>
      </table></td>
    </tr>
  </table>
				<%//fin				
				call CargarComentarios(RsComentarios("codigo_cop"))				
			else			
			//inicio
					''response.write(RsComentarios(0))
 					Set objLeido=Server.CreateObject("PryUSAT.clsAccesoDatos")
					objLeido.AbrirConexion
					set RsLeido=objLeido.Consultar("ConsultarComentarios","FO","CP",RsComentarios(0),session("codigo_Usu"))
					objLeido.CerrarConexion
			%>
			  <table <%if   RsLeido.BOF=true then %> 
			  bgcolor="#E1F1FB"
			  <% end if%> 
			  width="100%" style="cursor:hand" border="0" cellpadding="0" cellspacing="0" class="contornotabla" align="right">
    <tr>
      <td width="85%" align="left" valign="middle" class="bordeinf"><strong>De: </strong><%response.write(RsComentarios("nombre"))%></td>
      <td width="15%" align="center" valign="middle" class="bordeinf">	  
	  	  	  <% 
					set objLeido=nothing
					if   RsLeido.BOF=true then%>
					<font color="#3399CC"> <strong>No le&iacute;do </strong></font>
					<%else%>
					&nbsp;
					<%end if%>	  
	  </td>
    </tr>
    <tr>
      <td colspan="2" align="left" class="bordeinf"><div align="justify"><strong>Asunto: </strong><%response.write(RsComentarios("asunto_cop"))%></div></td>
      </tr>
    <tr>
      <td colspan="2" align="left" class="bordeinf"><strong>Enviado el: </strong>
        <%response.write(RsComentarios("fecha_cop"))%></td>
      </tr>    
    <tr>
      <td colspan="2" align="center" class="bordesup"><table width="100%" border="0" align="left" cellpadding="0" cellspacing="0">
        <tr>
          <td align="left"><img src="../../../../images/rpta.GIF">&nbsp;</td>
          </tr>
      </table></td>
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
</body>
</html>
