function validarcargaacademica() {
    var total = 0
    var chkcursos = frmcurso.chkcursoshabiles
    
    if (chkcursos.length == undefined) {
        if (chkcursos.checked) {
            total += 1
        }
    }
    else {
        for (i = 0; i < chkcursos.length; i++) {
            var Control = chkcursos[i]
            if (Control.checked == true) {
                total = eval(total) + 1
            }
        }
    }

    if (total == 0) {
        document.all.cmdGuardar.disabled = true
    }
    else {
        document.all.cmdGuardar.disabled = false
    }
}

function ConfirmarCurso() {
    var mensaje = ""
    mensaje = "¿Está completamente seguro que desea crear 'CURSOS INDIVIDUALES' según los checks marcados?"

    if (confirm(mensaje) == true) {
        DesactivarControlesfrm(frmcurso)
        frmcurso.submit()
    }
}

function consultaraulavirtual() {
    location.href = "frmaccesoaula.asp?resultado=S&codigo_cac=" + cbocodigo_cac.value + "&codigo_cpf=" + cbocodigo_cpf.value
}

function consultarAulaVirtualPorTipoEstudio(mod, ctf, id) {
    location.href = "frmaccesoaula.asp?resultado=S&codigo_cac=" + cbocodigo_cac.value + "&codigo_cpf=" + cbocodigo_cpf.value + "&mod=" + mod + "&ctf=" + ctf + "&id=" + id
}

function AbrirRecursoSeleccionado(ctrl, pagina, tipo) {
    ElegirRecurso(ctrl)
    parent.cmdabrirrecurso.style.display = ""
    parent.cmddescargas.style.display = "none"
    if (tipo != "E") {
        //Habilitar botón para descargas de documento
        parent.cmddescargas.style.display = ""
        parent.descargas.value = tipo
    }
    parent.rutarecurso.value = pagina
}

function AbrirRecurso(ctrl) {
    desabilitarbotones()
    frarecurso.location.href = "rpte" + ctrl + ".asp"
}

function VisualizarRecurso() {
    var ruta = rutarecurso.value
    if (ruta == "")
    { alert("Seleccione el recurso que desea visualizar") }
    else {
        desabilitarbotones()
        document.all.mensaje.innerHTML = "<b>&nbsp;Espere un momento por favor...</b>"
        if (cbxrecurso.value == "evaluacion")
        { frarecurso.location.href = ruta }
        else
        { AbrirPopUp(ruta, "500", "500", "yes", "yes", "yes", "Documento") }
        document.all.mensaje.innerHTML = ""
    }
}

function VisualizarDescargas() {
    var ruta = descargas.value
    if (ruta == "")
    { alert("Seleccione el documento que desea visualizar su historial descargas") }
    else {
        desabilitarbotones()
        frarecurso.location.href = ruta
    }
}

function desabilitarbotones() {
    cmdabrirrecurso.style.display = "none"
    cmddescargas.style.display = "none"
}

function AbrirAulaProfesor(pagina) {
    var alto = window.screen.Height - 90
    var ancho = window.screen.Width - 20
    var prop = "width=" + ancho + ",height=" + alto + ",statusbar=yes,scrollbars=yes,top=0,left=0,resizable=yes,toolbar=yes,menubar=no"
    var ventana = window.open(pagina, "AulaProfesor", prop)
    ventana.location.href = pagina
    ventana = null
}

function OcultarPreguntas() {
    if (divpreguntas.style.display == "none") {
        divpreguntas.style.display = ""
    }
    else {
        divpreguntas.style.display = "none"
    }
}