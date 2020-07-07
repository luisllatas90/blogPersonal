function ResaltarTarea(numfila,idtarea)
{
	var tbl = document.all.tbllistatareas
	var ArrFilas = tbl.getElementsByTagName('tr')
	var tfilas = ArrFilas.length
	for (var c = 0; c < tfilas; c++){
		var Fila=ArrFilas[c]
		var Celda=Fila.getElementsByTagName('td')
		
		if(numfila==c){
			Fila.style.backgroundColor = "#395ACC"
			Celda[0].style.color = "white"
			Celda[1].style.color = "white"
			Celda[2].style.color = "white"
		}
		else {
			Fila.style.backgroundColor = ""
			Celda[0].style.color = "black"
			Celda[1].style.color = "black"
			Celda[2].style.color = "black"
		}
	}
	txtelegido.value=idtarea
	mensajedetalletarea.style.display="none"
	fradetalle.location.href="detalletarea.asp?idtarea=" + idtarea + "&numfila=" + numfila
}

/* Enviar los datos de la carpeta para mostrar sus archivos*/
function MostrarVersionesDoc(idtarea,idtareausuario,escrit,creador)
{
	var imgElegido=document.getElementById("imgCarpeta" + idtareausuario)
	var spElegido=document.getElementById("spCarpeta" + idtareausuario)
	var ArrImg=document.all.arrImgCarpetas
	var ArrSpan = document.getElementsByTagName("span")
	
	//Abrir lista de archivos de la carpeta
	mensajedetalledoc.style.display="none"
	fradetalleversion.location.href="detalleversion.asp?idtarea=" + idtarea + "&idtareausuario=" + idtareausuario
	
	//Cambiar imagen y resaltar la Carpeta Abierta
   	for (i=0;i<ArrImg.length;i++){
		var imgActual=ArrImg[i]
		var spActual=ArrSpan[i]

   		if (imgActual.id==imgElegido.id){
			//Resaltar documento picado
			ResaltarVineta("1",spElegido,"")
		}
		else{
			//Quitar resalte al resto de documentos
			ResaltarVineta("0",spActual,"")
		}
	}
}

function ElegirEncuesta(fila)
{
	var idDoc=fila.id
	idDoc=idDoc.substr(3,20)
	var spElegido=document.getElementById("doc" + idDoc)

	//Quitar el resto de carpetas resaltadas
	var ArrSpan = document.getElementsByTagName("span")

	for (var i=0;i<ArrSpan.length;i++){
		//Extraer nombre de chk quitando los digitos doc/chk
		var spanActual=ArrSpan[i]
		var nchk=spanActual.id
		var nchk=nchk.substr(3,20)
		var chkActual=document.getElementById("chk" + nchk)

		if (chkActual!=null){
		  if (fila.checked==true){
			if (spanActual.id==spElegido.id){
				//Resaltar y pasar el valor a la caja de texto
				ResaltarVineta("1",spElegido,"")
			}
		  }
		  else{
			if (chkActual.checked==false){
				//Quitar carpeta resaltadas
				ResaltarVineta("0",spanActual,"")
			}
		  }	
		}
	}
}

function AbrirTarea(modo,numfila,pagina)
{
   var idtarea=txtelegido.value
	switch(modo)
	{
		case "A":
			AbrirPopUp("frmtarea.asp?accion=agregartarea","350","620")
			break
	}
}

function IntegrarRecurso(modo,idtarea,idtarearec)
{
	switch(modo) 
	{
		case "A":
			var ctrl=cbxrecurso.value
			if (ctrl!="")
				{location.href=ctrl + "?idtipopublicacion=0&idtarea=" + idtarea}
			break

	}
}

function ActivarProp()
{
	switch(document.all.idtipotarea.value)
	{
		case "4"://Subir archivos
			trintegrar.style.display="none"
			trpermitirreenvio.style.display=""
			break
		case "5"://Descargar y subir archivos
			trintegrar.style.display="none"
			trpermitirreenvio.style.display=""
			break
		case "6"://otra actividad
			trintegrar.style.display=""
			trpermitirreenvio.style.display="none"
			break
		default:
			trintegrar.style.display="none"
			trpermitirreenvio.style.display="none"
			break
	}
	document.all.cbxrecurso.selectedIndex=0
}

//Validar datos de ingreso del documento
function validartarea(formulario)
{
  var estado=true

  if (formulario.titulotarea.value == "")
  {
    alert("Ingrese el título de la tarea.");
    formulario.titulotarea.focus();
    return (false);
  }

  if (formulario.descripcion.value == "")
  {
    alert("Especifique en que consiste la tarea que va a asignar.");
    formulario.descripcion.focus();
    return (false);
  }

  estado=CompararFechas(formulario.fechainicio.value,formulario.fechafin.value)
  if (estado==false)
     {return (false)}

  DesactivarControlesfrm(formulario)
  return (true);
}

function validarenviotarea(formulario)
{
  DesactivarControlesfrm(formulario)
  return (true);
}


function Realizartarea(modo,estado,idt,idtr,arch,det)
{
   var estadoactual=estado.value

   if(estadoactual==0){
	switch (modo){
		case "D"://documentos
			mensaje="¿Está completamente seguro que desea registrar la tarea asignada como realizada?"
			if (confirm(mensaje)==true){
				estado.value=1
				det.innerHTML="<font color='#0000FF'>Realizada</font>"
				pagina="procesar.asp?accion=revisardocumento&idtarea=" + idt + "&idtarearecurso=" + idtr + "&archivo=" + arch
				AbrirPopUp(pagina,"550","650","yes","yes","yes")
			}
			break

		case "E"://encuestas
			alert("mantenimiento")
			break

		case "A"://Enviar tarea simple
			//AbrirPopUp("frmenviararchivo.asp?idtarea="+ idt,"200","550")
			AbrirPopUp("generacadena.asp?pagina=frmenviararchivo.aspx&idtarea="+ idt,"300","550")
			break
	}
   }
   else{
	alert("La tarea asignada, ya ha sido realizada por Ud.")
   }
}

function RegresarAListaTareas(idt,tt)
{
	location.href="listatareausuario.asp?idtarea=" + idt + "&titulotarea=" + tt
}


function AbrirVersionTarea(modo,idtarea,idtareausuario,arch)
{
	switch (modo){
		case "A":
			location.href="frmenviararchivo.asp?idtarea=" + idtarea + "&idtareausuario=" + idtareausuario
			break

		case "R":
			location.href="detalleversion.asp?idtarea=" + idtarea + "&idtareausuario=" + idtareausuario
			break
	}
}