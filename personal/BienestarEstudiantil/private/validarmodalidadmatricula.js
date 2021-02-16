/*
==========================================================================================
  CV-USAT
  Fecha de Creación: 19/01/2006
  Fecha de Modificación: 20/01/2006
  Creado por	: Gerardo Chunga Chinguel
  Observación	: Validar Otros tipos de matrícula: SUF/EXUBIC/COVTR/CVPR
==========================================================================================
*/

var Mensaje= new Array(10);
Mensaje[0] = "Debe elegir los alumnos que realizarán este Proceso de Actualización";


function validarotramatricula(modo,codigo_alu)
{
	var plan=document.all.cbocodigo_pes
	var ciclo=document.all.cbocodigo_cac
	var docente=1
	var codigocurso=document.all.txtcursoVal
	var curso=document.all.txtcurso
	var nota=document.all.txtNota
	var gh="A"
	var asist=0
	var inasist=0

	if (ciclo.value=="-2"){
		alert("Especifique el ciclo académico de matrícula")
		ciclo.focus()
		return(false)
	}

	if (modo=="EXSUF" || modo=="NORMAL"){
		docente=document.all.cbocodigo_per.value
		if (docente=="-2"){
			alert("Especifique el docente")
			document.all.cbocodigo_per.focus()
			return(false)
		}
	}


	if (curso.value=="" || codigocurso.value==""){
		alert("Especifique el curso, a matricular")
		curso.focus()
		return(false)
	}
	
	if (modo=="NORMAL"){
		gh=document.all.txtGrupohor_cup.value
		asist=document.all.txtAsistencias.value
		inasist=document.all.txtInasistencias.value
		if (gh==""){
			alert("Debe especificar el Grupo horario de la Asignatura programada")
			document.all.txtGrupohor_cup.focus()
			return(false)
		}
	}
		
	if (nota.value==""){
		alert("Especifique la nota")
		nota.focus()
		return(false)
	}
	if ((nota.value<0)||(nota.value>20)){
		alert("La nota especificada no es válida")
		nota.focus()
		return(false)
	}
	
	location.href="procesar.asp?accion=registrarotramodalidadmatricula&codigo_alu=" + codigo_alu + "&tipo=" + modo + "&codigo_cac=" + ciclo.value + "&codigo_pes=" + plan.value + "&codigo_cur=" + codigocurso.value + "&codigo_per=" + docente + "&notaFinal_Dma=" + nota.value + "&grupohor_cup=" + gh + "&asistencias=" + asist + "&inasistencias=" + inasist
}

function ActualizarListaotramatricula(codigo_alu)
{
	var md=document.all.cboModalidad.value
	var cpl=document.all.cbocodigo_pes
	if (cpl==undefined)
		{cpl=""}
	else
		{cpl=cpl.value}
	location.href="frmotramatricula.asp?modalidad=" + md + "&codigo_pes=" + cpl + "&codigo_alu=" + codigo_alu
}



function ValidarTraslados(modalidad,codigo_alu)
{
	var plan=document.all.cbocodigo_pes.value
	var tipo=document.all.cboModalidad.value
	
	location.href="procesar.asp?accion=actualizartrasladoalumno&codigo_alu=" + codigo_alu + "&codigo_pes=" + plan
}

function ConsultarConvalidaciones()
{
	location.href="frmadminconvalidaciones.asp?modo=R&cicloIng_Alu=" + cbocicloingreso.value  + "&tipoConvalidacion=" + cboModalidad.value + "&codigoorigen_cac=" + cboCicloOrigen.value + "&codigodestino_cac=" + cboCicloDestino.value
}


function EnviarCambioCicloConvalidacion()
{
	//Mostrar mensaje de confirmación
 	  if (confirm("Está seguro que desea Cambiar las covalidaciones registradas a los alumnos al ciclo " + cboCicloDestino.options[cboCicloDestino.selectedIndex].text)==true){
		//Declarar array de propiedades del check marcado
		var arrAlumnos=""
		var chkAlumnos=document.all.chk
		var totalmarcados=0

		//Recorriendo 1er Iframe Con cursos curriculares/complementarios
		if (chkAlumnos.length==undefined){
			if (chkAlumnos.checked==true){
				arrAlumnos=chkAlumnos.value
				totalmarcados=1
			}
		}
		else{
	           for(var i=0;i<chkAlumnos.length;i++){
			var Control=chkAlumnos[i]
			if(Control.checked==true){
				arrAlumnos+=Control.value + ","
				totalmarcados=eval(totalmarcados)+1
			}
		   }
		}
		if (totalmarcados==0){
			alert(Mensaje[0])
		}
		else{	
			pagina="procesar.asp?accion=CambiarCicloConvalidacion&Alumnos=" + arrAlumnos + "&tipoConvalidacion=" + cboModalidad.value + "&codigoOrigen_cac=" + cboCicloOrigen.value + "&codigoDestino_cac=" + cboCicloDestino.value
			AbrirPopUp(pagina,10,10)
		}
	}
}