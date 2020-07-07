/*
================================================================================================
	CVUSAT
	Fecha de Creaci�n: 20/04/2006
	Fecha de Modificaci�n: 20/04/2006
	Creador: Gerardo Chunga Chinguel
	Obs: Realiza las validaciones de actualizaci�n de datos del alumno
================================================================================================
*/

var ArrMensajes= new Array(2);
ArrMensajes[0] = "Estimado estudiante: \n\nS�rvase completar esta informaci�n para actualizar su Ficha Personal\n\Esta informaci�n ser� verificada y utilizada por la Universidad, para fines acad�micos\n\nGracias por su atenci�n\n\nAtte.\n\nUniversidad Cat�lica Santo Toribio de Mogrovejo\nDirecci�n de Desarrollo de Sistemas";
ArrMensajes[1] = "�Est� completamente seguro de Guardar los datos ingresados?"

function MostrarFicha(modo)
{
	if (modo=="P"){
		tblpersonal.style.display=""
		tblacademico.style.display="none"
		tblcontacto.style.display="none"
	}

	if (modo=="A"){
		tblacademico.style.display=""
		tblpersonal.style.display="none"
		tblcontacto.style.display="none"
	}

	if (modo=="C"){
		tblcontacto.style.display=""
		tblpersonal.style.display="none"
		tblacademico.style.display="none"
	}

	frmFicha.txtpasos.value=modo
}


function validarCorreo(campo,mensaje)
{
  if (mensaje=="" || mensaje==undefined){
	mensaje="Por favor debe ingresar correctamente su correo electr�nico"
  }

  var email= /@(\w+\.)*example\.(com|net|org)$/i;

  if(email.test(campo.value)){
	alert(mensaje)
	campo.focus()
	return(false)
  }

  var emailavz= /^\w+([.-]\w+)*@\w+([.-]\w+)*\.\w{2,8}$/;

  if(!emailavz.test(campo.value)){
	alert(mensaje)
	campo.focus()
	return(false)
  }

  return true;
}

function validarAnio(campo,mensaje,limite)
{
	if (campo.value.length!=4){
		alert(mensaje)
		campo.focus()
		return(false)
	}
	else{
		var dt=new Date()
		var aniocomp=eval(campo.value)
		var aniomax=eval(dt.getFullYear())-eval(limite) //Ej. 2006-12 a�os=1994
		
		if (aniocomp>aniomax){
			alert(mensaje)
			campo.focus()
			return(false)
		}
	}
	return(true)
}

function validarDocIdentidad(cbo,campo)
{
	var num=8
	switch(cbo.selectedIndex){
		case 0://boleta militar
			num=10
			break

		case 1://DNI
			num=8
			break

		case 2://Carn� de extranjer�a
			num=8
			break
		case 3://Partida de nacimiento
			num=0
			break
	}

	if (cbo.selectedIndex!=3){
		if (cbo.selectedIndex!=2 && campo.value.length!=num){
			alert("Por favor ingrese correctamente su N�mero de Documento de Identidad (M�ximo " + num + " d�gitos)")
			campo.focus()
			return(false)
		}
	
		if (cbo.selectedIndex==2 && campo.value.length<num){
			alert("Por favor ingrese correctamente su N�mero de Documento de Identidad (M�ximo " + num + " d�gitos)")
			campo.focus()
			return(false)
		}
	}

	return(true)
}

function validarTelefono(campo,mensaje,codigo)
{
	if (campo.value!="" && campo.value.length<6){
		alert(mensaje)
		campo.focus()
		return(false)
	}
	return(true)
}

function fnMensaje()
{
	document.all.dia.focus();
	//alert(ArrMensajes[0])
}

function EnviarFicha(frm,navegar)
{
   var modo=frmFicha.txtpasos.value
   var estadoFicha=false

   if (modo=="P"){
	if (frm.sexo_alu.value=="N"){
		alert("Por favor seleccione el sexo")
		frm.sexo_alu.focus()
		return(false)
	}
	
	if (frm.estadocivil_Dal.value=="NINGUNO"){
		alert("Por favor seleccione su Estado Civ�l")
		frm.estadocivil_Dal.focus()
		return(false)
	}


	if (frm.dia.value==0){
		alert("Por favor seleccione el d�a de su Nacimiento")
		frm.dia.focus()
		return(false)
	}
	if (frm.mes.value==0){
		alert("Por favor seleccione el mes de su Nacimiento")
		frm.mes.focus()
		return(false)
	}
	if (!validarAnio(frm.anio,'Por favor ingrese correctamente el A�o de Nacimiento',14)){
		return(false)
	}

	if (frm.nrodocident_alu.value.length<8){
		alert("Por favor debe especificar el n�mero de su documento de Identidad.\nSi no actualiza este dato no podr� hacer uso de los Servicios de biblioteca\nSi no tuviera DNI, realizar los tr�mites respectivos en RENIEC para la obtenci�n del mismo.")
		frm.nrodocident_alu.focus()
		return(false)
	}

	if (!validarCorreo(frm.email_alu)){
		return(false)
	}

	if (frm.email2_alu.value.length>5){
		if (!validarCorreo(frm.email2_alu,'Por favor debe ingresar correctamente su correo electr�nico Alternativo')){
			return(false)
		}
	}

	if (frm.direccion_dal.value.length<5){
		alert("Por favor debe especificar la Direcci�n donde reside actualmente")
		frm.direccion_dal.focus()
		return(false)
	}
	
	if (frm.urbanizacion_dal.value.length<5){
		alert("Por favor debe especificar correctamente el lugar de ubicaci�n (Urb./Residencial, etc) donde reside actualmente")
		frm.urbanizacion_dal.focus()
		return(false)
	}

	frm.distrito_dal.value=fralugarpersonal.cboDistrito.value
	if (frm.distrito_dal.value==-2){
		alert("Por favor seleccione el distrito, d�nde se encuentra ubicado su direcci�n")
		fralugarpersonal.cboDistrito.focus()
		return(false)
	}

	if (!validarTelefono(frm.telefonoCasa_Dal,'Por favor ingrese correctamente su Tel�fono de Casa')){
		return(false)
	}

	if (!validarTelefono(frm.telefonoMovil_Dal,'Por favor ingrese correctamente su Tel�fono M�vil')){
		return(false)
	}

	if (!validarTelefono(frm.telefonoTrabajo_Dal,'Por favor ingrese correctamente el Tel�fono del lugar d�nde labora (puede incluir anexo)')){
		return(false)
	}

	if (frm.telefonoTrabajo_Dal.value!="" && frm.tipoanexo.value=="Ax." && frm.anexo.value==""){
		alert("Por favor especifique el n�mero de anexo del �rea d�nde labora")
		frm.anexo.focus()
		return(false)
	}
	estadoFicha=true
    }

   if (modo=="A"){
	if (frm.tipocolegio_dal.value=="NINGUNO"){
		alert("Por favor el Tipo de Instituci�n Educativa")
		frm.tipocolegio_dal.focus()
		return(false)
	}

	if (!validarAnio(frm.anioegresosec_dal,'Por favor ingrese correctamente el a�o en el que Egres� sus Estudios Secundarios',-1)){
		return(false)
	}

	frm.codigo_col.value=fralugarcolegio.cboColegio.value

	if (frm.codigo_col.value=="-2"){
		alert("Por favor seleccione el Centro de Estudios d�nde realiz� sus estudios Secundarios")
		fralugarcolegio.cboColegio.focus()
		return(false)
	}

	if (frm.codigo_col.value=="0" && frm.nombrecolegio_dal.value.length<5){
		alert("Por favor especifique el Nombre de Centro de Estudios Secundarios")
		frm.nombrecolegio_dal.focus()
		return(false)
	}
	estadoFicha=true
    }

   if (modo=="C"){
	if (frm.PersonaFam_Dal.value==""){
		alert("Por favor debe ingresar la persona, con la nos podemos contactar. Principalmente un familiar")
		frm.PersonaFam_Dal.focus()
		return(false)
	}

	if (frm.direccionfam_dal.value==""){
		alert("Por favor debe ingresar la direcci�n de la persona de contacto")
		frm.direccionfam_dal.focus()
		return(false)
	}

	if (frm.urbanizacionfam_dal.value==""){
		alert("Por favor debe ingresar el lugar de Ubicaci�n (Urb./Residencial, etc) d�nde reside la persona de contacto")
		frm.urbanizacionfam_dal.focus()
		return(false)
	}

	frm.distritofam_dal.value=fralugarcontacto.cboDistrito.value

	if (frm.distritofam_dal.value=="-2"){
		alert("Por favor seleccione el Centro de Estudios d�nde realiz� sus estudios Secundarios")
		fralugarcontacto.cboDistrito.focus()
		return(false)
	}

	if (!validarTelefono(frm.telefonofam_dal,'Por favor debe ingresar el tel�fono de la persona de contacto')){
		return(false)
	}

	estadoFicha=true
    }


    if (estadoFicha==true){
	VerificarEstadoFicha(modo,navegar,frm)
    }
}

function MostrarAnexo(ctrl)
{
    if(ctrl.value=='Ax.')
	{frmFicha.anexo.style.display=""}
     else
	{frmFicha.anexo.style.display="none"}
}

function VerificarEstadoFicha(modo,navegar,frm)
{
   if (navegar!=modo){
	switch(navegar){
		case "P":
			ResaltarPestana2('0','','')
			MostrarFicha('P')
			break
		case "A":
			ResaltarPestana2('1','','')
			MostrarFicha('A')
			break
		case "C":
			ResaltarPestana2('2','','')
			MostrarFicha('C')
			break
	}
   }
   else{
	switch(modo){
		case "P":
			ResaltarPestana2('1','','')
			MostrarFicha('A')
			frm.tipocolegio_dal.focus()
			break

		case "A":
			ResaltarPestana2('2','','')
			MostrarFicha('C')
			frm.PersonaFam_Dal.focus()
			break

		case "C":
			if (confirm(ArrMensajes[1])==true){
				DesactivarControlesfrm(frm)
				frm.submit()
			}
			break
	}
   }
}

function ActivarNumDocumento(cbo,campo)
{
	if(cbo.selectedIndex==3)
		{campo.style.display="none"}
	else
		{campo.style.display=""}
}

/*
function CambiarClave(frm)
{

	if (frm.txtclaveanterior.value.length<5){
		alert("Por favor ingrese su contrase�a actual")
		frm.txtclaveactual.focus()
		return(false)
	}
}
*/