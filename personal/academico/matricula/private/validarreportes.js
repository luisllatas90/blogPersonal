/*
==========================================================================================
  CV-USAT
  Fecha de Creaci�n: 19/03/2006
  Fecha de Modificaci�n: 01/06/2006
  Creado por	: Gerardo Chunga Chinguel
  Observaci�n	: Validar reportes de matr�cula
==========================================================================================
*/

var Mensaje= new Array(10);
Mensaje[0] = "Debe elegir los cursos que Ud. se matricular�";
Mensaje[1] = "Ud. s�lo puede elegir un Grupo de Horario del Curso a Matricularse";

function ConsultarAlumnoCargos()
{
	/*
	var nombre_cpf=cboescuela.options[cboescuela.selectedIndex].text

	if (cboescuela.value=="-2"){
		alert("Especifique la Escuela Profesional")
		cboescuela.focus()
		return(false)
	}
	*/

	location.href="rptealumnoscargos.asp?resultado=S&codigo_sco=" + cbocodigo_sco.value + "&codigo_cac=" + cbociclo.value + "&estado_deu=" + cboestado_deu.value
}

function AbrirHistorial(codigouniver_alu)
{
	AbrirPopUp("../../clsbuscaralumno.asp?codigouniver_alu=" + codigouniver_alu + "&pagina=matricula/consultapublica/historial.asp","450","750","no","no","yes")
}

