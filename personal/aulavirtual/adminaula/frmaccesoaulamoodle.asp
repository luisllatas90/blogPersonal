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
	'obj.CerrarConexion
'Set obj=nothing

%>
<html>
<head>
<meta name="tipo_contenido"  content="text/html;" http-equiv="content-type" charset="utf-8">
<title>Administrar Aula Virtual</title>
<script language="JavaScript" src="../../../private/funciones.js"></script>
<script language="JavaScript" src="private/validaraula.js"></script>
<link rel="stylesheet" type="text/css" href="../../../private/estilo.css">
<link rel="stylesheet" type="text/css" href="../../../private/estiloimpresion.css" media="print"/>
   
    <style type="text/css">
        .style1
        {
            background-color: #E2E2E2;
        }
    </style>
   
</head>
<body>
<table border="0" cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%">
  <tr class="usattitulo">
    <td width="100%" colspan="2" height="5">Consulta de acceso al Aula Virtual</td>
  </tr>
  <tr>
    <td width="27%" height="5">Ciclo Académico</td>
    <td width="73%" height="5"><%call ciclosAcademicos("consultarAulaVirtualPorTipoEstudio2(" & modulo & "," & codigo_tfu & ","  & codigo_usu & ")",codigo_cac,"","")%></td>
 </tr>
  <tr>
    <td width="27%" height="5">Escuela Profesional</td>
    <td width="73%" height="5"><%call llenarlista("cbocodigo_cpf","consultarAulaVirtualPorTipoEstudio2(" & modulo & "," & codigo_tfu & ","  & codigo_usu & ")",rsEscuela,"codigo_cpf","nombre_cpf",codigo_cpf,"Seleccione la Escuela Profesional","","")%></td>
  </tr>
  <%if activo=true then%>  
  <tr class="NoImprimir">
    <td width="27%" height="5"></td>
    <td width="73%" height="5">
	<input type="button" class="usatimprimir"  value="Imprimir"  onClick="imprimir('N','2','')" name="cmdImprimir"></td>
  </tr>
   <%if codigo_cpf<>"-2" then
	'Set obj=Server.CreateObject("PryUSAT.clsAccesoDatos")
		'obj.AbrirConexion
	    Set rsProfesor= obj.Consultar("MOODLE_REPORTE_UsoAulaVirtual","FO","1",codigo_cac,codigo_cpf,0)    
	    'obj.CerrarConexion
	'Set obj=nothing
	
	Do while Not rsProfesor.EOF
	           ' Set obj=Server.CreateObject("PryUSAT.clsAccesoDatos")
		       ' obj.AbrirConexion
	            Set rsAcceso= obj.Consultar("MOODLE_REPORTE_UsoAulaVirtual","FO","4",rsProfesor("codigo_pso"),0,0)	    
	            'obj.CerrarConexion
		        'Set obj=nothing
		     
		        if rsAcceso.recordcount then
		            acceso = rsAcceso("ultimoacceso")
		            if mid(acceso,7,4)="1969" then
		               acceso = "Nunca"
		            end if
		        else
		            acceso = "Nunca"
		        end if
		        
	%>
  <tr>
    <td width="100%" colspan="2" height="75%" valign="top">
    <table border="1" cellpadding="2" cellspacing="0" style="border-collapse: collapse" bordercolor="#808080" width="100%" id="tblcursoprogramado">
      <tr class="etabla">
        <td width="76%" height="6%" colspan="9" 
              style="text-align: left; background-color: #8AC5FF;"><b>PROFESOR: <%=rsProfesor("docente")%> - <i>Último Acceso: <%=acceso%> </i></b></td>
      </tr>
      
      <tr class="SA">
        <!--<td width="3%" height="6%" rowspan="2">Acción</td>-->
        <td width="3%" height="6%" rowspan="2" class="style1">Nº</td>
        <td width="18%" height="6%" rowspan="2" class="style1">FECHA DE ACTIVACIÓN</td>        
        <td width="27%" height="6%" rowspan="2" class="style1">ASIGNATURA</td>
        <td width="25%" height="3%" colspan="6" class="style1">RECURSOS PUBLICADOS EN EL CURSO</td>
      </tr>
      <tr class="SA">
        <td width="5%" class="style1">Documentos</td>
        <td width="5%" class="style1">Foros</td>
        <td width="5%" class="style1">Tareas</td>
        <td width="5%" class="style1">Encuestas</td>        
       
      </tr>
      <%
     ' Set obj=Server.CreateObject("PryUSAT.clsAccesoDatos")
		'obj.AbrirConexion
	    Set rsCursos= obj.Consultar("MOODLE_REPORTE_UsoAulaVirtual","FO","2",codigo_cac,rsProfesor("codigo_per"),codigo_cpf)
	    
	   ' obj.CerrarConexion
		'Set obj=nothing
      
      	documentos=0:foros=0:tareas=0:encuestas=0:asistencias=0
        Do while Not rsCursos.EOF
      	    i=i+1
      	
      	      'Set obj=Server.CreateObject("PryUSAT.clsAccesoDatos")
		        'obj.AbrirConexion
	            Set rsCursosMdl= obj.Consultar("MOODLE_REPORTE_UsoAulaVirtual","FO","3",rsCursos("refcodigo_cup"),0,0)	    
	           ' obj.CerrarConexion
		       ' Set obj=nothing
		        
		        if rsCursosMdl.recordcount then 
      		        documentos=documentos+cint((rsCursosMdl("documentos")))
      		        foros=foros+cint((rsCursosMdl("foros")))
      		        tareas=tareas+cint(rsCursosMdl("tareas"))
      		        encuestas=encuestas+cint(rsCursosMdl("encuestas"))      		      		
      		       ' asistencias=asistencias+cint(rsCursosMdl("asistencias"))      		      		
      	%>
      <tr onMouseOver="Resaltar(1,this,'S')" onMouseOut="Resaltar(0,this,'S')">
        
        <td width="3%" height="3%"><%=i%>&nbsp;</td>
        
        <td width="18%" height="3%"><%=rsCursosMdl("fecha")%>&nbsp;</td>
        
        <td width="27%" height="3%"><%=rsCursos("descripcion_Cac") & " - " & rsCursos("nombre_cur") & " - "& rsCursos("grupohor_cup")  &" - " & ConvRomano(rsCursos("ciclo_cur"))& " Ciclo" %>&nbsp;</td>
        <td width="5%" class="etiqueta" align="center"><%=rsCursosMdl("documentos")%>&nbsp;</td>
        <td width="5%" class="etiqueta" align="center"><%=rsCursosMdl("foros")%>&nbsp;</td>
        <td width="5%" class="etiqueta" align="center"><%=rsCursosMdl("tareas")%>&nbsp;</td>
        <td width="5%" class="etiqueta" align="center"><%=rsCursosMdl("encuestas")%>&nbsp;</td>        
        
      </tr>
            
      	<%
      	    else %>
      	       <tr onMouseOver="Resaltar(1,this,'S')" onMouseOut="Resaltar(0,this,'S')">
      	        
        
        <td width="3%" height="3%"><%=i%>&nbsp;</td>
        
        <td width="18%" height="3%"><%response.Write("<font color='red'>Este curso no fue activado.</font>")%>&nbsp;</td>
        
        <td width="27%" height="3%"><%=rsCursos("descripcion_Cac") & " - " & rsCursos("nombre_cur") & " - "& rsCursos("grupohor_cup")  &" - " & ConvRomano(rsCursos("ciclo_cur"))& " Ciclo" %>&nbsp;</td>
        <td width="5%" class="etiqueta" align="center"><%response.write("-")%>&nbsp;</td>
        <td width="5%" class="etiqueta" align="center"><%response.write("-")%>&nbsp;</td>
        <td width="5%" class="etiqueta" align="center"><%response.write("-")%>&nbsp;</td>
        <td width="5%" class="etiqueta" align="center"><%response.write("-")%>&nbsp;</td>      
      	   <% end if
      	    set rsCursosMdl = nothing
      	    rsCursos.movenext
      	
      	    Loop
      	    response.Write("<br/>")
      %>
      <tr class="usatencabezadotabla">
        <td width="96%" height="3%" colspan="3" align="right">TOTAL</td>
        <td width="5%" class="etiqueta" align="center"><%=documentos%>&nbsp;</td>
        <td width="5%" class="etiqueta" align="center"><%=foros%>&nbsp;</td>
        <td width="5%" class="etiqueta" align="center"><%=tareas%>&nbsp;</td>
        <td width="5%" class="etiqueta" align="center"><%=encuestas%>&nbsp;</td>        
      
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
obj.CerrarConexion
Set obj=nothing
%>