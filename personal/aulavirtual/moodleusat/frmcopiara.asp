<!--#include file="../../../funciones.asp"-->
<%
if session("codigo_usu")="" then response.write "<script>top.location.href='../../../tiempofinalizado.asp'</script>"

modo=request.querystring("modo")
idcursovirtual=request.querystring("idcursovirtual")
codigo_tre=request.querystring("codigo_tre")
idtabla=request.querystring("idtabla")

'****************************************************
'Obtener los contenidos del curso virtual
'****************************************************
Sub CrearTematica(ByVal idcursovirtual,ByVal codigo_tre,ByVal idtabla,ByVal codigo_ccv,ByVal j)
		dim i,x
		dim ImagenMenu,TextoMenu,idPadre
		dim rsContenido
		x=0
		
		Set rsContenido=obj.Consultar("DI_ConsultarTematicaDisponibleCopiar","FO",idcursovirtual,codigo_tre,idtabla,codigo_ccv)
		
		for i=1 to rsContenido.recordcount
			cadena=""
			
			'Genera espacios para jerarquía
			for x=1 to j
				cadena=cadena & "..." ' incluir imagen en blanco
			next
					
			'====================================================================
			'Almacenar variables de campos
			'====================================================================
			TextoMenu=rsContenido("titulo_ccv")
								
			'====================================================================
			'Imprimir Fila del menú
			'====================================================================
			response.write "<option value=""" & rsContenido("codigo_ccv") &""">"
			response.write  cadena & TextoMenu 
			response.write "</option>" & vbcrlf

			x=x+1
													
			CrearTematica idcursovirtual,codigo_tre,idtabla,rsContenido("codigo_ccv"),x

			rsContenido.movenext 						
			
		next
end Sub

Set Obj= Server.CreateObject("aulaVirtual.clsAccesoDatos")
	obj.AbrirConexion
		Set rsCurso=obj.Consultar("ConsultarCursoVirtual","FO",11,session("codigo_usu"),0,0)
		
		if Not(rsCurso.BOF and rsCurso.EOF) then
			HayReg=true
		end if
	obj.CerrarConexion
Set Obj=nothing

if idcursovirtual="" then idcursovirtual=session("idcursovirtual")
%>
<html xmlns="http://www.w3.org/1999/xhtml">

<head>
<meta http-equiv="Content-Language" content="es" >
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252" >
<title>Copiar recursos...</title>
<link rel="stylesheet" type="text/css" href="../../../private/estiloaulavirtual.css">
<script type="text/javascript" language="Javascript">
function BuscarTematica(obj)
{
var pagina="frmcopiara.asp?modo=<%=modo%>&idtabla=<%=idtabla%>&codigo_tre=<%=codigo_tre%>&idcursovirtual=" + obj.value

location.href="cargando.asp?rutapagina=" + pagina
}

function ValidarEnvio(frm)
{
	if (frm.cbocodigo_ccv.value==""){
		alert("Debe elegir a dónde se copiará el recurso seleccionado")
		frm.cbocodigo_ccv.focus()
		return(false)
	}
	else{
		frm.submit()
	}
}
</script>
</head>
<body style="background-color: #EEEEEE">
<%if modo="M" then%>
<p class="e4">Mover recursos</p>
<%else%>
<p class="e4">Copiar recursos</p>
<%end if%>
<form name="frmTemas" method="post" action="procesar_ccv.asp?accion=MoverContenidoTematico&codigo_tre=<%=codigo_tre%>&idtabla=<%=idtabla%>&modo=<%=modo%>">
<%if HayReg=true then%>
<table style="width: 100%;height:90%" id="table1">
	<tr height="5%">
		<td style="width: 15%"><b>Curso</b></td>
		<td style="width: 85%">
<select id="cboidcursovirtual" name="cboidcursovirtual" class="cajas" onchange="BuscarTematica(this)">
   <%  do while not rsCurso.eof%>
  		<option value="<%=rsCurso("idcursovirtual")%>" <%if cdbl(rsCurso("idcursovirtual"))=cdbl(idcursovirtual) then response.write("SELECTED") end if%>><%=rsCurso("titulocursovirtual")%></option>
	  	<%rsCurso.movenext
	  loop
  Set rsCurso=nothing%>
  </select>
		
		</td>
	</tr>
	<tr height="10%" valign="top">
	<%
	if idcursovirtual<>"" then
		Set Obj= Server.CreateObject("aulaVirtual.clsAccesoDatos")
			obj.AbrirConexion
	%>		
	<td colspan="2" class="boton">&nbsp; Diseño de temas</td>
	</tr>
	<tr height="85%">
		<td colspan="2">
		<select style="height:100%;width:100%" name="cbocodigo_ccv" class="cajas" multiple>
		<option value="0">[Bloque principal]</option>
		<%
		call CrearTematica(idcursovirtual,codigo_tre,idtabla,0,0)
		%>
		</select>
		</td>
	</tr>
			<%obj.CerrarConexion
		Set Obj=nothing
	%>
	<tr height="5%">
		<td colspan="2" align="right" class="rojo">
		Marque a dónde se copiará el recurso seleccionado</td>
	</tr>
	<tr height="5%">
		<td colspan="2" align="center">
		<input name="cmdGuardar" type="button" value="Guardar" class="guardar" onclick="ValidarEnvio(frmTemas)">
		<input name="cmdCancelar" type="button" value="Cerrar" class="salir" onclick="window.close()"></td>
	</tr>
	<%end if%>
</table>
<%else%>
	<h5 class="sugerencia">&nbsp;&nbsp;&nbsp; No se han encontrado cursos virtuales compatibles con este diseño</h5>
<%end if%>
</form>
</body>

</html>