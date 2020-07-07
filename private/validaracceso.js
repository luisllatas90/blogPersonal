/*
==========================================================================================
  CV-USAT
  Fecha de Creación: 16/06/2006
  Fecha de Modificación: 16/06/2006
  Creado por	: Gerardo Chunga Chinguel
  Observación	: Validar Acceso al campus virtual
==========================================================================================
*/

function cambiartipo()
{
		var etiqueta="<b>Código Universitario</b>"
		if (frmAcceso.cbxtipo.selectedIndex==1){
			TRestudiante.style.display="none"
			//TRclave.style.display="none"
		}
		else{
			if (frmAcceso.cbxtipo.value=='T' && frmAcceso.cbxtipo.selectedIndex==2){
				etiqueta="<b>Usuario:</b>"
			}
			TRtipo.innerHTML=etiqueta
			TRestudiante.style.display=""
			//TRclave.style.display=""
	}
}

function ValidarAcceso(frm)
{
	var alto=window.screen.Height-90
	var ancho=window.screen.Width-20
	var prop="width=" + ancho +",height=" + alto +",statusbar=yes,scrollbars=no,top=0,left=0,resizable=yes,toolbar=no,menubar=no"
	var tipo='P'
		
	if (frm.cbxtipo.selectedIndex!=1){
		if (frm.Login.value.length<5){
			alert("Por favor ingrese correctamente su " + document.all.TRtipo.innerText);
	    		frm.Login.focus();
			return (false)
		}
	    	
		if (frm.Clave.value == ""){
			alert("Por favor ingrese su contraseña");
	    		frm.Clave.focus();
			return (false)
		}
	
		tipo='A'
	}
		

	if (tipo=='P'){
		//Abrir ventana de personal USAT

		/*Cerrar ventana actual*/
		/*top.window.opener=self;
		top.window.close()

		var pagina="personal/acceder.asp?cbxtipo=" + frm.cbxtipo.value
		var ventana=window.open(pagina,"PagMax",prop)
		ventana.location.href=pagina
		ventana=null*/
		//location.href=pagina
		var pagina="personal/acceder.asp?cbxtipo=" + frm.cbxtipo.value
		window.location.href=pagina
	}
	else{
		/*Abrir ventana de Alumno
		if (!window.focus){return true};
		window.open("estudiante/cargando.asp","PagAlumno",prop);*/
		mensaje.className="usatBoton"
		mensaje.innerHTML="<b>Por favor espere un momento...</b>"
		frm.cmdBuscar.disabled=false
		frm.action="estudiante/acceder.asp"
		//frm.target="PagAlumno"
		frm.submit()
		return true;
	}
}
	
