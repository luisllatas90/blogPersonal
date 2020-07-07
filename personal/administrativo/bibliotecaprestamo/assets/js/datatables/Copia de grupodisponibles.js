var arrCursos = [];
var arrData = [];
var cr, key;
var aData = [];
var aDataDet = [];
var aDataCnf = [];
var cmt;

$(document).ready(function() {
    $.ajax({
        type: "GET",
        contentType: "application/json; charset=utf-8",
        url: "procesar.aspx",
        data: { "param0": "lsCNFM" },
        dataType: "json",
        async: false,
        success: function(data) {
            //console.log(data);
            aDataCnf = data;

        },
        error: function(result) {
            location.reload();
        }
    });

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
        $('#divReglamento').hide(500);
        $('#divMatricula').show("fast");
    });
    $('#btnonacepto').click(function(e) {
        e.preventDefault();
        $('#divReglamento').hide(500);
        $('#divMatricula').show("fast");
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

    // cmt = parseInt($("#txtmat").val());

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
            console.log(data);
            aData = data;

            if (data.length > 0) {
                for (var i = 0; i < data.length; i++) {

                    if (data[i].vCurM)
                        tabla += '<tr role="row" id="' + data[i].codigo_cur + '" style="background-color: rgb(33, 150, 243);font-size:11px;">';
                    else
                        tabla += '<tr role="row" id="' + data[i].codigo_cur + '" style="font-size:11px;">';


                    tabla += '<td>' + data[i].nombre_Cur;


                    if (parseInt(data[i].vvec) > 0)
                        tabla += '<sub style="color:red;font-weight:bold;" title="Numero de veces desaprobado">' + data[i].vvec + '</sub>';



                    if (data[i].electivo_cur == "True")
                        tabla += '<sub style="color:blue;font-weight:bold;" title="Curso Electivo">E</sub>';



                    tabla += '</td>';
                    tabla += '<td>' + data[i].vCred + '</td>';
                    tabla += '<td>' + data[i].vCic + '</td>';

                    tabla += '</tr>';

                }
                //console.log(tabla);

                $('#tbCursos').html(tabla);
                /* oSettings = oTable.fnSettings();
                    


                    oSettings.aiDisplay = oSettings.aiDisplayMaster.slice();
                oTable.fnDraw();
                */
                //datatable.api().ajax.reload();
            }



        },
        error: function(result) {
            location.reload();
        }
    });

    //  console.log(aData);

    var oTable = $('#tablagruposdisponibles').DataTable({
        "bPaginate": false,
        "bFilter": false,
        "bLengthChange": false,
        "bInfo": true,

        "aaSorting": [[2, 'asc']],
        "aoColumnDefs": [{
            "bSortable": false,
            "aTargets": [0, 1, 2]
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
            //var selCurso = $('#selCurso' + cur).val();
            var selCurso = fnBuscarData(cur, "selC", "");


            if (oTable.fnIsOpen(nTr)) {
                $(nTr).removeClass('shown');
                $(nTr).css("font-weight", "");
                oTable.fnClose(nTr);
            }
            else {
                $(nTr).addClass('shown');
                $(nTr).css("font-weight", "Bold");
                //// //console.log(cur);
                var sOut = '';
                $.ajax({
                    type: "GET",
                    contentType: "application/json; charset=utf-8",
                    url: "detallematricula.aspx",
                    data: { "cur": cur },
                    dataType: "json",
                    async: false,
                    success: function(data) {
                        //// //console.log(data);
                        sOut = '<table cellpadding="1" cellspacing="1" border="0" style="width:100%;font-size:11px;">';
                        var c;

                        if (data.length > 0) {
                            for (var i = 0; i < data.length - 1; i++) {
                                if (!(data[i].vvec > 0 && data[i].vCic == 1 && data[i].spc == 1)) {
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
                                            sOut += '<tr>';

                                            sOut += '<td align="center">';
                                            if (data[i].estado_cup) {
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
                                            // // //console.log(Boolean(data[i].estado_cup));
                                            c = 0;
                                            sOut += '<td><table cellpadding="1" cellspacing="1" border="0" style="width:100%;font-size:11px;">';
                                            jQuery.each(data, function(j, hor) {
                                                if (hor.codigo_cup == data[i].codigo_cup) {
                                                    sOut += '<tr>';
                                                    sOut += '<td align="left" width="40%"><input type="hidden" id="txtdia' + hor.codigo_cup + '[' + c + ']" name="txtdia' + hor.codigo_cup + '[' + c + ']" value="' + hor.dia_Lho + '"/><input type="hidden" id="txtfecIni' + hor.codigo_cup + '[' + c + ']" name="txtfecIni' + hor.codigo_cup + '[' + c + ']" value="' + hor.nombre_Hor + '"/><input type="hidden" id="txtfecFin' + hor.codigo_cup + '[' + c + ']" name="txtfecFin' + hor.codigo_cup + '[' + c + ']" value="' + hor.horaFin_Lho + '"/>' + hor.dia_Lho + ' ' + hor.nombre_Hor + ' - ' + hor.horaFin_Lho + '</td>';
                                                    sOut += '<td align="left" width="60%">' + hor.ambiente + '</td>';
                                                    // sOut += '<td align="left" width="45%">' + hor.docente + '</td>';
                                                    sOut += '</tr>';
                                                    // // //console.log(hor.ambiente);
                                                    c++;
                                                }
                                            });
                                            sOut += '</table><input type="hidden" id="txthornum' + data[i].codigo_cup + '" value="' + c + '"/></td>';
                                            if (data[i].estado_cup || parseInt(data[i].vacantes_cup) - parseInt(data[i].nromatriculados) > 0)
                                                sOut += '<td align="center"><b>' + (parseInt(data[i].vacantes_cup) - parseInt(data[i].nromatriculados)) + '</b><br>Vacantes</td>';
                                            else
                                                sOut += '<td>[Cerrado]<i class="ion-alert" title="Grupo Cerrado"></td>';
                                            sOut += '<input type="hidden" name="txtst' + data[i].codigo_cur + '" value="' + data[i].estado_cup + '"/></tr>';
                                        }
                                    }
                                }
                            }


                            if (!(data[data.length - 1].vvec > 0 && data[data.length - 1].vCic == 1 && data[data.length - 1].spc == 1)) {

                                aDataDet[data[data.length - 1].dia_Lho + data[data.length - 1].codigo_cup] = {
                                    cup: parseInt(data[data.length - 1].codigo_cup),
                                    cur: parseInt(data[data.length - 1].codigo_cur),
                                    dia: data[data.length - 1].dia_Lho,
                                    fechaI: data[data.length - 1].nombre_Hor,
                                    fechaF: data[data.length - 1].horaFin_Lho,
                                    st: data[data.length - 1].estado_cup,
                                    vac: parseInt(data[data.length - 1].vacantes_cup) - parseInt(data[data.length - 1].nromatriculados)
                                }

                                sOut += '<tr>';
                                sOut += '<td align="center">';
                                if (data[data.length - 1].estado_cup) {
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
                                //// //console.log(Boolean(data[data.length - 1].estado_cup));

                                sOut += '<td><table cellpadding="1" cellspacing="1" border="0" style="width:100%;font-size:11px;">';
                                c = 0;
                                jQuery.each(data, function(j, hor) {
                                    if (hor.codigo_cup == data[data.length - 1].codigo_cup) {
                                        sOut += '<tr>';
                                        sOut += '<td align="left" width="40%"><input type="hidden" id="txtdia' + hor.codigo_cup + '[' + c + ']" name="txtdia' + hor.codigo_cup + '[' + c + ']"value="' + hor.dia_Lho + '"/><input type="hidden" id="txtfecIni' + hor.codigo_cup + '[' + c + ']" name="txtfecIni' + hor.codigo_cup + '[' + c + ']" value="' + hor.nombre_Hor + '"/><input type="hidden" id="txtfecFin' + hor.codigo_cup + '[' + c + ']" name="txtfecFin' + hor.codigo_cup + '[' + c + ']" value="' + hor.horaFin_Lho + '"/>' + hor.dia_Lho + ' ' + hor.nombre_Hor + ' - ' + hor.horaFin_Lho + '</td>';
                                        sOut += '<td align="left" width="60%">' + hor.ambiente + '</td>';
                                        // sOut += '<td align="left" width="45%">' + hor.docente + '</td>';
                                        sOut += '</tr>';
                                        // // //console.log(hor.ambiente);
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

                        sOut += '</table>';

                        oTable.fnOpen(nTr, sOut, 'details' + cur);
                        $('.details' + cur).val()
                        //console.log(aDataDet);
                    },
                    error: function(result) {
                        //// //console.log(result);
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
        } else {
            $('#divReglamento').show();
            $('#divMatricula').hide("slow");
            if (_SWUPREQ) $('#tablagruposdisponibles  thead  tr th:eq(0)').html('<a href ="#" class="btn btn-primary" onclick="fnUpReqCur();" style="font-size:10px">Actualizar <i class="ion-ios-refresh-empty"></i></a> ');
            else
                $('#tablagruposdisponibles  thead  tr th:eq(0)').html('Horario');
        }
        var timeoutId = setTimeout(fnHorario, 1000);


        fnSelecch();



    });
    function fnHorario() {
        fnCargaHorario();
        fnCalcularPension();
        fnDataHorario();

    }
    //    var arrCursos = [];
    //    var arrData = [];
    //    var cr, key;

    function fnSelecch() {
        //**********************VALIDA ELECTIVOS*********************//


        /***********************UP_ARR******************************/
        for (var i = 0; i < aData.length; i++) {
            if (aData[i].electivo_cur == 'True' && aData[i].vnel > 0) {
                if (aData[i].vnel <= aData[i].velap) {
                    fnBuscarData(aData[i].codigo_cur, "chkup", true);

                }
            }

        }
        /***********************UP_ARR*******************************/
        //**********************VALIDA ELECTIVOS*********************//
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

        //var rem = parseInt($("#txtmat").val());
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

            if (sThisVal == "1" && aData[i].vCurM == 1) {

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
                                    eventoCursoCargaHorario(aData[i].codigo_cur, aData[i].nombre_Cur, aData[i].codigo_cur, hor.dia_Lho, hor.nombre_Hor, hor.horaFin_Lho, aData[i].selCurso, aData[i].vvec, "A", aData[i].vCurM, rem, aData[i].vtc);
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
        //console.log(aDataCnf);
        var ccur;
        var sumCostoCuota = 0;
        for (var idx in arrData) {
            key = arrData[idx];
            ccur = key.ccur;


            //var selCred = parseInt($('#credCurso' + ccur).val());
            var selCred = parseFloat(fnBuscarData(ccur, "cred", ""));
            // var precioCredito = parseFloat($("#txtprecioCredito").val());
            var precioCredito = parseFloat(aDataCnf[0].precioCred);
            //var precioCalc = parseFloat($("#precioCalc" + ccur).val());
            var precioCalc = parseFloat(fnBuscarData(ccur, "pc", ""));
            var precioCur = 0;

            //var vecesDesap = parseInt($('#vecesDesap' + ccur).val());
            var vecesDesap = parseInt(fnBuscarData(ccur, "vec", ""));
            //var v1 = parseFloat($("#txtveces1").val()) + 1;
            var v1 = parseFloat(aDataCnf[0].c1veces) + 1;

            //var vn = parseFloat($("#txtvecesn").val()) + 1;

            var vn = parseFloat(aDataCnf[0].c2veces) + 1;

            //var cuotas = parseInt($("#txtcuotas").val());
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

            //var costoCuota = parseFloat($("#h6CostoCuota").html());

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


            ////console.log("sumCostoCuota: " + sumCostoCuota);
            $('#h6CostoCuota').html((sumCostoCuota / cuotas).toFixed(2));
            $('#h6CostoCiclo').html(sumCostoCuota.toFixed(2));

        }
    }

    function eventoCursoCargaHorario(cod, nom, ccur, dia, f1, f2, ccup, vcs, accion, cm, rem, vtc) {
        // // //console.log("i :" + i + "horCount: " + horCount);
        if (accion == "A") {

            arrCursos[dia + ccup] = {
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

            // // //console.log(arrCursos[$("#txtdia" + ccup + "\\[" + i + "\\]").val() + ccup]['dia']);
        }
        else {
            delete arrCursos[dia + ccup];
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
            if (sThisVal == "1" && aData[i].vCurM == 1 && aData[i].vcur == c && sw == false) {
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
                    //////////////////////////////UPDATE///////////////////////////////                                            
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
        //// //console.log("sw " + sw);   
        if (sw == 0) return false; else return true;
    }

    function fnValidaCursoInferiorJSON(c, e) {
        var cicCur = fnBuscarData(c, "cic", "");            // parseInt($("#cicCur" + c).val());

        var _cur;
        var sw = 1;
        var cic;
        fnBuscarData(c, "chkup", true);
        for (var i = 0; i < aData.length; i++) {
            if (aData[i].vCic < cicCur && sw == 1) {
                sThisVal = (aData[i].chk ? "1" : "0");
                if (sThisVal == "1") {
                    sw = 1;
                } else {
                    sw = 0;
                    if (!fnValidaSeleccionJSON(c, _cur)) sw = 1;
                }
            }

        }
        if (sw == 0) return false; else return true;


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
                //console.log(cic + "==" + cicCur + "  " + sThisVal + " cm: " + cm);
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


        //  console.log("ciclo curso: " + cicCur);

        // fnBuscarData(c, "chkup", true);
        for (var i = 0; i < aData.length; i++) {
            if (aData[i].vCic > cicCur) {
                sThisVal = (aData[i].chk ? "1" : "0");
                //console.log("JSON " + aData[i].vCic + "==" + cicCur + "  " + sThisVal + " cm: " + aData[i].vCurM);
                if (aData[i].vCurM == 0) {
                    // console.log("JSON " + aData[i].nomCurso + "   " + sThisVal + " electivo_cur: " + aData[i].electivo_cur);
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
                // //console.log("ciclo: " + cic + ": " + sThisVal + '  vvec:' + vvec + ' ccur:' + ccur);
                if (sThisVal == "0" && vvec > 0) {
                    ////console.log("seleccionar ccur: " + ccur + " curoso: " + $("#nomCurso" + cur).val());
                    sw = false;
                }
            }
        });
        ////console.log("sw: "+sw);
        return sw;
    }


    function fnValidaSelecciondpdJSON(cur) {
        var sThisVal;
        var sw = true;
        var cicCur = parseInt(fnBuscarData(cur, "cic", ""));

        for (var i = 0; i < aData.length; i++) {
            if (aData[i].vCic > cicCur && sw) {
                sThisVal = (aData[i].chk ? "1" : "0");
                if (sThisVal == "0" && aData[i].vvec > 0) {
                    sw = false;
                }
            }
        }
        return sw;
    }
    /*
    function fnValidaSeleccion(cur, _cur) {
    //// //console.log("cur: "+cur + " _cur: "+ _cur);
    var cicCur = parseInt($("#cicCur" + cur).val());

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
    var validaEl = 0;

        var velap;
    var vnel;
    $('input[name=chkCurso]').each(function() {
    cic = parseInt($(this).attr("vCic"));



    if (cic == cicCur) {
    sThisVal = (this.checked ? "1" : "0");
    if (sThisVal == "1") {
    sel = 1;
    }
    c++;

            }
    });

        var _elect;

        var arrDataEl = [];

        $('input[name=chkCurso]').each(function() {
    if (_sw) {
    cic = parseInt($(this).attr("vCic"));
    eachCur = $(this).attr("vcur");
    vtc = $(this).attr("vtc");
    velap = parseInt($(this).attr("velap"));
    vnel = parseInt($(this).attr("vnel"));
    _elect = $('#eleCur' + eachCur).val();
    sThisVal = (this.checked ? "1" : "0");

                if (_elect == 'True' && vnel > 0 && sThisVal == "0") {
    if (vnel <= velap) {
    $('#chkCurso' + eachCur).prop('checked', true);

                        arrDataEl[eachCur] = {
    ccur: eachCur
    }
    }
    }
    if (sThisVal == "0" && (vtc == "I" || vtc == "C")) {
    if (!fnValidadCC(vtc)) {
    _swtc = false;
    validaIC++;
    }
    }

                if (_swtc) {
    // console.log(cic +"<= "+cicCur+" && "+eachCur+" == "+cur);
    if (cic <= cicCur && eachCur == cur) {
    $('input[name=txtst' + eachCur + ']').each(function() {
    if (parseInt($(this).val()) == 1) st++;
    });
    //console.log("st: " + st);
    if (sThisVal == "0" && st == 0) {
    _sw = true;
    } else if (sThisVal == "0" && st > 0) {

                            _sw = false;
    }
    }

                } else {

                    _swtc = true;
    }
    }
    });

        for (var idx in arrDataEl) {
    key = arrDataEl[idx];
    //$('#chkCurso' + key.ccur).prop('checked', false);
    delete arrDataEl[key.ccur];
    }


        if (_sw || (c > 1 && sel == 1)) {
    if (validaIC == 0) {
    fnMensaje('warning', 'Debe seleccionar cursos de ciclos inferiores');
    return true;
    } else {
    return false;
    }

        } else {
    return false;
    }
    }
    */
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
            var validaEl = 0;

            var velap;
            var vnel;
            var i = 0;
            // fnBuscarData(c, "chkup", true);
            for (i = 0; i < aData.length; i++) {
                if (aData[i].vCic == cicCur) {
                    sThisVal = (aData[i].chk ? "1" : "0");
                    if (sThisVal == "1") {
                        sel = 1;
                    }
                    c++;
                }
            }
            for (i = 0; i < aData.length; i++) {

                if (_sw) {

                    if (aData[i].electivo_cur == 'True' && aData[i].vnel > 0 && sThisVal == "0") {
                        if (aData[i].vnel <= aData[i].velap) {
                            //  $('#chkCurso' + eachCur).prop('checked', true);
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
                        //  console.log("JSON "+aData[i].vCic +"<="+ cicCur +"&&"+ aData[i].codigo_cur+"=="+ cur)
                        if (aData[i].vCic <= cicCur && aData[i].codigo_cur == cur) {


                            /*
                            $('input[name=txtst' + eachCur + ']').each(function() {
                            if (parseInt($(this).val()) == 1) st++;
                            });
                            */


                            for (var idx in aDataDet) {
                                key = aDataDet[idx];
                                // console.log(key.cur +"=="+ aData[i].codigo_cur +"&&"+ key.st +"=="+ 1);                     
                                if (key.cur == aData[i].codigo_cur && key.st == 1) st++;
                            }
                            //console.log("stJSON: " + st);

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

            if (_sw || (c > 1 && sel == 1)) {
                if (validaIC == 0) {
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
            console.log(arrDataEl);
            console.log(err.message);
        }
    }

    function fnValidadCC(tc) {
        // // //console.log(arrData);
        var key;
        var i = 0;
        var c = 0;


        for (var idx in arrData) {
            key = arrData[idx];
            if (key.vtc == 'I')
                i++;
        }
        // if (i == 1) fnMensaje('warning', 'No puede seleccionar mas de 1 curso de idiomas');

        for (var idx in arrData) {
            key = arrData[idx];
            if (key.vtc == 'C')
                c++;
        }
        // if (i == 1) fnMensaje('warning', 'No puede seleccionar mas de 1 curso de computo');

        // //console.log(tc+ ": i-> " + i);
        // //console.log(tc+ ": c-> " + c);

        if (tc == 'I') {
            if (i == 1)
                return false;
            else
                return true;
        } else if (tc == 'C') {
            if (c == 1)
                return false;
            else
                return true;
        } else {
            return true;
        }


    }

    function fnNivel(cur) {
        //var c = $("#txtcond").val();
        var c = aDataCnf[0].cond;
        var i = 0;
        var niv = 0;
        var ad = 0;
        var _ad = parseInt(aDataCnf[0].ade);
        var _niv = parseInt(aDataCnf[0].niv);
        var _tipo = aDataCnf[0].tipocac;
        // for (var idx in arrData) i++;
        //'#chkCurso'+cur

        //var vcs = parseInt($('#chkCurso' + cur).attr("vvec"));
        var vcs = fnBuscarData(cur, "vec", "");
        if (vcs > 0) vcs = 1;

        for (var idx in arrData) {
            i++;
            key = arrData[idx];
            if (parseInt(key.vcs) > 0) {
                niv++;
            }
        }
        niv += vcs;
        i++;
        ad = i - niv;


        if (c == 'N' || c == 'O') {
            if (ad <= _ad && niv >= _niv) {
                return true;
            } else {
                fnMensaje('warning', 'No puede adelantar mas de 2 cursos');
                return false;
            }
        } else if (c == 'P') {
            if (ad <= _ad || niv <= _niv) {
                return true;
            } else {
                fnMensaje('warning', 'No puede adelantar mas de 1 curso');
                return false;
            }
        }
        else if (c == 'C') {
            if (ad == _ad && niv <= _niv) {
                return true;
            } else {
                fnMensaje('warning', 'No puede nivelar mas de 1 curso');
                return false;
            }
        }
    }

    function fnSeleccionar(ccup, ccur) {
        var rpta;
        var rpta1;
        var rpta2;
        var rpta3;
        // var lmtcrd = parseInt($('#txtcredMaxMat').val());
        var lmtcrd = parseInt(aDataCnf[0].credMaxMat);
        //alert(aDataCnf[0].credMaxMat);
        //var selCurso = $('#selCurso' + ccur).val();
        var selCurso = fnBuscarData(ccur, "selC", "");
        //var rem = parseInt($("#txtmat").val());
        var rem = parseInt(aDataCnf[0].codMat);

        //var selCred = parseInt($('#credCurso' + ccur).val());
        var selCred = parseInt(fnBuscarData(ccur, "cred", ""));
        //var precioCredito = parseFloat($("#txtprecioCredito").val());
        var precioCredito = parseFloat(aDataCnf[0].precioCred);
        //var precioCalc = parseFloat($("#precioCalc" + ccur).val());
        var precioCalc = parseFloat(fnBuscarData(ccur, "pc", ""));
        var precioCur = 0;

        //var precioCredito=precioCredito;
        //var vecesDesap = parseInt($('#vecesDesap' + ccur).val());
        var vecesDesap = parseInt(fnBuscarData(ccur, "vec", ""));
        //var v1 = parseFloat($("#txtveces1").val()) + 1;
        //var vn = parseFloat($("#txtvecesn").val()) + 1;
        var v1 = parseFloat(aDataCnf[0].c1veces) + 1;
        var vn = parseFloat(aDataCnf[0].c2veces) + 1;
        //var cuotas = parseInt($("#txtcuotas").val());
        var cuotas = parseInt(aDataCnf[0].cuotas);
        precioCredito = precioCredito * 5;

        var elecCur = fnBuscarData(ccur, "elec", "");
        var sw = false;
        rpta1 = fnValidaCursoMatriculadoJSON(ccur);
        if (rpta1) {
            fnMensaje('warning', 'No puede deseleccionar, ya se encuentra matriculado en este curso');
            return false;
        }
        // console.log("rpta1JSON: "+fnValidaCursoMatriculadoJSON(ccur));
        // console.log("rpta1: " + rpta1);
        // valida cursos inferiores
        rpta = fnValidaCursoInferiorJSON(ccur, elecCur);
        // console.log("rptaJSON: "+fnValidaCursoInferiorJSON(ccur, elecCur));
        // console.log("rpta: "+rpta);       
        // console.log("paso rpta1: " + rpta1);

        if (!rpta) {
            //console.log("sal if ");
            $('#chkCurso' + ccur).prop('checked', false);
            fnBuscarData(ccur, "chkup", false);
            return false;
        }

        //console.log(aData);
        // // //console.log("paso rpta: " + rpta);
        rpta2 = fnValidaCursoSuperiorJSON(ccur, elecCur);
        // console.log("rpta2JSON: " + fnValidaCursoSuperiorJSON(ccur, elecCur));
        // console.log("rpta2: " + rpta2);
        if (!rpta2) {
            fnMensaje('warning', 'No puede deseleccionar este curso, intente con cursos de ciclos superiores seleccionados');
            return false;
        }
        //// //console.log("paso rpta2: " + rpta2);
        rpta3 = fnValidaSelecciondpdJSON(ccur);

        // console.log("rpta3JSON: " + fnValidaSelecciondpdJSON(ccur, elecCur));
        // console.log("rpta3: " + rpta3);
        //// //console.log(rpta3);
        if (!rpta3) {
            fnMensaje('warning', 'No puede seleccionar este curso, primero seleccione cursos desaprobados');
            return false;
        }
        //// //console.log("paso rpta3: " + rpta3);
        //// //console.log("selCurso.length: "+selCurso.length);
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
                        //  console.log(sumCostoCuota + "=" + costoCuota + "-(" + selCred + "*" + precioCredito + "*" + v1+")");
                        sumCostoCuota = sumCostoCuota + costoCuota - (selCred * precioCredito * v1);
                        break;
                    case 2:
                        //  console.log(sumCostoCuota + "=" + costoCuota + "-(" + selCred + "*" + precioCredito + "*" + vn + ")");
                        sumCostoCuota = sumCostoCuota + costoCuota - (selCred * precioCredito * vn);
                        break;
                    default:
                        //  console.log(sumCostoCuota + "=" + costoCuota + "-(" + selCred + "*" + precioCredito + ")");
                        sumCostoCuota = sumCostoCuota + costoCuota - (selCred * precioCredito);
                        break;
                }

                //  console.log("1-  sumCostoCuota: " + sumCostoCuota);
                $('#h6CostoCuota').html((sumCostoCuota / cuotas).toFixed(2));
                $('#h6CostoCiclo').html(sumCostoCuota.toFixed(2));
            } else {
                //console.log(sumCostoCuota + "=" + sumCostoCuota + "+" + costoCuota + "-" + precioCalc);
                sumCostoCuota = sumCostoCuota + costoCuota - precioCalc;
                // console.log("2-  sumCostoCuota: " + sumCostoCuota);
                $('#h6CostoCuota').html(sumCostoCuota.toFixed(2));
                $('#h6CostoCiclo').html((sumCostoCuota / cuotas).toFixed(2));

            }
            //console.log(sumCostoCuota + "=" + costoCuota + "-" + selCred + "*" + precioCredito);

        }

        if (selCurso == ccup) {
            eliminarCurso(ccur);
            $('#' + ccur).css("background-color", "");
            $("input[name=btnCurso" + ccur + "]").css("background-color", "");
            //$('#selCurso' + ccur).val("");
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
            //$('#selCurso' + ccur).val(ccup);
            fnBuscarData(ccur, "selCup", ccup);
            $('#' + ccur).css("background-color", "#2196f3");

            $("input[name=btnCurso" + ccur + "]").css("background-color", "");
            $("input[id=btnCurso" + ccup + "]").css("background-color", "#2196f3");

            //CalendarAgregaEvento(ccup, nomCurso, fecInicio, fecFin, false);            
            //CalendarAgregaEvento(ccup, 1444032000000, 1444039200000, false);
            sw = true;
        }

        if (sw) {
            //// //console.log("agregar cur: " + ccur + " cup: " + ccup);

            if (aDataCnf[0].tipocac == 'E') {
                var _c = fnNivel(ccur);

                if (!_c) {
                    $('#' + ccur).css("background-color", "");
                    $("input[name=btnCurso" + ccur + "]").css("background-color", "");
                    //$('#selCurso' + ccur).val("");
                    fnBuscarData(ccur, "selCup", "");
                    $('#chkCurso' + ccur).prop('checked', false);
                    fnBuscarData(ccur, "chkup", false);
                    return false;
                }
            }
            // var tc = $('#chkCurso' + ccur).attr("vtc");
            var tc = fnBuscarData(ccur, "tc", "");

            _c = fnValidadCC(tc);
            if (!_c) {
                $('#' + ccur).css("background-color", "");
                $("input[name=btnCurso" + ccur + "]").css("background-color", "");
                //$('#selCurso' + ccur).val("");
                fnBuscarData(ccur, "selCup", "");
                $('#chkCurso' + ccur).prop('checked', false);
                fnBuscarData(ccur, "chkup", false);
                if (tc == "I") { fnMensaje('warning', 'No puede seleccionar mas de 1 curso de idiomas'); } else {
                    fnMensaje('warning', 'No puede seleccionar mas de 1 curso de computo');
                }
                return false;
            }


            var curSel = parseInt($('#h6CursosSeleccionados').html());
            var sumcurSel = 0;
            sumcurSel = curSel + 1;
            $('#h6CursosSeleccionados').html(sumcurSel);

            var credSel = parseInt($('#h6CreditosSeleccionados').html());
            var sumcredSel = 0;
            sumcredSel = credSel + selCred;
            if (sumcredSel > lmtcrd) fnMensaje('warning', 'No puede llevar mas de ' + lmtcrd + ' cr&eacute;ditos');
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

            $('#h6CostoCuota').html((sumCostoCuota / cuotas).toFixed(2));
            $('#h6CostoCiclo').html(sumCostoCuota.toFixed(2));


            var horCount = parseInt($("#txthornum" + ccup).val());

            var i;
            //var nomCurso = $('#nomCurso' + ccur).val();
            var nomCurso = fnBuscarData(ccur, "selC", "");
            //var vtc = $('#chkCurso' + ccur).attr("vtc");
            var vtc = fnBuscarData(ccur, "tc", "");
            //// //console.log("horCount :" + horCount);
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
            //// //console.log(key.cod +'!='+ obj.cod);
            if (key.cod != obj.cod) {
                var fecha = DevuelveFechaCalendario(obj.dia);
                //// //console.log(kfecha + ' ' + key.f1 + ' > ' + fecha + ' ' + obj.f1+' Y '+kfecha + ' ' + key.f1 +' < '+ fecha + ' ' + obj.f2);
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
        //var ccup = $('#selCurso' + ccur).val();
        var ccup = fnBuscarData(ccur, "selC", "");
        var horCount = parseInt($("#txthornum" + ccup).val());
        var i;
        // var nomCurso = $('#nomCurso' + ccur).val();
        for (i = 0; i < horCount; i++) {
            delete arrCursos[$("#txtdia" + ccup + "\\[" + i + "\\]").val() + ccup];
            delete arrData[ccup];
        }
    }

    function eventoCurso(i, ccup, ccur, horCount, accion, cm, rem, vtc) {
        // // //console.log("i :" + i + "horCount: " + horCount);
        var nomCurso = fnBuscarData(ccur, "nomC", "");
        var vcs = fnBuscarData(ccur, "vec", "");
        if (accion == "A") {
            arrCursos[$("#txtdia" + ccup + "\\[" + i + "\\]").val() + ccup] = {
                cod: $("#txtdia" + ccup + "\\[" + i + "\\]").val() + ccup,
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

            // // //console.log(arrCursos[$("#txtdia" + ccup + "\\[" + i + "\\]").val() + ccup]['dia']);
        }
        else {
            delete arrCursos[$("#txtdia" + ccup + "\\[" + i + "\\]").val() + ccup];
            delete arrData[ccup];
        }


    }

    function DevuelveFechaCalendario(nD) {
        //// //console.log(obj);
        var fecha;
        $.ajax({
            type: "GET",
            contentType: "application/json; charset=utf-8",
            url: "procesar.aspx",
            data: { "param0": "fSr", "param2": nD },
            dataType: "json",
            async: false,
            success: function(data) {
                // // //console.log(data);
                fecha = data[0].fec;
            },
            error: function(result) {
                //// //console.log(result)
            }
        });

        return fecha;
    }

    function fnUpReqCur() {
        $('.piluku-preloader').removeClass('hidden');
        $.ajax({
            type: "GET",
            contentType: "application/json; charset=utf-8",
            url: "procesar.aspx",
            data: { "param0": "fUpReqCur" },
            dataType: "json",
            async: false,
            success: function(data) {
                ////console.log(data);
                if (data[0].resultado == "OK") {
                    fnMensaje("success", data[0].msj);
                    $('#tablagruposdisponibles  thead  tr th:eq(0)').html('');
                    _SWUPREQ = false;
                    $('.piluku-preloader').addClass('hidden');
                    f_Menu('matricula.aspx');
                } else {
                    fnMensaje("warning", data[0].msj);
                }

            },
            error: function(result) {

            }
        });
    }

    function fnGuardar() {

        var obj = {};
        var i = 0;
        for (var idx in arrData) {
            key = arrData[idx];
            if (key.cm == "0")
                obj[i] = key;
            i++;
        }


        $.ajax({
            type: "GET",
            contentType: "application/json; charset=utf-8",
            url: "procesar.aspx",
            data: { "param0": "regMat", "param1": obj },
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
                }
            },
            error: function(result) {
            }
        });
    }