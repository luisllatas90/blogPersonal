/*
============================================================================================
CVUSAT
Fecha de Creación: 23/02/2006
Fecha de Modificación: 26/02/2006
Creador: Gerardo Chunga Chinguel
Obs: Realiza las validaciones y procedimientos para el módulo Horarios
============================================================================================
*/

var Mensaje = new Array(10);
Mensaje[0] = "Está seguro que desea Eliminar horario seleccionado"
Mensaje[1] = "La hora fin debe ser mayor que la hora de inicio"
Mensaje[2] = "¿Está completamente seguro que desea Habilitar, el llenado de Registro de Notas?"

function ResaltarHoras(idcheck) {
    var dia = idcheck.name
    var Celda = document.getElementById("TD" + idcheck.id.substr(2, 10));
    var Color = ""

    if (idcheck.checked == true) {
        Celda.className = dia
    }
    else {
        Celda.className = "cajas"
    }
}


function AccionLineaHorario(modo, cp, ca, cd, horario, reg) {
    var docente = document.all.cbocodigo_per
    if (docente == undefined)
    { docente = 1 }
    else
    { docente = document.all.cbocodigo_per.value }

    var pagina = "frmasignarhorario.asp?codigo_per=" + docente + "&codigo_cup=" + cp + "&codigo_cac=" + ca + "&codigo_dac=" + cd + "&codigo_Lho=" + horario + "&recordid=" + reg

    if (modo == "Eliminar") {
        ConfirmarEliminar(Mensaje[0], pagina)
    }
    else {
        location.href = pagina + "&Modalidad=" + modo
    }
}

function validarlineahorario(frm) {
    var ini = frm.cbohoraini.value
    var fin = frm.cbohorafin.value

    if (parseInt(ini) >= parseInt(fin)) {
        alert(Mensaje[1])
        frm.cbohorafin.focus()
        return (false)
    }
    return (true);
}

function GenerarEncabezadoHorario(tipo) {
    var cadHTML = ""
    var str1 = ""

    if (tipo == "HE") { //Horario por escuela
        str1 = "HORARIOS: " + cbocodigo_cpf.options[cbocodigo_cpf.selectedIndex].text
        str1 += " (" + cbocodigo_cac.options[cbocodigo_cac.selectedIndex].text + ")"
    }

    window.frahorario.document.title = str1
}

function GenerarVistaHorario(modo, estado) {
    var cac = cbocodigo_cac.value
    var dcac = cbocodigo_cac.options[cbocodigo_cac.selectedIndex].text
    
    var pagina = ""

    if (modo == "HE") { //Horarios por Escuela
        var escuela = cbocodigo_cpf.value
        var grupo = cbogrupohor_cup.value
        var ciclo = cbociclos.value
        cmdImprimir.style.display = ""
        /*if (chkpagina.checked==true){
        pagina="vsthorariocursociclo.asp"	
        }
        else{*/
        pagina = "vsthorario.asp"
        //}
        frahorario.location.href = pagina + "?codigo_cac=" + cac + "&codigo_cpf=" + escuela + "&grupohor_cup=" + grupo + "&ciclo_cur=" + ciclo
    }

    if (modo == "HD") { //Horario por profesor
        var cper = cbocodigo_per.value
        var ctest = cboTipoEstudio.value

        var titulo = "HORARIOS: " + cbocodigo_per.options[cbocodigo_per.selectedIndex].text
        titulo += " (" + cbocodigo_cac.options[cbocodigo_cac.selectedIndex].text + ")"

        location.href = "vsthorariodocente.asp?codigo_cac=" + cac + "&codigo_per=" + cper + "&titulo=" + titulo + "&codigo_test=" + ctest + "&modo=A"
    }

    if (modo == "HC") { //Horarios por ciclo y profesor
        location.href = "vsthorariodocente.asp?codigo_cac=" + cac + "&descripcion_cac=" + dcac + "&modo=" + estado
    }

}

function GenerarVistaHorario_clonado(modo, estado) {
    var cac = cbocodigo_cac.value
    var dcac = cbocodigo_cac.options[cbocodigo_cac.selectedIndex].text
    var pagina = ""

    if (modo == "HE") { //Horarios por Escuela
        var escuela = cbocodigo_cpf.value
        var grupo = cbogrupohor_cup.value
        var ciclo = cbociclos.value
        cmdImprimir.style.display = ""
        /*if (chkpagina.checked==true){
        pagina="vsthorariocursociclo.asp"	
        }
        else{*/
        pagina = "vsthorario_clonado.asp"
        //}
        frahorario.location.href = pagina + "?codigo_cac=" + cac + "&codigo_cpf=" + escuela + "&grupohor_cup=" + grupo + "&ciclo_cur=" + ciclo
    }

    if (modo == "HD") { //Horario por profesor
        var cper = cbocodigo_per.value

        var titulo = "HORARIOS: " + cbocodigo_per.options[cbocodigo_per.selectedIndex].text
        titulo += " (" + cbocodigo_cac.options[cbocodigo_cac.selectedIndex].text + ")"

        location.href = "vsthorariodocente.asp?codigo_cac=" + cac + "&codigo_per=" + cper + "&titulo=" + titulo + "&modo=A"
    }

    if (modo == "HC") { //Horarios por ciclo y profesor
        location.href = "vsthorariodocente.asp?codigo_cac=" + cac + "&descripcion_cac=" + dcac + "&modo=" + estado
    }

}


function ImprimirVista(tipo) {
    GenerarEncabezadoHorario(tipo)
    window.frahorario.focus()
    window.frahorario.print()
}