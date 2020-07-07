/*
================================================================================================
	CVUSAT
	Fecha de Creaci�n: 16/08/2006
	Fecha de Modificaci�n: 16/08/2006
	Creador: Gerardo Chunga Chinguel
	Obs: Permite validar que el alumno no adelante cursos de ciclos inferiores
================================================================================================
*/

//Declarar array de mensajes de alerta en matr�cula
var ArrMensajes= new Array(2);
ArrMensajes[0] = "Debe elegir las asignaturas que Ud. se matricular�";
ArrMensajes[1] = "Ud. s�lo puede elegir un Grupo de Horario de la asignatura Programada";
ArrMensajes[2] = "No se puede matricular en esta Asignatura, porque existe un cruce de Horario con el Curso:\n\n"
ArrMensajes[3] = "No se puede matricular en esta Asignatura, porque existen cruzes de horarios\n con las siguientes asignaturas:\n\n"
ArrMensajes[4] ="�Est� completamente seguro que desea GUARDAR la matricula?\n\n Recuerde que para cualquier modificaci�n de Agregado o Retiro de Asignaturas\n lo puede hacer habiendo pagado por el Concepto de Matr�cula y despu�s de 48 horas despu�s de realizado la matr�cula"
ArrMensajes[5] ="�Est� seguro que desea Retirarse de las asignaturas seleccionadas?"
ArrMensajes[6] ="�Est� seguro que desea Restablecer los retiros de asignaturas seleccionadas?"
ArrMensajes[7] ="RECUERDE:\n- ES OBLIGATORIO matricularse en al menos una ASIGNATURA COMPLEMENTARIA\n     (Hacer clic en la pesta�a Cursos Complementarios)\n- Despu�s de realizada la Matr�cula, NO PUEDE RETIRARSE de una asignatura complementaria"
ArrMensajes[8] ="UD. NO PUEDE RETIRARSE de cursos de ciclos inferiores, ya que es Obligatorio llevarlos.\n Si Ud. desea hacer cambio de grupo, debe acercarse a la Oficina de su Direcci�n de Escuela a realizar dicho proceso"

function BloquearRetiro(idCheck,fila)
{
	var curso=frmFicha.chk
	var total=0
	
	/*Solamente verificar en los cursos no marcados*/
	if (curso.length!=undefined){
		for (i=0;i<curso.length;i++){
			var Control=curso[i]
		     
		     /*Veriricar s�lo en check marcados y cursos obligatorios y electivos*/
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