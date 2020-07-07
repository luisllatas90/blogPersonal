<!--#include file="../NoCache.asp"-->
<!--#include file="../funciones.asp"-->
<%
on error resume next
Dim codigo_cpf,codigo_pes

nombre_cpf=session("nombre_cpf")
codigo_cpf=session("codigo_cpf")
codigo_pes=session("codigo_pes")
descripcion_pes=session("descripcion_pes")

if codigo_pes<>"" then
	Set objPlan=Server.CreateObject("PryUSAT.clsAccesoDatos")
		objPlan.AbrirConexion
		Set rsCursos= objPlan.Consultar("ConsultarCursoPlan","FO",3,codigo_pes,codigo_cpf,0)
		objPlan.CerrarConexion
	Set objPlan=nothing
	if Not (rsCursos.BOF and rsCursos.EOF) then
		HayReg=true
	end if
end if
%>
<html>
<head>
<meta http-equiv="Content-Language" content="es">
<meta name="GENERATOR" content="Microsoft FrontPage 12.0">
<meta name="ProgId" content="FrontPage.Editor.Document">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<title>Seleccione el curso que desea agregar a la matrícula</title>
<script type="text/javascript" language="JavaScript" src="../private/funciones.js"></script>
<link rel="stylesheet" type="text/css" href="../private/estilo.css">
<link rel="stylesheet" type="text/css" href="../private/estiloimpresion.css" media="print">
</head>
<body>
<table cellpadding="2" cellspacing="0" style="border-collapse: collapse; border: 0px solid #C0C0C0; " bordercolor="#111111" width="100%">
  <tr>
    <td width="95%" height="3%" class="usatTitulo">ESCUELA PROFESIONAL DE <%=nombre_cpf%>&nbsp;</td>
    <td width="5%" height="6%" rowspan="2" valign="top" class="NoImprimir">
    <input onclick="imprimir('N',0,'')" type="button" value="    Imprimir" name="cmdImprimir" class="imprimir2"></td>
  </tr>
  <tr>
    <td width="95%" height="3%" class="etiqueta"><i>&nbsp;<%=descripcion_pes%></i></td>
  </tr>
  </table>
<%
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
    <td width="10%" height="5%"><%=rsCursos("identificador_cur")%>&nbsp;</td>
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
    rsCursos.movenext
    Loop%>
</table>
  <%
    Set rscursos=nothing
end if


If Err.Number<>0 then
    session("pagerror")="estudiante/frmconsultarplan.asp"
    session("nroerror")=err.number
    session("descripcionerror")=err.description    
	response.write("<script>top.location.href='../error.asp'</script>")
End If
%>
<b><P class="azul">TOTAL DE CRÉDITOS DEL PLAN DE ESTUDIO: <%=TGC%></P></b>
</body>
</html>