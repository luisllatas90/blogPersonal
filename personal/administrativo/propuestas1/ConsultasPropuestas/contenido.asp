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
.Estilo3 {font-size: 6pt}
.Estilo4 {font-size: 5pt}
.Estilo5 {color: #000000}
-->
</style>
<script language="JavaScript" src="../../../../private/funciones.js"></script>
<script language="JavaScript" src="../../../../private/sorttable.js"></script>

<script>
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
instancia= Request.Form("cboinstancia")
area= Request.Form("cboarea")
mes= Request.Form("cbomes")
anio= Request.Form("anio")
tipoprp= Request.Form("cboTipo")
estado= Request.Form("cboestado")
proponente= Request.Form("txtproponente")
denominacion= Request.Form("txtNombrePropuesta")
informe= Request.Form("chkinforme")
prioridad= Request.Form("chkPrioridad")

if instancia= "" or instancia="0" then
	instancia=null
end if
if tipoprp="" or tipoprp<=0 then
	tipoprp=null
end if
if mes="" or mes<=0 then
	mes=null
end if
if anio="" then
	anio=null
end if
if area ="" or area<=0 then
	area=null
end if
if estado="" or estado="0" then
	estado=null
end if
if proponente="" then
	proponente=""
end if

if denominacion="" then
	denominacion=""
end if

if Request.Form("chkPrioridad")="on" then
	prioridad="A"
else
	prioridad=null
end if

if Request.Form("chkinforme")="on" then
	informe="0"
else
	informe=null
end if
menu=Request.QueryString("menu")
codigo_per=session("codigo_Usu")


%>
<table width="100%" height="100%" border="0" cellpadding="3" cellspacing="3">
  
  <tr> 	<%response.write (informe)%>
    <td width="100%" height="34%" valign="top"><input name="hidden" type="hidden" id="txtelegido">
	    <%			nombre_prp=Request.Form("txtNombrePrp")
  			Set objCC=Server.CreateObject("PryUSAT.clsAccesoDatos")
	     	ObjCC.AbrirConexion			
					set propuesta=objCC.Consultar("ConsultaAvanzadaPropuesta","FO",instancia,area,mes,anio,tipoprp,estado,proponente,denominacion,informe,prioridad)		
			ObjCC.CerrarConexion
			set objCC=nothing
%>
	    <table width="100%" height="100%"  border="0" cellpadding="0" cellspacing="0">
		  <tr>
			<td valign="top" bgcolor="#FFFFFF" class="contornotabla">

			<form id="frmpropuesta" name="frmpropuesta" method="post" action="contenido.asp?menu=<%=menu%>">
			  <table width="100%" height="100%" border="0" cellpadding="0" cellspacing="0">
              <tr height="25">
                <td width="1%" bgcolor="#F0F0F0" class="bordeinf">&nbsp;</td>
                <td bgcolor="#F0F0F0" class="bordeinf"><span class="Estilo1">
                  <%''Response.Write(Request.QueryString("menu"))%>
                </span>
                  <table width="100%" height="100%" border="0" cellpadding="0" cellspacing="0">
                    <tr>
                      <td>						<table width="100%"  border="0" cellspacing="1" cellpadding="2">
                        <tr>
                          <td width="25%"><select name="cboinstancia" id="cboinstancia">
                            <option <%if instancia=>"0" then%> selected <%end if%> value="0">--Todas las Instancias--</option>
                            <option <%if instancia="P" then%> selected <%end if%> value="P">Proponente</option>
                            <option <%if instancia="D" then%> selected <%end if%> value="D">Director</option>
                            <option <%if instancia="R" then%> selected <%end if%> value="R">Revisor</option>
                            <option <%if instancia="S" then%> selected <%end if%> value="S">Secretar&iacute;a General</option>
                            <option <%if instancia="C" then%> selected <%end if%> value="C">Consejo Universitario</option>
                                                                                                        </select></td>
                          <td width="25%"><span class="Estilo5">
						<% 
							Set objCC1=Server.CreateObject("PryUSAT.clsAccesoDatos")
							ObjCC1.AbrirConexion
							set rstipoprp=objCC1.Consultar("ConsultarTipoPropuestas","FO","TO")
							ObjCC1.CerrarConexion
							set objCC1=nothing
						
							call llenarlista("cboTipo","",rstipoprp,"codigo_tpr","descripcion_tpr",tipoprp,"Todos los Tipos","","")
							set rstipoprp = nothing
						 %>
                          </span> </td>
                          <td width="25%"><select name="cbomes" id="cbomes">
                            <option <%if mes=0 then%> selected<%end if%> value="0">--Todos los meses--</option>
                            <option <%if mes=1 then%> selected<%end if%> value="1">Enero</option>
                            <option <%if mes=2 then%> selected<%end if%> value="2">Febrero</option>
                            <option <%if mes=3 then%> selected<%end if%> value="3">Marzo</option>
                            <option <%if mes=4 then%> selected<%end if%> value="4">Abril</option>
                            <option <%if mes=5 then%> selected<%end if%> value="5">Mayo</option>
                            <option <%if mes=6 then%> selected<%end if%> value="6">Junio</option>
                            <option <%if mes=7 then%> selected<%end if%> value="7">Julio</option>
                            <option <%if mes=8 then%> selected<%end if%> value="8">Agosto</option>
                            <option <%if mes=9 then%> selected<%end if%> value="9">Setiembre</option>
                            <option <%if mes=10 then%> selected<%end if%> value="10">Octubre</option>
                            <option <%if mes=11 then%> selected<%end if%> value="11">Noviembre</option>
                            <option <%if mes=12 then%> selected<%end if%> value="12">Diciembre</option>
                          </select></td>
                          <td width="25%"><select name="anio" id="anio" >
                            <option <%if Request.Form("anio")="" then %> selected <%end if%> value="">-- Todos los A&ntilde;os --</option>
                            <%for i=0 to year(date)-1999 %>
                            <option <%if Request.Form("anio")=cstr(year(date)-i) then %> selected="selected" <%end if%>value="<%=year(date)-i%>"><%=year(date)-i%></option>
                            <%next %>
                          </select></td>
                        </tr>
                      </table></td>
                    </tr>
                    <tr>
                      <td><table width="100%"  border="0" cellspacing="1" cellpadding="2">
                        <tr>
                          <td width="45%"><span class="Estilo5">
                            <% 
							Set objCC1=Server.CreateObject("PryUSAT.clsAccesoDatos")
							ObjCC1.AbrirConexion
							set rsarea=objCC1.Consultar("consultarcentrocosto","FO","li",0)
							ObjCC1.CerrarConexion
							set objCC1=nothing
						
							call llenarlista("cboarea","",rsarea,"codigo_cco","descripcion_cco",area,"Todas los Áreas","","")
							set rsarea = nothing
						 %>
                          </span></td>
                          <td width="20%"><select name="cboestado" id="cboestado">
                            <option <%if estado="0" then%> selected<%end if%> value="0">--Todos los Estados--</option>
                            <option <%if estado="P" then%> selected<%end if%> value="P">Pendiente</option>
                            <option <%if estado="A" then%> selected<%end if%> value="A"> Aprobado</option>
                                                    </select> </td>
                          <td width="14%" align="right"><input <%if informe="0" then%> checked <%end if%> name="chkInforme" type="checkbox" id="chkInforme3">
Informe</td>
                          <td width="21%" align="right"><input <%if prioridad="A" then%> checked <%end if%> name="chkPrioridad" type="checkbox" id="chkPrioridad3">
Prioridad Alta</td>
                        </tr>
                      </table></td>
                    </tr>
                    <tr>
                      <td><table width="100%"  border="0" cellspacing="0" cellpadding="2">
                        <tr>
                          <td width="10%">Proponente</td>
                          <td width="30%"><input name="txtproponente" type="text" class="Cajas2" id="txtproponente" value="<%=proponente%>"></td>
                          <td width="22%">Nombre de la Propuesta </td>
                          <td width="28%"><input name="txtNombrePropuesta" type="text" class="Cajas2" id="txtNombrePropuesta" value="<%=denominacion%>"></td>
                        </tr>
                      </table></td>
                    </tr>
                  </table></td>
                <%if nombre_prp="NO" or nombre_prp="" then
					filtro_prp=""
				else
					filtro_prp=nombre_prp
				end if%>
                <td width="14%" align="center" bgcolor="#F0F0F0" class="bordeinf"><input name="Submit" type="submit" class="buscar1" value="     Buscar "></td>
              </tr>
              <tr>
                <td colspan="3" valign="top">
				<table width="100%" height="100%" border="0" cellspacing="0" cellpadding="0" >
                    <tr>
                      <td width="2%"  height="3%" align="center" bgcolor="#F0F0F0" class="bordeinf"><img src="../../../../images/menus/prioridad_.gif" alt="Alta prioridad" width="18" height="17"></td>
                      <td width="4%"  height="3%" align="center" bgcolor="#F0F0F0" class="bordeizqinf"><img src="../../../../images/menus/menu3.gif" alt="D&iacute;as restantes" width="16" height="16"></td>
                      <td width="34%" align="center" bgcolor="#F0F0F0" class="bordeizqinf"><strong>Nombre de la Propuesta </strong></td>
                      <td width="23%"  height="3%" align="center" bgcolor="#F0F0F0" class="bordeizqinf"><strong>Enviada el </strong></td>
                      <td width="29%"  height="3%" align="center" bgcolor="#F0F0F0" class="bordeizqinf"><strong>Proponente(s)</strong></td>
                      <td width="8%"  height="3%" align="center" bgcolor="#F0F0F0" class="bordeizqinf"><img src="../../../../images/menus/editar_1_s.gif" title="Observaciones sin leer">&nbsp;</td>
                    </tr>
                    <tr>
                      <td colspan="6"><div id="listadiv" style="height:100%" class="NoImprimir">
                          <table table class="sortable" id="unique_id" width="100%" border="0" cellpadding="0" cellspacing="0" name="rsTable" id=rsTable  cols=5 >
                            <tr>
                              <td width="2%" align="right" class="bordeinf">&nbsp;</td>
                              <td width="4%" align="right" class="bordeinf">&nbsp;</td>
                              <td width="34%" align="right" class="bordeinf">&nbsp;Ordenar</td>
                              <td width="23%" align="right" class="bordeinf">&nbsp;Ordenar</td>
                              <td width="29%" align="right" class="bordeinf">&nbsp;</td>
                              <td  width="8%" align="right" class="bordeinf">Ord.</td>
                            </tr>
                            <%
							''if propuesta.EOF and propuesta.BOF then
							''else
							do while not propuesta.eof	%>
							
                            <tr id="fila<%=propuesta(0)%>" height="17"  onMouseOver="Resaltar(1,this,'S')" onMouseOut="Resaltar(0,this,'S')" class="Sel" Typ="Sel" onClick="ResaltarfilaDetalle('',this,'datos_propuesta.asp?codigo_prp=<%=propuesta(0)%>');ResaltarPestana_1('0','','');activamodifica('<%=veredicto%>','<%=estado%>')">
                              <td width="2%"><%
									if propuesta("prioridad_prp") = "A" then%>
                                  <img src="../../../../images/menus/prioridad_.gif" width="18" height="17">
                                  <%end if%>                              </td>
                              <td width="4%" align="center">
										<%
										if veredicto="C" or veredicto="N" or estado="A" then
										Response.write("")
										else	
											Set objDia=Server.CreateObject("PryUSAT.clsAccesoDatos")
											objDia.AbrirConexion
											set RsDia=objDia.Consultar("ConsultarPropuestas","FO","DF",codigo_per,"C","",propuesta(0))
											objDia.CerrarConexion
											set objDia=nothing
											Response.write(RsDia(0))											
										end if
							 // ConsultarPropuestas 'df',33,'r','',317
							 %>							  </td>
                              <td width="34%"><% response.write(propuesta("nombre_prp"))%></td>
                              <td width="23%"><% response.write(propuesta("fecha_prp"))%></td>
                              <td width="29%">

                                  <%
											Set objInt=Server.CreateObject("PryUSAT.clsAccesoDatos")
											objInt.AbrirConexion
											set interesado=objInt.Consultar("ConsultarResponsablesPropuesta","FO","PR",propuesta(0),0)
											objInt.CerrarConexion
											set objInt=nothing
											do while not interesado.eof
												response.write("- " & ConvertirTitulo(interesado(0)))%>
												<br>
												<%
												interesado.movenext()
											loop
											set interesado = nothing					
											%>							  </td>
                              <td width="8%" align="center">
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
								  propuesta.movenext()
								  loop 
								 
							''end if	  
								  %>
                    </table>
                      </div>				  </td>
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
    <td height="40%" valign="top"><table cellspacing="0" cellpadding="0" style="border-collapse: collapse" bordercolor="#111111" width="100%" height="100%">
      <tr>
        <td class="pestanaresaltada_1" id="tab" align="center" width="20%" height="5%" onClick="ResaltarPestana_1('0','','');AbrirPropuesta('datos_propuesta.asp')">Datos Generales</td>
        <td width="1%" height="5%" class="bordeinf">&nbsp;</td>
        <td class="pestanabloqueada" id="tab" align="center" width="20%" height="5%" onClick="ResaltarPestana_1('1','','');AbrirPropuesta('revisores_propuesta.asp')">Revisores</td>
        <td width="1%" height="5%" class="bordeinf">&nbsp;</td>
		<td class="pestanabloqueada" id="tab" align="center" width="20%" height="5%" onClick="ResaltarPestana_1('2','','');AbrirPropuesta('comentarios_propuesta.asp')">Observaciones</td>
        <td width="45%" height="5%" class="bordeinf" align="right"><strong><%
 Response.Write(propuesta.RECORDCOUNT)
 set propuesta = nothing 
%></strong>&nbsp;propuestas encontradas</td>
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

