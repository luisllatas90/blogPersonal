<%
codigo_cup=request.querystring("codigo_cup")
descripcion_cac=request.querystring("descripcion_cac")
%>
<html>
	<head>
		<title>Subir silabus de asignatura programada </title>
		<link href="../../../private/estilo.css" rel="stylesheet" type="text/css">
		<script language="JavaScript" src="../../../private/funciones.js"></script>
		<script language="JavaScript" src="private/validarsilabos.js"></script>
	</head>
	<body>
        <form id="frmSubir" method="post" encType="multipart/form-data" action="guardararchivo.asp">
            <input type="hidden" id="hdcodigo_cup" name="hdcodigo_cup" value="<%=codigo_cup%>" />
	        <input type="hidden" id="hddescripcion_cac" name="hddescripcion_cac" value="<%=descripcion_cac%>" />

        <table border="0" cellpadding="5" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%" class="contornotabla">
          <tr>
            <td class="bordeinf" width="100%" class="etiqueta" bgcolor="#E6E6FA">
            <b>Especificar Ruta del Silabus de la asignatura programada: </b> </td>
          </tr>
          <tr>
            <td width="100%">
			<input type="file" name="File1" id="File1" size="40" class="cajas2"></td>
          </tr>
          <tr>
            <td width="100%" class="rojo">
			<b> NOTA:</b> Debe 
        tomar en cuenta que el tamaño del archivo, no sobrepase los <b>300 kb</b> y 
        debe estar empaquetado (.<b>ZIP</b>), caso contrario el sistema <b>no le 
            admitirá subir el archivo</b>. Recuerde tomar en cuenta estas 
            indicaciones para que estudiante pueda descargar con facilitad el 
            archivo.
            </td>
          </tr>
          <tr>
            <td width="100%" class="rojo" align="right">
			<input type="button" name="cmdEnviar" onclick="ValidarSilabos()" value="    Enviar Archivo" class="guardar2" style="width: 120"></td>
          </tr>
          </table>
		</form>
	</body>
</html>