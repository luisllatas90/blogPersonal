//Exclusiva para mostrar la lista de carpetas en viñetas
function ResaltarCarpeta(fila,colorfila)
{
	var idDoc=fila.id
	idDoc=idDoc.substr(3,20)

	//Quitar el resto de carpetas resaltadas
	var ArrSpan = document.getElementsByTagName("span")

	for (var i=0;i<ArrSpan.length;i++){
		var spanActual=ArrSpan[i]
		if (spanActual.id==fila.id){
			//Resaltar y pasar el valor a la caja de texto
			ResaltarVineta("1",fila,colorfila)
			txtCarpetaElegida.value=idDoc
		}
		else{
			//Quitar carpeta resaltadas
			ResaltarVineta("0",spanActual,colorfila)
		}	
	}
}

function ElegirDocumento(fila)
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

function ResaltarArchivo(numfila,iddoc)
{
	var tbl = document.all.tbllistadocumentos
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
	
	txtelegido.value=iddoc
	mensajedetalledoc.style.display="none"
	fradetalle.location.href="detalledocumento.asp?iddocumento=" + iddoc + "&numfila=" + numfila
}

function OcultarCarpetas(img)
{
	if(parent.tdcarpeta1.style.display=="none"){
		parent.tdcarpeta1.style.display=""
		parent.tdcarpeta2.style.display=""
		parent.tdcarpeta3.style.display=""
		img.src="../../../images/maximiza.gif"
	}
	else{
		parent.tdcarpeta1.style.display="none"
		parent.tdcarpeta2.style.display="none"
		parent.tdcarpeta3.style.display="none"
		img.src="../../../images/minimiza.gif"
	}
}

/* Enviar los datos de la carpeta para mostrar sus archivos*/
function MostrarArchivos(iddoc,escrit,titcarpeta,tipopublic,creador,tipofuncion,usuarioactual)
{
	var imgElegido=document.getElementById("imgCarpeta" + iddoc)
	var spElegido=document.getElementById("spCarpeta" + iddoc)
	var ArrImg=document.all.arrImgCarpetas
	var ArrSpan = document.getElementsByTagName("span")
	
	//Ocultar submenu desplegable
	OcultarMenuContextual(parent.MenuDoc)

	//Almacenar en un control hidden el IDCARPETA
	parent.txtidcarpeta.value=iddoc
	parent.txttitulocarpeta.value=titcarpeta

	//Abrir lista de archivos de la carpeta
	parent.fralista.location.href="listadocumentos.asp?IdCarpeta=" + iddoc
	
	//Mostrar los menús respectivos, dependiendo del permiso a la carpeta
	RestringirEscritura(creador,escrit,usuarioactual)
	if (ArrImg.length==undefined){
		imgElegido.src="../../../images/abierto.GIF"
		ResaltarVineta("1",spElegido,"#EBE1BF")
	}
	else{
		//Cambiar imagen y resaltar la Carpeta Abierta
   		for (i=0;i<ArrImg.length;i++){
			var imgActual=ArrImg[i]
			var spActual=ArrSpan[i]

   			if (imgActual.id==imgElegido.id){
				imgElegido.src="../../../images/abierto.GIF"
				ResaltarVineta("1",spElegido,"#EBE1BF")
			}
			else{
				imgActual.src="../../../images/cerrado.GIF"	
				ResaltarVineta("0",spActual,"#EBE1BF")
			}
		}
	}
}

/* Enviar los datos de la carpeta para mostrar sus archivos*/
function MostrarVersionesDoc(iddoc,idver,escrit,creador)
{
	var imgElegido=document.getElementById("imgCarpeta" + idver)
	var spElegido=document.getElementById("spCarpeta" + idver)
	var ArrImg=document.all.arrImgCarpetas
	var ArrSpan = document.getElementsByTagName("span")
	
	//Abrir lista de archivos de la carpeta
	mensajedetalledoc.style.display="none"
	parent.fradetalleversion.location.href="detalleversion.asp?iddocumento=" + iddoc + "&idversion=" + idver
	
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

function ResaltarCarpetaMarcada(tipofuncion,usuarioactual)
{
   var iddoc=parent.txtidcarpeta.value
   if (iddoc!=""){
	var imgElegido=document.getElementById("imgCarpeta" + iddoc)
	var spElegido=document.getElementById("spCarpeta" + iddoc)
	var ArrImg=document.all.arrImgCarpetas
	var ArrSpan = document.getElementsByTagName("span")
	
	//Ocultar submenu desplegable
	OcultarMenuContextual(parent.MenuDoc)

	if (imgElegido!=null){
		//Cambiar imagen y resaltar la Carpeta Abierta
   		for (i=0;i<ArrImg.length;i++){
			var imgActual=ArrImg[i]
			var spActual=ArrSpan[i]

	   		if (imgActual.id==imgElegido.id){
				//Resaltar carpeta picada
				imgElegido.src="../../../images/abierto.GIF"
				ResaltarVineta("1",spElegido,"#EBE1BF")
			}
			else{
				//cerrar carpetas y quitar resalte
				imgActual.src="../../../images/cerrado.GIF"
				ResaltarVineta("0",spActual,"#EBE1BF")
			}
		}
	}
   }
}

/*----------------------------------------------------------------------------------------------------------
Rutinas para activar el menú derecho de la página Carpetas
------------------------------------------------------------------------------------------------------------*/

function CrearMenuPopUp(tbl,escrit,titcarpeta,tipopublic,nombrecreador,creador,tipofuncion,usuarioactual)
{
	var iddoc=tbl.id
	iddoc=iddoc.substr(3,20)

	OcultarMenuContextual(parent.MenuDoc)
	if (!event.ctrlKey){
		if(creador==usuarioactual){
			//Verifica si se esta haciendo clic derecho en otra carpeta para actualizar la webpage
			if (parent.txtidcarpeta.value!=iddoc)
				{MostrarArchivos(iddoc,escrit,titcarpeta,tipopublic,creador,tipofuncion,usuarioactual)}
			MostrarMenuPopUp(tbl);
			return false;
		}
		else{
			if (parent.txtidcarpeta.value!=iddoc)
				{MostrarArchivos(iddoc,escrit,titcarpeta,tipopublic,creador,tipofuncion,usuarioactual)}
			alert("La carpeta está BLOQUEADA para administrarla\nRegistrada por " + nombrecreador)
		}
	}
}

function MostrarMenuPopUp(tbl)
{
   var PosY=tbl.offsetTop
   var Alto=tbl.offsetHeight

   fila=tbl//event.srcElement;
   Alto=Alto + PosY

   MenuDir.style.top=Alto
   MenuDir.style.left=event.clientX

   //MenuDir.style.leftPos+=10;
   //MenuDir.style.posLeft=event.clientX;
   //MenuDir.style.posTop=event.clientY;
   MenuDir.style.display="";
   MenuDir.setCapture();
}


function OcultarMenuPopUp()
{
	MenuDir.style.display="none"
}

function OcultarBotonAgregar()
{
	parent.cmdAgregar.style.display="none"
}

function RestringirEscritura(creador,tipo,usuarioactual)
{
   if (parent.cmdAgregar!=undefined){
		if (creador==usuarioactual)
			{parent.cmdAgregar.style.display=""}
		else{
			if (tipo=="1"){
				parent.cmdAgregar.style.display=""
			}
			else{
				parent.cmdAgregar.style.display="none"
			}
		}
   }
}

function ActivarMenuPopUp()
{
   MenuDir.releaseCapture();
   MenuDir.style.display="none";
   var Opt=event.srcElement;
   var iddoc=fila.id
   iddoc=iddoc.substr(3,20)

	switch(Opt.id)
	{
		case "mnuAgregar":
			AbrirPopUp("frmdocumento.asp?tipodoc=C&accion=agregarcarpeta&idcarpeta=" + iddoc,"300","600")
			//showModalDialog("frmdocumento.asp?tipodoc=C&accion=agregarcarpeta",window,"dialogWidth:350px;dialogHeight:200px;status:no;help:no;center:yes");
			break

		case "mnuModificar":
			AbrirPopUp("frmdocumento.asp?tipodoc=C&accion=modificarcarpeta&iddocumento=" + iddoc,"300","600")
			//showModalDialog("frmcarpeta.asp?accion=modificarcarpeta",window,"dialogWidth:350px;dialogHeight:200px;status:no;help:no;center:yes");
			break

		case "mnuEliminar":
			var pagina="procesar.asp?accion=eliminarcarpeta&iddocumento=" + iddoc
			var mensaje="La acción a realizar es irreversible.\n¿Está completamente seguro que desea Eliminar la carpeta con todo su contenido?"
			Eliminar(mensaje,pagina)
			break

		case "mnuMover":
			var iddocorigen=iddoc
			var iddocdestino=null
			showModalDialog("frmmovercarpeta.asp?idcarpeta=" + iddoc,window,"dialogWidth:380px;dialogHeight:400px;status:no;help:no;center:yes");
			break

		case "mnuPermisos":
			var ruta="../usuario/frmcompartirrecurso.asp"			
			var titulodoc=parent.txttitulocarpeta.value
			var descripcion=""
			
			AbrirPopUp(ruta + "?titulo=Documento: " + titulodoc + "&idtabla=" + iddoc + "&nombretabla=documento&descripcion=" + descripcion + "&tipodoc=C","500","600")
			break

	}
}

/*----------------------------------------------------------------------------------------------------------*/
//Procesos de acción con los documentos AGREGAR/MODIFICAR/ELIMINAR
function AgregarCarpeta()
{
	ResaltarVineta("1",spCarpeta0,"#EBE1BF")
	AbrirPopUp('frmdocumento.asp?tipodoc=C&accion=agregarcarpeta&idcarpeta=0','300','600')
}
function MoverCarpeta(idorigen)
{
	var iddestino=txtCarpetaElegida.value
	var Argumentos = window.dialogArguments;
		Argumentos.iddocorigen=idorigen
	   	Argumentos.iddocdestino=iddestino
   		Argumentos.ProcesarMovimientoCarpeta()
   		window.close()
}

function ProcesarMovimientoCarpeta()
{
	location.href="procesar.asp?accion=moverdocumento&tipodoc=C&iddocumento=" + iddocorigen + "&idcarpeta=" + iddocdestino
}
function AgregarDoc(modo)
{
	var iddir=txtidcarpeta.value
	OcultarMenuContextual(MenuDoc)

	if (iddir==""){
		alert("Elija la carpeta dónde desea publicar el documento")
	}
	else{
		if (modo=="A"){
			AbrirPopUp("frmdocumento.asp?Accion=agregardocumento&tipodoc=A&idcarpeta=" + iddir,"350","620")}

		if (modo=="L")
			{AbrirPopUp("frmdocumento.asp?Accion=agregardocumento&tipodoc=L&idcarpeta=" + iddir,"350","620")}
	}
}

function ModificarDoc(iddir,iddoc,tipodoc,nfila)
{
	AbrirPopUp("frmdocumento.asp?Accion=modificardocumento&idcarpeta=" + iddir + "&tipodoc=" + tipodoc + "&IdDocumento=" + iddoc + "&numfila=" + nfila,"300","620")
}

function EliminarDoc(iddoc,arch)
{
	var pagina="procesar.asp?accion=eliminardocumento&tipodoc=A&iddocumento=" + iddoc + "&archivo=" + arch
	Eliminar("¿Está seguro completamente seguro que desea Eliminar el documento seleccionado?",pagina)
}

function PermisosDoc(iddoc)
{
	var titulodoc=txttitulodocumento.value
	var ruta="../usuario/frmcompartirrecurso.asp"
	var descripcion=""
	AbrirPopUp(ruta + "?titulo=Documento: " + titulodoc + "&idtabla=" + iddoc + "&nombretabla=documento&descripcion=" + descripcion,"500","620")
}


function VersionDoc(iddoc)
{
	var titulodoc=txttitulodocumento.value	
	AbrirPopUp("listaversiones.asp?IdDocumento=" + iddoc + "&titulodocumento=" + titulodoc,"530","620","yes")
}

function AgregarVersion(iddoc,idver)
{
	location.href="frmversion.asp?iddocumento=" + iddoc + "&idversion=" + idver
}

function ModificarVersion(iddoc,idver)
{
	location.href="detalleversion.asp?modo=modificarversion&iddocumento=" + iddoc + "&idversion=" + idver
}

function EliminarVersion(iddoc,idver,arch)
{
	var pagina="procesar.asp?accion=eliminarversion&iddocumento=" + iddoc + "&idversion=" + idver + "&archivo=" + arch
	var mensaje="¿Está seguro completamente seguro que desea Eliminar la versión del documento\n\n Recuerde que si el documento tiene documentos dependientes, no podrá eliminarlo?"
	Eliminar(mensaje,pagina)
}

function GuardarVersion(iddoc,idver)
{
	var tituloversion=document.all.tituloversion.value
	var publica=0
	var bloqueada=0

	if (tituloversion.length<3){
		alert("Especifique el título de la versión del documento")
		tituloversion.focus()
		return(false)
	}
	else{
		if (document.all.publica.checked==true)
			{publica=1}
		if (document.all.bloqueada.checked==true)
			{bloqueada=1}
		location.href="procesar.asp?accion=modificarversion&iddocumento=" + iddoc + "&idversion=" + idver  + "&tituloversion=" + tituloversion + "&publica=" + publica + "&bloqueada=" + bloqueada
	}
}

function ModoVersion(iddoc,idver)
{
	location.href="detalleversion.asp?iddocumento=" + iddoc + "&idversion=" + idver
}

//Actualizar los datos de la carpeta, llamada por una ventana Modal
function ActualizarCarpetas()
{
	var Argumentos = window.dialogArguments;
//	Argumentos.termino=terminobusqueda
//   	Argumentos.campo=campobusqueda.value
	Argumentos.CargarListaDocumentos();

	window.close();
}

function CargarListaDocumentos()
{
	document.fracarpetas.location.href="carpetas.asp?veces=1"
}

//Validar datos de ingreso del documento
function validardocumento(formulario,modo,tipodoc,tipofuncion)
{
  var estado=true

  if (formulario.titulodocumento.value == "")
  {
    alert("Ingrese el título del documento.");
    formulario.titulodocumento.focus();
    return (false);
  }

  if (tipodoc=="A" && modo=="agregardocumento"){
	  if (formulario.file.value == ""){
	  	alert("Haga click en el botón EXAMINAR, para determinar la ubicación local del archivo");
		formulario.file.focus();
		return (false);
	   }
  }

  if(tipofuncion>1){
	estado=CompararFechas(formulario.fechainicio.value,formulario.fechafin.value)
	  if (estado==false)
	     {return (false)}
  }

  DesactivarControlesfrm(formulario)
  return (true);
}

function validarversiondoc(formulario)
{
  if (formulario.tituloversion.value == "")
  {
    alert("Ingrese el título de la versión del documento.");
    formulario.tituloversion.focus();
    return (false);
  }

  if (formulario.file.value == ""){
  	alert("Haga click en el botón EXAMINAR, para determinar la ubicación local del archivo");
	formulario.file.focus();
	return (false);
   }
  DesactivarControlesfrm(formulario)
  return (true);
}

function validarlink(formulario)
{

  if (formulario.tituloLink.value == "")
  {
    alert("Ingrese el título del enlace web.");
    formulario.tituloLink.focus();
    return (false);
  }

  if (formulario.URL.value == "")
  {
    alert("Ingrese la dirección web del enlace.");
    formulario.URL.focus();
    return (false);
  }

  return (true);
}

function EliminarLink(iddoc,idlink)
{
	var pagina="link.asp?modalidad=Eliminar&idDocumento=" + iddoc + "&IdLink=" + idlink
	var mensaje="¿Está seguro completamente seguro que desea Eliminar el link seleccionado?"
	Eliminar(mensaje,pagina)
}
