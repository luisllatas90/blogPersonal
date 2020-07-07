<!--#include file="../../../../../funciones.asp"-->
<html>
<head>
<title></title>
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
-->
</style>
<script language="JavaScript" src="../../../../../private/funciones.js"></script>
<script language="JavaScript" src="../../../../../private/sorttable.js"></script>

<script>
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
</script>
</head>

<body topmargin="5" leftmargin="0" rightmargin="0"> 


<%
//estado=Request.QueryString("estado")
//instancia=Request.QueryString("instancia")
menu=Request.QueryString("menu")
codigo_per=session("codigo_Usu")
estado=Request.QueryString("estado")

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

			<form id="frmpropuesta" name="frmpropuesta" method="post" action="contenido.asp?veredicto=<%=veredicto%>&menu=<%=menu%>&estado=<%=estado%>">
<%			nombre_prp=Request.Form("txtNombrePrp")
  			Set objCC=Server.CreateObject("PryUSAT.clsAccesoDatos")
	     	ObjCC.AbrirConexion			
			if nombre_prp ="" then
				nombre_prp="NO"
			end if

			if nombre_prp = "NO" then
				set convenio=objCC.Consultar("ConsultarConvenios","FO","FO",0,estado,0)
			else
				set convenio=objCC.Consultar("ConsultarConvenios","FO","FN",0,estado,nombre_prp)
			end if		
					
			ObjCC.CerrarConexion
			set objCC=nothing
%>
			<table width="100%" height="100%" border="0" cellpadding="0" cellspacing="0">
              <tr height="25">
                <td width="1%" bgcolor="#F0F0F0" class="bordeinf">&nbsp;</td>
                <td width="59%" bgcolor="#F0F0F0" class="bordeinf">                  <span class="Estilo1"><%Response.Write(Request.QueryString("menu"))%>
(<%=convenio.RecordCount%>)                  </span>
</td>
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
                              <td valign="top" width="35%"><%=convenio(1)%><br><br></td>
                              <td  valign="top"  width="18%" align="left">
                                 			 <%
											Set objInt1=Server.CreateObject("PryUSAT.clsAccesoDatos")
											objInt1.AbrirConexion
												set institucion=objInt1.Consultar("ConsultarInstitucion","FO","ES",convenio(0),"")
											objInt1.CerrarConexion
											set objInt1=nothing
											do while not institucion.eof
											%>
											<table>
											<tr>
											<td valign="top">-</td>
											<td>
											<%
													if len(trim(institucion("abreviatura_Ins")))=0 or isnull(institucion("abreviatura_Ins"))=true then
														response.write(left(ConvertirTitulo(institucion("nombre_Ins")),30))
													else
														response.write(ConvertirTitulo(institucion("abreviatura_Ins")))
													end if
											%>
											</td>
											</tr>
											</table>
											<%
											institucion.movenext()
											''response.write("<br>")
											loop
											set institucion = nothing					
											%>			
							  </td>
                              <td valign="top"  width="10%" align="center"><%=convenio("Descripcion_Amc")%></td>
                              <td valign="top"  width="11%" align="center"><%=convenio("descripcion_Mdc")%></td>
                              <td valign="top"  width="13%" align="center"><%=convenio("fechaInicio_Cni")%></td>
                              <td valign="top"  width="13%" align="center"><%=convenio("duracion_Cni") & " " & convenio("periododuracion_Cni")%>
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

