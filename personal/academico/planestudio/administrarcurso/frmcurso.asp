<!--#include file="../../../../funciones.asp"-->
<%
accion=request.querystring("accion")
codigo_cur=request.querystring("codigo_cur")

if codigo_cur<>"" then
Set obj=Server.CreateObject("PryUSAT.clsAccesoDatos")
	obj.AbrirConexion
		Set rsDpto=obj.Consultar("ConsultarDepartamentoAcademico","FO","TO",0)
		if accion="modificarcurso" then
			fondo="bgcolor='#EFEFEF'"
			Set rs=obj.Consultar("ConsultarCurso","FO","CO",codigo_cur,0)
			if Not(rs.BOF and rs.EOF) then
				identificador_cur=rs("identificador_cur")
				nombre_cur=rs("nombre_cur")
				codigo_dac=rs("codigo_dac")
				complementario_cur=iif(rs("complementario_cur")=true,1,0)
			end if
			Set rsCurso=nothing
		end if
	Obj.CerrarConexion
	
Set Obj=nothing

%>

<HTML>
<HEAD>
<title>curso</title>
<link rel="stylesheet" type="text/css" href="../../../../private/estilo.css">
<script language="JavaScript" >
function validarfrmcurso()
{
	if (frmcurso.txtidentificador_cur.value == "")
	{
	alert("Por favor ingrese el Código del curso");
	frmcurso.txtidentificador_cur.focus();
	return (false);
	}

	if (frmcurso.txtnombre_cur.value == "")
	{
	alert("Por favor ingrese la descripción del curso");
	frmcurso.txtnombre_cur.focus();
	return (false);
	}
	
	if (confirm("¿Está completamente seguro que desea Guardar los cambios?\nRecuerde que los cambios afectarán a todos los planes vinculados al curso Actual")==true){
		return(true)
	}
	else{
		return(false)
	}
}
</script>
</HEAD>
<BODY <%=fondo%>>
<form name="frmcurso" method="post" onsubmit="return validarfrmcurso()" action="../procesar.asp?accion=<%=accion%>&codigo_cur=<%=codigo_cur%>" onLoad="document.all.txtidentificador_cur.focus()">
  <table width="100%" border="0" style="border-collapse: collapse" bordercolor="#111111" cellpadding="3" cellspacing="0">
    <tr> 
      <td width="20%" height="32" class="usatetiqueta">Identificador Curso</td>
      <td width="80%" colspan="9">
      <input name="txtidentificador_cur" type="text" class="cajas" size="20" maxlength="15" value="<%=identificador_cur%>"> 
      </td>
    </tr>
    <tr> 
      <td width="20%" height="24"> Nombre Curso</td>
      <td colspan="9" width="80%">
      <input name="txtnombre_cur" type="text" class="cajas" size="70" maxlength="500" value="<%=nombre_cur%>"></td>
    </tr>
    <tr> 
      <td width="20%" height="27">Complementario</td>
      <td colspan="9" width="80%"><input type="checkbox" name="chkcomplementario_Cur" value="1" <%=SeleccionarItem("chk",complementario_cur,"1")%>> 
      </td>
    </tr>
    <tr> 
      <td width="20%" height="28">Departamento</td>
      <td colspan="9" width="80%">
      <%call llenarlista("cbocodigo_dac","",rsDpto,"codigo_dac","nombre_dac",codigo_dac,"Seleccionar el Dpto Académico","S","")%>
      </td></tr>
    <tr> 
      <td width="20%" height="30">&nbsp;</td>
      <td colspan="9" width="80%"> <input type="submit" value="Guardar" name="smtGuardar" class="usatGuardar"> 
        <input type="button" value="Cancelar" name="cmdCancelar" onClick="window.close()" class="usatSalir">
      </td>
    </tr>
  </table>
</form>
</BODY>
</HTML>
<%end if%>