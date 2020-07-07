/*
============================================================================================
	CVUSAT
	Fecha de Creación: 23/02/2006
	Fecha de Modificación: 07/04/2006
	Creador: Gerardo Chunga Chinguel
	Obs: Realiza las validaciones y procedimientos para el módulo de Carga Académica
============================================================================================
*/

var Mensaje= new Array(10);
Mensaje[0] = "Está seguro que desea Eliminar la Carga Académica del docente seleccionado"
Mensaje[1] = "¿Está completamente seguro que desea guardar los cambios?"
Mensaje[2] = "Por favor especifique el total de horas"
Mensaje[3] = "¿Está completamente seguro que sea AGRUPAR los cursos seleccionados\nRecuerde que esta acción solo se Aplica en cursos afines y de planes de estudio diferentes, pero de la misma Escuela Profesional ?"
Mensaje[4] = "¿Está completamente seguro que sea DESAGRUPAR los cursos marcados?\nRecuerde que al DESAGRUPAR los cursos marcados, debe volver a asignar Carga Académica y Horarios"
Mensaje[5] = "¿Está completamente seguro que desea Eliminar a los docentes seleccionados del Departamenteo adscrito?"

var termino	= "%";
var txtcodigo_cup=0
var th=0
var mat = 0

var txtnombre_Cur=""     /* agregado 02/10/2011 */


function ActualizarListaCarga(pagina)
{
	location.href=pagina + "?modo=R&codigo_cac=" + cboCiclo.value + "&codigo_dac=" + cboDpto.value
}

function AsignarHorario(cp,ca,cd)
{
	AbrirPopUp("frmasignarhorario.asp?codigo_cup=" + cp + "&codigo_cac=" + ca + "&codigo_dac=" + cd,"400","700")
}

function MostrarDatosCarga(ca,cd,pagina)
{
	if (txtelegido.value!=""){
		var fila=document.getElementById(txtelegido.value)
		if (fila!=undefined){
			var cp =fila.cells[1].innerText
			fradetalle.location.href=pagina + "?codigo_cup=" + cp + "&codigo_cac=" + ca + "&codigo_dac=" + cd
		}
	}
}

function ActualizarHorasCargaAcademica(ca,cp)
{
	if (confirm(Mensaje[1])==true){
		cmdGuardar.disabled=true
		var aula=fradetalle.document.all.totalhorasaula
		var asesoria=fradetalle.document.all.totalhorasasesoria

		var arrCP=""
		var arrAU=""
		var arrAS=""

		//Validar si existe sólo un alumno
		if (aula.length==undefined){
			arrCP=aula.codigo_cup
			arrAU=aula.value
			arrAS=asesoria.value
		}
		//Enviar datos de varios alumnos
		else{
			for(i=0;i<aula.length;i++)
			{
				arrCP+=aula[i].codigo_cup + ","
				arrAU+=aula[i].value + ","
				arrAS+=asesoria[i].value + ","
			}
		}
	
		fradetalle.location.href="procesar.asp?accion=ActualizarHorasCargaAcademica&codigo_cac=" + ca + "&codigo_per=" + cp + "&cursosprogramados=" + arrCP + "&totalhorasaula=" + arrAU + "&totalhorasasesoria=" + arrAS
		cmdGuardar.disabled=false
	}
}
function validarhorascarga(ctrl)
{
	var hrs=ctrl.value
	
	if(ctrl.value==""){
		alert(Mensaje[2])
		ctrl.focus()
		return(false)		
	}	
}

function ValidarEliminarDpto()
{
	if(confirm(Mensaje[5])==true){
		frmDpto.submit()
	}
	else{
		return(false)
	}
}

/*
Usadas en frmasignarcargaporescuela.asp para buscar cursos programados
*/

function ConsultarHorarios(modo, codigo_cup)
{
	var pagina="detallecarga.asp"

	if (modo=="H"){
		pagina="../../horarios/tblhorario.asp"
	}
	if (modo=="C"){
	    txtcodigo_cup = codigo_cup
	    
		SeleccionarFila()
		th=event.srcElement.parentElement.cells[5].innerText
		mat=event.srcElement.parentElement.cells[8].innerText
		ResaltarPestana2('0','','')
	}
	fraHorario.location.href = pagina + "?codigo_cup=" + txtcodigo_cup + "&codigo_cac=" + cbocodigo_cac.value + "&th=" + th + "&mat=" + mat + "&codigo_cpf=" + cbocodigo_cpf.value

}

function MostrarAsigProfesor(codigo_cup)
{
	txtcodigo_cup=codigo_cup
	var th=event.srcElement.parentElement.cells[5].innerText
	var mat=event.srcElement.parentElement.cells[8].innerText
	var pagina="detallecarga2.asp"
	SeleccionarFila()

	fraHorario.location.href=pagina + "?codigo_cup=" + txtcodigo_cup + "&codigo_cac=" + cbocodigo_cac.value + "&th=" + th + "&mat=" + mat + "&codigo_dac=" + cbocodigo_cpf.value

}

function ConsultarCursos(pag)
{
	var pagina="../academico/cargalectiva/administrarcargalectiva/" + pag + "?codigo_cac=" + cbocodigo_cac.value + "&codigo_cpf=" + cbocodigo_cpf.value + "&curso=" + termino
	window.location.href="../../aplicacionweb2/cargando.asp?rutapagina=" + pagina
}

function AbrirBusqueda(modulo)
{
	if (cbocodigo_cpf.value=="-2"){
		alert("Debe seleccionar el item correcto")
	}
	else{
	   //showModalDialog("../frmbuscarcursoprogramado.asp?pagina=" + pag,window,"dialogWidth:400px;dialogHeight:200px;status:no;help:no;center:yes");
	   location.href="frmasignarcargaporescuela.asp?codigo_cac=" + cbocodigo_cac.value + "&codigo_cpf=" + cbocodigo_cpf.value + "&mod=" + modulo
	}
}

/*
Usadas en detallecarga.asp, para asignar profesor
*/
function AbrirCarga(modo,cp,ca,cd,dac,th)
{

	if (modo=="A"){ //Modificado por mvillavicencio el 04/11/2011. Recibe y envia también el total horas del Plan en la variable dac
		location.href="frmlistadocentes.asp?codigo_cup=" + cp + "&codigo_cac=" + ca + "&codigo_dac=" + cd + "&th=" + dac
	}
	if (modo=="ACP"){
		location.href="frmlistadocentes2.asp?codigo_cup=" + cp + "&codigo_cac=" + ca + "&codigo_dac=" + cd + "&activarotroDpto_cup=" + dac
	}	
	
	if (modo=="E"){
		ConfirmarEliminar(Mensaje[0],"procesar.asp?pagina=detallecarga.asp&accion=eliminarcargaacademica&codigo_per=" + cd + "&codigo_cup=" + cp + "&codigo_cac=" + ca + "&codigo_dac=" + dac + "&th=" + th)
	}
	if (modo=="ECP"){
		ConfirmarEliminar(Mensaje[0],"procesar.asp?pagina=detallecarga2.asp&accion=eliminarcargaacademica&codigo_per=" + cd + "&codigo_cup=" + cp + "&codigo_cac=" + ca + "&codigo_dac=" + dac)
	}
	
	if (modo=="DER"){
		pagina="../administrarprogramacion/frmmodificargrupohorario.asp?tipo=C&codigo_cup=" + cp
		AbrirPopUp(pagina,"400","550")
	}
	
}