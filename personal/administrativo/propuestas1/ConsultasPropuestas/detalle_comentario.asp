<html>
<head>
<title>Documento sin t&iacute;tulo</title>
<link href="../../../../private/estilo.css" rel="stylesheet" type="text/css" />
<style type="text/css">
<!--
body {
	background-color: #FFFFFF;
}
-->
</style>
<link href="../../../../private/estilo.css" rel="stylesheet" type="text/css">

<script>
	function popUp(URL) {
	day = new Date();
	id = day.getTime();
	var izq = 300//(screen.width-ancho)/2
	//alert (izq)
   	var arriba= 200//(screen.height-alto)/2
	eval("page" + id + " = window.open(URL, '" + id + "', 'toolbar=NO,scrollbars=0,location=0,statusbar=0,status=0,menubar=0,resizable=1,width=600,height=360,left = "+ izq +",top = "+ arriba +"');");
	}
</script>
</head>



<%
	codigo_cop=Request.QueryString("codigo_cop")
	codigo_prp=Request.QueryString("codigo_prp")
	leido=Request.QueryString("leido")	
	
//	codigo_ipr=Request.QueryString("codigo_ipr")
//	coment1=Request.QueryString("coment1")
%>
	<%
	//if Request.QueryString("codigo_prp")<>"" then
	codigo_prp=Request.QueryString("codigo_prp")
	codigo_cop=Request.QueryString("codigo_cop")	
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
	objCom.AbrirConexion
	set comentario1=objCom.Consultar("ConsultarComentarios","FO","PC",codigo_prp,0)
	objCom.CerrarConexion
	set objCom=nothing
	coment1=comentario1(0)
	set comentario1 = nothing
	%>
<body topmargin="0" leftmargin="0" rightmargin="0"> 
	<%
if Request.QueryString("codigo_cop")<>"" then

if leido=0 then
	//insertar un registro en estados propuesta para que ese comentario cambie a estado leído
 					Set objLeido=Server.CreateObject("PryUSAT.clsAccesoDatos")
					objLeido.AbrirConexion
					objLeido.Ejecutar "RegistraEstadoPropuesta",false,codigo_prp,codigo_cop,"C",codigo_per
					objLeido.CerrarConexion
					set objLeido=Nothing
//	response.write(leido)
end if


	%> 
  	<table width="100%" height="100%" border="0" align="center" cellpadding="2" cellspacing="0" class="contornotabla">
  <tr>
    <td height="38" colspan="2" align="left" valign="middle" bgcolor="#F0F0F0" class="bordeinf">
	<input name="Submit" type="submit" class="nuevocomentario" value="        Nuevo" onClick="popUp('registracomentario.asp?codigo_prp=<%=codigo_prp%>&codigo_cop=<%=codigo_cop%>&coment1=<%=coment1%>&codigo_ipr=<%=codigo_ipr%>')">
    <input name="Submit2" type="submit" class="respondercomentario" value="      Responder" onClick="popUp('registracomentario.asp?codigo_prp=<%=codigo_prp%>&codigo_cop=<%=codigo_cop%>&coment1=<%=coment1%>&codigo_ipr=<%=codigo_ipr%>&respuesta=1')"></td>
  </tr>
  <tr height="20">
    <td width="18%" height="22" align="center" valign="top" bgcolor="#F0F0F0" class="bordeinf"><img src="../../../../images/menus/attachfiles.gif" width="35" height="35"></td>
    <td width="82%" align="left" valign="top" bgcolor="#F0F0F0" class="bordeinf">
 <table width="95%" border="0" align="center" cellpadding="0" cellspacing="0">
              <tr>
                <td align="left">&nbsp;</td>
                <td align="left">&nbsp;</td>
              </tr>
<%
					
					Set objArchivo=Server.CreateObject("PryUSAT.clsAccesoDatos")
					objArchivo.AbrirConexiontrans
					set rsArchivosPrp=objArchivo.Consultar("ConsultarArchivosPropuesta","FO","TO",codigo_cop)
					objArchivo.CerrarConexiontrans
					set objArchivo=nothing
					if rsArchivosPrp.Eof=true then
					Response.Write("No contiene datos adjuntos.")
					end if					
			  		do while not rsArchivosPrp.eof
			  %>              
			  <tr>
                <td width="8%" align="left">
			
									
			<a href="../../../../filespropuestas/<%=codigo_prp%>/<%=rsArchivosPrp("nombre_apr")%>" target="_blank">
				<img src="../../../../images/ext/<%=right(rsArchivosPrp(2),3)%>.gif" width="16" height="16"  border="0">
				</a>				</td>
                <td width="92%" align="left"><%response.write(rsArchivosPrp(3))
				%>

				</td>
              </tr>
              <%
			  	rsArchivosPrp.movenext()
				loop						
			  %>
			  <tr>
                <td align="left">&nbsp;</td>
                <td align="center">&nbsp;</td>
              </tr>
      </table>		
	
	</td>
  </tr>
  
  <tr>
					<%
 					Set objCom=Server.CreateObject("PryUSAT.clsAccesoDatos")
					objCom.AbrirConexiontrans
					set RsComentario=objCom.Consultar("ConsultarComentarios","FO","DE",codigo_cop,0)
					objCom.CerrarConexiontrans
					set objCom=nothing
				//	Response.Write(codigo_cop + " " +  codigo_prp)%>
    <td colspan="2" align="left" valign="top"><table width="100%" border="0" cellspacing="0" cellpadding="0">
      <tr>
        <td><%Response.Write(RsComentario("comentario_cop"))%></td>
      </tr>
      <tr>
        <td>&nbsp;</td>
      </tr>
      <tr>
        <td>&nbsp;</td>
      </tr>
    </table></td>
  </tr>
</table>
<%else%>
<table width="100%" height="100%" border="0" align="center" cellpadding="4" cellspacing="0" class="contornotabla">
  <tr>
    <td width="100%" height="38" colspan="2" align="left" valign="middle" bgcolor="#F0F0F0" class="bordeinf">
	<input name="Submit" type="submit" class="nuevocomentario" value="        Nuevo"  onClick="popUp('registracomentario.asp?codigo_prp=<%=codigo_prp%>&codigo_cop=<%=codigo_cop%>&coment1=<%=coment1%>&codigo_ipr=<%=codigo_ipr%>')"></td>
  </tr>
  <tr>
    <td colspan="2" align="left" valign="top">
		<table width="100%" border="0" cellspacing="0" cellpadding="0">
		  <tr>
			<td>&nbsp;</td>
		  </tr>
		  <tr>
			<td>&nbsp;</td>
		  </tr>
		  <tr>
			<td>&nbsp;</td>
		  </tr>
		</table>
	</td>
  </tr>
</table>
<%End if%>
</body>

</html>

