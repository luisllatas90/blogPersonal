

function ConfirmarEliminar(mensaje,pagina)
{
	var Confirmar=confirm(mensaje);
	if(Confirmar==true)
		{document.location.href=pagina + "&modalidad=Eliminar"}
	else
		{return false;}
}

function validarmenu(formulario)
{

  if (formulario.descripcion_men.value.length < 3)
  {
    alert("Ingrese la descripción del menú.");
    formulario.descripcion_men.focus();
    return (false);
  }

  if (formulario.enlace_men.value.length < 3)
  {
    alert("Ingrese la dirección web del menú.");
    formulario.enlace_men.focus();
    return (false);
  }
  return(true)
}



function validartarea(formulario)
{
  var estado=true

  if (formulario.titulotarea.value == "")
  {
    alert("Ingrese el título de la tarea.");
    formulario.titulotarea.focus();
    return (false);
  }
 
  if (formulario.lugar.value == "")
  {
    alert("Ingrese el lugar donde se desarrollará la tarea.");
    formulario.lugar.focus();
    return (false);
  }

  estado=CompararFechas(formulario.fechainicio.value,formulario.fechafin.value)

  if (estado==false)
     {return (false)}

  return (true);
}




function validardesempeno()
{

	if (frmdesempeno.obs.value == "")
	{
	alert("Por favor ingrese el campo Observaciones del desempeño");
	frmdesempeno.obs.focus();
	return (false);
	}

return (true);
}

function ElegirUnaRpta(numero)
	{
	var control = document.all.item("rptacorrecta");
	if (control!=null)
	   {if (control.length!=null)
		{
	        for (i=0; i<control.length; i++)
		    {
		       if (i!=numero)
			      {control(i).value=0}
			   else
			      {control(i).value=1}
		    }
		}
	    }
	}
	
function ElegirVariasRptas(numero)
{
	var control= document.all.rptacorrecta
	var chk= document.all.chkCorrecta
	for (i=0; i<chk.length; i++){
		if (chk[i].checked==true)
			{control(i).value=1}
		else
		    {control(i).value=0}
	}
}


function validarfechapresencial(formulario)
{

  if (formulario.dia.selectedIndex == 0)
  {
    alert("Elija el día presencial de la Actividad Académica.");
    formulario.dia.focus();
    return (false);
  }

  if (formulario.mes.selectedIndex == 0)
  {
    alert("Elija el mes presencial de la Actividad Académica.");
    formulario.mes.focus();
    return (false);
  }
  return (true);
}

function validarcriterioevaluacion(formulario)
{

  if (formulario.titulocriterio.value == "")
  {
    alert("Debe digitar la descripción del criterio de evaluación.");
    formulario.titulocriterio.focus();
    return (false);
  }
  return (true);
}