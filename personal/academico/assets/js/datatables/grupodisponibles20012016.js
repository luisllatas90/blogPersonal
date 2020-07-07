var arrCursos = [];
var arrData = [];
var cr, key;

var cmt;
$(document).ready(function() {
    // $('#divInfoDetails').hide();
    // var maxCred, montoCredPen, desaprobado1v, desaprobado2v;

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



    cmt = parseInt($("#txtmat").val());




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
            var selCurso = $('#selCurso' + cur).val();


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
                    success: function(data) {
                        //// //console.log(data);
                        sOut = '<table cellpadding="1" cellspacing="1" border="0" style="width:100%;font-size:11px;">';
                        var c;

                        if (data.length > 0) {
                            for (var i = 0; i < data.length - 1; i++) {
                                if (i < (data.length - 1)) {
                                    if (data[i].codigo_cup != data[i + 1].codigo_cup) {
                                        sOut += '<tr>';

                                        sOut += '<td align="center">';
                                        if (data[i].estado_cup) {
                                            if ($('#selCurso' + data[i].codigo_cur).val() == data[i].codigo_cup) {
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

                            sOut += '<tr>';
                            sOut += '<td align="center">';
                            if (data[data.length - 1].estado_cup) {
                                if ($('#selCurso' + data[data.length - 1].codigo_cur).val() == data[data.length - 1].codigo_cup) {
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


                        sOut += '</table>';

                        oTable.fnOpen(nTr, sOut, 'details' + cur);
                        $('.details' + cur).val()
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

        var cic;
        var eachCur;
        var velap;
        var vnel;


        $('input[name=chkCurso]').each(function() {

            eachCur = $(this).attr("vcur");
            vtc = $(this).attr("vtc");
            velap = parseInt($(this).attr("velap"));
            vnel = parseInt($(this).attr("vnel"));
            _elect = $('#eleCur' + eachCur).val();
            //$('#chkCurso' + eachCur).css("display", "block");
            if (_elect == 'True' && vnel > 0) {
                if (vnel <= velap) {
                    $('#chkCurso' + eachCur).prop('checked', true);
                }
            }

        });
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

        var rem = parseInt($("#txtmat").val());

        //  // //console.log("mat: " + cm);
        if (rem == 0) return false;
        //// //console.log("carga horario");
        var sThisVal;
        var cm;
        var cc;
        var vcs;
        var vtc;
        var param1 = "";
        
        
        $('input[name=chkCurso]').each(function() {
            sThisVal = (this.checked ? "1" : "0");
            cm = parseInt($(this).attr("vcurm"));
            cc = parseInt($(this).attr("vcur"));
            vtc = $(this).attr("vtc");
           
            if (sThisVal == "1" && cm == 1) {
                var ccup = parseInt($("#selCurso" + cc).val());               
                var i;
                var nomCurso = $('#nomCurso' + cc).val();
                vcs = parseInt($(this).attr("vvec"));
                $.ajax({
                    type: "GET",
                    contentType: "application/json; charset=utf-8",
                    url: "detallematricula.aspx",
                    data: { "cur": cc, "param1": param1 },
                    dataType: "json",
                    async: false,
                    success: function(data) {                  
                        if (data.length > 0) {
                            
                            jQuery.each(data, function(j, hor) {
                                if (hor.codigo_cup == ccup) {
                                    // // //console.log(hor.codigo_cup);
                                    eventoCursoCargaHorario(cc, nomCurso, cc, hor.dia_Lho, hor.nombre_Hor, hor.horaFin_Lho, ccup, vcs, "A", cm, rem, vtc);
                                    // // //console.log(cc + "  " + nomCurso + "  " + cc + "  " + hor.dia_Lho + "  " + hor.nombre_Hor + "  " + hor.horaFin_Lho + "  " + ccup + "  " + vcs + "  A");
                                }
                            });
                        } 
                    },
                    error: function(result) {   
                    }
                });
            }
        });

        $('input[name=chkCursoMat]').each(function() {
            sThisVal = (this.checked ? "1" : "0");
            cm = parseInt($(this).attr("vcurm"));
            cc = parseInt($(this).attr("vcur"));
            vtc = $(this).attr("vtc");
           
            if (sThisVal == "1" && cm == 1) {
                var ccup = parseInt($("#selCurso" + cc).val());
                var i;
                var nomCurso = $('#nomCurso' + cc).val();
                vcs = parseInt($(this).attr("vvec"));
                $.ajax({
                    type: "GET",
                    contentType: "application/json; charset=utf-8",
                    url: "detallematricula.aspx",
                    data: { "cur": cc, "param1": param1 },
                    dataType: "json",
                    async: false,
                    success: function(data) {
                        if (data.length > 0) {

                            jQuery.each(data, function(j, hor) {
                                if (hor.codigo_cup == ccup) {
                                   
                                    eventoCursoCargaHorario(cc, nomCurso, cc, hor.dia_Lho, hor.nombre_Hor, hor.horaFin_Lho, ccup, vcs, "A", cm, rem, vtc);
                                  
                                }
                            });
                        }
                    },
                    error: function(result) {
                    }
                });
            }
        });
        
        
    }

    function fnCalcularPension() {
      
        var ccur;
        var sumCostoCuota = 0;
        for (var idx in arrData) {
            key = arrData[idx];
            ccur = key.ccur;
          

            var selCred = parseInt($('#credCurso' + ccur).val());
            var precioCredito = parseFloat($("#txtprecioCredito").val());
            var precioCalc=parseFloat($("#precioCalc" + ccur).val());
            var precioCur = 0;

            var vecesDesap = parseInt($('#vecesDesap' + ccur).val());
            var v1 = parseFloat($("#txtveces1").val()) + 1;
            var vn = parseFloat($("#txtvecesn").val()) + 1;
            var cuotas = parseInt($("#txtcuotas").val());

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

            
            if (selCred>0) {
                switch (vecesDesap) {
                    case 1:
                       
                       sumCostoCuota = sumCostoCuota + (selCred * precioCredito * v1);
                        
                        break;
                    case 2:
                        
                       sumCostoCuota = sumCostoCuota +  (selCred * precioCredito * vn);
                        
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
             //   console.log(cic + "==" + cicCur + "  " + sThisVal + " cm: " + cm);
                if (cm == 0) {
                    if (sThisVal == "1" && _elect != "True") {
                        sel = 1;
                    }
                }
                c++;
            }
        });
       // console.log("cm: " + cm);
       //console.log("sel: " + sel + "c: "+c);

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

                // //console.log(arrDataEl);
                /*for (var idx in arrDataEl) {
                key = arrDataEl[idx];
                $('#chkCurso' + key.ccur).prop('checked', true);
                delete arrDataEl[key.ccur];
                }
                */
                if (sThisVal == "0" && (vtc == "I" || vtc == "C")) {
                    if (!fnValidadCC(vtc)) {
                        _swtc = false;
                        validaIC++;
                    }
                }

                if (_swtc) {

                    if (cic <= cicCur && eachCur == cur) {


                        $('input[name=txtst' + eachCur + ']').each(function() {
                            if (parseInt($(this).val()) == 1) st++;
                        });

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
        ////console.log("validaIC: " + validaIC);

        //// //console.log('2sThisVal ' + sThisVal + ' st' + st);
        // //console.log(_sw +" || " + c +" > " +1 + " && "+ sel +" == "+1)
        // if (!_sw) fnMensaje('warning', '1Debe seleccionar cursos de ciclos inferiores');
        //  if (!(c > 1 && sel == 1)) fnMensaje('warning', '2Debe seleccionar cursos de ciclos inferiores');

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
        var c = $("#txtcond").val();
        var i = 0;
        var niv = 0;
        var ad = 0;
        
        // for (var idx in arrData) i++;
        //'#chkCurso'+cur
        
        var vcs = parseInt($('#chkCurso' + cur).attr("vvec"));
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
            if(ad<=2 && niv>=0){
                return true;
            }else{
             fnMensaje('warning', 'No puede adelantar mas de 2 cursos');
                return false;
            }
        } else if (c == 'P') {
             if(ad<=2 || niv<=2){
               return true;
            } else {
            fnMensaje('warning', 'No puede adelantar mas de 1 curso');
                return false;
                
            }
        }
        else if (c == 'C') {
            if(ad==0 && niv<=2){
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
        var lmtcrd = parseInt($('#txtcredMaxMat').val());
        var selCurso = $('#selCurso' + ccur).val();
        var rem = parseInt($("#txtmat").val());

        var selCred = parseInt($('#credCurso' + ccur).val());
        var precioCredito = parseFloat($("#txtprecioCredito").val());
        var precioCalc = parseFloat($("#precioCalc" + ccur).val());
        var precioCur = 0;

        //var precioCredito=precioCredito;
        var vecesDesap = parseInt($('#vecesDesap' + ccur).val());
        var v1 = parseFloat($("#txtveces1").val()) + 1;
        var vn = parseFloat($("#txtvecesn").val()) + 1;
        var cuotas = parseInt($("#txtcuotas").val());
        precioCredito = precioCredito * 5;

        var elecCur = $("#eleCur" + ccur).val();
        var sw = false;
        rpta1 = fnValidaCursoMatriculado(ccur);
        //// //console.log(rpta1);

        // valida cursos inferiores
        rpta = fnValidaCursoInferior(ccur, elecCur);

        if (rpta1) {
            fnMensaje('warning', 'No puede deseleccionar, ya se encuentra matriculado en este curso');
            return false;
        }
        //// //console.log("paso rpta1: " + rpta1);

        if (!rpta) {
            // // //console.log("sal if ");
            $('#chkCurso' + ccur).prop('checked', false);
            return false;
        }
        // // //console.log("paso rpta: " + rpta);
        rpta2 = fnValidaCursoSuperior(ccur, elecCur);
        if (!rpta2) {
            fnMensaje('warning', 'No puede deseleccionar este curso, intente con cursos de ciclos superiores seleccionados');
            return false;
        }
        //// //console.log("paso rpta2: " + rpta2);
        rpta3 = fnValidaSelecciondpd(ccur);
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

            if (selCred>0) {

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
                       // console.log(sumCostoCuota + "=" + sumCostoCuota + "+" + costoCuota + "-" + precioCalc);
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
            $('#selCurso' + ccur).val("");
            $('#chkCurso' + ccur).prop('checked', false);

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
            $('#selCurso' + ccur).val(ccup);
            $('#' + ccur).css("background-color", "#2196f3");

            $("input[name=btnCurso" + ccur + "]").css("background-color", "");
            $("input[id=btnCurso" + ccup + "]").css("background-color", "#2196f3");

            //CalendarAgregaEvento(ccup, nomCurso, fecInicio, fecFin, false);            
            //CalendarAgregaEvento(ccup, 1444032000000, 1444039200000, false);
            sw = true;
        }

        if (sw) {
            //// //console.log("agregar cur: " + ccur + " cup: " + ccup);


            var _c = fnNivel(ccur);

            if (!_c) {
                $('#' + ccur).css("background-color", "");
                $("input[name=btnCurso" + ccur + "]").css("background-color", "");
                $('#selCurso' + ccur).val("");
                $('#chkCurso' + ccur).prop('checked', false);
                return false;
            }
            var tc = $('#chkCurso' + ccur).attr("vtc");

            _c = fnValidadCC(tc);
            if (!_c) {
                $('#' + ccur).css("background-color", "");
                $("input[name=btnCurso" + ccur + "]").css("background-color", "");
                $('#selCurso' + ccur).val("");
                $('#chkCurso' + ccur).prop('checked', false);
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
            
            if (selCred>0) {
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
             //console.log(sumCostoCuota + "=" + costoCuota + "+" + selCred + "*" + precioCredito);
            $('#h6CostoCuota').html((sumCostoCuota / cuotas).toFixed(2));
            $('#h6CostoCiclo').html(sumCostoCuota.toFixed(2));


            var horCount = parseInt($("#txthornum" + ccup).val());

            var i;
            var nomCurso = $('#nomCurso' + ccur).val();
            var vtc = $('#chkCurso' + ccur).attr("vtc");
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
                if ((kfecha + ' ' + key.f1 == (fecha + ' ' + obj.f1)) ) {

                    return true;
                }
            }
        }

        return false;
    }

    function eliminarCurso(ccur) {
        var ccup = $('#selCurso' + ccur).val();
        var horCount = parseInt($("#txthornum" + ccup).val());
        var i;
        var nomCurso = $('#nomCurso' + ccur).val();
        for (i = 0; i < horCount; i++) {
            delete arrCursos[$("#txtdia" + ccup + "\\[" + i + "\\]").val() + ccup];
            delete arrData[ccup];
        }
    }

    function eventoCurso(i, ccup, ccur, horCount, accion, cm, rem, vtc) {
        // // //console.log("i :" + i + "horCount: " + horCount);
        if (accion == "A") {

            arrCursos[$("#txtdia" + ccup + "\\[" + i + "\\]").val() + ccup] = {
                cod: $("#txtdia" + ccup + "\\[" + i + "\\]").val() + ccup,
                nom: $('#nomCurso' + ccur).val(),
                ccur: ccur,
                dia: $("#txtdia" + ccup + "\\[" + i + "\\]").val(),
                f1: $("#txtfecIni" + ccup + "\\[" + i + "\\]").val(),
                f2: $("#txtfecFin" + ccup + "\\[" + i + "\\]").val(),
                cup: ccup,
                vcs: parseInt($("#chkCurso" + ccur).attr("vvec")),
                cm: cm,
                rem: rem
            };

            arrData[ccup] = {
                nom: $('#nomCurso' + ccur).val(),
                ccur: ccur,
                cup: ccup,
                vcs: parseInt($("#chkCurso" + ccur).attr("vvec")),
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