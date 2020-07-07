<!--#include file="../../../../funciones.asp"-->
<%
codigo_ins=request.querystring("codigo_ins")
codigo_eped=request.querystring("codigo_eped")
codigo_usu= session("codigo_usu")' Request.QueryString("id") '
tipobandeja=request.querystring("tipobandeja")
menu=request.querystring("menu")
tipo=Request.QueryString("tipo")
sw=0
	Set Obj= Server.CreateObject("Biblioteca.clsAccesoDatos")
	Obj.AbrirConexion
		set rsInstanciaPed=Obj.Consultar("BI_ConsultarInstanciaEvaluacion", "FO")		
	Obj.CerrarConexion
	set Obj = nothing

	if request.Form("codigo_per") ="" then
		codigo_per=0
		'codigo_per=codigo_usu
	else
		codigo_per=cint(Request.Form("codigo_per"))
	end if

	if (request.Form("codigo_ins")<>"") then
		if (request.Form("codigo_ins")<>0) then
		    codigo_ins = cint(request.form("codigo_ins")) 
			sw=1
		else
		    codigo_ins = cint(request.QueryString("codigo_ins"))
			sw=0
		end if
	end if
	'response.Write("[F-")
	'RESPONSE.Write(request.Form("codigo_ins"))
	'response.Write("] - [Q-")
	'RESPONSE.Write(request.querystring("codigo_ins"))
	'response.Write("]")
	if tipo="PE" then ' Para solicitudes Pendientes
		instancia = cint(request.querystring("codigo_ins"))
		valor = request.querystring("codigo_ins")
		'select case codigo_ins 
		'	case 1 :
		'	estado = 1
		'	case 2 :
		'	estado = 2
		'	case 3 :
		'	estado = 3	
		'end select
	else			 ' Para solicitudes Atendidas
		
		instancia = cint(request.form("codigo_ins"))
		valor = request.querystring("codigo_ins")+1
		'select case codigo_ins 
		'	case 1 :
		'	estado = 2
		'	case 2 :
		'	estado = 3
		'	case 3 :
		'	estado = 4	
		'end select
	end if
	Set Obj2= Server.CreateObject("Biblioteca.clsAccesoDatos")
	Obj2.AbrirConexion
	
	'response.Write( codigo_ins & " - "  & codigo_usu & " - "  & codigo_ins & " - " & codigo_per & " - " & sw)
	if tipo ="PE" then
		if codigo_per = 0  then
			tipo_consulta = "P1"
			set rsPedidos=Obj2.Consultar("BI_BuscarPedidosFechas", "FO", "TS", codigo_ins, codigo_usu, null, null, 0, codigo_usu)
		else
			tipo_consulta = "P2"
			set rsPedidos=Obj2.Consultar("BI_BuscarPedidosFechas", "FO", "PS", codigo_ins, codigo_per, null, null, 0, codigo_usu)
		end if
	else
		if codigo_per = 0  then

			if sw = 1 then
				tipo_consulta = "TI"
				set rsPedidos=Obj2.Consultar("BI_BuscarPedidosAtendidos", "FO", "TI", request.Form("codigo_ins"), codigo_usu, null, null, codigo_usu)
			else
				tipo_consulta = "TS"
				set rsPedidos=Obj2.Consultar("BI_BuscarPedidosAtendidos", "FO", "TS", codigo_ins, codigo_usu, null, null, codigo_usu)
			end if
		else
			if sw = 1 then
				tipo_consulta = "PI"
				set rsPedidos=Obj2.Consultar("BI_BuscarPedidosAtendidos", "FO", "PI", request.Form("codigo_ins"), codigo_per, null, null, codigo_usu)
			else
				tipo_consulta = "PS"
				set rsPedidos=Obj2.Consultar("BI_BuscarPedidosAtendidos", "FO", "PS", codigo_ins, codigo_per, null, null, codigo_usu)
			end if
		end if
	end if	

	if Not(rsPedidos.BOF and rsPedidos.EOF) then
		HayReg=true
	end if
	Set rsCentroCosto=Obj2.Consultar("ConsultarPedidoBibliografico","FO",1,0,0,0)	
	Set rsPersonal=Obj2.Consultar("ConsultarPedidoBibliografico","FO",2,0,0,0)	

	Obj2.CerrarConexion
	Set Obj2=nothing	
	'response.write(codigo_per & "--" & codigo_ins   )	
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
<script language="javascript" type="text/javascript">
	function EnviarDatos(pagina)
	{
		location.href=pagina //+ "?cboCentroCosto=" + frmPresupuesto.cboCentroCosto.value + "&txtAno=" + frmPresupuesto.txtAno.value
		//alert (txtPto.value);

	}
</script>
</head>
<body style="margin-top:0">
<form name="form1" action="" method="post" >
<table width="100%" bgcolor="#F0F0F0">
	<tr>
		<td colspan="2" class="usatTitulo">Ubicación del pedido</td>
		<td align="right">&nbsp;
		<!--<input name="cmdRegresa" type="button" value="Regresar" class="regresar2" onClick="history.back(-1)" >-->	</td>
	</tr>
	<% 'if codigo_ins = 2 then %>
<!--	<tr valign="top">
	  <td width="9%" height="41" bgcolor="#F0F0F0" >
	    <input name="cmdDetalle" type="button" value="      Enviar pedido" class="enviaryrecibir1" disabled="TRUE" onClick="AbrirPedidoEnviado('A',txtelegido.value,'P','<%'=tipo%>')" style="visibility:hidden">
       <label>Escuela</label></td>
	  <td width="79%" bgcolor="#F0F0F0" >-->
	<% 'call llenarlista("cboCodigo_cco","ValidarCentroCosto(this,'" & modo & "')",rsCentroCosto,"codigo_cco","descripcion_cco",codigo_cco,"Seleccione la Dirección","","")%>
		<% 'if codigo_cco<>"" then
			'Response.Write("<script>document.all.cboCodigo_cco.disabled=true")
		'end if
		%>	  
<!--		</td>
	  <td width="12%" align="right"><label></label></td>
  	</tr> -->
	
    <% 'End if%>
	<tr valign="top">
	  <td height="41" bgcolor="#F0F0F0" >Solicitante</td>
	  <td bgcolor="#F0F0F0" >
	  <select name="codigo_per" id="codigo_per">
        <option value="0" >TODOS</option>
		<%   do while (not rsPersonal.EOF)
			 'response.Write(rsInstanciaPed("codigo_ins"))
			%>
        <option  <% if (cint(codigo_per)=cint(rsPersonal("codigo_per"))) then 
						response.Write("selected='selected'") 
						end if%> 
			value="<%=rsPersonal("codigo_per")%>"><%=rsPersonal("Personal")%> </option>
        <%
		  	rsPersonal.movenext
		  	Loop 

		  %>
      </select></td>
	  <td align="right"><input style="width:80px" name="cmdBuscar" type="submit" class="buscar" value="Buscar" ></td>
    </tr>
	<tr valign="top">
	  <td height="41" width="100px" bgcolor="#F0F0F0" >Ubicaci&oacute;n</td>
	  <td bgcolor="#F0F0F0" >
	    <select name="codigo_ins" <% if tipo = "PE" then %> disabled="disabled" <% end if%>>
        <option value=0 >TODOS</option>
		<% do while (not rsInstanciaPed.EOF)

				if (cint(rsInstanciaPed("codigo_ins")) > valor-1 ) then
		%>
        <option
		<% 			if (instancia =cint(rsInstanciaPed("codigo_ins"))) then 
						response.Write("selected='selected'") 
		   			end if
		%>
		value="<%=rsInstanciaPed("codigo_ins")%>" > <%=rsInstanciaPed("descripcion_ins")%> </option>
        <% 		end if
				rsInstanciaPed.movenext
		  Loop 
	    %>
      </select>
	  </td>
	  <td align="right"><input name="cmdExportar" type="submit" class="excel" id="cmdExportar" style="width:80px" value="Exportar" onClick="EnviarDatos('ExportarListado.asp?tipo_consulta=<%=tipo_consulta%>&codigo_ins=<%=codigo_ins%>&codigo_per=<%=codigo_per%>&codigo_usu=<%=codigo_usu%>&instancia=<%=request.form("codigo_ins")%>')"  ></td>
    </tr>
</table>
<br>
<%if HayReg=true then%>
<table width="100%" height="88%" cellpadding="0" cellspacing="0" class="contornotabla">
	<tbody id="trLista" >
	<tr height="5%">
            <td width="5%" height="5%" class="etabla">N&ordm;</td>
            <td width="10%" height="5%" class="etabla">Fecha Registro </td>
            <td width="15%" height="5%" class="etabla">&Aacute;rea</td>
            <td width="23%" class="etabla">T&iacute;tulo</td>
            <td width="16%" class="etabla">Autor</td>
            <td width="7%" class="etabla">Cantidad</td>
            <td width="7%"  class="etabla">Estado</td>
			<td width="5%" class="etabla">Año Pub.</td>
            <td width="12%"  class="etabla">Editorial</td>
    </tr>
	<tr height="50%">
	<td colspan="9" width="100%" height="40%" >
		<div id="listadiv" style="height:100%" class="NoImprimir">
		<table width="100%" >
         <% 
		 i=0
		 do while not rsPedidos.EOF
		 i=i+1
		 %>
	  <tr class="Sel" Typ="Sel" id="fila<%=rsPedidos("codigo_Ped")%>" class="piepagina" valign="top" onMouseOver="Resaltar(1,this,'S')" onMouseOut="Resaltar(0,this,'S')" onclick="SeleccionarPedidos_v2(this); AbrirDetallesdePedido('detallepedido.asp?tipo=<%=tipo%>&codigo_ins=<%=codigo_ins%>');" >
            <td align="right" class="bordesup" width="5%" ><%=i%></td>
            <td class="bordesup" width="10%" ><%=mid(rsPedidos("FechaReg_Ped"),1,10)%></td>
            <td class="bordesup" width="22%" ><%=rsPedidos("descripcion_cco")%></td>
            <td class="bordesup" width="20%" ><%=rsPedidos("Titulo")%></td>
            <td class="bordesup" width="15%" ><%=rsPedidos("NombreAutor")%></td>		
            <td class="bordesup" width="8%" ><%=rsPedidos("Cantidad_Dpe")%></td>						
            <td class="bordesup" width="8%" ><%=rsPedidos("descripcion_Eped")%></td>
            <td class="bordesup" width="4%" ><%=rsPedidos("FechaPublicacion")%></td>						
            <td class="bordesup" width="8%" ><%=rsPedidos("NombreEditorial")%></td>			
          </tr>
		  <%
		  rsPedidos.MoveNext
		  loop%>		
		</table>
		</div>	</td>
	</tr>
</tbody>
	<tr id="trDetalle">
  		<td width="100%" height="45%" valign="top" colspan="9">
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
		    	<td width="100%" valign="top" colspan="9" class="pestanarevez">
					
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
