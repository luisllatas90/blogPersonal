/*
================================================================================================
	CVUSAT
	Fecha de Creación: 13/08/2015
	Fecha de Modificación: 
	Creador: Edgard Peña Nuñez
	Obs: Permite validar que el alumno no adelante cursos de ciclos inferiores
================================================================================================
*/

//Declarar array de mensajes de alerta en matrícula
var arrMensajeCiclo= new Array(5);
arrMensajeCiclo[0] = "UD. NO PUEDE RETIRARSE de cursos de ciclos inferiores, ya que es Obligatorio llevarlos.\n Para cambios de grupo horario puede realizarlo en la Direccion de su Escuela Profesional"
arrMensajeCiclo[1] ="Ud. NO PUEDE ADELANTAR ASIGNATURAS DE CICLOS SUPERIORES,\nPRIMERO DEBE ELEGIR ASIGNATURAS DE CICLO INFERIORES"


function ValidarRetiroCursoInferior(codigo_dma)
{
    //var curso=document.all.hdcursos //Recorre solo las cabeceras de cursos
    //var idCheck = document.getElementById("hd" + codigo_dma);
    
    var idCheck = $("input:hidden[id=hd" + codigo_dma+"]")
    var sw = 1;
    $("input:hidden[name=hdcursos]").each(function() {
        if (parseInt($(this).attr("electivo")) == 0 && parseInt($(this).attr("ciclo")) > 0 && parseInt($(this).val()) > 0) {
            if (parseInt($(idCheck).attr("ciclo")) < parseInt($(this).attr("ciclo"))) {
                alert(arrMensajeCiclo[0]);
                sw = 0;
                return false;
            }
        }
    });

   if (sw==0)
    return 0;
   else    
    return 1;
}
