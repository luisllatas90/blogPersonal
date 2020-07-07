/*
CV-USAT
Fecha de Creación: 30/05/2005
Fecha de Modificación: 14/01/2006
Creado por	: Gerardo Chunga Chinguel
Observación	: Valida el Sistema de matrícula 
*/

//function LimpiarError() {
// window.status="Se ha producido un error. Contáctese con el Administrador del Sistema"
// return true;
//} 

//window.onerror = LimpiarError

//document.oncontextmenu="return false"


//Declarar array de mensajes de alerta en matrícula
var ArrMensajes= new Array(2);
ArrMensajes[0] = "Debe elegir las asignaturas que Ud. se matriculará";
ArrMensajes[1] = "Ud. sólo puede elegir un Grupo de Horario del Curso a Matricularse";
ArrMensajes[2] = "No se puede matricular en esta Asignatura, porque existe un cruce de Horario con el Curso:\n\n"
ArrMensajes[3] = "No se puede matricular en esta Asignatura, porque existen cruzes de horarios\n con las siguientes asignaturas:\n\n"
ArrMensajes[4] ="¿Está completamente seguro que desea matricularse en los cursos elegidos\n Recuerde que cualquier modificación de Agregado o Retiro de Asignaturas\n lo puede hacer después de 48 horas de realizada la matrícula?"
ArrMensajes[5] ="¿Está seguro que desea Retirarse de las asignaturas seleccionadas?"
ArrMensajes[6] ="¿Está seguro que desea Restablecer los retiros de asignaturas seleccionadas?"

/*
CV-USAT
Fecha de Creación: 30/05/2005
Fecha de Modificación: 06/01/2006
Creado por	: Gerardo Chunga Chinguel
Observación	: Permite sumar creditaje de cursos programados, para guardar en BD 
*/

function pintafilamarcada(fila,idcheck,modo)
{
   var Estado=true
   var Celdas=fila.cells;
   var ArrCeldas=Celdas.length

   for (var c=0;c<ArrCeldas; c++){
		if(idcheck.checked==true){
			Celdas[c].style.backgroundColor = '#DFEFFF';
		}
		else {
			Celdas[c].style.backgroundColor = ''; // #d6e7ef
			Estado=false
		}
   }

   if (modo==undefined)
	{CalcularcursosElegidos()}
   return true;
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

function ValidarHorario(chkmarcado)
{
	var HayCruce=false
	var CursosCruzados=""
	var mensajecruce=""
	var totalCruzes=0
	var crd=0
	
	var nhorario=chkmarcado.id
	var temp=nhorario.substr(3,10)
	var arrhorarioA=frmFicha.elements["txthorario" + temp]
	var chkcursos=frmFicha.chkcursoshabiles
	
	//Valida si existe Horario registrado para el curso
	if (arrhorarioA!=undefined){
		//1. Recorrer el array de horarios de check actual
		var numhorariosA=arrhorarioA.length
		if (numhorariosA==undefined){ //Es un solo horario
			numhorariosA=1
		}
			for (i=0; i<numhorariosA;i++){
				if (numhorariosA==1){
					var codhorarioA=arrhorarioA.value
				}
				else{
					var codhorarioA=arrhorarioA[i].value
				}
				var diaA=codhorarioA.substr(0,2)
				var inicioA=parseInt(eval(codhorarioA.substr(2,2)))
				var finA=parseInt(eval(codhorarioA.substr(4,2))-1)
				var finalreal=parseInt(eval(codhorarioA.substr(4,2)))
				
				//2. Recorrer todos otros los cursos programados marcados
				for (j=0; j<chkcursos.length;j++){
					var Control=chkcursos[j]
					if (Control.checked && Control.id!=chkmarcado.id){
						//Almacenar el codigocup en esta variable
						//Obtener control txthorario del chkmarcado
						var codigocup=Control.cp
						var arrhorarioN=frmFicha.elements["txthorario" + codigocup]
						
						if (arrhorarioN!=undefined){
							var numhorariosN=arrhorarioN.length
							
							if (numhorariosN==undefined){
								numhorariosN=1
							}
							//Recorrer los horarios de los otros cursos marcados
							for (k=0; k<numhorariosN;k++){
								if(numhorariosN==1){
									var codhorarioN=arrhorarioN.value
								}
								else{
									var codhorarioN=arrhorarioN[k].value
								}
								var diaN=codhorarioN.substr(0,2)
								var inicioN=parseInt(eval(codhorarioN.substr(2,2)))
								
								if (diaN==diaA){
									if (inicioN>=inicioA && inicioN<=finA){
										HayCruce=true
										totalCruzes=totalCruzes+1
										//Extraer el horario que se cruza
										var horariocruze="   En el horario de : " + convertirADia(diaA) + " de " + convertirAHora(inicioA) + " a " + convertirAHora(finalreal) + "\n"
										//Extraer el nombre del curso programado y el GRUPO horario
										var nombrecurso=chkcursos[j].nc + " (Grupo Horario " + chkcursos[j].gh + ")\n" + horariocruze
										CursosCruzados+="- " + nombrecurso + "\n"
									}
								}
							}
						}
					}
				}
			}
	}
	if(HayCruce==true){
		if (totalCruzes==1)
			{mensajecruce=ArrMensajes[2]}
		else
			{mensajecruce=ArrMensajes[3]}
		//Mostrar mensaje y desactivar check
		alert(mensajecruce + CursosCruzados)
		crd=chkmarcado.value
		chkmarcado.checked=false
		return crd
	}
	else{
		return 0	
	}
}

function CalcularcursosElegidos()
{
	var chkcursos=frmFicha.chkcursoshabiles
	var total=0

	if (chkcursos.length==undefined){
		if (chkcursos.checked){
			total=1
		}
	}
	else{
		for (i=0; i<chkcursos.length;i++){
		       var Control=chkcursos[i]
			if (Control.type=="checkbox" && Control.checked){
				total=eval(total)+1
			}
		}
	}
	tdCursos.innerHTML=total
	activarControles(total)
}


function Actualizar(idCheck,limitecrd,codCur,tipo)
{
	var valctrl=0
	var total=0
	var contadorGH=0
	var chkcursos=frmFicha.chkcursoshabiles

	if (chkcursos.length==undefined){
		if (chkcursos.checked){
			total+=eval(chkcursos.cr)
		}
	}
	else{
		for (i=0; i<chkcursos.length;i++){
		       var Control=chkcursos[i]
			if (Control.type=="checkbox" && Control.checked){
				if (Control.cc==codCur)
					{contadorGH=contadorGH+1}
				total+=eval(Control.cr)
			}
		}
	}
	
	if (eval(limitecrd)<=eval(total)){
	   alert("Ud. solo se puede matricularse hasta en " + limitecrd + " créditos\n\n Si aún no ha cumplido el límite de créditos\n Se le sugiere elija otro curso de menor creditaje.")
	   total=total-eval(idCheck.value)
	   idCheck.checked=false
	}
	
	if (contadorGH>1){
	   alert (ArrMensajes[1])
	   total=total-eval(idCheck.value)
	   idCheck.checked=false
	}
	
	if (idCheck.checked){
		total=total - eval(ValidarHorario(idCheck))
	}

	tdTotal.innerHTML=total
}

function activarControles(valor)
{
	if (valor==0){
		frmFicha.cmdGuardar.disabled=true
		frmFicha.cmdHorario.disabled=true
		frmFicha.cmdCursos.disabled=true
	}
	else{
		frmFicha.cmdGuardar.disabled=false
		frmFicha.cmdHorario.disabled=false
		frmFicha.cmdCursos.disabled=false
	}
}


function vercursoselegidos()
{
	var chkcursos=window.opener.frmFicha.chkcursoshabiles
	var tbl=document.all.tblelegidos //window.opener.tblcurricular

	if (chkcursos.length==undefined){
		if (chkcursos.checked){
			var fila=window.opener.document.getElementById("fila" + chkcursos.cp)
			AgregarCursoElegido(tbl,fila)
		}
	}
	else{
		for (i=0; i<chkcursos.length;i++){
		       var Control=chkcursos[i]
			if (Control.type=="checkbox" && Control.checked){
				var fila=window.opener.document.getElementById("fila" + Control.cp)
				AgregarCursoElegido(tbl,fila)
			}
		}
	}

}

function AgregarCursoElegido(tbl,fila)
{
  //verifica que la fila no exista para agregarla a la tabla
  //Crear fila con curso elegido con el mismo id=fila

	var UltimaFila = tbl.rows.length
	var Contador = UltimaFila
	var NuevaFila = tbl.insertRow(UltimaFila)
	NuevaFila.id=fila.id
	
	//Declara variables para fila marcada
	var Celdas=fila.cells
	var j=2
	
	for (var i=0;i<6; i++){
		var TextoCelda=Celdas[j].innerText
		var CeldaNueva=NuevaFila.insertCell(i)
		formatoCeldaAgregada(i,CeldaNueva)
	        CeldaNueva.appendChild(tbl.document.createTextNode(TextoCelda))
        	NuevaFila.appendChild(CeldaNueva)
	        j=j+1
	}
}

function formatoCeldaAgregada(numero,celda)
{
	switch(numero)
	{
		case 0:
			celda.style.width="5%"
			break
		case 1:
			celda.style.width="10%"
			break
		case 2:
			celda.style.width="62%"
			break
		case 3:
			celda.style.width="5%"
			break
		case 4:
			celda.style.width="5%"
			break
		case 5:
			celda.style.width="10%"
			break
	}
}

function QuitarCursoElegido(fila)
{
 //verifica que la fila exista para eliminarla
  var tbl = fracursoselegidos.tblelegidos
  var ArrFilas = tbl.getElementsByTagName('tr')
  var tfilas = ArrFilas.length
  //Calcular total de cursos elegidos, según fila elegidas
  if (tfilas==undefined)
	{tdCursos.innerHTML=0}

     for (var i = 0; i < tfilas; i++){
	if (ArrFilas[i].id==fila.id){
	   fracursoselegidos.tblelegidos.deleteRow(i)
	   tdCursos.innerHTML=eval(tfilas)-1
	   return //Salir de la función si encuentra fila
	}
    }
}

function EnviarFichaMatricula(accion,cm)
{
	//Mostrar mensaje de confirmación
	if (confirm(ArrMensajes[4])==true){
		//Declarar array de propiedades del check marcado
		//se puede usar para conctenar array.concat.
		var arrCP="" //Array de codigo de curso programado
		var arrTC="" //Array de tipo de curso
		var arrCR="" //Array de creditos del curso
		var arrVD="" //Array de veces que lleva el curso
		var chk1=frmFicha.chkcursoshabiles
		var totalmarcados=0

		//Recorriendo 1er Iframe Con cursos curriculares/complementarios
		if (chk1.length==undefined){
			if (chk1.checked==true){
				arrCP+=chk1.cp + ","
				arrTC+=chk1.tc + ","
				arrCR+=chk1.cr + ","
				arrVD+=chk1.vd + ","
				totalmarcados=1
			}
		}
		else{
			for(var I=0;I<chk1.length;I++){
				var Control=chk1[I]
				if(Control.checked==true){
					arrCP+=Control.cp + ","
					arrTC+=Control.tc + ","
					arrCR+=Control.cr + ","
					arrVD+=Control.vd + ","
					totalmarcados=eval(totalmarcados)+1
				}
			}
		}

		if (totalmarcados=="0"){
			alert(ArrMensajes[0])
		}
		else{
			AbrirMensaje('../images/')
			location.href="procesar.asp?accion=" + accion + "&CursosProgramados=" + arrCP + "&TipoCursos=" + arrTC + "&CreditoCursos=" + arrCR + "&VecesDesprobados=" + arrVD + "&codigo_mat=" + cm
		}
	}
}



function EnviarMatriculaAutomatica(tipo_cac,codigo_cac)
{
	//Mostrar mensaje de confirmación
	if (confirm("Está seguro que desea Guardar la Matrícula automática")==true){
		//Declarar array de propiedades del check marcado
		//se puede usar para conctenar array.concat.
		var arrCP="" //Array de codigo de curso programado
		var arrTC="" //Array de tipo de curso
		var arrCR="" //Array de creditos del curso
		var arrVD="" //Array de veces que lleva el curso
		var chk1=fradetalle.frmFicha.chkcursoshabiles
		var chkalumnos=document.all.chk
		var arrAlumnos=""
		var totalmarcados=0
		var totalalumnos=0

		//Recorrer alumnos seleccionados
		if (chkalumnos.length==undefined){
			arrAlumnos=chkalumnos.value
			totalalumnos=1
		}
		else{
			for(var I=0;I<chkalumnos.length;I++){
				var Control=chkalumnos[I]
				if(Control.checked==true){
					arrAlumnos+=Control.value + ","
					totalalumnos=eval(totalalumnos)+1
				}
			}
		}


		if (totalalumnos==0){
			alert("Debe seleccionar los alumnos que se les generará Matrícula automática")
		}
		else{
			//Recorriendo 1er Iframe Con cursos curriculares/complementarios
			if (chk1.length==undefined){
				if (chk1.checked==true){
					arrCP+=chk1.cp + ","
					arrTC+=chk1.tc + ","
					arrCR+=chk1.cr + ","
					arrVD+=chk1.vd + ","
					totalmarcados=1
				}
			}
			else{
				for(var I=0;I<chk1.length;I++){
					var Control=chk1[I]
					if(Control.checked==true){
						arrCP+=Control.cp + ","
						arrTC+=Control.tc + ","
						arrCR+=Control.cr + ","
						arrVD+=Control.vd + ","
						totalmarcados=eval(totalmarcados)+1
					}
				}
			}

			if (totalmarcados=="0"){
				alert(ArrMensajes[0])
			}
			else{
				//AbrirMensaje('../../../images/')
				cmdAnular.disabled=true
				location.href="procesarmatriculaautomatica.asp?CursosProgramados=" + arrCP + "&TipoCursos=" + arrTC + "&CreditoCursos=" + arrCR + "&VecesDesprobados=" + arrVD + "&Alumnos=" + arrAlumnos + "&tipo_cac=" + tipo_cac + "&codigo_cac=" + codigo_cac
			}
		}
	}
}


function modificarmatricula(modo,codigo_mat)
{
	if (modo=='A'){
		location.href="frmmatricula2.asp?accion=agregarcursomatricula&codigo_mat=" + codigo_mat
		AbrirMensaje('../images/')
	}
	else{
	  var num="5"
	  if (modo=="M"){num="6"}
 	  if (confirm(ArrMensajes[num])==true){
		//Declarar array de propiedades del check marcado
		var arrCP="" //Array de codigo de curso programado
		var chk=document.all.chk
		var totalmarcados=0

		//Recorriendo los check marcados
		if (chk.length==undefined){
			if (chk.checked==true){
				arrCP+=chk.value + ","
				totalmarcados=1
			}
		}
		else{
	           for(var i=0;i<chk.length;i++){
			if(chk[i].checked==true){
				arrCP+=chk[i].value + ","
				totalmarcados=eval(totalmarcados)+1
			}
		   }
		}

		if (totalmarcados!=0){
			location.href="procesar.asp?accion=cambiarestadocurso&CursosProgramados=" + arrCP + "&codigo_mat=" + codigo_mat + "&estado_dma=" + modo
		}
	   }
	}	
}

function MostrarTablaCursos(modo)
{
	if (modo=="R"){
		cursocurricular.style.display=""
		cursocomplementario.style.display="none"
	}
	else{
		cursocomplementario.style.display=""
		cursocurricular.style.display="none"

	}
}


function vistahorarioelegidos()
{
	window.open("vistahorario2.asp?estadomatricula=previo","vsthorario","height=400,width=700,statusbar=no,scrollbars=yes,top=100,left=200,resizable=yes,toolbar=no,menubar=no")
}

function vistahorariomatriculados()
{
	window.open("vistahorario.asp?estadomatricula=matriculados","vsthorario","height=400,width=700,statusbar=no,scrollbars=yes,top=100,left=200,resizable=yes,toolbar=no,menubar=no")
}

function vistacursosprematriculados()
{
	window.open("listacursoselegidos2.asp?modo=G","vstcursos","height=400,width=700,statusbar=no,scrollbars=no,top=200,left=200,resizable=no,toolbar=no,menubar=no")
}