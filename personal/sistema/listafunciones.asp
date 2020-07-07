<!--#include file="../../funciones.asp"-->
<%
dim codigo_men
dim obj
dim codigo_apl

modo=request.querystring("modo")
codigo_apl=request.querystring("codigo_apl")
codigo_tfu=request.querystring("codigo_tfu")
codigo_men=request.querystring("codigo_men")
if codigo_men="" then codigo_men=0
if codigo_tfu="" then codigo_tfu=0
if modo="" then modo="agregarmenufuncionusuario"

Sub CrearMenu(ByVal codigo_apl,ByVal codigoRaiz_Men,codigo_tfu,ByVal j)
		dim i,x
		dim ImagenMenu,EventoMenu,TextoMenu,idPadre,EstadoMenu
		dim rsMenu
		x=0
		
		Set rsMenu=Obj.Consultar("ConsultarAplicacionUsuario","FO","16",codigo_apl,codigoRaiz_men,codigo_tfu)
				
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
			'Verificar tipo de menú
			'====================================================================
			if rsMenu("total_men")>0 then
				ImagenMenu="mas.GIF" 'Carga el campo de íconos de 16x16
			else
				ImagenMenu="menu.gif"
			end if
			
			'====================================================================
			'Verificar enlace del menú y determinar si es padre o Hijo
			'====================================================================
			if rsMenu("total_men")>0 then
				EventoMenu="onclick=""ExpandirNodo(Mnu" & rsMenu("codigo_men") & ")"""
				cadena=cadena & "<img class='imagen' hspace='0' vspace='0' border='0' name='arrImgCarpetas' id='imgCarpeta" & rsMenu("codigo_men") & "' src='../../images/menus/" & ImagenMenu & "' align='middle' " & EventoMenu & ">&nbsp;"
			'elseif rsMenu("enlace_men")<>"" or IsNull(rsMenu("enlace_men"))=false  then
			'	EventoMenu=" onClick=""AbrirMenu(spCarpeta" & rsMenu("codigo_men") & ",'" & rsMenu("enlace_men") & "')"""
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
			response.write "<td>"
			response.write  cadena & "<input type='checkbox' value='" & rsMenu("codigo_men") & "' name='chkmenu' Onclick='VerificaCheckMarcados(frmfuncion.chkmenu,frmfuncion.cmdGrabar)' " & rsMenu("Marca") & ">" & TextoMenu & vbcrlf
			response.write "</td>" 								
			response.write  "</tr>" 

			x=x+1
													
			CrearMenu codigo_apl,rsMenu("codigo_men"),codigo_tfu,x

			rsMenu.movenext 						
			
		next
end Sub

%>
<html>
<head>
<meta http-equiv="Content-Language" content="es">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<title>funciones del modulo</title>
<link rel="stylesheet" type="text/css" href="../../private/estilo.css">
<script type="text/javascript" language="JavaScript" src="../../private/funciones.js"></script>
<script type="text/javascript" language="JavaScript">
	function ExpandirTodo()
	{
		var arrFilas = document.all.tblMenus.getElementsByTagName('tr')
		var modo=""
		var tipoimagen="menos.gif"
		
		if (frmfuncion.chkExpandir.checked==false){
			modo="none"
			tipoimagen="mas.gif"
		}
		
		for (var c = 0; c < arrFilas.length; c++){
			if (modo=="none" && arrFilas[c].id=="Mnu0"){
				arrFilas[c].style.display=""
			}
			else{
				arrFilas[c].style.display=modo
			}
			//arrImgCarpetas[c].src="../../images/menus/menos.gif"
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
			img.src="../../images/menus/mas.gif"
		}
		else{
			Hijo.style.display=""
			img.src="../../images/menus/menos.gif"
        }
	}

	function validartipofuncion()
	{
		if(confirm("¿Está completamente seguro que desea Guardar los cambios?")==true)
			{return(true)}
		else
			{return(false)}
	}
	
	function MarcarTodos()
	{
		frmfuncion.cmdGrabar.disabled=true
		VerificaSeleccion(frmfuncion.chkSeleccionar,frmfuncion.chkmenu);
		if (frmfuncion.chkSeleccionar.checked==true){
			frmfuncion.cmdGrabar.disabled=false
		}
	}

	function CargarMenuPermisos()
	{
		location.href="listafunciones.asp?codigo_tfu="+ frmfuncion.cbocodigo_tfu.value + "&codigo_apl=<%=codigo_apl%>"
	}
</script>
</head>
<body bgcolor="#EEEEEE">
<form name="frmfuncion" method="post" onSubmit="return validartipofuncion()" ACTION="procesar.asp?accion=<%=modo%>&codigo_apl=<%=codigo_apl%>">
<%
if modo="agregarmenufuncionusuario" then
	set obj=server.CreateObject("PryUSAT.clsAccesodatos")
		obj.Abrirconexion
			Set rsTipofuncion=Obj.Consultar("ConsultarAplicacionUsuario","FO","5",codigo_apl,0,0)
		
%>	
<table border="0" cellpadding="2" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%" <%if codigo_tfu>0 then%>height="98%"<%end if%>>
  <tr>
    <td width="20%" class="azul" height="5%" valign="top"><b>Tipo de Función</b></td>
    <td height="5%" width="80%" align="right">
	<%call llenarlista("cbocodigo_tfu","CargarMenuPermisos()",rsTipofuncion,"codigo_tfu","descripcion_tfu",codigo_tfu,"Seleccione el tipo de función","","")%>
	</td>
  </tr>
  <%if codigo_tfu>0 then%>
  <tr>
    <td width="100%" colspan="2" height="90%" valign="top">
	<table cellpadding="3" cellspacing="0" style="border-collapse: collapse;" width="100%" height="100%" bgcolor="white" class="contornotabla_azul">
      <tr>
        <td width="3%" colspan="2" height="100%">
        <div id="listadiv" style="height:100%">
		<%
			response.write  "<table cellpadding=1 cellspacing=0 border=0 width='100%' heigth='100%' id='tblMenus'>" & vbcrlf
				CrearMenu codigo_apl,codigo_Men,codigo_tfu,0
			response.write  "</table>"
		%>
		</div>
       </td>
      </tr>
     </table>
    </td>
  </tr>
  <%end if%>  
  <tr>
  	<td height="5%" width="100%" colspan="2">
  	<table width="100%">
		<tr class="etiqueta">
			<%if codigo_tfu>0 then%>
			<td><input type="checkbox" name="chkSeleccionar" Onclick="MarcarTodos()" value="0">Seleccionar todo</td>
			<td><input type="checkbox" name="chkExpandir" Onclick="ExpandirTodo()" value="0">Expandir todo</td>
			<%end if%>
			<td align="right">
		  	<input type="button" class="usatnuevo" value=" Asignar función" NAME="cmdModificar" OnClick="location.href='listafunciones.asp?modo=agregarfuncionusuario&codigo_apl=<%=codigo_apl%>'">
		  	<%if codigo_tfu>0 then%>
		    <input type="submit" class="usatguardar" value="Guardar" NAME="cmdGrabar" disabled="true">
		    <%end if%>
			</td>
		</tr>
	</table>
  	</td>
  </tr>
</table>
<%
		obj.CerrarConexion
	set obj=nothing
else
	Set Obj= Server.CreateObject("PryUSAT.clsAccesoDatos")
	obj.AbrirConexion
		Set rsTipofuncion=Obj.Consultar("ConsultarAplicacionUsuario","FO","4",codigo_apl,0,0)
	obj.CerrarConexion
	Set Obj=nothing
	j=0
	%>
	<table cellpadding="3" cellspacing="0" style="border-collapse: collapse;" width="100%" height="100%">
	<tr>
		<td width="100%" height="5%" class="usattitulousuario">
		Permisos según funciones de usuarios
		</td>
	</tr>
  	<tr>
        <td width="100%" height="95%" bgcolor="white" class="contornotabla_azul">
        <div id="listadiv" style="height:100%">
        <table width="100%" height="100%">
	      <%Do while not rsTipofuncion.EOF
	      	j=j+1
	      %>
	      <tr>
	        <td width="3%"><input type="hidden" name="codigo_tfu<%=j%>" value="<%=rsTipofuncion("codigo_tfu")%>"><input type="checkbox" value="<%=rsTipofuncion("codigo_tfu")%>" name="chkfuncion<%=j%>" <%=rsTipofuncion("Marca")%>>&nbsp;</td>
	        <td width="97%"><%=rsTipofuncion("descripcion_tfu")%>&nbsp;</td>
	      </tr>
	      <%
	      	rsTipofuncion.movenext
	      Loop
	      %>
	    </table>
	    </div>
	    </td>
	</tr>
	<tr>
		<td width="100%" height="5%" align="right">
			<input type='hidden' name='nocheck' value='<%=j%>'>
	    	<input type="submit" class="guardar" value="Guardar" NAME="cmdGrabar">
	    	<input type="button" class="salir" value="Cancelar" NAME="cmdCancelar" onClick="location.href='listafunciones.asp?codigo_apl=<%=codigo_apl%>&modo=agregarmenufuncionusuario'">
		</td>
	</tr>
    </table>
<%end if%>
</form>
</body>

</html>