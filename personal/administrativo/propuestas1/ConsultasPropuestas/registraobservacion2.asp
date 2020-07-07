<!--#include file="../../../../funciones.asp"-->

<html>
<head>

<title>Registro de Investigaciones</title>

<link href="../../../../private/estilo.css" rel="stylesheet" type="text/css">
</head>
<script language="JavaScript" src="private/validarpropuestas.js"></script>
<script language="JavaScript" src="../../../../private/funciones.js"></script>
<script>

function Busqueda(cadena)
{
location.href="registrapropuesta2.asp?RESPONSABLE=" + cadena 
}
</script>
<body>
<center>
  <span class="usatTituloAplicacion">Registro de Observaci&oacute;n</span> - <span class="usatMisTitulos">Paso 2 </span>
</center>
<p align="center"><span class="usatCeldaMenuSubTitulo">Se ha registrado su comentario satiscatoriamente. Si desea volver a la propuesta haga clic aqu&iacute;:</span></p>
<p align="center"><a href="verdetallepropuesta_dir.asp?propuesta=<%=67%>"><img src="../../../../images/back.gif" width="18" height="18" border="0"></a> <br>
<strong class="usatInfReg">(Volver a Propuesta) </strong></p>
<p align="center"><span class="usatCeldaMenuSubTitulo">Caso contrario continu&eacute; y adjunte sus archivos </span></p>
<table width="70%" border="0" align="center" cellpadding="0" cellspacing="0" class="usatsugerencia">
  <tr>
    <td>&nbsp;&nbsp;&nbsp;&nbsp;Escriba el nombre que describe al archivo adjunto de su comentario (propuesta, presupuesto, convenio, anexo, etc) y seleccione la ruta. </td>
  </tr>
</table>
<br>
<form id="frmSubir" action="guardararchivo.asp?codigo_prp=<%=session("propuesta")%>" method="post" enctype="multipart/form-data">
        <input type="hidden" name="doCreate" value="true">
        <table border="0" cellpadding="5" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%" class="contornotabla">
          <tr>
            <td class="bordeinf" width="100%" class="etiqueta" bgcolor="#E6E6FA">
            <b>Especificar Ruta del archivo de la propuesta: </b> </td>
          </tr>
          <tr>
            <td width="100%">Descripci&oacute;n del Archivo:            
            <input name="descripcion_archivo" type="text" id="descripcion_archivo" size="55"></td>
          </tr>
          
          <tr>
            <td width="100%" >Ruta del Archivo:            
            <input type="file" name="filename" size="62"></td>
          </tr>
          <tr>
            <td >&nbsp;</td>
          </tr>
          <tr>
            <td class="rojo"><b>NOTA:</b> Debe 
			    tomar en cuenta que el tamaño del archivo, no sobrepase los <b>100 Mb</b> y 
			    debe estar empaquetado (.<b>ZIP</b>), caso contrario el sistema <b>no le 
			      admitirá subir el archivo</b>. Recuerde tomar en cuenta estas 
			    indicaciones para que estudiante pueda descargar con facilitad el 
			    archivo.<br>
                <br>
Por favor haga click en el botón &quot;Sí&quot;, en el mensaje que se mostrará 
		    a continuación, para activar la validación del archivo. </td>
          </tr>
          <tr>
            <td width="100%" class="rojo" align="right">
			<input type="button" name="cmdEnviar" onClick="ValidarEnvioPropuestas()" value="    Enviar Archivo" class="guardar2" style="width: 120"></td>
          </tr>
  </table>
		<p class="usatCeldaMenuSubTitulo">Archivos:</p>
		<table width="80%" border="0" align="center" cellpadding="0" cellspacing="0" bordercolor="#FFFFFF" class="contornotabla">
          <tr>
            <td align="center" class="bordeinf">Nombre del Archivo </td>
            <td width="10%" align="center" class="bordeinf">Ver</td>
          </tr>
          <tr>
            <td>&nbsp;</td>
            <td align="center"><img src="../../../../images/readebook.gif" width="20" height="20"></td>
          </tr>
        </table>
		<p>&nbsp;</p>
</form>
</body>
</html>
