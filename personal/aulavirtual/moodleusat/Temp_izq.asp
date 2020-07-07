<!--#include file="../clscalendario.asp"-->
<%
if session("idcursovirtual")="" then response.redirect("../../../tiempofinalizado.asp")

Set Obj= Server.CreateObject("AulaVirtual.clsAccesoDatos")
	obj.AbrirConexion
		Set rsMenu=obj.Consultar("DI_ConsultarMenuCursoVirtual","FO",session("codigo_apl"),session("codigo_tfu"),session("idcursovirtual"),session("codigo_usu"))
	obj.CerrarConexion
Set Obj=nothing
%>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<title>Menu izquierdo</title>
<base target="menuizq">
<link rel="stylesheet" type="text/css" href="../../../private/estiloaulavirtual.css">
<script type="text/javascript" language="JavaScript" src="../../../private/funcionesaulavirtual.js"></script>
<script type="text/javascript" language="javascript">
	function ResaltarItemMenu(op,fila)
	{
		fila.style.cursor="hand"
		if(op==1)
			{fila.className="TextoResalte"}
		else
			{fila.className="TextoNormal"}	
	}
</script>
<style type="text/css">
<!--
.TextoNormal {background-color: #E9F3FC }
.titulomenu  {background-color: #A9C1D7 }
.TextoResalte {
	background-color: #FBF5D2;
}
.MenuCurso {
	border-collapse: collapse;
	border: 1px solid #395ACC;
	background-color: #E9F3FC;
}
-->
</style>
</head>
<body onload="marcarColorfila('1','#FBF5D2','cargando.asp?rutapagina=tematicacurso.asp')">
<table cellpadding="3" cellspacing="0" width="100%" id="tblMenu" class="MenuCurso">
  <tr>
    <td width="100%" class="titulomenu" align="center"><b>&nbsp;Menú de Opciones</b></td>
  </tr>
<%
If Not(rsMenu.BOF and rsMenu.EOF) then
  
	Do while Not rsMenu.EOF
		i=i+1
%>
  <tr onMouseOver="ResaltarItemMenu(1,this,'S','#DEE0C5')" onMouseOut="ResaltarItemMenu(0,this,'S','#DEE0C5')" onClick="marcarColorfila('<%=i%>','#FBF5D2','<%=rsMenu("enlace_men")%>')">
    <td  width="100%"><img border="0" src="<%=rsMenu("icono_men")%>">&nbsp;<%=rsMenu("descripcion_men")%></td>
  </tr>
  	<%
  		rsMenu.movenext
  	Loop
  	rsMenu.close
	Set Menu=nothing
End if
%>
  <tr onMouseOver="ResaltarItemMenu(1,this,'S','#DEE0C5')" onMouseOut="ResaltarItemMenu(0,this,'S','#DEE0C5')" onClick="top.location.href='../cerrarvisitacurso.asp'">
    <td width="100%"><img border="0" src="../../../images/salir.gif">Regresar</td>
  </tr>
  <tr onMouseOver="ResaltarItemMenu(1,this,'S','#DEE0C5')" onMouseOut="ResaltarItemMenu(0,this,'S','#DEE0C5')" onClick="cerrarSistema('../../../cerrar.asp?Decision=Si')">
    <td width="100%"><img border="0" src="../../../images/eliminar.gif"> Salir</td>
  </tr>
</table>
</body>
</html>