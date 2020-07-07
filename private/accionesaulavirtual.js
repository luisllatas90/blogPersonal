var valorActual=-1;
var alto=window.screen.Height-90
var ancho=window.screen.Width-20

var PropVentana="width=" + ancho +",height=" + alto +",statusbar=yes,scrollbars=yes,top=0,left=0,resizable=yes,toolbar=no,menubar=no"

function AbrirPopUp(pagina,alto,ancho,ajustable,bestado,barras)
{
   izq = (screen.width-ancho)/2
   arriba= (screen.height-alto)/2

   var ventana=window.open(pagina,"popup","height="+alto+",width="+ancho+",statusbar="+bestado+",scrollbars="+barras+",top=" + arriba +",left=" + izq + ",resizable="+ajustable+",toolbar=no,menubar=no");
   ventana.location.href=pagina
   ventana=null
}

function AbrirPublicidad()
{
   window.open("web/vistaprevia.asp","publicidad","height=500,width=400,statusbar=no,scrollbars=yes,top=50,left=100,resizable=yes,toolbar=no,menubar=no");
}

function SeleccionarTodos(formulario)
{
     var numTotal =0;
     for(i=0;i<document.frmLista.chk.length;i++){
          if(document.frmLista.chk[i].type =="checkbox"){
              document.frmLista.chk[i].checked=true;
               numTotal = numTotal + parseFloat(document.frmLista.chk[i].value)
          }
	}
}


function QuitarTodos(formulario)
{
     for(i=0;i<document.frmLista.chk.length;i++){
          if(document.frmLista.chk[i].type =="checkbox"){
              document.frmLista.chk[i].checked=false;
          }
     }
}

function VerificaSeleccion(formulario)
{
	if (document.frmLista.chkSeleccionar.checked == true)
		{SeleccionarTodos(formulario)}
	else
		{QuitarTodos(formulario)}	
}


function pintafilamarcada(fila,idcheck)
{
   var FilaSeleccionada=fila;
   var Celdas = FilaSeleccionada.getElementsByTagName('td');	
   var Celdas = FilaSeleccionada.cells;
   var ArrCeldas  = Celdas.length;
	for (var c = 0; c < ArrCeldas; c++){
		if(idcheck.checked==true){
			Celdas[c].style.backgroundColor = '#DFEFFF';
		}
		else {
			Celdas[c].style.backgroundColor = ''; // #d6e7ef
		}
	}
   return true;
}

//RESALTAR FILA SELECCIONADA
function Resaltar(op,fila,mostrarhand,colorfila)
{
	if (colorfila=="" || colorfila==undefined){
		colorfila="#FFFFFF"
	}

	if (mostrarhand=="S"){
		fila.style.cursor="hand"
	}
	if(op==1)
		{fila.bgColor="#FBF5D2"}
	else
		{fila.bgColor=colorfila}
}

function marcarColorfila(numfila,colorfila,pagina)
{
	if (colorfila=="" || colorfila==undefined){
		colorfila="#FFFDD2"//"#FFFFFF"
	}
	
	var tbl = document.all.tblMenu
	var ArrFilas = tbl.getElementsByTagName('tr')
	var tfilas = ArrFilas.length
	for (var c = 0; c < tfilas; c++){
		var Fila=ArrFilas[c]
		var Celda=Fila.getElementsByTagName('td')
		
		if(numfila==c){
			Fila.style.backgroundColor = colorfila
			Celda[0].style.color = "blue"
			//Celda[0].style.borderRight="0px"
		}
		else {
			Fila.style.backgroundColor = ""
			Celda[0].style.color = "black"
			//Celda[0].style.borderRight="1px SOLID #808080"
		}
	}
   
   if (pagina!="" || pagina!=undefined){
		window.parent.frames[2].location.href=pagina
	}
}

function actualizarlista(pagina)
{
	window.location.href=pagina
}

function MuestraMenu(Menu)
{
   //var margenderecho=document.body.clientWidth-event.clientX
   //var margeninferior=document.body.clientHeight-event.clientY

	//if (margenderecho<Menu.offsetWidth)
	//	Menu.style.left=document.body.scrollLeft+event.clientX-Menu.offsetWidth
	//else
	//	Menu.style.left=document.body.scrollLeft+event.clientX
	
	//if (margeninferior<Menu.offsetHeight)
	//	Menu.style.top=document.body.scrollTop+event.clientY-Menu.offsetHeight
	//else
	//	Menu.style.top=document.body.scrollTop+event.clientY

 		if(Menu.style.visibility=="hidden")
  			Menu.style.visibility="visible"
		else
			Menu.style.visibility="hidden"
}

function VistaPreviaEvaluacion(pagina)
{
  window.open(pagina,"vistaeval",PropVentana)
}


function AvisoSalida(pagina,variable)
{
	//alert("Esta aplicación se cerrará")
	cerrarSistema()
}

function cerrarSistema()
{
	window.open("../cerrar.asp?Decision=Si","cerrandoSistema","Width=150,height=80,statusbar=no,scrollbars=no,top=100,left=100,resizable=no,toolbar=no,menubar=no")
}


function AbrirActividadesUSAT(pagina,variable)
{
	cvirtual=window.open(pagina,variable,PropVentana)

	if (cvirtual && cvirtual.open && !cvirtual.closed)
		{cvirtual.close}
	else
		{window.open(pagina,variable,PropVentana)}
}

 function SeleccionarDestinatarios()
 {
   var Destinatario=document.frmListaCorreos.ListaPara.length
   var Temporal=""
   for (i=0; i < Destinatario; i++) {
	document.frmListaCorreos.ListaPara.options[i].selected=true
  	} 
 }

 function AgregarItem(select)
 {
    var text = new Array();
	var value = new Array();
	var num = select.options.length;
  	for (var i = num - 1; i >= 0; i--)
		{
    	if (select.options[i].selected)
			{
      		text[text.length] = select.options[i].text;
			value[value.length] = select.options[i].value;
			select.options[i] = null;
			}
		}
  	for (var i = 0; i < text.length; i++)
		{
	  	var opt = new Option(text[i], value[i])
		document.frmListaCorreos.ListaPara.options[document.frmListaCorreos.ListaPara.options.length] = opt;
		}
 }

 function QuitarItem(select)
 {
	var num = select.options.length;
  	var text = new Array();
	var value = new Array();
  	for (var i = num - 1; i >= 0; i--)
		{
    	if (select.options[i].selected)
			{
      		text[text.length] = select.options[i].text;
			value[value.length] = select.options[i].value;
			select.options[i] = null;
			}
		}
  	for (var i = 0; i < text.length; i++)
		{
	  	var opt = new Option(text[i], value[i])
		document.frmListaCorreos.ListaDe.options[document.frmListaCorreos.ListaDe.options.length] = opt;
		}
 }
 
 function validate(form)
 {
 	  if (form.ListaPara.options.length == 0)
		{
			alert("Debe seleccionar algún miembro de la lista nombres.");
			return false;
		}
		SeleccionarDestinatarios()
		return true;
 }

function MostrarTabla(idtabla,ruta,img)
{
	if(idtabla.style.display=="none"){
		idtabla.style.display=''
		img.src=ruta + "menos.gif"
		}
	else{
		idtabla.style.display="none"
		img.src=ruta + "mas.gif"
		}
}

function AbrirTodasTablas(ruta,img,tablaexcluir)
{
   var objTabla = document.getElementsByTagName('table');
   var ArrTablas  = objTabla.length;
      
	if (ArrTablas==undefined){
		objTabla.style.display=''
	}
	else{
		for (var i=0;i<ArrTablas;i++){
			var idtabla=objTabla[i]
			if (idtabla.id!=tablaexcluir.id){
				idtabla.style.display=''
			}
		}
	}
	cambiarimagenarbol("m",ruta)
}

function ResaltarBoton(Boton)
{
	var NumImagen=document.images.length
	var q=eval(Boton)
   	for (i=0; i < NumImagen; i++)
   		{if (i>0)
   			{if (i != q)
				//Desactivar el resto de botones
				{document.images[i].src="../images/off0" + i + '.jpg'}
			else
				//Activar el botón picado
				{document.images[q].src="../images/on0" + i + '.jpg'}
		  	}
		}
}

function cambiarimagenarbol(modo,ruta)
{
	var NumImagen=document.images.length
   	for (i=0; i < NumImagen; i++){
		if (i>0){
		    var encontrar=document.images[i].src
			if (modo=="m"){
				encontrar=encontrar.match("mas.gif")
					if (encontrar=="mas.gif" && modo=="m")
					   {document.images[i].src=ruta + "menos.gif"}
			}
			else{
				encontrar=encontrar.match("menos.gif")
				if (encontrar=="menos.gif" && modo=="n")
				   {document.images[i].src=ruta + "mas.gif"}

			}
		}
	}
}

function ResaltarControl(control)
{
	control.style.backgroundColor='#FEFFE1'
}

function ConvertirMayuscula(control)
{
	control.value=control.value.toUpperCase()
}

function ResaltarConvertirMayusc(control)
{
	control.style.backgroundColor="white"
	control.value=control.value.toUpperCase()
}
