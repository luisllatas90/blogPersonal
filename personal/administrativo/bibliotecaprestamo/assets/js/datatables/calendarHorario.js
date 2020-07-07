/*fnCargaHorario();
fnDataHorario();
*/

var arrCursos = [];
var arrData = [];

function fnDataHorario() {
try{
    var mod = $('#hdmod').val();
    CalendarEliminarEvento();
    for (var idx in arrCursos) {
        key = arrCursos[idx];
        //console.log(key);
        if (mod == '2') {
            var fecha = DevuelveFechaCalendario(key.dia);
            CalendarAgregaEvento(key.cup, key.nom, fecha + ' ' + key.f1, fecha + ' ' + key.f2, false, 0);
        } else {
            CalendarAgregaEvento(key.cup, key.nom,  key.f1, key.f2, false, 0);
        }
        
    }
}
catch (err) {

    console.log(err.message);
}
}
function fnCargaHorario() {
try{


    var nomCurso;
    var cc;
    var cup;
    var st;
    $('input[name=chkCurso]').each(function() {
        var i;
        cc = parseInt($(this).attr("vCur"));
        cup = parseInt($(this).attr("vCup"));
        nomCurso = $(this).attr("vNom");
        st = $(this).attr("vst");
        vcs = 0;
        //console.log(cc);
        if (st != 'R') {
            $.ajax({
                type: "GET",
                contentType: "application/json; charset=utf-8",
                url: "detallematricula.aspx",
                data: { "cur": cc, "param1": "mat" },
                dataType: "json",
                async: false,
                success: function(data) {
                    console.log('lista');
                    console.log(data);
                    var mod = $('#hdmod').val();
                    if (data.length > 0) {

                        jQuery.each(data, function(j, hor) {
                            if (hor.codigo_cup == cup && data.cur_Mat != "0") {
                                if (mod == '2') {
                                    //eventoCursoCargaHorario(cc, nomCurso + '\n Grupo (' + hor.grupohor_cup + ')\n' + hor.ambiente, cc, hor.dia_Lho, hor.nombre_Hor, hor.horaFin_Lho, cup, vcs, "A", 0, 0);
                                    eventoCursoCargaHorario(cc, nomCurso + '\n Grupo (' + hor.grupohor_cup + ') ' + hor.ambiente, cc, hor.dia_Lho, hor.nombre_Hor, hor.horaFin_Lho, cup, vcs, "A", 0, 0);
                                } else {
                                    eventoCursoCargaHorario(cc, nomCurso + '\n Grupo (' + hor.grupohor_cup + ') ' + hor.ambiente, cc, hor.dia_Lho, hor.fechaini, hor.fechafin, cup, vcs, "A", 0, 0);
                                }
                            }
                        });

                    } //if (data.length > 0)
                    // console.log(arrCursos);


                },
                error: function(result) {
                    //  console.log(result)
                    //f_Menu('matricula.aspx');
                }
            });
        }


    });
    }
    catch (err) {
    
       console.log(err.message);
    }
}

function eventoCursoCargaHorario(cod, nom, ccur, dia, f1, f2, ccup, vcs, accion, cm, rem) {
    // console.log("i :" + i + "horCount: " + horCount);
    if (accion == "A") {

        arrCursos[dia + f1+ccup] = {
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

       // console.log(arrCursos);

        // console.log(arrCursos[$("#txtdia" + ccup + "\\[" + i + "\\]").val() + ccup]['dia']);
    }
    else {
        delete arrCursos[dia +f1+ ccup];

    }


}

function DevuelveFechaCalendario(nD) {
    //console.log(obj);
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
            //console.log(result)
            //f_Menu('matricula.aspx');
        }
    });

    return fecha;
}