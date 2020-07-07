<!--#include file="../../NoCache.asp"-->
<!--#include file="../../funciones.asp"-->
<%
dim codigo_men
dim obj
dim pID
dim pPag
dim Nodo
dim codigo_usu

call Enviarfin(session("codigo_usu"),"../../")

codigo_men=request.querystring("codigo_men")
tipoImagen=request.querystring("tipoImagen")
codigo_usu=session("codigo_usu")

pID=""
pPag=""
pNodo=""

'Asignar codigo_us del rector si entra como rcumpa para hoja de vida
if int(session("codigo_apl"))=6 and int(codigo_usu)=501 then
	codigo_usu=72
end if

Sub CrearMenu(ByVal codigo_apl,ByVal codigo_tfu,ByVal codigoRaiz_Men, ByVal TipoImagen,ByVal j)
		dim i,x
		dim ImagenMenu,EventoMenu,TextoMenu,idPadre,EstadoMenu
		dim rsMenu
		x=0
		
		Set rsMenu=Obj.Consultar("ConsultarAplicacionUsuario","FO","11",codigo_apl,codigo_tfu,codigoRaiz_men)
			
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
			'Verificar si el estilo gral es íconos pequeños
			'====================================================================
			if (TipoImagen="iconosel_men" and trim(codigoRaiz_men)="0") then
				TipoImagen="iconosel_men" 'Carga el campo de íconos de 16x16
			end if
			
			'====================================================================
			'Verificar imágen del menú, caso contrario colocar una por defecto
			'====================================================================
			if rsMenu("total_men")>0 then
				ImagenMenu="NodoCerrado.GIF" 'Carga el campo de íconos de 16x16
			elseif (rsMenu(tipoImagen)="" or IsNull(rsMenu(tipoImagen))=true) then
				ImagenMenu="menu.gif"
			end if
		
			'====================================================================
			'Verificar enlace del menú y determinar si es padre o Hijo
			'====================================================================
			if rsMenu("total_men")>0 then
				EventoMenu="onclick=""ExpandirNodo(Mnu" & rsMenu("codigo_men") & ")"""
				if pID="" and pPag="" then
					pNodo=rsMenu("codigo_men")
				end if
				
			elseif rsMenu("enlace_men")<>"" or IsNull(rsMenu("enlace_men"))=false  then
				EventoMenu=" onClick=""AbrirMenu(spCarpeta" & rsMenu("codigo_men") & ",'" & rsMenu("enlace_men") & "')"""
				
				if pID="" and pPag="" then
					pID=rsMenu("codigo_men")	
					pPag=rsMenu("enlace_men")
				end if
				
			end if

			'====================================================================			
			'Verificar el ancho de la descripción del menú
			'====================================================================
			if len(rsMenu("descripcion_men"))>23 then
				TextoMenu=left(rsMenu("descripcion_men"),23) & "..."
				altmensaje=" tooltip='" & rsMenu("descripcion_men") & "'"
			else
				altmensaje=""
			end if
				
			'====================================================================
			'Ocultar menús hijo
			'====================================================================
			if (codigoRaiz_men=rsMenu("codigoRaiz_men")) then
				EstadoMenu="style=""display:none"""
			end if
			
			'====================================================================
			'Imprimir Fila del menú
			'====================================================================
			response.write  "<tr " & EstadoMenu & " id='Mnu" & idPadre & "'>"  & vbcrlf
									
			response.write "<td " & EventoMenu & ">"
			response.write  cadena & "<img hspace='0' vspace='0' border='0' name='arrImgCarpetas' id='imgCarpeta" & rsMenu("codigo_men") & "' src='../../images/menus/" & ImagenMenu & "' align=absbottom>&nbsp;" & _
																 "<span id='spCarpeta" & rsMenu("codigo_men") & "' " & altmensaje & ">" & TextoMenu & "</span>" & vbcrlf
			response.write "</td>" 								
			response.write  "</tr>" 

			x=x+1
													
			CrearMenu codigo_apl,codigo_tfu,rsMenu("codigo_men"),tipoImagen,x

			rsMenu.movenext 						
			
		next
end Sub
%>
<html>
<HEAD>
<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1">
<link rel="stylesheet" type="text/css" href="../../private/estilo.css">
<script language="JavaScript" src="../../private/funciones.js"></script>
<script language="JavaScript" src="../../private/tooltip.js"></script>
<STYLE>
<!--
	img 	{border:0px none;align:absbottom}
	span    {top: 0;cursor:hand }
-->
</STYLE>
<script type= "text/javascript" language="Javascript">
	function AbrirMenu(id,pagina)
	{
	 	var  menu = id.tooltip
	 	
	 	if (menu==undefined){
	 		menu=id.innerText
	 	}
	 	
		if (pagina!='about:blank' && pagina!="") {		
			if (pagina.indexOf('?')==-1){//Si no encuentra una referencia
				pagina=pagina + '?menu=' + menu + "&id=<%=codigo_usu%>"
			}
			else{
				pagina=pagina + '&menu=' + menu + "&id=<%=codigo_usu%>"
			}
			ElegirRecurso(id)
			top.parent.frames[2].location.href="cargando.asp?rutapagina=" + pagina
		}
	}
	
	function ExpandirNodo(item)
	{
		var idMenu=0;
		var img=""
		if (item.length==undefined){
			idMenu=item.id
			img=document.getElementById("imgCarpeta" + idMenu.substring(3,idMenu.length));
			MostrarMenu(item,img)
        }
		else{
			idMenu=item[0].id
			img=document.getElementById("imgCarpeta" + idMenu.substring(3,idMenu.length));
	
			for(i=0;i<item.length;i++){
				MostrarMenu(item[i],img)
	     	}
		}
	}
	
	function MostrarMenu(Hijo,img)
	{
		if (Hijo.style.display!="none"){
			Hijo.style.display="none"
			img.src="../../images/menus/NodoCerrado.gif"
		}
		else{
			Hijo.style.display=""
			img.src="../../images/menus/NodoAbierto.gif"
        }
	}
</script>

<base target="_self">
</HEAD>
<body topmargin="0">
<input type="hidden" value="<%=codigo_usu%>" id="txtcodigo_usu">
<%
		set obj=server.CreateObject("PryUSAT.clsAccesodatos")
			obj.Abrirconexion
			response.write  "<table cellpadding=2 cellspacing=0 border=0 width='100%' heigth='100%'>" & vbcrlf
				CrearMenu session("codigo_apl"),session("codigo_tfu"),codigo_Men,tipoimagen,0
			response.write  "</table>"
			obj.CerrarConexion
		set obj=nothing				

	if pID<>"" and session("codigo_apl")>1 then
		texto="<script language='javascript'>AbrirMenu(spCarpeta" & pID & ",'" & pPag & "');"
			if pNodo<>"" then
				texto=texto & "ExpandirNodo(Mnu" & pNodo & ")"
			end if
		texto=texto & "</script>"
		response.write texto
	end if
%>
</body>
</html>