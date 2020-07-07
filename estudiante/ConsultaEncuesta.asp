<%

	Response.ContentType = "application/msexcel"
	Response.AddHeader "Content-Disposition","attachment;filename=Cotizacion.xls"
		
		Set obj=Server.CreateObject("PryUSAT.clsAccesoDatos")
			Obj.AbrirConexion
				set mensaje=obj.Consultar("ENC_ConsultarEncuesta","FO")
			Obj.CerrarConexion
		Set obj=nothing
		filas =mensaje.recordcount
		columnas = mensaje.Fields.Count
	'Response.Write(columnas)%>
	<table border="1">
	<tr>
		<td>
		N°
		</td>
		<%	for i = 0 to columnas-1%>
			<td>
			<% RESPONSE.Write(mensaje.Fields.Item(i).Name) %>
			</td>
					
		<%
			next
		%>
	</tr>
	<% for j=0 to filas-1 %>
	<tr>
		<td>
		<%
		Response.Write(j+1)
		%>
		</td>
		<%for k = 0 to columnas-1 %>
		<td>
		<%
		Response.Write(mensaje.fields(k))
		%>
		</td>
		<%next%>
	</tr>	
	<%
	mensaje.movenext
	next%>
	
	</table>