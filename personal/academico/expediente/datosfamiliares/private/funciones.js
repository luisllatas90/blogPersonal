var alto=window.screen.Height-90
var ancho=window.screen.Width-20
var AnteriorFila=0;
var UltimaFila=0;

var PropVentana="width=" + ancho +",height=" + alto +",statusbar=yes,scrollbars=yes,top=0,left=0,resizable=yes,toolbar=no,menubar=no"


//desactiva botón derecho del menú

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

function AbrirAyuda(pagina)
{
	var prop="width=" + ancho +",height=" + alto +",statusbar=yes,scrollbars=yes,top=0,left=0,resizable=yes,toolbar=no,menubar=no"
	var ventana=window.open(pagina,"Ayuda",prop)
	ventana.location.href=pagina
	ventana=null
}


function convertirEnterTab()
{
	if (window.event.keyCode==13)
		{window.event.keyCode=9}
}

function validarnumero(e)
{
	tecla = (document.all) ? e.keyCode : e.which; // 2
    if (tecla==8) return true; // 3
    //patron =/[A-Za-z\s]/; // 4
    patron = /\d/; // Solo acepta números
	//patron = /\w/; // Acepta números y letras
	//patron = /\D/; // No acepta números
	//patron =/[A-Za-zñÑ\s]/; // igual que el ejemplo, pero acepta también las letras ñ y Ñ

	te = String.fromCharCode(tecla); // 5
	 	
	return patron.test(te); // 6

}

function DesactivarControlesfrm(frm)
{
	var guardar=frm.cmdGuardar
	var cancelar=frm.cmdCancelar

	if (guardar!=undefined){
		guardar.disabled=true
		cancelar.disabled=true
	}
	mensaje.innerHTML="<b>&nbsp;Espere un momento por favor...</b>"
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

/*Resaltar fila al pasar el mouse*/
function Resaltar(op,fila,mostrarhand)
{
	if (mostrarhand=="S"){
		fila.style.cursor="hand"
	}
	if(op==1)
		{fila.className="celdaresalta"}
	else
		{fila.className="celdanoresalta"}
}

function menuDorado(obj){
obj.className="menuDorado"
}

function menuBlanco(obj){
obj.className="menuBlanco"
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

function ResaltarfilaDetalle(tbl,filaseleccionada,pagina)
{
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

function Maximizar(img,ruta,tmMax,tmNormal)
{
	if(trLista.style.display==""){
		img.src=ruta + 'images/minimiza.gif' 
		trLista.style.display="none"
		trDetalle.style.height=tmMax
	}
	else{
		img.src=ruta + 'images/maximiza.gif'
		trLista.style.display=""
		trDetalle.style.height=tmNormal
	}
}

function ImprimirPagina(modo,panel,titulo)
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

function ImprimirDetalle(titulo)
{
	if(titulo==undefined){
		titulo=''
	}
	fradetalle.document.title=titulo
	fradetalle.focus();
	fradetalle.print();
}

function ContarTextArea(campo,limite,obj)
{
	if (campo.value.length >limite){
	    campo.value = campo.value.substring(0, limite);
	    alert( 'Este campo sólo aceptar hasta '  + limite + ' caracteres');
	    return false;
	}
	else{
		if (obj==undefined){
			lblcontador.innerHTML=eval(limite-campo.value.length) + ' caracteres.'
		}
		else{
			obj.innerHTML=eval(limite-campo.value.length) + ' caracteres.'
		}
	}
}

function VerificaCheck(idcheck,cmd)
{
    var numctrl=idcheck.length
    var total=0

    if (numctrl>0){
        for(i=0;i<numctrl;i++){
            if(idcheck[i].checked==true)
		        {total=total+1}
        }
    }
    else{
	    if (idcheck.checked==true)
		    {total=1}
    }
	
    if (total==0)
	    {cmd.disabled=true}
    else
	    {cmd.disabled=false}
} 

function OcultarTabla()
{
    document.getElementById("form1").style.display="none"
    document.getElementById("tblmensaje").style.display=""
}