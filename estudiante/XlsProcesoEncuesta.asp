<%
	Response.ContentType = "application/msexcel"
	Response.AddHeader "Content-Disposition","attachment;filename=Reporte.xls"
%>
<style type="text/css">
<!--
body,td,th {
	font-family: Verdana, Arial, Helvetica, sans-serif;
	font-size: 12px;
	color: #000000;
}
-->
</style>
<CENTER>
  <%
'	Response.ContentType = "application/msexcel"
'	Response.AddHeader "Content-Disposition","attachment;filename=Cotizacion.xls"
'	Response.Write(Request.Form("cboPregunta"))
'	Response.Write(Request.Form("cboCarrera"))
	pregunta = Request.Form("cboPregunta")
	carrera = Request.Form("cboCarrera")
	if pregunta="SELECCIONAR"	 THEN
		Response.Write("<br><br>")
		Response.Write("<h3>Seleccione una pregunta para consultar sus resultados</h3>")
	ELSE
		if carrera<>"-2" then
			pregunta=pregunta&"E"
			Response.Write("<h3>" & Request.Form("txtPregunta") & "  x " & carrera & "</H3>" )
		else
			Response.Write("<H3>" & Request.Form("txtPregunta") & "</H3>")
		end if
		Set obj=Server.CreateObject("PryUSAT.clsAccesoDatos")
			Obj.AbrirConexion
				set mensaje=obj.Consultar("ConsultaProcesodeEncuesta","FO",pregunta,carrera)
			Obj.CerrarConexion
		Set obj=nothing
		filas =mensaje.recordcount
		columnas = mensaje.Fields.Count
	'Response.Write(columnas)%>
	
	<table border="0">
	<tr>

		<%	for i = 0 to columnas-1%>
			<td bgcolor="#FFFF99" width="150" align="center">

			  <strong>
			  <% RESPONSE.Write(mensaje.Fields.Item(i).Name) %>
		    </strong> </td>
					
		    <%
			next
		%>
	</tr>
	<% for j=0 to filas-1 %>
	<% if j=filas-1 then %>
	<tr bgcolor="#FFFF99">
	<%else%>
	<tr>
	<%end if%>
		<%for k = 0 to columnas-1 %>
		
		<%if k=2 then
		Response.Write("<td align=right>")
		Response.Write(formatnumber(mensaje.fields(k),1) &"&nbsp;%")
		else
		if k=1 then
			Response.Write("<td align=right>")
		else
			Response.Write("<td>")
		end if
		Response.Write(mensaje.fields(k)&"&nbsp;")
		end if
		%>
		</td>
		<%next%>
	</tr>	
	<%
	mensaje.movenext
	next
	
	end if
	%>

	</table> 
</CENTER>