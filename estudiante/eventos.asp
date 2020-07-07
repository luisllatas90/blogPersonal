<%@LANGUAGE="VBSCRIPT" CODEPAGE="1252"%>
<%IF session("codigo_usu")<> 13379 then%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />
<title>Documento sin t&iacute;tulo</title>
<link href="../private/estilo.css" rel="stylesheet" type="text/css" />
<style type="text/css">
<!--
.Estilo2 {
	font-size: 12pt;
	font-family: Arial, Helvetica, sans-serif;
	color: #FFFFFF;
	font-weight: bold;
}
.Estilo3 {
	color: #FF6600;
	font-weight: bold;
}
-->
</style>
<script>
	function AbrirMenu(pagina)
	{ 	
		if (pagina!='about:blank' && pagina!=""){
			if (pagina.indexOf('?')==-1){//Si no encuentra una referencia
				pagina=pagina + "?id=<%=session("codigo_usu")%>"
			}
			else{
				pagina=pagina + "&id=<%=session("codigo_usu")%>"
			}
	
			MarcarMenu()
			top.parent.frames[2].location.href="cargando.asp?pagina=" + pagina
		}
	}
		function ExpandirNodo(item)
	{
		var idMenu=0;
		var img=""
		if (item.length==undefined){
			idMenu=item.id
			img=document.getElementById("imgCarpeta" + idMenu.substring(3,idMenu.length));
			MostrarMenu(item,img)
        }
		else{
			idMenu=item[0].id
			img=document.getElementById("imgCarpeta" + idMenu.substring(3,idMenu.length));
	
			for(i=0;i<item.length;i++){
				MostrarMenu(item[i],img)
	     	}
		}
	}
	
	function MostrarMenu(Hijo,img)
	{
		if (Hijo.style.display!="none"){
			Hijo.style.display="none"
			img.src="../images/mas2.gif"
		}
		else{
			Hijo.style.display=""
			img.src="../images/menos2.gif"
        }
	}
	
	function MarcarMenu()
	{
		oRow = window.event.srcElement.parentElement;
			
		if (oRow.tagName == "TR"){
			AnteriorFila.Typ = "Sel";
			AnteriorFila.className = ""
			AnteriorFila = oRow;
		}
		if (oRow.Typ == "Sel"){
			oRow.Typ ="Selected";
			oRow.className = oRow.Typ;
		}
	}
</script>
<script type="text/javascript" language="JavaScript" src="../private/funciones.js"></script>

</head>

<body>
<%'response.write(session("codigo_alu")) %>
<%'if session("estadodeuda_alu")=0 then%>

<table width="100%" height="500" border="0" cellpadding="0" cellspacing="0" class="contornotabla">
  <tr>
    <td valign="top" bgcolor="#F8F8F8"><table width="100%" border="0" cellspacing="0" cellpadding="0">
      <tr>
        <td width="100%" height="35" align="right" bgcolor="#003366" class="Estilo2">Eventos USAT&nbsp;&nbsp;&nbsp; </td>
      </tr>
      <tr >
        <td height="2"></td>
      </tr>
      <tr >
        <td height="21" align="center" bgcolor="#FFFFCC" class="bordeinf"><span class="Estilo3">CLIC en el evento para INSCRIBIRTE </span></td>
      </tr>     
      <tr>
        <td height="12" align="center">
<%
Set obj=Server.CreateObject("PryUSAT.clsAccesoDatos")
	obj.AbrirConexion
   
	 codigoCpf  = session("codigo_cpf")
     Set objEnc=server.CreateObject("EncriptaCodigos.clsEncripta")
     
	 enc=objEnc.Codifica("069" & session("codigouniver_alu"))
	 enc=objEnc.Codifica("069" & enc)
	'Para Programas especiale les muestre el mensaje
	if codigoCpf = 25 then
	    codigoCpf =3
	end if
	Set rsAvisos= Obj.Consultar("CV_ConsultarAvisoCampus","FO","V","E",codigoCpf)
%>		
		<table width="100%" border="0" cellspacing="1" cellpadding="1">		  
		  <%for i = 0 to rsAvisos.RecordCount-1%>
		  <tr>
            <td>&nbsp;</td>
          </tr>
          <%if rsAvisos("descripcion_avc") ="Participar en fiesta de gala" then %>
              <%if (session("codigo_test")=2  or session("codigo_test")=3  ) then%>
              <tr>
                <td>                                     
			    <a href="<%=rsAvisos("vermas_avc")%>&ctm=<%=enc%>" target="<%=rsAvisos("target")%>">
			    <!--<img border="1" style="border-color:#000000;cursor:hand" src="<%=rsAvisos("img_avc")%>" alt="<%=rsAvisos("descripcion_avc")%>" onClick="AbrirMenu('<%=rsAvisos("vermas_avc")%>')" >-->
			    <img border="1" style="border-color:#000000;cursor:hand" src="<%=rsAvisos("img_avc")%>" alt="<%=rsAvisos("descripcion_avc")%>">
			    </a>						
			    </td>			
              </tr>
              <%end if %>
          <%else %>
          <% if session("codigo_test")<>20 then %>
          <tr>          
            <td>                                     
			<a href="<%=rsAvisos("vermas_avc")%>&ctm=<%=enc%>" target="<%=rsAvisos("target")%>">
			<!--<img border="1" style="border-color:#000000;cursor:hand" src="<%=rsAvisos("img_avc")%>" alt="<%=rsAvisos("descripcion_avc")%>" onClick="AbrirMenu('<%=rsAvisos("vermas_avc")%>')" >-->
			<img border="1" style="border-color:#000000;cursor:hand" src="<%=rsAvisos("img_avc")%>" alt="<%=rsAvisos("descripcion_avc")%>">
			</a>						
			</td>			
          </tr>  
          <%end if %>        
          <% end if%>
		  <%
		  rsAvisos.movenext
		  next
		  %>
        </table>
		
		</td>
      </tr>
 <%'if  session("codigo_cpf")=2 then%>
<%'end if %>
	        <%if session("codigo_cpf")=11 or session("codigo_cpf")=24 or session("codigo_cpf")=27 or session("codigo_cpf")=31 then %>
      <%end if %> 
    </table></td>
  </tr>
</table>
<%
obj.CerrarConexion
'end if%>

<%

%>
</body>
</html>
<%end if%>