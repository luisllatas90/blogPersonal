<!--#include file="../../funciones.asp"-->
<%
dim codigo_men
dim obj
dim codigo_apl

codigo_apl=request.querystring("codigo_apl")
codigo_men=request.querystring("codigo_men")
if codigo_men="" then codigo_men=0

Sub CrearMenu(ByVal codigo_apl,ByVal codigoRaiz_Men,ByVal j)
		dim i,x
		dim ImagenMenu,EventoMenu,TextoMenu,idPadre,EstadoMenu
		dim rsMenu
		x=0
		
		Set rsMenu=Obj.Consultar("ConsultarAplicacionUsuario","FO","2",codigo_apl,codigoRaiz_men,0)
				
		for i=1 to rsMenu.recordcount
			cadena=""										

			'Genera espacios para jerarquía
			for x=1 to j
				'cadena=cadena & "..." ' incluir imagen en blanco
				cadena=cadena & "<img src='../../images/blanco.gif'>"
			next
					
			'====================================================================
			ImagenMenu=""
			EventoMenu=""
			TipoImagen="icono_men" 'Campo de íconos normales
			ImagenMenu=rsMenu(tipoImagen)
			TextoMenu=rsMenu("descripcion_men")
			idPadre=rsMenu("codigoRaiz_men")
						
			'====================================================================
			'Verificar imágen del menú, caso contrario colocar una por defecto
			'====================================================================
			if rsMenu("total_men")>0 then
				ImagenMenu="mas.GIF" 'Carga el campo de íconos de 16x16
			elseif rsMenu("icono_men")="" or IsNull(rsMenu("icono_men"))=true then
				ImagenMenu="menu.gif"
			end if

			'====================================================================
			'Verificar enlace del menú y determinar si es padre o Hijo
			'====================================================================
			if rsMenu("total_men")>0 then
				EventoMenu="onclick=""ExpandirNodo(Mnu" & rsMenu("codigo_men") & ")"""
			elseif rsMenu("enlace_men")<>"" or IsNull(rsMenu("enlace_men"))=false  then
				EventoMenu=" onClick=""AbrirMenu(spCarpeta" & rsMenu("codigo_men") & ",'" & rsMenu("enlace_men") & "')"""
			end if
				
			'====================================================================
			'Ocultar menús hijo
			'====================================================================
			if codigoRaiz_men>0 then
				if (codigoRaiz_men=rsMenu("codigoRaiz_men")) then
					EstadoMenu="style=""display:none"""
				end if
			end if
			
			'====================================================================
			'Imprimir Fila del menú
			'====================================================================
			response.write  "<tr " & EstadoMenu & " id='Mnu" & idPadre & "'>"  & vbcrlf

			response.write "<td " & EventoMenu & ">"
			response.write  cadena & "<img hspace='0' vspace='0' border='0' name='arrImgCarpetas' id='imgCarpeta" & rsMenu("codigo_men") & "' src='../../images/menus/" & ImagenMenu & "' align=absbottom ALT='" & rsMenu("descripcion_men") & "'>&nbsp;" & _
																 "<span id='spCarpeta" & rsMenu("codigo_men") & "'>" & TextoMenu & "</span>" & vbcrlf
			response.write "</td>" 								
			response.write  "</tr>" 

			x=x+1
													
			CrearMenu codigo_apl,rsMenu("codigo_men"),x

			rsMenu.movenext 						
			
		next
end Sub
%>
<html>
<HEAD>
<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1">
<link rel="stylesheet" type="text/css" href="../../private/estilo.css">
<script type= "text/javascript" language="JavaScript" src="../../private/funciones.js"></script>
<STYLE>
<!--
	img 	{
	border: 0px none;
	/*width: 16px;
	height: 16px;*/
}
	span    {top: 0;cursor:hand }
-->
</STYLE>
<script type= "text/javascript" language="Javascript">
var codigo_men=0
var codigoRaiz_men=0
var orden_men=1

	function AbrirMenu(idMenu,pagina)
	{
	 	var  menu = idMenu.innerText
			ElegirRecurso(idMenu)
			HabilitarBotones()
			codigo_men=idMenu.id.substring(9,idMenu.id.length)
			codigoRaiz_men=codigo_men
			orden_men=1
			obs_men.innerHTML="<b>URL:</b> " + pagina
	}
	
	function ExpandirNodo(item)
	{
		var idMenu=0;
		var img=""	
		obs_men.innerHTML="&nbsp;"

		if (item.length==undefined){
			idMenu=item.id
			img=document.getElementById("imgCarpeta" + idMenu.substring(3,idMenu.length));
						
			MostrarMenu(item,img)
	     	codigo_men=idMenu.substring(3,idMenu.length)
	     	codigoRaiz_men=codigo_men
	     	orden_men=2
        }
		else{
			idMenu=item[0].id
			img=document.getElementById("imgCarpeta" + idMenu.substring(3,idMenu.length));
			sp=document.getElementById("spCarpeta" + idMenu.substring(3,idMenu.length));
			
			for(i=0;i<item.length;i++){
				MostrarMenu(item[i],img)
	     	}
	     	
	     	codigo_men=idMenu.substring(3,idMenu.length)
	     	codigoRaiz_men=codigo_men
	     	orden_men=item.length+1
		}
	}
	
	function MostrarMenu(Hijo,img)
	{
		sp=document.getElementById("spCarpeta" + Hijo.id.substring(3,Hijo.id.length))
		
		if (Hijo.style.display!="none"){
			Hijo.style.display="none"
			img.src="../../images/menus/mas.gif"
		}
		else{
			Hijo.style.display=""
			img.src="../../images/menus/menos.gif"
        }

		ElegirRecurso(sp)
        HabilitarBotones()
	}
	
	function HabilitarBotones()
	{
		cmdModificar.disabled=false
		cmdEliminar.disabled=false
	}
	
	function EliminarMenu()
	{
		var mensaje="Acción irreversible. ¿Está seguro que desea eliminar el menú seleccionado?\n\n Recuerde que si eliminar el menú se eliminarán sus dependencias"
		
		if (codigo_men>0){
			if (confirm(mensaje)==true){
				location.href="procesar.asp?accion=Eliminarmenuaplicacion&codigo_men=" + codigo_men + "&codigo_apl=<%=codigo_apl%>"
			}
		}
	}
	
	function ModificarMenu()
	{
		if (codigo_men>0){
			AbrirPopUp("frmmenu.asp?accion=Modificarmenuaplicacion&codigo_apl=<%=codigo_apl%>&codigo_men=" + codigo_men,"250","550")
		}
	}
	
	function AgregarMenu()
	{
		AbrirPopUp("frmmenu.asp?accion=Agregarmenuaplicacion&codigo_apl=<%=codigo_apl%>&codigo_men=" + codigo_men + "&codigoRaiz_men=" + codigoRaiz_men + "&orden_men=" + orden_men,"250","550")
	}

</script>

<base target="_self" >
</HEAD>
<body bgcolor="#EEEEEE">
<table width="100%">
	<tr>
		<td>
		<input name="cmdAgregar" type="reset" value="  Agregar Menú" class="usatNuevo" onclick="AgregarMenu()">
		<input name="cmdModificar" type="button" value="Modificar" class="usatModificar" disabled="true" onclick="ModificarMenu()">
		<input name="cmdEliminar" type="button" value="Eliminar" class="usatEliminar" disabled="true" onclick="EliminarMenu()">
		</td>
		<td>&nbsp;</td>
	</tr>
	<tr>
		<td colspan="2" id="obs_men" class="usatTablaInfo">
		&nbsp;</td>
	</tr>
</table>
<table cellpadding="0" cellspacing="0" style="border-collapse: collapse;" width="100%" height="85%">
  	<tr>
        <td width="100%" height="95%" bgcolor="white" class="contornotabla_azul">
        <div id="listadiv" style="height:100%">
<%
		set obj=server.CreateObject("PryUSAT.clsAccesodatos")
			obj.Abrirconexion
			response.write  "<table cellpadding=2 cellspacing=0 border=0 width='100%' heigth='100%'>" & vbcrlf
				CrearMenu codigo_apl,codigo_Men,0
			response.write  "</table>"
			obj.CerrarConexion
		set obj=nothing
%>
		</div>
		</td>
	</tr>
</table>	
</body>
</html>