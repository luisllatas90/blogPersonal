/*
================================================================================================
	CVUSAT
	Fecha de Creación: 16/08/2006
	Fecha de Modificación: 16/08/2006
	Creador: Gerardo Chunga Chinguel
	Obs: Permite validar que el alumno no adelante cursos de ciclos inferiores
================================================================================================
*/

//Declarar array de mensajes de alerta en matrícula
var ArrMensajes= new Array(2);
ArrMensajes[0] = "Debe elegir las asignaturas que Ud. se matriculará";
ArrMensajes[1] = "Ud. sólo puede elegir un Grupo de Horario de la asignatura Programada";
ArrMensajes[2] = "No se puede matricular en esta Asignatura, porque existe un cruce de Horario con el Curso:\n\n"
ArrMensajes[3] = "No se puede matricular en esta Asignatura, porque existen cruzes de horarios\n con las siguientes asignaturas:\n\n"
ArrMensajes[4] ="¿Está completamente seguro que desea GUARDAR la matricula?\n\n Recuerde que para cualquier modificación de Agregado o Retiro de Asignaturas\n lo puede hacer habiendo pagado por el Concepto de Matrícula y después de 48 horas después de realizado la matrícula"
ArrMensajes[5] ="¿Está seguro que desea Retirarse de las asignaturas seleccionadas?"
ArrMensajes[6] ="¿Está seguro que desea Restablecer los retiros de asignaturas seleccionadas?"
ArrMensajes[7] ="RECUERDE:\n- ES OBLIGATORIO matricularse en al menos una ASIGNATURA COMPLEMENTARIA\n     (Hacer clic en la pestaña Cursos Complementarios)\n- Después de realizada la Matrícula, NO PUEDE RETIRARSE de una asignatura complementaria"
ArrMensajes[8] ="UD. NO PUEDE RETIRARSE de cursos de ciclos inferiores, ya que es Obligatorio llevarlos.\n Si Ud. desea hacer cambio de grupo, debe acercarse a la Oficina de su Dirección de Escuela a realizar dicho proceso"

function BloquearRetiro(idCheck,fila)
{
	var curso=frmFicha.chk
	var total=0
	
	/*Solamente verificar en los cursos no marcados*/
	if (curso.length!=undefined){
		for (i=0;i<curso.length;i++){
			var Control=curso[i]
		     
		     /*Veriricar sólo en check marcados y cursos obligatorios y electivos*/
		     if (Control.electivo=="0"){//Es curso obligatorio
			if (Control.checked==false && Control.ciclo!="0" && Control.cr!="0" && Control.estado_dma!="R" && Control.value!=idCheck.value){
				if (eval(Control.ciclo)>eval(idCheck.ciclo)){
					total=1
				}
			}
		     }
		}
	}

	/*Desactivar check marcado*/
	if (total>0){
		idCheck.checked=false
		document.all.cmdRetirar.disabled=true
		alert(ArrMensajes[8])
	}
	else{
		VerificaCheckMarcados(document.all.chk,document.all.cmdRetirar)
		pintafilamarcada(fila,idCheck,'M')
	}
}