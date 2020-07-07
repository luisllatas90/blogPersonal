<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ListarRendiciones.aspx.vb" Inherits="administrativo_Tesoreria_Rendiciones_AppRendiciones_ListarRendiciones" %>

<!DOCTYPE html>

<html lang="en">
     
    <head id="Head1" runat="server">
        <title></title>
        <meta charset="utf-8"/>
        <meta http-equiv="X-UA-Compatible" content="IE=edge"/> 
        <meta http-equiv='X-UA-Compatible' content='IE=8' />
        <meta http-equiv='X-UA-Compatible' content='IE=10' />   
        <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7, IE=EmulateIE9, IE=EDGE" /> 
        <meta http-equiv='cache-control' content='no-cache'/>
        <meta http-equiv='expires' content='0'/>
        <meta http-equiv='pragma' content='no-cache'/>
        <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=0"/> <!--320-->
       


        <link rel='stylesheet' href='../../../../academico/assets/css/bootstrap.min.css'/>
        <link rel='stylesheet' href='../../../../academico/assets/css/material.css'/>
        <link rel='stylesheet' href='../../../../academico/assets/css/style.css?x=1'/>
        <link rel='stylesheet' href='../../../../academico/assets/css/jquery.dataTables.min.css'/>

       
        <script src="../../../../academico/assets/js/jquery.js" type="text/javascript"></script>
 
 
        <script type="text/javascript" src='../../../../academico/assets/js/jquery.dataTables.min.js'></script>
        <script src="../../../../academico/assets/js/bootstrap.min.js" type="text/javascript"></script>

        <script src="../../../../academico/assets/js/noty/jquery.noty.js" type="text/javascript"></script>
        <script src="../../../../academico/assets/js/noty/layouts/top.js" type="text/javascript"></script>
        <script src="../../../../academico/assets/js/noty/layouts/default.js" type="text/javascript"></script>

        <script src="../../../../academico/assets/js/noty/jquery.desknoty.js" type="text/javascript"></script>
        <script src="../../../../academico/assets/js/noty/notifications-custom.js" type="text/javascript"></script>

        <script src="../../../../academico/assets/js/jquery.PrintArea.js" type="text/javascript"></script>


       
        <style type="text/css">
            
     /*       
        {
           
            position: absolute;  
            margin: auto;
            right: 0px;
            left: 5%;
            top: 5%;
            bottom: 0px;  
            width: 300px;  
        }
         
        {
          
            overflow: auto;
            position: absolute; 
            margin: auto;
            right: 0px;
            left: 5%;
            top: 2%;
            bottom: 0px;  
            width: 800px;  
        }*/
            
            td.details-control 
            {

                background: url('../../../../academico/assets/img/open.png') no-repeat center center;
                cursor: pointer;
            }
            tr.shown  td.details-control {
                background: url('../../../../academico/assets/img/details_close.png') no-repeat center center;
            } 
            .modal-title
            {
                margin:0;line-height:1.42857143;
                color:White;

            }
            .close{margin-top:-2px; color:White;}
            .modal-header
            {
                min-height:16.43px;
                padding:5px;
                border-bottom:1px solid #e5e5e5;
                background-color: #E33439;
                color:#fff;
            }
            .btnsearch {

                padding: 4px 8px;
                margin-bottom: 0;
                font-size: 14px;
                font-weight: 400;
                line-height: 1.42857143;
                text-align: center;
                white-space: nowrap;
                vertical-align: middle;
                -ms-touch-action: manipulation;

                cursor: pointer;
                -webkit-user-select: none;
                -moz-user-select: none;
                -ms-user-select: none;

                background-image: none;
                border: 1px solid transparent;

            }
            .form-group {
                margin-bottom: 1px;
            }
            .col-lg-1, 
            .col-lg-10, 
            .col-lg-11, 
            .col-lg-12, 
            .col-lg-2, 
            .col-lg-3, 
            .col-lg-4, 
            .col-lg-5, 
            .col-lg-6, 
            .col-lg-7, 
            .col-lg-8,
            .col-lg-9,
            .col-md-1, 
            .col-md-10, 
            .col-md-11, 
            .col-md-12, 
            .col-md-2, 
            .col-md-3, .col-md-4, 
            .col-md-5, .col-md-6, 
            .col-md-7, .col-md-8,
            .col-md-9, .col-sm-1, 
            .col-sm-10, .col-sm-11,
            .col-sm-12, .col-sm-2, 
            .col-sm-3, .col-sm-4, 
            .col-sm-5, .col-sm-6,
            .col-sm-7, .col-sm-8,
            .col-sm-9, .col-xs-1, 
            .col-xs-10, .col-xs-11,
            .col-xs-12, .col-xs-2, 
            .col-xs-3, .col-xs-4, 
            .col-xs-5, .col-xs-6, 
            .col-xs-7, 
            .col-xs-8, 
            .col-xs-9 {
                position: relative;
                min-height: 1px;
                padding-right: 1px;
                padding-left: 15px;
                top: 0px;
                left: 0px;
            }
            .control-label {
                font-weight: 400;
                letter-spacing: 0.25px;
                color: #707780;
                font-weight:bold;
                font-size: 13px;
                text-transform: capitalize;
            }

            .form-control {
                border-radius: 0px; 
                box-shadow: none;
                border-color: #d7dce5;
                height: 24px;
                font-weight: 300;
                /* line-height: 40px; */
                color:Black;
            }
            .modal.modal-content.modal-body 
            {
                overflow: hidden;
                position:fixed;
                color: #171819;
                font-family: 'Nunito',sans-serif;
                letter-spacing: 0.25px;
                line-height: 24px;
            }.
            .tdimprimir
            {
                font-size: 5px; 
                color: #ffffff; 
                font-family: 
                'Courier New';
                 height: 3px; 
                 background-color: darkolivegreen; 
                 font-variant: normal; 
                /* border-right: darkolivegreen 1px solid; 
                 border-top: darkolivegreen 1px solid; 
                 border-left: darkolivegreen 1px solid; */
                 width: 73px; 
                 /*border-bottom: darkolivegreen 1px solid; */
                 text-align: center;
            }
           .tablePrint {
            display: table;
         
            border-spacing: 4px;
           /* border-color:Black;*/
          /*  border-top-color: grey;*/
            border-right-color: Black;
           /* border-bottom-color: grey;-*/
            border-left-color: Black;
            }
          :-ms-input-placeholder.form-control {
            color: #aaaaaa;
            letter-spacing: 0.25px;
            font-weight: 300;
            font-size: 12px;
            
        }
        :-ms-input-placeholder.form-control {
            color: #aaaaaa;
            letter-spacing: 0.25px;
            font-weight: 300;
            font-size: 12px;
            line-height: 10px;
        }
        /*.modal-dialog {width:600px;}*/
        .thumbnail {margin-bottom:6px;}
        .ImportesRendicion
        {
            background-color:#2196f3;
            color:White;
            padding-bottom:10px;
            padding-top:10px;
            margin-bottom:10px;
            margin-left:3px;
            margin-right:3px;
            
        }
        .ImportesRendicion label.control-label
        {
            color:White;
            font-size:12px;
            font-weight:bold;
        }
        .noty_message
            
        {
            z-index:100;
        }
        </style>

        <script type="text/javascript">
            var OpInit = 0;
            var OpList = 0;
            $(document).ready(function() {
                $("#divGrupoCuenta").css("display", "none");
                $("#txtImporte").keydown(function(e) {
                    if ($.inArray(e.keyCode, [46, 8, 9, 27, 13, 110, 190]) !== -1 ||
                        (e.keyCode == 65 && e.ctrlKey === true) ||
                        (e.keyCode >= 35 && e.keyCode <= 39)) {
                        return;
                    }

                    if ((e.shiftKey || (e.keyCode < 48 || e.keyCode > 57)) && (e.keyCode < 96 || e.keyCode > 105)) {
                        e.preventDefault();
                    }
                });

                $("#listDocumento").change(function() {
                    if (this.value == 87) {
                        // alert(1);

                        $("#divGrupoCuenta").css("display", "block");
                        $("#txtSerie").css("display", "none");
                        $("#txtNumero").css("display", "none");
                        $("#txtImporte").val($("#Saldo").html().replace(",", ""));
                        $("#txtImporte").prop('readonly', true);
                        $("#txtObservacion").val("DEVOLUCIÓN DEPOSITO EN BANCO INTERBANK(700-3000186214).");
                        //$("divGrupoCuenta").addClass("hidden");
                    } else {
                        // alert(2);
                        $("#txtImporte").prop('readonly', false);
                        $("#txtImporte").val(0);
                        $("#txtSerie").css("display", "block");
                        $("#txtNumero").css("display", "block");
                        $("#divGrupoCuenta").css("display", "none");
                        //$("divGrupoCuenta").removeClass("hidden");
                    }
                    //alert(this.value);
                });
                $("#txtNroOperacion").keyup(function(e) {
                    $("#txtNumero").val($("#txtNroOperacion").val());
                    $("#txtObservacion").val("DEVOLUCIÓN DEPOSITO EN BANCO INTERBANK(700-3000186214). Nro. Operacion  : " + $("#txtNroOperacion").val());
                });

                SoloNumeros("txtNroRuc");
                SoloNumeros("txtNumero");
                $("#txtSerie").keyup(function(e) {

                    var Valor = $(this).val();
                    var i = 0;
                    for (i = 0; i < Valor.length; i++) {
                        //console.log(Valor.charAt(i));
                    }
                    //  console.log(Valor);
                });
                var oTable = $('#tablaRendiciones').DataTable({
                    "bPaginate": false,
                    "bFilter": true,
                    "bLengthChange": false,
                    "bInfo": false
                });

                var nCloneTh = document.createElement('th');
                var nCloneTd = document.createElement('td');
                nCloneTd.className = "details-control";

                $('#tablaRendiciones thead tr').each(function() {
                    this.insertBefore(nCloneTh, this.childNodes[0]);
                });
                $('#tablaRendiciones tbody tr').each(function() {
                    this.insertBefore(nCloneTd.cloneNode(true), this.childNodes[0]);
                });
                $('#tablaRendiciones  thead  tr th:eq(0)').html('&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;');
                $('#tablaRendiciones tbody td.details-control').on('click', function() {
                    var nTr = $(this).parents('tr')[0];
                    var cup = nTr.id;
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
                            url: "DataJson/ListarRendicion.aspx",
                            data: { "Id": cup, "Funcion": "RendDetalle" },
                            dataType: "json",
                            cache: false,
                            success: function(data) {
                                sOut = '<table cellpadding="0" cellspacing="0" border="1" style="padding-left:50px;width:95%;font-size:12px;">';
                                sOut += '<tr>';
                                sOut += '<th>' + 'Rubro' + '</th>';
                                sOut += '<th class="text-center">' + 'Nro.Rendición' + '</th>';
                                sOut += '<th class="text-right">' + 'Importe ' + '</th>';
                                sOut += '<th>' + 'Centro de costos ' + '</th>';
                                sOut += '<th>' + ' Estado ' + '</th>';
                                sOut += '<th>' + 'Observación ' + '</th>';
                                /*sOut += '<th>' + '' + '</th>';*/
                                sOut += '<th>' + '' + '</th>';
                                sOut += '</tr>';
                                jQuery.each(data, function(i, val) {
                                    var Finalizar = '';
                                    if (val.Finalizarend == '1') {
                                        Finalizar = '<a href="#" onclick="Finalizar(' + val.CodigoRend + ')" class="btn btn-red btn-icon-red btn-icon-block btn-icon-blockleft"><i  class="ion ion-checkmark"></i><span>Finalizar</span></a></div>';
                                    }
                                    var docente = '';
                                    sOut += '<tr>';
                                    sOut += '<td>' + val.Rubro + ' ' + '</td>';
                                    sOut += '<td class="text-center">' + val.NumeracionAnualRend + ' ' + '</td>';
                                    sOut += '<td class="text-right">' + number_format(val.Importe, 2) + ' ' + '</td>';
                                    sOut += '<td>' + val.Centrocostos + ' ' + '</td>';
                                    sOut += '<td>' + val.EstadoRendicion + ' ' + '</td>';
                                    sOut += '<td>' + val.Observacion + '</td>';
                                    /*sOut += '<td>' + '<a href="#" id=' + val.CodigoRend + ' onclick="Rendir(' + val.CodigoRend + ')"  class="btn btn-green" data-toggle="modal" data-target="#exampleModal" data-whatever="@getbootstrap"><i  class="ion-plus"><span></span></i></a>' + '</td>';*/
                                    sOut += '<td>';
                                    sOut += '<div class="btn-group">';
                                    sOut += '<a href="#" id=' + val.CodigoRend + ' onclick=Rendir(' + val.CodigoRend + ',' + val.NumeracionAnualRend + ',"' + val.FechaRend + '") class="btn btn-green btn-icon-green btn-icon-block btn-icon-blockleft" data-toggle="modal" data-target="#exampleModal" data-whatever="@getbootstrap"><i  class="ion ion-plus"></i><span>Rendir</span></a>';
                                    sOut += '<a href="#" onclick=imprSelec("Imprimir",' + val.CodigoRend + ')  class="btn btn-primary btn-icon-primary btn-icon-block btn-icon-blockleft" data-toggle="modal" data-target="#ModalPrint" data-whatever="@getbootstrap"><i  class="ion  ion-ios-printer-outline"></i><span>Imprimir</span></a>';
                                    sOut += Finalizar;
                                    sOut += '</tr>';
                                });
                                sOut += '</table>';
                                oTable.fnOpen(nTr, sOut, 'details');

                            },
                            error: function(result) {
                                sOut = '';
                                oTable.fnOpen(nTr, sOut, 'details');
                                // location.reload();
                            }
                        });
                    }
                });
                var OpForm = 0;
                $('#btnAgregar').on('click', function(ev) {
                    var Saldo = $("#Saldo").html().replace(",", "");
                    //console.log(Saldo);
                    if (Saldo <= 0) {
                        fnMensaje('warning', "No existe Saldo para esta Operación");
                        //$("#divMessage").html("No existe Saldo para esta Operación");
                        //flag = false;
                        //return;
                    } else {
                        OpList = 0;
                        if (OpForm == 0) {
                            OpForm = 1;
                            OpInit = 1;
                            $("#temp1").html(null);
                            $("#temp1").html($("#DivListado"));
                            $("#DivListado").css("display", "none");
                            $("#DivAgregar").css("display", "block");
                            $("#Content").html($("#DivAgregar"));
                        }
                    }
                    ///ev.preventDefault();
                });

                $('#btnCancelar').on('click', function(ev) {

                    OpForm = 0;
                    if (OpList == 0) {
                        OpList = 1;
                        OpInit = 1;
                        $("#temp2").html(null)
                        $("#temp2").html($("#DivAgregar"));
                        $("#DivAgregar").css("display", "none");
                        $("#DivListado").css("display", "block");
                        $("#Content").html($("#DivListado"));
                    }
                    Limpiar();
                    DetalleRencionDocumentos($("#NroRendicion").val(), 'TDocs');
                    /// ev.preventDefault();
                });
                $('#btnSalir').on('click', function(ev) {
                    //   alert("oks" + OpList);
                    OpForm = 0;
                    if (OpList == 0) {
                        OpList = 1;
                        $("#temp2").html(null)
                        $("#temp2").html($("#DivAgregar"));
                        $("#DivAgregar").css("display", "none");
                        $("#DivListado").css("display", "block");
                        $("#Content").html($("#DivListado"));
                        $("#divMessage").css("display", "none");
                        $("#divMessage").html('');
                    }
                    /// ev.preventDefault();
                });
            });
            function Limpiar() {




                TotalRendicion($("#NroRendicion").val(),1);
              //  DetalleRencionDocumentos($("#NroRendicion").val());
               $("#txtNroRuc").val("");
               $("#txtRazonSocial").val("");
               $("#txtDireccion").val("");
               $("#txtSerie").val("");
               $("#txtNumero").val("");
               $("#txtImporte").val("");
               $("#txtObservacion").val("");
               $("#txtFecha").val("dd/mm/yyyy");
               $("#fileData").val("");
               $("#txtNroOperacion").val("")
               $("#divMessage").css("display", "block");
               $("#txtNroRuc").focus();
               
           }

           function ValidaImportes() {
               $("#divMessage").css("display", "block");

//               file = $("#fileData").val();
//               alert($("#fileData")[0].files[0].path); //B

//              // file = $("#fileData").files[0];
//               alert($("#fileData")[0].files[0]); //C
//               console.log($("#fileData"));
//              // 
//               return;
//              
               var varfecha = $("#txtFecha").val();
              // console.log(varfecha);
               var fecha = '';
               if (varfecha.indexOf("-") != -1) {
                   var dateSplit = varfecha.split('-');
                   fecha = dateSplit[2] + '/' + dateSplit[1] + '/' + dateSplit[0];
                 //  $("#divMessage").html(fecha);
               } else {
                    fecha = $("#txtFecha").val();
               }
               var flag = true;
               var ImpEntregado = $("#ImpEntregado").html().replace(",","");
               var ImpRendido = $("#ImpRendido").html().replace(",", "");
               var ImpDevuelto = $("#ImpDevuelto").html().replace(",", "");
               var Saldo = $("#Saldo").html().replace(",", "");
               var Importe = $("#txtImporte").val().replace(",", "");

               var ImpRes = (parseFloat($("#txtImporte").val().replace(",", "")) + parseFloat(ImpRendido)) + parseFloat(ImpDevuelto);
               //console.log("ImpRes :" + ImpRes);
              // alert(ImpRes)
               $('#divMessage').addClass('alert alert-danger alert-dismissable');
               if (Saldo <= 0) {
                   $("#divMessage").html("No existe Saldo para esta Operación");
                   fnMensaje('warning', "No existe Saldo para esta Operación");
                   flag = false;
                   return;
               } else 
                {
                    if (Importe == 0) {
                        $("#divMessage").html("Ingrese el importe a Rendir");
                        fnMensaje('warning', "Ingrese el importe a Rendir");
                        flag = false;
                        return;
                    }
                    else {
                        if ($("#txtSerie").val() == "" && $("#txtNumero").val() == '') {
                            fnMensaje('warning', "Ingrese serie y número del Comprobante");
                            $("#divMessage").html("Ingrese serie y número del Comprobante");
                            flag = false;
                            return;
                        }
                        else {
                            if ($("#txtNroRuc").val() == '') {
                                fnMensaje('warning', "Ingrese el Número de Ruc");
                                $("#divMessage").html("Ingrese el Número de Ruc");
                                flag = false;
                                return;
                            } else {
                            if ($("#txtRazonSocial").val() == '') {
                                    fnMensaje('warning', "Ingrese la Empresa/Institución");
                                    $("#divMessage").html("Ingrese la Empresa/Institución");
                                    flag = false;
                                    return;
                                } else {
                                if (ImpRes > parseFloat(ImpEntregado)) {
                                    flag = false;
                                    fnMensaje('warning', "El Importe a rendir no puede ser mayor que el importe entregado");
                                    $("#divMessage").html("El Importe a rendir no puede ser mayor que el importe entregado");
                                    return;
                                }
                                else {
                                    if (validaFecha(fecha) == false) 
                                    {
                                        flag = false;
                                        fnMensaje('warning', "Ingrese una Fecha Valida en el Formato (dd/MM/yyyy)");
                                            $("#divMessage").html("Ingrese una Fecha Valida en el Formato (dd/MM/yyyy)" );
                                            return;
                                        }
                                    }
                                }
                                
                            }
                            
                        }
                    }
               }
               
               return flag;
           }
           function Rendir(Nrorend, NumeracionAnualRend,Fecha) {
                //   alert(OpInit)
               $("#myModalLabel").html("Registro de Documento a Rendir(" + NumeracionAnualRend + ")");
              // alert(Fecha);
               $("#NroRendAnual").val(NumeracionAnualRend);
               $("#NroRendicion").val(Nrorend);
               $("#FchaRendicion").val(Fecha);
                TotalRendicion(Nrorend,1);
               
                if (OpInit == 1) {
                    if (OpList == 0) {
                        OpList = 1;
                        $("#DivListado").css("display", "block");
                        $("#Content").html($("#DivListado"));
                    }
                } else {
                    $("#DivListado").css("display", "block");
                    $("#Content").html($("#DivListado"));

                }
                DetalleRencionDocumentos(Nrorend, 'TDocs');
                //  ev.preventDefault();
            }

            function Eliminar(codigoDren) {

                if (confirm("¿Desea eliminar el registro?")) {
                    $.ajax({
                        type: "POST",
                        //contentType: "application/json; charset=utf-8",
                        url: "DataJson/ListarRendicion.aspx",
                        data: { "Funcion": "Eliminar", "Id": codigoDren },
                        dataType: "json",
                        cache: false,
                        success: function(data) {
                            TotalRendicion($("#NroRendicion").val(), 1);
                            DetalleRencionDocumentos($("#NroRendicion").val(), 'TDocs');
                            jQuery.each(data, function(i, val) {
                                fnMensaje('warning', val.Message);
                            });

                        },
                        error: function(result) {

                        }
                    });                    
                }
                
            }
            function DetalleRencionDocumentos(NroRend, tabla) {

                $.ajax({
                    type: "GET",
                    contentType: "application/json; charset=utf-8",
                    url: "DataJson/ListarRendicion.aspx",
                    data: { "Id": NroRend, "Funcion": "ListDetalle" },
                    dataType: "json",
                    cache: false,
                    success: function(data) {
                        var row = 1;
                        //  alert(data.length);
                        var sOut = '';
                        $("#" + tabla).html("");
                        jQuery.each(data, function(i, val) {
                            var docente = '';
                            // alert(NroRend);
                            var Eliminar = '';
                            var Adjunto = '<a href="#" onclick="VistaPrevia(\'' + val.Codigo + '\',event)"><span>Adjunto</span></a>';
                            if (val.Finaliza == 'P') {
                                Eliminar = '<a href="#" onclick="Eliminar(\'' + val.Codigo + '\')"><span>Eliminar</span></a>';
                            }
                            if (tabla == 'TDocs') {
                                sOut += '<tr>';
                                sOut += '<td>' + val.TipoDoc + ' ' + '</td>';
                                sOut += '<td>' + val.SerieNumero + ' ' + '</td>';
                                sOut += '<td>' + val.Fecha + ' ' + '</td>';
                                sOut += '<td>' + val.Institucion + ' ' + '</td>';
                                sOut += '<td>' + number_format(val.Importe, 2) + ' ' + '</td>';
                                sOut += '<td>' + val.Descripcion + ' ' + '</td>';
                                sOut += '<td>' + Eliminar + '</td>';
                                sOut += '<td>' + Adjunto + '</td>';
                                sOut += '</tr>';
                            } else {
                                sOut += '<tr>';
                                sOut += '<td style="border-bottom: none;border-top: none;margin-left: 7px;">' + row + ' ' + '</td>';
                                sOut += '<td style="border-bottom: none;border-top: none;margin-left: 7px;font-size: 9px;">' + val.TipoDoc + ' ' + '</td>';
                                sOut += '<td style="border-bottom: none;border-top: none;margin-left: 7px;font-size: 10px;padding-right:6px;" class="text-right">' + val.SerieNumero + ' ' + '</td>';
                                sOut += '<td style="border-bottom: none;border-top: none;margin-left: 7px;font-size: 10px;">' + val.Fecha + ' ' + '</td>';
                                sOut += '<td style="border-bottom: none;border-top: none;margin-left: 7px;font-size: 10px;">' + val.Institucion + ' ' + '</td>';
                                sOut += '<td style="border-bottom: none;border-top: none;margin-left: 7px; padding-right:2px;"  class="text-right">' + number_format(val.Importe, 2) + ' ' + '</td>';
                                sOut += '<td style="border-bottom: none;border-top: none;"><div class="row"><div style=" margin-left: 7px;margin-top: 7px;font-size: 10px;" class="col-lg-11 col-md-11 col-sm-11">' + val.Descripcion + ' ' + '</div></div></td>';
                                sOut += '</tr>';
                            }

                            row++;
                        });

                        $("#" + tabla).html(sOut);
                        ///  oTable.fnOpen(nTr, sOut, 'details');

                    },
                    error: function(result) {
                        sOut = '';
                        // oTable.fnOpen(nTr, sOut, 'details');
                        // location.reload();
                    }
                });
            }
            function TotalRendicion(NroRend,Op) {

                $.ajax({
                    type: "GET",
                    contentType: "application/json; charset=utf-8",
                    url: "DataJson/ListarRendicion.aspx",
                    data: { "Id": NroRend, "Funcion": "RendImportes" },
                    dataType: "json",
                    cache: false,
                    success: function(data) {
                        jQuery.each(data, function(i, val) {
                            if (Op == 1) {
                                $("#ImpEntregado").html(number_format(val.Importe, 2));
                                $("#ImpRendido").html(number_format(val.MontoRendido, 2));
                                $("#ImpDevuelto").html(number_format(val.MontoDevuelto, 2));
                                //$("#Saldo").html(number_format(val.SaldoRendir, 2));
                                $("#Saldo").html(val.SaldoRendir.toFixed(2));
                            }
                            if (Op == 2) {
                                $("#ImpEntregadoPr").html(number_format(val.Importe, 2));
                                $("#ImpRendidoPr").html(number_format(val.MontoRendido, 2));
                                $("#ImpDevueltoPr").html(number_format(val.MontoDevuelto, 2));
                                $("#ImpSaldoPr").html(number_format(val.SaldoRendir, 2));
                                $("#lblNombreFirma").html(val.Cliente);
                                $("#lblCliente").html(val.Cliente);
                                $("#lblTipDoc").html(val.TipDoc);
                                $("#lblSerieNumero").html(val.SerieNumero);
                                $("#lblFecha").html(val.FechaEgr);
                                $("#lblRubro").html(val.Rubro);
                                $("#lblMoneda").html(val.Moneda);
                                $("#lblUsuario").html(val.Usuario);
                                $("#lblNumeroRendAnual").html('<h3>HOJA DE RENDICIÓN DE CUENTAS Nº. ' + val.NumeracionAnualRend + '</h3>');
                                $("#lblNumeroPedido").html( "Nro.Pedido : " + val.CodigoPed);
                                
                            }
                        });


                    },
                    error: function(result) {


                      
                    }
                });
             }
            
            function ConsultarRuc() {
                $.ajax({
                    type: "GET",
                    contentType: "application/json; charset=utf-8",
                    url: "DataJson/ConsultaSunat.aspx",
                    data: { "NroRuc": $("#txtNroRuc").val() },
                    dataType: "json",
                    success: function(data) {
                    //console.log(data);
                        var filas = data.length;
                        $("#txtRazonSocial").val(data.NombreRazonSocial);
                        $("#txtDireccion").val(data.Direccion);
                        $("#Departamento").val(data.Departamento);
                        $("#Provincia").val(data.Provincia);
                        $("#Distrito").val(data.Distrito);
                        $("#OpPersona").val(data.Codigo);
                        
                    },
                    error: function(result) {


                    }
                });
            }
            function Grabar() {
             // SubirArchivo(7888);
             // return;
                if (ValidaImportes() == true) {                     
                        var formData = $('#frmRegistro').serializeArray();
                        $.ajax({
                            type: "POST",
                            dataType: "html",
                            url: "DataJson/ListarRendicion.aspx",
                            data: formData,
                            dataType: "json",
                            success: function(data) {
                                var flag = false;
                                jQuery.each(data, function(i, val) {
                                    if (val.Status == "OK") {
                                        SubirArchivo(val.Code);
                                        flag = true;
                                        $("#divMessage").css("display", "block");
                                        fnMensaje('warning', 'Registro Grabado');
                                        $('#divMessage').addClass('alert alert-success alert-dismissable');
                                        $("#divMessage").html("Registro Grabado");
                                        Limpiar();
                                    }
                                    if (val.Status == "ERROR") {
                                        $("#divMessage").css("display", "block");
                                        if (val.Code == "0") {
                                            fnMensaje('warning', val.Message + " Click en el Boton Consultar Para validar los datos ingresados");
                                            $("#divMessage").html(val.Message + " Click en el Boton Consultar Para validar los datos ingresados");
                                        }
                                        if (val.Code == "504") {
                                            $("#continuemodal").modal("show");
                                            $("#DivSessionUser").html(val.Message);
                                        }

                                        $('#divMessage').addClass('alert alert-success alert-dismissable');
                                    }

                                });


                            },
                            error: function(result) {
                                $("#divMessage").css("display", "block");
                                $("#divMessage").html(result);
                            }
                        });
                    }                
            }
            function Finalizar(CodigoRend) {
                /// alert($("#NroRendicion").val());
                $.ajax({
                    type: "POST",
                    //contentType: "application/json; charset=utf-8",
                    url: "DataJson/ListarRendicion.aspx",
                    data: { "Funcion": "Finalizar", "Id": CodigoRend },
                    dataType: "json",
                    success: function(data) {
                     
                        jQuery.each(data, function(i, val) {
                            fnMensaje('warning', val.Message);
                         });
                       
                    },
                    error: function(result) {
                      
                    }
                });

            }

            function OpenWindowWithPost(url, windowoption, name, params) {
                var form = document.createElement("form");
                form.setAttribute("method", "post");
                form.setAttribute("action", url);
                form.setAttribute("target", name);

                for (var i in params) {
                    if (params.hasOwnProperty(i)) {
                        var input = document.createElement('input');
                        input.type = 'hidden';
                        input.name = i;
                        input.value = params[i];
                        form.appendChild(input);
                    }
                }

                document.body.appendChild(form);

                //note I am using a post.htm page since I did not want to make double request to the page 
                //it might have some Page_Load call which might screw things up.
                window.open(url, name, windowoption);

                form.submit();

                document.body.removeChild(form);
            }

            function NewFile() {
                var param = { 'uid': '1234' };
                OpenWindowWithPost("DataJson/DescargarArchivo.aspx", "", "NewFile", param);
            }
            function DescargarArchivo(codigoDren) {
                var flag = false;
                var data = new FormData();
                    // console.log($(codigoDren).attr('id'));
                    // alert(codigoDren);

                    data.append("Funcion", "DescargarArchivo");
                    data.append("CodigoDren", $(codigoDren).attr('id'));
                    // alert();
                    $.ajax({
                        type: "POST",
                        url: "DataJson/ListarRendicion.aspx",
                        data: data,
                        dataType: "json",
                        cache: false,
                        contentType: false,
                        processData: false,
                        success: function(data) {
                            //console.log(data);
                            jQuery.each(data, function(i, val) {
                                // alert(val.Status);
                                if (val.Status == "OK") {
                                    var file = 'data:application/octet-stream;base64,' + val.File;
                                    downloadWithName(file, val.Nombre);
                                    if (navigator.userAgent.indexOf("NET") > -1) {


                                        var param = { 'Id': $(codigoDren).attr('id') };
                                        // OpenWindowWithPost("DataJson/DescargarArchivo.aspx", "", "NewFile", param);
                                        window.open("DataJson/DescargarArchivo.aspx?Id=" + $(codigoDren).attr('id'), 'ta', "");

                                    } else {

                                        if (val.Extencion == '.png' || val.Extencion == '.jpg') {
                                            $("#imgDocumento").attr('src', 'data:image/png;base64,' + val.File);
                                        }
                                    }


                                    // $("#continuemodal").modal("show");
                                    //  $("#DivSessionUser").html(val.Message);
                                } else {
                                    // alert("Oks");
                                    $("#continuemodal").modal("show");
                                    $("#DivSessionUser").html(val.Message);
                                    // fnMensaje('warning', val.Message);
                                }

                            });

                            // $("#divMessage").html("Suviendo archivo");
                            //  Limpiar();
                        },
                        error: function(result) {
                            $("#divMessage").html(result);
                        }
                    });

                }
                function redireccionar() {
                //    alert(window.location.host + "/campusvirtual");
                    window.top.location.href = "http://" + window.location.host + "/campusvirtual/personal";
                }
                function getAbsolutePath() {
                    var loc = window.location;
                    var pathName = loc.pathname.substring(0, loc.pathname.lastIndexOf('/') + 1);
                    return loc.href.substring(0, loc.href.length - ((loc.pathname + loc.search + loc.hash).length - pathName.length));
                }
            function downloadWithName(uri, name) {
                var link = document.createElement("a");
                link.download = name;
                link.href = uri;
                link.click();
               // alert(link);
            }
            function Detalle(id) {
                $("#ModelDetalle").modal()
                $("#divDetallerend").html(id.attr('id')); 
             //   alert(id.attr('id'));                 
            }
            function number_format(amount, decimals) {

                amount += ''; // por si pasan un numero en vez de un string
                amount = parseFloat(amount.replace(/[^0-9\.]/g, '')); // elimino cualquier cosa que no sea numero o punto

                decimals = decimals || 0; // por si la variable no fue fue pasada

                // si no es un numero o es igual a cero retorno el mismo cero
                if (isNaN(amount) || amount === 0)
                    return parseFloat(0).toFixed(decimals);

                // si es mayor o menor que cero retorno el valor formateado como numero
                amount = '' + amount.toFixed(decimals);

                var amount_parts = amount.split('.'),regexp = /(\d+)(\d{3})/;

                while (regexp.test(amount_parts[0]))
                    amount_parts[0] = amount_parts[0].replace(regexp, '$1' + ',' + '$2');

                return amount_parts.join('.');
            }
            function imprSelec(nombre, Nrorend) {
                if (Nrorend!=0)
                {                    
                    TotalRendicion(Nrorend, 2);
                    DetalleRencionDocumentos(Nrorend, 'TbodyPrint');
                    document.title = 'Rendiciones';
               }else{               
                $("div#" + nombre + "").printArea();
                }
               /*
                var ficha = document.getElementById(nombre);
                var ventimp = window.open(' ', 'popimpr');
                ventimp.document.write(ficha.innerHTML);
                ventimp.document.close();
                ventimp.print();
                ventimp.close();
                */
            }
            function SubirArchivo(codigoDren) {
                var flag = false;
                var data = new FormData();

                var files = $("#fileData").get(0).files;

                // Add the uploaded image content to the form data collection
                if (files.length > 0) {
                    data.append("UploadedImage", files[0]);
                }
                data.append("Funcion", "UpFile");
                data.append("CodigoDren", codigoDren);
                data.append("CodigoRend", $("#NroRendAnual").val());
                data.append("Fecha", $("#FchaRendicion").val());
               // alert();
                $.ajax({
                    type: "POST",
                    url: "DataJson/ListarRendicion.aspx",
                    data: data,
                    dataType: "json",
                    cache: false,
                    contentType: false,
                    processData: false,
                    success: function(data) {
                        flag = true;
                        //console.log(data);
                        fnMensaje('warning', 'Subiendo Archivo');
                        $('#divMessage').addClass('alert alert-success alert-dismissable');
                        $fileupload = $('#fileData');
                        $fileupload.replaceWith($fileupload.clone(true));
                        // $("#divMessage").html("Suviendo archivo");
                        //  Limpiar();
                    },
                    error: function(result) {
                        //console.log(result);
                        $("#divMessage").html(result);

                        flag = false;
                    }
                });
                return flag;
            }
            function VistaPrevia(CodigoDren, event) {
                $("#imgDocumento").attr("src", "");
                $("#DivArchivos").html("");
                $('#GaleriaImages').modal({ show: true });
              // alert(CodigoDren);
                $.ajax({
                    type: "POST",
                    url: "DataJson/ListarRendicion.aspx",
                    data: { "Funcion": "ArchivoCompartido", "Id": CodigoDren },
                    dataType: "json",
                    cache: false,
                    success: function(data) {
                        //console.log(data);
                        if (data.length > 0) {
                            jQuery.each(data, function(i, val) {
                                //DivArchivos
                                $("#DivArchivos").html("<div><input type='hidden' id='" + val.IdArchivo + "' name='" + val.IdArchivo + "' /><a href='#' onclick='DescargarArchivo(this)'  id='" + val.IdArchivo + "' href=''>" + val.Nombre + '</a></div>')
                                //  $("#imgDocumento").attr("src", "../Archivosderendicion/" + val.Imagen + "");
                            });
                            //  event.preventDefault();
                        } else {

                        $("#DivArchivos").html("<h5>No se adjuntó ningun archivo del comprobante a este registro</h5>")
                        }
                    },
                    error: function(result) {

                    }
                });


               // event.preventDefault();
            }
           
            function Fixed(e) {
              
               // document.body.scroll = "yes";
                $('body').css("overflow", "auto");
                  e.preventDefault();
                ///alert('pls');
            }
            function validaFecha(fecha) {
                var dtCh = "/";
                var minYear = 1900;
                var maxYear = 2100;
                function isInteger(s) {
                    var i;
                    for (i = 0; i < s.length; i++) {
                        var c = s.charAt(i);
                        if (((c < "0") || (c > "9"))) return false;
                    }
                    return true;
                }
                function stripCharsInBag(s, bag) {
                    var i;
                    var returnString = "";
                    for (i = 0; i < s.length; i++) {
                        var c = s.charAt(i);
                        if (bag.indexOf(c) == -1) returnString += c;
                    }
                    return returnString;
                }
                function daysInFebruary(year) {
                    return (((year % 4 == 0) && ((!(year % 100 == 0)) || (year % 400 == 0))) ? 29 : 28);
                }
                function DaysArray(n) {
                    for (var i = 1; i <= n; i++) {
                        this[i] = 31
                        if (i == 4 || i == 6 || i == 9 || i == 11) { this[i] = 30 }
                        if (i == 2) { this[i] = 29 }
                    }
                    return this
                }
                function isDate(dtStr) {
                    var daysInMonth = DaysArray(12)
                    var pos1 = dtStr.indexOf(dtCh)
                    var pos2 = dtStr.indexOf(dtCh, pos1 + 1)
                    var strDay = dtStr.substring(0, pos1)
                    var strMonth = dtStr.substring(pos1 + 1, pos2)
                    var strYear = dtStr.substring(pos2 + 1)
                    strYr = strYear
                    if (strDay.charAt(0) == "0" && strDay.length > 1) strDay = strDay.substring(1)
                    if (strMonth.charAt(0) == "0" && strMonth.length > 1) strMonth = strMonth.substring(1)
                    for (var i = 1; i <= 3; i++) {
                        if (strYr.charAt(0) == "0" && strYr.length > 1) strYr = strYr.substring(1)
                    }
                    month = parseInt(strMonth)
                    day = parseInt(strDay)
                    year = parseInt(strYr)
                    if (pos1 == -1 || pos2 == -1) {
                        return false
                    }
                    if (strMonth.length < 1 || month < 1 || month > 12) {
                        return false
                    }
                    if (strDay.length < 1 || day < 1 || day > 31 || (month == 2 && day > daysInFebruary(year)) || day > daysInMonth[month]) {
                        return false
                    }
                    if (strYear.length != 4 || year == 0 || year < minYear || year > maxYear) {
                        return false
                    }
                    if (dtStr.indexOf(dtCh, pos2 + 1) != -1 || isInteger(stripCharsInBag(dtStr, dtCh)) == false) {
                        return false
                    }
                    return true
                }
                if (isDate(fecha)) {
                    return true;
                } else {
                    return false;
                }
            }
            function SoloNumeros(txt) {

                $("#"+txt).keydown(function(e) {
                    if ($.inArray(e.keyCode, [8, 9, 46]) !== -1) {
                        return;
                    }

                    if ((e.shiftKey || (e.keyCode < 48 || e.keyCode > 57)) && (e.keyCode < 96 || e.keyCode > 105)) {
                        e.preventDefault();
                    }
                });
            }
        </script>
    </head>
    <body>
       
            <div class="col-md-12 nopad-right">
                <!-- panel -->
                <div class="panel panel-piluku">
                    <div class="panel-heading">
                        <h3 class="panel-title">
                            Listado de rendiciones  
                            <span class="panel-options">
                                <a href="#" class="panel-refresh">
                                    <i class="icon ti-reload"></i> 
                                </a>
                                <a href="#" class="panel-minimize">
                                    <i class="icon ti-angle-up"></i> 
                                </a>
                                <a href="#" class="panel-close">
                                    <i class="icon ti-close"></i> 
                                </a>
                            </span>
                        </h3>
                    </div>
                    <div class="panel-body">
                        <div class="col-md-12">            
                            <div class="row">
                                <div class="col-md-6" style="float:left">

                                    <ul>
                                        <li class="list-unstyled"><i class="icon ion-checkmark text-primary"></i> &nbsp;
                                        <b><label class="">CODIGO:&nbsp;</label> </b><label runat="server" id="lblCodigo" name="lblCodigo"></label>. &nbsp;&nbsp; <i class="icon ion-checkmark text-primary"></i> &nbsp;<b><label>DNI: &nbsp;</label> </b><label runat="server" id="lblDni" name="lblDni"></label>.</li>
                                        <li class="list-unstyled"><i class="icon ion-checkmark text-primary"></i> &nbsp;<b><label>COLABORADOR:&nbsp; </label> </b><label runat="server" id="lblColaborador" name="Codigo"></label></li>
                                    </ul>

                                </div>		
                            </div>

                        </div> 

                        <div class="row">
                        <form  method="post" >
                      
                            <div class="col-md-4 col-sm-4 col-xs-4"  style="float:left">
                                <div class="form-group">
                                    <label class="col-sm-5 col-xs-5 control-label"><b>Estado de la Rendición :</b></label>
                                    <div class="col-sm-6 col-xs-6"> 
                                        <select class="name_search  form-control valid" name="EstRend" id="EstRend" data-validation="required" data-validation-error-msg="Please make a choice">
                                          
                                            <option value="P" >Pendientes </option>
                                            <option value="F"> Finalizadas </option>                					        
                                        </select>
                                    </div>            
                                </div>  
                            </div>
                            <div class="col-md-1 col-sm-2 col-xs-2" >
                                <div class="form-group">                           
                                    <div class="col-sm-3"><input type="submit" name="btnConsultar" id="btnConsultar"  Text="Consultar" class="btn btn-primary" value="Consultar"/></div>  
                                </div>  
                            </div>
                         </form>
                        </div> 						 
                        <!-- /row -->
                        <div class="row">
                            <div class="col-md-12">
                                <div id="PnlList">
                                    <table class="display dataTable cell-border" id="tablaRendiciones">
                                        <thead>
                                            <tr role="row">		
                                                <th style="width:8%">Fecha</th>
                                                <th style="width:8%">T.Documento</th>
                                                <th style="width:8%">Número</th>
                                                <th style="width:8%">Moneda</th>
                                                <th class="text-right" style="width:10%">Importe</th>											
                                                <th style="width:8%">Estado</th>
                                                <th style="width:10%">Usuario</th>
                                                <th style="width:40%">Observación</th>
                                               
                                                <th style="width:40%">Detalle</th>
                                            </tr>
                                        </thead>
                                        <tbody id="tbRendicion" runat="server">
                                        </tbody>									 
                                    </table>	
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- /panel -->
            </div>
            <!-- col-md-6 -->
            <div class="modal fade modal-md"  style=" z-index:0; overflow:scroll" id="exampleModal"  role="dialog" aria-labelledby="exampleModalLabel" data-backdrop="static" data-keyboard="false">
                <div class="modal-dialog modal-lg" role="document">
                    <div class="modal-content" style="max-width: 800px">
                        <!--Header-->
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close" id="btnSalir" name="btnSalir">
                                <span aria-hidden="true">&times;</span>
                            </button>
                            <h4 class="modal-title w-100 " id="myModalLabel">Registro de Documento a Rendir</h4>
                            
                        </div>
                        <!--Body-->
                        <div class="modal-body">
                            <div class="row ImportesRendicion">
                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                    <div class ="col-lg-3 col-md-3 col-sm-4 col-xs-4">
                                        <label class="col-sm-10 col-md-8 col-sm-9 col-xs-9 control-label"><b>Imp. Entregado :</b></label>
                                        <label class="col-sm-2  col-md-2 col-sm-2 col-xs-2 control-label" for="ImpEntregado"  id="ImpEntregado" name="ImpEntregado"><b>8,500.00 &nbsp;&nbsp;</b></label>
                                         <%--<h4 class = "col-sm-2  col-md-2 col-sm-2 col-xs-2" id="ImpEntregado" name="ImpEntregado"></h4>--%>
                                    </div>
                                    <div class ="col-lg-3 col-md-3 col-sm-3 col-xs-3">
                                        <label class="col-sm-8 col-xs-8 control-label"><b>Imp. Rendido:</b></label>
                                       
                                        <label class="col-sm-4 col-xs-4 control-label text-left" id="ImpRendido"><b>0.00</b></label>
                                    </div>
                                    <div class ="col-lg-3 col-md-3 col-sm-3 col-xs-3">
                                        <label class="col-sm-8 col-xs-8 control-label"><b>Imp. Devuelto:</b></label>
                                        <label class="col-sm-3 col-xs-3 control-label text-left" id="ImpDevuelto"><b>0.00</b></label>
                                    </div>
                                    <div class ="col-lg-3 col-md-3 col-sm-2 col-xs-2">
                                        <label class="col-sm-5 col-xs-5 control-label"><b>Saldo:</b></label>
                                        <label class="col-sm-3 col-xs-3 control-label text-left" id="Saldo" ><b>8,500.00</b></label>
                                    </div>
                                </div>
                            </div> 

                            <div class="row">
                                <div id="Content" class="col-lg-12 col-xs-12 col-md-12 col-sm-12">

                                </div>
                            </div>

                        </div>
                        <!--Footer-->
                        <%--<div class="modal-footer">
                            <button type="button" class="btn btn-default" data-dismiss="modal">Salir</button>
                        </div>--%>
                    </div>
                </div>
            </div> 
            <div id="DivListado" class="row" style="display:none">  
                <div class="row">
                    <div class ="col-lg-1 col-md-1 col-sm-1 col-xs-1">
                    </div>
                    <div class ="col-lg-1 col-xs-1 col-md-1 col-sm-1" >
                        <a id="btnAgregar" href="#" class="btn btn-success">Agregar</a>
                    </div>                    
                </div> 
                <div class="row">
                    <table class="display dataTable  cell-border right" id="tblDocumentos" style="width:90%;font-size:12px;">
                        <thead>
                            <tr role="row">		
                                <th style="width:15%">Tipo</th>
                                <th style="width:15%">Serie-Número</th>
                                <th style="width:8%">Fecha</th>
                                <th style="width:30%">Institución/Empresa</th>											
                                <th class="text-right" style="width:8%">Importe</th>
                                <th style="width:30%">Observacion</th>
                                 <th style="width:10%"></th>
                                  <th style="width:10%"></th>
                            </tr>
                        </thead>
                        <tbody id="TDocs" runat="server">
                        </tbody>									 
                    </table>	
                </div>
            </div>
            <div id="DivAgregar" class="form-horizontal" style="display:none">

                <div class="col-lg-12">
                    <form  enctype="multipart/form-data"  class="form-horizontal" role="form" id="frmRegistro" name="frmRegistro" enctype="multipart/form-data" method="post"   >
                        <input type="hidden" id="Id" name="Id" value="Grabar" />
                        <input type="hidden" id="NroRendicion" name="NroRendicion" />
                        <input type="hidden" id="Distrito" name="Distrito" />
                        <input type="hidden" id="Provincia" name="Provincia" />
                        <input type="hidden" id="Departamento" name="Departamento" />
                        <input type="hidden" id="OpPersona" name="OpPersona"  />
                        <input type="hidden" id="NroRendAnual" name="NroRendAnual" />
                        <input type="hidden" id="FchaRendicion" name="FchaRendicion" />
                        
                        <div class="form-group">
                            <label for="ejemplo_email_3" class="col-lg-3  col-md-2 col-sm-3 col-xs-3 control-label">Nro. Ruc(DNI) :</label>
                            <div class="col-lg-2 col-md-2 col-sm-5 col-xs-5">
                                <input type="text" class="form-control" id="txtNroRuc" placeholder="Nro de Ruc" name="txtNroRuc" maxlength=11/>
                            </div>
                            <div class="col-lg-2 col-md-2 col-sm-4 col-xs-4">
                                <a id="btnConsultar" onclick="ConsultarRuc();" name="btnConsultar" href="#" class="btnsearch btn-success">Consultar</a>
                            </div>
							<div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                                <a target="_black" name="btnSunat" href="http://e-consultaruc.sunat.gob.pe/cl-ti-itmrconsruc/jcrS00Alias" >Consulta Ruc Sunat</a>
                            </div>
                        </div> 
                        <div class="form-group">
                            <label for="ejemplo_email_3" class="col-lg-3 col-md-2 col-sm-3 col-xs-3 control-label">Empresa / Institución :</label>
                            <div class="col-lg-7 col-md-8 col-sm-8 col-xs-8">
                                <input type="text" class="form-control" id="txtRazonSocial" name="txtRazonSocial" maxlength=100
                                       placeholder="Nombre /Razon Social"/>
                            </div>
                        </div> 
                        <div class="form-group">
                            <label for="ejemplo_email_3" class="col-lg-3 col-md-2 col-sm-3 col-xs-3 control-label">Dirección :</label>
                            <div class="col-lg-7 col-md-8 col-sm-8 col-xs-8">
                                <input type="text" class="form-control" id="txtDireccion" name="txtDireccion"
                                       placeholder="Dirección de la Empresa que emite el comprobante"/>
                            </div>
                        </div>
                        <div class="form-group">
                            <label for="ejemplo_email_3" class="col-lg-3 col-md-2 col-sm-3 col-xs-3 control-label">Tipo de Documento :</label>
                            <div class="col-md-9 col-sm-8 col-xs-8">
                                <div class="form-group row">
                                    <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">
                                        <select id="listDocumento" class="form-control" name="listDocumento">
                                            <option value="2">FACTURA</option>
                                            <option value="1">BOLETA</option>
                                            <option value="40">BOLETO COMPRA AEREA</option>
                                            <option value="46">BOLETO DE VIAJE TERRESTRE</option>
                                            <option value="86">PLANILLA DE MOVILIDAD</option>
                                            <option value="33">TICKET</option>
											<option value="87">VOUCHER</option>
                                           
                                        </select>
                                    </div>                
                                    <div class="col-md-2 col-sm-2 col-xs-2">
                                        <input type="text" class="form-control" id="txtSerie" maxlength=4 placeholder="Serie" name="txtSerie"/>
                                    </div>                
                                    <div class="col-md-2 col-sm-2 col-xs-2">
                                        <input type="text" class="form-control" id="txtNumero" maxlength=10 placeholder="Número" name="txtNumero"/>
                                    </div>

                                </div>
                            </div>
                        </div>
                        <div class="form-group" id="divGrupoCuenta">
                            <label for="ejemplo_email_3" class="col-lg-3 col-md-2 col-sm-3 col-xs-3 control-label">Nro. Cta. Bancaria :</label>
                            <div class="col-md-9 col-sm-8 col-xs-8">
                                <div class="form-group row">
                                    <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">
                                        <select id="selectCuenta" class="form-control" name="selectCuenta">
                                            <option value="2">BANCO INTERBANK(700-3000186214)</option>                                                                                        
                                        </select>
                                    </div>                
                                    <div class="col-md-2 col-sm-2 col-xs-2">
                                        <input type="text" class="form-control" id="txtNroOperacion" maxlength=10 placeholder="Nro. Operación" name="txtNroOperacion"/>
                                    </div>                
                                    

                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <label for="ejemplo_email_3" class="col-lg-3 col-md-2 col-sm-3 col-xs-3 control-label">Fecha :</label>
                            <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                               <input type="Date"   placeholder="dd/mm/yyyy" class="form-control" id="txtFecha" name="txtFecha"/>(dd/mm/yyyy)
                              
							
                               
                            </div>
                        </div> 
                        <div class="form-group">
                            <label for="ejemplo_email_3" class="col-lg-3  col-md-2 col-sm-3 col-xs-3 control-label">Importe :</label>
                            <div class="col-lg-2 col-md-2 col-sm-3 col-xs-3">
                                <input type="Text" class="form-control" maxlength=10 id="txtImporte" placeholder="Importe" name="txtImporte"/>
                            </div>
                        </div> 
                        <div class="form-group">
                            <label for="ejemplo_email_3" class="col-lg-3  col-md-2 col-sm-3 col-xs-3 control-label">Archivo Adjunto :</label>
                            <div class="col-lg-2 col-md-2 col-sm-3 col-xs-3">
                                <input type="file" name="fileData" id="fileData"  runat="server"   />
                            </div>
                        </div>
                        <div class="form-group">
                            <label for="ejemplo_email_3" class="col-lg-3 col-md-2 col-sm-3 col-xs-3 control-label">Observación :</label>
                            <div class="col-lg-7 col-md-8 col-sm-8 col-xs-8">
                                <textarea   class="form-control" rows="5" maxlength="100" id="txtObservacion"  name="txtObservacion" placeholder="Ingrese las Observaciones referente al Comprobante que esta Ingresado"></textarea>
                            </div>
                        </div> 
                        <div class="form-group">
                            <label for="ejemplo_email_3" class="col-lg-3 col-md-2 col-sm-3 col-xs-3 control-label"></label>
                            <div class ="col-lg-1  col-md-1 col-sm-2 col-xs-2">
                                <a id="A2" onclick="Grabar();" href="#" class="btn btn-success">Grabar</a>
                            </div>
                            <div class ="col-lg-2 col-md-2 col-sm-2 col-xs-2">
                                <a id="btnCancelar"  href="#" class="btn btn-danger">Cancelar</a>
                            </div>
                        </div> 
                         <div class="form-group">
                            <label for="ejemplo_email_3" class="col-lg-3 col-md-2 col-sm-3 col-xs-3 control-label"></label>
                            <div  id="divMessage" name="divMessage" style="display:none" class ="col-lg-8  col-md-8 col-sm-8 col-xs-8 alert alert-danger alert-dismissable">
                               
                            </div>
                            
                        </div> 
                    </form>
                </div>         

            </div> 
            <div id="temp1"></div>
            <div id="temp2"></div>  
            
            
            
            
            
            <div class="modal fade modal-md" id="ModalPrint" tabindex="-1" role="dialog" aria-labelledby="ModalPrint" data-backdrop="static" data-keyboard="false">
                <div class="modal-dialog modal-lg" role="document">
                    <div class="modal-content" style="max-width: 800px">
                        <!--Header-->
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close" id="Button1" name="btnSalir">
                                <span aria-hidden="true">&times;</span>
                            </button>
                            <h4 class="modal-title w-100 " id="H1">Imprimir Rendición</h4>
                            
                        </div>
                        <!--Body-->
                        <div class="modal-body" id="Imprimier" name="Imprimier">
                        <div class="row">
                        <div class="col-lg-12 col-md-12 col-xs-12 col-sm-12">
                             <table cellpadding="3" border="0" cellspacing="0" bordercolor="gray" bgcolor="white" style="width: 98%; border-collapse: collapse" >
                                        <tr>
                                         
                                        <td class="tdimprimir" colspan="10"><label class="col-sm-12  col-md-12 col-sm-12 col-xs-12 control-label text-center" for="lblNumeroRendAnual"  id="lblNumeroRendAnual" name="lblNumeroRendAnual"><h1>0</h1></label></td>
                                        </tr>
                                        <tr>
                                            <td class="tdimprimir">Responsable </td>
                                            <td class="tdimprimir" colspan="5"><label class="col-sm-12  col-md-12 col-sm-12 col-xs-12 control-label" for="lblCliente"  id="lblCliente" name="lblCliente"><b>Telecedito</b></label></td>
                                            <td class="tdimprimir" colspan="3"><label class="col-sm-12  col-md-12 col-sm-12 col-xs-12 control-label" for="lblNumeroPedido"  id="lblNumeroPedido" name="lblNumeroPedido"><b>0</b></label></td>
                                        </tr>
                                        <tr>
                                            <td class="tdimprimir">
                                                Documento
                                            </td>
                                            <td class="tdimprimir"><label class="col-sm-12  col-md-12 col-sm-12 col-xs-12 control-label" for="lblTipDoc"  id="lblTipDoc" name="lblTipDoc"><b>Telecedito</b></label> </td> 
                                            <td class="tdimprimir">Nro.</td>
                                            <td class="tdimprimir" colspan=3 ><label class="col-sm-12  col-md-12 col-sm-12 col-xs-12 control-label" for="lblSerieNumero"  id="lblSerieNumero" name="lblSerieNumero"><b>000-00</b></label></td>
                                            <td class="tdimprimir">Fecha</td>
                                            <td class="tdimprimir" colspan=2><label class="col-sm-12  col-md-12 col-sm-12 col-xs-12 control-label" for="lblFecha"  id="lblFecha" name="lblFecha"><b>19/02/2017</b></label></td>
                                            
                                        </tr>
                                        <tr>
                                            <td class="tdimprimir">Rubro </td>
                                            <td class="tdimprimir" colspan=2><label class="col-sm-12  col-md-12 col-sm-12 col-xs-12 control-label" for="lblRubro"  id="lblRubro" name="lblRubro"><b>ENTREGA A RENDIR</b></label></td>
                                            <td class="tdimprimir">Moneda</td>
                                            <td class="tdimprimir" colspan="3"><label class="col-sm-12  col-md-12 col-sm-12 col-xs-12 control-label" for="lblMoneda"  id="lblMoneda" name="lblMoneda"><b>Soles</b></label></td>
                                            <td class="tdimprimir">Usuario</td>
                                            <td class="tdimprimir"><label class="col-sm-12  col-md-12 col-sm-12 col-xs-12 control-label" for="lblUsuario"  id="lblUsuario" name="lblUsuario"><b>jguevara</b></label></td>
                                        </tr>
                                        <tr>
                                        <td colspan="9">
                                            <%--<table cellpadding="6" cellspacing="0"  border="0" bgcolor="white" style="width: 100%; border-collapse: collapse" >
                                                <tr>
                                                     <td class="tdimprimir"> Imp.Entregado :</td>
                                                    <td class="tdimprimir"> <label class="col-sm-2  col-md-2 col-sm-2 col-xs-2 control-label" for="ImpEntregadoPr"  id="ImpEntregadoPr" name="ImpEntregadoPr"><b>8,500.00 &nbsp;&nbsp;</b></label></td>
                                                    <td class="tdimprimir"> Imp.Rendido :</td>
                                                    <td class="tdimprimir"> <label class="col-sm-2  col-md-2 col-sm-2 col-xs-2 control-label" for="ImpRendidoPr"  id="ImpRendidoPr" name="ImpRendidoPr"><b>8,500.00 &nbsp;&nbsp;</b></label></td>                                                                                                
                                                </tr>
                                                    <td class="tdimprimir"> Imp.Devuelto :</td>
                                                    <td class="tdimprimir"> <label class="col-sm-2  col-md-2 col-sm-2 col-xs-2 control-label" for="ImpDevueltoPr"  id="ImpDevueltoPr" name="ImpDevueltoPr"><b>8,500.00 &nbsp;&nbsp;</b></label></td>
                                                    <td class="tdimprimir"> Saldo :</td>
                                                    <td class="tdimprimir"> <label class="col-sm-2  col-md-2 col-sm-2 col-xs-2 control-label" for="ImpSaldoPr"  id="ImpSaldoPr" name="ImpSaldoPr"><b>8,500.00 &nbsp;&nbsp;</b></label></td>
                                                <tr>
                                                </tr>
                                            </table>--%>
                                        </td>
                                       
                                        </tr>
                                    </table>
                        </div>
                           
                        </div>
                         <div class="row">
                         <div class="col-lg-12 col-md-12 col-xs-12 col-sm-12">
                              <table border=1 class="tablePrint" id="tblPrint" style="width:100%;font-size:12px;">
                                    <thead>
                                        <tr role="row">		
                                            <th style="width:2%">#</th>
                                            <th style="width:15%">Tipo</th>
                                            <th style="width:15%;padding-right:6px;" class="text-right">Serie-Número</th>
                                            <th style="width:8%">Fecha</th>
                                            <th style="width:30%">Institución/Empresa</th>											
                                            <th class="text-right" style="width:8%;padding-right:6px;">Importe</th>
                                            <th style="width:30%">Observacion</th>
                                        </tr>
                                    </thead>
                                    <tbody id="TbodyPrint" runat="server">
                                    </tbody>									 
                                    <tfoot>
                                        <tr>
                                            <td colspan="5">
                                             <table>
                                             <tr><br /></tr>
                                                <tr>
                                                    <td>
                                                        _________________________________________
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align:center">
                                                        <label class="col-sm-12  col-md-12 col-sm-12 col-xs-12 control-label" for="lblNombreFirma"  id="lblNombreFirma" name="lblNombreFirma"><b>8,500.00</b></label>
                                                    </td>
                                                </tr>
                                            </table>
                                             </td>
                                            <td colspan=2>
                                            <table cellpadding="3" border="1" cellspacing="0" bordercolor="gray" bgcolor="white" style="width: 100%; border-collapse: collapse">
                                                <tr style=" border-color:gray; border-left:2px;border-bottom:none;border-top:none">
                                                    <td style="border-bottom: none;border-top: none;border-right:none;">Imp.Entregado </td>
                                                    <td style="border-bottom: none;border-top: none;border-left:none;border-right:none;">:</td>
                                                    <td style="border-bottom: none;border-top: none;"><label class="col-sm-12  col-md-11 col-sm-11 col-xs-11 control-label text-right" for="ImpEntregadoPr"  id="ImpEntregadoPr" name="ImpEntregadoPr"><b>0.00</b></label></td>
                                                </tr>
                                                <tr style=" border-color:gray; border-left:2px;border-bottom:none;border-top:none">
                                                    <td style="border-bottom: none;border-top: none;border-right:none;">Imp.Rendido </td>
                                                    <td style="border-bottom: none;border-top: none;border-left:none;border-right:none;">:</td>
                                                    <td style="border-bottom: none;border-top: none;"><label class="col-sm-11  col-md-11 col-sm-11 col-xs-11 control-label text-right" for="ImpRendidoPr"  id="ImpRendidoPr" name="ImpRendidoPr"><b>0.00 &nbsp;&nbsp;</b></label></td>
                                                </tr>
                                                <tr style=" border-color:gray; border-left:2px;border-bottom:none;border-top:none">
                                                    <td style="border-bottom: none;border-top: none;border-right:none;">Imp.Devuelto </td>
                                                    <td style="border-bottom: none;border-top: none;border-left:none;border-right:none;">:</td>
                                                    <td style="border-bottom: none;border-top: none;"> <label class="col-sm-11  col-md-11 col-sm-11 col-xs-11 control-label text-right" for="ImpDevueltoPr"  id="ImpDevueltoPr" name="ImpDevueltoPr"><b>8,500.00 &nbsp;&nbsp;</b></label></td>
                                                </tr>
                                                <tr style=" border-color:gray; border-left:2px;border-bottom:none;border-top:none">
                                                    <td style="border-bottom: none;border-top: none;border-right:none;">Saldo </td>
                                                    <td style="border-bottom: none;border-top: none;border-left:none;border-right:none;" >:</td>
                                                    <td style="border-bottom: none;border-top: none;"><label class="col-sm-11  col-md-11 col-sm-11 col-xs-11 control-label text-right" for="ImpSaldoPr"  id="ImpSaldoPr" name="ImpSaldoPr"><b class="text-right">8,500.00</b></label></td>
                                                </tr>
                                                
                                            </table>
                                            </td>
                                        </tr>
                                    </tfoot>
                                </table>	
                         </div>
                           
                        </div>
                                                                                                                                     
                        </div>
                        <div class="modal-footer">
                            <div class="row">
                                <div class="col-lg-6 col-md-6 col-xs-6 col-sm-6">
                                     <a href="#" onclick="imprSelec('Imprimier',0)"  class="btn btn-primary btn-icon-primary btn-icon-block btn-icon-blockleft" data-toggle="modal" data-target="#ModalPrint" data-whatever="@getbootstrap"><i  class="ion  ion-ios-printer-outline"></i><span>Imprimir</span></a>
                                </div>
                            </div>
                        </div>
                        <!--Footer-->
                        <%--<div class="modal-footer">
                            <button type="button" class="btn btn-default" data-dismiss="modal">Salir</button>
                        </div>--%>
                    </div>
                </div>
            </div> 
                 
                 
                 <div class="modal fade modal-md" style="z-index:0" id="ModelDetalle" tabindex="-1" role="dialog" aria-labelledby="ModelDetalle" data-backdrop="static" data-keyboard="false">
                <div class="modal-dialog modal-lg" role="document">
                    <div class="modal-content" style="max-width: 800px">
                        <!--Header-->
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close" id="Button2" name="btnSalir">
                                <span aria-hidden="true">&times;</span>
                            </button>
                            <h4 class="modal-title w-100 " id="H2">Detalle de la rendición</h4>
                            
                        </div>
                        <!--Body-->
                        <div class="modal-body" id="divDetallerend" name="Imprimier">
                        
                                                                                                                                     
                        </div>
                        <div class="modal-footer">
                            <div class="row">
                                <div class="col-lg-6 col-md-6 col-xs-6 col-sm-6">
                                     <button type="button" class="btn btn-red " data-dismiss="modal" aria-label="Close" id="Button3" name="btnSalir">
                                <span aria-hidden="true">Salir</span>
                            </button>
                                </div>
                            </div>
                        </div>
                        <!--Footer-->
                        <%--<div class="modal-footer">
                            <button type="button" class="btn btn-default" data-dismiss="modal">Salir</button>
                        </div>--%>
                    </div>
                </div>
            </div>
         <div  class="modal fade  "id="GaleriaImages" role="dialog" data-backdrop="static" data-keyboard="false">
          <div class="modal-dialog modal-md" role="document">
          <div class="modal-content">
            <div class="modal-header">
		        <button class="close" onclick="Fixed(event);" type="button" data-dismiss="modal"> <span aria-hidden="true">&times;</span></button>
		        <h3 class="modal-title">Archivos Adjunto del Comprobante</h3>
	        </div>
               
	        <div class="modal-body">
	                <div class="row">
	                    <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4" id="DivArchivos">
        		        <%-- <img id="imgDocumento"  class=" col-lg-12 col-md-12 col-xs-12 col-sm-12 img-responsive" src="../Archivosderendicion/A114.jpg"/>--%>
        		       </div>
        		       <div class="col-lg-7 col-md-7 col-sm-7 col-xs-7" id="DivVistaPrevia">
        		         <img id="imgDocumento"  class=" col-lg-12 col-md-12 col-xs-12 col-sm-12 img-responsive" src=""/>
        		       </div>
	                </div>
        		                     
	        </div>
	        <div class="modal-footer">
		        <button class="btn btn-default" data-dismiss="modal" onclick="Fixed(event);">Salir</button>
	        </div>
           </div>
          </div>
        </div>  
        
        <div class="modal fade" id="continuemodal"  role="dialog"   data-backdrop="static">
		<div class="modal-dialog modal-md" role="document">
			<div class="modal-content">
				<div class="modal-header">
					<button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true" class="ti-close"></span></button>
					<h4 class="modal-title" id="myModalLabel3">Alerta</h4>
				</div>
				<div class="modal-body">
					<div id="DivSessionUser" class=""></div>
				</div>
				<div class="modal-footer">
					<button type="button" class="btn btn-primary"  data-dismiss="modal">Cerrar</button>
				</div>
			</div>
		</div>
	</div>
    </body>
</html>
