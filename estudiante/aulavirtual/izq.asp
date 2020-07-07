<%
Set Obj= Server.CreateObject("AulaVirtual.clsDatAplicacion")
	ArrDatos=Obj.ConsultarAplicacionUsuario("9",session("codigo_apl"),session("codigo_tfu"),"")
Set Obj=nothing
%>
<html>
<head>
<meta name="GENERATOR" content="Microsoft FrontPage 5.0">
<meta name="ProgId" content="FrontPage.Editor.Document">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<title>Menu izquierdo</title>
<base target="menuizq">
<link rel="stylesheet" type="text/css" href="../../private/estiloaulavirtual.css">
<script language="JavaScript" src="../../private/funcionesaulavirtual.js"></script>
<style>
<!--
td           { border-left-width: 1; border-right-width: 1; border-bottom: 1px solid #808080 }
-->
</style>
<script>
function ModificarCurso()
{
AbrirPopUp("cursovirtual/frmadministrar.asp?accion=modificarcurso&codigo_apl=<%=session("codigo_apl")%>&idcursovirtual=<%=session("idcursovirtual")%>&titulocurso=<%=session("titulocursovirtual")%>&codigo_tfu=<%=session("codigo_tfu")%>","450","700")
}
</script>
</head>

<body class="menuizquierdo" topmargin="0" leftmargin="0">

<table border="0" cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%" id="tblMenu">
  <tr>
    <td width="100%" class="etabla">&nbsp;Menú de Opciones</td>
  </tr>
  <%If IsEmpty(ArrDatos)=false then
  for i=lbound(Arrdatos,2) to Ubound(Arrdatos,2)%>
  <tr onMouseOver="Resaltar(1,this,'S','#DEE0C5')" onMouseOut="Resaltar(0,this,'S','#DEE0C5')" onClick="marcarColorfila('<%=i+1%>','','<%=arrdatos(1,i)%>')">
    <td width="100%"><img border="0" src="<%=arrdatos(2,i)%>">&nbsp;<%=arrdatos(0,i)%></td>
  </tr>
  <%next
  end if
  if session("tipo_apl")=1 then%>
  <tr onMouseOver="Resaltar(1,this,'S','#DEE0C5')" onMouseOut="Resaltar(0,this,'S','#DEE0C5')" onClick="top.location.href='cerrarvisitacurso.asp'">
    <td width="100%"><img border="0" src="../../images/salir.gif">Regresar</td>
  </tr>
  <%end if%>
  <tr onMouseOver="Resaltar(1,this,'S','#DEE0C5')" onMouseOut="Resaltar(0,this,'S','#DEE0C5')" onClick="cerrarSistema('../../cerrar.asp?Decision=Si')">
    <td width="100%"><img border="0" src="../../images/cerrar.gif"> Salir</td>
  </tr>
  </table>

</body>

</html>