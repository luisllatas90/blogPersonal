<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmInscripcionAlmuerzo.aspx.vb"
    Inherits="frmInscripcionAlmuerzo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <%--Compatibilidad con IE--%>
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta http-equiv='X-UA-Compatible' content='IE=8' />
    <meta http-equiv='X-UA-Compatible' content='IE=10' />
    <%--Compatibilidad con IE--%>
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=0" />
    <meta http-equiv="Pragma" content="no-cache" />
    <link href="../assets/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="private/font-awesome-4.7.0/css/font-awesome.min.css" rel="stylesheet"
        type="text/css" />

    <script src="../assets/js/jquery.js" type="text/javascript"></script>

    <script src="../assets/js/bootstrap.min.js" type="text/javascript"></script>

    <script src="../assets/js/bootstrap-datepicker.js" type="text/javascript"></script>

    <link href="private/jquery.loadmask.css" rel="stylesheet" type="text/css" />

    <script src="private/jquery.loadmask.js" type="text/javascript"></script>

    <script src='https://www.google.com/recaptcha/api.js'></script>

    <script type="text/javascript">

        $(document).ready(function() {

            var x = document.referrer;
            //alert(x);
            //var tmp = x.split('/');
            //alert(tmp[2]);
            //if (tmp[2] != "usat.edu.pe") {
            var v = msieversion();
            if (v > 0) {
                var tmp = x.split('/');
                //alert(tmp[2]);
                if (tmp[2] != "usat.edu.pe") {
                    //if (x.contains("usat.edu.pe") == false) {
                   // window.location.href = "http://www.usat.edu.pe";
                }
            } else {
            if (x.indexOf("usat.edu.pe") == false) {
                 //   window.location.href = "http://www.usat.edu.pe";
                }
            }


            $("#txtfechaNacimiento").datepicker({
                dateFormat: 'dd/mm/yy' //Se especifica como deseamos representarla
            });

            $('body').on('click', '#chkTerminosCondiciones', function(e) {
                activacionBotonesSubmit();
            });

            $('body').on('click', '#lnkLeerTerminosCondiciones', function(e) {
                MostrarTerminosCondiciones();
                e.preventDefault();
            });
        });

        function msieversion() {

            var ua = window.navigator.userAgent;
            var msie = ua.indexOf("MSIE");
            var version = 0;
            if (msie > 0 || !!navigator.userAgent.match(/Trident.*rv\:11\./))  // If Internet Explorer, return version number
            {
                //alert(parseInt(ua.substring(msie + 5, ua.indexOf(".", msie))));
                version = parseInt(ua.substring(msie + 5, ua.indexOf(".", msie)));
            }
            else  // If another browser, return 0
            {
                version = 0;
            }

            return version;
        }

        function MascaraEspera(sw) {
            if (sw == "1")
                $("#shenlong").mask("Espere...");
            if (sw == "0")
                $("#shenlong").unmask();
        }

//        function CargaCombo() {
//            $.ajax({
//                type: "POST",
//                contentType: "application/json; charset=utf-8",
//                url: "../../congreso/congresousat.asmx/Universidades",
//                data: "{}",
//                dataType: "json",
//                success: function(Result) {
//                    Result = Result.d;
//                    $("#ddlUniversidad").append("<option value='0'>-- Seleccione --</option>");
//                    $.each(Result, function(key, value) {
//                        $("#ddlUniversidad").append("<option value='" + value.valor + "'>" + value.descripcion + "</option>");
//                    });
//                },
//                error: function(Result) {
//                    console.log(result)
//                }
//            });
//        }

        function infoCentroCostos() {
            var cco = parseInt(ObtenerValorGET("Evento"));

            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "../../congreso/congresousat.asmx/VerCentroCosto",
                data: "{'token_cco':'" + cco + "'}",
                dataType: "json",
                success: function(Result) {
                    Result = Result.d
                    $("#Descripcion").html("<h5 style='color:green;font-weight:bold'>" + Result[0].descripcion + "<h5>");
                },
                error: function(Result) {
                    console.log(Result)
                }
            });
        }

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

        function ValidarCaptcha() {
            form = $("#form1").serialize();
            $.ajax({
                type: "POST",
                //contentType: "application/json; charset=utf-8",
                url: "https: //www.google.com/recaptcha/api/siteverify",
                data: form,
                dataType: "json",
                success: function(Result) {
                    console.log(result);
                },
                error: function(Result) {
                    console.log(result)
                }
            });
        }



        function validar() {

            $("#mensaje").removeAttr("class");
            $("#mensaje").html("");

            if ($("#ddltipoparticipante").val() == "-1") {
                $("#mensaje").attr("class", "alert alert-danger")
                $("#mensaje").html("<p>Seleccione el Tipo de Participante.</p>")
                $("#ddltipoparticipante").focus();
                return false;
            }
            if ($("#ddltipodoc").val() == "0") {
                $("#mensaje").attr("class", "alert alert-danger")
                $("#mensaje").html("<p>Seleccione Tipo de Documento.</p>")
                return false;
            }
            if ($("#txtnrodoc").val() == "") {
                $("#mensaje").attr("class", "alert alert-danger")
                $("#mensaje").html("<p>Ingrese Número de Documento.</p>")
                $("#txtnrodoc").focus();
                return false;
            }
            var valida_enteros = /^\d*$/;
            if ($("#ddltipodoc").val() == "DNI") {
                if ($("#txtnrodoc").val().length != 8 || !valida_enteros.test($("#txtnrodoc").val())) {
                    $("#mensaje").attr("class", "alert alert-danger");
                    $("#mensaje").html("<p>Número de DNI debe Tener 8 Digitos (Números Enteros).</p>")
                    $("#txtnrodoc").focus();
                    return false;
                }
            }
            if ($("#ddltipodoc").val() == "RUC") {
                if ($("#txtnrodoc").val().length != 11 || !valida_enteros.test($("#txtnrodoc").val())) {
                    $("#mensaje").attr("class", "alert alert-danger");
                    $("#mensaje").html("<p>Número de RUC debe Tener 11 Digitos (Números Enteros).</p>")
                    $("#txtnrodoc").focus();
                    return false;
                }
            }
            var valida_letras = /^[a-zA-ZñÑ\s\W]/;
            if ($("#txtapepat").val() == "") {
                $("#mensaje").attr("class", "alert alert-danger")
                $("#mensaje").html("<p>Ingrese Apellido Paterno.</p>")
                $("#txtapepat").focus();
                return false;
            }
            if (!valida_letras.test($("#txtapepat").val())) {
                $("#mensaje").attr("class", "alert alert-danger")
                $("#mensaje").html("<p>No se Aceptan Números en el Apellido Paterno.</p>")
                $("#txtapepat").focus();
                return false;
            }
            if ($("#txtapemat").val() == "") {
                $("#mensaje").attr("class", "alert alert-danger")
                $("#mensaje").html("<p>Ingrese Apellido Materno.</p>")
                $("#txtapemat").focus();
                return false;
            }
            if (!valida_letras.test($("#txtapemat").val())) {
                $("#mensaje").attr("class", "alert alert-danger")
                $("#mensaje").html("<p>No se Aceptan Números en el Apellido Materno.</p>")
                $("#txtapemat").focus();
                return false;
            }
            if ($("#txtnombres").val() == "") {
                $("#mensaje").attr("class", "alert alert-danger")
                $("#mensaje").html("<p>Ingrese el Nombre.</p>")
                $("#txtnombres").focus();
                return false;
            }
            if (!valida_letras.test($("#txtnombres").val())) {
                $("#mensaje").attr("class", "alert alert-danger")
                $("#mensaje").html("<p>No se Aceptan Números en el Nombre.</p>")
                $("#txtnombres").focus();
                return false;
            }
            if ($("#txtfechaNacimiento").val() == "") {
                $("#mensaje").attr("class", "alert alert-danger")
                $("#mensaje").html("<p>Seleccione Fecha de Nacimiento.</p>")
                $("#txtfechaNacimiento").focus();
                return false;
            }
            if ($("#ddlsexo").val() == "0") {
                $("#mensaje").attr("class", "alert alert-danger")
                $("#mensaje").html("<p>Seleccione Sexo.</p>")
                $("#ddlsexo").focus();
                return false;
            }

            if ($("#txtemail").val() == "") {
                $("#mensaje").attr("class", "alert alert-danger")
                $("#mensaje").html("<p>Ingrese Correo Electrónico.</p>")
                $("#txtemail").focus();
                return false;
            }

            validamail = /^[-\w.%+]{1,64}@(?:[A-Z0-9-]{1,63}\.){1,125}[A-Z]{2,63}$/i;
            //Se muestra un texto a modo de ejemplo, luego va a ser un icono
            if (!validamail.test($("#txtemail").val())) {
                $("#mensaje").attr("class", "alert alert-danger")
                $("#mensaje").html("<p>Ingrese correctamente Correo Electrónico.</p>")
                $("#txtemail").focus();
                return false;

            }

            if ($("#ddlUniversidad").val() == "-1") {
                $("#mensaje").attr("class", "alert alert-danger")
                $("#mensaje").html("<p>Seleccione el Universidad.</p>")
                $("#ddlUniversidad").focus();
                return false;
            }

            var captcha = grecaptcha.getResponse();

            if (captcha == "" || captcha == undefined) {
                $("#mensaje").attr("class", "alert alert-danger")
                $("#mensaje").html("<p>Resolver Captcha.</p>")
                return false;
            }

            if (confirm("¿Esta Seguro que desea Registrar sus Datos?") == true) {
                MascaraEspera("1");
                return true;
            }

            return false;
        }

        function validarnumeros(e) {
            var unicode = e.charCode ? e.charCode : e.keyCode
            if (unicode != 8) {
                if (unicode < 48 || unicode > 57) //if not a number
                { return false } //disable key press    
            }
        }

        function activacionBotonesSubmit() {
            var validacion = $('#chkTerminosCondiciones').is(':checked') // && flagCaptchaValidated; //DESARROLLO
            $('#btnInscribir').prop('disabled', !validacion);
        }

        function MostrarTerminosCondiciones() {
            var $mdlGenerico = $('#mdlGenerico');
            $mdlGenerico.find('#mensajeGenerico').html($('#terminosCondiciones').html());
            $mdlGenerico.modal('show');
        }

    </script>

    <!-- Global site tag (gtag.js) - Google Analytics -->

    <script async src="https://www.googletagmanager.com/gtag/js?id=UA-121412813-1"></script>

    <script>
        window.dataLayer = window.dataLayer || [];
        function gtag() { dataLayer.push(arguments); }
        gtag('js', new Date());

        gtag('config', 'UA-121412813-1');
    </script>

    <!-- Fin Analytics-->
    <style type="text/css">
        .row
        {
            margin-right: 0px;
            margin-left: 0px;
            padding: 3px;
        }
        .form-group
        {
            margin: 3px;
        }
        .form-control
        {
            border-radius: 0px;
            box-shadow: none;
            border-color: #d9d9d9;
            height: 30px; /*font-weight: 300;  line-height: 40px; */
            color: black;
        }
        .control-label
        {
            padding-top: 5px;
            text-align: right;
        }
    </style>
</head>
<body>
    <div class="container">
        <div class="page-header">
            <h2>
                <span class="alert alert-danger"><i class="fa fa-pencil" aria-hidden="true"></i>
                </span>&nbsp;Inscripción</h2>
        </div>
        <div id="shenlong">
            <form id="form1" runat="server">
            <%--        <div class="row">
                <div class="form-group">
                    <label class="col-sm-3 control-label" for="txtceco">
                        Centro de Costos:</label>
                    <div class="col-sm-6">
                        <asp:TextBox runat="server" name="txtceco" ID="txtceco" value="2324" class="form-control"></asp:TextBox>
                    </div>
                </div>
            </div>--%>
            <div class="row">
                <div class="form-group">
                    <label class="col-sm-3 control-label" for="txtceco">
                        Evento:</label>
                    <div class="col-sm-6" id="Descripcion" runat="server">
                    </div>
                    <asp:HiddenField ID="hdtoken" runat="server" />
                    <asp:HiddenField ID="hdevento" runat="server" />
                    <asp:HiddenField ID="hdmonto" runat="server" />
                    <asp:HiddenField ID="hdfechavenc" runat="server" />
                </div>
            </div>
            <div class="row">
                <div class="form-group">
                    <label class="col-sm-3 control-label" for="ddltipoparticipante">
                        Tipo Inscripción:</label>
                    <div class="col-sm-3">
                        <select runat="server" name="ddltipoparticipante" id="ddltipoparticipante" class="form-control" onserverchange="ddltipoparticipante_ServerChange" onchange="javascript:form1.submit();">
                        </select>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="form-group">
                    <label class="col-sm-3 control-label" for="ddltipodoc">
                        Tipo Documento:</label>
                    <div class="col-sm-3">
                        <asp:DropDownList runat="server" name="ddltipodoc" ID="ddltipodoc" class="form-control">
                            <asp:ListItem Value="0">-- Seleccione --</asp:ListItem>
                            <asp:ListItem Value="DNI">DNI</asp:ListItem>
                            <asp:ListItem Value="RUC">RUC</asp:ListItem>
                            <asp:ListItem Value="PASAPORTE">PASAPORTE</asp:ListItem>
                            <asp:ListItem Value="CARNÉ DE EXTRANJERÍA">CARNÉ DE EXTRANJERÍA</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="form-group">
                    <label class="col-sm-3 control-label" for="txttitulo">
                        Número Documento:</label>
                    <div class="col-sm-3">
                        <asp:TextBox runat="server" name="txtnrodoc" ID="txtnrodoc" class="form-control"></asp:TextBox>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="form-group">
                    <label class="col-sm-3 control-label" for="txtapemat">
                        Apellido Paterno:</label>
                    <div class="col-sm-4">
                        <asp:TextBox runat="server" name="txtapepat" ID="txtapepat" class="form-control"></asp:TextBox>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="form-group">
                    <label class="col-sm-3 control-label" for="txttitulo">
                        Apellido Materno:</label>
                    <div class="col-sm-4">
                        <asp:TextBox runat="server" name="txtapemat" ID="txtapemat" class="form-control"></asp:TextBox>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="form-group">
                    <label class="col-sm-3 control-label" for="txtnombres">
                        Nombres:</label>
                    <div class="col-sm-4">
                        <asp:TextBox runat="server" runat="server" name="txtnombres" ID="txtnombres" class="form-control"></asp:TextBox>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="form-group">
                    <label class="col-sm-3 control-label" for="txtfechaNacimiento">
                        Fecha Nacimiento:</label>
                    <div class="col-sm-3">
                        <%--<asp:TextBox runat="server" name="txtfechaNacimiento" ID="txtfechaNacimiento" class="form-control"></asp:TextBox>--%>
                        <div class="input-group date" id="FechaNacimiento" runat="server">
                            <input name="txtfechaNacimiento" class="form-control" id="txtfechaNacimiento" style="text-align: right;"
                                runat="server" type="text" placeholder="__/__/____" data-provide="datepicker">
                            <span class="input-group-addon sm"><i class="fa fa-calendar" id="DivFechaNacimiento">
                            </i></span>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="form-group">
                    <label class="col-sm-3 control-label" for="ddlsexo">
                        Sexo:</label>
                    <div class="col-sm-3">
                        <asp:DropDownList runat="server" name="ddlsexo" ID="ddlsexo" class="form-control">
                            <asp:ListItem Value="0">-- Seleccione --</asp:ListItem>
                            <asp:ListItem Value="M">MASCULINO</asp:ListItem>
                            <asp:ListItem Value="F">FEMENINO</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="form-group">
                    <label class="col-sm-3 control-label" for="txtemail">
                        Correo Electrónico:</label>
                    <div class="col-sm-4">
                        <asp:TextBox runat="server" name="txtemail" ID="txtemail" class="form-control"></asp:TextBox>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="form-group">
                    <label class="col-sm-3 control-label" for="txttelefono">
                        Celular/Teléfono:</label>
                    <div class="col-sm-4">
                        <asp:TextBox runat="server" name="txttelefono" ID="txttelefono" class="form-control" onkeypress="return validarnumeros(event);"></asp:TextBox>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="form-group">
                    <asp:label class="col-sm-3 control-label" for="ddlUniversidad" ID="lblUniversidad" runat="server" style="font-weight:bold;">
                        Universidad:</asp:label>
                     <div class="col-sm-6" id="divSelec" runat="server">
                        <%--<asp:DropDownList runat="server" name="ddlUniversidad" ID="ddlUniversidad" class="form-control">
                        </asp:DropDownList>--%>
                        <select runat="server" name="ddlUniversidad" id="ddlUniversidad" class="form-control">
                        </select>
                        
                    </div>
                    <div class="col-sm-6" id="divText" runat="server" visible="false">
                        <asp:TextBox runat="server" runat="server" name="txtUniversidad" ID="txtUniversidad" class="form-control"></asp:TextBox>
                    </div>
                </div>
            </div>
            <div class="row hidden">
                <div class="form-group">
                     <label class="col-sm-3 control-label" for="txtemail">
                        Requiere Factura:</label>
                        <div id="Div1" class="col-sm-6" runat="server">
                            <asp:CheckBox ID="chkReqFactura" runat="server" />
                        </div>
                </div>
            </div>
            <div class="modal fade" id="mdlGenerico" tabindex="-1" role="dialog" aria-hidden="true">
                <div class="modal-dialog modal-dialog-centered modal-lg" role="document">
                    <div class="modal-content">
                        <div class="modal-body">
                            <div id="mensajeGenerico"></div>
                        </div>
                        <div class="modal-footer">
                            <button type="button" id="btnModalAceptar" class="btn btn-sm btn-danger" data-dismiss="modal">Aceptar</button>
                        </div>
                    </div>
                </div>
            </div>
             <div id="terminosCondiciones" style="display:none">
                <div class="terminos-condiciones">
                    <h1>Términos y Condiciones</h1>
                    <p>
                        De conformidad con la Ley N° 29733, Ley de Protección de Datos Personales, autorizo a la USAT a utilizar los datos personales proporcionados o que proporcione a futuro, para la gestión administrativa y comercial que realice. 
                    </p>
                    <p>
                        Asimismo, de conformidad con las Leyes N° 28493 y N° 29571 brindo mi consentimiento para que la USAT me envíe información, 
                        publicidad, encuestas y estadísticas de sus servicios educativos; teniendo pleno conocimiento que puedo acceder, rectificar, 
                        oponerme y cancelar mis datos personales, así como revocar mi consentimiento enviando un correo a informacion@usat.edu.pe. 
                    </p>
                </div>
            </div>
            <div id="mensaje" runat="server">
            </div>
           <div class="form-group row">
                <center>
                    <div class="g-recaptcha" data-sitekey="6LemTGAUAAAAAC4qhRnTPNDqY1XyNS35KSG6WxJo">
                    </div>
                    <br />
                    <div class="contenedor-terminos-condiciones">
                        <div class="custom-control custom-checkbox">    
                            <input type="checkbox" id="chkTerminosCondiciones" runat="server" class="custom-control-input ignore">
                            <label for="chkTerminosCondiciones" class="custom-control-label">
                                He leído y acepto los términos y condiciones -> <a href="#" id="lnkLeerTerminosCondiciones">Leer aquí*</a>
                            </label>
                        </div>
                    </div>
                    <br />
                    <asp:Button runat="server" Text="Inscribir" name="btnInscribir" ID="btnInscribir" disabled
                        OnClientClick="return validar();" class="btn btn-primary" />
                </center>
                
            </div>
        </form>
    </div>
    </div>
</body>
</html>
