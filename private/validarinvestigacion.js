// JavaScript Document
function validaInvestigador()
{
	if (document.all.txtTitulo_Inv.value =='')
		{	alert ('Debe de ingresar un Titulo de Investigacion');
			document.all.txtTitulo_Inv.focus();
			return false; }
	if (document.frminvestigacion.cboCodigo_Tem.value=='-2')	
		{	alert ('Seleccione una Tematica');
			document.frminvestigacion.cboCodigo_Tem.focus();
			return false; }
	if (document.frminvestigacion.cboTipoInvestigacion.value=='-2')	
		{	alert ('Seleccione Tipo de Investigacion');
			document.frminvestigacion.cboTipoInvestigacion.focus();
			return false; }
	if (document.all.TxtFecIniInv.value =='')
		{	alert ('Debe de Seleccionar Fecha de Inicio de Investigacion');
			document.all.TxtFecIniInv.focus();
			return false; }
	if (document.all.TxtFecFinInv.value =='')
		{	alert ('Debe de Seleccionar Fecha de Termino de Investigacion');
			document.all.TxtFecIniInv.focus();
			return false; }
}

function validaComentarios ()
{
	if (document.all.txtAsunto.value == '')
		{	alert('Ingrese un Asunto al Comentario.'); return false;	}
	if (document.all.txtObservacion.value=='')
		{   alert('Ingrese alguna observacion.'); return false;			}
}


function validaAgregaResponsable()
{
	if (document.frmResponsable.cboTipoInvestigador.value=='1')
		{	if (document.frmResponsable.cboPersonal==undefined)
				{	alert ('Ingrese nombre o apellido y busque un personal.');	return false;	}
			if (document.frmResponsable.cboPersonal.selectedIndex==-1)
				{	alert ('Debe seleccionar un Personal de la Lista');	return false;	}
			if (document.frmResponsable.cboTipoParticipacionInvestigacion.value=='-2')
				{	alert ('Seleccione un tipo de participacion'); 
					document.frmResponsable.cboTipoParticipacionInvestigacion.focus();
					return false; } }
	
	if (document.frmResponsable.cboTipoInvestigador.value=='2')
		{	if (document.frmResponsable.cboPersonal==undefined)
				{	alert ('Ingrese nombre o apellido y busque un alumno.');	return false;	}
			if (document.frmResponsable.cboPersonal.selectedIndex==-1)
				{	alert ('Debe seleccionar un Personal de la Lista');	return false;	}
			if (document.frmResponsable.cboTipoParticipacionInvestigacion.value=='-2')
				{	alert ('Seleccione un tipo de participacion'); 
					document.frmResponsable.cboTipoParticipacionInvestigacion.focus();					
					return false;		} }
				
	if (document.frmResponsable.cboTipoInvestigador.value=='3')
		{	if (document.all.txtNombreInvestigador_Res.value=='')
				{	alert ('Ingrese un nombre de Investigador.');	return false;	}
			if (document.all.txtApellidoPatInvestigador_Res.value=='')
				{	alert ('Ingrese un Apellido Paterno de Investigador.');	return false;	}
			if (document.all.txtApellidoMatInvestigador_Res.value=='')
				{	alert ('Ingrese un Apellido Materno de Investigador.');	return false;	}
			if (document.all.txtCentroLaboral_Investigador_Res.value=='')
				{	alert ('Ingrese un Centro Laboral de Investigador.');	return false;	}
			if (document.frmResponsable.cboTipoParticipacionInvestigacion.value=='-2')
				{	alert ('Seleccione un tipo de participacion');  
					document.frmResponsable.cboTipoParticipacionInvestigacion.focus();
					return false;	} 
		}
}

function ValidaArea()
{
	if (document.all.txtNombreArea.value=='')
		{
		alert ('Nombre de area requerido');
		document.all.txtNombreArea.focus();
		return false;
		}
	if (document.all.txtProposito.value=='')
		{
		alert('Ingrese breve descripcion de proposito del área.');
		document.all.txtProposito.focus();
		return false;
		}
	}

function ValidaLinea()
{
	if (document.all.txtNombreArea.value=='')
		{
		alert ('Nombre de Linea requerido');
		document.all.txtNombreArea.focus();
		return false;
		}
	if (document.all.txtProposito.value=='')
		{
		alert('Ingrese breve descripcion de proposito de la linea de investigación.');
		document.all.txtProposito.focus();
		return false;
		}
	}

function ValidaTematica()
{
	if (document.all.txtNombreArea.value=='')
		{
		alert ('Nombre de Tematica Requerido');
		document.all.txtNombreArea.focus();
		return false;
		}
	if (document.all.txtProposito.value=='')
		{
		alert('Ingrese breve descripcion de proposito de la Tematica de investigación.');
		document.all.txtProposito.focus();
		return false;
		}
	}

function ValidaSubirProyecto()
{
	if (document.all.txtCosto.value=='')
		{
		alert ('Ingrese un costo del proyecto');
		document.all.txtCosto.focus();
		return false;
		}
	if (document.frminvestigacion.cbotipoFinanciamiento_Inv.value=='0')
		{
		alert ('Seleccione un tipo de financiamiento')
		document.frminvestigacion.cbotipoFinanciamiento_Inv.focus();
		return false;
		}
}