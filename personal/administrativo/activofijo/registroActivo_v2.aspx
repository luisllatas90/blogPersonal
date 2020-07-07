<%@ Page Language="VB" AutoEventWireup="false" CodeFile="registroActivo_v2.aspx.vb" Inherits="administrativo_activofijo_registroActivo_v2" %>
<html id="Html1" lang="en" runat="server">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1">
    <title>Registro de Serie</title>

	<!-- custom scrollbar stylesheet -->
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta http-equiv='X-UA-Compatible' content='IE=7' />
    <meta http-equiv='X-UA-Compatible' content='IE=8' />
    <meta http-equiv='X-UA-Compatible' content='IE=10' />
    <meta http-equiv="X-UA-Compatible" content="IE=edge"/> 
    
	<meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=0"/> <!--320-->
	<script type="text/javascript" src="../../assets/js/jquery.js"></script>
	<script type="text/javascript" src="../../assets/js/bootstrap.min.js"></script>	
	<script type="text/javascript" src='../../assets/js/noty/jquery.noty.js'></script>
    <script type="text/javascript" src='../../assets/js/noty/layouts/top.js'></script>
    <script type="text/javascript" src='../../assets/js/noty/layouts/default.js'></script>
    <script type="text/javascript" src='../../assets/js/noty/notifications-custom.js'></script>
    <script type="text/javascript" src='../../assets/js/jquery-ui-1.10.3.custom.min.js'></script>
    
    <!-- Manejo de tablas -->
    <script type="text/javascript" src='../../assets/js/jquery.dataTables.min.js'></script>
    <link href="../../assets/css/jquery.dataTables.min.css" rel="stylesheet" type="text/css" />
    <script src="../../assets/js/funcionesDataTable.js?y=1" type="text/javascript"></script>
    
    <!-- Piluku -->        
    <link rel="stylesheet" type="text/css" href="../../assets/css/bootstrap.min.css"/>
	<link rel="stylesheet" href="../../assets/css/material.css?x=1"/>		
	<link rel="stylesheet" type="text/css" href="../../assets/css/style.css?y=4"/>
    
    <script src="assets/js/funcionesDataTableAF.js" type="text/javascript"></script>
    
	<!-- activo fijo -->
    <link href="assets/css/style_af.css" rel="stylesheet" type="text/css" />

    <script src="../../assets/js/bootstrap-datepicker.js" type="text/javascript"></script>

<style type="text/css">

td.details-control {
	background: url('assets/img/details_open.png') no-repeat center center;
	cursor: pointer;
	font-size:small;
}
tr.shown td.details-control {
	background: url('assets/img/details_close.png') no-repeat center center;
}

	</style>

</head>
<script type="text/javascript">
    var lstPedido;
    var lstDetPedido;
    
    var aDataP = [];
    var aDataDP = [];
    var aDataAC = [];
    var aDataCC = [];
    var aDataDR = [];
    var CChk = 0;
    var Ctxt = 0;

    var articulos;
    var cantidades;
    var egresos;
    
    jQuery(document).ready(function() {
        document.execCommand('ClearAuthenticationCache');
        
        fnResetDataTableTramite('tbArtCom', 0, 'desc');
        //fnResetDataTableTramite('tbActivoFijo', 0, 'desc');
        //console.log($('#param1').val());
        $("#btnRegresar").click(fnRegresar);
        $("#btnGuardarCrt").click(fnGuardarDetalleActivoFijo);
        $("#btnActualuzarSerie").click(fnActualizarSerie);
        llenarFormulario($('#param1').val(), "P");
        llenarFormulario($('#param1').val(), "DP");

        articulos = $('#param2').val().split(",");
        cantidades = $('#param3').val().split(",");
        egresos = $('#hdIEgreso').val().split(",");
        //alert($('#param2').val() + "-" +  articulos.length );
        //alert($('#param3').val() + "-" + cantidades.length);

    });

    function fnActualizarSerie() {
        $('#hdAccion').val("ACT");
        $('#hdUnid').val("1");
        //$('#hdIEgreso').val(0);
        //alert($('#param1').val());
        //alert($('#hdIEgreso').val());
//        alert($('#txtSerie').val());
//        alert($('#hdNumeroEs').val());
//        alert($('#hdArt').val());
//        alert($('#hdAccion').val());
//        alert($('#txtNroPedido').val());
        if ($('#txtSerie').val() == '') {            
            alert("Ingrese Serie");
        }
        else {

            var boton = document.getElementById("btnActualuzarSerie");
            boton.innerHTML = "Procesando"
            boton.disabled = true;
            
            $("input#param0").val("gActualizarSerieEgreso");
            var form = $('#frmRegistroActivo').serialize();
            $.ajax({
                type: "GET",
                contentType: "application/json; charset=utf-8",
                url: "../../DataJson/activofijo/processactivofijo.aspx",
                data: form,
                dataType: "json",
                success: function(data) {
                    //console.log("-------------------");
                    //console.log(data);
                    if (data[0].Status == "success") {
                        alert(data[0].Message);
                    } else {
                        alert("Error: " + data[0].Message);
                    }
                    //fnMensaje(data[0].Status, data[0].Message);
                    document.getElementById('txtSerie').disabled = true;
                    document.getElementById("btnGuardarCrt").disabled = false;
                    document.execCommand('ClearAuthenticationCache');
                    location.reload(); 
                    ActualizarTablaDetalle();
                    $('.piluku-preloader').addClass('hidden');
                },
                error: function(result) {
                    $('.piluku-preloader').addClass('hidden');
                }
            });
            document.getElementById("param0").value = "";
            
        }
    }

    function ActualizarTablaDetalle() {
        //fnDestroyDataTableDetalle('tbActivoFijo');
        //$('.details').hide().removeClass('show');        
        llenarFormulario($('#param1').val(), "P");
        llenarFormulario($('#param1').val(), "DP");
        //fnResetDataTableTramite('tbActivoFijo', 0, 'desc');
        setTimeout("-", 10000); 
        location.reload();
    }

    function llenarFormulario(codigo, param) {

        $.ajax({
            type: "GET",
            contentType: "application/json; charset=utf-8",
            url: "../../DataJson/activofijo/processactivofijo.aspx",
            data: { "param0": "lstRegistroActivo", "param1": codigo, "tipo": param, "param2": $('#param2').val(), "hdIEgreso": $('#hdIEgreso').val() },
            dataType: "json",
            async: false,
            success: function(data) {
                //alert(data.length);
                var t = '';
                var conta = 0;
                var contaA = 0;
                var detalle = 0;
                if (param == "P") {
                    aDataP = data;
                    for (i = 0; i < aDataP.length; i++) {
                        $('#txtNroPedido').val(data[i].c_ped);
                        $('#txtEntregado').val(data[i].d_per);
                        //$('#txtNroSerie').val(data[i].d_alm);
                        //$('#txtCco').val(data[i].d_cco);
                    }
                } else {
                    aDataDP = data;

                    //detalle = $('#param3').val();
                    //alert("Param: " + $('#param3').val());
                    //console.log("Cant: " & $('#param3').val());
                    //console.log("Det: " & detalle);
                    //if (articulos.length = cantidades.length) {
                    //alert("P2: "+$('input#param2').val());
                    //alert("P3: "+$('input#param3').val());
                    articulos = $('input#param2').val().split(",");
                    cantidades = $('input#param3').val().split(",");
                    //alert(aDataDP.length);
                    for (var i = 0; i < aDataDP.length; i++) {
                        conta += 1;
                        //t += '<tr role="row" id="' + data[i].d_cant + ',' + i + '">'; 
                        t += '<tr role="row" id="' + data[i].d_filas + ',' + i + ',' + data[i].c_egr + '">';
                        t += '<td>' + conta + '</td>';
                        //t += '<td>' + data[i].d_cant + '</td>';
                        t += '<td>' + data[i].d_filas + '</td>';
                        t += '<td>' + data[i].d_art + '</td>';
                        t += '<td>' + data[i].d_est + '</td>';
                        t += '</tr>';
                    }
                    //}
                    
                    fnDestroyDataTableDetalle('tbActivoFijo');
                    $('#pActivoFijo').html(t);
                }
            },
            error: function(result) {
                console.log(result);
            }
        });

        
    }


    function fnRegistrarSerie(fila, c_det, activo, serie) {
        //alert("fila: " + fila);
        //alert("c_det: " + c_det)
        //alert("activo: " + activo);

        //alert("F: " + fila + " D:" + c_det + " A:" + activo);

        //alert(aDataDP[c_det].c_art);

        $('#txtSerie').val(serie);
        
        $("input#param0").val("LstDetalleRegistroAF");
        var form = $('#frmRegistroActivo').serialize();
        $.ajax({
            type: "GET",
            contentType: "application/json; charset=utf-8",
            url: "../../DataJson/activofijo/processactivofijo.aspx",
            data: { "param0": "LstDetalleRegistroAF", "param1": $('#txtNroPedido').val(), "param2": $('#param2').val(), "hdIEgreso": $('#hdIEgreso').val() },
            dataType: "json",
            success: function(data) {
                aDataDR = data;
            },
            error: function(result) {
                console.log(result);
            }
        });

        egresos = $('#hdIEgreso').val().split(",");
        //alert(egresos[0] + "-" + egresos[1]);
        //alert("Egresiiii:"+$('input#hdIEgreso').val());
        $('#hdIdEgresoS').val(egresos[c_det]);
        //alert("IDEA: " + $('#hdIdEgresoS').val());
        
        if (activo == 0) {
            //alert("0000000000000");
            $('#hdSerie').val("0");
            $('#txtSerie').val("");
            document.getElementById('txtSerie').disabled = false;
            
            document.getElementById('hdFila').value = fila;
            $('#hdNumeroEs').val(fila + 1);
            document.getElementById('hdDetalle').value = c_det;
            $('#txtCodArt').val(aDataDP[c_det].d_art);
            
        }
        else {

            $('#hdSerie').val(aDataDR[fila + c_det].c_ser);

            //$('#txtSerie').val(aDataDR[fila + c_det].d_ser);
            document.getElementById('txtSerie').disabled = true;

            document.getElementById('hdFila').value = c_det;
            $('#hdNumeroEs').val(fila + 1);
            document.getElementById('hdDetalle').value = c_det;
            $('#txtCodArt').val(aDataDP[c_det].d_art);
        }

        //alert(aDataDP[c_det].c_art);
        //alert("filaaaaaaaaa:" + fila);
        //alert("c_detttttttt:" + c_det);
        //alert($('#hdSerie').val());
        //alert(fila + c_det + 1);
        //alert(aDataDR[fila].d_ser);
        //alert($('#txtSerie').val());

        mostrarArticuloComponente(aDataDP[c_det].c_art, fila, c_det, $('#hdSerie').val(), $('#txtSerie').val());
        
        $('div#mdRegistroSerieAF').modal('show');
        return true;
    }

    function mostrarArticuloComponente(c_art, pos1, pos, hserie, txtserie) {
        //alert(aDataDP[pos].c_art);
        //alert(aDataDP[pos].d_cant);
        //alert("Mooooostrar articulo componente:" + c_art);
        
        //alert("pos   :" + pos);
        //alert("pos1  :" + pos1);
        
        var cont = pos1 + 1;
        //alert(hserie);
        //alert($('#txtCSerie\\[' + pos + '\\]').val() + $('#txtCSerie\\[' + pos1 + '\\]').val());
        //alert($('#txtNroComp\\[' + pos + '\\]').val() + $('#txtNroComp\\[' + pos1 + '\\]').val());
        if (hserie==0) {
            document.getElementById('txtSerie').disabled = false;
            $('#txtSerie').val("");
            document.getElementById("btnGuardarCrt").disabled = true;
            document.getElementById('divBtnActualizaSerie').style.display = 'block';
        }else{

            if ($('#txtCSerie\\[' + cont + '\\]').val() == '-' && $('#txtNroComp\\[' + cont + '\\]').val() > 0) {
                //alert("uno");
                document.getElementById('txtSerie').disabled = false;
                $('#txtSerie').val("");
                document.getElementById("btnGuardarCrt").disabled = true;            
                document.getElementById('divBtnActualizaSerie').style.display = 'block';
            } else {
            if ($('#txtCSerie\\[' + cont + '\\]').val() == '-' && $('#txtNroComp\\[' + cont + '\\]').val() == '-') {
                //alert("dos");
                    $('#txtSerie').val("");
                    document.getElementById("btnGuardarCrt").disabled = true;
                    document.getElementById('txtSerie').disabled = false;
                    document.getElementById('divBtnActualizaSerie').style.display = 'block';
                } else {
                    if ($('#txtCSerie\\[' + cont + '\\]').val() == '-' && $('#txtNroComp\\[' + cont + '\\]').val() == 0) {
                        //alert("tres");
                        $('#txtSerie').val("");
                        document.getElementById("btnGuardarCrt").disabled = true;
                        document.getElementById('txtSerie').disabled = false;
                        document.getElementById('divBtnActualizaSerie').style.display = 'block';
                    } else {
                    //alert("cuatro");
                        document.getElementById("btnGuardarCrt").disabled = false;
                        document.getElementById('txtSerie').disabled = true;
                        document.getElementById('divBtnActualizaSerie').style.display = 'none';
                    }
                    
                }      

            }
            
        }

        //alert("serie: " + $('#txtSerie').val());
        $('#hdSerieB').val($('#txtSerie').val());
        //alert("hdSerieB"+$('#hdSerieB').val());
        document.getElementById('divArtCom').style.display = 'block';
        document.getElementById('divCrtCom').style.display = 'none';
        $('#hdArt1').val(c_art);
        $('#hdArt').val(c_art);
        
        $("input#param0").val("gLstArticuloComponentesRegAF");
        var form = $('#frmRegistroActivo').serialize();
        $.ajax({
            type: "GET",
            contentType: "application/json; charset=utf-8",
            url: "../../DataJson/activofijo/processactivofijo.aspx",
            data: form,
            dataType: "json",
            success: function(data) {
                //console.log("--------");
                //console.log(data);
                aDataAC = data;
                var contaAC = 0;
                var t = '';
                if (data.length > 0) {
                    for (var i = 0; i < aDataAC.length; i++) {
                        contaAC = i + 1;
                        t += '<tr>';
                        t += '<td><a href="#" class="btn btn-green btn-xs" onclick="fnCrtComp(\'' + aDataAC[i].c_hart + '\')" ><i class="ion-edit"></i></a></td>';
                        if (aDataAC[i].c_chk == "1") {
                            t += '<td><input type="checkbox" name="chkAC[' + i + ']" value="' + 1 + '" checked/>';
                        } else {
                            t += '<td><input type="checkbox" name="chkAC[' + i + ']" value="' + 0 + '" class="mark-complete"/>';
                        }
                        t += '<input type="hidden" id="txtComp[' + i + ']" name="txtComp[' + i + ']" value="' + aDataAC[i].c_hart + '" /></td>';
                        t += '<td>' + contaAC + '</td>';
                        //t += '<td>' + aDataAC[i].c_dar + '</td>';
                        t += '<td>' + aDataAC[i].d_com + '</td>';
                        t += '</tr>';
                    }
                }
                fnDestroyDataTableDetalle('tbArtCom');
                $('#pArtCom').html(t);
                fnResetDataTableTramite('tbArtCom', 0, 'desc');
            },
            error: function(result) {
                $('.piluku-preloader').addClass('hidden');
            }
        });
        document.getElementById("param0").value = "";
    
    }


    function fnCrtComp(c_art) {
        $.ajax({
            type: "GET",
            contentType: "application/json; charset=utf-8",
            url: "../../DataJson/activofijo/processactivofijo.aspx",
            data: { "param0": "lstRegistroActivo", "param1": $('#param1').val(), "tipo": "DP", "param2": $('#param2').val() },
            dataType: "json",
            async: false,
            success: function(data) {
                aDataDP = data;
            },
            error: function(result) {
                console.log(result);
            }
        });


        document.getElementById('divArtCom').style.display = 'none';
        document.getElementById('divCrtCom').style.display = 'block';
        $('#hdArt').val(c_art);
//        alert("mostrar CART componente:" + c_art);
        $("input#param0").val("gLstArtCrtRegAF");
        //$("input#param0").val("gLstArtCrt");
        //alert($('#hdFila').val());
        var form = $('#frmRegistroActivo').serialize();
        $.ajax({
            type: "GET",
            contentType: "application/json; charset=utf-8",
            url: "../../DataJson/activofijo/processactivofijo.aspx",
            data: form,
            dataType: "json",
            success: function(data) {
                aDataCC = data;
                var contaCC = 0;
                var contaCCN = 0;
                var t = '';
                if (data.length > 0) {
                    for (var i = 0; i < aDataCC.length; i++) {
                        contaCC = i + 1;
                        $('#txtComponente').val(aDataCC[i].d_art);
                        //$('#txtDatos').val(aDataDR[$('#hdFila').val()].c_ser);
                        var nuevo = $('#hdFila').val();

                        //$('#hdCom').val(aDataCC[i].c_art);
                        //$('#hdCantCC').val(aDataCC.length);
                        if (aDataCC[i].c_chk == "1") {

                            //$('#hdUnid').val(aDataDP[nuevo].d_cant); 

                            $('#hdAccion').val("REG");
                            contaCCN++;
                            t += '<tr>';
                            if (aDataCC[i].c_chk == "1" && aDataCC[i].d_val != "X") {
                                CChk++;
                                t += '<td><input type="checkbox" id="chkCC[' + contaCCN + ']" name="chkCC[' + contaCCN + ']" value="' + aDataCC[i].c_idO + '" checked/>';
                            } else {
                                t += '<td><input type="checkbox" id="chkCC[' + contaCCN + ']" name="chkCC[' + contaCCN + ']" value="' + aDataCC[i].c_idO + '" />';
                            }
                            t += '<input type="hidden" id="txtIdCAC[' + contaCCN + ']" name="txtIdCAC[' + contaCCN + ']" value="' + aDataCC[i].c_idO + '" />';
                            t += '<input type="hidden" id="txtIdDAF[' + contaCCN + ']" name="txtIdDAF[' + contaCCN + ']" value="' + aDataCC[i].c_daf + '" />';
                            t += '</td>';
                            t += '<td>' + aDataCC[i].c_idO + '</td>';
                            t += '<td>' + aDataCC[i].d_crt + '</td>';
                            if (aDataCC[i].d_val != "X") {
                                Ctxt++;
                                t += '<td>' + '<input name="txtValorC[' + contaCCN + ']" type="text" id="txtValorC[' + contaCCN + ']" value="' + aDataCC[i].d_val + '" width="100%" onblur="focusText(' + contaCCN + ')"/>' + '</td>';
                            } else {
                                t += '<td>' + '<input name="txtValorC[' + contaCCN + ']" type="text" id="txtValorC[' + contaCCN + ']" value="" width="100%" onblur="focusText(' + contaCCN + ')"/>' + '</td>';
                            }
                            t += '</tr>';
                        }
                    }
                }
                $('#hdCantCC').val(contaCCN);
                //alert("CCN:"+contaCCN);
                fnDestroyDataTableDetalle('tbCrtCom');
                $('#pCrtCom').html(t);
                fnResetDataTableTramite('tbCrtCom', 0, 'asc');
            },
            error: function(result) {
                $('.piluku-preloader').addClass('hidden');
            }
        });
        document.getElementById("param0").value = "";
    }

    function focusText(index) {        
        //alert(nrofilas);
        if (document.getElementById('txtValorC[' + index + ']').value != "") {            
            document.getElementById('chkCC[' + index + ']').checked = true;
        } else {            
            document.getElementById('chkCC[' + index + ']').checked = false;
        }
        //$('#hdCantCC').val(nrofilas);
        
    }   

    function fnRegresar() {
        document.getElementById('divArtCom').style.display = 'block';
        document.getElementById('divCrtCom').style.display = 'none';
    }

    function fnGuardarDetalleActivoFijo() {
        var N = $('#hdCantCC').val();
        //alert("N: " + N);
        var sw = 0;
        var a = 0;
        var detalleIdCCO = "";
        var detalleValorCCO = "";

        var vckd = 0;
        var vtxt = 0;
        
        for (i = 1; i <= N; i++) {
            if ($('#chkCC\\[' + i + '\\]').is(':checked')) {
                //alert("CHK");
                vckd++;
            }
            if ($('#txtValorC\\[' + i + '\\]').val() != "") {
                //alert("VALO");    
                vtxt++;
            }
        }
        if ($('#txtSerie').val() == "") {
            alert("Ingrese Serie");
        } else { 
            if (vckd == 0 && vtxt == 0) {
                //alert("Ingrese Valores");
                sw = 1;
                if (CChk == 0 && Ctxt == 0) {
                    sw = 1;
                    //alert("Ingrese Valores: CHK: " + vckd + " TXT:" + vtxt);
                } else {
                    sw = 0;
                }
            }
            if (sw == 1) {
                alert("Ingrese Valores: CHK: " + vckd + " TXT:" + vtxt);
            } else { 
                if (vckd != vtxt) {
                    //alert("Validar selección con valores ingresados");
                    alert("Validar check con valores: " + vckd + "-" + vtxt);
                } else {
                    //alert(N);
                    for (i = 1; i <= N; i++) {
                        if ($('#chkCC\\[' + i + '\\]').is(':checked')) {
                            if ($('#txtValorC\\[' + i + '\\]').val() != "") {
                                //alert("Valor: "+$('#txtValorC\\[' + i + '\\]').val());
                                if (a == 1) {
                                    detalleIdCCO = detalleIdCCO + ",";
                                    detalleValorCCO = detalleValorCCO + ",";
                                }
                                a = 1;
                                detalleIdCCO = detalleIdCCO + $('#txtIdCAC\\[' + i + '\\]').val();
                                detalleValorCCO = detalleValorCCO + $('#txtValorC\\[' + i + '\\]').val();
                            }
                        }
                    }
                    $('#hdDetalleIdCCO').val(detalleIdCCO);
                    $('#hdDetalleValorCCO').val(detalleValorCCO);
                    //alert("ID: " + $('#hdDetalleIdCCO').val()); //33
                    //alert("Valor: " + $('#hdDetalleValorCCO').val()); // rojo
                    //alert("C PED: " + $('#txtNroPedido').val()); // 108299
                    //alert("Articulo: " + $('#hdArt1').val()); // 44604
                    //alert("Componente: " + $('#hdArt').val()); //38956
                    //alert("Unidades: " + $('#hdUnid').val()); // vaciooooo
                    //alert("NumeroEs: " + $('#hdNumeroEs').val()); // 1
                    //alert("IdEgreso: " + $('#hdIdEgresoS').val()); // 1092093,1092094
                    //alert("hdserie: " + $('#hdSerie').val()); // 3288
                    //alert("txtserie: " + $('#txtSerie').val()); //cuaderno 1
                    //alert("hdDetalleIdCCO: " + $('#hdDetalleIdCCO').val()); //33
                    //alert("hdDetalleValorCCO: " + $('#hdDetalleValorCCO').val()); //rojo
                    //alert("hdAccion: " + $('#hdAccion').val()); //REG
                    $('#hdSerie').val($('#txtSerie').val());
                    
                    
                    //$('#hdIEgreso').val(0); //Se envia desde el VB
                    

                    $("input#param0").val("gRegistrarDetalleAF");
                    //$("input#param0").val("gLstArtCrt");

                    var boton = document.getElementById("btnGuardarCrt");
                    boton.innerHTML = "Procesando"
                    boton.disabled
                     = true;
                    var form = $('#frmRegistroActivo').serialize();
                    $.ajax({
                        type: "GET",
                        contentType: "application/json; charset=utf-8",
                        url: "../../DataJson/activofijo/processactivofijo.aspx",
                        data: form,
                        dataType: "json",
                        success: function(data) {
                            console.log("--------------");
                            console.log(data);
                            //fnMensaje("success", data[0].Message);
                            if (data[0].Status == 'error') {
                                //fnMensaje(data[0].Status, data[0].Message + ": " + data[0].Code);
                                alert('ERROR: ' + data[0].Message + '-' + data[0].Code);
                            } else {
                                //fnMensaje(data[0].Status, data[0].Message);
                                alert(data[0].Message);
                                document.execCommand('ClearAuthenticationCache');
                                location.reload(); 
                            }
                            //$('.piluku-preloader').addClass('hidden');
                            //f_Menu("listacaracteristicas.aspx");
                            //actualizarTabla();
                        },
                        error: function(result) {
                            $('.piluku-preloader').addClass('hidden');
                        }
                    });
                    document.getElementById("param0").value = "";
                    
                }
            }

        }
    }

    function fnComponentes(fila, c_det, egr) {
        $('#hdIdEgresoS').val(egr);
        $('#hdProceso').val("5");
        alert(egr);

        alert("Id Egreso: " + $('#hdIEgreso').val());
        alert("Cod Art: " + $('#hdArt').val());
        alert("Unid: " + $('#hdUnid').val());
        alert("Serie: " + $('#hdSerie').val());
        alert("Cod Pedido: " + $('#txtNroPedido').val());
        alert("Cant: " + $('#hdCantCC').val());
        alert("fila: " + fila);
        alert("det: " + c_det);  
        
        if ($('#hdProceso').val() == "0") {

            // alert("Id Egreso: " + $('#hdIEgreso').val());
            // alert("Cod Art: " + $('#hdArt').val());
            // alert("Unid: " + $('#hdUnid').val());
            //alert("Serie: " + $('#hdSerie').val());
            // alert("Cod Pedido: " + $('#txtNroPedido').val());
            // alert("Cant: " + $('#hdCantCC').val());

            // alert("fila: " + fila);
            // alert("det: " + c_det);  
            
            
            
            //$('#hdIEgreso').val("0");
            $('#hdArt').val(aDataDP[c_det].c_art);
            $('#hdUnid').val("1");
            $('#hdSerie').val("-");
            $('#hdCantCC').val(aDataDP[c_det].d_cant);
            $('#hdAccion').val("CLO");        
            
            $("input#param0").val("gClonarDetalleAF");
            var form = $('#frmRegistroActivo').serialize();
            $.ajax({
                type: "GET",
                contentType: "application/json; charset=utf-8",
                url: "../../DataJson/activofijo/processactivofijo.aspx",
                data: form,
                dataType: "json",
                success: function(data) {
                    $('#hdProceso').val("1");
                    if (data[0].Status == 'error') {
                        alert('ERROR: ' + data[0].Message);
                        location.reload();
                    } else {
                        alert(data[0].Message);
                        document.execCommand('ClearAuthenticationCache');
                        location.reload();                    
                    }
                    $('.piluku-preloader').addClass('hidden');
                },
                error: function(result) {
                    $('.piluku-preloader').addClass('hidden');
                }
            });
            $('#hdProceso').val("0");
            document.getElementById("param0").value = "";
        
        }
       
    }
    
</script>
<body>
    
<form id="frmRegistroActivo" name="frmRegistroActivo" action="#"  > 
    <input type="hidden" id="param0" name="param0" value="" />  
    <input type="hidden" id="param1" name="param1" value="" runat="server" />
    <input type="hidden" id="param2" name="param2" value="" runat="server" />
    <input type="hidden" id="param3" name="param3" value="" runat="server" />
    <input type="hidden" id="hdFila" name="hdFila" value="" runat="server" />
    <input type="hidden" id="hdDetalle" name="hdDetalle" value="" runat="server" />
    <input type="hidden" id="hdArt" name="hdArt" value="" runat="server" />
    <input type="hidden" id="hdArt1" name="hdArt1" value="" runat="server" />
    <input type="hidden" id="hdCom" name="hdCom" value="" runat="server" />
    <input type="hidden" id="hdCantCC" name="hdCantCC" value="" runat="server" />
    <input type="hidden" id="hdDetalleIdCCO" name="hdDetalleIdCCO" value="" runat="server" />
    <input type="hidden" id="hdDetalleValorCCO" name="hdDetalleValorCCO" value="" runat="server" />
    <input type="hidden" id="hdUnid" name="hdUnid" value="" runat="server" />
    <input type="hidden" id="hdNumeroEs" name="hdNumeroEs" value="" runat="server" />
    <input type="hidden" id="hdAccion" name="hdAccion" value="" runat="server" />
    <input type="hidden" id="hdSerie" name="hdSerie" value="" runat="server" />
    <input type="hidden" id="hdIEgreso" name="hdIEgreso" value="" runat="server" />
    <input type="hidden" id="hdProceso" name="hdProceso" value="" runat="server" />
    <input type="hidden" id="hdTotalCant" name="hdTotalCant" value="" runat="server" />
    <input type="hidden" id="hdIdEgresoS" name="hdIdEgresoS" value="" runat="server" />
    <input type="hidden" id="hdSerieB" name="hdSerieB" value="" runat="server" />
    <input type="hidden" id="hdCClonar" name="hdCClonar" value="" runat="server" />
    
    
<div class="col-md-7" >
    <div class="panel panel-piluku">
        <div class="panel-heading">
                <h3 class="panel-title">
                    Registro de Activo
                </h3>
        </div>                            																						
	    <div class="panel-body">								
	        <div class="col-md-12">
	            
                <div class="row">			            
                <div class="form-group">
                    <label class="col-md-2 control-label">Nro. Pedido:</label>
	                <div class="col-md-4">			        
                        <input name="txtNroPedido" type="text" id="txtNroPedido" value="" class="form-control" placeholder="Buscar Pedido"/>				       
	                </div>	  
	                <div class="col-md-1">					        
			            <div class="diverror" id="error[0]" style="visibility:hidden"><p>(*)</p></div>
                    </div>      
                </div>
                </div> 
                <div class="row">			            
                <div class="form-group">
                    <label class="col-md-2 control-label">Entregado a:</label>
	                <div class="col-md-7">			        
                        <input name="txtEntregado" type="text" id="txtEntregado" value="" class="form-control" placeholder="Buscar Pedido"/>				       
	                </div>	  
	                <div class="col-md-1">					        
			            <div class="diverror" id="error[1]" style="visibility:hidden"><p>(*)</p></div>
                    </div>      
                </div>
                </div> 
                <!--<div class="row">			            
                <div class="form-group">
                    <label class="col-md-2 control-label">Centro Costo:</label>
	                <div class="col-md-7">			        
                        <input name="txtCco" type="text" id="txtCco" value="" class="form-control" placeholder="Nro Serie"/>				       
	                </div>	  
	                <div class="col-md-1">					        
			            <div class="diverror" id="Div2" style="visibility:hidden"><p>(*)</p></div>
                    </div>      
                </div>
                </div> 
                <div class="row">			            
                <div class="form-group">
                    <label class="col-md-2 control-label">Nro. Serie:</label>
	                <div class="col-md-4">			        
                        <input name="txtNroSerie" type="text" id="txtNroSerie" value="" class="form-control" placeholder="Nro Serie"/>				       
	                </div>	  
	                <div class="col-md-1">					        
			            <div class="diverror" id="error[2]" style="visibility:hidden"><p>(*)</p></div>
                    </div>      
                </div>
                </div> -->
                
                <br />

                
                <!--<div class="row">			            
                <div class="form-group">
                    <label class="col-md-2 control-label">Nro. Componente:</label>
	                <div class="col-md-7">			        
                        <input name="txtNroComponentes" type="text" id="txtNroComponentes" value="" class="form-control" placeholder="Nro Componente"/>				       
	                </div>	  
	                <div class="col-md-1">					        
			            <div class="diverror" id="Div1" style="visibility:hidden"><p>(*)</p></div>
                    </div>      
                </div>
                </div>  -->                 
                <br />
                <div class="row">			            
                <div class="form-group">
                    <script type="text/javascript" src='assets/js/datatables/activofijo.js'></script>
                    <table class='display dataTable cell-border' id='tbActivoFijo' style="width:100%;font-size:smaller;">
                        <thead>
                        <tr>                             
                             <th style="width:10%;">ITEM</th>                             
                             <th style="width:10%;">CANT</th>
                             <th style="width:50%;">ARTICULO</th>
                             <th style="width:10%;">ESTADO</th>                             
                         </tr>
                         </thead>     
                         <tbody id ="pActivoFijo" runat ="server">
                         </tbody>                             
                           <tfoot>
                            <tr>
                            <th colspan="6"></th>
                            </tr>
                            </tfoot>
                    </table>
                </div>
                </div>           
                                                     
            </div>
        </div>
        
    </div>	
</div>	


<div class="modal fade" id="mdRegistroSerieAF" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" data-backdrop="static" data-keyboard="false" style="z-index:0;"> 
<div class="modal-dialog">
	<div class="modal-content">
		<div class="modal-header" style="background-color:#E33439;" >
			<button type="button" class="close" data-dismiss="modal" aria-label="Close" style="color:White;"><span aria-hidden="true" class="ti-close" style="color:White;"></span></button>
			<h4 class="modal-title"  style="color:White">Registrar/Actualizar Serie Activo Fijo</h4>
		</div>
		<div class="modal-body">
	        <div class="row">
	            <div class="col-md-12" id="divConfirmar">
	            
			        <div class="row">
                    <div class="form-group">
				        <label class="col-md-4 control-label">Serie:</label>
				        <div class="col-md-7">
					        <input name="txtSerie" type="text" id="txtSerie" value="" class="form-control" runat="server"/>
				        </div>
                        <div class="col-md-1">					        
					        <div class="diverror" id="Div3" style="visibility:hidden"><p>(*)</p></div>
				        </div>
			        </div>
			        </div>	
			        
                    <div class="row">
                    <div class="form-group">
				        <label class="col-md-4 control-label">Articulo:</label>
				        <div class="col-md-7">
					        <input name="txtCodArt" type="text" id="txtCodArt" value="" class="form-control" disabled="disabled"/>
				        </div>
                        <div class="col-md-1">					        
					        <div class="diverror" id="Div4" style="visibility:hidden"><p>(*)</p></div>
				        </div>
			        </div>
			        </div>
			        
                    <div class="row" id="divBtnActualizaSerie" style='display:none;'>
                    <div class="form-group">				        
                        <center>
                            <div class="btn-group">			      
                                <button type="button" class="btn btn-primary" id="btnActualuzarSerie"><i class="ion-android-done"></i>&nbsp;Actualizar Serie</button>		
                            </div>
                        </center>
			        </div>
			        </div>
			        
<%--			        <div class="row">
                    <div class="form-group">
				        <label class="col-md-4 control-label">DATOS:</label>
				        <div class="col-md-7">
					        <input name="" type="text" id="txtDatos" value="" class="form-control" disabled="disabled"/>
				        </div>
                        <div class="col-md-1">					        
					        <div class="diverror" id="Div5" style="visibility:hidden"><p>(*)</p></div>
				        </div>
			        </div>
			        </div>--%>
			        
			        	
			        <br />
			        <div class="modal-footer"></div>
			        
			        <div id="divArtCom">
                        <table class='display dataTable cell-border' id='tbArtCom' width="100%" style="font-size:smaller;">
                            <thead>
                            <tr>              
                                 <th style="width:5%;text-align:center;"></th>               
                                 <th style="width:5%;"></th>
                                 <th style="width:10%;">ITEM</th>
                                 <th style="width:80%;">COMPONENTE</th>                             
                             </tr>
                             </thead>     
                             <tbody id ="pArtCom" runat ="server">
                             </tbody>                             
                               <tfoot>
                                <tr>
                                <th colspan="4"></th>
                                </tr>
                               </tfoot>
                        </table>
                        
                        <div class="modal-footer">
                            <center>
                              <div class="btn-group">			      
                                    <!--<button type="button" class="btn btn-primary" id="btnGuardarRegAF" ><i class="ion-android-done"></i>&nbsp;Guardar</button>	-->
                                    <button type="button" class="btn btn-danger" data-dismiss="modal"><i class="ion-android-cancel"></i>&nbsp;Cancelar</button>		
                               </div>
                            </center>
                        </div>
                    
                    </div>			        			        
			        
                    <div id='divCrtCom' style='display:none;'>
                        <div class="row">
                        <div class="form-group">
				            <label class="col-md-4 control-label">Componente:</label>
				            <div class="col-md-7">
					            <input name="txtComponente" type="text" id="txtComponente" value="" class="form-control" disabled="disabled" />
				            </div>                            
			            </div>
			            </div>
                        <br />
                        <table class='display dataTable cell-border' id='tbCrtCom' width="100%" style="font-size:smaller;">
                            <thead>
                            <tr>              
                                 <!--<th style="width:5%;text-align:center;"></th>-->
                                 <th style="width:5%;"></th>         
                                 <th style="width:10%;">ITEM</th>
                                 <th style="width:40%;">CARACTERISTICA</th>
                                 <th style="width:40%;">VALOR</th>
                             </tr>
                             </thead>     
                             <tbody id ="pCrtCom" runat ="server">
                             </tbody>                             
                               <tfoot>
                                <tr>
                                <th colspan="5"></th>
                                </tr>
                               </tfoot>
                        </table>
                        
                        <br />
                        
                		<div class="modal-footer">
                            <center>
                              <div class="btn-group">			      
                                    <button type="button" class="btn btn-info btn-radius" id="btnGuardarCrt" ><i class="ion-android-done"></i>&nbsp;Guardar Crt</button>
                                    <button type="button" class="btn btn-default btn-radius" id="btnRegresar"><i class="ion-android-cancel"></i>&nbsp;Regresar</button>		
                               </div>
                            </center>
                        </div>
			        		        				        	        			            		                 
	                </div>	
	                

                        	
	                <br />
	        </div>
	        
	        
	        
		</div>
		

		</div>
	</div>
</div>
</div>


<div class="modal fade" id="mdDelRegistro" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" data-backdrop="static" data-keyboard="false" style="z-index: 5;"> 
<div class="modal-dialog">
	<div class="modal-content">
		<div class="modal-header" style="background-color:#E33439;" >
			<button type="button" class="close" data-dismiss="modal" aria-label="Close" style="color:White;"><span aria-hidden="true" class="ti-close" style="color:White;"></span></button>
			<h4 class="modal-title"  style="color:White">Confirmar Operaci&oacute;n</h4>
		</div>
		<div class="modal-body">
            <div class="row">
	            <div class="col-md-12" id="">
	                <label class="col-md-12 control-label">Desea Confirmar la Eliminaci&oacute;n del Registro</label>
	            </div>
            </div>
	            
		</div>		
		<div class="modal-footer">
		  <center>
		      <div class="btn-group">			      
		            <button type="button" class="btn btn-primary" id="btnDelReg" ><i class="ion-android-done"></i>&nbsp;Eliminar</button>	
		            <button type="button" class="btn btn-danger" data-dismiss="modal"><i class="ion-android-cancel"></i>&nbsp;Cancelar</button>		
		       </div>
		  </center>
		</div>
	</div>
</div>
</div>


</form>
</body>
</html>




