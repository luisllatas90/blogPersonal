<%
if(session("codigo_Usu") = "") then
    response.Redirect "../../../../sinacceso.html"
end if

num=request.querystring("num")
if num="" then num=1



%>
<html xmlns="http://www.w3.org/1999/xhtml">

<head>
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252" />
<title>Enviar fotos de estudiantes</title>
<link rel="stylesheet" type="text/css" href="../../../../private/estilo.css" />
<script type="text/javascript" language="JavaScript" src="../../../../private/funciones.js"></script>
<script type="text/javascript" language="javascript">
function EnviarFotos()
{
	var vacias=0
	var invalidas=0
	
	//Validar Objetos File y tipo de archivo
	for(var i = 0; i<frmFotos.elements.length; i ++) {
		if(frmFotos.elements[i].type=='file'){
			var actual=frmFotos.elements[i].value
			
			if (actual.length>4){
				actual=actual.toLocaleLowerCase()
				extension=actual.substr(actual.length-3,3)
				
				if (extension!="jpg"){
					invalidas=i
					break
				}
			}
			else{	
				vacias=i
				break
			}
		}
	}
	
	if (vacias>0){	
		alert("Debe especificar la ruta de la foto Nº [" + vacias + "]")
		frmFotos.elements[vacias].focus()
		return (false);
	}
	
	if (invalidas>0){
		alert("ERROR! en el tipo de archivo de la foto Nº [" + invalidas + "]\nSólo debe publicar archivos de tipo .JPG")
		frmFotos.elements[invalidas].focus()		
		return (false);
	}
	
	if (confirm("¿Está seguro que desea publicar las fotos seleccionadas?")==true){
		frmFotos.cmdCopiar.style.display="none"
		tblfotos.style.display="none"
		tblmensaje.style.display=""
		
		frmFotos.action="guardarfotos.asp?num=<%=num%>"
		frmFotos.submit()
	}
}
</script>
</head>

<body bgcolor="#f0f0f0">
<p class="usatTitulo">Publicar fotos de estudiantes</p>
<FORM name="frmFotos" method="post" encType="multipart/form-data">
<table id="tblfotos" width="100%" cellpadding="3" class="contornotabla">
	<tr class="pestanabloqueada">
		<td style="width: 5%" class="etiqueta">Fotos</td>
		<td style="width: 95%" class="rojo">
		<select name="txtnumero" onchange="location.href='frmsubirfoto.asp?num=' + this.value">
			<%for i=1 to 20%>	
			<option value="<%=i%>" <%if int(num)=int(i) then response.write "SELECTED"%>><%="&nbsp;&nbsp;" & i%></option>
			<%next%>
		</select>
		(*)Especifique el número de fotos a publicar
		</td>
	</tr>
	<%for i=1 to num%>	
	<tr>
		<td style="width: 5%" align="right"><%=i%></td>
		<td style="width: 95%">
		<INPUT class="Cajas" type="file" name="File<%=i%>" size="70">		
		</td>
	</tr>
	<%next%>
</table>
<p align="right">
<INPUT type="button" value="      Guardar fotos" name="cmdCopiar" onclick="EnviarFotos()" class="guardar_prp">
</p>
<table id="tblmensaje" style="display:none" width="100%" height="300px" cellpadding="3" class="contornotabla">
	<tr>
	<td align="center">
		<h4 align='center' class='rojo'>Espere un momento por favor<br>
		<img src='../../../../images/cargando.gif' width='300' height='25'>
		</h4>
		<h5 align='center'>se están publicando las fotos al sistema...</h5>
	</td>
	</tr>	
</table>

</FORM>
</body>
</html>