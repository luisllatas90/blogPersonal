<!--#include file="../../../../funciones.asp"-->
<html>
<head>
<title>Documento sin t&iacute;tulo</title>
<link href="../../../../private/estilo.css" rel="stylesheet" type="text/css" />
<style type="text/css">
<!--
body {
	background-color: #F0F0F0;
}

.Estilo1 {
	font-size: 10pt;
	font-weight: bold;
}
-->
</style>
<style type="text/css">
<!--
a:link {
	text-decoration: none;
}
a:visited {
	text-decoration: none;
}
a:hover {
	text-decoration: none;
}
a:active {
	text-decoration: none;
}
-->
</style>
<style type="text/css" >
#textovertical {
writing-mode: tb-rl;
filter: flipv fliph;
} 
</style>
<script language="JavaScript" src="../../../../private/funciones.js"></script>
<script language="JavaScript" src="../../../../private/sorttable.js"></script>
<script language="JavaScript" src="../../../../private/tooltip.js"></script>

<script>

function AbrirAplicacion()
{
   	top.location.href="abriraplicacion.asp?codigo_tfu=" + ctfu + "&codigo_apl=" + c_apl + "&descripcion_apl=" + d_apl + "&estilo_apl=" + e_apl + "&descripcion_tfu=" + dtfu}

function AbrirMenu(){
showModalDialog("../../../abrirfuncionVolver.asp?codigo_apl=7&descripcion_apl=SistemaPropuestas&estilo_apl=P",window,"dialogWidth:400px;dialogHeight:250px;status:no;help:no;center:yes")
}
	function AbrirPropuesta(pagina)
	{
		var codigo_prp=document.all.txtelegido.value
		
		if (txtelegido.value!="" || txtelegido.value!=0)
			{fradetalle.location.href=pagina + "?codigo_prp=" + codigo_prp.substring(4,codigo_prp.length)}
	}
	
	function AbrirPagina(pagina)
	{
		var codigo_prp=document.all.txtelegido.value
		
		if (txtelegido.value!="" || txtelegido.value!=0)
			{
			AbrirPopup(pagina + "?codigo_prp=" + codigo_prp.substring(4,codigo_prp.length))
			}
	}	
	function activamodifica(instancia){
	//alert (instancia)
	if (instancia=='P'){
		cmdmodificar.disabled=false
		}
	}
	function ActivaBotones(instancia,veredicto,pendiente){
	//alert (instancia)
	//alert (veredicto)
	//alert (pendiente)
		if (pendiente!='A'){
			if ((veredicto=='P') | (veredicto=='O')){
			document.all.cmdconforme.disabled=false
		//	document.all.cmdnoconforme.disabled=false
			document.all.cmdobservar.disabled=false						
			}
			
		}
	}
	function darveredicto(veredicto,menu,instancia,estado,informe, inf){
	var codigo_prp=document.all.txtelegido.value
	
	var veredicto1=veredicto

	if (veredicto=="C"){
		veredicto1="CONFORME"
	}
	if (veredicto=="N"){
		veredicto1="NO CONFORME"
	}	
	if (veredicto=="O"){
		veredicto1="OBSERVADO"
	}
	calificar=confirm ("¿Desea calificar la propuesta como " + veredicto1 + "?")
		if (calificar==true){

			frmpropuesta.action="procesar.asp?accion=enviar&veredicto="+veredicto+"&codigo_prp="+codigo_prp.substring(4,codigo_prp.length)+"&menu="+menu+"&instancia="+instancia+"&estado="+estado+"&informe="+informe+"&inf="+inf //  (instancia B) proponente que guarda como borrador |  (instancia P) proponente que envìa como director
			frmpropuesta.submit()
	
		}		
	}
	
function Aprobar ()
	{
		var codigo_prp=document.all.txtelegido.value
//		alert (persona)
		if (txtelegido.value!="" || txtelegido.value!=0)
			{
		//	AbrirPopup("calificar_propuesta.asp?codigo_prp=" + codigo_prp.substring(4,codigo_prp.length))
		window.open("calificar_propuesta.asp?codigo_prp=" + codigo_prp.substring(4,codigo_prp.length))
//alert (codigo_prp.substring(4,codigo_prp.length))
			}
	}
	
</script>

</head>

<body topmargin="5" leftmargin="0" rightmargin="0"> 


<%
inf=Request.QueryString("inf")
estado=Request.QueryString("estado")
instancia=Request.QueryString("instancia")
codigo_ipr=Request.QueryString("codigo_ipr")
coment1=Request.QueryString("coment1")
menu=Request.QueryString("menu")
codigo_per=session("codigo_Usu")
nombre_prp=Request.Form("txtNombrePrp")
informe=Request.QueryString("informe")
estado_prop=Request.QueryString("estado_prop")

%>
<table width="100%" height="100%" border="0" cellpadding="3" cellspacing="3">
  <tr>
    <td width="100%" height="6%">
		<table width="100%" height="100%" border="0" cellpadding="0" cellspacing="0">
			  <tr>
					<td width="10%"><input style="visibility:hidden" name="Submit2" <%if Request.QueryString("inf") ="SI"then%>style="visibility:hidden" <%end if%> type="button" class="nuevo1"  value="       Nuevo"  onClick="AbrirPopUp('registrapropuesta.asp','580','750','false','yes',0,0,'yes')"></td>
				<td width="10%"><input name="cmdmodificar" type="button" class="modificar_1" id="cmdmodificar" value="       Modificar" disabled="disabled" onClick="AbrirPopUp('registrapropuesta.asp?modificacion=1&codigo_prp=' + txtelegido.value.substring(4,txtelegido.value.length) + '&instancia_prp=<%=instancia%>','580','750')" style="visibility:hidden">
				<td width="10%"><input name="cmdver" disabled="disabled" type="button" class="ver2" id="cmdver" onClick="AbrirPopUp('registrapropuesta.asp?modifica=2&codigo_prp=' + txtelegido.value.substring(4,txtelegido.value.length) + '&instancia_prp=<%=instancia%>','430','600')" value="         Ver" style="visibility:hidden"></td>
				<td width="40%">
				<%if estado_prop="P" then%>
				<input  onClick="Aprobar()" name="cmdconforme2" type="submit"  class="enviaryrecibir1" id="cmdconforme2" value="         Calificar">
				<input name="Submit3" type="button" class="enviaryrecibir1" onClick="AbrirMenu()" value="      Cambiar Funci&oacute;n"></td>
				<%end if%>
					<td align="right">&nbsp;</td>
					<td width="10%"><input onClick="darveredicto('C','<%=Request.QueryString("menu")%>','<%=Request.QueryString("instancia")%>','<%=Request.QueryString("estado")%>','<%=Request.QueryString("informe")%>','<%=Request.QueryString("inf")%>')" name="cmdconforme" type="submit" disabled="disabled" class="conforme1" id="cmdconforme" value="       Conforme"></td>
					<td width="10%"><input onClick="darveredicto('O','<%=Request.QueryString("menu")%>','<%=Request.QueryString("instancia")%>','<%=Request.QueryString("estado")%>','<%=Request.QueryString("informe")%>','<%=Request.QueryString("inf")%>')" name="cmdobservar" type="submit" disabled="disabled" class="editar1" id="cmdobservar" value="       Observar"></td>
				    <td>&nbsp;</td>
			  </tr>
		</table>
	</td>
  </tr>
  <tr>
    <td height="29%" valign="top"><input name="hidden" type="HIDDEN" id="txtelegido">
	    <table width="100%" height="100%"  border="0" cellpadding="0" cellspacing="0">
		  <tr>
			<td valign="top" bgcolor="#FFFFFF" class="contornotabla">

			<form id="frmpropuesta" name="frmpropuesta" method="post" action="contenido.asp?estado=<%=estado%>&instancia=<%=instancia%>&menu=<%=menu%>&inf=<%=inf%>&informe=<%=informe%>&estado_prop=<%=estado_prop%>">
<%	
  			Set objCC=Server.CreateObject("PryUSAT.clsAccesoDatos")
	     	ObjCC.AbrirConexion	
			if instancia="" then
				instancia="%"
			else
				instancia=instancia
			end if	
		
			if estado="" then
				estado="%"
			else
				estado=estado
			end if	
			
			if informe="" then
				informe="%"
			else
				informe=informe
			end if		
			if  nombre_prp="" then
				nombre_prp="%"
			else
				nombre_prp=nombre_prp
			end if
			if inf="SI" then			
				set propuesta=objCC.Consultar("ConsultarPropuestas","FO","ID",instancia,estado,codigo_per,nombre_prp,informe)		
			else
''				set propuesta=objCC.Consultar("ConsultarPropuestas","FO","PD",instancia,estado,codigo_per,nombre_prp,informe)
				set propuesta=objCC.Consultar("ConsultarPropuestasRevision","FO","PD",instancia,estado,codigo_per,nombre_prp,informe,estado_prop)				
			end if
			ObjCC.CerrarConexion
			set objCC=nothing
%>
			<table width="100%" height="100%" border="0" cellpadding="0" cellspacing="0">
              <tr height="25">
                <td width="1%" bgcolor="#F0F0F0" class="bordeinf">&nbsp;</td>
                <td width="59%" bgcolor="#F0F0F0" class="bordeinf"><span class="Estilo1">
                  <%Response.Write(Request.QueryString("menu"))%>
                </span></td>
                <td width="6%" bgcolor="#F0F0F0" class="bordeinf">&nbsp;</td>
	
                <td width="20%" bgcolor="#F0F0F0" class="bordeinf"><input name="txtNombrePrp" type="text" id="txtNombrePrp" size="30" maxlength="30" value="<%if nombre_prp="%" then nombre_prp=""else Response.write(nombre_prp) end if%>"></td>
                <td width="14%" bgcolor="#F0F0F0" class="bordeinf"><input name="Submit" type="submit" class="buscar1" value="     Buscar "></td>
              </tr>
              <tr>
                <td colspan="5" valign="top">
				<table width="100%" height="100%" border="0" cellspacing="0" cellpadding="0" >
                    <tr>
                      <td width="2%"  height="3%" align="center" bgcolor="#F0F0F0" class="bordeinf"><img src="../../../../images/menus/prioridad_.gif" alt="Alta prioridad" width="18" height="17"></td>
                      <td width="3%%" align="center" bgcolor="#F0F0F0" class="bordeizqinf"><strong>D&iacute;as</strong></td>
                      <td width="40%"  height="3%" align="center" bgcolor="#F0F0F0" class="bordeizqinf"><strong>Nombre de la Propuesta </strong></td>
                      <td width="10%"  height="3%" align="center" bgcolor="#F0F0F0" class="bordeizqinf"><strong>Enviada el </strong></td>
                      <td width="40%"  height="3%" align="center" bgcolor="#F0F0F0" class="bordeizqinf"><strong>Proponente(s)</strong></td>
                      <td width="5%"  height="3%" align="center" bgcolor="#F0F0F0" class="bordeizqinf"><img src="../../../../images/menus/editar_1_s.gif" title="Observaciones sin leer">&nbsp;</td>
                    </tr>
                    <tr>
                      <td colspan="6"><div id="listadiv" style="height:100%" class="NoImprimir">
                          <table table class="sortable" id="unique_id" width="100%" border="0" cellpadding="0" cellspacing="0" name="rsTable" id=rsTable  cols=5 >
                            <tr>
                              <td width="2%" align="right" class="bordeinf">&nbsp;</td>
                              <td width="3%" align="right" class="bordeinf" ><span tooltip="Ordenar" >Ord.&darr;&uarr;</span></td>							  
                              <td width="40%" align="right" class="bordeinf">&nbsp; <div id="textovertical">&nbsp;Ordenar&darr;&uarr;</div> </td>
                              <td width="10%" align="right" class="bordeinf">&nbsp;Ordenar&darr;&uarr;</td>
                              <td width="40%" align="right" class="bordeinf">&nbsp;</td>
                              <td  width="5%" align="right" class="bordeinf">Ord.&darr;&uarr;</td>
                            </tr>
                            <%do while not propuesta.eof
								''--------------------------------------------------------
							 	''response.write(propuesta("fecha_prp"))
							  	Set objVence=Server.CreateObject("PryUSAT.clsAccesoDatos")
							  	objVence.AbrirConexion
								set RsFechas=objVence.Consultar("ConsultarFechasPropuesta","FO","FE",propuesta(0),codigo_per,instancia)
								objVence.CerrarConexion
								set objVence=nothing
								set interesado = nothing	
								''--------------------------------------------------------							
							%>
							
                            <tr id="fila<%=propuesta(0)%>" height="17"  onMouseOver="Resaltar(1,this,'S')" onMouseOut="Resaltar(0,this,'S')" class="Sel" Typ="Sel" onClick="<%if inf="SI" then%>ResaltarfilaDetalle('',this,'datos_informe.asp?codigo_prp=<%=propuesta(0)%>&instancia=<%=Request.QueryString("instancia")%>')<%else%>ResaltarfilaDetalle('',this,'datos_propuesta.asp?codigo_prp=<%=propuesta(0)%>&instancia=<%=Request.QueryString("instancia")%>')<%end if%>;ResaltarPestana_1('0','','');activamodifica('<%=instancia%>');ActivaBotones('<%=instancia%>','<%=estado%>','<%=informe%>')">
                              <td width="3%" class="bordeinf"><%
							 if propuesta("prioridad_prp") = "A" then%>
                                  <img src="../../../../images/menus/prioridad_.gif" width="18" height="17">
                             <%ELSE%>
							 &nbsp;
							  <%end if%>                              </td>
                              <td width="7%" align="center" class="bordeinf"><% 
							 if IsNull(RsFechas(1))=true then
								Response.Write("-")
							 else%>
							  <strong> <font color="#FF0000">	<%Response.Write(RsFechas(1))%> </font> </strong>
							 <%end if%>
							  
							  </td>

                              <td width="35%" class="bordeinf"><% response.write(propuesta("nombre_prp"))%></td>
                              <td width="10%" class="bordeinf" align="center">
							  <% 				
							  Response.Write(RsFechas(0))
							  %>							  </td>
                              <td width="40%" class="bordeinf" align="left">

                                  <%
											Set objInt=Server.CreateObject("PryUSAT.clsAccesoDatos")
											objInt.AbrirConexion
											set interesado=objInt.Consultar("ConsultarResponsablesPropuesta","FO","PR",propuesta(0),0)
											objInt.CerrarConexion
											set objInt=nothing
											do while not interesado.eof
												response.write("&nbsp;&nbsp;   "&  ConvertirTitulo(interesado(0)))%>
												<br>
							  <%
												interesado.movenext()
											loop
											set interesado = nothing					
											%></td>
                              <td width="5%" align="center" class="bordeinf">
							  <%											
											Set objObs=Server.CreateObject("PryUSAT.clsAccesoDatos")
											objObs.AbrirConexion
											set rsObs=objObs.Consultar("ConsultarComentarios","FO","SL",propuesta(0),codigo_per)											
											objObs.CerrarConexion
											set objObs=nothing
											Response.write(RsObs(0))																				 
											%>							  </td>
                            </tr>
                            <% 
								  np=np+1
								  propuesta.movenext()
								  loop 
								  
								  %>
                    </table>
                      </div>					  
				  </td>
                </tr>
              </table>
			  </form></td>
          </tr>
    </table></td>
  </tr>
</table>
	</td>
  </tr>
  <tr>
    <td height="53%" valign="top">
	<table cellspacing="0" cellpadding="0" style="border-collapse: collapse" bordercolor="#111111" width="100%" height="100%">
      <tr>

		<%if inf="SI" then%>
        <td class="pestanaresaltada_1" id="tab" align="center" width="20%" height="5%" onClick="ResaltarPestana_1('0','','');AbrirPropuesta('datos_informe.asp')">Datos Generales</td>
		<%else%>
        <td class="pestanaresaltada_1" id="tab" align="center" width="20%" height="5%" onClick="ResaltarPestana_1('0','','');AbrirPropuesta('datos_propuesta.asp')">Datos Generales</td>
		<%end if%>	
        <td width="1%" height="5%" class="bordeinf">&nbsp;</td>
        <td class="pestanabloqueada" id="tab" align="center" width="20%" height="5%" onClick="ResaltarPestana_1('1','','');AbrirPropuesta('revisores_propuesta.asp')">Revisores</td>
        <td width="1%" height="5%" class="bordeinf">&nbsp;</td>
		<td class="pestanabloqueada" id="tab" align="center" width="20%" height="5%" onClick="ResaltarPestana_1('2','','');AbrirPropuesta('comentarios_propuesta.asp')">Observaciones</td>
        <td width="45%" height="5%" class="bordeinf" align="right">
		<%
		if np="" then np=0 end if
		Response.write (np & " propuestas")
		set propuesta = nothing 
		%></td>
      </tr>
      <tr>
        <td width="100%" height="70%" valign="top" colspan="6" class="pestanarevez"><span id="mensajedetalle" class="usatsugerencia">&nbsp; &nbsp;&nbsp;&nbsp;Seleccione una propuesta para visualizar su detalle</span>
            <iframe id="fradetalle" height="100%" width="100%" border="0" frameborder="0" scrolling="yes"> </iframe></td>
      </tr>
    </table></td>
  </tr>
</table>

</body>


</html>
