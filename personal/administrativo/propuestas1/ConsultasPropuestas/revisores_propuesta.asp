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
<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1"></head>


<p>
	<%
	if Request.QueryString("codigo_prp")<>"" then
	codigo_prp=Request.QueryString("codigo_prp")

	%>
<body topmargin="0" leftmargin="0" rightmargin="0">  
  	
</p>
<table width="100%" height="100%" border="0" align="center" cellpadding="0" cellspacing="0">
  <tr>
    <td align="left" valign="top"><table width="100%" border="0" cellpadding="0" cellspacing="0">

      <tr>
        <td width="50%" align="center">&nbsp;</td>
        <td width="50%" align="center">&nbsp;</td>
      </tr>
      <tr>
        <td align="center" valign="top"><table width="95%" border="0" align="center" cellpadding="0" cellspacing="3">
            <tr>
              <td colspan="2" align="left"><strong>Director del &Aacute;rea </strong></td>
              <td align="center">&nbsp;</td>
              <td align="center">&nbsp;</td>
            </tr>
		
            <tr>
              <td colspan="2" align="left">&nbsp;</td>
              <td align="center">&nbsp;</td>
              <td align="center">&nbsp;</td>
            </tr>
            <%Set objRev=Server.CreateObject("PryUSAT.clsAccesoDatos")
			objRev.AbrirConexiontrans
			set revisor=objRev.Consultar("ConsultarResponsablesPropuesta","FO","RC",codigo_prp,0)
			objRev.CerrarConexiontrans
			set objRev=nothing					
					do while not revisor.eof
						if UCase(revisor(1))="D" then
						%>			
            <tr>		
              <td align="left">-</td>
              <td align="left"><%response.write(revisor(0))%></td>
              <td align="center">					  <%
							select case revisor(2)
							case "S"%>
								<img border="0" src="../../../../images/menus/menu3.gif">
							<%case "C"%>
								<img border="0" src="../../../../images/menus/conforme_small.gif">
							<%case "N"%>
								<img border="0" src="../../../../images/menus/noconforme_small.gif">
							<%case "O"%>
								<img border="0" src="../../../../images/menus/editar_1_s.gif">
							<%end select				

						%></td>
              <td align="center">					  <%
							select case revisor(2)
							case "S"
								response.write("Pendiente")
							case "C"
								response.write("Conforme")
							case "N"
								response.write("No Conforme")
							case "O"
								response.write("Observado")
							end select				

						%></td>
			</tr>  
				<%  end if
					revisor.movenext()
					loop
					set revisor = nothing %>          
            <tr>
              <td colspan="2" align="left">&nbsp;</td>
              <td align="center">&nbsp;</td>
              <td align="center">&nbsp;</td>
            </tr>
            <tr>
              <td colspan="2" align="left"><strong>Revisores</strong></td>
              <td width="4%" align="center">&nbsp;</td>
              <td width="21%" align="center">&nbsp;</td>
            </tr>
            <tr>
              <td colspan="2" align="left">&nbsp;</td>
              <td align="center">&nbsp;</td>
              <td align="center">&nbsp;</td>
            </tr>
            <%		Set objRev=Server.CreateObject("PryUSAT.clsAccesoDatos")
					objRev.AbrirConexiontrans
					set revisor=objRev.Consultar("ConsultarResponsablesPropuesta","FO","RC",codigo_prp,0)
					objRev.CerrarConexiontrans
					set objRev=nothing					
					do while not revisor.eof
						if UCase(revisor(1))="R" then
						%>
					<tr>
					  <td width="3%" align="left">-</td>
					  <td width="72%" align="left">
					  <%response.write(revisor(0))%>					  </td>
					  <td align="center">					  <%
							select case revisor(2)
							case "S"%>
								<img border="0" src="../../../../images/menus/menu3.gif">
							<%case "C"%>
								<img border="0" src="../../../../images/menus/conforme_small.gif">
							<%case "N"%>
								<img border="0" src="../../../../images/menus/noconforme_small.gif">
							<%case "O"%>
								<img border="0" src="../../../../images/menus/editar_1_s.gif">
							<%end select				

						%></td>
					  <td align="center">
					  <%
							select case revisor(2)
							case "S"
								response.write("Pendiente")
							case "C"
								response.write("Conforme")
							case "N"
								response.write("No Conforme")
							case "O"
								response.write("Observado")								
							end select				
						%></td>
											<% end if
					revisor.movenext()
					loop						
					
					set revisor = nothing %>
					</tr>
					<tr>
					  <td align="left">&nbsp;</td>
					  <td align="left">&nbsp;</td>
					  <td align="center">&nbsp;</td>
					  <td align="center">&nbsp;</td>
			  </tr>
					<tr>
					            <%		Set objRev=Server.CreateObject("PryUSAT.clsAccesoDatos")
					objRev.AbrirConexiontrans
					set revisor=objRev.Consultar("ConsultarResponsablesPropuesta","FO","RC",codigo_prp,0)
					objRev.CerrarConexiontrans
					set objRev=nothing					
					do while not revisor.eof
						if UCase(revisor(1))="S" then
						%>
					  <td colspan="2" align="left"><strong>Secretar&iacute;a General </strong></td>
					  <td align="center">&nbsp;</td>
					  <td align="center">&nbsp;</td>
			  </tr>
					<tr>
					  <td align="left">-</td>
					  <td align="left"><%response.write(revisor(0))%>                      </td>
					  <td align="center"><%
							select case revisor(2)
							case "S"%>
                          <img border="0" src="../../../../images/menus/menu3.gif">
                          <%case "C"%>
                          <img border="0" src="../../../../images/menus/conforme_small.gif">
                          <%case "N"%>
                          <img border="0" src="../../../../images/menus/noconforme_small.gif">
                          <%case "O"%>
                          <img border="0" src="../../../../images/menus/editar_1_s.gif">
                          <%end select				

						%></td>
					  <td align="center"><%
							select case revisor(2)
							case "S"
								response.write("Pendiente")
							case "C"
								response.write("Conforme")
							case "N"
								response.write("No Conforme")
							case "O"
								response.write("Observado")								
							end select				
						%></td>
			  </tr>
					<% end if
					revisor.movenext()
					loop						
					
					set revisor = nothing %>
        </table></td>
        <td align="center" valign="top"><table width="95%" border="0" align="center" cellpadding="0" cellspacing="3">
			        <%
					Set objRev=Server.CreateObject("PryUSAT.clsAccesoDatos")
					objRev.AbrirConexiontrans
					set revisor=objRev.Consultar("ConsultarResponsablesPropuesta","FO","RC",codigo_prp,0)
					objRev.CerrarConexiontrans
					set objRev=nothing					
						
					%>
          <tr>
            <td colspan="2" align="left"><strong>Consejo Universitario </strong></td>
            <td width="7%" align="center">&nbsp;</td>
            <td width="17%" align="center">&nbsp;</td>
          </tr>
          <tr>
            <td colspan="2" align="left">&nbsp;</td>
            <td align="center">&nbsp;</td>
            <td align="center">&nbsp;</td>
          </tr>
          <%
					revisor.movefirst()
					do while not revisor.eof
						if ucase(revisor(1))="C" then
%>
          <tr>
            <td width="3%" align="left">-</td>
            <td width="73%" align="left"><%response.write(revisor(0))%></td>
            <td align="center">					  <%
							select case revisor(2)
							case "S"%>
								<img border="0" src="../../../../images/menus/menu3.gif">
							<%case "C"%>
								<img border="0" src="../../../../images/menus/conforme_small.gif">
							<%case "N"%>
								<img border="0" src="../../../../images/menus/noconforme_small.gif">
							<%case "O"%>
								<img border="0" src="../../../../images/menus/editar_1_s.gif">
							<%end select				

						%></td>
            <td align="center"><%
				select case UCase(revisor(2))
		  		case "S"
					response.write("Pendiente")
		  		case "C"
					response.write("Conforme")
		  		case "N"
					response.write("No Conforme")
				case "O"
					response.write("Observado")					
				end select					
				%></td>
          </tr>
          <% end if
					revisor.movenext()
				loop%>
          <% set revisor = nothing %>
        </table></td>
      </tr>
    </table></td>
  </tr>
</table>
</body>
<%end if%>
</html>
