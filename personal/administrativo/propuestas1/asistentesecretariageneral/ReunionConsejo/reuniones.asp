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
<script language="JavaScript" src="../../../../private/funciones.js"></script>
<script language="JavaScript" src="../../../../private/sorttable.js"></script>

<script>
	function Informar(){
//	cmdInformar.disabled="true"
		var codigo_prp=document.all.txtelegido.value
		
		if (txtelegido.value!="" || txtelegido.value!=0)

			fradetalle.cargar.style.visibility='visible'
			{fradetalle.location.href="datos_reunion.asp?informar=SI&codigo_rec=" + codigo_prp.substring(4,codigo_prp.length)}

	
	}
	function verPresentacion(){
		var codigo_rec=document.all.txtelegido.value
		codigo_rec=codigo_rec.substring(4,codigo_rec.length)	
		day = new Date();
		id = day.getTime();
		//var izq = 300//(screen.width-ancho)/2
		//alert (izq)
		//var arriba= 200//(screen.height-alto)/2
		var URL="presentacion_intro.asp?codigo_rec=" + codigo_rec	
		eval("page" + id + " = window.open(URL, '" + id + "', 'toolbar=NO,scrollbars=0,location=0,statusbar=0,status=0,menubar=0,resizable=1,fullscreen=yes');");
	}
	function verActa(){
		var codigo_rec=document.all.txtelegido.value
		codigo_rec=codigo_rec.substring(4,codigo_rec.length)
		day = new Date();
		id = day.getTime();
		var izq = 80//(screen.width-ancho)/2
		//alert (izq)
		var arriba= 20//(screen.height-alto)/2
		var URL="GenerarActa.asp?codigo_rec=" + codigo_rec	
		//alert (codigo_rec)
		location.href=URL
		//eval("page" + id + " = window.open(URL, '" + id + "', 'toolbar=NO,scrollbars=1,location=0,statusbar=0,status=0,menubar=0,resizable=1,width=900,height=600');");
		//window.close()
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
	function activamodifica(veredicto,estado){
	//cmdmodificar.disabled=false
	cmdver.disabled=false
	cmdmodificar.disabled=false
	cmdGeneraActa.disabled=false
	cmdPresentacion.disabled=false
	cmdInformar.disabled=false

	}
	function observar(veredicto,veredicto2,menu){
	var codigo_prp=document.all.txtelegido.value
	var veredicto1=veredicto	
	popUp("registracomentario.asp?accion=veredicto&codigo_prp=" + codigo_prp.substring(4,codigo_prp.length) + "&veredicto=" + veredicto + "&veredicto2=" + veredicto2 + "&menu=" + menu)	
	}
	 
	function darveredicto(veredicto,veredicto2,menu){
	var codigo_prp=document.all.txtelegido.value
	var veredicto1=veredicto
//	alert(veredicto2)
//	alert(menu)
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
			location.href="procesar.asp?accion=veredicto&codigo_prp=" + codigo_prp.substring(4,codigo_prp.length) + "&veredicto=" + veredicto + "&instanciaRevisores=C&veredicto2=" + veredicto2 + "&menu=" + menu
			if (veredicto=="O"){
				observar(veredicto,veredicto2,menu)
			}
		}		
	}
	 

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

<body topmargin="5" leftmargin="0" rightmargin="0"> 


<%
//estado=Request.QueryString("estado")
//instancia=Request.QueryString("instancia")
codigo_ipr=Request.QueryString("codigo_ipr")
coment1=Request.QueryString("coment1")
veredicto=Request.QueryString("veredicto")
menu=Request.QueryString("menu")
codigo_per=session("codigo_Usu")
estado=Request.QueryString("estado")

%>
<table width="100%" height="100%" border="0" cellpadding="3" cellspacing="3">
  <tr>
    <td width="100%" height="6%">
		<table width="100%" height="100%" border="0" cellpadding="0" cellspacing="0">
			  <tr>
					<td width="10%"><input name="Submit2" type="button" class="nuevo1"  value="       Nuevo"  onClick="AbrirPopUp('registrareunion.asp?modifica=0','400','650')"></td>
					<td width="10%"><input name="cmdmodificar" type="button" class="modificar_1" <%if estado="A" then%> style="visibility:hidden" <%end if%> id="cmdmodificar" value="       Modificar" disabled="disabled" onClick="AbrirPopUp('registrareunion.asp?modifica=1&codigo_rec=' + txtelegido.value.substring(4,txtelegido.value.length),'430','600')"></td>
					<td width="10%"><input name="cmdver" disabled="disabled" style="visibility:hidden" type="button" class="ver2" id="cmdver" onClick="AbrirPopUp('registrapropuesta.asp?modifica=2&codigo_prp=' + txtelegido.value.substring(4,txtelegido.value.length) + '&instancia_prp=<%=instancia%>','430','600')" value="         Ver"></td>
					<td width="10%">&nbsp;</td>
					<td width="10%">&nbsp;</td>
					<td width="10%">&nbsp;</td>
				<td width="40%" align="right">&nbsp;</td>
					<td width="10%">&nbsp;</td>
					<td width="10%">&nbsp;</td>
					<td width="10%">&nbsp;</td>
			  </tr>
		</table>
	</td>
  </tr>
  <tr>
    <td height="34%" valign="top"><input name="hidden" type="hidden" id="txtelegido">
	    <table width="100%" height="100%"  border="0" cellpadding="0" cellspacing="0">
		  <tr>
			<td valign="top" bgcolor="#FFFFFF" class="contornotabla">

			<form id="frmpropuesta" name="frmpropuesta" method="post" action="reuniones.asp?veredicto=<%=veredicto%>&menu=<%=menu%>&estado=<%=estado%>">
<%			nombre_prp=Request.Form("txtNombrePrp")
  			Set objCC=Server.CreateObject("PryUSAT.clsAccesoDatos")
	     	ObjCC.AbrirConexion			
			if nombre_prp ="" then
				nombre_prp="NO"
			end if

				if nombre_prp = "NO" then
					set reunion=objCC.Consultar("ConsultarReunionConsejo","FO","CO",0,0,0)			
				else
					set reunion=objCC.Consultar("ConsultarReunionConsejo","FO","CN",0,0,nombre_prp)	

					//set propuesta=objCC.Consultar("ConsultarPropuestas","FO","AN",codigo_per,0,estado,nombre_prp)			
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
				<%if nombre_prp="NO" or nombre_prp="" then
					filtro_prp=""
				else
					filtro_prp=nombre_prp
				end if%>
                <td width="20%" bgcolor="#F0F0F0" class="bordeinf"><input name="txtNombrePrp" type="text" id="txtNombrePrp" size="30" maxlength="30" value="<%=filtro_prp%>"></td>
                <td width="14%" bgcolor="#F0F0F0" class="bordeinf"><input name="Submit" type="submit" class="buscar1" value="     Buscar "></td>
              </tr>
              <tr>
                <td colspan="5" valign="top">
				<table width="100%" height="100%" border="0" cellspacing="0" cellpadding="0" >
                    <tr>
                      <td width="2%"  height="3%" align="center" bgcolor="#F0F0F0" class="bordeinf">&nbsp;</td>
                      <td width="16%"  height="3%" align="center" bgcolor="#F0F0F0" class="bordeinf"><strong>Fecha</strong></td>
                      <td width="16%" align="center" bgcolor="#F0F0F0" class="bordeizqinf"><strong>Tipo</strong></td>
                      <td width="47%"  height="3%" align="center" bgcolor="#F0F0F0" class="bordeizqinf"><strong>Descripci&oacute;n</strong></td>
                      <td width="19%"  height="3%" align="center" bgcolor="#F0F0F0" class="bordeizqinf"><strong>Lugar</strong></td>
                    </tr>
                    <tr>
                      <td colspan="6"><div id="listadiv" style="height:100%" class="NoImprimir">
                          <table table class="sortable" id="unique_id" width="100%" border="0" cellpadding="0" cellspacing="0" name="rsTable" id=rsTable  cols=5 >
                            <tr>
                              <td width="2%" align="right" class="bordeinf">&nbsp;</td>
                              <td width="16%" align="right" class="bordeinf">&nbsp;Ordenar</td>
                              <td width="16%" align="right" class="bordeinf">&nbsp;Ordenar</td>
                              <td width="47%" align="right" class="bordeinf">&nbsp;Ordenar</td>
                              <td width="19%" align="right" class="bordeinf">&nbsp;Ordenar</td>

                            </tr>
                            <%
							''if reunion.EOF and reunion.BOF then
							''else
							do while not reunion.eof	%>
							
                            <tr id="fila<%=reunion(0)%>" height="17"  onMouseOver="Resaltar(1,this,'S')" onMouseOut="Resaltar(0,this,'S')" class="Sel" Typ="Sel" onClick="ResaltarfilaDetalle('',this,'datos_reunion.asp?codigo_rec=<%=reunion(0)%>');ResaltarPestana_1('0','','');activamodifica('<%=veredicto%>','<%=estado%>')">
                              <td width="2%" align="center">&nbsp;</td>
                              <td width="16%" align="center"><%response.write reunion("fecha_rec")%></td>
                              <td width="16%" align="center"><%
							  if Ucase(reunion("tipo_rec")) = "O"then
								  	response.write ("Ordinaria")
							  else
							  		response.write ("Extraordinaria")
							  end if							  
							  %></td>
                              <td width="47%"><%response.write reunion("agenda_rec")%></td>
                              <td width="19%"><%response.write reunion("lugar_rec")%></td>
                            </tr>
                            <% 
								  reunion.movenext()
								  loop 
								  set reunion = nothing 
							''end if	  
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
    <td height="48%" valign="top"><table cellspacing="0" cellpadding="0" style="border-collapse: collapse" bordercolor="#111111" width="100%" height="100%">
      <tr>
        <td class="pestanaresaltada_1" id="tab" align="center" width="20%" height="5%" onClick="ResaltarPestana_1('0','','')">Datos Generales</td>
        <td width="30%" height="5%" align="right" class="bordeinf">
		<input <%if codigo_rec="" then%> disabled="disabled"<%end if%> onClick="Informar()" name="cmdPresentacion2" type="button" class="prioridad" id="cmdInformar" value="     Informar Agenda">
        <input <%if codigo_rec="" then%> disabled="disabled"<%end if%> onClick="verPresentacion()" name="cmdPresentacion" type="button" class="presentacion" id="cmdPresentacion" value="     Presentaci&oacute;n">
        <input <%if codigo_rec="" then%> disabled="disabled"<%end if%> onClick="verActa()" name="cmdGeneraActa" type="button" class="enviarpropuesta" id="cmdGeneraActa" value="      Genera Acta">
		</td>

      </tr>
      <tr>
        <td width="100%" height="70%" valign="top" colspan="6" class="pestanarevez"><span id="mensajedetalle" class="usatsugerencia">&nbsp; &nbsp;&nbsp;&nbsp;Seleccione una reuni&oacute;n de consejo para visualizar su detalle</span>
            <iframe id="fradetalle" height="100%" width="100%" border="0" frameborder="0" scrolling="yes"> </iframe></td>
      </tr>
    </table></td>
  </tr>
</table>

</body>


</html>

