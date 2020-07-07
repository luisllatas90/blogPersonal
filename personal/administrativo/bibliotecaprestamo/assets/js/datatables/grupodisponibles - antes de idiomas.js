
 arrCursos = [];
var arrData = [];
var cr, key;
var aData = [];
var aDataDet = [];
var aDataCnf = [];
var aDataR = [];
var cmt;
var cElf = false;
$(document).ready(function() {
    fnDivRefresh('divGrupoDisponible', 1000);
    fnDivRefresh('divHorario', 2000);
    $.ajax({
        type: "GET",
        contentType: "application/json; charset=utf-8",
        url: "procesar.aspx",
        data: { "param0": "lsCNFM" },
        dataType: "json",
        async: false,
        success: function(data) {
            aDataCnf = data;
        },
        error: function(result) {
            location.reload();
        }
    });


    $('#h6CostoCuota2').hide();
    $('#h6CostoCiclo2').hide();

    var sThisVal = ($('#toggle-switcha').is(':checked') ? "1" : "0");

    if (sThisVal == "0") {
        $("#btnacepto").attr("disabled", true);
        $("#btnacepto").attr("class", "btn btn-success");
    } else {
        $("#btnacepto").attr("disabled", false);
        $("#btnacepto").attr("class", "btn btn-success");
    }

    $('#btnacepto').click(function(e) {
        e.preventDefault();
        $('#divReglamento').hide();
        $('#divMatricula').show();
        fnIncidentes();
    });
    $('#btnonacepto').click(function(e) {
        e.preventDefault();
        $('#divReglamento').hide();
        $('#divMatricula').show();
    });

    $('#toggle-switcha').click(function() {
        var sThisVal = (this.checked ? "1" : "0");

        if (sThisVal == "0") {
            $("#btnacepto").attr("disabled", true);
            $("#btnacepto").attr("class", "btn btn-success");
        } else {
            $("#btnacepto").attr("disabled", false);
            $("#btnacepto").attr("class", "btn btn-success");
        }
    });

    fnResetMar();
    $('#cboR').change(fnCboRet);
    $('#cboA').change(fnCboAgr);
    cmt = parseInt(aDataCnf[0].codMat);

    var tabla = "";
    $.ajax({
        type: "GET",
        contentType: "application/json; charset=utf-8",
        url: "procesar.aspx",
        data: { "param0": "lsCM" },
        dataType: "json",
        async: false,
        success: function(data) {
            aData = data;
            var nel = 0;
            var nidi = 0;
            var msjel = "";
            if (data.length > 0) {
                if ((parseInt(aDataCnf[0].codMat) > 0))
                    $("#opcion").html("R.");
                for (var i = 0; i < data.length; i++) {
                    if (data[i].cup_Mat)
                        tabla += '<tr role="row" id="' + data[i].codigo_cur + '" style="background-color: rgb(33, 150, 243);font-size:11px;">';
                    else
                        tabla += '<tr role="row" id="' + data[i].codigo_cur + '" style="font-size:11px;">';

                    tabla += '<td>' + data[i].nombre_Cur;

                    if (parseInt(data[i].vvec) > 0)
                        tabla += '<sub style="color:red;font-weight:bold;" title="Numero de veces desaprobado">' + data[i].vvec + '</sub>';

                    if (data[i].electivo_cur == "True") {
                        if (data[i].vtc != "I") {
                            tabla += '<sub style="color:blue;font-weight:bold;" title="Curso Electivo">E</sub>';
                        }
                    }

                    if (data[i].electivo_cur == "True") {
                        nel = data[i].vnel - data[i].velap;
                        if (nel < 0) {
                            msjel = '0 electivos';
                        } else {
                            if (nel == 0) {
                                msjel = nel + ' electivos';
                            } else if (nel == 1) {
                                msjel = nel + ' electivo';
                            }
                        }
                        if (data[i].vtc != "I") {
                            tabla += '<br><sup style="font-weight:bold;">Para el ' + data[i].vCic + ' ciclo matricularse al menos en ' + msjel + '</sup>';
                        }
                    }
/**/
                    if (data[i].vtc == "I") {
                        nidi = data[i].vnidi - data[i].vidiap;
                        if (nidi < 0) {
                            msjel = '0 idiomas';
                        } else {
                        if (nidi == 0) {
                                msjel = nidi + ' idiomas';
                            } else if (nidi == 1) {
                            msjel = nidi + ' idioma';
                            }
                        }
                       
                            tabla += '<br><sup style="font-weight:bold;">Para el ' + data[i].vCic + ' ciclo matricularse al menos en ' + msjel + '</sup>';
                       
                    }
/**/

                    tabla += '</td>';

                    tabla += '<td>' + data[i].vCred + '</td>';
                    tabla += '<td>' + data[i].vCic + '</td>';

                    if ((parseInt(aDataCnf[0].codMat) > 0) && data[i].chk == true && data.length > 1) {
                        if (aDataCnf[0].swR) {
                            tabla += '<td style="text-align:center;background:#E33439;"> <a href="#" class="btn" style="cursor: pointer;font-size:small;" data-toggle="modal" data-target="#continuemodal" onclick="fnEliminarConfirmar(' + data[i].cod_Dmat + ',' + data[i].codigo_cur + ',\'' + data[i].nombre_Cur + '\',\'¿Deseas retirarte del curso \'' + ')"><i class="ion-android-cancel"></i></a></td>';
                        } else {
                            tabla += '<td></td>';
                        }

                    } else {
                        tabla += '<td></td>';
                    }

                    tabla += '</tr>';

                }


                $('#tbCursos').html(tabla);

            }



        },
        error: function(result) {
            location.reload();
        }
    });



    var oTable = $('#tablagruposdisponibles').DataTable({
        "bPaginate": false,
        "bFilter": false,
        "bLengthChange": false,
        "bInfo": true,
        "aaSorting": [[2, 'asc']],
        "aoColumnDefs": [{
            "bSortable": false,
            "aTargets": [0, 1, 2, 3]
}]
        });



        var nCloneTh = document.createElement('th');
        var nCloneTd = document.createElement('td');
        nCloneTd.className = "details-control";

        $('#tablagruposdisponibles thead tr').each(function() {
            this.insertBefore(nCloneTh, this.childNodes[0]);
        });

        $('#tablagruposdisponibles tbody tr').each(function() {
            this.insertBefore(nCloneTd.cloneNode(true), this.childNodes[0]);
        });

        $('#tablagruposdisponibles tbody td.details-control').on('click', function() {

            var nTr = $(this).parents('tr')[0];
            var cur = nTr.id;
            var cadcup = '';
            var selCurso = fnBuscarData(cur, "selC", "");


            if (oTable.fnIsOpen(nTr)) {
                $(nTr).removeClass('shown');
                $(nTr).css("font-weight", "");
                oTable.fnClose(nTr);
            }
            else {
                $(nTr).addClass('shown');
                $(nTr).css("font-weight", "Bold");

                var sOut = '';
                $.ajax({
                    type: "GET",
                    contentType: "application/json; charset=utf-8",
                    url: "detallematricula.aspx",
                    data: { "cur": cur },
                    dataType: "json",
                    async: false,
                    success: function(data) {
                        console.log(data);
                        sOut = '<table class="display" cellpadding="1" cellspacing="1" style="width:100%;font-size:11px;border: 1px #2196f3 solid;">';
                        var c;
                        var _null;
                        if (data.length > 0) {
                            for (var i = 0; i < data.length - 1; i++) {
                                if (!(data[i].vvec > 0 && data[i].vCic == 1 && data[i].soloPrimerCiclo_cup == 'True')) {
                                    if (i < (data.length - 1)) {

                                        aDataDet[data[i].dia_Lho + data[i].codigo_cup] = {
                                            cup: parseInt(data[i].codigo_cup),
                                            cur: parseInt(data[i].codigo_cur),
                                            dia: data[i].dia_Lho,
                                            fechaI: data[i].nombre_Hor,
                                            fechaF: data[i].horaFin_Lho,
                                            st: data[i].estado_cup,
                                            vac: parseInt(data[i].vacantes_cup) - parseInt(data[i].nromatriculados)
                                        }

                                        if (data[i].codigo_cup != data[i + 1].codigo_cup) {

                                            cadcup += data[i].codigo_cup + ',';
                                            if (parseInt(data[i].vacantes_cup) - parseInt(data[i].nromatriculados) == 0) {
                                                _null = "red";
                                            } else {
                                                _null = "";
                                            }

                                            sOut += '<tr style="color:' + _null + '">';

                                            sOut += '<td align="center">';
                                            if (data[i].estado_cup && parseInt(data[i].vacantes_cup) > parseInt(data[i].nromatriculados)) {
                                                if (fnBuscarData(data[i].codigo_cur, "selC", "") == data[i].codigo_cup) {
                                                    sOut += '<input type="button" id="btnCurso' + data[i].codigo_cup + '" name="btnCurso' + data[i].codigo_cur + '"  onclick="fnSeleccionar(' + data[i].codigo_cup + ',' + data[i].codigo_cur + ')" style="background-color:#2196f3;"/>';
                                                } else {
                                                    sOut += '<input type="button" id="btnCurso' + data[i].codigo_cup + '" name="btnCurso' + data[i].codigo_cur + '"  onclick="fnSeleccionar(' + data[i].codigo_cup + ',' + data[i].codigo_cur + ')"/>';
                                                }
                                            } else {
                                                if (parseInt(data[i].cup_Mat) == parseInt(data[i].codigo_cup)) {
                                                    sOut += '<input type="button" id="btnCurso' + data[i].codigo_cup + '" name="btnCurso' + data[i].codigo_cur + '"  onclick="fnSeleccionar(' + data[i].codigo_cup + ',' + data[i].codigo_cur + ')" style="background-color:#2196f3;" readonly="readonly"/>';
                                                }
                                            }
                                            sOut += '</td>';

                                            sOut += '<td>[' + data[i].grupohor_cup + ']</td>';
                                            sOut += '<td>[' + data[i].docente + ']</td>';

                                            c = 0;
                                            sOut += '<td><table cellpadding="1" cellspacing="1" border="0" style="width:100%;font-size:11px;">';
                                            jQuery.each(data, function(j, hor) {
                                                if (hor.codigo_cup == data[i].codigo_cup) {
                                                    sOut += '<tr>';
                                                    sOut += '<td align="left" width="40%"><input type="hidden" id="txtdia' + hor.codigo_cup + '[' + c + ']" name="txtdia' + hor.codigo_cup + '[' + c + ']" value="' + hor.dia_Lho + '"/><input type="hidden" id="txtfecIni' + hor.codigo_cup + '[' + c + ']" name="txtfecIni' + hor.codigo_cup + '[' + c + ']" value="' + hor.nombre_Hor + '"/><input type="hidden" id="txtfecFin' + hor.codigo_cup + '[' + c + ']" name="txtfecFin' + hor.codigo_cup + '[' + c + ']" value="' + hor.horaFin_Lho + '"/>' + hor.dia_Lho + ' ' + hor.nombre_Hor + ' - ' + hor.horaFin_Lho + '</td>';
                                                    sOut += '<td align="left" width="60%">' + hor.ambiente + '</td>';

                                                    sOut += '</tr>';

                                                    c++;
                                                }
                                            });
                                            sOut += '</table><input type="hidden" id="txthornum' + data[i].codigo_cup + '" value="' + c + '"/></td>';
                                            // if (data[i].estado_cup || parseInt(data[i].vacantes_cup) - parseInt(data[i].nromatriculados) > 0)
                                            if (data[i].estado_cup)
                                                sOut += '<td align="center"><b>' + (parseInt(data[i].vacantes_cup) - parseInt(data[i].nromatriculados)) + '</b><br>Vacantes</td>';
                                            else
                                                sOut += '<td>[Cerrado]<i class="ion-alert" title="Grupo Cerrado"></td>';
                                            sOut += '<input type="hidden" name="txtst' + data[i].codigo_cur + '" value="' + data[i].estado_cup + '"/></tr>';
                                        }
                                    }
                                }
                            }


                            if (!(data[data.length - 1].vvec > 0 && data[data.length - 1].vCic == 1 && data[data.length - 1].soloPrimerCiclo_cup == "True")) {

                                aDataDet[data[data.length - 1].dia_Lho + data[data.length - 1].codigo_cup] = {
                                    cup: parseInt(data[data.length - 1].codigo_cup),
                                    cur: parseInt(data[data.length - 1].codigo_cur),
                                    dia: data[data.length - 1].dia_Lho,
                                    fechaI: data[data.length - 1].nombre_Hor,
                                    fechaF: data[data.length - 1].horaFin_Lho,
                                    st: data[data.length - 1].estado_cup,
                                    vac: parseInt(data[data.length - 1].vacantes_cup) - parseInt(data[data.length - 1].nromatriculados)
                                }
                                console.log(cadcup);

                                if (cadcup.indexOf(data[data.length - 1].codigo_cup) == -1) {
                                    // alert(data[data.length - 1].codigo_cup + " found");


                                    if (parseInt(data[data.length - 1].vacantes_cup) - parseInt(data[data.length - 1].nromatriculados) == 0) {
                                        _null = "red";
                                    }

                                    sOut += '<tr style="color:' + _null + '">';
                                    sOut += '<td align="center">';
                                    if (data[data.length - 1].estado_cup && parseInt(data[data.length - 1].vacantes_cup) > parseInt(data[data.length - 1].nromatriculados)) {
                                        if (fnBuscarData(data[data.length - 1].codigo_cur, "selC", "") == data[data.length - 1].codigo_cup) {
                                            sOut += '<input type="button" id="btnCurso' + data[data.length - 1].codigo_cup + '" name="btnCurso' + data[data.length - 1].codigo_cur + '"  onclick="fnSeleccionar(' + data[data.length - 1].codigo_cup + ',' + data[data.length - 1].codigo_cur + ')" style="background-color:#2196f3;"/>';
                                        }
                                        else {
                                            sOut += '<input type="button" id="btnCurso' + data[data.length - 1].codigo_cup + '" name="btnCurso' + data[data.length - 1].codigo_cur + '"  onclick="fnSeleccionar(' + data[data.length - 1].codigo_cup + ',' + data[data.length - 1].codigo_cur + ')" />';
                                        }
                                    } else {
                                        if (parseInt(data[data.length - 1].cup_Mat) == parseInt(data[data.length - 1].codigo_cup)) {
                                            sOut += '<input type="button" id="btnCurso' + data[data.length - 1].codigo_cup + '" name="btnCurso' + data[data.length - 1].codigo_cur + '"  onclick="fnSeleccionar(' + data[data.length - 1].codigo_cup + ',' + data[data.length - 1].codigo_cur + ')" style="background-color:#2196f3;" readonly="readonly"/>';
                                        }
                                    }



                                    sOut += '</td>';
                                    sOut += '<td>[' + data[data.length - 1].grupohor_cup + ']</td>';
                                    sOut += '<td>[' + data[data.length - 1].docente + ']</td>';


                                    sOut += '<td><table cellpadding="1" cellspacing="1" border="0" style="width:100%;font-size:11px;">';
                                    c = 0;
                                    jQuery.each(data, function(j, hor) {
                                        if (hor.codigo_cup == data[data.length - 1].codigo_cup) {
                                            sOut += '<tr>';
                                            sOut += '<td align="left" width="40%"><input type="hidden" id="txtdia' + hor.codigo_cup + '[' + c + ']" name="txtdia' + hor.codigo_cup + '[' + c + ']"value="' + hor.dia_Lho + '"/><input type="hidden" id="txtfecIni' + hor.codigo_cup + '[' + c + ']" name="txtfecIni' + hor.codigo_cup + '[' + c + ']" value="' + hor.nombre_Hor + '"/><input type="hidden" id="txtfecFin' + hor.codigo_cup + '[' + c + ']" name="txtfecFin' + hor.codigo_cup + '[' + c + ']" value="' + hor.horaFin_Lho + '"/>' + hor.dia_Lho + ' ' + hor.nombre_Hor + ' - ' + hor.horaFin_Lho + '</td>';
                                            sOut += '<td align="left" width="60%">' + hor.ambiente + '</td>';

                                            sOut += '</tr>';

                                            c++;
                                        }
                                    });
                                    sOut += '</table><input type="hidden" id="txthornum' + data[data.length - 1].codigo_cup + '" value="' + c + '"/></td>';
                                    if (data[data.length - 1].estado_cup)
                                        sOut += '<td align="center"><b>' + (parseInt(data[data.length - 1].vacantes_cup) - parseInt(data[data.length - 1].nromatriculados)) + '</b><br>Vacantes</td>';
                                    else
                                        sOut += '<td>[Cerrado]<i class="ion-alert" title="Grupo Cerrado"></i></td>';

                                    sOut += '<input type="hidden" name="txtst' + data[data.length - 1].codigo_cur + '" value="' + data[data.length - 1].estado_cup + '"/></tr>';

                                }


                            }
                        }

                        sOut += '</table>';

                        oTable.fnOpen(nTr, sOut, 'details' + cur);
                        $('.details' + cur).val()

                    },
                    error: function(result) {

                        sOut = '...';
                        oTable.fnOpen(nTr, sOut, 'details');
                        location.reload();
                    }
                });
            }

        });

        if (cmt > 0) {
            $('#divReglamento').hide();
            $('#divMatricula').show("slow");
            $('#tablagruposdisponibles  thead  tr th:eq(0)').html('Horario');
            fnCargaMar('R');
            fnCargaMar('A');
            $('#btnGuardar').attr("data-toggle", "modal");
            $('#btnGuardar').attr("data-target", "#continuemodal");
            $('#btnGuardar').attr('onclick', 'fnAgr()');

        } else {
            $('#divReglamento').show();
            $('#divMatricula').hide("slow");
            //if (_SWUPREQ) $('#tablagruposdisponibles  thead  tr th:eq(0)').html('<a href ="#" class="btn btn-primary" onclick="fnUpReqCur();" style="font-size:10px">Actualizar <i class="ion-ios-refresh-empty"></i></a> ');
            //else
            $('#tablagruposdisponibles  thead  tr th:eq(0)').html('Horario');
        }
        var timeoutId = setTimeout(fnHorario, 1000);
        fnSelecch();
    });

    function fnResetMar() {

        $('#divmA').hide();
        $('#divmR').hide();
    }

    function fnCboRet() {
        var sThisVal = $("#cboR option:selected").text();
        if (sThisVal == "OTROS") {
            $('#divmR').show();
            $('#txtmR').focus();
        } else {
            $('#divmR').hide();
            $('#txtmR').val('');
        }
        $('#divmA').hide();
    }

    function fnCboAgr() {
        var sThisVal = $("#cboA option:selected").text();
        if (sThisVal == "OTROS") {
            $('#divmA').show();
            $('#txtmA').focus();
        } else {
            $('#divmA').hide();
            $('#txtmA').val('');
        }
        $('#divmR').hide();
    }


    function fnCargaMar(o) {

        $.ajax({
            type: "GET",
            contentType: "application/json; charset=utf-8",
            url: "procesar.aspx",
            data: { "param0": "lsMar", "param2": o },
            dataType: "json",
            async: false,
            success: function(data) {

                var i = 0;
                var t = '';
                if (data.length > 0) {
                    t += '<option value="0">SELECCIONE AQUI</option>';
                    for (i = 0; i < data.length; i++) {
                        t += '<option value="' + data[i].c + '">' + data[i].desc + '</option>';
                    }
                }
                if (o == 'R') { $('#cboR').html(t); } else { $('#cboA').html(t); }
            },
            error: function(result) {
            }
        });

    }


    function fnEliminarConfirmar(cdm, cu, registro, mensaje) {
        $('#cboA').hide();
        $('#cboR').show();
        $('#btnA').hide();
        $('#btnR').show();
        aDataR = {
            cdm: cdm,
            cu: cu,
            registro: registro,
            mensaje: mensaje
        }
        fnCboRet();
    }

    function fnRet() {
        fnMensajeConfirmarEliminar('top', aDataR.mensaje + '[' + aDataR.registro + ']?', 'fnRetiro', aDataR.cdm, aDataR.cu);
    }

    function fnAgr() {
        $('#cboA').show();
        $('#cboR').hide();
        $('#btnA').show();
        $('#btnR').hide();
        fnCboAgr();
    }

    function fnHorario() {
        fnCargaHorario();
        fnCalcularPension();
        fnDataHorario();
    }

    function fnSelecch() {
        for (var i = 0; i < aData.length; i++) {
            if (aData[i].electivo_cur == 'True' && aData[i].vnel > 0) {
                if (aData[i].vnel <= aData[i].velap) {
                    fnBuscarData(aData[i].codigo_cur, "chkup", true);

                }
            }

        }

    }

    function fnDataHorario() {
        CalendarEliminarEvento();
        for (var idx in arrCursos) {
            key = arrCursos[idx];
            var fecha = DevuelveFechaCalendario(key.dia);
            cr = cruceHorario(key, fecha);
            CalendarAgregaEvento(key.ccup, key.nom, fecha + ' ' + key.f1, fecha + ' ' + key.f2, false, cr);

        }
    }
    function fnCargaHorario() {

        var rem = parseInt(aDataCnf[0].codMat)
        if (rem == 0) return false;
        var sThisVal;
        var cm;
        var cc;
        var vcs;
        var vtc;
        var param1 = "";

        for (var i = 0; i < aData.length; i++) {
            sThisVal = (aData[i].chk ? "1" : "0");

            if (sThisVal == "1" && aData[i].cup_Mat > 0) {

                $.ajax({
                    type: "GET",
                    contentType: "application/json; charset=utf-8",
                    url: "detallematricula.aspx",
                    data: { "cur": aData[i].codigo_cur, "param1": param1 },
                    dataType: "json",
                    async: false,
                    success: function(data) {
                        if (data.length > 0) {
                            jQuery.each(data, function(j, hor) {
                                if (hor.codigo_cup == aData[i].selCurso) {
                                    eventoCursoCargaHorario(aData[i].codigo_cur, aData[i].nombre_Cur, aData[i].codigo_cur, hor.dia_Lho, hor.nombre_Hor, hor.horaFin_Lho, aData[i].selCurso, aData[i].vvec, "A", aData[i].cup_Mat, rem, aData[i].vtc);
                                }
                            });
                        }
                    },
                    error: function(result) {
                    }
                });

            }


        }



    }

    function fnCalcularPension() {

        var ccur;
        var sumCostoCuota = 0;
        for (var idx in arrData) {
            key = arrData[idx];
            ccur = key.ccur;



            var selCred = parseFloat(fnBuscarData(ccur, "cred", ""));
            var precioCredito = parseFloat(aDataCnf[0].precioCred);
            var precioCalc = parseFloat(fnBuscarData(ccur, "pc", ""));
            var precioCur = 0;


            var vecesDesap = parseInt(fnBuscarData(ccur, "vec", ""));

            var v1 = parseFloat(aDataCnf[0].c1veces) + 1;



            var vn = parseFloat(aDataCnf[0].c2veces) + 1;


            var cuotas = parseInt(aDataCnf[0].cuotas);

            precioCredito = precioCredito * 5;

            var curSel = parseInt($('#h6CursosSeleccionados').html());

            var sumcurSel = 0;
            sumcurSel = curSel + 1;

            $('#h6CursosSeleccionados').html(sumcurSel);

            var credSel = parseInt($('#h6CreditosSeleccionados').html());
            var sumcredSel = 0;
            sumcredSel = credSel + selCred;

            $('#h6CreditosSeleccionados').html(sumcredSel);



            if (selCred > 0) {
                switch (vecesDesap) {
                    case 1:

                        sumCostoCuota = sumCostoCuota + (selCred * precioCredito * v1);

                        break;
                    case 2:

                        sumCostoCuota = sumCostoCuota + (selCred * precioCredito * vn);

                        break;
                    default:

                        sumCostoCuota = sumCostoCuota + (selCred * precioCredito);

                        break;
                }
            } else {
                sumCostoCuota = sumCostoCuota + precioCalc;

            }


            if (aDataCnf[0].maxCred > 0 && sumcredSel > aDataCnf[0].maxCred) {
                $('#h6CostoCuota2').html(aDataCnf[0].montoCredpen.toFixed(2));
                $('#h6CostoCiclo2').html((aDataCnf[0].montoCredpen * cuotas).toFixed(2));
                $('#h6CostoCuota').hide();
                $('#h6CostoCiclo').hide();
                $('#h6CostoCuota2').show();
                $('#h6CostoCiclo2').show();
            } else {
                $('#h6CostoCuota2').html('0.00');
                $('#h6CostoCiclo2').html('0.00');
                $('#h6CostoCuota').show();
                $('#h6CostoCiclo').show();
                $('#h6CostoCuota2').hide();
                $('#h6CostoCiclo2').hide();
            }

            $('#h6CostoCuota').html((sumCostoCuota / cuotas).toFixed(2));
            $('#h6CostoCiclo').html(sumCostoCuota.toFixed(2));

        }
    }

    function eventoCursoCargaHorario(cod, nom, ccur, dia, f1, f2, ccup, vcs, accion, cm, rem, vtc) {

        if (accion == "A") {

            arrCursos[dia + f1 + ccup] = {
                cod: dia + ccup,
                nom: nom,
                ccur: ccur,
                dia: dia,
                f1: f1,
                f2: f2,
                cup: ccup,
                vcs: parseInt(vcs),
                cm: cm,
                rem: rem
            };

            arrData[ccup] = {
                nom: nom,
                ccur: ccur,
                cup: ccup,
                vcs: parseInt(vcs),
                cm: cm,
                rem: rem,
                vtc: vtc
            }


        }
        else {
            delete arrCursos[dia + f1 + ccup];
            delete arrData[ccup];
        }


    }

    function fnValidaCursoMatriculado(c) {
        var sThisVal;
        var cm;
        var cc;
        var sw = false;
        $('input[name=chkCurso]').each(function() {
            sThisVal = (this.checked ? "1" : "0");
            cm = parseInt($(this).attr("vcurm"));
            cc = parseInt($(this).attr("vcur"));
            if (sThisVal == "1" && cm == 1 && cc == c && sw == false) {
                sw = true;
            }
        });

        return sw;
    }

    function fnValidaCursoMatriculadoJSON(c) {
        var sThisVal;
        var cm;
        var cc;
        var sw = false;

        for (var i = 0; i < aData.length; i++) {
            sThisVal = (aData[i].chk ? "1" : "0");
            if (sThisVal == "1" && aData[i].cup_Mat > 0 && aData[i].vcur == c && sw == false) {
                sw = true;
            }

        }

        return sw;
    }

    function fnBuscarDataDet(valor, resultado) {
        var r = false;
        for (var idx in aDataDet) {
            key = aDataDet[idx];

            if (key.cup == valor) {
                switch (resultado) {
                    case 'dia':
                        r = key.dia;
                        break;
                    case 'hi':
                        r = key.fechaI;
                        break;
                    case 'hf':
                        r = key.fechaF;
                        break;
                    default:
                        r = "";
                        break;
                }
            }
        }


        for (var i = 0; i < aDataDet.length; i++) {

            if (aDataDet[i].cup == valor) {
                switch (resulado) {
                    case 'dia':
                        r = aDataDet[i].dia;
                        break;
                    case 'hi':
                        r = aDataDet[i].fechaI;
                        break;
                    case 'hf':
                        r = aDataDet[i].fechaF;
                        break;
                    default:
                        r = "";
                        break;
                }
            }
        }
        return r;
    }

    function fnBuscarData(valor, resulado, up) {
        var r = false;
        for (var i = 0; i < aData.length; i++) {

            if (aData[i].codigo_cur == valor) {
                switch (resulado) {
                    case 'cupm':
                        r = aData[i].cup_Mat;
                        break;
                    case 'cic':
                        r = aData[i].vCic;
                        break;
                    case 'cred':
                        r = aData[i].vCred;
                        break;
                    case 'nel':
                        r = aData[i].vnel;
                        break;
                    case 'elap':
                        r = aData[i].velap;
                        break;
                    case 'elec':
                        r = aData[i].electivo_cur;
                        break;
                    case 'selC':
                        r = aData[i].selCurso;
                        break;
                    case 'vec':
                        r = aData[i].vvec;
                        break;
                    case 'tc':
                        r = aData[i].vtc;
                        break;
                    case 'nomC':
                        r = aData[i].nombre_Cur;
                        break;
                    case 'pc':
                        r = aData[i].vpc;
                        break;

                    case 'chkup':
                        aData[i].chk = up;
                        break;
                    case 'selCup':
                        aData[i].selCurso = up;
                        break;
                    default:
                        r = "";
                        break;
                }
            }
        }
        return r;
    }

    function fnValidaCursoInferior(c, e) {
        var cicCur = parseInt($("#cicCur" + c).val());
        var _cur;
        var sw = 1;
        var cic;

        $('#chkCurso' + c).prop('checked', true);

        $('input[name=chkCurso]').each(function() {
            var sThisVal;
            cic = parseInt($(this).attr("vCic"));
            _cur = parseInt($(this).attr("vcur"));

            if (cic < cicCur && sw == 1) {
                sThisVal = (this.checked ? "1" : "0");
                if (sThisVal == "1") {
                    sw = 1;
                } else {
                    sw = 0;
                    if (!fnValidaSeleccion(c, _cur)) sw = 1;
                }
            }
        });

        if (sw == 0) return false; else return true;
    }
    
    function fnValidaCursoInferiorJSON(c, e) {
        var cicCur = fnBuscarData(c, "cic", "");

        var _cur;
        var sw = 1;
        var cic;
        fnBuscarData(c, "chkup", true);


        if (!fnValidaSeleccionElectivo(c)) {
            fnMensaje('warning', 'Debes cursos electivos');
            cElf = false;
            return false;
        } else {
            cElf = true;
            if (!fnValidaSeleccionComplementario(c)) {

                fnMensaje('warning', 'Debes seleccionar un curso complementario (idiomas - cómputo)');
                return false;
            } else {
                if (!fnValidaSeleccionInferior(c)) {
                    fnMensaje('warning', 'Debes cursos de ciclos inferiores');
                    return false;
                } else {
                    return true;
                }
            }
        }


    }

 function fnRetiro(dm, c) {

        if (fnValidaRetiro(dm, c)) {
            var r = parseInt($('#cboR').val());
            var m = $('#txtmR').val();
            var t = $("#cboR option:selected").text();

            $.ajax({
                type: "GET",
                contentType: "application/json; charset=utf-8",
                url: "procesar.aspx",
                data: { "param0": "retMat", "param1": dm, "r": r, "m": m, "t": t },
                dataType: "json",
                success: function(data) {

                    if (data[0].R == "OK") {
                        fnMensaje("success", data[0].Mensaje);
                        f_Menu('matricula.aspx');
                    }
                    else {
                        fnMensaje("warning", data[0].Mensaje);

                    }
                },
                error: function(result) {
                }
            });
        }
        $('#txtmA').val('');
        $('#txtmR').val('');
    }

    function fnValidaRetiro(dm, c) {


        var credSel = parseInt($('#h6CreditosSeleccionados').html());
        var cred = parseInt(fnBuscarData(c, "cred", ""));
        var tc = credSel - cred;


        if (!fnValidaRetiroComplementario(c)) {
            fnMensaje('warning', 'No puedes retirarte de cursos complementarios');
            return false;
        } else {
            if (parseInt(tc) < 15) {
                fnMensaje('warning', 'No puedes retirarte de este curso. Cr&eacute;ditos mínimos para llevar en ciclo regular: 15');
                return false;
            } else {
                if (!fnValidaRetiroSuperiordp(c)) {

                    fnMensaje('warning', 'No puedes retirarte de cursos desaprobados');
                    return false;
                } else {
                    if (!fnValidaRetiroSuperior(c)) {
                        fnMensaje('warning', 'Primero debes retirarte de cursos de ciclos superiores');
                        return false;
                    } else {

                        return true;
                    }
                }
            }
        }
    }


    function fnValidaRetiroComplementario(cur) {
        var sw = true;


        var vtc = fnBuscarData(cur, "tc", "");


        if (vtc == 'I' || vtc == 'C') {

            sw = false;
            i = aData.length;

        }

        return sw;
    }



    function fnValidaRetiroSuperiordp(cur) {
        var sw = true;


        var veces = parseInt(fnBuscarData(cur, "vec", ""));


        if (veces > 0) {

            sw = false;
            i = aData.length;

        }

        return sw;
    }

    function fnValidaRetiroSuperior(cur) {
        var sw = true;

        var cicSel = parseInt(fnBuscarData(cur, "cic", ""));
        var cup = parseInt(fnBuscarData(cur, "cupm", ""));



        if (cup > 0) {
            for (i = 0; i < aData.length; i++) {

                if (parseInt(aData[i].vCic) > parseInt(cicSel) && parseInt(aData[i].cup_Mat) > 0) { // && sThisVal == "0" && ) {

                    sw = false;
                    i = aData.length;
                }
            }
        }

        return sw;
    }




    function fnValidaCursoSuperior(c, e) {
        var cicCur = parseInt($("#cicCur" + c).val());
        var cic;
        var sel = 0;
        var sThisVal;
        var c = 0;
        var _elect;
        var eachCur;
        var cm = 0;

        $('input[name=chkCurso]').each(function() {
            cic = parseInt($(this).attr("vCic"));
            eachCur = parseInt($(this).attr("vcur"));
            _elect = $('#eleCur' + eachCur).val();
            cm = parseInt($(this).attr("vcurm"));
            if (cic > cicCur) {
                sThisVal = (this.checked ? "1" : "0");

                if (cm == 0) {
                    if (sThisVal == "1" && _elect != "True") {
                        sel = 1;
                    }
                }
                c++;
            }
        });


        if (sel == 0)
            return true;
        else
            return false;
    }

    function fnValidaCursoSuperiorJSON(c, e) {
        var cicCur = parseInt(fnBuscarData(c, "cic", ""));

        var sel = 0;
        var sThisVal;
        var c = 0;
        var _elect;



        for (var i = 0; i < aData.length; i++) {
            if (aData[i].vCic > cicCur) {
                sThisVal = (aData[i].chk ? "1" : "0");

                if (aData[i].cup_Mat == 0) {

                    if (sThisVal == "1" && aData[i].electivo_cur != "True") {
                        sel = 1;
                    }
                }
                c++;
            }
        }


        if (sel == 0)
            return true;
        else
            return false;
    }

    function fnValidaSelecciondpd(cur) {
        var cic;
        var sThisVal;
        var vvec;
        var sw = true;
        var ccur;
        var cicCur = parseInt($("#cicCur" + cur).val());
        $('input[name=chkCurso]').each(function() {
            cic = parseInt($(this).attr("vCic"));
            vvec = parseInt($(this).attr("vvec"));
            ccur = parseInt($(this).attr("vcur"));

            if (cic < cicCur && sw) {
                sThisVal = (this.checked ? "1" : "0");

                if (sThisVal == "0" && vvec > 0) {

                    sw = false;
                }
            }
        });

        return sw;
    }


    function fnValidaSelecciondpdJSON(cur) {
        var sThisVal;
        var sw = true;
        var cicCur = parseInt(fnBuscarData(cur, "cic", ""));

        for (var i = 0; i < aData.length; i++) {

            if (parseInt(aData[i].stt) == 1 && parseInt(aData[i].vCic) < cicCur && sw) {
                sThisVal = (aData[i].chk ? "1" : "0");
                if (sThisVal == "0" && parseInt(aData[i].vvec) > 0) {
                    if (!cElf) {
                        sw = false;
                    }
                }
            }
        }

        
        return sw;
    }



    function fnValidaSeleccionComplementario(cur) {
        var sw = true;
        var sThisVal;
        var cicSel = parseInt(fnBuscarData(cur, "cic", ""));
        var elect = fnBuscarData(cur, "elec", "")
        if (elect == "False") {
            for (i = 0; i < aData.length; i++) {

                sThisVal = (aData[i].chk ? "1" : "0");
                if (parseInt(aData[i].vCic) < parseInt(cicSel) && parseInt(aData[i].stt) == 1) { // && sThisVal == "0" && ) {

                    if (sThisVal == "0" && (aData[i].vtc == "I" || aData[i].vtc == "C")) {

                        if (fnValidadCC(aData[i].vtc)) {

                            sw = false;
                            i = aData.length;
                        }
                    }
                }
            }
        }

        return sw;
    }

    function fnValidaSeleccionInferior(cur) {
        var sw = true;
        var sThisVal;
        var cicSel = parseInt(fnBuscarData(cur, "cic", ""));
        var elect = fnBuscarData(cur, "elec", "")

        
            for (i = 0; i < aData.length; i++) {

                sThisVal = (aData[i].chk ? "1" : "0");
                if (aData[i].electivo_cur == 'False' && parseInt(aData[i].vCic) < parseInt(cicSel) && parseInt(aData[i].stt) == 1 && aData[i].vtc != "I") { // && sThisVal == "0" && ) {

                    if (aData[i].vtc != "C") {
                        if (sThisVal == "0") {
                            sw = false;
                            i = aData.length;

                        }
                    }
                }
            }
        

        return sw;
    }



    function fnValidaSeleccionElectivo(cur) {

        if (aDataCnf[0].rElec) {            
            return true;
        } else {
            var sw = true;
            var sThisVal;
            var cicSel = parseInt(fnBuscarData(cur, "cic", ""));
            var elect = fnBuscarData(cur, "elec", "")


            for (i = 0; i < aData.length; i++) {
                sThisVal = (aData[i].chk ? "1" : "0");

                if (aData[i].electivo_cur == 'True' && parseInt(aData[i].vnel) > parseInt(aData[i].velap) && parseInt(aData[i].vCic) < parseInt(cicSel) && parseInt(aData[i].stt) == 1) { // && sThisVal == "0" && ) {

                    if (sThisVal == "0") {

                        if (fnValidaSeleccionElectivoCicloChk(aData[i].vCic)) {
                            sw = false;
                            i = aData.length;

                        }
                    }

                }
            }

            return sw;
        }

    }

    function fnValidaSeleccionElectivoCicloChk(c) {
        var sThisVal = "";
        var sw = true;

        for (i = 0; i < aData.length; i++) {
            if (parseInt(aData[i].vCic) == parseInt(c) && aData[i].electivo_cur == 'True') {
                sThisVal = (aData[i].chk ? "1" : "0");

                if (sThisVal == "1") {

                    sw = false;
                    i = aData.length;
                }

            }
        }

        return sw;
    }

    function fnValidaSeleccionJSON(cur, _cur) {
        try {
            var _elect;
            var arrDataEl = [];
            var cicCur = fnBuscarData(cur, "cic", "");

            var cic;
            var c = 0;
            var sel = 0;
            var st = 0;
            var cant_cup = 0;

            var _sw = true;
            var sThisVal;
            var eachCur;

            var _swtc = true;
            var vtc;
            var validaIC = 0;
            var _validaIC = 0;
            var validaEl = 0;

            var velap;
            var vnel;
            var i = 0;

            for (i = 0; i < aData.length; i++) {
                if (_sw) {

                    if (aData[i].electivo_cur == 'True' && aData[i].vnel > 0 && sThisVal == "0") {


                        if (aData[i].vnel <= aData[i].velap) {

                            fnBuscarData(aData[i].codigo_cur, "chkup", true);


                            arrDataEl[aData[i].codigo_cur] = {
                                ccur: aData[i].codigo_cur
                            }

                        }
                    }

                    if (sThisVal == "0" && (aData[i].vtc == "I" || aData[i].vtc == "C")) {
                        if (!fnValidadCC(aData[i].vtc)) {
                            _swtc = false;
                            validaIC++;
                        }
                    }

                    if (_swtc) {

                        if (aData[i].vCic < cicCur) { //&& aData[i].codigo_cur == cur) {

                            if (aData[i].vtc == "I" || aData[i].vtc == "C") _validaIC++;


                            for (var idx in aDataDet) {
                                key = aDataDet[idx];

                                if (key.cur == aData[i].codigo_cur && key.st == 1) st++;
                            }

                            if (sThisVal == "0" && st == 0) {
                                _sw = true;

                            } else if (sThisVal == "0" && st > 0) {

                                _sw = false;
                            }


                        }

                    } else {

                        _swtc = true;
                    }

                } //if (_sw)
            } // for 







            for (var idx in arrDataEl) {
                key = arrDataEl[idx];

                delete arrDataEl[key.ccur];
            }
            var _swic = false;

            if (_validaIC > 0 && validaIC == 0) _swic = true;

            if (_sw || (c > 1 && sel == 1)) {
                if (_swic) {
                    fnMensaje('warning', 'Debe seleccionar cursos de ciclos inferiores');
                    return true;
                } else {
                    return false;
                }

            } else {
                return false;
            }

        }
        catch (err) {
            //console.log(err.message);
        }
    }

    function fnValidadCC(tc) {

        var key;
        var i = 0;
        var c = 0;
        var sw = false;

        for (var idx in arrData) {
            key = arrData[idx];
            if (key.vtc == 'I')
                i++;
        }


        for (var idx in arrData) {
            key = arrData[idx];
            if (key.vtc == 'C')
                c++;
        }


        if (tc == 'I') {
            if (i == 1)
                sw = false;
            else
                sw = true;
        } else if (tc == 'C') {
            if (c == 1)
                sw = false;
            else
                sw = true;
        } else {
            sw = true;
        }

        return sw;
    }

    function fnNivel(cur) {

        var c = aDataCnf[0].cond;
        
        var i = 0;
        var niv = 0;
        var ad = 0;
        var _ad = parseInt(aDataCnf[0].ade);
        var _niv = parseInt(aDataCnf[0].niv);
        var _tipo = aDataCnf[0].tipocac;
        var _ivt = parseInt(aDataCnf[0].ivt);
        var _crf = parseInt(aDataCnf[0].cicloRefMat);
        var vcs = fnBuscarData(cur, "vec", "");
        var cicloData = 0;
        
        var ap = 0;
        var nap = 0;
        
        if (vcs > 0) vcs = 1; else ap = 1;

      //  alert(_crf);
        
        for (var idx in arrData) {
//           i++;
//            key = arrData[idx];
//            if (parseInt(key.vcs) > 0) {
//                niv++;
//            }
//            else {
//                nap++;
            //            }

            key = arrData[idx];

            cicloData = parseInt(fnBuscarData(key.ccur, "cic", ""));
           // alert("ciclo cur Data: " + cicloData);
           /* if (_ivt == 1) {
                if (cicloData <= _crf) {
                    niv++;
                } 

            } else {*/
                if (cicloData <= _crf) {
                    niv++;
                } else {
                    nap++;
                }
            //}

        }

        cicloData = parseInt(fnBuscarData(cur, "cic", ""));
        

       /* if (_ivt == 1) {
            if (cicloData <= _crf) {
                niv++;
            }

        } else {*/
            if (cicloData <= _crf) {
                niv++;
            } else {
                nap++;
            }
       // }

        

        var nd = niv;
        var na = nap;
       // alert("ciclo cur SEL: " + cicloData);
       // alert(niv +' '+_niv);
       // alert(na + ' ' + _ad);
        

      
      
            if (c == 'N') {
                if (niv <= _niv && na<=_ad) {
                    return true;
                } else {

                fnMensaje('warning', 'Solo puedes adelantar [' + _ad + '] cursos<br>Solo puedes Nivelar [' + _niv + '] cursos');
                    
                    return false;
                }
 
            }  else if (c == 'O' || c == 'C' || c == 'P') {
            if (niv <= _niv && na <= _ad) {
                    return true;
                } else {
                fnMensaje('warning', 'Solo puedes adelantar [' + _ad + '] cursos<br>Solo puedes Nivelar [' + _niv + '] cursos');
                    return false;
                }
            }
        
    }

    function fnSeleccionar(ccup, ccur) {             
        var rpta;
        var rpta1;
        var rpta2;
        var rpta3;

        var lmtcrd = parseInt(aDataCnf[0].credMaxMat);

        var selCurso = fnBuscarData(ccur, "selC", "");

        var rem = parseInt(aDataCnf[0].codMat);


        var selCred = parseInt(fnBuscarData(ccur, "cred", ""));

        var precioCredito = parseFloat(aDataCnf[0].precioCred);

        var precioCalc = parseFloat(fnBuscarData(ccur, "pc", ""));
        var precioCur = 0;


        var vecesDesap = parseInt(fnBuscarData(ccur, "vec", ""));

        var v1 = parseFloat(aDataCnf[0].c1veces) + 1;
        var vn = parseFloat(aDataCnf[0].c2veces) + 1;

        var cuotas = parseInt(aDataCnf[0].cuotas);
        precioCredito = precioCredito * 5;

        var elecCur = fnBuscarData(ccur, "elec", "");
        var sw = false;
        rpta1 = fnValidaCursoMatriculadoJSON(ccur);
        if (rpta1) {
            fnMensaje('warning', 'Quitar la selecci&oacute;n de cursos de ciclos superiores que no est&eacute; matriculado');
            return false;
        }

        rpta = fnValidaCursoInferiorJSON(ccur, elecCur);


        if (!rpta) {

            $('#chkCurso' + ccur).prop('checked', false);
            fnBuscarData(ccur, "chkup", false);
            return false;
        }


        rpta2 = fnValidaCursoSuperiorJSON(ccur, elecCur);

        if (!rpta2) {
            fnMensaje('warning', 'Quitar la selecci&oacute;n de cursos de ciclos superiores');
            return false;
        }

        rpta3 = fnValidaSelecciondpdJSON(ccur);


        if (!rpta3) {
            fnMensaje('warning', 'No puede seleccionar este curso, primero seleccione cursos desaprobados');
            return false;
        }

        if (selCurso.length == 0) {
            sw = true;
        } else {
            var curSel = parseInt($('#h6CursosSeleccionados').html());
            var sumcurSel = 0;
            sumcurSel = curSel - 1;
            $('#h6CursosSeleccionados').html(sumcurSel);

            var credSel = parseInt($('#h6CreditosSeleccionados').html());
            var sumcredSel = 0;
            sumcredSel = credSel - selCred;
            $('#h6CreditosSeleccionados').html(sumcredSel);
            var costoCuota = parseFloat($("#h6CostoCiclo").html());
            var sumCostoCuota = 0;
            if (selCred > 0) {

                switch (vecesDesap) {
                    case 1:

                        sumCostoCuota = sumCostoCuota + costoCuota - (selCred * precioCredito * v1);
                        break;
                    case 2:

                        sumCostoCuota = sumCostoCuota + costoCuota - (selCred * precioCredito * vn);
                        break;
                    default:

                        sumCostoCuota = sumCostoCuota + costoCuota - (selCred * precioCredito);
                        break;
                }

                if (aDataCnf[0].maxCred > 0 && sumcredSel > aDataCnf[0].maxCred) {
                    $('#h6CostoCuota2').html(aDataCnf[0].montoCredpen.toFixed(2));
                    $('#h6CostoCiclo2').html((aDataCnf[0].montoCredpen * cuotas).toFixed(2));
                    $('#h6CostoCuota').hide();
                    $('#h6CostoCiclo').hide();
                    $('#h6CostoCuota2').show();
                    $('#h6CostoCiclo2').show();
                } else {
                    $('#h6CostoCuota2').html('0.00');
                    $('#h6CostoCiclo2').html('0.00');
                    $('#h6CostoCuota').show();
                    $('#h6CostoCiclo').show();
                    $('#h6CostoCuota2').hide();
                    $('#h6CostoCiclo2').hide();
                }


                $('#h6CostoCuota').html((sumCostoCuota / cuotas).toFixed(2));
                $('#h6CostoCiclo').html(sumCostoCuota.toFixed(2));

            } else {

                sumCostoCuota = sumCostoCuota + costoCuota - precioCalc;

                $('#h6CostoCuota').html(sumCostoCuota.toFixed(2));
                $('#h6CostoCiclo').html((sumCostoCuota / cuotas).toFixed(2));


                if (aDataCnf[0].maxCred > 0 && sumcredSel > aDataCnf[0].maxCred) {
                    $('#h6CostoCuota2').html(aDataCnf[0].montoCredpen.toFixed(2));
                    $('#h6CostoCiclo2').html((aDataCnf[0].montoCredpen * cuotas).toFixed(2));
                    $('#h6CostoCuota').hide();
                    $('#h6CostoCiclo').hide();
                    $('#h6CostoCuota2').show();
                    $('#h6CostoCiclo2').show();
                } else {
                    $('#h6CostoCuota2').html('0.00');
                    $('#h6CostoCiclo2').html('0.00');
                    $('#h6CostoCuota').show();
                    $('#h6CostoCiclo').show();
                    $('#h6CostoCuota2').hide();
                    $('#h6CostoCiclo2').hide();
                }


            }


        }

        if (selCurso == ccup) {
            eliminarCurso(ccur);
            $('#' + ccur).css("background-color", "");
            $("input[name=btnCurso" + ccur + "]").css("background-color", "");

            fnBuscarData(ccur, "selCup", "");
            $('#chkCurso' + ccur).prop('checked', false);
            var _elap = fnBuscarData(ccur, "elap", "");
            var _nel = fnBuscarData(ccur, "nel", "");
            var _ele = fnBuscarData(ccur, "elec", "");

            if (_ele == 'True' && _nel > 0 && _nel <= _elap) {
                fnBuscarData(ccur, "chkup", true);
            } else {
                fnBuscarData(ccur, "chkup", false);
            }




            CalendarEliminarEvento();
            for (var idx in arrCursos) {
                key = arrCursos[idx];
                var fecha = DevuelveFechaCalendario(key.dia);


                cr = cruceHorario(key, fecha);
                
                CalendarAgregaEvento(ccup, key.nom, fecha + ' ' + key.f1, fecha + ' ' + key.f2, false, cr);
            }
        }
        else {
            eliminarCurso(ccur);

            fnBuscarData(ccur, "selCup", ccup);
            $('#' + ccur).css("background-color", "#2196f3");

            $("input[name=btnCurso" + ccur + "]").css("background-color", "");
            $("input[id=btnCurso" + ccup + "]").css("background-color", "#2196f3");


            sw = true;
        }

        if (sw) {


            if (aDataCnf[0].tipocac == 'E') {
                var _c = fnNivel(ccur);

                if (!_c) {
                    $('#' + ccur).css("background-color", "");
                    $("input[name=btnCurso" + ccur + "]").css("background-color", "");

                    fnBuscarData(ccur, "selCup", "");
                    $('#chkCurso' + ccur).prop('checked', false);
                    fnBuscarData(ccur, "chkup", false);
                    return false;
                }
            }

            var tc = fnBuscarData(ccur, "tc", "");

            var curSel = parseInt($('#h6CursosSeleccionados').html());
            var sumcurSel = 0;
            sumcurSel = curSel + 1;
            $('#h6CursosSeleccionados').html(sumcurSel);

            var credSel = parseInt($('#h6CreditosSeleccionados').html());
            var sumcredSel = 0;
            sumcredSel = credSel + selCred;
            if (sumcredSel > lmtcrd) {
                fnMensaje('warning', 'No puede llevar m&aacute;s de ' + lmtcrd + ' cr&eacute;ditos');
                sumcredSel = sumcredSel - selCred;
                $('#h6CreditosSeleccionados').html(sumcredSel);


                var curSel = parseInt($('#h6CursosSeleccionados').html());
                curSel--;
                $('#h6CursosSeleccionados').html(curSel);


                $('#' + ccur).css("background-color", "");
                $("input[name=btnCurso" + ccur + "]").css("background-color", "");

                fnBuscarData(ccur, "selCup", "");
                $('#chkCurso' + ccur).prop('checked', false);
                fnBuscarData(ccur, "chkup", false);




                return false;
            }

            $('#h6CreditosSeleccionados').html(sumcredSel);

            var costoCuota = parseFloat($("#h6CostoCiclo").html());
            var sumCostoCuota = 0;

            if (selCred > 0) {
                switch (vecesDesap) {
                    case 1:
                        sumCostoCuota = sumCostoCuota + costoCuota + (selCred * precioCredito * v1);
                        break;
                    case 2:
                        sumCostoCuota = sumCostoCuota + costoCuota + (selCred * precioCredito * vn);

                        break;
                    default:
                        sumCostoCuota = sumCostoCuota + costoCuota + (selCred * precioCredito);
                        break;
                }

            } else {
                sumCostoCuota = sumCostoCuota + costoCuota + precioCalc;

            }

            if (aDataCnf[0].maxCred > 0 && sumcredSel > aDataCnf[0].maxCred) {
                $('#h6CostoCuota2').html(aDataCnf[0].montoCredpen.toFixed(2));
                $('#h6CostoCiclo2').html((aDataCnf[0].montoCredpen * cuotas).toFixed(2));
                $('#h6CostoCuota').hide();
                $('#h6CostoCiclo').hide();
                $('#h6CostoCuota2').show();
                $('#h6CostoCiclo2').show();
            } else {
                $('#h6CostoCuota2').html('0.00');
                $('#h6CostoCiclo2').html('0.00');
                $('#h6CostoCuota').show();
                $('#h6CostoCiclo').show();
                $('#h6CostoCuota2').hide();
                $('#h6CostoCiclo2').hide();
            }


            $('#h6CostoCuota').html((sumCostoCuota / cuotas).toFixed(2));
            $('#h6CostoCiclo').html(sumCostoCuota.toFixed(2));


            var horCount = parseInt($("#txthornum" + ccup).val());

            var i;

            var nomCurso = fnBuscarData(ccur, "selC", "");

            var vtc = fnBuscarData(ccur, "tc", "");

            for (i = 0; i < horCount; i++) {

                eventoCurso(i, ccup, ccur, horCount, "A", 0, rem, vtc);
            }
            CalendarEliminarEvento();
            for (var idx in arrCursos) {
                key = arrCursos[idx];
                var fecha = DevuelveFechaCalendario(key.dia);
                cr = cruceHorario(key, fecha);

                CalendarAgregaEvento(ccup, key.nom, fecha + ' ' + key.f1, fecha + ' ' + key.f2, false, cr);

            }
        }

    }

    function cruceHorario(key, kfecha) {
        for (var i in arrCursos) {
            obj = arrCursos[i];

            if (key.cod != obj.cod) {
                var fecha = DevuelveFechaCalendario(obj.dia);

                if ((kfecha + ' ' + key.f1 > (fecha + ' ' + obj.f1)) && (kfecha + ' ' + key.f1 < (fecha + ' ' + obj.f2))) {

                    return true;
                }
                if ((kfecha + ' ' + key.f1 == (fecha + ' ' + obj.f1)) && (kfecha + ' ' + key.f2 == (fecha + ' ' + obj.f2))) {

                    return true;
                }
                if ((kfecha + ' ' + key.f1 == (fecha + ' ' + obj.f1))) {

                    return true;
                }
            }
        }

        return false;
    }

    function eliminarCurso(ccur) {

        var ccup = fnBuscarData(ccur, "selC", "");
        var horCount = parseInt($("#txthornum" + ccup).val());
        var i;

        for (i = 0; i < horCount; i++) {
            delete arrCursos[$("#txtdia" + ccup + "\\[" + i + "\\]").val() + $("#txtfecFin" + ccup + "\\[" + i + "\\]").val() + ccup];
            delete arrData[ccup];
        }
    }

    function eventoCurso(i, ccup, ccur, horCount, accion, cm, rem, vtc) {

        var nomCurso = fnBuscarData(ccur, "nomC", "");
        var vcs = fnBuscarData(ccur, "vec", "");
        if (accion == "A") {
            arrCursos[$("#txtdia" + ccup + "\\[" + i + "\\]").val() + $("#txtfecFin" + ccup + "\\[" + i + "\\]").val() + ccup] = {
                cod: $("#txtdia" + ccup + "\\[" + i + "\\]").val() + $("#txtfecFin" + ccup + "\\[" + i + "\\]").val() + ccup,
                nom: nomCurso,
                ccur: ccur,
                dia: $("#txtdia" + ccup + "\\[" + i + "\\]").val(),
                f1: $("#txtfecIni" + ccup + "\\[" + i + "\\]").val(),
                f2: $("#txtfecFin" + ccup + "\\[" + i + "\\]").val(),
                cup: ccup,
                vcs: vcs,
                cm: cm,
                rem: rem
            };

            arrData[ccup] = {
                nom: nomCurso,
                ccur: ccur,
                cup: ccup,
                vcs: parseInt(vcs),
                cm: cm,
                rem: rem,
                vtc: vtc
            }


        }
        else {
            delete arrCursos[$("#txtdia" + ccup + "\\[" + i + "\\]").val() + $("#txtfecFin" + ccup + "\\[" + i + "\\]").val() + ccup];
            delete arrData[ccup];
        }


    }

    function DevuelveFechaCalendario(nD) {
        
        var fecha;
        $.ajax({
            type: "GET",
            contentType: "application/json; charset=utf-8",
            url: "procesar.aspx",
            data: { "param0": "fSr", "param2": nD },
            dataType: "json",
            async: false,
            success: function(data) {

                fecha = data[0].fec;
            },
            error: function(result) {

            }
        });

        return fecha;
    }



    function fnGuardar() {
        var m = 0;
        var o;
        var t;
        if (cmt > 0) {
            m = $('#cboA').val();
            o = $('#txtmA').val();
            t = $("#cboA option:selected").text();
        }

        var obj = {};
        var i = 0;
        for (var idx in arrData) {
            key = arrData[idx];
            obj[i] = key;
            i++;
        }



        $.ajax({
            type: "GET",
            contentType: "application/json; charset=utf-8",
            url: "procesar.aspx",
            data: { "param0": "regMat", "param1": obj, "M": m, "O": o, "T": t },
            dataType: "json",
            success: function(data) {
                if (data[0].R == "C") {
                    fnMensaje("warning", "Se ha encontrado cruce de horarios");
                }
                else if (data[0].R == "OK") {
                    fnMensaje("success", data[0].Mensaje);
                    f_Menu('cursosmatriculados.aspx');
                }
                else {
                    fnMensaje("warning", data[0].Mensaje);
                    if (data[0].UP) {
                        // $('#tablagruposdisponibles  thead  tr th:eq(0)').html('<a href ="#" class="btn btn-primary" onclick="fnUpReqCur();" style="font-size:10px">Actualizar <i class="ion-ios-refresh-empty"></i></a> ');
                    }
                }
            },
            error: function(result) {
            }
        });
    }

