<%
if session("mododesarrollo")="N" then
    'Set Obj= Server.CreateObject("AulaVirtual.clsAccesoDatos")
	'obj.AbrirConexion
	'	Set rsMenu=obj.Consultar("DI_ConsultarMenuCursoVirtual","FO",session("codigo_apl"),session("codigo_tfu"),session("idcursovirtual"),session("codigo_usu"))
	'obj.CerrarConexion
    'Set Obj=nothing   
    'ArrDatos=rsMenu.Getrows
'else
    Set Obj= Server.CreateObject("AulaVirtual.clsDatAplicacion")
	    ArrDatos=Obj.ConsultarAplicacionUsuario("9",session("codigo_apl"),session("codigo_tfu"),"")
    Set Obj=nothing
end if
%>

<html>
<head>
<meta name="GENERATOR" content="Microsoft FrontPage 5.0">
<meta name="ProgId" content="FrontPage.Editor.Document">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<title>Menú superior</title>
<link rel="stylesheet" type="text/css" href="../../private/estiloaulavirtual.css">
<script language="JavaScript" src="../../private/funcionesaulavirtual.js"></script>
<script>
function ModificarCurso()
{
marcarColorfila('1','','')
AbrirPopUp("cursovirtual/frmadministrar.asp?accion=modificarcurso&codigo_apl=<%=session("codigo_apl")%>&idcursovirtual=<%=session("idcursovirtual")%>&titulocurso=<%=session("titulocursovirtual")%>&codigo_tfu=<%=session("codigo_tfu")%>","450","700")
}

function ResaltarOpcion(op,fila)
{
	fila.style.cursor="hand"
	
	if(op==1)
		{fila.className="OpcionElegida"}
	else
		{fila.className="OpcionNoElegida"}
}

function MarcarOpcionElegida(numfila,colorfila,pagina)
{
	var tbl = document.getElementById("tblMenu")
	var ArrFilas = tbl.getElementsByTagName('tr')
	var tfilas = ArrFilas.length
	for (var c = 0; c < tfilas; c++){
		var Fila=ArrFilas[c]
		var Celda=Fila.getElementsByTagName('td')
		
		if(numfila==c){
			Fila.style.backgroundColor = colorfila
			Celda[0].style.color = "blue"
			//Celda[0].style.borderRight="0px"
		}
		else {
			Fila.style.backgroundColor = ""
			Celda[0].style.color = "black"
			//Celda[0].style.borderRight="1px SOLID #808080"
		}
	}
   
   if (pagina!=""){
	window.parent.frames[1].location.href=pagina
   }
}

</script>
<style type="text/css">
.titulocurso {
	font-weight: bold;
	color: #800000;
	font-family: Arial;
	font-size: 20px;
}
.OpcionElegida{
	border: 1px solid #808080;
	background-color: #F7EEB3
}
.OpcionNoElegida{
	border: 0px;
	background-color: #FFFFFF
}
</style>
</head>
<body style="border-style: none none solid none; border-width: 1px; border-color: #808080">
<h3 class="titulocurso"><%=session("nombrecursovirtual")%>&nbsp;</h3>
<%
item=1
if session("codigo_tfu")=1 AND session("mododesarrollo")="N" then
	item=2
%>
<table id="tblMenu">
  <tr>
    <td onMouseOver="ResaltarOpcion(1,this)" onMouseOut="ResaltarOpcion(0,this)" onClick="ModificarCurso()"><img src="../../images/disenar.gif">&nbsp;Administrar</td>
<%end if%>
  <%If IsEmpty(ArrDatos)=false then
  for i=lbound(Arrdatos,2) to Ubound(Arrdatos,2)%>
    <td onMouseOver="ResaltarOpcion(1,this)" onMouseOut="ResaltarOpcion(0,this)" onClick="MarcarOpcionElegida('<%=i+item%>','#808080','<%=arrdatos(1,i)%>')"><img border="0" src="<%=arrdatos(2,i)%>">&nbsp;<%=arrdatos(0,i)%></td>
 <%next%>
    <td onMouseOver="ResaltarOpcion(1,this,'S','#DEE0C5')" onMouseOut="ResaltarOpcion(0,this,'S','#DEE0C5')" onClick="location.href='cerrarvisitacurso.asp'"><img border="0" src="../../images/salir.gif">Regresar</td>
 <%end if%>
  <!--
  <tr onMouseOver="Resaltar(1,this,'S','#DEE0C5')" onMouseOut="Resaltar(0,this,'S','#DEE0C5')" onClick="cerrarSistema('../../cerrar.asp?Decision=Si')">
    <td width="100%"><img border="0" src="../../images/cerrar.gif"> Salir</td>
  </tr>
  -->
  </tr>
</table>
</body>
</html>