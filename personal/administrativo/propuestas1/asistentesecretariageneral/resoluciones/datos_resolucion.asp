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
.Estilo3 {color: #000000}
-->
</style>
</head>


<p>
  <%if Request.QueryString("codigo_cni")<>"" then
	codigo_cni=Request.QueryString("codigo_cni")
	 Set objProp=Server.CreateObject("PryUSAT.clsAccesoDatos")
	 objProp.AbrirConexion
	 set convenio=objProp.Consultar("RS_ConsultarResoluciones","FO","ES",Request.QueryString("Codigo_cni"),"")
	 objProp.CerrarConexion
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
            <%response.write(convenio("descripcion_rsu"))%>
        </strong></div>
        </div></td>
        </tr>
      <tr>
        <td colspan="4" align="center" valign="top">&nbsp;</td>
      </tr>
      <tr>
        <td colspan="4" valign="top"><span class="Estilo2">1.- Datos Generales</span> </td>
        </tr>
      <tr>
        <td colspan="4" valign="top">&nbsp;</td>
      </tr>
      <tr>
        <td valign="top">&nbsp;</td>
        <td valign="top"><span class="Estilo2">N&uacute;mero</span></td>
        <td valign="top"><span class="Estilo2">:</span></td>
        <td valign="top"><strong>
          <%response.write(convenio("descripcion_rsu"))%>
        </strong></td>
      </tr>
      <tr>
        <td valign="top">&nbsp;</td>
        <td valign="top"><span class="Estilo3"><strong>Tipo</strong></span></td>
        <td valign="top"><span class="Estilo3"><strong>: </strong></span></td>
        <td valign="top"><table width="100%" border="0" cellspacing="0" cellpadding="0">
          <tr>
            <td class="Estilo3"><%Response.write(convenio("descripcion_tru"))%>	</td>
          </tr>
        </table></td>
      </tr>
      <tr>
        <td valign="top">&nbsp;</td>
        <td valign="top"><span class="Estilo3"><strong>Fecha</strong></span></td>
        <td valign="top"><span class="Estilo3"><strong>: </strong></span></td>
        <td valign="top">
		  <span class="Estilo3">
		  <%response.write(convenio("fecha_rsu"))%>
	    </span></td>
      </tr>
      <tr>
        <td valign="top">&nbsp;</td>
        <td valign="top"><span class="Estilo3"><strong>Observaci&oacute;n</strong></span></td>
        <td valign="top"><span class="Estilo3"><strong>: </strong></span></td>
        <td valign="top"><span class="Estilo3">
          <%response.write(convenio("asunto_rsu"))%>
        </span></td>
      </tr>
      <tr>
        <td valign="top">&nbsp;</td>
        <td valign="top"><span class="Estilo3"><strong>Fecha de Registro </strong></span></td>
        <td valign="top"><span class="Estilo3"><strong>: </strong></span></td>
        <td valign="top"><span class="Estilo3">
          <%response.write(convenio("fechaReg"))%>
        </span></td>
      </tr>
      <tr>
        <td valign="top">&nbsp;</td>
        <td valign="top"><span class="Estilo3"></span></td>
        <td valign="top"><span class="Estilo3"></span></td>
        <td valign="top"><span class="Estilo3"></span></td>
      </tr>
      <tr>
        <td valign="top">&nbsp;</td>
        <td valign="top"><span class="Estilo3"><strong>Archivo Adjunto </strong></span></td>
        <td valign="top"><span class="Estilo3"><strong>:</strong></span></td>
		
        <td valign="middle">
		<% if convenio("archivoPdf_rsu") ="" then%>
		<span class="Estilo3"><a  href="../../../../../resoluciones/<%=convenio("archivoPdf_rsu")%>.pdf" target="_blank"><img src="../../../../../images/ext/pdf.gif" border="0" width="18" height="16"></a> </span>
		<%else%>
		<span class="Estilo3">-</span>

		<%end if%>		</td>
      </tr>
      <tr>
        <td valign="top">&nbsp;</td>
        <td valign="top"><span class="Estilo3"><strong>Propuesta</strong></span></td>
        <td valign="top"><span class="Estilo3"><strong>:</strong></span></td>
        <td valign="middle">
		<% 
		if isnull(convenio("Nombre_prp")) then
			Response.write(trim(convenio("Nombre_prp")))
		else
			Response.Write("-")
		end if
		%>        </td>
      </tr>
      <tr>
        <td valign="top">&nbsp;</td>
        <td valign="top">&nbsp;</td>
        <td valign="top">&nbsp;</td>
        <td valign="middle">&nbsp;</td>
      </tr>
      <tr>
        <td valign="top">&nbsp;</td>
        <td valign="top">&nbsp;</td>
        <td valign="top">&nbsp;</td>
        <td valign="middle">&nbsp;</td>
      </tr>
	  	<%if isnull(convenio("Nombre_prp")) then

		else
			Set objProp=Server.CreateObject("PryUSAT.clsAccesoDatos")
			objProp.AbrirConexion
				set Acuerdos=objProp.Consultar("RS_ConsultarAcuerdosResolucion","FO","ES",codigo_cni,0,0)
			objProp.CerrarConexion
			set objProP=nothing		
		%>
      <tr>
        <td colspan="4" valign="top"><span class="Estilo2">2.- Acuerdos</span></td>
      </tr>
	  
      <tr>
        <td valign="top">&nbsp;</td>
        <td valign="top">&nbsp;</td>
        <td valign="top">&nbsp;</td>
        <td valign="middle">&nbsp;</td>
      </tr>

      <tr>
        <td valign="top">&nbsp;</td>
        <td colspan="3" valign="top"><table width="80%" border="0" cellspacing="0" cellpadding="0">
          <tr>
            <td width="5%"><strong>N&ordm;</strong></td>
            <td width="95%"><strong>Acuerdo</strong></td>
          </tr>
         <%do while not Acuerdos.Eof
		 i=i+1
		 %>
		  <tr>
            <td><%Response.Write(i)%></td>
            <td><%Response.Write(Acuerdos("descripcion_apr"))%></td>
          </tr>
		  <%		  		 
	 	 Acuerdos.MoveNext
		 loop		 
		  %>
        </table></td>
      </tr>
	  	  <%end if%>
      <tr>
        <td valign="top">&nbsp;</td>
        <td valign="top">&nbsp;</td>
        <td valign="top">&nbsp;</td>
        <td valign="middle">&nbsp;</td>
      </tr>
      
</table>
</body>
<%end if%>
</html>
