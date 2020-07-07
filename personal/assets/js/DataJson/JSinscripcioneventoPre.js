var lstCco;
var cco;
var titulo;
$(document).ready(function() {
    var dt = fnCreateDataTableBasic('dtParticipanteInsc', 1, 'asc');
    $('#PanelEvento').hide();
    $('#PanelInscripcionRegistroSinCargo').hide();
   
    ope = fnOperacion(2);
    lstCco = fnCargaCentroCosto();
    //console.log(lstCco);
    fnLoading(false);
    //var myArray = '[{"OriginId":2609,"OriginName":"14th Mile Stone"},{"OriginId":2007,"OriginName":"Aachara"},{"OriginId":2220,"OriginName":"Aarni"},{"OriginId":2216,"OriginName":"Aasind"},{"OriginId":637,"OriginName":"Aathankarai"},{"OriginId":1292,"OriginName":"Aatthur"},{"OriginId":1144,"OriginName":"Aavanam"},{"OriginId":2909,"OriginName":"Abad (Airport)"},{"OriginId":379,"OriginName":"Abiramam"},{"OriginId":4556,"OriginName":"ABLOLI"},{"OriginId":4554,"OriginName":"ABLOLI KALE HOUSE"},{"OriginId":2346,"OriginName":"Abohar"},{"OriginId":2500,"OriginName":"Abu Road"},{"OriginId":4395,"OriginName":"ACHALPUR"},{"OriginId":1594,"OriginName":"Achanta"},{"OriginId":2769,"OriginName":"Adda Road"}]';

    var jsonString = JSON.parse(lstCco);
    //console.log(jsonString);
    
    $('#busPoint').autocomplete({
        source: $.map(jsonString, function(item) {
            return item.nombre;
        }),
        select: function(event, ui) {
            var selectecItem = jsonString.filter(function(value) {
                return value.nombre == ui.item.value;
            });
            cco = selectecItem[0].cod;
            titulo = selectecItem[0].nombre;

            $('#PanelEvento').hide("fade");
            //alert("cod: " + selectecItem[0].cod + ", nombre: " + selectecItem[0].nombre);
        },
        minLength: 0,
        delay: 100
    });

    $('#btnListar').click(fnConsultar);
    
    $('#busPoint').keyup(function() {
        var l = parseInt($(this).val().length);
        if (l == 0) {
            cco = "";
            titulo = "";
        }
    });
    
    $('#btnBuscarParticipanteInsc').click(fnBuscarParticipanteInsc);


    $('#lnkInscripciones').click(fnBuscarParticipanteInsc);
    $('#btnPerNatSinCargo').click(fnPerNatSinCargo);
    $('#btnPerNatSinCargoGuardar').click(fnPerNatSinCargoGuardar);
    $('#btnPerNatSinCargoCancelar').click(fnPerNatSinCargoCancelar);
    $('#cbopscdpto').change(fnProvincia);
    $('#cbopscdprov').change(fnDistrito);
    $('#txtpscnrodocident').keypress(function(e) {
        if (e.which == 13) {
            fnBuscarParticipanteInscDoc();
        }
    });
    $('#btnBuscarPersonasc').click(fnBuscarParticipanteInscDoc);
    $('#btnComprobarNombres').click(fnBuscarParticipanteInscApe);

});
function openModal(md) {
    $('#' + md).modal('show');
  //  $('#' + md).modal('toggle');
}

function closeModal(md) {
   // $('#' + md).close();
    //$('#modal').modal('toggle');
    //$('#' + md).modal().hide();
    $('#' + md).modal('hide');
}

function fnSelPersona(cod) {

    fnDivLoad('PanelRegPersonasc', 1000);

    var valor = $('#txtpscnrodocident').val();
    $('#frmBuscarPartInscDoc').append('<input type="text" id="process" name="process" value="' + ope.bsq + '" />');
    $('#frmBuscarPartInscDoc').append('<input type="text" id="cco" name="cco" value="' + cco + '" />');
    $('#frmBuscarPartInscDoc').append('<input type="text" id="tipo" name="tipo" value="COE" />');
    $('#frmBuscarPartInscDoc').append('<input type="text" id="valor" name="valor" value="' + cod + '" />');

    var form = $("#frmBuscarPartInscDoc").serializeArray();
    console.log(form);

    $.ajax({
        type: "POST",
        url: "../../DataJson/PreUniversitaria/persona.aspx",
        data: form,
        dataType: "json",
        cache: false,
        success: function(data) {
            $("form#frmBuscarPartInscDoc input[id=process]").remove();
            $("form#frmBuscarPartInscDoc input[id=cco]").remove();
            $("form#frmBuscarPartInscDoc input[id=tipo]").remove();
            $("form#frmBuscarPartInscDoc input[id=valor]").remove();
            console.log(data);
            var tb = '';
            var i = 0;
            var filas = data.length;
            if (filas > 0) {
                if (data[0].sw) {
                    fnLimpiar('psc');
                    $('#cbopscTipoDoc').val(data[0].tipodoc);
                    $('#txtpscnrodocident').val(data[0].nrodoc);                    
                    $('#txtpscapepat').val(data[0].apepat);
                    $('#txtpscapemat').val(data[0].apemat);
                    $('#txtpscnombre').val(data[0].nombre);
                    $('#txtpsfechanac').val(data[0].fecnac);
                    $('#cbopscSexo').val(data[0].sexo);
                    $('#cbopscEstadoCivil').val(data[0].estadocivil);
                    $('#txtpscemailpri').val(data[0].emailpri);
                    $('#txtpscemailalt').val(data[0].emailalt);
                    $('#txtpscdireccion').val(data[0].direccion);
                    $('#cbopscdpto').val(data[0].dpto);
                    fnProvincia();
                    $('#cbopscdprov').val(data[0].prov);
                    fnDistrito();
                    $('#cbopscddist').val(data[0].dist);
                    $('#txtpscfono').val(data[0].fonofijo);
                    $('#txtpsccel').val(data[0].fonomovil);
                    $('#txtpscruc').val(data[0].ruc);
                    $('#cbopscModIng').val(data[0].codMin);
                    $("#cbopscModIng option[value='" + data[0].codMin + "']").attr('selected', 'selected');
                    if (data[0].bloqMin) {
                        console.log(data[0].bloqMin);
                        $("#cbopscModIng").attr("onchange", "fnSelectChange('cbopscModIng')");
                        $("#cbopscModIng").attr("onfocus", "fnSelectFocus('cbopscModIng')");
                    }
                    closeModal('mdListaCoincidencia');

                } else {
                    fnMensaje(data[0].alert, data[0].msje);
                    fnLimpiar('psc');
                    $('#txtpscnrodocident').val(nrodoc);
                }


            }


        },
        error: function(result) {
            $("form#frmBuscarPartInscDoc input[id=process]").remove();
            $("form#frmBuscarPartInscDoc input[id=cco]").remove();
            $("form#frmBuscarPartInscDoc input[id=tipo]").remove();
            $("form#frmBuscarPartInscDoc input[id=valor]").remove();
        }
    });    
    
    
}

function fnBuscarParticipanteInscApe() {
    fnDivLoad('PanelRegPersonasc', 1000);

    var valor = $('#txtpscapepat').val() + ' ' + $('#txtpscapemat').val();
    
    $('#frmBuscarPartInscDoc').append('<input type="text" id="process" name="process" value="' + ope.bsq + '" />');
    $('#frmBuscarPartInscDoc').append('<input type="text" id="cco" name="cco" value="' + cco + '" />');
    $('#frmBuscarPartInscDoc').append('<input type="text" id="tipo" name="tipo" value="APE" />');
    $('#frmBuscarPartInscDoc').append('<input type="text" id="valor" name="valor" value="' + valor + '" />');

    var form = $("#frmBuscarPartInscDoc").serializeArray();
    console.log(form);
    
    $.ajax({
        type: "POST",
        url: "../../DataJson/PreUniversitaria/persona.aspx",
        data: form,
        dataType: "json",
        cache: false,
        success: function(data) {
            $("form#frmBuscarPartInscDoc input[id=process]").remove();
            $("form#frmBuscarPartInscDoc input[id=cco]").remove();
            $("form#frmBuscarPartInscDoc input[id=tipo]").remove();
            $("form#frmBuscarPartInscDoc input[id=valor]").remove();
            //console.log(data);
            var tb = '';
            var i = 0;
            var filas = data.length;
            for (i = 0; i < filas; i++) {
                if (i == 0 && !data[i].sw) {
                    fnMensaje('warning', data[i].msje);
                    break;
                }
                fnLimpiar('psc');
                tb += '<tr>';
                tb += '<td style="width:4%;">';
                tb += '<a href="#" class="btn btn-primary" onclick="fnSelPersona(\'' + data[i].cod + '\')" ><i class="ion-android-done"></i></a>';
                tb += '</td>';
                tb += '<td style="width:15%;">' + data[i].apepat + '</td>';
                tb += '<td style="width:15%;">' + data[i].apemat + '</td>';
                tb += '<td style="width:15%;">' + data[i].nombre + '</td>';
                tb += '<td style="width:10%;">' + data[i].nrodocident + '</td>';
                tb += '<td style="width:10%;">' + data[i].fechanac + ' </td>';
                tb += '<td style="width:20%;">' + data[i].direccion + ' </td>';
                tb += '<td style="width:15%;">' + data[i].email + ' </td>';
                tb += '</tr>';

            }

            fnDestroyDataTableDetalle('tblPersona');
            $('#tbdPersona').html(tb);
            fnResetDataTableBasic('tblPersona', 1, 'asc');
//            var oTableAprobReq = $('#tblPersona').DataTable({
//                "bJQueryUI": false,
//                "sPaginationType": "full_numbers",
//                "bFilter": true,
//                "bLengthChange": false,
//                "bSort": true,
//                "bInfo": true,
//                "bAutoWidth": true,
//                "aaSorting": [[1, "asc"]]
//            });
            openModal('mdListaCoincidencia');

        },
        error: function(result) {
            $("form#frmBuscarPartInscDoc input[id=process]").remove();
            $("form#frmBuscarPartInscDoc input[id=cco]").remove();
            $("form#frmBuscarPartInscDoc input[id=tipo]").remove();
            $("form#frmBuscarPartInscDoc input[id=valor]").remove();
        }
    });                              
    

}


function fnBuscarParticipanteInscDoc() {
    fnDivLoad('PanelRegPersonasc', 1000);
    
    var valor = $('#txtpscnrodocident').val();
    $('#frmBuscarPartInscDoc').append('<input type="text" id="process" name="process" value="' + ope.bsq + '" />');
    $('#frmBuscarPartInscDoc').append('<input type="text" id="cco" name="cco" value="' + cco + '" />');
    $('#frmBuscarPartInscDoc').append('<input type="text" id="tipo" name="tipo" value="DNIE" />');
    $('#frmBuscarPartInscDoc').append('<input type="text" id="valor" name="valor" value="' + valor + '" />');
    
    var form = $("#frmBuscarPartInscDoc").serializeArray();
    console.log(form);

    $.ajax({
        type: "POST",
        url: "../../DataJson/PreUniversitaria/persona.aspx",
        data: form,
        dataType: "json",
        cache: false,
        success: function(data) {
            $("form#frmBuscarPartInscDoc input[id=process]").remove();
            $("form#frmBuscarPartInscDoc input[id=cco]").remove();
            $("form#frmBuscarPartInscDoc input[id=tipo]").remove();
            $("form#frmBuscarPartInscDoc input[id=valor]").remove();
            console.log(data);
            var tb = '';
            var i = 0;
            var filas = data.length;
            if (filas > 0) {
                if (data[0].sw) {
                    $('#cbopscTipoDoc').val(data[0].tipodoc);
                    $('#txtpscapepat').val(data[0].apepat);
                    $('#txtpscapemat').val(data[0].apemat);
                    $('#txtpscnombre').val(data[0].nombre);
                    $('#txtpsfechanac').val(data[0].fecnac);
                    $('#cbopscSexo').val(data[0].sexo);
                    $('#cbopscEstadoCivil').val(data[0].estadocivil);
                    $('#txtpscemailpri').val(data[0].emailpri);
                    $('#txtpscemailalt').val(data[0].emailalt);
                    $('#txtpscdireccion').val(data[0].direccion);
                    $('#cbopscdpto').val(data[0].dpto);
                    fnProvincia();
                    $('#cbopscdprov').val(data[0].prov);
                    fnDistrito();
                    $('#cbopscddist').val(data[0].dist);
                    $('#txtpscfono').val(data[0].fonofijo);
                    $('#txtpsccel').val(data[0].fonomovil);
                    $('#txtpscruc').val(data[0].ruc);
                    $('#cbopscModIng').val(data[0].codMin);
                    $("#cbopscModIng option[value='"+ data[0].codMin + "']").attr('selected', 'selected');
                    if (data[0].bloqMin) {
                        console.log(data[0].bloqMin);
                        $("#cbopscModIng").attr("onchange", "fnSelectChange('cbopscModIng')");
                        $("#cbopscModIng").attr("onfocus", "fnSelectFocus('cbopscModIng')");
                    }


                } else {
                    fnMensaje(data[0].alert, data[0].msje);
                    fnLimpiar('psc');
                    $('#txtpscnrodocident').val(nrodoc);
                }


            }


            /* for (i = 0; i < filas; i++) {
            if (i == 0 && !data[i].sw) {
            fnMensaje('warning', data[i].msje);
            break;
            }
            tb += '<tr>';
            tb += '<td style="width:4%;">';
            tb += '<div class="btn-group">';
            tb += '<button class="btn btn-default," data-toggle="dropdown">';
            tb += '<i class="ion-android-list"></i> <span class="caret"></span>';
            tb += '</button>';
            tb += '<ul class="dropdown-menu btn-drop" role="menu">';
            tb += '<li><a href="#"><i class="ion-search"></i> Mov</a>';
            tb += '</li>';
            tb += '<li><a href="#"><i class="ion-edit"></i> Modificar</a>';
            tb += '</li>';
            tb += '<li><a href="#"><i class="ion-email"></i> Enviar</a>';
            tb += '</li>';
            tb += '</li>';
            tb += '<li><a href="#"><i class="fa fa-file-pdf-o"></i> Imprimir</a>';
            tb += '</li>';
            tb += '</li>';
            tb += '<li><a href="#"><i class="ion-android-cancel"></i> Sol. Anulaci&oacute;n</a>';
            tb += '</li>';
            tb += '<li class="divider"></li>';
            tb += '<li><a href="#">Convenio</a>';
            tb += '</li>';
            tb += '</ul>';
            tb += '</div>';
            tb += '</td>';
            tb += '<td>' + (i + 1) + "" + '</td>';
            tb += '<td>' + data[i].nTipoDoc + '</td>';
            tb += '<td>' + data[i].nParticipante + '</td>';
            tb += '<td>' + data[i].cCodUni + '</td>';
            tb += '<td>' + data[i].nCicloIng + '</td>';
            tb += '<td style="text-align:right;">' + data[i].mCargoTotal + ' </td>';
            tb += '<td style="text-align:right;">' + data[i].mAbonoTotal + ' </td>';
            tb += '<td style="text-align:right;">' + data[i].mSaldoTotal + ' </td>';
            tb += '</tr>';
            }
            */

        },
        error: function(result) {
            $("form#frmBuscarPartInscDoc input[id=process]").remove();
            $("form#frmBuscarPartInscDoc input[id=cco]").remove();
            $("form#frmBuscarPartInscDoc input[id=tipo]").remove();
            $("form#frmBuscarPartInscDoc input[id=valor]").remove();
        }
    });                              
    
}

 function fnBuscarParticipanteInsc(sw) {
             if (sw){  fnLoading(true);}
             $('#frmBuscarPartInsc').append('<input type="hidden" id="process" name="process" value="'+ope.lst+'" />');
             $('#frmBuscarPartInsc').append('<input type="hidden" id="cco" name="cco" value="'+cco+'" />');
                    
               var form = $("#frmBuscarPartInsc").serializeArray();
               //console.log(form);
               
                  $.ajax({
                      type: "POST",
                      url: "../../DataJson/PreUniversitaria/inscripcioneventoPre.aspx",
                      data: form,
                      dataType: "json",
                      cache: false,
                      success: function(data) {
                         $("form#frmBuscarPartInsc input[id=process]").remove();
                         $("form#frmBuscarPartInsc input[id=cco]").remove();
                          console.log(data);
                          var tb = '';
                          var i = 0;
                          var filas = data.length;
                          for (i = 0; i < filas; i++) {
                              if (i == 0 && !data[i].sw) {
                                  fnMensaje('warning', data[i].msje);                                 
                                  break;
                              }
                              tb += '<tr>';
                              tb += '<td style="width:4%;">';
                              tb += '<div class="btn-group">';
							  tb += '<button class="btn btn-default," data-toggle="dropdown">';
							  tb += '<i class="ion-android-list"></i> <span class="caret"></span>';
							  tb += '</button>';
							  tb += '<ul class="dropdown-menu btn-drop" role="menu">';
							  tb += '<li><a href="#"><i class="ion-search"></i> Mov</a>';
							  tb += '</li>';
							  tb += '<li><a href="#"><i class="ion-edit"></i> Modificar</a>';
							  tb += '</li>';
							  tb += '<li><a href="#"><i class="ion-email"></i> Enviar</a>';
							  tb += '</li>';
							  tb += '</li>';
							  tb += '<li><a href="#"><i class="fa fa-file-pdf-o"></i> Imprimir</a>';
							  tb += '</li>';
							  tb += '</li>';
							  tb += '<li><a href="#"><i class="ion-android-cancel"></i> Sol. Anulaci&oacute;n</a>';
							  tb += '</li>';
							  tb += '<li class="divider"></li>';
							  tb += '<li><a href="#">Convenio</a>';
						      tb += '</li>';
							  tb += '</ul>';
						      tb += '</div>';
                              tb += '</td>';
                              tb += '<td>' + (i + 1) + "" + '</td>';
                              tb += '<td>' + data[i].nTipoDoc  + '</td>';
                              tb += '<td>' + data[i].nParticipante + '</td>';
                              tb += '<td>' + data[i].cCodUni + '</td>';
                              tb += '<td>' + data[i].nCicloIng + '</td>';
                              tb += '<td style="text-align:right;">' + data[i].mCargoTotal + ' </td>';
                              tb += '<td style="text-align:right;">' + data[i].mAbonoTotal + ' </td>';
                              tb += '<td style="text-align:right;">' + data[i].mSaldoTotal + ' </td>';
                              tb += '</tr>';
                          }
                          fnDestroyDataTableDetalle('dtParticipanteInsc');
                          $('#tbParticipanteInsc').html(tb);
                          fnResetDataTableBasic('dtParticipanteInsc', 1, 'asc');
                          $("div.toolbar").html(' <div class="btn-group"><a href="#" class="btn btn-green" ><i class="fa fa-file-excel-o"></i>&nbsp;Ver Reporte</a></div>');
                          if (sw) {fnLoading(false);}
                      },
                      error: function(result) {
                      }
                  });                          
          }

function fnConsultar() {
    if (titulo.length > 0) {      
        var tit = 'Inscripci&oacute;n: ';
        $('#PanelEvento').show("fade", function() {
            fnLoading(true);
            var arr = $.parseJSON(fnEventos(2, 2, cco));
            console.log(arr);
            if (parseInt(arr.length) > 0) {
                fnDivLoad('evento', 1000);
                $('#tittle').html(tit + ': ' + arr[0].nombre)
                $('#evenombrecorto').html(arr[0].nombre);
                $('#evenroresolucion').html(arr[0].nroresolucion);
                $('#evecordinador').html(arr[0].coordinador);
                $('#evefecini').html(arr[0].feciniprop);
                $('#evefecfin').html(arr[0].fecfinprop);

                $('#evecontado').html(arr[0].preunitcont);
                $('#evefinanciado').html(arr[0].preunifin);
                $('#evemontoincial').html(arr[0].montoinicial);
                $('#evecuota').html(arr[0].cuotas);

                $('#evedctoper').html(arr[0].porcdctoper);
                $('#evedctoalu').html(arr[0].porcdctoalu);
                $('#evedctocor').html(arr[0].porcdctocorp);
                $('#evedctoegr').html(arr[0].porcdctoegr);

                $('#evegestion').html(arr[0].gestion);
                $('#evehorario').html(arr[0].horario);
                $('#eveobs').html(arr[0].obs);
                fnLoading(false);   
            }
        });
                
    } else {
        $('#PanelEvento').hide("fade", function() {
            fnMensaje("warning", "Ingrese Evento");
        });
    }


}
function fnCargaCentroCosto() {
    var f = $('#hdTF').val();
    return fnCentroCosto(2, ope.teepu, f);
}

function fnMostrarPanelCapa(mostrar, ocultar) {
    $('#' + mostrar).show();
    $('#' + ocultar).hide();
    return true;
}

function fnCargaModalidad(cbo,sel) {
    var arr = fnModalidadIngreso(2, '', '', ope.teepu);
    
    var n = arr.length;
    var str = "";
    str += '<option value="" selected>-- Seleccione -- </option>';
    for (i = 0; i < n; i++) {
        str += '<option value="' + arr[i].cod + '">' + arr[i].nombre + '</option>';
    }
    $('#' + cbo).html(str);
}

function fnPerNatSinCargo() {
    fnDivLoad('inscripciones', 1000);
    fnMostrarPanelCapa('PanelInscripcionRegistroSinCargo', 'PanelInscripcionListar');
    var arrv = $.parseJSON(fnEventos(2, 0, cco));
    console.log(arrv);
    fnCargaModalidad('cbopscModIng', '');
    if (arrv[0].sw) {
        fnSelectTipoDocIdent('cbopscTipoDoc', '');
        fnSelectSexo('cbopscSexo', '');
        fnSelectEstadoCivil('cbopscEstadoCivil', '');
        
        fnDepartamento(156);
        $('#MsjeRegPsc').html('');
        $('#MsjeRegPsc').hide('');
        $('#btnPerNatSinCargoGuardar').show();        
    } else {    
        $('#MsjeRegPsc').show('');
        $('#MsjeRegPsc').html('No puede registrar participantes en este evento, debido a que no se ha registrado un Plan de Estudios');
        $('#btnPerNatSinCargoGuardar').hide();        
        $('#cbopscModIng').val(arrv[0].codmin);
    }
}

function fnPerNatSinCargoGuardar() {
    
    //if (sw) { fnLoading(true); }
    $('#frmPerNatSinCargo').append('<input type="hidden" id="process" name="process" value="' + ope.rpsc + '" />');
    $('#frmPerNatSinCargo').append('<input type="hidden" id="cco" name="cco" value="' + cco + '" />');

    var form = $("#frmPerNatSinCargo").serializeArray();
    console.log(form);

    $.ajax({
        type: "POST",
        url: "../../DataJson/PreUniversitaria/persona.aspx",
        data: form,
        dataType: "json",
        cache: false,
        success: function(data) {
            fnMostrarPanelCapa('PanelInscripcionListar', 'PanelInscripcionRegistroSinCargo');
            $("form#frmPerNatSinCargo input[id=process]").remove();
            $("form#frmPerNatSinCargo input[id=cco]").remove();
            console.log(data);
            var tb = '';
            var i = 0;
            var filas = data.length;
            
            
            //if (sw) { fnLoading(false); }
        },
        error: function(result) {
        }
    });                   
}

function fnPerNatSinCargoCancelar() {
    fnMostrarPanelCapa('PanelInscripcionListar', 'PanelInscripcionRegistroSinCargo');
    fnLimpiar('psc');
}

function fnLimpiar(opc) {

    if (opc == 'psc') {
        $('#cbopscTipoDoc').val('DNI');
        $('#txtpscnrodocident').val('');
        $('#txtpscapepat').val('');
        $('#txtpscapemat').val('');
        $('#txtpscnombre').val('');
        $('#txtpsfechanac').val('');
        $('#cbopscSexo').val('');
        $('#cbopscEstadoCivil').val('');
        $('#txtpscemailpri').val('');
        $('#txtpscemailalt').val('');
        $('#txtpscdireccion').val('');
        $('#cbopscdpto').val('');
        $('#cbopscdprov').html('');
        $('#cbopscddist').html('');
        $('#txtpscfono').val('');
        $('#txtpsccel').val('');
        $('#txtpscruc').val('');
        $('#cbopscModIng').val('');
        fnSelectReset('cbopscModIng');
    }
}


/* Ubigeo*/
function fnDepartamento(c) {
    // 156
    var arr = fnUbigeo(2, 2, c, 0);
    var n = arr.length;
    var str = "";
    str += '<option value="" selected>-- Seleccione -- </option>';
    for (i = 0; i < n; i++) {
        str += '<option value="' + arr[i].cod + '">' + arr[i].nombre + '</option>';
    }
    $('#cbopscdpto').html(str);
}

function fnProvincia() {
    $('#cbopscdprov').html("");
    $('#cbopscddist').html("");
    var c = $('#cbopscdpto').val();
    var arr = fnUbigeo(2, 3, c, 0);
    var n = arr.length;
    var str = "";
    str += '<option value="" selected>-- Seleccione -- </option>';
    for (i = 0; i < n; i++) {
        str += '<option value="' + arr[i].cod + '">' + arr[i].nombre + '</option>';
    }
    $('#cbopscdprov').html(str);
}

function fnDistrito() {
    $('#cbopscddist').html("");
    var c = $('#cbopscdprov').val();
    var arr = fnUbigeo(2, 4, c, 0);
    var n = arr.length;
    var str = "";
    str += '<option value="" selected>-- Seleccione -- </option>';
    for (i = 0; i < n; i++) {
        str += '<option value="' + arr[i].cod + '">' + arr[i].nombre + '</option>';
    }
    $('#cbopscddist').html(str);
}
/* Ubigeo*/

