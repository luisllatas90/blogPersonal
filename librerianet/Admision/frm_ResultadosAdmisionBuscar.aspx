<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frm_ResultadosAdmisionBuscar.aspx.vb" Inherits="Admision_frm_ResultadosAdmisionBuscar" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta name="description" content="Estudiar en la USAT no solo es estudiar una carrera profesional, nos comprometemos con el logro de tu proyecto de vida">
    <meta name="author" content="USAT">
    <title>Resultados de evaluación USAT</title>
    <!-- Style -->
    <link href="css/bootstrap.css" rel="stylesheet">
    <!--<link href="css/style2.css" rel="stylesheet">-->
    <!-- Responsive -->
    <link href="css/responsive.css" rel="stylesheet">
    <link rel="stylesheet" href="../assets/css/toastr/toastr.css">
    <style>
        .topspace30 {
            margin-top: 30px;
        }

        ::selection {
            background: #f54828;
            color: #fff;
        }

        body {
            font-family: 'Open Sans', "Helvetica Neue", Helvetica, Arial, sans-serif;
            font-size: 13px;
            line-height: 22px;
            color: #777;
            overflow-y: hidden;
        }

        .row {
            margin-left: 0;
            margin-right: 0;
        }

        .car-highlight1 {
            font-size: 20px;
            line-height: 20px;
            font-weight: 800;
            color: rgb(255, 255, 255);
            text-decoration: none;
            background-color: #f54828;
            padding: 10px;
            border-width: 0px;
            border-color: rgb(255, 214, 88);
            border-style: none;
            display: inline-block;
        }

        .btn,
        .alert,
        .progress,
        .form-control,
        .breadcrumb,
        .well {
            border-radius: 0;
        }

        input,
        button,
        select,
        textarea {
            background-image: none;
            border: 1px solid #e1e1e1;
            padding: 7px;
            margin-bottom: 15px;
            font-size: 12px;
        }

        .btn-default {
            color: #fff;
            background-color: #f54828;
            border: 0;
        }

        .btn {
            padding: 8px 12px;
        }

        .error input,
        input.error,
        .error textarea,
        textarea.error {
            background-color: #ffffff;
            border: 1px solid red !Important;
            -webkit-transition: border linear 0.2s, box-shadow linear 0.2s;
            -moz-transition: border linear 0.2s, box-shadow linear 0.2s;
            -o-transition: border linear 0.2s, box-shadow linear 0.2s;
            transition: border linear 0.2s, box-shadow linear 0.2s;
        }

        .form-group {
            margin-bottom: 0px;
        }

        input.error,
        select.error {
            margin-bottom: 0px;
        }

        select.error {
            border: 1px solid #FF0000;
        }

        .form-horizontal .control-label {
            color: white;
        }

        label.error {
            color: red;
        }

        .g-recaptcha {
            text-align: left;
        }
    </style>
</head>

<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="scriptManager" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="udpForm" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
            <ContentTemplate>
                <section class="topspace30">
                    <div class="bgarea-examen2">
                        <div class="container">
                            <div class="row">
                                <div class="col-md-12">
                                    <center>
                                        <div>

                                            <br>
                                            <div class="car-highlight1" style="width:100%">
                                                <b>CONSULTA DE RESULTADOS</b></div>
                                            <br>
                                            <div id="ocultar" name="ocultar">
                                                <br>
                                                <div class="form-group">
                                                    <label for="dni" class="col-sm-4 control-label" style="color:#666">Ingrese su DNI</label>
                                                    <div class="col-sm-5">
                                                        <input onkeypress="return IsNumber(event);" type="text" name="dni" runat="server" class="form-control" id="dni"
                                                            placeholder="N° DNI o Carnet de extranjería" maxlength="12" autocomplete="off">
                                                        <input type="hidden" id="evaluacion" name="evaluacion" value="">
                                                    </div>

                                                </div>
                                                <div class="form-group">
                                                    <label for="hiddenRecaptcha" class="col-sm-4 control-label">Valide el Captcha</label>
                                                    <div class="col-sm-8">
                                                        <div class="g-recaptcha" data-sitekey="6LcYUmoUAAAAAMoNG382w_R4kquhM45M6I0Zerus"></div>
                                                        <input type="hidden" class="hiddenRecaptcha required" name="hiddenRecaptcha" id="hiddenRecaptcha">
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="col-sm-12">
                                                        <br>
                                                        <asp:LinkButton type="button" id="btnEnviar" runat="server" class="btn btn-default btn-block" OnClientClick="return validar();">
                                                            <i class="fa fa-search"></i>
                                                            <span class="text">Enviar</span>
                                                        </asp:LinkButton>
                                                        <br>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </center>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-12">
                                    <center>
                                        <asp:UpdatePanel ID="udpMensaje" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                            <ContentTemplate>
                                                <div id="mensaje" runat="server"></div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </center>
                                </div>
                            </div>
                        </div>
                    </div>
                </section>
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
    <!-- SCRIPTS, placed at the end of the document so the pages load faster
    ================================================== -->
    <script src='https://www.google.com/recaptcha/api.js'></script>
    <script src="../assets/js/jquery-3.3.1/jquery-3.3.1.js"></script>
    <script src="../assets/js/bootstrap-4.1/bootstrap.js"></script>
    <script src="../assets/js/toastr/toastr.min.js"></script>
    <script>
        function validar() {
            var dni = $('#dni').val().trim();
            if (dni == '') {
                toastr.error('Debe ingresar un número de DNI', 'Error');
                $('#dni').focus();
                return false;
            }

            // CAPTCHA
            var isCaptchaValidated = false;
            var response = grecaptcha.getResponse();
            if (response.length == 0) {
                isCaptchaValidated = false;
                toastr.error('Debe validar el captcha', 'Error');
                return false;
            }

            return true;
        }

        function IsNumber(evt) {
            var nav4 = window.Event ? true : false;
            // Backspace = 8, Enter = 13, ’0′ = 48, ’9′ = 57, ‘.’ = 46
            var key = nav4 ? evt.which : evt.keyCode;
            return ((key >= 48 && key <= 57) || key == 8);
        }
    </script>
</body>

</html>