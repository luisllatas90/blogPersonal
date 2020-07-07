<!--#include file="../../../funciones.asp"-->
<%
dim rsHorario

modo=request.querystring("modo")
codigo_cpf=request.querystring("codigo_cpf")
codigo_cup=request.querystring("codigo_cup")
codigo_amb=request.querystring("codigo_amb")
codigo_cac=request.querystring("codigo_cac")
codigo_per=request.querystring("codigo_per")
th=request.querystring("th")
codigo_tfu=session("codigo_tfu")
mat=request.querystring("mat")

if (codigo_amb="" or codigo_amb="-2") then codigo_amb=0
if codigo_cup="" then codigo_cup=0
if codigo_per="" then codigo_per=1
if modo="" then modo="C"

if (codigo_cup<>0 or codigo_amb<>0) then

Set Obj= Server.CreateObject("PryUSAT.clsAccesoDatos")
	obj.AbrirConexion
	
		if modo="A" then
			Set rsProfesor=obj.Consultar("ConsultarCargaAcademica","FO","EXC",codigo_cup,0)
			if Not(rsProfesor.BOF and rsProfesor.EOF) then
				HayProfesor=true
				if codigo_per="1" then codigo_per=rsProfesor("codigo_per")
			else
				codigo_per=1
			end if

			'******************************************************
			'Mostrar ambientes asignados de acuerdo a fechas
			'******************************************************			
			Set rsFechaAsignadas=obj.Consultar("ConsultarHorariosAmbiente","FO",0,codigo_amb,codigo_cac,codigo_cpf,0)
			if Not(rsFechaAsignadas.BOF and rsFechaAsignadas.EOF) then
				HayFechas=true
			end if
		end if
		
		docente=iif(codigo_per=1,0,codigo_per)
		fechaini=request.querystring("fechaini")
		fechafin=request.querystring("fechafin")
		
		if fechaini="" then fechaini=null
		if fechafin="" then fechafin=null

		Set rsHorario=obj.Consultar("ConsultarHorarioDisponible","FO",codigo_cac,codigo_cup,docente,codigo_amb,fechaini,fechafin)
		
		'******************************************************
		'Permite agregar/modificar horario
		'******************************************************
		if (modo<>"A" and mat=0) then
			Autorizado=true
		elseif modo<>"A" and mat>0 then
			'******************************************************
			'Permite agregar/modificar horario con AUTORIZACIÓN
			'******************************************************		
			Set rsAutorizacion=obj.Consultar("ConsultarAutorizacionCambioDatos","FO",1,codigo_cup,session("codigo_usu"),codigo_cac,docente)

			'******************************************************
			'Verificar si AUTORIZACIÓN HA SIDO APROBADA
			'******************************************************
			if Not(rsAutorizacion.BOF and rsAutorizacion.EOF) then
				Autorizado=true
			end if
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
	<%if modo="A" then%>
	cursor:hand
	<%end if%>	
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
	if ((celda.className=="AU" || celda.className=="PR" || celda.className=="CU") && ("<%=modo%>"=="A")){
		var codigo_per=document.all.cbocodigo_per
		if (codigo_per!=undefined){
			codigo_per=document.all.cbocodigo_per.value
		}
		else{
			codigo_per=1
		}
		
		AbrirPopUp('lsthorarioregistrado.asp?modo=' + celda.className + '&dia=' + celda.id + "&codigo_cup=<%=codigo_cup%>&codigo_per=" + codigo_per + "&codigo_amb=" + cbocodigo_amb.value + "&codigo_cac=<%=codigo_cac%>&codigo_cpf=<%=codigo_cpf%>",'400','700','yes','yes','yes')
	}
	else{
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
			
			if (celda.className=="Selected"){
				celda.className="SelOff"
		  		celda.innerHTML="&nbsp;"
				tdMarcas.innerHTML=eval(tdMarcas.innerText)-1
			}
			else{
				if (eval(tdMarcas.innerText)+1><%=th%>){
					Bandera=false
					if (confirm("Ha sobrepasado las horas del curso\n¿Desea seguir marcando el horario")==true){
						Bandera=true
					}
				}
			
				if (Bandera==true){
					celda.className="Selected"
					celda.innerHTML='<img src="../../../images/bien.gif">'
					tdMarcas.innerHTML=eval(tdMarcas.innerText)+1
				}
			}
			
			if (eval(tdMarcas.innerText)>0){
				cmdGuardar.disabled=false
			}
		}
		
	}
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
			codigo_amb=fechasBD[i].ambiente
		}
	}
	else{
		codigo_amb=0
	}
	
	location.href="tblhorario.asp?modo=A&codigo_cup=<%=codigo_cup%>&codigo_amb=" + codigo_amb + "&codigo_cac=<%=codigo_cac%>&th=<%=th%>&codigo_cpf=<%=codigo_cpf%>&codigo_per=" + codigo_per + "&fechaini=" + fechaini + "&fechafin=" + fechafin

}

function GuardarHorario()
{
	var Marcas=""
	var codigo_per=document.all.cbocodigo_per
	
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
		location.href="procesar.asp?Accion=registrarhorario&Marcas=" + Marcas + "&codigo_cup=<%=codigo_cup%>&codigo_cac=<%=codigo_cac%>&codigo_amb=" + cbocodigo_amb.value + "&tipo_lho=" + cbotipo_Lho.value + "&codigo_per=" + codigo_per + "&th=<%=th%>&codigo_cpf=<%=codigo_cpf%>&fechaini=" + ini + "&fechafin=" + fin
	}
	else{
		alert("Debe definir el horario de la asignatura, marcando las celdas según el día y la hora")
	}
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

<body style="background-color: #DCDCDC;" onload="AgregarItemObjeto()">
<%if modo="A" then%>
<table border="0" cellpadding="2" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" align="center" width="100%" height="5%">
  <tr>
    <td width="40%">
    <%if HayFechas=true and modo="A" then%>
    <select name="cbocodigo_amb" onchange="ConsultarHorarios()">
	<option value="-1">[Seleccione Ambiente y Fechas asignadas]</option>
	<%
	rsFechaAsignadas.movefirst
	Do while Not rsFechaAsignadas.EOF
		if isnull(fechaini)=false then
			if cdbl(codigo_amb)=cdbl(rsFechaAsignadas("codigo_amb")) AND _
				cdate(fechaini)=cdate(rsFechaAsignadas("fechaini_daa")) AND _
				cdate(fechafin)=cdate(rsFechaAsignadas("fechafin_daa")) then
				marcado="SELECTED"
			else
				marcado=""
			end if
		end if
	%>
	<option value="<%=rsFechaAsignadas("codigo_amb")%>" <%=marcado%>><%=rsFechaAsignadas("ambiente")%> (<%=rsFechaAsignadas("fechaini_daa")%> hasta <%=rsFechaAsignadas("fechafin_daa")%>)</option>
		<%rsFechaAsignadas.movenext
	Loop
	%>
	</select>
	<%elseif modo="A" then
		response.write "<tr><td colspan=2>[Solicite a Dirección Académica la asignación de ambientes]</td></tr>"
	end if
	%>
    </td>
    <td width="10%" class="etiqueta">Profesor:</td>
    <td width="40%" style="text-align:left">
    <%
    if HayProfesor=false then
    	response.write "--NO DEFINIDO--"
    else
	    call llenarlista("cbocodigo_per","ConsultarHorarios()",rsProfesor,"codigo_per","funciondocente",codigo_per,"","","")
	end if
    %>
    </td>
  </tr>
  </table>  
<%end if%>
<table width="100%" height="92%" style="border-collapse: collapse;" border="0" bordercolor="#CCCCCC" cellpadding="0">
<tr>
<td width="80%">
<%
dim dia,hora
dim diaBD,inicioBD,finBD
dim TextoCelda
dim marcas

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

for f=1 to 14
	response.write vbtab & "<tr height='7%'>" & vbcrlf
	
	for c=0 to 6
		if c=0 then		
			response.write vbtab & "<td width='30%' height='7%'  class='etiquetaTabla'>" & f+6 & ":00 - " & f+6 & ":50</td>"
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
		
					'si el día es el mismo y la horaactual es menor que horafin y mayor que la horainicio
					if trim(dia)=trim(diaBD) AND int(hora)>=int(inicioBD) AND int(hora)<int(finBD) then
						temp=replace(rsHorario("color_hor"),"""","'")
						if trim(temp)="class='CU'" then
							marcas=marcas+1
							TextoCelda=TextoCelda & " tooltip='" & rsHorario("ambiente") & "' "
						end if
						TextoCelda=TextoCelda & rsHorario("color_hor")
					end if
					rsHorario.movenext
				Loop
			end if
			TextoCelda=TextoCelda & "></td>" & vbcrlf
			
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
		<td>Tipo de Horario</td>
	</tr>
	<tr>
		<td>	
	    <select size="1" name="cbotipo_Lho" class="cajas">
		<option value="T" >Teoría</option>
		<option value="P">Práctica</option>
		<option value="A">Asesoría</option>
		<option value="L">Labotatorio</option>
		</select>
		</td>
	</tr>
	<%end if
	
'*************************************************************************************
'Bloquear llenado de horarios, solo se llena mayor o igual al ciclo actual
'*************************************************************************************
if ((int(codigo_cac)>=int(session("codigo_cac"))) AND codigo_cpf<>25) then
	'*************************************************************************************
	'Sólo Agregar/Modificar Horarios si no hay matriculados, salvo autorización
	'*************************************************************************************
	%>
	<tr>
		<td class="rojo">
		<%if Autorizado=true then%>
			<input name="cmdNuevo" type="button" value="Nuevo" class="agregar2" onclick="ConsultarHorarios()" /><br><br>
			<%if marcas>0 then%>
			<input name="cmdModificar" type="button" value="  Modificar" class="modificar2" onclick="ConsultarHorarios()" />
			<%end if%>
		<%elseif modo<>"A" and mat>0 then%>
			[Bloqueado para modificar o agregar horarios, ya que existen estudiantes matriculados].
			<br><br><a href="Javascript:AbrirPopUp('administrar/frmautorizarcambiodatos.asp?estado_acd=P&nombre_tbl=cursoprogramado&codigo_tbl=<%=codigo_cup%>&accion_acd=Cambiar horario&destino=Direccion academica','300','550')"><u>Haga click aquí para SOLICITAR AUTORIZACIÓN</u></a>
		<%end if%>
		</td>
	</tr>
	<%	
	if modo="A" then%>	
	<tr>
		<td>
		<input name="cmdGuardar" type="button" value="Guardar" class="guardar2" disabled="true" onclick="GuardarHorario()" />		
		</td>
	</tr>
	<%end if
else%>	
	<tr>
		<td class="rojo">[No se puede modificar horarios de ciclos anteriores]</td>
	</tr>
<%end if%>
	<tr>
		<td class="etiqueta">
		Horas del Curso</td>
	</tr>
	<tr>
		<td class="rojo">
		<b><%=th%></b></td>
	</tr>
	<tr>
		<td class="etiqueta">
		Horas Marcadas</td>
	</tr>
	<tr>
		<td id="tdMarcas" class="azul">
		<%=marcas%></td>
	</tr>
	</table>

</td>
</tr>
</table>
</body>
</html>
<%end if%>