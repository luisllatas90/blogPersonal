

<html xmlns="http://www.w3.org/1999/xhtml">

<head>
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252" />
<title>Consulta de disponibilidad de ambientes</title>
<link rel="stylesheet" type="text/css" href="../../../private/estilo.css">
</head>

<body>
<%
	dim rs 
	dim rs_ambiente
	DIM rs_horarios
	set cn=server.createobject("pryusat.clsaccesodatos")

	codigo_cac=request.querystring("codigo_Cac")

	cn.abrirconexion
	

	
	
	set rs_horarios= cn.consultar ("sp_vercrucesambientesporescuela","FO",codigo_Cac,codigo_Cac)
	response.write("<table border='1'>")
		response.write		"<tr>"
		for i=0 to  rs_horarios.fields.count-1
			response.write		("<td class='LU'>" & ucase(rs_horarios.fields(i).name) &"</td>")
		next
		response.write		"</tr>"
	while rs_horarios.eof =false and rs_horarios.bof =false 

				response.write("<tr>")
					for i=0 to  rs_horarios.fields.count-1
						response.write		("<td>" & rs_horarios.fields(i) &" </td>")
					next
				response.write("</tr>")
		
		rs_horarios.movenext 
	wend
	response.write("</table>")
	
	
	set rs_ambiente=cn.consultar("consultarhorarios","FO",8,30,0,0)
	
		while  rs_ambiente.eof=false and rs_ambiente.bof=false		
			if rs_ambiente.fields("capacidad_amb")<>-485 then
				set rs=cn.consultar("sp_pintarhorario","FO",rs_ambiente.fields("codigo_amb"),cdate("2008/03/01"),cdate("2008/07/31"))
				response.write ("<p>" & rs_ambiente.fields("ambiente") &" (Capacidad : " & rs_ambiente.fields("capacidad_amb") & " )</p>")
				response.write("<table border='1' >")
				response.write("<tr>")
					for i=0 to  rs.fields.count-1
						response.write		("<td >" & rs.fields(i).name &" </td>")
					next
				response.write("</tr>")
				
					while  rs.eof=false and rs.bof=false 
						response.write("<tr>")
						for i=0 to rs.fields.count-1
		
							if i>0 then
								if rs.fields(i)>0 then						
									response.write ("<td class='LU'> " & rs.fields(i) & " </td>")
								else
									response.write ("<td> " & rs.fields(i) & " </td>")
								end if 
							else
								response.write ("<td> " & rs.fields(i) & " </td>")
							end if
						next
						response.write("</tr>")
						rs.movenext
					wend
					

				response.write("</table>")
			end if 
   			rs_ambiente.movenext
			
		wend

	cn.cerrarconexion
%>
</body>

</html>