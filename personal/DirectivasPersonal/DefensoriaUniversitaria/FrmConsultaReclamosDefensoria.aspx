<%@ Page Language="VB" AutoEventWireup="false" CodeFile="FrmConsultaReclamosDefensoria.aspx.vb"
    Inherits="Crm_FrmListaTipoComunicacion" %>

<!DOCTYPE html>
<html lang="en">
<head id="Head1" runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta http-equiv='X-UA-Compatible' content='IE=8' />
    <meta http-equiv='X-UA-Compatible' content='IE=10' />
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=0" />
    <meta http-equiv="Pragma" content="no-cache" />
    <link rel='stylesheet' href='../assets/css/bootstrap.min.css?x=1' />
    <link rel='stylesheet' href='../assets/css/material.css' />
    <link rel='stylesheet' href='../assets/css/style.css?x=2' />

    <script type="text/javascript" src='../assets/js/jquery.js'></script>

    <script type="text/javascript" src='../assets/js/app.js'></script>

    <script type="text/javascript" src='../assets/js/jquery-ui-1.10.3.custom.min.js'></script>

    <script type="text/javascript" src='../assets/js/bootstrap.min.js'></script>

    <script type="text/javascript" src='../assets/js/jquery.nicescroll.min.js'></script>

    <script type="text/javascript" src='../assets/js/wow.min.js'></script>

    <script type="text/javascript" src="../assets/js/jquery.nicescroll.min.js"></script>

    <script type="text/javascript" src='../assets/js/jquery.loadmask.min.js'></script>

    <script type="text/javascript" src='../assets/js/jquery.accordion.js'></script>

    <script type="text/javascript" src='../assets/js/materialize.js'></script>

    <script type="text/javascript" src='../assets/js/bic_calendar.js'></script>

    <script type="text/javascript" src='../assets/js/core.js'></script>

    <script type="text/javascript" src='../assets/js/jquery.countTo.js'></script>

    <script type="text/javascript" src="../assets/js/noty/jquery.noty.js"></script>

    <script type="text/javascript" src='../assets/js/noty/layouts/top.js'></script>

    <script type="text/javascript" src='../assets/js/noty/layouts/default.js'></script>

    <script type="text/javascript" src="../assets/js/noty/notifications-custom.js"></script>

    <script type="text/javascript" src='../assets/js/jquery.dataTables.min.js'></script>

    <link rel='stylesheet' href='../assets/css/jquery.dataTables.min.css' />
    <%--<script type="text/javascript" src='../assets/js/funciones.js'></script>

    <script type="text/javascript" src='../assets/js/funcionesDataTable.js?x=2'></script>--%>

    <script type="text/javascript" src='../assets/js/form-elements.js'></script>

    <script type="text/javascript" src='../assets/js/select2.js'></script>

    <script type="text/javascript" src='../assets/js/jquery.multi-select.js'></script>

    <script type="text/javascript" src='../assets/js/bootstrap-colorpicker.js'></script>

    <script src='../assets/js/bootstrap-datepicker.js'></script>

    <script type="text/javascript" src="js/Defensoria.js?x=3"></script>

    <script type="text/javascript" src="js/JsDefensoria.js?x=3"></script>

    <title></title>
    <style type="text/css">
        .row
        {
            margin-right: 0px;
            margin-left: 0px;
        }
        .content .main-content
        {
            padding-right: 15px;
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
            height: 30px;
            font-weight: 300; /* line-height: 40px; */
            color: black;
        }
        .form-group
        {
            margin: 6px;
        }
        .form-horizontal .control-label
        {
            padding-top: 5px;
        }
        .i-am-new
        {
            z-index: 100;
        }
    </style>

    <script language="javascript" type="text/javascript">
        function btnGuardar_onclick() {
        }

    </script>

</head>
<body class="">
    <div class="piluku-preloader text-center hidden">
        <div class="loader">
            Loading...</div>
    </div>
    <div class="wrapper">
        <div class="content">
            <div class="overlay">
            </div>
            <div class="main-content">
                <div class="row">
                    <div class="manage_buttons" id="divOpc">
                        <div class="row">
                            <div id="PanelBusqueda">
                                <div class="col-md-12 col-sm-12 col-lg-12">
                                    <div class="page_header">
                                        <div class="pull-left">
                                            <i class="icon ti-volume page_header_icon"></i><span class="main-text">Consulta de Registros
                                                de Defensoría Universitaria</span>
                                        </div>
                                        <div class="buttons-list">
                                            <div class="pull-right-btn">
                                                <a class="btn btn-primary" id="btnListarConsulta" name="btnListarConsulta" href="#">
                                                    <i class="ion-android-search"></i>&nbsp;Listar</a>
                                            </div>
                                        </div>
                                        <form class="form form-horizontal" id="frmBuscarConsultaDefensoria" onsubmit="return false;"
                                        action="#" method="post">
                                        <input name="process" type="hidden" id="process">
                                        <div class="row">
                                            <div class="col-md-6">
                                                <label class="col-md-3 control-label ">
                                                    Tipo</label>
                                                <div class="col-md-9">
                                                    <select name="cboTipo" class="form-control" id="cboTipo">
                                                            <option value="T" selected="">-- Seleccione -- </option>
                                                            <option value="C">CONSULTA </option>
                                                            <option value="R">RECLAMO</option>
                                                            <option value="D">DENUNCIA</option>
                                                        </select>
                                                </div>
                                            </div>
                                        </div>
                                        </form>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <!-- panel -->
                    <div class="panel panel-piluku" id="PanelLista">
                        <div class="panel-heading">
                            <h3 class="panel-title">
                                Detalle de Registros de Defensoría Universitaria <span class="panel-options"><a class="panel-refresh"
                                    href="#"><i class="icon ti-reload" onclick="fnBuscarConsulta(false)"></i></a><a class="panel-minimize"
                                        href="#"><i class="icon ti-angle-up"></i></a></span>
                            </h3>
                        </div>
                        <div class="panel-body">
                            <div class="table-responsive">
                                <div id="tConsultaDefensoria_wrapper" class="dataTables_wrapper" role="grid">
                                    <table id="tConsultaDefensoria" name="tConsultaDefensoria" class="display dataTable" width="100%">
                                        <thead>
                                            <tr role="row">
                                                <td width="5%" style="font-weight: bold; width: 5px; text-align: center" class="sorting_asc"
                                                    tabindex="0" rowspan="1" colspan="1" aria-sort="ascending" aria-label="N°: activate to sort column descending">
                                                    N°
                                                </td>
                                                <td width="8%" style="font-weight: bold; width: 8px; text-align: center" class="sorting"
                                                    tabindex="0" rowspan="1" colspan="1" aria-label="Nombre: activate to sort column ascending">
                                                    Fecha
                                                </td>
                                                <td width="10%" style="font-weight: bold; width: 10px; text-align: center" class="sorting"
                                                    tabindex="0" rowspan="1" colspan="1" aria-label="Nombre: activate to sort column ascending">
                                                    Telefono
                                                </td>
                                                <td width="10%" style="font-weight: bold; width: 10px; text-align: center" class="sorting"
                                                    tabindex="0" rowspan="1" colspan="1" aria-label="Nombre: activate to sort column ascending">
                                                    Mail
                                                </td>
                                                <td width="30%" style="font-weight: bold; width: 30px; text-align: center" class="sorting"
                                                    tabindex="0" rowspan="1" colspan="1" aria-label="Nombre: activate to sort column ascending">
                                                    Detalle
                                                </td>
                                                <td width="10%" style="font-weight: bold; width: 10px; text-align: center" class="sorting"
                                                    tabindex="0" rowspan="1" colspan="1" aria-label="Nombre: activate to sort column ascending">
                                                    Tipo
                                                </td>
                                                <td width="20%" style="font-weight: bold; width: 20px; text-align: center" class="sorting"
                                                    tabindex="0" rowspan="1" colspan="1" aria-label="Nombre: activate to sort column ascending">
                                                    Persona
                                                </td>
                                            </tr>
                                        </thead>
                                        <tfoot>
                                            <tr>
                                                <th colspan="8" rowspan="1">
                                                </th>
                                            </tr>
                                        </tfoot>
                                        <tbody id="tbConsultaDefensoria">
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                        </div>
                    </div>
                    <!-- /panel -->
                </div>
                <!-- row -->
            </div>
        </div>
    </div>
    <div class="hiddendiv common">
    </div>
</body>
</html>
