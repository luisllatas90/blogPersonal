<%
Dim rsContenido
'****************************************************
'Obtener los datos del curso
'****************************************************
codigo_tre=request.querystring("codigo_tre")
idtabla=request.querystring("idtabla")
campo2="Observaciones"
campo3="Realizado por"
campo4="Acciones"

select case codigo_tre
	case "S": campo2="Fecha Salida":campo3="Registrado por":campo4=""
	case "F": campo2="Respuestas":campo3="Empezado por":campo4="Último mensaje"
end select

'****************************************************
'Obtener los contenidos del curso virtual
'****************************************************
Sub CrearArbolContenido(Byval codigo_tre,ByVal idcursovirtual,ByVal codigo_tfu,ByVal codigo_usu,ByVal codigo_rec,ByVal refId,ByVal j)
		dim i,x
		dim ImagenMenu,TextoMenu,idPadre
		dim rsContenido
		x=0
		
		select case codigo_tre
			case "S"
				Set rsContenido=obj.Consultar("ConsultarChat","FO",5,codigo_usu,codigo_rec,codigo_tfu)
			case "T"		
				Set rsContenido=obj.Consultar("DI_ConsultarTareasUsuario","FO",idcursovirtual,codigo_tfu,codigo_usu,codigo_rec,refId)
			case "F"
				Set rsContenido=obj.Consultar("ConsultarForo","FO",8,idcursovirtual,codigo_rec,refId)
			case "E"
				Set rsContenido=obj.Consultar("ConsultarEvaluacion","FO",18,codigo_usu,codigo_rec,codigo_tfu)			
		end select
		
		for i=1 to rsContenido.recordcount
			cadena=""
			
			'Genera espacios para jerarquía
			for x=1 to j
				'cadena=cadena & "..." ' incluir imagen en blanco
				cadena=cadena & "<img border=""0"" src='../../../images/blanco.gif'>"
			next
						
			'====================================================================
			'Almacenar variables de campos
			'====================================================================
			if codigo_tre="T" then
				archivo=rsContenido("archivo")
				if archivo="" or isnull(archivo)=true then
					ImagenMenu="mensaje.gif"
					TextoMenu=rsContenido("fechareg")
				else
					ImagenMenu="ext/" & right(archivo,3) & ".gif"
					TextoMenu="<a target='_blank' href='tar/descargar.asp?idtareausuario=" & rsContenido("idtareausuario") & "'>" & rsContenido("fechareg") & "</a>"
					'if rsContenido("obs")<>"" then
					'	TextoMenu=TextoMenu & "<br><br>" & replace(rscontenido("obs"),chr(13),"<br>") & "<br><br>"
					'end if
				end if
			elseif codigo_tre="F" then
				TextoMenu="<a href='for/detallemensaje.asp?idforo=" & rsContenido("idforo") & "&idforomensaje=" & rsContenido("idforomensaje") & "'>" & rsContenido("texto") & "</a>"
				ImagenMenu=rsContenido("icono")	
			else
				ImagenMenu=rsContenido("icono")
				TextoMenu=rsContenido("texto")			
			end if
			
			if isnull(rsContenido("obs"))=false then
				obs=replace(rscontenido("obs"),chr(13),"<br>")
			end if
								
			'====================================================================
			'Imprimir Fila del menú
			'====================================================================
			response.write  "<tr onMouseOver=""Resaltar(1,this,'S')"" onMouseOut=""Resaltar(0,this,'S')"">"  & vbcrlf
			response.write "<td width=""20%"" valign=""top"">"
			response.write  cadena & "<img hspace='0' vspace='0' border='0' name='arrImgCarpetas' src='../../../images/" & ImagenMenu & "' align=absbottom>&nbsp;" & TextoMenu	
			response.write "</td>" & vbcrlf
			response.write "<td valign=""top"">" & obs & "</td>" & vbcrlf
			response.write "<td valign=""top"">" & rsContenido("nombreusuario") & "</td>" & vbcrlf
			response.write "<td valign=""top"" align=center>" & rsContenido("accion") & "</td>" & vbcrlf
			response.write  "</tr>"  & vbcrlf

			x=x+1
			
			if (codigo_tre="T" or codigo_tre="F") then
				CrearArbolContenido codigo_tre,idcursovirtual,codigo_tfu,codigo_usu,codigo_rec,rsContenido(0),x
			end if

			rsContenido.movenext			
		next
end Sub

%>
<html xmlns="http://www.w3.org/1999/xhtml">

<head>
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<title>contenido del curso</title>
<link rel="stylesheet" type="text/css" href="../../../private/estiloaulavirtual.css">
<script type="text/javascript" language="JavaScript" src="../../../private/funcionesaulavirtual.js"></script>
<style type="text/css" fprolloverstyle>A:hover {color: #FF0000; text-decoration: underline; font-weight: bold}
</style>
<style type="text/css">
<!--
.franjasup   { border-left-width: 1; border-right-width: 1; border-top-width: 1; 
               background-color: #CCCCBB; color:#000080 }
.cajapregunta {
	border: 1px solid #808080;
	background-color: #FEFFE1;
	font-size: 12px;
	font-weight: bold;
	color: #000080
}
-->
</style>
<script type="text/javascript" language="javascript">
	function AccederRecurso(str1,str2)
	{
		var alto = 350
		var ancho= 620
		var pagina=""
		if('<%=codigo_tre%>'=='T'){
			if(str1=="H"){ //Realizar tarea por primera vez
				pagina="abrirrecurso.asp?accion=agregartareausuario&codigo_tre=TU&idtarea=<%=idtabla%>&idtareausuario=0"
			}
			
			if(str1=="U" && str2.value.indexOf('eliminar')==-1){ //Responder a tarea o comentarla
				pagina=str2.value
				str2.value=""
			}
			
			if (str1=="U" && str2.value.indexOf('eliminar')!=-1){
				if (confirm("¿Está completamente seguro que desea eliminar el trabajo seleccionado?")==true){
					window.location.href="cargando.asp?rutapagina=" + str2.value
					str2.value=""
					return
				}
			}
		}
		
		if ('<%=codigo_tre%>'=='E'){
			alto="650"
			ancho="750"
			if(str1=="H"){ //Realizar encuesta por primera vez
				pagina="enc/abrirevaluacion.asp?accion=iniciarencuesta&idevaluacion=<%=idtabla%>"
				document.all.tdestadorecurso.innerHTML="<h5><img src='../../../images/bloquear.gif'><br><span class=rojo>Ud. acaba de iniciar la encuesta. Recuerde que sólo puede ingresar una vez </span></h5>"
			}
			
			if(str1=="R" && str2.value.indexOf('eliminar')==-1){ //visualizar la encuesta
				//location.href=str2.value
				AbrirPopUp("cargando.asp?rutapagina=" + str2.value,"600","750","yes","yes","yes")
				str2.value=""
				return(false)
			}
			
			if (str1=="R" && str2.value.indexOf('eliminar')!=-1){
				if (confirm("¿Está completamente seguro que desea eliminar el trabajo seleccionado?")==true){
					window.location.href="cargando.asp?rutapagina=" + str2.value
					str2.value=""
					return
				}
			}
		}
		
		if('<%=codigo_tre%>'=='S'){
			alto="500"
			ancho="650"
			pagina="chat/abrirchat.asp?modo=A&idchat=<%=idtabla%>"
			document.all.tdestadorecurso.innerHTML="<h5><img src='../../../images/bloquear.gif'><br><span class=rojo>Ud. acaba de iniciar una sesión de chat</span></h5>"
		}
		
		if('<%=codigo_tre%>'=='F'){
			alto="550"
			ancho="680"
			pagina="for/frmmensaje.asp?accion=agregarforomensaje&idforo=<%=idtabla%>"
		}
		
		
		if (pagina!=""){
			var izq = (screen.width-ancho)/2
			var arriba= (screen.height-alto)/2
			window.open(pagina,"recursos","height=" + alto + ",width=" + ancho + "&,statusbar=yes,scrollbars=yes,top=" + arriba + ",left=" + izq + ",resizable=yes,toolbar=no,menubar=no");
		}
	}
	
	function Regresar()
	{
		location.href="cargando.asp?rutapagina=tematicacurso.asp"
	}
</script>
</head>
<body>
<%
Set Obj= Server.CreateObject("AulaVirtual.clsAccesoDatos")
obj.AbrirConexion
	Set rsRecurso=obj.Consultar("DI_ConsultarDetalleContenidoCursoVirtual","FO",session("idcursovirtual"),codigo_tre,idtabla,session("codigo_tfu"),session("codigo_usu"))
		
	If not(rsRecurso.BOF and rsRecurso.EOF) then
		titulo_ccv=rsRecurso("titulo")
		descripcion_ccv=rsRecurso("descripcion")
		duracion=formatdatetime(rsRecurso("fechainicio"),1) & " al " & formatdatetime(rsRecurso("fechafin"),1)
		nombreusuario=rsRecurso("nombreusuario")
		estadoacceso=rsRecurso("estadoacceso")
%>
<table class="contornotabla" border="0" cellpadding="3" cellspacing="0" style="border-collapse: collapse" width="100%">
  <tr class="franjasup">
    <td width="3%" height="30">
	<img alt="" src="../../../images/libroabierto.gif" width="15" height="15"></td>
    <td width="97%" height="30" style="text-align:left;" class="e4" colspan="2"><%=titulo_ccv%></td>
  </tr>
  <tr>
    <td width="3%">&nbsp;</td>
    <td width="10%" class="etiqueta">Registrado por</td>
    <td width="87%">:&nbsp;<%=nombreusuario%></td>
  </tr>
  <tr>
    <td width="3%">&nbsp;</td>
    <td width="10%" class="etiqueta">Duración</td>
    <td width="87%">:&nbsp;<%=duracion%></td>
  </tr>
  <%if descripcion_ccv<>"" then%> 
  <tr>
    <td width="3%">&nbsp;</td>
    <td width="94%" colspan="2">
	<%=replace(descripcion_ccv,chr(13),"<br>")%> 
    </td>
  </tr>
  <%end if
  if estadoacceso <>"" then%>  
  <tr>
    <td width="3%">&nbsp;</td>
    <td width="94%" colspan="2" align="center" id="tdestadorecurso">
		<%=estadoacceso%>&nbsp;    
    </td>
  </tr>
  <%end if%>
</table>  
<br>
    <%if estadoacceso="" or (codigo_tre="T" or codigo_tre="S" or codigo_tre="F" or codigo_tre="E") then%>
	<table border="1" cellpading="3" width="100%" style="border-collapse: collapse" bordercolor="#C0C0C0">
		<tr class="etabla" height="25px">
			<td width="20%">Fecha de registro</td>
			<td><%=campo2%>&nbsp;</td>
			<td><%=campo3%>&nbsp;</td>
			<td><%=campo4%>&nbsp;</td>
		</tr>
	    <%CrearArbolContenido codigo_tre,session("idcursovirtual"),session("codigo_tfu"),session("codigo_usu"),idtabla,0,0%>
	</table>
	<%end if%>
<%else%>
	<h5>No se ha encontrado el recurso seleccionado</h5>
<%
end if
obj.CerrarConexion
Set Obj=nothing
Set recurso=nothing
%>
	<p>
	   	<input name="cmdregresar" type="button" value="Regresar" class="salir" onclick="Regresar()">
	</p>
</body>
</html>