$(document).ready(function () {
    fnLoading(true);

    var dt = fnCreateDataTableBasic('tComunicacion', 0, 'asc');
    var dt = fnCreateDataTableBasic('tAcuerdo', 1, 'asc');

    ope = fnOperacion(1);
    //console.log(ope)
    //rpta = fnvalidaSession()
    //alert(rpta)
    //if (rpta == true) {
    ObteneridInteresado();
    fnListarTelefonos();
    fnListarEventosPorInteresado();
    fnListarComunicacion(false);
    fnListarInteresado();
    fnListaRequisitosIngresante();

    fnEstadoComunicacion();
    fnMotivo("R");    //Listar en Ventana de Registro

    fnTipoComunicacion("R");    //Listar en Ventana de Registro
    fnCargarCboEvento();
    fnCargarCboOrigen();
    fnLlenarTipoEstudioCombo(); //Adicionado por @jquepuy | 22ENE2019
    fnListarAnexo();

    $('#btnGuardar').click(fnGuardarComunicacion);
    $('#BtnCancelar').click(fnCancelarComunicacion);
    $('#btnGuardarAcuerdo').click(fnGuardarAcuerdo);

    //} else {
    //  window.location.href = rpta
    //}
    fnLoading(false);

    $('#mdComunicacion').on('show.bs.modal', function (event) {
        var button = $(event.relatedTarget) // Botón que activó el modal
        //alert('--')

        if (button.attr("id") == "btnAgregarComunicacion") {
            $('#hdcod').remove();
            $('#frmRegistro').append('<input type="hidden" id="hdcod" name="hdcod" value="0" />');
            Limpiar();
            $("#Contador_Com").html("0 Caracteres");
        } else if (button.attr("id") == "btnE") {
            $('#hdcod').remove();
            $('#frmRegistro').append('<input type="hidden" id="hdcod" name="hdcod" value="' + button.attr("hdc") + '" />');
            EditComunicacion();
        }
    });

    $('#rowEvento').collapse({
        toggle: false
    });

    $('#rowNrosCallcenter').collapse({
        toggle: false
    });

    $('#mdAcuerdo').on('show.bs.modal', function (event) {
        var button = $(event.relatedTarget) // Botón que activó el modal
        //alert(button.attr("id"))
        if (button.attr("id") == "btnAdd") {
            $('#hdcod').remove();
            $('#hdID').remove();
            $('#frmAcuerdo').append('<input type="hidden" id="hdID" name="hdID" value="0" />');
            $('#frmAcuerdo').append('<input type="hidden" id="hdcod" name="hdcod" value="' + button.attr("hdc") + '" />');
            LimpiarAcuerdo();
            $("#Contador_Acu").html("0 Caracteres");
        } else if (button.attr("id") == "btnEditAcuerdo") {
            $('#hdcod').remove();
            $('#hdID').remove();
            $('#frmAcuerdo').append('<input type="hidden" id="hdID" name="hdID" value="' + button.attr("hdc") + '" />');
            $('#frmAcuerdo').append('<input type="hidden" id="hdcod" name="hdcod" value="' + $("#codigo_com").val() + '" />');
            EditAcuerdo();
        }
    });

    $("#txtdetalle").keyup(function () {
        if ($(this).val() != '') {
            longitud = $(this).val();
            longitud = longitud.length;
            $("#Contador_Com").html(longitud + " Caracteres");
            // if (longitud <= 400) {
            //     $("#Contador_Com").html(longitud + " Caracteres");
            // } else {
            //     fnMensaje("error", "Longitud Máxima de Detalle 400 Caracteres");
            //     $("#txtdetalle").val($(this).val().substring(0, 400));
            //     $("#Contador_Acu").html("400 Caracteres");
            // }
        } else {
            $("#Contador_Com").html("0 Caracteres");
        }
    });

    $("#txtdetalle_acu").keyup(function () {
        if ($(this).val() != '') {
            longitud = $(this).val();
            longitud = longitud.length;
            if (longitud <= 300) {
                $("#Contador_Acu").html(longitud + " Caracteres");
            } else {
                fnMensaje("error", "Longitud Máxima de Detalle 300 Caracteres");
                $("#txtdetalle_acu").val($(this).val().substring(0, 300));
                $("#Contador_Acu").html("300 Caracteres");
            }
        } else {
            $("#Contador_Acu").html("0 Caracteres");
        }
    });

    $('#txtFecha_acu').datepicker().on('changeDate', function (ev) {
        $('#txtFecha_acu').datepicker('hide');
    });

    $('#txtHora_acu').timepicker({
        timeFormat: 'h:mm p',
        interval: 60,
        dynamic: true,
        dropdown: true,
        scrollbar: true
    });

    $('#cboEstadoComunicacion').on('change', function (e) {
        if ($(this).find('option:selected').text() == 'NO INTERESADO EN EVENTO') {
            if ($('#hdcod').val() == '0') {
                $('#cboEvento').val($('#hdCodigoEveIni').val());
            } else {
                $('#cboEvento').val($('#hdCodigoEve').val());
            }
            $('#rowEvento').collapse('show');
        } else {
            $('#cboEvento').val('');
            $('#rowEvento').collapse('hide');
        }
    });

    $('#txtNroAnexo').on('change', function (e) {
        var tipoTelefonoFijo = ($('#cboTipoComunicacionR').find('option:selected').text().indexOf('FONO FIJO') !== -1);
        if (tipoTelefonoFijo) {
            $(this).data('lastValue', $(this).val());
        }
    });

    $('#txtNroInteresado').on('change', function (e) {
        var tipoTelefonoFijo = ($('#cboTipoComunicacionR').find('option:selected').text().indexOf('FONO FIJO') !== -1);
        var valCboNroInteresado = $('#cboNroInteresado').val();
        if (tipoTelefonoFijo && valCboNroInteresado == '') {
            $(this).data('lastValue', $(this).val());
        }
    });

    $('#cboTipoComunicacionR').on('change', function (e) {
        if ($(this).find('option:selected').text() == 'WHATSAPP') {
            $('#chat-wsp').show();
        } else {
            $('#chat-wsp').hide();
        }

        var llamadaVerificada = $('#rowNrosCallcenter').data('llamadaVerificada');
        var llamadaTelefonoFijo = ($(this).find('option:selected').text().indexOf('FONO FIJO') !== -1);

        if (llamadaTelefonoFijo) {
            $('#rowNrosCallcenter').collapse('show');
            $('#rowNrosCallcenter').removeClass('div-disabled');

            if (llamadaVerificada) {
                $('#rowNrosMensaje').collapse('show');
                $('#rowNrosCallcenter').addClass('div-disabled');
            } else {
                $('#rowNrosMensaje').collapse('hide');
            }
        } else {
            $('#rowNrosCallcenter').collapse('hide');
            $('#rowNrosMensaje').collapse('hide');
        }

        if (!llamadaVerificada) {
            if (llamadaTelefonoFijo) {
                var saliente = ($(this).find('option:selected').text().indexOf('SALIENTE') !== -1);

                if (saliente) {
                    $('#lblChkNroInteresado').show();
                } else {
                    $('#lblChkNroInteresado').hide();
                }

                $('#txtNroAnexo').val($('#txtNroAnexo').data('lastValue'));
                $('#txtNroAnexo').trigger('change');

                $('#cboNroInteresado').val($('#cboNroInteresado').data('lastValue'));
                $('#cboNroInteresado').trigger('change');

            } else {
                $('#rowNrosCallcenter').collapse('hide');
                $('#txtNroAnexo').val('');
                $('#chkNroAnexo').prop('checked', false);
                $('#cboNroInteresado').val('');
                $('#cboNroInteresado').trigger('change');
            }
            $('#chkNroAnexo').trigger('change');
            // $(this).data('oldText', $(this).find('option:selected').text());
        }
    });
    $('#cboTipoComunicacionR').trigger('change'); // Disparo el evento al inicio

    $('#chat-wsp').on('click', function (e) {
        var celular = $('#info-celular').html();
        var codGrado = $('#hdGrado').val();
        var interesado = encodeURI($('#lblInteresado').html());
        var carrera = encodeURI($('#lblInteres').html());
        var preguntas = '';
        var text = '';

        var esMedicina = $('#lblInteres').data('es-medicina');

        if (esMedicina) {
            preguntas = 'El%20examen%20consta%20de%2060%20preguntas%20divididas%20en%206%20%C3%A1reas%3A%20anatom%C3%ADa%2C%20biolog%C3%ADa%2C%20f%C3%ADsica%2C%20qu%C3%ADmica%2C%20comunicaci%C3%B3n%20y%20matem%C3%A1tica.'
        } else {
            preguntas = 'El%20examen%20contiene%20%2A50%2A%20preguntas%3A%2011%20de%20redacci%C3%B3n%2C%2014%20de%20comprensi%C3%B3n%20y%2025%20de%20matem%C3%A1tica.%0ASi%20postulas%20a%20una%20carrera%20de%20la%20Facultad%20de%20%2AIngenier%C3%ADa%2A%2C%20las%20preguntas%20de%20matem%C3%A1tica%20tienen%20peso%20doble%20%0ASi%20postulas%20a%20una%20carrera%20de%20las%20Facultades%20de%20%2ACiencias%20Empresariales%2C%20Humanidades%2C%20Enfermer%C3%ADa%20y%20Derecho%2A%2C%20las%20preguntas%20de%20comprensi%C3%B3n%20y%20redacci%C3%B3n%20tienen%20peso%20doble';
        }

        var videoUrlDcp = $("#lblInteres").data('videoUrlDcp');
        var brochureUrlDcp = $("#lblInteres").data('brochureUrlDcp');
        var temarioUrlDcp = $("#lblInteres").data('temarioUrlDcp');

        if (['Q', 'E', 'U'].indexOf(codGrado) > -1) {
            text = 'Hola%20' + interesado + '%20%F0%9F%99%8B%2C%20te%20informo%20que%20el%20%28FECHA%20DE%20PR%C3%93XIMO%20EVENTO%29%20habr%C3%A1%20un%20examen%20para%20ingresar%20a%20la%20USAT%2C%20tiene%20un%20costo%20de%20%2AS%2F.150%2A.%20Si%20est%C3%A1s%20interesado%28a%29%2C%20tienes%20que%20acercarte%20en%20compa%C3%B1%C3%ADa%20de%20tu%20padre%2C%20madre%20o%20apoderado%20a%20la%20Oficina%20de%20Informes%20en%20USAT%2C%20de%20lunes%20a%20viernes%20de%208%20a.m.%20a%204%3A45%20p.m.%20o%20al%20Real%20Plaza%20de%20lunes%20a%20domingo%20de%201%20a%209%20p.m.%20%0A%0A%2ARequisitos%3A%2A%0A%E2%9C%85%20Certificado%20de%20estudios%20original%0A%E2%9C%85%20Los%20postulantes%20que%20se%20encuentren%20cursando%205to.%20a%C3%B1o%20de%20secundaria%2C%20presentar%C3%A1n%20una%20constancia%20de%20estar%20cursando%205to%20de%20secundaria%20firmada%20por%20el%20director%20de%20la%20Instituci%C3%B3n%20Educativa%20o%20copia%20de%20libreta%20de%20notas%20del%20%C3%BAltimo%20bimestre%20cursado%0A%E2%9C%85%20Copia%20de%20DNI%0A%E2%9C%85%20Copia%20de%20%C3%BAltimo%20recibo%20de%20luz%20o%20agua%0A%E2%9C%85%20Ficha%20de%20inscripci%C3%B3n%20firmada%0A%0A' + preguntas + '%0A%0A%F0%9F%92%AA%F0%9F%8F%BC%20Conoce%20los%20%2Abeneficios%2A%20de%20estudiar%20en%20la%20USAT%3A%20http%3A%2F%2Fbit.ly%2F2JoZmJ6%0A%F0%9F%8C%8E%20%2B%20De%2050%20%2AConvenios%20Internacionales%2A%3A%20http%3A%2F%2Fbit.ly%2F2XKY98h%0A%F0%9F%A4%94%20%2A%C2%BFPor%20qu%C3%A9%20elegir%20USAT%3F%2A%3A%20http%3A%2F%2Fbit.ly%2F2LDtV0A%0A%F0%9F%93%88%20Informaci%C3%B3n%20de%20la%20carrera%20de%20%2A' + carrera + '%2A%3A%20' + brochureUrlDcp + '%20%0A%F0%9F%8E%A5%20Conoce%20m%C3%A1s%20sobre%20la%20carrera%20de%20%2A' + carrera + '%2A%3A%20' + videoUrlDcp + '%0A%F0%9F%93%8B%20Temario%20de%20la%20carrera%20de%20%2A' + carrera + '%2A%3A%20' + temarioUrlDcp + '%0A%F0%9F%93%9D%20Simulacro%20de%20examen%20de%20admisi%C3%B3n%20%2Aonline%2A%3A%20http%3A%2F%2Fbit.ly%2F2XsWqQy%0A%F0%9F%93%A9%20Si%20tienes%20alguna%20%2Aconsulta%2A%20cont%C3%A1ctanos%20al%20%28074%29%20606217%20o%20al%20correo%20informes.admision%40usat.edu.pe'
        } else {
            text = 'Hola%20' + interesado + '%20%F0%9F%99%8B%2C%20en%20la%20USAT%20%2Abuscamos%20j%C3%B3venes%20talentosos%20como%20t%C3%BA%20y%20apasionados%20por%20su%20carrera%2A.%20Nos%20%2Acomprometemos%20contigo%2A%20desde%20el%20primer%20d%C3%ADa%20para%20que%20tengas%20un%20%2Afuturo%20profesional%20exitoso%2A.%20%C2%A1Forma%20parte%20de%20la%20familia%20USAT%21%20%0A%0AConoce%20todo%20sobre%20tu%20futura%20carrera%20y%20beneficios%20de%20pertenecer%20a%20la%20USAT%3A%0A%2A%C2%A1USAT%2C%20primera%20universidad%20de%20Lambayeque%20Licenciada%20por%20SUNEDU%21%2A%20%0A%0A%F0%9F%92%AA%F0%9F%8F%BC%20Conoce%20los%20%2Abeneficios%2A%20de%20estudiar%20en%20la%20USAT%3A%20http%3A%2F%2Fbit.ly%2F2JoZmJ6%0A%F0%9F%8C%8E%20%2B%20De%2050%20%2AConvenios%20Internacionales%2A%3A%20http%3A%2F%2Fbit.ly%2F2XKY98h%0A%F0%9F%A4%94%20%2A%C2%BFPor%20qu%C3%A9%20elegir%20USAT%3F%2A%3A%20http%3A%2F%2Fbit.ly%2F2LDtV0A%0A%F0%9F%93%88%20Informaci%C3%B3n%20de%20la%20carrera%20de%20%2A' + carrera + '%2A%3A%20' + brochureUrlDcp + '%20%0A%F0%9F%8E%A5%20Conoce%20m%C3%A1s%20sobre%20la%20carrera%20de%20%2A' + carrera + '%2A%3A%20' + videoUrlDcp + '%0A%F0%9F%93%8B%20Temario%20de%20la%20carrera%20de%20%2A' + carrera + '%2A%3A%20' + temarioUrlDcp + '%0A%F0%9F%93%9D%20Simulacro%20de%20examen%20de%20admisi%C3%B3n%20%2Aonline%2A%3A%20http%3A%2F%2Fbit.ly%2F2XsWqQy%0A%F0%9F%93%A9%20Si%20tienes%20alguna%20%2Aconsulta%2A%20cont%C3%A1ctanos%20al%20%28074%29%20606217%20o%20al%20correo%20informes.admision%40usat.edu.pe'
        }

        window.open('https://api.whatsapp.com/send?phone=51' + celular + '&text=' + text);
    });

    $('#chkNroAnexo').on('change', function (e) {
        var checked = $('#chkNroAnexo').is(':checked');

        $('#txtNroAnexo').prop('readonly', !checked);
        if (checked) {
            $('#txtNroAnexo').focus();
            $('#txtNroAnexo').get(0).setSelectionRange(0, $('#txtNroAnexo').val().length);
        }
    });

    $('body').on('keypress', 'input.only-digits', function (e) {
        return (e.charCode >= 48 && e.charCode <= 57);
    });

    $('body').on('change', '#cboNroInteresado', function (e) {
        var tipoTelefonoFijo = ($('#cboTipoComunicacionR').find('option:selected').text().indexOf('FONO FIJO') !== -1);
        if (tipoTelefonoFijo) {
            $(this).data('lastValue', $(this).val());
        }

        var nroInteresado = $(this).val();

        if (nroInteresado != '') {
            $('#txtNroInteresado').val(nroInteresado);
            $('#txtNroInteresado').prop('readonly', true);
            $('#txtNroInteresado').hide();
        } else {
            $('#txtNroInteresado').val($('#txtNroInteresado').data('lastValue'));
            $('#txtNroInteresado').prop('readonly', false);
            $('#txtNroInteresado').show();
            $('#txtNroInteresado').focus();
        }
    });
});


//********************************************[ I N T E R E S A D O ]********************************************//
function ObteneridInteresado() {
    $("form#frmInteresado input[id=action]").remove();
    $('#frmInteresado').append('<input type="hidden" id="action" name="action" value="' + ope.Idsint + '" />');
    var form = $("#frmInteresado").serializeArray();
    $("form#frmInteresado input[id=action]").remove();
    //console.log(form);
    $.ajax({
        type: "POST",
        url: "../DataJson/crm/InformacionInteresado.aspx",
        data: form,
        dataType: "json",
        cache: false,
        async: false,
        success: function (data) {
            //console.log(data);
            $("#hdcodint").val(data[0].cod)
            $("#hdFiltros").val(data[0].filtros)
            $("#hdCodigoTest").val(data[0].codigoTest)
            $("#hdCodigoCon").val(data[0].codigoCon)
            $("#hdCodigoEveIni").val(data[0].codigoEve)
            $("#hdCodigoEve").val(data[0].codigoEve)
            fnLoading(false);
        },
        error: function (result) {
            fnLoading(false)
            //console.log(result)
        }
    });
}

function fnListarInteresado(sw) {
    rpta = fnvalidaSession()
    if (rpta == true) {
        fnLoading(true)

        // Listo carreras seleccionadas del interesado
        $.ajax({
            type: "POST",
            url: "../DataJson/crm/CarreraProfesional.aspx",
            data: [
                { name: "action", value: ope.lst },
                { name: "hdcodiCP", value: $('#hdcodint').val() },
            ],
            dataType: "json",
            cache: false,
            success: function (response) {
                var otrasCarreras = '';
                response.forEach(function (carrera, i) {
                    if (carrera.pri != 1) {
                        if (otrasCarreras != '') {
                            otrasCarreras += '&nbsp&nbsp|&nbsp&nbsp';
                        }
                        otrasCarreras += carrera.ncpf;
                    }
                });
                if (otrasCarreras != '') {
                    $('#lblOtrasCarreras').before('<br>');
                    $('#lblOtrasCarreras').html(otrasCarreras);
                }
            },
            error: function (a, b, c) {
                console.log(a, b, c)
            }
        });

        $("form#frmInteresado input[id=action]").remove();
        $('#frmInteresado').append('<input type="hidden" id="action" name="action" value="' + ope.edi + '" />');
        var form = $("#frmInteresado").serializeArray();
        $("form#frmInteresado input[id=action]").remove();
        // console.log(form)

        $.ajax({
            type: "POST",
            url: "../DataJson/crm/InformacionInteresado.aspx",
            data: form,
            dataType: "json",
            cache: false,
            success: function (data) {
                var tb = '';
                var tb1 = '';
                var i = 0;
                var tb1 = '<table style="width:100%">'
                var filas = data.length;

                $("#lblInteresado").html(data[i].cNombres + '  ' + data[i].cApePat + '  ' + data[i].cApeMat);
                $("#lblInteres").html(data[i].cCarPro);
                $("#lblInteres").data('es-medicina', data[i].cEsMedicina);

                //Modificado por @jquepuy | 22ENE2019 
                //La información que muestra la tabla fue cambiada, se adicionaron más campos: Celular, Grado, Ubicación de la institución, Edad, Sexo, Dirección del interesado, Ubicación del interesado, y usuario que lo registró
                for (i = 0; i < filas; i++) {
                    // Cargo los datos adicionales de la carrera profesional principal           
                    $.ajax({
                        type: "POST",
                        url: "../DataJson/crm/CarreraProfesional.aspx",
                        data: {
                            action: ope.dcp,
                            codigoCpf: data[i].cCodCarPro,
                        },
                        dataType: "json",
                        cache: false,
                        async: false,
                        success: function (response) {
                            for (var i = 0; i < response.length; i++) {
                                $("#lblInteres").data('codigoCpf', response[0].codigoCpf);
                                $("#lblInteres").data('videoUrlDcp', response[0].videoUrlDcp);
                                $("#lblInteres").data('brochureUrlDcp', response[0].brochureUrlDcp);
                                $("#lblInteres").data('temarioUrlDcp', response[0].temarioUrlDcp);
                            }
                        },
                        error: function (result) {
                            console.log(result)
                        }
                    });

                    tb1 += '<tr><td colspan="6">&nbsp;</td></tr>'
                    tb1 += '<tr>'
                    tb1 += '  <td><ul class="list-inline list-unstyled"><i class="fa fa-mobile primary-info"></i><li style="font-size:12px; font-weight:800; color:#54606f;"><span id="info-celular">' + data[i].cCelular + '</span><p style="border-top: 1px solid #dad7d7; font-size: 12px; font-weight: bold;">Celular</p></li></ul></td>'
                    tb1 += '  <td><ul class="list-inline list-unstyled"><i class="fa fa-phone primary-info"></i><li style="font-size:12px; font-weight:800; color:#54606f;">' + data[i].cTelefono + '<p style="border-top: 1px solid #dad7d7; font-size: 12px; font-weight: bold;">Teléfono</p></li></ul></td>'
                    tb1 += '  <td colspan="2"><ul class="list-inline list-unstyled"><i class="fa fa-at primary-info"></i><li style="font-size:12px; font-weight:300; color:#54606f;"><a href="mailto:' + data[i].cEmail + '">' + data[i].cEmail + '</a><p style="border-top: 1px solid #dad7d7; font-size: 12px; font-weight: bold;">Correo Electrónico</p></li></ul></td>'
                    tb1 += '  <td><ul class="list-inline list-unstyled"><i class="fa fa-birthday-cake primary-info"></i><li style="font-size:12px; font-weight:600; color:#54606f;">' + data[i].cFecNac + '<p style="border-top: 1px solid #dad7d7; font-size: 12px; font-weight: bold;">Fecha de Nacimiento</p></li></ul></td>'
                    tb1 += '  <td><ul class="list-inline list-unstyled"><i class="fa fa-info-circle primary-info"></i><li style="font-size:12px; font-weight:600; color:#54606f;">' + data[i].cEdad + '<p style="border-top: 1px solid #dad7d7; font-size: 12px; font-weight: bold;">Edad</p></li></ul></td>'
                    tb1 += '</tr>'
                    tb1 += '<tr>'
                    tb1 += '  <td><ul class="list-inline list-unstyled"><i class="fa fa-venus-mars primary-info"></i><li style="font-size:12px; font-weight:600; color:#54606f;">' + data[i].cSexo + '<p style="border-top: 1px solid #dad7d7; font-size: 12px; font-weight: bold;">Sexo</p></li></ul></td>'
                    tb1 += '  <td><ul class="list-inline list-unstyled"><i class="fa fa-graduation-cap primary-info"></i><li style="font-size:12px; font-weight:600; color:#54606f;">' + data[i].cGrado + '<p style="border-top: 1px solid #dad7d7; font-size: 12px; font-weight: bold;">Grado</p></li></ul></td>'
                    tb1 += '  <td colspan="2"><ul class="list-inline list-unstyled"><i class="fa fa-bookmark primary-info"></i><li style="font-size:12px; font-weight:600; color:#54606f;">' + data[i].cInsEdu + '<p style="border-top: 1px solid #dad7d7; font-size: 12px; font-weight: bold;">Institución Educativa</p></li></ul></td>'
                    tb1 += '  <td><ul class="list-inline list-unstyled"><i class="fa fa-map-marker primary-info"></i><li style="font-size:12px; font-weight:600; color:#54606f;">' + data[i].cUbiInsEdu + '<p style="border-top: 1px solid #dad7d7; font-size: 12px; font-weight: bold;">Ubicación de la institución</p></li></ul></td>'
                    tb1 += '  <td><ul class="list-inline list-unstyled"><i class="fa fa-graduation-cap primary-info"></i><li style="font-size:12px; font-weight:600; color:#54606f;">' + data[i].cAnioEgreso + '<p style="border-top: 1px solid #dad7d7; font-size: 12px; font-weight: bold;">Año de Egreso</p></li></ul></td>'
                    tb1 += '</tr>'
                    tb1 += '<tr>'
                    tb1 += '  <td colspan="2"><ul class="list-inline list-unstyled"><i class="fa fa-home primary-info"></i><li style="font-size:12px; font-weight:600; color:#54606f;">' + data[i].cDireccion + '<p style="border-top: 1px solid #dad7d7; font-size: 12px; font-weight: bold;">Dirección del interesado</p></li></ul></td>'
                    tb1 += '  <td colspan="2"><ul class="list-inline list-unstyled"><i class="fa fa-map-marker primary-info"></i><li style="font-size:12px; font-weight:600; color:#54606f;">' + data[i].cUbiInt + '<p style="border-top: 1px solid #dad7d7; font-size: 12px; font-weight: bold;">Ubicación del interesado</p></li></ul></td>'
                    tb1 += '  <td colspan="1"><ul class="list-inline list-unstyled"><i class="fa fa-credit-card primary-info"></i><li style="font-size:12px; font-weight:600; color:#54606f;"><a id="lblNumDoc" name="lblNumDoc">' + data[i].cNumDoc + '</a><p style="border-top: 1px solid #dad7d7; font-size: 12px; font-weight: bold;">N° de Documento</p></li></ul></td>'
                    tb1 += '  <td colspan="1"><ul class="list-inline list-unstyled"><i class="fa fa-desktop primary-info"></i><li style="font-size:12px; font-weight:600; color:#54606f;">' + data[i].cUsuario + ' (' + data[i].cFechaReg + ')<p style="border-top: 1px solid #dad7d7; font-size: 12px; font-weight: bold;">Registrado por</p></li></ul></td>'
                    tb1 += '</tr>'

                    $('#hdGrado').val(data[i].cCodigoGrado);
                }
                tb1 += '</table>'

                //$('#datos1').html(tb);
                $('#datos').html(tb1);

                fnLlenarHistorico(true);
                fnLoading(false);
            },
            error: function (result) {
                fnLoading(false)
                //console.log(result)
            }
        });
    } else {
        window.location.href = rpta
    }
}

function fnListaRequisitosIngresante() {
    var data = {
        action: ope.reqing,
        codigoInt: $('#hdcodint').val(),
        codigoCon: $('#hdCodigoCon').val(),
    }

    $.ajax({
        type: "POST",
        url: "../DataJson/crm/InformacionInteresado.aspx",
        data: data,
        dataType: "json",
        cache: false,
        async: false,
        success: function (data) {
            var tBody = '';
            var filas = data.length;

            var ingresante = false;

            for (var i = 0; i < filas; i++) {
                ingresante = (data[i].codigo_Alu != '');

                if (data[i].codigo_aluAsi != '') {
                    var tr = '<tr>';
                    tr += '<td>' + (i + 1) + '</td>'
                    tr += '<td>' + data[i].descripcion + '</td>'
                    tr += '<td>' + data[i].fecha_asi + '</td>'
                    tr += '</tr>';
                    tBody += tr;
                }
            }

            if (ingresante) {
                $('#trRequisitos').show();
            } else {
                $('#trRequisitos').hide();
            }

            fnDestroyDataTableDetalle('tRequisitos');
            $('#tbRequisitos').html(tBody);
            fnResetDataTableBasic('tRequisitos', 0, 'asc');

            fnLoading(false);
        },
        error: function (result) {
            fnLoading(false)
            //console.log(result)
        }
    });
}

//********************************************[ C O M U N I C A C I Ó N ]********************************************//

function fnMotivo(op) {
    var arr = fMotivo(1, "C", op);
    //console.log(arr)
    var n = arr.length;
    var str = "";
    str += '<option value="" selected>-- Seleccione -- </option>';
    for (i = 0; i < n; i++) {
        str += '<option value="' + arr[i].cod + '">' + arr[i].nombre + '</option>';
    }
    //if (op == 'L') {
    //$('#cboMotivoR').html(str);
    //}
    if (op == 'R') {
        $('#cboMotivoR').html(str);
    }
}


function fnTipoComunicacion(op) {
    var arr = fTipoComunicacion(1, "C", op);
    //console.log(arr)
    var n = arr.length;
    var str = "";
    str += '<option value="" selected>-- Seleccione -- </option>';
    for (i = 0; i < n; i++) {
        str += '<option value="' + arr[i].cod + '">' + arr[i].nombre + '</option>';
    }
    //if (op == 'L') {
    //$('#cboMotivoR').html(str);
    //}
    if (op == 'R') {
        $('#cboTipoComunicacionR').html(str);
    }
}

function fnCargarCboEvento() {
    var f = $("#hdCodigoTest").val();
    var cod_con = $("#hdCodigoCon").val();
    var arr = fnEvento(1, "C", f, cod_con);
    //    console.log(arr)
    var n = arr.length;
    var str = "";
    str += '<option value="" selected>-- Seleccione -- </option>';
    for (i = 0; i < n; i++) {
        str += '<option value="' + arr[i].cod + '">' + arr[i].nombre + '</option>';
    }
    $('#cboEvento').html(str);
}

function fnListarEventosPorInteresado() {
    rpta = fnvalidaSession();
    if (rpta) {
        var data = {
            action: ope.lstxint,
            codigoInt: $("#hdcodint").val()
        }

        //console.log(form);
        $.ajax({
            type: "POST",
            url: "../DataJson/crm/Evento.aspx",
            data: data,
            dataType: "json",
            cache: false,
            success: function (data) {
                var tb = ''
                var colConsulta = false;
                for (var i = 0; i < data.length; i++) {
                    var row = data[i];
                    if (row.cConsulta.trim() != '') {
                        colConsulta = true;
                        $('#tEventos thead th:nth-child(3)').before('<th>Consulta</th>');
                        break;
                    }
                }

                for (var i = 0; i < data.length; i++) {
                    var row = data[i];
                    tb += '<tr>';
                    tb += '<td>' + row.cEvento + '</td>';
                    tb += '<td>' + row.cOrigen + '</td>';
                    if (colConsulta) {
                        tb += '<td>' + row.cConsulta + '</td>';
                    }
                    tb += '<td>' + row.cFecha + '</td>';
                    tb += '<td>' + row.cUsuario + '</td>';
                    tb += '</tr>';
                }
                fnDestroyDataTableDetalle('tEventos');
                $('#tbEventos').html(tb);
                fnResetDataTableBasic('tEventos', 2, 'desc');
            },
            error: function (result) {
                //console.log(result)
            }
        });
    } else {
        window.location.href = rpta
    }
}

function fnListarComunicacion(sw) {
    rpta = fnvalidaSession()
    if (rpta == true) {
        if (sw) { fnLoading(true); }
        $("form#frmListaComunicacion input[id=hdcodintC]").remove();
        $('#frmListaComunicacion').append('<input type="hidden" id="action" name="action" value="' + ope.lst + '" />');
        $('#frmListaComunicacion').append('<input type="hidden" id="hdcodintC" name="hdcodintC" value="' + $("#hdcodint").val() + '" />');
        var form = $("#frmListaComunicacion").serializeArray();
        //console.log(form);
        $.ajax({
            type: "POST",
            url: "../DataJson/crm/Comunicacion.aspx",
            data: form,
            dataType: "json",
            cache: false,
            success: function (data) {
                // console.log(data)
                $("form#frmListaComunicacion input[id=action]").remove();
                $("form#frmListaComunicacion input[id=hdcodintC]").remove();
                //console.log(data);
                var tb = '';
                var i = 0;
                var filas = data.length;
                var color_fila = ""
                for (i = 0; i < filas; i++) {
                    if (i == 0) {
                        color_fila = 'select';
                    } else {
                        color_fila = '';
                    }

                    tb += '<tr id="' + data[i].cCod + '" class="dataCom ' + color_fila + '">';
                    tb += '<td>' + (i + 1) + '</td>';
                    tb += '<td>' + data[i].cFecha + '</td>';
                    tb += '<td>' + data[i].cMotivoCom + '</td>';
                    tb += '<td>' + data[i].cTipoCom + '</td>';
                    tb += '<td>' + data[i].cDetalle + '</td>';
                    tb += '<td>' + data[i].cUsuario + '</td>';
                    tb += '<td style="text-align:center"><button type="button" id="btnE" name="btnE" class="btn btn-info" data-toggle="modal" data-target="#mdComunicacion" hdc="' + data[i].cCod + '" title="Editar" ><i class="ion-edit"></i></button>';
                    tb += '<button type="button" id="btnAdd" name="btnAdd" class="btn btn-orange btn-icon-orange" data-toggle="modal" data-target="#mdAcuerdo" hdc="' + data[i].cCod + '" title="Agregar Acuerdos" ><i class="ion-android-add-circle"></i></button>';
                    tb += '<button type="button" id="btnSel" name="btnSel" class="btn btn-primary btn-icon-primary" onclick="fnListarAcuerdo(false,\'' + data[i].cCod + '\')" title="Seleccionar Comunicación" ><i class="ion-android-hand"></i></button>';
                    tb += '<button type="button" id="btnDelCom" name="btnDelCom" class="btn btn-red btn-icon-red" onclick="fnDeleteCom(\'' + data[i].cCod + '\')" title="Eliminar Comunicación" ><i class="ion-android-delete"></i></button>';

                    var mostrarEnvioManual = data[i].mostrarEnvioManual;
                    if (mostrarEnvioManual == '1') {
                        tb += '<button type="button" id="btnReeMEn" name="btnReeMEn" class="btn btn-success btn-icon-primary" onclick="fnReenCom(\'' + data[i].cCod + '\')" title="Reenviar comunicación" ><i class="ion-android-send"></i></button>';
                    }

                    tb += '</td>';
                    tb += '</tr>';
                }
                fnDestroyDataTableDetalle('tComunicacion');
                $('#tbComunicacion').html(tb);
                fnResetDataTableBasic('tComunicacion', 0, 'asc');
                if (sw) { fnLoading(false); }
                //fnLoading(false);
            },
            error: function (result) {
                //console.log(result)
            }
        });
    } else {
        window.location.href = rpta
    }
}


function fnCancelarComunicacion() {
    Limpiar();
    //fnListarComunicacion(true);
}


function fnGuardarComunicacion() {
    rpta = fnvalidaSession()
    if (rpta == true) {
        if (fnValidar() == true) {
            fnLoading(true)
            if ($("#hdcod").val() == 0) {
                $("form#frmRegistro input[id=action]").remove();
                $("form#frmRegistro input[id=hdcodintC]").remove();
                $('#frmRegistro').append('<input type="hidden" id="action" name="action" value="' + ope.reg + '" />');
                $('#frmRegistro').append('<input type="hidden" id="hdcodintC" name="hdcodintC" value="' + $("#hdcodint").val() + '" />');
                var form = $("#frmRegistro").serializeArray();
                $("form#frmRegistro input[id=action]").remove();
                $("form#frmRegistro input[id=hdcodintC]").remove();
                //$('#hdcod').remove();
                //console.log(form);
                $.ajax({
                    type: "POST",
                    url: "../DataJson/crm/Comunicacion.aspx",
                    data: form,
                    dataType: "json",
                    cache: false,
                    success: function (data) {
                        //console.log(data);
                        if (data[0].rpta == 1) {
                            fnMensaje("success", data[0].msje)
                            //$("#cboConvocatoria").val($("#cboConvocatoriaR").val())
                            fnListarComunicacion(false);
                            Limpiar();
                            $("#mdComunicacion").modal("hide");
                            //fnBuscarEvento(false);
                        } else {
                            fnMensaje("warning", data[0].msje)
                        }
                    },
                    error: function (result) {
                        //console.log(result)
                        fnMensaje("warning", result)
                    }
                });
            } else {
                $("form#frmRegistro input[id=action]").remove();
                $("form#frmRegistro input[id=hdcodintC]").remove();
                $('#frmRegistro').append('<input type="hidden" id="action" name="action" value="' + ope.mod + '" />');
                $('#frmRegistro').append('<input type="hidden" id="hdcodintC" name="hdcodintC" value="' + $("#hdcodint").val() + '" />');
                var form = $("#frmRegistro").serializeArray();
                $("form#frmRegistro input[id=action]").remove();
                $("form#frmRegistro input[id=hdcodintC]").remove();
                $('#hdcod').val(0);
                //console.log(form);
                $.ajax({
                    type: "POST",
                    url: "../DataJson/crm/Comunicacion.aspx",
                    data: form,
                    dataType: "json",
                    cache: false,
                    success: function (data) {
                        //console.log(data);
                        if (data[0].rpta == 1) {
                            fnMensaje("success", data[0].msje)
                            fnListarComunicacion(false);
                            Limpiar();
                            $("#mdComunicacion").modal("hide");
                            //fnBuscarEvento(false);
                        } else {
                            fnMensaje("warning", data[0].msje)
                        }
                    },
                    error: function (result) {
                        //console.log(result)
                        fnMensaje("warning", result)
                    }
                });
            }
            fnLoading(false)
        }
    } else {
        window.location.href = rpta
    }
}

function EditComunicacion() {
    rpta = fnvalidaSession()
    if (rpta == true) {
        fnLoading(true)
        $("form#frmRegistro input[id=action]").remove();
        $('#frmRegistro').append('<input type="hidden" id="action" name="action" value="' + ope.edi + '" />');
        var form = $("#frmRegistro").serializeArray();
        $("form#frmRegistro input[id=action]").remove();
        $.ajax({
            type: "POST",
            url: "../DataJson/crm/Comunicacion.aspx",
            data: form,
            dataType: "json",
            cache: false,
            success: function (data) {
                //console.log(data);
                $("#cboMotivoR").val(data[0].cMot);
                $('#cboTipoComunicacionR').val(data[0].cTip);
                $('#cboEstadoComunicacion').val(data[0].cEst);

                var nroInteresado = "";
                var nroAnexo = "";
                var llamadaTelefonoFijo = ($('#cboTipoComunicacionR').find('option:selected').text().indexOf('FONO FIJO') !== -1);

                if (llamadaTelefonoFijo) {
                    var saliente = ($('#cboTipoComunicacionR').find('option:selected').text().indexOf('SALIENTE') !== -1);
                    if (saliente) {
                        nroInteresado = data[0].cDestinatario;
                        nroAnexo = data[0].cRemitente;
                    } else {
                        nroInteresado = data[0].cRemitente;
                        nroAnexo = data[0].cDestinatario;
                    }
                }

                $('#cboNroInteresado').data('lastValue', nroInteresado);

                var itemNroInteresado = $('#cboNroInteresado').find('option[value="' + nroInteresado + '"]');
                if (itemNroInteresado.length > 0) {
                    $('#cboNroInteresado').val(nroInteresado);
                } else {
                    $('#cboNroInteresado').val('');
                }
                $('#cboNroInteresado').trigger('change');

                $('#txtNroInteresado').val(nroInteresado);
                $('#txtNroInteresado').data('lastValue', nroInteresado);

                $('#txtNroAnexo').data('lastValue', nroAnexo);
                $('#txtNroAnexo').val(nroAnexo);
                $('#txtNroAnexo').trigger('change');

                $("#txtdetalle").val(data[0].cDetalle);
                $("#Contador_Com").html(data[0].cDetalle.length + " Caracteres");

                var codigoEve = data[0].cCodigoEve;
                $('#cboEvento').val(codigoEve);
                if (codigoEve != '') {
                    $('#hdCodigoEve').val(codigoEve);
                }

                $('#rowNrosCallcenter').data('llamadaVerificada', data[0].cVerifCall);
                $('#cboEstadoComunicacion').trigger('change');

                $('#cboTipoComunicacionR').trigger('change');

                //if (sw) { fnLoading(false); }
                fnLoading(false);
            },
            error: function (result) {
                fnLoading(false)
                //console.log(result)
            }
        });
    } else {
        window.location.href = rpta
    }
}

function fnEliminarComunicacion(cod) {
    rpta = fnvalidaSession()
    if (rpta == true) {
        //fnLoading(true)
        $.ajax({
            type: "POST",
            url: "../DataJson/crm/Comunicacion.aspx",
            data: { "action": ope.eli, "hdID": cod },
            dataType: "json",
            cache: false,
            async: false,
            success: function (data) {
                //console.log(data);
                if (data[0].rpta == 1) {
                    fnMensaje("success", data[0].msje)
                    fnListarComunicacion(false);
                    //fnBuscarEvento(false);
                } else {
                    fnMensaje("error", data[0].msje)
                }
            },
            error: function (result) {
                //console.log(result)
                fnMensaje("warning", result)
            }
        });
        //fnLoading(false)
    } else {
        window.location.href = rpta
    }
}

var aDataC = [];
function fnDeleteCom(cod) {
    aDataC = {
        cod: cod,
        mensaje: '¿Desea Eliminar la Comunicación?'
    }
    fnMensajeConfirmarEliminar('top', aDataC.mensaje, 'fnEliminarComunicacion', aDataC.cod);
    //fnMensajeConfirmarEliminar('top', aDataR.mensaje + '[' + aDataR.registro + ']?', 'fnRetiro', aDataR.cdm, aDataR.cu);
}

function Limpiar() {
    $("#cboMotivoR").prop('selectedIndex', 0);
    $('#cboTipoComunicacionR').prop('selectedIndex', 0);
    $('#cboTipoComunicacionR').trigger('change');

    $('#cboEstadoComunicacion').prop('selectedIndex', 0);
    $("#txtdetalle").val("");

    $('#rowNrosCallcenter').data('llamadaVerificada', false);
    $('#rowNrosCallcenter').collapse('hide');

    $('#txtNroAnexo').val($('#txtNroAnexo').data('anexoUsuario'));
    $('#txtNroAnexo').data('lastValue', $('#txtNroAnexo').val());

    $('#chkNroAnexo').prop('checked', false);
    $('#chkNroAnexo').trigger('false');

    $('#cboNroInteresado').val($('#cboNroInteresado').data('nroInteresado'));
    $('#cboNroInteresado').data('lastValue', $('#cboNroInteresado').val());
    $('#cboNroInteresado').trigger('change');

    $('#txtNroInteresado').val('');
    $('#txtNroInteresado').data('lastValue', '');
    $('#txtNroInteresado').trigger('change');
}

function fnValidar() {
    if ($("#cboMotivoR").val() == '') {
        fnMensaje("warning", 'Seleccione un Motivo')
        return false
    }
    if ($("#cboTipoComunicacionR").val() == '') {
        fnMensaje("warning", 'Seleccione una Tipo de Comunicación')
        return false
    }
    if ($("#cboEstadoComunicacion").val() == '0') {
        fnMensaje("warning", 'Seleccione un Estado de La Comunicación')
        return false
    }

    var llamadaTelefonoFijo = ($('#cboTipoComunicacionR option:selected').text().indexOf('FONO FIJO') !== -1);
    if (llamadaTelefonoFijo && $("#txtNroInteresado").val() == '') {
        fnMensaje("warning", 'Ingrese el ' + $('#lblNroInteresado').html().replace(':', ''));
        $("#txtNroInteresado").focus();
        return false
    }
    if ($("#txtdetalle").val() == '') {
        fnMensaje("warning", 'Ingrese Detalle')
        return false
    }
    return true
}


//********************************************[  A C U E R D O S  ]********************************************//

function fnListarAcuerdo(sw, cCod) {
    rpta = fnvalidaSession()
    if (rpta == true) {
        if (sw) { fnLoading(true); }

        //Pintar la fila seleccionada
        $("#tComunicacion .dataCom").each(function () {
            $(this).removeClass('unselect');
            $(this).removeClass('select');
        });

        $("#tComunicacion .dataCom").each(function () { //$('#tComunicacion').find('tr #' + cCod).each(function() {
            if ($(this).attr('id') == cCod) {
                $(this).addClass('select');
            }
        });

        $("form#frmListaAcuerdo input[id=action]").remove();
        $("form#frmListaAcuerdo input[id=codigo_com]").remove();
        $('#frmListaAcuerdo').append('<input type="hidden" id="action" name="action" value="' + ope.lst + '" />');
        $('#frmListaAcuerdo').append('<input type="hidden" id="codigo_com" name="codigo_com" value="' + cCod + '" />');
        var form = $("#frmListaAcuerdo").serializeArray();
        //        console.log(form);
        $.ajax({
            type: "POST",
            url: "../DataJson/crm/Acuerdo.aspx",
            data: form,
            dataType: "json",
            cache: false,
            success: function (data) {
                $("form#frmListaAcuerdo input[id=action]").remove();
                //                console.log(data);
                var tb = '';
                var i = 0;
                var filas = data.length;
                for (i = 0; i < filas; i++) {
                    tb += '<tr>';
                    tb += '<td style="text-align:center">' + (i + 1) + "" + '</td>';
                    tb += '<td>' + data[i].cFecha + '</td>';
                    tb += '<td>' + data[i].cHora + '</td>';
                    tb += '<td>' + data[i].cDetalle + '</td>';
                    tb += '<td>' + data[i].cUsuario + '</td>';
                    tb += '<td style="text-align:center"><button type="button" id="btnEditAcuerdo" name="btnEditAcuerdo" class="btn btn-sm btn-info" data-toggle="modal" data-target="#mdAcuerdo" hdc="' + data[i].cCod + '" title="Editar Acuerdo" ><i class="ion-edit"></i></button>';
                    tb += '<button type="button" id="btnDelAcuerdo" name="btnDelAcuerdo" class="btn btn-red btn-icon-red" onclick="fnDelete(\'' + data[i].cCod + '\')" title="Eliminar Acuerdo" ><i class="ion-android-delete"></i></button></td>';
                    tb += '</tr>';
                }
                fnDestroyDataTableDetalle('tAcuerdo');
                $('#tbAcuerdo').html(tb);
                fnResetDataTableBasic('tAcuerdo', 1, 'desc');
                //if (sw) { fnLoading(false); }
                //fnLoading(false);
            },
            error: function (result) {
                //console.log(result)
            }
        });
    } else {
        window.location.href = rpta
    }
}


function fnGuardarAcuerdo() {
    rpta = fnvalidaSession()
    if (rpta == true) {
        if (fnValidarAcuerdo() == true) {
            fnLoading(true)
            if ($("#hdID").val() == "0") {
                //console.log($("#hdID").val());

                $("form#frmAcuerdo input[id=action]").remove();
                $('#frmAcuerdo').append('<input type="hidden" id="action" name="action" value="' + ope.reg + '" />');
                var form = $("#frmAcuerdo").serializeArray();
                $("form#frmAcuerdo input[id=action]").remove();
                //                console.log(form);
                $.ajax({
                    type: "POST",
                    url: "../DataJson/crm/Acuerdo.aspx",
                    data: form,
                    dataType: "json",
                    cache: false,
                    async: false,
                    success: function (data) {
                        //                        console.log(data);
                        if (data[0].rpta == 1) {
                            fnMensaje("success", data[0].msje)
                            fnListarAcuerdo(false, $("#hdcod").val())
                            LimpiarAcuerdo()
                            $("#mdAcuerdo").modal("hide");
                            //fnBuscarEvento(false);
                        } else {
                            fnMensaje("warning", data[0].msje)
                        }
                    },
                    error: function (result) {
                        //                        console.log(result)
                        fnMensaje("warning", result)
                    }
                });
            } else {
                $("form#frmAcuerdo input[id=action]").remove();
                $('#frmAcuerdo').append('<input type="hidden" id="action" name="action" value="' + ope.mod + '" />');
                var form = $("#frmAcuerdo").serializeArray();
                $("form#frmAcuerdo input[id=action]").remove();
                //$('#hdcod').val(0);
                //                console.log(form);
                $.ajax({
                    type: "POST",
                    url: "../DataJson/crm/Acuerdo.aspx",
                    data: form,
                    dataType: "json",
                    cache: false,
                    success: function (data) {
                        //                        console.log(data);
                        if (data[0].rpta == 1) {
                            fnMensaje("success", data[0].msje)
                            fnListarAcuerdo(false, $("#hdcod").val())
                            LimpiarAcuerdo()
                            $("#mdAcuerdo").modal("hide");
                            //fnBuscarEvento(false);
                        } else {
                            fnMensaje("warning", data[0].msje)
                        }
                    },
                    error: function (result) {
                        //console.log(result)
                        fnMensaje("warning", result)
                    }
                });
            }
            fnLoading(false)
        }
    } else {
        window.location.href = rpta
    }
}


function EditAcuerdo() {
    rpta = fnvalidaSession()
    if (rpta == true) {
        fnLoading(true)
        $("form#frmAcuerdo input[id=action]").remove();
        $('#frmAcuerdo').append('<input type="hidden" id="action" name="action" value="' + ope.edi + '" />');
        var form = $("#frmAcuerdo").serializeArray();
        $("form#frmAcuerdo input[id=action]").remove();
        //console.log(form);
        $.ajax({
            type: "POST",
            url: "../DataJson/crm/Acuerdo.aspx",
            data: form,
            dataType: "json",
            cache: false,
            success: function (data) {
                //console.log(data);
                $("#txtdetalle_acu").val(data[0].cDetalle);
                $("#txtFecha_acu").val(data[0].cFecha);

                //[INICIO] Adicionado por @jquepuy | 07ENE2019
                var hra = data[0].cHora;
                var min = "";
                min = hra.substring(3);
                hra = hra.substring(0, 2);

                if (parseInt(hra) == 12) {
                    $("#txtHora_acu").val("12:" + min + " PM");
                } else {
                    if (parseInt(hra) > 12) {
                        $("#txtHora_acu").val((parseInt(hra) - 12) + ":" + min + " PM");
                    } else {
                        $("#txtHora_acu").val(hra + ":" + min + " AM");
                    }
                }
                //[FIN] Adicionado por @jquepuy | 07ENE2019

                $("#Contador_Acu").html(data[0].cDetalle.length + " Caracteres");
                //if (sw) { fnLoading(false); }
                fnLoading(false);
            },
            error: function (result) {
                fnLoading(false)
                //console.log(result)
            }
        });
    } else {
        window.location.href = rpta
    }
}

function LimpiarAcuerdo() {
    $("#txtdetalle_acu").val("");
    $("#txtFecha_acu").val("");
    $("#txtHora_acu").val(""); //Adicionado por @jquepuy | 07ENE2019
}


function fnValidarAcuerdo() {
    if ($("#txtdetalle_acu").val() == '') {
        fnMensaje("warning", 'Ingrese una Descripción')
        return false
    }

    if ($("#txtFecha_acu").val() == '') {
        fnMensaje("warning", 'Ingrese una Fecha')
        return false
    }

    if ($("#txtHora_acu").val() == '') { //Adicionado por @jquepuy | 07ENE2019
        fnMensaje("warning", 'Ingrese una Hora')
        return false
    }
    return true
}


function fnEliminarAcuerdo(cod) {
    rpta = fnvalidaSession()
    if (rpta == true) {
        //fnLoading(true)
        $.ajax({
            type: "POST",
            url: "../DataJson/crm/Acuerdo.aspx",
            data: { "action": ope.eli, "hdID": cod },
            dataType: "json",
            cache: false,
            async: false,
            success: function (data) {
                //console.log(data);
                if (data[0].rpta == 1) {
                    fnMensaje("success", data[0].msje)
                    fnListarAcuerdo(false, $("#codigo_com").val())
                    //fnBuscarEvento(false);
                } else {
                    fnMensaje("danger", data[0].msje)
                }
            },
            error: function (result) {
                //console.log(result)
                fnMensaje("warning", result)
            }
        });
        //fnLoading(false)
    } else {
        window.location.href = rpta
    }
}

var aDataR = [];
function fnDelete(cod) {
    aDataR = {
        cod: cod,
        mensaje: '¿Desea Eliminar el Acuerdo?'
    }
    fnMensajeConfirmarEliminar('top', aDataR.mensaje, 'fnEliminarAcuerdo', aDataR.cod);
    //fnMensajeConfirmarEliminar('top', aDataR.mensaje + '[' + aDataR.registro + ']?', 'fnRetiro', aDataR.cdm, aDataR.cu);
}


//********************************************[  I N T E R É S  ]********************************************//

function fnLlenarTipoEstudioCombo() {
    $("form#frmListaInteres input[id=action]").remove();
    $('#frmListaInteres').append('<input type="hidden" id="action" name="action" value="' + ope.test + '" />');

    var form = $("#frmListaInteres").serializeArray();
    $("form#frmListaInteres input[id=action]").remove();
    $("form#frmListaInteres input[id=cboTipoEstudio]").remove();
    $("form#frmListaInteres input[id=cboConvocatoria]").remove();

    $.ajax({
        type: "POST",
        url: "../DataJson/crm/Interes.aspx",
        data: form,
        dataType: "json",
        cache: false,
        success: function (data) {
            var i = 0;
            var filas = data.length;

            $('#cboTipoEstudio')[0].options.length = 0;
            for (i = 0; i < filas; i++) {
                //if (data[i].cEvento == "TODOS") {
                //    $('#cboTipoEstudio').append('<option value="' + data[i].cCodigo + '" selected>' + data[i].cEvento + '</option>');
                //} else {
                $('#cboTipoEstudio').append('<option value="' + data[i].cCodigo + '">' + data[i].cEvento + '</option>');
                //}
            }

            $("#cboTipoEstudio").trigger("change");
        },
        error: function (result) {
            console.log(result);
        }
    });
}

function fnLlenarConvocatoriaCombo(tipo) {
    $("form#frmListaInteres input[id=action]").remove();
    $('#frmListaInteres').append('<input type="hidden" id="action" name="action" value="' + ope.conv + '" />');
    $('#frmListaInteres').append('<input type="hidden" id="tipo" name="tipo" value="' + tipo.value + '" />');

    var form = $("#frmListaInteres").serializeArray();
    $("form#frmListaInteres input[id=action]").remove();
    $("form#frmListaInteres input[id=tipo]").remove();
    $("form#frmListaInteres input[id=cboTipoEstudio]").remove();
    $("form#frmListaInteres input[id=cboConvocatoria]").remove();

    $.ajax({
        type: "POST",
        url: "../DataJson/crm/Interes.aspx",
        data: form,
        dataType: "json",
        cache: false,
        success: function (data) {
            var i = 0;
            var filas = data.length;

            $('#cboConvocatoria')[0].options.length = 0;
            for (i = 0; i < filas; i++) {
                //if (data[i].cEvento == "TODOS") {
                //    $('#cboConvocatoria').append('<option value="' + data[i].cCodigo + ' selected">' + data[i].cConvocatoria + '</option>');
                //} else {
                $('#cboConvocatoria').append('<option value="' + data[i].cCodigo + '">' + data[i].cConvocatoria + '</option>');
                //}
            }

            $("#cboConvocatoria").trigger("change");
        },
        error: function (result) {
            console.log(result);
        }
    });
}

function seleccionarTodo(obj) {
    var checkboxes = document.getElementsByName('chk');
    for (var i = 0, n = checkboxes.length; i < n; i++) {
        checkboxes[i].checked = obj.checked;
    }
}

function fnListarInteres(sw) {
    rpta = fnvalidaSession();

    var con = $("#cboConvocatoria :selected").text();
    $("#chkMarcar").prop("checked", false);

    if (rpta == true) {
        if (sw) { fnLoading(true); }

        //console.log(ope.lst);
        var cod = $("#hdcodint").val();
        //console.log(cod);

        $.ajax({
            type: "POST",
            url: "../DataJson/crm/Interes.aspx",
            data: { "action": ope.lst, "codigo": cod, "convocatoria": con },
            dataType: "json",
            cache: false,
            success: function (data) {
                var tb = '';
                var i = 0;
                var filas = data.length;

                for (i = 0; i < filas; i++) {
                    tb += '<tr>';

                    if (data[i].cCheck == "1") {
                        tb += '<td class="valor"><input type="checkbox" id="' + data[i].cCod + '" name="chk" checked="checked" /></td>';
                    } else {
                        tb += '<td class="valor"><input type="checkbox" id="' + data[i].cCod + '" name="chk" /></td>';
                    }

                    tb += '<td><label for="' + data[i].cCod + '">' + data[i].cEvento + ' (' + data[i].cConvocatoria + ')</label></td>';
                    tb += '</tr>';
                }

                $('#tbInteres').html(tb);
                if (sw) { fnLoading(false); }
            },
            error: function (result) {
                console.log(result);
            }
        });
    } else {
        window.location.href = rpta;
    }
}


function fnGuardarInteres() {
    rpta = fnvalidaSession();
    if (rpta == true) {
        if (fnValidarInteres() == true) {
            fnLoading(true);

            //console.log(ope.reg);
            var cod = $("#hdcodint").val();
            var codigoOri = $('#cboOrigen').val();
            var checked = '';
            var eventos = '';
            //console.log(cod);

            $("#tbInteres").find("td").each(function () {
                var $opt = $(this).find('input:checkbox');
                var evt = $(this).find('input:checkbox').attr("id");

                if (evt !== undefined) {
                    eventos = eventos + evt + ',';

                    if ($opt.is(":checked")) {
                        checked = checked + evt + ',';
                    }
                }
            });

            //Quitar la última coma de la cadena compuesta
            if (eventos.trim() !== '') {
                eventos = eventos.trim();
                eventos = eventos.substring(0, eventos.length - 1);
            }

            if (checked.trim() !== '') {
                checked = checked.trim();
                checked = checked.substring(0, checked.length - 1);
            }

            $.ajax({
                type: "POST",
                url: "../DataJson/crm/Interes.aspx",
                data: { "action": ope.reg, "codigo": cod, "codigo_ori": codigoOri, "eventos": eventos, "checked": checked },
                dataType: "json",
                cache: false,
                success: function (data) {
                    if (data[0].rpta == 1) {
                        fnMensaje("success", data[0].msje);
                        $("#mdInteres").modal("hide");
                        fnListarEventosPorInteresado();
                    } else {
                        fnMensaje("warning", data[0].msje);
                    }
                },
                error: function (result) {
                    fnMensaje("warning", result);
                }
            });

            fnLoading(false);
        } else {
            fnMensaje("warning", "Seleccione al menos un elemento de la lista de intereses");
        }
    } else {
        window.location.href = rpta;
    }
}

function fnValidarInteres() {
    var chkCount = 0;

    if ($('#cboOrigen').val().trim() == '') {
        fnMensaje("warning", 'Debe seleccionar un origen');
        $('#cboOrigen').focus();
        return false;
    }

    $("#tbInteres").find("td").each(function () {
        var $opt = $(this).find('input:checkbox');
        var evt = $(this).find('input:checkbox').attr("id");

        if (evt !== undefined && $opt.is(":checked")) {
            chkCount++;
        }
    });

    if (chkCount == 0) {
        return false;
    } else {
        return true;
    }
}


function fnLlenarHistorico(sw) {
    rpta = fnvalidaSession()

    if (rpta == true) {
        fnLoading(true);

        var doc = $("#lblNumDoc").html();
        //console.log(doc);

        $.ajax({
            type: "POST",
            url: "../DataJson/crm/InformacionInteresado.aspx",
            data: { "action": ope.lst, "doc": doc },
            dataType: "json",
            cache: false,
            success: function (data) {
                var tb = '';
                var i = 0;
                var filas = data.length;

                for (i = 0; i < filas; i++) {
                    //console.log(data[i].cIngreso);
                    tb += '<tr>';
                    tb += '<td>' + data[i].cIngreso + '</td>';
                    tb += '<td>' + data[i].cEgreso + '</td>';
                    tb += '<td>' + data[i].cPrograma + '</td>';
                    tb += '<td>' + data[i].cModalidad + '</td>';
                    tb += '<td>' + data[i].cEstadoActual + '</td>';
                    tb += '<td>' + data[i].cEstadoDeuda + '</td>';
                    tb += '</tr>';
                }

                fnDestroyDataTableDetalle('tHistorial');
                $('#tbHistorial').html(tb);
                fnResetDataTableBasic('tHistorial', 0, 'asc');
                fnLoading(false);
            },
            error: function (result) {
                console.log(result);
            }
        });
    } else {
        window.location.href = rpta;
    }
}

function fnLlenarEventos(sw) {
    rpta = fnvalidaSession()

    if (rpta == true) {
        fnLoading(true);

        var doc = $("#lblNumDoc").html();
        //console.log(doc);

        $.ajax({
            type: "POST",
            url: "../DataJson/crm/InformacionInteresado.aspx",
            data: { "action": ope.lst, "doc": doc },
            dataType: "json",
            cache: false,
            success: function (data) {
                var tb = '';
                var i = 0;
                var filas = data.length;

                for (i = 0; i < filas; i++) {
                    //console.log(data[i].cIngreso);
                    tb += '<tr>';
                    tb += '<td>' + data[i].cIngreso + '</td>';
                    tb += '<td>' + data[i].cEgreso + '</td>';
                    tb += '<td>' + data[i].cPrograma + '</td>';
                    tb += '<td>' + data[i].cModalidad + '</td>';
                    tb += '<td>' + data[i].cEstadoActual + '</td>';
                    tb += '<td>' + data[i].cEstadoDeuda + '</td>';
                    tb += '</tr>';
                }

                fnDestroyDataTableDetalle('tHistorial');
                $('#tbHistorial').html(tb);
                fnResetDataTableBasic('tHistorial', 0, 'asc');
                fnLoading(false);
            },
            error: function (result) {
                console.log(result);
            }
        });
    } else {
        window.location.href = rpta;
    }
}

//********************************************[  R E G R E S A R  ]********************************************//

//function fnRegresar(cod) {
function fnRegresar() {
    rpta = fnvalidaSession()
    if (rpta == true) {
        fnLoading(true);
        $("form#frmInteresado input[id=action]").remove();
        //$("form#frmbuscar input[id=cod_int]").remove();
        //$("#frmbuscar").append('<input type="hidden" id="cod_int"  name="cod_int" value="' + cod + '" />')
        $("#frmInteresado").append('<input type="hidden" id="action"  name="action" value="' + ope.pint + '" />')
        var form = $("#frmInteresado").serializeArray();
        //$("form#frmbuscar input[id=cod_int]").remove();
        $("form#frmInteresado input[id=action]").remove();
        //console.log(form);
        $.ajax({
            type: "POST",
            url: "../DataJson/crm/InformacionInteresado.aspx",
            data: form,
            dataType: "json",
            async: false,
            cache: false,
            success: function (data) {
                //console.log(data);
                if (data[0].msje == true) {
                    window.location.href = data[0].link
                    //$("#pagina")
                } else {
                    //fnMensaje("warning", data[0].msje)
                }
                // fnLoading(false);
            },
            error: function (result) {
                fnLoading(false);
                //console.log(result)
            }
        });
    } else {
        window.location.href = rpta
    }
}


//function fnBuscarEvento(sw) {
//    if ($("#cboConvocatoria").val() == "") {
//        fnMensaje("warning", "Debe Seleccionar una Convocatoria.")
//    } else {
//        rpta = fnvalidaSession()
//        if (rpta == true) {
//            if (sw) { fnLoading(true); }
//            //    fnLoading(true)
//            $('#frmBuscarEvento').append('<input type="hidden" id="action" name="action" value="' + ope.lst + '" />');

//            var form = $("#frmBuscarEvento").serializeArray();
//            console.log(form);

//            $.ajax({
//                type: "POST",
//                url: "../DataJson/crm/Evento.aspx",
//                data: form,
//                dataType: "json",
//                cache: false,
//                success: function(data) {
//                    $("form#frmBuscarEvento input[id=action]").remove();
//                    console.log(data);
//                    var tb = '';
//                    var i = 0;
//                    var filas = data.length;
//                    for (i = 0; i < filas; i++) {
//                        tb += '<tr>';
//                        tb += '<td style="text-align:center">' + (i + 1) + "" + '</td>';
//                        tb += '<td>' + data[i].cEvento + '</td>';
//                        tb += '<td>' + data[i].cConvocatoria + '</td>';
//                        tb += '<td>' + data[i].cActividad + '</td>';
//                        tb += '<td style="text-align:center"><button type="button" id="btnE" name="btnE" class="btn btn-sm btn-info" data-toggle="modal" data-target="#mdRegistro" hdc="' + data[i].cCod + '" title="Editar" ><i class="ion-edit"></i></button>';
//                        tb += '<button type="button" id="btnD" name="btnD" class="btn btn-red btn-icon-red" onclick="fnDelete(\'' + data[i].cCod + '\')" title="Eliminar" ><i class="ion-android-delete"></i></button></td>';
//                        tb += '</tr>';
//                    }
//                    fnDestroyDataTableDetalle('tEvento');
//                    $('#tbEvento').html(tb);
//                    fnResetDataTableBasic('tEvento', 2, 'asc');
//                    if (sw) { fnLoading(false); }
//                    //            fnLoading(false);
//                },
//                error: function(result) {
//                    //console.log(result)
//                }
//            });
//        } else {
//            window.location.href = rpta
//        }
//    }
//}

function fnCreateDataTableBasic(table, col, ord) {
    var dt = $('#' + table).DataTable({
        "sPaginationType": "full_numbers",
        "bLengthChange": false,
        "bAutoWidth": true,
        "aLengthMenu": [[10, 30, 20, 10, 10, -1], [10, 30, 20, 10, 10, "All"]],
        "iDisplayLength": 10,
        "aaSorting": [[col, ord]]
    });
    return dt;
}

function fnResetDataTableBasic(table, col, ord) {
    if ($.fn.DataTable.fnIsDataTable('#' + table)) {
        $('#' + table).DataTable({
            "bDestroy": true
        });
    }
    else {
        var dt = $('#' + table).DataTable({
            "sContentPadding": false
        });
        dt = $('#' + table).DataTable().fnDestroy();
        dt = $('#' + table).DataTable({
            "sPaginationType": "full_numbers",
            "bLengthChange": false,
            "bAutoWidth": true,
            "aLengthMenu": [[10, 30, 20, 10, 10, 10], [10, 30, 20, 10, 10, "All"]],
            "iDisplayLength": 10,
            "aaSorting": [[col, ord]]
        });

        return dt;
    }
}

function fnDestroyDataTableDetalle(table) {
    var dt = $('#' + table).DataTable().fnDestroy();
    return dt;
}

function fnLoading(sw) {
    if (sw) {
        $('.piluku-preloader').removeClass('hidden');
    } else {
        $('.piluku-preloader').addClass('hidden');
    }
}

function fnLoadingDiv(div, sw) {
    if (sw) {
        $("#" + div).removeClass('hidden');
    } else {
        $("#" + div).addClass('hidden');
    }
}

function fnEstadoComunicacion() {
    var arr = fEstadoComunicacion(1, "C", "0");
    //console.log(arr)
    var n = arr.length;
    var str = "";
    str += '<option value="0" selected >-- Seleccione -- </option>';
    for (i = 0; i < n; i++) {
        str += '<option value="' + arr[i].cod + '">' + arr[i].nombre + '</option>';
    }
    $('#cboEstadoComunicacion').html(str);
}

function fnReenCom(cod) {
    aDataC = {
        cod: cod,
        mensaje: '¿Desea Reenviar la Comunicación?'
    }
    fnMensajeConfirmarEliminar('top', aDataC.mensaje, 'fnReenviarComunicacion', aDataC.cod);
}

function fnReenviarComunicacion(cod) {
    rpta = fnvalidaSession()
    if (rpta == true) {
        //fnLoading(true)
        $.ajax({
            type: "POST",
            url: "../DataJson/crm/Comunicacion.aspx",
            data: { "action": ope.reecom, "hdID": cod },
            dataType: "json",
            cache: false,
            async: false,
            success: function (data) {
                //console.log(data);
                if (data[0].rpta == 1) {
                    fnMensaje("success", data[0].msje)
                    fnListarComunicacion(false);
                    //fnBuscarEvento(false);
                } else {
                    fnMensaje("error", data[0].msje)
                }
            },
            error: function (result) {
                //console.log(result)
                fnMensaje("warning", result)
            }
        });
        //fnLoading(false)
    } else {
        window.location.href = rpta
    }
}

function fnCargarCboOrigen() {
    var origen = [];
    var arr = fnOrigen(1);
    for (i = 0; i < arr.length; i++) {
        origen.push({
            cod: arr[i].cod,
            nombre: arr[i].nombre
        });
    }

    var n = origen.length;
    var str = "";
    str += '<option value="" selected>-- Seleccione -- </option>';
    for (i = 0; i < n; i++) {
        str += '<option value="' + origen[i].cod + '">' + origen[i].nombre + '</option>';
    }
    $('#cboOrigen').html(str);
}

function fnListarTelefonos() {
    rpta = fnvalidaSession()
    if (rpta == true) {
        $.ajax({
            type: "POST",
            url: "../DataJson/crm/Telefono.aspx",
            data: {
                hdcodiT: $("#hdcodint").val(),
                action: ope.lst,
            },
            dataType: "json",
            cache: false,
            success: function (data) {
                //                console.log(data);
                var tb = '', options = '', nroInteresadoVigente = '';
                var i = 0;
                var filas = data.length;
                for (i = 0; i < filas; i++) {
                    tb += '<tr>';
                    tb += '<td style="text-align:center">' + (i + 1) + '</td>';
                    tb += '<td>' + data[i].tip + '</td>';
                    tb += '<td >' + data[i].nro + '</td>';
                    //tb += '<td >' + data[i].det + '</td>';
                    tb += '<td >' + data[i].fec + '</td>';
                    tb += '<td >' + data[i].nprt + '</td>';
                    tb += '<td align="center">';

                    if (data[i].vig == 1) {
                        nroInteresadoVigente = data[i].nro;
                        tb += '<input type="checkbox" checked="checked" id="' + i + '" style="display:block;" disabled="disabled" /></td>';
                        options += '<option value="' + data[i].nro + '" selected="selected">' + data[i].nro + '</option>';
                    } else {
                        tb += '<input type="checkbox" class="editor-active" id="' + i + '" style="display:block;" disabled="disabled" /></td>';
                        options += '<option value="' + data[i].nro + '">' + data[i].nro + '</option>';
                    }
                    tb += '</tr>';
                }
                fnDestroyDataTableDetalle('tTelefonos');
                $('#tbTelefonos').html(tb);
                fnResetDataTableBasic('tTelefonos', 0, 'asc', 5);

                options += '<option value="">-- OTRO --</option>'
                $('#cboNroInteresado').empty();
                $('#cboNroInteresado').html(options);
                $('#cboNroInteresado').data('nroInteresado', nroInteresadoVigente);

                $('#mdTelefono').modal('hide');
            },
            error: function (result) {
                console.log(result);
                //                console.log(result)
            }
        });
    } else {
        window.location.href = rpta
    }
}

function fnListarAnexo() {
    $.ajax({
        type: "POST",
        url: "../DataJson/crm/PersonalAnexo.aspx",
        data: { "action": ope.lst },
        dataType: "json",
        cache: false,
        async: false,
        success: function (data) {
            for (var i = 0; i < data.length; i++) {
                $('#txtNroAnexo').data('anexoUsuario', data[i].cNumero);
            }
            $('#mdAnexo').modal('show');
        },
        error: function (result) {
            //                console.log(result)
            fnMensaje("error", result)
        },
        complete: function () {
            fnLoading(false);
        }
    });
}