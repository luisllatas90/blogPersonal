<!--#include file="../NoCache.asp"-->
<!--#include file="clsMatricula.asp"-->
<%
call Enviarfin(session("codigo_usu"),"../")
Dim titulopestana,cicloIngreso,mostrarcomplementarios,clsCurr,clsComp
On error Resume next

cicloIngreso=left(session("cicloIng_Alu"),4)


'Hector: El error de matematica financiera era porque lo restringen a 4 digitos y por eso no entra al if agregue cicloIngreso2 para no malear algo
cicloIngreso2=session("cicloIng_Alu")


codigo_alu=session("codigo_alu")
codigo_pes=session("Codigo_Pes")
codigo_cac=session("Codigo_Cac")
descripcion_cac=session("descripcion_cac")
dim cn
set cn=server.createobject("PryUSAT.clsAccesoDatos")

'### consultar mensajes bloqueos ###
cn.AbrirConexion
	Set rsMensajes=cn.Consultar("VerificarAccesoMatriculaEstudiante_V2","FO","ALU",session("codigo_alu"),session("codigo_cac"))
    set rsCuotas=cn.Consultar("MAT_ConsultarCronogramaPensiones","FO",session("codigo_cac") )    
cn.CerrarConexion	

if (rsMensajes.BOF = false and rsMensajes.EOF=false)  then
	'rsMensajes.movefirst
	'response.write("<tr><td colspan='3'>Lo sentimos, usted tiene bloqueada esta opción por los siguientes motivos:<br><br>")
	response.Write("<style type='text/css'> #basic-modal-content {display:none;}</style>")
	response.write("<h3>PROCESO DE MATRICULA " & descripcion_cac & " </h3>")
	response.write("<font style='font-family: Arial;font-size: 11pt'>Lo sentimos, usted tiene <font color='red'><B>BLOQUEADA<B></font> esta opción por los siguientes motivos:<br><br>")
	response.write("<table border='1' cellpadding='3' cellspacing='0' style='border-collapse: collapse; font-family: Arial;font-size: 11pt' bordercolor='#111111' width='100%'>")
	response.write("<tr  style='background-color: #395ACC; color:#FFFFFF;'><td width='60%' height='30' align='center' > <b>Motivo</b>")
	response.write("</td>")
	response.write("<td class='usattitulo' align='center' > <b>Acudir A</b>")
	response.write("</td>")
	response.write("<td class='usattitulo' align='center' > <b>Fecha Fin</b>")
	response.write("</td></tr>")
	Do while not rsMensajes.EOF
		response.write("<tr>")
		response.write("<td> » " & rsMensajes("mensaje_blo") & "</td>")
		response.write("<td>" & rsMensajes("acudirA_blo") & "</td>")
		response.write("<td>" & rsMensajes("fechaVence_blo") & "</td></tr>")
		rsMensajes.movenext
	loop
	response.write("</table></font>")
else

dim rshabilitado
dim rstotales , rsrequisitoalumno 

cn.abrirconexion
	set rstotales=cn.consultar("sp_obtenerciclootros","FO",codigo_alu, codigo_pes)
	set rsrequisitoalumno=cn.consultar("ConsultarRequisitosMatricula_v2","FO",codigo_alu, codigo_pes, codigo_cac)
cn.cerrarconexion    
	credaprobados=rstotales.fields("creditosaprobados")
	if rstotales.fields("ciclo")<0 then
		cicloalumno = 0
	else
		cicloalumno = rstotales.fields("ciclo")
	end if 
	'cicloalumno=iif(rstotales.fields("ciclo")<0,0,rstotales.fields("ciclo"))
	'cicloalumno = rstotales.fields("ciclo")
	cantidadmaxcreditos=cint(rsrequisitoalumno.fields("creditomaximomatricula"))
	session("CredMax")=cantidadmaxcreditos
	session("credMat")=rsrequisitoalumno.fields("credMatriculados")
	session("cantCursos")=rsrequisitoalumno.fields("cantCursos")
	ponderado =rsrequisitoalumno.fields("Ponderado")
	notaminima =rsrequisitoalumno.fields("notaMinima")
	nroCuotas = rsrequisitoalumno.fields("nroCuotas")
	if rsrequisitoalumno.fields("codigo_mat") > 0 then
		tieneMatricula=1
	else
		tieneMatricula=0
	end if 
	'tieneMatricula = iif(rsrequisitoalumno.fields("codigo_mat")>0, 1, 0)
	set rstotales=nothing
	
Dim rsInformacion
cn.abrirconexion
    set rsInformacion=cn.consultar("MAT_RetornaFechasEstudiante","FO", cint(session("codigo_test")), codigo_cac)
cn.cerrarconexion   	                                                            
            
Set cn=nothing

codigo_mat=request.querystring("codigo_mat")
accion=request.querystring("accion")

titulopestana="Cursos Curriculares"
clsCurr="pestanaresaltada"

if accion="" then accion="matriculasegura"
if codigo_mat="" then codigo_mat=0

'#########################################################
'Verificar si hay una matricula existente bloquear acceso
if tieneMatricula = 1  and codigo_mat = 0 then
	response.redirect("mensajes.asp?proceso=EMAT")	
end if 
'response.redirect("mensajes.asp?proceso=F")
'#########################################################
'*************************************************************************************
'1. Verifica si es alumno libre, cuyo plan es 1
'*************************************************************************************
if session("codigo_pes")=1 then
	titulopestana="Cursos a elegir"
	'mostrarcomplementarios=false
end if

'*************************************************************************************
'Aumentar tiempo de conexión al servidor y verificar si el cliente está conectado
'al servidor, caso contrario volverlo a conectar
'*************************************************************************************
Server.ScriptTimeout=1000

If Not Response.IsClientConnected Then
  	'Recetear si se desconectó el cliente
	Shutdownid = Session.SessionID
	'Obtimizar proceso
	Shutdown(Shutdownid)
End If

'*************************************************************************************
'4. Deshabilitar el botón horario para modulares
'*************************************************************************************
if 	int(session("codigo_cpf"))=20 or _
	int(session("codigo_cpf"))=22 or _
	int(session("codigo_cpf"))=23 then
	quitar="style=""display:none"""
end if
%> 
<!DOCTYPE HTML>
<html class="no-js">
	<head>
        <meta http-equiv="Content-Language" content="es" />
        <meta http-equiv="Content-Type" content="text/html; charset=windows-1252" />
		<title>FICHA DE MATRICULA</title>
		<link rel="stylesheet" type="text/css" href="../private/estilo.css" />
		<script type="text/javascript" language="JavaScript" src="../private/funciones.js"></script>		
		<!-- <script type="text/javascript" language="JavaScript" src="private/validarhorario.js"></script>  -->
		<script type="text/javascript" src="scriptMatricula/validarhorario.js?v=<% response.Write DateDiff("s", "12/31/1969 00:00:00", Now) %>"></script> 
        <!-- <script type="text/javascript" language="JavaScript" src="private/validarciclocurso.js"></script> -->
        <script type="text/javascript" src="scriptMatricula/validarciclocurso.js?v=<% response.Write DateDiff("s", "12/31/1969 00:00:00", Now) %>"></script> 
        <!-- <script type="text/javascript" language="JavaScript" src="private/validarmatricula.js?v=<% response.Write DateDiff("s", "12/31/1969 00:00:00", Now) %>"></script>  -->
        <script type="text/javascript" src="scriptMatricula/validarmatricula.js?v=<% response.Write DateDiff("s", "12/31/1969 00:00:00", Now) %>"></script>         
        <!-- Libreria jQuery 1.9 -->
        <script type="text/javascript" src="private/jquery.js"></script>        
        <script type="text/javascript" src="private/jquery.simplemodal.js"></script>        
        <script type="text/javascript" src="private/modernizr.custom.js"></script>
        <!-- Libreria jQuery 1.3 -->
        <!-- <script type="text/javacript" language="JavaScript" src="../private/jq/jquery.js" ></script> -->        
        <style type="text/css">
	        #basic-modal-content {display:none;}	

			#simplemodal-overlay {background-color:#000;}			
			
			#simplemodal-container {height:400px; width:600px; color:#000; background-color:#fff; border:4px solid #444; padding:12px;}
			#simplemodal-container .simplemodal-data {padding:8px;}
			#simplemodal-container code {background:#141414; border-left:3px solid #65B43D; color:#bbb; display:block; font-size:12px; margin-bottom:12px; padding:4px 6px 6px;}
			#simplemodal-container a {color:#ddd;}
			#simplemodal-container a.modalCloseImg {background:url(../img/basic/x.png) no-repeat; width:25px; height:29px; display:inline; z-index:3200; position:absolute; top:-15px; right:-16px; cursor:pointer;}
			#simplemodal-container h3 {color:#000;}	
			
            .style1
            {
                height: 23px;
            }
            .style2
            {
            }
        </style>
        <script>            
            jQuery(function($) {
                // Load dialog on page load
                //$('#basic-modal-content').modal();

                // Load dialog on click
                $('#linkModal').click(function(e) {
                    if ($('#txtacepta').val() == 1) {
                        $('#chkAcepto').attr('checked', true);
                    } else {
                        $('#chkAcepto').attr('checked', false);
                    }
                    $('#basic-modal-content').modal();
                    return false;
                });

                $('#chkAcepto').click(function(e) {
                    if ($('#txtacepta').val() == 0) {
                        $('#txtacepta').val(1);
                    } else {
                        $('#txtacepta').val(0);
                    }                    
                });
            });
		</script>
        <script type="text/javascript" language="javascript">
            function OcultarTbl() {

                if (jQuery.browser.msie != true) {
                    alert("Utilice el Navegador Internet Explorer 7 o Superior")
                    location.href = 'avisos/reglamento/ReglamentoPensiones2011-0.html?id=11268'
                }

                if (document.all.cursocomplementario != undefined)
                { document.all.cursocomplementario.style.display = 'none' }
            }
	    </script>
	</head>	
	<!-- <body onLoad="OcultarTbl()" >-->
	<body>
	
	<table id="tblmensaje" border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse;display:none" bordercolor="#111111" width="100%" height="100%" class="contornotabla">
	<tr>
	<td width="100%" align="center" class="usatTitulo" bgcolor="#FEFFE1">
	Procesando<br/>
	Por favor espere un momento...<br/>
	<img border="0" src="../images/cargando.gif" width="209px" height="20px" alt="aviso" />
	</td>
	</tr>
	</table>

<form id="frmFicha" name="frmFicha" method="post" action="procesarmatricula.asp?accion=<%=accion%>&codigo_mat=<%=codigo_mat%>">
<input id="CursosProgramados" name="CursosProgramados" type="hidden" value="0" />
<input id="txtcicloalumno" name="txtcicloalumno" type="hidden" value="<%=cicloalumno%>" />
<input id="VecesDesprobados" name="VecesDesprobados" type="hidden" value="0" />
<input id="txtcodigo_cpf" name="txtcodigo_cpf" type="hidden" value="<%=session("codigo_cpf")%>" />
<input id="txttipo_cac" name="txttipo_cac" type="hidden" value="<%=session("tipo_cac")%>" />

<div id="tblFicha">
<table border="0" cellpadding="0" cellspacing="0" style="BORDER-COLLAPSE: collapse; border-color:#111111; height:75%" width="100%">
	<tr>
		<td height="7%" class="usattitulo" width="50%" valign="top"><%=iif(codigo_mat>0,"Agregado de cursos","Búsqueda de cursos")%> (Matrícula: <%=session("descripcion_cac")%>)</td>
		<td height="7%" class="rojo" align="right" style="cursor:hand" width="50%" valign="top">
	    <% if(Session("prereq") = "") then %>
		    <input name="cmdPreMatricula" type="button" visible="false" value="Actualizar lista de cursos" 
                class="modificar2" style="width:150px;" onclick="ActualizaPreRequisito()" /> 
        <% end if %> 
		<input onClick="VistaHorario('P')" type="button" value="    Vista horario" name="cmdHorario" class="horario2" id="cmdHorario" disabled="true" <%=quitar%> />
         <input onclick="EnviarFichaMatricula()" type="button" 
                value="    Guardar Matrícula" name="cmdGuardar" class="guardar2" 
                id="cmdGuardar" style="width: 110px; height: 19px;" />         
        </td>
	</tr>
	<% if (mid(session("descripcion_cac"), 5, 2) = "-0") then                                    
	        if (rsInformacion.BOF = false and rsInformacion.EOF=false)  then 	        
	%>
	<tr>
		<td colspan="2" style="font-size:7pt"> 		
		<b>» Inscripción por web: </b> del <% response.Write rsInformacion("IniciaMat") %> al <% response.Write rsInformacion("FinalizaMat") %><br/>		
		<b>» Agregados y retiros: </b> hasta el <% response.Write rsInformacion("FinalizaAgryRet") %> (hora límite 12 m) <br/>
		<b>» Fecha de venc. de matricula: </b> <% response.Write rsInformacion("AnulaMat") %> <br/>
		<b>»</b> A partir del <% response.Write session("descripcion_Cac") %> las matriculas serán registradas como parte 
		del récord académico y contabilizadas para los casos de retiros temporales y definitivos<br/><br/>
		</td>
	</tr>
	   <%   end if %>
	<tr>
		<td colspan="2" style="font-size:7pt"> <b>[Límite de créditos matriculados]</b> Artículo 93°: En el ciclo académico extraordinario el estudiante puede matricularse en un máximo de ocho créditos. 
		Si su promedio ponderado del ciclo académico inmediato anterior es superior a dieciséis puntos, puede matricularse hasta en doce créditos.
		</td>
	</tr>
	<% end if %>
	<tr>
		<td width="100%" height="88%" colspan="2" valign="top">
			<table cellspacing="0" cellpadding="4" style="border-collapse: collapse" bordercolor="#111111" width="100%" height="100%">
				<tr>
					<td class="bordeinf" align="left" width="65%" height="5%">
					  <font color="#0000FF">Créditos máx. para Matricularse: <b><% response.write(session("CredMax")) %> </b>
							| Total de créditos aprobados : <%=credaprobados%> | Ciclo 
							actual : <%=cicloalumno%> 
					  </font>&nbsp;														
							</td>
				</tr>
				<tr>
					<td height="100%" width="100%" colspan="4" class="pestanarevez" bgcolor="#EEEEEE" valign="top">
					<%
		
						ingreso=trim(session("cicloIng_alu"))
						conteoCompl=0
						conteoCurr=0
																
					Dim rsCursos ',rsRequisitos
					Dim i,evento,controlimg
					Dim vacantesdisponibles
					Dim tipo_cur,ciclo_cur,creditos_cur
					Dim descripciontipo,posicion,estadocurso,nombre_cur
					Dim sumcreditos,codigoUnico,credMax,BloquearCurso
					Dim ExigirCompl
	
					'Inicializar y verificar variables locales
					i=0
					cursounico=""
					sumcreditos=0
					MatrCompl=0
					ExigirCompl=true
					Par=false
		
	                Dim ObjMatricula
					Set ObjMatricula = Server.CreateObject("PryUSAT.clsAccesoDatos")
					ObjMatricula.AbrirConexion
					'response.write (codigo_alu  & " * " & codigo_pes & " * " & codigo_cac)
				    Set rsCursos=ObjMatricula.Consultar("ConsultarCursosHabilesMatriculav6","FO",codigo_alu,codigo_pes,codigo_cac)
					'Set rsRequisitos=ObjMatricula.Consultar("ConsultarRequisitosMatricula","FO",codigo_alu,codigo_pes)
					
					'Quitar ciclo academico de derecho y plan 36		
					if (rsCursos.BOF And rsCursos.EOF) then
						response.write "<div id=""curso" & descripciontipo & """><p class=""usatsugerencia"">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;No se han Programado Cursos para este ciclo acadÃ©mico " & descripcion_cac  & ", segÃºn su Plan de Estudios. Counsultar con la DirecciÃ³n de su Escuela Profesional</p></div>"
					Else
						credMax=IIf(IsNull(rsrequisitoalumno("creditomaximomatricula"))=True,0,rsrequisitoalumno("creditomaximomatricula"))
						totalcredAprob=IIf(IsNull(rsrequisitoalumno("totalCredAprobados"))=True,0,rsrequisitoalumno("totalCredAprobados"))                
    %>
					
	<table id="curso<%=descripciontipo%>" border="0" cellpadding="0" cellspacing="0" style="BORDER-COLLAPSE: collapse; height:300px" width="100%">
	<tr class="usatceldatitulo">
		<td width="3%" height="5%" align="center">
		<input type="hidden" name="txtcredMax" id="txtcredMax" value="<%=session("credMax")%>" />
		<input type="hidden" name="txttotalcredAprob" id="txttotalcredAprob" value="<%=session("totalcredAprob")%>" />	
		</td>
		<td width="4%" height="5%" align="center">#</td>
		<td width="5%" height="5%" align="center">Tipo</td>
		<td width="10%" height="5%" align="center">Código</td>
		<td width="45%" height="5%" align="left">Descripción</td>
		<td width="5%" height="5%" align="center">Ciclo</td>
		<td width="5%" height="5%" align="center">Créd.</td>
	</tr>
	<tr>
		<td width="100%" height="95%" colspan="7" valign="top" class="contornotabla">
		<div id="listadiv" style="height: 100%" class="NoImprimir">
			<table id="tbl<%=descripciontipo%>" border="0" cellpadding="3" cellspacing="0" style="border-collapse: collapse" width="100%" bgcolor="#FFFFFF">
			<%
			
			Do while not rsCursos.EOF
		  		i=i+1
			  	electivo=0
			  	nombre_cur=rsCursos("nombre_cur")
			  	BloquearCurso=false
	
				'*******************************************************************************
				'Verificar vacantes disponibles para el GH
				'*******************************************************************************
				VacantesDisponibles=cdbl(rsCursos("vacantes_cup"))-cdbl(rsCursos("nromatriculados"))
	  			if rsCursos("estado_cup")=0 then
	  				VacantesDisponibles=0
	  			end if
			
				if VacantesDisponibles<=0 then
					estadocurso=true
					VacantesDisponibles="[GRUPO CERRADO]"
				else
					estadocurso=false
					VacantesDisponibles=VacantesDisponibles & " vacantes disponibles"
				end if
	  			
				'*******************************************************************************
	  			'Contar los cursos complementarios matriculados
				'*******************************************************************************
				if int(rsCursos("EsCursoMatriculado"))>0 then
					if ((trim(rsCursos("tipo_cur"))="CO" or trim(rsCursos("tipo_cur"))="CC") AND int(rsCursos("creditos_cur"))=0) then
	  					MatrCompl=MatrCompl+1
					end if
		  		end if

				'*******************************************************************************
	  			'Agregar el texto (electivo) al nombre del curso
				'*******************************************************************************	
	  			if rsCursos("electivo_cur")=true then
					electivo=1
					nombre_cur=nombre_cur & "<font color='#0000FF'>(Electivo)</font>"
				end if

				'*******************************************************************************
	  			'Bloquear Matemática Financiera para Derecho, Plan 99 hasta Ingresantes 2002-I
	  			'*******************************************************************************
	  			if int(codigo_pes)=36 and int(rsCursos("codigo_cur"))=588 AND _
	  				(trim(cicloIngreso2)="1999-I" OR _
	  				trim(cicloIngreso2)="1999-II" OR _
	  				trim(cicloIngreso2)="2000-I" OR _
	  				trim(cicloIngreso2)="2000-II" OR _
	  				trim(cicloIngreso2)="2001-I" OR _
	  				trim(cicloIngreso2)="2001-II" OR _
	  				trim(cicloIngreso2)="2002-I" OR _
	  				trim(cicloIngreso2)="2002-II") then
					
					BloquearCurso=true

				end if

				'response.write(".." & bloquearCurso & "-" & codigo_pes & "--" & codigo_cur)
				
				'response.write "ciclo ingreso: " & cicloIngreso & " plan=" & codigo_pes & "-->" & BloquearCurso
				'*******************************************************************************
	  			'Bloquear Computación Aplicativa II para Ing. de sistemas y matemática, computación en todos los planes
	  			'*******************************************************************************
				if cdbl(rsCursos("codigo_cur"))=1020 and (int(session("codigo_cpf"))=3 or int(session("codigo_cpf"))=7) then
				    BloquearCurso=true
				end if
				
				'*******************************************************************************
	  			'Mostrar sólo cursos desbloqueados
	  			'*******************************************************************************
	  			
	  			
	  			'Hector Zelada:05/08/09 Complementarios solicita bloqueo para cachimbos
	  			if ((trim(rsCursos("tipo_cur"))="CO" or trim(rsCursos("tipo_cur"))="CC") AND int(rsCursos("creditos_cur"))=0 AND session("cicloIng_Alu")="2009-II") then
                    
                        
                    'Solo Ingles e Italiano
                    'rsCursos("codigo_cur")=2165  <-- Ingles especializado
                    if rsCursos("codigo_cur")=2164 or rsCursos("codigo_cur")=1025 or rsCursos("codigo_cur")=1026 or rsCursos("codigo_cur")=1145 or rsCursos("codigo_cur")=1027 or rsCursos("codigo_cur")=1336 or rsCursos("codigo_cur")=1028 or rsCursos("codigo_cur")= 1333 or rsCursos("codigo_cur")= 2166 or rsCursos("codigo_cur")=1331 or  rsCursos("codigo_cur")=1029 or  rsCursos("codigo_cur")=1030 or  rsCursos("codigo_cur")=2167 or  rsCursos("codigo_cur")=1143 or  rsCursos("codigo_cur")=1334 or rsCursos("codigo_cur")=1337 or rsCursos("codigo_cur")=1031 or rsCursos("codigo_cur")=1032  then
                        estadocurso=true
					    VacantesDisponibles="[GRUPO CERRADO*]"
					end if
                end if
	  			'----------------------------------------------
	  			
	  			
	  			
	  			if cdbl(rsCursos("codigo_cur"))<>cdbl(codigoUnico_cur) AND BloquearCurso=false then
	  				codigoUnico_cur=rsCursos("codigo_cur")
	  				j=j+1
	  				
					'if Par=True then
					'	Clase="Par"
					'else
					'	Clase="Impar"
					'end if
					'Par=Not Par
  			%>
  				
  				<tr style="cursor:hand" onClick="AbrirCurso('<%=codigoUnico_cur%>')" id="curso_padre<%=codigoUnico_cur%>" clase="<%=Clase%>">
				<td class="bordesup" width="3%" align="center"><img src="../images/mas.gif" alt="Abrir Grupos horarios" id="img<%=codigoUnico_cur%>"></td>
				<td class="bordesup" width="3%" align="center"><%=j%></td>
				<td class="bordesup" width="4%" align="center"><%=rsCursos("tipo_cur")%></td>
				<td class="bordesup" width="10%" align="center"><%=rsCursos("identificador_cur")%></td>
				<td class="bordesup" width="50%"><B><%=nombre_cur%></B>
				<%
				'*******************************************************************************
	  			'Mostrar sólo cursos desbloqueados y que no se haya matriculado
	  			'*******************************************************************************
				if BloquearCurso=false and rsCursos("EsCursoMatriculado")=0 then%>
				<input style="display:none" name="chkcursos" type="checkbox" id="chkcursoUnico<%=codigoUnico_cur%>" tc="<%=rsCursos("tipo_cur")%>" cc='<%=rsCursos("codigo_cur")%>' ciclo="<%=rsCursos("ciclo_cur")%>" electivo="<%=electivo%>" value="<%=rsCursos("creditos_cur")%>" />
				<%end if%>
				</td>
				<td class="bordesup" width="5%" align="center"><%=ConvRomano(rsCursos("ciclo_cur"))%></td>
				<td class="bordesup" width="5%" align="center"><%=rsCursos("creditos_cur")%></td>
				</tr>
			<%
  				end if
  		  	%>
			<tr valign="top" style="display:none" id="codigo_cur<%=rsCursos("codigo_cur")%>">
				<td colspan="7" width="3%" align="right">
				<table style="border-collapse:collapse" width="100%" class="bordesup">
				<tr onMouseOver="Resaltar(1,this,'S')" onMouseOut="Resaltar(0,this,'S')">
				<td class="rojo" width="10%" align="center"><%=VacantesDisponibles%></td>
				<td width="10%" align="center">GRUPO <%=rsCursos("grupohor_cup")%>
				<% if rsCursos("soloPrimerCiclo_cup") = true then  response.Write("<br><font color='blue'>[Sólo para<br>ingresantes]</font>") %>
				</td>
				<td width="50%">
				<%
					i=0
					estadocheck=true
					Set rsHorario=ObjMatricula.Consultar("ConsultarHorarios","FO","4",rsCursos("codigo_cup"),0,0)
					If Not(rsHorario.BOF AND rsHorario.EOF) then
						ExigirCompl=rscursos("TieneComplementario")
						response.write "<table width='100%'>"
						Do while Not rsHorario.EOF
							i=i+1
							clase=""
			  				inicio=Extraercaracter(1,2,rsHorario("nombre_hor"))
			  				fin=Extraercaracter(1,2,rsHorario("horafin_Lho"))
							if IsNull(rsHorario("docente"))=false then
								docente=ConvertirTitulo(rsHorario("docente"))
							end if
							
							if i>1 then clase="class='lineahorario'"
		
		  					obs="Inicio: " & rsHorario("fechainicio_cup") & " Fin " & rsHorario("fechafin_cup")
			  				response.write "<tr><td " & clase & ">" & vbcrlf
							response.write("<input type=""hidden"" name=""txthorario" & rsCursos("codigo_cup") & """ value=""" & rsHorario("dia_Lho") & inicio & fin & """>")
							response.write("<input type=""hidden"" name=""txtambiente" & rsCursos("codigo_cup") & """ value=""" & rsHorario("ambiente") & """>")
							
							response.write(ConvDia(rsHorario("dia_Lho")) & " " & rsHorario("nombre_hor") & "-" & rsHorario("horafin_Lho") & "<br>")
							response.write(ConvertirTitulo(rsHorario("ambiente")) & "(Hrs. " & rsHorario("tipohoracur_lho") & ")") & vbcrlf
							response.write "</td><td " & clase & ">" & vbcrlf
							response.write(docente & "<br>" & obs) & vbcrlf
							response.write "</td></tr>"
							rsHorario.movenext
				  		Loop
				  		response.write "</table>"
					else
						response.write "<span class='cursoC'>[No se ha registrado horarios del grupo a elegir]</span>"
						estadocheck=false
					End If
				Set horario=nothing
				%>				
				</td>
				<td width="5%" align="center">
				<%
				if int(rsCursos("vecesdesaprobada"))>0 then
					response.write rsCursos("vecesdesaprobada") & " vez."
				end if				
				%>&nbsp;
				</td>
				<td width="5%" align="right">
				<%if estadocurso=false and BloquearCurso=false and rsCursos("EsCursoMatriculado")=0 and estadocheck=true then%>
				<input type="checkbox" tcomp="<%=rsCursos("tipocomplementario_cur")%>"  onclick="Actualizar(this,'<%=session("CredMax")%>','<%=tipo%>')" preciocurso='<%=rsCursos("preciocurso")%>' cicloalumno='<%=cicloalumno%>' preciocalculadocurso='<%=rsCursos("preciocalculadocurso")%>'  cp='<%=rsCursos("codigo_cup")%>' tc="<%=rsCursos("tipo_cur")%>" vd='<%=rsCursos("vecesdesaprobada")%>' cc='<%=rsCursos("codigo_cur")%>' nc='<%=rsCursos("nombre_cur")%>' gh='<%=rsCursos("grupohor_cup")%>' ciclo='<%=rsCursos("ciclo_cur")%>' electivo="<%=electivo%>" value="<%=rsCursos("creditos_cur")%>" name="chkcursoshabiles" id='chk<%=rsCursos("codigo_cup")%>' Posicion="<%=i%>" /> 				
				<%end if%>
				</td>
				</tr>
				</table>
				</td>
			</tr>
				<%
  			rsCursos.movenext
	  	  Loop
	  	  
	  	  %>
			</table>
		</div>
		</td>
	</tr>
	<tr>
	    <td colspan="7" style="color:#FF0000" >        
           <%   
            'response.Write "6 <br/>"      
            Set obj1=Server.CreateObject("PryUSAT.clsAccesoDatos")	        
            'set rscondicion = Server.CreateObject("ADODB.Recordset")
            Dim rscondicion
            obj1.AbrirConexion               
            set rsingresante=obj1.consultar("ACAD_VerificaIngresante","FO",codigo_alu, codigo_cac)                                        
            obj1.CerrarConexion              
            
            obj1.AbrirConexion               
            set rscondicion=obj1.consultar("MAT_BloqueoCondicionMatricula","FO", codigo_alu, codigo_cac)      
            obj1.CerrarConexion              
            
            'response.Write "7 <br/>"      
            Dim tipocondicion
            Dim motivocondicion
            tipocondicion = 0            
            motivocondicion = ""
            
            'response.Write "<script>alert(' ok " & rscondicion.recordCount & " ')</script>"
            if Not(rscondicion.BOF and rscondicion.EOF) then                                                                            
                tipocondicion = Cint(rscondicion("tipoMensaje_blo"))                   
                if not (rsingresante.BOF and rsingresante.EOF) then
                    if(rscondicion("tipoMensaje_blo") = "1" or rscondicion("tipoMensaje_blo") = "2") then                
                        response.Write "Usted tiene <b>PROBLEMAS ACADEMICOS</b>"
                    else            
                        if(rscondicion("tipoMensaje_blo") = "3" or rscondicion("tipoMensaje_blo") = "4") then                
                            response.Write "Usted tiene <b>MATRICULA CONDICIONAL</b>"
                        end if                        
                    end if 
                end if                    
                motivocondicion = rscondicion("mensaje_blo")
                'response.Write ("<b>Motivo:</b> " & rscondicion("mensaje_blo"))       
            end if
            set obj1 = nothing
            'response.Write "8 <br/>"
           %>           
           <input type="hidden" id="hdtipomat" name="hdtipomat" value="<% response.write(tipocondicion) %>" />
           <input type="hidden" id="hdmotivo" name="hdmotivo" value="<% response.write(motivocondicion) %>" />
           <br /> 
           <div id='linkModal'>			
<%  if not (rscondicion.EOF AND rscondicion.BOF) then  %>    
        	<a href='#' class='basic'>Carta de compromiso</a>
<%  end if %>
    </div>
        </td>
	</tr>
	</table>
	<%end if
	'response.Write "9 <br/>"      
	ObjMatricula.CerrarConexion
	rsCursos.close
	Set rsCursos=nothing
	Set rsHorarios=nothing
	Set ObjMatricula=nothing
	  	  
	'''''''''''''''''''''	
	
	'ExigirCompl=conteoCompl+conteoCurr
	%>
		</td>
		</tr>
	</table>	
	
	</td>
	</tr>
	<tr class="azul">
			<td height="5%" class="pestanarevez" bgcolor="#FFFFCC" width="50%" >
						Total Créditos Preinscritos: <span id="tdTotal"><%=session("credMat")%></span>
						| Total Cursos Preinscritos: <span id="tdCursos"><%=session("cantCursos")%></span> <font color='red'>*</font>
			</td>

			<td align="right" height="5%" width="50%" class="pestanarevez" background="../images/fa.gif">
					<%if session("tipo_cac")="E" then%>
					Pago por inscripción: S/. <span id="lblInscripcion">0</span>&nbsp;
					<%end if%>
					Pensión por ciclo académico: S/. <span id="lblPrecioCurso">0</span>&nbsp;
					<% if session("tipo_Cac")= "E" then %><br/>(Pago en 2 Cuotas) <% end if %><font color='red'>**</font>
			</td>
	</tr>	
	</table>
	<% if tieneMatricula = 0 and codigo_mat = 0 then %>
	<table style="border: 6px double #C0C0C0; width:100%;">
        <tr>
            <td class="style1" style="font-weight: bold" width="50%">
                Elegir número de cuotas (también rige para complementario)</td>
            <td class="style1" >
                </td>
            <td class="style1" align="right" style="color: #0033CC; font-weight: bold">
                Cronograma de pago de pensión:</td>
        </tr>
        <tr>
            <td style="font-weight: bold" width="50%" class="style2">
                <input type="radio" value="5" name="grupoCuotas" onclick="CalcularCuota(this.value, lblPrecioCurso.innerHTML)" checked />Cinco cuotas (05) 
                <div style="display:none;">
                    <%  if(session("tipo_Cac") <> "E") then %>
                            <input type="radio" value="4" name="grupoCuotas" onclick="CalcularCuota(this.value, lblPrecioCurso.innerHTML)" />Cuatro cuotas (04)
                            <!-- <input type="radio" value="5" name="grupoCuotas" onclick="CalcularCuota(this.value, lblPrecioCurso.innerHTML)" checked />Cinco cuotas (05)  -->
                    <%  else  %>
                            <input type="radio" value="1" name="grupoCuotas" onclick="CalcularCuota(this.value, lblPrecioCurso.innerHTML)" />Una cuota (01)
                            <input type="radio" value="2" name="grupoCuotas" onclick="CalcularCuota(this.value, lblPrecioCurso.innerHTML)"/>Dos cuota (02) 
                    <%  end if %>                                            
                </div>                
            </td>
            <td class="style2">
                </td>
            <td class="style2" align="left" width="220px" rowspan="2">
            <%  if (rsCuotas.BOF = false and rsCuotas.EOF=false)  then
                    Do while not rsCuotas.EOF         
                        response.Write(rsCuotas("cuota") & "<br>")
                        rsCuotas.movenext
                    loop
                end if 
            %>
            </td>
        </tr>
        <tr>
            <td>
                <div style="border: 1px solid #000000; width: 279px; background-color: #FFFF00; height: 25px; vertical-align: middle;" 
                    align="center"> Cuota mensual <span id="lblCuota">0</span>
                </div>
            </td>
            <td>
                &nbsp;</td>
        </tr>
    </table>
	<%end if %>
    <font style="font-size:7pt">
	* El sistema automáticamente sumará los cursos y créditos matriculados anteriormente.</br>
	** El monto calculado sólo corresponde a la pensión de los cursos marcados.
	</font>
</div>
<%

'Validar si exije o no complementarios
if trim(session("tipo_Cac"))="E" then
	ExigirCompl=1
end if

'if session("codigo_cpf")=23 then
'	response.write("<p class='rojo'><b>Las asignaturas de 5to y 6to ciclo están sujetas a variación de horario. Cualquier consultar acercarse a dirección de Escuela</b></p>")
'end if
end if 
%>
<input type="hidden" id="txtExigirCompl" name="txtExigirCompl" value="<%=ExigirCompl%>" />	
<input type="hidden" name="credMat" id="credMat" value="<%=session("credMat")%>" />	
<input type="hidden" name="cantCursos" id="cantCursos" value="<%=session("cantCursos")%>" />	
<!-- <input type="hidden" name="NroCuotas" id="NroCuotas" value="<%=nroCuotas%>" />	-->
<input type="hidden" name="NroCuotas" id="NroCuotas" value="5" />
<input type="hidden" name="TieneMatricula" id="TieneMatricula" value="<%=tieneMatricula%>" />	
<div id="basic-modal-content" title="Basic dialog">
			<h3>CARTA DE COMPROMISO</h3>			
			<p>
			Lorem ipsum dolor sit amet, consectetur adipiscing elit. Suspendisse lobortis, lectus vitae venenatis iaculis, mauris felis varius purus, efficitur bibendum felis lectus vitae tortor. Integer scelerisque quam sem, ac eleifend lacus blandit luctus. Phasellus facilisis velit nec purus ornare porta. Cras dui massa, mattis ac porta sit amet, rhoncus ac libero. Morbi quam velit, egestas vel vulputate et, placerat eget purus. Nullam sed varius purus. Proin a diam dolor. Donec ut aliquam dolor. Vestibulum ligula felis, fermentum at sollicitudin quis, semper non nisl. Sed sit amet turpis eget ante faucibus interdum. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Fusce massa lectus, pretium nec varius lacinia, sodales bibendum purus.
			</p>
			<p>
			Curabitur diam neque, venenatis sit amet dictum et, efficitur eget turpis. Donec a suscipit turpis. In hac habitasse platea dictumst. Donec non rutrum sem. Vestibulum accumsan quam sed ipsum vestibulum, at tincidunt enim faucibus. Sed dictum lorem id mollis varius. Ut non diam id neque pulvinar dapibus at sed eros. Sed faucibus enim id enim sollicitudin malesuada. Quisque a velit rhoncus ex rhoncus iaculis. Cras sed feugiat diam, non fringilla eros. Aliquam purus mi, aliquam sed leo sit amet, viverra porttitor ante. Maecenas ullamcorper orci vitae finibus lobortis. Praesent pharetra ipsum eget tortor posuere, at suscipit neque condimentum. Quisque feugiat rutrum iaculis. Proin odio diam, condimentum eget blandit a, commodo in nisl.
			</p>			
			<input type="checkbox" id="chkAcepto" name="chkAcepto" value="1" /> Acepto carta de compromiso
		</div>
		<input type="hidden" id="txtacepta" name="txtacepta" value="0" />
</form>
<%
	If Err.Number<>0 then
	    response.Write "Error: " & Err.Description	    
	    'dim objErr
        'set objErr=Server.GetLastError()
        'objErr.Line
		'response.redirect "mensajes.asp?proceso=B"
	End If
	 %>
</body>
</html>