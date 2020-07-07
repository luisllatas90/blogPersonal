<!--#include file="../../../../funciones.asp"-->

<%
codigo_ins=request.querystring("codigo_ins")
codigo_eped=request.querystring("codigo_eped")
codigo_usu=session("codigo_usu")' Request.QueryString("id") '
tipobandeja=request.querystring("tipobandeja")
menu=request.querystring("menu")
tipo=Request.QueryString("tipo")
	if (request.Form("CboInstancia")="") then
		instancia = 0
	else
		instancia = cint(request.Form("CboInstancia"))
	end if	
	
	Set Obj= Server.CreateObject("Biblioteca.clsAccesoDatos")
		'Obj.AbrirConexion
		'if tipo="SO" then
			'Set rsPedidos=Obj.Consultar("BI_ConsultarPedidos","FO",tipo,codigo_ins,codigo_usu)
		'else
			'if tipo="PE" then
				'Set rsPedidos=Obj.Consultar("BI_ConsultarPedidos","FO",tipo,codigo_ins,codigo_usu)
			'else
				'Set rsPedidos=Obj.Consultar("BI_ConsultarPedidos","FO",tipo,codigo_ins,codigo_usu)		
			'end if		
		'end if
		'Obj.CerrarConexion

	
	Obj.AbrirConexion
		set rsInstanciaPed=Obj.Consultar("BI_ConsultarInstanciaEvaluacion", "FO")		
	Obj.CerrarConexion
	set Obj = nothing
%>
<%
	'instancia = 0
	Set Obj2= Server.CreateObject("Biblioteca.clsAccesoDatos")
	Obj2.AbrirConexion
	if request.Form("ChkFechas") = 1 then
		'response.Write("Fechas")
		set rsPedidos=Obj2.Consultar("BI_BuscarPedidosFechas", "FO", "CF", instancia, codigo_usu, request.Form("TxtFinicio"), request.Form("TxtFfin"), 0, 0)
	else
		'response.write("S/F")
		set rsPedidos=Obj2.Consultar("BI_BuscarPedidosFechas", "FO", "SF", instancia, codigo_usu, null, null,0,0)
	end if
	Obj2.CerrarConexion
	Set Obj2=nothing	
	if Not(rsPedidos.BOF and rsPedidos.EOF) then
		HayReg=true
	end if	
%>
<html>
<head>
<meta http-equiv="Content-Language" content="es">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<title>Lista de Pedidos</title>
<link rel="stylesheet" type="text/css" href="../../../../private/estilo.css">
<script language="JavaScript" src="../../../../private/funciones.js"></script>
<script language="JavaScript" src="../../../../private/validarpagina.js"></script>
<script language="JavaScript" src="../private/validarpedido.js"></script>
<script src="../../../../private/PopCalendar.js" language="javascript" type="text/javascript"></script>
<script> 
function ValidarBusqueda()
{	var a;
	a=0;
	fecha1=document.form1.TxtFinicio.value;
	fecha2=document.form1.TxtFfin.value; 
	f1=new Date(fecha1); 
	f2=new Date(fecha2); 

	if (document.form1.ChkFechas.checked == true)
	{	
		if (f1 == "" )
		{	alert('Debe ingresar una fecha de inicio');	a=a+1;	} 
		
		if (f2 == "" )
		{	alert('Debe ingresar una fecha final');	a=a+1; }
		
/*		if (f2 < f1)
		{	alert('La fecha final debe ser mayor a la fecha de inicio'); a=a+1; }
*/
		if (CompararFechas(fecha2,fecha1)== false)
		{	alert('La fecha final debe ser mayor a la fecha de inicio'); a=a+1; }
		
		if (a==0)
		{	return true;}
		else
		{	return false; }
	}
	else {return true;}
}
function ActivarCajas(check)
{
	if (check.checked == true)
	{	
		document.form1.BtnFini.disabled=false;
		document.form1.TxtFinicio.disabled=false;
		document.form1.BtnFfin.disabled=false;
		document.form1.TxtFfin.disabled=false;
		document.form1.TxtFinicio.style.backgroundColor="#FFFFFF";
		document.form1.TxtFfin.style.backgroundColor="#FFFFFF";
	}
	else
	{	
		document.form1.BtnFini.disabled=true;
		document.form1.BtnFfin.disabled=true;
		document.form1.TxtFinicio.value ="";
		document.form1.TxtFinicio.disabled=true;
		document.form1.TxtFfin.value ="";
		document.form1.TxtFfin.disabled=true;
		document.form1.TxtFinicio.style.backgroundColor="#CCCCCC";
		document.form1.TxtFfin.style.backgroundColor="#CCCCCC";
	}
}
</script>
</head>
<body style="margin-top:0">
<form name="form1" action="" method="post" onSubmit="return ValidarBusqueda()">
<script>
PopCalendar=getCalendarInstance();
PopCalendar.startAt = 0;
PopCalendar.showWeekNumber = 0;
PopCalendar.showToday = 1;
PopCalendar.showWeekend = 1;
PopCalendar.showHolidays = 1;
PopCalendar.selectWeekend = 0;
PopCalendar.selectHoliday = 0;
PopCalendar.addCarnival = 0;
PopCalendar.addGoodFriday = 1;
PopCalendar.language = 0;
PopCalendar.defaultFormat ='mm-dd-yyyy';
PopCalendar.fixedX = -1;
PopCalendar.fixedY = -1;
PopCalendar.fade = 0;
PopCalendar.shadow = 1;
PopCalendar.move = 1;
PopCalendar.saveMovePos = 1;
PopCalendar.centuryLimit = 40;
PopCalendar.initCalendar();
</script>
<table width="100%" bgcolor="#F0F0F0">

	<tr>
		<td colspan="5" class="usatTitulo">Ubicación del pedido</td>
		<td colspan="3" align="right">&nbsp;
		<!--<input name="cmdRegresa" type="button" value="Regresar" class="regresar2" onClick="history.back(-1)" >-->
	</td>
	</tr>
	<tr valign="top">
	  <td width="17%" height="41" bgcolor="#F0F0F0" >
	  
	  <!--<input name="cmdDetalle" type="button" value="      Enviar pedido" class="enviaryrecibir1" disabled="TRUE" onClick="AbrirPedidoEnviado('A',txtelegido.value,'P','<%'=tipo%>')">-->
	    <label>
      <input type="checkbox" name="ChkFechas" value="1"  <%if request.form("ChkFechas") = 1 then response.Write("checked='checked'") end if  %> onClick="ActivarCajas(this)"> Filtrar por fechas        </label>	  </td>
	  <td width="11%" bgcolor="#F0F0F0" >Fecha de inicio:	    </td>
	  <td width="18%" bgcolor="#F0F0F0" ><input name="TxtFinicio" type="text" id="TxtFinicio" size="15" value="<%=request.form("TxtFinicio")%>">
      <input name="BtnFini" type="button"  class="cunia" id="BtnFini" style="height: 22px" onClick="PopCalendar.selectWeekendHoliday(1,1);PopCalendar.show(document.form1.TxtFinicio,'dd/mm/yyyy')" /></td>
	  <td width="8%" bgcolor="#F0F0F0" >Fecha Final: </td>
	  <td width="17%" bgcolor="#F0F0F0" >
	    <label>
	      <input name="TxtFfin" id="TxtFfin" type="text" size="15" value="<%=request.form("TxtFfin")%>">
      </label>	  <input name="BtnFfin" type="button"  class="cunia" id="BtnFfin" style="height: 22px" onClick="PopCalendar.selectWeekendHoliday(1,1);PopCalendar.show(document.form1.TxtFfin,'dd/mm/yyyy')" /></td>
	  <td width="7%" align="right">Ubicaci&oacute;n:</td>
      <td width="10%" align="right"><label>
        <div align="left">
          <select name="cboInstancia">
		  <% 
		     do while (not rsInstanciaPed.EOF)
			 'response.Write(rsInstanciaPed("codigo_ins"))
			%>
            <option  <% if (cint(instancia)=cint(rsInstanciaPed("codigo_ins"))) then 
						response.Write("selected='selected'") 
						end if%> 
			value="<%=rsInstanciaPed("codigo_ins")%>"><%=rsInstanciaPed("descripcion_ins")%> </option>
		  <%
		  	rsInstanciaPed.movenext
		  	Loop 

		  %>
          </select>
        </div>
      </label></td>
      <td width="12%" align="right"><label>
      <input style="width:80px" name="cmdBuscar" type="submit" class="buscar" value="Buscar" >
      </label></td>
  </tr>
</table>
<br>
<%if HayReg=true then%>
<table width="100%" height="88%" cellpadding="0" cellspacing="0" class="contornotabla">
	<tbody id="trLista" >
	<tr height="5%">
            <td width="5%" height="5%" class="etabla">N&ordm;</td>
            <td width="10%" height="5%" class="etabla">Fecha Registro </td>
            <td width="22%" height="5%" class="etabla">&Aacute;rea</td>
            <td width="25%" class="etabla">T&iacute;tulo</td>
            <td width="20%" class="etabla">Autor</td>
            <td width="8%" class="etabla">Cantidad</td>
            <td width="10%"  class="etabla">Estado</td>
    </tr>
	<tr height="50%">
	<td colspan="8" width="100%" height="50%" >
		<div id="listadiv" style="height:100%" class="NoImprimir">
		<table width="100%" >
         <% 
		 i=0
		 do while not rsPedidos.EOF
		 i=i+1
		 %>
	  <tr class="Sel" Typ="Sel" id="fila<%=rsPedidos(1)%>" class="piepagina" valign="top" onMouseOver="Resaltar(1,this,'S')" onMouseOut="Resaltar(0,this,'S')" onclick="SeleccionarPedidos_v2(this); AbrirFicha('detallepedido.asp?codigo_ins=<%=codigo_ins%>&tipo=<%=tipo%>',this);">
            <td align="right" class="bordesup" width="3%" ><%=i%></td>
            <td class="bordesup" width="10%" ><%=rsPedidos(0)%></td>
            <td class="bordesup" width="22%" ><%=rsPedidos(2)%></td>
            <td class="bordesup" width="25%" ><%=rsPedidos("Titulo")%></td>
            <td class="bordesup" width="20%" ><%=rsPedidos("NombreAutor")%></td>		
            <td class="bordesup" width="8%" ><%=rsPedidos("Cantidad_Dpe")%></td>						
            <td class="bordesup" width="10%" ><%=rsPedidos("descripcion_Eped")%></td>
          </tr>
		  <%
		  rsPedidos.MoveNext
		  loop%>		
		</table>
		</div>	</td>
	</tr>
</tbody>
	<tr id="trDetalle">
  		<td width="100%" height="45%" valign="top" colspan="8">
			<table cellspacing="0" cellpadding="0" style="border-collapse: collapse" bordercolor="#111111" width="100%" height="100%">
				<tr height="8%">
					<td class="pestanaresaltada" id="tab" align="center" width="20%" onClick="ResaltarPestana2('0','',''); AbrirDetallesdePedido('detallepedido.asp?tipo=<%=tipo%>&codigo_ins=<%=codigo_ins%>');">
                    Datos del Pedido</td>
					<td width="1%" class="bordeinf">&nbsp;</td>
					<!--
					<td class="pestanabloqueada" id="tab" align="center" width="20%" onClick="ResaltarPestana2('1','',''); AbrirDetallesdePedido('historial.asp?tipo=<%'=tipo%>');">
                    Historial de Movimientos</td>-->
                    <td width="1%" class="bordeinf">&nbsp;</td>
                   <!--
					<td class="pestanabloqueada" id="tab" align="center" width="20%" height="8%" onclick="ResaltarPestana2('2','','');AbrirFicha('movimientopagos.asp',this)">
                    Estado de Cuenta</td>
                    end if
                    -->
                    <td width="20%" class="bordeinf" align="right">
                    <img border="0" src="../../../../images/imprimir.gif" style="cursor:hand" ALT="Imprimir Ficha" onClick="ImprimirFicha()">
                    <img border="0" src="../../../../images/maximiza.gif" style="cursor:hand" ALT="Maximizar ventana" onClick="Maximizar(this,'../../../../','100%','45%')">                    </td>
				</tr>
	  			<tr height="92%">
		    	<td width="100%" valign="top" colspan="8" class="pestanarevez">
					
					<iframe id="fradetalle" height="100%" width="100%" border="0" frameborder="0" style="background-color:#CCCCCC">					</iframe>				</td>
			  </tr>
			</table>  		</td>
  	</tr>
</table>
</form>
<input type="hidden" id="txtelegido" name="txtelegido" value="0">
<%else%>
	<h5 class="usatsugerencia">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; No se 
	han encontrado Pedidos Bibliográficos registrados</h5>
<%end if%>
</body>
</html>
