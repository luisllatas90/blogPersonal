<%@ Page Language="VB" AutoEventWireup="false" CodeFile="FrmListaConcurso.aspx.vb"
    Inherits="GestionInvestigacion_FrmListaConcurso" %>

<!DOCTYPE html>
<html lang="en">
<head runat="server">
    <%--Compatibilidad con IE--%>
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta http-equiv='X-UA-Compatible' content='IE=8' />
    <meta http-equiv='X-UA-Compatible' content='IE=10' />
    <%--Compatibilidad con IE--%>
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=0" />
    <meta http-equiv="Pragma" content="no-cache" />
    <!-- Cargamos css -->
    <link rel='stylesheet' href='../assets/css/bootstrap.min.css?x=1' />
    <link rel='stylesheet' href='../assets/css/material.css' />
    <link rel='stylesheet' href='../assets/css/style.css?x=1' />
    <!-- Cargamos JS -->

    <script type="text/javascript" src='../assets/js/jquery.js'></script>

    <script src="../assets/js/app.js" type="text/javascript"></script>

    <script type="text/javascript" src='../assets/js/jquery-ui-1.10.3.custom.min.js'></script>

    <script src="../assets/js/bootstrap.min.js" type="text/javascript"></script>

    <script type="text/javascript" src='../assets/js/jquery.nicescroll.min.js'></script>

    <script type="text/javascript" src='../assets/js/wow.min.js'></script>

    <script type="text/javascript" src="../assets/js/jquery.nicescroll.min.js"></script>

    <script type="text/javascript" src='../assets/js/jquery.loadmask.min.js'></script>

    <script type="text/javascript" src='../assets/js/jquery.accordion.js'></script>

    <script type="text/javascript" src='../assets/js/materialize.js'></script>

    <script type="text/javascript" src='../assets/js/form-elements.js'></script>

    <script type="text/javascript" src='../assets/js/select2.js'></script>

    <script src='../assets/js/bootstrap-datepicker.js' type="text/javascript"></script>

    <script type="text/javascript" src='../assets/js/jquery.multi-select.js'></script>

    <script type="text/javascript" src='../assets/js/bootstrap-colorpicker.js'></script>

    <script type="text/javascript" src='../assets/js/jquery.dataTables.min.js'></script>

    <link rel='stylesheet' href='../assets/css/jquery.dataTables.min.css' />
    <%-- ======================= Inicio Notificaciones =============================================--%>

    <script type="text/javascript" src="../assets/js/noty/jquery.noty.js"></script>

    <script type="text/javascript" src='../assets/js/noty/layouts/top.js'></script>

    <script type="text/javascript" src='../assets/js/noty/layouts/default.js'></script>

    <script type="text/javascript" src="../assets/js/noty/notifications-custom.js"></script>

    <%-- ======================= Fin Notificaciones =============================================--%>

    <script src="js/_General.js?x=1" type="text/javascript"></script>

    <script src="js/Concurso.js?z=2" type="text/javascript"></script>

    <title>Lista de Concursos</title>
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
            text-align: left;
        }
    </style>
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
                    <div class="manage_buttons">
                        <div class="row">
                            <div id="PanelBusqueda">
                                <div class="col-md-12 col-sm-12 col-lg-12">
                                    <div class="page_header">
                                        <div class="pull-left">
                                            <i class="icon ti-bookmark-alt page_header_icon"></i><span class="main-text">Concursos</span>
                                        </div>
                                        <div class="buttons-list">
                                            <div class="pull-right-btn">
                                                <button class="btn btn-primary" id="btnConsultar" value="Consultar" onclick="fnListarConcurso()">
                                                    Consultar</button>
                                                <button class="btn btn-green" id="btnAgregar" value="Agregar" onclick="fnNuevoConcurso();">
                                                    Agregar</button>
                                            </div>
                                        </div>
                                        <form class="form form-horizontal" id="frmbuscar" onsubmit="return false;" action="#"
                                        method="post">
                                        <div class="form-group">
                                            <div class="col-md-7">
                                                <label class="col-md-3 control-label ">
                                                    Titulo de Concurso</label>
                                                <div class="col-md-9">
                                                    <input type="text" class="form-control" id="txtBusqueda" name="txtBusqueda" />
                                                </div>
                                            </div>
                                            <div class="col-md-5">
                                                <label class="col-md-4 control-label ">
                                                    Estado
                                                </label>
                                                <div class="col-md-8">
                                                    <select name="cboEstado" class="form-control" id="cboEstado">
                                                        <option value="">-- Seleccione -- </option>
                                                        <option value="1" selected="selected">EN PROCESO</option>
                                                        <option value="2">CULMINADO</option>
                                                        <option value="T">TODOS</option>
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
                    <div class="panel-piluku">
                        <div class="panel-heading">
                            <h3 class="panel-title">
                                Concursos
                            </h3>
                        </div>
                        <div class="panel-body">
                            <form class="form form-horizontal" id="frmLista" onsubmit="return false;" action="#"
                            method="post">
                            </form>
                            <div class="table-responsive">
                                <div id="tProyectos_wrapper" class="dataTables_wrapper" role="grid">
                                    <table id="tConcursos" name="tConcursos" class="display dataTable" width="100%">
                                        <thead>
                                            <tr role="row">
                                                <td width="4%" style="font-weight: bold; width: 54px; text-align: center" class="sorting_asc"
                                                    tabindex="0" rowspan="1" colspan="1" aria-sort="ascending" aria-label="N°: activate to sort column descending">
                                                    N°
                                                </td>
                                                <td width="33%" style="font-weight: bold; width: 89px; text-align: center" class="sorting"
                                                    tabindex="0" rowspan="1" colspan="1" aria-label="Nombre: activate to sort column ascending">
                                                    Titulo
                                                </td>
                                                <td width="10%" style="font-weight: bold; width: 169px; text-align: center" class="sorting"
                                                    tabindex="0" rowspan="1" colspan="1" aria-label="Tipo de Estudio: activate to sort column ascending">
                                                    Fecha Inicio
                                                </td>
                                                <td width="10%" style="font-weight: bold; width: 272px; text-align: center" class="sorting"
                                                    tabindex="0" rowspan="1" colspan="1" aria-label="Fecha Inicio: activate to sort column ascending">
                                                    Fecha Fin
                                                </td>
                                                <td width="10%" style="font-weight: bold; width: 272px; text-align: center" class="sorting"
                                                    tabindex="0" rowspan="1" colspan="1" aria-label="Ámbito: activate to sort column ascending">
                                                    Ámbito
                                                </td>
                                                <td width="13%" style="font-weight: bold; width: 272px; text-align: center" class="sorting"
                                                    tabindex="0" rowspan="1" colspan="1" aria-label="Fecha Inicio: activate to sort column ascending">
                                                    Tipo
                                                </td>
                                                <td width="13%" style="font-weight: bold; width: 272px; text-align: center" class="sorting"
                                                    tabindex="0" rowspan="1" colspan="1" aria-label="Fecha Inicio: activate to sort column ascending">
                                                    Estado
                                                </td>
                                                <td width="8%" style="font-weight: bold; width: 111px; text-align: center" class="sorting"
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
                                        <tbody id="tbConcursos">
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="modal fade" id="mdRegistro" role="dialog" aria-labelledby="myModalLabel"
                        style="z-index: 0;" aria-hidden="true" data-backdrop="static" data-keyboard="false">
                        <div class="modal-dialog modal-lg" id="modalReg">
                            <div class="modal-content">
                                <div class="modal-header" style="background-color: #E33439; color: White; font-weight: bold;">
                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close" style="float: right;">
                                        <span aria-hidden="true" class="ti-close" style="color: White;"></span>
                                    </button>
                                    <h4 class="modal-title" id="myModalLabel3">
                                        Concurso</h4>
                                </div>
                                <div class="modal-body">
                                    <form id="frmRegistro" name="frmRegistro" enctype="multipart/form-data" class="form-horizontal"
                                    method="post" onsubmit="return false;" action="#">
                                    <div class="row">
                                        <div class="form-group">
                                            <label class="col-sm-3 control-label" for="txttitulo">
                                                Título:</label>
                                            <div class="col-sm-8">
                                                <input type="hidden" id="hdcod" name="hdcod" value="0" />
                                                <input type="text" id="txttitulo" name="txttitulo" class="form-control" maxlength="500" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="form-group">
                                            <label class="col-sm-3 control-label">
                                                Breve Descripción:</label>
                                            <div class="col-md-8">
                                                <textarea class="form-control" cols="50" rows="3" id="txtdescripcion" name="txtdescripcion"
                                                    maxlength="800"></textarea>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="form-group">
                                            <label class="col-sm-3 control-label">
                                                Ámbito:</label>
                                            <div class="col-md-3">
                                                <select id="cboAmbito" name="cboAmbito" class="form-control">
                                                    <option value="">--Seleccione--</option>
                                                    <option value="0">INTERNO</option>
                                                    <option value="1">EXTERNO</option>
                                                </select>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="form-group">
                                            <label class="col-sm-3 control-label">
                                                Fecha Inicio Postulación:</label>
                                            <div class="col-sm-3" id="date-popup-group">
                                                <div id="txtFechaInicio">
                                                </div>
                                                <div class="input-group date" id="FechaInicio">
                                                    <input name="txtfecini" class="form-control" id="txtfecini" style="text-align: right;"
                                                        type="text" placeholder="__/__/____" data-provide="datepicker" />
                                                    <span class="input-group-addon sm"><i class="ion ion-ios-calendar-outline" id="FecInicial">
                                                    </i></span>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="form-group">
                                            <label class="col-sm-3 control-label">
                                                Fecha Fin Postulación:</label>
                                            <div class="col-sm-3" id="date-popup-group">
                                                <div class="input-group date" id="FechaFin">
                                                    <input name="txtfecfin" class="form-control" id="txtfecfin" style="text-align: right;"
                                                        type="text" placeholder="__/__/____" data-provide="datepicker" />
                                                    <span class="input-group-addon sm"><i class="ion ion-ios-calendar-outline" id="FecFinal">
                                                    </i></span>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="form-group">
                                            <label class="col-sm-3 control-label">
                                                Fecha Fin Evaluación:</label>
                                            <div class="col-sm-3" id="date-popup-group">
                                                <div class="input-group date" id="FechaFinEvaluacion">
                                                    <input name="txtfecfineva" class="form-control" id="txtfecfineva" style="text-align: right;"
                                                        type="text" placeholder="__/__/____" data-provide="datepicker" />
                                                    <span class="input-group-addon sm"><i class="ion ion-ios-calendar-outline" id="I1"></i>
                                                    </span>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="form-group">
                                            <label class="col-sm-3 control-label">
                                                Fecha de Publicación de Resultados:</label>
                                            <div class="col-sm-3" id="date-popup-group">
                                                <div class="input-group date" id="FechaResultados">
                                                    <input name="txtfecres" class="form-control" id="txtfecres" style="text-align: right;"
                                                        type="text" placeholder="__/__/____" data-provide="datepicker" />
                                                    <span class="input-group-addon sm"><i class="ion ion-ios-calendar-outline" id="I2"></i>
                                                    </span>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="form-group">
                                            <label class="col-sm-3 control-label">
                                                Tipo:</label>
                                            <div class="col-md-3">
                                                <select id="cbotipo" name="cbotipo" class="form-control">
                                                    <option value="">--Seleccione--</option>
                                                    <option value="0">INDIVIDUAL</option>
                                                    <%--<option value="1">UNIDISCIPLINARIO</option>--%>
                                                    <option value="2">GRUPAL</option>
                                                    <option value="3">INDIVIDUAL/GRUPAL</option>
                                                </select>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="form-group">
                                            <label class="col-sm-3 control-label">
                                                Bases(PDF)</label>
                                            <div class="col-sm-8">
                                                <input type="file" id="fileBases" name="fileBases" />
                                                <div id="file_Bases">
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    </form>
                                </div>
                                <div class="modal-footer">
                                    <center>
                                        <button type="button" id="btnA" class="btn btn-primary" onclick="fnGuardarConcurso();">
                                            Guardar</button>
                                        <button type="button" id="btnCancelarReg" class="btn btn-danger" onclick="fnCancelar();">
                                            Cancelar</button>
                                    </center>
                                </div>
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
