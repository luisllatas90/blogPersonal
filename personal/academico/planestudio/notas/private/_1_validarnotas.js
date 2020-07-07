/*
============================================================================================
	CVUSAT
	Fecha de Creación: 10/11/2005
	Fecha de Modificación: 08/11/2006
	Creador: Gerardo Chunga Chinguel
	Obs: Realiza las validaciones y procedimientos para el módulo de notas
============================================================================================
*/

var Mensaje= new Array(10);
Mensaje[0] = "La nota no debe ser mayor a 20"
Mensaje[1] = "¿RECUERDE QUE EL LLENADO DE NOTAS SE REALIZARÁ EN UN SOLO ACTO \nNO SE PERMITIRÁ LA ACTUALIZACIÓN DE NOTAS A TRAVÉS DE ESTA PAGINA\n\n¿ESTÁ SEGURO DE GUARDAR SU REGISTRO DE NOTAS?"
Mensaje[2] = "¿Está completamente seguro que desea Habilitar, el llenado de Registro de Notas?"
Mensaje[3] = "¿Está completamente seguro que desea GUARDAR LA NUEVA NOTA?"
Mensaje[4] = "No se han encontrado estudiantes matriculados en la asignatura seleccionada"
Mensaje[5] = "No se han encontrado sucesos realizados en la asignatura seleccionada"

var fila=0

/*Usado en miscursos.asp y todocurso.asp*/
function HabilitarRegistro(nivel)
{
	//Asignar a variable el codigo_cup elegido
	fila=event.srcElement.parentElement
	
	//Habilitar botones
	document.all.cmdAbrir.disabled=false

	if (document.all.cmdAbrir2!=undefined)
		{
			document.all.cmdAbrir2.disabled=false
		}


	document.all.cmdDescargar.disabled=false
	document.all.cmdBitacora.disabled=false

	//Habilitar botones para autorización de llenado de notas
	if (nivel>0){
		if (fila.codigo_aut==0 && fila.estadonota_cup!="P" && document.all.cmdAutorizar!=undefined){
			document.all.cmdAutorizar.style.display=""
			document.all.cmdQuitar.style.display="none"	
		}
		else{
			if (fila.codigo_aut>0 && fila.estadonota_cup!="P" && document.all.cmdAutorizar!=undefined){
				document.all.cmdQuitar.style.display=""
				document.all.cmdAutorizar.style.display="none"
			}
		}
	}

	SeleccionarFila()
}

/*Usado en miscursos.asp y todocurso.asp*/
function AbrirRegistro(modo,cd,nd,nivel)
{
	
	switch (modo){
		case "A": //Abrir registro de notas
			if (fila.cells[6].innerText=="0"){
				alert(Mensaje[4])
			}
			else{
				mensaje.innerHTML="<b>&nbsp;Espere un momento por favor...</b>"
				var nombre_cur=fila.cells[2].innerText + "(Grupo " + fila.cells[3].innerText + ")"
				location.href="../administrarconsultar/lstalumnosmatriculados.asp?codigo_cac=" + cbocodigo_cac.value + "&codigo_per=" + cd + "&nombre_per=" + nd + "&codigo_cup=" + fila.codigo_cup + "&nombre_cur=" + nombre_cur + "&nivel="+ nivel
			}
			break
	
		case "I": //Abrir registro de notas
			if (fila.cells[6].innerText=="0"){
				alert(Mensaje[4])
			}
			else{
				mensaje.innerHTML="<b>&nbsp;Espere un momento por favor...</b>"
				var nombre_cur=fila.cells[2].innerText + "(Grupo " + fila.cells[3].innerText + ")"
				location.href="../../expediente/subirarchivos.asp?codigo_cac=" + cbocodigo_cac.value + "&codigo_per=" + cd + "&nombre_per=" + nd + "&codigo_cup=" + fila.codigo_cup + "&nombre_cur=" + nombre_cur + "&nivel="+ nivel
			}
			break

		
		case "D": //Descargar registro de notas
			if (fila.cells[6].innerText=="0"){
				alert(Mensaje[4])
			}
			else{
				location.href="../administrarconsultar/rpteregistro.asp?codigo_cac=" + cbocodigo_cac.value + "&descripcion_cac=" + cbocodigo_cac.options[cbocodigo_cac.selectedIndex].text + "&codigo_per=" + cd + "&nombre_per=" + nd + "&codigo_cup=" + fila.codigo_cup + "&identificador_cur=" + fila.cells[1].innerText + "&nombre_cur=" + fila.cells[2].innerText + "&grupohor_cur=" + fila.cells[3].innerText + "&ciclo_cur=" + fila.cells[4].innerText
			}
			break

		case "B": //Bitácora de registro de notas
			if (fila.estadonota_cup!="P"){			
				AbrirPopUp("../administrarconsultar/bitacoranotas.asp?codigo_cup=" + fila.codigo_cup,"400","600","yes","yes","yes")
			}
			else{
				alert(Mensaje[5])
			}
			break
		case "V": //Permite ver la carga del profesor seleccionado
			location.href="todocurso.asp?codigo_cac=" + cbocodigo_cac.value + "&codigo_per=" + cbocodigo_per.value + "&nombre_per=" + cbocodigo_per.options(cbocodigo_per.selectedIndex).text
			break
	}
	
}

/*Usado en todocurso.asp*/
function AbrirAutorizacionNota(tipo)
{
	if (tipo=='A'){
		var cc=fila.cells[2].innerText
		var nc=fila.cells[3].innerText
	
		AbrirPopUp("frmautorizarpermiso.asp?codigo_cup=" + fila.codigo_cup + "&codigo_cur=" + cc + "&nombre_cur=" + nc,"250","530")
	}
	else{
		AbrirPopUp("../administrarconsultar/procesar.asp?accion=AutorizarRegNotas&modo=D&codigo_cup=" + fila.codigo_cup,"20","20")
	}
}

/*Usado en lstalumnosmatriculados.asp  para solo la informacion del estudiante*/
function VerInformacionEstudiante(codigouniver_alu, codigo_alu)
{
	AbrirPopUp("../../estudiante/misdatos.asp?codigouniver_alu=" + codigouniver_alu + "&codigo_alu=" + codigo_alu ,"250","750","no","no","yes")
}

/*Usado en lstalumnosmatriculados.asp*/
function AbrirHistorial(codigouniver_alu)
{
	AbrirPopUp("../../clsbuscaralumno.asp?codigouniver_alu=" + codigouniver_alu + "&pagina=estudiante/historial2.asp","450","750","no","no","yes")
}

/*Usado en lstalumnosmatriculados.asp*/
function AbrirModificarNota(notaminima,codigo_dma)
{
	fila=event.srcElement.parentElement.parentElement
		
	var codigouniver_alu=fila.cells[2].innerText
	var alumno=fila.cells[3].innerText
	var nota=fila.cells[4].innerHTML
	var condicion=fila.cells[5].innerHTML
	var codigo_aut=document.all.txtcodigo_aut.value
	
	AbrirPopUp("frmmodificarnota.asp?codigo_aut=" + codigo_aut + "&codigo_dma=" + codigo_dma + "&codigouniver_alu=" + codigouniver_alu + "&alumno=" + alumno + "&nota=" + nota + "&condicion=" + condicion + "&notaminima_cac=" + notaminima,"230","530")
}

/*Usado en lstalumnosmatriculados.asp*/
function validarnota(ctrl,notamin,condicion)
{
	var notaActual=parseFloat(ctrl.value)
	var notaMin=parseFloat(notamin)

	if(notaActual>20){
		alert(Mensaje[0])
		ctrl.focus()
		ctrl.value=0
		return(false)		
	}

	if (notaActual>notaMin){
		ctrl.className="azul"
		condicion.innerHTML="Aprobado"
		condicion.className="Aprobado"
	}
	else{
		ctrl.className="rojo"
		condicion.innerHTML="Desaprobado"
		condicion.className="Desaprobado"
	}
}

/*Usado en lstalumnosmatriculados.asp*/
function EnviarNotaNueva()
{
	if (confirm(Mensaje[1])==true){
		document.all.cmdGuardar.disabled=true
		mensaje.innerHTML="<b>&nbsp;Espere un momento por favor...</b>"
		frmRegistro.submit()		
	}
}

/*Usado en frmmodificarnota.asp*/
function EnviarNotaModificada(codigo_dma,codigo_aut,notaminima_cac)
{
	if (txtmotivo_bin.value.length<4){
		alert("Por favor especifique el motivo del cambio de nota")
		txtmotivo_bin.focus()
		return(false)
	}
	
	if (txtnotafinal_bin.value=="")
		{txtnotafinal_bin.value=0}
	
	if (confirm(Mensaje[3])==true){
		mensaje.innerHTML="<b>&nbsp;Espere un momento por favor...</b>"
		location.href="procesar.asp?accion=modificarnota&codigo_aut=" + codigo_aut + "&codigo_dma=" + codigo_dma + "&notafinal_bin=" + txtnotafinal_bin.value + "&motivo_bin=" + txtmotivo_bin.value + "&notaminima_cac=" + notaminima_cac
	}
}

/*Usado en todocurso.asp*/
function EnviarAutorizacionNotas(cp)
{
	if (txtmotivo_aut.value.length<3){
		alert("Debe ingresar el motivo de la autorización para el llenado de notas")
		txtmotivo_aut.focus()
		return(false)
	}

	location.href="../administrarconsultar/procesar.asp?accion=AutorizarRegNotas&modo=A&codigo_cup=" + cp + "&fechaini_aut=" + txtfechaini_aut.value + "&fechafin_aut=" + txtfechafin_aut.value + "&motivo_aut=" + txtmotivo_aut.value
}

function ActualizarListaRegistros()
{
	var nca=cboCiclo.options[cboCiclo.selectedIndex].text
	var ca=cboCiclo.value
	var da=cboDpto.value
	var nda=cboDpto.options[cboDpto.selectedIndex].text
	var estado=cboEstado.value

	AbrirMensaje('../../../../images/')
	location.href="frmimprimirregistros.asp?codigo_cac=" + ca + "&descripcion_cac=" + nca + "&codigo_dac=" + da + "&nombre_dac=" + nda + "&estadonota_cup=" + estado
}

/*Usado en lstalumnosmatriculados.asp*/
function OcultarRetirados()
{
  var activo=document.all.chkretirados
  if (activo.checked==true)
	{document.all.textoOcultar.innerHTML="Mostrar estudiantes retirados"}
  else
	{document.all.textoOcultar.innerHTML="Ocultar estudiantes retirados"}

  //Asignar valores de tabla de registro
  var ArrFilas=document.all.tlbRegistro.getElementsByTagName('tr')

  //Verificar filas de registro
  if (ArrFilas.length!=undefined){
	for (var i = 0; i < ArrFilas.length; i++){
		var filaActual=ArrFilas[i]
		filaActual.style.display=""
		if (filaActual.className=="cursoR" && activo.checked==true){
			filaActual.style.display="none"
		}
    }
  }
}

//Buscar el estudiante por código y refrescar página principal
function BuscarNotasEstudiante()
{
	var codigo=document.all.codigouniversitario
	if (codigo.value==""){
		alert("Debe ingresar el código universitario del estudiante")
		return(false)
		codigo.focus()
	}
	location.href="../administrarconsultar/procesar.asp?accion=buscarestudiante&codigouniver_alu=" + codigo.value
}

