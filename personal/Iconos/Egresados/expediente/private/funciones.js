var alto=window.screen.Height-90
var ancho=window.screen.Width-20
var AnteriorFila=0;
var UltimaFila=0;

var PropVentana="width=" + ancho +",height=" + alto +",statusbar=yes,scrollbars=yes,top=0,left=0,resizable=yes,toolbar=no,menubar=no"


//desactiva bot�n derecho del men�

//document.oncontextmenu="return false"

function AbrirPopUp(pagina,alto,ancho,ajustable,bestado,barras,variable)
{
   var izq = (screen.width-ancho)/2
   var arriba= (screen.height-alto)/2
   if (variable=="" || variable==undefined)
	{variable="popup"}

   var ventana=window.open(pagina,variable,"height="+alto+",width="+ancho+",statusbar="+bestado+",scrollbars="+barras+",top=" + arriba + ",left=" + izq + ",resizable="+ajustable+",toolbar=no,menubar=no");
   ventana.location.href=pagina
   //alert (izq + "-" + arriba)
   ventana=null
}

function AbrirMDIChild(pagina)
{
	fraMDIchild.location.href=pagina	
}

function AbrirVentanaMax(pagina)
{
	var prop="width=" + ancho +",height=" + alto +",statusbar=yes,scrollbars=no,top=0,left=0,resizable=yes,toolbar=no,menubar=no"
	var ventana=window.open(pagina,"PagMax",prop)
	ventana.location.href=pagina
	ventana=null
}

function AbrirAlumno(frm)
{
	var prop="width=" + ancho +",height=" + alto +",statusbar=yes,scrollbars=no,top=0,left=0,resizable=yes,toolbar=no,menubar=no"
	if (!window.focus){return true}

	window.open("otro/cargando.htm","PagAlumno",prop)

	frm.action="otro/acceder.asp"
	frm.target="PagAlumno"
	frm.submit()
	return true;
}

function AbrirAyuda(pagina)
{
	var prop="width=" + ancho +",height=" + alto +",statusbar=yes,scrollbars=yes,top=0,left=0,resizable=yes,toolbar=no,menubar=no"
	var ventana=window.open(pagina,"Ayuda",prop)
	ventana.location.href=pagina
	ventana=null
}

function AbrirMensaje(ruta)
{
    var ventana = window.createPopup();
    var contenido = ventana.document.body;
    contenido.style.backgroundColor = "#FFFFCC";
    contenido.style.border = "solid black 2px";
    contenido.innerHTML = "<center><h3>Cargando</h3><p><img border='0' src='" + ruta + "loading.gif'></p><p><font color='#800000' face='Verdana' style='font-size: 8pt'>Espere un momento por favor...</font></p>"
    ventana.show(150,150,250,100, document.body);
    setTimeout("window.close()", 1000)
}

function convertirEnterTab()
{
	if (event.keyCode==13)
		{event.keyCode=9}
}

function validarnumero()
{
	if (event.keyCode < 45 || event.keyCode > 57)
		{event.returnValue = false}
}

function validarnota(ctrl)
{
	if(ctrl.value>20){
		alert("La nota no debe ser mayor a 20")
		ctrl.focus()
		ctrl.value=0
		return(false)		
	}
}

function DesactivarControlesfrm(frm)
{
	/*
	var numctrls=frm.elements.length
	if (numctrls!=undefined){
		for(var i=0;i<numctrls;i++){
			var Control=frm.elements[i]
			Control.disabled=true
		}
	}
	*/
	var guardar=frm.cmdGuardar
	var cancelar=frm.cmdCancelar

	if (guardar!=undefined){
		guardar.disabled=true
		cancelar.disabled=true
	}
	mensaje.innerHTML="<b>&nbsp;Espere un momento por favor...</b>"
}



function ConfirmarEliminar(mensaje,pagina)
{
	var Confirmar=confirm(mensaje);
	if(Confirmar==true)
		{document.location.href=pagina + "&modalidad=Eliminar"}
	else
		{return false;}
}

function SeleccionarFila()
{
	oRow = window.event.srcElement.parentElement;
		
	if (oRow.tagName == "TR"){
		AnteriorFila.Typ = "Sel";
		AnteriorFila.className = AnteriorFila.Typ + "Off";
		AnteriorFila = oRow;
	}
	if (oRow.Typ == "Sel"){
		oRow.Typ ="Selected";
		oRow.className = oRow.Typ;
	}
}


function SeleccionarTodos(control)
{
     var numTotal =0;
	if (control.length==undefined)
		{control.checked=true}
	else{
	     for(i=0;i<control.length;i++){
        	  if(control[i].type =="checkbox" && control[i].disabled==false){
			control[i].checked=true
			numTotal = numTotal + parseFloat(control[i].value)
          	   }
	     }
	}
}

function QuitarTodos(control)
{
	if (control.length==undefined)
		{control.checked=false}
	else{
	     for(i=0;i<control.length;i++){
        	  if(control[i].type =="checkbox"){
              		control[i].checked=false
          	}
     	     }
	}
}

function VerificaSeleccion(chkpadre,chkhijos)
{
	if (chkpadre.checked == true)
		{SeleccionarTodos(chkhijos)}
	else
		{QuitarTodos(chkhijos)}	
}


function MarcarTodoCheck()
{
var ArrChk=document.all.chk

	if (document.all.chkSeleccionar.checked==true){
		if (ArrChk.length==undefined)
			{ArrChk.checked=true}
		else{
		     for(i=0;i<ArrChk.length;i++){
        		  if(ArrChk[i].type =="checkbox"){
              			ArrChk[i].checked=true;
				pintarfilamarcada(ArrChk[i])
		          }
		     }
		}
	}
	else{
		if (ArrChk.length==undefined)
			{ArrChk.checked=false}
		else{
		     for(i=0;i<ArrChk.length;i++){
        		  if(ArrChk[i].type =="checkbox"){
              			ArrChk[i].checked=false;
				nopintarfilamarcada(ArrChk[i])
		          }
     		     }
		}
	}
}

function VerificaCheckMarcados(idcheck,cmd)
{
	var numctrl=idcheck.length
	var total=0

	if (numctrl==undefined){
		if (idcheck.checked==true)
			{total=1}
	}
	else{
		for(i=0;i<numctrl;i++){
        	  if(idcheck[i].checked==true)
			{total=total+1}
     		}
	}
	
	if (total==0)
		{cmd.disabled=true}
	else
		{cmd.disabled=false}
}


function pintarfilamarcada(idcheck)
{
   //Asignar siempre como ID del check Ex: 'chk1,chk2, etc'
   var nTemp=idcheck.id
	nTemp=nTemp.substr(3,50)
   var FilaSeleccionada=document.getElementById("fila" + nTemp);
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

function nopintarfilamarcada(idcheck)
{
   //Asignar siempre como ID del check Ex: 'chk1,chk2, etc'
   var nTemp=idcheck.id
	nTemp=nTemp.substr(3,50)
   var FilaSeleccionada=document.getElementById("fila" + nTemp);
   var Celdas = FilaSeleccionada.cells;
   var ArrCeldas  = Celdas.length;
	for (var c = 0; c < ArrCeldas; c++){
		Celdas[c].style.backgroundColor = ''; // #d6e7ef
	}
   return true;
}


function cerrarSistema(pagina)
{
	if (confirm("Est� seguro que desea salir del Sistema")==true)
		{top.location.href=pagina}
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

function Resaltar_azul(op,fila,mostrarhand,colorfila)
{
	if (colorfila=="" || colorfila==undefined){
		colorfila="#FFFFFF"
	}

	if (mostrarhand=="S"){
		fila.style.cursor="hand"
	}
	if(op==1)
		{fila.bgColor="#C6E2F0"}
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
   
   if (pagina!=""){
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

function MostrarTabla(idtabla,img,ruta)
{
     if(idtabla.style.display=="none"){
	 idtabla.style.display=''
	 img.src=ruta + "/menos.gif"
       }
     else{
	idtabla.style.display="none"
	img.src=ruta + "/mas.gif"
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

function imprimir(modo,panel,titulo)
{
	if (modo=="N"){
		window.document.title=titulo
		window.print()
	}
	else{
		window.parent.frames[panel].document.title=titulo
		window.parent.frames[panel].focus()
		window.parent.frames[panel].print()
	}
}


function ResaltarItemMenu(control,colorfila,pagina)
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
		
		if(control.id==Fila.id){
			Fila.style.backgroundColor = colorfila
			Celda[0].style.color = "blue"
		}
		else {
			Fila.style.backgroundColor = ""
			Celda[0].style.color = "black"
		}
	}
   
   if (pagina!="" || pagina!=undefined || pagina!="#"){
	window.parent.frames[2].location.href=pagina
   }
}

function Resaltarfila(tbl,filaseleccionada)
{
	var ArrFilas = tbl.getElementsByTagName('tr')
	var tfilas = ArrFilas.length
	for (var i = 0; i < tfilas; i++){
		var filaactual=ArrFilas[i]
		var Celda=filaactual.getElementsByTagName('td')
		
		if(filaseleccionada==filaactual.id){
			filaactual.style.backgroundColor = "#395ACC"
			for (j=0;j<Celda.length;j++){
				Celda[j].style.color = "white"
			}
		}
		else {
			filaactual.style.backgroundColor = ""
			for (j=0;j<Celda.length;j++){
				Celda[j].style.color = "black"
			}
		}
	}
	
}

function ResaltarfilaDetalle(tbl,filaseleccionada,pagina)
{
/*
	var ArrFilas = tbl.getElementsByTagName('tr')
	var tfilas = ArrFilas.length
	for (var i = 0; i < tfilas; i++){
		var filaactual=ArrFilas[i]
		var Celda=filaactual.getElementsByTagName('td')
		
		if(filaseleccionada.id==filaactual.id){
			filaactual.style.backgroundColor = "#395ACC"
			for (j=0;j<Celda.length;j++){
				Celda[j].style.color = "white"
			}
		}
		else {
			filaactual.style.backgroundColor = ""
			for (j=0;j<Celda.length;j++){
				Celda[j].style.color = "black"
			}
		}
	}

*/
function DetallarComentario(pagina)
{
//SeleccionarFila();

//	if (pagina!="" || pagina!=undefined){
		//txtelegido.value=filaseleccionada.id
		//mensajedetalle.style.display="none"
		//alert(filaseleccionada)
//		fradetallecomentario.location.href=pagina
//	}
	
}

	SeleccionarFila();

	if (pagina!="" || pagina!=undefined){
		txtelegido.value=filaseleccionada.id
		mensajedetalle.style.display="none"
		fradetalle.location.href=pagina
	}
	
}
function ResaltarPestana(numcol,colorfila,pagina)
{
	if (colorfila=="" || colorfila==undefined){
		colorfila="#EEEEEE"
	}
	for (var c = 0; c < tab.length; c++){
		var Celda=tab[c]	
		if(numcol==c){
			Celda.style.backgroundColor = colorfila
			Celda.style.color = "blue"
		}
		else {
			Celda.style.backgroundColor = "#C0C0C0"
			Celda.style.color = "#808080"
		}
	}
   
   if (pagina!=""){
	fracontenedor.location.href=pagina
   }
}

function ResaltarPestana2(numcol,clsResaltado,pagina)
{
	if (clsResaltado=="" || clsResaltado==undefined){
		clsResaltado="pestanaresaltada"
	}
	for (var c = 0; c < tab.length; c++){
		var Celda=tab[c]	
		if(numcol==c)
			{Celda.className=clsResaltado}
		else
			{Celda.className="pestanabloqueada"}
	}
   
   if (pagina!=""){
	fracontenedor.location.href=pagina
   }
}

/*POR HUGO PARA WEB DE PROPUESTAS*/
function ResaltarPestana_1(numcol,clsResaltado,pagina)
{
	if (clsResaltado=="" || clsResaltado==undefined){
		clsResaltado="pestanaresaltada_1"
	}
	for (var c = 0; c < tab.length; c++){
		var Celda=tab[c]	
		if(numcol==c)
			{Celda.className=clsResaltado}
		else
			{Celda.className="pestanabloqueada"}
	}
   
   if (pagina!=""){
	fracontenedor.location.href=pagina
   }
}

//funciones para ejecutar acciones en modo GRID
function MM_goToURL()
{
  var i, args=MM_goToURL.arguments; document.MM_returnValue = false;
  for (i=0; i<(args.length-1); i+=2)
  		eval(args[i]+".location='"+args[i+1]+"'");
}

function EjecutarFormulario(formulario)
{
	formulario.submit
}


//Permite pasar entre listas los datos


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

 function EnviarItem(select,destino)
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
  	for (var i = 0; i < text.length; i++){
	  	var opt = new Option(text[i], value[i])
		destino.options[destino.options.length]=opt
	}
 }
 
 function AgregarDocente(select)
 {
    var text = new Array();
	var value = new Array();
	var num = select.options.length;
  	for (var i = num - 1; i >= 0; i--)
		{
    	if (select.options[i].selected)
			{
				if (frmListaCorreos.ListaPara.options.length==0)
		      		{text[text.length] = select.options[i].text + '(Coordinador)';}
				else
					{text[text.length] = select.options[i].text;}
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
 
 function Validarlistaagregada(form)
 {
 	  if (form.ListaPara.options.length == 0)
		{
			alert("Debe seleccionar alg�n miembro de la lista nombres.");
			return false;
		}
		SeleccionarDestinatarios()
		return true;
 }


function ElegirRecurso(spElegido)
{
	//Quitar el resto de carpetas resaltadas
	var ArrSpan = document.getElementsByTagName("span")

	for (var i=0;i<ArrSpan.length;i++){
		var spanActual=ArrSpan[i]

		if (spanActual.id==spElegido.id)
			{ResaltarVineta("1",spElegido,"")}
	  	else
			{ResaltarVineta("0",spanActual,"")}
	}
}

function ResaltarVineta(op,fila,colorfila)
{
	if (colorfila=="" || colorfila==undefined){
		colorfila="#FFFFFF"
	}

	if(op==1){
		fila.style.backgroundColor="#395ACC"
		fila.style.color = "white"
	}
	else{
		fila.style.backgroundColor=colorfila
		fila.style.color = "black"	
	}
}

// AGRUPA LOS DATO NUMERICOS Y FECHAS EN UN ARRAY
// PARA ORDENARLOS CUANDO LO REQUIERA
function AgruparTipoDato(cValue)
  {

    var isDate = new Date(cValue);
    if (isDate == "NaN")
      {
        if (isNaN(cValue))
          {
            // THE VALUE IS A STRING, MAKE ALL CHARACTERS IN
            // STRING UPPER CASE TO ASSURE PROPER A-Z SORT
            cValue = cValue.toUpperCase();
            return cValue;
          }
        else
          {
            // VALUE IS A NUMBER, TO PREVENT STRING SORTING OF A NUMBER
            // ADD AN ADDITIONAL DIGIT THAT IS THE + TO THE LENGTH OF
            // THE NUMBER WHEN IT IS A STRING
            var myNum;
            myNum = String.fromCharCode(48 + cValue.length) + cValue;
            return myNum;
          }
        }
  else
      {
        // VALUE TO SORT IS A DATE, REMOVE ALL OF THE PUNCTUATION AND
        // AND RETURN THE STRING NUMBER
        //BUG - STRING AND NOT NUMERICAL SORT .....
        // ( 1 - 10 - 11 - 2 - 3 - 4 - 41 - 5  etc.)
        var myDate = new String();
        myDate = isDate.getFullYear() + " " ;
        myDate = myDate + isDate.getMonth() + " ";
        myDate = myDate + isDate.getDate(); + " ";
        myDate = myDate + isDate.getHours(); + " ";
        myDate = myDate + isDate.getMinutes(); + " ";
        myDate = myDate + isDate.getSeconds();
        //myDate = String.fromCharCode(48 + myDate.length) + myDate;
        return myDate ;
      }
  }
function OrdenarTabla(col, IdTabla,ruta)
  {
    var iCurCell = col + IdTabla.cols;
    var totalRows = IdTabla.rows.length;
    var bSort = 0;
    var colArray = new Array();
    var oldIndex = new Array();
    var indexArray = new Array();
    var bArray = new Array();
    var newRow;
    var newCell;
    var i;
    var c;
    var j;
    //CAMBIAR EL CURSOR
    window.defaultStatus="Ordenando..."
    //COLOCAR IMAGEN EN LA CELDA A ORDENAR
    for (i=0; i < IdTabla.cols; i++){
		IdTabla.cells[i].innerHTML=IdTabla.cells[i].innerText
		if (i==col){
			IdTabla.cells[col].innerHTML="<img id='img' src='" + ruta + "' /> " + IdTabla.cells[col].innerText
		}
      }
    
    // ** POPULATE THE ARRAY colArray WITH CONTENTS OF THE COLUMN SELECTED
    for (i=1; i < IdTabla.rows.length; i++){
        colArray[i - 1] = AgruparTipoDato(IdTabla.cells(iCurCell).innerText);
        iCurCell = iCurCell + IdTabla.cols;
      }
    // ** COPY ARRAY FOR COMPARISON AFTER SORT
    for (i=0; i < colArray.length; i++)
      {
        bArray[i] = colArray[i];
      }
    // ** SORT THE COLUMN ITEMS
    //alert ( colArray );
    colArray.sort();
    //alert ( colArray );
    for (i=0; i < colArray.length; i++)
      { // LOOP THROUGH THE NEW SORTED ARRAY
        indexArray[i] = (i+1);
        for(j=0; j < bArray.length; j++)
          { // LOOP THROUGH THE OLD ARRAY
            if (colArray[i] == bArray[j])
              {  // WHEN THE ITEM IN THE OLD AND NEW MATCH, PLACE THE
                // CURRENT ROW NUMBER IN THE PROPER POSITION IN THE
                // NEW ORDER ARRAY SO ROWS CAN BE MOVED ....
                // MAKE SURE CURRENT ROW NUMBER IS NOT ALREADY IN THE
                // NEW ORDER ARRAY
                for (c=0; c<i; c++)
                  {
                    if ( oldIndex[c] == (j+1) )
                    {
                      bSort = 1;
                    }
                      }
                      if (bSort == 0)
                        {
                          oldIndex[i] = (j+1);
                        }
                          bSort = 0;
                        }
          }
    }
  // ** SORTING COMPLETE, ADD NEW ROWS TO BASE OF TABLE ....
  for (i=0; i<oldIndex.length; i++)
    {
      newRow = IdTabla.insertRow();
      for (c=0; c<IdTabla.cols; c++)
        {
          newCell = newRow.insertCell();
          newCell.innerHTML = IdTabla.rows(oldIndex[i]).cells(c).innerHTML;
          if (c==col)
			{newCell.style.backgroundColor="#F2F2F2";}
          }
      }
  //MOVE NEW ROWS TO TOP OF TABLE ....
  for (i=1; i<totalRows; i++)
    {
      IdTabla.moveRow((IdTabla.rows.length -1),1);
    }
  //DELETE THE OLD ROWS FROM THE BOTTOM OF THE TABLE ....
  for (i=1; i<totalRows; i++)
    {
      IdTabla.deleteRow();
    }
	window.defaultStatus=""
}

//Autocompletar combo

function elegirItem(texto,lista)
{
	texto.value=lista.options[lista.selectedIndex].text
}

function LimpiarError() {
 window.status="Se ha producido un error. Cont�ctese con el Administrador del Sistema"
 return true;
} 

//window.onerror = LimpiarError


function AutocompletarCombo(cajatexto, combo, campobusquedacombo, forzarbusqueda){
	var found = false;

	for (var i = 0; i < combo.options.length; i++){
		if (combo.options[i][campobusquedacombo].toUpperCase().indexOf(cajatexto.value.toUpperCase()) == 0){
			found=true; break;
		}
	}

	if (found)
		{combo.selectedIndex = i;}
	else
		{combo.selectedIndex = -1;}

	if(cajatexto.createTextRange){
		if (forzarbusqueda && !found){
			cajatexto.value=cajatexto.value.substring(0,cajatexto.value.length-1);
			return;
		}
		
		var cursorKeys ="8;46;37;38;39;40;33;34;35;36;45;";
		if (cursorKeys.indexOf(event.keyCode+";") == -1){
			var r1 = cajatexto.createTextRange();
			var valoranterior = r1.text;
			var nuevovalor = found ? combo.options[i][campobusquedacombo] : valoranterior;
			
			if (nuevovalor != cajatexto.value) {
				cajatexto.value = nuevovalor;
				
				var rNew = cajatexto.createTextRange();
				rNew.moveStart('character',valoranterior.length) ;
				rNew.select();
			}
		}
	}
}