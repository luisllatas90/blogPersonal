<%@ Page Language="VB" AutoEventWireup="false" CodeFile="GestionDocumetacion.aspx.vb" Inherits="administrativo_Sunedo_GestionDocumetacion" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html lang="en">
<head id="Head1" runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta http-equiv='X-UA-Compatible' content='IE=8' />
    <meta http-equiv='X-UA-Compatible' content='IE=10' />
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=0" />
    <meta http-equiv="Pragma" content="no-cache" />
    <link rel='stylesheet' href='../../academico/assets/css/bootstrap.min.css?x=1' />
    <link rel='stylesheet' href='../../academico/assets/css/material.css' />
    <link rel='stylesheet' href='../../academico/assets/css/style.css?x=1' />
    <link rel="stylesheet" href='../../assets/css/sweet-alerts/sweetalert.css'/>
    <script type="text/javascript" src='../../academico/assets/js/jquery.js'></script>

    <script type="text/javascript" src='../../academico/assets/js/app.js'></script>

    <script type="text/javascript" src="../../assets/jqwidgets/scripts/jquery-1.11.1.min.js"></script>

    <script type="text/javascript" src='../../academico/assets/js/bootstrap.min.js'></script>

    <script type="text/javascript" src='../../academico/assets/js/jquery.nicescroll.min.js'></script>

    <script type="text/javascript" src='../../academico/assets/js/wow.min.js'></script>

    <script type="text/javascript" src="../../academico/assets/js/jquery.nicescroll.min.js"></script>

    <script type="text/javascript" src='../../academico/assets/js/jquery.loadmask.min.js'></script>

    <script type="text/javascript" src='../../academico/assets/js/jquery.accordion.js'></script>

    <script type="text/javascript" src='../../academico/assets/js/materialize.js'></script>

    <script type="text/javascript" src='../../academico/assets/js/bic_calendar.js'></script>

    <script type="text/javascript" src='../../academico/assets/js/core.js'></script>

    <script type="text/javascript" src='../../academico/assets/js/jquery.countTo.js'></script>

    <script type="text/javascript" src="../../academico/assets/js/noty/jquery.noty.js"></script>

    <script type="text/javascript" src='../../academico/assets/js/noty/layouts/top.js'></script>

    <script type="text/javascript" src='../../academico/assets/js/noty/layouts/default.js'></script>

    <script type="text/javascript" src="../../academico/assets/js/noty/notifications-custom.js"></script>

    <script type="text/javascript" src='../../academico/assets/js/jquery.dataTables.min.js'></script>

    <link rel='stylesheet' href='../../academico/assets/css/jquery.dataTables.min.css' />
    <%--<script type="text/javascript" src='../assets/js/funciones.js'></script>

    <script type="text/javascript" src='../assets/js/funcionesDataTable.js?x=2'></script>--%>

    <script type="text/javascript" src='../../academico/assets/js/form-elements.js'></script>

    <script type="text/javascript" src='../../academico/assets/js/select2.js'></script>

    <script type="text/javascript" src='../../academico/assets/js/jquery.multi-select.js'></script>

    <script type="text/javascript" src='../../academico/assets/js/bootstrap-colorpicker.js'></script>

    <script src='../../academico/assets/js/bootstrap-datepicker.js'></script>

    
<script type="text/javascript" src="../../academico/assets/js/sweet-alert/sweetalert.min.js?x=3"></script>

<link rel="stylesheet" href="../../assets/jqwidgets/jqwidgets/styles/jqx.base.css" type="text/css" />
    <link href="../../assets/jqwidgets/jqwidgets/styles/jqx.light.css" rel="stylesheet" type="text/css" />
    
    <script type="text/javascript" src="../../assets/jqwidgets/jqwidgets/jqxcore.js"></script>
    <script type="text/javascript" src="../../assets/jqwidgets/jqwidgets/jqxdata.js"></script>
    <script type="text/javascript" src="../../assets/jqwidgets/jqwidgets/jqxbuttons.js"></script>
    <script type="text/javascript" src="../../assets/jqwidgets/jqwidgets/jqxscrollbar.js"></script>
    <script type="text/javascript" src="../../assets/jqwidgets/jqwidgets/jqxlistbox.js"></script>
    <script type="text/javascript" src="../../assets/jqwidgets/jqwidgets/jqxdropdownlist.js"></script>
    <script type="text/javascript" src="../../assets/jqwidgets/jqwidgets/jqxmenu.js"></script>
    <script type="text/javascript" src="../../assets/jqwidgets/jqwidgets/jqxinput.js"></script>
    <script type="text/javascript" src="../../assets/jqwidgets/jqwidgets/jqxgrid.js"></script>
    <script type="text/javascript" src="../../assets/jqwidgets/jqwidgets/jqxgrid.filter.js"></script>
    <script type="text/javascript" src="../../assets/jqwidgets/jqwidgets/jqxgrid.sort.js"></script>
    <script type="text/javascript" src="../../assets/jqwidgets/jqwidgets/jqxgrid.selection.js"></script>
    <script type="text/javascript" src="../../assets/jqwidgets/jqwidgets/jqxpanel.js"></script>
    <script type="text/javascript" src="../../assets/jqwidgets/jqwidgets/globalization/globalize.js"></script>
    <script type="text/javascript" src="../../assets/jqwidgets/jqwidgets/jqxcalendar.js"></script>
    <script type="text/javascript" src="../../assets/jqwidgets/jqwidgets/jqxdatetimeinput.js"></script>
    <script type="text/javascript" src="../../assets/jqwidgets/jqwidgets/jqxcheckbox.js"></script>
    <script type="text/javascript" src="../../assets/jqwidgets/jqwidgets/jqxgrid.pager.js"></script>
    <script type="text/javascript" src="../../assets/jqwidgets/jqwidgets/jqxdata.export.js"></script> 
    <script type="text/javascript" src="../../assets/jqwidgets/jqwidgets/jqxgrid.export.js"></script>     <script type="text/javascript" src="../../assets/jqwidgets/jqwidgets/jqxtooltip.js"></script> 

    <script type="text/javascript" src="js/gestionarArchivo.js" type="text/javascript"></script>
 

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
        jqx-grid-cell-left-align {
            overflow: hidden;
            text-overflow: ellipsis;
            padding-bottom: 2px;
            text-align: left;
            margin-right: 2px;
            margin-left: 4px;
            font-size: 8px;
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
                                            <i class="icon ti-volume page_header_icon"></i><span class="main-text">Documentación</span>
                                        </div>
                                        <div class="buttons-list">
                                            <div class="pull-right-btn">
                                                <a class="btn btn-primary" id="btnListar" href="#"><i class="ion-android-search"></i>
                                                    &nbsp;Listar</a> 
                                                    <%--<a class="btn btn-green" id="btnAgregar" href="#" ><i class="ion-android-add"></i>&nbsp;Agregar</a>--%>
                                            </div>
                                        </div>
                                        <form class="form form-horizontal" id="frmBuscarConvoc" onsubmit="return false;"
                                        action="#" method="post">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="form-group">
                                                    <label class="col-sm-2 col-md-4 control-label">
                                                       Año</label>
                                                    <div class="col-sm-4 col-md-4">
                                                        <select name="cboAnnioContable" class="form-control" id="cboAnnioContable">
                                                            <option value="" selected="">-- Seleccione -- </option>
                                                        </select>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label class="col-sm-2 col-md-4 control-label">
                                                       Indicador</label>
                                                    <div class="col-sm-4 col-md-4">
                                                        <select name="cboIndicadorFill" class="form-control" id="cboIndicadorFill">
                                                            <option value="" selected="">-- Seleccione -- </option>
                                                        </select>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        </form>
                                    </div>
                                </div>
                                <%--<div class="col-md-3 col-sm-12  col-lg-3">
                                    <div class="buttons-list">
                                        <div class="right pull-right">
                                            <ul class="right_bar">
                                                <a href="javascript:get_loc();">Mostrar localización</a>
                                                 <li class="list-unstyled"><i class="icon ion-checkmark text-primary"></i>&nbsp;Consultar</li>
                                                <li class="list-unstyled"><i class="icon ion-checkmark text-primary"></i>&nbsp;Registrar</li>
                                                <li class="list-unstyled"><i class="icon ion-checkmark text-primary"></i>&nbsp;Modificar</li>
                                                <li class="list-unstyled"><i class="icon ion-checkmark text-primary"></i>&nbsp;Eliminar</li>
                                                <li class="list-unstyled"><i class="icon ion-checkmark text-primary"></i>&nbsp;Imprimir</li>
                                            </ul>
                                        </div>
                                    </div>
                                </div>--%>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <!-- panel -->
                    <div class="panel panel-piluku" id="PanelLista">
                        <div class="panel-heading">
                            <h3 class="panel-title">
                                Listado de Documentos Indicadores y Medios de Verificación  <span class="panel-options"><a class="panel-refresh" href="#">
                                    <i class="icon ti-reload" onclick="fnBuscarConvocatoria(false)"></i></a><a class="panel-minimize"
                                        href="#"><i class="icon ti-angle-up"></i></a></span>
                            </h3>
                        </div>
                        <div class="panel-body">
                           
                                <div id="jqxgrid" class="table-responsive" role="grid">
                                     
                                </div>
                            
                        </div>
                    </div>
                    <!-- /panel -->
                    <div class="row">
                        <div class="modal fade" id="mdRegistro" role="dialog" aria-labelledby="myModalLabel" style='z-index:99999999'
                            aria-hidden="true" data-backdrop="static" data-keyboard="false">
                            <div class="modal-dialog modal-md" id="modalReg" role="document">
                                <div class="modal-content">
                                    <div class="modal-header" style="background-color: #E33439; color: White; font-weight: bold;">
                                        <button type="button" class="close" data-dismiss="modal" aria-label="Close" style="float: right;">
                                            <span aria-hidden="true" class="ti-close" style="color: White;"></span>
                                        </button>
                                        <h4 class="modal-title" id="myModalLabel3">
                                            Registro de Documentos</h4>
                                    </div>
                                    <div class="modal-body">
                                        <form id="frmRegistro" name="frmRegistro" enctype="multipart/form-data" class="form-horizontal"
                                        method="post" onsubmit="return false;" action="#">
                                       <input type="hidden" id="Funcion" name="Funcion" value="Grabar" />
                                          <input type="hidden" id="op" name="op" value="1" />
                                          <input type="hidden" id="txtCodigoFml" name="txtCodigoFml" value="0" />
                                          <input type="hidden" id="txtSession" name="txtSession" value="0" runat="server"/>
                                       <div class="row">
                                            <div class="form-group">
                                                 <div class="col-sm-12">
                                                    <div class="row">
                                                        <div class="form-group">
                                                            <label class="col-sm-4 control-label">
                                                                Indicador</label>
                                                            <div class="col-sm-7">
                                                                <select class="form-control" id="cboIndicador" name="cboIndicador">
                                                                    <option value="" selected="">-- Seleccione -- </option>
                                                                </select>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    
                                                    <div class="row">
                                                        <div class="form-group">
                                                            <label class="col-sm-4 control-label">
                                                                Medio Verificación:</label>
                                                            <div class="col-sm-7">
                                                                <select class="form-control" id="cboMedioVerficacion" name="cboMedioVerficacion">
                                                                    <option value="" selected="">-- Seleccione -- </option>
                                                                </select>     
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="form-group">
                                                            <label class="col-sm-4 control-label">
                                                                Tipo Documento:</label>
                                                            <div class="col-sm-7">
                                                                <select class="form-control" id="cboDocumento" name="cboDocumento">
                                                                    <option value="" selected="">-- Seleccione -- </option>
                                                                </select>     
                                                            </div>
                                                        </div>
                                                    </div>
                                                     <div class="row">
                                                        <div class="form-group">
                                                            <label class="col-sm-4 control-label">
                                                                Responsable:</label>
                                                            <div class="col-sm-7">
                                                                 <select class="form-control" id="cboResponsable" name="cboResponsable">
                                                                    <option value="" selected="">-- Seleccione -- </option>
                                                                </select>
                                                            </div>
                                                        </div>
                                                    </div>
                                                     
                                                    <div class="row">
                                                        <div class="form-group">
                                                            <label class="col-sm-4 control-label">
                                                                Frecuencia:</label>
                                                            <div class="col-sm-7">
                                                                <input type="text" id="txtFrecuencia" name="txtFrecuencia" class="form-control" readonly />
                                                            </div>
                                                        </div>
                                                    </div>
                                                     
                                                    
                                                    
                                                    
                                                    <div class="row">
                                                        <div class="form-group">
                                                            <label class="col-sm-4 control-label">Periódo:</label>
                                                            <div class="col-sm-3 col-md-3">
                                                               <select class="form-control" id="cmbAnio" name="cmbAnio">
                                                                    <option value="" selected="">-- Seleccione -- </option>
                                                                </select>
                                                            </div>
                                                            <div class="col-sm-4 col-md-4">
                                                               <select class="form-control" id="cmbPeriodo" name="cmbPeriodo">
                                                                    <option value="" selected="">-- Seleccione -- </option>
                                                                </select>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    
                                                    <div class="row">
                                                        <div class="form-group">
                                                            <label class="col-sm-4 control-label">
                                                                Documento:</label>
                                                            <div class="col-sm-7">
                                                                <input type="file" id="fileData" name="fileData" class="form-control" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                     <div class="row">
                                                        <div class="form-group">
                                                            <label class="col-sm-4 control-label">
                                                                Observación:</label>
                                                            <div class="col-sm-7 col-md-7">
                                                                <textarea cols="40" rows="3" >
                                                                </textarea>
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
                                    <div class="modal-footer">
                                        <center>
                                            <button type="button" id="btnGrabar"  name="btnGrabar"class="btn btn-primary">
                                                Guardar</button>
                                            <button type="button" class="btn btn-danger" id="btnCancelarReg" data-dismiss="modal">
                                                Cancelar</button>
                                        </center>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <!-- row -->
                    <div class="row">
                         <div class="modal fade" id="divVersion" role="dialog" aria-labelledby="myModalLabel" style='z-index:99999999'
                            aria-hidden="true" data-backdrop="static" data-keyboard="false">
                            <div class="modal-dialog modal-lg" id="Div2" role="document">
                                <div class="modal-content">
                                    <div class="modal-header" style="background-color: #E33439; color: White; font-weight: bold;">
                                        <button type="button" class="close" data-dismiss="modal" aria-label="Close" style="float: right;">
                                            <span aria-hidden="true" class="ti-close" style="color: White;"></span>
                                        </button>
                                        <h4 class="modal-title" id="H1">
                                            Listado de Versión de Documentos</h4>
                                    </div>
                                    <div class="modal-body">
                                         <table class="display dataTable  cell-border right" id="tblDocumentos" style="width:90%;font-size:15px;">
                                            <thead>
                                                <tr role="row">		
                                                    <th style="width:10%">Id</th>
                                                    <th style="width:30%">Documento</th>
                                                    <th style="width:20%">Nombre. Documento</th>
                                                    <th style="width:20%">Fecha</th>											                                                    
                                                    <th style="width:30%">Observación</th>
                                                    <th style="width:10%"></th>
                                                </tr>
                                            </thead>
                                            <tbody id="TDocs" runat="server">
                                            </tbody>									 
                                        </table>	
                                    </div>
                                    <div class="modal-footer">
                                        <center>                                          
                                            <button type="button" class="btn btn-danger" id="Button2" data-dismiss="modal">
                                                Salir</button>
                                        </center>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <!-- row -->
                </div>
                <!-- row -->
            </div>
        </div>
    </div>
    <div class="hiddendiv common">
    </div>
</body>
</html>
--%> 