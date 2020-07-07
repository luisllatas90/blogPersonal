<!--#include file="../../../../../funciones.asp"-->
<html>
<head>
<title>Criterios de B&uacute;squeda</title>
<link href="../../../../../private/estilo.css" rel="stylesheet" type="text/css" />
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
.Estilo3 {
	color: #395ACC;
	font-size: 10pt;
	font-weight: bold;
}
.Estilo5 {color: #000000}
-->
</style>
<script language="JavaScript" src="../../../../../private/funciones.js"></script>
<script language="JavaScript" src="../../../../../private/sorttable.js"></script>

<script>
<!--
function AbrirPropuesta(pagina)
	{
		var codigo_cni=document.all.txtelegido.value
		
		if (txtelegido.value!="" || txtelegido.value!=0)
			{fradetalle.location.href=pagina + "?codigo_cni=" + codigo_cni.substring(4,codigo_cni.length)}
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
	cmdmodificar.disabled=false
	//cmdver.disabled=false

	}
	function observar(veredicto,veredicto2,menu){
	var codigo_prp=document.all.txtelegido.value
	var veredicto1=veredicto	
	popUp("registracomentario.asp?accion=veredicto&codigo_prp=" + codigo_prp.substring(4,codigo_prp.length) + "&veredicto=" + veredicto + "&veredicto2=" + veredicto2 + "&menu=" + menu)	
	}
	 
	 function imprimir(){
	 fradetalle.focus()
	 fradetalle.print()
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
			location.href="procesar.asp?accion=veredicto&codigo_prp=" + codigo_prp.substring(4,codigo_prp.length) + "&veredicto=" + veredicto + "&instanciaRevisores=S&veredicto2=" + veredicto2 + "&menu=" + menu
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
   	var arriba= 60//(screen.height-alto)/2
	eval("page" + id + " = window.open(URL, '" + id + "', 'toolbar=NO,scrollbars=1,location=0,statusbar=0,status=0,menubar=0,resizable=0,width=600,height=550,left = "+ izq +",top = "+ arriba +"');");
	}

function MM_jumpMenu(targ,selObj,restore){ //v3.0
  eval(targ+".location='"+selObj.options[selObj.selectedIndex].value+"'");
  if (restore) selObj.selectedIndex=0;
}
//-->
</script>
</head>

<body topmargin="5" leftmargin="0" rightmargin="0"> 


<%
//estado=Request.QueryString("estado")
//instancia=Request.QueryString("instancia")
menu=Request.QueryString("menu")
codigo_per=session("codigo_Usu")
%>
<table width="100%" height="100%" border="0" cellpadding="3" cellspacing="3">
  <tr>
    <td width="100%" height="4%">
		<table width="100%" height="100%" border="0" cellpadding="0" cellspacing="0">
			  <tr>
					<td width="10%"><input name="Submit2" type="button" class="nuevo1"  value="       Nuevo"  onClick="popUp('registra_convenio.asp?modifica=0','600','600','yes')"></td>
					<td width="10%"><input name="cmdmodificar" type="button" class="modificar_1" <%if estado="A" then%> style="visibility:hidden" <%end if%> id="cmdmodificar" value="       Modificar" disabled="disabled" onClick="popUp('registra_convenio.asp?modifica=1&codigo_cni=' + txtelegido.value.substring(4,txtelegido.value.length) ,'600','600','yes')">
					</td>
					<td width="10%">&nbsp;</td>
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
    <td height="40%" valign="top"><input name="hidden" type="hidden" id="txtelegido">
	    <table width="100%" height="100%"  border="0" cellpadding="0" cellspacing="0">
		  <tr>
			<td valign="top" bgcolor="#FFFFFF" class="contornotabla">

			<form id="frmpropuesta" name="frmpropuesta" method="post" action="contenido_avanazado.asp?menu=<%=menu%>">
<%			
			ambito=Request.Form("ambito")
			modalidad=Request.Form("modalidad")
			renovacion=Request.Form("renovacion")
			aniofirma=Request.Form("aniofirma")
			estado=Request.Form("estado")
			denominacion=Request.Form("denominacion")
			if ambito ="" or cint(ambito)<0 then
				ambito=null
			end if
			if modalidad =""  or cint(modalidad)<0 then
				modalidad=null
			end if
			if renovacion ="" then
				renovacion=null
			end if
			if aniofirma ="" then
				aniofirma=null
			end if
			if estado ="" then
				estado=null
			end if	
			if denominacion ="" then
				denominacion=""
			end if				
			
										
  			Set objCC=Server.CreateObject("PryUSAT.clsAccesoDatos")
	     	ObjCC.AbrirConexion			
				set convenio=objCC.Consultar("ConsultaAvanzadaConvenio","FO"," "," ",ambito,modalidad,renovacion,aniofirma,estado,denominacion)
			ObjCC.CerrarConexion
			set objCC=nothing
%>
			<table width="100%" height="100%" border="0" cellpadding="0" cellspacing="0">
              <tr height="25">
                <td width="1%" rowspan="2" bgcolor="#F0F0F0" class="bordeinf">&nbsp;</td>
                <td width="88%" rowspan="2" align="right" valign="top" bgcolor="#F0F0F0" class="bordeinf">				<table width="100%"  border="0">
                  <tr>
                    <td width="30%"><span class="Estilo5">
                      <% 
		 	Set objCC1=Server.CreateObject("PryUSAT.clsAccesoDatos")
	     	ObjCC1.AbrirConexion
			set rstipoinst=objCC1.Consultar("ConsultarAmbitoConvenio","FO","TO",0)
	    	ObjCC1.CerrarConexion
			set objCC1=nothing
		
		 	call llenarlista("ambito","",rstipoinst,"codigo_amc","descripcion_amc",ambito,"-- Todos los Ámbitos --","","")
			set rsAmbito = nothing
		 %>
                    </span></td>
                    <td width="30%"><span class="Estilo5">
                      <% 
		 	Set objCC1=Server.CreateObject("PryUSAT.clsAccesoDatos")
	     	ObjCC1.AbrirConexion
			set rstipoinst=objCC1.Consultar("ConsultarModalidadConvenio","FO","TO",0)
	    	ObjCC1.CerrarConexion
			set objCC1=nothing
		
		 	call llenarlista("modalidad","",rstipoinst,"codigo_mdc","descripcion_mdc",modalidad,"-- Todas las Modalidades --","","")
			set rsAmbito = nothing
		 %>
                    </span></td>
                    <td width="40%">
					<select name="renovacion" class="Cajas2" id="renovacion" >
                    <option <%if Request.Form("renovacion")="" then %> selected <%end if%> value="">-- Todos Renovaci&oacute;n --</option>
                    <option <%if Request.Form("renovacion")="0" then %> selected <%end if%> value="0">Sin Renovaci&oacute;n</option>
                    <option <%if Request.Form("renovacion")="1" then %> selected <%end if%> value="1">Con Renovaci&oacute;n</option>
                    </select></td>
                  </tr>
                  <tr>
                    <td>
					
					<select name="aniofirma" class="Cajas2" id="aniofirma" >
                      <option <%if Request.Form("aniofirma")="" then %> selected <%end if%> value="">-- Todos los Años --</option>
                      <%for i=0 to year(date)-1999 %>
					  <option <%if Request.Form("aniofirma")=cstr(year(date)-i) then %> selected="selected" <%end if%>value="<%=year(date)-i%>"><%=year(date)-i%></option>
					  <%next %>
                    </select></td>
                    <td><select name="estado" class="Cajas2" id="estado" >
                      <option <%if Request.Form("estado")="" then %> selected <%end if%> value="">-- Todos los Estados --</option>
                      <option <%if Request.Form("estado")="V" then %> selected <%end if%>value="V">Vigente</option>
                      <option <%if Request.Form("estado")="C" then %> selected <%end if%>value="C">Caducado</option>
                      <option <%if Request.Form("estado")="F" then %> selected <%end if%>value="F">Finalizado</option>
                                        </select></td>
                    <td><table width="100%"  border="0">
                      <tr>
                        <td width="29%">Denominaci&oacute;n                          </td>
                        <td width="71%"><input name="denominacion" type="text" class="Cajas2" value="<%=Request.Form("denominacion")%>" size="20" maxlength="150"></td>
                      </tr>
                    </table></td>
                  </tr>
                </table></td>
                <td width="11%" align="center" bgcolor="#F0F0F0">
                  <input name="Submit" type="submit" class="buscar1" value="     Buscar">
                </td>
              </tr>
              <tr height="25">
                <td align="center" bgcolor="#F0F0F0" class="bordeinf"><span class="Estilo1">(<%=convenio.RecordCount%>) </span></td>
              </tr>
              <tr>
                <td colspan="3" valign="top">
				<table width="100%" height="100%" border="0" cellspacing="0" cellpadding="0" >
                    <tr>
                      <td width="35%"  height="3%" align="center" bgcolor="#F0F0F0" class="bordeinf"><strong>Denominaci&oacute;n</strong></td>
                      <td width="18%"  height="3%" align="center" bgcolor="#F0F0F0" class="bordeizqinf"><strong>Instituci&oacute;n (s) </strong></td>
                      <td width="10%" align="center" bgcolor="#F0F0F0" class="bordeizqinf"><strong>&Aacute;mbito</strong></td>
                      <td width="11%"  height="3%" align="center" bgcolor="#F0F0F0" class="bordeizqinf"><strong>Modalidad</strong></td>
                      <td width="13%"  height="3%" align="center" bgcolor="#F0F0F0" class="bordeizqinf"><strong>Fecha  Inicio</strong></td>
                      <td width="13%"  height="3%" align="center" bgcolor="#F0F0F0" class="bordeizqinf"><strong>Duraci&oacute;n</strong>&nbsp;</td>
                    </tr>
                    <tr>
                      <td colspan="6"><div id="listadiv" style="height:100%" class="NoImprimir">
                          <table table class="sortable" id="unique_id" width="100%" border="0" cellpadding="0" cellspacing="0" name="rsTable" id=rsTable  cols=5 >
                            <tr>
                              <td width="35%" align="right" class="bordeinf">&nbsp;Ordenar</td>
                              <td width="18%" align="right" class="bordeinf">&nbsp;Ordenar</td>
                              <td width="10%" align="right" class="bordeinf">&nbsp;Ordenar</td>
                              <td width="11%" align="right" class="bordeinf">&nbsp;Ordenar</td>
                              <td width="13%" align="right" class="bordeinf">&nbsp;Ordenar</td>
                              <td  width="13%" align="right" class="bordeinf">&nbsp;Ordenar</td>
                            </tr>
                            <%
							do while not convenio.eof	%>
							
                            <tr id="fila<%=convenio(0)%>" height="17"  onMouseOver="Resaltar(1,this,'S')" onMouseOut="Resaltar(0,this,'S')" class="Sel" Typ="Sel" onClick="ResaltarfilaDetalle('',this,'datos_convenio.asp?codigo_cni=<%=convenio(0)%>');activamodifica('<%=veredicto%>','<%=estado%>')">
                              <td width="35%" valign="top" class="bordeinf"><%=convenio(1)%><br><br></td>
                              <td  width="18%" align="center"  valign="top" class="bordeinf">
                                 			 <%
											Set objInt1=Server.CreateObject("PryUSAT.clsAccesoDatos")
											objInt1.AbrirConexion
												set institucion=objInt1.Consultar("ConsultarInstitucion","FO","ES",convenio(0),"")
											objInt1.CerrarConexion
											set objInt1=nothing
											do while not institucion.eof
											IF institucion("CODIGO_INS")<> "68" THEN
											%>
											<!-- <table>
											<tr>
											<td valign="top">-</td>
											<td>-->
											<%
													if len(trim(institucion("abreviatura_Ins")))=0 or isnull(institucion("abreviatura_Ins"))=true then
														response.write(UCASE(institucion("nombre_Ins")))
														//response.write(left(UCASE(institucion("nombre_Ins")),30))														//response.write(left(UCASE(institucion("nombre_Ins")),30))
													else
														response.write(UCASE(institucion("abreviatura_Ins")))
													end if
													Response.Write("<BR>("&UCASE(institucion("nombre_pai"))&")")	
											%>
											<!-- </td>
											</tr>
											</table>-->
							  <%
											END IF
											institucion.movenext()
											''response.write("<br>")
											
											loop
											set institucion = nothing					
											%></td>
                              <td  width="10%" align="center" valign="top" class="bordeinf"><%=convenio("Descripcion_Amc")%></td>
                              <td  width="11%" align="center" valign="top" class="bordeinf"><%=convenio("descripcion_Mdc")%></td>
                              <td  width="13%" align="center" valign="top" class="bordeinf"><%=convenio("fechaInicio_Cni")%></td>
                              <td  width="13%" align="center" valign="top" class="bordeinf"><%=convenio("duracion_Cni") & " " & convenio("periododuracion_Cni")%>
							 <% if convenio("vigencia_cni")=0then
									Response.write("Indefinida")
							  end if
							  %> </td>
                            </tr>
                            <% 
								  convenio.movenext()
								  loop 
								  set convenio = nothing 
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
    <td height="48%" valign="top"><table width="100%" height="100%" cellpadding="0" cellspacing="0" bordercolor="#111111" class="contornotabla" style="border-collapse: collapse">
      <tr>
        <td width="81%" height="5%" align="center" bgcolor="#E1F1FB" class="bordeinf" id="tab" onClick="AbrirPropuesta('datos_convenio.asp')"><span class="Estilo3">&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Datos del Convenio </span></td>
        <td width="19%" align="center" bgcolor="#E1F1FB" class="bordeinf" id="tab" onClick="AbrirPropuesta('datos_convenio.asp')"><input name="Submit3" type="submit" class="imprimir_prp" value="         Imprimir" onClick="imprimir()"></td>
      </tr>
      <tr>
        <td height="70%" valign="top" colspan="7" class="pestanarevez"><span id="mensajedetalle" class="usatsugerencia">&nbsp; &nbsp;&nbsp;&nbsp;Seleccione una propuesta para visualizar su detalle</span>
        <iframe id="fradetalle" height="100%" width="100%" border="0" frameborder="0" scrolling="yes"> </iframe></td>
      </tr>
    </table></td>
  </tr>
</table>

</body>


</html>