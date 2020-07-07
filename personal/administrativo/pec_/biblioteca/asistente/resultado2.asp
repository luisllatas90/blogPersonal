<html>
<head>

<title>Vista previa de etiquetas de Biblioteca - USAT</title>
<link href="../../../private/estilo.css" rel="stylesheet" type="text/css">
<style type="text/css">
<!--
.Estilo1 {color: #000000}
.Estilo2 {color: #000000; font-weight: bold; }
.Estilo4 {color: #000000; font-weight: bold; font-size: 16px; }
-->
</style>
<link href="../../../../private/estilo.css" rel="stylesheet" type="text/css">
</head>
<%
''Response.ContentType = "application/msword"
''Response.AddHeader "Content-Disposition","attachment;filename=" & NOMBRE & ".doc"
%>
<body topmargin="0" leftmargin="0" rightmargin="0">


<%
if Request.QueryString("accion")="rango"then
Inicio=Request.Form("txtInicio")
Fin=Request.Form("TxtFin")
	 Set objProp=Server.CreateObject("Biblioteca.clsAccesoDatos")
	 objProp.AbrirConexion
	 set RsEtiquetas=objProp.Consultar("ConsultarReportes","FO","14",inicio,fin,0,0,0,0)

	 objProp.CerrarConexion
	 set objProP=nothing
	 Counter=RsEtiquetas.RecordCount
	'' Response.Write(Counter)
	 %>
<table width="100%" border="0" cellspacing="0" cellpadding="0" >
<% 
if Counter=0 then
Response.Write("No se encontraron etiquetas en el Rango Solicitado: " & Inicio & " - " & Fin)
else
for i=1 to counter%>

  <tr>
<%	if i mod 2 <> 0 then%>
    <td width="50%"><%

		''Response.Write(i)

	e=i+1
	''RsEtiquetas.MoveNext
	%>
      <table width="100%" border="0" cellspacing="2" cellpadding="2">
      <%
	 Set objProp=Server.CreateObject("Biblioteca.clsAccesoDatos")
	 objProp.AbrirConexion
	 set RsEtiqueta=objProp.Consultar("ConsultarReportes","FO","18",RsEtiquetas("IdIngreso"),0,0,0,0,0)

	 objProp.CerrarConexion
	 set objProP=nothing
	 %>
      <tr>
        <td width="25%" rowspan="2" align="center" class="bordeder Estilo1"><table width="80%" border="0" cellspacing="0" cellpadding="0">
          <tr>
            <td align="center"><span class="Estilo4">
              <%
		  	COD_EJEMP =RsEtiqueta("CodigoIngreso")
			COD_EJEMP=replace (COD_EJEMP,"/", "<br>")
			COD_EJEMP=replace (COD_EJEMP,"Ej","<BR>Ej")
			COD_EJEMP=replace (COD_EJEMP,"v","<BR>v")
		 	RESPONSE.Write COD_EJEMP %>
            </span></td>
          </tr>
        </table></td>
        <td class="bordeinf"><table width="100%" border="0" cellspacing="1" cellpadding="1">
            <tr>
              <td width="1%" class="Estilo2">&nbsp;</td>
              <%
        If rsetiqueta("Abreviatura_pre") <> "" Then
            codigo = rsetiqueta("Abreviatura_pre") & "/" & rsetiquetas("CodigoIngreso")
        Else
            codigo = rsetiqueta("CodigoIngreso")
        End If
%>
              <td width="99%" class="Estilo2"><%=codigo%></td>
            </tr>
            <tr>
              <td class="Estilo1">&nbsp;</td>
              <td class="Estilo1"><%=LEFT(RsEtiqueta("titulo"),45)%></td>
            </tr>
            <tr>
              <td class="Estilo1">&nbsp;</td>
              <td class="Estilo1"><%=LEFT(RsEtiqueta("NombreAutor"),45)%></td>
            </tr>
            <tr>
              <td class="Estilo2">&nbsp;</td>
              <td class="Estilo2"><%rESPONSE.Write "USAT-" & RsEtiqueta("NumeroIngreso")%></td>
            </tr>
        </table></td>
      </tr>
      <tr>
        <td><table width="100%" border="0" cellspacing="1" cellpadding="1">
            <tr>
              <td width="1%" class="Estilo2">&nbsp;</td>
              <%
        If rsetiqueta("Abreviatura_pre") <> "" Then
            codigo = rsetiqueta("Abreviatura_pre") & "/" & rsetiquetas("CodigoIngreso")
        Else
            codigo = rsetiqueta("CodigoIngreso")
        End If
%>
              <td width="99%" class="Estilo2"><%=codigo%></td>
            </tr>
            <tr>
              <td class="Estilo1">&nbsp;</td>
              <td class="Estilo1"><%=LEFT(RsEtiqueta("titulo"),45)%></td>
            </tr>
            <tr>
              <td class="Estilo1">&nbsp;</td>
              <td class="Estilo1"><%=LEFT(RsEtiqueta("NombreAutor"),45)%></td>
            </tr>
            <tr>
              <td class="Estilo2">&nbsp;</td>
              <td class="Estilo2"><%rESPONSE.Write "USAT-" & RsEtiqueta("NumeroIngreso")%></td>
            </tr>
        </table></td>
      </tr>
	  <%SET RsEtiqueta=NOTHING%>
    </table></td>
    <%	end if%>
	<%if e<=counter then %>
		<%if e Mod 2 = 0  then
		I=I+1%>

	<td width="50%">
		<%''Response.Write(e)
		RsEtiquetas.MoveNext
		%>
		<table width="100%" border="0" cellspacing="2" cellpadding="2">
          <%
	 Set objProp=Server.CreateObject("Biblioteca.clsAccesoDatos")
	 objProp.AbrirConexion
	 set RsEtiqueta=objProp.Consultar("ConsultarReportes","FO","18",RsEtiquetas("IdIngreso"),0,0,0,0,0)

	 objProp.CerrarConexion
	 set objProP=nothing
	 %>
          <tr>
            <td width="25%" rowspan="2" align="center" class="bordeder Estilo1"><table width="80%" border="0" cellspacing="0" cellpadding="0">
              <tr>
                <td align="center"><span class="Estilo4">
                  <%
		  	COD_EJEMP =RsEtiqueta("CodigoIngreso")
			COD_EJEMP=replace (COD_EJEMP,"/", "<br>")
			COD_EJEMP=replace (COD_EJEMP,"Ej","<BR>Ej")
			COD_EJEMP=replace (COD_EJEMP,"v","<BR>v")
		 	RESPONSE.Write COD_EJEMP %>
                </span></td>
              </tr>
            </table></td>
            <td class="bordeinf"><table width="100%" border="0" cellspacing="1" cellpadding="1">
                <tr>
                  <td width="1%" class="Estilo2">&nbsp;</td>
                  <%
        If rsetiqueta("Abreviatura_pre") <> "" Then
            codigo = rsetiqueta("Abreviatura_pre") & "/" & rsetiquetas("CodigoIngreso")
        Else
            codigo = rsetiqueta("CodigoIngreso")
        End If
%>
                  <td width="99%" class="Estilo2"><%=codigo%></td>
                </tr>
                <tr>
                  <td class="Estilo1">&nbsp;</td>
                  <td class="Estilo1"><%=LEFT(RsEtiqueta("titulo"),45)%></td>
                </tr>
                <tr>
                  <td class="Estilo1">&nbsp;</td>
                  <td class="Estilo1"><%=LEFT(RsEtiqueta("NombreAutor"),45)%></td>
                </tr>
                <tr>
                  <td class="Estilo2">&nbsp;</td>
                  <td class="Estilo2"><%rESPONSE.Write "USAT-" & RsEtiqueta("NumeroIngreso")%></td>
                </tr>
            </table></td>
          </tr>
          <tr>
            <td><table width="100%" border="0" cellspacing="1" cellpadding="1">
                <tr>
                  <td width="1%" class="Estilo2">&nbsp;</td>
                  <%
        If rsetiqueta("Abreviatura_pre") <> "" Then
            codigo = rsetiqueta("Abreviatura_pre") & "/" & rsetiquetas("CodigoIngreso")
        Else
            codigo = rsetiqueta("CodigoIngreso")
        End If
%>
                  <td width="99%" class="Estilo2"><%=codigo%></td>
                </tr>
                <tr>
                  <td class="Estilo1">&nbsp;</td>
                  <td class="Estilo1"><%=LEFT(RsEtiqueta("titulo"),45)%></td>
                </tr>
                <tr>
                  <td class="Estilo1">&nbsp;</td>
                  <td class="Estilo1"><%=LEFT(RsEtiqueta("NombreAutor"),45)%></td>
                </tr>
                <tr>
                  <td class="Estilo2">&nbsp;</td>
                  <td class="Estilo2"><%rESPONSE.Write "USAT-" & RsEtiqueta("NumeroIngreso")%></td>
                </tr>
            </table></td>
          </tr>
          <%SET RsEtiqueta=NOTHING%>
        </table></td>
	<%end if%>
	<%end if%>	
  </tr>
		<% if e mod 14 = 0 then%>
		<tr><td>&nbsp;<br>
		</td>
		</tr>
		<%end if%>

<%
RsEtiquetas.MoveNext
next%>

</table>
<%
end if
end if
%>

<%
if Request.QueryString("accion")="lista"then
	cadenaLista=Request.Form("ListaLibros")

''	Response.Write(cadenaLista)
	arr=split(cadenaLista,",")

%>				
<% 
ids=""
for i=lbound(arr) to ubound(arr)
	 CODIGO=trim(arr(i))
	 Set objProp=Server.CreateObject("Biblioteca.clsAccesoDatos")
	 objProp.AbrirConexion
	 
	 set RsEtiquetas=objProp.Consultar("ConsultarReportes","FO","14",CODIGO,CODIGO,0,0,0,0)
	if RsEtiquetas.eof then
		errores=errores+1
		if errores>0 then
			Response.Write("<script>alert("&errores&")</script>")
		end if
	else
		ids=ids & RsEtiquetas("idingreso") & ","
	end if 
	objProp.CerrarConexion
	set objProP=nothing
	set RsEtiquetas=Nothing
	''Response.Write codigo_etiqueta&" "&numero&"<br>"
next%>

<%ids=left(ids,len(ids)-1)
''Response.Write(ids)
arrLibros=split(ids,",")%>
<table width="100%" border="0" cellspacing="0" cellpadding="0" >
<%
''Response.Write(lbound(arrLibros))
counter=ubound(arrLibros)
Response.Write(counter)
for i=lbound(arrLibros) to counter
%>
<%Response.Write( arrLibros(i))%>

<%
next
%>


</table>
<%
end if
%>
</body>
</html>
