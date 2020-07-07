/*
=====================================================================================
 CVUSAT
 Fecha Creación: 13/12/2006
 Autor: desarrollosistemas@usat.edu.pe
 Observación: Permitirá agregar un ToolTipText a cualquier objeto o TAG
=====================================================================================
*/

var instatus =false;	//Determina si se mostrará en la barra de estado
var ttname = "tooltip"; //Nombres del tooltip div tag (used on next line)
var bcolor = "#ffffe0";	//Fondo de tooltip
var fcolor = "#000000";		//Color de texto del tooltip
var opacity =200;		//Transparencia del tooltip
var borderstyle = "outset";	//Estilo de Borde del tooltip
var borderwidth = 1;		//Ancho de Borde del tooltip
var bordercolor = "#808080";	//Color de Borde del tooltip
var font = "MS Sans Serif";	//Tipo de Letra
var fontsize = 8;		//Tamaño de Letra

//Escribir el DIV que mostrará mensaje
document.write('<div style="color: '+fcolor+'; z-index: 2000;padding: 5px; position: absolute; filter: alpha(opacity='+opacity+');background-color: '+bcolor+'; border-style: '+borderstyle+'; border-width: '+borderwidth+'; border-color: '+bordercolor+'; font-family: '+font+'; font-size: '+fontsize+'; visibility: hidden;" id="'+ttname+'">');
document.write('Sin mensaje');
document.write('</div>');

//Muestra el Tip
function tooltip() {
	e = event;
	es = event.srcElement;	//event source
	//if the event source has a tooltip set and you DID move the
	//mouse over a tag, then drag the tooltip
	if (es.tooltip != "" && es.tooltip != undefined) {
		document.getElementById(ttname).style.visibility = "visible";
		document.getElementById(ttname).style.left = e.x+10;
		document.getElementById(ttname).style.top = e.y;
		document.getElementById(ttname).style.position = "absolute";
		document.getElementById(ttname).innerHTML = es.tooltip;
		if (instatus) {
			window.status = es.tooltip;
		}
	}
}

//Oculta el Tip
function OcultarTip() {
	document.getElementById(ttname).style.visibility = "hidden";
	if (instatus) {
		window.status = "";
	}
}

document.onmousemove = tooltip;
document.onmouseout = OcultarTip;