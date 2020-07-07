$(document).ready(function () {
    // Lugar de Nacimiento
    var codigoPaiPeru = 156;
    lf_CargarComboDepartamento($('#departamento'), codigoPaiPeru)
    // lf_CargarComboProvincia($('#prvActual'), '')
    // lf_CargarComboDistrito($('#dstActual'), '')

    function lf_LimpiarCombo($combo) {
        $combo.html('');
        if ($combo.hasClass('selectpicker')) {
            $combo.selectpicker('refresh');
        }
    }

    function lf_ResetearCombo($combo) {
        $combo.val('')
        $combo.trigger('change')
        if ($combo.hasClass('selectpicker')) {
            $combo.selectpicker('val', '');
            $combo.selectpicker('refresh');
        }
    }

    function lf_CargarComboDepartamento($combo, ls_CodigoPai) {
        lf_LimpiarCombo($combo);

        $.ajax({
            method: 'GET',
            headers: { 'Access-Control-Allow-Origin': '*' },
            url: 'http://localhost:50493/WSUSAT/WSUSAT.asmx?op=ListaPais',
            crossDomain: true,
            dataType: 'jsonp',
            data: {
                accion: 'listarDepartamento',
                ls_CodigoPai: ls_CodigoPai
            }
        }).done(function (response) {
            console.log(response)
            $combo.html('')
            for (i = 0; i < response.length; i++) {
                $combo.append('<option value="' + response[i].cod + '">' + response[i].nombre + '</option>')
            }
            lf_ResetearCombo($combo)
        }).fail(function (response) {
            console.log(response)
        });
    }

    function lf_CargarComboProvincia($combo, ls_CodDepartamento) {
        lf_LimpiarCombo($combo);

        $.ajax({
            method: 'GET',
            url: '../../DataJson/Admision/Inscripcion.aspx',
            dataType: "json",
            data: {
                accion: 'listarProvincia',
                codDepartamento: ls_CodDepartamento
            }
        }).done(function (response) {
            $combo.html('')
            for (i = 0; i < response.length; i++) {
                $combo.append('<option value="' + response[i].cod + '">' + response[i].nombre + '</option>')
            }
            lf_ResetearCombo($combo);
        }).fail(function (response) {
            console.log(response)
        });
    }

    function lf_CargarComboDistrito($combo, ls_CodProvincia) {
        lf_LimpiarCombo($combo);

        $.ajax({
            method: 'GET',
            url: '../../DataJson/Admision/Inscripcion.aspx',
            dataType: "json",
            data: {
                accion: 'listarDistrito',
                codProvincia: ls_CodProvincia
            }
        }).done(function (response) {
            $combo.html('')
            for (i = 0; i < response.length; i++) {
                $combo.append('<option value="' + response[i].cod + '">' + response[i].nombre + '</option>')
            }
            lf_ResetearCombo($combo);
        }).fail(function (response) {
            console.log(response)
        });
    }

    function lf_CargarComboColegio(ls_CodDistrito) {
        $combo = $('#institucionEducativa')
        lf_LimpiarCombo($combo);

        $.ajax({
            method: 'GET',
            url: '../../DataJson/Admision/Inscripcion.aspx',
            dataType: "json",
            data: {
                accion: 'listarColegio',
                codDistrito: ls_CodDistrito,
            }
        }).done(function (response) {
            $combo.html('')
            for (i = 0; i < response.length; i++) {
                $combo.append('<option value="' + response[i].cod + '">' + response[i].nombre + '</option>')
            }
            lf_ResetearCombo($combo)
        }).fail(function (msg) {
            console.log(msg)
        });
    }
});