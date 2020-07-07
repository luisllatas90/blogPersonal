<%
Dim ArrEncabezados,titulorpte,ArrCampos,ArrCeldas,IncluyeContador
Dim i,j,k
Dim rsDatos

	tipoarchivo=request.querystring("tipoarchivo")
	archivo=request.querystring("archivo")	
	IncluyeContador=request.querystring("IncluyeContador")
	titulorpte=session("titulorpte")
	Set rsDatos=session("rsDatos")
	ArrEncabezados=session("ArrEncabezados")
	ArrCampos=session("ArrCampos")
	ArrCeldas=session("ArrCeldas")
	if tipoarchivo="" then tipoarchivo="xls"
	if archivo="" then archivo="rpte" & day(now) & year(now)
	i=0:j=0
	
if (rsDatos.BOF AND rsDatos.EOF)=true AND (IsEmpty(ArrCampos)=false) then
	response.write "<script>alert('No se han encontrado registros en la Base de datos')</script>"
else

	if tipoarchivo="xls" then
		Response.ContentType = "application/vnd.ms-excel"
	else
		Response.ContentType = "application/msword"
	end if
	
	Response.AddHeader "Content-Disposition","attachment;filename=" & archivo & "." & tipoarchivo
%>
<html>
<head>
<meta name="GENERATOR" content="Microsoft FrontPage 5.0">
<meta name="ProgId" content="FrontPage.Editor.Document">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
</head>
<body>
<table border="1" cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%" id="tbllista">
  <%if titulorpte<>"" then%><caption><b><font size="4"><%=titulorpte%></font></b></caption><%end if%>
  <tr style="background-color: #FFFFCC; font-weight:bold">
  <%if incluyeContador="S" then%><td width="5%" height="5%">Nº</td><%end if
	for i=lbound(ArrEncabezados) to ubound(ArrEncabezados)%>
	  <td width="<%=ArrCeldas(i)%>" align="center" height="5%"><%=ArrEncabezados(i)%>&nbsp;</td>
	<%next%>
  </tr>
	  <%
	  rsDatos.MoveFirst
	  Do While Not rsDatos.EOF
	  	  j=j+1%>
  <tr><%if incluyeContador="S" then%><td width="5%"><%=j%>&nbsp;</td><%end if
	 for k=lbound(ArrEncabezados) to ubound(ArrEncabezados)
	 		
	 		if trim(ArrCampos(k))="codigouniver_alu" then
	 			response.write "<td width=""" & ArrCeldas(k) & """>&nbsp;" & cstr(rsDatos(ArrCampos(k))) & "</td>"
	 		else
		 		response.write "<td width=""" & ArrCeldas(k) & """>" & rsDatos(ArrCampos(k)) & "</td>"
		 	end if
	 next%>
  </tr>
	  	<%rsDatos.movenext
	  Loop
	  %>
	</table>
</body>
</html>
<%
end if
'Limpiar valores
Set rsDatos=nothing
%>