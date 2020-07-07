<!--#include file="../../../../funciones.asp"-->
<%
accion=request.querystring("accion")
modo=request.querystring("modo")
codigo_cac=request.querystring("codigo_cac")
codigo_dac=request.querystring("codigo_dac")
if session("codigo_tfu")=1 then todos="S"

if codigo_cac="" then codigo_cac=session("codigo_cac")
if codigo_per="" then codigo_per="-2"

tipoconsulta="DP"
if session("codigo_tfu")=9 then tipoconsulta="ES"

	Set Obj= Server.CreateObject("PryUSAT.clsAccesoDatos")
		Obj.AbrirConexion
		
		Set rsCac= Obj.Consultar("ConsultarCicloAcademico","FO","TO",0)
		
		if int(session("codigo_tfu"))=1 or int(session("codigo_tfu"))=7 or int(session("codigo_tfu"))=16 then
			Set rsDpto=obj.Consultar("ConsultarDepartamentoAcademico","FO","TO",0)
		else
			Set rsDpto=obj.Consultar("ConsultarCentroCosto","FO",tipoconsulta,session("codigo_usu"))
		end if
		
		if (modo="R" and codigo_dac<>"-2") then
			Set rsProfesor=Obj.Consultar("ConsultarDocente","FO","2",codigo_cac,codigo_dac)
			if Not(rsProfesor.BOF and rsProfesor.EOF) then
				estado="R"
				alto="99%"
			end if			
		end if
		Obj.CerrarConexion
	Set obj=nothing
	
%>
<html>
<head>
<meta http-equiv="Content-Language" content="es">
<meta name="GENERATOR" content="Microsoft FrontPage 5.0">
<meta name="ProgId" content="FrontPage.Editor.Document">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<title>Administrar matrícula del estudiante</title>
<link rel="stylesheet" type="text/css" href="../../../../private/estilo.css">
<script language="JavaScript" src="../../../../private/funciones.js"></script>
<script language="JavaScript" src="../private/validarcargaacademica.js"></script>
<script language="Javascript">
	function ConsultarCargaProfesor()
	{
		document.all.cmdGuardar.style.display="none"
		fradetalle.location.href="frmcursosprofesor.asp?codigo_per=" + document.all.cboprofesor.value + "&codigo_cac=<%=codigo_cac%>&codigo_dac=<%=codigo_dac%>"
		
		if (document.all.cboprofesor.value!="-2"){
			document.all.cmdGuardar.style.display=""
		}
	}
	
</script>
</head>
<body>
<table border="0" cellpadding="2" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%" height="<%=alto%>">
    <tr>
      <td width="100%" class="usattitulo" height="5%" colspan="4" valign="top">
      Actualizar&nbsp; Carga Académica por Profesor</td>
    </tr>
    <tr>
        <td width="18%" height="3%" valign="top">Ciclo Académico</td>
        <td width="10%" height="3%" valign="top"><%call llenarlista("cboCiclo","actualizarlista('frmactualizarhorascarga.asp?codigo_cac=' + this.value)",rsCac,"codigo_cac","descripcion_cac",codigo_cac,"","","")%></td>
        <td width="20%" height="3%" valign="top" align="right">Departamento Acad.</td>
        <td width="40%" height="3%" valign="top"><%call llenarlista("cboDpto","actualizarlista('frmactualizarhorascarga.asp?modo=R&codigo_cac=' + cboCiclo.value + '&codigo_dac=' + this.value)",rsDpto,"codigo_dac","nombre_dac",codigo_dac,"Seleccionar el Dpto Académico",todos,"")%></td>
        </tr>
    <%if estado="R" then%>
    <tr>
        <td width="18%" height="3%" valign="top">Profesor</td>
        <td width="70%" height="3%" valign="top" colspan="3">
        <%call llenarlista("cboprofesor","ConsultarCargaProfesor()",rsProfesor,"codigo_per","docente",codigo_per,"Seleccionar el Profesor","","")%>
		</td>
        </tr>
	 	<tr >
		    	<td width="100%" height="3%" class="usatCeldaTabActivo" colspan="4" >
					<table border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%">
                      <tr>
                        <td width="42%"><b><font size="2" color="#FFFF00">Carga Académica del Profesor</font></b></td>
                        <td width="58%" align="right">
						<input style="display:none" type="button" class="guardar2"  value="      Guardar hrs."  onClick="ActualizarHorasCargaAcademica('<%=codigo_cac%>',cboprofesor.value)" name="cmdGuardar" id="cmdGuardar">
						</td>
                      </tr>
                    </table>
				</td>
  		</tr>			  
	 	<tr>
		    	<td width="100%" height="80%" valign="top" colspan="4" class="contornotabla">
					<iframe id="fradetalle" src="frmcursosprofesor.asp" height="100%" width="100%" border="0" frameborder="0">
					</iframe>
				</td>
  		</tr>
	<%elseif modo="R" then%>
		<tr><td width="100%" height="5%" colspan="4" class="usatsugerencia">&nbsp;&nbsp;&nbsp; No se han registrado Profesores para el Departamento Seleccionado</td></tr>
	<%end if%>
</table>
</body>
</html>