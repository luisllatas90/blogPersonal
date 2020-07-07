<%@ Page Language="VB" AutoEventWireup="false" CodeFile="GenerarMatrizCSV.aspx.vb" Inherits="academico_PredictorDiserccion_GenerarMatrizCSV" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html lang="en">
<head>
    <title id='Description'>jqxGrid supports custom Filter Menus. To create such menus, you will have to override the built-in Filter Menu by implmenting the "createfilterpanel" function.
    </title>
<meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta http-equiv='X-UA-Compatible' content='IE=8' />
    <meta http-equiv='X-UA-Compatible' content='IE=10' />
    <meta http-equiv="X-UA-Compatible" content="IE=edge"/> 
    
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7, IE=EmulateIE9, IE=EDGE" /> 
        
     <link rel='stylesheet' href='../assets/css/bootstrap.min.css?x=1' />
    <link rel='stylesheet' href='../assets/css/material.css' />
    <link rel='stylesheet' href='../assets/css/style.css?x=1' />
    <link rel="stylesheet" href="../../assets/jqwidgets/jqwidgets/styles/jqx.base.css" type="text/css" />
    <link href="../../assets/jqwidgets/jqwidgets/styles/jqx.light.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../../assets/jqwidgets/scripts/jquery-1.11.1.min.js"></script>
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
  <%--  <script type="text/javascript" src="../../assets/jqwidgets/scripts/demos.js"></script>--%>
   <%-- <script type="text/javascript" src="js/generatedata.js"></script>--%>

    <script src="js/ExportarCVS.js" type="text/javascript"></script>
    <script src="js/JqxTable.js" type="text/javascript"></script>
</head>
<body class=''>

    <div class="piluku-preloader text-center hidden" >
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
                                        <form class="form form-horizontal" id="frmBuscarConvoc" onsubmit="return false;" action="#" method="post">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="form-group">
                                                    <label class="col-sm-12 col-md-3 control-label">Facultad</label>
                                                    <div class="col-sm-12 col-md-5">
                                                        <select name="cboFacultad" class="form-control" id="cboFacultad">
                                                            <option value="" selected="">-- Seleccione -- </option>
                                                        </select>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label class="col-sm-12 col-md-3 control-label">Escuela Profesional</label>
                                                    <div class="col-sm-12 col-md-5">
                                                        <select name="cboEscuela" class="form-control" id="cboEscuela">
                                                            <option value="" selected="">-- Seleccione -- </option>
                                                        </select>
                                                    </div>
                                                </div>
                                                 <div class="form-group">
                                                    <label class="col-sm-6 col-md-3 control-label">Fecha de la Información</label>
                                                    <div class="col-sm-12 col-md-3">
                                                         <div id='txtFecha'>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-6 col-md-2"><input type="button" value="Consultar" id='jqxConsultar' class="btn btn-primary" /></div>
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
                    <div class="panel panel-piluku" id="PanelLista">
                       <div class="panel-heading">
                            <h3 class="panel-title">
                               Listado <span class="panel-options"><a class="panel-refresh" href="#">
                                <i class="icon ti-reload" onclick="fnBuscarConvocatoria(false)"></i></a><a class="panel-minimize"
                                        href="#"><i class="icon ti-angle-up"></i></a></span>
                            </h3>
                        </div>
                        <div class="panel-body">
                            <div class="col-sm-1 col-md-1" style="float: left;" id="jqxlistbox">ds</div>
                             <div class="col-sm-10 col-md-10" id='jqxWidget' style="font-size: 13px; font-family: Verdana; float: left;">
                                <div id="jqxgrid">
                                </div>
                                <div id="eventslog" style="margin-top: 30px;">
                                    <div style="width: 400px; float: left; margin-right: 10px;">
                                        <input value="Quitar filtros" id="clearfilteringbutton" type="button" />
                                        <div style="margin-top: 10px;" id='filterbackground'>Filtrar en Background</div>
                                        <div style="margin-top: 10px;" id='filtericons'>Mostrar todos los iconos de filtros</div>
                                    </div>
                                    <div style="float: left;">
                                        Logs:
                                        <div style="border: none;" id="events">
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
   
</body>
</html>