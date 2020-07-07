<!--#include file="../../../../funciones.asp"-->


<html>
<head>
<title>Registro de Propuestas</title>

<style type="text/css">
<!--
body {
	background-color: #f0f0f0;
}
.Estilo4 {
	color: #395ACC;
	font-weight: bold;
	font-size: 12pt;
}
.Estilo8 {color: #000000}
.Estilo12 {color: #395ACC; font-weight: bold; }
.Estilo14 {color: #000000; font-weight: bold; }
.Estilo15 {color: #395ACC}
-->
</style>
<script language="JavaScript" src="../../../../funciones.js"></script>
<script language="JavaScript" src="../../../../private/calendario.js"></script>


</head>
<%

fechaInicio=Request.Form("txtFechaInicio")
fechaFin=Request.Form("txtFechaFin")
tipo= Request.Form("cbotipo")
ubicacion=Request.Form("CboUbicacion")
usuario=Request.Form("txtUsuario")

''response.write fechaInicio & " - "  & fechaFin
if fechaInicio="" and fechaFin="" then
	fechaInicio=Date-1
	fechaFin=Date-1
end if

if tipo="" then
	tipo="5"
end if
if ubicacion="" then
	ubicacion="1"
end if

if usuario = "" then
	usuario=" "
end if
%>
<body topmargin="0" rightmargin="0" leftmargin="0">
		    <form method="post"  name="frmpropuesta" id="frmpropuesta" action="ConsultaDevoluciones.asp">
		      <table width="100%" border="0" cellpadding="0" cellspacing="0">
                <tr>
                  <td align="center">&nbsp;</td>
                </tr>
                <tr>
                  <td align="center"><span class="Estilo4">Reporte de Pr&eacute;stamos y Devoluciones </span></td>
                </tr>
                <tr>
                  <td>&nbsp;</td>
                </tr>
                <tr>
                  <td>&nbsp;</td>
                </tr>
                <tr>
                  <td>
<%
	Response.ContentType = "application/msexcel"
	Response.AddHeader "Content-Disposition","attachment;filename=ordencompra.xls"
%>
  <%
  

	 Set objProp=Server.CreateObject("Biblioteca.clsAccesoDatos")
	 objProp.AbrirConexion
	  
	 set Libros=objProp.Consultar("ConsultarPrestamos","FO",tipo,cDate(fechaInicio),cDate(fechaFin),ubicacion,usuario,"")

	 objProp.CerrarConexion
	 set objProP=nothing
	// Response.Write(Libros.RecordCount) 

	%>
	
					  </td>
                </tr>
                <tr>
                  <td>&nbsp;</td>
                </tr>
                <tr>
                  <td>&nbsp;</td>
                </tr>
                <tr>
                  <td>
				  <%if tipo="5" then%>
				  <table width="95%" border="0" align="center" cellpadding="0" cellspacing="0" class="contornotabla">
                    <tr>
                      <td width="4%" align="center" bgcolor="#E1F1FB" class="bordeizqinf"><span class="Estilo12">Num.</span></td>

                      <td width="8%" align="center" bgcolor="#E1F1FB" class="bordeizqinf"><span class="Estilo12">Fecha Pr&eacute;stamo </span></td>
                      <td width="10%" align="center" bgcolor="#E1F1FB" class="bordeizqinf"><span class="Estilo12">C&oacute;digo </span></td>
                      <td width="11%" align="center" bgcolor="#E1F1FB" class="bordeizqinf"><span class="Estilo12">T&iacute;tulo</span></td>
                      <td width="9%" align="center" bgcolor="#E1F1FB" class="bordeizqinf"><span class="Estilo12">Autor </span></td>
                      <td width="9%" align="center" bgcolor="#E1F1FB" class="bordeizqinf"><span class="Estilo12">C&oacute;digo Lector </span></td>
                      <td width="7%" align="center" bgcolor="#E1F1FB" class="bordeizqinf"><span class="Estilo12">Lector</span></td>
                      <td width="7%" align="center" bgcolor="#E1F1FB" class="bordeizqinf"><span class="Estilo12">Usuario<br>
                     Pr&eacute;stamo </span></td>
                      <td width="7%" align="center" bgcolor="#E1F1FB" class="bordeizqinf"><span class="Estilo12">Fecha<br>
                      Devoluci&oacute;n</span></td>
                      <td width="7%" align="center" bgcolor="#E1F1FB" class="bordeizqinf"><span class="Estilo12">Usuario<br>
                     Devoluci&oacute;n </span></td>
                    </tr>
					<%do while not Libros.eof%>                    
					<tr>
					<%i=i+1%>
                      <td align="center" bgcolor="#FFFFFF" class="bordeizqinf Estilo8"><%=i%></td>

                      <td align="center" bgcolor="#FFFFFF" class="bordeizqinf Estilo8"><%=Libros(0)%></td>
                      <td align="center" bgcolor="#FFFFFF" class="bordeizqinf Estilo8"><%=Libros(1)%></td>
                      <td align="center" bgcolor="#FFFFFF" class="bordeizqinf Estilo8"><%=Libros(2)%></td>
                      <td align="center" bgcolor="#FFFFFF" class="bordeizqinf Estilo8"><%=Libros(3)%></td>
                      <td align="center" bgcolor="#FFFFFF" class="bordeizqinf Estilo8"><%=Libros(4)%></td>
                      <td align="center" bgcolor="#FFFFFF" class="bordeizqinf Estilo8"><%=Libros(5)%></td>
                      <td align="center" bgcolor="#FFFFFF" class="bordeizqinf Estilo8"><%=Libros(6)%></td>
                      <td align="center" bgcolor="#FFFFFF" class="bordeizqinf Estilo8"><%=Libros(7)%></td>
                      <td align="center" bgcolor="#FFFFFF" class="bordeizqinf Estilo8"><%=Libros(8)%></td>
                    </tr>
					<%
					Libros.MoveNext
					loop%>
                  </table>
				  <%else%>
				  <table width="95%" border="0" align="center" cellpadding="0" cellspacing="0" class="contornotabla">
                    <tr>
                      <td width="4%" align="center" bgcolor="#E1F1FB" class="bordeizqinf"><span class="Estilo12">Num.</span></td>

                      <td width="8%" align="center" bgcolor="#E1F1FB" class="bordeizqinf"><span class="Estilo12">Fecha Pr&eacute;stamo </span></td>
                      <td width="10%" align="center" bgcolor="#E1F1FB" class="bordeizqinf"><span class="Estilo12">C&oacute;digo </span></td>
                      <td width="11%" align="center" bgcolor="#E1F1FB" class="bordeizqinf"><span class="Estilo12">T&iacute;tulo</span></td>
                      <td width="9%" align="center" bgcolor="#E1F1FB" class="bordeizqinf"><span class="Estilo12">Autor </span></td>
                      <td width="9%" align="center" bgcolor="#E1F1FB" class="bordeizqinf"><span class="Estilo12">C&oacute;digo Lector </span></td>
                      <td width="7%" align="center" bgcolor="#E1F1FB" class="bordeizqinf"><span class="Estilo12">Lector</span></td>
                      <td width="7%" align="center" bgcolor="#E1F1FB" class="bordeizqinf"><span class="Estilo12">Usuario<br>
                     Pr&eacute;stamo </span></td>
                      <td width="7%" align="center" bgcolor="#E1F1FB" class="bordeizqinf"><span class="Estilo12">Fecha<br>
                      Devoluci&oacute;n</span></td>
                      <td width="7%" align="center" bgcolor="#E1F1FB" class="bordeizqinf"><span class="Estilo12">Usuario<br>
                     Devoluci&oacute;n </span></td>
                    </tr>
					<%do while not Libros.eof%>                    
					<tr>
					<%i=i+1%>
                      <td align="center" bgcolor="#FFFFFF" class="bordeizqinf Estilo8"><%=i%></td>

                      <td align="center" bgcolor="#FFFFFF" class="bordeizqinf Estilo8"><%=Libros(0)%></td>
                      <td align="center" bgcolor="#FFFFFF" class="bordeizqinf Estilo8"><%=Libros(1)%></td>
                      <td align="center" bgcolor="#FFFFFF" class="bordeizqinf Estilo8"><%=Libros(2)%></td>
                      <td align="center" bgcolor="#FFFFFF" class="bordeizqinf Estilo8"><%=Libros(3)%></td>
                      <td align="center" bgcolor="#FFFFFF" class="bordeizqinf Estilo8"><%=Libros(4)%></td>
                      <td align="center" bgcolor="#FFFFFF" class="bordeizqinf Estilo8"><%=Libros(5)%></td>
                      <td align="center" bgcolor="#FFFFFF" class="bordeizqinf Estilo8"><%=Libros("login")%></td>
                      <td align="center" bgcolor="#FFFFFF" class="bordeizqinf Estilo8">-</td>
                      <td align="center" bgcolor="#FFFFFF" class="bordeizqinf Estilo8">-</td>
                    </tr>
					<%
					Libros.MoveNext
					loop%>
                  </table>				  
				  <%end if%>
				  
				  </td>
                </tr>
                <tr>
                  <td>&nbsp;</td>
                </tr>
              </table>
</form>

	     	
</body>
</html>
