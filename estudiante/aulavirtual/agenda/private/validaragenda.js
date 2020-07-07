function validaragenda(formulario)
{
  var estado=true

  if (formulario.tituloagenda.value == "")
  {
    alert("Ingrese el título del evento.");
    formulario.tituloagenda.focus();
    return (false);
  }
 
  if (formulario.lugar.value == "")
  {
    alert("Ingrese el lugar donde se desarrollará el evento.");
    formulario.lugar.focus();
    return (false);
  }
 
  estado=CompararFechas(formulario.fechainicio.value,formulario.fechafin.value)

  if (estado==false)
     {return (false)}

 return (true);
}


function AbrirAgenda(modo)
{
  var idagenda=fralista.txtelegido.value

	if (modo=="A"){
		AbrirPopUp("frmagenda.asp?accion=agregaragenda&idagenda=" + idagenda,"300","620")
	}
	else{
   	  if (idagenda!=""){
		if (modo=="M"){
			AbrirPopUp("frmagenda.asp?accion=modificaragenda&idagenda=" + idagenda,"300","620")
		}

		if (modo=="E"){
			var pagina="procesar.asp?accion=eliminaragenda&idagenda=" + idagenda
			Eliminar("¿Está seguro que desea Eliminar la Agenda seleccionada?",pagina)
		}

		if (modo=="P"){
			var nombreevento=fralista.document.getElementById("nombreevento" + idagenda).innerText
			var ruta="../usuario/frmcompartirrecurso.asp"
			var descripcion=""
			AbrirPopUp(ruta + "?titulo=Agenda: " + nombreevento + "&idtabla=" + idagenda + "&nombretabla=agenda&descripcion=" + descripcion,"500","620")
		}
	   }
	   else
		{alert("Seleccione el evento de la agenda para ejecutar esta operación")}
	}
}