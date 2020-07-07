function calcularvalores(control,ctotal)
  {
	var total=0;
	var valctrl=0;
	for (i=0; i<control.length; i++)
	{
		if (control[i].value!="")
		{
			 valctrl=parseFloat(control[i].value)
	    		 if (isNaN(valctrl))
	    			{valctrl=0}
	    		  total+=eval(valctrl)
		} 
	}

	
	ctotal.value=total

	//ctotal.value=math.round(total)


  }

function calcularsubtotal(id1,id2,precio)
{
	id2.value=id1.value * precio.value

	//id2.value=math.round(id2.value*100)/100
}

function recalcular()
{
	var cantidad=form1.txtCantidad
	var subtotal=form1.txtSubTotal

	for (i=0; i<cantidad.length; i++)
	{
		if (cantidad[i].value!=0 || subtotal[i].value!=0){
		   subtotal[i].value=parseFloat(form1.txtPrecio.value) * parseFloat(cantidad[i].value)
		}

	}
	calcularvalores(cantidad,form1.txtCantGlobal)
	calcularvalores(subtotal,form1.txtSubTotalGlobal)
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

function VerificaSeleccion(chkmaestro,chk)
{
	if (chkmaestro.checked == true)
		{SeleccionarTodos(formulario)}
	else
		{QuitarTodos(formulario)}	
}

//RESALTAR FILA SELECCIONADA
function Resaltar(op,fila)
{
if(op==1)
	{fila.bgColor="#FBF5D2"}
else
	{fila.bgColor="#FFFFFF"}   
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


function MostrarTabla(control,img)
{
  if(control.style.display=="none")
    {
		control.style.display=""
		img.src="../images/menos.gif"
    }
    else
    {
		control.style.display="none"
		img.src="../images/mas.gif"
    }
}


function MostrarTabla2(control)
{
  if(control.style.display=="none")
    {
		control.style.display=""
	
    }
    else
    {
		control.style.display="none"
	
    }
}


function AbrirPopUp(pagina,alto,ancho,ajustable,bestado,barras,ubArr,UbIzq)
{
   var ventana=window.open(pagina,"popup","height="+alto+",width="+ancho+",statusbar="+bestado+",scrollbars="+barras+",top=" + ubArr + ",left=" +  UbIzq  + ",resizable="+ajustable+",toolbar=no,menubar=no");
   ventana.location.href=pagina
   ventana=null
}


function imgover(imgname){
imgname.src = "../images/arrow.gif"
}
function imgout(imgname){
imgname.src = "../images/blank.gif"
}

function validarnumero()
{
//permite numero y puntos, Borrar tambien
	if (event.keyCode < 45 || event.keyCode > 57)
		{event.returnValue = false}
}

function validarSoloNumero()
{
	if (event.keyCode!=46 && event.keyCode!=8 )
	{	 
		if (event.keyCode < 48 || event.keyCode > 57  )
			{event.returnValue = false}
	}
}

function noescribir()
{
	

		event.keyCode = 0;
		event.returnValue = false

      
}

