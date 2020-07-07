<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmGenerarCsv.aspx.vb" Inherits="academico_PredictorDiserccion_frmGenerarCsv" %>


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
    <link rel="stylesheet" href='../../assets/css/sweet-alerts/sweetalert.css'/>
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

 
<script type="text/javascript" src="../../assets/js/sweet-alert/sweetalert.min.js?x=3"></script>

    <script type="text/javascript" src="js/ExportarCVS.js" type="text/javascript"></script>
 

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
            margin: 2px;
        }
        .form-horizontal .control-label
        {
            padding-top: 5px;
        }
        </style>
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
                                            <i class="icon ti-volume page_header_icon"></i><span class="main-text">Exportar Matriz para estudio de Poblaciones</span>
                                        </div>
                                        <div class="buttons-list">
                                            <div class="pull-right-btn">
                                                <a class="btn btn-primary" id="btnListar" href="#"><i class="ion-android-search"></i>
                                                    &nbsp;Exportar</a>  
                                            </div>
                                        </div>
                                        <form class="form form-horizontal" id="frmBuscarConvoc" onsubmit="return false;"
                                        action="#" method="post">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="form-group">
                                                    <label class="col-sm-12 col-md-4 control-label">
                                                        Facultad</label>
                                                    <div class="col-sm-12 col-md-8">
                                                        <select name="cboCicloAcademico" class="form-control" id="cboCicloAcademico">
                                                            <option value="" selected="">-- Seleccione -- </option>
                                                        </select>
                                                    </div>
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
                               Parametros de Consulta <span class="panel-options"><a class="panel-refresh" href="#">
                                <i class="icon ti-reload" onclick="fnBuscarConvocatoria(false)"></i></a><a class="panel-minimize"
                                        href="#"><i class="icon ti-angle-up"></i></a></span>
                            </h3>
                        </div>
                        <div class="panel-body">
                            <form id="frmRegistro" name="frmRegistro" enctype="multipart/form-data" class="form-horizontal"
                                        method="post" onsubmit="return false;" action="#">
                                       <input type="hidden" id="Funcion" name="Funcion" value="Grabar" />
                                          <input type="hidden" id="op" name="op" value="1" />
                                          <input type="hidden" id="txtCodigoFml" name="txtCodigoFml" value="0" />
                                          <input type="hidden" id="txtSession" name="txtSession" value="0" runat="server"/>
                                       <div class="row">
                                            <div class="form-group">
                                                 <div class="col-sm-8">
                                                    <div class="row">
                                                        <div class="form-group">
                                                            <label class="col-sm-4 control-label">
                                                                Escuela:</label>
                                                            <div class="col-sm-6">
                                                                <select class="form-control" id="cboCicloAcademicoR" name="cboCicloAcademicoR">
                                                                    <option value="" selected="">-- Seleccione -- </option>
                                                                </select>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="form-group">
                                                            <label class="col-sm-4 control-label">
                                                              Entre los Ciclos:</label>
                                                            <div class="col-sm-3">
                                                                <select class="form-control" id="Select1" name="cboCicloAcademicoR">
                                                                    <option value="" selected="">-- Seleccione -- </option>
                                                                </select>                                                             
                                                            </div>
                                                            <div class="col-sm-3">
                                                               <select class="form-control" id="Select2" name="cboCicloAcademicoR">
                                                                    <option value="" selected="">-- Seleccione -- </option>
                                                                </select>                                                    
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="form-group">
                                                            <label class="col-sm-4 control-label">
                                                                Entre las Edades:</label>
                                                            <div class="col-sm-3">
                                                                <input type="text" id="txtIntercep" name="txtIntercep" class="form-control" />
                                                            </div>
                                                           
                                                            <div class="col-sm-3">
                                                                <input type="text" id="Text3" name="txtIntercep" class="form-control" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                     
                                                      <div class="row">
                                                        <div class="form-group">
                                                            <label class="col-sm-4 control-label">
                                                                Sexo:</label>
                                                            <div class="col-sm-5">
                                                                 <select class="form-control" id="Select3" name="cboCicloAcademicoR">
                                                                    <option value="" selected="">-- Seleccione -- </option>
                                                                </select>   
                                                            </div>
                                                        </div>
                                                    </div>
                                                    
                                                    <div class="row">
                                                        <div class="form-group">
                                                            <label class="col-sm-4 control-label">
                                                                Ubigeo:</label>
                                                            <div class="col-sm-5">
                                                                <input type="text" id="Text2" name="txtIntercep" class="form-control" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="form-group">
                                                            
                                                        </div>
                                                    </div>
                                                 </div>                                                 
                                            </div>
                                        </div>                                                                                                                    
                                   </form>
                        </div>
                    </div>
                    <!-- /panel -->
                    
                    <!-- row -->
                   
                    <!-- row -->
                </div>
                <!-- row -->
            </div>
        </div>
    </div>
     
</body>
</html>
--%> 