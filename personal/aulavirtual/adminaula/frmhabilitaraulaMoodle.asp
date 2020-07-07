<!--#include file="../../../funciones.asp"-->
<% 

'###YPEREZ####

accion=request.querystring("accion")
modo=request.querystring("modo")
codigo_cac=request.querystring("codigo_cac")
codigo_dac=request.querystring("codigo_dac")
codigo_per=request.querystring("codigo_per")


if session("codigo_tfu")="1" or session("codigo_tfu")="85" or session("codigo_tfu")="181"  then Todos="S" 

'if session("codigo_usu")="1002" then  'helen
'   Todos="S" 
'end if


Set objDpto=Server.CreateObject("PryUSAT.clsDatDepartamentoAcademico")
	Set rsDpto=objDpto.ConsultarDepartamentoAcademico("RS","CO",session("codigo_dac"))
Set ObjDpto=nothing

Set Obj=Server.CreateObject("PryUSAT.clsDatCicloAcademico")
	Set rsCac= Obj.ConsultarCicloAcademico ("RS","TO",0)
Set Obj=nothing

if (modo="R" and codigo_dac<>"-2") then
	Set Obj= Server.CreateObject("PryUSAT.clsAccesoDatos")
		Obj.AbrirConexion
		Set rsProfesor=Obj.Consultar("ConsultarDocente","FO","2",codigo_cac,codigo_dac)
		Obj.CerrarConexion
	Set obj=nothing
	
	if Not(rsProfesor.BOF and rsProfesor.EOF) then
		alto="99%"
		estado="R"
	end if
end if
%>
<html>
<head>
<meta http-equiv="Content-Language" content="es">
<meta name="GENERATOR" content="Microsoft FrontPage 5.0">
<meta name="ProgId" content="FrontPage.Editor.Document">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<title>Acceso Aula Virtual</title>
<link rel="stylesheet" type="text/css" href="../../../private/estilo.css">
<script language="JavaScript" src="../../../private/funciones.js"></script>
</head>
<body>
<table border="0" cellpadding="2" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%" height="<%=alto%>">
    <tr>
      <td width="100%" class="usattitulo" height="5%" colspan="4" valign="top">
      Habilitar Acceso al Aula Virtual Virtual para Profesores </td>
     
    </tr>
    <tr>
        <td width="18%" height="3%" valign="top">Ciclo Académico</td>
        <td width="10%" height="3%" valign="top"><%call llenarlista("cboCiclo","actualizarlista('frmhabilitaraulaMoodle.asp?codigo_cac=' + this.value)",rsCac,"codigo_cac","descripcion_cac",codigo_cac,"","","")%></td>
        <td width="20%" height="3%" valign="top" align="right">Departamento Acad.</td>
        <td width="40%" height="3%" valign="top"><%call llenarlista("cboDpto","actualizarlista('frmhabilitaraulaMoodle.asp?modo=R&codigo_cac=' + cboCiclo.value + '&codigo_dac=' + this.value)",rsDpto,"codigo_dac","nombre_dac",codigo_dac,"Seleccionar el Dpto Académico",Todos,"")%></td>
        </tr>
    <%if estado="R"  then%>
    <tr>
        <td width="18%" height="3%" valign="top">Profesor</td>
        <td width="70%" height="3%" valign="top" colspan="3">
        <%call llenarlista("cboprofesor","fradetalle.location.href='lstcargadocenteMoodle.asp?codigo_per=' + this.value + '&codigo_cac=' + cboCiclo.value",rsProfesor,"codigo_per","docente",codigo_per,"Seleccionar el Profesor","","")%>
		</td>
        </tr>
		<tr>
		<td width="100%" height="40%" colspan="4" valign="top">
		<iframe name="fradetalle" id="fradetalle" height="100%" width="100%" border="0" frameborder="0">
        El explorador no admite los marcos flotantes o no está configurado actualmente para mostrarlos.</iframe>
		</td>
		</tr>
	<%elseif modo="R" then%>
		<tr><td width="100%" height="5%" colspan="4" class="usatsugerencia">&nbsp;&nbsp;&nbsp; No se han registrado Profesores para el Departamento Seleccionado</td></tr>
	<%end if%>
	
</table>
</body>
</html>
	
 