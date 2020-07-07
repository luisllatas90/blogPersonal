<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmRegistroGrupoInvestigacion.aspx.vb" Inherits="GestionInvestigacion_frmRegistroGrupoInvestigacion" %>
<html>
<head>
    <title>Grupo de Investigación</title>
    <%--Compatibilidad con IE--%>
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta http-equiv='X-UA-Compatible' content='IE=7' />
    <meta http-equiv='X-UA-Compatible' content='IE=8' />
    <meta http-equiv='X-UA-Compatible' content='IE=10' />
    <meta http-equiv="X-UA-Compatible" content="IE=edge"/> 
    <%--Compatibilidad con IE--%>
	<script type="text/javascript" src="../assets/js/jquery.js"></script>
	<script type="text/javascript" src="../assets/js/bootstrap.min.js"></script>	
	<script type="text/javascript" src='../assets/js/noty/jquery.noty.js'></script>
    <script type="text/javascript" src='../assets/js/noty/layouts/top.js'></script>
    <script type="text/javascript" src='../assets/js/noty/layouts/default.js'></script>
    <script type="text/javascript" src='../assets/js/noty/notifications-custom.js'></script>
    <script type="text/javascript" src='../assets/js/jquery-ui-1.10.3.custom.min.js'></script>
    
    <!-- Manejo de tablas -->
    <script type="text/javascript" src='../assets/js/jquery.dataTables.min.js'></script>
    <link href="../assets/css/jquery.dataTables.min.css" rel="stylesheet" type="text/css" />
    <script src="../assets/js/funcionesDataTable.js?y=1" type="text/javascript"></script>
    
    <!-- Piluku -->        
    <link rel="stylesheet" type="text/css" href="../assets/css/bootstrap.min.css"/>
	<link rel="stylesheet" href="../assets/css/material.css?x=1"/>		
	<link rel="stylesheet" type="text/css" href="../assets/css/style.css?y=4"/>
	

    
    <%-- ======================= Inicio Notificaciones =============================================--%>

   <!-- <script type="text/javascript" src="../assets/js/noty/jquery.noty.js"></script>
    <script type="text/javascript" src='../assets/js/noty/layouts/top.js'></script>
    <script type="text/javascript" src='../assets/js/noty/layouts/default.js'></script>
    <script type="text/javascript" src="../assets/js/noty/notifications-custom.js"></script>-->

    <%-- ======================= Fin Notificaciones =============================================--%>

    <!--<script src="js/_General.js?x=1" type="text/javascript"></script>-->
    
    <style type="text/css">
        .row
        {
            margin-right: 0px;
            margin-left: 0px;
            padding: 3px;
        }
        .content .main-content
        {
            padding-right: 18px;
        }
        .content
        {
            margin-left: 0px;
        }
        .form-control
        {
            border-radius: 0px;
            box-shadow: none;
            border-color: #d9d9d9;
            height: 30px; /*font-weight: 300;  line-height: 40px; */
            color: black;
        }
        .i-am-new
        {
            z-index: 100;
        }
        .form-group
        {
            margin: 3px;
        }
        .form-horizontal .control-label
        {
            padding-top: 3px;
        }
    </style>

    <script type="text/javascript">
        var aDataA = [];
        var aDataS = [];
        var aDataD = [];
        var aDataRI = [];
        var aDataGI = [];
        var aDataL = [];
        var aDataR = [];
        var listaInv;
        var c_per;
        var d_per;
        var d_tip;
        var dni_per;

        $(document).ready(function() {
            $('#hdCoord').val("");
            $('#hdAccion').val("R");
            
            rpta = fnvalidaSession();
            if (rpta == false) {
                window.location.href = rpta;
            }

            //fnResetDataTableTramite('tbInvestigador', 0, 'desc');
            //fnResetDataTableTramite('tGrupoInvestigador', 0, 'desc');
            //fnResetDataTableBasic('tbInvestigador', 0, 'desc');
            //fnResetDataTableBasic('tGrupoInvestigador', 0, 'desc');
            //document.execCommand('ClearAuthenticationCache');
            var dt = fnCreateDataTableBasic('tbInvestigador', 1, 'asc');
            var dt1 = fnCreateDataTableBasic('tGrupoInvestigador', 1, 'asc');
            $('#divLineasOCDE').hide();
            $('#chkOCDE').change(function() {
                if ($(this).is(':checked')) {
                    $('#divLineasOCDE').show();
                } else {
                    $('#divLineasOCDE').hide();
                }
            });

            listaArea();
            listaGrupoInvestigador();
            listaLineasUsat();
            listaRolInvestigador();
            listaRegion();
            $("#btnAgregarInv").click(fnAgregarInv);
            $("#btnAgregar").click(fnAgregar);

            $("#btnDelReg").click(fnDelRegistro);
            $("#btnRegistrarInvestigador").click(fnRegistrarInv);

            listaInv = fnCargaInvestigadores(0, 'TO');
            var jsonString = JSON.parse(listaInv);

            $('#txtInvestigador').autocomplete({
                source: $.map(jsonString, function(item) {
                    return item.d_per;
                }),
                select: function(event, ui) {
                    var selectecItem = jsonString.filter(function(value) {
                        return value.d_per == ui.item.value;
                    });
                    d_tip = selectecItem[0].d_tip;
                    c_per = selectecItem[0].c_inv;
                    d_per = selectecItem[0].d_per;
                    dni_per = selectecItem[0].c_dni;
                    //alert("cod: " + selectecItem[0].c_per + ", nombre: " + selectecItem[0].d_per);
                },
                minLength: 3,
                delay: 100
            });

            $('#txtInvestigador').keyup(function() {
                var l = parseInt($(this).val().length);
                if (l == 0) {
                    c_per = "";
                    d_per = "";
                    dni_per = "";
                }
            });

            $("#cboArea").change(function() {
                var t = '';
                //alert(this.value);
                if (this.value == "0") {
                    t = '<option value="0" selected="selected" > -- Seleccione -- </option>';
                    $('#cboSubArea').html(t);
                    $('#cboSubArea').val("0");
                    t = '<option value="0" selected="selected" > -- Seleccione -- </option>';
                    $('#cboDisciplina').html(t);
                    $('#cboDisciplina').val("0");
                }
                else {
                    $("#action").val("lAreaConocimientosOCDE");
                    var form = $('#frmRegistroGrupoInvestigacion').serialize();
                    $.ajax({
                        type: "GET",
                        //contentType: "application/json; charset=utf-8",
                        url: "../DataJson/GestionInvestigacion/operaciones.aspx",
                        data: { "action": $("#action").val(), "param1": this.value, "param2": "SA" },
                        dataType: "json",
                        cache: false,
                        async: false,
                        success: function(data) {
                            aDataS = data;
                            //console.log(data);
                            var i = 0;
                            var t = '';
                            if (aDataS.length > 0) {
                                for (var i = 0; i < aDataS.length; i++) {
                                    t += '<option value="' + aDataS[i].codigo + '" selected="' + aDataS[i].selected + '">' + aDataS[i].descripcion + '</option>';
                                }
                            }
                            $('#cboSubArea').html(t);
                        },
                        error: function(result) {
                            sOut = '';
                        }
                    });
                    $('#cboDisciplina').val('<option value="0" selected="selected" > -- Seleccione -- </option>');
                    $('#cboDisciplina').val("0");
                }
            });

            $("#cboSubArea").change(function() {
                var t = '';
                if (this.value == "0") {
                    t = '<option value="0" selected="selected" > -- Seleccione -- </option>';
                    $('#cboDisciplina').html(t);
                    $('#cboDisciplina').val("0");
                }
                else {
                    if ($("#cboArea").val() != "0") {
                        $("#action").val("lAreaConocimientosOCDE");
                        var form = $('#frmRegistroGrupoInvestigacion').serialize();
                        $.ajax({
                            type: "GET",
                            //contentType: "application/json; charset=utf-8",
                            url: "../DataJson/GestionInvestigacion/operaciones.aspx",
                            data: { "action": $("#action").val(), "param1": this.value, "param2": "DI" },
                            dataType: "json",
                            cache: false,
                            async: false,
                            success: function(data) {
                                aDataD = data;
                                var i = 0;
                                var t = '';
                                if (aDataD.length > 0) {
                                    for (var i = 0; i < aDataD.length; i++) {
                                        t += '<option value="' + aDataD[i].codigo + '" selected="' + aDataD[i].selected + '">' + aDataD[i].descripcion + '</option>';
                                    }
                                }
                                $('#cboDisciplina').html(t);
                            },
                            error: function(result) {
                                sOut = '';
                            }

                        });

                    }
                }
            });

            $("#cboRegion").change(function() {
                if ($(this).val() != 0) {
                    fnListarProvincia($(this).val());
                } else {
                    $("#cboProvincia").html("<option value='0'>-- Seleccione --</option>");
                }
                $("#cboDistrito").html("<option value='0'>-- Seleccione --</option>");
            })

            $("#cboProvincia").change(function() {
                if ($(this).val() != 0) {
                    fnListarDistrito($(this).val());
                } else {
                    $("#cboDistrito").html("<option value='0'>-- Seleccione --</option>");
                }
            });

        });

        function fnvalidaSession() {
            var rpta = false
            $('body').append('<form id="frm"><input type="hidden" id="action" name="action" value="ValidaSession" /></form>');
            var form = $("#frm").serializeArray();
            $("#frm").remove();
            $.ajax({
                type: "POST",
                url: "../DataJson/GestionInvestigacion/operaciones.aspx",
                data: form,
                dataType: "json",
                cache: false,
                async: false,
                success: function(data) {
                    //console.log(data);
                    if (data[0].msje == true) {
                        rpta = data[0].msje
                    } else {
                        rpta = data[0].link
                    }
                },
                error: function(result) {
                    console.log(result)
                }
            });
            return rpta;
        }
        
        function fnListarProvincia(cod) {
            var arr;
            $('body').append('<form id="frmOpe"><input type="hidden" id="action" name="action" value="lProvincia" /><input type="hidden" id="codRegion" name="codRegion" value="' + cod + '" /></form>');
            var form = $("#frmOpe").serializeArray();
            $("#frmOpe").remove();
            //console.log(form);
            $.ajax({
                type: "POST",
                url: "../DataJson/GestionInvestigacion/operaciones.aspx",
                data: form,
                dataType: "json",
                cache: false,
                async: false,
                success: function(data) {
                    var tb = '';
                    var filas = data.length;
                    tb += '<option value="0" selected="selected">-- Seleccione --</option>';
                    if (filas > 0) {
                        for (i = 0; i < filas; i++) {
                            tb += '<option value="' + data[i].cod + '">' + data[i].nombre + '</option>';
                        }
                        $("#cboProvincia").html(tb);
                    }
                },
                error: function(result) {
                    //console.log(result)
                    arr = result;
                }
            });

            return arr;
        }

        function fnListarDistrito(cod) {
            var arr;
            $('body').append('<form id="frmOpe"><input type="hidden" id="action" name="action" value="lDistrito" /><input type="hidden" id="codProvincia" name="codProvincia" value="' + cod + '" /></form>');
            var form = $("#frmOpe").serializeArray();
            $("#frmOpe").remove();
            //console.log(form);
            $.ajax({
                type: "POST",
                url: "../DataJson/GestionInvestigacion/operaciones.aspx",
                data: form,
                dataType: "json",
                cache: false,
                async: false,
                success: function(data) {
                    var tb = '';
                    var filas = data.length;
                    tb += '<option value="0" selected="selected">-- Seleccione --</option>';
                    if (filas > 0) {
                        for (i = 0; i < filas; i++) {
                            tb += '<option value="' + data[i].cod + '">' + data[i].nombre + '</option>';
                        }
                        $("#cboDistrito").html(tb);
                    }
                },
                error: function(result) {
                    //console.log(result)
                    arr = result;
                }
            });

            return arr;
        }

        function fnListarObservaciones(cod, gru) {
            $.ajax({
                type: "GET",
                //contentType: "application/json; charset=utf-8",
                url: "../DataJson/GestionInvestigacion/operaciones.aspx",
                data: { "action": "gVisualizarHistorialGI", "param1": cod, "param2": gru },
                dataType: "json",
                cache: false,
                async: false,
                success: function(data) {
                    //console.log("*---------");
                    //console.log(data);
                    var filas = data.length;
                    var div = "";
                    var obs = 0;
                    var res = 0;
                    if (filas > 0) {
                        div += "<div class='alert alert-danger'>";
                        div += "<label style='font-size:12px;text-align:center;margin-botton:2px;color:black'>OBSERVACIONES</label></br>";
                        for (i = 0; i < filas; i++) {
                            if (data[i].c_obs == 0) {
                                obs = obs + 1;
                                div += "<label style='font-size:12px;line-height:10px;margin-botton:2px;color:red'>" + obs + ".- " + data[i].d_fech + " - " + data[i].d_eia + " - " + data[i].d_obs + "</label></br>";
                            } else {
                                if (data[i].c_obs == 2 || data[i].c_obs == 3) {
                                    div += "<label style='font-size:12px;line-height:10px;margin-botton:2px;color:blue'> .: " + data[i].d_fech + " - " + data[i].d_eia + " - " + data[i].d_obs + "</label></br>";
                                } else {
                                    res = res + 1;
                                    div += "<label style='font-size:12px;line-height:10px;margin-botton:2px;color:green'>" + res + ".- " + data[i].d_fech + " - " + data[i].d_eia + " - " + data[i].d_obs + "</label></br>";
                                }
                            }
                        }
                        div += "</div>";
                    } else {
                        div = ""
                    }
                    $("#DivObservaciones").html(div);

                },
                error: function(result) {
                    //console.log(result); //--para errores                      
                    fnMensaje("warning", result);
                }

            });
            
        }
        
        function listaRegion() {
            $.ajax({
                type: "GET",
                //contentType: "application/json; charset=utf-8",
                url: "../DataJson/GestionInvestigacion/operaciones.aspx",
                data: { "action": "lRegion" },
                dataType: "json",
                cache: false,
                async: false,
                success: function(data) {
                aDataR = data;
                    //console.log(data);
                    //console.log("++++++++++");
                    var i = 0;
                    var t = '';
                    if (aDataR.length > 0) {
                        for (var i = 0; i < aDataR.length; i++) {
                            t += '<option value="' + aDataR[i].c_reg + '" selected="' + aDataR[i].d_sel + '" >' + aDataR[i].d_reg + '</option>';
                        }
                    }
                    //console.log(t);
                    //console.log("++++++++++");
                    $('#cboRegion').html(t);
                },
                error: function(result) {
                    sOut = '';
                    //console.log(result);
                }
            });
        }

        function listaLineasUsat() {
            $.ajax({
                type: "GET",
                //contentType: "application/json; charset=utf-8",
                url: "../DataJson/GestionInvestigacion/operaciones.aspx",
                data: { "action": "lLineasUsat", "param1": "%" },
                dataType: "json",
                cache: false,
                async: false,
                success: function(data) {
                    aDataL = data;
                    //console.log(data);
                    //console.log("*******");
                    var i = 0;
                    var t = '';
                    if (aDataL.length > 0) {
                        for (var i = 0; i < aDataL.length; i++) {
                            t += '<option value="' + aDataL[i].c_lin + '" >' + aDataL[i].d_lin + '</option>';
                        }
                    }
                    $('#cboLineasUSAT').html(t);
                },
                error: function(result) {
                    sOut = '';
                    //console.log(result);
                }
            });
        }

        
        function fnValidaInvestigador() {
            $.ajax({
                type: "GET",
                contentType: "application/json; charset=utf-8",
                url: "../DataJson/GestionInvestigacion/operaciones.aspx",
                data: { "action": "validaInvestigador", "param1": $("#hdUser").val(), "param2": "VA" },
                dataType: "json",
                cache: false,
                async: false,
                success: function(data) {
                    var filas = data.length;
                    if (filas > 0) {
                        $("#hdValidaInv").val("1");
                    } else {
                        $("#hdValidaInv").val("0");
                    }
                },
                error: function(result) {
                    $("#hdValidaInv").val("0");
                }
            });
        }

        function llenarsubarea(area, combo, cod) {
            var param2 = "";
            if (combo == "S") {
                param2 = "SA";
            } else {
                param2 = "DI";
            }
            $.ajax({
                type: "GET",
                //contentType: "application/json; charset=utf-8",
                url: "../DataJson/GestionInvestigacion/operaciones.aspx",
                data: { "action": "lAreaConocimientosOCDE", "param1": area, "param2": param2 },
                dataType: "json",
                cache: false,
                async: false,
                success: function(data) {
                    aDataS = data;
                    //console.log(data);
                    var i = 0;
                    var t = '';
                    var chk = '';
                    if (aDataS.length > 0) {
                        for (var i = 0; i < aDataS.length; i++) {
                            if (aDataS[i].codigo == cod) {
                                chk = "selected";
                            } else {
                                chk = "";
                            }
                            t += '<option value="' + aDataS[i].codigo + '" ' + chk + ' >' + aDataS[i].descripcion + '</option>';
                        }
                    }
                    if (combo == "S") {
                        $('#cboSubArea').html(t);
                    } else {
                        $('#cboDisciplina').html(t);
                    }
                    
                },
                error: function(result) {
                    sOut = '';
                }
            });
        }

        function limpiar() {
            $('#txtGrupo').val("");
            $('#cboLineasUSAT').val(0);
            $('#cboArea').val(0);
            $('#cboSubArea').val(0);
            $('#cboDisciplina').val(0);
            $('#cboRegion').val(0);
            $('#cboProvincia').val(0);
            $('#cboDistrito').val(0);
            $('#txtLugar').val("");
            $('#file_propuesta').val("");
            $('#hdCoord').val("");
            $("#hdPropuesta").val("");
            $("#propuesta").html("");
            
            $('#txtInvestigador').val("");
            //$("#c5").prop("checked", false);
            //$("#c6").prop("checked", true);
            $("#chkOCDE").prop("checked", false);
            $('#divLineasOCDE').hide();
            $('#hdDetalleTip').val("");
            $('#hdDetalleTipo').val("");
            $('#hdDetalleDed').val("");
            $('#hdDetalleInv').val("");
            var boton = document.getElementById("btnRegistrarInvestigador");
            var boton1 = document.getElementById("btnAgregarInv");
            //boton.disabled = false;
            boton1.disabled = false;
            fnDestroyDataTableDetalle('tbInvestigador');
            $('#pInvestigador').html('');
            //fnResetDataTableTramite('tbInvestigador', 0, 'desc');
            fnResetDataTableBasic('tbInvestigador', 0, 'asc');
        }

        function fnEditar(gru) {
            limpiar();
            $('#hdAccion').val("A");
            var y = fnBuscarG(gru);
            $('#hdGru').val(aDataGI[y].c_gru);
            $('#txtGrupo').val(aDataGI[y].d_gru);
            $('#cboLineasUSAT').val(aDataGI[y].c_lin);
//            if (aDataGI[y].c_tipo == 1) {
//                $("#c6").prop("checked", true);
//            }
//            if (aDataGI[y].c_tipo == 2) {
//                $("#c5").prop("checked", true);
//            }
            if (aDataGI[y].dis != 0) {
                $("#chkOCDE").prop("checked", true);
                $('#divLineasOCDE').show();
                llenarsubarea(aDataGI[y].c_are, "S", aDataGI[y].c_sub);
                llenarsubarea(aDataGI[y].c_sub, "D", aDataGI[y].dis);
                $('#cboArea').val(aDataGI[y].c_are);
            }else{
                $("#chkOCDE").prop("checked", false);
                $('#divLineasOCDE').hide();
            }
            fnListarProvincia(aDataGI[y].c_reg);
            fnListarDistrito(aDataGI[y].prov_gru);
            $('#cboRegion').val(aDataGI[y].c_reg);
            $('#cboProvincia').val(aDataGI[y].prov_gru);
            $('#cboDistrito').val(aDataGI[y].dist_gru);
            $('#txtLugar').val(aDataGI[y].lug_gru);
            $("#hdPropuesta").val(1);
            $('#divPlan').hide();
            
            if (aDataGI[y].cv_eve != "") {
                //$("#propuesta").html("<a href='" + aDataGI[y].prop_gru + "' target='_blank' style='font-weight:bold'>Descargar Plan</a>")
                $("#propuesta").html('<a onclick="fnDownload(\'' + aDataGI[y].prop_gru + '\')" target="_blank" style="font-weight:bold">Descargar Plan</a>')
            } else {
                $("#propuesta").html("");
            }
            $('#txtInvestigador').val("");
            var boton = document.getElementById("btnRegistrarInvestigador");
            var boton1 = document.getElementById("btnAgregarInv");
            if (aDataGI[y].d_iea != "REGISTRO") {
                if (aDataGI[y].d_iea == "OBSERVADO") {
                    boton.disabled = false;
                    boton1.disabled = false;
                    boton.innerHTML = "Actualizar Grupo";
                } else {
                    boton.disabled = true;
                    boton1.disabled = true;
                }               
            } else {
                boton.innerHTML = "Actualizar Grupo";
                boton.disabled = false;
                boton1.disabled = false;
            }
            
            var t = "";
            var c = "";
            var conta = 0;
            var conta1 = 0;
            var sele = "";
            listaRolInvestigador();
            listaGrupoInvestigadorDATA();
            fnDestroyDataTableDetalle('tbInvestigador');
            $('#pInvestigador').html('');
            for (i = 0; i < aDataGI.length; i++) {
                if (aDataGI[i].c_gru == gru) {
                    j = i;

                    if (i == 0) {
                        conta = 0;
                    }
                    conta1 = parseInt(conta) + parseInt(1);
                    $('#hdRowsDT').val(conta1);
                    conta = conta + 1;
                    t += '<tr id="fila' + conta1 + '">';
                    if (aDataGI[y].d_iea != "REGISTRO") {
                        if (aDataGI[y].d_iea == "OBSERVADO") {
                            t += '<td style="text-align:center"><a href="#" class="btn btn-red btn-xs" onclick="fnBorrarDT(\'' + conta1 + '\')" ><i class="ion-android-cancel"></i></a>';
                            t += '<input type="hidden" id="txtInv[' + conta1 + ']" name="txtInv[' + conta1 + ']" value="' + aDataGI[i].c_inv + '" >';
                            t += '<input type="hidden" id="txtTip[' + conta1 + ']" name="txtTip[' + conta1 + ']" value="' + "INV" + '" >';
                        } else {
                            t += '<td><a href="#" class="btn btn-red btn-xs" onclick="fnBorrarDT(\'' + conta1 + '\')" disabled><i class="ion-android-cancel"></i></a>';
                            t += '<input type="hidden" id="txtInv[' + conta1 + ']" name="txtInv[' + conta1 + ']" value="' + aDataGI[i].c_inv + '" >';
                            t += '<input type="hidden" id="txtTip[' + conta1 + ']" name="txtTip[' + conta1 + ']" value="' + "INV" + '" >';
                        }
                        
                    } else {
                        t += '<td style="text-align:center"><a href="#" class="btn btn-red btn-xs" onclick="fnBorrarDT(\'' + conta1 + '\')" ><i class="ion-android-cancel"></i></a>';
                        t += '<input type="hidden" id="txtInv[' + conta1 + ']" name="txtInv[' + conta1 + ']" value="' + aDataGI[i].c_inv + '" >';
                        t += '<input type="hidden" id="txtTip[' + conta1 + ']" name="txtTip[' + conta1 + ']" value="' + "INV" + '" >';
                    }
                    t += '<input type="hidden" id="txtDNI[' + conta1 + ']" name="txtDNI[' + conta1 + ']" value="' + aDataGI[i].c_dni + '" >';
                    t += '</td>';
                    t += '<td style="text-align:center">' + conta1 + '</td>';
                    t += '<td>' + aDataGI[i].d_nom + '</td>';
                    if (aDataGI[i].coord_dgi == 1) {
                        $('#hdCoord').val(aDataGI[y].c_inv);
                        t += '<td><input type="checkbox" id="chkC[' + (conta1) + ']" name="chkC[' + (conta1) + ']" value="' + (aDataGI[i].c_inv) + '" onclick="checkin(' + (conta1) + ')" checked/></td>';
                    } else {
                        t += '<td><input type="checkbox" id="chkC[' + (conta1) + ']" name="chkC[' + (conta1) + ']" value="' + (aDataGI[i].c_inv) + '" onclick="checkin(' + (conta1) + ')" disabled/></td>';
                    }                    
                    t += '<td style="text-align:center">' + aDataGI[i].c_dni + '</td>';
                    
                    //-- Reunion 2
                    //t += '<td><input type="text" id="txtD[' + conta1 + ']" name="txtD[' + conta1 + ']" value="' + aDataGI[j].d_ded + '" ></td>';
                    //c = '<select name="cboTipo[' + conta1 + ']" id="cboTipo[' + conta1 + ']">';
                    //if (aDataRI.length > 0) {
                    //    for (var k = 0; k < aDataRI.length; k++) {
                    //        if (aDataRI[k].c_rin == aDataGI[i].c_rin) {
                    //            sele = "selected"
                    //        } else {
                    //            sele = "";
                    //        }
                    //        c += '<option value="' + aDataRI[k].c_rin + '" ' + sele + '>' + aDataRI[k].d_rin + '</option>';
                    //    }
                    //}
                    //t += '<td>' + c + '</td>';
                    //-- Reunion 2
                    t += '</tr>';
                }
            }
            $('#pInvestigador').append(t);
            //fnResetDataTableTramite('tbInvestigador', 0, 'desc');
            fnResetDataTableBasic('tbInvestigador', 0, 'asc');

            fnListarObservaciones($("#hdGru").val(), 'GRU');
            
            $('div#mdEdicion').modal('show');
        }
/*
        function fnDownload(id_ar) {
            var flag = false;
            var form = new FormData();
            form.append("action", "Download2");
            form.append("IdArchivo", id_ar);
            // alert();
            //            console.log(form);
            $.ajax({
                type: "POST",
                url: "../DataJson/GestionInvestigacion/Operaciones.aspx",
                data: form,
                dataType: "json",
                cache: false,
                contentType: false,
                processData: false,
                success: function(data) {
                    console.log(data);
                    flag = true;

                    var file = 'data:application/octet-stream;base64,' + data[0].File;
                    var link = document.createElement("a");
                    link.download = data[0].Nombre;
                    link.href = file;
                    link.click();
                },
                error: function(result) {
                    console.log(result);
                    flag = false;
                }
            });
            return flag;
        }
*/
        function fnDownload(id_ar) {
            window.open("DescargarArchivo.aspx?Id=" + id_ar);
        }

        function downloadWithName(uri, name) {
            var link = document.createElement("a");
            link.download = name;
            link.href = uri;
            link.click();
        }

        function listaGrupoInvestigadorDATA() {
            $.ajax({
                type: "GET",
                //contentType: "application/json; charset=utf-8",
                url: "../DataJson/GestionInvestigacion/operaciones.aspx",
                data: { "action": "lGrupoInvestigadores", "param1": $("#hdCod").val() },
                dataType: "json",
                cache: false,
                async: false,
                success: function(data) {
                    aDataGI = data;
                },
                error: function(result) {
                    //alert(result);
                    fnMensaje("warning", result)
                }
            });

        }

        function listaGrupoInvestigador() {
            //alert($("#hdCod").val());
            var nombreGrupo = "";
            var tipoB = "PE";
            $.ajax({
                //type: "POST",
                type: "GET",
                //contentType: "application/json; charset=utf-8",
                url: "../DataJson/GestionInvestigacion/operaciones.aspx",
                data: { "action": "lGrupoInvestigadores", "param1": $("#hdCod").val() },
                dataType: "json",
                cache: false,
                async: false,
                success: function(data) {
                    //console.log("-----------");
                    //console.log(data);
                //console.log("-----------");
                //$("div#mostrar").html("");
                    var tb = '';
                    var i = 0;
                    var mostrar = '';
                    aDataGI = data;
                    var contador = 0;
                    var filas = aDataGI.length;
                    //alert("alert filas: " + filas);
                    if (filas > 0) {
                        for (i = 0; i < filas; i++) {
                            if (nombreGrupo != aDataGI[i].d_gru) {
                                contador = contador + 1;
                                tb += '<tr>';
                                tb += '<td style="text-align:center">' + (contador) + "" + '</td>';
                                tb += '<td style="text-align:left">' + aDataGI[i].d_gru + '</td>';
                                tb += '<td style="text-align:left">' + aDataGI[i].d_lin + '</td>';
                                tb += '<td style="text-align:left">' + aDataGI[i].d_nom + '</td>';
                                if (aDataGI[i].c_tipo == 1) {
                                    tb += '<td style="text-align:center">UNIDISCIPLINARIO</td>';
                                } else {
                                    tb += '<td style="text-align:center">MULTIDISCIPLINARIO</td>';
                                }
                                tb += '<td style="text-align:center">' + aDataGI[i].d_iea + '</td>';
                                tb += '<td style="text-align:center">';
                                tb += '<button type="button" id="btnE" name="btnE" class="btn btn-sm btn-info" onclick="fnEditar(' + aDataGI[i].c_gru + ')" title="Editar" ><i class="ion-eye"></i></button>';
                                if (aDataGI[i].d_iea != 'REGISTRO') {
                                    if (aDataGI[i].d_iea == 'OBSERVADO') {
                                        mostrar = "";
                                    } else {
                                        mostrar = "disabled";
                                    }
                                } else {
                                    mostrar = "";
                                }
                                tb += '<button type="button" id="btnEnviar" name="btnEnviar" class="btn btn-sm btn-orange" onclick="CambiarEstadoGrupoInvestigadorConfirmar(' + aDataGI[i].c_gru + ',\'' + 'E' + '\')" title="Aprobar Revisión" ' + mostrar + ' ><i class="ion-arrow-right-a"></i></button>';
                                tb += '<button type="button" id="btnD" name="btnD" class="btn btn-sm btn-red" onclick="CambiarEstadoGrupoInvestigadorConfirmar(' + aDataGI[i].c_gru + ',\'' + 'X' + '\')" title="Eliminar Grupo" ' + mostrar + '><i class="ion-close"></i></button>';
                                tb += '</td>';
                                tb += '</tr>';
                            }
                            nombreGrupo = aDataGI[i].d_gru;
                        }
                    }
                    
                    //$("div#mostrar").html(tb);
                
                    fnDestroyDataTableDetalle('tGrupoInvestigador');
                    //fnDestroyDataTableDetalle('tBonos');
                    //$('#tbGrupoInvestigador').append(tb);
                    $('#tbGrupoInvestigador').html(tb);
                    //fnResetDataTableTramite('tGrupoInvestigador', 0, 'asc');
                    fnResetDataTableBasic('tGrupoInvestigador', 0, 'asc');

                },
                error: function(result) {
                    //alert(result);
                    fnMensaje("warning", result)
                }
            });
        
        }

        function CambiarEstadoGrupoInvestigador1(reg, tip) {
            $("#hdGru").val(reg);
            $('#hdAccion').val(tip);
        }

        function CambiarEstadoGrupoInvestigadorConfirmar(reg, tip) {
            var y = fnBuscarG(reg);
            var mensaje = "";
            if (tip == "E") {
                mensaje = "enviar a evaluación ";
            }
            if (tip == "X") {
                mensaje = "eliminar";
            }
            fnMensajeConfirmarEliminar('top', "¿Desea " + mensaje + " el grupo: " + aDataGI[y].d_gru + "?", 'CambiarEstadoGrupoInvestigador', reg, tip);      
        }
        
        
        function CambiarEstadoGrupoInvestigador(reg, tip) {
            $("#hdGru").val(reg);
            $('#hdAccion').val(tip);
            var sw = 0;
            
            rpta = fnvalidaSession();
            if (rpta == true) {
            
                $.ajax({
                    type: "GET",
                    //contentType: "application/json; charset=utf-8",
                    url: "../DataJson/GestionInvestigacion/operaciones.aspx",
                    data: { "action": "actualizarGrupoInvestigador", "param1": $("#hdGru").val(), "param2": $("#hdAccion").val(), "param3": $("#hdUser").val() },
                    dataType: "json",
                    cache: false,
                    async: false,
                    success: function(data) {
                        //console.log("------");
                        //console.log(data);
                        if (data[0].Status == "success") {
                            //alert("aaa");
                            //fnMensaje("success", data[0].Message);
                            sw = 1;
                            //fnDestroyDataTableDetalle('tGrupoInvestigador');
                            //$('#tbGrupoInvestigador').html('');
                            fnMensaje(data[0].Status, data[0].Message);
                            //document.execCommand('ClearAuthenticationCache');

                            fnDestroyDataTableDetalle('tGrupoInvestigador');
                            $('#tbGrupoInvestigador').html('');

                            listaGrupoInvestigador();

                        }

                    },
                    error: function(result) {
                        //console.log(result);
                        fnMensaje("warning",result);
                    }
                });
                $('div#mdEdicionG').modal('hide');
                $('div#mdMensaje').modal('hide');
                return false;

            } else {
                window.location.href = rpta;
            }
        }


        function fnRegistrarInv() {
            //alert($('#hdAccion').val());
            $('#hdDetalleTip').val("");
            $('#hdDetalleTipo').val("");
            $('#hdDetalleDed').val("");
            $('#hdDetalleInv').val("");
            
            rpta = fnvalidaSession();
            if (rpta == true) {
            
            //arrayDetalleDedTipo();
            //alert($('#hdDetalleInv').val() + "-" + $('#hdDetalleTipo').val());
            //if ($('#hdAccion').val() == "R") {
                var sw = 0;
                var swT = 0;
                var mensaje = "";
                var arrayvalida = new Array();

                contadorTablaDetalle();
                if ($('#hdCoord').val() == "") {
                    swT = 1;
                    mensaje = "Elegir un coordinador para el grupo"
                }
                if ($('#hdRowsDT').val() == 0) {
                    swT = 1;
                    mensaje = "Añadir Colaboradores al grupo"
                }
                if ($('#hdRowsDT').val() == 1) {
                    swT = 1;
                    mensaje = "Mínimo 2 colaboradores en el grupo"
                }
//                if ($("#c6").prop("checked") == true) {
//                    $("#hdTipo").val("1");
//                }
//                if ($("#c5").prop("checked") == true) {
//                    $("#hdTipo").val("2");
//                }
                if ($("#chkOCDE").prop("checked") == true) {
                    if ($("#cboDisciplina").val() == 0) {
                        sw = 1;
                        mensaje = "Seleccione Disciplina";
                    }
                    if ($("#cboSubArea").val() == 0) {
                        sw = 1;
                        mensaje = "Seleccione Sub área";
                    }
                    if ($("#cboArea").val() == 0) {
                        sw = 1;
                        mensaje = "Seleccione Área Temática";
                    }
                } else {
                    $("#cboArea").val("0");
                    $("#cboSubArea").val("0");
                    $("#cboDisciplina").val("0");
                }

                if ($('#hdAccion').val() == "A") {
                    swCoordGrl = 1;
                } else {
                    if ($("#file_propuesta").val() == "") {
                        sw = 1;
                        mensaje = "Suba archivo de Propuesta";
                    }
                }
                
                if ($("#txtLugar").val() == "") {
                    sw = 1;
                    mensaje = "Ingrese Lugar";
                }
                if ($("#cboDistrito").val() == "0") {
                    sw = 1;
                    mensaje = "Seleccione Distrito";
                }
                if ($("#cboProvincia").val() == "0") {
                    sw = 1;
                    mensaje = "Seleccione Provincia";
                }
                if ($("#cboRegion").val() == "0") {
                    sw = 1;
                    mensaje = "Seleccione Departamento";
                }
                if ($("#cboLineasUSAT").val() == 0) {
                    sw = 1;
                    mensaje = "Seleccione Linea USAT";
                }
                if ($("#txtGrupo").val() == "") {
                    sw = 1;
                    mensaje = "Digite Nombre Grupo de Investigación";
                }

                //alert(sw);
                if (sw == 1) {
                    fnMensaje("error", mensaje);
                    return false;
                } else {
                    if (swT == 1) {
                        fnMensaje("error", mensaje);
                        return false;
                    } else {
                        var swGuardar;
                        //swGuardar = validaInputsTablaDetalle(); -- Reunion 2
                        swGuardar = 1; 
                        if (swGuardar == 0) {
                            fnMensaje("error", "Ingresar Datos en detalle del Colaborador");
                            return false;
                        } else {
                            var swCoordGrl;
                            //swCoordGrl = validaCoordGrlTablaDetalle(); -- Reunion 2
                            swCoordGrl = 1;
                            if (swCoordGrl == 0) {
                                fnMensaje("error", "Asignar un Coordinador General al grupo");
                                return false;
                            } else {

                                $("#DivGuardar").attr("style", "display:none");
                                $("#MensajeGuardar").attr("style", "display:block");
                                $("#MensajeGuardar").html('<b>Guardando...</b>');
                            
                                arrayDetalleDedTipo();
                                //alert($('#hdRowsDT').val() + "-" + $('#hdDetalleInv').val() + "-" + $('#hdDetalleTip').val() + "-" + $('#hdDetalleDed').val());
                                $("#action").val("gGrupoDetalleInvestigacion");
                                //alert("A");
                                var form = $('#frmRegistroGrupoInvestigacion').serialize();
                                $.ajax({
                                    type: "POST",
                                    //contentType: "application/json; charset=utf-8",
                                    url: "../DataJson/GestionInvestigacion/operaciones.aspx",
                                    data: form,
                                    dataType: "json",
                                    cache: false,
                                    async: false,
                                    success: function(data) {
                                        //console.log("a");
                                        //console.log(data);
                                        //document.execCommand('ClearAuthenticationCache');

                                        var boton = document.getElementById("btnRegistrarInvestigador");
                                        boton.disabled = false;

                                        //fnDestroyDataTableDetalle('tGrupoInvestigador');
                                        //$('#tbGrupoInvestigador').html('');

                                        if ($("#hdPropuesta").val() == 1 && $("#file_propuesta").val() != "") {
                                            fnCargarPropuesta(data[0].Code, "PR");

                                        } else {
                                            if ($("#hdPropuesta").val() == 0) {
                                                fnCargarPropuesta(data[0].Code, "PR");
                                            }
                                        }

                                        $("#hdPropuesta").val(0);
                                        listaGrupoInvestigador();
                                        limpiar();
                                        $("#DivGuardar").attr("style", "display:block");
                                        $("#MensajeGuardar").attr("style", "display:none");
                                        $("#MensajeGuardar").html('');
                                        fnMensaje(data[0].Status, data[0].Message);
                                        
                                    },
                                    error: function(result) {
                                        fnMensaje("warning", result);
                                        $("#DivGuardar").attr("style", "display:block");
                                        $("#MensajeGuardar").attr("style", "display:none");
                                        $("#MensajeGuardar").html('');
                                        //console.log(result); //--para errores
                                        //alert("A");
                                        return false;
                                    }

                                });
                                $('div#mdEdicion').modal('hide');
                                $('div#mdMensaje').modal('hide');
                                $('div#mdEdicionG').modal('hide');
                                return false;
                            }
                        }
                    }
                }

            //} else {
            //    if ($('#hdAccion').val() == "A") {
            //        contadorTablaDetalle();
            //        arrayDetalleDedTipo();
            //        alert($('#hdRowsDT').val() + "-" + $('#hdDetalleInv').val() + "-" + $('#hdDetalleTip').val() + "-" + $('#hdDetalleDed').val());
            //        return false;
            //    }
            //}

            } else {
                window.location.href = rpta;
                
            }
            
        }

        function fnCargarPropuesta(cod, tipo) {
            var flag = false;
            try {

                var data = new FormData();
                data.append("action", "SubirPropuestaGrupoNew");
                data.append("codigo", cod);
                data.append("tipo", tipo);
                
                if (tipo == "PR") {
                    var files = $("#file_propuesta").get(0).files;
                    if (files.length > 0) {
                        data.append("ArchivoASubir", files[0]);
                    }
                }
                if (files.length > 0) {
                    $.ajax({
                        type: "POST",
                        url: "../DataJson/GestionInvestigacion/operaciones.aspx",
                        data: data,
                        dataType: "json",
                        cache: false,
                        contentType: false,
                        processData: false,
                        async: false,
                        success: function(data) {
                            //alert("a");
                            flag = true;
                        },
                        error: function(result) {
                            //alert("b: " + result);
                            flag = false;
                        }
                    });
                }
                return flag;

            }
            catch (err) {
                return false;
            }
        }
        
        function validaCoordGrlTablaDetalle() {
            var swCG = 0;
            var cantCG = 0;
            for (var i = 1; i <= $('#hdRowsDT').val(); i++) {
                if ($('#cboTipo\\[' + i + '\\]').val() == 1) {
                    swCG = 1;
                    cantCG = cantCG + 1;
                }
            }
            if (cantCG > 1) {
                swCG = 0;
            }
            
            return swCG;
        }
        
        
        function validaInputsTablaDetalle() {
            var swVacio = 1;
            for (var i = 1; i <= $('#hdRowsDT').val(); i++) {
                if ($('#chkPos\\[' + i + '\\]').is(':checked')) {
                    //alert(i);
                } 
            }
            return swVacio;
        }


        function arrayDetalleDedTipo() {

            $('#hdDetalleInv').val("");
            $('#hdDetalleTip').val("");
            $('#hdDetalleTipo').val("");
            $('#hdDetalleDed').val("");
            var detalle = ""
            var detalle2 = ""
            var detalle3 = ""
            var detalle4 = ""
            var swr = 0;
            for (var i = 1; i <= $('#hdRowsDT').val(); i++) {
                //-- Reunion 2
                //if ($('#txtD\\[' + i + '\\]').val() != undefined || $('#txtD\\[' + i + '\\]').val() != '' || $('#cboTipo\\[' + i + '\\]').val() != undefined  || $('#cboTipo\\[' + i + '\\]').val() != 0) {
                    if (swr == 1) {
                        //json para Investigador
                        detalle3 = $('#hdDetalleInv').val();
                        detalle3 = detalle3 + ",";
                        $('#hdDetalleInv').val(detalle3);
                        //json para Dedicación
                        detalle = $('#hdDetalleTip').val();
                        detalle = detalle + ",";
                        $('#hdDetalleTip').val(detalle);
                        //json para Tipo
                        detalle2 = $('#hdDetalleDed').val();
                        detalle2 = detalle2 + ",";
                        $('#hdDetalleDed').val(detalle2);
                        //json para tesista, egresados
                        detalle4 = $('#hdDetalleTipo').val();
                        detalle4 = detalle4 + ",";
                        $('#hdDetalleTipo').val(detalle4);
                    }
                    swr = 1;
                    //json para Investigador
                    detalle3 = $('#hdDetalleInv').val();
                    detalle3 = detalle3 + $('#txtInv\\[' + i + '\\]').val();
                    
                    $('#hdDetalleInv').val(detalle3);
                    //json para Dedicación
                    detalle = $('#hdDetalleTip').val();
                    //detalle = detalle + $('#cboTipo\\[' + i + '\\]').val(); -- Reunion 2
                    detalle = detalle + "0";
                    $('#hdDetalleTip').val(detalle);

                    //json para Tipo
                    detalle2 = $('#hdDetalleDed').val();
                    //detalle2 = detalle2 + $('#txtD\\[' + i + '\\]').val(); -- Reunion 2
                    detalle2 = detalle2 + "0";
                    $('#hdDetalleDed').val(detalle2);

                    //json para Tipo
                    detalle4 = $('#hdDetalleTipo').val();
                    detalle4 = detalle4 + $('#txtTip\\[' + i + '\\]').val();
                    $('#hdDetalleTipo').val(detalle4);
                }
                //} -- Reunion 2

        }
        

        function fnDelRegistro() {
            //alert($('#param1').val());
            fnDestroyDataTableDetalle('tbInvestigador');
            var inveliminar = $('#txtInv\\[' + $('#param1').val() + '\\]').val();
            $("#fila" + $('#param1').val()).remove();
            fnResetDataTableTramite('tbInvestigador', 0, 'desc');            
            eliminarInvestigadorGrupo(inveliminar, $('#hdGru').val());
            //fnResetDataTableBasic('tbInvestigador', 0, 'asc');
            $('div#mdDelRegistro').modal('hide');
        }

        function eliminarInvestigadorGrupo(inveliminar, gru) {

            $.ajax({
                //type: "GET",
                type: "POST",
                //contentType: "application/json; charset=utf-8",
                url: "../DataJson/GestionInvestigacion/operaciones.aspx",
                data: { "action": "actualizarEliminaInvGrupoInvestigador", "param1": inveliminar, "param2": gru },
                dataType: "json",
                cache: false,
                async: false,
                success: function(data) {
                    if (data[0].Status == "success") {
                        //document.execCommand('ClearAuthenticationCache');
                        recargarTablaEliminacionEnEditar(gru);
                        fnMensaje(data[0].Status, data[0].Message);
                    }
                },
                error: function(result) {
                    //console.log(result);
                    fnMensaje("warning", result);
                }
            });
            
        }

        function recargarTablaEliminacionEnEditar(gru) {
            var t = "";
            var i = 0;
            var j = 0;
            var c = "";
            var conta = 0;
            var conta1 = 0;
            var sele = "";

            $.ajax({
                type: "GET",
                //contentType: "application/json; charset=utf-8",
                url: "../DataJson/GestionInvestigacion/operaciones.aspx",
                data: { "action": "lGrupoInvestigadores", "param1": $("#hdCod").val() },
                dataType: "json",
                cache: false,
                async: false,
                success: function(data) {
                    aDataGI = data;

                    fnDestroyDataTableDetalle('tbInvestigador');
                    $('#pInvestigador').html('');
                    //alert("aaa: " + aDataGI.length + "-" + aDataGI.length);
                    for (i = 0; i < aDataGI.length; i++) {
                        //alert(aDataGI[i].c_gru + '-' + $('#hdCod').val());
                        if (aDataGI[i].c_gru == gru) {
                            j = i;

                            if (i == 0) {
                                conta = 0;
                            }
                            conta1 = parseInt(conta) + parseInt(1);
                            $('#hdRowsDT').val(conta1);
                            conta = conta + 1;
                            t += '<tr id="fila' + conta1 + '">';
                            if (aDataGI[i].d_iea != "REGISTRO") {
                                if (aDataGI[i].d_iea == "OBSERVADO") {
                                    t += '<td style="text-align:center"><a href="#" class="btn btn-red btn-xs" onclick="fnBorrarDT(\'' + conta1 + '\')" ><i class="ion-android-cancel"></i></a>';
                                    t += '<input type="hidden" id="txtInv[' + conta1 + ']" name="txtInv[' + conta1 + ']" value="' + aDataGI[i].c_inv + '" >';
                                    t += '<input type="hidden" id="txtTip[' + conta1 + ']" name="txtTip[' + conta1 + ']" value="' + "INV" + '" >';
                                } else {
                                    t += '<td><a href="#" class="btn btn-red btn-xs" onclick="fnBorrarDT(\'' + conta1 + '\')" disabled><i class="ion-android-cancel"></i></a>';
                                    t += '<input type="hidden" id="txtInv[' + conta1 + ']" name="txtInv[' + conta1 + ']" value="' + aDataGI[i].c_inv + '" >';
                                    t += '<input type="hidden" id="txtTip[' + conta1 + ']" name="txtTip[' + conta1 + ']" value="' + "INV" + '" >';
                                }

                            } else {
                                t += '<td style="text-align:center"><a href="#" class="btn btn-red btn-xs" onclick="fnBorrarDT(\'' + conta1 + '\')" ><i class="ion-android-cancel"></i></a>';
                                t += '<input type="hidden" id="txtInv[' + conta1 + ']" name="txtInv[' + conta1 + ']" value="' + aDataGI[i].c_inv + '" >';
                                t += '<input type="hidden" id="txtTip[' + conta1 + ']" name="txtTip[' + conta1 + ']" value="' + "INV" + '" >';
                            }
                            t += '<input type="hidden" id="txtDNI[' + conta1 + ']" name="txtDNI[' + conta1 + ']" value="' + (aDataGI[i].c_dni) + '" >';
                            t += '</td>';
                            t += '<td style="text-align:center">' + conta1 + '</td>';
                            t += '<td>' + aDataGI[i].d_nom + '</td>';
                            if (aDataGI[i].coord_dgi == 1) {
                                $('#hdCoord').val(aDataGI[i].c_inv);
                                t += '<td><input type="checkbox" id="chkC[' + (conta1) + ']" name="chkC[' + (conta1) + ']" value="' + (aDataGI[i].c_inv) + '" onclick="checkin(' + (conta1) + ')" checked/></td>';
                            } else {
                                t += '<td><input type="checkbox" id="chkC[' + (conta1) + ']" name="chkC[' + (conta1) + ']" value="' + (aDataGI[i].c_inv) + '" onclick="checkin(' + (conta1) + ')" disabled/></td>';
                            }
                            t += '<td style="text-align:center">' + aDataGI[i].c_dni + '</td>';
                            t += '</tr>';
                        }
                    }
                    $('#pInvestigador').append(t);
                    fnResetDataTableBasic('tbInvestigador', 0, 'asc');

                },
                error: function(result) {
                    //alert(result);
                    fnMensaje("warning", result)
                }
            });

            
            
        }
        function contadorTablaDetalle() {
            var tableReg = document.getElementById('tbInvestigador');
            var searchText = 'No se ha encontrado informacion';
            var cellsOfRow = "";
            var compareWith = "";
            var contawors = "";
            for (var i = 1; i < tableReg.rows.length; i++) {
                cellsOfRow = tableReg.rows[i].getElementsByTagName('td');
                for (var j = 0; j < cellsOfRow.length; j++) {
                    compareWith = cellsOfRow[j].innerHTML;
                    if (searchText.length == 0 || (compareWith.indexOf(searchText) > -1)) {
                        contawors = 0;
                        $("#hdRowsDT").val(contawors);
                    } else {
                        $("#hdRowsDT").val(i);
                    }
                }
            }
        }
        
        function fnRestarDT(cont) {
            texto = document.getElementById('hdRowsDT').value;
            conta = parseInt(texto);
            if (conta > 0) {
                conta = conta - parseInt(1);
                $('#hdRowsDT').val(conta);
            } else {
                $('#hdRowsDT').val("0");
            }
            //alert("nro de detalles: "+$('#hdRowsDT').val());

        }  
        
        function fnAgregarInv() {
            contadorTablaDetalle();
            //alert($('#hdRowsDT').val());
            if ($('#txtInvestigador').val() == "" || c_per == "") {
                //error[5]
                //alert("INV");
                fnMensaje("error", "Digite y seleccione un colaborador para agregar");
                return false;
            } else {
                //return false;
                var t = "";
                var c = "";
                var conta = 0;
                var conta1 = 0;
                
                conta = $('#hdRowsDT').val();
                conta1 = parseInt(conta) + parseInt(1);
                $('#hdRowsDT').val(conta1);

                if (ValidadInvestigadorAdd(dni_per, conta) == true) {
                    fnDestroyDataTableDetalle('tbInvestigador');
                    t += '<tr id="fila' + conta1 + '">';
                    t += '<td style="text-align:center"><a href="#" class="btn btn-red btn-xs" onclick="fnBorrarDT(\'' + conta1 + '\')" ><i class="ion-android-cancel"></i></a>';
                    t += '<input type="hidden" id="txtInv[' + conta1 + ']" name="txtInv[' + conta1 + ']" value="' + c_per + '" >';
                    t += '<input type="hidden" id="txtTip[' + conta1 + ']" name="txtTip[' + conta1 + ']" value="' + d_tip + '" >';
                    t += '<input type="hidden" id="txtDNI[' + conta1 + ']" name="txtDNI[' + conta1 + ']" value="' + dni_per + '" >';
                    t += '</td>';
                    t += '<td style="text-align:center">' + conta1 + '</td>';
                    t += '<td>' + $('#txtInvestigador').val() + '</td>';
                    t += '<td style="text-align:center;"><input type="checkbox" id="chkC[' + (conta1) + ']" name="chkC[' + (conta1) + ']" value="' + (c_per) + '" onclick="checkin(' + (conta1) + ')"/></td>';
                    t += '<td>' + dni_per + '</td>';

                    //-- Reunion 2
                    //t += '<td><input type="text" id="txtD[' + conta1 + ']" name="txtD[' + conta1 + ']" value="" ></td>';
                    //c = '<select name="cboTipo[' + conta1 + ']" id="cboTipo[' + conta1 + ']">';
                    //if (aDataRI.length > 0) {
                    //    for (var i = 0; i < aDataRI.length; i++) {
                    //        c += '<option value="' + aDataRI[i].c_rin + '" ' + aDataRI[i].selected + '>' + aDataRI[i].d_rin + '</option>';
                    //    }
                    //}
                    //t += '<td>' + c + '</td>';
                    //-- Reunion 2
                    t += '</tr>';
                    
                    $('#pInvestigador').append(t);
                    //fnResetDataTableTramite('tbInvestigador', 0, 'desc');
                    fnResetDataTableBasic('tbInvestigador', 0, 'asc');

                    $('#txtInvestigador').val("");
                    $("#txtInvestigador").focus();
                    return false;
                
                }else{
                    fnMensaje("warning", "Ya se encuentra agregado, por favor validar.");
                    $('#txtInvestigador').val("");
                    $("#txtInvestigador").focus();
                    return false;
                }

            }

        }

        function ValidadInvestigadorAdd(reg, cont) {
            for (var i = 0; i <= cont; i++) {
                if ($('#txtDNI\\[' + i + '\\]').val() == reg) {
                    return false;
                }
            }
            return true;
        }

        function fnAgregar() {
            fnValidaInvestigador();
            if ($("#hdValidaInv").val() == "1") {
                limpiar();

                $('#divPlan').show();
                 
                equipo = [];
                var arr = fnPersonal(-1, '%');
                equipo.push({
                    per: arr[0].c_per,
                    inv: arr[0].c_inv,
                    nombre: arr[0].d_per,
                    dni: arr[0].dni_per,
                    estado: 1
                });
                var tb;
                for (i = 0; i < equipo.length; i++) {
                    if (equipo[i].estado == 1) {
                        tb += '<tr>';
                        tb += '<td style="text-align:center" width="5%">';
                        tb += '<input type="hidden" id="txtInv[' + (i + 1) + ']" name="txtInv[' + (i + 1) + ']" value="' + (equipo[i].inv) + '" ></td>';
                        tb += '<input type="hidden" id="txtTip[' + (i + 1) + ']" name="txtTip[' + (i + 1) + ']" value="' + "INV" + '" >';
                        tb += '<input type="hidden" id="txtDNI[' + (i + 1) + ']" name="txtDNI[' + (i + 1) + ']" value="' + (equipo[i].dni) + '" >';
                        tb += '<td style="text-align:center" width="5%">' + (i + 1) + '</td>';
                        tb += '<td>' + equipo[i].nombre + '</td>';
                        tb += '<td style="text-align:center" width="5%"><input type="checkbox" id="chkC[' + (i + 1) + ']" name="chkC[' + (i + 1) + ']" value="' + (equipo[i].inv) + '" onclick="checkin(' + (i + 1) + ')"/></td>';
                        tb += '<td>' + equipo[i].dni + '</td>';
                        tb += '</tr>';
                    }
                }
                fnDestroyDataTableDetalle('tbInvestigador');
                $('#pInvestigador').html(tb);
                fnResetDataTableBasic('tbInvestigador', 0, 'asc');

                $('#hdAccion').val("R");
                $("#DivObservaciones").html("");
                var boton = document.getElementById("btnRegistrarInvestigador");
                boton.innerHTML = "Registrar Grupo";
                $('div#mdEdicion').modal('show');
                return false;
            } else {
                var contenido = '';
                contenido = contenido + "<center>Debe registrarse como colaborador(a) con actividad de investigación</center><br>";
                contenido = contenido + "<center><a href='../GestionInvestigacion/frmRegistroInvestigadores.aspx?id=" + $("#hdId").val() + "&ctf=" + $("#hdCtf").val() + "'>Registro de colaborador(a) con actividad de investigación</a></center><br>"
                $('#ContenidoMensajeValidaInv').html(contenido);
                $('div#mdEdicion').modal('show');
                return false;
            }
        }

        function checkin(index) {
            contadorTablaDetalle();
            conta = $('#hdRowsDT').val();
            if ($('#chkC\\[' + index + '\\]').is(':checked')) {
                var coord = $('#chkC\\[' + index + '\\]').val();
                $("#hdCoord").val(coord);
                for (i = 1; i <= conta; i++) {
                    if (i != index) {
                        document.getElementById('chkC[' + i + ']').checked = false;
                        document.getElementById('chkC[' + i + ']').disabled = true;
                    } else {
                        document.getElementById('chkC[' + i + ']').checked = true;
                        document.getElementById('chkC[' + i + ']').disabled = false;
                    }
                }
            } else {
                $("#hdCoord").val("");
                for (i = 1; i <= conta; i++) {
                    document.getElementById('chkC[' + i + ']').disabled = false;
                }
            }
            

        }

        function fnPersonal(ctf, texto) {
            var arr;
            $.ajax({
                type: "POST",
                url: "../DataJson/GestionInvestigacion/operaciones.aspx",
                data: { "action": "validaInvestigador", "param1": $("#hdUser").val(), "param2": "VA" },
                dataType: "json",
                cache: false,
                async: false,
                success: function(data) {
                    //console.log(data);
                    arr = data;
                },
                error: function(result) {
                    //console.log(result)
                    arr = result;
                }
            });

            return arr;
        }
        
        function fnBorrarDT(cont) {
            document.getElementById('param1').value = cont;
            $('div#mdDelRegistro').modal('show');
            return true;
        }

        function fnCargaInvestigadores(param1, param2) {
            try {
                var arr;
                $.ajax({
                    type: "GET",
                    contentType: "application/json; charset=utf-8",
                    url: "../DataJson/GestionInvestigacion/operaciones.aspx",
                    //data: { "action": "lInvestigadores", "param1": param1, "param2": param2 }, -- 
                    data: { "action": "lInvestigadorTesistaGraduado"},
                    async: false,
                    cache: false,
                    success: function(data) {
                        arr = data;
                    },
                    error: function(result) {
                        arr = null;
                    }
                })
                return arr;
            }
            catch (err) {
                //alert(err.message);
                //console.log('error');
                fnMensaje("warning", err);
            }
        }


        function listaRolInvestigador() {
            $("#action").val("lRolInvestigador");
            var form = $('#frmRegistroGrupoInvestigacion').serialize();
            //alert($("#action").val());
            $.ajax({
                type: "GET",
                //contentType: "application/json; charset=utf-8",
                url: "../DataJson/GestionInvestigacion/operaciones.aspx",
                data: { "action": $("#action").val() },
                dataType: "json",
                cache: false,
                async: false,
                success: function(data) {
                    aDataRI = data;
                    //console.log(data);
                },
                error: function(result) {
                    sOut = '';
                    //alert(result[0].msje);
                }
            });
        }
        
        function listaArea() {
            $("#action").val("lAreaConocimientosOCDE");
            var form = $('#frmRegistroGrupoInvestigacion').serialize();
            //alert($("#action").val());
            $.ajax({
                type: "GET",
                //contentType: "application/json; charset=utf-8",
                url: "../DataJson/GestionInvestigacion/operaciones.aspx",
                data: { "action": $("#action").val(), "param1": 0, "param2": "AR" },
                dataType: "json",
                cache: false,
                async: false,
                success: function(data) {
                    aDataA = data;
                    //console.log("------");
                    //console.log(data);
                    //console.log("------");
                    var i = 0;
                    var t = '';
                    if (aDataA.length > 0) {
                        for (var i = 0; i < aDataA.length; i++) {
                            t += '<option value="' + aDataA[i].codigo + '" selected="' + aDataA[i].selected + '">' + aDataA[i].descripcion + '</option>';
                        }
                    }
                    //console.log(t);
                    //console.log("------");
                    $('#cboArea').html(t);
                },
                error: function(result) {
                    sOut = '';
                    //console.log(result);
                    fnMensaje("warning",result)
                    //alert(result[0].msje);
                }
            });
        }


        function fnBuscarG(c) {
            var i;
            var j = -1;
            var l;
            l = aDataGI.length;
            for (i = 0; i < l; i++) {
                if (aDataGI[i].c_gru == c) {
                    j = i;
                    return j;
                }
            }
        }
        
        
    </script>


</head>
<body>

<form id="frmRegistroGrupoInvestigacion" name="frmRegistroGrupoInvestigacion">	
    <input type="hidden" id="action" name="action" value="" />   
    <input type="hidden" id="hdUser" name="hdUser" value="" runat="server"/>   
    <input type="hidden" id="hdRowsDT" name="hdRowsDT" value="" />
    <input type="hidden" id="param1" name="param1" value="" />
    <input type="hidden" id="hdDetalleDed" name="hdDetalleDed" value="" />
    <input type="hidden" id="hdDetalleTip" name="hdDetalleTip" value="" />
    <input type="hidden" id="hdDetalleInv" name="hdDetalleInv" value="" />
    <input type="hidden" id="hdTipo" name="hdTipo" value="0" />
    
    <input type="hidden" id="hdCod" name="hdCod" value="" runat="server"/>
    <input type="hidden" id="hdAccion" name="hdAccion" value="" runat="server"/>
    <input type="hidden" id="hdGru" name="hdGru" value="" runat="server"/>

    <input type="hidden" id="hdCtf" name="hdCtf" value="" runat="server" />   
    <input type="hidden" id="hdId" name="hdId" value="" runat="server" />   
    
    <input type="hidden" id="hdValidaInv" name="hdValidaInv" value="" runat="server" />   
    <input type="hidden" id="hdCoord" name="hdCoord" value="" runat="server" />   
    <input type="hidden" id="hdPropuesta" name="hdPropuesta" value="0" runat="server" />   
    
    <input type="hidden" id="hdDetalleTipo" name="hdDetalleTipo" value="" />
    
    <!--
    <div class="piluku-preloader text-center hidden">
        <div class="loader">
            Loading...</div>
    </div>-->
    <div class="wrapper">
        <div class="content">
            <!--<div class="overlay">
            </div>-->
            <div class="main-content">
                <div class="row">
                    <div class="manage_buttons">
                        <div class="row">
                            <div id="PanelBusqueda">
                                <div class="col-md-12 col-sm-12 col-lg-12">
                                    <div class="page_header">
                                        <div class="pull-left">
                                            <i class="icon ti-bookmark-alt page_header_icon"></i>
                                            <span class="main-text">Grupo de Investigación </span>
                                        </div>
                                        
                                        <div class="buttons-list">
                                            <div class="pull-right-btn">
                                                <button class="btn btn-green" id="btnAgregar" value="Agregar" >
                                                    Agregar</button>
                                            </div>
                                        </div>
                                        
                                    </div>
                                </div>
                            </div>
                        </div>
                        
                                        <div class="row">
                    <div class="panel-piluku">
                        <div class="panel-heading">
                            <h3 class="panel-title">
                                Grupo de Investigaci&oacute;n
                            </h3>
                        </div>
                        
                        <!-- JR 
                        <div id="mostrar"></div>-->
                        
                        <div class='table-responsive'>	        
                            <div class='panel-body' >
                               <div class='table-responsive'>
                                <!--Default Form-->
                                                   
                                    <div id="tBonos_wrapper" class="dataTables_wrapper" role="grid">
                                        <table id="tGrupoInvestigador" name="tGrupoInvestigador" class="display dataTable" width="100%">
                                            <thead>
                                                <tr>
                                                    <td width="5%" style="font-weight: bold; width: 54px; text-align:center" class="sorting_asc"
                                                        tabindex="0" rowspan="1" colspan="1" aria-sort="ascending" aria-label="N°: activate to sort column descending">
                                                        N°
                                                    </td>
                                                    <td width="18%" style="font-weight: bold; width: 89px; text-align:center" class="sorting"
                                                        tabindex="0" rowspan="1" colspan="1" aria-label="Nombre: activate to sort column ascending">
                                                        Grupo
                                                    </td>
                                                    <td width="18%" style="font-weight: bold; width: 89px; text-align:center" class="sorting"
                                                        tabindex="0" rowspan="1" colspan="1" aria-label="Nombre: activate to sort column ascending">
                                                        Línea USAT
                                                    </td>
                                                    <td width="18%" style="font-weight: bold; width: 89px; text-align:center" class="sorting"
                                                        tabindex="0" rowspan="1" colspan="1" aria-label="Nombre: activate to sort column ascending">
                                                        Responsable
                                                    </td>
                                                    <td width="18%" style="font-weight: bold; width: 89px; text-align:center" class="sorting"
                                                        tabindex="0" rowspan="1" colspan="1" aria-label="Nombre: activate to sort column ascending">
                                                        Tipo
                                                    </td>
                                                    <td width="8%" style="font-weight: bold; width: 169px; text-align:center" class="sorting"
                                                        tabindex="0" rowspan="1" colspan="1" aria-label="Tipo de Estudio: activate to sort column ascending">
                                                        Estado
                                                    </td>
                                                    <td width="11%" style="font-weight: bold; width: 111px; text-align:center" class="sorting"
                                                        tabindex="0" rowspan="1" colspan="1" aria-label="Opciones: activate to sort column ascending">
                                                        Opciones
                                                    </td>
                                                </tr>
                                            </thead>
                                            <tfoot>
                                                <tr>
                                                    <th colspan="7" rowspan="1">
                                                    </th>
                                                </tr>
                                            </tfoot>
                                            <tbody id="tbGrupoInvestigador">
                                            </tbody>
                                        </table>
                                    </div>
                                    
                                <!--Default Form-->           
                            </div>              
                        </div> 
                            
                        <br />
                        </div>
                                                
                                                
                        
                        
                        
                        <!-- JR -->
                    <div class="modal fade" id="mdEdicion" role="dialog" aria-labelledby="myModalLabel"
                        style="z-index: 0;" aria-hidden="true" data-backdrop="static" data-keyboard="false">
                        <div class="modal-dialog modal-lg" id="modalRegI">
                        
                            <div class="modal-content">
                                <div class="modal-header" style="background-color: #E33439; color: White; font-weight: bold;">
                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close" style="float: right;">
                                        <span aria-hidden="true" class="ti-close" style="color: White;"></span>
                                    </button>
                                    <h4 class="modal-title" id="myModalLabel3">
                                        Grupo de Investigación</h4>
                                </div>
                                
                                <div class="modal-body">
                                
                                    <div class="form-group">
                                        <div class="col-md-12" id="ContenidoMensajeValidaInv">
                                            <div id="DivObservaciones">
                                            </div>
                                            <div class="row">
                                                <label class="col-md-2 control-label ">
                                                    Nombre Grupo</label>
                                                <div class="col-md-7">
                                                    <input name="txtGrupo" type="text" id="txtGrupo" value="" class="form-control"  runat="server" placeholder="Digitar nombre de grupo"/>
                                                </div>
                                                
                                                <div class="col-md-2">
                                                    <div id="DivGuardar"> 
                                                        <button class="btn btn-primary" id="btnRegistrarInvestigador" value="Registrar" >
                                                        Registrar Grupo</button>
                                                    </div>
                                                    <div class="alert alert-success" id="MensajeGuardar" style="display: none;">                                                        
                                                    </div>
                                                </div>
                                                
                                            </div>
                                            <!--<div class="row">
                                                <label class="col-md-2 control-label ">
                                                    Tipo</label>
                                                <ul class="list-inline checkboxes-radio">
                                                    <li class="ms-hover">
									                    <input type="radio" name="active" id="c6" checked="">
									                    <label for="c6"><span></span>Unidisciplinario</label>
								                    </li>
								                    <li class="ms-hover">
									                    <input type="radio" name="active" id="c5">
									                    <label for="c5"><span></span>Multidisciplinario</label>
								                    </li>
							                    </ul>
                                            </div>-->
                                            <div class="row">
                                                <label class="col-md-2 control-label ">
                                                    Líneas USAT</label>
                                                <div class="col-md-7">
                                                    <select name="cboLineasUSAT" class="form-control" id="cboLineasUSAT">
                                                        <!--<option value="0" selected="">-- Seleccione -- </option>-->
                                                    </select>
                                                </div>
                                            </div>
                                            <div class="row">
                                                    <label class="col-md-2 control-label ">
                                                        Líneas OCDE</label>
                                                    <div class="col-md-1">
                                                        <input type="checkbox" id="chkOCDE" name="chkOCDE" value="0" data-validation="checkbox_group" data-validation-qty="1-2" runat="server">
                                                    </div>
                                            </div>
                                            <div class="row" id="divLineasOCDE">
                                                    <div class="row">
                                                        <label class="col-md-2 control-label ">
                                                            &Aacute;rea Tem&aacute;tica</label>
                                                        <div class="col-md-7">
                                                            <select name="cboArea" class="form-control" id="cboArea">
                                                                <!--<option value="0" selected="">-- Seleccione -- </option>-->
                                                            </select>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <label class="col-md-2 control-label ">
                                                            Sub &Aacute;rea</label>
                                                        <div class="col-md-7">
                                                            <select name="cboSubArea" class="form-control" id="cboSubArea">
                                                                <option value="0" selected="">-- Seleccione -- </option>
                                                            </select>
                                                        </div>
                                                    </div> 
                                                    <div class="row">
                                                        <label class="col-md-2 control-label ">
                                                            Disciplina</label>
                                                        <div class="col-md-7">
                                                            <select name="cboDisciplina" class="form-control" id="cboDisciplina">
                                                                <option value="0" selected="">-- Seleccione -- </option>
                                                            </select>
                                                        </div>
                                                    </div>
                                            </div>
                                            <div class="row">
                                                <div class="form-group">
                                                    <label class="col-sm-2 control-label">
                                                        Aplicación:</label>
                                                    <div class="col-md-3">
                                                        <select id="cboRegion" name="cboRegion" class="form-control">
                                                            <option value="" selected="">-- Seleccione -- </option>
                                                        </select>
                                                    </div>
                                                    <label class="col-md-1 control-label">
                                                        Provincia:</label>
                                                    <div class="col-md-3">
                                                        <select id="cboProvincia" name="cboProvincia" class="form-control">
                                                            <option value="" selected="">-- Seleccione -- </option>
                                                        </select>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="form-group">
                                                    <label class="col-md-2 control-label">
                                                        Distrito:</label>
                                                    <div class="col-md-3">
                                                        <select id="cboDistrito" name="cboDistrito" class="form-control">
                                                            <option value="" selected="">-- Seleccione -- </option>
                                                        </select>
                                                    </div>
                                                    <label class="col-md-1 control-label">
                                                        Lugar:</label>
                                                    <div class="col-md-3">
                                                        <input type="text" id="txtLugar" name="txtLugar" class="form-control" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="form-group">
                                                    <label class="col-sm-2 control-label">
                                                        Plan de Acción(PDF)</label>
                                                    <div class="col-sm-5">
                                                        <input type="file" id="file_propuesta" name="file_propuesta" />
                                                        <div id="propuesta">
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-2" id="divPlan">
                                                        <a href='Archivos/Concursos/Estructura/PLAN DE ACCIÓN DEL GRUPO DE INVESTIGACIÓN.docx' target='_blank' style='font-weight:bold'>Descargar Plan</a>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row" >
                                                <label class="col-md-2 control-label ">
                                                    Colaborador</label>
                                                <div class="col-md-7">
                                                    <input name="txtInvestigador" type="text" id="txtInvestigador" value="" class="form-control"  runat="server" placeholder="Digitar Nombres/Apellidos para Buscar"/>
                                                </div>
                                                <div class="col-md-2">
                                                    <div class="pull-right-btn">
                                                    <button class="btn btn-green" id="btnAgregarInv" value="AgregarInv" >
                                                    Agregar Colaborador</button>
                                                    </div>
                                                </div>
             
                                            </div>
                                            
                                            <br />
                                            
                                            <div class="row">    
                                                <div class="col-sm-12">
                                                    <div class="table-responsive" style="font-size: 10px; font-weight: 300; line-height: 18px;">
                                                        <div id="Div2" class="dataTables_wrapper" role="grid">
                                                        
                                                        
                                                            <table id="tbInvestigador" name="tbInvestigador" class="display dataTable" width="100%">
                                                            <thead>
                                                                <tr role="row">
                                                                    <td width="5%" style="font-weight: bold; width: 54px; text-align: center" class="sorting_asc"
                                                                        tabindex="0" rowspan="1" colspan="1" aria-sort="ascending" aria-label="N°: activate to sort column descending">
                                                                        
                                                                    </td>
                                                                    <td width="5%" style="font-weight: bold; width: 54px; text-align: center" class="sorting_asc"
                                                                        tabindex="0" rowspan="1" colspan="1" aria-sort="ascending" aria-label="N°: activate to sort column descending">
                                                                        Nro
                                                                    </td>
                                                                    <td width="50%" style="font-weight: bold; width: 89px; text-align: center" class="sorting"
                                                                        tabindex="0" rowspan="1" colspan="1" aria-label="Nombre: activate to sort column ascending">
                                                                        Colaborador
                                                                    </td>
                                                                    <td width="5%" style="font-weight: bold; width: 89px; text-align: center" class="sorting"
                                                                        tabindex="0" rowspan="1" colspan="1" aria-label="Nombre: activate to sort column ascending">
                                                                        Coord.
                                                                    </td>
                                                                    <td width="5%" style="font-weight: bold; width: 89px; text-align: center" class="sorting"
                                                                        tabindex="0" rowspan="1" colspan="1" aria-label="Nombre: activate to sort column ascending">
                                                                        DNI
                                                                    </td>
                                                                    
                                                                    
                                                                </tr>
                                                            </thead>
                                                            <tfoot>
                                                                <tr>
                                                                    <th colspan="6" rowspan="1">
                                                                    </th>
                                                                </tr>
                                                            </tfoot>
                                                            <tbody id="pInvestigador">
                                                            </tbody>
                                                        </table>
            
            

                                                                
                                                                
                                                        </div>
                                                    </div>
                                                </div>
                                    
                                            </div>
                                            
                                            
                                        </div>
                                    </div>

                                </div>
                                
                                <div class="modal-footer">
                                    <center>
                                        <button type="button" class="btn btn-danger" id="btnCancelarReg" data-dismiss="modal">
                                            Cancelar</button>
                                    </center>
                                </div>
                            </div>
                        </div>
                        

                        
                    </div>
                </div>

                        <!-- JR -->
                    </div>
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


