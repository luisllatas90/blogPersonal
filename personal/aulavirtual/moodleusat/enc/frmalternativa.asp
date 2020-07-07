<%
accion=request.querystring("accion")
idalternativa=request.querystring("idalternativa")
codigo_ccv=request.querystring("codigo_ccv")
orden=request.querystring("orden")
correcto=0

if accion="modificaralternativa" then
	Set Obj= Server.CreateObject("AulaVirtual.clsAccesoDatos")
	obj.AbrirConexion
			rsAlternativa=obj.Consultar("ConsultarEvaluacion","FO",6,idalternativa)
	obj.CerrarConexion
	Set Obj=nothing
	
	if Not(rsAlternativa.BOF and rsAlternativa.EOF) then
		orden=rsAlternativa("orden")
		tituloalternativa=rsAlternativa("tituloalternativa")
		correcto=rsAlternativa("rptaCorrecta")
		mensaje=rsAlternativa("mensaje")	
	end if
end if
%>
<html>
<head>
<meta http-equiv="Content-Language" content="es">
<meta name="GENERATOR" content="Microsoft FrontPage 5.0">
<meta name="ProgId" content="FrontPage.Editor.Document">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<title>Registro de alternativas</title>
<link rel="stylesheet" type="text/css" href="../../../../private/estiloaulavirtual.css">
<script type="text/javascript" language="Javascript">
	function ValidarAlternativa()
	{
		if (txtorden.value==""){
			alert("Especifique el orden de la alternativa")
			txtorden.focus()
			return(false)
		}
		if (txttitulo.value==""){
			alert("Especifique el título de la alternativa")
			txttitulo.focus()
			return(false)
		}
				
		var correcto=0
		if (document.all.correctoalt.checked==true){correcto=1}
		var Argumentos = window.dialogArguments;
		
	   	Argumentos.ordenalt=document.all.txtorden.value
	   	Argumentos.tituloalt=document.all.txttitulo.value
	   	Argumentos.mensajealt=document.all.txtmensaje.value
		Argumentos.correctoalt=correcto
		Argumentos.codigo_ccv='<%=codigo_ccv%>'
	   	Argumentos.EnviarPreguntaAlternativa();
		window.close();
	}
</script>
</head>
<body bgcolor="#EEEEEE" onload=txttitulo.focus()>
    <table width="100%" border="0" cellpadding="4" cellspacing="0" style="border-collapse: collapse" bordercolor="#808080">
  	<tr>
      <td width="100%" class="etiqueta">Orden / Descripción de alternativa</td>
  	</tr>
  	<tr> 
      <td width="100%">
      <input type="text" name="txtorden" size="2" maxlength="2" value="<%=orden%>" class="cajas2">
      <input type="text" name="txttitulo" size="20" class="cajas2" value="<%=tituloalternativa%>" style="width: 90%" onkeyup="if(event.keyCode==13){ValidarAlternativa()}"></td>
  	</tr>
  	<tr> 
      <td width="100%"><b>¿Desea mostrar un mensaje si la&nbsp; respuesta incorrecta?</b></td>
  	</tr>
  	<tr>
      <td width="100%">
      <input type="text" name="txtmensaje" size="20" class="cajas" value="<%=mensaje%>"></td>
      </tr>
    <tr>
      <td width="100%" align="right"><b>¿Esta alternativa es la correcta?&nbsp;&nbsp;</b><input type="checkbox" name="correctoalt" <%if correcto=1 then response.write "checked" end if%> value="ON"></td>
      </tr>
  	</table>
  	<p align="center">
    <input type="button" value="   Guardar" name="cmdGuardar" class="guardar3" onclick="ValidarAlternativa()">
    <input type="button" value="Cerrar" name="cmdCerrar" class="cerrar3" onclick="window.close()">
    </p>

</body>

</html>