/*
================================================================================================
	CVUSAT
	Fecha de Creación: 11/03/2006
	Fecha de Modificación: 13/03/2006
	Creador: Gerardo Chunga Chinguel 
	Modificado por: hreyes
	Obs: Realiza las validaciones y procedimientos para el módulo de Sílabos
================================================================================================
*/

var Mensaje= new Array(10);
Mensaje[0] = "¿Está completamente seguro que desea guardar los cambios?\n Recuerde que para la modificación de notas debe acercarse al área de Evaluación y Registro"
Mensaje[1] = "¿Está seguro que desea Eliminar el Sílabos del curso programado?"


function ValidarSilabos()
{
   var NombreArch
   var extension
   var ancho
   NombreArch=new String(frmSubir("File1").value)
   ancho=NombreArch.length

   //Validar el tipo de archivo
   if(ancho>4){
	NombreArch=NombreArch.toLocaleLowerCase() 
	extension=NombreArch.substr(ancho-3,3)
	if(extension=="zip"){
		frmSubir.submit()//ValidarTamanio(310,NombreArch)
	}
	else{
		alert("Solo se pueden subir arhivos con extensión ZIP")
	}
   }
   else{
	alert("Debe especificar la ruta del archivo ZIP")
   }
}

function ValidarTamanio(limite,rutaarchivo)
{
	var TamanoArchivo=0
	var fs,archivo

	fs = new ActiveXObject("Scripting.FileSystemObject")

	archivo = fs.getFile(rutaarchivo) //frmSubir.File1.value

	TamanoArchivo=archivo.size
	TamanoArchivo=parseInt(eval(TamanoArchivo)/1024) //FormatNumber(cdbl(TamanoArchivo)/1024,0,0,0,-1)
	
	if (TamanoArchivo>limite){
		alert("El archivo sobrepasa el límite recomendado de 300 kb. Por favor trate de reducir el tamaño del archivo")
	}
	else{
		frmSubir.submit()
	}
}


function AccionSilabos(modo,ca,esc,cu,cup,tbl,fila)
{
	var pagina=""
	switch(modo)
	{
		case "E":
			var Confirmar=confirm(Mensaje[1]);
			if (Confirmar == true) {
			    pagina = "procesar.asp?accion=eliminarsilabos&codigo_cup=" + cu + "&codigo_cac=" + ca + "&codigo_cpf=" + esc + "&descripcion_cac=" + cup + "&modulo=" + tbl
				//AbrirPopUp(pagina,"10","10")
				location.href = pagina
				
			}
			else
				{return false}
			break

		case "A":
			AbrirPopUp("frmsubirsilaboAdministrador.asp?descripcion_cac=" + ca + "&codigo_cup=" + cu,"250","450")
			break
			
		case "C":
			if (cbocodigo_cpf.value==-2){
				alert("Debe seleccionar una Escuela Profesional")
			}
			else{
				pagina="../academico/silabos/frmadminsilabosAdministrador.asp?codigo_cac=" + cbocodigo_cac.value + "&codigo_cpf=" + cbocodigo_cpf.value + "&descripcion_cac=" + cbocodigo_cac.options[cbocodigo_cac.selectedIndex].text + "&mod=" + ca
			}
			
			if (pagina!=""){
				window.location.href="../../aplicacionweb2/cargando.asp?rutapagina=" + pagina
			}
			
			break
	}
}

function ActualizarListaSilabos(pagina)
{
	var cac=cbocodigo_cac.value
	var cpf=cbocodigo_cpf.value
	var dca=cbocodigo_cac.options[cbocodigo_cac.selectedIndex].text

	AbrirMensaje('../../../images/')
	location.href=pagina + "?codigo_cac=" + cac + "&codigo_cpf=" + cpf + "&descripcion_cac=" + dca
}