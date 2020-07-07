//Exclusiva para mostrar la lista de carpetas en viñetas
function Resaltarcontenido(fila)
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

/* Enviar los datos del índice*/
function MostrarContenido(tipo,bloqindice,idcont,titcontenido)
{
	var imgElegido=document.getElementById("imgcontenido" + idcont)
	var spElegido=document.getElementById("spcontenido" + idcont)
	var ArrImg=document.all.arrImgcontenidos
	var ArrSpan = document.getElementsByTagName("span")
	var imgabierto="libroabierto.GIF"
	var imgcerrado="librocerrado.GIF"
	
	//Almacenar en un control hidden el IDcontenido
	txtidcontenido.value=idcont
	txttitulocontenido.value=titcontenido

	/*Cambiar imagen y resaltar la contenido Abierta*/
	if (ArrImg.length==undefined){
		imgElegido.src="../../../images/" + imgabierto
		ResaltarVineta("1",spElegido,"")
	}
	else{
   	  for (i=0;i<ArrImg.length;i++){
		var imgActual=ArrImg[i]
		var spActual=ArrSpan[i]

   		if (imgActual.id==imgElegido.id){
			if (imgElegido.tipocont=="C")
				{imgabierto="librohoja.GIF"}
			
			imgElegido.src="../../../images/" + imgabierto
			ResaltarVineta("1",spElegido,"")
		}
		else{			
			if (imgActual.tipocont=="C")
				{imgcerrado="librohoja.GIF"}
			else
				{imgcerrado="librocerrado.GIF"}
			imgActual.src="../../../images/" + imgcerrado
			ResaltarVineta("0",spActual,"")
		}
	  }
	}
}

/*----------------------------------------------------------------------------------------------------------
Rutinas para activar el menú derecho del índice
------------------------------------------------------------------------------------------------------------*/

function CrearMenuPopUp(tbl,modo)
{
	if (!event.ctrlKey){
		MostrarMenuPopUp(tbl);
		return false;
	}
}

function MostrarMenuPopUp(tbl)
{
   var PosY=tbl.offsetTop
   var Alto=tbl.offsetHeight

   fila=tbl//event.srcElement;
   Alto=Alto + PosY

   MenuDir.style.top=Alto + 30
   MenuDir.style.left=event.clientX

   MenuDir.style.display="";
   MenuDir.setCapture();
}


function OcultarMenuPopUp()
{
	MenuDir.style.display="none"
}


function ActivarMenuPopUp()
{
   MenuDir.releaseCapture();
   MenuDir.style.display="none";
   var Opt=event.srcElement;
   var idlibro=txtidlibrodigital.value
   var titulolibro=txttitulolibro.value
   var idcont=fila.id
   idcont=idcont.substr(3,20)

	switch(Opt.id)
	{
		case "mnuAgregar":
			AbrirPopUp("frmindice.asp?accion=agregarindice&idindice=" + idcont + "&idlibrodigital=" + idlibro,"280","600")
			break

		case "mnuModificar":
			AbrirPopUp("frmindice.asp?accion=modificarindice&idcontenido=" + idcont,"280","600")
			break

		case "mnuEliminar":
			var pagina="procesar.asp?accion=eliminarindice&idcontenido=" + idcont + "&idlibrodigital=" + idlibro + "&titulolibro=" + titulolibro
			Eliminar("La acción a realizar es irreversible\n¿Está seguro completamente seguro que desea Eliminar el Contenido temático \ny todas sus dependencias incluyendo visitas registras al recurso?",pagina)
			break

		case "mnuContenido":
			AbrirPopUp("frmcontenido.asp?accion=modificarcontenidoweb&idcontenido=" + idcont,"450","700","yes")
			break
	}
}


//Procesos de acción con los documentos AGREGAR/MODIFICAR/ELIMINAR

function AbrirContenido(modo,idlibro)
{
	var idcont=txtidcontenido.value
	var titulolibro=txttitulolibro.value

	switch(modo)
	{
		case 'A':
			ResaltarVineta("1",spcontenido0,"")
			AbrirPopUp("frmindice.asp?accion=agregarindice&idindice=0&idlibrodigital=" + idlibro,"280","600")
			break	
		case 'V': //Vista previa del contenido
			location.href="detallecontenido.asp?idcontenido=" + idcont + "&idlibrodigital=" + idlibro + "&titulolibro=" + titulolibro
			break
		case 'R':
			AbrirPopUp("frmrecopilar.asp?idlibrodigital=" + idlibro,"500","600","yes")
			break
	}
}

function AbrirLibrodigital(modo,id)
{
  if (modo!='A'){
   var FilaSeleccionada=document.getElementById("fila" + id);
   var Celdas = FilaSeleccionada.getElementsByTagName('td');
   var Celdas = FilaSeleccionada.cells;
   var titulolibro=Celdas[1].innerText
   }

	switch(modo)
	{
		case 'A':
			AbrirPopUp("frmlibrodigital.asp?accion=agregarlibrodigital","280","600")
			break
		case 'M':
			AbrirPopUp("frmlibrodigital.asp?accion=modificarlibrodigital&idlibrodigital=" + id,"280","600")
			break
		case 'E':
			var pagina="procesar.asp?accion=eliminarlibrodigital&idlibrodigital=" + id
			Eliminar("La acción a realizar es irreversible\n¿Está seguro completamente seguro que desea Eliminar el contenido digital\ny todas sus dependencias incluyendo visitas registras al recurso?",pagina)
			break
		case 'P':
			var ruta="../usuario/frmcompartirrecurso.asp"
			var titulodoc=""//parent.txttitulocarpeta.value
			var descripcion=""
			
			AbrirPopUp(ruta + "?titulo=Contenido digital: " + titulodoc + "&idtabla=" + id + "&nombretabla=librodigital&descripcion=" + descripcion,"500","600")
			break
		case 'C':
			location.href="listaindice.asp?modo=administrar&idlibrodigital=" + id + "&titulolibro=" + titulolibro
			break
		case 'V':
			location.href="listaindice.asp?modo=visualizar&idlibrodigital=" + id + "&titulolibro=" + titulolibro
			break
		case 'B':
			alert("No se ha registrado ningún contenido temático.\nPor favor consulte con el Administrador el Curso o Evento Académico")
			break
			
	}
}


function AbrirGlosario(modo,id,letra,accion)
{
	switch(modo)
	{
		case 'A':
			fradetalleglosario.location.href="frmglosario.asp?accion=agregarglosario&idlibrodigital=" + id
			break
		case 'M':
			location.href="frmglosario.asp?accion=modificarglosario&idglosario=" + id
			break
		case 'E':
			var pagina="procesar.asp?accion=eliminarglosario&idglosario=" + id
			Eliminar("La acción a realizar es irreversible\n¿Está seguro completamente seguro que desea Eliminar el término del glosario?",pagina)
			break
		case 'L':
			fradetalleglosario.location.href='detalleglosario.asp?idlibrodigital=' + id + "&letra=" + letra + "&modo=" + accion
			break
		case 'B':
			var termino 	= null;
	   		var campo	= null;
			showModalDialog("frmbuscarglosario.asp?idlibrodigital=" + id + "&modo=" + accion,window,"dialogWidth:350px;dialogHeight:180px;status:no;help:no;center:yes");
			break
		case 'V':
			AbrirPopUp("glosario.asp?modo=" + accion + "&idlibrodigital=" + id,"500","780","yes")
			break
	
	}
}

function EvaluarCriteriosbusquedaGlosario(idlibro,modo)
{
   	var controlbusqueda=document.all.texto
   	var terminobusqueda=controlbusqueda.value
   	var campobusqueda=document.all.cbxcampo
	
	if (terminobusqueda.length<3 && campobusqueda.selectedIndex!=2){
		alert("Por favor escriba el término de búsqueda")
		controlbusqueda.value=""
		controlbusqueda.focus()
		return(false)
	}
 	else{
		var Argumentos = window.dialogArguments;
	   	Argumentos.termino=terminobusqueda
   		Argumentos.campo=campobusqueda.value
	   	Argumentos.RecuperarCondicionBusqueda(idlibro,modo);
		window.close();
	}
}

function desactivarCamposBusqueda()
{
	if (document.all.cbxcampo.selectedIndex==2){
		document.all.texto.disabled=true
	}
	else{
		document.all.texto.disabled=false
		document.all.texto.focus()
	}
}


function RecuperarCondicionBusqueda(idlibro,modo)
{
	document.fradetalleglosario.location.href="detalleglosario.asp?modalidad=busqueda&termino=" + termino + "&campo=" + campo + "&idlibrodigital=" + idlibro + "&modo=" + modo
}


//Validar datos de ingreso del documento
function validarlibrodigital(formulario)
{
  var estado=true

  if (formulario.titulolibrodigital.value == "")
  {
    alert("Ingrese el título del contenido digital.");
    formulario.titulolibrodigital.focus();
    return (false);
  }

  estado=CompararFechas(formulario.fechainicio.value,formulario.fechafin.value)
  if (estado==false)
     {return (false)}

  DesactivarControlesfrm(formulario)
  return (true);
}


//Validar datos de ingreso del documento
function validarindice(formulario)
{
  var estado=true

  if (formulario.ordencontenido.value == "")
  {
    alert("Ingrese el orden en el que se mostrará el índice.");
    formulario.ordencontenido.focus();
    return (false);
  }

  if (formulario.titulocontenido.value == "")
  {
    alert("Ingrese el título del índice.");
    formulario.titulocontenido.focus();
    return (false);
  }


  estado=CompararFechas(formulario.fechainicio.value,formulario.fechafin.value)
  if (estado==false)
     {return (false)}

  DesactivarControlesfrm(formulario)
  return (true);
}

function validarrecopilacion(formulario)
{
  var numTotal=formulario.chkidcontenido.length;
  var total=0

  for(i=0;i<numTotal;i++){
	  if(formulario.chkidcontenido[i].type =="checkbox"){
		if (formulario.chkidcontenido[i].checked==true)
			{total+=total+1}
          }
  }

  if (total==0){
	alert("Debe seleccionar al menos un item del Contenido temático")
	return(false)
  }
  return(true)  
}

function validarglosario(formulario)
{
if (formulario.tituloglosario.value == "")
  {
    alert("Ingrese el término del glosario.");
    formulario.tituloglosario.focus();
    return (false);
  }

  if (formulario.descripcion.value == "")
  {
    alert("Ingrese la descripción del término del glosario.");
    formulario.descripcion.focus();
    return (false);
  }
  return(true)
}
