function validarinscripcion()
{
	if (frminscripcioncursovirtual.cbxasignatura.value == "-2")
	{
	alert("Por favor especifique la asignatura, en la cual desarrollará su investigación");
	frminscripcioncursovirtual.cbxasignatura.focus();
	return (false);
	}

	if (frminscripcioncursovirtual.cbxescuela.value == "-2")
	{
	alert("Por favor especifique la Escuela Profesional de la asignatura");
	frminscripcioncursovirtual.cbxescuela.focus();
	return (false);
	}

	if (frminscripcioncursovirtual.txttiempo.value=="" || frminscripcioncursovirtual.txttiempo.value=="0")
	{
	alert("Por favor especifique el tiempo en el que desarrolla la asignatura");
	frminscripcioncursovirtual.txttiempo.focus();
	return (false);
	}

	if (frminscripcioncursovirtual.file!=undefined){
	   if (frminscripcioncursovirtual.file.value!=""){
		var ext = document.frminscripcioncursovirtual.file.value;
  		ext = ext.substring(ext.length-3,ext.length);
		ext = ext.toLowerCase();
		if(ext != 'jpg'){
		    alert("El tipo de archivo no es válido para la foto personal\n Sólo se aceptan tipos de archivo imagen en formato JPG");
		    frminscripcioncursovirtual.file.focus();
   		    return false;
		}
	   }
	}
	
    return (true);
}
