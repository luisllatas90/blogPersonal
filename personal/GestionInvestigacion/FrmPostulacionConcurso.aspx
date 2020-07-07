<%@ Page Language="VB" AutoEventWireup="false" CodeFile="FrmPostulacionConcurso.aspx.vb"
    Inherits="GestionInvestigacion_FrmPostulacionConcurso" %>

<!DOCTYPE html>
<html>
<head>
    <title>Postular a Concurso</title>
    <%--Compatibilidad con IE--%>
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta http-equiv='X-UA-Compatible' content='IE=8' />
    <meta http-equiv='X-UA-Compatible' content='IE=10' />
    <%--Compatibilidad con IE--%>
    <!-- Cargamos css -->
    <link rel='stylesheet' href='../assets/css/bootstrap.min.css?x=1' />
    <link rel='stylesheet' href='../assets/css/material.css' />
    <link rel='stylesheet' href='../assets/css/style.css?x=1' />
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
    <!-- Cargamos JS -->

    <script type="text/javascript" src='../assets/js/jquery.js'></script>

    <script type="text/javascript" src='../assets/js/jquery-ui-1.10.3.custom.min.js'></script>

    <script src="../assets/js/bootstrap.min.js" type="text/javascript"></script>

    <script type="text/javascript" src='../assets/js/jquery.loadmask.min.js'></script>

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

    <script src="js/_General.js?x=9" type="text/javascript"></script>

    <script src="js/PostularConcurso.js?x=15" type="text/javascript"></script>

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
        table tbody tr td
        {
            word-wrap: break-word;
        }
    </style>
</head>
<body>
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
                                            <i class="icon ti-bookmark-alt page_header_icon"></i><span class="main-text">Postular
                                                a Concursos</span>
                                        </div>
                                        <div class="buttons-list">
                                            <div class="pull-right-btn">
                                                <button class="btn btn-primary" id="btnConsultar" value="Consultar" onclick="fnListarConcurso()">
                                                    Consultar</button>
                                            </div>
                                        </div>
                                        <form class="form form-horizontal" id="frmbuscar" onsubmit="return false;" action="#"
                                        method="post">
                                        <div class="form-group">
                                            <div class="col-md-7">
                                                <label class="col-md-3 control-label ">
                                                    Titulo de Concurso</label>
                                                <div class="col-md-9">
                                                    <input type="hidden" id="hdcod" name="hdcod" value="0" />
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
                <div class="row" id="ListaConcursos">
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
                                                <td width="5%" style="font-weight: bold; width: 54px; text-align: center" class="sorting_asc"
                                                    tabindex="0" rowspan="1" colspan="1" aria-sort="ascending" aria-label="N°: activate to sort column descending">
                                                    N°
                                                </td>
                                                <td width="35%" style="font-weight: bold; width: 89px; text-align: center" class="sorting"
                                                    tabindex="0" rowspan="1" colspan="1" aria-label="Nombre: activate to sort column ascending">
                                                    Titulo
                                                </td>
                                                <td width="15%" style="font-weight: bold; width: 169px; text-align: center" class="sorting"
                                                    tabindex="0" rowspan="1" colspan="1" aria-label="Tipo de Estudio: activate to sort column ascending">
                                                    Fecha Inicio
                                                </td>
                                                <td width="15%" style="font-weight: bold; width: 272px; text-align: center" class="sorting"
                                                    tabindex="0" rowspan="1" colspan="1" aria-label="Fecha Inicio: activate to sort column ascending">
                                                    Fecha Fin
                                                </td>
                                                <td width="15%" style="font-weight: bold; width: 272px; text-align: center" class="sorting"
                                                    tabindex="0" rowspan="1" colspan="1" aria-label="Fecha Inicio: activate to sort column ascending">
                                                    Tipo
                                                </td>
                                                <td width="15%" style="font-weight: bold; width: 111px; text-align: center" class="sorting"
                                                    tabindex="0" rowspan="1" colspan="1" aria-label="Opciones: activate to sort column ascending">
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
                                        <tbody id="tbConcursos">
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row" id="VerConcurso" style="display: none;">
                    <div class="panel-piluku">
                        <div class="panel-heading">
                            <h3 class="panel-title">
                                Detalle de Concurso
                            </h3>
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="form-group">
                                    <label class="col-sm-3 control-label" for="txttitulo">
                                        Título:</label>
                                    <div class="col-sm-8">
                                        <input type="text" id="txttitulo" name="txttitulo" class="form-control" disabled="disabled" />
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="form-group">
                                    <label class="col-sm-3 control-label">
                                        Breve Descripción:</label>
                                    <div class="col-md-8">
                                        <textarea class="form-control" cols="50" rows="3" id="txtdescripcion" name="txtdescripcion"
                                            disabled="disabled"></textarea>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="form-group">
                                    <label class="col-sm-3 control-label">
                                        Ámbito:</label>
                                    <div class="col-md-3">
                                        <select id="cboAmbito" name="cboAmbito" class="form-control" disabled="disabled">
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
                                    <div class="col-sm-2">
                                        <input name="txtfecini" class="form-control" id="txtfecini" style="text-align: right;"
                                            disabled="disabled" />
                                    </div>
                                    <div class="col-sm-1">
                                    </div>
                                    <label class="col-sm-3 control-label">
                                        Fecha Fin Evaluación:</label>
                                    <div class="col-sm-2">
                                        <input name="txtfecfineva" class="form-control" id="txtfecfineva" style="text-align: right;"
                                            disabled="disabled" />
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="form-group">
                                    <label class="col-sm-3 control-label">
                                        Fecha Fin Postulación:</label>
                                    <div class="col-sm-2">
                                        <input name="txtfecfin" class="form-control" id="txtfecfin" style="text-align: right;"
                                            disabled="disabled" />
                                    </div>
                                    <div class="col-sm-1">
                                    </div>
                                    <label class="col-sm-3 control-label">
                                        Fecha Publicación de Resultados:</label>
                                    <div class="col-sm-2">
                                        <input name="txtfecres" class="form-control" id="txtfecres" style="text-align: right;"
                                            disabled="disabled" />
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="form-group">
                                    <label class="col-sm-3 control-label">
                                        Tipo:</label>
                                    <div class="col-md-3">
                                        <select id="cbotipo" name="cbotipo" class="form-control" disabled="disabled">
                                            <option value="">--Seleccione--</option>
                                            <option value="0">INDIVIDUAL</option>
                                            <option value="1">UNIDISCIPLINARIO</option>
                                            <option value="2">GRUPAL</option>
                                            <option value="3">INDIVIDUAL/GRUPAL</option>
                                        </select>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="form-group">
                                    <label class="col-sm-3 control-label">
                                        Bases(Pdf)</label>
                                    <div class="col-sm-8">
                                        <div id="file_Bases">
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="form-group">
                                    <label class="col-sm-3 control-label">
                                        Estructura de la Propuesta</label>
                                    <div class="col-sm-8">
                                        <a href='Archivos/Concursos/Estructura/FORMATO PROPUESTA - FONDO CONCURSABLE DOCENTE.docx'
                                            target='_blank' style='font-weight: bold;'>Estructura de La Propuesta</a>
                                    </div>
                                    <div class="row">
                                        <div class="form-group">
                                            <div id="mensaje">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row" id="divPostulacion" style="display: none">
                                        <h3>
                                            Listado de Postulación</h3>
                                        <div class="table-responsive">
                                            <div id="Div2" class="dataTables_wrapper" role="grid">
                                                <table id="tPostulacion" name="tPostulacion" class="display dataTable" width="100%">
                                                    <thead>
                                                        <tr role="row">
                                                            <td style="font-weight: bold; width: 5%; text-align: center" class="sorting_asc"
                                                                tabindex="0" rowspan="1" colspan="1" aria-sort="ascending" aria-label="N°: activate to sort column descending">
                                                                N°
                                                            </td>
                                                            <td style="font-weight: bold; width: 60%; text-align: center" class="sorting" tabindex="0"
                                                                rowspan="1" colspan="1" aria-label="Nombre: activate to sort column ascending">
                                                                Titulo
                                                            </td>
                                                            <td style="font-weight: bold; width: 10%; text-align: center" class="sorting" tabindex="0"
                                                                rowspan="1" colspan="1" aria-label="Tipo de Estudio: activate to sort column ascending">
                                                                Fecha Postulación
                                                            </td>
                                                            <td style="font-weight: bold; width: 20%; text-align: center" class="sorting" tabindex="0"
                                                                rowspan="1" colspan="1" aria-label="Tipo de Estudio: activate to sort column ascending">
                                                                Etapa
                                                            </td>
                                                            <td style="font-weight: bold; width: 5%; text-align: center" class="sorting" tabindex="0"
                                                                rowspan="1" colspan="1" aria-label="Opciones: activate to sort column ascending">
                                                                Opciones
                                                            </td>
                                                        </tr>
                                                    </thead>
                                                    <tfoot>
                                                        <tr>
                                                            <th colspan="5" rowspan="1">
                                                            </th>
                                                        </tr>
                                                    </tfoot>
                                                    <tbody id="tbPostulacion">
                                                    </tbody>
                                                </table>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="form-group">
                                            <center>
                                                <button id="btnPostular" name="btnPostular" class="btn btn-success">
                                                    Postular</button>
                                                <button id="btnRegresar" name="btnRegresar" class="btn btn-danger">
                                                    Regresar</button>
                                            </center>
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
                                                Registrar Postulación</h4>
                                        </div>
                                        <div class="modal-body">
                                            <form id="frmRegistro" name="frmRegistro" enctype="multipart/form-data" class="form-horizontal"
                                            method="post" onsubmit="return false;" action="#">
                                            <div role="tabpanel">
                                                <!-- Nav tabs -->
                                                <ul class="nav nav-tabs nav-justified piluku-tabs piluku-noborder" role="tablist">
                                                    <li role="presentation" class="active"><a href="#hometabnb" aria-controls="home"
                                                        role="tab" data-toggle="tab" aria-expanded="true" id="tab1">Información General</a></li>
                                                    <li role="presentation" class=""><a href="#profiletabnb" aria-controls="profile"
                                                        role="tab" data-toggle="tab" aria-expanded="false" id="tab2">Equipo de Investigación</a></li>
                                                    <li role="presentation" class=""><a href="#messagestabnb" aria-controls="messages"
                                                        role="tab" data-toggle="tab" aria-expanded="false" id="tab3">Propuesta</a></li>
                                                    <li role="presentation" class=""><a href="#settingstabnb" aria-controls="settings"
                                                        role="tab" data-toggle="tab" aria-expanded="false" id="tab4">Archivo de Propuesta</a></li>
                                                </ul>
                                                <!-- Tab panes -->
                                                <div class="tab-content piluku-tab-content">
                                                    <div role="tabpanel" class="tab-pane active" id="hometabnb">
                                                        <div class="row">
                                                            <div class="form-group">
                                                                <label class="col-sm-3 control-label" for="txttituloPos">
                                                                    Título:</label>
                                                                <div class="col-sm-9">
                                                                    <input type="hidden" id="hdcodPos" name="hdcodPos" value="0" />
                                                                    <input type="hidden" id="hdcodCon" name="hdcodCon" value="0" />
                                                                    <input type="text" id="txttituloPos" name="txttituloPos" class="form-control" maxlength="1000" />
                                                                </div>
                                                            </div>
                                                        </div>
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
                                                                <label class="col-sm-3 control-label">
                                                                    Asignar Linea OCDE</label>
                                                                <div class="col-md-9">
                                                                    <input type="checkbox" id="chkOCDE" name="chkOCDE" />
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div id="ocde" style="display: none">
                                                            <div class="row">
                                                                <div class="form-group">
                                                                    <label class="col-sm-3 control-label">
                                                                        Área Temática:</label>
                                                                    <div class="col-md-8">
                                                                        <select id="cboArea" name="cboArea" class="form-control">
                                                                            <option value="" selected="">-- Seleccione -- </option>
                                                                        </select>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="form-group">
                                                                    <label class="col-sm-3 control-label">
                                                                        Sub Área:</label>
                                                                    <div class="col-md-8">
                                                                        <select id="cboSubArea" name="cboSubArea" class="form-control">
                                                                            <option value="" selected="">-- Seleccione -- </option>
                                                                        </select>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="form-group">
                                                                    <label class="col-sm-3 control-label">
                                                                        Disciplina:</label>
                                                                    <div class="col-md-8">
                                                                        <select id="cboDisciplina" name="cboDisciplina" class="form-control">
                                                                            <option value="" selected="">-- Seleccione -- </option>
                                                                        </select>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="form-group">
                                                                <label class="col-sm-3 control-label">
                                                                    Región:</label>
                                                                <div class="col-md-4">
                                                                    <select id="cboRegion" name="cboRegion" class="form-control">
                                                                        <option value="" selected="">-- Seleccione -- </option>
                                                                    </select>
                                                                </div>
                                                                <label class="col-md-1 control-label">
                                                                    Provincia:</label>
                                                                <div class="col-md-4">
                                                                    <select id="cboProvincia" name="cboProvincia" class="form-control">
                                                                        <option value="" selected="">-- Seleccione -- </option>
                                                                    </select>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="form-group">
                                                                <label class="col-md-3 control-label">
                                                                    Distrito:</label>
                                                                <div class="col-md-4">
                                                                    <select id="cboDistrito" name="cboDistrito" class="form-control">
                                                                        <option value="" selected="">-- Seleccione -- </option>
                                                                    </select>
                                                                </div>
                                                                <label class="col-md-1 control-label">
                                                                    Lugar:</label>
                                                                <div class="col-md-4">
                                                                    <input type="text" id="txtLugar" name="txtLugar" class="form-control" />
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div role="tabpanel" class="tab-pane" id="profiletabnb">
                                                        <div class="row">
                                                            <div class="form-group">
                                                                <label class="col-sm-2 control-label">
                                                                    Participante(s):</label>
                                                                <div class="col-md-2" id="rbIndividual">
                                                                    <input type="radio" name="rbtipo" id="rbtipoparticipante1" value="0">
                                                                    <label for="rbtipoparticipante1" style="color: Black; font-size: 13px">
                                                                        <span></span>&nbsp; Individual</label></div>
                                                                <div id="rbGrupo">
                                                                    <div class="col-md-2" id="rbGrupoU">
                                                                        <input type="radio" name="rbtipo" id="rbtipoparticipante2" value="1">
                                                                        <label for="rbtipoparticipante2" style="color: Black; font-size: 13px" id="nomGrupo">
                                                                            <span></span>&nbsp; Unidisciplinario</label></div>
                                                                    <div class="col-md-3" id="rbGrupoM">
                                                                        <input type="radio" name="rbtipo" id="rbtipoparticipante3" value="2">
                                                                        <label for="rbtipoparticipante3" style="color: Black; font-size: 13px" id="Label1">
                                                                            <span></span>&nbsp; Grupal</label></div>
                                                                    <%--<div class="col-md-5">
                                                                        <select id="cboGrupo" name="cboGrupo" class="form-control">
                                                                            <option value="" selected="">-- Seleccione -- </option>
                                                                        </select>
                                                                    </div>--%>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row" id="divDocente">
                                                            <label class="col-sm-2 control-label">
                                                                Docente:</label>
                                                            <div class="col-sm-6">
                                                                <input type="text" id="txtDocente" name="txtDocente" class="form-control" />
                                                            </div>
                                                            <div class="col-sm-2">
                                                                <button id="btnAgregarDocente" name="btnAgregarDocente" class="btn btn-orange" onclick="fnAgregarDocente();">
                                                                    Agregar</button>
                                                            </div>
                                                        </div>
                                                        <%--<div class="row" id="divalumno">
                                                            <div class="form-group">
                                                                <label class="col-sm-2 control-label">
                                                                    Alumno:</label>
                                                                <div class="col-md-5">
                                                                    <input type="text" id="txtalumno" name="txtalumno" class="form-control" />
                                                                </div>
                                                                <div class="col-md-2">
                                                                    <button id="btnAgregarAlumno" name="btnAgregarAlumno" class="btn btn-orange" onclick="fnAgregarAlumno();">
                                                                        Agregar</button></div>
                                                            </div>
                                                        </div>--%>
                                                        <div class="row">
                                                            <div class="table-responsive" style="font-size: 12px; font-weight: 300; line-height: 18px;">
                                                                <div id="Div1" class="dataTables_wrapper" role="grid">
                                                                    <table id="tGrupo" name="tGrupo" class="display dataTable" width="100%">
                                                                        <thead>
                                                                            <tr role="row">
                                                                                <th style="font-weight: bold; width: 3%; text-align: center;" class="sorting_asc"
                                                                                    tabindex="0" rowspan="1" colspan="1" aria-sort="ascending" aria-label="N°: activate to sort column descending">
                                                                                    N°
                                                                                </th>
                                                                                <th style="font-weight: bold; width: 39%; text-align: center;" class="sorting" tabindex="0"
                                                                                    rowspan="1" colspan="1" aria-label="Nombre: activate to sort column ascending">
                                                                                    Nombre
                                                                                </th>
                                                                                <th style="font-weight: bold; width: 28%; text-align: center;" class="sorting" tabindex="0"
                                                                                    rowspan="1" colspan="1" aria-label="Fecha Inicio: activate to sort column ascending">
                                                                                    Url DINA
                                                                                </th>
                                                                                <th style="font-weight: bold; width: 30%; text-align: center;" class="sorting" tabindex="0"
                                                                                    rowspan="1" colspan="1" aria-label="Tipo de Estudio: activate to sort column ascending">
                                                                                    Rol
                                                                                </th>
                                                                           <%--     <th style="font-weight: bold; width: 4%; text-align: center;" class="sorting" tabindex="0"
                                                                                    rowspan="1" colspan="1" aria-label="Tipo de Estudio: activate to sort column ascending">
                                                                                    Dedicación
                                                                                </th>--%>
                                                                                <th style="font-weight: bold; width: 4%; text-align: center;" class="sorting" tabindex="0"
                                                                                    rowspan="1" colspan="1" aria-label="Tipo de Estudio: activate to sort column ascending">
                                                                                    Quitar
                                                                                </th>
                                                                            </tr>
                                                                        </thead>
                                                                        <tfoot>
                                                                            <tr>
                                                                                <th colspan="6" rowspan="1">
                                                                                </th>
                                                                            </tr>
                                                                        </tfoot>
                                                                        <tbody id="tbGrupo">
                                                                        </tbody>
                                                                    </table>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div role="tabpanel" class="tab-pane" id="messagestabnb">
                                                        <div class="row">
                                                            <div class="form-group">
                                                                <label class="col-sm-3 control-label">
                                                                    Resumen:</label>
                                                                <div class="col-sm-8">
                                                                    <textarea id="txtresumen" name="txtresumen" class="form-control" cols="100%" rows="3"
                                                                        maxlength="8000"></textarea>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="form-group">
                                                                <label class="col-sm-3 control-label">
                                                                </label>
                                                                <div class="col-md-1">
                                                                    <button type="button" id="btnObjetivos" name="btnObjetivos" class="btn btn-green">
                                                                        Objetivos &nbsp;&nbsp;<i class="ion-ios-compose"></i>
                                                                    </button>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="form-group">
                                                                <label class="col-sm-3 control-label">
                                                                    Palabras Clave:</label>
                                                                <div class="col-sm-8">
                                                                    <textarea id="txtpalabras" name="txtpalabras" class="form-control" cols="100%" rows="2"
                                                                        maxlength="8000"></textarea>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="form-group">
                                                                <label class="col-sm-3 control-label">
                                                                    Justificación:</label>
                                                                <div class="col-sm-8">
                                                                    <textarea id="txtjustificacion" name="txtjustificacion" class="form-control" cols="100%"
                                                                        rows="3" maxlength="8000"></textarea>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="form-group">
                                                                <label class="col-sm-3 control-label">
                                                                    Fecha Inicio Proyecto:</label>
                                                                <div class="col-sm-3" id="date-popup-group">
                                                                    <div id="txtFechaInicio">
                                                                    </div>
                                                                    <div class="input-group date" id="FechaInicio">
                                                                        <input name="txtfeciniPos" class="form-control" id="txtfeciniPos" style="text-align: right;"
                                                                            type="text" placeholder="__/__/____" data-provide="datepicker" />
                                                                        <span class="input-group-addon sm"><i class="ion ion-ios-calendar-outline" id="FecInicial">
                                                                        </i></span>
                                                                    </div>
                                                                </div>
                                                                <label class="col-sm-2 control-label">
                                                                    Fecha Fin Proyecto:</label>
                                                                <div class="col-sm-3" id="date-popup-group">
                                                                    <div class="input-group date" id="FechaFin">
                                                                        <input name="txtfecfinPos" class="form-control" id="txtfecfinPos" style="text-align: right;"
                                                                            type="text" placeholder="__/__/____" data-provide="datepicker" />
                                                                        <span class="input-group-addon sm"><i class="ion ion-ios-calendar-outline" id="FecFinal">
                                                                        </i></span>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div role="tabpanel" class="tab-pane" id="settingstabnb">
                                                        <div class="row">
                                                            <div class="form-group">
                                                                <label class="col-sm-3 control-label">
                                                                    Propuesta(PDF)</label>
                                                                <div class="col-sm-8">
                                                                    <input type="file" id="file_producto" name="file_producto" />
                                                                    <div id="producto">
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <%--<div class="row" id="archivo2">
                                                            <div class="form-group">
                                                                <label class="col-sm-3 control-label">
                                                                    Presupuesto(Excel)</label>
                                                                <div class="col-sm-8">
                                                                    <input type="file" id="file_pto" name="file_pto" />
                                                                    <div id="pto">
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row" id="archivo3">
                                                            <div class="form-group">
                                                                <label class="col-sm-3 control-label">
                                                                    Cronograma(Excel)</label>
                                                                <div class="col-sm-8">
                                                                    <input type="file" id="file_cronograma" name="file_cronograma" />
                                                                    <div id="cronograma">
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row" id="archivo4">
                                                            <div class="form-group">
                                                                <label class="col-sm-3 control-label">
                                                                    Resultados Esperados(PDF)</label>
                                                                <div class="col-sm-8">
                                                                    <input type="file" id="file_resultados" name="file_resultados" />
                                                                    <div id="resultados">
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row" id="archivo5">
                                                            <div class="form-group">
                                                                <label class="col-sm-3 control-label">
                                                                    Declaración Jurada(PDF)</label>
                                                                <div class="col-sm-8">
                                                                    <input type="file" id="file_declaracion" name="file_declaracion" />
                                                                    <div id="declaracion">
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>--%>
                                                    </div>
                                                </div>
                                            </div>
                                            </form>
                                        </div>
                                        <div class="modal-footer" id="Footer_Modal">
                                            <div id="DivGuardar">
                                                <center>
                                                    <button type="button" id="btnA" class="btn btn-primary" onclick="fnConfirmar();">
                                                        Guardar</button>
                                                    <button type="button" id="btnCancelarReg" class="btn btn-danger" onclick="fnCancelar();">
                                                        Regresar</button>
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
                        <div class="row">
                            <div class="modal fade" id="mdObjetivos" role="dialog" aria-labelledby="myModalLabel"
                                style="z-index: 0;" aria-hidden="true" data-backdrop="static" data-keyboard="false">
                                <div class="modal-dialog modal-lg" id="Div5">
                                    <div class="modal-content">
                                        <div class="modal-header" style="background-color: #E33439; color: White; font-weight: bold;">
                                            <button type="button" class="close" data-dismiss="modal" aria-label="Close" style="float: right;">
                                                <span aria-hidden="true" class="ti-close" style="color: White;"></span>
                                            </button>
                                            <h4 class="modal-title" id="H2">
                                                Objetivos de Proyecto</h4>
                                        </div>
                                        <div class="modal-body">
                                            <form id="frmObjetivos" name="frmObjetivos" enctype="multipart/form-data" class="form-horizontal"
                                            method="post" onsubmit="return false;" action="#">
                                            <div class="row">
                                                <div class="form-group">
                                                    <label class="col-sm-3 control-label" for="txtobjetivo">
                                                        Descripción:</label>
                                                    <div class="col-sm-7">
                                                        <input type="text" id="txtobjetivo" name="txtobjetivo" class="form-control ui-autocomplete-input"
                                                            maxlength="8000" autocomplete="off" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="form-group">
                                                    <label class="col-sm-3 control-label" for="cboTipoObjetivo">
                                                        Tipo:</label>
                                                    <div class="col-sm-5">
                                                        <select id="cboTipoObjetivo" name="cboTipoObjetivo" class="form-control">
                                                            <option value="" selected="">-- Seleccione -- </option>
                                                            <option value="1">GENERAL</option>
                                                            <option value="2">ESPECÍFICO</option>
                                                        </select>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <center>
                                                    <button type="button" id="btnAgregarObjetivo" name="btnAgregarObjetivo" class="btn btn-success">
                                                        Agregar Objetivo
                                                    </button>
                                                </center>
                                            </div>
                                            <div class="row">
                                                <div class="form-group">
                                                    <div class="col-sm-12">
                                                        <div class="table-responsive" style="font-size: 12px; font-weight: 300; line-height: 18px;">
                                                            <div id="Div6" class="dataTables_wrapper" role="grid">
                                                                <table id="tObjetivos" name="tObjetivos" class="display dataTable" width="100%">
                                                                    <thead>
                                                                        <tr role="row">
                                                                            <td style="font-weight: bold; width: 70%; text-align: center" class="sorting_asc"
                                                                                tabindex="0" rowspan="1" colspan="1" aria-sort="ascending" aria-label="N°: activate to sort column descending">
                                                                                Objetivo
                                                                            </td>
                                                                            <td style="font-weight: bold; width: 20%; text-align: center" class="sorting" tabindex="0"
                                                                                rowspan="1" colspan="1" aria-label="Tipo de Estudio: activate to sort column ascending">
                                                                                Tipo
                                                                            </td>
                                                                            <td style="font-weight: bold; width: 10%; text-align: center" class="sorting" tabindex="0"
                                                                                rowspan="1" colspan="1" aria-label="Opciones: activate to sort column ascending">
                                                                                Opciones
                                                                            </td>
                                                                        </tr>
                                                                    </thead>
                                                                    <tfoot>
                                                                        <tr>
                                                                            <th colspan="3" rowspan="1">
                                                                            </th>
                                                                        </tr>
                                                                    </tfoot>
                                                                    <tbody id="tbObjetivos">
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
                    </div>
                </div>
            </div>
        </div>
        <div class="hiddendiv common">
        </div>
</body>
</html>
