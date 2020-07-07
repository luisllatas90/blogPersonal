<%
codigo_aut=request.querystring("codigo_aut")
codigo_dma=request.querystring("codigo_dma")
notaminima_cac=request.querystring("notaminima_cac")
codigouniver_alu=trim(request.querystring("codigouniver_alu"))

Set obEnc=server.createobject("EncriptaCodigos.clsEncripta")
	codigofoto=obEnc.CodificaWeb("069" & codigouniver_alu)
set obEnc=Nothing
%>

<html xmlns="http://www.w3.org/1999/xhtml">

<head>
<meta http-equiv="Content-Language" content="es">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252" />
<title>MODIFICAR NOTA</title>
<link rel="stylesheet" type="text/css" href="../../../../private/estilo.css">
<script language="JavaScript" src="../../../../private/funciones.js"></script>
<script language="JavaScript" src="../private/validarnotas.js"></script>
<style type="text/css">
.Aprobado {
	color: #0000FF;
}
.Desaprobado{
	color: #FF0000;
}

</style>
</head>
<body style="background-color: #CCCCCC" onload="txtnotafinal_bin.focus();txtnotafinal_bin.select()">
<table cellpadding="4" cellspacing="0" width="100%" class="contornotabla">
	<tr>
		<td rowspan="3" style="width: 15%" valign="top">
		<!--
		    '---------------------------------------------------------------------------------------------------------------
            'Fecha: 29.10.2012
            'Usuario: dguevara
            'Modificacion: Se modifico el http://www.usat.edu.pe por http://intranet.usat.edu.pe
            '---------------------------------------------------------------------------------------------------------------
		-->
		<img border="1" src="//intranet.usat.edu.pe/imgestudiantes/<%=codigofoto%>" width="100px" height="120px" alt="Sin Foto">
		</td>
		<td >Código</td>
		<td style="width: 75%" >:&nbsp;<%=codigouniver_alu%>&nbsp;</td>
	</tr>
	<tr>
		<td >Estudiante</td>
		<td style="width: 75%" >:&nbsp;<%=request.querystring("alumno")%>&nbsp;</td>
	</tr>
	<tr>
		<td colspan="2" >
		<table width="100%" cellspacing="3" cellpadding="3" border="0">
			<tr>
		<td style="width: 20%">Nota anterior:</td>
		<td style="width: 15%">&nbsp;<%=request.querystring("nota")%></td>
		<td style="width: 30%">Condición anterior:</td>
		<td style="width: 15%" class="<%=request.querystring("condicion")%>"><%=request.querystring("condicion")%>&nbsp;</td>
			</tr>
			<tr>
		<td style="height: 7px; width: 20%;" class="etiqueta">Nota Nueva:</td>
		<td style="height: 7px; width: 15%;">
		<input class="Cajas" type="textbox" name="txtnotafinal_bin" onkeypress="validarnumero()" onkeyup="validarnota(this,'<%=notaminima_cac%>',msgcondicion_dma)" size="3" maxlength="2" condicion_dma="D" value="0" onblur="if(this.value==''){this.value=0}">
		</td>
		<td style="height: 7px; width: 30%;" class="etiqueta">Condición Nueva:</td>
		<td style="width: 15%; height: 7px;" id="msgcondicion_dma"></td>
		</tr>
		<tr>
		<td colspan="4" >Indique el motivo de cambio de nota</td>
		</tr>
		<tr>
		<td colspan="4" >
		<input type="text" name="txtmotivo_bin" id="txtmotivo_bin" class="Cajas2" maxlength="50"/></td>
		</tr>
		</table>
		</td>
	</tr>
		<tr>
		<td colspan="3" align="right" id="mensaje" class="rojo">
		<input name="cmdGuardar" type="submit" value="Guardar" class="usatGuardar" onclick="EnviarNotaModificada('<%=codigo_dma%>','<%=codigo_aut%>','<%=notaminima_cac%>')">
		<input name="cmdCancelar" type="button" value="Cancelar" class="usatSalir" onclick="window.close()">
		</td>
	</tr>

</table>

</body>

</html>
