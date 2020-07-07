
<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />
<%Int_Codigo=Request.QueryString("Codigo")

	 Set objProp=Server.CreateObject("Biblioteca.clsAccesoDatos")
	 objProp.AbrirConexion
	 set RsEtiquetas=objProp.Consultar("ConsultarReportes","FO","14",Int_Codigo,Int_Codigo,0,0,0,0)
if 	 RsEtiquetas.eof then
	Response.Write("Etiqueta no encontrada")
else	
	 objProp.CerrarConexion
	 set objProP=nothing
	 Int_Codigo2=RsEtiquetas("IDINGRESO")


%>
<title>Etiqueta: <%Request.QueryString(Int_Codigo)%></title>
<style type="text/css">
<!--
.Estilo1 {color: #000000}
.Estilo2 {color: #000000; font-weight: bold; }
.Estilo4 {color: #000000; font-weight: bold; font-size: 16px; }
-->
</style>
<link href="../../../private/estilo.css" rel="stylesheet" type="text/css" />
<style type="text/css">
<!--
.Estilo5 {
	font-size: 14pt;
	color: #006699;
}
-->
</style>
<link href="../../../../private/estilo.css" rel="stylesheet" type="text/css">
</head>

<body topmargin="5">
<p align="center" class="Estilo5">Etiqueta del ejemplar: <%Response.Write(Int_Codigo)%></p>
<table width="100%" border="0" align="center" cellpadding="2" cellspacing="2" class="contornotabla">
  <%  
  	 Set objProp=Server.CreateObject("Biblioteca.clsAccesoDatos")
	 objProp.AbrirConexion
	 	set RsEtiqueta=objProp.Consultar("ConsultarReportes","FO","18",Int_Codigo2,0,0,0,0,0)
	 objProp.CerrarConexion
	 set objProP=nothing
	 
 
	
	 if RsEtiqueta.RecordCount<>0 then
	 %>
  <tr>
    <td width="25%" rowspan="2" align="center" class="bordeder Estilo1"><table width="80%" border="0" cellspacing="0" cellpadding="0">
      <tr>
        <td align="center"><span class="Estilo4">
          <%

		  			PARTE=trim(mid(RsEtiqueta("CodigoIngreso"),5,3))
					if  isNumeric(PARTE)=true then
						bajar="SI"
					else
						bajar="NO"
					end if
					
					COD_EJEMP =RsEtiqueta("CodigoIngreso")
					if bajar="SI" then
						COD_EJEMP=replace (COD_EJEMP,"."&PARTE,"<br>"&PARTE)
					end if

			COD_EJEMP=replace (COD_EJEMP,"/", "<br>")
			COD_EJEMP=replace (COD_EJEMP,"T.", "<br>T.")
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
            codigo = rsetiqueta("Abreviatura_pre") & "/" & Int_Codigo
        Else
            codigo = Int_Codigo
        End If
%>
        <td width="99%" class="Estilo2"><%=RsEtiqueta("CodigoIngreso")%></td>
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
            codigo = Int_Codigo
        End If
%>
        <td width="99%" class="Estilo2"><%=RsEtiqueta("CodigoIngreso")%></td>
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
  <%SET RsEtiqueta=NOTHING
  end if
 end if%>
</table>
</body>

</html>
