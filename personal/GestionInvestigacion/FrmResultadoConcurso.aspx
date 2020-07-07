<%@ Page Language="VB" AutoEventWireup="false" CodeFile="FrmResultadoConcurso.aspx.vb" Inherits="GestionInvestigacion_FrmResultadoPostulacion" %>
<!DOCTYPE html>
<html>
<head>
    <title>Resultado Final Concurso</title>
    <%--Compatibilidad con IE--%>
	<!-- custom scrollbar stylesheet -->
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta http-equiv='X-UA-Compatible' content='IE=7' />
    <meta http-equiv='X-UA-Compatible' content='IE=8' />
    <meta http-equiv='X-UA-Compatible' content='IE=10' />
    <meta http-equiv="X-UA-Compatible" content="IE=edge"/> 
    
	<meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=0"/> <!--320-->
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
	
	
    <script src="../assets/js/bootstrap-datepicker.js" type="text/javascript"></script>

    <style type="text/css">
        td.details-control {
	        background: url('../assets/img/details_open.png') no-repeat center center;
	        cursor: pointer;
	        font-size:small;
        }
        tr.shown td.details-control {
	        background: url('../assets/img/details_close.png') no-repeat center center;
        }
	</style>
	

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
            padding-top: 5px;
        }
    </style>
    
    <script type="text/javascript">
        var aDataPost = [];
        var aDataCon = [];

        $(document).ready(function() {
            //fnResetDataTableTramite('tPostulacion', 0, 'desc');
            rpta = fnvalidaSession();
            if (rpta == false) {
                window.location.href = rpta;
            }
        
            var dt = fnCreateDataTableBasic('tEvaluadores', 1, 'asc');
            fnListarConcursos();
            cLineas();
            listarEvaluadoresExternos('C', $("#cboLinea").val());

            $("#hdcodPos").val("");
            $("#cboLinea").change(function() {
                listarEvaluadoresExternos('C', $(this).val());
            })

            $("#cboConcurso").change(function() {
            //if ($(this).val() != '') {
                rpta = fnvalidaSession();
                if (rpta == true) {
            
                    var txt = $(this).val();
                    var y = fnBuscar(txt);
                    $("#hdCON1").val(aDataCon[y].cod_con1);
                    //alert($("#hdCON1").val());
                    $.post("FrmAjaxTablaPostResultadoFinal.aspx",
                        {
                            param1: txt
                        },
                        function(data, status) {
                            //alert("Data: " + data + "\nStatus: " + status);
                            $('#divTablePostulaciones').html(data);
                            $('#divTablePostulaciones').show();
                        });
                        $("#hdCON").val(txt);
                    //}

                } else {
                    window.location.href = rpta;
                }

            });

            $('[data-toggle="popover"]').popover();
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

        /*function fnDownload(id_ar) {
            var flag = false;
            var form = new FormData();
            form.append("action", "Download2");
            form.append("IdArchivo", id_ar);
//            alert("a");
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
        }*/


        function fnDownload(id_ar) {
            window.open("DescargarArchivo.aspx?Id=" + id_ar);
        }
        
        function fnBuscar(c) {
            var i;
            var j = -1;
            var l;
            l = aDataCon.length;
            for (i = 0; i < l; i++) {
                if (aDataCon[i].cod_con == c) {
                    j = i;
                    return j;
                }
            }
        }
        function fnAgregarEve(cod) {
            rpta = fnvalidaSession();
            if (rpta == true) {
                fnDestroyDataTableDetalle('tEvaluadores');
                $('#tbEvaluadores').html('');
                
                $("#hdcodPos").val(cod);
                //alert(cod);
                $.ajax({
                    type: "POST",
                    url: "../DataJson/GestionInvestigacion/operaciones.aspx",
                    data: { "action": "ListaEvaluadoresRF", "param1": cod },
                    dataType: "json",
                    cache: false,
                    async: false,
                    success: function(data) {
                        //console.log("--------------------");
                        //console.log(data);
                        var tb = '';
                        var i = 0;
                        var filas = data.length;
                        if (filas > 0) {
                            for (i = 0; i < filas; i++) {
                                tb += '<tr>';
                                tb += '<td style="text-align:center">' + (i + 1) + "" + '</td>';
                                tb += '<td>' + data[i].nombre + '</td>';
                                tb += '<td>' + data[i].dina + '</td>';
                                //tb += '<td style="text-align:center">';
                                //tb += '<button type="button" id="btnE" name="btnE" class="btn btn-sm btn-danger" onclick="fnQuitar(\'' + data[i].cod + '\')" title="Ver Evaluadores" disabled><i class="ion-android-delete"></i></button>';
                                //tb += '</td>';
                                tb += '</tr>';
                            }
                        }

                        fnDestroyDataTableDetalle('tEvaluadores');
                        $('#tbEvaluadores').append(tb);
                        //fnResetDataTableTramite('tEvaluadores', 0, 'asc');
                        fnResetDataTableBasic('tEvaluadores', 0, 'asc');


                        $("#cboEvaluador").val("");
                        $("#cboLinea").val("");
                        $("#mdEvaluadores").modal("show");
                    },
                    error: function(result) {
                        fnMensaje("warning", result)
                    }
                });

            } else {
                window.location.href = rpta;
            }

        }

        function fnQuitar(cod) {
            $("form#frmbuscar input[id=action]").remove();
            $("form#frmbuscar input[id=hdcod]").remove();
            $('form#frmbuscar').append('<input type="hidden" id="action" name="action" value="' + ope.qev + '" />');
            $('form#frmbuscar').append('<input type="hidden" id="hdcod" name="hdcod" value="' + cod + '" />');
            var form = $("#frmbuscar").serializeArray();
            $("form#frmbuscar input[id=action]").remove();
            $("form#frmbuscar input[id=hdcod]").remove();
            $.ajax({
                type: "POST",
                url: "../DataJson/GestionInvestigacion/Postulacion.aspx",
                data: form,
                dataType: "json",
                cache: false,
                async: false,
                success: function(data) {
                    if (data[0].rpta == 1) {
                        fnMensaje("success", data[0].msje);
                        fnEvaluadores($("#hdcodPos").val());
                        fnListarPostulacion($("#cboConcurso").val());
                    } else {
                        fnMensaje("error", data[0].msje)
                    }
                },
                error: function(result) {
                    fnMensaje("warning", result)
                }
            });
        }


        function fnOrdenMerito(orden) {
            document.getElementById("divMensaje").innerHTML = "";
            $('div#mdMensaje').modal('show');
            divOrdenMerito
        }

        function fnGanador(cod) {
            rpta = fnvalidaSession();
            if (rpta == true) {
                var sw = 0;
                var mensaje = '';
                $('#txtNotaGlobal\\[' + cod + '\\]').removeAttr('disabled');
                //alert($('#txtNotaGlobal\\[' + cod + '\\]').val());
                if ($('#txtNotaGlobal\\[' + cod + '\\]').val() == "" || $('#txtNotaGlobal\\[' + cod + '\\]').val() == "0") {
                    sw = 1;
                    mensaje = 'Pf. Genere Nota Final de la postulación';
                }
                if (sw == 1) {
                    fnMensaje("warning", mensaje);
                    return false;
                } else {
                    //alert(cod+ "-" + $("#cboConcurso").val() + "-" + $("#hdId").val());
                    $.ajax({
                        type: "POST",
                        url: "../DataJson/GestionInvestigacion/operaciones.aspx",
                        data: { "action": "gGanadorConcursoPostulaciones", "param1": cod, "param2": $("#cboConcurso").val(), "param3": $("#hdId").val() },
                        dataType: "json",
                        cache: false,
                        async: false,
                        success: function(data) {
                            //console.log("------");
                            //console.log(data);
                            if (data[0].Status == "success") {
                                //EnviarEmail(cod, "POS", "GANADOR", $("#cboConcurso").text());
                                fnMensaje("success", data[0].Message);
                                //fnEvaluadores($("#hdcodPos").val());
                                RecargarTablaAjax();
                            } else {
                                fnMensaje("error", data[0].Message)
                            }
                        },
                        error: function(result) {
                            fnMensaje("warning", result)
                        }
                    });
                }
            } else {
                window.location.href = rpta;
            }
        }

        function EnviarEmail(cod, tipo, estado, descripcion) {
            $.ajax({
                type: "GET",
                contentType: "application/json; charset=utf-8",
                url: "../DataJson/GestionInvestigacion/operaciones.aspx",
                data: { "action": "SubmitEMail", "param1": cod, "param2": tipo, "param3": estado, "param4": descripcion },
                dataType: "json",
                success: function(data) {
                    if (data[0].Status == "success") {
                        alert("Mensaje de correo enviado");
                    }
                },
                error: function(result) {
                    console.log(result); //--para errores                      
                }

            });
        }

        function fnEvaluadores(cod) {
            $.ajax({
                type: "POST",
                url: "../DataJson/GestionInvestigacion/operaciones.aspx",
                data: { "action": "ListaEvaluadoresRF", "param1": cod },
                dataType: "json",
                cache: false,
                async: false,
                success: function(data) {
                    var tb = '';
                    var i = 0;
                    var filas = data.length;
                    if (filas > 0) {
                        for (i = 0; i < filas; i++) {
                            tb += '<tr>';
                            tb += '<td width="5%" style="text-align:center">' + (i + 1) + "" + '</td>';
                            tb += '<td width="65%">' + data[i].nombre + '</td>';
                            tb += '<td width="20%">' + data[i].dina + '</td>';
                            //tb += '<td style="text-align:center">';
                            //tb += '<button type="button" id="btnE" name="btnE" class="btn btn-sm btn-danger" onclick="fnQuitar(\'' + data[i].cod + '\')" title="Ver Evaluadores" ><i class="ion-android-delete"></i></button>';
                            //tb += '</td>';
                            tb += '</tr>';
                        }
                    }
                    fnDestroyDataTableDetalle('tEvaluadores');
                    $('#tbEvaluadores').html(tb);
                    //fnResetDataTableTramite('tEvaluadores', 0, 'asc');
                    fnResetDataTableBasic('tEvaluadores', 0, 'asc');
                    
                    $("#cboEvaluador").val("");
                    $("#cboLinea").val("");
                    $("#mdEvaluadores").modal("show");
                },
                error: function(result) {
                    fnMensaje("warning", result)
                }
            });
        }

        function AsignarEvaluador() {
            //alert($("#hdcodPos").val() + '-' + $("#cboEvaluador").val() + '-' + $("#hdidi").val());
            if ($("#cboEvaluador").val() != "") {
                $.ajax({
                    type: "POST",
                    url: "../DataJson/GestionInvestigacion/operaciones.aspx",
                    data: { "action": "AsignarEvaluadorRF", "param1": $("#hdcodPos").val(), "param2": $("#cboEvaluador").val(), "param3": $("#hdidi").val() },
                    dataType: "json",
                    cache: false,
                    async: false,
                    success: function(data) {
                        if (data[0].Status == 1) {
                            fnMensaje("success", data[0].Message);
                            fnEvaluadores($("#hdcodPos").val());
                            RecargarTablaAjax();
                            EnviarEmailEvaluadorExterno($("#hdCON1").val(), $("#hdcodPos").val());
                        } else {
                            fnMensaje("error", data[0].Message)
                        }
                    },
                    error: function(result) {
                        fnMensaje("warning", result)
                    }
                });
            } else {
                fnMensaje("warning", 'Seleccione un Evaluador Externo.');
                return false;
            }
        }

        function EnviarEmailEvaluadorExterno(con, pos) {
            $.ajax({
                type: "GET",
                contentType: "application/json; charset=utf-8",
                url: "../DataJson/GestionInvestigacion/operaciones.aspx",
                data: { "action": "envioEmailEvaluadorExterno", "param1": con, "param2": pos },
                dataType: "json",
                success: function(data) {
                    return true;
                },
                error: function(result) {
                    //console.log(result); //--para errores                      
                }

            });
            
        }

        function RecargarTablaAjax() {
            var txt = $("#cboConcurso").val();
            $.post("FrmAjaxTablaPostResultadoFinal.aspx",
                    {
                        param1: txt
                    },
                    function(data, status) {
                        //alert("Data: " + data + "\nStatus: " + status);
                        $('#divTablePostulaciones').html(data);
                        $('#divTablePostulaciones').show();
                    });
            $("#hdCON").val(txt);
        }

        function cLineas() {
            var arr = fnLineas('%');
            var n = arr.length;
            var str = "";
            str += '<option value="">-- Seleccione -- </option>';
            for (i = 0; i < n; i++) {
                str += '<option value="' + arr[i].cod + '">' + arr[i].nombre + '</option>';
            }
            str += '<option value="%" selected="selected">TODOS</option>';
            $('#cboLinea').html(str);
        }

        function fnLineas(cpf) {
            var arr;
            $('body').append('<form id="frmOpe"><input type="hidden" id="action" name="action" value="ConsultarLineas" /></form>');
            $('#frmOpe').append('<input type="hidden" id="cpf" name="cpf" value="' + cpf + '" />');
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

        function listarEvaluadoresExternos(tipo, parametro) {
            $.ajax({
                type: "GET",
                contentType: "application/json; charset=utf-8",
                url: "../DataJson/GestionInvestigacion/operaciones.aspx",
                data: { "action": "lEvaluadoresExt", "tipo": tipo, "parametro": parametro },
                dataType: "json",
                success: function(data) {
                    var tb = ''
                    tb += "<option value=''>--Seleccione--</option>"
                    if (data.length) {
                        for (i = 0; i < data.length; i++) {
                            if (data[i].est_eve == 1) {
                                tb += "<option value='" + data[i].cod_eve + "'>" + data[i].nom_eve + "</option>"
                            }
                        }
                    }
                    $("#cboEvaluador").html(tb)
                },
                error: function(result) {
                    sOut = '';
                    //console.log(result);
                }
            });
        }

        function AsginarNotaFinal(objeto) {
            var sw_nochk = 0;

            var datos = $(objeto).val().split(",");
            //alert(datos[0] + "-" + datos[1] + "-" + datos[2] + "-" + datos[3] + "-" + datos[4]);
            var notatext = $('#txtNotaGlobal\\[' + datos[0] + '\\]').val();
            if (objeto.checked) {
                notatext = parseInt(notatext) + parseInt(datos[1]);
            } else {
                sw_nochk = 1;
            }
            var cant_chk = 0;
            var datos_array = 0;
            var nota_new = 0;
            for (i = 1; i <= parseInt(datos[2]); i++) {
                //alert("-- " + $('#chkOCDE' + datos[4] + '\\[' + i + '\\]').val());
                if ($('#chkOCDE' + datos[4] + '\\[' + i + '\\]').is(':checked')) {
                    cant_chk++;
                    datos_array = $('#chkOCDE' + datos[4] + '\\[' + i + '\\]').val().split(",");
                    nota_new = parseInt(nota_new) + parseInt(datos_array[1]); 
                } 
            }
            if (cant_chk == 0) {
                notatext = 0;
            } else {
                notatext = nota_new / cant_chk;
            }

            $.ajax({
                type: "POST",
                url: "../DataJson/GestionInvestigacion/operaciones.aspx",
                data: { "action": "gActualizarEvaluacionFinal", "param1": $("#hdCON").val(), "param2": datos[0], "param3": notatext },
                dataType: "json",
                cache: false,
                async: false,
                success: function(data) {
                    //fnMensaje(data[0].Status, data[0].Message);
                    var sin_mensaje = "";
                },
                error: function(result) {
                    fnMensaje("warning", result);
                }
            });
           
            $('#txtNotaGlobal\\[' + datos[0] + '\\]').val(notatext);
        }
        
        function AsginarNotaFinal_BK(objeto) {
            var datos = $(objeto).val().split(",");
            var notatext = $('#txtNotaGlobal\\[' + datos[0] + '\\]').val();
            if (objeto.checked) {
                notatext = parseInt(notatext) + parseInt(datos[1]);
            } else {
            notatext = parseInt(notatext) - parseInt(datos[1]);
                
                
            }
            var cant_chk = 0;
            for (i = 1; i <= parseInt(datos[2]); i++) {
                if ($('#chkOCDE\\[' + i + '\\]').is(':checked')) {
                        cant_chk++;
                }
            }
            if (cant_chk == 0) {
                notatext = 0;
            } else {
                notatext = notatext / cant_chk;
            }

            $.ajax({
                type: "POST",
                url: "../DataJson/GestionInvestigacion/operaciones.aspx",
                data: { "action": "gActualizarEvaluacionFinal", "param1": $("#hdCON").val(), "param2": datos[0], "param3": notatext },
                dataType: "json",
                cache: false,
                async: false,
                success: function(data) {
                    //fnMensaje(data[0].Status, data[0].Message);
                    var sinmensaje=""
                },
                error: function(result) {
                    fnMensaje("warning", result);
                }
            });
            $('#txtNotaGlobal\\[' + datos[0] + '\\]').val(notatext); 
        }
    
        function fnListarConcursos() {
            $.ajax({
                type: "POST",
                url: "../DataJson/GestionInvestigacion/operaciones.aspx",
                data: { "action": "lConcursosEvaluacionFinal", "param1": $("#hdId").val(), "param2": $("#hdCtf").val(), "param3": "LCP", "param4": "T" },
                dataType: "json",
                cache: false,
                async: false,
                success: function(data) {
                    aDataCon = data;
                    var tb = ''
                    tb += "<option value='0'>--Seleccione--</option>"
                    if (data.length > 0) {
                        for (i = 0; i < data.length; i++) {
                            tb += "<option value='" + data[i].cod_con + "'>" + data[i].tit_con + "</option>"
                        }
                    }
                    $("#cboConcurso").html(tb)
                },
                error: function(result) {
                    fnMensaje("warning", result)
                }
            });
        }

        function fnListarPostulacionesNO(con) {
            var nombrePostulacion = "";
            $.ajax({
                type: "POST",
                url: "../DataJson/GestionInvestigacion/operaciones.aspx",
                data: { "action": "lPostulacionesEvaluacionFinal", "param1": con },
                dataType: "json",
                cache: false,
                async: false,
                success: function(data) {
                    //console.log(data);
                    //console.log("-----------");
                    var tb = '';
                    var i = 0;
                    var mostrar = '';
                    aDataPost = data;
                    var contador = 0;
                    var filas = aDataPost.length;
                    if (filas > 0) {
                        for (i = 0; i < aDataPost.length; i++) {
                            if (nombrePostulacion != aDataPost[i].tit_pos) {
                                contador = contador + 1;
                                tb += '<tr role="row" id="' + data[i].cod_pos + ',' + contador + '">';
                                tb += '<td style="text-align:center">' + (contador)+ '</td>';
                                tb += '<td style="text-align:center">' + aDataPost[i].tit_pos + '</td>';
                                tb += '<td style="text-align:center">' + "nota" + '</td>';
                                tb += '<td style="text-align:center">' + "agregar" + '</td>';
                                tb += '<td style="text-align:center">';
                                tb += '<button type="button" id="btnD" name="btnD" class="btn btn-sm btn-red" onclick="GanadorConcurso(' + con + "," + contador + ')" title="Eliminar Grupo" ' + mostrar + '><i class="ion-close"></i></button>';
                                tb += '</td>';
                                tb += '</tr>';
                            }
                            nombrePostulacion = aDataPost[i].tit_pos;
                        }
                    }
                    //fnDestroyDataTableDetalle('tPostulacion');
                    $('#tbPostulacion').html(tb);
                    $('#tPostulacion  thead  tr th:eq(0)').html('DETALLE');
                    //fnResetDataTableTramite('tPostulacion', 0, 'asc');
                },
                error: function(result) {
                    //alert(result);
                    fnMensaje("warning", result)
                }
            });
        }   
        
        
    </script>

    
</head>
<body>
    <input type="hidden" id="hdRowsDT" name="hdRowsDT" value="" />
    <input type="hidden" id="hdCON" name="hdCON" value="" />
    <input type="hidden" id="hdEVE" name="hdEVE" value="" />
    <input type="hidden" id="hdcodPos" name="hdcodPos" value="" />
    
    <input type="hidden" id="hdId" name="hdId" value="" runat="server"/>
    <input type="hidden" id="hdidi" name="hdidi" value="" runat="server"/>
    <input type="hidden" id="hdCtf" name="hdCtf" value="" runat="server"/>
    <input type="hidden" id="hdCON1" name="hdCON1" value="" />
    
    <div class="piluku-preloader text-center hidden">
        <!--<div class="progress">
                <div class="indeterminate"></div>
                </div>-->
        <div class="loader">
            Loading...</div>
    </div>
    <div class="wrapper">
        <div class="content">
            <div class="overlay">
            </div>
            <div class="main-content">
                <div class="row">
                    <div class="manage_buttons">
                        <div class="row">
                            <div id="PanelBusqueda">
                                <div class="col-md-12 col-sm-12 col-lg-12">
                                    <div class="page_header">
                                        <div class="pull-left">
                                            <i class="icon ti-bookmark-alt page_header_icon"></i><span class="main-text">
                                                Resultado Final de Postulaciones a Concurso</span>
                                        </div>
                                        <div class="buttons-list">
                                            <div class="pull-right-btn">
                                                <%-- <button class="btn btn-primary" id="btnConsultar" value="Consultar" onclick="fnListarPostulaciones()">
                                                    Consultar</button>--%>
                                            </div>
                                        </div>
                                        <div class="row">
                                        </div>
                                        <div class="form-group">
                                            <div class="col-md-12">
                                                <label class="col-md-1 control-label ">
                                                    Concurso</label>
                                                <div class="col-md-10">
                                                    <select id="cboConcurso" name="cboConcurso" class="form-control">
                                                        <option value="0">--Seleccione--</option>
                                                    </select>
                                                </div>
                                            </div>

                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                
                <div class="row" id="divPostulacion">
                    <div class="panel-piluku">
                        <div class="panel-heading">
                            <h3 class="panel-title">
                                Lista de Postulaciones
                            </h3>
                        </div>
                        <div class="panel-body">
                            <div class="row">
                            
                                <div class="table-responsive">
                                    <div id="divTablePostulaciones" class="dataTables_wrapper" role="grid" style="display:none">
                                        hola
                                        <!--<script type="text/javascript" src='js/TablaResultadoFinal.js'></script>-->
                                        <!--<table id="tPostulacion" name="tPostulacion" class="display dataTable cell-border" width="100%">
                                                    <thead>
                                                        <tr>                             
                                                             <th style="width:10%;">N°</th>                             
                                                             <th style="width:10%;">Titulo</th>
                                                             <th style="width:50%;">Nota</th>
                                                             <th style="width:10%;">Agregar</th>                             
                                                             <th style="width:10%;">Ganador</th>                             
                                                         </tr>
                                                    </thead>   
<%--                                            <thead>
                                                <tr role="row">
                                                    <td width="5%" style="font-weight: bold; width: 54px; text-align: center" class="sorting_asc"
                                                        tabindex="0" rowspan="1" colspan="1" aria-sort="ascending" aria-label="N°: activate to sort column descending">
                                                        
                                                    </td>
                                                    <td width="5%" style="font-weight: bold; width: 54px; text-align: center" class="sorting_asc"
                                                        tabindex="0" rowspan="1" colspan="1" aria-sort="ascending" aria-label="N°: activate to sort column descending">
                                                        N°
                                                    </td>
                                                    <td width="65%" style="font-weight: bold; width: 89px; text-align: center" class="sorting"
                                                        tabindex="0" rowspan="1" colspan="1" aria-label="Nombre: activate to sort column ascending">
                                                        Titulo
                                                    </td>
                                                    <td width="15%" style="font-weight: bold; width: 169px; text-align: center" class="sorting"
                                                        tabindex="0" rowspan="1" colspan="1" aria-label="Tipo de Estudio: activate to sort column ascending">
                                                        Nota
                                                    </td>
                                                    <td width="5%" style="font-weight: bold; width: 169px; text-align: center" class="sorting"
                                                        tabindex="0" rowspan="1" colspan="1" aria-label="Tipo de Estudio: activate to sort column ascending">
                                                        Agregar
                                                    </td>
                                                    <td width="15%" style="font-weight: bold; width: 111px; text-align: center" class="sorting"
                                                        tabindex="0" rowspan="1" colspan="1" aria-label="Opciones: activate to sort column ascending">
                                                        Ganador
                                                    </td>
                                                </tr>
                                            </thead>--%>
                                            <tbody id ="tbPostulacion" runat ="server">
                                            </tbody>                             
                                           <tfoot>
                                            <tr>
                                            <th colspan="5"></th>
                                            </tr>
                                            </tfoot>
                                        </table>-->
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                
                <div class="row">
                    <div class="modal fade" id="mdEvaluadores" role="dialog" aria-labelledby="myModalLabel"
                        style="z-index: 0;" aria-hidden="true" data-backdrop="static" data-keyboard="false">
                        <div class="modal-dialog modal-lg" id="Div5">
                            <div class="modal-content">
                                <div class="modal-header" style="background-color: #E33439; color: White; font-weight: bold;">
                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close" style="float: right;">
                                        <span aria-hidden="true" class="ti-close" style="color: White;"></span>
                                    </button>
                                    <h4 class="modal-title" id="H2">
                                        Evaluadores Externos</h4>
                                </div>
                                <div class="modal-body">
                                    <form id="frmEvaluadores" name="frmEvaluadores" enctype="multipart/form-data" class="form-horizontal"
                                    method="post" onsubmit="return false;" action="#">
                                    <div class="row">
                                        <div class="form-group">
                                            <label class="col-sm-3 control-label">
                                                Linea de Investigación USAT:</label>
                                            <div class="col-md-9">
                                                <select id="cboLinea" name="cboLinea" class="form-control">
                                                    <option value="" selected="">-- Seleccione -- </option>
                                                </select>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="form-group">
                                            <label class="col-sm-3 control-label" for="txtobjetivo">
                                                Evaluador Externo:</label>
                                            <div class="col-sm-7">
                                                <select id="cboEvaluador" name="cboEvaluador" class="form-control">
                                                    <option value="">--Seleccione--</option>
                                                </select>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <center>
                                            <button type="button" id="btnAgregar" name="btnAgregar" class="btn btn-success" onclick="AsignarEvaluador()">
                                                Agregar Evaluador
                                            </button>
                                        </center>
                                    </div>
                                    <div class="row">
                                        <div class="form-group">
                                            <div class="col-sm-12">
                                                <div class="table-responsive" style="font-size: 12px; font-weight: 300; line-height: 18px;">
                                                    <div id="Div6" class="dataTables_wrapper" role="grid">
                                                        <table id="tEvaluadores" name="tEvaluadores" class="display dataTable" width="100%">
                                                            <thead>
                                                                <tr role="row">
                                                                    <td width="5%" style="font-weight: bold; text-align: center" class="sorting" tabindex="0"
                                                                        rowspan="1" colspan="1" aria-label="Nombre: activate to sort column ascending">
                                                                        N°
                                                                    </td>
                                                                    <td width="65%" style="font-weight: bold; text-align: center" class="sorting" tabindex="0"
                                                                        rowspan="1" colspan="1" aria-label="Nombre: activate to sort column ascending">
                                                                        Nombre
                                                                    </td>
                                                                    <td width="20%" style="font-weight: bold; text-align: center" class="sorting" tabindex="0"
                                                                        rowspan="1" colspan="1" aria-label="Tipo: activate to sort column ascending">
                                                                        Url Dina
                                                                    </td>
                                                                </tr>
                                                            </thead>
                                                            <tfoot>
                                                                <tr>
                                                                    <th colspan="3" rowspan="1">
                                                                    </th>
                                                                </tr>
                                                            </tfoot>
                                                            <tbody id="tbEvaluadores">
                                                            </tbody>
                                                        </table>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    </form>
                                </div>
                                <div class="modal-footer">
                                    <center>
                                        <button type="button" class="btn btn-danger" id="Button7" data-dismiss="modal">
                                            Volver</button>
                                    </center>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                
     
                     <div class="modal fade" id="mdMensaje" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" data-backdrop="static" data-keyboard="false" style="z-index: 0;"> 
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header" style="background-color:#E33439;" >
	                <button type="button" class="close" data-dismiss="modal" aria-label="Close" style="color:White;"><span aria-hidden="true" class="ti-close" style="color:White;"></span></button>
	                <h4 class="modal-title"  style="color:White">
	                    <div id="divTitle">Confirmación de Envio a Evaluadores Externos</div>
	                </h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-12" id="divMensaje">
                            
                        </div>
                    </div>
        	            
                </div>		
                <div class="modal-footer">
                  <center>
                      <div class="btn-group">			      
                            <button type="button" class="btn btn-primary" id="btnAceptar" ><i class="ion-android-done"></i>&nbsp;Aceptar</button>	
                            <button type="button" class="btn btn-danger" data-dismiss="modal"><i class="ion-android-cancel"></i>&nbsp;Cancelar</button>		
                       </div>
                  </center>
                </div>
            </div>
        </div>
        </div>


                
            </div>
        </div>
    </div>
    <div class="hiddendiv common">
    </div>
    

    
</body>
</html>
