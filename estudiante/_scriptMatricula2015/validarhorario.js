//************************************************************************************
//CV-USAT
//Fecha de Creación: 25/06/2005
//Creado por	: Gerardo Chunga Chinguel
//Observación	: Da formato para mostrar el horario de los cursos matriculados y elegidos 
//************************************************************************************

var ArrAlertas= new Array(2);
ArrAlertas[0] = "No se puede matricular en esta Asignatura, porque existe cruce de Horarios:\n\n"

//Valida el ancho del Texto
function anchotexto(valor)
{
	var texto=""
	var auxiliar=new String(valor)
			    
	if (auxiliar.length==1)
		{texto="0"+valor}
	else
		{texto=valor}
	return texto
}

function convertirADia(id)
{
 var dia=""
	switch(id)
	{
		case "LU":
			dia="Lunes"
			break
		case "MA":
			dia="Martes"
			break
		case "MI":
			dia="Miércoles"
			break
		case "JU":
			dia="Jueves"
			break
		case "VI":
			dia="Viernes"
			break
		case "SA":
			dia="Sábado"
			break
	}
	return dia
}

function convertirAHora(num)
{
 var hora=""
 var tmno=""
 var trno=" A.M."
 
	//Verifica medio día
	if (num==12){
		trno=" M."
	}
	//Resta 12 si pasa el medio día
	if (num>12){
		num=num-12
		trno=" P.M."
	}
	//Asigna formato
	hora=num + ":00" + trno
	
	return hora
}


/*Validar Horario para Pre-Matrícula*/
function ValidarHorarioCurso(idCheck)
{
	var CursosCruzados=""
	var totalCruzes=0
	var HorarioMarcado=""
	var HorarioCursos=""
	
	var arrHorarioMarcado=document.getElementById("txthorario" + idCheck.cp)
	var chkcursos=frmFicha.chkcursoshabiles
	
	//Valida si hay horarios registrados del check analizado
	if (arrHorarioMarcado!=undefined){
		//1. Recorrer los horarios de check analizado
		var cantHorarioMarcado=arrHorarioMarcado.length
		if (cantHorarioMarcado==undefined){ //Es un solo horario
			cantHorarioMarcado=1
		}
		
		for (i=0;i<cantHorarioMarcado;i++){
            if(cantHorarioMarcado==1){
	            HorarioMarcado=arrHorarioMarcado.value
            }
            else{
	            HorarioMarcado=arrHorarioMarcado[i].value
            }		
			
			var DiaHM=HorarioMarcado.substr(0,2)
			var InicioHM=parseInt(eval(HorarioMarcado.substr(2,2)))
			var FinHM=parseInt(eval(HorarioMarcado.substr(4,2)))
			
			//2. Recorrer todos otros los cursos programados marcados
			for (j=0; j<chkcursos.length;j++){
				var Control=chkcursos[j]
				/*Validar todos los horarios menos el anaizado*/
				if (Control.checked && Control.id!=idCheck.id){
					//Obtener control txthorario del idCheck
					var arrHorarioCursos=document.getElementById("txthorario" + Control.cp)
					
					if (arrHorarioCursos!=undefined){
						var cantHorarioCursos=arrHorarioCursos.length
						
						if (cantHorarioCursos==undefined){
							cantHorarioCursos=1
						}
						//Recorrer los horarios de los otros cursos marcados
						for (k=0; k<cantHorarioCursos;k++){
						    if(cantHorarioCursos==1){
								HorarioCursos=arrHorarioCursos.value
							}
							else{
								HorarioCursos=arrHorarioCursos[k].value
							}

							var DiaHC=HorarioCursos.substr(0,2)
							var InicioHC=parseInt(eval(HorarioCursos.substr(2,2)))
							var FinHC=parseInt(eval(HorarioCursos.substr(4,2)))
							
							if (DiaHM==DiaHC && InicioHM>=InicioHC && InicioHM<FinHC){
								totalCruzes=totalCruzes+1
								//Extraer el horario que se cruza
								var Horariocruze="   En el horario de : " + convertirADia(DiaHM) + " de " + convertirAHora(InicioHM) + " a " + convertirAHora(FinHM) + "\n"
								//Extraer el nombre del curso programado y el GRUPO horario
								var nombrecurso=chkcursos[j].nc + " (Grupo Horario " + chkcursos[j].gh + ")\n" + Horariocruze
								CursosCruzados+="- " + nombrecurso + "\n"
							}
						}
					}
				}
			}
	    }
    }
	
	/*Mostrar mensaje de Cruzes y desactivar check analizado*/
	if (totalCruzes>0){
	    alert(ArrAlertas[0] + CursosCruzados)
	    idCheck.checked=false
	}
}

//Muestra el horario de los curso elegidos antes de GUARDAR la matrícula
function MostrarHorarioCursosElegido()
{
	//Verifica si la ventana que lo abrió está activa
	if (window.opener && !window.opener.closed){
		var chkcursos=window.opener.frmFicha.chkcursoshabiles
		var HorarioCursos=""
		var AmbienteCurso=""
		var contador=0
		
		for (var j=0; j<chkcursos.length;j++){
			var Control=chkcursos[j]
			
			if (Control.type=="checkbox" && Control.checked){
				/*
				Obtener el código del curso programado
				Obtener el nombre del Curso
				Obtener control txthorario del idCheck
				Obtener control txtambiente del idCheck
				*/
				var codigocup=Control.cp
				var codigoCurso=Control.nc
				var arrHorarioCursos=window.opener.frmFicha.elements["txthorario" + codigocup]
				var arrAmbienteCursos=window.opener.frmFicha.elements["txtambiente" + codigocup]
			
				//Verifica si no hay horario
				if (arrHorarioCursos!=undefined){
					var cantHorarioCursos=arrHorarioCursos.length
				
					if (cantHorarioCursos==undefined){
						cantHorarioCursos=1
					}
					//Recorrer los horarios de los cursos marcados
					for (var k=0; k<cantHorarioCursos;k++){
						if(cantHorarioCursos==1){
							HorarioCursos=arrHorarioCursos.value
							AmbienteCurso=arrAmbienteCursos.value
						}
						else{
							HorarioCursos=arrHorarioCursos[k].value
							AmbienteCurso=arrAmbienteCursos[k].value
						}
						//Extraer día --> hora de inicio --> fin -1
						var DiaHC=HorarioCursos.substr(0,2)
						var InicioHC=parseInt(eval(HorarioCursos.substr(2,2)))
						var FinHC=parseInt(eval(HorarioCursos.substr(4,2))-1)
																									
						/*Verifica si existe la celda de inicio
						Almacena en variable si existe la celda con el codigo horaro
						*/
						var celdainicio=document.getElementById(DiaHC+anchotexto(InicioHC))
                        if (celdainicio!=null){
                            celdainicio.className="CeldaMarcada"
                            celdainicio.innerHTML=codigoCurso + "<br><i>" + AmbienteCurso + "</i>"
                            contador=0
                        }
					
						//Verifica el rango de la fecha fin para pintar								
						if (FinHC>InicioHC){
							for(c=InicioHC;c<=FinHC;c++){
								var celdafin=document.getElementById(DiaHC + anchotexto(c))
									if (celdafin!=null){
										celdafin.className="CeldaMarcada"
										contador=eval(contador)+1
									}
							}
						}						
					}
				}
			}
		}
	}
}


//Muestra el horario de los curso matriculados en el semestre actual
function MostrarHorarioCursosMatriculados()
{
	//Verifica si la ventana que lo abrió está activa
	if (window.opener && !window.opener.closed){
		
		var HorarioCursos=""
		var AmbienteCurso=""
		
		//Almacenar array de codigo de cursos programadoss
		var arrControles=window.opener.document.all.txtCodCursoProgramado
		var numControles=arrControles.length
		
		for (var j=0; j<numControles;j++){
			/*
			Obtener el código del curso programado
			Obtener el nombre del Curso
			Obtener control txthorario
			Obtener control txtambiente
			*/
			var codigocup=arrControles[j].value
			var codigoCurso=window.opener.document.getElementById("txtNombreCurso" + codigocup).value
			var arrHorarioCursos=window.opener.frmFicha.elements["txthorario" + codigocup]
			var arrAmbienteCursos=window.opener.frmFicha.elements["txtambiente" + codigocup]

			//var arrHorarioCursos=window.opener.document.getElementById("txthorario" + codigocup)
			//var arrAmbienteCursos=window.opener.document.getElementById("txtambiente" + codigocup)
			
            //Verifica si no hay horario
			if (arrHorarioCursos!=undefined){
				var cantHorarioCursos=arrHorarioCursos.length
			
				if (cantHorarioCursos==undefined){
					cantHorarioCursos=1
				}
				//Recorrer los horarios de los cursos marcados
				for (var k=0; k<cantHorarioCursos;k++){
					if(cantHorarioCursos==1){
						HorarioCursos=arrHorarioCursos.value
						AmbienteCurso=arrAmbienteCursos.value
					}
					else{
						HorarioCursos=arrHorarioCursos[k].value
						AmbienteCurso=arrAmbienteCursos[k].value
					}
					//Extraer día --> hora de inicio --> fin -1
					var DiaHC=HorarioCursos.substr(0,2)
					var InicioHC=parseInt(eval(HorarioCursos.substr(2,2)))
					var FinHC=parseInt(eval(HorarioCursos.substr(4,2))-1)
																								
					/*Verifica si existe la celda de inicio
					Almacena en variable si existe la celda con el codigo horaro
					*/
					var celdainicio=document.getElementById(DiaHC+anchotexto(InicioHC))
					if (celdainicio!=null){
						celdainicio.className="CeldaMarcada"
						celdainicio.innerHTML=codigoCurso + "<br><i>" + AmbienteCurso + "</i>"
					}
					//Verifica el rango de la fecha fin para pintar								
					if (FinHC>InicioHC){
						for(c=InicioHC;c<=FinHC;c++){
							var celdafin=document.getElementById(DiaHC + anchotexto(c))
								if (celdafin!=null){
									celdafin.className="CeldaMarcada"
								}
						}
					}
				}
			}
		}
	}
}
