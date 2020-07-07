
var Mensaje= new Array(10);
Mensaje[0] = "¿Está seguro que desea Eliminar el Libro seleccionado?"
Mensaje[1] = "¿Está seguro que desea Enviar el pedido seleccionado para su Aprobación?"
Mensaje[2] = "¿Está seguro que desea Eliminar el pedido seleccionado?"
Mensaje[3] = "¿Está completamente seguro que desea registrar la bibliografía?"

function AbrirFicha(pagina,obj){

	var fila=window.event.srcElement.parentElement
	if (fila.tagName=="TR"){

		nombreficha=obj.innerText
		//var codigo_ped=fila.cells[1].innerText	
		codigo_ped = obj.id.substring(4,obj.id.length)
		fradetalle.location.href=pagina + "&codigo_ped=" + codigo_ped
	}
}

function AbrirPedido(pagina)
{	fradetalle.location.href=pagina
}

function AbrirDetallesdePedido(pagina) {
//alert(document.all.txtelegido.value)
//alert(pagina)
	fradetalle.location.href=pagina + "&codigo_ped=" + document.all.txtelegido.value
}

function AbrirPedido(modo,param1,param2)
{
	switch(modo)
	{
		case 'A':
				if (event.srcElement.name=="cmdGuardar"){
					location.href="procesar.asp?accion=EnviarPedido&codigo_ped=" + param1 + "&tipoenvio=" + param2
				}
				else{				
					if (confirm(Mensaje[1])==true){
						location.href="procesar.asp?accion=EnviarPedido&codigo_ped=" + param1 + "&tipoenvio=" + param2
					}
				}
				break
		case 'M':
				location.href="frmpedido.asp?modo=M&codigo_ped=" + param1
				break
				
		case 'E':
				if (confirm(Mensaje[2])==true){
					location.href="procesar.asp?accion=eliminarpedido&codigo_ped=" + param1
				}
				break
	}
}

function AbrirPedidoEnviado(modo,param1,param2,param3)
{
	switch(modo)
	{
		case 'A':
				if (event.srcElement.name=="cmdGuardar"){
					location.href="procesar.asp?accion=EnviarPedido&codigo_ped=" + param1 + "&tipoenvio=" + param2
				}
				else{				
					if (confirm(Mensaje[1])==true){
						location.href="procesar.asp?accion=EnviarPedido&codigo_ped=" + param1 + "&tipoenvio=" + param2+"%&tipo="+param3
					}
				}
				break
		case 'M':
				location.href="frmpedido.asp?modo=M&codigo_ped=" + param1
				break
				
		case 'E':
				if (confirm(Mensaje[2])==true){
					location.href="procesar.asp?accion=eliminarpedido&codigo_ped=" + param1
				}
				break
	}
}


function ValidarCentroCosto(obj,modo)
{
	
if (obj.value!="-2")
	{document.all.cmdAgregar.disabled=false}
else
	{document.all.cmdAgregar.disabled=true}
	
//enviar datos al DETALLE

	if (modo=="M"){
		fradetalle.location.href="lstdetallepedido.asp"
	}
}

function SeleccionarPedidos_v2(obj){
	SeleccionarFila()
	txtelegido.value=obj.id.substring(4,obj.id.length)
}

function SeleccionarDetalle(obj)
{
	SeleccionarFila()
	
	txtelegido.value=obj.id.substring(4,obj.id.length)
	HabilitarBotones('P',false)
}

function SeleccionarPedido(obj)
{
	SeleccionarFila()
	
	txtelegido.value=obj.id.substring(4,obj.id.length)
	HabilitarBotones('D',false)
}

function HabilitarBotones(tipo,estado)
{
	if (tipo=='D'){
		document.all.cmdModificar.disabled=estado
		document.all.cmdEliminar.disabled=estado
		document.all.cmdDetalle.disabled=estado
	}
	
	if (tipo=='P'){
		parent.document.all.cmdModificar.disabled=estado
		parent.document.all.cmdEliminar.disabled=estado
//		parent.document.all.cmdAgregar.disabled=estado
//		parent.document.all.cmdDetalle.disabled=estado
	}

	if (tipo=='LP'){
		document.all.cmdEliminar.disabled=true
	}
	if (tipo=='DE'){
		document.all.cmdDetalle.disabled=true
	}		
}

function HabilitarBotonesLstPedidos(tipo){
	if (tipo=='PE'){
		document.all.cmdDetalle.disabled=false;
	}
	else{
		document.all.cmdDetalle.disabled=true;
	}	
}




function AbrirDetalleDeCatalogo(param1,titulo,precio,moneda){

	var pagina=""
	pagina="frmagregarbibliografia.asp?accion=agregarcatalogo&codigo_cco="+param1+"&titulo_cat="+titulo+"&moneda_Cat="+moneda+"&preciounit_cat="+precio
	AbrirPopUp(pagina,'450','590')

}



function MostrarOtro(valor)
{
	if (valor=="0"){
		document.all.trOtraJustificacion.style.display=""
	}
	else{
		document.all.trOtraJustificacion.style.display="none"
		document.all.txtJustificacion_Dpe.value=""
	}
}

function BuscarAsignatura()
{
	if (frmPedido.txtCurso.value.length<3){
		alert("Por favor especifique la Asignatura que desea Buscar")
		frmPedido.txtCurso.focus()
		return(false)
	}
	document.all.fraCurso.style.display="none"
	trListaAsignaturas.style.display=""
	lblmensajecurso.innerHTML="<b>Buscando curso, espere un momento...</b>"
	trBlanco.style.display="none"

	fraCurso.location.href="frmbuscarcurso.asp?criterio=" + frmPedido.txtCurso.value
}



function ValidarRegPedido(frm,modo,idLibro)
{	

	if (frm.cbocodigo_Jpe.value=="0" && frm.txtJustificacion_Dpe.value.lenght<3){
		alert("Debe ingresar la justificación de la solicitud de Pedido")
		frm.txtJustificacion_Dpe.focus()
		return(false)
	}
	
	if (frm.txtCantidad_Dpe.value=="" || frm.txtCantidad_Dpe.value=="0"){
		alert("Debe especificar la cantidad de Ejemplares a solicitar")
		frm.txtCantidad_Dpe.focus()
		return(false)
	}
//	alert(frm.txtCodigo_cur)
	if (frm.txtCurso!=undefined){
		if (frm.txtCodigo_cur.value==""){
			alert("Debe seleccionar la asignatura para la cual solicita la bibliografía,\n\haciendo clic en el botón [Buscar]")
			// frm.txtCurso.focus()
			return(false)
		}
	}
	
	if (frm.txtTema_Dpe.value.length<=5){
		alert("Debe especificar el tema del sílabos u otro tema de investigación")
		frm.txtTema_Dpe.focus()
		return(false)	
	}
	
	mensaje.innerHTML="<b>Por favor espere un momento...</b>"
	frm.cmdGuardar.disabled=true
	frm.cmdCancelar.disabled=true
	frm.submit()
	
	//if (modo=="A"){
		//Cambiar de estado del libro seleccionado
	//	var Argumentos=window.dialogArguments
		//	Argumentos.Libro=idLibro    //frm.txtLibro.value
		//	alert(frm.txtLibro.value)
	//		Argumentos.AgregarCanasta()
	//}
}

function ValidarTotalPedidos(total)
{
	//Activar Botones de envio
	parent.document.all.spTotal.innerHTML=total	
	if (total!="0"){
		parent.document.all.cmdGuardar.disabled=false
		parent.document.all.cmdEnviar.disabled=false
	}
	else{
		parent.document.all.cmdGuardar.disabled=true
		parent.document.all.cmdEnviar.disabled=true
	}
	
	//Activar Botón de Agregar libros
	if (parent.document.all.cboCodigo_cco.value!="-2")
		{parent.document.all.cmdAgregar.disabled=false}
	else
		{parent.document.all.cmdAgregar.disabled=true}
}

function ValidarNvaBibliografia(codigo_cco)
{
	
	//alert (codigo_cco)
	if (frmcatalogo.titulo_cat.value.length<3){
		alert("Ingrese el título de la bibliografía")
		frmcatalogo.titulo_cat.focus()
		return(false)
	}
	
	if (frmcatalogo.cboautor_cat==undefined){
		alert("Seleccione el autor de la bibliografía")
		return(false)	
	}

	if (frmcatalogo.cboautor_cat.value==""){
		alert("Seleccione el autor de la bibliografía")
		frmcatalogo.cboautor_cat.focus()
		return(false)		
	}
	
	if (frmcatalogo.tipoMaterial_Cat.value==""){
		alert("Seleccione el tipo de bibliografía")
		frmcatalogo.tipoMaterial_Cat.focus()
		return(false)		
	}

/*	
	if (frmcatalogo.cbomateria_cat.value=="-2"){
		alert("Seleccione la materia de clasificación de la bibliografía")
		frmcatalogo.cbomateria_cat.focus()
		return(false)		
	}
	
//	alert(frmcatalogo.cboeditorial_cat.value)
	if (frmcatalogo.cboeditorial_cat.value==609){
		alert("Seleccione la editorial de la bibliografía")
		frmcatalogo.cboeditorial_cat.focus()
		return(false)		
	}

	if (frmcatalogo.NumEdic.value.length<1){
		alert("Ingrese el número de la edición")
		frmcatalogo.NumEdic.focus()
		return(false)		
	}	

	if (frmcatalogo.cbolugar_cat.value=="1"){
		alert("Seleccione el lugar de publicación de la bibliografía")
		frmcatalogo.cbolugar_cat.focus()
		return(false)		
	}*/		
	
/*
	if (frmcatalogo.edicion_cat.value.length<4){
		alert("Ingrese el año de publicación de la bibliografía")
		frmcatalogo.edicion_cat.focus()
		return(false)		
	}	
*/
	if (frmcatalogo.preciounit_cat.length<1){
		alert("Ingrese un precio referencial para la Bibliografía")
		frmcatalogo.preciounit_cat.focus()
		return(false)		
	}	
	
	if (confirm(Mensaje[3])==true){
		frmcatalogo.action="procesar.asp?accion=agregarnvocatalogo&codigo_cco="+codigo_cco
		//+"&moneda="+frmcatalogo.cbomoneda_cat.value+"&precio="+frmcatalogo.preciounit_cat.value
		//alert (codigo_cco)
		frmcatalogo.submit()
	}
}

function AlmacenarTextoLista(txt,cbo)
{
	txt.value=cbo.options[cbo.selectedIndex].text
}

function AbrirDetallePedido(modo,param1)
{
	var codigo_cco=0
	var pagina=""	
	switch(modo)
		{
		case "A":
				var codigo_cco=document.all.cboCodigo_cco.value		
				var tipoBD=document.all.cbotipoBD
				var textoBD=tipoBD.options[tipoBD.selectedIndex].text
				var Libro=null
				pagina="frmdetallepedido.asp?accion=agregardetallepedido&tipoBD=" + tipoBD.value + "&textoBD=" + textoBD + "&idLibro=" + param1 + "&codigo_cco=" + codigo_cco				
				showModalDialog(pagina,window,"dialogWidth:590px;dialogHeight:400px;status:no;help:no;center:yes")
				break				
		case "M":
				var codigo_cco=document.all.cboCodigo_cco.value
				pagina="frmdetallepedido.asp?accion=agregardetallepedido&idLibro=0&codigo_dpe=" + fraDetalle.document.all.txtelegido.value + "&codigo_cco=" + codigo_cco
				//showModalDialog(pagina,window,"dialogWidth:590px;dialogHeight:400px;status:no;help:no;center:yes")
				AbrirPopUp(pagina,'450','590')
				break
		
		case "E":
				if (confirm(Mensaje[0])==true){
					pagina="procesar.asp?accion=eliminardetallepedido&codigo_dpe=" + fraDetalle.document.all.txtelegido.value
					HabilitarBotones('D',true)
					fraDetalle.location.href=pagina
				}
				break				
		case "B":
		codigo_cco=document.all.cboCodigo_cco.value

			if (codigo_cco!=-2){				
				pagina="frmbuscarbibliografia.asp?codigo_cco=" + codigo_cco
				location.href=pagina
				break
			}else{
				alert ("Seleccione el área por la cual va a elevar el pedido.")
				break			
			}
			
		
		case "N":
				pagina="frmagregarbibliografia.asp?accion=agregarcatalogo&codigo_cco=" + param1
				AbrirPopUp(pagina,'450','590')
				break
		}
}

