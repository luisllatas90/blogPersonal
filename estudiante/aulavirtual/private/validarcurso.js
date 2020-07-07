function validarcursovirtual(formulario,capl)
{
  if (capl!="4"){
	 if (formulario.titulocursovirtual.value == ""){
	    alert("Ingrese la denominación del curso virtual.");
	   formulario.titulocursovirtual.focus();
	    return(false);
  	}
  }
 
  DesactivarControlesfrm(formulario)
  return(true)
}

function validareliminarpermiso(formulario)
{
  var numTotal=formulario.chk.length
  var total=0

  for(i=0;i<numTotal;i++){
	if (formulario.chk[i].checked==true)
		{total+=total+1}
  }

  if (total==0){
	alert("Debe seleccionar al menos un participante del curso virtual, para eliminar el permiso")
	return(false)
  }
  
  if (confirm("¿Está seguro que desea eliminar a los participantes seleccionados, del curso virtual?")==true)
	{return(true)}
}


function AbrirCurso(modo,idcurso,idapl,idtfu)
{
	var titulocurso=document.getElementById("curso" + idcurso).innerText
 	switch(modo)
	{
		case "A":
			AbrirPopUp("frmcurso.asp?accion=agregarcurso&codigo_apl=" + idapl + "&codigo_tfu=" + idtfu,'320','600')
			break
		case "M":
			AbrirPopUp("frmadministrar.asp?accion=modificarcurso&codigo_apl=" + idapl + "&idcursovirtual=" + idcurso + "&titulocurso=" + titulocurso + "&codigo_tfu=" + idtfu,'450','700')
			break
	}
}

function EliminarCurso(idcurso)
{
	var pagina="procesar.asp?accion=eliminarcurso&idcursovirtual=" + idcurso
	var mensaje="Acción irreversible. ¿Está complétamente seguro que desea Eliminar el curso virtual seleccionada?\nRecuerde que se eliminarán todos los registros relacionados con el Curso Virtual"
	Eliminar(mensaje,pagina)
}

function AbrirCargaAcademica(fila,ciclo,cd,nd,pagina,lp)
{
	var Celda = fila.getElementsByTagName('td')
	var cp=fila.codigo_cup
	var cc=Celda[1].innerHTML
	var nc=Celda[2].innerHTML
	var gh=Celda[3].innerHTML
	var sc=Celda[4].innerHTML

	location.href="lstalumnosmatriculados.asp?codigo_cac=" + ciclo + "&codigo_per=" + cd + "&nombre_per=" + nd + "&codigo_cup=" + cp + "&identificador_cur=" + cc + "&nombre_cur=" + nc + "&grupohor_cur=" + gh + "&ciclo_cur=" + sc + "&pagina=" + pagina + "&codigo_prof=" + lp
}


function MatricularCursoVirtual(cd,fi,ff,cup)
{
	DesactivarControlesfrm(frmLista)

	var ctrl=document.all.chk
	var nc=nombre_cur.innerText + '(' + grupo_cur.innerText + ')'
	var descrip='Código del curso:' + identificador_cur.innerText + ' / ' + grupo_cur.innerText + ' / ' + ciclo_cur.innerText

	var arrCU=""
	var arrNU=""
	var arrEU=""

	for(i=0;i<ctrl.length;i++)
	{
		if (ctrl[i].checked==true){
			arrCU+=ctrl[i].value + ";"
			arrNU+=ctrl[i].nombre_alu + ";"
			arrEU+=ctrl[i].email_alu + ";"
		}
	}

	location.href="procesar.asp?accion=matricularcurso&codigo_cup=" + cup + "&codigo_prof=" + cd + "&titulocursovirtual=" + nc + "&descripcion=" + descrip + "&fechainicio=" + fi + "&fechafin=" + ff + "&codigosuniversitarios=" + arrCU + "&nombrealumnos=" + arrNU + "&emailalumnos=" + arrEU
}

function EnviarDatosAcceso(codigo_cv)
{
	DesactivarControlesfrm(frmLista)
	var ctrl=document.all.chk
	var arrTU=""
	var arrCU=""
	var arrNU=""
	var arrPU=""
	var arrEU=""

	for(i=0;i<ctrl.length;i++)
	{
		if (ctrl[i].checked==true){
			arrTU+=ctrl[i].idtipo_usu + ";"
			arrCU+=ctrl[i].codigo_usu + ";"
			arrNU+=ctrl[i].nombre_usu + ";"
			arrPU+=ctrl[i].clave_usu + ";"
			arrEU+=ctrl[i].email_usu + ";"
		}
	}

	location.href="enviaracceso.asp?ambito=A&idcursovirtual=" + codigo_cv + "&tipos=" + arrTU + "&codigos=" + arrCU + "&nombres=" + arrNU + "&claves=" + arrPU + "&emails=" + arrEU
}

function actualizarcorreo(correo,chk)
{
	if (correo.value!="")
		{chk.email_alu=correo.value}

}

/*
	Acciones para visualizar las estadísticas del recurso

*/


function AbrirRecursoSeleccionado(ctrl,pagina,tipo)
{
	ElegirRecurso(ctrl)
	parent.cmdabrirrecurso.style.display=""
	parent.cmddescargas.style.display="none"
	if (tipo!="E"){
		//Habilitar botón para descargas de documento
		parent.cmddescargas.style.display=""
		parent.descargas.value=tipo
	}
	parent.rutarecurso.value=pagina
}

	function AbrirRecurso(ctrl)
	{
		desabilitarbotones()
		frarecurso.location.href="rpte" + ctrl + ".asp"
	}
	
	function VisualizarRecurso()
	{
		var ruta=rutarecurso.value
			if (ruta=="")
				{alert("Seleccione el recurso que desea visualizar")}
			else{
				AbrirMensaje('../../images/')
				desabilitarbotones()
				if (cbxrecurso.value=="evaluacion")
					{frarecurso.location.href=ruta}
				else
					{AbrirPopUp(ruta,"500","600","yes","yes","yes")}
			}
	}
	
	function VisualizarDescargas()
	{
		var ruta=descargas.value
			if (ruta=="")
				{alert("Seleccione el documento que desea visualizar su historial descargas")}
			else{
				desabilitarbotones()
				frarecurso.location.href=ruta
			}
	}
	
	function desabilitarbotones()
	{
		cmdabrirrecurso.style.display="none"
		cmddescargas.style.display="none"
	}


function validarcargaacademica()
{
	var total=0
	var chkcursos=frmcurso.chkcursoshabiles

	if (chkcursos.length==undefined){
		if (chkcursos.checked){
			total+=1
		}
	}
	else{
		for (i=0; i<chkcursos.length;i++){
		       var Control=chkcursos[i]
			if (Control.checked==true){
				total=eval(total)+1
			}
		}
	}

	if (total==0){
		document.all.cmdGuardar.disabled=true
	}
	else{
		document.all.cmdGuardar.disabled=false
	}
}

function ConfirmarCurso()
{
	var mensaje=""
	if (frmcurso.chkagrupar.checked==true){
		mensaje="¿Está completamente seguro que desea crear 'UN SÓLO CURSO' AGRUPANDO los checks marcados?"
	}
	else{
		mensaje="¿Está completamente seguro que desea crear 'CURSOS INDIVIDUALES' según los checks marcados?"
	}		

	if (confirm(mensaje)==true){
		DesactivarControlesfrm(frmcurso)
		frmcurso.submit()
	}
}