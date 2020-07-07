<%@ Page Language="VB" AutoEventWireup="false" CodeFile="trasladoMercaderia_v2.aspx.vb" Inherits="administrativo_activofijo_trasladoMercaderia_v2" %>
<html id="Html1" lang="en" runat="server">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1">
    <title>Traslado de Activo Fijo</title>
    
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

</head>
<script type="text/javascript">
    var lstPer;
    var codigo_per;
    var nombre_per;
    var lstCco;
    var codigo_cco;
    var nombre_cco;
    var lstArt;
    var codigo_art;
    var nombre_art = "";
    
    //20180813ENEVADO - Declaracion Variables
    var lstUbi;
    var codigo_ubi;
    var nombre_ubi;
    var codigo_asi;
    var nombre_asi;
    var lstContenido = [];
    var codigo_cen;
    var nombre_cen;
    var codcen_aux;
    var nomcen_aux;
    var email_aux;
    var eti_art;
    var des_art;
    var mod_art;
    var mar_art;
    var ser_art;
    var email_per;
    //---------------------------------------

    jQuery(document).ready(function() {
        fnResetDataTableTramite('tbTraslado', 0, 'desc');
        fnResetDataTableTramite('tbDetTraslado', 0, 'desc');
        document.getElementById('divOcultoTraslado').style.display = 'block';
        $("#btnDelReg").click(fnDelRegistro);
        $("#btnConfirmar").click(fnGuardarTraslado);
        $("#btnBuscarTraslado").click(fnBuscarTraslado);
        $("#btnCancelarDetT").click(fnCancelarDetTraslado);
        $("#btnAgregarDetTraslado").click(fnAgregarDetTrasladoV2);
        //$("#btnPdf").click(fnGenerarPdf);

        var Fecha = new Date();
        var Mes = Fecha.getMonth() + 1;
        var valFecha = Fecha.getDate() + "/" + Mes + "/" + Fecha.getFullYear();
        $("#txtFechaIni").val(valFecha);
        $("#txtFechaFin").val(valFecha);

        fc_HabilitarControles(true);

        $('#btnAgregarTraslado').click(function() {
            limpia();
            document.getElementById('divOcultoTraslado').style.display = 'none';
            document.getElementById('divOcultoDetTraslado').style.display = 'block';
            fc_HabilitarControles(false);
            $("#btnAgregarDetTraslado").removeAttr("disabled");
            $("#btnConfirmar").removeAttr("disabled");
            //$('div#mdTraslado').modal('show');
        });

        lstCco = fnCargaLista("LstCentroCo");
        var jsonStringC = JSON.parse(lstCco);

        $('#txtCentroCo').autocomplete({
            source: $.map(jsonStringC, function(item) {
                return item.d_des;
            }),
            select: function(event, ui) {
                var selectecItem = jsonStringC.filter(function(value) {
                    return value.d_des == ui.item.value;
                });
                codigo_cco = selectecItem[0].d_id;
                nombre_cco = selectecItem[0].d_des;
                $('#hdCco').val(codigo_cco);
                //alert("cod: " + selectecItem[0].d_id + ", nombre: " + selectecItem[0].d_des);
            },
            minLength: 3,
            delay: 100
        });

        $('#txtCentroCo').keyup(function() {
            var l = parseInt($(this).val().length);
            if (l == 0) {
                codigo_cco = "";
                nombre_cco = "";
            }

        });



        lstPer = fnCargaLista("lstPersonal");
        var jsonStringP = JSON.parse(lstPer);

        $('#txtPersonal').autocomplete({
            source: $.map(jsonStringP, function(item) {
                return item.d_des;
            }),
            select: function(event, ui) {
                var selectecItem = jsonStringP.filter(function(value) {
                    return value.d_des == ui.item.value;
                });
                codigo_per = selectecItem[0].d_id;
                nombre_per = selectecItem[0].d_des;
                email_per = selectecItem[0].u_des;
                $('#hdPer').val(codigo_per);
                $('#hdEmailUser').val(email_per + '@usat.edu.pe');
                //alert("cod: " + selectecItem[0].d_id + ", nombre: " + selectecItem[0].d_des);
            },
            minLength: 3,
            delay: 100
        });

        $('#txtPersonal').keyup(function() {
            var l = parseInt($(this).val().length);
            if (l == 0) {
                codigo_per = "";
                nombre_per = "";
                email_per = "";
            }
        });

        //lstArt = fnCargaLista("lstDetallePedidoAlmDes");
        lstArt = fnCargaLista("lstActivoFijo");
        var jsonStringA = JSON.parse(lstArt);

        $('#txtArticulo').autocomplete({
            source: $.map(jsonStringA, function(item) {
                return item.d_des;
            }),
            select: function(event, ui) {
                var selectecItem = jsonStringA.filter(function(value) {
                    return value.d_des == ui.item.value;
                });
                codigo_art = selectecItem[0].d_id;
                nombre_art = selectecItem[0].d_des;
                eti_art = selectecItem[0].d_eti;
                des_art = selectecItem[0].d_nom;
                mod_art = selectecItem[0].d_mod;
                mar_art = selectecItem[0].d_mar;
                ser_art = selectecItem[0].d_ser;
                $('#hdArt').val(codigo_art);
                //alert("cod: " + selectecItem[0].d_id + ", nombre: " + selectecItem[0].d_des);
            },
            minLength: 3,
            delay: 100
        });

        $('#txtArticulo').keyup(function() {
            var l = parseInt($(this).val().length);
            if (l == 0) {
                codigo_art = "";
                nombre_art = "";
                eti_art = "";
                des_art = "";
                mod_art = "";
                mar_art = "";
                ser_art = "";
                $('#hdArt').val("");
            }
        });

        //20180813ENEVADO - Eventos para Ubicacion ----------------------------------
        lstUbi = fnCargaLista("lstUbicacion");
        var jsonStringU = JSON.parse(lstUbi);

        $('#txtUbicacion').autocomplete({
            source: $.map(jsonStringU, function(item) {
                return item.d_des;
            }),
            select: function(event, ui) {
                var selectedItem = jsonStringU.filter(function(value) {
                    return value.d_des == ui.item.value;
                });
                codigo_ubi = selectedItem[0].d_id;
                nombre_ubi = selectedItem[0].d_des;
                $('#hdUbi').val(codigo_ubi);
            },
            minLength: 3,
            delay: 100
        });

        $('#txtUbicacion').keyup(function() {
            var l = parseInt($(this).val().length);
            if (l == 0) {
                codigo_ubi = "";
                nombre_art = "";
            }
        });
        //----------------------------------------------------------------------------

        //20180813ENEVADO - Eventos para Asignado ------------------------------------
        $('#txtAsignado').autocomplete({
            source: $.map(jsonStringP, function(item) {
                return item.d_des;
            }),
            select: function(event, ui) {
                var selectedItem = jsonStringP.filter(function(value) {
                    return value.d_des == ui.item.value;
                });
                //console.log(selectedItem);
                codigo_asi = selectedItem[0].d_id;
                nombre_asi = selectedItem[0].d_des;
                codcen_aux = selectedItem[0].c_id;
                nomcen_aux = selectedItem[0].c_des;
                email_aux = selectedItem[0].u_des;
                $('#hdAsi').val(codigo_asi);
                $('#txtCco').val(nomcen_aux);
                $('#hdCen').val(codcen_aux);
                $('#hdEmailAsig').val(email_aux + '@usat.edu.pe;');
                //alert($('#hdEmailAsig').val());
            },
            minLength: 3,
            delay: 100
        })

        $('#txtAsignado').keyup(function() {
            var l = parseInt($(this).val().length);
            if (l == 0) {
                codigo_asi = "";
                nombre_asi = "";
                codcen_aux = "";
                nomcen_aux = "";
                email_aux = "";
            }
        })
        //-----------------------------------------------------------------------------

        //20181011ENEVADO - Eventos para Centro Costo ---------------------------------
        $('#txtCco').autocomplete({
            source: $.map(jsonStringC, function(item) {
                return item.d_des;
            }),
            select: function(event, ui) {
                var selectecItem = jsonStringC.filter(function(value) {
                    return value.d_des == ui.item.value;
                });
                codigo_cen = selectecItem[0].d_id;
                nombre_cen = selectecItem[0].d_des;
                $('#hdCen').val(codigo_cen);
                //alert("cod: " + selectecItem[0].d_id + ", nombre: " + selectecItem[0].d_des);
            },
            minLength: 3,
            delay: 100
        });

        $('#txtCco').keyup(function() {
            var l = parseInt($(this).val().length);
            if (l == 0) {
                codigo_cen = "";
                nombre_cen = "";
            }

        });
        //-----------------------------------------------------------------------------

    });

    function fnCancelarDetTraslado() {
        fnLoading(true);
        document.body.style.cursor = 'wait';
        document.getElementById('divOcultoTraslado').style.display = 'block';
        document.getElementById('divOcultoDetTraslado').style.display = 'none';
        fc_HabilitarControles(true);
        MostrarContenido($('#txtFechaIni').val(), $('#txtFechaFin').val());
        fnLoading(false);
        document.body.style.cursor = 'default';
    }
    
    
    function fnCargaLista(param) {
        try {
            var arr;
            $.ajax({
                type: "GET",
                contentType: "application/json; charset=utf-8",
                url: "../../DataJson/activofijo/processactivofijo.aspx",
                data: { "param0": param },
                async: false,
                cache: false,
                success: function(data) {
                //console.log(data)
                    arr = data;
                },
                error: function(result) {
                console.log(result)
                    arr = null;
                }
            })
            return arr;
        }
        catch (err) {
            //alert(err.message);
            console.log('error');
        }
    }

    function fnBuscarTraslado() {
        fnLoading(true);
        document.body.style.cursor = 'wait';
        var sw = 0;
        var i = 0;

        var x = $('#txtFechaIni').val().split("/");
        var z = $('#txtFechaFin').val().split("/");
        var fecha1;
        var fecha2;

        fecha1 = x[1] + "-" + x[0] + "-" + x[2];
        fecha2 = z[1] + "-" + z[0] + "-" + z[2];

        if (Date.parse(fecha1) > Date.parse(fecha2)) {
            sw = 1;
        }
        if ($('#txtFechaIni').val() == "") {
            sw = 1;
        }
        if ($('#txtFechaFin').val() == "") {
            sw = 1;
        }
        if (sw == 1) {
            fnLoading(false);
            document.body.style.cursor = 'default';
            alert("Verificar fechas");
            return false;
        }
        else {
            document.getElementById('divOcultoTraslado').style.display = 'block';
            MostrarContenido($('#txtFechaIni').val(), $('#txtFechaFin').val());
        }
        fnLoading(false);
        document.body.style.cursor = 'default';
    }

    function MostrarContenido(Fini, Ffin) {
        $("input#param0").val("gLstTrasladosMercaderia");
        var form = $('#frmTrasladoMercaderia').serialize();
        $.ajax({
            type: "GET",
            contentType: "application/json; charset=utf-8",
            url: "../../DataJson/activofijo/processactivofijo.aspx",
            data: form,
            dataType: "json",
            async: false,
            success: function(data) {
                //aData = data;
                lstContenido = data;
                //console.log("Listar Contenido:");
                //console.log(lstContenido);
                var i = 0;
                var conta = 0;
                var t = '';
                var tt = '';
                var tipo = '';
                var ctf = $('#hdPerfil').val();
                if (data.length > 0) {
                    //fnResetDataTableTramite('tbArtCrt', 0, 'desc');
                    for (var i = 0; i < data.length; i++) {
                        conta += 1;
                        t += '<tr>';
                        t += '<td>';
                        t += '<a href="#" class="btn btn-primary btn-xs" onclick="fnEditar(' + i + ')" ><i class="ion-ios-search-strong"></i></a>';
                        if (ctf==1) {
                            t += '<a href="#" class="btn btn-red btn-xs" onclick="fnBorrar(' + i + ')" ><i class="ion-android-cancel"></i></a>';   
                        }
                        t += '</td>';
                        t += '<td>' + conta + '</td>';
                        t += '<td>' + data[i].d_nro + '</td>';
                        if (data[i].d_tip == "I") {
                            tipo = "INTERNA"
                        }
                        else {
                            tipo = "EXTERNA"
                        }
                        t += '<td>' + tipo + '</td>';
                        t += '<td>' + data[i].d_nom + '</td>';
                        t += '<td>' + data[i].d_cco + '</td>';
                        t += '<td>' + data[i].d_fec + '</td>';
                        t += '<td>' + data[i].d_est + '</td>';
                        t += '</tr>';
                    }
                }
                fnDestroyDataTableDetalle('tbTraslado');
                $('#pTraslado').html(t);
                fnResetDataTableTramite('tbTraslado', 0, 'desc');
            },
            error: function(result) {
                console.log(result);
            }
        });
        document.getElementById("param0").value = "";
        //$('div#mdConfigArticulo').modal('hide');
        
    }

    function fnBorrar(i) {        
        document.getElementById('param1').value = lstContenido[i].c_id;
        $('#hdTE').val("T");
        $('div#mdDelRegistro').modal('show');
        return true;
    }

    function fnBorrarDT(cont) {
        //alert(ct)
        document.getElementById('param1').value = cont;
        //$('#hdtipoA').val("ELI"); hdTE
        $('#hdTE').val("D");
        $('div#mdDelRegistro').modal('show');
        return true;
    } 

    function fnDelRegistro() {
        if ($('#hdTE').val() == "D") {
            fnDestroyDataTableDetalle('tbDetTraslado');
            $("#fila" + $('#param1').val()).remove();
            fnResetDataTableTramite('tbDetTraslado', 0, 'desc');
        } else {
                fnDestroyDataTableDetalle('tbTraslado');
                $("input#param0").val("dEliminarTraslado");
                var form = $('#frmTrasladoMercaderia').serialize();
                $.ajax({
                    type: "GET",
                    contentType: "application/json; charset=utf-8",
                    url: "../../DataJson/activofijo/processactivofijo.aspx",
                    data: form,
                    dataType: "json",
                    success: function(data) {
                        fnMensaje(data[0].Status, data[0].Message);
                        MostrarContenido($('#txtFechaIni').val(), $('#txtFechaFin').val());
                    },
                    error: function(result) {
                        console.log(result);
                    }
                });
                fnResetDataTableTramite('tbTraslado', 0, 'desc');
        }
    
        document.getElementById("param0").value = "";
        document.getElementById("param1").value = "";
        
        $('div#mdDelRegistro').modal('hide');
        
    }

    function limpia() {
        $('#hdId').val("");
        $('#hdPer').val($('#hdUser').val());
        $('#hdCco').val($('#hdCodCcoUser').val());
        $('#txtPersonal').val($('#hdNombreUser').val());
        $('#txtCentroCo').val($('#hdNomCcoUser').val());
        $('#txtFechaTraslado').val("");
        $('#dpTipo').val("0");
        $('#dpMotivo').val("0");
        $('#txtUbicacion').val("");
        $('#txtAsignado').val("");
        $('#txtCco').val("");
        $('#txtObservacion').val(""); 
        document.getElementById("c5").checked = true;
        document.getElementById('c6').checked = false;
        $('#hdtipoA').val("G");
        
        fnDestroyDataTableDetalle('tbDetTraslado');
        $('#pDetTraslado').html('');
        fnResetDataTableTramite('tbDetTraslado', 0, 'desc');
    }

    function fnEditar(i) {
        fnLoading(true);

        fc_HabilitarControles(false);

        $("#btnAgregarDetTraslado").attr('disabled', true);
        $("#btnConfirmar").attr('disabled', true);
        
        $('#hdId').val(lstContenido[i].c_id);
        $('#hdPer').val(lstContenido[i].c_per);
        $('#hdCco').val(lstContenido[i].c_cco);
        $("#txtObservacion").val(lstContenido[i].d_obs);
        $("#txtPersonal").val(lstContenido[i].d_nom);
        $("#txtCentroCo").val(lstContenido[i].d_cco);
        $("#txtFechaTraslado").val(lstContenido[i].d_fec);
        $("#dpTipo").val(lstContenido[i].d_tip);
        $("#dpMotivo").val(lstContenido[i].d_mot);
        $("#txtAsignado").val(lstContenido[i].d_nom2);
        $("#txtCco").val(lstContenido[i].d_cco2);
        $("#txtUbicacion").val(lstContenido[i].d_uba);

        if (lstContenido[i].d_est == "Activo") {
            document.getElementById("c5").checked = true;
            document.getElementById('c6').checked = false;
            $("#hdEstado").val($("#c5").val());
        }
        if (lstContenido[i].d_est == "Inactivo") {
            document.getElementById("c5").checked = false;
            document.getElementById('c6').checked = true;
            $("#hdEstado").val($("#c6").val());
        }
        $('#hdtipoA').val("A");
        
        TablaDetalleTraslado(lstContenido[i].c_id);

        document.getElementById('divOcultoTraslado').style.display = 'none';
        document.getElementById('divOcultoDetTraslado').style.display = 'block';

        fnLoading(false);
       
        return true;
    }

    function validarDetalleTraslado(idT) {
        var swd = "";
        var smsd = "";
        
            if ($("#hdRowsDT").val() == 0) {
                document.getElementById('divOcultoTraslado').style.display = 'none';
                document.getElementById('divOcultoDetTraslado').style.display = 'block';
                }
            else{
                RecorreTablaDetalle();            
            }
        
    }

    function RecorreTablaDetalle() {
        var detalle = ""
        var swr = 0;
        for (var i = 1; i <= $('#hdRowsDT').val(); i++) {
            if ($('#txtA\\[' + i + '\\]').val() != undefined){// || $('#txtA\\[' + i + '\\]').val() != null || $('#txtA\\[' + i + '\\]').val() != '') {
                //alert($('#txtA\\[' + i + '\\]').val());
                if (swr == 1) {                    
                    //json para articulos
                    detalle = $('#hdDetalleArt').val();
                    detalle = detalle + ",";
                    $('#hdDetalleArt').val(detalle);
                }
                swr = 1;
                //json para articulos
                detalle = $('#hdDetalleArt').val();
                detalle = detalle + $('#txtA\\[' + i + '\\]').val();
                $('#hdDetalleArt').val(detalle);

            }
        }

        //alert($('#hdDetalleArt').val());
        //alert($('#hdDetalleCco').val());

    }

    function validarRegistroDetalleTraslado(newArt) {
        //console.log('Dato: '+newArt);
        for (var i = 1; i <= $('#hdRowsDT').val(); i++) {
            //console.log('Valor '+i+': '+$('#txtA\\[' + i + '\\]').val());
            if ($('#txtA\\[' + i + '\\]').val() != undefined || $('#txtA\\[' + i + '\\]').val() != null || $('#txtA\\[' + i + '\\]').val() != '' ) {
                if ($('#txtA\\[' + i + '\\]').val() == newArt) {
                    $('#hdsw').val("1");
                    break;
                }
                else {
                    $('#hdsw').val("0");
                }
            }
        }
    }

    function fnAgregarDetTrasladoV2() {
        if ($('#hdRowsDT').val()=="") {
            contadorTablaDetalle();
        }
        
        var t = "";
        var conta = 0;
        var swd = 0;
        var smsd = "";
        var detalle = "";
        var detalle2 = "";
        var cantidad = "";
        var texto = "";
        var nombre_control = "";

        if ($('#txtAsignado').val() == "" || $('#hdAsi').val() == "") {
            swd = 1;
            smsd = smsd + " Persona Asignada";
            nombre_control = '#txtAsignado';
        }
        if ((swd == 0) && ($('#txtCco').val() == "" || $('#hdCen').val() == "")) {
            swd = 1;
            smsd = smsd + " Centro Costo Asignado";
            nombre_control = '#txtCco';
        }
        if ((swd == 0) && ($('#txtUbicacion').val() == "" || $('#hdUbi').val() == "")) {
            swd = 1;
            smsd = smsd + " Ubicacion";
            nombre_control = '#txtUbicacion';
        }
        if ((swd == 0) && ($('#txtArticulo').val() == "" || $('#hdArt').val() == "")) {
            swd = 1;
            smsd = smsd + " Articulo";
            nombre_control = '#txtArticulo';
        }
        if (swd == 1) {
            alert("Ingresar " + smsd);
            document.getElementById('divOcultoTraslado').style.display = 'none';
            document.getElementById('divOcultoDetTraslado').style.display = 'block';
            $(nombre_control).focus();
        } else {
            //valida registros repetidos en tabla detalle
            validarRegistroDetalleTraslado($('#hdArt').val());
            
            if ($('#hdsw').val() == "1") {
                alert("Articulo ya asignado");
                document.getElementById('divOcultoTraslado').style.display = 'none';
                document.getElementById('divOcultoDetTraslado').style.display = 'block';
                $('#txtArticulo').focus();
            } else {

            fnDestroyDataTableDetalle('tbDetTraslado');

            texto = document.getElementById('hdRowsDT').value;
            conta = parseInt(texto);
            if (conta > 0) {
                $('#hdRowsDT').val(conta);
            }
            conta = conta + parseInt(1);
            $('#hdRowsDT').val(conta);

            t += '<tr id="fila' + conta + '">';
            t += '<td><a href="#" class="btn btn-red btn-xs" onclick="fnBorrarDT(\'' + conta + '\')" ><i class="ion-android-cancel"></i></a>';
            t += '<input type="hidden" id="txtA[' + conta + ']" name="txtA[' + conta + ']" value="' + $('#hdArt').val() + '" >';
            t += '<input type="hidden" id="txtU[' + conta + ']" name="txtU[' + conta + ']" value="' + $('#hdUbi').val() + '" >';
            t += '<input type="hidden" id="txtX[' + conta + ']" name="txtX[' + conta + ']" value="' + $('#hdAsi').val() + '" >';
            t += '<input type="hidden" id="txtC[' + conta + ']" name="txtC[' + conta + ']" value="' + $('#hdCen').val() + '" >';
            t += '</td>';
            t += '<td>' + $('#txtArticulo').val() + '</td>';
            t += '<td>' + $('#txtUbicacion').val() + '</td>';
            t += '<td>' + $('#txtAsignado').val() + '</td>';
            t += '<td>' + $('#txtCco').val() + '</td>';
            t += '</tr>';

            $('#pDetTraslado').append(t);
            fnResetDataTableTramite('tbDetTraslado', 0, 'desc');

            $('#txtArticulo').val("");
            //$('#txtUbicacion').val("");
            //$('#txtAsignado').val("");
            //$('#txtCco').val("");
                
            }
            
        }

    }

    function contadorTablaDetalle() {
        var tableReg = document.getElementById('tbDetTraslado');
        var searchText = 'No se ha encontrado informacion';
        var cellsOfRow = "";
        var compareWith = "";
        var contawors = "";
        for (var i = 1; i < tableReg.rows.length; i++) {
            cellsOfRow = tableReg.rows[i].getElementsByTagName('td');
            for (var j = 0; j < cellsOfRow.length; j++) {
                compareWith = cellsOfRow[j].innerHTML;
                if (searchText.length == 0 || (compareWith.indexOf(searchText) > -1)) {
                    $("#hdCantDT").val("0");
                    contawors = 0;
                    $("#hdRowsDT").val(contawors);
                }
            }
        }
    }


    function fnLoading(sw) {
        //console.log(sw);
        if (sw) {
            $('.piluku-preloader').removeClass('hidden');
        } else {
            $('.piluku-preloader').addClass('hidden');
        }
    }


    function fnGuardarTraslado() {

        fnLoading(true);
        document.body.style.cursor = 'wait';
        //$("#btnConfirmar").attr("disabled", "disabled");
        
        $("#hdDetalleArt").val("");

        document.getElementById('divOcultoTraslado').style.display = 'block';
        document.getElementById('divOcultoDetTraslado').style.display = 'none';

        var sw = 0;
        var swR = 0;
        var mensaje = "";
        var comasms = ", ";
        var swI = 0;
        var nombre_control = "";

        if ($("#dpTipo").val() == "0") {
            mensaje = mensaje + "Tipo" + comasms ;
            sw = 1;
            nombre_control = "#dpTipo";
        }
        if ((sw == 0) && ($("#txtFechaTraslado").val() == "")) {
            mensaje = mensaje + "Fecha";
            sw = 1;
            nombre_control = "#txtFechaTraslado"; 
        }
        if ((sw == 0) && ($("#txtPersonal").val() == "" || codigo_per == "")) {
            mensaje = mensaje + "Personal" + comasms;
            sw = 1;
            nombre_control = "#txtPersonal";
        }
        if ((sw == 0) && ($("#txtCentroCo").val() == "" || codigo_cco == "")) {
            mensaje = mensaje + "Centro Costo" + comasms;
            sw = 1;
            nombre_control = "#txtCentroCo";
        }
        if ($("#dpMotivo").val() == "0") {
            mensaje = mensaje + "Motivo" + comasms;
            sw = 1;
            nombre_control = "#dpMotivo";
        }
        if ((sw == 0) && ($('#txtAsignado').val() == "")) {
            sw = 1;
            mensaje = mensaje + " Asignado" + comasms;
            nombre_control = '#txtAsignado';
        }
        if ((sw == 0) && ($('#txtCco').val() == "")) {
            sw = 1;
            mensaje = mensaje + " Centro Costo" + comasms;
            nombre_control = '#txtCco';
        }
        if ((sw == 0) && ($('#txtUbicacion').val() == "")) {
            sw = 1;
            mensaje = mensaje + " Ubicacion" + comasms;
            nombre_control = '#txtUbicacion';
        }
        if ((sw == 0) && ($("#txtObservacion").val() == "")) {
            mensaje = mensaje + "Observacion" + comasms;
            sw = 1;
            nombre_control = "#txtObservacion";
        }

        if (document.getElementById('c5').checked) {
            $("#hdEstado").val($("#c5").val());
        } else if (document.getElementById('c6').checked) {
            $("#hdEstado").val($("#c6").val());
        }

        if (sw == 1) {
            alert("Completar Información " + mensaje);
            document.getElementById('divOcultoTraslado').style.display = 'none';
            document.getElementById('divOcultoDetTraslado').style.display = 'block';
            $("#hsSW1").val("0");
            $(nombre_control).focus();
            fnLoading(false);
            $("#btnBuscarTraslado").removeAttr("disabled");
            document.body.style.cursor = 'default';
            //$("#btnConfirmar").removeAttr("disabled");
        }
        else {
            $("#hsSW1").val("1");
            contadorTablaDetalle();
            validarDetalleTraslado($("#hdId").val());

            if ($("#hdRowsDT").val() == 0) {
                alert("Ingresar detalles");
                document.getElementById('divOcultoTraslado').style.display = 'none';
                document.getElementById('divOcultoDetTraslado').style.display = 'block';
                fnLoading(false);
                $("#btnBuscarTraslado").removeAttr("disabled");
                document.body.style.cursor = 'default';
                //$("#btnConfirmar").removeAttr("disabled");
            } 
            else {
                document.getElementById('divOcultoTraslado').style.display = 'block';
                document.getElementById('divOcultoDetTraslado').style.display = 'none';

                $("input#param0").val("gTrasladoMercaderiaV2");
                var form = $('#frmTrasladoMercaderia').serialize();
                //console.log(form);
                try {
                    //console.log("Envia a Guardar XD");
                    $.ajax({
                        type: "GET",
                        contentType: "application/json; charset=utf-8",
                        url: "../../DataJson/activofijo/processactivofijo.aspx",
                        data: form,
                        dataType: "json",
                        success: function(data) {
                            console.log(data);
                            //console.log("+++++++++++++++++++");
                            if (data[0].Status == "success") {
                                //console.log("exitooooooooo");
                                fnMensaje(data[0].Status, data[0].Message);
                                document.getElementById('divOcultoTraslado').style.display = 'block';
                                document.getElementById('divOcultoDetTraslado').style.display = 'none';
                                MostrarContenido($('#txtFechaIni').val(), $('#txtFechaFin').val());
                            }
                            else {
                                //console.log("erroooooorrr");
                                fnMensaje(data[0].Status, data[0].Message);
                                document.getElementById('divOcultoTraslado').style.display = 'none';
                                document.getElementById('divOcultoDetTraslado').style.display = 'block';
                                MostrarContenido($('#txtFechaIni').val(), $('#txtFechaFin').val());
                            }
                            //fnLoading(false);
                        },
                        error: function(result) {
                            fnLoading(false);
                            $("#btnBuscarTraslado").removeAttr("disabled");
                            document.body.style.cursor = 'default';
                            //$("#btnConfirmar").removeAttr("disabled");
                            console.log(result);
                        }
                        
                    });
                    document.getElementById("param0").value = "";

                    MostrarContenido($('#txtFechaIni').val(), $('#txtFechaFin').val());

                    fnLoading(false);
                    $("#btnBuscarTraslado").removeAttr("disabled");
                    document.body.style.cursor = 'default';
                    //$("#btnConfirmar").removeAttr("disabled");

                }
                catch (err) {
                    fnLoading(false);
                    $("#btnBuscarTraslado").removeAttr("disabled");
                    document.body.style.cursor = 'default';
                    //$("#btnConfirmar").removeAttr("disabled");
                    console.log(err.message);
                }    
                
            }

        }
           
    }

    function TablaDetalleTraslado(ctra) {
        fnDestroyDataTableDetalle('tbDetTraslado');
        $("#param1").val(ctra);
        $("input#param0").val("gLstDetalleTraslado");
        var form = $('#frmTrasladoMercaderia').serialize();
        $.ajax({
            type: "GET",
            contentType: "application/json; charset=utf-8",
            url: "../../DataJson/activofijo/processactivofijo.aspx",
            data: form,
            dataType: "json",
            success: function(data) {
                aData = data;
                //console.log(data);
                var i = 0;
                var t = '';
                var tt = '';
                var tipo = '';
                var conta = 0;
                if (data.length > 0) {
                    //fnResetDataTableTramite('tbArtCrt', 0, 'desc');
                    for (var i = 0; i < data.length; i++) {
                        conta++;
                        t += '<tr id="fila' + conta + '">';
                        t += '<td>'
                        if ($('#hdtipoA').val() == "A") {
                            t += '<a href="#" class="btn btn-red btn-xs disabled" onclick="fnBorrarDT(\'' + conta + '\')" ><i class="ion-android-cancel"></i></a>';
                        }
                        else {
                            t += '<a href="#" class="btn btn-red btn-xs active" onclick="fnBorrarDT(\'' + conta + '\')" ><i class="ion-android-cancel"></i></a>';
                        }
                        t += '<input type="hidden" id="txtA[' + conta + ']" name="txtA[' + conta + ']" value="' + data[i].c_af + '" >';
                        t += '<input type="hidden" id="txtU[' + conta + ']" name="txtU[' + conta + ']" value="' + data[i].c_uba + '" >';
                        t += '<input type="hidden" id="txtX[' + conta + ']" name="txtX[' + conta + ']" value="' + data[i].c_per + '" >';
                        t += '<input type="hidden" id="txtC[' + conta + ']" name="txtC[' + conta + ']" value="' + data[i].c_cco + '" >';
                        t += '</td>';
                        t += '<td>' + data[i].d_art + '</td>';
                        t += '<td>' + data[i].u_nom + '</td>';
                        t += '<td>' + data[i].d_nom + '</td>';
                        t += '<td>' + data[i].d_cco + '</td>';
                        t += '</tr>';
                    }
                    $("#hdRowsDT").val(conta);
                } else {
                    $("#hdCantDT").val("0");
                }

                $('#pDetTraslado').html(t);
                fnResetDataTableTramite('tbDetTraslado', 0, 'desc');
            },
            error: function(result) {
                //f_Menu("listaConfigArticulos.aspx");
            }
        });
        document.getElementById("param0").value = "";
        //$('div#mdConfigArticulo').modal('hide');
    }

    function fc_HabilitarControles(flag) {
        if (flag) {
            $("#btnBuscarTraslado").removeAttr("disabled");
            $("#btnAgregarTraslado").removeAttr("disabled");
            $("#txtFechaIni").removeAttr("disabled");
            $("#txtFechaFin").removeAttr("disabled");
        }
        else {
            $("#btnBuscarTraslado").attr('disabled', true);
            $("#btnAgregarTraslado").attr('disabled', true);
            $("#txtFechaIni").attr('disabled', true);
            $("#txtFechaFin").attr('disabled', true);
        }
    }
    
</script>
<body>
    
    <div class="piluku-preloader text-center hidden">
        <div class="loader">
            Loading...
        </div>
    </div>
     
<form id="frmTrasladoMercaderia" name="frmTrasladoMercaderia" action="#" > 
    
    <input type="hidden" id="param0" name="param0" value="" />  
    <input type="hidden" id="param1" name="param1" value="" />
    <input type="hidden" id="hdId" name="hdId" value="" />
    <input type="hidden" id="hdArt" name="hdArt" value="" />
    <input type="hidden" id="hdPer" name="hdPer" value="" />
    <input type="hidden" id="hdCco" name="hdCco" value="" />
    <input type="hidden" id="hdEstado" name="hdEstado" value="" />
    <input type="hidden" id="hdtipoA" name="hdtipoA" value="" />
    <input type="hidden" id="hdCantDT" name="hdCantDT" value="" />
    <input type="hidden" id="hdsw" name="hdsw" value="" />
    <input type="hidden" id="hdTE" name="hdTE" value="" />
    <input type="hidden" id="hsSW1" name="hsSW1" value="" />
    <input type="hidden" id="hdDetalleArt" name="hdDetalleArt" value="" />
    <input type="hidden" id="hdRowsDT" name="hdRowsDT" value="" />
    <input type="hidden" id="hdUser" name="hdUser" runat="server" />
    
    <input type="hidden" id="hdUbi" name="hdUbi" value="" />
    <input type="hidden" id="hdAsi" name="hdAsi" value="" />
    <input type="hidden" id="hdPerfil" name="hdPerfil" runat="server" />
    <input type="hidden" id="hdCen" name="hdCen" value="" />
    <input type="hidden" id="hdNombreUser" name="hdNombreUser" runat="server" />
    <input type="hidden" id="hdCodCcoUser" name="hdCodCcoUser" runat="server" /> 
    <input type="hidden" id="hdNomCcoUser" name="hdNomCcoUser" runat="server" />
    <input type="hidden" id="hdEmailUser" name="hdEmailUser" runat="server" />
    <input type="hidden" id="hdEmailAsig" name="hdEmailAsig" value="" />
    
<div class="col-md-12" >
    <div class="panel panel-piluku">
        <div class="panel-heading">
                <h3 class="panel-title">
                    Traslado de Activo Fijo
                </h3>
        </div>                            																						
	    <div class="panel-body">								
	        <div class="col-md-12">
                        
                        <div class="row">			            
                        <div class="form-group">
				            <div class="col-md-3">			        
					            <div class="input-group date">
									    <input type="text" class="form-control" id="txtFechaIni" data-provide="datepicker" placeholder="Fecha Inicio" name="txtFechaIni" >
									    <span class="input-group-addon bg">
										    <i class="ion ion-ios-calendar-outline"></i>
									    </span>
							    </div>					       
				            </div>					        
                            <div class="col-md-3">
					            <div class="input-group date">
									    <input type="text" class="form-control" id="txtFechaFin" data-provide="datepicker" placeholder="Fecha Fin" name="txtFechaFin" >
									    <span class="input-group-addon bg">
										    <i class="ion ion-ios-calendar-outline"></i>
									    </span>
							    </div>	
				            </div>
                            <div class="col-md-2">                      
                                <a href="#" id="btnBuscarTraslado" class="btn btn-green btn-xs" style="width:100%"><i class="ion-search"></i>&nbsp;Buscar</a>                
                            </div>
                            
                            <div class="col-md-2">   
                                <a href="#" id="btnAgregarTraslado" class="btn btn-primary btn-xs" style="width:100%"><i class="ion-android-done"></i>&nbsp;Agregar</a>                
                            </div>
		        
			            </div>
                        </div> 
                        <br />
                         <div id='divOcultoTraslado' style="display:none;width:100%;">
                              <table class='display dataTable cell-border' id='tbTraslado' width="100%" style="font-size:smaller;">
                                <thead>
                                <tr>
                                     <th style="width:10%;text-align:center;"></th>
                                     <th style="width:5%;">Item</th>
                                     <th style="width:5%;">Nro</th>
                                     <th style="width:10%;">Tipo</th>
                                     <th style="width:20%;">Nombre</th>
                                     <th style="width:30%;">Centro Costo</th>
                                     <th style="width:10%;">Fecha</th>
                                     <th style="width:10%;">Estado</th>
                                 </tr>
                                 </thead>     
                                 <tbody id ="pTraslado" runat ="server">
                                 </tbody>                             
                                   <tfoot>
                                    <tr>
                                    <th colspan="8"></th>
                                    </tr>
                                    </tfoot>
                                </table>
                        </div> 
                    
                    <div id="divOcultoDetTraslado" style="display:none;width:100%;"> 

                        <div class="modal-content">
		                    <div class="panel-heading" style="background-color:#E33439;" >
			                    <h4 class="panel-title"  style="color:White">Detalle Traslado de Activo Fijo</h4>
		                    </div>
                                <br />
                                    <div class="col-md-12">
                    	                
                                        <div class="row">
                                        <div class="form-group">
			                                <label class="col-md-2 control-label">Tipo Solicitud:</label>
			                                <div class="col-md-4">
                                                <select class="form-control" id="dpTipo" name="dpTipo">
				                                    <option value="0" selected>Seleccione</option>
				                                    <option value="I" selected> INTERNO</option>
				                                    <option value="E" selected> EXTERNO</option>
                                                </select>
			                                </div>
			                                
                                            <label class="col-md-2 control-label">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Fecha:</label>
			                                <div class="col-md-4">
                                                <div class="input-group date">
							                            <input type="text" class="form-control" id="txtFechaTraslado" data-provide="datepicker" placeholder="Seleccione Fecha" name="txtFechaTraslado" >
							                            <span class="input-group-addon bg">
								                            <i class="ion ion-ios-calendar-outline"></i>
							                            </span>
					                            </div>	
			                                </div>
		                                </div>
		                                </div>	
                    			        
                                        <div class="row">
                                        <div class="form-group">
			                                <label class="col-md-2 control-label">Apellidos y Nombres:</label>
			                                <div class="col-md-7">
				                                <input name="txtPersonal" type="text" id="txtPersonal" value="" class="form-control" placeholder="Elegir al escribir"/>
			                                </div>
		                                </div>
		                                </div>	
                    			        
                                        <div class="row">
                                        <div class="form-group">
			                                <label class="col-md-2 control-label">Centro de Costo:</label>
			                                <div class="col-md-7">
				                                <input name="txtCentroCo" type="text" id="txtCentroCo" value="" class="form-control" placeholder="Elegir al escribir"/>
			                                </div>

		                                </div>
		                                </div>	
                    			        
		                                <div class="row">
                                        <div class="form-group">
			                                <label class="col-md-2 control-label">Motivo:</label>
			                                <div class="col-md-4">
                                                <select class="form-control" id="dpMotivo" name="dpMotivo">
				                                    <option value="0" selected>Seleccione</option>
				                                    <option value="1" selected> REASIGNACION</option>
				                                    <option value="2" selected> MANTENIMIENTO</option>
                                                </select>
			                                </div>

                                            <label class="col-md-2 control-label">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Estado:</label>
                                            <div class="col-sm-4">
						                        <ul class="list-inline checkboxes-radio">
							                        <li>
								                        <input type="radio" name="active" id="c5" value="1" checked/>
								                        <label for="c5"><span></span>ACTIVO</label>
							                        </li>
							                        <li>
								                        <input type="radio" name="active" id="c6"  value="0" />
								                        <label for="c6"><span></span>INACTIVO</label>
							                        </li>
						                        </ul>
					                        </div>
		                                </div>
		                                </div>
		                                <div class="row">
		                                    <div class="form-group">
		                                        <label class="col-md-2 control-label">Asignar(Personal)[*]:</label> 
		                                        <div class="col-md-6">
		                                            <input name="txtAsignado" type="text" id="txtAsignado" value="" class="form-control" placeholder="Elegir al escribir" />
		                                        </div>
		                                    </div>
		                                </div>
		                                <div class="row">
		                                    <div class="form-group">
		                                        <label class="col-md-2 control-label">Centro Costo [*]:</label>
		                                        <div class="col-md-6">
		                                            <input name="txtCco" type="text" id="txtCco" value="" class="form-control" placeholder="Elegir al escribir" />
		                                        </div>
		                                    </div>
		                                </div>
		                                <div class="row">
		                                    <div class="form-group">
		                                        <label class="col-md-2 control-label">Ubicaci&oacute;n de Destino:[*]:</label>
		                                        <div class="col-md-6">
		                                            <input name="txtUbicacion" type="text" id="txtUbicacion" value="" class="form-control" placeholder="Elegir al escribir"/>
		                                        </div>
		                                        <label class="col-md-3">[*] Digitar como minimo 3 letras</label>
		                                    </div>
		                                </div>
		                                <div class="row">
		                                <div class="form-group">
		                                    <label class="col-md-2 control-label">Observación</label>    
		                                    <div class="col-md-10">
		                                        <input name="txtObservacion" type="text" id="txtObservacion" value="" class="form-control" />
		                                    </div>
		                                </div>
		                                </div>	
                                        <br />
                                        
                                        <div class="modal-footer"> </div>
                                        
                                        <!-- ******************** <Div>: Detalle Traslado ******************** -->
                                        <!-- Proyecto: Activo Fijo  | Fecha: 2018-07-19 | Programador: enevado -->
                                        <div class="row">
                                            <div class="form-group">
                                                <label class="col-md-2 control-label">Activo Fijo [*]:</label>
			                                    <div class="col-md-6">
				                                    <input name="txtArticulo" type="text" id="txtArticulo" value="" class="form-control" placeholder="Buscar Codigo, Articulo"/>
			                                    </div>
                                                <div class="col-md-2">
				                                    <a href="#" id="btnAgregarDetTraslado" class="btn btn-primary btn-xs" style="width:100%">
				                                        <i class="ion-android-done"></i>&nbsp;Agregar
				                                    </a>                
			                                    </div>       
		                                    </div>
		                                </div>
		                                
		                                <!-- ******************** </Div>: Detalle Traslado ******************** -->
		                                
                                        <br />
                                        <div class="modal-footer"> </div>
                                        
                                        <!-- ******************** <table>: Detalle Traslado ******************** -->
                                        <!-- Proyecto: Activo Fijo  | Fecha: 2018-07-19 | Programador: enevado -->
                                        <table class='display dataTable cell-border' id='tbDetTraslado' width="100%" style="font-size:smaller;">
                                            <thead>
                                                <tr>
                                                    <th style="width:5%;text-align:center;"></th>
                                                    <th style="width:35%;">Activo Fijo</th>
                                                    <th style="width:20%;">Ubicación</th>
                                                    <th style="width:20%;">Asignado</th>
                                                    <th style="width:20%;">Centro Costo</th>
                                                </tr>
                                            </thead>     
                                            <tbody id ="pDetTraslado" runat ="server">
                                            </tbody>                             
                                            <tfoot>
                                                <tr>
                                                    <th colspan="5"></th>
                                                </tr>
                                            </tfoot>
                                        </table>
                    			        <!-- ******************** </table>: Detalle Traslado ******************** -->
                    			        
                                    <br />
                    			    <div class="modal-footer">                  			
                    			    </div>
                    			
                    			   </div>
                    			   	     
	                          <center>
	                              <div class="btn-group">			      
	                                    <button type="button" class="btn btn-primary" id="btnConfirmar" ><i class="ion-android-done"></i>&nbsp;Guardar</button>	
	                                    <button type="button" class="btn btn-danger" id="btnCancelarDetT"><i class="ion-android-cancel"></i>&nbsp;Cancelar</button>	
	                               </div>
	                          </center>
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
