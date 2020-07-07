/*
================================================================================================
	CVUSAT
	Fecha de Creación: 30/05/2005
	Fecha de Modificación: 25/07/2006
	Creador: Gerardo Chunga Chinguel
	Obs: Realiza las validaciones y procedimientos para el módulo de Matrícula del Alumno
================================================================================================
*/

function LimpiarError() {
 window.status="Se ha producido un error. Contáctese con el Administrador del Sistema"
 return true;
}

//window.onerror = LimpiarError

//document.oncontextmenu="return false"


//Declarar array de mensajes de alerta en matrícula
var ArrMensajes= new Array(2);
ArrMensajes[0] = "Debe elegir las asignaturas que Ud. se matriculará";
ArrMensajes[1] = "Ud. sólo puede elegir un Grupo de Horario de la asignatura Programada";
ArrMensajes[2] = "No se puede matricular en esta Asignatura, porque existe un cruce de Horario con el Curso:\n\n"
ArrMensajes[3] = "No se puede matricular en esta Asignatura, porque existen cruzes de horarios\n con las siguientes asignaturas:\n\n"
ArrMensajes[4] ="¿Está completamente seguro que desea GUARDAR la matricula?\n\n Recuerde que para cualquier modificación de Agregado o Retiro de Asignaturas\n lo puede hacer después de 48 horas de realizado este proceso"
ArrMensajes[5] ="¿Está seguro que desea Retirarse de las asignaturas seleccionadas?"
ArrMensajes[6] ="¿Está seguro que desea Restablecer los retiros de asignaturas seleccionadas?"
ArrMensajes[7] ="RECUERDE:\n- ES OBLIGATORIO matricularse en al menos una ASIGNATURA COMPLEMENTARIA\n     (Hacer clic en la pestaña Cursos Complementarios)\n- Después de realizada la Matrícula, NO PUEDE RETIRARSE de una asignatura complementaria"
ArrMensajes[8] ="Ud. NO PUEDE ADELANTAR ASIGNATURAS DE CICLOS SUPERIORES,\nPRIMERO DEBE ELEGIR ASIGNATURAS DE CICLO INFERIORES"

/*
================================================================================================
Creado por	: Gerardo Chunga Chinguel
Observación	: Permite sumar creditaje de cursos programados, para guardar en BD 
================================================================================================
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

function ValidarAdelantoCursos(idCheck)
{
	var curso=frmFicha.chkcursoshabiles
	var cursounico=""
	var total=0
	var ciclo1=0
	var ciclo2=0
	var ciclo3=0
	var ciclo4=0
	var ciclo5=0
	var ciclo6=0
	var ciclo7=0
	var ciclo8=0
	var ciclo9=0
	var ciclo10=0
	var ciclo11=0
	var ciclo12=0
	var ciclo13=0
	var ciclo14=0

	/*Solamente verificar en los cursos no marcados*/
	if (curso.length!=undefined){
		for (i=0;i<curso.length;i++){
			var Control=curso[i]
		     
			/*Cuenta total de cursos únicos por ciclo*/
		     if (Control.electivo=="0"){//Es curso obligatorio
			if (Control.checked==false && Control.cc!=cursounico && Control.ciclo!="0" && Control.value!="0"){
				cursounico=Control.cc
				if (Control.ciclo=="1"){ciclo1=eval(ciclo1)+1}
				if (Control.ciclo=="2"){ciclo2=eval(ciclo2)+1}
				if (Control.ciclo=="3"){ciclo3=eval(ciclo3)+1}
				if (Control.ciclo=="4"){ciclo4=eval(ciclo4)+1}
				if (Control.ciclo=="5"){ciclo5=eval(ciclo5)+1}
				if (Control.ciclo=="6"){ciclo6=eval(ciclo6)+1}
				if (Control.ciclo=="7"){ciclo7=eval(ciclo7)+1}
				if (Control.ciclo=="8"){ciclo8=eval(ciclo8)+1}
				if (Control.ciclo=="9"){ciclo9=eval(ciclo9)+1}
				if (Control.ciclo=="10"){ciclo10=eval(ciclo10)+1}
				if (Control.ciclo=="11"){ciclo11=eval(ciclo11)+1}
				if (Control.ciclo=="12"){ciclo12=eval(ciclo12)+1}
				if (Control.ciclo=="13"){ciclo13=eval(ciclo13)+1}
				if (Control.ciclo=="14"){ciclo14=eval(ciclo14)+1}
			}
		     }

			/*Resta los cursos marcados*/
			if (Control.checked==true){
				if (Control.ciclo=="1"){ciclo1=eval(ciclo1)-1}
				if (Control.ciclo=="2"){ciclo2=eval(ciclo2)-1}
				if (Control.ciclo=="3"){ciclo3=eval(ciclo3)-1}
				if (Control.ciclo=="4"){ciclo4=eval(ciclo4)-1}
				if (Control.ciclo=="5"){ciclo5=eval(ciclo5)-1}
				if (Control.ciclo=="6"){ciclo6=eval(ciclo6)-1}
				if (Control.ciclo=="7"){ciclo7=eval(ciclo7)-1}
				if (Control.ciclo=="8"){ciclo8=eval(ciclo8)-1}
				if (Control.ciclo=="9"){ciclo9=eval(ciclo9)-1}
				if (Control.ciclo=="10"){ciclo10=eval(ciclo10)-1}
				if (Control.ciclo=="11"){ciclo11=eval(ciclo11)-1}
				if (Control.ciclo=="12"){ciclo12=eval(ciclo12)-1}
				if (Control.ciclo=="13"){ciclo13=eval(ciclo13)-1}
				if (Control.ciclo=="14"){ciclo14=eval(ciclo14)-1}
			}
		}

		/*No permitir marcar el check actual si no estan marcados los cursos de ciclo inferior*/

		if (idCheck.ciclo>1 && ciclo1>0){total=1}
		else{
			if (idCheck.ciclo>2 && ciclo2>0){total=1}
			else{
				if (idCheck.ciclo>3 && ciclo3>0){total=1}
				else{
					if (idCheck.ciclo>4 && ciclo4>0){total=1}
					else{
						if (idCheck.ciclo>5 && ciclo5>0){total=1}
						else{
							if (idCheck.ciclo>6 && ciclo6>0){total=1}
							else{
								if (idCheck.ciclo>7 && ciclo7>0){total=1}
								else{
									if (idCheck.ciclo>8 && ciclo8>0){total=1}
									else{
										if (idCheck.ciclo>9 && ciclo9>0){total=1}
										else{
											if (idCheck.ciclo>10 && ciclo10>0){total=1}
											else{
												if (idCheck.ciclo>11 && ciclo11>0){total=1}
												else{
													if (idCheck.ciclo>12 && ciclo12>0){total=1}
													else{
														if (idCheck.ciclo>13 && ciclo13>0){total=1}
														else{
															if (idCheck.ciclo>14 && ciclo14>0){total=1}
														}
													}
												}
											}
										}
									}
								}
							}
						}
					}
				}
			}
		}
	}

	/*Desactivar check marcado*/
	if (total>0){
		idCheck.checked=false
		frmFicha.cmdGuardar.disabled=true//<--Para no activar a pesar que quiten la marca
		alert(ArrMensajes[8])
	}

	return(total)

	//alert('CICLO 1=' + ciclo1 + '\nCICLO 2=' + ciclo2 + '\nCICLO 3=' + ciclo3 + '\nCICLO 4=' + ciclo4 + '\nCICLO 5=' + ciclo5 + '\nCICLO 6=' + ciclo6 + '\nCICLO 7=' + ciclo7 + '\nCICLO 8=' + ciclo8 + '\nCICLO 9=' + ciclo9 + '\nCICLO 10=' + ciclo10 + '\nCICLO 11=' + ciclo11 + '\nCICLO 12=' + ciclo12 + '\nCICLO 13=' + ciclo13 + '\nCICLO 14=' + ciclo14)
}

function Actualizar(idCheck,limitecrd,tipo,escuela)
{
	var valctrl=0
	var total=0
	var contadorGH=0
	var chkcursos=frmFicha.chkcursoshabiles

	if (chkcursos.length==undefined){
		if (chkcursos.checked){
			total=eval(chkcursos.value)
		}
	}
	else{
		for (i=0; i<chkcursos.length;i++){
		       var Control=chkcursos[i]
			if (Control.type=="checkbox" && Control.checked){
				if (Control.cc==idCheck.cc)
					{contadorGH=contadorGH+1}
				total+=eval(Control.value)
			}
		}
	}
	/*Validar máximo de créditos*/
	
	if (eval(total)>eval(limitecrd)){
	   alert("Ud. solo se puede matricularse hasta en " + limitecrd + " créditos\n\n Si aún no ha cumplido el límite de créditos\n Se le sugiere elija otro curso de menor creditaje.")
	   total=total-eval(idCheck.value)
	   idCheck.checked=false
	}
	
	/*Validar matrícula en un único grupo horario del curso*/
	if (contadorGH>1){
	   alert (ArrMensajes[1])
	   total=total-eval(idCheck.value)
	   idCheck.checked=false
	}

	/*Validar horario del grupo horario marcado*/	
	if (idCheck.checked){
		if(escuela!=20 && escuela!=22 && escuela!=23)
		   {total=total - eval(ValidarHorario(idCheck))}
	}

	/*Validar adelanto de cursos, debe matricularse en ciclo inferiores*/
	if (idCheck.checked){
		total=total-eval(ValidarAdelantoCursos(idCheck))
	}
	//ValidarAdelantoCursos(idCheck)

	tdTotal.innerHTML=total
}

function activarControles(valor)
{
	if (valor==0){
		frmFicha.cmdGuardar.disabled=true
		frmFicha.cmdHorario.disabled=true
		//frmFicha.cmdCursos.disabled=true
	}
	else{
		frmFicha.cmdGuardar.disabled=false
		frmFicha.cmdHorario.disabled=false
		//frmFicha.cmdCursos.disabled=false
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
		//Declarar array de propiedades del check marcado
		//se puede usar para conctenar array.concat.
		var arrCP="" //Array de codigo de curso programado
		var arrTC="" //Array de tipo de curso
		var arrCR="" //Array de creditos del curso
		var arrVD="" //Array de veces que lleva el curso
		var chk1=frmFicha.chkcursoshabiles
		var totalmarcados=0
		var totalcomplementario=0
		var ExisteComp=0
		var Bandera=0 //Falso
		var ExigirCompl=frmFicha.txtExigirCompl.value //Permite validar si se exige o no el complementario

		//Recorriendo 1er Iframe Con cursos curriculares/complementarios de plan antiguo o nuevo
		if (chk1.length==undefined){
			if (chk1.checked==true){
				arrCP+=chk1.cp + ","
				arrTC+=chk1.tc + ","
				arrCR+=chk1.value + ","
				arrVD+=chk1.vd + ","
				totalmarcados=1
				totalcomplementario=1
				ExisteComp=1
			}
		}
		else{
			for(var I=0;I<chk1.length;I++){
				var Control=chk1[I]
				if (Control.tc=="CO" || Control.tc=="CC" && Control.value=="0")//Es complementario pago aparte
					{ExisteComp=eval(ExisteComp)+1}

				if(Control.checked==true){
					arrCP+=Control.cp + ","
					arrTC+=Control.tc + ","
					arrCR+=Control.value + ","
					arrVD+=Control.vd + ","
					totalmarcados=eval(totalmarcados)+1

					if (Control.tc=="CO" || Control.tc=="CC" && Control.value=="0")//Es complementario pago aparte
						{totalcomplementario=eval(totalcomplementario)+1}

				}
			}
		}
		
		//Si no ha elegido cursos -->NO MATRICULAR
		if (totalmarcados=="0"){
			alert(ArrMensajes[0])
		}
		else{
			/* Verificar si ha marcado al menos un complementario */
			if (ExisteComp>0 && totalcomplementario=="0" && ExigirCompl=="0"){
				alert(ArrMensajes[7])
			}
			else{
				//Mostrar mensaje de confirmación
				if (confirm(ArrMensajes[4])==true){
					procesarMensaje()
					location.href="procesarmatricula.asp?accion=" + accion + "&CursosProgramados=" + arrCP + "&TipoCursos=" + arrTC + "&CreditoCursos=" + arrCR + "&VecesDesprobados=" + arrVD + "&codigo_mat=" + cm
				}
			}
		}
}


function modificarmatricula(modo,codigo_mat)
{
	if (modo=='A'){
		location.href="cargando.asp?pagina=agregado&codigo_mat=" + codigo_mat
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
			location.href="procesarmatricula.asp?accion=cambiarestadocurso&CursosProgramados=" + arrCP + "&codigo_mat=" + codigo_mat + "&estado_dma=" + modo
		}
	   }
	}	
}

function MostrarTablaCursos(modo)
{
	var comp=document.all.cursocomplementario
	var curr=document.all.cursocurricular

	if (modo=="R"){
		curr.style.display=""
		if (comp!=undefined){comp.style.display="none"}
	}
	else{
		comp.style.display=""
		if (curr!=undefined){curr.style.display="none"}
	}
}


function vistahorarioelegidos()
{
	window.open("vistahorario.asp?estadomatricula=previo","vsthorario","height=400,width=700,statusbar=no,scrollbars=yes,top=100,left=200,resizable=yes,toolbar=no,menubar=no")
}

function vistahorariomatriculados()
{
	window.open("vistahorario.asp?estadomatricula=matriculados","vsthorario","height=400,width=700,statusbar=no,scrollbars=yes,top=100,left=200,resizable=yes,toolbar=no,menubar=no")
}

function vistacursosprematriculados()
{
	window.open("listacursoselegidos.asp?modo=G","vstcursos","height=400,width=700,statusbar=no,scrollbars=no,top=200,left=200,resizable=no,toolbar=no,menubar=no")
}

function procesarMensaje()
{
	var texto="<h1>&nbsp;</h1><h3 align='center'>Procesando matrícula<br>Por favor. Espere un momento...<br><br>"
	    texto+="<img border='0' src=\"../images/cargando.gif\" width=\"300\" height=\"25\"></h3><h1>&nbsp;</h1>"

	frmFicha.className="contornotabla"
	frmFicha.innerHTML=texto
}