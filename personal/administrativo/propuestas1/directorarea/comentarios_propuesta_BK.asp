<html>
<head>
<title>Documento sin t&iacute;tulo</title>
<link href="../../../../private/estilo.css" rel="stylesheet" type="text/css" />
<strong><script language="JavaScript" src="../../../../private/funciones.js"></script></strong>
<style type="text/css">
<!--
body {
	background-color: #FFFFFF;
}
-->
</style>
<STYLE type="text/css">
BODY {
scrollbar-face-color:#A6D6FF;
scrollbar-highlight-color:#FFFFFF;
scrollbar-3dlight-color:#FFFFFF;
scrollbar-darkshadow-color:#FFFFFF;
scrollbar-shadow-color:#FFFFFF;
scrollbar-arrow-color:#000000;

scrollbar-track-color:#FFFFFF;
}
a:link {text-decoration: none; color: #00080; }
a:visited {text-decoration: none; color: #000080; }
a:hover {text-decoration: none; black; }
a:hover{color: black; text-decoration: none; }
</STYLE>
<script>
function SeleccionarFila1()
{

	oRow = window.event.srcElement.parentElement;

	if (oRow.tagName == "TR"){
		AnteriorFila.Typ = "Sel";
		AnteriorFila.className = AnteriorFila.Typ + "Off";
		AnteriorFila = oRow;
	}
	//if (oRow.Typ == "Sel"){
		oRow.Typ ="Selected";
		oRow.className = oRow.Typ;
	//}
}	
function LeerComentario(codigo_cop,codigo_prp,codigo_ipr,coment1,leido,codigo_per){

location.href='comentarios_propuesta.asp?codigo_cop='+codigo_cop+'&codigo_prp='+codigo_prp+'&codigo_ipr='+codigo_ipr+'&coment1='+coment1+'&leido='+leido+'&codigo_per='+codigo_per

//fradetallecomentario.location='detalle_comentario.asp?codigo_cop='+codigo_cop+'&codigo_prp='+codigo_prp+'&codigo_ipr='+codigo_ipr+'&coment1='+coment1+'&leido='+leido+'&codigo_per='+codigo_per

//document.frames['fradetallecomentario'].location='detalle_comentario.asp?codigo_cop='+codigo_cop+'&codigo_prp='+codigo_prp+'&codigo_ipr='+codigo_ipr+'&coment1='+coment1+'&leido='+leido+'&codigo_per='+codigo_per

}

</script>

</head>
<p>

	<%
	if Request.QueryString("codigo_prp")<>"" then
	codigo_prp=Request.QueryString("codigo_prp")
	codigo_per=session("codigo_Usu")
	Set objInv=Server.CreateObject("PryUSAT.clsAccesoDatos")
	objInv.AbrirConexion
	set involucrado=objInv.Consultar("ConsultarInvolucradoPropuesta","FO","JE",codigo_per,codigo_prp)
	codigo_ipr=involucrado(0)
	''Response.write(codigo_inv)
	objInv.CerrarConexion
	set objInv=nothing
	set involucrado = nothing	

	Set objCom=Server.CreateObject("PryUSAT.clsAccesoDatos")
	objCom.AbrirConexiontrans
		set comentario1=objCom.Consultar("ConsultarComentarios","FO","PI",codigo_prp,0)
	objCom.CerrarConexiontrans 
	set objCom=nothing
	''Response.Write(comentario1.RecordCount)
	if comentario1.EOF then
	else
	
	coment1=comentario1("codigo_cop")
	set comentario1 = nothing
	%>


	</p>
<body topmargin="0" leftmargin="0" rightmargin="0">  
  	<form id="frmcomentario" name="frmcomentario">


<table width="100%" height="100%" border="0" align="center" cellpadding="0" cellspacing="0">
  <tr>
    <td align="left" valign="top">
	
	<table width="100%" height="100%" border="0" cellpadding="0" cellspacing="3">

      <tr height="17">
        <td width="57%" align="center" valign="top">&nbsp;</td>
        <td width="1%" align="center">&nbsp;</td>
        <td width="42%" align="center">&nbsp;</td>
      </tr>
      <tr>
        <td align="center" valign="top">
		
		<table width="100%" height="100%" border="0" cellpadding="0" cellspacing="0">
                      <tr>
                        <td valign="top">
								<%codigo_cop=Request.QueryString("codigo_cop")
		leido=Request.QueryString("leido")
		if cint(leido)=0 and codigo_cop<>"" then
	//insertar un registro en estados propuesta para que ese comentario cambie a estado leído
 					Set objLeido=Server.CreateObject("PryUSAT.clsAccesoDatos")
					objLeido.AbrirConexion
						objLeido.Ejecutar "RegistraEstadoPropuesta",false,codigo_prp,codigo_cop,"C",codigo_per
					objLeido.CerrarConexion
					set objLeido=Nothing

//	response.write(leido)
	end if
		%>
					<%
		
					CargarComentarios coment1
					
					%></td>
                      </tr>
                      <tr>
                        <td valign="top"></td>
                      </tr>
        </table>		  
		</td>
        <td align="center" valign="top">&nbsp;</td>
        <td align="center" valign="top">

		
		<iframe name="fraDetalleComentario" id="fradetallecomentario"  height="100%" width="100%" frameborder="0" scrolling="no" src="detalle_comentario.asp?codigo_cop=<%=codigo_cop%>&codigo_prp=<%=codigo_prp%>&codigo_ipr=<%=codigo_ipr%>&coment1=<%=coment1%>&leido=<%=leido%>&codigo_per=<%=codigo_per%>">		</iframe></td>
      </tr>
    </table>
	</td>
  </tr>
</table>
<%end if

end if%>
</form>
</body>

</html>


<%

Public Sub CargarComentarios(byval codigorespuesta_cop)
dim objCom1  
dim RsComentarios
 //consulta mi recordset
 		Set objCom1=Server.CreateObject("PryUSAT.clsAccesoDatos")
		objCom1.AbrirConexiontrans
		set RsComentarios=objCom1.Consultar("ConsultarComentarios","FO","TO",codigorespuesta_cop,0)
		objCom1.CerrarConexiontrans
		set objCom1=nothing%>
		<table  width="97%" align="right" cellspacing="0" border="0" cellpadding="0">
		<input type="hidden" id="txtelegido">
		<%
		do while not  RsComentarios.eof
		o=o+1
		%>
		<tr><td>
		<%
			if RsComentarios("HIJOS")>0 then
				//response.write(RsComentarios("asunto_cop"))
				//response.write("<br>")
				//inicio 
				  ''response.write(RsComentarios("codigo_cop"))
 					Set objLeido=Server.CreateObject("PryUSAT.clsAccesoDatos")
					objLeido.AbrirConexion
					set RsLeido=objLeido.Consultar("ConsultarComentarios","FO","CP",RsComentarios(0),codigo_per)
					objLeido.CerrarConexion
					set objLeido=nothing
					if   RsLeido.BOF=true then
						leido=0
					else
						leido=1							
					end if
				%>
			  <table cellspacing="1" width="100%" <%if   RsLeido.BOF=true then %> bgcolor="#FFFFD9" <% end if%> width="100%"  border="0" cellpadding="0" cellspacing="0" class="contornotabla" id="fila<%=RsComentarios(0)%>"  <%if cint(RsComentarios(0))=cint(codigo_cop) then%>bgcolor="#0099FF" <%end if%> onClick="LeerComentario(<%=RsComentarios(0)%>,<%=codigo_prp%>,<%=codigo_ipr%>,<%=coment1%>,<%=leido%>,<%=codigo_per%>)" style="cursor:hand"> 
    <tr>
      <td width="75%" align="left" valign="middle"><strong>De: </strong><%response.write(RsComentarios("nombre"))%></td>
      <td width="25%" align="center" valign="middle">	 
	  	  <%
					if   RsLeido.BOF=true then%>

					<img src="../../../../images/menus/rpta.GIF">					
					<font color="#395ACC"> <strong>No le&iacute;do </strong></font>
					<%else%>
					&nbsp;
					<%end if%>	
			  
	  </td>
    </tr>
    <tr>
      <td colspan="2" align="left"><strong>Asunto: </strong><%response.write(RsComentarios("asunto_cop"))%></td>
      </tr>
    <tr>
      <td colspan="2" align="left"><strong>Enviado el: </strong>
        <%response.write(RsComentarios("fecha_cop"))%></td>
		<TD>

								
		</TD>		
      </tr>
  </table>
				<%//fin				
				call CargarComentarios(RsComentarios("codigo_cop"))				
			else			
			//inicio
					''response.write(RsComentarios(0))
 					Set objLeido=Server.CreateObject("PryUSAT.clsAccesoDatos")
					objLeido.AbrirConexion
					set RsLeido=objLeido.Consultar("ConsultarComentarios","FO","CP",RsComentarios(0),codigo_per)
					objLeido.CerrarConexion
					if   RsLeido.BOF=true then
						leido=0
					else
						leido=1	
					end if					
			%>
			  <table cellspacing="1" width="100%" <%if   RsLeido.BOF=true then %> bgcolor="#FFFFD9" <% end if%> width="100%"  border="0" cellpadding="0" cellspacing="0" class="contornotabla" id="fila<%=RsComentarios(0)%>"  <%if cint(RsComentarios(0))=cint(codigo_cop) then%>bgcolor="#0099FF" <%end if%> onClick="LeerComentario(<%=RsComentarios(0)%>,<%=codigo_prp%>,<%=codigo_ipr%>,<%=coment1%>,<%=leido%>,<%=codigo_per%>)" style="cursor:hand"> 

    <tr>
      <td   width="75%" align="left" valign="middle"><strong>De: </strong><%response.write(RsComentarios("nombre"))%></td>
      <td width="25%" align="center" valign="middle">	  
	  	  	  <% 
					set objLeido=nothing
					if   RsLeido.BOF=true then%>
					<img src="../../../../images/menus/rpta.GIF"><font color="#395ACC"> <strong>No le&iacute;do </strong></font>
					<%else%>
					&nbsp;
					<%end if%>	
	  </td>
    </tr>
    <tr>
      <td colspan="2" align="left" >
	  <strong>Asunto: </strong><%response.write(RsComentarios("asunto_cop"))%></td>
      </tr>
    <tr>
      <td colspan="2" align="left" ><strong>Enviado el: </strong>
        <%response.write(RsComentarios("fecha_cop"))%></td>
<TD>
		<%''if RsComentarios("nivelrestriccion_cop")="A" then%>
		<font color="#395ACC"> <strong><%''Response.Write("Inf.")%></strong></font>
		<%''end if%>
						
</TD>
      </tr>    
    <tr>
    </tr>
  </table>		
			
<%//		fin
//		response.write(RsComentarios("asunto_cop"))
	//			response.write("<br>")
			end if				
			RsComentarios.movenext
		%>					
		</td><tr>		
		<%
		loop %>
	</table>
		<%
		set RsComentarios = nothing	
  end Sub
%>


