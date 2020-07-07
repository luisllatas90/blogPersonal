function PasarCheck() {    
	$("#hdchkDisAuditiva").val($("#chkDisAuditiva").attr('checked'));
    $("#hdchkDisFisica").val($("#chkDisFisica").attr('checked'));
    $("#hdchkDisVisual").val($("#chkDisVisual").attr('checked'));
}

function Limpiar() {
    
    $("#hdcodigo_pso").val(0);
    $("#lblmensaje").val(0);
    $("#dpdepartamento").val(-1);
    $("#dpprovincia").find('option').remove().end();
    $("#dpdistrito").find('option').remove().end();    
    $("#lnkComprobarNombres").hide();
    $("#grwDeudas").hide();
    $("#grwCoincidencias").hide();
    LimpiarCajas();
    $("#txtdni").val("");
    //$('#txtdni').removeAttr("disabled");
    $("#txtdni").attr("disabled", false);
    $("#txtdni").focus();
    $("#txtFechaNac").attr("disabled", true);
}

function LimpiarCajas() {
    $("#txtAPaterno").val("");
    $("#txtAMaterno").val("");
    $("#txtNombres").val("");
    $("#txtFechaNac").val("");
    $("#dpSexo").val(-1);
    $("#dpTipoDoc").val(-1);
    $("#txtemail1").val("");
    $("#txtemail2").val("");
    $("#txtdireccion").val("");
    $("#dpprovincia").val(-1);
    $("#dpdistrito").val(-1);
    $("#txttelefono").val("");
    $("#txtcelular").val("");
    $("#dpEstadoCivil").val(-1);
    $("#dpPaisColegio").val(156);
    $("#dpprovinciacolegio").val(-1);
    $("#dpdistritocolegio").val(-1);
    $("#dpprovinciacolegio").find('option').remove().end();
    $("#dpdistritocolegio").find('option').remove().end();
    $("#dpCodigo_col").val(-1);
    $("#dpPromocion").val(-1);    
    $("#chkCentroAplicacion").attr("checked", false);
    $("#chkDisAuditiva").attr("checked", false);
    $("#chkDisFisica").attr("checked", false);
    $("#chkDisVisual").attr("checked", false);
    $("#dpPaisNacimiento").val(-1);
    $("#dpdepartamentonac").val(-1);
    $("#dpprovincianac").val(-1);
    $("#dpdistritonac").val(-1);
    $("#dpprovincianac").find('option').remove().end();
    $("#dpdistritonac").find('option').remove().end();
    $("#dpOperador").val("--Seleccione--");
    $("#txtNombresPadre").val("");
    $("#txtdireccionPadre").val("");
    $("#txturbanizacionPadre").val("");
    $("#dpdepartamentoPadre").val(-1);
    $("#dpprovinciaPadre").val(-1);
    $("#dpdistritoPadre").val(-1);
    $("#dpprovinciaPadre").find('option').remove().end();
    $("#dpdistritoPadre").find('option').remove().end();
    $("#txttelefonoPadre").val("");
    $("#txttelefonooficinaPadre").val("");
    $("#txtcelularPadre").val("");
    $("#dpOperadorPadre").val("--Seleccione--");
    $("#txtemailPadre").val("");
    $("#txtNombresApoderado").val("");
    $("#txtdireccionApoderado").val("");
    $("#txturbanizacionApoderado").val("");
    $("#dpdepartamentoApoderado").val(-1);
    $("#dpprovinciaApoderado").val(-1);
    $("#dpdistritoApoderado").val(-1);
    $("#dpprovinciaApoderado").find('option').remove().end();
    $("#dpdistritoApoderado").find('option').remove().end();
    $("#txttelefonoApoderado").val("");
    $("#txttelefonooficinaApoderado").val("");
    $("#txtcelularApoderado").val("");
    $("#dpOperadorApoderado").val("--Seleccione--");
    $("#txtemailApoderado").val("");
    $("#txtObservaciones").val("");

    //Limpiar cajas de texto ocultas
    $("#hdcodigo_cco").val(0);
    $("#hdgestionanotas").val(0);
    $("#hdcodigo_cpf").val(0);                    
    $("#hdcodigo_pso").val(0);
    $("#hdtxtAPaterno").val(0);
    $("#hdtxtAMaterno").val(0);
    $("#hdtxtNombres").val(0);
    $("#hdtxtFechaNac").val(0);
    $("#hddpSexo").val(0);
    $("#hddpTipoDoc").val(0);
    $("#hdtxtdni").val(0);
    $("#hdtxtemail1").val(0);
    $("#hdtxtemail2").val(0);
    $("#hdtxtdireccion").val(0);
    $("#hddpdistrito").val(0);
    $("#hdtxttelefono").val(0);
    $("#hdtxtcelular").val(0);
    $("#hddpEstadoCivil").val(0);
    $("#hddpPaisNacimiento").val(0);
    $("#hddpdistritonac").val(0);
    $("#hddpOperador").val(0);
    $("#hdcodigobd").val(0);
    //$("#hdPaso").val(0);
    $("#hdchkDisAuditiva").val(0);
    $("#hdchkDisFisica").val(0);
    $("#hdchkDisVisual").val(0);
    
    if ($("#hdcodigo_cpf").val() != 9) {
        $("#dpModalidad").val(-1);
    }
}

function Salir() {

    window.location = "pec/lstinscripcion.aspx?mod=" + $("#hdurcodigo_test").val() + "&id=" + $("#hdurlid").val() + "&ctf=" + $("#hdurlctf").val() + "&cco=" + $("#hdurlcco").val();
}

function IrGeneracionCargos() {
    //Todas las personas se registran como alumnos  tcl = "E"              
    window.location = "pec/frmgeneracioncargos.aspx?mod=" + $("#hdurcodigo_test").val() + "&id=" + $("#hdurlid").val() + "&ctf=" + $("#hdurlctf").val() + "&cco=" + $("#hdurlcco").val() + "&tcl=E&cli=" + $("#hdcodigobd").val() + "&pso=" + $("#hdcodigopso").val() + "&tab=3"; 
}