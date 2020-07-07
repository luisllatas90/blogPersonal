<!--#include file="../../../funciones.asp"-->
<%
Dim codigo_acc

codigo_cac=request.querystring("codigo_cac")
codigo_cpf=request.querystring("codigo_cpf")
resultado=request.querystring("resultado")
modulo=request.QueryString("mod")
codigo_tfu=request.QueryString("ctf")
codigo_usu=request.QueryString("id")

if codigo_cac="" then codigo_cac=session("codigo_cac")
if codigo_cpf="" then codigo_cpf="-2"
codigo_acc=session("codigo_usu")
if codigo_acc="" then codigo_acc=0

if codigo_cpf<>"-2" and resultado="S" then
	activo=true
end if

Set obj=Server.CreateObject("PryUSAT.clsAccesoDatos")
	obj.AbrirConexion
		if session("codigo_tfu")=1 or session("codigo_tfu")=27 then
			'Comentado por hreyes, cambiado a filtro por módulo 15/03/2011
			'Set rsEscuela= obj.Consultar("ConsultarCarreraProfesional","ST","MA",0)
			Set rsEscuela= obj.Consultar("EVE_ConsultarCarreraProfesional","ST",modulo, codigo_tfu, codigo_usu)

		else
			Set rsEscuela= obj.Consultar("ConsultarAcceso","ST","ESC","Silabo",codigo_acc)
		end if
	obj.CerrarConexion
Set obj=nothing

%>
<html>
<head>
<title>Administrar Aula Virtual</title>
<script language="JavaScript" src="../../../private/funciones.js"></script>
<script language="JavaScript" src="private/validaraula.js"></script>
<link rel="stylesheet" type="text/css" href="../../../private/estilo.css">
<link rel="stylesheet" type="text/css" href="../../../private/estiloimpresion.css" media="print"/>
</head>
<body>
<table border="0" cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%">
  <tr class="usattitulo">
    <td width="100%" colspan="2" height="5">Consulta de acceso al Aula Virtual</td>
  </tr>
  <tr>
    <td width="27%" height="5">Ciclo Académico</td>
    <td width="73%" height="5"><%call ciclosAcademicos("consultarAulaVirtualPorTipoEstudio(" & modulo & "," & codigo_tfu & ","  & codigo_usu & ")",codigo_cac,"","")%></td>
 </tr>
  <tr>
    <td width="27%" height="5">Escuela Profesional</td>
    <td width="73%" height="5"><%call llenarlista("cbocodigo_cpf","consultarAulaVirtualPorTipoEstudio(" & modulo & "," & codigo_tfu & ","  & codigo_usu & ")",rsEscuela,"codigo_cpf","nombre_cpf",codigo_cpf,"Seleccione la Escuela Profesional","","")%></td>
  </tr>
  <%if activo=true then%>  
  <tr class="NoImprimir">
    <td width="27%" height="5"></td>
    <td width="73%" height="5">
	<input type="button" class="usatimprimir"  value="Imprimir"  onClick="imprimir('N','2','')" name="cmdImprimir"></td>
  </tr>
   <%if codigo_cpf<>"-2" then
	Set obj=Server.CreateObject("PryUSAT.clsAccesoDatos")
		obj.AbrirConexion
	    Set rsProfesor= obj.Consultar("ConsultarAulaVirtual","FO","1",codigo_cac,codigo_cpf,0)
	    obj.CerrarConexion
	Set obj=nothing
	
	Do while Not rsProfesor.EOF
	%>
  <tr>
    <td width="100%" colspan="2" height="75%" valign="top">
    <table border="1" cellpadding="2" cellspacing="0" style="border-collapse: collapse" bordercolor="#808080" width="100%" id="tblcursoprogramado">
      <tr class="etabla">
        <td width="76%" height="6%" colspan="9" style="text-align: left"><b>PROFESOR: <%=rsProfesor("docente")%></b></td>
      </tr>
      <tr class="SA">
        <td width="3%" height="6%" rowspan="2">Acción</td>
        <td width="3%" height="6%" rowspan="2">Nº</td>
        <td width="18%" height="6%" rowspan="2">FECHA DE ACTIVACIÓN</td>
        <td width="27%" height="6%" rowspan="2">ASIGNATURA</td>
        <td width="25%" height="3%" colspan="5">RECURSOS PUBLICADOS EN EL CURSO</td>
      </tr>
      <tr class="SA">
        <td width="5%">Actividades</td>
        <td width="5%">Documentos</td>
        <td width="5%">Tareas</td>
        <td width="5%">Encuestas</td>
        <td width="5%">Foros</td>
      </tr>
      <%
      Set obj=Server.CreateObject("PryUSAT.clsAccesoDatos")
		obj.AbrirConexion
	    Set rsCursos= obj.Consultar("ConsultarAulaVirtual","FO","2",codigo_cac,rsProfesor("codigo_per"),codigo_cpf)
	    obj.CerrarConexion
		Set obj=nothing
      	agenda=0:documentos=0:tareas=0:evaluaciones=0:foro=0
      Do while Not rsCursos.EOF
      	i=i+1
      		agenda=agenda+rscursos("agenda")
      		documentos=documentos+rscursos("documentos")
      		tareas=tareas+rscursos("tareas")
      		evaluaciones=evaluaciones+rscursos("evaluaciones")
      		foro=foro+rscursos("foro")
      	%>
      <tr onMouseOver="Resaltar(1,this,'S')" onMouseOut="Resaltar(0,this,'S')">
        <td width="3%" height="3%"><img src="../../../images/previo.gif" onClick="AbrirAulaProfesor('../../../personal/aulavirtualProfesores/vstrecursospublicados.aspx?idcursovirtual=<%=rsCursos("IdcursoVirtual")%>')">&nbsp;</td>
        <td width="3%" height="3%"><%=i%>&nbsp;</td>
        <td width="18%" height="3%"><%=rsCursos("fechareg")%>&nbsp;</td>
        <td width="27%" height="3%"><%=rsCursos("titulocursovirtual")%>&nbsp;</td>
        <td width="5%" class="etiqueta" align="center"><%=rsCursos("agenda")%>&nbsp;</td>
        <td width="5%" class="etiqueta" align="center"><%=rsCursos("documentos")%>&nbsp;</td>
        <td width="5%" class="etiqueta" align="center"><%=rsCursos("tareas")%>&nbsp;</td>
        <td width="5%" class="etiqueta" align="center"><%=rsCursos("evaluaciones")%>&nbsp;</td>
        <td width="5%" class="etiqueta" align="center"><%=rsCursos("foro")%>&nbsp;</td>
      </tr>
      	<%rsCursos.movenext
      	Loop
      %>
      <tr class="usatencabezadotabla">
        <td width="96%" height="3%" colspan="4" align="right">TOTAL</td>
        <td width="5%" class="etiqueta" align="center"><%=agenda%>&nbsp;</td>
        <td width="5%" class="etiqueta" align="center"><%=documentos%>&nbsp;</td>
        <td width="5%" class="etiqueta" align="center"><%=tareas%>&nbsp;</td>
        <td width="5%" class="etiqueta" align="center"><%=evaluaciones%>&nbsp;</td>
        <td width="5%" class="etiqueta" align="center"><%=foro%>&nbsp;</td>
      </tr>
      	</table>
  </td>
  </tr>
  			<%rsProfesor.movenext
  		Loop
  	end if
  end if%>   
</table>
</body>
</html>
<%
Set rsProfesor=nothing
Set rsCursos=nothing
Set rsEscuela=nothing
%>