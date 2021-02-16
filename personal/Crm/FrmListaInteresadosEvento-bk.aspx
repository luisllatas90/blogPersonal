<%@ Page Language="VB" AutoEventWireup="false" CodeFile="FrmListaInteresadosEvento-bk.aspx.vb"
    Inherits="Crm_FrmListaConvocatoria" %>

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
    <link rel='stylesheet' href='../assets/css/style.css?x=1' />

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

    <script type="text/javascript" src='../assets/js/jquery.dataTables.min.js?x=1'></script>

    <link rel='stylesheet' href='../assets/css/jquery.dataTables.min.css' />
    <%--<script src="../assets/js/dataTables.buttons.min.js" type="text/javascript"></script>--%>
    <%--<link href="../assets/css/dataTables.tableTools.css" rel="stylesheet" type="text/css" />--%>

    <script src="../assets/js/dataTables.tableTools.js" type="text/javascript"></script>

    <%--<script type="text/javascript" src='../assets/js/funciones.js'></script>--%>

    <%--<script type="text/javascript" src='../assets/js/funcionesDataTable.js?x=1'></script>--%>

    <%--<script type="text/javascript" src='../assets/js/form-elements.js'></script>--%>

    <%--<script src="../assets/js/jquery.maskedinput.js" type="text/javascript"></script>--%>

    <%--<script type="text/javascript" src='../assets/js/select2.js'></script>--%>

    <script type="text/javascript" src='../assets/js/jquery.multi-select.js'></script>

    <script type="text/javascript" src='../assets/js/bootstrap-colorpicker.js'></script>

    <script src='../assets/js/bootstrap-datepicker.js'></script>

    <script type="text/javascript" src="js/crm.js?x=4"></script>

    <script type="text/javascript" src="js/JsInteresado.js?x=10"></script>

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
            height: 28px;
            font-weight: 300; /* line-height: 40px; */
            color: black;
        }
        .form-group
        {
            margin: 3px;
        }
        .form-horizontal .control-label
        {
            padding-top: 5px;
        }
        .i-am-new
        {
            z-index: 100;
        }
        .help_block
        {
            margin-bottom: 0px;
            margin-top: 0px;
            vertical-align: middle;
        }
        .style1
        {
            color: #FF0000;
        }
        .style2
        {
            text-align: left;
        }
    </style>

    <script language="javascript" type="text/javascript">
// <!CDATA[

        function txtie_onclick() {

        }

// ]]>
    </script>

</head>
<body class="">
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
                    <div class="manage_buttons" id="divOpc">
                        <div class="row">
                            <div id="PanelBusqueda">
                                <div class="col-md-12 col-sm-12 col-lg-12">
                                    <div class="page_header">
                                        <div class="pull-left">
                                            <i class="icon ti-pencil-alt page_header_icon"></i><span class="main-text">Inscripción
                                                Interesados</span>
                                        </div>
                                        <div class="buttons-list">
                                            <div class="pull-right-btn">
                                                <a class="btn btn-primary" id="btnListar" onclick="fnListar()" href="#"><i class="ion-android-search">
                                                </i>&nbsp;Listar</a>
                                            </div>
                                        </div>
                                        <form class="form form-horizontal" id="frmbuscar" onsubmit="return false;" action="#"
                                        method="post">
                                        <div class="form-group">
                                            <div class="col-md-12">
                                                <label class="col-md-0 control-label ">
                                                    <span class="style1">(*) Campos obligatorios a filtrar</span></label>
                                            </div>
                                            <div class="col-md-6">
                                                <label class="col-md-3 control-label ">
                                                    Tipo Estudio<span class="style1">(*)</span></label></label>
                                                <div class="col-md-9">
                                                    <select name="cboTipoEstudio" class="form-control" id="cboTipoEstudio">
                                                        <option value="" selected="">-- Seleccione -- </option>
                                                    </select>
                                                </div>
                                            </div>
                                            <div class="col-md-6">
                                                <label class="col-md-3 control-label ">
                                                    Situación</label>
                                                <div class="col-md-9">
                                                    <select name="cboTipoPersona" class="form-control" id="cboTipoPersona">
                                                        <option value="" selected="">-- Seleccione -- </option>
                                                    </select>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="col-md-6">
                                                <label class="col-md-3 control-label ">
                                                    Convocatoria<span class="style1">(*)</span></label>
                                                <div class="col-md-9">
                                                    <select name="cboConvocatoria" class="form-control" id="cboConvocatoria">
                                                        <option value="" selected="">-- Seleccione -- </option>
                                                    </select>
                                                </div>
                                            </div>
                                            <div class="col-md-6">
                                                <label class="col-md-3 control-label ">
                                                    Comunicación</label>
                                                <div class="col-md-9">
                                                    <select name="cboComunicacion" class="form-control" id="cboComunicacion">
                                                        <%--<option value="T" selected="selected">TODOS </option>
                                                        <option value="1">Con Comunicación</option>
                                                        <option value="0">Sin Comunicación</option>--%>
                                                    </select>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="col-md-6">
                                                <label class="col-md-3 control-label ">
                                                    Evento<span class="style1">(*)</span></label>
                                                <div class="col-md-9">
                                                    <select name="cboEvento" class="form-control" id="cboEvento">
                                                        <option value="" selected="">-- Seleccione -- </option>
                                                    </select>
                                                </div>
                                            </div>
                                            <div class="col-md-6">
                                                <label class="col-md-3 control-label ">
                                                    Acuerdo</label>
                                                <div class="col-md-9">
                                                    <select name="cboAcuerdo" class="form-control" id="cboAcuerdo">
                                                        <option value="T" selected="selected">TODOS </option>
                                                        <option value="1">Con Acuerdo</option>
                                                        <option value="0">Sin Acuerdo</option>
                                                    </select>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="col-md-6 col-sm-12">
                                                <label class="col-md-3 control-label ">
                                                    DNI/Nombres</label>
                                                <div class="col-md-9">
                                                    <input type="text" id="txttexto" name="txttexto" class="form-control" placeholder="DNI / Apellidos Y Nombres"
                                                        style="border: 1px solid #d9d9d9; vertical-align: middle" />
                                                </div>
                                            </div>
                                            <div class="col-md-6 col-sm-12">
                                                <label class="col-md-3 control-label ">
                                                    Carrera Profesional</label>
                                                <div class="col-md-9">
                                                    <select name="cboCarreraProfesional" class="form-control" id="cboCarreraProfesional">
                                                        <option value="" selected="">-- Seleccione -- </option>
                                                    </select>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="col-md-6 col-sm-12">
                                                <label class="col-md-3 control-label">
                                                    Rango de Letras</label>
                                                <div class="col-md-2">
                                                    <input type="text" id="txtletraini" name="txtletraini" class="form-control" placeholder="A"
                                                        maxlength="1" style="border: 1px solid #d9d9d9; text-align: center" />
                                                </div>
                                                <label class="col-md-1 control-label">
                                                    -</label>
                                                <div class="col-md-2">
                                                    <input type="text" id="txtletrafin" name="txtletrafin" class="form-control" placeholder="Z"
                                                        maxlength="1" style="border: 1px solid #d9d9d9; text-align: center" />
                                                </div>
                                            </div>
                                        </form>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="modal fade" id="mdPerfilInteresado" role="dialog" aria-labelledby="myModalLabel"
                    aria-hidden="true" data-backdrop="static" data-keyboard="false">
                    <div class="modal-dialog modal-lg" style="width: 98%">
                        <div class="modal-content">
                            <div id="pagina">
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
                            <%--<i class="ion-android-list"></i>--%>
                            Listado de Interesados <span class="panel-options"><a class="panel-refresh" href="#">
                                <i class="icon ti-reload" onclick="fnListar(false)"></i></a><a class="panel-minimize"
                                    href="#"><i class="icon ti-angle-up"></i></a></span>
                        </h3>
                    </div>
                    <div class="panel-body">
                        <div class="row">
                            <a class="btn btn-green" id="btnAgregar" href="#" data-toggle="modal" data-target="#mdRegistro"
                                style="display: none"><i class="ion-android-add"></i>&nbsp;Agregar</a>
                        </div>
                        <div class="table-responsive">
                            <div id="tEvento_wrapper" class="dataTables_wrapper" role="grid">
                                <table id="tInteresados" class="display dataTable cell-border" width="100%" style="width: 100%;">
                                    <thead>
                                        <tr role="row">
                                            <td width="5%" style="font-weight: bold; width: 50px;" class="sorting_asc" tabindex="0"
                                                rowspan="1" colspan="1" aria-sort="ascending" aria-label="N.: activate to sort column descending">
                                                N.
                                            </td>
                                            <td width="20%" style="font-weight: bold; width: 200px;" class="sorting" tabindex="0"
                                                rowspan="1" colspan="1" aria-label="Semestre: activate to sort column ascending">
                                                Evento
                                            </td>
                                            <td width="9%" style="font-weight: bold; width: 90px;" class="sorting" tabindex="0"
                                                rowspan="1" colspan="1" aria-label="Semestre: activate to sort column ascending">
                                                Tipo Documento
                                            </td>
                                            <td width="9%" style="font-weight: bold; width: 90px;" class="sorting" tabindex="0"
                                                rowspan="1" colspan="1" aria-label="Semestre: activate to sort column ascending">
                                                N° Documento
                                            </td>
                                            <td width="13%" style="font-weight: bold; width: 130px;" class="sorting" tabindex="0"
                                                rowspan="1" colspan="1" aria-label="Dep. Académico: activate to sort column ascending">
                                                Apellido Paterno
                                            </td>
                                            <td width="13%" style="font-weight: bold; width: 130px;" class="sorting" tabindex="0"
                                                rowspan="1" colspan="1" aria-label="Dep. Académico: activate to sort column ascending">
                                                Apellidos Materno
                                            </td>
                                            <td width="13%" style="font-weight: bold; width: 130px;" class="sorting" tabindex="0"
                                                rowspan="1" colspan="1" aria-label="Dep. Académico: activate to sort column ascending">
                                                Nombres
                                            </td>
                                            <td width="10%" style="font-weight: bold; width: 100px;" class="sorting" tabindex="0"
                                                rowspan="1" colspan="1" aria-label="Detalle: activate to sort column ascending">
                                                Situación
                                            </td>
                                            <td width="8%" style="font-weight: bold; width: 80px;" class="sorting" tabindex="0"
                                                rowspan="1" colspan="1" aria-label="Opciones: activate to sort column ascending">
                                                Opciones
                                            </td>
                                        </tr>
                                    </thead>
                                    <tfoot>
                                        <tr>
                                            <th colspan="9" rowspan="1">
                                            </th>
                                        </tr>
                                    </tfoot>
                                    <tbody id="tbInteresados">
                                        <%--  <tr class="odd">
                                            <td valign="top" colspan="6" class="dataTables_empty">
                                                No se ha encontrado informacion
                                            </td>
                                        </tr>--%>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                        <!-- /panel -->
                        <div class="row">
                            <div class="modal fade" id="mdRegistro" role="dialog" aria-labelledby="myModalLabel"
                                aria-hidden="true" data-backdrop="static" data-keyboard="false" style="z-index: 0;">
                                <div class="modal-dialog">
                                    <div class="modal-content">
                                        <div class="modal-header" style="background-color: #E33439; color: White; font-weight: bold;">
                                            <button type="button" class="close" data-dismiss="modal" aria-label="Close" style="float: right;">
                                                <span aria-hidden="true" class="ti-close" style="color: White;"></span>
                                            </button>
                                            <h4 class="modal-title" id="myModalLabel3">
                                                Registrar Interesado</h4>
                                        </div>
                                        <div class="modal-body">
                                            <form id="frmRegistro" name="frmRegistro" enctype="multipart/form-data" class="form-horizontal"
                                            method="post" onsubmit="return false;" action="#">
                                            <div class="row">
                                                <div class="form-group">
                                                    <label class="col-sm-3 control-label">
                                                        Tipo Documento:</label>
                                                    <div class="col-sm-8">
                                                        <select class="form-control" id="cboTipoDocumento" name="cboTipoDocumento">
                                                            <option value="" selected="">-- Seleccione -- </option>
                                                        </select>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="form-group">
                                                    <label class="col-sm-3 control-label">
                                                        N° Documento:</label>
                                                    <div class="col-sm-4">
                                                        <input type="text" id="txtnum_doc" name="txtnum_doc" class="form-control" />
                                                    </div>
                                                    <div class="col-sm-2">
                                                        <a class="btn btn-primary" id="btnBuscaInt" href="#" onclick="fnBuscaxTipoyNumDoc()">
                                                            <i class="ion-android-search"></i></a>
                                                        <%--<a class="btn btn-green btn-icon-green" id="btnBack"
                                                                href="#" onclick=""><i class="ion ion-checkmark"></i></a>--%>
                                                    </div>
                                                </div>
                                            </div>
                                            <div id="datos">
                                                <div class="row">
                                                    <div class="form-group">
                                                        <label class="col-sm-3 control-label">
                                                            Apellido Paterno:</label>
                                                        <div class="col-sm-8">
                                                            <input type="text" id="txtapepat" name="txtapepat" class="form-control" />
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="form-group">
                                                        <label class="col-sm-3 control-label">
                                                            Apellido Materno:</label>
                                                        <div class="col-sm-8">
                                                            <input type="text" id="txtapemat" name="txtapemat" class="form-control" />
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="form-group">
                                                        <label class="col-sm-3 control-label">
                                                            Nombres:</label>
                                                        <div class="col-sm-8">
                                                            <input type="text" id="txtnombre" name="txtnombre" class="form-control" />
                                                        </div>
                                                    </div>
                                                </div>
                                                <div id="FormMensajeBusq">
                                                </div>
                                                <div class="row">
                                                    <div class="form-group">
                                                        <div class="col-md-12">
                                                            <center>
                                                                <button id="btnCoincidencias" name="btnCoincidencias" class="btn btn-orange" onclick="fnBuscaxApeyNom()"
                                                                    style="display: none">
                                                                    <span>Buscar Coincidencias</span> <i class="ion-android-search"></i>
                                                                </button>
                                                            </center>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div id="detalle_int">
                                                    <%--<div class="row">
                                                        <div class="form-group">
                                                            <label class="col-sm-3 control-label">
                                                                Fecha Nacimiento:</label>
                                                            <div class="col-sm-4" id="date-popup-group">
                                                                <div class="input-group date-block">
                                                                    <div class="input-group date" id="FechaInicio">
                                                                        <input name="txtfecnac" class="form-control" id="txtfecnac" style="text-align: right;"
                                                                            type="text" placeholder="__/__/____" data-provide="datepicker" />
                                                                            <span class="input-group-addon sm" id="btnfecha"><i class="ion ion-ios-calendar-outline"
                                                                            id="FechaNacimiento"></i></span>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>--%>
                                                    <div class="row">
                                                        <div class="form-group">
                                                            <label class="col-sm-3 control-label">
                                                                Fecha Nacimiento</label>
                                                            <div class="col-sm-5" id="date-popup-group">
                                                                <div class="input-group date-block">
                                                                    <div class="input-group date" id="FecNac">
                                                                        <input type="text" class="form-control" id="date" name="txtfecnac" data-provide="datepicker" />
                                                                        <span class="input-group-addon sm" id="btnfecha"><i class="ion ion-ios-calendar-outline"
                                                                            id="FechaNacimiento"></i></span>
                                                                    </div>
                                                                    <span class="help-block">Formato: dd/mm/aaaa</span>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="form-group">
                                                            <label class="col-sm-3 control-label">
                                                                Institución Educativa:</label>
                                                            <div class="col-sm-7">
                                                                <input type="text" id="txtie" name="txtie" class="form-control" onclick="return txtie_onclick()" />
                                                            </div>
                                                            <div class="col-sm-2">
                                                                <button type="button" id="btnie" class="btn btn-warning" data-toggle="modal" data-target="#mdInstitucionEd">
                                                                    <i class="ion-android-search"></i>
                                                                </button>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="form-group">
                                                            <label class="col-sm-3 control-label">
                                                                Dirección:</label>
                                                            <div class="col-sm-7">
                                                                <input type="text" id="txtdir" name="txtdir" class="form-control" />
                                                            </div>
                                                            <div class="col-sm-2">
                                                                <button type="button" id="btndir" class="btn btn-primary" <%--data-toggle="modal" data-target="#mdDireccion"--%>>
                                                                    <i class="ion-ios-home"></i>
                                                                </button>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="form-group">
                                                            <label class="col-sm-3 control-label">
                                                                Teléfono:</label>
                                                            <div class="col-sm-7">
                                                                <input type="text" id="txttel" name="txttel" class="form-control" />
                                                            </div>
                                                            <div class="col-sm-2">
                                                                <button type="button" id="btntel" class="btn btn-primary" <%--data-toggle="modal" data-target="#mdTelefono"--%>>
                                                                    <i class="ion-ios-telephone"></i>
                                                                </button>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="form-group">
                                                            <label class="col-sm-3 control-label">
                                                                Correo Electrónico:</label>
                                                            <div class="col-sm-7">
                                                                <input type="text" id="txtema" name="txtema" class="form-control" />
                                                            </div>
                                                            <div class="col-sm-2">
                                                                <button type="button" id="btnema" class="btn btn-primary" <%--data-toggle="modal" data-target="#mdEmail"--%>>
                                                                    <i class="ion-ios-email-outline"></i>
                                                                </button>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="form-group">
                                                            <label class="col-sm-3 control-label">
                                                                Carrera Profesional:</label>
                                                            <div class="col-sm-7">
                                                                <input type="text" id="txtcp" name="txtcp" class="form-control" />
                                                            </div>
                                                            <div class="col-sm-2">
                                                                <button type="button" id="btncp" class="btn btn-primary" <%--data-toggle="modal" data-target="#mdCarrera"--%>>
                                                                    <i class="ion-university"></i>
                                                                </button>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <center>
                                                        <button type="button" id="btnInscribir" class="btn btn-primary" onclick="fnInscribir();">
                                                            Inscribir</button>
                                                        <button type="button" class="btn btn-danger" id="btnCancelarReg" data-dismiss="modal">
                                                            Cancelar</button>
                                                    </center>
                                                </div>
                                            </div>
                                            <input type="hidden" id="hdcod_i" name="hdcod_i" value="0" />
                                            <input type="hidden" id="codie" name="codie" value="0" />
                                            <input type="hidden" id="codcpf" name="codcpf" value="0" />
                                            <input type="hidden" id="codeve" name="codeve" value="0" />
                                            </form>
                                            <div id="Coincidencias" style="display: none">
                                                <div class="row">
                                                    <div class="form-group">
                                                        <div class="col-sm-12">
                                                            <div class="table-responsive" style="font-size: 11px; font-weight: 300; line-height: 18px;">
                                                                <div id="Div2" class="dataTables_wrapper" role="grid">
                                                                    <table id="tCoincidencias" class="display dataTable cell-border" width="99%">
                                                                        <thead>
                                                                            <tr role="row">
                                                                                <td width="5%" style="font-weight: bold; width: 89px;" class="sorting" tabindex="0"
                                                                                    rowspan="1" colspan="1" aria-label="Semestre: activate to sort column ascending">
                                                                                    Doc.
                                                                                </td>
                                                                                <td width="10%" style="font-weight: bold; width: 89px;" class="sorting" tabindex="0"
                                                                                    rowspan="1" colspan="1" aria-label="Semestre: activate to sort column ascending">
                                                                                    Numero
                                                                                </td>
                                                                                <td width="25%" style="font-weight: bold; width: 54px;" class="sorting_asc" tabindex="0"
                                                                                    rowspan="1" colspan="1" aria-sort="ascending" aria-label="N.: activate to sort column descending">
                                                                                    Apellido Paterno
                                                                                </td>
                                                                                <td width="25%" style="font-weight: bold; width: 89px;" class="sorting" tabindex="0"
                                                                                    rowspan="1" colspan="1" aria-label="Semestre: activate to sort column ascending">
                                                                                    Apellido Materno
                                                                                </td>
                                                                                <td width="30%" style="font-weight: bold; width: 169px;" class="sorting" tabindex="0"
                                                                                    rowspan="1" colspan="1" aria-label="Dep. Académico: activate to sort column ascending">
                                                                                    Apellido Nombres
                                                                                </td>
                                                                                <td width="5%" style="font-weight: bold; width: 111px;" class="sorting" tabindex="0"
                                                                                    rowspan="1" colspan="1" aria-label="Opciones: activate to sort column ascending">
                                                                                    Seleccionar
                                                                                </td>
                                                                            </tr>
                                                                        </thead>
                                                                        <tfoot>
                                                                            <tr>
                                                                                <th colspan="6" rowspan="1">
                                                                                </th>
                                                                            </tr>
                                                                        </tfoot>
                                                                        <tbody id="TbCoincidencias">
                                                                        </tbody>
                                                                    </table>
                                                                    <br />
                                                                    <center>
                                                                        <button id="btnNuevoInt" name="btnNuevoInt" class="btn btn-green" onclick="NuevoInteresado()">
                                                                            <span>Nuevo Interesado</span> <i class="ion-android-person-add"></i>
                                                                        </button>
                                                                    </center>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div id="FormMensaje">
                                            </div>
                                        </div>
                                        <%--                                        <div class="row">
                                            <div class="form-group">
                                                <label class="col-sm-3 control-label">
                                                    Adjunto:</label>
                                                <div class="col-sm-8">
                                                    <input name="txtfile" type="file" id="txtfile" class="form-control">
                                                </div>
                                            </div>
                                        </div>--%>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <!-- row -->
                        <div class="row">
                            <div class="modal fade" id="mdDireccion" role="dialog" aria-labelledby="myModalLabel"
                                style="z-index: 1;" aria-hidden="true" data-backdrop="static" data-keyboard="false">
                                <div class="modal-dialog modal-lg">
                                    <div class="modal-content">
                                        <div class="modal-header" style="background-color: #E33439; color: White; font-weight: bold;">
                                            <button type="button" class="close" data-dismiss="modal" aria-label="Close" style="float: right;">
                                                <span aria-hidden="true" class="ti-close" style="color: White;"></span>
                                            </button>
                                            <h4 class="modal-title" id="H3">
                                                Registrar Dirección</h4>
                                        </div>
                                        <div class="modal-body">
                                            <form id="frmRegistroD" name="frmRegistroD" enctype="multipart/form-data" class="form-horizontal"
                                            method="post" onsubmit="return false;" action="#">
                                            <div class="row">
                                                <div class="form-group">
                                                    <label class="col-sm-3 control-label">
                                                        Región:</label>
                                                    <div class="col-sm-8">
                                                        <select class="form-control" id="cboRegionD" name="cboRegionD">
                                                            <option value="" selected="">-- Seleccione -- </option>
                                                        </select>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="form-group">
                                                    <label class="col-sm-3 control-label">
                                                        Provincia:</label>
                                                    <div class="col-sm-8">
                                                        <select class="form-control" id="cboProvinciaD" name="cboProvinciaD">
                                                            <option value="" selected="">-- Seleccione -- </option>
                                                        </select>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="form-group">
                                                    <label class="col-sm-3 control-label">
                                                        Distrito:</label>
                                                    <div class="col-sm-8">
                                                        <select class="form-control" id="cboDistritoD" name="cboDistritoD">
                                                            <option value="" selected="">-- Seleccione -- </option>
                                                        </select>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="form-group">
                                                    <label class="col-sm-3 control-label">
                                                        Dirección:</label>
                                                    <div class="col-sm-8">
                                                        <input type="text" id="txtDireccion" name="txtDireccion" class="form-control" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="form-group">
                                                    <label class="col-sm-3 control-label">
                                                        Domicilio Actual:</label>
                                                    <div class="col-sm-8">
                                                        <input type="checkbox" id="chkVigenciaD" name="chkVigenciaD" style="display: block;"
                                                            checked="checked" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div id="FormMensajeD">
                                            </div>
                                            <input type="hidden" id="hdcod_D" name="hdcod_D" value="0" />
                                            <center>
                                                <button type="button" id="Button10" class="btn btn-primary" onclick="fnGuardarDireccion();">
                                                    Guardar</button>
                                                <button type="button" class="btn btn-danger" id="Button11" data-dismiss="modal">
                                                    Cancelar</button>
                                            </center>
                                            </form>
                                            <br />
                                            <div class="row">
                                                <div class="form-group">
                                                    <div class="col-sm-12">
                                                        <div class="table-responsive" style="font-size: 12px; font-weight: 300; line-height: 18px;">
                                                            <div id="Div1" class="dataTables_wrapper" role="grid">
                                                                <table id="tDirecciones" class="display dataTable cell-border" width="100%" style="width: 100%;">
                                                                    <thead>
                                                                        <tr role="row">
                                                                            <td width="5%" style="font-weight: bold; width: 54px;" class="sorting_asc" tabindex="0"
                                                                                rowspan="1" colspan="1" aria-sort="ascending" aria-label="N.: activate to sort column descending">
                                                                                N.
                                                                            </td>
                                                                            <td width="30%" style="font-weight: bold; width: 89px;" class="sorting" tabindex="0"
                                                                                rowspan="1" colspan="1" aria-label="Semestre: activate to sort column ascending">
                                                                                Direccion
                                                                            </td>
                                                                            <td width="15%" style="font-weight: bold; width: 89px;" class="sorting" tabindex="0"
                                                                                rowspan="1" colspan="1" aria-label="Semestre: activate to sort column ascending">
                                                                                Región
                                                                            </td>
                                                                            <td width="15%" style="font-weight: bold; width: 169px;" class="sorting" tabindex="0"
                                                                                rowspan="1" colspan="1" aria-label="Semestre: activate to sort column ascending">
                                                                                Provincia
                                                                            </td>
                                                                            <td width="15%" style="font-weight: bold; width: 203px;" class="sorting" tabindex="0"
                                                                                rowspan="1" colspan="1" aria-label="Detalle: activate to sort column ascending">
                                                                                Distrito
                                                                            </td>
                                                                            <td width="10%" style="font-weight: bold; width: 111px;" class="sorting" tabindex="0"
                                                                                rowspan="1" colspan="1" aria-label="Opciones: activate to sort column ascending">
                                                                                Fecha Registro
                                                                            </td>
                                                                            <td width="5%" style="font-weight: bold; width: 111px;" class="sorting" tabindex="0"
                                                                                rowspan="1" colspan="1" aria-label="Opciones: activate to sort column ascending">
                                                                                Vigencia
                                                                            </td>
                                                                            <td width="5%" style="font-weight: bold; width: 111px;" class="sorting" tabindex="0"
                                                                                rowspan="1" colspan="1" aria-label="Opciones: activate to sort column ascending">
                                                                                Opciones
                                                                            </td>
                                                                        </tr>
                                                                    </thead>
                                                                    <tfoot>
                                                                        <tr>
                                                                            <th colspan="8" rowspan="1">
                                                                            </th>
                                                                        </tr>
                                                                    </tfoot>
                                                                    <tbody id="tbDirecciones">
                                                                        <%--<tr class="odd">
                                                                            <td valign="top" colspan="6" class="dataTables_empty">
                                                                                No se ha encontrado informacion
                                                                            </td>
                                                                        </tr>--%>
                                                                    </tbody>
                                                                </table>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <%--                                        <div class="row">
                                            <div class="form-group">
                                                <label class="col-sm-3 control-label">
                                                    Adjunto:</label>
                                                <div class="col-sm-8">
                                                    <input name="txtfile" type="file" id="txtfile" class="form-control">
                                                </div>
                                            </div>
                                        </div>--%>
                                        <div class="modal-footer">
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <!-- row -->
                        <div class="row">
                            <div class="modal fade" id="mdTelefono" role="dialog" aria-labelledby="myModalLabel"
                                style="z-index: 1;" aria-hidden="true" data-backdrop="static" data-keyboard="false">
                                <div class="modal-dialog">
                                    <div class="modal-content">
                                        <div class="modal-header" style="background-color: #E33439; color: White; font-weight: bold;">
                                            <button type="button" class="close" data-dismiss="modal" aria-label="Close" style="float: right;">
                                                <span aria-hidden="true" class="ti-close" style="color: White;"></span>
                                            </button>
                                            <h4 class="modal-title" id="H4">
                                                Registrar Teléfono</h4>
                                        </div>
                                        <div class="modal-body">
                                            <form id="frmRegistroT" name="frmRegistroT" enctype="multipart/form-data" class="form-horizontal"
                                            method="post" onsubmit="return false;" action="#">
                                            <div class="row">
                                                <div class="form-group">
                                                    <label class="col-sm-3 control-label">
                                                        Tipo:</label>
                                                    <div class="col-sm-8">
                                                        <select class="form-control" id="cboTipoTelefono" name="cboTipoTelefono">
                                                            <option value="" selected="">-- Seleccione -- </option>
                                                            <option value="FIJO">FIJO</option>
                                                            <option value="CELULAR">CELULAR </option>
                                                        </select>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="form-group">
                                                    <label class="col-sm-3 control-label">
                                                        Numero:</label>
                                                    <div class="col-sm-8">
                                                        <input type="text" id="txtnumeroTel" name="txtnumeroTel" class="form-control" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="form-group">
                                                    <label class="col-sm-3 control-label">
                                                        Detalle:</label>
                                                    <div class="col-sm-8">
                                                        <input type="text" id="txtdetalleTel" name="txtdetalleTel" class="form-control" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="form-group">
                                                    <label class="col-sm-3 control-label">
                                                        Vigencia:</label>
                                                    <div class="col-sm-8">
                                                        <input type="checkbox" id="chkVigenciaT" name="chkVigenciaT" style="display: block;"
                                                            checked="checked" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div id="FormMensajeT">
                                            </div>
                                            <input type="hidden" id="hdcod_T" name="hdcod_T" value="0" />
                                            <center>
                                                <button type="button" id="Button12" class="btn btn-primary" onclick="fnGuardarTelefono();">
                                                    Guardar</button>
                                                <button type="button" class="btn btn-danger" id="Button13" data-dismiss="modal">
                                                    Cancelar</button>
                                            </center>
                                            </form>
                                            <br />
                                            <div class="row">
                                                <div class="form-group">
                                                    <div class="col-sm-12">
                                                        <div class="table-responsive" style="font-size: 12px; font-weight: 300; line-height: 18px;">
                                                            <div id="Div8" class="dataTables_wrapper" role="grid">
                                                                <table id="tTelefonos" class="display dataTable cell-border" width="100%" style="width: 100%;">
                                                                    <thead>
                                                                        <tr role="row">
                                                                            <td width="10%" style="font-weight: bold; width: 54px;" class="sorting_asc" tabindex="0"
                                                                                rowspan="1" colspan="1" aria-sort="ascending" aria-label="N.: activate to sort column descending">
                                                                                N.
                                                                            </td>
                                                                            <td width="15%" style="font-weight: bold; width: 89px;" class="sorting" tabindex="0"
                                                                                rowspan="1" colspan="1" aria-label="Tipo: activate to sort column ascending">
                                                                                Tipo
                                                                            </td>
                                                                            <td width="30%" style="font-weight: bold; width: 89px;" class="sorting" tabindex="0"
                                                                                rowspan="1" colspan="1" aria-label="Número: activate to sort column ascending">
                                                                                Número
                                                                            </td>
                                                                            <%--                                                                 <td width="35%" style="font-weight: bold; width: 169px;" class="sorting" tabindex="0"
                                                                                rowspan="1" colspan="1" aria-label="Detalle: activate to sort column ascending">
                                                                                Detalle
                                                                            </td>--%>
                                                                            <td width="15%" style="font-weight: bold; width: 111px;" class="sorting" tabindex="0"
                                                                                rowspan="1" colspan="1" aria-label="Fecha: activate to sort column ascending">
                                                                                Fecha
                                                                            </td>
                                                                            <td width="10%" style="font-weight: bold; width: 111px;" class="sorting" tabindex="0"
                                                                                rowspan="1" colspan="1" aria-label="Vigencia: activate to sort column ascending">
                                                                                Vigencia
                                                                            </td>
                                                                            <td width="10%" style="font-weight: bold; width: 111px;" class="sorting" tabindex="0"
                                                                                rowspan="1" colspan="1" aria-label="Opciones: activate to sort column ascending">
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
                                                                    <tbody id="tbTelefonos">
                                                                    </tbody>
                                                                </table>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <%--                                        <div class="row">
                                            <div class="form-group">
                                                <label class="col-sm-3 control-label">
                                                    Adjunto:</label>
                                                <div class="col-sm-8">
                                                    <input name="txtfile" type="file" id="txtfile" class="form-control">
                                                </div>
                                            </div>
                                        </div>--%>
                                        <div class="modal-footer">
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <!-- row -->
                        <div class="row">
                            <div class="modal fade" id="mdEmail" role="dialog" aria-labelledby="myModalLabel"
                                style="z-index: 1;" aria-hidden="true" data-backdrop="static" data-keyboard="false">
                                <div class="modal-dialog">
                                    <div class="modal-content">
                                        <div class="modal-header" style="background-color: #E33439; color: White; font-weight: bold;">
                                            <button type="button" class="close" data-dismiss="modal" aria-label="Close" style="float: right;">
                                                <span aria-hidden="true" class="ti-close" style="color: White;"></span>
                                            </button>
                                            <h4 class="modal-title" id="H5">
                                                Registrar Email</h4>
                                        </div>
                                        <div class="modal-body">
                                            <form id="frmRegistroEmail" name="frmRegistroEmail" enctype="multipart/form-data"
                                            class="form-horizontal" method="post" onsubmit="return false;" action="#">
                                            <div class="row">
                                                <div class="form-group">
                                                    <label class="col-sm-3 control-label">
                                                        Tipo:</label>
                                                    <div class="col-sm-8">
                                                        <select class="form-control" id="cboTipoEMail" name="cboTipoEMail">
                                                            <option value="" selected="">-- Seleccione -- </option>
                                                            <option value="PERSONAL">Personal </option>
                                                            <option value="INSTITUCIONAL">Institucional </option>
                                                            <option value="OTRO">Otro </option>
                                                        </select>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="form-group">
                                                    <label class="col-sm-3 control-label">
                                                        Email:</label>
                                                    <div class="col-sm-8">
                                                        <input type="text" id="txtDescripcionEMail" name="txtDescripcionEMail" class="form-control" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="form-group">
                                                    <label class="col-sm-3 control-label">
                                                        Detalle:</label>
                                                    <div class="col-sm-8">
                                                        <input type="text" id="txtDetalleEMail" name="txtDetalleEMail" class="form-control" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="form-group">
                                                    <label class="col-sm-3 control-label">
                                                        Vigencia:</label>
                                                    <div class="col-sm-8">
                                                        <input type="checkbox" id="chkVigenciaEMail" name="chkVigenciaEMail" style="display: block;"
                                                            checked="checked" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div id="FormMensajeEmail">
                                            </div>
                                            <input type="hidden" id="hdcod_EMail" name="hdcod_EMail" value="0" />
                                            <center>
                                                <button type="button" id="BtnGuardarEMail" name="BtnGuardarEMail" class="btn btn-primary"
                                                    onclick="fnGuardarEMail();">
                                                    Guardar</button>
                                                <button type="button" class="btn btn-danger" id="BtnCancelarEMail" name="BtnCancelarEMail"
                                                    data-dismiss="modal">
                                                    Cancelar</button>
                                            </center>
                                            </form>
                                            <br />
                                            <div class="row">
                                                <div class="form-group">
                                                    <div class="col-sm-12">
                                                        <div class="table-responsive" style="font-size: 12px; font-weight: 300; line-height: 18px;">
                                                            <div id="Div11" class="dataTables_wrapper" role="grid">
                                                                <table id="tEMail" class="display dataTable cell-border" width="100%" style="width: 100%;">
                                                                    <thead>
                                                                        <tr role="row">
                                                                            <td width="10%" style="font-weight: bold; width: 54px;" class="sorting_asc" tabindex="0"
                                                                                rowspan="1" colspan="1" aria-sort="ascending" aria-label="N.: activate to sort column descending">
                                                                                N.
                                                                            </td>
                                                                            <td width="20%" style="font-weight: bold; width: 89px;" class="sorting" tabindex="0"
                                                                                rowspan="1" colspan="1" aria-label="Semestre: activate to sort column ascending">
                                                                                Tipo
                                                                            </td>
                                                                            <td width="30%" style="font-weight: bold; width: 89px;" class="sorting" tabindex="0"
                                                                                rowspan="1" colspan="1" aria-label="Semestre: activate to sort column ascending">
                                                                                Email
                                                                            </td>
                                                                            <td width="15%" style="font-weight: bold; width: 169px;" class="sorting" tabindex="0"
                                                                                rowspan="1" colspan="1" aria-label="Dep. Académico: activate to sort column ascending">
                                                                                Fecha Registro
                                                                            </td>
                                                                            <td width="10%" style="font-weight: bold; width: 111px;" class="sorting" tabindex="0"
                                                                                rowspan="1" colspan="1" aria-label="Vigencia: activate to sort column ascending">
                                                                                Vigencia
                                                                            </td>
                                                                            <td width="15%" style="font-weight: bold; width: 111px;" class="sorting" tabindex="0"
                                                                                rowspan="1" colspan="1" aria-label="Opciones: activate to sort column ascending">
                                                                                Opciones
                                                                            </td>
                                                                        </tr>
                                                                    </thead>
                                                                    <tfoot>
                                                                        <tr>
                                                                            <th colspan="6" rowspan="1">
                                                                            </th>
                                                                        </tr>
                                                                    </tfoot>
                                                                    <tbody id="tbEMail">
                                                                    </tbody>
                                                                </table>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="modal-footer">
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <!-- row -->
                        <div class="row">
                            <div class="modal fade" id="mdInstitucionEd" role="dialog" aria-labelledby="myModalLabel"
                                style="z-index: 1;" aria-hidden="true" data-backdrop="static" data-keyboard="false">
                                <div class="modal-dialog modal-lg">
                                    <div class="modal-content">
                                        <div class="modal-header" style="background-color: #E33439; color: White; font-weight: bold;">
                                            <button type="button" class="close" data-dismiss="modal" aria-label="Close" style="float: right;">
                                                <span aria-hidden="true" class="ti-close" style="color: White;"></span>
                                            </button>
                                            <h4 class="modal-title" id="H6">
                                                Buscar Institución Educativa</h4>
                                        </div>
                                        <div class="modal-body">
                                            <form id="frmRegistroIE" name="frmRegistroIE" enctype="multipart/form-data" class="form-horizontal"
                                            method="post" onsubmit="return false;" action="#">
                                            <div class="row">
                                                <div class="form-group">
                                                    <label class="col-sm-3 control-label">
                                                        Región:</label>
                                                    <div class="col-sm-8">
                                                        <select class="form-control" id="cboRegionIE" name="cboRegionIE">
                                                            <option value="" selected="">-- Seleccione -- </option>
                                                        </select>
                                                    </div>
                                                </div>
                                            </div>
                                            <%--<div class="row">
                                                <div class="form-group">
                                                    <label class="col-sm-3 control-label">
                                                        Nombre:</label>
                                                    <div class="col-sm-8">
                                                        <input type="text" id="txtnombreIE" name="txtnombreIE" class="form-control" />
                                                    </div>
                                                </div>
                                            </div>--%>
                                            </form>
                                            <br />
                                            <div class="row">
                                                <div class="form-group">
                                                    <div class="col-sm-12">
                                                        <div class="table-responsive" style="font-size: 12px; font-weight: 300; line-height: 18px;">
                                                            <div id="Div10" class="dataTables_wrapper" role="grid">
                                                                <table id="tInstitucionEducativa" class="display dataTable cell-border" width="100%"
                                                                    style="width: 100%;">
                                                                    <thead>
                                                                        <tr role="row">
                                                                            <td width="25%" style="font-weight: bold;" class="sorting_asc" tabindex="0" rowspan="1"
                                                                                colspan="1" aria-sort="ascending" aria-label="Institución Educativa : activate to sort column descending">
                                                                                Institución Educativa
                                                                            </td>
                                                                            <td width="30%" style="font-weight: bold;" class="sorting" tabindex="0" rowspan="1"
                                                                                colspan="1" aria-label="Dirección: activate to sort column ascending">
                                                                                Dirección
                                                                            </td>
                                                                            <td width="40%" style="font-weight: bold;" class="sorting" tabindex="0" style="text-align: center"
                                                                                rowspan="1" colspan="1" aria-label="Distrito / Provincia / Región : activate to sort column ascending">
                                                                                Distrito / Provincia / Región
                                                                            </td>
                                                                            <td width="3%" style="font-weight: bold;" class="sorting" tabindex="0" rowspan="1"
                                                                                colspan="1" aria-label="Opciones: activate to sort column ascending">
                                                                                Seleccionar
                                                                            </td>
                                                                        </tr>
                                                                    </thead>
                                                                    <tfoot>
                                                                        <tr>
                                                                            <th colspan="4" rowspan="1">
                                                                            </th>
                                                                        </tr>
                                                                    </tfoot>
                                                                    <tbody id="tbInstitucionEducativa">
                                                                        <%--<tr class="odd">
                                                                            <td valign="top" colspan="6" class="dataTables_empty">
                                                                                No se ha encontrado informacion
                                                                            </td>
                                                                        </tr>--%>
                                                                    </tbody>
                                                                </table>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <%--                                        <div class="row">
                                            <div class="form-group">
                                                <label class="col-sm-3 control-label">
                                                    Adjunto:</label>
                                                <div class="col-sm-8">
                                                    <input name="txtfile" type="file" id="txtfile" class="form-control">
                                                </div>
                                            </div>
                                        </div>--%>
                                        <div class="modal-footer">
                                            <center>
                                                <button type="button" class="btn btn-danger" id="Button17" data-dismiss="modal">
                                                    Cancelar</button>
                                            </center>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <!-- row -->
                        <div class="row">
                            <div class="modal fade" id="mdCarrera" role="dialog" aria-labelledby="myModalLabel"
                                style="z-index: 1;" aria-hidden="true" data-backdrop="static" data-keyboard="false">
                                <div class="modal-dialog modal-lg">
                                    <div class="modal-content">
                                        <div class="modal-header" style="background-color: #E33439; color: White; font-weight: bold;">
                                            <button type="button" class="close" data-dismiss="modal" aria-label="Close" style="float: right;">
                                                <span aria-hidden="true" class="ti-close" style="color: White;"></span>
                                            </button>
                                            <h4 class="modal-title" id="H7">
                                                Carrera Profesional</h4>
                                        </div>
                                        <div class="modal-body">
                                            <form id="frmRegistroCP" name="frmRegistroCP" enctype="multipart/form-data" class="form-horizontal"
                                            method="post" onsubmit="return false;" action="#">
                                            <div class="row">
                                                <div class="form-group">
                                                    <label class="col-sm-3 control-label">
                                                        Carrera Profesional:</label>
                                                    <div class="col-sm-8">
                                                        <select class="form-control" id="cboCarrera" name="cboCarrera">
                                                            <option value="" selected="">-- Seleccione -- </option>
                                                        </select>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="form-group">
                                                    <label class="col-sm-3 control-label">
                                                        Obervación:</label>
                                                    <div class="col-sm-8">
                                                        <input type="text" id="txtdetalleCpf" name="txtdetalleCpf" class="form-control" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="form-group">
                                                    <label class="col-sm-3 control-label">
                                                        Prioridad:</label>
                                                    <div class="col-sm-8">
                                                        <%--<input type="checkbox" id="chkVigenciaCpf" name="chkVigenciaCpf" style="display: block;"
                                                            checked="checked" />--%>
                                                        <select id="cboPrioridad" name="cboPrioridad">
                                                            <option value="" selected="">-- Seleccione -- </option>
                                                        </select>
                                                    </div>
                                                </div>
                                            </div>
                                            <div id="FormMensajeCP">
                                            </div>
                                            <input type="hidden" id="hdcod_CP" name="hdcod_CP" value="0" />
                                            <center>
                                                <button type="button" id="Button16" class="btn btn-primary" onclick="fnGuardarCpf();">
                                                    Guardar</button>
                                                <button type="button" class="btn btn-danger" id="Button17" data-dismiss="modal">
                                                    Cancelar</button>
                                            </center>
                                            </form>
                                            <br />
                                            <div class="row">
                                                <div class="form-group">
                                                    <div class="col-sm-12">
                                                        <div class="table-responsive" style="font-size: 12px; font-weight: 300; line-height: 18px;">
                                                            <div id="Div14" class="dataTables_wrapper" role="grid">
                                                                <table id="tCarrera" class="display dataTable cell-border" width="100%">
                                                                    <thead>
                                                                        <tr role="row">
                                                                            <td width="5%" style="font-weight: bold; width: 54px;" class="sorting_asc" tabindex="0"
                                                                                rowspan="1" colspan="1" aria-sort="ascending" aria-label="N.: activate to sort column descending">
                                                                                N.
                                                                            </td>
                                                                            <td width="31%" style="font-weight: bold; width: 89px;" class="sorting" tabindex="0"
                                                                                rowspan="1" colspan="1" aria-label="Semestre: activate to sort column ascending">
                                                                                Carrera Prof.
                                                                            </td>
                                                                            <td width="15%" style="font-weight: bold; width: 89px;" class="sorting" tabindex="0"
                                                                                rowspan="1" colspan="1" aria-label="Semestre: activate to sort column ascending">
                                                                                Tipo Estudio
                                                                            </td>
                                                                            <td width="35%" style="font-weight: bold; width: 89px;" class="sorting" tabindex="0"
                                                                                rowspan="1" colspan="1" aria-label="Semestre: activate to sort column ascending">
                                                                                Evento
                                                                            </td>
                                                                            <td width="8%" style="font-weight: bold; width: 169px;" class="sorting" tabindex="0"
                                                                                rowspan="1" colspan="1" aria-label="Dep. Académico: activate to sort column ascending">
                                                                                Fecha Reg.
                                                                            </td>
                                                                            <td width="3%" style="font-weight: bold; width: 169px;" class="sorting" tabindex="0"
                                                                                rowspan="1" colspan="1" aria-label="Dep. Académico: activate to sort column ascending">
                                                                                Prioridad
                                                                            </td>
                                                                            <td width="3%" style="font-weight: bold; width: 111px;" class="sorting" tabindex="0"
                                                                                rowspan="1" colspan="1" aria-label="Opciones: activate to sort column ascending">
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
                                                                    <tbody id="tbCarrera">
                                                                    </tbody>
                                                                </table>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <%--                                        <div class="row">
                                            <div class="form-group">
                                                <label class="col-sm-3 control-label">
                                                    Adjunto:</label>
                                                <div class="col-sm-8">
                                                    <input name="txtfile" type="file" id="txtfile" class="form-control">
                                                </div>
                                            </div>
                                        </div>--%>
                                        <div class="modal-footer">
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <!-- row -->
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
