function ResaltarEvaluacion(numfila,ideval)
{
	var tbl = document.all.tbllistaevaluacion
	var ArrFilas = tbl.getElementsByTagName('tr')
	var tfilas = ArrFilas.length
	for (var c = 0; c < tfilas; c++){
		var Fila=ArrFilas[c]
		var Celda=Fila.getElementsByTagName('td')
		
		if(numfila==c){
			Fila.style.backgroundColor = "#395ACC"
			Celda[0].style.backgroundColor = ""
			Celda[1].style.color = "white"
			Celda[2].style.color = "white"
		}
		else {
			Fila.style.backgroundColor = ""
			Celda[1].style.color = "black"
			Celda[2].style.color = "black"
		}
	}
	
	txtelegido.value=ideval
	mensajedetalleeval.style.display="none"
	fradetalle.location.href="detalleevaluacion.asp?idevaluacion=" + ideval + "&numfila=" + numfila
}


function AbrirEvaluacion(modo,ideval,nfila)
{
	switch (modo)
	{
		case "A":
			AbrirPopUp("frmevaluacion.asp?Accion=agregarevaluacion","400","620")
			OcultarMenuContextual(MenuEval)		
			break

		case "M":
			var tituloeval=txttituloevaluacion.innerText
			AbrirPopUp("frmadministrar.asp?Accion=modificarevaluacion&idevaluacion=" + ideval + "&tituloevaluacion=" + tituloeval + "&numfila=" + nfila,"500","650")
			break

		case "E":
			var pagina="procesar.asp?accion=eliminarevaluacion&idevaluacion=" + ideval
			Eliminar("Acción Irreversible.\n¿Está seguro completamente seguro que desea Eliminar la encuesta seleccionada?",pagina)
			break

		case "P":
			var tituloeval=txttituloevaluacion.value
			var ruta="../usuario/frmcompartirrecurso.asp"
			var descripcion=""
			AbrirPopUp(ruta + "?titulo=Evaluación: " + tituloeval + "&idtabla=" + ideval + "&nombretabla=evaluacion&descripcion=" + descripcion,"500","620")
			break
		case "I":
			window.open("abrirevaluacion.asp?accion=iniciarencuesta&idevaluacion=" + ideval,"frmevaluacion",PropVentana)
			txtmensajeinicio.innerHTML="<img src='../../../images/bloquear.gif'><br>Encuesta iniciada"
			break
		case "T":
			top.location.href="abrirevaluacion.asp?accion=terminarencuesta"
			break
	}
}

function validartiempoevaluacion()
{
 if (frmevaluacion.minutos.value < 5 ||frmevaluacion.minutos.value > 120)
 	{
 		alert('El tiempo de evaluación debe ser entre 5 y 120 minutos')
 		frmevaluacion.minutos.value=0;
 		frmevaluacion.minutos.focus();
	    return (false);
     	}
 return (true);
}

//Validar datos de ingreso de la evaluación
function validarevaluacion(formulario)
{
  var estado=true

  if (formulario.tituloevaluacion.value == "")
  {
   alert("Ingrese el título de la evaluación.");
    formulario.tituloevaluacion.focus();
    return (false);
  }

  if (formulario.descripcion.value == "")
  {
    alert("Ingrese la descripción sobre qué trata la evaluación.");
    formulario.descripcion.focus();
    return (false);
  }

  if (formulario.instrucciones.value == "")
  {
    alert("Ingrese las instrucciones que deben de tomar en cuenta los que serán evaluados.");
    formulario.instrucciones.focus();
    return (false);
  }

  estado=CompararFechas(formulario.fechainicio.value,formulario.fechafin.value)

  if (estado==false)
     {return (false)}

  DesactivarControlesfrm(formulario)
  return (true);
}


function validarpregunta(formulario)
{
	if (formulario.titulopregunta.value == "")
  		{
		alert("Escriba el enunciado de la pregunta.");
	    	formulario.titulopregunta.focus();
		return (false)
	    	}
	if (formulario.ordenpregunta.value == "")
  		{
	    	alert("Ingrese el orden que tiene la pregunta respecto a la evaluación.");
	    	formulario.ordenpregunta.focus();
		return (false)
	    	}
     return (true);
}

function seleccionarTipopregunta(idG)
{
var Confirmar=confirm("Recuerde que si cambia el tipo de Pregunta una vez \n agregadas las alternativas, estas se ELIMINARÁN \n\n Está seguro que desea cambiar el Tipo de Pregunta \n\n Para confirmar la Eliminación de las alternativas \n haga click en el botón Guardar caso contrario Cancelar")
if(idG!=frmPregunta.idtipopregunta.value)
	if (Confirmar==true)
		frmPregunta.idtipopregunta.value=frmPregunta.idtipopregunta.value
	else
		frmPregunta.idtipopregunta.value=idG
}
	
function cambiartipopregunta(accion,ideval,idpreg)
{
	location.href="frmpregunta.asp?idtipopregunta=" + frmPregunta.idtipopregunta.value + "&accion=" + accion + "&idevaluacion=" + ideval + "&IdPregunta=" + idpreg
}


function validarAlternativa(formulario)
{

  if (formulario.orden.value == "")
  {
    alert("Ingrese el orden que va a tener la alternativa.");
    formulario.orden.focus();
    return (false);
  }

  if (formulario.tituloalternativa.value == "")
  {
    alert("Ingrese la descripción de la alternativa.");
    formulario.tituloalternativa.focus();
    return (false);
  }
  return (true);
}

function EliminarAlternativa(idalt,idtipopreg,idpreg)
{
	var pagina="frmalternativa.asp?modalidad=Eliminar&idalternativa=" + idalt + "&idtipopregunta=" + idtipopreg + "&idpregunta=" + idpreg
	var mensaje="¿Está seguro completamente seguro que desea Eliminar la alternativa seleccionada?"
	Eliminar(mensaje,pagina)
}

function EliminarPregunta(idpreg,ideval)
{
	var pagina="procesar.asp?accion=eliminarpregunta&idpregunta=" + idpreg + "&idevaluacion=" + ideval
	var mensaje="Acción irreversible.\n ¿Está seguro completamente seguro que desea Eliminar la pregunta?"
	Eliminar(mensaje,pagina)
}