<!--#include file="../../../../funciones.asp"-->
<%
condicion_alu=request.querystring("condicion_alu")
codigo_cpf=request.querystring("codigo_cpf")
foto_alu=request.querystring("foto_alu")
cicloIng_alu=request.querystring("cicloIng_alu")

codigo_tfu=session("codigo_tfu")
codigo_usu=session("codigo_usu")
codigo_test=request.querystring("mod")

if codigo_cac="" then codigo_cac="-2"
if codigo_cpf="" then codigo_cpf="-2"

Set obj=Server.CreateObject("PryUSAT.clsAccesoDatos")
	obj.AbrirConexion

	Set rsCicloIngreso=Obj.Consultar("ConsultarCicloAcademico","FO","CI2",0)	
	Set rsEscuela= obj.Consultar("EVE_ConsultarCarreraProfesional","FO",codigo_test,codigo_tfu,codigo_usu)
	
	if codigo_cpf<>"-2" then
		Set rsAlumno=Obj.Consultar("ConsultarFotosAlumno","FO",1,codigo_cpf,condicion_alu,foto_alu,cicloIng_alu)

		if Not(rsAlumno.BOF and rsAlumno.EOF) then
			activo=true
			alto="height=""100%"""
		end if
	end if
    obj.CerrarConexion
Set obj=nothing
%>
<html>
<head>
<title>Consulta de fotos por Escuela</title>
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252" >
<link href="../../../../private/estilo.css" rel="stylesheet" type="text/css">
<script type="text/javascript" language="JavaScript" src="../../../../private/funciones.js"></script>
<script type="text/javascript" language="JavaScript">
	function SeleccionarFoto(celda)
	{
		if (celda.className=="Selected"){
			celda.className="SelOff"
		}
		else{
			celda.className="Selected"
		}
	}	
	
	function AbrirFotos()
	{
		if (document.all.listadiv!=undefined){
			listadiv.innerHTML="<h5>&nbsp;</h5><h5 align='center'>Buscando fotos de estudiantes...</h5>"
		}
		location.href="vstfotosescuela.asp?mod=<%=codigo_test%>&codigo_cpf=" + cbocodigo_cpf.value + "&condicion_alu=" + cbocondicion_alu.value + "&foto_alu=" + cbofoto_alu.value + "&cicloIng_alu=" + cbocicloIng_alu.value
	}
</script>
</head>
<body bgcolor="#F0F0F0">
<table border="0" cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%" <%=alto%>>
<tr>
	<td height="3%" colspan="6" class="usattitulo">Consulta de Fotos por Escuela Profesional</td>
</tr>
<tr>
	<td height="3%" style="width: 10%" align="right">Escuela:</td>
	<td height="3%" style="width: 40%">
	<%call llenarlista("cbocodigo_cpf","",rsEscuela,"codigo_cpf","nombre_cpf",codigo_cpf,"Seleccione la Escuela Profesional","","")%>
	</td>
	<td height="3%" style="width: 25%" align="right">
	<select name="cbocondicion_alu">
		<option value="I" <%=SeleccionarItem("cbo",condicion_alu,"I")%>>Ingresante</option>
		<option value="P" <%=SeleccionarItem("cbo",condicion_alu,"P")%>>Postulante</option>		
	</select>
	<select name="cbofoto_alu">
		<option value="0" <%=SeleccionarItem("cbo",foto_alu,0)%>>Sin Foto</option>
		<option value="1" <%=SeleccionarItem("cbo",foto_alu,1)%>>Con Foto</option>
	</select>	
	</td>
	<td height="3%" style="width: 5%" align="right">Ingreso</td>
	<td height="3%" style="width: 12%" align="right">
	<%call llenarlista("cbocicloIng_alu","",rsCicloIngreso,"cicloIng_Alu","cicloIng_Alu",cicloIng_alu,"","","")%>
	</td>
	<td height="3%" style="width: 5%" align="right">
	<img alt="Buscar fotos" class="imagen" src="../../../../images/buscar.gif" width="58" height="17" onclick="AbrirFotos()"></td>
</tr>
  <%if activo=true then%>
  <tr valign="top">
    <td height="92%" width="100%" colspan="6">
        <div id="listadiv" style="height:100%" class="contornotabla">
		<table width="100%" border="1" bordercolor="gray" style="border-collapse: collapse">
		<%	i=0
			Hayfoto=0
			Nofoto=0
			Set obEnc=server.createobject("EncriptaCodigos.clsEncripta")
			Do while not rsAlumno.eof
				i=i+1
				
				if rsAlumno("foto_alu")=1 then
					foto=obEnc.CodificaWeb("069" & rsAlumno("codigouniver_alu"))
					'---------------------------------------------------------------------------------------------------------------
                    'Fecha: 29.10.2012
                    'Usuario: dguevara
                    'Modificacion: Se modifico el http://www.usat.edu.pe por http://intranet.usat.edu.pe
                    '---------------------------------------------------------------------------------------------------------------
					foto="//intranet.usat.edu.pe/imgestudiantes/" & foto
				else
					foto="../../../../images/fotovacia.gif"
				end if
				
				if (i mod 5= 0) then
					response.write "</tr><tr>"
				elseif i=1 then
					response.write "<tr>"
				end if
			%>			
			<td align="center" valign="top" class="Sel" Typ="Sel">
	    	<img src="<%=foto%>" alt="Código:<%=rsAlumno("codigouniver_alu") & chr(13) & "Estudiante:" & rsAlumno("alumno")%>" width="160" height="190">
		    <br>
			<span><%=rsAlumno("alumno")%></span>
    		</td>
			<%
				rsAlumno.movenext
			Loop
			set rsAlumno=nothing
			set obEnc=Nothing
		%>
		</table>
		</div>
	</td>
    </tr>
  <tr>
   	<td height="5%" width="100%" colspan="6" align="right" class="azul">TOTAL: <%=i%> estudiantes</td>
   </tr>
<%end if%>   
</table>
<%if activo<>true and codigo_cpf<>"-2" then%>
	<h5 class="usatsugerencia">&nbsp;&nbsp;&nbsp;&nbsp; No se han encontrado estudiantes registrados en la Escuela Profesional seleccionada</h5>
<%end if%>
</body>
</html>