<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmRegistrarExternos.aspx.vb"
    Inherits="GestionInvestigacion_frmRegistrarExternos" %>

<!DOCTYPE html>
<html>
<head>
    <title>Evaluadores Externos</title>
    <%--Compatibilidad con IE--%>
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta http-equiv='X-UA-Compatible' content='IE=7' />
    <meta http-equiv='X-UA-Compatible' content='IE=8' />
    <meta http-equiv='X-UA-Compatible' content='IE=10' />
    <%--Compatibilidad con IE--%>
    <meta http-equiv="Pragma" content="no-cache" />
    <!-- Piluku -->
    <link rel="stylesheet" type="text/css" href="../assets/css/bootstrap.min.css" />
    <link rel="stylesheet" href="../assets/css/material.css?x=1" />
    <link rel="stylesheet" type="text/css" href="../assets/css/style.css?y=4" />
    <!-- Piluku -->

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
        var aDataPais = [];
        var aDataEE = [];

        $(document).ready(function() {
        //fnResetDataTableTramite('tEvaluadorExterno', 0, 'desc');
            
            rpta = fnvalidaSession();
            if (rpta == false) {
                window.location.href = rpta;
            }
            var dt = fnCreateDataTableBasic('tEvaluadorExterno', 1, 'asc');
            $("#btnAgregar").click(fnAgregar);
            $('#divInvestigador').hide();
            $("#btnDelReg").click(CambiarEstadoGrupoInvestigador1);
            listaPais();
            listarEvaluadoresExternos('L', '');
            $('#chkDINA').change(function() {
                if ($(this).is(':checked')) {
                    $("#chkDINA").val("1");
                } else {
                    $("#chkDINA").val("0");
                }
            });
            $('#chkREGINA').change(function() {
                if ($(this).is(':checked')) {
                    $("#chkREGINA").val("1");
                } else {
                    $("#chkREGINA").val("0");
                }
            });
            $('#txtURLDINA').keyup(function() {
                var l = parseInt($(this).val().length);
                if (l == 0) {
                    document.getElementById('chkDINA').checked = false;
                    $("#chkDINA").val("0");
                } else {
                    document.getElementById('chkDINA').checked = true;
                    $("#chkDINA").val("1");
                }
            });

            $("#cboPais").change(function() {
                if (this.value == "156") {
                    $('#divInvestigador').show();
                }
                else {
                    $('#divInvestigador').hide();
                }
            });

            //---- PARA LINEA DE INVESTIGACIÓN OCDE
            cLineas();
            fnListarOCDE(0, 'AR')
            $("#cboArea").change(function() {
                if ($("#cboArea").val() != 0) {
                    fnListarOCDE($("#cboArea").val(), 'SA');
                } else {
                    $("#cboSubArea").html("<option value='0'>-- Seleccione --</option>");
                }
                $("#cboDisciplina").html("<option value='0'>-- Seleccione --</option>");
            })
            $("#cboSubArea").change(function() {
                if ($("#cboSubArea").val() != 0) {
                    fnListarOCDE($("#cboSubArea").val(), 'DI');
                } else {
                    $("#cboDisciplina").html("<option value='0'>-- Seleccione --</option>");
                }
            })

            $('#chkOCDE').click(function() {
                $("#cboArea").val("0");
                $("#cboSubArea").html("<option value='0'>-- Seleccione --</option>");
                $("#cboDisciplina").html("<option value='0'>-- Seleccione --</option>");
                if ($(this).is(':checked')) {
                    $("#ocde").attr("style", "display:block");
                } else {
                    $("#ocde").removeAttr("style");
                    $("#ocde").attr("style", "display:none");
                }
            });
            //----
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
                    //console.log(result)
                fnMensaje("warning", result);
                }
            });
            return rpta;
        }

        //---- PARA LINEA DE INVESTIGACIÓN
        function cLineas() {
            var arr = fnLineas('%');
            var n = arr.length;
            var str = "";
            str += '<option value="" selected>-- Seleccione -- </option>';
            for (i = 0; i < n; i++) {
                str += '<option value="' + arr[i].cod + '">' + arr[i].nombre + '</option>';
            }

            $('#cboLinea').html(str);
        }

        function fnLineas(cpf) {
            var arr;
            $('body').append('<form id="frmOpe"><input type="hidden" id="action" name="action" value="ConsultarLineas" /></form>');
            $('#frmOpe').append('<input type="hidden" id="cpf" name="cpf" value="' + cpf + '" />');
            var form = $("#frmOpe").serializeArray();
            $("#frmOpe").remove();

            $.ajax({
                type: "POST",
                url: "../DataJson/GestionInvestigacion/operaciones.aspx",
                data: form,
                dataType: "json",
                cache: false,
                async: false,
                success: function(data) {

                    arr = data;
                },
                error: function(result) {

                    arr = result;
                }
            });

            return arr;
        }

        function fnListarOCDE(cod, tipo) {
            //            rpta = fnvalidaSession()
            //            if (rpta == true) {
            //            fnLoading(true)


            $('body').append('<form id="frmOpe"><input type="hidden" id="action" name="action" value="lAreaConocimientosOCDE" /></form>');
            $('form#frmOpe').append('<input type="hidden" id="param1" name="param1" value="' + cod + '" />');
            $('form#frmOpe').append('<input type="hidden" id="param2" name="param2" value="' + tipo + '" />');
            var form = $("#frmOpe").serializeArray();
            $("#frmOpe").remove();

            $.ajax({
                type: "POST",
                url: "../DataJson/GestionInvestigacion/Operaciones.aspx",
                data: form,
                dataType: "json",
                cache: false,
                async: false,
                success: function(data) {

                    var tb = '';
                    var filas = data.length;
                    if (filas > 0) {
                        for (i = 0; i < filas; i++) {
                            tb += '<option value="' + data[i].codigo + '">' + data[i].descripcion + '</option>';
                        }
                    }
                    if (tipo == "AR") {
                        $("#cboArea").html(tb);
                    }
                    if (tipo == "SA") {
                        $("#cboSubArea").html(tb);
                    }
                    if (tipo == "DI") {
                        $("#cboDisciplina").html(tb);
                    }
                },
                error: function(result) {
                    fnMensaje("warning", result)
                }
            });
            //            fnLoading(false);
            //            } else {
            //                window.location.href = rpta
            //            }
        }



        //----

        function listarEvaluadoresExternos(tipo, parametro) {
            $.ajax({
                type: "POST",
                //                contentType: "application/json; charset=utf-8",
                url: "../DataJson/GestionInvestigacion/operaciones.aspx",
                data: { "action": "lEvaluadoresExt", "tipo": tipo, "parametro": parametro },
                dataType: "json",
                cache: false,
                async: false,
                success: function(data) {
//                    console.log(data);
                    aDataEE = data;
                    var tb = '';
                    var i = 0;
                    var mostrar = '';
                    aDataEE = data;
                    var contador = 0;
                    var filas = aDataEE.length;
                    if (filas > 0) {
                        for (i = 0; i < aDataEE.length; i++) {
                            contador = contador + 1;
                            tb += '<tr>';
                            tb += '<td style="text-align:center">' + (contador) + "" + '</td>';
                            tb += '<td>' + aDataEE[i].nom_eve + '</td>';
                            tb += '<td style="text-align:center">' + aDataEE[i].nom_pai + '</td>';
                            tb += '<td>';
                            tb += '<button type="button" id="btnE" name="btnE" class="btn btn-sm btn-info" onclick="fnEditar(\'' + aDataEE[i].cod_eve + '\')" title="Editar" ><i class="ion-eye"></i></button>';
                            tb += '<button type="button" id="btnD" name="btnD" class="btn btn-sm btn-red" onclick="CambiarEstadoGrupoInvestigador(\'' + aDataEE[i].cod_eve + '\',\'' + 'X' + '\')" title="Eliminar Grupo" ><i class="ion-close"></i></button>';
                            tb += '</td>';
                            tb += '</tr>';
                        }
                    }
                    fnDestroyDataTableDetalle('tEvaluadorExterno');
                    //fnDestroyDataTableDetalle('tBonos');
                    $('#tbEvaluadorExterno').html(tb);
                    //fnResetDataTableTramite('tEvaluadorExterno', 0, 'asc');
                    fnResetDataTableBasic('tEvaluadorExterno', 0, 'asc');
                },
                error: function(result) {
                    sOut = '';

                }
            });
        }

        function CambiarEstadoGrupoInvestigador(cod, tipo) {
            $('#hdAccion').val("ELI");
            $('#hdCod').val(cod);
            var x = fnBuscar(cod);
            fnMensajeConfirmarEliminar('top', '¿Desea Eliminar al Evaluador?', 'CambiarEstadoGrupoInvestigador1');
        }

        function CambiarEstadoGrupoInvestigador1() {
            rpta = fnvalidaSession();
            if (rpta == true) {
                $.ajax({
                    type: "GET",
                    //contentType: "application/json; charset=utf-8",
                    url: "../DataJson/GestionInvestigacion/operaciones.aspx",
                    data: { "action": "gEstadoInvestigadorExterno", "param1": $('#hdCod').val(), "param2": "", "param3": $('#hdAccion').val() },
                    dataType: "json",
                    cache: false,
                    async: false,
                    success: function(data) {
                        if (data[0].Status == "success") {
                            //document.execCommand('ClearAuthenticationCache');
                            $('div#mdDelRegistro').modal('hide');
                            fnDestroyDataTableDetalle('tEvaluadorExterno');
                            $('#tbEvaluadorExterno').html('');

                            listarEvaluadoresExternos('L', '');
                            fnMensaje(data[0].Status, data[0].Message);
                        } else {
                            fnMensaje(data[0].Status, data[0].Message);
                        }
                    },
                    error: function(result) {
                        fnMensaje("warning", result);
                    }
                });
            } else {
                window.location.href = rpta;
            }
        }

        function fnEditar(eve) {
            $("#divcv").html("");
            listarEvaluadoresExternos('L', '');
            
            $('#hdAccion').val("A");
            var y = fnBuscar(eve);
            $('#hdCod').val(aDataEE[y].cod_eve);
            $('#cboPais').val(aDataEE[y].pai_eve);
            $('#txtEvaluador').val(aDataEE[y].nom_eve);
            $('#txtEmail').val(aDataEE[y].ema_eve);
            $('#txtNroTlf').val(aDataEE[y].tlf_eve);

            if (aDataEE[y].din_eve == 0) {
                $("#chkDINA").prop("checked", false);
                $("#chkDINA").val(0);
            } else {
                $("#chkDINA").prop("checked", true);
                $("#chkDINA").val(1);
            }
            if (aDataEE[y].reg_eve == 0) {
                $("#chkREGINA").prop("checked", false);
                $("#chkREGINA").val(0);
            } else {
                $("#chkREGINA").prop("checked", true);
                $("#chkREGINA").val(1);
            }
            $('#txtURLDINA').val(aDataEE[y].url_eve);

//            alert(aDataEE[y].cv_eve);
            if (aDataEE[y].cv_eve != "") {
                //$("#divcv").html("<a href='" + aDataEE[y].cv_eve + "' target='_blank' style='font-weight:bold'>Descargar CV</a>")
                $("#divcv").html('<a onclick="fnDownload(\'' + aDataEE[y].cv_eve + '\')" >Descargar CV</a>')
                $("#hdCV").val(1);
            } else {
                $("#divcv").html("");
                $("#hdCV").val(0);
            }

            if (aDataEE[y].pai_eve == 156) {
                $('#divInvestigador').show();
            } else {
                $('#divInvestigador').hide();
            }
            //---- linea de investigacion
            document.getElementById("hdValidaUrl").value = "1";
            $('#cboLinea').val(aDataEE[y].linea);
            if (aDataEE[y].cod_dis != "0") {
                $("#chkOCDE").prop('checked', true);
                $("#ocde").attr("style", "display:block");
                $("#cboArea").val(aDataEE[y].cod_area)
                fnListarOCDE($("#cboArea").val(), 'SA');
                $("#cboSubArea").val(aDataEE[y].cod_sub)
                fnListarOCDE($("#cboSubArea").val(), 'DI');
                $("#cboDisciplina").val(aDataEE[y].cod_dis)
            } else {
                $("#chkOCDE").prop('checked', false);
                $("#ocde").removeAttr("style");
                $("#ocde").attr("style", "display:none");
            }
            //----
            $('div#mdEdicion').modal('show');
            return false;
        }

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

        function listaPais() {
            $.ajax({
                type: "GET",
                //contentType: "application/json; charset=utf-8",
                url: "../DataJson/GestionInvestigacion/operaciones.aspx",
                data: { "action": "lPais" },
                dataType: "json",
                cache: false,
                async: false,
                success: function(data) {
                    aDataPais = data;
                    var i = 0;
                    var t = '';
                    if (aDataPais.length > 0) {
                        for (var i = 0; i < aDataPais.length; i++) {
                            t += '<option value="' + aDataPais[i].c_pai + '" selected="' + aDataPais[i].d_sel + '" >' + aDataPais[i].d_pai + '</option>';
                        }
                    }
                    $('#cboPais').html(t);
                },
                error: function(result) {
                    sOut = '';
                }
            });
        }

        function fnAgregar() {
            rpta = fnvalidaSession();
            if (rpta == true) {
                
                $("#chkREGINA").val("0");
                $("#chkDINA").val("0");
                $("#chkDINA").prop("checked", false);
                $("#chkREGINA").prop("checked", false);

                $("#divcv").html("");

                $('#hdAccion').val("R");
                $('#hdCod').val("0");
                $("#hdCV").val(0);

                limpiar();
                $('div#mdEdicion').modal('show');
                return false;
            } else {
                window.location.href = rpta;
            }
        }

        function limpiar() {
            $('#cboPais').val("0");
            $('#txtEvaluador').val("");
            $('#txtEmail').val("");
            $('#txtNroTlf').val("");
            //$('#chkDINA').val("0");
            $('#txtURLDINA').val("");
            $('#file_cv').val("");
            $("#hdCV").val(0);

            $("#cboLinea").val("");
            $("#cboArea").val("0");
            $("#cboSubArea").html("<option value='0'>-- Seleccione --</option>");
            $("#cboDisciplina").html("<option value='0'>-- Seleccione --</option>");
            $("#ocde").removeAttr("style");
            $("#ocde").attr("style", "display:none");
            $("#chkOCDE").prop('checked', false);
        }

        function fnGuardarEvaluador() {
            var sw = 0;
            var mensaje = "";
            rpta = fnvalidaSession();
            if (rpta == true) {
            
                if ($('#hdAccion').val() == "A") {
                    $("#hdCV").val(1);
                } else {
                    $("#hdCV").val(0);
                    if ($("#file_cv").val() == "") {
                        sw = 1;
                        mensaje = "Ingrese CV Evaluador";
                    }
                }

                if ($("#txtURLDINA").val() == "") {
                    sw = 1;
                    mensaje = "Ingrese URL DINA Evaluador";
                }
                if ($("#txtNroTlf").val() == "") {
                    sw = 1;
                    mensaje = "Ingrese Nro. Teléfono Evaluador";
                }
                if ($("#txtEmail").val() == "") {
                    sw = 1;
                    mensaje = "Ingrese Email Evaluador";
                } else {
                    if ($("#hdValidaUrl").val() == 0) {
                        sw = 1;
                        mensaje = "Ingrese Email Evaluador válida";
                    }
                }
                if ($("#txtEvaluador").val() == "") {
                    sw = 1;
                    mensaje = "Ingrese Nombre Evaluador";
                }
                if ($("#cboPais").val() == 0) {
                    sw = 1;
                    mensaje = "Seleccione Pais";
                } else {
                    if ($("#cboPais").val() == 156) {
                        if ($("#chkDINA").val() == 0 && $("#chkDINA").val() == 0) {
                            sw = 1;
                            mensaje = "Seleccionar al menos tipo de Investigador DINA";
                        }
                    }
                }

                if ($("#cboLinea").val() == 0) {
                    sw = 1;
                    mensaje = "Seleccione Linea de Investigación USAT";
                }

                if ($("#chkOCDE").is(':checked')) {
                    if ($("#cboArea").val() == "0") {
                        sw = 1;
                        mensaje = "Debe Seleccionar Una Área de Investigación OCDE";
                    }
                    if ($("#cboSubArea").val() == "0") {
                        sw = 1;
                        mensaje = "warning", "Debe Seleccionar una Sub Área de Investigación OCDE";
                    }
                    if ($("#cboDisciplina").val() == "0") {
                        sw = 1;
                        mensaje = "Debe Seleccionar una Disciplina de Investigación OCDE";
                    }
                }


                if (sw == 1) {
                    fnMensaje("warning", mensaje);
                    return false;
                } else {

                    $("#DivGuardar").attr("style", "display:none");
                    $("#MensajeGuardar").attr("style", "display:block");
                    $("#MensajeGuardar").html('<b>Guardando Postulación...</b>');
                
                    $("#action").val("gEvaluadorExterno");
                    var form = $('#frmEvaluadorExterno').serialize();

                    $.ajax({
                        type: "GET",
                        //contentType: "application/json; charset=utf-8",
                        url: "../DataJson/GestionInvestigacion/operaciones.aspx",
                        //data: { "action": "gEvaluadorExterno", "hdAccion": $("#hdAccion").val(), "hdCod": $("#hdCod").val(), "cboPais": $("#cboPais").val(), "txtEvaluador": $("#txtEvaluador").val(), "txtEmail": $("#txtEmail").val(), "txtNroTlf": $("#txtNroTlf").val(), "txtURLDINA": $("#txtURLDINA").val(), "chkDINA": $("#chkDINA").val(), "chkREGINA": $("#chkREGINA").val(), "hdUser": $("#hdUser").val() },
                        data: form,
                        dataType: "json",
                        cache: false,
                        async: false,
                        success: function(data) {
                            if (data[0].Status == "success") {
                                //document.execCommand('ClearAuthenticationCache');

//                                fnDestroyDataTableDetalle('tEvaluadorExterno');
//                                $('#tbEvaluadorExterno').html('');
                                listarEvaluadoresExternos('L', '%');
                                $('div#mdEdicion').modal('hide');

                                $("#DivGuardar").attr("style", "display:block");
                                $("#MensajeGuardar").attr("style", "display:none");
                                $("#MensajeGuardar").html('');
                                
                                
                                if ($("#hdCV").val() == 1 && $("#file_cv").val() != "") {
                                    fnCargarCV(data[0].Code, "CV");

                                } else {
                                    if ($("#hdCV").val() == 0) {
                                        fnCargarCV(data[0].Code, "CV");
                                        //alert("3");
                                    }
                                }
                                limpiar();
                                fnMensaje(data[0].Status, data[0].Message);
                                return false;
                            }
                            else {
                                //document.execCommand('ClearAuthenticationCache');

                                $("#DivGuardar").attr("style", "display:block");
                                $("#MensajeGuardar").attr("style", "display:none");
                                $("#MensajeGuardar").html('');

//                                fnDestroyDataTableDetalle('tEvaluadorExterno');
//                                $('#tbEvaluadorExterno').html('');
                                listarEvaluadoresExternos('L', '');
                                $('div#mdEdicion').modal('hide');
                                limpiar();

                                fnMensaje(data[0].Status, data[0].Message);
                                return false;
                            }
                        },
                        error: function(result) {
                            $("#DivGuardar").attr("style", "display:block");
                            $("#MensajeGuardar").attr("style", "display:none");
                            $("#MensajeGuardar").html('');
                        }
                    });

                    return false;


                }

            } else {
                window.location.href = rpta;
            }

        }

        function fnCargarCV(cod, tipo) {
            var flag = 1;
            try {

                var data = new FormData();
                data.append("action", "SurbirCVNew");
                data.append("codigo", cod);
                data.append("tipo", tipo);

                if (tipo == "CV") {
                    var files = $("#file_cv").get(0).files;
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
                            flag = 2;
                        },
                        error: function(result) {
                            //alert("b");
                            flag = 1;
                        }
                    });
                }
                return flag;

            }
            catch (err) {
                return flag;
            }
        }

        function fnBuscar(c) {
            var i;
            var j = -1;
            var l;
            l = aDataEE.length;
            for (i = 0; i < l; i++) {
                if (aDataEE[i].cod_eve == c) {
                    j = i;
                    return j;
                }
            }
        }

        function validateMail(idMail) {
            object = document.getElementById(idMail);
            valueForm = object.value;

            var patron = /^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,4})+$/;
            if (valueForm.search(patron) == 0) {
                object.style.color = "#a1a5ac";
                document.getElementById("hdValidaUrl").value = "1";
                return;
            }
            object.style.color = "#f00";
            document.getElementById("hdValidaUrl").value = "0";
        }

        function soloNumeros(e) {
            var key = window.Event ? e.which : e.keyCode
            return (key >= 48 && key <= 57)
        }
        
    </script>

</head>
<body>
    <form id="frmEvaluadorExterno" name="frmEvaluadorExterno">
    <input type="hidden" id="action" name="action" value="" />
    <input type="hidden" id="hdUser" name="hdUser" value="" runat="server" />
    <input type="hidden" id="hdCod" name="hdCod" value="" runat="server" />
    <input type="hidden" id="hdAccion" name="hdAccion" value="" runat="server" />
    <input type="hidden" id="hdCV" name="hdCV" value="" runat="server" />
    <input type="hidden" id="hdValidaUrl" name="hdValidaUrl" value="" />
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
                                            <i class="icon ti-bookmark-alt page_header_icon"></i><span class="main-text">Evaluadores
                                                Externos </span>
                                        </div>
                                        <div class="buttons-list">
                                            <div class="pull-right-btn">
                                                <button class="btn btn-green" id="btnAgregar" value="Agregar">
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
                                        Evaluadores Externos
                                    </h3>
                                </div>
                                <!-- JR -->
                                <div class='table-responsive'>
                                    <div class='panel-body'>
                                        <div class='table-responsive'>
                                            <!--Default Form-->
                                            <div id="tBonos_wrapper" class="dataTables_wrapper" role="grid">
                                                <table id="tEvaluadorExterno" name="tEvaluadorExterno" class="display dataTable"
                                                    width="100%">
                                                    <thead>
                                                        <tr>
                                                            <td width="5%" style="font-weight: bold; width: 54px; text-align: center" class="sorting_asc"
                                                                tabindex="0" rowspan="1" colspan="1" aria-sort="ascending" aria-label="N°: activate to sort column descending">
                                                                N°
                                                            </td>
                                                            <td width="40%" style="font-weight: bold; width: 89px; text-align: center" class="sorting"
                                                                tabindex="0" rowspan="1" colspan="1" aria-label="Nombre: activate to sort column ascending">
                                                                Evaluador
                                                            </td>
                                                            <td width="20%" style="font-weight: bold; width: 89px; text-align: center" class="sorting"
                                                                tabindex="0" rowspan="1" colspan="1" aria-label="Nombre: activate to sort column ascending">
                                                                Nacionalidad
                                                            </td>
                                                            <td width="10%" style="font-weight: bold; width: 111px; text-align: center" class="sorting"
                                                                tabindex="0" rowspan="1" colspan="1" aria-label="Opciones: activate to sort column ascending">
                                                                Opciones
                                                            </td>
                                                        </tr>
                                                    </thead>
                                                    <tfoot>
                                                        <tr>
                                                            <th colspan="4" rowspan="1">
                                                            </th>
                                                        </tr>
                                                    </tfoot>
                                                    <tbody id="tbEvaluadorExterno">
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
                                                    Evaluador Externo</h4>
                                            </div>
                                            <div class="modal-body">
                                                    <div id="DivObservaciones">
                                                    </div>
                                                        <div class="row">
                                                            <label class="col-md-3 control-label ">
                                                                Nacionalidad</label>
                                                            <div class="col-md-5">
                                                                <select name="cboPais" class="form-control" id="cboPais">
                                                                    <option value="0" selected="">-- Seleccione -- </option>
                                                                </select>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <label class="col-md-3 control-label ">
                                                                Nombre Evaluador</label>
                                                            <div class="col-md-7">
                                                                <input name="txtEvaluador" type="text" id="txtEvaluador" value="" class="form-control"
                                                                    runat="server" placeholder="Digitar nombre de Evaluador" />
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <label class="col-md-3 control-label ">
                                                                E-mail</label>
                                                            <div class="col-md-7">
                                                                <input name="txtEmail" type="text" id="txtEmail" value="" class="form-control" runat="server"
                                                                    placeholder="Digitar E-Mail de evaluador" onkeyup="javascript:validateMail('txtEmail')" />
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <label class="col-md-3 control-label ">
                                                                Nro Teléfono</label>
                                                            <div class="col-md-7">
                                                                <input name="txtNroTlf" type="text" id="txtNroTlf" value="" class="form-control"
                                                                    runat="server" placeholder="Digitar Nro de teléfono de evaluador" onkeypress="return soloNumeros(event)" />
                                                            </div>
                                                        </div>
                                                        <div class="row" id="divInvestigador">
                                                            <label class="col-md-3 control-label ">
                                                                Investigador</label>
                                                            <label class="col-md-1 control-label ">
                                                                DINA</label>
                                                            <div class="col-md-1">
                                                                <input type="checkbox" id="chkDINA" name="chkDINA" value="0" data-validation="checkbox_group"
                                                                    data-validation-qty="1-2" runat="server">
                                                            </div>
                                                            <label class="col-md-1 control-label ">
                                                                REGINA</label>
                                                            <div class="col-md-1">
                                                                <input type="checkbox" id="chkREGINA" name="chkREGINA" value="0" data-validation="checkbox_group"
                                                                    data-validation-qty="1-2" runat="server">
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <label class="col-md-3 control-label ">
                                                                URL Investigador</label>
                                                            <div class="col-md-7">
                                                                <input name="txtURLDINA" type="text" id="txtURLDINA" value="" class="form-control"
                                                                    runat="server" placeholder="Digitar URL DINA del investigador" />
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <label class="col-sm-3 control-label">
                                                                Adjuntar CV(PDF):</label>
                                                            <div class="col-sm-7">
                                                                <input type="file" id="file_cv" name="file_cv" />
                                                                <div id="divcv">
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <label class="col-sm-3 control-label">
                                                                L&iacute;nea de Investigación USAT:</label>
                                                            <div class="col-md-9">
                                                                <select id="cboLinea" name="cboLinea" class="form-control">
                                                                    <option value="" selected="">-- Seleccione -- </option>
                                                                </select>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <label class="col-sm-3 control-label">
                                                                L&iacute;nea OCDE</label>
                                                            <div class="col-md-9">
                                                                <input type="checkbox" id="chkOCDE" name="chkOCDE" />
                                                            </div>
                                                        </div>
                                                        <div id="ocde" style="display: none">
                                                            <div class="row">
                                                                <label class="col-sm-3 control-label">
                                                                    Área Temática:</label>
                                                                <div class="col-md-8">
                                                                    <select id="cboArea" name="cboArea" class="form-control">
                                                                        <option value="" selected="">-- Seleccione -- </option>
                                                                    </select>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <label class="col-sm-3 control-label">
                                                                    Sub Área:</label>
                                                                <div class="col-md-8">
                                                                    <select id="cboSubArea" name="cboSubArea" class="form-control">
                                                                        <option value="" selected="">-- Seleccione -- </option>
                                                                    </select>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <label class="col-sm-3 control-label">
                                                                    Disciplina:</label>
                                                                <div class="col-md-8">
                                                                    <select id="cboDisciplina" name="cboDisciplina" class="form-control">
                                                                        <option value="0" selected="">-- Seleccione -- </option>
                                                                    </select>
                                                                </div>
                                                            </div>
                                                        </div>
                                            </div>
                                            
                                            <div class="modal-footer">
                                                <div id="DivGuardar">
                                                    <center>
                                                        <button type="button" id="btnA" class="btn btn-success" onclick="fnGuardarEvaluador();">
                                                            Guardar</button>
                                                        <button type="button" class="btn btn-danger" id="btnCancelarReg" data-dismiss="modal">
                                                            Cancelar</button>
                                                    </center>
                                                </div>
                                                 <center>
                                                    <div class="alert alert-success" id="MensajeGuardar" style="display: none;">
                                                    </div>
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
    <div class="modal fade" id="mdDelRegistro" tabindex="-1" role="dialog" aria-labelledby="myModalLabel"
        aria-hidden="true" data-backdrop="static" data-keyboard="false" style="z-index: 5;">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header" style="background-color: #E33439;">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close" style="color: White;">
                        <span aria-hidden="true" class="ti-close" style="color: White;"></span>
                    </button>
                    <h4 class="modal-title" style="color: White">
                        Confirmar Operaci&oacute;n</h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-12" id="">
                            <label class="col-md-12 control-label">
                                Desea Confirmar la Eliminaci&oacute;n del Registro
                                <div id="divEvaluadorExterno">
                                </div>
                            </label>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <center>
                        <div class="btn-group">
                            <button type="button" class="btn btn-primary" id="btnDelReg">
                                <i class="ion-android-done"></i>&nbsp;Eliminar</button>
                            <button type="button" class="btn btn-danger" data-dismiss="modal">
                                <i class="ion-android-cancel"></i>&nbsp;Cancelar</button>
                        </div>
                    </center>
                </div>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
