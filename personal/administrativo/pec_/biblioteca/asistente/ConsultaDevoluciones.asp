<!--#include file="../../../../funciones.asp"-->
<script>
	function EnviarDatos(pagina)
	{
		frmpropuesta.action=pagina;
		frmpropuesta.submit();
	}
</script>

<html>
<head>
<title>Devoluciones y préstamos</title>

<link href="../../../../private/estilo.css" rel="stylesheet" type="text/css">
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
                  <td><table width="95%" border="0" align="center" cellpadding="3" cellspacing="0" bgcolor="#FFFFFF" class="contornotabla">
                    <tr>
                      <td>&nbsp;</td>
                      <td>&nbsp;</td>
                      <td colspan="2">&nbsp;</td>
                    </tr>
                    <tr>
                      <td>&nbsp;</td>
                      <td>&nbsp;</td>
                      <td>&nbsp;</td>
                      <td width="38%" rowspan="4" align="center" valign="middle">&nbsp;</td>
                    </tr>
                    <tr>
                      <td>&nbsp;</td>
                      <td><span class="Estilo8"><strong>Pr&eacute;stamo/Devoluci&oacute;n</strong></span></td>
                      <td><select name="CboTipo" id="CboTipo">
                        <option value="6"  <%if tipo="6" then %> selected="selected" <%end if%>>Pr&eacute;stamo</option>
                        <option value="5" <%if tipo="5" then %> selected="selected" <%end if%>>Devoluci&oacute;n</option>
                      </select>                      </td>
                    </tr>
                    <tr>
                      <td width="3%">&nbsp;</td>
                      <td width="20%"><span class="Estilo14">Fecha de Inicio </span></td>
                      <td width="39%"><span class="Estilo8">
                        <input  name="txtFechaInicio" type="text" class="Cajas" id="txtFechaInicio" value="<%=fechainicio%>">
                        <input name="Submit2" type="button" class="cunia" onClick="MostrarCalendario('txtFechaInicio')" >
                      </span></td>
                    </tr>
                    <tr>
                      <td>&nbsp;</td>
                      <td><span class="Estilo14">Fecha de Fin </span></td>
                      <td><span class="Estilo8">
                        <input name="txtFechaFin" type="text" class="Cajas" id="txtFechaFin" value="<%=fechaFin%>">
                        <input name="Submit3" type="button" class="cunia" value="  " onClick="MostrarCalendario('txtFechaFin')" >
                      </span></td>
                    </tr>
                    <tr>
                      <td>&nbsp;</td>
                      <td><span class="Estilo8"><strong>Usuario</strong></span></td>
                      <td><span class="Estilo8">
                        <input name="txtUsuario" type="text" class="Cajas" id="txtUsuario" value="<%=usuario%>">
                      </span></td>
                      <td align="center" valign="middle">&nbsp;</td>
                    </tr>
                    <tr>
                      <td>&nbsp;</td>
                      <td><span class="Estilo8"><strong>Ubicaci&oacute;n</strong></span></td>
                      <td><select name="CboUbicacion" id="CboUbicacion">
                        <option value="21" <%if ubicacion="21" then%> selected="selected"<%end if%>>Humanidades</option>
                        <option value="1" <%if ubicacion="1" then%> selected="selected"<%end if%>>Ciencias </option>
                        <option value="45" <%if ubicacion="45" then%> selected="selected"<%end if%>>Ciencias de la salud</option>
                        <option value="46" <%if ubicacion="46" then%> selected="selected"<%end if%>>Derecho</option>
                        <option value="9" <%if ubicacion="9" then%> selected="selected"<%end if%>>Hemeroteca </option>
                        <option value="10" <%if ubicacion="10" then%> selected="selected"<%end if%>>Referencia y Tesis </option>
                        <option value="47" <%if ubicacion="47" then%> selected="selected"<%end if%>>Mediateca</option>
                      </select>                      </td>
                      <td align="center" valign="middle">&nbsp;</td>
                    </tr>
                    <tr>
                      <td>&nbsp;</td>
                      <td>&nbsp;</td>
                      <td>&nbsp;</td>
                      <td align="center" valign="middle">&nbsp;</td>
                    </tr>
                    <tr>
                      <td colspan="2">&nbsp;</td>
                      <td colspan="2" align="right"><strong><a href="javascript:history.back()"><span class="Estilo15">Volver</span></strong></a> </td>
                    </tr>
                    <tr>
                      <td colspan="4" align="center" bgcolor="#F0F0F0" class="contornotabla">
					  <input name="Submit" type="Submit" class="buscar2" style="height:25" value="    Consultar" >		
					  <input type="button" name="cmdExportar" id="cmdexportar" style="width:85" CLASS="excel" onClick="EnviarDatos('XLS_libromasprestado.asp')" value="    Exportar" >
		      </td>
                    </tr>
                    
                    
                  </table></td>
                </tr>
                <tr>
                  <td>
  <%
  

	 Set objProp=Server.CreateObject("Biblioteca.clsAccesoDatos")
	 objProp.AbrirConexion
	  
	 set Libros=objProp.Consultar("ConsultarPrestamos","FO",tipo,cDate(fechaInicio),cDate(fechaFin),ubicacion,usuario,"")

	 objProp.CerrarConexion
	 set objProP=nothing
	// Response.Write(Libros.RecordCount) 

	%>				  </td>
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
