<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ListarRendiciones.aspx.vb" Inherits="administrativo_Tesoreria_Rendiciones_AppRendiciones_ListarRendiciones" %>

<!DOCTYPE html>

<html lang="en">
    <meta charset="utf-8"/>
    <meta http-equiv="X-UA-Compatible" content="IE=edge"/> 
    <meta http-equiv='X-UA-Compatible' content='IE=8' />
    <meta http-equiv='X-UA-Compatible' content='IE=10' />
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=0"/> <!--320-->
    <head id="Head1" runat="server">
        <title></title>



        <link rel='stylesheet' href='../../../../academico/assets/css/bootstrap.min.css'/>
        <link rel='stylesheet' href='../../../../academico/assets/css/material.css'/>
        <link rel='stylesheet' href='../../../../academico/assets/css/style.css?x=1'/>
        <link rel='stylesheet' href='../../../../academico/assets/css/jquery.dataTables.min.css'/>

        <script src="../../../../academico/assets/js/jquery.js" type="text/javascript"></script>
        <script src="../../../../academico/assets/js/app.js" type="text/javascript"></script>
        <script type="text/javascript" src='../../../../academico/assets/js/jquery.dataTables.min.js'></script>

        <script src="../../../../academico/assets/js/bootstrap.min.js" type="text/javascript"></script>
        <style type="text/css">
            td.details-control 
            {

                background: url('../../../../academico/assets/img/open.png') no-repeat center center;
                cursor: pointer;
            }
            tr.details td.details-control {
                background: url('../../../../academico/assets/img/resources/details_close.png') no-repeat center center;
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
        </style>

        <script type="text/javascript">
            var OpInit = 0;
            var OpList = 0;
            $(document).on("click", "#frmRegistro input[type='text']", function() {
                alert('ppp');
            });
            $(document).ready(function() {


                $("input[type='text']").each(function() {
                    console.log("Oks");
                    $(this).keydown(function(e) {
                        console.log('***' + e.keyCode);
                        if (e.keyCode == 40) {
                            //if ($(this).parent().next().length > 0)
                            console.log($(this).parent().next().children());
                            $(this).parent().next().children()[0].focus();
                        }
                        else if (e.keyCode == 37) {
                            if ($(this).parent().prev().length > 0)
                                $(this).parent().prev().children()[0].focus();
                        }
                    });
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
                            success: function(data) {
                                sOut = '<table cellpadding="5" cellspacing="2" border="0" style="padding-left:50px;width:80%;font-size:12px;">';
                                sOut += '<tr>';
                                sOut += '<th>' + 'Rubro' + '</th>';
                                sOut += '<th>' + 'Importe ' + '</th>';
                                sOut += '<th>' + 'Centro de costos ' + '</th>';
                                sOut += '<th>' + ' Estado ' + '</th>';
                                sOut += '<th>' + 'Observacion ' + '</th>';
                                sOut += '<th>' + '' + '</th>';
                                sOut += '</tr>';
                                jQuery.each(data, function(i, val) {
                                    var docente = '';
                                    sOut += '<tr>';
                                    sOut += '<td>' + val.Rubro + ' ' + '</td>';
                                    sOut += '<td>' + val.Importe + ' ' + '</td>';
                                    sOut += '<td>' + val.Centrocostos + ' ' + '</td>';
                                    sOut += '<td>' + val.EstadoRendicion + ' ' + '</td>';
                                    sOut += '<td>' + val.Observacion + ' ' + '</td>';
                                    sOut += '<td>' + '<a href="#" id=' + val.CodigoRend + ' onclick="Rendir(' + val.CodigoRend + ')"  class="btn btn-green" data-toggle="modal" data-target="#exampleModal" data-whatever="@getbootstrap"><i  class="ion-plus"><span></span></i></a>' + '</td>';
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
                    }
                    /// ev.preventDefault();
                });
            });
            function Rendir(Nrorend) {
                //   alert(OpInit)
                $("#myModalLabel").html("Registro de Documento a Rendir(" + Nrorend + ")");
                TotalRendicion(Nrorend);
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
                //  ev.preventDefault();
            }
            function TotalRendicion(NroRend) {

                $.ajax({
                    type: "GET",
                    contentType: "application/json; charset=utf-8",
                    url: "DataJson/ListarRendicion.aspx",
                    data: { "Id": NroRend, "Funcion": "RendImportes" },
                    dataType: "json",
                    success: function(data) {
                        console.log(data);
                        jQuery.each(data, function(i, val) {
                        var docente = '';
                        console.log("" + val.Importe)
                        $("#ImpEntregado").html(val.Importe);
                        $("#ImpRendido").html(val.MontoRendido);
                        $("#ImpDevuelto").html(val.MontoDevuelto);
                        $("#Saldo").html(val.SaldoRendir);
                        });


                    },
                    error: function(result) {


                    console.log(result);
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
                        console.log(data);
                        var filas = data.length;
                        $("#txtRazonSocial").val(data.NombreRazonSocial);
                        $("#txtDireccion").val(data.Direccion);
                    },
                    error: function(result) {
                        //  console.log(data);
                        // console.log(result.responseText);
                    }
                });
            }
            function Grabar() {
                alert("oKS");
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    url: "DataJson/ListarRendicion.aspx",
                    data:  $("#frmRegistro").serialize(),
                    dataType: "json",
                    success: function(data) {
                        console.log(data);
                         
                    },
                    error: function(result) {
                    console.log(result);
                        // console.log(result.responseText);
                    }
                });
            
            }
        </script>
    </head>
    <body>
        <form id="Form1" action="#"  runat="server">  
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
                                        <li class="list-unstyled"><i class="icon ion-checkmark text-primary"></i> &nbsp;<b>Codigo: </b>684. &nbsp;&nbsp; <i class="icon ion-checkmark text-primary"></i> &nbsp;<b>DNI: </b>41893730.</li>
                                        <li class="list-unstyled"><i class="icon ion-checkmark text-primary"></i> &nbsp;<b>Colaborador: </b>Hugo Saavedra Sanchez.</li>
                                    </ul>

                                </div>		
                            </div>

                        </div> 

                        <div class="row">
                            <div class="col-md-4"  style="float:left">
                                <div class="form-group">
                                    <label class="col-sm-5 control-label"><b>Estado de la Rendición :</b></label>
                                    <div class="col-sm-7"> 
                                        <select class="name_search form-control valid" name="choice" data-validation="required" data-validation-error-msg="Please make a choice">
                                            <option value=""> - - Seleccione - - </option>
                                            <option> Pendientes </option>
                                            <option> Finalizadas </option>                					        
                                        </select>
                                    </div>            
                                </div>  
                            </div>
                            <div class="col-md-1" >
                                <div class="form-group">                           
                                    <div class="col-sm-3"><Button type="button" name="btnConsultar" id="btnConsultar"  Text="Consultar Cursos" class="btn btn-primary">Consultar</button></div>  
                                </div>  
                            </div>
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
                                                <th style="width:8%">Numero</th>
                                                <th style="width:8%">Moneda</th>
                                                <th style="width:10%">Importe</th>											
                                                <th style="width:8%">Estado</th>
                                                <th style="width:10%">Usuario</th>
                                                <th style="width:40%">Observacion</th>
                                                <th style="width:40%">Rendir</th>
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
            <div class="modal fade modal-md" id="exampleModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" data-backdrop="static" data-keyboard="false">
                <div class="modal-dialog modal-lg" role="document">
                    <div class="modal-content">
                        <!--Header-->
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close" id="btnSalir" name="btnSalir">
                                <span aria-hidden="true">&times;</span>
                            </button>
                            <h4 class="modal-title w-100 " id="myModalLabel">Registro de Documento a Rendir</h4>
                        </div>
                        <!--Body-->
                        <div class="modal-body">
                            <div class="row">
                                <div class="col-lg-12 col-md-12">
                                    <div class ="col-lg-3 col-md-3">
                                        <label class="col-sm-10 col-md-8 control-label"><b>Imp. Entregado :</b></label>
                                        <label class="col-sm-2  col-md-2 control-label" id="ImpEntregado"><b>8,500.00 &nbsp;&nbsp;</b></label>
                                    </div>
                                    <div class ="col-lg-3 col-md-3">
                                        <label class="col-sm-10 control-label"><b>Imp. Rendido:</b></label>
                                        <label class="col-sm-2 control-label" id="ImpRendido"><b>0.00</b></label>
                                    </div>
                                    <div class ="col-lg-3 col-md-3">
                                        <label class="col-sm-10 control-label"><b>Imp. Devuelto:</b></label>
                                        <label class="col-sm-2 control-label" id="ImpDevuelto"><b>0.00</b></label>
                                    </div>
                                    <div class ="col-lg-3 col-md-3">
                                        <label class="col-sm-7 control-label"><b>Saldo:</b></label>
                                        <label class="col-sm-2 control-label" id="Saldo" ><b>8,500.00</b></label>
                                    </div>
                                </div>
                            </div> 

                            <div class="row">
                                <div id="Content" class="col-lg-12">

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
                    <div class =col-lg-1>
                    </div>
                    <div class =col-lg-1>
                        <a id="btnAgregar" href="#" class="btn btn-success">Agregar</a>
                    </div>                    
                </div> 
                <div class="row">
                    <table class="display dataTable cell-border" id="tblDocumentos" style="width:90%;font-size:12px;">
                        <thead>
                            <tr role="row">		
                                <th style="width:15%">Tipo</th>
                                <th style="width:15%">Serie-Número</th>
                                <th style="width:8%">Fecha</th>
                                <th style="width:30%">Institución/Empresa</th>											
                                <th style="width:8%">Importe</th>
                                <th style="width:30%">Observacion</th>
                            </tr>
                        </thead>
                        <tbody id="TDocs" runat="server">
                        </tbody>									 
                    </table>	
                </div>
            </div>
            <div id="DivAgregar" class="form-horizontal" style="display:none">

                <div class="col-lg-12">
                    <form action="#" class="form-horizontal" role="form" id="frmRegistro" name="frmRegistro">
                        <input type="hidden" id="id" value="Grabar" />
                        <div class="form-group">
                            <label for="ejemplo_email_3" class="col-lg-3  col-md-2 col-sm-3 col-xs-12 control-label">Nro. Ruc :</label>
                            <div class="col-lg-2 col-md-2 col-sm-5 col-xs-5">
                                <input type="text" class="form-control" id="txtNroRuc" placeholder="Nro de Ruc" name="txtNroRuc"/>
                            </div>
                            <div class="col-lg-2 col-md-2 col-sm-4 col-xs-4">
                                <a id="btnConsultar" onclick="ConsultarRuc();" name="btnConsultar" href="#" class="btnsearch btn-success">Consultar</a>
                            </div>
                        </div> 
                        <div class="form-group">
                            <label for="ejemplo_email_3" class="col-lg-3 col-md-2 control-label">Empresa / Institución :</label>
                            <div class="col-lg-7 col-md-8">
                                <input type="text" class="form-control" id="txtRazonSocial" name="txtRazonSocial"
                                       placeholder="Nombre /Razon Social"/>
                            </div>
                        </div> 
                        <div class="form-group">
                            <label for="ejemplo_email_3" class="col-lg-3 col-md-2 control-label">Dirección :</label>
                            <div class="col-lg-7 col-md-8">
                                <input type="text" class="form-control" id="txtDireccion" 
                                       placeholder="Dirección de la Empresa que emite el comprobante"/>
                            </div>
                        </div>
                        <div class="form-group">
                            <label for="ejemplo_email_3" class="col-lg-3 col-md-2 control-label">Tipo de Documento :</label>
                            <div class="col-md-9">
                                <div class="form-group row">
                                    <div class="col-lg-3 col-md-3">
                                        <select id="listDocumento" class="form-control">
                                            <option>Factura</option>
                                            <option>Boleta</option>
                                        </select>
                                    </div>                
                                    <div class="col-md-2">
                                        <input type="text" class="form-control" id="txtSerie" placeholder="Serie">
                                    </div>                
                                    <div class="col-md-2">
                                        <input type="text" class="form-control" id="txtNumero" placeholder="Número">
                                    </div>

                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <label for="ejemplo_email_3" class="col-lg-3 col-md-2 control-label">Fecha :</label>
                            <div class="col-lg-2 col-md-2">
                                <input type="Date" class="form-control" id="txtFecha"/>
                            </div>
                        </div> 
                        <div class="form-group">
                            <label for="ejemplo_email_3" class="col-lg-3  col-md-2 control-label">Importe :</label>
                            <div class="col-lg-2 col-md-2">
                                <input type="Text" class="form-control" id="txtImporte" placeholder="Importe"/>
                            </div>
                        </div> 
                        <div class="form-group">
                            <label for="ejemplo_email_3" class="col-lg-3 col-md-2 control-label">Observación :</label>
                            <div class="col-lg-7 col-md-8">
                                <textarea class="form-control" rows="5" id="txtObservacion"  placeholder="Ingrese las Observaciones referente al Comprobante que esta Ingresado"></textarea>
                            </div>
                        </div> 
                        <div class="form-group">
                            <label for="ejemplo_email_3" class="col-lg-3 col-md-2 control-label"></label>
                            <div class ="col-lg-1  col-md-1">
                                <a id="A2" onclick="Grabar();" href="#" class="btn btn-success">Grabar</a>
                            </div>
                            <div class ="col-lg-2 col-md-2">
                                <a id="btnCancelar"  href="#" class="btn btn-danger">Cancelar</a>
                            </div>
                        </div> 
                    </form>
                </div>         

            </div> 
            <div id="temp1"></div>
            <div id="temp2"></div>
        </form>
    </body>
</html>
