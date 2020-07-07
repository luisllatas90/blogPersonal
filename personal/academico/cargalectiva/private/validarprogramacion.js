/*
================================================================================================
	CVUSAT
	Fecha de Creación: 16/11/2005
	Fecha de Modificación: 19/01/2007
	Creador: Gerardo Chunga Chinguel
	Obs: Realiza las validaciones y procedimientos para el módulo de Programación de Cursos
================================================================================================
*/

var Mensaje= new Array(10);
Mensaje[0] = "No se puede ELIMINAR un grupo horario que tenga estudiantes inscritos"
Mensaje[1] = "¿Está seguro que desea Eliminar el curso programado seleccionado\nRecuerde que si hay alumnos matriculados no podrá eliminarlo?"
Mensaje[2] = "¿Está completamente seguro que desea Programar las asignaturas seleccionadas y sus equivalentes marcados?"

var total=0
var codigo_cup=0
var codigo_pes=0
var codigo_cur=0

/*
Usado en frmnuevaprogramacion.asp
*/
function MarcarCurso()
{
	var filaActual=event.srcElement.parentElement
	cmdGuardar.disabled=true
	
	if (filaActual.className=="Selected"){
		filaActual.className="SelOff"
		total=total-1
	}
	else{
		if (cbocodigo_pes.value!="-2"){
			filaActual.className="Selected"
			total=total+1
		}
		else{
			alert("Debe elegir un plan de estudio")
		}
	}
	
	if (total!="" && document.all.cmdGuardar!=undefined){
		cmdGuardar.disabled=false
	}
	seleccionadas.innerHTML="Elegidos: " + total
}

function FiltrarCursos()
{
	var ArrFilas = document.all.tblcursoprogramado.getElementsByTagName('tr')

  	if (ArrFilas.length!=undefined){
    	for (var i=0;i<ArrFilas.length; i++){
			var filaActual=ArrFilas[i]

			filaActual.style.display="none"
			
			if (cboFiltro.value=="T")
				{filaActual.style.display=""}
				
			if (cboFiltro.value=="N" && filaActual.cells[9].innerText=="")
				{filaActual.style.display=""}

			if (cboFiltro.value=="P" && filaActual.cells[9].innerText!="")
				{filaActual.style.display=""}
   		}
   	}
}

function EnviarMarcas()
{
	var ArrFilas = document.all.tblcursoprogramado.getElementsByTagName('tr')
	var arrcodigo_cur=""
	var arrcodigo_pes=""

	if (ArrFilas.length==undefined){
		arrcodigo_cur=ArrFilas.codigo_cur
		arrcodigo_pes=ArrFilas.codigo_pes
	}
	else{
    	for (var i=0;i<ArrFilas.length; i++){
			var filaActual=ArrFilas[i]

			if (filaActual.className=="Selected"){
				arrcodigo_cur+=filaActual.codigo_cur + ","
				arrcodigo_pes+=filaActual.codigo_pes + ","
			}
   		}
   	}
   	
   	//Enviar formulario según las marcas
  	frmPlanCurso.arrCursosMarcados.value=arrcodigo_cur
  	frmPlanCurso.arrPlanesMarcados.value=arrcodigo_pes
 	frmPlanCurso.submit()
}

/*
Usado en frmconfirmarprogramacion.asp
*/
function OcultarEquivalencias(estado)
{
	var ArrFilas=document.all.trEquivalencias
	
	if (ArrFilas!=null){
		if (ArrFilas.length==undefined){
			ArrFilas.style.display=estado
		}
		else{
	    	for (var i=0;i<ArrFilas.length; i++){
				ArrFilas[i].style.display=estado
	   		}
	   	}
	}
}

/*
Usado en frmconfirmarprogramacion.asp
*/
function GuardarProgramacion()
{
	if (confirm(Mensaje[2])==true){
		frmlistacursos.submit()
	}
}

/*
Usado en frmmodificaprogramacion.asp y frmnuevaprogramacion.asp
*/
function BuscarProgramacion(modo)
{
	var pagina=""
	
	if (modo=="N"){
		if (cbocodigo_pes.value==-2){
			alert("Debe seleccionar un Plan de Estudios")
		}
		else{
			pagina="../academico/cargalectiva/administrarprogramacion/frmnuevaprogramacion.asp?codigo_cac=" + cbocodigo_cac.value + "&codigo_pes=" + cbocodigo_pes.value
		}
	}

	if (modo=="M"){
		if (cbocodigo_cpf.value==-2){
			alert("Debe seleccionar una Escuela Profesional")
		}
		else{
			pagina="../academico/cargalectiva/administrarprogramacion/frmmodificaprogramacion.asp?codigo_cac=" + cbocodigo_cac.value + "&codigo_cpf=" + cbocodigo_cpf.value
		}
	}	
	
	if (pagina!=""){
		window.location.href="../../../aplicacionweb2/cargando.asp?rutapagina=" + pagina
	}
}

function OcultarCursos()
{
	if (cbocodigo_pes.value==-2 && trCursos!=undefined){
		trCursos.style.display="none"
	}
}

/*
Usado en frmmodificaprogramacion.asp
*/
function MarcarCursoProgramado()
{
	var fila=event.srcElement.parentElement
	codigo_cur=fila.codigo_cur
	codigo_pes=fila.codigo_pes 
	codigo_cup=fila.codigo_cup
	
	SeleccionarFila()
	
	//var pagina="../academico/cargalectiva/administrarprogramacion/detallecursoprogramado.asp?codigo_cac=" + cbocodigo_cac.value + "&codigo_cpf=" + cbocodigo_cpf.value + "&codigo_cur=" + fila.codigo_cur + "&codigo_pes=" + fila.codigo_pes + "&codigo_cup=" + fila.codigo_cup
	var pagina="detallecursoprogramado.asp?codigo_cac=" + cbocodigo_cac.value + "&codigo_cpf=" + cbocodigo_cpf.value + "&codigo_cur=" + fila.codigo_cur + "&codigo_pes=" + fila.codigo_pes + "&codigo_cup=" + fila.codigo_cup
	
	//Habilitar botón si no está bloqueado el ciclo para programar	
	if (cmdAgregar.disabled!=undefined){
		cmdAgregar.disabled=false
		cmdModificar.disabled=false
	}
	mensajedetalle.style.display="none"

	fradetalle.document.location.href=pagina//"../../../aplicacionweb2/cargando.asp?rutapagina=" + pagina
}

/*
Usado en detallecursoprogramado.asp
*/
function AbrirGrupo(modo,ca,pl,cu,cup)
{
	var pagina=""
	switch(modo)
	{
		case "A": //Añadir nuevo grupo horario		
				location.href="frmconfirmarprogramacion.asp?codigo_cac=" + cbocodigo_cac.value + "&codigo_pes=" + codigo_pes + "&codigo_cur=" + codigo_cur
				break	
	
		case "M": //modificar grupo horario
				pagina="frmmodificargrupohorario.asp?codigo_cup=" + codigo_cup
				AbrirPopUp(pagina,"400","550")
				break
	
		case "E": //eliminar grupo horario
			var fila=event.srcElement.parentElement.parentElement
			if (eval(fila.cells[3].innerText)>0){
				alert(Mensaje[0])
			}
			else{
				var Confirmar=confirm(Mensaje[1]);
				if(Confirmar==true){
					pagina="procesar.asp?accion=eliminarcursoprogramado&codigo_cup=" + cup + "&codigo_cac=" + ca + "&codigo_pes=" + pl + "&codigo_cur=" + cu + "&codigo_cupPadre=" + txtcodigo_cupPadre.value
					document.location.href=pagina
				}
			}
			break
	}
}

function ValidarGrupoHorario()
{
	if (frmlistacursos.txtGrupos.value.length<0){
		alert("Debe asignar un nombre al Grupo horario")
		frmlistacursos.txtGrupos.focus()
		retur(false)
	}
	
	if (frmlistacursos.txtVacantes.value.length<0){
		alert("Debe especificar el número de vacantes de Grupo horario")
		frmlistacursos.txtVacantes.focus()
		retur(false)
	}	

	frmlistacursos.submit()
}

function HabilitarVacantes(obj)
{
	if(obj.value==0){
		trVacantes.style.display="none"
	}
	else{
		trVacantes.style.display=""
	}
}