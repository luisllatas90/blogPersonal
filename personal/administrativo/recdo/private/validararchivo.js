var AIniciarBusqueda = "";
var timeoutCtr = 0;
var listadescriptores;
var txtFind;
var keycode;

function BloquearEnter()
{
	if (event.keyCode==13)
		{event.returnValue = false}
}


function validararchivo()
{

	if (frmarchivo.numeroexpediente.value == "")
	{
	alert("Por favor ingrese el Numero de Expediente");
	frmarchivo.numeroexpediente.focus();
	return (false);
	}

	if (frmarchivo.numerotipo.value == "")
	{
	alert("Por favor ingrese el Numero según el Tipo de documento");
	frmarchivo.numerotipo.focus();
	return (false);
	}

	if (frmarchivo.asunto.value == "")
	{
	alert("Por favor ingrese el asunto sobre lo que trata el documento");
	frmarchivo.asunto.focus();
	return (false);
	}

	if (frmarchivo.idprocedencia.value=="0" || frmarchivo.idprocedencia.value=="")
	{
	alert("Por favor seleccione la procedencia del documento")
	return(false)
	}

	if (frmarchivo.iddestinatario.value=="0" || frmarchivo.iddestinatario.value=="0")
	{
	alert("Por favor seleccione el destino del documento")
	return(false)
	}

	if (confirm("Está seguro que desea guardar los cambios?")==true)
		{return (true)}
	else
		{return (false)}
}

function validarprocedencia()
{

	if (frmprocedencia.razon.value.length < 3)
	{
	alert("Por favor ingrese la Razón social o los apellidos y nombres de la Procedencia del documento");
	frmprocedencia.razon.focus();
	return (false);
	}

return (true);
}

function validardestinatario()
{

	if (frmdestinatario.nombre.value.length < 3)
	{
	alert("Por favor ingrese el área o los apellidos y nombres del Destinatario del documento");
	frmdestinatario.nombre.focus();
	return (false);
	}

return (true);
}


function cambiarRazon(modo)
{
	if (modo==0)
		razonsocial.innerHTML="<b>Apellidos y Nombres</b>"
	else
		razonsocial.innerHTML="<b>Razón Social</b>"
}

function enviarvaloresArchivo(pagina,idarchivo,accion)
{
	location.href=pagina + "&accion=" + accion + "&idarchivo=" + idarchivo + "&numeroexpediente="+ frmarchivo.numeroexpediente.value +"&numerotipo="+frmarchivo.numerotipo.value+"&idtipoarchivo="+frmarchivo.idtipoarchivo.value+"&prioridad="+frmarchivo.prioridad.value+"&dia="+frmarchivo.dia.value+"&mes=" + frmarchivo.mes.value + "&hora=" + frmarchivo.hora.value + "&min=" + frmarchivo.min.value +"&turno=" + frmarchivo.turno.value + "&iddestinatario="+frmarchivo.iddestinatario.value+"&idprocedencia="+frmarchivo.idprocedencia.value+"&obs="+frmarchivo.obs.value+"&asunto="+frmarchivo.asunto.value

}

function AbrirFicha(pagina,obj)
{
	fradetalle.location.href=pagina + "?" + hdString.value
}

function AbrirArchivo(idarchivo,accion)
{
	var izq = (screen.width-600)/2
	var arriba= (screen.height-300)/2
	var pagina="documento.asp?idarchivo=" + idarchivo + "&accion=" + accion
	var ventana=window.open(pagina,"VentanaArchivo","height=300,width=600,statusbar=yes,scrollbars=no,top=" + arriba + ",left=" + izq + ",resizable=no,toolbar=no,menubar=no");
	ventana.location.href=pagina
	ventana=null
}

function AbrirMovimiento(accion,idarchivo,numeroexpediente)
{
	var izq = (screen.width-600)/2
	var arriba= (screen.height-350)/2
	var idmovimiento=0

	if (accion=="A"){
		pagina="movimiento.asp?accion=agregarmovimiento&idarchivo=" + idarchivo + "&numeroexpediente=" + numeroexpediente
		}
	else{
		var txtIdmov=document.all.txtmovimiento.value
		if (txtIdmov==""){
			alert("Debe elegir un movimiento para modificarlo")
			return(false)
		}
		else{
			pagina="movimiento.asp?accion=modificarmovimiento&idarchivo=" + idarchivo + "&numeroexpediente=" + numeroexpediente + "&idmovimiento=" + txtIdmov
		}
	}
	//Enviar datos a la página
	var ventana=window.open(pagina,"VentanaArchivo","height=350,width=600,statusbar=yes,scrollbars=no,top=" + arriba + ",left=" + izq + ",resizable=no,toolbar=no,menubar=no");	
	ventana.location.href=pagina
	ventana=null
}

//Términos para realizar las búsqueda de documentos
//1. Abrir ventana
//2. Evaluar criterios de búsqueda
//3. Recuperar criterios de búsqueda y realizarla

function AbrirBusqueda()
	{
	   var termino 	= null;
	   var campo	= null;
	   showModalDialog("buscar.asp",window,"dialogWidth:400px;dialogHeight:250px;status:no;help:no;center:yes");
	}

function EvaluarCriteriosbusqueda()
{
   	var controlbusqueda=document.all.texto
   	var terminobusqueda=controlbusqueda.value
   	var campobusqueda=document.all.cbxcampo
	
	if (terminobusqueda.length<3 && campobusqueda.selectedIndex!=5){
		alert("Por favor escriba el término de búsqueda")
		controlbusqueda.value=""
		controlbusqueda.focus()
		return(false)
	}
 	else{
		var Argumentos = window.dialogArguments;
	   	Argumentos.termino=terminobusqueda
   		Argumentos.campo=campobusqueda.value
	   	Argumentos.RecuperarCondicionBusqueda();
		window.close();
	}
}

function desactivarCamposBusqueda()
{
	if (document.all.cbxcampo.selectedIndex==5){
		document.all.texto.disabled=true
	}
	else{
		document.all.texto.disabled=false
		document.all.texto.focus()
	}
}


function RecuperarCondicionBusqueda()
{
	window.location.href="bentrada.asp?tipobus=7&termino=" + termino + "&campo=" + campo
}

function MedioDiaHoraInicio()
{
	if (document.all.hora.value==12){
		document.all.turno.value="p.m."
		document.all.turno.disabled=true
	}
	else{
		document.all.turno.value="a.m."
		document.all.turno.disabled=false
	}
}

function EliminarArchivo(accion,valor)
{
	var Confirmar=confirm("¿Está seguro que desea eliminar este archivo?");
	if(Confirmar==true)
		{document.location.href="procesar.asp?accion=" + accion + valor}
	else
		{return false;}
}

function EliminarMovimiento(idarch,numexp)
{
	var txtIdmov=document.all.txtmovimiento.value
	
	if (txtIdmov=="")
		{alert("Debe elegir un movimiento para eliminar")}
	else{
		var Confirmar=confirm("¿Está seguro que desea eliminar este movimiento del archivo?");
		if(Confirmar==true){
			document.location.href="procesar.asp?accion=eliminarmovimiento&idarchivo=" + idarch + "&numeroexpediente=" + numexp + "&idmovimiento=" + txtIdmov
		}
		else
			{return false}
	}
}
