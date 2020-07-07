<!--#include file="../../../../funciones.asp"-->
<%
if(session("codigo_usu") = "") then
    Response.Redirect("../../../../sinacceso.html")
end if

accion=request.querystring("accion")
codigo_cac=request.querystring("codigo_cac")
codigo_cpf=request.querystring("codigo_cpf")
codigo_usu=session("codigo_usu")
codigo_tfu=session("codigo_tfu")
if codigo_cac="" then codigo_cac=session("codigo_cac")

Set obj=Server.CreateObject("PryUSAT.clsAccesoDatos")
obj.AbrirConexion
	Set rsCiclo=Obj.Consultar("ConsultarCicloAcademico","FO","TO",0)
	if codigo_tfu=1 OR codigo_tfu=7 OR codigo_tfu=16 then
	    Set rsEscuela=obj.Consultar("ConsultarCarreraProfesional","FO","MA",0)
	else
		Set rsEscuela=obj.Consultar("consultaracceso","FO","ESC",0,codigo_usu)
	end if
	
	if codigo_cpf<>"" and codigo_cpf<>"-2" and Not(rsEscuela.BOF and rsEscuela.EOF) then
		Set rsCursos=Obj.Consultar("ConsultarCursoProgramado","FO","19",codigo_cac,codigo_cpf,"%",0)
		
		if Not(rsCursos.BOf and rsCursos.EOF) then
			HayReg=true
			alto="height=""92%"""
		end if
	end if	
Obj.CerrarConexion
Set objEscuela=nothing
%>
<html>

<head>
<meta http-equiv="Content-Language" content="es">
<meta name="GENERATOR" content="Microsoft FrontPage 12.0">
<meta name="ProgId" content="FrontPage.Editor.Document">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<title>Agrupar Cursos Programados</title>
<link rel="stylesheet" type="text/css" href="../../../../private/estilo.css">
<script type="text/javascript" language="JavaScript" src="../../../../private/funciones.js"></script>
<script type="text/javascript" language="JavaScript">
var Contador=0

	function MarcarCurso(Control)
	{
		var tbl=fradetalle.document.all.tblelegidos
		if (tbl==undefined){
			fradetalle.location.href="tblcursoselegidos.asp"
			tbl=fradetalle.document.all.tblelegidos
		}

		if (tbl!=undefined){
			//Verificar que sean al menos del mismo grupo horario
			
			if (Control.checked==true){
				AgregarCurso(tbl,Control)
				Contador=Contador+1
			}
			else{
				QuitarCurso(tbl,Control)
				Contador=Contador-1			
			}
			
			//Habilitar botón Guardar
			if (Contador>1){
				cmdGuardar.disabled=false
			}
			else{
				cmdGuardar.disabled=true
			}
			
			pintarfilamarcada(Control)
		}
		else{
			Control.checked=false
		}
	}

	function AgregarCurso(tbl,Control)
	{
		var fila=document.getElementById("fila" + Control.id.substring(3,50))
		var NuevaFila=tbl.insertRow(tbl.rows.length)
		NuevaFila.id=fila.id
		fila.className="SelOff"
		//Declara variables para fila marcada
		
		var Celdas=fila.cells
		var j=0
		
		for (var i=0;i<7; i++){
			if (j==0){
				if (j==0){var marcado='checked="checked"'}
				var opt='<input name="optcodigo_cup" type="radio" value="' + Control.value + '" ' + marcado + ' />'
				
	  			NuevaFila.insertCell(i).appendChild(tbl.document.createElement(opt));
			}
			else{
				var TextoCelda=Celdas[j].innerText
				NuevaFila.insertCell(i).appendChild(tbl.document.createTextNode(TextoCelda))
		    }
	        j=j+1
		}
	}

	function QuitarCurso(tbl,Control)
	{
	 //verifica que la fila exista para eliminarla
	  var ArrFilas=tbl.getElementsByTagName('tr')
		
		for (var i = 1; i < ArrFilas.length; i++){
			if (ArrFilas[i].id=="fila" + Control.id.substring(3,50)){
			   tbl.deleteRow(i)
			   return //Salir de la función si encuentra fila
			}
	    }
	}
	
	function MostrarCursosHijo(codigo_cup,fila)
	{
		var curso="&nbsp;Código: " + fila.cells[2].innerText + "<br>"
		curso+="&nbsp;Descripción: " + fila.cells[3].innerText + "<br>"
		curso+="&nbsp;Grupo Horario: " + fila.cells[6].innerText
		 
		//AbrirPopUp("tblcursoshijo.asp?codigo_cac=" + cbocodigo_cac.value + "&codigo_cup=" + codigo_cup + "&curso=" + curso,"400","650","no","yes","yes")
		location.href="tblcursoshijo.asp?codigo_cpf=" + cbocodigo_cpf.value + "&codigo_cac=" + cbocodigo_cac.value + "&codigo_cup=" + codigo_cup + "&curso=" + curso
	}
	
	function AgruparCursos()
	{
		var opt=fradetalle.document.all.optcodigo_cup
		var Padre="0"
		var Hijos=""
		
		if (confirm("¿Está completamente seguro que desea Agrupar los cursos seleccionados?")==true){		
			for (var i=0;i<opt.length;i++){
					if (opt[i].checked==true){
						Padre=opt[i].value
					}
					else{
						Hijos+=opt[i].value + ','
					}
			}
		
			//alert('Padre:' + Padre + '\nHijos:' + Hijos)
			location.href="procesar.asp?accion=agruparcursos&codigo_cac=" + cbocodigo_cac.value + "&codigo_cpf=" + cbocodigo_cpf.value + "&Padre=" + Padre + "&Hijos=" + Hijos
		}	
	}
</script>
</head>
<body>
<p class="usatTitulo">Agrupar Cursos Programados</p>
<table border="0" cellpadding="2" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%" <%=alto%>>
	<tr height="5%" valign="top">
		<td width="18%">Semestres Académico</td>
		<td width="10%"><%call llenarlista("cbocodigo_cac","actualizarlista('frmagruparcursos.asp?codigo_cac=' + this.value)",rsCiclo,"codigo_cac","descripcion_cac",codigo_cac,"","","")%></td>
		<td width="20%" align="right">Carrera Profesional</td>
		<td width="40%"><%call llenarlista("cbocodigo_cpf","actualizarlista('frmagruparcursos.asp?codigo_cac=' + cbocodigo_cac.value + '&codigo_cpf=' + this.value)",rsEscuela,"codigo_cpf","nombre_cpf",codigo_cpf,"Seleccione la Carrera Profesional","S","")%></td>
	</tr>
	<%if HayReg=true then%>
	<tr height="50%" valign="top">
		<td width="100%" colspan="4">
		<table border="1" cellpadding="2" cellspacing="0" style="border-collapse: collapse" bordercolor="#808080" width="100%" height="100%">
			<tr class="etabla">
				<td width="8%" height="3%">Acción</td>			
				<td width="5%" height="3%">Semestre Académico</td>
				<td width="10%" height="3%">Código</td>
				<td width="30%" height="3%">Descripción</td>
				<td width="5%" height="3%">Crd.</td>
				<td width="5%" height="3%">TH</td>
				<td width="5%" height="3%">GH</td>
				<td width="20%" height="3%">Docente</td>
				<td width="5%" height="3%">Primer Ciclo</td>
			</tr>
			<tr>
				<td width="100%" colspan="13">
				<div id="listadiv" style="height:100%" class="NoImprimir">
					<table width="100%" border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse" bordercolor="#ccccccc" id="tblcursosprogramados">
						<%	i=0
							codigo=0
							Do while not rsCursos.EOF
								i=i+1
								bordeciclo=Agrupar(rsCursos("identificador_cur"),codigo)
						%>
						<tr id="fila<%=i%>" onmouseover="Resaltar(1,this,'S')" onmouseout="Resaltar(0,this,'S')" class="Sel" Typ="Sel">
							<td <%=bordeciclo%> align="center" width="8%">
							<%if codigo_cac<>session("codigo_cac") then%>
							<input name="chkcodigo_cup" id="chk<%=i%>" type="checkbox" value="<%=rsCursos("codigo_cup")%>" onclick="MarcarCurso(this)" />
							<img src="../../../../images/propiedades.gif" onclick="MostrarCursosHijo('<%=rsCursos("codigo_cup")%>',fila<%=i%>)">
							<%end if%>
							</td>
							<td <%=bordeciclo%> align="center" width="5%"><%=ConvRomano(rsCursos("ciclo_Cur"))%></td>
							<td <%=bordeciclo%> width="10%"><%=rsCursos("identificador_Cur")%></td>
							<td <%=bordeciclo%> width="35%"><%=replace(rsCursos("nombre_Cur"),"<br>","")%></td>
							<td <%=bordeciclo%> align="center" width="5%"><%=rsCursos("creditos_cur")%></td>
							<td <%=bordeciclo%> align="center" width="5%"><%=rsCursos("totalhoras_Cur")%></td>
							<td <%=bordeciclo%> width="5%"><%=rsCursos("grupohor_Cup")%></td>
							<td <%=bordeciclo%> width="20%"><%=rsCursos("profesor")%></td>
							<td <%=bordeciclo%> width="5%"><% if rsCursos("soloPrimerCiclo_cup") then response.Write("Si") else response.Write("No") end if %></td>
						</tr>
						<%rsCursos.movenext
							loop
							set rsCursos=nothing
						%>
					</table>
				</div>
				</td>
			</tr>
		</table>
		</td>
	</tr>
	<tr height="5%" valign="top">
		<td width="100%" colspan="4" class="pestanaresaltada">
		<table width="100%">
			<tr>
			<td width="80%" class="azul" id="mensaje">Lista de cursos elegidos 
			para Agrupar</td>
			<td width="20%" align="right">
			<input name="cmdGuardar" type="button" value="Agrupar" class="guardar"
			onclick="AgruparCursos()" disabled="true">
			</td>
			
			</tr>
		</table>
		</td>
	</tr>
	
	<tr height="45%" valign="top">
		<td width="100%" colspan="4" class="contornotabla">
         <iframe id="fradetalle" height="100%" width="100%" class="contornotabla" frameborder="0" style="border:0px" src="tblcursoselegidos.asp">
         El explorador no admite los marcos flotantes o no está configurado actualmente para mostrarlos.
         </iframe>
		</td>
	</tr>
	<%end if%>	
</table>

</body>

</html>
