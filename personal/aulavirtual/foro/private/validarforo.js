function AbrirForo(modo,idforo,numfila,idestado)
{
 	switch(modo)
	{
		case "A":
			AbrirPopUp("frmforo.asp?accion=agregarforo","350","600")
			break

		case "M":
			AbrirPopUp("frmforo.asp?accion=modificarforo&idforo=" + idforo + "&numfila=" + numfila,"350","600")
			break

		case "E":
			var pagina="procesar.asp?accion=eliminarforo&idforo=" + idforo
			Eliminar("La acción a realizar es irreversible\n¿Está seguro completamente seguro que desea eliminar el foro?",pagina)
			break
		case "R"://Regresar de mensajes a foro
			location.href="listamensajes.asp?idforo=" + idforo + "&tituloforo=" + numfila + "&idestadorecurso=" + idestado
			break
	}
}

function AbrirMensaje(modo,id,var1,var2,var3)
{
 	switch(modo)
	{
		case "A": //Agregar
			AbrirPopUp("frmmensaje.asp?accion=agregarforomensaje&idforo=" + id,"450","650")
			break

		case "M": //Modificar
			AbrirPopUp("frmmensaje.asp?accion=modificarforomensaje&idforomensaje=" + id,"450","650")
			break

		case "E": //Eliminar
			var pagina="procesar.asp?accion=eliminarforomensaje&idforomensaje=" + id + "&idforo=" + var1 + "&tituloforo=" + var2 + "&idestadorecurso=" + var3
			Eliminar("La acción a realizar es irreversible\n¿Está seguro completamente seguro que desea eliminar el mensaje seleccionado?",pagina)
			break
		case "D": //Detalle del mensaje
			location.href="detallemensaje.asp?idforomensaje=" + id + "&idforo=" + var1 + "&tituloforo=" + var2 + "&idestadorecurso=" + var3
			break

		case "R": //Responder a mensaje
			AbrirPopUp("frmmensaje.asp?accion=agregarforomensaje&rpta=RE:&idforo=" + id + "&titulomensaje=" + var1 + "&idforomensaje=" + var2,"450","650")
			break

	}
}

//Activar criterios de calificacion

function activarcalificacion(ctrl)
{
	if (ctrl.checked==true){
		frmforo.tipocalificacion.disabled=""
		frmforo.numcalificacion.disabled=""
	}
	else{	
		frmforo.tipocalificacion.disabled="true"
		frmforo.numcalificacion.disabled="true"
	}
}

//Validar datos de ingreso del documento
function validarforo(formulario)
{
  var estado=true

  if (formulario.tituloforo.value == "")
  {
    alert("Ingrese el título de la foro.");
    formulario.tituloforo.focus();
    return (false);
  }

  if (formulario.descripcion.value == "")
  {
    alert("Especifique en que consiste la foro que va a asignar.");
    formulario.descripcion.focus();
    return (false);
  }

  estado=CompararFechas(formulario.fechainicio.value,formulario.fechafin.value)
  if (estado==false)
     {return (false)}

  DesactivarControlesfrm(formulario)
  return (true);
}

//Validar datos de ingreso del documento
function validarmensaje(formulario)
{
  if (formulario.titulomensaje.value == "")
  {
    alert("Ingrese el título de la respuesta.");
    formulario.titulomensaje.focus();
    return (false);
  }

  formulario.web.value = Contenido.document.body.innerHTML

  if (formulario.web.value=="")
  {
    alert("Especifique en que consiste el contenido del mensaje.");
    Contenido.focus();
    return (false);
  }


  if (formulario.web.value.length>3999)
  {
    alert("El contenido de la respuesta no debe contener más de 4000 caracteres.");
    Contenido.focus();
    return (false);
  }
  
  return (true);
}


