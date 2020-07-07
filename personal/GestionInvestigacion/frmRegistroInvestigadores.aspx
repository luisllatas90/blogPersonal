<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmRegistroInvestigadores.aspx.vb"
    Inherits="GestionInvestigacion_frmRegistroInvestigadores" %>

<html id="Html1" lang="en" runat="server">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1">
    <title>Lista de Proyectos</title>
    <%--Compatibilidad con IE--%>
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta http-equiv='X-UA-Compatible' content='IE=7' />
    <meta http-equiv='X-UA-Compatible' content='IE=8' />
    <meta http-equiv='X-UA-Compatible' content='IE=10' />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
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
    <link rel="stylesheet" type="text/css" href="../assets/css/bootstrap.min.css" />
    <link rel="stylesheet" href="../assets/css/material.css?x=1" />
    <link rel="stylesheet" type="text/css" href="../assets/css/style.css?y=4" />
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
        $(document).ready(function() {
            var dt = fnCreateDataTableBasic('tbGrupoInvestigacion', 1, 'asc');
            //fnResetDataTableTramite('tbGrupoInvestigacion', 0, 'asc');
            $("#btnRegistrarInvestigador").click(fnAddInvestigador);

            rpta = fnvalidaSession();
            if (rpta == false) {
                window.location.href = rpta;
            }
            fnLineas();
            cargaTxtBoton();
            llenarChk();
            fnListarObservacionesINV($("#hdCod").val(), 'INV');
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

            $('#txtURLDina').keyup(function() {
                var l = parseInt($(this).val().length);
                if (l == 0) {
                    document.getElementById('chkDINA').checked = false;
                    $("#chkDINA").val("0");
                } else {
                    document.getElementById('chkDINA').checked = true;
                    $("#chkDINA").val("1");
                }
            });
            //ObtenerDatosORCID();
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

        function llenarChk() {
            if ($("#hdDina").val() == 1) {
                document.getElementById('chkDINA').checked = true;
                $("#chkDINA").val("1");
            } else {
                document.getElementById('chkDINA').checked = false;
                $("#chkDINA").val("0");
            }
            if ($("#hdRegina").val() == 1) {
                document.getElementById('chkREGINA').checked = true;
                $("#chkREGINA").val("1");
            } else {
                document.getElementById('chkREGINA').checked = false;
                $("#chkREGINA").val("0");
            }
        }

        function fnListarObservacionesINV(cod, inv) {
            $.ajax({
                type: "GET",
                contentType: "application/json; charset=utf-8",
                url: "../DataJson/GestionInvestigacion/operaciones.aspx",
                data: { "action": "gVisualizarHistorialGI", "param1": cod, "param2": inv },
                dataType: "json",
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
                                if (data[i].c_obs == 2) {
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
                    $("#DivObservacionesInv").html(div);

                },
                error: function(result) {
                    console.log(result); //--para errores                      
                }

            });
            return false;
        }

        function cargaTxtBoton() {
            if ($("#hdRevision").val() != "") {
                var boton = document.getElementById("btnRegistrarInvestigador");
                boton.innerHTML = $("#hdRevision").val();
                boton.disabled = true;

                var URLDina = document.getElementById("txtURLDina");
                URLDina.readOnly = true;
                var codOrcid = document.getElementById("txtOrcid");
                codOrcid.readOnly = true;


                if ($("#hdRevision").val() == 'APROBADO') {
                    document.getElementById("btnRegistrarInvestigador").className = "btn btn-green";
                    document.getElementById('chkDINA').disabled = true;
                    document.getElementById('chkREGINA').disabled = true;
                    $('#cboLinea').attr("disabled", "disabled");
                } else {
                    if ($("#hdRevision").val() == 'RECHAZADO') {
                        document.getElementById("btnRegistrarInvestigador").className = "btn btn-orange";
                        document.getElementById('chkDINA').disabled = true;
                        document.getElementById('chkREGINA').disabled = true;
                        $('#cboLinea').attr("disabled", "disabled");
                    }
                    else {
                        if ($("#hdRevision").val() == 'OBSERVADO') {
                            document.getElementById('chkDINA').disabled = false;
                            document.getElementById('chkREGINA').disabled = false;
                            $('#cboLinea').removeAttr("disabled");
                            boton.innerHTML = "REENVIAR";
                            document.getElementById("btnRegistrarInvestigador").className = "btn btn-primary";
                            boton.disabled = false;
                            $("#hdAccion").val("A");
                            URLDina.readOnly = false;
                            codOrcid.readOnly = false;
                            $('#cboLinea').removeAttr("disabled");
                        } else {
                            document.getElementById('chkDINA').disabled = false;
                            document.getElementById('chkREGINA').disabled = false;
                            $('#cboLinea').attr("disabled", "disabled");
                            boton.innerHTML = $("#hdRevision").val();
                            boton.disabled = true;
                            document.getElementById("btnRegistrarInvestigador").className = "btn btn-gray";
                        }
                    }
                }


            }
            else {
                $("#hdAccion").val("R");
            }
        }


        function fnAddInvestigador() {
            var sw = 0;
            var sw1 = 0;
            var arrayvalida = new Array();
            var mensaje = "";

            //alert($("#chkDINA").val() + "-" + $("#chkREGINA").val());

            rpta = fnvalidaSession();
            if (rpta == true) {
                for (i = 0; i < 1; i++) {
                    document.getElementById('error[' + i + ']').style.visibility = 'hidden';
                }

                var cod_orcid = $("#txtOrcid").val();
                cod_orcid = cod_orcid.replace("https://orcid.org/", "")
                cod_orcid = cod_orcid.replace("http://orcid.org/", "")
                if ($("#txtOrcid").val() != "") {
                    if (cod_orcid.length != 19) {
                        sw1 = 1;
                        mensaje = "Ingrese Correctamente Código ORCID";
                    }
                }

                if ($("#txtOrcid").val() != "") {
                    var str = cod_orcid.split("-")
                    if (str.length != 4) {
                        sw1 = 1;
                        mensaje = "Ingrese Correctamente Código ORCID";
                    }
                }

                if ($("#txtOrcid").val() == "") {
                    sw1 = 1;
                    mensaje = "Ingrese Código ORCID";
                }

                if ($("#txtURLDina").val() == "") {
                    arrayvalida[0] = "1";
                    sw = 1;
                    mensaje = "Ingrese URL DINA";
                }
                if ($("#chkDINA").checked == false) {
                    sw1 = 1;
                    mensaje = "Verifique marcado DINA";
                }

                if ($("#cboLinea").val() == "") {
                    sw1 = 1;
                    mensaje = "Seleccione una Linea de Investigación";
                }

                if (sw == 1 || sw1 == 1) {
                    if (sw1 == 1) {
                        fnMensaje("error", mensaje);
                    } else {
                        for (i = 0; i < arrayvalida.length; i++) {
                            if (arrayvalida[i] == "1") {
                                document.getElementById('error[' + i + ']').style.visibility = 'visible';
                                fnMensaje("error", mensaje);
                            }
                        }
                    }
                    return false;
                } else {
                    //$('.piluku-preloader').removeClass('hidden');
                    $("#action").val("gRegistrarInvestigador");
                    var form = $('#frmRegistroInvestigadores').serialize();
                    //alert($("#action").val());
                    $.ajax({
                        type: "GET",
                        contentType: "application/json; charset=utf-8",
                        url: "../DataJson/GestionInvestigacion/operaciones.aspx",
                        data: form,
                        dataType: "json",
                        success: function(data) {
                            //console.log(data);
                            if (data[0].Status == "success") {
                                //alert("a");
                                fnMensaje(data[0].Status, data[0].Message);
                                document.execCommand('ClearAuthenticationCache');
                                var boton = document.getElementById("btnRegistrarInvestigador");
                                document.getElementById("btnRegistrarInvestigador").className = "btn btn-gray";
                                boton.innerHTML = "En Evaluación"
                                boton.disabled = true;

                                document.getElementById('chkDINA').disabled = true;
                                document.getElementById('chkREGINA').disabled = true;
                                $('#cboLinea').attr("disabled", "disabled");

                                var URLDina = document.getElementById("txtURLDina");
                                URLDina.readOnly = true;
                                var codOrcid = document.getElementById("txtOrcid");
                                codOrcid.readOnly = true;
                                $('#cboLinea').attr("disabled", "disabled");
                                fnListarObservacionesINV($("#hdCod").val(), 'INV');
                            }
                            else {
                                //alert("b");
                                document.getElementById('chkDINA').disabled = false;
                                document.getElementById('chkREGINA').disabled = false;
                                $('#cboLinea').attr("disabled", "disabled");

                                fnListarObservacionesINV($("#hdCod").val(), 'INV');

                                fnMensaje(data[0].Status, data[0].Message);
                            }
                        },
                        error: function(result) {
                            //alert("c");
                            console.log(result);
                        }
                    });
                    return false;
                    $("#action").val("");
                    //alert("aaaaa");
                }

            } else {
                window.location.href = rpta;
                return false;
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

        function CerrarSesionORCID() {
            $.ajax({
                //url: 'https://sandbox.orcid.org/userStatus.json?logUserOut=true',
                url: 'https://orcid.org/userStatus.json?logUserOut=true',
                dataType: 'jsonp',
                success: function(result, status, xhr) {
                    console.log("Logged In: " + result.loggedIn);
                },
                error: function(xhr, status, error) {
                    console.log(status);
                }
            });
        }


        function getHTTPObject() {
            var http = false;
            //Use IE's ActiveX items to load the file.
            if (typeof ActiveXObject != 'undefined') {
                try { http = new ActiveXObject("Msxml2.XMLHTTP"); }
                catch (e) {
                    try { http = new ActiveXObject("Microsoft.XMLHTTP"); }
                    catch (E) { http = false; }
                }
                //If ActiveX is not available, use the XMLHttpRequest of Firefox/Mozilla etc. to load the document.
            } else if (XMLHttpRequest) {
                try { http = new XMLHttpRequest(); }
                catch (e) { http = false; }
            }
            return http;
        }

        /*
        function ObtenerDatosORCID() {
        if (ObtenerValorGET("code") != "") {

                $('body').append('<form id="frmOpe" action="https://sandbox.orcid.org/oauth/token" method="POST"></form>');
        $("#frmOpe").append('<input type="hidden" id="client_id" name="client_id" value="APP-1F3MW3HOFU4NP0H3" />')
        $("#frmOpe").append('<input type="hidden" id="client_secret" name="client_secret" value="1acc4edc-c731-48c0-9c3a-870c14fa2251" />')
        $("#frmOpe").append('<input type="hidden" id="grant_type" name="grant_type" value="authorization_code" />')
        $("#frmOpe").append('<input type="hidden" id="code" name="code" value="' + ObtenerValorGET("code") + '" />')
        $("#frmOpe").append('<input type="hidden" id="redirect_uri" name="redirect_uri" value="http://serverdev/campusvirtual/personal/GestionInvestigacion/frmRegistroInvestigadores.aspx" />')
        $("#frmOpe").append('<input type="submit" id="btnEnviarORCID" name="btnEnviarORCID" />')
        $("#btnEnviarORCID").trigger('click');

                $("#btnEnviarORCID").target = 'popupVisa';
        //$("#frmOpe").remove();


//                $.ajax({
        //                type: "POST",
        //                contentType: "application/json",
        //                url: 'https://sandbox.orcid.org/oauth/token',
        //                data: { "client_id": "APP-1F3MW3HOFU4NP0H3", "client_secret": "1acc4edc-c731-48c0-9c3a-870c14fa2251", "grant_type": "authorization_code", "code": ObtenerValorGET("code"), "redirect_uri": "http://serverdev/campusvirtual/personal/GestionInvestigacion/frmRegistroInvestigadores.aspx?id=684&ctf=1" },
        //                dataType: "jsonp",
        //                success: function(result) {
        //                console.log(result.tostring);
        //                },
        //                error: function(error) {
        //                console.log(error);
        //                }
        //                });
        }
        }
        */
        /**
        * Funcion que captura las variables pasados por GET
        * Devuelve un array de clave=>valor
        */
        function ObtenerValorGET(valor) {
            var valoraDevolver = "";
            // capturamos la url
            var loc = document.location.href;
            // si existe el interrogante
            if (loc.indexOf('?') > 0) {
                // cogemos la parte de la url que hay despues del interrogante
                var getString = loc.split('?')[1];
                // obtenemos un array con cada clave=valor
                var GET = getString.split('&');
                var get = {};
                // recorremos todo el array de valores
                for (var i = 0, l = GET.length; i < l; i++) {
                    var tmp = GET[i].split('=');
                    if (tmp[0] == valor) {
                        valoraDevolver = tmp[1]
                    }
                    //get[tmp[0]] = unescape(decodeURI(tmp[1]));
                }
                return valoraDevolver;
            }
        }

        function fnLineas() {
            var arr;
            $('body').append('<form id="frmOpe"><input type="hidden" id="action" name="action" value="ConsultarLineas" /></form>');
            $('#frmOpe').append('<input type="hidden" id="cpf" name="cpf" value="%" />');
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
                    var valor = $("#cboLinea").val();
                    tb = ""
                    tb = "<option value=''>-- Seleccione --</option>"
                    if (data.length > 0) {
                        for (i = 0; i < data.length; i++) {
                            if (valor == data[i].cod) {
                                tb += "<option value='" + data[i].cod + "' selected='selected'>" + data[i].nombre + "</option>";
                            } else {
                                tb += "<option value='" + data[i].cod + "'>" + data[i].nombre + "</option>";
                            }
                        }
                    }
                    $("#cboLinea").html(tb);
                },
                error: function(result) {
                    //console.log(result)
                    arr = result;
                }
            });

            return arr;
        }

        function CambiarSelect(cod) {
            console.log(cod);
            alert(cod);
            $("#cboLinea option[value='" + cod + "']").attr('selected', 'selected');
        }
        
    </script>

    <style type="text/css">
        #btnORCID
        {
            border: 1px solid #D3D3D3;
            padding: .3em;
            background-color: #fff;
            border-radius: 8px;
            box-shadow: 1px 1px 3px #999;
            cursor: pointer;
            color: #995;
            font-weight: bold;
            font-size: .8em;
            line-height: 24px;
            vertical-align: middle;
        }
        #btnORCID:hover
        {
            border: 1px solid #338caf;
            color: #338caf;
        }
        #orcid-id-icon
        {
            display: block;
            margin: 0 .5em 0 0;
            padding: 0;
            float: left;
        }
    </style>
</head>
<body>
    <form id="frmRegistroInvestigadores" name="frmRegistroInvestigadores">
    <input type="hidden" id="action" name="action" value="" />
    <input type="hidden" id="hdUser" name="hdUser" value="" runat="server" />
    <input type="hidden" id="hdCod" name="hdCod" value="" runat="server" />
    <input type="hidden" id="hdRevision" name="hdRevision" value="" runat="server" />
    <input type="hidden" id="hdDina" name="hdDina" value="" runat="server" />
    <input type="hidden" id="hdRegina" name="hdRegina" value="" runat="server" />
    <input type="hidden" id="hdAccion" name="hdAccion" value="" />
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
                                            <i class="icon ti-bookmark-alt page_header_icon"></i><span class="main-text">Actividad
                                                de Investigación </span>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="panel-piluku">
                                <div class="panel-heading">
                                    <h3 class="panel-title">
                                        Registro de Colaborador con Actividad de Investigación
                                    </h3>
                                </div>
                                <div class="panel-body">
                                    <div class="form-group">
                                        <div class="col-md-12">
                                            <div id="DivObservacionesInv">
                                            </div>
                                            <div class="row">
                                                <label class="col-md-2 control-label ">
                                                    Nombre</label>
                                                <div class="col-md-8">
                                                    <input name="txtNombre" type="text" id="txtNombre" value="" class="form-control"
                                                        readonly="true" runat="server" />
                                                </div>
                                                <div class="col-md-2">
                                                    <div class="pull-right-btn">
                                                        <button class="btn btn-primary" id="btnRegistrarInvestigador" value="Registrar">
                                                            Agregar</button>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <label class="col-md-2 control-label ">
                                                    <div id="divTipoDoc" runat="server">
                                                    </div>
                                                </label>
                                                <div class="col-md-3">
                                                    <input name="txtDNI" type="text" id="txtDNI" value="" class="form-control" readonly="true"
                                                        runat="server" />
                                                </div>
                                                <label class="col-md-1 control-label ">
                                                    Tipo</label>
                                                <div class="col-md-4">
                                                    <input name="txtTrabajador" type="text" id="txtTrabajador" value="" class="form-control"
                                                        readonly="true" runat="server" />
                                                </div>
                                            </div>
                                            <div class="row">
                                                <label class="col-md-2 control-label ">
                                                    <div id="divAreaFacultad" runat="server">
                                                    </div>
                                                </label>
                                                <div class="col-md-8">
                                                    <input name="txtEspecialidad" type="text" id="txtEspecialidad" value="" class="form-control"
                                                        readonly="true" runat="server" />
                                                </div>
                                            </div>
                                            <div class="row">
                                                <label class="col-md-2 control-label ">
                                                    Departamento</label>
                                                <div class="col-md-8">
                                                    <input name="txtDepartamento" type="text" id="txtDepartamento" value="" class="form-control"
                                                        readonly="true" runat="server" />
                                                </div>
                                            </div>
                                            <div class="row">
                                                <label class="col-md-2 control-label ">
                                                    Línea</label>
                                                <div class="col-md-8">
                                                    <%--<input name="txtLinea" type="text" id="txtLinea" value="" class="form-control" readonly="true"
                                                        runat="server" />--%>
                                                    <select id="cboLinea" class="form-control" runat="server">
                                                        <option value="">-- Seleccione --</option>
                                                    </select>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <label class="col-md-2 control-label ">
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
                                                <label class="col-md-2 control-label ">
                                                    URL DINA</label>
                                                <div class="col-md-8">
                                                    <input name="txtURLDina" type="text" id="txtURLDina" value="" class="form-control"
                                                        runat="server" />
                                                </div>
                                                <div class="col-md-1">
                                                    <div class="diverror" id="error[0]" style="visibility: hidden">
                                                        <p>
                                                            (*)</p>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <label class="col-md-2 control-label ">
                                                    ORCID</label>
                                                <div class="col-md-4">
                                                    <input name="txtOrcid" type="text" id="txtOrcid" value="" class="form-control" runat="server" />
                                                </div>
                                                <div class="col-md-3">
                                                    <div class="pull-right-btn">
                                                        <button runat="server" id="btnORCID" onclick="return false;">
                                                            <img id="orcid-id-icon" src="https://orcid.org/sites/default/files/images/orcid_24x24.png"
                                                                alt="ORCID iD icon" />
                                                            Registro o Conectar ORCID</button>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="panel-piluku">
                                                <div class="panel-heading">
                                                    <h3 class="panel-title">
                                                        Grupos de Investigaci&oacute;n
                                                    </h3>
                                                </div>
                                                <div class="panel-body">
                                                    <div class="table-responsive">
                                                        <div id="tBonos_wrapper" class="dataTables_wrapper" role="grid">
                                                            <table id="tbGrupoInvestigacion" name="tbGrupoInvestigacion" class="display dataTable"
                                                                width="100%">
                                                                <thead>
                                                                    <tr role="row">
                                                                        <td width="5%" style="font-weight: bold; width: 54px; text-align: center" class="sorting_asc"
                                                                            tabindex="0" rowspan="1" colspan="1" aria-sort="ascending" aria-label="N°: activate to sort column descending">
                                                                            N°
                                                                        </td>
                                                                        <td width="20%" style="font-weight: bold; width: 89px; text-align: center" class="sorting"
                                                                            tabindex="0" rowspan="1" colspan="1" aria-label="Nombre: activate to sort column ascending">
                                                                            Grupo
                                                                        </td>
                                                                        <td width="20%" style="font-weight: bold; width: 89px; text-align: center" class="sorting"
                                                                            tabindex="0" rowspan="1" colspan="1" aria-label="Nombre: activate to sort column ascending">
                                                                            Línea
                                                                        </td>
                                                                        <td width="20%" style="font-weight: bold; width: 89px; text-align: center" class="sorting"
                                                                            tabindex="0" rowspan="1" colspan="1" aria-label="Nombre: activate to sort column ascending">
                                                                            Responsable
                                                                        </td>
                                                                        <td width="10%" style="font-weight: bold; width: 272px; text-align: center" class="sorting"
                                                                            tabindex="0" rowspan="1" colspan="1" aria-label="Fecha Inicio: activate to sort column ascending">
                                                                            Estado
                                                                        </td>
                                                                    </tr>
                                                                </thead>
                                                                <tfoot>
                                                                    <tr>
                                                                        <th colspan="5" rowspan="1">
                                                                        </th>
                                                                    </tr>
                                                                </tfoot>
                                                                <tbody id="pGrupoInvestigacion" runat="server">
                                                                </tbody>
                                                            </table>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    </div>
    </form>
</body>
</html>
