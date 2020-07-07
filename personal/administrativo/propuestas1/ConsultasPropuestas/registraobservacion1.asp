
<html>
<head>

<title>Registra Observación</title>
<link href="../../../../private/funciones.js" rel="stylesheet" type="text/css">
<script type="text/JavaScript">
<!--
function MM_jumpMenu(targ,selObj,restore){ //v3.0
  eval(targ+".location='"+selObj.options[selObj.selectedIndex].value+"'");
  if (restore) selObj.selectedIndex=0;
}
//-->
</script>
<link href="../../../../private/estilo.css" rel="stylesheet" type="text/css">
</head>

<body>
<center>
  <p><span class="usatTituloAplicacion">Registro de Observaci&oacute;n </span> - <span class="usatMisTitulos">Paso 1</span></p>
  <p>&nbsp;  </p>
</center>
<table width="70%" border="0" align="center" cellpadding="0" cellspacing="0" class="usatsugerencia">
  <tr>
    <td>&nbsp;&nbsp;&nbsp;&nbsp; Registre  los datos generales de su observaci&oacute;n, Asunto, Descripci&oacute;n, Nivel de Restricci&oacute;n y Archivos de ser necesario, luego de clic en grabar y continuar. </td>
  </tr>
</table>
<br>
<table width="80%" border="0" align="center" cellpadding="0" cellspacing="0" class="contornotabla">
  <tr>
  	<%
	accion="observacion_paso1"
//	response.write(session("codigo_Usu"))
//	response.write(//request.querystring("codigo_prop"))
	//CONSULTA EL ID DEL DIRECTOR DE ÁREA QUE RESPONDERÁ A UN COMENTARIO
		 	Set objInv=Server.CreateObject("PryUSAT.clsAccesoDatos")
	     	objInv.AbrirConexiontrans
			set rsInvolucrado=objInv.Consultar("ConsultarInvolucradoPropuesta","FO","JE",session("codigo_Usu"),request.querystring("propuesta"))
	    	objInv.CerrarConexiontrans
			set objInv=nothing
	Involucrado=rsInvolucrado(0)
	//response.write(involucrado)
	set rsInvolucrado = nothing
	%>
    <td align="center"><form id="frmregistro_Observacion_1" name="frmregistro_Observacion_1" method="post" onSubmit="return validarfrmpropuesta()" action="procesar.asp?accion=<%=accion%>">

     
      <table width="90%" border="0" cellpadding="0" cellspacing="0">
        <tr>
          <td width="13%" valign="top">&nbsp;</td>
          <td width="1%" valign="top">&nbsp;</td>
          <td colspan="3">&nbsp;</td>
        </tr>
        <tr>
          <td valign="top">&nbsp;</td>
          <td valign="top">&nbsp;</td>
          <td colspan="3"><p>
            <input name="involucrado_prp" type="hidden" id="involucrado_prp" value="<%=Involucrado%>">
			<input name="propuesta_cod" type="hidden" value="<%=request.querystring("comentario")%>" >
</p>
            </td>
        </tr>
        <tr>
          <td valign="top">Asunto</td>
          <td valign="top">&nbsp;</td>
          <td colspan="3"><input name="txtasunto_prp" type="text" id="txtasunto_prp" tabindex="1" size="79" maxlength="200" /> 
          (Max. 200 caracteres) </td>
        </tr>
        <tr>
          <td valign="top">Descripci&oacute;n</td>
          <td valign="top">&nbsp;</td>
          <td colspan="3"><textarea name="txtdescripcion_prp" cols="60" rows="8" wrap="physical" id="txtdescripcion_prp" tabindex="1"></textarea>
            (Max. 2000 caracteres) </td>
        </tr>
        <tr>
          <td valign="top">Nivel de Restricci&oacute;n </td>
          <td valign="top">&nbsp;</td>
          <td colspan="3"><select name="nivel_restriccion" id="nivel_restriccion">
            <option value="P">P&uacute;blico</option>
            <option value="R">S&oacute;lo Revisores</option>
            <option value="C">S&oacute;lo Consejo</option>
                    </select></td>
        </tr>
        <tr>
          <td valign="top">&nbsp;</td>
          <td valign="top">&nbsp;</td>
          <td colspan="3">&nbsp;</td>
        </tr>
        <tr>
          <td valign="top">&nbsp;</td>
          <td valign="top">&nbsp;</td>
          <td colspan="3">&nbsp;</td>
        </tr>
        <tr>
          <td valign="top">&nbsp;</td>
          <td valign="top">&nbsp;</td>
          <td width="24%"><input style="width:137" name="Submit" type="submit" class="guardar" value="      Guardar y Continuar" /></td>
          <td width="5%">&nbsp;</td>
          <td width="57%"><input style="width:137" name="Submit2" type="reset" class="usatSalir" value="      Reestablecer" /></td>
        </tr>
      </table>
    </form>    </td>
  </tr>
</table>
</body>

</html>
