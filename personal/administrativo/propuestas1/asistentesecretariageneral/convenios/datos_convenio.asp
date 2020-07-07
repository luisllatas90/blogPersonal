<!--#include file="../../../../../funciones.asp"-->
<html>
<head>
<title>Documento sin t&iacute;tulo</title>

<style type="text/css">
<!--
body {
	background-color: #FFFFFF;
}
-->
</style>
<link href="../../../../../private/estilo.css" rel="stylesheet" type="text/css">

<style type="text/css">
<!--
.Estilo1 {
	font-size: 10pt;
	color: #395ACC;
}
.Estilo2 {
	color: #000000;
	font-weight: bold;
}
.Estilo3 {
	color: #395ACC;
	font-weight: bold;
}
-->
</style>
</head>


<p>
  <%if Request.QueryString("codigo_cni")<>"" then
	codigo_cni=Request.QueryString("codigo_cni")
	 Set objProp=Server.CreateObject("PryUSAT.clsAccesoDatos")
	 objProp.AbrirConexiontrans
	 set convenio=objProp.Consultar("ConsultarConvenios","FO","ES",codigo_cni,0,0)
	 objProp.CerrarConexiontrans
	 set objProP=nothing
	%>
<body topmargin="0" leftmargin="0" rightmargin="0">  
  	
</p>
<table width="100%" height="100%" border="0" align="center" cellpadding="0" cellspacing="0">
  <tr>
    <td colspan="3" align="left" valign="top"><table width="95%" border="0" align="center" cellpadding="0" cellspacing="1">
      <tr>
        <td width="3%" valign="top">&nbsp;</td>
        <td width="20%" valign="top">&nbsp;</td>
        <td width="1%" valign="top">&nbsp;</td>
        <td width="76%" valign="top">&nbsp;</td>
      </tr>
      <tr>
        <td colspan="4" align="center" valign="top"><div align="justify" class="Estilo1"><div align="center"><strong>
            <%response.write(convenio("denominacion_cni"))%>
        </strong></div>
        </div></td>
        </tr>
      <tr>
        <td colspan="4" align="center" valign="top">&nbsp;</td>
      </tr>
      <tr>
        <td colspan="4" valign="top"><span class="Estilo2">1.- Instituciones Participantes </span></td>
        </tr>
      <tr>
        <td valign="top">&nbsp;</td>
        <td valign="top">&nbsp;</td>
        <td valign="top">&nbsp;</td>
        <td valign="top">&nbsp;</td>
      </tr>
      <tr>
        <td valign="top">&nbsp;</td>
        <td colspan="3" valign="top">
		
		<%
	 Set objPART=Server.CreateObject("PryUSAT.clsAccesoDatos")
	 objPART.AbrirConexiontrans
	 set PARTICIPANTES=objPART.Consultar("consultarParticipantesConvenio","FO","ES",codigo_cni)
	 objPART.CerrarConexiontrans
	 set objPART=nothing		
		%>
		
		<table width="100%" border="0" align="center" cellpadding="1" cellspacing="0" class="contornotabla">
          <tr>
            <td colspan="2" align="center" bgcolor="#E1F1FB" class="bordederinf"><span class="Estilo3">Instituci&oacute;n</span></td>
            <td colspan="2" align="center" bgcolor="#E1F1FB" class="bordederinf"><span class="Estilo3">Firmante</span></td>
            </tr>
		  <%do while Not PARTICIPANTES.eof
		  i=i+1
		  %>
          <tr>		  
            <td width="2%" valign="top"><%response.write( i & ".-")%></td>
            <td width="57%" valign="top"><%response.write(ConvertirTitulo(PARTICIPANTES(0)))%></td>
            <td width="38%" valign="top">
			<%
			response.write(PARTICIPANTES(1))%>
			</td>
            <td width="3%" valign="top">
			<%if PARTICIPANTES(2)=1 then%>
				<img src="../../../../../images/menus/conforme_small.gif" width="16" height="16">
			<%end if%>			</td>			
          </tr>
		  <%PARTICIPANTES.MoveNext
		  loop%>
        </table>		</td>
        </tr>
      <tr>
        <td colspan="4" valign="top">&nbsp;</td>
      </tr>
      <tr>
        <td colspan="4" valign="top"><span class="Estilo2">2.- Datos Generales </span></td>
        </tr>
      <tr>
        <td colspan="4" valign="top">&nbsp;</td>
      </tr>
      <tr>
        <td valign="top">&nbsp;</td>
        <td valign="top"><strong>&Aacute;mbito</strong></td>
        <td valign="top">:</td>
        <td valign="top"><%response.write(convenio("Descripcion_Amc"))%></td>
      </tr>
      <tr>
        <td valign="top">&nbsp;</td>
        <td valign="top"><strong>Modalidad</strong></td>
        <td valign="top"><strong>: </strong></td>
        <td valign="top"><table width="100%" border="0" cellspacing="0" cellpadding="0">
          <tr>
            <td><%Response.write(convenio("descripcion_Mdc"))%>	</td>
          </tr>
        </table></td>
      </tr>
      <tr>
        <td valign="top">&nbsp;</td>
        <td valign="top"><strong>Objetivos</strong></td>
        <td valign="top"><strong>: </strong></td>
        <td valign="top"><%Response.write(convenio("objetivos_Cni"))%></td>
      </tr>
      <tr>
        <td valign="top">&nbsp;</td>
        <td valign="top"><strong>Fecha Inicio</strong></td>
        <td valign="top"><strong>: </strong></td>
        <td valign="top">
		<%response.write(convenio("fechaInicio_Cni"))%></td>
      </tr>
      <tr>
        <td valign="top">&nbsp;</td>
        <td valign="top"><strong>Duraci&oacute;n</strong></td>
        <td valign="top"><strong>: </strong></td>
        <td valign="top"><%
		if isnull(convenio("Duracion_Cni")) then
			Response.write("  -")
		else
			response.write(convenio("Duracion_Cni") & " " & convenio("periodoDuracion_Cni"))
		end if
			%></td>
      </tr>
      <tr>
        <td valign="top">&nbsp;</td>
        <td valign="top"><strong>Fecha Fin</strong></td>
        <td valign="top"><strong>: </strong></td>
        <td valign="top"><font color="#990000"><%
		if isnull(convenio("fechaFin_Cni")) then
			Response.Write("-")
		else
			response.write(convenio("fechaFin_Cni"))
				if Convenio("diferenciaDias") <>"" then
				if Convenio("diferenciaDias")<=0 then
					Response.Write("  (" & abs(Convenio("diferenciaDias")) & " días de caducado convenio") & ")"
				else
					Response.Write("  (" & abs(Convenio("diferenciaDias")) & " días para el fin del convenio") & ")"
				end if
			end if
		end if
		%></font></td>
      </tr>
      <tr>
        <td valign="top">&nbsp;</td>
        <td valign="top"><strong>Vigencia Indefinida</strong></td>
        <td valign="top"><strong>: </strong></td>
        <td valign="top">
          <% if convenio("vigencia_Cni") = 0 then
		  response.write("Si")
		  else
		  response.write("No")		  
		  end if
		  %>        </td>
      </tr>
      <tr>
        <td valign="top">&nbsp;</td>
        <td valign="top"><strong>Estado</strong></td>
        <td valign="top"><strong>: </strong></td>
        <td valign="top"><div align="justify">
          <% 
		  if (convenio("estado_Cni"))="V"then
		  	Response.write ("Vigente")
		  end if
		  if (convenio("estado_Cni"))="F"then
		  	Response.write ("Finalizado")
		  end if
		  if (convenio("estado_Cni"))="C"then
		  	Response.write ("Caducado")
		  end if		  		

		  %>
        </div></td>
      </tr>
      <tr>
        <td valign="top">&nbsp;</td>
        <td valign="top"><strong>Renovaci&oacute;n</strong></td>
        <td valign="top"><strong>: </strong></td>
        <td valign="top"><div align="justify">
          <%
		  if convenio("renovacion_Cni")=0 then
			  response.write("No")
		  else
			  response.write("Si")		  
		  end if
		  %>
        </div></td>
      </tr>
      <tr>
        <td valign="top">&nbsp;</td>
        <td valign="top"><strong>Observaci&oacute;n</strong></td>
        <td valign="top"><strong>:</strong></td>
        <td valign="top">
          <%
		  if isnull(convenio("observacion_Cni"))then
		   	Response.Write("-")
		  else
		  	response.write(convenio("observacion_Cni"))
		 end if%>        </td>
      </tr>
      <tr>
        <td valign="top">&nbsp;</td>
        <td valign="top"><strong>Archivo Adjunto </strong></td>
        <td valign="top"><strong>:</strong></td>
        <td valign="middle">
          <%
		  if isnull(convenio("archivoPdf_Cni"))  then
			Response.Write("-")
		  else
			Response.write(convenio("archivoPdf_Cni"))
		  %>
          <a  href="../../../../../convenios/<%=convenio("codigo_Cni")%>.pdf" target="_blank"><img src="../../../../../images/ext/pdf.gif" border="0" width="18" height="16"></a>          <a  href="../../../../../convenios/<%=convenio("codigo_Cni")%>.pdf" target="_blank"></a><a  href="../../../../../convenios/<%=convenio("codigo_Cni")%>.pdf" target="_blank"></a></td>
		  
        <%end if%>
      </tr>
      <tr>
        <td valign="top">&nbsp;</td>
        <td valign="top"><strong>N&uacute;mero de Copias </strong></td>
        <td valign="top"><strong>:</strong></td>
        <td valign="top">
          <%
		  if isnull(convenio("numCopias_Cni") ) then
				Response.write("0")
		  else
			  response.write(convenio("numCopias_Cni"))
		  end if%>        </td>
      </tr>
      <tr>
        <td valign="top">&nbsp;</td>
        <td valign="top"><strong>Resoluci&oacute;n</strong></td>
        <td valign="top"><strong>:</strong></td>
        <td valign="top">
          <% if IsNUll(convenio("descripcion_Rsu")) then
				Response.write("Sin Resolución")
		  else
			   	Response.write(convenio("descripcion_Rsu"))
		  end if
		  %>        </td>
      </tr>
      <tr>
        <td valign="top">&nbsp;</td>
        <td valign="top"><strong>Referencia</strong></td>
        <td valign="top"><strong>:</strong></td>
        <td valign="top">
          <%
		if IsNull(convenio("referencia_Cni")) then
		  	  Response.Write("-")
		else
			 Set objRef=Server.CreateObject("PryUSAT.clsAccesoDatos")
			 objRef.AbrirConexiontrans
			 set ConvRef=objRef.Consultar("ConsultarConvenios","FO","ES",convenio("referencia_Cni"),0,0)
			 objRef.CerrarConexiontrans
			 set objRef=nothing		  
			 Response.write(ConvRef(1))
	 	end if
	%>        </td>
      </tr>
      <tr>
        <td valign="top">&nbsp;</td>
        <td valign="top">&nbsp;</td>
        <td valign="top">&nbsp;</td>
        <td valign="top">&nbsp;</td>
      </tr>
      <tr>
        <td colspan="4" valign="top"><span class="Estilo2">3.- Responsable del Convenio</span></td>
        </tr>
      <tr>
        <td valign="top">&nbsp;</td>
        <td valign="top">
		<%
		 Set objResp=Server.CreateObject("PryUSAT.clsAccesoDatos")
		 objResp.AbrirConexiontrans
		 set Responsables=objResp.Consultar("ConsultarResponsablesConvenio","FO","ES",codigo_cni)
		 objResp.CerrarConexiontrans
		 set objResp=nothing		
		%></td>
        <td valign="top">&nbsp;</td>
        <td valign="top">&nbsp;</td>
      </tr>
      <tr>
        <td valign="top">&nbsp;</td>
        <td colspan="3" valign="top">
		<table width="100%" border="0" cellspacing="0" cellpadding="0">
          <%do while not Responsables.eof
		  e=e+1%>
		  
		  <tr>
            <td width="3%"><%Response.write("-")''Response.write(e) & ".- "%></td>
            <td width="97%"><%Response.Write(Responsables(0))%></td>
		  </tr>
        </table>
		<%
		Responsables.MoveNext
		loop%>
		</td>
        </tr>
      <tr>
        <td valign="top">&nbsp;</td>
        <td colspan="3" valign="top">&nbsp;</td>
      </tr>
    </table>    </td>
  </tr>
</table>
</body>
<%end if%>
</html>
