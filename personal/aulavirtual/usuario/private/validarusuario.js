function ValidarPermiso(itabla,ntabla,idtipopanterior)
{
	var tpublicacion=document.all.cbxidtipopublicacion.value
	switch(tpublicacion)
	{
		case "2":
			frausuarios.frm.submit;
			break;
		default:
			location.href="procesar.asp?accion=actualizartipopublicacion&idtipopublicacion=" + idtipopanterior + "&cbxidtipopublicacion=" + tpublicacion + "&idtabla=" + itabla + "&nombretabla=" + ntabla;
			break;
	}
}


function validarenviomensajes(frm)
	{
		if (frm.ListaPara.options.length == 0)
		{
			alert("Debe seleccionar algún destinatario para poder enviar el email.");
			return false;
		}
		if (frm.txtAsunto.value=="")
			{
				alert("Debe especificar el asunto del email")
				frm.txtAsunto.focus()
				return(false);
			}
		if (frm.txtMensaje.value=="")
			{
				alert("Debe de ingresar el texto del email")
				frm.txtMensaje.focus()
				return(false);				
			}
		SeleccionarDestinatarios();
		DesactivarControlesfrm(frm)
		return(true);
	}