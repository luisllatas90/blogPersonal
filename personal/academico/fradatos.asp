<%
Set obEnc=server.createobject("EncriptaCodigos.clsEncripta")
	codigofoto=obEnc.CodificaWeb("069" & session("codigoUniver_alu"))
set obEnc=Nothing
%>
<script language="Javascript">
	function enviarConsulta()
	{
		var cu=document.all.txtcodigouniver_alu
		<%
		ruta=session("rutaactual")
		if ruta="" then ruta="../../"
		%>
		if (cu.value==""){
			alert("Debe ingresar el c�digo universitario del Estudiante")
			cu.focus()
			return(false)
		}
		else{
			location.href="<%=ruta%>clsbuscaralumno.asp?codigouniver_alu=" + cu.value + "&pagina=<%=session("urlpagina")%>"
		}
	}
</script>
<table border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%" id="tbldatosestudiante">
  <tr>
    <td width="15%" valign="top">
    <!--
    '---------------------------------------------------------------------------------------------------------------
        'Fecha: 29.10.2012
        'Usuario: dguevara
        'Modificacion: Se modifico el http://www.usat.edu.pe por http://intranet.usat.edu.pe
    '---------------------------------------------------------------------------------------------------------------
    -->
        <img border="0" src="//intranet.usat.edu.pe/imgestudiantes/<%=codigofoto%>" width="95" height="106" alt="Sin Foto"></td>
    <td width="85%" valign="top">
        <table border="0" cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%" class="contornotabla">
          <tr>
    <td width="15%">C�digo Universitario&nbsp;</td>
    <td class="usatsubtitulousuario" width="75%" colspan="3">: 
    <input class="cajas" size="27" name="txtcodigouniver_alu" value="<%=session("codigoUniver_alu")%>" onkeyup="if(event.keyCode==13){enviarConsulta()}"><%if session("qb")="" then%><input type="button" class="buscar2" class="NoImprimir" onclick="enviarConsulta()" value="  Buscar...">
		<%end if%>
    </td>
          </tr>
          <tr>
    <td width="15%">Apellidos y Nombres</td>
    <td class="usatsubtitulousuario" width="75%" colspan="3">: <%=session("alumno")%></td>
          </tr>
          <tr>
    <td width="15%">Escuela Profesional&nbsp;</td>
    <td class="usatsubtitulousuario" width="50%" colspan="3">: <%=session("nombre_cpf")%>&nbsp;</td>
          </tr>
          <tr>
	    	<td width="15%">Ciclo de Ingreso</td>
	    	<td class="usatsubtitulousuario" width="15%">: <%=session("cicloIng_alu")%>&nbsp;</td>
	    	<td width="10%" align="right">Modalidad</td>
	    	<td class="usatsubtitulousuario" width="30%">: <%=session("nombre_min")%>&nbsp;</td>
          </tr>
          <tr>
	    	<td width="15%">Plan de Estudio</td>
	    	<td class="usatsubtitulousuario" width="50%" colspan="3">: <%=session("descripcion_pes")%>&nbsp;</td>
          </tr>
        </table>
    </td>
  </tr>
  </table>