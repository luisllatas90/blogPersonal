//************************************************************************************
//CV-USAT
//Fecha de Creación: 25/06/2005
//Creado por	: Gerardo Chunga Chinguel
//Observación	: Da formato para mostrar el horario de los cursos matriculados y elegidos 
//************************************************************************************

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

//Muestra el horario de los curso elegidos antes de GUARDAR la matrícula
function MostrarHorarioCursosElegido()
{
	//Verifica si la ventana que lo abrió está activa
	if (window.opener && !window.opener.closed){
		var valoresinicio=""
		var valoresfin=""
		var numControles=window.opener.t1Contenido.frmFicha.elements.length
		
		for (var j=0; j<numControles;j++){
			var Control=window.opener.t1Contenido.frmFicha.elements[j]
			
			if (Control.type=="checkbox" && Control.checked){
				/*
				Obtener el código del curso programado
				Obtener el nombre del Curso
				Obtener control txthorario del chkmarcado
				Obtener control txtambiente del chkmarcado
				*/
				var codigocup=window.opener.t1Contenido.frmFicha(j).cp
				var codigoCurso=window.opener.t1Contenido.frmFicha(j).nc
				var arrhorarioN=window.opener.t1Contenido.frmFicha.elements["txthorario" + codigocup]
				var arrambienteN=window.opener.t1Contenido.frmFicha.elements["txtambiente" + codigocup]
			
				//Verifica si no hay horario
				if (arrhorarioN!=undefined){
					var numhorariosN=arrhorarioN.length
				
					if (numhorariosN==undefined){
						numhorariosN=1
					}
					//Recorrer los horarios de los cursos marcados
						for (var k=0; k<numhorariosN;k++){
							if(numhorariosN==1){
								var codhorarioN=arrhorarioN.value
								var codambienteN=arrambienteN.value
							}
							else{
								var codhorarioN=arrhorarioN[k].value
								var codambienteN=arrambienteN[k].value
							}
							//Extraer día --> hora de inicio --> fin
							var diaN=codhorarioN.substr(0,2)
							var NuminicioN=parseInt(eval(codhorarioN.substr(2,2)))
							var NumfinN=parseInt(eval(codhorarioN.substr(4,2))-1)
							var inicioN=anchotexto(NuminicioN)
							var finN=anchotexto(NumfinN)

							var diainicioN=diaN+inicioN
							var diafinN=diaN+finN
																										
							/*Verifica si existe la celda de inicio
							valoresinicio+=diainicioN + "\n"
							valoresfin+=diafinN + "\n"
							Almacena en variable si existe la celda con el codigo horaro
							*/
							var celdainicio=document.getElementById(diainicioN)
							if (celdainicio!=null){
								celdainicio.style.backgroundColor="#99CCFF"
								celdainicio.innerHTML=codigoCurso + "<br><i>" + codambienteN + "</i>"
							}
							//Verifica el rango de la fecha fin para pintar								
							if (NumfinN>NuminicioN){
								for(c=NuminicioN;c<=NumfinN;c++){
									var celdafin=document.getElementById(diaN + anchotexto(c))
										if (celdafin!=null){
											celdafin.style.backgroundColor="#99CCFF"
										}
								}
							}
						}
				}
			}
		}
	}
	//alert ("INICIO:\n" + valoresinicio+ "\nFIN:\n" + valoresfin)
}


//Muestra el horario de los curso matriculados en el semestre actual
function MostrarHorarioCursosMatriculados()
{
	//Verifica si la ventana que lo abrió está activa
	if (window.opener && !window.opener.closed){
		
		var valoresinicio=""
		var valoresfin=""
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
				var arrhorarioN=window.opener.frmFicha.elements["txthorario" + codigocup]
				var arrambienteN=window.opener.frmFicha.elements["txtambiente" + codigocup]

				//var arrhorarioN=window.opener.document.getElementById("txthorario" + codigocup)
				//var arrambienteN=window.opener.document.getElementById("txtambiente" + codigocup)
				
				//Verifica si no hay horario
				if (arrhorarioN!=undefined){
					var numhorariosN=arrhorarioN.length
				
					if (numhorariosN==undefined){
						numhorariosN=1
					}
					//Recorrer los horarios de los cursos matriculados
						for (var k=0; k<numhorariosN;k++){
							if(numhorariosN==1){
								var codhorarioN=arrhorarioN.value
								var codambienteN=arrambienteN.value
							}
							else{
								var codhorarioN=arrhorarioN[k].value
								var codambienteN=arrambienteN[k].value
							}

							//Extraer día --> hora de inicio --> fin
							var diaN=codhorarioN.substr(0,2)
							var NuminicioN=parseInt(eval(codhorarioN.substr(2,2)))
							var NumfinN=parseInt(eval(codhorarioN.substr(4,2))-1)
							var inicioN=anchotexto(NuminicioN)
							var finN=anchotexto(NumfinN)

							var diainicioN=diaN+inicioN
							var diafinN=diaN+finN
																										
							/*Verifica si existe la celda de inicio
							valoresinicio+=diainicioN + "\n"
							valoresfin+=diafinN + "\n"
							Almacena en variable si existe la celda con el codigo horaro
							*/
							var celdainicio=document.getElementById(diainicioN)
							if (celdainicio!=null){
								celdainicio.style.backgroundColor="#99CCFF"
								celdainicio.innerHTML=codigoCurso + "<br><i>" + codambienteN + "</i>"
							}
							//Verifica el rango de la fecha fin para pintar								
							if (NumfinN>NuminicioN){
								for(c=NuminicioN;c<=NumfinN;c++){
									var celdafin=document.getElementById(diaN + anchotexto(c))
										if (celdafin!=null){
											celdafin.style.backgroundColor="#99CCFF"
										}
								}
							}
						}
				}
		}
	}
	//alert ("INICIO:\n" + valoresinicio+ "\nFIN:\n" + valoresfin)
}
