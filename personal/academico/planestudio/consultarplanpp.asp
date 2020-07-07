<!--#include file="../../../funciones.asp"-->
<%
call Enviarfin(session("codigo_usu"),"../../../")

Set objPlan=Server.CreateObject("PryUSAT.clsAccesoDatos")
	objPlan.AbrirConexion
	Set rsDatos= objPlan.Consultar("ConsultarDatosProgramaEspecial","FO",2, session("codigo_tfu"),session("codigo_usu") , 0, 0)
	objPlan.CerrarConexion
Set objPlan=nothing

codigo_pes=0
codigo_pes=request.querystring("codigo_pes")
modo=request.querystring("modo")
%>
<html>
<head>
<meta http-equiv="Content-Language" content="es">
<meta name="GENERATOR" content="Microsoft FrontPage 5.0">
<meta name="ProgId" content="FrontPage.Editor.Document">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<title>Seleccione el curso que desea agregar a la matrícula</title>
<link rel="stylesheet" type="text/css" href="../../../private/estilo.css">
<script language="JavaScript" src="../../../private/funciones.js"></script>
<script language="JavaScript" src="private/validarplan.js"></script>
</head>
<body>
<table cellpadding="2" cellspacing="0" style="border-collapse: collapse; border: 0px solid #C0C0C0; " bordercolor="#111111" width="100%">
  <tr>
    <td width="100%" height="3%" colspan="2">
    <table border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%" id="AutoNumber2">
      <tr>
        <td width="81%" class="usatTitulo">Planes de Estudio</td>
        <td width="19%" align="right">
    <input type="button" value=" Imprimir..." name="cmdBuscar" class="imprimir" id="cmdexportar" onClick="window.print()"></td>
      </tr>
    </table>
    </td>
  </tr>
  <tr>
    <td width="26%" height="3%">Plan de Estudio</td>
    <td width="74%" height="3%">
    <%
	call llenarlista("cbocodigo_pes","location.href='consultarplanpp.asp?modo=R&codigo_pes=' + this.value",rsDatos,"codigo_pes","descripcion_pes",codigo_pes,"Seleccione el Plan de Estudios","","")
    %>
    </td>
  </tr>
  </table>
<br>
<%
if int(codigo_pes)<>0 and modo="R" then
	Set objPlan=Server.CreateObject("PryUSAT.clsAccesoDatos")
		objPlan.AbrirConexion
		Set rsCursos= objPlan.Consultar("ConsultarCursoPlan","FO",3,codigo_pes,25,0)
		objPlan.CerrarConexion
	Set objPlan=nothing

	if Not (rsCursos.BOF and rsCursos.EOF) then
		HayReg=true
	end if

if HayReg=true then
	Ciclo=1
	i=0:j=0
	ImprimirCabezera=false

Do while not rsCursos.eof
	i=i+1

	TGC=TGC+rsCursos("creditos_cur")
	TC=TC+ rsCursos("creditos_cur")
	HT=HT+ rsCursos("horasteo_cur")
	HP=HP+ rsCursos("horaspra_cur")
	HL=HL+ rsCursos("horaslab_cur")
	HA=HA+ rsCursos("horasase_cur")
	TH=TH+ rsCursos("totalhoras_cur")
			
		If trim(rsCursos("ciclo_cur"))<>trim(Ciclo) or (Ciclo=1 and i=1)then
			if Ciclo<>"1" then j=j+1
			Ciclo=rsCursos("ciclo_cur")
			'TC=0
			'HT=0
			'HP=0
			'HL=0
			'HA=0
			'TH=0
			
			if j mod 2 then
				response.write "<br class=""SaltoDePagina"">"
			end if
%>
<br>
<table cellpadding="2" cellspacing="0" style="border-collapse: collapse" width="100%" border="1" bordercolor="#808080">
    <tr>
    <td width="100%" height="5%" colspan="10" bgcolor="#E6E6FA" class="usattitulo" align="center">Ciclo <%=ConvRomano(Ciclo)%></td>
    </tr>
    <tr class="etabla">
    <td width="5%" height="5%">Tipo</td>
    <td width="10%" height="5%">Código</td>
    <td width="30%" height="5%">Asignatura</td>
    <td width="5%">Crd.</td>
    <td width="5%">HT</td>
    <td width="5%">HP</td>
    <td width="5%">HL</td>
    <td width="5%">HA</td>
    <td width="5%">TH</td>
    <td width="25%">Pre-Requisito</td>
    </tr>
    <%
    	end if
	%>
    <tr>
    <td width="5%" height="5%"><%=iif(rsCursos("electivo_cur")=0,"Obligatorio","Electivo")%>&nbsp;</td>
    <td width="10%" height="5%"><%=rsCursos("identificador_cur") & "/" & rsCursos("ciclo_cur") %>&nbsp;</td>
    <td width="30%" height="5%"><%=rsCursos("nombre_cur")%>&nbsp;</td>
    <td width="5%" align="center"><%=rsCursos("creditos_cur")%>&nbsp;</td>
    <td width="5%" align="center"><%=rsCursos("horasteo_cur")%>&nbsp;</td>
    <td width="5%" align="center"><%=rsCursos("horaspra_cur")%>&nbsp;</td>
    <td width="5%" align="center"><%=rsCursos("horaslab_cur")%>&nbsp;</td>
    <td width="5%" align="center"><%=rsCursos("horasase_cur")%>&nbsp;</td>
    <td width="5%" align="center"><%=rsCursos("totalhoras_cur")%>&nbsp;</td>
    <td width="25%" class="piepagina"><%=rsCursos("requisitos_cur")%>&nbsp;</td>
    </tr>
    <%
    'response.write j
   	'If j=0 then%>
    <!--<tr>
    <td width="45%" height="5%" colspan="3" align="right">TOTAL</td>
    <td width="5%" align="center"><%=TC%>&nbsp;</td>
    <td width="5%" align="center"><%=HT%>&nbsp;</td>
    <td width="5%" align="center"><%=HP%>&nbsp;</td>
    <td width="5%" align="center"><%=HL%>&nbsp;</td>
    <td width="5%" align="center"><%=HA%>&nbsp;</td>
    <td width="5%" align="center"><%=TH%>&nbsp;</td>
    <td width="25%">&nbsp;</td>
    </tr>
	-->
    <%'end if
    rsCursos.movenext
    Loop%>
</table>
  <%
    Set rscursos=nothing
end if%>
<b><P class="azul">TOTAL DE CRÉDITOS DEL PLAN DE ESTUDIO: <%=TGC%></P></b>
<%end if%>
</body>
</html>