/*--------------------------------------------------------------------------------------------
'CV-USAT
'Archivo			: validaciones.JS
'Autor			: Hugo E. Saavedra Sánchez
'Fecha de Creación		: 12/07/200606:20:53 p.m.
'Observaciones		: Permite validar los campos de ingreso para la tabla propuesta
--------------------------------------------------------------------------------------------*/
var Mensaje= new Array(10);
Mensaje[0] = "¿Está completamente seguro que desea guardar los cambios?\n Recuerde que para la modificación de notas debe acercarse al área de Evaluación y Registro"
Mensaje[1] = "¿Está seguro que desea Eliminar el Sílabos del curso programado?"


function ValidarEnvioPropuestas()
{
   var NombreArch
   var extension
   var ancho
   NombreArch=new String(frmSubir("filename").value)
   ancho=NombreArch.length

   //Validar el tipo de archivo
   if(ancho>4){
	NombreArch=NombreArch.toLocaleLowerCase() 
	extension=NombreArch.substr(ancho-3,3)
	if(extension=="zip"){
		frmSubir.submit()//ValidarTamanio(310,NombreArch)
	}
	else{
		alert("Solo se pueden subir arhivos con extensiones DOC, XLS, PPT, PDF o ZIP")
	}
   }
   else{
	alert("Debe especificar la ruta del archivo")
   }
}



function AccionSilabos(modo,ca,esc,cu,cup,tbl,fila)
{
	var pagina=""
	switch(modo)
	{
		case "E":
			var Confirmar=confirm(Mensaje[1]);
			if(Confirmar==true){
				pagina="procesar.asp?accion=eliminarsilabos&codigo_cup=" + cu + "&codigo_cac=" + ca + "&codigo_cpf=" + esc + "&descripcion_cac=" + cup
				AbrirPopUp(pagina,"10","10")
				//location.href=pagina
			}
			else
				{return false}
			break

		case "A":
			AbrirPopUp("frmsubirsilabo.asp?descripcion_cac=" + ca + "&codigo_cup=" + cu,"250","450")
			break
	}
}

/*function ActualizarListaSilabos(pagina)
{
	var cac=cbocodigo_cac.value
	var cpf=cbocodigo_cpf.value
	var dca=cbocodigo_cac.options[cbocodigo_cac.selectedIndex].text

	AbrirMensaje('../../../images/')
	location.href=pagina + "?codigo_cac=" + cac + "&codigo_cpf=" + cpf + "&descripcion_cac=" + dca
}*/

/**EN FORMULARIO REGISTRO PROPUESTAS PASO 1*/

function validarfrmpropuesta()
{

	if (frmpropuesta.txtnombre_prp.value == "")
	{
	alert("Por favor ingrese el campo nombre_Prp");
	frmpropuesta.txtnombre_prp.focus();
	return (false);
	}

	if (frmpropuesta.txtdescripcion_prp.value == "")
	{
	alert("Por favor ingrese el campo descripcion_Prp");
	frmpropuesta.txtdescripcion_prp.focus();
	return (false);
	}

return (true);
}
