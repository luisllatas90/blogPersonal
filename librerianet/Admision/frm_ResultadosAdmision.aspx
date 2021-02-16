<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frm_ResultadosAdmision.aspx.vb" Inherits="Admision_frm_ResultadosAdmision" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta name="description" content="Estudiar en la USAT no solo es estudiar una carrera profesional, nos comprometemos con el logro de tu proyecto de vida">
    <meta name="author" content="USAT">
    <meta http-equiv="Expires" content="0">
    <meta http-equiv="Last-Modified" content="0">
    <meta http-equiv="Cache-Control" content="no-cache, mustrevalidate">
    <meta http-equiv="Pragma" content="no-cache">
    <title>Listado de evaluaciones USAT</title>
    <!-- Style -->
    <link href="css/bootstrap.css" rel="stylesheet">
    <!--<link href="css/style2.css" rel="stylesheet">-->
    <!-- Responsive -->
    <link href="css/responsive.css" rel="stylesheet">
    <style>
        .topspace30 {
            margin-top: 30px;
        }

        body {
            font-family: 'Open Sans', "Helvetica Neue", Helvetica, Arial, sans-serif;
            font-size: 13px;
            line-height: 22px;
            color: #777;
            overflow-y: hidden;
        }

        ::selection {
            background: #f54828;
            color: #fff;
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

        a {
            color: #f54828;
            text-decoration: none;
        }

        .alert {
            font-size: 14px;
        }

        .btn,
        .alert,
        .progress,
        .form-control,
        .breadcrumb,
        .well {
            border-radius: 0;
        }

        strong,
        b {
            font-weight: 500;
        }
    </style>
</head>

<body>
    <!--INICIO SCRIPT FORMULARIO-->
    <!-- Load jQuery and jQuery-Validate scripts -->
    <script src="js/jquery.js"></script>
    <%--<form id="form1" runat="server">
    <div>--%>
    <section class="topspace30">
        <!--<div class="container">-->
        <div class="row">
            <div class="col-md-12">
                <br>
                <div class="car-highlight1" style="width:100%">
                    <center><b>ÚLTIMOS RESULTADOS</b></center>
                </div>
                <br>
                <br>
                <asp:Repeater id="rptEventoAdmision" runat="server">
                    <ItemTemplate>
                        <a href='frm_ResultadosAdmisionBuscar.aspx?cco=<%# Eval("codigo_Cco") %>'>
                            <div class="alert alert-warning alert-dismissable">
                                <b><%# Eval("descripcion_Cco") %></b>
                            </div>
                        </a>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
            <!--<div class="col-md-6 animated fadeInRightNow notransition"></div>-->
        </div>
        <!--</div>-->
    </section>
    <script src="js/bootstrap.js"></script>
    <%--</div>
    </form>--%>
</body>

</html>