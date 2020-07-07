//------------------Variables para controlar el tiempo de evaluación------------------------
var InicioTiempo = 0;
var TiempoActual=0;
var tEmpezar  = 1; //DEBE INICIAR EN 0 Y SE CAMBIA A 1 CUANDO SE PRESIONE ALGUN BONTON
var TiempoInicial=null
//-------------------------------------------------------------------------------------------


function clsPregunta()
{
	this.idPre=0
	this.Controles=new Array
}

function Buscar(Lista,ID)
{
 for(var i=0;i<Lista.length;i++){
  if(Lista[i].idPre==ID)
  	{return i}
 }
 return -1 //-1 significa elemento no encontrado
}

function Organizar(Formulario,Lista)
{
 var Control
 for(var i=0 ;i< Formulario.elements.length;i++){
  Control=Formulario.elements[i]
  
  if(Control.type=="radio" || Control.type=="text" || Control.type=="textarea" || Control.type=="checkbox")
 	{
	  var pos=Buscar(Lista,Control.idPre)
  		if(pos==-1){//pregunta no existe en lista
  		pos=Lista.length
	  	Lista[pos]=new clsPregunta()
  		Lista[pos].idPre=Control.idPre
  		}
  	Lista[pos].Controles.push(Control)
   }
 }
}

function validarPregunta(Pregunta)
{
 var i
 var HayRpta=0
 for(i=0;i<Pregunta.Controles.length;i++){
	var Control=Pregunta.Controles[i]
	switch(Control.type){
		case "text":
		case "textarea":
			if(Control.value!="") 
				HayRpta=1
				break
		case "radio":
		case "checkbox":
			if(Control.checked!=0) 
				HayRpta=1		
				break
	}
  }
  
 if(HayRpta!=1){
	//+ Pregunta.idPre
	alert("Por favor debe responder a la pregunta formulada") 
	return(false)
 	}
}

function ValidarFormulario(formulario)
{
 var Lista=new Array
 var esCorrecto=true
 Organizar(formulario,Lista)
 for(var i=0; i<Lista.length;i++){
	//var Rpta=validarPregunta(Lista[i])
	esCorrecto=esCorrecto && validarPregunta(Lista[i])
 }
 
 if(esCorrecto==false)
 	{return(false);}
 return(true)
}


function ActualizarTiempo(TiempoDuracion) {
var Minutos, Segundos
   if(tEmpezar)
      {
      	if(TiempoInicial==null)
		{TiempoInicial=new Date()}
		var FechaActual = new Date();
		var DiferenciaFecha = FechaActual.getTime() - TiempoInicial.getTime();
		TiempoActual=(DiferenciaFecha / 1000);
		Minutos=Math.floor(TiempoActual / 60)
		Segundos = Math.floor(TiempoActual % 60)
		txtTiempo.innerHTML=Minutos + " min. " + Segundos + " seg."
		  if(Minutos==TiempoDuracion)
	   			{
	   				alert("Su Tiempo para responder a la Evaluación HA FINALIZADO\n Esta ventana se cerrará y los cambios no guardados se perderán.")
	   				top.location.href="abrirevaluacion.asp?accion=terminarencuesta"
	   			}
		   else
		   		{setTimeout("ActualizarTiempo()",1000)}
	  }
}

function Empezar(TiempoDuracion) {
   tEmpezar   = new Date();
   txtTiempo.innerHTML = "0";
   InicioTiempo  = setTimeout("ActualizarTiempo("+ TiempoDuracion +")", 1000);
}

function PararTiempo()
{
  clearInterval(InicioTiempo)
}

function Alerta()
{
	var Confirmar=confirm("Está seguro que desea Guardar su respuesta y pasar a la siguiente pregunta. \n Recuerde que al aceptar este mensaje, ya no podrá regresar ni modificar las respuestas");
	if(Confirmar==true)
		{frmPreguntas.submit}
	else{
		enfoquepregunta()
		return false;
	}
}

function mensajecontrol()
{
     if(frmPreguntas.descripcionrpta.value=="<Escriba aquí su respuesta>")
	{
		frmPreguntas.descripcionrpta.value==""
	}
}

function enfoquepregunta()
{
	for (i=0; i<frmPreguntas.elements.length; i++)
	{
		var Control=frmPreguntas.elements[i]

		if (Control.type=="text" || Control.type=="textarea")
		{
		    Control.focus()
		    return	
	    	}
	}

}


function validarTodaPregunta(formulario)
{
var totalRptas=0
var totalPgtas=document.all.cIdPregunta.length

	for(var i=0;i<totalPgtas;i++){
	    var Control=document.getElementById("descripcionrpta" + formulario.cIdPregunta[i].value)
	    
	        /*Validar las cajas de Texto si están vacías*/
	        if(Control.type=="text" || Control.type=="textarea"){					
				if (Control.value=="")
					   {totalRptas=eval(totalRptas)-1}
			        else
					   {totalRptas=eval(totalRptas)+1}
			}
        	/*Validar los check/option*/
			if(Control.type=="radio" || Control.type=="checkbox"){
			  	totalRptas=totalRptas+VerificarMarca(Control.name)
			}	
	}
	
	totalRptas=Math.abs(totalRptas)

  	if(totalRptas<totalPgtas){
		anteriorPgta=""
		alert("Por favor debe responder a todas las preguntas formuladas")
		return(false)
 	}
	else{
	  	return(true)
	}
}

function VerificarMarca(ctrl)
{
	var estado=0
	var arrOpt=document.all.item(ctrl)
	
	if (arrOpt.length==undefined){
		if (arrOpt.checked==true)
			{estado=1}
	}
	else{
	    for(var i=0;i<arrOpt.length;i++){
			//if (document.all.item(ctrl,i).checked==true){
			if (arrOpt[i].checked==true){
				estado=1
				break
			}
	    }
	}
	
	return(estado)
}