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

	<% ' CAPTURA EL ID DE LA PROPUESTA
	if Request.QueryString("codigo_prp")<>"" then
	codigo_prp=Request.QueryString("codigo_prp")
	%>
<body topmargin="0" leftmargin="0" rightmargin="0">  
<%
' VERIFICA SI ES PROPUESTA PROVENIENTE DE CONSEJO UNIVERSITARIO O DE FACULAD
Set objDestino=Server.CreateObject("PryUSAT.clsAccesoDatos")
objDestino.AbrirConexion
 destino=objDestino.Ejecutar("PRP_ConsejoDeDestino",true,codigo_prp,"")
objDestino.CerrarConexion

if destino="U"  then
%>  	

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
							case "P"%>
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
							case "P"
								response.write("Pendiente")
							case "C"
								response.write("Conforme")
							case "N"
								response.write("No Conforme")
							case "O"
								response.write("Observado")
							case "-"
								response.write("Informado")						
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
							case "P"%>
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
							case "P"
								response.write("Pendiente")
							case "C"
								response.write("Conforme")
							case "N"
								response.write("No Conforme")
							case "O"
								response.write("Observado")								
							case "-"
								response.write("Informado")	
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
					  <td colspan="2" align="left">&nbsp;</td>
					  <td align="center">&nbsp;</td>
					  <td align="center">&nbsp;</td>
			  </tr>
					<tr>
					  <td align="left">-</td>
					  <td align="left"><%response.write(revisor(0))%>                      </td>
					  <td align="center"><%
							select case revisor(2)
							case "P"%>
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
							case "P"
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
							case "P"%>
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
		  		case "P"
					response.write("Pendiente")
		  		case "C"
					response.write("Conforme")
		  		case "N"
					response.write("No Conforme")
				case "O"
					response.write("Observado")	
				case "-"
					response.write("Informado")										
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
<p>
  <%
' si es consejo de facultad
else
%>
</p>
<table width="100%" border="0" cellpadding="0" cellspacing="0">
  <tr>
    <td width="50%" align="center">&nbsp;</td>
    <td width="50%" align="center">&nbsp;</td>
  </tr>
  <tr>
    <td align="center" valign="top"><table width="95%" border="0" align="center" cellpadding="0" cellspacing="3">
      <tr>
        <td colspan="2" align="left"><strong>Consejo de Facultad </strong></td>
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
			set revisor=objRev.Consultar("ConsultarResponsablesPropuesta","FO","FA",codigo_prp,0)
			objRev.CerrarConexiontrans
			set objRev=nothing					
					do while not revisor.eof

						%>
      <tr>
        <td align="left">-</td>
        <td align="left"><%response.write(revisor(0))%></td>
        <td align="center"><%
							select case revisor(2)
							case "P"%>
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
							case "P"
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
      <% 
					revisor.movenext()
					loop
					set revisor = nothing %>
      <%Set objRev1=Server.CreateObject("PryUSAT.clsAccesoDatos")
			objRev1.AbrirConexiontrans
			set revisor1=objRev1.Consultar("ConsultarResponsablesPropuesta","FO","FE",codigo_prp,0)
			objRev1.CerrarConexiontrans
			set objRev1=nothing					
					do while not revisor1.eof

						%>
      <tr>
        <td align="left">-</td>
        <td align="left"><%response.write(revisor1(0))%></td>
        <td align="center"><%
							select case revisor1(2)
							case "P"%>
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
							select case revisor1(2)
							case "P"
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
      <% 
					revisor1.movenext()
					loop
					set revisor1 = nothing %>
					
      <tr>
        <td colspan="2" align="left">&nbsp;</td>
        <td align="center">&nbsp;</td>
        <td align="center">&nbsp;</td>
      </tr>
      <tr>
        <td colspan="2" align="left"><strong>Revisor</strong></td>
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
        <td width="72%" align="left"><%response.write(revisor(0))%>        </td>
        <td align="center"><%
							select case revisor(2)
							case "P"%>
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
							case "P"
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

    </table></td>
    <td align="center" valign="top">
	
	<table width="95%" border="0" align="center" cellpadding="0" cellspacing="3">
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
        <td width="7%" align="center">&nbsp;</td>
        <td width="17%" align="center">&nbsp;</td>
      </tr>
      <tr>
        <td colspan="2" align="left">&nbsp;</td>
        <td align="center">&nbsp;</td>
        <td align="center">&nbsp;</td>
      </tr>
      <tr>
        <td width="3%" align="left">-</td>
        <td width="73%" align="left"><%response.write(revisor(0))%>        </td>
        <td align="center"><%
							select case revisor(2)
							case "P"%>
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
							case "P"
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
      <tr>
        <td align="left">&nbsp;</td>
        <td align="left">&nbsp;</td>
        <td align="center">&nbsp;</td>
        <td align="center">&nbsp;</td>
      </tr>
      <% end if
					revisor.movenext()
					loop						
					
					set revisor = nothing %>
    </table>
	<table width="95%" border="0" align="center" cellpadding="0" cellspacing="3">
      <%
					Set objRev=Server.CreateObject("PryUSAT.clsAccesoDatos")
					objRev.AbrirConexiontrans
					set revisor=objRev.Consultar("ConsultarResponsablesPropuesta","FO","RC",codigo_prp,0)
					objRev.CerrarConexiontrans
					set objRev=nothing					
						
					%>
      <tr>
        <td colspan="2" align="left"><strong><% if revisor.recordcount>1 then%>Consejo Universitario <%end if%> </strong></td>
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
        <td align="center"><%
							select case revisor(2)
							case "P"%>
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
		  		case "P"
					response.write("Pendiente")
		  		case "C"
					response.write("Conforme")
		  		case "N"
					response.write("No Conforme")
				case "O"
					response.write("Observado")	
				case "-"
					response.write("Informado")										
				end select					
				%></td>
      </tr>
      <% end if
					revisor.movenext()
				loop%>
      <% set revisor = nothing %>
    </table>
	<p>&nbsp;</p></td>
  </tr>
</table>
<p>
  <%end if%>
</p>
</body>
<%end if%>
</html>
