/*
	Permite deshabilitar las teclas ALT-Backspace-F5 entre otras accesos
*/

//Deshabilitar clic derecho

if (window.Event) 
	document.captureEvents(Event.MOUSEUP);

function QuitarMenu() 
{ 
	event.cancelBubble = true 
	event.returnValue = false
	return false
} 

function QuitarClicDerecho(e) 
{ 
	if (window.Event){ 
		if (e.which !=1) 
			{return false}
	} 
	else 
		if (event.button !=1){ 
			event.cancelBubble = true
			event.returnValue = false
			return false
		}
	}
} 

document.oncontextmenu = QuitarMenu
document.onmousedown = QuitarClicDerecho

/*Bloquear teclas

function DeshabilitarAcceso()
{ 
	if ((event.altKey) || ((event.keyCode == 8) && 
		(event.srcElement.type != "text" && event.srcElement.type != "textarea" && event.srcElement.type != "password")) || 
		((event.ctrlKey) && ((event.keyCode == 78) || (event.keyCode == 82)) ) || (event.keyCode == 116) ){ 
		event.keyCode = 0; 
		event.returnValue = false; 
        } 
} 

onKeyDown="DeshabilitarAcceso()"*/