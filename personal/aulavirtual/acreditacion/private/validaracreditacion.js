function validarnumero()
{
	if (event.keyCode < 45 || event.keyCode > 57)
		{event.returnValue = false}
}

function MM_goToURL()
{
  var i, args=MM_goToURL.arguments; document.MM_returnValue = false;
  for (i=0; i<(args.length-1); i+=2)
  		eval(args[i]+".location='"+args[i+1]+"'");
}

function ConfirmarEliminar(mensaje,pagina)
{
	var Confirmar=confirm(mensaje);
	if(Confirmar==true)
		{document.location.href=pagina + "&modalidad=Eliminar"}
	else
		{return false;}
}

function MedioDiaHoraFin()
{
	if (document.all.horafin.value==12){
		document.all.turnofin.value="p.m.";
		document.all.turnofin.disabled=true
	}
	else{
		document.all.turnofin.value="a.m.";
		document.all.turnofin.disabled=false
	}
}

function validartareaevaluacion(formulario)
{

  if (formulario.titulotarea.value.length < 3)
  {
    alert("Ingrese la descripción de la tarea.");
    formulario.titulotarea.focus();
    return (false);
  }
   return (true);
}


/*
Validar el inicio del avance, especificando si se acepta o no la tarea
y luego validar el formulario para subir el archivo
*/

function mensajetipoavance(num)
{
	var numetiq=etiqueta.length
		
	for (i=0;i<numetiq;i++){
		var etiq=etiqueta[i]
		var fil=fila[i]
		if (i==num){
		 	fil.style.display=""
		 	fil.style.backgroundColor="#DFEFFF"
		 	etiq.style.backgroundColor="#DFEFFF"
		}
		else{
			fil.style.display="none"
		 	fil.style.backgroundColor="#FFFFFF"		 		
		 	etiq.style.backgroundColor="#FFFFFF"
	 		cmdAceptar.value="Guardar"
	 		cmdAceptar.disabled=true
	 	}
		if (num==0){
			cmdAceptar.value="Siguiente"
			cmdAceptar.disabled=false
 		}
	}
}

function validarmotivoavance()
{
	var pagina=""
	var motivo=""

	if (tipoavance[1].checked==true && motivoincompleto.value.length>3){
		alert("Por favor especifique el motivo por la cual no se ha completo el documento")
		motivoincompleto.focus()
		return(false)
	}

	if (tipoavance[2].checked==true && motivonoexiste.value.length>3){
		alert("Por favor especifique el motivo por la cual no existe el documento")
		motivonoexiste.focus()
		return(false)
	}

	if (tipoavance[3].checked==true && idusuario.value==0){
		alert("Por favor especifique a que persona delegará el desarrollo de la Tarea")
		idusuario.focus()
		return(false)
	}
	
   return(true)
}


function validaravance(formulario,maxpje)
{

  if (formulario.tituloarchivoavance.value.length < 3)
  {
    alert("Ingrese la descripción del documento");
    formulario.tituloarchivoavance.focus();
    return (false);
  }
  
  if (formulario.file.value == "")
  {
  	alert("Seleccione el documento que va a adjuntar.Haga click en el botón EXAMINAR");
	formulario.file.focus();
    return (false);
   }
  
  if (formulario.pjeavance.value==""){
    alert("El porcentaje de avance debe ser mayor al " + maxpje + "% y menor o igual al 100%");
    formulario.pjeavance.focus();
    return (false);
  }
	else{
		if (parseFloat(formulario.pjeavance.value)<maxpje){
			alert("El porcentaje de avance debe ser mayor al " + maxpje + "% y menor o igual al 100%");
			formulario.pjeavance.focus();
			return (false);
		}
	  if (parseFloat(formulario.pjeavance.value)>100){
		alert("El porcentaje de avance debe ser menor o igual al 100%");
		formulario.pjeavance.focus();
		formulario.pjeavance.value="";
		return (false);
	}
}

   return (true);
}

function AbrirAvance(idtarea,autoriz,maxpje,idevalindicador,idvar,idsecc)
{
	var izq = (screen.width-600)/2
	var arriba= (screen.height-300)/2
	var pagina="realizartarea1.asp?modo=agregaravance&idtareaevaluacion=" + idtarea + "&autorizar=" + autoriz + "&maxpje=" + maxpje + "&idevaluacionindicador=" + idevalindicador + "&idvariable=" + idvar + "&idseccion=" + idsecc
	var ventana=window.open("","popup","height=300,width=600,statusbar=yes,scrollbars=no,top=" + arriba + ",left=" + izq + ",resizable=no,toolbar=no,menubar=no");
	ventana.location.href=pagina
	ventana=null
}

function abrirseccionmenu(idseccion,nombresecc,filamarcada)
{
	var tbl = document.all.tblMenu
	var ArrFilas = tbl.getElementsByTagName('tr')
	var tfilas = ArrFilas.length
	for (var c = 0; c < tfilas; c++){
		var filaactual=ArrFilas[c]
		var Celda=filaactual.getElementsByTagName('td')
		
		if(filaactual.id==filamarcada.id){
			filaactual.style.backgroundColor = "#FFFDD2"
			Celda[0].style.color = "blue"
		}
		else {
			filaactual.style.backgroundColor = ""
			Celda[0].style.color = "black"
		}
	}
   
	window.parent.frames[1].location.href="listavariables.asp?idseccion=" + idseccion + "&nombreseccion=" + nombresecc
}
