<!--#include file="../../../../funciones.asp"-->
<!--#include file="../../../../estudiante/cronogramapensiones.asp"-->
<%
if(session("codigo_usu") = "") then
    Response.Redirect("../../../../sinacceso.html")
end if

accion=request.querystring("accion")
codigo_pes=request.querystring("codigo_pes")
codigo_alu=session("codigo_alu")
codigo_cac=session("codigo_cac")
descripcion_cac=session("descripcion_cac")
codigo_cpf=session("codigo_cpf")
nombre_cpf=session("nombre_cpf")
codigo_tfu=session("codigo_tfu")
alto=""


on error resume next
'******************* COM1: Comentado porque repite el proceso en el primer filtro *********************************
'Set objPermite=Server.CreateObject("PryUSAT.clsAccesoDatos")	
'objPermite.AbrirConexion		
'Set rsPermite = objPermite.Consultar("ACAD_VerificaAccesomatricula","FO",codigo_alu,codigo_cac)
'objPermite.CerrarConexion

'if(rsPermite("Apto") = "N") then
'    response.Redirect "frmadminmatricula2015.asp"
'end if
'********************************************* FIN COM1 *********************************************

if trim(codigo_pes)<>"-2" then    
	Set obj=Server.CreateObject("PryUSAT.clsAccesoDatos")	
	obj.AbrirConexion		
	if(Right(descripcion_cac, 1) = "0") then
	    Set rsCursosProgramados= Obj.Consultar("ConsultarCursoProgramadoPorAsesorVerano","FO",1,codigo_alu,codigo_pes,codigo_cac,session("codigo_tfu"),session("cicloRefMat"))
	else
	    Set rsCursosProgramados= Obj.Consultar("ConsultarCursoProgramadoPorAsesorGO","FO",1,codigo_alu,codigo_pes,codigo_cac,session("codigo_tfu"))
	end if		
	set rsAlumno=Obj.Consultar("ConsultarAlumno", "FO", "AUN", codigo_alu)
	
		'Agregado 
	' VERIFICACIÓN DE MATRÍCULA CONDICIONAL
	' SI EL ALUMNO HA DESAPROBADO UN CURSO MAS DE TRES VECES Y AÚN LO TIENE PENDIENTE SÓLO  SE DEBE MATRICULAR EN ESE Y COMO MATRICULA CONDICIONAL
	' YPEREZ 27/07/2011
	
	'set rsVerificarMatCond = Obj.Consultar("verificarMatriculaCondicional", "FO", codigo_alu,codigo_pes,codigo_cac)
	'set rsingresante= Obj.Consultar("ACAD_VerificaIngresante","FO",codigo_alu, codigo_cac)                                        
	'esMatCondicional = 0
	msj = ""
	'if not (rsingresante.BOF and rsingresante.EOF) then
	'    if Not(rsVerificarMatCond.BOF and rsVerificarMatCond.EOF) then
	'        esMatCondicional = 1
	'	    msj = "<td width='18%' style='background-color:Yellow; color:red; border: 1px solid black; padding:5px; font-weight:bold;'>Matricula Condicional</td>"
	'    end if
	'end if
	'fin agregado
	
	
	if Not(rsCursosProgramados.BOF and rsCursosProgramados.EOF) then
		activo=true
		alto="height=""100%"" "
	end if
	
		'--------------------------------------------------
	'consultar los creditos aprobados y el ciclo actual del alumno
	dim rstotales
	'set rstotales=obj.consultar("sp_obtenerciclootros","FO",codigo_alu, codigo_pes)
	set rstotales=Obj.consultar("sp_obtenerciclootros_v1","FO",codigo_alu, codigo_cac, session("codigo_cpf"))
	credaprobados=rstotales.fields("creditosaprobados") 	
    strUltMatricula = rstotales.fields("UltimaMatricula") 	
	strUltimoPromedio = rstotales.fields("UltimoPromedio") 
	NroCursosMatriculados = rstotales.fields("CursosMatriculados") 	
	MensajeReglamento = rstotales.fields("MensajeReglamento") 	
	
	if rstotales.fields("ciclo")<0 then
	    set cicloalumno=0   
	else
	    cicloalumno=rstotales.fields("ciclo")
	end if 
	cicloIng_Alu =rstotales.fields("cicloIng_Alu")  'Hector: 05-08-09
	'************** Campo Agregado en la consulta sp_obtenerciclootros_v1 *********************
	creditosMatriculados = rstotales.fields("CreditoCicloActual")  
	'******************************************************************************************
	llevaIdiomas = rstotales.fields("llevaIdiomas")  	
	set rstotales=nothing
	'-------------------------------------------------
	
	dim rsRequisitos	
	'set rsRequisitos=obj.consultar("ConsultarRequisitosMatricula_v2_2015", "FO", codigo_alu, codigo_pes, codigo_cac)	
	set rsRequisitos=obj.consultar("MAT_ConsultarRequisitosMatriculaAsesor", "FO", codigo_alu, codigo_pes, codigo_cac)	
	obj.CerrarConexion
	Set obj=nothing
	if Not(rsRequisitos.BOF and rsRequisitos.EOF) then
		ponderado = rsRequisitos("Ponderado")
		maxCreditos = rsRequisitos("creditomaximomatricula")
		session("credMat")=rsRequisitos.fields("credMatriculados")
	    session("cantCursos")=rsRequisitos.fields("cantCursos")
	    precioCredito=cdbl(rsRequisitos.fields("precioCredito"))    	    
	    TieneMatricula = iif(rsRequisitos.fields("codigo_mat")>0, 1, 0)
	    if(session("tipo_Cac") <> "E") then 
	        nroCuotas = rsRequisitos.fields("nroCuotas")
	    else
	        nroCuotas = 2
	    end if
	    
	    motivo = rsRequisitos.fields("motivo_mat") 
	    tipo_motivo = rsRequisitos.fields("motivo_mat")
	    
	    if(tipo_motivo = "C") then
	        msj = "<td width='18%' style='background-color:Yellow; color:red; border: 1px solid black; padding:5px; font-weight:bold;'>Matr&iacute;cula con problemas acad&eacute;micos</td>"
	    end if
	    
	    if(tipo_motivo = "P") then
	        msj = "<td width='18%' style='background-color:Yellow; color:red; border: 1px solid black; padding:5px; font-weight:bold;'>Matr&iacute;cula con problemas acad&eacute;micos</td>"
	    end if	    	    
	end if

end if
%>
<html>
<head>

<title>Elija los cursos que desea agregar a la matrícula <%=descripcion_cac%></title>
<link rel="stylesheet" type="text/css" href="../../../../private/estilo.css" />
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<script type="text/javascript" language="JavaScript" src="../../../../private/funciones.js"></script>
<script type="text/javascript" language="JavaScript" src="../../../../private/jq/jquery-1.4.2.min.js"></script>
<!-- <script type="text/javascript" language="JavaScript" src="private/validarfichamatricula_v1-original.js"></script>-->
<script type="text/javascript" language="JavaScript" src="private/validarfichamatricula_v12015.js?v=<% response.Write DateDiff("s", "12/31/1969 00:00:00", Now) %>"></script>
<script type="text/javascript" language="JavaScript" src="../../../../private/jq/lbox/thickbox.js"></script>
<script type="text/javascript" src="private/jquery.js"></script>
<script type="text/javascript" language="javascript">
    $(document).ready(function() {
        $("#cbocodigo_mar").change(function() {            
            if ($("#cbocodigo_mar").val() == "-2") {
                $("#txtMotAyR").hide();
            } else {
                $("#txtMotAyR").show();
            }
        });
    });    
</script>
<link rel="stylesheet" href="../../../../private/jq/lbox/thickbox.css" type="text/css" media="screen" /> 
<style type="text/css">
.bloque {
	border-style: solid none solid none;
	border-width: 1px;
	border-color: #808080;
}
    .style1
    {
        height: 3%;
    }
    .style2
    {
        width: 245px;
        font-size: 9px;
    }
</style>
</head>

<body bgcolor="#EEEEEE"   link="#FFFFFF" vlink="#FFFFFF">
<div id="divCruces">
</div>
<form name="frmFicha" id="frmFicha" method="post" action="procesarmatricula2015.asp?accion=<%=accion%>&codigo_mat=<%=rsRequisitos.fields("codigo_mat")%>">
<input name="CursosProgramados" id="CursosProgramados" type="hidden" value="0"/>
<input name="VecesDesprobados" id="VecesDesprobados" type="hidden" value="0"/>
<input name="txtAccionMat" id="txtAccionMat" type="hidden" value="<%=accion%>"/>
<input name="txtcodigo_cpf" id="txtcodigo_cpf" type="hidden" value="<%=session("codigo_cpf")%>"/>
<input name="txttipo_cac" id="txttipo_cac" type="hidden" value="<%=session("tipo_cac")%>"/>

<input name="esMatCondicional" type="hidden" value="<%=esMatCondicional%>" />

<div id="tblFicha">
    <%'### consultar mensajes bloqueos ###
    Set obj=Server.CreateObject("PryUSAT.clsAccesoDatos")	
    obj.AbrirConexion
	Set rsMensajes=obj.Consultar("VerificarAccesoMatriculaEstudiante_V2","FO","ASE",session("codigo_alu"),codigo_cac) ' ASESOR DE MATRICULA
    obj.CerrarConexion
    dim bloqueado	
    if (rsMensajes.BOF = false and rsMensajes.EOF = false)  then
        response.Write("<h6>El estudiante <font color='#000000' size='2'>" & rsAlumno("apellidoPat_Alu") & " " & rsAlumno("apellidoMat_Alu") & " " & rsAlumno("nombres_Alu")  & "</font> tiene<font color='#FF0000' size='2'> BLOQUEOS</font> activos. <a href='BloqueosMatricula.aspx?cac=" & codigo_cac & "&alu=" & session("codigo_alu") & "&KeepThis=true&TB_iframe=true&height=495&width=500&modal=true' title='Ver Bloqueos' class='thickbox'><font color='#FF0000' style='text-decoration:blink'>(Clic aqui para ver los Bloqueos)</font></a></h6>")
        bloqueado = "No"
        Do while not rsMensajes.EOF         
            if rsMensajes("tipoMensaje_blo") = "NOTA PEND." then 
                bloqueado = "Si"
            end if
            rsMensajes.movenext
        Loop
    end if 

    set obj= nothing
    %>
    <input type="hidden" id="llevoidioma" value="<% response.write llevaIdiomas %>" />
<% if bloqueado <> "Si" then %>    
    <input type="hidden" value="<% response.Write(cicloIng_Alu) %>" name="lblCicloAcademico" id="lblCicloAcademico"/>
    <input type="hidden" value="<% response.Write(session("codigo_alu")) %>" name="lblCicloActual" id="lblCicloActual"/>
    <input type="hidden" value="<% response.Write(session("numCursosDesap")) %>" name="lblNroCursosDesap" id="lblNroCursosDesap"/>
    <input type="hidden" value="<% response.Write(NroCursosMatriculados) %>" name="lblCursosMat" id="lblCursosMat"/>    
    <table cellpadding="0" cellspacing="0" style="border-collapse: collapse; border: 0px solid #C0C0C0; " width="100%" height="94%" <%=alto%>>
	    <tr>
		    <td colspan="5">
		        <h5>Selecionar asignaturas para matrícula <%=descripcion_cac%> </h5>
		    </td>
	    </tr>
	
	<%  Set obj=Server.CreateObject("PryUSAT.clsAccesoDatos")	
        obj.AbrirConexion        
	    FechaInicioCrog=obj.Ejecutar("DevuelveCronogramaInicioAmbiente", True, codigo_cac, "") 
        obj.CerrarConexion
        set obj = nothing        
        %>             
        <!--<tr>
	        <td colspan="3" width="50%"  style="color:Red">
            AMBIENTES POR DEFINIR: Los Ambientes seran mostrados a partir del <% response.write(FechaInicioCrog) %> 
            </td>
            <%response.Write(msj) %>
        </tr>
      -->
        <%        
    %>       
    
	<!--<tr height="5%">
	<td bgcolor="#E1F1FB" bordercolor="#000000"><b><u>Información adicional</u></b></td>
		<td colspan="3" width="80%" bgcolor="#E1F1FB" bordercolor="#000000">&nbsp;</td>        
	</tr>
	<tr>
		<td bgcolor="#FFFFFF" bordercolor="#000000">Observacion : (Max 500 caracteres)</td>
		<td colspan="3" width="100%" bgcolor="#FFFFFF" bordercolor="#000000">
        <input id="txtMotivoAnterior"  type="text" disabled="disabled" value="<%=motivo%>"
                style="width: 100%; background-color: #F0F0F0;" />
         <textarea class="cajas2" rows="4" name="txtMotivo" cols="20" > </textarea>
         </td>	
	</tr>
-->
  <tr style="height:3%" valign="top" class="azul">
    <td width="30%" class="etiqueta">Plan del estudiante</td>
    <td width="65%" colspan="3"><%=session("nombre_cpf") & "(" & session("descripcion_pes") & ")"%>
        <input name="cmdPreMatricula" type="button" value="Actualizar PreRequisito" 
            class="modificar2" style="width:150px; "        
            onclick="ActualizaPreRequisito(<% response.write(codigo_alu) %>,<% response.write(codigo_cac) %>, <% response.write(codigo_pes) %>, '<% response.write(accion) %>')" /> 
        <input name="cmdVerHistorial" type="button" value="Ver Historial" 
            class="buscar2" style="width:100px; "        
            onclick="window.open('../../../../librerianet/academico/historial_personal.aspx?id=<% response.write(codigo_alu) %>', '', 'menubar=no,status=no,toolbar=no,height=500px,width=800,resizable=yes,scrollbars=yes')" /> 
    </td>         
  </tr>
  <tr>
    <td colspan="4">        
       <%         
        
       %>
      <input type="hidden" id="hdtipomat" name="hdtipomat" value="<% response.write(tipo_motivo) %>" />
     <!--  <input type="hidden" id="hdtipomat" name="hdtipomat" value="3" />-->
       <input type="hidden" id="hdmotivo" name="hdmotivo" value="<% response.write(tipo_motivo) %>" />
       <br /> 
    </td>
  </tr>
  <tr>
    <td style="width:10%">
        <div id="agregado-col1">
        Motivo de Agregado: 
	    <%  Set obj=Server.CreateObject("PryUSAT.clsAccesoDatos")
	        Obj.AbrirConexion
	        set rsMotivo = Obj.Consultar("ACAD_BuscaMotivoAyR","FO",0,"A")
	        Obj.CerrarConexion	        
	        Set Obj=nothing
	    %>
	    </div>
    </td>    
    <td colspan="2" style="width:50%">
        <div id="agregado-col2">        
        <% call llenarlista("cbocodigo_mar","",rsMotivo,"codigo_mar","descripcion_mar",codigo_mar,"Seleccione el motivo","","") %>
        <input type="text" id="txtMotAyR" value="" style="width:100%" />
        </div>
    </td>
    <td colspan="2" align="right" style="width:30%">
        <div align="right" style="border: 1px solid #000000; width: 170px; background-color: #FFFF00; height: 22px; vertical-align: middle; text-align:center">
            Cuota mensual <span id="lblCuota">0</span>
        </div>
    </td>
  </tr>
  <tr>
    <td class="usatTablaInfo" colspan="5"><%=MensajeReglamento%></td>    
  </tr>
  <%if (activo=true) then%>
  <tr style="height:5%">  
  <td width="100%" colspan="5" class="usattablainfo" align="left">
  	Cantidad de créditos aprobados : <b><%=credaprobados%></b> | 
    Máximo de Créditos a matricular :  <b><%=maxCreditos%></b> |
    Ultima Matricula: <b><span id="Span1"><% response.Write(strUltMatricula)%> </span></b> | 
	Ultimo Promedio: <b><span id="Span2"><% response.Write(strUltimoPromedio) %> </span></b> 
    <br />
    Créditos matriculados: <b><span id="creditosMat"><% response.Write(creditosMatriculados) 'session("credMat") %> </span></b> | 
	Créditos seleccionados: <b><span id="totalcrd">0 </span></b> |
	Cursos seleccionados :<b><span id="totalcur">0 </span></b>	
	<%  if(Right(descripcion_cac, 1) = "0") then 
	        if(session("max_nivelacion") <> "") then
	            response.Write " | Nivelar hasta: <b><span id='maxNivelacion'>" & session("max_nivelacion") & "</span></b>"
	        end if
	        if(session("max_adelantar") <> "") then
	            response.Write " | Adelantar hasta: <b><span id='maxAdelantar'>" & session("max_adelantar") & "</span></b>"
	        end if        
	    end if 
	%>
	<input name="creditosmatriculados" value="<% response.Write(creditosMatriculados) %>" type="hidden" />	
	</td>
  </tr>
  <tr>
    <td width="100%" colspan="5" height="90%" valign="top">
	    <table border="1" cellpadding="2" cellspacing="0" style="border-collapse: collapse; border-color: #808080; height:99%" width="100%" bgcolor="white">
			
			<tr class="etabla">
				<td width="3%">&nbsp;</td>
				<td width="3%">Tipo</td>	    
				<td width="5%" height="3%">Ciclo</td>
				<td width="8%" height="3%">Código</td>
				<td width="45%" height="3%">Descripción [Veces Desaprobadas]</td>
				<td width="5%" height="3%">Créd.</td>
				<td width="5%" height="3%">TH</td>    
				<td width="5%" height="3%">Req.</td>  
			</tr>
		    <tr>
		        <td width="100%" colspan="8" height="100%">
		        <div id="listadiv" style="height:100%" class="NoImprimir">		
				<table width="100%"  border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse" id="tblcursoprogramado">
				<%	i=0
					codigo_cur=0
					codigo_cup=0
								
					Do while not rsCursosProgramados.eof
						i=i+1
						j=j+1
						HayHorario=false
						imagenrequisito=""
						DebeComplementario = rsCursosProgramados("DebeComplementario")
						DebeIdioma = rsCursosProgramados("DebeIdioma")
												
					    if cdbl(rsCursosProgramados("codigo_cur"))<>cdbl(codigo_cur) then
							    '*******************************************************************************
							    'Agregar el texto (electivo) al nombre del curso
							    '*******************************************************************************	
							    if CBOOL(rsCursosProgramados("electivo_cur"))=true then
								    electivo=1
								    nombre_cur=rsCursosProgramados("nombre_Cur") & "<font color='#0000FF'>(Electivo)</font>"
								else
								    nombre_cur=rsCursosProgramados("nombre_Cur")
								    electivo=0
							    end if
    					
			  				    codigo_cur=rsCursosProgramados("codigo_cur")
			  				    k=k+1

				%>
					<tr valign="top" style="height:15px" onmouseover="Resaltar(1,this,'S')" onmouseout="Resaltar(0,this,'S')" id="curso_padre<%=codigo_cur%>" clase="<%=Clase%>">
					<td class="bloque" width="3%" align="center" class="NoImprimir" onclick="AbrirCurso('<%=codigo_cur%>')">
					<img alt="Ver horarios" src="../../../../images/mas.gif" id="img<%=codigo_cur%>" alt="Imagen"/>
					</td>
					<td onclick="AbrirCurso('<%=codigo_cur%>')" class="bloque" align="center" width="5%"><%=rsCursosProgramados("tipo_Cur")%></td>			
					<td onclick="AbrirCurso('<%=codigo_cur%>')" class="bloque" align="center" width="5%"><%=ConvRomano(rsCursosProgramados("ciclo_Cur"))%>&nbsp;</td>
					<td onclick="AbrirCurso('<%=codigo_cur%>')" class="bloque" width="10%"><%=rsCursosProgramados("identificador_Cur")%></td>
					<% if rsCursosProgramados("NroVecesDes") = 1 then %>
					<td onclick="AbrirCurso('<%=codigo_cur%>')" class="bloque" width="50%"><%response.Write(nombre_cur & " <font color='#FF0000'>[" & rsCursosProgramados("NroVecesDes")) & " vez]</font>"  %></td>
					<% elseif rsCursosProgramados("NroVecesDes") >= 2 then %>
					<td onclick="AbrirCurso('<%=codigo_cur%>')" class="bloque" width="50%"><%response.Write(nombre_cur & " <font color='#FF0000'>[" & rsCursosProgramados("NroVecesDes")) & " veces]</font>"  %></td>
					<% else %>
					<td onclick="AbrirCurso('<%=codigo_cur%>')" class="bloque" width="50%"><%response.Write(nombre_cur)  %></td>
					<% end if  %>
					<td onclick="AbrirCurso('<%=codigo_cur%>')" class="bloque" align="center" width="5%"><%=rsCursosProgramados("creditos_Cur")%></td>			
					<td class="bloque" align="center" width="5%"><%=rsCursosProgramados("totalhoras_Cur")%>  </td>
					<td class="bloque" align="center" width="5%">
					<!--<img src="../../../../images/libroabierto.gif" class="imagen" onClick="AbrirPopUp('../vstrequisitos.asp?codigo_alu=<%=codigo_alu%>&codigo_pes=<%=codigo_pes%>&codigo_cur=<%=codigo_cur%>','400','600','yes','yes','yes')">-->
					<a href="../vstrequisitos.asp?codigo_alu=<%=codigo_alu%>&codigo_pes=<%=codigo_pes%>&codigo_cur=<%=codigo_cur%>&KeepThis=true&TB_iframe=true&height=495&width=500&modal=true" title='Ver Requisitos' class='thickbox'><img src="../../../../images/libroabierto.gif" class="imagen" /></a>
					</td>
					</tr>
					<% end if %>	
					<tr valign="top" style="display:none" id="codigo_cur<%=rsCursosProgramados("codigo_cur")%>">
						<td colspan="7" width="100%" align="right">
						<table style="border-collapse:collapse" width="100%">
						<%	
							clase=""
			  				inicio=Extraercaracter(1,2,rsCursosProgramados("nombre_hor"))
			  				fin=Extraercaracter(1,2,rsCursosProgramados("horafin_Lho"))
							if IsNull(rsCursosProgramados("docente"))=false then
								docente=ConvertirTitulo(rsCursosProgramados("docente"))
							end if
							
							grupo=""
							VacantesDisponibles=""
							clasehorario=""
							activar=false
		
							if cdbl(codigo_cup)<>cdbl(rsCursosProgramados("codigo_cup")) then
								if rsCursosProgramados("soloPrimerCiclo_cup")= true then
								    grupo="GRUPO " & rsCursosProgramados("grupohor_cup") & "<br><font color='blue'> [1er Ciclo]</font>"
								else
								    grupo="GRUPO " & rsCursosProgramados("grupohor_cup") 
								end if 
								codigo_cup=rsCursosProgramados("codigo_cup")
										
								'*******************************************************************************
								'Verificar vacantes disponibles para el GH
								'*******************************************************************************
								VacantesDisponibles=0
								VacantesDisponibles=cdbl(rsCursosProgramados("vacantes_cup"))-cdbl(rsCursosProgramados("nroMatriculados"))
								if rsCursosProgramados("estado_cup")=0 then
									VacantesDisponibles=0
								end if
								
								'*******************************************************************************
								'Mostrar mensaje si se ha matriculado en el CURSO
								'*******************************************************************************
								if rsCursosProgramados("EsCursomatriculado")=0 then
									if VacantesDisponibles<=0 then
										VacantesDisponibles="[GRUPO CERRADO]"
										activar=false
									else
										VacantesDisponibles=VacantesDisponibles & " vacantes disponibles"
										activar=true
									end if
								
								    
								    'Hector Zelada:05/08/09 Complementarios solicita bloqueo para cachimbos
	  			                    'if ((trim(rsCursosProgramados("tipo_cur"))="CO" or trim(rsCursosProgramados("tipo_cur"))="CC") AND int(rsCursosProgramados("creditos_cur"))=0 AND cicloIng_Alu="2009-II") then
                                       
                                       '  if rsCursosProgramados("codigo_cur")=2164 or rsCursosProgramados("codigo_cur")=1025 or rsCursosProgramados("codigo_cur")=1026 or rsCursosProgramados("codigo_cur")=2165  or rsCursosProgramados("codigo_cur")=1145 or rsCursosProgramados("codigo_cur")=1027 or rsCursosProgramados("codigo_cur")=1336 or rsCursosProgramados("codigo_cur")=1028 or rsCursosProgramados("codigo_cur")= 1333 or rsCursosProgramados("codigo_cur")= 2166 or rsCursosProgramados("codigo_cur")=1331 or  rsCursosProgramados("codigo_cur")=1029 or  rsCursosProgramados("codigo_cur")=1030 or  rsCursosProgramados("codigo_cur")=2167 or  rsCursosProgramados("codigo_cur")=1143 or  rsCursosProgramados("codigo_cur")=1334 or rsCursosProgramados("codigo_cur")=1337 or rsCursosProgramados("codigo_cur")=1031 or rsCursosProgramados("codigo_cur")=1032 then
                                            
					                 '       VacantesDisponibles="[GRUPO CERRADO*]"
					                 '       activar=false
					                  '   end if
					                      
                                    'end if
	  			                    '----------------------------------------------
								
								
								end if
								
								if j>1 then clasehorario="class='lineahorario'"
								j=0
							end if
		
		  					obs="Inicio: " & rsCursosProgramados("fechainicio_cup") & " Fin " & rsCursosProgramados("fechafin_cup")
			  				response.write "<tr>"
			  				response.write "<td width='3%' " & clasehorario & "></td>" & vbcrlf			  				
			  				response.write "<td width='10%' " & clasehorario & ">" & grupo & "</td>" & vbcrlf
			  				response.write "<td width='30%' " & clasehorario & ">" & vbcrlf
			  				
			  				if rsCursosProgramados("dia_lho")<>"" OR IsNull(rsCursosProgramados("dia_lho"))=false then
			  				    'Se quitó por solicitud de mfhuidobro porque era información innecesaria 12.01.2015
			  					response.write("- " & ConvDia(rsCursosProgramados("dia_Lho")) & " " & rsCursosProgramados("nombre_hor") & "-" & rsCursosProgramados("horafin_Lho") & "<br>")
								'response.write("&nbsp;&nbsp;" & ConvertirTitulo(rsCursosProgramados("ambiente")) & "(Hrs. " & rsCursosProgramados("tipohoracur_lho") & ")") & vbcrlf
								response.write "</td><td width='40%' " & clasehorario & ">" & vbcrlf
								response.write(docente & "" ) & vbcrlf
							else							    
								response.write "<span class=rojo>[No hay horario registrado]</span>"
								if rsCursosProgramados("codigo_test")<>2 then 'ACTIVAR PARA PROGRAMAS A PESAR QUE NO HAYA HORARIOS
									activar=true
								else
									activar=false
								end if
							end if
							response.write "</td><td width='15%' " & clasehorario & ">" & vbcrlf
							response.write "<span class='rojo'>" & VacantesDisponibles & "</span></td>"
							response.write "<td align='right' width='5%' " & clasehorario  & ">"
							
							if activar=true then
							%>
							<input type="checkbox" onclick="Actualizar(this, <%=maxCreditos%>); CalcularCuota(Hidden1.value, lblPrecioCurso.innerHTML)" 
							            cp='<%=rsCursosProgramados("codigo_cup")%>' tc="<%=rsCursosProgramados("tipo_cur")%>" 
							            cc='<%=rsCursosProgramados("codigo_cur")%>' ciclo="<%=rsCursosProgramados("ciclo_cur")%>" 
							            electivo="<%=electivo%>" value="<%=rsCursosProgramados("creditos_cur")%>" name="chkcursoshabiles" 
							            id='chk<%=rsCursosProgramados("codigo_cup")%>' vd='<%=rsCursosProgramados("NroVecesDes")%>' 
							            obl='<%=rsCursosProgramados("Obligatorio")%>' dei ='<%= DebeIdioma %>' dec = '<%= DebeComplementario %>' 
							            tipocomp = '<%=rsCursosProgramados("tipocomplementario_cur") %>' obligaComp = '<%=rsCursosProgramados("obligaComp") %>'
							            />
							<%
							end if
							response.write "</td>" & vbcrlf
							response.write "</tr>"
						%>
						</table>
						</td>
					</tr>
						<%
					
						rsCursosProgramados.movenext
					loop					
					set rsCursosProgramados=nothing
				%>
				</table>
				</div>
			    </td>
		    </tr>
		</table>
    </td>
  </tr>
  <tr style="font-weight: bold">
    <td colspan="2" align="left" bgcolor="#FFFF66" >
        <% if TieneMatricula = 1 then response.Write("Nro. Cuotas seleccionadas: " & nroCuotas) %></td>
    <td colspan="2" align="right" bgcolor="#FFFF66">
    Pensión por cursos marcados*: S/. <span id="lblPrecioCiclo">0</span>&nbsp;
    </td>
  </tr>  
  <%end if%>
  
  <!--Cambio: form Informacion Adicional, se cambio de lugar -->
     <tr>
	        <td colspan="4" width="50%"  style="color:Red">
            AMBIENTES POR DEFINIR: Los Ambientes seran mostrados a partir del <% response.write(FechaInicioCrog) %> 
            <%response.Write(msj) %>
	
            </td>
    </tr>    
 	<tr>
		<td colspan="4" bgcolor="#FFFFFF" style="border-color: #000000">
		    <div style="position:relative">
		        <div style="position:absolute; top:3px; left: 0px">
		            <% 
                        response.write (session("cuota1") & "</br>")
	                    response.write (session("cuota2") & "</br>")
	                    response.write (session("cuota3") & "</br>")    	    
                    %>
		        </div>
		        <div style="position:absolute; top:3px; left: 20%">
		            <% 
                        response.write (session("cuota4") & "</br>")
	                    response.write (session("cuota5") & "</br>")  
                    %>
		        </div>
		        <div style="position:absolute; left:70%">
                    <input type="button" value="       Cancelar" name="cmdCancelar1" 
                        onclick="location.href='frmadminmatricula2015.asp?modo=resultado&codigo_alu=<%=session("codigo_alu")%>'" 
                        class="noconforme1"/>
                    <input type="button" value="     Guardar matrícula" disabled="false" name="cmdAgregar" id="cmdAgregar" class="conforme1" onclick="EnviarFichaMatricula(<%=codigo_alu%>,<%=codigo_pes%>,<%=codigo_cac%>)"/>
                </div>  
		    </div>		    
		</td>				
	</tr>
    <!--fin cambio -->  
  
  </table>  
<% 
else %>
    <input type="button" value="       Regresar" name="cmdCancelar" onclick="location.href='frmadminmatricula2015.asp?modo=resultado&codigo_alu=<%=session("codigo_alu")%>'" class="regresar2"/>
<%
end if %>  
    </div> 
<input type="hidden" name="credMat" id="credMat" value="<%=session("credMat")%>"/>	
<input type="hidden" name="cantCursos" id="cantCursos" value="<%=session("cantCursos")%>"/>	
<input type="hidden" name="precioCredito" id="lblPrecioCurso" value="<%= CDbl(precioCredito) %>"/>	
<%  if(session("tipo_Cac") <> "E") then %>
    <%
        if(nroCuotas = 0) then
            nroCuotas = 5
        end if
    %>
<%  else 
        nroCuotas = 2
%>    
<%  end if %>
<input type="hidden" name="NroCuotas" id="Hidden1" value="<% response.write(nroCuotas) %>"/>	
<% if (TieneMatricula = 0) then %>
        <%  if(session("tipo_Cac") <> "E") then %>                       
            <input type="radio" value="5" name="grupoCuotas" checked="checked" onclick="CalcularCuota(this.value, lblPrecioCiclo.innerHTML)" style="visibility:hidden"/> <!-- Cinco cuotas (05) -->
        <%  else  %>
                <input type="radio" value="1" name="grupoCuotas" onclick="CalcularCuota(this.value, lblPrecioCurso.innerHTML)" style="visibility:hidden"/> 
                <input type="radio" value="2" checked="checked" name="grupoCuotas" onclick="CalcularCuota(this.value, lblPrecioCurso.innerHTML)" style="visibility:hidden"/>
        <%  end if %>   
<% end if  %>

<input type="hidden" name="TieneMatricula" id="TieneMatricula" value="<%=tieneMatricula%>"/>	
<textarea style="visibility:hidden" rows="4" id="txtMotivo" name="txtMotivo" cols="20" > </textarea>
<input id="txtMotivoAnterior"  type="text" disabled="disabled" value="<%=motivo%>" style="width: 50%; visibility:hidden; background-color: #F0F0F0;" />
</form>
<table id="tblmensaje" border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse;display:none" bordercolor="#111111" width="100%" height="100%" class="contornotabla">
	<tr>
	<td width="100%" align="center" class="usatTitulo" bgcolor="#FEFFE1">
	Procesando<br />
	Por favor espere un momento...<br />
	<img border="0" src="../../../../images/cargando.gif" width="209" height="20" alt="Imagen" />
	</td>
	</tr>
</table>
<%
    If Err.Number <> 0 Then  
        Response.Write (Err.Description& "<br/>")                     
    End If
%>
<script type="text/javascript" language="JavaScript" src="../private/analytics-personal.js"></script>
</body>
</html>