var valorActual=-1;
var alto=window.screen.Height-90
var ancho=window.screen.Width-20
var timer

var PropVentana="width=" + ancho +",height=" + alto +",statusbar=yes,scrollbars=no,top=0,left=0,resizable=yes,toolbar=no,menubar=no"


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

function MM_goToURL()
{
  var i, args=MM_goToURL.arguments; document.MM_returnValue = false;
  for (i=0; i<(args.length-1); i+=2)
  		eval(args[i]+".location='"+args[i+1]+"'");
}

function AbrirPopUp(pagina,alto,ancho,ajustable,bestado,barras)
{
   izq = (screen.width-ancho)/2
   arriba= (screen.height-alto)/2

   var ventana=window.open(pagina,"popup","height="+alto+",width="+ancho+",statusbar="+bestado+",scrollbars="+barras+",top=" + arriba +",left=" + izq + ",resizable="+ajustable+",toolbar=no,menubar=no");
   ventana.location.href=pagina
   ventana=null
}

function AbrirAyuda(pagina)
{
   var izq = (screen.width-600)/2
   var arriba= (screen.height-400)/2

   var ventana=window.open(pagina,"frmAyuda","height=400,width=600,statusbar=yes,scrollbars=yes,top=" + arriba +",left=" + izq + ",resizable=no,toolbar=no,menubar=no");
   ventana.location.href=pagina
   ventana=null
}

function AbrirCorreo(ntabla,itabla,modo)
{
	var pagina="../../../mailnet/enviaremail.aspx?idcursovirtual=" + itabla
	AbrirPopUp(pagina,"500","650")

}

function AbrirMensaje()
{
    var ventana = window.createPopup();
    var contenido = ventana.document.body;
    contenido.style.backgroundColor = "#FFFFCC";
    contenido.style.border = "solid black 2px";
    contenido.innerHTML = "<center><h3>Cargando</h3><p><img border='0' src='../../../images/loading.gif'></p><p><font color='#800000' face='Verdana' style='font-size: 8pt'>Espere un momento por favor...</font></p>"
    ventana.show(150,150,250,100, document.body);
    setTimeout("window.close()", 1000)
}

function Eliminar(mensaje,pagina)
{
	var Confirmar=confirm(mensaje);
	if(Confirmar==true)
		{document.location.href=pagina}
	else
		{return false;}
}

function HabilitarEleccion(control,incluyebtn)
{
	txtelegido.value=control.value
	if (incluyebtn=="I"){
		cmdModificar.disabled=false
		cmdEliminar.disabled=false
		cmdPermisos.disabled=false
	}
	else{
		parent.cmdModificar.disabled=false
		parent.cmdEliminar.disabled=false
		parent.cmdPermisos.disabled=false
	}
}


function MarcarTodoCheck(formulario)
{
var numTotal=formulario.chk.length;

	if (formulario.chkSeleccionar.checked==true){
		if (numTotal==undefined)
			{formulario.chk.checked=true}
		else{
		     for(i=0;i<numTotal;i++){
        		  if(formulario.chk[i].type =="checkbox"){
              			formulario.chk[i].checked=true;
				pintafilamarcada(formulario.chk[i])
		          }
		     }
		}
	}
	else{
		if (numTotal==undefined)
			{formulario.chk.checked=false}
		else{
		     for(i=0;i<numTotal;i++){
        		  if(formulario.chk[i].type =="checkbox"){
              			formulario.chk[i].checked=false;
				nopintafilamarcada(formulario.chk[i])
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
		if (idcheck==true)
			numctrl=1
	}
	else{
		for(i=0;i<numctrl;i++){
        	  if(idcheck[i].checked=true)
			{total=total+1}
     		}
	}
	if (total>0)
		{cmd.disabled=false}
	else
		{cmd.disabled=true}

}


function pintafilamarcada(idcheck)
{
   var FilaSeleccionada=document.getElementById("fila" + idcheck.value);
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

function nopintafilamarcada(idcheck)
{
   var FilaSeleccionada=document.getElementById("fila" + idcheck.value);
   var Celdas = FilaSeleccionada.getElementsByTagName('td');	
   var Celdas = FilaSeleccionada.cells;
   var ArrCeldas  = Celdas.length;
	for (var c = 0; c < ArrCeldas; c++){
		//if(idcheck.checked==false){
			Celdas[c].style.backgroundColor = ''; // #d6e7ef
		//}
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

function MuestraMenuTemp(Mnu)
{
   var margenderecho=document.body.clientWidth-event.clientX
   var margeninferior=document.body.clientHeight-event.clientY

	if (margenderecho<Mnu.offsetWidth)
		{Mnu.style.left=document.body.scrollLeft+event.clientX-Mnu.offsetWidth}
	else
		{Mnu.style.left=document.body.scrollLeft+event.clientX}
	
	if (margeninferior<Mnu.offsetHeight)
		{Mnu.style.top=document.body.scrollTop+event.clientY-Mnu.offsetHeight}
	else
		{Mnu.style.top=document.body.scrollTop+event.clientY}

 	if(Mnu.style.visibility=="hidden")
  		{Mnu.style.visibility="visible"}
	else
		{Mnu.style.visibility="hidden"}
}

function MostrarMenuContextual(tbl,Mnu)
{
	var PosY=tbl.offsetTop+15
	var Alto=tbl.offsetHeight
	Alto=Alto + PosY

	Mnu.style.top=Alto
	//Mnu.style.visibility="visible"
	//clearTimeout(timer)
 	if(Mnu.style.visibility=="hidden")
  		Mnu.style.visibility="visible"
	else
		Mnu.style.visibility="hidden"
}

function OcultarMenuContextual(Mnu)
{
	//var str=Mnu.style.visibility="hidden"
	//timer=setTimeout(Mnu.style.visibility="hidden",1500)
	Mnu.style.visibility="hidden"
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

function cerrarSistema(pagina)
{
	if (confirm("Está seguro que desea salir del Sistema")==true)
		{top.location.href=pagina}
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
 
 function validarlistaElegida(form)
 {
 	  if (form.ListaPara.options.length == 0)
		{
			alert("Debe seleccionar algún miembro de la lista nombres.");
			return false;
		}
		SeleccionarDestinatarios()
		DesactivarControlesfrm(form)
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

function ResaltarCelda(celda)
{
	celda.style.backgroundColor="#F7EEB3"
	celda.style.borderStyle="solid"
	celda.style.borderWidth="1"
}

function ResaltarPestana(numcol,colorfila,pagina)
{
	if (colorfila=="" || colorfila==undefined){
		colorfila="#FFFDD2"
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

//Resaltar viñetas resaltada
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

function ConvertirMayuscula(control)
{
	control.value=control.value.toUpperCase()
}

function ResaltarConvertirMayusc(control)
{
	control.style.backgroundColor="white"
	control.value=control.value.toUpperCase()
}

function Resaltarfila(tbl,filaseleccionada,pagina)
{
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

	if (pagina!="" || pagina!=undefined){
		var codigo=filaseleccionada.id
		codigo=codigo.substr(0,4)
		txtelegido.value=codigo
		mensajedetalle.style.display="none"
		fradetalle.location.href=pagina
	}
	
}


function ImprimirPagina(modo,frameYOTabla,encabezadoframe)
{
	switch(modo)
		{
		case "1": //Enviar la misma página ubicada en frames a imprimir
			window.parent.frames[frameYOTabla].focus()
			window.parent.frames[frameYOTabla].print()
			break			
		case "2": //Enviar el encabezado del iframe y la tabla incluida en el iframe
			ImprimirIframe("Reporte de documentos",frameYOTabla)
			break
		case "3":
			//var encabezado="<table><tr>" + encabezadoframe.innerHTML + "</tr></table>"
			//var textoHTML=encabezado + frameYOTabla.outerHTML
			//textoHTML=QuitarScriptObjeto()//ReemplazarTodo(textoHTML,[["<TR id=NoImprimir","<tr>"]])
			//crearVentanaImpresion(textoHTML)
			window.print()
			break
	}
}


function ImprimirIframe(titulo,tablaorigen)
{
	var izq = (screen.width-ancho)/2
	var arriba= (screen.height-alto)/2
   
	var ventana=window.open("../vistaprevia.htm","ReporteImpresion","height="+alto+",width="+ancho+",statusbar=yes,scrollbars=yes,top=" + arriba +",left=" + izq + ",resizable=yes,toolbar=no,menubar=no");
	ventana.location.href="../vistaprevia.htm"
	//ventana.tituloreporte.innerHTML=titulo
	copiarTablaAreporte(tablaorigen,ventana.tblreporte,ventana)
	ventana=null
}

//copiar la tabla origen a la tabla destino del reporte
function copiarTablaAreporte(tblorigen,tbldestino,pagina)
{
	var ArrFilas = tblorigen.getElementsByTagName('tr')
	var tfilas = ArrFilas.length

     for (var i = 0; i < tfilas; i++){
		var ArrCeldas=ArrFilas[i].getElementsByTagName('td').cells
		var ArrCeldas=ArrFilas[i].cells
		var tceldas=ArrCeldas.length
		//variables de filas y celdas destino
		var UltimaFila=tbldestino.rows.length
		var FilaNueva=tbldestino.insertRow(UltimaFila)
 				
		for (var j=0;j<tceldas;j++){
			var TextoCelda=ArrCeldas[j].innerText
			
			var arrCeldasN=FilaNueva.getElementsByTagName('td').cells
			var arrCeldasN=FilaNueva.cells
			var tCeldasN=arrCeldasN.length
			
			var CeldaNueva=FilaNueva.insertCell(tCeldasN)
			
			CeldaNueva.appendChild(pagina.document.createTextNode(TextoCelda))
			FilaNueva.appendChild(CeldaNueva)
		}
     }
}

function crearVentanaImpresion(textoHTML)		
{
   var izq = (screen.width-ancho)/2
   var arriba= (screen.height-alto)/2
		
	var ventana=window.open("temporal.htm","ReporteImpresion","height="+alto+",width="+ancho+",statusbar=yes,scrollbars=yes,top=" + arriba +",left=" + izq + ",resizable=yes,toolbar=no,menubar=no");
		//ventana.location.href="temporal.htm"
		ventana.document.write("<html>\n")
		ventana.document.write("<head>\n")
		ventana.document.write("<link rel='stylesheet' type='text/css' href='../private/estilo.css'>\n")
		//ventana.document.write("<script language=Javascript>\n")
		//ventana.document.write("{document.all.NoImprimir.innerHTML='<tr>'}\n")
		//ventana.document.write("</script>\n")
		ventana.document.write("</head>\n")
		ventana.document.write("<body>\n")
		ventana.document.write("" + textoHTML + "")
		ventana.document.write("</body>\n")
		ventana.document.write("</html>\n")
		ventana=null
}

/*
function ReemplazarTodoUnoPorUno(cadena,textobusqueda,textoreemplazo)
{
    var idx = cadena.indexOf(textobusqueda);

    while ( idx > -1 ){
        cadena = cadena.replace(textobusqueda,textoreemplazo); 
        idx = cadena.indexOf(textobusqueda);
    }

    return cadena;
}
*/
function ReemplazarTodo(cadena,arrReemplazos)
{
    for (i=0;i<arrReemplazos.length;i++){
        var idx=cadena.indexOf(arrReemplazos[i][0]);

        while(idx>-1){
            cadena=cadena.replace(arrReemplazos[i][0],arrReemplazos[i][1] ); 
            idx=cadena.indexOf(arrReemplazos[i][0]);
        }

    }
    return cadena;
}

function ConfigurarImpresion()
{
   var a = fralista.document.all.item("NoImprimir");
        if (a!=null){
            if (a.length!=null){
                for (i=0; i< a.length; i++){
	                a(i).style.display = window.event.type == "beforeprint" ? "none" :"inline";
            	}
	    }
            else
	    	{a.style.display = window.event.type == "beforeprint" ? "none" :"inline";}
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