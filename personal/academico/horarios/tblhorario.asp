<!--#include file="../../../funciones.asp"-->
<%
dim rsHorario
dim rsHorarioTemp

on error resume next
modo=request.querystring("modo")
test=request.querystring("mod")
codigo_cpf=request.querystring("codigo_cpf")
codigo_cup=request.querystring("codigo_cup")
codigo_amb=request.querystring("codigo_amb")

codigo_cac=request.querystring("codigo_cac")
codigo_per=request.querystring("codigo_per")
th=request.querystring("th")
    
tp=request.querystring("tp") 'Total horas del Plan de Estudio 31/10/2011
codigo_tfu=session("codigo_tfu")
mat=request.querystring("mat")
Habilitar=request.QueryString("Hab")
Agregar=false
Modificar=false
Eliminar=false

if (codigo_amb="" or codigo_amb="-2") then codigo_amb=0
if codigo_cup="" then codigo_cup=0
if codigo_per="" then codigo_per=1
if modo="" then modo="C"

Set Obj= Server.CreateObject("PryUSAT.clsAccesoDatos")
	obj.AbrirConexion	
							
		if codigo_cup<>0 or codigo_amb<>0 then
		    'Agregado por mvillavicencio 09/11/2011. El so triplica el th cuando sea ciclo de verano.	        
	        Set rsCiclosVerano = obj.Consultar("PER_VerificarCiclosVerano", "FO", codigo_cac, th)
	        
	        if not(rsCiclosVerano.BOF and rsCiclosVerano.EOF) then				    
                thorasmenor = rsCiclosVerano("menor")
                thorasmayor = rsCiclosVerano("mayor")
	        end if 
        	
        	Set rsCursoVerano = obj.Consultar("ACAD_VerificaVeranoComplementario", "FO", codigo_cup)
        	if not(rsCursoVerano.BOF and rsCursoVerano.EOF) then				    
        	    thorasmenor = 10
                thorasmayor = 10
        	end if
        	
			if modo="A" then
				Set rsProfesor=obj.Consultar("ConsultarCargaAcademica","FO","EXC",codigo_cup,0)
				set rsCursoProg = obj.Consultar("ConsultarCursoProgramadoVacantes","FO", codigo_cup)
				

				if Not(rsProfesor.BOF and rsProfesor.EOF) then
					HayProfesor=true
					if codigo_per="1" then codigo_per=rsProfesor("codigo_per")
				else
					codigo_per=1
				end if
				if not(rsCursoProg.BOF and rsCursoProg.EOF) then
				    Vacantes = rsCursoProg("vacantes_Cup")
				end if
				

			
				'******************************************************
				'Mostrar ambientes asignados de acuerdo a fechas
				'******************************************************						
				Set rsFechaAsignadas=obj.Consultar("ConsultarHorariosAmbiente","FO",0,codigo_amb,codigo_cac,codigo_cpf,0)				
				'Set rsCombo=obj.Consultar("ACAD_ListaAmbientexEscuela","FO",codigo_cac,codigo_cpf)				
				if Not(rsFechaAsignadas.BOF and rsFechaAsignadas.EOF) then
					HayFechas=true
				end if
			end if
		end if
		
		docente=iif(codigo_per=1,0,codigo_per)
		fechaini=request.querystring("fechaini")
		fechafin=request.querystring("fechafin")
		
		if fechaini="" then fechaini=null
		if fechafin="" then fechafin=null

		'******************************************************
		'Pintar las horas del curso o el profesor
		'******************************************************
		'Set rsHorario=obj.Consultar("ConsultarHorarioDisponible","FO",codigo_cac,codigo_cup,docente,codigo_amb) '-- Cambio 10.06.15
		'response.Write("<script>alert('docente:" &  docente &"')</script>")
		'response.Write("<script>alert('cup:" &  codigo_cup &"')</script>")
		'response.Write("<script>alert('codigo_amb:" &  codigo_amb &"')</script>")
		
		'response.Write(docente)
		
		Set rsHorario=obj.Consultar("ConsultarHorarioDisponibleV3","FO",codigo_cac,codigo_cup,docente,codigo_amb)
		Set rsHorarioTemp=obj.Consultar("ConsultarHorarioDisponibleV3","FO",codigo_cac,codigo_cup,docente,codigo_amb)
		
		'******************************************************************
		'EPENA 11/05/2018:    Pintar las horas disponibles del profesor
		'*******************************************************************
		
		Set rsHorarioDisponible=obj.Consultar("ConsultarHorariosAmbiente","FO","21",codigo_cac,docente,"","")
		
		
		
		codigo_usu =session("codigo_usu")
		'******************************************************
		'Validar los permisos para horarios
		'******************************************************
		'set rsPermisos=obj.Consultar("ValidarPermisoAccionesEnProcesoMatricula","FO","0",codigo_cac,session("codigo_usu"),"lineahorario")
		set rsPermisos=obj.Consultar("ValidarPermisoAccionesEnProcesoMatriculav2","FO","0",codigo_cac,session("codigo_usu"),"lineahorario",test)
		
		if not(rsPermisos.BOF and rsPermisos.EOF) then
			Agregar=rsPermisos("agregar_acr")
			Modificar=rsPermisos("modificar_acr")
			Eliminar=rsPermisos("eliminar_acr")
		end if
				
		'Asignar permisos a Evaluación y Registros, Administrador,Programas especiales y si el ciclo >ciclo actual
		if (session("codigo_tfu")=1 OR codigo_cpf=25) then
			Agregar=true
			Modificar=true
			Eliminar=true
			'response.write("Sin Permisos")
		end if
		
	obj.CerrarConexion
Set Obj=nothing

function AnchoHora(byVal cad)
	if len(cad)<2 then
		AnchoHora="0" & cad
	else
		anchohora=cad
	end if
end function




%>
<html>
<head>
<meta http-equiv="Content-Language" content="es" />
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<title>horario</title>
<link rel="stylesheet" type="text/css" href="../../../private/estilo.css">
<script type="text/javascript" language="JavaScript" src="../../../private/funciones.js"></script>
<script type="text/javascript" language="JavaScript" src="../../../private/tooltip.js"></script>
<style type="text/css">
td {
	font-size: xx-small;
	text-align: center;
}
.CU {
	background-color: #FFCC00;
	<%'if modo="A"  then%>
	cursor:hand
	<%'end if%>	
}
.DE2{
	background-color:gray;
	<%'if modo="A"  then %>
	cursor:hand;
	<%'end if%>	
}
 
.AU {
	background-color: #FF3300;
	<%if modo="A" then%>
	cursor:hand
	<%end if%>
}
.PR {
	background-color: #9999FF;
	<%if modo="A" then%>
	cursor:hand
	<%end if%>
}
.etiquetaTabla {
	background-color: #EAEAEA;
	color: #0000FF;
}
</style>
<script type="text/javascript" language="Javascript">
var contador=0
var fechasBD = new Array()

function pintaHora(celda)
{
   // alert('<%=modo%>');
   // alert(celda.className);
	if ((celda.className=="AU" || celda.className=="PR" || celda.className=="CU" ) && ("<%=modo%>"=="A")){
		var codigo_per=document.all.cbocodigo_per
		if (codigo_per!=undefined){
			codigo_per=document.all.cbocodigo_per.value
		}
		else{
			codigo_per=1
		}
		
		//AbrirPopUp('lsthorarioregistrado.asp?modo=' + celda.className + '&dia=' + celda.id + "&codigo_cup=<%=codigo_cup%>&codigo_per=" + codigo_per + "&codigo_amb=" + cbocodigo_amb.value + "&codigo_cac=<%=codigo_cac%>&codigo_cpf=<%=codigo_cpf%>",'400','700','yes','yes','yes')
		
		var codigo_amb = 0;
		codigo_amb=document.all.cbocodigo_amb.value
		
		
		    
		
	   //YPEREZ 24/09/2018 Cambio de versión a asp.net + opción editar
		AbrirPopUp('lsthorarioregistradov2.aspx?modo=' + celda.className + '&dia=' + celda.id + "&codigo_cup=<%=codigo_cup%>&codigo_per=" + codigo_per + "&codigo_usu=<%=codigo_usu%>&codigo_amb=" + codigo_amb + "&codigo_cac=<%=codigo_cac%>&codigo_cpf=<%=codigo_cpf%>",'400','850','yes','yes','yes')
	
	}
	else
	{   var obj =document.getElementById("hddHabilitar")
        //alert(document.getElementById("hddHabilitar").value)
		if ("<%=modo%>"=="A"){
		
			
			var Bandera=true
			cmdGuardar.disabled=true			
		
			/*Si la hora está libre*/
			if ("<%=codigo_amb%>"=="0" || "<%=codigo_cup%>"=="0"){
				if ("<%=codigo_amb%>"=="0"){
					alert("Seleccione en qué ambiente asignará el Horario")			
				}
				else{
					alert("Seleccione la asignatura para Asignar Horario")
				}
				return(false)
			}
			
			if ( celda.className=="" && celda.ded!="1" && (celda.per=="0" || celda.per=="1")==false ){
			    alert('AVISO: El horario seleccionado no corresponde a la disponibildiad registrada por el docente!');
			}
		
			
			if (obj.value == 1){   
			if (celda.className=="Selected"){
				    //celda.className="SelOff"
				    celda.className=celda.css;
				    celda.innerHTML="&nbsp;"
				    tdMarcas.innerHTML=eval(tdMarcas.innerText)-1				    				    
				    
				    //Obtiene diferencia. Por mvillavicencio el 15/11/11
				    if (document.getElementById('tdFaltantesMenor'))  tdFaltantesMenor.innerHTML=eval(tdFaltantesMenor.innerText)-1
				    if (document.getElementById('tdFaltantesMayor'))  tdFaltantesMayor.innerHTML=eval(tdFaltantesMayor.innerText)-1
				    if (document.getElementById('tdFaltantes'))       tdFaltantes.innerHTML=eval(tdFaltantes.innerText)-1

				    //Asignar signo positivo. Por mvillavicencio el 15/11/11				     
				     if (document.getElementById('tdFaltantesMenor') && document.getElementById('tdFaltantesMayor'))
				     {
					    //al tdmenor
					    if (tdFaltantesMenor.innerHTML > 0 )
					    {
		                   tdsignoMenor.innerHTML= "+";
		                }
		                else
		                {
		                   tdsignoMenor.innerHTML= "&nbsp;";
		                }
		                
		                //al tdmayor
					    if (tdFaltantesMayor.innerHTML  > 0 )
					    {
		                   tdsignoMayor.innerHTML= "+";
		                }
		                else
		                {
		                   tdsignoMayor.innerHTML= "&nbsp;";
		                }
		             }
		             
		             if (document.getElementById('tdFaltantes'))
		             {   
		                 //al td unico
					    if (tdFaltantes.innerHTML  > 0 )
					    {
		                   tdsigno.innerHTML= "+";
		                }
		                else
		                {
		                   tdsigno.innerHTML= "&nbsp;";
		                }
		              }				    
			    }
			    else{
			        //Se agregó esta validación. 14/11/2011 mvillavicencio y dguevara
			        Bandera=true
			        if (eval(tdMarcas.innerText)+1><%=thorasmayor%>){
					    Bandera=false
					    alert("Ha sobrepasado las horas del curso, no puede seguir marcando el horario.");
					 }
					    
				    /*if (eval(tdMarcas.innerText)+1><%'=th%>){
					    Bandera=false
					    if (confirm("Ha sobrepasado las horas del curso\n¿Desea seguir marcando el horario")==true){
						    Bandera=true
					    }
				    }
				    Bandera=true*/
    			    
				    if (Bandera==true){
					    celda.className="Selected"
					    celda.innerHTML='<img src="../../../images/bien.gif">'
					    tdMarcas.innerHTML=eval(tdMarcas.innerText)+1
					    
					    //Obtiene diferencia. Por mvillavicencio el 15/11/11					    
					    if (document.getElementById('tdFaltantesMenor'))  tdFaltantesMenor.innerHTML=eval(tdFaltantesMenor.innerText)+1
					    if (document.getElementById('tdFaltantesMayor'))  tdFaltantesMayor.innerHTML=eval(tdFaltantesMayor.innerText)+1
					    if (document.getElementById('tdFaltantes'))       tdFaltantes.innerHTML=eval(tdFaltantes.innerText)+1
					    
					    //Asignar signo positivo. Por mvillavicencio el 15/11/11
					    //al tdmenor
					    if (document.getElementById('tdFaltantesMenor') && document.getElementById('tdFaltantesMayor'))
					    {
					        if (tdFaltantesMenor.innerHTML > 0 )
					        {
		                       tdsignoMenor.innerHTML= "+";
		                    }
		                    else
		                    {
		                       tdsignoMenor.innerHTML= "&nbsp;";
		                    }
    		                
		                    //al tdmayor
					        if (tdFaltantesMayor.innerHTML  > 0 )
					        {
		                       tdsignoMayor.innerHTML= "+";
		                    }
		                    else
		                    {
		                       tdsignoMayor.innerHTML= "&nbsp;";
		                    }
		                 }
		                
		                //al td unico
		                if (document.getElementById('tdFaltantes'))
		                {
					        if (tdFaltantes.innerHTML  > 0 )
					        {
		                       tdsigno.innerHTML= "+";
		                    }
		                    else
		                    {
		                       tdsigno.innerHTML= "&nbsp;";
		                    }
		                }
		                    
		                  
				    }
			    }
			
			    if (eval(tdMarcas.innerText)>0){
				    cmdGuardar.disabled=false
			    }
			 }
		
		
			
		
		
		
		}
	
	}

}
function ConsultarHorarioAmbientes()
{
   var capacidadAula 
   var vacantes
   var aula
   aula = document.getElementById("cbocodigo_amb").options[document.getElementById("cbocodigo_amb").selectedIndex].text
   inicio= aula.indexOf('[')
   fin = aula.indexOf(']') 
   capacidadAula = parseInt(aula.substring(inicio + 4, fin))
   vacantes =  document.getElementById("HddVacantes").value
      
   if (capacidadAula< vacantes)
   {    alert('No puede asignar un ambiente con menor capacidad a la del curso programado')
        document.getElementById("hddHabilitar").value = 0
   }
   else
   {   
        document.getElementById("hddHabilitar").value = 1
   }
   //alert(document.getElementById("hddHabilitar").value)     
  
   ConsultarHorarios() 

}

function ConsultarHorarios()
{
	var codigo_amb=document.all.cbocodigo_amb
	var codigo_per=document.all.cbocodigo_per
	var fechaini=""
	var fechafin=""
	
	if (codigo_per!=undefined){
		codigo_per=document.all.cbocodigo_per.value
	}
	else{
		codigo_per=1
	}

	if (codigo_amb!=undefined){
		/*Asignar los valores según el ambiente y fecha*/
		if (cbocodigo_amb.value==-1)
			{codigo_amb=0}
		else{
			var i=cbocodigo_amb.selectedIndex-1
			fechaini=fechasBD[i].inicio
			fechafin=fechasBD[i].fin
			/*codigo_amb=fechasBD[i].ambiente
			alert ('ads');*/
			codigo_amb=document.all.cbocodigo_amb.value;
		}
	}
	else{
		codigo_amb=0
	}
	var obj =document.getElementById("hddHabilitar")
	Habilitar = obj.value
	location.href="tblhorario.asp?modo=A&codigo_cup=<%=codigo_cup%>&codigo_amb=" + codigo_amb + "&codigo_cac=<%=codigo_cac%>&th=<%=th%>&codigo_cpf=<%=codigo_cpf%>&codigo_per=" + codigo_per + "&fechaini=" + fechaini + "&fechafin=" + fechafin + "&Hab=" + Habilitar
   
}

//No se usa porque el total de horas programas puede ser menor o igual que las del plan.
/*
function validaTotalHorasProgramadas()
{    
   var thoras =  <%=thoras%>      
   var th = <% = th%>
   		  	
   var marcas = eval(tdMarcas.innerText);   
   //Se agregó esta validación 14/11/2011 mvillavicencio y dguevara. 
   //El total de horas debe ser igual que el Plan, para ciclos regulares.
   if (thoras == th && marcas < thoras) //si es ciclo regular
   {
        alert("El total de horas programadas debe ser igual a las Horas del Curso (" + thoras + " hrs.)");
        return false;
   }
   
   return true;   
}*/

function GuardarHorario()
{
	var Marcas=""
	var codigo_per=document.all.cbocodigo_per
	var codigo_daa=document.all.cbocodigo_amb //codigo de detalle asignacion ambiente
	
	//if (validaTotalHorasProgramadas())
	//{
	    for (var d=0;d<6;d++){
		    switch (d)
		    {
			    case 0:
				    dia=document.all.LU
				    break
			    case 1:
				    dia=document.all.MA
				    break
			    case 2:
				    dia=document.all.MI
				    break				
			    case 3:
				    dia=document.all.JU
				    break
			    case 4:
				    dia=document.all.VI
				    break				
			    case 5:
				    dia=document.all.SA
				    break	
		    }
    		
		    for (var i=0;i<dia.length;i++){
			    if (dia[i].className=="Selected"){
				    Marcas+= d + '' + dia[i].hora + ","
			    }
		    }
	    }

	    if (codigo_per==undefined){
		    codigo_per=1
	    }
	    else{
		    codigo_per=document.all.cbocodigo_per.value
	    }

	    if (Marcas!=""){
		    var ini=""
		    var fin=""
    		
		    <%if isnull(fechaini)=false then
			    ini=replace(fechaini," ","e")
			    ini=replace(ini,":","d")
			    ini=replace(ini,"/","x")
			    ini=replace(ini,".","q")
    			
			    fin=replace(fechafin," ","e")
			    fin=replace(fin,":","d")
			    fin=replace(fin,"/","x")
			    fin=replace(fin,".","q")
		    %>
			    ini="<%=ini%>"
			    fin="<%=fin%>"
		    <%end if%>
		    location.href="procesar.asp?Accion=registrarhorario&Marcas=" + Marcas + "&codigo_cup=<%=codigo_cup%>&codigo_cac=<%=codigo_cac%>&codigo_amb=" + cbocodigo_amb.value + "&tipo_lho=" + cbotipo_Lho.value + "&codigo_per=" + codigo_per + "&th=<%=th%>&codigo_cpf=<%=codigo_cpf%>&fechaini=" + ini + "&fechafin=" + fin + "&codigo_daa=" + codigo_daa 
	    }
	    else{
		    alert("Debe definir el horario de la asignatura, marcando las celdas según el día y la hora")
	    }
	//}   
}

function ObjFechas(fi,ff,ca)
{
	this.inicio=fi
  	this.fin = ff
  	this.ambiente=ca
}

function AgregarItemObjeto()
{	
	<%if HayFechas=true then
		Do while Not rsFechaAsignadas.EOF
		response.write "fechasBD[fechasBD.length]=new ObjFechas('" & rsFechaAsignadas("fechaini_daa") & "','" & rsFechaAsignadas("fechafin_daa") & "','" & rsFechaAsignadas("codigo_amb") & "')" & vbNewLine & vbtab & vbtab
	
		rsFechaAsignadas.movenext
	Loop
	end if%>
}
</script>
</head>

<body style="background-color: #DCDCDC;" onLoad="AgregarItemObjeto()">
<%if modo="A" then%>
    <table border="0" cellpadding="2" cellspacing="0" style="border-collapse: collapse; border-color:#111111" align="center" width="100%" height="5%">
        <tr>
            <td width="40%">
            <%if HayFechas=true and modo="A" then%>
                <select name="cbocodigo_amb"  onChange="ConsultarHorarioAmbientes();">
		            <option value="-1">[Seleccione Ambiente y Fechas asignadas]</option>
	        <%
	                rsFechaAsignadas.movefirst
	                Do while Not rsFechaAsignadas.EOF
		                if isnull(fechaini)=false then
                            ' se cambio codigo daa amb
			                if cdbl(codigo_amb)=cdbl(rsFechaAsignadas("codigo_daa")) AND _
				                cdate(fechaini)=cdate(rsFechaAsignadas("fechaini_daa")) AND _
				                cdate(fechafin)=cdate(rsFechaAsignadas("fechafin_daa")) then
				                marcado="SELECTED"
			                else
				                marcado=""
			                end if
		                end if
        	            
	                    If session("codigo_tfu") = 25 or session("codigo_tfu") = 18 or session("codigo_tfu") = 1 then
	                    %>
	                        <option value="<%=rsFechaAsignadas("codigo_daa")%>" <%=marcado%>><%response.Write(rsFechaAsignadas("ambiente") & " {" & rsFechaAsignadas("ambienteReal") & "} ")%> [Cap <%=rsFechaAsignadas("capacidad_Amb") %>] (<%=rsFechaAsignadas("fechaini_daa")%> hasta <%=rsFechaAsignadas("fechafin_daa")%>)</option>
	                    <%else	%>
                            <option value="<%=rsFechaAsignadas("codigo_daa")%>" <%=marcado%>><%=rsFechaAsignadas("ambiente")%> [Cap <%=rsFechaAsignadas("capacidad_Amb") %>] (<%=rsFechaAsignadas("fechaini_daa")%> hasta <%=rsFechaAsignadas("fechafin_daa")%>)</option>	
	                    <%end if 
	                rsFechaAsignadas.movenext
	                Loop
	        %>
	            </select>
	        <%
	        elseif modo="A" then
		        response.write "<tr><td colspan=2>[Solicite la habilitación de ambientes a Dirección Académica]</td></tr>"
            end if
	        %>
            </td>            
        </tr>
        <tr align="left">            
            <td width="100%">
            <%
                if HayProfesor=false then
            	    response.write "<b>DOCENTE NO DEFINIDO</b><br/>"
                else
	                call llenarlista("cbocodigo_per","ConsultarHorarios()",rsProfesor,"codigo_per","funciondocente",codigo_per,"","","")
	            end if
            %>
            </td>                    
        </tr>
    </table>  
<%end if%>
<table width="100%" style="border-collapse: collapse; height:92%; border-color:#CCCCCC" border="0" cellpadding="0">
<tr>
<td width="80%" >
<%
function fnBuscaClaseCelda(byval dia, byval hora, byval horaI, byval horaF)

dim css
css=""

'response.Write("dia: " & cstr(dia) &  "____<br>")
if Not(rsHorarioTemp.BOF and rsHorarioTemp.EOF) then
				rsHorarioTemp.movefirst
			
				Do while not rsHorarioTemp.EOF
				    if(mid(rsHorarioTemp("dia_lho"),1,2)=dia  AND int(hora)>=int(mid(rsHorarioTemp("nombre_hor"),1,2)) AND int(hora)<int(mid(rsHorarioTemp("horafin_lho"),1,2))) then
				      '      response.Write("ambiente: " & rsHorarioTemp("ambiente") & "HI: " &  rsHorarioTemp("nombre_hor") & "HF: " &  rsHorarioTemp("horafin_lho") & "css: " & rsHorarioTemp("css") & "<br>" )				    
				    css=rsHorarioTemp("css") 
				      				    
				    end if        
			        rsHorarioTemp.movenext
			  
				Loop
end if	
'response.Write( css & "<br>")
fnBuscaClaseCelda= css
			

End function


dim clasetd
dim dia,hora
dim diaBD,inicioBD,finBD
dim TextoCelda
dim marcas
dim dedicacion
dedicacion=""

marcas=0
response.write "<table id='tblHorario' style='border-collapse: collapse;' width='100%' height='100%' border='1' bgcolor='white' bordercolor='#CCCCCC'>" & vbcrlf
response.write vbtab & "<tr class='etiquetaTabla' height='8%'>" & vbcrlf
response.write vbtab & "<th width='30%'>Horas</th>" & vbcrlf
response.write vbtab & "<th width='10%'>Lunes</th>" & vbcrlf
response.write vbtab & "<th width='10%'>Martes</th>" & vbcrlf
response.write vbtab & "<th width='10%'>Miércoles</th>" & vbcrlf
response.write vbtab & "<th width='10%'>Jueves</th>" & vbcrlf
response.write vbtab & "<th width='10%'>Viernes</th>" & vbcrlf
response.write vbtab & "<th width='10%'>Sábado</th>" & vbcrlf
response.write vbtab & "</tr>" & vbcrlf

for f=1 to 16
	response.write vbtab & "<tr height='7%'>" & vbcrlf
	
	for c=0 to 6
		if c=0 then	
		    'linea que genera el la columna de horas	
			'response.write vbtab & "<td width='30%' height='7%'  class='etiquetaTabla'>" & f+6 & ":10 - " & f+1+6 & ":00</td>"
			response.write vbtab & "<td width='30%' height='7%'  class='etiquetaTabla'>" & f+6 & ":00 - " & f+1+6 & ":00</td>"
		else
			
			if c=1 then dia="LU"
			if c=2 then dia="MA"
			if c=3 then dia="MI"
			if c=4 then dia="JU"
			if c=5 then dia="VI"
			if c=6 then dia="SA"
			
			hora=AnchoHora(f+6)
		
			TextoCelda=vbtab & "<td width='10%' onClick='pintaHora(this)' id='" & dia & "' hora='" & hora & "' "
			
			'Si hay horario
			if Not(rsHorario.BOF and rsHorario.EOF) then
				rsHorario.movefirst
			
				Do while not rsHorario.EOF
				
					diaBD=mid(rsHorario("dia_lho"),1,2)
					inicioBD=mid(rsHorario("nombre_hor"),1,2)
					finBD=mid(rsHorario("horafin_lho"),1,2) 
					
					if dedicacion="" then
					dedicacion=rsHorario("codigo_ded")
		            end if 
					'si el día es el mismo y la horaactual es menor que horafin y mayor que la horainicio
					if trim(dia)=trim(diaBD) AND int(hora)>=int(inicioBD) AND int(hora)<int(finBD) then
					    
						temp=replace(rsHorario("color_hor"),"""","'")
						if trim(temp)="class='CU'" then
							marcas=marcas+1
    						TextoCelda=TextoCelda & " tooltip='" & rsHorario("ambiente") & "' "	
						end if					
						
						clasetd=fnBuscaClaseCelda(diaBD,hora,inicioBD,finBD)
						TextoCelda=TextoCelda & "class='" & clasetd & "'" 'rsHorario("color_hor")
					    TextoCelda=TextoCelda & " css=" &   rsHorario("css")
					
					'response.Write(fnBuscaClaseCelda(diaBD,hora,inicioBD,finBD))
					end if
					
					rsHorario.movenext
				Loop
			end if
			
			
		    TextoCelda=TextoCelda & " ded='" & dedicacion & "' " 
		    TextoCelda=TextoCelda & " per='" & codigo_per & "' " 
		    
		    
		    
			TextoCelda=TextoCelda &   ">"  
			 
			'CAMBIO YPEREZ 14.03.17
			'Si hay horario
			if Not(rsHorario.BOF and rsHorario.EOF) then
				rsHorario.movefirst
			
				Do while not rsHorario.EOF
					diaBD=mid(rsHorario("dia_lho"),1,2)
					inicioBD=mid(rsHorario("nombre_hor"),1,2)
					finBD=mid(rsHorario("horafin_lho"),1,2)
		
					'si el día es el mismo y la horaactual es menor que horafin y mayor que la horainicio
					if trim(dia)=trim(diaBD) AND int(hora)>=int(inicioBD) AND int(hora)<int(finBD) then
						temp=replace(rsHorario("color_hor"),"""","'")
						if trim(temp)="class='CU'" then							
							TextoCelda=TextoCelda & rsHorario("ambiente") 
							
						end if
						
					end if
					rsHorario.movenext
				Loop
			end if
			'CAMBIO fin YPEREZ 14.03.17
			
			TextoCelda=TextoCelda & "</td>" & vbcrlf
			
			response.write TextoCelda
		end if
	next
	response.write vbtab & "</tr>" & vbcrlf
next

response.write "</table>"


%>
</td>
<td width="20%" valign="top" align="right">
<table width="100%" align="right">
<%if modo="A" then%>
	<tr>
		<td colspan="8">Tipo de Horario</td>
	</tr>
	<tr>
		<td colspan="8">	
	    <select size="1" name="cbotipo_Lho" class="cajas">
		<option value="T" >Teoría</option>
		<option value="P">Práctica</option>
		</select>
		</td>
	</tr>
<%end if
if cbool(Agregar)=true then
	%>
	<tr>
		<td class="rojo" colspan="8">
			<input name="cmdNuevo" type="button" value="Nuevo" class="agregar2" onClick="ConsultarHorarios()" />
		</td>
	</tr>
<%elseif cbool(Modificar)=false then%>
	<tr><td colspan="8">No está habilitado para añadir horarios en el ciclo académico seleccionado</td></tr>
<%end if
if marcas>0 and cbool(Modificar)=true then%>
	<tr>
	<td colspan="8">
		<input name="cmdModificar" type="button" value="  Modificar" class="modificar2" onClick="ConsultarHorarios()" />
	</td>
	</tr>
<%elseif cbool(Agregar)=false then%>
	<tr><td colspan="8">No está habilitado para Modificar horarios en el ciclo académico seleccionado</td></tr>
<%end if

if modo="A" then%>	
	<tr>
		<td colspan="8">
		<input name="cmdGuardar" id="cmdGuardar" type="button" value="Guardar" class="guardar2" disabled="true" onClick="GuardarHorario()" />		
		</td>
	</tr>
<%end if%>

	<tr>
		<td class="etiqueta" colspan="8">
		Horas del Curso</td>
	</tr>
	<tr>
		<td class="rojo" colspan="8">						
		<b>
		<% if (thorasmayor <> thorasmenor) then %>
		    Mínimo: <% =thorasmenor %> | Máximo: <% =thorasmayor %>
		   <% else %>
		    <% =thorasmenor %>
		<% end if %>		
		</b></td>
		
	</tr>
	<tr>
		<td class="etiqueta" colspan="8">
		Horas Marcadas</td>
	</tr>	
	<tr>
		<td id="tdMarcas" class="azul" colspan="8">
		<%=marcas%></td>
	</tr>
	<tr>
		<td class="etiqueta" colspan="8">
		Diferencia</td>
	</tr>	
	<tr>		
	     <% if (thorasmayor <> thorasmenor) then %> 
	     <td style="width:30%;">Mínima: </td>
	     <td id="tdsignoMenor" style="width:5%;">
		    <%If (marcas-thorasmenor) > 0 Then %>
		    +
		    <%  else%>
		    &nbsp;
		    <%End If%>
		   </td>
		   <td id="tdFaltantesMenor" class="azul" style="width:10%;" align="right">
		    <%=marcas-thorasmenor%></td>
	       <td style="width:35%;"> | Máxima: </td>
	       <td id="tdsignoMayor" style="width:5%;">
		    <%If (marcas-thorasmayor) > 0 Then %>
		    +
		    <%  else%>
		    &nbsp;
		    <%End If%>
		   </td>
		   <td id="tdFaltantesMayor" class="azul" style="width:10%;" align="left">
		    <%=marcas-thorasmayor%></td>
		    <% else %>		   
		    <td id="td1" style="width:40%;">
		    <td id="tdsigno" style="width:10%;">
		    <%If (marcas-thorasmenor) > 0 Then %>
		     +
		    <%  else%>
		     &nbsp;
		    <%End If%>
		   </td>
		   <td id="tdFaltantes" class="azul" style="width:10%;">
		    <%=marcas-thorasmenor%></td>
		    <td id="td2" style="width:40%;">
		    <% end if %> 
		   </td>		
	</tr>
	<tr>
	<td class="DE2"> &nbsp;</td>
	<td colspan="9" style="text-align:left" >Disponibilidad Docente</td>
	</tr>
	<tr>
	<td class="CU">&nbsp;</td>
	<td colspan="9" style="text-align:left">Horario asignado</td>
	</tr>
	<tr>
	<td class="AU">&nbsp;</td>
	<td colspan="9" style="text-align:left">Horario ocupado</td>
	</tr>
	<tr>
	<td class="PR">&nbsp;</td>
	<td colspan="9" style="text-align:left" >Horario docente asignado</td>
	</tr>
	</table>
</td>
</tr>
</table>
    <p>
        <input id="HddVacantes" type="hidden" value="<%=Vacantes %>" />
        <input id="hddHabilitar" type="hidden"  value="<%=Habilitar %>"   />
    </p>
</body>
</html>
<%
    if(Err.number <> 0) then
        response.Write "Error: " & Err.Description 
    end if
%>