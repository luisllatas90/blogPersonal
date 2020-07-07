/*
==========================================================================================
  CV-USAT
  Fecha de Creación: 17/08/2005
  Fecha de Modificación: 08/01/2006
  Creado por	: Gerardo Chunga Chinguel
  Modificado por: Helen Reyes Hernández 25/02/2011
  Observación	: Validar Curso Programado para GRABAR en la tabla detalle matricula
==========================================================================================
*/

var arrMensajes= new Array(10);
arrMensajes[0] = "Debe elegir los cursos que Ud. se matriculará";
arrMensajes[1] = "Ud. sólo puede elegir un Grupo de Horario del Curso a Matricularse";
arrMensajes[2] = "¿Está completamente seguro que desea matricular en los cursos elegidos?"
arrMensajes[3]="¿Está completamente seguro que desea retirar la asignatura seleccionada?"
arrMensajes[4]="¿ACCIÓN IRREVERSIBLE.\nEstá completamente seguro que desea ELIMINAR la asignatura seleccionada?"

function AbrirCurso(codigo_cur)
{
	var fila=document.all.item("codigo_cur" + codigo_cur)
	var img="../../../../images/mas.gif"
			
	if (fila.length==undefined){
		if (fila.style.display=="none"){
			fila.style.display=""
			img="../../../../images/menos.gif"
		}
		else{
			fila.style.display="none"
		}
	}
	else{
		for (var i=0;i<fila.length;i++){
			var item=fila[i]
			
			if (item.style.display=="none"){
				item.style.display=""
				img="../../../../images/menos.gif"
			}
			else{
				item.style.display="none"
			}
		}
	}

	document.getElementById("img" + codigo_cur).src=img
}

function pintafilamarcada(idcheck)
{
    var fila=event.srcElement.parentElement.parentElement
    var curso=document.all.item("curso_padre" + idcheck.cc)
	var claseAnterior=curso.clase
      
    if(idcheck.checked==true){
        fila.className="SelOn"
        curso.className="SelOn"
	}
    else{
	    fila.className=""
	    curso.className=claseAnterior
    }	
}

function Actualizar(idCheck)
{
	var cursos=0
	var creditos = 0
	var totalcur = 0
	var totalcrd = 0	
	var chkcursos=frmFicha.chkcursoshabiles
    //Agregado por hreyes ---> 25/02/2011 para el calculo de cuotas
	var cantCursosMat = 0;
	var credMat = 0;
	var CreditosPension = 0;

	cantCursosMat = frmFicha.cantCursos.value
	credMat = frmFicha.credMat.value
	
	if (chkcursos.length==undefined && idCheck.checked==true){
		creditos=idCheck.value
		cursos=1
	}
	else{
		for (i=0; i<chkcursos.length;i++){
		    var Control=chkcursos[i]
			//Desactivar los otros grupos del curso
			if (Control.cc==idCheck.cc && Control.id!=idCheck.id){
				Control.disabled=idCheck.checked==true
			}
			
			if (Control.checked==true){
				creditos+=eval(Control.value)
				cursos = eval(cursos) + 1
				switch (parseInt(Control.vd)) {
				    case 1: CreditosPension += redondear(parseFloat(Control.value) * 1.3, 2)//parseFloat(Control.vd)
				        break;
				    case 0: CreditosPension += redondear(parseFloat(Control.value), 2)
				        break;
				    default: CreditosPension += redondear(parseFloat(Control.value) * 1.5, 2)//parseFloat(Control.vd)
				        break;
				}
			}
		}
	}

	if (cursos > 0 || totalcur > 0) {
		document.all.cmdAgregar.disabled=false
	}
	else{
		document.all.cmdAgregar.disabled=true
	}

	document.all.totalcrd.innerHTML=creditos
	document.all.totalcur.innerHTML=cursos
	document.all.creditosMat.innerHTML = creditos
	pintafilamarcada(idCheck)

}

function modificarmotivo(codigo_dma,tipo,  motivoactual , obsactual)
{
	var pagina='';

	// mostrar ventana con informacion respectiva
	pagina = "../matricula/administrarPP";
	window.open ("frmmotivoagregadoretiro.asp?ruta="+ pagina + "&accion=modificarmotivo&codigo_dma=" + codigo_dma + "&tipo_mar=" + tipo + "&motivoactual=" + motivoactual + "&obsactual=" + obsactual,"","height=400,width=450") ;
}

function modificarmatricula(modo,ID,dc,cp,accion)
{

	var pagina=""
	if (modo=='N'){ //Para nueva matrícula
		pagina="../academico/matricula/administrarPP/frmagregarcurso.asp?esnuevamatricula=S&accion=agregarcursomatricula&codigo_pes=" + ID
		window.location.href="../../../aplicacionweb2/cargando.asp?rutapagina=" + pagina
	}
		
	if (modo=='A'){ //Para agregado de matrícula
		pagina="../academico/matricula/administrarPP/frmagregarcurso.asp?accion=" + accion + "&codigo_cac=" + ID + "&descripcion_cac=" + dc + "&codigo_pes=" + cp
		parent.location.href="../../../aplicacionweb2/cargando.asp?rutapagina=" + pagina
	}
	
	if (modo=='R'){ //Para retiro de asinaturas
  		if (confirm(arrMensajes[3])==true){
			// mostrar informacion del retiro de la asignatura
  		    pagina = "../administrarPP";
			window.open ("frmmotivoagregadoretiro.asp?ruta="+ pagina + "&accion=retirarcursomatricula&codigo_dma=" + ID + "&tipo_mar=R&motivoactual=" ,"","height=400,width=450") ;			
			//location.href="procesarmatricula.asp?accion=retirarcursomatricula&codigo_dma=" + ID + "&estado_dma=" + modo + "&codigo_cac=" + dc
		}
	}
	
	if (modo=='E'){ //Para ELIMINAR asinaturas
  		if (confirm(arrMensajes[4])==true){
			location.href="procesarmatricula.asp?accion=eliminarcursomatricula&codigo_dma=" + ID + "&estado_dma=" + modo + "&codigo_cac=" + dc
		}
	}

}

function BuscarCursosProgramados(ca,dc)
{
	pagina="../academico/matricula/administrarPP/frmagregarcurso.asp?accion=agregarcursomatricula&codigo_pes=" + document.all.cbocodigo_pes.value + "&codigo_cac=" + ca + "&descripcion_cac=" + dc
	window.location.href="../../../aplicacionweb2/cargando.asp?rutapagina=" + pagina
}

function EnviarFichaMatricula() {

	//validar que este seleccionado algun elemento de la lista
	if (frmFicha.cbocodigo_mar!=undefined){
		if (frmFicha.cbocodigo_mar.selectedIndex==0 &&  frmFicha.txtesnuevamatricula.value !="S")
		{
			alert("seleccione motivo del agregado") ;
			return false;
		}
    }

    //Mostrar arrMensajes de confirmación
    if (confirm("Está seguro que desea agregar los cursos seleccionados a la matrícula") == true) {
        //Declarar array de propiedades del check marcado
        var arrCP = "" //Array de codigo de curso programado
        var arrVD = "" //Array de codigo de curso programado
        var chk1 = frmFicha.chkcursoshabiles
        var totalmarcados = 0

        //Recorriendo 1er Iframe Con cursos curriculares/complementarios
        if (chk1.length == undefined) {
            //alert('indefinido')
            if (chk1.checked == true) {
                arrCP += chk1.cp + ","
                arrVD += chk1.vd + ","
                //arrCP+=0 + ","
                totalmarcados = 1
            }
        }
        else {
            //alert(chk1.length)
            for (var i = 0; i < chk1.length; i++) {
                Control = chk1[i]
                if (Control.checked == true) {
                    arrCP += Control.cp + ","
                    arrVD += Control.vd + ","
                    totalmarcados = eval(totalmarcados) + 1
                }
            }
        }
        if (totalmarcados == 0) {
            alert(arrMensajes[0])
        }
        else {
            //Mostrar arrMensajes de confirmación y enviar datos
            procesararrMensajes()
            frmFicha.CursosProgramados.value = arrCP
            frmFicha.VecesDesprobados.value = arrVD
            frmFicha.submit()
        }
    }

}

function procesararrMensajes()
{
	tblFicha.style.display="none"
	tblmensaje.style.display=""
}


function redondear(cantidad, decimales) {
    var cantidad = parseFloat(cantidad);
    var decimales = parseFloat(decimales);
    decimales = (!decimales ? 2 : decimales);
    return Math.round(cantidad * Math.pow(10, decimales)) / Math.pow(10, decimales);
}